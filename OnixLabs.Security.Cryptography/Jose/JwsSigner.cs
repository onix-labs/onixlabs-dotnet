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
/// Represents factories for <see cref="IJwsSigner"/> instances backed by the library's private key types.
/// </summary>
public static class JwsSigner
{
    /// <summary>
    /// Creates a signer that signs with the specified Ed25519 private key, as <see cref="JwsAlgorithm.EdDsa"/>.
    /// </summary>
    /// <param name="key">The Ed25519 private key.</param>
    /// <param name="keyId">The key identifier to write as <c>kid</c>, or <see langword="null"/>.</param>
    /// <returns>Returns a new <see cref="IJwsSigner"/>.</returns>
    public static IJwsSigner Create(EddsaPrivateKey key, string? keyId = null)
    {
        RequireNotNull(key);
        return new EddsaSigner(key, keyId);
    }

    /// <summary>
    /// Creates a signer that signs with the specified ECDSA private key, as <see cref="JwsAlgorithm.Es256"/>,
    /// <see cref="JwsAlgorithm.Es384"/>
    /// or <see cref="JwsAlgorithm.Es512"/> according to its curve.
    /// </summary>
    /// <param name="key">The ECDSA private key, on P-256, P-384 or P-521.</param>
    /// <param name="keyId">The key identifier to write as <c>kid</c>, or <see langword="null"/>.</param>
    /// <returns>Returns a new <see cref="IJwsSigner"/>.</returns>
    /// <exception cref="NotSupportedException">Thrown when the key is on secp256k1, for which only verification is supported.</exception>
    public static IJwsSigner Create(EcdsaPrivateKey key, string? keyId = null)
    {
        RequireNotNull(key);

        string curve;
        using (JsonWebKey jwk = JsonWebKey.Create(key)) curve = jwk.Curve!;

        string algorithm = JwsAlgorithm.GetEcdsaAlgorithm(curve);

        if (algorithm == JwsAlgorithm.Es256K)
            throw new NotSupportedException("Signing with secp256k1 (ES256K) is not supported; only verification is.");

        JwsAlgorithm.TryGetEcdsaParameters(algorithm, out _, out HashAlgorithmName hashAlgorithm);
        return new EcdsaSigner(key, keyId, algorithm, hashAlgorithm);
    }

    /// <summary>
    /// Represents a signer backed by an Ed25519 private key.
    /// </summary>
    /// <param name="key">The Ed25519 private key.</param>
    /// <param name="keyId">The key identifier.</param>
    private sealed class EddsaSigner(EddsaPrivateKey key, string? keyId) : IJwsSigner
    {
        /// <inheritdoc/>
        public string Algorithm => JwsAlgorithm.EdDsa;

        /// <inheritdoc/>
        public string? KeyId => keyId;

        /// <inheritdoc/>
        public ValueTask<byte[]> SignAsync(ReadOnlyMemory<byte> signingInput, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return ValueTask.FromResult(key.SignData(signingInput.Span));
        }
    }

    /// <summary>
    /// Represents a signer backed by an ECDSA private key.
    /// </summary>
    /// <param name="key">The ECDSA private key.</param>
    /// <param name="keyId">The key identifier.</param>
    /// <param name="algorithm">The JWS algorithm.</param>
    /// <param name="hashAlgorithm">The hash algorithm of <paramref name="algorithm"/>.</param>
    private sealed class EcdsaSigner(EcdsaPrivateKey key, string? keyId, string algorithm, HashAlgorithmName hashAlgorithm) : IJwsSigner
    {
        /// <inheritdoc/>
        public string Algorithm => algorithm;

        /// <inheritdoc/>
        public string? KeyId => keyId;

        /// <inheritdoc/>
        public ValueTask<byte[]> SignAsync(ReadOnlyMemory<byte> signingInput, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            const DSASignatureFormat format = DSASignatureFormat.IeeeP1363FixedFieldConcatenation;
            return ValueTask.FromResult(key.SignData(signingInput.Span, hashAlgorithm, format));
        }
    }
}
