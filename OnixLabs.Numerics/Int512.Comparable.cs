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

public readonly partial struct Int512
{
    /// <summary>Compares two <see cref="Int512"/> values and returns an integer that indicates their relative order, using signed semantics.</summary>
    /// <param name="left">The <paramref name="left"/> value to compare.</param>
    /// <param name="right">The <paramref name="right"/> value to compare.</param>
    /// <returns>Returns a negative value when <paramref name="left"/> precedes, zero when equal, and a positive value when <paramref name="left"/> follows.</returns>
    public static int Compare(Int512 left, Int512 right)
    {
        bool leftNegative = IsNegative(left);
        bool rightNegative = IsNegative(right);

        if (leftNegative && !rightNegative) return -1;
        if (!leftNegative && rightNegative) return 1;

        if (left.UpperBits < right.UpperBits) return -1;
        if (left.UpperBits > right.UpperBits) return 1;
        if (left.LowerBits < right.LowerBits) return -1;
        if (left.LowerBits > right.LowerBits) return 1;
        return 0;
    }

    /// <summary>Compares the current instance with the specified object.</summary>
    /// <param name="obj">An object to compare with this instance.</param>
    /// <returns>Returns a negative integer, zero, or a positive integer indicating the relative order.</returns>
    /// <exception cref="ArgumentException">Thrown when <paramref name="obj"/> is not <see langword="null"/> and not an <see cref="Int512"/>.</exception>
    public int CompareTo(object? obj)
    {
        if (obj is null) return 1;
        if (obj is Int512 other) return CompareTo(other);
        throw new ArgumentException($"Object must be of type {nameof(Int512)}.", nameof(obj));
    }

    /// <summary>Compares the current instance with another <see cref="Int512"/> value.</summary>
    /// <param name="other">An <see cref="Int512"/> value to compare with this instance.</param>
    /// <returns>Returns a negative integer, zero, or a positive integer indicating the relative order.</returns>
    public int CompareTo(Int512 other) => Compare(this, other);

    /// <summary>Determines whether <paramref name="left"/> is less than <paramref name="right"/>.</summary>
    /// <param name="left">The <paramref name="left"/> value to compare.</param>
    /// <param name="right">The <paramref name="right"/> value to compare.</param>
    /// <returns>Returns <see langword="true"/> if <paramref name="left"/> is less than <paramref name="right"/>; otherwise, <see langword="false"/>.</returns>
    public static bool operator <(Int512 left, Int512 right) => Compare(left, right) < 0;

    /// <summary>Determines whether <paramref name="left"/> is less than or equal to <paramref name="right"/>.</summary>
    /// <param name="left">The <paramref name="left"/> value to compare.</param>
    /// <param name="right">The <paramref name="right"/> value to compare.</param>
    /// <returns>Returns <see langword="true"/> if <paramref name="left"/> is less than or equal to <paramref name="right"/>; otherwise, <see langword="false"/>.</returns>
    public static bool operator <=(Int512 left, Int512 right) => Compare(left, right) <= 0;

    /// <summary>Determines whether <paramref name="left"/> is greater than <paramref name="right"/>.</summary>
    /// <param name="left">The <paramref name="left"/> value to compare.</param>
    /// <param name="right">The <paramref name="right"/> value to compare.</param>
    /// <returns>Returns <see langword="true"/> if <paramref name="left"/> is greater than <paramref name="right"/>; otherwise, <see langword="false"/>.</returns>
    public static bool operator >(Int512 left, Int512 right) => Compare(left, right) > 0;

    /// <summary>Determines whether <paramref name="left"/> is greater than or equal to <paramref name="right"/>.</summary>
    /// <param name="left">The <paramref name="left"/> value to compare.</param>
    /// <param name="right">The <paramref name="right"/> value to compare.</param>
    /// <returns>Returns <see langword="true"/> if <paramref name="left"/> is greater than or equal to <paramref name="right"/>; otherwise, <see langword="false"/>.</returns>
    public static bool operator >=(Int512 left, Int512 right) => Compare(left, right) >= 0;
}
