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

public readonly partial struct Float128
{
    /// <summary>
    /// The magnitude below which <see cref="Atan(Float128)"/> evaluates its Taylor series directly without further argument reduction.
    /// </summary>
    private static readonly Float128 AtanReductionThreshold = Parse("0.0625", CultureInfo.InvariantCulture);

    /// <summary>
    /// Computes the angle whose sine is the specified <see cref="Float128"/> value.
    /// </summary>
    /// <param name="value">The value whose arcsine is to be computed, in the closed interval <c>[-1, 1]</c>.</param>
    /// <returns>Returns the angle, in radians, whose sine is <paramref name="value"/>; NaN for NaN or values outside <c>[-1, 1]</c>; preserves the sign of zero.</returns>
    public static Float128 Asin(Float128 value)
    {
        if (IsNaN(value)) return value;
        if (IsZero(value)) return value;

        Float128 absValue = Abs(value);
        if (absValue > One) return NaN;
        if (absValue == One) return IsNegative(value) ? -PiOver2 : PiOver2;

        Float128 denominator = Sqrt(One - value * value);
        return Atan(value / denominator);
    }

    /// <summary>
    /// Computes the angle whose cosine is the specified <see cref="Float128"/> value.
    /// </summary>
    /// <param name="value">The value whose arccosine is to be computed, in the closed interval <c>[-1, 1]</c>.</param>
    /// <returns>Returns the angle, in radians, whose cosine is <paramref name="value"/>; NaN for NaN or values outside <c>[-1, 1]</c>.</returns>
    public static Float128 Acos(Float128 value)
    {
        if (IsNaN(value)) return value;
        if (value == One) return Zero;
        if (value == NegativeOne) return Pi;

        Float128 absValue = Abs(value);
        if (absValue > One) return NaN;

        return PiOver2 - Asin(value);
    }

    /// <summary>
    /// Computes the angle whose tangent is the specified <see cref="Float128"/> value.
    /// </summary>
    /// <param name="value">The value whose arctangent is to be computed.</param>
    /// <returns>Returns the angle, in radians, whose tangent is <paramref name="value"/>; preserves the sign of zero; ±π/2 for ±infinity.</returns>
    public static Float128 Atan(Float128 value)
    {
        if (IsNaN(value)) return value;
        if (IsZero(value)) return value;
        if (IsPositiveInfinity(value)) return PiOver2;
        if (IsNegativeInfinity(value)) return -PiOver2;

        bool negative = IsNegative(value);
        Float128 absValue = Abs(value);
        bool inverted = absValue > One;
        Float128 reduced = inverted ? One / absValue : absValue;

        int doublings = 0;
        while (reduced > AtanReductionThreshold)
        {
            Float128 oneePlusSquared = One + reduced * reduced;
            reduced = reduced / (One + Sqrt(oneePlusSquared));
            doublings++;
        }

        Float128 result = ScaleB(AtanSeries(reduced), doublings);

        if (inverted) result = PiOver2 - result;
        if (negative) result = -result;

        return result;
    }

    /// <summary>
    /// Computes the angle whose tangent is the quotient of the specified <see cref="Float128"/> values, accounting for the quadrant of <c>(x, y)</c>.
    /// </summary>
    /// <param name="y">The numerator of the quotient.</param>
    /// <param name="x">The denominator of the quotient.</param>
    /// <returns>Returns the angle, in radians, whose tangent is <c>y / x</c>; resolves all four quadrants.</returns>
    public static Float128 Atan2(Float128 y, Float128 x)
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

        Float128 base_atan = Atan(y / x);
        if (IsPositive(x)) return base_atan;
        return yNegative ? base_atan - Pi : base_atan + Pi;
    }

    /// <summary>
    /// Computes the angle whose sine is the specified <see cref="Float128"/> value, divided by π.
    /// </summary>
    /// <param name="value">The value whose arcsine is to be computed, in the closed interval <c>[-1, 1]</c>.</param>
    /// <returns>Returns <c>Asin(value) / π</c>; NaN for NaN or values outside <c>[-1, 1]</c>; preserves the sign of zero.</returns>
    public static Float128 AsinPi(Float128 value)
    {
        if (IsNaN(value)) return value;
        if (IsZero(value)) return value;

        return Asin(value) / Pi;
    }

    /// <summary>
    /// Computes the angle whose cosine is the specified <see cref="Float128"/> value, divided by π.
    /// </summary>
    /// <param name="value">The value whose arccosine is to be computed, in the closed interval <c>[-1, 1]</c>.</param>
    /// <returns>Returns <c>Acos(value) / π</c>; NaN for NaN or values outside <c>[-1, 1]</c>.</returns>
    public static Float128 AcosPi(Float128 value)
    {
        if (IsNaN(value)) return value;

        return Acos(value) / Pi;
    }

    /// <summary>
    /// Computes the angle whose tangent is the specified <see cref="Float128"/> value, divided by π.
    /// </summary>
    /// <param name="value">The value whose arctangent is to be computed.</param>
    /// <returns>Returns <c>Atan(value) / π</c>; preserves the sign of zero; ±½ for ±infinity.</returns>
    public static Float128 AtanPi(Float128 value)
    {
        if (IsNaN(value)) return value;
        if (IsZero(value)) return value;

        return Atan(value) / Pi;
    }

    /// <summary>
    /// Computes the angle whose tangent is the quotient of the specified <see cref="Float128"/> values, divided by π, accounting for the quadrant of <c>(x, y)</c>.
    /// </summary>
    /// <param name="y">The numerator of the quotient.</param>
    /// <param name="x">The denominator of the quotient.</param>
    /// <returns>Returns <c>Atan2(y, x) / π</c>; resolves all four quadrants.</returns>
    public static Float128 Atan2Pi(Float128 y, Float128 x) => Atan2(y, x) / Pi;

    /// <summary>
    /// Evaluates the Taylor series for <c>atan(y)</c> on the reduced argument, suitable for small magnitudes of <paramref name="y"/>.
    /// </summary>
    /// <param name="y">The reduced argument whose magnitude is small enough for rapid series convergence.</param>
    /// <returns>The <see cref="Float128"/> approximation of <c>atan(y)</c>.</returns>
    private static Float128 AtanSeries(Float128 y)
    {
        Float128 sum = y;
        Float128 term = y;
        Float128 ySquared = y * y;
        for (int k = 1; k <= 200; k++)
        {
            term = -term * ySquared;
            Float128 summand = term / (Float128)(2 * k + 1);
            Float128 previous = sum;
            sum = sum + summand;
            if (sum == previous) break;
        }
        return sum;
    }
}
