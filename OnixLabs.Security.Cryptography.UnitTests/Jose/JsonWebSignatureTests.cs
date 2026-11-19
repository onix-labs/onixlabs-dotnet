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
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using OnixLabs.Security.Cryptography.Jose;
using OnixLabs.Security.Cryptography.UnitTests.Data.Jose;

namespace OnixLabs.Security.Cryptography.UnitTests.Jose;

public sealed class JsonWebSignatureTests
{
    /// <summary>
    /// Gets the cancellation token of the current test.
    /// </summary>
    /// <value>The cancellation token of the current test.</value>
    private static System.Threading.CancellationToken Token => TestContext.Current.CancellationToken;

    /// <summary>
    /// Verifies the Ed25519 signing example of RFC 8037 Appendix A.4: Ed25519 is deterministic, so signing the payload with the key of A.1
    /// must reproduce the published protected header and signature exactly.
    /// </summary>
    [Fact(DisplayName = "JsonWebSignature must reproduce the RFC 8037 Appendix A.4 Ed25519 signature exactly")]
    public async Task JsonWebSignatureMustReproduceRfc8037Signature()
    {
        // Given
        JsonElement vector = JoseTestVectors.Load("rfc8037_appendix_a.json");
        string[] compact = vector.GetText("jwsCompact").Split('.');
        using JsonWebKey jwk = JsonWebKey.Parse(vector.GetJson("ed25519PrivateKey"));
        IJwsSigner signer = JwsSigner.Create(jwk.ToEddsaPrivateKey());

        // When
        JsonWebSignature jws = await JsonWebSignature.SignAsync(Base64Url.Decode(compact[1]), [signer], cancellationToken: Token);

        // Then
        Assert.Equal("Example of Ed25519 signing", Encoding.UTF8.GetString(jws.Payload));
        Assert.Equal(compact[0], jws.Signatures[0].EncodedProtectedHeader);
        Assert.Equal(compact[2], Base64Url.Encode(jws.Signatures[0].Signature));
        Assert.True(jws.Verify(jwk.ToPublicKey()));
    }

    /// <summary>
    /// Verifies the validation example of RFC 8037 Appendix A.5, with the compact JWS of A.4 written in the Flattened JSON serialization.
    /// </summary>
    [Fact(DisplayName = "JsonWebSignature must validate the RFC 8037 Appendix A.5 example, and reject it when modified")]
    public void JsonWebSignatureMustValidateRfc8037Example()
    {
        // Given
        JsonElement vector = JoseTestVectors.Load("rfc8037_appendix_a.json");
        string[] compact = vector.GetText("jwsCompact").Split('.');
        using JsonWebKey publicKey = JsonWebKey.Parse(vector.GetJson("ed25519PrivateKey")).ToPublicKey();
        string flattened = $$"""{"payload":"{{compact[1]}}","protected":"{{compact[0]}}","signature":"{{compact[2]}}"}""";
        string modified = $$"""{"payload":"S{{compact[1][1..]}}","protected":"{{compact[0]}}","signature":"{{compact[2]}}"}""";

        // When
        JsonWebSignature jws = JsonWebSignature.Parse(flattened);

        // Then
        Assert.Equal(JwsAlgorithm.EdDsa, jws.Signatures[0].Algorithm);
        Assert.True(jws.Verify(publicKey));
        Assert.False(JsonWebSignature.Parse(modified).Verify(publicKey));
        Assert.Equal(flattened, jws.ToFlattenedJson());
    }

    /// <summary>
    /// Verifies the ES512 example of RFC 7520 §4.3 in both JSON serializations, with the public part of the P-521 key of Figure 2.
    /// </summary>
    [Fact(DisplayName = "JsonWebSignature must verify the RFC 7520 §4.3 ES512 example in the General and Flattened serializations")]
    public void JsonWebSignatureMustVerifyRfc7520Es512Example()
    {
        // Given
        JsonElement vector = JoseTestVectors.Load("rfc7520_4_3_es512.json");
        using JsonWebKey publicKey = JsonWebKey.Parse(vector.GetJson("key")).ToPublicKey();

        // When
        JsonWebSignature general = JsonWebSignature.Parse(vector.GetJson("general"));
        JsonWebSignature flattened = JsonWebSignature.Parse(vector.GetJson("flattened"));

        // Then
        Assert.True(general.Verify(publicKey));
        Assert.True(flattened.Verify(publicKey));
        Assert.Equal("bilbo.baggins@hobbiton.example", general.Signatures[0].KeyId);
        Assert.StartsWith("It’s a dangerous business, Frodo", Encoding.UTF8.GetString(general.Payload));
        Assert.Equal(general.ToFlattenedJson(), flattened.ToFlattenedJson());
    }

