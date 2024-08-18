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
using Xunit;

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
        Result result = await Result.OfAsync(async () => await Task.CompletedTask);

        // Then
        Assert.IsType<Success>(result);
    }

    [Fact(DisplayName = "Result.OfAsync should produce the expected failure result.")]
    public async Task ResultOfAsyncShouldProduceExpectedFailureResult()
    {
        // Given / When
        Result result = await Result.OfAsync(async () => await Task.FromException(FailureException));

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
        Assert.Equal("The current result is not in a failure state.", exception.Message);
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
        await result.MatchAsync(Success, Failure);

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

    [Fact(DisplayName = "Result.MatchAsync(Func<Task>, Action<Exception>) from failure should produce the expected result.")]
    public async Task ResultMatchAsyncFuncTaskActionFromFailureShouldProduceExpectedResult()
    {
        // Given
        Result result = Result.Failure(FailureException);
        bool isSuccess = false;
        bool isFailure = false;

        // When
        await result.MatchAsync(Success, Failure);

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

    [Fact(DisplayName = "Result.MatchAsync(Func<CancellationToken, Task>, Action<Exception>) from success should produce the expected result.")]
    public async Task ResultMatchAsyncFuncCancellationTokenTaskActionFromSuccessShouldProduceExpectedResult()
    {
        // Given
        Result result = Result.Success();
        bool isSuccess = false;
        bool isFailure = false;
        CancellationToken token = new();

        // When
        await result.MatchAsync(Success, Failure, token);

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

    [Fact(DisplayName = "Result.MatchAsync(Func<CancellationToken, Task>, Action<Exception>) from failure should produce the expected result.")]
    public async Task ResultMatchAsyncFuncCancellationTokenTaskActionFromFailureShouldProduceExpectedResult()
    {
        // Given
        Result result = Result.Failure(FailureException);
        bool isSuccess = false;
        bool isFailure = false;
        CancellationToken token = new();

        // When
        await result.MatchAsync(Success, Failure, token);

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

    [Fact(DisplayName = "Result.MatchAsync(Action, Func<Exception, Task>) from success should produce the expected result.")]
    public async Task ResultMatchAsyncActionFuncExceptionTaskFromSuccessShouldProduceExpectedResult()
    {
        // Given
        Result result = Result.Success();
        bool isSuccess = false;
        bool isFailure = false;

        // When
        await result.MatchAsync(Success, Failure);

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

    [Fact(DisplayName = "Result.MatchAsync(Action, Func<Exception, Task>) from failure should produce the expected result.")]
    public async Task ResultMatchAsyncActionFuncExceptionTaskFromFailureShouldProduceExpectedResult()
    {
        // Given
        Result result = Result.Failure(FailureException);
        bool isSuccess = false;
        bool isFailure = false;

        // When
        await result.MatchAsync(Success, Failure);

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

    [Fact(DisplayName = "Result.MatchAsync(Action, Func<Exception, CancellationToken, Task>) from success should produce the expected result.")]
    public async Task ResultMatchAsyncActionFuncExceptionCancellationTokenTaskFromSuccessShouldProduceExpectedResult()
    {
        // Given
        Result result = Result.Success();
        bool isSuccess = false;
        bool isFailure = false;
        CancellationToken token = new();

        // When
        await result.MatchAsync(Success, Failure, token);

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

    [Fact(DisplayName = "Result.MatchAsync(Action, Func<Exception, CancellationToken, Task>) from failure should produce the expected result.")]
    public async Task ResultMatchAsyncActionFuncExceptionCancellationTokenTaskFromFailureShouldProduceExpectedResult()
    {
        // Given
        Result result = Result.Failure(FailureException);
        bool isSuccess = false;
        bool isFailure = false;
        CancellationToken token = new();

        // When
        await result.MatchAsync(Success, Failure, token);

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

    [Fact(DisplayName = "Result.MatchAsync(Func<Task>, Func<Exception, Task>) from success should produce the expected result.")]
    public async Task ResultMatchAsyncFuncTaskFuncExceptionTaskFromSuccessShouldProduceExpectedResult()
    {
        // Given
        Result result = Result.Success();
        bool isSuccess = false;
        bool isFailure = false;
        CancellationToken token = new();

        // When
        await result.MatchAsync(Success, Failure, token);

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

    [Fact(DisplayName = "Result.MatchAsync(Func<Task>, Func<Exception, Task>) from failure should produce the expected result.")]
    public async Task ResultMatchAsyncFuncTaskFuncExceptionTaskFromFailureShouldProduceExpectedResult()
    {
        // Given
        Result result = Result.Failure(FailureException);
        bool isSuccess = false;
        bool isFailure = false;
        CancellationToken token = new();

        // When
        await result.MatchAsync(Success, Failure, token);

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

    [Fact(DisplayName = "Result.MatchAsync(Func<CancellationToken, Task>, Func<Exception, CancellationToken, Task>) from success should produce the expected result.")]
    public async Task ResultMatchAsyncFuncCancellationTokenTaskFuncExceptionCancellationTokenTaskFromSuccessShouldProduceExpectedResult()
    {
        // Given
        Result result = Result.Success();
        bool isSuccess = false;
        bool isFailure = false;
        CancellationToken token = new();

        // When
        await result.MatchAsync(Success, Failure, token);

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

    [Fact(DisplayName = "Result.MatchAsync(Func<CancellationToken, Task>, Func<Exception, CancellationToken, Task>) from failure should produce the expected result.")]
    public async Task ResultMatchAsyncFuncCancellationTokenTaskFuncExceptionCancellationTokenTaskFromFailureShouldProduceExpectedResult()
    {
        // Given
        Result result = Result.Failure(FailureException);
        bool isSuccess = false;
        bool isFailure = false;
        CancellationToken token = new();

        // When
        await result.MatchAsync(Success, Failure, token);

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
        int actual = await result.MatchAsync(Success, Failure);

        // Then
        Assert.Equal(expected, actual);

        return;
        Task<int> Success() => Task.FromResult(1);
        int Failure(Exception exception) => 0;
    }

    [Fact(DisplayName = "Result.MatchAsync(Func<Task<TResult>>, Func<Exception, TResult>) from failure should produce the expected result.")]
    public async Task ResultMatchAsyncFuncTaskTResultFuncExceptionTResultFromFailureShouldProduceExpectedResult()
    {
        // Given
        Result result = Result.Failure(FailureException);
        const int expected = 0;

        // When
        int actual = await result.MatchAsync(Success, Failure);

        // Then
        Assert.Equal(expected, actual);

        return;
        Task<int> Success() => Task.FromResult(1);
        int Failure(Exception exception) => 0;
    }

    [Fact(DisplayName = "Result.MatchAsync(Func<CancellationToken, Task<TResult>>, Func<Exception, TResult>) from success should produce the expected result.")]
    public async Task ResultMatchAsyncFuncCancellationTokenTaskTResultFuncExceptionTResultFromSuccessShouldProduceExpectedResult()
    {
        // Given
        Result result = Result.Success();
        const int expected = 1;
        CancellationToken token = new();

        // When
        int actual = await result.MatchAsync(Success, Failure, token);

        // Then
        Assert.Equal(expected, actual);

        return;
        Task<int> Success(CancellationToken ct) => Task.FromResult(1);
        int Failure(Exception exception) => 0;
    }

    [Fact(DisplayName = "Result.MatchAsync(Func<CancellationToken, Task<TResult>>, Func<Exception, TResult>) from failure should produce the expected result.")]
    public async Task ResultMatchAsyncFuncCancellationTokenTaskTResultFuncExceptionTResultFromFailureShouldProduceExpectedResult()
    {
        // Given
        Result result = Result.Failure(FailureException);
        const int expected = 0;
        CancellationToken token = new();

        // When
        int actual = await result.MatchAsync(Success, Failure, token);

        // Then
        Assert.Equal(expected, actual);

        return;
        Task<int> Success(CancellationToken ct) => Task.FromResult(1);
        int Failure(Exception exception) => 0;
    }

    [Fact(DisplayName = "Result.MatchAsync(Func<TResult>, Func<Exception, Task<TResult>>) from success should produce the expected result.")]
    public async Task ResultMatchAsyncFuncTResultFuncExceptionTaskTResultFromSuccessShouldProduceExpectedResult()
    {
        // Given
        Result result = Result.Success();
        const int expected = 1;

        // When
        int actual = await result.MatchAsync(Success, Failure);

        // Then
        Assert.Equal(expected, actual);

        return;
        int Success() => 1;
        Task<int> Failure(Exception exception) => Task.FromResult(0);
    }

    [Fact(DisplayName = "Result.MatchAsync(Func<TResult>, Func<Exception, Task<TResult>>) from failure should produce the expected result.")]
    public async Task ResultMatchAsyncFuncTResultFuncExceptionTaskTResultFromFailureShouldProduceExpectedResult()
    {
        // Given
        Result result = Result.Failure(FailureException);
        const int expected = 0;

        // When
        int actual = await result.MatchAsync(Success, Failure);

        // Then
        Assert.Equal(expected, actual);

        return;
        int Success() => 1;
        Task<int> Failure(Exception exception) => Task.FromResult(0);
    }

    [Fact(DisplayName = "Result.MatchAsync(Func<TResult>, Func<Exception, CancellationToken, Task<TResult>>) from success should produce the expected result.")]
    public async Task ResultMatchAsyncFuncTResultFuncExceptionCancellationTokenTaskTResultFromSuccessShouldProduceExpectedResult()
    {
        // Given
        Result result = Result.Success();
        const int expected = 1;
        CancellationToken token = new();

        // When
        int actual = await result.MatchAsync(Success, Failure, token);

        // Then
        Assert.Equal(expected, actual);

        return;
        int Success() => 1;
        Task<int> Failure(Exception exception, CancellationToken ct) => Task.FromResult(0);
    }

    [Fact(DisplayName = "Result.MatchAsync(Func<TResult>, Func<Exception, CancellationToken, Task<TResult>>) from failure should produce the expected result.")]
    public async Task ResultMatchAsyncFuncTResultFuncExceptionCancellationTokenTaskTResultFromFailureShouldProduceExpectedResult()
    {
        // Given
        Result result = Result.Failure(FailureException);
        const int expected = 0;
        CancellationToken token = new();

        // When
        int actual = await result.MatchAsync(Success, Failure, token);

        // Then
        Assert.Equal(expected, actual);

        return;
        int Success() => 1;
        Task<int> Failure(Exception exception, CancellationToken ct) => Task.FromResult(0);
    }

    [Fact(DisplayName = "Result.MatchAsync(Func<Task<TResult>>, Func<Exception, Task<TResult>>) from success should produce the expected result.")]
    public async Task ResultMatchAsyncFuncTaskTResultFuncExceptionTaskTResultFromSuccessShouldProduceExpectedResult()
    {
        // Given
        Result result = Result.Success();
        const int expected = 1;
        CancellationToken token = new();

        // When
        int actual = await result.MatchAsync(Success, Failure, token);

        // Then
        Assert.Equal(expected, actual);

        return;
        Task<int> Success() => Task.FromResult(1);
        Task<int> Failure(Exception exception) => Task.FromResult(0);
    }

    [Fact(DisplayName = "Result.MatchAsync(Func<Task<TResult>>, Func<Exception, Task<TResult>>) from failure should produce the expected result.")]
    public async Task ResultMatchAsyncFuncTaskTResultFuncExceptionTaskTResultFromFailureShouldProduceExpectedResult()
    {
        // Given
        Result result = Result.Failure(FailureException);
        const int expected = 0;
        CancellationToken token = new();

        // When
        int actual = await result.MatchAsync(Success, Failure, token);

        // Then
        Assert.Equal(expected, actual);

        return;
        Task<int> Success() => Task.FromResult(1);
        Task<int> Failure(Exception exception) => Task.FromResult(0);
    }

    [Fact(DisplayName = "Result.MatchAsync(Func<CancellationToken, Task<TResult>>, Func<Exception, CancellationToken, Task<TResult>>) from success should produce the expected result.")]
    public async Task ResultMatchAsyncFuncCancellationTokenTaskTResultFuncExceptionCancellationTokenTaskTResultFromSuccessShouldProduceExpectedResult()
    {
        // Given
        Result result = Result.Success();
        const int expected = 1;
        CancellationToken token = new();

        // When
        int actual = await result.MatchAsync(Success, Failure, token);

        // Then
        Assert.Equal(expected, actual);

        return;
        Task<int> Success(CancellationToken ct) => Task.FromResult(1);
        Task<int> Failure(Exception exception, CancellationToken ct) => Task.FromResult(0);
    }

    [Fact(DisplayName = "Result.MatchAsync(Func<CancellationToken, Task<TResult>>, Func<Exception, CancellationToken, Task<TResult>>) from failure should produce the expected result.")]
    public async Task ResultMatchAsyncFuncCancellationTokenTaskTResultFuncExceptionCancellationTokenTaskTResultFromFailureShouldProduceExpectedResult()
    {
        // Given
        Result result = Result.Failure(FailureException);
        const int expected = 0;
        CancellationToken token = new();

        // When
        int actual = await result.MatchAsync(Success, Failure, token);

        // Then
        Assert.Equal(expected, actual);

        return;
        Task<int> Success(CancellationToken ct) => Task.FromResult(1);
        Task<int> Failure(Exception exception, CancellationToken ct) => Task.FromResult(0);
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
        Result actual = await result.SelectAsync(() => Task.CompletedTask);

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
        Result actual = await result.SelectAsync(() => Task.CompletedTask);

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
        Result actual = await result.SelectAsync(() => throw FailureException);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result.SelectAsync(Func<CancellationToken, Task>) from failure should produce the expected result.")]
    public async Task ResultSelectAsyncFuncCancellationTokenTaskFromFailureShouldProduceExpectedResult()
    {
        // Given
        Result result = Result.Failure(FailureException);
        Result expected = Result.Failure(FailureException);
        CancellationToken token = new();

        // When
        Result actual = await result.SelectAsync(_ => Task.CompletedTask, token);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result.SelectAsync(Func<CancellationToken, Task>) from success should produce the expected result.")]
    public async Task ResultSelectAsyncFuncCancellationTokenTaskFromSuccessShouldProduceExpectedResult()
    {
        // Given
        Result result = Result.Success();
        Result expected = Result.Success();
        CancellationToken token = new();

        // When
        Result actual = await result.SelectAsync(_ => Task.CompletedTask, token);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result.SelectAsync(Func<CancellationToken, Task>) from success with exception should produce the expected result.")]
    public async Task ResultSelectAsyncFuncCancellationTokenTaskFromSuccessWithExceptionShouldProduceExpectedResult()
    {
        // Given
        Result result = Result.Success();
        Result expected = Result.Failure(FailureException);
        CancellationToken token = new();

        // When
        Result actual = await result.SelectAsync(_ => throw FailureException, token);

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
        Result<int> actual = await result.SelectAsync(() => Task.FromResult(1));

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
        Result<int> actual = await result.SelectAsync(() => Task.FromResult(1));

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
        Result<int> actual = await result.SelectAsync<int>(() => throw FailureException);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result.SelectAsync function of TResult with CancellationToken from failure should produce the expected result.")]
    public async Task ResultSelectAsyncFunctionOfTResultWithCancellationTokenFromFailureShouldProduceExpectedResult()
    {
        // Given
        Result result = Result.Failure(FailureException);
        Result<int> expected = Result<int>.Failure(FailureException);
        CancellationToken token = new();

        // When
        Result<int> actual = await result.SelectAsync(_ => Task.FromResult(1), token);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result.SelectAsync function of TResult with CancellationToken from success should produce the expected result.")]
    public async Task ResultSelectAsyncFunctionOfTResultWithCancellationTokenFromSuccessShouldProduceExpectedResult()
    {
        // Given
        Result result = Result.Success();
        Result<int> expected = 1;
        CancellationToken token = new();

        // When
        Result<int> actual = await result.SelectAsync(_ => Task.FromResult(1), token);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result.SelectAsync function of TResult with CancellationToken from success with exception should produce the expected result.")]
    public async Task ResultSelectAsyncFunctionOfTResultWithCancellationTokenFromSuccessWithExceptionShouldProduceExpectedResult()
    {
        // Given
        Result result = Result.Success();
        Result<int> expected = Result<int>.Failure(FailureException);
        CancellationToken token = new();

        // When
        Result<int> actual = await result.SelectAsync<int>(_ => throw FailureException, token);

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
        Result actual = await result.SelectManyAsync(async () => await Task.FromResult(Result.Success()));

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
        Result actual = await result.SelectManyAsync(async () => await Task.FromResult(Result.Success()));

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
        Result actual = await result.SelectManyAsync(() => throw FailureException);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result.SelectManyAsync function with CancellationToken from failure should produce the expected result.")]
    public async Task ResultSelectManyAsyncFunctionWithCancellationTokenFromFailureShouldProduceExpectedResult()
    {
        // Given
        Result result = Result.Failure(FailureException);
        Result expected = Result.Failure(FailureException);
        CancellationToken token = new();

        // When
        Result actual = await result.SelectManyAsync(async _ => await Task.FromResult(Result.Success()), token);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result.SelectManyAsync function with CancellationToken from success should produce the expected result.")]
    public async Task ResultSelectManyAsyncFunctionWithCancellationTokenFromSuccessShouldProduceExpectedResult()
    {
        // Given
        Result result = Result.Success();
        Result expected = Result.Success();
        CancellationToken token = new();

        // When
        Result actual = await result.SelectManyAsync(async _ => await Task.FromResult(Result.Success()), token);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result.SelectManyAsync function with CancellationToken from success with exception should produce the expected result.")]
    public async Task ResultSelectManyAsyncFunctionWithCancellationTokenFromSuccessWithExceptionShouldProduceExpectedResult()
    {
        // Given
        Result result = Result.Success();
        Result expected = Result.Failure(FailureException);
        CancellationToken token = new();

        // When
        Result actual = await result.SelectManyAsync(_ => throw FailureException, token);

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
        Result<int> actual = await result.SelectManyAsync<int>(async () => await Task.FromResult(Result<int>.Success(1)));

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
        Result<int> actual = await result.SelectManyAsync<int>(async () => await Task.FromResult(Result<int>.Success(1)));

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
        Result<int> actual = await result.SelectManyAsync<int>(() => throw FailureException);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result.SelectManyAsync function of TResult with CancellationToken from failure should produce the expected result.")]
    public async Task ResultSelectManyAsyncFunctionOfTResultWithCancellationTokenFromFailureShouldProduceExpectedResult()
    {
        // Given
        Result result = Result.Failure(FailureException);
        Result<int> expected = Result<int>.Failure(FailureException);
        CancellationToken token = new();

        // When
        Result<int> actual = await result.SelectManyAsync<int>(async _ => await Task.FromResult(Result<int>.Success(1)), token);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result.SelectManyAsync function of TResult with CancellationToken from success should produce the expected result.")]
    public async Task ResultSelectManyAsyncFunctionOfTResultWithCancellationTokenFromSuccessShouldProduceExpectedResult()
    {
        // Given
        Result result = Result.Success();
        Result<int> expected = Result<int>.Success(1);
        CancellationToken token = new();

        // When
        Result<int> actual = await result.SelectManyAsync<int>(async _ => await Task.FromResult(Result<int>.Success(1)), token);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result.SelectManyAsync function of TResult with CancellationToken from success with exception should produce the expected result.")]
    public async Task ResultSelectManyAsyncFunctionOfTResultWithCancellationTokenFromSuccessWithExceptionShouldProduceExpectedResult()
    {
        // Given
        Result result = Result.Success();
        Result<int> expected = Result<int>.Failure(FailureException);
        CancellationToken token = new();

        // When
        Result<int> actual = await result.SelectManyAsync<int>(_ => throw FailureException, token);

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
