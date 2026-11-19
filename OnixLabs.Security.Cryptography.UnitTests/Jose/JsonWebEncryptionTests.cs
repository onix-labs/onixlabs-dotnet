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
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using OnixLabs.Security.Cryptography.Jose;
using OnixLabs.Security.Cryptography.UnitTests.Data.Jose;

namespace OnixLabs.Security.Cryptography.UnitTests.Jose;

public sealed class JsonWebEncryptionTests
{
    /// <summary>
    /// Gets the cancellation token of the current test.
    /// </summary>
    /// <value>The cancellation token of the current test.</value>
    private static CancellationToken Token => TestContext.Current.CancellationToken;

    /// <summary>
    /// Verifies that both recipients of draft-madden-jose-ecdh-1pu-04 Appendix B.11 (ECDH-1PU+A128KW, A256CBC-HS512, X25519) can decrypt
    /// it, given Alice's static public key.
    /// </summary>
    [Theory(DisplayName = "JsonWebEncryption must decrypt draft-madden-jose-ecdh-1pu-04 Appendix B for both recipients")]
    [InlineData("bob", "bob-key-2")]
    [InlineData("charlie", "2021-05-06")]
    public async Task JsonWebEncryptionMustDecryptEcdh1PuAppendixB(string recipientName, string keyId)
    {
        // Given
        JsonElement vector = JoseTestVectors.Load("ecdh_1pu_04_appendix_b.json");
        JsonWebEncryption jwe = JsonWebEncryption.Parse(vector.GetJson("jwe"));
        using JsonWebKey recipientKey = JsonWebKey.Parse(vector.GetJson(recipientName));
        using JsonWebKey senderKey = JsonWebKey.Parse(vector.GetJson("aliceStatic"));
        IJweKeyAgreement recipient = JweKeyAgreement.Create(recipientKey.ToXdhPrivateKey(), keyId);

        // When
        byte[] plaintext = await jwe.DecryptAsync(recipient, senderKey.ToPublicKey(), Token);

        // Then
        Assert.Equal("Three is a magic number.", Encoding.UTF8.GetString(plaintext));
        Assert.Equal(JweAlgorithm.Ecdh1PuA128Kw, jwe.Algorithm);
        Assert.Equal("https://alice.example.com/keys.jwks", jwe.UnprotectedHeader!.Value.GetProperty("jku").GetString());
    }

    /// <summary>
    /// Verifies that encrypting the plaintext of draft-madden-jose-ecdh-1pu-04 Appendix B, with its ephemeral key, CEK and IV, reproduces
    /// the JWE of Appendix B.11 exactly: the protected header byte for byte, both encrypted keys, the ciphertext and the tag.
    /// </summary>
    [Fact(DisplayName = "JsonWebEncryption must reproduce draft-madden-jose-ecdh-1pu-04 Appendix B.11 exactly")]
    public async Task JsonWebEncryptionMustReproduceEcdh1PuAppendixB()
    {
        // Given
        JsonElement vector = JoseTestVectors.Load("ecdh_1pu_04_appendix_b.json");
        JsonElement expected = vector.GetProperty("jwe");
        using JsonWebKey aliceStatic = JsonWebKey.Parse(vector.GetJson("aliceStatic"));
        using JsonWebKey aliceEphemeral = JsonWebKey.Parse(vector.GetJson("aliceEphemeral"));
        using JsonWebKey bob = JsonWebKey.Parse(vector.GetJson("bob"));
        using JsonWebKey charlie = JsonWebKey.Parse(vector.GetJson("charlie"));
        JweEncryptionOptions options = new()
        {
            Algorithm = JweAlgorithm.Ecdh1PuA128Kw,
            EncryptionAlgorithm = JweAlgorithm.A256CbcHs512,
            Sender = JweKeyAgreement.Create(aliceStatic.ToXdhPrivateKey()),
            AgreementPartyUInfo = Encoding.UTF8.GetBytes("Alice"),
            AgreementPartyVInfo = Encoding.UTF8.GetBytes("Bob and Charlie"),
            SharedUnprotectedHeader = new Dictionary<string, string> { ["jku"] = "https://alice.example.com/keys.jwks" },
            EphemeralKeyFactory = _ => JweKeyAgreement.Create(aliceEphemeral.ToXdhPrivateKey()),
            ContentEncryptionKey = Base64Url.Decode(vector.GetText("cek")),
            InitializationVector = Base64Url.Decode(vector.GetText("iv"))
        };

        // When
        JsonWebKey[] recipients = [bob.ToPublicKey().WithKeyId("bob-key-2"), charlie.ToPublicKey().WithKeyId("2021-05-06")];
        byte[] plaintext = Base64Url.Decode(vector.GetText("plaintext"));
        JsonWebEncryption jwe = await JsonWebEncryption.EncryptAsync(plaintext, recipients, options, Token);
        JsonElement actual = JsonDocument.Parse(jwe.ToJson()).RootElement;

        // Then
        Assert.Equal(expected.GetText("protected"), actual.GetText("protected"));
        Assert.Equal(expected.GetText("iv"), actual.GetText("iv"));
        Assert.Equal(expected.GetText("ciphertext"), actual.GetText("ciphertext"));
        Assert.Equal(expected.GetText("tag"), actual.GetText("tag"));
        Assert.Equal(Compact(expected, "unprotected"), Compact(actual, "unprotected"));

        for (int i = 0; i < 2; i++)
        {
            JsonElement expectedRecipient = expected.GetProperty("recipients")[i];
            JsonElement actualRecipient = actual.GetProperty("recipients")[i];
            Assert.Equal(expectedRecipient.GetText("encrypted_key"), actualRecipient.GetText("encrypted_key"));
            Assert.Equal(Compact(expectedRecipient, "header"), Compact(actualRecipient, "header"));
        }
    }

