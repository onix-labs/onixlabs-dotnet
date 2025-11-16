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
using System.Linq;

namespace OnixLabs.Core.Collections;

// ReSharper disable MemberCanBePrivate.Global
public static partial class Collection
{
    /// <summary>
    /// Creates an <see cref="IEnumerable{T}"/> of the specified items.
    /// </summary>
    /// <param name="items">The items which will populate the enumerable.</param>
    /// <typeparam name="T">The underlying type of the enumerable.</typeparam>
    /// <returns>Returns an enumerable populated with the specified items.</returns>
    public static IEnumerable<T> EnumerableOf<T>(params IEnumerable<T> items) => items;

    /// <summary>
    /// Creates a <typeparamref name="T"/> array of the specified items.
    /// </summary>
    /// <param name="items">The items which will populate the array.</param>
    /// <typeparam name="T">The underlying type of the array.</typeparam>
    /// <returns>Returns an array populated with the specified items.</returns>
    public static T[] ArrayOf<T>(params T[] items) => items;

    /// <summary>
    /// Creates an <see cref="ImmutableArray{T}"/> of the specified items.
    /// </summary>
    /// <param name="items">The items which will populate the immutable array.</param>
    /// <typeparam name="T">The underlying type of the immutable array.</typeparam>
    /// <returns>Returns an immutable array populated with the specified items.</returns>
    public static ImmutableArray<T> ImmutableArrayOf<T>(params ImmutableArray<T> items) => items;

    /// <summary>
    /// Creates a <see cref="List{T}"/> of the specified items.
    /// </summary>
    /// <param name="items">The items which will populate the list.</param>
    /// <typeparam name="T">The underlying type of the list.</typeparam>
    /// <returns>Returns a list populated with the specified items.</returns>
    public static List<T> ListOf<T>(params List<T> items) => items;

    /// <summary>
    /// Creates an <see cref="ImmutableList{T}"/> of the specified items.
    /// </summary>
    /// <param name="items">The items which will populate the immutable list.</param>
    /// <typeparam name="T">The underlying type of the immutable list.</typeparam>
    /// <returns>Returns an immutable list populated with the specified items.</returns>
    public static ImmutableList<T> ImmutableListOf<T>(params ImmutableList<T> items) => items;

    /// <summary>
    /// Creates a <see cref="HashSet{T}"/> of the specified items.
    /// </summary>
    /// <param name="items">The items which will populate the hash set.</param>
    /// <typeparam name="T">The underlying type of the hash set.</typeparam>
    /// <returns>Returns a hash set populated with the specified items.</returns>
    public static HashSet<T> HashSetOf<T>(params HashSet<T> items) => items;

    /// <summary>
    /// Creates an <see cref="ImmutableHashSet{T}"/> of the specified items.
    /// </summary>
    /// <param name="items">The items which will populate the immutable hash set.</param>
    /// <typeparam name="T">The underlying type of the immutable hash set.</typeparam>
    /// <returns>Returns an immutable hash set populated with the specified items.</returns>
    public static ImmutableHashSet<T> ImmutableHashSetOf<T>(params ImmutableHashSet<T> items) => items;

    /// <summary>
    /// Creates a <see cref="SortedSet{T}"/> of the specified items.
    /// </summary>
    /// <param name="items">The items which will populate the sorted set.</param>
    /// <typeparam name="T">The underlying type of the sorted set.</typeparam>
    /// <returns>Returns a sorted set populated with the specified items.</returns>
    public static SortedSet<T> SortedSetOf<T>(params SortedSet<T> items) => items;

    /// <summary>
    /// Creates an <see cref="ImmutableSortedSet{T}"/> of the specified items.
    /// </summary>
    /// <param name="items">The items which will populate the immutable sorted set.</param>
    /// <typeparam name="T">The underlying type of the immutable sorted set.</typeparam>
    /// <returns>Returns an immutable sorted set populated with the specified items.</returns>
    public static ImmutableSortedSet<T> ImmutableSortedSetOf<T>(params ImmutableSortedSet<T> items) => items;

