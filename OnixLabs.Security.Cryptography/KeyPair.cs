// Copyright 2020-2022 ONIXLabs
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
/// Represents a cryptographic public/private key pair.
/// </summary>
public sealed partial class KeyPair
{
    /// <summary>
    /// Prevents new instances of the <see cref="KeyPair"/> class from being created.
    /// </summary>
    /// <param name="privateKey">The private key component of this key pair.</param>
    /// <param name="publicKey">The public key component of this key pair.</param>
    /// <param name="algorithmType">The hash algorithm type of this key pair.</param>
    private KeyPair(PrivateKey privateKey, PublicKey publicKey, HashAlgorithmType algorithmType)
    {
        PrivateKey = privateKey;
        PublicKey = publicKey;
        AlgorithmType = algorithmType;
    }

    /// <summary>
    /// Gets the private key component of this key pair.
    /// </summary>
    public PrivateKey PrivateKey { get; }

    /// <summary>
    /// Gets the public key component of this key pair.
    /// </summary>
    public PublicKey PublicKey { get; }

    /// <summary>
    /// Gets the hash algorithm type of this key pair.
    /// </summary>
    public HashAlgorithmType AlgorithmType { get; }
}
