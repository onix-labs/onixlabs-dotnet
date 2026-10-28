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

using System.Text;

namespace OnixLabs.Core.UnitTests.Data;

/// <summary>
/// Provides the library's original Base32 encoder and decoder, used as an oracle for the codec.
/// </summary>
/// <remarks>
/// This is the original algorithm kept unchanged, built on <see cref="StringBuilder"/> and <see cref="string.IndexOf(char)"/>, so that the
/// faster replacement can be checked against it, including its decoding quirks. For example, a trailing character that contributes no
/// bits to a whole output byte is never read, so it is not validated. The library's behaviour must not change.
/// </remarks>
public static class Base32Reference
{
    private const int InputSize = 8;
    private const int OutputSize = 5;

    /// <summary>
    /// Encodes the specified bytes as Base32.
    /// </summary>
    /// <param name="value">The bytes to encode.</param>
    /// <param name="alphabet">The 32-character alphabet.</param>
    /// <param name="isPadded">A value indicating whether the encoding is padded to a multiple of eight characters.</param>
    /// <returns>Returns the Base32 encoding of the specified bytes.</returns>
    public static string Encode(byte[] value, string alphabet, bool isPadded)
    {
        if (value.Length == 0) return string.Empty;

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

        outputPosition <<= OutputSize - outputSubPosition;
        outputPosition &= 0x1F;
        builder.Append(alphabet[outputPosition]);

        while (isPadded && builder.Length % InputSize != 0) builder.Append('=');

        return builder.ToString();
    }

    /// <summary>
    /// Decodes the specified Base32 text.
    /// </summary>
    /// <param name="value">The text to decode.</param>
    /// <param name="alphabet">The 32-character alphabet.</param>
    /// <param name="isPadded">A value indicating whether the encoding is padded to a multiple of eight characters.</param>
    /// <returns>Returns the decoded bytes, or <see langword="null"/> if the original decoder rejected the text.</returns>
    public static byte[]? Decode(string value, string alphabet, bool isPadded)
    {
        if (value.Length == 0) return [];
        if (isPadded && value.Length % InputSize != 0) return null;

        string valueWithoutPadding = isPadded ? value.TrimEnd('=') : value;
        byte[] outputBytes = new byte[valueWithoutPadding.Length * OutputSize / InputSize];

        if (outputBytes.Length == 0) return null;

        int inputPosition = 0;
        int inputSubPosition = 0;
        int outputPosition = 0;
        int outputSubPosition = 0;

        while (outputPosition < outputBytes.Length)
        {
            int index = alphabet.IndexOf(valueWithoutPadding[inputPosition]);

            if (index < 0) return null;

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
