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
/// Represents the single-step key derivation function of NIST SP 800-56A Rev. 3 §5.8.2.1 with a hash function (the Concatenation KDF, or
/// Concat KDF), as JOSE uses it in RFC 7518 §4.6.2 for ECDH-ES and in draft-madden-jose-ecdh-1pu-04 for ECDH-1PU.
/// </summary>
/// <remarks>
/// The derived key is the leftmost bytes of H(1 ‖ Z ‖ OtherInfo) ‖ H(2 ‖ Z ‖ OtherInfo) ‖ …, where each counter is a 32-bit big-endian
/// integer. The caller builds <c>OtherInfo</c>; for JOSE it is <c>AlgorithmID ‖ PartyUInfo ‖ PartyVInfo ‖ SuppPubInfo ‖ SuppPrivInfo</c>.
/// </remarks>
public static class ConcatKdf
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
    /// Derives key material from the specified shared secret and other information into the specified destination.
    /// </summary>
    /// <param name="hashAlgorithm">The hash algorithm to use, for example <see cref="HashAlgorithmName.SHA256"/> as JOSE requires.</param>
    /// <param name="sharedSecret">The shared secret Z, for example the raw output of an ECDH or X25519 key agreement.</param>
    /// <param name="otherInfo">The context-specific <c>OtherInfo</c> bytes, which are hashed after <paramref name="sharedSecret"/>.</param>
    /// <param name="destination">The buffer that receives the derived key; its length is the key length.</param>
    /// <exception cref="ArgumentException">
    /// Thrown when <paramref name="hashAlgorithm"/> has no name, or when <paramref name="destination"/> is empty.
    /// </exception>
    /// <exception cref="CryptographicException">Thrown when <paramref name="hashAlgorithm"/> is not supported on the current
    /// platform.</exception>
    public static void DeriveKey(
        HashAlgorithmName hashAlgorithm,
        ReadOnlySpan<byte> sharedSecret,
        ReadOnlySpan<byte> otherInfo,
        Span<byte> destination)
    {
        Require(!string.IsNullOrEmpty(hashAlgorithm.Name), "The hash algorithm must have a name.", nameof(hashAlgorithm));
        Require(destination.Length > 0, "The destination must not be empty.", nameof(destination));

        int inputLength = sizeof(uint) + sharedSecret.Length + otherInfo.Length;
        byte[]? rented = inputLength > MaxStackInputLength ? ArrayPool<byte>.Shared.Rent(inputLength) : null;
        Span<byte> input = (rented ?? stackalloc byte[MaxStackInputLength])[..inputLength];
        Span<byte> block = stackalloc byte[MaxHashLength];

        try
        {
            sharedSecret.CopyTo(input[sizeof(uint)..]);
            otherInfo.CopyTo(input[(sizeof(uint) + sharedSecret.Length)..]);

            for (uint round = 1, offset = 0; offset < destination.Length; round++)
            {
                BinaryPrimitives.WriteUInt32BigEndian(input, round);
                int hashLength = HashData(hashAlgorithm, input, block);
                int count = Math.Min(hashLength, destination.Length - (int)offset);
                block[..count].CopyTo(destination[(int)offset..]);
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
    /// Hashes the specified input with the specified hash algorithm.
    /// </summary>
    /// <param name="hashAlgorithm">The hash algorithm.</param>
    /// <param name="input">The input to hash.</param>
    /// <param name="destination">The buffer that receives the hash, which must be at least 64 bytes long.</param>
    /// <returns>Returns the length of the hash, in bytes.</returns>
    /// <exception cref="CryptographicException">Thrown when the hash algorithm is not supported on the current platform.</exception>
    /// <remarks>
    /// Why: the one-shot SHA-2 methods avoid creating a hash object for every round, which on some platforms costs more than hashing the
    /// short JOSE inputs. Other algorithms use <see cref="IncrementalHash"/>.
    /// </remarks>
    private static int HashData(HashAlgorithmName hashAlgorithm, ReadOnlySpan<byte> input, Span<byte> destination)
    {
        if (hashAlgorithm == HashAlgorithmName.SHA256) return SHA256.HashData(input, destination);
        if (hashAlgorithm == HashAlgorithmName.SHA384) return SHA384.HashData(input, destination);
        if (hashAlgorithm == HashAlgorithmName.SHA512) return SHA512.HashData(input, destination);

        using IncrementalHash hash = IncrementalHash.CreateHash(hashAlgorithm);

        if (hash.HashLengthInBytes > destination.Length)
            throw new CryptographicException($"The hash algorithm '{hashAlgorithm.Name}' produces hashes over {MaxHashLength} bytes.");

        hash.AppendData(input);
        return hash.GetHashAndReset(destination);
    }
}
