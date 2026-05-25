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
    /// <remarks>
    /// Rounds natively via the identity <c>round(value * 10^d) / 10^d</c> using the precomputed exact
    /// <see cref="PowersOfTen"/> table for <c>|d| ≤ 33</c>. Decimal scaling adds up to two round-to-nearest
    /// steps (the multiply and the divide-back), so the result can differ from the mathematically-exact
    /// decimal-rounded value by at most ~2 ULPs — a tighter bound than is typical for binary float rounding.
    /// </remarks>
    public static Float128 Round(Float128 value, int digits, MidpointRounding mode)
    {
        if (!IsFinite(value) || IsZero(value)) return value;
        if (digits == 0) return Round(value, mode);
        if (digits == int.MinValue) return IsNegative(value) ? NegativeZero : Zero;

        bool negative = digits < 0;
        int magnitude = negative ? -digits : digits;

        Float128 scale = PowerOfTenForRound(magnitude);

        if (IsPositiveInfinity(scale))
        {
            // Scale exceeds Float128's range. For positive digits the precision is long exhausted
            // (the value cannot resolve a 10^-digits step) so rounding is a no-op. For negative digits
            // the granularity exceeds every finite Float128 magnitude, so every value rounds to zero.
            if (!negative) return value;

            return IsNegative(value) ? NegativeZero : Zero;
        }

        if (negative) return Round(value / scale, mode) * scale;

        Float128 scaled = value * scale;

        // If the multiplication overflows then the value is already coarser than the requested
        // rounding step, so return the input unchanged.
        if (!IsFinite(scaled)) return value;

        return Round(scaled, mode) / scale;
    }

    /// <summary>
    /// Returns <c>10^magnitude</c> as a <see cref="Float128"/>, using the exact precomputed table when <paramref name="magnitude"/> fits,
    /// otherwise falling back to <see cref="Pow(Float128,Float128)"/>.
    /// </summary>
    /// <param name="magnitude">The non-negative power of ten to compute.</param>
    /// <returns>Returns <c>10^magnitude</c>, which can be <see cref="PositiveInfinity"/> when the result overflows the Float128 range.</returns>
    private static Float128 PowerOfTenForRound(int magnitude)
    {
        if (magnitude < PowersOfTen.Length) return PowersOfTen[magnitude];

        return Pow(PowersOfTen[1], magnitude);
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
    /// <returns>Returns the value of <paramref name="value"/> rounded to the nearest integer using banker's rounding.</returns>
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
    /// <returns>Returns the value of <paramref name="value"/> rounded to the nearest integer, with halves moved away from zero.</returns>
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
    /// <returns>Returns <see langword="true"/> if rounding should proceed; otherwise, <see langword="false"/> when an early-exit result is supplied in <paramref name="context"/>.</returns>
    private static bool TryStartRound(Float128 value, out RoundingContext context)
    {
        context = default;
        UInt128 bits = value.Bits;

        if (!IsFinite(value) || IsZero(value))
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
        bool lsbBit = unbiasedExponent == 0 || (trailingSignificand & (UInt128.One << lsbPosition)) != UInt128.Zero;

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
    /// <returns>Returns the <see cref="Float128"/> integer value obtained after applying the rounding decision.</returns>
    private static Float128 ApplyRounding(RoundingContext context, bool roundUp)
    {
        if (!roundUp) return context.Truncated;

        if (context.IsUnitOnIncrement) return new Float128(context.Sign | One.Bits);

        UInt128 absoluteTruncatedBits = context.Truncated.Bits & ~SignMask;
        if (absoluteTruncatedBits == UInt128.Zero) return new Float128(context.Sign | One.Bits);

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
