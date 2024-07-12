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

using System.ComponentModel;
using System.Text;

namespace OnixLabs.Core.Text;

/// <summary>
/// Provides extension methods for string builders.
/// </summary>
// ReSharper disable UnusedMethodReturnValue.Global
[EditorBrowsable(EditorBrowsableState.Never)]
public static class StringBuilderExtensions
{
    private const char EscapeSequence = '\\';

    /// <summary>
    /// Appends the specified values to the current <see cref="StringBuilder"/>.
    /// </summary>
    /// <param name="builder">The <see cref="StringBuilder"/> to append to.</param>
    /// <param name="values">The values to append.</param>
    /// <returns>Returns the current <see cref="StringBuilder"/> with the specified values appended.</returns>
    public static StringBuilder Append(this StringBuilder builder, params object[] values) => builder.AppendJoin(string.Empty, values);

    /// <summary>
    /// Appends the specified value, prefixed with the escape sequence to the current <see cref="StringBuilder"/>.
    /// </summary>
    /// <param name="builder">The <see cref="StringBuilder"/> to append to.</param>
    /// <param name="value">The value to append.</param>
    /// <returns>Returns the current <see cref="StringBuilder"/> with the escape sequence and specified value appended.</returns>
    internal static StringBuilder AppendEscaped(this StringBuilder builder, char value) => builder.Append(EscapeSequence).Append(value);

    /// <summary>
    /// Prepends the specified value to the current <see cref="StringBuilder"/>
    /// </summary>
    /// <param name="builder">The <see cref="StringBuilder"/> to prepend to.</param>
    /// <param name="value">The value to prepend.</param>
    /// <returns>Returns the current <see cref="StringBuilder"/> with the specified values prepended.</returns>
    public static StringBuilder Prepend(this StringBuilder builder, object value) => builder.Insert(0, value);

    /// <summary>
    /// Prepends the specified values to the current <see cref="StringBuilder"/>.
    /// </summary>
    /// <param name="builder">The <see cref="StringBuilder"/> to prepend to.</param>
    /// <param name="values">The values to prepend.</param>
    /// <returns>Returns the current <see cref="StringBuilder"/> with the specified values prepended.</returns>
    public static StringBuilder Prepend(this StringBuilder builder, params object[] values) => builder.PrependJoin(string.Empty, values);

    /// <summary>
    /// Concatenates the string representations of the elements in the provided array of objects, using the specified separator between each member, then prepends the result to the current instance of the string builder.
    /// </summary>
    /// <param name="builder">The <see cref="StringBuilder"/> to prepend to.</param>
    /// <param name="separator">The string to use as a separator. <paramref name="separator" /> is included in the joined strings only if <paramref name="values" /> has more than one element.</param>
    /// <param name="values">An array that contains the strings to concatenate and append to the current instance of the string builder.</param>
    /// <returns>Returns the current <see cref="StringBuilder"/> with the specified values joined and prepended.</returns>
    public static StringBuilder PrependJoin(this StringBuilder builder, string separator, params object?[] values) =>
        builder.Prepend(string.Join(separator, values));

    /// <summary>
    /// Trims the specified <see cref="char"/> value from the start and end of the current <see cref="StringBuilder"/>.
    /// </summary>
    /// <param name="builder">The <see cref="StringBuilder"/> to trim.</param>
    /// <param name="value">The <see cref="char"/> value to trim.</param>
    /// <returns>Returns the current <see cref="StringBuilder"/> with the specified <see cref="char"/> value trimmed from the start and end.</returns>
    public static StringBuilder Trim(this StringBuilder builder, char value) => builder.TrimStart(value).TrimEnd(value);

    /// <summary>
    /// Trims the specified <see cref="char"/> value from the end of the current <see cref="StringBuilder"/>.
    /// </summary>
    /// <param name="builder">The <see cref="StringBuilder"/> to trim.</param>
    /// <param name="value">The <see cref="char"/> value to trim.</param>
    /// <returns>Returns the current <see cref="StringBuilder"/> with the specified <see cref="char"/> value trimmed from the end.</returns>
    public static StringBuilder TrimEnd(this StringBuilder builder, char value)
    {
        while (builder.Length > 0 && builder[^1] == value)
            builder.Remove(builder.Length - 1, 1);

        return builder;
    }

