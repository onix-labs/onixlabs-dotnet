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
using System.Runtime.CompilerServices;

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
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static IEnumerable<T> EmptyEnumerable<T>()
    {
        return Enumerable.Empty<T>();
    }

    /// <summary>
    /// Creates an empty array.
    /// </summary>
    /// <typeparam name="T">The underlying type of the array.</typeparam>
    /// <returns>Returns an empty array.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static T[] EmptyArray<T>()
    {
        return Array.Empty<T>();
    }

    /// <summary>
    /// Creates an empty immutable array.
    /// </summary>
    /// <typeparam name="T">The underlying type of the immutable array.</typeparam>
    /// <returns>Returns an empty immutable array.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static ImmutableArray<T> EmptyImmutableArray<T>()
    {
        return ImmutableArray<T>.Empty;
    }

    /// <summary>
    /// Creates an empty list.
    /// </summary>
    /// <typeparam name="T">The underlying type of the list.</typeparam>
    /// <returns>Returns an empty list.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static List<T> EmptyList<T>()
    {
        return [];
    }

    /// <summary>
    /// Creates an empty immutable list.
    /// </summary>
    /// <typeparam name="T">The underlying type of the immutable list.</typeparam>
    /// <returns>Returns an empty immutable list.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static ImmutableList<T> EmptyImmutableList<T>()
    {
        return ImmutableList<T>.Empty;
    }

    /// <summary>
    /// Creates an empty dictionary.
    /// </summary>
    /// <typeparam name="TKey">The underlying type of the dictionary key.</typeparam>
    /// <typeparam name="TValue">The underlying type of the dictionary value.</typeparam>
    /// <returns>Returns an empty dictionary.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Dictionary<TKey, TValue> EmptyDictionary<TKey, TValue>() where TKey : notnull
    {
        return [];
    }

    /// <summary>
    /// Creates an empty immutable dictionary.
    /// </summary>
    /// <typeparam name="TKey">The underlying type of the immutable dictionary key.</typeparam>
    /// <typeparam name="TValue">The underlying type of the immutable dictionary value.</typeparam>
    /// <returns>Returns an empty immutable dictionary.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static ImmutableDictionary<TKey, TValue> EmptyImmutableDictionary<TKey, TValue>() where TKey : notnull
    {
        return ImmutableDictionary<TKey, TValue>.Empty;
    }

    /// <summary>
    /// Creates an empty sorted dictionary.
    /// </summary>
    /// <typeparam name="TKey">The underlying type of the sorted dictionary key.</typeparam>
    /// <typeparam name="TValue">The underlying type of the sorted dictionary value.</typeparam>
    /// <returns>Returns an empty sorted dictionary.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static SortedDictionary<TKey, TValue> EmptySortedDictionary<TKey, TValue>() where TKey : notnull
    {
        return [];
    }

    /// <summary>
    /// Creates an empty immutable sorted dictionary.
    /// </summary>
    /// <typeparam name="TKey">The underlying type of the immutable sorted dictionary key.</typeparam>
    /// <typeparam name="TValue">The underlying type of the immutable sorted dictionary value.</typeparam>
    /// <returns>Returns an empty immutable sorted dictionary.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static ImmutableSortedDictionary<TKey, TValue> EmptyImmutableSortedDictionary<TKey, TValue>() where TKey : notnull
    {
        return ImmutableSortedDictionary<TKey, TValue>.Empty;
    }

    /// <summary>
    /// Creates an empty hash set.
    /// </summary>
    /// <typeparam name="T">The underlying type of the hash set.</typeparam>
    /// <returns>Returns an empty hash set.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static HashSet<T> EmptyHashSet<T>()
    {
        return [];
    }

    /// <summary>
    /// Creates an empty immutable hash set.
    /// </summary>
    /// <typeparam name="T">The underlying type of the immutable hash set.</typeparam>
    /// <returns>Returns an empty immutable hash set.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static ImmutableHashSet<T> EmptyImmutableHashSet<T>()
    {
        return ImmutableHashSet<T>.Empty;
    }

    /// <summary>
    /// Creates an empty sorted set.
    /// </summary>
    /// <typeparam name="T">The underlying type of the sorted set.</typeparam>
    /// <returns>Returns an empty sorted set.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static SortedSet<T> EmptySortedSet<T>()
    {
        return [];
    }

    /// <summary>
    /// Creates an empty immutable sorted set.
    /// </summary>
    /// <typeparam name="T">The underlying type of the immutable sorted set.</typeparam>
    /// <returns>Returns an empty immutable sorted set.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static ImmutableSortedSet<T> EmptyImmutableSortedSet<T>()
    {
        return ImmutableSortedSet<T>.Empty;
    }

    /// <summary>
    /// Creates an empty stack.
    /// </summary>
    /// <typeparam name="T">The underlying type of the stack.</typeparam>
    /// <returns>Returns an empty stack.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Stack<T> EmptyStack<T>()
    {
        return [];
    }

    /// <summary>
    /// Creates an empty immutable stack.
    /// </summary>
    /// <typeparam name="T">The underlying type of the immutable stack.</typeparam>
    /// <returns>Returns an empty immutable stack.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static ImmutableStack<T> EmptyImmutableStack<T>()
    {
        return ImmutableStack<T>.Empty;
    }

    /// <summary>
    /// Creates an empty queue.
    /// </summary>
    /// <typeparam name="T">The underlying type of the queue.</typeparam>
    /// <returns>Returns an empty queue.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Queue<T> EmptyQueue<T>()
    {
        return [];
    }

    /// <summary>
    /// Creates an empty immutable queue.
    /// </summary>
    /// <typeparam name="T">The underlying type of the immutable queue.</typeparam>
    /// <returns>Returns an empty immutable queue.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static ImmutableQueue<T> EmptyImmutableQueue<T>()
    {
        return ImmutableQueue<T>.Empty;
    }
}
