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
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using OnixLabs.Core.Text;

namespace OnixLabs.Core.Numerics;

/// <summary>
/// Represents a formatter for formatting <see cref="BigDecimal"/> values.
/// </summary>
/// <param name="value">The <see cref="BigDecimal"/> value to format.</param>
/// <param name="info">The <see cref="NumberFormatInfo"/> information used for formatting.</param>
internal sealed partial class BigDecimalFormatter(BigDecimal value, NumberFormatInfo info)
{
    private const char DefaultFormat = 'G';
    private const char LeadingParenthesis = '(';
    private const char TrailingParenthesis = ')';
    private const char Whitespace = ' ';

    private readonly StringBuilder builder = new();

    /// <summary>
    /// Formats a <see cref="BigDecimal"/> value using the specified format.
    /// </summary>
    /// <param name="format">The format of the <see cref="BigDecimal"/> value.</param>
    /// <returns>Returns a <see cref="string"/> representation of the <see cref="BigDecimal"/> value.</returns>
    public string Format(ReadOnlySpan<char> format)
    {
        if (!TryScaleValue(format)) return format.ToString();

        char specifier = format.IsEmpty || format.IsWhiteSpace() ? DefaultFormat : format[0];

        switch (char.ToUpperInvariant(specifier))
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
    /// Attempts to scale the <see cref="BigDecimal"/> value either using a custom scale applied by the specified format, or using the default scale for the specified format.
    /// </summary>
    /// <param name="format">The format from which to obtain a custom scale.</param>
    /// <returns>Returns true if the scale was applied correctly; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    private bool TryScaleValue(ReadOnlySpan<char> format)
    {
        int scale;

        if (format.Length > 1)
        {
            if (!int.TryParse(format[1..], out scale)) return false;
        }
        else
        {
            scale = char.ToUpperInvariant(format[0]) switch
            {
                'C' => info.CurrencyDecimalDigits,
                'F' => info.NumberDecimalDigits,
                'N' => info.NumberDecimalDigits,
                'P' => info.PercentDecimalDigits,
                _ => value.Scale
            };
        }

        value = value.SetScale(scale);
        return true;
    }

    /// <summary>
    /// Formats the integer component of the <see cref="BigDecimal"/> value.
    /// </summary>
    /// <param name="grouping">The size of each number group.</param>
    /// <param name="separator">The separator that separates each number group.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    private void FormatIntegerComponent(int grouping = default, string separator = "")
    {
        builder.Append(BigInteger.Abs(value.IntegralValue));

        if (grouping == 0) return;

        int position = builder.Length - 1;
        int count = 0;

        while (position > 0)
        {
            if (char.IsDigit(builder[position])) count++;

            if (count == grouping)
            {
                builder.Insert(position, separator);
                count = 0;
            }

            position--;
        }
    }

    /// <summary>
    /// Formats the fraction component of the <see cref="BigDecimal"/> value.
    /// </summary>
    /// <param name="separator">The separator that separates the fraction component from the integer component.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    private void FormatFractionComponent(string separator)
    {
        if (value.Scale > 0) builder.Append(separator, value.FractionalValue.ToString().PadLeft(value.Scale, '0'));
    }
}
