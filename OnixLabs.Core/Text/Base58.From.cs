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
using System.Linq;
using System.Text;

namespace OnixLabs.Core.Text
{
    public readonly partial struct Base58
    {
        /// <summary>
        /// Creates a <see cref="Base58"/> instance from the specified <see cref="byte"/> array.
        /// </summary>
        /// <param name="value">The underlying value.</param>
        /// <returns>Returns a new <see cref="Base58"/> instance.</returns>
        public static Base58 FromByteArray(byte[] value)
        {
            return new Base58(value, Base58Alphabet.Default);
        }

        /// <summary>
        /// Creates a <see cref="Base58"/> instance from the specified <see cref="byte"/> array.
        /// </summary>
        /// <param name="value">The underlying value.</param>
        /// <param name="alphabet">The alphabet that will be used for Base-58 encoding and decoding operations.</param>
        /// <returns>Returns a new <see cref="Base58"/> instance.</returns>
        public static Base58 FromByteArray(byte[] value, Base58Alphabet alphabet)
        {
            return new Base58(value, alphabet);
        }

        /// <summary>
        /// Creates a <see cref="Base58"/> instance from the specified <see cref="char"/> array.
        /// </summary>
        /// <param name="value">The underlying value.</param>
        /// <returns>Returns a new <see cref="Base58"/> instance.</returns>
        public static Base58 FromCharArray(char[] value)
        {
            return FromCharArray(value, Encoding.Default, Base58Alphabet.Default);
        }

        /// <summary>
        /// Creates a <see cref="Base58"/> instance from the specified <see cref="char"/> array.
        /// </summary>
        /// <param name="value">The underlying value.</param>
        /// <param name="encoding">The encoding to use to obtain the underlying value.</param>
        /// <returns>Returns a new <see cref="Base58"/> instance.</returns>
        public static Base58 FromCharArray(char[] value, Encoding encoding)
        {
            return FromCharArray(value, encoding, Base58Alphabet.Default);
        }

        /// <summary>
        /// Creates a <see cref="Base58"/> instance from the specified <see cref="char"/> array.
        /// </summary>
        /// <param name="value">The underlying value.</param>
        /// <param name="alphabet">The alphabet that will be used for Base-58 encoding and decoding operations.</param>
        /// <returns>Returns a new <see cref="Base58"/> instance.</returns>
        public static Base58 FromCharArray(char[] value, Base58Alphabet alphabet)
        {
            return FromCharArray(value, Encoding.Default, alphabet);
        }

        /// <summary>
        /// Creates a <see cref="Base58"/> instance from the specified <see cref="char"/> array.
        /// </summary>
        /// <param name="value">The underlying value.</param>
        /// <param name="encoding">The encoding to use to obtain the underlying value.</param>
        /// <param name="alphabet">The alphabet that will be used for Base-58 encoding and decoding operations.</param>
        /// <returns>Returns a new <see cref="Base58"/> instance.</returns>
        public static Base58 FromCharArray(char[] value, Encoding encoding, Base58Alphabet alphabet)
        {
            byte[] bytes = encoding.GetBytes(value);
            return FromByteArray(bytes, alphabet);
        }

        /// <summary>
        /// Creates a <see cref="Base58"/> instance from the specified <see cref="ReadOnlySpan{Char}"/>.
        /// </summary>
        /// <param name="value">The underlying value.</param>
        /// <returns>Returns a new <see cref="Base58"/> instance.</returns>
        public static Base58 FromSpan(ReadOnlySpan<char> value)
        {
            return FromSpan(value, Encoding.Default, Base58Alphabet.Default);
        }

        /// <summary>
        /// Creates a <see cref="Base58"/> instance from the specified <see cref="ReadOnlySpan{Char}"/>.
        /// </summary>
        /// <param name="value">The underlying value.</param>
        /// <param name="encoding">The encoding to use to obtain the underlying value.</param>
        /// <returns>Returns a new <see cref="Base58"/> instance.</returns>
        public static Base58 FromSpan(ReadOnlySpan<char> value, Encoding encoding)
        {
            return FromSpan(value, encoding, Base58Alphabet.Default);
        }

        /// <summary>
        /// Creates a <see cref="Base58"/> instance from the specified <see cref="ReadOnlySpan{Char}"/>.
        /// </summary>
        /// <param name="value">The underlying value.</param>
        /// <param name="alphabet">The alphabet that will be used for Base-58 encoding and decoding operations.</param>
        /// <returns>Returns a new <see cref="Base58"/> instance.</returns>
        public static Base58 FromSpan(ReadOnlySpan<char> value, Base58Alphabet alphabet)
        {
            return FromSpan(value, Encoding.Default, alphabet);
        }

        /// <summary>
        /// Creates a <see cref="Base58"/> instance from the specified <see cref="ReadOnlySpan{Char}"/>.
        /// </summary>
        /// <param name="value">The underlying value.</param>
        /// <param name="encoding">The encoding to use to obtain the underlying value.</param>
        /// <param name="alphabet">The alphabet that will be used for Base-58 encoding and decoding operations.</param>
        /// <returns>Returns a new <see cref="Base58"/> instance.</returns>
        public static Base58 FromSpan(ReadOnlySpan<char> value, Encoding encoding, Base58Alphabet alphabet)
        {
            char[] characters = value.ToArray();
            return FromCharArray(characters, encoding, alphabet);
        }

        /// <summary>
        /// Creates a <see cref="Base58"/> instance from the specified <see cref="string"/>.
        /// </summary>
        /// <param name="value">The underlying value.</param>
        /// <returns>Returns a new <see cref="Base58"/> instance.</returns>
        public static Base58 FromString(string value)
        {
            return FromString(value, Encoding.Default, Base58Alphabet.Default);
        }

        /// <summary>
        /// Creates a <see cref="Base58"/> instance from the specified <see cref="string"/>.
        /// </summary>
        /// <param name="value">The underlying value.</param>
        /// <param name="encoding">The encoding to use to obtain the underlying value.</param>
        /// <returns>Returns a new <see cref="Base58"/> instance.</returns>
        public static Base58 FromString(string value, Encoding encoding)
        {
            return FromString(value, encoding, Base58Alphabet.Default);
        }

        /// <summary>
        /// Creates a <see cref="Base58"/> instance from the specified <see cref="string"/>.
        /// </summary>
        /// <param name="value">The underlying value.</param>
        /// <param name="alphabet">The alphabet that will be used for Base-58 encoding and decoding operations.</param>
        /// <returns>Returns a new <see cref="Base58"/> instance.</returns>
        public static Base58 FromString(string value, Base58Alphabet alphabet)
        {
            return FromString(value, Encoding.Default, alphabet);
        }

        /// <summary>
        /// Creates a <see cref="Base58"/> instance from the specified <see cref="string"/>.
        /// </summary>
        /// <param name="value">The underlying value.</param>
        /// <param name="encoding">The encoding to use to obtain the underlying value.</param>
        /// <param name="alphabet">The alphabet that will be used for Base-58 encoding and decoding operations.</param>
        /// <returns>Returns a new <see cref="Base58"/> instance.</returns>
        public static Base58 FromString(string value, Encoding encoding, Base58Alphabet alphabet)
        {
            char[] characters = value.ToArray();
            return FromCharArray(characters, encoding, alphabet);
        }
    }
}
