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
/// Provides extension methods for <see cref="StringBuilder"/> instances.
/// </summary>
// ReSharper disable UnusedMethodReturnValue.Global
[EditorBrowsable(EditorBrowsableState.Never)]
public static class StringBuilderExtensions
{
    /// <summary>
    /// The backslash character prepended by <see cref="AppendEscaped"/> to escape the following character.
    /// </summary>
    private const char EscapeSequence = '\\';

    /// <summary>
    /// Appends the specified values to the current <see cref="StringBuilder"/>.
    /// </summary>
    /// <param name="values">The values to append.</param>
    /// <param name="receiver">The current <see cref="StringBuilder"/> instance.</param>
    /// <returns>Returns the current <see cref="StringBuilder"/> with the specified values appended.</returns>
    public static StringBuilder Append(this StringBuilder receiver, params object[] values) => receiver.AppendJoin(string.Empty, values);

    /// <summary>
    /// Appends the specified value, prefixed with the escape sequence to the current <see cref="StringBuilder"/>.
    /// </summary>
    /// <param name="value">The value to append.</param>
    /// <param name="receiver">The current <see cref="StringBuilder"/> instance.</param>
    /// <returns>Returns the current <see cref="StringBuilder"/> with the escape sequence and specified value appended.</returns>
    internal static StringBuilder AppendEscaped(this StringBuilder receiver, char value) => receiver.Append(EscapeSequence).Append(value);

    /// <summary>
    /// Prepends the specified <see cref="object"/> value to the current <see cref="StringBuilder"/>.
    /// </summary>
    /// <param name="value">The value to prepend.</param>
    /// <param name="receiver">The current <see cref="StringBuilder"/> instance.</param>
    /// <returns>Returns the current <see cref="StringBuilder"/> with the specified value prepended.</returns>
    public static StringBuilder Prepend(this StringBuilder receiver, object value) => receiver.Insert(0, value);

    /// <summary>
    /// Prepends the specified <see cref="char"/> value to the current <see cref="StringBuilder"/>.
    /// </summary>
    /// <param name="value">The value to prepend.</param>
    /// <param name="receiver">The current <see cref="StringBuilder"/> instance.</param>
    /// <returns>Returns the current <see cref="StringBuilder"/> with the specified value prepended.</returns>
    public static StringBuilder Prepend(this StringBuilder receiver, char value) => receiver.Insert(0, value);

    /// <summary>
    /// Prepends the specified <see cref="string"/> value to the current <see cref="StringBuilder"/>.
    /// </summary>
    /// <param name="value">The value to prepend.</param>
    /// <param name="receiver">The current <see cref="StringBuilder"/> instance.</param>
    /// <returns>Returns the current <see cref="StringBuilder"/> with the specified value prepended.</returns>
    public static StringBuilder Prepend(this StringBuilder receiver, string value) => receiver.Insert(0, value);

    /// <summary>
    /// Prepends the specified values to the current <see cref="StringBuilder"/>.
    /// </summary>
    /// <param name="values">The values to prepend.</param>
    /// <param name="receiver">The current <see cref="StringBuilder"/> instance.</param>
    /// <returns>Returns the current <see cref="StringBuilder"/> with the specified values prepended.</returns>
    public static StringBuilder Prepend(this StringBuilder receiver, params object[] values) => receiver.PrependJoin(string.Empty, values);

    /// <summary>
    /// Concatenates the string representations of the elements in the provided array of objects, using the specified separator between each member, then prepends the result to the current instance of the string builder.
    /// </summary>
    /// <param name="separator">The string to use as a separator. <paramref name="separator" /> is included in the joined strings only if <paramref name="values" /> has more than one element.</param>
    /// <param name="values">An array that contains the strings to concatenate and append to the current instance of the string builder.</param>
    /// <param name="receiver">The current <see cref="StringBuilder"/> instance.</param>
    /// <returns>Returns the current <see cref="StringBuilder"/> with the specified values joined and prepended.</returns>
    public static StringBuilder PrependJoin(this StringBuilder receiver, string separator, params object?[] values) => receiver.Prepend(string.Join(separator, values));

    /// <summary>
    /// Trims the specified <see cref="char"/> value from the start and end of the current <see cref="StringBuilder"/>.
    /// </summary>
    /// <param name="value">The <see cref="char"/> value to trim.</param>
    /// <param name="receiver">The current <see cref="StringBuilder"/> instance.</param>
    /// <returns>Returns the current <see cref="StringBuilder"/> with the specified <see cref="char"/> value trimmed from the start and end.</returns>
    public static StringBuilder Trim(this StringBuilder receiver, char value) => receiver.TrimStart(value).TrimEnd(value);

    /// <summary>
    /// Trims the specified <see cref="char"/> value from the end of the current <see cref="StringBuilder"/>.
    /// </summary>
    /// <param name="value">The <see cref="char"/> value to trim.</param>
    /// <param name="receiver">The current <see cref="StringBuilder"/> instance.</param>
    /// <returns>Returns the current <see cref="StringBuilder"/> with the specified <see cref="char"/> value trimmed from the end.</returns>
    public static StringBuilder TrimEnd(this StringBuilder receiver, char value)
    {
        int length = receiver.Length;

        while (length > 0 && receiver[length - 1] == value) length--;

        receiver.Length = length;
        return receiver;
    }

