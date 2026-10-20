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
    /// The character used to pad a Base-32 value to a whole number of quanta.
    /// </summary>
    private const char Padding = '=';

    /// <summary>
    /// The character that Crockford Base-32 decoders ignore, so that it can be inserted to aid readability.
    /// </summary>
    private const char Hyphen = '-';

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

            if (!IBaseCodec.TryGetFormatProvider(provider, Base32FormatProvider.UnpaddedRfc4648, out Base32FormatProvider formatProvider))
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

            while (formatProvider.IsPadded && builder.Length % InputSize != 0) builder.Append(Padding);

            result = builder.ToString();
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

            if (!IBaseCodec.TryGetFormatProvider(provider, Base32FormatProvider.UnpaddedRfc4648, out Base32FormatProvider formatProvider))
            {
                result = [];
                return false;
            }

            bool isCrockford = formatProvider == Base32FormatProvider.UnpaddedCrockford || formatProvider == Base32FormatProvider.PaddedCrockford;

            // Why: Crockford's specification requires decoders to ignore hyphens, which may be inserted to aid readability.
            ReadOnlySpan<char> characters = isCrockford && value.Contains(Hyphen) ? value.ToString().Replace(Hyphen.ToString(), string.Empty) : value;

            if (!TryRemovePadding(characters, formatProvider.IsPadded, out ReadOnlySpan<char> valueWithoutPadding) || !IsValidLength(valueWithoutPadding.Length))
            {
                result = [];
                return false;
            }

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
                int index = GetIndex(valueWithoutPadding[inputPosition], formatProvider, isCrockford);

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

            if (!HasZeroTrailingBits(valueWithoutPadding, outputBytes.Length, formatProvider, isCrockford))
            {
                result = [];
                return false;
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

    /// <summary>
    /// Gets the index of the specified character in the alphabet of the specified format provider.
    /// </summary>
    /// <param name="character">The character to find.</param>
    /// <param name="formatProvider">The format provider whose alphabet will be searched.</param>
    /// <param name="isCrockford">A value indicating whether the format provider is a Crockford format provider.</param>
    /// <returns>Returns the index of the specified character in the alphabet, or -1 if the character is not in the alphabet.</returns>
    private static int GetIndex(char character, Base32FormatProvider formatProvider, bool isCrockford)
    {
        if (!isCrockford) return formatProvider.Alphabet.IndexOf(character);

        // Why: Crockford's specification requires decoders to accept either case, and to read I and L as 1, and O as 0.
        char normalized = char.ToUpperInvariant(character) switch
        {
            'I' or 'L' => '1',
            'O' => '0',
            var other => other
        };

        return formatProvider.Alphabet.IndexOf(normalized);
    }

    /// <summary>
    /// Determines whether the specified number of unpadded characters can be produced by encoding a whole number of bytes.
    /// </summary>
    /// <param name="length">The number of unpadded characters.</param>
    /// <returns>Returns <see langword="true"/> if the specified length is possible; otherwise, <see langword="false"/>.</returns>
    private static bool IsValidLength(int length) => length % InputSize is not (1 or 3 or 6);

    /// <summary>
    /// Determines whether the bits of the final character beyond the final decoded byte are zero.
    /// </summary>
    /// <param name="value">The unpadded value that was decoded.</param>
    /// <param name="byteCount">The number of bytes that were decoded from the specified value.</param>
    /// <param name="formatProvider">The format provider whose alphabet was used to decode the specified value.</param>
    /// <param name="isCrockford">A value indicating whether the format provider is a Crockford format provider.</param>
    /// <returns>Returns <see langword="true"/> if the trailing bits are zero; otherwise, <see langword="false"/>.</returns>
    private static bool HasZeroTrailingBits(ReadOnlySpan<char> value, int byteCount, Base32FormatProvider formatProvider, bool isCrockford)
    {
        // Why: the final character carries more bits than the final byte needs. RFC 4648 §3.5 lets decoders ignore them,
        // but accepting non-zero values would give each byte sequence several encodings, which breaks identifiers and signed bytes.
        int trailingBits = value.Length * OutputSize - byteCount * InputSize;
        int index = GetIndex(value[^1], formatProvider, isCrockford);
        return (index & ((1 << trailingBits) - 1)) == 0;
    }

    /// <summary>
    /// Tries to remove the padding from the specified value, rejecting padding that is missing, misplaced, or excessive.
    /// </summary>
    /// <param name="value">The value from which to remove padding.</param>
    /// <param name="isPadded">A value indicating whether the specified value is expected to be padded.</param>
    /// <param name="result">The specified value without its padding.</param>
    /// <returns>Returns <see langword="true"/> if the padding of the specified value is valid; otherwise, <see langword="false"/>.</returns>
    private static bool TryRemovePadding(ReadOnlySpan<char> value, bool isPadded, out ReadOnlySpan<char> result)
    {
        result = value;

        if (!isPadded) return true;
        if (value.Length % InputSize != 0) return false;

        result = value.TrimEnd(Padding);
        return value.Length - result.Length <= 6;
    }
}
