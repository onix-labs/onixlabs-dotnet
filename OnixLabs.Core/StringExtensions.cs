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
using System.ComponentModel;
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
    /// Returns a substring before the first occurrence of the specified delimiter.
    /// </summary>
    /// <param name="value">The original <see cref="string"/> from which to obtain a sub-string.</param>
    /// <param name="delimiter">The delimiter to find within the original string.</param>
    /// <param name="defaultValue">The value to return if the delimiter is not found, which defaults to the original string.</param>
    /// <returns>Returns a substring before the first occurrence of the specified delimiter.</returns>
    public static string SubstringBefore(this string value, char delimiter, string? defaultValue = null)
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
    public static string SubstringBefore(
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
    public static string SubstringAfter(this string value, char delimiter, string? defaultValue = null)
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
    public static string SubstringAfter(
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
