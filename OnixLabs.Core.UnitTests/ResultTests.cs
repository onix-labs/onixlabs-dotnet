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

public sealed class ResultTests
{
    [Fact(DisplayName = "Result should have a value via constructor")]
    public void ResultShouldHaveValueViaConstructor()
    {
        // Given / When
        Result<int> number = new(123);
        Result<string> text = new("abc");
        Result<Func<Guid>> func = new(() => Guid.Empty);

        // Then
        Assert.True(number.HasValue);
        Assert.True(text.HasValue);
        Assert.True(func.HasValue);
        Assert.False(number.HasError);
        Assert.False(text.HasError);
        Assert.False(func.HasError);
        Assert.Equal(123, number);
        Assert.Equal("abc", text);
    }

    [Fact(DisplayName = "Result should have a value via FromValue")]
    public void ResultShouldHaveValueViaFromValue()
    {
        // Given / When
        Result<int> number = Result<int>.FromValue(123);
        Result<string> text = Result<string>.FromValue("abc");
        Result<Func<Guid>> func = Result<Func<Guid>>.FromValue(() => Guid.Empty);

        // Then
        Assert.True(number.HasValue);
        Assert.True(text.HasValue);
        Assert.True(func.HasValue);
        Assert.False(number.HasError);
        Assert.False(text.HasError);
        Assert.False(func.HasError);
        Assert.Equal(123, number);
        Assert.Equal("abc", text);
    }

    [Fact(DisplayName = "Result should have a value via implicit operator")]
    public void ResultShouldHaveValueViaImplicitOperator()
    {
        // Given / When
        Result<int> number = 123;
        Result<string> text = "abc";
        Result<Func<Guid>> func = (Func<Guid>)(() => Guid.Empty);

        // Then
        Assert.True(number.HasValue);
        Assert.True(text.HasValue);
        Assert.True(func.HasValue);
        Assert.False(number.HasError);
        Assert.False(text.HasError);
        Assert.False(func.HasError);
        Assert.Equal(123, number);
        Assert.Equal("abc", text);
    }

    [Fact(DisplayName = "Result should have an error via constructor")]
    public void ResultShouldHaveErrorViaConstructor()
    {
        // Given / When
        Exception error = new("Result error.");
        Result<int> number = new(error);
        Result<string> text = new(error);
        Result<Func<Guid>> func = new(error);

        // Then
        Assert.False(number.HasValue);
        Assert.False(text.HasValue);
        Assert.False(func.HasValue);
        Assert.True(number.HasError);
        Assert.True(text.HasError);
        Assert.True(func.HasError);
        Assert.Equal("Result error.", number.Error.Message);
        Assert.Equal("Result error.", text.Error.Message);
        Assert.Equal("Result error.", func.Error.Message);
    }

    [Fact(DisplayName = "Result should have an unknown error via constructor")]
    public void ResultShouldHaveUnknownErrorViaConstructor()
    {
        // Given / When
        Result<int> number = new();
        Result<string> text = new();
        Result<Func<Guid>> func = new();

        // Then
        Assert.False(number.HasValue);
        Assert.False(text.HasValue);
        Assert.False(func.HasValue);
        Assert.True(number.HasError);
        Assert.True(text.HasError);
        Assert.True(func.HasError);
        Assert.Equal("Unknown error.", number.Error.Message);
        Assert.Equal("Unknown error.", text.Error.Message);
        Assert.Equal("Unknown error.", func.Error.Message);
    }

    [Fact(DisplayName = "Result should have an error via FromError")]
    public void ResultShouldHaveErrorViaFromError()
    {
        // Given / When
        Exception error = new("Result error");
        Result<int> number = Result<int>.FromError(error);
        Result<string> text = Result<string>.FromError(error);
        Result<Func<Guid>> func = Result<Func<Guid>>.FromError(error);

        // Then
        Assert.False(number.HasValue);
        Assert.False(text.HasValue);
        Assert.False(func.HasValue);
        Assert.True(number.HasError);
        Assert.True(text.HasError);
        Assert.True(func.HasError);
        Assert.Equal("Result error", number.Error.Message);
        Assert.Equal("Result error", text.Error.Message);
        Assert.Equal("Result error", func.Error.Message);
    }

