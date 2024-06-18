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

namespace OnixLabs.Security.Cryptography;

/// <summary>
/// Represents a named cryptographic private key.
/// </summary>
public readonly partial record struct NamedPrivateKey : ICryptoPrimitive<NamedPrivateKey>
{
    private const string KeyAlgorithmNameNullOrWhiteSpace = "Key algorithm name must not be null or whitespace.";

    /// <summary>
    /// Initializes a new instance of the <see cref="NamedPrivateKey"/> struct.
    /// </summary>
    /// <param name="privateKey">The underlying private key value.</param>
    /// <param name="algorithmName">The name of the key algorithm that was used to produce the associated private key.</param>
    public NamedPrivateKey(PrivateKey privateKey, string algorithmName)
    {
        PrivateKey = privateKey;
        AlgorithmName = RequireNotNullOrWhiteSpace(algorithmName, KeyAlgorithmNameNullOrWhiteSpace);
    }

    /// <summary>
    /// Gets the underlying private key value.
    /// </summary>
    public PrivateKey PrivateKey { get; }

    /// <summary>
    /// Gets the name of the key algorithm that was used to produce the associated private key.
    /// </summary>
    public string AlgorithmName { get; }
}
