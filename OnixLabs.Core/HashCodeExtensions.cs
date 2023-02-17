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
using System.Collections.Generic;
using System.ComponentModel;
using OnixLabs.Core.Linq;

namespace OnixLabs.Core;

/// <summary>
/// Provides extension methods for hash codes.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public static class HashCodeExtensions
{
    /// <summary>
    /// Adds an item to be hashed into a <see cref="HashCode"/> instance.
    /// </summary>
    /// <param name="hashCode">The <see cref="HashCode"/> which will receive the item to hash.</param>
    /// <param name="item">The item to hash into the <see cref="HashCode"/>.</param>
    /// <typeparam name="T">The underlying type of the item to hash.</typeparam>
    /// <returns>Returns the <see cref="HashCode"/> containing the added item.</returns>
    public static HashCode AddItem<T>(this HashCode hashCode, T item)
    {
        hashCode.Add(item);
        return hashCode;
    }

    /// <summary>
    /// Adds the items to be hashed into a <see cref="HashCode"/> instance.
    /// </summary>
    /// <param name="hashCode">The <see cref="HashCode"/> which will receive the items to hash.</param>
    /// <param name="items">The items to hash into the <see cref="HashCode"/>.</param>
    /// <typeparam name="T">The underlying type of the items to hash.</typeparam>
    /// <returns>Returns the <see cref="HashCode"/> containing the added items.</returns>
    public static HashCode AddItems<T>(this HashCode hashCode, IEnumerable<T> items)
    {
        items.ForEach(hashCode.Add);
        return hashCode;
    }
}
