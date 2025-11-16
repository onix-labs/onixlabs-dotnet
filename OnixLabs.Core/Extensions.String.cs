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
using OnixLabs.Core.Text;

namespace OnixLabs.Core;

/// <summary>
/// Provides extension methods for <see cref="string"/> instances.
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
    /// Provides extension methods for <see cref="string"/> instances.
    /// </summary>
    /// <param name="receiver">The current <see cref="string"/> instance.</param>
    extension(string receiver)
    {
        /// <summary>
        /// Obtains the zero-based index of the nth occurrence of the specified character in this instance.
        /// If the specified occurrence does not exist, returns -1.
        /// </summary>
        /// <param name="seekValue">The character to seek.</param>
        /// <param name="count">The number of occurrences to skip before returning an index.</param>
        /// <returns>
        /// Returns the zero-based index position of the nth occurrence of <paramref name="seekValue"/>, if found; otherwise, -1.
        /// </returns>
        public int NthIndexOf(char seekValue, int count)
        {
            if (string.IsNullOrEmpty(receiver) || count <= 0) return -1;

            int occurrences = 0;

            for (int index = 0; index < receiver.Length; index++)
            {
                if (receiver[index] != seekValue)
                    continue;

                occurrences++;

                if (occurrences != count)
                    continue;

                return index;
            }

            return NotFound;
        }

        /// <summary>
        /// Obtains the zero-based index of the nth occurrence of the specified character in this instance.
        /// If the specified occurrence does not exist, returns -1.
        /// </summary>
        /// <param name="seekValue">The substring to seek.</param>
        /// <param name="count">The number of occurrences to skip before returning an index.</param>
        /// <param name="comparison">The comparison that will be used to compare the current value and the seek value.</param>
        /// <returns>
        /// Returns the zero-based index position of the nth occurrence of <paramref name="seekValue"/>, if found; otherwise, -1.
        /// </returns>
        public int NthIndexOf(string seekValue, int count, StringComparison comparison = DefaultComparison)
        {
            if (string.IsNullOrEmpty(receiver) || string.IsNullOrEmpty(seekValue) || count <= 0) return -1;

            int occurrences = 0;
            int startIndex = 0;

            while (true)
            {
                int index = receiver.IndexOf(seekValue, startIndex, comparison);

                if (index is -1)
                    return -1;

                occurrences++;

                if (occurrences == count)
                    return index;

                startIndex = index + seekValue.Length;

                if (startIndex >= receiver.Length)
                    return NotFound;
            }
        }

        /// <summary>
        /// Repeats the current <see cref="String"/> by the specified number of repetitions.
        /// </summary>
        /// <param name="count">The number of repetitions of the current <see cref="String"/> instance.</param>
        /// <returns>Returns a new <see cref="String"/> instance representing the repetition of the current <see cref="String"/> instance.</returns>
        public string Repeat(int count) => count > 0 ? string.Join(string.Empty, Enumerable.Repeat(receiver, count)) : string.Empty;

        /// <summary>
        /// Obtains a sub-string before the specified index within the current <see cref="String"/> instance.
        /// </summary>
        /// <param name="index">The index in the current <see cref="String"/> from which to obtain a sub-string.</param>
        /// <param name="defaultValue">
        /// The <see cref="String"/> value to return in the event that the index is less than zero.
        /// If the default value is <see langword="null"/> then the current <see cref="String"/> instance is returned.
        /// </param>
        /// <returns>
        /// Returns a sub-string before the specified index. If the index is less than zero, then the default value will be returned.
        /// If the default value is <see langword="null"/>, then the current <see cref="String"/> instance will be returned.
        /// </returns>
        // ReSharper disable once HeapView.ObjectAllocation
        private string SubstringBeforeIndex(int index, string? defaultValue = null) =>
            index <= NotFound ? defaultValue ?? receiver : receiver[..index];

        /// <summary>
        /// Obtains a sub-string after the specified index within the current <see cref="String"/> instance.
        /// </summary>
        /// <param name="index">The index in the current <see cref="String"/> from which to obtain a sub-string.</param>
        /// <param name="offset">The offset from the index which marks the beginning of the sub-string.</param>
        /// <param name="defaultValue">
        /// The <see cref="String"/> value to return in the event that the index is less than zero.
        /// If the default value is <see langword="null"/> then the current <see cref="String"/> instance is returned.
        /// </param>
        /// <returns>
        /// Returns a sub-string after the specified index. If the index is less than zero, then the default value will be returned.
        /// If the default value is <see langword="null"/>, then the current <see cref="String"/> instance will be returned.
        /// </returns>
        // ReSharper disable once HeapView.ObjectAllocation
        private string SubstringAfterIndex(int index, int offset, string? defaultValue = null) =>
            index <= NotFound ? defaultValue ?? receiver : receiver[(index + offset)..];

        /// <summary>
        /// Obtains a sub-string before the first occurrence of the specified delimiter within the current <see cref="String"/> instance.
        /// </summary>
        /// <param name="delimiter">The delimiter to find within the current <see cref="String"/> instance.</param>
        /// <param name="defaultValue">
        /// The <see cref="String"/> value to return in the event that the delimiter is not found in the current <see cref="String"/> instance.
        /// If the default value is <see langword="null"/> then the current <see cref="String"/> instance is returned.
        /// </param>
        /// <returns>
        /// Returns a sub-string before the first occurrence of the specified delimiter within the current <see cref="String"/> instance.
        /// If the delimiter is not found, then the default value will be returned.
        /// If the default value is <see langword="null"/>, then the current <see cref="String"/> instance is returned.
        /// </returns>
        public string SubstringBeforeFirst(char delimiter, string? defaultValue = null) =>
            receiver.SubstringBeforeIndex(receiver.IndexOf(delimiter), defaultValue);

        ///  <summary>
        ///  Obtains a sub-string before the first occurrence of the specified delimiter within the current <see cref="String"/> instance.
        ///  </summary>
        ///  <param name="delimiter">The delimiter to find within the current <see cref="String"/> instance.</param>
        ///  <param name="defaultValue">
        ///  The <see cref="String"/> value to return in the event that the delimiter is not found in the current <see cref="String"/> instance.
        ///  If the default value is <see langword="null"/> then the current <see cref="String"/> instance is returned.
        ///  </param>
        ///  <param name="comparison">
        ///  The culture, case and sort rules that will be applied when searching for the specified delimiter.
        ///  The default value is <see cref="StringComparison.Ordinal"/>.
        ///  </param>
        ///  <returns>
        /// Returns a sub-string before the first occurrence of the specified delimiter within the current <see cref="String"/> instance.
        ///  If the delimiter is not found, then the default value will be returned.
        ///  If the default value is <see langword="null"/>, then the current <see cref="String"/> instance is returned.
        ///  </returns>
        public string SubstringBeforeFirst(string delimiter, string? defaultValue = null, StringComparison comparison = DefaultComparison) =>
            receiver.SubstringBeforeIndex(receiver.IndexOf(delimiter, comparison), defaultValue);

        ///  <summary>
        ///  Obtains a sub-string before the last occurrence of the specified delimiter within the current <see cref="String"/> instance.
        ///  </summary>
        ///  <param name="delimiter">The delimiter to find within the current <see cref="String"/> instance.</param>
        ///  <param name="defaultValue">
        ///  The <see cref="String"/> value to return in the event that the delimiter is not found in the current <see cref="String"/> instance.
        ///  If the default value is <see langword="null"/> then the current <see cref="String"/> instance is returned.
        ///  </param>
        ///  <returns>
        /// Returns a sub-string before the last occurrence of the specified delimiter within the current <see cref="String"/> instance.
        ///  If the delimiter is not found, then the default value will be returned.
        ///  If the default value is <see langword="null"/>, then the current <see cref="String"/> instance is returned.
        ///  </returns>
        public string SubstringBeforeLast(char delimiter, string? defaultValue = null) =>
            receiver.SubstringBeforeIndex(receiver.LastIndexOf(delimiter), defaultValue);

        ///  <summary>
        ///  Obtains a sub-string before the last occurrence of the specified delimiter within the current <see cref="String"/> instance.
        ///  </summary>
        ///  <param name="delimiter">The delimiter to find within the current <see cref="String"/> instance.</param>
        ///  <param name="defaultValue">
        ///  The <see cref="String"/> value to return in the event that the delimiter is not found in the current <see cref="String"/> instance.
        ///  If the default value is <see langword="null"/> then the current <see cref="String"/> instance is returned.
        ///  </param>
        ///  <param name="comparison">
        ///  The culture, case and sort rules that will be applied when searching for the specified delimiter.
        ///  The default value is <see cref="StringComparison.Ordinal"/>.
        ///  </param>
        ///  <returns>
        /// Returns a sub-string before the last occurrence of the specified delimiter within the current <see cref="String"/> instance.
        ///  If the delimiter is not found, then the default value will be returned.
        ///  If the default value is <see langword="null"/>, then the current <see cref="String"/> instance is returned.
        ///  </returns>
        public string SubstringBeforeLast(string delimiter, string? defaultValue = null, StringComparison comparison = DefaultComparison) =>
            receiver.SubstringBeforeIndex(receiver.LastIndexOf(delimiter, comparison), defaultValue);

        /// <summary>
        /// Obtains a sub-string after the first occurrence of the specified delimiter within the current <see cref="String"/> instance.
        /// </summary>
        /// <param name="delimiter">The delimiter to find within the current <see cref="String"/> instance.</param>
        /// <param name="defaultValue">
        /// The <see cref="String"/> value to return in the event that the delimiter is not found in the current <see cref="String"/> instance.
        /// If the default value is <see langword="null"/> then the current <see cref="String"/> instance is returned.
        /// </param>
        /// <returns>
        /// Returns a sub-string after the first occurrence of the specified delimiter within the current <see cref="String"/> instance.
        /// If the delimiter is not found, then the default value will be returned.
        /// If the default value is <see langword="null"/>, then the current <see cref="String"/> instance is returned.
        /// </returns>
        public string SubstringAfterFirst(char delimiter, string? defaultValue = null) =>
            receiver.SubstringAfterIndex(receiver.IndexOf(delimiter), 1, defaultValue);

        ///  <summary>
        ///  Obtains a sub-string after the first occurrence of the specified delimiter within the current <see cref="String"/> instance.
        ///  </summary>
        ///  <param name="delimiter">The delimiter to find within the current <see cref="String"/> instance.</param>
        ///  <param name="defaultValue">
        ///  The <see cref="String"/> value to return in the event that the delimiter is not found in the current <see cref="String"/> instance.
        ///  If the default value is <see langword="null"/> then the current <see cref="String"/> instance is returned.
        ///  </param>
        ///  <param name="comparison">
        ///  The culture, case and sort rules that will be applied when searching for the specified delimiter.
        ///  The default value is <see cref="StringComparison.Ordinal"/>.
        ///  </param>
        ///  <returns>
        /// Returns a sub-string after the first occurrence of the specified delimiter within the current <see cref="String"/> instance.
        ///  If the delimiter is not found, then the default value will be returned.
        ///  If the default value is <see langword="null"/>, then the current <see cref="String"/> instance is returned.
        ///  </returns>
        public string SubstringAfterFirst(string delimiter, string? defaultValue = null, StringComparison comparison = DefaultComparison) =>
            receiver.SubstringAfterIndex(receiver.IndexOf(delimiter, comparison), delimiter.Length, defaultValue);

        ///  <summary>
        ///  Obtains a sub-string after the last occurrence of the specified delimiter within the current <see cref="String"/> instance.
        ///  </summary>
        ///  <param name="delimiter">The delimiter to find within the current <see cref="String"/> instance.</param>
        ///  <param name="defaultValue">
        ///  The <see cref="String"/> value to return in the event that the delimiter is not found in the current <see cref="String"/> instance.
        ///  If the default value is <see langword="null"/> then the current <see cref="String"/> instance is returned.
        ///  </param>
        ///  <returns>
        /// Returns a sub-string after the last occurrence of the specified delimiter within the current <see cref="String"/> instance.
        ///  If the delimiter is not found, then the default value will be returned.
        ///  If the default value is <see langword="null"/>, then the current <see cref="String"/> instance is returned.
        ///  </returns>
        public string SubstringAfterLast(char delimiter, string? defaultValue = null) =>
            receiver.SubstringAfterIndex(receiver.LastIndexOf(delimiter), 1, defaultValue);

        ///  <summary>
        ///  Obtains a sub-string after the last occurrence of the specified delimiter within the current <see cref="String"/> instance.
        ///  </summary>
        ///  <param name="delimiter">The delimiter to find within the current <see cref="String"/> instance.</param>
        ///  <param name="defaultValue">
        ///  The <see cref="String"/> value to return in the event that the delimiter is not found in the current <see cref="String"/> instance.
        ///  If the default value is <see langword="null"/> then the current <see cref="String"/> instance is returned.
        ///  </param>
        ///  <param name="comparison">
        ///  The culture, case and sort rules that will be applied when searching for the specified delimiter.
        ///  The default value is <see cref="StringComparison.Ordinal"/>.
        ///  </param>
        ///  <returns>
        /// Returns a sub-string after the last occurrence of the specified delimiter within the current <see cref="String"/> instance.
        ///  If the delimiter is not found, then the default value will be returned.
        ///  If the default value is <see langword="null"/>, then the current <see cref="String"/> instance is returned.
        ///  </returns>
        public string SubstringAfterLast(string delimiter, string? defaultValue = null, StringComparison comparison = DefaultComparison) =>
            receiver.SubstringAfterIndex(receiver.LastIndexOf(delimiter, comparison), 1, defaultValue);

        /// <summary>
        /// Obtains a sub-string before the nth occurrence of the specified character within the current <see cref="String"/> instance.
        /// If the nth occurrence is not found, returns the <paramref name="defaultValue"/> or the entire string if default is null.
        /// </summary>
        /// <param name="seekValue">The character to find the nth occurrence of.</param>
        /// <param name="count">The nth occurrence to find.</param>
        /// <param name="defaultValue">
        /// The <see cref="String"/> value to return if the nth occurrence is not found.
        /// If the default value is <see langword="null"/>, the current <see cref="String"/> instance is returned.
        /// </param>
        /// <returns>
        /// A sub-string before the nth occurrence of <paramref name="seekValue"/> if found; otherwise,
        /// <paramref name="defaultValue"/> or the entire string if default is null.
        /// </returns>
        public string SubstringBeforeNth(char seekValue, int count, string? defaultValue = null)
        {
            int index = receiver.NthIndexOf(seekValue, count);
            return receiver.SubstringBeforeIndex(index, defaultValue);
        }

        /// <summary>
        /// Obtains a sub-string before the nth occurrence of the specified substring within the current <see cref="String"/> instance.
        /// If the nth occurrence is not found, returns the <paramref name="defaultValue"/> or the entire string if default is null.
        /// </summary>
        /// <param name="seekValue">The substring to find the nth occurrence of.</param>
        /// <param name="count">The nth occurrence to find.</param>
        /// <param name="defaultValue">
        /// The <see cref="String"/> value to return if the nth occurrence is not found.
        /// If the default value is <see langword="null"/>, the current <see cref="String"/> instance is returned.
        /// </param>
        /// <param name="comparison">The comparison that will be used to compare the current value and the seek value.</param>
        /// <returns>
        /// A sub-string before the nth occurrence of <paramref name="seekValue"/> if found; otherwise,
        /// <paramref name="defaultValue"/> or the entire string if default is null.
        /// </returns>
        public string SubstringBeforeNth(string seekValue, int count, string? defaultValue = null, StringComparison comparison = DefaultComparison)
        {
            int index = receiver.NthIndexOf(seekValue, count, comparison);
            return receiver.SubstringBeforeIndex(index, defaultValue);
        }

        /// <summary>
        /// Obtains a sub-string after the nth occurrence of the specified character within the current <see cref="String"/> instance.
        /// If the nth occurrence is not found, returns the <paramref name="defaultValue"/> or the entire string if default is null.
        /// </summary>
        /// <param name="seekValue">The character to find the nth occurrence of.</param>
        /// <param name="count">The nth occurrence to find.</param>
        /// <param name="defaultValue">
        /// The <see cref="String"/> value to return if the nth occurrence is not found.
        /// If the default value is <see langword="null"/>, the current <see cref="String"/> instance is returned.
        /// </param>
        /// <returns>
        /// A sub-string after the nth occurrence of <paramref name="seekValue"/> if found; otherwise,
        /// <paramref name="defaultValue"/> or the entire string if default is null.
        /// </returns>
        public string SubstringAfterNth(char seekValue, int count, string? defaultValue = null)
        {
            int index = receiver.NthIndexOf(seekValue, count);
            // Move 1 character after the nth occurrence index.
            return receiver.SubstringAfterIndex(index, 1, defaultValue);
        }

        /// <summary>
        /// Obtains a sub-string after the nth occurrence of the specified substring within the current <see cref="String"/> instance.
        /// If the nth occurrence is not found, returns the <paramref name="defaultValue"/> or the entire string if default is null.
        /// </summary>
        /// <param name="seekValue">The substring to find the nth occurrence of.</param>
        /// <param name="count">The nth occurrence to find.</param>
        /// <param name="defaultValue">
        /// The <see cref="String"/> value to return if the nth occurrence is not found.
        /// If the default value is <see langword="null"/>, the current <see cref="String"/> instance is returned.
        /// </param>
        /// <param name="comparison">The comparison that will be used to compare the current value and the seek value.</param>
        /// <returns>
        /// A sub-string after the nth occurrence of <paramref name="seekValue"/> if found; otherwise,
        /// <paramref name="defaultValue"/> or the entire string if default is null.
        /// </returns>
        public string SubstringAfterNth(string seekValue, int count, string? defaultValue = null, StringComparison comparison = DefaultComparison)
        {
            int index = receiver.NthIndexOf(seekValue, count, comparison);
            // Move by the length of the found substring after the nth occurrence index.
            int offset = (index != NotFound && !string.IsNullOrEmpty(seekValue)) ? seekValue.Length : 0;
            return receiver.SubstringAfterIndex(index, offset, defaultValue);
        }

        /// <summary>
        /// Converts the current <see cref="String"/> instance into a new <see cref="byte"/> array.
        /// </summary>
        /// <param name="encoding">The encoding that will be used to convert the current <see cref="String"/> into a <see cref="byte"/> array.</param>
        /// <returns>Returns a new <see cref="byte"/> array representation of the current <see cref="String"/> instance.</returns>
        public byte[] ToByteArray(Encoding? encoding = null) => encoding.GetOrDefault().GetBytes(receiver);

        /// <summary>
        /// Converts the current <see cref="String"/> instance into a new <see cref="DateTime"/> instance.
        /// </summary>
        /// <param name="provider">An object that provides culture-specific formatting information.</param>
        /// <param name="styles">A bit-wise combination of enumeration values that indicate the style elements that can be present.</param>
        /// <returns>Returns a new <see cref="DateTime"/> instance parsed from the current <see cref="String"/> instance.</returns>
        public DateTime ToDateTime(IFormatProvider? provider = null, DateTimeStyles styles = DefaultStyles) =>
            DateTime.Parse(receiver, provider, styles);

        /// <summary>
        /// Converts the current <see cref="String"/> instance into a new <see cref="DateOnly"/> instance.
        /// </summary>
        /// <param name="provider">An object that provides culture-specific formatting information.</param>
        /// <param name="styles">A bit-wise combination of enumeration values that indicate the style elements that can be present.</param>
        /// <returns>Returns a new <see cref="DateOnly"/> instance parsed from the current <see cref="String"/> instance.</returns>
        public DateOnly ToDateOnly(IFormatProvider? provider = null, DateTimeStyles styles = DefaultStyles) =>
            DateOnly.Parse(receiver, provider, styles);

        /// <summary>
        /// Converts the current <see cref="String"/> instance into a new <see cref="TimeOnly"/> instance.
        /// </summary>
        /// <param name="provider">An object that provides culture-specific formatting information.</param>
        /// <param name="styles">A bit-wise combination of enumeration values that indicate the style elements that can be present.</param>
        /// <returns>Returns a new <see cref="TimeOnly"/> instance parsed from the current <see cref="String"/> instance.</returns>
        public TimeOnly ToTimeOnly(IFormatProvider? provider = null, DateTimeStyles styles = DefaultStyles) =>
            TimeOnly.Parse(receiver, provider, styles);

        /// <summary>
        /// Returns a <see cref="string"/> with all escape characters formatted as escape character literals.
        /// </summary>
        /// <returns>Returns a <see cref="string"/> with all escape characters formatted as escape character literals.</returns>
        public string ToEscapedString()
        {
            // ReSharper disable once HeapView.ObjectAllocation.Evident
            StringBuilder result = new();

            foreach (char c in receiver)
            {
                switch (c)
                {
                    case '\n':
                        result.AppendEscaped('n');
                        break;
                    case '\r':
                        result.AppendEscaped('r');
                        break;
                    case '\t':
                        result.AppendEscaped('t');
                        break;
                    case '\"':
                        result.AppendEscaped('"');
                        break;
                    case '\'':
                        result.AppendEscaped('\'');
                        break;
                    case '\\':
                        result.AppendEscaped('\\');
                        break;
                    default:
                        if (char.IsControl(c)) result.Append($"\\u{(int)c:X4}");
                        else result.Append(c);
                        break;
                }
            }

            return result.ToString();
        }

        /// <summary>
        /// Tries to copy the current <see cref="String"/> instance into the destination <see cref="Span{T}"/>.
        /// </summary>
        /// <param name="destination">The <see cref="Span{T}"/> into which the current <see cref="String"/> contents will be copied.</param>
        /// <param name="charsWritten">The number of characters written to the destination <see cref="Span{T}"/>.</param>
        /// <returns>Returns <see langword="true"/> if the current <see cref="String"/> instance was copied into the destination <see cref="Span{T}"/>; otherwise, <see langword="false"/> if the destination was too short.</returns>
        public bool TryCopyTo(Span<char> destination, out int charsWritten)
        {
            bool result = receiver.TryCopyTo(destination);
            charsWritten = result ? receiver.Length : 0;
            return result;
        }

        /// <summary>
        /// Wraps the current <see cref="String"/> instance between the specified before and after <see cref="Char"/> instances.
        /// </summary>
        /// <param name="before">The <see cref="Char"/> that should precede the current <see cref="String"/> instance.</param>
        /// <param name="after">The <see cref="Char"/> that should succeed the current <see cref="String"/> instance.</param>
        /// <returns>Returns a new <see cref="String"/> instance representing the current <see cref="String"/> instance, wrapped between the specified before and after <see cref="Char"/> instances.</returns>
        public string Wrap(char before, char after) => string.Concat(before.ToString(), receiver, after.ToString());

        /// <summary>
        /// Wraps the current <see cref="String"/> instance between the specified before and after <see cref="String"/> instances.
        /// </summary>
        /// <param name="before">The <see cref="String"/> that should precede the current <see cref="String"/> instance.</param>
        /// <param name="after">The <see cref="String"/> that should succeed the current <see cref="String"/> instance.</param>
        /// <returns>Returns a new <see cref="String"/> instance representing the current <see cref="String"/> instance, wrapped between the specified before and after <see cref="String"/> instances.</returns>
        public string Wrap(string before, string after) => string.Concat(before, receiver, after);
    }
}
