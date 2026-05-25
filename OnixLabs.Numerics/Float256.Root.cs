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

    /// <summary>
    /// Computes the cube root of the specified <see cref="Float256"/> value.
    /// </summary>
    /// <param name="value">The value whose cube root is to be computed.</param>
    /// <returns>Returns the cube root of <paramref name="value"/>; preserves the sign of zero and of negative inputs; ±infinity for ±infinity.</returns>
    public static Float256 Cbrt(Float256 value)
    {
        if (IsNaN(value)) return value;
        if (IsZero(value)) return value;
        if (IsInfinity(value)) return value;

        bool negative = IsNegative(value);
        Float256 absValue = Abs(value);

        DecomposeFinite(absValue.Bits, out _, out int unbiasedExponent, out UInt256 significand);
        NormalizeSubnormal(ref significand, ref unbiasedExponent);

        int thirdExponent = unbiasedExponent / 3;
        UInt256 initialEstimateBits = new UInt256(UInt128.Zero, (UInt128)(uint)(thirdExponent + ExponentBias)) << BiasedExponentShift;
        Float256 estimate = new(initialEstimateBits);

        for (int iteration = 0; iteration < 12; iteration++)
        {
            Float256 squared = estimate * estimate;
            Float256 next = (Two * estimate + absValue / squared) / CbrtThree;
            if (next == estimate) break;
            estimate = next;
        }

        return negative ? -estimate : estimate;
    }

    /// <summary>
    /// Computes the <c>n</c>th root of the specified <see cref="Float256"/> value.
    /// </summary>
    /// <param name="value">The value whose root is to be computed.</param>
    /// <param name="n">The index of the root.</param>
    /// <returns>Returns the <c>n</c>th root of <paramref name="value"/>; NaN for an even root of a negative value; preserves the sign of zero and infinities for odd <c>n</c>.</returns>
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

    /// <summary>
    /// Computes the Euclidean length, <c>√(x² + y²)</c>, of the specified <see cref="Float256"/> values, avoiding intermediate overflow.
    /// </summary>
    /// <param name="x">The first value.</param>
    /// <param name="y">The second value.</param>
    /// <returns>Returns the Euclidean length of <paramref name="x"/> and <paramref name="y"/>; <see cref="PositiveInfinity"/> when either input is infinite.</returns>
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
