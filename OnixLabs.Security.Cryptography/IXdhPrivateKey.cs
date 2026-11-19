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
/// Defines an X25519 Diffie-Hellman cryptographic private key, as specified in RFC 7748.
/// </summary>
public interface IXdhPrivateKey :
    IPrivateKeyDerivable<XdhPublicKey>,
    IPrivateKeyRawImportable<XdhPrivateKey>,
    IPrivateKeyPkcs8Importable<XdhPrivateKey>,
    IPrivateKeyPemImportable<XdhPrivateKey>,
    IPrivateKeyRawExportable,
    IPrivateKeyPkcs8Exportable,
    IPrivateKeyPkcs8PemExportable,
    IBinaryConvertible
{
    /// <summary>
    /// Derives a hashed cryptographic shared secret from the current X25519 cryptographic private key, and the specified cryptographic
    /// public key.
    /// </summary>
    /// <param name="publicKey">The X25519 cryptographic public key from which to derive the cryptographic shared secret.</param>
    /// <returns>
    /// Returns the SHA-256 hash of the raw shared secret returned by <see cref="DeriveRawSharedSecret"/>, as a 32-byte cryptographic shared
    /// secret.
    /// </returns>
    /// <exception cref="System.Security.Cryptography.CryptographicException">
    /// Thrown when <paramref name="publicKey"/> is not 32 bytes, or is a point of small order, so that the raw shared secret is all zeros.
    /// </exception>
    /// <remarks>
    /// Use this method when both parties use it, and need a symmetric key without agreeing on a key derivation function. It behaves as
    /// <see cref="IEcdhPrivateKey.DeriveSharedSecret"/> does, which hashes with SHA-256 as .NET's <c>ECDiffieHellman.DeriveKeyMaterial</c>
    /// does by default. Use <see cref="DeriveRawSharedSecret"/> where a standard specifies its own key derivation, as JOSE does.
    /// </remarks>
    Secret DeriveSharedSecret(IXdhPublicKey publicKey);

    /// <summary>
    /// Derives the raw X25519 shared secret from the current X25519 cryptographic private key, and the specified cryptographic public key.
    /// </summary>
    /// <param name="publicKey">The X25519 cryptographic public key from which to derive the cryptographic shared secret.</param>
    /// <returns>
    /// Returns the raw 32-byte X25519 output, X25519(k, u), as a cryptographic shared secret. Unlike <see cref="DeriveSharedSecret"/>, it
    /// is not hashed; derive keys from it with a key derivation function such as <see cref="ConcatKdf"/>.
    /// </returns>
    /// <exception cref="System.Security.Cryptography.CryptographicException">
    /// Thrown when <paramref name="publicKey"/> is not 32 bytes, or is a point of small order, so that the shared secret is all zeros.
    /// </exception>
    /// <remarks>
    /// Use this method where a standard specifies how keys are derived from the shared secret, as JOSE's ECDH-ES and ECDH-1PU do. It
    /// behaves as <see cref="IEcdhPrivateKey.DeriveRawSharedSecret"/> does. The raw secret is not uniformly random, so it must not be used
    /// as a key directly.
    /// </remarks>
    Secret DeriveRawSharedSecret(IXdhPublicKey publicKey);
}
