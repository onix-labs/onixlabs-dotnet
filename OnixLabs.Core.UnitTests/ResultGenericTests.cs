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
    private static readonly Exception FailureException = new("Failure");

    [Fact(DisplayName = "Result.IsSuccess should produce the expected result")]
    public void ResultIsSuccessShouldProduceExpectedResult()
    {
        // Given
        Result<int> result = Result<int>.Success(1);

        // When / Then
        Assert.True(result.IsSuccess);
        Assert.False(result.IsFailure);
    }

    [Fact(DisplayName = "Result.IsFailure should produce the expected result")]
    public void ResultIsFailureShouldProduceExpectedResult()
    {
        // Given
        Result<int> result = Result<int>.Failure(FailureException);

        // When / Then
        Assert.True(result.IsFailure);
        Assert.False(result.IsSuccess);
    }

    [Fact(DisplayName = "Result.Of should produce the expected success result.")]
    public void ResultOfShouldProduceExpectedSuccessResult()
    {
        // Given / When
        Result<int> result = Result<int>.Of(() => 1);

        // Then
        Assert.IsType<Success<int>>(result);
    }

    [Fact(DisplayName = "Result.Of should produce the expected failure result.")]
    public void ResultOfShouldProduceExpectedFailureResult()
    {
        // Given / When
        Result<int> result = Result<int>.Of(() => throw FailureException);

        // Then
        Assert.IsType<Failure<int>>(result);
        Assert.Equal(FailureException, result.GetExceptionOrThrow());
    }

    [Fact(DisplayName = "Result.OfAsync should produce the expected success result.")]
    public async Task ResultOfAsyncShouldProduceExpectedSuccessResult()
    {
        // Given / When
        Result<int> result = await Result<int>.OfAsync(async () => await Task.FromResult(1));

        // Then
        Assert.IsType<Success<int>>(result);
    }

    [Fact(DisplayName = "Result.OfAsync should produce the expected failure result.")]
    public async Task ResultOfAsyncShouldProduceExpectedFailureResult()
    {
        // Given / When
        Result<int> result = await Result<int>.OfAsync(async () => await Task.FromException<int>(FailureException));

        // Then
        Assert.IsType<Failure<int>>(result);
        Assert.Equal(FailureException, result.GetExceptionOrThrow());
    }

    [Fact(DisplayName = "Result.OfAsync with cancellation token should produce the expected success result.")]
    public async Task ResultOfAsyncWithCancellationTokenShouldProduceExpectedSuccessResult()
    {
        // Given / When
        Result<int> result = await Result<int>.OfAsync(async () => await Task.FromResult(1), CancellationToken.None);

        // Then
        Assert.IsType<Success<int>>(result);
    }

    [Fact(DisplayName = "Result.OfAsync with cancellation token should produce the expected failure result.")]
    public async Task ResultOfAsyncWithCancellationTokenShouldProduceExpectedFailureResult()
    {
        // Given / When
        Result<int> result = await Result<int>.OfAsync(async () => await Task.FromException<int>(FailureException), CancellationToken.None);

        // Then
        Assert.IsType<Failure<int>>(result);
        Assert.Equal(FailureException, result.GetExceptionOrThrow());
    }

    [Fact(DisplayName = "Result.OfAsync with cancellable function should produce the expected success result.")]
    public async Task ResultOfAsyncWithCancellableFunctionShouldProduceExpectedSuccessResult()
    {
        // Given / When
        Result<int> result = await Result<int>.OfAsync(async _ => await Task.FromResult(1), CancellationToken.None);

        // Then
        Assert.IsType<Success<int>>(result);
    }

    [Fact(DisplayName = "Result.OfAsync with cancellable function should produce the expected failure result.")]
    public async Task ResultOfAsyncWithCancellableFunctionShouldProduceExpectedFailureResult()
    {
        // Given / When
        Result<int> result = await Result<int>.OfAsync(async _ => await Task.FromException<int>(FailureException), CancellationToken.None);

        // Then
        Assert.IsType<Failure<int>>(result);
        Assert.Equal(FailureException, result.GetExceptionOrThrow());
    }

    [Fact(DisplayName = "Result.Success should produce the expected success result.")]
    public void ResultSuccessShouldProduceExpectedSuccessResult()
    {
        // Given / When
        Result<int> result = Result<int>.Success(123);

        // Then
        Assert.IsType<Success<int>>(result);
    }

    [Fact(DisplayName = "Result.Failure should produce the expected failure result.")]
    public void ResultFailureShouldProduceExpectedFailureResult()
    {
        // Given / When
        Result<int> result = Result<int>.Failure(FailureException);

        // Then
        Assert.IsType<Failure<int>>(result);
        Assert.Equal(FailureException, result.GetExceptionOrThrow());
    }

    [Fact(DisplayName = "Result from implicit value should produce the expected success result.")]
    public void ResultFromImplicitValueShouldProduceExpectedSuccessResult()
    {
        // Given / When
        Result<int> result = 1;

        // Then
        Assert.IsType<Success<int>>(result);
        Assert.Equal(1, result.GetValueOrThrow());
    }

    [Fact(DisplayName = "Result from implicit exception should produce the expected failure result.")]
    public void ResultFromImplicitExceptionShouldProduceExpectedFailureResult()
    {
        // Given / When
        Result<int> result = FailureException;

        // Then
        Assert.IsType<Failure<int>>(result);
        Assert.Equal(FailureException, result.GetExceptionOrThrow());
    }

    [Fact(DisplayName = "Result to explicit value from success should produce the expected result.")]
    public void ResultToExplicitValueFromSuccessShouldProduceExpectedResult()
    {
        // Given
        Result<int> result = 1;
        const int expected = 1;

        // When
        int actual = (int)result;

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result to explicit value from failure should produce the expected result.")]
    public void ResultToExplicitValueFromFailureShouldProduceExpectedResult()
    {
        // Given
        Result<int> result = Result<int>.Failure(FailureException);

        // When
        Exception exception = Assert.Throws<Exception>(() => (int)result);

        // Then
        Assert.Equal(FailureException, exception);
    }

    [Fact(DisplayName = "Result equality should produce the expected result.")]
    public void ResultEqualityShouldProduceExpectedResult()
    {
        // ReSharper disable EqualExpressionComparison

        // Equals Method
        Assert.True(Result<int>.Success(1).Equals(Result<int>.Success(1)));
        Assert.True(Result<int>.Failure(FailureException).Equals(Result<int>.Failure(FailureException)));
        Assert.False(Result<int>.Success(1).Equals(Result<int>.Success(2)));
        Assert.False(Result<int>.Success(1).Equals(null));
        Assert.False(Result<int>.Failure(FailureException).Equals(null));

        // Equality Operator
        Assert.True(Result<int>.Success(1) == Result<int>.Success(1));
        Assert.True(Result<int>.Failure(FailureException) == Result<int>.Failure(FailureException));
        Assert.False(Result<int>.Success(1) == Result<int>.Success(2));
        Assert.False(Result<int>.Success(1) == Result<int>.Failure(FailureException));
        Assert.False(Result<int>.Success(1) == null);
        Assert.False(null == Result<int>.Success(1));

        // Inequality Operator
        Assert.True(Result<int>.Success(1) != Result<int>.Failure(FailureException));
        Assert.True(Result<int>.Failure(FailureException) != Result<int>.Failure(new Exception()));
        Assert.True(Result<int>.Success(1) != null);
        Assert.True(null != Result<int>.Success(1));

        // Hash Code Generation
        Assert.True(Result<int>.Success(1).GetHashCode() == Result<int>.Success(1).GetHashCode());
        Assert.True(Result<int>.Failure(FailureException).GetHashCode() == Result<int>.Failure(FailureException).GetHashCode());
    }

    [Fact(DisplayName = "Result.GetExceptionOrDefault from success should produce the expected result.")]
    public void ResultGetExceptionOrDefaultFromSuccessShouldProduceExpectedResult()
    {
        // Given
        Result<int> result = Result<int>.Success(1);

        // Then
        Exception? exception = result.GetExceptionOrDefault();

        // Then
        Assert.Null(exception);
    }

    [Fact(DisplayName = "Result.GetExceptionOrDefault from failure should produce the expected result.")]
    public void ResultGetExceptionOrDefaultFromFailureShouldProduceExpectedResult()
    {
        // Given
        Result<int> result = Result<int>.Failure(FailureException);

        // Then
        Exception? exception = result.GetExceptionOrDefault();

        // Then
        Assert.Equal(FailureException, exception);
    }

    [Fact(DisplayName = "Result.GetExceptionOrDefault from success with default value should produce the expected result.")]
    public void ResultGetExceptionOrDefaultFromSuccessWithDefaultValueShouldProduceExpectedResult()
    {
        // Given
        Result<int> result = Result<int>.Success(1);
        Exception defaultValue = new("Default");

        // Then
        Exception exception = result.GetExceptionOrDefault(defaultValue);

        // Then
        Assert.Equal(defaultValue, exception);
    }

    [Fact(DisplayName = "Result.GetExceptionOrDefault from failure with default value should produce the expected result.")]
    public void ResultGetExceptionOrDefaultFromFailureWithDefaultValueShouldProduceExpectedResult()
    {
        // Given
        Result<int> result = Result<int>.Failure(FailureException);
        Exception defaultValue = new("Default");

        // Then
        Exception exception = result.GetExceptionOrDefault(defaultValue);

        // Then
        Assert.Equal(FailureException, exception);
    }

    [Fact(DisplayName = "Result.GetExceptionOrThrow from success should produce the expected result.")]
    public void ResultGetExceptionOrThrowFromSuccessShouldProduceExpectedResult()
    {
        // Given
        Result<int> result = Result<int>.Success(1);

        // Then
        Exception exception = Assert.Throws<InvalidOperationException>(() => result.GetExceptionOrThrow());

        // Then
        Assert.Equal("The current result is not in a failure state.", exception.Message);
    }

    [Fact(DisplayName = "Result.GetExceptionOrThrow from failure should produce the expected result.")]
    public void ResultGetExceptionOrThrowFromFailureShouldProduceExpectedResult()
    {
        // Given
        Result<int> result = Result<int>.Failure(FailureException);

        // Then
        Exception exception = result.GetExceptionOrThrow();

        // Then
        Assert.Equal(FailureException, exception);
    }

    [Fact(DisplayName = "Result.Match action from success should produce the expected result.")]
    public void ResultMatchActionFromSuccessShouldProduceExpectedResult()
    {
        // Given
        Result<int> result = Result<int>.Success(1);
        bool isSuccess = false;
        bool isFailure = false;

        // When
        result.Match(
            success: _ => isSuccess = true,
            failure: _ => isFailure = true
        );

        // Then
        Assert.True(isSuccess);
        Assert.False(isFailure);
    }

    [Fact(DisplayName = "Result.Match action from failure should produce the expected result.")]
    public void ResultMatchActionFromFailureShouldProduceExpectedResult()
    {
        // Given
        Result<int> result = Result<int>.Failure(FailureException);
        bool isSuccess = false;
        bool isFailure = false;

        // When
        result.Match(
            success: _ => isSuccess = true,
            failure: _ => isFailure = true
        );

        // Then
        Assert.False(isSuccess);
        Assert.True(isFailure);
    }

    [Fact(DisplayName = "Result.Match function from success should produce the expected result.")]
    public void ResultMatchFunctionFromSuccessShouldProduceExpectedResult()
    {
        // Given
        Result<int> result = Result<int>.Success(1);
        const int expected = 1;

        // When
        int actual = result.Match(
            success: _ => 1,
            failure: _ => 0
        );

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result.Match function from failure should produce the expected result.")]
    public void ResultMatchFunctionFromFailureShouldProduceExpectedResult()
    {
        // Given
        Result<int> result = Result<int>.Failure(FailureException);
        const int expected = 1;

        // When
        int actual = result.Match(
            success: _ => 0,
            failure: _ => 1
        );

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result.Select action from failure should produce the expected result.")]
    public void ResultSelectActionFromFailureShouldProduceExpectedResult()
    {
        // Given
        Result<int> result = Result<int>.Failure(FailureException);
        Result expected = Result.Failure(FailureException);

        // Then
        Result actual = result.Select(_ => { });

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result.Select action from success should produce the expected result.")]
    public void ResultSelectActionFromSuccessShouldProduceExpectedResult()
    {
        // Given
        Result<int> result = Result<int>.Success(1);
        Result expected = Result.Success();

        // Then
        Result actual = result.Select(_ => { });

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result.Select action from success with exception should produce the expected result.")]
    public void ResultSelectActionFromSuccessWithExceptionShouldProduceExpectedResult()
    {
        // Given
        Result<int> result = Result<int>.Success(1);
        Result expected = Result.Failure(FailureException);

        // Then
        Result actual = result.Select(_ => throw FailureException);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result.Select function of TResult from failure should produce the expected result.")]
    public void ResultSelectFunctionOfTResultFromFailureShouldProduceExpectedResult()
    {
        // Given
        Result<int> result = Result<int>.Failure(FailureException);
        Result<int> expected = Result<int>.Failure(FailureException);

        // Then
        Result<int> actual = result.Select(_ => 1);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result.Select function of TResult from success should produce the expected result.")]
    public void ResultSelectFunctionOfTResultFromSuccessShouldProduceExpectedResult()
    {
        // Given
        Result<int> result = Result<int>.Success(2);
        Result<int> expected = 1;

        // Then
        Result<int> actual = result.Select(_ => 1);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result.Select function of TResult from success with exception should produce the expected result.")]
    public void ResultSelectFunctionOfTResultFromSuccessWithExceptionShouldProduceExpectedResult()
    {
        // Given
        Result<int> result = Result<int>.Success(1);
        Result<int> expected = Result<int>.Failure(FailureException);

        // Then
        Result<int> actual = result.Select<int>(_ => throw FailureException);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result.SelectMany function from failure should produce the expected result.")]
    public void ResultSelectManyFunctionFromFailureShouldProduceExpectedResult()
    {
        // Given
        Result<int> result = Result<int>.Failure(FailureException);
        Result expected = Result.Failure(FailureException);

        // When
        Result actual = result.SelectMany(_ => Result.Success());

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result.SelectMany function from success should produce the expected result.")]
    public void ResultSelectManyFunctionFromSuccessShouldProduceExpectedResult()
    {
        // Given
        Result<int> result = Result<int>.Success(1);
        Result expected = Result.Success();

        // When
        Result actual = result.SelectMany(_ => Result.Success());

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result.SelectMany function from success with exception should produce the expected result.")]
    public void ResultSelectManyFunctionFromSuccessWithExceptionShouldProduceExpectedResult()
    {
        // Given
        Result<int> result = Result<int>.Success(1);
        Result expected = Result.Failure(FailureException);

        // When
        Result actual = result.SelectMany(_ => throw FailureException);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result.SelectMany function of TResult from failure should produce the expected result.")]
    public void ResultSelectManyFunctionOfTResultFromFailureShouldProduceExpectedResult()
    {
        // Given
        Result<int> result = Result<int>.Failure(FailureException);
        Result<int> expected = Result<int>.Failure(FailureException);

        // When
        Result<int> actual = result.SelectMany<int>(_ => 1);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result.SelectMany function of TResult from success should produce the expected result.")]
    public void ResultSelectManyFunctionOfTResultFromSuccessShouldProduceExpectedResult()
    {
        // Given
        Result<int> result = Result<int>.Success(2);
        Result<int> expected = 1;

        // When
        Result<int> actual = result.SelectMany<int>(_ => 1);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result.SelectMany function of TResult from success with exception should produce the expected result.")]
    public void ResultSelectManyFunctionOfTResultFromSuccessWithExceptionShouldProduceExpectedResult()
    {
        // Given
        Result<int> result = Result<int>.Success(1);
        Result<int> expected = Result<int>.Failure(FailureException);

        // When
        Result<int> actual = result.SelectMany<int>(_ => throw FailureException);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result.Throw from success should produce expected result.")]
    public void ResultThrowFromSuccessShouldProduceExpectedResult()
    {
        // Given
        Result<int> result = Result<int>.Success(1);

        // When / Then
        result.Throw();
    }

    [Fact(DisplayName = "Result.Throw from failure should produce expected result.")]
    public void ResultThrowFromFailureShouldProduceExpectedResult()
    {
        // Given
        Result<int> result = Result<int>.Failure(FailureException);

        // When
        Exception exception = Assert.Throws<Exception>(() => result.Throw());

        // Then
        Assert.Equal(FailureException, exception);
    }

    [Fact(DisplayName = "Result.Dispose from success should produce the expected result.")]
    public void ResultDisposeFromSuccessShouldProduceExpectedResult()
    {
        // Given
        Disposable disposable = new();
        Result<Disposable> result = Result<Disposable>.Success(disposable);

        // When
        result.Dispose();

        // Then
        Assert.True(disposable.IsDisposed);
    }

    [Fact(DisplayName = "Result.Dispose from failure should produce the expected result.")]
    public void ResultDisposeFromFailureShouldProduceExpectedResult()
    {
        // Given
        Disposable disposable = new();
        Result<Disposable> result = Result<Disposable>.Failure(FailureException);

        // When
        result.Dispose();

        // Then
        Assert.False(disposable.IsDisposed);
    }

    [Fact(DisplayName = "Result.DisposeAsync from success should produce the expected result.")]
    public async Task ResultDisposeAsyncFromSuccessShouldProduceExpectedResult()
    {
        // Given
        Disposable disposable = new();
        Result<Disposable> result = Result<Disposable>.Success(disposable);

        // When
        await result.DisposeAsync();

        // Then
        Assert.True(disposable.IsDisposed);
    }

    [Fact(DisplayName = "Result.DisposeAsync from failure should produce the expected result.")]
    public async Task ResultDisposeAsyncFromFailureShouldProduceExpectedResult()
    {
        // Given
        Disposable disposable = new();
        Result<Disposable> result = Result<Disposable>.Failure(FailureException);

        // When
        await result.DisposeAsync();

        // Then
        Assert.False(disposable.IsDisposed);
    }

    [Fact(DisplayName = "Result.ToString from success should produce expected result.")]
    public void ResultToStringFromSuccessShouldProduceExpectedResult()
    {
        // Given
        Result<int> result = Result<int>.Success(1);
        const string expected = "1";

        // When
        string actual = result.ToString();

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result.ToString from failure should produce expected result.")]
    public void ResultToStringFromFailureShouldProduceExpectedResult()
    {
        // Given
        Result<int> result = Result<int>.Failure(FailureException);
        string expected = FailureException.Message;

        // When
        string actual = result.ToString();

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Failure.ToTypedResult should produce the expected result.")]
    public void FailureToTypedResultShouldProduceExpectedResult()
    {
        // Given
        Failure<int> result = Result<int>.Failure(FailureException);
        Result<string> expected = FailureException;

        // When
        Result<string> actual = result.ToTypedResult<string>();

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Failure.ToUntypedResult should produce the expected result.")]
    public void FailureToUntypedResultShouldProduceExpectedResult()
    {
        // Given
        Failure<int> result = Result<int>.Failure(FailureException);
        Result expected = FailureException;

        // When
        Result actual = result.ToUntypedResult();

        // Then
        Assert.Equal(expected, actual);
    }
}
