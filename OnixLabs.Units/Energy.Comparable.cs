// Copyright 2020-2025 ONIXLabs
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
using OnixLabs.Core;

namespace OnixLabs.Units;

public readonly partial struct Energy<T>
{
/// <summary>
    /// Compares the current instance with another object of the same type and returns an integer that indicates
    /// whether the current instance precedes, follows, or occurs in the same position in the sort order as the other object.
    /// </summary>
    /// <param name="obj">An object to compare with this instance.</param>
    /// <returns>
    /// A value that indicates the relative order of the objects being compared.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// Thrown if the specified <paramref name="obj"/> is not an instance of <see cref="Energy{T}"/>.
    /// </exception>
    public int CompareTo(object? obj)
    {
        if (obj is null) return 1;
        if (obj is Energy<T> other) return CompareTo(other);
        throw new ArgumentException("Object must be of type Energy<T>.", nameof(obj));
    }

    /// <summary>
    /// Compares the current instance with another <see cref="Energy{T}"/> instance and returns an integer that indicates
    /// whether the current instance precedes, follows, or occurs in the same position in the sort order as the other instance.
    /// </summary>
    /// <param name="other">An <see cref="Energy{T}"/> instance to compare with this instance.</param>
    /// <returns>
    /// A value that indicates the relative order of the instances being compared.
    /// </returns>
    public int CompareTo(Energy<T> other) => YoctoJoules.CompareTo(other.YoctoJoules);

    /// <summary>
    /// Compares two <see cref="Energy{T}"/> instances and returns a value that indicates their relative order.
    /// </summary>
    /// <param name="left">The left-hand value to compare.</param>
    /// <param name="right">The right-hand value to compare.</param>
    /// <returns>
    /// A signed integer that indicates the relative values of <paramref name="left"/> and <paramref name="right"/>.
    /// </returns>
    public static int Compare(Energy<T> left, Energy<T> right) => left.CompareTo(right);

    /// <summary>
    /// Determines whether the left-hand value is greater than the right-hand value.
    /// </summary>
    /// <param name="left">The left-hand value to compare.</param>
    /// <param name="right">The right-hand value to compare.</param>
    /// <returns>
    /// Returns <see langword="true"/> if the left-hand operand is greater than the right-hand operand; otherwise, <see langword="false"/>.
    /// </returns>
    public static bool operator >(Energy<T> left, Energy<T> right) => Compare(left, right) > 0;

    /// <summary>
    /// Determines whether the left-hand value is greater than or equal to the right-hand value.
    /// </summary>
    /// <param name="left">The left-hand value to compare.</param>
    /// <param name="right">The right-hand value to compare.</param>
    /// <returns>
    /// Returns <see langword="true"/> if the left-hand operand is greater than or equal to the right-hand operand; otherwise, <see langword="false"/>.
    /// </returns>
    public static bool operator >=(Energy<T> left, Energy<T> right) => Compare(left, right) >= 0;

    /// <summary>
    /// Determines whether the left-hand value is less than the right-hand value.
    /// </summary>
    /// <param name="left">The left-hand value to compare.</param>
    /// <param name="right">The right-hand value to compare.</param>
    /// <returns>
    /// Returns <see langword="true"/> if the left-hand operand is less than the right-hand operand; otherwise, <see langword="false"/>.
    /// </returns>
    public static bool operator <(Energy<T> left, Energy<T> right) => Compare(left, right) < 0;

    /// <summary>
    /// Determines whether the left-hand value is less than or equal to the right-hand value.
    /// </summary>
    /// <param name="left">The left-hand value to compare.</param>
    /// <param name="right">The right-hand value to compare.</param>
    /// <returns>
    /// Returns <see langword="true"/> if the left-hand operand is less than or equal to the right-hand operand; otherwise, <see langword="false"/>.
    /// </returns>
    public static bool operator <=(Energy<T> left, Energy<T> right) => Compare(left, right) <= 0;
}
