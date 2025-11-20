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
using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using OnixLabs.Core;

namespace OnixLabs.Units;

public readonly partial struct Energy<T>
{
/// <summary>
    /// Determines whether the current instance is equal to the specified <see cref="Energy{T}"/> instance.
    /// </summary>
    /// <param name="other">The <see cref="Energy{T}"/> instance to compare with the current instance.</param>
    /// <returns>
    /// Returns <see langword="true"/> if the current instance is equal to the specified <see cref="Energy{T}"/> instance; otherwise, <see langword="false"/>.
    /// </returns>
    public bool Equals(Energy<T> other) => YoctoJoules.Equals(other.YoctoJoules);

    /// <summary>
    /// Determines whether the specified object is equal to the current instance.
    /// </summary>
    /// <param name="obj">The object to compare with the current instance.</param>
    /// <returns>
    /// Returns <see langword="true"/> if the specified object is equal to the current instance; otherwise, <see langword="false"/>.
    /// </returns>
    public override bool Equals([NotNullWhen(true)] object? obj) => obj is Energy<T> other && Equals(other);

    /// <summary>
    /// Returns a hash code for the current instance.
    /// </summary>
    /// <returns>Returns a hash code for the current instance.</returns>
    public override int GetHashCode() => YoctoJoules.GetHashCode();

    /// <summary>
    /// Determines whether two specified <see cref="Energy{T}"/> instances are equal.
    /// </summary>
    /// <param name="left">The left-hand value to compare.</param>
    /// <param name="right">The right-hand value to compare.</param>
    /// <returns>
    /// Returns <see langword="true"/> if the specified <see cref="Energy{T}"/> instances are equal; otherwise, <see langword="false"/>.
    /// </returns>
    public static bool Equals(Energy<T> left, Energy<T> right) => left.Equals(right);

    /// <summary>
    /// Determines whether two specified <see cref="Energy{T}"/> instances are equal.
    /// </summary>
    /// <param name="left">The left-hand value to compare.</param>
    /// <param name="right">The right-hand value to compare.</param>
    /// <returns>
    /// Returns <see langword="true"/> if the specified <see cref="Energy{T}"/> instances are equal; otherwise, <see langword="false"/>.
    /// </returns>
    public static bool operator ==(Energy<T> left, Energy<T> right) => Equals(left, right);

    /// <summary>
    /// Determines whether two specified <see cref="Energy{T}"/> instances are not equal.
    /// </summary>
    /// <param name="left">The left-hand value to compare.</param>
    /// <param name="right">The right-hand value to compare.</param>
    /// <returns>
    /// Returns <see langword="true"/> if the specified <see cref="Energy{T}"/> instances are not equal; otherwise, <see langword="false"/>.
    /// </returns>
    public static bool operator !=(Energy<T> left, Energy<T> right) => !Equals(left, right);
}
