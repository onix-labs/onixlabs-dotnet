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
    /// <summary>Computes the product of two <see cref="UInt256"/> values, wrapping on overflow (keeping the low 256 bits).</summary>
    /// <param name="left">The left multiplicand.</param>
    /// <param name="right">The right multiplicand.</param>
    /// <returns>Returns the low 256 bits of the product of <paramref name="left"/> and <paramref name="right"/>.</returns>
    public static UInt256 operator *(UInt256 left, UInt256 right)
    {
        // (a_upper * 2^128 + a_lower) × (b_upper * 2^128 + b_lower)
        //   = a_upper·b_upper·2^256  (drops on overflow)
        //   + (a_upper·b_lower + a_lower·b_upper)·2^128
        //   + a_lower·b_lower

        UInt256 lowerProduct = BigMul(left.LowerBits, right.LowerBits);
        UInt128 crossProductUpperByLower = left.UpperBits * right.LowerBits;
        UInt128 crossProductLowerByUpper = left.LowerBits * right.UpperBits;
        UInt128 crossSum = crossProductUpperByLower + crossProductLowerByUpper;
        UInt128 newUpper = lowerProduct.UpperBits + crossSum;
        return new UInt256(newUpper, lowerProduct.LowerBits);
    }

    /// <summary>Computes the product of two <see cref="UInt256"/> values, throwing on overflow.</summary>
    /// <param name="left">The left multiplicand.</param>
    /// <param name="right">The right multiplicand.</param>
    /// <returns>Returns the product of <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <exception cref="OverflowException">Thrown when the product exceeds <see cref="MaxValue"/>.</exception>
    public static UInt256 operator checked *(UInt256 left, UInt256 right)
    {
        UInt256 high = BigMul(left, right, out UInt256 low);
        if (!IsZero(high)) throw new OverflowException($"Value was either too large or too small for the specified type: {nameof(UInt256)}.");
        return low;
    }

    /// <summary>Multiplies two <see cref="UInt128"/> values and returns the exact 256-bit product.</summary>
    /// <param name="a">The first multiplicand.</param>
    /// <param name="b">The second multiplicand.</param>
    /// <returns>Returns the exact 256-bit product of <paramref name="a"/> and <paramref name="b"/>.</returns>
    public static UInt256 BigMul(UInt128 a, UInt128 b)
    {
        ulong aLow = (ulong)a;
        ulong aHigh = (ulong)(a >> 64);
        ulong bLow = (ulong)b;
        ulong bHigh = (ulong)(b >> 64);

        ulong llHigh = Math.BigMul(aLow, bLow, out ulong llLow);
        ulong lhHigh = Math.BigMul(aLow, bHigh, out ulong lhLow);
        ulong hlHigh = Math.BigMul(aHigh, bLow, out ulong hlLow);
        ulong hhHigh = Math.BigMul(aHigh, bHigh, out ulong hhLow);

        ulong word0 = llLow;

        ulong word1 = llHigh;
        ulong carryTo2 = 0;

        ulong temp = word1 + lhLow;
        if (temp < word1) carryTo2++;
        word1 = temp;

        temp = word1 + hlLow;
        if (temp < word1) carryTo2++;
        word1 = temp;

        ulong word2 = lhHigh;
        ulong carryTo3 = 0;

        temp = word2 + hlHigh;
        if (temp < word2) carryTo3++;
        word2 = temp;

        temp = word2 + hhLow;
        if (temp < word2) carryTo3++;
        word2 = temp;

        temp = word2 + carryTo2;
        if (temp < word2) carryTo3++;
        word2 = temp;

        ulong word3 = hhHigh + carryTo3;

        UInt128 newLower = ((UInt128)word1 << 64) | word0;
        UInt128 newUpper = ((UInt128)word3 << 64) | word2;

        return new UInt256(newUpper, newLower);
    }

    /// <summary>Multiplies two <see cref="UInt256"/> values and returns the full 512-bit product split into two <see cref="UInt256"/> halves.</summary>
    /// <param name="left">The left multiplicand.</param>
    /// <param name="right">The right multiplicand.</param>
    /// <param name="low">When this method returns, contains the low 256 bits of the product.</param>
    /// <returns>Returns the high 256 bits of the product.</returns>
    public static UInt256 BigMul(UInt256 left, UInt256 right, out UInt256 low)
    {
        UInt256 lowerProduct = BigMul(left.LowerBits, right.LowerBits);
        UInt256 crossProductA = BigMul(left.UpperBits, right.LowerBits);
        UInt256 crossProductB = BigMul(left.LowerBits, right.UpperBits);
        UInt256 upperProduct = BigMul(left.UpperBits, right.UpperBits);

        UInt128 word0 = lowerProduct.LowerBits;

        UInt128 sumMiddle = lowerProduct.UpperBits;
        UInt128 carryToHigh = UInt128.Zero;

        UInt128 temp = sumMiddle + crossProductA.LowerBits;
        if (temp < sumMiddle) carryToHigh += UInt128.One;
        sumMiddle = temp;

        temp = sumMiddle + crossProductB.LowerBits;
        if (temp < sumMiddle) carryToHigh += UInt128.One;
        sumMiddle = temp;

        UInt128 word1 = sumMiddle;

        UInt128 sumUpper = upperProduct.LowerBits;
        UInt128 carryHighest = UInt128.Zero;

        temp = sumUpper + crossProductA.UpperBits;
        if (temp < sumUpper) carryHighest += UInt128.One;
        sumUpper = temp;

        temp = sumUpper + crossProductB.UpperBits;
        if (temp < sumUpper) carryHighest += UInt128.One;
        sumUpper = temp;

        temp = sumUpper + carryToHigh;
        if (temp < sumUpper) carryHighest += UInt128.One;
        sumUpper = temp;

        UInt128 word2 = sumUpper;
        UInt128 word3 = upperProduct.UpperBits + carryHighest;

        low = new UInt256(word1, word0);
        return new UInt256(word3, word2);
    }
}
