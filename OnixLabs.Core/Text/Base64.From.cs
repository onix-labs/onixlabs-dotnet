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
    public readonly partial struct Base64
    {
        /// <summary>
        /// Creates a <see cref="Base64"/> instance from the specified <see cref="byte"/> array.
        /// </summary>
        /// <param name="value">The underlying value.</param>
        /// <returns>Returns a new <see cref="Base64"/> instance.</returns>
        public static Base64 FromByteArray(byte[] value)
        {
            return new Base64(value);
        }

        /// <summary>
        /// Creates a <see cref="Base64"/> instance from the specified <see cref="char"/> array.
        /// </summary>
        /// <param name="value">The underlying value.</param>
        /// <returns>Returns a new <see cref="Base64"/> instance.</returns>
        public static Base64 FromCharArray(char[] value)
        {
            return FromCharArray(value, Encoding.Default);
        }

        /// <summary>
        /// Creates a <see cref="Base64"/> instance from the specified <see cref="char"/> array.
        /// </summary>
        /// <param name="value">The underlying value.</param>
        /// <param name="encoding">The encoding to use to obtain the underlying value.</param>
        /// <returns>Returns a new <see cref="Base64"/> instance.</returns>
        public static Base64 FromCharArray(char[] value, Encoding encoding)
        {
            byte[] bytes = encoding.GetBytes(value);
            return FromByteArray(bytes);
        }

        /// <summary>
        /// Creates a <see cref="Base64"/> instance from the specified <see cref="ReadOnlySpan{Char}"/>.
        /// </summary>
        /// <param name="value">The underlying value.</param>
        /// <returns>Returns a new <see cref="Base64"/> instance.</returns>
        public static Base64 FromSpan(ReadOnlySpan<char> value)
        {
            return FromSpan(value, Encoding.Default);
        }

        /// <summary>
        /// Creates a <see cref="Base64"/> instance from the specified <see cref="ReadOnlySpan{Char}"/>.
        /// </summary>
        /// <param name="value">The underlying value.</param>
        /// <param name="encoding">The encoding to use to obtain the underlying value.</param>
        /// <returns>Returns a new <see cref="Base64"/> instance.</returns>
        public static Base64 FromSpan(ReadOnlySpan<char> value, Encoding encoding)
        {
            char[] characters = value.ToArray();
            return FromCharArray(characters, encoding);
        }

        /// <summary>
        /// Creates a <see cref="Base64"/> instance from the specified <see cref="string"/>.
        /// </summary>
        /// <param name="value">The underlying value.</param>
        /// <returns>Returns a new <see cref="Base64"/> instance.</returns>
        public static Base64 FromString(string value)
        {
            return FromString(value, Encoding.Default);
        }

        /// <summary>
        /// Creates a <see cref="Base64"/> instance from the specified <see cref="string"/>.
        /// </summary>
        /// <param name="value">The underlying value.</param>
        /// <param name="encoding">The encoding to use to obtain the underlying value.</param>
        /// <returns>Returns a new <see cref="Base64"/> instance.</returns>
        public static Base64 FromString(string value, Encoding encoding)
        {
            char[] characters = value.ToArray();
            return FromCharArray(characters, encoding);
        }
    }
}
