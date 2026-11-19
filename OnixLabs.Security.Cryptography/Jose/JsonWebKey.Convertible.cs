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

namespace OnixLabs.Security.Cryptography.Jose;

// ReSharper disable HeapView.ObjectAllocation.Evident
public sealed partial class JsonWebKey
{
    /// <summary>
    /// Creates an X25519 octet key pair JWK from the specified public key.
    /// </summary>
    /// <param name="key">The X25519 public key.</param>
    /// <param name="keyId">The key identifier, or <see langword="null"/>.</param>
    /// <returns>Returns a new <see cref="JsonWebKey"/> with <c>"kty": "OKP"</c> and <c>"crv": "X25519"</c>.</returns>
    public static JsonWebKey Create(XdhPublicKey key, string? keyId = null) =>
        new(OctetKeyPair, "X25519", key.Export(), null, null, null, default, keyId);

    /// <summary>
    /// Creates an X25519 octet key pair JWK, including its private part, from the specified private key.
    /// </summary>
    /// <param name="key">The X25519 private key.</param>
    /// <param name="keyId">The key identifier, or <see langword="null"/>.</param>
    /// <returns>Returns a new <see cref="JsonWebKey"/> with <c>"kty": "OKP"</c> and <c>"crv": "X25519"</c>.</returns>
    public static JsonWebKey Create(XdhPrivateKey key, string? keyId = null) =>
        CreateOctetKeyPair("X25519", key.Export(), key.GetPublicKey().Export(), keyId);

    /// <summary>
    /// Creates an Ed25519 octet key pair JWK from the specified public key.
    /// </summary>
    /// <param name="key">The Ed25519 public key.</param>
    /// <param name="keyId">The key identifier, or <see langword="null"/>.</param>
    /// <returns>Returns a new <see cref="JsonWebKey"/> with <c>"kty": "OKP"</c> and <c>"crv": "Ed25519"</c>.</returns>
    public static JsonWebKey Create(EddsaPublicKey key, string? keyId = null) =>
        new(OctetKeyPair, "Ed25519", key.Export(), null, null, null, default, keyId);

    /// <summary>
    /// Creates an Ed25519 octet key pair JWK, including its private part (the seed), from the specified private key.
    /// </summary>
    /// <param name="key">The Ed25519 private key.</param>
    /// <param name="keyId">The key identifier, or <see langword="null"/>.</param>
    /// <returns>Returns a new <see cref="JsonWebKey"/> with <c>"kty": "OKP"</c> and <c>"crv": "Ed25519"</c>.</returns>
    public static JsonWebKey Create(EddsaPrivateKey key, string? keyId = null) =>
        CreateOctetKeyPair("Ed25519", key.Export(), key.GetPublicKey().Export(), keyId);

    /// <summary>
    /// Creates an elliptic curve JWK from the specified EC Diffie-Hellman public key.
    /// </summary>
    /// <param name="key">The public key, on P-256, P-384 or P-521.</param>
    /// <param name="keyId">The key identifier, or <see langword="null"/>.</param>
    /// <returns>Returns a new <see cref="JsonWebKey"/> with <c>"kty": "EC"</c>.</returns>
    /// <exception cref="CryptographicException">Thrown when the key is not on a supported curve.</exception>
    public static JsonWebKey Create(EcdhPublicKey key, string? keyId = null) => CreateEllipticCurve(key.Export(), keyId);

    /// <summary>
    /// Creates an elliptic curve JWK from the specified ECDSA public key.
    /// </summary>
    /// <param name="key">The public key, on P-256, P-384, P-521 or secp256k1.</param>
    /// <param name="keyId">The key identifier, or <see langword="null"/>.</param>
    /// <returns>Returns a new <see cref="JsonWebKey"/> with <c>"kty": "EC"</c>.</returns>
    /// <exception cref="CryptographicException">Thrown when the key is not on a supported curve.</exception>
    public static JsonWebKey Create(EcdsaPublicKey key, string? keyId = null) => CreateEllipticCurve(key.Export(), keyId);

