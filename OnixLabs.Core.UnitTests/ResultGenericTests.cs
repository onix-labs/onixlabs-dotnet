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
using System.Threading;
using System.Threading.Tasks;
using OnixLabs.Core.UnitTests.Data;
using Xunit;

namespace OnixLabs.Core.UnitTests;

public sealed class ResultGenericTests
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

    [Fact(DisplayName = "Result.OfAsync should produce expected success result")]
    public async Task ResultOfAsyncShouldProduceExpectedSuccessResult()
    {
        // Given / When
        Result<int> number = await Result<int>.OfAsync(async () => await Task.FromResult(123));
        Result<string> text = await Result<string>.OfAsync(async () => await Task.FromResult("abc"));

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

    [Fact(DisplayName = "Result.OfAsync should produce expected failure result")]
    public async Task ResultOfAsyncShouldProduceExpectedFailureResult()
    {
        // Given / When
        Exception exception = new("failure");
        Result<int> number = await Result<int>.OfAsync(() => throw exception);
        Result<string> text = await Result<string>.OfAsync(() => throw exception);

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

    [Fact(DisplayName = "Result.OfAsync with cancellation token should produce expected success result")]
    public async Task ResultOfAsyncWithCancellationTokenShouldProduceExpectedSuccessResult()
    {
        // Given / When
        CancellationToken token = CancellationToken.None;
        Result<int> number = await Result<int>.OfAsync(async _ => await Task.FromResult(123), token);
        Result<string> text = await Result<string>.OfAsync(async _ => await Task.FromResult("abc"), token);

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

    [Fact(DisplayName = "Result.OfAsync with cancellation token should produce expected failure result")]
    public async Task ResultOfAsyncWithCancellationTokenShouldProduceExpectedFailureResult()
    {
        // Given / When
        Exception exception = new("failure");
        CancellationToken token = CancellationToken.None;
        Result<int> number = await Result<int>.OfAsync(_ => throw exception, token);
        Result<string> text = await Result<string>.OfAsync(_ => throw exception, token);

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
        Success<int> a = Result<int>.Success(123);
        Success<int> b = Result<int>.Success(123);

        // When / Then
        Assert.Equal(a, b);
        Assert.True(a.Equals(b));
        Assert.True(a == b);
        Assert.False(a != b);
    }

    [Fact(DisplayName = "Result Success values should not be considered equal.")]
    public void ResultSuccessValuesShouldNotBeConsideredEqual()
    {
        // Given
        Success<int> a = Result<int>.Success(123);
        Success<int> b = Result<int>.Success(456);

        // When / Then
        Assert.NotEqual(a, b);
        Assert.False(a.Equals(b));
        Assert.True(a != b);
        Assert.False(a == b);
    }

    [Fact(DisplayName = "Result Failure values should be considered equal.")]
    public void ResultFailureValuesShouldBeConsideredEqual()
    {
        // Given
        Exception exception = new("failure");
        Failure<int> a = Result<int>.Failure(exception);
        Failure<int> b = Result<int>.Failure(exception);

        // When / Then
        Assert.Equal(a, b);
        Assert.True(a.Equals(b));
        Assert.True(a == b);
        Assert.False(a != b);

        // Note that a and b are equal because they share references to the same exception.
    }

    [Fact(DisplayName = "Result Failure values should not be considered equal.")]
    public void ResultFailureValuesShouldNotBeConsideredEqual()
    {
        // Given
        Exception exception1 = new("failure a");
        Exception exception2 = new("failure b");
        Failure<int> a = Result<int>.Failure(exception1);
        Failure<int> b = Result<int>.Failure(exception2);

        // When / Then
        Assert.NotEqual(a, b);
        Assert.False(a.Equals(b));
        Assert.True(a != b);
        Assert.False(a == b);
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
        Assert.False(a.Equals(b));
        Assert.True(a != b);
        Assert.False(a == b);
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

    [Fact(DisplayName = "Result Success.GetExceptionOrDefault should produce the expected result.")]
    public void ResultSuccessGetExceptionOrDefaultShouldProduceExpectedResult()
    {
        // Given
        Result<int> result = Result<int>.Success(123);

        // When
        Exception? actual = result.GetExceptionOrDefault();

        // Then
        Assert.Null(actual);
    }

    [Fact(DisplayName = "Result Success.GetExceptionOrDefault with default value should produce the expected result.")]
    public void ResultSuccessGetExceptionOrDefaultWithDefaultValueShouldProduceExpectedResult()
    {
        // Given
        Exception expected = new("failure");
        Result<int> result = Result<int>.Success(123);

        // When
        Exception actual = result.GetExceptionOrDefault(expected);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result Success.GetExceptionOrThrow should produce the expected result.")]
    public void ResultSuccessGetExceptionOrThrowShouldProduceExpectedResult()
    {
        // Given
        Result<int> result = Result<int>.Success(123);

        // When
        Exception exception = Assert.Throws<InvalidOperationException>(() => result.GetExceptionOrThrow());

        // Then
        Assert.Equal("The current result is not in a Failure<T> state.", exception.Message);
    }

    [Fact(DisplayName = "Result Failure.GetExceptionOrDefault should produce the expected result.")]
    public void ResultFailureGetExceptionOrDefaultShouldProduceExpectedResult()
    {
        // Given
        Exception expected = new("failure");
        Result<int> result = Result<int>.Failure(expected);

        // When
        Exception? actual = result.GetExceptionOrDefault();

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result Failure.GetExceptionOrDefault with default value should produce the expected result.")]
    public void ResultFailureGetExceptionOrDefaultWithDefaultValueShouldProduceExpectedResult()
    {
        // Given
        Exception expected = new("failure");
        Result<int> result = Result<int>.Failure(expected);

        // When
        Exception actual = result.GetExceptionOrDefault(new Exception("unexpected exception"));

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result Failure.GetExceptionOrThrow should produce the expected result.")]
    public void ResultFailureGetExceptionOrThrowShouldProduceExpectedResult()
    {
        // Given
        Exception expected = new("failure");
        Result<int> result = Result<int>.Failure(expected);

        // When
        Exception actual = result.GetExceptionOrThrow();

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
        bool successCalled = false;
        Result<int> result = 123;

        // When
        result.Match(success: _ => { successCalled = true; });

        // Then
        Assert.True(successCalled);
    }

    [Fact(DisplayName = "Result Failure.Match should execute the none action.")]
    public void ResultFailureMatchShouldExecuteFailureAction()
    {
        // Given
        bool failureCalled = false;
        Exception exception = new("failure");
        Result<int> result = Result<int>.Failure(exception);

        // When
        result.Match(failure: _ => { failureCalled = true; });

        // Then
        Assert.True(failureCalled);
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
    public void ResultSuccessSelectManyTResultShouldProduceExpectedResult()
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
    public void ResultFailureSelectManyTResultShouldProduceExpectedResult()
    {
        // Given
        Exception exception = new("failure");
        Result<int> result = Result<int>.Failure(exception);

        // When
        Result<int> actual = result.SelectMany(x => Result<int>.Success(x * x));

        // Then
        Assert.Equal(Result<int>.Failure(exception), actual);
    }

    [Fact(DisplayName = "Result Success.Throw should do nothing")]
    public void ResultSuccessThrowShouldDoNothing()
    {
        // Given
        Result<int> result = Result<int>.Success(123);

        // When / Then
        result.Throw();
    }

    [Fact(DisplayName = "Result Failure.Throw should throw Exception")]
    public void ResultFailureThrowShouldThrowException()
    {
        // Given
        Exception exception = new("failure");
        Result<int> result = Result<int>.Failure(exception);

        // When / Then
        Assert.Throws<Exception>(() => result.Throw());
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

    [Fact(DisplayName = "Result Failure.ToTypedResult should produce the expected result.")]
    public void ResultFailureToTypedResultShouldProduceExpectedResult()
    {
        // Given
        Exception exception = new("failure");
        Failure<string> result = Result<string>.Failure(exception);

        // When
        Result<int> actual = result.ToTypedResult<int>();

        // Then
        Assert.IsType<Failure<int>>(actual);
        Assert.Equal("System.Exception: failure", actual.GetExceptionOrThrow().ToString());
    }

    [Fact(DisplayName = "Result Failure.ToUntypedResult should produce the expected result.")]
    public void ResultFailureToUntypedResultShouldProduceExpectedResult()
    {
        // Given
        Exception exception = new("failure");
        Failure<string> result = Result<string>.Failure(exception);

        // When
        Result actual = result.ToUntypedResult();

        // Then
        Assert.IsType<Failure>(actual);
        Assert.Equal("System.Exception: failure", actual.GetExceptionOrThrow().ToString());
    }

    [Fact(DisplayName = "Result Success.Dispose should dispose of the underlying value.")]
    public void ResultSuccessDisposeShouldDisposeUnderlyingValue()
    {
        // Given
        Disposable disposable = new();
        Success<Disposable> result = Result<Disposable>.Success(disposable);

        // When
        result.Dispose();

        // Then
        Assert.True(disposable.IsDisposed);
    }

    [Fact(DisplayName = "Result Failure.Dispose should do nothing.")]
    public void ResultFailureDisposeShouldDoNothing()
    {
        // Given
        Disposable disposable = new();
        Exception exception = new("failure");
        Failure<Disposable> result = Result<Disposable>.Failure(exception);

        // When
        result.Dispose();

        // Then
        Assert.False(disposable.IsDisposed);
    }

    [Fact(DisplayName = "Result Success.DisposeAsync should dispose of the underlying value.")]
    public async Task ResultSuccessDisposeAsyncShouldDisposeUnderlyingValue()
    {
        // Given
        Disposable disposable = new();
        Success<Disposable> result = Result<Disposable>.Success(disposable);

        // When
        await result.DisposeAsync();

        // Then
        Assert.True(disposable.IsDisposed);
    }

    [Fact(DisplayName = "Result Failure.DisposeAsync should do nothing.")]
    public async Task ResultFailureDisposeAsyncShouldDoNothing()
    {
        // Given
        Disposable disposable = new();
        Exception exception = new("failure");
        Failure<Disposable> result = Result<Disposable>.Failure(exception);

        // When
        await result.DisposeAsync();

        // Then
        Assert.False(disposable.IsDisposed);
    }
}
