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
using System.Text;
using System.Text.Json;

namespace OnixLabs.Security.Cryptography.Jose;

/// <summary>
/// Represents one signature of a JSON Web Signature, with its protected and unprotected headers (RFC 7515 §7.2.1).
/// </summary>
public sealed class JwsSignature
{
    /// <summary>
    /// The base64url-encoded payload of the JWS this signature belongs to.
    /// </summary>
    private readonly string encodedPayload;

    /// <summary>
    /// The decoded signature.
    /// </summary>
    private readonly byte[] signature;

    /// <summary>
    /// Initializes a new instance of the <see cref="JwsSignature"/> class.
    /// </summary>
    /// <param name="encodedPayload">The base64url-encoded payload of the JWS.</param>
    /// <param name="encodedProtectedHeader">The base64url-encoded protected header, exactly as signed.</param>
    /// <param name="protectedHeader">The decoded protected header.</param>
    /// <param name="header">The unprotected header, or <see langword="null"/>.</param>
    /// <param name="signature">The decoded signature.</param>
    /// <exception cref="CryptographicException">Thrown when the headers share a member, or neither has an <c>alg</c>.</exception>
    internal JwsSignature(
        string encodedPayload,
        string encodedProtectedHeader,
        JsonElement protectedHeader,
        JsonElement? header,
        byte[] signature)
    {
        JoseJson.RequireDisjoint(protectedHeader, header);

        this.encodedPayload = encodedPayload;
        this.signature = signature;
        EncodedProtectedHeader = encodedProtectedHeader;
        ProtectedHeader = protectedHeader;
        Header = header;
        Algorithm = JoseJson.FindString("alg", protectedHeader, header)
            ?? throw new CryptographicException("The JWS signature has no 'alg'.");
        KeyId = JoseJson.FindString("kid", protectedHeader, header);
    }

    /// <summary>
    /// Gets the base64url-encoded protected header, exactly as it was signed.
    /// </summary>
    /// <value>The base64url-encoded protected header.</value>
    public string EncodedProtectedHeader { get; }

    /// <summary>
    /// Gets the decoded protected header.
    /// </summary>
    /// <value>The protected header as a JSON object.</value>
    public JsonElement ProtectedHeader { get; }

    /// <summary>
    /// Gets the unprotected header, which is not covered by the signature.
    /// </summary>
    /// <value>The unprotected header as a JSON object, or <see langword="null"/> if there is none.</value>
    public JsonElement? Header { get; }

    /// <summary>
    /// Gets the signature algorithm, from <c>alg</c> in the protected or unprotected header.
    /// </summary>
    /// <value>The JWS <c>alg</c> value.</value>
    public string Algorithm { get; }

    /// <summary>
    /// Gets the signing key identifier, from <c>kid</c> in the protected or unprotected header.
    /// </summary>
    /// <value>The key identifier, or <see langword="null"/> if there is none.</value>
    public string? KeyId { get; }

    /// <summary>
    /// Gets the raw signature.
    /// </summary>
    /// <value>The decoded signature bytes.</value>
    public ReadOnlySpan<byte> Signature => signature;

    /// <summary>
    /// Determines whether the current signature is valid for its payload under the specified public key.
    /// </summary>
    /// <param name="publicKey">
    /// The public key: an Ed25519 key for <see cref="JwsAlgorithm.EdDsa"/>, or an elliptic curve key on the curve that the ECDSA algorithm
    /// requires. Its <c>kid</c>, if any, is not compared; select the signature by <see cref="KeyId"/> before verifying.
    /// </param>
    /// <returns>
    /// Returns <see langword="true"/> if the signature is valid; otherwise, <see langword="false"/>, including when the algorithm is not
    /// supported, the key does not suit the algorithm, or the protected header has a <c>crit</c> member.
    /// </returns>
    /// <remarks>
    /// Why: the key must match the algorithm, rather than the algorithm being taken on trust from the header, so that an attacker cannot
    /// choose how a key is used. Signatures with a <c>crit</c> member are rejected because no extensions are understood (RFC 7515 §4.1.11).
    /// </remarks>
    public bool Verify(JsonWebKey publicKey)
    {
        ArgumentNullException.ThrowIfNull(publicKey);

        if (ProtectedHeader.TryGetProperty("crit", out _) || Header is { } header && header.TryGetProperty("crit", out _)) return false;

        byte[] signingInput = Encoding.ASCII.GetBytes($"{EncodedProtectedHeader}.{encodedPayload}");

        if (Algorithm == JwsAlgorithm.EdDsa)
        {
            return publicKey is { KeyType: JsonWebKey.OctetKeyPair, Curve: "Ed25519" }
                   && publicKey.ToEddsaPublicKey().IsDataValid(signature, signingInput);
        }

        if (!JwsAlgorithm.TryGetEcdsaParameters(Algorithm, out string curve, out HashAlgorithmName hashAlgorithm)) return false;
        if (publicKey.KeyType != JsonWebKey.EllipticCurve || publicKey.Curve != curve) return false;
        if (signature.Length != 2 * EcKeyEncoding.GetFieldLength(curve)) return false;

        const DSASignatureFormat format = DSASignatureFormat.IeeeP1363FixedFieldConcatenation;
        return publicKey.ToEcdsaPublicKey().IsDataValid(signature, signingInput, hashAlgorithm, format);
    }
}
