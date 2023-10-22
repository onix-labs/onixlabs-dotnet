// Copyright © 2020 ONIXLabs
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

public readonly partial struct Base32
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
    /// Encode a byte array into a Base-32 string.
    /// </summary>
    /// <param name="value">The value to encode.</param>
    /// <param name="alphabet">The Base-32 alphabet to use for encoding.</param>
    /// <param name="padding">Determines whether padding should be applied for Base-32 encoding and decoding operations.</param>
    /// <returns>Returns a Base-32 encoded string.</returns>
    private static string Encode(byte[] value, string alphabet, bool padding)
    {
        if (value.Length == 0)
        {
            return string.Empty;
        }

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
            builder.Append(alphabet[outputPosition]);
            outputSubPosition = 0;
        }

        if (outputSubPosition <= 0) return builder.ToString();

        outputPosition <<= (OutputSize - outputSubPosition);
        outputPosition &= 0x1F;
        builder.Append(alphabet[outputPosition]);

        while (padding && builder.Length % InputSize != 0)
        {
            builder.Append('=');
        }

        return builder.ToString();
    }

    /// <summary>
    /// Decodes a Base-32 <see cref="ReadOnlySpan{T}"/> into a byte array. 
    /// </summary>
    /// <param name="value">The value to decode.</param>
    /// <param name="alphabet">The Base-32 alphabet to use for decoding.</param>
    /// <param name="padding">Determines whether padding should be applied for Base-32 encoding and decoding operations.</param>
    /// <returns>Returns a byte array.</returns>
    /// <exception cref="FormatException">If the Base-32 string format is invalid.</exception>
    private static byte[] Decode(ReadOnlySpan<char> value, string alphabet, bool padding)
    {
        if (value == string.Empty)
        {
            return Array.Empty<byte>();
        }

        if (padding && value.Length % InputSize != 0)
        {
            throw new FormatException("Base32 string is invalid. Insufficient padding has been applied.");
        }

        ReadOnlySpan<char> valueWithoutPadding = padding ? value.TrimEnd('=') : value;

        byte[] outputBytes = new byte[valueWithoutPadding.Length * OutputSize / InputSize];

        if (outputBytes.Length == 0)
        {
            throw new FormatException("Base32 string is invalid. Not enough data to construct byte array.");
        }

        int inputPosition = 0;
        int inputSubPosition = 0;
        int outputPosition = 0;
        int outputSubPosition = 0;

        while (outputPosition < outputBytes.Length)
        {
            char character = valueWithoutPadding[inputPosition];
            int index = alphabet.IndexOf(character);

            if (index < 0)
            {
                throw new FormatException($"Invalid Base32 character '{character}' at position {inputPosition}");
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

        return outputBytes;
    }
}
