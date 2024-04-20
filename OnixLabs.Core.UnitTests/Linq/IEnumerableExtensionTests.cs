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
using System.Globalization;
using System.Linq;
using OnixLabs.Core.Linq;
using OnixLabs.Core.UnitTests.Data.Objects;
using Xunit;
using Record = OnixLabs.Core.UnitTests.Data.Objects.Record;

namespace OnixLabs.Core.UnitTests.Linq;

// ReSharper disable InconsistentNaming
public sealed class IEnumerableExtensionTests
{
    [Fact(DisplayName = "IEnumerable.AllEqualBy should return true when all items are equal by the same property")]
    public void AllEqualByShouldProduceExpectedResultTrue()
    {
        // Given
        Record element1 = new("abc", 123, DateTime.Now, Guid.NewGuid());
        Record element2 = new("abc", 123, DateTime.Now, Guid.NewGuid());
        Record element3 = new("abc", 123, DateTime.Now, Guid.NewGuid());
        IEnumerable<Record> elements = new[] { element1, element2, element3 };

        // When
        bool result = elements.AllEqualBy(element => element.Text);

        // Then
        Assert.True(result);
    }

    [Fact(DisplayName = "IEnumerable.AllEqualBy should return false when all items are not equal by the same property")]
    public void AllEqualByShouldProduceExpectedResultFalse()
    {
        // Given
        Record element1 = new("abc", 123, DateTime.Now, Guid.NewGuid());
        Record element2 = new("abc", 123, DateTime.Now, Guid.NewGuid());
        Record element3 = new("xyz", 123, DateTime.Now, Guid.NewGuid());
        IEnumerable<Record> elements = new[] { element1, element2, element3 };

        // When
        bool result = elements.AllEqualBy(element => element.Text);

        // Then
        Assert.False(result);
    }

    [Fact(DisplayName = "IEnumerable.AnyEqualBy should return true when any items are equal by the same property")]
    public void AnyEqualByShouldProduceExpectedResultTrue()
    {
        // Given
        Record element1 = new("abc", 123, DateTime.Now, Guid.NewGuid());
        Record element2 = new("abc", 123, DateTime.Now, Guid.NewGuid());
        Record element3 = new("xyz", 123, DateTime.Now, Guid.NewGuid());
        IEnumerable<Record> elements = new[] { element1, element2, element3 };

        // When
        bool result = elements.AnyEqualBy(element => element.Text);

        // Then
        Assert.True(result);
    }

    [Fact(DisplayName = "IEnumerable.AnyEqualBy should return false when any items are not equal by the same property")]
    public void AnyEqualByShouldProduceExpectedResultFalse()
    {
        // Given
        Record element1 = new("abc", 123, DateTime.Now, Guid.NewGuid());
        Record element2 = new("def", 123, DateTime.Now, Guid.NewGuid());
        Record element3 = new("xyz", 123, DateTime.Now, Guid.NewGuid());
        IEnumerable<Record> elements = new[] { element1, element2, element3 };

        // When
        bool result = elements.AnyEqualBy(element => element.Text);

        // Then
        Assert.False(result);
    }

