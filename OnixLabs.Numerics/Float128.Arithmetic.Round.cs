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
    /// Rounds the specified <see cref="Float128"/> value to the nearest integer, using the round-half-to-even (banker's) rule for ties.
    /// </summary>
    /// <param name="value">The value to round.</param>
    /// <returns>Returns the value rounded to the nearest integer; NaN, ±infinity and ±zero are returned unchanged.</returns>
    public static Float128 Round(Float128 value) => Round(value, MidpointRounding.ToEven);

    /// <summary>
    /// Rounds the specified <see cref="Float128"/> value to the specified number of fractional digits, using the round-half-to-even (banker's) rule for ties.
    /// </summary>
    /// <param name="value">The value to round.</param>
    /// <param name="digits">The number of fractional digits to retain when positive, or the negative number of integral digits to clear when negative.</param>
    /// <returns>Returns the value rounded to the specified number of digits.</returns>
    public static Float128 Round(Float128 value, int digits) => Round(value, digits, MidpointRounding.ToEven);

    /// <summary>
    /// Rounds the specified <see cref="Float128"/> value to the specified number of fractional digits using the specified midpoint-rounding strategy.
    /// </summary>
    /// <param name="value">The value to round.</param>
    /// <param name="digits">The number of fractional digits to retain when positive, or the negative number of integral digits to clear when negative.</param>
    /// <param name="mode">The strategy to use when rounding values that are exactly midway between two representable results.</param>
    /// <returns>Returns the value rounded to the specified number of digits.</returns>
    public static Float128 Round(Float128 value, int digits, MidpointRounding mode)
    {
        if (!IsFinite(value)) return value;
        if (IsZero(value)) return value;
        if (digits == 0) return Round(value, mode);

        // Decimal rounding is performed via BigDecimal to guarantee mathematically correct
        // decimal-digit semantics. A native pow10-based approach would introduce double-rounding
        // (once when scaling by 10^digits, once when dividing back) and disagree with the
        // exact decimal answer on edge cases. Integer-valued rounding (digits == 0) above stays native.
        BigDecimal exact = (BigDecimal)value;

        if (digits > 0)
        {
            BigDecimal rounded = exact.Scale > digits ? exact.SetScale(digits, mode) : exact;
            return (Float128)rounded;
        }

        int absoluteDigits = -digits;
        BigDecimal shifted = new(exact.UnscaledValue, exact.Scale + absoluteDigits);
        BigDecimal roundedShifted = shifted.SetScale(0, mode);
        System.Numerics.BigInteger paddedUnscaledValue = roundedShifted.UnscaledValue * System.Numerics.BigInteger.Pow(10, absoluteDigits);
        return (Float128)new BigDecimal(paddedUnscaledValue, 0);
    }

    /// <summary>
    /// Rounds the specified <see cref="Float128"/> value to the nearest integer using the specified midpoint-rounding strategy.
    /// </summary>
    /// <param name="value">The value to round.</param>
    /// <param name="mode">The strategy to use when rounding values that are exactly midway between two integers.</param>
    /// <returns>Returns the value rounded to the nearest integer; NaN, ±infinity and ±zero are returned unchanged.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="mode"/> is not a defined <see cref="MidpointRounding"/> value.</exception>
    public static Float128 Round(Float128 value, MidpointRounding mode) => mode switch
    {
        MidpointRounding.ToEven => RoundHalfToEven(value),
        MidpointRounding.AwayFromZero => RoundHalfAwayFromZero(value),
        MidpointRounding.ToZero => Truncate(value),
        MidpointRounding.ToNegativeInfinity => Floor(value),
        MidpointRounding.ToPositiveInfinity => Ceiling(value),
        _ => throw new ArgumentOutOfRangeException(nameof(mode))
    };

    /// <summary>
    /// Rounds the specified <see cref="Float128"/> value to the nearest integer, with ties breaking to the value whose least significant bit is even.
    /// </summary>
    /// <param name="value">The <see cref="Float128"/> value to round.</param>
    /// <returns>The value of <paramref name="value"/> rounded to the nearest integer using banker's rounding.</returns>
    private static Float128 RoundHalfToEven(Float128 value)
    {
        if (!TryStartRound(value, out RoundingContext context)) return context.EarlyResult;

        bool tieBreakerKeepsLsb = context.LsbBitOfTruncated;
        return ApplyRounding(context, roundUp: context.RoundBit && (context.StickyBit || tieBreakerKeepsLsb));
    }

    /// <summary>
    /// Rounds the specified <see cref="Float128"/> value to the nearest integer, with ties breaking away from zero.
    /// </summary>
    /// <param name="value">The <see cref="Float128"/> value to round.</param>
    /// <returns>The value of <paramref name="value"/> rounded to the nearest integer, with halves moved away from zero.</returns>
    private static Float128 RoundHalfAwayFromZero(Float128 value)
    {
        if (!TryStartRound(value, out RoundingContext context)) return context.EarlyResult;
        return ApplyRounding(context, roundUp: context.RoundBit);
    }

    /// <summary>
    /// Prepares a <see cref="RoundingContext"/> for the specified <see cref="Float128"/> value, handling special cases and computing the round and sticky bits.
    /// </summary>
    /// <param name="value">The <see cref="Float128"/> value being rounded.</param>
    /// <param name="context">When this method returns, contains either the rounding context to apply or an early-exit result.</param>
    /// <returns><see langword="true"/> if rounding should proceed; otherwise, <see langword="false"/> when an early-exit result is supplied in <paramref name="context"/>.</returns>
    private static bool TryStartRound(Float128 value, out RoundingContext context)
    {
        context = default;
        UInt128 bits = value.bits;

        if (!IsFinite(value))
        {
            context = new RoundingContext { EarlyResult = value, EarlyExit = true };
            return false;
        }

        if (IsZero(value))
        {
            context = new RoundingContext { EarlyResult = value, EarlyExit = true };
            return false;
        }

        uint biasedExponent = ExtractBiasedExponent(bits);
        UInt128 sign = bits & SignMask;

        if (biasedExponent == 0u)
        {
            context = new RoundingContext { EarlyResult = new Float128(sign), EarlyExit = true };
            return false;
        }

        int unbiasedExponent = (int)biasedExponent - ExponentBias;

        if (unbiasedExponent >= TrailingSignificandBits)
        {
            context = new RoundingContext { EarlyResult = value, EarlyExit = true };
            return false;
        }

        if (unbiasedExponent < -1)
        {
            context = new RoundingContext { EarlyResult = new Float128(sign), EarlyExit = true };
            return false;
        }

        UInt128 trailingSignificand = ExtractTrailingSignificand(bits);

        if (unbiasedExponent == -1)
        {
            bool nonZeroFraction = trailingSignificand != UInt128.Zero;
            context = new RoundingContext
            {
                Sign = sign,
                Truncated = new Float128(sign),
                RoundBit = true,
                StickyBit = nonZeroFraction,
                LsbBitOfTruncated = false,
                IsUnitOnIncrement = true
            };
            return true;
        }

        int lsbPosition = TrailingSignificandBits - unbiasedExponent;
        int roundPosition = lsbPosition - 1;
        UInt128 fractionMask = (UInt128.One << lsbPosition) - UInt128.One;
        UInt128 stickyMask = (UInt128.One << roundPosition) - UInt128.One;
        UInt128 truncatedBits = bits & ~fractionMask;
        bool roundBit = (trailingSignificand & (UInt128.One << roundPosition)) != UInt128.Zero;
        bool stickyBit = (trailingSignificand & stickyMask) != UInt128.Zero;
        bool lsbBit = unbiasedExponent == 0
            ? true
            : (trailingSignificand & (UInt128.One << lsbPosition)) != UInt128.Zero;

        context = new RoundingContext
        {
            Sign = sign,
            Truncated = new Float128(truncatedBits),
            RoundBit = roundBit,
            StickyBit = stickyBit,
            LsbBitOfTruncated = lsbBit,
            IsUnitOnIncrement = false
        };
        return true;
    }

    /// <summary>
    /// Applies the rounding decision encoded in the specified <see cref="RoundingContext"/>, returning either the truncated or the incremented integer value.
    /// </summary>
    /// <param name="context">The rounding context produced by <see cref="TryStartRound"/>.</param>
    /// <param name="roundUp">A value indicating whether the magnitude should be incremented.</param>
    /// <returns>The <see cref="Float128"/> integer value obtained after applying the rounding decision.</returns>
    private static Float128 ApplyRounding(RoundingContext context, bool roundUp)
    {
        if (!roundUp) return context.Truncated;

        if (context.IsUnitOnIncrement) return new Float128(context.Sign | One.bits);

        UInt128 absoluteTruncatedBits = context.Truncated.bits & ~SignMask;
        if (absoluteTruncatedBits == UInt128.Zero) return new Float128(context.Sign | One.bits);

        UInt128 incrementedAbsolute = IncrementIntegerMagnitudeBits(absoluteTruncatedBits);
        return new Float128(incrementedAbsolute | context.Sign);
    }

    /// <summary>
    /// Represents the intermediate state required to round a <see cref="Float128"/> value to an integer.
    /// </summary>
    private struct RoundingContext
    {
        /// <summary>
        /// The <see cref="Float128"/> value to return immediately when no rounding work is required.
        /// </summary>
        public Float128 EarlyResult;

        /// <summary>
        /// A value indicating whether <see cref="EarlyResult"/> should be returned without further rounding.
        /// </summary>
        public bool EarlyExit;

        /// <summary>
        /// The sign bit of the original value, preserved across rounding.
        /// </summary>
        public UInt128 Sign;

        /// <summary>
        /// The <see cref="Float128"/> value formed by clearing the fractional bits of the original value.
        /// </summary>
        public Float128 Truncated;

        /// <summary>
        /// The IEEE 754 round bit immediately below the integer position.
        /// </summary>
        public bool RoundBit;

        /// <summary>
        /// The IEEE 754 sticky bit summarizing all fractional bits below the round bit.
        /// </summary>
        public bool StickyBit;

        /// <summary>
        /// The least significant bit of <see cref="Truncated"/> at the integer position, used for ties-to-even tie breaking.
        /// </summary>
        public bool LsbBitOfTruncated;

        /// <summary>
        /// A value indicating whether incrementing the magnitude collapses the result to one rather than incrementing the significand.
        /// </summary>
        public bool IsUnitOnIncrement;
    }
}
