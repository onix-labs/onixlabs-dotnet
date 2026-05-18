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

namespace OnixLabs.Numerics;

public readonly partial struct Float128
{
    /// <summary>
    /// Computes the sine of the specified <see cref="Float128"/> value, where the angle is expressed in radians.
    /// </summary>
    /// <param name="value">The angle, in radians, for which to compute the sine.</param>
    /// <returns>Returns the sine of <paramref name="value"/>; NaN for NaN or infinity; preserves the sign of zero.</returns>
    public static Float128 Sin(Float128 value)
    {
        if (IsNaN(value) || IsInfinity(value)) return NaN;
        if (IsZero(value)) return value;

        ReducePiOver2(value, out Float128 r, out int quadrant);
        return SinFromQuadrant(r, quadrant);
    }

    /// <summary>
    /// Computes the cosine of the specified <see cref="Float128"/> value, where the angle is expressed in radians.
    /// </summary>
    /// <param name="value">The angle, in radians, for which to compute the cosine.</param>
    /// <returns>Returns the cosine of <paramref name="value"/>; NaN for NaN or infinity; <see cref="One"/> for zero.</returns>
    public static Float128 Cos(Float128 value)
    {
        if (IsNaN(value) || IsInfinity(value)) return NaN;
        if (IsZero(value)) return One;

        ReducePiOver2(value, out Float128 r, out int quadrant);
        return CosFromQuadrant(r, quadrant);
    }

    /// <summary>
    /// Computes the tangent of the specified <see cref="Float128"/> value, where the angle is expressed in radians.
    /// </summary>
    /// <param name="value">The angle, in radians, for which to compute the tangent.</param>
    /// <returns>Returns the tangent of <paramref name="value"/>; NaN for NaN or infinity; preserves the sign of zero.</returns>
    public static Float128 Tan(Float128 value)
    {
        if (IsNaN(value) || IsInfinity(value)) return NaN;
        if (IsZero(value)) return value;

        (Float128 sin, Float128 cos) = SinCos(value);
        return sin / cos;
    }

    /// <summary>
    /// Computes the sine and cosine of the specified <see cref="Float128"/> value, where the angle is expressed in radians.
    /// </summary>
    /// <param name="value">The angle, in radians, for which to compute the sine and cosine.</param>
    /// <returns>Returns a tuple containing the sine and cosine of <paramref name="value"/>.</returns>
    public static (Float128 Sin, Float128 Cos) SinCos(Float128 value)
    {
        if (IsNaN(value) || IsInfinity(value)) return (NaN, NaN);
        if (IsZero(value)) return (value, One);

        ReducePiOver2(value, out Float128 r, out int quadrant);
        return SinCosFromQuadrant(r, quadrant);
    }

    /// <summary>
    /// Computes the sine of π multiplied by the specified <see cref="Float128"/> value.
    /// </summary>
    /// <param name="value">The angle, in half-turns, for which to compute the sine.</param>
    /// <returns>Returns the sine of <c>π · value</c>; NaN for NaN or infinity; preserves the sign of zero.</returns>
    public static Float128 SinPi(Float128 value)
    {
        if (IsNaN(value) || IsInfinity(value)) return NaN;
        if (IsZero(value)) return value;
        if (IsInteger(value)) return IsNegative(value) ? NegativeZero : Zero;

        return Sin(value * Pi);
    }

    /// <summary>
    /// Computes the cosine of π multiplied by the specified <see cref="Float128"/> value.
    /// </summary>
    /// <param name="value">The angle, in half-turns, for which to compute the cosine.</param>
    /// <returns>Returns the cosine of <c>π · value</c>; NaN for NaN or infinity; <see cref="One"/> for zero.</returns>
    public static Float128 CosPi(Float128 value)
    {
        if (IsNaN(value) || IsInfinity(value)) return NaN;
        if (IsZero(value)) return One;

        return Cos(value * Pi);
    }

    /// <summary>
    /// Computes the tangent of π multiplied by the specified <see cref="Float128"/> value.
    /// </summary>
    /// <param name="value">The angle, in half-turns, for which to compute the tangent.</param>
    /// <returns>Returns the tangent of <c>π · value</c>; NaN for NaN or infinity; preserves the sign of zero.</returns>
    public static Float128 TanPi(Float128 value)
    {
        if (IsNaN(value) || IsInfinity(value)) return NaN;
        if (IsZero(value)) return value;
        if (IsInteger(value)) return IsNegative(value) ? NegativeZero : Zero;

        return Tan(value * Pi);
    }

    /// <summary>
    /// Computes the sine and cosine of π multiplied by the specified <see cref="Float128"/> value.
    /// </summary>
    /// <param name="value">The angle, in half-turns, for which to compute the sine and cosine.</param>
    /// <returns>Returns a tuple containing the sine and cosine of <c>π · value</c>.</returns>
    public static (Float128 SinPi, Float128 CosPi) SinCosPi(Float128 value)
    {
        if (IsNaN(value) || IsInfinity(value)) return (NaN, NaN);
        if (IsZero(value)) return (value, One);

        return SinCos(value * Pi);
    }

    /// <summary>
    /// Reduces the specified angle into the interval <c>|r| ≤ π/4</c> together with the quadrant index in <c>[0, 3]</c> of the original argument.
    /// </summary>
    /// <param name="value">The angle, in radians, to reduce.</param>
    /// <param name="r">When this method returns, contains the reduced angle whose magnitude is at most <c>π/4</c>.</param>
    /// <param name="quadrant">When this method returns, contains the quadrant index in the range <c>[0, 3]</c>.</param>
    private static void ReducePiOver2(Float128 value, out Float128 r, out int quadrant)
    {
        Float128 k = Round(value * TwoOverPi);
        r = value - k * PiOver2;
        long kLong = (long)k;
        long quadrantLong = ((kLong % 4L) + 4L) % 4L;
        quadrant = (int)quadrantLong;
    }

    /// <summary>
    /// Evaluates the Taylor series for <c>sin(r)</c> on the reduced argument <c>|r| ≤ π/4</c>.
    /// </summary>
    /// <param name="r">The reduced argument whose magnitude is at most <c>π/4</c>.</param>
    /// <returns>The <see cref="Float128"/> approximation of <c>sin(r)</c>.</returns>
    private static Float128 SinSeries(Float128 r)
    {
        Float128 sum = r;
        Float128 term = r;
        Float128 rSquared = r * r;
        for (int k = 1; k <= 80; k++)
        {
            int factor = (2 * k) * (2 * k + 1);
            term = -term * rSquared / (Float128)factor;
            Float128 previous = sum;
            sum = sum + term;
            if (sum == previous) break;
        }
        return sum;
    }

    /// <summary>
    /// Evaluates the Taylor series for <c>cos(r)</c> on the reduced argument <c>|r| ≤ π/4</c>.
    /// </summary>
    /// <param name="r">The reduced argument whose magnitude is at most <c>π/4</c>.</param>
    /// <returns>The <see cref="Float128"/> approximation of <c>cos(r)</c>.</returns>
    private static Float128 CosSeries(Float128 r)
    {
        Float128 sum = One;
        Float128 term = One;
        Float128 rSquared = r * r;
        for (int k = 1; k <= 80; k++)
        {
            int factor = (2 * k - 1) * (2 * k);
            term = -term * rSquared / (Float128)factor;
            Float128 previous = sum;
            sum = sum + term;
            if (sum == previous) break;
        }
        return sum;
    }

    /// <summary>
    /// Recovers <c>sin(value)</c> from the reduced argument and quadrant index produced by <see cref="ReducePiOver2"/>.
    /// </summary>
    /// <param name="r">The reduced angle whose magnitude is at most <c>π/4</c>.</param>
    /// <param name="quadrant">The quadrant index in the range <c>[0, 3]</c>.</param>
    /// <returns>The <see cref="Float128"/> approximation of <c>sin(value)</c>.</returns>
    private static Float128 SinFromQuadrant(Float128 r, int quadrant) => quadrant switch
    {
        0 => SinSeries(r),
        1 => CosSeries(r),
        2 => -SinSeries(r),
        _ => -CosSeries(r)
    };

    /// <summary>
    /// Recovers <c>cos(value)</c> from the reduced argument and quadrant index produced by <see cref="ReducePiOver2"/>.
    /// </summary>
    /// <param name="r">The reduced angle whose magnitude is at most <c>π/4</c>.</param>
    /// <param name="quadrant">The quadrant index in the range <c>[0, 3]</c>.</param>
    /// <returns>The <see cref="Float128"/> approximation of <c>cos(value)</c>.</returns>
    private static Float128 CosFromQuadrant(Float128 r, int quadrant) => quadrant switch
    {
        0 => CosSeries(r),
        1 => -SinSeries(r),
        2 => -CosSeries(r),
        _ => SinSeries(r)
    };

    /// <summary>
    /// Recovers both <c>sin(value)</c> and <c>cos(value)</c> from the reduced argument and quadrant index produced by <see cref="ReducePiOver2"/>, sharing the underlying series evaluations.
    /// </summary>
    /// <param name="r">The reduced angle whose magnitude is at most <c>π/4</c>.</param>
    /// <param name="quadrant">The quadrant index in the range <c>[0, 3]</c>.</param>
    /// <returns>A tuple containing the <see cref="Float128"/> approximations of <c>sin(value)</c> and <c>cos(value)</c>.</returns>
    private static (Float128 Sin, Float128 Cos) SinCosFromQuadrant(Float128 r, int quadrant)
    {
        Float128 sin = SinSeries(r);
        Float128 cos = CosSeries(r);
        return quadrant switch
        {
            0 => (sin, cos),
            1 => (cos, -sin),
            2 => (-sin, -cos),
            _ => (-cos, sin)
        };
    }
}
