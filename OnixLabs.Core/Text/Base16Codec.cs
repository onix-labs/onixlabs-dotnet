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
/// Represents a codec for encoding and decoding Base-16 values.
/// </summary>
// ReSharper disable StringLiteralTypo
public sealed class Base16Codec : IBaseCodec
{
    /// <summary>
    /// The set of uppercase Base-16 alphabetic characters, used to reject input that violates a lowercase format provider.
    /// </summary>
    private static readonly SearchValues<char> Base16UppercaseValues = SearchValues.Create("ABCDEF");

    /// <summary>
    /// The set of lowercase Base-16 alphabetic characters, used to reject input that violates an uppercase format provider.
    /// </summary>
    private static readonly SearchValues<char> Base16LowercaseValues = SearchValues.Create("abcdef");

    /// <inheritdoc/>
    public string Encode(ReadOnlySpan<byte> value, IFormatProvider? provider = null) =>
        TryEncode(value, provider, out string result) ? result : throw new FormatException(IBaseCodec.EncodingFormatException);

    /// <inheritdoc/>
    public byte[] Decode(ReadOnlySpan<char> value, IFormatProvider? provider = null) =>
        TryDecode(value, provider, out byte[] result) ? result : throw new FormatException(IBaseCodec.DecodingFormatException);

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

            if (!IBaseCodec.TryGetFormatProvider(provider, Base16FormatProvider.Invariant, out Base16FormatProvider formatProvider))
            {
                result = string.Empty;
                return false;
            }

            result = formatProvider == Base16FormatProvider.Uppercase ? Convert.ToHexString(value) : ToLowercaseHexString(value);

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

            if (!IBaseCodec.TryGetFormatProvider(provider, Base16FormatProvider.Invariant, out Base16FormatProvider formatProvider))
            {
                result = [];
                return false;
            }

            if (formatProvider == Base16FormatProvider.Uppercase && value.ContainsAny(Base16LowercaseValues))
            {
                result = [];
                return false;
            }

            if (formatProvider == Base16FormatProvider.Lowercase && value.ContainsAny(Base16UppercaseValues))
            {
                result = [];
                return false;
            }

#if NET9_0_OR_GREATER
            // Why: .NET 9 added a span-based hex decoder that reports malformed input with a status instead of an exception, in a single
            // pass. A separate validation pass made successful decodes measurably slower, so .NET 8 keeps the original code.
            byte[] bytes = new byte[value.Length / 2];

            if (value.Length % 2 != 0 || Convert.FromHexString(value, bytes, out int _, out int _) != OperationStatus.Done)
            {
                result = [];
                return false;
            }

            result = bytes;
#else
            result = Convert.FromHexString(value);
#endif
            return true;
        }
        catch
        {
            result = [];
            return false;
        }
    }

    /// <summary>
    /// Tries to format the specified value as Base-16 into the specified destination, as <see cref="ISpanFormattable"/> requires.
    /// </summary>
    /// <param name="value">The value to encode.</param>
    /// <param name="provider">The format provider, which must be a <see cref="Base16FormatProvider"/>, a culture, or <see langword="null"/>.</param>
    /// <param name="destination">The destination for the encoded characters.</param>
    /// <param name="charsWritten">When this method returns, contains the number of characters written to <paramref name="destination"/>.</param>
    /// <returns>Returns <see langword="true"/> if the encoding fits in <paramref name="destination"/>; otherwise, <see langword="false"/>.</returns>
    /// <exception cref="FormatException">Thrown when <paramref name="provider"/> is not a supported format provider.</exception>
    internal static bool TryFormat(ReadOnlySpan<byte> value, IFormatProvider? provider, Span<char> destination, out int charsWritten)
    {
        if (!IBaseCodec.TryGetFormatProvider(provider, Base16FormatProvider.Invariant, out Base16FormatProvider formatProvider))
            throw new FormatException(IBaseCodec.EncodingFormatException);

        if (destination.Length < value.Length * 2)
        {
            charsWritten = 0;
            return false;
        }

#if NET9_0_OR_GREATER
        // Why: .NET 9 added span-based hex formatting, which writes straight into the destination without allocating.
        return formatProvider == Base16FormatProvider.Uppercase
            ? Convert.TryToHexString(value, destination, out charsWritten)
            : Convert.TryToHexStringLower(value, destination, out charsWritten);
#else
        // Why: .NET 8 has no span-based hex formatting, and a portable loop measured slower than encoding to a string and copying it.
        string encoded = formatProvider == Base16FormatProvider.Uppercase ? Convert.ToHexString(value) : ToLowercaseHexString(value);
        encoded.CopyTo(destination);
        charsWritten = encoded.Length;
        return true;
#endif
    }

    /// <summary>
    /// Converts the specified bytes to a lowercase hexadecimal string.
    /// </summary>
    /// <param name="value">The bytes to convert.</param>
    /// <returns>Returns the lowercase hexadecimal representation of the specified bytes.</returns>
    private static string ToLowercaseHexString(ReadOnlySpan<byte> value)
    {
#if NET9_0_OR_GREATER
        // Why: .NET 9 added vectorised lowercase hex, which builds the string in one pass and one allocation.
        return Convert.ToHexStringLower(value);
#else
        // Why: on .NET 8, two vectorised passes measured faster than any portable single-allocation loop.
        return Convert.ToHexString(value).ToLowerInvariant();
#endif
    }
}
