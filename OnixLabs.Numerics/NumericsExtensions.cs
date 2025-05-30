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
using System.ComponentModel;
using System.Numerics;

namespace OnixLabs.Numerics;

/// <summary>
/// Provides extension methods for numeric types.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public static class NumericsExtensions
{
    /// <summary>
    /// The maximum scale of a <see cref="decimal"/> value.
    /// </summary>
    private const int MaxScale = 28;

    /// <summary>
    /// Gets the minimum value of a <see cref="decimal"/> value as a <see cref="BigInteger"/>.
    /// </summary>
    private static readonly BigInteger MinDecimal = new(decimal.MinValue);

    /// <summary>
    /// Gets the maximum value of a <see cref="decimal"/> value as a <see cref="BigInteger"/>.
    /// </summary>
    private static readonly BigInteger MaxDecimal = new(decimal.MaxValue);

    /// <summary>
    /// Gets the unscaled value of the current <see cref="decimal"/>.
    /// </summary>
    /// <param name="value">The <see cref="decimal"/> from which to obtain an unscaled value.</param>
    /// <returns>Returns the unscaled value of the current <see cref="decimal"/> as a <see cref="BigInteger"/>.</returns>
    public static BigInteger GetUnscaledValue(this decimal value)
    {
        const int significandSize = 13;
        int[] significandBits = decimal.GetBits(value);
        // ReSharper disable once HeapView.ObjectAllocation.Evident
        byte[] significandBytes = new byte[significandSize];
        Buffer.BlockCopy(significandBits, 0, significandBytes, 0, significandSize);
        BigInteger result = new(significandBytes);

        return decimal.IsPositive(value) ? result : -result;
    }

    /// <summary>
    /// Sets the scale (number of digits after the decimal point) of the current <see cref="decimal"/> value.
    /// <remarks>
    /// If the scale of the current <see cref="decimal"/> value is less than the specified scale, then the scale will be padded with zeroes.
    /// If the scale of the current <see cref="decimal"/> value is greater that the specified scale, then the scale will be truncated,
    /// provided that there is no loss of precision; otherwise, <see cref="InvalidOperationException"/> will be thrown.
    /// </remarks>
    /// </summary>
    /// <param name="value">The decimal value to adjust.</param>
    /// <param name="scale">The desired, non-negative scale.</param>
    /// <returns>A new <see cref="decimal"/> with the exact specified scale.</returns>
    /// <exception cref="InvalidOperationException"> if reducing the scale would result in a loss of precision.</exception>
    /// <exception cref="ArgumentException"> if <paramref name="scale"/> is negative.</exception>
    public static decimal SetScale(this decimal value, int scale)
    {
        RequireWithinRangeInclusive(scale, 0, MaxScale, "Scale must be within the inclusive range of 0 to 28.", nameof(scale));

        // Determine maximum representable scale given the integer part length
        BigInteger unscaledValue = value.GetUnscaledValue();
        BigInteger absUnscaled = BigInteger.Abs(unscaledValue);
        BigInteger factorCurrent = BigInteger.Pow(10, value.Scale);
        BigInteger integerPart = BigInteger.DivRem(absUnscaled, factorCurrent, out _);
        int integerDigits = integerPart.IsZero ? 1 : integerPart.ToString().Length;
        int maxPossibleScale = MaxScale - integerDigits;

        Require(scale <= maxPossibleScale, $"Maximum possible scale for the specified value is {maxPossibleScale}.", nameof(scale));

        // No change needed
        if (value.Scale == scale)
            return value;

        // Increase scale: pad with zeros
        if (value.Scale < scale)
        {
            int diff = scale - value.Scale;
            BigInteger padded = unscaledValue * BigInteger.Pow(10, diff);
            return padded.ToDecimal(scale);
        }

        // Decrease scale: drop extra digits
        int drop = value.Scale - scale;
        BigInteger divisor = BigInteger.Pow(10, drop);
        BigInteger quotient = BigInteger.DivRem(unscaledValue, divisor, out BigInteger remainder);

        // If there is any remainder, dropping would lose precision
        Check(remainder == 0, $"Cannot set scale to {scale} due to a loss of precision.");

        return quotient.ToDecimal(scale);
    }

    /// <summary>
    /// Sets the scale (number of digits after the decimal point) of the current <see cref="decimal"/> value.
    /// <remarks>
    /// If the scale of the current <see cref="decimal"/> value is less than the specified scale, then the scale will be padded with zeroes.
    /// If the scale of the current <see cref="decimal"/> value is greater that the specified scale, then the scale will be truncated,
    /// provided that there is no loss of precision; otherwise, the value is rounded using the specified <see cref="MidpointRounding"/> mode.
    /// </remarks>
    /// </summary>
    /// <param name="value">The decimal value to adjust.</param>
    /// <param name="scale">The desired scale (number of decimal digits). Must be non-negative.</param>
    /// <param name="mode">The rounding strategy to apply if the scale must be reduced with precision loss.</param>
    /// <returns>A new <see cref="decimal"/> with the exact specified scale.</returns>
    /// <exception cref="ArgumentException"> if <paramref name="scale"/> is negative.</exception>
    public static decimal SetScale(this decimal value, int scale, MidpointRounding mode)
    {
        RequireWithinRangeInclusive(scale, 0, MaxScale, "Scale must be within the inclusive range of 0 to 28.", nameof(scale));

        // Determine maximum representable scale
        BigInteger unscaledValue = value.GetUnscaledValue();
        BigInteger absUnscaled = BigInteger.Abs(unscaledValue);
        BigInteger factorCurrent = BigInteger.Pow(10, value.Scale);
        BigInteger integerPart = BigInteger.DivRem(absUnscaled, factorCurrent, out _);
        int integerDigits = integerPart.IsZero ? 1 : integerPart.ToString().Length;
        int maxPossibleScale = MaxScale - integerDigits;

        Require(scale <= maxPossibleScale, $"Maximum possible scale for the specified value is {maxPossibleScale}.", nameof(scale));

        // No change needed
        if (value.Scale == scale)
            return value;

        // Increase scale: pad with zeros
        if (value.Scale < scale)
        {
            int diff = scale - value.Scale;
            BigInteger padded = unscaledValue * BigInteger.Pow(10, diff);
            return padded.ToDecimal(scale);
        }

        // Decrease scale: drop or round extra digits
        int drop = value.Scale - scale;
        BigInteger divisor = BigInteger.Pow(10, drop);
        BigInteger.DivRem(unscaledValue, divisor, out BigInteger remainder);

        // If fractional remainder, then rounding required
        if (remainder != 0)
            return decimal.Round(value, scale, mode);

        // No rounding required
        BigInteger quotient = unscaledValue / divisor;
        return quotient.ToDecimal(scale);
    }

    /// <summary>
    /// Gets the current value as an unscaled integer.
    /// </summary>
    /// <param name="value">The value to get as an unscaled integer.</param>
    /// <param name="scale">The desired scale of the result.</param>
    /// <param name="mode">The scale mode of the desired result.</param>
    /// <typeparam name="T">The underlying <see cref="IBinaryInteger{TSelf}"/> type.</typeparam>
    /// <returns>Returns an unscaled integer representation of the current value.</returns>
    private static BigInteger GetUnscaledInteger<T>(this T value, int scale, ScaleMode mode) where T : IBinaryInteger<T>
    {
        Require(scale >= 0, "Scale must be greater than or equal to zero.", nameof(value));
        RequireIsDefined(mode, nameof(mode));

        BigInteger integer = value.ToBigInteger();
        return scale == 0 || mode == ScaleMode.Fractional ? integer : integer * BigInteger.Pow(10, scale);
    }

    /// <summary>
    /// Determines whether the current value is inclusively between the specified minimum and maximum values.
    /// </summary>
    /// <param name="value">The value to check.</param>
    /// <param name="minimum">The inclusive minimum value.</param>
    /// <param name="maximum">The inclusive maximum value.</param>
    /// <typeparam name="T">The underlying <see cref="INumber{TSelf}"/> type.</typeparam>
    /// <returns>Returns <see langword="true"/> if the current value is inclusively between the specified minimum and maximum values; otherwise, <see langword="false"/>.</returns>
    public static bool IsBetween<T>(this T value, T minimum, T maximum) where T : INumber<T> => value >= minimum && value <= maximum;

    /// <summary>
    /// Converts the current <see cref="IBinaryInteger{TSelf}"/> value to a <see cref="BigDecimal"/> value.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <param name="scale">The scale of the <see cref="BigDecimal"/> value.</param>
    /// <param name="mode">The scale mode that determines how the current value should be scaled.</param>
    /// <typeparam name="T">The underlying <see cref="IBinaryInteger{TSelf}"/> type of the value to convert.</typeparam>
    /// <returns>Returns a <see cref="BigDecimal"/> representing the current value.</returns>
    public static BigDecimal ToBigDecimal<T>(this T value, int scale = 0, ScaleMode mode = ScaleMode.Integral) where T : IBinaryInteger<T> =>
        new(value.ToBigInteger(), scale, mode);

    /// <summary>
    /// Converts the current <see cref="float"/> value to a <see cref="BigDecimal"/>.
    /// </summary>
    /// <param name="value">The <see cref="float"/> value to convert.</param>
    /// <param name="mode">The mode that specifies whether the <see cref="BigDecimal"/> value should be converted using its binary or decimal representation.</param>
    /// <returns>Returns a new <see cref="BigDecimal"/> representing the current <see cref="float"/> value.</returns>
    public static BigDecimal ToBigDecimal(this float value, ConversionMode mode = default) => new(value, mode);

    /// <summary>
    /// Converts the current <see cref="double"/> value to a <see cref="BigDecimal"/>.
    /// </summary>
    /// <param name="value">The <see cref="double"/> value to convert.</param>
    /// <param name="mode">The mode that specifies whether the <see cref="BigDecimal"/> value should be converted using its binary or decimal representation.</param>
    /// <returns>Returns a new <see cref="BigDecimal"/> representing the current <see cref="double"/> value.</returns>
    public static BigDecimal ToBigDecimal(this double value, ConversionMode mode = default) => new(value, mode);

    /// <summary>
    /// Converts the current <see cref="decimal"/> value to a <see cref="BigDecimal"/>.
    /// </summary>
    /// <param name="value">The <see cref="decimal"/> value to convert.</param>
    /// <returns>Returns a new <see cref="BigDecimal"/> representing the current <see cref="decimal"/> value.</returns>
    public static BigDecimal ToBigDecimal(this decimal value) => new(value);

    /// <summary>
    /// Converts the current <see cref="IBinaryInteger{TSelf}"/> value to a <see cref="BigInteger"/> value.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <typeparam name="T">The underlying <see cref="IBinaryInteger{TSelf}"/> type of the value to convert.</typeparam>
    /// <returns>Returns a <see cref="BigInteger"/> representing the current value.</returns>
    public static BigInteger ToBigInteger<T>(this T value) where T : IBinaryInteger<T> => BigInteger.CreateChecked(value);

    /// <summary>
    /// Converts the current value to a <see cref="decimal"/>.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <param name="scale">The scale of the <see cref="decimal"/> value.</param>
    /// <param name="mode">The scale mode that determines how the current value should be scaled.</param>
    /// <typeparam name="T">The underlying <see cref="IBinaryInteger{TSelf}"/> type of the value to convert.</typeparam>
    /// <returns>Returns a new <see cref="decimal"/> representing the current value.</returns>
    public static decimal ToDecimal<T>(this T value, int scale = 0, ScaleMode mode = default) where T : IBinaryInteger<T>
    {
        Require(scale.IsBetween(0, 28), "Scale must be between 0 and 28.");
        RequireIsDefined(mode, nameof(mode));

        BigInteger scaled = value.GetUnscaledInteger(scale, mode);
        Check(scaled.IsBetween(MinDecimal, MaxDecimal), $"Value is either too large or too small to convert to {nameof(Decimal)}.");

        Int128 integer = Int128.Abs((Int128)scaled);
        int lo = (int)integer;
        int mid = (int)(integer >> 32);
        int hi = (int)(integer >> 64);

        return new decimal(lo, mid, hi, T.IsNegative(value), (byte)scale);
    }

    /// <summary>
    /// Converts the current <see cref="IBinaryInteger{TSelf}"/> value to a <see cref="NumberInfo"/> value.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <param name="scale">The desired scale of the specified value.</param>
    /// <param name="mode">The scale mode that determines how the specified value should be scaled.</param>
    /// <typeparam name="T">The underlying <see cref="IBinaryInteger{TSelf}"/> type.</typeparam>
    /// <returns>Returns a <see cref="NumberInfo"/> representing the current value.</returns>
    public static NumberInfo ToNumberInfo<T>(this T value, int scale = 0, ScaleMode mode = default) where T : IBinaryInteger<T>
    {
        Require(scale >= 0, "Scale must be greater than or equal to zero", nameof(scale));
        RequireIsDefined(mode, nameof(mode));
        BigInteger unscaledValue = value.GetUnscaledInteger(scale, mode);
        return new NumberInfo(unscaledValue, scale);
    }

    /// <summary>
    /// Converts the current <see cref="float"/> value to a <see cref="NumberInfo"/> value.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <param name="mode">The conversion mode that determines whether the current <see cref="float"/> value should be converted from its binary or decimal representation.</param>
    /// <returns>Returns a <see cref="NumberInfo"/> representing the current value.</returns>
    public static NumberInfo ToNumberInfo(this float value, ConversionMode mode = default)
    {
        RequireIsDefined(mode, nameof(mode));
        return Ieee754Converter.Convert(value, mode);
    }

    /// <summary>
    /// Converts the current <see cref="double"/> value to a <see cref="NumberInfo"/> value.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <param name="mode">The conversion mode that determines whether the current <see cref="double"/> value should be converted from its binary or decimal representation.</param>
    /// <returns>Returns a <see cref="NumberInfo"/> representing the current value.</returns>
    public static NumberInfo ToNumberInfo(this double value, ConversionMode mode = default)
    {
        RequireIsDefined(mode, nameof(mode));
        return Ieee754Converter.Convert(value, mode);
    }

    /// <summary>
    /// Converts the current <see cref="decimal"/> value to a <see cref="NumberInfo"/> value.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns a <see cref="NumberInfo"/> representing the current value.</returns>
    public static NumberInfo ToNumberInfo(this decimal value) => new(value.GetUnscaledValue(), value.Scale);
}
