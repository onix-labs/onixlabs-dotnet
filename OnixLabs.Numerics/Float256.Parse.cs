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

public readonly partial struct Float256
{
    /// <summary>
    /// The default <see cref="NumberStyles"/> permitted when parsing a <see cref="Float256"/> without an explicit style argument.
    /// </summary>
    private const NumberStyles DefaultNumberStyles = NumberStyles.Float | NumberStyles.AllowThousands;

    /// <summary>Parses the specified string value into a <see cref="Float256"/> value.</summary>
    /// <param name="value">The value to parse.</param>
    /// <param name="provider">An object that provides culture-specific information about the specified value.</param>
    /// <returns>Returns the parsed <see cref="Float256"/> value.</returns>
    public static Float256 Parse(string value, IFormatProvider? provider = null) => Parse(value.AsSpan(), provider);

    /// <summary>Parses the specified string value into a <see cref="Float256"/> value.</summary>
    /// <param name="value">The value to parse.</param>
    /// <param name="style">A bitwise combination of number styles that can be present in the specified value.</param>
    /// <param name="provider">An object that provides culture-specific information about the specified value.</param>
    /// <returns>Returns the parsed <see cref="Float256"/> value.</returns>
    public static Float256 Parse(string value, NumberStyles style, IFormatProvider? provider = null) => Parse(value.AsSpan(), style, provider);

    /// <summary>Parses the specified span value into a <see cref="Float256"/> value.</summary>
    /// <param name="value">The value to parse.</param>
    /// <param name="provider">An object that provides culture-specific information about the specified value.</param>
    /// <returns>Returns the parsed <see cref="Float256"/> value.</returns>
    public static Float256 Parse(ReadOnlySpan<char> value, IFormatProvider? provider = null) => Parse(value, DefaultNumberStyles, provider);

    /// <summary>Parses the specified span value into a <see cref="Float256"/> value.</summary>
    /// <param name="value">The value to parse.</param>
    /// <param name="style">A bitwise combination of number styles that can be present in the specified value.</param>
    /// <param name="provider">An object that provides culture-specific information about the specified value.</param>
    /// <returns>Returns the parsed <see cref="Float256"/> value.</returns>
    /// <exception cref="FormatException">Thrown when <paramref name="value"/> is not in a correct format.</exception>
    public static Float256 Parse(ReadOnlySpan<char> value, NumberStyles style, IFormatProvider? provider = null)
    {
        if (TryParse(value, style, provider, out Float256 result)) return result;
        throw new FormatException($"The input string '{value.ToString()}' was not in a correct format.");
    }

    /// <summary>Tries to parse the specified string value into a <see cref="Float256"/> value.</summary>
    /// <param name="value">The value to parse.</param>
    /// <param name="result">When this method returns, contains the parsed value if successful; otherwise, the default value.</param>
    /// <returns>Returns <see langword="true"/> if the value was parsed successfully; otherwise, <see langword="false"/>.</returns>
    public static bool TryParse(string? value, out Float256 result) => TryParse(value.AsSpan(), out result);

    /// <summary>Tries to parse the specified string value into a <see cref="Float256"/> value.</summary>
    /// <param name="value">The value to parse.</param>
    /// <param name="provider">An object that provides culture-specific information about the specified value.</param>
    /// <param name="result">When this method returns, contains the parsed value if successful; otherwise, the default value.</param>
    /// <returns>Returns <see langword="true"/> if the value was parsed successfully; otherwise, <see langword="false"/>.</returns>
    public static bool TryParse(string? value, IFormatProvider? provider, out Float256 result) => TryParse(value.AsSpan(), provider, out result);

    /// <summary>Tries to parse the specified string value into a <see cref="Float256"/> value.</summary>
    /// <param name="value">The value to parse.</param>
    /// <param name="style">A bitwise combination of number styles that can be present in the specified value.</param>
    /// <param name="provider">An object that provides culture-specific information about the specified value.</param>
    /// <param name="result">When this method returns, contains the parsed value if successful; otherwise, the default value.</param>
    /// <returns>Returns <see langword="true"/> if the value was parsed successfully; otherwise, <see langword="false"/>.</returns>
    public static bool TryParse(string? value, NumberStyles style, IFormatProvider? provider, out Float256 result) => TryParse(value.AsSpan(), style, provider, out result);

    /// <summary>Tries to parse the specified span value into a <see cref="Float256"/> value.</summary>
    /// <param name="value">The value to parse.</param>
    /// <param name="result">When this method returns, contains the parsed value if successful; otherwise, the default value.</param>
    /// <returns>Returns <see langword="true"/> if the value was parsed successfully; otherwise, <see langword="false"/>.</returns>
    public static bool TryParse(ReadOnlySpan<char> value, out Float256 result) => TryParse(value, CultureInfo.CurrentCulture, out result);

    /// <summary>Tries to parse the specified span value into a <see cref="Float256"/> value.</summary>
    /// <param name="value">The value to parse.</param>
    /// <param name="provider">An object that provides culture-specific information about the specified value.</param>
    /// <param name="result">When this method returns, contains the parsed value if successful; otherwise, the default value.</param>
    /// <returns>Returns <see langword="true"/> if the value was parsed successfully; otherwise, <see langword="false"/>.</returns>
    public static bool TryParse(ReadOnlySpan<char> value, IFormatProvider? provider, out Float256 result) => TryParse(value, DefaultNumberStyles, provider, out result);

    /// <summary>Tries to parse the specified span value into a <see cref="Float256"/> value.</summary>
    /// <param name="value">The value to parse.</param>
    /// <param name="style">A bitwise combination of number styles that can be present in the specified value.</param>
    /// <param name="provider">An object that provides culture-specific information about the specified value.</param>
    /// <param name="result">When this method returns, contains the parsed value if successful; otherwise, the default value.</param>
    /// <returns>Returns <see langword="true"/> if the value was parsed successfully; otherwise, <see langword="false"/>.</returns>
    public static bool TryParse(ReadOnlySpan<char> value, NumberStyles style, IFormatProvider? provider, out Float256 result)
    {
        NumberFormatInfo numberFormat = NumberFormatInfo.GetInstance(provider);
        ReadOnlySpan<char> trimmed = value.Trim();

        if (TryParseSpecialValue(trimmed, numberFormat, out result)) return true;

        if (TryParseBigDecimalWithExponent(value, style, provider, out BigDecimal parsed))
        {
            result = (Float256)parsed;
            return true;
        }

        result = default;
        return false;
    }

    /// <summary>
    /// Tries to parse a numeric span as a <see cref="BigDecimal"/>, splitting on an exponent marker (<c>E</c> or <c>e</c>) and folding the decimal exponent into the result.
    /// </summary>
    /// <param name="value">The value to parse.</param>
    /// <param name="style">A bitwise combination of number styles that may be present in <paramref name="value"/>.</param>
    /// <param name="provider">An object that provides culture-specific information about <paramref name="value"/>.</param>
    /// <param name="result">When this method returns, contains the parsed <see cref="BigDecimal"/> value if successful; otherwise, the default value.</param>
    /// <returns>Returns <see langword="true"/> if <paramref name="value"/> was parsed successfully; otherwise, <see langword="false"/>.</returns>
    private static bool TryParseBigDecimalWithExponent(ReadOnlySpan<char> value, NumberStyles style, IFormatProvider? provider, out BigDecimal result)
    {
        int exponentMarkerIndex = -1;
        for (int i = 0; i < value.Length; i++)
        {
            if (value[i] == 'E' || value[i] == 'e')
            {
                exponentMarkerIndex = i;
                break;
            }
        }

        if (exponentMarkerIndex < 0)
        {
            return BigDecimal.TryParse(value, style, provider, out result);
        }

        ReadOnlySpan<char> mantissaSpan = value[..exponentMarkerIndex];
        ReadOnlySpan<char> exponentSpan = value[(exponentMarkerIndex + 1)..];

        NumberStyles mantissaStyle = style & ~NumberStyles.AllowExponent;
        if (!BigDecimal.TryParse(mantissaSpan, mantissaStyle, provider, out BigDecimal mantissa))
        {
            result = default;
            return false;
        }

        if (!int.TryParse(exponentSpan, NumberStyles.Integer, provider, out int decimalExponent))
        {
            result = default;
            return false;
        }

        result = ApplyDecimalExponent(mantissa, decimalExponent);
        return true;
    }

    /// <summary>
    /// Applies a decimal exponent to a parsed mantissa by adjusting its scale or unscaled value as appropriate.
    /// </summary>
    /// <param name="mantissa">The parsed mantissa.</param>
    /// <param name="decimalExponent">The decimal exponent to apply.</param>
    /// <returns>Returns a <see cref="BigDecimal"/> equal to <c>mantissa × 10^decimalExponent</c>.</returns>
    private static BigDecimal ApplyDecimalExponent(BigDecimal mantissa, int decimalExponent)
    {
        if (decimalExponent == 0) return mantissa;

        if (decimalExponent > 0)
        {
            int absorbedScale = Math.Min(mantissa.Scale, decimalExponent);
            int newScale = mantissa.Scale - absorbedScale;
            int remainingExponent = decimalExponent - absorbedScale;

            BigInteger newUnscaledValue = remainingExponent > 0
                ? mantissa.UnscaledValue * BigInteger.Pow(10, remainingExponent)
                : mantissa.UnscaledValue;

            return new BigDecimal(newUnscaledValue, newScale);
        }

        int additionalScale = -decimalExponent;
        return new BigDecimal(mantissa.UnscaledValue, mantissa.Scale + additionalScale);
    }

    /// <summary>
    /// Tries to recognise the specified trimmed span as one of the culture-specific special-value symbols (<c>NaN</c>, positive infinity, or negative infinity).
    /// </summary>
    /// <param name="trimmed">The trimmed input span to inspect.</param>
    /// <param name="numberFormat">The number format providing the special-value symbols.</param>
    /// <param name="result">When this method returns, contains the matched <see cref="Float256"/> value if successful; otherwise, the default value.</param>
    /// <returns>Returns <see langword="true"/> if <paramref name="trimmed"/> matches a special-value symbol; otherwise, <see langword="false"/>.</returns>
    private static bool TryParseSpecialValue(ReadOnlySpan<char> trimmed, NumberFormatInfo numberFormat, out Float256 result)
    {
        if (trimmed.SequenceEqual(numberFormat.NaNSymbol.AsSpan()))
        {
            result = NaN;
            return true;
        }

        if (trimmed.SequenceEqual(numberFormat.PositiveInfinitySymbol.AsSpan()))
        {
            result = PositiveInfinity;
            return true;
        }

        if (trimmed.SequenceEqual(numberFormat.NegativeInfinitySymbol.AsSpan()))
        {
            result = NegativeInfinity;
            return true;
        }

        if (trimmed.Length > 0 && trimmed[0] == '+'
            && trimmed[1..].SequenceEqual(numberFormat.PositiveInfinitySymbol.AsSpan()))
        {
            result = PositiveInfinity;
            return true;
        }

        result = default;
        return false;
    }
}
