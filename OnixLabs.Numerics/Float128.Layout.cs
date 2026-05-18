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
    /// Gets the sign bit of the specified IEEE 754 binary128 bit pattern as a boolean value.
    /// </summary>
    /// <param name="bits">The IEEE 754 binary128 bit pattern from which to extract the sign.</param>
    /// <returns>Returns <see langword="true"/> if the sign bit is set; otherwise, <see langword="false"/>.</returns>
    internal static bool ExtractSignBit(UInt128 bits) => (bits & SignMask) != UInt128.Zero;

    /// <summary>
    /// Gets the biased exponent of the specified IEEE 754 binary128 bit pattern.
    /// </summary>
    /// <param name="bits">The IEEE 754 binary128 bit pattern from which to extract the biased exponent.</param>
    /// <returns>Returns the 15-bit biased exponent as an unsigned integer in the range [0, 32767].</returns>
    internal static uint ExtractBiasedExponent(UInt128 bits) => (uint)((bits & BiasedExponentMask) >> BiasedExponentShift);

    /// <summary>
    /// Gets the trailing significand of the specified IEEE 754 binary128 bit pattern.
    /// </summary>
    /// <param name="bits">The IEEE 754 binary128 bit pattern from which to extract the trailing significand.</param>
    /// <returns>Returns the 112-bit trailing significand, with the implicit leading bit omitted.</returns>
    internal static UInt128 ExtractTrailingSignificand(UInt128 bits) => bits & TrailingSignificandMask;

    /// <summary>
    /// Determines whether the specified bit pattern encodes a zero (positive or negative).
    /// </summary>
    /// <param name="bits">The IEEE 754 binary128 bit pattern to inspect.</param>
    /// <returns>Returns <see langword="true"/> if the bit pattern encodes a zero; otherwise, <see langword="false"/>.</returns>
    internal static bool IsZeroBits(UInt128 bits) => (bits & ~SignMask) == UInt128.Zero;

    /// <summary>
    /// Determines whether the specified bit pattern encodes positive or negative infinity.
    /// </summary>
    /// <param name="bits">The IEEE 754 binary128 bit pattern to inspect.</param>
    /// <returns>Returns <see langword="true"/> if the bit pattern encodes infinity; otherwise, <see langword="false"/>.</returns>
    internal static bool IsInfinityBits(UInt128 bits) => (bits & ~SignMask) == BiasedExponentMask;

    /// <summary>
    /// Determines whether the specified bit pattern encodes a not-a-number (NaN) value.
    /// </summary>
    /// <param name="bits">The IEEE 754 binary128 bit pattern to inspect.</param>
    /// <returns>Returns <see langword="true"/> if the bit pattern encodes a NaN value; otherwise, <see langword="false"/>.</returns>
    internal static bool IsNaNBits(UInt128 bits) => (bits & ~SignMask) > BiasedExponentMask;

    /// <summary>
    /// Determines whether the specified bit pattern encodes a finite value.
    /// </summary>
    /// <param name="bits">The IEEE 754 binary128 bit pattern to inspect.</param>
    /// <returns>Returns <see langword="true"/> if the bit pattern encodes a finite value; otherwise, <see langword="false"/>.</returns>
    internal static bool IsFiniteBits(UInt128 bits) => ExtractBiasedExponent(bits) != MaxBiasedExponent;

    /// <summary>
    /// Determines whether the specified bit pattern encodes a normal value.
    /// </summary>
    /// <param name="bits">The IEEE 754 binary128 bit pattern to inspect.</param>
    /// <returns>Returns <see langword="true"/> if the bit pattern encodes a normal value; otherwise, <see langword="false"/>.</returns>
    internal static bool IsNormalBits(UInt128 bits)
    {
        uint biased = ExtractBiasedExponent(bits);
        return biased > 0u && biased < MaxBiasedExponent;
    }

    /// <summary>
    /// Determines whether the specified bit pattern encodes a subnormal, non-zero value.
    /// </summary>
    /// <param name="bits">The IEEE 754 binary128 bit pattern to inspect.</param>
    /// <returns>Returns <see langword="true"/> if the bit pattern encodes a subnormal, non-zero value; otherwise, <see langword="false"/>.</returns>
    internal static bool IsSubnormalBits(UInt128 bits) => ExtractBiasedExponent(bits) == 0u && ExtractTrailingSignificand(bits) != UInt128.Zero;

    /// <summary>
    /// Returns the bit pattern of an integer-valued <see cref="Float128"/> incremented by one unit at its integer least-significant bit.
    /// </summary>
    /// <param name="absoluteIntegerBits">The raw bit pattern of a non-negative integer-valued <see cref="Float128"/> (sign bit cleared).</param>
    /// <returns>Returns the bit pattern of the incremented value. Carries propagate naturally through the trailing significand into the biased exponent, automatically promoting powers of two.</returns>
    /// <remarks>
    /// This helper assumes <paramref name="absoluteIntegerBits"/> represents a non-negative integer whose unbiased exponent is in the range [0, 112].
    /// The caller is responsible for handling zero (which is no longer an integer with a "next integer bit" — adding one to absolute zero is simply one).
    /// </remarks>
    internal static UInt128 IncrementIntegerMagnitudeBits(UInt128 absoluteIntegerBits)
    {
        uint biasedExponent = (uint)((absoluteIntegerBits & BiasedExponentMask) >> BiasedExponentShift);

        if (biasedExponent == 0u) return One.bits;

        int unbiasedExponent = (int)biasedExponent - ExponentBias;

        if (unbiasedExponent > TrailingSignificandBits) return absoluteIntegerBits;

        int lsbPosition = TrailingSignificandBits - unbiasedExponent;
        UInt128 lsbBit = UInt128.One << lsbPosition;

        return absoluteIntegerBits + lsbBit;
    }

    /// <summary>
    /// Packs a sign, unbiased exponent, and 113-bit significand into a finite-or-special <see cref="Float128"/>, applying IEEE 754 round-to-nearest, ties-to-even.
    /// </summary>
    /// <param name="sign">The sign of the result.</param>
    /// <param name="unbiasedExponent">The unbiased binary exponent of the would-be normalised result, where the implicit leading bit is at position <see cref="TrailingSignificandBits"/> of the significand.</param>
    /// <param name="significand">A 113-bit significand value with its leading <c>1</c> at position <see cref="TrailingSignificandBits"/> (or all zero, indicating a true zero result). Bits above position <c>113</c> must not be set.</param>
    /// <param name="roundBit">The bit immediately below the least-significant retained bit, accumulated from any prior shifts performed by the caller.</param>
    /// <param name="stickyBit">A boolean OR of every bit below <paramref name="roundBit"/> from the caller's wider intermediate.</param>
    /// <returns>Returns the correctly-rounded <see cref="Float128"/> representation, saturating to <see cref="PositiveInfinity"/> or <see cref="NegativeInfinity"/> on exponent overflow and tapering to subnormal or signed zero on exponent underflow.</returns>
    internal static Float128 RoundToNearestEven(bool sign, int unbiasedExponent, UInt128 significand, bool roundBit, bool stickyBit)
    {
        UInt128 signBit = sign ? SignMask : UInt128.Zero;

        if (significand == UInt128.Zero && !roundBit && !stickyBit)
        {
            return new Float128(signBit);
        }

        if (unbiasedExponent < MinNormalUnbiasedExponent)
        {
            int shift = MinNormalUnbiasedExponent - unbiasedExponent;

            if (shift > TrailingSignificandBits + 1)
            {
                return new Float128(signBit);
            }

            UInt128 droppedMask = (UInt128.One << shift) - UInt128.One;
            UInt128 droppedBits = significand & droppedMask;
            UInt128 newSignificand = significand >> shift;

            UInt128 newRoundMask = UInt128.One << (shift - 1);
            bool newRoundBit = (droppedBits & newRoundMask) != UInt128.Zero;

            UInt128 newStickyMask = newRoundMask - UInt128.One;
            bool newStickyBit = (droppedBits & newStickyMask) != UInt128.Zero || roundBit || stickyBit;

            significand = newSignificand;
            roundBit = newRoundBit;
            stickyBit = newStickyBit;
            unbiasedExponent = MinNormalUnbiasedExponent;
        }

        if (roundBit)
        {
            bool lsb = (significand & UInt128.One) != UInt128.Zero;
            if (stickyBit || lsb)
            {
                significand += UInt128.One;

                if ((significand & (UInt128.One << (TrailingSignificandBits + 1))) != UInt128.Zero)
                {
                    significand >>= 1;
                    unbiasedExponent++;
                }
            }
        }

        if (unbiasedExponent > MaxFiniteUnbiasedExponent)
        {
            return new Float128(signBit | BiasedExponentMask);
        }

        UInt128 trailingSignificand = significand & TrailingSignificandMask;

        if (unbiasedExponent == MinNormalUnbiasedExponent && (significand & ImplicitSignificandBit) == UInt128.Zero)
        {
            return new Float128(signBit | trailingSignificand);
        }

        UInt128 biasedExponent = (UInt128)(uint)(unbiasedExponent + ExponentBias) << BiasedExponentShift;
        return new Float128(signBit | biasedExponent | trailingSignificand);
    }

    /// <summary>
    /// Decomposes a finite <see cref="Float128"/> bit pattern into its sign, unbiased exponent, and 113-bit significand (with implicit leading bit included where applicable).
    /// </summary>
    /// <param name="bits">The bit pattern to decompose. Must be finite (not NaN, not infinity).</param>
    /// <param name="sign">When this method returns, contains the sign of the value (<see langword="true"/> if negative).</param>
    /// <param name="unbiasedExponent">When this method returns, contains the unbiased binary exponent. For subnormal values this is the minimum normal exponent so that the significand can be interpreted in unified form.</param>
    /// <param name="significand">When this method returns, contains the 113-bit significand with leading bit at position <see cref="TrailingSignificandBits"/> for normal values, or with leading bit at a lower position for subnormal values.</param>
    internal static void DecomposeFinite(UInt128 bits, out bool sign, out int unbiasedExponent, out UInt128 significand)
    {
        sign = ExtractSignBit(bits);
        uint biased = ExtractBiasedExponent(bits);
        UInt128 trailing = ExtractTrailingSignificand(bits);

        if (biased == 0u)
        {
            unbiasedExponent = MinNormalUnbiasedExponent;
            significand = trailing;
        }
        else
        {
            unbiasedExponent = (int)biased - ExponentBias;
            significand = ImplicitSignificandBit | trailing;
        }
    }
}
