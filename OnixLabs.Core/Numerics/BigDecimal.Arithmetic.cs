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
    /// Gets the sum of the specified <see cref="BigDecimal"/> values.
    /// </summary>
    /// <param name="left">The left-hand value to add to.</param>
    /// <param name="right">The right-hand value to add.</param>
    /// <returns>Returns the sum of the specified <see cref="BigDecimal"/> values.</returns>
    public static BigDecimal Add(BigDecimal left, BigDecimal right)
    {
        (BigDecimal leftBalanced, BigDecimal rightBalanced) = Balance(left, right);
        BigInteger unscaledValue = leftBalanced.UnscaledValue + rightBalanced.UnscaledValue;

        return new BigDecimal(unscaledValue, leftBalanced.Scale);
    }

    /// <summary>
    /// Gets the difference between the specified <see cref="BigDecimal"/> values.
    /// </summary>
    /// <param name="left">The left-hand value to subtract from.</param>
    /// <param name="right">The right-hand value to subtract.</param>
    /// <returns>Returns the difference between the specified <see cref="BigDecimal"/> values.</returns>
    public static BigDecimal Subtract(BigDecimal left, BigDecimal right)
    {
        (BigDecimal leftBalanced, BigDecimal rightBalanced) = Balance(left, right);
        BigInteger unscaledValue = leftBalanced.UnscaledValue - rightBalanced.UnscaledValue;

        return new BigDecimal(unscaledValue, leftBalanced.Scale);
    }

    /// <summary>
    /// Gets the product of the specified <see cref="BigDecimal"/> values.
    /// </summary>
    /// <param name="left">The left-hand value to multiply by.</param>
    /// <param name="right">The right-hand value to multiply.</param>
    /// <returns>Returns the product of the specified <see cref="BigDecimal"/> values.</returns>
    public static BigDecimal Multiply(BigDecimal left, BigDecimal right)
    {
        return new BigDecimal(left.UnscaledValue * right.UnscaledValue, left.Scale + right.Scale);
    }

    /// <summary>
    /// Gets the quotient of the specified <see cref="BigDecimal"/> values.
    /// </summary>
    /// <param name="left">The left-hand value to divide.</param>
    /// <param name="right">The right-hand value to divide by.</param>
    /// <returns>Returns the quotient of the specified <see cref="BigDecimal"/> values.</returns>
    public static BigDecimal Divide(BigDecimal left, BigDecimal right)
    {
        (BigInteger min, BigInteger max) = MinMax(left.Magnitude, right.Magnitude);
        BigInteger magnitude = left > right ? min : max;

        BigInteger leftUnscaled = BigInteger.Multiply(left.UnscaledValue, magnitude);
        BigInteger rightUnscaled = right.UnscaledValue;

        BigInteger unscaledValue = BigInteger.Divide(leftUnscaled, rightUnscaled);

        return new BigDecimal(unscaledValue, left.Scale);
    }
}
