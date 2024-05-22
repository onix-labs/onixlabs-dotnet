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

namespace OnixLabs.Numerics;

public readonly partial struct BigDecimal
{
    /// <summary>
    /// Gets a <see cref="NumberInfo"/> representing the current <see cref="BigDecimal"/>.
    /// </summary>
    /// <returns>Returns a <see cref="NumberInfo"/> representing the current <see cref="BigDecimal"/>.</returns>
    public NumberInfo ToNumberInfo() => number;

    /// <summary>
    /// Formats the value of the current instance using the default format.
    /// </summary>
    /// <returns>The value of the current instance in the default format.</returns>
    public override string ToString() => ToString(DefaultNumberFormat, DefaultCulture);

    /// <summary>
    /// Formats the value of the current instance using the specified format.
    /// </summary>
    /// <param name="format">The format to use, or null to use the default format.</param>
    /// <param name="formatProvider">The provider to use to format the value.</param>
    /// <returns>The value of the current instance in the specified format.</returns>
    public string ToString(string? format, IFormatProvider? formatProvider = null) => ToString((format ?? DefaultNumberFormat).AsSpan(), formatProvider);

    /// <summary>
    /// Formats the value of the current instance using the specified format.
    /// </summary>
    /// <param name="format">The format to use, or null to use the default format.</param>
    /// <param name="formatProvider">The provider to use to format the value.</param>
    /// <returns>The value of the current instance in the specified format.</returns>
    public string ToString(ReadOnlySpan<char> format, IFormatProvider? formatProvider = null)
    {
        CultureInfo info = formatProvider as CultureInfo ?? DefaultCulture;

        if (!TryGetScaledNumberInfo(format, info.NumberFormat, out NumberInfo value)) return format.ToString();

        NumberInfoFormatter formatter = new(value, info, ['C', 'D', 'E', 'F', 'G', 'N', 'P']);
        return formatter.Format(format);
    }

    /// <summary>
    /// Attempts to obtain a <see cref="NumberInfo"/> value that is scaled by either using a custom
    /// scale that is specified by the format, or by using the default scale for the specified format.
    /// </summary>
    /// <param name="format">The format specifier from which to obtain the desired scale.</param>
    /// <param name="numberFormat">The number format of the target culture that determines default scales for specific formats.</param>
    /// <param name="result">The <see cref="NumberInfo"/> value with a correctly applied scale.</param>
    /// <returns>Returns <see langword="true"/> if the scale is applied correctly; otherwise, <see langword="false"/>.</returns>
    private bool TryGetScaledNumberInfo(ReadOnlySpan<char> format, NumberFormatInfo numberFormat, out NumberInfo result)
    {
        const MidpointRounding mode = MidpointRounding.AwayFromZero;
        char specifier = format.IsEmpty || format.IsWhiteSpace() ? NumberInfoFormatter.DefaultFormat : format[0];

        if (format.Length > 1)
        {
            if (!int.TryParse(format[1..], out int scale))
            {
                result = default;
                return false;
            }

            result = SetScale(scale).number;
            return true;
        }

        result = char.ToUpperInvariant(specifier) switch
        {
            'C' => SetScale(numberFormat.CurrencyDecimalDigits, mode).ToNumberInfo(),
            'F' => SetScale(numberFormat.NumberDecimalDigits, mode).ToNumberInfo(),
            'N' => SetScale(numberFormat.NumberDecimalDigits, mode).ToNumberInfo(),
            'P' => Multiply(this, 100).SetScale(numberFormat.PercentDecimalDigits, mode).ToNumberInfo(),
            _ => ToNumberInfo()
        };

        return true;
    }
}
