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

namespace OnixLabs.Numerics;

internal sealed partial class NumberInfoFormatter
{
    /// <summary>
    /// Applies the scale implied by the format specifier to the current <see cref="value"/> field.
    /// </summary>
    /// <param name="specifier">The uppercase format specifier (e.g. <c>N</c>, <c>F</c>, <c>C</c>, <c>P</c>).</param>
    /// <param name="format">The complete format span, which may include a trailing precision suffix.</param>
    /// <returns>
    /// Returns <see langword="true"/> if the format scale was applied successfully, or <see langword="false"/> if the
    /// precision suffix is present but invalid (in which case the caller falls back to echoing the format string).
    /// </returns>
    private bool TryApplyFormatScale(char specifier, ReadOnlySpan<char> format)
    {
        // R (round-trip) and X (hexadecimal) preserve the exact unscaled value: they do not apply
        // decimal scaling, and any precision suffix is interpreted downstream by the formatter.
        if (specifier is 'R' or 'X') return true;

        // P scales the underlying value by 100 before applying decimal scaling.
        if (specifier is 'P') value = new NumberInfo(value.UnscaledValue * 100, value.Scale);

        if (format.Length > 1)
        {
            if (!int.TryParse(format[1..], NumberStyles.Integer, CultureInfo.InvariantCulture, out int scale) || scale < 0) return false;
            value = Rescale(value, scale);
            return true;
        }

        int defaultScale = specifier switch
        {
            'C' => numberFormat.CurrencyDecimalDigits,
            'F' => numberFormat.NumberDecimalDigits,
            'N' => numberFormat.NumberDecimalDigits,
            'P' => numberFormat.PercentDecimalDigits,
            _ => -1
        };

        if (defaultScale >= 0) value = Rescale(value, defaultScale);
        return true;
    }

    /// <summary>
    /// Returns a copy of the specified <see cref="NumberInfo"/> rescaled to the desired decimal scale.
    /// Uses <see cref="MidpointRounding.AwayFromZero"/> when the target scale is less than the current scale.
    /// </summary>
    private static NumberInfo Rescale(in NumberInfo value, int targetScale)
    {
        if (targetScale == value.Scale) return value;

        if (targetScale > value.Scale)
        {
            BigInteger magnitude = BigInteger.Pow(10, targetScale - value.Scale);
            return new NumberInfo(value.UnscaledValue * magnitude, targetScale);
        }

        BigInteger divisor = BigInteger.Pow(10, value.Scale - targetScale);
        BigInteger absValue = BigInteger.Abs(value.UnscaledValue);
        BigInteger quotient = absValue / divisor;
        BigInteger remainder = absValue % divisor;

        if (remainder * 2 >= divisor) quotient += 1;

        return new NumberInfo(value.UnscaledValue.Sign < 0 ? -quotient : quotient, targetScale);
    }
}
