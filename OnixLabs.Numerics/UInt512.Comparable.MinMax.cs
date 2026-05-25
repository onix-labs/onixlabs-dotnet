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
    /// <summary>Returns the smaller of two <see cref="UInt512"/> values.</summary>
    /// <param name="x">The first value to compare.</param>
    /// <param name="y">The second value to compare.</param>
    /// <returns>Returns the smaller of <paramref name="x"/> and <paramref name="y"/>.</returns>
    public static UInt512 Min(UInt512 x, UInt512 y) => x <= y ? x : y;

    /// <summary>Returns the larger of two <see cref="UInt512"/> values.</summary>
    /// <param name="x">The first value to compare.</param>
    /// <param name="y">The second value to compare.</param>
    /// <returns>Returns the larger of <paramref name="x"/> and <paramref name="y"/>.</returns>
    public static UInt512 Max(UInt512 x, UInt512 y) => x >= y ? x : y;

    /// <summary>Returns <paramref name="value"/> clamped to the inclusive range <c>[min, max]</c>.</summary>
    /// <param name="value">The value to clamp.</param>
    /// <param name="min">The lower bound of the result.</param>
    /// <param name="max">The upper bound of the result.</param>
    /// <returns>Returns <paramref name="value"/> clamped to <c>[min, max]</c>.</returns>
    /// <exception cref="ArgumentException">Thrown when <paramref name="min"/> is greater than <paramref name="max"/>.</exception>
    public static UInt512 Clamp(UInt512 value, UInt512 min, UInt512 max)
    {
        if (min > max) throw new ArgumentException("Min must be less than or equal to max.", nameof(min));
        if (value < min) return min;
        if (value > max) return max;
        return value;
    }

    /// <summary>Returns the value with the smaller magnitude. For unsigned types this is the smaller of the two values.</summary>
    /// <param name="x">The first value to compare.</param>
    /// <param name="y">The second value to compare.</param>
    /// <returns>Returns the value with the smaller magnitude.</returns>
    public static UInt512 MinMagnitude(UInt512 x, UInt512 y) => Min(x, y);

    /// <summary>Returns the value with the larger magnitude. For unsigned types this is the larger of the two values.</summary>
    /// <param name="x">The first value to compare.</param>
    /// <param name="y">The second value to compare.</param>
    /// <returns>Returns the value with the larger magnitude.</returns>
    public static UInt512 MaxMagnitude(UInt512 x, UInt512 y) => Max(x, y);

    /// <inheritdoc cref="INumberBase{TSelf}.MinMagnitudeNumber"/>
    static UInt512 INumberBase<UInt512>.MinMagnitudeNumber(UInt512 x, UInt512 y) => MinMagnitude(x, y);

    /// <inheritdoc cref="INumberBase{TSelf}.MaxMagnitudeNumber"/>
    static UInt512 INumberBase<UInt512>.MaxMagnitudeNumber(UInt512 x, UInt512 y) => MaxMagnitude(x, y);

    /// <inheritdoc cref="INumber{TSelf}.MinNumber"/>
    static UInt512 INumber<UInt512>.MinNumber(UInt512 x, UInt512 y) => Min(x, y);

    /// <inheritdoc cref="INumber{TSelf}.MaxNumber"/>
    static UInt512 INumber<UInt512>.MaxNumber(UInt512 x, UInt512 y) => Max(x, y);

    /// <inheritdoc cref="INumber{TSelf}.Sign"/>
    public static int Sign(UInt512 value) => IsZero(value) ? 0 : 1;

    /// <inheritdoc cref="INumber{TSelf}.CopySign"/>
    static UInt512 INumber<UInt512>.CopySign(UInt512 value, UInt512 sign) => value;

    /// <inheritdoc cref="INumberBase{TSelf}.Abs"/>
    public static UInt512 Abs(UInt512 value) => value;
}
