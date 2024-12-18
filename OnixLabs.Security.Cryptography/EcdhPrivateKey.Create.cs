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

using System.Security.Cryptography;

namespace OnixLabs.Security.Cryptography;

// ReSharper disable HeapView.ObjectAllocation.Evident
public sealed partial class EcdhPrivateKey
{
    /// <summary>
    /// Creates a new EC Diffie-Hellman cryptographic private key.
    /// </summary>
    /// <returns>Returns a new <see cref="EcdsaPrivateKey"/> instance.</returns>
    public static EcdhPrivateKey Create()
    {
        using ECDiffieHellman algorithm = ECDiffieHellman.Create();
        return new EcdhPrivateKey(algorithm);
    }

    /// <summary>
    /// Creates a new EC Diffie-Hellman cryptographic private key.
    /// </summary>
    /// <param name="curve">The elliptic curve from which to create a new EC Diffie-Hellman cryptographic private key.</param>
    /// <returns>Returns a new <see cref="EcdsaPrivateKey"/> instance.</returns>
    public static EcdhPrivateKey Create(ECCurve curve)
    {
        using ECDiffieHellman algorithm = ECDiffieHellman.Create(curve);
        return new EcdhPrivateKey(algorithm);
    }

    /// <summary>
    /// Creates a new EC Diffie-Hellman cryptographic private key.
    /// </summary>
    /// <param name="parameters">The elliptic curve parameters from which to create a new EC Diffie-Hellman cryptographic private key.</param>
    /// <returns>Returns a new <see cref="EcdsaPrivateKey"/> instance.</returns>
    public static EcdhPrivateKey Create(ECParameters parameters)
    {
        using ECDiffieHellman algorithm = ECDiffieHellman.Create(parameters);
        return new EcdhPrivateKey(algorithm);
    }
}
