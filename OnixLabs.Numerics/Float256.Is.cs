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
    /// Determines whether the specified <see cref="Float256"/> value represents a not-a-number (NaN) value.
    /// </summary>
    /// <param name="value">The value to be checked.</param>
    /// <returns>Returns <see langword="true"/> if the specified value is NaN; otherwise, <see langword="false"/>.</returns>
    public static bool IsNaN(Float256 value) => IsNaNBits(value.RawBits);

    /// <summary>
    /// Determines whether the specified <see cref="Float256"/> value represents positive or negative infinity.
    /// </summary>
    /// <param name="value">The value to be checked.</param>
    /// <returns>Returns <see langword="true"/> if the specified value is infinite; otherwise, <see langword="false"/>.</returns>
    public static bool IsInfinity(Float256 value) => IsInfinityBits(value.RawBits);

    /// <summary>
    /// Determines whether the specified <see cref="Float256"/> value represents positive infinity.
    /// </summary>
    /// <param name="value">The value to be checked.</param>
    /// <returns>Returns <see langword="true"/> if the specified value is positive infinity; otherwise, <see langword="false"/>.</returns>
    public static bool IsPositiveInfinity(Float256 value) => value.RawBits == PositiveInfinity.RawBits;

    /// <summary>
    /// Determines whether the specified <see cref="Float256"/> value represents negative infinity.
    /// </summary>
    /// <param name="value">The value to be checked.</param>
    /// <returns>Returns <see langword="true"/> if the specified value is negative infinity; otherwise, <see langword="false"/>.</returns>
    public static bool IsNegativeInfinity(Float256 value) => value.RawBits == NegativeInfinity.RawBits;

    /// <summary>
    /// Determines whether the specified <see cref="Float256"/> value represents a finite value.
    /// </summary>
    /// <param name="value">The value to be checked.</param>
    /// <returns>Returns <see langword="true"/> if the specified value is finite; otherwise, <see langword="false"/>.</returns>
    public static bool IsFinite(Float256 value) => IsFiniteBits(value.RawBits);

    /// <summary>
    /// Determines whether the specified <see cref="Float256"/> value represents a normal value.
    /// </summary>
    /// <param name="value">The value to be checked.</param>
    /// <returns>Returns <see langword="true"/> if the specified value is a normal value; otherwise, <see langword="false"/>.</returns>
    public static bool IsNormal(Float256 value) => IsNormalBits(value.RawBits);

    /// <summary>
    /// Determines whether the specified <see cref="Float256"/> value represents a subnormal, non-zero value.
    /// </summary>
    /// <param name="value">The value to be checked.</param>
    /// <returns>Returns <see langword="true"/> if the specified value is a subnormal, non-zero value; otherwise, <see langword="false"/>.</returns>
    public static bool IsSubnormal(Float256 value) => IsSubnormalBits(value.RawBits);

    /// <summary>
    /// Determines whether the specified <see cref="Float256"/> value represents a zero (positive or negative).
    /// </summary>
    /// <param name="value">The value to be checked.</param>
    /// <returns>Returns <see langword="true"/> if the specified value is zero; otherwise, <see langword="false"/>.</returns>
    public static bool IsZero(Float256 value) => IsZeroBits(value.RawBits);

    /// <summary>
    /// Determines whether the specified <see cref="Float256"/> value has its sign bit set.
    /// </summary>
    /// <param name="value">The value to be checked.</param>
    /// <returns>Returns <see langword="true"/> if the sign bit is set; otherwise, <see langword="false"/>.</returns>
    public static bool IsNegative(Float256 value) => ExtractSignBit(value.RawBits);

    /// <summary>
    /// Determines whether the specified <see cref="Float256"/> value has its sign bit cleared.
    /// </summary>
    /// <param name="value">The value to be checked.</param>
    /// <returns>Returns <see langword="true"/> if the sign bit is not set; otherwise, <see langword="false"/>.</returns>
    public static bool IsPositive(Float256 value) => !ExtractSignBit(value.RawBits);

    /// <summary>
    /// Determines whether the specified <see cref="Float256"/> value represents an integral value.
    /// </summary>
    /// <param name="value">The value to be checked.</param>
    /// <returns>Returns <see langword="true"/> if the specified value is an integral value; otherwise, <see langword="false"/>.</returns>
    public static bool IsInteger(Float256 value)
    {
        UInt256 bits = value.RawBits;
        uint biased = ExtractBiasedExponent(bits);

        if (biased == MaxBiasedExponent) return false;
        if (IsZeroBits(bits)) return true;
        if (biased == 0u) return false;

        int unbiased = (int)biased - ExponentBias;

        if (unbiased >= TrailingSignificandBits) return true;
        if (unbiased < 0) return false;

        UInt256 fractionMask = (UInt256.One << (TrailingSignificandBits - unbiased)) - UInt256.One;
        return (ExtractTrailingSignificand(bits) & fractionMask) == UInt256.Zero;
    }

    /// <summary>
    /// Determines whether the specified <see cref="Float256"/> value represents an even integral value.
    /// </summary>
    /// <param name="value">The value to be checked.</param>
    /// <returns>Returns <see langword="true"/> if the specified value is an even integral value; otherwise, <see langword="false"/>.</returns>
    public static bool IsEvenInteger(Float256 value)
    {
        if (!IsInteger(value)) return false;
        if (IsZeroBits(value.RawBits)) return true;

        int unbiased = (int)ExtractBiasedExponent(value.RawBits) - ExponentBias;

        if (unbiased > TrailingSignificandBits) return true;
        if (unbiased == 0) return false;

        int unitBitPosition = TrailingSignificandBits - unbiased;
        return (ExtractTrailingSignificand(value.RawBits) & (UInt256.One << unitBitPosition)) == UInt256.Zero;
    }

    /// <summary>
    /// Determines whether the specified <see cref="Float256"/> value represents an odd integral value.
    /// </summary>
    /// <param name="value">The value to be checked.</param>
    /// <returns>Returns <see langword="true"/> if the specified value is an odd integral value; otherwise, <see langword="false"/>.</returns>
    public static bool IsOddInteger(Float256 value)
    {
        if (!IsInteger(value)) return false;
        if (IsZeroBits(value.RawBits)) return false;

        int unbiased = (int)ExtractBiasedExponent(value.RawBits) - ExponentBias;

        if (unbiased > TrailingSignificandBits) return false;
        if (unbiased == 0) return true;

        int unitBitPosition = TrailingSignificandBits - unbiased;
        return (ExtractTrailingSignificand(value.RawBits) & (UInt256.One << unitBitPosition)) != UInt256.Zero;
    }

    /// <summary>
    /// Determines whether the specified <see cref="Float256"/> value represents a positive integer power of two.
    /// </summary>
    /// <param name="value">The value to be checked.</param>
    /// <returns>Returns <see langword="true"/> if the specified value is a positive power of two; otherwise, <see langword="false"/>.</returns>
    public static bool IsPow2(Float256 value)
    {
        if (!IsFinite(value)) return false;
        if (IsNegative(value)) return false;
        if (IsZero(value)) return false;

        UInt256 bits = value.RawBits;
        uint biased = ExtractBiasedExponent(bits);
        UInt256 trailing = ExtractTrailingSignificand(bits);

        if (biased == 0u)
        {
            return trailing != UInt256.Zero && (trailing & (trailing - UInt256.One)) == UInt256.Zero;
        }

        return trailing == UInt256.Zero;
    }

    /// <summary>
    /// Determines whether the specified <see cref="Float256"/> value is in its canonical representation.
    /// </summary>
    /// <param name="value">The value to be checked.</param>
    /// <returns>Returns <see langword="true"/> for any <see cref="Float256"/> value; the binary256 format has a single canonical bit pattern for each finite value.</returns>
    static bool INumberBase<Float256>.IsCanonical(Float256 value) => true;

    /// <summary>
    /// Determines whether the specified <see cref="Float256"/> value represents a complex number.
    /// </summary>
    /// <param name="value">The value to be checked.</param>
    /// <returns>Returns <see langword="false"/>; <see cref="Float256"/> values are real-only.</returns>
    static bool INumberBase<Float256>.IsComplexNumber(Float256 value) => false;

    /// <summary>
    /// Determines whether the specified <see cref="Float256"/> value represents a pure imaginary number.
    /// </summary>
    /// <param name="value">The value to be checked.</param>
    /// <returns>Returns <see langword="false"/>; <see cref="Float256"/> values are real-only.</returns>
    static bool INumberBase<Float256>.IsImaginaryNumber(Float256 value) => false;

    /// <summary>
    /// Determines whether the specified <see cref="Float256"/> value represents a real number.
    /// </summary>
    /// <param name="value">The value to be checked.</param>
    /// <returns>Returns <see langword="true"/> for any non-NaN <see cref="Float256"/> value, including ±infinity.</returns>
    static bool INumberBase<Float256>.IsRealNumber(Float256 value) => !IsNaN(value);
}
