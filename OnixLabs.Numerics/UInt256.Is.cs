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

public readonly partial struct UInt256
{
    /// <summary>
    /// Determines whether the specified <see cref="UInt256"/> value represents zero.
    /// </summary>
    /// <param name="value">The value to be checked.</param>
    /// <returns>Returns <see langword="true"/> if the specified value is zero; otherwise, <see langword="false"/>.</returns>
    public static bool IsZero(UInt256 value) => value.Upper == UInt128.Zero && value.Lower == UInt128.Zero;

    /// <summary>
    /// Determines whether the specified <see cref="UInt256"/> value is even.
    /// </summary>
    /// <param name="value">The value to be checked.</param>
    /// <returns>Returns <see langword="true"/> if the specified value is even; otherwise, <see langword="false"/>.</returns>
    public static bool IsEvenInteger(UInt256 value) => (value.Lower & UInt128.One) == UInt128.Zero;

    /// <summary>
    /// Determines whether the specified <see cref="UInt256"/> value is odd.
    /// </summary>
    /// <param name="value">The value to be checked.</param>
    /// <returns>Returns <see langword="true"/> if the specified value is odd; otherwise, <see langword="false"/>.</returns>
    public static bool IsOddInteger(UInt256 value) => (value.Lower & UInt128.One) != UInt128.Zero;

    /// <summary>
    /// Determines whether the specified <see cref="UInt256"/> value is a positive integer power of two.
    /// </summary>
    /// <param name="value">The value to be checked.</param>
    /// <returns>Returns <see langword="true"/> if the specified value is a positive power of two; otherwise, <see langword="false"/>.</returns>
    public static bool IsPow2(UInt256 value)
    {
        if (IsZero(value)) return false;
        return (value & (value - One)) == Zero;
    }

    /// <inheritdoc cref="INumberBase{TSelf}.IsCanonical"/>
    static bool INumberBase<UInt256>.IsCanonical(UInt256 value) => true;

    /// <inheritdoc cref="INumberBase{TSelf}.IsComplexNumber"/>
    static bool INumberBase<UInt256>.IsComplexNumber(UInt256 value) => false;

    /// <inheritdoc cref="INumberBase{TSelf}.IsFinite"/>
    static bool INumberBase<UInt256>.IsFinite(UInt256 value) => true;

    /// <inheritdoc cref="INumberBase{TSelf}.IsImaginaryNumber"/>
    static bool INumberBase<UInt256>.IsImaginaryNumber(UInt256 value) => false;

    /// <inheritdoc cref="INumberBase{TSelf}.IsInfinity"/>
    static bool INumberBase<UInt256>.IsInfinity(UInt256 value) => false;

    /// <inheritdoc cref="INumberBase{TSelf}.IsInteger"/>
    static bool INumberBase<UInt256>.IsInteger(UInt256 value) => true;

    /// <inheritdoc cref="INumberBase{TSelf}.IsNaN"/>
    static bool INumberBase<UInt256>.IsNaN(UInt256 value) => false;

    /// <inheritdoc cref="INumberBase{TSelf}.IsNegative"/>
    static bool INumberBase<UInt256>.IsNegative(UInt256 value) => false;

    /// <inheritdoc cref="INumberBase{TSelf}.IsNegativeInfinity"/>
    static bool INumberBase<UInt256>.IsNegativeInfinity(UInt256 value) => false;

    /// <inheritdoc cref="INumberBase{TSelf}.IsNormal"/>
    static bool INumberBase<UInt256>.IsNormal(UInt256 value) => !IsZero(value);

    /// <inheritdoc cref="INumberBase{TSelf}.IsPositive"/>
    static bool INumberBase<UInt256>.IsPositive(UInt256 value) => true;

    /// <inheritdoc cref="INumberBase{TSelf}.IsPositiveInfinity"/>
    static bool INumberBase<UInt256>.IsPositiveInfinity(UInt256 value) => false;

    /// <inheritdoc cref="INumberBase{TSelf}.IsRealNumber"/>
    static bool INumberBase<UInt256>.IsRealNumber(UInt256 value) => true;

    /// <inheritdoc cref="INumberBase{TSelf}.IsSubnormal"/>
    static bool INumberBase<UInt256>.IsSubnormal(UInt256 value) => false;

    /// <inheritdoc cref="INumberBase{TSelf}.IsZero"/>
    static bool INumberBase<UInt256>.IsZero(UInt256 value) => IsZero(value);
}
