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
using System.Buffers;
using System.Collections.Generic;
using System.Globalization;
using System.Numerics;
using System.Text;

namespace OnixLabs.Numerics;

/// <summary>
/// Represents a formatter for formatting <see cref="NumberInfo"/> values.
/// </summary>
internal sealed partial class NumberInfoFormatter
{
    internal const char DefaultFormat = 'G';
    private const char LeadingParenthesis = '(';
    private const char TrailingParenthesis = ')';
    private const char Whitespace = ' ';

    // ReSharper disable once HeapView.ObjectAllocation.Evident
    /// <summary>
    /// The format specifiers supported when formatting a wide integer type: round-trip and hexadecimal.
    /// </summary>
    internal static readonly SearchValues<char> RoundTripAndHexadecimalFormats = SearchValues.Create("RX");

    private readonly StringBuilder builder = new();
    private readonly NumberFormatInfo numberFormat;
    private readonly SearchValues<char> allowedFormats;
    private NumberInfo value;
    private string? absoluteDigits;

    /// <summary>
    /// Initializes a new instance of the <see cref="NumberInfoFormatter"/> class.
    /// </summary>
    /// <param name="value">The <see cref="NumberInfo"/> value to format.</param>
    /// <param name="formatProvider">The format provider, which should be a <see cref="CultureInfo"/> containing the desired format details.</param>
    /// <param name="allowedFormats">Specifies which formats are allowed for formatting, as uppercase format specifiers.</param>
    public NumberInfoFormatter(NumberInfo value, IFormatProvider formatProvider, SearchValues<char> allowedFormats)
    {
        this.value = value;
        numberFormat = NumberFormatInfo.GetInstance(formatProvider);
        this.allowedFormats = allowedFormats;
    }

    /// <summary>
    /// Formats the current <see cref="NumberInfo"/> value using the specified format.
    /// </summary>
    /// <param name="format">The desired format of the <see cref="NumberInfo"/> value.</param>
    /// <returns>Returns a <see cref="string"/> representation of the current <see cref="NumberInfo"/> value.</returns>
    public string Format(ReadOnlySpan<char> format)
    {
        char specifier = format.IsEmpty || format.IsWhiteSpace() ? DefaultFormat : format[0];
        char specifierUpperInvariant = char.ToUpperInvariant(specifier);

        if (!allowedFormats.Contains(specifierUpperInvariant)) return format.ToString();
        if (!TryApplyFormatScale(specifierUpperInvariant, format)) return format.ToString();

        switch (specifierUpperInvariant)
        {
            case 'C':
                FormatCurrency();
                break;
            case 'D':
                FormatDecimal();
                break;
            case 'E':
                FormatExponential(specifier);
                break;
            case 'F':
                FormatFixed();
                break;
            case 'G':
                FormatGeneral();
                break;
            case 'N':
                FormatNumber();
                break;
            case 'P':
                FormatPercent();
                break;
            case 'R':
                FormatRoundTrip();
                break;
            case 'X':
                if (!FormatHexadecimal(specifier, format)) return format.ToString();
                break;
            default:
                return format.ToString();
        }

        return builder.ToString();
    }

    /// <summary>
    /// Formats the integral component of the current <see cref="NumberInfo"/> value.
    /// </summary>
    /// <param name="grouping">The sizes of each number group.</param>
    /// <param name="separator">The separator that separates each number group.</param>
    private void FormatInteger(IReadOnlyList<int> grouping, string separator = "")
    {
        string digits = AbsoluteDigits;
        int integerLength = digits.Length - value.Scale;

        if (integerLength > 0) builder.Append(digits, 0, integerLength);
        else builder.Append('0');

        if (grouping.Count == 0) return;

        int position = builder.Length - 1;
        int count = 0;
        int index = 0;

        while (position > 0)
        {
            if (char.IsDigit(builder[position])) count++;

            if (count == grouping[index])
            {
                builder.Insert(position, separator);
                count = 0;

                if (index < grouping.Count - 1) index++;
            }

            position--;
        }
    }

    /// <summary>
    /// Formats the fractional component of the current <see cref="NumberInfo"/> value.
    /// </summary>
    /// <param name="separator">The separator that separates the integral and fractional components.</param>
    private void FormatFraction(string separator)
    {
        if (value.Scale <= 0) return;

        string digits = AbsoluteDigits;
        builder.Append(separator);

        if (digits.Length >= value.Scale) builder.Append(digits, digits.Length - value.Scale, value.Scale);
        else builder.Append('0', value.Scale - digits.Length).Append(digits);
    }

    /// <summary>
    /// Gets the decimal digits of the absolute unscaled value: the integer part followed by exactly <see cref="NumberInfo.Scale"/>
    /// fraction digits, once leading zeros are restored.
    /// </summary>
    /// <value>The decimal digits of the absolute unscaled value, computed once per format after any format scaling.</value>
    /// <remarks>
    /// Why: the integer and fraction parts are the leading and trailing digits of the same string, so one conversion replaces a
    /// division, a remainder and two conversions. Every reassignment of the value happens while applying the format scale, before
    /// the first read.
    /// </remarks>
    private string AbsoluteDigits => absoluteDigits ??= BigInteger.Abs(value.UnscaledValue).ToString(CultureInfo.InvariantCulture);
}
