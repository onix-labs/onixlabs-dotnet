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

namespace OnixLabs.Core.Text;

/// <summary>
/// Represents a codec for encoding and decoding Base-64 values.
/// </summary>
public sealed class Base64Codec : IBaseCodec
{
    /// <inheritdoc/>
    public string Encode(ReadOnlySpan<byte> value, IFormatProvider? provider = null) => TryEncode(value, provider, out string result)
        ? result
        : throw new FormatException(IBaseCodec.EncodingFormatException);

    /// <inheritdoc/>
    public byte[] Decode(ReadOnlySpan<char> value, IFormatProvider? provider = null) => TryDecode(value, provider, out byte[] result)
        ? result
        : throw new FormatException(IBaseCodec.DecodingFormatException);

    /// <inheritdoc/>
    public bool TryEncode(ReadOnlySpan<byte> value, IFormatProvider? provider, out string result)
    {
        try
        {
            if (value.IsEmpty)
            {
                result = string.Empty;
                return true;
            }

            if (!IBaseCodec.TryGetFormatProvider(provider, Base64FormatProvider.Rfc4648, out Base64FormatProvider format))
            {
                result = string.Empty;
                return false;
            }

            result = format == Base64FormatProvider.Url ? EncodeUrl(value) : Convert.ToBase64String(value);
            return true;
        }
        catch
        {
            result = string.Empty;
            return false;
        }
    }

    /// <inheritdoc/>
    public bool TryDecode(ReadOnlySpan<char> value, IFormatProvider? provider, out byte[] result)
    {
        try
        {
            if (value.IsEmpty)
            {
                result = [];
                return true;
            }

            if (!IBaseCodec.TryGetFormatProvider(provider, Base64FormatProvider.Rfc4648, out Base64FormatProvider format))
            {
                result = [];
                return false;
            }

            return format == Base64FormatProvider.Url ? TryDecodeUrl(value, out result) : TryDecodeCore(value, out result);
        }
        catch
        {
            result = [];
            return false;
        }
    }

    /// <summary>
    /// Tries to format the specified value as Base-64 into the specified destination, as <see cref="ISpanFormattable"/> requires.
    /// </summary>
    /// <param name="value">The value to encode.</param>
    /// <param name="provider">The format provider, which must be a <see cref="Base64FormatProvider"/>, a culture, or <see langword="null"/>.</param>
    /// <param name="destination">The destination for the encoded characters.</param>
    /// <param name="charsWritten">When this method returns, contains the number of characters written to <paramref name="destination"/>.</param>
    /// <returns>Returns <see langword="true"/> if the encoding fits in <paramref name="destination"/>; otherwise, <see langword="false"/>.</returns>
    /// <exception cref="FormatException">Thrown when <paramref name="provider"/> is not a supported format provider.</exception>
    internal static bool TryFormat(ReadOnlySpan<byte> value, IFormatProvider? provider, Span<char> destination, out int charsWritten)
    {
        if (!IBaseCodec.TryGetFormatProvider(provider, Base64FormatProvider.Rfc4648, out Base64FormatProvider format))
            throw new FormatException(IBaseCodec.EncodingFormatException);

        return format == Base64FormatProvider.Url
            ? TryFormatUrl(value, destination, out charsWritten)
            : Convert.TryToBase64Chars(value, destination, out charsWritten);
    }

    /// <summary>
    /// Decodes the specified Base-64 characters with the same rules as <see cref="Convert.FromBase64String"/>, which ignores whitespace.
    /// </summary>
    /// <param name="value">The characters to decode.</param>
    /// <param name="result">When this method returns, contains the decoded bytes, or an empty array if decoding failed.</param>
    /// <returns>Returns <see langword="true"/> if <paramref name="value"/> is valid Base-64; otherwise, <see langword="false"/>.</returns>
    /// <remarks>
    /// Why: <see cref="Convert.FromBase64String"/> needs a string copy of the input and reports invalid input by throwing.
    /// <see cref="Convert.TryFromBase64Chars"/> applies the same rules to a span and reports failure with a return value.
    /// </remarks>
    private static bool TryDecodeCore(ReadOnlySpan<char> value, out byte[] result)
    {
        const int maxStackSize = 256;

        int maxLength = (value.Length + 3) / 4 * 3;
        byte[]? rented = maxLength > maxStackSize ? ArrayPool<byte>.Shared.Rent(maxLength) : null;
        Span<byte> buffer = rented is null ? stackalloc byte[maxStackSize] : rented;

        try
        {
            if (!Convert.TryFromBase64Chars(value, buffer, out int bytesWritten))
            {
                result = [];
                return false;
            }

            result = buffer[..bytesWritten].ToArray();
            return true;
        }
        finally
        {
            // Why: the decoded value may be key material.
            buffer.Clear();
            if (rented is not null) ArrayPool<byte>.Shared.Return(rented);
        }
    }

    /// <summary>
    /// The 64 characters of the base64url alphabet of RFC 4648 §5.
    /// </summary>
    private static readonly SearchValues<char> UrlAlphabet = SearchValues.Create(Base64FormatProvider.Url.Alphabet);

    /// <summary>
    /// Gets the length, in characters, of the unpadded base64url encoding of the specified number of bytes.
    /// </summary>
    /// <param name="length">The number of bytes to encode.</param>
    /// <returns>Returns the length of the unpadded encoding.</returns>
    private static int GetUrlEncodedLength(int length) => checked((length / 3 * 4) + ((length % 3 * 4) + 2) / 3);

