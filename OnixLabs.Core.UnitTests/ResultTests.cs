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
    [Fact(DisplayName = "Result.Of should produce expected success result")]
    public void ResultOfShouldProduceExpectedSuccessResult()
    {
        // Given / When
        Result<int> number = Result<int>.Of(() => 123);
        Result<string> text = Result<string>.Of(() => "abc");

        // Then
        Assert.True(number.IsSuccess);
        Assert.False(number.IsFailure);
        Assert.IsType<Success<int>>(number);
        Assert.Equal(123, number);

        Assert.True(text.IsSuccess);
        Assert.False(text.IsFailure);
        Assert.IsType<Success<string>>(text);
        Assert.Equal("abc", text);
    }

    [Fact(DisplayName = "Result.Of should produce expected failure result")]
    public void ResultOfShouldProduceExpectedFailureResult()
    {
        // Given / When
        Exception exception = new("failure");
        Result<int> number = Result<int>.Of(() => throw exception);
        Result<string> text = Result<string>.Of(() => throw exception);

        // Then
        Assert.False(number.IsSuccess);
        Assert.True(number.IsFailure);
        Assert.IsType<Failure<int>>(number);
        Assert.Equal("failure", (number as Failure<int>)!.Exception.Message);

        Assert.False(text.IsSuccess);
        Assert.True(text.IsFailure);
        Assert.IsType<Failure<string>>(text);
        Assert.Equal("failure", (text as Failure<string>)!.Exception.Message);
    }

    [Fact(DisplayName = "Result.Success should produce the expected result")]
    public void ResultSuccessShouldProduceExpectedResult()
    {
        // Given / When
        Result<int> number = Result<int>.Success(123);
        Result<string> text = Result<string>.Success("abc");

        // Then
        Assert.True(number.IsSuccess);
        Assert.False(number.IsFailure);
        Assert.IsType<Success<int>>(number);
        Assert.Equal(123, number);

        Assert.True(text.IsSuccess);
        Assert.False(text.IsFailure);
        Assert.IsType<Success<string>>(text);
        Assert.Equal("abc", text);
    }

    [Fact(DisplayName = "Result.Failure should produce the expected result")]
    public void ResultFailureShouldProduceExpectedResult()
    {
        // Given / When
        Exception exception = new("failure");
        Result<int> number = Result<int>.Failure(exception);
        Result<string> text = Result<string>.Failure(exception);

        // Then
        Assert.False(number.IsSuccess);
        Assert.True(number.IsFailure);
        Assert.IsType<Failure<int>>(number);
        Assert.Equal("failure", (number as Failure<int>)!.Exception.Message);

        Assert.False(text.IsSuccess);
        Assert.True(text.IsFailure);
        Assert.IsType<Failure<string>>(text);
        Assert.Equal("failure", (text as Failure<string>)!.Exception.Message);
    }

    [Fact(DisplayName = "Result implicit operator should produce the expected success result.")]
    public void ResultImplicitOperatorShouldProduceTheExpectedSuccessResult()
    {
        // Given / When
        Result<int> number = 123;
        Result<string> text = "abc";

        // Then
        Assert.True(number.IsSuccess);
        Assert.False(number.IsFailure);
        Assert.IsType<Success<int>>(number);
        Assert.Equal(123, number);

        Assert.True(text.IsSuccess);
        Assert.False(text.IsFailure);
        Assert.IsType<Success<string>>(text);
        Assert.Equal("abc", text);
    }

    [Fact(DisplayName = "Result implicit operator should produce the expected failure result.")]
    public void ResultImplicitOperatorShouldProduceTheExpectedFailureResult()
    {
        // Given / When
        Exception exception = new("failure");
        Result<object> result = exception;

        // Then
        Assert.False(result.IsSuccess);
        Assert.True(result.IsFailure);
        Assert.IsType<Failure<object>>(result);
        Assert.Equal("failure", (result as Failure<object>)!.Exception.Message);
    }

    [Fact(DisplayName = "Result Success explicit operator should produce the expected result.")]
    public void ResultSuccessExplicitOperatorShouldProduceTheExpectedResult()
    {
        // Given
        Result<int> number = Result<int>.Success(123);
        Result<string> text = Result<string>.Success("abc");

        // When
        int underlyingNumber = (int)number;
        string underlyingText = (string)text;

        // Then
        Assert.Equal(123, underlyingNumber);
        Assert.Equal("abc", underlyingText);
    }

    [Fact(DisplayName = "Result Failure explicit operator should produce the expected result.")]
    public void ResultFailureExplicitOperatorShouldProduceTheExpectedResult()
    {
        // Given
        Exception exception = new("failure");
        Result<int> number = Result<int>.Failure(exception);
        Result<string> text = Result<string>.Failure(exception);

        // When
        Exception numberException = Assert.Throws<Exception>(() => (int)number);
        Exception textException = Assert.Throws<Exception>(() => (string)text);

        // Then
        Assert.Equal("failure", numberException.Message);
        Assert.Equal("failure", textException.Message);
    }

    [Fact(DisplayName = "Result Success values should be considered equal.")]
    public void ResultSuccessValuesShouldBeConsideredEqual()
    {
        // Given
        Result<int> a = Result<int>.Success(123);
        Result<int> b = Result<int>.Success(123);

        // When / Then
        Assert.Equal(a, b);
        Assert.True(a == b);
        Assert.True(a.Equals(b));
    }

    [Fact(DisplayName = "Result Success values should not be considered equal.")]
    public void ResultSuccessValuesShouldNotBeConsideredEqual()
    {
        // Given
        Result<int> a = Result<int>.Success(123);
        Result<int> b = Result<int>.Success(456);

        // When / Then
        Assert.NotEqual(a, b);
        Assert.True(a != b);
        Assert.False(a.Equals(b));
    }

    [Fact(DisplayName = "Result Failure values should be considered equal.")]
    public void ResultFailureValuesShouldBeConsideredEqual()
    {
        // Given
        Exception exception = new("failure");
        Result<int> a = Result<int>.Failure(exception);
        Result<int> b = Result<int>.Failure(exception);

        // When / Then
        Assert.Equal(a, b);
        Assert.True(a == b);
        Assert.True(a.Equals(b));

        // Note that a and b are equal because they share references to the same exception.
    }

    [Fact(DisplayName = "Result Failure values should not be considered equal.")]
    public void ResultFailureValuesShouldNotBeConsideredEqual()
    {
        // Given
        Exception exception1 = new("failure a");
        Exception exception2 = new("failure b");
        Result<int> a = Result<int>.Failure(exception1);
        Result<int> b = Result<int>.Failure(exception2);

        // When / Then
        Assert.NotEqual(a, b);
        Assert.True(a != b);
        Assert.False(a.Equals(b));
    }

    [Fact(DisplayName = "Result Success and Failure values should not be considered equal.")]
    public void ResultSuccessAndFailureValuesShouldNotBeConsideredEqual()
    {
        // Given
        Exception exception = new("failure");
        Result<int> a = Result<int>.Success(123);
        Result<int> b = Result<int>.Failure(exception);

        // When / Then
        Assert.NotEqual(a, b);
        Assert.True(a != b);
        Assert.False(a.Equals(b));
    }

    [Fact(DisplayName = "Result Success.GetHashCode should produce the expected result.")]
    public void ResultSuccessGetHashCodeShouldProduceExpectedResult()
    {
        // Given
        int expected = 123.GetHashCode();
        Result<int> result = Result<int>.Success(123);

        // When
        int actual = result.GetHashCode();

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result Failure.GetHashCode should produce the expected result.")]
    public void ResultFailureGetHashCodeShouldProduceExpectedResult()
    {
        // Given
        Exception exception = new("failure");
        int expected = exception.GetHashCode();
        Result<int> result = Result<int>.Failure(exception);

        // When
        int actual = result.GetHashCode();

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result Success.GetValueOrDefault should produce the expected result.")]
    public void ResultSuccessGetValueOrDefaultShouldProduceExpectedResult()
    {
        // Given
        Result<int> number = Result<int>.Success(123);
        Result<string> text = Result<string>.Success("abc");

        // When
        int actualNumber = number.GetValueOrDefault();
        string? actualText = text.GetValueOrDefault();

        // Then
        Assert.Equal(123, actualNumber);
        Assert.Equal("abc", actualText);
    }

    [Fact(DisplayName = "Result Failure.GetValueOrDefault should produce the expected result.")]
    public void ResultFailureGetValueOrDefaultShouldProduceExpectedResult()
    {
        // Given
        Exception exception = new("failure");
        Result<int> number = Result<int>.Failure(exception);
        Result<string> text = Result<string>.Failure(exception);

        // When
        int actualNumber = number.GetValueOrDefault();
        string? actualText = text.GetValueOrDefault();

        // Then
        Assert.Equal(default, actualNumber);
        Assert.Equal(default, actualText);
    }

    [Fact(DisplayName = "Result Success.GetValueOrDefault with default value should produce the expected result.")]
    public void ResultSuccessGetValueOrDefaultWithDefaultValueShouldProduceExpectedResult()
    {
        // Given
        Result<int> number = Result<int>.Success(123);
        Result<string> text = Result<string>.Success("abc");

        // When
        int actualNumber = number.GetValueOrDefault(456);
        string? actualText = text.GetValueOrDefault("xyz");

        // Then
        Assert.Equal(123, actualNumber);
        Assert.Equal("abc", actualText);
    }

    [Fact(DisplayName = "Result Failure.GetValueOrDefault with default value should produce the expected result.")]
    public void ResultFailureGetValueOrDefaultWithDefaultValueShouldProduceExpectedResult()
    {
        // Given
        Exception exception = new("failure");
        Result<int> number = Result<int>.Failure(exception);
        Result<string> text = Result<string>.Failure(exception);

        // When
        int actualNumber = number.GetValueOrDefault(456);
        string? actualText = text.GetValueOrDefault("xyz");

        // Then
        Assert.Equal(456, actualNumber);
        Assert.Equal("xyz", actualText);
    }

    [Fact(DisplayName = "Result Success.GetValueOrThrow should produce the expected result.")]
    public void ResultSuccessGetValueOrThrowShouldProduceExpectedResult()
    {
        // Given
        Result<int> number = Result<int>.Success(123);
        Result<string> text = Result<string>.Success("abc");

        // When
        int underlyingNumber = number.GetValueOrThrow();
        string underlyingText = text.GetValueOrThrow();

        // Then
        Assert.Equal(123, underlyingNumber);
        Assert.Equal("abc", underlyingText);
    }

    [Fact(DisplayName = "Result Failure.GetValueOrThrow should produce the expected result.")]
    public void ResultFailureGetValueOrThrowShouldProduceExpectedResult()
    {
        // Given
        Exception exception = new("failure");
        Result<int> number = Result<int>.Failure(exception);
        Result<string> text = Result<string>.Failure(exception);

        // When
        Exception numberException = Assert.Throws<Exception>(() => number.GetValueOrThrow());
        Exception textException = Assert.Throws<Exception>(() => text.GetValueOrThrow());

        // Then
        Assert.Equal("failure", numberException.Message);
        Assert.Equal("failure", textException.Message);
    }

    [Fact(DisplayName = "Result Success.Match should execute the some action.")]
    public void ResultSuccessMatchShouldExecuteSuccessAction()
    {
        // Given
        bool someCalled = false;
        Result<int> result = 123;

        // When
        result.Match(success: _ => { someCalled = true; });

        // Then
        Assert.True(someCalled);
    }

    [Fact(DisplayName = "Result Failure.Match should execute the none action.")]
    public void ResultFailureMatchShouldExecuteFailureAction()
    {
        // Given
        bool noneCalled = false;
        Exception exception = new("failure");
        Result<int> result = Result<int>.Failure(exception);

        // When
        result.Match(failure: _ => { noneCalled = true; });

        // Then
        Assert.True(noneCalled);
    }

    [Fact(DisplayName = "Result Success.Match should produce the expected result.")]
    public void ResultSuccessMatchShouldProduceExpectedResult()
    {
        // Given
        const int expected = 9;
        Result<int> result = 3;

        // When
        int actual = result.Match(
            success: value => value * value,
            failure: _ => 0
        );

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result Failure.Match should produce the expected result.")]
    public void ResultFailureMatchShouldProduceExpectedResult()
    {
        // Given
        const int expected = 0;
        Exception exception = new("failure");
        Result<int> result = Result<int>.Failure(exception);

        // When
        int actual = result.Match(
            success: value => value * value,
            failure: _ => 0
        );

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result Success.Select should produce the expected result")]
    public void ResultSuccessSelectShouldProduceExpectedResult()
    {
        // Given
        Result expected = Result.Success();
        Result<int> result = 123;

        // When
        Result actual = result.Select(_ => { });

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result Failure.Select should produce the expected result")]
    public void ResultFailureSelectShouldProduceExpectedResult()
    {
        // Given
        Exception exception = new("Failure");
        Result expected = Result.Failure(exception);
        Result<int> result = Result<int>.Failure(exception);

        // When
        Result actual = result.Select(_ => { });

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result Success.Select<TResult> should produce the expected result")]
    public void ResultSuccessSelectTResultShouldProduceExpectedResult()
    {
        // Given
        Result<int> expected = 9;
        Result<int> result = 3;

        // When
        Result<int> actual = result.Select(x => x * x);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result Failure.Select<TResult> should produce the expected result")]
    public void ResultFailureSelectTResultShouldProduceExpectedResult()
    {
        // Given
        Exception exception = new("failure");
        Result<int> result = Result<int>.Failure(exception);

        // When
        Result<int> actual = result.Select(x => x * x);

        // Then
        Assert.Equal(Result<int>.Failure(exception), actual);
    }

    [Fact(DisplayName = "Result Success.SelectMany should produce the expected result")]
    public void ResultSuccessSelectManyShouldProduceExpectedResult()
    {
        // Given
        Result expected = Result.Success();
        Result<int> result = 3;

        // When
        Result actual = result.SelectMany(_ => Result.Success());

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result Failure.SelectMany should produce the expected result")]
    public void ResultFailureSelectManyShouldProduceExpectedResult()
    {
        // Given
        Exception exception = new("Failure");
        Result expected = Result.Failure(exception);
        Result<int> result = Result<int>.Failure(exception);

        // When
        Result actual = result.SelectMany(_ => Result.Success());

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result Success.SelectMany<TResult> should produce the expected result")]
    public void ResultSuccessSelectTResultManyShouldProduceExpectedResult()
    {
        // Given
        Result<int> expected = 9;
        Result<int> result = 3;

        // When
        Result<int> actual = result.SelectMany(x => Result<int>.Success(x * x));

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result Failure.SelectMany<TResult> should produce the expected result")]
    public void ResultFailureSelectTResultManyShouldProduceExpectedResult()
    {
        // Given
        Exception exception = new("failure");
        Result<int> result = Result<int>.Failure(exception);

        // When
        Result<int> actual = result.SelectMany(x => Result<int>.Success(x * x));

        // Then
        Assert.Equal(Result<int>.Failure(exception), actual);
    }

    [Fact(DisplayName = "Result Success.ToString should produce the expected result.")]
    public void ResultSuccessToStringShouldProduceExpectedResult()
    {
        // Given
        Result<int> number = 123;
        Result<string> text = "abc";

        // When
        string numberString = number.ToString();
        string textString = text.ToString();

        // Then
        Assert.Equal("123", numberString);
        Assert.Equal("abc", textString);
    }

    [Fact(DisplayName = "Result Failure.ToString should produce the expected result.")]
    public void ResultFailureToStringShouldProduceExpectedResult()
    {
        // Given
        Exception exception = new("failure");
        Result<int> number = Result<int>.Failure(exception);
        Result<string> text = Result<string>.Failure(exception);

        // When
        string numberString = number.ToString();
        string textString = text.ToString();

        // Then
        Assert.Equal("System.Exception: failure", numberString);
        Assert.Equal("System.Exception: failure", textString);
    }
}
