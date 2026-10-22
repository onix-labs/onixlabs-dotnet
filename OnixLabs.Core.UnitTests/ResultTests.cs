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

public sealed class ResultTests
{
    private static readonly Exception FailureException = new("Failure");

    [Fact(DisplayName = "Result.IsSuccess should produce the expected result")]
    public void ResultIsSuccessShouldProduceExpectedResult()
    {
        // Given
        Result result = Result.Success();

        // When / Then
        Assert.True(result.IsSuccess);
        Assert.False(result.IsFailure);
    }

    [Fact(DisplayName = "Result.IsFailure should produce the expected result")]
    public void ResultIsFailureShouldProduceExpectedResult()
    {
        // Given
        Result result = Result.Failure(FailureException);

        // When / Then
        Assert.True(result.IsFailure);
        Assert.False(result.IsSuccess);
    }

    [Fact(DisplayName = "Result.Of should produce the expected success result.")]
    public void ResultOfShouldProduceExpectedSuccessResult()
    {
        // Given / When
        Result result = Result.Of(() => { });

        // Then
        Assert.IsType<Success>(result);
    }

    [Fact(DisplayName = "Result.Of should produce the expected failure result.")]
    public void ResultOfShouldProduceExpectedFailureResult()
    {
        // Given / When
        Result result = Result.Of(() => throw FailureException);

        // Then
        Assert.IsType<Failure>(result);
        Assert.Equal(FailureException, result.GetExceptionOrThrow());
    }

    [Fact(DisplayName = "Result.OfAsync should produce the expected success result.")]
    public async Task ResultOfAsyncShouldProduceExpectedSuccessResult()
    {
        // Given / When
        Result result = await Result.OfAsync(async () => await Task.CompletedTask, TestContext.Current.CancellationToken);

        // Then
        Assert.IsType<Success>(result);
    }

    [Fact(DisplayName = "Result.OfAsync should produce the expected failure result.")]
    public async Task ResultOfAsyncShouldProduceExpectedFailureResult()
    {
        // Given / When
        Result result = await Result.OfAsync(async () => await Task.FromException(FailureException), TestContext.Current.CancellationToken);

        // Then
        Assert.IsType<Failure>(result);
        Assert.Equal(FailureException, result.GetExceptionOrThrow());
    }

    [Fact(DisplayName = "Result.OfAsync with cancellation token should produce the expected success result.")]
    public async Task ResultOfAsyncWithCancellationTokenShouldProduceExpectedSuccessResult()
    {
        // Given / When
        Result result = await Result.OfAsync(async () => await Task.CompletedTask, CancellationToken.None);

        // Then
        Assert.IsType<Success>(result);
    }

    [Fact(DisplayName = "Result.OfAsync with cancellation token should produce the expected failure result.")]
    public async Task ResultOfAsyncWithCancellationTokenShouldProduceExpectedFailureResult()
    {
        // Given / When
        Result result = await Result.OfAsync(async () => await Task.FromException(FailureException), CancellationToken.None);

        // Then
        Assert.IsType<Failure>(result);
        Assert.Equal(FailureException, result.GetExceptionOrThrow());
    }

    [Fact(DisplayName = "Result.OfAsync with cancellable function should produce the expected success result.")]
    public async Task ResultOfAsyncWithCancellableFunctionShouldProduceExpectedSuccessResult()
    {
        // Given / When
        Result result = await Result.OfAsync(async _ => await Task.CompletedTask, CancellationToken.None);

        // Then
        Assert.IsType<Success>(result);
    }

    [Fact(DisplayName = "Result.OfAsync with cancellable function should produce the expected failure result.")]
    public async Task ResultOfAsyncWithCancellableFunctionShouldProduceExpectedFailureResult()
    {
        // Given / When
        Result result = await Result.OfAsync(async _ => await Task.FromException(FailureException), CancellationToken.None);

        // Then
        Assert.IsType<Failure>(result);
        Assert.Equal(FailureException, result.GetExceptionOrThrow());
    }

    [Fact(DisplayName = "Result.Success should produce the expected success result.")]
    public void ResultSuccessShouldProduceExpectedSuccessResult()
    {
        // Given / When
        Result result = Result.Success();

        // Then
        Assert.IsType<Success>(result);
    }

    [Fact(DisplayName = "Result.Failure should produce the expected failure result.")]
    public void ResultFailureShouldProduceExpectedFailureResult()
    {
        // Given / When
        Result result = Result.Failure(FailureException);

        // Then
        Assert.IsType<Failure>(result);
        Assert.Equal(FailureException, result.GetExceptionOrThrow());
    }

    [Fact(DisplayName = "Result from implicit exception should produce the expected failure result.")]
    public void ResultFromImplicitExceptionShouldProduceExpectedFailureResult()
    {
        // Given / When
        Result result = FailureException;

        // Then
        Assert.IsType<Failure>(result);
        Assert.Equal(FailureException, result.GetExceptionOrThrow());
    }

    [Fact(DisplayName = "Result equality should produce the expected result.")]
    public void ResultEqualityShouldProduceExpectedResult()
    {
        // ReSharper disable EqualExpressionComparison

        // Equals Method
        Assert.True(Result.Success().Equals(Result.Success()));
        Assert.True(Result.Failure(FailureException).Equals(Result.Failure(FailureException)));
        Assert.False(Result.Success().Equals(null));
        Assert.False(Result.Failure(FailureException).Equals(null));

        // Equality Operator
        Assert.True(Result.Success() == Result.Success());
        Assert.True(Result.Failure(FailureException) == Result.Failure(FailureException));
        Assert.False(Result.Success() == Result.Failure(FailureException));
        Assert.False(Result.Success() == null);
        Assert.False(null == Result.Success());

        // Inequality Operator
        Assert.True(Result.Success() != Result.Failure(FailureException));
        Assert.True(Result.Failure(FailureException) != Result.Failure(new Exception()));
        Assert.True(Result.Success() != null);
        Assert.True(null != Result.Success());

        // Hash Code Generation
        Assert.True(Result.Success().GetHashCode() == Result.Success().GetHashCode());
        Assert.True(Result.Failure(FailureException).GetHashCode() == Result.Failure(FailureException).GetHashCode());
    }

    [Fact(DisplayName = "Result.GetExceptionOrDefault from success should produce the expected result.")]
    public void ResultGetExceptionOrDefaultFromSuccessShouldProduceExpectedResult()
    {
        // Given
        Result result = Result.Success();

        // Then
        Exception? exception = result.GetExceptionOrDefault();

        // Then
        Assert.Null(exception);
    }

    [Fact(DisplayName = "Result.GetExceptionOrDefault from failure should produce the expected result.")]
    public void ResultGetExceptionOrDefaultFromFailureShouldProduceExpectedResult()
    {
        // Given
        Result result = Result.Failure(FailureException);

        // Then
        Exception? exception = result.GetExceptionOrDefault();

        // Then
        Assert.Equal(FailureException, exception);
    }

    [Fact(DisplayName = "Result.GetExceptionOrDefault from success with default value should produce the expected result.")]
    public void ResultGetExceptionOrDefaultFromSuccessWithDefaultValueShouldProduceExpectedResult()
    {
        // Given
        Result result = Result.Success();
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
        Result result = Result.Failure(FailureException);
        Exception defaultValue = new("Default");

        // Then
        Exception exception = result.GetExceptionOrDefault(defaultValue);

        // Then
        Assert.Equal(FailureException, exception);
    }

    [Fact(DisplayName = "Result.GetExceptionOrNone from success should produce the expected result.")]
    public void ResultGetExceptionOrNoneFromSuccessShouldProduceExpectedResult()
    {
        // Given
        Result result = Result.Success();

        // When
        Optional<Exception> exception = result.GetExceptionOrNone();

        // Then
        Assert.Equal(Optional<Exception>.None, exception);
    }

    [Fact(DisplayName = "Result.GetExceptionOrNone from failure should produce the expected result.")]
    public void ResultGetExceptionOrNoneFromFailureShouldProduceExpectedResult()
    {
        // Given
        Result result = Result.Failure(FailureException);

        // When
        Optional<Exception> exception = result.GetExceptionOrNone();

        // Then
        Assert.Equal(Optional<Exception>.Some(FailureException), exception);
    }

    [Fact(DisplayName = "Result.GetExceptionOrThrow from success should produce the expected result.")]
    public void ResultGetExceptionOrThrowFromSuccessShouldProduceExpectedResult()
    {
        // Given
        Result result = Result.Success();

        // Then
        Exception exception = Assert.Throws<InvalidOperationException>(() => result.GetExceptionOrThrow());

        // Then
        Assert.Equal("The current result is in a success state; no exception to retrieve.", exception.Message);
    }

    [Fact(DisplayName = "Result.GetExceptionOrThrow from failure should produce the expected result.")]
    public void ResultGetExceptionOrThrowFromFailureShouldProduceExpectedResult()
    {
        // Given
        Result result = Result.Failure(FailureException);

        // Then
        Exception exception = result.GetExceptionOrThrow();

        // Then
        Assert.Equal(FailureException, exception);
    }

    [Fact(DisplayName = "Result.Match action from success should produce the expected result.")]
    public void ResultMatchActionFromSuccessShouldProduceExpectedResult()
    {
        // Given
        Result result = Result.Success();
        bool isSuccess = false;
        bool isFailure = false;

        // When
        result.Match(Success, Failure);

        // Then
        Assert.True(isSuccess);
        Assert.False(isFailure);

        return;
        void Success() => isSuccess = true;
        void Failure(Exception exception) => isFailure = true;
    }

    [Fact(DisplayName = "Result.Match action from failure should produce the expected result.")]
    public void ResultMatchActionFromFailureShouldProduceExpectedResult()
    {
        // Given
        Result result = Result.Failure(FailureException);
        bool isSuccess = false;
        bool isFailure = false;

        // When
        result.Match(Success, Failure);

        // Then
        Assert.False(isSuccess);
        Assert.True(isFailure);

        return;
        void Success() => isSuccess = true;
        void Failure(Exception exception) => isFailure = true;
    }

    [Fact(DisplayName = "Result.MatchAsync(Func<Task>, Action<Exception>) from success should produce the expected result.")]
    public async Task ResultMatchAsyncFuncTaskActionFromSuccessShouldProduceExpectedResult()
    {
        // Given
        Result result = Result.Success();
        bool isSuccess = false;
        bool isFailure = false;

        // When
        await result.MatchAsync(Success, Failure, TestContext.Current.CancellationToken);

        // Then
        Assert.True(isSuccess);
        Assert.False(isFailure);

        return;

        Task Success()
        {
            isSuccess = true;
            return Task.CompletedTask;
        }

        void Failure(Exception exception) => isFailure = true;
    }

    [Fact(DisplayName = "Result.MatchAsync(Func<Task>, Action<Exception>) from success should produce the expected result when the delegates complete asynchronously.")]
    public async Task ResultMatchAsyncFuncTaskActionFromSuccessShouldProduceExpectedResultWhenDelegatesCompleteAsynchronously()
    {
        // Given
        Result result = Result.Success();
        bool isSuccess = false;
        bool isFailure = false;

        // When
        await result.MatchAsync(Success, Failure, TestContext.Current.CancellationToken);

        // Then
        Assert.True(isSuccess);
        Assert.False(isFailure);

        return;

        Task Success()
        {
            isSuccess = true;
            return Yielding.CompletedTask();
        }

        void Failure(Exception exception) => isFailure = true;
    }

    [Fact(DisplayName = "Result.MatchAsync(Func<ValueTask>, Action<Exception>) from success should produce the expected result.")]
    public async Task ResultMatchAsyncFuncTaskActionFromSuccessShouldProduceExpectedResultWithValueTaskDelegates()
    {
        // Given
        Result result = Result.Success();
        bool isSuccess = false;
        bool isFailure = false;

        // When
        await result.MatchAsync(Success, Failure, TestContext.Current.CancellationToken);

        // Then
        Assert.True(isSuccess);
        Assert.False(isFailure);

        return;

        ValueTask Success()
        {
            isSuccess = true;
            return ValueTask.CompletedTask;
        }

        void Failure(Exception exception) => isFailure = true;
    }

    [Fact(DisplayName = "Result.MatchAsync(Func<ValueTask>, Action<Exception>) from success should produce the expected result when the delegates complete asynchronously.")]
    public async Task ResultMatchAsyncFuncTaskActionFromSuccessShouldProduceExpectedResultWithValueTaskDelegatesWhenDelegatesCompleteAsynchronously()
    {
        // Given
        Result result = Result.Success();
        bool isSuccess = false;
        bool isFailure = false;

        // When
        await result.MatchAsync(Success, Failure, TestContext.Current.CancellationToken);

        // Then
        Assert.True(isSuccess);
        Assert.False(isFailure);

        return;

        ValueTask Success()
        {
            isSuccess = true;
            return Yielding.CompletedValueTask();
        }

        void Failure(Exception exception) => isFailure = true;
    }

    [Fact(DisplayName = "Result.MatchAsync(Func<Task>, Action<Exception>) from failure should produce the expected result.")]
    public async Task ResultMatchAsyncFuncTaskActionFromFailureShouldProduceExpectedResult()
    {
        // Given
        Result result = Result.Failure(FailureException);
        bool isSuccess = false;
        bool isFailure = false;

        // When
        await result.MatchAsync(Success, Failure, TestContext.Current.CancellationToken);

        // Then
        Assert.False(isSuccess);
        Assert.True(isFailure);

        return;

        Task Success()
        {
            isSuccess = false;
            return Task.CompletedTask;
        }

        void Failure(Exception exception) => isFailure = true;
    }

    [Fact(DisplayName = "Result.MatchAsync(Func<Task>, Action<Exception>) from failure should produce the expected result when the delegates complete asynchronously.")]
    public async Task ResultMatchAsyncFuncTaskActionFromFailureShouldProduceExpectedResultWhenDelegatesCompleteAsynchronously()
    {
        // Given
        Result result = Result.Failure(FailureException);
        bool isSuccess = false;
        bool isFailure = false;

        // When
        await result.MatchAsync(Success, Failure, TestContext.Current.CancellationToken);

        // Then
        Assert.False(isSuccess);
        Assert.True(isFailure);

        return;

        Task Success()
        {
            isSuccess = false;
            return Yielding.CompletedTask();
        }

        void Failure(Exception exception) => isFailure = true;
    }

    [Fact(DisplayName = "Result.MatchAsync(Func<ValueTask>, Action<Exception>) from failure should produce the expected result.")]
    public async Task ResultMatchAsyncFuncTaskActionFromFailureShouldProduceExpectedResultWithValueTaskDelegates()
    {
        // Given
        Result result = Result.Failure(FailureException);
        bool isSuccess = false;
        bool isFailure = false;

        // When
        await result.MatchAsync(Success, Failure, TestContext.Current.CancellationToken);

        // Then
        Assert.False(isSuccess);
        Assert.True(isFailure);

        return;

        ValueTask Success()
        {
            isSuccess = false;
            return ValueTask.CompletedTask;
        }

        void Failure(Exception exception) => isFailure = true;
    }