    [Fact(DisplayName = "Result should return the expected result via Value")]
    public void ResultShouldReturnExpectedResultViaValue()
    {
        // Given
        Result<int> number = new(123);
        Result<string> text = new("abc");

        // When
        int actualNumber = number.Value;
        string actualText = text.Value;

        // Then
        Assert.Equal(123, actualNumber);
        Assert.Equal("abc", actualText);
    }

    [Fact(DisplayName = "Result should return the expected result via GetValueOrThrow")]
    public void ResultShouldReturnExpectedResultViaGetValueOrThrow()
    {
        // Given
        Result<int> number = new(123);
        Result<string> text = new("abc");

        // When
        int actualNumber = number.GetValueOrThrow();
        string actualText = text.GetValueOrThrow();

        // Then
        Assert.Equal(123, actualNumber);
        Assert.Equal("abc", actualText);
    }

    [Fact(DisplayName = "Result should return the expected result via explicit operator")]
    public void ResultShouldReturnExpectedResultViaExplicitOperator()
    {
        // Given
        Result<int> number = new(123);
        Result<string> text = new("abc");

        // When
        int actualNumber = (int)number;
        string actualText = (string)text;

        // Then
        Assert.Equal(123, actualNumber);
        Assert.Equal("abc", actualText);
    }

    [Fact(DisplayName = "Result should throw exception via Value")]
    public void ResultShouldThrowInvalidOperationExceptionViaValue()
    {
        // Given
        Exception error = new("Result error.");
        Result<int> number = Result<int>.FromError(error);
        Result<string> text = Result<string>.FromError(error);

        // Then
        Exception numberException = Assert.Throws<Exception>(() => number.Value);
        Exception textException = Assert.Throws<Exception>(() => text.Value);

        // Then
        Assert.Equal("Result error.", numberException.Message);
        Assert.Equal("Result error.", textException.Message);
    }

    [Fact(DisplayName = "Result should throw exception via GetValueOrThrow")]
    public void ResultShouldThrowInvalidOperationExceptionViaGetValueOrThrow()
    {
        // Given
        Exception error = new("Result error.");
        Result<int> number = Result<int>.FromError(error);
        Result<string> text = Result<string>.FromError(error);

        // Then
        Exception numberException = Assert.Throws<Exception>(() => number.GetValueOrThrow());
        Exception textException = Assert.Throws<Exception>(() => text.GetValueOrThrow());

        // Then
        Assert.Equal("Result error.", numberException.Message);
        Assert.Equal("Result error.", textException.Message);
    }

    [Fact(DisplayName = "Result should throw exception via explicit operator")]
    public void ResultShouldThrowInvalidOperationExceptionViaExplicitOperator()
    {
        // Given
        Exception error = new("Result error.");
        Result<int> number = Result<int>.FromError(error);
        Result<string> text = Result<string>.FromError(error);

        // Then
        Exception numberException = Assert.Throws<Exception>(() => (int)number);
        Exception textException = Assert.Throws<Exception>(() => (string)text);

        // Then
        Assert.Equal("Result error.", numberException.Message);
        Assert.Equal("Result error.", textException.Message);
    }

    [Fact(DisplayName = "Result.GetValueOrDefault should return a present value.")]
    public void ResultGetValueOrDefaultShouldReturnPresentValue()
    {
        // Given
        Result<int> number = 123;
        Result<string> text = "abc";

        // When
        int actualNumber = number.GetValueOrDefault(456);
        string actualText = text.GetValueOrDefault("xyz");

        // Then
        Assert.Equal(123, actualNumber);
        Assert.Equal("abc", actualText);
    }

    [Fact(DisplayName = "Result.GetValueOrDefault should return a default value.")]
    public void ResultGetValueOrDefaultShouldReturnDefaultValue()
    {
        // Given
        Exception error = new("Result error.");
        Result<int> number = Result<int>.FromError(error);
        Result<string> text = Result<string>.FromError(error);

        // When
        int actualNumber = number.GetValueOrDefault(456);
        string actualText = text.GetValueOrDefault("xyz");

        // Then
        Assert.Equal(456, actualNumber);
        Assert.Equal("xyz", actualText);
    }

