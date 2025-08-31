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

using System.Diagnostics.CodeAnalysis;

namespace OnixLabs.Units;

public readonly partial struct Temperature<T>
{
    /// <summary>
    /// Compares two instances of <see cref="Temperature{T}"/> to determine whether their values are equal.
    /// </summary>
    /// <param name="left">The left-hand value to compare.</param>
    /// <param name="right">The right-hand value to compare.</param>
    /// <returns>Returns <see langword="true"/> if the two specified instances are equal; otherwise, <see langword="false"/>.</returns>
    public static bool Equals(Temperature<T> left, Temperature<T> right) => Equals(left.Kelvin, right.Kelvin);

    /// <summary>
    /// Compares the current instance of <see cref="Temperature{T}"/> with the specified other instance of <see cref="Temperature{T}"/>.
    /// </summary>
    /// <param name="other">The other instance of <see cref="Temperature{T}"/> to compare with the current instance.</param>
    /// <returns>Returns <see langword="true"/> if the current instance is equal to the specified other instance; otherwise, <see langword="false"/>.</returns>
    public bool Equals(Temperature<T> other) => Equals(this, other);

    /// <summary>
    /// Checks for equality between this instance and another object.
    /// </summary>
    /// <param name="obj">The object to check for equality.</param>
    /// <returns>Returns <see langword="true"/> if the object is equal to this instance; otherwise, <see langword="false"/>.</returns>
    public override bool Equals([NotNullWhen(true)] object? obj) => obj is Temperature<T> other && Equals(other);

    /// <summary>
    /// Serves as a hash code function for this instance.
    /// </summary>
    /// <returns>A hash code for this instance.</returns>
    public override int GetHashCode() => Kelvin.GetHashCode();

    /// <summary>
    /// Compares two instances of <see cref="Temperature{T}"/> to determine whether their values are equal.
    /// </summary>
    /// <param name="left">The left-hand value to compare.</param>
    /// <param name="right">The right-hand value to compare.</param>
    /// <returns>Returns <see langword="true"/> if the two specified instances are equal; otherwise, <see langword="false"/>.</returns>
    public static bool operator ==(Temperature<T> left, Temperature<T> right) => Equals(left, right);

    /// <summary>
    /// Compares two instances of <see cref="Temperature{T}"/> to determine whether their values are not equal.
    /// </summary>
    /// <param name="left">The left-hand value to compare.</param>
    /// <param name="right">The right-hand value to compare.</param>
    /// <returns>Returns <see langword="true"/> if the two specified instances are not equal; otherwise, <see langword="false"/>.</returns>
    public static bool operator !=(Temperature<T> left, Temperature<T> right) => !Equals(left, right);
}
