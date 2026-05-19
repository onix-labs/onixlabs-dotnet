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
    /// <summary>Returns the smaller of two <see cref="Int512"/> values (signed comparison).</summary>
    /// <param name="x">The first value.</param>
    /// <param name="y">The second value.</param>
    /// <returns>Returns the smaller of <paramref name="x"/> and <paramref name="y"/>.</returns>
    public static Int512 Min(Int512 x, Int512 y) => x <= y ? x : y;

    /// <summary>Returns the larger of two <see cref="Int512"/> values (signed comparison).</summary>
    /// <param name="x">The first value.</param>
    /// <param name="y">The second value.</param>
    /// <returns>Returns the larger of <paramref name="x"/> and <paramref name="y"/>.</returns>
    public static Int512 Max(Int512 x, Int512 y) => x >= y ? x : y;

    /// <summary>Returns <paramref name="value"/> clamped to the inclusive range <c>[min, max]</c>.</summary>
    /// <param name="value">The value to clamp.</param>
    /// <param name="min">The lower bound of the result.</param>
    /// <param name="max">The upper bound of the result.</param>
    /// <returns>Returns <paramref name="value"/> clamped to <c>[min, max]</c>.</returns>
    /// <exception cref="ArgumentException">Thrown when <paramref name="min"/> is greater than <paramref name="max"/>.</exception>
    public static Int512 Clamp(Int512 value, Int512 min, Int512 max)
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
    public static Int512 MinMagnitude(Int512 x, Int512 y)
    {
        Int512 absX = AbsUnchecked(x);
        Int512 absY = AbsUnchecked(y);
        if (absX < absY) return x;
        if (absX > absY) return y;
        return IsNegative(x) ? x : y;
    }

    /// <summary>Returns the value with the larger magnitude.</summary>
    /// <param name="x">The first value.</param>
    /// <param name="y">The second value.</param>
    /// <returns>Returns the value with the larger absolute magnitude.</returns>
    public static Int512 MaxMagnitude(Int512 x, Int512 y)
    {
        Int512 absX = AbsUnchecked(x);
        Int512 absY = AbsUnchecked(y);
        if (absX > absY) return x;
        if (absX < absY) return y;
        return IsNegative(x) ? y : x;
    }

    /// <inheritdoc cref="INumberBase{TSelf}.MinMagnitudeNumber"/>
    static Int512 INumberBase<Int512>.MinMagnitudeNumber(Int512 x, Int512 y) => MinMagnitude(x, y);

    /// <inheritdoc cref="INumberBase{TSelf}.MaxMagnitudeNumber"/>
    static Int512 INumberBase<Int512>.MaxMagnitudeNumber(Int512 x, Int512 y) => MaxMagnitude(x, y);

    /// <inheritdoc cref="INumber{TSelf}.MinNumber"/>
    static Int512 INumber<Int512>.MinNumber(Int512 x, Int512 y) => Min(x, y);

    /// <inheritdoc cref="INumber{TSelf}.MaxNumber"/>
    static Int512 INumber<Int512>.MaxNumber(Int512 x, Int512 y) => Max(x, y);
}
