// Copyright 2020 ONIXLabs
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

#if !NET11_0_OR_GREATER
using System;
using System.Buffers.Binary;
using System.Security.Cryptography;

namespace OnixLabs.Security.Cryptography;

/// <summary>
/// Provides <see cref="Aes"/> with the AES Key Wrap methods of RFC 3394 that .NET 11 adds, for .NET 8, 9 and 10. JOSE uses this algorithm
/// as <c>A128KW</c>, <c>A192KW</c> and <c>A256KW</c> (RFC 7518 §4.4).
/// </summary>
/// <remarks>
/// <para>
/// Why: the members have the names, parameters and exceptions of .NET 11's own <c>Aes.EncryptKeyWrap</c>, <c>Aes.DecryptKeyWrap</c>,
/// <c>Aes.TryDecryptKeyWrap</c> and <c>Aes.GetKeyWrapLength</c>, so the same calling code compiles on every target, and on .NET 11 calls
/// .NET's own. This file is not compiled for .NET 11.
/// </para>
/// <para>
/// This is not <c>Aes.EncryptKeyWrapPadded</c>, which .NET 10 has: that is AES Key Wrap with Padding (RFC 5649), which uses a different
/// initial value and so produces different output for the same input.
/// </para>
/// </remarks>
internal static class AesExtensions
{
    /// <summary>
    /// The length in bytes of one key wrap block, a 64-bit half of an AES block.
    /// </summary>
    private const int SemiblockLength = 8;

    /// <summary>
    /// The smallest plaintext that can be wrapped, in bytes: two semiblocks, as RFC 3394 §2 requires.
    /// </summary>
    private const int MinimumPlaintextLength = 2 * SemiblockLength;

    /// <summary>
    /// The default initial value of RFC 3394 §2.2.3.1, A6A6A6A6A6A6A6A6.
    /// </summary>
    private const ulong DefaultInitialValue = 0xA6A6A6A6A6A6A6A6;

    /// <summary>
    /// The number of wrapping rounds, j = 0 to 5, of RFC 3394 §2.2.1.
    /// </summary>
    private const int Rounds = 6;

    /// <summary>
    /// Provides AES Key Wrap length calculation.
    /// </summary>
    extension(Aes)
    {
        /// <summary>
        /// Gets the length of the ciphertext that AES Key Wrap produces for a plaintext of the specified length.
        /// </summary>
        /// <param name="plaintextLengthInBytes">The length of the plaintext, in bytes.</param>
        /// <returns>Returns the length of the wrapped key, which is 8 bytes longer than the plaintext.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="plaintextLengthInBytes"/> is shorter than 16 bytes, or is not a multiple of 8 bytes.
        /// </exception>
        public static int GetKeyWrapLength(int plaintextLengthInBytes) => IsValidPlaintextLength(plaintextLengthInBytes)
            ? checked(plaintextLengthInBytes + SemiblockLength)
            : throw new ArgumentOutOfRangeException(
                nameof(plaintextLengthInBytes),
                "The plaintext is not a valid length for the Key Wrap algorithm.");
    }

    /// <summary>
    /// Provides AES Key Wrap encryption and decryption with the key of the current <see cref="Aes"/> instance.
    /// </summary>
    /// <param name="aes">The <see cref="Aes"/> instance whose key is the key-encryption key.</param>
    extension(Aes aes)
    {
        /// <summary>
        /// Wraps the specified key, per RFC 3394 §2.2.1.
        /// </summary>
        /// <param name="plaintext">The key to wrap, which must be a whole number of 8-byte semiblocks, and at least 16 bytes.</param>
        /// <returns>Returns a new <see cref="byte"/> array containing the wrapped key.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="plaintext"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="plaintext"/> is not a valid length.</exception>
        public byte[] EncryptKeyWrap(byte[] plaintext) => aes.EncryptKeyWrap(new ReadOnlySpan<byte>(RequireNotNull(plaintext)));

