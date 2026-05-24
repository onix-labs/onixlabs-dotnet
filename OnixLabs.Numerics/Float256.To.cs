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
    /// The literal used to format a positive zero <see cref="Float256"/> value.
    /// </summary>
    private const string PositiveZeroSymbol = "0";

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
        NumberFormatInfo numberFormat = NumberFormatInfo.GetInstance(formatProvider);

        if (IsNaN(this)) return numberFormat.NaNSymbol;
        if (IsPositiveInfinity(this)) return numberFormat.PositiveInfinitySymbol;
        if (IsNegativeInfinity(this)) return numberFormat.NegativeInfinitySymbol;
        if (IsZero(this))
        {
            // Only short-circuit zero for round-trip/general formats. Precision-bearing formats (N3, F2, C, P, …)
            // expect padded fractional digits, so route through NumberInfo for correct padding and grouping.
            if (format.IsEmpty || format.IsWhiteSpace() || IsRoundTripOrGeneralFormat(format))
                return IsNegative(this) ? numberFormat.NegativeSign + PositiveZeroSymbol : PositiveZeroSymbol;
            return NumberInfo.Zero.ToString(format.ToString(), formatProvider);
        }

        int significantDigits = ResolveSignificantDigits(format);
        NumberInfo numberInfo = ToDecimalNumberInfo(this, significantDigits);
        return numberInfo.ToString(format.ToString(), formatProvider);
    }

    /// <summary>
    /// Returns <see langword="true"/> if the supplied format specifier is <c>G</c> or <c>R</c>
    /// (with or without a digit suffix); otherwise <see langword="false"/>.
    /// </summary>
    private static bool IsRoundTripOrGeneralFormat(ReadOnlySpan<char> format)
    {
        char specifier = char.ToUpperInvariant(format[0]);
        return specifier is 'G' or 'R';
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
    /// Converts the specified finite, non-zero <see cref="Float256"/> value to a <see cref="NumberInfo"/> with at most <paramref name="significantDigits"/> significant decimal digits, rounded to nearest, ties-to-even.
    /// </summary>
    /// <param name="value">The finite, non-zero value to convert.</param>
    /// <param name="significantDigits">The maximum number of significant decimal digits to retain in the result.</param>
    /// <returns>Returns a <see cref="NumberInfo"/> whose unscaled value contains the rounded decimal digits and whose scale positions the decimal point.</returns>
    /// <remarks>
    /// The conversion goes binary → decimal natively: it estimates the decimal exponent via <see cref="Log10(Float256)"/>,
    /// scales the value by chunked exact <see cref="PowersOfTen"/> entries so the result lands in <c>[10^(N-1), 10^N)</c>, then
    /// rounds to an integer that fits losslessly in Float256's 237-bit significand. The intermediate
    /// multiplications introduce at most ~1 ULP of binary error per chunk, so the last decimal digit can
    /// occasionally disagree with the mathematically-exact rounded value at the very edges of the precision range.
    /// </remarks>
    internal static NumberInfo ToDecimalNumberInfo(Float256 value, int significantDigits)
    {
        bool sign = IsNegative(value);
        Float256 absValue = Abs(value);

        // Estimate the decimal exponent D such that 10^D ≤ |value| < 10^(D+1).
        int decimalExponent = (int)Floor(Log10(absValue));
        int decimalScale = significantDigits - 1 - decimalExponent;

        Float256 scaled = ScaleByPowerOfTen(absValue, decimalScale);
        Float256 rounded = Round(scaled, MidpointRounding.ToEven);
        BigInteger unscaledValue = (BigInteger)rounded;

        // Post-check: the rounded integer should have exactly `significantDigits` decimal digits
        // (i.e. lie in [10^(N-1), 10^N)). The Log10 estimate can be off by 1 — adjust here.
        BigInteger lowerLimit = BigInteger.Pow(10, significantDigits - 1);
        BigInteger upperLimit = lowerLimit * 10;

        while (unscaledValue >= upperLimit)
        {
            BigInteger remainder = unscaledValue % 10;
            unscaledValue /= 10;
            if (remainder > 5 || (remainder == 5 && !unscaledValue.IsEven)) unscaledValue += BigInteger.One;
            decimalScale--;
        }

        while (!unscaledValue.IsZero && unscaledValue < lowerLimit)
        {
            unscaledValue *= 10;
            decimalScale++;
        }

        if (sign) unscaledValue = -unscaledValue;

        // Trim trailing zeros so the output reads naturally (e.g. "1.5" rather than "1.50000…").
        while (decimalScale > 0 && !unscaledValue.IsZero && (unscaledValue % 10).IsZero)
        {
            unscaledValue /= 10;
            decimalScale--;
        }

        return new NumberInfo(unscaledValue, decimalScale);
    }

    /// <summary>
    /// Multiplies the specified non-negative value by <c>10^decimalScale</c>, supporting any integer scale by chunking through the precomputed <see cref="PowersOfTen"/> table.
    /// </summary>
    /// <param name="value">The non-negative finite value to scale.</param>
    /// <param name="decimalScale">The decimal exponent to apply; positive multiplies, negative divides, zero is a no-op.</param>
    /// <returns>Returns <paramref name="value"/> scaled by <c>10^decimalScale</c>, possibly with up to ~1 ULP of accumulated multiplicative error per chunk.</returns>
    private static Float256 ScaleByPowerOfTen(Float256 value, int decimalScale)
    {
        if (decimalScale == 0) return value;

        bool divide = decimalScale < 0;
        int remaining = decimalScale < 0 ? -decimalScale : decimalScale;
        int maxChunk = PowersOfTen.Length - 1; // 71 — the largest exact 10^k that Float256 can hold

        Float256 result = value;
        while (remaining > maxChunk)
        {
            Float256 chunk = PowersOfTen[maxChunk];
            result = divide ? result / chunk : result * chunk;
            remaining -= maxChunk;
        }

        if (remaining > 0)
        {
            Float256 chunk = PowersOfTen[remaining];
            result = divide ? result / chunk : result * chunk;
        }

        return result;
    }
}