    /// <summary>
    /// Creates an elliptic curve JWK, including its private part, from the specified EC Diffie-Hellman private key.
    /// </summary>
    /// <param name="key">The private key, on P-256, P-384 or P-521.</param>
    /// <param name="keyId">The key identifier, or <see langword="null"/>.</param>
    /// <returns>Returns a new <see cref="JsonWebKey"/> with <c>"kty": "EC"</c>.</returns>
    /// <exception cref="CryptographicException">Thrown when the key is not on a supported curve.</exception>
    public static JsonWebKey Create(EcdhPrivateKey key, string? keyId = null) => CreateEllipticCurvePrivate(key.Export(), keyId);

    /// <summary>
    /// Creates an elliptic curve JWK, including its private part, from the specified ECDSA private key.
    /// </summary>
    /// <param name="key">The private key, on P-256, P-384 or P-521.</param>
    /// <param name="keyId">The key identifier, or <see langword="null"/>.</param>
    /// <returns>Returns a new <see cref="JsonWebKey"/> with <c>"kty": "EC"</c>.</returns>
    /// <exception cref="CryptographicException">Thrown when the key is not on a supported curve.</exception>
    public static JsonWebKey Create(EcdsaPrivateKey key, string? keyId = null) => CreateEllipticCurvePrivate(key.Export(), keyId);

    /// <summary>
    /// Converts the current key to an X25519 public key.
    /// </summary>
    /// <returns>Returns a new <see cref="XdhPublicKey"/>.</returns>
    /// <exception cref="CryptographicException">Thrown when the current key is not an X25519 octet key pair.</exception>
    public XdhPublicKey ToXdhPublicKey()
    {
        RequireKind(OctetKeyPair, "X25519", requirePrivateKey: false);
        return XdhPublicKey.Import(x);
    }

    /// <summary>
    /// Converts the current key to an X25519 private key, after checking that its public part matches.
    /// </summary>
    /// <returns>Returns a new <see cref="XdhPrivateKey"/>.</returns>
    /// <exception cref="CryptographicException">
    /// Thrown when the current key is not an X25519 octet key pair with a private part, or when <c>x</c> is not the public key of <c>d</c>.
    /// </exception>
    public XdhPrivateKey ToXdhPrivateKey()
    {
        RequireKind(OctetKeyPair, "X25519", requirePrivateKey: true);
        XdhPrivateKey key = WithPrivateKeyBytes(bytes => XdhPrivateKey.Import(bytes));
        RequireMatchingPublicKey(key.GetPublicKey().Export(), x);
        return key;
    }

    /// <summary>
    /// Converts the current key to an Ed25519 public key.
    /// </summary>
    /// <returns>Returns a new <see cref="EddsaPublicKey"/>.</returns>
    /// <exception cref="CryptographicException">Thrown when the current key is not an Ed25519 octet key pair.</exception>
    public EddsaPublicKey ToEddsaPublicKey()
    {
        RequireKind(OctetKeyPair, "Ed25519", requirePrivateKey: false);
        return EddsaPublicKey.Import(x);
    }

    /// <summary>
    /// Converts the current key to an Ed25519 private key, after checking that its public part matches.
    /// </summary>
    /// <returns>Returns a new <see cref="EddsaPrivateKey"/>.</returns>
    /// <exception cref="CryptographicException">
    /// Thrown when the current key is not an Ed25519 octet key pair with a private part, or when <c>x</c> is not the public key of
    /// <c>d</c>.
    /// </exception>
    public EddsaPrivateKey ToEddsaPrivateKey()
    {
        RequireKind(OctetKeyPair, "Ed25519", requirePrivateKey: true);
        EddsaPrivateKey key = WithPrivateKeyBytes(bytes => EddsaPrivateKey.Import(bytes));
        RequireMatchingPublicKey(key.GetPublicKey().Export(), x);
        return key;
    }

    /// <summary>
    /// Converts the current key to an EC Diffie-Hellman public key.
    /// </summary>
    /// <returns>Returns a new <see cref="EcdhPublicKey"/>, whose data is a SubjectPublicKeyInfo.</returns>
    /// <exception cref="CryptographicException">Thrown when the current key is not an elliptic curve key, or is not a valid
    /// point.</exception>
    public EcdhPublicKey ToEcdhPublicKey()
    {
        RequireKind(EllipticCurve, null, requirePrivateKey: false);
        return EcdhPublicKey.Import(EcKeyEncoding.EncodeSubjectPublicKeyInfo(Curve!, x, y));
    }

