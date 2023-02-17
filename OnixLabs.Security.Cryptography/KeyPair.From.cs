// Copyright 2020-2023 ONIXLabs
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
using static OnixLabs.Core.Preconditions;

namespace OnixLabs.Security.Cryptography;

public sealed partial class KeyPair
{
    /// <summary>
    /// Creates a key pair from the specified public and private key components.
    /// </summary>
    /// <param name="privateKey">The private key component of the key pair.</param>
    /// <param name="publicKey">The public key component of the key pair.</param>
    /// <param name="type">The hash algorithm type of the key pair.</param>
    /// <returns>Returns a new <see cref="KeyPair"/> instance from the key components.</returns>
    /// <exception cref="ArgumentException">If the private key hash algorithm type is mismatched.</exception>
    /// <exception cref="ArgumentException">If the public key hash algorithm type is mismatched.</exception>
    /// <exception cref="ArgumentException">If the private and public key components are mismatched.</exception>
    public static KeyPair FromKeyComponents(PrivateKey privateKey, PublicKey publicKey, HashAlgorithmType type)
    {
        Require(privateKey.AlgorithmType == type, $"Private key hash algorithm type is mismatched with '{type}'.", nameof(privateKey));
        Require(publicKey.AlgorithmType == type, $"Public key hash algorithm type is mismatched with '{type}'.", nameof(publicKey));

        byte[] random = Guid.NewGuid().ToByteArray();
        Hash hash = Hash.ComputeSha2Hash256(random);
        DigitalSignature signature = privateKey.SignHash(hash);

        Check(signature.IsHashValid(hash, publicKey), "Invalid key pair. The specified public and private keys are mismatched.");

        return new KeyPair(privateKey, publicKey, type);
    }
}