        /// <summary>
        /// Wraps the specified key, per RFC 3394 §2.2.1.
        /// </summary>
        /// <param name="plaintext">The key to wrap, which must be a whole number of 8-byte semiblocks, and at least 16 bytes.</param>
        /// <returns>Returns a new <see cref="byte"/> array containing the wrapped key.</returns>
        /// <exception cref="ArgumentException">Thrown when <paramref name="plaintext"/> is not a valid length.</exception>
        public byte[] EncryptKeyWrap(ReadOnlySpan<byte> plaintext)
        {
            RequirePlaintextLength(plaintext.Length);

            byte[] ciphertext = new byte[plaintext.Length + SemiblockLength];
            aes.EncryptKeyWrap(plaintext, ciphertext);
            return ciphertext;
        }

        /// <summary>
        /// Wraps the specified key into the specified destination, per RFC 3394 §2.2.1.
        /// </summary>
        /// <param name="plaintext">The key to wrap, which must be a whole number of 8-byte semiblocks, and at least 16 bytes.</param>
        /// <param name="destination">The buffer that receives the wrapped key, which must be exactly 8 bytes longer than
        /// <paramref name="plaintext"/>.</param>
        /// <exception cref="ArgumentException">
        /// Thrown when <paramref name="plaintext"/> is not a valid length, or <paramref name="destination"/> is not exactly the wrapped
        /// length.
        /// </exception>
        public void EncryptKeyWrap(ReadOnlySpan<byte> plaintext, Span<byte> destination)
        {
            RequirePlaintextLength(plaintext.Length);

            int length = plaintext.Length + SemiblockLength;
            Require(destination.Length == length, $"Destination must be exactly {length} bytes.", nameof(destination));

            using Aes cipher = CreateBlockCipher(aes);
            using ICryptoTransform encryptor = cipher.CreateEncryptor();

            int n = plaintext.Length / SemiblockLength;
            byte[] block = new byte[2 * SemiblockLength];
            ulong a = DefaultInitialValue;
            plaintext.CopyTo(destination[SemiblockLength..]);

            try
            {
                for (int j = 0; j < Rounds; j++)
                for (int i = 1; i <= n; i++)
                {
                    Span<byte> r = destination.Slice(i * SemiblockLength, SemiblockLength);

                    BinaryPrimitives.WriteUInt64BigEndian(block, a);
                    r.CopyTo(block.AsSpan(SemiblockLength));
                    encryptor.TransformBlock(block, 0, block.Length, block, 0);

                    a = BinaryPrimitives.ReadUInt64BigEndian(block) ^ (ulong)((n * j) + i);
                    block.AsSpan(SemiblockLength).CopyTo(r);
                }

                BinaryPrimitives.WriteUInt64BigEndian(destination, a);
            }
            finally
            {
                CryptographicOperations.ZeroMemory(block);
            }
        }

        /// <summary>
        /// Unwraps the specified wrapped key, per RFC 3394 §2.2.2.
        /// </summary>
        /// <param name="ciphertext">The wrapped key.</param>
        /// <returns>Returns a new <see cref="byte"/> array containing the unwrapped key.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="ciphertext"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="ciphertext"/> is not a valid length.</exception>
        /// <exception cref="CryptographicException">
        /// Thrown when <paramref name="ciphertext"/> fails the integrity check, because it was not wrapped with this key or has been
        /// modified.
        /// </exception>
        public byte[] DecryptKeyWrap(byte[] ciphertext) => aes.DecryptKeyWrap(new ReadOnlySpan<byte>(RequireNotNull(ciphertext)));

        /// <summary>
        /// Unwraps the specified wrapped key, per RFC 3394 §2.2.2.
        /// </summary>
        /// <param name="ciphertext">The wrapped key.</param>
        /// <returns>Returns a new <see cref="byte"/> array containing the unwrapped key.</returns>
        /// <exception cref="ArgumentException">Thrown when <paramref name="ciphertext"/> is not a valid length.</exception>
        /// <exception cref="CryptographicException">
        /// Thrown when <paramref name="ciphertext"/> fails the integrity check, because it was not wrapped with this key or has been
        /// modified.
        /// </exception>
        public byte[] DecryptKeyWrap(ReadOnlySpan<byte> ciphertext)
        {
            RequireCiphertextLength(ciphertext.Length);

            byte[] plaintext = new byte[ciphertext.Length - SemiblockLength];
            aes.DecryptKeyWrap(ciphertext, plaintext);
            return plaintext;
        }

