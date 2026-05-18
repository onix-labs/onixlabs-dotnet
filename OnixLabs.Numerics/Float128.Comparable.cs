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

public readonly partial struct Float128
{
    /// <summary>
    /// Compares two <see cref="Float128"/> values and returns an integer that indicates
    /// whether the <paramref name="left"/> value is less than, equal to, or greater than the <paramref name="right"/> value.
    /// </summary>
    /// <param name="left">The <paramref name="left"/> value to compare.</param>
    /// <param name="right">The <paramref name="right"/> value to compare.</param>
    /// <returns>Returns a value that indicates the relative order of the values being compared.</returns>
    /// <remarks>
    /// This method imposes a total order on <see cref="Float128"/> values for use by sorted collections:
    /// any NaN value sorts before all numeric values; negative infinity is the smallest numeric value;
    /// positive zero and negative zero compare equal.
    /// </remarks>
    public static int Compare(Float128 left, Float128 right)
    {
        bool leftNaN = IsNaNBits(left.RawBits);
        bool rightNaN = IsNaNBits(right.RawBits);

        if (leftNaN) return rightNaN ? 0 : -1;
        if (rightNaN) return 1;
        if (IsZeroBits(left.RawBits) && IsZeroBits(right.RawBits)) return 0;

        UInt128 leftKey = ExtractSignBit(left.RawBits) ? ~left.RawBits : left.RawBits | SignMask;
        UInt128 rightKey = ExtractSignBit(right.RawBits) ? ~right.RawBits : right.RawBits | SignMask;

        return leftKey.CompareTo(rightKey);
    }

    /// <summary>
    /// Compares the current instance with another object of the same type and returns an integer that indicates
    /// whether the current instance precedes, follows, or occurs in the same position in the sort order as the
    /// other object.
    /// </summary>
    /// <param name="obj">An object to compare with this instance.</param>
    /// <returns>Returns a value that indicates the relative order of the objects being compared.</returns>
    /// <exception cref="ArgumentException">Thrown when <paramref name="obj"/> is not <see langword="null"/> and not a <see cref="Float128"/>.</exception>
    public int CompareTo(object? obj)
    {
        if (obj is null) return 1;
        if (obj is Float128 other) return CompareTo(other);
        throw new ArgumentException($"Object must be of type {nameof(Float128)}.", nameof(obj));
    }

    /// <summary>
    /// Compares the current instance with another <see cref="Float128"/> value and returns an integer that indicates
    /// whether the current instance precedes, follows, or occurs in the same position in the sort order as the
    /// other value.
    /// </summary>
    /// <param name="other">A <see cref="Float128"/> value to compare with this instance.</param>
    /// <returns>Returns a value that indicates the relative order of the values being compared.</returns>
    public int CompareTo(Float128 other) => Compare(this, other);

    /// <summary>
    /// Determines whether the <paramref name="left"/> value is less than the <paramref name="right"/> value.
    /// </summary>
    /// <param name="left">The <paramref name="left"/> value to compare.</param>
    /// <param name="right">The <paramref name="right"/> value to compare.</param>
    /// <returns>Returns <see langword="true"/> if the <paramref name="left"/> operand is less than the <paramref name="right"/> operand; otherwise, <see langword="false"/>.</returns>
    /// <remarks>If either operand is NaN this comparison returns <see langword="false"/>, matching IEEE 754 ordering semantics.</remarks>
    public static bool operator <(Float128 left, Float128 right)
    {
        if (IsNaNBits(left.RawBits) || IsNaNBits(right.RawBits)) return false;
        return Compare(left, right) < 0;
    }

    /// <summary>
    /// Determines whether the <paramref name="left"/> value is less than or equal to the <paramref name="right"/> value.
    /// </summary>
    /// <param name="left">The <paramref name="left"/> value to compare.</param>
    /// <param name="right">The <paramref name="right"/> value to compare.</param>
    /// <returns>Returns <see langword="true"/> if the <paramref name="left"/> operand is less than or equal to the <paramref name="right"/> operand; otherwise, <see langword="false"/>.</returns>
    /// <remarks>If either operand is NaN this comparison returns <see langword="false"/>, matching IEEE 754 ordering semantics.</remarks>
    public static bool operator <=(Float128 left, Float128 right)
    {
        if (IsNaNBits(left.RawBits) || IsNaNBits(right.RawBits)) return false;
        return Compare(left, right) <= 0;
    }

    /// <summary>
    /// Determines whether the <paramref name="left"/> value is greater than the <paramref name="right"/> value.
    /// </summary>
    /// <param name="left">The <paramref name="left"/> value to compare.</param>
    /// <param name="right">The <paramref name="right"/> value to compare.</param>
    /// <returns>Returns <see langword="true"/> if the <paramref name="left"/> operand is greater than the <paramref name="right"/> operand; otherwise, <see langword="false"/>.</returns>
    /// <remarks>If either operand is NaN this comparison returns <see langword="false"/>, matching IEEE 754 ordering semantics.</remarks>
    public static bool operator >(Float128 left, Float128 right)
    {
        if (IsNaNBits(left.RawBits) || IsNaNBits(right.RawBits)) return false;
        return Compare(left, right) > 0;
    }

    /// <summary>
    /// Determines whether the <paramref name="left"/> value is greater than or equal to the <paramref name="right"/> value.
    /// </summary>
    /// <param name="left">The <paramref name="left"/> value to compare.</param>
    /// <param name="right">The <paramref name="right"/> value to compare.</param>
    /// <returns>Returns <see langword="true"/> if the <paramref name="left"/> operand is greater than or equal to the <paramref name="right"/> operand; otherwise, <see langword="false"/>.</returns>
    /// <remarks>If either operand is NaN this comparison returns <see langword="false"/>, matching IEEE 754 ordering semantics.</remarks>
    public static bool operator >=(Float128 left, Float128 right)
    {
        if (IsNaNBits(left.RawBits) || IsNaNBits(right.RawBits)) return false;
        return Compare(left, right) >= 0;
    }
}
