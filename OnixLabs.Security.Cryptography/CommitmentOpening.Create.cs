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
using System.Security.Cryptography;

namespace OnixLabs.Security.Cryptography;

public readonly partial struct CommitmentOpening
{
    private const string SeedTooShort = "The seed must be at least 32 bytes.";
    private const string PseudorandomKeyTooShort = "The pseudorandom key must be at least as long as the output of the hash algorithm.";

    /// <summary>
    /// Creates a new, cryptographically random <see cref="CommitmentOpening"/>.
    /// </summary>
    /// <returns>Returns a new, cryptographically random <see cref="CommitmentOpening"/>.</returns>
    public static CommitmentOpening Create() => new(RandomNumberGenerator.GetBytes(Length));

    /// <summary>
    /// Derives a <see cref="CommitmentOpening"/> from the specified seed using HKDF-SHA-256 Extract-then-Expand (RFC 5869).
    /// </summary>
    /// <remarks>
    /// Use this method when the seed is secret key material that is not already uniformly random. The extract step uses an empty salt,
    /// which RFC 5869 §2.2 defines as a string of zero bytes. Derivation is deterministic: the same seed and info always produce the same opening,
    /// so use a distinct <paramref name="info"/> for every opening derived from one seed.
    /// </remarks>
    /// <param name="seed">The secret input key material, which must be at least 32 bytes.</param>
    /// <param name="info">The context that binds the derived opening to its purpose.</param>
    /// <returns>Returns a new <see cref="CommitmentOpening"/> derived from the specified seed and info.</returns>
    /// <exception cref="ArgumentException">Thrown when <paramref name="seed"/> is shorter than 32 bytes.</exception>
    public static CommitmentOpening Derive(ReadOnlySpan<byte> seed, ReadOnlySpan<byte> info) =>
        Derive(seed, info, HashAlgorithmName.SHA256);

    /// <summary>
    /// Derives a <see cref="CommitmentOpening"/> from the specified seed using HKDF Extract-then-Expand (RFC 5869).
    /// </summary>
    /// <remarks>
    /// Use this method when the seed is secret key material that is not already uniformly random. The extract step uses an empty salt,
    /// which RFC 5869 §2.2 defines as a string of zero bytes. Derivation is deterministic: the same seed, info and algorithm always produce the same
    /// opening, so use a distinct <paramref name="info"/> for every opening derived from one seed.
    /// </remarks>
    /// <param name="seed">The secret input key material, which must be at least 32 bytes.</param>
    /// <param name="info">The context that binds the derived opening to its purpose.</param>
    /// <param name="algorithm">The hash algorithm to use, which must be SHA-256, SHA-384 or SHA-512.</param>
    /// <returns>Returns a new <see cref="CommitmentOpening"/> derived from the specified seed and info.</returns>
    /// <exception cref="ArgumentException">
    /// Thrown when <paramref name="seed"/> is shorter than 32 bytes, or when <paramref name="algorithm"/> is not supported.
    /// </exception>
    public static CommitmentOpening Derive(ReadOnlySpan<byte> seed, ReadOnlySpan<byte> info, HashAlgorithmName algorithm)
    {
        Require(SaltedCommitment.TryGetHashLength(algorithm.Name, out int _), SaltedCommitment.UnsupportedAlgorithm, nameof(algorithm));
        Require(seed.Length >= Length, SeedTooShort, nameof(seed));

        Span<byte> output = stackalloc byte[Length];
        HKDF.DeriveKey(algorithm, seed, output, ReadOnlySpan<byte>.Empty, info);
        return FromSensitiveBuffer(output);
    }

    /// <summary>
    /// Derives a <see cref="CommitmentOpening"/> from the specified pseudorandom key using HKDF-SHA-256 Expand only (RFC 5869).
    /// </summary>
    /// <remarks>
    /// Use this method only when the key is already uniformly random, for example the output of a previous HKDF extract step;
    /// otherwise use <see cref="Derive(ReadOnlySpan{byte}, ReadOnlySpan{byte})"/>. Derivation is deterministic: the same key and info always produce
    /// the same opening, so use a distinct <paramref name="info"/> for every opening derived from one key.
    /// </remarks>
    /// <param name="pseudorandomKey">The uniformly random key, which must be at least 32 bytes.</param>
    /// <param name="info">The context that binds the derived opening to its purpose.</param>
    /// <returns>Returns a new <see cref="CommitmentOpening"/> derived from the specified key and info.</returns>
    /// <exception cref="ArgumentException">Thrown when <paramref name="pseudorandomKey"/> is shorter than 32 bytes.</exception>
    public static CommitmentOpening Expand(ReadOnlySpan<byte> pseudorandomKey, ReadOnlySpan<byte> info) =>
        Expand(pseudorandomKey, info, HashAlgorithmName.SHA256);

    /// <summary>
    /// Derives a <see cref="CommitmentOpening"/> from the specified pseudorandom key using HKDF Expand only (RFC 5869).
    /// </summary>
    /// <remarks>
    /// Use this method only when the key is already uniformly random, for example the output of a previous HKDF extract step;
    /// otherwise use <see cref="Derive(ReadOnlySpan{byte}, ReadOnlySpan{byte}, HashAlgorithmName)"/>. Derivation is deterministic: the same key,
    /// info and algorithm always produce the same opening, so use a distinct <paramref name="info"/> for every opening derived from one key.
    /// </remarks>
    /// <param name="pseudorandomKey">The uniformly random key, which must be at least as long as the output of <paramref name="algorithm"/>.</param>
    /// <param name="info">The context that binds the derived opening to its purpose.</param>
    /// <param name="algorithm">The hash algorithm to use, which must be SHA-256, SHA-384 or SHA-512.</param>
    /// <returns>Returns a new <see cref="CommitmentOpening"/> derived from the specified key and info.</returns>
    /// <exception cref="ArgumentException">
    /// Thrown when <paramref name="pseudorandomKey"/> is shorter than the output of <paramref name="algorithm"/>,
    /// or when <paramref name="algorithm"/> is not supported.
    /// </exception>
    public static CommitmentOpening Expand(ReadOnlySpan<byte> pseudorandomKey, ReadOnlySpan<byte> info, HashAlgorithmName algorithm)
    {
        Require(SaltedCommitment.TryGetHashLength(algorithm.Name, out int hashLength), SaltedCommitment.UnsupportedAlgorithm, nameof(algorithm));
        Require(pseudorandomKey.Length >= hashLength, PseudorandomKeyTooShort, nameof(pseudorandomKey));

        Span<byte> output = stackalloc byte[Length];
        HKDF.Expand(algorithm, pseudorandomKey, output, info);
        return FromSensitiveBuffer(output);
    }

    /// <summary>
    /// Creates a new <see cref="CommitmentOpening"/> from the specified buffer, then zeroes the buffer.
    /// </summary>
    /// <param name="buffer">The buffer containing the opening, which is zeroed once it has been copied.</param>
    /// <returns>Returns a new <see cref="CommitmentOpening"/> containing a copy of the specified buffer.</returns>
    private static CommitmentOpening FromSensitiveBuffer(Span<byte> buffer)
    {
        CommitmentOpening result = new(buffer);
        CryptographicOperations.ZeroMemory(buffer);
        return result;
    }
}
