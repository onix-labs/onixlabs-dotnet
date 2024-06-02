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

namespace OnixLabs.Core.Text;

/// <summary>
/// Defines a codec for encoding and decoding Base-N values.
/// </summary>
public interface IBaseCodec
{
    /// <summary>
    /// The exception message to throw for encoding operations.
    /// </summary>
    protected const string EncodingFormatException = "The specified value was not in the correct format and could not be encoded.";

    /// <summary>
    /// The exception message to throw for decoding operations.
    /// </summary>
    protected const string DecodingFormatException = "The specified value was not in the correct format and could not be decoded.";

    /// <summary>
    /// Gets a new <see cref="Base16Codec"/> instance.
    /// </summary>
    public static Base16Codec Base16 => new();

    /// <summary>
    /// Gets a new <see cref="Base32Codec"/> instance.
    /// </summary>
    public static Base32Codec Base32 => new();

    /// <summary>
    /// Gets a new <see cref="Base58Codec"/> instance.
    /// </summary>
    public static Base58Codec Base58 => new();

    /// <summary>
    /// Gets a new <see cref="Base64Codec"/> instance.
    /// </summary>
    public static Base64Codec Base64 => new();

    /// <summary>
    /// Obtains a base <see cref="string"/> representation of the specified <see cref="ReadOnlySpan{Byte}"/> value.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <param name="provider">
    /// The <see cref="IFormatProvider"/> which will be used to obtain the base representation.
    /// <remarks> Allowed values for this parameter are <see cref="Base16FormatProvider"/>, <see cref="Base32FormatProvider"/>, <see cref="Base58FormatProvider"/>, and <see cref="Base64FormatProvider"/>.</remarks>
    /// </param>
    /// <returns>Returns a base <see cref="string"/> representation of the specified <see cref="ReadOnlySpan{Byte}"/> value.</returns>
    /// <exception cref="FormatException">If the specified <see cref="ReadOnlySpan{Byte}"/> value cannot be converted.</exception>
    public static string GetString(ReadOnlySpan<byte> value, IFormatProvider provider)
    {
        if (TryGetString(value, provider, out string result)) return result;
        throw new FormatException(EncodingFormatException);
    }

    /// <summary>
    /// Obtains a new <see cref="T:byte[]"/> array by converting the specified <see cref="ReadOnlySpan{Char}"/> value.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <param name="provider">
    /// The <see cref="IFormatProvider"/> which will be used to convert the specified <see cref="ReadOnlySpan{Char}"/> value.
    /// <remarks> Allowed values for this parameter are <see cref="Base16FormatProvider"/>, <see cref="Base32FormatProvider"/>, <see cref="Base58FormatProvider"/>, and <see cref="Base64FormatProvider"/>.</remarks>
    /// </param>
    /// <returns>Returns a new <see cref="T:byte[]"/> array by converting the specified <see cref="ReadOnlySpan{Char}"/> value.</returns>
    /// <exception cref="FormatException">If the specified <see cref="ReadOnlySpan{Char}"/> value cannot be converted.</exception>
    public static byte[] GetBytes(ReadOnlySpan<char> value, IFormatProvider provider)
    {
        if (TryGetBytes(value, provider, out byte[] result)) return result;
        throw new FormatException(DecodingFormatException);
    }

    /// <summary>
    /// Tries to obtain a base <see cref="string"/> representation of the specified <see cref="ReadOnlySpan{Byte}"/> value.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <param name="provider">
    /// The <see cref="IFormatProvider"/> which will be used to obtain the base representation.
    /// <remarks> Allowed values for this parameter are <see cref="Base16FormatProvider"/>, <see cref="Base32FormatProvider"/>, <see cref="Base58FormatProvider"/>, and <see cref="Base64FormatProvider"/>.</remarks>
    /// </param>
    /// <param name="result"> A base <see cref="string"/> representation of the specified <see cref="ReadOnlySpan{Byte}"/> value, or an empty string if the value cannot be converted.</param>
    /// <returns>Returns <see langword="true"/> if the conversion was successful; otherwise, <see langword="false"/>.</returns>
    public static bool TryGetString(ReadOnlySpan<byte> value, IFormatProvider provider, out string result)
    {
        result = string.Empty;

        return provider switch
        {
            Base16FormatProvider => Base16.TryEncode(value, provider, out result),
            Base32FormatProvider => Base32.TryEncode(value, provider, out result),
            Base58FormatProvider => Base58.TryEncode(value, provider, out result),
            Base64FormatProvider => Base64.TryEncode(value, provider, out result),
            _ => false
        };
    }

