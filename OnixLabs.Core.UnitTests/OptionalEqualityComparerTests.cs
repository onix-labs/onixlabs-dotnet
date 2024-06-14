// Copyright 2020 ONIXLabs
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
using Xunit;

namespace OnixLabs.Core.UnitTests;

public class OptionalEqualityComparerTests
{
    [Fact(DisplayName = "OptionalEqualityComparer.Equals should return true when comparing null values")]
    public void OptionalEqualityComparerEqualsShouldReturnTrueWhenComparingNullValues()
    {
        // Given
        Optional<string>? x = null;
        Optional<string>? y = null;
        OptionalEqualityComparer<string> comparer = new();

        // When
        bool result = comparer.Equals(x, y);

        // Then
        Assert.True(result);
    }

    [Fact(DisplayName = "OptionalEqualityComparer.Equals should return false when comparing null and non-null values")]
    public void OptionalEqualityComparerEqualsShouldReturnFalseWhenComparingNullAndNonNullValues()
    {
        // Given
        Optional<string>? x = null;
        Optional<string> y = "abc";
        OptionalEqualityComparer<string> comparer = new();

        // When
        bool result1 = comparer.Equals(x, y);
        bool result2 = comparer.Equals(y, x);

        // Then
        Assert.False(result1);
        Assert.False(result2);
    }

    [Fact(DisplayName = "OptionalEqualityComparer.Equals should return true when comparing None values")]
    public void OptionalEqualityComparerEqualsShouldReturnTrueWhenComparingNoneValues()
    {
        // Given
        Optional<string> x = Optional<string>.None;
        Optional<string> y = Optional<string>.None;
        OptionalEqualityComparer<string> comparer = new();

        // When
        bool result1 = comparer.Equals(x, y);
        bool result2 = comparer.Equals(y, x);

        // Then
        Assert.True(result1);
        Assert.True(result2);
    }

    [Fact(DisplayName = "OptionalEqualityComparer.Equals should return false when comparing None and Some values")]
    public void OptionalEqualityComparerEqualsShouldReturnFalseWhenComparingNoneAndSomeValues()
    {
        // Given
        Optional<string> x = Optional<string>.None;
        Optional<string> y = "abc";
        OptionalEqualityComparer<string> comparer = new();

        // When
        bool result1 = comparer.Equals(x, y);
        bool result2 = comparer.Equals(y, x);

        // Then
        Assert.False(result1);
        Assert.False(result2);
    }

    [Fact(DisplayName = "OptionalEqualityComparer.Equals should return true when comparing identical Some values")]
    public void OptionalEqualityComparerEqualsShouldReturnTrueWhenComparingIdenticalSomeValues()
    {
        // Given
        Optional<string> x = "abc";
        Optional<string> y = "abc";
        OptionalEqualityComparer<string> comparer = new();

        // When
        bool result1 = comparer.Equals(x, y);
        bool result2 = comparer.Equals(y, x);

        // Then
        Assert.True(result1);
        Assert.True(result2);
    }

    [Fact(DisplayName = "OptionalEqualityComparer.Equals should return false when comparing non-identical Some values")]
    public void OptionalEqualityComparerEqualsShouldReturnFalseWhenComparingNonIdenticalSomeValues()
    {
        // Given
        Optional<string> x = "abc";
        Optional<string> y = "xyz";
        OptionalEqualityComparer<string> comparer = new();

        // When
        bool result1 = comparer.Equals(x, y);
        bool result2 = comparer.Equals(y, x);

        // Then
        Assert.False(result1);
        Assert.False(result2);
    }

    [Fact(DisplayName = "OptionalEqualityComparer.Equals should return true when comparing identical Some values with value comparer")]
    public void OptionalEqualityComparerEqualsShouldReturnTrueWhenComparingIdenticalSomeValuesWithValueComparer()
    {
        // Given
        Optional<string> x = "abc";
        Optional<string> y = "ABC";
        OptionalEqualityComparer<string> comparer = new(new CaseInsensitiveStringComparer());

        // When
        bool result1 = comparer.Equals(x, y);
        bool result2 = comparer.Equals(y, x);

        // Then
        Assert.True(result1);
        Assert.True(result2);
    }

    [Fact(DisplayName = "OptionalEqualityComparer.Equals should return false when comparing non-identical Some values with value comparer")]
    public void OptionalEqualityComparerEqualsShouldReturnFalseWhenComparingNonIdenticalSomeValuesWithValueComparer()
    {
        // Given
        Optional<string> x = "abc";
        Optional<string> y = "XYZ";
        OptionalEqualityComparer<string> comparer = new(new CaseInsensitiveStringComparer());

        // When
        bool result1 = comparer.Equals(x, y);
        bool result2 = comparer.Equals(y, x);

        // Then
        Assert.False(result1);
        Assert.False(result2);
    }

    [Fact(DisplayName = "OptionalEqualityComparer.GetHashCode should return zero when the optional value is none.")]
    public void OptionalEqualityComparerGetHashCodeShouldReturnZeroWhenOptionalValueIsNone()
    {
        // Given
        const int expected = default;
        Optional<string> optional = Optional<string>.None;
        OptionalEqualityComparer<string> comparer = new();

        // When
        int actual = comparer.GetHashCode(optional);

        // Then
        Assert.Equal(expected, actual);
    }

    private sealed class CaseInsensitiveStringComparer : EqualityComparer<string>
    {
        public override bool Equals(string? x, string? y) => string.Equals(x, y, StringComparison.InvariantCultureIgnoreCase);
        public override int GetHashCode(string obj) => obj.GetHashCode();
    }
}
