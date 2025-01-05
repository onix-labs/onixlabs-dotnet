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
/// Represents a named cryptographic hash.
/// </summary>
public readonly partial record struct NamedHash : ICryptoPrimitive<NamedHash>, ISpanParsable<NamedHash>
{
    private const string Separator = ":";
    private const string HashAlgorithmNameNullOrWhiteSpace = "Hash algorithm name must not be null or whitespace.";

    /// <summary>
    /// Initializes a new instance of the <see cref="NamedHash"/> struct.
    /// </summary>
    /// <param name="hash">The underlying hash value.</param>
    /// <param name="algorithmName">The name of the hash algorithm that was used to produce the associated hash.</param>
    public NamedHash(Hash hash, string algorithmName)
    {
        Hash = hash;
        AlgorithmName = RequireNotNullOrWhiteSpace(algorithmName, HashAlgorithmNameNullOrWhiteSpace);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="NamedHash"/> struct.
    /// </summary>
    /// <param name="hash">The underlying hash value.</param>
    /// <param name="algorithmName">The name of the hash algorithm that was used to produce the associated hash.</param>
    public NamedHash(Hash hash, HashAlgorithmName algorithmName)
    {
        Hash = hash;
        AlgorithmName = RequireNotNullOrWhiteSpace(algorithmName.Name, HashAlgorithmNameNullOrWhiteSpace);
    }

    /// <summary>
    /// Gets the underlying hash value.
    /// </summary>
    // ReSharper disable once MemberCanBePrivate.Global
    public Hash Hash { get; }

    /// <summary>
    /// Gets name of the hash algorithm that was used to produce the associated hash.
    /// </summary>
    // ReSharper disable once MemberCanBePrivate.Global
    public string AlgorithmName { get; }
}
