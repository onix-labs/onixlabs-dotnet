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
using System.Text.Json.Nodes;
using System.Threading;
using System.Threading.Tasks;

namespace OnixLabs.Security.Cryptography.Jose;

/// <summary>
/// Represents a JSON Web Signature (RFC 7515) in the JSON serializations of RFC 7515 §7.2: General, with any number of signatures, and
/// Flattened, with one.
/// </summary>
/// <remarks>The compact serialization is not supported. Payloads are always base64url-encoded; the <c>b64</c> option of RFC 7797 is not
/// supported.</remarks>
public sealed class JsonWebSignature
{
    /// <summary>
    /// The base64url-encoded payload.
    /// </summary>
    private readonly string encodedPayload;

    /// <summary>
    /// The decoded payload.
    /// </summary>
    private readonly byte[] payload;

    /// <summary>
    /// Initializes a new instance of the <see cref="JsonWebSignature"/> class.
    /// </summary>
    /// <param name="encodedPayload">The base64url-encoded payload.</param>
    /// <param name="payload">The decoded payload.</param>
    /// <param name="signatures">The signatures.</param>
    private JsonWebSignature(string encodedPayload, byte[] payload, IReadOnlyList<JwsSignature> signatures)
    {
        this.encodedPayload = encodedPayload;
        this.payload = payload;
        Signatures = signatures;
    }

    /// <summary>
    /// Gets the payload.
    /// </summary>
    /// <value>The decoded payload, which is only authentic once a signature has been verified.</value>
    public ReadOnlySpan<byte> Payload => payload;

    /// <summary>
    /// Gets the signatures.
    /// </summary>
    /// <value>The signatures, at least one.</value>
    public IReadOnlyList<JwsSignature> Signatures { get; }

    /// <summary>
    /// Signs the specified payload with each of the specified signers.
    /// </summary>
    /// <param name="payload">The payload to sign.</param>
    /// <param name="signers">The signers, at least one.</param>
    /// <param name="type">The <c>typ</c> to write to each protected header, such as <c>application/didcomm-signed+json</c>, or
    /// <see langword="null"/>.</param>
    /// <param name="cancellationToken">A token used to cancel the operation.</param>
    /// <returns>Returns a new <see cref="JsonWebSignature"/>, whose protected headers carry <c>typ</c> (if given), <c>alg</c> and
    /// <c>kid</c> (if the signer has one).</returns>
    /// <exception cref="ArgumentException">Thrown when <paramref name="signers"/> is empty.</exception>
    public static async ValueTask<JsonWebSignature> SignAsync(
        ReadOnlyMemory<byte> payload,
        IReadOnlyList<IJwsSigner> signers,
        string? type = null,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(signers);
        Require(signers.Count > 0, "At least one signer is required.", nameof(signers));

        string encodedPayload = Base64Url.Encode(payload.Span);
        List<JwsSignature> signatures = [];

        foreach (IJwsSigner signer in signers)
        {
            JsonObject header = [];
            if (type is not null) header["typ"] = type;
            header["alg"] = signer.Algorithm;
            if (signer.KeyId is not null) header["kid"] = signer.KeyId;

            string headerJson = JoseJson.Write(header);
            string encodedHeader = Base64Url.Encode(Encoding.UTF8.GetBytes(headerJson));
            byte[] signingInput = Encoding.ASCII.GetBytes($"{encodedHeader}.{encodedPayload}");
            byte[] signature = await signer.SignAsync(signingInput, cancellationToken).ConfigureAwait(false);

            JsonElement protectedHeader = JoseJson.ParseObject(headerJson, "JWS protected header");
            signatures.Add(new JwsSignature(encodedPayload, encodedHeader, protectedHeader, null, signature));
        }

        return new JsonWebSignature(encodedPayload, payload.ToArray(), signatures);
    }