    /// <summary>
    /// Creates a <see cref="Stack{T}"/> of the specified items.
    /// </summary>
    /// <param name="items">The items which will populate the stack.</param>
    /// <typeparam name="T">The underlying type of the stack.</typeparam>
    /// <returns>Returns a stack populated with the specified items.</returns>
    public static Stack<T> StackOf<T>(params IEnumerable<T> items) => new(items);

    /// <summary>
    /// Creates an <see cref="ImmutableStack{T}"/> of the specified items.
    /// </summary>
    /// <param name="items">The items which will populate the immutable stack.</param>
    /// <typeparam name="T">The underlying type of the immutable stack.</typeparam>
    /// <returns>Returns an immutable stack populated with the specified items.</returns>
    public static ImmutableStack<T> ImmutableStackOf<T>(params ImmutableStack<T> items) => items;

    /// <summary>
    /// Creates a <see cref="Queue{T}"/> of the specified items.
    /// </summary>
    /// <param name="items">The items which will populate the queue.</param>
    /// <typeparam name="T">The underlying type of the queue.</typeparam>
    /// <returns>Returns a queue populated with the specified items.</returns>
    public static Queue<T> QueueOf<T>(params IEnumerable<T> items) => new(items);

    /// <summary>
    /// Creates an <see cref="ImmutableQueue{T}"/> of the specified items.
    /// </summary>
    /// <param name="items">The items which will populate the immutable queue.</param>
    /// <typeparam name="T">The underlying type of the immutable queue.</typeparam>
    /// <returns>Returns an immutable queue populated with the specified items.</returns>
    public static ImmutableQueue<T> ImmutableQueueOf<T>(params ImmutableQueue<T> items) => items;

    /// <summary>
    /// Create a <see cref="Dictionary{TKey,TValue}"/> of the specified items.
    /// </summary>
    /// <param name="items">The items which wil populate the dictionary.</param>
    /// <typeparam name="TKey">The underlying type of the dictionary key.</typeparam>
    /// <typeparam name="TValue">The underlying type of the dictionary value.</typeparam>
    /// <returns>Returns a dictionary populated with the specified items.</returns>
    public static Dictionary<TKey, TValue> DictionaryOf<TKey, TValue>
        (params IEnumerable<KeyValuePair<TKey, TValue>> items) where TKey : notnull => new(items);

    /// <summary>
    /// Create a <see cref="Dictionary{TKey,TValue}"/> of the specified items.
    /// </summary>
    /// <param name="items">The items which wil populate the dictionary.</param>
    /// <typeparam name="TKey">The underlying type of the dictionary key.</typeparam>
    /// <typeparam name="TValue">The underlying type of the dictionary value.</typeparam>
    /// <returns>Returns a dictionary populated with the specified items.</returns>
    public static Dictionary<TKey, TValue> DictionaryOf<TKey, TValue>
        (params IEnumerable<(TKey key, TValue value)> items) where TKey : notnull =>
        DictionaryOf(items.Select(item => new KeyValuePair<TKey, TValue>(item.key, item.value)));

    /// <summary>
    /// Create an <see cref="ImmutableDictionary{TKey,TValue}"/> of the specified items.
    /// </summary>
    /// <param name="items">The items which wil populate the immutable dictionary.</param>
    /// <typeparam name="TKey">The underlying type of the immutable dictionary key.</typeparam>
    /// <typeparam name="TValue">The underlying type of the immutable dictionary value.</typeparam>
    /// <returns>Returns an immutable dictionary populated with the specified items.</returns>
    public static ImmutableDictionary<TKey, TValue> ImmutableDictionaryOf<TKey, TValue>
        (params IEnumerable<KeyValuePair<TKey, TValue>> items) where TKey : notnull => ImmutableDictionary.CreateRange(items);

