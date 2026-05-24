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
using System.Numerics;

namespace OnixLabs.Numerics;

public readonly partial struct BigDecimal
{
    /// <summary>
    /// Computes the quotient of the specified <see cref="BigDecimal"/> values.
    /// </summary>
    /// <param name="left">The <paramref name="left"/> value to divide.</param>
    /// <param name="right">The <paramref name="right"/> value to divide by.</param>
    /// <param name="mode">The rounding strategy to use. The default value is <see cref="MidpointRounding.ToEven"/>.</param>
    /// <returns>Returns the quotient of the specified <see cref="BigDecimal"/> values.</returns>
    public static BigDecimal Divide(BigDecimal left, BigDecimal right, MidpointRounding mode = default)
    {
        RequireIsDefined(mode);

        if (right == Zero) throw new DivideByZeroException();
        if (right == One) return left;
        if (left == Zero) return Zero;

        BigInteger dividend = left.UnscaledValue * BigInteger.Pow(10, right.Scale);
        BigInteger quotient = DivideAndRound(dividend, right.UnscaledValue, mode);

        return new BigDecimal(quotient, left.Scale);
    }

    /// <summary>
    /// Computes the quotient of the specified <see cref="BigDecimal"/> values.
    /// </summary>
    /// <param name="left">The <paramref name="left"/> value to divide.</param>
    /// <param name="right">The <paramref name="right"/> value to divide by.</param>
    /// <returns>Returns the quotient of the specified <see cref="BigDecimal"/> values.</returns>
    public static BigDecimal operator /(BigDecimal left, BigDecimal right) => Divide(left, right);

    /// <summary>
    /// Divides the specified dividend by the specified divisor and rounds the remainder using the specified rounding mode.
    /// </summary>
    /// <param name="dividend">The dividend value to divide.</param>
    /// <param name="divisor">The divisor to divide by.</param>
    /// <param name="mode">The rounding strategy to use. The default value is <see cref="MidpointRounding.ToEven"/>.</param>
    /// <returns>Returns the quotient of the specified <see cref="BigInteger"/> values, rounded using the specified rounding mode.</returns>
    private static BigInteger DivideAndRound(BigInteger dividend, BigInteger divisor, MidpointRounding mode)
    {
        // 1. An exact division leaves no fractional part, so there is nothing to round towards.
        (BigInteger quotient, BigInteger remainder) = BigInteger.DivRem(dividend, divisor);
        if (remainder.IsZero) return quotient;

        // 2. DivRem truncates the quotient towards zero, so rounding away from zero means stepping in the
        //    direction of the true value's sign; the truncated quotient alone cannot reveal that sign when it is
        //    zero, so it is derived from the operands instead.
        int valueSign = dividend.Sign * divisor.Sign;

        // 3. The directional modes are defined relative to the number line rather than the midpoint, so the size
        //    of the fraction is irrelevant to them, and only the sign decides the outcome.
        switch (mode)
        {
            case MidpointRounding.ToZero: return quotient;
            case MidpointRounding.ToPositiveInfinity: return valueSign > 0 ? quotient + 1 : quotient;
            case MidpointRounding.ToNegativeInfinity: return valueSign < 0 ? quotient - 1 : quotient;

            // Ignored cases — fall through to step 4.
            case MidpointRounding.ToEven or MidpointRounding.AwayFromZero:
            default: break;
        }

        // 4. The remaining modes round to the nearest integer, so the fraction must be weighed against one half.
        //    Doubling the remainder and comparing it to the divisor performs that comparison exactly in integers,
        //    avoiding the precision loss that converting the fraction to a double would introduce.
        int comparison = (BigInteger.Abs(remainder) * 2).CompareTo(BigInteger.Abs(divisor));

        // 5. Below the midpoint rounds towards zero, and above it rounds away; an exact midpoint is a tie, broken
        //    away from zero or towards the even quotient depending on the requested mode.
        if (comparison < 0) return quotient;
        if (comparison > 0 || mode is MidpointRounding.AwayFromZero) return quotient + valueSign;

        return quotient.IsEven ? quotient : quotient + valueSign;
    }
}
