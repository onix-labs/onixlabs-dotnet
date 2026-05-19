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
    /// <summary>Returns the smaller of two <see cref="Int256"/> values (signed comparison).</summary>
    /// <param name="x">The first value.</param>
    /// <param name="y">The second value.</param>
    /// <returns>Returns the smaller of <paramref name="x"/> and <paramref name="y"/>.</returns>
    public static Int256 Min(Int256 x, Int256 y) => x <= y ? x : y;

    /// <summary>Returns the larger of two <see cref="Int256"/> values (signed comparison).</summary>
    /// <param name="x">The first value.</param>
    /// <param name="y">The second value.</param>
    /// <returns>Returns the larger of <paramref name="x"/> and <paramref name="y"/>.</returns>
    public static Int256 Max(Int256 x, Int256 y) => x >= y ? x : y;

    /// <summary>Returns <paramref name="value"/> clamped to the inclusive range <c>[min, max]</c>.</summary>
    /// <param name="value">The value to clamp.</param>
    /// <param name="min">The lower bound of the result.</param>
    /// <param name="max">The upper bound of the result.</param>
    /// <returns>Returns <paramref name="value"/> clamped to <c>[min, max]</c>.</returns>
    /// <exception cref="ArgumentException">Thrown when <paramref name="min"/> is greater than <paramref name="max"/>.</exception>
    public static Int256 Clamp(Int256 value, Int256 min, Int256 max)
    {
        if (min > max) throw new ArgumentException("Min must be less than or equal to max.", nameof(min));
        if (value < min) return min;
        if (value > max) return max;
        return value;
    }

    /// <summary>Returns the value with the smaller magnitude.</summary>
    /// <param name="x">The first value.</param>
    /// <param name="y">The second value.</param>
    /// <returns>Returns the value with the smaller absolute magnitude.</returns>
    public static Int256 MinMagnitude(Int256 x, Int256 y)
    {
        Int256 absX = AbsUnchecked(x);
        Int256 absY = AbsUnchecked(y);
        if (absX < absY) return x;
        if (absX > absY) return y;
        return IsNegative(x) ? x : y;
    }

    /// <summary>Returns the value with the larger magnitude.</summary>
    /// <param name="x">The first value.</param>
    /// <param name="y">The second value.</param>
    /// <returns>Returns the value with the larger absolute magnitude.</returns>
    public static Int256 MaxMagnitude(Int256 x, Int256 y)
    {
        Int256 absX = AbsUnchecked(x);
        Int256 absY = AbsUnchecked(y);
        if (absX > absY) return x;
        if (absX < absY) return y;
        return IsNegative(x) ? y : x;
    }

    /// <inheritdoc cref="INumberBase{TSelf}.MinMagnitudeNumber"/>
    static Int256 INumberBase<Int256>.MinMagnitudeNumber(Int256 x, Int256 y) => MinMagnitude(x, y);

    /// <inheritdoc cref="INumberBase{TSelf}.MaxMagnitudeNumber"/>
    static Int256 INumberBase<Int256>.MaxMagnitudeNumber(Int256 x, Int256 y) => MaxMagnitude(x, y);

    /// <inheritdoc cref="INumber{TSelf}.MinNumber"/>
    static Int256 INumber<Int256>.MinNumber(Int256 x, Int256 y) => Min(x, y);

    /// <inheritdoc cref="INumber{TSelf}.MaxNumber"/>
    static Int256 INumber<Int256>.MaxNumber(Int256 x, Int256 y) => Max(x, y);
}
