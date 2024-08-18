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
/// Represents an equality comparer for comparing <see cref="Optional{T}"/> instances.
/// </summary>
/// <param name="valueEqualityComparer">The <see cref="EqualityComparer{T}"/> that will be used to compare the underlying values of each <see cref="Optional{T}"/> instance.</param>
/// <typeparam name="T">The underlying type of the <see cref="Optional{T}"/> instance.</typeparam>
public sealed class OptionalEqualityComparer<T>(EqualityComparer<T>? valueEqualityComparer = null) : IEqualityComparer<Optional<T>> where T : notnull
{
    /// <summary>
    /// Gets the default <see cref="OptionalEqualityComparer{T}"/> instance.
    /// </summary>
    // ReSharper disable once HeapView.ObjectAllocation.Evident
    public static readonly OptionalEqualityComparer<T> Default = new();

    /// <summary>Determines whether the specified <see cref="Optional{T}"/> values are equal.</summary>
    /// <param name="x">The first object of type <see cref="Optional{T}"/> to compare.</param>
    /// <param name="y">The second object of type <see cref="Optional{T}"/> to compare.</param>
    /// <returns> Returns <see langword="true" /> if the specified <see cref="Optional{T}"/> values are equal; otherwise, <see langword="false" />.</returns>
    public bool Equals(Optional<T>? x, Optional<T>? y)
    {
        if (ReferenceEquals(x, y)) return true;
        if (x is null || y is null) return x is null && y is null;
        if (Optional<T>.IsNone(x) && Optional<T>.IsNone(y)) return true;
        if (Optional<T>.IsSome(x) && Optional<T>.IsNone(y)) return false;
        if (Optional<T>.IsNone(x) && Optional<T>.IsSome(y)) return false;
        return (valueEqualityComparer ?? EqualityComparer<T>.Default).Equals(x.GetValueOrDefault(), y.GetValueOrDefault());
    }

    /// <summary>Returns a hash code for the specified object.</summary>
    /// <param name="obj">The <see cref="object" /> for which a hash code is to be returned.</param>
    /// <returns>A hash code for the specified object.</returns>
    public int GetHashCode(Optional<T> obj) => obj.GetHashCode();
}
