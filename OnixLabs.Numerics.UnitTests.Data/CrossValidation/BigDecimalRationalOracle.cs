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

namespace OnixLabs.Numerics.UnitTests.Data.CrossValidation;

/// <summary>
/// Provides an exact rational oracle for <see cref="BigDecimal"/> arithmetic. A <see cref="BigDecimal"/> represents
/// the exact value <c>UnscaledValue × 10^(-Scale)</c>, so its value is the rational <c>UnscaledValue / 10^Scale</c>.
/// Comparing results as rationals (via cross-multiplication over <see cref="BigInteger"/>) validates value equality
/// independently of scale/representation and without re-using <see cref="BigDecimal"/>'s own arithmetic.
/// </summary>
public static class BigDecimalRationalOracle
{
    /// <summary>Decomposes the specified value into its exact rational <c>(numerator, denominator)</c>, where the denominator is a power of ten.</summary>
    public static (BigInteger Numerator, BigInteger Denominator) ToRational(BigDecimal value)
        => (value.UnscaledValue, BigInteger.Pow(10, value.Scale));

    /// <summary>Returns <see langword="true"/> if <paramref name="actual"/> equals the rational <paramref name="expectedNumerator"/> / <paramref name="expectedDenominator"/> in value.</summary>
    public static bool ValueEquals(BigDecimal actual, BigInteger expectedNumerator, BigInteger expectedDenominator)
    {
        (BigInteger numerator, BigInteger denominator) = ToRational(actual);
        return numerator * expectedDenominator == expectedNumerator * denominator;
    }

    /// <summary>
    /// Rounds the exact rational <paramref name="dividend"/> / <paramref name="divisor"/> to an integer under the
    /// specified <paramref name="mode"/>. This is an independent reference for <c>BigDecimal.DivideAndRound</c>: it is
    /// expressed in terms of the mathematical floor and a non-negative remainder, whereas the production code rounds
    /// from a truncated-towards-zero quotient — so the two agree only if both are correct.
    /// </summary>
    public static BigInteger RoundedQuotient(BigInteger dividend, BigInteger divisor, MidpointRounding mode)
    {
        // Normalise so the divisor is positive; flipping both operands preserves the value of the ratio.
        if (divisor.Sign < 0)
        {
            dividend = -dividend;
            divisor = -divisor;
        }

        // Floored division: floor = ⌊dividend / divisor⌋ with remainder in [0, divisor).
        BigInteger floor = BigInteger.DivRem(dividend, divisor, out BigInteger remainder);
        if (remainder.Sign < 0)
        {
            floor -= BigInteger.One;
            remainder += divisor;
        }

        if (remainder.IsZero) return floor;

        BigInteger ceiling = floor + BigInteger.One;

        switch (mode)
        {
            case MidpointRounding.ToNegativeInfinity: return floor;
            case MidpointRounding.ToPositiveInfinity: return ceiling;
            case MidpointRounding.ToZero: return dividend.Sign > 0 ? floor : ceiling;
            default: break;
        }

        // Nearest modes: weigh the fraction (remainder / divisor) against one half by doubling the remainder.
        int comparison = (remainder * 2).CompareTo(divisor);
        if (comparison < 0) return floor;
        if (comparison > 0) return ceiling;

        // Exact midpoint tie: away from zero steps in the value's direction; to-even picks the even neighbour.
        if (mode is MidpointRounding.AwayFromZero) return dividend.Sign > 0 ? ceiling : floor;
        return floor.IsEven ? floor : ceiling;
    }
}