    /// <summary>
    /// Converts the current key to an ECDSA public key. A secp256k1 key is supported on every platform.
    /// </summary>
    /// <returns>Returns a new <see cref="EcdsaPublicKey"/>, whose data is a SubjectPublicKeyInfo.</returns>
    /// <exception cref="CryptographicException">Thrown when the current key is not an elliptic curve key, or is not a valid
    /// point.</exception>
    public EcdsaPublicKey ToEcdsaPublicKey()
    {
        RequireKind(EllipticCurve, null, requirePrivateKey: false);
        return EcdsaPublicKey.Import(EcKeyEncoding.EncodeSubjectPublicKeyInfo(Curve!, x, y));
    }

    /// <summary>
    /// Converts the current key to an EC Diffie-Hellman private key, after checking that its public part matches.
    /// </summary>
    /// <returns>Returns a new <see cref="EcdhPrivateKey"/>.</returns>
    /// <exception cref="CryptographicException">
    /// Thrown when the current key is not an elliptic curve key with a private part, the platform does not support its curve, or the public
    /// point is not that of <c>d</c>.
    /// </exception>
    public EcdhPrivateKey ToEcdhPrivateKey()
    {
        RequireKind(EllipticCurve, null, requirePrivateKey: true);
        EcdhPrivateKey key = WithECPrivateKey(bytes => EcdhPrivateKey.Import(bytes));
        RequireMatchingPublicKey(key.GetPublicKey().Export(), EcKeyEncoding.EncodeSubjectPublicKeyInfo(Curve!, x, y));
        return key;
    }

    /// <summary>
    /// Converts the current key to an ECDSA private key, after checking that its public part matches.
    /// </summary>
    /// <returns>Returns a new <see cref="EcdsaPrivateKey"/>.</returns>
    /// <exception cref="CryptographicException">
    /// Thrown when the current key is not an elliptic curve key with a private part, the platform does not support its curve, or the public
    /// point is not that of <c>d</c>.
    /// </exception>
    public EcdsaPrivateKey ToEcdsaPrivateKey()
    {
        RequireKind(EllipticCurve, null, requirePrivateKey: true);
        EcdsaPrivateKey key = WithECPrivateKey(bytes => EcdsaPrivateKey.Import(bytes));
        RequireMatchingPublicKey(key.GetPublicKey().Export(), EcKeyEncoding.EncodeSubjectPublicKeyInfo(Curve!, x, y));
        return key;
    }

    /// <summary>
    /// Creates an octet key pair JWK with a private part, zeroing the specified private key bytes afterwards.
    /// </summary>
    /// <param name="curve">The curve name.</param>
    /// <param name="privateKey">The 32-byte private key, which is zeroed.</param>
    /// <param name="publicKey">The 32-byte public key.</param>
    /// <param name="keyId">The key identifier.</param>
    /// <returns>Returns a new <see cref="JsonWebKey"/>.</returns>
    private static JsonWebKey CreateOctetKeyPair(string curve, byte[] privateKey, byte[] publicKey, string? keyId)
    {
        try
        {
            return new JsonWebKey(OctetKeyPair, curve, publicKey, null, null, null, privateKey, keyId);
        }
        finally
        {
            CryptographicOperations.ZeroMemory(privateKey);
        }
    }

    /// <summary>
    /// Creates an elliptic curve JWK from a SubjectPublicKeyInfo.
    /// </summary>
    /// <param name="subjectPublicKeyInfo">The DER-encoded SubjectPublicKeyInfo.</param>
    /// <param name="keyId">The key identifier.</param>
    /// <returns>Returns a new <see cref="JsonWebKey"/>.</returns>
    private static JsonWebKey CreateEllipticCurve(byte[] subjectPublicKeyInfo, string? keyId)
    {
        (string curve, byte[] x, byte[] y) = EcKeyEncoding.DecodeSubjectPublicKeyInfo(subjectPublicKeyInfo);
        return new JsonWebKey(EllipticCurve, curve, x, y, null, null, default, keyId);
    }

