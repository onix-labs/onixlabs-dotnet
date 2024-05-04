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

namespace OnixLabs.Core.Text;

public readonly partial struct Base16
{
    /// <summary>
    /// Parses the specified Base-16 encoded <see cref="System.String"/> value into a <see cref="Base16"/> value.
    /// </summary>
    /// <param name="value">The Base-16 encoded value to parse.</param>
    /// <param name="provider">The format provider that will be used to decode the specified value.</param>
    /// <returns>Returns a new <see cref="Base16"/> instance, parsed from the specified Base-16 encoded <see cref="System.String"/> value.</returns>
    public static Base16 Parse(string value, IFormatProvider? provider = null)
    {
        return Parse(value.AsSpan(), provider);
    }

    /// <summary>
    /// Parses the specified Base-16 encoded <see cref="System.ReadOnlySpan{T}"/> value into a <see cref="Base16"/> value.
    /// </summary>
    /// <param name="value">The Base-16 encoded value to parse.</param>
    /// <param name="provider">The format provider that will be used to decode the specified value.</param>
    /// <returns>Returns a new <see cref="Base16"/> instance, parsed from the specified Base-16 encoded <see cref="System.ReadOnlySpan{T}"/> value.</returns>
    public static Base16 Parse(ReadOnlySpan<char> value, IFormatProvider? provider = null)
    {
        byte[] bytes = IBaseCodec.Base16.Decode(value, provider);
        return new Base16(bytes);
    }

    /// <summary>
    /// Tries to parse the specified Base-16 encoded <see cref="System.String"/> value into a <see cref="Base16"/> value.
    /// </summary>
    /// <param name="value">The Base-16 encoded value to parse.</param>
    /// <param name="provider">The format provider that will be used to decode the specified value.</param>
    /// <param name="result">
    /// A new <see cref="Base16"/> instance, parsed from the specified Base-16 encoded <see cref="System.String"/> value,
    /// or the default <see cref="Base16"/> value if the specified Base-16 encoded <see cref="System.String"/> could not be parsed.
    /// </param>
    /// <returns>Returns <see langword="true"/> if the specified Base-16 value was decoded successfully; otherwise, <see langword="false"/>.</returns>
    public static bool TryParse(string? value, IFormatProvider? provider, out Base16 result)
    {
        return TryParse(value.AsSpan(), provider, out result);
    }

    /// <summary>
    /// Tries to parse the specified Base-16 encoded <see cref="System.ReadOnlySpan{T}"/> value into a <see cref="Base16"/> value.
    /// </summary>
    /// <param name="value">The Base-16 encoded value to parse.</param>
    /// <param name="provider">The format provider that will be used to decode the specified value.</param>
    /// <param name="result">
    /// A new <see cref="Base16"/> instance, parsed from the specified Base-16 encoded <see cref="System.ReadOnlySpan{T}"/> value,
    /// or the default <see cref="Base16"/> value if the specified Base-16 encoded <see cref="System.ReadOnlySpan{T}"/> could not be parsed.
    /// </param>
    /// <returns>Returns <see langword="true"/> if the specified Base-16 value was decoded successfully; otherwise, <see langword="false"/>.</returns>
    public static bool TryParse(ReadOnlySpan<char> value, IFormatProvider? provider, out Base16 result)
    {
        if (IBaseCodec.Base16.TryDecode(value, provider, out byte[] bytes))
        {
            result = new Base16(bytes);
            return true;
        }

        result = default;
        return false;
    }
}
