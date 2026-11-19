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

using System;
using System.Buffers.Binary;
using System.Security.Cryptography;

namespace OnixLabs.Security.Cryptography;

/// <summary>
/// Represents the AES Key Wrap algorithm of RFC 3394, used by JOSE as <c>A128KW</c>, <c>A192KW</c> and <c>A256KW</c> (RFC 7518 §4.4).
/// </summary>
/// <remarks>
/// Why: this is not <c>Aes.EncryptKeyWrapPadded</c>, which implements AES Key Wrap with Padding (RFC 5649). That
/// algorithm uses a different initial value and so produces different output for the same input; JOSE requires RFC 3394.
/// </remarks>
public static class AesKeyWrap
{
    /// <summary>
    /// The length in bytes of one key wrap block, a 64-bit half of an AES block.
    /// </summary>
    public const int SemiblockLength = 8;

    /// <summary>
    /// The smallest key that can be wrapped, in bytes: two semiblocks, as RFC 3394 §2 requires.
    /// </summary>
    public const int MinimumKeyLength = 2 * SemiblockLength;

    /// <summary>
    /// The default initial value of RFC 3394 §2.2.3.1, A6A6A6A6A6A6A6A6.
    /// </summary>
    private const ulong DefaultInitialValue = 0xA6A6A6A6A6A6A6A6;

    /// <summary>
    /// The number of wrapping rounds, j = 0 to 5, of RFC 3394 §2.2.1.
    /// </summary>
    private const int Rounds = 6;

    /// <summary>
    /// Wraps the specified key with the specified key-encryption key, per RFC 3394 §2.2.1.
    /// </summary>
    /// <param name="keyEncryptionKey">The 16, 24 or 32-byte AES key-encryption key.</param>
    /// <param name="key">The key to wrap, which must be a whole number of 8-byte semiblocks, and at least 16 bytes.</param>
    /// <returns>Returns a new <see cref="byte"/> array containing the wrapped key, which is 8 bytes longer than
    /// <paramref name="key"/>.</returns>
    /// <exception cref="ArgumentException">
    /// Thrown when <paramref name="keyEncryptionKey"/> is not 16, 24 or 32 bytes, or <paramref name="key"/> is shorter than 16 bytes or not
    /// a
    /// multiple of 8 bytes.
    /// </exception>
    public static byte[] Wrap(ReadOnlySpan<byte> keyEncryptionKey, ReadOnlySpan<byte> key)
    {
        RequireKeyEncryptionKey(keyEncryptionKey);

        Require
        (
            condition: key.Length >= MinimumKeyLength && key.Length % SemiblockLength == 0,
            message: $"The key to wrap must be at least {MinimumKeyLength} bytes and a multiple of {SemiblockLength} bytes.",
            parameterName: nameof(key)
        );

        using Aes aes = CreateCipher(keyEncryptionKey);
        using ICryptoTransform encryptor = aes.CreateEncryptor();

        int n = key.Length / SemiblockLength;
        byte[] output = new byte[key.Length + SemiblockLength];
        byte[] block = new byte[2 * SemiblockLength];
        ulong a = DefaultInitialValue;
        key.CopyTo(output.AsSpan(SemiblockLength));

        try
        {
            for (int j = 0; j < Rounds; j++)
            for (int i = 1; i <= n; i++)
            {
                Span<byte> r = output.AsSpan(i * SemiblockLength, SemiblockLength);

                BinaryPrimitives.WriteUInt64BigEndian(block, a);
                r.CopyTo(block.AsSpan(SemiblockLength));
                encryptor.TransformBlock(block, 0, block.Length, block, 0);

                a = BinaryPrimitives.ReadUInt64BigEndian(block) ^ (ulong)((n * j) + i);
                block.AsSpan(SemiblockLength).CopyTo(r);
            }

            BinaryPrimitives.WriteUInt64BigEndian(output, a);
            return output;
        }
        finally
        {
            CryptographicOperations.ZeroMemory(block);
        }
    }

    /// <summary>
    /// Unwraps the specified wrapped key with the specified key-encryption key, per RFC 3394 §2.2.2.
    /// </summary>
    /// <param name="keyEncryptionKey">The 16, 24 or 32-byte AES key-encryption key.</param>
    /// <param name="wrappedKey">The wrapped key.</param>
    /// <returns>Returns a new <see cref="byte"/> array containing the unwrapped key, which is 8 bytes shorter than
    /// <paramref name="wrappedKey"/>.</returns>
    /// <exception cref="ArgumentException">Thrown when <paramref name="keyEncryptionKey"/> is not 16, 24 or 32 bytes.</exception>
    /// <exception cref="CryptographicException">
    /// Thrown when <paramref name="wrappedKey"/> is not a valid wrapped key length, or fails the integrity check because it was not
    /// wrapped with <paramref name="keyEncryptionKey"/> or has been modified.
    /// </exception>
    public static byte[] Unwrap(ReadOnlySpan<byte> keyEncryptionKey, ReadOnlySpan<byte> wrappedKey)
    {
        RequireKeyEncryptionKey(keyEncryptionKey);

        byte[] output = new byte[Math.Max(0, wrappedKey.Length - SemiblockLength)];

        return TryUnwrap(keyEncryptionKey, wrappedKey, output, out int _)
            ? output
            : throw new CryptographicException("The wrapped key could not be unwrapped: it is malformed, or failed the integrity check.");
    }

