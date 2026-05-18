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

        int significantDigits = ResolveSignificantDigits(format);
        NumberInfo numberInfo = ToDecimalNumberInfo(this, significantDigits);
        return numberInfo.ToString(format.ToString(), formatProvider);
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
    /// Converts the specified finite, non-zero <see cref="Float128"/> value to a <see cref="NumberInfo"/> with at most <paramref name="significantDigits"/> significant decimal digits, rounded to nearest, ties-to-even.
    /// </summary>
    /// <param name="value">The finite, non-zero value to convert.</param>
    /// <param name="significantDigits">The maximum number of significant decimal digits to retain in the result.</param>
    /// <returns>Returns a <see cref="NumberInfo"/> whose unscaled value contains the rounded decimal digits and whose scale positions the decimal point.</returns>
    /// <remarks>
    /// Routes the conversion through a lossless widening to <see cref="Float256"/> so the intermediate
    /// power-of-ten multiplications retain full binary128 precision. Float128 alone cannot hold
    /// <c>value × 10^(N-1-D)</c> for typical N=36 requests (the product can exceed 113 bits), but Float256's
    /// 237-bit significand has plenty of headroom — keeping the binary→decimal round-trip exact.
    /// </remarks>
    private static NumberInfo ToDecimalNumberInfo(Float128 value, int significantDigits)
    {
        Float256 widened = (Float256)value;
        return Float256.ToDecimalNumberInfo(widened, significantDigits);
    }
}
