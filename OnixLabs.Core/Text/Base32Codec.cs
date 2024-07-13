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
using System.Text;

namespace OnixLabs.Core.Text;

/// <summary>
/// Represents a codec for encoding and decoding Base-32 values.
/// </summary>
public sealed class Base32Codec : IBaseCodec
{
    /// <summary>
    /// The Base-32 input data size.
    /// </summary>
    private const int InputSize = 8;

    /// <summary>
    /// The Base-32 output data size.
    /// </summary>
    private const int OutputSize = 5;

    /// <summary>
    /// Encodes the specified <see cref="ReadOnlySpan{T}"/> value into a Base-32 <see cref="String"/> representation.
    /// </summary>
    /// <param name="value">The value to encode into a Base-32 <see cref="String"/> representation.</param>
    /// <param name="provider">The format provider that will be used to encode the specified value.</param>
    /// <returns>Returns a new Base-32 <see cref="String"/> representation encoded from the specified value.</returns>
    public string Encode(ReadOnlySpan<byte> value, IFormatProvider? provider = null)
    {
        if (TryEncode(value, provider, out string result)) return result;
        throw new FormatException(IBaseCodec.EncodingFormatException);
    }

    /// <summary>
    /// Decodes the specified <see cref="ReadOnlySpan{T}"/> Base-32 representation into a <see cref="T:Byte[]"/>.
    /// </summary>
    /// <param name="value">The Base-32 value to decode into a <see cref="T:Byte[]"/>.</param>
    /// <param name="provider">The format provider that will be used to decode the specified value.</param>
    /// <returns>Returns a new <see cref="T:Byte[]"/> decoded from the specified value.</returns>
    public byte[] Decode(ReadOnlySpan<char> value, IFormatProvider? provider = null)
    {
        if (TryDecode(value, provider, out byte[] result)) return result;
        throw new FormatException(IBaseCodec.DecodingFormatException);
    }

    /// <summary>
    /// Tries to encode the specified <see cref="ReadOnlySpan{T}"/> value into a Base-32 <see cref="String"/> representation.
    /// </summary>
    /// <param name="value">The value to encode into a Base-32 <see cref="String"/> representation.</param>
    /// <param name="provider">The format provider that will be used to encode the specified value.</param>
    /// <param name="result">
    /// A new Base-32 <see cref="String"/> representation encoded from the specified value,
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

            if (!IBaseCodec.TryGetFormatProvider(provider, Base32FormatProvider.Rfc4648, out Base32FormatProvider formatProvider))
            {
                result = string.Empty;
                return false;
            }

            // ReSharper disable once HeapView.ObjectAllocation.Evident
            StringBuilder builder = new(value.Length * InputSize / OutputSize);

            int inputPosition = 0;
            int inputSubPosition = 0;
            byte outputPosition = 0;
            int outputSubPosition = 0;

            while (inputPosition < value.Length)
            {
                int availableBits = Math.Min(InputSize - inputSubPosition, OutputSize - outputSubPosition);

                outputPosition <<= availableBits;
                outputPosition |= (byte)(value[inputPosition] >> (InputSize - (inputSubPosition + availableBits)));
                inputSubPosition += availableBits;

                if (inputSubPosition >= InputSize)
                {
                    inputPosition++;
                    inputSubPosition = 0;
                }

                outputSubPosition += availableBits;

                if (outputSubPosition < OutputSize) continue;

                outputPosition &= 0x1F;
                builder.Append(formatProvider.Alphabet[outputPosition]);
                outputSubPosition = 0;
            }

            if (outputSubPosition <= 0)
            {
                result = builder.ToString();
                return true;
            }

            outputPosition <<= OutputSize - outputSubPosition;
            outputPosition &= 0x1F;
            builder.Append(formatProvider.Alphabet[outputPosition]);

            while (formatProvider.IsPadded && builder.Length % InputSize != 0) builder.Append('=');

            result = builder.ToString();
            return true;
        }
        catch
        {
            result = string.Empty;
            return false;
        }
    }

    /// <summary>
    /// Tries to decode the specified <see cref="ReadOnlySpan{T}"/> Base-32 representation into a <see cref="T:Byte[]"/>.
    /// </summary>
    /// <param name="value">The Base-32 value to decode into a <see cref="T:Byte[]"/>.</param>
    /// <param name="provider">The format provider that will be used to decode the specified value.</param>
    /// <param name="result">
    /// A new <see cref="T:Byte[]"/> decoded from the specified value,
    /// or an empty <see cref="T:Byte[]"/> if the specified value could not be decoded.
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

            if (!IBaseCodec.TryGetFormatProvider(provider, Base32FormatProvider.Rfc4648, out Base32FormatProvider formatProvider))
            {
                result = [];
                return false;
            }

            if (formatProvider.IsPadded && value.Length % InputSize != 0)
            {
                result = [];
                return false;
            }

            ReadOnlySpan<char> valueWithoutPadding = formatProvider.IsPadded ? value.TrimEnd('=') : value;

            // ReSharper disable once HeapView.ObjectAllocation.Evident
            byte[] outputBytes = new byte[valueWithoutPadding.Length * OutputSize / InputSize];

            if (outputBytes.Length == 0)
            {
                result = [];
                return false;
            }

            int inputPosition = 0;
            int inputSubPosition = 0;
            int outputPosition = 0;
            int outputSubPosition = 0;

            while (outputPosition < outputBytes.Length)
            {
                char character = valueWithoutPadding[inputPosition];
                int index = formatProvider.Alphabet.IndexOf(character);

                if (index < 0)
                {
                    result = [];
                    return false;
                }

                int availableBits = Math.Min(OutputSize - inputSubPosition, InputSize - outputSubPosition);

                outputBytes[outputPosition] <<= availableBits;
                outputBytes[outputPosition] |= (byte)(index >> (OutputSize - (inputSubPosition + availableBits)));
                outputSubPosition += availableBits;

                if (outputSubPosition >= InputSize)
                {
                    outputPosition++;
                    outputSubPosition = 0;
                }

                inputSubPosition += availableBits;

                if (inputSubPosition < OutputSize) continue;

                inputPosition++;
                inputSubPosition = 0;
            }

            result = outputBytes;
            return true;
        }
        catch
        {
            result = [];
            return false;
        }
    }
}