    /// <summary>
    /// Verifies the three signed messages of DIDComm Messaging v2.1 Appendix C.2 (EdDSA, ES256 and ES256K), each with the key that its
    /// <c>kid</c> names in the sender's DID document of Appendix B.1.
    /// </summary>
    [Theory(DisplayName = "JsonWebSignature must verify the DIDComm v2.1 Appendix C.2 signed messages")]
    [InlineData(0, "EdDSA")]
    [InlineData(1, "ES256")]
    [InlineData(2, "ES256K")]
    public void JsonWebSignatureMustVerifyDidCommSignedMessages(int index, string algorithm)
    {
        // Given
        JsonElement vectors = JoseTestVectors.Load("didcomm_v2_1_appendix.json");
        JsonWebSignature jws = JsonWebSignature.Parse(vectors.GetProperty("signed")[index].GetRawText());
        string keyId = jws.Signatures[0].KeyId!;
        using JsonWebKey publicKey = DidCommKeys.FindPublicKey(vectors.GetProperty("senderDidDoc"), keyId);
        using JsonWebKey otherKey = DidCommKeys.FindPublicKey(vectors.GetProperty("senderDidDoc"), "did:example:alice#key-2");

        // When
        bool valid = jws.Verify(publicKey);

        // Then
        Assert.Equal(algorithm, jws.Signatures[0].Algorithm);
        Assert.True(valid);
        Assert.Equal("1234567890", JsonDocument.Parse(jws.Payload.ToArray()).RootElement.GetProperty("id").GetString());
        if (algorithm != "ES256") Assert.False(jws.Signatures[0].Verify(otherKey));
    }

    [Theory(DisplayName = "JsonWebSignature should sign and verify with every algorithm it can sign with")]
    [InlineData("EdDSA")]
    [InlineData("ES256")]
    [InlineData("ES384")]
    [InlineData("ES512")]
    public async Task JsonWebSignatureShouldSignAndVerify(string algorithm)
    {
        // Given
        (IJwsSigner signer, JsonWebKey publicKey) = CreateSigner(algorithm, "key-1");
        byte[] payload = Encoding.UTF8.GetBytes("""{"id":"1234567890"}""");

        // When
        JsonWebSignature jws = await JsonWebSignature.SignAsync(payload, [signer], "application/didcomm-signed+json", Token);
        JsonWebSignature parsed = JsonWebSignature.Parse(jws.ToJson());

        // Then
        Assert.Equal(algorithm, parsed.Signatures[0].Algorithm);
        Assert.Equal("key-1", parsed.Signatures[0].KeyId);
        Assert.Equal("application/didcomm-signed+json", parsed.Signatures[0].ProtectedHeader.GetProperty("typ").GetString());
        Assert.True(parsed.Verify(publicKey));
        Assert.True(JsonWebSignature.Parse(jws.ToFlattenedJson()).Verify(publicKey));
        Assert.Equal(payload, parsed.Payload.ToArray());
    }

    [Fact(DisplayName = "JsonWebSignature should carry several signatures, and verify each by its key identifier")]
    public async Task JsonWebSignatureShouldCarrySeveralSignatures()
    {
        // Given
        (IJwsSigner first, JsonWebKey firstKey) = CreateSigner("EdDSA", "first");
        (IJwsSigner second, JsonWebKey secondKey) = CreateSigner("ES256", "second");
        (_, JsonWebKey unrelatedKey) = CreateSigner("ES256", "second");

        // When
        JsonWebSignature signed = await JsonWebSignature.SignAsync("payload"u8.ToArray(), [first, second], cancellationToken: Token);
        JsonWebSignature jws = JsonWebSignature.Parse(signed.ToJson());

        // Then
        Assert.Equal(2, jws.Signatures.Count);
        Assert.True(jws.Verify(firstKey));
        Assert.True(jws.Verify(secondKey));
        Assert.False(jws.Verify(unrelatedKey));
        Assert.Throws<InvalidOperationException>(() => jws.ToFlattenedJson());
    }

    [Fact(DisplayName = "JwsSignature.Verify should return false when the key does not suit the algorithm")]
    public async Task JwsSignatureVerifyShouldRejectKeyOfWrongKind()
    {
        // Given
        (IJwsSigner signer, _) = CreateSigner("ES256", null);
        using JsonWebKey p384 = JsonWebKey.Create(EcdsaPrivateKey.Create(ECCurve.NamedCurves.nistP384).GetPublicKey());
        using JsonWebKey ed25519 = JsonWebKey.Create(EddsaPrivateKey.Create().GetPublicKey());

        // When
        JsonWebSignature jws = await JsonWebSignature.SignAsync("payload"u8.ToArray(), [signer], cancellationToken: Token);

        // Then
        Assert.False(jws.Signatures[0].Verify(p384));
        Assert.False(jws.Signatures[0].Verify(ed25519));
    }

