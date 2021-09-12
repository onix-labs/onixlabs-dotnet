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

namespace OnixLabs.Core.Text
{
    /// <summary>
    /// Represents a Base-58 value.
    /// </summary>
    public readonly partial struct Base58
    {
        /// <summary>
        /// Parses a Base-58 value into a <see cref="Base58"/> instance.
        /// </summary>
        /// <param name="value">The Base-16 (hexadecimal) value to parse.</param>
        /// <returns>A new <see cref="Base58"/> instance.</returns>
        public static Base58 Parse(string value)
        {
            return Parse(value, Base58Alphabet.Default);
        }

        /// <summary>
        /// Parses a Base-58 value into a <see cref="Base58"/> instance.
        /// </summary>
        /// <param name="value">The Base-16 (hexadecimal) value to parse.</param>
        /// <param name="alphabet">The alphabet that will be used for Base-58 encoding and decoding operations.</param>
        /// <returns>A new <see cref="Base58"/> instance.</returns>
        public static Base58 Parse(string value, Base58Alphabet alphabet)
        {
            ReadOnlySpan<char> characters = value.AsSpan();
            return Parse(characters, alphabet);
        }

        /// <summary>
        /// Parses a Base-58 value into a <see cref="Base58"/> instance.
        /// </summary>
        /// <param name="value">The Base-16 (hexadecimal) value to parse.</param>
        /// <returns>A new <see cref="Base58"/> instance.</returns>
        public static Base58 Parse(char[] value)
        {
            return Parse(value, Base58Alphabet.Default);
        }

        /// <summary>
        /// Parses a Base-58 value into a <see cref="Base58"/> instance.
        /// </summary>
        /// <param name="value">The Base-16 (hexadecimal) value to parse.</param>
        /// <param name="alphabet">The alphabet that will be used for Base-58 encoding and decoding operations.</param>
        /// <returns>A new <see cref="Base58"/> instance.</returns>
        public static Base58 Parse(char[] value, Base58Alphabet alphabet)
        {
            ReadOnlySpan<char> characters = value.AsSpan();
            return Parse(characters, alphabet);
        }

        /// <summary>
        /// Parses a Base-58 value into a <see cref="Base58"/> instance.
        /// </summary>
        /// <param name="value">The Base-16 (hexadecimal) value to parse.</param>
        /// <returns>A new <see cref="Base58"/> instance.</returns>
        public static Base58 Parse(ReadOnlySpan<char> value)
        {
            return Parse(value, Base58Alphabet.Default);
        }

        /// <summary>
        /// Parses a Base-58 value into a <see cref="Base58"/> instance.
        /// </summary>
        /// <param name="value">The Base-16 (hexadecimal) value to parse.</param>
        /// <param name="alphabet">The alphabet that will be used for Base-58 encoding and decoding operations.</param>
        /// <returns>A new <see cref="Base58"/> instance.</returns>
        public static Base58 Parse(ReadOnlySpan<char> value, Base58Alphabet alphabet)
        {
            byte[] bytes = Decode(value, alphabet.Alphabet);
            return FromByteArray(bytes, alphabet);
        }

        /// <summary>
        /// Parses a Base-58 value with a checksum into a <see cref="Base58"/> instance.
        /// </summary>
        /// <param name="value">The Base-16 (hexadecimal) value to ParseWithChecksum.</param>
        /// <returns>A new <see cref="Base58"/> instance.</returns>
        public static Base58 ParseWithChecksum(string value)
        {
            return ParseWithChecksum(value, Base58Alphabet.Default);
        }

        /// <summary>
        /// Parses a Base-58 value with a checksum into a <see cref="Base58"/> instance.
        /// </summary>
        /// <param name="value">The Base-16 (hexadecimal) value to ParseWithChecksum.</param>
        /// <param name="alphabet">The alphabet that will be used for Base-58 encoding and decoding operations.</param>
        /// <returns>A new <see cref="Base58"/> instance.</returns>
        public static Base58 ParseWithChecksum(string value, Base58Alphabet alphabet)
        {
            ReadOnlySpan<char> characters = value.AsSpan();
            return ParseWithChecksum(characters, alphabet);
        }

        /// <summary>
        /// Parses a Base-58 value with a checksum into a <see cref="Base58"/> instance.
        /// </summary>
        /// <param name="value">The Base-16 (hexadecimal) value to ParseWithChecksum.</param>
        /// <returns>A new <see cref="Base58"/> instance.</returns>
        public static Base58 ParseWithChecksum(char[] value)
        {
            return ParseWithChecksum(value, Base58Alphabet.Default);
        }

        /// <summary>
        /// Parses a Base-58 value with a checksum into a <see cref="Base58"/> instance.
        /// </summary>
        /// <param name="value">The Base-16 (hexadecimal) value to ParseWithChecksum.</param>
        /// <param name="alphabet">The alphabet that will be used for Base-58 encoding and decoding operations.</param>
        /// <returns>A new <see cref="Base58"/> instance.</returns>
        public static Base58 ParseWithChecksum(char[] value, Base58Alphabet alphabet)
        {
            ReadOnlySpan<char> characters = value.AsSpan();
            return ParseWithChecksum(characters, alphabet);
        }

        /// <summary>
        /// Parses a Base-58 value with a checksum into a <see cref="Base58"/> instance.
        /// </summary>
        /// <param name="value">The Base-16 (hexadecimal) value to ParseWithChecksum.</param>
        /// <returns>A new <see cref="Base58"/> instance.</returns>
        public static Base58 ParseWithChecksum(ReadOnlySpan<char> value)
        {
            return ParseWithChecksum(value, Base58Alphabet.Default);
        }

        /// <summary>
        /// Parses a Base-58 value with a checksum into a <see cref="Base58"/> instance.
        /// </summary>
        /// <param name="value">The Base-16 (hexadecimal) value to ParseWithChecksum.</param>
        /// <param name="alphabet">The alphabet that will be used for Base-58 encoding and decoding operations.</param>
        /// <returns>A new <see cref="Base58"/> instance.</returns>
        public static Base58 ParseWithChecksum(ReadOnlySpan<char> value, Base58Alphabet alphabet)
        {
            byte[] bytes = Decode(value, alphabet.Alphabet);
            byte[] bytesWithoutChecksum = RemoveChecksum(bytes);

            VerifyChecksum(bytes);

            return FromByteArray(bytesWithoutChecksum, alphabet);
        }
    }
}
