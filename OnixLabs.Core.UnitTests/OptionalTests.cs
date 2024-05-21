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
using Xunit;

namespace OnixLabs.Core.UnitTests;

public sealed class OptionalTests
{
    [Fact(DisplayName = "Optional should have a present value via constructor")]
    public void OptionalShouldHavePresentValueViaConstructor()
    {
        // Given / When
        Optional<int> number = new(123);
        Optional<string> text = new("abc");
        Optional<Func<Guid>> func = new(() => Guid.Empty);

        // Then
        Assert.True(number.HasValue);
        Assert.True(text.HasValue);
        Assert.True(func.HasValue);
        Assert.Equal(123, number);
        Assert.Equal("abc", text);
    }

    [Fact(DisplayName = "Optional should have a present value via FromValue")]
    public void OptionalShouldHavePresentValueViaFromValue()
    {
        // Given / When
        Optional<int> number = Optional<int>.FromValue(123);
        Optional<string> text = Optional<string>.FromValue("abc");
        Optional<Func<Guid>> func = Optional<Func<Guid>>.FromValue(() => Guid.Empty);

        // Then
        Assert.True(number.HasValue);
        Assert.True(text.HasValue);
        Assert.True(func.HasValue);
        Assert.Equal(123, number);
        Assert.Equal("abc", text);
    }

    [Fact(DisplayName = "Optional should have a present value via implicit operator")]
    public void OptionalShouldHavePresentValueViaImplicitOperator()
    {
        // Given / When
        Optional<int> number = 123;
        Optional<string> text = "abc";
        Optional<Func<Guid>> func = (Func<Guid>)(() => Guid.Empty);

        // Then
        Assert.True(number.HasValue);
        Assert.True(text.HasValue);
        Assert.True(func.HasValue);
        Assert.Equal(123, number);
        Assert.Equal("abc", text);
    }

    [Fact(DisplayName = "Optional should not have a present value via constructor")]
    public void OptionalShouldNotHavePresentValueViaConstructor()
    {
        // Given / When
        Optional<int> number = new();
        Optional<string> text = new();
        Optional<Func<Guid>> func = new();

        // Then
        Assert.False(number.HasValue);
        Assert.False(text.HasValue);
        Assert.False(func.HasValue);
        Assert.Equal(Optional<int>.None, number);
        Assert.Equal(Optional<string>.None, text);
    }

    [Fact(DisplayName = "Optional should not have a present value via None")]
    public void OptionalShouldNotHavePresentValueViaNone()
    {
        // Given / When
        Optional<int> number = Optional<int>.None;
        Optional<string> text = Optional<string>.None;
        Optional<Func<Guid>> func = Optional<Func<Guid>>.None;

        // Then
        Assert.False(number.HasValue);
        Assert.False(text.HasValue);
        Assert.False(func.HasValue);
        Assert.Equal(Optional<int>.None, number);
        Assert.Equal(Optional<string>.None, text);
    }

    [Fact(DisplayName = "Optional should not have a present value via default")]
    public void OptionalShouldNotHavePresentValueViaDefault()
    {
        // Given / When
        Optional<int> number = default;
        Optional<string> text = default;
        Optional<Func<Guid>> func = default;

        // Then
        Assert.False(number.HasValue);
        Assert.False(text.HasValue);
        Assert.False(func.HasValue);
        Assert.Equal(Optional<int>.None, number);
        Assert.Equal(Optional<string>.None, text);
    }

    [Fact(DisplayName = "Optional should return the expected result via Value")]
    public void OptionalShouldReturnExpectedResultViaValue()
    {
        // Given
        Optional<int> number = new(123);
        Optional<string> text = new("abc");

        // When
        int actualNumber = number.Value;
        string actualText = text.Value;

        // Then
        Assert.Equal(123, actualNumber);
        Assert.Equal("abc", actualText);
    }

