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
    private static readonly SearchValues<char> Base16UppercaseValues = SearchValues.Create("ABCDEF");
    private static readonly SearchValues<char> Base16LowercaseValues = SearchValues.Create("abcdef");

    /// <summary>
    /// Encodes the specified <see cref="ReadOnlySpan{T}"/> value into a Base-16 <see cref="String"/> representation.
    /// </summary>
    /// <param name="value">The value to encode into a Base-16 <see cref="String"/> representation.</param>
    /// <param name="provider">The format provider that will be used to encode the specified value.</param>
    /// <returns>Returns a new Base-16 <see cref="String"/> representation encoded from the specified value.</returns>
    public string Encode(ReadOnlySpan<byte> value, IFormatProvider? provider = null) => TryEncode(value, provider, out string result)
        ? result
        : throw new FormatException(IBaseCodec.EncodingFormatException);

    /// <summary>
    /// Decodes the specified <see cref="ReadOnlySpan{T}"/> Base-16 representation into a <see cref="byte"/> array.
    /// </summary>
    /// <param name="value">The Base-16 value to decode into a <see cref="byte"/> array.</param>
    /// <param name="provider">The format provider that will be used to decode the specified value.</param>
    /// <returns>Returns a new <see cref="byte"/> array decoded from the specified value.</returns>
    public byte[] Decode(ReadOnlySpan<char> value, IFormatProvider? provider = null) => TryDecode(value, provider, out byte[] result)
        ? result
        : throw new FormatException(IBaseCodec.DecodingFormatException);

    /// <summary>
    /// Tries to encode the specified <see cref="ReadOnlySpan{T}"/> value into a Base-16 <see cref="String"/> representation.
    /// </summary>
    /// <param name="value">The value to encode into a Base-16 <see cref="String"/> representation.</param>
    /// <param name="provider">The format provider that will be used to encode the specified value.</param>
    /// <param name="result">
    /// A new Base-16 <see cref="String"/> representation encoded from the specified value,
    /// or an empty string if the specified value could not be encoded.
    /// </param>
    /// <returns>Returns <see langword="true"/> if the specified value was encoded successfully; otherwise, <see langword="false"/>.</returns>
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

            result = formatProvider == Base16FormatProvider.Uppercase
                ? Convert.ToHexString(value)
                : Convert.ToHexString(value).ToLower();

            return true;
        }
        catch
        {
            result = string.Empty;
            return false;
        }
    }

    /// <summary>
    /// Tries to decode the specified <see cref="ReadOnlySpan{T}"/> Base-16 representation into a <see cref="byte"/> array.
    /// </summary>
    /// <param name="value">The Base-16 value to decode into a <see cref="byte"/> array.</param>
    /// <param name="provider">The format provider that will be used to decode the specified value.</param>
    /// <param name="result">
    /// A new <see cref="byte"/> array decoded from the specified value,
    /// or an empty <see cref="byte"/> array if the specified value could not be decoded.
    /// </param>
    /// <returns>Returns <see langword="true"/> if the specified value was decoded successfully; otherwise, <see langword="false"/>.</returns>
    public bool TryDecode(ReadOnlySpan<char> value, IFormatProvider? provider, out byte[] result)
    {
        try
        {
            if (value.IsEmpty)
            {
                result = [];
                return true;
            }

            // ReSharper disable DuplicatedSequentialIfBodies
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

            result = Convert.FromHexString(value);
            return true;
        }
        catch
        {
            result = [];
            return false;
        }
    }
}
