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

public readonly partial struct Float256
{
    /// <summary>Computes the sine of the specified angle in radians.</summary>
    /// <param name="value">The angle, in radians.</param>
    /// <returns>Returns the sine of <paramref name="value"/>.</returns>
    public static Float256 Sin(Float256 value)
    {
        if (IsNaN(value) || IsInfinity(value)) return NaN;
        if (IsZero(value)) return value;

        ReducePiOver2(value, out Float256 r, out int quadrant);
        return SinFromQuadrant(r, quadrant);
    }

    /// <summary>Computes the cosine of the specified angle in radians.</summary>
    /// <param name="value">The angle, in radians.</param>
    /// <returns>Returns the cosine of <paramref name="value"/>.</returns>
    public static Float256 Cos(Float256 value)
    {
        if (IsNaN(value) || IsInfinity(value)) return NaN;
        if (IsZero(value)) return One;

        ReducePiOver2(value, out Float256 r, out int quadrant);
        return CosFromQuadrant(r, quadrant);
    }

    /// <summary>Computes the tangent of the specified angle in radians.</summary>
    /// <param name="value">The angle, in radians.</param>
    /// <returns>Returns the tangent of <paramref name="value"/>.</returns>
    public static Float256 Tan(Float256 value)
    {
        if (IsNaN(value) || IsInfinity(value)) return NaN;
        if (IsZero(value)) return value;

        (Float256 sin, Float256 cos) = SinCos(value);
        return sin / cos;
    }

    /// <summary>Computes the sine and cosine of the specified angle in radians.</summary>
    /// <param name="value">The angle, in radians.</param>
    /// <returns>Returns a tuple containing the sine and cosine.</returns>
    public static (Float256 Sin, Float256 Cos) SinCos(Float256 value)
    {
        if (IsNaN(value) || IsInfinity(value)) return (NaN, NaN);
        if (IsZero(value)) return (value, One);

        ReducePiOver2(value, out Float256 r, out int quadrant);
        return SinCosFromQuadrant(r, quadrant);
    }

    /// <summary>Computes the sine of π multiplied by the specified value.</summary>
    /// <param name="value">The angle, in half-turns.</param>
    /// <returns>Returns the sine of <c>π · value</c>.</returns>
    public static Float256 SinPi(Float256 value)
    {
        if (IsNaN(value) || IsInfinity(value)) return NaN;
        if (IsZero(value)) return value;
        if (IsInteger(value)) return IsNegative(value) ? NegativeZero : Zero;

        return Sin(value * Pi);
    }

    /// <summary>Computes the cosine of π multiplied by the specified value.</summary>
    /// <param name="value">The angle, in half-turns.</param>
    /// <returns>Returns the cosine of <c>π · value</c>.</returns>
    public static Float256 CosPi(Float256 value)
    {
        if (IsNaN(value) || IsInfinity(value)) return NaN;
        if (IsZero(value)) return One;

        return Cos(value * Pi);
    }

    /// <summary>Computes the tangent of π multiplied by the specified value.</summary>
    /// <param name="value">The angle, in half-turns.</param>
    /// <returns>Returns the tangent of <c>π · value</c>.</returns>
    public static Float256 TanPi(Float256 value)
    {
        if (IsNaN(value) || IsInfinity(value)) return NaN;
        if (IsZero(value)) return value;
        if (IsInteger(value)) return IsNegative(value) ? NegativeZero : Zero;

        return Tan(value * Pi);
    }

    /// <summary>Computes the sine and cosine of π multiplied by the specified value.</summary>
    /// <param name="value">The angle, in half-turns.</param>
    /// <returns>Returns a tuple containing the sine and cosine of <c>π · value</c>.</returns>
    public static (Float256 SinPi, Float256 CosPi) SinCosPi(Float256 value)
    {
        if (IsNaN(value) || IsInfinity(value)) return (NaN, NaN);
        if (IsZero(value)) return (value, One);

        return SinCos(value * Pi);
    }

    /// <summary>
    /// Reduces the specified angle modulo <c>π/2</c>, returning the reduced argument and the quadrant index in the range <c>[0, 3]</c>.
    /// </summary>
    /// <param name="value">The angle, in radians, to reduce.</param>
    /// <param name="r">When this method returns, contains the residue of <paramref name="value"/> after subtracting the nearest multiple of <c>π/2</c>.</param>
    /// <param name="quadrant">When this method returns, contains the quadrant index in the range <c>[0, 3]</c> used to recover the sign and trigonometric function.</param>
    private static void ReducePiOver2(Float256 value, out Float256 r, out int quadrant)
    {
        Float256 k = Round(value * TwoOverPi);
        r = value - k * PiOver2;
        long kLong = (long)k;
        long quadrantLong = ((kLong % 4L) + 4L) % 4L;
        quadrant = (int)quadrantLong;
    }

    /// <summary>
    /// Evaluates the Maclaurin series for sine at the specified reduced argument <paramref name="r"/>.
    /// </summary>
    /// <param name="r">The reduced argument in <c>[-π/4, π/4]</c>.</param>
    /// <returns>Returns the sine of <paramref name="r"/> rounded to <see cref="Float256"/> precision.</returns>
    private static Float256 SinSeries(Float256 r)
    {
        Float256 sum = r;
        Float256 term = r;
        Float256 rSquared = r * r;
        for (int k = 1; k <= 160; k++)
        {
            int factor = (2 * k) * (2 * k + 1);
            term = -term * rSquared / (Float256)factor;
            Float256 previous = sum;
            sum = sum + term;
            if (sum == previous) break;
        }
        return sum;
    }

    /// <summary>
    /// Evaluates the Maclaurin series for cosine at the specified reduced argument <paramref name="r"/>.
    /// </summary>
    /// <param name="r">The reduced argument in <c>[-π/4, π/4]</c>.</param>
    /// <returns>Returns the cosine of <paramref name="r"/> rounded to <see cref="Float256"/> precision.</returns>
    private static Float256 CosSeries(Float256 r)
    {
        Float256 sum = One;
        Float256 term = One;
        Float256 rSquared = r * r;
        for (int k = 1; k <= 160; k++)
        {
            int factor = (2 * k - 1) * (2 * k);
            term = -term * rSquared / (Float256)factor;
            Float256 previous = sum;
            sum = sum + term;
            if (sum == previous) break;
        }
        return sum;
    }

    /// <summary>
    /// Recovers the sine of the original angle from the reduced argument and quadrant index.
    /// </summary>
    /// <param name="r">The reduced argument produced by <see cref="ReducePiOver2"/>.</param>
    /// <param name="quadrant">The quadrant index in the range <c>[0, 3]</c>.</param>
    /// <returns>Returns the sine of the original angle.</returns>
    private static Float256 SinFromQuadrant(Float256 r, int quadrant) => quadrant switch
    {
        0 => SinSeries(r),
        1 => CosSeries(r),
        2 => -SinSeries(r),
        _ => -CosSeries(r)
    };

    /// <summary>
    /// Recovers the cosine of the original angle from the reduced argument and quadrant index.
    /// </summary>
    /// <param name="r">The reduced argument produced by <see cref="ReducePiOver2"/>.</param>
    /// <param name="quadrant">The quadrant index in the range <c>[0, 3]</c>.</param>
    /// <returns>Returns the cosine of the original angle.</returns>
    private static Float256 CosFromQuadrant(Float256 r, int quadrant) => quadrant switch
    {
        0 => CosSeries(r),
        1 => -SinSeries(r),
        2 => -CosSeries(r),
        _ => SinSeries(r)
    };

    /// <summary>
    /// Recovers both the sine and cosine of the original angle from the reduced argument and quadrant index.
    /// </summary>
    /// <param name="r">The reduced argument produced by <see cref="ReducePiOver2"/>.</param>
    /// <param name="quadrant">The quadrant index in the range <c>[0, 3]</c>.</param>
    /// <returns>Returns a tuple containing the sine and cosine of the original angle.</returns>
    private static (Float256 Sin, Float256 Cos) SinCosFromQuadrant(Float256 r, int quadrant)
    {
        Float256 sin = SinSeries(r);
        Float256 cos = CosSeries(r);
        return quadrant switch
        {
            0 => (sin, cos),
            1 => (cos, -sin),
            2 => (-sin, -cos),
            _ => (-cos, sin)
        };
    }
}
