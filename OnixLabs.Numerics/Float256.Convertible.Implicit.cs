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
    /// <summary>Implicitly converts a <see cref="byte"/> value to a <see cref="Float256"/> value.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the <see cref="Float256"/> representation of <paramref name="value"/>.</returns>
    public static implicit operator Float256(byte value) => FromUInt64(value);

    /// <summary>Implicitly converts an <see cref="sbyte"/> value to a <see cref="Float256"/> value.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the <see cref="Float256"/> representation of <paramref name="value"/>.</returns>
    public static implicit operator Float256(sbyte value) => FromInt64(value);

    /// <summary>Implicitly converts a <see cref="short"/> value to a <see cref="Float256"/> value.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the <see cref="Float256"/> representation of <paramref name="value"/>.</returns>
    public static implicit operator Float256(short value) => FromInt64(value);

    /// <summary>Implicitly converts a <see cref="ushort"/> value to a <see cref="Float256"/> value.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the <see cref="Float256"/> representation of <paramref name="value"/>.</returns>
    public static implicit operator Float256(ushort value) => FromUInt64(value);

    /// <summary>Implicitly converts an <see cref="int"/> value to a <see cref="Float256"/> value.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the <see cref="Float256"/> representation of <paramref name="value"/>.</returns>
    public static implicit operator Float256(int value) => FromInt64(value);

    /// <summary>Implicitly converts a <see cref="uint"/> value to a <see cref="Float256"/> value.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the <see cref="Float256"/> representation of <paramref name="value"/>.</returns>
    public static implicit operator Float256(uint value) => FromUInt64(value);

    /// <summary>Implicitly converts a <see cref="long"/> value to a <see cref="Float256"/> value.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the <see cref="Float256"/> representation of <paramref name="value"/>.</returns>
    public static implicit operator Float256(long value) => FromInt64(value);

    /// <summary>Implicitly converts a <see cref="ulong"/> value to a <see cref="Float256"/> value.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the <see cref="Float256"/> representation of <paramref name="value"/>.</returns>
    public static implicit operator Float256(ulong value) => FromUInt64(value);

    /// <summary>Implicitly converts an <see cref="Int128"/> value to a <see cref="Float256"/> value.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the <see cref="Float256"/> representation of <paramref name="value"/>.</returns>
    public static implicit operator Float256(Int128 value) => FromInt128(value);

    /// <summary>Implicitly converts a <see cref="UInt128"/> value to a <see cref="Float256"/> value.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the <see cref="Float256"/> representation of <paramref name="value"/>.</returns>
    public static implicit operator Float256(UInt128 value) => FromUInt128(value);

    /// <summary>Implicitly converts a <see cref="char"/> value to a <see cref="Float256"/> value.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the <see cref="Float256"/> representation of <paramref name="value"/>.</returns>
    public static implicit operator Float256(char value) => FromUInt64(value);

    /// <summary>Implicitly converts a <see cref="decimal"/> value to a <see cref="Float256"/> value.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the <see cref="Float256"/> representation of <paramref name="value"/>.</returns>
    public static implicit operator Float256(decimal value) => FromDecimal(value);

    /// <summary>Implicitly converts a <see cref="Half"/> value to a <see cref="Float256"/> value.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the <see cref="Float256"/> representation of <paramref name="value"/>.</returns>
    public static implicit operator Float256(Half value) => FromDouble((double)value);

    /// <summary>Implicitly converts a <see cref="float"/> value to a <see cref="Float256"/> value.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the <see cref="Float256"/> representation of <paramref name="value"/>.</returns>
    public static implicit operator Float256(float value) => FromSingle(value);

    /// <summary>Implicitly converts a <see cref="double"/> value to a <see cref="Float256"/> value.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the <see cref="Float256"/> representation of <paramref name="value"/>.</returns>
    public static implicit operator Float256(double value) => FromDouble(value);

    /// <summary>Implicitly converts a <see cref="Float128"/> value to a <see cref="Float256"/> value.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the <see cref="Float256"/> representation of <paramref name="value"/>.</returns>
    public static implicit operator Float256(Float128 value) => FromFloat128(value);

    /// <summary>
    /// Converts the specified <see cref="ulong"/> value to its exact <see cref="Float256"/> representation.
    /// </summary>
    /// <param name="value">The unsigned 64-bit integer value to convert.</param>
    /// <returns>Returns a <see cref="Float256"/> representing the exact value of <paramref name="value"/>.</returns>
    private static Float256 FromUInt64(ulong value)
    {
        if (value == 0UL) return Zero;

        int leadingBitPosition = 63 - (int)ulong.LeadingZeroCount(value);
        ulong trailing = value & ~(1UL << leadingBitPosition);
        UInt256 trailingSignificand = (UInt256)trailing << (TrailingSignificandBits - leadingBitPosition);
        UInt256 biasedExponent = new UInt256(UInt128.Zero, (UInt128)(uint)(leadingBitPosition + ExponentBias)) << BiasedExponentShift;

        return new Float256(biasedExponent | trailingSignificand);
    }

    /// <summary>
    /// Converts the specified <see cref="long"/> value to its exact <see cref="Float256"/> representation, preserving sign.
    /// </summary>
    /// <param name="value">The signed 64-bit integer value to convert.</param>
    /// <returns>Returns a <see cref="Float256"/> representing the exact value of <paramref name="value"/>.</returns>
    private static Float256 FromInt64(long value)
    {
        if (value == 0L) return Zero;

        ulong magnitude = value >= 0L ? (ulong)value : ((ulong)~value) + 1UL;
        Float256 positive = FromUInt64(magnitude);

        return value < 0L ? new Float256(positive.bits | SignMask) : positive;
    }

    /// <summary>
    /// Converts the specified <see cref="UInt128"/> value to its exact <see cref="Float256"/> representation.
    /// </summary>
    /// <param name="value">The unsigned 128-bit integer value to convert.</param>
    /// <returns>Returns a <see cref="Float256"/> representing the exact value of <paramref name="value"/>.</returns>
    private static Float256 FromUInt128(UInt128 value)
    {
        if (value == UInt128.Zero) return Zero;

        int leadingBitPosition = 127 - (int)UInt128.LeadingZeroCount(value);
        UInt128 trailing = value & ~(UInt128.One << leadingBitPosition);
        UInt256 trailingSignificand = (UInt256)trailing << (TrailingSignificandBits - leadingBitPosition);
        UInt256 biasedExponent = new UInt256(UInt128.Zero, (UInt128)(uint)(leadingBitPosition + ExponentBias)) << BiasedExponentShift;

        return new Float256(biasedExponent | trailingSignificand);
    }

    /// <summary>
    /// Converts the specified <see cref="Int128"/> value to its exact <see cref="Float256"/> representation, preserving sign.
    /// </summary>
    /// <param name="value">The signed 128-bit integer value to convert.</param>
    /// <returns>Returns a <see cref="Float256"/> representing the exact value of <paramref name="value"/>.</returns>
    private static Float256 FromInt128(Int128 value)
    {
        if (value == Int128.Zero) return Zero;

        bool sign = value < Int128.Zero;
        // Two's-complement negation in unsigned space handles Int128.MinValue correctly:
        // (UInt128.Zero - (UInt128)Int128.MinValue) wraps to 2^127, the true magnitude.
        UInt128 magnitude = sign ? UInt128.Zero - (UInt128)value : (UInt128)value;
        Float256 positive = FromUInt128(magnitude);

        return sign ? new Float256(positive.bits | SignMask) : positive;
    }

    /// <summary>
    /// Converts the specified <see cref="float"/> value to its exact <see cref="Float256"/> representation, preserving NaN payloads, infinities and subnormals.
    /// </summary>
    /// <param name="value">The <see cref="float"/> value to convert.</param>
    /// <returns>Returns a <see cref="Float256"/> representing the exact value of <paramref name="value"/>.</returns>
    private static Float256 FromSingle(float value)
    {
        const int singleBias = 127;
        const int singleSignificandBits = 23;
        const uint singleMaxBiasedExponent = 0xFFu;
        const uint singleSignificandMask = (1u << singleSignificandBits) - 1u;
        const uint singleQuietNaNBit = 1u << (singleSignificandBits - 1);

        uint singleBits = BitConverter.SingleToUInt32Bits(value);
        UInt256 sign = (UInt256)(singleBits >> 31) << SignShift;
        uint biasedExponent = (singleBits >> singleSignificandBits) & singleMaxBiasedExponent;
        uint trailingSignificand = singleBits & singleSignificandMask;

        if (biasedExponent == singleMaxBiasedExponent)
        {
            if (trailingSignificand == 0u) return new Float256(sign | BiasedExponentMask);

            bool isQuiet = (trailingSignificand & singleQuietNaNBit) != 0u;
            uint payload = trailingSignificand & ~singleQuietNaNBit;
            UInt256 quietBit = isQuiet ? QuietNaNBit : UInt256.Zero;
            UInt256 newTrailing = quietBit | ((UInt256)payload << (TrailingSignificandBits - singleSignificandBits));
            return new Float256(sign | BiasedExponentMask | newTrailing);
        }

        if (biasedExponent == 0u)
        {
            if (trailingSignificand == 0u) return new Float256(sign);
            return FromSubnormal(sign, trailingSignificand, singleSignificandBits, -126);
        }

        int unbiasedExponent = (int)biasedExponent - singleBias;
        UInt256 newBiasedExponent = new UInt256(UInt128.Zero, (UInt128)(uint)(unbiasedExponent + ExponentBias)) << BiasedExponentShift;
        UInt256 newTrailingSignificand = (UInt256)trailingSignificand << (TrailingSignificandBits - singleSignificandBits);

        return new Float256(sign | newBiasedExponent | newTrailingSignificand);
    }

    /// <summary>
    /// Converts the specified <see cref="double"/> value to its exact <see cref="Float256"/> representation, preserving NaN payloads, infinities and subnormals.
    /// </summary>
    /// <param name="value">The <see cref="double"/> value to convert.</param>
    /// <returns>Returns a <see cref="Float256"/> representing the exact value of <paramref name="value"/>.</returns>
    private static Float256 FromDouble(double value)
    {
        const int doubleBias = 1023;
        const int doubleSignificandBits = 52;
        const uint doubleMaxBiasedExponent = 0x7FFu;
        const ulong doubleSignificandMask = (1UL << doubleSignificandBits) - 1UL;
        const ulong doubleQuietNaNBit = 1UL << (doubleSignificandBits - 1);

        ulong doubleBits = BitConverter.DoubleToUInt64Bits(value);
        UInt256 sign = (UInt256)(doubleBits >> 63) << SignShift;
        uint biasedExponent = (uint)((doubleBits >> doubleSignificandBits) & doubleMaxBiasedExponent);
        ulong trailingSignificand = doubleBits & doubleSignificandMask;

        if (biasedExponent == doubleMaxBiasedExponent)
        {
            if (trailingSignificand == 0UL) return new Float256(sign | BiasedExponentMask);

            bool isQuiet = (trailingSignificand & doubleQuietNaNBit) != 0UL;
            ulong payload = trailingSignificand & ~doubleQuietNaNBit;
            UInt256 quietBit = isQuiet ? QuietNaNBit : UInt256.Zero;
            UInt256 newTrailing = quietBit | ((UInt256)payload << (TrailingSignificandBits - doubleSignificandBits));
            return new Float256(sign | BiasedExponentMask | newTrailing);
        }

        if (biasedExponent == 0u)
        {
            if (trailingSignificand == 0UL) return new Float256(sign);
            return FromSubnormal(sign, trailingSignificand, doubleSignificandBits, -1022);
        }

        int unbiasedExponent = (int)biasedExponent - doubleBias;
        UInt256 newBiasedExponent = new UInt256(UInt128.Zero, (UInt128)(uint)(unbiasedExponent + ExponentBias)) << BiasedExponentShift;
        UInt256 newTrailingSignificand = (UInt256)trailingSignificand << (TrailingSignificandBits - doubleSignificandBits);

        return new Float256(sign | newBiasedExponent | newTrailingSignificand);
    }

    /// <summary>
    /// Converts a subnormal source significand to a normalised <see cref="Float256"/> by locating its leading bit and recomputing the exponent.
    /// </summary>
    /// <param name="sign">The pre-shifted sign bit of the source value.</param>
    /// <param name="sourceSignificand">The non-zero trailing significand of the subnormal source value.</param>
    /// <param name="sourceSignificandBits">The number of trailing significand bits of the source format.</param>
    /// <param name="sourceMinNormalExponent">The unbiased exponent of the smallest normal value in the source format.</param>
    /// <returns>Returns a normalised <see cref="Float256"/> equivalent to the subnormal source value.</returns>
    private static Float256 FromSubnormal(UInt256 sign, ulong sourceSignificand, int sourceSignificandBits, int sourceMinNormalExponent)
    {
        int leadingBitPosition = 63 - (int)ulong.LeadingZeroCount(sourceSignificand);
        ulong lowerBits = sourceSignificand & ((1UL << leadingBitPosition) - 1UL);
        int unbiasedExponent = sourceMinNormalExponent - (sourceSignificandBits - leadingBitPosition);
        UInt256 trailingSignificand = (UInt256)lowerBits << (TrailingSignificandBits - leadingBitPosition);
        UInt256 biasedExponent = new UInt256(UInt128.Zero, (UInt128)(uint)(unbiasedExponent + ExponentBias)) << BiasedExponentShift;

        return new Float256(sign | biasedExponent | trailingSignificand);
    }

    /// <summary>
    /// Converts the specified <see cref="Float128"/> value to its exact <see cref="Float256"/> representation, preserving NaN payloads, infinities and subnormals.
    /// </summary>
    /// <param name="value">The <see cref="Float128"/> value to convert.</param>
    /// <returns>Returns a <see cref="Float256"/> representing the exact value of <paramref name="value"/>.</returns>
    /// <remarks>
    /// Every binary128 value is exactly representable in binary256: the wider format has a larger exponent
    /// range and a longer significand, so widening cannot lose information or introduce rounding.
    /// </remarks>
    private static Float256 FromFloat128(Float128 value)
    {
        const int sourceSignificandBits = Float128.TrailingSignificandBits;
        const int sourceBias = Float128.ExponentBias;
        const uint sourceMaxBiasedExponent = (1u << Float128.BiasedExponentBits) - 1u;
        UInt128 sourceQuietNaNBit = UInt128.One << (sourceSignificandBits - 1);
        UInt128 sourceSignificandMask = (UInt128.One << sourceSignificandBits) - UInt128.One;

        UInt128 sourceBits = value.RawBits;
        UInt256 sign = (UInt256)(uint)(sourceBits >> Float128.SignShift) << SignShift;
        uint biasedExponent = (uint)(uint)((sourceBits >> sourceSignificandBits) & sourceMaxBiasedExponent);
        UInt128 trailingSignificand = sourceBits & sourceSignificandMask;

        if (biasedExponent == sourceMaxBiasedExponent)
        {
            if (trailingSignificand == UInt128.Zero) return new Float256(sign | BiasedExponentMask);

            bool isQuiet = (trailingSignificand & sourceQuietNaNBit) != UInt128.Zero;
            UInt128 payload = trailingSignificand & ~sourceQuietNaNBit;
            UInt256 quietBit = isQuiet ? QuietNaNBit : UInt256.Zero;
            UInt256 newTrailing = quietBit | ((UInt256)payload << (TrailingSignificandBits - sourceSignificandBits));
            return new Float256(sign | BiasedExponentMask | newTrailing);
        }

        if (biasedExponent == 0u)
        {
            if (trailingSignificand == UInt128.Zero) return new Float256(sign);
            return FromSubnormalFloat128(sign, trailingSignificand);
        }

        int unbiasedExponent = (int)biasedExponent - sourceBias;
        UInt256 newBiasedExponent = new UInt256(UInt128.Zero, (UInt128)(uint)(unbiasedExponent + ExponentBias)) << BiasedExponentShift;
        UInt256 newTrailingSignificand = (UInt256)trailingSignificand << (TrailingSignificandBits - sourceSignificandBits);

        return new Float256(sign | newBiasedExponent | newTrailingSignificand);
    }

    /// <summary>
    /// Renormalises a subnormal Float128 significand into the Float256 layout, locating the leading bit and adjusting the exponent.
    /// </summary>
    /// <param name="sign">The pre-shifted sign bit of the source value.</param>
    /// <param name="sourceSignificand">The non-zero trailing significand of the subnormal Float128 source value.</param>
    /// <returns>Returns a normalised <see cref="Float256"/> equivalent to the subnormal Float128 source value.</returns>
    private static Float256 FromSubnormalFloat128(UInt256 sign, UInt128 sourceSignificand)
    {
        const int sourceSignificandBits = Float128.TrailingSignificandBits;
        const int sourceMinNormalExponent = 1 - Float128.ExponentBias;

        int leadingBitPosition = 127 - (int)UInt128.LeadingZeroCount(sourceSignificand);
        UInt128 lowerBits = sourceSignificand & ((UInt128.One << leadingBitPosition) - UInt128.One);
        int unbiasedExponent = sourceMinNormalExponent - (sourceSignificandBits - leadingBitPosition);
        UInt256 trailingSignificand = (UInt256)lowerBits << (TrailingSignificandBits - leadingBitPosition);
        UInt256 biasedExponent = new UInt256(UInt128.Zero, (UInt128)(uint)(unbiasedExponent + ExponentBias)) << BiasedExponentShift;

        return new Float256(sign | biasedExponent | trailingSignificand);
    }

    /// <summary>
    /// Converts the specified <see cref="decimal"/> value to a correctly-rounded <see cref="Float256"/> via the exact relationship
    /// <c>value = (low | (mid &lt;&lt; 32) | ((UInt128)high &lt;&lt; 64)) / 10^scale</c>, with sign preserved.
    /// </summary>
    /// <param name="value">The <see cref="decimal"/> value to convert.</param>
    /// <returns>Returns the <see cref="Float256"/> closest to <paramref name="value"/>.</returns>
    /// <remarks>
    /// <see cref="decimal"/>'s 96-bit significand fits losslessly in Float256's 237-bit significand, and
    /// every <c>10^scale</c> for the allowed scale range [0, 28] fits in a UInt128 (10^28 occupies ~94 bits).
    /// Dividing two exactly-represented Float256 values produces a correctly-rounded result per IEEE 754.
    /// </remarks>
    private static Float256 FromDecimal(decimal value)
    {
        if (value == 0m) return Zero;

        Span<int> bits = stackalloc int[4];
        decimal.GetBits(value, bits);
        uint low = (uint)bits[0];
        uint mid = (uint)bits[1];
        uint high = (uint)bits[2];
        uint flags = (uint)bits[3];

        bool sign = (flags & 0x80000000u) != 0u;
        int scale = (int)((flags >> 16) & 0xFFu);

        UInt128 magnitude = ((UInt128)high << 64) | ((UInt128)mid << 32) | low;
        Float256 magnitudeFloat = FromUInt128(magnitude);
        Float256 result = scale == 0 ? magnitudeFloat : magnitudeFloat / PowersOfTen[scale];
        return sign ? new Float256(result.bits | SignMask) : result;
    }

    /// <summary>
    /// The first 72 non-negative powers of ten as <see cref="Float256"/> values, indexed by exponent.
    /// </summary>
    /// <remarks>
    /// Used by <see cref="FromDecimal"/> as exact-divisor lookups and by <see cref="Round(Float256,int,MidpointRounding)"/>
    /// as scale factors. The table extends to <c>10^71</c> because that is the largest power of ten whose binary expansion
    /// fits in Float256's 237-bit significand (10^71 occupies ~236 bits); every entry is therefore stored exactly.
    /// </remarks>
    internal static readonly Float256[] PowersOfTen = ComputePowersOfTen();

    /// <summary>
    /// Computes the lookup table of <c>10^k</c> as <see cref="Float256"/> values for <c>k</c> in the range [0, 71].
    /// </summary>
    /// <returns>Returns an array indexed by exponent, where entry <c>k</c> equals <c>10^k</c> as a <see cref="Float256"/>.</returns>
    /// <remarks>
    /// Entries 0..38 are built via exact <see cref="UInt128"/> multiplication and <see cref="FromUInt128"/>.
    /// Entries 39..71 extend the table by Float256 multiplication: each step <c>10^k * 10</c> is exact while
    /// the product still fits in 237 bits, which holds through <c>10^71</c>.
    /// </remarks>
    private static Float256[] ComputePowersOfTen()
    {
        const int tableSize = 72;
        const int uint128LimitExponent = 38;

        Float256[] result = new Float256[tableSize];
        UInt128 powerU128 = UInt128.One;
        for (int i = 0; i <= uint128LimitExponent; i++)
        {
            result[i] = FromUInt128(powerU128);
            if (i < uint128LimitExponent) powerU128 *= 10U;
        }

        Float256 ten = FromUInt128((UInt128)10U);
        for (int i = uint128LimitExponent + 1; i < tableSize; i++)
        {
            result[i] = result[i - 1] * ten;
        }

        return result;
    }

    /// <summary>
    /// Converts the specified <see cref="BigInteger"/> value to a correctly-rounded <see cref="Float256"/>, saturating to ±infinity for magnitudes that exceed the binary256 range.
    /// </summary>
    /// <param name="value">The <see cref="BigInteger"/> value to convert.</param>
    /// <returns>Returns the <see cref="Float256"/> closest to <paramref name="value"/>.</returns>
    /// <remarks>
    /// Integers with at most 237 significant bits round-trip exactly. Larger integers are rounded to nearest, ties-to-even
    /// using guard and sticky bits accumulated from the bits shifted off the bottom of the 237-bit significand.
    /// </remarks>
    internal static Float256 FromBigInteger(BigInteger value)
    {
        if (value.IsZero) return Zero;

        bool sign = value.Sign < 0;
        BigInteger magnitude = sign ? -value : value;

        int bitLength = (int)magnitude.GetBitLength();
        int unbiasedExponent = bitLength - 1;

        if (unbiasedExponent > MaxFiniteUnbiasedExponent)
        {
            return sign ? NegativeInfinity : PositiveInfinity;
        }

        UInt256 significand;
        bool roundBit;
        bool stickyBit;

        int shift = bitLength - SignificandPrecision;
        if (shift > 0)
        {
            BigInteger shifted = magnitude >> shift;
            significand = (UInt256)shifted;

            BigInteger roundBitMask = BigInteger.One << (shift - 1);
            roundBit = !(magnitude & roundBitMask).IsZero;

            BigInteger stickyMask = roundBitMask - BigInteger.One;
            stickyBit = !(magnitude & stickyMask).IsZero;
        }
        else if (shift == 0)
        {
            significand = (UInt256)magnitude;
            roundBit = false;
            stickyBit = false;
        }
        else
        {
            significand = (UInt256)magnitude << -shift;
            roundBit = false;
            stickyBit = false;
        }

        return RoundToNearestEven(sign, unbiasedExponent, significand, roundBit, stickyBit);
    }
}
