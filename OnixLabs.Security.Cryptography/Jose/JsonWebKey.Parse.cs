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
using System.Text.Json;

namespace OnixLabs.Security.Cryptography.Jose;

public sealed partial class JsonWebKey
{
    /// <summary>
    /// Parses a JSON Web Key from its JSON representation.
    /// </summary>
    /// <param name="json">The JSON representation of the key.</param>
    /// <returns>Returns a new <see cref="JsonWebKey"/>.</returns>
    /// <exception cref="CryptographicException">
    /// Thrown when the JSON is malformed, or the key's type or curve is unsupported, or a member is missing or of the wrong length.
    /// </exception>
    public static JsonWebKey Parse(string json) => FromJson(JoseJson.ParseObject(json, "JSON Web Key"));

    /// <summary>
    /// Reads a JSON Web Key from a parsed JSON object.
    /// </summary>
    /// <param name="element">The JSON object representing the key.</param>
    /// <returns>Returns a new <see cref="JsonWebKey"/>.</returns>
    /// <exception cref="CryptographicException">
    /// Thrown when the element is not an object, or the key's type or curve is unsupported, or a member is missing or of the wrong length.
    /// </exception>
    public static JsonWebKey FromJson(JsonElement element)
    {
        if (element.ValueKind != JsonValueKind.Object) throw new CryptographicException("The JSON Web Key is not a JSON object.");

        string keyType = JoseJson.GetRequiredString(element, "kty");
        string? keyId = JoseJson.GetOptionalString(element, "kid");

        return keyType switch
        {
            OctetKeyPair => ReadOctetKeyPair(element, keyId),
            EllipticCurve => ReadEllipticCurve(element, keyId),
            Rsa => ReadRsa(element, keyId),
            _ => throw new CryptographicException($"The JSON Web Key type '{keyType}' is not supported.")
        };
    }

    /// <summary>
    /// Reads an RSA public key, for its thumbprint only; any private members are ignored.
    /// </summary>
    /// <param name="element">The JSON object representing the key.</param>
    /// <param name="keyId">The key identifier.</param>
    /// <returns>Returns a new <see cref="JsonWebKey"/>.</returns>
    /// <exception cref="CryptographicException">Thrown when <c>n</c> or <c>e</c> is missing.</exception>
    private static JsonWebKey ReadRsa(JsonElement element, string? keyId) =>
        new(Rsa, null, null, null, JoseJson.GetRequiredBytes(element, "n"), JoseJson.GetRequiredBytes(element, "e"), default, keyId);

    /// <summary>
    /// Reads an octet key pair (RFC 8037 §2) on X25519 or Ed25519, whose <c>x</c> and <c>d</c> are 32 bytes.
    /// </summary>
    /// <param name="element">The JSON object representing the key.</param>
    /// <param name="keyId">The key identifier.</param>
    /// <returns>Returns a new <see cref="JsonWebKey"/>.</returns>
    /// <exception cref="CryptographicException">Thrown when the curve is unsupported or a member has the wrong length.</exception>
    private static JsonWebKey ReadOctetKeyPair(JsonElement element, string? keyId)
    {
        string curve = JoseJson.GetRequiredString(element, "crv");

        if (curve is not ("X25519" or "Ed25519")) throw new CryptographicException($"The octet key pair curve '{curve}' is not supported.");

        byte[] x = JoseJson.GetRequiredBytes(element, "x");
        byte[]? d = JoseJson.GetOptionalBytes(element, "d");

        try
        {
            if (x.Length != 32 || d is { Length: not 32 }) throw new CryptographicException($"The {curve} key members must be 32 bytes.");

            return new JsonWebKey(OctetKeyPair, curve, x, null, null, null, d, keyId);
        }
        finally
        {
            if (d is not null) CryptographicOperations.ZeroMemory(d);
        }
    }

    /// <summary>
    /// Reads an elliptic curve key (RFC 7518 §6.2), whose <c>x</c>, <c>y</c> and <c>d</c> must each be the full field length of the curve,
    /// as RFC 7518 §6.2.1.2, §6.2.1.3 and §6.2.2.1 require.
    /// </summary>
    /// <param name="element">The JSON object representing the key.</param>
    /// <param name="keyId">The key identifier.</param>
    /// <returns>Returns a new <see cref="JsonWebKey"/>.</returns>
    /// <exception cref="CryptographicException">Thrown when the curve is unsupported or a member has the wrong length.</exception>
    private static JsonWebKey ReadEllipticCurve(JsonElement element, string? keyId)
    {
        string curve = JoseJson.GetRequiredString(element, "crv");
        int length = EcKeyEncoding.GetFieldLength(curve);
        byte[] x = JoseJson.GetRequiredBytes(element, "x");
        byte[] y = JoseJson.GetRequiredBytes(element, "y");
        byte[]? d = JoseJson.GetOptionalBytes(element, "d");

        try
        {
            if (x.Length != length || y.Length != length || d is not null && d.Length != length)
                throw new CryptographicException($"The {curve} key members must each be {length} bytes.");

            return new JsonWebKey(EllipticCurve, curve, x, y, null, null, d, keyId);
        }
        finally
        {
            if (d is not null) CryptographicOperations.ZeroMemory(d);
        }
    }
}
