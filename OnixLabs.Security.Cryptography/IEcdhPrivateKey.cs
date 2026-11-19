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
/// Defines an EC Diffie-Hellman cryptographic private key.
/// </summary>
public interface IEcdhPrivateKey :
    IPrivateKeyDerivable<EcdhPublicKey>,
    IPrivateKeyImportable<EcdhPrivateKey>,
    IPrivateKeyExportable,
    IBinaryConvertible
{
    /// <summary>
    /// Derives a hashed cryptographic shared secret from the current EC Diffie-Hellman cryptographic private key, and the specified
    /// cryptographic public key.
    /// </summary>
    /// <param name="publicKey">The EC Diffie-Hellman cryptographic public key from which to derive cryptographic shared secret.</param>
    /// <returns>
    /// Returns the SHA-256 hash of the raw shared secret returned by <see cref="DeriveRawSharedSecret"/>, as a 32-byte cryptographic shared
    /// secret, whatever the curve.
    /// </returns>
    /// <exception cref="System.Security.Cryptography.CryptographicException">
    /// Thrown when <paramref name="publicKey"/> cannot be imported, is not on the same named curve as the current private key, or is not a
    /// point on that curve.
    /// </exception>
    /// <remarks>
    /// Use this method when both parties use it, and need a symmetric key without agreeing on a key derivation function. The result is the
    /// same as .NET's <c>ECDiffieHellman.DeriveKeyMaterial</c>, which hashes with SHA-256 by default. It behaves as
    /// <see cref="IXdhPrivateKey.DeriveSharedSecret"/> does. Use <see cref="DeriveRawSharedSecret"/> where a standard specifies its own key
    /// derivation, as JOSE does.
    /// </remarks>
    Secret DeriveSharedSecret(IEcdhPublicKey publicKey);

    /// <summary>
    /// Derives the raw EC Diffie-Hellman shared secret from the current EC Diffie-Hellman cryptographic private key, and the specified
    /// cryptographic public key.
    /// </summary>
    /// <param name="publicKey">The EC Diffie-Hellman cryptographic public key from which to derive the cryptographic shared secret.</param>
    /// <returns>
    /// Returns the raw shared secret Z, the big-endian x-coordinate of the shared point, as long as the field size of the curve (32 bytes
    /// for P-256, 48 for P-384 and 66 for P-521). Unlike <see cref="DeriveSharedSecret"/>, it is not hashed; derive keys from it with a key
    /// derivation function such as <see cref="ConcatKdf"/>.
    /// </returns>
    /// <exception cref="System.Security.Cryptography.CryptographicException">
    /// Thrown when <paramref name="publicKey"/> cannot be imported, is not on the same named curve as the current private key, or is not a
    /// point on that curve.
    /// </exception>
    /// <remarks>
    /// Use this method where a standard specifies how keys are derived from the shared secret, as JOSE's ECDH-ES and ECDH-1PU do. It
    /// behaves as <see cref="IXdhPrivateKey.DeriveRawSharedSecret"/> does. The raw secret is not uniformly random, so it must not be used
    /// as a key directly.
    /// </remarks>
    Secret DeriveRawSharedSecret(IEcdhPublicKey publicKey);
}
