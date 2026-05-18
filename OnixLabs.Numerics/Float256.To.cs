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
    /// The maximum number of significant decimal digits sufficient to uniquely identify any <see cref="Float256"/> value when parsed back.
    /// </summary>
    /// <remarks>The IEEE 754 binary256 round-trip digit count is 73.</remarks>
    public const int MaxRoundTripDigits = 73;

    /// <summary>
    /// The literal used to format a NaN <see cref="Float256"/> value.
    /// </summary>
    private const string NaNSymbol = "NaN";

    /// <summary>
    /// The literal used to format a positive infinity <see cref="Float256"/> value.
    /// </summary>
    private const string PositiveInfinitySymbol = "Infinity";

    /// <summary>
    /// The literal used to format a negative infinity <see cref="Float256"/> value.
    /// </summary>
    private const string NegativeInfinitySymbol = "-Infinity";

    /// <summary>
    /// The literal used to format a positive zero <see cref="Float256"/> value.
    /// </summary>
    private const string PositiveZeroSymbol = "0";

    /// <summary>
    /// The literal used to format a negative zero <see cref="Float256"/> value.
    /// </summary>
    private const string NegativeZeroSymbol = "-0";

    /// <summary>
    /// The default format specifier used when none is supplied to <see cref="ToString()"/>.
    /// </summary>
    private const string DefaultFormatSpecifier = "G";

    /// <summary>Formats the value of the current instance using the default format and current culture.</summary>
    /// <returns>Returns the value of the current instance formatted using the default format.</returns>
    public override string ToString() => ToString(DefaultFormatSpecifier, CultureInfo.CurrentCulture);

    /// <summary>Formats the value of the current instance using the specified format and format provider.</summary>
    /// <param name="format">The format to use, or <see langword="null"/> to use the default format.</param>
    /// <param name="formatProvider">The provider to use to format the value, or <see langword="null"/> to use the current culture.</param>
    /// <returns>Returns the value of the current instance formatted using the specified format and provider.</returns>
    public string ToString(string? format, IFormatProvider? formatProvider = null) => ToString((format ?? DefaultFormatSpecifier).AsSpan(), formatProvider);

    /// <summary>Formats the value of the current instance using the specified format and format provider.</summary>
    /// <param name="format">A span containing the characters of the format specifier.</param>
    /// <param name="formatProvider">The provider to use to format the value, or <see langword="null"/> to use the current culture.</param>
    /// <returns>Returns the value of the current instance formatted using the specified format and provider.</returns>
    public string ToString(ReadOnlySpan<char> format, IFormatProvider? formatProvider = null)
    {
        if (IsNaN(this)) return NaNSymbol;
        if (IsPositiveInfinity(this)) return PositiveInfinitySymbol;
        if (IsNegativeInfinity(this)) return NegativeInfinitySymbol;
        if (IsZero(this)) return IsNegative(this) ? NegativeZeroSymbol : PositiveZeroSymbol;

        BigDecimal exactValue = (BigDecimal)this;
        int digitsForFormat = ResolveSignificantDigits(format);
        BigDecimal rounded = RoundToSignificantDigits(exactValue, digitsForFormat);
        BigDecimal trimmed = TrimFractionalTrailingZeros(rounded);

        return trimmed.ToString(format.ToString(), formatProvider);
    }

    /// <summary>
    /// Removes any redundant trailing zero decimal places from the specified <see cref="BigDecimal"/> while keeping the scale non-negative.
    /// </summary>
    /// <param name="value">The value whose scale should be minimised.</param>
    /// <returns>Returns a <see cref="BigDecimal"/> equal to <paramref name="value"/> but with the smallest non-negative scale that still represents it exactly.</returns>
    private static BigDecimal TrimFractionalTrailingZeros(BigDecimal value)
    {
        if (BigDecimal.IsZero(value)) return value;
        if (value.Scale == 0) return value;

        BigInteger remainingUnscaledValue = value.UnscaledValue;
        int currentScale = value.Scale;
        BigInteger ten = (BigInteger)10;

        while (currentScale > 0)
        {
            BigInteger quotient = BigInteger.DivRem(remainingUnscaledValue, ten, out BigInteger remainder);
            if (!remainder.IsZero) break;
            remainingUnscaledValue = quotient;
            currentScale--;
        }

        return new BigDecimal(remainingUnscaledValue, currentScale);
    }

    /// <summary>
    /// Resolves the number of significant decimal digits to retain when formatting, based on the specified format specifier.
    /// </summary>
    /// <param name="format">The format specifier; an empty or whitespace span yields the round-trip default.</param>
    /// <returns>Returns the number of significant decimal digits to retain.</returns>
    private static int ResolveSignificantDigits(ReadOnlySpan<char> format)
    {
        if (format.IsEmpty || format.IsWhiteSpace()) return MaxRoundTripDigits;

        char specifier = char.ToUpperInvariant(format[0]);
        if (specifier == 'G' || specifier == 'R')
        {
            if (format.Length > 1 && int.TryParse(format[1..], NumberStyles.None, CultureInfo.InvariantCulture, out int digits) && digits > 0)
            {
                return digits;
            }

            return MaxRoundTripDigits;
        }

        return MaxRoundTripDigits;
    }

    /// <summary>
    /// Rounds the specified <see cref="BigDecimal"/> to at most <paramref name="maxDigits"/> significant decimal digits using round-half-to-even.
    /// </summary>
    /// <param name="value">The value to round.</param>
    /// <param name="maxDigits">The maximum number of significant decimal digits to retain.</param>
    /// <returns>Returns <paramref name="value"/> rounded to <paramref name="maxDigits"/> significant digits.</returns>
    private static BigDecimal RoundToSignificantDigits(BigDecimal value, int maxDigits)
    {
        if (BigDecimal.IsZero(value)) return value;

        BigInteger absoluteUnscaledValue = BigInteger.Abs(value.UnscaledValue);
        int currentDigits = CountDecimalDigits(absoluteUnscaledValue);

        if (currentDigits <= maxDigits) return value;

        int excessDigits = currentDigits - maxDigits;
        int newScale = value.Scale - excessDigits;

        if (newScale >= 0) return value.SetScale(newScale, MidpointRounding.ToEven);

        BigInteger divisor = BigInteger.Pow(10, excessDigits);
        BigInteger quotient = BigInteger.DivRem(value.UnscaledValue, divisor, out BigInteger remainder);
        BigInteger halfDivisor = divisor >> 1;
        BigInteger absoluteRemainder = BigInteger.Abs(remainder);

        if (absoluteRemainder > halfDivisor || (absoluteRemainder == halfDivisor && !quotient.IsEven))
        {
            quotient += value.UnscaledValue.Sign;
        }

        BigInteger paddedUnscaledValue = quotient * divisor;
        return new BigDecimal(paddedUnscaledValue, 0);
    }

    /// <summary>
    /// Counts the number of decimal digits in the absolute value of the specified <see cref="BigInteger"/>.
    /// </summary>
    /// <param name="value">The value whose decimal digits should be counted.</param>
    /// <returns>Returns the number of decimal digits; returns <c>1</c> when <paramref name="value"/> is zero.</returns>
    private static int CountDecimalDigits(BigInteger value)
    {
        if (value.IsZero) return 1;
        return value.ToString().Length;
    }
}
