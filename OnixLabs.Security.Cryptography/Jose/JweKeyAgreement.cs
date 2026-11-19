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
using System.Threading;
using System.Threading.Tasks;

namespace OnixLabs.Security.Cryptography.Jose;

/// <summary>
/// Represents factories for <see cref="IJweKeyAgreement"/> instances backed by the library's private key types.
/// </summary>
public static class JweKeyAgreement
{
    /// <summary>
    /// Creates a key agreement backed by the specified X25519 private key.
    /// </summary>
    /// <param name="key">The X25519 private key.</param>
    /// <param name="keyId">The key identifier, or <see langword="null"/>.</param>
    /// <returns>Returns a new <see cref="IJweKeyAgreement"/>.</returns>
    public static IJweKeyAgreement Create(XdhPrivateKey key, string? keyId = null)
    {
        ArgumentNullException.ThrowIfNull(key);
        return new XdhKeyAgreement(key, JsonWebKey.Create(key.GetPublicKey(), keyId));
    }

    /// <summary>
    /// Creates a key agreement backed by the specified EC Diffie-Hellman private key.
    /// </summary>
    /// <param name="key">The private key, on P-256, P-384 or P-521.</param>
    /// <param name="keyId">The key identifier, or <see langword="null"/>.</param>
    /// <returns>Returns a new <see cref="IJweKeyAgreement"/>.</returns>
    public static IJweKeyAgreement Create(EcdhPrivateKey key, string? keyId = null)
    {
        ArgumentNullException.ThrowIfNull(key);
        return new EcdhKeyAgreement(key, JsonWebKey.Create(key.GetPublicKey(), keyId));
    }

    /// <summary>
    /// Checks that the specified public key is of the same type and curve as the specified key.
    /// </summary>
    /// <param name="expected">The key whose type and curve are required.</param>
    /// <param name="actual">The key to check.</param>
    /// <exception cref="CryptographicException">Thrown when the keys differ in type or curve, or <paramref name="actual"/> has a private
    /// part.</exception>
    internal static void RequireSameCurve(JsonWebKey expected, JsonWebKey actual)
    {
        if (actual.KeyType != expected.KeyType || actual.Curve != expected.Curve)
            throw new CryptographicException($"The key is on {actual.Curve}, but {expected.Curve} is required.");

        if (actual.HasPrivateKey) throw new CryptographicException("The other party's key must be a public key.");
    }

    /// <summary>
    /// Represents a key agreement backed by an X25519 private key.
    /// </summary>
    /// <param name="key">The X25519 private key.</param>
    /// <param name="publicKey">The public JWK of <paramref name="key"/>.</param>
    private sealed class XdhKeyAgreement(XdhPrivateKey key, JsonWebKey publicKey) : IJweKeyAgreement
    {
        /// <inheritdoc/>
        public JsonWebKey PublicKey => publicKey;

        /// <inheritdoc/>
        public ValueTask<Secret> DeriveSharedSecretAsync(JsonWebKey otherPartyPublicKey, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            RequireSameCurve(publicKey, otherPartyPublicKey);
            return ValueTask.FromResult(key.DeriveRawSharedSecret(otherPartyPublicKey.ToXdhPublicKey()));
        }
    }

    /// <summary>
    /// Represents a key agreement backed by an EC Diffie-Hellman private key.
    /// </summary>
    /// <param name="key">The EC Diffie-Hellman private key.</param>
    /// <param name="publicKey">The public JWK of <paramref name="key"/>.</param>
    private sealed class EcdhKeyAgreement(EcdhPrivateKey key, JsonWebKey publicKey) : IJweKeyAgreement
    {
        /// <inheritdoc/>
        public JsonWebKey PublicKey => publicKey;

        /// <inheritdoc/>
        public ValueTask<Secret> DeriveSharedSecretAsync(JsonWebKey otherPartyPublicKey, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            RequireSameCurve(publicKey, otherPartyPublicKey);
            return ValueTask.FromResult(key.DeriveRawSharedSecret(otherPartyPublicKey.ToEcdhPublicKey()));
        }
    }
}
