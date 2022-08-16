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

using System.Numerics;

namespace OnixLabs.Core.Numerics;

public readonly partial struct BigDecimal
{
    /// <summary>
    /// Gets the quotient of the specified <see cref="BigDecimal"/> values.
    /// </summary>
    /// <param name="left">The left-hand value to divide.</param>
    /// <param name="right">The right-hand value to divide by.</param>
    /// <returns>Returns the quotient of the specified <see cref="BigDecimal"/> values.</returns>
    public static BigDecimal operator /(BigDecimal left, BigDecimal right)
    {
        return Divide(left, right);
    }

    /// <summary>
    /// Gets the quotient of the specified <see cref="BigDecimal"/> and <see cref="float"/> values.
    /// </summary>
    /// <param name="left">The left-hand value to divide.</param>
    /// <param name="right">The right-hand value to divide by.</param>
    /// <returns>Returns the quotient of the specified <see cref="BigDecimal"/> and <see cref="float"/> values.</returns>
    public static BigDecimal operator /(BigDecimal left, float right)
    {
        return Divide(left, right);
    }

    /// <summary>
    /// Gets the quotient of the specified <see cref="BigDecimal"/> and <see cref="double"/> values.
    /// </summary>
    /// <param name="left">The left-hand value to divide.</param>
    /// <param name="right">The right-hand value to divide by.</param>
    /// <returns>Returns the quotient of the specified <see cref="BigDecimal"/> and <see cref="double"/> values.</returns>
    public static BigDecimal operator /(BigDecimal left, double right)
    {
        return Divide(left, right);
    }

    /// <summary>
    /// Gets the quotient of the specified <see cref="BigDecimal"/> and <see cref="decimal"/> values.
    /// </summary>
    /// <param name="left">The left-hand value to divide.</param>
    /// <param name="right">The right-hand value to divide by.</param>
    /// <returns>Returns the quotient of the specified <see cref="BigDecimal"/> and <see cref="decimal"/> values.</returns>
    public static BigDecimal operator /(BigDecimal left, decimal right)
    {
        return Divide(left, right);
    }

    /// <summary>
    /// Gets the quotient of the specified <see cref="float"/> and <see cref="BigDecimal"/> values.
    /// </summary>
    /// <param name="left">The left-hand value to divide.</param>
    /// <param name="right">The right-hand value to divide by.</param>
    /// <returns>Returns the quotient of the specified <see cref="float"/> and <see cref="BigDecimal"/> values.</returns>
    public static BigDecimal operator /(float left, BigDecimal right)
    {
        return Divide(left, right);
    }

    /// <summary>
    /// Gets the quotient of the specified <see cref="double"/> and <see cref="BigDecimal"/> values.
    /// </summary>
    /// <param name="left">The left-hand value to divide.</param>
    /// <param name="right">The right-hand value to divide by.</param>
    /// <returns>Returns the quotient of the specified <see cref="double"/> and <see cref="BigDecimal"/> values.</returns>
    public static BigDecimal operator /(double left, BigDecimal right)
    {
        return Divide(left, right);
    }

    /// <summary>
    /// Gets the quotient of the specified <see cref="decimal"/> and <see cref="BigDecimal"/> values.
    /// </summary>
    /// <param name="left">The left-hand value to divide.</param>
    /// <param name="right">The right-hand value to divide by.</param>
    /// <returns>Returns the quotient of the specified <see cref="decimal"/> and <see cref="BigDecimal"/> values.</returns>
    public static BigDecimal operator /(decimal left, BigDecimal right)
    {
        return Divide(left, right);
    }

    /// <summary>
    /// Gets the quotient of the specified <see cref="BigDecimal"/> values.
    /// </summary>
    /// <param name="left">The left-hand value to divide.</param>
    /// <param name="right">The right-hand value to divide by.</param>
    /// <returns>Returns the quotient of the specified <see cref="BigDecimal"/> values.</returns>
    public static BigDecimal Divide(BigDecimal left, BigDecimal right)
    {
        (BigInteger leftBalanced, BigInteger rightBalanced) = BalanceUnscaled(left, right);
        BigInteger quotient = BigInteger.DivRem(leftBalanced, rightBalanced, out BigInteger remainder);

        for (int index = 0; index < left.Scale; index++)
        {
            remainder *= 10;
            quotient = quotient * 10 + BigInteger.DivRem(remainder, rightBalanced, out remainder);
        }

        return new BigDecimal(quotient, left.Scale);

        /*
         * The following code has been commented out and left here deliberately!
         * It requires further investigation and performance testing to determine
         * whether it may prove to be a better division algorithm.
         */

        // (BigInteger min, BigInteger max) = MinMax(left.Magnitude, right.Magnitude);
        // BigInteger magnitude = left > right ? min : max;
        // BigInteger leftUnscaled = left.UnscaledValue * magnitude;
        // BigInteger rightUnscaled = right.UnscaledValue;
        // BigInteger unscaledValue = leftUnscaled / rightUnscaled;
        //
        // return new BigDecimal(unscaledValue, left.Scale);
    }

    /// <summary>
    /// Gets the quotient of the specified <see cref="BigDecimal"/> and <see cref="float"/> values.
    /// </summary>
    /// <param name="left">The left-hand value to divide.</param>
    /// <param name="right">The right-hand value to divide by.</param>
    /// <returns>Returns the quotient of the specified <see cref="BigDecimal"/> and <see cref="float"/> values.</returns>
    public static BigDecimal Divide(BigDecimal left, float right)
    {
        return Divide(left, right.ToBigDecimal());
    }

    /// <summary>
    /// Gets the quotient of the specified <see cref="BigDecimal"/> and <see cref="double"/> values.
    /// </summary>
    /// <param name="left">The left-hand value to divide.</param>
    /// <param name="right">The right-hand value to divide by.</param>
    /// <returns>Returns the quotient of the specified <see cref="BigDecimal"/> and <see cref="double"/> values.</returns>
    public static BigDecimal Divide(BigDecimal left, double right)
    {
        return Divide(left, right.ToBigDecimal());
    }

    /// <summary>
    /// Gets the quotient of the specified <see cref="BigDecimal"/> and <see cref="decimal"/> values.
    /// </summary>
    /// <param name="left">The left-hand value to divide.</param>
    /// <param name="right">The right-hand value to divide by.</param>
    /// <returns>Returns the quotient of the specified <see cref="BigDecimal"/> and <see cref="decimal"/> values.</returns>
    public static BigDecimal Divide(BigDecimal left, decimal right)
    {
        return Divide(left, right.ToBigDecimal());
    }

    /// <summary>
    /// Gets the quotient of the specified <see cref="float"/> and <see cref="BigDecimal"/> values.
    /// </summary>
    /// <param name="left">The left-hand value to divide.</param>
    /// <param name="right">The right-hand value to divide by.</param>
    /// <returns>Returns the quotient of the specified <see cref="float"/> and <see cref="BigDecimal"/> values.</returns>
    public static BigDecimal Divide(float left, BigDecimal right)
    {
        return Divide(left.ToBigDecimal(), right);
    }

    /// <summary>
    /// Gets the quotient of the specified <see cref="double"/> and <see cref="BigDecimal"/> values.
    /// </summary>
    /// <param name="left">The left-hand value to divide.</param>
    /// <param name="right">The right-hand value to divide by.</param>
    /// <returns>Returns the quotient of the specified <see cref="double"/> and <see cref="BigDecimal"/> values.</returns>
    public static BigDecimal Divide(double left, BigDecimal right)
    {
        return Divide(left.ToBigDecimal(), right);
    }

    /// <summary>
    /// Gets the quotient of the specified <see cref="decimal"/> and <see cref="BigDecimal"/> values.
    /// </summary>
    /// <param name="left">The left-hand value to divide.</param>
    /// <param name="right">The right-hand value to divide by.</param>
    /// <returns>Returns the quotient of the specified <see cref="decimal"/> and <see cref="BigDecimal"/> values.</returns>
    public static BigDecimal Divide(decimal left, BigDecimal right)
    {
        return Divide(left.ToBigDecimal(), right);
    }
}
