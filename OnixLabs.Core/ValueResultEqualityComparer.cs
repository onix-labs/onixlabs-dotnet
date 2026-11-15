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
using OnixLabs.Core.Collections.Generic;

namespace OnixLabs.Core;

/// <summary>
/// Represents an equality comparer for <see cref="ValueResult{T}"/> values.
/// </summary>
/// <param name="valueComparer">The equality comparer used to compare the values of successful results; or <see langword="null"/> to use the default.</param>
/// <typeparam name="T">The underlying type of the result values to compare.</typeparam>
public sealed class ValueResultEqualityComparer<T>(EqualityComparer<T>? valueComparer = null) : IEqualityComparer<ValueResult<T>>
{
    /// <summary>
    /// Gets the default <see cref="ValueResultEqualityComparer{T}"/>, which compares the values of successful results with the default equality comparer.
    /// </summary>
    // ReSharper disable once HeapView.ObjectAllocation.Evident
    public static readonly ValueResultEqualityComparer<T> Default = new();

    /// <summary>
    /// Determines whether the specified values are equal.
    /// </summary>
    /// <param name="x">The first value to compare.</param>
    /// <param name="y">The second value to compare.</param>
    /// <returns>Returns <see langword="true"/> if the specified values are equal; otherwise, <see langword="false"/>.</returns>
    public bool Equals(ValueResult<T> x, ValueResult<T> y)
    {
        if (x.IsSuccess != y.IsSuccess) return false;
        if (x.IsFailure) return object.Equals(x.GetExceptionOrDefault(), y.GetExceptionOrDefault());
        return valueComparer.GetOrDefault().Equals(x.GetValueOrDefault(), y.GetValueOrDefault());
    }

    /// <summary>
    /// Returns a hash code for the specified value.
    /// </summary>
    /// <param name="obj">The value for which to get a hash code.</param>
    /// <returns>Returns a hash code for the specified value.</returns>
    public int GetHashCode(ValueResult<T> obj)
    {
        if (obj.IsFailure) return obj.GetHashCode();
        T? value = obj.GetValueOrDefault();
        return value is null ? 0 : valueComparer.GetOrDefault().GetHashCode(value);
    }
}
