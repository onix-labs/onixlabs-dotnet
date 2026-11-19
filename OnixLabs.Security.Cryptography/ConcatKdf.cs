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
using System.Buffers;
using System.Buffers.Binary;
using System.Security.Cryptography;

namespace OnixLabs.Security.Cryptography;

/// <summary>
/// Represents the single-step key derivation function of NIST SP 800-56A Rev. 3 §5.8.2.1 with a hash function (the Concatenation KDF,
/// or Concat KDF), as JOSE uses it in RFC 7518 §4.6.2 for ECDH-ES and in draft-madden-jose-ecdh-1pu-04 for ECDH-1PU.
/// </summary>
/// <remarks>
/// <para>
/// The derived key is the leftmost bytes of H(1 ‖ Z ‖ OtherInfo) ‖ H(2 ‖ Z ‖ OtherInfo) ‖ …, where each counter is a 32-bit
/// big-endian integer. The caller builds <c>OtherInfo</c>; for JOSE it is
/// <c>AlgorithmID ‖ PartyUInfo ‖ PartyVInfo ‖ SuppPubInfo ‖ SuppPrivInfo</c>.
/// </para>
/// <para>
/// Why: the shape follows <see cref="HKDF"/>, .NET's other key derivation function: a static class whose <c>DeriveKey</c> methods take
/// the hash algorithm first, then the secret, then the output, then the context, and throw the same exceptions for the same faults.
/// </para>
/// </remarks>
internal static class ConcatKdf
{
    /// <summary>
    /// The largest round input, in bytes, built in a stack buffer rather than a pooled one.
    /// </summary>
    private const int MaxStackInputLength = 512;

    /// <summary>
    /// The largest supported hash length, in bytes, that of SHA-512.
    /// </summary>
    private const int MaxHashLength = 64;

    /// <summary>
    /// Derives key material of the specified length from the specified shared secret and other information.
    /// </summary>
    /// <param name="hashAlgorithmName">The hash algorithm: <see cref="HashAlgorithmName.SHA256"/>, as JOSE requires,
    /// <see cref="HashAlgorithmName.SHA384"/> or <see cref="HashAlgorithmName.SHA512"/>.</param>
    /// <param name="sharedSecret">The shared secret Z, for example the raw output of an ECDH or X25519 key agreement.</param>
    /// <param name="outputLength">The length of the key material to derive, in bytes.</param>
    /// <param name="otherInfo">The context-specific <c>OtherInfo</c> bytes, which are hashed after <paramref name="sharedSecret"/>.</param>
    /// <returns>Returns a new <see cref="byte"/> array containing the derived key material.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="sharedSecret"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown when <paramref name="outputLength"/> is not positive, or <paramref name="hashAlgorithmName"/> is not SHA-256, SHA-384 or
    /// SHA-512.
    /// </exception>
    public static byte[] DeriveKey(HashAlgorithmName hashAlgorithmName, byte[] sharedSecret, int outputLength, byte[]? otherInfo = null)
    {
        RequireNotNull(sharedSecret);
        RequireWithinRangeInclusive(outputLength, 1, int.MaxValue);

        byte[] output = new byte[outputLength];
        DeriveKey(hashAlgorithmName, sharedSecret, output, otherInfo);
        return output;
    }

    /// <summary>
    /// Derives key material from the specified shared secret and other information into the specified output buffer.
    /// </summary>
    /// <param name="hashAlgorithmName">The hash algorithm: <see cref="HashAlgorithmName.SHA256"/>, as JOSE requires,
    /// <see cref="HashAlgorithmName.SHA384"/> or <see cref="HashAlgorithmName.SHA512"/>.</param>
    /// <param name="sharedSecret">The shared secret Z, for example the raw output of an ECDH or X25519 key agreement.</param>
    /// <param name="output">The buffer that receives the derived key material; its length is the key length.</param>
    /// <param name="otherInfo">The context-specific <c>OtherInfo</c> bytes, which are hashed after <paramref name="sharedSecret"/>.</param>
    /// <exception cref="ArgumentException">Thrown when <paramref name="output"/> is empty.</exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown when <paramref name="hashAlgorithmName"/> is not SHA-256, SHA-384 or SHA-512.
    /// </exception>
    public static void DeriveKey(
        HashAlgorithmName hashAlgorithmName,
        ReadOnlySpan<byte> sharedSecret,
        Span<byte> output,
        ReadOnlySpan<byte> otherInfo)
    {
        RequireSupportedHash(hashAlgorithmName);
        Require(output.Length > 0, "The output must not be empty.", nameof(output));

        int inputLength = sizeof(uint) + sharedSecret.Length + otherInfo.Length;
        byte[]? rented = inputLength > MaxStackInputLength ? ArrayPool<byte>.Shared.Rent(inputLength) : null;
        Span<byte> input = (rented ?? stackalloc byte[MaxStackInputLength])[..inputLength];
        Span<byte> block = stackalloc byte[MaxHashLength];

        try
        {
            sharedSecret.CopyTo(input[sizeof(uint)..]);
            otherInfo.CopyTo(input[(sizeof(uint) + sharedSecret.Length)..]);

            for (uint round = 1, offset = 0; offset < output.Length; round++)
            {
                BinaryPrimitives.WriteUInt32BigEndian(input, round);
                int hashLength = HashData(hashAlgorithmName, input, block);
                int count = Math.Min(hashLength, output.Length - (int)offset);
                block[..count].CopyTo(output[(int)offset..]);
                offset += (uint)hashLength;
            }
        }
        finally
        {
            CryptographicOperations.ZeroMemory(input);
            CryptographicOperations.ZeroMemory(block);
            if (rented is not null) ArrayPool<byte>.Shared.Return(rented);
        }
    }

    /// <summary>
    /// Checks that the specified hash algorithm is one that Concat KDF supports.
    /// </summary>
    /// <param name="hashAlgorithmName">The hash algorithm to check.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the hash algorithm is not SHA-256, SHA-384 or SHA-512.</exception>
    /// <remarks>Why: <see cref="HKDF"/> reports an unsupported hash as <see cref="ArgumentOutOfRangeException"/> too.</remarks>
    private static void RequireSupportedHash(HashAlgorithmName hashAlgorithmName)
    {
        if (hashAlgorithmName == HashAlgorithmName.SHA256 ||
            hashAlgorithmName == HashAlgorithmName.SHA384 ||
            hashAlgorithmName == HashAlgorithmName.SHA512) return;

        throw new ArgumentOutOfRangeException(nameof(hashAlgorithmName), "The hash algorithm must be SHA-256, SHA-384 or SHA-512.");
    }

    /// <summary>
    /// Hashes the specified input with the specified SHA-2 hash algorithm.
    /// </summary>
    /// <param name="hashAlgorithmName">The hash algorithm, which is SHA-256, SHA-384 or SHA-512.</param>
    /// <param name="input">The input to hash.</param>
    /// <param name="destination">The buffer that receives the hash, which must be at least 64 bytes long.</param>
    /// <returns>Returns the length of the hash, in bytes.</returns>
    /// <remarks>
    /// Why: the one-shot SHA-2 methods avoid creating a hash object for every round, which on some platforms costs more than hashing the
    /// short JOSE inputs.
    /// </remarks>
    private static int HashData(HashAlgorithmName hashAlgorithmName, ReadOnlySpan<byte> input, Span<byte> destination)
    {
        if (hashAlgorithmName == HashAlgorithmName.SHA256) return SHA256.HashData(input, destination);
        if (hashAlgorithmName == HashAlgorithmName.SHA384) return SHA384.HashData(input, destination);
        return SHA512.HashData(input, destination);
    }
}
