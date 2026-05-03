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
/// Represents an equality comparer for comparing <see cref="Result{T}"/> instances.
/// </summary>
/// <param name="valueComparer">The <see cref="EqualityComparer{T}"/> that will be used to compare the underlying values of each <see cref="Result{T}"/> instance.</param>
/// <typeparam name="T">The underlying type of the <see cref="Result{T}"/> instance.</typeparam>
public sealed class ResultEqualityComparer<T>(EqualityComparer<T>? valueComparer = null) : IEqualityComparer<Result<T>>
{
    /// <summary>
    /// Gets the default <see cref="ResultEqualityComparer{T}"/> instance.
    /// </summary>
    // ReSharper disable once HeapView.ObjectAllocation.Evident
    public static readonly ResultEqualityComparer<T> Default = new();

    /// <inheritdoc/>
    public bool Equals(Result<T>? x, Result<T>? y)
    {
        if (ReferenceEquals(x, y)) return true;
        if (x is null || y is null) return x is null && y is null;

        if (x is Failure<T> xFailure && y is Failure<T> yFailure)
            return object.Equals(xFailure.Exception, yFailure.Exception);

        T? xValue = x.GetValueOrDefault();
        T? yValue = y.GetValueOrDefault();

        return valueComparer.GetOrDefault().Equals(xValue, yValue);
    }

    /// <inheritdoc/>
    public int GetHashCode(Result<T> obj) => obj.GetHashCode();
}
