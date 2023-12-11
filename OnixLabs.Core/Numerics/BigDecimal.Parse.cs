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
using System.Globalization;

namespace OnixLabs.Core.Numerics;

public readonly partial struct BigDecimal
{
    /// <summary>
    /// Parses the specified <see cref="string"/> value into a <see cref="BigDecimal"/> value.
    /// </summary>
    /// <param name="value">The value to parse.</param>
    /// <returns>Returns a new <see cref="BigDecimal"/> instance parsed from the specified value.</returns>
    public static BigDecimal Parse(string value)
    {
        return Parse(value.AsSpan());
    }

    /// <summary>
    /// Parses the specified <see cref="string"/> value into a <see cref="BigDecimal"/> value.
    /// </summary>
    /// <param name="value">The value to parse.</param>
    /// <param name="provider">An object that provides culture-specific information about the specified value.</param>
    /// <returns>Returns a new <see cref="BigDecimal"/> instance parsed from the specified value.</returns>
    public static BigDecimal Parse(string value, IFormatProvider? provider)
    {
        return Parse(value.AsSpan(), provider);
    }

    /// <summary>
    /// Parses the specified <see cref="string"/> value into a <see cref="BigDecimal"/> value.
    /// </summary>
    /// <param name="value">The value to parse.</param>
    /// <param name="style">A bitwise combination of number styles that can be present in the specified value.</param>
    /// <param name="provider">An object that provides culture-specific information about the specified value.</param>
    /// <returns>Returns a new <see cref="BigDecimal"/> instance parsed from the specified value.</returns>
    public static BigDecimal Parse(string value, NumberStyles style, IFormatProvider? provider)
    {
        return Parse(value.AsSpan(), style, provider);
    }

    /// <summary>
    /// Parses the specified <see cref="ReadOnlySpan{T}"/> value into a <see cref="BigDecimal"/> value.
    /// </summary>
    /// <param name="value">The value to parse.</param>
    /// <returns>Returns a new <see cref="BigDecimal"/> instance parsed from the specified value.</returns>
    public static BigDecimal Parse(ReadOnlySpan<char> value)
    {
        return Parse(value, CultureInfo.CurrentCulture);
    }

    /// <summary>
    /// Parses the specified <see cref="ReadOnlySpan{T}"/> value into a <see cref="BigDecimal"/> value.
    /// </summary>
    /// <param name="value">The value to parse.</param>
    /// <param name="provider">An object that provides culture-specific information about the specified value.</param>
    /// <returns>Returns a new <see cref="BigDecimal"/> instance parsed from the specified value.</returns>
    public static BigDecimal Parse(ReadOnlySpan<char> value, IFormatProvider? provider)
    {
        return Parse(value, DefaultNumberStyles, provider);
    }

    /// <summary>
    /// Parses the specified <see cref="ReadOnlySpan{T}"/> value into a <see cref="BigDecimal"/> value.
    /// </summary>
    /// <param name="value">The value to parse.</param>
    /// <param name="style">A bitwise combination of number styles that can be present in the specified value.</param>
    /// <param name="provider">An object that provides culture-specific information about the specified value.</param>
    /// <returns>Returns a new <see cref="BigDecimal"/> instance parsed from the specified value.</returns>
    public static BigDecimal Parse(ReadOnlySpan<char> value, NumberStyles style, IFormatProvider? provider)
    {
        NumberFormatInfo info = NumberFormatInfo.GetInstance(provider);
        BigDecimalParser parser = new(info);

        return parser.Parse(value, style);
    }

    /// <summary>
    /// Tries to parse the specified <see cref="string"/> value into a <see cref="BigDecimal"/> value.
    /// </summary>
    /// <param name="value">The value to parse.</param>
    /// <param name="result">
    /// On return, contains the result of parsing the specified value,
    /// or the default value in the event that the specified value could not be parsed.
    /// </param>
    /// <returns>Returns true if the specified value was parsed successfully; otherwise, false.</returns>
    public static bool TryParse(string? value, out BigDecimal result)
    {
        return TryParse(value.AsSpan(), out result);
    }

    /// <summary>
    /// Tries to parse the specified <see cref="string"/> value into a <see cref="BigDecimal"/> value.
    /// </summary>
    /// <param name="value">The value to parse.</param>
    /// <param name="provider">An object that provides culture-specific information about the specified value.</param>
    /// <param name="result">
    /// On return, contains the result of parsing the specified value,
    /// or the default value in the event that the specified value could not be parsed.
    /// </param>
    /// <returns>Returns true if the specified value was parsed successfully; otherwise, false.</returns>
    public static bool TryParse(string? value, IFormatProvider? provider, out BigDecimal result)
    {
        return TryParse(value.AsSpan(), provider, out result);
    }

    /// <summary>
    /// Tries to parse the specified <see cref="string"/> value into a <see cref="BigDecimal"/> value.
    /// </summary>
    /// <param name="value">The value to parse.</param>
    /// <param name="style">A bitwise combination of number styles that can be present in the specified value.</param>
    /// <param name="provider">An object that provides culture-specific information about the specified value.</param>
    /// <param name="result">
    /// On return, contains the result of parsing the specified value,
    /// or the default value in the event that the specified value could not be parsed.
    /// </param>
    /// <returns>Returns true if the specified value was parsed successfully; otherwise, false.</returns>
    public static bool TryParse(string? value, NumberStyles style, IFormatProvider? provider, out BigDecimal result)
    {
        return TryParse(value.AsSpan(), style, provider, out result);
    }

    /// <summary>
    /// Tries to parse the specified <see cref="string"/> value into a <see cref="BigDecimal"/> value.
    /// </summary>
    /// <param name="value">The value to parse.</param>
    /// <param name="result">
    /// On return, contains the result of parsing the specified value,
    /// or the default value in the event that the specified value could not be parsed.
    /// </param>
    /// <returns>Returns true if the specified value was parsed successfully; otherwise, false.</returns>
    public static bool TryParse(ReadOnlySpan<char> value, out BigDecimal result)
    {
        return TryParse(value, CultureInfo.CurrentCulture, out result);
    }

    /// <summary>
    /// Tries to parse the specified <see cref="string"/> value into a <see cref="BigDecimal"/> value.
    /// </summary>
    /// <param name="value">The value to parse.</param>
    /// <param name="provider">An object that provides culture-specific information about the specified value.</param>
    /// <param name="result">
    /// On return, contains the result of parsing the specified value,
    /// or the default value in the event that the specified value could not be parsed.
    /// </param>
    /// <returns>Returns true if the specified value was parsed successfully; otherwise, false.</returns>
    public static bool TryParse(ReadOnlySpan<char> value, IFormatProvider? provider, out BigDecimal result)
    {
        return TryParse(value, DefaultNumberStyles, provider, out result);
    }

    /// <summary>
    /// Tries to parse the specified <see cref="string"/> value into a <see cref="BigDecimal"/> value.
    /// </summary>
    /// <param name="value">The value to parse.</param>
    /// <param name="style">A bitwise combination of number styles that can be present in the specified value.</param>
    /// <param name="provider">An object that provides culture-specific information about the specified value.</param>
    /// <param name="result">
    /// On return, contains the result of parsing the specified value,
    /// or the default value in the event that the specified value could not be parsed.
    /// </param>
    /// <returns>Returns true if the specified value was parsed successfully; otherwise, false.</returns>
    public static bool TryParse(ReadOnlySpan<char> value, NumberStyles style, IFormatProvider? provider, out BigDecimal result)
    {
        try
        {
            result = Parse(value, style, provider);
            return true;
        }
        catch
        {
            result = Zero;
            return false;
        }
    }
}
