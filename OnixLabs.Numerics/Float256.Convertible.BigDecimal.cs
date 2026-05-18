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

public readonly partial struct Float256
{
    /// <summary>
    /// Converts the specified finite <see cref="Float256"/> value to a <see cref="BigDecimal"/> exactly.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns a <see cref="BigDecimal"/> whose value equals <paramref name="value"/> exactly.</returns>
    /// <exception cref="OverflowException">Thrown when <paramref name="value"/> is NaN or infinite.</exception>
    public static explicit operator BigDecimal(Float256 value)
    {
        if (IsNaN(value)) throw new OverflowException($"Cannot convert NaN to {nameof(BigDecimal)}.");
        if (IsInfinity(value)) throw new OverflowException($"Cannot convert infinity to {nameof(BigDecimal)}.");
        if (IsZero(value)) return BigDecimal.Zero;

        DecomposeFinite(value.RawBits, out bool sign, out int unbiasedExponent, out UInt256 significand);
        NormalizeSubnormal(ref significand, ref unbiasedExponent);

        int binaryExponent = unbiasedExponent - TrailingSignificandBits;
        BigInteger significandAsBigInteger = (BigInteger)significand;
        if (sign) significandAsBigInteger = -significandAsBigInteger;

        if (binaryExponent >= 0)
        {
            BigInteger unscaledValue = significandAsBigInteger << binaryExponent;
            return new BigDecimal(unscaledValue, 0);
        }

        int absoluteExponent = -binaryExponent;
        BigInteger scaledUnscaledValue = significandAsBigInteger * BigInteger.Pow(5, absoluteExponent);
        return TrimRedundantScale(new BigDecimal(scaledUnscaledValue, absoluteExponent));
    }

    /// <summary>
    /// Removes any redundant trailing zero decimal places from the specified <see cref="BigDecimal"/> by dividing the unscaled value by ten while the remainder is zero.
    /// </summary>
    /// <param name="value">The value whose scale should be minimised.</param>
    /// <returns>Returns a <see cref="BigDecimal"/> equal to <paramref name="value"/> but with the smallest non-negative scale that still represents it exactly.</returns>
    private static BigDecimal TrimRedundantScale(BigDecimal value)
    {
        if (BigDecimal.IsZero(value)) return value;
        if (value.Scale == 0) return value;

        BigInteger remainingUnscaledValue = value.UnscaledValue;
        int currentScale = value.Scale;
        BigInteger ten = (BigInteger)10;

        while (currentScale > 0)
        {
            BigInteger quotient = BigInteger.DivRem(remainingUnscaledValue, ten, out BigInteger remainder);
            if (!remainder.IsZero) break;
            remainingUnscaledValue = quotient;
            currentScale--;
        }

        return new BigDecimal(remainingUnscaledValue, currentScale);
    }

    /// <summary>
    /// Converts the specified <see cref="BigDecimal"/> value to a <see cref="Float256"/>, rounding to nearest, ties-to-even.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the <see cref="Float256"/> value closest to <paramref name="value"/>; saturates to ±infinity on overflow; underflows to signed zero or a subnormal.</returns>
    public static explicit operator Float256(BigDecimal value)
    {
        BigInteger unscaledValue = value.UnscaledValue;
        int scale = value.Scale;

        if (unscaledValue.IsZero) return Zero;

        bool sign = unscaledValue.Sign < 0;
        if (sign) unscaledValue = -unscaledValue;

        int extraBits = ChooseExtraBits(unscaledValue, scale);
        int totalLeftShift = extraBits - scale;

        BigInteger numerator;
        BigInteger denominator;

        if (totalLeftShift >= 0)
        {
            numerator = unscaledValue << totalLeftShift;
            denominator = scale >= 0 ? BigInteger.Pow(5, scale) : BigInteger.One;
        }
        else
        {
            numerator = unscaledValue;
            denominator = scale >= 0
                ? BigInteger.Pow(5, scale) << -totalLeftShift
                : BigInteger.One << -totalLeftShift;
        }

        if (scale < 0)
        {
            numerator *= BigInteger.Pow(5, -scale);
        }

        BigInteger wideSignificand = BigInteger.DivRem(numerator, denominator, out BigInteger remainder);

        if (wideSignificand.IsZero)
        {
            return sign ? NegativeZero : Zero;
        }

        int leadingBitPosition = (int)wideSignificand.GetBitLength() - 1;
        int shiftRight = leadingBitPosition - TrailingSignificandBits;

        UInt256 significand;
        bool roundBit;
        bool stickyBit;

        if (shiftRight > 0)
        {
            BigInteger shifted = wideSignificand >> shiftRight;
            significand = (UInt256)shifted;

            BigInteger roundBitMask = BigInteger.One << (shiftRight - 1);
            roundBit = !(wideSignificand & roundBitMask).IsZero;

            BigInteger stickyMask = roundBitMask - BigInteger.One;
            bool stickyFromShifted = !(wideSignificand & stickyMask).IsZero;
            stickyBit = stickyFromShifted || !remainder.IsZero;
        }
        else if (shiftRight == 0)
        {
            significand = (UInt256)wideSignificand;
            roundBit = false;
            stickyBit = !remainder.IsZero;
        }
        else
        {
            significand = (UInt256)wideSignificand << -shiftRight;
            roundBit = false;
            stickyBit = !remainder.IsZero;
        }

        int unbiasedExponent = leadingBitPosition - extraBits;
        return RoundToNearestEven(sign, unbiasedExponent, significand, roundBit, stickyBit);
    }

    /// <summary>
    /// Chooses the number of guard bits required when scaling a <see cref="BigDecimal"/> to a binary fraction so that the rounded <see cref="Float256"/> result is correctly rounded.
    /// </summary>
    /// <param name="absoluteUnscaledValue">The absolute unscaled integer value of the <see cref="BigDecimal"/> being converted.</param>
    /// <param name="scale">The decimal scale of the <see cref="BigDecimal"/> being converted.</param>
    /// <returns>Returns the number of additional binary bits to retain beyond the <see cref="Float256"/> significand for safe rounding.</returns>
    private static int ChooseExtraBits(BigInteger absoluteUnscaledValue, int scale)
    {
        const double Log2Of10 = 3.3219280948873623d;
        const int MinExtraBits = SignificandPrecision + 16;
        const int SafetyMargin = 16;

        int numeratorBitLength = (int)absoluteUnscaledValue.GetBitLength();
        double approximateLog2Value = (numeratorBitLength - 1) - scale * Log2Of10;
        int approximateBinaryExponent = (int)Math.Floor(approximateLog2Value);

        int required = SignificandPrecision - approximateBinaryExponent + SafetyMargin;
        return Math.Max(MinExtraBits, required);
    }

    /// <summary>
    /// Packs a sign, unbiased exponent, and 237-bit significand into a finite-or-special <see cref="Float256"/>, applying IEEE 754 round-to-nearest, ties-to-even.
    /// </summary>
    /// <param name="sign">The sign of the result.</param>
    /// <param name="unbiasedExponent">The unbiased binary exponent of the would-be normalised result, where the implicit leading bit is at position <see cref="TrailingSignificandBits"/> of the significand.</param>
    /// <param name="significand">A 237-bit significand value with its leading <c>1</c> at position <see cref="TrailingSignificandBits"/> (or all zero, indicating a true zero result). Bits above position <c>237</c> must not be set.</param>
    /// <param name="roundBit">The bit immediately below the least-significant retained bit, accumulated from any prior shifts performed by the caller.</param>
    /// <param name="stickyBit">A boolean OR of every bit below <paramref name="roundBit"/> from the caller's wider intermediate.</param>
    /// <returns>Returns the correctly-rounded <see cref="Float256"/> representation, saturating to <see cref="PositiveInfinity"/> or <see cref="NegativeInfinity"/> on exponent overflow and tapering to subnormal or signed zero on exponent underflow.</returns>
    internal static Float256 RoundToNearestEven(bool sign, int unbiasedExponent, UInt256 significand, bool roundBit, bool stickyBit)
    {
        UInt256 signBit = sign ? SignMask : UInt256.Zero;

        if (significand == UInt256.Zero && !roundBit && !stickyBit)
        {
            return new Float256(signBit);
        }

        if (unbiasedExponent < MinNormalUnbiasedExponent)
        {
            int shift = MinNormalUnbiasedExponent - unbiasedExponent;

            if (shift > TrailingSignificandBits + 1)
            {
                return new Float256(signBit);
            }

            UInt256 droppedMask = (UInt256.One << shift) - UInt256.One;
            UInt256 droppedBits = significand & droppedMask;
            UInt256 newSignificand = significand >> shift;

            UInt256 newRoundMask = UInt256.One << (shift - 1);
            bool newRoundBit = (droppedBits & newRoundMask) != UInt256.Zero;

            UInt256 newStickyMask = newRoundMask - UInt256.One;
            bool newStickyBit = (droppedBits & newStickyMask) != UInt256.Zero || roundBit || stickyBit;

            significand = newSignificand;
            roundBit = newRoundBit;
            stickyBit = newStickyBit;
            unbiasedExponent = MinNormalUnbiasedExponent;
        }

        if (roundBit)
        {
            bool lsb = (significand & UInt256.One) != UInt256.Zero;
            if (stickyBit || lsb)
            {
                significand = significand + UInt256.One;

                if ((significand & (UInt256.One << (TrailingSignificandBits + 1))) != UInt256.Zero)
                {
                    significand = significand >> 1;
                    unbiasedExponent++;
                }
            }
        }

        if (unbiasedExponent > MaxFiniteUnbiasedExponent)
        {
            return new Float256(signBit | BiasedExponentMask);
        }

        UInt256 trailingSignificand = significand & TrailingSignificandMask;

        if (unbiasedExponent == MinNormalUnbiasedExponent && (significand & ImplicitSignificandBit) == UInt256.Zero)
        {
            return new Float256(signBit | trailingSignificand);
        }

        UInt256 biasedExponent = new UInt256(UInt128.Zero, (UInt128)(uint)(unbiasedExponent + ExponentBias)) << BiasedExponentShift;
        return new Float256(signBit | biasedExponent | trailingSignificand);
    }
}
