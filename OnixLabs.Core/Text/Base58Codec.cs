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
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace OnixLabs.Core.Text;

/// <summary>
/// Represents a codec for encoding and decoding Base-58 values.
/// </summary>
public sealed class Base58Codec : IBaseCodec
{
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

            // ReSharper disable once HeapView.ObjectAllocation.Evident
            StringBuilder builder = new();
            BigInteger data = BigInteger.Zero;

            foreach (byte b in value)
                data = data * 256 + b;

            while (data > 0)
            {
                BigInteger remainder = data % 58;
                data /= 58;
                builder.Insert(0, formatProvider.Alphabet[(int)remainder]);
            }

            for (int index = 0; index < value.Length && value[index] == 0; index++)
                builder.Insert(0, '1');

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

            if (!IBaseCodec.TryGetFormatProvider(provider, Base58FormatProvider.Bitcoin, out Base58FormatProvider formatProvider))
            {
                result = [];
                return false;
            }

            BigInteger data = BigInteger.Zero;

            foreach (char character in value)
            {
                int characterIndex = formatProvider.Alphabet.IndexOf(character);

                if (characterIndex < 0)
                {
                    result = [];
                    return false;
                }

                data = data * 58 + characterIndex;
            }

            int leadingZeroCount = value
                .ToArray()
                .TakeWhile(character => character is '1')
                .Count();

            IEnumerable<byte> leadingZeros = Enumerable
                .Repeat(byte.MinValue, leadingZeroCount);

            IEnumerable<byte> bytesWithoutLeadingZeros = data
                .ToByteArray()
                .AsEnumerable()
                .Reverse()
                .SkipWhile(byteValue => byteValue is 0);

            result = leadingZeros
                .Concat(bytesWithoutLeadingZeros)
                .ToArray();

            return true;
        }
        catch
        {
            result = [];
            return false;
        }
    }
}
