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
using Xunit;
using static OnixLabs.Core.Collections.Collection;

namespace OnixLabs.Core.UnitTests.Collections;

public sealed class CollectionTests
{
    private static readonly KeyValuePair<object, object?>[] DictionaryInitializers =
        [new(123, true), new("abc", null), new(123.45f, "value")];

    private static readonly KeyValuePair<string, object>[] SortedDictionaryInitializers =
        [new("key1", 123), new("key2", false), new("key3", "abc")];

    [Fact(DisplayName = "Collections.EmptyEnumerable should produce the expected result")]
    public void CollectionsEmptyEnumerableShouldProduceTheExpectedResult()
    {
        // Given
        IEnumerable<object> expected = [];
        IEnumerable<object> actual = EmptyEnumerable<object>();

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Collections.EmptyArray should produce the expected result")]
    public void CollectionsEmptyArrayShouldProduceTheExpectedResult()
    {
        // Given
        object[] expected = [];
        object[] actual = EmptyArray<object>();

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Collections.EmptyImmutableArray should produce the expected result")]
    public void CollectionsEmptyImmutableArrayShouldProduceTheExpectedResult()
    {
        // Given
        ImmutableArray<object> expected = [];
        ImmutableArray<object> actual = EmptyImmutableArray<object>();

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Collections.EmptyList should produce the expected result")]
    public void CollectionsEmptyListShouldProduceTheExpectedResult()
    {
        // Given
        List<object> expected = [];
        List<object> actual = EmptyList<object>();

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Collections.EmptyImmutableList should produce the expected result")]
    public void CollectionsEmptyImmutableListShouldProduceTheExpectedResult()
    {
        // Given
        ImmutableList<object> expected = [];
        ImmutableList<object> actual = EmptyImmutableList<object>();

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Collections.EmptyDictionary should produce the expected result")]
    public void CollectionsEmptyDictionaryShouldProduceTheExpectedResult()
    {
        // Given
        Dictionary<object, object> expected = [];
        Dictionary<object, object> actual = EmptyDictionary<object, object>();

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Collections.EmptyImmutableDictionary should produce the expected result")]
    public void CollectionsEmptyImmutableDictionaryShouldProduceTheExpectedResult()
    {
        // Given
        ImmutableDictionary<object, object> expected = ImmutableDictionary.Create<object, object>();
        ImmutableDictionary<object, object> actual = EmptyImmutableDictionary<object, object>();

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Collections.EmptySortedDictionary should produce the expected result")]
    public void CollectionsEmptySortedDictionaryShouldProduceTheExpectedResult()
    {
        // Given
        SortedDictionary<object, object> expected = [];
        SortedDictionary<object, object> actual = EmptySortedDictionary<object, object>();

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Collections.EmptyImmutableSortedDictionary should produce the expected result")]
    public void CollectionsEmptyImmutableSortedDictionaryShouldProduceTheExpectedResult()
    {
        // Given
        ImmutableSortedDictionary<object, object> expected = ImmutableSortedDictionary.Create<object, object>();
        ImmutableSortedDictionary<object, object> actual = EmptyImmutableSortedDictionary<object, object>();

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Collections.EmptyHashSet should produce the expected result")]
    public void CollectionsEmptyHashSetShouldProduceTheExpectedResult()
    {
        // Given
        HashSet<object> expected = [];
        HashSet<object> actual = EmptyHashSet<object>();

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Collections.EmptyImmutableHashSet should produce the expected result")]
    public void CollectionsEmptyImmutableHashSetShouldProduceTheExpectedResult()
    {
        // Given
        ImmutableHashSet<object> expected = [];
        ImmutableHashSet<object> actual = EmptyImmutableHashSet<object>();

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Collections.EmptySortedSet should produce the expected result")]
    public void CollectionsEmptySortedSetShouldProduceTheExpectedResult()
    {
        // Given
        SortedSet<object> expected = [];
        SortedSet<object> actual = EmptySortedSet<object>();

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Collections.EmptyImmutableSortedSet should produce the expected result")]
    public void CollectionsEmptyImmutableSortedSetShouldProduceTheExpectedResult()
    {
        // Given
        ImmutableSortedSet<object> expected = [];
        ImmutableSortedSet<object> actual = EmptyImmutableSortedSet<object>();

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Collections.EmptyStack should produce the expected result")]
    public void CollectionsEmptyStackShouldProduceTheExpectedResult()
    {
        // Given
        Stack<object> expected = [];
        Stack<object> actual = EmptyStack<object>();

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Collections.EmptyImmutableStack should produce the expected result")]
    public void CollectionsEmptyImmutableStackShouldProduceTheExpectedResult()
    {
        // Given
        ImmutableStack<object> expected = [];
        ImmutableStack<object> actual = EmptyImmutableStack<object>();

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Collections.EmptyQueue should produce the expected result")]
    public void CollectionsEmptyQueueShouldProduceTheExpectedResult()
    {
        // Given
        Queue<object> expected = [];
        Queue<object> actual = EmptyQueue<object>();

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Collections.EmptyImmutableQueue should produce the expected result")]
    public void CollectionsEmptyImmutableQueueShouldProduceTheExpectedResult()
    {
        // Given
        ImmutableQueue<object> expected = [];
        ImmutableQueue<object> actual = EmptyImmutableQueue<object>();

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Collections.EnumerableOf should return the expected result")]
    public void CollectionsEnumerableOfShouldReturnTheExpectedResult()
    {
        // Given
        IEnumerable<object> expected = [true, false, 123, "abc", 1.23, true, false, 123, "abc", 1.23];
        IEnumerable<object> actual = EnumerableOf<object>(true, false, 123, "abc", 1.23, true, false, 123, "abc", 1.23);

        // Then
        Assert.True(expected.SequenceEqual(actual));
    }

    [Fact(DisplayName = "Collections.ArrayOf should return the expected result")]
    public void CollectionsArrayOfShouldReturnTheExpectedResult()
    {
        // Given
        object[] expected = [true, false, 123, "abc", 1.23, true, false, 123, "abc", 1.23];
        object[] actual = ArrayOf<object>(true, false, 123, "abc", 1.23, true, false, 123, "abc", 1.23);

        // Then
        Assert.True(expected.SequenceEqual(actual));
    }

    [Fact(DisplayName = "Collections.ImmutableArrayOf should return the expected result")]
    public void CollectionsImmutableArrayOfShouldReturnTheExpectedResult()
    {
        // Given
        ImmutableArray<object> expected = [true, false, 123, "abc", 1.23, true, false, 123, "abc", 1.23];
        ImmutableArray<object> actual = ImmutableArrayOf<object>(true, false, 123, "abc", 1.23, true, false, 123, "abc", 1.23);

        // Then
        Assert.True(expected.SequenceEqual(actual));
    }

    [Fact(DisplayName = "Collections.ListOf should return the expected result")]
    public void CollectionsListOfShouldReturnTheExpectedResult()
    {
        // Given
        List<object> expected = [true, false, 123, "abc", 1.23, true, false, 123, "abc", 1.23];
        List<object> actual = ListOf<object>(true, false, 123, "abc", 1.23, true, false, 123, "abc", 1.23);

        // Then
        Assert.True(expected.SequenceEqual(actual));
    }

    [Fact(DisplayName = "Collections.ImmutableListOf should return the expected result")]
    public void CollectionsImmutableListOfShouldReturnTheExpectedResult()
    {
        // Given
        ImmutableList<object> expected = ImmutableList.Create<object>(true, false, 123, "abc", 1.23, true, false, 123, "abc", 1.23);
        ImmutableList<object> actual = ImmutableListOf<object>(true, false, 123, "abc", 1.23, true, false, 123, "abc", 1.23);

        // Then
        Assert.True(expected.SequenceEqual(actual));
    }

    [Fact(DisplayName = "Collections.DictionaryOf should return the expected result")]
    public void CollectionsDictionaryOfShouldReturnTheExpectedResult()
    {
        // Given
        Dictionary<object, object?> expected = new(DictionaryInitializers);
        Dictionary<object, object?> actual = DictionaryOf(DictionaryInitializers);

        // Then
        // ReSharper disable once UsageOfDefaultStructEquality
        Assert.True(expected.SequenceEqual(actual));
    }

    [Fact(DisplayName = "Collections.ImmutableDictionaryOf should return the expected result")]
    public void CollectionsImmutableDictionaryOfShouldReturnTheExpectedResult()
    {
        // Given
        ImmutableDictionary<object, object?> expected = new Dictionary<object, object?>(DictionaryInitializers).ToImmutableDictionary();
        ImmutableDictionary<object, object?> actual = ImmutableDictionaryOf(DictionaryInitializers);

        // Then
        // ReSharper disable once UsageOfDefaultStructEquality
        Assert.True(expected.SequenceEqual(actual));
    }

    [Fact(DisplayName = "Collections.SortedDictionaryOf should return the expected result")]
    public void CollectionsSortedDictionaryOfShouldReturnTheExpectedResult()
    {
        // Given
        SortedDictionary<string, object> expected = new(DictionaryOf(SortedDictionaryInitializers));
        SortedDictionary<string, object> actual = SortedDictionaryOf(SortedDictionaryInitializers);

        // Then
        // ReSharper disable once UsageOfDefaultStructEquality
        Assert.True(expected.SequenceEqual(actual));
    }

    [Fact(DisplayName = "Collections.ImmutableSortedDictionaryOf should return the expected result")]
    public void CollectionsImmutableSortedDictionaryOfShouldReturnTheExpectedResult()
    {
        // Given
        SortedDictionary<string, object> sorted = new(DictionaryOf(SortedDictionaryInitializers));
        ImmutableSortedDictionary<string, object> expected = sorted.ToImmutableSortedDictionary();
        ImmutableSortedDictionary<string, object> actual = ImmutableSortedDictionaryOf(SortedDictionaryInitializers);

        // Then
        // ReSharper disable once UsageOfDefaultStructEquality
        Assert.True(expected.SequenceEqual(actual));
    }

    [Fact(DisplayName = "Collections.HashSetOf should return the expected result")]
    public void CollectionsHashSetOfShouldReturnTheExpectedResult()
    {
        // Given
        HashSet<object> expected = [true, false, 123, "abc", 1.23, true, false, 123, "abc", 1.23];
        HashSet<object> actual = HashSetOf<object>(true, false, 123, "abc", 1.23, true, false, 123, "abc", 1.23);

        // Then
        Assert.True(expected.SequenceEqual(actual));
    }

    [Fact(DisplayName = "Collections.ImmutableHashSetOf should return the expected result")]
    public void CollectionsImmutableHashSetOfShouldReturnTheExpectedResult()
    {
        // Given
        ImmutableHashSet<object> expected = ImmutableHashSet.Create<object>(true, false, 123, "abc", 1.23, true, false, 123, "abc", 1.23);
        ImmutableHashSet<object> actual = ImmutableHashSetOf<object>(true, false, 123, "abc", 1.23, true, false, 123, "abc", 1.23);

        // Then
        Assert.True(expected.SequenceEqual(actual));
    }

    [Fact(DisplayName = "Collections.SortedSetOf should return the expected result")]
    public void CollectionsSortedSetOfShouldReturnTheExpectedResult()
    {
        // Given
        SortedSet<int> expected = new([123, 456, 789, 0, 1, -1, -987, 123, 456, 789, 0, 1, -1, -987]);
        SortedSet<int> actual = SortedSetOf(123, 456, 789, 0, 1, -1, -987, 123, 456, 789, 0, 1, -1, -987);

        // Then
        Assert.True(expected.SequenceEqual(actual));
    }

    [Fact(DisplayName = "Collections.ImmutableSortedSetOf should return the expected result")]
    public void CollectionsImmutableSortedSetOfShouldReturnTheExpectedResult()
    {
        // Given
        ImmutableSortedSet<int> expected = ImmutableSortedSet.Create(123, 456, 789, 0, 1, -1, -987, 123, 456, 789, 0, 1, -1, -987);
        ImmutableSortedSet<int> actual = ImmutableSortedSetOf(123, 456, 789, 0, 1, -1, -987, 123, 456, 789, 0, 1, -1, -987);

        // Then
        Assert.True(expected.SequenceEqual(actual));
    }

    [Fact(DisplayName = "Collections.StackOf should return the expected result")]
    public void CollectionsStackOfShouldReturnTheExpectedResult()
    {
        // Given
        Stack<object> expected = new([true, false, 123, "abc", 1.23, true, false, 123, "abc", 1.23]);
        Stack<object> actual = StackOf<object>(true, false, 123, "abc", 1.23, true, false, 123, "abc", 1.23);

        // Then
        Assert.True(expected.SequenceEqual(actual));
    }

    [Fact(DisplayName = "Collections.ImmutableStackOf should return the expected result")]
    public void CollectionsImmutableStackOfShouldReturnTheExpectedResult()
    {
        // Given
        ImmutableStack<object> expected = ImmutableStack.Create<object>(true, false, 123, "abc", 1.23, true, false, 123, "abc", 1.23);
        ImmutableStack<object> actual = ImmutableStackOf<object>(true, false, 123, "abc", 1.23, true, false, 123, "abc", 1.23);

        // Then
        Assert.True(expected.SequenceEqual(actual));
    }

    [Fact(DisplayName = "Collections.QueueOf should return the expected result")]
    public void CollectionsQueueOfShouldReturnTheExpectedResult()
    {
        // Given
        Queue<object> expected = new([true, false, 123, "abc", 1.23, true, false, 123, "abc", 1.23]);
        Queue<object> actual = QueueOf<object>(true, false, 123, "abc", 1.23, true, false, 123, "abc", 1.23);

        // Then
        Assert.True(expected.SequenceEqual(actual));
    }

    [Fact(DisplayName = "Collections.ImmutableQueueOf should return the expected result")]
    public void CollectionsImmutableQueueOfShouldReturnTheExpectedResult()
    {
        // Given
        ImmutableQueue<object> expected = ImmutableQueue.Create<object>(true, false, 123, "abc", 1.23, true, false, 123, "abc", 1.23);
        ImmutableQueue<object> actual = ImmutableQueueOf<object>(true, false, 123, "abc", 1.23, true, false, 123, "abc", 1.23);

        // Then
        Assert.True(expected.SequenceEqual(actual));
    }
}
