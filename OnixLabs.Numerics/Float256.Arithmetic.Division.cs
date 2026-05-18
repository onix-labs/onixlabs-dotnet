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
    /// <summary>Computes the quotient of two <see cref="Float256"/> values.</summary>
    /// <param name="left">The dividend.</param>
    /// <param name="right">The divisor.</param>
    /// <returns>Returns <c>left / right</c> with IEEE 754 special-value handling.</returns>
    public static Float256 Divide(Float256 left, Float256 right)
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

        DecomposeFinite(left.bits, out _, out int expLeft, out UInt256 sigLeft);
        DecomposeFinite(right.bits, out _, out int expRight, out UInt256 sigRight);

        NormalizeSubnormal(ref sigLeft, ref expLeft);
        NormalizeSubnormal(ref sigRight, ref expRight);

        UInt256 numeratorHigh = sigLeft >> 19;
        UInt256 numeratorLow = sigLeft << 237;
        UInt512 numerator = new(numeratorHigh, numeratorLow);

        UInt256 quotient = UInt512.DivRemBy256(numerator, sigRight, out UInt256 remainder);

        bool quotientIs238Bits = (quotient & (UInt256.One << (TrailingSignificandBits + 1))) != UInt256.Zero;

        ComputeRemainderRoundSticky(remainder, sigRight, out bool originalRoundBit, out bool originalStickyBit);

        UInt256 newSignificand;
        bool roundBit;
        bool stickyBit;
        int resultExponent;

        if (quotientIs238Bits)
        {
            newSignificand = quotient >> 1;
            roundBit = (quotient & UInt256.One) != UInt256.Zero;
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
    /// Computes the IEEE 754 round and sticky bits for a long-division remainder by comparing twice the remainder against the divisor.
    /// </summary>
    /// <param name="remainder">The remainder produced by the long division.</param>
    /// <param name="divisor">The divisor used in the long division.</param>
    /// <param name="roundBit">When this method returns, contains the round bit derived from the remainder comparison.</param>
    /// <param name="stickyBit">When this method returns, contains the sticky bit derived from the remainder comparison.</param>
    private static void ComputeRemainderRoundSticky(UInt256 remainder, UInt256 divisor, out bool roundBit, out bool stickyBit)
    {
        UInt256 doubledRemainder = remainder << 1;

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
            stickyBit = !UInt256.IsZero(remainder);
        }
    }

    /// <inheritdoc cref="Divide(Float256, Float256)"/>
    public static Float256 operator /(Float256 left, Float256 right) => Divide(left, right);
}
