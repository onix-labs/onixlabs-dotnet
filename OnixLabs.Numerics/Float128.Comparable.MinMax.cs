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

namespace OnixLabs.Numerics;

public readonly partial struct Float128
{
    /// <summary>
    /// Returns the smaller of two <see cref="Float128"/> values, propagating NaN.
    /// </summary>
    /// <param name="x">The first value to compare.</param>
    /// <param name="y">The second value to compare.</param>
    /// <returns>Returns the smaller of <paramref name="x"/> and <paramref name="y"/>; <see cref="NaN"/> if either operand is NaN.</returns>
    public static Float128 Min(Float128 x, Float128 y)
    {
        if (IsNaN(x) || IsNaN(y)) return NaN;
        int comparison = Compare(x, y);
        if (comparison < 0) return x;
        if (comparison > 0) return y;
        return IsNegative(x) ? x : y;
    }

    /// <summary>
    /// Returns the larger of two <see cref="Float128"/> values, propagating NaN.
    /// </summary>
    /// <param name="x">The first value to compare.</param>
    /// <param name="y">The second value to compare.</param>
    /// <returns>Returns the larger of <paramref name="x"/> and <paramref name="y"/>; <see cref="NaN"/> if either operand is NaN.</returns>
    public static Float128 Max(Float128 x, Float128 y)
    {
        if (IsNaN(x) || IsNaN(y)) return NaN;
        int comparison = Compare(x, y);
        if (comparison > 0) return x;
        if (comparison < 0) return y;
        return IsNegative(x) ? y : x;
    }

    /// <summary>
    /// Returns the smaller of two <see cref="Float128"/> values, treating NaN as missing.
    /// </summary>
    /// <param name="x">The first value to compare.</param>
    /// <param name="y">The second value to compare.</param>
    /// <returns>Returns the smaller of <paramref name="x"/> and <paramref name="y"/>; if exactly one operand is NaN, returns the non-NaN operand; if both are NaN, returns NaN.</returns>
    public static Float128 MinNumber(Float128 x, Float128 y)
    {
        if (IsNaN(x)) return IsNaN(y) ? NaN : y;
        if (IsNaN(y)) return x;
        return Min(x, y);
    }

    /// <summary>
    /// Returns the larger of two <see cref="Float128"/> values, treating NaN as missing.
    /// </summary>
    /// <param name="x">The first value to compare.</param>
    /// <param name="y">The second value to compare.</param>
    /// <returns>Returns the larger of <paramref name="x"/> and <paramref name="y"/>; if exactly one operand is NaN, returns the non-NaN operand; if both are NaN, returns NaN.</returns>
    public static Float128 MaxNumber(Float128 x, Float128 y)
    {
        if (IsNaN(x)) return IsNaN(y) ? NaN : y;
        if (IsNaN(y)) return x;
        return Max(x, y);
    }

    /// <summary>
    /// Returns the <see cref="Float128"/> with the smaller magnitude, propagating NaN.
    /// </summary>
    /// <param name="x">The first value to compare.</param>
    /// <param name="y">The second value to compare.</param>
    /// <returns>Returns the value with the smaller absolute magnitude; <see cref="NaN"/> if either operand is NaN; ties prefer the negative value.</returns>
    public static Float128 MinMagnitude(Float128 x, Float128 y)
    {
        if (IsNaN(x) || IsNaN(y)) return NaN;
        Float128 absoluteX = Abs(x);
        Float128 absoluteY = Abs(y);
        if (absoluteX < absoluteY) return x;
        if (absoluteX > absoluteY) return y;
        return IsNegative(x) ? x : y;
    }

    /// <summary>
    /// Returns the <see cref="Float128"/> with the larger magnitude, propagating NaN.
    /// </summary>
    /// <param name="x">The first value to compare.</param>
    /// <param name="y">The second value to compare.</param>
    /// <returns>Returns the value with the larger absolute magnitude; <see cref="NaN"/> if either operand is NaN; ties prefer the positive value.</returns>
    public static Float128 MaxMagnitude(Float128 x, Float128 y)
    {
        if (IsNaN(x) || IsNaN(y)) return NaN;
        Float128 absoluteX = Abs(x);
        Float128 absoluteY = Abs(y);
        if (absoluteX > absoluteY) return x;
        if (absoluteX < absoluteY) return y;
        return IsNegative(x) ? y : x;
    }

    /// <summary>
    /// Returns the <see cref="Float128"/> with the smaller magnitude, treating NaN as missing.
    /// </summary>
    /// <param name="x">The first value to compare.</param>
    /// <param name="y">The second value to compare.</param>
    /// <returns>Returns the value with the smaller absolute magnitude; if exactly one operand is NaN, returns the non-NaN operand; if both are NaN, returns NaN.</returns>
    public static Float128 MinMagnitudeNumber(Float128 x, Float128 y)
    {
        if (IsNaN(x)) return IsNaN(y) ? NaN : y;
        if (IsNaN(y)) return x;
        return MinMagnitude(x, y);
    }

    /// <summary>
    /// Returns the <see cref="Float128"/> with the larger magnitude, treating NaN as missing.
    /// </summary>
    /// <param name="x">The first value to compare.</param>
    /// <param name="y">The second value to compare.</param>
    /// <returns>Returns the value with the larger absolute magnitude; if exactly one operand is NaN, returns the non-NaN operand; if both are NaN, returns NaN.</returns>
    public static Float128 MaxMagnitudeNumber(Float128 x, Float128 y)
    {
        if (IsNaN(x)) return IsNaN(y) ? NaN : y;
        if (IsNaN(y)) return x;
        return MaxMagnitude(x, y);
    }
}
