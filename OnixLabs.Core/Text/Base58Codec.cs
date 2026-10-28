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
using System.Buffers.Binary;

namespace OnixLabs.Core.Text;

/// <summary>
/// Represents a codec for encoding and decoding Base-58 values.
/// </summary>
/// <remarks>
/// Why: Base-58 treats the whole input as one big-endian integer, so encoding and decoding are inherently quadratic. Converting through a
/// <see cref="System.Numerics.BigInteger"/> rebuilt for every input byte and divided once per output digit made a 1 KiB encode take about
/// 4 ms and allocate about 1.9 MB. This works on 32-bit words instead, moving five digits at a time (58^5 fits in 32 bits) with no
/// allocation inside the loops, and produces identical output.
/// </remarks>
public sealed class Base58Codec : IBaseCodec
{
    private const int Radix = 58;
    private const int DigitsPerChunk = 5;
    private const uint ChunkRadix = Radix * Radix * Radix * Radix * Radix;
    private const int MaxStackBytes = 512;

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

            if (!IBaseCodec.TryGetFormatProvider(provider, Base58FormatProvider.Bitcoin, out Base58FormatProvider formatProvider))
            {
                result = string.Empty;
                return false;
            }

            int maxLength = GetMaxEncodedLength(value.Length);
            char[]? rented = maxLength > MaxStackBytes / sizeof(char) ? ArrayPool<char>.Shared.Rent(maxLength) : null;
            Span<char> buffer = rented is null ? stackalloc char[MaxStackBytes / sizeof(char)] : rented;

            try
            {
                TryEncodeCore(value, formatProvider, buffer, out int charsWritten);
                result = new string(buffer[..charsWritten]);
                return true;
            }
            finally
            {
                buffer.Clear();
                if (rented is not null) ArrayPool<char>.Shared.Return(rented);
            }
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

            if (!IBaseCodec.TryGetFormatProvider(provider, Base58FormatProvider.Bitcoin, out Base58FormatProvider formatProvider))
            {
                result = [];
                return false;
            }

