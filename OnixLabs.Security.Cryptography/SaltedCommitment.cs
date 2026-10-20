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

/// <summary>
/// Represents a salted commitment: a value that binds to a committed value without revealing it, until its <see cref="CommitmentOpening"/> is disclosed.
/// </summary>
/// <remarks>
/// <para>
/// The commitment is computed as <c>HMAC(key = opening, data = UInt32BE(len(UTF8(domain))) ‖ UTF8(domain) ‖ value)</c>,
/// using HMAC-SHA-256, HMAC-SHA-384 or HMAC-SHA-512.
/// </para>
/// <para>
/// <b>Hiding</b> relies on the opening being secret and uniformly random. A commitment reveals nothing about its value only while the opening stays
/// secret. <b>Binding</b> relies on the collision resistance of the HMAC: whoever created the commitment cannot later open it to a different value.
/// </para>
/// <para>
/// <b>Low-entropy values:</b> a commitment does not protect a guessable value once its opening is disclosed, and anyone who learns the opening can
/// test candidate values against the commitment. Disclose an opening only to parties who may learn the value.
/// </para>
/// <para>
/// <b>Opening reuse:</b> use a fresh opening for every commitment. Reusing an opening links the commitments that share it, and disclosing it opens
/// all of them.
/// </para>
/// <para>
/// <b>Canonical encoding:</b> the caller is responsible for encoding the value canonically. Two encodings of the same logical value produce different
/// commitments, and a verifier must encode the value exactly as the committer did.
/// </para>
/// </remarks>
public readonly partial record struct SaltedCommitment : ICryptoPrimitive<SaltedCommitment>, ISpanParsable<SaltedCommitment>
{
    private const string Separator = ":";
    internal const string UnsupportedAlgorithm = "The hash algorithm must be SHA-256, SHA-384 or SHA-512.";
    private const string HashLengthMismatch = "The length of the commitment does not match the output of the hash algorithm.";

    /// <summary>
    /// Initializes a new instance of the <see cref="SaltedCommitment"/> struct.
    /// </summary>
    /// <param name="hash">The underlying commitment value.</param>
    /// <param name="algorithm">The hash algorithm of the HMAC that produced the commitment.</param>
    /// <exception cref="ArgumentException">
    /// Thrown when <paramref name="algorithm"/> is not supported, or when the length of <paramref name="hash"/> does not match its output.
    /// </exception>
    public SaltedCommitment(Hash hash, HashAlgorithmName algorithm)
    {
        Require(TryGetHashLength(algorithm.Name, out int length), UnsupportedAlgorithm, nameof(algorithm));
        Require(hash.Length == length, HashLengthMismatch, nameof(hash));

        Hash = hash;
        AlgorithmName = algorithm.Name!;
    }

    /// <summary>
    /// Gets the underlying commitment value.
    /// </summary>
    /// <value>The underlying <see cref="Hash"/> value of the commitment.</value>
    // ReSharper disable once MemberCanBePrivate.Global
    public Hash Hash { get; }

    /// <summary>
    /// Gets the name of the hash algorithm of the HMAC that produced the commitment.
    /// </summary>
    /// <value>The name of the hash algorithm of the HMAC that produced the commitment.</value>
    // ReSharper disable once MemberCanBePrivate.Global
    public string AlgorithmName { get; }
}
