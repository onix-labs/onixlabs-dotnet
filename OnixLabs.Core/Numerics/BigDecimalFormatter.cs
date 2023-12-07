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
using System.Linq;
using System.Numerics;
using System.Text;
using OnixLabs.Core.Text;

namespace OnixLabs.Core.Numerics;

internal static class BigDecimalFormatter
{
    private const char Space = ' ';
    private const char WrapStart = '(';
    private const char WrapEnd = ')';
    private const char InvariantDecimalSeparator = '.';

    public static string Format(BigDecimal value, ReadOnlySpan<char> format, NumberFormatInfo info)
    {
        StringBuilder builder = new();

        if (!TryGetSpecifier(format, out char specifier)) return format.ToString();
        BigDecimal scaled;

        switch (specifier)
        {
            case 'C' or 'c':
                if (!TryGetScaledValue(format, value, info.CurrencyDecimalDigits, out scaled)) return format.ToString();
                FormatCurrency(builder, scaled, info);
                break;

            case 'D' or 'd':
                if (!TryGetScaledValue(format, value, value.Scale, out scaled)) return format.ToString();
                FormatDecimal(builder, scaled, info);
                break;

            case 'E' or 'e':
                if (!TryGetScaledValue(format, value, value.Scale, out scaled)) return format.ToString();
                FormatEngineering(builder, scaled, info, specifier);
                break;

            case 'F' or 'f':
                if (!TryGetScaledValue(format, value, info.NumberDecimalDigits, out scaled)) return format.ToString();
                FormatFixed(builder, scaled, info);
                break;

            case 'G' or 'g':
                if (!TryGetScaledValue(format, value, value.Scale, out scaled)) return format.ToString();
                FormatGeneral(builder, scaled, info);
                break;

            case 'N' or 'n':
                if (!TryGetScaledValue(format, value, info.NumberDecimalDigits, out scaled)) return format.ToString();
                FormatNumber(builder, scaled, info);
                break;

            case 'P' or 'p':
                if (!TryGetScaledValue(format, value, info.PercentDecimalDigits, out scaled)) return format.ToString();
                FormatPercentage(builder, scaled, info);
                break;

            default:
                return format.ToString();
        }

        return builder.ToString();
    }

    private static void FormatCurrency(StringBuilder builder, BigDecimal value, NumberFormatInfo info)
    {
        FormatIntegralValue(builder, value, info.CurrencyGroupSizes[0], info.CurrencyGroupSeparator);
        FormatFractionalValue(builder, value, info.NumberDecimalSeparator);
        FormatCurrencyNegativePattern(builder, value, info);
        FormatCurrencyPositivePattern(builder, value, info);
    }

    private static void FormatDecimal(StringBuilder builder, BigDecimal value, NumberFormatInfo info)
    {
        FormatIntegralValue(builder, value, info.NumberGroupSizes[0], info.NumberGroupSeparator);
        FormatFractionalValue(builder, value, info.NumberDecimalSeparator);
        FormatNumberNegativePattern(builder, value, info);
    }

    private static void FormatEngineering(StringBuilder builder, BigDecimal value, NumberFormatInfo info, char specifier)
    {
        builder.Append(BigInteger.Abs(value.UnscaledValue));

        int exponent = builder.Length - value.Scale - 1;
        builder.Trim('0').Insert(1, InvariantDecimalSeparator).TrimEnd(InvariantDecimalSeparator);

        if (exponent == 0) return;

        string sign = exponent > 0 ? info.PositiveSign : info.NegativeSign;
        builder.Append(specifier, sign, int.Abs(exponent));
    }

    private static void FormatFixed(StringBuilder builder, BigDecimal value, NumberFormatInfo info)
    {
        FormatIntegralValue(builder, value, default, string.Empty);
        FormatFractionalValue(builder, value, info.NumberDecimalSeparator);
        FormatNumberNegativePattern(builder, value, info);
    }

    private static void FormatGeneral(StringBuilder builder, BigDecimal value, NumberFormatInfo info)
    {
        FormatIntegralValue(builder, value, default, string.Empty);
        FormatFractionalValue(builder, value, info.NumberDecimalSeparator);
        FormatNumberNegativePattern(builder, value, info);
    }

    private static void FormatNumber(StringBuilder builder, BigDecimal value, NumberFormatInfo info)
    {
        FormatIntegralValue(builder, value, info.NumberGroupSizes[0], info.NumberGroupSeparator);
        FormatFractionalValue(builder, value, info.NumberDecimalSeparator);
        FormatNumberNegativePattern(builder, value, info);
    }

    private static void FormatPercentage(StringBuilder builder, BigDecimal value, NumberFormatInfo info)
    {
        FormatIntegralValue(builder, value, info.PercentGroupSizes[0], info.PercentGroupSeparator);
        FormatFractionalValue(builder, value, info.PercentDecimalSeparator);
        FormatPercentNegativePattern(builder, value, info);
        FormatPercentPositivePattern(builder, value, info);
    }

    private static void FormatIntegralValue(StringBuilder builder, BigDecimal value, int grouping, string separator)
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

    private static void FormatFractionalValue(StringBuilder builder, BigDecimal value, string separator)
    {
        if (value.Scale > 0) builder.Append(separator, value.FractionalValue.ToString().PadLeft(value.Scale, '0'));
    }

    private static void FormatCurrencyPositivePattern(StringBuilder builder, BigDecimal value, NumberFormatInfo info)
    {
        if (BigDecimal.IsNegative(value)) return;

        switch (info.CurrencyPositivePattern)
        {
            case 0:
                builder.Prepend(info.CurrencySymbol);
                break;

            case 1:
                builder.Append(info.CurrencySymbol);
                break;

            case 2:
                builder.Prepend(info.CurrencySymbol, Space);
                break;

            case 3:
                builder.Append(Space, info.CurrencySymbol);
                break;
        }
    }

