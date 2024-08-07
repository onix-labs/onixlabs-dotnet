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

namespace OnixLabs.Numerics;

public readonly partial struct NumberInfo
{
    /// <summary>
    /// Parses the specified <see cref="string"/> value into a <see cref="NumberInfo"/> value.
    /// </summary>
    /// <param name="value">The value to parse.</param>
    /// <param name="provider">An object that provides culture-specific information about the specified value.</param>
    /// <returns>Returns a new <see cref="NumberInfo"/> instance parsed from the specified value.</returns>
    public static NumberInfo Parse(string value, IFormatProvider? provider = null) => Parse(value.AsSpan(), provider);

    /// <summary>
    /// Parses the specified <see cref="string"/> value into a <see cref="NumberInfo"/> value.
    /// </summary>
    /// <param name="value">The value to parse.</param>
    /// <param name="style">A bitwise combination of number styles that can be present in the specified value.</param>
    /// <param name="provider">An object that provides culture-specific information about the specified value.</param>
    /// <returns>Returns a new <see cref="NumberInfo"/> instance parsed from the specified value.</returns>
    public static NumberInfo Parse(string value, NumberStyles style, IFormatProvider? provider = null) => Parse(value.AsSpan(), style, provider);

    /// <summary>
    /// Parses the specified <see cref="ReadOnlySpan{T}"/> value into a <see cref="NumberInfo"/> value.
    /// </summary>
    /// <param name="value">The value to parse.</param>
    /// <param name="provider">An object that provides culture-specific information about the specified value.</param>
    /// <returns>Returns a new <see cref="NumberInfo"/> instance parsed from the specified value.</returns>
    public static NumberInfo Parse(ReadOnlySpan<char> value, IFormatProvider? provider = null) => Parse(value, DefaultNumberStyles, provider);

    /// <summary>
    /// Parses the specified <see cref="ReadOnlySpan{T}"/> value into a <see cref="NumberInfo"/> value.
    /// </summary>
    /// <param name="value">The value to parse.</param>
    /// <param name="style">A bitwise combination of number styles that can be present in the specified value.</param>
    /// <param name="provider">An object that provides culture-specific information about the specified value.</param>
    /// <returns>Returns a new <see cref="NumberInfo"/> instance parsed from the specified value.</returns>
    // ReSharper disable once MemberCanBePrivate.Global
    public static NumberInfo Parse(ReadOnlySpan<char> value, NumberStyles style, IFormatProvider? provider = null)
    {
        CultureInfo info = provider as CultureInfo ?? DefaultCulture;
        if (TryParse(value, style, info, out NumberInfo result)) return result;
        throw new FormatException($"The input string '{value}' was not in a correct format.");
    }

    /// <summary>
    /// Tries to parse the specified <see cref="string"/> value into a <see cref="NumberInfo"/> value.
    /// </summary>
    /// <param name="value">The value to parse.</param>
    /// <param name="result">
    /// On return, contains the result of parsing the specified value,
    /// or the default value in the event that the specified value could not be parsed.
    /// </param>
    /// <returns>Returns <see langword="true"/> if the specified value was parsed successfully; otherwise, <see langword="false"/>.</returns>
    public static bool TryParse(string? value, out NumberInfo result) => TryParse(value.AsSpan(), out result);

    /// <summary>
    /// Tries to parse the specified <see cref="string"/> value into a <see cref="NumberInfo"/> value.
    /// </summary>
    /// <param name="value">The value to parse.</param>
    /// <param name="provider">An object that provides culture-specific information about the specified value.</param>
    /// <param name="result">
    /// On return, contains the result of parsing the specified value,
    /// or the default value in the event that the specified value could not be parsed.
    /// </param>
    /// <returns>Returns <see langword="true"/> if the specified value was parsed successfully; otherwise, <see langword="false"/>.</returns>
    public static bool TryParse(string? value, IFormatProvider? provider, out NumberInfo result) => TryParse(value.AsSpan(), provider, out result);

    /// <summary>
    /// Tries to parse the specified <see cref="string"/> value into a <see cref="NumberInfo"/> value.
    /// </summary>
    /// <param name="value">The value to parse.</param>
    /// <param name="style">A bitwise combination of number styles that can be present in the specified value.</param>
    /// <param name="provider">An object that provides culture-specific information about the specified value.</param>
    /// <param name="result">
    /// On return, contains the result of parsing the specified value,
    /// or the default value in the event that the specified value could not be parsed.
    /// </param>
    /// <returns>Returns <see langword="true"/> if the specified value was parsed successfully; otherwise, <see langword="false"/>.</returns>
    public static bool TryParse(string? value, NumberStyles style, IFormatProvider? provider, out NumberInfo result) => TryParse(value.AsSpan(), style, provider, out result);

    /// <summary>
    /// Tries to parse the specified <see cref="string"/> value into a <see cref="NumberInfo"/> value.
    /// </summary>
    /// <param name="value">The value to parse.</param>
    /// <param name="result">
    /// On return, contains the result of parsing the specified value,
    /// or the default value in the event that the specified value could not be parsed.
    /// </param>
    /// <returns>Returns <see langword="true"/> if the specified value was parsed successfully; otherwise, <see langword="false"/>.</returns>
    // ReSharper disable once MemberCanBePrivate.Global
    public static bool TryParse(ReadOnlySpan<char> value, out NumberInfo result) => TryParse(value, DefaultCulture, out result);

    /// <summary>
    /// Tries to parse the specified <see cref="string"/> value into a <see cref="NumberInfo"/> value.
    /// </summary>
    /// <param name="value">The value to parse.</param>
    /// <param name="provider">An object that provides culture-specific information about the specified value.</param>
    /// <param name="result">
    /// On return, contains the result of parsing the specified value,
    /// or the default value in the event that the specified value could not be parsed.
    /// </param>
    /// <returns>Returns <see langword="true"/> if the specified value was parsed successfully; otherwise, <see langword="false"/>.</returns>
    public static bool TryParse(ReadOnlySpan<char> value, IFormatProvider? provider, out NumberInfo result) => TryParse(value, DefaultNumberStyles, provider, out result);

    /// <summary>
    /// Tries to parse the specified <see cref="string"/> value into a <see cref="NumberInfo"/> value.
    /// </summary>
    /// <param name="value">The value to parse.</param>
    /// <param name="style">A bitwise combination of number styles that can be present in the specified value.</param>
    /// <param name="provider">An object that provides culture-specific information about the specified value.</param>
    /// <param name="result">
    /// On return, contains the result of parsing the specified value,
    /// or the default value in the event that the specified value could not be parsed.
    /// </param>
    /// <returns>Returns <see langword="true"/> if the specified value was parsed successfully; otherwise, <see langword="false"/>.</returns>
    // ReSharper disable once MemberCanBePrivate.Global
    public static bool TryParse(ReadOnlySpan<char> value, NumberStyles style, IFormatProvider? provider, out NumberInfo result)
    {
        CultureInfo info = provider as CultureInfo ?? DefaultCulture;
        // ReSharper disable once HeapView.ObjectAllocation.Evident
        NumberInfoParser parser = new(style, info);
        return parser.TryParse(value, out result);
    }
}