            return TryDecodeCore(value, formatProvider, out result);
        }
        catch
        {
            result = [];
            return false;
        }
    }

    /// <summary>
    /// Tries to format the specified value as Base-58 into the specified destination, as <see cref="ISpanFormattable"/> requires.
    /// </summary>
    /// <param name="value">The value to encode.</param>
    /// <param name="provider">The format provider, which must be a <see cref="Base58FormatProvider"/>, a culture, or <see langword="null"/>.</param>
    /// <param name="destination">The destination for the encoded characters.</param>
    /// <param name="charsWritten">When this method returns, contains the number of characters written to <paramref name="destination"/>.</param>
    /// <returns>Returns <see langword="true"/> if the encoding fits in <paramref name="destination"/>; otherwise, <see langword="false"/>.</returns>
    /// <exception cref="FormatException">Thrown when <paramref name="provider"/> is not a supported format provider.</exception>
    internal static bool TryFormat(ReadOnlySpan<byte> value, IFormatProvider? provider, Span<char> destination, out int charsWritten)
    {
        if (!IBaseCodec.TryGetFormatProvider(provider, Base58FormatProvider.Bitcoin, out Base58FormatProvider formatProvider))
            throw new FormatException(IBaseCodec.EncodingFormatException);

        return TryEncodeCore(value, formatProvider, destination, out charsWritten);
    }

    /// <summary>
    /// Gets an upper bound on the number of characters needed to encode the specified number of bytes.
    /// </summary>
    /// <param name="byteCount">The number of bytes to encode.</param>
    /// <returns>Returns an upper bound on the number of characters needed to encode <paramref name="byteCount"/> bytes.</returns>
    /// <remarks>Each byte needs log(256) / log(58) ≈ 1.366 digits; 138 / 100 bounds that, plus one for the rounding.</remarks>
    private static int GetMaxEncodedLength(int byteCount) => byteCount * 138 / 100 + 1;

    /// <summary>
    /// Encodes the specified value as Base-58 into the specified destination.
    /// </summary>
    /// <param name="value">The value to encode.</param>
    /// <param name="formatProvider">The format provider that supplies the alphabet.</param>
    /// <param name="destination">The destination for the encoded characters.</param>
    /// <param name="charsWritten">When this method returns, contains the number of characters written to <paramref name="destination"/>.</param>
    /// <returns>Returns <see langword="true"/> if the encoding fits in <paramref name="destination"/>; otherwise, <see langword="false"/>.</returns>
    private static bool TryEncodeCore(ReadOnlySpan<byte> value, Base58FormatProvider formatProvider, Span<char> destination, out int charsWritten)
    {
        // Each leading zero byte is encoded as the alphabet's first character; the rest is encoded as one big-endian integer.
        int zeroCount = value.IndexOfAnyExcept((byte)0);
        if (zeroCount < 0) zeroCount = value.Length;
        ReadOnlySpan<byte> magnitude = value[zeroCount..];

        int wordCount = (magnitude.Length + 3) / 4;
        int maxDigitCount = GetMaxEncodedLength(magnitude.Length) + DigitsPerChunk;

        uint[]? rentedWords = wordCount > MaxStackBytes / sizeof(uint) ? ArrayPool<uint>.Shared.Rent(wordCount) : null;
        byte[]? rentedDigits = maxDigitCount > MaxStackBytes ? ArrayPool<byte>.Shared.Rent(maxDigitCount) : null;
        Span<uint> words = (rentedWords is null ? stackalloc uint[MaxStackBytes / sizeof(uint)] : rentedWords)[..wordCount];
        Span<byte> digits = (rentedDigits is null ? stackalloc byte[MaxStackBytes] : rentedDigits)[..maxDigitCount];

        try
        {
            ToBigEndianWords(magnitude, words);

            // Repeatedly divide the integer by 58^5, emitting the remainder's five digits from least to most significant.
            int digitStart = digits.Length;
            int firstWord = 0;

            while (firstWord < words.Length)
            {
                ulong remainder = 0;

                for (int index = firstWord; index < words.Length; index++)
                {
                    (ulong quotient, remainder) = Math.DivRem((remainder << 32) | words[index], ChunkRadix);
                    words[index] = (uint)quotient;
                }

                while (firstWord < words.Length && words[firstWord] == 0) firstWord++;

                for (int digit = 0; digit < DigitsPerChunk; digit++)
                {
                    (remainder, ulong digitValue) = Math.DivRem(remainder, Radix);
                    digits[--digitStart] = (byte)digitValue;
                }
            }

            // The most significant chunk may contribute leading zero digits, which are not part of the encoding.
            while (digitStart < digits.Length && digits[digitStart] == 0) digitStart++;

            int length = zeroCount + digits.Length - digitStart;

            if (destination.Length < length)
            {
                charsWritten = 0;
                return false;
            }

            string alphabet = formatProvider.Alphabet;
            destination[..zeroCount].Fill(alphabet[0]);

            for (int index = digitStart; index < digits.Length; index++)
                destination[zeroCount + index - digitStart] = alphabet[digits[index]];

            charsWritten = length;
            return true;
        }
        finally
        {
            // Why: the value may be key material, for example a private key formatted as Base-58.
            words.Clear();
            digits.Clear();
            if (rentedWords is not null) ArrayPool<uint>.Shared.Return(rentedWords);
            if (rentedDigits is not null) ArrayPool<byte>.Shared.Return(rentedDigits);
        }
    }

    /// <summary>
    /// Decodes the specified Base-58 characters.
    /// </summary>
    /// <param name="value">The characters to decode.</param>
    /// <param name="formatProvider">The format provider that supplies the alphabet.</param>
    /// <param name="result">When this method returns, contains the decoded bytes, or an empty array if decoding failed.</param>
    /// <returns>Returns <see langword="true"/> if every character is in the alphabet; otherwise, <see langword="false"/>.</returns>
    private static bool TryDecodeCore(ReadOnlySpan<char> value, Base58FormatProvider formatProvider, out byte[] result)
    {
        // Each leading occurrence of the alphabet's first character decodes to a zero byte; the rest is one big-endian integer.
        int zeroCount = value.IndexOfAnyExcept(formatProvider.Alphabet[0]);
        if (zeroCount < 0) zeroCount = value.Length;
        ReadOnlySpan<char> digits = value[zeroCount..];

        // Each digit needs log(58) / log(256) ≈ 0.7322 bytes; 733 / 1000 bounds that, plus one for the rounding.
        int wordCount = (digits.Length * 733 / 1000 + 1) / 4 + 1;

        uint[]? rentedWords = wordCount > MaxStackBytes / sizeof(uint) ? ArrayPool<uint>.Shared.Rent(wordCount) : null;
        Span<uint> words = (rentedWords is null ? stackalloc uint[MaxStackBytes / sizeof(uint)] : rentedWords)[..wordCount];
        words.Clear();

        try
        {
            // The integer occupies words[firstWord..], most significant word first.
            int firstWord = words.Length - 1;

            for (int start = 0; start < digits.Length; start += DigitsPerChunk)
            {
                int count = Math.Min(DigitsPerChunk, digits.Length - start);
                uint chunk = 0;
                uint multiplier = 1;

                for (int offset = 0; offset < count; offset++)
                {
                    int digit = formatProvider.Index.IndexOf(digits[start + offset]);

                    if (digit < 0)
                    {
                        result = [];
                        return false;
                    }

                    chunk = chunk * Radix + (uint)digit;
                    multiplier *= Radix;
                }

                // Multiply the integer by 58^count and add the chunk.
                ulong carry = chunk;

                for (int index = words.Length - 1; index >= firstWord; index--)
                {
                    ulong product = (ulong)words[index] * multiplier + carry;
                    words[index] = (uint)product;
                    carry = product >> 32;
                }

                if (carry != 0) words[--firstWord] = (uint)carry;
            }

            int magnitudeLength = GetBigEndianLength(words[firstWord..]);
            result = new byte[zeroCount + magnitudeLength];
            WriteBigEndian(words[firstWord..], result.AsSpan(zeroCount));

            return true;
        }
        finally
        {
            words.Clear();
            if (rentedWords is not null) ArrayPool<uint>.Shared.Return(rentedWords);
        }
    }

    /// <summary>
    /// Packs the specified big-endian bytes into big-endian 32-bit words, padding the most significant word with leading zeros.
    /// </summary>
    /// <param name="bytes">The big-endian bytes to pack.</param>
    /// <param name="words">The destination words, exactly (bytes.Length + 3) / 4 long.</param>
    private static void ToBigEndianWords(ReadOnlySpan<byte> bytes, Span<uint> words)
    {
        int leadingByteCount = bytes.Length - (words.Length - 1) * 4;

        for (int index = 0; index < words.Length; index++)
        {
            int byteCount = index == 0 ? leadingByteCount : 4;
            int offset = index == 0 ? 0 : leadingByteCount + (index - 1) * 4;
            uint word = 0;

            for (int byteIndex = 0; byteIndex < byteCount; byteIndex++)
                word = (word << 8) | bytes[offset + byteIndex];

            words[index] = word;
        }
    }

    /// <summary>
    /// Gets the number of bytes in the big-endian representation of the specified words, excluding leading zero bytes.
    /// </summary>
    /// <param name="words">The big-endian 32-bit words of the integer.</param>
    /// <returns>Returns the number of bytes in the integer's big-endian representation, excluding leading zero bytes.</returns>
    private static int GetBigEndianLength(ReadOnlySpan<uint> words)
    {
        int firstNonZero = words.IndexOfAnyExcept(0u);

        if (firstNonZero < 0) return 0;

        int leadingZeroBytes = System.Numerics.BitOperations.LeadingZeroCount(words[firstNonZero]) / 8;
        return (words.Length - firstNonZero) * 4 - leadingZeroBytes;
    }

    /// <summary>
    /// Writes the big-endian representation of the specified words, excluding leading zero bytes, into the specified destination.
    /// </summary>
    /// <param name="words">The big-endian 32-bit words of the integer.</param>
    /// <param name="destination">The destination, exactly as long as <see cref="GetBigEndianLength"/> returns for the same words.</param>
    private static void WriteBigEndian(ReadOnlySpan<uint> words, Span<byte> destination)
    {
        Span<byte> wordBytes = stackalloc byte[sizeof(uint)];
        int position = destination.Length;

        for (int index = words.Length - 1; index >= 0 && position > 0; index--)
        {
            BinaryPrimitives.WriteUInt32BigEndian(wordBytes, words[index]);
            int byteCount = Math.Min(sizeof(uint), position);
            wordBytes[(sizeof(uint) - byteCount)..].CopyTo(destination[(position - byteCount)..position]);
            position -= byteCount;
        }
    }
}
