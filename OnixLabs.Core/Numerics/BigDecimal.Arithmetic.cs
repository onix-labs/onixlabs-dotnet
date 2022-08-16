// Copyright 2020-2022 ONIXLabs
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
using System.Numerics;
using static OnixLabs.Core.Preconditions;

namespace OnixLabs.Core.Numerics;

public readonly partial struct BigDecimal
{
    /// <summary>
    /// Gets the absolute value of a <see cref="BigDecimal"/> object.
    /// </summary>
    /// <param name="value">The <see cref="BigDecimal"/> from which to obtain an absolute value.</param>
    /// <returns>Returns the absolute value of a <see cref="BigDecimal"/> object.</returns>
    public static BigDecimal Abs(BigDecimal value)
    {
        return new BigDecimal(BigInteger.Abs(value.UnscaledValue), value.Scale);
    }

    /// <summary>
    /// Balances the scale of the specified <see cref="BigDecimal"/> values.
    /// </summary>
    /// <param name="left">The left value to balance.</param>
    /// <param name="right">The right value to balance.</param>
    /// <returns>Returns the left and right balanced <see cref="BigDecimal"/> values.</returns>
    public static (BigDecimal left, BigDecimal right) Balance(BigDecimal left, BigDecimal right)
    {
        int scale = Math.Max(left.Scale, right.Scale);
        (BigInteger leftUnscaled, BigInteger rightUnscaled) = BalanceUnscaled(left, right);
        BigDecimal leftResult = new(leftUnscaled, scale);
        BigDecimal rightResult = new(rightUnscaled, scale);

        return (leftResult, rightResult);
    }

    /// <summary>
    /// Balances the scale of the specified <see cref="BigDecimal"/> values and obtains their unscaled values.
    /// </summary>
    /// <param name="left">The left value to balance.</param>
    /// <param name="right">The right value to balance.</param>
    /// <returns>Returns the left and right balanced, unscaled <see cref="BigInteger"/> values.</returns>
    public static (BigInteger left, BigInteger right) BalanceUnscaled(BigDecimal left, BigDecimal right)
    {
        BigInteger magnitude = BigInteger.Min(left.Magnitude, right.Magnitude);
        BigInteger leftResult = left.UnscaledValue * right.Magnitude / magnitude;
        BigInteger rightResult = right.UnscaledValue * left.Magnitude / magnitude;

        return (leftResult, rightResult);
    }

    /// <summary>
    /// Gets the smallest and the largest of the specified <see cref="BigDecimal"/> values.
    /// </summary>
    /// <param name="left">The left value to compare.</param>
    /// <param name="right">The right value to compare.</param>
    /// <returns>Returns the smallest and the largest of the specified <see cref="BigDecimal"/> values.</returns>
    public static (BigDecimal min, BigDecimal max) MinMax(BigDecimal left, BigDecimal right)
    {
        return left < right ? (left, right) : (right, left);
    }

    /// <summary>
    /// Gets the smallest and the largest of the specified <see cref="BigInteger"/> values.
    /// </summary>
    /// <param name="left">The left value to compare.</param>
    /// <param name="right">The right value to compare.</param>
    /// <returns>Returns the smallest and the largest of the specified <see cref="BigInteger"/> values.</returns>
    private static (BigInteger min, BigInteger max) MinMax(BigInteger left, BigInteger right)
    {
        return left < right ? (left, right) : (right, left);
    }

    /// <summary>
    /// Gets the smallest of the specified <see cref="BigDecimal"/> values.
    /// </summary>
    /// <param name="left">The left value to compare.</param>
    /// <param name="right">The right value to compare.</param>
    /// <returns>Returns the smallest of the specified <see cref="BigDecimal"/> values.</returns>
    public static BigDecimal Min(BigDecimal left, BigDecimal right)
    {
        return MinMax(left, right).min;
    }

    /// <summary>
    /// Gets the largest of the specified <see cref="BigDecimal"/> values.
    /// </summary>
    /// <param name="left">The left value to compare.</param>
    /// <param name="right">The right value to compare.</param>
    /// <returns>Returns the largest of the specified <see cref="BigDecimal"/> values.</returns>
    public static BigDecimal Max(BigDecimal left, BigDecimal right)
    {
        return MinMax(left, right).max;
    }

    /// <summary>
    /// Negates the specified <see cref="BigDecimal"/> value.
    /// </summary>
    /// <param name="value">The value to negate.</param>
    /// <returns>The result of the value parameter multiplied by negative one (-1).</returns>
    public static BigDecimal Negate(BigDecimal value)
    {
        BigInteger unscaledValue = BigInteger.Negate(value.UnscaledValue);
        return new BigDecimal(unscaledValue, value.Scale);
    }

    /// <summary>
    /// Rounds the specified <see cref="BigDecimal"/> value to the nearest integer, or to the specified scale.
    /// </summary>
    /// <param name="value">The value to be rounded.</param>
    /// <param name="scale">The number of decimal places to round by. The default value is zero.</param>
    /// <param name="rounding">The rounding strategy to use. The default value is <see cref="MidpointRounding.ToEven"/>.</param>
    /// <returns>
    /// Returns the specified <see cref="BigDecimal"/> rounded to the specified number of fractional digits. 
    /// If the scale of the specified value is less than the specified scale to round by, the value is returned unchanged.
    /// </returns>
    /// <exception cref="ArgumentException">If the specified scale is non-negative.</exception>
    /// <exception cref="ArgumentException">If the specified rounding mode is invalid.</exception>
    public static BigDecimal Round(BigDecimal value, int scale = 0, MidpointRounding rounding = MidpointRounding.ToEven)
    {
        Check(scale >= 0, "BigDecimal scale cannot be non-negative.");

        if (value.Scale <= scale)
        {
            return value;
        }

        BigInteger divisor = value.Magnitude / BigInteger.Pow(Ten.UnscaledValue, scale);
        BigInteger quotient = BigInteger.DivRem(BigInteger.Abs(value.UnscaledValue), divisor, out BigInteger remainder);
        BigInteger midpoint = BigInteger.Pow(Ten.UnscaledValue, value.Scale - scale - 1) * 5;
        BigInteger sign = value.Sign is 1 ? BigInteger.One : BigInteger.MinusOne;

        bool isAddOneRequired = rounding switch
        {
            MidpointRounding.ToZero => false,
            MidpointRounding.AwayFromZero => remainder >= midpoint,
            MidpointRounding.ToPositiveInfinity => remainder > 0 && sign > 0,
            MidpointRounding.ToNegativeInfinity => remainder > 0 && sign < 0,
            MidpointRounding.ToEven => remainder > midpoint || remainder == midpoint && !quotient.IsEven,
            _ => throw new ArgumentException($"The specified value is not a valid MidpointRounding mode: {rounding}", nameof(rounding))
        };

        return isAddOneRequired ? new BigDecimal((quotient + BigInteger.One) * sign, scale) : new BigDecimal(quotient * sign, scale);
    }

    /// <summary>
    /// Rounds the specified <see cref="BigDecimal"/> value up to the smallest integral value greater than or equal to the specified number.
    /// This kind of rounding is sometimes called rounding towards positive infinity, following IEEE Standard 754, section 4.
    /// </summary>
    /// <param name="value">The value to be rounded up towards positive infinity.</param>
    /// <returns>Returns the smallest integral value that is greater than or equal to the specified <see cref="BigDecimal"/> value.</returns>
    public static BigDecimal Ceiling(BigDecimal value)
    {
        return Round(value, 0, MidpointRounding.ToPositiveInfinity);
    }

    /// <summary>
    /// Rounds the specified <see cref="BigDecimal"/> value down to the largest integral value less than or equal to the specified number.
    /// This kind of rounding is sometimes called rounding towards negative infinity, following IEEE Standard 754, section 4.
    /// </summary>
    /// <param name="value">The value to be rounded down towards positive infinity.</param>
    /// <returns>Returns the largest integral value that is less than or equal to the specified <see cref="BigDecimal"/> value.</returns>
    public static BigDecimal Floor(BigDecimal value)
    {
        return Round(value, 0, MidpointRounding.ToNegativeInfinity);
    }

    /// <summary>
    /// Truncates the fractional part of the specified <see cref="BigDecimal"/> value, leaving only the integral component.
    /// </summary>
    /// <param name="value">The value to truncate.</param>
    /// <returns>Returns the fractional part of the specified <see cref="BigDecimal"/> value, leaving only the integral component.</returns>
    public static BigDecimal Truncate(BigDecimal value)
    {
        return new BigDecimal(value.IntegralValue, 0);
    }
}