    [Fact(DisplayName = "Optional should return the expected result via GetValueOrThrow")]
    public void OptionalShouldReturnExpectedResultViaGetValueOrThrow()
    {
        // Given
        Optional<int> number = new(123);
        Optional<string> text = new("abc");

        // When
        int actualNumber = number.GetValueOrThrow();
        string actualText = text.GetValueOrThrow();

        // Then
        Assert.Equal(123, actualNumber);
        Assert.Equal("abc", actualText);
    }

    [Fact(DisplayName = "Optional should return the expected result via explicit operator")]
    public void OptionalShouldReturnExpectedResultViaExplicitOperator()
    {
        // Given
        Optional<int> number = new(123);
        Optional<string> text = new("abc");

        // When
        int actualNumber = (int)number;
        string actualText = (string)text;

        // Then
        Assert.Equal(123, actualNumber);
        Assert.Equal("abc", actualText);
    }

    [Fact(DisplayName = "Optional should throw InvalidOperationException via Value")]
    public void OptionalShouldThrowInvalidOperationExceptionViaValue()
    {
        // Given
        Optional<int> number = Optional<int>.None;
        Optional<string> text = Optional<string>.None;

        // Then
        Exception numberException = Assert.Throws<InvalidOperationException>(() => number.Value);
        Exception textException = Assert.Throws<InvalidOperationException>(() => text.Value);

        // Then
        Assert.Equal("Optional value of type System.Int32 is not present.", numberException.Message);
        Assert.Equal("Optional value of type System.String is not present.", textException.Message);
    }

    [Fact(DisplayName = "Optional should throw InvalidOperationException via GetValueOrThrow")]
    public void OptionalShouldThrowInvalidOperationExceptionViaGetValueOrThrow()
    {
        // Given
        Optional<int> number = Optional<int>.None;
        Optional<string> text = Optional<string>.None;

        // Then
        Exception numberException = Assert.Throws<InvalidOperationException>(() => number.GetValueOrThrow());
        Exception textException = Assert.Throws<InvalidOperationException>(() => text.GetValueOrThrow());

        // Then
        Assert.Equal("Optional value of type System.Int32 is not present.", numberException.Message);
        Assert.Equal("Optional value of type System.String is not present.", textException.Message);
    }

    [Fact(DisplayName = "Optional should throw InvalidOperationException via explicit operator")]
    public void OptionalShouldThrowInvalidOperationExceptionViaExplicitOperator()
    {
        // Given
        Optional<int> number = Optional<int>.None;
        Optional<string> text = Optional<string>.None;

        // Then
        Exception numberException = Assert.Throws<InvalidOperationException>(() => (int)number);
        Exception textException = Assert.Throws<InvalidOperationException>(() => (string)text);

        // Then
        Assert.Equal("Optional value of type System.Int32 is not present.", numberException.Message);
        Assert.Equal("Optional value of type System.String is not present.", textException.Message);
    }

    [Fact(DisplayName = "Optional.FromNullableOrDefaultValue should return None for implicit default values")]
    public void OptionalFromNullableOrDefaultValueShouldReturnNoneForImplicitDefaultValues()
    {
        // Given / When
        Optional<int> number = Optional<int>.FromNullableOrDefaultValue(default);
        Optional<string> text = Optional<string>.FromNullableOrDefaultValue(default);

        // Then
        Assert.False(number.HasValue);
        Assert.False(text.HasValue);
        Assert.Equal(Optional<int>.None, number);
        Assert.Equal(Optional<string>.None, text);
    }

    [Fact(DisplayName = "Optional.FromNullableOrDefaultValue should return None for explicit default values")]
    public void OptionalFromNullableOrDefaultValueShouldReturnNoneForExplicitDefaultValues()
    {
        // Given / When
        Optional<int> number = Optional<int>.FromNullableOrDefaultValue(0);
        Optional<string> text = Optional<string>.FromNullableOrDefaultValue(null);

        // Then
        Assert.False(number.HasValue);
        Assert.False(text.HasValue);
        Assert.Equal(Optional<int>.None, number);
        Assert.Equal(Optional<string>.None, text);
    }

