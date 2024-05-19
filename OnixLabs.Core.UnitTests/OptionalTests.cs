// Copyright 2020-2024 ONIXLabs
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
using Xunit;

namespace OnixLabs.Core.UnitTests;

public sealed class OptionalTests
{
    [Fact(DisplayName = "Optional should have a present value.")]
    public void OptionalShouldHavePresentValue()
    {
        // Given
        Optional<int> optionalInt = 123;
        Optional<string> optionalString = "abc";
        Optional<Func<string>> optionalFunc = new(() => "abc");

        // Then
        bool optionalIntResult = optionalInt.HasValue;
        bool optionalStringResult = optionalString.HasValue;
        bool optionalFuncResult = optionalFunc.HasValue;

        // Then
        Assert.True(optionalIntResult);
        Assert.True(optionalStringResult);
        Assert.True(optionalFuncResult);
    }

    [Fact(DisplayName = "Optional of default should have no present value.")]
    public void OptionalOfDefaultShouldHaveNoPresentValue()
    {
        // Given
        Optional<int> optionalInt = default;
        Optional<string> optionalString = default;
        Optional<Func<string>> optionalFunc = default;

        // Then
        bool optionalIntResult = optionalInt.HasValue;
        bool optionalStringResult = optionalString.HasValue;
        bool optionalFuncResult = optionalFunc.HasValue;

        // Then
        Assert.False(optionalIntResult);
        Assert.False(optionalStringResult);
        Assert.False(optionalFuncResult);
    }

    [Fact(DisplayName = "Optional.None should have no present value.")]
    public void OptionalNoneShouldHaveNoPresentValue()
    {
        // Given
        Optional<int> optionalInt = Optional<int>.None;
        Optional<string> optionalString = Optional<string>.None;
        Optional<Func<string>> optionalFunc = Optional<Func<string>>.None;

        // Then
        bool optionalIntResult = optionalInt.HasValue;
        bool optionalStringResult = optionalString.HasValue;
        bool optionalFuncResult = optionalFunc.HasValue;

        // Then
        Assert.False(optionalIntResult);
        Assert.False(optionalStringResult);
        Assert.False(optionalFuncResult);
    }

    [Fact(DisplayName = "Optional.Value should return a present value.")]
    public void OptionalValueShouldReturnPresentValue()
    {
        // Given
        Optional<int> optionalInt = 123;
        Optional<string> optionalString = "abc";

        // When
        int intValue = optionalInt.Value;
        string stringValue = optionalString.Value;

        // Then
        Assert.Equal(123, intValue);
        Assert.Equal("abc", stringValue);
    }

    [Fact(DisplayName = "Optional.Value should throw InvalidOperationException if there is no present value.")]
    public void OptionalValueShouldThrowInvalidOperationExceptionIfThereIsNoPresentValue()
    {
        // Given
        Optional<int> optionalInt = default;
        Optional<string> optionalString = default;

        // When
        InvalidOperationException optionalIntException = Assert.Throws<InvalidOperationException>(() => optionalInt.Value);
        InvalidOperationException optionalStringException = Assert.Throws<InvalidOperationException>(() => optionalString.Value);

        // Then
        Assert.Equal("Optional value of type System.Int32 is not present.", optionalIntException.Message);
        Assert.Equal("Optional value of type System.String is not present.", optionalStringException.Message);
    }

    [Fact(DisplayName = "Optional.Some should have a present value.")]
    public void OptionalSomeShouldHavePresentValue()
    {
        // Given
        Optional<int> optionalInt = Optional<int>.Some(123);
        Optional<string> optionalString = Optional<string>.Some("abc");
        Optional<Func<string>> optionalFunc = Optional<Func<string>>.Some(() => "abc");

        // Then
        bool optionalIntResult = optionalInt.HasValue;
        bool optionalStringResult = optionalString.HasValue;
        bool optionalFuncResult = optionalFunc.HasValue;

        // Then
        Assert.True(optionalIntResult);
        Assert.True(optionalStringResult);
        Assert.True(optionalFuncResult);
    }

