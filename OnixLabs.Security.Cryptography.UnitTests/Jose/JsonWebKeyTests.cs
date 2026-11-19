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
using OnixLabs.Security.Cryptography.Jose;
using OnixLabs.Security.Cryptography.UnitTests.Data.Jose;

namespace OnixLabs.Security.Cryptography.UnitTests.Jose;

public sealed class JsonWebKeyTests
{
    /// <summary>
    /// Verifies the JWK SHA-256 Thumbprint example of RFC 7638 §3.1, an RSA public key.
    /// </summary>
    [Fact(DisplayName = "JsonWebKey.ComputeThumbprint must satisfy the RFC 7638 §3.1 example")]
    public void ComputeThumbprintMustSatisfyRfc7638Example()
    {
        // Given
        JsonElement vector = JoseTestVectors.Load("rfc7638_3_1_thumbprint.json");
        JsonWebKey key = JsonWebKey.Parse(vector.GetJson("key"));

        // When
        byte[] thumbprint = key.ComputeThumbprint();

        // Then
        Assert.Equal(vector.GetText("thumbprint"), Base64Url.Encode(thumbprint));
        Assert.Equal("2011-04-29", key.KeyId);
    }

    /// <summary>
    /// Verifies the Ed25519 JWK Thumbprint of RFC 8037 Appendix A.3, computed from the private key of A.1, whose thumbprint is that of its
    /// public part.
    /// </summary>
    [Fact(DisplayName = "JsonWebKey.ComputeThumbprint must satisfy the RFC 8037 Appendix A.3 example")]
    public void ComputeThumbprintMustSatisfyRfc8037Example()
    {
        // Given
        JsonElement vector = JoseTestVectors.Load("rfc8037_appendix_a.json");
        using JsonWebKey privateKey = JsonWebKey.Parse(vector.GetJson("ed25519PrivateKey"));

        // When
        byte[] fromPrivate = privateKey.ComputeThumbprint();
        byte[] fromPublic = privateKey.ToPublicKey().ComputeThumbprint();

        // Then
        Assert.Equal(vector.GetText("thumbprint"), Base64Url.Encode(fromPrivate));
        Assert.Equal(fromPrivate, fromPublic);
    }

    /// <summary>
    /// Verifies that the Ed25519 private key of RFC 8037 Appendix A.1 converts to a key whose derived public key is its <c>x</c>.
    /// </summary>
    [Fact(DisplayName = "JsonWebKey must convert the RFC 8037 Appendix A.1 Ed25519 key, and derive its public key")]
    public void JsonWebKeyMustConvertRfc8037Ed25519Key()
    {
        // Given
        JsonElement vector = JoseTestVectors.Load("rfc8037_appendix_a.json");
        using JsonWebKey jwk = JsonWebKey.Parse(vector.GetJson("ed25519PrivateKey"));

        // When
        EddsaPrivateKey privateKey = jwk.ToEddsaPrivateKey();
        using JsonWebKey roundTrip = JsonWebKey.Create(privateKey);

        // Then
        Assert.Equal(jwk.X.ToArray(), privateKey.GetPublicKey().Export());
        Assert.Equal(JsonDocument.Parse(vector.GetJson("ed25519PrivateKey")).RootElement.GetProperty("d").GetString(),
            JsonDocument.Parse(roundTrip.ToJson(includePrivateKey: true)).RootElement.GetProperty("d").GetString());
    }

    /// <summary>
    /// Verifies that every private key of DIDComm Messaging v2.1 Appendix A converts to the library's key types, with its public part
    /// matching, except secp256k1, whose private key the library does not use; that one converts as a public key.
    /// </summary>
    [Fact(DisplayName = "JsonWebKey must convert every DIDComm v2.1 Appendix A secret, and its public part must match")]
    public void JsonWebKeyMustConvertDidCommSecrets()
    {
        // Given
        JsonElement vectors = JoseTestVectors.Load("didcomm_v2_1_appendix.json");
        int converted = 0;

        foreach (string section in (string[])["senderSecrets", "recipientSecrets"])
        foreach (JsonElement secret in vectors.GetProperty(section).EnumerateArray())
        {
            // When
            using JsonWebKey jwk = JsonWebKey.Parse(secret.GetRawText());
            using JsonWebKey publicKey = jwk.ToPublicKey();

            JsonWebKey roundTrip = jwk.Curve switch
            {
                "X25519" => JsonWebKey.Create(jwk.ToXdhPrivateKey()),
                "Ed25519" => JsonWebKey.Create(jwk.ToEddsaPrivateKey()),
                "secp256k1" => JsonWebKey.Create(jwk.ToEcdsaPublicKey()),
                _ => JsonWebKey.Create(jwk.ToEcdhPrivateKey())
            };

            // Then
            using (roundTrip)
            {
                Assert.Equal(jwk.ComputeThumbprint(), roundTrip.ComputeThumbprint());
                Assert.False(publicKey.HasPrivateKey);
                Assert.Equal(jwk.Curve != "secp256k1", roundTrip.HasPrivateKey);
                if (roundTrip.HasPrivateKey)
                    Assert.Equal(jwk.ToJson(includePrivateKey: true), roundTrip.WithKeyId(jwk.KeyId).ToJson(includePrivateKey: true));
            }

            converted++;
        }

        Assert.Equal(15, converted);
    }

