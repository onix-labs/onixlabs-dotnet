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
/// Represents a codec for encoding and decoding Base-64 values.
/// </summary>
public sealed class Base64Codec : IBaseCodec
{
    /// <summary>
    /// The number of bytes in a complete Base-64 quantum.
    /// </summary>
    private const int BytesPerQuantum = 3;

    /// <summary>
    /// The number of characters in a complete Base-64 quantum.
    /// </summary>
    private const int CharactersPerQuantum = 4;

    /// <summary>
    /// The character used to pad a Base-64 value to a whole number of quanta.
    /// </summary>
    private const char Padding = '=';

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

            if (!IBaseCodec.TryGetFormatProvider(provider, Base64FormatProvider.PaddedRfc4648, out Base64FormatProvider formatProvider))
            {
                result = string.Empty;
                return false;
            }

            // Why: Convert produces exactly the canonical padded RFC 4648 §4 encoding, and is vectorised.
            result = formatProvider == Base64FormatProvider.PaddedRfc4648 ? Convert.ToBase64String(value) : EncodeWithAlphabet(value, formatProvider);
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

            if (IBaseCodec.TryGetFormatProvider(provider, Base64FormatProvider.PaddedRfc4648, out Base64FormatProvider formatProvider))
                return TryDecodeCanonical(value, formatProvider, out result);

            result = [];
            return false;
        }
        catch
        {
            result = [];
            return false;
        }
    }

    /// <summary>
    /// Encodes the specified value using the alphabet and padding of the specified format provider.
    /// </summary>
    /// <param name="value">The value to encode.</param>
    /// <param name="formatProvider">The format provider whose alphabet and padding will be used to encode the specified value.</param>
    /// <returns>Returns the Base-64 representation of the specified value.</returns>
    private static string EncodeWithAlphabet(ReadOnlySpan<byte> value, Base64FormatProvider formatProvider)
    {
        int paddedLength = (value.Length + BytesPerQuantum - 1) / BytesPerQuantum * CharactersPerQuantum;
        int unpaddedLength = (value.Length * 8 + 5) / 6;

        // ReSharper disable once HeapView.ObjectAllocation.Evident
        char[] output = new char[formatProvider.IsPadded ? paddedLength : unpaddedLength];
        output.AsSpan(unpaddedLength).Fill(Padding);

        for (int offset = 0; offset < value.Length; offset += BytesPerQuantum)
        {
            ReadOnlySpan<byte> quantum = value.Slice(offset, Math.Min(BytesPerQuantum, value.Length - offset));
            int outputOffset = offset / BytesPerQuantum * CharactersPerQuantum;
            WriteQuantum(quantum, output.AsSpan(outputOffset, quantum.Length + 1), formatProvider.Alphabet);
        }

        return new string(output);
    }

    /// <summary>
    /// Tries to decode the specified value, rejecting any representation that is not the single canonical encoding of its bytes.
    /// </summary>
    /// <param name="value">The value to decode.</param>
    /// <param name="formatProvider">The format provider whose alphabet and padding will be used to decode the specified value.</param>
    /// <param name="result">The decoded bytes, or an empty <see cref="byte"/> array if the specified value is not a canonical encoding.</param>
    /// <returns>Returns <see langword="true"/> if the specified value was decoded successfully; otherwise, <see langword="false"/>.</returns>
    private static bool TryDecodeCanonical(ReadOnlySpan<char> value, Base64FormatProvider formatProvider, out byte[] result)
    {
        result = [];

        if (!TryRemovePadding(value, formatProvider.IsPadded, out ReadOnlySpan<char> characters)) return false;
        if (characters.Length % CharactersPerQuantum == 1) return false;

        // ReSharper disable once HeapView.ObjectAllocation.Evident
        byte[] output = new byte[characters.Length * 6 / 8];

        for (int offset = 0; offset < characters.Length; offset += CharactersPerQuantum)
        {
            ReadOnlySpan<char> quantum = characters.Slice(offset, Math.Min(CharactersPerQuantum, characters.Length - offset));
            int outputOffset = offset / CharactersPerQuantum * BytesPerQuantum;
            if (!TryReadQuantum(quantum, output.AsSpan(outputOffset, quantum.Length - 1), formatProvider.Alphabet)) return false;
        }

        result = output;
        return true;
    }

    /// <summary>
    /// Writes the Base-64 characters for a single quantum of one to three bytes.
    /// </summary>
    /// <param name="quantum">The one to three bytes to encode.</param>
    /// <param name="destination">The destination that will receive one more character than there are bytes in the quantum.</param>
    /// <param name="alphabet">The alphabet used to encode each 6-bit group.</param>
    private static void WriteQuantum(ReadOnlySpan<byte> quantum, Span<char> destination, string alphabet)
    {
        int block = 0;

        for (int index = 0; index < BytesPerQuantum; index++)
            block = block << 8 | (index < quantum.Length ? quantum[index] : 0);

        for (int index = 0; index < destination.Length; index++)
            destination[index] = alphabet[block >> (18 - 6 * index) & 0x3F];
    }

    /// <summary>
    /// Tries to read the bytes encoded by a single quantum of two to four Base-64 characters.
    /// </summary>
    /// <param name="quantum">The two to four characters to decode.</param>
    /// <param name="destination">The destination that will receive one fewer byte than there are characters in the quantum.</param>
    /// <param name="alphabet">The alphabet used to decode each character.</param>
    /// <returns>
    /// Returns <see langword="true"/> if every character is in the alphabet and the bits beyond the final byte are zero; otherwise, <see langword="false"/>.
    /// </returns>
    private static bool TryReadQuantum(ReadOnlySpan<char> quantum, Span<byte> destination, string alphabet)
    {
        int block = 0;

        foreach (char character in quantum)
        {
            int index = alphabet.IndexOf(character);
            if (index < 0) return false;
            block = block << 6 | index;
        }

        // Why: a partial quantum carries more bits than its bytes need. RFC 4648 §3.5 lets decoders ignore them,
        // but accepting non-zero values would give each byte sequence several encodings, which breaks identifiers and signed bytes.
        int trailingBits = quantum.Length * 6 - destination.Length * 8;
        if ((block & ((1 << trailingBits) - 1)) != 0) return false;

        block >>= trailingBits;

        for (int index = destination.Length - 1; index >= 0; index--)
        {
            destination[index] = (byte)block;
            block >>= 8;
        }

        return true;
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
        if (value.Length % CharactersPerQuantum != 0) return false;

        result = value.TrimEnd(Padding);
        return value.Length - result.Length <= 2;
    }
}
