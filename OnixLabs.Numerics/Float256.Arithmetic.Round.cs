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
    /// <summary>Rounds the specified <see cref="Float256"/> value to the nearest integer using round-half-to-even.</summary>
    /// <param name="value">The value to round.</param>
    /// <returns>Returns the value rounded to the nearest integer.</returns>
    public static Float256 Round(Float256 value) => Round(value, MidpointRounding.ToEven);

    /// <summary>Rounds the specified <see cref="Float256"/> value to the nearest integer using the specified rounding mode.</summary>
    /// <param name="value">The value to round.</param>
    /// <param name="mode">The rounding mode.</param>
    /// <returns>Returns the value rounded to the nearest integer.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="mode"/> is not a defined <see cref="MidpointRounding"/> value.</exception>
    public static Float256 Round(Float256 value, MidpointRounding mode) => mode switch
    {
        MidpointRounding.ToEven => RoundHalfToEven(value),
        MidpointRounding.AwayFromZero => RoundHalfAwayFromZero(value),
        MidpointRounding.ToZero => Truncate(value),
        MidpointRounding.ToNegativeInfinity => Floor(value),
        MidpointRounding.ToPositiveInfinity => Ceiling(value),
        _ => throw new ArgumentOutOfRangeException(nameof(mode))
    };

    /// <summary>Rounds the specified <see cref="Float256"/> value to the specified number of fractional digits using round-half-to-even.</summary>
    /// <param name="value">The value to round.</param>
    /// <param name="digits">The number of fractional digits to retain.</param>
    /// <returns>Returns the value rounded to the specified number of digits.</returns>
    public static Float256 Round(Float256 value, int digits) => Round(value, digits, MidpointRounding.ToEven);

    /// <summary>Rounds the specified <see cref="Float256"/> value to the specified number of fractional digits using the specified rounding mode.</summary>
    /// <param name="value">The value to round.</param>
    /// <param name="digits">The number of fractional digits to retain when positive, or the negative number of integer digits to clear when negative.</param>
    /// <param name="mode">The rounding mode.</param>
    /// <returns>Returns the value rounded to the specified number of digits.</returns>
    /// <remarks>
    /// Rounds natively via the identity <c>round(value * 10^d) / 10^d</c> using the precomputed exact
    /// <see cref="PowersOfTen"/> table for <c>|d| ≤ 71</c>. Decimal scaling adds up to two round-to-nearest
    /// steps (the multiply and the divide-back), so the result can differ from the mathematically-exact
    /// decimal-rounded value by at most ~2 ULPs — a tighter bound than is typical for binary float rounding.
    /// </remarks>
    public static Float256 Round(Float256 value, int digits, MidpointRounding mode)
    {
        if (!IsFinite(value) || IsZero(value)) return value;
        if (digits == 0) return Round(value, mode);
        if (digits == int.MinValue) return IsNegative(value) ? NegativeZero : Zero;

        bool negative = digits < 0;
        int magnitude = negative ? -digits : digits;

        Float256 scale = PowerOfTenForRound(magnitude);

        if (IsPositiveInfinity(scale))
        {
            // Scale exceeds Float256's range. For positive digits the precision is long exhausted
            // (the value cannot resolve a 10^-digits step) so rounding is a no-op. For negative digits
            // the granularity exceeds every finite Float256 magnitude, so every value rounds to zero.
            return negative ? (IsNegative(value) ? NegativeZero : Zero) : value;
        }

        if (negative)
        {
            Float256 reduced = value / scale;
            Float256 rounded = Round(reduced, mode);
            return rounded * scale;
        }

        Float256 scaled = value * scale;
        // If the multiplication overflows then the value is already coarser than the requested
        // rounding step, so return the input unchanged.
        if (!IsFinite(scaled)) return value;
        Float256 roundedScaled = Round(scaled, mode);
        return roundedScaled / scale;
    }

    /// <summary>
    /// Returns <c>10^magnitude</c> as a <see cref="Float256"/>, using the exact precomputed table when <paramref name="magnitude"/> fits,
    /// otherwise falling back to <see cref="Pow(Float256,Float256)"/>.
    /// </summary>
    /// <param name="magnitude">The non-negative power of ten to compute.</param>
    /// <returns>Returns <c>10^magnitude</c>, which can be <see cref="PositiveInfinity"/> when the result overflows the Float256 range.</returns>
    private static Float256 PowerOfTenForRound(int magnitude)
    {
        if (magnitude < PowersOfTen.Length) return PowersOfTen[magnitude];
        return Pow(PowersOfTen[1], (Float256)magnitude);
    }

    /// <summary>
    /// Rounds the specified <see cref="Float256"/> value to the nearest integer using IEEE 754 round-half-to-even (banker's rounding).
    /// </summary>
    /// <param name="value">The value to round.</param>
    /// <returns>Returns <paramref name="value"/> rounded to the nearest integer, breaking ties by rounding toward the even integer.</returns>
    private static Float256 RoundHalfToEven(Float256 value)
    {
        if (!TryStartRound(value, out RoundingContext context)) return context.EarlyResult;

        bool tieBreakerKeepsLsb = context.LsbBitOfTruncated;
        return ApplyRounding(context, roundUp: context.RoundBit && (context.StickyBit || tieBreakerKeepsLsb));
    }

    /// <summary>
    /// Rounds the specified <see cref="Float256"/> value to the nearest integer, breaking ties by rounding away from zero.
    /// </summary>
    /// <param name="value">The value to round.</param>
    /// <returns>Returns <paramref name="value"/> rounded to the nearest integer, with halfway cases rounded toward the larger magnitude.</returns>
    private static Float256 RoundHalfAwayFromZero(Float256 value)
    {
        if (!TryStartRound(value, out RoundingContext context)) return context.EarlyResult;
        return ApplyRounding(context, roundUp: context.RoundBit);
    }

    /// <summary>
    /// Decomposes the specified value into the round, sticky, and truncation context required by the integer rounding helpers, handling specials and out-of-range exponents up front.
    /// </summary>
    /// <param name="value">The value to prepare for rounding.</param>
    /// <param name="context">When this method returns, contains the populated <see cref="RoundingContext"/>; if rounding can be short-circuited, the context carries the early result.</param>
    /// <returns>Returns <see langword="true"/> if the caller should apply rounding to <paramref name="context"/>; <see langword="false"/> if the context already holds the final result.</returns>
    private static bool TryStartRound(Float256 value, out RoundingContext context)
    {
        context = default;
        UInt256 bits = value.RawBits;

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
        UInt256 sign = bits & SignMask;

        if (biasedExponent == 0u)
        {
            context = new RoundingContext { EarlyResult = new Float256(sign), EarlyExit = true };
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
            context = new RoundingContext { EarlyResult = new Float256(sign), EarlyExit = true };
            return false;
        }

        UInt256 trailingSignificand = ExtractTrailingSignificand(bits);

        if (unbiasedExponent == -1)
        {
            bool nonZeroFraction = !UInt256.IsZero(trailingSignificand);
            context = new RoundingContext
            {
                Sign = sign,
                Truncated = new Float256(sign),
                RoundBit = true,
                StickyBit = nonZeroFraction,
                LsbBitOfTruncated = false,
                IsUnitOnIncrement = true
            };
            return true;
        }

        int lsbPosition = TrailingSignificandBits - unbiasedExponent;
        int roundPosition = lsbPosition - 1;
        UInt256 fractionMask = (UInt256.One << lsbPosition) - UInt256.One;
        UInt256 stickyMask = (UInt256.One << roundPosition) - UInt256.One;
        UInt256 truncatedBits = bits & ~fractionMask;
        bool roundBit = (trailingSignificand & (UInt256.One << roundPosition)) != UInt256.Zero;
        bool stickyBit = (trailingSignificand & stickyMask) != UInt256.Zero;
        bool lsbBit = unbiasedExponent == 0
            ? true
            : (trailingSignificand & (UInt256.One << lsbPosition)) != UInt256.Zero;

        context = new RoundingContext
        {
            Sign = sign,
            Truncated = new Float256(truncatedBits),
            RoundBit = roundBit,
            StickyBit = stickyBit,
            LsbBitOfTruncated = lsbBit,
            IsUnitOnIncrement = false
        };
        return true;
    }

    /// <summary>
    /// Applies the rounding decision computed by the caller to the truncated value stored in the specified <see cref="RoundingContext"/>.
    /// </summary>
    /// <param name="context">The rounding context produced by <see cref="TryStartRound"/>.</param>
    /// <param name="roundUp">A value indicating whether to step up to the next integer magnitude; otherwise, the truncated value is returned.</param>
    /// <returns>Returns the truncated value, or the value incremented by one unit at its integer least-significant bit when <paramref name="roundUp"/> is <see langword="true"/>.</returns>
    private static Float256 ApplyRounding(RoundingContext context, bool roundUp)
    {
        if (!roundUp) return context.Truncated;

        if (context.IsUnitOnIncrement) return new Float256(context.Sign | One.RawBits);

        UInt256 absoluteTruncatedBits = context.Truncated.RawBits & ~SignMask;
        if (UInt256.IsZero(absoluteTruncatedBits)) return new Float256(context.Sign | One.RawBits);

        UInt256 incrementedAbsolute = IncrementIntegerMagnitudeBits(absoluteTruncatedBits);
        return new Float256(incrementedAbsolute | context.Sign);
    }

    /// <summary>
    /// Returns the bit pattern of an integer-valued <see cref="Float256"/> incremented by one unit at its integer least-significant bit.
    /// </summary>
    /// <param name="absoluteIntegerBits">The raw bit pattern of a non-negative integer-valued <see cref="Float256"/> (sign bit cleared).</param>
    /// <returns>Returns the bit pattern of the incremented value. Carries propagate naturally through the trailing significand into the biased exponent, automatically promoting powers of two.</returns>
    private static UInt256 IncrementIntegerMagnitudeBits(UInt256 absoluteIntegerBits)
    {
        uint biasedExponent = (uint)((absoluteIntegerBits & BiasedExponentMask) >> BiasedExponentShift).Lower;

        if (biasedExponent == 0u) return One.RawBits;

        int unbiasedExponent = (int)biasedExponent - ExponentBias;

        if (unbiasedExponent > TrailingSignificandBits) return absoluteIntegerBits;

        int lsbPosition = TrailingSignificandBits - unbiasedExponent;
        UInt256 lsbBit = UInt256.One << lsbPosition;

        return absoluteIntegerBits + lsbBit;
    }

    /// <summary>
    /// Represents the state carried between <see cref="TryStartRound"/> and <see cref="ApplyRounding"/> when rounding a <see cref="Float256"/> to an integer.
    /// </summary>
    private struct RoundingContext
    {
        /// <summary>
        /// The pre-computed result returned when <see cref="TryStartRound"/> short-circuits rounding.
        /// </summary>
        public Float256 EarlyResult;

        /// <summary>
        /// A value indicating whether <see cref="EarlyResult"/> should be returned directly without further rounding.
        /// </summary>
        public bool EarlyExit;

        /// <summary>
        /// The sign-bit pattern of the value being rounded.
        /// </summary>
        public UInt256 Sign;

        /// <summary>
        /// The value truncated toward zero at the integer position, before applying any rounding step.
        /// </summary>
        public Float256 Truncated;

        /// <summary>
        /// The IEEE 754 round bit immediately below the integer least-significant bit.
        /// </summary>
        public bool RoundBit;

        /// <summary>
        /// The IEEE 754 sticky bit aggregating every bit below the round bit.
        /// </summary>
        public bool StickyBit;

        /// <summary>
        /// The least-significant retained bit of the truncated value, used to break ties in round-half-to-even.
        /// </summary>
        public bool LsbBitOfTruncated;

        /// <summary>
        /// A value indicating whether incrementing the magnitude produces the value one rather than incrementing the integer least-significant bit.
        /// </summary>
        public bool IsUnitOnIncrement;
    }
}
