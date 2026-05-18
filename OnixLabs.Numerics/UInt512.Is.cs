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

public readonly partial struct UInt512
{
    /// <summary>Determines whether the specified <see cref="UInt512"/> value represents zero.</summary>
    /// <param name="value">The value.</param>
    /// <returns>True if zero; otherwise false.</returns>
    public static bool IsZero(UInt512 value) => UInt256.IsZero(value.Upper) && UInt256.IsZero(value.Lower);

    /// <summary>Determines whether the specified <see cref="UInt512"/> value is even.</summary>
    /// <param name="value">The value.</param>
    /// <returns>True if even; otherwise false.</returns>
    public static bool IsEvenInteger(UInt512 value) => UInt256.IsEvenInteger(value.Lower);

    /// <summary>Determines whether the specified <see cref="UInt512"/> value is odd.</summary>
    /// <param name="value">The value.</param>
    /// <returns>True if odd; otherwise false.</returns>
    public static bool IsOddInteger(UInt512 value) => UInt256.IsOddInteger(value.Lower);

    /// <summary>Determines whether the specified <see cref="UInt512"/> value is a positive integer power of two.</summary>
    /// <param name="value">The value.</param>
    /// <returns>True if a positive power of two; otherwise false.</returns>
    public static bool IsPow2(UInt512 value)
    {
        if (IsZero(value)) return false;
        return (value & (value - One)) == Zero;
    }

    /// <inheritdoc cref="INumberBase{TSelf}.IsCanonical"/>
    static bool INumberBase<UInt512>.IsCanonical(UInt512 value) => true;

    /// <inheritdoc cref="INumberBase{TSelf}.IsComplexNumber"/>
    static bool INumberBase<UInt512>.IsComplexNumber(UInt512 value) => false;

    /// <inheritdoc cref="INumberBase{TSelf}.IsFinite"/>
    static bool INumberBase<UInt512>.IsFinite(UInt512 value) => true;

    /// <inheritdoc cref="INumberBase{TSelf}.IsImaginaryNumber"/>
    static bool INumberBase<UInt512>.IsImaginaryNumber(UInt512 value) => false;

    /// <inheritdoc cref="INumberBase{TSelf}.IsInfinity"/>
    static bool INumberBase<UInt512>.IsInfinity(UInt512 value) => false;

    /// <inheritdoc cref="INumberBase{TSelf}.IsInteger"/>
    static bool INumberBase<UInt512>.IsInteger(UInt512 value) => true;

    /// <inheritdoc cref="INumberBase{TSelf}.IsNaN"/>
    static bool INumberBase<UInt512>.IsNaN(UInt512 value) => false;

    /// <inheritdoc cref="INumberBase{TSelf}.IsNegative"/>
    static bool INumberBase<UInt512>.IsNegative(UInt512 value) => false;

    /// <inheritdoc cref="INumberBase{TSelf}.IsNegativeInfinity"/>
    static bool INumberBase<UInt512>.IsNegativeInfinity(UInt512 value) => false;

    /// <inheritdoc cref="INumberBase{TSelf}.IsNormal"/>
    static bool INumberBase<UInt512>.IsNormal(UInt512 value) => !IsZero(value);

    /// <inheritdoc cref="INumberBase{TSelf}.IsPositive"/>
    static bool INumberBase<UInt512>.IsPositive(UInt512 value) => true;

    /// <inheritdoc cref="INumberBase{TSelf}.IsPositiveInfinity"/>
    static bool INumberBase<UInt512>.IsPositiveInfinity(UInt512 value) => false;

    /// <inheritdoc cref="INumberBase{TSelf}.IsRealNumber"/>
    static bool INumberBase<UInt512>.IsRealNumber(UInt512 value) => true;

    /// <inheritdoc cref="INumberBase{TSelf}.IsSubnormal"/>
    static bool INumberBase<UInt512>.IsSubnormal(UInt512 value) => false;

    /// <inheritdoc cref="INumberBase{TSelf}.IsZero"/>
    static bool INumberBase<UInt512>.IsZero(UInt512 value) => IsZero(value);
}
