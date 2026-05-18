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

public readonly partial struct Int512
{
    /// <summary>Determines whether the specified <see cref="Int512"/> value represents zero.</summary>
    /// <param name="value">The value to be checked.</param>
    /// <returns>Returns <see langword="true"/> if the specified value is zero; otherwise, <see langword="false"/>.</returns>
    public static bool IsZero(Int512 value) => UInt256.IsZero(value.upper) && UInt256.IsZero(value.lower);

    /// <summary>Determines whether the specified <see cref="Int512"/> value is negative.</summary>
    /// <param name="value">The value to be checked.</param>
    /// <returns>Returns <see langword="true"/> if the sign bit is set; otherwise, <see langword="false"/>.</returns>
    public static bool IsNegative(Int512 value) => !UInt256.IsZero(value.upper & SignBitMask);

    /// <summary>Determines whether the specified <see cref="Int512"/> value is positive (non-negative).</summary>
    /// <param name="value">The value to be checked.</param>
    /// <returns>Returns <see langword="true"/> if the sign bit is not set; otherwise, <see langword="false"/>.</returns>
    public static bool IsPositive(Int512 value) => UInt256.IsZero(value.upper & SignBitMask);

    /// <summary>Determines whether the specified <see cref="Int512"/> value is even.</summary>
    /// <param name="value">The value to be checked.</param>
    /// <returns>Returns <see langword="true"/> if the specified value is even; otherwise, <see langword="false"/>.</returns>
    public static bool IsEvenInteger(Int512 value) => UInt256.IsZero(value.lower & UInt256.One);

    /// <summary>Determines whether the specified <see cref="Int512"/> value is odd.</summary>
    /// <param name="value">The value to be checked.</param>
    /// <returns>Returns <see langword="true"/> if the specified value is odd; otherwise, <see langword="false"/>.</returns>
    public static bool IsOddInteger(Int512 value) => !UInt256.IsZero(value.lower & UInt256.One);

    /// <summary>Determines whether the specified <see cref="Int512"/> value is a positive integer power of two.</summary>
    /// <param name="value">The value to be checked.</param>
    /// <returns>Returns <see langword="true"/> if the specified value is a positive power of two; otherwise, <see langword="false"/>.</returns>
    public static bool IsPow2(Int512 value)
    {
        if (IsNegative(value) || IsZero(value)) return false;
        return (value & (value - One)) == Zero;
    }

    /// <inheritdoc cref="INumberBase{TSelf}.IsCanonical"/>
    static bool INumberBase<Int512>.IsCanonical(Int512 value) => true;

    /// <inheritdoc cref="INumberBase{TSelf}.IsComplexNumber"/>
    static bool INumberBase<Int512>.IsComplexNumber(Int512 value) => false;

    /// <inheritdoc cref="INumberBase{TSelf}.IsFinite"/>
    static bool INumberBase<Int512>.IsFinite(Int512 value) => true;

    /// <inheritdoc cref="INumberBase{TSelf}.IsImaginaryNumber"/>
    static bool INumberBase<Int512>.IsImaginaryNumber(Int512 value) => false;

    /// <inheritdoc cref="INumberBase{TSelf}.IsInfinity"/>
    static bool INumberBase<Int512>.IsInfinity(Int512 value) => false;

    /// <inheritdoc cref="INumberBase{TSelf}.IsInteger"/>
    static bool INumberBase<Int512>.IsInteger(Int512 value) => true;

    /// <inheritdoc cref="INumberBase{TSelf}.IsNaN"/>
    static bool INumberBase<Int512>.IsNaN(Int512 value) => false;

    /// <inheritdoc cref="INumberBase{TSelf}.IsNegativeInfinity"/>
    static bool INumberBase<Int512>.IsNegativeInfinity(Int512 value) => false;

    /// <inheritdoc cref="INumberBase{TSelf}.IsNormal"/>
    static bool INumberBase<Int512>.IsNormal(Int512 value) => !IsZero(value);

    /// <inheritdoc cref="INumberBase{TSelf}.IsPositiveInfinity"/>
    static bool INumberBase<Int512>.IsPositiveInfinity(Int512 value) => false;

    /// <inheritdoc cref="INumberBase{TSelf}.IsRealNumber"/>
    static bool INumberBase<Int512>.IsRealNumber(Int512 value) => true;

    /// <inheritdoc cref="INumberBase{TSelf}.IsSubnormal"/>
    static bool INumberBase<Int512>.IsSubnormal(Int512 value) => false;

    /// <inheritdoc cref="INumberBase{TSelf}.IsZero"/>
    static bool INumberBase<Int512>.IsZero(Int512 value) => IsZero(value);
}