    /// <summary>
    /// Trims the specified <see cref="char"/> value from the start of the current <see cref="StringBuilder"/>.
    /// </summary>
    /// <param name="value">The <see cref="char"/> value to trim.</param>
    /// <param name="receiver">The current <see cref="StringBuilder"/> instance.</param>
    /// <returns>Returns the current <see cref="StringBuilder"/> with the specified <see cref="char"/> value trimmed from the start.</returns>
    public static StringBuilder TrimStart(this StringBuilder receiver, char value)
    {
        // Why: count the leading characters first and remove them once; removing one at a time shifts the whole buffer every time.
        int count = 0;

        while (count < receiver.Length && receiver[count] == value) count++;

        return receiver.Remove(0, count);
    }

    /// <summary>
    /// Trims the specified <see cref="string"/> value from the start and end of the current <see cref="StringBuilder"/>.
    /// </summary>
    /// <param name="value">The <see cref="string"/> value to trim.</param>
    /// <param name="receiver">The current <see cref="StringBuilder"/> instance.</param>
    /// <returns>Returns the current <see cref="StringBuilder"/> with the specified <see cref="string"/> value trimmed from the start and end.</returns>
    public static StringBuilder Trim(this StringBuilder receiver, string value) => receiver.TrimStart(value).TrimEnd(value);

    /// <summary>
    /// Trims the specified <see cref="string"/> value from the end of the current <see cref="StringBuilder"/>.
    /// </summary>
    /// <param name="value">The <see cref="string"/> value to trim.</param>
    /// <param name="receiver">The current <see cref="StringBuilder"/> instance.</param>
    /// <returns>Returns the current <see cref="StringBuilder"/> with the specified <see cref="string"/> value trimmed from the end.</returns>
    public static StringBuilder TrimEnd(this StringBuilder receiver, string value)
    {
        if (string.IsNullOrEmpty(value)) return receiver;

        // Why: compare characters in place; extracting a substring to compare allocated on every iteration.
        int length = receiver.Length;

        while (length >= value.Length && receiver.Matches(length - value.Length, value)) length -= value.Length;

        receiver.Length = length;
        return receiver;
    }

    /// <summary>
    /// Trims the specified <see cref="string"/> value from the start of the current <see cref="StringBuilder"/>.
    /// </summary>
    /// <param name="value">The <see cref="string"/> value to trim.</param>
    /// <param name="receiver">The current <see cref="StringBuilder"/> instance.</param>
    /// <returns>Returns the current <see cref="StringBuilder"/> with the specified <see cref="string"/> value trimmed from the start.</returns>
    public static StringBuilder TrimStart(this StringBuilder receiver, string value)
    {
        if (string.IsNullOrEmpty(value)) return receiver;

        // Why: compare characters in place and remove once; extracting a substring to compare allocated on every iteration.
        int count = 0;

        while (receiver.Length - count >= value.Length && receiver.Matches(count, value)) count += value.Length;

        return receiver.Remove(0, count);
    }

    /// <summary>
    /// Wraps the current <see cref="StringBuilder"/> between the specified start and end <see cref="char"/> values.
    /// </summary>
    /// <param name="start">The <see cref="char"/> value to prepend.</param>
    /// <param name="end">The <see cref="char"/> value to append.</param>
    /// <param name="receiver">The current <see cref="StringBuilder"/> instance.</param>
    /// <returns>Returns the current <see cref="StringBuilder"/> wrapped between the specified start and end <see cref="char"/> values.</returns>
    public static StringBuilder Wrap(this StringBuilder receiver, char start, char end) => receiver.Prepend(start).Append(end);

    /// <summary>
    /// Wraps the current <see cref="StringBuilder"/> between the specified start and end <see cref="string"/> values.
    /// </summary>
    /// <param name="start">The <see cref="string"/> value to prepend.</param>
    /// <param name="end">The <see cref="string"/> value to append.</param>
    /// <param name="receiver">The current <see cref="StringBuilder"/> instance.</param>
    /// <returns>Returns the current <see cref="StringBuilder"/> wrapped between the specified start and end <see cref="string"/> values.</returns>
    public static StringBuilder Wrap(this StringBuilder receiver, string start, string end) => receiver.Prepend(start).Append(end);

    /// <summary>
    /// Determines whether the specified value occurs in the current <see cref="StringBuilder"/> at the specified index, using ordinal comparison.
    /// </summary>
    /// <param name="receiver">The current <see cref="StringBuilder"/> instance.</param>
    /// <param name="index">The index at which to compare.</param>
    /// <param name="value">The value to compare, which must fit within the builder from <paramref name="index"/>.</param>
    /// <returns>Returns <see langword="true"/> if <paramref name="value"/> occurs at <paramref name="index"/>; otherwise, <see langword="false"/>.</returns>
    private static bool Matches(this StringBuilder receiver, int index, string value)
    {
        for (int offset = 0; offset < value.Length; offset++)
            if (receiver[index + offset] != value[offset]) return false;

        return true;
    }
}