    /// <summary>
    /// Trims the specified <see cref="char"/> value from the start of the current <see cref="StringBuilder"/>.
    /// </summary>
    /// <param name="builder">The <see cref="StringBuilder"/> to trim.</param>
    /// <param name="value">The <see cref="char"/> value to trim.</param>
    /// <returns>Returns the current <see cref="StringBuilder"/> with the specified <see cref="char"/> value trimmed from the start.</returns>
    public static StringBuilder TrimStart(this StringBuilder builder, char value)
    {
        while (builder.Length > 0 && builder[0] == value)
            builder.Remove(0, 1);

        return builder;
    }

    /// <summary>
    /// Trims the specified <see cref="string"/> value from the start and end of the current <see cref="StringBuilder"/>.
    /// </summary>
    /// <param name="builder">The <see cref="StringBuilder"/> to trim.</param>
    /// <param name="value">The <see cref="string"/> value to trim.</param>
    /// <returns>Returns the current <see cref="StringBuilder"/> with the specified <see cref="string"/> value trimmed from the start and end.</returns>
    public static StringBuilder Trim(this StringBuilder builder, string value) => builder.TrimStart(value).TrimEnd(value);

    /// <summary>
    /// Trims the specified <see cref="string"/> value from the end of the current <see cref="StringBuilder"/>.
    /// </summary>
    /// <param name="builder">The <see cref="StringBuilder"/> to trim.</param>
    /// <param name="value">The <see cref="string"/> value to trim.</param>
    /// <returns>Returns the current <see cref="StringBuilder"/> with the specified <see cref="string"/> value trimmed from the end.</returns>
    public static StringBuilder TrimEnd(this StringBuilder builder, string value)
    {
        if (string.IsNullOrEmpty(value)) return builder;

        while (builder.Length >= value.Length && builder.ToString(builder.Length - value.Length, value.Length) == value)
            builder.Remove(builder.Length - value.Length, value.Length);

        return builder;
    }

    /// <summary>
    /// Trims the specified <see cref="string"/> value from the start of the current <see cref="StringBuilder"/>.
    /// </summary>
    /// <param name="builder">The <see cref="StringBuilder"/> to trim.</param>
    /// <param name="value">The <see cref="string"/> value to trim.</param>
    /// <returns>Returns the current <see cref="StringBuilder"/> with the specified <see cref="string"/> value trimmed from the start.</returns>
    public static StringBuilder TrimStart(this StringBuilder builder, string value)
    {
        if (string.IsNullOrEmpty(value)) return builder;

        while (builder.Length >= value.Length && builder.ToString(0, value.Length) == value)
            builder.Remove(0, value.Length);

        return builder;
    }

    /// <summary>
    /// Wraps the current <see cref="StringBuilder"/> between the specified start and end <see cref="char"/> values.
    /// </summary>
    /// <param name="builder">The <see cref="StringBuilder"/> to wrap.</param>
    /// <param name="start">The <see cref="char"/> value to prepend.</param>
    /// <param name="end">The <see cref="char"/> value to append.</param>
    /// <returns>Returns the current <see cref="StringBuilder"/> wrapped between the specified start and end <see cref="char"/> values.</returns>
    public static StringBuilder Wrap(this StringBuilder builder, char start, char end) => builder.Prepend(start).Append(end);

    /// <summary>
    /// Wraps the current <see cref="StringBuilder"/> between the specified start and end <see cref="string"/> values.
    /// </summary>
    /// <param name="builder">The <see cref="StringBuilder"/> to wrap.</param>
    /// <param name="start">The <see cref="string"/> value to prepend.</param>
    /// <param name="end">The <see cref="string"/> value to append.</param>
    /// <returns>Returns the current <see cref="StringBuilder"/> wrapped between the specified start and end <see cref="string"/> values.</returns>
    public static StringBuilder Wrap(this StringBuilder builder, string start, string end) => builder.Prepend(start).Append(end);
}
