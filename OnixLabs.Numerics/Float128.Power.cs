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
    /// Computes the specified <see cref="Float128"/> base raised to the specified <see cref="Float128"/> exponent.
    /// </summary>
    /// <param name="x">The base of the exponentiation.</param>
    /// <param name="y">The exponent of the exponentiation.</param>
    /// <returns>Returns <c>x^y</c> following the IEEE 754-2019 specification for special cases.</returns>
    public static Float128 Pow(Float128 x, Float128 y)
    {
        if (IsZero(y)) return One;
        if (x == One) return One;
        if (IsNaN(x)) return x;
        if (IsNaN(y)) return y;

        if (IsZero(x))
        {
            bool yNegative = IsNegative(y);
            bool yIsOddInt = IsOddInteger(y);

            if (yNegative)
            {
                return yIsOddInt && IsNegative(x) ? NegativeInfinity : PositiveInfinity;
            }

            return yIsOddInt && IsNegative(x) ? NegativeZero : Zero;
        }

        if (x == NegativeOne && IsInfinity(y)) return One;

        if (IsPositiveInfinity(x))
        {
            return IsNegative(y) ? Zero : PositiveInfinity;
        }

        if (IsNegativeInfinity(x))
        {
            bool yIsOddInt = IsOddInteger(y);
            if (IsNegative(y))
            {
                return yIsOddInt ? NegativeZero : Zero;
            }
            return yIsOddInt ? NegativeInfinity : PositiveInfinity;
        }

        if (IsInfinity(y))
        {
            Float128 absX = Abs(x);
            bool yPositive = IsPositive(y);
            if (absX < One) return yPositive ? Zero : PositiveInfinity;
            return yPositive ? PositiveInfinity : Zero;
        }

        if (IsNegative(x))
        {
            if (!IsInteger(y)) return NaN;
            bool yIsOdd = IsOddInteger(y);
            Float128 absResult = PowPositive(-x, y);
            return yIsOdd ? -absResult : absResult;
        }

        return PowPositive(x, y);
    }

    /// <summary>
    /// Computes <c>x^y</c> for a positive base, using integer exponentiation when <paramref name="y"/> is a small integer and falling back to <c>exp(y log x)</c> otherwise.
    /// </summary>
    /// <param name="x">The positive base.</param>
    /// <param name="y">The exponent.</param>
    /// <returns>The <see cref="Float128"/> value of <c>x^y</c>.</returns>
    private static Float128 PowPositive(Float128 x, Float128 y)
    {
        if (IsInteger(y) && Abs(y) <= (Float128)64L)
        {
            long n = (long)y;
            return PowInteger(x, n);
        }

        return Exp(y * Log(x));
    }

    /// <summary>
    /// Computes <c>x^n</c> for an integer exponent using exponentiation by squaring, inverting the result for negative exponents.
    /// </summary>
    /// <param name="x">The base.</param>
    /// <param name="n">The signed integer exponent.</param>
    /// <returns>The <see cref="Float128"/> value of <c>x^n</c>.</returns>
    private static Float128 PowInteger(Float128 x, long n)
    {
        if (n == 0L) return One;
        if (n == 1L) return x;

        long absExponent = n < 0L ? -n : n;
        Float128 result = One;
        Float128 baseValue = x;

        while (absExponent > 0L)
        {
            if ((absExponent & 1L) != 0L) result = result * baseValue;
            absExponent >>= 1;
            if (absExponent > 0L) baseValue = baseValue * baseValue;
        }

        return n < 0L ? One / result : result;
    }
}