    /// <summary>
    /// Encodes the specified value as unpadded base64url.
    /// </summary>
    /// <param name="value">The value to encode.</param>
    /// <returns>Returns the unpadded base64url encoding of <paramref name="value"/>.</returns>
    private static string EncodeUrl(ReadOnlySpan<byte> value)
    {
        const int maxStackSize = 512;

        int length = GetUrlEncodedLength(value.Length);
        char[]? rented = length > maxStackSize ? ArrayPool<char>.Shared.Rent(length) : null;
        Span<char> buffer = (rented ?? stackalloc char[maxStackSize])[..length];

        try
        {
            TryFormatUrl(value, buffer, out int _);
            return new string(buffer);
        }
        finally
        {
            if (rented is not null) ArrayPool<char>.Shared.Return(rented);
        }
    }

    /// <summary>
    /// Tries to encode the specified value as unpadded base64url into the specified destination.
    /// </summary>
    /// <param name="value">The value to encode.</param>
    /// <param name="destination">The destination for the encoded characters.</param>
    /// <param name="charsWritten">When this method returns, contains the number of characters written to <paramref name="destination"/>.</param>
    /// <returns>Returns <see langword="true"/> if the encoding fits in <paramref name="destination"/>; otherwise, <see langword="false"/>.</returns>
    /// <remarks>
    /// Why: .NET 9 and later have a vectorized base64url encoder; on .NET 8, the platform's base64 encoder is used and its two
    /// alphabet-specific characters replaced, which is also vectorized.
    /// </remarks>
    private static bool TryFormatUrl(ReadOnlySpan<byte> value, Span<char> destination, out int charsWritten)
    {
        int length = GetUrlEncodedLength(value.Length);
        charsWritten = 0;

        // Why: .NET's encoder reports a partial write when the destination is too short; the other Base-N codecs report none.
        if (destination.Length < length) return false;

#if NET9_0_OR_GREATER
        return System.Buffers.Text.Base64Url.TryEncodeToChars(value, destination, out charsWritten);
#else
        const int maxStackSize = 512;

        int paddedLength = (value.Length + 2) / 3 * 4;
        char[]? rented = paddedLength > maxStackSize ? ArrayPool<char>.Shared.Rent(paddedLength) : null;
        Span<char> buffer = rented ?? stackalloc char[maxStackSize];

        try
        {
            Convert.TryToBase64Chars(value, buffer, out int _);
            Span<char> encoded = destination[..length];
            buffer[..length].CopyTo(encoded);
            encoded.Replace('+', '-');
            encoded.Replace('/', '_');
            charsWritten = length;
            return true;
        }
        finally
        {
            if (rented is not null) ArrayPool<char>.Shared.Return(rented);
        }
#endif
    }

    /// <summary>
    /// Decodes the specified unpadded base64url characters strictly.
    /// </summary>
    /// <param name="value">The characters to decode.</param>
    /// <param name="result">When this method returns, contains the decoded bytes, or an empty array if decoding failed.</param>
    /// <returns>Returns <see langword="true"/> if <paramref name="value"/> is valid, canonical, unpadded base64url; otherwise,
    /// <see langword="false"/>.</returns>
    /// <remarks>
    /// Why: <c>System.Buffers.Text.Base64Url</c> accepts padding and whitespace, and the platform's decoders ignore unused bits. Rejecting
    /// all three first means each value has exactly one accepted encoding.
    /// </remarks>
    private static bool TryDecodeUrl(ReadOnlySpan<char> value, out byte[] result)
    {
        result = [];

        if (value.Length % 4 == 1 || value.ContainsAnyExcept(UrlAlphabet) || !HasZeroUnusedBits(value)) return false;

        int remainder = value.Length % 4;
        int length = (value.Length / 4 * 3) + (remainder == 0 ? 0 : remainder - 1);
        byte[] decoded = new byte[length];

#if NET9_0_OR_GREATER
        if (!System.Buffers.Text.Base64Url.TryDecodeFromChars(value, decoded, out int written) || written != length) return false;
#else
        const int maxStackSize = 512;

        int paddedLength = value.Length + ((4 - remainder) % 4);
        char[]? rented = paddedLength > maxStackSize ? ArrayPool<char>.Shared.Rent(paddedLength) : null;
        Span<char> buffer = (rented ?? stackalloc char[maxStackSize])[..paddedLength];

        try
        {
            value.CopyTo(buffer);
            buffer.Replace('-', '+');
            buffer.Replace('_', '/');
            buffer[value.Length..].Fill('=');

            if (!Convert.TryFromBase64Chars(buffer, decoded, out int written) || written != length) return false;
        }
        finally
        {
            if (rented is not null) ArrayPool<char>.Shared.Return(rented);
        }
#endif

        result = decoded;
        return true;
    }

    /// <summary>
    /// Determines whether the unused low bits of the last character of the specified unpadded base64url value are zero.
    /// </summary>
    /// <param name="value">The unpadded base64url value, whose characters are all in the alphabet.</param>
    /// <returns>Returns <see langword="true"/> if the unused bits are zero, or there are none; otherwise,
    /// <see langword="false"/>.</returns>
    private static bool HasZeroUnusedBits(ReadOnlySpan<char> value)
    {
        int unusedBitsMask = (value.Length % 4) switch { 2 => 0b1111, 3 => 0b11, _ => 0 };

        return unusedBitsMask == 0 || (Base64FormatProvider.Url.Alphabet.IndexOf(value[^1]) & unusedBitsMask) == 0;
    }
}
