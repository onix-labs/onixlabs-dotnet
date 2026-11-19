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
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace OnixLabs.Security.Cryptography.Jose;

/// <summary>
/// Represents a JSON Web Encryption (RFC 7516) in the General JSON serialization of RFC 7516 §7.2.1, with key agreement by ECDH-ES or
/// ECDH-1PU and key wrapping, as DIDComm Messaging v2.1 uses.
/// </summary>
/// <remarks>
/// The Flattened JSON serialization is read as well. The compact serialization is not supported. Every recipient shares one ephemeral key,
/// <c>epk</c>, in the protected header, so all recipient keys must be on the same curve.
/// </remarks>
public sealed partial class JsonWebEncryption
{
    /// <summary>
    /// The initialization vector.
    /// </summary>
    private readonly byte[] iv;

    /// <summary>
    /// The ciphertext.
    /// </summary>
    private readonly byte[] ciphertext;

    /// <summary>
    /// The authentication tag.
    /// </summary>
    private readonly byte[] tag;

    /// <summary>
    /// Initializes a new instance of the <see cref="JsonWebEncryption"/> class.
    /// </summary>
    /// <param name="encodedProtectedHeader">The base64url-encoded protected header, exactly as authenticated.</param>
    /// <param name="protectedHeader">The decoded protected header.</param>
    /// <param name="unprotectedHeader">The shared unprotected header, or <see langword="null"/>.</param>
    /// <param name="recipients">The recipients.</param>
    /// <param name="iv">The initialization vector.</param>
    /// <param name="ciphertext">The ciphertext.</param>
    /// <param name="tag">The authentication tag.</param>
    /// <param name="encodedAdditionalData">The base64url-encoded <c>aad</c> member, or <see langword="null"/>.</param>
    private JsonWebEncryption(
        string encodedProtectedHeader,
        JsonElement protectedHeader,
        JsonElement? unprotectedHeader,
        IReadOnlyList<JweRecipient> recipients,
        byte[] iv,
        byte[] ciphertext,
        byte[] tag,
        string? encodedAdditionalData)
    {
        EncodedProtectedHeader = encodedProtectedHeader;
        ProtectedHeader = protectedHeader;
        UnprotectedHeader = unprotectedHeader;
        Recipients = recipients;
        EncodedAdditionalData = encodedAdditionalData;
        this.iv = iv;
        this.ciphertext = ciphertext;
        this.tag = tag;
    }

    /// <summary>
    /// Gets the base64url-encoded protected header, exactly as authenticated.
    /// </summary>
    /// <value>The base64url-encoded protected header.</value>
    public string EncodedProtectedHeader { get; }

    /// <summary>
    /// Gets the decoded protected header.
    /// </summary>
    /// <value>The protected header as a JSON object.</value>
    public JsonElement ProtectedHeader { get; }

    /// <summary>
    /// Gets the shared unprotected header, which is not authenticated.
    /// </summary>
    /// <value>The shared unprotected header as a JSON object, or <see langword="null"/> if there is none.</value>
    public JsonElement? UnprotectedHeader { get; }

    /// <summary>
    /// Gets the recipients.
    /// </summary>
    /// <value>The recipients, at least one.</value>
    public IReadOnlyList<JweRecipient> Recipients { get; }

    /// <summary>
    /// Gets the base64url-encoded additional authenticated data, the <c>aad</c> member.
    /// </summary>
    /// <value>The <c>aad</c> member, or <see langword="null"/> if there is none.</value>
    public string? EncodedAdditionalData { get; }

    /// <summary>
    /// Gets the initialization vector.
    /// </summary>
    /// <value>The decoded <c>iv</c> member.</value>
    public ReadOnlySpan<byte> InitializationVector => iv;

    /// <summary>
    /// Gets the ciphertext.
    /// </summary>
    /// <value>The decoded <c>ciphertext</c> member.</value>
    public ReadOnlySpan<byte> Ciphertext => ciphertext;

    /// <summary>
    /// Gets the authentication tag.
    /// </summary>
    /// <value>The decoded <c>tag</c> member.</value>
    public ReadOnlySpan<byte> Tag => tag;

    /// <summary>
    /// Gets the key management algorithm, <c>alg</c>, from the protected or shared unprotected header.
    /// </summary>
    /// <value>The <c>alg</c> value, or <see langword="null"/> if it is only in the per-recipient headers.</value>
    public string? Algorithm => JoseJson.FindString("alg", ProtectedHeader, UnprotectedHeader);

    /// <summary>
    /// Gets the content encryption algorithm, <c>enc</c>, from the protected or shared unprotected header.
    /// </summary>
    /// <value>The <c>enc</c> value, or <see langword="null"/> if it is only in the per-recipient headers.</value>
    public string? EncryptionAlgorithm => JoseJson.FindString("enc", ProtectedHeader, UnprotectedHeader);

    /// <summary>
    /// Gets the sender's key identifier, <c>skid</c> (draft-madden-jose-ecdh-1pu-04 §2.2.1), which says which key to pass to
    /// <see cref="DecryptAsync"/> for ECDH-1PU.
    /// </summary>
    /// <value>The <c>skid</c> value, or <see langword="null"/> if there is none.</value>
    public string? SenderKeyId => JoseJson.FindString("skid", ProtectedHeader, UnprotectedHeader);