    /// <summary>
    /// Verifies the ECDH-ES+A128KW and A128GCM example of RFC 7520 §5.4 (P-384) in both JSON serializations, and that the published
    /// ephemeral key and CEK reproduce its encrypted key.
    /// </summary>
    [Fact(DisplayName = "JsonWebEncryption must decrypt the RFC 7520 §5.4 example, and reproduce its encrypted key")]
    public async Task JsonWebEncryptionMustDecryptRfc7520Example()
    {
        // Given
        JsonElement vector = JoseTestVectors.Load("rfc7520_5_4_ecdh_es_a128kw.json");
        using JsonWebKey key = JsonWebKey.Parse(vector.GetJson("key"));
        using JsonWebKey ephemeral = JsonWebKey.Parse(vector.GetJson("ephemeralKey"));
        IJweKeyAgreement recipient = JweKeyAgreement.Create(key.ToEcdhPrivateKey());
        byte[] expected = Base64Url.Decode(vector.GetText("plaintext"));
        JweEncryptionOptions options = new()
        {
            Algorithm = JweAlgorithm.EcdhEsA128Kw,
            EncryptionAlgorithm = JweAlgorithm.A128Gcm,
            EphemeralKeyFactory = _ => JweKeyAgreement.Create(ephemeral.ToEcdhPrivateKey()),
            ContentEncryptionKey = Base64Url.Decode(vector.GetText("cek")),
            InitializationVector = Base64Url.Decode(vector.GetText("iv"))
        };

        // When
        byte[] fromGeneral = await JsonWebEncryption.Parse(vector.GetJson("general")).DecryptAsync(recipient, cancellationToken: Token);
        byte[] fromFlattened = await JsonWebEncryption.Parse(vector.GetJson("flattened")).DecryptAsync(recipient, cancellationToken: Token);
        JsonWebEncryption reproduced = await JsonWebEncryption.EncryptAsync(expected, [key.ToPublicKey()], options, Token);

        // Then
        Assert.Equal(expected, fromGeneral);
        Assert.Equal(expected, fromFlattened);
        Assert.StartsWith("You can trust us to stick with you", Encoding.UTF8.GetString(fromGeneral));
        Assert.Equal(vector.GetText("encryptedKey"), Base64Url.Encode(reproduced.Recipients[0].EncryptedKey));
        Assert.Equal(expected, await reproduced.DecryptAsync(recipient, cancellationToken: Token));
    }

