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
using System.Collections.Generic;
using System.ComponentModel;

namespace OnixLabs.Core;

/// <summary>
/// Provides extension methods for objects.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public static class RandomExtensions
{
    /// <summary>
    /// Obtains a random element from the specified <see cref="IReadOnlyList{T}"/> items.
    /// </summary>
    /// <param name="random">The current <see cref="Random"/> instance.</param>
    /// <param name="items">The <see cref="IReadOnlyList{T}"/> items from which to obtain a random element.</param>
    /// <typeparam name="T">The underlying type of the <see cref="IReadOnlyList{T}"/> collection.</typeparam>
    /// <returns>Returns a random element from the specified <see cref="IReadOnlyList{T}"/> items.</returns>
    public static T Next<T>(this Random random, IReadOnlyList<T> items) => items[random.Next(0, items.Count)];
}
