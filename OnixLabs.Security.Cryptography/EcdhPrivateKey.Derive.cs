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

public sealed partial class EcdhPrivateKey
{
    /// <summary>
    /// Derives a cryptographic shared secret from the current EC Diffie-Hellman cryptographic private key, and the specified cryptographic public key.
    /// </summary>
    /// <param name="publicKey">The EC Diffie-Hellman cryptographic public key from which to derive cryptographic shared secret.</param>
    /// <returns>Returns a cryptographic shared secret, derived from the current EC Diffie-Hellman cryptographic private key, and the specified cryptographic public key.</returns>
    public Secret DeriveSharedSecret(IEcdhPublicKey publicKey)
    {
        using ECDiffieHellman ourKey = ImportKeyData();
        using ECDiffieHellman theirKey = ECDiffieHellman.Create();
        theirKey.ImportSubjectPublicKeyInfo(publicKey.AsReadOnlySpan(), out int _);
        byte[] secretData = ourKey.DeriveKeyMaterial(theirKey.PublicKey);
        return new Secret(secretData);
    }
}
