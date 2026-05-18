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
    /// <summary>
    /// Gets the sign bit of the specified IEEE 754 binary256 bit pattern as a boolean value.
    /// </summary>
    /// <param name="bits">The IEEE 754 binary256 bit pattern from which to extract the sign.</param>
    /// <returns>Returns <see langword="true"/> if the sign bit is set; otherwise, <see langword="false"/>.</returns>
    internal static bool ExtractSignBit(UInt256 bits) => (bits & SignMask) != UInt256.Zero;

    /// <summary>
    /// Gets the biased exponent of the specified IEEE 754 binary256 bit pattern.
    /// </summary>
    /// <param name="bits">The IEEE 754 binary256 bit pattern from which to extract the biased exponent.</param>
    /// <returns>Returns the 19-bit biased exponent as an unsigned integer in the range [0, 524287].</returns>
    internal static uint ExtractBiasedExponent(UInt256 bits) => (uint)((bits & BiasedExponentMask) >> BiasedExponentShift).Lower;

    /// <summary>
    /// Gets the trailing significand of the specified IEEE 754 binary256 bit pattern.
    /// </summary>
    /// <param name="bits">The IEEE 754 binary256 bit pattern from which to extract the trailing significand.</param>
    /// <returns>Returns the 236-bit trailing significand, with the implicit leading bit omitted.</returns>
    internal static UInt256 ExtractTrailingSignificand(UInt256 bits) => bits & TrailingSignificandMask;

    /// <summary>
    /// Determines whether the specified bit pattern encodes a zero (positive or negative).
    /// </summary>
    /// <param name="bits">The IEEE 754 binary256 bit pattern to inspect.</param>
    /// <returns>Returns <see langword="true"/> if the bit pattern encodes a zero; otherwise, <see langword="false"/>.</returns>
    internal static bool IsZeroBits(UInt256 bits) => (bits & ~SignMask) == UInt256.Zero;

    /// <summary>
    /// Determines whether the specified bit pattern encodes positive or negative infinity.
    /// </summary>
    /// <param name="bits">The IEEE 754 binary256 bit pattern to inspect.</param>
    /// <returns>Returns <see langword="true"/> if the bit pattern encodes infinity; otherwise, <see langword="false"/>.</returns>
    internal static bool IsInfinityBits(UInt256 bits) => (bits & ~SignMask) == BiasedExponentMask;

    /// <summary>
    /// Determines whether the specified bit pattern encodes a not-a-number (NaN) value.
    /// </summary>
    /// <param name="bits">The IEEE 754 binary256 bit pattern to inspect.</param>
    /// <returns>Returns <see langword="true"/> if the bit pattern encodes a NaN value; otherwise, <see langword="false"/>.</returns>
    internal static bool IsNaNBits(UInt256 bits) => (bits & ~SignMask) > BiasedExponentMask;

    /// <summary>
    /// Determines whether the specified bit pattern encodes a finite value.
    /// </summary>
    /// <param name="bits">The IEEE 754 binary256 bit pattern to inspect.</param>
    /// <returns>Returns <see langword="true"/> if the bit pattern encodes a finite value; otherwise, <see langword="false"/>.</returns>
    internal static bool IsFiniteBits(UInt256 bits) => ExtractBiasedExponent(bits) != MaxBiasedExponent;

    /// <summary>
    /// Determines whether the specified bit pattern encodes a normal value.
    /// </summary>
    /// <param name="bits">The IEEE 754 binary256 bit pattern to inspect.</param>
    /// <returns>Returns <see langword="true"/> if the bit pattern encodes a normal value; otherwise, <see langword="false"/>.</returns>
    internal static bool IsNormalBits(UInt256 bits)
    {
        uint biased = ExtractBiasedExponent(bits);
        return biased > 0u && biased < MaxBiasedExponent;
    }

    /// <summary>
    /// Determines whether the specified bit pattern encodes a subnormal, non-zero value.
    /// </summary>
    /// <param name="bits">The IEEE 754 binary256 bit pattern to inspect.</param>
    /// <returns>Returns <see langword="true"/> if the bit pattern encodes a subnormal, non-zero value; otherwise, <see langword="false"/>.</returns>
    internal static bool IsSubnormalBits(UInt256 bits) => ExtractBiasedExponent(bits) == 0u && ExtractTrailingSignificand(bits) != UInt256.Zero;

    /// <summary>
    /// Decomposes a finite <see cref="Float256"/> bit pattern into its sign, unbiased exponent, and 237-bit significand (with implicit leading bit included where applicable).
    /// </summary>
    /// <param name="bits">The bit pattern to decompose. Must be finite (not NaN, not infinity).</param>
    /// <param name="sign">When this method returns, contains the sign of the value (<see langword="true"/> if negative).</param>
    /// <param name="unbiasedExponent">When this method returns, contains the unbiased binary exponent. For subnormal values this is the minimum normal exponent so that the significand can be interpreted in unified form.</param>
    /// <param name="significand">When this method returns, contains the 237-bit significand with leading bit at position <see cref="TrailingSignificandBits"/> for normal values, or with leading bit at a lower position for subnormal values.</param>
    internal static void DecomposeFinite(UInt256 bits, out bool sign, out int unbiasedExponent, out UInt256 significand)
    {
        sign = ExtractSignBit(bits);
        uint biased = ExtractBiasedExponent(bits);
        UInt256 trailing = ExtractTrailingSignificand(bits);

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

    /// <summary>
    /// Renormalises a subnormal significand by shifting it left until its leading bit reaches the implicit-bit position, adjusting the exponent accordingly.
    /// </summary>
    /// <param name="significand">A reference to the significand to renormalise; updated in place. A zero or already-normal significand is left unchanged.</param>
    /// <param name="unbiasedExponent">A reference to the unbiased exponent associated with the significand; decremented by the number of left-shift positions.</param>
    internal static void NormalizeSubnormal(ref UInt256 significand, ref int unbiasedExponent)
    {
        if (significand == UInt256.Zero) return;
        if ((significand & ImplicitSignificandBit) != UInt256.Zero) return;

        int leadingBitPosition = 255 - (int)UInt256.LeadingZeroCount(significand);
        int shift = TrailingSignificandBits - leadingBitPosition;
        significand <<= shift;
        unbiasedExponent -= shift;
    }
}
