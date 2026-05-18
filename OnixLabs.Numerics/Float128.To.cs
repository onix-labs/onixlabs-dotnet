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
    /// The maximum number of significant decimal digits sufficient to uniquely identify any <see cref="Float128"/> value when parsed back.
    /// </summary>
    /// <remarks>The IEEE 754 binary128 round-trip digit count is 36.</remarks>
    public const int MaxRoundTripDigits = 36;

    /// <summary>
    /// The textual symbol returned by <see cref="ToString()"/> for <see cref="NaN"/> values.
    /// </summary>
    private const string NaNSymbol = "NaN";

    /// <summary>
    /// The textual symbol returned by <see cref="ToString()"/> for <see cref="PositiveInfinity"/>.
    /// </summary>
    private const string PositiveInfinitySymbol = "Infinity";

    /// <summary>
    /// The textual symbol returned by <see cref="ToString()"/> for <see cref="NegativeInfinity"/>.
    /// </summary>
    private const string NegativeInfinitySymbol = "-Infinity";

    /// <summary>
    /// The textual symbol returned by <see cref="ToString()"/> for positive zero.
    /// </summary>
    private const string PositiveZeroSymbol = "0";

    /// <summary>
    /// The textual symbol returned by <see cref="ToString()"/> for <see cref="NegativeZero"/>.
    /// </summary>
    private const string NegativeZeroSymbol = "-0";

    /// <summary>
    /// The default format specifier used by <see cref="ToString()"/> when none is supplied by the caller.
    /// </summary>
    private const string DefaultFormatSpecifier = "G";

    /// <summary>
    /// Formats the value of the current instance using the default format and current culture.
    /// </summary>
    /// <returns>Returns the value of the current instance formatted using the default format.</returns>
    public override string ToString() => ToString(DefaultFormatSpecifier, CultureInfo.CurrentCulture);

    /// <summary>
    /// Formats the value of the current instance using the specified format and format provider.
    /// </summary>
    /// <param name="format">The format to use, or <see langword="null"/> to use the default format.</param>
    /// <param name="formatProvider">The provider to use to format the value, or <see langword="null"/> to use the current culture.</param>
    /// <returns>Returns the value of the current instance formatted using the specified format and provider.</returns>
    public string ToString(string? format, IFormatProvider? formatProvider = null) => ToString((format ?? DefaultFormatSpecifier).AsSpan(), formatProvider);

    /// <summary>
    /// Formats the value of the current instance using the specified format and format provider.
    /// </summary>
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
    /// Removes redundant trailing zeros from the fractional portion of the specified <see cref="BigDecimal"/>.
    /// </summary>
    /// <param name="value">The <see cref="BigDecimal"/> value to normalize.</param>
    /// <returns>An equivalent <see cref="BigDecimal"/> with redundant fractional trailing zeros trimmed.</returns>
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
    /// Resolves the number of significant digits requested by the specified format specifier, defaulting to <see cref="MaxRoundTripDigits"/> when unspecified.
    /// </summary>
    /// <param name="format">The format specifier to inspect.</param>
    /// <returns>The number of significant digits to use when rendering the value.</returns>
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
    /// Rounds the specified <see cref="BigDecimal"/> to at most the requested number of significant digits using banker's rounding.
    /// </summary>
    /// <param name="value">The <see cref="BigDecimal"/> value to round.</param>
    /// <param name="maxDigits">The maximum number of significant decimal digits permitted in the result.</param>
    /// <returns>A <see cref="BigDecimal"/> rounded to the requested significant digit count.</returns>
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
    /// Counts the number of decimal digits required to represent the specified <see cref="BigInteger"/> magnitude.
    /// </summary>
    /// <param name="value">The <see cref="BigInteger"/> whose digit count is required.</param>
    /// <returns>The number of decimal digits in the absolute value of <paramref name="value"/>, with at least one digit returned for zero.</returns>
    private static int CountDecimalDigits(BigInteger value)
    {
        if (value.IsZero) return 1;
        return value.ToString().Length;
    }
}