    [Fact(DisplayName = "Result.Match should execute the value action when a value is present.")]
    public void OptionalMatchShouldExecuteValueActionWhenValueIsPresent()
    {
        // Given
        bool result = false;
        Result<int> number = 3;

        // When
        number.Match(
            value: _ => { result = true; },
            error: _ => { }
        );

        // Then
        Assert.True(result);
    }

    [Fact(DisplayName = "Result.Match should execute the error action when a value is absent.")]
    public void ResultMatchShouldExecuteErrorActionWhenValueIsAbsent()
    {
        // Given
        bool result = false;
        Exception error = new("Result error.");
        Result<int> number = Result<int>.FromError(error);

        // When
        number.Match(
            value: _ => { },
            error: _ => { result = true; }
        );

        // Then
        Assert.True(result);
    }

    [Fact(DisplayName = "Result.Match should produce the expected result of the value function when a value is present.")]
    public void ResultMatchShouldProduceExpectedResultOfValueFunctionWhenValueIsPresent()
    {
        // Given
        const int expected = 9;
        Result<int> number = 3;

        // When
        int actual = number.Match(
            value: value => value * value,
            error: _ => 0
        );

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result.Match should produce the expected result of the error function when a value is present.")]
    public void ResultMatchShouldProduceExpectedResultOfErrorFunctionWhenValueIsPresent()
    {
        // Given
        const int expected = 0;
        Exception error = new("Result error.");
        Result<int> number = Result<int>.FromError(error);

        // When
        int actual = number.Match(
            value: value => value * value,
            error: _ => 0
        );

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result.Select should return the result of the selector function when a value is present.")]
    public void ResultSelectShouldReturnTheResultOfSelectorFunctionWhenAValueIsPresent()
    {
        // Given
        const int expected = 9;
        Result<int> value = 3;

        // When
        Result<int> actual = value.Select(number => number * number);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result.Select should return an errored result when a value is not present.")]
    public void ResultSelectShouldReturnErroredResultWhenValueIsNotPresent()
    {
        // Given
        Exception error = new("Result error.");
        Result<int> expected = Result<int>.FromError(error);
        Result<int> value = Result<int>.FromError(error);

        // When
        Result<int> actual = value.Select(number => number * number);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result.SelectMany should return the result of the selector function when a value is present.")]
    public void ResultSelectManyShouldReturnTheResultOfSelectorFunctionWhenAValueIsPresent()
    {
        // Given
        const int expected = 9;
        Result<int> value = 3;

        // When
        Result<int> actual = value.SelectMany<int>(number => number * number);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result.SelectMany should return an errored result when a value is not present.")]
    public void ResultSelectManyShouldReturnErroredResultWhenValueIsNotPresent()
    {
        // Given
        Exception error = new("Result error.");
        Result<int> expected = Result<int>.FromError(error);
        Result<int> value = Result<int>.FromError(error);

        // When
        Result<int> actual = value.SelectMany<int>(number => number * number);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result.Throw should throw the underlying exception when the result is in an errored state.")]
    public void ResultThrowShouldThrowUnderlyingExceptionWhenResultIsInErroredState()
    {
        // Given
        Exception error = new("Result error.");
        Result<int> value = Result<int>.FromError(error);

        // When
        Exception exception = Assert.Throws<Exception>(() => value.Throw());

        // Then
        Assert.Equal("Result error.", exception.Message);
    }

    [Fact(DisplayName = "Result.Throw should continue when a value is present.")]
    public void ResultThrowShouldContinueWhenValueIsPresent()
    {
        // Given
        Result<int> value = 3;

        // When / Then
        value.Throw();
    }

    [Fact(DisplayName = "Result.ToString should return a string representation of the value when a value is present.")]
    public void ResultToStringShouldReturnStringRepresentationOfValueWhenPresent()
    {
        // Given
        const string expected = "1234.56789";
        Result<decimal> value = 1234.56789m;

        // When
        string actual = value.ToString();

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result.ToString should return the underlying error message when the result is in an errored state.")]
    public void ResultToStringShouldReturnUnderlyingErrorMessageWhenResultIsInErroredState()
    {
        // Given
        Exception error = new("Result error.");
        Result<int> value = Result<int>.FromError(error);

        // When
        string actual = value.ToString();

        // Then
        Assert.Equal(error.ToString(), actual);
    }
}
