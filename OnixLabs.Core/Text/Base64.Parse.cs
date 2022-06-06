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

namespace OnixLabs.Core.Text;

public readonly partial struct Base64
{
    /// <summary>
    /// Parses a Base-64 value into a <see cref="Base64"/> instance.
    /// </summary>
    /// <param name="value">The Base-16 (hexadecimal) value to parse.</param>
    /// <returns>Returns a new <see cref="Base64"/> instance.</returns>
    public static Base64 Parse(string value)
    {
        char[] characters = value.ToCharArray();
        return Parse(characters);
    }

    /// <summary>
    /// Parses a Base-64 value into a <see cref="Base64"/> instance.
    /// </summary>
    /// <param name="value">The Base-16 (hexadecimal) value to parse.</param>
    /// <returns>Returns a new <see cref="Base64"/> instance.</returns>
    public static Base64 Parse(char[] value)
    {
        byte[] bytes = Convert.FromBase64CharArray(value, 0, value.Length);
        return FromByteArray(bytes);
    }

    /// <summary>
    /// Parses a Base-64 value into a <see cref="Base64"/> instance.
    /// </summary>
    /// <param name="value">The Base-16 (hexadecimal) value to parse.</param>
    /// <returns>Returns a new <see cref="Base64"/> instance.</returns>
    public static Base64 Parse(ReadOnlySpan<char> value)
    {
        char[] characters = value.ToArray();
        return Parse(characters);
    }
}