    [Fact(DisplayName = "IEnumerable.CountNot should produce the expected result.")]
    public void CountNotShouldProduceExpectedResult()
    {
        // Given
        Record element1 = new("abc", 123, DateTime.Now, Guid.NewGuid());
        Record element2 = new("def", 123, DateTime.Now, Guid.NewGuid());
        Record element3 = new("xyz", 456, DateTime.Now, Guid.NewGuid());
        IEnumerable<Record> elements = new[] { element1, element2, element3 };
        const int expected = 2;

        // When
        int actual = elements.CountNot(element => element.Number == 456);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "IEnumerable.ForEach should iterate over every element in the enumerable")]
    public void ForEachShouldProduceExpectedResult()
    {
        // Given
        IEnumerable<Element> enumerable = new[] { new Element(), new Element(), new Element() };

        // When
        enumerable.ForEach(element => element.Called = true);

        // Then
        Assert.All(enumerable, element => Assert.True(element.Called));
    }

    [Fact(DisplayName = "IEnumerable.GetContentHashCode should produce equal hash codes")]
    public void GetContentHashCodeShouldProduceExpectedResultEqual()
    {
        // Given
        IEnumerable<Element> enumerable1 = new[] { new Element(1), new Element(2), new Element(3) };
        IEnumerable<Element> enumerable2 = new[] { new Element(1), new Element(2), new Element(3) };

        // When
        int hashCode1 = enumerable1.GetContentHashCode();
        int hashCode2 = enumerable2.GetContentHashCode();

        // Then
        Assert.Equal(hashCode1, hashCode2);
    }

    [Fact(DisplayName = "IEnumerable.GetContentHashCode should produce different hash codes")]
    public void GetContentHashCodeShouldProduceExpectedResultDifferent()
    {
        // Given
        IEnumerable<Element> enumerable1 = new[] { new Element(1), new Element(2), new Element(3) };
        IEnumerable<Element> enumerable2 = new[] { new Element(3), new Element(2), new Element(1) };

        // When
        int hashCode1 = enumerable1.GetContentHashCode();
        int hashCode2 = enumerable2.GetContentHashCode();

        // Then
        Assert.NotEqual(hashCode1, hashCode2);
    }

    [Fact(DisplayName = "IEnumerable.IsEmpty should return true when the enumerable is empty")]
    public void IsEmptyShouldProduceExpectedResultTrue()
    {
        // Given
        IEnumerable<Element> enumerable = Enumerable.Empty<Element>();

        // When
        bool result = enumerable.IsEmpty();

        // Then
        Assert.True(result);
    }

    [Fact(DisplayName = "IEnumerable.IsEmpty should return false when the enumerable is not empty")]
    public void IsEmptyShouldProduceExpectedResultFalse()
    {
        // Given
        IEnumerable<Element> enumerable = new[] { new Element() };

        // When
        bool result = enumerable.IsEmpty();

        // Then
        Assert.False(result);
    }

    [Fact(DisplayName = "IEnumerable.IsNotEmpty should return true when the enumerable is not empty")]
    public void IsNotEmptyShouldProduceExpectedResultTrue()
    {
        // Given
        IEnumerable<Element> enumerable = new[] { new Element() };

        // When
        bool result = enumerable.IsNotEmpty();

        // Then
        Assert.True(result);
    }

    [Fact(DisplayName = "IEnumerable.IsNotEmpty should return false when the enumerable is empty")]
    public void IsNotEmptyShouldProduceExpectedResultFalse()
    {
        // Given
        IEnumerable<Element> enumerable = Enumerable.Empty<Element>();

        // When
        bool result = enumerable.IsNotEmpty();

        // Then
        Assert.False(result);
    }

    [Fact(DisplayName = "IEnumerable.IsSingle should return true when the enumerable contains a single element")]
    public void IsSingleShouldProduceExpectedResultTrue()
    {
        // Given
        IEnumerable<Element> enumerable = new[] { new Element() };

        // When
        bool result = enumerable.IsSingle();

        // Then
        Assert.True(result);
    }

    [Fact(DisplayName = "IEnumerable.IsSingle should return false when the enumerable is empty")]
    public void IsSingleShouldProduceExpectedResultFalseWhenEmpty()
    {
        // Given
        IEnumerable<Element> enumerable = Enumerable.Empty<Element>();

        // When
        bool result = enumerable.IsSingle();

        // Then
        Assert.False(result);
    }

    [Fact(DisplayName = "IEnumerable.IsSingle should return false when the enumerable contains more than one element")]
    public void IsSingleShouldProduceExpectedResultFalseWhenMoreThanOneElement()
    {
        // Given
        IEnumerable<Element> enumerable = new[] { new Element(), new Element() };

        // When
        bool result = enumerable.IsSingle();

        // Then
        Assert.False(result);
    }

    [Fact(DisplayName = "IEnumerable.IsCountEven should return true when the enumerable contains an even number of elements")]
    public void IsCountEvenShouldProduceExpectedResultTrue()
    {
        // Given
        IEnumerable<Element> enumerable = new[] { new Element(), new Element() };

        // When
        bool result = enumerable.IsCountEven();

        // Then
        Assert.True(result);
    }

    [Fact(DisplayName = "IEnumerable.IsCountEven should return false when the enumerable contains an odd number of elements")]
    public void IsCountEvenShouldProduceExpectedResultFalse()
    {
        // Given
        IEnumerable<Element> enumerable = new[] { new Element() };

        // When
        bool result = enumerable.IsCountEven();

        // Then
        Assert.False(result);
    }

    [Fact(DisplayName = "IEnumerable.IsCountOdd should return true when the enumerable contains an odd number of elements")]
    public void IsCountOddShouldProduceExpectedResultTrue()
    {
        // Given
        IEnumerable<Element> enumerable = new[] { new Element() };

        // When
        bool result = enumerable.IsCountOdd();

        // Then
        Assert.True(result);
    }

    [Fact(DisplayName = "IEnumerable.IsCountOdd should return false when the enumerable contains an even number of elements")]
    public void IsCountOddShouldProduceExpectedResultFalse()
    {
        // Given
        IEnumerable<Element> enumerable = new[] { new Element(), new Element() };

        // When
        bool result = enumerable.IsCountOdd();

        // Then
        Assert.False(result);
    }

    [Fact(DisplayName = "IEnumerable.JoinToString should produce the expected result with the default separator")]
    public void JoinToStringShouldProduceExpectedResultWithDefaultSeparator()
    {
        // Given
        IEnumerable<object> enumerable = new object[] { 1, 2, 3, 4.5, true, false };
        const string expected = "1, 2, 3, 4.5, True, False";

        // When
        string actual = enumerable.JoinToString();

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "IEnumerable.JoinToString should produce the expected result with a custom separator")]
    public void JoinToStringShouldProduceExpectedResultWithCustomSeparator()
    {
        // Given
        IEnumerable<object> enumerable = new object[] { 1, 2, 3, 4.5, true, false };
        const string expected = "1 *$ 2 *$ 3 *$ 4.5 *$ True *$ False";

        // When
        string actual = enumerable.JoinToString(" *$ ");

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "IEnumerable.None should return true when none of the elements satisfy the specified predicate condition")]
    public void NoneShouldProduceExpectedResultTrue()
    {
        // Given
        Record element1 = new("abc", 123, DateTime.Now, Guid.NewGuid());
        Record element2 = new("def", 456, DateTime.Now, Guid.NewGuid());
        Record element3 = new("xyz", 789, DateTime.Now, Guid.NewGuid());
        IEnumerable<Record> elements = new[] { element1, element2, element3 };

        // When
        bool result = elements.None(element => element.Number == 0);

        // Then
        Assert.True(result);
    }

    [Fact(DisplayName = "IEnumerable.None should return false when any of the elements satisfy the specified predicate condition")]
    public void NoneShouldProduceExpectedResultFalseAny()
    {
        // Given
        Record element1 = new("abc", 123, DateTime.Now, Guid.NewGuid());
        Record element2 = new("def", 456, DateTime.Now, Guid.NewGuid());
        Record element3 = new("xyz", 0, DateTime.Now, Guid.NewGuid());
        IEnumerable<Record> elements = new[] { element1, element2, element3 };

        // When
        bool result = elements.None(element => element.Number == 0);

        // Then
        Assert.False(result);
    }

    [Fact(DisplayName = "IEnumerable.None should return false when all of the elements satisfy the specified predicate condition")]
    public void NoneShouldProduceExpectedResultFalseAll()
    {
        // Given
        Record element1 = new("abc", 0, DateTime.Now, Guid.NewGuid());
        Record element2 = new("def", 0, DateTime.Now, Guid.NewGuid());
        Record element3 = new("xyz", 0, DateTime.Now, Guid.NewGuid());
        IEnumerable<Record> elements = new[] { element1, element2, element3 };

        // When
        bool result = elements.None(element => element.Number == 0);

        // Then
        Assert.False(result);
    }

    [Fact(DisplayName = "IEnumerable.Sum should produce the expected result")]
    public void SumShouldProduceExpectedResult()
    {
        // Given
        IEnumerable<decimal> elements = [12.34m, 34.56m, 56.78m];
        const decimal expected = 103.68m;

        // When
        decimal actual = elements.Sum();

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "IEnumerable.Sum with selector should produce the expected result")]
    public void SumWithSelectorShouldProduceExpectedResult()
    {
        // Given
        Numeric<decimal> element1 = new(1234.567m);
        Numeric<decimal> element2 = new(890.1234m);
        Numeric<decimal> element3 = new(56.78901m);
        IEnumerable<Numeric<decimal>> elements = [element1, element2, element3];
        const decimal expected = 2181.47941m;

        // When
        decimal actual = elements.SumBy(element => element.Value);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "IEnumerable.WhereNot should produce the expected result")]
    public void WhereNotShouldProduceExpectedResult()
    {
        // Given
        Record element1 = new("abc", 123, DateTime.Now, Guid.NewGuid());
        Record element2 = new("def", 456, DateTime.Now, Guid.NewGuid());
        Record element3 = new("xyz", 789, DateTime.Now, Guid.NewGuid());
        IEnumerable<Record> elements = new[] { element1, element2, element3 };
        IEnumerable<Record> expected = new[] { element2, element3 };

        // When
        IEnumerable<Record> actual = elements.WhereNot(element => element.Number == 123);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "IEnumerable.WhereNotNull should produce the expected result")]
    public void WhereNotNullShouldProduceExpectedResult()
    {
        // Given
        Record element1 = new("abc", 123, DateTime.Now, Guid.NewGuid());
        Record element2 = new("def", 456, DateTime.Now, Guid.NewGuid());
        IEnumerable<Record> elements = new[] { element1, element2, null };
        IEnumerable<Record> expected = new[] { element1, element2 };

        // When
        IEnumerable<Record> actual = elements.WhereNotNull();

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "IEnumerable.ToCollectionString should produce expected result (Object)")]
    public void ToCollectionStringShouldProduceExpectedResultObject()
    {
        // Given
        object[] values = [123, "abc", true, 123.456];
        const string expected = "[123, abc, True, 123.456]";

        // When
        string actual = values.ToCollectionString();

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "IEnumerable.ToCollectionString should produce expected result (String)")]
    public void ToCollectionStringShouldProduceExpectedResultString()
    {
        // Given
        string[] values = ["abc", "xyz", "123"];
        const string expected = "[abc, xyz, 123]";

        // When
        string actual = values.ToCollectionString();

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "IEnumerable.ToCollectionString should produce expected result (Int32)")]
    public void ToCollectionStringShouldProduceExpectedResultInt32()
    {
        // Given
        int[] values = [0, 1, 12, 123, 1234, -1, -12, -123, -1234];
        const string expected = "[0, 1, 12, 123, 1234, -1, -12, -123, -1234]";

        // When
        string actual = values.ToCollectionString();

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "IEnumerable.ToCollectionString should produce expected result (Int32, IFormattable)")]
    public void ToCollectionStringShouldProduceExpectedResultInt32IFormattable()
    {
        // Given
        int[] values = [0, 1, 12, 123, 1234, -1, -12, -123, -1234];
        const string expected = "[£0.00, £1.00, £12.00, £123.00, £1,234.00, -£1.00, -£12.00, -£123.00, -£1,234.00]";

        // When
        string actual = values.ToCollectionString("C", CultureInfo.GetCultureInfo("en-GB"));

        // Then
        Assert.Equal(expected, actual);
    }
}
