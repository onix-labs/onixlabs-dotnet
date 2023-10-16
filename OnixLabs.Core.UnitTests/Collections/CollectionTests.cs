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

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using OnixLabs.Core.Collections;
using Xunit;

namespace OnixLabs.Core.UnitTests.Collections;

public sealed class CollectionTests
{
    private static readonly object[] EnumerableInitializers = { true, false, 123, "abc", 1.23, true, false, 123, "abc", 1.23 };

    private static readonly int[] NumericInitializers = { 123, 456, 789, 0, 1, -1, -987, 123, 456, 789, 0, 1, -1, -987 };

    private static readonly KeyValuePair<object, object>[] DictionaryInitializers =
    {
        new(123, true),
        new("abc", null),
        new(123.45f, "value")
    };

    private static readonly KeyValuePair<string, object>[] SortedDictionaryInitializers =
    {
        new("key1", 123),
        new("key2", false),
        new("key3", "abc")
    };

    [Fact(DisplayName = "Collections.EmptyEnumerable should produce the expected result")]
    public void CollectionsEmptyEnumerableShouldProduceTheExpectedResult()
    {
        // Given
        IEnumerable<object> expected = Enumerable.Empty<object>();
        IEnumerable<object> actual = Collection.EmptyEnumerable<object>();

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Collections.EmptyArray should produce the expected result")]
    public void CollectionsEmptyArrayShouldProduceTheExpectedResult()
    {
        // Given
        object[] expected = Array.Empty<object>();
        object[] actual = Collection.EmptyArray<object>();

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Collections.EmptyImmutableArray should produce the expected result")]
    public void CollectionsEmptyImmutableArrayShouldProduceTheExpectedResult()
    {
        // Given
        ImmutableArray<object> expected = ImmutableArray.Create<object>();
        ImmutableArray<object> actual = Collection.EmptyImmutableArray<object>();

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Collections.EmptyList should produce the expected result")]
    public void CollectionsEmptyListShouldProduceTheExpectedResult()
    {
        // Given
        List<object> expected = new();
        List<object> actual = Collection.EmptyList<object>();

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Collections.EmptyImmutableList should produce the expected result")]
    public void CollectionsEmptyImmutableListShouldProduceTheExpectedResult()
    {
        // Given
        ImmutableList<object> expected = ImmutableList.Create<object>();
        ImmutableList<object> actual = Collection.EmptyImmutableList<object>();

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Collections.EmptyDictionary should produce the expected result")]
    public void CollectionsEmptyDictionaryShouldProduceTheExpectedResult()
    {
        // Given
        Dictionary<object, object> expected = new();
        Dictionary<object, object> actual = Collection.EmptyDictionary<object, object>();

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Collections.EmptyImmutableDictionary should produce the expected result")]
    public void CollectionsEmptyImmutableDictionaryShouldProduceTheExpectedResult()
    {
        // Given
        ImmutableDictionary<object, object> expected = ImmutableDictionary.Create<object, object>();
        ImmutableDictionary<object, object> actual = Collection.EmptyImmutableDictionary<object, object>();

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Collections.EmptySortedDictionary should produce the expected result")]
    public void CollectionsEmptySortedDictionaryShouldProduceTheExpectedResult()
    {
        // Given
        SortedDictionary<object, object> expected = new();
        SortedDictionary<object, object> actual = Collection.EmptySortedDictionary<object, object>();

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Collections.EmptyImmutableSortedDictionary should produce the expected result")]
    public void CollectionsEmptyImmutableSortedDictionaryShouldProduceTheExpectedResult()
    {
        // Given
        ImmutableSortedDictionary<object, object> expected = ImmutableSortedDictionary.Create<object, object>();
        ImmutableSortedDictionary<object, object> actual = Collection.EmptyImmutableSortedDictionary<object, object>();

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Collections.EmptyHashSet should produce the expected result")]
    public void CollectionsEmptyHashSetShouldProduceTheExpectedResult()
    {
        // Given
        HashSet<object> expected = new();
        HashSet<object> actual = Collection.EmptyHashSet<object>();

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Collections.EmptyImmutableHashSet should produce the expected result")]
    public void CollectionsEmptyImmutableHashSetShouldProduceTheExpectedResult()
    {
        // Given
        ImmutableHashSet<object> expected = ImmutableHashSet.Create<object>();
        ImmutableHashSet<object> actual = Collection.EmptyImmutableHashSet<object>();

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Collections.EmptySortedSet should produce the expected result")]
    public void CollectionsEmptySortedSetShouldProduceTheExpectedResult()
    {
        // Given
        SortedSet<object> expected = new();
        SortedSet<object> actual = Collection.EmptySortedSet<object>();

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Collections.EmptyImmutableSortedSet should produce the expected result")]
    public void CollectionsEmptyImmutableSortedSetShouldProduceTheExpectedResult()
    {
        // Given
        ImmutableSortedSet<object> expected = ImmutableSortedSet.Create<object>();
        ImmutableSortedSet<object> actual = Collection.EmptyImmutableSortedSet<object>();

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Collections.EmptyStack should produce the expected result")]
    public void CollectionsEmptyStackShouldProduceTheExpectedResult()
    {
        // Given
        Stack<object> expected = new();
        Stack<object> actual = Collection.EmptyStack<object>();

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Collections.EmptyImmutableStack should produce the expected result")]
    public void CollectionsEmptyImmutableStackShouldProduceTheExpectedResult()
    {
        // Given
        ImmutableStack<object> expected = ImmutableStack.Create<object>();
        ImmutableStack<object> actual = Collection.EmptyImmutableStack<object>();

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Collections.EmptyQueue should produce the expected result")]
    public void CollectionsEmptyQueueShouldProduceTheExpectedResult()
    {
        // Given
        Queue<object> expected = new();
        Queue<object> actual = Collection.EmptyQueue<object>();

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Collections.EmptyImmutableQueue should produce the expected result")]
    public void CollectionsEmptyImmutableQueueShouldProduceTheExpectedResult()
    {
        // Given
        ImmutableQueue<object> expected = ImmutableQueue.Create<object>();
        ImmutableQueue<object> actual = Collection.EmptyImmutableQueue<object>();

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Collections.EnumerableOf should return the expected result")]
    public void CollectionsEnumerableOfShouldReturnTheExpectedResult()
    {
        // Given
        IEnumerable<object> expected = EnumerableInitializers;
        IEnumerable<object> actual = Collection.EnumerableOf(EnumerableInitializers);

        // Then
        Assert.True(expected.SequenceEqual(actual));
    }

    [Fact(DisplayName = "Collections.ArrayOf should return the expected result")]
    public void CollectionsArrayOfShouldReturnTheExpectedResult()
    {
        // Given
        object[] expected = EnumerableInitializers.ToArray();
        object[] actual = Collection.ArrayOf(EnumerableInitializers);

        // Then
        Assert.True(expected.SequenceEqual(actual));
    }

    [Fact(DisplayName = "Collections.ImmutableArrayOf should return the expected result")]
    public void CollectionsImmutableArrayOfShouldReturnTheExpectedResult()
    {
        // Given
        ImmutableArray<object> expected = ImmutableArray.Create(EnumerableInitializers);
        ImmutableArray<object> actual = Collection.ImmutableArrayOf(EnumerableInitializers);

        // Then
        Assert.True(expected.SequenceEqual(actual));
    }

    [Fact(DisplayName = "Collections.ListOf should return the expected result")]
    public void CollectionsListOfShouldReturnTheExpectedResult()
    {
        // Given
        List<object> expected = new(EnumerableInitializers);
        List<object> actual = Collection.ListOf(EnumerableInitializers);

        // Then
        Assert.True(expected.SequenceEqual(actual));
    }

    [Fact(DisplayName = "Collections.ImmutableListOf should return the expected result")]
    public void CollectionsImmutableListOfShouldReturnTheExpectedResult()
    {
        // Given
        ImmutableList<object> expected = ImmutableList.Create(EnumerableInitializers);
        ImmutableList<object> actual = Collection.ImmutableListOf(EnumerableInitializers);

        // Then
        Assert.True(expected.SequenceEqual(actual));
    }

    [Fact(DisplayName = "Collections.DictionaryOf should return the expected result")]
    public void CollectionsDictionaryOfShouldReturnTheExpectedResult()
    {
        // Given
        Dictionary<object, object> expected = new(DictionaryInitializers);
        Dictionary<object, object> actual = Collection.DictionaryOf(DictionaryInitializers);

        // Then
        Assert.True(expected.SequenceEqual(actual));
    }

    [Fact(DisplayName = "Collections.ImmutableDictionaryOf should return the expected result")]
    public void CollectionsImmutableDictionaryOfShouldReturnTheExpectedResult()
    {
        // Given
        ImmutableDictionary<object, object> expected = new Dictionary<object, object>(DictionaryInitializers).ToImmutableDictionary();
        ImmutableDictionary<object, object> actual = Collection.ImmutableDictionaryOf(DictionaryInitializers);

        // Then
        Assert.True(expected.SequenceEqual(actual));
    }

    [Fact(DisplayName = "Collections.SortedDictionaryOf should return the expected result")]
    public void CollectionsSortedDictionaryOfShouldReturnTheExpectedResult()
    {
        // Given
        SortedDictionary<string, object> expected = new(Collection.DictionaryOf(SortedDictionaryInitializers));
        SortedDictionary<string, object> actual = Collection.SortedDictionaryOf(SortedDictionaryInitializers);

        // Then
        Assert.True(expected.SequenceEqual(actual));
    }

    [Fact(DisplayName = "Collections.ImmutableSortedDictionaryOf should return the expected result")]
    public void CollectionsImmutableSortedDictionaryOfShouldReturnTheExpectedResult()
    {
        // Given
        SortedDictionary<string, object> sorted = new(Collection.DictionaryOf(SortedDictionaryInitializers));
        ImmutableSortedDictionary<string, object> expected = sorted.ToImmutableSortedDictionary();
        ImmutableSortedDictionary<string, object> actual = Collection.ImmutableSortedDictionaryOf(SortedDictionaryInitializers);

        // Then
        Assert.True(expected.SequenceEqual(actual));
    }

    [Fact(DisplayName = "Collections.HashSetOf should return the expected result")]
    public void CollectionsHashSetOfShouldReturnTheExpectedResult()
    {
        // Given
        HashSet<object> expected = new(EnumerableInitializers);
        HashSet<object> actual = Collection.HashSetOf(EnumerableInitializers);

        // Then
        Assert.True(expected.SequenceEqual(actual));
    }

    [Fact(DisplayName = "Collections.ImmutableHashSetOf should return the expected result")]
    public void CollectionsImmutableHashSetOfShouldReturnTheExpectedResult()
    {
        // Given
        ImmutableHashSet<object> expected = ImmutableHashSet.Create(EnumerableInitializers);
        ImmutableHashSet<object> actual = Collection.ImmutableHashSetOf(EnumerableInitializers);

        // Then
        Assert.True(expected.SequenceEqual(actual));
    }

    [Fact(DisplayName = "Collections.SortedSetOf should return the expected result")]
    public void CollectionsSortedSetOfShouldReturnTheExpectedResult()
    {
        // Given
        SortedSet<int> expected = new(NumericInitializers);
        SortedSet<int> actual = Collection.SortedSetOf(NumericInitializers);

        // Then
        Assert.True(expected.SequenceEqual(actual));
    }

    [Fact(DisplayName = "Collections.ImmutableSortedSetOf should return the expected result")]
    public void CollectionsImmutableSortedSetOfShouldReturnTheExpectedResult()
    {
        // Given
        ImmutableSortedSet<int> expected = ImmutableSortedSet.Create(NumericInitializers);
        ImmutableSortedSet<int> actual = Collection.ImmutableSortedSetOf(NumericInitializers);

        // Then
        Assert.True(expected.SequenceEqual(actual));
    }

    [Fact(DisplayName = "Collections.StackOf should return the expected result")]
    public void CollectionsStackOfShouldReturnTheExpectedResult()
    {
        // Given
        Stack<object> expected = new(EnumerableInitializers);
        Stack<object> actual = Collection.StackOf(EnumerableInitializers);

        // Then
        Assert.True(expected.SequenceEqual(actual));
    }

    [Fact(DisplayName = "Collections.ImmutableStackOf should return the expected result")]
    public void CollectionsImmutableStackOfShouldReturnTheExpectedResult()
    {
        // Given
        ImmutableStack<object> expected = ImmutableStack.Create(EnumerableInitializers);
        ImmutableStack<object> actual = Collection.ImmutableStackOf(EnumerableInitializers);

        // Then
        Assert.True(expected.SequenceEqual(actual));
    }

    [Fact(DisplayName = "Collections.QueueOf should return the expected result")]
    public void CollectionsQueueOfShouldReturnTheExpectedResult()
    {
        // Given
        Queue<object> expected = new(EnumerableInitializers);
        Queue<object> actual = Collection.QueueOf(EnumerableInitializers);

        // Then
        Assert.True(expected.SequenceEqual(actual));
    }

    [Fact(DisplayName = "Collections.ImmutableQueueOf should return the expected result")]
    public void CollectionsImmutableQueueOfShouldReturnTheExpectedResult()
    {
        // Given
        ImmutableQueue<object> expected = ImmutableQueue.Create(EnumerableInitializers);
        ImmutableQueue<object> actual = Collection.ImmutableQueueOf(EnumerableInitializers);

        // Then
        Assert.True(expected.SequenceEqual(actual));
    }
}
