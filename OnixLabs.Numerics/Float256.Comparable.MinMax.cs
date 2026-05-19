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

public readonly partial struct Float256
{
    /// <summary>Returns the smaller of two <see cref="Float256"/> values, propagating NaN.</summary>
    /// <param name="x">The first value to compare.</param>
    /// <param name="y">The second value to compare.</param>
    /// <returns>Returns the smaller value; NaN if either operand is NaN.</returns>
    public static Float256 Min(Float256 x, Float256 y)
    {
        if (IsNaN(x) || IsNaN(y)) return NaN;
        int comparison = Compare(x, y);
        if (comparison < 0) return x;
        if (comparison > 0) return y;
        return IsNegative(x) ? x : y;
    }

    /// <summary>Returns the larger of two <see cref="Float256"/> values, propagating NaN.</summary>
    /// <param name="x">The first value to compare.</param>
    /// <param name="y">The second value to compare.</param>
    /// <returns>Returns the larger value; NaN if either operand is NaN.</returns>
    public static Float256 Max(Float256 x, Float256 y)
    {
        if (IsNaN(x) || IsNaN(y)) return NaN;
        int comparison = Compare(x, y);
        if (comparison > 0) return x;
        if (comparison < 0) return y;
        return IsNegative(x) ? y : x;
    }

    /// <summary>Returns the smaller value, treating NaN as missing.</summary>
    /// <param name="x">The first value to compare.</param>
    /// <param name="y">The second value to compare.</param>
    /// <returns>Returns the smaller value; the non-NaN operand if exactly one is NaN.</returns>
    public static Float256 MinNumber(Float256 x, Float256 y)
    {
        if (IsNaN(x)) return IsNaN(y) ? NaN : y;
        if (IsNaN(y)) return x;
        return Min(x, y);
    }

    /// <summary>Returns the larger value, treating NaN as missing.</summary>
    /// <param name="x">The first value to compare.</param>
    /// <param name="y">The second value to compare.</param>
    /// <returns>Returns the larger value; the non-NaN operand if exactly one is NaN.</returns>
    public static Float256 MaxNumber(Float256 x, Float256 y)
    {
        if (IsNaN(x)) return IsNaN(y) ? NaN : y;
        if (IsNaN(y)) return x;
        return Max(x, y);
    }

    /// <summary>Returns the value with the smaller magnitude, propagating NaN.</summary>
    /// <param name="x">The first value to compare.</param>
    /// <param name="y">The second value to compare.</param>
    /// <returns>Returns the value with the smaller absolute magnitude; NaN if either operand is NaN.</returns>
    public static Float256 MinMagnitude(Float256 x, Float256 y)
    {
        if (IsNaN(x) || IsNaN(y)) return NaN;
        Float256 absoluteX = Abs(x);
        Float256 absoluteY = Abs(y);
        if (absoluteX < absoluteY) return x;
        if (absoluteX > absoluteY) return y;
        return IsNegative(x) ? x : y;
    }

    /// <summary>Returns the value with the larger magnitude, propagating NaN.</summary>
    /// <param name="x">The first value to compare.</param>
    /// <param name="y">The second value to compare.</param>
    /// <returns>Returns the value with the larger absolute magnitude; NaN if either operand is NaN.</returns>
    public static Float256 MaxMagnitude(Float256 x, Float256 y)
    {
        if (IsNaN(x) || IsNaN(y)) return NaN;
        Float256 absoluteX = Abs(x);
        Float256 absoluteY = Abs(y);
        if (absoluteX > absoluteY) return x;
        if (absoluteX < absoluteY) return y;
        return IsNegative(x) ? y : x;
    }

    /// <summary>Returns the value with the smaller magnitude, treating NaN as missing.</summary>
    /// <param name="x">The first value to compare.</param>
    /// <param name="y">The second value to compare.</param>
    /// <returns>Returns the value with the smaller absolute magnitude; the non-NaN operand if exactly one is NaN.</returns>
    public static Float256 MinMagnitudeNumber(Float256 x, Float256 y)
    {
        if (IsNaN(x)) return IsNaN(y) ? NaN : y;
        if (IsNaN(y)) return x;
        return MinMagnitude(x, y);
    }

    /// <summary>Returns the value with the larger magnitude, treating NaN as missing.</summary>
    /// <param name="x">The first value to compare.</param>
    /// <param name="y">The second value to compare.</param>
    /// <returns>Returns the value with the larger absolute magnitude; the non-NaN operand if exactly one is NaN.</returns>
    public static Float256 MaxMagnitudeNumber(Float256 x, Float256 y)
    {
        if (IsNaN(x)) return IsNaN(y) ? NaN : y;
        if (IsNaN(y)) return x;
        return MaxMagnitude(x, y);
    }
}
