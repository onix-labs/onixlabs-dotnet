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

namespace OnixLabs.Core.UnitTests;

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
        // Arrange
        IEnumerable<object> expected = Enumerable.Empty<object>();
        IEnumerable<object> actual = Collection.EmptyEnumerable<object>();

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Collections.EmptyArray should produce the expected result")]
    public void CollectionsEmptyArrayShouldProduceTheExpectedResult()
    {
        // Arrange
        object[] expected = Array.Empty<object>();
        object[] actual = Collection.EmptyArray<object>();

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Collections.EmptyImmutableArray should produce the expected result")]
    public void CollectionsEmptyImmutableArrayShouldProduceTheExpectedResult()
    {
        // Arrange
        ImmutableArray<object> expected = ImmutableArray.Create<object>();
        ImmutableArray<object> actual = Collection.EmptyImmutableArray<object>();

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Collections.EmptyList should produce the expected result")]
    public void CollectionsEmptyListShouldProduceTheExpectedResult()
    {
        // Arrange
        List<object> expected = new();
        List<object> actual = Collection.EmptyList<object>();

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Collections.EmptyImmutableList should produce the expected result")]
    public void CollectionsEmptyImmutableListShouldProduceTheExpectedResult()
    {
        // Arrange
        ImmutableList<object> expected = ImmutableList.Create<object>();
        ImmutableList<object> actual = Collection.EmptyImmutableList<object>();

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Collections.EmptyDictionary should produce the expected result")]
    public void CollectionsEmptyDictionaryShouldProduceTheExpectedResult()
    {
        // Arrange
        Dictionary<object, object> expected = new();
        Dictionary<object, object> actual = Collection.EmptyDictionary<object, object>();

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Collections.EmptyImmutableDictionary should produce the expected result")]
    public void CollectionsEmptyImmutableDictionaryShouldProduceTheExpectedResult()
    {
        // Arrange
        ImmutableDictionary<object, object> expected = ImmutableDictionary.Create<object, object>();
        ImmutableDictionary<object, object> actual = Collection.EmptyImmutableDictionary<object, object>();

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Collections.EmptySortedDictionary should produce the expected result")]
    public void CollectionsEmptySortedDictionaryShouldProduceTheExpectedResult()
    {
        // Arrange
        SortedDictionary<object, object> expected = new();
        SortedDictionary<object, object> actual = Collection.EmptySortedDictionary<object, object>();

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Collections.EmptyImmutableSortedDictionary should produce the expected result")]
    public void CollectionsEmptyImmutableSortedDictionaryShouldProduceTheExpectedResult()
    {
        // Arrange
        ImmutableSortedDictionary<object, object> expected = ImmutableSortedDictionary.Create<object, object>();
        ImmutableSortedDictionary<object, object> actual = Collection.EmptyImmutableSortedDictionary<object, object>();

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Collections.EmptyHashSet should produce the expected result")]
    public void CollectionsEmptyHashSetShouldProduceTheExpectedResult()
    {
        // Arrange
        HashSet<object> expected = new();
        HashSet<object> actual = Collection.EmptyHashSet<object>();

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Collections.EmptyImmutableHashSet should produce the expected result")]
    public void CollectionsEmptyImmutableHashSetShouldProduceTheExpectedResult()
    {
        // Arrange
        ImmutableHashSet<object> expected = ImmutableHashSet.Create<object>();
        ImmutableHashSet<object> actual = Collection.EmptyImmutableHashSet<object>();

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Collections.EmptySortedSet should produce the expected result")]
    public void CollectionsEmptySortedSetShouldProduceTheExpectedResult()
    {
        // Arrange
        SortedSet<object> expected = new();
        SortedSet<object> actual = Collection.EmptySortedSet<object>();

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Collections.EmptyImmutableSortedSet should produce the expected result")]
    public void CollectionsEmptyImmutableSortedSetShouldProduceTheExpectedResult()
    {
        // Arrange
        ImmutableSortedSet<object> expected = ImmutableSortedSet.Create<object>();
        ImmutableSortedSet<object> actual = Collection.EmptyImmutableSortedSet<object>();

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Collections.EmptyStack should produce the expected result")]
    public void CollectionsEmptyStackShouldProduceTheExpectedResult()
    {
        // Arrange
        Stack<object> expected = new();
        Stack<object> actual = Collection.EmptyStack<object>();

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Collections.EmptyImmutableStack should produce the expected result")]
    public void CollectionsEmptyImmutableStackShouldProduceTheExpectedResult()
    {
        // Arrange
        ImmutableStack<object> expected = ImmutableStack.Create<object>();
        ImmutableStack<object> actual = Collection.EmptyImmutableStack<object>();

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Collections.EmptyQueue should produce the expected result")]
    public void CollectionsEmptyQueueShouldProduceTheExpectedResult()
    {
        // Arrange
        Queue<object> expected = new();
        Queue<object> actual = Collection.EmptyQueue<object>();

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Collections.EmptyImmutableQueue should produce the expected result")]
    public void CollectionsEmptyImmutableQueueShouldProduceTheExpectedResult()
    {
        // Arrange
        ImmutableQueue<object> expected = ImmutableQueue.Create<object>();
        ImmutableQueue<object> actual = Collection.EmptyImmutableQueue<object>();

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Collections.EnumerableOf should return the expected result")]
    public void CollectionsEnumerableOfShouldReturnTheExpectedResult()
    {
        // Arrange
        IEnumerable<object> expected = EnumerableInitializers;
        IEnumerable<object> actual = Collection.EnumerableOf(EnumerableInitializers);

        // Assert
        Assert.True(expected.SequenceEqual(actual));
    }

    [Fact(DisplayName = "Collections.ArrayOf should return the expected result")]
    public void CollectionsArrayOfShouldReturnTheExpectedResult()
    {
        // Arrange
        object[] expected = EnumerableInitializers.ToArray();
        object[] actual = Collection.ArrayOf(EnumerableInitializers);

        // Assert
        Assert.True(expected.SequenceEqual(actual));
    }

    [Fact(DisplayName = "Collections.ImmutableArrayOf should return the expected result")]
    public void CollectionsImmutableArrayOfShouldReturnTheExpectedResult()
    {
        // Arrange
        ImmutableArray<object> expected = ImmutableArray.Create(EnumerableInitializers);
        ImmutableArray<object> actual = Collection.ImmutableArrayOf(EnumerableInitializers);

        // Assert
        Assert.True(expected.SequenceEqual(actual));
    }

    [Fact(DisplayName = "Collections.ListOf should return the expected result")]
    public void CollectionsListOfShouldReturnTheExpectedResult()
    {
        // Arrange
        List<object> expected = new(EnumerableInitializers);
        List<object> actual = Collection.ListOf(EnumerableInitializers);

        // Assert
        Assert.True(expected.SequenceEqual(actual));
    }

    [Fact(DisplayName = "Collections.ImmutableListOf should return the expected result")]
    public void CollectionsImmutableListOfShouldReturnTheExpectedResult()
    {
        // Arrange
        ImmutableList<object> expected = ImmutableList.Create(EnumerableInitializers);
        ImmutableList<object> actual = Collection.ImmutableListOf(EnumerableInitializers);

        // Assert
        Assert.True(expected.SequenceEqual(actual));
    }

    [Fact(DisplayName = "Collections.DictionaryOf should return the expected result")]
    public void CollectionsDictionaryOfShouldReturnTheExpectedResult()
    {
        // Arrange
        Dictionary<object, object> expected = new(DictionaryInitializers);
        Dictionary<object, object> actual = Collection.DictionaryOf(DictionaryInitializers);

        // Assert
        Assert.True(expected.SequenceEqual(actual));
    }

    [Fact(DisplayName = "Collections.ImmutableDictionaryOf should return the expected result")]
    public void CollectionsImmutableDictionaryOfShouldReturnTheExpectedResult()
    {
        // Arrange
        ImmutableDictionary<object, object> expected = new Dictionary<object, object>(DictionaryInitializers).ToImmutableDictionary();
        ImmutableDictionary<object, object> actual = Collection.ImmutableDictionaryOf(DictionaryInitializers);

        // Assert
        Assert.True(expected.SequenceEqual(actual));
    }

    [Fact(DisplayName = "Collections.SortedDictionaryOf should return the expected result")]
    public void CollectionsSortedDictionaryOfShouldReturnTheExpectedResult()
    {
        // Arrange
        SortedDictionary<string, object> expected = new(Collection.DictionaryOf(SortedDictionaryInitializers));
        SortedDictionary<string, object> actual = Collection.SortedDictionaryOf(SortedDictionaryInitializers);

        // Assert
        Assert.True(expected.SequenceEqual(actual));
    }

    [Fact(DisplayName = "Collections.ImmutableSortedDictionaryOf should return the expected result")]
    public void CollectionsImmutableSortedDictionaryOfShouldReturnTheExpectedResult()
    {
        // Arrange
        SortedDictionary<string, object> sorted = new(Collection.DictionaryOf(SortedDictionaryInitializers));
        ImmutableSortedDictionary<string, object> expected = sorted.ToImmutableSortedDictionary();
        ImmutableSortedDictionary<string, object> actual = Collection.ImmutableSortedDictionaryOf(SortedDictionaryInitializers);

        // Assert
        Assert.True(expected.SequenceEqual(actual));
    }

    [Fact(DisplayName = "Collections.HashSetOf should return the expected result")]
    public void CollectionsHashSetOfShouldReturnTheExpectedResult()
    {
        // Arrange
        HashSet<object> expected = new(EnumerableInitializers);
        HashSet<object> actual = Collection.HashSetOf(EnumerableInitializers);

        // Assert
        Assert.True(expected.SequenceEqual(actual));
    }

    [Fact(DisplayName = "Collections.ImmutableHashSetOf should return the expected result")]
    public void CollectionsImmutableHashSetOfShouldReturnTheExpectedResult()
    {
        // Arrange
        ImmutableHashSet<object> expected = ImmutableHashSet.Create(EnumerableInitializers);
        ImmutableHashSet<object> actual = Collection.ImmutableHashSetOf(EnumerableInitializers);

        // Assert
        Assert.True(expected.SequenceEqual(actual));
    }

    [Fact(DisplayName = "Collections.SortedSetOf should return the expected result")]
    public void CollectionsSortedSetOfShouldReturnTheExpectedResult()
    {
        // Arrange
        SortedSet<int> expected = new(NumericInitializers);
        SortedSet<int> actual = Collection.SortedSetOf(NumericInitializers);

        // Assert
        Assert.True(expected.SequenceEqual(actual));
    }

    [Fact(DisplayName = "Collections.ImmutableSortedSetOf should return the expected result")]
    public void CollectionsImmutableSortedSetOfShouldReturnTheExpectedResult()
    {
        // Arrange
        ImmutableSortedSet<int> expected = ImmutableSortedSet.Create(NumericInitializers);
        ImmutableSortedSet<int> actual = Collection.ImmutableSortedSetOf(NumericInitializers);

        // Assert
        Assert.True(expected.SequenceEqual(actual));
    }

    [Fact(DisplayName = "Collections.StackOf should return the expected result")]
    public void CollectionsStackOfShouldReturnTheExpectedResult()
    {
        // Arrange
        Stack<object> expected = new(EnumerableInitializers);
        Stack<object> actual = Collection.StackOf(EnumerableInitializers);

        // Assert
        Assert.True(expected.SequenceEqual(actual));
    }

    [Fact(DisplayName = "Collections.ImmutableStackOf should return the expected result")]
    public void CollectionsImmutableStackOfShouldReturnTheExpectedResult()
    {
        // Arrange
        ImmutableStack<object> expected = ImmutableStack.Create(EnumerableInitializers);
        ImmutableStack<object> actual = Collection.ImmutableStackOf(EnumerableInitializers);

        // Assert
        Assert.True(expected.SequenceEqual(actual));
    }

    [Fact(DisplayName = "Collections.QueueOf should return the expected result")]
    public void CollectionsQueueOfShouldReturnTheExpectedResult()
    {
        // Arrange
        Queue<object> expected = new(EnumerableInitializers);
        Queue<object> actual = Collection.QueueOf(EnumerableInitializers);

        // Assert
        Assert.True(expected.SequenceEqual(actual));
    }

    [Fact(DisplayName = "Collections.ImmutableQueueOf should return the expected result")]
    public void CollectionsImmutableQueueOfShouldReturnTheExpectedResult()
    {
        // Arrange
        ImmutableQueue<object> expected = ImmutableQueue.Create(EnumerableInitializers);
        ImmutableQueue<object> actual = Collection.ImmutableQueueOf(EnumerableInitializers);

        // Assert
        Assert.True(expected.SequenceEqual(actual));
    }
}