    private static void FormatCurrencyNegativePattern(StringBuilder builder, BigDecimal value, NumberFormatInfo info)
    {
        if (BigDecimal.IsPositive(value)) return;

        switch (info.CurrencyNegativePattern)
        {
            case 0:
                builder.Prepend(info.CurrencySymbol).Wrap(WrapStart, WrapEnd);
                break;

            case 1:
                builder.Prepend(info.NegativeSign, info.CurrencySymbol);
                break;

            case 2:
                builder.Prepend(info.CurrencySymbol, info.NegativeSign);
                break;

            case 3:
                builder.Prepend(info.CurrencySymbol).Append(info.NegativeSign);
                break;

            case 4:
                builder.Append(info.CurrencySymbol).Wrap(WrapStart, WrapEnd);
                break;

            case 5:
                builder.Prepend(info.NegativeSign).Append(info.CurrencySymbol);
                break;

            case 6:
                builder.Append(info.NegativeSign, info.CurrencySymbol);
                break;

            case 7:
                builder.Append(info.CurrencySymbol, info.NegativeSign);
                break;

            case 8:
                builder.Prepend(info.NegativeSign).Append(Space, info.CurrencySymbol);
                break;

            case 9:
                builder.Prepend(info.NegativeSign, info.CurrencySymbol, Space);
                break;

            case 10:
                builder.Append(Space, info.CurrencySymbol, info.NegativeSign);
                break;

            case 11:
                builder.Prepend(info.CurrencySymbol, Space).Append(info.NegativeSign);
                break;

            case 12:
                builder.Prepend(info.CurrencySymbol, Space, info.NegativeSign);
                break;

            case 13:
                builder.Append(info.NegativeSign, Space, info.CurrencySymbol);
                break;

            case 14:
                builder.Prepend(info.CurrencySymbol, Space).Wrap(WrapStart, WrapEnd);
                break;

            case 15:
                builder.Append(Space, info.CurrencySymbol).Wrap(WrapStart, WrapEnd);
                break;

            case 16:
                builder.Prepend(info.CurrencySymbol, info.NegativeSign, Space);
                break;
        }
    }

    private static void FormatNumberNegativePattern(StringBuilder builder, BigDecimal value, NumberFormatInfo info)
    {
        if (BigDecimal.IsPositive(value)) return;

        switch (info.NumberNegativePattern)
        {
            case 0:
                builder.Wrap(WrapStart, WrapEnd);
                break;

            case 1:
                builder.Prepend(info.NegativeSign);
                break;

            case 2:
                builder.Prepend(info.NegativeSign, Space);
                break;

            case 3:
                builder.Append(info.NegativeSign);
                break;

            case 4:
                builder.Append(Space, info.NegativeSign);
                break;
        }
    }

    private static void FormatPercentPositivePattern(StringBuilder builder, BigDecimal value, NumberFormatInfo info)
    {
        if (BigDecimal.IsNegative(value)) return;

        switch (info.PercentPositivePattern)
        {
            case 0:
                builder.Append(Space, info.PercentSymbol);
                break;

            case 1:
                builder.Append(info.PercentSymbol);
                break;

            case 2:
                builder.Prepend(info.PercentSymbol);
                break;

            case 4:
                builder.Prepend(info.PercentSymbol, Space);
                break;
        }
    }

    private static void FormatPercentNegativePattern(StringBuilder builder, BigDecimal value, NumberFormatInfo info)
    {
        if (BigDecimal.IsPositive(value)) return;

        switch (info.PercentNegativePattern)
        {
            case 0:
                builder.Prepend(info.NegativeSign).Append(Space, info.PercentSymbol);
                break;

            case 1:
                builder.Prepend(info.NegativeSign).Append(info.PercentSymbol);
                break;

            case 2:
                builder.Prepend(info.NegativeSign, info.PercentSymbol);
                break;

            case 3:
                builder.Prepend(info.PercentSymbol, info.NegativeSign);
                break;

            case 4:
                builder.Prepend(info.PercentSymbol).Append(info.NegativeSign);
                break;

            case 5:
                builder.Append(info.NegativeSign, info.PercentSymbol);
                break;

            case 6:
                builder.Append(info.PercentSymbol, info.NegativeSign);
                break;

            case 7:
                builder.Prepend(info.NegativeSign, info.PercentSymbol);
                break;

            case 8:
                builder.Append(Space, info.PercentSymbol, info.NegativeSign);
                break;

            case 9:
                builder.Prepend(info.PercentSymbol, Space).Append(info.NegativeSign);
                break;

            case 10:
                builder.Prepend(info.PercentSymbol, Space, info.NegativeSign);
                break;

            case 11:
                builder.Append(info.NegativeSign, Space, info.PercentSymbol);
                break;
        }
    }

    private static bool TryGetSpecifier(ReadOnlySpan<char> format, out char result)
    {
        if (format.IsEmpty || format.IsWhiteSpace())
        {
            result = 'G';
            return true;
        }

        if (!ArrayOf('C', 'D', 'E', 'F', 'G', 'N', 'P').Contains(char.ToUpperInvariant(format[0])))
        {
            result = default;
            return false;
        }

        result = format[0];
        return true;
    }

    private static bool TryGetScaledValue(ReadOnlySpan<char> format, BigDecimal value, int scale, out BigDecimal result)
    {
        if (format.Length > 1 && !int.TryParse(format[1..], out scale))
        {
            result = default;
            return false;
        }

        if (scale < 0)
        {
            result = default;
            return false;
        }

        result = value.SetScale(scale);
        return true;
    }
}