    [Fact(DisplayName = "Result.MatchAsync(Func<ValueTask>, Action<Exception>) from failure should produce the expected result when the delegates complete asynchronously.")]
    public async Task ResultMatchAsyncFuncTaskActionFromFailureShouldProduceExpectedResultWithValueTaskDelegatesWhenDelegatesCompleteAsynchronously()
    {
        // Given
        Result result = Result.Failure(FailureException);
        bool isSuccess = false;
        bool isFailure = false;

        // When
        await result.MatchAsync(Success, Failure, TestContext.Current.CancellationToken);

        // Then
        Assert.False(isSuccess);
        Assert.True(isFailure);

        return;

        ValueTask Success()
        {
            isSuccess = false;
            return Yielding.CompletedValueTask();
        }

        void Failure(Exception exception) => isFailure = true;
    }

    [Fact(DisplayName = "Result.MatchAsync(Func<CancellationToken, Task>, Action<Exception>) from success should produce the expected result.")]
    public async Task ResultMatchAsyncFuncCancellationTokenTaskActionFromSuccessShouldProduceExpectedResult()
    {
        // Given
        Result result = Result.Success();
        bool isSuccess = false;
        bool isFailure = false;
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        await result.MatchAsync(Success, Failure, cancellationToken);

        // Then
        Assert.True(isSuccess);
        Assert.False(isFailure);

        return;

        Task Success(CancellationToken ct)
        {
            isSuccess = true;
            return Task.CompletedTask;
        }

        void Failure(Exception exception) => isFailure = true;
    }

    [Fact(DisplayName = "Result.MatchAsync(Func<CancellationToken, Task>, Action<Exception>) from success should produce the expected result when the delegates complete asynchronously.")]
    public async Task ResultMatchAsyncFuncCancellationTokenTaskActionFromSuccessShouldProduceExpectedResultWhenDelegatesCompleteAsynchronously()
    {
        // Given
        Result result = Result.Success();
        bool isSuccess = false;
        bool isFailure = false;
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        await result.MatchAsync(Success, Failure, cancellationToken);

        // Then
        Assert.True(isSuccess);
        Assert.False(isFailure);

        return;

        Task Success(CancellationToken ct)
        {
            isSuccess = true;
            return Yielding.CompletedTask();
        }

        void Failure(Exception exception) => isFailure = true;
    }

    [Fact(DisplayName = "Result.MatchAsync(Func<CancellationToken, ValueTask>, Action<Exception>) from success should produce the expected result.")]
    public async Task ResultMatchAsyncFuncCancellationTokenTaskActionFromSuccessShouldProduceExpectedResultWithValueTaskDelegates()
    {
        // Given
        Result result = Result.Success();
        bool isSuccess = false;
        bool isFailure = false;
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        await result.MatchAsync(Success, Failure, cancellationToken);

        // Then
        Assert.True(isSuccess);
        Assert.False(isFailure);

        return;

        ValueTask Success(CancellationToken ct)
        {
            isSuccess = true;
            return ValueTask.CompletedTask;
        }

        void Failure(Exception exception) => isFailure = true;
    }

    [Fact(DisplayName = "Result.MatchAsync(Func<CancellationToken, ValueTask>, Action<Exception>) from success should produce the expected result when the delegates complete asynchronously.")]
    public async Task ResultMatchAsyncFuncCancellationTokenTaskActionFromSuccessShouldProduceExpectedResultWithValueTaskDelegatesWhenDelegatesCompleteAsynchronously()
    {
        // Given
        Result result = Result.Success();
        bool isSuccess = false;
        bool isFailure = false;
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        await result.MatchAsync(Success, Failure, cancellationToken);

        // Then
        Assert.True(isSuccess);
        Assert.False(isFailure);

        return;

        ValueTask Success(CancellationToken ct)
        {
            isSuccess = true;
            return Yielding.CompletedValueTask();
        }

        void Failure(Exception exception) => isFailure = true;
    }

    [Fact(DisplayName = "Result.MatchAsync(Func<CancellationToken, Task>, Action<Exception>) from failure should produce the expected result.")]
    public async Task ResultMatchAsyncFuncCancellationTokenTaskActionFromFailureShouldProduceExpectedResult()
    {
        // Given
        Result result = Result.Failure(FailureException);
        bool isSuccess = false;
        bool isFailure = false;
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        await result.MatchAsync(Success, Failure, cancellationToken);

        // Then
        Assert.False(isSuccess);
        Assert.True(isFailure);

        return;

        Task Success(CancellationToken ct)
        {
            isSuccess = false;
            return Task.CompletedTask;
        }

        void Failure(Exception exception) => isFailure = true;
    }

    [Fact(DisplayName = "Result.MatchAsync(Func<CancellationToken, Task>, Action<Exception>) from failure should produce the expected result when the delegates complete asynchronously.")]
    public async Task ResultMatchAsyncFuncCancellationTokenTaskActionFromFailureShouldProduceExpectedResultWhenDelegatesCompleteAsynchronously()
    {
        // Given
        Result result = Result.Failure(FailureException);
        bool isSuccess = false;
        bool isFailure = false;
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        await result.MatchAsync(Success, Failure, cancellationToken);

        // Then
        Assert.False(isSuccess);
        Assert.True(isFailure);

        return;

        Task Success(CancellationToken ct)
        {
            isSuccess = false;
            return Yielding.CompletedTask();
        }

        void Failure(Exception exception) => isFailure = true;
    }

    [Fact(DisplayName = "Result.MatchAsync(Func<CancellationToken, ValueTask>, Action<Exception>) from failure should produce the expected result.")]
    public async Task ResultMatchAsyncFuncCancellationTokenTaskActionFromFailureShouldProduceExpectedResultWithValueTaskDelegates()
    {
        // Given
        Result result = Result.Failure(FailureException);
        bool isSuccess = false;
        bool isFailure = false;
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        await result.MatchAsync(Success, Failure, cancellationToken);

        // Then
        Assert.False(isSuccess);
        Assert.True(isFailure);

        return;

        ValueTask Success(CancellationToken ct)
        {
            isSuccess = false;
            return ValueTask.CompletedTask;
        }

        void Failure(Exception exception) => isFailure = true;
    }

    [Fact(DisplayName = "Result.MatchAsync(Func<CancellationToken, ValueTask>, Action<Exception>) from failure should produce the expected result when the delegates complete asynchronously.")]
    public async Task ResultMatchAsyncFuncCancellationTokenTaskActionFromFailureShouldProduceExpectedResultWithValueTaskDelegatesWhenDelegatesCompleteAsynchronously()
    {
        // Given
        Result result = Result.Failure(FailureException);
        bool isSuccess = false;
        bool isFailure = false;
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        await result.MatchAsync(Success, Failure, cancellationToken);

        // Then
        Assert.False(isSuccess);
        Assert.True(isFailure);

        return;

        ValueTask Success(CancellationToken ct)
        {
            isSuccess = false;
            return Yielding.CompletedValueTask();
        }

        void Failure(Exception exception) => isFailure = true;
    }

    [Fact(DisplayName = "Result.MatchAsync(Action, Func<Exception, Task>) from success should produce the expected result.")]
    public async Task ResultMatchAsyncActionFuncExceptionTaskFromSuccessShouldProduceExpectedResult()
    {
        // Given
        Result result = Result.Success();
        bool isSuccess = false;
        bool isFailure = false;

        // When
        await result.MatchAsync(Success, Failure, TestContext.Current.CancellationToken);

        // Then
        Assert.True(isSuccess);
        Assert.False(isFailure);

        return;
        void Success() => isSuccess = true;

        Task Failure(Exception exception)
        {
            isFailure = true;
            return Task.CompletedTask;
        }
    }

    [Fact(DisplayName = "Result.MatchAsync(Action, Func<Exception, Task>) from success should produce the expected result when the delegates complete asynchronously.")]
    public async Task ResultMatchAsyncActionFuncExceptionTaskFromSuccessShouldProduceExpectedResultWhenDelegatesCompleteAsynchronously()
    {
        // Given
        Result result = Result.Success();
        bool isSuccess = false;
        bool isFailure = false;

        // When
        await result.MatchAsync(Success, Failure, TestContext.Current.CancellationToken);

        // Then
        Assert.True(isSuccess);
        Assert.False(isFailure);

        return;
        void Success() => isSuccess = true;

        Task Failure(Exception exception)
        {
            isFailure = true;
            return Yielding.CompletedTask();
        }
    }

    [Fact(DisplayName = "Result.MatchAsync(Action, Func<Exception, ValueTask>) from success should produce the expected result.")]
    public async Task ResultMatchAsyncActionFuncExceptionTaskFromSuccessShouldProduceExpectedResultWithValueTaskDelegates()
    {
        // Given
        Result result = Result.Success();
        bool isSuccess = false;
        bool isFailure = false;

        // When
        await result.MatchAsync(Success, Failure, TestContext.Current.CancellationToken);

        // Then
        Assert.True(isSuccess);
        Assert.False(isFailure);

        return;
        void Success() => isSuccess = true;

        ValueTask Failure(Exception exception)
        {
            isFailure = true;
            return ValueTask.CompletedTask;
        }
    }

    [Fact(DisplayName = "Result.MatchAsync(Action, Func<Exception, ValueTask>) from success should produce the expected result when the delegates complete asynchronously.")]
    public async Task ResultMatchAsyncActionFuncExceptionTaskFromSuccessShouldProduceExpectedResultWithValueTaskDelegatesWhenDelegatesCompleteAsynchronously()
    {
        // Given
        Result result = Result.Success();
        bool isSuccess = false;
        bool isFailure = false;

        // When
        await result.MatchAsync(Success, Failure, TestContext.Current.CancellationToken);

        // Then
        Assert.True(isSuccess);
        Assert.False(isFailure);

        return;
        void Success() => isSuccess = true;

        ValueTask Failure(Exception exception)
        {
            isFailure = true;
            return Yielding.CompletedValueTask();
        }
    }

    [Fact(DisplayName = "Result.MatchAsync(Action, Func<Exception, Task>) from failure should produce the expected result.")]
    public async Task ResultMatchAsyncActionFuncExceptionTaskFromFailureShouldProduceExpectedResult()
    {
        // Given
        Result result = Result.Failure(FailureException);
        bool isSuccess = false;
        bool isFailure = false;

        // When
        await result.MatchAsync(Success, Failure, TestContext.Current.CancellationToken);

        // Then
        Assert.False(isSuccess);
        Assert.True(isFailure);

        return;
        void Success() => isSuccess = false;

        Task Failure(Exception exception)
        {
            isFailure = true;
            return Task.CompletedTask;
        }
    }

    [Fact(DisplayName = "Result.MatchAsync(Action, Func<Exception, Task>) from failure should produce the expected result when the delegates complete asynchronously.")]
    public async Task ResultMatchAsyncActionFuncExceptionTaskFromFailureShouldProduceExpectedResultWhenDelegatesCompleteAsynchronously()
    {
        // Given
        Result result = Result.Failure(FailureException);
        bool isSuccess = false;
        bool isFailure = false;

        // When
        await result.MatchAsync(Success, Failure, TestContext.Current.CancellationToken);

        // Then
        Assert.False(isSuccess);
        Assert.True(isFailure);

        return;
        void Success() => isSuccess = false;

        Task Failure(Exception exception)
        {
            isFailure = true;
            return Yielding.CompletedTask();
        }
    }

    [Fact(DisplayName = "Result.MatchAsync(Action, Func<Exception, ValueTask>) from failure should produce the expected result.")]
    public async Task ResultMatchAsyncActionFuncExceptionTaskFromFailureShouldProduceExpectedResultWithValueTaskDelegates()
    {
        // Given
        Result result = Result.Failure(FailureException);
        bool isSuccess = false;
        bool isFailure = false;

        // When
        await result.MatchAsync(Success, Failure, TestContext.Current.CancellationToken);

        // Then
        Assert.False(isSuccess);
        Assert.True(isFailure);

        return;
        void Success() => isSuccess = false;

        ValueTask Failure(Exception exception)
        {
            isFailure = true;
            return ValueTask.CompletedTask;
        }
    }

    [Fact(DisplayName = "Result.MatchAsync(Action, Func<Exception, ValueTask>) from failure should produce the expected result when the delegates complete asynchronously.")]
    public async Task ResultMatchAsyncActionFuncExceptionTaskFromFailureShouldProduceExpectedResultWithValueTaskDelegatesWhenDelegatesCompleteAsynchronously()
    {
        // Given
        Result result = Result.Failure(FailureException);
        bool isSuccess = false;
        bool isFailure = false;

        // When
        await result.MatchAsync(Success, Failure, TestContext.Current.CancellationToken);

        // Then
        Assert.False(isSuccess);
        Assert.True(isFailure);

        return;
        void Success() => isSuccess = false;

        ValueTask Failure(Exception exception)
        {
            isFailure = true;
            return Yielding.CompletedValueTask();
        }
    }

    [Fact(DisplayName = "Result.MatchAsync(Action, Func<Exception, CancellationToken, Task>) from success should produce the expected result.")]
    public async Task ResultMatchAsyncActionFuncExceptionCancellationTokenTaskFromSuccessShouldProduceExpectedResult()
    {
        // Given
        Result result = Result.Success();
        bool isSuccess = false;
        bool isFailure = false;
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        await result.MatchAsync(Success, Failure, cancellationToken);

        // Then
        Assert.True(isSuccess);
        Assert.False(isFailure);

        return;
        void Success() => isSuccess = true;

        Task Failure(Exception exception, CancellationToken ct)
        {
            isFailure = true;
            return Task.CompletedTask;
        }
    }

    [Fact(DisplayName = "Result.MatchAsync(Action, Func<Exception, CancellationToken, Task>) from success should produce the expected result when the delegates complete asynchronously.")]
    public async Task ResultMatchAsyncActionFuncExceptionCancellationTokenTaskFromSuccessShouldProduceExpectedResultWhenDelegatesCompleteAsynchronously()
    {
        // Given
        Result result = Result.Success();
        bool isSuccess = false;
        bool isFailure = false;
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        await result.MatchAsync(Success, Failure, cancellationToken);

        // Then
        Assert.True(isSuccess);
        Assert.False(isFailure);

        return;
        void Success() => isSuccess = true;

        Task Failure(Exception exception, CancellationToken ct)
        {
            isFailure = true;
            return Yielding.CompletedTask();
        }
    }

    [Fact(DisplayName = "Result.MatchAsync(Action, Func<Exception, CancellationToken, ValueTask>) from success should produce the expected result.")]
    public async Task ResultMatchAsyncActionFuncExceptionCancellationTokenTaskFromSuccessShouldProduceExpectedResultWithValueTaskDelegates()
    {
        // Given
        Result result = Result.Success();
        bool isSuccess = false;
        bool isFailure = false;
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        await result.MatchAsync(Success, Failure, cancellationToken);

        // Then
        Assert.True(isSuccess);
        Assert.False(isFailure);

        return;
        void Success() => isSuccess = true;

        ValueTask Failure(Exception exception, CancellationToken ct)
        {
            isFailure = true;
            return ValueTask.CompletedTask;
        }
    }

    [Fact(DisplayName = "Result.MatchAsync(Action, Func<Exception, CancellationToken, ValueTask>) from success should produce the expected result when the delegates complete asynchronously.")]
    public async Task ResultMatchAsyncActionFuncExceptionCancellationTokenTaskFromSuccessShouldProduceExpectedResultWithValueTaskDelegatesWhenDelegatesCompleteAsynchronously()
    {
        // Given
        Result result = Result.Success();
        bool isSuccess = false;
        bool isFailure = false;
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        await result.MatchAsync(Success, Failure, cancellationToken);

        // Then
        Assert.True(isSuccess);
        Assert.False(isFailure);

        return;
        void Success() => isSuccess = true;

        ValueTask Failure(Exception exception, CancellationToken ct)
        {
            isFailure = true;
            return Yielding.CompletedValueTask();
        }
    }

