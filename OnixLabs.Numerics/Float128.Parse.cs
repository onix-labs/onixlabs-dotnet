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

public readonly partial struct Float128
{
    /// <summary>
    /// The default <see cref="NumberStyles"/> combination used when no explicit style is supplied to <see cref="Parse(string, IFormatProvider?)"/> or <see cref="TryParse(string?, out Float128)"/>.
    /// </summary>
    private const NumberStyles DefaultNumberStyles = NumberStyles.Float | NumberStyles.AllowThousands;

    /// <summary>
    /// Parses the specified <see cref="string"/> value into a <see cref="Float128"/> value.
    /// </summary>
    /// <param name="value">The value to parse.</param>
    /// <param name="provider">An object that provides culture-specific information about the specified value.</param>
    /// <returns>Returns a new <see cref="Float128"/> instance parsed from the specified value.</returns>
    /// <exception cref="FormatException">Thrown when <paramref name="value"/> is not in a correct format.</exception>
    public static Float128 Parse(string value, IFormatProvider? provider = null) => Parse(value.AsSpan(), provider);

    /// <summary>
    /// Parses the specified <see cref="string"/> value into a <see cref="Float128"/> value.
    /// </summary>
    /// <param name="value">The value to parse.</param>
    /// <param name="style">A bitwise combination of number styles that can be present in the specified value.</param>
    /// <param name="provider">An object that provides culture-specific information about the specified value.</param>
    /// <returns>Returns a new <see cref="Float128"/> instance parsed from the specified value.</returns>
    /// <exception cref="FormatException">Thrown when <paramref name="value"/> is not in a correct format.</exception>
    public static Float128 Parse(string value, NumberStyles style, IFormatProvider? provider = null) => Parse(value.AsSpan(), style, provider);

    /// <summary>
    /// Parses the specified <see cref="ReadOnlySpan{T}"/> value into a <see cref="Float128"/> value.
    /// </summary>
    /// <param name="value">The value to parse.</param>
    /// <param name="provider">An object that provides culture-specific information about the specified value.</param>
    /// <returns>Returns a new <see cref="Float128"/> instance parsed from the specified value.</returns>
    /// <exception cref="FormatException">Thrown when <paramref name="value"/> is not in a correct format.</exception>
    public static Float128 Parse(ReadOnlySpan<char> value, IFormatProvider? provider = null) => Parse(value, DefaultNumberStyles, provider);

    /// <summary>
    /// Parses the specified <see cref="ReadOnlySpan{T}"/> value into a <see cref="Float128"/> value.
    /// </summary>
    /// <param name="value">The value to parse.</param>
    /// <param name="style">A bitwise combination of number styles that can be present in the specified value.</param>
    /// <param name="provider">An object that provides culture-specific information about the specified value.</param>
    /// <returns>Returns a new <see cref="Float128"/> instance parsed from the specified value.</returns>
    /// <exception cref="FormatException">Thrown when <paramref name="value"/> is not in a correct format.</exception>
    public static Float128 Parse(ReadOnlySpan<char> value, NumberStyles style, IFormatProvider? provider = null)
    {
        if (TryParse(value, style, provider, out Float128 result)) return result;
        throw new FormatException($"The input string '{value.ToString()}' was not in a correct format.");
    }

    /// <summary>
    /// Tries to parse the specified <see cref="string"/> value into a <see cref="Float128"/> value.
    /// </summary>
    /// <param name="value">The value to parse.</param>
    /// <param name="result">When this method returns, contains the parsed value if successful; otherwise, the default value.</param>
    /// <returns>Returns <see langword="true"/> if the value was parsed successfully; otherwise, <see langword="false"/>.</returns>
    public static bool TryParse(string? value, out Float128 result) => TryParse(value.AsSpan(), out result);

    /// <summary>
    /// Tries to parse the specified <see cref="string"/> value into a <see cref="Float128"/> value.
    /// </summary>
    /// <param name="value">The value to parse.</param>
    /// <param name="provider">An object that provides culture-specific information about the specified value.</param>
    /// <param name="result">When this method returns, contains the parsed value if successful; otherwise, the default value.</param>
    /// <returns>Returns <see langword="true"/> if the value was parsed successfully; otherwise, <see langword="false"/>.</returns>
    public static bool TryParse(string? value, IFormatProvider? provider, out Float128 result) => TryParse(value.AsSpan(), provider, out result);

    /// <summary>
    /// Tries to parse the specified <see cref="string"/> value into a <see cref="Float128"/> value.
    /// </summary>
    /// <param name="value">The value to parse.</param>
    /// <param name="style">A bitwise combination of number styles that can be present in the specified value.</param>
    /// <param name="provider">An object that provides culture-specific information about the specified value.</param>
    /// <param name="result">When this method returns, contains the parsed value if successful; otherwise, the default value.</param>
    /// <returns>Returns <see langword="true"/> if the value was parsed successfully; otherwise, <see langword="false"/>.</returns>
    public static bool TryParse(string? value, NumberStyles style, IFormatProvider? provider, out Float128 result) => TryParse(value.AsSpan(), style, provider, out result);

    /// <summary>
    /// Tries to parse the specified <see cref="ReadOnlySpan{T}"/> value into a <see cref="Float128"/> value.
    /// </summary>
    /// <param name="value">The value to parse.</param>
    /// <param name="result">When this method returns, contains the parsed value if successful; otherwise, the default value.</param>
    /// <returns>Returns <see langword="true"/> if the value was parsed successfully; otherwise, <see langword="false"/>.</returns>
    public static bool TryParse(ReadOnlySpan<char> value, out Float128 result) => TryParse(value, CultureInfo.CurrentCulture, out result);

    /// <summary>
    /// Tries to parse the specified <see cref="ReadOnlySpan{T}"/> value into a <see cref="Float128"/> value.
    /// </summary>
    /// <param name="value">The value to parse.</param>
    /// <param name="provider">An object that provides culture-specific information about the specified value.</param>
    /// <param name="result">When this method returns, contains the parsed value if successful; otherwise, the default value.</param>
    /// <returns>Returns <see langword="true"/> if the value was parsed successfully; otherwise, <see langword="false"/>.</returns>
    public static bool TryParse(ReadOnlySpan<char> value, IFormatProvider? provider, out Float128 result) => TryParse(value, DefaultNumberStyles, provider, out result);

    /// <summary>
    /// Tries to parse the specified <see cref="ReadOnlySpan{T}"/> value into a <see cref="Float128"/> value.
    /// </summary>
    /// <param name="value">The value to parse.</param>
    /// <param name="style">A bitwise combination of number styles that can be present in the specified value.</param>
    /// <param name="provider">An object that provides culture-specific information about the specified value.</param>
    /// <param name="result">When this method returns, contains the parsed value if successful; otherwise, the default value.</param>
    /// <returns>Returns <see langword="true"/> if the value was parsed successfully; otherwise, <see langword="false"/>.</returns>
    public static bool TryParse(ReadOnlySpan<char> value, NumberStyles style, IFormatProvider? provider, out Float128 result)
    {
        NumberFormatInfo numberFormat = NumberFormatInfo.GetInstance(provider);
        ReadOnlySpan<char> trimmed = value.Trim();

        if (TryParseSpecialValue(trimmed, numberFormat, out result)) return true;

        if (TryParseBigDecimalWithExponent(value, style, provider, out BigDecimal parsed))
        {
            result = (Float128)parsed;
            return true;
        }

        result = default;
        return false;
    }

    /// <summary>
    /// Attempts to parse the specified character span as a <see cref="BigDecimal"/>, accepting an optional decimal exponent of the form <c>E±n</c>.
    /// </summary>
    /// <param name="value">The character span to parse.</param>
    /// <param name="style">The <see cref="NumberStyles"/> permitted within the mantissa portion.</param>
    /// <param name="provider">An object that provides culture-specific information.</param>
    /// <param name="result">When this method returns, contains the parsed <see cref="BigDecimal"/> if successful; otherwise, the default value.</param>
    /// <returns><see langword="true"/> if the value was parsed successfully; otherwise, <see langword="false"/>.</returns>
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
    /// Applies the specified decimal exponent to the parsed mantissa, adjusting its scale or unscaled value to represent <c>mantissa × 10^decimalExponent</c>.
    /// </summary>
    /// <param name="mantissa">The <see cref="BigDecimal"/> mantissa.</param>
    /// <param name="decimalExponent">The signed decimal exponent.</param>
    /// <returns>The <see cref="BigDecimal"/> value obtained after applying <paramref name="decimalExponent"/>.</returns>
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
    /// Attempts to match the specified character span against the culture-specific NaN, positive infinity, or negative infinity symbols.
    /// </summary>
    /// <param name="trimmed">The trimmed character span to inspect.</param>
    /// <param name="numberFormat">The culture-specific number format providing the special-value symbols.</param>
    /// <param name="result">When this method returns, contains the matched special <see cref="Float128"/> value if successful; otherwise, the default value.</param>
    /// <returns><see langword="true"/> if <paramref name="trimmed"/> matches a special value; otherwise, <see langword="false"/>.</returns>
    private static bool TryParseSpecialValue(ReadOnlySpan<char> trimmed, NumberFormatInfo numberFormat, out Float128 result)
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
