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
    /// Computes the quotient of the specified <see cref="Float128"/> values, rounded to nearest, ties-to-even per IEEE 754.
    /// </summary>
    /// <param name="left">The <see cref="Float128"/> dividend.</param>
    /// <param name="right">The <see cref="Float128"/> divisor.</param>
    /// <returns>Returns the correctly-rounded quotient of the specified <see cref="Float128"/> values.</returns>
    public static Float128 Divide(Float128 left, Float128 right)
    {
        if (IsNaN(left) || IsNaN(right)) return NaN;

        bool resultSign = IsNegative(left) != IsNegative(right);

        if (IsInfinity(left))
        {
            if (IsInfinity(right)) return NaN;
            return resultSign ? NegativeInfinity : PositiveInfinity;
        }

        if (IsInfinity(right))
        {
            return resultSign ? NegativeZero : Zero;
        }

        if (IsZero(left))
        {
            if (IsZero(right)) return NaN;
            return resultSign ? NegativeZero : Zero;
        }

        if (IsZero(right))
        {
            return resultSign ? NegativeInfinity : PositiveInfinity;
        }

        DecomposeFinite(left.RawBits, out _, out int expLeft, out UInt128 sigLeft);
        DecomposeFinite(right.RawBits, out _, out int expRight, out UInt128 sigRight);

        NormalizeSubnormal(ref sigLeft, ref expLeft);
        NormalizeSubnormal(ref sigRight, ref expRight);

        UInt128 numeratorHigh = sigLeft >> 15;
        UInt128 numeratorLow = sigLeft << 113;
        UInt256 numerator = new(numeratorHigh, numeratorLow);

        UInt128 quotient = UInt256.DivRemBy128(numerator, sigRight, out UInt128 remainder);

        bool quotientIs114Bits = (quotient & (UInt128.One << (TrailingSignificandBits + 1))) != UInt128.Zero;

        ComputeRemainderRoundSticky(remainder, sigRight, out bool originalRoundBit, out bool originalStickyBit);

        UInt128 newSignificand;
        bool roundBit;
        bool stickyBit;
        int resultExponent;

        if (quotientIs114Bits)
        {
            newSignificand = quotient >> 1;
            roundBit = (quotient & UInt128.One) != UInt128.Zero;
            stickyBit = originalRoundBit || originalStickyBit;
            resultExponent = expLeft - expRight;
        }
        else
        {
            newSignificand = quotient;
            roundBit = originalRoundBit;
            stickyBit = originalStickyBit;
            resultExponent = expLeft - expRight - 1;
        }

        return RoundToNearestEven(resultSign, resultExponent, newSignificand, roundBit, stickyBit);
    }

    /// <summary>
    /// Computes the quotient of the specified <see cref="Float128"/> values, rounded to nearest, ties-to-even per IEEE 754.
    /// </summary>
    /// <param name="left">The <see cref="Float128"/> dividend.</param>
    /// <param name="right">The <see cref="Float128"/> divisor.</param>
    /// <returns>Returns the correctly-rounded quotient of the specified <see cref="Float128"/> values.</returns>
    public static Float128 operator /(Float128 left, Float128 right) => Divide(left, right);

    /// <summary>
    /// From the division remainder and divisor, derives the round bit and sticky bit needed for correctly-rounded division.
    /// </summary>
    /// <param name="remainder">The remainder produced by the integer significand division.</param>
    /// <param name="divisor">The divisor against which the remainder is compared.</param>
    /// <param name="roundBit">When this method returns, contains the IEEE 754 round bit.</param>
    /// <param name="stickyBit">When this method returns, contains the IEEE 754 sticky bit.</param>
    private static void ComputeRemainderRoundSticky(UInt128 remainder, UInt128 divisor, out bool roundBit, out bool stickyBit)
    {
        UInt128 doubledRemainder = remainder << 1;

        if (doubledRemainder < remainder)
        {
            roundBit = true;
            stickyBit = true;
            return;
        }

        int comparison = doubledRemainder.CompareTo(divisor);

        if (comparison > 0)
        {
            roundBit = true;
            stickyBit = true;
        }
        else if (comparison == 0)
        {
            roundBit = true;
            stickyBit = false;
        }
        else
        {
            roundBit = false;
            stickyBit = remainder != UInt128.Zero;
        }
    }
}