    [Fact(DisplayName = "Result.MatchAsync(Action, Func<Exception, CancellationToken, Task>) from failure should produce the expected result.")]
    public async Task ResultMatchAsyncActionFuncExceptionCancellationTokenTaskFromFailureShouldProduceExpectedResult()
    {
        // Given
        Result result = Result.Failure(FailureException);
        bool isSuccess = false;
        bool isFailure = false;
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        await result.MatchAsync(Success, Failure, cancellationToken);

        // Then
        Assert.False(isSuccess);
        Assert.True(isFailure);

        return;
        void Success() => isSuccess = false;

        Task Failure(Exception exception, CancellationToken ct)
        {
            isFailure = true;
            return Task.CompletedTask;
        }
    }

    [Fact(DisplayName = "Result.MatchAsync(Action, Func<Exception, CancellationToken, Task>) from failure should produce the expected result when the delegates complete asynchronously.")]
    public async Task ResultMatchAsyncActionFuncExceptionCancellationTokenTaskFromFailureShouldProduceExpectedResultWhenDelegatesCompleteAsynchronously()
    {
        // Given
        Result result = Result.Failure(FailureException);
        bool isSuccess = false;
        bool isFailure = false;
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        await result.MatchAsync(Success, Failure, cancellationToken);

        // Then
        Assert.False(isSuccess);
        Assert.True(isFailure);

        return;
        void Success() => isSuccess = false;

        Task Failure(Exception exception, CancellationToken ct)
        {
            isFailure = true;
            return Yielding.CompletedTask();
        }
    }

    [Fact(DisplayName = "Result.MatchAsync(Action, Func<Exception, CancellationToken, ValueTask>) from failure should produce the expected result.")]
    public async Task ResultMatchAsyncActionFuncExceptionCancellationTokenTaskFromFailureShouldProduceExpectedResultWithValueTaskDelegates()
    {
        // Given
        Result result = Result.Failure(FailureException);
        bool isSuccess = false;
        bool isFailure = false;
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        await result.MatchAsync(Success, Failure, cancellationToken);

        // Then
        Assert.False(isSuccess);
        Assert.True(isFailure);

        return;
        void Success() => isSuccess = false;

        ValueTask Failure(Exception exception, CancellationToken ct)
        {
            isFailure = true;
            return ValueTask.CompletedTask;
        }
    }

    [Fact(DisplayName = "Result.MatchAsync(Action, Func<Exception, CancellationToken, ValueTask>) from failure should produce the expected result when the delegates complete asynchronously.")]
    public async Task ResultMatchAsyncActionFuncExceptionCancellationTokenTaskFromFailureShouldProduceExpectedResultWithValueTaskDelegatesWhenDelegatesCompleteAsynchronously()
    {
        // Given
        Result result = Result.Failure(FailureException);
        bool isSuccess = false;
        bool isFailure = false;
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        await result.MatchAsync(Success, Failure, cancellationToken);

        // Then
        Assert.False(isSuccess);
        Assert.True(isFailure);

        return;
        void Success() => isSuccess = false;

        ValueTask Failure(Exception exception, CancellationToken ct)
        {
            isFailure = true;
            return Yielding.CompletedValueTask();
        }
    }

    [Fact(DisplayName = "Result.MatchAsync(Func<Task>, Func<Exception, Task>) from success should produce the expected result.")]
    public async Task ResultMatchAsyncFuncTaskFuncExceptionTaskFromSuccessShouldProduceExpectedResult()
    {
        // Given
        Result result = Result.Success();
        bool isSuccess = false;
        bool isFailure = false;
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        await result.MatchAsync(Success, Failure, cancellationToken);

        // Then
        Assert.True(isSuccess);
        Assert.False(isFailure);

        return;

        Task Success()
        {
            isSuccess = true;
            return Task.CompletedTask;
        }

        Task Failure(Exception exception)
        {
            isFailure = true;
            return Task.CompletedTask;
        }
    }

    [Fact(DisplayName = "Result.MatchAsync(Func<Task>, Func<Exception, Task>) from success should produce the expected result when the delegates complete asynchronously.")]
    public async Task ResultMatchAsyncFuncTaskFuncExceptionTaskFromSuccessShouldProduceExpectedResultWhenDelegatesCompleteAsynchronously()
    {
        // Given
        Result result = Result.Success();
        bool isSuccess = false;
        bool isFailure = false;
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        await result.MatchAsync(Success, Failure, cancellationToken);

        // Then
        Assert.True(isSuccess);
        Assert.False(isFailure);

        return;

        Task Success()
        {
            isSuccess = true;
            return Yielding.CompletedTask();
        }

        Task Failure(Exception exception)
        {
            isFailure = true;
            return Yielding.CompletedTask();
        }
    }

    [Fact(DisplayName = "Result.MatchAsync(Func<ValueTask>, Func<Exception, ValueTask>) from success should produce the expected result.")]
    public async Task ResultMatchAsyncFuncTaskFuncExceptionTaskFromSuccessShouldProduceExpectedResultWithValueTaskDelegates()
    {
        // Given
        Result result = Result.Success();
        bool isSuccess = false;
        bool isFailure = false;
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        await result.MatchAsync(Success, Failure, cancellationToken);

        // Then
        Assert.True(isSuccess);
        Assert.False(isFailure);

        return;

        ValueTask Success()
        {
            isSuccess = true;
            return ValueTask.CompletedTask;
        }

        ValueTask Failure(Exception exception)
        {
            isFailure = true;
            return ValueTask.CompletedTask;
        }
    }

    [Fact(DisplayName = "Result.MatchAsync(Func<ValueTask>, Func<Exception, ValueTask>) from success should produce the expected result when the delegates complete asynchronously.")]
    public async Task ResultMatchAsyncFuncTaskFuncExceptionTaskFromSuccessShouldProduceExpectedResultWithValueTaskDelegatesWhenDelegatesCompleteAsynchronously()
    {
        // Given
        Result result = Result.Success();
        bool isSuccess = false;
        bool isFailure = false;
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        await result.MatchAsync(Success, Failure, cancellationToken);

        // Then
        Assert.True(isSuccess);
        Assert.False(isFailure);

        return;

        ValueTask Success()
        {
            isSuccess = true;
            return Yielding.CompletedValueTask();
        }

        ValueTask Failure(Exception exception)
        {
            isFailure = true;
            return Yielding.CompletedValueTask();
        }
    }

    [Fact(DisplayName = "Result.MatchAsync(Func<Task>, Func<Exception, Task>) from failure should produce the expected result.")]
    public async Task ResultMatchAsyncFuncTaskFuncExceptionTaskFromFailureShouldProduceExpectedResult()
    {
        // Given
        Result result = Result.Failure(FailureException);
        bool isSuccess = false;
        bool isFailure = false;
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        await result.MatchAsync(Success, Failure, cancellationToken);

        // Then
        Assert.False(isSuccess);
        Assert.True(isFailure);

        return;

        Task Success()
        {
            isSuccess = false;
            return Task.CompletedTask;
        }

        Task Failure(Exception exception)
        {
            isFailure = true;
            return Task.CompletedTask;
        }
    }

    [Fact(DisplayName = "Result.MatchAsync(Func<Task>, Func<Exception, Task>) from failure should produce the expected result when the delegates complete asynchronously.")]
    public async Task ResultMatchAsyncFuncTaskFuncExceptionTaskFromFailureShouldProduceExpectedResultWhenDelegatesCompleteAsynchronously()
    {
        // Given
        Result result = Result.Failure(FailureException);
        bool isSuccess = false;
        bool isFailure = false;
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        await result.MatchAsync(Success, Failure, cancellationToken);

        // Then
        Assert.False(isSuccess);
        Assert.True(isFailure);

        return;

        Task Success()
        {
            isSuccess = false;
            return Yielding.CompletedTask();
        }

        Task Failure(Exception exception)
        {
            isFailure = true;
            return Yielding.CompletedTask();
        }
    }

    [Fact(DisplayName = "Result.MatchAsync(Func<ValueTask>, Func<Exception, ValueTask>) from failure should produce the expected result.")]
    public async Task ResultMatchAsyncFuncTaskFuncExceptionTaskFromFailureShouldProduceExpectedResultWithValueTaskDelegates()
    {
        // Given
        Result result = Result.Failure(FailureException);
        bool isSuccess = false;
        bool isFailure = false;
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        await result.MatchAsync(Success, Failure, cancellationToken);

        // Then
        Assert.False(isSuccess);
        Assert.True(isFailure);

        return;

        ValueTask Success()
        {
            isSuccess = false;
            return ValueTask.CompletedTask;
        }

        ValueTask Failure(Exception exception)
        {
            isFailure = true;
            return ValueTask.CompletedTask;
        }
    }

    [Fact(DisplayName = "Result.MatchAsync(Func<ValueTask>, Func<Exception, ValueTask>) from failure should produce the expected result when the delegates complete asynchronously.")]
    public async Task ResultMatchAsyncFuncTaskFuncExceptionTaskFromFailureShouldProduceExpectedResultWithValueTaskDelegatesWhenDelegatesCompleteAsynchronously()
    {
        // Given
        Result result = Result.Failure(FailureException);
        bool isSuccess = false;
        bool isFailure = false;
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        await result.MatchAsync(Success, Failure, cancellationToken);

        // Then
        Assert.False(isSuccess);
        Assert.True(isFailure);

        return;

        ValueTask Success()
        {
            isSuccess = false;
            return Yielding.CompletedValueTask();
        }

        ValueTask Failure(Exception exception)
        {
            isFailure = true;
            return Yielding.CompletedValueTask();
        }
    }

    [Fact(DisplayName = "Result.MatchAsync(Func<CancellationToken, Task>, Func<Exception, CancellationToken, Task>) from success should produce the expected result.")]
    public async Task ResultMatchAsyncFuncCancellationTokenTaskFuncExceptionCancellationTokenTaskFromSuccessShouldProduceExpectedResult()
    {
        // Given
        Result result = Result.Success();
        bool isSuccess = false;
        bool isFailure = false;
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        await result.MatchAsync(Success, Failure, cancellationToken);

        // Then
        Assert.True(isSuccess);
        Assert.False(isFailure);

        return;

        Task Success(CancellationToken ct)
        {
            isSuccess = true;
            return Task.CompletedTask;
        }

        Task Failure(Exception exception, CancellationToken ct)
        {
            isFailure = true;
            return Task.CompletedTask;
        }
    }

    [Fact(DisplayName = "Result.MatchAsync(Func<CancellationToken, Task>, Func<Exception, CancellationToken, Task>) from success should produce the expected result when the delegates complete asynchronously.")]
    public async Task ResultMatchAsyncFuncCancellationTokenTaskFuncExceptionCancellationTokenTaskFromSuccessShouldProduceExpectedResultWhenDelegatesCompleteAsynchronously()
    {
        // Given
        Result result = Result.Success();
        bool isSuccess = false;
        bool isFailure = false;
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        await result.MatchAsync(Success, Failure, cancellationToken);

        // Then
        Assert.True(isSuccess);
        Assert.False(isFailure);

        return;

        Task Success(CancellationToken ct)
        {
            isSuccess = true;
            return Yielding.CompletedTask();
        }

        Task Failure(Exception exception, CancellationToken ct)
        {
            isFailure = true;
            return Yielding.CompletedTask();
        }
    }

    [Fact(DisplayName = "Result.MatchAsync(Func<CancellationToken, ValueTask>, Func<Exception, CancellationToken, ValueTask>) from success should produce the expected result.")]
    public async Task ResultMatchAsyncFuncCancellationTokenTaskFuncExceptionCancellationTokenTaskFromSuccessShouldProduceExpectedResultWithValueTaskDelegates()
    {
        // Given
        Result result = Result.Success();
        bool isSuccess = false;
        bool isFailure = false;
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        await result.MatchAsync(Success, Failure, cancellationToken);

        // Then
        Assert.True(isSuccess);
        Assert.False(isFailure);

        return;

        ValueTask Success(CancellationToken ct)
        {
            isSuccess = true;
            return ValueTask.CompletedTask;
        }

        ValueTask Failure(Exception exception, CancellationToken ct)
        {
            isFailure = true;
            return ValueTask.CompletedTask;
        }
    }

    [Fact(DisplayName = "Result.MatchAsync(Func<CancellationToken, ValueTask>, Func<Exception, CancellationToken, ValueTask>) from success should produce the expected result when the delegates complete asynchronously.")]
    public async Task ResultMatchAsyncFuncCancellationTokenTaskFuncExceptionCancellationTokenTaskFromSuccessShouldProduceExpectedResultWithValueTaskDelegatesWhenDelegatesCompleteAsynchronously()
    {
        // Given
        Result result = Result.Success();
        bool isSuccess = false;
        bool isFailure = false;
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        await result.MatchAsync(Success, Failure, cancellationToken);

        // Then
        Assert.True(isSuccess);
        Assert.False(isFailure);

        return;

        ValueTask Success(CancellationToken ct)
        {
            isSuccess = true;
            return Yielding.CompletedValueTask();
        }

        ValueTask Failure(Exception exception, CancellationToken ct)
        {
            isFailure = true;
            return Yielding.CompletedValueTask();
        }
    }

    [Fact(DisplayName = "Result.MatchAsync(Func<CancellationToken, Task>, Func<Exception, CancellationToken, Task>) from failure should produce the expected result.")]
    public async Task ResultMatchAsyncFuncCancellationTokenTaskFuncExceptionCancellationTokenTaskFromFailureShouldProduceExpectedResult()
    {
        // Given
        Result result = Result.Failure(FailureException);
        bool isSuccess = false;
        bool isFailure = false;
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        await result.MatchAsync(Success, Failure, cancellationToken);

        // Then
        Assert.False(isSuccess);
        Assert.True(isFailure);

        return;

        Task Success(CancellationToken ct)
        {
            isSuccess = false;
            return Task.CompletedTask;
        }

        Task Failure(Exception exception, CancellationToken ct)
        {
            isFailure = true;
            return Task.CompletedTask;
        }
    }

    [Fact(DisplayName = "Result.MatchAsync(Func<CancellationToken, Task>, Func<Exception, CancellationToken, Task>) from failure should produce the expected result when the delegates complete asynchronously.")]
    public async Task ResultMatchAsyncFuncCancellationTokenTaskFuncExceptionCancellationTokenTaskFromFailureShouldProduceExpectedResultWhenDelegatesCompleteAsynchronously()
    {
        // Given
        Result result = Result.Failure(FailureException);
        bool isSuccess = false;
        bool isFailure = false;
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        await result.MatchAsync(Success, Failure, cancellationToken);

        // Then
        Assert.False(isSuccess);
        Assert.True(isFailure);

        return;

        Task Success(CancellationToken ct)
        {
            isSuccess = false;
            return Yielding.CompletedTask();
        }

        Task Failure(Exception exception, CancellationToken ct)
        {
            isFailure = true;
            return Yielding.CompletedTask();
        }
    }