    /// <summary>
    /// Parses a JSON Web Encryption in the General or Flattened JSON serialization.
    /// </summary>
    /// <param name="json">The JSON text.</param>
    /// <returns>Returns a new <see cref="JsonWebEncryption"/>. Nothing is decrypted or authenticated.</returns>
    /// <exception cref="CryptographicException">Thrown when the JSON is not a well-formed JWE in either JSON serialization.</exception>
    public static JsonWebEncryption Parse(string json)
    {
        JsonElement root = JoseJson.ParseObject(json, "JSON Web Encryption");
        string encodedHeader = JoseJson.GetRequiredString(root, "protected");
        JsonElement protectedHeader = JoseJson.ParseObject(JoseJson.GetRequiredBytes(root, "protected"), "JWE protected header");
        JsonElement? unprotectedHeader = JoseJson.GetOptionalObject(root, "unprotected")?.Clone();
        List<JweRecipient> recipients = [];

        if (root.TryGetProperty("recipients", out JsonElement array))
        {
            if (array.ValueKind != JsonValueKind.Array || array.GetArrayLength() == 0)
                throw new CryptographicException("The JWE 'recipients' member must be a non-empty array.");

            if (root.TryGetProperty("encrypted_key", out _) || root.TryGetProperty("header", out _))
                throw new CryptographicException("The JWE uses both the General and the Flattened serialization.");

            foreach (JsonElement entry in array.EnumerateArray())
            {
                if (entry.ValueKind != JsonValueKind.Object) throw new CryptographicException("A JWE recipient must be a JSON object.");
                recipients.Add(ReadRecipient(entry, protectedHeader, unprotectedHeader));
            }
        }
        else
        {
            recipients.Add(ReadRecipient(root, protectedHeader, unprotectedHeader));
        }

        return new JsonWebEncryption(
            encodedHeader,
            protectedHeader,
            unprotectedHeader,
            recipients,
            JoseJson.GetRequiredBytes(root, "iv"),
            JoseJson.GetRequiredBytes(root, "ciphertext"),
            JoseJson.GetRequiredBytes(root, "tag"),
            JoseJson.GetOptionalBytes(root, "aad") is null ? null : JoseJson.GetRequiredString(root, "aad"));
    }

    /// <summary>
    /// Returns the General JWE JSON serialization (RFC 7516 §7.2.1).
    /// </summary>
    /// <returns>Returns compact JSON text.</returns>
    public string ToJson()
    {
        JsonObject result = new() { ["protected"] = EncodedProtectedHeader };

        if (UnprotectedHeader is { } unprotected) result["unprotected"] = JsonNode.Parse(unprotected.GetRawText());

        JsonArray recipients = [];

        foreach (JweRecipient recipient in Recipients)
        {
            JsonObject entry = [];
            if (recipient.Header is { } header) entry["header"] = JsonNode.Parse(header.GetRawText());
            entry["encrypted_key"] = JoseJson.EncodeBytes(recipient.EncryptedKey);
            recipients.Add(entry);
        }

        result["recipients"] = recipients;
        if (EncodedAdditionalData is not null) result["aad"] = EncodedAdditionalData;
        result["iv"] = JoseJson.EncodeBytes(iv);
        result["ciphertext"] = JoseJson.EncodeBytes(ciphertext);
        result["tag"] = JoseJson.EncodeBytes(tag);

        return JoseJson.Write(result);
    }

    /// <inheritdoc/>
    public override string ToString() => ToJson();

    /// <summary>
    /// Reads one recipient, checking that its header shares no member with the protected and shared unprotected headers.
    /// </summary>
    /// <param name="entry">The JSON object holding the recipient's <c>header</c> and <c>encrypted_key</c>.</param>
    /// <param name="protectedHeader">The protected header.</param>
    /// <param name="unprotectedHeader">The shared unprotected header, or <see langword="null"/>.</param>
    /// <returns>Returns a new <see cref="JweRecipient"/>.</returns>
    /// <exception cref="CryptographicException">Thrown when the recipient is malformed, or a header member appears more than
    /// once.</exception>
    private static JweRecipient ReadRecipient(JsonElement entry, JsonElement protectedHeader, JsonElement? unprotectedHeader)
    {
        JsonElement? header = JoseJson.GetOptionalObject(entry, "header")?.Clone();
        JoseJson.RequireDisjoint(protectedHeader, unprotectedHeader, header);
        return new JweRecipient(header, JoseJson.GetOptionalBytes(entry, "encrypted_key") ?? []);
    }

    /// <summary>
    /// Gets the additional authenticated data of the content encryption, as RFC 7516 §5.1 step 14 defines it.
    /// </summary>
    /// <returns>Returns ASCII(BASE64URL(protected header)), followed by '.' and the <c>aad</c> member if there is one.</returns>
    private byte[] GetAuthenticatedData() =>
        Encoding.ASCII.GetBytes(EncodedAdditionalData is null
            ? EncodedProtectedHeader
            : $"{EncodedProtectedHeader}.{EncodedAdditionalData}");

    /// <summary>
    /// Creates an <see cref="Aes"/> instance keyed with the specified key-encryption key, for AES Key Wrap.
    /// </summary>
    /// <param name="keyEncryptionKey">The 16, 24 or 32-byte key-encryption key.</param>
    /// <returns>Returns a new <see cref="Aes"/> instance, which the caller must dispose.</returns>
    private static Aes CreateKeyWrap(byte[] keyEncryptionKey)
    {
        Aes aes = Aes.Create();

        try
        {
            aes.Key = keyEncryptionKey;
            return aes;
        }
        catch
        {
            aes.Dispose();
            throw;
        }
    }
}
