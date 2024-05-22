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
    /// <param name="left">The left-hand value to divide.</param>
    /// <param name="right">The right-hand value to divide by.</param>
    /// <param name="mode">The rounding strategy to use. The default value is <see cref="MidpointRounding.ToEven"/>.</param>
    /// <returns>Returns the quotient of the specified <see cref="BigDecimal"/> values.</returns>
    public static BigDecimal Divide(BigDecimal left, BigDecimal right, MidpointRounding mode = default)
    {
        RequireIsDefined(mode, nameof(mode));

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
    /// <param name="left">The left-hand value to divide.</param>
    /// <param name="right">The right-hand value to divide by.</param>
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
        // 1. Obtain the quotient and remainder by dividing the dividend by the divisor.
        (BigInteger quotient, BigInteger remainder) = BigInteger.DivRem(dividend, divisor);

        // 2. Obtain the unit value of the quotient as this is required to accurately round towards an even number.
        int unit = (int)(quotient % 10);

        // 3. Obtain the remainder with 10 digits of precision, which is required for accurate rounding.
        remainder = remainder * RoundingMagnitude / divisor;

        // 4. Convert the remainder to a double with 10 digits of fractional precision and add the unit.
        double valueToRound = (double)remainder / RoundingMagnitude + unit;

        // 5. Round until zero fractional digits remain and subtract the unit; yields 1 or 0.
        int rounded = (int)Math.Round(valueToRound, mode) - unit;

        return quotient + rounded;
    }
}
