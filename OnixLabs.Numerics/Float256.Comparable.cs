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

namespace OnixLabs.Numerics;

public readonly partial struct Float256
{
    /// <summary>
    /// Compares two <see cref="Float256"/> values and returns an integer that indicates relative order.
    /// </summary>
    /// <param name="left">The <paramref name="left"/> value to compare.</param>
    /// <param name="right">The <paramref name="right"/> value to compare.</param>
    /// <returns>Returns a negative number when <paramref name="left"/> precedes <paramref name="right"/>, zero when equal, and a positive number when <paramref name="left"/> follows.</returns>
    /// <remarks>This method imposes a total order on <see cref="Float256"/> values: NaN sorts before all numeric values; ±zero compare equal.</remarks>
    public static int Compare(Float256 left, Float256 right)
    {
        bool leftNaN = IsNaNBits(left.bits);
        bool rightNaN = IsNaNBits(right.bits);

        if (leftNaN) return rightNaN ? 0 : -1;
        if (rightNaN) return 1;
        if (IsZeroBits(left.bits) && IsZeroBits(right.bits)) return 0;

        UInt256 leftKey = ExtractSignBit(left.bits) ? ~left.bits : left.bits | SignMask;
        UInt256 rightKey = ExtractSignBit(right.bits) ? ~right.bits : right.bits | SignMask;

        if (leftKey < rightKey) return -1;
        if (leftKey > rightKey) return 1;
        return 0;
    }

    /// <summary>
    /// Compares this instance with the specified object.
    /// </summary>
    /// <param name="obj">An object to compare with this instance.</param>
    /// <returns>Returns a negative integer, zero, or a positive integer to indicate the relative order.</returns>
    /// <exception cref="ArgumentException">Thrown when <paramref name="obj"/> is not <see langword="null"/> and not a <see cref="Float256"/>.</exception>
    public int CompareTo(object? obj)
    {
        if (obj is null) return 1;
        if (obj is Float256 other) return CompareTo(other);
        throw new ArgumentException($"Object must be of type {nameof(Float256)}.", nameof(obj));
    }

    /// <summary>
    /// Compares this instance with another <see cref="Float256"/> value.
    /// </summary>
    /// <param name="other">The other <see cref="Float256"/> value to compare with.</param>
    /// <returns>Returns a negative integer, zero, or a positive integer to indicate the relative order.</returns>
    public int CompareTo(Float256 other) => Compare(this, other);

    /// <summary>Determines whether <paramref name="left"/> is less than <paramref name="right"/>.</summary>
    /// <param name="left">The <paramref name="left"/> value to compare.</param>
    /// <param name="right">The <paramref name="right"/> value to compare.</param>
    /// <returns>Returns <see langword="true"/> if <paramref name="left"/> is strictly less than <paramref name="right"/>; <see langword="false"/> if either operand is NaN.</returns>
    public static bool operator <(Float256 left, Float256 right)
    {
        if (IsNaNBits(left.bits) || IsNaNBits(right.bits)) return false;
        return Compare(left, right) < 0;
    }

    /// <summary>Determines whether <paramref name="left"/> is less than or equal to <paramref name="right"/>.</summary>
    /// <param name="left">The <paramref name="left"/> value to compare.</param>
    /// <param name="right">The <paramref name="right"/> value to compare.</param>
    /// <returns>Returns <see langword="true"/> if <paramref name="left"/> is less than or equal to <paramref name="right"/>; <see langword="false"/> if either operand is NaN.</returns>
    public static bool operator <=(Float256 left, Float256 right)
    {
        if (IsNaNBits(left.bits) || IsNaNBits(right.bits)) return false;
        return Compare(left, right) <= 0;
    }

    /// <summary>Determines whether <paramref name="left"/> is greater than <paramref name="right"/>.</summary>
    /// <param name="left">The <paramref name="left"/> value to compare.</param>
    /// <param name="right">The <paramref name="right"/> value to compare.</param>
    /// <returns>Returns <see langword="true"/> if <paramref name="left"/> is strictly greater than <paramref name="right"/>; <see langword="false"/> if either operand is NaN.</returns>
    public static bool operator >(Float256 left, Float256 right)
    {
        if (IsNaNBits(left.bits) || IsNaNBits(right.bits)) return false;
        return Compare(left, right) > 0;
    }

    /// <summary>Determines whether <paramref name="left"/> is greater than or equal to <paramref name="right"/>.</summary>
    /// <param name="left">The <paramref name="left"/> value to compare.</param>
    /// <param name="right">The <paramref name="right"/> value to compare.</param>
    /// <returns>Returns <see langword="true"/> if <paramref name="left"/> is greater than or equal to <paramref name="right"/>; <see langword="false"/> if either operand is NaN.</returns>
    public static bool operator >=(Float256 left, Float256 right)
    {
        if (IsNaNBits(left.bits) || IsNaNBits(right.bits)) return false;
        return Compare(left, right) >= 0;
    }
}