    /// <summary>
    /// Attempts to unwrap the specified wrapped key with the specified key-encryption key, per RFC 3394 §2.2.2.
    /// </summary>
    /// <param name="keyEncryptionKey">The 16, 24 or 32-byte AES key-encryption key.</param>
    /// <param name="wrappedKey">The wrapped key.</param>
    /// <param name="destination">The buffer that receives the unwrapped key, which must be at least 8 bytes shorter than
    /// <paramref name="wrappedKey"/>.</param>
    /// <param name="bytesWritten">When this method returns, contains the number of bytes written to <paramref name="destination"/>, or zero
    /// on failure.</param>
    /// <returns>
    /// Returns <see langword="true"/> if the wrapped key passed the integrity check and was unwrapped; otherwise, <see langword="false"/>,
    /// in which
    /// case nothing remains in <paramref name="destination"/>. A wrapped key whose length is not a whole number of semiblocks, or is
    /// shorter than
    /// 24 bytes, also returns <see langword="false"/>.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// Thrown when <paramref name="keyEncryptionKey"/> is not 16, 24 or 32 bytes, or <paramref name="destination"/> is too short.
    /// </exception>
    /// <remarks>
    /// Why: the recovered initial value is compared with
    /// <see cref="CryptographicOperations.FixedTimeEquals(ReadOnlySpan{byte}, ReadOnlySpan{byte})"/>, and <paramref name="destination"/>
    /// is zeroed on failure, so that a failed unwrap reveals neither how close the value was nor any of the unauthenticated key.
    /// </remarks>
    public static bool TryUnwrap(
        ReadOnlySpan<byte> keyEncryptionKey,
        ReadOnlySpan<byte> wrappedKey,
        Span<byte> destination,
        out int bytesWritten)
    {
        RequireKeyEncryptionKey(keyEncryptionKey);
        bytesWritten = 0;

        if (wrappedKey.Length < MinimumKeyLength + SemiblockLength || wrappedKey.Length % SemiblockLength != 0) return false;

        int keyLength = wrappedKey.Length - SemiblockLength;
        Require(destination.Length >= keyLength, $"The destination must be at least {keyLength} bytes long.", nameof(destination));

        using Aes aes = CreateCipher(keyEncryptionKey);
        using ICryptoTransform decryptor = aes.CreateDecryptor();

        int n = keyLength / SemiblockLength;
        Span<byte> output = destination[..keyLength];
        byte[] block = new byte[2 * SemiblockLength];
        Span<byte> expected = stackalloc byte[SemiblockLength];
        Span<byte> actual = stackalloc byte[SemiblockLength];
        ulong a = BinaryPrimitives.ReadUInt64BigEndian(wrappedKey);
        wrappedKey[SemiblockLength..].CopyTo(output);

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

            if (!CryptographicOperations.FixedTimeEquals(expected, actual))
            {
                CryptographicOperations.ZeroMemory(output);
                return false;
            }

            bytesWritten = keyLength;
            return true;
        }
        finally
        {
            CryptographicOperations.ZeroMemory(block);
        }
    }

    /// <summary>
    /// Checks that the specified key-encryption key is a valid AES key length.
    /// </summary>
    /// <param name="keyEncryptionKey">The key-encryption key to check.</param>
    /// <exception cref="ArgumentException">Thrown when <paramref name="keyEncryptionKey"/> is not 16, 24 or 32 bytes.</exception>
    private static void RequireKeyEncryptionKey(ReadOnlySpan<byte> keyEncryptionKey) =>
        Require(keyEncryptionKey.Length is 16 or 24 or 32, "The key-encryption key must be 16, 24 or 32 bytes.", nameof(keyEncryptionKey));

    /// <summary>
    /// Creates an AES-ECB cipher, without padding, keyed with the specified key-encryption key.
    /// </summary>
    /// <param name="keyEncryptionKey">The key-encryption key.</param>
    /// <returns>Returns a new <see cref="Aes"/> instance, which the caller must dispose.</returns>
    /// <remarks>
    /// Why: one ECB transform is created per wrap or unwrap and reused for every block. Calling
    /// <see cref="SymmetricAlgorithm.EncryptEcb(ReadOnlySpan{byte}, PaddingMode)"/>
    /// for each block sets up the platform cipher every time, and wrapping a 64-byte key takes 48 blocks. The key is set through
    /// <see cref="SymmetricAlgorithm.Key"/> because <c>SymmetricAlgorithm.SetKey</c>, which takes a span, only exists from .NET 10; the
    /// temporary copy it requires is zeroed.
    /// </remarks>
    private static Aes CreateCipher(ReadOnlySpan<byte> keyEncryptionKey)
    {
        Aes aes = Aes.Create();
        byte[] copy = keyEncryptionKey.ToArray();

        try
        {
            aes.Key = copy;
            aes.Mode = CipherMode.ECB;
            aes.Padding = PaddingMode.None;
            return aes;
        }
        catch
        {
            aes.Dispose();
            throw;
        }
        finally
        {
            CryptographicOperations.ZeroMemory(copy);
        }
    }
}
