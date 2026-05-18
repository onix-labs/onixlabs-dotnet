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

public readonly partial struct Int256
{
    /// <summary>Determines whether the specified <see cref="Int256"/> value represents zero.</summary>
    /// <param name="value">The value to be checked.</param>
    /// <returns>Returns <see langword="true"/> if the specified value is zero; otherwise, <see langword="false"/>.</returns>
    public static bool IsZero(Int256 value) => value.upper == UInt128.Zero && value.lower == UInt128.Zero;

    /// <summary>Determines whether the specified <see cref="Int256"/> value is negative.</summary>
    /// <param name="value">The value to be checked.</param>
    /// <returns>Returns <see langword="true"/> if the sign bit is set; otherwise, <see langword="false"/>.</returns>
    public static bool IsNegative(Int256 value) => (value.upper & SignBitMask) != UInt128.Zero;

    /// <summary>Determines whether the specified <see cref="Int256"/> value is positive (non-negative).</summary>
    /// <param name="value">The value to be checked.</param>
    /// <returns>Returns <see langword="true"/> if the sign bit is not set; otherwise, <see langword="false"/>.</returns>
    public static bool IsPositive(Int256 value) => (value.upper & SignBitMask) == UInt128.Zero;

    /// <summary>Determines whether the specified <see cref="Int256"/> value is even.</summary>
    /// <param name="value">The value to be checked.</param>
    /// <returns>Returns <see langword="true"/> if the specified value is even; otherwise, <see langword="false"/>.</returns>
    public static bool IsEvenInteger(Int256 value) => (value.lower & UInt128.One) == UInt128.Zero;

    /// <summary>Determines whether the specified <see cref="Int256"/> value is odd.</summary>
    /// <param name="value">The value to be checked.</param>
    /// <returns>Returns <see langword="true"/> if the specified value is odd; otherwise, <see langword="false"/>.</returns>
    public static bool IsOddInteger(Int256 value) => (value.lower & UInt128.One) != UInt128.Zero;

    /// <summary>Determines whether the specified <see cref="Int256"/> value is a positive integer power of two.</summary>
    /// <param name="value">The value to be checked.</param>
    /// <returns>Returns <see langword="true"/> if the specified value is a positive power of two; otherwise, <see langword="false"/>.</returns>
    public static bool IsPow2(Int256 value)
    {
        if (IsNegative(value) || IsZero(value)) return false;
        return (value & (value - One)) == Zero;
    }

    /// <inheritdoc cref="INumberBase{TSelf}.IsCanonical"/>
    static bool INumberBase<Int256>.IsCanonical(Int256 value) => true;

    /// <inheritdoc cref="INumberBase{TSelf}.IsComplexNumber"/>
    static bool INumberBase<Int256>.IsComplexNumber(Int256 value) => false;

    /// <inheritdoc cref="INumberBase{TSelf}.IsFinite"/>
    static bool INumberBase<Int256>.IsFinite(Int256 value) => true;

    /// <inheritdoc cref="INumberBase{TSelf}.IsImaginaryNumber"/>
    static bool INumberBase<Int256>.IsImaginaryNumber(Int256 value) => false;

    /// <inheritdoc cref="INumberBase{TSelf}.IsInfinity"/>
    static bool INumberBase<Int256>.IsInfinity(Int256 value) => false;

    /// <inheritdoc cref="INumberBase{TSelf}.IsInteger"/>
    static bool INumberBase<Int256>.IsInteger(Int256 value) => true;

    /// <inheritdoc cref="INumberBase{TSelf}.IsNaN"/>
    static bool INumberBase<Int256>.IsNaN(Int256 value) => false;

    /// <inheritdoc cref="INumberBase{TSelf}.IsNegativeInfinity"/>
    static bool INumberBase<Int256>.IsNegativeInfinity(Int256 value) => false;

    /// <inheritdoc cref="INumberBase{TSelf}.IsNormal"/>
    static bool INumberBase<Int256>.IsNormal(Int256 value) => !IsZero(value);

    /// <inheritdoc cref="INumberBase{TSelf}.IsPositiveInfinity"/>
    static bool INumberBase<Int256>.IsPositiveInfinity(Int256 value) => false;

    /// <inheritdoc cref="INumberBase{TSelf}.IsRealNumber"/>
    static bool INumberBase<Int256>.IsRealNumber(Int256 value) => true;

    /// <inheritdoc cref="INumberBase{TSelf}.IsSubnormal"/>
    static bool INumberBase<Int256>.IsSubnormal(Int256 value) => false;

    /// <inheritdoc cref="INumberBase{TSelf}.IsZero"/>
    static bool INumberBase<Int256>.IsZero(Int256 value) => IsZero(value);
}