    /// <summary>
    /// Verifies the X25519 ECDH-ES key agreement of RFC 8037 Appendix A.6: the ephemeral public key, and Z computed by sender and receiver.
    /// The receiver's private key is not given there; it is Bob's key of RFC 7748 §6.1, whose public key A.6 uses.
    /// </summary>
    [Fact(DisplayName = "JweKeyAgreement must reproduce the RFC 8037 Appendix A.6 X25519 key agreement")]
    public async Task JweKeyAgreementMustReproduceRfc8037X25519Example()
    {
        // Given
        JsonElement vector = JoseTestVectors.Load("rfc8037_appendix_a.json");
        using JsonWebKey bobPublic = JsonWebKey.Parse(vector.GetJson("x25519RecipientPublicKey"));
        byte[] ephemeralSecret = Convert.FromHexString(vector.GetText("x25519EphemeralSecret"));
        IJweKeyAgreement ephemeral = JweKeyAgreement.Create(XdhPrivateKey.Import(ephemeralSecret));
        IJweKeyAgreement bob = JweKeyAgreement.Create(XdhPrivateKey.Import(Convert.FromHexString(
            "5dab087e624a8a4b79e17f8b83800ee66f3bb1292618b6fd1c2f8b27ff88e0eb")));

        // When
        using Secret senderZ = await ephemeral.DeriveSharedSecretAsync(bobPublic, Token);
        using Secret receiverZ = await bob.DeriveSharedSecretAsync(ephemeral.PublicKey, Token);

        // Then
        Assert.Equal(vector.GetText("x25519EphemeralPublic"), Convert.ToHexString(ephemeral.PublicKey.X).ToLowerInvariant());
        Assert.Equal(bobPublic.X.ToArray(), bob.PublicKey.X.ToArray());
        Assert.Equal(vector.GetText("x25519SharedSecret"), Convert.ToHexString(senderZ.AsReadOnlySpan()).ToLowerInvariant());
        Assert.Equal(senderZ, receiverZ);
    }

    /// <summary>
    /// Verifies the encrypted messages of DIDComm Messaging v2.1 Appendix C.3 whose content encryption is supported, for every recipient
    /// key: the decrypted plaintext is the message of Appendix C.1, or for the fifth example a JWS of it signed with
    /// <c>did:example:alice#key-1</c>.
    /// </summary>
    /// <remarks>
    /// The fifth example is described as using P-521, but its header and recipient keys are P-256; the data is what is tested.
    /// </remarks>
    [Theory(DisplayName = "JsonWebEncryption must decrypt the DIDComm v2.1 Appendix C.3 encrypted messages for every recipient")]
    [InlineData(1, "ECDH-ES+A256KW", "A256CBC-HS512", null)]
    [InlineData(2, "ECDH-ES+A256KW", "A256GCM", null)]
    [InlineData(3, "ECDH-1PU+A256KW", "A256CBC-HS512", "did:example:alice#key-x25519-1")]
    [InlineData(4, "ECDH-1PU+A256KW", "A256CBC-HS512", "did:example:alice#key-p256-1")]
    public async Task JsonWebEncryptionMustDecryptDidCommMessages(int index, string algorithm, string encryption, string? senderKeyId)
    {
        // Given
        JsonElement vectors = JoseTestVectors.Load("didcomm_v2_1_appendix.json");
        JsonWebEncryption jwe = JsonWebEncryption.Parse(vectors.GetProperty("encrypted")[index].GetRawText());
        using JsonWebKey? sender = senderKeyId is null ? null : DidCommKeys.FindPublicKey(vectors.GetProperty("senderDidDoc"), senderKeyId);
        string[] keyIds = jwe.Recipients.Select(recipient => recipient.KeyId!).ToArray();

        foreach (string keyId in keyIds)
        {
            IJweKeyAgreement recipient = DidCommKeys.CreateKeyAgreement(vectors.GetProperty("recipientSecrets"), keyId);

            // When
            byte[] plaintext = await jwe.DecryptAsync(recipient, sender, Token);

            // Then
            Assert.Equal(algorithm, jwe.Algorithm);
            Assert.Equal(encryption, jwe.EncryptionAlgorithm);
            Assert.Equal(senderKeyId, jwe.SenderKeyId);
            Assert.Equal("1234567890", ReadMessage(vectors, plaintext).GetProperty("id").GetString());
        }

        string apv = Base64Url.Encode(JweAgreementInfo.CreateDidCommApv(keyIds));
        Assert.Equal(apv, jwe.ProtectedHeader.GetProperty("apv").GetString());

        if (senderKeyId is null) return;

        string apu = Base64Url.Encode(JweAgreementInfo.CreateDidCommApu(senderKeyId));
        Assert.Equal(apu, jwe.ProtectedHeader.GetProperty("apu").GetString());
    }

