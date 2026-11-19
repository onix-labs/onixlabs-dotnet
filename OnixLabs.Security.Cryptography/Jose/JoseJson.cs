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
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Nodes;
using OnixLabs.Core.Text;

namespace OnixLabs.Security.Cryptography.Jose;

/// <summary>
/// Represents the JSON reading and writing conventions shared by the JOSE types.
/// </summary>
internal static class JoseJson
{
    /// <summary>
    /// The options with which JOSE JSON is written: compact, and without escaping characters such as <c>+</c> that need none.
    /// </summary>
    /// <remarks>
    /// Why: the default encoder writes <c>+</c> as <c>+</c>. That is valid JSON, but headers such as <c>"ECDH-1PU+A128KW"</c> would
    /// then differ, byte for byte, from those of other implementations and of the published examples.
    /// </remarks>
    public static readonly JsonSerializerOptions WriteOptions = new()
    {
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
        WriteIndented = false
    };

    /// <summary>
    /// Parses the specified text as a JSON object.
    /// </summary>
    /// <param name="json">The JSON text to parse.</param>
    /// <param name="description">A description of the object, used in exception messages.</param>
    /// <returns>Returns the root element, cloned so that it outlives the document.</returns>
    /// <exception cref="CryptographicException">Thrown when the text is not valid JSON, or is not an object.</exception>
    public static JsonElement ParseObject(ReadOnlySpan<byte> json, string description)
    {
        try
        {
            JsonReaderOptions options = new() { AllowTrailingCommas = false, CommentHandling = JsonCommentHandling.Disallow };
            Utf8JsonReader reader = new(json, options);
            using JsonDocument document = JsonDocument.ParseValue(ref reader);

            return document.RootElement.ValueKind == JsonValueKind.Object
                ? document.RootElement.Clone()
                : throw new CryptographicException($"The {description} is not a JSON object.");
        }
        catch (JsonException exception)
        {
            throw new CryptographicException($"The {description} is not valid JSON.", exception);
        }
    }

    /// <summary>
    /// Parses the specified text as a JSON object.
    /// </summary>
    /// <param name="json">The JSON text to parse.</param>
    /// <param name="description">A description of the object, used in exception messages.</param>
    /// <returns>Returns the root element, cloned so that it outlives the document.</returns>
    /// <exception cref="CryptographicException">Thrown when the text is not valid JSON, or is not an object.</exception>
    public static JsonElement ParseObject(
        string json,
        string description) => ParseObject(System.Text.Encoding.UTF8.GetBytes(json), description);

    /// <summary>
    /// Gets the specified string member of the specified object, if it is present.
    /// </summary>
    /// <param name="element">The JSON object.</param>
    /// <param name="name">The member name.</param>
    /// <returns>Returns the string value, or <see langword="null"/> if the member is absent.</returns>
    /// <exception cref="CryptographicException">Thrown when the member is present but is not a string.</exception>
    public static string? GetOptionalString(JsonElement element, string name)
    {
        if (!element.TryGetProperty(name, out JsonElement value)) return null;

        return value.ValueKind == JsonValueKind.String
            ? value.GetString()
            : throw new CryptographicException($"The JOSE member '{name}' must be a string.");
    }

    /// <summary>
    /// Gets the specified string member of the specified object.
    /// </summary>
    /// <param name="element">The JSON object.</param>
    /// <param name="name">The member name.</param>
    /// <returns>Returns the string value.</returns>
    /// <exception cref="CryptographicException">Thrown when the member is absent or is not a string.</exception>
    public static string GetRequiredString(JsonElement element, string name) =>
        GetOptionalString(element, name) ?? throw new CryptographicException($"The JOSE member '{name}' is required.");

    /// <summary>
    /// Encodes the specified bytes as unpadded base64url, as JOSE writes every binary value (RFC 7515 §2).
    /// </summary>
    /// <param name="value">The bytes to encode.</param>
    /// <returns>Returns the unpadded base64url encoding of <paramref name="value"/>.</returns>
    public static string EncodeBytes(ReadOnlySpan<byte> value) => IBaseCodec.Base64.Encode(value, Base64FormatProvider.Url);

