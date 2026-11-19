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

namespace OnixLabs.Security.Cryptography.Jose;

/// <summary>
/// Represents a JSON Web Key (RFC 7517) of one of the types that DIDComm Messaging v2.1 uses: an octet key pair (<c>"kty": "OKP"</c>,
/// RFC 8037) on X25519 or Ed25519, or an elliptic curve key (<c>"kty": "EC"</c>, RFC 7518 §6.2) on P-256, P-384, P-521 or secp256k1
/// (RFC 8812 §3.1). An RSA public key (<c>"kty": "RSA"</c>) can also be read, for its thumbprint only.
/// </summary>
/// <remarks>
/// Instances are immutable. The private part, when present, is held as a <see cref="Secret"/>, which keeps it encrypted in memory.
/// Members that this type does not use, such as <c>use</c>, <c>key_ops</c> and <c>alg</c>, are ignored, as RFC 7517 §4 permits.
/// </remarks>
public sealed partial class JsonWebKey : IDisposable
{
    /// <summary>
    /// The <c>kty</c> value of an octet key pair (RFC 8037 §2).
    /// </summary>
    public const string OctetKeyPair = "OKP";

    /// <summary>
    /// The <c>kty</c> value of an elliptic curve key (RFC 7518 §6.1).
    /// </summary>
    public const string EllipticCurve = "EC";

    /// <summary>
    /// The <c>kty</c> value of an RSA key (RFC 7518 §6.1).
    /// </summary>
    public const string Rsa = "RSA";

    /// <summary>
    /// The x-coordinate (EC), or the public key (OKP), or <see langword="null"/> for an RSA key.
    /// </summary>
    private readonly byte[]? x;

    /// <summary>
    /// The y-coordinate (EC), or <see langword="null"/>.
    /// </summary>
    private readonly byte[]? y;

    /// <summary>
    /// The RSA modulus, or <see langword="null"/>.
    /// </summary>
    private readonly byte[]? n;

    /// <summary>
    /// The RSA public exponent, or <see langword="null"/>.
    /// </summary>
    private readonly byte[]? e;

    /// <summary>
    /// The private part <c>d</c>, encrypted in memory, or <see langword="null"/> for a public key.
    /// </summary>
    private readonly Secret? d;

    /// <summary>
    /// Initializes a new instance of the <see cref="JsonWebKey"/> class from members that have already been validated.
    /// </summary>
    /// <param name="keyType">The <c>kty</c> value.</param>
    /// <param name="curve">The <c>crv</c> value, or <see langword="null"/> for an RSA key.</param>
    /// <param name="x">The <c>x</c> value, or <see langword="null"/>.</param>
    /// <param name="y">The <c>y</c> value, or <see langword="null"/>.</param>
    /// <param name="n">The <c>n</c> value, or <see langword="null"/>.</param>
    /// <param name="e">The <c>e</c> value, or <see langword="null"/>.</param>
    /// <param name="d">The <c>d</c> value, or <see langword="null"/> for a public key; it is copied into a <see cref="Secret"/>.</param>
    /// <param name="keyId">The <c>kid</c> value, or <see langword="null"/>.</param>
    private JsonWebKey(string keyType, string? curve, byte[]? x, byte[]? y, byte[]? n, byte[]? e, ReadOnlySpan<byte> d, string? keyId)
    {
        KeyType = keyType;
        Curve = curve;
        KeyId = keyId;
        this.x = x;
        this.y = y;
        this.n = n;
        this.e = e;
        this.d = d.IsEmpty ? null : new Secret(d);
    }

    /// <summary>
    /// Gets the key type, the <c>kty</c> member: <see cref="OctetKeyPair"/>, <see cref="EllipticCurve"/> or <see cref="Rsa"/>.
    /// </summary>
    /// <value>The key type.</value>
    public string KeyType { get; }

