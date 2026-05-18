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
    /// <summary>Computes the sum of two <see cref="Float256"/> values.</summary>
    /// <param name="left">The left value to add.</param>
    /// <param name="right">The right value to add.</param>
    /// <returns>Returns <c>left + right</c> with IEEE 754 special-value handling.</returns>
    public static Float256 Add(Float256 left, Float256 right)
    {
        if (IsNaN(left) || IsNaN(right)) return NaN;

        if (IsInfinity(left))
        {
            if (IsInfinity(right)) return IsNegative(left) == IsNegative(right) ? left : NaN;
            return left;
        }

        if (IsInfinity(right)) return right;

        if (IsZero(left))
        {
            if (IsZero(right)) return IsNegative(left) == IsNegative(right) ? left : Zero;
            return right;
        }

        if (IsZero(right)) return left;

        DecomposeFinite(left.RawBits, out bool signLeft, out int expLeft, out UInt256 sigLeft);
        DecomposeFinite(right.RawBits, out bool signRight, out int expRight, out UInt256 sigRight);

        return signLeft == signRight
            ? AddMagnitudes(signLeft, expLeft, sigLeft, expRight, sigRight)
            : SubtractMagnitudes(signLeft, expLeft, sigLeft, signRight, expRight, sigRight);
    }

    /// <inheritdoc cref="Add(Float256, Float256)"/>
    public static Float256 operator +(Float256 left, Float256 right) => Add(left, right);

    /// <summary>
    /// Adds two magnitudes that share the same sign, aligning exponents and rounding the result to nearest, ties-to-even.
    /// </summary>
    /// <param name="sign">The common sign of both operands.</param>
    /// <param name="expA">The unbiased exponent of the first operand.</param>
    /// <param name="sigA">The full significand of the first operand including its implicit leading bit.</param>
    /// <param name="expB">The unbiased exponent of the second operand.</param>
    /// <param name="sigB">The full significand of the second operand including its implicit leading bit.</param>
    /// <returns>Returns the correctly rounded <see cref="Float256"/> sum of the two magnitudes.</returns>
    private static Float256 AddMagnitudes(bool sign, int expA, UInt256 sigA, int expB, UInt256 sigB)
    {
        if (expA < expB)
        {
            (expA, expB) = (expB, expA);
            (sigA, sigB) = (sigB, sigA);
        }

        int delta = expA - expB;

        AlignSmallerForArithmetic(sigB, delta, out UInt256 smallerHigh, out UInt256 smallerLow, out bool extraSticky);

        UInt256 resultHigh = sigA + smallerHigh;
        UInt256 resultLow = smallerLow;

        int resultExp = expA;

        if ((resultHigh & (UInt256.One << (TrailingSignificandBits + 1))) != UInt256.Zero)
        {
            UInt256 carryOutBit = resultHigh & UInt256.One;
            resultHigh = resultHigh >> 1;
            resultLow = (carryOutBit << 255) | (resultLow >> 1);
            resultExp++;
        }

        ExtractRoundSticky(resultLow, extraSticky, out bool roundBit, out bool stickyBit);
        return RoundToNearestEven(sign, resultExp, resultHigh, roundBit, stickyBit);
    }

    /// <summary>
    /// Subtracts the smaller magnitude from the larger and assigns the sign of the larger operand, rounding to nearest, ties-to-even.
    /// </summary>
    /// <param name="signLeft">The sign of the left operand.</param>
    /// <param name="expLeft">The unbiased exponent of the left operand.</param>
    /// <param name="sigLeft">The full significand of the left operand including its implicit leading bit.</param>
    /// <param name="signRight">The sign of the right operand.</param>
    /// <param name="expRight">The unbiased exponent of the right operand.</param>
    /// <param name="sigRight">The full significand of the right operand including its implicit leading bit.</param>
    /// <returns>Returns the correctly rounded <see cref="Float256"/> difference of the two magnitudes.</returns>
    private static Float256 SubtractMagnitudes(bool signLeft, int expLeft, UInt256 sigLeft, bool signRight, int expRight, UInt256 sigRight)
    {
        int magnitudeComparison;
        if (expLeft > expRight) magnitudeComparison = 1;
        else if (expLeft < expRight) magnitudeComparison = -1;
        else magnitudeComparison = sigLeft.CompareTo(sigRight);

        if (magnitudeComparison == 0) return Zero;

        bool resultSign;
        int largerExp;
        int smallerExp;
        UInt256 largerSig;
        UInt256 smallerSig;

        if (magnitudeComparison > 0)
        {
            resultSign = signLeft;
            largerExp = expLeft; largerSig = sigLeft;
            smallerExp = expRight; smallerSig = sigRight;
        }
        else
        {
            resultSign = signRight;
            largerExp = expRight; largerSig = sigRight;
            smallerExp = expLeft; smallerSig = sigLeft;
        }

        int delta = largerExp - smallerExp;
        AlignSmallerForArithmetic(smallerSig, delta, out UInt256 smallerHigh, out UInt256 smallerLow, out bool extraSticky);

        UInt256 borrow = !UInt256.IsZero(smallerLow) ? UInt256.One : UInt256.Zero;
        UInt256 diffLow = UInt256.Zero - smallerLow;
        UInt256 diffHigh = largerSig - smallerHigh - borrow;

        int resultExp = largerExp;
        NormalizeSubtractionResult(ref diffHigh, ref diffLow, ref resultExp);

        ExtractRoundSticky(diffLow, extraSticky, out bool roundBit, out bool stickyBit);
        return RoundToNearestEven(resultSign, resultExp, diffHigh, roundBit, stickyBit);
    }

    /// <summary>
    /// Shifts the smaller operand's significand right by the exponent difference into a 512-bit high/low pair, retaining dropped bits as a sticky flag.
    /// </summary>
    /// <param name="smallerSig">The full significand of the smaller-magnitude operand.</param>
    /// <param name="delta">The non-negative difference between the larger and smaller unbiased exponents.</param>
    /// <param name="smallerHigh">When this method returns, contains the upper 256 bits of the aligned significand.</param>
    /// <param name="smallerLow">When this method returns, contains the lower 256 bits of the aligned significand.</param>
    /// <param name="extraSticky">When this method returns, contains <see langword="true"/> if any non-zero bit was shifted past the 512-bit window; otherwise, <see langword="false"/>.</param>
    private static void AlignSmallerForArithmetic(UInt256 smallerSig, int delta, out UInt256 smallerHigh, out UInt256 smallerLow, out bool extraSticky)
    {
        if (delta == 0)
        {
            smallerHigh = smallerSig;
            smallerLow = UInt256.Zero;
            extraSticky = false;
        }
        else if (delta < 256)
        {
            smallerHigh = smallerSig >> delta;
            smallerLow = smallerSig << (256 - delta);
            extraSticky = false;
        }
        else if (delta == 256)
        {
            smallerHigh = UInt256.Zero;
            smallerLow = smallerSig;
            extraSticky = false;
        }
        else if (delta < 512)
        {
            int extraShift = delta - 256;
            smallerHigh = UInt256.Zero;
            UInt256 droppedMask = (UInt256.One << extraShift) - UInt256.One;
            extraSticky = (smallerSig & droppedMask) != UInt256.Zero;
            smallerLow = smallerSig >> extraShift;
        }
        else
        {
            smallerHigh = UInt256.Zero;
            smallerLow = UInt256.Zero;
            extraSticky = !UInt256.IsZero(smallerSig);
        }
    }

    /// <summary>
    /// Renormalises the 512-bit subtraction result so that the leading bit sits at the implicit-significand position, adjusting the exponent accordingly.
    /// </summary>
    /// <param name="diffHigh">A reference to the upper 256 bits of the difference; updated in place.</param>
    /// <param name="diffLow">A reference to the lower 256 bits of the difference; updated in place.</param>
    /// <param name="resultExp">A reference to the unbiased exponent of the result; decremented by the number of left-shift positions.</param>
    private static void NormalizeSubtractionResult(ref UInt256 diffHigh, ref UInt256 diffLow, ref int resultExp)
    {
        if (UInt256.IsZero(diffHigh) && UInt256.IsZero(diffLow)) return;

        if (!UInt256.IsZero(diffHigh))
        {
            int leadingZeros = (int)UInt256.LeadingZeroCount(diffHigh);
            int leadingBitPosition = 255 - leadingZeros;
            int shift = TrailingSignificandBits - leadingBitPosition;

            if (shift > 0)
            {
                ShiftPairLeft(ref diffHigh, ref diffLow, shift);
                resultExp -= shift;
            }
            else if (shift < 0)
            {
                ShiftPairRight(ref diffHigh, ref diffLow, -shift);
                resultExp -= shift;
            }
        }
        else
        {
            int leadingZeros = (int)UInt256.LeadingZeroCount(diffLow);
            int leadingBitPositionInPair = (255 - leadingZeros) - 256;
            int shift = TrailingSignificandBits - leadingBitPositionInPair;

            ShiftPairLeft(ref diffHigh, ref diffLow, shift);
            resultExp -= shift;
        }
    }

    /// <summary>
    /// Shifts a 512-bit value composed of an upper and lower <see cref="UInt256"/> pair to the left by the specified number of bits.
    /// </summary>
    /// <param name="high">A reference to the upper 256 bits; updated in place.</param>
    /// <param name="low">A reference to the lower 256 bits; updated in place.</param>
    /// <param name="shift">The number of bits to shift left.</param>
    private static void ShiftPairLeft(ref UInt256 high, ref UInt256 low, int shift)
    {
        if (shift <= 0) return;

        if (shift < 256)
        {
            high = (high << shift) | (low >> (256 - shift));
            low = low << shift;
        }
        else if (shift < 512)
        {
            high = low << (shift - 256);
            low = UInt256.Zero;
        }
        else
        {
            high = UInt256.Zero;
            low = UInt256.Zero;
        }
    }

    /// <summary>
    /// Shifts a 512-bit value composed of an upper and lower <see cref="UInt256"/> pair to the right by the specified number of bits.
    /// </summary>
    /// <param name="high">A reference to the upper 256 bits; updated in place.</param>
    /// <param name="low">A reference to the lower 256 bits; updated in place.</param>
    /// <param name="shift">The number of bits to shift right.</param>
    private static void ShiftPairRight(ref UInt256 high, ref UInt256 low, int shift)
    {
        if (shift <= 0) return;

        if (shift < 256)
        {
            low = (low >> shift) | (high << (256 - shift));
            high = high >> shift;
        }
        else if (shift < 512)
        {
            low = high >> (shift - 256);
            high = UInt256.Zero;
        }
        else
        {
            high = UInt256.Zero;
            low = UInt256.Zero;
        }
    }

    /// <summary>
    /// Extracts the IEEE 754 round and sticky bits from the lower 256 bits of a wide intermediate, combining with any prior sticky flag.
    /// </summary>
    /// <param name="low">The lower 256 bits of the wide intermediate result.</param>
    /// <param name="extraSticky">Any sticky bit accumulated from earlier shifts.</param>
    /// <param name="roundBit">When this method returns, contains the round bit (the most significant bit of <paramref name="low"/>).</param>
    /// <param name="stickyBit">When this method returns, contains the OR of every bit below the round bit together with <paramref name="extraSticky"/>.</param>
    private static void ExtractRoundSticky(UInt256 low, bool extraSticky, out bool roundBit, out bool stickyBit)
    {
        UInt256 roundBitMask = UInt256.One << 255;
        roundBit = (low & roundBitMask) != UInt256.Zero;
        UInt256 stickyMask = roundBitMask - UInt256.One;
        stickyBit = (low & stickyMask) != UInt256.Zero || extraSticky;
    }
}
