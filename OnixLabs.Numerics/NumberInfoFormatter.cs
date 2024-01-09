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
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Text;
using OnixLabs.Core.Text;

namespace OnixLabs.Numerics;

/// <summary>
/// Represents a formatter for formatting <see cref="NumberInfo"/> values.
/// </summary>
/// <param name="value">The <see cref="NumberInfo"/> value to format.</param>
/// <param name="formatProvider">The format provider, which should be a <see cref="CultureInfo"/> containing the desired format details.</param>
/// <param name="allowedFormats">Specifies which formats are allowed for formatting.</param>
internal sealed partial class NumberInfoFormatter(NumberInfo value, IFormatProvider formatProvider, IEnumerable<char> allowedFormats)
{
    private const char DefaultFormat = 'G';
    private const char LeadingParenthesis = '(';
    private const char TrailingParenthesis = ')';
    private const char Whitespace = ' ';

    private readonly StringBuilder builder = new();
    private readonly NumberFormatInfo numberFormat = NumberFormatInfo.GetInstance(formatProvider);
    private readonly IEnumerable<char> allowedFormats = allowedFormats.Select(char.ToUpperInvariant);

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
        builder.Append(BigInteger.Abs(value.Integer));

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
        if (value.Scale > 0) builder.Append(separator, value.Fraction.ToString().PadLeft(value.Scale, '0'));
    }
}
