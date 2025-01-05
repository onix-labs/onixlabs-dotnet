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

using OnixLabs.Core;

namespace OnixLabs.Security.Cryptography;

/// <summary>
/// Represents a named cryptographic public key.
/// </summary>
public readonly partial record struct NamedPublicKey : ICryptoPrimitive<NamedPublicKey>
{
    private const string Separator = ":";
    private const string KeyAlgorithmNameNullOrWhiteSpace = "Key algorithm name must not be null or whitespace.";

    /// <summary>
    /// Initializes a new instance of the <see cref="NamedPublicKey"/> struct.
    /// </summary>
    /// <param name="publicKey">The underlying public key value.</param>
    /// <param name="algorithmName">The name of the key algorithm that was used to produce the associated public key.</param>
    public NamedPublicKey(PublicKey publicKey, string algorithmName)
    {
        PublicKey = publicKey;
        AlgorithmName = RequireNotNullOrWhiteSpace(algorithmName, KeyAlgorithmNameNullOrWhiteSpace);
    }

    /// <summary>
    /// Gets the underlying public key value.
    /// </summary>
    // ReSharper disable once MemberCanBePrivate.Global
    public PublicKey PublicKey { get; }

    /// <summary>
    /// Gets the name of the key algorithm that was used to produce the associated public key.
    /// </summary>
    // ReSharper disable once MemberCanBePrivate.Global
    public string AlgorithmName { get; }
}
