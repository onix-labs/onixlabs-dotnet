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

public readonly partial record struct SaltedCommitment
{
    /// <summary>
    /// Determines whether the current <see cref="SaltedCommitment"/> opens to the specified value, domain and opening.
    /// </summary>
    /// <remarks>
    /// The comparison runs in constant time, so it does not reveal how much of the commitment matched.
    /// </remarks>
    /// <param name="value">The canonically encoded value that the commitment is claimed to commit to.</param>
    /// <param name="domain">The domain that the commitment is claimed to have been made in.</param>
    /// <param name="opening">The disclosed opening of the commitment.</param>
    /// <returns>
    /// Returns <see langword="true"/> if the current commitment opens to the specified value, domain and opening; otherwise, <see langword="false"/>,
    /// including when <paramref name="opening"/> is not a valid opening or the current commitment is <see langword="default"/>.
    /// </returns>
    /// <exception cref="ArgumentException">Thrown when <paramref name="domain"/> is <see langword="null"/> or whitespace.</exception>
    public bool Verify(ReadOnlySpan<byte> value, string domain, CommitmentOpening opening)
    {
        RequireNotNullOrWhiteSpace(domain, DomainNullOrWhiteSpace);

        if (AlgorithmName is null || !opening.IsValid) return false;

        byte[] expected = Compute(value, domain, opening, new HashAlgorithmName(AlgorithmName));
        return CryptographicOperations.FixedTimeEquals(expected, Hash.AsReadOnlySpan());
    }
}
