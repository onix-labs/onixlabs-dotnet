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

namespace OnixLabs.Security.Cryptography;

/// <summary>
/// Represents the secret opening of a <see cref="SaltedCommitment"/>: the 32-byte key that, together with the committed value and domain,
/// reproduces the commitment.
/// </summary>
/// <remarks>
/// <para>
/// To open a commitment, disclose the committed value together with this opening. In the literature, "opening" often refers to that pair,
/// or the random part is called the salt, randomness or blinding factor; here, it refers only to the random part.
/// </para>
/// <para>
/// Anyone who holds the opening and the committed value can prove what was committed, so the opening must be treated as secret until it is
/// deliberately disclosed. For this reason, <see cref="ToString()"/> never reveals the value; use <see cref="ToString(IFormatProvider)"/> or
/// <see cref="AsReadOnlySpan"/> to export it on purpose.
/// </para>
/// <para>
/// An opening is always exactly <see cref="Length"/> bytes. HMAC zero-pads short keys and hashes long ones, so different keys could otherwise
/// produce the same commitment; a fixed length is what makes each opening unique.
/// </para>
/// </remarks>
// ReSharper disable MemberCanBePrivate.Global
public readonly partial struct CommitmentOpening : ICryptoPrimitive<CommitmentOpening>
{
    /// <summary>
    /// The length of every commitment opening, in bytes.
    /// </summary>
    public const int Length = 32;

    private const string InvalidLength = "A commitment opening must be exactly 32 bytes.";

    private readonly byte[] value;

    /// <summary>
    /// Initializes a new instance of the <see cref="CommitmentOpening"/> struct.
    /// </summary>
    /// <remarks>
    /// This constructor is intentionally marked <see langword="private"/> and is used exclusively to validate and initialize the underlying <see cref="byte"/> array.
    /// Because <see cref="CommitmentOpening"/> is designed to be immutable, external array references are not permitted.
    /// The overloaded constructors below ensure immutability by creating defensive copies of the provided arrays.
    /// </remarks>
    /// <param name="value">The value from which to initialize a new <see cref="CommitmentOpening"/> instance.</param>
    /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not exactly <see cref="Length"/> bytes.</exception>
    private CommitmentOpening(byte[] value)
    {
        Require(value.Length == Length, InvalidLength, nameof(value));
        this.value = value;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="CommitmentOpening"/> struct.
    /// </summary>
    /// <param name="value">The value from which to initialize a new <see cref="CommitmentOpening"/> instance.</param>
    /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not exactly <see cref="Length"/> bytes.</exception>
    public CommitmentOpening(ReadOnlySpan<byte> value) : this(value.ToArray())
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="CommitmentOpening"/> struct.
    /// </summary>
    /// <param name="value">The value from which to initialize a new <see cref="CommitmentOpening"/> instance.</param>
    /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not exactly <see cref="Length"/> bytes.</exception>
    public CommitmentOpening(ReadOnlyMemory<byte> value) : this(value.ToArray())
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="CommitmentOpening"/> struct.
    /// </summary>
    /// <param name="value">The value from which to initialize a new <see cref="CommitmentOpening"/> instance.</param>
    /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not exactly <see cref="Length"/> bytes.</exception>
    public CommitmentOpening(ReadOnlySequence<byte> value) : this(value.ToArray())
    {
    }

    /// <summary>
    /// Gets a value indicating whether the current <see cref="CommitmentOpening"/> holds a valid opening.
    /// </summary>
    /// <value>
    /// <see langword="true"/> if the current opening is exactly <see cref="Length"/> bytes; otherwise, <see langword="false"/>,
    /// which is only the case for a <see langword="default"/> instance.
    /// </value>
    internal bool IsValid => value?.Length == Length;
}
