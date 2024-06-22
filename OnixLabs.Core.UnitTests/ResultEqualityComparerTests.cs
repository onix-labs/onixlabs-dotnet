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

public class ResultEqualityComparerTests
{
    [Fact(DisplayName = "ResultEqualityComparer.Equals should return true when comparing null values")]
    public void ResultEqualityComparerEqualsShouldReturnTrueWhenComparingNullValues()
    {
        // Given
        Result<string>? x = null;
        Result<string>? y = null;
        ResultEqualityComparer<string> comparer = new();

        // When
        bool result = comparer.Equals(x, y);

        // Then
        Assert.True(result);
    }

    [Fact(DisplayName = "ResultEqualityComparer.Equals should return false when comparing null and non-null values")]
    public void ResultEqualityComparerEqualsShouldReturnFalseWhenComparingNullAndNonNullValues()
    {
        // Given
        Result<string>? x = null;
        Result<string> y = "abc";
        ResultEqualityComparer<string> comparer = new();

        // When
        bool result1 = comparer.Equals(x, y);
        bool result2 = comparer.Equals(y, x);

        // Then
        Assert.False(result1);
        Assert.False(result2);
    }

    [Fact(DisplayName = "ResultEqualityComparer.Equals should return true when comparing Failure values")]
    public void ResultEqualityComparerEqualsShouldReturnTrueWhenComparingNoneValues()
    {
        // Given
        Exception exception = new("Failure");
        Result<string> x = exception;
        Result<string> y = exception;
        ResultEqualityComparer<string> comparer = new();

        // When
        bool result1 = comparer.Equals(x, y);
        bool result2 = comparer.Equals(y, x);

        // Then
        Assert.True(result1);
        Assert.True(result2);
    }

    [Fact(DisplayName = "ResultEqualityComparer.Equals should return false when comparing Success and Failure values")]
    public void ResultEqualityComparerEqualsShouldReturnFalseWhenComparingNoneAndSomeValues()
    {
        // Given
        Exception exception = new("Failure");
        Result<string> x = exception;
        Result<string> y = "abc";
        ResultEqualityComparer<string> comparer = new();

        // When
        bool result1 = comparer.Equals(x, y);
        bool result2 = comparer.Equals(y, x);

        // Then
        Assert.False(result1);
        Assert.False(result2);
    }

    [Fact(DisplayName = "ResultEqualityComparer.Equals should return true when comparing identical Success values")]
    public void ResultEqualityComparerEqualsShouldReturnTrueWhenComparingIdenticalSomeValues()
    {
        // Given
        Result<string> x = "abc";
        Result<string> y = "abc";
        ResultEqualityComparer<string> comparer = new();

        // When
        bool result1 = comparer.Equals(x, y);
        bool result2 = comparer.Equals(y, x);

        // Then
        Assert.True(result1);
        Assert.True(result2);
    }

    [Fact(DisplayName = "ResultEqualityComparer.Equals should return false when comparing non-identical Success values")]
    public void ResultEqualityComparerEqualsShouldReturnFalseWhenComparingNonIdenticalSomeValues()
    {
        // Given
        Result<string> x = "abc";
        Result<string> y = "xyz";
        ResultEqualityComparer<string> comparer = new();

        // When
        bool result1 = comparer.Equals(x, y);
        bool result2 = comparer.Equals(y, x);

        // Then
        Assert.False(result1);
        Assert.False(result2);
    }

    [Fact(DisplayName = "ResultEqualityComparer.Equals should return true when comparing identical Success values with value comparer")]
    public void ResultEqualityComparerEqualsShouldReturnTrueWhenComparingIdenticalSomeValuesWithValueComparer()
    {
        // Given
        Result<string> x = "abc";
        Result<string> y = "ABC";
        ResultEqualityComparer<string> comparer = new(new CaseInsensitiveStringComparer());

        // When
        bool result1 = comparer.Equals(x, y);
        bool result2 = comparer.Equals(y, x);

        // Then
        Assert.True(result1);
        Assert.True(result2);
    }

    [Fact(DisplayName = "ResultEqualityComparer.Equals should return false when comparing non-identical Success values with value comparer")]
    public void ResultEqualityComparerEqualsShouldReturnFalseWhenComparingNonIdenticalSomeValuesWithValueComparer()
    {
        // Given
        Result<string> x = "abc";
        Result<string> y = "XYZ";
        ResultEqualityComparer<string> comparer = new(new CaseInsensitiveStringComparer());

        // When
        bool result1 = comparer.Equals(x, y);
        bool result2 = comparer.Equals(y, x);

        // Then
        Assert.False(result1);
        Assert.False(result2);
    }

    [Fact(DisplayName = "ResultEqualityComparer.GetHashCode should produce the expected result")]
    public void ResultEqualityComparerEqualsShouldReturnExpectedResult()
    {
        // Given
        int expected = "abc".GetHashCode();
        Result<string> value = "abc";
        ResultEqualityComparer<string> comparer = new();

        // When
        int actual = comparer.GetHashCode(value);

        // Then
        Assert.Equal(expected, actual);
    }

    private sealed class CaseInsensitiveStringComparer : EqualityComparer<string>
    {
        public override bool Equals(string? x, string? y) => string.Equals(x, y, StringComparison.InvariantCultureIgnoreCase);
        public override int GetHashCode(string obj) => obj.GetHashCode();
    }
}
