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
using System.Numerics;

namespace OnixLabs.Numerics;

public readonly partial struct UInt256
{
    /// <summary>Computes the quotient of two <see cref="UInt256"/> values.</summary>
    /// <param name="left">The dividend.</param>
    /// <param name="right">The divisor.</param>
    /// <returns>Returns the quotient of <paramref name="left"/> divided by <paramref name="right"/>.</returns>
    /// <exception cref="DivideByZeroException">Thrown when <paramref name="right"/> is zero.</exception>
    public static UInt256 operator /(UInt256 left, UInt256 right)
    {
        (UInt256 quotient, _) = DivRem(left, right);
        return quotient;
    }

    /// <summary>Computes the quotient and remainder of two <see cref="UInt256"/> values.</summary>
    /// <param name="left">The dividend.</param>
    /// <param name="right">The divisor.</param>
    /// <returns>Returns a tuple containing the quotient and the remainder.</returns>
    /// <exception cref="DivideByZeroException">Thrown when <paramref name="right"/> is zero.</exception>
    public static (UInt256 Quotient, UInt256 Remainder) DivRem(UInt256 left, UInt256 right)
    {
        if (IsZero(right)) throw new DivideByZeroException();
        if (left < right) return (Zero, left);
        if (left == right) return (One, Zero);

        if (right.UpperBits == UInt128.Zero)
        {
            UInt128 divisor = right.LowerBits;
            if (left.UpperBits == UInt128.Zero)
            {
                (UInt128 quotientSmall, UInt128 remainderSmall) = UInt128.DivRem(left.LowerBits, divisor);
                return (new UInt256(UInt128.Zero, quotientSmall), new UInt256(UInt128.Zero, remainderSmall));
            }

            UInt128 quotientLow = DivRemBy128(left, divisor, out UInt128 remainder128);
            UInt128 quotientHigh = left.UpperBits / divisor;
            return (new UInt256(quotientHigh, quotientLow), new UInt256(UInt128.Zero, remainder128));
        }

        // The divisor is wider than 128 bits, so the quotient fits in 128 bits. Hacker's Delight divdu: estimate the quotient by dividing
        // by the divisor's normalized top 128 bits, which can only overestimate the true quotient by at most one after the adjustment below.
        int shift = (int)UInt128.LeadingZeroCount(right.UpperBits);
        UInt128 normalizedDivisor = (right << shift).UpperBits;

        // Why: halving the dividend guarantees that its upper half is below the normalized divisor, as the two-word division requires.
        UInt256 halved = left >>> 1;
        UInt128 estimate = DivideTwoWords(halved.UpperBits, halved.LowerBits, normalizedDivisor, out UInt128 _);
        UInt256 quotient = ((UInt256)estimate << shift) >>> (HalfBitWidth - 1);

        if (!IsZero(quotient)) quotient--;

        UInt256 remainder = left - quotient * right;

        if (remainder >= right)
        {
            quotient++;
            remainder -= right;
        }

        return (quotient, remainder);
    }

    /// <summary>Divides a 256-bit dividend by a 128-bit divisor and returns the low 128 bits of the quotient and the remainder.</summary>
    /// <param name="dividend">The 256-bit dividend.</param>
    /// <param name="divisor">The 128-bit divisor.</param>
    /// <param name="remainder">When this method returns, contains the 128-bit remainder.</param>
    /// <returns>Returns the low 128 bits of the quotient, which is the whole quotient when <c>dividend.Upper &lt; divisor</c>.</returns>
    /// <exception cref="DivideByZeroException">Thrown when <paramref name="divisor"/> is zero.</exception>
    public static UInt128 DivRemBy128(UInt256 dividend, UInt128 divisor, out UInt128 remainder)
    {
        if (divisor == UInt128.Zero) throw new DivideByZeroException();

        // Why: reducing the upper half modulo the divisor leaves the low 128 bits of the quotient and the remainder unchanged,
        // because dividend = (upper div divisor)·divisor·2^128 + ((upper mod divisor)·2^128 + lower).
        UInt128 upper = dividend.UpperBits;
        if (upper >= divisor) upper %= divisor;

        return DivideTwoWords(upper, dividend.LowerBits, divisor, out remainder);
    }

    /// <summary>
    /// Divides the 256-bit value <paramref name="high"/>·2^128 + <paramref name="low"/> by a 128-bit divisor, where
    /// <paramref name="high"/> is less than the divisor so that the quotient fits in 128 bits.
    /// </summary>
    /// <param name="high">The upper 128 bits of the dividend, which must be less than <paramref name="divisor"/>.</param>
    /// <param name="low">The lower 128 bits of the dividend.</param>
    /// <param name="divisor">The non-zero 128-bit divisor.</param>
    /// <param name="remainder">When this method returns, contains the 128-bit remainder.</param>
    /// <returns>Returns the 128-bit quotient.</returns>
    /// <remarks>
    /// Why: this replaces a bit-at-a-time long division of 256 iterations. It is Hacker's Delight divlu with 128-bit words and 64-bit
    /// half-words (Knuth's Algorithm D for a two-digit divisor): after normalizing the divisor so that its top bit is set, each 64-bit
    /// quotient half is estimated from a single division by the divisor's top half and corrected at most twice. Every intermediate
    /// value either fits in 128 bits or is correct modulo 2^128, as in the original.
    /// </remarks>
    private static UInt128 DivideTwoWords(UInt128 high, UInt128 low, UInt128 divisor, out UInt128 remainder)
    {
        UInt128 halfBase = UInt128.One << 64;
        int shift = (int)UInt128.LeadingZeroCount(divisor);

        divisor <<= shift;
        ulong divisorHigh = (ulong)(divisor >> 64);
        ulong divisorLow = (ulong)divisor;

        // A shift of zero must not compute low >> 128, which UInt128 treats as low >> 0.
        UInt128 numeratorHigh = shift == 0 ? high : (high << shift) | (low >> (128 - shift));
        UInt128 numeratorLow = low << shift;
        ulong numeratorLow1 = (ulong)(numeratorLow >> 64);
        ulong numeratorLow0 = (ulong)numeratorLow;

        UInt128 quotient1 = numeratorHigh / divisorHigh;
        UInt128 remainderEstimate = numeratorHigh - quotient1 * divisorHigh;

        while (quotient1 >= halfBase || quotient1 * divisorLow > ((remainderEstimate << 64) | numeratorLow1))
        {
            quotient1--;
            remainderEstimate += divisorHigh;
            if (remainderEstimate >= halfBase) break;
        }

        UInt128 numeratorMiddle = ((numeratorHigh << 64) | numeratorLow1) - quotient1 * divisor;
        UInt128 quotient0 = numeratorMiddle / divisorHigh;
        remainderEstimate = numeratorMiddle - quotient0 * divisorHigh;

        while (quotient0 >= halfBase || quotient0 * divisorLow > ((remainderEstimate << 64) | numeratorLow0))
        {
            quotient0--;
            remainderEstimate += divisorHigh;
            if (remainderEstimate >= halfBase) break;
        }

        remainder = (((numeratorMiddle << 64) | numeratorLow0) - quotient0 * divisor) >> shift;
        return (quotient1 << 64) | quotient0;
    }
}
