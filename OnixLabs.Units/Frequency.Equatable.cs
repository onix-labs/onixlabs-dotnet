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
public readonly partial struct Frequency<T>
{
    /// <summary>
    /// Determines whether two <see cref="Frequency{T}"/> instances have the same value.
    /// </summary>
    /// <param name="left">The left-hand value to compare.</param>
    /// <param name="right">The right-hand value to compare.</param>
    /// <returns>Returns <see langword="true"/> if the two specified <see cref="Frequency{T}"/> instances are equal; otherwise, <see langword="false"/>.</returns>
    public static bool Equals(Frequency<T> left, Frequency<T> right) => Equals(left.YoctoHertz, right.YoctoHertz);

    /// <summary>
    /// Determines whether the current <see cref="Frequency{T}"/> instance is equal to the specified <see cref="Frequency{T}"/> instance.
    /// </summary>
    /// <param name="other">The other <see cref="Frequency{T}"/> instance to compare with this instance.</param>
    /// <returns>Returns <see langword="true"/> if the current instance is equal to the specified instance; otherwise, <see langword="false"/>.</returns>
    public bool Equals(Frequency<T> other) => Equals(this, other);

    /// <summary>
    /// Determines whether the specified object is equal to the current <see cref="Frequency{T}"/> instance.
    /// </summary>
    /// <param name="obj">The object to check for equality.</param>
    /// <returns>Returns <see langword="true"/> if the object is equal to this instance; otherwise, <see langword="false"/>.</returns>
    public override bool Equals([NotNullWhen(true)] object? obj) => obj is Frequency<T> other && Equals(other);

    /// <summary>
    /// Serves as a hash code function for the <see cref="Frequency{T}"/> type.
    /// </summary>
    /// <returns>Returns a hash code for the current instance.</returns>
    public override int GetHashCode() => YoctoHertz.GetHashCode();

    /// <summary>
    /// Compares two instances of <see cref="Frequency{T}"/> to determine whether their values are equal.
    /// </summary>
    /// <param name="left">The left-hand value to compare.</param>
    /// <param name="right">The right-hand value to compare.</param>
    /// <returns>Returns <see langword="true"/> if the two specified instances are equal; otherwise, <see langword="false"/>.</returns>
    public static bool operator ==(Frequency<T> left, Frequency<T> right) => Equals(left, right);

    /// <summary>
    /// Compares two instances of <see cref="Frequency{T}"/> to determine whether their values are not equal.
    /// </summary>
    /// <param name="left">The left-hand value to compare.</param>
    /// <param name="right">The right-hand value to compare.</param>
    /// <returns>Returns <see langword="true"/> if the two specified instances are not equal; otherwise, <see langword="false"/>.</returns>
    public static bool operator !=(Frequency<T> left, Frequency<T> right) => !Equals(left, right);
}