    /// <summary>
    /// Verifies the two encrypted messages of DIDComm Messaging v2.1 Appendix C.3 that use XC20P, which is not supported: decryption is
    /// refused, but each recipient's key agreement and key unwrapping succeed, the A256KW integrity check authenticating the result, and
    /// every recipient recovers the same content encryption key.
    /// </summary>
    [Theory(DisplayName = "JsonWebEncryption must unwrap the key of the DIDComm v2.1 XC20P messages, and refuse to decrypt them")]
    [InlineData(0)]
    [InlineData(5)]
    public async Task JsonWebEncryptionMustUnwrapDidCommXc20PMessages(int index)
    {
        // Given
        JsonElement vectors = JoseTestVectors.Load("didcomm_v2_1_appendix.json");
        JsonWebEncryption jwe = JsonWebEncryption.Parse(vectors.GetProperty("encrypted")[index].GetRawText());
        List<byte[]> keys = [];

        foreach (JweRecipient entry in jwe.Recipients)
        {
            IJweKeyAgreement recipient = DidCommKeys.CreateKeyAgreement(vectors.GetProperty("recipientSecrets"), entry.KeyId!);

            // When
            keys.Add(await jwe.UnwrapContentEncryptionKeyAsync(recipient, null, Token));

            // Then
            await Assert.ThrowsAsync<NotSupportedException>(async () => await jwe.DecryptAsync(recipient, cancellationToken: Token));
        }

        Assert.Equal("XC20P", jwe.EncryptionAlgorithm);
        Assert.All(keys, key => Assert.Equal(keys[0], key));
        Assert.Equal(32, keys[0].Length);
    }

    [Theory(DisplayName = "JsonWebEncryption should round-trip anoncrypt and authcrypt on every curve, for several recipients")]
    [InlineData("X25519", "ECDH-ES+A256KW", "A256CBC-HS512")]
    [InlineData("X25519", "ECDH-ES+A256KW", "A256GCM")]
    [InlineData("X25519", "ECDH-1PU+A256KW", "A256CBC-HS512")]
    [InlineData("P-256", "ECDH-ES+A256KW", "A256CBC-HS512")]
    [InlineData("P-256", "ECDH-1PU+A256KW", "A256CBC-HS512")]
    [InlineData("P-384", "ECDH-ES+A256KW", "A256GCM")]
    [InlineData("P-384", "ECDH-1PU+A256KW", "A256CBC-HS512")]
    [InlineData("P-521", "ECDH-ES+A192KW", "A192CBC-HS384")]
    [InlineData("P-521", "ECDH-1PU+A128KW", "A128CBC-HS256")]
    public async Task JsonWebEncryptionShouldRoundTrip(string curve, string algorithm, string encryption)
    {
        // Given
        IJweKeyAgreement[] recipients = [CreateKeyAgreement(curve, "did:example:bob#1"), CreateKeyAgreement(curve, "did:example:bob#2")];
        IJweKeyAgreement? sender = algorithm.StartsWith("ECDH-1PU") ? CreateKeyAgreement(curve, "did:example:alice#1") : null;
        byte[] plaintext = Encoding.UTF8.GetBytes("""{"id":"1234567890","body":{}}""");
        string[] keyIds = recipients.Select(recipient => recipient.PublicKey.KeyId!).ToArray();
        JweEncryptionOptions options = new()
        {
            Algorithm = algorithm,
            EncryptionAlgorithm = encryption,
            Sender = sender,
            Type = "application/didcomm-encrypted+json",
            AgreementPartyUInfo = sender is null ? null : JweAgreementInfo.CreateDidCommApu(sender.PublicKey.KeyId!),
            AgreementPartyVInfo = JweAgreementInfo.CreateDidCommApv(keyIds)
        };

        // When
        JsonWebKey[] publicKeys = recipients.Select(recipient => recipient.PublicKey).ToArray();
        JsonWebEncryption encrypted = await JsonWebEncryption.EncryptAsync(plaintext, publicKeys, options, Token);
        JsonWebEncryption jwe = JsonWebEncryption.Parse(encrypted.ToJson());

        // Then
        foreach (IJweKeyAgreement recipient in recipients)
            Assert.Equal(plaintext, await jwe.DecryptAsync(recipient, sender?.PublicKey, Token));

        Assert.Equal(sender?.PublicKey.KeyId, jwe.SenderKeyId);
        Assert.Equal("application/didcomm-encrypted+json", jwe.ProtectedHeader.GetProperty("typ").GetString());
        Assert.Equal(curve, jwe.ProtectedHeader.GetProperty("epk").GetProperty("crv").GetString());
        Assert.False(jwe.ProtectedHeader.GetProperty("epk").TryGetProperty("d", out _));
    }

