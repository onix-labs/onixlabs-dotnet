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

public class ValueResultEqualityComparerTests
{
    [Fact(DisplayName = "ValueResultEqualityComparer.Equals should return true when comparing Failure values")]
    public void ValueResultEqualityComparerEqualsShouldReturnTrueWhenComparingNoneValues()
    {
        // Given
        Exception exception = new("Failure");
        ValueResult<string> x = exception;
        ValueResult<string> y = exception;
        ValueResultEqualityComparer<string> comparer = new();

        // When
        bool result1 = comparer.Equals(x, y);
        bool result2 = comparer.Equals(y, x);

        // Then
        Assert.True(result1);
        Assert.True(result2);
    }

    [Fact(DisplayName = "ValueResultEqualityComparer.Equals should return false when comparing Success and Failure values")]
    public void ValueResultEqualityComparerEqualsShouldReturnFalseWhenComparingNoneAndSomeValues()
    {
        // Given
        Exception exception = new("Failure");
        ValueResult<string> x = exception;
        ValueResult<string> y = "abc";
        ValueResultEqualityComparer<string> comparer = new();

        // When
        bool result1 = comparer.Equals(x, y);
        bool result2 = comparer.Equals(y, x);

        // Then
        Assert.False(result1);
        Assert.False(result2);
    }

    [Fact(DisplayName = "ValueResultEqualityComparer.Equals should return false when comparing Success(default) and Failure for value types")]
    public void ValueResultEqualityComparerEqualsShouldReturnFalseWhenComparingSuccessDefaultAndFailureForValueTypes()
    {
        // Given
        Exception exception = new("Failure");
        ValueResult<int> success = 0;
        ValueResult<int> failure = exception;
        ValueResultEqualityComparer<int> comparer = new();

        // When
        bool result1 = comparer.Equals(success, failure);
        bool result2 = comparer.Equals(failure, success);

        // Then
        Assert.False(result1);
        Assert.False(result2);
    }

    [Fact(DisplayName = "ValueResultEqualityComparer.Equals should return false when comparing Success(default) and Failure for booleans")]
    public void ValueResultEqualityComparerEqualsShouldReturnFalseWhenComparingSuccessDefaultAndFailureForBooleans()
    {
        // Given
        Exception exception = new("Failure");
        ValueResult<bool> success = false;
        ValueResult<bool> failure = exception;
        ValueResultEqualityComparer<bool> comparer = new();

        // When
        bool result1 = comparer.Equals(success, failure);
        bool result2 = comparer.Equals(failure, success);

        // Then
        Assert.False(result1);
        Assert.False(result2);
    }

    [Fact(DisplayName = "ValueResultEqualityComparer.Equals should return true when comparing identical Success values")]
    public void ValueResultEqualityComparerEqualsShouldReturnTrueWhenComparingIdenticalSomeValues()
    {
        // Given
        ValueResult<string> x = "abc";
        ValueResult<string> y = "abc";
        ValueResultEqualityComparer<string> comparer = new();

        // When
        bool result1 = comparer.Equals(x, y);
        bool result2 = comparer.Equals(y, x);

        // Then
        Assert.True(result1);
        Assert.True(result2);
    }

    [Fact(DisplayName = "ValueResultEqualityComparer.Equals should return false when comparing non-identical Success values")]
    public void ValueResultEqualityComparerEqualsShouldReturnFalseWhenComparingNonIdenticalSomeValues()
    {
        // Given
        ValueResult<string> x = "abc";
        ValueResult<string> y = "xyz";
        ValueResultEqualityComparer<string> comparer = new();

        // When
        bool result1 = comparer.Equals(x, y);
        bool result2 = comparer.Equals(y, x);

        // Then
        Assert.False(result1);
        Assert.False(result2);
    }

    [Fact(DisplayName = "ValueResultEqualityComparer.Equals should return true when comparing identical Success values with value comparer")]
    public void ValueResultEqualityComparerEqualsShouldReturnTrueWhenComparingIdenticalSomeValuesWithValueComparer()
    {
        // Given
        ValueResult<string> x = "abc";
        ValueResult<string> y = "ABC";
        ValueResultEqualityComparer<string> comparer = new(new CaseInsensitiveStringComparer());

        // When
        bool result1 = comparer.Equals(x, y);
        bool result2 = comparer.Equals(y, x);

        // Then
        Assert.True(result1);
        Assert.True(result2);
    }

    [Fact(DisplayName = "ValueResultEqualityComparer.Equals should return false when comparing non-identical Success values with value comparer")]
    public void ValueResultEqualityComparerEqualsShouldReturnFalseWhenComparingNonIdenticalSomeValuesWithValueComparer()
    {
        // Given
        ValueResult<string> x = "abc";
        ValueResult<string> y = "XYZ";
        ValueResultEqualityComparer<string> comparer = new(new CaseInsensitiveStringComparer());

        // When
        bool result1 = comparer.Equals(x, y);
        bool result2 = comparer.Equals(y, x);

        // Then
        Assert.False(result1);
        Assert.False(result2);
    }

    [Fact(DisplayName = "ValueResultEqualityComparer.GetHashCode should produce the expected result")]
    public void ValueResultEqualityComparerEqualsShouldReturnExpectedResult()
    {
        // Given
        int expected = "abc".GetHashCode();
        ValueResult<string> value = "abc";
        ValueResultEqualityComparer<string> comparer = new();

        // When
        int actual = comparer.GetHashCode(value);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResultEqualityComparer.GetHashCode should use the supplied custom comparer when comparing Success values")]
    public void ValueResultEqualityComparerGetHashCodeShouldUseSuppliedCustomComparerWhenComparingSuccessValues()
    {
        // Given
        ValueResult<string> upper = "HELLO";
        ValueResult<string> lower = "hello";
        ValueResultEqualityComparer<string> comparer = new(new CaseInsensitiveStringComparer());

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