    /// <summary>
    /// Create an <see cref="ImmutableDictionary{TKey,TValue}"/> of the specified items.
    /// </summary>
    /// <param name="items">The items which wil populate the immutable dictionary.</param>
    /// <typeparam name="TKey">The underlying type of the immutable dictionary key.</typeparam>
    /// <typeparam name="TValue">The underlying type of the immutable dictionary value.</typeparam>
    /// <returns>Returns an immutable dictionary populated with the specified items.</returns>
    public static ImmutableDictionary<TKey, TValue> ImmutableDictionaryOf<TKey, TValue>
        (params IEnumerable<(TKey key, TValue value)> items) where TKey : notnull =>
        ImmutableDictionary.CreateRange(items.Select(item => new KeyValuePair<TKey, TValue>(item.key, item.value)));

    /// <summary>
    /// Create a <see cref="SortedDictionary{TKey,TValue}"/> of the specified items.
    /// </summary>
    /// <param name="items">The items which wil populate the sorted dictionary.</param>
    /// <typeparam name="TKey">The underlying type of the sorted dictionary key.</typeparam>
    /// <typeparam name="TValue">The underlying type of the sorted dictionary value.</typeparam>
    /// <returns>Returns a sorted dictionary populated with the specified items.</returns>
    public static SortedDictionary<TKey, TValue> SortedDictionaryOf<TKey, TValue>
        (params IEnumerable<KeyValuePair<TKey, TValue>> items) where TKey : notnull => new(DictionaryOf(items));

    /// <summary>
    /// Create a <see cref="SortedDictionary{TKey,TValue}"/> of the specified items.
    /// </summary>
    /// <param name="items">The items which wil populate the sorted dictionary.</param>
    /// <typeparam name="TKey">The underlying type of the sorted dictionary key.</typeparam>
    /// <typeparam name="TValue">The underlying type of the sorted dictionary value.</typeparam>
    /// <returns>Returns a sorted dictionary populated with the specified items.</returns>
    public static SortedDictionary<TKey, TValue> SortedDictionaryOf<TKey, TValue>
        (params IEnumerable<(TKey key, TValue value)> items) where TKey : notnull => new(DictionaryOf(items));

    /// <summary>
    /// Create an <see cref="ImmutableSortedDictionary{TKey,TValue}"/> of the specified items.
    /// </summary>
    /// <param name="items">The items which wil populate the immutable sorted dictionary.</param>
    /// <typeparam name="TKey">The underlying type of the immutable sorted dictionary key.</typeparam>
    /// <typeparam name="TValue">The underlying type of the immutable sorted dictionary value.</typeparam>
    /// <returns>Returns an immutable sorted dictionary populated with the specified items.</returns>
    public static ImmutableSortedDictionary<TKey, TValue> ImmutableSortedDictionaryOf<TKey, TValue>
        (params IEnumerable<KeyValuePair<TKey, TValue>> items) where TKey : notnull => ImmutableSortedDictionary.CreateRange(items);

    /// <summary>
    /// Create an <see cref="ImmutableSortedDictionary{TKey,TValue}"/> of the specified items.
    /// </summary>
    /// <param name="items">The items which wil populate the immutable sorted dictionary.</param>
    /// <typeparam name="TKey">The underlying type of the immutable sorted dictionary key.</typeparam>
    /// <typeparam name="TValue">The underlying type of the immutable sorted dictionary value.</typeparam>
    /// <returns>Returns an immutable sorted dictionary populated with the specified items.</returns>
    public static ImmutableSortedDictionary<TKey, TValue> ImmutableSortedDictionaryOf<TKey, TValue>
        (params IEnumerable<(TKey key, TValue value)> items) where TKey : notnull =>
        ImmutableSortedDictionary.CreateRange(items.Select(item => new KeyValuePair<TKey, TValue>(item.key, item.value)));
}