    [Fact(DisplayName = "Optional.GetValueOrThrow should return a present value.")]
    public void OptionalGetValueOrThrowShouldReturnPresentValue()
    {
        // Given
        Optional<int> optionalInt = 123;
        Optional<string> optionalString = "abc";

        // When
        int intValue = optionalInt.GetValueOrThrow();
        string stringValue = optionalString.GetValueOrThrow();

        // Then
        Assert.Equal(123, intValue);
        Assert.Equal("abc", stringValue);
    }

    [Fact(DisplayName = "Optional.GetValueOrThrow should throw InvalidOperationException if there is no present value.")]
    public void OptionalGetValueOrThrowShouldThrowInvalidOperationExceptionIfThereIsNoPresentValue()
    {
        // Given
        Optional<int> optionalInt = default;
        Optional<string> optionalString = default;

        // When
        InvalidOperationException optionalIntException = Assert.Throws<InvalidOperationException>(() => optionalInt.GetValueOrThrow());
        InvalidOperationException optionalStringException = Assert.Throws<InvalidOperationException>(() => optionalString.GetValueOrThrow());

        // Then
        Assert.Equal("Optional value of type System.Int32 is not present.", optionalIntException.Message);
        Assert.Equal("Optional value of type System.String is not present.", optionalStringException.Message);
    }

    [Fact(DisplayName = "Optional.GetValueOrDefault should return a present value.")]
    public void OptionalGetValueOrDefaultShouldReturnPresentValue()
    {
        // Given
        Optional<int> optionalInt = 123;
        Optional<string> optionalString = "abc";

        // When
        int intValue = optionalInt.GetValueOrDefault(456);
        string stringValue = optionalString.GetValueOrDefault("xyz");

        // Then
        Assert.Equal(123, intValue);
        Assert.Equal("abc", stringValue);
    }

    [Fact(DisplayName = "Optional.GetValueOrDefault should return a default value.")]
    public void OptionalGetValueOrDefaultShouldReturnDefaultValue()
    {
        // Given
        Optional<int> optionalInt = default;
        Optional<string> optionalString = default;

        // When
        int intValue = optionalInt.GetValueOrDefault(456);
        string stringValue = optionalString.GetValueOrDefault("xyz");

        // Then
        Assert.Equal(456, intValue);
        Assert.Equal("xyz", stringValue);
    }

    [Fact(DisplayName = "Optional.Bind should return the result of the bound function when a value is present.")]
    public void OptionalBindShouldReturnTheResultOfTheBoundFunctionWhenAValueIsPresent()
    {
        // Given
        const int expected = 9;
        Optional<int> value = 3;

        // When
        Optional<int> actual = value.Bind<int>(number => number * number);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Optional.Bind should return None when a value is not present.")]
    public void OptionalBindShouldReturnNoneWhenAValueIsNotPresent()
    {
        // Given
        Optional<int> expected = Optional<int>.None;
        Optional<int> value = Optional<int>.None;

        // When
        Optional<int> actual = value.Bind<int>(number => number * number);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Optional.Select should return the result of the bound function when a value is present.")]
    public void OptionalSelectShouldReturnTheResultOfTheBoundFunctionWhenAValueIsPresent()
    {
        // Given
        const int expected = 9;
        Optional<int> value = 3;

        // When
        Optional<int> actual = value.Select(number => number * number);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Optional.Select should return None when a value is not present.")]
    public void OptionalSelectShouldReturnNoneWhenAValueIsNotPresent()
    {
        // Given
        Optional<int> expected = Optional<int>.None;
        Optional<int> value = Optional<int>.None;

        // When
        Optional<int> actual = value.Select(number => number * number);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Optional.ToString should return a string representation of the value when a value is present.")]
    public void OptionalToStringShouldReturnStringRepresentationOfValueWhenPresent()
    {
        // Given
        const string expected = "1234.56789";
        Optional<decimal> value = 1234.56789m;

        // When
        string actual = value.ToString();

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Optional.ToString should return None when a value is not present.")]
    public void OptionalToStringShouldReturnNoneWhenNotPresent()
    {
        // Given
        const string expected = "None";
        Optional<decimal> value = Optional<decimal>.None;

        // When
        string actual = value.ToString();

        // Then
        Assert.Equal(expected, actual);
    }
}
