// Copyright 2020-2021 ONIXLabs
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

namespace OnixLabs.Core.Text
{
    public readonly partial struct Base58
    {
        /// <summary>
        /// Encode a byte array into a Base-58 string.
        /// </summary>
        /// <param name="value">The value to encode.</param>
        /// <param name="alphabet">The Base-58 alphabet to use for encoding.</param>
        /// <returns>Returns a Base-58 encoded string.</returns>
        private static string Encode(byte[] value, string alphabet)
        {
            BigInteger data = value.Aggregate(BigInteger.Zero, (a, b) => a * 256 + b);
            StringBuilder result = new();

            while (data > 0)
            {
                BigInteger remainder = data % 58;
                data /= 58;
                result.Insert(0, alphabet[(int) remainder]);
            }

            for (int index = 0; index < value.Length && value[index] == 0; index++)
            {
                result.Insert(0, '1');
            }

            return result.ToString();
        }

        /// <summary>
        /// Decodes a Base-58 <see cref="ReadOnlySpan{T}"/> into a byte array. 
        /// </summary>
        /// <param name="value">The value to decode.</param>
        /// <param name="alphabet">The Base-58 alphabet to use for decoding.</param>
        /// <returns>Returns a byte array.</returns>
        /// <exception cref="FormatException">If the Base-58 string format is invalid.</exception>
        private static byte[] Decode(ReadOnlySpan<char> value, string alphabet)
        {
            BigInteger data = BigInteger.Zero;

            for (int index = 0; index < value.Length; index++)
            {
                char character = value[index];
                int characterIndex = alphabet.IndexOf(character);

                if (characterIndex < 0)
                {
                    throw new FormatException($"Invalid Base58 character '{character}' at position {index}");
                }

                data = data * 58 + characterIndex;
            }

            int leadingZeroCount = value
                .ToArray()
                .TakeWhile(character => character == '1')
                .Count();

            IEnumerable<byte> leadingZeros = Enumerable
                .Repeat(byte.MinValue, leadingZeroCount);

            IEnumerable<byte> bytesWithoutLeadingZeros = data
                .ToByteArray()
                .Reverse()
                .SkipWhile(byteValue => byteValue == 0);

            return leadingZeros
                .Concat(bytesWithoutLeadingZeros)
                .ToArray();
        }
    }
}
