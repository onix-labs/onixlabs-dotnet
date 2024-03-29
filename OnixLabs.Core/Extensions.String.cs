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
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;

namespace OnixLabs.Core;

/// <summary>
/// Provides extension methods for strings.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public static class StringExtensions
{
    /// <summary>
    /// The value of an index not found.
    /// </summary>
    private const int IndexNotFound = -1;

    /// <summary>
    /// Repeats the current <see cref="string"/> by the specified number of repetitions.
    /// </summary>
    /// <param name="value">The <see cref="string"/> value to repeat.</param>
    /// <param name="count">The specified number of repetitions to repeat by.</param>
    /// <returns>Returns a new <see cref="string"/> containing the repeated value.</returns>
    public static string Repeat(this string value, int count)
    {
        if (count <= 0) return string.Empty;
        IEnumerable<string> repetitions = Enumerable.Repeat(value, count);
        return string.Join(string.Empty, repetitions);
    }

    /// <summary>
    /// Returns a substring before the first occurrence of the specified delimiter.
    /// </summary>
    /// <param name="value">The original <see cref="string"/> from which to obtain a sub-string.</param>
    /// <param name="delimiter">The delimiter to find within the original string.</param>
    /// <param name="defaultValue">The value to return if the delimiter is not found, which defaults to the original string.</param>
    /// <returns>Returns a substring before the first occurrence of the specified delimiter.</returns>
    public static string SubstringBeforeFirst(this string value, char delimiter, string? defaultValue = null)
    {
        int index = value.IndexOf(delimiter);

        return index switch
        {
            IndexNotFound => defaultValue ?? value,
            _ => value[..index]
        };
    }

    /// <summary>
    /// Returns a substring before the first occurrence of the specified delimiter.
    /// </summary>
    /// <param name="value">The original <see cref="string"/> from which to obtain a sub-string.</param>
    /// <param name="delimiter">The delimiter to find within the original string.</param>
    /// <param name="defaultValue">The value to return if the delimiter is not found, which defaults to the original string.</param>
    /// <param name="comparison">Specifies the string comparison rule for the search.</param>
    /// <returns>Returns a substring before the first occurrence of the specified delimiter.</returns>
    public static string SubstringBeforeFirst(
        this string value,
        string delimiter,
        string? defaultValue = null,
        StringComparison comparison = StringComparison.Ordinal)
    {
        int index = value.IndexOf(delimiter, comparison);

        return index switch
        {
            IndexNotFound => defaultValue ?? value,
            _ => value[..index]
        };
    }

    /// <summary>
    /// Returns a substring before the last occurrence of the specified delimiter.
    /// </summary>
    /// <param name="value">The original <see cref="string"/> from which to obtain a sub-string.</param>
    /// <param name="delimiter">The delimiter to find within the original string.</param>
    /// <param name="defaultValue">The value to return if the delimiter is not found, which defaults to the original string.</param>
    /// <returns>Returns a substring before the last occurrence of the specified delimiter.</returns>
    public static string SubstringBeforeLast(this string value, char delimiter, string? defaultValue = null)
    {
        int index = value.LastIndexOf(delimiter);

        return index switch
        {
            IndexNotFound => defaultValue ?? value,
            _ => value[..index]
        };
    }

    /// <summary>
    /// Returns a substring before the last occurrence of the specified delimiter.
    /// </summary>
    /// <param name="value">The original <see cref="string"/> from which to obtain a sub-string.</param>
    /// <param name="delimiter">The delimiter to find within the original string.</param>
    /// <param name="defaultValue">The value to return if the delimiter is not found, which defaults to the original string.</param>
    /// <param name="comparison">Specifies the string comparison rule for the search.</param>
    /// <returns>Returns a substring before the last occurrence of the specified delimiter.</returns>
    public static string SubstringBeforeLast(
        this string value,
        string delimiter,
        string? defaultValue = null,
        StringComparison comparison = StringComparison.Ordinal)
    {
        int index = value.LastIndexOf(delimiter, comparison);

        return index switch
        {
            IndexNotFound => defaultValue ?? value,
            _ => value[..index]
        };
    }

    /// <summary>
    /// Returns a substring after the first occurrence of the specified delimiter.
    /// </summary>
    /// <param name="value">The original <see cref="string"/> from which to obtain a sub-string.</param>
    /// <param name="delimiter">The delimiter to find within the original string.</param>
    /// <param name="defaultValue">The value to return if the delimiter is not found, which defaults to the original string.</param>
    /// <returns>Returns a substring after the first occurrence of the specified delimiter.</returns>
    public static string SubstringAfterFirst(this string value, char delimiter, string? defaultValue = null)
    {
        int index = value.IndexOf(delimiter);

        return index switch
        {
            IndexNotFound => defaultValue ?? value,
            _ => value[(index + 1)..value.Length]
        };
    }

    /// <summary>
    /// Returns a substring after the first occurrence of the specified delimiter.
    /// </summary>
    /// <param name="value">The original <see cref="string"/> from which to obtain a sub-string.</param>
    /// <param name="delimiter">The delimiter to find within the original string.</param>
    /// <param name="defaultValue">The value to return if the delimiter is not found, which defaults to the original string.</param>
    /// <param name="comparison">Specifies the string comparison rule for the search.</param>
    /// <returns>Returns a substring after the first occurrence of the specified delimiter.</returns>
    public static string SubstringAfterFirst(
        this string value,
        string delimiter,
        string? defaultValue = null,
        StringComparison comparison = StringComparison.Ordinal)
    {
        int index = value.IndexOf(delimiter, comparison);

        return index switch
        {
            IndexNotFound => defaultValue ?? value,
            _ => value[(index + delimiter.Length)..value.Length]
        };
    }

    /// <summary>
    /// Returns a substring after the last occurrence of the specified delimiter.
    /// </summary>
    /// <param name="value">The original <see cref="string"/> from which to obtain a sub-string.</param>
    /// <param name="delimiter">The delimiter to find within the original string.</param>
    /// <param name="defaultValue">The value to return if the delimiter is not found, which defaults to the original string.</param>
    /// <returns>Returns a substring after the last occurrence of the specified delimiter.</returns>
    public static string SubstringAfterLast(this string value, char delimiter, string? defaultValue = null)
    {
        int index = value.LastIndexOf(delimiter);

        return index switch
        {
            IndexNotFound => defaultValue ?? value,
            _ => value[(index + 1)..value.Length]
        };
    }

    /// <summary>
    /// Returns a substring after the last occurrence of the specified delimiter.
    /// </summary>
    /// <param name="value">The original <see cref="string"/> from which to obtain a sub-string.</param>
    /// <param name="delimiter">The delimiter to find within the original string.</param>
    /// <param name="defaultValue">The value to return if the delimiter is not found, which defaults to the original string.</param>
    /// <param name="comparison">Specifies the string comparison rule for the search.</param>
    /// <returns>Returns a substring after the last occurrence of the specified delimiter.</returns>
    public static string SubstringAfterLast(
        this string value,
        string delimiter,
        string? defaultValue = null,
        StringComparison comparison = StringComparison.Ordinal)
    {
        int index = value.LastIndexOf(delimiter, comparison);

        return index switch
        {
            IndexNotFound => defaultValue ?? value,
            _ => value[(index + delimiter.Length)..value.Length]
        };
    }

    /// <summary>
    /// Converts the current <see cref="string"/> to a <see cref="byte"/> array using the default <see cref="Encoding"/>.
    /// </summary>
    /// <param name="value">The original <see cref="string"/> from which to obtain a byte array.</param>
    /// <returns>Returns a <see cref="byte"/> array using the default <see cref="Encoding"/>.</returns>
    public static byte[] ToByteArray(this string value)
    {
        return ToByteArray(value, Encoding.Default);
    }

    /// <summary>
    /// Converts the current <see cref="string"/> to a <see cref="byte"/> array using the specified <see cref="Encoding"/>.
    /// </summary>
    /// <param name="value">The original <see cref="string"/> from which to obtain a byte array.</param>
    /// <param name="encoding">The <see cref="Encoding"/> which will be used to convert the current <see cref="string"/> to a <see cref="byte"/> array.</param>
    /// <returns>Returns a <see cref="byte"/> array using the specified <see cref="Encoding"/>.</returns>
    public static byte[] ToByteArray(this string value, Encoding encoding)
    {
        return encoding.GetBytes(value);
    }

    /// <summary>
    /// Converts the current <see cref="string"/> to a <see cref="DateTime"/>.
    /// </summary>
    /// <param name="value">The original <see cref="string"/> from which to obtain a <see cref="DateTime"/>.</param>
    /// <param name="provider">An object that supplies culture-specific formatting information.</param>
    /// <param name="styles">A bitwise combination of the enumeration values that indicates the style elements that can be present.</param>
    /// <returns>Returns a <see cref="DateTime"/> parsed from the current <see cref="string"/> value.</returns>
    public static DateTime ToDateTime(this string value, IFormatProvider? provider = null, DateTimeStyles styles = DateTimeStyles.None)
    {
        return DateTime.Parse(value, provider, styles);
    }

    /// <summary>
    /// Converts the current <see cref="string"/> to a <see cref="DateOnly"/>.
    /// </summary>
    /// <param name="value">The original <see cref="string"/> from which to obtain a <see cref="DateOnly"/>.</param>
    /// <param name="provider">An object that supplies culture-specific formatting information.</param>
    /// <param name="styles">A bitwise combination of the enumeration values that indicates the style elements that can be present.</param>
    /// <returns>Returns a <see cref="DateOnly"/> parsed from the current <see cref="string"/> value.</returns>
    public static DateOnly ToDateOnly(this string value, IFormatProvider? provider = null, DateTimeStyles styles = DateTimeStyles.None)
    {
        return DateOnly.Parse(value, provider, styles);
    }

    /// <summary>
    /// Converts the current <see cref="string"/> to a <see cref="TimeOnly"/>.
    /// </summary>
    /// <param name="value">The original <see cref="string"/> from which to obtain a <see cref="TimeOnly"/>.</param>
    /// <param name="provider">An object that supplies culture-specific formatting information.</param>
    /// <param name="styles">A bitwise combination of the enumeration values that indicates the style elements that can be present.</param>
    /// <returns>Returns a <see cref="TimeOnly"/> parsed from the current <see cref="string"/> value.</returns>
    public static TimeOnly ToTimeOnly(this string value, IFormatProvider? provider = null, DateTimeStyles styles = DateTimeStyles.None)
    {
        return TimeOnly.Parse(value, provider, styles);
    }

    /// <summary>
    /// Converts the current <see cref="string"/> between the specified before and after <see cref="string"/> values.
    /// </summary>
    /// <param name="value">The current value to wrap.</param>
    /// <param name="before">The <see cref="string"/> that should precede the current value.</param>
    /// <param name="after">The <see cref="string"/> that should succeed the current value.</param>
    /// <returns>Returns the current <see cref="string"/> wrapped between the specified before and after <see cref="string"/> values.</returns>
    public static string Wrap(this string value, string before, string after)
    {
        return $"{before}{value}{after}";
    }
}