    [Fact(DisplayName = "Optional.FromNullableOrDefaultValue should return the expected result for non-default values")]
    public void OptionalFromNullableOrDefaultValueShouldReturnExpectedResultForNonDefaultValues()
    {
        // Given / When
        Optional<int> number = Optional<int>.FromNullableOrDefaultValue(123);
        Optional<string> text = Optional<string>.FromNullableOrDefaultValue("abc");

        // Then
        Assert.True(number.HasValue);
        Assert.True(text.HasValue);
        Assert.Equal(123, number);
        Assert.Equal("abc", text);
    }

    [Fact(DisplayName = "Optional.GetValueOrDefault should return a present value.")]
    public void OptionalGetValueOrDefaultShouldReturnPresentValue()
    {
        // Given
        Optional<int> number = 123;
        Optional<string> text = "abc";

        // When
        int actualNumber = number.GetValueOrDefault(456);
        string actualText = text.GetValueOrDefault("xyz");

        // Then
        Assert.Equal(123, actualNumber);
        Assert.Equal("abc", actualText);
    }

    [Fact(DisplayName = "Optional.GetValueOrDefault should return a default value.")]
    public void OptionalGetValueOrDefaultShouldReturnDefaultValue()
    {
        // Given
        Optional<int> number = Optional<int>.None;
        Optional<string> text = Optional<string>.None;

        // When
        int actualNumber = number.GetValueOrDefault(456);
        string actualText = text.GetValueOrDefault("xyz");

        // Then
        Assert.Equal(456, actualNumber);
        Assert.Equal("xyz", actualText);
    }

    [Fact(DisplayName = "Optional.Match should execute the some action when a value is present.")]
    public void OptionalMatchShouldExecuteSomeActionWhenValueIsPresent()
    {
        // Given
        bool result = false;
        Optional<int> number = 3;

        // When
        number.Match(
            some: _ => { result = true; },
            none: () => { }
        );

        // Then
        Assert.True(result);
    }

    [Fact(DisplayName = "Optional.Match should execute the none action when a value is absent.")]
    public void OptionalMatchShouldExecuteNoneActionWhenValueIsAbsent()
    {
        // Given
        bool result = false;
        Optional<int> number = Optional<int>.None;

        // When
        number.Match(
            some: _ => { },
            none: () => { result = true; }
        );

        // Then
        Assert.True(result);
    }

    [Fact(DisplayName = "Optional.Match should produce the expected result of the some function when a value is present.")]
    public void OptionalMatchShouldProduceExpectedResultOfSomeFunctionWhenValueIsPresent()
    {
        // Given
        const int expected = 9;
        Optional<int> number = 3;

        // When
        int actual = number.Match(
            some: value => value * value,
            none: () => 0
        );

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Optional.Match should produce the expected result of the none function when a value is absent.")]
    public void OptionalMatchShouldProduceExpectedResultOfNoneFunctionWhenValueIsAbsent()
    {
        // Given
        const int expected = 0;
        Optional<int> number = Optional<int>.None;

        // When
        int actual = number.Match(
            some: value => value * value,
            none: () => 0
        );

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Optional.Select should return the result of the selector function when a value is present.")]
    public void OptionalSelectShouldReturnTheResultOfSelectorFunctionWhenAValueIsPresent()
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

    [Fact(DisplayName = "Optional.SelectMany should return the result of the bound function when a value is present.")]
    public void OptionalSelectManyShouldReturnTheResultOfTheBoundFunctionWhenAValueIsPresent()
    {
        // Given
        const int expected = 9;
        Optional<int> value = 3;

        // When
        Optional<int> actual = value.SelectMany<int>(number => number * number);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Optional.SelectMany should return None when a value is not present.")]
    public void OptionalSelectManyShouldReturnNoneWhenAValueIsNotPresent()
    {
        // Given
        Optional<int> expected = Optional<int>.None;
        Optional<int> value = Optional<int>.None;

        // When
        Optional<int> actual = value.SelectMany<int>(number => number * number);

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