    [Theory(DisplayName = "JsonWebEncryption.DecryptAsync should fail when any member is modified")]
    [InlineData("protected", typeof(AuthenticationTagMismatchException))]
    [InlineData("iv", typeof(AuthenticationTagMismatchException))]
    [InlineData("ciphertext", typeof(AuthenticationTagMismatchException))]
    [InlineData("tag", typeof(CryptographicException))]
    [InlineData("encrypted_key", typeof(CryptographicException))]
    public async Task JsonWebEncryptionDecryptShouldFailWhenModified(string member, Type expected)
    {
        // Given
        IJweKeyAgreement recipient = CreateKeyAgreement("X25519", "bob");
        IJweKeyAgreement sender = CreateKeyAgreement("X25519", "alice");
        JweEncryptionOptions options = new() { Algorithm = JweAlgorithm.Ecdh1PuA256Kw, Sender = sender };
        JsonWebEncryption jwe = await JsonWebEncryption.EncryptAsync("secret"u8.ToArray(), [recipient.PublicKey], options, Token);
        string modified = Modify(jwe.ToJson(), member);

        // When
        JsonWebEncryption parsed = JsonWebEncryption.Parse(modified);
        Exception? exception = await Record.ExceptionAsync(async () => await parsed.DecryptAsync(recipient, sender.PublicKey, Token));

        // Then
        Assert.NotNull(exception);
        Assert.IsAssignableFrom(expected, exception);
    }

    [Fact(DisplayName = "JsonWebEncryption.DecryptAsync should fail for ECDH-1PU with the wrong sender key, or without one")]
    public async Task JsonWebEncryptionDecryptShouldFailWithWrongSender()
    {
        // Given
        IJweKeyAgreement recipient = CreateKeyAgreement("P-256", "bob");
        IJweKeyAgreement sender = CreateKeyAgreement("P-256", "alice");
        IJweKeyAgreement impostor = CreateKeyAgreement("P-256", null);
        JweEncryptionOptions options = new() { Algorithm = JweAlgorithm.Ecdh1PuA256Kw, Sender = sender };
        JsonWebEncryption jwe = await JsonWebEncryption.EncryptAsync("secret"u8.ToArray(), [recipient.PublicKey], options, Token);

        // When / Then
        await Assert.ThrowsAnyAsync<CryptographicException>(async () => await jwe.DecryptAsync(recipient, impostor.PublicKey, Token));
        JsonWebKey mallory = impostor.PublicKey.WithKeyId("mallory");
        await Assert.ThrowsAsync<ArgumentException>(async () => await jwe.DecryptAsync(recipient, mallory, Token));
        await Assert.ThrowsAsync<ArgumentException>(async () => await jwe.DecryptAsync(recipient, cancellationToken: Token));
    }

