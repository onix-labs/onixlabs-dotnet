// Copyright 2020-2024 ONIXLabs
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

public sealed partial class EcdsaPrivateKey
{
    /// <summary>
    /// Creates an ECDSA cryptographic private key.
    /// </summary>
    /// <returns>Returns a new ECDSA cryptographic private key.</returns>
    public static EcdsaPrivateKey Create() => Create(ECDsa.Create());

    /// <summary>
    /// Creates an ECDSA cryptographic private key using the specified <see cref="ECCurve"/>.
    /// </summary>
    /// <param name="curve">The elliptic curve from which to create an ECDSA cryptographic private key.</param>
    /// <returns>Returns a new ECDSA cryptographic private key.</returns>
    public static EcdsaPrivateKey Create(ECCurve curve) => Create(ECDsa.Create(curve));

    /// <summary>
    /// Creates an ECDSA cryptographic private key using the specified <see cref="ECParameters"/>.
    /// </summary>
    /// <param name="parameters">The elliptic curve parameters from which to create an ECDSA cryptographic private key.</param>
    /// <returns>Returns a new ECDSA cryptographic private key.</returns>
    public static EcdsaPrivateKey Create(ECParameters parameters) => Create(ECDsa.Create(parameters));

    /// <summary>
    /// Creates an ECDSA cryptographic private key using the specified <see cref="ECAlgorithm"/>.
    /// </summary>
    /// <param name="algorithm">The elliptic curve algorithm from which to create an ECDSA cryptographic private key.</param>
    /// <returns>Returns a new ECDSA cryptographic private key.</returns>
    private static EcdsaPrivateKey Create(ECAlgorithm algorithm) => new(algorithm.ExportECPrivateKey());
}
