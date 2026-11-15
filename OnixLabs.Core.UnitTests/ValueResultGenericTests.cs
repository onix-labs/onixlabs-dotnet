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

namespace OnixLabs.Core.UnitTests;

public sealed class ValueResultGenericTests
{
    private static readonly Exception FailureException = new("Failure");

    [Fact(DisplayName = "ValueResult<T>.IsSuccess should produce the expected result")]
    public void ValueResultIsSuccessShouldProduceExpectedResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(123);

        // When / Then
        Assert.True(result.IsSuccess);
        Assert.False(result.IsFailure);
    }

    [Fact(DisplayName = "ValueResult<T>.IsFailure should produce the expected result")]
    public void ValueResultIsFailureShouldProduceExpectedResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);

        // When / Then
        Assert.True(result.IsFailure);
        Assert.False(result.IsSuccess);
    }

    [Fact(DisplayName = "ValueResult<T>.Of should produce the expected success result")]
    public void ValueResultOfShouldProduceExpectedSuccessResult()
    {
        // Given / When
        ValueResult<int> result = ValueResult<int>.Of(() => 123);

        // Then
        Assert.True(result.IsSuccess);
        Assert.Equal(123, result.GetValueOrThrow());
    }

    [Fact(DisplayName = "ValueResult<T>.Of should produce the expected failure result")]
    public void ValueResultOfShouldProduceExpectedFailureResult()
    {
        // Given / When
        ValueResult<int> result = ValueResult<int>.Of(() => throw FailureException);

        // Then
        Assert.True(result.IsFailure);
        Assert.Same(FailureException, result.GetExceptionOrDefault());
    }

    [Fact(DisplayName = "ValueResult<T>.OfAsync should produce the expected success result.")]
    public async Task ValueResultOfAsyncShouldProduceExpectedSuccessResult()
    {
        // Given / When
        ValueResult<int> result = await ValueResult<int>.OfAsync(async () => await Task.FromResult(1), TestContext.Current.CancellationToken);

        // Then
        Assert.True(result.IsSuccess);
        Assert.Equal(1, result.GetValueOrThrow());
    }

    [Fact(DisplayName = "ValueResult<T>.OfAsync should produce the expected failure result.")]
    public async Task ValueResultOfAsyncShouldProduceExpectedFailureResult()
    {
        // Given / When
        ValueResult<int> result = await ValueResult<int>.OfAsync(async () => await Task.FromException<int>(FailureException), TestContext.Current.CancellationToken);

        // Then
        Assert.True(result.IsFailure);
        Assert.Equal(FailureException, result.GetExceptionOrThrow());
    }

    [Fact(DisplayName = "ValueResult<T>.OfAsync with cancellation token should produce the expected success result.")]
    public async Task ValueResultOfAsyncWithCancellationTokenShouldProduceExpectedSuccessResult()
    {
        // Given / When
        ValueResult<int> result = await ValueResult<int>.OfAsync(async () => await Task.FromResult(1), CancellationToken.None);

        // Then
        Assert.True(result.IsSuccess);
        Assert.Equal(1, result.GetValueOrThrow());
    }

    [Fact(DisplayName = "ValueResult<T>.OfAsync with cancellation token should produce the expected failure result.")]
    public async Task ValueResultOfAsyncWithCancellationTokenShouldProduceExpectedFailureResult()
    {
        // Given / When
        ValueResult<int> result = await ValueResult<int>.OfAsync(async () => await Task.FromException<int>(FailureException), CancellationToken.None);

        // Then
        Assert.True(result.IsFailure);
        Assert.Equal(FailureException, result.GetExceptionOrThrow());
    }

    [Fact(DisplayName = "ValueResult<T>.OfAsync with cancellable function should produce the expected success result.")]
    public async Task ValueResultOfAsyncWithCancellableFunctionShouldProduceExpectedSuccessResult()
    {
        // Given / When
        ValueResult<int> result = await ValueResult<int>.OfAsync(async _ => await Task.FromResult(1), CancellationToken.None);

        // Then
        Assert.True(result.IsSuccess);
        Assert.Equal(1, result.GetValueOrThrow());
    }

    [Fact(DisplayName = "ValueResult<T>.OfAsync with cancellable function should produce the expected failure result.")]
    public async Task ValueResultOfAsyncWithCancellableFunctionShouldProduceExpectedFailureResult()
    {
        // Given / When
        ValueResult<int> result = await ValueResult<int>.OfAsync(async _ => await Task.FromException<int>(FailureException), CancellationToken.None);

        // Then
        Assert.True(result.IsFailure);
        Assert.Equal(FailureException, result.GetExceptionOrThrow());
    }

    [Fact(DisplayName = "ValueResult<T>.Success should produce the expected success result.")]
    public void ValueResultSuccessShouldProduceExpectedSuccessResult()
    {
        // Given / When
        ValueResult<int> result = ValueResult<int>.Success(123);

        // Then
        Assert.True(result.IsSuccess);
        Assert.Equal(123, result.GetValueOrThrow());
    }

    [Fact(DisplayName = "ValueResult<T>.Failure should produce the expected failure result.")]
    public void ValueResultFailureShouldProduceExpectedFailureResult()
    {
        // Given / When
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);

        // Then
        Assert.True(result.IsFailure);
        Assert.Equal(FailureException, result.GetExceptionOrThrow());
    }

    [Fact(DisplayName = "ValueResult<T> from implicit value should produce the expected success result.")]
    public void ValueResultFromImplicitValueShouldProduceExpectedSuccessResult()
    {
        // Given / When
        ValueResult<int> result = 1;

        // Then
        Assert.True(result.IsSuccess);
        Assert.Equal(1, result.GetValueOrThrow());
    }

    [Fact(DisplayName = "ValueResult<T> from implicit exception should produce the expected failure result.")]
    public void ValueResultFromImplicitExceptionShouldProduceExpectedFailureResult()
    {
        // Given / When
        ValueResult<int> result = FailureException;

        // Then
        Assert.True(result.IsFailure);
        Assert.Equal(FailureException, result.GetExceptionOrThrow());
    }

    [Fact(DisplayName = "ValueResult<T> to explicit value from success should produce the expected result.")]
    public void ValueResultToExplicitValueFromSuccessShouldProduceExpectedResult()
    {
        // Given
        ValueResult<int> result = 1;
        const int expected = 1;

        // When
        int actual = (int)result;

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult<T> to explicit value from failure should produce the expected result.")]
    public void ValueResultToExplicitValueFromFailureShouldProduceExpectedResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);

        // When
        Exception exception = Assert.Throws<Exception>(() => (int)result);

        // Then
        Assert.Equal(FailureException, exception);
    }

    [Fact(DisplayName = "ValueResult<T> equality should produce the expected result")]
    public void ValueResultEqualityShouldProduceExpectedResult()
    {
        // Given
        ValueResult<int> success = ValueResult<int>.Success(123);
        ValueResult<int> failure = ValueResult<int>.Failure(FailureException);

        // When / Then
        Assert.True(success == ValueResult<int>.Success(123));
        Assert.True(success != ValueResult<int>.Success(456));
        Assert.True(failure == ValueResult<int>.Failure(FailureException));
        Assert.True(failure != ValueResult<int>.Failure(new Exception("Failure")));
        Assert.True(success != failure);
        Assert.True(ValueResult<int>.Success(0) != default);
        Assert.True(ValueResult<string?>.Success(null) == ValueResult<string?>.Success(null));
        Assert.True(success.Equals((object)ValueResult<int>.Success(123)));
        Assert.False(success.Equals((object)Result<int>.Success(123)));
        Assert.Equal(123.GetHashCode(), success.GetHashCode());
        Assert.Equal(FailureException.GetHashCode(), failure.GetHashCode());

        // ReSharper disable EqualExpressionComparison

        // Equals Method
        Assert.True(ValueResult<int>.Success(1).Equals(ValueResult<int>.Success(1)));
        Assert.True(ValueResult<int>.Failure(FailureException).Equals(ValueResult<int>.Failure(FailureException)));
        Assert.False(ValueResult<int>.Success(1).Equals(ValueResult<int>.Success(2)));
        Assert.False(ValueResult<int>.Success(1).Equals((object?)null));
        Assert.False(ValueResult<int>.Failure(FailureException).Equals((object?)null));

        // Equality Operator
        Assert.True(ValueResult<int>.Success(1) == ValueResult<int>.Success(1));
        Assert.True(ValueResult<int>.Failure(FailureException) == ValueResult<int>.Failure(FailureException));
        Assert.False(ValueResult<int>.Success(1) == ValueResult<int>.Success(2));
        Assert.False(ValueResult<int>.Success(1) == ValueResult<int>.Failure(FailureException));

        // Inequality Operator
        Assert.True(ValueResult<int>.Success(1) != ValueResult<int>.Failure(FailureException));
        Assert.True(ValueResult<int>.Failure(FailureException) != ValueResult<int>.Failure(new Exception()));

        // Hash Code Generation
        Assert.True(ValueResult<int>.Success(1).GetHashCode() == ValueResult<int>.Success(1).GetHashCode());
        Assert.True(ValueResult<int>.Failure(FailureException).GetHashCode() == ValueResult<int>.Failure(FailureException).GetHashCode());
    }

    [Fact(DisplayName = "ValueResult<T>.GetValue should produce the expected result when the result is Success.")]
    public void ValueResultGetValueShouldProduceExpectedResultWhenResultIsSuccess()
    {
        // Given
        const string expected = "abc";
        ValueResult<string> result = expected;

        // When
        ValueResult<string> actual = result.GetValue(out string value);

        // Then
        Assert.True(actual.IsSuccess);
        Assert.Equal(expected, value);
    }

    [Fact(DisplayName = "ValueResult<T>.GetValue should produce the expected result when the result is Failure.")]
    public void ValueResultGetValueShouldProduceExpectedResultWhenResultIsFailure()
    {
        // Given
        ValueResult<string> result = FailureException;

        // When
        ValueResult<string> actual = result.GetValue(out string value);

        // Then
        Assert.True(actual.IsFailure);
        Assert.Null(value);
    }

    [Fact(DisplayName = "ValueResult<T>.GetExceptionOrDefault from success should produce the expected result.")]
    public void ValueResultGetExceptionOrDefaultFromSuccessShouldProduceExpectedResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(1);

        // Then
        Exception? exception = result.GetExceptionOrDefault();

        // Then
        Assert.Null(exception);
    }

    [Fact(DisplayName = "ValueResult<T>.GetExceptionOrDefault from failure should produce the expected result.")]
    public void ValueResultGetExceptionOrDefaultFromFailureShouldProduceExpectedResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);

        // Then
        Exception? exception = result.GetExceptionOrDefault();

        // Then
        Assert.Equal(FailureException, exception);
    }

    [Fact(DisplayName = "ValueResult<T>.GetExceptionOrDefault from success with default value should produce the expected result.")]
    public void ValueResultGetExceptionOrDefaultFromSuccessWithDefaultValueShouldProduceExpectedResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(1);
        Exception defaultValue = new("Default");

        // Then
        Exception exception = result.GetExceptionOrDefault(defaultValue);

        // Then
        Assert.Equal(defaultValue, exception);
    }

    [Fact(DisplayName = "ValueResult<T>.GetExceptionOrDefault from failure with default value should produce the expected result.")]
    public void ValueResultGetExceptionOrDefaultFromFailureWithDefaultValueShouldProduceExpectedResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        Exception defaultValue = new("Default");

        // Then
        Exception exception = result.GetExceptionOrDefault(defaultValue);

        // Then
        Assert.Equal(FailureException, exception);
    }

    [Fact(DisplayName = "ValueResult<T>.GetExceptionOrNone from success should produce the expected result.")]
    public void ValueResultGetExceptionOrNoneFromSuccessShouldProduceExpectedResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(1);

        // When
        ValueOptional<Exception> exception = result.GetExceptionOrNone();

        // Then
        Assert.Equal(ValueOptional<Exception>.None, exception);
    }

    [Fact(DisplayName = "ValueResult<T>.GetExceptionOrNone from failure should produce the expected result.")]
    public void ValueResultGetExceptionOrNoneFromFailureShouldProduceExpectedResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);

        // When
        ValueOptional<Exception> exception = result.GetExceptionOrNone();

        // Then
        Assert.Equal(ValueOptional<Exception>.Some(FailureException), exception);
    }

    [Fact(DisplayName = "ValueResult<T>.GetExceptionOrThrow from success should produce the expected result.")]
    public void ValueResultGetExceptionOrThrowFromSuccessShouldProduceExpectedResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(1);

        // Then
        Exception exception = Assert.Throws<InvalidOperationException>(() => result.GetExceptionOrThrow());

        // Then
        Assert.Equal("The current result is in a success state; no exception to retrieve.", exception.Message);
    }

    [Fact(DisplayName = "ValueResult<T>.GetExceptionOrThrow from failure should produce the expected result.")]
    public void ValueResultGetExceptionOrThrowFromFailureShouldProduceExpectedResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);

        // Then
        Exception exception = result.GetExceptionOrThrow();

        // Then
        Assert.Equal(FailureException, exception);
    }

    [Fact(DisplayName = "ValueResult<T>.Match action from success should produce the expected result.")]
    public void ValueResultMatchActionFromSuccessShouldProduceExpectedResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(1);
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

    [Fact(DisplayName = "ValueResult<T>.Match action from failure should produce the expected result.")]
    public void ValueResultMatchActionFromFailureShouldProduceExpectedResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
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

    [Fact(DisplayName = "ValueResult<T>.MatchAsync Func<T, Task> from success should produce the expected result.")]
    public async Task ValueResultMatchAsyncFuncTTaskFromSuccessShouldProduceExpectedResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(1);
        bool isSuccess = false;
        bool isFailure = false;

        // When
        await result.MatchAsync(
            success: _ =>
            {
                isSuccess = true;
                return Task.CompletedTask;
            },
            failure: _ => isFailure = true,
            cancellationToken: TestContext.Current.CancellationToken
        );

        // Then
        Assert.True(isSuccess);
        Assert.False(isFailure);
    }

    [Fact(DisplayName = "ValueResult<T>.MatchAsync Func<T, Task> from success should produce the expected result when the delegates complete asynchronously.")]
    public async Task ValueResultMatchAsyncFuncTTaskFromSuccessShouldProduceExpectedResultWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(1);
        bool isSuccess = false;
        bool isFailure = false;

        // When
        await result.MatchAsync(
            success: _ =>
            {
                isSuccess = true;
                return Yielding.CompletedTask();
            },
            failure: _ => isFailure = true,
            cancellationToken: TestContext.Current.CancellationToken
        );

        // Then
        Assert.True(isSuccess);
        Assert.False(isFailure);
    }

    [Fact(DisplayName = "ValueResult<T>.MatchAsync Func<T, ValueTask> from success should produce the expected result.")]
    public async Task ValueResultMatchAsyncFuncTTaskFromSuccessShouldProduceExpectedResultWithValueTaskDelegates()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(1);
        bool isSuccess = false;
        bool isFailure = false;

        // When
        await result.MatchAsync(
            success: _ =>
            {
                isSuccess = true;
                return ValueTask.CompletedTask;
            },
            failure: _ => isFailure = true,
            cancellationToken: TestContext.Current.CancellationToken
        );

        // Then
        Assert.True(isSuccess);
        Assert.False(isFailure);
    }

    [Fact(DisplayName = "ValueResult<T>.MatchAsync Func<T, ValueTask> from success should produce the expected result when the delegates complete asynchronously.")]
    public async Task ValueResultMatchAsyncFuncTTaskFromSuccessShouldProduceExpectedResultWithValueTaskDelegatesWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(1);
        bool isSuccess = false;
        bool isFailure = false;

        // When
        await result.MatchAsync(
            success: _ =>
            {
                isSuccess = true;
                return Yielding.CompletedValueTask();
            },
            failure: _ => isFailure = true,
            cancellationToken: TestContext.Current.CancellationToken
        );

        // Then
        Assert.True(isSuccess);
        Assert.False(isFailure);
    }

    [Fact(DisplayName = "ValueResult<T>.MatchAsync Func<T, Task> from failure should produce the expected result.")]
    public async Task ValueResultMatchAsyncFuncTTaskFromFailureShouldProduceExpectedResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        bool isSuccess = false;
        bool isFailure = false;

        // When
        await result.MatchAsync(
            success: _ =>
            {
                isSuccess = true;
                return Task.CompletedTask;
            },
            failure: _ => isFailure = true,
            cancellationToken: TestContext.Current.CancellationToken
        );

        // Then
        Assert.False(isSuccess);
        Assert.True(isFailure);
    }

    [Fact(DisplayName = "ValueResult<T>.MatchAsync Func<T, Task> from failure should produce the expected result when the delegates complete asynchronously.")]
    public async Task ValueResultMatchAsyncFuncTTaskFromFailureShouldProduceExpectedResultWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        bool isSuccess = false;
        bool isFailure = false;

        // When
        await result.MatchAsync(
            success: _ =>
            {
                isSuccess = true;
                return Yielding.CompletedTask();
            },
            failure: _ => isFailure = true,
            cancellationToken: TestContext.Current.CancellationToken
        );

        // Then
        Assert.False(isSuccess);
        Assert.True(isFailure);
    }

    [Fact(DisplayName = "ValueResult<T>.MatchAsync Func<T, ValueTask> from failure should produce the expected result.")]
    public async Task ValueResultMatchAsyncFuncTTaskFromFailureShouldProduceExpectedResultWithValueTaskDelegates()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        bool isSuccess = false;
        bool isFailure = false;

        // When
        await result.MatchAsync(
            success: _ =>
            {
                isSuccess = true;
                return ValueTask.CompletedTask;
            },
            failure: _ => isFailure = true,
            cancellationToken: TestContext.Current.CancellationToken
        );

        // Then
        Assert.False(isSuccess);
        Assert.True(isFailure);
    }

    [Fact(DisplayName = "ValueResult<T>.MatchAsync Func<T, ValueTask> from failure should produce the expected result when the delegates complete asynchronously.")]
    public async Task ValueResultMatchAsyncFuncTTaskFromFailureShouldProduceExpectedResultWithValueTaskDelegatesWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        bool isSuccess = false;
        bool isFailure = false;

        // When
        await result.MatchAsync(
            success: _ =>
            {
                isSuccess = true;
                return Yielding.CompletedValueTask();
            },
            failure: _ => isFailure = true,
            cancellationToken: TestContext.Current.CancellationToken
        );

        // Then
        Assert.False(isSuccess);
        Assert.True(isFailure);
    }

    [Fact(DisplayName = "ValueResult<T>.MatchAsync Func<T, CancellationToken, Task> from success should produce the expected result.")]
    public async Task ValueResultMatchAsyncFuncTCancellationTokenTaskFromSuccessShouldProduceExpectedResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(1);
        bool isSuccess = false;
        bool isFailure = false;
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        await result.MatchAsync(
            success: (_, _) =>
            {
                isSuccess = true;
                return Task.CompletedTask;
            },
            failure: _ => isFailure = true,
            cancellationToken
        );

        // Then
        Assert.True(isSuccess);
        Assert.False(isFailure);
    }

    [Fact(DisplayName = "ValueResult<T>.MatchAsync Func<T, CancellationToken, Task> from success should produce the expected result when the delegates complete asynchronously.")]
    public async Task ValueResultMatchAsyncFuncTCancellationTokenTaskFromSuccessShouldProduceExpectedResultWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(1);
        bool isSuccess = false;
        bool isFailure = false;
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        await result.MatchAsync(
            success: (_, _) =>
            {
                isSuccess = true;
                return Yielding.CompletedTask();
            },
            failure: _ => isFailure = true,
            cancellationToken
        );

        // Then
        Assert.True(isSuccess);
        Assert.False(isFailure);
    }

    [Fact(DisplayName = "ValueResult<T>.MatchAsync Func<T, CancellationToken, ValueTask> from success should produce the expected result.")]
    public async Task ValueResultMatchAsyncFuncTCancellationTokenTaskFromSuccessShouldProduceExpectedResultWithValueTaskDelegates()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(1);
        bool isSuccess = false;
        bool isFailure = false;
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        await result.MatchAsync(
            success: (_, _) =>
            {
                isSuccess = true;
                return ValueTask.CompletedTask;
            },
            failure: _ => isFailure = true,
            cancellationToken
        );

        // Then
        Assert.True(isSuccess);
        Assert.False(isFailure);
    }

    [Fact(DisplayName = "ValueResult<T>.MatchAsync Func<T, CancellationToken, ValueTask> from success should produce the expected result when the delegates complete asynchronously.")]
    public async Task ValueResultMatchAsyncFuncTCancellationTokenTaskFromSuccessShouldProduceExpectedResultWithValueTaskDelegatesWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(1);
        bool isSuccess = false;
        bool isFailure = false;
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        await result.MatchAsync(
            success: (_, _) =>
            {
                isSuccess = true;
                return Yielding.CompletedValueTask();
            },
            failure: _ => isFailure = true,
            cancellationToken
        );

        // Then
        Assert.True(isSuccess);
        Assert.False(isFailure);
    }

    [Fact(DisplayName = "ValueResult<T>.MatchAsync Func<T, CancellationToken, Task> from failure should produce the expected result.")]
    public async Task ValueResultMatchAsyncFuncTCancellationTokenTaskFromFailureShouldProduceExpectedResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        bool isSuccess = false;
        bool isFailure = false;
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        await result.MatchAsync(
            success: (_, _) =>
            {
                isSuccess = true;
                return Task.CompletedTask;
            },
            failure: _ => isFailure = true,
            cancellationToken
        );

        // Then
        Assert.False(isSuccess);
        Assert.True(isFailure);
    }

    [Fact(DisplayName = "ValueResult<T>.MatchAsync Func<T, CancellationToken, Task> from failure should produce the expected result when the delegates complete asynchronously.")]
    public async Task ValueResultMatchAsyncFuncTCancellationTokenTaskFromFailureShouldProduceExpectedResultWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        bool isSuccess = false;
        bool isFailure = false;
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        await result.MatchAsync(
            success: (_, _) =>
            {
                isSuccess = true;
                return Yielding.CompletedTask();
            },
            failure: _ => isFailure = true,
            cancellationToken
        );

        // Then
        Assert.False(isSuccess);
        Assert.True(isFailure);
    }

    [Fact(DisplayName = "ValueResult<T>.MatchAsync Func<T, CancellationToken, ValueTask> from failure should produce the expected result.")]
    public async Task ValueResultMatchAsyncFuncTCancellationTokenTaskFromFailureShouldProduceExpectedResultWithValueTaskDelegates()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        bool isSuccess = false;
        bool isFailure = false;
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        await result.MatchAsync(
            success: (_, _) =>
            {
                isSuccess = true;
                return ValueTask.CompletedTask;
            },
            failure: _ => isFailure = true,
            cancellationToken
        );

        // Then
        Assert.False(isSuccess);
        Assert.True(isFailure);
    }

    [Fact(DisplayName = "ValueResult<T>.MatchAsync Func<T, CancellationToken, ValueTask> from failure should produce the expected result when the delegates complete asynchronously.")]
    public async Task ValueResultMatchAsyncFuncTCancellationTokenTaskFromFailureShouldProduceExpectedResultWithValueTaskDelegatesWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        bool isSuccess = false;
        bool isFailure = false;
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        await result.MatchAsync(
            success: (_, _) =>
            {
                isSuccess = true;
                return Yielding.CompletedValueTask();
            },
            failure: _ => isFailure = true,
            cancellationToken
        );

        // Then
        Assert.False(isSuccess);
        Assert.True(isFailure);
    }

    [Fact(DisplayName = "ValueResult<T>.MatchAsync Action<T>, Func<Exception, Task> from success should produce the expected result.")]
    public async Task ValueResultMatchAsyncActionTFuncExceptionTaskFromSuccessShouldProduceExpectedResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(1);
        bool isSuccess = false;
        bool isFailure = false;

        // When
        await result.MatchAsync(
            success: _ => isSuccess = true,
            failure: _ =>
            {
                isFailure = true;
                return Task.CompletedTask;
            },
            cancellationToken: TestContext.Current.CancellationToken
        );

        // Then
        Assert.True(isSuccess);
        Assert.False(isFailure);
    }

    [Fact(DisplayName = "ValueResult<T>.MatchAsync Action<T>, Func<Exception, Task> from success should produce the expected result when the delegates complete asynchronously.")]
    public async Task ValueResultMatchAsyncActionTFuncExceptionTaskFromSuccessShouldProduceExpectedResultWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(1);
        bool isSuccess = false;
        bool isFailure = false;

        // When
        await result.MatchAsync(
            success: _ => isSuccess = true,
            failure: _ =>
            {
                isFailure = true;
                return Yielding.CompletedTask();
            },
            cancellationToken: TestContext.Current.CancellationToken
        );

        // Then
        Assert.True(isSuccess);
        Assert.False(isFailure);
    }

    [Fact(DisplayName = "ValueResult<T>.MatchAsync Action<T>, Func<Exception, ValueTask> from success should produce the expected result.")]
    public async Task ValueResultMatchAsyncActionTFuncExceptionTaskFromSuccessShouldProduceExpectedResultWithValueTaskDelegates()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(1);
        bool isSuccess = false;
        bool isFailure = false;

        // When
        await result.MatchAsync(
            success: _ => isSuccess = true,
            failure: _ =>
            {
                isFailure = true;
                return ValueTask.CompletedTask;
            },
            cancellationToken: TestContext.Current.CancellationToken
        );

        // Then
        Assert.True(isSuccess);
        Assert.False(isFailure);
    }

    [Fact(DisplayName = "ValueResult<T>.MatchAsync Action<T>, Func<Exception, ValueTask> from success should produce the expected result when the delegates complete asynchronously.")]
    public async Task ValueResultMatchAsyncActionTFuncExceptionTaskFromSuccessShouldProduceExpectedResultWithValueTaskDelegatesWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(1);
        bool isSuccess = false;
        bool isFailure = false;

        // When
        await result.MatchAsync(
            success: _ => isSuccess = true,
            failure: _ =>
            {
                isFailure = true;
                return Yielding.CompletedValueTask();
            },
            cancellationToken: TestContext.Current.CancellationToken
        );

        // Then
        Assert.True(isSuccess);
        Assert.False(isFailure);
    }

    [Fact(DisplayName = "ValueResult<T>.MatchAsync Action<T>, Func<Exception, Task> from failure should produce the expected result.")]
    public async Task ValueResultMatchAsyncActionTFuncExceptionTaskFromFailureShouldProduceExpectedResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        bool isSuccess = false;
        bool isFailure = false;

        // When
        await result.MatchAsync(
            success: _ => isSuccess = true,
            failure: _ =>
            {
                isFailure = true;
                return Task.CompletedTask;
            },
            cancellationToken: TestContext.Current.CancellationToken
        );

        // Then
        Assert.False(isSuccess);
        Assert.True(isFailure);
    }

    [Fact(DisplayName = "ValueResult<T>.MatchAsync Action<T>, Func<Exception, Task> from failure should produce the expected result when the delegates complete asynchronously.")]
    public async Task ValueResultMatchAsyncActionTFuncExceptionTaskFromFailureShouldProduceExpectedResultWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        bool isSuccess = false;
        bool isFailure = false;

        // When
        await result.MatchAsync(
            success: _ => isSuccess = true,
            failure: _ =>
            {
                isFailure = true;
                return Yielding.CompletedTask();
            },
            cancellationToken: TestContext.Current.CancellationToken
        );

        // Then
        Assert.False(isSuccess);
        Assert.True(isFailure);
    }

    [Fact(DisplayName = "ValueResult<T>.MatchAsync Action<T>, Func<Exception, ValueTask> from failure should produce the expected result.")]
    public async Task ValueResultMatchAsyncActionTFuncExceptionTaskFromFailureShouldProduceExpectedResultWithValueTaskDelegates()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        bool isSuccess = false;
        bool isFailure = false;

        // When
        await result.MatchAsync(
            success: _ => isSuccess = true,
            failure: _ =>
            {
                isFailure = true;
                return ValueTask.CompletedTask;
            },
            cancellationToken: TestContext.Current.CancellationToken
        );

        // Then
        Assert.False(isSuccess);
        Assert.True(isFailure);
    }

    [Fact(DisplayName = "ValueResult<T>.MatchAsync Action<T>, Func<Exception, ValueTask> from failure should produce the expected result when the delegates complete asynchronously.")]
    public async Task ValueResultMatchAsyncActionTFuncExceptionTaskFromFailureShouldProduceExpectedResultWithValueTaskDelegatesWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        bool isSuccess = false;
        bool isFailure = false;

        // When
        await result.MatchAsync(
            success: _ => isSuccess = true,
            failure: _ =>
            {
                isFailure = true;
                return Yielding.CompletedValueTask();
            },
            cancellationToken: TestContext.Current.CancellationToken
        );

        // Then
        Assert.False(isSuccess);
        Assert.True(isFailure);
    }

    [Fact(DisplayName = "ValueResult<T>.MatchAsync Action<T>, Func<Exception, CancellationToken, Task> from success should produce the expected result.")]
    public async Task ValueResultMatchAsyncActionTFuncExceptionCancellationTokenTaskFromSuccessShouldProduceExpectedResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(1);
        bool isSuccess = false;
        bool isFailure = false;
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        await result.MatchAsync(
            success: _ => isSuccess = true,
            failure: (_, _) =>
            {
                isFailure = true;
                return Task.CompletedTask;
            },
            cancellationToken
        );

        // Then
        Assert.True(isSuccess);
        Assert.False(isFailure);
    }

    [Fact(DisplayName = "ValueResult<T>.MatchAsync Action<T>, Func<Exception, CancellationToken, Task> from success should produce the expected result when the delegates complete asynchronously.")]
    public async Task ValueResultMatchAsyncActionTFuncExceptionCancellationTokenTaskFromSuccessShouldProduceExpectedResultWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(1);
        bool isSuccess = false;
        bool isFailure = false;
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        await result.MatchAsync(
            success: _ => isSuccess = true,
            failure: (_, _) =>
            {
                isFailure = true;
                return Yielding.CompletedTask();
            },
            cancellationToken
        );

        // Then
        Assert.True(isSuccess);
        Assert.False(isFailure);
    }

    [Fact(DisplayName = "ValueResult<T>.MatchAsync Action<T>, Func<Exception, CancellationToken, ValueTask> from success should produce the expected result.")]
    public async Task ValueResultMatchAsyncActionTFuncExceptionCancellationTokenTaskFromSuccessShouldProduceExpectedResultWithValueTaskDelegates()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(1);
        bool isSuccess = false;
        bool isFailure = false;
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        await result.MatchAsync(
            success: _ => isSuccess = true,
            failure: (_, _) =>
            {
                isFailure = true;
                return ValueTask.CompletedTask;
            },
            cancellationToken
        );

        // Then
        Assert.True(isSuccess);
        Assert.False(isFailure);
    }

    [Fact(DisplayName = "ValueResult<T>.MatchAsync Action<T>, Func<Exception, CancellationToken, ValueTask> from success should produce the expected result when the delegates complete asynchronously.")]
    public async Task ValueResultMatchAsyncActionTFuncExceptionCancellationTokenTaskFromSuccessShouldProduceExpectedResultWithValueTaskDelegatesWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(1);
        bool isSuccess = false;
        bool isFailure = false;
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        await result.MatchAsync(
            success: _ => isSuccess = true,
            failure: (_, _) =>
            {
                isFailure = true;
                return Yielding.CompletedValueTask();
            },
            cancellationToken
        );

        // Then
        Assert.True(isSuccess);
        Assert.False(isFailure);
    }

    [Fact(DisplayName = "ValueResult<T>.MatchAsync Action<T>, Func<Exception, CancellationToken, Task> from failure should produce the expected result.")]
    public async Task ValueResultMatchAsyncActionTFuncExceptionCancellationTokenTaskFromFailureShouldProduceExpectedResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        bool isSuccess = false;
        bool isFailure = false;
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        await result.MatchAsync(
            success: _ => isSuccess = true,
            failure: (_, _) =>
            {
                isFailure = true;
                return Task.CompletedTask;
            },
            cancellationToken
        );

        // Then
        Assert.False(isSuccess);
        Assert.True(isFailure);
    }

    [Fact(DisplayName = "ValueResult<T>.MatchAsync Action<T>, Func<Exception, CancellationToken, Task> from failure should produce the expected result when the delegates complete asynchronously.")]
    public async Task ValueResultMatchAsyncActionTFuncExceptionCancellationTokenTaskFromFailureShouldProduceExpectedResultWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        bool isSuccess = false;
        bool isFailure = false;
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        await result.MatchAsync(
            success: _ => isSuccess = true,
            failure: (_, _) =>
            {
                isFailure = true;
                return Yielding.CompletedTask();
            },
            cancellationToken
        );

        // Then
        Assert.False(isSuccess);
        Assert.True(isFailure);
    }

    [Fact(DisplayName = "ValueResult<T>.MatchAsync Action<T>, Func<Exception, CancellationToken, ValueTask> from failure should produce the expected result.")]
    public async Task ValueResultMatchAsyncActionTFuncExceptionCancellationTokenTaskFromFailureShouldProduceExpectedResultWithValueTaskDelegates()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        bool isSuccess = false;
        bool isFailure = false;
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        await result.MatchAsync(
            success: _ => isSuccess = true,
            failure: (_, _) =>
            {
                isFailure = true;
                return ValueTask.CompletedTask;
            },
            cancellationToken
        );

        // Then
        Assert.False(isSuccess);
        Assert.True(isFailure);
    }

    [Fact(DisplayName = "ValueResult<T>.MatchAsync Action<T>, Func<Exception, CancellationToken, ValueTask> from failure should produce the expected result when the delegates complete asynchronously.")]
    public async Task ValueResultMatchAsyncActionTFuncExceptionCancellationTokenTaskFromFailureShouldProduceExpectedResultWithValueTaskDelegatesWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        bool isSuccess = false;
        bool isFailure = false;
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        await result.MatchAsync(
            success: _ => isSuccess = true,
            failure: (_, _) =>
            {
                isFailure = true;
                return Yielding.CompletedValueTask();
            },
            cancellationToken
        );

        // Then
        Assert.False(isSuccess);
        Assert.True(isFailure);
    }

    [Fact(DisplayName = "ValueResult<T>.MatchAsync Func<T, Task>, Func<Exception, Task> from success should produce the expected result.")]
    public async Task ValueResultMatchAsyncFuncTTaskFuncExceptionTaskFromSuccessShouldProduceExpectedResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(1);
        bool isSuccess = false;
        bool isFailure = false;
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        await result.MatchAsync(
            success: _ =>
            {
                isSuccess = true;
                return Task.CompletedTask;
            },
            failure: _ =>
            {
                isFailure = true;
                return Task.CompletedTask;
            },
            cancellationToken
        );

        // Then
        Assert.True(isSuccess);
        Assert.False(isFailure);
    }

    [Fact(DisplayName = "ValueResult<T>.MatchAsync Func<T, Task>, Func<Exception, Task> from success should produce the expected result when the delegates complete asynchronously.")]
    public async Task ValueResultMatchAsyncFuncTTaskFuncExceptionTaskFromSuccessShouldProduceExpectedResultWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(1);
        bool isSuccess = false;
        bool isFailure = false;
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        await result.MatchAsync(
            success: _ =>
            {
                isSuccess = true;
                return Yielding.CompletedTask();
            },
            failure: _ =>
            {
                isFailure = true;
                return Yielding.CompletedTask();
            },
            cancellationToken
        );

        // Then
        Assert.True(isSuccess);
        Assert.False(isFailure);
    }

    [Fact(DisplayName = "ValueResult<T>.MatchAsync Func<T, ValueTask>, Func<Exception, ValueTask> from success should produce the expected result.")]
    public async Task ValueResultMatchAsyncFuncTTaskFuncExceptionTaskFromSuccessShouldProduceExpectedResultWithValueTaskDelegates()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(1);
        bool isSuccess = false;
        bool isFailure = false;
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        await result.MatchAsync(
            success: _ =>
            {
                isSuccess = true;
                return ValueTask.CompletedTask;
            },
            failure: _ =>
            {
                isFailure = true;
                return ValueTask.CompletedTask;
            },
            cancellationToken
        );

        // Then
        Assert.True(isSuccess);
        Assert.False(isFailure);
    }

    [Fact(DisplayName = "ValueResult<T>.MatchAsync Func<T, ValueTask>, Func<Exception, ValueTask> from success should produce the expected result when the delegates complete asynchronously.")]
    public async Task ValueResultMatchAsyncFuncTTaskFuncExceptionTaskFromSuccessShouldProduceExpectedResultWithValueTaskDelegatesWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(1);
        bool isSuccess = false;
        bool isFailure = false;
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        await result.MatchAsync(
            success: _ =>
            {
                isSuccess = true;
                return Yielding.CompletedValueTask();
            },
            failure: _ =>
            {
                isFailure = true;
                return Yielding.CompletedValueTask();
            },
            cancellationToken
        );

        // Then
        Assert.True(isSuccess);
        Assert.False(isFailure);
    }

    [Fact(DisplayName = "ValueResult<T>.MatchAsync Func<T, Task>, Func<Exception, Task> from failure should produce the expected result.")]
    public async Task ValueResultMatchAsyncFuncTTaskFuncExceptionTaskFromFailureShouldProduceExpectedResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        bool isSuccess = false;
        bool isFailure = false;
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        await result.MatchAsync(
            success: _ =>
            {
                isSuccess = true;
                return Task.CompletedTask;
            },
            failure: _ =>
            {
                isFailure = true;
                return Task.CompletedTask;
            },
            cancellationToken
        );

        // Then
        Assert.False(isSuccess);
        Assert.True(isFailure);
    }

    [Fact(DisplayName = "ValueResult<T>.MatchAsync Func<T, Task>, Func<Exception, Task> from failure should produce the expected result when the delegates complete asynchronously.")]
    public async Task ValueResultMatchAsyncFuncTTaskFuncExceptionTaskFromFailureShouldProduceExpectedResultWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        bool isSuccess = false;
        bool isFailure = false;
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        await result.MatchAsync(
            success: _ =>
            {
                isSuccess = true;
                return Yielding.CompletedTask();
            },
            failure: _ =>
            {
                isFailure = true;
                return Yielding.CompletedTask();
            },
            cancellationToken
        );

        // Then
        Assert.False(isSuccess);
        Assert.True(isFailure);
    }

    [Fact(DisplayName = "ValueResult<T>.MatchAsync Func<T, ValueTask>, Func<Exception, ValueTask> from failure should produce the expected result.")]
    public async Task ValueResultMatchAsyncFuncTTaskFuncExceptionTaskFromFailureShouldProduceExpectedResultWithValueTaskDelegates()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        bool isSuccess = false;
        bool isFailure = false;
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        await result.MatchAsync(
            success: _ =>
            {
                isSuccess = true;
                return ValueTask.CompletedTask;
            },
            failure: _ =>
            {
                isFailure = true;
                return ValueTask.CompletedTask;
            },
            cancellationToken
        );

        // Then
        Assert.False(isSuccess);
        Assert.True(isFailure);
    }

    [Fact(DisplayName = "ValueResult<T>.MatchAsync Func<T, ValueTask>, Func<Exception, ValueTask> from failure should produce the expected result when the delegates complete asynchronously.")]
    public async Task ValueResultMatchAsyncFuncTTaskFuncExceptionTaskFromFailureShouldProduceExpectedResultWithValueTaskDelegatesWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        bool isSuccess = false;
        bool isFailure = false;
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        await result.MatchAsync(
            success: _ =>
            {
                isSuccess = true;
                return Yielding.CompletedValueTask();
            },
            failure: _ =>
            {
                isFailure = true;
                return Yielding.CompletedValueTask();
            },
            cancellationToken
        );

        // Then
        Assert.False(isSuccess);
        Assert.True(isFailure);
    }

    [Fact(DisplayName = "ValueResult<T>.MatchAsync Func<T, CancellationToken, Task>, Func<Exception, CancellationToken, Task> from success should produce the expected result.")]
    public async Task ValueResultMatchAsyncFuncTCancellationTokenTaskFuncExceptionCancellationTokenTaskFromSuccessShouldProduceExpectedResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(1);
        bool isSuccess = false;
        bool isFailure = false;
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        await result.MatchAsync(
            success: (_, _) =>
            {
                isSuccess = true;
                return Task.CompletedTask;
            },
            failure: (_, _) =>
            {
                isFailure = true;
                return Task.CompletedTask;
            },
            cancellationToken
        );

        // Then
        Assert.True(isSuccess);
        Assert.False(isFailure);
    }

    [Fact(DisplayName = "ValueResult<T>.MatchAsync Func<T, CancellationToken, Task>, Func<Exception, CancellationToken, Task> from success should produce the expected result when the delegates complete asynchronously.")]
    public async Task ValueResultMatchAsyncFuncTCancellationTokenTaskFuncExceptionCancellationTokenTaskFromSuccessShouldProduceExpectedResultWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(1);
        bool isSuccess = false;
        bool isFailure = false;
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        await result.MatchAsync(
            success: (_, _) =>
            {
                isSuccess = true;
                return Yielding.CompletedTask();
            },
            failure: (_, _) =>
            {
                isFailure = true;
                return Yielding.CompletedTask();
            },
            cancellationToken
        );

        // Then
        Assert.True(isSuccess);
        Assert.False(isFailure);
    }

    [Fact(DisplayName = "ValueResult<T>.MatchAsync Func<T, CancellationToken, ValueTask>, Func<Exception, CancellationToken, ValueTask> from success should produce the expected result.")]
    public async Task ValueResultMatchAsyncFuncTCancellationTokenTaskFuncExceptionCancellationTokenTaskFromSuccessShouldProduceExpectedResultWithValueTaskDelegates()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(1);
        bool isSuccess = false;
        bool isFailure = false;
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        await result.MatchAsync(
            success: (_, _) =>
            {
                isSuccess = true;
                return ValueTask.CompletedTask;
            },
            failure: (_, _) =>
            {
                isFailure = true;
                return ValueTask.CompletedTask;
            },
            cancellationToken
        );

        // Then
        Assert.True(isSuccess);
        Assert.False(isFailure);
    }

    [Fact(DisplayName = "ValueResult<T>.MatchAsync Func<T, CancellationToken, ValueTask>, Func<Exception, CancellationToken, ValueTask> from success should produce the expected result when the delegates complete asynchronously.")]
    public async Task ValueResultMatchAsyncFuncTCancellationTokenTaskFuncExceptionCancellationTokenTaskFromSuccessShouldProduceExpectedResultWithValueTaskDelegatesWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(1);
        bool isSuccess = false;
        bool isFailure = false;
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        await result.MatchAsync(
            success: (_, _) =>
            {
                isSuccess = true;
                return Yielding.CompletedValueTask();
            },
            failure: (_, _) =>
            {
                isFailure = true;
                return Yielding.CompletedValueTask();
            },
            cancellationToken
        );

        // Then
        Assert.True(isSuccess);
        Assert.False(isFailure);
    }

    [Fact(DisplayName = "ValueResult<T>.MatchAsync Func<T, CancellationToken, Task>, Func<Exception, CancellationToken, Task> from failure should produce the expected result.")]
    public async Task ValueResultMatchAsyncFuncTCancellationTokenTaskFuncExceptionCancellationTokenTaskFromFailureShouldProduceExpectedResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        bool isSuccess = false;
        bool isFailure = false;
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        await result.MatchAsync(
            success: (_, _) =>
            {
                isSuccess = true;
                return Task.CompletedTask;
            },
            failure: (_, _) =>
            {
                isFailure = true;
                return Task.CompletedTask;
            },
            cancellationToken
        );

        // Then
        Assert.False(isSuccess);
        Assert.True(isFailure);
    }

    [Fact(DisplayName = "ValueResult<T>.MatchAsync Func<T, CancellationToken, Task>, Func<Exception, CancellationToken, Task> from failure should produce the expected result when the delegates complete asynchronously.")]
    public async Task ValueResultMatchAsyncFuncTCancellationTokenTaskFuncExceptionCancellationTokenTaskFromFailureShouldProduceExpectedResultWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        bool isSuccess = false;
        bool isFailure = false;
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        await result.MatchAsync(
            success: (_, _) =>
            {
                isSuccess = true;
                return Yielding.CompletedTask();
            },
            failure: (_, _) =>
            {
                isFailure = true;
                return Yielding.CompletedTask();
            },
            cancellationToken
        );

        // Then
        Assert.False(isSuccess);
        Assert.True(isFailure);
    }

    [Fact(DisplayName = "ValueResult<T>.MatchAsync Func<T, CancellationToken, ValueTask>, Func<Exception, CancellationToken, ValueTask> from failure should produce the expected result.")]
    public async Task ValueResultMatchAsyncFuncTCancellationTokenTaskFuncExceptionCancellationTokenTaskFromFailureShouldProduceExpectedResultWithValueTaskDelegates()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        bool isSuccess = false;
        bool isFailure = false;
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        await result.MatchAsync(
            success: (_, _) =>
            {
                isSuccess = true;
                return ValueTask.CompletedTask;
            },
            failure: (_, _) =>
            {
                isFailure = true;
                return ValueTask.CompletedTask;
            },
            cancellationToken
        );

        // Then
        Assert.False(isSuccess);
        Assert.True(isFailure);
    }

    [Fact(DisplayName = "ValueResult<T>.MatchAsync Func<T, CancellationToken, ValueTask>, Func<Exception, CancellationToken, ValueTask> from failure should produce the expected result when the delegates complete asynchronously.")]
    public async Task ValueResultMatchAsyncFuncTCancellationTokenTaskFuncExceptionCancellationTokenTaskFromFailureShouldProduceExpectedResultWithValueTaskDelegatesWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        bool isSuccess = false;
        bool isFailure = false;
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        await result.MatchAsync(
            success: (_, _) =>
            {
                isSuccess = true;
                return Yielding.CompletedValueTask();
            },
            failure: (_, _) =>
            {
                isFailure = true;
                return Yielding.CompletedValueTask();
            },
            cancellationToken
        );

        // Then
        Assert.False(isSuccess);
        Assert.True(isFailure);
    }

    [Fact(DisplayName = "ValueResult<T>.Match function from success should produce the expected result.")]
    public void ValueResultMatchFunctionFromSuccessShouldProduceExpectedResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(1);
        const int expected = 1;

        // When
        int actual = result.Match(
            success: _ => 1,
            failure: _ => 0
        );

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult<T>.Match function from failure should produce the expected result.")]
    public void ValueResultMatchFunctionFromFailureShouldProduceExpectedResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        const int expected = 1;

        // When
        int actual = result.Match(
            success: _ => 0,
            failure: _ => 1
        );

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult<T>.MatchAsync Func<T, Task<TResult>>, Func<Exception, TResult> from success should produce the expected result.")]
    public async Task ValueResultMatchAsyncFuncTTaskTResultFuncExceptionTResultFromSuccessShouldProduceExpectedResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(1);
        const int expected = 1;

        // When
        int actual = await result.MatchAsync(
            success: _ => Task.FromResult(1),
            failure: _ => 0,
            cancellationToken: TestContext.Current.CancellationToken
        );

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult<T>.MatchAsync Func<T, Task<TResult>>, Func<Exception, TResult> from success should produce the expected result when the delegates complete asynchronously.")]
    public async Task ValueResultMatchAsyncFuncTTaskTResultFuncExceptionTResultFromSuccessShouldProduceExpectedResultWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(1);
        const int expected = 1;

        // When
        int actual = await result.MatchAsync(
            success: _ => Yielding.TaskOf(1),
            failure: _ => 0,
            cancellationToken: TestContext.Current.CancellationToken
        );

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult<T>.MatchAsync Func<T, ValueTask<TResult>>, Func<Exception, TResult> from success should produce the expected result.")]
    public async Task ValueResultMatchAsyncFuncTTaskTResultFuncExceptionTResultFromSuccessShouldProduceExpectedResultWithValueTaskDelegates()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(1);
        const int expected = 1;

        // When
        int actual = await result.MatchAsync(
            success: _ => ValueTask.FromResult(1),
            failure: _ => 0,
            cancellationToken: TestContext.Current.CancellationToken
        );

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult<T>.MatchAsync Func<T, ValueTask<TResult>>, Func<Exception, TResult> from success should produce the expected result when the delegates complete asynchronously.")]
    public async Task ValueResultMatchAsyncFuncTTaskTResultFuncExceptionTResultFromSuccessShouldProduceExpectedResultWithValueTaskDelegatesWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(1);
        const int expected = 1;

        // When
        int actual = await result.MatchAsync(
            success: _ => Yielding.ValueTaskOf(1),
            failure: _ => 0,
            cancellationToken: TestContext.Current.CancellationToken
        );

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult<T>.MatchAsync Func<T, Task<TResult>>, Func<Exception, TResult> from failure should produce the expected result.")]
    public async Task ValueResultMatchAsyncFuncTTaskTResultFuncExceptionTResultFromFailureShouldProduceExpectedResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        const int expected = 1;

        // When
        int actual = await result.MatchAsync(
            success: _ => Task.FromResult(0),
            failure: _ => 1,
            cancellationToken: TestContext.Current.CancellationToken
        );

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult<T>.MatchAsync Func<T, Task<TResult>>, Func<Exception, TResult> from failure should produce the expected result when the delegates complete asynchronously.")]
    public async Task ValueResultMatchAsyncFuncTTaskTResultFuncExceptionTResultFromFailureShouldProduceExpectedResultWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        const int expected = 1;

        // When
        int actual = await result.MatchAsync(
            success: _ => Yielding.TaskOf(0),
            failure: _ => 1,
            cancellationToken: TestContext.Current.CancellationToken
        );

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult<T>.MatchAsync Func<T, ValueTask<TResult>>, Func<Exception, TResult> from failure should produce the expected result.")]
    public async Task ValueResultMatchAsyncFuncTTaskTResultFuncExceptionTResultFromFailureShouldProduceExpectedResultWithValueTaskDelegates()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        const int expected = 1;

        // When
        int actual = await result.MatchAsync(
            success: _ => ValueTask.FromResult(0),
            failure: _ => 1,
            cancellationToken: TestContext.Current.CancellationToken
        );

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult<T>.MatchAsync Func<T, ValueTask<TResult>>, Func<Exception, TResult> from failure should produce the expected result when the delegates complete asynchronously.")]
    public async Task ValueResultMatchAsyncFuncTTaskTResultFuncExceptionTResultFromFailureShouldProduceExpectedResultWithValueTaskDelegatesWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        const int expected = 1;

        // When
        int actual = await result.MatchAsync(
            success: _ => Yielding.ValueTaskOf(0),
            failure: _ => 1,
            cancellationToken: TestContext.Current.CancellationToken
        );

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult<T>.MatchAsync Func<T, CancellationToken, Task<TResult>>, Func<Exception, TResult> from success should produce the expected result.")]
    public async Task ValueResultMatchAsyncFuncTCancellationTokenTaskTResultFuncExceptionTResultFromSuccessShouldProduceExpectedResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(1);
        const int expected = 1;
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        int actual = await result.MatchAsync(
            success: (_, _) => Task.FromResult(1),
            failure: _ => 0,
            cancellationToken
        );

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult<T>.MatchAsync Func<T, CancellationToken, Task<TResult>>, Func<Exception, TResult> from success should produce the expected result when the delegates complete asynchronously.")]
    public async Task ValueResultMatchAsyncFuncTCancellationTokenTaskTResultFuncExceptionTResultFromSuccessShouldProduceExpectedResultWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(1);
        const int expected = 1;
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        int actual = await result.MatchAsync(
            success: (_, _) => Yielding.TaskOf(1),
            failure: _ => 0,
            cancellationToken
        );

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult<T>.MatchAsync Func<T, CancellationToken, ValueTask<TResult>>, Func<Exception, TResult> from success should produce the expected result.")]
    public async Task ValueResultMatchAsyncFuncTCancellationTokenTaskTResultFuncExceptionTResultFromSuccessShouldProduceExpectedResultWithValueTaskDelegates()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(1);
        const int expected = 1;
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        int actual = await result.MatchAsync(
            success: (_, _) => ValueTask.FromResult(1),
            failure: _ => 0,
            cancellationToken
        );

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult<T>.MatchAsync Func<T, CancellationToken, ValueTask<TResult>>, Func<Exception, TResult> from success should produce the expected result when the delegates complete asynchronously.")]
    public async Task ValueResultMatchAsyncFuncTCancellationTokenTaskTResultFuncExceptionTResultFromSuccessShouldProduceExpectedResultWithValueTaskDelegatesWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(1);
        const int expected = 1;
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        int actual = await result.MatchAsync(
            success: (_, _) => Yielding.ValueTaskOf(1),
            failure: _ => 0,
            cancellationToken
        );

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult<T>.MatchAsync Func<T, CancellationToken, Task<TResult>>, Func<Exception, TResult> from failure should produce the expected result.")]
    public async Task ValueResultMatchAsyncFuncTCancellationTokenTaskTResultFuncExceptionTResultFromFailureShouldProduceExpectedResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        const int expected = 1;
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        int actual = await result.MatchAsync(
            success: (_, _) => Task.FromResult(0),
            failure: _ => 1,
            cancellationToken
        );

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult<T>.MatchAsync Func<T, CancellationToken, Task<TResult>>, Func<Exception, TResult> from failure should produce the expected result when the delegates complete asynchronously.")]
    public async Task ValueResultMatchAsyncFuncTCancellationTokenTaskTResultFuncExceptionTResultFromFailureShouldProduceExpectedResultWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        const int expected = 1;
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        int actual = await result.MatchAsync(
            success: (_, _) => Yielding.TaskOf(0),
            failure: _ => 1,
            cancellationToken
        );

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult<T>.MatchAsync Func<T, CancellationToken, ValueTask<TResult>>, Func<Exception, TResult> from failure should produce the expected result.")]
    public async Task ValueResultMatchAsyncFuncTCancellationTokenTaskTResultFuncExceptionTResultFromFailureShouldProduceExpectedResultWithValueTaskDelegates()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        const int expected = 1;
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        int actual = await result.MatchAsync(
            success: (_, _) => ValueTask.FromResult(0),
            failure: _ => 1,
            cancellationToken
        );

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult<T>.MatchAsync Func<T, CancellationToken, ValueTask<TResult>>, Func<Exception, TResult> from failure should produce the expected result when the delegates complete asynchronously.")]
    public async Task ValueResultMatchAsyncFuncTCancellationTokenTaskTResultFuncExceptionTResultFromFailureShouldProduceExpectedResultWithValueTaskDelegatesWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        const int expected = 1;
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        int actual = await result.MatchAsync(
            success: (_, _) => Yielding.ValueTaskOf(0),
            failure: _ => 1,
            cancellationToken
        );

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult<T>.MatchAsync Func<T, TResult>, Func<Exception, Task<TResult>> from success should produce the expected result.")]
    public async Task ValueResultMatchAsyncFuncTResultFuncExceptionTaskTResultFromSuccessShouldProduceExpectedResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(1);
        const int expected = 1;

        // When
        int actual = await result.MatchAsync(
            success: _ => 1,
            failure: _ => Task.FromResult(0),
            cancellationToken: TestContext.Current.CancellationToken
        );

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult<T>.MatchAsync Func<T, TResult>, Func<Exception, Task<TResult>> from success should produce the expected result when the delegates complete asynchronously.")]
    public async Task ValueResultMatchAsyncFuncTResultFuncExceptionTaskTResultFromSuccessShouldProduceExpectedResultWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(1);
        const int expected = 1;

        // When
        int actual = await result.MatchAsync(
            success: _ => 1,
            failure: _ => Yielding.TaskOf(0),
            cancellationToken: TestContext.Current.CancellationToken
        );

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult<T>.MatchAsync Func<T, TResult>, Func<Exception, ValueTask<TResult>> from success should produce the expected result.")]
    public async Task ValueResultMatchAsyncFuncTResultFuncExceptionTaskTResultFromSuccessShouldProduceExpectedResultWithValueTaskDelegates()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(1);
        const int expected = 1;

        // When
        int actual = await result.MatchAsync(
            success: _ => 1,
            failure: _ => ValueTask.FromResult(0),
            cancellationToken: TestContext.Current.CancellationToken
        );

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult<T>.MatchAsync Func<T, TResult>, Func<Exception, ValueTask<TResult>> from success should produce the expected result when the delegates complete asynchronously.")]
    public async Task ValueResultMatchAsyncFuncTResultFuncExceptionTaskTResultFromSuccessShouldProduceExpectedResultWithValueTaskDelegatesWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(1);
        const int expected = 1;

        // When
        int actual = await result.MatchAsync(
            success: _ => 1,
            failure: _ => Yielding.ValueTaskOf(0),
            cancellationToken: TestContext.Current.CancellationToken
        );

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult<T>.MatchAsync Func<T, TResult>, Func<Exception, Task<TResult>> from failure should produce the expected result.")]
    public async Task ValueResultMatchAsyncFuncTResultFuncExceptionTaskTResultFromFailureShouldProduceExpectedResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        const int expected = 1;

        // When
        int actual = await result.MatchAsync(
            success: _ => 0,
            failure: _ => Task.FromResult(1),
            cancellationToken: TestContext.Current.CancellationToken
        );

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult<T>.MatchAsync Func<T, TResult>, Func<Exception, Task<TResult>> from failure should produce the expected result when the delegates complete asynchronously.")]
    public async Task ValueResultMatchAsyncFuncTResultFuncExceptionTaskTResultFromFailureShouldProduceExpectedResultWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        const int expected = 1;

        // When
        int actual = await result.MatchAsync(
            success: _ => 0,
            failure: _ => Yielding.TaskOf(1),
            cancellationToken: TestContext.Current.CancellationToken
        );

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult<T>.MatchAsync Func<T, TResult>, Func<Exception, ValueTask<TResult>> from failure should produce the expected result.")]
    public async Task ValueResultMatchAsyncFuncTResultFuncExceptionTaskTResultFromFailureShouldProduceExpectedResultWithValueTaskDelegates()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        const int expected = 1;

        // When
        int actual = await result.MatchAsync(
            success: _ => 0,
            failure: _ => ValueTask.FromResult(1),
            cancellationToken: TestContext.Current.CancellationToken
        );

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult<T>.MatchAsync Func<T, TResult>, Func<Exception, ValueTask<TResult>> from failure should produce the expected result when the delegates complete asynchronously.")]
    public async Task ValueResultMatchAsyncFuncTResultFuncExceptionTaskTResultFromFailureShouldProduceExpectedResultWithValueTaskDelegatesWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        const int expected = 1;

        // When
        int actual = await result.MatchAsync(
            success: _ => 0,
            failure: _ => Yielding.ValueTaskOf(1),
            cancellationToken: TestContext.Current.CancellationToken
        );

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult<T>.MatchAsync Func<T, TResult>, Func<Exception, CancellationToken, Task<TResult>> from success should produce the expected result.")]
    public async Task ValueResultMatchAsyncFuncTResultFuncExceptionCancellationTokenTaskTResultFromSuccessShouldProduceExpectedResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(1);
        const int expected = 1;
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        int actual = await result.MatchAsync(
            success: _ => 1,
            failure: (_, _) => Task.FromResult(0),
            cancellationToken
        );

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult<T>.MatchAsync Func<T, TResult>, Func<Exception, CancellationToken, Task<TResult>> from success should produce the expected result when the delegates complete asynchronously.")]
    public async Task ValueResultMatchAsyncFuncTResultFuncExceptionCancellationTokenTaskTResultFromSuccessShouldProduceExpectedResultWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(1);
        const int expected = 1;
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        int actual = await result.MatchAsync(
            success: _ => 1,
            failure: (_, _) => Yielding.TaskOf(0),
            cancellationToken
        );

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult<T>.MatchAsync Func<T, TResult>, Func<Exception, CancellationToken, ValueTask<TResult>> from success should produce the expected result.")]
    public async Task ValueResultMatchAsyncFuncTResultFuncExceptionCancellationTokenTaskTResultFromSuccessShouldProduceExpectedResultWithValueTaskDelegates()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(1);
        const int expected = 1;
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        int actual = await result.MatchAsync(
            success: _ => 1,
            failure: (_, _) => ValueTask.FromResult(0),
            cancellationToken
        );

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult<T>.MatchAsync Func<T, TResult>, Func<Exception, CancellationToken, ValueTask<TResult>> from success should produce the expected result when the delegates complete asynchronously.")]
    public async Task ValueResultMatchAsyncFuncTResultFuncExceptionCancellationTokenTaskTResultFromSuccessShouldProduceExpectedResultWithValueTaskDelegatesWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(1);
        const int expected = 1;
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        int actual = await result.MatchAsync(
            success: _ => 1,
            failure: (_, _) => Yielding.ValueTaskOf(0),
            cancellationToken
        );

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult<T>.MatchAsync Func<T, TResult>, Func<Exception, CancellationToken, Task<TResult>> from failure should produce the expected result.")]
    public async Task ValueResultMatchAsyncFuncTResultFuncExceptionCancellationTokenTaskTResultFromFailureShouldProduceExpectedResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        const int expected = 1;
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        int actual = await result.MatchAsync(
            success: _ => 0,
            failure: (_, _) => Task.FromResult(1),
            cancellationToken
        );

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult<T>.MatchAsync Func<T, TResult>, Func<Exception, CancellationToken, Task<TResult>> from failure should produce the expected result when the delegates complete asynchronously.")]
    public async Task ValueResultMatchAsyncFuncTResultFuncExceptionCancellationTokenTaskTResultFromFailureShouldProduceExpectedResultWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        const int expected = 1;
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        int actual = await result.MatchAsync(
            success: _ => 0,
            failure: (_, _) => Yielding.TaskOf(1),
            cancellationToken
        );

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult<T>.MatchAsync Func<T, TResult>, Func<Exception, CancellationToken, ValueTask<TResult>> from failure should produce the expected result.")]
    public async Task ValueResultMatchAsyncFuncTResultFuncExceptionCancellationTokenTaskTResultFromFailureShouldProduceExpectedResultWithValueTaskDelegates()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        const int expected = 1;
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        int actual = await result.MatchAsync(
            success: _ => 0,
            failure: (_, _) => ValueTask.FromResult(1),
            cancellationToken
        );

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult<T>.MatchAsync Func<T, TResult>, Func<Exception, CancellationToken, ValueTask<TResult>> from failure should produce the expected result when the delegates complete asynchronously.")]
    public async Task ValueResultMatchAsyncFuncTResultFuncExceptionCancellationTokenTaskTResultFromFailureShouldProduceExpectedResultWithValueTaskDelegatesWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        const int expected = 1;
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        int actual = await result.MatchAsync(
            success: _ => 0,
            failure: (_, _) => Yielding.ValueTaskOf(1),
            cancellationToken
        );

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult<T>.MatchAsync Func<T, Task<TResult>>, Func<Exception, Task<TResult>> from success should produce the expected result.")]
    public async Task ValueResultMatchAsyncFuncTTaskTResultFuncExceptionTaskTResultFromSuccessShouldProduceExpectedResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(1);
        const int expected = 1;
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        int actual = await result.MatchAsync(
            success: _ => Task.FromResult(1),
            failure: _ => Task.FromResult(0),
            cancellationToken
        );

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult<T>.MatchAsync Func<T, Task<TResult>>, Func<Exception, Task<TResult>> from success should produce the expected result when the delegates complete asynchronously.")]
    public async Task ValueResultMatchAsyncFuncTTaskTResultFuncExceptionTaskTResultFromSuccessShouldProduceExpectedResultWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(1);
        const int expected = 1;
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        int actual = await result.MatchAsync(
            success: _ => Yielding.TaskOf(1),
            failure: _ => Yielding.TaskOf(0),
            cancellationToken
        );

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult<T>.MatchAsync Func<T, ValueTask<TResult>>, Func<Exception, ValueTask<TResult>> from success should produce the expected result.")]
    public async Task ValueResultMatchAsyncFuncTTaskTResultFuncExceptionTaskTResultFromSuccessShouldProduceExpectedResultWithValueTaskDelegates()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(1);
        const int expected = 1;
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        int actual = await result.MatchAsync(
            success: _ => ValueTask.FromResult(1),
            failure: _ => ValueTask.FromResult(0),
            cancellationToken
        );

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult<T>.MatchAsync Func<T, ValueTask<TResult>>, Func<Exception, ValueTask<TResult>> from success should produce the expected result when the delegates complete asynchronously.")]
    public async Task ValueResultMatchAsyncFuncTTaskTResultFuncExceptionTaskTResultFromSuccessShouldProduceExpectedResultWithValueTaskDelegatesWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(1);
        const int expected = 1;
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        int actual = await result.MatchAsync(
            success: _ => Yielding.ValueTaskOf(1),
            failure: _ => Yielding.ValueTaskOf(0),
            cancellationToken
        );

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult<T>.MatchAsync Func<T, Task<TResult>>, Func<Exception, Task<TResult>> from failure should produce the expected result.")]
    public async Task ValueResultMatchAsyncFuncTTaskTResultFuncExceptionTaskTResultFromFailureShouldProduceExpectedResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        const int expected = 1;
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        int actual = await result.MatchAsync(
            success: _ => Task.FromResult(0),
            failure: _ => Task.FromResult(1),
            cancellationToken
        );

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult<T>.MatchAsync Func<T, Task<TResult>>, Func<Exception, Task<TResult>> from failure should produce the expected result when the delegates complete asynchronously.")]
    public async Task ValueResultMatchAsyncFuncTTaskTResultFuncExceptionTaskTResultFromFailureShouldProduceExpectedResultWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        const int expected = 1;
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        int actual = await result.MatchAsync(
            success: _ => Yielding.TaskOf(0),
            failure: _ => Yielding.TaskOf(1),
            cancellationToken
        );

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult<T>.MatchAsync Func<T, ValueTask<TResult>>, Func<Exception, ValueTask<TResult>> from failure should produce the expected result.")]
    public async Task ValueResultMatchAsyncFuncTTaskTResultFuncExceptionTaskTResultFromFailureShouldProduceExpectedResultWithValueTaskDelegates()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        const int expected = 1;
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        int actual = await result.MatchAsync(
            success: _ => ValueTask.FromResult(0),
            failure: _ => ValueTask.FromResult(1),
            cancellationToken
        );

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult<T>.MatchAsync Func<T, ValueTask<TResult>>, Func<Exception, ValueTask<TResult>> from failure should produce the expected result when the delegates complete asynchronously.")]
    public async Task ValueResultMatchAsyncFuncTTaskTResultFuncExceptionTaskTResultFromFailureShouldProduceExpectedResultWithValueTaskDelegatesWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        const int expected = 1;
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        int actual = await result.MatchAsync(
            success: _ => Yielding.ValueTaskOf(0),
            failure: _ => Yielding.ValueTaskOf(1),
            cancellationToken
        );

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult<T>.MatchAsync Func<T, CancellationToken, Task<TResult>>, Func<Exception, CancellationToken, Task<TResult>> from success should produce the expected result.")]
    public async Task ValueResultMatchAsyncFuncTCancellationTokenTaskTResultFuncExceptionCancellationTokenTaskTResultFromSuccessShouldProduceExpectedResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(1);
        const int expected = 1;
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        int actual = await result.MatchAsync(
            success: (_, _) => Task.FromResult(1),
            failure: (_, _) => Task.FromResult(0),
            cancellationToken
        );

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult<T>.MatchAsync Func<T, CancellationToken, Task<TResult>>, Func<Exception, CancellationToken, Task<TResult>> from success should produce the expected result when the delegates complete asynchronously.")]
    public async Task ValueResultMatchAsyncFuncTCancellationTokenTaskTResultFuncExceptionCancellationTokenTaskTResultFromSuccessShouldProduceExpectedResultWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(1);
        const int expected = 1;
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        int actual = await result.MatchAsync(
            success: (_, _) => Yielding.TaskOf(1),
            failure: (_, _) => Yielding.TaskOf(0),
            cancellationToken
        );

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult<T>.MatchAsync Func<T, CancellationToken, ValueTask<TResult>>, Func<Exception, CancellationToken, ValueTask<TResult>> from success should produce the expected result.")]
    public async Task ValueResultMatchAsyncFuncTCancellationTokenTaskTResultFuncExceptionCancellationTokenTaskTResultFromSuccessShouldProduceExpectedResultWithValueTaskDelegates()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(1);
        const int expected = 1;
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        int actual = await result.MatchAsync(
            success: (_, _) => ValueTask.FromResult(1),
            failure: (_, _) => ValueTask.FromResult(0),
            cancellationToken
        );

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult<T>.MatchAsync Func<T, CancellationToken, ValueTask<TResult>>, Func<Exception, CancellationToken, ValueTask<TResult>> from success should produce the expected result when the delegates complete asynchronously.")]
    public async Task ValueResultMatchAsyncFuncTCancellationTokenTaskTResultFuncExceptionCancellationTokenTaskTResultFromSuccessShouldProduceExpectedResultWithValueTaskDelegatesWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(1);
        const int expected = 1;
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        int actual = await result.MatchAsync(
            success: (_, _) => Yielding.ValueTaskOf(1),
            failure: (_, _) => Yielding.ValueTaskOf(0),
            cancellationToken
        );

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult<T>.MatchAsync Func<T, CancellationToken, Task<TResult>>, Func<Exception, CancellationToken, Task<TResult>> from failure should produce the expected result.")]
    public async Task ValueResultMatchAsyncFuncTCancellationTokenTaskTResultFuncExceptionCancellationTokenTaskTResultFromFailureShouldProduceExpectedResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        const int expected = 1;
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        int actual = await result.MatchAsync(
            success: (_, _) => Task.FromResult(0),
            failure: (_, _) => Task.FromResult(1),
            cancellationToken
        );

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult<T>.MatchAsync Func<T, CancellationToken, Task<TResult>>, Func<Exception, CancellationToken, Task<TResult>> from failure should produce the expected result when the delegates complete asynchronously.")]
    public async Task ValueResultMatchAsyncFuncTCancellationTokenTaskTResultFuncExceptionCancellationTokenTaskTResultFromFailureShouldProduceExpectedResultWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        const int expected = 1;
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        int actual = await result.MatchAsync(
            success: (_, _) => Yielding.TaskOf(0),
            failure: (_, _) => Yielding.TaskOf(1),
            cancellationToken
        );

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult<T>.MatchAsync Func<T, CancellationToken, ValueTask<TResult>>, Func<Exception, CancellationToken, ValueTask<TResult>> from failure should produce the expected result.")]
    public async Task ValueResultMatchAsyncFuncTCancellationTokenTaskTResultFuncExceptionCancellationTokenTaskTResultFromFailureShouldProduceExpectedResultWithValueTaskDelegates()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        const int expected = 1;
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        int actual = await result.MatchAsync(
            success: (_, _) => ValueTask.FromResult(0),
            failure: (_, _) => ValueTask.FromResult(1),
            cancellationToken
        );

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult<T>.MatchAsync Func<T, CancellationToken, ValueTask<TResult>>, Func<Exception, CancellationToken, ValueTask<TResult>> from failure should produce the expected result when the delegates complete asynchronously.")]
    public async Task ValueResultMatchAsyncFuncTCancellationTokenTaskTResultFuncExceptionCancellationTokenTaskTResultFromFailureShouldProduceExpectedResultWithValueTaskDelegatesWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        const int expected = 1;
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        int actual = await result.MatchAsync(
            success: (_, _) => Yielding.ValueTaskOf(0),
            failure: (_, _) => Yielding.ValueTaskOf(1),
            cancellationToken
        );

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult<T>.Select action from failure should produce the expected result.")]
    public void ValueResultSelectActionFromFailureShouldProduceExpectedResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        ValueResult expected = ValueResult.Failure(FailureException);

        // Then
        ValueResult actual = result.Select(_ => { });

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult<T>.Select action from success should produce the expected result.")]
    public void ValueResultSelectActionFromSuccessShouldProduceExpectedResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(1);
        ValueResult expected = ValueResult.Success();

        // Then
        ValueResult actual = result.Select(_ => { });

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult<T>.Select action from success with exception should produce the expected result.")]
    public void ValueResultSelectActionFromSuccessWithExceptionShouldProduceExpectedResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(1);
        ValueResult expected = ValueResult.Failure(FailureException);

        // Then
        ValueResult actual = result.Select(_ => throw FailureException);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult<T>.SelectAsync Func<T, Task> from failure should produce the expected result.")]
    public async Task ValueResultSelectAsyncFuncTTaskFromFailureShouldProduceExpectedResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        ValueResult expected = ValueResult.Failure(FailureException);

        // When
        ValueResult actual = await result.SelectAsync(_ => Task.CompletedTask, TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult<T>.SelectAsync Func<T, Task> from failure should produce the expected result when the delegates complete asynchronously.")]
    public async Task ValueResultSelectAsyncFuncTTaskFromFailureShouldProduceExpectedResultWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        ValueResult expected = ValueResult.Failure(FailureException);

        // When
        ValueResult actual = await result.SelectAsync(_ => Yielding.CompletedTask(), TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult<T>.SelectAsync Func<T, ValueTask> from failure should produce the expected result.")]
    public async Task ValueResultSelectAsyncFuncTTaskFromFailureShouldProduceExpectedResultWithValueTaskDelegates()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        ValueResult expected = ValueResult.Failure(FailureException);

        // When
        ValueResult actual = await result.SelectAsync(_ => ValueTask.CompletedTask, TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult<T>.SelectAsync Func<T, ValueTask> from failure should produce the expected result when the delegates complete asynchronously.")]
    public async Task ValueResultSelectAsyncFuncTTaskFromFailureShouldProduceExpectedResultWithValueTaskDelegatesWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        ValueResult expected = ValueResult.Failure(FailureException);

        // When
        ValueResult actual = await result.SelectAsync(_ => Yielding.CompletedValueTask(), TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult<T>.SelectAsync Func<T, Task> from success should produce the expected result.")]
    public async Task ValueResultSelectAsyncFuncTTaskFromSuccessShouldProduceExpectedResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(1);
        ValueResult expected = ValueResult.Success();

        // When
        ValueResult actual = await result.SelectAsync(_ => Task.CompletedTask, TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult<T>.SelectAsync Func<T, Task> from success should produce the expected result when the delegates complete asynchronously.")]
    public async Task ValueResultSelectAsyncFuncTTaskFromSuccessShouldProduceExpectedResultWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(1);
        ValueResult expected = ValueResult.Success();

        // When
        ValueResult actual = await result.SelectAsync(_ => Yielding.CompletedTask(), TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult<T>.SelectAsync Func<T, ValueTask> from success should produce the expected result.")]
    public async Task ValueResultSelectAsyncFuncTTaskFromSuccessShouldProduceExpectedResultWithValueTaskDelegates()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(1);
        ValueResult expected = ValueResult.Success();

        // When
        ValueResult actual = await result.SelectAsync(_ => ValueTask.CompletedTask, TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult<T>.SelectAsync Func<T, ValueTask> from success should produce the expected result when the delegates complete asynchronously.")]
    public async Task ValueResultSelectAsyncFuncTTaskFromSuccessShouldProduceExpectedResultWithValueTaskDelegatesWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(1);
        ValueResult expected = ValueResult.Success();

        // When
        ValueResult actual = await result.SelectAsync(_ => Yielding.CompletedValueTask(), TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult<T>.SelectAsync Func<T, Task> from success with exception should produce the expected result.")]
    public async Task ValueResultSelectAsyncFuncTTaskFromSuccessWithExceptionShouldProduceExpectedResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(1);
        ValueResult expected = ValueResult.Failure(FailureException);

        // When
        ValueResult actual = await result.SelectAsync(Task (_) => throw FailureException, TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult<T>.SelectAsync Func<T, ValueTask> from success with exception should produce the expected result.")]
    public async Task ValueResultSelectAsyncFuncTTaskFromSuccessWithExceptionShouldProduceExpectedResultWithValueTaskDelegates()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(1);
        ValueResult expected = ValueResult.Failure(FailureException);

        // When
        ValueResult actual = await result.SelectAsync(ValueTask (_) => throw FailureException, TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult<T>.SelectAsync Func<T, CancellationToken, Task> from failure should produce the expected result.")]
    public async Task ValueResultSelectAsyncFuncTCancellationTokenTaskFromFailureShouldProduceExpectedResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        ValueResult expected = ValueResult.Failure(FailureException);
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        ValueResult actual = await result.SelectAsync((_, _) => Task.CompletedTask, cancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult<T>.SelectAsync Func<T, CancellationToken, Task> from failure should produce the expected result when the delegates complete asynchronously.")]
    public async Task ValueResultSelectAsyncFuncTCancellationTokenTaskFromFailureShouldProduceExpectedResultWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        ValueResult expected = ValueResult.Failure(FailureException);
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        ValueResult actual = await result.SelectAsync((_, _) => Yielding.CompletedTask(), cancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult<T>.SelectAsync Func<T, CancellationToken, ValueTask> from failure should produce the expected result.")]
    public async Task ValueResultSelectAsyncFuncTCancellationTokenTaskFromFailureShouldProduceExpectedResultWithValueTaskDelegates()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        ValueResult expected = ValueResult.Failure(FailureException);
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        ValueResult actual = await result.SelectAsync((_, _) => ValueTask.CompletedTask, cancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult<T>.SelectAsync Func<T, CancellationToken, ValueTask> from failure should produce the expected result when the delegates complete asynchronously.")]
    public async Task ValueResultSelectAsyncFuncTCancellationTokenTaskFromFailureShouldProduceExpectedResultWithValueTaskDelegatesWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        ValueResult expected = ValueResult.Failure(FailureException);
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        ValueResult actual = await result.SelectAsync((_, _) => Yielding.CompletedValueTask(), cancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult<T>.SelectAsync Func<T, CancellationToken, Task> from success should produce the expected result.")]
    public async Task ValueResultSelectAsyncFuncTCancellationTokenTaskFromSuccessShouldProduceExpectedResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(1);
        ValueResult expected = ValueResult.Success();
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        ValueResult actual = await result.SelectAsync((_, _) => Task.CompletedTask, cancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult<T>.SelectAsync Func<T, CancellationToken, Task> from success should produce the expected result when the delegates complete asynchronously.")]
    public async Task ValueResultSelectAsyncFuncTCancellationTokenTaskFromSuccessShouldProduceExpectedResultWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(1);
        ValueResult expected = ValueResult.Success();
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        ValueResult actual = await result.SelectAsync((_, _) => Yielding.CompletedTask(), cancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult<T>.SelectAsync Func<T, CancellationToken, ValueTask> from success should produce the expected result.")]
    public async Task ValueResultSelectAsyncFuncTCancellationTokenTaskFromSuccessShouldProduceExpectedResultWithValueTaskDelegates()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(1);
        ValueResult expected = ValueResult.Success();
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        ValueResult actual = await result.SelectAsync((_, _) => ValueTask.CompletedTask, cancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult<T>.SelectAsync Func<T, CancellationToken, ValueTask> from success should produce the expected result when the delegates complete asynchronously.")]
    public async Task ValueResultSelectAsyncFuncTCancellationTokenTaskFromSuccessShouldProduceExpectedResultWithValueTaskDelegatesWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(1);
        ValueResult expected = ValueResult.Success();
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        ValueResult actual = await result.SelectAsync((_, _) => Yielding.CompletedValueTask(), cancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult<T>.SelectAsync Func<T, CancellationToken, Task> from success with exception should produce the expected result.")]
    public async Task ValueResultSelectAsyncFuncTCancellationTokenTaskFromSuccessWithExceptionShouldProduceExpectedResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(1);
        ValueResult expected = ValueResult.Failure(FailureException);
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        ValueResult actual = await result.SelectAsync(Task (_, _) => throw FailureException, cancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult<T>.SelectAsync Func<T, CancellationToken, ValueTask> from success with exception should produce the expected result.")]
    public async Task ValueResultSelectAsyncFuncTCancellationTokenTaskFromSuccessWithExceptionShouldProduceExpectedResultWithValueTaskDelegates()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(1);
        ValueResult expected = ValueResult.Failure(FailureException);
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        ValueResult actual = await result.SelectAsync(ValueTask (_, _) => throw FailureException, cancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult<T>.Select function of TResult from failure should produce the expected result.")]
    public void ValueResultSelectFunctionOfTResultFromFailureShouldProduceExpectedResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        ValueResult<int> expected = ValueResult<int>.Failure(FailureException);

        // Then
        ValueResult<int> actual = result.Select(_ => 1);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult<T>.Select function of TResult from success should produce the expected result.")]
    public void ValueResultSelectFunctionOfTResultFromSuccessShouldProduceExpectedResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(2);
        ValueResult<int> expected = 1;

        // Then
        ValueResult<int> actual = result.Select(_ => 1);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult<T>.Select function of TResult from success with exception should produce the expected result.")]
    public void ValueResultSelectFunctionOfTResultFromSuccessWithExceptionShouldProduceExpectedResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(1);
        ValueResult<int> expected = ValueResult<int>.Failure(FailureException);

        // Then
        ValueResult<int> actual = result.Select<int>(_ => throw FailureException);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult<T>.SelectAsync function of TResult from failure should produce the expected result.")]
    public async Task ValueResultSelectAsyncFunctionOfTResultFromFailureShouldProduceExpectedResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        ValueResult<int> expected = ValueResult<int>.Failure(FailureException);

        // When
        ValueResult<int> actual = await result.SelectAsync(_ => Task.FromResult(1), TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult<T>.SelectAsync function of TResult from failure should produce the expected result when the delegates complete asynchronously.")]
    public async Task ValueResultSelectAsyncFunctionOfTResultFromFailureShouldProduceExpectedResultWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        ValueResult<int> expected = ValueResult<int>.Failure(FailureException);

        // When
        ValueResult<int> actual = await result.SelectAsync(_ => Yielding.TaskOf(1), TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult<T>.SelectAsync function of TResult from failure should produce the expected result with ValueTask delegates.")]
    public async Task ValueResultSelectAsyncFunctionOfTResultFromFailureShouldProduceExpectedResultWithValueTaskDelegates()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        ValueResult<int> expected = ValueResult<int>.Failure(FailureException);

        // When
        ValueResult<int> actual = await result.SelectAsync(_ => ValueTask.FromResult(1), TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult<T>.SelectAsync function of TResult from failure should produce the expected result with ValueTask delegates when the delegates complete asynchronously.")]
    public async Task ValueResultSelectAsyncFunctionOfTResultFromFailureShouldProduceExpectedResultWithValueTaskDelegatesWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        ValueResult<int> expected = ValueResult<int>.Failure(FailureException);

        // When
        ValueResult<int> actual = await result.SelectAsync(_ => Yielding.ValueTaskOf(1), TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult<T>.SelectAsync function of TResult from success should produce the expected result.")]
    public async Task ValueResultSelectAsyncFunctionOfTResultFromSuccessShouldProduceExpectedResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(2);
        ValueResult<int> expected = ValueResult<int>.Success(1);

        // When
        ValueResult<int> actual = await result.SelectAsync(_ => Task.FromResult(1), TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult<T>.SelectAsync function of TResult from success should produce the expected result when the delegates complete asynchronously.")]
    public async Task ValueResultSelectAsyncFunctionOfTResultFromSuccessShouldProduceExpectedResultWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(2);
        ValueResult<int> expected = ValueResult<int>.Success(1);

        // When
        ValueResult<int> actual = await result.SelectAsync(_ => Yielding.TaskOf(1), TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult<T>.SelectAsync function of TResult from success should produce the expected result with ValueTask delegates.")]
    public async Task ValueResultSelectAsyncFunctionOfTResultFromSuccessShouldProduceExpectedResultWithValueTaskDelegates()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(2);
        ValueResult<int> expected = ValueResult<int>.Success(1);

        // When
        ValueResult<int> actual = await result.SelectAsync(_ => ValueTask.FromResult(1), TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult<T>.SelectAsync function of TResult from success should produce the expected result with ValueTask delegates when the delegates complete asynchronously.")]
    public async Task ValueResultSelectAsyncFunctionOfTResultFromSuccessShouldProduceExpectedResultWithValueTaskDelegatesWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(2);
        ValueResult<int> expected = ValueResult<int>.Success(1);

        // When
        ValueResult<int> actual = await result.SelectAsync(_ => Yielding.ValueTaskOf(1), TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult<T>.SelectAsync function of TResult from success with exception should produce the expected result.")]
    public async Task ValueResultSelectAsyncFunctionOfTResultFromSuccessWithExceptionShouldProduceExpectedResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(1);
        ValueResult<int> expected = ValueResult<int>.Failure(FailureException);

        // When
        ValueResult<int> actual = await result.SelectAsync<int>(Task<int> (_) => throw FailureException, TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult<T>.SelectAsync function of TResult from success with exception should produce the expected result with ValueTask delegates.")]
    public async Task ValueResultSelectAsyncFunctionOfTResultFromSuccessWithExceptionShouldProduceExpectedResultWithValueTaskDelegates()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(1);
        ValueResult<int> expected = ValueResult<int>.Failure(FailureException);

        // When
        ValueResult<int> actual = await result.SelectAsync<int>(ValueTask<int> (_) => throw FailureException, TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult<T>.SelectAsync function of TResult with CancellationToken from failure should produce the expected result.")]
    public async Task ValueResultSelectAsyncFunctionOfTResultWithCancellationTokenFromFailureShouldProduceExpectedResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        ValueResult<int> expected = ValueResult<int>.Failure(FailureException);
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        ValueResult<int> actual = await result.SelectAsync((_, _) => Task.FromResult(1), cancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult<T>.SelectAsync function of TResult with CancellationToken from failure should produce the expected result when the delegates complete asynchronously.")]
    public async Task ValueResultSelectAsyncFunctionOfTResultWithCancellationTokenFromFailureShouldProduceExpectedResultWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        ValueResult<int> expected = ValueResult<int>.Failure(FailureException);
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        ValueResult<int> actual = await result.SelectAsync((_, _) => Yielding.TaskOf(1), cancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult<T>.SelectAsync function of TResult with CancellationToken from failure should produce the expected result with ValueTask delegates.")]
    public async Task ValueResultSelectAsyncFunctionOfTResultWithCancellationTokenFromFailureShouldProduceExpectedResultWithValueTaskDelegates()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        ValueResult<int> expected = ValueResult<int>.Failure(FailureException);
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        ValueResult<int> actual = await result.SelectAsync((_, _) => ValueTask.FromResult(1), cancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult<T>.SelectAsync function of TResult with CancellationToken from failure should produce the expected result with ValueTask delegates when the delegates complete asynchronously.")]
    public async Task ValueResultSelectAsyncFunctionOfTResultWithCancellationTokenFromFailureShouldProduceExpectedResultWithValueTaskDelegatesWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        ValueResult<int> expected = ValueResult<int>.Failure(FailureException);
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        ValueResult<int> actual = await result.SelectAsync((_, _) => Yielding.ValueTaskOf(1), cancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult<T>.SelectAsync function of TResult with CancellationToken from success should produce the expected result.")]
    public async Task ValueResultSelectAsyncFunctionOfTResultWithCancellationTokenFromSuccessShouldProduceExpectedResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(2);
        ValueResult<int> expected = ValueResult<int>.Success(1);
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        ValueResult<int> actual = await result.SelectAsync((_, _) => Task.FromResult(1), cancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult<T>.SelectAsync function of TResult with CancellationToken from success should produce the expected result when the delegates complete asynchronously.")]
    public async Task ValueResultSelectAsyncFunctionOfTResultWithCancellationTokenFromSuccessShouldProduceExpectedResultWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(2);
        ValueResult<int> expected = ValueResult<int>.Success(1);
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        ValueResult<int> actual = await result.SelectAsync((_, _) => Yielding.TaskOf(1), cancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult<T>.SelectAsync function of TResult with CancellationToken from success should produce the expected result with ValueTask delegates.")]
    public async Task ValueResultSelectAsyncFunctionOfTResultWithCancellationTokenFromSuccessShouldProduceExpectedResultWithValueTaskDelegates()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(2);
        ValueResult<int> expected = ValueResult<int>.Success(1);
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        ValueResult<int> actual = await result.SelectAsync((_, _) => ValueTask.FromResult(1), cancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult<T>.SelectAsync function of TResult with CancellationToken from success should produce the expected result with ValueTask delegates when the delegates complete asynchronously.")]
    public async Task ValueResultSelectAsyncFunctionOfTResultWithCancellationTokenFromSuccessShouldProduceExpectedResultWithValueTaskDelegatesWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(2);
        ValueResult<int> expected = ValueResult<int>.Success(1);
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        ValueResult<int> actual = await result.SelectAsync((_, _) => Yielding.ValueTaskOf(1), cancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult<T>.SelectAsync function of TResult with CancellationToken from success with exception should produce the expected result.")]
    public async Task ValueResultSelectAsyncFunctionOfTResultWithCancellationTokenFromSuccessWithExceptionShouldProduceExpectedResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(1);
        ValueResult<int> expected = ValueResult<int>.Failure(FailureException);
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        ValueResult<int> actual = await result.SelectAsync<int>(Task<int> (_, _) => throw FailureException, cancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult<T>.SelectAsync function of TResult with CancellationToken from success with exception should produce the expected result with ValueTask delegates.")]
    public async Task ValueResultSelectAsyncFunctionOfTResultWithCancellationTokenFromSuccessWithExceptionShouldProduceExpectedResultWithValueTaskDelegates()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(1);
        ValueResult<int> expected = ValueResult<int>.Failure(FailureException);
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        ValueResult<int> actual = await result.SelectAsync<int>(ValueTask<int> (_, _) => throw FailureException, cancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult<T>.SelectMany function from failure should produce the expected result.")]
    public void ValueResultSelectManyFunctionFromFailureShouldProduceExpectedResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        ValueResult expected = ValueResult.Failure(FailureException);

        // When
        ValueResult actual = result.SelectMany(_ => ValueResult.Success());

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult<T>.SelectMany function from success should produce the expected result.")]
    public void ValueResultSelectManyFunctionFromSuccessShouldProduceExpectedResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(1);
        ValueResult expected = ValueResult.Success();

        // When
        ValueResult actual = result.SelectMany(_ => ValueResult.Success());

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult<T>.SelectMany function from success with exception should produce the expected result.")]
    public void ValueResultSelectManyFunctionFromSuccessWithExceptionShouldProduceExpectedResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(1);
        ValueResult expected = ValueResult.Failure(FailureException);

        // When
        ValueResult actual = result.SelectMany(_ => throw FailureException);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult<T>.SelectManyAsync function from failure should produce the expected result.")]
    public async Task ValueResultSelectManyAsyncFunctionFromFailureShouldProduceExpectedResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        ValueResult expected = ValueResult.Failure(FailureException);

        // When
        ValueResult actual = await result.SelectManyAsync(async Task<ValueResult> (_) =>
            await Task.FromResult(ValueResult.Success()), TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult<T>.SelectManyAsync function from failure should produce the expected result when the delegates complete asynchronously.")]
    public async Task ValueResultSelectManyAsyncFunctionFromFailureShouldProduceExpectedResultWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        ValueResult expected = ValueResult.Failure(FailureException);

        // When
        ValueResult actual = await result.SelectManyAsync(async Task<ValueResult> (_) =>
            await Yielding.TaskOf(ValueResult.Success()), TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult<T>.SelectManyAsync function from failure should produce the expected result with ValueTask delegates.")]
    public async Task ValueResultSelectManyAsyncFunctionFromFailureShouldProduceExpectedResultWithValueTaskDelegates()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        ValueResult expected = ValueResult.Failure(FailureException);

        // When
        ValueResult actual = await result.SelectManyAsync(async ValueTask<ValueResult> (_) =>
            await ValueTask.FromResult(ValueResult.Success()), TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult<T>.SelectManyAsync function from failure should produce the expected result with ValueTask delegates when the delegates complete asynchronously.")]
    public async Task ValueResultSelectManyAsyncFunctionFromFailureShouldProduceExpectedResultWithValueTaskDelegatesWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        ValueResult expected = ValueResult.Failure(FailureException);

        // When
        ValueResult actual = await result.SelectManyAsync(async ValueTask<ValueResult> (_) =>
            await Yielding.ValueTaskOf(ValueResult.Success()), TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult<T>.SelectManyAsync function from success should produce the expected result.")]
    public async Task ValueResultSelectManyAsyncFunctionFromSuccessShouldProduceExpectedResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(1);
        ValueResult expected = ValueResult.Success();

        // When
        ValueResult actual = await result.SelectManyAsync(async Task<ValueResult> (_) =>
            await Task.FromResult(ValueResult.Success()), TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult<T>.SelectManyAsync function from success should produce the expected result when the delegates complete asynchronously.")]
    public async Task ValueResultSelectManyAsyncFunctionFromSuccessShouldProduceExpectedResultWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(1);
        ValueResult expected = ValueResult.Success();

        // When
        ValueResult actual = await result.SelectManyAsync(async Task<ValueResult> (_) =>
            await Yielding.TaskOf(ValueResult.Success()), TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult<T>.SelectManyAsync function from success should produce the expected result with ValueTask delegates.")]
    public async Task ValueResultSelectManyAsyncFunctionFromSuccessShouldProduceExpectedResultWithValueTaskDelegates()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(1);
        ValueResult expected = ValueResult.Success();

        // When
        ValueResult actual = await result.SelectManyAsync(async ValueTask<ValueResult> (_) =>
            await ValueTask.FromResult(ValueResult.Success()), TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult<T>.SelectManyAsync function from success should produce the expected result with ValueTask delegates when the delegates complete asynchronously.")]
    public async Task ValueResultSelectManyAsyncFunctionFromSuccessShouldProduceExpectedResultWithValueTaskDelegatesWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(1);
        ValueResult expected = ValueResult.Success();

        // When
        ValueResult actual = await result.SelectManyAsync(async ValueTask<ValueResult> (_) =>
            await Yielding.ValueTaskOf(ValueResult.Success()), TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult<T>.SelectManyAsync function from success with exception should produce the expected result.")]
    public async Task ValueResultSelectManyAsyncFunctionFromSuccessWithExceptionShouldProduceExpectedResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(1);
        ValueResult expected = ValueResult.Failure(FailureException);

        // When
        ValueResult actual = await result.SelectManyAsync(Task<ValueResult> (_) => throw FailureException, TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult<T>.SelectManyAsync function from success with exception should produce the expected result with ValueTask delegates.")]
    public async Task ValueResultSelectManyAsyncFunctionFromSuccessWithExceptionShouldProduceExpectedResultWithValueTaskDelegates()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(1);
        ValueResult expected = ValueResult.Failure(FailureException);

        // When
        ValueResult actual = await result.SelectManyAsync(ValueTask<ValueResult> (_) => throw FailureException, TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult<T>.SelectManyAsync function with CancellationToken from failure should produce the expected result.")]
    public async Task ValueResultSelectManyAsyncFunctionWithCancellationTokenFromFailureShouldProduceExpectedResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        ValueResult expected = ValueResult.Failure(FailureException);
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        ValueResult actual = await result.SelectManyAsync(async Task<ValueResult> (_, _) => await Task.FromResult(ValueResult.Success()), cancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult<T>.SelectManyAsync function with CancellationToken from failure should produce the expected result when the delegates complete asynchronously.")]
    public async Task ValueResultSelectManyAsyncFunctionWithCancellationTokenFromFailureShouldProduceExpectedResultWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        ValueResult expected = ValueResult.Failure(FailureException);
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        ValueResult actual = await result.SelectManyAsync(async Task<ValueResult> (_, _) => await Yielding.TaskOf(ValueResult.Success()), cancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult<T>.SelectManyAsync function with CancellationToken from failure should produce the expected result with ValueTask delegates.")]
    public async Task ValueResultSelectManyAsyncFunctionWithCancellationTokenFromFailureShouldProduceExpectedResultWithValueTaskDelegates()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        ValueResult expected = ValueResult.Failure(FailureException);
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        ValueResult actual = await result.SelectManyAsync(async ValueTask<ValueResult> (_, _) => await ValueTask.FromResult(ValueResult.Success()), cancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult<T>.SelectManyAsync function with CancellationToken from failure should produce the expected result with ValueTask delegates when the delegates complete asynchronously.")]
    public async Task ValueResultSelectManyAsyncFunctionWithCancellationTokenFromFailureShouldProduceExpectedResultWithValueTaskDelegatesWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        ValueResult expected = ValueResult.Failure(FailureException);
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        ValueResult actual = await result.SelectManyAsync(async ValueTask<ValueResult> (_, _) => await Yielding.ValueTaskOf(ValueResult.Success()), cancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult<T>.SelectManyAsync function with CancellationToken from success should produce the expected result.")]
    public async Task ValueResultSelectManyAsyncFunctionWithCancellationTokenFromSuccessShouldProduceExpectedResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(1);
        ValueResult expected = ValueResult.Success();
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        ValueResult actual = await result.SelectManyAsync(async Task<ValueResult> (_, _) => await Task.FromResult(ValueResult.Success()), cancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult<T>.SelectManyAsync function with CancellationToken from success should produce the expected result when the delegates complete asynchronously.")]
    public async Task ValueResultSelectManyAsyncFunctionWithCancellationTokenFromSuccessShouldProduceExpectedResultWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(1);
        ValueResult expected = ValueResult.Success();
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        ValueResult actual = await result.SelectManyAsync(async Task<ValueResult> (_, _) => await Yielding.TaskOf(ValueResult.Success()), cancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult<T>.SelectManyAsync function with CancellationToken from success should produce the expected result with ValueTask delegates.")]
    public async Task ValueResultSelectManyAsyncFunctionWithCancellationTokenFromSuccessShouldProduceExpectedResultWithValueTaskDelegates()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(1);
        ValueResult expected = ValueResult.Success();
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        ValueResult actual = await result.SelectManyAsync(async ValueTask<ValueResult> (_, _) => await ValueTask.FromResult(ValueResult.Success()), cancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult<T>.SelectManyAsync function with CancellationToken from success should produce the expected result with ValueTask delegates when the delegates complete asynchronously.")]
    public async Task ValueResultSelectManyAsyncFunctionWithCancellationTokenFromSuccessShouldProduceExpectedResultWithValueTaskDelegatesWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(1);
        ValueResult expected = ValueResult.Success();
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        ValueResult actual = await result.SelectManyAsync(async ValueTask<ValueResult> (_, _) => await Yielding.ValueTaskOf(ValueResult.Success()), cancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult<T>.SelectManyAsync function with CancellationToken from success with exception should produce the expected result.")]
    public async Task ValueResultSelectManyAsyncFunctionWithCancellationTokenFromSuccessWithExceptionShouldProduceExpectedResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(1);
        ValueResult expected = ValueResult.Failure(FailureException);
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        ValueResult actual = await result.SelectManyAsync(Task<ValueResult> (_, _) => throw FailureException, cancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult<T>.SelectManyAsync function with CancellationToken from success with exception should produce the expected result with ValueTask delegates.")]
    public async Task ValueResultSelectManyAsyncFunctionWithCancellationTokenFromSuccessWithExceptionShouldProduceExpectedResultWithValueTaskDelegates()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(1);
        ValueResult expected = ValueResult.Failure(FailureException);
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        ValueResult actual = await result.SelectManyAsync(ValueTask<ValueResult> (_, _) => throw FailureException, cancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult<T>.SelectMany function of TResult from failure should produce the expected result.")]
    public void ValueResultSelectManyFunctionOfTResultFromFailureShouldProduceExpectedResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        ValueResult<int> expected = ValueResult<int>.Failure(FailureException);

        // When
        ValueResult<int> actual = result.SelectMany<int>(_ => 1);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult<T>.SelectMany function of TResult from success should produce the expected result.")]
    public void ValueResultSelectManyFunctionOfTResultFromSuccessShouldProduceExpectedResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(2);
        ValueResult<int> expected = 1;

        // When
        ValueResult<int> actual = result.SelectMany<int>(_ => 1);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult<T>.SelectMany function of TResult from success with exception should produce the expected result.")]
    public void ValueResultSelectManyFunctionOfTResultFromSuccessWithExceptionShouldProduceExpectedResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(1);
        ValueResult<int> expected = ValueResult<int>.Failure(FailureException);

        // When
        ValueResult<int> actual = result.SelectMany<int>(_ => throw FailureException);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult<T>.SelectManyAsync function of TResult from failure should produce the expected result.")]
    public async Task ValueResultSelectManyAsyncFunctionOfTResultFromFailureShouldProduceExpectedResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        ValueResult<int> expected = ValueResult<int>.Failure(FailureException);

        // When
        ValueResult<int> actual = await result.SelectManyAsync<int>(async Task<ValueResult<int>> (_) =>
            await Task.FromResult(ValueResult<int>.Success(1)), TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult<T>.SelectManyAsync function of TResult from failure should produce the expected result when the delegates complete asynchronously.")]
    public async Task ValueResultSelectManyAsyncFunctionOfTResultFromFailureShouldProduceExpectedResultWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        ValueResult<int> expected = ValueResult<int>.Failure(FailureException);

        // When
        ValueResult<int> actual = await result.SelectManyAsync<int>(async Task<ValueResult<int>> (_) =>
            await Yielding.TaskOf(ValueResult<int>.Success(1)), TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult<T>.SelectManyAsync function of TResult from failure should produce the expected result with ValueTask delegates.")]
    public async Task ValueResultSelectManyAsyncFunctionOfTResultFromFailureShouldProduceExpectedResultWithValueTaskDelegates()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        ValueResult<int> expected = ValueResult<int>.Failure(FailureException);

        // When
        ValueResult<int> actual = await result.SelectManyAsync<int>(async ValueTask<ValueResult<int>> (_) =>
            await ValueTask.FromResult(ValueResult<int>.Success(1)), TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult<T>.SelectManyAsync function of TResult from failure should produce the expected result with ValueTask delegates when the delegates complete asynchronously.")]
    public async Task ValueResultSelectManyAsyncFunctionOfTResultFromFailureShouldProduceExpectedResultWithValueTaskDelegatesWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        ValueResult<int> expected = ValueResult<int>.Failure(FailureException);

        // When
        ValueResult<int> actual = await result.SelectManyAsync<int>(async ValueTask<ValueResult<int>> (_) =>
            await Yielding.ValueTaskOf(ValueResult<int>.Success(1)), TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult<T>.SelectManyAsync function of TResult from success should produce the expected result.")]
    public async Task ValueResultSelectManyAsyncFunctionOfTResultFromSuccessShouldProduceExpectedResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(2);
        ValueResult<int> expected = ValueResult<int>.Success(1);

        // When
        ValueResult<int> actual = await result.SelectManyAsync<int>(async Task<ValueResult<int>> (_) =>
            await Task.FromResult(ValueResult<int>.Success(1)), TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult<T>.SelectManyAsync function of TResult from success should produce the expected result when the delegates complete asynchronously.")]
    public async Task ValueResultSelectManyAsyncFunctionOfTResultFromSuccessShouldProduceExpectedResultWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(2);
        ValueResult<int> expected = ValueResult<int>.Success(1);

        // When
        ValueResult<int> actual = await result.SelectManyAsync<int>(async Task<ValueResult<int>> (_) =>
            await Yielding.TaskOf(ValueResult<int>.Success(1)), TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult<T>.SelectManyAsync function of TResult from success should produce the expected result with ValueTask delegates.")]
    public async Task ValueResultSelectManyAsyncFunctionOfTResultFromSuccessShouldProduceExpectedResultWithValueTaskDelegates()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(2);
        ValueResult<int> expected = ValueResult<int>.Success(1);

        // When
        ValueResult<int> actual = await result.SelectManyAsync<int>(async ValueTask<ValueResult<int>> (_) =>
            await ValueTask.FromResult(ValueResult<int>.Success(1)), TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult<T>.SelectManyAsync function of TResult from success should produce the expected result with ValueTask delegates when the delegates complete asynchronously.")]
    public async Task ValueResultSelectManyAsyncFunctionOfTResultFromSuccessShouldProduceExpectedResultWithValueTaskDelegatesWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(2);
        ValueResult<int> expected = ValueResult<int>.Success(1);

        // When
        ValueResult<int> actual = await result.SelectManyAsync<int>(async ValueTask<ValueResult<int>> (_) =>
            await Yielding.ValueTaskOf(ValueResult<int>.Success(1)), TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult<T>.SelectManyAsync function of TResult from success with exception should produce the expected result.")]
    public async Task ValueResultSelectManyAsyncFunctionOfTResultFromSuccessWithExceptionShouldProduceExpectedResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(1);
        ValueResult<int> expected = ValueResult<int>.Failure(FailureException);

        // When
        ValueResult<int> actual = await result.SelectManyAsync<int>(Task<ValueResult<int>> (_) => throw FailureException, TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult<T>.SelectManyAsync function of TResult from success with exception should produce the expected result with ValueTask delegates.")]
    public async Task ValueResultSelectManyAsyncFunctionOfTResultFromSuccessWithExceptionShouldProduceExpectedResultWithValueTaskDelegates()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(1);
        ValueResult<int> expected = ValueResult<int>.Failure(FailureException);

        // When
        ValueResult<int> actual = await result.SelectManyAsync<int>(ValueTask<ValueResult<int>> (_) => throw FailureException, TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult<T>.SelectManyAsync function of TResult with CancellationToken from failure should produce the expected result.")]
    public async Task ValueResultSelectManyAsyncFunctionOfTResultWithCancellationTokenFromFailureShouldProduceExpectedResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        ValueResult<int> expected = ValueResult<int>.Failure(FailureException);
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        ValueResult<int> actual = await result.SelectManyAsync<int>(async Task<ValueResult<int>> (_, _) => await Task.FromResult(ValueResult<int>.Success(1)), cancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult<T>.SelectManyAsync function of TResult with CancellationToken from failure should produce the expected result when the delegates complete asynchronously.")]
    public async Task ValueResultSelectManyAsyncFunctionOfTResultWithCancellationTokenFromFailureShouldProduceExpectedResultWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        ValueResult<int> expected = ValueResult<int>.Failure(FailureException);
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        ValueResult<int> actual = await result.SelectManyAsync<int>(async Task<ValueResult<int>> (_, _) => await Yielding.TaskOf(ValueResult<int>.Success(1)), cancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult<T>.SelectManyAsync function of TResult with CancellationToken from failure should produce the expected result with ValueTask delegates.")]
    public async Task ValueResultSelectManyAsyncFunctionOfTResultWithCancellationTokenFromFailureShouldProduceExpectedResultWithValueTaskDelegates()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        ValueResult<int> expected = ValueResult<int>.Failure(FailureException);
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        ValueResult<int> actual = await result.SelectManyAsync<int>(async ValueTask<ValueResult<int>> (_, _) => await ValueTask.FromResult(ValueResult<int>.Success(1)), cancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult<T>.SelectManyAsync function of TResult with CancellationToken from failure should produce the expected result with ValueTask delegates when the delegates complete asynchronously.")]
    public async Task ValueResultSelectManyAsyncFunctionOfTResultWithCancellationTokenFromFailureShouldProduceExpectedResultWithValueTaskDelegatesWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        ValueResult<int> expected = ValueResult<int>.Failure(FailureException);
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        ValueResult<int> actual = await result.SelectManyAsync<int>(async ValueTask<ValueResult<int>> (_, _) => await Yielding.ValueTaskOf(ValueResult<int>.Success(1)), cancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult<T>.SelectManyAsync function of TResult with CancellationToken from success should produce the expected result.")]
    public async Task ValueResultSelectManyAsyncFunctionOfTResultWithCancellationTokenFromSuccessShouldProduceExpectedResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(2);
        ValueResult<int> expected = ValueResult<int>.Success(1);
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        ValueResult<int> actual = await result.SelectManyAsync<int>(async Task<ValueResult<int>> (_, _) => await Task.FromResult(ValueResult<int>.Success(1)), cancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult<T>.SelectManyAsync function of TResult with CancellationToken from success should produce the expected result when the delegates complete asynchronously.")]
    public async Task ValueResultSelectManyAsyncFunctionOfTResultWithCancellationTokenFromSuccessShouldProduceExpectedResultWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(2);
        ValueResult<int> expected = ValueResult<int>.Success(1);
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        ValueResult<int> actual = await result.SelectManyAsync<int>(async Task<ValueResult<int>> (_, _) => await Yielding.TaskOf(ValueResult<int>.Success(1)), cancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult<T>.SelectManyAsync function of TResult with CancellationToken from success should produce the expected result with ValueTask delegates.")]
    public async Task ValueResultSelectManyAsyncFunctionOfTResultWithCancellationTokenFromSuccessShouldProduceExpectedResultWithValueTaskDelegates()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(2);
        ValueResult<int> expected = ValueResult<int>.Success(1);
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        ValueResult<int> actual = await result.SelectManyAsync<int>(async ValueTask<ValueResult<int>> (_, _) => await ValueTask.FromResult(ValueResult<int>.Success(1)), cancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult<T>.SelectManyAsync function of TResult with CancellationToken from success should produce the expected result with ValueTask delegates when the delegates complete asynchronously.")]
    public async Task ValueResultSelectManyAsyncFunctionOfTResultWithCancellationTokenFromSuccessShouldProduceExpectedResultWithValueTaskDelegatesWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(2);
        ValueResult<int> expected = ValueResult<int>.Success(1);
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        ValueResult<int> actual = await result.SelectManyAsync<int>(async ValueTask<ValueResult<int>> (_, _) => await Yielding.ValueTaskOf(ValueResult<int>.Success(1)), cancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult<T>.SelectManyAsync function of TResult with CancellationToken from success with exception should produce the expected result.")]
    public async Task ValueResultSelectManyAsyncFunctionOfTResultWithCancellationTokenFromSuccessWithExceptionShouldProduceExpectedResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(1);
        ValueResult<int> expected = ValueResult<int>.Failure(FailureException);
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        ValueResult<int> actual = await result.SelectManyAsync<int>(Task<ValueResult<int>> (_, _) => throw FailureException, cancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult<T>.SelectManyAsync function of TResult with CancellationToken from success with exception should produce the expected result with ValueTask delegates.")]
    public async Task ValueResultSelectManyAsyncFunctionOfTResultWithCancellationTokenFromSuccessWithExceptionShouldProduceExpectedResultWithValueTaskDelegates()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(1);
        ValueResult<int> expected = ValueResult<int>.Failure(FailureException);
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        ValueResult<int> actual = await result.SelectManyAsync<int>(ValueTask<ValueResult<int>> (_, _) => throw FailureException, cancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult<T>.Throw from success should produce expected result.")]
    public void ValueResultThrowFromSuccessShouldProduceExpectedResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(1);

        // When / Then
        result.Throw();
    }

    [Fact(DisplayName = "ValueResult<T>.Throw from failure should produce expected result.")]
    public void ValueResultThrowFromFailureShouldProduceExpectedResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);

        // When
        Exception exception = Assert.Throws<Exception>(() => result.Throw());

        // Then
        Assert.Equal(FailureException, exception);
    }

    [Fact(DisplayName = "ValueResult<T>.ToString from success should produce expected result.")]
    public void ValueResultToStringFromSuccessShouldProduceExpectedResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(1);
        const string expected = "1";

        // When
        string actual = result.ToString();

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult<T>.ToString from failure should produce expected result.")]
    public void ValueResultToStringFromFailureShouldProduceExpectedResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        string expected = FailureException.Message;

        // When
        string actual = result.ToString();

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult<T> default should be a failure reporting that it was never initialised")]
    public void ValueResultDefaultShouldBeFailureReportingThatItWasNeverInitialised()
    {
        // Given
        ValueResult<int>[] results = new ValueResult<int>[1];

        // When
        ValueResult<int> result = results[0];

        // Then
        Assert.True(result.IsFailure);
        InvalidOperationException exception = Assert.IsType<InvalidOperationException>(result.GetExceptionOrDefault());
        Assert.Contains("never initialised", exception.Message);
        Assert.Same(exception, Assert.Throws<InvalidOperationException>(() => result.GetValueOrThrow()));
        Assert.Equal(default(ValueResult<int>), result);
    }

    [Fact(DisplayName = "ValueResult<T>.Success should allow a null value when T is nullable")]
    public void ValueResultSuccessShouldAllowNullValueWhenTIsNullable()
    {
        // Given / When
        ValueResult<string?> result = ValueResult<string?>.Success(null);

        // Then
        Assert.True(result.IsSuccess);
        Assert.Null(result.GetValueOrThrow());
    }

    [Fact(DisplayName = "ValueResult<T>.Failure with a null exception should throw ArgumentNullException")]
    public void ValueResultFailureWithNullExceptionShouldThrowArgumentNullException()
    {
        // Given / When / Then
        Assert.Throws<ArgumentNullException>(() => ValueResult<int>.Failure(null!));
    }

    [Fact(DisplayName = "ValueResult<T> implicit and explicit conversions should produce the expected result")]
    public void ValueResultImplicitAndExplicitConversionsShouldProduceExpectedResult()
    {
        // Given / When
        ValueResult<int> success = 123;
        ValueResult<int> failure = FailureException;

        // Then
        Assert.Equal(123, (int)success);
        Assert.Same(FailureException, failure.GetExceptionOrDefault());
        Assert.Same(FailureException, Assert.Throws<Exception>(() => (int)failure));
    }

    [Fact(DisplayName = "ValueResult<T> exception getters should produce the expected result")]
    public void ValueResultExceptionGettersShouldProduceExpectedResult()
    {
        // Given
        ValueResult<int> success = ValueResult<int>.Success(123);
        ValueResult<int> failure = ValueResult<int>.Failure(FailureException);
        Exception defaultException = new("Default");

        // When / Then
        Assert.Null(success.GetExceptionOrDefault());
        Assert.Same(FailureException, failure.GetExceptionOrDefault());
        Assert.Same(defaultException, success.GetExceptionOrDefault(defaultException));
        Assert.Same(FailureException, failure.GetExceptionOrDefault(defaultException));
        Assert.Equal(ValueOptional<Exception>.None, success.GetExceptionOrNone());
        Assert.Equal(ValueOptional<Exception>.Some(FailureException), failure.GetExceptionOrNone());
        Assert.Same(FailureException, failure.GetExceptionOrThrow());
        Assert.Throws<InvalidOperationException>(() => success.GetExceptionOrThrow());
    }

    [Fact(DisplayName = "ValueResult<T> value getters should produce the expected result")]
    public void ValueResultValueGettersShouldProduceExpectedResult()
    {
        // Given
        ValueResult<int> success = ValueResult<int>.Success(123);
        ValueResult<int> failure = ValueResult<int>.Failure(FailureException);

        // When / Then
        Assert.Equal(123, success.GetValueOrDefault());
        Assert.Equal(0, failure.GetValueOrDefault());
        Assert.Equal(123, success.GetValueOrDefault(456));
        Assert.Equal(456, failure.GetValueOrDefault(456));
        Assert.Equal(123, success.GetValueOrThrow());
        Assert.Same(FailureException, Assert.Throws<Exception>(() => failure.GetValueOrThrow()));
    }

    [Fact(DisplayName = "ValueResult<T>.Match should invoke the delegate that matches the state")]
    public void ValueResultMatchShouldInvokeTheDelegateThatMatchesTheState()
    {
        // Given
        ValueResult<int> success = ValueResult<int>.Success(123);
        ValueResult<int> failure = ValueResult<int>.Failure(FailureException);
        string matched = string.Empty;

        // When / Then
        success.Match(success: value => matched = $"Value: {value}", failure: exception => matched = exception.Message);
        Assert.Equal("Value: 123", matched);

        failure.Match(success: value => matched = $"Value: {value}", failure: exception => matched = exception.Message);
        Assert.Equal("Failure", matched);

        Assert.Equal("Value: 123", success.Match(success: value => $"Value: {value}", failure: exception => exception.Message));
        Assert.Equal("Failure", failure.Match(success: value => $"Value: {value}", failure: exception => exception.Message));
    }

    [Fact(DisplayName = "ValueResult<T> switch expression should match the value of a success and the exception of a failure")]
    public void ValueResultSwitchExpressionShouldMatchTheValueOfSuccessAndTheExceptionOfFailure()
    {
        // Given
        ValueResult<int> success = ValueResult<int>.Success(123);
        ValueResult<int> failure = ValueResult<int>.Failure(FailureException);
        ValueResult<int> uninitialised = default;

        // When / Then
        Assert.Equal("Value: 123", Describe(success));
        Assert.Equal("Failure", Describe(failure));
        Assert.Contains("never initialised", Describe(uninitialised));

        // No discard or null arm is needed: neither case type is nullable, and default is a failure.
        static string Describe(ValueResult<int> result) => result switch
        {
            int value => $"Value: {value}",
            Exception exception => exception.Message
        };
    }

    [Fact(DisplayName = "ValueResult<T> switch expression should match null for a success holding null")]
    public void ValueResultSwitchExpressionShouldMatchNullForSuccessHoldingNull()
    {
        // Given
        ValueResult<string?> success = ValueResult<string?>.Success("abc");
        ValueResult<string?> successOfNull = ValueResult<string?>.Success(null);
        ValueResult<string?> failure = ValueResult<string?>.Failure(FailureException);

        // When / Then
        Assert.Equal("Value: abc", Describe(success));
        Assert.Equal("Value: null", Describe(successOfNull));
        Assert.Equal("Failure", Describe(failure));

        // T is nullable, so a success can hold null; removing the null arm reports CS8655 (null not handled).
        static string Describe(ValueResult<string?> result) => result switch
        {
            string value => $"Value: {value}",
            Exception exception => exception.Message,
            null => "Value: null"
        };
    }

    [Fact(DisplayName = "ValueResult<T> type patterns should test the underlying value or exception")]
    public void ValueResultTypePatternsShouldTestTheUnderlyingValueOrException()
    {
        // Given
        ValueResult<int> success = ValueResult<int>.Success(123);
        ValueResult<int> failure = ValueResult<int>.Failure(new InvalidOperationException("Failure"));

        // When / Then
        Assert.True(success is 123);
        Assert.True(success is > 100 and < 200);
        Assert.False(success is Exception);
        Assert.True(failure is InvalidOperationException { Message: "Failure" });
        Assert.False(failure is int);
    }

    [Fact(DisplayName = "ValueResult<T>.Select should produce the expected result")]
    public void ValueResultSelectShouldProduceExpectedResult()
    {
        // Given
        ValueResult<int> success = ValueResult<int>.Success(123);
        ValueResult<int> failure = ValueResult<int>.Failure(FailureException);
        int observed = 0;

        // When
        ValueResult successThenAction = success.Select(value => { observed = value; });
        ValueResult failureThenAction = failure.Select(value => { observed = -value; });
        ValueResult<string> successThenFunction = success.Select(value => $"Value: {value}");
        ValueResult<string> successThenThrow = success.Select<string>(_ => throw FailureException);
        ValueResult<string> failureThenFunction = failure.Select(value => $"Value: {value}");

        // Then
        Assert.True(successThenAction.IsSuccess);
        Assert.Same(FailureException, failureThenAction.GetExceptionOrDefault());
        Assert.Equal(123, observed);
        Assert.Equal("Value: 123", successThenFunction.GetValueOrThrow());
        Assert.Same(FailureException, successThenThrow.GetExceptionOrDefault());
        Assert.Same(FailureException, failureThenFunction.GetExceptionOrDefault());
    }

    [Fact(DisplayName = "ValueResult<T>.SelectMany should produce the expected result")]
    public void ValueResultSelectManyShouldProduceExpectedResult()
    {
        // Given
        ValueResult<int> success = ValueResult<int>.Success(123);
        ValueResult<int> failure = ValueResult<int>.Failure(FailureException);
        Exception otherException = new("Other");

        // When
        ValueResult successThenUntyped = success.SelectMany(_ => ValueResult.Failure(otherException));
        ValueResult failureThenUntyped = failure.SelectMany(_ => ValueResult.Success());
        ValueResult<string> successThenTyped = success.SelectMany(value => ValueResult<string>.Success($"Value: {value}"));
        ValueResult<string> successThenThrow = success.SelectMany<string>(_ => throw otherException);
        ValueResult<string> failureThenTyped = failure.SelectMany(value => ValueResult<string>.Success($"Value: {value}"));

        // Then
        Assert.Same(otherException, successThenUntyped.GetExceptionOrDefault());
        Assert.Same(FailureException, failureThenUntyped.GetExceptionOrDefault());
        Assert.Equal("Value: 123", successThenTyped.GetValueOrThrow());
        Assert.Same(otherException, successThenThrow.GetExceptionOrDefault());
        Assert.Same(FailureException, failureThenTyped.GetExceptionOrDefault());
    }

    [Fact(DisplayName = "ValueResult.Select and ValueResult.SelectMany should produce a typed result")]
    public void ValueResultSelectAndSelectManyShouldProduceTypedResult()
    {
        // Given
        ValueResult success = ValueResult.Success();
        ValueResult failure = ValueResult.Failure(FailureException);

        // When
        ValueResult<int> successThenSelect = success.Select(() => 123);
        ValueResult<int> failureThenSelect = failure.Select(() => 123);
        ValueResult<int> successThenSelectMany = success.SelectMany(() => ValueResult<int>.Success(456));
        ValueResult<int> successThenThrow = success.SelectMany<int>(() => throw FailureException);

        // Then
        Assert.Equal(123, successThenSelect.GetValueOrThrow());
        Assert.Same(FailureException, failureThenSelect.GetExceptionOrDefault());
        Assert.Equal(456, successThenSelectMany.GetValueOrThrow());
        Assert.Same(FailureException, successThenThrow.GetExceptionOrDefault());
    }

    [Fact(DisplayName = "ValueResult<T> default should propagate its uninitialised exception through Select")]
    public void ValueResultDefaultShouldPropagateItsUninitialisedExceptionThroughSelect()
    {
        // Given
        ValueResult<int> uninitialised = default;

        // When
        ValueResult<string> selected = uninitialised.Select(value => $"Value: {value}");

        // Then
        Assert.True(selected.IsFailure);
        Assert.Same(uninitialised.GetExceptionOrDefault(), selected.GetExceptionOrDefault());
    }

    [Fact(DisplayName = "ValueResult<T>.Throw should throw the underlying exception of a failure only")]
    public void ValueResultThrowShouldThrowTheUnderlyingExceptionOfFailureOnly()
    {
        // Given
        ValueResult<int> success = ValueResult<int>.Success(123);
        ValueResult<int> failure = ValueResult<int>.Failure(FailureException);

        // When
        success.Throw();
        Exception exception = Assert.Throws<Exception>(failure.Throw);

        // Then
        Assert.Same(FailureException, exception);
    }

    [Fact(DisplayName = "ValueResult<T>.ToResult and ValueResult<T>.From should round-trip through Result<T>")]
    public void ValueResultToResultAndFromShouldRoundTripThroughResult()
    {
        // Given
        ValueResult<int> success = ValueResult<int>.Success(123);
        ValueResult<int> failure = ValueResult<int>.Failure(FailureException);

        // When
        Result<int> successResult = success.ToResult();
        Result<int> failureResult = failure.ToResult();

        // Then
        Assert.Equal(123, Assert.IsType<Success<int>>(successResult).Value);
        Assert.Same(FailureException, Assert.IsType<Failure<int>>(failureResult).Exception);
        Assert.Equal(success, ValueResult<int>.From(successResult));
        Assert.Equal(failure, ValueResult<int>.From(failureResult));
    }

    [Fact(DisplayName = "ValueResult<T>.ToString should produce the expected result")]
    public void ValueResultToStringShouldProduceExpectedResult()
    {
        // Given
        ValueResult<int> success = ValueResult<int>.Success(123);
        ValueResult<string?> successOfNull = ValueResult<string?>.Success(null);
        ValueResult<int> failure = ValueResult<int>.Failure(FailureException);

        // When / Then
        Assert.Equal("123", success.ToString());
        Assert.Equal(string.Empty, successOfNull.ToString());
        Assert.Equal("Failure", failure.ToString());
    }
}