    [Fact(DisplayName = "Result.MatchAsync(Func<CancellationToken, ValueTask>, Func<Exception, CancellationToken, ValueTask>) from failure should produce the expected result.")]
    public async Task ResultMatchAsyncFuncCancellationTokenTaskFuncExceptionCancellationTokenTaskFromFailureShouldProduceExpectedResultWithValueTaskDelegates()
    {
        // Given
        Result result = Result.Failure(FailureException);
        bool isSuccess = false;
        bool isFailure = false;
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        await result.MatchAsync(Success, Failure, cancellationToken);

        // Then
        Assert.False(isSuccess);
        Assert.True(isFailure);

        return;

        ValueTask Success(CancellationToken ct)
        {
            isSuccess = false;
            return ValueTask.CompletedTask;
        }

        ValueTask Failure(Exception exception, CancellationToken ct)
        {
            isFailure = true;
            return ValueTask.CompletedTask;
        }
    }

    [Fact(DisplayName = "Result.MatchAsync(Func<CancellationToken, ValueTask>, Func<Exception, CancellationToken, ValueTask>) from failure should produce the expected result when the delegates complete asynchronously.")]
    public async Task ResultMatchAsyncFuncCancellationTokenTaskFuncExceptionCancellationTokenTaskFromFailureShouldProduceExpectedResultWithValueTaskDelegatesWhenDelegatesCompleteAsynchronously()
    {
        // Given
        Result result = Result.Failure(FailureException);
        bool isSuccess = false;
        bool isFailure = false;
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        await result.MatchAsync(Success, Failure, cancellationToken);

        // Then
        Assert.False(isSuccess);
        Assert.True(isFailure);

        return;

        ValueTask Success(CancellationToken ct)
        {
            isSuccess = false;
            return Yielding.CompletedValueTask();
        }

        ValueTask Failure(Exception exception, CancellationToken ct)
        {
            isFailure = true;
            return Yielding.CompletedValueTask();
        }
    }

    [Fact(DisplayName = "Result.Match function from success should produce the expected result.")]
    public void ResultMatchFunctionFromSuccessShouldProduceExpectedResult()
    {
        // Given
        Result result = Result.Success();
        const int expected = 1;

        // When
        int actual = result.Match(Success, Failure);

        // Then
        Assert.Equal(expected, actual);

        return;
        int Success() => 1;
        int Failure(Exception exception) => 0;
    }

    [Fact(DisplayName = "Result.Match function from failure should produce the expected result.")]
    public void ResultMatchFunctionFromFailureShouldProduceExpectedResult()
    {
        // Given
        Result result = Result.Failure(FailureException);
        const int expected = 1;

        // When
        int actual = result.Match(Success, Failure);

        // Then
        Assert.Equal(expected, actual);

        return;
        int Success() => 0;
        int Failure(Exception exception) => 1;
    }

