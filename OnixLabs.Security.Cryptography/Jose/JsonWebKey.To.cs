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
using System.Text.Json.Nodes;

namespace OnixLabs.Security.Cryptography.Jose;

public sealed partial class JsonWebKey
{
    /// <summary>
    /// Returns the JSON representation of the current key.
    /// </summary>
    /// <param name="includePrivateKey">
    /// <see langword="true"/> to include the private part, <c>d</c>, if the key has one; otherwise, <see langword="false"/>, the default.
    /// </param>
    /// <returns>Returns the compact JSON representation, with members in the order <c>kty</c>, <c>crv</c>, <c>x</c>, <c>y</c>, <c>d</c>,
    /// <c>kid</c>.</returns>
    public string ToJson(bool includePrivateKey = false) => JoseJson.Write(ToJsonObject(includePrivateKey));

    /// <inheritdoc/>
    public override string ToString() => ToJson();

    /// <summary>
    /// Computes the JWK SHA-256 Thumbprint of the current key, as RFC 7638 §3 defines it.
    /// </summary>
    /// <returns>Returns the 32-byte SHA-256 thumbprint; its base64url encoding is often used as a <c>kid</c>.</returns>
    /// <remarks>
    /// The hash input is a JSON object of only the required public members, in lexicographic order and without whitespace: <c>crv</c>,
    /// <c>kty</c> and <c>x</c> for an octet key pair (RFC 8037 §2); <c>crv</c>, <c>kty</c>, <c>x</c> and <c>y</c> for an elliptic curve
    /// key; <c>e</c>, <c>kty</c> and <c>n</c> for an RSA key. The thumbprint of a private key is that of its public part (RFC 7638 §3.2.1).
    /// </remarks>
    public byte[] ComputeThumbprint()
    {
        JsonObject canonical = KeyType switch
        {
            OctetKeyPair => new JsonObject { ["crv"] = Curve, ["kty"] = KeyType, ["x"] = JoseJson.EncodeBytes(x) },
            EllipticCurve => new JsonObject
            {
                ["crv"] = Curve,
                ["kty"] = KeyType,
                ["x"] = JoseJson.EncodeBytes(x),
                ["y"] = JoseJson.EncodeBytes(y)
            },
            _ => new JsonObject { ["e"] = JoseJson.EncodeBytes(e), ["kty"] = KeyType, ["n"] = JoseJson.EncodeBytes(n) }
        };

        return SHA256.HashData(Encoding.UTF8.GetBytes(JoseJson.Write(canonical)));
    }

    /// <summary>
    /// Returns the JSON object representation of the current key.
    /// </summary>
    /// <param name="includePrivateKey"><see langword="true"/> to include the private part, if the key has one; otherwise,
    /// <see langword="false"/>.</param>
    /// <returns>Returns a new <see cref="JsonObject"/>.</returns>
    internal JsonObject ToJsonObject(bool includePrivateKey = false)
    {
        JsonObject result = new() { ["kty"] = KeyType };

        if (KeyType == Rsa)
        {
            result["n"] = JoseJson.EncodeBytes(n);
            result["e"] = JoseJson.EncodeBytes(e);
        }
        else
        {
            result["crv"] = Curve;
            result["x"] = JoseJson.EncodeBytes(x);
            if (y is not null) result["y"] = JoseJson.EncodeBytes(y);
        }

        if (includePrivateKey && d is not null)
        {
            Span<byte> privateKey = stackalloc byte[d.Value.Length];

            try
            {
                d.Value.CopyTo(privateKey);
                result["d"] = JoseJson.EncodeBytes(privateKey);
            }
            finally
            {
                CryptographicOperations.ZeroMemory(privateKey);
            }
        }

        if (KeyId is not null) result["kid"] = KeyId;

        return result;
    }
}
