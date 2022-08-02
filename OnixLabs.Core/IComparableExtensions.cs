// Copyright 2020-2022 ONIXLabs
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
using System.ComponentModel;

namespace OnixLabs.Core;

/// <summary>
/// Provides extension methods for <see cref="IComparable{T}"/>.
/// </summary>
// ReSharper disable InconsistentNaming
[EditorBrowsable(EditorBrowsableState.Never)]
internal static class IComparableExtensions
{
    /// <summary>
    /// Provides a mechanism to compare the current <see cref="IComparable{T}"/> to compare to the specified object.
    /// </summary>
    /// <param name="comparable">The current <see cref="IComparable{T}"/> to compare to the specified object.</param>
    /// <param name="obj">The <see cref="object"/> to compare to the current <see cref="IComparable{T}"/>.</param>
    /// <typeparam name="T">The underlying type of the current <see cref="IComparable{T}"/>.</typeparam>
    /// <returns>Returns a value that indicates the relative order of the objects being compared.</returns>
    /// <exception cref="ArgumentException">If the specified object is not null, or of the specified type.</exception>
    public static int CompareObject<T>(this IComparable<T> comparable, object? obj) => obj switch
    {
        null => 1,
        T other => comparable.CompareTo(other),
        _ => throw new ArgumentException($"Object must be of type {typeof(T)}", nameof(obj))
    };
}