    [Fact(DisplayName = "JwsSignature.Verify should return false for a signature with 'crit' or an unsupported algorithm")]
    public async Task JwsSignatureVerifyShouldRejectCritAndUnsupportedAlgorithms()
    {
        // Given
        using EddsaPrivateKey key = EddsaPrivateKey.Create();
        using JsonWebKey publicKey = JsonWebKey.Create(key.GetPublicKey());
        string payload = Base64Url.Encode("payload"u8);

        // When
        JsonWebSignature crit = await SignWithHeaderAsync(key, """{"alg":"EdDSA","crit":["exp"],"exp":1}""", payload);
        JsonWebSignature none = await SignWithHeaderAsync(key, """{"alg":"none"}""", payload);
        JsonWebSignature control = await SignWithHeaderAsync(key, """{"alg":"EdDSA"}""", payload);

        // Then
        Assert.False(crit.Verify(publicKey));
        Assert.False(none.Verify(publicKey));
        Assert.True(control.Verify(publicKey));
    }

    [Theory(DisplayName = "JsonWebSignature.Parse should throw CryptographicException for malformed JWS JSON")]
    [InlineData("""{"payload":"cGF5bG9hZA","signatures":[]}""")]
    [InlineData("""{"payload":"cGF5bG9hZA","signatures":[{"protected":"eyJhbGciOiJFZERTQSJ9","header":{"alg":"x"},"signature":"AAAA"}]}""")]
    [InlineData("""{"payload":"cGF5bG9hZA","signatures":[{"protected":"e30","signature":"AAAA"}]}""")]
    [InlineData("""{"payload":"cGF5bG9hZA","signatures":[{"protected":"bm90IGpzb24","signature":"AAAA"}]}""")]
    [InlineData("""{"payload":"cGF5bG9hZA=","protected":"eyJhbGciOiJFZERTQSJ9","signature":"AAAA"}""")]
    [InlineData("""{"protected":"eyJhbGciOiJFZERTQSJ9","signature":"AAAA"}""")]
    public void JsonWebSignatureParseShouldRejectMalformedJson(string json)
    {
        // When / Then
        Assert.Throws<CryptographicException>(() => JsonWebSignature.Parse(json));
    }

    [Fact(DisplayName = "JwsSigner.Create should throw NotSupportedException for a secp256k1 key, which is verification-only")]
    public void JwsSignerCreateShouldRejectSecp256k1()
    {
        // Given
        EcdsaPrivateKey key;

        try
        {
            key = EcdsaPrivateKey.Create(ECCurve.CreateFromValue("1.3.132.0.10"));
        }
        catch (Exception exception) when (exception is PlatformNotSupportedException or CryptographicException)
        {
            // Why: macOS cannot create secp256k1 keys at all, so there is nothing to refuse there.
            return;
        }

        // When / Then
        Assert.Throws<NotSupportedException>(() => JwsSigner.Create(key));
    }

    /// <summary>
    /// Creates a signer and its public JWK for the specified algorithm, with a fresh key.
    /// </summary>
    /// <param name="algorithm">The JWS algorithm.</param>
    /// <param name="keyId">The key identifier.</param>
    /// <returns>Returns the signer and the public key that verifies it.</returns>
    private static (IJwsSigner Signer, JsonWebKey PublicKey) CreateSigner(string algorithm, string? keyId)
    {
        if (algorithm == "EdDSA")
        {
            EddsaPrivateKey eddsa = EddsaPrivateKey.Create();
            return (JwsSigner.Create(eddsa, keyId), JsonWebKey.Create(eddsa.GetPublicKey(), keyId));
        }

        ECCurve curve = algorithm switch
        {
            "ES256" => ECCurve.NamedCurves.nistP256,
            "ES384" => ECCurve.NamedCurves.nistP384,
            _ => ECCurve.NamedCurves.nistP521
        };

        EcdsaPrivateKey ecdsa = EcdsaPrivateKey.Create(curve);
        return (JwsSigner.Create(ecdsa, keyId), JsonWebKey.Create(ecdsa.GetPublicKey(), keyId));
    }

    /// <summary>
    /// Signs a payload under an arbitrary protected header, to test how verification treats headers that the API would not produce.
    /// </summary>
    /// <param name="key">The Ed25519 key to sign with.</param>
    /// <param name="headerJson">The protected header JSON.</param>
    /// <param name="encodedPayload">The base64url-encoded payload.</param>
    /// <returns>Returns the parsed JWS.</returns>
    private static Task<JsonWebSignature> SignWithHeaderAsync(EddsaPrivateKey key, string headerJson, string encodedPayload)
    {
        string encodedHeader = Base64Url.Encode(Encoding.UTF8.GetBytes(headerJson));
        string signature = Base64Url.Encode(key.SignData(Encoding.ASCII.GetBytes($"{encodedHeader}.{encodedPayload}")));
        string json = $$"""{"payload":"{{encodedPayload}}","protected":"{{encodedHeader}}","signature":"{{signature}}"}""";
        return Task.FromResult(JsonWebSignature.Parse(json));
    }
}
