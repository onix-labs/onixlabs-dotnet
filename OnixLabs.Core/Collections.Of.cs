// Copyright 2020-2023 ONIXLabs
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
using System.Net.Http;

namespace OnixLabs.Core;

public static partial class Collections
{
    /// <summary>
    /// Creates an enumerable of the specified items.
    /// </summary>
    /// <param name="items">The items which will populate the enumerable.</param>
    /// <typeparam name="T">The underlying type of the enumerable.</typeparam>
    /// <returns>Returns an enumerable populated with the specified items.</returns>
    public static IEnumerable<T> EnumerableOf<T>(params T[] items)
    {
        return items.Copy();
    }

    /// <summary>
    /// Creates an array of the specified items.
    /// </summary>
    /// <param name="items">The items which will populate the array.</param>
    /// <typeparam name="T">The underlying type of the array.</typeparam>
    /// <returns>Returns an array populated with the specified items.</returns>
    public static T[] ArrayOf<T>(params T[] items)
    {
        return items.Copy();
    }

    /// <summary>
    /// Creates an immutable array of the specified items.
    /// </summary>
    /// <param name="items">The items which will populate the immutable array.</param>
    /// <typeparam name="T">The underlying type of the immutable array.</typeparam>
    /// <returns>Returns an immutable array populated with the specified items.</returns>
    public static ImmutableArray<T> ImmutableArrayOf<T>(params T[] items)
    {
        return ArrayOf(items).ToImmutableArray();
    }

    /// <summary>
    /// Creates a list of the specified items.
    /// </summary>
    /// <param name="items">The items which will populate the list.</param>
    /// <typeparam name="T">The underlying type of the list.</typeparam>
    /// <returns>Returns a list populated with the specified items.</returns>
    public static List<T> ListOf<T>(params T[] items)
    {
        return new List<T>(items.Copy());
    }

    /// <summary>
    /// Creates an immutable list of the specified items.
    /// </summary>
    /// <param name="items">The items which will populate the immutable list.</param>
    /// <typeparam name="T">The underlying type of the immutable list.</typeparam>
    /// <returns>Returns an immutable list populated with the specified items.</returns>
    public static ImmutableList<T> ImmutableListOf<T>(params T[] items)
    {
        return ListOf(items).ToImmutableList();
    }

    /// <summary>
    /// Create a dictionary of the specified items.
    /// </summary>
    /// <param name="items">The items which wil populate the dictionary.</param>
    /// <typeparam name="TKey">The underlying type of the dictionary key.</typeparam>
    /// <typeparam name="TValue">The underlying type of the dictionary value.</typeparam>
    /// <returns>Returns a dictionary populated with the specified items.</returns>
    public static Dictionary<TKey, TValue> DictionaryOf<TKey, TValue>(params KeyValuePair<TKey, TValue>[] items) where TKey : notnull
    {
        return new Dictionary<TKey, TValue>(items.Copy());
    }

    /// <summary>
    /// Create a dictionary of the specified items.
    /// </summary>
    /// <param name="items">The items which wil populate the dictionary.</param>
    /// <typeparam name="TKey">The underlying type of the dictionary key.</typeparam>
    /// <typeparam name="TValue">The underlying type of the dictionary value.</typeparam>
    /// <returns>Returns a dictionary populated with the specified items.</returns>
    public static Dictionary<TKey, TValue> DictionaryOf<TKey, TValue>(params (TKey key, TValue value)[] items) where TKey : notnull
    {
        return DictionaryOf(items.Select(item => new KeyValuePair<TKey, TValue>(item.key, item.value)).ToArray());
    }

    /// <summary>
    /// Create an immutable dictionary of the specified items.
    /// </summary>
    /// <param name="items">The items which wil populate the immutable dictionary.</param>
    /// <typeparam name="TKey">The underlying type of the immutable dictionary key.</typeparam>
    /// <typeparam name="TValue">The underlying type of the immutable dictionary value.</typeparam>
    /// <returns>Returns an immutable dictionary populated with the specified items.</returns>
    public static ImmutableDictionary<TKey, TValue> ImmutableDictionaryOf<TKey, TValue>(
        params KeyValuePair<TKey, TValue>[] items) where TKey : notnull
    {
        return DictionaryOf(items).ToImmutableDictionary();
    }
    
    /// <summary>
    /// Create an immutable dictionary of the specified items.
    /// </summary>
    /// <param name="items">The items which wil populate the immutable dictionary.</param>
    /// <typeparam name="TKey">The underlying type of the immutable dictionary key.</typeparam>
    /// <typeparam name="TValue">The underlying type of the immutable dictionary value.</typeparam>
    /// <returns>Returns an immutable dictionary populated with the specified items.</returns>
    public static ImmutableDictionary<TKey, TValue> ImmutableDictionaryOf<TKey, TValue>(
        params (TKey key, TValue value)[] items) where TKey : notnull
    {
        return DictionaryOf(items).ToImmutableDictionary();
    }

    /// <summary>
    /// Create a sorted dictionary of the specified items.
    /// </summary>
    /// <param name="items">The items which wil populate the sorted dictionary.</param>
    /// <typeparam name="TKey">The underlying type of the sorted dictionary key.</typeparam>
    /// <typeparam name="TValue">The underlying type of the sorted dictionary value.</typeparam>
    /// <returns>Returns a sorted dictionary populated with the specified items.</returns>
    public static SortedDictionary<TKey, TValue> SortedDictionaryOf<TKey, TValue>(
        params KeyValuePair<TKey, TValue>[] items) where TKey : notnull
    {
        return new SortedDictionary<TKey, TValue>(DictionaryOf(items));
    }
    
