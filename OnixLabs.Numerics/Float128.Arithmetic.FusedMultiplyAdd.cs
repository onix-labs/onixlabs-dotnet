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
    /// Computes the single-rounded multiply-add of the specified <see cref="Float128"/> values: <c>(left × right) + addend</c>.
    /// </summary>
    /// <param name="left">The first multiplicand.</param>
    /// <param name="right">The second multiplicand.</param>
    /// <param name="addend">The value to add to the product.</param>
    /// <returns>Returns <c>(left × right) + addend</c> with a single final rounding; NaN when any operand is NaN or for <c>0 × ∞</c>; standard infinity arithmetic otherwise.</returns>
    public static Float128 FusedMultiplyAdd(Float128 left, Float128 right, Float128 addend)
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
            Float128 productInf = productSign ? NegativeInfinity : PositiveInfinity;
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

        DecomposeFinite(left.RawBits, out _, out int expLeft, out UInt128 sigLeft);
        DecomposeFinite(right.RawBits, out _, out int expRight, out UInt128 sigRight);
        DecomposeFinite(addend.RawBits, out bool signC, out int expC, out UInt128 sigC);

        NormalizeSubnormal(ref sigLeft, ref expLeft);
        NormalizeSubnormal(ref sigRight, ref expRight);
        NormalizeSubnormal(ref sigC, ref expC);

        UInt256 productBits = UInt256.BigMul(sigLeft, sigRight);
        int prodLsbExp = expLeft + expRight - 224;
        int cLsbExp = expC - 112;

        int prodMsbBit = productBits.GetShortestBitLength() - 1;
        int prodMsbExp = prodLsbExp + prodMsbBit;
        int cMsbExp = cLsbExp + 112;

        int maxMsbExp = Math.Max(prodMsbExp, cMsbExp);
        int baseExp = maxMsbExp - WideBufferMsbTarget;

        PlaceProductInBuffer(productBits, prodLsbExp - baseExp, out UInt512 productBuffer, out bool productSticky);
        PlaceAddendInBuffer(sigC, cLsbExp - baseExp, out UInt512 cBuffer, out bool cSticky);

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
                if (productSticky && !cSticky)
                {
                    return productSign ? NegativeZero : Zero;
                }
                if (cSticky && !productSticky)
                {
                    return signC ? NegativeZero : Zero;
                }
                return Zero;
            }
        }

        if (UInt512.IsZero(magnitude))
        {
            if (!postSticky) return Zero;
            return RoundToNearestEven(resultSign, MinNormalUnbiasedExponent, UInt128.Zero, false, true);
        }

        int leadingBit = magnitude.GetShortestBitLength() - 1;
        int significandLsbPos = leadingBit - TrailingSignificandBits;

        UInt128 significand;
        bool roundBit;
        bool stickyBit;
        int resultUnbiasedExponent;

        resultUnbiasedExponent = baseExp + leadingBit;

        if (significandLsbPos < 0)
        {
            int shiftUp = -significandLsbPos;
            UInt512 shifted = magnitude << shiftUp;
            significand = (UInt128)shifted & SignificandFullMask;
            roundBit = false;
            stickyBit = postSticky;
        }
        else
        {
            UInt512 sigBits = magnitude >> significandLsbPos;
            significand = (UInt128)sigBits & SignificandFullMask;

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
    /// The bit position within the 512-bit accumulator at which the most significant bit of the product is anchored.
    /// </summary>
    private const int WideBufferMsbTarget = 448;

    /// <summary>
    /// A mask covering all bits of a normalized <see cref="Float128"/> significand including the implicit leading bit.
    /// </summary>
    private static readonly UInt128 SignificandFullMask = (UInt128.One << (TrailingSignificandBits + 1)) - UInt128.One;

    /// <summary>
    /// Places the 256-bit product into the 512-bit accumulator at the specified bit position, capturing any dropped bits as sticky.
    /// </summary>
    /// <param name="product">The 256-bit product of the two multiplicand significands.</param>
    /// <param name="lsbPosition">The bit position at which the least significant bit of <paramref name="product"/> is placed.</param>
    /// <param name="buffer">When this method returns, contains the 512-bit accumulator holding the placed product.</param>
    /// <param name="stickyBit">When this method returns, indicates whether any non-zero bits were shifted out below the accumulator.</param>
    private static void PlaceProductInBuffer(UInt256 product, int lsbPosition, out UInt512 buffer, out bool stickyBit)
    {
        if (lsbPosition >= 0)
        {
            buffer = (UInt512)product << lsbPosition;
            stickyBit = false;
            return;
        }

        int shiftDown = -lsbPosition;

        if (shiftDown >= 226)
        {
            buffer = UInt512.Zero;
            stickyBit = !UInt256.IsZero(product);
            return;
        }

        UInt256 mask = (UInt256.One << shiftDown) - UInt256.One;
        stickyBit = !UInt256.IsZero(product & mask);
        buffer = (UInt512)(product >> shiftDown);
    }

    /// <summary>
    /// Places the addend significand into the 512-bit accumulator at the specified bit position, capturing any dropped bits as sticky.
    /// </summary>
    /// <param name="sigC">The full significand of the addend including its implicit leading bit.</param>
    /// <param name="lsbPosition">The bit position at which the least significant bit of <paramref name="sigC"/> is placed.</param>
    /// <param name="buffer">When this method returns, contains the 512-bit accumulator holding the placed addend.</param>
    /// <param name="stickyBit">When this method returns, indicates whether any non-zero bits were shifted out below the accumulator.</param>
    private static void PlaceAddendInBuffer(UInt128 sigC, int lsbPosition, out UInt512 buffer, out bool stickyBit)
    {
        if (lsbPosition >= 0)
        {
            buffer = (UInt512)sigC << lsbPosition;
            stickyBit = false;
            return;
        }

        int shiftDown = -lsbPosition;

        if (shiftDown >= 113)
        {
            buffer = UInt512.Zero;
            stickyBit = sigC != UInt128.Zero;
            return;
        }

        UInt128 mask = (UInt128.One << shiftDown) - UInt128.One;
        stickyBit = (sigC & mask) != UInt128.Zero;
        buffer = (UInt512)(sigC >> shiftDown);
    }
}
