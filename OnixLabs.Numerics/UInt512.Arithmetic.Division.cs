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

public readonly partial struct UInt512
{
    /// <summary>Computes the quotient of two <see cref="UInt512"/> values.</summary>
    /// <param name="left">The dividend.</param>
    /// <param name="right">The divisor.</param>
    /// <returns>Returns the quotient of <paramref name="left"/> divided by <paramref name="right"/>.</returns>
    /// <exception cref="DivideByZeroException">Thrown when <paramref name="right"/> is zero.</exception>
    public static UInt512 operator /(UInt512 left, UInt512 right)
    {
        (UInt512 quotient, _) = DivRem(left, right);
        return quotient;
    }

    /// <summary>Computes the quotient and remainder of two <see cref="UInt512"/> values.</summary>
    /// <param name="left">The dividend.</param>
    /// <param name="right">The divisor.</param>
    /// <returns>Returns a tuple containing the quotient and the remainder.</returns>
    /// <exception cref="DivideByZeroException">Thrown when <paramref name="right"/> is zero.</exception>
    public static (UInt512 Quotient, UInt512 Remainder) DivRem(UInt512 left, UInt512 right)
    {
        if (IsZero(right)) throw new DivideByZeroException();
        if (left < right) return (Zero, left);
        if (left == right) return (One, Zero);

        if (UInt256.IsZero(right.UpperBits))
        {
            UInt256 divisor = right.LowerBits;
            if (UInt256.IsZero(left.UpperBits))
            {
                (UInt256 quotientSmall, UInt256 remainderSmall) = UInt256.DivRem(left.LowerBits, divisor);
                return (new UInt512(UInt256.Zero, quotientSmall), new UInt512(UInt256.Zero, remainderSmall));
            }

            UInt256 quotientLow = DivRemBy256(left, divisor, out UInt256 remainder256);
            UInt256 quotientHigh = left.UpperBits / divisor;
            return (new UInt512(quotientHigh, quotientLow), new UInt512(UInt256.Zero, remainder256));
        }

        // The divisor is wider than 256 bits, so the quotient fits in 256 bits. Hacker's Delight divdu, as in UInt256.DivRem: estimate the
        // quotient by dividing by the divisor's normalized top 256 bits, then correct it by at most one.
        int shift = right.UpperBits.UpperBits == UInt128.Zero
            ? UInt256.HalfBitWidth + (int)UInt128.LeadingZeroCount(right.UpperBits.LowerBits)
            : (int)UInt128.LeadingZeroCount(right.UpperBits.UpperBits);

        UInt256 normalizedDivisor = (right << shift).UpperBits;

        // Why: halving the dividend guarantees that its upper half is below the normalized divisor, as the two-word division requires.
        UInt512 halved = left >>> 1;
        UInt256 estimate = DivideTwoWords(halved.UpperBits, halved.LowerBits, normalizedDivisor, out UInt256 _);
        UInt512 quotient = ((UInt512)estimate << shift) >>> (HalfBitWidth - 1);

        if (!IsZero(quotient)) quotient--;

        UInt512 remainder = left - quotient * right;

        if (remainder >= right)
        {
            quotient++;
            remainder -= right;
        }

        return (quotient, remainder);
    }

    /// <summary>Divides a 512-bit dividend by a 256-bit divisor and returns the low 256 bits of the quotient and the remainder.</summary>
    /// <param name="dividend">The 512-bit dividend.</param>
    /// <param name="divisor">The 256-bit divisor.</param>
    /// <param name="remainder">When this method returns, contains the 256-bit remainder.</param>
    /// <returns>Returns the low 256 bits of the quotient, which is the whole quotient when <c>dividend.Upper &lt; divisor</c>.</returns>
    /// <exception cref="DivideByZeroException">Thrown when <paramref name="divisor"/> is zero.</exception>
    public static UInt256 DivRemBy256(UInt512 dividend, UInt256 divisor, out UInt256 remainder)
    {
        if (UInt256.IsZero(divisor)) throw new DivideByZeroException();

        // Why: reducing the upper half modulo the divisor leaves the low 256 bits of the quotient and the remainder unchanged,
        // because dividend = (upper div divisor)·divisor·2^256 + ((upper mod divisor)·2^256 + lower).
        UInt256 upper = dividend.UpperBits;
        if (upper >= divisor) upper %= divisor;

        return DivideTwoWords(upper, dividend.LowerBits, divisor, out remainder);
    }

    /// <summary>
    /// Divides the 512-bit value <paramref name="high"/>·2^256 + <paramref name="low"/> by a 256-bit divisor, where
    /// <paramref name="high"/> is less than the divisor so that the quotient fits in 256 bits.
    /// </summary>
    /// <param name="high">The upper 256 bits of the dividend, which must be less than <paramref name="divisor"/>.</param>
    /// <param name="low">The lower 256 bits of the dividend.</param>
    /// <param name="divisor">The non-zero 256-bit divisor.</param>
    /// <param name="remainder">When this method returns, contains the 256-bit remainder.</param>
    /// <returns>Returns the 256-bit quotient.</returns>
    /// <remarks>
    /// Why: this replaces a bit-at-a-time long division of 512 iterations. It is Hacker's Delight divlu with 256-bit words and 128-bit
    /// half-words, the same algorithm as the private two-word division in <see cref="UInt256"/>: each 128-bit quotient half is
    /// estimated from one division by the normalized divisor's top half and corrected at most twice.
    /// </remarks>
    private static UInt256 DivideTwoWords(UInt256 high, UInt256 low, UInt256 divisor, out UInt256 remainder)
    {
        UInt256 halfBase = UInt256.One << UInt256.HalfBitWidth;
        int shift = divisor.UpperBits == UInt128.Zero
            ? UInt256.HalfBitWidth + (int)UInt128.LeadingZeroCount(divisor.LowerBits)
            : (int)UInt128.LeadingZeroCount(divisor.UpperBits);

        divisor <<= shift;
        UInt256 divisorHigh = divisor.UpperBits;
        UInt256 divisorLow = divisor.LowerBits;

        // A shift of zero must not compute low >> 256, which UInt256 treats as low >> 0.
        UInt256 numeratorHigh = shift == 0 ? high : (high << shift) | (low >> (UInt256.BitWidth - shift));
        UInt256 numeratorLow = low << shift;
        UInt256 numeratorLow1 = numeratorLow.UpperBits;
        UInt256 numeratorLow0 = numeratorLow.LowerBits;

        UInt256 quotient1 = numeratorHigh / divisorHigh;
        UInt256 remainderEstimate = numeratorHigh - quotient1 * divisorHigh;

        while (quotient1 >= halfBase || quotient1 * divisorLow > ((remainderEstimate << UInt256.HalfBitWidth) | numeratorLow1))
        {
            quotient1--;
            remainderEstimate += divisorHigh;
            if (remainderEstimate >= halfBase) break;
        }

        UInt256 numeratorMiddle = ((numeratorHigh << UInt256.HalfBitWidth) | numeratorLow1) - quotient1 * divisor;
        UInt256 quotient0 = numeratorMiddle / divisorHigh;
        remainderEstimate = numeratorMiddle - quotient0 * divisorHigh;

        while (quotient0 >= halfBase || quotient0 * divisorLow > ((remainderEstimate << UInt256.HalfBitWidth) | numeratorLow0))
        {
            quotient0--;
            remainderEstimate += divisorHigh;
            if (remainderEstimate >= halfBase) break;
        }

        remainder = (((numeratorMiddle << UInt256.HalfBitWidth) | numeratorLow0) - quotient0 * divisor) >> shift;
        return (quotient1 << UInt256.HalfBitWidth) | quotient0;
    }
}