        /// <summary>
        /// Unwraps the specified wrapped key into the specified destination, per RFC 3394 §2.2.2.
        /// </summary>
        /// <param name="ciphertext">The wrapped key.</param>
        /// <param name="destination">The buffer that receives the unwrapped key, which must be at least 8 bytes shorter than
        /// <paramref name="ciphertext"/>.</param>
        /// <returns>Returns the number of bytes written to <paramref name="destination"/>.</returns>
        /// <exception cref="ArgumentException">
        /// Thrown when <paramref name="ciphertext"/> is not a valid length, or <paramref name="destination"/> is too short.
        /// </exception>
        /// <exception cref="CryptographicException">
        /// Thrown when <paramref name="ciphertext"/> fails the integrity check, in which case <paramref name="destination"/> is zeroed.
        /// </exception>
        public int DecryptKeyWrap(ReadOnlySpan<byte> ciphertext, Span<byte> destination)
        {
            RequireCiphertextLength(ciphertext.Length);
            Require(destination.Length >= ciphertext.Length - SemiblockLength, "Destination is too short.", nameof(destination));

            return Unwrap(aes, ciphertext, destination);
        }

        /// <summary>
        /// Attempts to unwrap the specified wrapped key into the specified destination, per RFC 3394 §2.2.2.
        /// </summary>
        /// <param name="ciphertext">The wrapped key.</param>
        /// <param name="destination">The buffer that receives the unwrapped key.</param>
        /// <param name="bytesWritten">When this method returns, contains the number of bytes written to
        /// <paramref name="destination"/>.</param>
        /// <returns>
        /// Returns <see langword="true"/> if the key was unwrapped; or <see langword="false"/> if <paramref name="destination"/> is too
        /// short.
        /// </returns>
        /// <exception cref="ArgumentException">Thrown when <paramref name="ciphertext"/> is not a valid length.</exception>
        /// <exception cref="CryptographicException">
        /// Thrown when <paramref name="ciphertext"/> fails the integrity check, in which case <paramref name="destination"/> is zeroed.
        /// </exception>
        /// <remarks>
        /// Why: as in .NET 11, only a short destination returns <see langword="false"/>; an integrity failure still throws.
        /// </remarks>
        public bool TryDecryptKeyWrap(ReadOnlySpan<byte> ciphertext, Span<byte> destination, out int bytesWritten)
        {
            RequireCiphertextLength(ciphertext.Length);

            if (destination.Length < ciphertext.Length - SemiblockLength)
            {
                bytesWritten = 0;
                return false;
            }

            bytesWritten = Unwrap(aes, ciphertext, destination);
            return true;
        }
    }

    /// <summary>
    /// Unwraps the specified wrapped key into the specified destination, whose lengths have been checked.
    /// </summary>
    /// <param name="aes">The <see cref="Aes"/> instance whose key is the key-encryption key.</param>
    /// <param name="ciphertext">The wrapped key, which is a valid length.</param>
    /// <param name="destination">The buffer that receives the unwrapped key, which is long enough.</param>
    /// <returns>Returns the number of bytes written to <paramref name="destination"/>.</returns>
    /// <exception cref="CryptographicException">Thrown when the wrapped key fails the integrity check.</exception>
    /// <remarks>
    /// Why: the recovered initial value is compared with
    /// <see cref="CryptographicOperations.FixedTimeEquals(ReadOnlySpan{byte}, ReadOnlySpan{byte})"/>, and the destination is zeroed on
    /// failure, so that a failed unwrap reveals neither how close the value was nor any of the unauthenticated key.
    /// </remarks>
    private static int Unwrap(Aes aes, ReadOnlySpan<byte> ciphertext, Span<byte> destination)
    {
        int keyLength = ciphertext.Length - SemiblockLength;

        using Aes cipher = CreateBlockCipher(aes);
        using ICryptoTransform decryptor = cipher.CreateDecryptor();

        int n = keyLength / SemiblockLength;
        Span<byte> output = destination[..keyLength];
        byte[] block = new byte[2 * SemiblockLength];
        Span<byte> expected = stackalloc byte[SemiblockLength];
        Span<byte> actual = stackalloc byte[SemiblockLength];
        ulong a = BinaryPrimitives.ReadUInt64BigEndian(ciphertext);
        ciphertext[SemiblockLength..].CopyTo(output);

        try
        {
            for (int j = Rounds - 1; j >= 0; j--)
            for (int i = n; i >= 1; i--)
            {
                Span<byte> r = output.Slice((i - 1) * SemiblockLength, SemiblockLength);

                BinaryPrimitives.WriteUInt64BigEndian(block, a ^ (ulong)((n * j) + i));
                r.CopyTo(block.AsSpan(SemiblockLength));
                decryptor.TransformBlock(block, 0, block.Length, block, 0);

                a = BinaryPrimitives.ReadUInt64BigEndian(block);
                block.AsSpan(SemiblockLength).CopyTo(r);
            }

            BinaryPrimitives.WriteUInt64BigEndian(expected, DefaultInitialValue);
            BinaryPrimitives.WriteUInt64BigEndian(actual, a);

            if (CryptographicOperations.FixedTimeEquals(expected, actual)) return keyLength;

            CryptographicOperations.ZeroMemory(output);
            throw new CryptographicException("The wrapped key failed the AES Key Wrap integrity check.");
        }
        finally
        {
            CryptographicOperations.ZeroMemory(block);
        }
    }

