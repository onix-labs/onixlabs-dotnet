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

public readonly partial struct Float128
{
    /// <summary>
    /// Converts the specified finite <see cref="Float128"/> value to a <see cref="BigDecimal"/> exactly.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns a <see cref="BigDecimal"/> whose value equals <paramref name="value"/> exactly.</returns>
    /// <exception cref="OverflowException">Thrown when <paramref name="value"/> is NaN or infinite.</exception>
    public static explicit operator BigDecimal(Float128 value)
    {
        if (IsNaN(value)) throw new OverflowException($"Cannot convert NaN to {nameof(BigDecimal)}.");
        if (IsInfinity(value)) throw new OverflowException($"Cannot convert infinity to {nameof(BigDecimal)}.");
        if (IsZero(value)) return BigDecimal.Zero;

        DecomposeFinite(value.bits, out bool sign, out int unbiasedExponent, out UInt128 significand);
        NormalizeSubnormal(ref significand, ref unbiasedExponent);

        int binaryExponent = unbiasedExponent - TrailingSignificandBits;
        BigInteger significandAsBigInteger = UInt128ToBigInteger(significand);
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
    /// Removes trailing zeros from the unscaled value of the specified <see cref="BigDecimal"/>, reducing its scale without changing the represented number.
    /// </summary>
    /// <param name="value">The <see cref="BigDecimal"/> value to normalize.</param>
    /// <returns>An equivalent <see cref="BigDecimal"/> with redundant scale digits trimmed.</returns>
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
    /// Converts the specified <see cref="BigDecimal"/> value to a <see cref="Float128"/>, rounding to nearest, ties-to-even.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the <see cref="Float128"/> value closest to <paramref name="value"/>; saturates to <see cref="PositiveInfinity"/> or <see cref="NegativeInfinity"/> on overflow; underflows to signed zero or a subnormal.</returns>
    public static explicit operator Float128(BigDecimal value)
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

        UInt128 significand;
        bool roundBit;
        bool stickyBit;

        if (shiftRight > 0)
        {
            BigInteger shifted = wideSignificand >> shiftRight;
            significand = BigIntegerToUInt128(shifted);

            BigInteger roundBitMask = BigInteger.One << (shiftRight - 1);
            roundBit = !(wideSignificand & roundBitMask).IsZero;

            BigInteger stickyMask = roundBitMask - BigInteger.One;
            bool stickyFromShifted = !(wideSignificand & stickyMask).IsZero;
            stickyBit = stickyFromShifted || !remainder.IsZero;
        }
        else if (shiftRight == 0)
        {
            significand = BigIntegerToUInt128(wideSignificand);
            roundBit = false;
            stickyBit = !remainder.IsZero;
        }
        else
        {
            significand = BigIntegerToUInt128(wideSignificand) << -shiftRight;
            roundBit = false;
            stickyBit = !remainder.IsZero;
        }

        int unbiasedExponent = leadingBitPosition - extraBits;
        return RoundToNearestEven(sign, unbiasedExponent, significand, roundBit, stickyBit);
    }

    /// <summary>
    /// Chooses the number of extra precision bits required to convert a <see cref="BigDecimal"/> with the specified magnitude and scale to a correctly-rounded <see cref="Float128"/>.
    /// </summary>
    /// <param name="absoluteUnscaledValue">The absolute value of the <see cref="BigDecimal.UnscaledValue"/>.</param>
    /// <param name="scale">The scale of the <see cref="BigDecimal"/> being converted.</param>
    /// <returns>The number of guard bits to retain beyond the binary128 significand precision during conversion.</returns>
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
    /// Converts the specified <see cref="UInt128"/> value to a non-negative <see cref="BigInteger"/>.
    /// </summary>
    /// <param name="value">The <see cref="UInt128"/> value to convert.</param>
    /// <returns>A <see cref="BigInteger"/> with the same numeric value as <paramref name="value"/>.</returns>
    private static BigInteger UInt128ToBigInteger(UInt128 value)
    {
        ulong high = (ulong)(value >> 64);
        ulong low = (ulong)value;
        return ((BigInteger)high << 64) | low;
    }

    /// <summary>
    /// Converts the low 128 bits of the specified <see cref="BigInteger"/> to a <see cref="UInt128"/>, truncating any higher-order bits.
    /// </summary>
    /// <param name="value">The <see cref="BigInteger"/> value to convert.</param>
    /// <returns>A <see cref="UInt128"/> containing the low 128 bits of <paramref name="value"/>.</returns>
    private static UInt128 BigIntegerToUInt128(BigInteger value)
    {
        UInt128 result = UInt128.Zero;
        BigInteger remainingBits = value;
        for (int shift = 0; shift < 128 && !remainingBits.IsZero; shift += 64)
        {
            ulong chunk = (ulong)(remainingBits & (BigInteger)ulong.MaxValue);
            result |= (UInt128)chunk << shift;
            remainingBits >>= 64;
        }
        return result;
    }
}