    [Fact(DisplayName = "JsonWebEncryption.DecryptAsync should throw CryptographicException for a key that is not a recipient")]
    public async Task JsonWebEncryptionDecryptShouldFailForNonRecipient()
    {
        // Given
        IJweKeyAgreement recipient = CreateKeyAgreement("X25519", "bob");
        IJweKeyAgreement stranger = CreateKeyAgreement("X25519", "eve");
        IJweKeyAgreement impersonator = JweKeyAgreement.Create(XdhPrivateKey.Create(), "bob");
        JsonWebEncryption jwe = await JsonWebEncryption.EncryptAsync("secret"u8.ToArray(), [recipient.PublicKey], new(), Token);

        // When / Then
        await Assert.ThrowsAnyAsync<CryptographicException>(async () => await jwe.DecryptAsync(stranger, cancellationToken: Token));
        await Assert.ThrowsAnyAsync<CryptographicException>(async () => await jwe.DecryptAsync(impersonator, cancellationToken: Token));
    }

    [Fact(DisplayName = "JsonWebEncryption.EncryptAsync should reject invalid algorithm, sender and curve combinations")]
    public async Task JsonWebEncryptionEncryptShouldRejectInvalidOptions()
    {
        // Given
        IJweKeyAgreement x25519 = CreateKeyAgreement("X25519", "a");
        IJweKeyAgreement p256 = CreateKeyAgreement("P-256", "b");

        // When / Then
        JweEncryptionOptions onePassUnifiedWithGcm = new()
        {
            Algorithm = JweAlgorithm.Ecdh1PuA256Kw,
            EncryptionAlgorithm = JweAlgorithm.A256Gcm,
            Sender = x25519
        };

        await Assert.ThrowsAsync<CryptographicException>(async () => await JsonWebEncryption.EncryptAsync(
            "m"u8.ToArray(), [x25519.PublicKey], onePassUnifiedWithGcm, Token));
        await Assert.ThrowsAsync<ArgumentException>(async () => await JsonWebEncryption.EncryptAsync(
            "m"u8.ToArray(), [x25519.PublicKey], new JweEncryptionOptions { Algorithm = JweAlgorithm.Ecdh1PuA256Kw }, Token));
        await Assert.ThrowsAsync<ArgumentException>(async () => await JsonWebEncryption.EncryptAsync(
            "m"u8.ToArray(), [x25519.PublicKey], new JweEncryptionOptions { Sender = x25519 }, Token));
        await Assert.ThrowsAsync<ArgumentException>(async () => await JsonWebEncryption.EncryptAsync(
            "m"u8.ToArray(), [x25519.PublicKey, p256.PublicKey], new JweEncryptionOptions(), Token));
        await Assert.ThrowsAsync<NotSupportedException>(async () => await JsonWebEncryption.EncryptAsync(
            "m"u8.ToArray(), [x25519.PublicKey], new JweEncryptionOptions { EncryptionAlgorithm = "XC20P" }, Token));
    }

    [Fact(DisplayName = "JsonWebEncryption.Parse should throw CryptographicException when a header member appears more than once")]
    public async Task JsonWebEncryptionParseShouldRejectDuplicateHeaderMembers()
    {
        // Given
        IJweKeyAgreement recipient = CreateKeyAgreement("X25519", "bob");
        JsonWebEncryption jwe = await JsonWebEncryption.EncryptAsync("m"u8.ToArray(), [recipient.PublicKey], new(), Token);
        string json = jwe.ToJson().Replace("\"header\":{\"kid\":\"bob\"}", "\"header\":{\"kid\":\"bob\",\"alg\":\"ECDH-ES+A128KW\"}");

        // When / Then
        Assert.Throws<CryptographicException>(() => JsonWebEncryption.Parse(json));
    }

