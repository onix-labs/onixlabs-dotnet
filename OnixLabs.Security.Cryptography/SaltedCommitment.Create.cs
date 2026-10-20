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
using System.Text;

namespace OnixLabs.Security.Cryptography;

public readonly partial record struct SaltedCommitment
{
    private const string DomainNullOrWhiteSpace = "The domain must not be null or whitespace.";
    private const string InvalidOpening = "The commitment opening must be exactly 32 bytes.";

    /// <summary>
    /// Creates a new HMAC-SHA-256 <see cref="SaltedCommitment"/> to the specified value, using a new random opening.
    /// </summary>
    /// <param name="value">The canonically encoded value to commit to.</param>
    /// <param name="domain">The domain that separates this commitment from commitments made for other purposes.</param>
    /// <param name="opening">The new random opening, which must be kept secret until the commitment is opened.</param>
    /// <returns>Returns a new <see cref="SaltedCommitment"/> to the specified value.</returns>
    /// <exception cref="ArgumentException">Thrown when <paramref name="domain"/> is <see langword="null"/> or whitespace.</exception>
    public static SaltedCommitment Create(ReadOnlySpan<byte> value, string domain, out CommitmentOpening opening) =>
        Create(value, domain, out opening, HashAlgorithmName.SHA256);

    /// <summary>
    /// Creates a new <see cref="SaltedCommitment"/> to the specified value, using a new random opening.
    /// </summary>
    /// <param name="value">The canonically encoded value to commit to.</param>
    /// <param name="domain">The domain that separates this commitment from commitments made for other purposes.</param>
    /// <param name="opening">The new random opening, which must be kept secret until the commitment is opened.</param>
    /// <param name="algorithm">The hash algorithm of the HMAC, which must be SHA-256, SHA-384 or SHA-512.</param>
    /// <returns>Returns a new <see cref="SaltedCommitment"/> to the specified value.</returns>
    /// <exception cref="ArgumentException">
    /// Thrown when <paramref name="domain"/> is <see langword="null"/> or whitespace, or when <paramref name="algorithm"/> is not supported.
    /// </exception>
    public static SaltedCommitment Create(ReadOnlySpan<byte> value, string domain, out CommitmentOpening opening, HashAlgorithmName algorithm)
    {
        opening = CommitmentOpening.Create();
        return Create(value, domain, opening, algorithm);
    }

    /// <summary>
    /// Creates a new HMAC-SHA-256 <see cref="SaltedCommitment"/> to the specified value, using the specified opening.
    /// </summary>
    /// <param name="value">The canonically encoded value to commit to.</param>
    /// <param name="domain">The domain that separates this commitment from commitments made for other purposes.</param>
    /// <param name="opening">The secret opening, which must not have been used for any other commitment.</param>
    /// <returns>Returns a new <see cref="SaltedCommitment"/> to the specified value.</returns>
    /// <exception cref="ArgumentException">
    /// Thrown when <paramref name="domain"/> is <see langword="null"/> or whitespace, or when <paramref name="opening"/> is not exactly 32 bytes.
    /// </exception>
    public static SaltedCommitment Create(ReadOnlySpan<byte> value, string domain, CommitmentOpening opening) =>
        Create(value, domain, opening, HashAlgorithmName.SHA256);

    /// <summary>
    /// Creates a new <see cref="SaltedCommitment"/> to the specified value, using the specified opening.
    /// </summary>
    /// <param name="value">The canonically encoded value to commit to.</param>
    /// <param name="domain">The domain that separates this commitment from commitments made for other purposes.</param>
    /// <param name="opening">The secret opening, which must not have been used for any other commitment.</param>
    /// <param name="algorithm">The hash algorithm of the HMAC, which must be SHA-256, SHA-384 or SHA-512.</param>
    /// <returns>Returns a new <see cref="SaltedCommitment"/> to the specified value.</returns>
    /// <exception cref="ArgumentException">
    /// Thrown when <paramref name="domain"/> is <see langword="null"/> or whitespace, when <paramref name="opening"/> is not exactly 32 bytes,
    /// or when <paramref name="algorithm"/> is not supported.
    /// </exception>
    public static SaltedCommitment Create(ReadOnlySpan<byte> value, string domain, CommitmentOpening opening, HashAlgorithmName algorithm)
    {
        RequireNotNullOrWhiteSpace(domain, DomainNullOrWhiteSpace);
        Require(opening.IsValid, InvalidOpening, nameof(opening));
        Require(TryGetHashLength(algorithm.Name, out int _), UnsupportedAlgorithm, nameof(algorithm));

        return new SaltedCommitment(new Hash(Compute(value, domain, opening, algorithm)), algorithm);
    }

    /// <summary>
    /// Computes the HMAC of the domain-separated message for the specified value, keyed with the specified opening.
    /// </summary>
    /// <param name="value">The canonically encoded value to commit to.</param>
    /// <param name="domain">The domain that separates this commitment from commitments made for other purposes.</param>
    /// <param name="opening">The secret opening, used as the HMAC key.</param>
    /// <param name="algorithm">The hash algorithm of the HMAC.</param>
    /// <returns>Returns the HMAC of <c>UInt32BE(len(UTF8(domain))) ‖ UTF8(domain) ‖ value</c>, keyed with the opening.</returns>
    private static byte[] Compute(ReadOnlySpan<byte> value, string domain, CommitmentOpening opening, HashAlgorithmName algorithm)
    {
        byte[] domainBytes = Encoding.UTF8.GetBytes(domain);
        Span<byte> domainLength = stackalloc byte[sizeof(uint)];
        BinaryPrimitives.WriteUInt32BigEndian(domainLength, (uint)domainBytes.Length);

        using IncrementalHash hmac = IncrementalHash.CreateHMAC(algorithm, opening.AsReadOnlySpan());
        hmac.AppendData(domainLength);
        hmac.AppendData(domainBytes);
        hmac.AppendData(value);
        return hmac.GetHashAndReset();
    }
}
