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

public readonly partial struct Base64
{
    /// <summary>
    /// Creates a <see cref="Base64"/> instance from the specified <see cref="byte"/> array.
    /// </summary>
    /// <param name="value">The underlying value.</param>
    /// <returns>Returns a new <see cref="Base64"/> instance.</returns>
    public static Base64 Create(byte[] value)
    {
        return new Base64(value);
    }

    /// <summary>
    /// Creates a <see cref="Base64"/> instance from the specified <see cref="ReadOnlySpan{Char}"/>.
    /// </summary>
    /// <param name="value">The underlying value.</param>
    /// <returns>Returns a new <see cref="Base64"/> instance.</returns>
    public static Base64 Create(ReadOnlySpan<char> value)
    {
        return Create(value, Encoding.Default);
    }

    /// <summary>
    /// Creates a <see cref="Base64"/> instance from the specified <see cref="ReadOnlySpan{Char}"/>.
    /// </summary>
    /// <param name="value">The underlying value.</param>
    /// <param name="encoding">The encoding to use to obtain the underlying value.</param>
    /// <returns>Returns a new <see cref="Base64"/> instance.</returns>
    public static Base64 Create(ReadOnlySpan<char> value, Encoding encoding)
    {
        // TODO : Check if future versions support GetBytes with ReadOnlySpan<char> overload.
        byte[] bytes = encoding.GetBytes(value.ToArray());
        return Create(bytes);
    }
}
