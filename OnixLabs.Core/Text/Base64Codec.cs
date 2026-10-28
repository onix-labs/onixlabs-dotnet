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

            if (!IBaseCodec.TryGetFormatProvider(provider, Base64FormatProvider.Rfc4648, out Base64FormatProvider _))
            {
                result = string.Empty;
                return false;
            }

            result = Convert.ToBase64String(value);
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

            if (!IBaseCodec.TryGetFormatProvider(provider, Base64FormatProvider.Rfc4648, out Base64FormatProvider _))
            {
                result = [];
                return false;
            }

            return TryDecodeCore(value, out result);
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
        if (!IBaseCodec.TryGetFormatProvider(provider, Base64FormatProvider.Rfc4648, out Base64FormatProvider _))
            throw new FormatException(IBaseCodec.EncodingFormatException);

        return Convert.TryToBase64Chars(value, destination, out charsWritten);
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
}
