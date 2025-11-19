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

using OnixLabs.Core;

namespace OnixLabs.Units;

// ReSharper disable MemberCanBePrivate.Global
public readonly partial struct Area<T>
{
    /// <summary>
    /// Compares two specified <see cref="Area{T}"/> values and returns an integer that indicates their relative order.
    /// </summary>
    /// <param name="left">The left-hand value to compare.</param>
    /// <param name="right">The right-hand value to compare.</param>
    /// <returns>
    /// Returns a value that indicates the relative order of the values being compared.
    /// Less than zero if <paramref name="left"/> is less than <paramref name="right"/>,
    /// zero if they are equal, and greater than zero if <paramref name="left"/> is greater than <paramref name="right"/>.
    /// </returns>
    public static int Compare(Area<T> left, Area<T> right) => left.SquareYoctoMeters.CompareTo(right.SquareYoctoMeters);

    /// <summary>
    /// Compares the current instance with another <see cref="Area{T}"/> instance and returns an integer that indicates their relative order.
    /// </summary>
    /// <param name="other">An <see cref="Area{T}"/> instance to compare with the current instance.</param>
    /// <returns>
    /// Returns a value that indicates the relative order of the instances being compared.
    /// Less than zero if the current instance is less than <paramref name="other"/>,
    /// zero if they are equal, and greater than zero if the current instance is greater than <paramref name="other"/>.
    /// </returns>
    public int CompareTo(Area<T> other) => Compare(this, other);

    /// <summary>
    /// Compares the current instance with another object and returns an integer that indicates their relative order.
    /// </summary>
    /// <param name="obj">An object to compare with the current instance.</param>
    /// <returns>
    /// Returns a value that indicates the relative order of the objects being compared.
    /// Less than zero if the current instance is less than <paramref name="obj"/>,
    /// zero if they are equal, and greater than zero if the current instance is greater than <paramref name="obj"/>.
    /// </returns>
    public int CompareTo(object? obj) => this.CompareToObject(obj);

    /// <summary>
    /// Determines whether the left-hand value is greater than the right-hand value.
    /// </summary>
    /// <param name="left">The left-hand value to compare.</param>
    /// <param name="right">The right-hand value to compare.</param>
    /// <returns>
    /// Returns <see langword="true"/> if <paramref name="left"/> is greater than <paramref name="right"/>; otherwise, <see langword="false"/>.
    /// </returns>
    public static bool operator >(Area<T> left, Area<T> right) => Compare(left, right) is 1;

    /// <summary>
    /// Determines whether the left-hand value is greater than or equal to the right-hand value.
    /// </summary>
    /// <param name="left">The left-hand value to compare.</param>
    /// <param name="right">The right-hand value to compare.</param>
    /// <returns>
    /// Returns <see langword="true"/> if <paramref name="left"/> is greater than or equal to <paramref name="right"/>; otherwise, <see langword="false"/>.
    /// </returns>
    public static bool operator >=(Area<T> left, Area<T> right) => Compare(left, right) is 1 or 0;

    /// <summary>
    /// Determines whether the left-hand value is less than the right-hand value.
    /// </summary>
    /// <param name="left">The left-hand value to compare.</param>
    /// <param name="right">The right-hand value to compare.</param>
    /// <returns>
    /// Returns <see langword="true"/> if <paramref name="left"/> is less than <paramref name="right"/>; otherwise, <see langword="false"/>.
    /// </returns>
    public static bool operator <(Area<T> left, Area<T> right) => Compare(left, right) is -1;

    /// <summary>
    /// Determines whether the left-hand value is less than or equal to the right-hand value.
    /// </summary>
    /// <param name="left">The left-hand value to compare.</param>
    /// <param name="right">The right-hand value to compare.</param>
    /// <returns>
    /// Returns <see langword="true"/> if <paramref name="left"/> is less than or equal to <paramref name="right"/>; otherwise, <see langword="false"/>.
    /// </returns>
    public static bool operator <=(Area<T> left, Area<T> right) => Compare(left, right) is -1 or 0;
}
