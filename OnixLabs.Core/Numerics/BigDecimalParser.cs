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
using static System.Globalization.NumberStyles;

namespace OnixLabs.Core.Numerics;

internal sealed partial class BigDecimalParser(NumberStyles style, NumberFormatInfo info)
{
    private const string LeadingParenthesis = "(";
    private const string TrailingParenthesis = ")";
    private const string ExponentSymbol = "e";
    private const StringComparison Comparison = StringComparison.InvariantCultureIgnoreCase;

    public bool TryParse(ReadOnlySpan<char> value, out BigDecimal result)
    {
        result = default;

        int sign = 1;
        int exponent = 0;

        // Disallow the following number styles as they are unsupported.
        if (style == None) return false;
        if (style.HasFlag(AllowHexSpecifier)) return false;
        if (style.HasFlag(AllowBinarySpecifier)) return false;

        // Special handling for sanitization of currency values.
        if (style == Currency)
        {
            if (!TrySanitizeCurrency(ref value, out sign)) return false;
        }
        else
        {
            if (!TrySanitizeNumber(ref value, out sign, out exponent)) return false;
        }

        // At this point, only digits, thousand and decimal separators should remain. 
        if (!TryGetRawBigDecimal(ref value, out RawBigDecimal rawResult)) return false;

        result = new BigDecimal(rawResult.UnscaledValue * sign, int.Max(rawResult.Scale - exponent, 0));
        return true;
    }

    private bool TryGetRawBigDecimal(ref ReadOnlySpan<char> value, out RawBigDecimal result)
    {
        result = default;

        BigInteger unscaledValue = BigInteger.Zero;
        int scale = 0;
        bool hasDecimalPoint = false;

        while (value.Length > 0)
        {
            if (char.IsAsciiDigit(value[0]))
            {
                int digit = value[0] - '0';
                unscaledValue *= 10;
                unscaledValue += digit;

                if (hasDecimalPoint) scale++;

                value = value[1..];
                continue;
            }

            if (value.StartsWith(info.NumberGroupSeparator))
            {
                if (!style.HasFlag(AllowThousands)) return false;
                value = value.TrimStart(info.NumberGroupSeparator);
                continue;
            }

            if (value.StartsWith(info.CurrencyGroupSeparator))
            {
                if (!style.HasFlag(AllowThousands)) return false;
                value = value.TrimStart(info.CurrencyGroupSeparator);
                continue;
            }

            if (value.StartsWith(info.NumberDecimalSeparator))
            {
                if (hasDecimalPoint || !style.HasFlag(AllowDecimalPoint)) return false;
                hasDecimalPoint = true;
                value = value.TrimStart(info.NumberDecimalSeparator);
                continue;
            }

            if (value.StartsWith(info.CurrencyDecimalSeparator))
            {
                if (hasDecimalPoint || !style.HasFlag(AllowDecimalPoint)) return false;
                hasDecimalPoint = true;
                value = value.TrimStart(info.CurrencyDecimalSeparator);
                continue;
            }

            // If we reach this point, the start of the string isn't an ascii digit, thousand or decimal separator, therefore false.
            return false;
        }

        result = new RawBigDecimal(unscaledValue, scale);
        return true;
    }
}
