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

using System.Collections.Generic;

namespace OnixLabs.Core;

/// <summary>
/// Represents an equality comparer for <see cref="ValueOptional{T}"/> values.
/// </summary>
/// <param name="valueEqualityComparer">The equality comparer used to compare the underlying values; or <see langword="null"/> to use the default.</param>
/// <typeparam name="T">The underlying type of the optional values to compare.</typeparam>
public sealed class ValueOptionalEqualityComparer<T>(EqualityComparer<T>? valueEqualityComparer = null) : IEqualityComparer<ValueOptional<T>> where T : notnull
{
    /// <summary>
    /// Gets the default <see cref="ValueOptionalEqualityComparer{T}"/>, which compares the underlying values with the default equality comparer.
    /// </summary>
    // ReSharper disable once HeapView.ObjectAllocation.Evident
    public static readonly ValueOptionalEqualityComparer<T> Default = new();

    /// <summary>
    /// Determines whether the specified values are equal.
    /// </summary>
    /// <param name="x">The first value to compare.</param>
    /// <param name="y">The second value to compare.</param>
    /// <returns>Returns <see langword="true"/> if the specified values are equal; otherwise, <see langword="false"/>.</returns>
    public bool Equals(ValueOptional<T> x, ValueOptional<T> y)
    {
        if (x.HasValue != y.HasValue) return false;
        if (!x.HasValue) return true;
        return (valueEqualityComparer ?? EqualityComparer<T>.Default).Equals(x.GetValueOrDefault(), y.GetValueOrDefault());
    }

    /// <summary>
    /// Returns a hash code for the specified value.
    /// </summary>
    /// <param name="obj">The value for which to get a hash code.</param>
    /// <returns>Returns a hash code for the specified value.</returns>
    public int GetHashCode(ValueOptional<T> obj) => obj.HasValue ? (valueEqualityComparer ?? EqualityComparer<T>.Default).GetHashCode(obj.GetValueOrThrow()) : 0;
}