    /// <summary>
    /// Determines whether the specified plaintext length can be wrapped.
    /// </summary>
    /// <param name="length">The plaintext length, in bytes.</param>
    /// <returns>Returns <see langword="true"/> if the length is at least 16 bytes and a multiple of 8 bytes; otherwise,
    /// <see langword="false"/>.</returns>
    private static bool IsValidPlaintextLength(int length) => length >= MinimumPlaintextLength && length % SemiblockLength == 0;

    /// <summary>
    /// Checks that the specified plaintext length can be wrapped.
    /// </summary>
    /// <param name="length">The plaintext length, in bytes.</param>
    /// <exception cref="ArgumentException">Thrown when the length is shorter than 16 bytes, or is not a multiple of 8 bytes.</exception>
    private static void RequirePlaintextLength(int length) =>
        Require(IsValidPlaintextLength(length), "The plaintext is not a valid length for the Key Wrap algorithm.", "plaintext");

    /// <summary>
    /// Checks that the specified ciphertext length is a valid wrapped key length.
    /// </summary>
    /// <param name="length">The ciphertext length, in bytes.</param>
    /// <exception cref="ArgumentException">Thrown when the length is shorter than 24 bytes, or is not a multiple of 8 bytes.</exception>
    private static void RequireCiphertextLength(int length) =>
        Require(
            IsValidPlaintextLength(length - SemiblockLength),
            "The ciphertext is not a valid length for the Key Wrap algorithm.",
            "ciphertext");

    /// <summary>
    /// Creates an AES-ECB cipher, without padding, keyed with the key of the specified <see cref="Aes"/> instance.
    /// </summary>
    /// <param name="aes">The <see cref="Aes"/> instance whose key to use.</param>
    /// <returns>Returns a new <see cref="Aes"/> instance, which the caller must dispose.</returns>
    /// <remarks>
    /// Why: one ECB transform is created per wrap or unwrap and reused for every block, rather than calling
    /// <see cref="SymmetricAlgorithm.EncryptEcb(ReadOnlySpan{byte}, PaddingMode)"/> for each block, which sets up the platform cipher every
    /// time; wrapping a 64-byte key takes 48 blocks. A separate instance is used so that the caller's mode and padding are left untouched.
    /// The copy of the key that <see cref="SymmetricAlgorithm.Key"/> returns is zeroed.
    /// </remarks>
    private static Aes CreateBlockCipher(Aes aes)
    {
        Aes cipher = Aes.Create();
        byte[] key = aes.Key;

        try
        {
            cipher.Key = key;
            cipher.Mode = CipherMode.ECB;
            cipher.Padding = PaddingMode.None;
            return cipher;
        }
        catch
        {
            cipher.Dispose();
            throw;
        }
        finally
        {
            CryptographicOperations.ZeroMemory(key);
        }
    }
}
#endif
