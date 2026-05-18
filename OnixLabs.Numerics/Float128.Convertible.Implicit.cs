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
    /// Performs an implicit conversion from the specified <see cref="byte"/> value to a <see cref="Float128"/> value.
    /// </summary>
    /// <param name="value">The <see cref="byte"/> value to convert.</param>
    /// <returns>Returns a new <see cref="Float128"/> value that represents the specified <see cref="byte"/> value.</returns>
    public static implicit operator Float128(byte value) => FromUInt64(value);

    /// <summary>
    /// Performs an implicit conversion from the specified <see cref="sbyte"/> value to a <see cref="Float128"/> value.
    /// </summary>
    /// <param name="value">The <see cref="sbyte"/> value to convert.</param>
    /// <returns>Returns a new <see cref="Float128"/> value that represents the specified <see cref="sbyte"/> value.</returns>
    public static implicit operator Float128(sbyte value) => FromInt64(value);

    /// <summary>
    /// Performs an implicit conversion from the specified <see cref="short"/> value to a <see cref="Float128"/> value.
    /// </summary>
    /// <param name="value">The <see cref="short"/> value to convert.</param>
    /// <returns>Returns a new <see cref="Float128"/> value that represents the specified <see cref="short"/> value.</returns>
    public static implicit operator Float128(short value) => FromInt64(value);

    /// <summary>
    /// Performs an implicit conversion from the specified <see cref="ushort"/> value to a <see cref="Float128"/> value.
    /// </summary>
    /// <param name="value">The <see cref="ushort"/> value to convert.</param>
    /// <returns>Returns a new <see cref="Float128"/> value that represents the specified <see cref="ushort"/> value.</returns>
    public static implicit operator Float128(ushort value) => FromUInt64(value);

    /// <summary>
    /// Performs an implicit conversion from the specified <see cref="int"/> value to a <see cref="Float128"/> value.
    /// </summary>
    /// <param name="value">The <see cref="int"/> value to convert.</param>
    /// <returns>Returns a new <see cref="Float128"/> value that represents the specified <see cref="int"/> value.</returns>
    public static implicit operator Float128(int value) => FromInt64(value);

    /// <summary>
    /// Performs an implicit conversion from the specified <see cref="uint"/> value to a <see cref="Float128"/> value.
    /// </summary>
    /// <param name="value">The <see cref="uint"/> value to convert.</param>
    /// <returns>Returns a new <see cref="Float128"/> value that represents the specified <see cref="uint"/> value.</returns>
    public static implicit operator Float128(uint value) => FromUInt64(value);

    /// <summary>
    /// Performs an implicit conversion from the specified <see cref="long"/> value to a <see cref="Float128"/> value.
    /// </summary>
    /// <param name="value">The <see cref="long"/> value to convert.</param>
    /// <returns>Returns a new <see cref="Float128"/> value that represents the specified <see cref="long"/> value.</returns>
    public static implicit operator Float128(long value) => FromInt64(value);

    /// <summary>
    /// Performs an implicit conversion from the specified <see cref="ulong"/> value to a <see cref="Float128"/> value.
    /// </summary>
    /// <param name="value">The <see cref="ulong"/> value to convert.</param>
    /// <returns>Returns a new <see cref="Float128"/> value that represents the specified <see cref="ulong"/> value.</returns>
    public static implicit operator Float128(ulong value) => FromUInt64(value);

    /// <summary>
    /// Performs an implicit conversion from the specified <see cref="char"/> value to a <see cref="Float128"/> value.
    /// </summary>
    /// <param name="value">The <see cref="char"/> value to convert.</param>
    /// <returns>Returns a new <see cref="Float128"/> value that represents the specified <see cref="char"/> value.</returns>
    public static implicit operator Float128(char value) => FromUInt64(value);

    /// <summary>
    /// Performs an implicit conversion from the specified <see cref="Half"/> value to a <see cref="Float128"/> value.
    /// </summary>
    /// <param name="value">The <see cref="Half"/> value to convert.</param>
    /// <returns>Returns a new <see cref="Float128"/> value that represents the specified <see cref="Half"/> value.</returns>
    public static implicit operator Float128(Half value) => FromDouble((double)value);

    /// <summary>
    /// Performs an implicit conversion from the specified <see cref="float"/> value to a <see cref="Float128"/> value.
    /// </summary>
    /// <param name="value">The <see cref="float"/> value to convert.</param>
    /// <returns>Returns a new <see cref="Float128"/> value that represents the specified <see cref="float"/> value.</returns>
    public static implicit operator Float128(float value) => FromSingle(value);

    /// <summary>
    /// Performs an implicit conversion from the specified <see cref="double"/> value to a <see cref="Float128"/> value.
    /// </summary>
    /// <param name="value">The <see cref="double"/> value to convert.</param>
    /// <returns>Returns a new <see cref="Float128"/> value that represents the specified <see cref="double"/> value.</returns>
    public static implicit operator Float128(double value) => FromDouble(value);

    /// <summary>
    /// Converts the specified <see cref="ulong"/> value to its exact <see cref="Float128"/> representation.
    /// </summary>
    /// <param name="value">The unsigned 64-bit integer value to convert.</param>
    /// <returns>A <see cref="Float128"/> representing the exact value of <paramref name="value"/>.</returns>
    private static Float128 FromUInt64(ulong value)
    {
        if (value == 0UL) return Zero;

        int leadingBitPosition = 63 - (int)ulong.LeadingZeroCount(value);
        ulong trailing = value & ~(1UL << leadingBitPosition);
        UInt128 trailingSignificand = (UInt128)trailing << (TrailingSignificandBits - leadingBitPosition);
        UInt128 biasedExponent = (UInt128)(uint)(leadingBitPosition + ExponentBias) << BiasedExponentShift;

        return new Float128(biasedExponent | trailingSignificand);
    }

    /// <summary>
    /// Converts the specified <see cref="long"/> value to its exact <see cref="Float128"/> representation, preserving sign.
    /// </summary>
    /// <param name="value">The signed 64-bit integer value to convert.</param>
    /// <returns>A <see cref="Float128"/> representing the exact value of <paramref name="value"/>.</returns>
    private static Float128 FromInt64(long value)
    {
        if (value == 0L) return Zero;

        ulong magnitude = value >= 0L ? (ulong)value : ((ulong)~value) + 1UL;
        Float128 positive = FromUInt64(magnitude);

        return value < 0L ? new Float128(positive.RawBits | SignMask) : positive;
    }

    /// <summary>
    /// Converts the specified <see cref="float"/> value to its exact <see cref="Float128"/> representation, preserving NaN payloads, infinities and subnormals.
    /// </summary>
    /// <param name="value">The <see cref="float"/> value to convert.</param>
    /// <returns>A <see cref="Float128"/> representing the exact value of <paramref name="value"/>.</returns>
    private static Float128 FromSingle(float value)
    {
        const int singleBias = 127;
        const int singleSignificandBits = 23;
        const uint singleMaxBiasedExponent = 0xFFu;
        const uint singleSignificandMask = (1u << singleSignificandBits) - 1u;
        const uint singleQuietNaNBit = 1u << (singleSignificandBits - 1);

        uint singleBits = BitConverter.SingleToUInt32Bits(value);
        UInt128 sign = (UInt128)(singleBits >> 31) << SignShift;
        uint biasedExponent = (singleBits >> singleSignificandBits) & singleMaxBiasedExponent;
        uint trailingSignificand = singleBits & singleSignificandMask;

        if (biasedExponent == singleMaxBiasedExponent)
        {
            if (trailingSignificand == 0u) return new Float128(sign | BiasedExponentMask);

            bool isQuiet = (trailingSignificand & singleQuietNaNBit) != 0u;
            uint payload = trailingSignificand & ~singleQuietNaNBit;
            UInt128 quietBit = isQuiet ? QuietNaNBit : UInt128.Zero;
            UInt128 newTrailing = quietBit | ((UInt128)payload << (TrailingSignificandBits - singleSignificandBits));
            return new Float128(sign | BiasedExponentMask | newTrailing);
        }

        if (biasedExponent == 0u)
        {
            if (trailingSignificand == 0u) return new Float128(sign);
            return FromSubnormal(sign, trailingSignificand, singleSignificandBits, -126);
        }

        int unbiasedExponent = (int)biasedExponent - singleBias;
        UInt128 newBiasedExponent = (UInt128)(uint)(unbiasedExponent + ExponentBias) << BiasedExponentShift;
        UInt128 newTrailingSignificand = (UInt128)trailingSignificand << (TrailingSignificandBits - singleSignificandBits);

        return new Float128(sign | newBiasedExponent | newTrailingSignificand);
    }

    /// <summary>
    /// Converts the specified <see cref="double"/> value to its exact <see cref="Float128"/> representation, preserving NaN payloads, infinities and subnormals.
    /// </summary>
    /// <param name="value">The <see cref="double"/> value to convert.</param>
    /// <returns>A <see cref="Float128"/> representing the exact value of <paramref name="value"/>.</returns>
    private static Float128 FromDouble(double value)
    {
        const int doubleBias = 1023;
        const int doubleSignificandBits = 52;
        const uint doubleMaxBiasedExponent = 0x7FFu;
        const ulong doubleSignificandMask = (1UL << doubleSignificandBits) - 1UL;
        const ulong doubleQuietNaNBit = 1UL << (doubleSignificandBits - 1);

        ulong doubleBits = BitConverter.DoubleToUInt64Bits(value);
        UInt128 sign = (UInt128)(doubleBits >> 63) << SignShift;
        uint biasedExponent = (uint)((doubleBits >> doubleSignificandBits) & doubleMaxBiasedExponent);
        ulong trailingSignificand = doubleBits & doubleSignificandMask;

        if (biasedExponent == doubleMaxBiasedExponent)
        {
            if (trailingSignificand == 0UL) return new Float128(sign | BiasedExponentMask);

            bool isQuiet = (trailingSignificand & doubleQuietNaNBit) != 0UL;
            ulong payload = trailingSignificand & ~doubleQuietNaNBit;
            UInt128 quietBit = isQuiet ? QuietNaNBit : UInt128.Zero;
            UInt128 newTrailing = quietBit | ((UInt128)payload << (TrailingSignificandBits - doubleSignificandBits));
            return new Float128(sign | BiasedExponentMask | newTrailing);
        }

        if (biasedExponent == 0u)
        {
            if (trailingSignificand == 0UL) return new Float128(sign);
            return FromSubnormal(sign, trailingSignificand, doubleSignificandBits, -1022);
        }

        int unbiasedExponent = (int)biasedExponent - doubleBias;
        UInt128 newBiasedExponent = (UInt128)(uint)(unbiasedExponent + ExponentBias) << BiasedExponentShift;
        UInt128 newTrailingSignificand = (UInt128)trailingSignificand << (TrailingSignificandBits - doubleSignificandBits);

        return new Float128(sign | newBiasedExponent | newTrailingSignificand);
    }

    /// <summary>
    /// Converts a subnormal source significand to a normalized <see cref="Float128"/> by locating its leading bit and recomputing the exponent.
    /// </summary>
    /// <param name="sign">The pre-shifted sign bit of the source value.</param>
    /// <param name="sourceSignificand">The non-zero trailing significand of the subnormal source value.</param>
    /// <param name="sourceSignificandBits">The number of trailing significand bits of the source format.</param>
    /// <param name="sourceMinNormalExponent">The unbiased exponent of the smallest normal value in the source format.</param>
    /// <returns>A normalized <see cref="Float128"/> equivalent to the subnormal source value.</returns>
    private static Float128 FromSubnormal(UInt128 sign, ulong sourceSignificand, int sourceSignificandBits, int sourceMinNormalExponent)
    {
        int leadingBitPosition = 63 - (int)ulong.LeadingZeroCount(sourceSignificand);
        ulong lowerBits = sourceSignificand & ((1UL << leadingBitPosition) - 1UL);
        int unbiasedExponent = sourceMinNormalExponent - (sourceSignificandBits - leadingBitPosition);
        UInt128 trailingSignificand = (UInt128)lowerBits << (TrailingSignificandBits - leadingBitPosition);
        UInt128 biasedExponent = (UInt128)(uint)(unbiasedExponent + ExponentBias) << BiasedExponentShift;

        return new Float128(sign | biasedExponent | trailingSignificand);
    }

    /// <summary>
    /// Converts the specified <see cref="UInt128"/> value to a correctly-rounded <see cref="Float128"/>, rounding values whose magnitudes exceed 113 bits.
    /// </summary>
    /// <param name="value">The unsigned 128-bit integer value to convert.</param>
    /// <returns>Returns the <see cref="Float128"/> closest to <paramref name="value"/>.</returns>
    /// <remarks>
    /// Integers whose bit length is at most 113 round-trip exactly; larger integers are rounded to nearest, ties-to-even
    /// using guard and sticky bits accumulated from the bits shifted off the bottom of the 113-bit significand.
    /// </remarks>
    internal static Float128 FromUInt128(UInt128 value)
    {
        if (value == UInt128.Zero) return Zero;

        int leadingBitPosition = 127 - (int)UInt128.LeadingZeroCount(value);
        int unbiasedExponent = leadingBitPosition;

        UInt128 significand;
        bool roundBit;
        bool stickyBit;

        int shift = leadingBitPosition - TrailingSignificandBits;
        if (shift > 0)
        {
            significand = value >> shift;
            UInt128 roundBitMask = UInt128.One << (shift - 1);
            roundBit = (value & roundBitMask) != UInt128.Zero;
            UInt128 stickyMask = roundBitMask - UInt128.One;
            stickyBit = (value & stickyMask) != UInt128.Zero;
        }
        else if (shift == 0)
        {
            significand = value;
            roundBit = false;
            stickyBit = false;
        }
        else
        {
            significand = value << -shift;
            roundBit = false;
            stickyBit = false;
        }

        return RoundToNearestEven(sign: false, unbiasedExponent, significand, roundBit, stickyBit);
    }

    /// <summary>
    /// Converts the specified <see cref="Int128"/> value to a correctly-rounded <see cref="Float128"/>, preserving sign.
    /// </summary>
    /// <param name="value">The signed 128-bit integer value to convert.</param>
    /// <returns>Returns the <see cref="Float128"/> closest to <paramref name="value"/>.</returns>
    internal static Float128 FromInt128(Int128 value)
    {
        if (value == Int128.Zero) return Zero;

        bool sign = value < Int128.Zero;
        // Two's-complement negation in unsigned space handles Int128.MinValue correctly:
        // (UInt128.Zero - (UInt128)Int128.MinValue) wraps to 2^127, the true magnitude.
        UInt128 magnitude = sign ? UInt128.Zero - (UInt128)value : (UInt128)value;
        Float128 positive = FromUInt128(magnitude);

        return sign ? new Float128(positive.RawBits | SignMask) : positive;
    }

    /// <summary>
    /// Converts the specified <see cref="BigInteger"/> value to a correctly-rounded <see cref="Float128"/>, saturating to ±infinity for magnitudes that exceed the binary128 range.
    /// </summary>
    /// <param name="value">The <see cref="BigInteger"/> value to convert.</param>
    /// <returns>Returns the <see cref="Float128"/> closest to <paramref name="value"/>.</returns>
    /// <remarks>
    /// Integers with at most 113 significant bits round-trip exactly. Larger integers are rounded to nearest, ties-to-even
    /// using guard and sticky bits accumulated from the bits shifted off the bottom of the 113-bit significand.
    /// </remarks>
    internal static Float128 FromBigInteger(BigInteger value)
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

        UInt128 significand;
        bool roundBit;
        bool stickyBit;

        int shift = bitLength - SignificandPrecision;
        if (shift > 0)
        {
            BigInteger shifted = magnitude >> shift;
            significand = (UInt128)shifted;

            BigInteger roundBitMask = BigInteger.One << (shift - 1);
            roundBit = !(magnitude & roundBitMask).IsZero;

            BigInteger stickyMask = roundBitMask - BigInteger.One;
            stickyBit = !(magnitude & stickyMask).IsZero;
        }
        else if (shift == 0)
        {
            significand = (UInt128)magnitude;
            roundBit = false;
            stickyBit = false;
        }
        else
        {
            significand = (UInt128)magnitude << -shift;
            roundBit = false;
            stickyBit = false;
        }

        return RoundToNearestEven(sign, unbiasedExponent, significand, roundBit, stickyBit);
    }

    /// <summary>
    /// Converts the specified <see cref="decimal"/> value to a correctly-rounded <see cref="Float128"/> via the exact relationship
    /// <c>value = (low | (mid &lt;&lt; 32) | ((UInt128)high &lt;&lt; 64)) / 10^scale</c>, with sign preserved.
    /// </summary>
    /// <param name="value">The <see cref="decimal"/> value to convert.</param>
    /// <returns>Returns the <see cref="Float128"/> closest to <paramref name="value"/>.</returns>
    /// <remarks>
    /// <see cref="decimal"/>'s 96-bit significand fits losslessly in Float128's 113-bit significand, and every
    /// <c>10^scale</c> for the allowed scale range [0, 28] fits in a UInt128. Dividing two exactly-represented
    /// Float128 values produces a correctly-rounded result per IEEE 754.
    /// </remarks>
    internal static Float128 FromDecimal(decimal value)
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
        Float128 magnitudeFloat = FromUInt128(magnitude);
        Float128 result = scale == 0 ? magnitudeFloat : magnitudeFloat / PowersOfTen[scale];
        return sign ? new Float128(result.RawBits | SignMask) : result;
    }

    /// <summary>
    /// The first 34 non-negative powers of ten as <see cref="Float128"/> values, indexed by exponent.
    /// </summary>
    /// <remarks>
    /// Used by <see cref="FromDecimal"/> as exact-divisor lookups and by <see cref="Round(Float128,int,MidpointRounding)"/>
    /// as scale factors. The table extends to <c>10^33</c> because that is the largest power of ten whose binary expansion
    /// fits in Float128's 113-bit significand (10^33 occupies ~110 bits); every entry is therefore stored exactly.
    /// </remarks>
    internal static readonly Float128[] PowersOfTen = ComputePowersOfTen();

    /// <summary>
    /// Computes the lookup table of <c>10^k</c> as <see cref="Float128"/> values for <c>k</c> in the range [0, 33].
    /// </summary>
    /// <returns>Returns an array indexed by exponent, where entry <c>k</c> equals <c>10^k</c> as a <see cref="Float128"/>.</returns>
    /// <remarks>
    /// Every entry is built via exact <see cref="UInt128"/> multiplication and <see cref="FromUInt128"/>, since
    /// <c>10^33</c> fits in 110 bits — well within UInt128's 128-bit range.
    /// </remarks>
    private static Float128[] ComputePowersOfTen()
    {
        Float128[] result = new Float128[34];
        UInt128 power = UInt128.One;
        for (int i = 0; i < result.Length; i++)
        {
            result[i] = FromUInt128(power);
            if (i < result.Length - 1) power *= 10U;
        }
        return result;
    }
}
