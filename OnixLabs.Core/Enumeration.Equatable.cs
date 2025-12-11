// Copyright 2020 ONIXLabs
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

namespace OnixLabs.Core;

public abstract partial class Enumeration<T>
{
    /// <inheritdoc/>
    public bool Equals(T? other) =>
        ReferenceEquals(this, other)
        || other is not null
        && other.GetType() == GetType()
        && other.Value == Value
        && other.Name == Name;

    /// <inheritdoc/>
    public override bool Equals(object? obj) => Equals(obj as T);

    /// <inheritdoc/>
    public override int GetHashCode() => HashCode.Combine(GetType(), Name, Value);

    /// <summary>
    /// Determines whether the specified <paramref name="left"/> <see cref="Enumeration{T}"/>
    /// value is equal to the specified <paramref name="right"/> <see cref="Enumeration{T}"/> value.
    /// </summary>
    /// <param name="left">The left-hand value to compare.</param>
    /// <param name="right">The right-hand value to compare.</param>
    /// <returns>
    /// Returns <see langword="true"/> if the specified <paramref name="left"/> <see cref="Enumeration{T}"/> value is equal to
    /// the specified <paramref name="right"/> <see cref="Enumeration{T}"/> value; otherwise, <see langword="false"/>.
    /// </returns>
    public static bool operator ==(Enumeration<T> left, Enumeration<T> right) => Equals(left, right);

    /// <summary>
    /// Determines whether the specified <paramref name="left"/> <see cref="Enumeration{T}"/>
    /// value is not equal to the specified <paramref name="right"/> <see cref="Enumeration{T}"/> value.
    /// </summary>
    /// <param name="left">The left-hand value to compare.</param>
    /// <param name="right">The right-hand value to compare.</param>
    /// <returns>
    /// Returns <see langword="true"/> if the specified <paramref name="left"/> <see cref="Enumeration{T}"/> value is not equal to
    /// the specified <paramref name="right"/> <see cref="Enumeration{T}"/> value; otherwise, <see langword="false"/>.
    /// </returns>
    public static bool operator !=(Enumeration<T> left, Enumeration<T> right) => !Equals(left, right);
}