    [Fact(DisplayName = "JweOtherInfo.Create must produce the FixedInfo of draft-madden-jose-ecdh-1pu-04 Appendix B.9")]
    public void JweOtherInfoMustProduceEcdh1PuFixedInfo()
    {
        // Given (draft-madden-jose-ecdh-1pu-04 Appendix B.8 and B.9)
        byte[] tag = Base64Url.Decode("HLb4fTlm8spGmij3RyOs2gJ4DpHM4hhVRwdF_hGb3WQ");
        const string expected =
            "00 00 00 0f 45 43 44 48 2d 31 50 55 2b 41 31 32" +
            "38 4b 57 00 00 00 05 41 6c 69 63 65 00 00 00 0f" +
            "42 6f 62 20 61 6e 64 20 43 68 61 72 6c 69 65 00" +
            "00 00 80 00 00 00 20 1c b6 f8 7d 39 66 f2 ca 46" +
            "9a 28 f7 47 23 ac da 02 78 0e 91 cc e2 18 55 47" +
            "07 45 fe 11 9b dd 64";

        // When
        byte[] otherInfo = JweOtherInfo.Create("ECDH-1PU+A128KW", "Alice"u8, "Bob and Charlie"u8, 128, tag);

        // Then
        Assert.Equal(Convert.FromHexString(expected.Replace(" ", string.Empty)), otherInfo);
    }

    /// <summary>
    /// Writes the specified member compactly, so that JSON objects can be compared regardless of how they were laid out.
    /// </summary>
    /// <param name="element">The JSON object.</param>
    /// <param name="name">The member name.</param>
    /// <returns>Returns the compact JSON text of the member.</returns>
    private static string Compact(JsonElement element, string name) => JsonSerializer.Serialize(element.GetProperty(name));

    /// <summary>
    /// Reads the DIDComm plaintext message from a decrypted payload, unwrapping it first if it is a signed message.
    /// </summary>
    /// <param name="vectors">The DIDComm test vectors, whose sender DID document holds the signing key.</param>
    /// <param name="plaintext">The decrypted payload.</param>
    /// <returns>Returns the plaintext message.</returns>
    private static JsonElement ReadMessage(JsonElement vectors, byte[] plaintext)
    {
        JsonElement root = JsonDocument.Parse(plaintext).RootElement;

        if (!root.TryGetProperty("signatures", out _)) return root;

        JsonWebSignature jws = JsonWebSignature.Parse(Encoding.UTF8.GetString(plaintext));
        using JsonWebKey signingKey = DidCommKeys.FindPublicKey(vectors.GetProperty("senderDidDoc"), "did:example:alice#key-1");
        Assert.True(jws.Verify(signingKey));
        return JsonDocument.Parse(jws.Payload.ToArray()).RootElement;
    }

    /// <summary>
    /// Creates a key agreement with a fresh key on the specified curve.
    /// </summary>
    /// <param name="curve">The curve: X25519, P-256, P-384 or P-521.</param>
    /// <param name="keyId">The key identifier.</param>
    /// <returns>Returns a new key agreement.</returns>
    private static IJweKeyAgreement CreateKeyAgreement(string curve, string? keyId) => curve switch
    {
        "X25519" => JweKeyAgreement.Create(XdhPrivateKey.Create(), keyId),
        "P-256" => JweKeyAgreement.Create(EcdhPrivateKey.Create(ECCurve.NamedCurves.nistP256), keyId),
        "P-384" => JweKeyAgreement.Create(EcdhPrivateKey.Create(ECCurve.NamedCurves.nistP384), keyId),
        _ => JweKeyAgreement.Create(EcdhPrivateKey.Create(ECCurve.NamedCurves.nistP521), keyId)
    };

    /// <summary>
    /// Modifies one base64url member of a General JWE JSON serialization by flipping a bit of its first decoded byte.
    /// </summary>
    /// <param name="json">The JWE JSON.</param>
    /// <param name="member">The member to modify: <c>protected</c>, <c>iv</c>, <c>ciphertext</c>, <c>tag</c> or
    /// <c>encrypted_key</c>.</param>
    /// <returns>Returns the modified JSON.</returns>
    private static string Modify(string json, string member)
    {
        JsonElement root = JsonDocument.Parse(json).RootElement;
        string original = member == "encrypted_key" ? root.GetProperty("recipients")[0].GetText(member) : root.GetText(member);
        byte[] bytes = Base64Url.Decode(original);

        if (member == "protected")
            bytes = Encoding.UTF8.GetBytes(Encoding.UTF8.GetString(bytes).Replace("\"enc\"", "\"zip\":\"x\",\"enc\""));
        else
            bytes[0] ^= 1;

        return json.Replace($"\"{original}\"", $"\"{Base64Url.Encode(bytes)}\"");
    }
}
