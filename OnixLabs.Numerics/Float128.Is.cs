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
    /// Determines whether the specified <see cref="Float128"/> value represents a not-a-number (NaN) value.
    /// </summary>
    /// <param name="value">The value to be checked.</param>
    /// <returns>Returns <see langword="true"/> if the specified value is NaN; otherwise, <see langword="false"/>.</returns>
    public static bool IsNaN(Float128 value) => IsNaNBits(value.bits);

    /// <summary>
    /// Determines whether the specified <see cref="Float128"/> value represents positive or negative infinity.
    /// </summary>
    /// <param name="value">The value to be checked.</param>
    /// <returns>Returns <see langword="true"/> if the specified value is infinite; otherwise, <see langword="false"/>.</returns>
    public static bool IsInfinity(Float128 value) => IsInfinityBits(value.bits);

    /// <summary>
    /// Determines whether the specified <see cref="Float128"/> value represents positive infinity.
    /// </summary>
    /// <param name="value">The value to be checked.</param>
    /// <returns>Returns <see langword="true"/> if the specified value is positive infinity; otherwise, <see langword="false"/>.</returns>
    public static bool IsPositiveInfinity(Float128 value) => value.bits == PositiveInfinity.bits;

    /// <summary>
    /// Determines whether the specified <see cref="Float128"/> value represents negative infinity.
    /// </summary>
    /// <param name="value">The value to be checked.</param>
    /// <returns>Returns <see langword="true"/> if the specified value is negative infinity; otherwise, <see langword="false"/>.</returns>
    public static bool IsNegativeInfinity(Float128 value) => value.bits == NegativeInfinity.bits;

    /// <summary>
    /// Determines whether the specified <see cref="Float128"/> value represents a finite value.
    /// </summary>
    /// <param name="value">The value to be checked.</param>
    /// <returns>Returns <see langword="true"/> if the specified value is finite; otherwise, <see langword="false"/>.</returns>
    public static bool IsFinite(Float128 value) => IsFiniteBits(value.bits);

    /// <summary>
    /// Determines whether the specified <see cref="Float128"/> value represents a normal value.
    /// </summary>
    /// <param name="value">The value to be checked.</param>
    /// <returns>Returns <see langword="true"/> if the specified value is a normal value; otherwise, <see langword="false"/>.</returns>
    public static bool IsNormal(Float128 value) => IsNormalBits(value.bits);

    /// <summary>
    /// Determines whether the specified <see cref="Float128"/> value represents a subnormal, non-zero value.
    /// </summary>
    /// <param name="value">The value to be checked.</param>
    /// <returns>Returns <see langword="true"/> if the specified value is a subnormal, non-zero value; otherwise, <see langword="false"/>.</returns>
    public static bool IsSubnormal(Float128 value) => IsSubnormalBits(value.bits);

    /// <summary>
    /// Determines whether the specified <see cref="Float128"/> value represents a zero (positive or negative).
    /// </summary>
    /// <param name="value">The value to be checked.</param>
    /// <returns>Returns <see langword="true"/> if the specified value is zero; otherwise, <see langword="false"/>.</returns>
    public static bool IsZero(Float128 value) => IsZeroBits(value.bits);

    /// <summary>
    /// Determines whether the specified <see cref="Float128"/> value has its sign bit set.
    /// </summary>
    /// <param name="value">The value to be checked.</param>
    /// <returns>Returns <see langword="true"/> if the sign bit is set; otherwise, <see langword="false"/>.</returns>
    /// <remarks>
    /// This predicate inspects only the sign bit; it returns <see langword="true"/> for negative zero
    /// and for negative NaN bit patterns, matching the behaviour of <see cref="double.IsNegative(double)"/>.
    /// </remarks>
    public static bool IsNegative(Float128 value) => ExtractSignBit(value.bits);

    /// <summary>
    /// Determines whether the specified <see cref="Float128"/> value has its sign bit cleared.
    /// </summary>
    /// <param name="value">The value to be checked.</param>
    /// <returns>Returns <see langword="true"/> if the sign bit is not set; otherwise, <see langword="false"/>.</returns>
    /// <remarks>
    /// This predicate inspects only the sign bit; it returns <see langword="true"/> for positive zero
    /// and for canonical NaN bit patterns, matching the behaviour of <see cref="double.IsPositive(double)"/>.
    /// </remarks>
    public static bool IsPositive(Float128 value) => !ExtractSignBit(value.bits);

    /// <summary>
    /// Determines whether the specified <see cref="Float128"/> value represents an integral value.
    /// </summary>
    /// <param name="value">The value to be checked.</param>
    /// <returns>Returns <see langword="true"/> if the specified value is an integral value; otherwise, <see langword="false"/>.</returns>
    public static bool IsInteger(Float128 value)
    {
        UInt128 bits = value.bits;
        uint biased = ExtractBiasedExponent(bits);

        if (biased == MaxBiasedExponent) return false;
        if (IsZeroBits(bits)) return true;
        if (biased == 0u) return false;

        int unbiased = (int)biased - ExponentBias;

        if (unbiased >= TrailingSignificandBits) return true;
        if (unbiased < 0) return false;

        UInt128 fractionMask = (UInt128.One << (TrailingSignificandBits - unbiased)) - UInt128.One;
        return (ExtractTrailingSignificand(bits) & fractionMask) == UInt128.Zero;
    }

    /// <summary>
    /// Determines whether the specified <see cref="Float128"/> value represents an even integral value.
    /// </summary>
    /// <param name="value">The value to be checked.</param>
    /// <returns>Returns <see langword="true"/> if the specified value is an even integral value; otherwise, <see langword="false"/>.</returns>
    public static bool IsEvenInteger(Float128 value)
    {
        if (!IsInteger(value)) return false;
        if (IsZeroBits(value.bits)) return true;

        int unbiased = (int)ExtractBiasedExponent(value.bits) - ExponentBias;

        if (unbiased > TrailingSignificandBits) return true;
        if (unbiased == 0) return false;

        int unitBitPosition = TrailingSignificandBits - unbiased;
        return (ExtractTrailingSignificand(value.bits) & (UInt128.One << unitBitPosition)) == UInt128.Zero;
    }

    /// <summary>
    /// Determines whether the specified <see cref="Float128"/> value represents an odd integral value.
    /// </summary>
    /// <param name="value">The value to be checked.</param>
    /// <returns>Returns <see langword="true"/> if the specified value is an odd integral value; otherwise, <see langword="false"/>.</returns>
    public static bool IsOddInteger(Float128 value)
    {
        if (!IsInteger(value)) return false;
        if (IsZeroBits(value.bits)) return false;

        int unbiased = (int)ExtractBiasedExponent(value.bits) - ExponentBias;

        if (unbiased > TrailingSignificandBits) return false;
        if (unbiased == 0) return true;

        int unitBitPosition = TrailingSignificandBits - unbiased;
        return (ExtractTrailingSignificand(value.bits) & (UInt128.One << unitBitPosition)) != UInt128.Zero;
    }

    /// <summary>
    /// Determines whether the specified <see cref="Float128"/> value represents a positive integer power of two.
    /// </summary>
    /// <param name="value">The value to be checked.</param>
    /// <returns>Returns <see langword="true"/> if the specified value is a positive power of two; otherwise, <see langword="false"/>.</returns>
    public static bool IsPow2(Float128 value)
    {
        if (!IsFinite(value)) return false;
        if (IsNegative(value)) return false;
        if (IsZero(value)) return false;

        UInt128 bits = value.bits;
        uint biased = ExtractBiasedExponent(bits);
        UInt128 trailing = ExtractTrailingSignificand(bits);

        if (biased == 0u)
        {
            return trailing != UInt128.Zero && (trailing & (trailing - UInt128.One)) == UInt128.Zero;
        }

        return trailing == UInt128.Zero;
    }

    /// <summary>
    /// Determines whether the specified <see cref="Float128"/> value is in its canonical representation.
    /// </summary>
    /// <param name="value">The value to be checked.</param>
    /// <returns>Returns <see langword="true"/> for any <see cref="Float128"/> value; the binary128 format has a single canonical bit pattern for each finite value.</returns>
    static bool INumberBase<Float128>.IsCanonical(Float128 value) => true;

    /// <summary>
    /// Determines whether the specified <see cref="Float128"/> value represents a complex number.
    /// </summary>
    /// <param name="value">The value to be checked.</param>
    /// <returns>Returns <see langword="false"/>; <see cref="Float128"/> values are real-only.</returns>
    static bool INumberBase<Float128>.IsComplexNumber(Float128 value) => false;

    /// <summary>
    /// Determines whether the specified <see cref="Float128"/> value represents a pure imaginary number.
    /// </summary>
    /// <param name="value">The value to be checked.</param>
    /// <returns>Returns <see langword="false"/>; <see cref="Float128"/> values are real-only.</returns>
    static bool INumberBase<Float128>.IsImaginaryNumber(Float128 value) => false;

    /// <summary>
    /// Determines whether the specified <see cref="Float128"/> value represents a real number.
    /// </summary>
    /// <param name="value">The value to be checked.</param>
    /// <returns>Returns <see langword="true"/> for any non-NaN <see cref="Float128"/> value, including ±infinity.</returns>
    static bool INumberBase<Float128>.IsRealNumber(Float128 value) => !IsNaN(value);
}
