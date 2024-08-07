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

namespace OnixLabs.Numerics;

internal sealed partial class NumberInfoParser(NumberStyles style, IFormatProvider culture)
{
    private const string LeadingParenthesis = "(";
    private const string TrailingParenthesis = ")";
    private const string ExponentSymbol = "e";
    private const StringComparison Comparison = StringComparison.InvariantCultureIgnoreCase;

    private readonly NumberFormatInfo numberFormat = NumberFormatInfo.GetInstance(culture);

    public bool TryParse(ReadOnlySpan<char> value, out NumberInfo result)
    {
        result = default;

        int sign = 1;
        int exponent = 0;

        // Disallow the following number styles as they are unsupported.
        if (style == None) return false;
        if ((style & AllowHexSpecifier) is not 0) return false;
        if ((style & AllowBinarySpecifier) is not 0) return false;

        // Special handling for sanitization of currency values.
        if (style == Currency)
        {
            if (!TrySanitizeCurrency(ref value, out sign)) return false;
        }
        else
        {
            if (!TrySanitizeNumber(ref value, out sign, out exponent)) return false;
        }

        // At this point, only digits, thousands and decimal separators should remain.
        if (!TryGetNumberInfo(ref value, out NumberInfo rawResult)) return false;

        result = new NumberInfo(rawResult.UnscaledValue * sign, int.Max(rawResult.Scale - exponent, 0));
        return true;
    }

    private bool TryGetNumberInfo(ref ReadOnlySpan<char> value, out NumberInfo result)
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

            if (value.StartsWith(numberFormat.NumberGroupSeparator))
            {
                if ((style & AllowThousands) is 0) return false;
                value = value.TrimStart(numberFormat.NumberGroupSeparator);
                continue;
            }

            if (value.StartsWith(numberFormat.CurrencyGroupSeparator))
            {
                if ((style & AllowThousands) is 0) return false;
                value = value.TrimStart(numberFormat.CurrencyGroupSeparator);
                continue;
            }

            if (value.StartsWith(numberFormat.NumberDecimalSeparator))
            {
                if (hasDecimalPoint || (style & AllowDecimalPoint) is 0) return false;
                hasDecimalPoint = true;
                value = value.TrimStart(numberFormat.NumberDecimalSeparator);
                continue;
            }

            // ReSharper disable once InvertIf
            if (value.StartsWith(numberFormat.CurrencyDecimalSeparator))
            {
                if (hasDecimalPoint || (style & AllowDecimalPoint) is 0) return false;
                hasDecimalPoint = true;
                value = value.TrimStart(numberFormat.CurrencyDecimalSeparator);
                continue;
            }

            // If we reach this point, the start of the string isn't an ascii digit, thousands or decimal separator, therefore false.
            return false;
        }

        result = new NumberInfo(unscaledValue, scale);
        return true;
    }
}
