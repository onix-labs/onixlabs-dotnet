// Copyright 2020-2023 ONIXLabs
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
        BigInteger magnitude = BigInteger.Min(left.ScaleFactor, right.ScaleFactor);
        BigInteger leftResult = left.UnscaledValue * right.ScaleFactor / magnitude;
        BigInteger rightResult = right.UnscaledValue * left.ScaleFactor / magnitude;

        return (leftResult, rightResult);
    }

    /// <summary>
    /// Raises the specified <see cref="BigDecimal"/> value by the specified exponent power.
    /// </summary>
    /// <param name="value">The <see cref="BigDecimal"/> value to raise to the exponent power.</param>
    /// <param name="exponent">The exponent to raise by.</param>
    /// <param name="additionalScale">
    /// Provides additional scale digits for values that are raised by a negative exponent.
    /// By default, negative exponents will only scale to the first non-zero significant digit to the right of the decimal point.
    /// The default value for this parameter is zero (0) meaning that no additional scale digits will be applied.
    /// </param>
    /// <returns>Returns a <see cref="BigDecimal"/> result of raising the value by the specified exponent power.</returns>
    public static BigDecimal Pow(BigDecimal value, int exponent, int additionalScale = 0)
    {
        if (exponent is 0)
        {
            return One;
        }

        if (exponent is 1)
        {
            return value;
        }

        BigDecimal result = value;
        int abs = int.Abs(exponent);
        int count = abs;

        while (--count > 0)
        {
            result *= value;
        }

        if (exponent > 0)
        {
            return result;
        }

        int scale = result.UnscaledValue.GetDigitLength() + additionalScale;
        BigDecimal n = BigInteger.Pow(10, scale).ToBigDecimal(scale);

        result = n / result;

        return value > 0 && value < 1 ? result.IntegralValue : result;
    }

    /// <summary>
    /// Applies the specified scale to the specified <see cref="BigDecimal"/> value.
    /// </summary>
    /// <param name="value">The <see cref="BigDecimal"/> value for which to apply the specified scale.</param>
    /// <param name="scale">The scale value to apply to the new <see cref="BigDecimal"/> value.</param>
    /// <returns>Returns a new <see cref="BigDecimal"/> value with the specified scale.</returns>
    public static BigDecimal SetScale(BigDecimal value, int scale)
    {
        return new BigDecimal(value.UnscaledValue, scale);
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
