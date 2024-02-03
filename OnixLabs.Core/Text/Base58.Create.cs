// Copyright © 2020 ONIXLabs
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

public readonly partial struct Base58
{
    /// <summary>
    /// Creates a new <see cref="Base58"/> value from the specified <see cref="T:byte[]"/> value.
    /// </summary>
    /// <param name="value">The <see cref="T:byte[]"/> value from which to create a new <see cref="Base58"/> value.</param>
    /// <returns>Returns a new <see cref="Base58"/> value from the specified <see cref="T:byte[]"/> value.</returns>
    public static Base58 Create(byte[] value)
    {
        return new Base58(value);
    }

    /// <summary>
    /// Creates a new <see cref="Base58"/> value from the specified <see cref="ReadOnlySpan{T}"/> value.
    /// </summary>
    /// <param name="value">The <see cref="ReadOnlySpan{T}"/> value from which to create a new <see cref="Base58"/> value.</param>
    /// <returns>Returns a new <see cref="Base58"/> value from the specified <see cref="ReadOnlySpan{T}"/> value.</returns>
    public static Base58 Create(ReadOnlySpan<byte> value)
    {
        return Create(value.ToArray());
    }

    /// <summary>
    /// Creates a new <see cref="Base58"/> value from the specified <see cref="string"/> value, using the default <see cref="Encoding"/>.
    /// </summary>
    /// <param name="value">The <see cref="string"/> value from which to create a new <see cref="Base58"/> value.</param>
    /// <returns>Returns a new <see cref="Base58"/> value from the specified <see cref="string"/> value.</returns>
    public static Base58 Create(string value)
    {
        return Create(value.ToCharArray(), Encoding.Default);
    }

    /// <summary>
    /// Creates a new <see cref="Base58"/> value from the specified <see cref="string"/> value.
    /// </summary>
    /// <param name="value">The <see cref="string"/> value from which to create a new <see cref="Base58"/> value.</param>
    /// <param name="encoding">The <see cref="Encoding"/> which will be used to obtain a <see cref="T:byte[]"/> from the specified <see cref="string"/> value.</param>
    /// <returns>Returns a new <see cref="Base58"/> value from the specified <see cref="string"/> value.</returns>
    public static Base58 Create(string value, Encoding encoding)
    {
        return Create(value.ToCharArray(), encoding);
    }

    /// <summary>
    /// Creates a new <see cref="Base58"/> value from the specified <see cref="T:char[]"/> value, using the default <see cref="Encoding"/>.
    /// </summary>
    /// <param name="value">The <see cref="T:char[]"/> value from which to create a new <see cref="Base58"/> value.</param>
    /// <returns>Returns a new <see cref="Base58"/> value from the specified <see cref="T:char[]"/> value.</returns>
    public static Base58 Create(char[] value)
    {
        return Create(value, Encoding.Default);
    }

    /// <summary>
    /// Creates a new <see cref="Base58"/> value from the specified <see cref="T:char[]"/> value.
    /// </summary>
    /// <param name="value">The <see cref="T:char[]"/> value from which to create a new <see cref="Base58"/> value.</param>
    /// <param name="encoding">The <see cref="Encoding"/> which will be used to obtain a <see cref="T:byte[]"/> from the specified <see cref="T:char[]"/> value.</param>
    /// <returns>Returns a new <see cref="Base58"/> value from the specified <see cref="T:char[]"/> value.</returns>
    public static Base58 Create(char[] value, Encoding encoding)
    {
        return Create(encoding.GetBytes(value));
    }

    /// <summary>
    /// Creates a new <see cref="Base58"/> value from the specified <see cref="ReadOnlySpan{T}"/> value, using the default <see cref="Encoding"/>.
    /// </summary>
    /// <param name="value">The <see cref="ReadOnlySpan{T}"/> value from which to create a new <see cref="Base58"/> value.</param>
    /// <returns>Returns a new <see cref="Base58"/> value from the specified <see cref="ReadOnlySpan{T}"/> value.</returns>
    public static Base58 Create(ReadOnlySpan<char> value)
    {
        return Create(value.ToArray(), Encoding.Default);
    }

    /// <summary>
    /// Creates a new <see cref="Base58"/> value from the specified <see cref="ReadOnlySpan{T}"/> value.
    /// </summary>
    /// <param name="value">The <see cref="ReadOnlySpan{T}"/> value from which to create a new <see cref="Base58"/> value.</param>
    /// <param name="encoding">The <see cref="Encoding"/> which will be used to obtain a <see cref="T:byte[]"/> from the specified <see cref="ReadOnlySpan{T}"/> value.</param>
    /// <returns>Returns a new <see cref="Base58"/> value from the specified <see cref="ReadOnlySpan{T}"/> value.</returns>
    public static Base58 Create(ReadOnlySpan<char> value, Encoding encoding)
    {
        return Create(value.ToArray(), encoding);
    }
}
