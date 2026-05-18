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
    /// <summary>Computes the product of two <see cref="Float256"/> values.</summary>
    /// <param name="left">The left multiplicand.</param>
    /// <param name="right">The right multiplicand.</param>
    /// <returns>Returns <c>left × right</c> with IEEE 754 special-value handling.</returns>
    public static Float256 Multiply(Float256 left, Float256 right)
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

        DecomposeFinite(left.RawBits, out _, out int expLeft, out UInt256 sigLeft);
        DecomposeFinite(right.RawBits, out _, out int expRight, out UInt256 sigRight);

        NormalizeSubnormal(ref sigLeft, ref expLeft);
        NormalizeSubnormal(ref sigRight, ref expRight);

        UInt512 product = UInt512.BigMul(sigLeft, sigRight);

        bool leadingAt473 = (product.Upper & (UInt256.One << 217)) != UInt256.Zero;
        int shift = leadingAt473 ? 237 : 236;
        int resultExponent = expLeft + expRight + (leadingAt473 ? 1 : 0);

        UInt256 newSignificand = leadingAt473
            ? (product.Upper << 19) | (product.Lower >> 237)
            : (product.Upper << 20) | (product.Lower >> 236);

        UInt256 roundMask = UInt256.One << (shift - 1);
        UInt256 stickyMask = roundMask - UInt256.One;
        bool roundBit = (product.Lower & roundMask) != UInt256.Zero;
        bool stickyBit = (product.Lower & stickyMask) != UInt256.Zero;

        return RoundToNearestEven(resultSign, resultExponent, newSignificand, roundBit, stickyBit);
    }

    /// <inheritdoc cref="Multiply(Float256, Float256)"/>
    public static Float256 operator *(Float256 left, Float256 right) => Multiply(left, right);
}
