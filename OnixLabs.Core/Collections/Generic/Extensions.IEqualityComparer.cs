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
using System.ComponentModel;

namespace OnixLabs.Core.Collections.Generic;

/// <summary>
/// Provides LINQ-like extension methods for <see cref="IEqualityComparer{T}"/>.
/// </summary>
// ReSharper disable InconsistentNaming
[EditorBrowsable(EditorBrowsableState.Never)]
public static class IEqualityComparerExtensions
{
    /// <summary>
    /// Gets the current <see cref="IEqualityComparer{T}"/>, or the default comparer if the current comparer is <see langword="null"/>.
    /// </summary>
    /// <param name="comparer">The current <see cref="IEqualityComparer{T}"/>.</param>
    /// <typeparam name="T">The underlying type of the current <see cref="IEqualityComparer{T}"/>.</typeparam>
    /// <returns>Returns the current <see cref="IEqualityComparer{T}"/>, or the default comparer if the current comparer is <see langword="null"/>.</returns>
    public static IEqualityComparer<T> GetOrDefault<T>(this IEqualityComparer<T>? comparer) => comparer ?? EqualityComparer<T>.Default;
}
