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
    /// <summary>Computes the single-rounded multiply-add of the specified <see cref="Float256"/> values.</summary>
    /// <param name="left">The first multiplicand.</param>
    /// <param name="right">The second multiplicand.</param>
    /// <param name="addend">The value to add to the product.</param>
    /// <returns>Returns <c>(left × right) + addend</c> with a single final rounding.</returns>
    public static Float256 FusedMultiplyAdd(Float256 left, Float256 right, Float256 addend)
    {
        if (IsNaN(left) || IsNaN(right) || IsNaN(addend)) return NaN;

        bool leftInfinity = IsInfinity(left);
        bool rightInfinity = IsInfinity(right);
        bool leftZero = IsZero(left);
        bool rightZero = IsZero(right);

        if ((leftZero && rightInfinity) || (leftInfinity && rightZero)) return NaN;

        bool productSign = IsNegative(left) ^ IsNegative(right);

        if (leftInfinity || rightInfinity)
        {
            Float256 productInf = productSign ? NegativeInfinity : PositiveInfinity;
            if (IsInfinity(addend) && IsNegative(addend) != productSign) return NaN;
            return IsInfinity(addend) ? addend : productInf;
        }

        if (IsInfinity(addend)) return addend;

        bool addendZero = IsZero(addend);

        if (leftZero || rightZero)
        {
            if (addendZero) return productSign && IsNegative(addend) ? NegativeZero : Zero;
            return addend;
        }

        if (addendZero) return Multiply(left, right);

        DecomposeFinite(left.Bits, out _, out int expLeft, out UInt256 sigLeft);
        DecomposeFinite(right.Bits, out _, out int expRight, out UInt256 sigRight);
        DecomposeFinite(addend.Bits, out bool signC, out int expC, out UInt256 sigC);

        NormalizeSubnormal(ref sigLeft, ref expLeft);
        NormalizeSubnormal(ref sigRight, ref expRight);
        NormalizeSubnormal(ref sigC, ref expC);

        UInt512 productBits = UInt512.BigMul(sigLeft, sigRight);
        int prodLsbExp = expLeft + expRight - 472;
        int cLsbExp = expC - 236;

        int prodMsbBit = productBits.GetShortestBitLength() - 1;
        int prodMsbExp = prodLsbExp + prodMsbBit;
        int cMsbExp = cLsbExp + 236;

        int maxMsbExp = Math.Max(prodMsbExp, cMsbExp);
        int baseExp = maxMsbExp - FmaWideBufferMsbTarget;

        PlaceFmaProductInBuffer(productBits, prodLsbExp - baseExp, out UInt512 productBuffer, out bool productSticky);
        PlaceFmaAddendInBuffer(sigC, cLsbExp - baseExp, out UInt512 cBuffer, out bool cSticky);

        UInt512 magnitude;
        bool resultSign;
        bool postSticky;

        if (productSign == signC)
        {
            magnitude = productBuffer + cBuffer;
            resultSign = productSign;
            postSticky = productSticky || cSticky;
        }
        else
        {
            int bufferComparison = productBuffer.CompareTo(cBuffer);

            if (bufferComparison > 0)
            {
                magnitude = productBuffer - cBuffer;
                resultSign = productSign;
                postSticky = productSticky;
                if (cSticky)
                {
                    magnitude = magnitude - UInt512.One;
                    postSticky = true;
                }
            }
            else if (bufferComparison < 0)
            {
                magnitude = cBuffer - productBuffer;
                resultSign = signC;
                postSticky = cSticky;
                if (productSticky)
                {
                    magnitude = magnitude - UInt512.One;
                    postSticky = true;
                }
            }
            else
            {
                if (!productSticky && !cSticky) return Zero;
                if (productSticky && !cSticky) return productSign ? NegativeZero : Zero;
                if (cSticky && !productSticky) return signC ? NegativeZero : Zero;
                return Zero;
            }
        }

        if (UInt512.IsZero(magnitude))
        {
            if (!postSticky) return Zero;
            return RoundToNearestEven(resultSign, MinNormalUnbiasedExponent, UInt256.Zero, false, true);
        }

        int leadingBit = magnitude.GetShortestBitLength() - 1;
        int significandLsbPos = leadingBit - TrailingSignificandBits;

        UInt256 significand;
        bool roundBit;
        bool stickyBit;
        int resultUnbiasedExponent = baseExp + leadingBit;

        if (significandLsbPos < 0)
        {
            int shiftUp = -significandLsbPos;
            UInt512 shifted = magnitude << shiftUp;
            significand = (UInt256)shifted & FmaSignificandFullMask;
            roundBit = false;
            stickyBit = postSticky;
        }
        else
        {
            UInt512 sigBits = magnitude >> significandLsbPos;
            significand = (UInt256)sigBits & FmaSignificandFullMask;

            if (significandLsbPos >= 1)
            {
                int roundPos = significandLsbPos - 1;
                roundBit = !UInt512.IsZero((magnitude >> roundPos) & UInt512.One);
                UInt512 stickyMask = (UInt512.One << roundPos) - UInt512.One;
                stickyBit = !UInt512.IsZero(magnitude & stickyMask) || postSticky;
            }
            else
            {
                roundBit = false;
                stickyBit = postSticky;
            }
        }

        return RoundToNearestEven(resultSign, resultUnbiasedExponent, significand, roundBit, stickyBit);
    }

    /// <summary>
    /// The bit position within the 512-bit FMA accumulation buffer at which the most-significant bit of any operand is placed.
    /// </summary>
    private const int FmaWideBufferMsbTarget = 510;

    /// <summary>
    /// A 237-bit mask covering the full <see cref="Float256"/> significand including the implicit leading bit.
    /// </summary>
    private static readonly UInt256 FmaSignificandFullMask = (UInt256.One << (TrailingSignificandBits + 1)) - UInt256.One;

    /// <summary>
    /// Places the 512-bit product of the FMA multiplicands into the wide accumulation buffer at the specified least-significant bit position, accumulating dropped bits into a sticky flag.
    /// </summary>
    /// <param name="product">The 512-bit product of the multiplicand significands.</param>
    /// <param name="lsbPosition">The bit position within the buffer at which the product's least-significant bit should sit; negative values shift the product right.</param>
    /// <param name="buffer">When this method returns, contains the product positioned within the 512-bit accumulation buffer.</param>
    /// <param name="stickyBit">When this method returns, contains <see langword="true"/> if any non-zero bit was shifted past the buffer's least-significant end; otherwise, <see langword="false"/>.</param>
    private static void PlaceFmaProductInBuffer(UInt512 product, int lsbPosition, out UInt512 buffer, out bool stickyBit)
    {
        if (lsbPosition >= 0)
        {
            buffer = product << lsbPosition;
            stickyBit = false;
            return;
        }

        int shiftDown = -lsbPosition;

        if (shiftDown >= 474)
        {
            buffer = UInt512.Zero;
            stickyBit = !UInt512.IsZero(product);
            return;
        }

        UInt512 mask = (UInt512.One << shiftDown) - UInt512.One;
        stickyBit = !UInt512.IsZero(product & mask);
        buffer = product >> shiftDown;
    }

    /// <summary>
    /// Places the FMA addend's significand into the wide accumulation buffer at the specified least-significant bit position, accumulating dropped bits into a sticky flag.
    /// </summary>
    /// <param name="sigC">The full significand of the addend including its implicit leading bit.</param>
    /// <param name="lsbPosition">The bit position within the buffer at which the addend's least-significant bit should sit; negative values shift the addend right.</param>
    /// <param name="buffer">When this method returns, contains the addend positioned within the 512-bit accumulation buffer.</param>
    /// <param name="stickyBit">When this method returns, contains <see langword="true"/> if any non-zero bit was shifted past the buffer's least-significant end; otherwise, <see langword="false"/>.</param>
    private static void PlaceFmaAddendInBuffer(UInt256 sigC, int lsbPosition, out UInt512 buffer, out bool stickyBit)
    {
        if (lsbPosition >= 0)
        {
            buffer = (UInt512)sigC << lsbPosition;
            stickyBit = false;
            return;
        }

        int shiftDown = -lsbPosition;

        if (shiftDown >= 237)
        {
            buffer = UInt512.Zero;
            stickyBit = !UInt256.IsZero(sigC);
            return;
        }

        UInt256 mask = (UInt256.One << shiftDown) - UInt256.One;
        stickyBit = !UInt256.IsZero(sigC & mask);
        buffer = (UInt512)(sigC >> shiftDown);
    }
}