    [Fact(DisplayName = "Result.MatchAsync(Func<Task<TResult>>, Func<Exception, TResult>) from success should produce the expected result.")]
    public async Task ResultMatchAsyncFuncTaskTResultFuncExceptionTResultFromSuccessShouldProduceExpectedResult()
    {
        // Given
        Result result = Result.Success();
        const int expected = 1;

        // When
        int actual = await result.MatchAsync(Success, Failure, TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        Task<int> Success() => Task.FromResult(1);
        int Failure(Exception exception) => 0;
    }

    [Fact(DisplayName = "Result.MatchAsync(Func<Task<TResult>>, Func<Exception, TResult>) from success should produce the expected result when the delegates complete asynchronously.")]
    public async Task ResultMatchAsyncFuncTaskTResultFuncExceptionTResultFromSuccessShouldProduceExpectedResultWhenDelegatesCompleteAsynchronously()
    {
        // Given
        Result result = Result.Success();
        const int expected = 1;

        // When
        int actual = await result.MatchAsync(Success, Failure, TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        Task<int> Success() => Yielding.TaskOf(1);
        int Failure(Exception exception) => 0;
    }

    [Fact(DisplayName = "Result.MatchAsync(Func<ValueTask<TResult>>, Func<Exception, TResult>) from success should produce the expected result.")]
    public async Task ResultMatchAsyncFuncTaskTResultFuncExceptionTResultFromSuccessShouldProduceExpectedResultWithValueTaskDelegates()
    {
        // Given
        Result result = Result.Success();
        const int expected = 1;

        // When
        int actual = await result.MatchAsync(Success, Failure, TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        ValueTask<int> Success() => ValueTask.FromResult(1);
        int Failure(Exception exception) => 0;
    }

    [Fact(DisplayName = "Result.MatchAsync(Func<ValueTask<TResult>>, Func<Exception, TResult>) from success should produce the expected result when the delegates complete asynchronously.")]
    public async Task ResultMatchAsyncFuncTaskTResultFuncExceptionTResultFromSuccessShouldProduceExpectedResultWithValueTaskDelegatesWhenDelegatesCompleteAsynchronously()
    {
        // Given
        Result result = Result.Success();
        const int expected = 1;

        // When
        int actual = await result.MatchAsync(Success, Failure, TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        ValueTask<int> Success() => Yielding.ValueTaskOf(1);
        int Failure(Exception exception) => 0;
    }

    [Fact(DisplayName = "Result.MatchAsync(Func<Task<TResult>>, Func<Exception, TResult>) from failure should produce the expected result.")]
    public async Task ResultMatchAsyncFuncTaskTResultFuncExceptionTResultFromFailureShouldProduceExpectedResult()
    {
        // Given
        Result result = Result.Failure(FailureException);
        const int expected = 0;

        // When
        int actual = await result.MatchAsync(Success, Failure, TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        Task<int> Success() => Task.FromResult(1);
        int Failure(Exception exception) => 0;
    }

    [Fact(DisplayName = "Result.MatchAsync(Func<Task<TResult>>, Func<Exception, TResult>) from failure should produce the expected result when the delegates complete asynchronously.")]
    public async Task ResultMatchAsyncFuncTaskTResultFuncExceptionTResultFromFailureShouldProduceExpectedResultWhenDelegatesCompleteAsynchronously()
    {
        // Given
        Result result = Result.Failure(FailureException);
        const int expected = 0;

        // When
        int actual = await result.MatchAsync(Success, Failure, TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        Task<int> Success() => Yielding.TaskOf(1);
        int Failure(Exception exception) => 0;
    }

    [Fact(DisplayName = "Result.MatchAsync(Func<ValueTask<TResult>>, Func<Exception, TResult>) from failure should produce the expected result.")]
    public async Task ResultMatchAsyncFuncTaskTResultFuncExceptionTResultFromFailureShouldProduceExpectedResultWithValueTaskDelegates()
    {
        // Given
        Result result = Result.Failure(FailureException);
        const int expected = 0;

        // When
        int actual = await result.MatchAsync(Success, Failure, TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        ValueTask<int> Success() => ValueTask.FromResult(1);
        int Failure(Exception exception) => 0;
    }

    [Fact(DisplayName = "Result.MatchAsync(Func<ValueTask<TResult>>, Func<Exception, TResult>) from failure should produce the expected result when the delegates complete asynchronously.")]
    public async Task ResultMatchAsyncFuncTaskTResultFuncExceptionTResultFromFailureShouldProduceExpectedResultWithValueTaskDelegatesWhenDelegatesCompleteAsynchronously()
    {
        // Given
        Result result = Result.Failure(FailureException);
        const int expected = 0;

        // When
        int actual = await result.MatchAsync(Success, Failure, TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        ValueTask<int> Success() => Yielding.ValueTaskOf(1);
        int Failure(Exception exception) => 0;
    }

    [Fact(DisplayName = "Result.MatchAsync(Func<CancellationToken, Task<TResult>>, Func<Exception, TResult>) from success should produce the expected result.")]
    public async Task ResultMatchAsyncFuncCancellationTokenTaskTResultFuncExceptionTResultFromSuccessShouldProduceExpectedResult()
    {
        // Given
        Result result = Result.Success();
        const int expected = 1;
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        int actual = await result.MatchAsync(Success, Failure, cancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        Task<int> Success(CancellationToken ct) => Task.FromResult(1);
        int Failure(Exception exception) => 0;
    }

    [Fact(DisplayName = "Result.MatchAsync(Func<CancellationToken, Task<TResult>>, Func<Exception, TResult>) from success should produce the expected result when the delegates complete asynchronously.")]
    public async Task ResultMatchAsyncFuncCancellationTokenTaskTResultFuncExceptionTResultFromSuccessShouldProduceExpectedResultWhenDelegatesCompleteAsynchronously()
    {
        // Given
        Result result = Result.Success();
        const int expected = 1;
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        int actual = await result.MatchAsync(Success, Failure, cancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        Task<int> Success(CancellationToken ct) => Yielding.TaskOf(1);
        int Failure(Exception exception) => 0;
    }

    [Fact(DisplayName = "Result.MatchAsync(Func<CancellationToken, ValueTask<TResult>>, Func<Exception, TResult>) from success should produce the expected result.")]
    public async Task ResultMatchAsyncFuncCancellationTokenTaskTResultFuncExceptionTResultFromSuccessShouldProduceExpectedResultWithValueTaskDelegates()
    {
        // Given
        Result result = Result.Success();
        const int expected = 1;
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        int actual = await result.MatchAsync(Success, Failure, cancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        ValueTask<int> Success(CancellationToken ct) => ValueTask.FromResult(1);
        int Failure(Exception exception) => 0;
    }

    [Fact(DisplayName = "Result.MatchAsync(Func<CancellationToken, ValueTask<TResult>>, Func<Exception, TResult>) from success should produce the expected result when the delegates complete asynchronously.")]
    public async Task ResultMatchAsyncFuncCancellationTokenTaskTResultFuncExceptionTResultFromSuccessShouldProduceExpectedResultWithValueTaskDelegatesWhenDelegatesCompleteAsynchronously()
    {
        // Given
        Result result = Result.Success();
        const int expected = 1;
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        int actual = await result.MatchAsync(Success, Failure, cancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        ValueTask<int> Success(CancellationToken ct) => Yielding.ValueTaskOf(1);
        int Failure(Exception exception) => 0;
    }

    [Fact(DisplayName = "Result.MatchAsync(Func<CancellationToken, Task<TResult>>, Func<Exception, TResult>) from failure should produce the expected result.")]
    public async Task ResultMatchAsyncFuncCancellationTokenTaskTResultFuncExceptionTResultFromFailureShouldProduceExpectedResult()
    {
        // Given
        Result result = Result.Failure(FailureException);
        const int expected = 0;
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        int actual = await result.MatchAsync(Success, Failure, cancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        Task<int> Success(CancellationToken ct) => Task.FromResult(1);
        int Failure(Exception exception) => 0;
    }

    [Fact(DisplayName = "Result.MatchAsync(Func<CancellationToken, Task<TResult>>, Func<Exception, TResult>) from failure should produce the expected result when the delegates complete asynchronously.")]
    public async Task ResultMatchAsyncFuncCancellationTokenTaskTResultFuncExceptionTResultFromFailureShouldProduceExpectedResultWhenDelegatesCompleteAsynchronously()
    {
        // Given
        Result result = Result.Failure(FailureException);
        const int expected = 0;
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        int actual = await result.MatchAsync(Success, Failure, cancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        Task<int> Success(CancellationToken ct) => Yielding.TaskOf(1);
        int Failure(Exception exception) => 0;
    }

    [Fact(DisplayName = "Result.MatchAsync(Func<CancellationToken, ValueTask<TResult>>, Func<Exception, TResult>) from failure should produce the expected result.")]
    public async Task ResultMatchAsyncFuncCancellationTokenTaskTResultFuncExceptionTResultFromFailureShouldProduceExpectedResultWithValueTaskDelegates()
    {
        // Given
        Result result = Result.Failure(FailureException);
        const int expected = 0;
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        int actual = await result.MatchAsync(Success, Failure, cancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        ValueTask<int> Success(CancellationToken ct) => ValueTask.FromResult(1);
        int Failure(Exception exception) => 0;
    }

    [Fact(DisplayName = "Result.MatchAsync(Func<CancellationToken, ValueTask<TResult>>, Func<Exception, TResult>) from failure should produce the expected result when the delegates complete asynchronously.")]
    public async Task ResultMatchAsyncFuncCancellationTokenTaskTResultFuncExceptionTResultFromFailureShouldProduceExpectedResultWithValueTaskDelegatesWhenDelegatesCompleteAsynchronously()
    {
        // Given
        Result result = Result.Failure(FailureException);
        const int expected = 0;
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        int actual = await result.MatchAsync(Success, Failure, cancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        ValueTask<int> Success(CancellationToken ct) => Yielding.ValueTaskOf(1);
        int Failure(Exception exception) => 0;
    }

    [Fact(DisplayName = "Result.MatchAsync(Func<TResult>, Func<Exception, Task<TResult>>) from success should produce the expected result.")]
    public async Task ResultMatchAsyncFuncTResultFuncExceptionTaskTResultFromSuccessShouldProduceExpectedResult()
    {
        // Given
        Result result = Result.Success();
        const int expected = 1;

        // When
        int actual = await result.MatchAsync(Success, Failure, TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        int Success() => 1;
        Task<int> Failure(Exception exception) => Task.FromResult(0);
    }

    [Fact(DisplayName = "Result.MatchAsync(Func<TResult>, Func<Exception, Task<TResult>>) from success should produce the expected result when the delegates complete asynchronously.")]
    public async Task ResultMatchAsyncFuncTResultFuncExceptionTaskTResultFromSuccessShouldProduceExpectedResultWhenDelegatesCompleteAsynchronously()
    {
        // Given
        Result result = Result.Success();
        const int expected = 1;

        // When
        int actual = await result.MatchAsync(Success, Failure, TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        int Success() => 1;
        Task<int> Failure(Exception exception) => Yielding.TaskOf(0);
    }

    [Fact(DisplayName = "Result.MatchAsync(Func<TResult>, Func<Exception, ValueTask<TResult>>) from success should produce the expected result.")]
    public async Task ResultMatchAsyncFuncTResultFuncExceptionTaskTResultFromSuccessShouldProduceExpectedResultWithValueTaskDelegates()
    {
        // Given
        Result result = Result.Success();
        const int expected = 1;

        // When
        int actual = await result.MatchAsync(Success, Failure, TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        int Success() => 1;
        ValueTask<int> Failure(Exception exception) => ValueTask.FromResult(0);
    }

    [Fact(DisplayName = "Result.MatchAsync(Func<TResult>, Func<Exception, ValueTask<TResult>>) from success should produce the expected result when the delegates complete asynchronously.")]
    public async Task ResultMatchAsyncFuncTResultFuncExceptionTaskTResultFromSuccessShouldProduceExpectedResultWithValueTaskDelegatesWhenDelegatesCompleteAsynchronously()
    {
        // Given
        Result result = Result.Success();
        const int expected = 1;

        // When
        int actual = await result.MatchAsync(Success, Failure, TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        int Success() => 1;
        ValueTask<int> Failure(Exception exception) => Yielding.ValueTaskOf(0);
    }

    [Fact(DisplayName = "Result.MatchAsync(Func<TResult>, Func<Exception, Task<TResult>>) from failure should produce the expected result.")]
    public async Task ResultMatchAsyncFuncTResultFuncExceptionTaskTResultFromFailureShouldProduceExpectedResult()
    {
        // Given
        Result result = Result.Failure(FailureException);
        const int expected = 0;

        // When
        int actual = await result.MatchAsync(Success, Failure, TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        int Success() => 1;
        Task<int> Failure(Exception exception) => Task.FromResult(0);
    }

    [Fact(DisplayName = "Result.MatchAsync(Func<TResult>, Func<Exception, Task<TResult>>) from failure should produce the expected result when the delegates complete asynchronously.")]
    public async Task ResultMatchAsyncFuncTResultFuncExceptionTaskTResultFromFailureShouldProduceExpectedResultWhenDelegatesCompleteAsynchronously()
    {
        // Given
        Result result = Result.Failure(FailureException);
        const int expected = 0;

        // When
        int actual = await result.MatchAsync(Success, Failure, TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        int Success() => 1;
        Task<int> Failure(Exception exception) => Yielding.TaskOf(0);
    }

    [Fact(DisplayName = "Result.MatchAsync(Func<TResult>, Func<Exception, ValueTask<TResult>>) from failure should produce the expected result.")]
    public async Task ResultMatchAsyncFuncTResultFuncExceptionTaskTResultFromFailureShouldProduceExpectedResultWithValueTaskDelegates()
    {
        // Given
        Result result = Result.Failure(FailureException);
        const int expected = 0;

        // When
        int actual = await result.MatchAsync(Success, Failure, TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        int Success() => 1;
        ValueTask<int> Failure(Exception exception) => ValueTask.FromResult(0);
    }

    [Fact(DisplayName = "Result.MatchAsync(Func<TResult>, Func<Exception, ValueTask<TResult>>) from failure should produce the expected result when the delegates complete asynchronously.")]
    public async Task ResultMatchAsyncFuncTResultFuncExceptionTaskTResultFromFailureShouldProduceExpectedResultWithValueTaskDelegatesWhenDelegatesCompleteAsynchronously()
    {
        // Given
        Result result = Result.Failure(FailureException);
        const int expected = 0;

        // When
        int actual = await result.MatchAsync(Success, Failure, TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        int Success() => 1;
        ValueTask<int> Failure(Exception exception) => Yielding.ValueTaskOf(0);
    }

    [Fact(DisplayName = "Result.MatchAsync(Func<TResult>, Func<Exception, CancellationToken, Task<TResult>>) from success should produce the expected result.")]
    public async Task ResultMatchAsyncFuncTResultFuncExceptionCancellationTokenTaskTResultFromSuccessShouldProduceExpectedResult()
    {
        // Given
        Result result = Result.Success();
        const int expected = 1;
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        int actual = await result.MatchAsync(Success, Failure, cancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        int Success() => 1;
        Task<int> Failure(Exception exception, CancellationToken ct) => Task.FromResult(0);
    }

    [Fact(DisplayName = "Result.MatchAsync(Func<TResult>, Func<Exception, CancellationToken, Task<TResult>>) from success should produce the expected result when the delegates complete asynchronously.")]
    public async Task ResultMatchAsyncFuncTResultFuncExceptionCancellationTokenTaskTResultFromSuccessShouldProduceExpectedResultWhenDelegatesCompleteAsynchronously()
    {
        // Given
        Result result = Result.Success();
        const int expected = 1;
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        int actual = await result.MatchAsync(Success, Failure, cancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        int Success() => 1;
        Task<int> Failure(Exception exception, CancellationToken ct) => Yielding.TaskOf(0);
    }

    [Fact(DisplayName = "Result.MatchAsync(Func<TResult>, Func<Exception, CancellationToken, ValueTask<TResult>>) from success should produce the expected result.")]
    public async Task ResultMatchAsyncFuncTResultFuncExceptionCancellationTokenTaskTResultFromSuccessShouldProduceExpectedResultWithValueTaskDelegates()
    {
        // Given
        Result result = Result.Success();
        const int expected = 1;
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        int actual = await result.MatchAsync(Success, Failure, cancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        int Success() => 1;
        ValueTask<int> Failure(Exception exception, CancellationToken ct) => ValueTask.FromResult(0);
    }

    [Fact(DisplayName = "Result.MatchAsync(Func<TResult>, Func<Exception, CancellationToken, ValueTask<TResult>>) from success should produce the expected result when the delegates complete asynchronously.")]
    public async Task ResultMatchAsyncFuncTResultFuncExceptionCancellationTokenTaskTResultFromSuccessShouldProduceExpectedResultWithValueTaskDelegatesWhenDelegatesCompleteAsynchronously()
    {
        // Given
        Result result = Result.Success();
        const int expected = 1;
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        int actual = await result.MatchAsync(Success, Failure, cancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        int Success() => 1;
        ValueTask<int> Failure(Exception exception, CancellationToken ct) => Yielding.ValueTaskOf(0);
    }

    [Fact(DisplayName = "Result.MatchAsync(Func<TResult>, Func<Exception, CancellationToken, Task<TResult>>) from failure should produce the expected result.")]
    public async Task ResultMatchAsyncFuncTResultFuncExceptionCancellationTokenTaskTResultFromFailureShouldProduceExpectedResult()
    {
        // Given
        Result result = Result.Failure(FailureException);
        const int expected = 0;
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        int actual = await result.MatchAsync(Success, Failure, cancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        int Success() => 1;
        Task<int> Failure(Exception exception, CancellationToken ct) => Task.FromResult(0);
    }

    [Fact(DisplayName = "Result.MatchAsync(Func<TResult>, Func<Exception, CancellationToken, Task<TResult>>) from failure should produce the expected result when the delegates complete asynchronously.")]
    public async Task ResultMatchAsyncFuncTResultFuncExceptionCancellationTokenTaskTResultFromFailureShouldProduceExpectedResultWhenDelegatesCompleteAsynchronously()
    {
        // Given
        Result result = Result.Failure(FailureException);
        const int expected = 0;
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        int actual = await result.MatchAsync(Success, Failure, cancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        int Success() => 1;
        Task<int> Failure(Exception exception, CancellationToken ct) => Yielding.TaskOf(0);
    }

    [Fact(DisplayName = "Result.MatchAsync(Func<TResult>, Func<Exception, CancellationToken, ValueTask<TResult>>) from failure should produce the expected result.")]
    public async Task ResultMatchAsyncFuncTResultFuncExceptionCancellationTokenTaskTResultFromFailureShouldProduceExpectedResultWithValueTaskDelegates()
    {
        // Given
        Result result = Result.Failure(FailureException);
        const int expected = 0;
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        int actual = await result.MatchAsync(Success, Failure, cancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        int Success() => 1;
        ValueTask<int> Failure(Exception exception, CancellationToken ct) => ValueTask.FromResult(0);
    }

    [Fact(DisplayName = "Result.MatchAsync(Func<TResult>, Func<Exception, CancellationToken, ValueTask<TResult>>) from failure should produce the expected result when the delegates complete asynchronously.")]
    public async Task ResultMatchAsyncFuncTResultFuncExceptionCancellationTokenTaskTResultFromFailureShouldProduceExpectedResultWithValueTaskDelegatesWhenDelegatesCompleteAsynchronously()
    {
        // Given
        Result result = Result.Failure(FailureException);
        const int expected = 0;
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        int actual = await result.MatchAsync(Success, Failure, cancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        int Success() => 1;
        ValueTask<int> Failure(Exception exception, CancellationToken ct) => Yielding.ValueTaskOf(0);
    }

    [Fact(DisplayName = "Result.MatchAsync(Func<Task<TResult>>, Func<Exception, Task<TResult>>) from success should produce the expected result.")]
    public async Task ResultMatchAsyncFuncTaskTResultFuncExceptionTaskTResultFromSuccessShouldProduceExpectedResult()
    {
        // Given
        Result result = Result.Success();
        const int expected = 1;
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        int actual = await result.MatchAsync(Success, Failure, cancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        Task<int> Success() => Task.FromResult(1);
        Task<int> Failure(Exception exception) => Task.FromResult(0);
    }

    [Fact(DisplayName = "Result.MatchAsync(Func<Task<TResult>>, Func<Exception, Task<TResult>>) from success should produce the expected result when the delegates complete asynchronously.")]
    public async Task ResultMatchAsyncFuncTaskTResultFuncExceptionTaskTResultFromSuccessShouldProduceExpectedResultWhenDelegatesCompleteAsynchronously()
    {
        // Given
        Result result = Result.Success();
        const int expected = 1;
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        int actual = await result.MatchAsync(Success, Failure, cancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        Task<int> Success() => Yielding.TaskOf(1);
        Task<int> Failure(Exception exception) => Yielding.TaskOf(0);
    }

    [Fact(DisplayName = "Result.MatchAsync(Func<ValueTask<TResult>>, Func<Exception, ValueTask<TResult>>) from success should produce the expected result.")]
    public async Task ResultMatchAsyncFuncTaskTResultFuncExceptionTaskTResultFromSuccessShouldProduceExpectedResultWithValueTaskDelegates()
    {
        // Given
        Result result = Result.Success();
        const int expected = 1;
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        int actual = await result.MatchAsync(Success, Failure, cancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        ValueTask<int> Success() => ValueTask.FromResult(1);
        ValueTask<int> Failure(Exception exception) => ValueTask.FromResult(0);
    }

    [Fact(DisplayName = "Result.MatchAsync(Func<ValueTask<TResult>>, Func<Exception, ValueTask<TResult>>) from success should produce the expected result when the delegates complete asynchronously.")]
    public async Task ResultMatchAsyncFuncTaskTResultFuncExceptionTaskTResultFromSuccessShouldProduceExpectedResultWithValueTaskDelegatesWhenDelegatesCompleteAsynchronously()
    {
        // Given
        Result result = Result.Success();
        const int expected = 1;
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        int actual = await result.MatchAsync(Success, Failure, cancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        ValueTask<int> Success() => Yielding.ValueTaskOf(1);
        ValueTask<int> Failure(Exception exception) => Yielding.ValueTaskOf(0);
    }

    [Fact(DisplayName = "Result.MatchAsync(Func<Task<TResult>>, Func<Exception, Task<TResult>>) from failure should produce the expected result.")]
    public async Task ResultMatchAsyncFuncTaskTResultFuncExceptionTaskTResultFromFailureShouldProduceExpectedResult()
    {
        // Given
        Result result = Result.Failure(FailureException);
        const int expected = 0;
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        int actual = await result.MatchAsync(Success, Failure, cancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        Task<int> Success() => Task.FromResult(1);
        Task<int> Failure(Exception exception) => Task.FromResult(0);
    }

    [Fact(DisplayName = "Result.MatchAsync(Func<Task<TResult>>, Func<Exception, Task<TResult>>) from failure should produce the expected result when the delegates complete asynchronously.")]
    public async Task ResultMatchAsyncFuncTaskTResultFuncExceptionTaskTResultFromFailureShouldProduceExpectedResultWhenDelegatesCompleteAsynchronously()
    {
        // Given
        Result result = Result.Failure(FailureException);
        const int expected = 0;
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        int actual = await result.MatchAsync(Success, Failure, cancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        Task<int> Success() => Yielding.TaskOf(1);
        Task<int> Failure(Exception exception) => Yielding.TaskOf(0);
    }

    [Fact(DisplayName = "Result.MatchAsync(Func<ValueTask<TResult>>, Func<Exception, ValueTask<TResult>>) from failure should produce the expected result.")]
    public async Task ResultMatchAsyncFuncTaskTResultFuncExceptionTaskTResultFromFailureShouldProduceExpectedResultWithValueTaskDelegates()
    {
        // Given
        Result result = Result.Failure(FailureException);
        const int expected = 0;
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        int actual = await result.MatchAsync(Success, Failure, cancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        ValueTask<int> Success() => ValueTask.FromResult(1);
        ValueTask<int> Failure(Exception exception) => ValueTask.FromResult(0);
    }

    [Fact(DisplayName = "Result.MatchAsync(Func<ValueTask<TResult>>, Func<Exception, ValueTask<TResult>>) from failure should produce the expected result when the delegates complete asynchronously.")]
    public async Task ResultMatchAsyncFuncTaskTResultFuncExceptionTaskTResultFromFailureShouldProduceExpectedResultWithValueTaskDelegatesWhenDelegatesCompleteAsynchronously()
    {
        // Given
        Result result = Result.Failure(FailureException);
        const int expected = 0;
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        int actual = await result.MatchAsync(Success, Failure, cancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        ValueTask<int> Success() => Yielding.ValueTaskOf(1);
        ValueTask<int> Failure(Exception exception) => Yielding.ValueTaskOf(0);
    }

    [Fact(DisplayName = "Result.MatchAsync(Func<CancellationToken, Task<TResult>>, Func<Exception, CancellationToken, Task<TResult>>) from success should produce the expected result.")]
    public async Task ResultMatchAsyncFuncCancellationTokenTaskTResultFuncExceptionCancellationTokenTaskTResultFromSuccessShouldProduceExpectedResult()
    {
        // Given
        Result result = Result.Success();
        const int expected = 1;
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        int actual = await result.MatchAsync(Success, Failure, cancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        Task<int> Success(CancellationToken ct) => Task.FromResult(1);
        Task<int> Failure(Exception exception, CancellationToken ct) => Task.FromResult(0);
    }

    [Fact(DisplayName = "Result.MatchAsync(Func<CancellationToken, Task<TResult>>, Func<Exception, CancellationToken, Task<TResult>>) from success should produce the expected result when the delegates complete asynchronously.")]
    public async Task ResultMatchAsyncFuncCancellationTokenTaskTResultFuncExceptionCancellationTokenTaskTResultFromSuccessShouldProduceExpectedResultWhenDelegatesCompleteAsynchronously()
    {
        // Given
        Result result = Result.Success();
        const int expected = 1;
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        int actual = await result.MatchAsync(Success, Failure, cancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        Task<int> Success(CancellationToken ct) => Yielding.TaskOf(1);
        Task<int> Failure(Exception exception, CancellationToken ct) => Yielding.TaskOf(0);
    }

    [Fact(DisplayName = "Result.MatchAsync(Func<CancellationToken, ValueTask<TResult>>, Func<Exception, CancellationToken, ValueTask<TResult>>) from success should produce the expected result.")]
    public async Task ResultMatchAsyncFuncCancellationTokenTaskTResultFuncExceptionCancellationTokenTaskTResultFromSuccessShouldProduceExpectedResultWithValueTaskDelegates()
    {
        // Given
        Result result = Result.Success();
        const int expected = 1;
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        int actual = await result.MatchAsync(Success, Failure, cancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        ValueTask<int> Success(CancellationToken ct) => ValueTask.FromResult(1);
        ValueTask<int> Failure(Exception exception, CancellationToken ct) => ValueTask.FromResult(0);
    }

    [Fact(DisplayName = "Result.MatchAsync(Func<CancellationToken, ValueTask<TResult>>, Func<Exception, CancellationToken, ValueTask<TResult>>) from success should produce the expected result when the delegates complete asynchronously.")]
    public async Task ResultMatchAsyncFuncCancellationTokenTaskTResultFuncExceptionCancellationTokenTaskTResultFromSuccessShouldProduceExpectedResultWithValueTaskDelegatesWhenDelegatesCompleteAsynchronously()
    {
        // Given
        Result result = Result.Success();
        const int expected = 1;
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        int actual = await result.MatchAsync(Success, Failure, cancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        ValueTask<int> Success(CancellationToken ct) => Yielding.ValueTaskOf(1);
        ValueTask<int> Failure(Exception exception, CancellationToken ct) => Yielding.ValueTaskOf(0);
    }

    [Fact(DisplayName = "Result.MatchAsync(Func<CancellationToken, Task<TResult>>, Func<Exception, CancellationToken, Task<TResult>>) from failure should produce the expected result.")]
    public async Task ResultMatchAsyncFuncCancellationTokenTaskTResultFuncExceptionCancellationTokenTaskTResultFromFailureShouldProduceExpectedResult()
    {
        // Given
        Result result = Result.Failure(FailureException);
        const int expected = 0;
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        int actual = await result.MatchAsync(Success, Failure, cancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        Task<int> Success(CancellationToken ct) => Task.FromResult(1);
        Task<int> Failure(Exception exception, CancellationToken ct) => Task.FromResult(0);
    }

    [Fact(DisplayName = "Result.MatchAsync(Func<CancellationToken, Task<TResult>>, Func<Exception, CancellationToken, Task<TResult>>) from failure should produce the expected result when the delegates complete asynchronously.")]
    public async Task ResultMatchAsyncFuncCancellationTokenTaskTResultFuncExceptionCancellationTokenTaskTResultFromFailureShouldProduceExpectedResultWhenDelegatesCompleteAsynchronously()
    {
        // Given
        Result result = Result.Failure(FailureException);
        const int expected = 0;
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        int actual = await result.MatchAsync(Success, Failure, cancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        Task<int> Success(CancellationToken ct) => Yielding.TaskOf(1);
        Task<int> Failure(Exception exception, CancellationToken ct) => Yielding.TaskOf(0);
    }

    [Fact(DisplayName = "Result.MatchAsync(Func<CancellationToken, ValueTask<TResult>>, Func<Exception, CancellationToken, ValueTask<TResult>>) from failure should produce the expected result.")]
    public async Task ResultMatchAsyncFuncCancellationTokenTaskTResultFuncExceptionCancellationTokenTaskTResultFromFailureShouldProduceExpectedResultWithValueTaskDelegates()
    {
        // Given
        Result result = Result.Failure(FailureException);
        const int expected = 0;
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        int actual = await result.MatchAsync(Success, Failure, cancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        ValueTask<int> Success(CancellationToken ct) => ValueTask.FromResult(1);
        ValueTask<int> Failure(Exception exception, CancellationToken ct) => ValueTask.FromResult(0);
    }

    [Fact(DisplayName = "Result.MatchAsync(Func<CancellationToken, ValueTask<TResult>>, Func<Exception, CancellationToken, ValueTask<TResult>>) from failure should produce the expected result when the delegates complete asynchronously.")]
    public async Task ResultMatchAsyncFuncCancellationTokenTaskTResultFuncExceptionCancellationTokenTaskTResultFromFailureShouldProduceExpectedResultWithValueTaskDelegatesWhenDelegatesCompleteAsynchronously()
    {
        // Given
        Result result = Result.Failure(FailureException);
        const int expected = 0;
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        int actual = await result.MatchAsync(Success, Failure, cancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        ValueTask<int> Success(CancellationToken ct) => Yielding.ValueTaskOf(1);
        ValueTask<int> Failure(Exception exception, CancellationToken ct) => Yielding.ValueTaskOf(0);
    }

    [Fact(DisplayName = "Result.Select action from failure should produce the expected result.")]
    public void ResultSelectActionFromFailureShouldProduceExpectedResult()
    {
        // Given
        Result result = Result.Failure(FailureException);
        Result expected = Result.Failure(FailureException);

        // Then
        Result actual = result.Select(() => { });

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result.Select action from success should produce the expected result.")]
    public void ResultSelectActionFromSuccessShouldProduceExpectedResult()
    {
        // Given
        Result result = Result.Success();
        Result expected = Result.Success();

        // Then
        Result actual = result.Select(() => { });

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result.Select action from success with exception should produce the expected result.")]
    public void ResultSelectActionFromSuccessWithExceptionShouldProduceExpectedResult()
    {
        // Given
        Result result = Result.Success();
        Result expected = Result.Failure(FailureException);

        // Then
        Result actual = result.Select(() => throw FailureException);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result.SelectAsync(Func<Task>) from failure should produce the expected result.")]
    public async Task ResultSelectAsyncFuncTaskFromFailureShouldProduceExpectedResult()
    {
        // Given
        Result result = Result.Failure(FailureException);
        Result expected = Result.Failure(FailureException);

        // When
        Result actual = await result.SelectAsync(() => Task.CompletedTask, TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result.SelectAsync(Func<Task>) from failure should produce the expected result when the delegates complete asynchronously.")]
    public async Task ResultSelectAsyncFuncTaskFromFailureShouldProduceExpectedResultWhenDelegatesCompleteAsynchronously()
    {
        // Given
        Result result = Result.Failure(FailureException);
        Result expected = Result.Failure(FailureException);

        // When
        Result actual = await result.SelectAsync(() => Yielding.CompletedTask(), TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result.SelectAsync(Func<ValueTask>) from failure should produce the expected result.")]
    public async Task ResultSelectAsyncFuncTaskFromFailureShouldProduceExpectedResultWithValueTaskDelegates()
    {
        // Given
        Result result = Result.Failure(FailureException);
        Result expected = Result.Failure(FailureException);

        // When
        Result actual = await result.SelectAsync(() => ValueTask.CompletedTask, TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result.SelectAsync(Func<ValueTask>) from failure should produce the expected result when the delegates complete asynchronously.")]
    public async Task ResultSelectAsyncFuncTaskFromFailureShouldProduceExpectedResultWithValueTaskDelegatesWhenDelegatesCompleteAsynchronously()
    {
        // Given
        Result result = Result.Failure(FailureException);
        Result expected = Result.Failure(FailureException);

        // When
        Result actual = await result.SelectAsync(() => Yielding.CompletedValueTask(), TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result.SelectAsync(Func<Task>) from success should produce the expected result.")]
    public async Task ResultSelectAsyncFuncTaskFromSuccessShouldProduceExpectedResult()
    {
        // Given
        Result result = Result.Success();
        Result expected = Result.Success();

        // When
        Result actual = await result.SelectAsync(() => Task.CompletedTask, TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result.SelectAsync(Func<Task>) from success should produce the expected result when the delegates complete asynchronously.")]
    public async Task ResultSelectAsyncFuncTaskFromSuccessShouldProduceExpectedResultWhenDelegatesCompleteAsynchronously()
    {
        // Given
        Result result = Result.Success();
        Result expected = Result.Success();

        // When
        Result actual = await result.SelectAsync(() => Yielding.CompletedTask(), TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result.SelectAsync(Func<ValueTask>) from success should produce the expected result.")]
    public async Task ResultSelectAsyncFuncTaskFromSuccessShouldProduceExpectedResultWithValueTaskDelegates()
    {
        // Given
        Result result = Result.Success();
        Result expected = Result.Success();

        // When
        Result actual = await result.SelectAsync(() => ValueTask.CompletedTask, TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result.SelectAsync(Func<ValueTask>) from success should produce the expected result when the delegates complete asynchronously.")]
    public async Task ResultSelectAsyncFuncTaskFromSuccessShouldProduceExpectedResultWithValueTaskDelegatesWhenDelegatesCompleteAsynchronously()
    {
        // Given
        Result result = Result.Success();
        Result expected = Result.Success();

        // When
        Result actual = await result.SelectAsync(() => Yielding.CompletedValueTask(), TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result.SelectAsync(Func<Task>) from success with exception should produce the expected result.")]
    public async Task ResultSelectAsyncFuncTaskFromSuccessWithExceptionShouldProduceExpectedResult()
    {
        // Given
        Result result = Result.Success();
        Result expected = Result.Failure(FailureException);

        // When
        Result actual = await result.SelectAsync(Task () => throw FailureException, TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result.SelectAsync(Func<ValueTask>) from success with exception should produce the expected result.")]
    public async Task ResultSelectAsyncFuncTaskFromSuccessWithExceptionShouldProduceExpectedResultWithValueTaskDelegates()
    {
        // Given
        Result result = Result.Success();
        Result expected = Result.Failure(FailureException);

        // When
        Result actual = await result.SelectAsync(ValueTask () => throw FailureException, TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result.SelectAsync(Func<CancellationToken, Task>) from failure should produce the expected result.")]
    public async Task ResultSelectAsyncFuncCancellationTokenTaskFromFailureShouldProduceExpectedResult()
    {
        // Given
        Result result = Result.Failure(FailureException);
        Result expected = Result.Failure(FailureException);
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        Result actual = await result.SelectAsync(_ => Task.CompletedTask, cancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result.SelectAsync(Func<CancellationToken, Task>) from failure should produce the expected result when the delegates complete asynchronously.")]
    public async Task ResultSelectAsyncFuncCancellationTokenTaskFromFailureShouldProduceExpectedResultWhenDelegatesCompleteAsynchronously()
    {
        // Given
        Result result = Result.Failure(FailureException);
        Result expected = Result.Failure(FailureException);
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        Result actual = await result.SelectAsync(_ => Yielding.CompletedTask(), cancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result.SelectAsync(Func<CancellationToken, ValueTask>) from failure should produce the expected result.")]
    public async Task ResultSelectAsyncFuncCancellationTokenTaskFromFailureShouldProduceExpectedResultWithValueTaskDelegates()
    {
        // Given
        Result result = Result.Failure(FailureException);
        Result expected = Result.Failure(FailureException);
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        Result actual = await result.SelectAsync(_ => ValueTask.CompletedTask, cancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result.SelectAsync(Func<CancellationToken, ValueTask>) from failure should produce the expected result when the delegates complete asynchronously.")]
    public async Task ResultSelectAsyncFuncCancellationTokenTaskFromFailureShouldProduceExpectedResultWithValueTaskDelegatesWhenDelegatesCompleteAsynchronously()
    {
        // Given
        Result result = Result.Failure(FailureException);
        Result expected = Result.Failure(FailureException);
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        Result actual = await result.SelectAsync(_ => Yielding.CompletedValueTask(), cancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result.SelectAsync(Func<CancellationToken, Task>) from success should produce the expected result.")]
    public async Task ResultSelectAsyncFuncCancellationTokenTaskFromSuccessShouldProduceExpectedResult()
    {
        // Given
        Result result = Result.Success();
        Result expected = Result.Success();
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        Result actual = await result.SelectAsync(_ => Task.CompletedTask, cancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result.SelectAsync(Func<CancellationToken, Task>) from success should produce the expected result when the delegates complete asynchronously.")]
    public async Task ResultSelectAsyncFuncCancellationTokenTaskFromSuccessShouldProduceExpectedResultWhenDelegatesCompleteAsynchronously()
    {
        // Given
        Result result = Result.Success();
        Result expected = Result.Success();
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        Result actual = await result.SelectAsync(_ => Yielding.CompletedTask(), cancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result.SelectAsync(Func<CancellationToken, ValueTask>) from success should produce the expected result.")]
    public async Task ResultSelectAsyncFuncCancellationTokenTaskFromSuccessShouldProduceExpectedResultWithValueTaskDelegates()
    {
        // Given
        Result result = Result.Success();
        Result expected = Result.Success();
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        Result actual = await result.SelectAsync(_ => ValueTask.CompletedTask, cancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result.SelectAsync(Func<CancellationToken, ValueTask>) from success should produce the expected result when the delegates complete asynchronously.")]
    public async Task ResultSelectAsyncFuncCancellationTokenTaskFromSuccessShouldProduceExpectedResultWithValueTaskDelegatesWhenDelegatesCompleteAsynchronously()
    {
        // Given
        Result result = Result.Success();
        Result expected = Result.Success();
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        Result actual = await result.SelectAsync(_ => Yielding.CompletedValueTask(), cancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result.SelectAsync(Func<CancellationToken, Task>) from success with exception should produce the expected result.")]
    public async Task ResultSelectAsyncFuncCancellationTokenTaskFromSuccessWithExceptionShouldProduceExpectedResult()
    {
        // Given
        Result result = Result.Success();
        Result expected = Result.Failure(FailureException);
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        Result actual = await result.SelectAsync(Task (_) => throw FailureException, cancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result.SelectAsync(Func<CancellationToken, ValueTask>) from success with exception should produce the expected result.")]
    public async Task ResultSelectAsyncFuncCancellationTokenTaskFromSuccessWithExceptionShouldProduceExpectedResultWithValueTaskDelegates()
    {
        // Given
        Result result = Result.Success();
        Result expected = Result.Failure(FailureException);
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        Result actual = await result.SelectAsync(ValueTask (_) => throw FailureException, cancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result.Select function of TResult from failure should produce the expected result.")]
    public void ResultSelectFunctionOfTResultFromFailureShouldProduceExpectedResult()
    {
        // Given
        Result result = Result.Failure(FailureException);
        Result<int> expected = Result<int>.Failure(FailureException);

        // Then
        Result<int> actual = result.Select(() => 1);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result.Select function of TResult from success should produce the expected result.")]
    public void ResultSelectFunctionOfTResultFromSuccessShouldProduceExpectedResult()
    {
        // Given
        Result result = Result.Success();
        Result<int> expected = 1;

        // Then
        Result<int> actual = result.Select(() => 1);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result.Select function of TResult from success with exception should produce the expected result.")]
    public void ResultSelectFunctionOfTResultFromSuccessWithExceptionShouldProduceExpectedResult()
    {
        // Given
        Result result = Result.Success();
        Result<int> expected = Result<int>.Failure(FailureException);

        // Then
        Result<int> actual = result.Select<int>(() => throw FailureException);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result.SelectAsync function of TResult from failure should produce the expected result.")]
    public async Task ResultSelectAsyncFunctionOfTResultFromFailureShouldProduceExpectedResult()
    {
        // Given
        Result result = Result.Failure(FailureException);
        Result<int> expected = Result<int>.Failure(FailureException);

        // When
        Result<int> actual = await result.SelectAsync(() => Task.FromResult(1), TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result.SelectAsync function of TResult from failure should produce the expected result when the delegates complete asynchronously.")]
    public async Task ResultSelectAsyncFunctionOfTResultFromFailureShouldProduceExpectedResultWhenDelegatesCompleteAsynchronously()
    {
        // Given
        Result result = Result.Failure(FailureException);
        Result<int> expected = Result<int>.Failure(FailureException);

        // When
        Result<int> actual = await result.SelectAsync(() => Yielding.TaskOf(1), TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result.SelectAsync function of TResult from failure should produce the expected result with ValueTask delegates.")]
    public async Task ResultSelectAsyncFunctionOfTResultFromFailureShouldProduceExpectedResultWithValueTaskDelegates()
    {
        // Given
        Result result = Result.Failure(FailureException);
        Result<int> expected = Result<int>.Failure(FailureException);

        // When
        Result<int> actual = await result.SelectAsync(() => ValueTask.FromResult(1), TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result.SelectAsync function of TResult from failure should produce the expected result with ValueTask delegates when the delegates complete asynchronously.")]
    public async Task ResultSelectAsyncFunctionOfTResultFromFailureShouldProduceExpectedResultWithValueTaskDelegatesWhenDelegatesCompleteAsynchronously()
    {
        // Given
        Result result = Result.Failure(FailureException);
        Result<int> expected = Result<int>.Failure(FailureException);

        // When
        Result<int> actual = await result.SelectAsync(() => Yielding.ValueTaskOf(1), TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result.SelectAsync function of TResult from success should produce the expected result.")]
    public async Task ResultSelectAsyncFunctionOfTResultFromSuccessShouldProduceExpectedResult()
    {
        // Given
        Result result = Result.Success();
        Result<int> expected = 1;

        // When
        Result<int> actual = await result.SelectAsync(() => Task.FromResult(1), TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result.SelectAsync function of TResult from success should produce the expected result when the delegates complete asynchronously.")]
    public async Task ResultSelectAsyncFunctionOfTResultFromSuccessShouldProduceExpectedResultWhenDelegatesCompleteAsynchronously()
    {
        // Given
        Result result = Result.Success();
        Result<int> expected = 1;

        // When
        Result<int> actual = await result.SelectAsync(() => Yielding.TaskOf(1), TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result.SelectAsync function of TResult from success should produce the expected result with ValueTask delegates.")]
    public async Task ResultSelectAsyncFunctionOfTResultFromSuccessShouldProduceExpectedResultWithValueTaskDelegates()
    {
        // Given
        Result result = Result.Success();
        Result<int> expected = 1;

        // When
        Result<int> actual = await result.SelectAsync(() => ValueTask.FromResult(1), TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result.SelectAsync function of TResult from success should produce the expected result with ValueTask delegates when the delegates complete asynchronously.")]
    public async Task ResultSelectAsyncFunctionOfTResultFromSuccessShouldProduceExpectedResultWithValueTaskDelegatesWhenDelegatesCompleteAsynchronously()
    {
        // Given
        Result result = Result.Success();
        Result<int> expected = 1;

        // When
        Result<int> actual = await result.SelectAsync(() => Yielding.ValueTaskOf(1), TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result.SelectAsync function of TResult from success with exception should produce the expected result.")]
    public async Task ResultSelectAsyncFunctionOfTResultFromSuccessWithExceptionShouldProduceExpectedResult()
    {
        // Given
        Result result = Result.Success();
        Result<int> expected = Result<int>.Failure(FailureException);

        // When
        Result<int> actual = await result.SelectAsync<int>(Task<int> () => throw FailureException, TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result.SelectAsync function of TResult from success with exception should produce the expected result with ValueTask delegates.")]
    public async Task ResultSelectAsyncFunctionOfTResultFromSuccessWithExceptionShouldProduceExpectedResultWithValueTaskDelegates()
    {
        // Given
        Result result = Result.Success();
        Result<int> expected = Result<int>.Failure(FailureException);

        // When
        Result<int> actual = await result.SelectAsync<int>(ValueTask<int> () => throw FailureException, TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result.SelectAsync function of TResult with CancellationToken from failure should produce the expected result.")]
    public async Task ResultSelectAsyncFunctionOfTResultWithCancellationTokenFromFailureShouldProduceExpectedResult()
    {
        // Given
        Result result = Result.Failure(FailureException);
        Result<int> expected = Result<int>.Failure(FailureException);
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        Result<int> actual = await result.SelectAsync(_ => Task.FromResult(1), cancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result.SelectAsync function of TResult with CancellationToken from failure should produce the expected result when the delegates complete asynchronously.")]
    public async Task ResultSelectAsyncFunctionOfTResultWithCancellationTokenFromFailureShouldProduceExpectedResultWhenDelegatesCompleteAsynchronously()
    {
        // Given
        Result result = Result.Failure(FailureException);
        Result<int> expected = Result<int>.Failure(FailureException);
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        Result<int> actual = await result.SelectAsync(_ => Yielding.TaskOf(1), cancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result.SelectAsync function of TResult with CancellationToken from failure should produce the expected result with ValueTask delegates.")]
    public async Task ResultSelectAsyncFunctionOfTResultWithCancellationTokenFromFailureShouldProduceExpectedResultWithValueTaskDelegates()
    {
        // Given
        Result result = Result.Failure(FailureException);
        Result<int> expected = Result<int>.Failure(FailureException);
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        Result<int> actual = await result.SelectAsync(_ => ValueTask.FromResult(1), cancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result.SelectAsync function of TResult with CancellationToken from failure should produce the expected result with ValueTask delegates when the delegates complete asynchronously.")]
    public async Task ResultSelectAsyncFunctionOfTResultWithCancellationTokenFromFailureShouldProduceExpectedResultWithValueTaskDelegatesWhenDelegatesCompleteAsynchronously()
    {
        // Given
        Result result = Result.Failure(FailureException);
        Result<int> expected = Result<int>.Failure(FailureException);
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        Result<int> actual = await result.SelectAsync(_ => Yielding.ValueTaskOf(1), cancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result.SelectAsync function of TResult with CancellationToken from success should produce the expected result.")]
    public async Task ResultSelectAsyncFunctionOfTResultWithCancellationTokenFromSuccessShouldProduceExpectedResult()
    {
        // Given
        Result result = Result.Success();
        Result<int> expected = 1;
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        Result<int> actual = await result.SelectAsync(_ => Task.FromResult(1), cancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result.SelectAsync function of TResult with CancellationToken from success should produce the expected result when the delegates complete asynchronously.")]
    public async Task ResultSelectAsyncFunctionOfTResultWithCancellationTokenFromSuccessShouldProduceExpectedResultWhenDelegatesCompleteAsynchronously()
    {
        // Given
        Result result = Result.Success();
        Result<int> expected = 1;
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        Result<int> actual = await result.SelectAsync(_ => Yielding.TaskOf(1), cancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result.SelectAsync function of TResult with CancellationToken from success should produce the expected result with ValueTask delegates.")]
    public async Task ResultSelectAsyncFunctionOfTResultWithCancellationTokenFromSuccessShouldProduceExpectedResultWithValueTaskDelegates()
    {
        // Given
        Result result = Result.Success();
        Result<int> expected = 1;
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        Result<int> actual = await result.SelectAsync(_ => ValueTask.FromResult(1), cancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result.SelectAsync function of TResult with CancellationToken from success should produce the expected result with ValueTask delegates when the delegates complete asynchronously.")]
    public async Task ResultSelectAsyncFunctionOfTResultWithCancellationTokenFromSuccessShouldProduceExpectedResultWithValueTaskDelegatesWhenDelegatesCompleteAsynchronously()
    {
        // Given
        Result result = Result.Success();
        Result<int> expected = 1;
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        Result<int> actual = await result.SelectAsync(_ => Yielding.ValueTaskOf(1), cancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result.SelectAsync function of TResult with CancellationToken from success with exception should produce the expected result.")]
    public async Task ResultSelectAsyncFunctionOfTResultWithCancellationTokenFromSuccessWithExceptionShouldProduceExpectedResult()
    {
        // Given
        Result result = Result.Success();
        Result<int> expected = Result<int>.Failure(FailureException);
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        Result<int> actual = await result.SelectAsync<int>(Task<int> (_) => throw FailureException, cancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result.SelectAsync function of TResult with CancellationToken from success with exception should produce the expected result with ValueTask delegates.")]
    public async Task ResultSelectAsyncFunctionOfTResultWithCancellationTokenFromSuccessWithExceptionShouldProduceExpectedResultWithValueTaskDelegates()
    {
        // Given
        Result result = Result.Success();
        Result<int> expected = Result<int>.Failure(FailureException);
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        Result<int> actual = await result.SelectAsync<int>(ValueTask<int> (_) => throw FailureException, cancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result.SelectMany function from failure should produce the expected result.")]
    public void ResultSelectManyFunctionFromFailureShouldProduceExpectedResult()
    {
        // Given
        Result result = Result.Failure(FailureException);
        Result expected = Result.Failure(FailureException);

        // When
        Result actual = result.SelectMany(Result.Success);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result.SelectMany function from success should produce the expected result.")]
    public void ResultSelectManyFunctionFromSuccessShouldProduceExpectedResult()
    {
        // Given
        Result result = Result.Success();
        Result expected = Result.Success();

        // When
        Result actual = result.SelectMany(Result.Success);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result.SelectMany function from success with exception should produce the expected result.")]
    public void ResultSelectManyFunctionFromSuccessWithExceptionShouldProduceExpectedResult()
    {
        // Given
        Result result = Result.Success();
        Result expected = Result.Failure(FailureException);

        // When
        Result actual = result.SelectMany(() => throw FailureException);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result.SelectManyAsync function from failure should produce the expected result.")]
    public async Task ResultSelectManyAsyncFunctionFromFailureShouldProduceExpectedResult()
    {
        // Given
        Result result = Result.Failure(FailureException);
        Result expected = Result.Failure(FailureException);

        // When
        Result actual = await result.SelectManyAsync(async Task<Result> () => await Task.FromResult(Result.Success()), TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result.SelectManyAsync function from failure should produce the expected result when the delegates complete asynchronously.")]
    public async Task ResultSelectManyAsyncFunctionFromFailureShouldProduceExpectedResultWhenDelegatesCompleteAsynchronously()
    {
        // Given
        Result result = Result.Failure(FailureException);
        Result expected = Result.Failure(FailureException);

        // When
        Result actual = await result.SelectManyAsync(async Task<Result> () => await Yielding.TaskOf(Result.Success()), TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result.SelectManyAsync function from failure should produce the expected result with ValueTask delegates.")]
    public async Task ResultSelectManyAsyncFunctionFromFailureShouldProduceExpectedResultWithValueTaskDelegates()
    {
        // Given
        Result result = Result.Failure(FailureException);
        Result expected = Result.Failure(FailureException);

        // When
        Result actual = await result.SelectManyAsync(async ValueTask<Result> () => await ValueTask.FromResult(Result.Success()), TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result.SelectManyAsync function from failure should produce the expected result with ValueTask delegates when the delegates complete asynchronously.")]
    public async Task ResultSelectManyAsyncFunctionFromFailureShouldProduceExpectedResultWithValueTaskDelegatesWhenDelegatesCompleteAsynchronously()
    {
        // Given
        Result result = Result.Failure(FailureException);
        Result expected = Result.Failure(FailureException);

        // When
        Result actual = await result.SelectManyAsync(async ValueTask<Result> () => await Yielding.ValueTaskOf(Result.Success()), TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result.SelectManyAsync function from success should produce the expected result.")]
    public async Task ResultSelectManyAsyncFunctionFromSuccessShouldProduceExpectedResult()
    {
        // Given
        Result result = Result.Success();
        Result expected = Result.Success();

        // When
        Result actual = await result.SelectManyAsync(async Task<Result> () => await Task.FromResult(Result.Success()), TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result.SelectManyAsync function from success should produce the expected result when the delegates complete asynchronously.")]
    public async Task ResultSelectManyAsyncFunctionFromSuccessShouldProduceExpectedResultWhenDelegatesCompleteAsynchronously()
    {
        // Given
        Result result = Result.Success();
        Result expected = Result.Success();

        // When
        Result actual = await result.SelectManyAsync(async Task<Result> () => await Yielding.TaskOf(Result.Success()), TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result.SelectManyAsync function from success should produce the expected result with ValueTask delegates.")]
    public async Task ResultSelectManyAsyncFunctionFromSuccessShouldProduceExpectedResultWithValueTaskDelegates()
    {
        // Given
        Result result = Result.Success();
        Result expected = Result.Success();

        // When
        Result actual = await result.SelectManyAsync(async ValueTask<Result> () => await ValueTask.FromResult(Result.Success()), TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result.SelectManyAsync function from success should produce the expected result with ValueTask delegates when the delegates complete asynchronously.")]
    public async Task ResultSelectManyAsyncFunctionFromSuccessShouldProduceExpectedResultWithValueTaskDelegatesWhenDelegatesCompleteAsynchronously()
    {
        // Given
        Result result = Result.Success();
        Result expected = Result.Success();

        // When
        Result actual = await result.SelectManyAsync(async ValueTask<Result> () => await Yielding.ValueTaskOf(Result.Success()), TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result.SelectManyAsync function from success with exception should produce the expected result.")]
    public async Task ResultSelectManyAsyncFunctionFromSuccessWithExceptionShouldProduceExpectedResult()
    {
        // Given
        Result result = Result.Success();
        Result expected = Result.Failure(FailureException);

        // When
        Result actual = await result.SelectManyAsync(Task<Result> () => throw FailureException, TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result.SelectManyAsync function from success with exception should produce the expected result with ValueTask delegates.")]
    public async Task ResultSelectManyAsyncFunctionFromSuccessWithExceptionShouldProduceExpectedResultWithValueTaskDelegates()
    {
        // Given
        Result result = Result.Success();
        Result expected = Result.Failure(FailureException);

        // When
        Result actual = await result.SelectManyAsync(ValueTask<Result> () => throw FailureException, TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result.SelectManyAsync function with CancellationToken from failure should produce the expected result.")]
    public async Task ResultSelectManyAsyncFunctionWithCancellationTokenFromFailureShouldProduceExpectedResult()
    {
        // Given
        Result result = Result.Failure(FailureException);
        Result expected = Result.Failure(FailureException);
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        Result actual = await result.SelectManyAsync(async Task<Result> (_) => await Task.FromResult(Result.Success()), cancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result.SelectManyAsync function with CancellationToken from failure should produce the expected result when the delegates complete asynchronously.")]
    public async Task ResultSelectManyAsyncFunctionWithCancellationTokenFromFailureShouldProduceExpectedResultWhenDelegatesCompleteAsynchronously()
    {
        // Given
        Result result = Result.Failure(FailureException);
        Result expected = Result.Failure(FailureException);
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        Result actual = await result.SelectManyAsync(async Task<Result> (_) => await Yielding.TaskOf(Result.Success()), cancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result.SelectManyAsync function with CancellationToken from failure should produce the expected result with ValueTask delegates.")]
    public async Task ResultSelectManyAsyncFunctionWithCancellationTokenFromFailureShouldProduceExpectedResultWithValueTaskDelegates()
    {
        // Given
        Result result = Result.Failure(FailureException);
        Result expected = Result.Failure(FailureException);
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        Result actual = await result.SelectManyAsync(async ValueTask<Result> (_) => await ValueTask.FromResult(Result.Success()), cancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result.SelectManyAsync function with CancellationToken from failure should produce the expected result with ValueTask delegates when the delegates complete asynchronously.")]
    public async Task ResultSelectManyAsyncFunctionWithCancellationTokenFromFailureShouldProduceExpectedResultWithValueTaskDelegatesWhenDelegatesCompleteAsynchronously()
    {
        // Given
        Result result = Result.Failure(FailureException);
        Result expected = Result.Failure(FailureException);
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        Result actual = await result.SelectManyAsync(async ValueTask<Result> (_) => await Yielding.ValueTaskOf(Result.Success()), cancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result.SelectManyAsync function with CancellationToken from success should produce the expected result.")]
    public async Task ResultSelectManyAsyncFunctionWithCancellationTokenFromSuccessShouldProduceExpectedResult()
    {
        // Given
        Result result = Result.Success();
        Result expected = Result.Success();
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        Result actual = await result.SelectManyAsync(async Task<Result> (_) => await Task.FromResult(Result.Success()), cancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result.SelectManyAsync function with CancellationToken from success should produce the expected result when the delegates complete asynchronously.")]
    public async Task ResultSelectManyAsyncFunctionWithCancellationTokenFromSuccessShouldProduceExpectedResultWhenDelegatesCompleteAsynchronously()
    {
        // Given
        Result result = Result.Success();
        Result expected = Result.Success();
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        Result actual = await result.SelectManyAsync(async Task<Result> (_) => await Yielding.TaskOf(Result.Success()), cancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result.SelectManyAsync function with CancellationToken from success should produce the expected result with ValueTask delegates.")]
    public async Task ResultSelectManyAsyncFunctionWithCancellationTokenFromSuccessShouldProduceExpectedResultWithValueTaskDelegates()
    {
        // Given
        Result result = Result.Success();
        Result expected = Result.Success();
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        Result actual = await result.SelectManyAsync(async ValueTask<Result> (_) => await ValueTask.FromResult(Result.Success()), cancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result.SelectManyAsync function with CancellationToken from success should produce the expected result with ValueTask delegates when the delegates complete asynchronously.")]
    public async Task ResultSelectManyAsyncFunctionWithCancellationTokenFromSuccessShouldProduceExpectedResultWithValueTaskDelegatesWhenDelegatesCompleteAsynchronously()
    {
        // Given
        Result result = Result.Success();
        Result expected = Result.Success();
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        Result actual = await result.SelectManyAsync(async ValueTask<Result> (_) => await Yielding.ValueTaskOf(Result.Success()), cancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result.SelectManyAsync function with CancellationToken from success with exception should produce the expected result.")]
    public async Task ResultSelectManyAsyncFunctionWithCancellationTokenFromSuccessWithExceptionShouldProduceExpectedResult()
    {
        // Given
        Result result = Result.Success();
        Result expected = Result.Failure(FailureException);
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        Result actual = await result.SelectManyAsync(Task<Result> (_) => throw FailureException, cancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result.SelectManyAsync function with CancellationToken from success with exception should produce the expected result with ValueTask delegates.")]
    public async Task ResultSelectManyAsyncFunctionWithCancellationTokenFromSuccessWithExceptionShouldProduceExpectedResultWithValueTaskDelegates()
    {
        // Given
        Result result = Result.Success();
        Result expected = Result.Failure(FailureException);
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        Result actual = await result.SelectManyAsync(ValueTask<Result> (_) => throw FailureException, cancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result.SelectMany function of TResult from failure should produce the expected result.")]
    public void ResultSelectManyFunctionOfTResultFromFailureShouldProduceExpectedResult()
    {
        // Given
        Result result = Result.Failure(FailureException);
        Result<int> expected = Result<int>.Failure(FailureException);

        // When
        Result<int> actual = result.SelectMany<int>(() => 1);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result.SelectMany function of TResult from success should produce the expected result.")]
    public void ResultSelectManyFunctionOfTResultFromSuccessShouldProduceExpectedResult()
    {
        // Given
        Result result = Result.Success();
        Result<int> expected = 1;

        // When
        Result<int> actual = result.SelectMany<int>(() => 1);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result.SelectMany function of TResult from success with exception should produce the expected result.")]
    public void ResultSelectManyFunctionOfTResultFromSuccessWithExceptionShouldProduceExpectedResult()
    {
        // Given
        Result result = Result.Success();
        Result<int> expected = Result<int>.Failure(FailureException);

        // When
        Result<int> actual = result.SelectMany<int>(() => throw FailureException);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result.SelectManyAsync function of TResult from failure should produce the expected result.")]
    public async Task ResultSelectManyAsyncFunctionOfTResultFromFailureShouldProduceExpectedResult()
    {
        // Given
        Result result = Result.Failure(FailureException);
        Result<int> expected = Result<int>.Failure(FailureException);

        // When
        Result<int> actual = await result.SelectManyAsync<int>(async Task<Result<int>> () =>
            await Task.FromResult(Result<int>.Success(1)), TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result.SelectManyAsync function of TResult from failure should produce the expected result when the delegates complete asynchronously.")]
    public async Task ResultSelectManyAsyncFunctionOfTResultFromFailureShouldProduceExpectedResultWhenDelegatesCompleteAsynchronously()
    {
        // Given
        Result result = Result.Failure(FailureException);
        Result<int> expected = Result<int>.Failure(FailureException);

        // When
        Result<int> actual = await result.SelectManyAsync<int>(async Task<Result<int>> () =>
            await Yielding.TaskOf(Result<int>.Success(1)), TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result.SelectManyAsync function of TResult from failure should produce the expected result with ValueTask delegates.")]
    public async Task ResultSelectManyAsyncFunctionOfTResultFromFailureShouldProduceExpectedResultWithValueTaskDelegates()
    {
        // Given
        Result result = Result.Failure(FailureException);
        Result<int> expected = Result<int>.Failure(FailureException);

        // When
        Result<int> actual = await result.SelectManyAsync<int>(async ValueTask<Result<int>> () =>
            await ValueTask.FromResult(Result<int>.Success(1)), TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result.SelectManyAsync function of TResult from failure should produce the expected result with ValueTask delegates when the delegates complete asynchronously.")]
    public async Task ResultSelectManyAsyncFunctionOfTResultFromFailureShouldProduceExpectedResultWithValueTaskDelegatesWhenDelegatesCompleteAsynchronously()
    {
        // Given
        Result result = Result.Failure(FailureException);
        Result<int> expected = Result<int>.Failure(FailureException);

        // When
        Result<int> actual = await result.SelectManyAsync<int>(async ValueTask<Result<int>> () =>
            await Yielding.ValueTaskOf(Result<int>.Success(1)), TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result.SelectManyAsync function of TResult from success should produce the expected result.")]
    public async Task ResultSelectManyAsyncFunctionOfTResultFromSuccessShouldProduceExpectedResult()
    {
        // Given
        Result result = Result.Success();
        Result<int> expected = Result<int>.Success(1);

        // When
        Result<int> actual = await result.SelectManyAsync<int>(async Task<Result<int>> () =>
            await Task.FromResult(Result<int>.Success(1)), TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result.SelectManyAsync function of TResult from success should produce the expected result when the delegates complete asynchronously.")]
    public async Task ResultSelectManyAsyncFunctionOfTResultFromSuccessShouldProduceExpectedResultWhenDelegatesCompleteAsynchronously()
    {
        // Given
        Result result = Result.Success();
        Result<int> expected = Result<int>.Success(1);

        // When
        Result<int> actual = await result.SelectManyAsync<int>(async Task<Result<int>> () =>
            await Yielding.TaskOf(Result<int>.Success(1)), TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result.SelectManyAsync function of TResult from success should produce the expected result with ValueTask delegates.")]
    public async Task ResultSelectManyAsyncFunctionOfTResultFromSuccessShouldProduceExpectedResultWithValueTaskDelegates()
    {
        // Given
        Result result = Result.Success();
        Result<int> expected = Result<int>.Success(1);

        // When
        Result<int> actual = await result.SelectManyAsync<int>(async ValueTask<Result<int>> () =>
            await ValueTask.FromResult(Result<int>.Success(1)), TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result.SelectManyAsync function of TResult from success should produce the expected result with ValueTask delegates when the delegates complete asynchronously.")]
    public async Task ResultSelectManyAsyncFunctionOfTResultFromSuccessShouldProduceExpectedResultWithValueTaskDelegatesWhenDelegatesCompleteAsynchronously()
    {
        // Given
        Result result = Result.Success();
        Result<int> expected = Result<int>.Success(1);

        // When
        Result<int> actual = await result.SelectManyAsync<int>(async ValueTask<Result<int>> () =>
            await Yielding.ValueTaskOf(Result<int>.Success(1)), TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result.SelectManyAsync function of TResult from success with exception should produce the expected result.")]
    public async Task ResultSelectManyAsyncFunctionOfTResultFromSuccessWithExceptionShouldProduceExpectedResult()
    {
        // Given
        Result result = Result.Success();
        Result<int> expected = Result<int>.Failure(FailureException);

        // When
        Result<int> actual = await result.SelectManyAsync<int>(Task<Result<int>> () => throw FailureException, TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result.SelectManyAsync function of TResult from success with exception should produce the expected result with ValueTask delegates.")]
    public async Task ResultSelectManyAsyncFunctionOfTResultFromSuccessWithExceptionShouldProduceExpectedResultWithValueTaskDelegates()
    {
        // Given
        Result result = Result.Success();
        Result<int> expected = Result<int>.Failure(FailureException);

        // When
        Result<int> actual = await result.SelectManyAsync<int>(ValueTask<Result<int>> () => throw FailureException, TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result.SelectManyAsync function of TResult with CancellationToken from failure should produce the expected result.")]
    public async Task ResultSelectManyAsyncFunctionOfTResultWithCancellationTokenFromFailureShouldProduceExpectedResult()
    {
        // Given
        Result result = Result.Failure(FailureException);
        Result<int> expected = Result<int>.Failure(FailureException);
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        Result<int> actual = await result.SelectManyAsync<int>(async Task<Result<int>> (_) => await Task.FromResult(Result<int>.Success(1)), cancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result.SelectManyAsync function of TResult with CancellationToken from failure should produce the expected result when the delegates complete asynchronously.")]
    public async Task ResultSelectManyAsyncFunctionOfTResultWithCancellationTokenFromFailureShouldProduceExpectedResultWhenDelegatesCompleteAsynchronously()
    {
        // Given
        Result result = Result.Failure(FailureException);
        Result<int> expected = Result<int>.Failure(FailureException);
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        Result<int> actual = await result.SelectManyAsync<int>(async Task<Result<int>> (_) => await Yielding.TaskOf(Result<int>.Success(1)), cancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result.SelectManyAsync function of TResult with CancellationToken from failure should produce the expected result with ValueTask delegates.")]
    public async Task ResultSelectManyAsyncFunctionOfTResultWithCancellationTokenFromFailureShouldProduceExpectedResultWithValueTaskDelegates()
    {
        // Given
        Result result = Result.Failure(FailureException);
        Result<int> expected = Result<int>.Failure(FailureException);
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        Result<int> actual = await result.SelectManyAsync<int>(async ValueTask<Result<int>> (_) => await ValueTask.FromResult(Result<int>.Success(1)), cancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result.SelectManyAsync function of TResult with CancellationToken from failure should produce the expected result with ValueTask delegates when the delegates complete asynchronously.")]
    public async Task ResultSelectManyAsyncFunctionOfTResultWithCancellationTokenFromFailureShouldProduceExpectedResultWithValueTaskDelegatesWhenDelegatesCompleteAsynchronously()
    {
        // Given
        Result result = Result.Failure(FailureException);
        Result<int> expected = Result<int>.Failure(FailureException);
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        Result<int> actual = await result.SelectManyAsync<int>(async ValueTask<Result<int>> (_) => await Yielding.ValueTaskOf(Result<int>.Success(1)), cancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result.SelectManyAsync function of TResult with CancellationToken from success should produce the expected result.")]
    public async Task ResultSelectManyAsyncFunctionOfTResultWithCancellationTokenFromSuccessShouldProduceExpectedResult()
    {
        // Given
        Result result = Result.Success();
        Result<int> expected = Result<int>.Success(1);
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        Result<int> actual = await result.SelectManyAsync<int>(async Task<Result<int>> (_) => await Task.FromResult(Result<int>.Success(1)), cancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result.SelectManyAsync function of TResult with CancellationToken from success should produce the expected result when the delegates complete asynchronously.")]
    public async Task ResultSelectManyAsyncFunctionOfTResultWithCancellationTokenFromSuccessShouldProduceExpectedResultWhenDelegatesCompleteAsynchronously()
    {
        // Given
        Result result = Result.Success();
        Result<int> expected = Result<int>.Success(1);
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        Result<int> actual = await result.SelectManyAsync<int>(async Task<Result<int>> (_) => await Yielding.TaskOf(Result<int>.Success(1)), cancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result.SelectManyAsync function of TResult with CancellationToken from success should produce the expected result with ValueTask delegates.")]
    public async Task ResultSelectManyAsyncFunctionOfTResultWithCancellationTokenFromSuccessShouldProduceExpectedResultWithValueTaskDelegates()
    {
        // Given
        Result result = Result.Success();
        Result<int> expected = Result<int>.Success(1);
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        Result<int> actual = await result.SelectManyAsync<int>(async ValueTask<Result<int>> (_) => await ValueTask.FromResult(Result<int>.Success(1)), cancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result.SelectManyAsync function of TResult with CancellationToken from success should produce the expected result with ValueTask delegates when the delegates complete asynchronously.")]
    public async Task ResultSelectManyAsyncFunctionOfTResultWithCancellationTokenFromSuccessShouldProduceExpectedResultWithValueTaskDelegatesWhenDelegatesCompleteAsynchronously()
    {
        // Given
        Result result = Result.Success();
        Result<int> expected = Result<int>.Success(1);
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        Result<int> actual = await result.SelectManyAsync<int>(async ValueTask<Result<int>> (_) => await Yielding.ValueTaskOf(Result<int>.Success(1)), cancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result.SelectManyAsync function of TResult with CancellationToken from success with exception should produce the expected result.")]
    public async Task ResultSelectManyAsyncFunctionOfTResultWithCancellationTokenFromSuccessWithExceptionShouldProduceExpectedResult()
    {
        // Given
        Result result = Result.Success();
        Result<int> expected = Result<int>.Failure(FailureException);
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        Result<int> actual = await result.SelectManyAsync<int>(Task<Result<int>> (_) => throw FailureException, cancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result.SelectManyAsync function of TResult with CancellationToken from success with exception should produce the expected result with ValueTask delegates.")]
    public async Task ResultSelectManyAsyncFunctionOfTResultWithCancellationTokenFromSuccessWithExceptionShouldProduceExpectedResultWithValueTaskDelegates()
    {
        // Given
        Result result = Result.Success();
        Result<int> expected = Result<int>.Failure(FailureException);
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        Result<int> actual = await result.SelectManyAsync<int>(ValueTask<Result<int>> (_) => throw FailureException, cancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result.Throw from success should produce expected result.")]
    public void ResultThrowFromSuccessShouldProduceExpectedResult()
    {
        // Given
        Result result = Result.Success();

        // When / Then
        result.Throw();
    }

    [Fact(DisplayName = "Result.Throw from failure should produce expected result.")]
    public void ResultThrowFromFailureShouldProduceExpectedResult()
    {
        // Given
        Result result = Result.Failure(FailureException);

        // When
        Exception exception = Assert.Throws<Exception>(() => result.Throw());

        // Then
        Assert.Equal(FailureException, exception);
    }

    [Fact(DisplayName = "Result.ToString from success should produce expected result.")]
    public void ResultToStringFromSuccessShouldProduceExpectedResult()
    {
        // Given
        Result result = Result.Success();
        string expected = string.Empty;

        // When
        string actual = result.ToString();

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result.ToString from failure should produce expected result.")]
    public void ResultToStringFromFailureShouldProduceExpectedResult()
    {
        // Given
        Result result = Result.Failure(FailureException);
        string expected = FailureException.Message;

        // When
        string actual = result.ToString();

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Success.ToTypedResult should produce the expected result.")]
    public void SuccessToTypedResultShouldProduceExpectedResult()
    {
        // Given
        Success result = Result.Success();
        Result<int> expected = 1;

        // When
        Result<int> actual = result.ToTypedResult(1);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Failure.ToTypedResult should produce the expected result.")]
    public void FailureToTypedResultShouldProduceExpectedResult()
    {
        // Given
        Failure result = Result.Failure(FailureException);
        Result<int> expected = FailureException;

        // When
        Result<int> actual = result.ToTypedResult<int>();

        // Then
        Assert.Equal(expected, actual);
    }
}
