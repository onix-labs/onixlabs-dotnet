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

public readonly partial struct Base58
{
    /// <summary>
    /// Parses a Base-58 value into a <see cref="Base58"/> value.
    /// </summary>
    /// <param name="value">The Base-58 value to parse.</param>
    /// <param name="provider">An object that provides format-specific information about the specified value. </param>
    /// <returns>Returns a new <see cref="Base58"/> instance.</returns>
    public static Base58 Parse(string value, IFormatProvider? provider = null)
    {
        return Parse(value.AsSpan(), provider);
    }

    /// <summary>
    /// Parses a Base-58 value into a <see cref="Base58"/> value.
    /// </summary>
    /// <param name="value">The Base-58 value to parse.</param>
    /// <param name="provider">An object that provides format-specific information about the specified value. </param>
    /// <returns>Returns a new <see cref="Base58"/> instance.</returns>
    public static Base58 Parse(ReadOnlySpan<char> value, IFormatProvider? provider = null)
    {
        if (TryParse(value, provider, out Base58 result)) return result;
        throw new FormatException($"The input string '{value}' was not in a correct format.");
    }

    /// <summary>
    /// Parses a Base-58 value with a checksum into a <see cref="Base58"/> value.
    /// </summary>
    /// <param name="value">The Base-58 value to parse.</param>
    /// <param name="provider">An object that provides format-specific information about the specified value. </param>
    /// <returns>Returns a new <see cref="Base58"/> instance.</returns>
    public static Base58 ParseWithChecksum(string value, IFormatProvider? provider = null)
    {
        return ParseWithChecksum(value.AsSpan(), provider);
    }

    /// <summary>
    /// Parses a Base-58 value with a checksum into a <see cref="Base58"/> value.
    /// </summary>
    /// <param name="value">The Base-58 value to parse.</param>
    /// <param name="provider">An object that provides format-specific information about the specified value. </param>
    /// <returns>Returns a new <see cref="Base58"/> instance.</returns>
    public static Base58 ParseWithChecksum(ReadOnlySpan<char> value, IFormatProvider? provider = null)
    {
        if (TryParseWithChecksum(value, provider, out Base58 result)) return result;
        throw new FormatException($"The input string '{value}' was not in a correct format.");
    }

    /// <summary>
    /// Tries to parse the specified Base-58 value into a <see cref="Base58"/> value.
    /// </summary>
    /// <param name="value">The Base-58 value to parse.</param>
    /// <param name="provider"> An object that provides format-specific information about the specified value. </param>
    /// <param name="result">
    /// On return, contains the result of parsing the specified value,
    /// or the default value in the event that the specified value could not be parsed.
    /// </param>
    /// <returns>Returns true if the specified value was parsed successfully; otherwise, false.</returns>
    public static bool TryParse(string? value, IFormatProvider? provider, out Base58 result)
    {
        return TryParse(value.AsSpan(), provider, out result);
    }

    /// <summary>
    /// Tries to parse the specified Base-58 value into a <see cref="Base58"/> value.
    /// </summary>
    /// <param name="value">The Base-58 value to parse.</param>
    /// <param name="provider"> An object that provides format-specific information about the specified value. </param>
    /// <param name="result">
    /// On return, contains the result of parsing the specified value,
    /// or the default value in the event that the specified value could not be parsed.
    /// </param>
    /// <returns>Returns true if the specified value was parsed successfully; otherwise, false.</returns>
    public static bool TryParse(ReadOnlySpan<char> value, IFormatProvider? provider, out Base58 result)
    {
        try
        {
            Base58FormatInfo info = provider as Base58FormatInfo ?? Base58FormatInfo.Default;
            byte[] bytes = Base58Codec.Decode(value, info.Alphabet);
            result = Create(bytes);
            return true;
        }
        catch
        {
            result = Empty;
            return false;
        }
    }

    /// <summary>
    /// Tries to parse the specified Base-58 value with a checksum into a <see cref="Base58"/> value.
    /// </summary>
    /// <param name="value">The Base-58 value to parse.</param>
    /// <param name="provider"> An object that provides format-specific information about the specified value. </param>
    /// <param name="result">
    /// On return, contains the result of parsing the specified value,
    /// or the default value in the event that the specified value could not be parsed.
    /// </param>
    /// <returns>Returns true if the specified value was parsed successfully; otherwise, false.</returns>
    public static bool TryParseWithChecksum(string? value, IFormatProvider? provider, out Base58 result)
    {
        return TryParseWithChecksum(value.AsSpan(), provider, out result);
    }

    /// <summary>
    /// Tries to parse the specified Base-58 value with a checksum into a <see cref="Base58"/> value.
    /// </summary>
    /// <param name="value">The Base-58 value to parse.</param>
    /// <param name="provider"> An object that provides format-specific information about the specified value. </param>
    /// <param name="result">
    /// On return, contains the result of parsing the specified value,
    /// or the default value in the event that the specified value could not be parsed.
    /// </param>
    /// <returns>Returns true if the specified value was parsed successfully; otherwise, false.</returns>
    public static bool TryParseWithChecksum(ReadOnlySpan<char> value, IFormatProvider? provider, out Base58 result)
    {
        try
        {
            Base58FormatInfo info = provider as Base58FormatInfo ?? Base58FormatInfo.Default;
            byte[] bytes = Base58Codec.Decode(value, info.Alphabet);
            byte[] bytesWithoutChecksum = RemoveChecksum(bytes);

            VerifyChecksum(bytes);

            result = Create(bytesWithoutChecksum);
            return true;
        }
        catch
        {
            result = Empty;
            return false;
        }
    }
}
