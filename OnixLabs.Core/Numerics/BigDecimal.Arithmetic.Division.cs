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
using System.Collections.Generic;
using System.Numerics;

namespace OnixLabs.Core.Numerics;

public readonly partial struct BigDecimal
{
    /// <summary>
    /// Computes the quotient of the specified <see cref="BigDecimal"/> values.
    /// </summary>
    /// <param name="left">The left-hand value to divide.</param>
    /// <param name="right">The right-hand value to divide by.</param>
    /// <param name="maxIterations">
    /// The maximum number of iterations when calculating the decimal expansion.
    /// More iterations result in higher precision, but will use significantly more memory and will perform slower.
    /// Fewer iterations result in lower precision, but will use less memory and will perform faster.
    /// The default value for this parameter is 10,000.
    /// </param>
    /// <returns>Returns the quotient of the specified <see cref="BigDecimal"/> values.</returns>
    public static BigDecimal Divide(BigDecimal left, BigDecimal right, int maxIterations = DefaultMaxScale)
    {
        if (left == Zero) return Zero;
        if (right == Zero) throw new DivideByZeroException();
        if (right == One) return left;

        (BigInteger dividend, BigInteger divisor) = NormalizeUnscaledOrderOfMagnitude(left, right);
        (BigInteger quotient, BigInteger remainder) = BigInteger.DivRem(dividend, divisor);

        HashSet<BigInteger> remainders = new();

        while (remainder > 0 && maxIterations-- > 0)
        {
            if (remainders.Contains(remainder)) break;

            remainders.Add(remainder);
            remainder *= 10;
            quotient *= 10;
            quotient += remainder / divisor;
            remainder %= divisor;
        }

        return new BigDecimal(quotient, remainders.Count);
    }

    /// <summary>
    /// Computes the quotient of the specified <see cref="BigDecimal"/> values.
    /// </summary>
    /// <param name="left">The left-hand value to divide.</param>
    /// <param name="right">The right-hand value to divide by.</param>
    /// <returns>Returns the quotient of the specified <see cref="BigDecimal"/> values.</returns>
    public static BigDecimal operator /(BigDecimal left, BigDecimal right)
    {
        return Divide(left, right);
    }

    /// <summary>
    /// Computes the quotient of the current <see cref="BigDecimal"/> value and the specified value.
    /// </summary>
    /// <param name="right">The right-hand value to divide by.</param>
    /// <param name="maxIterations">
    /// The maximum number of iterations when calculating the decimal expansion.
    /// More iterations result in higher precision, but will use significantly more memory and will perform slower.
    /// Fewer iterations result in lower precision, but will use less memory and will perform faster.
    /// The default value for this parameter is 10,000.
    /// </param>
    /// <returns>Returns the quotient of the current <see cref="BigDecimal"/> value and the specified value.</returns>
    public BigDecimal Divide(BigDecimal right, int maxIterations = DefaultMaxScale)
    {
        return Divide(this, right, maxIterations);
    }
}
