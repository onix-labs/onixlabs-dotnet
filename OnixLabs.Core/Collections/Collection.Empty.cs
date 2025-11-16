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

using System.Collections.Generic;
using System.Collections.Immutable;

namespace OnixLabs.Core.Collections;

/// <summary>
/// Provides methods to create empty or populated mutable and immutable collections.
/// </summary>
public static partial class Collection
{
    /// <summary>
    /// Creates an empty <see cref="IEnumerable{T}"/>.
    /// </summary>
    /// <typeparam name="T">The underlying type of the enumerable.</typeparam>
    /// <returns>Returns an empty enumerable.</returns>
    public static IEnumerable<T> EmptyEnumerable<T>() => [];

    /// <summary>
    /// Creates an empty <typeparamref name="T"/> array.
    /// </summary>
    /// <typeparam name="T">The underlying type of the array.</typeparam>
    /// <returns>Returns an empty array.</returns>
    public static T[] EmptyArray<T>() => [];

    /// <summary>
    /// Creates an empty <see cref="ImmutableArray{T}"/>.
    /// </summary>
    /// <typeparam name="T">The underlying type of the immutable array.</typeparam>
    /// <returns>Returns an empty immutable array.</returns>
    public static ImmutableArray<T> EmptyImmutableArray<T>() => [];

    /// <summary>
    /// Creates an empty <see cref="List{T}"/>.
    /// </summary>
    /// <typeparam name="T">The underlying type of the list.</typeparam>
    /// <returns>Returns an empty list.</returns>
    public static List<T> EmptyList<T>() => [];

    /// <summary>
    /// Creates an empty <see cref="ImmutableList{T}"/>.
    /// </summary>
    /// <typeparam name="T">The underlying type of the immutable list.</typeparam>
    /// <returns>Returns an empty immutable list.</returns>
    public static ImmutableList<T> EmptyImmutableList<T>() => [];

    /// <summary>
    /// Creates an empty <see cref="Dictionary{TKey,TValue}"/>.
    /// </summary>
    /// <typeparam name="TKey">The underlying type of the dictionary key.</typeparam>
    /// <typeparam name="TValue">The underlying type of the dictionary value.</typeparam>
    /// <returns>Returns an empty dictionary.</returns>
    public static Dictionary<TKey, TValue> EmptyDictionary<TKey, TValue>() where TKey : notnull => [];

    /// <summary>
    /// Creates an empty <see cref="ImmutableDictionary{TKey,TValue}"/>.
    /// </summary>
    /// <typeparam name="TKey">The underlying type of the immutable dictionary key.</typeparam>
    /// <typeparam name="TValue">The underlying type of the immutable dictionary value.</typeparam>
    /// <returns>Returns an empty immutable dictionary.</returns>
    public static ImmutableDictionary<TKey, TValue> EmptyImmutableDictionary<TKey, TValue>() where TKey : notnull => ImmutableDictionary<TKey, TValue>.Empty;

    /// <summary>
    /// Creates an empty <see cref="SortedDictionary{TKey,TValue}"/>.
    /// </summary>
    /// <typeparam name="TKey">The underlying type of the sorted dictionary key.</typeparam>
    /// <typeparam name="TValue">The underlying type of the sorted dictionary value.</typeparam>
    /// <returns>Returns an empty sorted dictionary.</returns>
    public static SortedDictionary<TKey, TValue> EmptySortedDictionary<TKey, TValue>() where TKey : notnull => [];

    /// <summary>
    /// Creates an empty <see cref="ImmutableSortedDictionary{TKey,TValue}"/>.
    /// </summary>
    /// <typeparam name="TKey">The underlying type of the immutable sorted dictionary key.</typeparam>
    /// <typeparam name="TValue">The underlying type of the immutable sorted dictionary value.</typeparam>
    /// <returns>Returns an empty immutable sorted dictionary.</returns>
    public static ImmutableSortedDictionary<TKey, TValue> EmptyImmutableSortedDictionary<TKey, TValue>() where TKey : notnull => ImmutableSortedDictionary<TKey, TValue>.Empty;

    /// <summary>
    /// Creates an empty <see cref="HashSet{T}"/>.
    /// </summary>
    /// <typeparam name="T">The underlying type of the hash set.</typeparam>
    /// <returns>Returns an empty hash set.</returns>
    public static HashSet<T> EmptyHashSet<T>() => [];

    /// <summary>
    /// Creates an empty <see cref="ImmutableHashSet{T}"/>.
    /// </summary>
    /// <typeparam name="T">The underlying type of the immutable hash set.</typeparam>
    /// <returns>Returns an empty immutable hash set.</returns>
    public static ImmutableHashSet<T> EmptyImmutableHashSet<T>() => [];

    /// <summary>
    /// Creates an empty <see cref="SortedSet{T}"/>.
    /// </summary>
    /// <typeparam name="T">The underlying type of the sorted set.</typeparam>
    /// <returns>Returns an empty sorted set.</returns>
    public static SortedSet<T> EmptySortedSet<T>() => [];

    /// <summary>
    /// Creates an empty <see cref="ImmutableSortedSet{T}"/>.
    /// </summary>
    /// <typeparam name="T">The underlying type of the immutable sorted set.</typeparam>
    /// <returns>Returns an empty immutable sorted set.</returns>
    public static ImmutableSortedSet<T> EmptyImmutableSortedSet<T>() => [];

    /// <summary>
    /// Creates an empty <see cref="Stack{T}"/>.
    /// </summary>
    /// <typeparam name="T">The underlying type of the stack.</typeparam>
    /// <returns>Returns an empty stack.</returns>
    public static Stack<T> EmptyStack<T>() => [];

    /// <summary>
    /// Creates an empty <see cref="ImmutableStack{T}"/>.
    /// </summary>
    /// <typeparam name="T">The underlying type of the immutable stack.</typeparam>
    /// <returns>Returns an empty immutable stack.</returns>
    public static ImmutableStack<T> EmptyImmutableStack<T>() => [];

    /// <summary>
    /// Creates an empty <see cref="Queue{T}"/>.
    /// </summary>
    /// <typeparam name="T">The underlying type of the queue.</typeparam>
    /// <returns>Returns an empty queue.</returns>
    public static Queue<T> EmptyQueue<T>() => [];

    /// <summary>
    /// Creates an empty <see cref="ImmutableQueue{T}"/>.
    /// </summary>
    /// <typeparam name="T">The underlying type of the immutable queue.</typeparam>
    /// <returns>Returns an empty immutable queue.</returns>
    public static ImmutableQueue<T> EmptyImmutableQueue<T>() => [];
}
