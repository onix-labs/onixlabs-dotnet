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

namespace OnixLabs.Numerics;

public readonly partial struct Float256
{
    /// <summary>
    /// The absolute argument threshold below which the <see cref="Atan"/> series converges quickly without further half-angle reduction.
    /// </summary>
    private static readonly Float256 AtanReductionThreshold = Parse("0.03125", CultureInfo.InvariantCulture);

    /// <summary>Computes the angle whose sine is the specified value.</summary>
    /// <param name="value">The value whose arcsine is to be computed.</param>
    /// <returns>Returns the angle in radians.</returns>
    public static Float256 Asin(Float256 value)
    {
        if (IsNaN(value)) return value;
        if (IsZero(value)) return value;

        Float256 absValue = Abs(value);
        if (absValue > One) return NaN;
        if (absValue == One) return IsNegative(value) ? -PiOver2 : PiOver2;

        Float256 denominator = Sqrt(One - value * value);
        return Atan(value / denominator);
    }

    /// <summary>Computes the angle whose cosine is the specified value.</summary>
    /// <param name="value">The value whose arccosine is to be computed.</param>
    /// <returns>Returns the angle in radians.</returns>
    public static Float256 Acos(Float256 value)
    {
        if (IsNaN(value)) return value;
        if (value == One) return Zero;
        if (value == NegativeOne) return Pi;

        Float256 absValue = Abs(value);
        if (absValue > One) return NaN;

        return PiOver2 - Asin(value);
    }

    /// <summary>Computes the angle whose tangent is the specified value.</summary>
    /// <param name="value">The value whose arctangent is to be computed.</param>
    /// <returns>Returns the angle in radians.</returns>
    public static Float256 Atan(Float256 value)
    {
        if (IsNaN(value)) return value;
        if (IsZero(value)) return value;
        if (IsPositiveInfinity(value)) return PiOver2;
        if (IsNegativeInfinity(value)) return -PiOver2;

        bool negative = IsNegative(value);
        Float256 absValue = Abs(value);
        bool inverted = absValue > One;
        Float256 reduced = inverted ? One / absValue : absValue;

        int doublings = 0;
        while (reduced > AtanReductionThreshold)
        {
            Float256 onePlusSquared = One + reduced * reduced;
            reduced = reduced / (One + Sqrt(onePlusSquared));
            doublings++;
        }

        Float256 result = ScaleB(AtanSeries(reduced), doublings);

        if (inverted) result = PiOver2 - result;
        if (negative) result = -result;

        return result;
    }

    /// <summary>Computes the angle whose tangent is the quotient of the specified values.</summary>
    /// <param name="y">The numerator.</param>
    /// <param name="x">The denominator.</param>
    /// <returns>Returns the angle in radians, in <c>(-π, π]</c>.</returns>
    public static Float256 Atan2(Float256 y, Float256 x)
    {
        if (IsNaN(y) || IsNaN(x)) return NaN;

        bool yNegative = IsNegative(y);

        if (IsZero(y))
        {
            if (IsPositive(x)) return y;
            return yNegative ? -Pi : Pi;
        }

        if (IsZero(x))
        {
            return yNegative ? -PiOver2 : PiOver2;
        }

        if (IsInfinity(y))
        {
            if (IsInfinity(x))
            {
                if (IsPositive(x)) return yNegative ? -PiOver4 : PiOver4;
                return yNegative ? -ThreePiOver4 : ThreePiOver4;
            }
            return yNegative ? -PiOver2 : PiOver2;
        }

        if (IsInfinity(x))
        {
            if (IsPositive(x)) return yNegative ? NegativeZero : Zero;
            return yNegative ? -Pi : Pi;
        }

        Float256 atan = Atan(y / x);
        if (IsPositive(x)) return atan;
        return yNegative ? atan - Pi : atan + Pi;
    }

    /// <summary>Computes <see cref="Asin"/> divided by π.</summary>
    /// <param name="value">The value whose arcsine is to be computed.</param>
    /// <returns>Returns <c>Asin(value) / π</c>.</returns>
    public static Float256 AsinPi(Float256 value)
    {
        if (IsNaN(value)) return value;
        if (IsZero(value)) return value;

        return Asin(value) / Pi;
    }

    /// <summary>Computes <see cref="Acos"/> divided by π.</summary>
    /// <param name="value">The value whose arccosine is to be computed.</param>
    /// <returns>Returns <c>Acos(value) / π</c>.</returns>
    public static Float256 AcosPi(Float256 value)
    {
        if (IsNaN(value)) return value;

        return Acos(value) / Pi;
    }

    /// <summary>Computes <see cref="Atan"/> divided by π.</summary>
    /// <param name="value">The value whose arctangent is to be computed.</param>
    /// <returns>Returns <c>Atan(value) / π</c>.</returns>
    public static Float256 AtanPi(Float256 value)
    {
        if (IsNaN(value)) return value;
        if (IsZero(value)) return value;

        return Atan(value) / Pi;
    }

    /// <summary>Computes <see cref="Atan2"/> divided by π.</summary>
    /// <param name="y">The numerator.</param>
    /// <param name="x">The denominator.</param>
    /// <returns>Returns <c>Atan2(y, x) / π</c>.</returns>
    public static Float256 Atan2Pi(Float256 y, Float256 x) => Atan2(y, x) / Pi;

    /// <summary>
    /// Evaluates the Maclaurin series for arctangent at the specified reduced argument <paramref name="y"/>.
    /// </summary>
    /// <param name="y">The reduced argument, assumed to be small enough for rapid series convergence.</param>
    /// <returns>Returns the arctangent of <paramref name="y"/> rounded to <see cref="Float256"/> precision.</returns>
    private static Float256 AtanSeries(Float256 y)
    {
        Float256 sum = y;
        Float256 term = y;
        Float256 ySquared = y * y;
        for (int k = 1; k <= 400; k++)
        {
            term = -term * ySquared;
            Float256 summand = term / (Float256)(2 * k + 1);
            Float256 previous = sum;
            sum = sum + summand;
            if (sum == previous) break;
        }
        return sum;
    }
}
