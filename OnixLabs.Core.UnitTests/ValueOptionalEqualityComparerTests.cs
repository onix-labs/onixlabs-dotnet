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

namespace OnixLabs.Core.UnitTests;

public class ValueOptionalEqualityComparerTests
{
    [Fact(DisplayName = "ValueOptionalEqualityComparer.Equals should return true when comparing None values")]
    public void ValueOptionalEqualityComparerEqualsShouldReturnTrueWhenComparingNoneValues()
    {
        // Given
        ValueOptional<string> x = ValueOptional<string>.None;
        ValueOptional<string> y = ValueOptional<string>.None;
        ValueOptionalEqualityComparer<string> comparer = new();

        // When
        bool result1 = comparer.Equals(x, y);
        bool result2 = comparer.Equals(y, x);

        // Then
        Assert.True(result1);
        Assert.True(result2);
    }

    [Fact(DisplayName = "ValueOptionalEqualityComparer.Equals should return false when comparing None and Some values")]
    public void ValueOptionalEqualityComparerEqualsShouldReturnFalseWhenComparingNoneAndSomeValues()
    {
        // Given
        ValueOptional<string> x = ValueOptional<string>.None;
        ValueOptional<string> y = "abc";
        ValueOptionalEqualityComparer<string> comparer = new();

        // When
        bool result1 = comparer.Equals(x, y);
        bool result2 = comparer.Equals(y, x);

        // Then
        Assert.False(result1);
        Assert.False(result2);
    }

    [Fact(DisplayName = "ValueOptionalEqualityComparer.Equals should return true when comparing identical Some values")]
    public void ValueOptionalEqualityComparerEqualsShouldReturnTrueWhenComparingIdenticalSomeValues()
    {
        // Given
        ValueOptional<string> x = "abc";
        ValueOptional<string> y = "abc";
        ValueOptionalEqualityComparer<string> comparer = new();

        // When
        bool result1 = comparer.Equals(x, y);
        bool result2 = comparer.Equals(y, x);

        // Then
        Assert.True(result1);
        Assert.True(result2);
    }

    [Fact(DisplayName = "ValueOptionalEqualityComparer.Equals should return false when comparing non-identical Some values")]
    public void ValueOptionalEqualityComparerEqualsShouldReturnFalseWhenComparingNonIdenticalSomeValues()
    {
        // Given
        ValueOptional<string> x = "abc";
        ValueOptional<string> y = "xyz";
        ValueOptionalEqualityComparer<string> comparer = new();

        // When
        bool result1 = comparer.Equals(x, y);
        bool result2 = comparer.Equals(y, x);

        // Then
        Assert.False(result1);
        Assert.False(result2);
    }

    [Fact(DisplayName = "ValueOptionalEqualityComparer.Equals should return true when comparing identical Some values with value comparer")]
    public void ValueOptionalEqualityComparerEqualsShouldReturnTrueWhenComparingIdenticalSomeValuesWithValueComparer()
    {
        // Given
        ValueOptional<string> x = "abc";
        ValueOptional<string> y = "ABC";
        ValueOptionalEqualityComparer<string> comparer = new(new CaseInsensitiveStringComparer());

        // When
        bool result1 = comparer.Equals(x, y);
        bool result2 = comparer.Equals(y, x);

        // Then
        Assert.True(result1);
        Assert.True(result2);
    }

    [Fact(DisplayName = "ValueOptionalEqualityComparer.Equals should return false when comparing non-identical Some values with value comparer")]
    public void ValueOptionalEqualityComparerEqualsShouldReturnFalseWhenComparingNonIdenticalSomeValuesWithValueComparer()
    {
        // Given
        ValueOptional<string> x = "abc";
        ValueOptional<string> y = "XYZ";
        ValueOptionalEqualityComparer<string> comparer = new(new CaseInsensitiveStringComparer());

        // When
        bool result1 = comparer.Equals(x, y);
        bool result2 = comparer.Equals(y, x);

        // Then
        Assert.False(result1);
        Assert.False(result2);
    }

    [Fact(DisplayName = "ValueOptionalEqualityComparer.GetHashCode should return zero when the optional value is none.")]
    public void ValueOptionalEqualityComparerGetHashCodeShouldReturnZeroWhenOptionalValueIsNone()
    {
        // Given
        const int expected = 0;
        ValueOptional<string> optional = ValueOptional<string>.None;
        ValueOptionalEqualityComparer<string> comparer = new();

        // When
        int actual = comparer.GetHashCode(optional);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueOptionalEqualityComparer.GetHashCode should use the supplied custom comparer when comparing Some values")]
    public void ValueOptionalEqualityComparerGetHashCodeShouldUseSuppliedCustomComparerWhenComparingSomeValues()
    {
        // Given
        ValueOptional<string> upper = "HELLO";
        ValueOptional<string> lower = "hello";
        ValueOptionalEqualityComparer<string> comparer = new(new CaseInsensitiveStringComparer());

        // When
        int upperHash = comparer.GetHashCode(upper);
        int lowerHash = comparer.GetHashCode(lower);

        // Then
        Assert.True(comparer.Equals(upper, lower));
        Assert.Equal(upperHash, lowerHash);
    }

    private sealed class CaseInsensitiveStringComparer : EqualityComparer<string>
    {
        public override bool Equals(string? x, string? y) => string.Equals(x, y, StringComparison.InvariantCultureIgnoreCase);
        public override int GetHashCode(string obj) => StringComparer.InvariantCultureIgnoreCase.GetHashCode(obj);
    }
}
