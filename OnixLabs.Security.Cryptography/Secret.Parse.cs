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
using OnixLabs.Core.Text;

namespace OnixLabs.Security.Cryptography;

public readonly partial struct Secret
{
    /// <summary>
    /// Parses the specified <see cref="string"/> value into a <see cref="Secret"/> value.
    /// </summary>
    /// <param name="value">The value to parse.</param>
    /// <param name="provider">The format provider that will be used to decode the specified value.</param>
    /// <returns>Returns a new <see cref="Secret"/> instance, parsed from the specified <see cref="string"/> value.</returns>
    public static Secret Parse(string value, IFormatProvider? provider = null) => Parse(value.AsSpan(), provider);

    /// <summary>
    /// Parses the specified <see cref="ReadOnlySpan{T}"/> value into a <see cref="Secret"/> value.
    /// </summary>
    /// <param name="value">The value to parse.</param>
    /// <param name="provider">The format provider that will be used to decode the specified value.</param>
    /// <returns>Returns a new <see cref="Secret"/> instance, parsed from the specified <see cref="ReadOnlySpan{T}"/> value.</returns>
    public static Secret Parse(ReadOnlySpan<char> value, IFormatProvider? provider = null)
    {
        if (TryParse(value, provider, out Secret result)) return result;
        throw new FormatException($"The input string '{value}' was not in a correct format.");
    }

    /// <summary>
    /// Tries to parse the specified <see cref="string"/> value into a <see cref="Secret"/> value.
    /// </summary>
    /// <param name="value">The value to parse.</param>
    /// <param name="provider">The format provider that will be used to decode the specified value.</param>
    /// <param name="result">
    /// A new <see cref="Secret"/> instance, parsed from the specified <see cref="string"/> value, or the default
    /// <see cref="Secret"/> value if the specified <see cref="string"/> could not be parsed.
    /// </param>
    /// <returns>Returns <see langword="true"/> if the specified value was decoded successfully; otherwise, <see langword="false"/>.</returns>
    public static bool TryParse(string? value, IFormatProvider? provider, out Secret result) => TryParse(value.AsSpan(), provider, out result);

    /// <summary>
    /// Tries to parse the specified <see cref="ReadOnlySpan{T}"/> value into a <see cref="Secret"/> value.
    /// </summary>
    /// <param name="value">The value to parse.</param>
    /// <param name="provider">The format provider that will be used to decode the specified value.</param>
    /// <param name="result">
    /// A new <see cref="Secret"/> instance, parsed from the specified <see cref="ReadOnlySpan{T}"/> value, or the default
    /// <see cref="Secret"/> value if the specified <see cref="ReadOnlySpan{T}"/> could not be parsed.
    /// </param>
    /// <returns>Returns <see langword="true"/> if the specified value was decoded successfully; otherwise, <see langword="false"/>.</returns>
    public static bool TryParse(ReadOnlySpan<char> value, IFormatProvider? provider, out Secret result)
    {
        if (IBaseCodec.TryGetBytes(value, provider ?? Base16FormatProvider.Invariant, out byte[] bytes))
        {
            result = new Secret(bytes);
            return true;
        }

        result = default;
        return false;
    }
}
