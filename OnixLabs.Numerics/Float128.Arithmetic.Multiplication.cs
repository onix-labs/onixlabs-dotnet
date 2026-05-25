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
    /// Computes the product of the specified <see cref="Float128"/> values, rounded to nearest, ties-to-even per IEEE 754.
    /// </summary>
    /// <param name="left">The <paramref name="left"/> value to multiply.</param>
    /// <param name="right">The <paramref name="right"/> value to multiply by.</param>
    /// <returns>Returns the correctly-rounded product of the specified <see cref="Float128"/> values.</returns>
    public static Float128 Multiply(Float128 left, Float128 right)
    {
        if (IsNaN(left) || IsNaN(right)) return NaN;

        bool resultSign = IsNegative(left) != IsNegative(right);

        if (IsInfinity(left))
        {
            if (IsZero(right)) return NaN;
            return resultSign ? NegativeInfinity : PositiveInfinity;
        }

        if (IsInfinity(right))
        {
            if (IsZero(left)) return NaN;
            return resultSign ? NegativeInfinity : PositiveInfinity;
        }

        if (IsZero(left) || IsZero(right))
        {
            return resultSign ? NegativeZero : Zero;
        }

        DecomposeFinite(left.Bits, out _, out int expLeft, out UInt128 sigLeft);
        DecomposeFinite(right.Bits, out _, out int expRight, out UInt128 sigRight);

        NormalizeSubnormal(ref sigLeft, ref expLeft);
        NormalizeSubnormal(ref sigRight, ref expRight);

        UInt256 product = UInt256.BigMul(sigLeft, sigRight);

        bool leadingAt225 = (product.UpperBits & (UInt128.One << 97)) != UInt128.Zero;
        int shift = leadingAt225 ? 113 : 112;
        int resultExponent = expLeft + expRight + (leadingAt225 ? 1 : 0);

        UInt128 newSignificand = leadingAt225
            ? (product.UpperBits << 15) | (product.LowerBits >> 113)
            : (product.UpperBits << 16) | (product.LowerBits >> 112);

        UInt128 roundMask = UInt128.One << (shift - 1);
        UInt128 stickyMask = roundMask - UInt128.One;
        bool roundBit = (product.LowerBits & roundMask) != UInt128.Zero;
        bool stickyBit = (product.LowerBits & stickyMask) != UInt128.Zero;

        return RoundToNearestEven(resultSign, resultExponent, newSignificand, roundBit, stickyBit);
    }

    /// <summary>
    /// Computes the product of the specified <see cref="Float128"/> values, rounded to nearest, ties-to-even per IEEE 754.
    /// </summary>
    /// <param name="left">The <paramref name="left"/> value to multiply.</param>
    /// <param name="right">The <paramref name="right"/> value to multiply by.</param>
    /// <returns>Returns the correctly-rounded product of the specified <see cref="Float128"/> values.</returns>
    public static Float128 operator *(Float128 left, Float128 right) => Multiply(left, right);

    /// <summary>
    /// Normalizes a subnormal significand by left-shifting until its leading bit aligns with the implicit hidden-bit position, decrementing the exponent accordingly.
    /// </summary>
    /// <param name="significand">A reference to the significand, updated in place.</param>
    /// <param name="unbiasedExponent">A reference to the unbiased exponent, decremented by the number of left shifts applied.</param>
    internal static void NormalizeSubnormal(ref UInt128 significand, ref int unbiasedExponent)
    {
        if (significand == UInt128.Zero) return;
        if ((significand & ImplicitSignificandBit) != UInt128.Zero) return;

        int leadingBitPosition = 127 - (int)UInt128.LeadingZeroCount(significand);
        int shift = TrailingSignificandBits - leadingBitPosition;
        significand <<= shift;
        unbiasedExponent -= shift;
    }
}