    [Fact(DisplayName = "JsonWebKey should round-trip generated keys of every supported type through JSON and the library's key types")]
    public void JsonWebKeyShouldRoundTripGeneratedKeys()
    {
        // Given
        JsonWebKey[] keys =
        [
            JsonWebKey.Create(XdhPrivateKey.Create(), "x25519"),
            JsonWebKey.Create(EddsaPrivateKey.Create(), "ed25519"),
            JsonWebKey.Create(EcdhPrivateKey.Create(ECCurve.NamedCurves.nistP256), "p256"),
            JsonWebKey.Create(EcdhPrivateKey.Create(ECCurve.NamedCurves.nistP384), "p384"),
            JsonWebKey.Create(EcdsaPrivateKey.Create(ECCurve.NamedCurves.nistP521), "p521")
        ];

        foreach (JsonWebKey key in keys)
        {
            // When
            using JsonWebKey parsed = JsonWebKey.Parse(key.ToJson(includePrivateKey: true));
            using JsonWebKey publicOnly = JsonWebKey.Parse(key.ToJson());

            // Then
            Assert.True(parsed.HasPrivateKey);
            Assert.False(publicOnly.HasPrivateKey);
            Assert.Equal(key.ToJson(includePrivateKey: true), parsed.ToJson(includePrivateKey: true));
            Assert.Equal(key.KeyId, publicOnly.KeyId);
            Assert.DoesNotContain("\"d\"", key.ToJson());
            key.Dispose();
        }
    }

    [Fact(DisplayName = "JsonWebKey should convert a secp256k1 public key to and from EcdsaPublicKey on every platform")]
    public void JsonWebKeyShouldConvertSecp256k1PublicKey()
    {
        // Given (DIDComm v2.1 Appendix B.1, did:example:alice#key-3)
        JsonWebKey jwk = JsonWebKey.Parse(
            """
            {
              "kty": "EC",
              "crv": "secp256k1",
              "x": "aToW5EaTq5mlAf8C5ECYDSkqsJycrW-e1SQ6_GJcAOk",
              "y": "JAGX94caA21WKreXwYUaOCYTBMrqaX4KWIlsQZTHWCk"
            }
            """);

        // When
        EcdsaPublicKey key = jwk.ToEcdsaPublicKey();
        JsonWebKey roundTrip = JsonWebKey.Create(key);

        // Then
        Assert.Equal(jwk.ToJson(), roundTrip.ToJson());
    }

    [Fact(DisplayName = "JsonWebKey private key conversion should throw CryptographicException when the public part does not match")]
    public void JsonWebKeyShouldRejectMismatchedPublicPart()
    {
        // Given
        string other = Base64Url.Encode(EddsaPrivateKey.Create().GetPublicKey().Export());
        JsonElement json = JsonDocument.Parse(JsonWebKey.Create(EddsaPrivateKey.Create()).ToJson(includePrivateKey: true)).RootElement;
        string tampered = $$"""{"kty":"OKP","crv":"Ed25519","x":"{{other}}","d":"{{json.GetProperty("d").GetString()}}"}""";
        using JsonWebKey jwk = JsonWebKey.Parse(tampered);

        // When / Then
        Assert.Throws<CryptographicException>(() => jwk.ToEddsaPrivateKey());
    }

    [Theory(DisplayName = "JsonWebKey.Parse should throw CryptographicException for unsupported or malformed keys")]
    [InlineData("""{"crv":"X25519","x":"AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA"}""")]
    [InlineData("""{"kty":"oct","k":"AAAA"}""")]
    [InlineData("""{"kty":"OKP","crv":"X448","x":"AAAA"}""")]
    [InlineData("""{"kty":"OKP","crv":"X25519","x":"AAAA"}""")]
    [InlineData("""{"kty":"OKP","crv":"X25519","x":"AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA="}""")]
    [InlineData("""{"kty":"EC","crv":"P-256","x":"AAAA","y":"AAAA"}""")]
    [InlineData("""{"kty":"EC","crv":"P-192","x":"AAAA","y":"AAAA"}""")]
    [InlineData("""{"kty":"OKP","crv":"X25519","x":1}""")]
    [InlineData("""["not", "an", "object"]""")]
    [InlineData("""{"kty":"OKP",""")]
    public void JsonWebKeyParseShouldRejectInvalidKeys(string json)
    {
        // When / Then
        Assert.Throws<CryptographicException>(() => JsonWebKey.Parse(json));
    }

    [Fact(DisplayName = "JsonWebKey conversion should throw CryptographicException for a key of the wrong type or curve")]
    public void JsonWebKeyConversionShouldRejectWrongKind()
    {
        // Given
        using JsonWebKey x25519 = JsonWebKey.Create(XdhPrivateKey.Create());

        // When / Then
        Assert.Throws<CryptographicException>(() => x25519.ToEddsaPublicKey());
        Assert.Throws<CryptographicException>(() => x25519.ToEcdsaPublicKey());
        Assert.Throws<CryptographicException>(() => x25519.ToPublicKey().ToXdhPrivateKey());
    }
}