    /// <summary>
    /// Gets the curve, the <c>crv</c> member: <c>X25519</c>, <c>Ed25519</c>, <c>P-256</c>, <c>P-384</c>, <c>P-521</c> or <c>secp256k1</c>.
    /// </summary>
    /// <value>The curve, or <see langword="null"/> for an RSA key.</value>
    public string? Curve { get; }

    /// <summary>
    /// Gets the key identifier, the <c>kid</c> member.
    /// </summary>
    /// <value>The key identifier, or <see langword="null"/> if the key has none.</value>
    public string? KeyId { get; }

    /// <summary>
    /// Gets a value indicating whether the current key includes its private part, the <c>d</c> member.
    /// </summary>
    /// <value><see langword="true"/> if the current key is a private key; otherwise, <see langword="false"/>.</value>
    public bool HasPrivateKey => d is not null;

    /// <summary>
    /// Gets the public key bytes of an octet key pair, or the x-coordinate of an elliptic curve key.
    /// </summary>
    /// <value>The <c>x</c> member, decoded, or empty for an RSA key.</value>
    public ReadOnlySpan<byte> X => x;

    /// <summary>
    /// Gets the y-coordinate of an elliptic curve key.
    /// </summary>
    /// <value>The <c>y</c> member, decoded, or empty for other key types.</value>
    public ReadOnlySpan<byte> Y => y;

    /// <summary>
    /// Returns a copy of the current key with the specified key identifier.
    /// </summary>
    /// <param name="keyId">The key identifier, or <see langword="null"/> to remove it.</param>
    /// <returns>Returns a new <see cref="JsonWebKey"/> that differs from the current key only in its <c>kid</c>.</returns>
    public JsonWebKey WithKeyId(string? keyId) => WithPrivatePart(keyId, includePrivateKey: true);

    /// <summary>
    /// Returns the public part of the current key, without <c>d</c>.
    /// </summary>
    /// <returns>Returns a new <see cref="JsonWebKey"/> holding only the public part of the current key.</returns>
    public JsonWebKey ToPublicKey() => WithPrivatePart(KeyId, includePrivateKey: false);

    /// <summary>
    /// Disposes the encrypted private part of the current key, if it has one.
    /// </summary>
    public void Dispose() => d?.Dispose();

    /// <summary>
    /// Copies the decrypted private part into the specified destination.
    /// </summary>
    /// <param name="destination">The buffer that receives <c>d</c>, which must be at least <see cref="GetPrivateKeyLength"/> long.</param>
    /// <exception cref="InvalidOperationException">Thrown when the current key has no private part.</exception>
    internal void CopyPrivateKeyTo(Span<byte> destination)
    {
        Secret secret = d ?? throw new InvalidOperationException("The JSON Web Key has no private part.");
        secret.CopyTo(destination);
    }

    /// <summary>
    /// Gets the length of the private part.
    /// </summary>
    /// <returns>Returns the length of <c>d</c> in bytes, or zero for a public key.</returns>
    internal int GetPrivateKeyLength() => d?.Length ?? 0;

    /// <summary>
    /// Returns a copy of the current key with the specified key identifier, with or without its private part.
    /// </summary>
    /// <param name="keyId">The key identifier of the copy.</param>
    /// <param name="includePrivateKey"><see langword="true"/> to keep the private part; otherwise, <see langword="false"/>.</param>
    /// <returns>Returns a new <see cref="JsonWebKey"/>.</returns>
    private JsonWebKey WithPrivatePart(string? keyId, bool includePrivateKey)
    {
        if (!includePrivateKey || d is null) return new JsonWebKey(KeyType, Curve, x, y, n, e, default, keyId);

        Span<byte> privateKey = stackalloc byte[d.Value.Length];

        try
        {
            d.Value.CopyTo(privateKey);
            return new JsonWebKey(KeyType, Curve, x, y, n, e, privateKey, keyId);
        }
        finally
        {
            System.Security.Cryptography.CryptographicOperations.ZeroMemory(privateKey);
        }
    }
}
