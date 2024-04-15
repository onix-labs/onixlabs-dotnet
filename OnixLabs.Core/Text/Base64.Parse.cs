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

namespace OnixLabs.Core.Text;

public readonly partial struct Base64
{
    /// <summary>
    /// Parses a Base-64 value into a <see cref="Base64"/> value.
    /// </summary>
    /// <param name="value">The Base-64 value to parse.</param>
    /// <param name="provider">
    /// An object that provides format-specific information about the specified value.
    /// The parameter is ignored by the current implementation of <see cref="Base64"/>.
    /// </param>
    /// <returns>Returns a new <see cref="Base64"/> instance.</returns>
    public static Base64 Parse(string value, IFormatProvider? provider = null)
    {
        return Parse(value.AsSpan(), provider);
    }

    /// <summary>
    /// Parses a Base-64 value into a <see cref="Base64"/> value.
    /// </summary>
    /// <param name="value">The Base-64 value to parse.</param>
    /// <param name="provider">
    /// An object that provides format-specific information about the specified value.
    /// The parameter is ignored by the current implementation of <see cref="Base64"/>.
    /// </param>
    /// <returns>Returns a new <see cref="Base64"/> instance.</returns>
    public static Base64 Parse(ReadOnlySpan<char> value, IFormatProvider? provider = null)
    {
        if (TryParse(value, provider, out Base64 result)) return result;
        throw new FormatException($"The input string '{value}' was not in a correct format.");
    }

    /// <summary>
    /// Tries to parse the specified Base-64 value into a <see cref="Base64"/> value.
    /// </summary>
    /// <param name="value">The Base-64 value to parse.</param>
    /// <param name="provider">
    /// An object that provides format-specific information about the specified value.
    /// The parameter is ignored by the current implementation of <see cref="Base64"/>.
    /// </param>
    /// <param name="result">
    /// On return, contains the result of parsing the specified value,
    /// or the default value in the event that the specified value could not be parsed.
    /// </param>
    /// <returns>Returns <see langword="true"/> if the specified value was parsed successfully; otherwise, <see langword="false"/>.</returns>
    public static bool TryParse(string? value, IFormatProvider? provider, out Base64 result)
    {
        return TryParse(value.AsSpan(), provider, out result);
    }

    /// <summary>
    /// Tries to parse the specified Base-64 value into a <see cref="Base64"/> value.
    /// </summary>
    /// <param name="value">The Base-64 value to parse.</param>
    /// <param name="provider">
    /// An object that provides format-specific information about the specified value.
    /// The parameter is ignored by the current implementation of <see cref="Base64"/>.
    /// </param>
    /// <param name="result">
    /// On return, contains the result of parsing the specified value,
    /// or the default value in the event that the specified value could not be parsed.
    /// </param>
    /// <returns>Returns <see langword="true"/> if the specified value was parsed successfully; otherwise, <see langword="false"/>.</returns>
    public static bool TryParse(ReadOnlySpan<char> value, IFormatProvider? provider, out Base64 result)
    {
        try
        {
            byte[] bytes = Convert.FromBase64String(value.ToString());
            result = new Base64(bytes);
            return true;
        }
        catch
        {
            result = Empty;
            return false;
        }
    }
}
