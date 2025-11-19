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

// ReSharper disable MemberCanBePrivate.Global
public readonly partial struct Area<T>
{
    /// <summary>
    /// Determines whether two specified <see cref="Area{T}"/> values are equal.
    /// </summary>
    /// <param name="left">The left-hand value to compare.</param>
    /// <param name="right">The right-hand value to compare.</param>
    /// <returns>
    /// Returns <see langword="true"/> if the two specified <see cref="Area{T}"/> instances are equal; otherwise, <see langword="false"/>.
    /// </returns>
    public static bool Equals(Area<T> left, Area<T> right) => Equals(left.SquareYoctoMeters, right.SquareYoctoMeters);

    /// <summary>
    /// Determines whether the current <see cref="Area{T}"/> instance is equal to another <see cref="Area{T}"/> instance.
    /// </summary>
    /// <param name="other">The other <see cref="Area{T}"/> instance to compare with the current instance.</param>
    /// <returns>
    /// Returns <see langword="true"/> if the current instance is equal to <paramref name="other"/>; otherwise, <see langword="false"/>.
    /// </returns>
    public bool Equals(Area<T> other) => Equals(this, other);

    /// <summary>
    /// Determines whether the current <see cref="Area{T}"/> instance is equal to a specified object.
    /// </summary>
    /// <param name="obj">The object to compare with the current instance.</param>
    /// <returns>
    /// Returns <see langword="true"/> if <paramref name="obj"/> is an <see cref="Area{T}"/> and is equal to the current instance; otherwise, <see langword="false"/>.
    /// </returns>
    public override bool Equals([NotNullWhen(true)] object? obj) => obj is Area<T> other && Equals(other);

    /// <summary>
    /// Returns a hash code for the current <see cref="Area{T}"/> instance.
    /// </summary>
    /// <returns>Returns a hash code for the current <see cref="Area{T}"/> instance.</returns>
    public override int GetHashCode() => SquareYoctoMeters.GetHashCode();

    /// <summary>
    /// Determines whether two specified <see cref="Area{T}"/> instances are equal.
    /// </summary>
    /// <param name="left">The left-hand value to compare.</param>
    /// <param name="right">The right-hand value to compare.</param>
    /// <returns>
    /// Returns <see langword="true"/> if the specified <see cref="Area{T}"/> instances are equal; otherwise, <see langword="false"/>.
    /// </returns>
    public static bool operator ==(Area<T> left, Area<T> right) => Equals(left, right);

    /// <summary>
    /// Determines whether two specified <see cref="Area{T}"/> instances are not equal.
    /// </summary>
    /// <param name="left">The left-hand value to compare.</param>
    /// <param name="right">The right-hand value to compare.</param>
    /// <returns>
    /// Returns <see langword="true"/> if the specified <see cref="Area{T}"/> instances are not equal; otherwise, <see langword="false"/>.
    /// </returns>
    public static bool operator !=(Area<T> left, Area<T> right) => !Equals(left, right);
}
