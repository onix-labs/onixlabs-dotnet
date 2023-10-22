// Copyright © 2020 ONIXLabs
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
using System.Collections.Immutable;
using System.Linq;

namespace OnixLabs.Core.Collections;

/// <summary>
/// Provides methods to create empty or populated mutable and immutable collections.
/// </summary>
public static partial class Collection
{
    /// <summary>
    /// Creates an empty enumerable.
    /// </summary>
    /// <typeparam name="T">The underlying type of the enumerable.</typeparam>
    /// <returns>Returns an empty enumerable.</returns>
    public static IEnumerable<T> EmptyEnumerable<T>()
    {
        return Enumerable.Empty<T>();
    }

    /// <summary>
    /// Creates an empty array.
    /// </summary>
    /// <typeparam name="T">The underlying type of the array.</typeparam>
    /// <returns>Returns an empty array.</returns>
    public static T[] EmptyArray<T>()
    {
        return Array.Empty<T>();
    }

    /// <summary>
    /// Creates an empty immutable array.
    /// </summary>
    /// <typeparam name="T">The underlying type of the immutable array.</typeparam>
    /// <returns>Returns an empty immutable array.</returns>
    public static ImmutableArray<T> EmptyImmutableArray<T>()
    {
        return EmptyArray<T>().ToImmutableArray();
    }

    /// <summary>
    /// Creates an empty list.
    /// </summary>
    /// <typeparam name="T">The underlying type of the list.</typeparam>
    /// <returns>Returns an empty list.</returns>
    public static List<T> EmptyList<T>()
    {
        return new List<T>();
    }

    /// <summary>
    /// Creates an empty immutable list.
    /// </summary>
    /// <typeparam name="T">The underlying type of the immutable list.</typeparam>
    /// <returns>Returns an empty immutable list.</returns>
    public static ImmutableList<T> EmptyImmutableList<T>()
    {
        return EmptyList<T>().ToImmutableList();
    }

    /// <summary>
    /// Creates an empty dictionary.
    /// </summary>
    /// <typeparam name="TKey">The underlying type of the dictionary key.</typeparam>
    /// <typeparam name="TValue">The underlying type of the dictionary value.</typeparam>
    /// <returns>Returns an empty dictionary.</returns>
    public static Dictionary<TKey, TValue> EmptyDictionary<TKey, TValue>() where TKey : notnull
    {
        return new Dictionary<TKey, TValue>();
    }

    /// <summary>
    /// Creates an empty immutable dictionary.
    /// </summary>
    /// <typeparam name="TKey">The underlying type of the immutable dictionary key.</typeparam>
    /// <typeparam name="TValue">The underlying type of the immutable dictionary value.</typeparam>
    /// <returns>Returns an empty immutable dictionary.</returns>
    public static ImmutableDictionary<TKey, TValue> EmptyImmutableDictionary<TKey, TValue>() where TKey : notnull
    {
        return EmptyDictionary<TKey, TValue>().ToImmutableDictionary();
    }

    /// <summary>
    /// Creates an empty sorted dictionary.
    /// </summary>
    /// <typeparam name="TKey">The underlying type of the sorted dictionary key.</typeparam>
    /// <typeparam name="TValue">The underlying type of the sorted dictionary value.</typeparam>
    /// <returns>Returns an empty sorted dictionary.</returns>
    public static SortedDictionary<TKey, TValue> EmptySortedDictionary<TKey, TValue>() where TKey : notnull
    {
        return new SortedDictionary<TKey, TValue>();
    }

    /// <summary>
    /// Creates an empty immutable sorted dictionary.
    /// </summary>
    /// <typeparam name="TKey">The underlying type of the immutable sorted dictionary key.</typeparam>
    /// <typeparam name="TValue">The underlying type of the immutable sorted dictionary value.</typeparam>
    /// <returns>Returns an empty immutable sorted dictionary.</returns>
    public static ImmutableSortedDictionary<TKey, TValue> EmptyImmutableSortedDictionary<TKey, TValue>() where TKey : notnull
    {
        return EmptySortedDictionary<TKey, TValue>().ToImmutableSortedDictionary();
    }

    /// <summary>
    /// Creates an empty hash set.
    /// </summary>
    /// <typeparam name="T">The underlying type of the hash set.</typeparam>
    /// <returns>Returns an empty hash set.</returns>
    public static HashSet<T> EmptyHashSet<T>()
    {
        return new HashSet<T>();
    }

    /// <summary>
    /// Creates an empty immutable hash set.
    /// </summary>
    /// <typeparam name="T">The underlying type of the immutable hash set.</typeparam>
    /// <returns>Returns an empty immutable hash set.</returns>
    public static ImmutableHashSet<T> EmptyImmutableHashSet<T>()
    {
        return EmptyHashSet<T>().ToImmutableHashSet();
    }

    /// <summary>
    /// Creates an empty sorted set.
    /// </summary>
    /// <typeparam name="T">The underlying type of the sorted set.</typeparam>
    /// <returns>Returns an empty sorted set.</returns>
    public static SortedSet<T> EmptySortedSet<T>()
    {
        return new SortedSet<T>();
    }

    /// <summary>
    /// Creates an empty immutable sorted set.
    /// </summary>
    /// <typeparam name="T">The underlying type of the immutable sorted set.</typeparam>
    /// <returns>Returns an empty immutable sorted set.</returns>
    public static ImmutableSortedSet<T> EmptyImmutableSortedSet<T>()
    {
        return EmptySortedSet<T>().ToImmutableSortedSet();
    }

    /// <summary>
    /// Creates an empty stack.
    /// </summary>
    /// <typeparam name="T">The underlying type of the stack.</typeparam>
    /// <returns>Returns an empty stack.</returns>
    public static Stack<T> EmptyStack<T>()
    {
        return new Stack<T>();
    }

    /// <summary>
    /// Creates an empty immutable stack.
    /// </summary>
    /// <typeparam name="T">The underlying type of the immutable stack.</typeparam>
    /// <returns>Returns an empty immutable stack.</returns>
    public static ImmutableStack<T> EmptyImmutableStack<T>()
    {
        return ImmutableStack.Create<T>();
    }

    /// <summary>
    /// Creates an empty queue.
    /// </summary>
    /// <typeparam name="T">The underlying type of the queue.</typeparam>
    /// <returns>Returns an empty queue.</returns>
    public static Queue<T> EmptyQueue<T>()
    {
        return new Queue<T>();
    }

    /// <summary>
    /// Creates an empty immutable queue.
    /// </summary>
    /// <typeparam name="T">The underlying type of the immutable queue.</typeparam>
    /// <returns>Returns an empty immutable queue.</returns>
    public static ImmutableQueue<T> EmptyImmutableQueue<T>()
    {
        return ImmutableQueue.Create<T>();
    }
}