    /// <summary>
    /// Parses a JSON Web Signature in the General or Flattened JSON serialization.
    /// </summary>
    /// <param name="json">The JSON text.</param>
    /// <returns>Returns a new <see cref="JsonWebSignature"/>. Its signatures are not verified.</returns>
    /// <exception cref="CryptographicException">Thrown when the JSON is not a well-formed JWS in either JSON serialization.</exception>
    public static JsonWebSignature Parse(string json)
    {
        JsonElement root = JoseJson.ParseObject(json, "JSON Web Signature");
        string encodedPayload = JoseJson.GetRequiredString(root, "payload");
        byte[] payload = JoseJson.GetRequiredBytes(root, "payload");
        List<JwsSignature> signatures = [];

        if (root.TryGetProperty("signatures", out JsonElement array))
        {
            if (array.ValueKind != JsonValueKind.Array || array.GetArrayLength() == 0)
                throw new CryptographicException("The JWS 'signatures' member must be a non-empty array.");

            if (root.TryGetProperty("signature", out _))
                throw new CryptographicException("The JWS uses both the General and the Flattened serialization.");

            foreach (JsonElement entry in array.EnumerateArray()) signatures.Add(ReadSignature(encodedPayload, entry));
        }
        else
        {
            signatures.Add(ReadSignature(encodedPayload, root));
        }

        return new JsonWebSignature(encodedPayload, payload, signatures);
    }

    /// <summary>
    /// Determines whether any signature whose key identifier matches the specified key is valid under it.
    /// </summary>
    /// <param name="publicKey">
    /// The public key. If it has a <c>kid</c>, only signatures with the same <c>kid</c> are tried; otherwise every signature is tried.
    /// </param>
    /// <returns>Returns <see langword="true"/> if a matching signature is valid; otherwise, <see langword="false"/>.</returns>
    public bool Verify(JsonWebKey publicKey)
    {
        ArgumentNullException.ThrowIfNull(publicKey);

        return Signatures
            .Where(signature => publicKey.KeyId is null || signature.KeyId == publicKey.KeyId)
            .Any(signature => signature.Verify(publicKey));
    }

    /// <summary>
    /// Returns the General JWS JSON serialization (RFC 7515 §7.2.1).
    /// </summary>
    /// <returns>Returns compact JSON text.</returns>
    public string ToJson()
    {
        JsonArray signatures = [];
        foreach (JwsSignature signature in Signatures) signatures.Add(WriteSignature(signature, []));

        return JoseJson.Write(new JsonObject { ["payload"] = encodedPayload, ["signatures"] = signatures });
    }

    /// <summary>
    /// Returns the Flattened JWS JSON serialization (RFC 7515 §7.2.2).
    /// </summary>
    /// <returns>Returns compact JSON text.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the current JWS does not have exactly one signature.</exception>
    public string ToFlattenedJson()
    {
        if (Signatures.Count != 1)
            throw new InvalidOperationException("Only a JWS with exactly one signature has a flattened serialization.");

        return JoseJson.Write(WriteSignature(Signatures[0], new JsonObject { ["payload"] = encodedPayload }));
    }

    /// <inheritdoc/>
    public override string ToString() => ToJson();

    /// <summary>
    /// Reads one signature object, with its <c>protected</c>, <c>header</c> and <c>signature</c> members.
    /// </summary>
    /// <param name="encodedPayload">The base64url-encoded payload.</param>
    /// <param name="entry">The JSON object holding the signature.</param>
    /// <returns>Returns a new <see cref="JwsSignature"/>.</returns>
    /// <exception cref="CryptographicException">Thrown when the signature object is malformed.</exception>
    private static JwsSignature ReadSignature(string encodedPayload, JsonElement entry)
    {
        if (entry.ValueKind != JsonValueKind.Object) throw new CryptographicException("A JWS signature must be a JSON object.");

        string encodedHeader = JoseJson.GetRequiredString(entry, "protected");
        byte[] headerBytes = JoseJson.GetRequiredBytes(entry, "protected");
        JsonElement protectedHeader = JoseJson.ParseObject(headerBytes, "JWS protected header");
        JsonElement? header = JoseJson.GetOptionalObject(entry, "header")?.Clone();
        byte[] signature = JoseJson.GetRequiredBytes(entry, "signature");

        return new JwsSignature(encodedPayload, encodedHeader, protectedHeader, header, signature);
    }

    /// <summary>
    /// Writes the members of one signature to the specified JSON object.
    /// </summary>
    /// <param name="signature">The signature to write.</param>
    /// <param name="target">The JSON object to write to.</param>
    /// <returns>Returns <paramref name="target"/>.</returns>
    private static JsonObject WriteSignature(JwsSignature signature, JsonObject target)
    {
        target["protected"] = signature.EncodedProtectedHeader;
        if (signature.Header is { } header) target["header"] = JsonNode.Parse(header.GetRawText());
        target["signature"] = Base64Url.Encode(signature.Signature);
        return target;
    }
}
