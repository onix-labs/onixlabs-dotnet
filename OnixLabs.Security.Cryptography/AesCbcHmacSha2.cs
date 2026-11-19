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
/// Represents the AES_CBC_HMAC_SHA2 authenticated encryption algorithms of RFC 7518 §5.2: <c>A128CBC-HS256</c>, <c>A192CBC-HS384</c> and
/// <c>A256CBC-HS512</c>, chosen by the length of the composite key.
/// </summary>
/// <remarks>
/// <para>
/// The first half of the composite key is the HMAC key and the second half is the AES key. Encryption pads the plaintext with PKCS #7,
/// encrypts it with AES-CBC, and computes the tag as the first half of HMAC(MAC_KEY, A ‖ IV ‖ E ‖ AL), where AL is the length of the
/// associated data in bits as a 64-bit big-endian integer.
/// </para>
/// <para>
/// Like <see cref="AesGcm"/>, an instance is not thread-safe, and the caller supplies the initialization vector, which must be 16 random
/// bytes generated afresh for every encryption.
/// </para>
/// </remarks>
public sealed class AesCbcHmacSha2 : IDisposable
{
    /// <summary>
    /// The length in bytes of the initialization vector, one AES block.
    /// </summary>
    public const int IvLength = 16;

    /// <summary>
    /// The length in bytes of an AES block, to which ciphertext is padded.
    /// </summary>
    private const int BlockLength = 16;

    /// <summary>
    /// The AES-CBC cipher keyed with ENC_KEY, the second half of the composite key.
    /// </summary>
    private readonly Aes aes;

    /// <summary>
    /// MAC_KEY, the first half of the composite key.
    /// </summary>
    private readonly byte[] macKey;

    /// <summary>
    /// The HMAC hash algorithm: SHA-256, SHA-384 or SHA-512, for 32, 48 or 64-byte composite keys respectively.
    /// </summary>
    private readonly HashAlgorithmName hashAlgorithm;

    /// <summary>
    /// A value indicating whether the current instance has been disposed.
    /// </summary>
    private bool isDisposed;

    /// <summary>
    /// Initializes a new instance of the <see cref="AesCbcHmacSha2"/> class.
    /// </summary>
    /// <param name="key">
    /// The composite key: 32 bytes for <c>A128CBC-HS256</c>, 48 bytes for <c>A192CBC-HS384</c>, or 64 bytes for <c>A256CBC-HS512</c>.
    /// </param>
    /// <exception cref="ArgumentException">Thrown when <paramref name="key"/> is not 32, 48 or 64 bytes.</exception>
    public AesCbcHmacSha2(ReadOnlySpan<byte> key)
    {
        Require(key.Length is 32 or 48 or 64, "The composite key must be 32, 48 or 64 bytes.", nameof(key));

        int half = key.Length / 2;

        hashAlgorithm = key.Length switch
        {
            32 => HashAlgorithmName.SHA256,
            48 => HashAlgorithmName.SHA384,
            _ => HashAlgorithmName.SHA512
        };

        TagSizeInBytes = half;
        macKey = key[..half].ToArray();
        aes = Aes.Create();
        SetKey(aes, key[half..]);
    }

    /// <summary>
    /// Gets the length in bytes of the authentication tag, which is half the length of the composite key.
    /// </summary>
    /// <value>The length in bytes of the authentication tag: 16, 24 or 32.</value>
    public int TagSizeInBytes { get; }

