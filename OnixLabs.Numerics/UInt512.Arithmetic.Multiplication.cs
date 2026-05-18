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
    /// <summary>Computes the product of two <see cref="UInt512"/> values, wrapping on overflow (keeping the low 512 bits).</summary>
    /// <param name="left">The left multiplicand.</param>
    /// <param name="right">The right multiplicand.</param>
    /// <returns>Returns the low 512 bits of the product of <paramref name="left"/> and <paramref name="right"/>.</returns>
    public static UInt512 operator *(UInt512 left, UInt512 right)
    {
        // (a_upper * 2^256 + a_lower) × (b_upper * 2^256 + b_lower)
        //   = a_upper·b_upper·2^512  (drops on overflow)
        //   + (a_upper·b_lower + a_lower·b_upper)·2^256
        //   + a_lower·b_lower

        UInt512 lowerProduct = BigMul(left.lower, right.lower);
        UInt256 crossProductUpperByLower = left.upper * right.lower;
        UInt256 crossProductLowerByUpper = left.lower * right.upper;
        UInt256 crossSum = crossProductUpperByLower + crossProductLowerByUpper;
        UInt256 newUpper = lowerProduct.upper + crossSum;
        return new UInt512(newUpper, lowerProduct.lower);
    }

    /// <summary>Computes the product of two <see cref="UInt512"/> values, throwing on overflow.</summary>
    /// <param name="left">The left multiplicand.</param>
    /// <param name="right">The right multiplicand.</param>
    /// <returns>Returns the product of <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <exception cref="OverflowException">Thrown when the product exceeds <see cref="MaxValue"/>.</exception>
    public static UInt512 operator checked *(UInt512 left, UInt512 right)
    {
        UInt512 high = BigMul(left, right, out UInt512 low);
        if (!IsZero(high)) throw new OverflowException($"Value was either too large or too small for the specified type: {nameof(UInt512)}.");
        return low;
    }

    /// <summary>Multiplies two <see cref="UInt256"/> values and returns the exact 512-bit product.</summary>
    /// <param name="a">The first multiplicand.</param>
    /// <param name="b">The second multiplicand.</param>
    /// <returns>Returns the exact 512-bit product of <paramref name="a"/> and <paramref name="b"/>.</returns>
    public static UInt512 BigMul(UInt256 a, UInt256 b)
    {
        UInt256 high = UInt256.BigMul(a, b, out UInt256 low);
        return new UInt512(high, low);
    }

    /// <summary>Multiplies two <see cref="UInt512"/> values and returns the full 1024-bit product split into two <see cref="UInt512"/> halves.</summary>
    /// <param name="left">The left multiplicand.</param>
    /// <param name="right">The right multiplicand.</param>
    /// <param name="low">When this method returns, contains the low 512 bits of the product.</param>
    /// <returns>Returns the high 512 bits of the product.</returns>
    public static UInt512 BigMul(UInt512 left, UInt512 right, out UInt512 low)
    {
        UInt512 lowerProduct = BigMul(left.lower, right.lower);
        UInt512 crossProductA = BigMul(left.upper, right.lower);
        UInt512 crossProductB = BigMul(left.lower, right.upper);
        UInt512 upperProduct = BigMul(left.upper, right.upper);

        UInt256 word0 = lowerProduct.lower;

        UInt256 sumMiddle = lowerProduct.upper;
        UInt256 carryToHigh = UInt256.Zero;

        UInt256 temp = sumMiddle + crossProductA.lower;
        if (temp < sumMiddle) carryToHigh += UInt256.One;
        sumMiddle = temp;

        temp = sumMiddle + crossProductB.lower;
        if (temp < sumMiddle) carryToHigh += UInt256.One;
        sumMiddle = temp;

        UInt256 word1 = sumMiddle;

        UInt256 sumUpper = upperProduct.lower;
        UInt256 carryHighest = UInt256.Zero;

        temp = sumUpper + crossProductA.upper;
        if (temp < sumUpper) carryHighest += UInt256.One;
        sumUpper = temp;

        temp = sumUpper + crossProductB.upper;
        if (temp < sumUpper) carryHighest += UInt256.One;
        sumUpper = temp;

        temp = sumUpper + carryToHigh;
        if (temp < sumUpper) carryHighest += UInt256.One;
        sumUpper = temp;

        UInt256 word2 = sumUpper;
        UInt256 word3 = upperProduct.upper + carryHighest;

        low = new UInt512(word1, word0);
        return new UInt512(word3, word2);
    }
}