    /// <summary>
    /// Attempts to decode the specified unpadded base64url text, strictly: padding, whitespace, characters outside the alphabet, and unused
    /// bits that are not zero are all rejected.
    /// </summary>
    /// <param name="value">The text to decode.</param>
    /// <param name="result">When this method returns, contains the decoded bytes, or an empty array on failure.</param>
    /// <returns>Returns <see langword="true"/> if <paramref name="value"/> is valid unpadded base64url; otherwise,
    /// <see langword="false"/>.</returns>
    public static bool TryDecodeBytes(ReadOnlySpan<char> value, out byte[] result) =>
        IBaseCodec.Base64.TryDecode(value, Base64FormatProvider.Url, out result);

    /// <summary>
    /// Gets and decodes the specified base64url string member of the specified object, if it is present.
    /// </summary>
    /// <param name="element">The JSON object.</param>
    /// <param name="name">The member name.</param>
    /// <returns>Returns the decoded bytes, or <see langword="null"/> if the member is absent.</returns>
    /// <exception cref="CryptographicException">Thrown when the member is present but is not a base64url string.</exception>
    public static byte[]? GetOptionalBytes(JsonElement element, string name)
    {
        string? value = GetOptionalString(element, name);

        if (value is null) return null;

        return TryDecodeBytes(value, out byte[] bytes)
            ? bytes
            : throw new CryptographicException($"The JOSE member '{name}' is not valid base64url.");
    }

    /// <summary>
    /// Gets and decodes the specified base64url string member of the specified object.
    /// </summary>
    /// <param name="element">The JSON object.</param>
    /// <param name="name">The member name.</param>
    /// <returns>Returns the decoded bytes.</returns>
    /// <exception cref="CryptographicException">Thrown when the member is absent or is not a base64url string.</exception>
    public static byte[] GetRequiredBytes(JsonElement element, string name) =>
        GetOptionalBytes(element, name) ?? throw new CryptographicException($"The JOSE member '{name}' is required.");

    /// <summary>
    /// Gets the specified object member of the specified object, if it is present.
    /// </summary>
    /// <param name="element">The JSON object.</param>
    /// <param name="name">The member name.</param>
    /// <returns>Returns the member, or <see langword="null"/> if it is absent.</returns>
    /// <exception cref="CryptographicException">Thrown when the member is present but is not an object.</exception>
    public static JsonElement? GetOptionalObject(JsonElement element, string name)
    {
        if (!element.TryGetProperty(name, out JsonElement value)) return null;

        return value.ValueKind == JsonValueKind.Object
            ? value
            : throw new CryptographicException($"The JOSE member '{name}' must be a JSON object.");
    }

    /// <summary>
    /// Checks that no member name appears in more than one of the specified header objects, as RFC 7515 §7.2.1 and RFC 7516 §7.2.1 require.
    /// </summary>
    /// <param name="headers">The header objects that together make up a JOSE header; absent headers are skipped.</param>
    /// <exception cref="CryptographicException">Thrown when a member name appears in more than one header.</exception>
    public static void RequireDisjoint(params JsonElement?[] headers)
    {
        System.Collections.Generic.HashSet<string> names = new(StringComparer.Ordinal);

        foreach (JsonElement? header in headers)
        {
            if (header is null) continue;

            foreach (JsonProperty property in header.Value.EnumerateObject())
                if (!names.Add(property.Name))
                    throw new CryptographicException($"The JOSE header member '{property.Name}' appears more than once.");
        }
    }

    /// <summary>
    /// Gets the first occurrence of the specified string member across the specified header objects.
    /// </summary>
    /// <param name="name">The member name.</param>
    /// <param name="headers">The header objects, which must be disjoint; absent headers are skipped.</param>
    /// <returns>Returns the string value, or <see langword="null"/> if no header has the member.</returns>
    /// <exception cref="CryptographicException">Thrown when the member is present but is not a string.</exception>
    public static string? FindString(string name, params JsonElement?[] headers)
    {
        foreach (JsonElement? header in headers)
            if (header is not null && GetOptionalString(header.Value, name) is { } value)
                return value;

        return null;
    }

    /// <summary>
    /// Writes the specified JSON node as compact UTF-8 text.
    /// </summary>
    /// <param name="node">The node to write.</param>
    /// <returns>Returns the compact JSON text.</returns>
    public static string Write(JsonNode node) => node.ToJsonString(WriteOptions);
}