    /// <summary>
    /// Create a sorted dictionary of the specified items.
    /// </summary>
    /// <param name="items">The items which wil populate the sorted dictionary.</param>
    /// <typeparam name="TKey">The underlying type of the sorted dictionary key.</typeparam>
    /// <typeparam name="TValue">The underlying type of the sorted dictionary value.</typeparam>
    /// <returns>Returns a sorted dictionary populated with the specified items.</returns>
    public static SortedDictionary<TKey, TValue> SortedDictionaryOf<TKey, TValue>(
        params (TKey key, TValue value)[] items) where TKey : notnull
    {
        return new SortedDictionary<TKey, TValue>(DictionaryOf(items));
    }
    
    /// <summary>
    /// Create an immutable sorted dictionary of the specified items.
    /// </summary>
    /// <param name="items">The items which wil populate the immutable sorted dictionary.</param>
    /// <typeparam name="TKey">The underlying type of the immutable sorted dictionary key.</typeparam>
    /// <typeparam name="TValue">The underlying type of the immutable sorted dictionary value.</typeparam>
    /// <returns>Returns an immutable sorted dictionary populated with the specified items.</returns>
    public static ImmutableSortedDictionary<TKey, TValue> ImmutableSortedDictionaryOf<TKey, TValue>(
        params KeyValuePair<TKey, TValue>[] items) where TKey : notnull
    {
        return SortedDictionaryOf(items).ToImmutableSortedDictionary();
    }

    /// <summary>
    /// Create an immutable sorted dictionary of the specified items.
    /// </summary>
    /// <param name="items">The items which wil populate the immutable sorted dictionary.</param>
    /// <typeparam name="TKey">The underlying type of the immutable sorted dictionary key.</typeparam>
    /// <typeparam name="TValue">The underlying type of the immutable sorted dictionary value.</typeparam>
    /// <returns>Returns an immutable sorted dictionary populated with the specified items.</returns>
    public static ImmutableSortedDictionary<TKey, TValue> ImmutableSortedDictionaryOf<TKey, TValue>(
        params (TKey key, TValue value)[] items) where TKey : notnull
    {
        return SortedDictionaryOf(items).ToImmutableSortedDictionary();
    }

    /// <summary>
    /// Creates a hash set of the specified items.
    /// </summary>
    /// <param name="items">The items which will populate the hash set.</param>
    /// <typeparam name="T">The underlying type of the hash set.</typeparam>
    /// <returns>Returns a hash set populated with the specified items.</returns>
    public static HashSet<T> HashSetOf<T>(params T[] items)
    {
        return new HashSet<T>(items.Copy());
    }

    /// <summary>
    /// Creates an immutable hash set of the specified items.
    /// </summary>
    /// <param name="items">The items which will populate the immutable hash set.</param>
    /// <typeparam name="T">The underlying type of the immutable hash set.</typeparam>
    /// <returns>Returns an immutable hash set populated with the specified items.</returns>
    public static ImmutableHashSet<T> ImmutableHashSetOf<T>(params T[] items)
    {
        return HashSetOf(items).ToImmutableHashSet();
    }

    /// <summary>
    /// Creates a sorted set of the specified items.
    /// </summary>
    /// <param name="items">The items which will populate the sorted set.</param>
    /// <typeparam name="T">The underlying type of the sorted set.</typeparam>
    /// <returns>Returns a sorted set populated with the specified items.</returns>
    public static SortedSet<T> SortedSetOf<T>(params T[] items)
    {
        return new SortedSet<T>(items.Copy());
    }

    /// <summary>
    /// Creates an immutable sorted set of the specified items.
    /// </summary>
    /// <param name="items">The items which will populate the immutable sorted set.</param>
    /// <typeparam name="T">The underlying type of the immutable sorted set.</typeparam>
    /// <returns>Returns an immutable sorted set populated with the specified items.</returns>
    public static ImmutableSortedSet<T> ImmutableSortedSetOf<T>(params T[] items)
    {
        return SortedSetOf(items).ToImmutableSortedSet();
    }

    /// <summary>
    /// Creates a stack of the specified items.
    /// </summary>
    /// <param name="items">The items which will populate the stack.</param>
    /// <typeparam name="T">The underlying type of the stack.</typeparam>
    /// <returns>Returns a stack populated with the specified items.</returns>
    public static Stack<T> StackOf<T>(params T[] items)
    {
        return new Stack<T>(items.Copy());
    }

    /// <summary>
    /// Creates an immutable stack of the specified items.
    /// </summary>
    /// <param name="items">The items which will populate the immutable stack.</param>
    /// <typeparam name="T">The underlying type of the immutable stack.</typeparam>
    /// <returns>Returns an immutable stack populated with the specified items.</returns>
    public static ImmutableStack<T> ImmutableStackOf<T>(params T[] items)
    {
        return ImmutableStack.Create(items.Copy());
    }

    /// <summary>
    /// Creates a queue of the specified items.
    /// </summary>
    /// <param name="items">The items which will populate the queue.</param>
    /// <typeparam name="T">The underlying type of the queue.</typeparam>
    /// <returns>Returns a queue populated with the specified items.</returns>
    public static Queue<T> QueueOf<T>(params T[] items)
    {
        return new Queue<T>(items.Copy());
    }

    /// <summary>
    /// Creates an immutable queue of the specified items.
    /// </summary>
    /// <param name="items">The items which will populate the immutable queue.</param>
    /// <typeparam name="T">The underlying type of the immutable queue.</typeparam>
    /// <returns>Returns an immutable queue populated with the specified items.</returns>
    public static ImmutableQueue<T> ImmutableQueueOf<T>(params T[] items)
    {
        return ImmutableQueue.Create(items.Copy());
    }
}