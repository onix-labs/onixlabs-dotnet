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
    /// Computes the sum of the specified <see cref="Float128"/> values, rounded to nearest, ties-to-even per IEEE 754.
    /// </summary>
    /// <param name="left">The <paramref name="left"/> value to add to.</param>
    /// <param name="right">The <paramref name="right"/> value to add.</param>
    /// <returns>Returns the correctly rounded sum of the specified <see cref="Float128"/> values.</returns>
    public static Float128 Add(Float128 left, Float128 right)
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

        DecomposeFinite(left.Bits, out bool signLeft, out int expLeft, out UInt128 sigLeft);
        DecomposeFinite(right.Bits, out bool signRight, out int expRight, out UInt128 sigRight);

        return signLeft == signRight
            ? AddMagnitudes(signLeft, expLeft, sigLeft, expRight, sigRight)
            : SubtractMagnitudes(signLeft, expLeft, sigLeft, signRight, expRight, sigRight);
    }

    /// <summary>
    /// Computes the sum of the specified <see cref="Float128"/> values, rounded to nearest, ties-to-even per IEEE 754.
    /// </summary>
    /// <param name="left">The <paramref name="left"/> value to add to.</param>
    /// <param name="right">The <paramref name="right"/> value to add.</param>
    /// <returns>Returns the correctly rounded sum of the specified <see cref="Float128"/> values.</returns>
    public static Float128 operator +(Float128 left, Float128 right) => Add(left, right);

    /// <summary>
    /// Adds two magnitudes that share the same sign, aligning exponents and rounding the result to nearest, ties-to-even.
    /// </summary>
    /// <param name="sign">The common sign of both operands.</param>
    /// <param name="expA">The unbiased exponent of the first operand.</param>
    /// <param name="sigA">The full significand of the first operand including its implicit leading bit.</param>
    /// <param name="expB">The unbiased exponent of the second operand.</param>
    /// <param name="sigB">The full significand of the second operand including its implicit leading bit.</param>
    /// <returns>The correctly rounded <see cref="Float128"/> sum of the two magnitudes.</returns>
    private static Float128 AddMagnitudes(bool sign, int expA, UInt128 sigA, int expB, UInt128 sigB)
    {
        if (expA < expB)
        {
            (expA, expB) = (expB, expA);
            (sigA, sigB) = (sigB, sigA);
        }

        int delta = expA - expB;

        AlignSmallerForArithmetic(sigB, delta, out UInt128 smallerHigh, out UInt128 smallerLow, out bool extraSticky);

        UInt128 resultHigh = sigA + smallerHigh;
        UInt128 resultLow = smallerLow;

        int resultExp = expA;

        if ((resultHigh & (UInt128.One << (TrailingSignificandBits + 1))) != UInt128.Zero)
        {
            UInt128 carryOutBit = resultHigh & UInt128.One;
            resultHigh >>= 1;
            resultLow = (carryOutBit << 127) | (resultLow >> 1);
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
    /// <returns>The correctly rounded <see cref="Float128"/> difference of the two magnitudes.</returns>
    private static Float128 SubtractMagnitudes(bool signLeft, int expLeft, UInt128 sigLeft, bool signRight, int expRight, UInt128 sigRight)
    {
        int magnitudeComparison;
        if (expLeft > expRight) magnitudeComparison = 1;
        else if (expLeft < expRight) magnitudeComparison = -1;
        else magnitudeComparison = sigLeft.CompareTo(sigRight);

        if (magnitudeComparison == 0) return Zero;

        bool resultSign;
        int largerExp;
        int smallerExp;
        UInt128 largerSig;
        UInt128 smallerSig;

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
        AlignSmallerForArithmetic(smallerSig, delta, out UInt128 smallerHigh, out UInt128 smallerLow, out bool extraSticky);

        UInt128 borrow = smallerLow != UInt128.Zero ? UInt128.One : UInt128.Zero;
        UInt128 diffLow = UInt128.Zero - smallerLow;
        UInt128 diffHigh = largerSig - smallerHigh - borrow;

        int resultExp = largerExp;
        NormalizeSubtractionResult(ref diffHigh, ref diffLow, ref resultExp);

        ExtractRoundSticky(diffLow, extraSticky, out bool roundBit, out bool stickyBit);
        return RoundToNearestEven(resultSign, resultExp, diffHigh, roundBit, stickyBit);
    }

    /// <summary>
    /// Aligns the smaller operand's significand by shifting it right by the exponent difference into a (high, low) pair, capturing any dropped bits as sticky.
    /// </summary>
    /// <param name="smallerSig">The significand of the smaller-magnitude operand.</param>
    /// <param name="delta">The exponent difference between the larger and smaller operands.</param>
    /// <param name="smallerHigh">When this method returns, contains the high 128 bits of the aligned significand.</param>
    /// <param name="smallerLow">When this method returns, contains the low 128 bits of the aligned significand.</param>
    /// <param name="extraSticky">When this method returns, indicates whether any non-zero bits were shifted past the low limb.</param>
    private static void AlignSmallerForArithmetic(UInt128 smallerSig, int delta, out UInt128 smallerHigh, out UInt128 smallerLow, out bool extraSticky)
    {
        if (delta == 0)
        {
            smallerHigh = smallerSig;
            smallerLow = UInt128.Zero;
            extraSticky = false;
        }
        else if (delta < 128)
        {
            smallerHigh = smallerSig >> delta;
            smallerLow = smallerSig << (128 - delta);
            extraSticky = false;
        }
        else if (delta == 128)
        {
            smallerHigh = UInt128.Zero;
            smallerLow = smallerSig;
            extraSticky = false;
        }
        else if (delta < 256)
        {
            int extraShift = delta - 128;
            smallerHigh = UInt128.Zero;
            UInt128 droppedMask = (UInt128.One << extraShift) - UInt128.One;
            extraSticky = (smallerSig & droppedMask) != UInt128.Zero;
            smallerLow = smallerSig >> extraShift;
        }
        else
        {
            smallerHigh = UInt128.Zero;
            smallerLow = UInt128.Zero;
            extraSticky = smallerSig != UInt128.Zero;
        }
    }

    /// <summary>
    /// Normalizes the (high, low) difference pair so its leading set bit aligns with the implicit hidden-bit position, adjusting the exponent accordingly.
    /// </summary>
    /// <param name="diffHigh">A reference to the high 128 bits of the difference, updated in place.</param>
    /// <param name="diffLow">A reference to the low 128 bits of the difference, updated in place.</param>
    /// <param name="resultExp">A reference to the exponent, decremented by the number of left shifts applied.</param>
    private static void NormalizeSubtractionResult(ref UInt128 diffHigh, ref UInt128 diffLow, ref int resultExp)
    {
        if (diffHigh == UInt128.Zero && diffLow == UInt128.Zero) return;

        if (diffHigh != UInt128.Zero)
        {
            int leadingZeros = (int)UInt128.LeadingZeroCount(diffHigh);
            int leadingBitPosition = 127 - leadingZeros;
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
            int leadingZeros = (int)UInt128.LeadingZeroCount(diffLow);
            int leadingBitPositionInPair = (127 - leadingZeros) - 128;
            int shift = TrailingSignificandBits - leadingBitPositionInPair;

            ShiftPairLeft(ref diffHigh, ref diffLow, shift);
            resultExp -= shift;
        }
    }

    /// <summary>
    /// Shifts a (high, low) significand pair left by the specified amount, propagating bits between the pair.
    /// </summary>
    /// <param name="high">A reference to the high 128 bits, updated in place.</param>
    /// <param name="low">A reference to the low 128 bits, updated in place.</param>
    /// <param name="shift">The number of bit positions to shift left.</param>
    private static void ShiftPairLeft(ref UInt128 high, ref UInt128 low, int shift)
    {
        if (shift <= 0) return;

        if (shift < 128)
        {
            high = (high << shift) | (low >> (128 - shift));
            low <<= shift;
        }
        else if (shift < 256)
        {
            high = low << (shift - 128);
            low = UInt128.Zero;
        }
        else
        {
            high = UInt128.Zero;
            low = UInt128.Zero;
        }
    }

    /// <summary>
    /// Shifts a (high, low) significand pair right by the specified amount, propagating bits between the pair.
    /// </summary>
    /// <param name="high">A reference to the high 128 bits, updated in place.</param>
    /// <param name="low">A reference to the low 128 bits, updated in place.</param>
    /// <param name="shift">The number of bit positions to shift right.</param>
    private static void ShiftPairRight(ref UInt128 high, ref UInt128 low, int shift)
    {
        if (shift <= 0) return;

        if (shift < 128)
        {
            low = (low >> shift) | (high << (128 - shift));
            high >>= shift;
        }
        else if (shift < 256)
        {
            low = high >> (shift - 128);
            high = UInt128.Zero;
        }
        else
        {
            high = UInt128.Zero;
            low = UInt128.Zero;
        }
    }

    /// <summary>
    /// Extracts the IEEE 754 round and sticky bits from the low limb of an intermediate significand pair, combining the existing extra sticky flag.
    /// </summary>
    /// <param name="low">The low 128 bits of the intermediate significand.</param>
    /// <param name="extraSticky">A flag indicating whether bits beyond the low limb were non-zero.</param>
    /// <param name="roundBit">When this method returns, contains the round bit immediately below the kept significand.</param>
    /// <param name="stickyBit">When this method returns, contains the sticky bit summarizing all lower-order bits.</param>
    private static void ExtractRoundSticky(UInt128 low, bool extraSticky, out bool roundBit, out bool stickyBit)
    {
        UInt128 roundBitMask = UInt128.One << 127;
        roundBit = (low & roundBitMask) != UInt128.Zero;
        UInt128 stickyMask = roundBitMask - UInt128.One;
        stickyBit = (low & stickyMask) != UInt128.Zero || extraSticky;
    }
}
