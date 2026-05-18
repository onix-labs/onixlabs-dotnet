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
    /// <summary>
    /// The constant three used as the divisor in the Newton-Raphson iteration for <see cref="Cbrt"/>.
    /// </summary>
    private static readonly Float256 CbrtThree = (Float256)3;

    /// <summary>Computes the cube root of the specified <see cref="Float256"/> value.</summary>
    /// <param name="value">The value.</param>
    /// <returns>Returns the cube root of <paramref name="value"/>.</returns>
    public static Float256 Cbrt(Float256 value)
    {
        if (IsNaN(value)) return value;
        if (IsZero(value)) return value;
        if (IsInfinity(value)) return value;

        bool negative = IsNegative(value);
        Float256 absValue = Abs(value);
        double initialEstimate = Math.Cbrt((double)absValue);
        Float256 estimate = (Float256)initialEstimate;

        for (int iteration = 0; iteration < 12; iteration++)
        {
            Float256 squared = estimate * estimate;
            Float256 next = (Two * estimate + absValue / squared) / CbrtThree;
            if (next == estimate) break;
            estimate = next;
        }

        return negative ? -estimate : estimate;
    }

    /// <summary>Computes the <c>n</c>th root of the specified <see cref="Float256"/> value.</summary>
    /// <param name="value">The value.</param>
    /// <param name="n">The index of the root.</param>
    /// <returns>Returns the <c>n</c>th root of <paramref name="value"/>.</returns>
    public static Float256 RootN(Float256 value, int n)
    {
        if (n == 0) return NaN;
        if (n == 1) return value;
        if (n == 2) return Sqrt(value);
        if (n == 3) return Cbrt(value);
        if (n == -1) return One / value;

        if (IsNaN(value)) return value;

        bool nIsOdd = (n & 1) != 0;
        bool negative = IsNegative(value);

        if (IsZero(value))
        {
            if (n > 0)
            {
                return nIsOdd ? value : Zero;
            }
            return nIsOdd && negative ? NegativeInfinity : PositiveInfinity;
        }

        if (IsInfinity(value))
        {
            if (n > 0)
            {
                return nIsOdd && negative ? NegativeInfinity : PositiveInfinity;
            }
            return nIsOdd && negative ? NegativeZero : Zero;
        }

        if (negative && !nIsOdd) return NaN;

        Float256 absValue = Abs(value);
        Float256 result = Exp(Log(absValue) / (Float256)n);
        return negative ? -result : result;
    }

    /// <summary>Computes the Euclidean length <c>√(x² + y²)</c>, avoiding intermediate overflow.</summary>
    /// <param name="x">The first value.</param>
    /// <param name="y">The second value.</param>
    /// <returns>Returns the Euclidean length.</returns>
    public static Float256 Hypot(Float256 x, Float256 y)
    {
        if (IsInfinity(x) || IsInfinity(y)) return PositiveInfinity;
        if (IsNaN(x) || IsNaN(y)) return NaN;

        Float256 absX = Abs(x);
        Float256 absY = Abs(y);

        if (IsZero(absX)) return absY;
        if (IsZero(absY)) return absX;

        Float256 maxAbs;
        Float256 minAbs;
        if (absX >= absY)
        {
            maxAbs = absX;
            minAbs = absY;
        }
        else
        {
            maxAbs = absY;
            minAbs = absX;
        }

        Float256 ratio = minAbs / maxAbs;
        return maxAbs * Sqrt(One + ratio * ratio);
    }
}