    /// <summary>
    /// Gets the length of the ciphertext produced for a plaintext of the specified length, which PKCS #7 padding extends to the next whole
    /// AES block, adding a whole block when the plaintext is already a multiple of 16 bytes.
    /// </summary>
    /// <param name="plaintextLength">The length of the plaintext, in bytes.</param>
    /// <returns>Returns the length of the ciphertext, in bytes.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="plaintextLength"/> is negative.</exception>
    public static int GetCiphertextLength(int plaintextLength)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(plaintextLength);
        return checked(((plaintextLength / BlockLength) + 1) * BlockLength);
    }

    /// <summary>
    /// Encrypts the specified plaintext and authenticates it, together with the specified associated data.
    /// </summary>
    /// <param name="iv">The 16-byte initialization vector, which must be random and never reused with the same key.</param>
    /// <param name="plaintext">The plaintext to encrypt.</param>
    /// <param name="tag">The buffer that receives the authentication tag, which must be <see cref="TagSizeInBytes"/> long.</param>
    /// <param name="associatedData">The associated data, which is authenticated but not encrypted; for JWE, the ASCII of the encoded
    /// protected header.</param>
    /// <returns>Returns a new <see cref="byte"/> array containing the ciphertext, of length <see cref="GetCiphertextLength"/>.</returns>
    /// <exception cref="ArgumentException">Thrown when <paramref name="iv"/> is not 16 bytes, or <paramref name="tag"/> is not
    /// <see cref="TagSizeInBytes"/> long.</exception>
    /// <exception cref="ObjectDisposedException">Thrown when the current instance has been disposed.</exception>
    public byte[] Encrypt(ReadOnlySpan<byte> iv, ReadOnlySpan<byte> plaintext, Span<byte> tag, ReadOnlySpan<byte> associatedData = default)
    {
        byte[] ciphertext = new byte[GetCiphertextLength(plaintext.Length)];
        Encrypt(iv, plaintext, ciphertext, tag, associatedData);
        return ciphertext;
    }

    /// <summary>
    /// Encrypts the specified plaintext into the specified destination and authenticates it, together with the specified associated data.
    /// </summary>
    /// <param name="iv">The 16-byte initialization vector, which must be random and never reused with the same key.</param>
    /// <param name="plaintext">The plaintext to encrypt.</param>
    /// <param name="ciphertext">The buffer that receives the ciphertext, which must be exactly <see cref="GetCiphertextLength"/>
    /// long.</param>
    /// <param name="tag">The buffer that receives the authentication tag, which must be <see cref="TagSizeInBytes"/> long.</param>
    /// <param name="associatedData">The associated data, which is authenticated but not encrypted; for JWE, the ASCII of the encoded
    /// protected header.</param>
    /// <exception cref="ArgumentException">
    /// Thrown when <paramref name="iv"/> is not 16 bytes, <paramref name="ciphertext"/> is not <see cref="GetCiphertextLength"/> long, or
    /// <paramref name="tag"/> is not <see cref="TagSizeInBytes"/> long.
    /// </exception>
    /// <exception cref="ObjectDisposedException">Thrown when the current instance has been disposed.</exception>
    public void Encrypt(
        ReadOnlySpan<byte> iv,
        ReadOnlySpan<byte> plaintext,
        Span<byte> ciphertext,
        Span<byte> tag,
        ReadOnlySpan<byte> associatedData = default)
    {
        ObjectDisposedException.ThrowIf(isDisposed, this);
        RequireIv(iv);
        RequireTag(tag.Length);
        Require(ciphertext.Length == GetCiphertextLength(plaintext.Length),
            "The ciphertext buffer must be exactly the padded length of the plaintext.", nameof(ciphertext));

        aes.EncryptCbc(plaintext, iv, ciphertext, PaddingMode.PKCS7);
        ComputeTag(iv, ciphertext, associatedData, tag);
    }

    /// <summary>
    /// Verifies the authentication tag, and only then decrypts the specified ciphertext.
    /// </summary>
    /// <param name="iv">The 16-byte initialization vector used to encrypt.</param>
    /// <param name="ciphertext">The ciphertext to decrypt.</param>
    /// <param name="tag">The authentication tag, which must be <see cref="TagSizeInBytes"/> long.</param>
    /// <param name="associatedData">The associated data that was authenticated with the ciphertext.</param>
    /// <returns>Returns a new <see cref="byte"/> array containing the plaintext.</returns>
    /// <exception cref="ArgumentException">Thrown when <paramref name="iv"/> is not 16 bytes, or <paramref name="tag"/> is not
    /// <see cref="TagSizeInBytes"/> long.</exception>
    /// <exception cref="AuthenticationTagMismatchException">
    /// Thrown when the tag does not match the IV, ciphertext and associated data, or the ciphertext is not a whole, non-zero number of
    /// blocks.
    /// </exception>
    /// <exception cref="ObjectDisposedException">Thrown when the current instance has been disposed.</exception>
    public byte[] Decrypt(
        ReadOnlySpan<byte> iv,
        ReadOnlySpan<byte> ciphertext,
        ReadOnlySpan<byte> tag,
        ReadOnlySpan<byte> associatedData = default)
    {
        byte[] buffer = new byte[ciphertext.Length];

        try
        {
            int length = Decrypt(iv, ciphertext, tag, buffer, associatedData);
            return buffer.AsSpan(0, length).ToArray();
        }
        finally
        {
            CryptographicOperations.ZeroMemory(buffer);
        }
    }

    /// <summary>
    /// Verifies the authentication tag, and only then decrypts the specified ciphertext into the specified destination.
    /// </summary>
    /// <param name="iv">The 16-byte initialization vector used to encrypt.</param>
    /// <param name="ciphertext">The ciphertext to decrypt.</param>
    /// <param name="tag">The authentication tag, which must be <see cref="TagSizeInBytes"/> long.</param>
    /// <param name="plaintext">The buffer that receives the plaintext, which must be at least as long as
    /// <paramref name="ciphertext"/>.</param>
    /// <param name="associatedData">The associated data that was authenticated with the ciphertext.</param>
    /// <returns>Returns the number of plaintext bytes written to <paramref name="plaintext"/>.</returns>
    /// <exception cref="ArgumentException">
    /// Thrown when <paramref name="iv"/> is not 16 bytes, <paramref name="tag"/> is not <see cref="TagSizeInBytes"/> long, or
    /// <paramref name="plaintext"/> is shorter than <paramref name="ciphertext"/>.
    /// </exception>
    /// <exception cref="AuthenticationTagMismatchException">
    /// Thrown when the tag does not match the IV, ciphertext and associated data, or the ciphertext is not a whole, non-zero number of
    /// blocks.
    /// Nothing is written to <paramref name="plaintext"/>.
    /// </exception>
    /// <exception cref="CryptographicException">Thrown when the tag matches but the PKCS #7 padding is invalid, which only the key holder
    /// can cause.</exception>
    /// <exception cref="ObjectDisposedException">Thrown when the current instance has been disposed.</exception>
    /// <remarks>
    /// Why: the tag is compared in constant time before any decryption, as RFC 7518 §5.2.2.2 requires, so that unauthenticated ciphertext
    /// never
    /// reaches the padding check and cannot be used as a padding oracle.
    /// </remarks>
    public int Decrypt(
        ReadOnlySpan<byte> iv,
        ReadOnlySpan<byte> ciphertext,
        ReadOnlySpan<byte> tag,
        Span<byte> plaintext,
        ReadOnlySpan<byte> associatedData = default)
    {
        ObjectDisposedException.ThrowIf(isDisposed, this);
        RequireIv(iv);
        RequireTag(tag.Length);
        Require(
            plaintext.Length >= ciphertext.Length,
            "The plaintext buffer must be at least as long as the ciphertext.",
            nameof(plaintext));

        Span<byte> expected = stackalloc byte[TagSizeInBytes];
        ComputeTag(iv, ciphertext, associatedData, expected);

        bool isAuthentic = CryptographicOperations.FixedTimeEquals(expected, tag);
        bool isWholeBlocks = ciphertext.Length > 0 && ciphertext.Length % BlockLength == 0;

        if (!isAuthentic || !isWholeBlocks) throw new AuthenticationTagMismatchException();

        return aes.DecryptCbc(ciphertext, iv, plaintext, PaddingMode.PKCS7);
    }

    /// <summary>
    /// Releases the cipher and zeroes the HMAC key.
    /// </summary>
    public void Dispose()
    {
        if (isDisposed) return;

        CryptographicOperations.ZeroMemory(macKey);
        aes.Dispose();
        isDisposed = true;
    }

    /// <summary>
    /// Computes the authentication tag, the first <see cref="TagSizeInBytes"/> bytes of HMAC(MAC_KEY, A ‖ IV ‖ E ‖ AL).
    /// </summary>
    /// <param name="iv">The initialization vector.</param>
    /// <param name="ciphertext">The ciphertext E.</param>
    /// <param name="associatedData">The associated data A.</param>
    /// <param name="tag">The buffer that receives the tag, which is <see cref="TagSizeInBytes"/> long.</param>
    private void ComputeTag(ReadOnlySpan<byte> iv, ReadOnlySpan<byte> ciphertext, ReadOnlySpan<byte> associatedData, Span<byte> tag)
    {
        using IncrementalHash hmac = IncrementalHash.CreateHMAC(hashAlgorithm, macKey);
        Span<byte> associatedDataLength = stackalloc byte[sizeof(ulong)];
        Span<byte> mac = stackalloc byte[hmac.HashLengthInBytes];
        BinaryPrimitives.WriteUInt64BigEndian(associatedDataLength, (ulong)associatedData.Length * 8);

        hmac.AppendData(associatedData);
        hmac.AppendData(iv);
        hmac.AppendData(ciphertext);
        hmac.AppendData(associatedDataLength);
        hmac.GetHashAndReset(mac);

        mac[..TagSizeInBytes].CopyTo(tag);
    }

    /// <summary>
    /// Checks that the specified initialization vector is 16 bytes.
    /// </summary>
    /// <param name="iv">The initialization vector to check.</param>
    /// <exception cref="ArgumentException">Thrown when <paramref name="iv"/> is not 16 bytes.</exception>
    private static void RequireIv(ReadOnlySpan<byte> iv) =>
        Require(iv.Length == IvLength, $"The initialization vector must be {IvLength} bytes.", nameof(iv));

    /// <summary>
    /// Checks that the specified tag length is <see cref="TagSizeInBytes"/>.
    /// </summary>
    /// <param name="length">The tag length to check.</param>
    /// <exception cref="ArgumentException">Thrown when <paramref name="length"/> is not <see cref="TagSizeInBytes"/>.</exception>
    private void RequireTag(int length) =>
        Require(length == TagSizeInBytes, $"The authentication tag must be {TagSizeInBytes} bytes.", "tag");

    /// <summary>
    /// Sets the key of the specified cipher, zeroing the temporary copy that the <see cref="SymmetricAlgorithm.Key"/> property requires.
    /// </summary>
    /// <param name="aes">The cipher whose key to set.</param>
    /// <param name="key">The key to set.</param>
    /// <remarks>Why: <c>SymmetricAlgorithm.SetKey</c>, which takes a span, only exists from .NET 10.</remarks>
    private static void SetKey(Aes aes, ReadOnlySpan<byte> key)
    {
        byte[] copy = key.ToArray();

        try
        {
            aes.Key = copy;
        }
        finally
        {
            CryptographicOperations.ZeroMemory(copy);
        }
    }
}