    /// <summary>
    /// Creates an elliptic curve JWK with a private part from an ECPrivateKey, zeroing the decoded material afterwards.
    /// </summary>
    /// <param name="ecPrivateKey">The DER-encoded ECPrivateKey, which is zeroed.</param>
    /// <param name="keyId">The key identifier.</param>
    /// <returns>Returns a new <see cref="JsonWebKey"/>.</returns>
    private static JsonWebKey CreateEllipticCurvePrivate(byte[] ecPrivateKey, string? keyId)
    {
        byte[]? d = null;

        try
        {
            (string curve, d, byte[] x, byte[] y) = EcKeyEncoding.DecodeECPrivateKey(ecPrivateKey);
            return new JsonWebKey(EllipticCurve, curve, x, y, null, null, d, keyId);
        }
        finally
        {
            CryptographicOperations.ZeroMemory(ecPrivateKey);
            if (d is not null) CryptographicOperations.ZeroMemory(d);
        }
    }

    /// <summary>
    /// Checks that the current key has the specified type and curve, and a private part if one is required.
    /// </summary>
    /// <param name="keyType">The required <c>kty</c>.</param>
    /// <param name="curve">The required <c>crv</c>, or <see langword="null"/> to accept any supported curve.</param>
    /// <param name="requirePrivateKey"><see langword="true"/> if the key must have a private part.</param>
    /// <exception cref="CryptographicException">Thrown when the key does not meet the requirement.</exception>
    private void RequireKind(string keyType, string? curve, bool requirePrivateKey)
    {
        if (KeyType != keyType || curve is not null && Curve != curve)
            throw new CryptographicException($"The JSON Web Key is not a {curve ?? keyType} key (it is {KeyType} {Curve}).");

        if (requirePrivateKey && d is null)
            throw new CryptographicException("The JSON Web Key has no private part.");
    }

    /// <summary>
    /// Passes the decrypted private part to the specified function, zeroing it afterwards.
    /// </summary>
    /// <typeparam name="T">The type of private key the function creates.</typeparam>
    /// <param name="import">The function that creates the private key from the raw private part.</param>
    /// <returns>Returns the private key created by <paramref name="import"/>.</returns>
    private T WithPrivateKeyBytes<T>(Func<byte[], T> import)
    {
        byte[] privateKey = new byte[d!.Value.Length];

        try
        {
            d.Value.CopyTo(privateKey);
            return import(privateKey);
        }
        finally
        {
            CryptographicOperations.ZeroMemory(privateKey);
        }
    }

    /// <summary>
    /// Passes the current key, encoded as an ECPrivateKey, to the specified function, zeroing the encoding afterwards.
    /// </summary>
    /// <typeparam name="T">The type of private key the function creates.</typeparam>
    /// <param name="import">The function that imports the ECPrivateKey.</param>
    /// <returns>Returns the private key created by <paramref name="import"/>.</returns>
    private T WithECPrivateKey<T>(Func<byte[], T> import)
    {
        byte[]? encoded = null;

        try
        {
            encoded = WithPrivateKeyBytes(privateKey => EcKeyEncoding.EncodeECPrivateKey(Curve!, privateKey, x, y));
            return import(encoded);
        }
        finally
        {
            if (encoded is not null) CryptographicOperations.ZeroMemory(encoded);
        }
    }

    /// <summary>
    /// Checks that a public key derived from the private part equals the public key in the JWK.
    /// </summary>
    /// <param name="derived">The public key derived from the private part.</param>
    /// <param name="expected">The public key from the JWK, in the same encoding.</param>
    /// <exception cref="CryptographicException">Thrown when the public keys differ.</exception>
    /// <remarks>
    /// Why: a JWK whose public part does not belong to its private part is malformed, and signing with such a pair can be dangerous;
    /// Ed25519
    /// signatures made under a mismatched public key can reveal the private key.
    /// </remarks>
    private static void RequireMatchingPublicKey(ReadOnlySpan<byte> derived, ReadOnlySpan<byte> expected)
    {
        if (!derived.SequenceEqual(expected))
            throw new CryptographicException("The public part of the JSON Web Key does not match its private part.");
    }
}
