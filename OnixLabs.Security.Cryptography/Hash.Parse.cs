// Copyright 2020-2022 ONIXLabs
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
using OnixLabs.Core;

namespace OnixLabs.Security.Cryptography
{
    public readonly partial struct Hash
    {
        /// <summary>
        /// Parses a hexadecimal representation of a hash into a <see cref="Hash"/> instance.
        /// </summary>
        /// <param name="value">A <see cref="string"/> that contains a hash to convert.</param>
        /// <param name="type">The hash algorithm type of the hash.</param>
        /// <returns>A new <see cref="Hash"/> instance.</returns>
        public static Hash Parse(string value, HashAlgorithmType? type = null)
        {
            HashAlgorithmType parsedType = GetParsedHashAlgorithmType(value);
            byte[] parsedValue = GetParsedHashValue(value);

            if (type is not null)
            {
                CheckMatchingHashAlgorithms(parsedType, type);
            }

            return FromByteArray(parsedValue, parsedType);
        }

        /// <summary>
        /// Attempts to parse a hexadecimal representation of a hash into a <see cref="Hash"/> instance.
        /// </summary>
        /// <param name="value">A <see cref="string"/> that contains a hash to convert.</param>
        /// <param name="type">The hash algorithm type of the hash.</param>
        /// <param name="hash">The <see cref="Hash"/> result if conversion was successful.</param>
        /// <returns>Returns true if the hash conversion was successful; otherwise, false.</returns>
        public static bool TryParse(string value, HashAlgorithmType? type, out Hash hash)
        {
            try
            {
                hash = Parse(value, type);
                return true;
            }
            catch
            {
                hash = default;
                return false;
            }
        }

        /// <summary>
        /// Parses a <see cref="HashAlgorithmType"/> from the specified <see cref="string"/> value. 
        /// </summary>
        /// <param name="value">The hash value to parse.</param>
        /// <returns>Returns a <see cref="HashAlgorithmType"/> from the specified <see cref="string"/> value.</returns>
        private static HashAlgorithmType GetParsedHashAlgorithmType(string value)
        {
            string defaultHashAlgorithmType = HashAlgorithmType.Unknown.Name;
            string parsedType = value.SubstringBefore(':', defaultHashAlgorithmType);
            return HashAlgorithmType.FromName(parsedType);
        }

        /// <summary>
        /// Parses a <see cref="byte"/> array from the specified <see cref="string"/> value.
        /// </summary>
        /// <param name="value">The hash value to parse.</param>
        /// <returns>Returns a <see cref="byte"/> array from the specified <see cref="string"/> value.</returns>
        private static byte[] GetParsedHashValue(string value)
        {
            string parsedValue = value.SubstringAfter(':');
            return Convert.FromHexString(parsedValue);
        }

        /// <summary>
        /// Checks that the parsed and specified <see cref="HashAlgorithmType"/> instances match.
        /// </summary>
        /// <param name="parsed">The parsed <see cref="HashAlgorithmType"/> to check.</param>
        /// <param name="specified">The specified <see cref="HashAlgorithmType"/> to check.</param>
        /// <exception cref="InvalidOperationException">If the <see cref="HashAlgorithmType"/> instances do not match.</exception>
        private static void CheckMatchingHashAlgorithms(HashAlgorithmType parsed, HashAlgorithmType specified)
        {
            if (parsed != specified)
            {
                throw new InvalidOperationException(
                    $"The parsed hash algorithm type '{parsed}' does not match the expected hash algorithm type '{specified}'.");
            }
        }
    }
}