    /// <summary>
    /// Tries to obtain a new <see cref="T:byte[]"/> array by converting the specified <see cref="ReadOnlySpan{Char}"/> value.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <param name="provider">
    /// The <see cref="IFormatProvider"/> which will be used to convert the specified <see cref="ReadOnlySpan{Char}"/> value.
    /// <remarks> Allowed values for this parameter are <see cref="Base16FormatProvider"/>, <see cref="Base32FormatProvider"/>, <see cref="Base58FormatProvider"/>, and <see cref="Base64FormatProvider"/>.</remarks>
    /// </param>
    /// <param name="result">A new <see cref="T:byte[]"/> array by converting the specified <see cref="ReadOnlySpan{Char}"/> value, or an empty <see cref="T:byte[]"/> array if the value cannot be converted.</param>
    /// <returns>Returns <see langword="true"/> if the conversion was successful; otherwise, <see langword="false"/>.</returns>
    public static bool TryGetBytes(ReadOnlySpan<char> value, IFormatProvider provider, out byte[] result)
    {
        result = [];

        return provider switch
        {
            Base16FormatProvider => Base16.TryDecode(value, provider, out result),
            Base32FormatProvider => Base32.TryDecode(value, provider, out result),
            Base58FormatProvider => Base58.TryDecode(value, provider, out result),
            Base64FormatProvider => Base64.TryDecode(value, provider, out result),
            _ => false
        };
    }

    /// <summary>
    /// Encodes the specified <see cref="ReadOnlySpan{T}"/> value into a Base-N <see cref="String"/> representation.
    /// </summary>
    /// <param name="value">The value to encode into a Base-N <see cref="String"/> representation.</param>
    /// <param name="provider">The format provider that will be used to encode the specified value.</param>
    /// <returns>Returns a new Base-N <see cref="String"/> representation encoded from the specified value.</returns>
    string Encode(ReadOnlySpan<byte> value, IFormatProvider? provider = null);

    /// <summary>
    /// Decodes the specified <see cref="ReadOnlySpan{T}"/> Base-N representation into a <see cref="T:Byte[]"/>.
    /// </summary>
    /// <param name="value">The Base-N value to decode into a <see cref="T:Byte[]"/>.</param>
    /// <param name="provider">The format provider that will be used to decode the specified value.</param>
    /// <returns>Returns a new <see cref="T:Byte[]"/> decoded from the specified value.</returns>
    byte[] Decode(ReadOnlySpan<char> value, IFormatProvider? provider = null);

    /// <summary>
    /// Tries to encode the specified <see cref="ReadOnlySpan{T}"/> value into a Base-N <see cref="String"/> representation.
    /// </summary>
    /// <param name="value">The value to encode into a Base-N <see cref="String"/> representation.</param>
    /// <param name="provider">The format provider that will be used to encode the specified value.</param>
    /// <param name="result">
    /// A new Base-N <see cref="String"/> representation encoded from the specified value,
    /// or an empty string if the specified value could not be encoded.
    /// </param>
    /// <returns>Returns <see langword="true"/> if the specified value was encoded successfully; otherwise, <see langword="false"/>.</returns>
    bool TryEncode(ReadOnlySpan<byte> value, IFormatProvider? provider, out string result);

    /// <summary>
    /// Tries to decode the specified <see cref="ReadOnlySpan{T}"/> Base-N representation into a <see cref="T:Byte[]"/>.
    /// </summary>
    /// <param name="value">The Base-N value to decode into a <see cref="T:Byte[]"/>.</param>
    /// <param name="provider">The format provider that will be used to decode the specified value.</param>
    /// <param name="result">
    /// A new <see cref="T:Byte[]"/> decoded from the specified value,
    /// or an empty <see cref="T:Byte[]"/> if the specified value could not be decoded.
    /// </param>
    /// <returns>Returns <see langword="true"/> if the specified value was decoded successfully; otherwise, <see langword="false"/>.</returns>
    bool TryDecode(ReadOnlySpan<char> value, IFormatProvider? provider, out byte[] result);
}
