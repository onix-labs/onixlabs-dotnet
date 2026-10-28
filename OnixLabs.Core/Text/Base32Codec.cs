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
    private const int MaxStackChars = 256;

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

            if (!IBaseCodec.TryGetFormatProvider(provider, Base32FormatProvider.Rfc4648, out Base32FormatProvider formatProvider))
            {
                result = string.Empty;
                return false;
            }

            int length = GetEncodedLength(value.Length, formatProvider.IsPadded);
            char[]? rented = length > MaxStackChars ? ArrayPool<char>.Shared.Rent(length) : null;
            Span<char> buffer = (rented is null ? stackalloc char[MaxStackChars] : rented)[..length];

            try
            {
                EncodeCore(value, formatProvider, buffer);
                result = new string(buffer);
                return true;
            }
            finally
            {
                // Why: the value may be key material, for example a private key formatted as Base-32.
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
                int index = formatProvider.Index.IndexOf(character);

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

    /// <summary>
    /// Tries to format the specified value as Base-32 into the specified destination, as <see cref="ISpanFormattable"/> requires.
    /// </summary>
    /// <param name="value">The value to encode.</param>
    /// <param name="provider">The format provider, which must be a <see cref="Base32FormatProvider"/>, a culture, or <see langword="null"/>.</param>
    /// <param name="destination">The destination for the encoded characters.</param>
    /// <param name="charsWritten">When this method returns, contains the number of characters written to <paramref name="destination"/>.</param>
    /// <returns>Returns <see langword="true"/> if the encoding fits in <paramref name="destination"/>; otherwise, <see langword="false"/>.</returns>
    /// <exception cref="FormatException">Thrown when <paramref name="provider"/> is not a supported format provider.</exception>
    internal static bool TryFormat(ReadOnlySpan<byte> value, IFormatProvider? provider, Span<char> destination, out int charsWritten)
    {
        if (!IBaseCodec.TryGetFormatProvider(provider, Base32FormatProvider.Rfc4648, out Base32FormatProvider formatProvider))
            throw new FormatException(IBaseCodec.EncodingFormatException);

        int length = GetEncodedLength(value.Length, formatProvider.IsPadded);

        if (destination.Length < length)
        {
            charsWritten = 0;
            return false;
        }

        EncodeCore(value, formatProvider, destination[..length]);
        charsWritten = length;
        return true;
    }

    /// <summary>
    /// Gets the number of characters needed to encode the specified number of bytes.
    /// </summary>
    /// <param name="byteCount">The number of bytes to encode.</param>
    /// <param name="isPadded">A value indicating whether the encoding is padded to a multiple of eight characters.</param>
    /// <returns>Returns the number of characters needed to encode <paramref name="byteCount"/> bytes.</returns>
    private static int GetEncodedLength(int byteCount, bool isPadded)
    {
        int length = (byteCount * InputSize + OutputSize - 1) / OutputSize;
        return isPadded ? (length + InputSize - 1) / InputSize * InputSize : length;
    }

    /// <summary>
    /// Encodes the specified non-empty value as Base-32 into the specified destination.
    /// </summary>
    /// <param name="value">The value to encode.</param>
    /// <param name="formatProvider">The format provider that supplies the alphabet and padding.</param>
    /// <param name="destination">The destination, exactly as long as <see cref="GetEncodedLength"/> returns.</param>
    private static void EncodeCore(ReadOnlySpan<byte> value, Base32FormatProvider formatProvider, Span<char> destination)
    {
        int inputPosition = 0;
        int inputSubPosition = 0;
        byte outputPosition = 0;
        int outputSubPosition = 0;
        int written = 0;

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
            destination[written++] = formatProvider.Alphabet[outputPosition];
            outputSubPosition = 0;
        }

        if (outputSubPosition > 0)
        {
            outputPosition <<= OutputSize - outputSubPosition;
            outputPosition &= 0x1F;
            destination[written++] = formatProvider.Alphabet[outputPosition];
        }

        // The remaining characters, if any, are padding: GetEncodedLength only adds them for padded providers.
        destination[written..].Fill('=');
    }
}
