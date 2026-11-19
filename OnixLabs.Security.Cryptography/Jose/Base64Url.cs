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
using System.Buffers;

namespace OnixLabs.Security.Cryptography.Jose;

/// <summary>
/// Represents the unpadded base64url encoding that JOSE uses for every binary value, as RFC 7515 §2 and Appendix C define it.
/// </summary>
/// <remarks>
/// Why: <c>System.Buffers.Text.Base64Url</c> only exists from .NET 9, and this library targets .NET 8; its decoder also accepts padding and
/// whitespace. Decoding here is strict: padding, whitespace, and characters outside the base64url alphabet are rejected, so that each value
/// has exactly one accepted encoding. Both directions use the platform's base64 code and vectorized span operations.
/// </remarks>
public static class Base64Url
{
    /// <summary>
    /// The 64 characters of the base64url alphabet (RFC 4648 §5).
    /// </summary>
    private static readonly SearchValues<char> Alphabet =
        SearchValues.Create("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789-_");

    /// <summary>
    /// The largest encoded length, in characters, decoded into a stack buffer rather than a pooled one.
    /// </summary>
    private const int MaxStackLength = 512;

    /// <summary>
    /// Encodes the specified bytes as unpadded base64url.
    /// </summary>
    /// <param name="data">The bytes to encode.</param>
    /// <returns>Returns the unpadded base64url encoding of <paramref name="data"/>.</returns>
    public static string Encode(ReadOnlySpan<byte> data)
    {
        if (data.IsEmpty) return string.Empty;

        int paddedLength = ((data.Length + 2) / 3) * 4;
        int length = paddedLength - ((3 - (data.Length % 3)) % 3);
        char[]? rented = paddedLength > MaxStackLength ? ArrayPool<char>.Shared.Rent(paddedLength) : null;
        Span<char> buffer = rented ?? stackalloc char[MaxStackLength];

        try
        {
            Convert.TryToBase64Chars(data, buffer, out int _);
            Span<char> result = buffer[..length];
            result.Replace('+', '-');
            result.Replace('/', '_');
            return new string(result);
        }
        finally
        {
            if (rented is not null) ArrayPool<char>.Shared.Return(rented);
        }
    }

    /// <summary>
    /// Decodes the specified unpadded base64url text.
    /// </summary>
    /// <param name="value">The unpadded base64url text to decode.</param>
    /// <returns>Returns the decoded bytes.</returns>
    /// <exception cref="FormatException">Thrown when <paramref name="value"/> is not valid unpadded base64url.</exception>
    public static byte[] Decode(ReadOnlySpan<char> value) =>
        TryDecode(value, out byte[] result) ? result : throw new FormatException("The value is not valid unpadded base64url.");

    /// <summary>
    /// Attempts to decode the specified unpadded base64url text.
    /// </summary>
    /// <param name="value">The unpadded base64url text to decode.</param>
    /// <param name="result">When this method returns, contains the decoded bytes, or an empty array on failure.</param>
    /// <returns>Returns <see langword="true"/> if <paramref name="value"/> is valid unpadded base64url; otherwise,
    /// <see langword="false"/>.</returns>
    public static bool TryDecode(ReadOnlySpan<char> value, out byte[] result)
    {
        result = [];

        if (value.Length % 4 == 1 || value.ContainsAnyExcept(Alphabet)) return false;
        if (value.IsEmpty) return true;

        int padding = (4 - (value.Length % 4)) % 4;
        int paddedLength = value.Length + padding;
        char[]? rented = paddedLength > MaxStackLength ? ArrayPool<char>.Shared.Rent(paddedLength) : null;
        Span<char> buffer = (rented ?? stackalloc char[MaxStackLength])[..paddedLength];

        try
        {
            value.CopyTo(buffer);
            buffer.Replace('-', '+');
            buffer.Replace('_', '/');
            buffer[value.Length..].Fill('=');

            byte[] decoded = new byte[(paddedLength / 4 * 3) - padding];

            if (!Convert.TryFromBase64Chars(buffer, decoded, out int written) || written != decoded.Length) return false;

            result = decoded;
            return true;
        }
        finally
        {
            if (rented is not null) ArrayPool<char>.Shared.Return(rented);
        }
    }
}
