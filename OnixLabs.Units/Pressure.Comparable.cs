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
public readonly partial struct Pressure<T>
{
    /// <summary>
    /// Compares two <see cref="Pressure{T}"/> values and returns an integer that indicates
    /// whether the left-hand value is less than, equal to, or greater than the right-hand value.
    /// </summary>
    /// <param name="left">The left-hand value to compare.</param>
    /// <param name="right">The right-hand value to compare.</param>
    /// <returns>
    /// Returns a value that indicates the relative order of the values being compared.
    /// The return value is less than zero if <paramref name="left"/> is less than <paramref name="right"/>,
    /// zero if <paramref name="left"/> equals <paramref name="right"/>,
    /// or greater than zero if <paramref name="left"/> is greater than <paramref name="right"/>.
    /// </returns>
    public static int Compare(Pressure<T> left, Pressure<T> right) => left.QuectoPascals.CompareTo(right.QuectoPascals);

    /// <summary>
    /// Compares the current instance with another object of the same type and returns an integer that indicates
    /// whether the current instance precedes, follows, or occurs in the same position in the sort order as the
    /// other object.
    /// </summary>
    /// <param name="other">An object to compare with this instance.</param>
    /// <returns>
    /// Returns a value that indicates the relative order of the values being compared.
    /// The return value is less than zero if the current instance is less than <paramref name="other"/>,
    /// zero if the current instance equals <paramref name="other"/>,
    /// or greater than zero if the current instance is greater than <paramref name="other"/>.
    /// </returns>
    public int CompareTo(Pressure<T> other) => Compare(this, other);

    /// <summary>
    /// Compares the current instance with another object of the same type and returns an integer that indicates
    /// whether the current instance precedes, follows, or occurs in the same position in the sort order as the
    /// other object.
    /// </summary>
    /// <param name="obj">An object to compare with this instance.</param>
    /// <returns>Returns a value that indicates the relative order of the objects being compared.</returns>
    // ReSharper disable once HeapView.BoxingAllocation
    public int CompareTo(object? obj) => this.CompareToObject(obj);

    /// <summary>
    /// Determines whether the left-hand value is greater than the right-hand value.
    /// </summary>
    /// <param name="left">The left-hand value to compare.</param>
    /// <param name="right">The right-hand value to compare.</param>
    /// <returns>
    /// Returns <see langword="true"/> if the left-hand value is greater than the right-hand value;
    /// otherwise, <see langword="false"/>.
    /// </returns>
    public static bool operator >(Pressure<T> left, Pressure<T> right) => Compare(left, right) is 1;

    /// <summary>
    /// Determines whether the left-hand value is greater than or equal to the right-hand value.
    /// </summary>
    /// <param name="left">The left-hand value to compare.</param>
    /// <param name="right">The right-hand value to compare.</param>
    /// <returns>
    /// Returns <see langword="true"/> if the left-hand value is greater than or equal to the right-hand value;
    /// otherwise, <see langword="false"/>.
    /// </returns>
    public static bool operator >=(Pressure<T> left, Pressure<T> right) => Compare(left, right) is 1 or 0;

    /// <summary>
    /// Determines whether the left-hand value is less than the right-hand value.
    /// </summary>
    /// <param name="left">The left-hand value to compare.</param>
    /// <param name="right">The right-hand value to compare.</param>
    /// <returns>
    /// Returns <see langword="true"/> if the left-hand value is less than the right-hand value;
    /// otherwise, <see langword="false"/>.
    /// </returns>
    public static bool operator <(Pressure<T> left, Pressure<T> right) => Compare(left, right) is -1;

    /// <summary>
    /// Determines whether the left-hand value is less than or equal to the right-hand value.
    /// </summary>
    /// <param name="left">The left-hand value to compare.</param>
    /// <param name="right">The right-hand value to compare.</param>
    /// <returns>
    /// Returns <see langword="true"/> if the left-hand value is less than or equal to the right-hand value;
    /// otherwise, <see langword="false"/>.
    /// </returns>
    public static bool operator <=(Pressure<T> left, Pressure<T> right) => Compare(left, right) is -1 or 0;
}
