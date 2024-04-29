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
    private const int NotFound = -1;

    /// <summary>
    /// The default string comparison.
    /// </summary>
    private const StringComparison DefaultComparison = StringComparison.Ordinal;

    /// <summary>
    /// The default date/time styles.
    /// </summary>
    private const DateTimeStyles DefaultStyles = DateTimeStyles.None;

    /// <summary>
    /// The default encoding.
    /// </summary>
    private static readonly Encoding DefaultEncoding = Encoding.UTF8;

    /// <summary>
    /// Repeats the current <see cref="System.String"/> by the specified number of repetitions.
    /// </summary>
    /// <param name="value">The <see cref="System.String"/> instance to repeat.</param>
    /// <param name="count">The number of repetitions of the current <see cref="System.String"/> instance.</param>
    /// <returns>Returns a new <see cref="System.String"/> instance representing the repetition of the current <see cref="System.String"/> instance.</returns>
    public static string Repeat(this string value, int count)
    {
        return count > 0 ? string.Join(string.Empty, Enumerable.Repeat(value, count)) : string.Empty;
    }

    /// <summary>
    /// Obtains a sub-string before the specified index within the current <see cref="System.String"/> instance.
    /// </summary>
    /// <param name="value">The current <see cref="System.String"/> instance from which to obtain a sub-string.</param>
    /// <param name="index">The index in the current <see cref="System.String"/> from which to obtain a sub-string.</param>
    /// <param name="defaultValue">
    /// The <see cref="System.String"/> value to return in the event that the index is less than zero.
    /// If the default value is <see langword="null"/> then the current <see cref="System.String"/> instance is returned.
    /// </param>
    /// <returns>
    /// Returns a sub-string before the specified index. If the index is less than zero, then the default value will be returned.
    /// If the default value is <see langword="null"/>, then the current <see cref="System.String"/> instance will be returned.
    /// </returns>
    private static string SubstringBeforeIndex(this string value, int index, string? defaultValue = null)
    {
        return index <= NotFound ? defaultValue ?? value : value[..index];
    }

    /// <summary>
    /// Obtains a sub-string after the specified index within the current <see cref="System.String"/> instance.
    /// </summary>
    /// <param name="value">The current <see cref="System.String"/> instance from which to obtain a sub-string.</param>
    /// <param name="index">The index in the current <see cref="System.String"/> from which to obtain a sub-string.</param>
    /// <param name="offset">The offset from the index which marks the beginning of the sub-string.</param>
    /// <param name="defaultValue">
    /// The <see cref="System.String"/> value to return in the event that the index is less than zero.
    /// If the default value is <see langword="null"/> then the current <see cref="System.String"/> instance is returned.
    /// </param>
    /// <returns>
    /// Returns a sub-string after the specified index. If the index is less than zero, then the default value will be returned.
    /// If the default value is <see langword="null"/>, then the current <see cref="System.String"/> instance will be returned.
    /// </returns>
    private static string SubstringAfterIndex(this string value, int index, int offset, string? defaultValue = null)
    {
        return index <= NotFound ? defaultValue ?? value : value[(index + offset)..value.Length];
    }

    /// <summary>
    /// Obtains a sub-string before the first occurrence of the specified delimiter within the current <see cref="System.String"/> instance.
    /// </summary>
    /// <param name="value">The current <see cref="System.String"/> instance from which to obtain a sub-string.</param>
    /// <param name="delimiter">The delimiter to find within the current <see cref="System.String"/> instance.</param>
    /// <param name="defaultValue">
    /// The <see cref="System.String"/> value to return in the event that the delimiter is not found in the current <see cref="System.String"/> instance.
    /// If the default value is <see langword="null"/> then the current <see cref="System.String"/> instance is returned.
    /// </param>
    /// <returns>
    /// Returns a sub-string before the first occurrence of the specified delimiter within the current <see cref="System.String"/> instance.
    /// If the delimiter is not found, then the default value will be returned.
    /// If the default value is <see langword="null"/>, then the current <see cref="System.String"/> instance is returned.
    /// </returns>
    public static string SubstringBeforeFirst(this string value, char delimiter, string? defaultValue = null)
    {
        return value.SubstringBeforeIndex(value.IndexOf(delimiter), defaultValue);
    }

    /// <summary>
    /// Obtains a sub-string before the first occurrence of the specified delimiter within the current <see cref="System.String"/> instance.
    /// </summary>
    /// <param name="value">The current <see cref="System.String"/> instance from which to obtain a sub-string.</param>
    /// <param name="delimiter">The delimiter to find within the current <see cref="System.String"/> instance.</param>
    /// <param name="defaultValue">
    /// The <see cref="System.String"/> value to return in the event that the delimiter is not found in the current <see cref="System.String"/> instance.
    /// If the default value is <see langword="null"/> then the current <see cref="System.String"/> instance is returned.
    /// </param>
    /// <param name="comparison">
    /// The culture, case and sort rules that will be applied when searching for the specified delimiter.
    /// The default value is <see cref="StringComparison.Ordinal"/>.
    /// </param>
    /// <returns>
    ///Returns a sub-string before the first occurrence of the specified delimiter within the current <see cref="System.String"/> instance.
    /// If the delimiter is not found, then the default value will be returned.
    /// If the default value is <see langword="null"/>, then the current <see cref="System.String"/> instance is returned.
    /// </returns>
    public static string SubstringBeforeFirst(this string value, string delimiter, string? defaultValue = null, StringComparison comparison = DefaultComparison)
    {
        return value.SubstringBeforeIndex(value.IndexOf(delimiter, comparison), defaultValue);
    }

    /// <summary>
    /// Obtains a sub-string before the last occurrence of the specified delimiter within the current <see cref="System.String"/> instance.
    /// </summary>
    /// <param name="value">The current <see cref="System.String"/> instance from which to obtain a sub-string.</param>
    /// <param name="delimiter">The delimiter to find within the current <see cref="System.String"/> instance.</param>
    /// <param name="defaultValue">
    /// The <see cref="System.String"/> value to return in the event that the delimiter is not found in the current <see cref="System.String"/> instance.
    /// If the default value is <see langword="null"/> then the current <see cref="System.String"/> instance is returned.
    /// </param>
    /// <returns>
    ///Returns a sub-string before the last occurrence of the specified delimiter within the current <see cref="System.String"/> instance.
    /// If the delimiter is not found, then the default value will be returned.
    /// If the default value is <see langword="null"/>, then the current <see cref="System.String"/> instance is returned.
    /// </returns>
    public static string SubstringBeforeLast(this string value, char delimiter, string? defaultValue = null)
    {
        return value.SubstringBeforeIndex(value.LastIndexOf(delimiter), defaultValue);
    }

    /// <summary>
    /// Obtains a sub-string before the last occurrence of the specified delimiter within the current <see cref="System.String"/> instance.
    /// </summary>
    /// <param name="value">The current <see cref="System.String"/> instance from which to obtain a sub-string.</param>
    /// <param name="delimiter">The delimiter to find within the current <see cref="System.String"/> instance.</param>
    /// <param name="defaultValue">
    /// The <see cref="System.String"/> value to return in the event that the delimiter is not found in the current <see cref="System.String"/> instance.
    /// If the default value is <see langword="null"/> then the current <see cref="System.String"/> instance is returned.
    /// </param>
    /// <param name="comparison">
    /// The culture, case and sort rules that will be applied when searching for the specified delimiter.
    /// The default value is <see cref="StringComparison.Ordinal"/>.
    /// </param>
    /// <returns>
    ///Returns a sub-string before the last occurrence of the specified delimiter within the current <see cref="System.String"/> instance.
    /// If the delimiter is not found, then the default value will be returned.
    /// If the default value is <see langword="null"/>, then the current <see cref="System.String"/> instance is returned.
    /// </returns>
    public static string SubstringBeforeLast(this string value, string delimiter, string? defaultValue = null, StringComparison comparison = DefaultComparison)
    {
        return value.SubstringBeforeIndex(value.LastIndexOf(delimiter, comparison), defaultValue);
    }

    /// <summary>
    /// Obtains a sub-string after the first occurrence of the specified delimiter within the current <see cref="System.String"/> instance.
    /// </summary>
    /// <param name="value">The current <see cref="System.String"/> instance from which to obtain a sub-string.</param>
    /// <param name="delimiter">The delimiter to find within the current <see cref="System.String"/> instance.</param>
    /// <param name="defaultValue">
    /// The <see cref="System.String"/> value to return in the event that the delimiter is not found in the current <see cref="System.String"/> instance.
    /// If the default value is <see langword="null"/> then the current <see cref="System.String"/> instance is returned.
    /// </param>
    /// <returns>
    /// Returns a sub-string after the first occurrence of the specified delimiter within the current <see cref="System.String"/> instance.
    /// If the delimiter is not found, then the default value will be returned.
    /// If the default value is <see langword="null"/>, then the current <see cref="System.String"/> instance is returned.
    /// </returns>
    public static string SubstringAfterFirst(this string value, char delimiter, string? defaultValue = null)
    {
        return value.SubstringAfterIndex(value.IndexOf(delimiter), 1, defaultValue);
    }

    /// <summary>
    /// Obtains a sub-string after the first occurrence of the specified delimiter within the current <see cref="System.String"/> instance.
    /// </summary>
    /// <param name="value">The current <see cref="System.String"/> instance from which to obtain a sub-string.</param>
    /// <param name="delimiter">The delimiter to find within the current <see cref="System.String"/> instance.</param>
    /// <param name="defaultValue">
    /// The <see cref="System.String"/> value to return in the event that the delimiter is not found in the current <see cref="System.String"/> instance.
    /// If the default value is <see langword="null"/> then the current <see cref="System.String"/> instance is returned.
    /// </param>
    /// <param name="comparison">
    /// The culture, case and sort rules that will be applied when searching for the specified delimiter.
    /// The default value is <see cref="StringComparison.Ordinal"/>.
    /// </param>
    /// <returns>
    ///Returns a sub-string after the first occurrence of the specified delimiter within the current <see cref="System.String"/> instance.
    /// If the delimiter is not found, then the default value will be returned.
    /// If the default value is <see langword="null"/>, then the current <see cref="System.String"/> instance is returned.
    /// </returns>
    public static string SubstringAfterFirst(this string value, string delimiter, string? defaultValue = null, StringComparison comparison = DefaultComparison)
    {
        return value.SubstringAfterIndex(value.IndexOf(delimiter, comparison), delimiter.Length, defaultValue);
    }

    /// <summary>
    /// Obtains a sub-string after the last occurrence of the specified delimiter within the current <see cref="System.String"/> instance.
    /// </summary>
    /// <param name="value">The current <see cref="System.String"/> instance from which to obtain a sub-string.</param>
    /// <param name="delimiter">The delimiter to find within the current <see cref="System.String"/> instance.</param>
    /// <param name="defaultValue">
    /// The <see cref="System.String"/> value to return in the event that the delimiter is not found in the current <see cref="System.String"/> instance.
    /// If the default value is <see langword="null"/> then the current <see cref="System.String"/> instance is returned.
    /// </param>
    /// <returns>
    ///Returns a sub-string after the last occurrence of the specified delimiter within the current <see cref="System.String"/> instance.
    /// If the delimiter is not found, then the default value will be returned.
    /// If the default value is <see langword="null"/>, then the current <see cref="System.String"/> instance is returned.
    /// </returns>
    public static string SubstringAfterLast(this string value, char delimiter, string? defaultValue = null)
    {
        return value.SubstringAfterIndex(value.LastIndexOf(delimiter), 1, defaultValue);
    }

    /// <summary>
    /// Obtains a sub-string after the last occurrence of the specified delimiter within the current <see cref="System.String"/> instance.
    /// </summary>
    /// <param name="value">The current <see cref="System.String"/> instance from which to obtain a sub-string.</param>
    /// <param name="delimiter">The delimiter to find within the current <see cref="System.String"/> instance.</param>
    /// <param name="defaultValue">
    /// The <see cref="System.String"/> value to return in the event that the delimiter is not found in the current <see cref="System.String"/> instance.
    /// If the default value is <see langword="null"/> then the current <see cref="System.String"/> instance is returned.
    /// </param>
    /// <param name="comparison">
    /// The culture, case and sort rules that will be applied when searching for the specified delimiter.
    /// The default value is <see cref="StringComparison.Ordinal"/>.
    /// </param>
    /// <returns>
    ///Returns a sub-string after the last occurrence of the specified delimiter within the current <see cref="System.String"/> instance.
    /// If the delimiter is not found, then the default value will be returned.
    /// If the default value is <see langword="null"/>, then the current <see cref="System.String"/> instance is returned.
    /// </returns>
    public static string SubstringAfterLast(this string value, string delimiter, string? defaultValue = null, StringComparison comparison = DefaultComparison)
    {
        return value.SubstringAfterIndex(value.LastIndexOf(delimiter, comparison), 1, defaultValue);
    }

    /// <summary>
    /// Converts the current <see cref="System.String"/> instance into a new <see cref="T:System.Byte[]"/> instance.
    /// </summary>
    /// <param name="value">The current <see cref="System.String"/> from which to obtain a <see cref="T:System.Byte[]"/>.</param>
    /// <param name="encoding">The encoding that will be used to convert the current <see cref="System.String"/> into a <see cref="T:System.Byte[]"/>.</param>
    /// <returns>Returns a new <see cref="T:System.Byte[]"/> representation of the current <see cref="System.String"/> instance.</returns>
    public static byte[] ToByteArray(this string value, Encoding? encoding = null)
    {
        return (encoding ?? DefaultEncoding).GetBytes(value);
    }

    /// <summary>
    /// Converts the current <see cref="System.String"/> instance into a new <see cref="System.DateTime"/> instance.
    /// </summary>
    /// <param name="value">The current <see cref="System.String"/> instance to convert.</param>
    /// <param name="provider">An object that provides culture-specific formatting information.</param>
    /// <param name="styles">A bit-wise combination of enumeration values that indicate the style elements that can be present.</param>
    /// <returns>Returns a new <see cref="System.DateTime"/> instance parsed from the current <see cref="System.String"/> instance.</returns>
    public static DateTime ToDateTime(this string value, IFormatProvider? provider = null, DateTimeStyles styles = DefaultStyles)
    {
        return DateTime.Parse(value, provider, styles);
    }

    /// <summary>
    /// Converts the current <see cref="System.String"/> instance into a new <see cref="System.DateOnly"/> instance.
    /// </summary>
    /// <param name="value">The current <see cref="System.String"/> instance to convert.</param>
    /// <param name="provider">An object that provides culture-specific formatting information.</param>
    /// <param name="styles">A bit-wise combination of enumeration values that indicate the style elements that can be present.</param>
    /// <returns>Returns a new <see cref="System.DateOnly"/> instance parsed from the current <see cref="System.String"/> instance.</returns>
    public static DateOnly ToDateOnly(this string value, IFormatProvider? provider = null, DateTimeStyles styles = DefaultStyles)
    {
        return DateOnly.Parse(value, provider, styles);
    }

    /// <summary>
    /// Converts the current <see cref="System.String"/> instance into a <see cref="System.TimeOnly"/> instance.
    /// </summary>
    /// <param name="value">The current <see cref="System.String"/> instance to convert.</param>
    /// <param name="provider">An object that provides culture-specific formatting information.</param>
    /// <param name="styles">A bit-wise combination of enumeration values that indicate the style elements that can be present.</param>
    /// <returns>Returns a new <see cref="System.TimeOnly"/> instance parsed from the current <see cref="System.String"/> instance.</returns>
    public static TimeOnly ToTimeOnly(this string value, IFormatProvider? provider = null, DateTimeStyles styles = DefaultStyles)
    {
        return TimeOnly.Parse(value, provider, styles);
    }

    /// <summary>
    /// Tries to copy the current <see cref="System.String"/> instance into the destination <see cref="System.Span{T}"/>.
    /// </summary>
    /// <param name="value">The current <see cref="System.String"/> instance to copy.</param>
    /// <param name="destination">The <see cref="System.Span{T}"/> into which the current <see cref="System.String"/> contents will be copied.</param>
    /// <param name="charsWritten">The number of characters written to the destination <see cref="System.Span{T}"/>.</param>
    /// <returns>Returns <see langword="true"/> if the current <see cref="System.String"/> instance was copied into the destination <see cref="System.Span{T}"/>; otherwise, <see langword="false"/> if the destination was too short.</returns>
    public static bool TryCopyTo(this string value, Span<char> destination, out int charsWritten)
    {
        bool result = value.TryCopyTo(destination);
        charsWritten = result ? value.Length : 0;
        return result;
    }

    /// <summary>
    /// Wraps the current <see cref="System.String"/> instance between the specified before and after <see cref="System.Char"/> instances.
    /// </summary>
    /// <param name="value">The current <see cref="System.String"/> instance to wrap.</param>
    /// <param name="before">The <see cref="System.Char"/> that should precede the current <see cref="System.String"/> instance.</param>
    /// <param name="after">The <see cref="System.Char"/> that should succeed the current <see cref="System.String"/> instance.</param>
    /// <returns>Returns a new <see cref="System.String"/> instance representing the current <see cref="System.String"/> instance, wrapped between the specified before and after <see cref="System.Char"/> instances.</returns>
    public static string Wrap(this string value, char before, char after)
    {
        return $"{before}{value}{after}";
    }

    /// <summary>
    /// Wraps the current <see cref="System.String"/> instance between the specified before and after <see cref="System.String"/> instances.
    /// </summary>
    /// <param name="value">The current <see cref="System.String"/> instance to wrap.</param>
    /// <param name="before">The <see cref="System.String"/> that should precede the current <see cref="System.String"/> instance.</param>
    /// <param name="after">The <see cref="System.String"/> that should succeed the current <see cref="System.String"/> instance.</param>
    /// <returns>Returns a new <see cref="System.String"/> instance representing the current <see cref="System.String"/> instance, wrapped between the specified before and after <see cref="System.String"/> instances.</returns>
    public static string Wrap(this string value, string before, string after)
    {
        return $"{before}{value}{after}";
    }
}
