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

public sealed class ValueResultTests
{
    private static readonly Exception FailureException = new("Failure");

    [Fact(DisplayName = "ValueResult.IsSuccess should produce the expected result")]
    public void ValueResultIsSuccessShouldProduceExpectedResult()
    {
        // Given
        ValueResult result = ValueResult.Success();

        // When / Then
        Assert.True(result.IsSuccess);
        Assert.False(result.IsFailure);
    }

    [Fact(DisplayName = "ValueResult.IsFailure should produce the expected result")]
    public void ValueResultIsFailureShouldProduceExpectedResult()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);

        // When / Then
        Assert.True(result.IsFailure);
        Assert.False(result.IsSuccess);
    }

    [Fact(DisplayName = "ValueResult default should be a success result")]
    public void ValueResultDefaultShouldBeSuccessResult()
    {
        // Given
        ValueResult[] results = new ValueResult[1];

        // When
        ValueResult result = results[0];

        // Then
        Assert.True(result.IsSuccess);
        Assert.Equal(ValueResult.Success(), result);
        Assert.Equal(ValueResult.Success(), default);
    }

    [Fact(DisplayName = "ValueResult.Of should produce the expected success result")]
    public void ValueResultOfShouldProduceExpectedSuccessResult()
    {
        // Given / When
        ValueResult result = ValueResult.Of(() => { });

        // Then
        Assert.True(result.IsSuccess);
    }

    [Fact(DisplayName = "ValueResult.Of should produce the expected failure result")]
    public void ValueResultOfShouldProduceExpectedFailureResult()
    {
        // Given / When
        ValueResult result = ValueResult.Of(() => throw FailureException);

        // Then
        Assert.True(result.IsFailure);
        Assert.Same(FailureException, result.GetExceptionOrDefault());
    }

    [Fact(DisplayName = "ValueResult.OfAsync should produce the expected success result.")]
    public async Task ValueResultOfAsyncShouldProduceExpectedSuccessResult()
    {
        // Given / When
        ValueResult result = await ValueResult.OfAsync(async () => await Task.CompletedTask, TestContext.Current.CancellationToken);

        // Then
        Assert.True(result.IsSuccess);
    }

    [Fact(DisplayName = "ValueResult.OfAsync should produce the expected failure result.")]
    public async Task ValueResultOfAsyncShouldProduceExpectedFailureResult()
    {
        // Given / When
        ValueResult result = await ValueResult.OfAsync(async () => await Task.FromException(FailureException), TestContext.Current.CancellationToken);

        // Then
        Assert.True(result.IsFailure);
        Assert.Equal(FailureException, result.GetExceptionOrThrow());
    }

    [Fact(DisplayName = "ValueResult.OfAsync with cancellation token should produce the expected success result.")]
    public async Task ValueResultOfAsyncWithCancellationTokenShouldProduceExpectedSuccessResult()
    {
        // Given / When
        ValueResult result = await ValueResult.OfAsync(async () => await Task.CompletedTask, CancellationToken.None);

        // Then
        Assert.True(result.IsSuccess);
    }

    [Fact(DisplayName = "ValueResult.OfAsync with cancellation token should produce the expected failure result.")]
    public async Task ValueResultOfAsyncWithCancellationTokenShouldProduceExpectedFailureResult()
    {
        // Given / When
        ValueResult result = await ValueResult.OfAsync(async () => await Task.FromException(FailureException), CancellationToken.None);

        // Then
        Assert.True(result.IsFailure);
        Assert.Equal(FailureException, result.GetExceptionOrThrow());
    }

    [Fact(DisplayName = "ValueResult.OfAsync with cancellable function should produce the expected success result.")]
    public async Task ValueResultOfAsyncWithCancellableFunctionShouldProduceExpectedSuccessResult()
    {
        // Given / When
        ValueResult result = await ValueResult.OfAsync(async _ => await Task.CompletedTask, CancellationToken.None);

        // Then
        Assert.True(result.IsSuccess);
    }

    [Fact(DisplayName = "ValueResult.OfAsync with cancellable function should produce the expected failure result.")]
    public async Task ValueResultOfAsyncWithCancellableFunctionShouldProduceExpectedFailureResult()
    {
        // Given / When
        ValueResult result = await ValueResult.OfAsync(async _ => await Task.FromException(FailureException), CancellationToken.None);

        // Then
        Assert.True(result.IsFailure);
        Assert.Equal(FailureException, result.GetExceptionOrThrow());
    }

    [Fact(DisplayName = "ValueResult.Success should produce the expected success result.")]
    public void ValueResultSuccessShouldProduceExpectedSuccessResult()
    {
        // Given / When
        ValueResult result = ValueResult.Success();

        // Then
        Assert.True(result.IsSuccess);
    }

    [Fact(DisplayName = "ValueResult.Failure should produce the expected failure result.")]
    public void ValueResultFailureShouldProduceExpectedFailureResult()
    {
        // Given / When
        ValueResult result = ValueResult.Failure(FailureException);

        // Then
        Assert.True(result.IsFailure);
        Assert.Equal(FailureException, result.GetExceptionOrThrow());
    }

    [Fact(DisplayName = "ValueResult.Failure with a null exception should throw ArgumentNullException")]
    public void ValueResultFailureWithNullExceptionShouldThrowArgumentNullException()
    {
        // Given / When / Then
        Assert.Throws<ArgumentNullException>(() => ValueResult.Failure(null!));
    }

    [Fact(DisplayName = "ValueResult from implicit exception should produce the expected failure result")]
    public void ValueResultFromImplicitExceptionShouldProduceExpectedFailureResult()
    {
        // Given / When
        ValueResult result = FailureException;

        // Then
        Assert.True(result.IsFailure);
        Assert.Same(FailureException, result.GetExceptionOrDefault());
    }

    [Fact(DisplayName = "ValueResult equality should produce the expected result")]
    public void ValueResultEqualityShouldProduceExpectedResult()
    {
        // Given
        ValueResult success = ValueResult.Success();
        ValueResult failure = ValueResult.Failure(FailureException);

        // When / Then
        Assert.True(success == ValueResult.Success());
        Assert.True(failure == ValueResult.Failure(FailureException));
        Assert.True(failure != ValueResult.Failure(new Exception("Failure")));
        Assert.True(success != failure);
        Assert.True(failure.Equals((object)ValueResult.Failure(FailureException)));
        Assert.False(success.Equals((object)Result.Success()));
        Assert.Equal(ValueResult.Failure(FailureException).GetHashCode(), failure.GetHashCode());
        Assert.Equal(0, success.GetHashCode());

        // ReSharper disable EqualExpressionComparison

        // Equals Method
        Assert.True(ValueResult.Success().Equals(ValueResult.Success()));
        Assert.True(ValueResult.Failure(FailureException).Equals(ValueResult.Failure(FailureException)));
        Assert.False(ValueResult.Success().Equals((object?)null));
        Assert.False(ValueResult.Failure(FailureException).Equals((object?)null));

        // Equality Operator
        Assert.False(ValueResult.Success() == ValueResult.Failure(FailureException));

        // Inequality Operator
        Assert.True(ValueResult.Failure(FailureException) != ValueResult.Failure(new Exception()));

        // Hash Code Generation
        Assert.True(ValueResult.Success().GetHashCode() == ValueResult.Success().GetHashCode());
        Assert.True(ValueResult.Failure(FailureException).GetHashCode() == ValueResult.Failure(FailureException).GetHashCode());
    }

    [Fact(DisplayName = "ValueResult.GetExceptionOrDefault should produce the expected result")]
    public void ValueResultGetExceptionOrDefaultShouldProduceExpectedResult()
    {
        // Given
        ValueResult success = ValueResult.Success();
        ValueResult failure = ValueResult.Failure(FailureException);
        Exception defaultException = new("Default");

        // When / Then
        Assert.Null(success.GetExceptionOrDefault());
        Assert.Same(FailureException, failure.GetExceptionOrDefault());
        Assert.Same(defaultException, success.GetExceptionOrDefault(defaultException));
        Assert.Same(FailureException, failure.GetExceptionOrDefault(defaultException));
    }

    [Fact(DisplayName = "ValueResult.GetExceptionOrNone should produce the expected result")]
    public void ValueResultGetExceptionOrNoneShouldProduceExpectedResult()
    {
        // Given
        ValueResult success = ValueResult.Success();
        ValueResult failure = ValueResult.Failure(FailureException);

        // When / Then
        Assert.Equal(ValueOptional<Exception>.None, success.GetExceptionOrNone());
        Assert.Equal(ValueOptional<Exception>.Some(FailureException), failure.GetExceptionOrNone());
    }

    [Fact(DisplayName = "ValueResult.GetExceptionOrThrow should produce the expected result")]
    public void ValueResultGetExceptionOrThrowShouldProduceExpectedResult()
    {
        // Given
        ValueResult success = ValueResult.Success();
        ValueResult failure = ValueResult.Failure(FailureException);

        // When / Then
        Assert.Same(FailureException, failure.GetExceptionOrThrow());
        Exception exception = Assert.Throws<InvalidOperationException>(() => success.GetExceptionOrThrow());
        Assert.Equal("The current result is in a success state; no exception to retrieve.", exception.Message);
    }

    [Fact(DisplayName = "ValueResult.Match action should invoke the delegate that matches the state")]
    public void ValueResultMatchActionShouldInvokeTheDelegateThatMatchesTheState()
    {
        // Given
        ValueResult success = ValueResult.Success();
        ValueResult failure = ValueResult.Failure(FailureException);
        string matched = string.Empty;

        // When / Then
        success.Match(success: () => matched = "Success", failure: exception => matched = exception.Message);
        Assert.Equal("Success", matched);

        failure.Match(success: () => matched = "Success", failure: exception => matched = exception.Message);
        Assert.Equal("Failure", matched);
    }

    [Fact(DisplayName = "ValueResult.MatchAsync(Func<Task>, Action<Exception>) from success should produce the expected result.")]
    public async Task ValueResultMatchAsyncFuncTaskActionFromSuccessShouldProduceExpectedResult()
    {
        // Given
        ValueResult result = ValueResult.Success();
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

    [Fact(DisplayName = "ValueResult.MatchAsync(Func<Task>, Action<Exception>) from success should produce the expected result when the delegates complete asynchronously.")]
    public async Task ValueResultMatchAsyncFuncTaskActionFromSuccessShouldProduceExpectedResultWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult result = ValueResult.Success();
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

    [Fact(DisplayName = "ValueResult.MatchAsync(Func<ValueTask>, Action<Exception>) from success should produce the expected result.")]
    public async Task ValueResultMatchAsyncFuncTaskActionFromSuccessShouldProduceExpectedResultWithValueTaskDelegates()
    {
        // Given
        ValueResult result = ValueResult.Success();
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

    [Fact(DisplayName = "ValueResult.MatchAsync(Func<ValueTask>, Action<Exception>) from success should produce the expected result when the delegates complete asynchronously.")]
    public async Task ValueResultMatchAsyncFuncTaskActionFromSuccessShouldProduceExpectedResultWithValueTaskDelegatesWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult result = ValueResult.Success();
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

    [Fact(DisplayName = "ValueResult.MatchAsync(Func<Task>, Action<Exception>) from failure should produce the expected result.")]
    public async Task ValueResultMatchAsyncFuncTaskActionFromFailureShouldProduceExpectedResult()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
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

    [Fact(DisplayName = "ValueResult.MatchAsync(Func<Task>, Action<Exception>) from failure should produce the expected result when the delegates complete asynchronously.")]
    public async Task ValueResultMatchAsyncFuncTaskActionFromFailureShouldProduceExpectedResultWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
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

    [Fact(DisplayName = "ValueResult.MatchAsync(Func<ValueTask>, Action<Exception>) from failure should produce the expected result.")]
    public async Task ValueResultMatchAsyncFuncTaskActionFromFailureShouldProduceExpectedResultWithValueTaskDelegates()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
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

    [Fact(DisplayName = "ValueResult.MatchAsync(Func<ValueTask>, Action<Exception>) from failure should produce the expected result when the delegates complete asynchronously.")]
    public async Task ValueResultMatchAsyncFuncTaskActionFromFailureShouldProduceExpectedResultWithValueTaskDelegatesWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
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

    [Fact(DisplayName = "ValueResult.MatchAsync(Func<CancellationToken, Task>, Action<Exception>) from success should produce the expected result.")]
    public async Task ValueResultMatchAsyncFuncCancellationTokenTaskActionFromSuccessShouldProduceExpectedResult()
    {
        // Given
        ValueResult result = ValueResult.Success();
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

    [Fact(DisplayName = "ValueResult.MatchAsync(Func<CancellationToken, Task>, Action<Exception>) from success should produce the expected result when the delegates complete asynchronously.")]
    public async Task ValueResultMatchAsyncFuncCancellationTokenTaskActionFromSuccessShouldProduceExpectedResultWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult result = ValueResult.Success();
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

    [Fact(DisplayName = "ValueResult.MatchAsync(Func<CancellationToken, ValueTask>, Action<Exception>) from success should produce the expected result.")]
    public async Task ValueResultMatchAsyncFuncCancellationTokenTaskActionFromSuccessShouldProduceExpectedResultWithValueTaskDelegates()
    {
        // Given
        ValueResult result = ValueResult.Success();
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

    [Fact(DisplayName = "ValueResult.MatchAsync(Func<CancellationToken, ValueTask>, Action<Exception>) from success should produce the expected result when the delegates complete asynchronously.")]
    public async Task ValueResultMatchAsyncFuncCancellationTokenTaskActionFromSuccessShouldProduceExpectedResultWithValueTaskDelegatesWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult result = ValueResult.Success();
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

    [Fact(DisplayName = "ValueResult.MatchAsync(Func<CancellationToken, Task>, Action<Exception>) from failure should produce the expected result.")]
    public async Task ValueResultMatchAsyncFuncCancellationTokenTaskActionFromFailureShouldProduceExpectedResult()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
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

    [Fact(DisplayName = "ValueResult.MatchAsync(Func<CancellationToken, Task>, Action<Exception>) from failure should produce the expected result when the delegates complete asynchronously.")]
    public async Task ValueResultMatchAsyncFuncCancellationTokenTaskActionFromFailureShouldProduceExpectedResultWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
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

    [Fact(DisplayName = "ValueResult.MatchAsync(Func<CancellationToken, ValueTask>, Action<Exception>) from failure should produce the expected result.")]
    public async Task ValueResultMatchAsyncFuncCancellationTokenTaskActionFromFailureShouldProduceExpectedResultWithValueTaskDelegates()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
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

    [Fact(DisplayName = "ValueResult.MatchAsync(Func<CancellationToken, ValueTask>, Action<Exception>) from failure should produce the expected result when the delegates complete asynchronously.")]
    public async Task ValueResultMatchAsyncFuncCancellationTokenTaskActionFromFailureShouldProduceExpectedResultWithValueTaskDelegatesWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
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

    [Fact(DisplayName = "ValueResult.MatchAsync(Action, Func<Exception, Task>) from success should produce the expected result.")]
    public async Task ValueResultMatchAsyncActionFuncExceptionTaskFromSuccessShouldProduceExpectedResult()
    {
        // Given
        ValueResult result = ValueResult.Success();
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

    [Fact(DisplayName = "ValueResult.MatchAsync(Action, Func<Exception, Task>) from success should produce the expected result when the delegates complete asynchronously.")]
    public async Task ValueResultMatchAsyncActionFuncExceptionTaskFromSuccessShouldProduceExpectedResultWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult result = ValueResult.Success();
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

    [Fact(DisplayName = "ValueResult.MatchAsync(Action, Func<Exception, ValueTask>) from success should produce the expected result.")]
    public async Task ValueResultMatchAsyncActionFuncExceptionTaskFromSuccessShouldProduceExpectedResultWithValueTaskDelegates()
    {
        // Given
        ValueResult result = ValueResult.Success();
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

    [Fact(DisplayName = "ValueResult.MatchAsync(Action, Func<Exception, ValueTask>) from success should produce the expected result when the delegates complete asynchronously.")]
    public async Task ValueResultMatchAsyncActionFuncExceptionTaskFromSuccessShouldProduceExpectedResultWithValueTaskDelegatesWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult result = ValueResult.Success();
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

    [Fact(DisplayName = "ValueResult.MatchAsync(Action, Func<Exception, Task>) from failure should produce the expected result.")]
    public async Task ValueResultMatchAsyncActionFuncExceptionTaskFromFailureShouldProduceExpectedResult()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
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

    [Fact(DisplayName = "ValueResult.MatchAsync(Action, Func<Exception, Task>) from failure should produce the expected result when the delegates complete asynchronously.")]
    public async Task ValueResultMatchAsyncActionFuncExceptionTaskFromFailureShouldProduceExpectedResultWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
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

    [Fact(DisplayName = "ValueResult.MatchAsync(Action, Func<Exception, ValueTask>) from failure should produce the expected result.")]
    public async Task ValueResultMatchAsyncActionFuncExceptionTaskFromFailureShouldProduceExpectedResultWithValueTaskDelegates()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
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

    [Fact(DisplayName = "ValueResult.MatchAsync(Action, Func<Exception, ValueTask>) from failure should produce the expected result when the delegates complete asynchronously.")]
    public async Task ValueResultMatchAsyncActionFuncExceptionTaskFromFailureShouldProduceExpectedResultWithValueTaskDelegatesWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
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

    [Fact(DisplayName = "ValueResult.MatchAsync(Action, Func<Exception, CancellationToken, Task>) from success should produce the expected result.")]
    public async Task ValueResultMatchAsyncActionFuncExceptionCancellationTokenTaskFromSuccessShouldProduceExpectedResult()
    {
        // Given
        ValueResult result = ValueResult.Success();
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

    [Fact(DisplayName = "ValueResult.MatchAsync(Action, Func<Exception, CancellationToken, Task>) from success should produce the expected result when the delegates complete asynchronously.")]
    public async Task ValueResultMatchAsyncActionFuncExceptionCancellationTokenTaskFromSuccessShouldProduceExpectedResultWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult result = ValueResult.Success();
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

    [Fact(DisplayName = "ValueResult.MatchAsync(Action, Func<Exception, CancellationToken, ValueTask>) from success should produce the expected result.")]
    public async Task ValueResultMatchAsyncActionFuncExceptionCancellationTokenTaskFromSuccessShouldProduceExpectedResultWithValueTaskDelegates()
    {
        // Given
        ValueResult result = ValueResult.Success();
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

    [Fact(DisplayName = "ValueResult.MatchAsync(Action, Func<Exception, CancellationToken, ValueTask>) from success should produce the expected result when the delegates complete asynchronously.")]
    public async Task ValueResultMatchAsyncActionFuncExceptionCancellationTokenTaskFromSuccessShouldProduceExpectedResultWithValueTaskDelegatesWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult result = ValueResult.Success();
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

    [Fact(DisplayName = "ValueResult.MatchAsync(Action, Func<Exception, CancellationToken, Task>) from failure should produce the expected result.")]
    public async Task ValueResultMatchAsyncActionFuncExceptionCancellationTokenTaskFromFailureShouldProduceExpectedResult()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
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

    [Fact(DisplayName = "ValueResult.MatchAsync(Action, Func<Exception, CancellationToken, Task>) from failure should produce the expected result when the delegates complete asynchronously.")]
    public async Task ValueResultMatchAsyncActionFuncExceptionCancellationTokenTaskFromFailureShouldProduceExpectedResultWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
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

    [Fact(DisplayName = "ValueResult.MatchAsync(Action, Func<Exception, CancellationToken, ValueTask>) from failure should produce the expected result.")]
    public async Task ValueResultMatchAsyncActionFuncExceptionCancellationTokenTaskFromFailureShouldProduceExpectedResultWithValueTaskDelegates()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
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

    [Fact(DisplayName = "ValueResult.MatchAsync(Action, Func<Exception, CancellationToken, ValueTask>) from failure should produce the expected result when the delegates complete asynchronously.")]
    public async Task ValueResultMatchAsyncActionFuncExceptionCancellationTokenTaskFromFailureShouldProduceExpectedResultWithValueTaskDelegatesWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
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

    [Fact(DisplayName = "ValueResult.MatchAsync(Func<Task>, Func<Exception, Task>) from success should produce the expected result.")]
    public async Task ValueResultMatchAsyncFuncTaskFuncExceptionTaskFromSuccessShouldProduceExpectedResult()
    {
        // Given
        ValueResult result = ValueResult.Success();
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

    [Fact(DisplayName = "ValueResult.MatchAsync(Func<Task>, Func<Exception, Task>) from success should produce the expected result when the delegates complete asynchronously.")]
    public async Task ValueResultMatchAsyncFuncTaskFuncExceptionTaskFromSuccessShouldProduceExpectedResultWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult result = ValueResult.Success();
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

    [Fact(DisplayName = "ValueResult.MatchAsync(Func<ValueTask>, Func<Exception, ValueTask>) from success should produce the expected result.")]
    public async Task ValueResultMatchAsyncFuncTaskFuncExceptionTaskFromSuccessShouldProduceExpectedResultWithValueTaskDelegates()
    {
        // Given
        ValueResult result = ValueResult.Success();
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

    [Fact(DisplayName = "ValueResult.MatchAsync(Func<ValueTask>, Func<Exception, ValueTask>) from success should produce the expected result when the delegates complete asynchronously.")]
    public async Task ValueResultMatchAsyncFuncTaskFuncExceptionTaskFromSuccessShouldProduceExpectedResultWithValueTaskDelegatesWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult result = ValueResult.Success();
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

    [Fact(DisplayName = "ValueResult.MatchAsync(Func<Task>, Func<Exception, Task>) from failure should produce the expected result.")]
    public async Task ValueResultMatchAsyncFuncTaskFuncExceptionTaskFromFailureShouldProduceExpectedResult()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
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

    [Fact(DisplayName = "ValueResult.MatchAsync(Func<Task>, Func<Exception, Task>) from failure should produce the expected result when the delegates complete asynchronously.")]
    public async Task ValueResultMatchAsyncFuncTaskFuncExceptionTaskFromFailureShouldProduceExpectedResultWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
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

    [Fact(DisplayName = "ValueResult.MatchAsync(Func<ValueTask>, Func<Exception, ValueTask>) from failure should produce the expected result.")]
    public async Task ValueResultMatchAsyncFuncTaskFuncExceptionTaskFromFailureShouldProduceExpectedResultWithValueTaskDelegates()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
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

    [Fact(DisplayName = "ValueResult.MatchAsync(Func<ValueTask>, Func<Exception, ValueTask>) from failure should produce the expected result when the delegates complete asynchronously.")]
    public async Task ValueResultMatchAsyncFuncTaskFuncExceptionTaskFromFailureShouldProduceExpectedResultWithValueTaskDelegatesWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
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

    [Fact(DisplayName = "ValueResult.MatchAsync(Func<CancellationToken, Task>, Func<Exception, CancellationToken, Task>) from success should produce the expected result.")]
    public async Task ValueResultMatchAsyncFuncCancellationTokenTaskFuncExceptionCancellationTokenTaskFromSuccessShouldProduceExpectedResult()
    {
        // Given
        ValueResult result = ValueResult.Success();
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

    [Fact(DisplayName = "ValueResult.MatchAsync(Func<CancellationToken, Task>, Func<Exception, CancellationToken, Task>) from success should produce the expected result when the delegates complete asynchronously.")]
    public async Task ValueResultMatchAsyncFuncCancellationTokenTaskFuncExceptionCancellationTokenTaskFromSuccessShouldProduceExpectedResultWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult result = ValueResult.Success();
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

    [Fact(DisplayName = "ValueResult.MatchAsync(Func<CancellationToken, ValueTask>, Func<Exception, CancellationToken, ValueTask>) from success should produce the expected result.")]
    public async Task ValueResultMatchAsyncFuncCancellationTokenTaskFuncExceptionCancellationTokenTaskFromSuccessShouldProduceExpectedResultWithValueTaskDelegates()
    {
        // Given
        ValueResult result = ValueResult.Success();
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

    [Fact(DisplayName = "ValueResult.MatchAsync(Func<CancellationToken, ValueTask>, Func<Exception, CancellationToken, ValueTask>) from success should produce the expected result when the delegates complete asynchronously.")]
    public async Task ValueResultMatchAsyncFuncCancellationTokenTaskFuncExceptionCancellationTokenTaskFromSuccessShouldProduceExpectedResultWithValueTaskDelegatesWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult result = ValueResult.Success();
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

    [Fact(DisplayName = "ValueResult.MatchAsync(Func<CancellationToken, Task>, Func<Exception, CancellationToken, Task>) from failure should produce the expected result.")]
    public async Task ValueResultMatchAsyncFuncCancellationTokenTaskFuncExceptionCancellationTokenTaskFromFailureShouldProduceExpectedResult()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
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

    [Fact(DisplayName = "ValueResult.MatchAsync(Func<CancellationToken, Task>, Func<Exception, CancellationToken, Task>) from failure should produce the expected result when the delegates complete asynchronously.")]
    public async Task ValueResultMatchAsyncFuncCancellationTokenTaskFuncExceptionCancellationTokenTaskFromFailureShouldProduceExpectedResultWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
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

    [Fact(DisplayName = "ValueResult.MatchAsync(Func<CancellationToken, ValueTask>, Func<Exception, CancellationToken, ValueTask>) from failure should produce the expected result.")]
    public async Task ValueResultMatchAsyncFuncCancellationTokenTaskFuncExceptionCancellationTokenTaskFromFailureShouldProduceExpectedResultWithValueTaskDelegates()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
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

    [Fact(DisplayName = "ValueResult.MatchAsync(Func<CancellationToken, ValueTask>, Func<Exception, CancellationToken, ValueTask>) from failure should produce the expected result when the delegates complete asynchronously.")]
    public async Task ValueResultMatchAsyncFuncCancellationTokenTaskFuncExceptionCancellationTokenTaskFromFailureShouldProduceExpectedResultWithValueTaskDelegatesWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
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

    [Fact(DisplayName = "ValueResult.Match function should return the result of the delegate that matches the state")]
    public void ValueResultMatchFunctionShouldReturnTheResultOfTheDelegateThatMatchesTheState()
    {
        // Given
        ValueResult success = ValueResult.Success();
        ValueResult failure = ValueResult.Failure(FailureException);

        // When
        string successText = success.Match(success: () => "Success", failure: exception => exception.Message);
        string failureText = failure.Match(success: () => "Success", failure: exception => exception.Message);

        // Then
        Assert.Equal("Success", successText);
        Assert.Equal("Failure", failureText);
    }

    [Fact(DisplayName = "ValueResult.MatchAsync(Func<Task<TResult>>, Func<Exception, TResult>) from success should produce the expected result.")]
    public async Task ValueResultMatchAsyncFuncTaskTResultFuncExceptionTResultFromSuccessShouldProduceExpectedResult()
    {
        // Given
        ValueResult result = ValueResult.Success();
        const int expected = 1;

        // When
        int actual = await result.MatchAsync(Success, Failure, TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        Task<int> Success() => Task.FromResult(1);
        int Failure(Exception exception) => 0;
    }

    [Fact(DisplayName = "ValueResult.MatchAsync(Func<Task<TResult>>, Func<Exception, TResult>) from success should produce the expected result when the delegates complete asynchronously.")]
    public async Task ValueResultMatchAsyncFuncTaskTResultFuncExceptionTResultFromSuccessShouldProduceExpectedResultWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult result = ValueResult.Success();
        const int expected = 1;

        // When
        int actual = await result.MatchAsync(Success, Failure, TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        Task<int> Success() => Yielding.TaskOf(1);
        int Failure(Exception exception) => 0;
    }

    [Fact(DisplayName = "ValueResult.MatchAsync(Func<ValueTask<TResult>>, Func<Exception, TResult>) from success should produce the expected result.")]
    public async Task ValueResultMatchAsyncFuncTaskTResultFuncExceptionTResultFromSuccessShouldProduceExpectedResultWithValueTaskDelegates()
    {
        // Given
        ValueResult result = ValueResult.Success();
        const int expected = 1;

        // When
        int actual = await result.MatchAsync(Success, Failure, TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        ValueTask<int> Success() => ValueTask.FromResult(1);
        int Failure(Exception exception) => 0;
    }

    [Fact(DisplayName = "ValueResult.MatchAsync(Func<ValueTask<TResult>>, Func<Exception, TResult>) from success should produce the expected result when the delegates complete asynchronously.")]
    public async Task ValueResultMatchAsyncFuncTaskTResultFuncExceptionTResultFromSuccessShouldProduceExpectedResultWithValueTaskDelegatesWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult result = ValueResult.Success();
        const int expected = 1;

        // When
        int actual = await result.MatchAsync(Success, Failure, TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        ValueTask<int> Success() => Yielding.ValueTaskOf(1);
        int Failure(Exception exception) => 0;
    }

    [Fact(DisplayName = "ValueResult.MatchAsync(Func<Task<TResult>>, Func<Exception, TResult>) from failure should produce the expected result.")]
    public async Task ValueResultMatchAsyncFuncTaskTResultFuncExceptionTResultFromFailureShouldProduceExpectedResult()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
        const int expected = 0;

        // When
        int actual = await result.MatchAsync(Success, Failure, TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        Task<int> Success() => Task.FromResult(1);
        int Failure(Exception exception) => 0;
    }

    [Fact(DisplayName = "ValueResult.MatchAsync(Func<Task<TResult>>, Func<Exception, TResult>) from failure should produce the expected result when the delegates complete asynchronously.")]
    public async Task ValueResultMatchAsyncFuncTaskTResultFuncExceptionTResultFromFailureShouldProduceExpectedResultWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
        const int expected = 0;

        // When
        int actual = await result.MatchAsync(Success, Failure, TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        Task<int> Success() => Yielding.TaskOf(1);
        int Failure(Exception exception) => 0;
    }

    [Fact(DisplayName = "ValueResult.MatchAsync(Func<ValueTask<TResult>>, Func<Exception, TResult>) from failure should produce the expected result.")]
    public async Task ValueResultMatchAsyncFuncTaskTResultFuncExceptionTResultFromFailureShouldProduceExpectedResultWithValueTaskDelegates()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
        const int expected = 0;

        // When
        int actual = await result.MatchAsync(Success, Failure, TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        ValueTask<int> Success() => ValueTask.FromResult(1);
        int Failure(Exception exception) => 0;
    }

    [Fact(DisplayName = "ValueResult.MatchAsync(Func<ValueTask<TResult>>, Func<Exception, TResult>) from failure should produce the expected result when the delegates complete asynchronously.")]
    public async Task ValueResultMatchAsyncFuncTaskTResultFuncExceptionTResultFromFailureShouldProduceExpectedResultWithValueTaskDelegatesWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
        const int expected = 0;

        // When
        int actual = await result.MatchAsync(Success, Failure, TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        ValueTask<int> Success() => Yielding.ValueTaskOf(1);
        int Failure(Exception exception) => 0;
    }

    [Fact(DisplayName = "ValueResult.MatchAsync(Func<CancellationToken, Task<TResult>>, Func<Exception, TResult>) from success should produce the expected result.")]
    public async Task ValueResultMatchAsyncFuncCancellationTokenTaskTResultFuncExceptionTResultFromSuccessShouldProduceExpectedResult()
    {
        // Given
        ValueResult result = ValueResult.Success();
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

    [Fact(DisplayName = "ValueResult.MatchAsync(Func<CancellationToken, Task<TResult>>, Func<Exception, TResult>) from success should produce the expected result when the delegates complete asynchronously.")]
    public async Task ValueResultMatchAsyncFuncCancellationTokenTaskTResultFuncExceptionTResultFromSuccessShouldProduceExpectedResultWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult result = ValueResult.Success();
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

    [Fact(DisplayName = "ValueResult.MatchAsync(Func<CancellationToken, ValueTask<TResult>>, Func<Exception, TResult>) from success should produce the expected result.")]
    public async Task ValueResultMatchAsyncFuncCancellationTokenTaskTResultFuncExceptionTResultFromSuccessShouldProduceExpectedResultWithValueTaskDelegates()
    {
        // Given
        ValueResult result = ValueResult.Success();
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

    [Fact(DisplayName = "ValueResult.MatchAsync(Func<CancellationToken, ValueTask<TResult>>, Func<Exception, TResult>) from success should produce the expected result when the delegates complete asynchronously.")]
    public async Task ValueResultMatchAsyncFuncCancellationTokenTaskTResultFuncExceptionTResultFromSuccessShouldProduceExpectedResultWithValueTaskDelegatesWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult result = ValueResult.Success();
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

    [Fact(DisplayName = "ValueResult.MatchAsync(Func<CancellationToken, Task<TResult>>, Func<Exception, TResult>) from failure should produce the expected result.")]
    public async Task ValueResultMatchAsyncFuncCancellationTokenTaskTResultFuncExceptionTResultFromFailureShouldProduceExpectedResult()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
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

    [Fact(DisplayName = "ValueResult.MatchAsync(Func<CancellationToken, Task<TResult>>, Func<Exception, TResult>) from failure should produce the expected result when the delegates complete asynchronously.")]
    public async Task ValueResultMatchAsyncFuncCancellationTokenTaskTResultFuncExceptionTResultFromFailureShouldProduceExpectedResultWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
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

    [Fact(DisplayName = "ValueResult.MatchAsync(Func<CancellationToken, ValueTask<TResult>>, Func<Exception, TResult>) from failure should produce the expected result.")]
    public async Task ValueResultMatchAsyncFuncCancellationTokenTaskTResultFuncExceptionTResultFromFailureShouldProduceExpectedResultWithValueTaskDelegates()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
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

    [Fact(DisplayName = "ValueResult.MatchAsync(Func<CancellationToken, ValueTask<TResult>>, Func<Exception, TResult>) from failure should produce the expected result when the delegates complete asynchronously.")]
    public async Task ValueResultMatchAsyncFuncCancellationTokenTaskTResultFuncExceptionTResultFromFailureShouldProduceExpectedResultWithValueTaskDelegatesWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
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

    [Fact(DisplayName = "ValueResult.MatchAsync(Func<TResult>, Func<Exception, Task<TResult>>) from success should produce the expected result.")]
    public async Task ValueResultMatchAsyncFuncTResultFuncExceptionTaskTResultFromSuccessShouldProduceExpectedResult()
    {
        // Given
        ValueResult result = ValueResult.Success();
        const int expected = 1;

        // When
        int actual = await result.MatchAsync(Success, Failure, TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        int Success() => 1;
        Task<int> Failure(Exception exception) => Task.FromResult(0);
    }

    [Fact(DisplayName = "ValueResult.MatchAsync(Func<TResult>, Func<Exception, Task<TResult>>) from success should produce the expected result when the delegates complete asynchronously.")]
    public async Task ValueResultMatchAsyncFuncTResultFuncExceptionTaskTResultFromSuccessShouldProduceExpectedResultWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult result = ValueResult.Success();
        const int expected = 1;

        // When
        int actual = await result.MatchAsync(Success, Failure, TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        int Success() => 1;
        Task<int> Failure(Exception exception) => Yielding.TaskOf(0);
    }

    [Fact(DisplayName = "ValueResult.MatchAsync(Func<TResult>, Func<Exception, ValueTask<TResult>>) from success should produce the expected result.")]
    public async Task ValueResultMatchAsyncFuncTResultFuncExceptionTaskTResultFromSuccessShouldProduceExpectedResultWithValueTaskDelegates()
    {
        // Given
        ValueResult result = ValueResult.Success();
        const int expected = 1;

        // When
        int actual = await result.MatchAsync(Success, Failure, TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        int Success() => 1;
        ValueTask<int> Failure(Exception exception) => ValueTask.FromResult(0);
    }

    [Fact(DisplayName = "ValueResult.MatchAsync(Func<TResult>, Func<Exception, ValueTask<TResult>>) from success should produce the expected result when the delegates complete asynchronously.")]
    public async Task ValueResultMatchAsyncFuncTResultFuncExceptionTaskTResultFromSuccessShouldProduceExpectedResultWithValueTaskDelegatesWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult result = ValueResult.Success();
        const int expected = 1;

        // When
        int actual = await result.MatchAsync(Success, Failure, TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        int Success() => 1;
        ValueTask<int> Failure(Exception exception) => Yielding.ValueTaskOf(0);
    }

    [Fact(DisplayName = "ValueResult.MatchAsync(Func<TResult>, Func<Exception, Task<TResult>>) from failure should produce the expected result.")]
    public async Task ValueResultMatchAsyncFuncTResultFuncExceptionTaskTResultFromFailureShouldProduceExpectedResult()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
        const int expected = 0;

        // When
        int actual = await result.MatchAsync(Success, Failure, TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        int Success() => 1;
        Task<int> Failure(Exception exception) => Task.FromResult(0);
    }

    [Fact(DisplayName = "ValueResult.MatchAsync(Func<TResult>, Func<Exception, Task<TResult>>) from failure should produce the expected result when the delegates complete asynchronously.")]
    public async Task ValueResultMatchAsyncFuncTResultFuncExceptionTaskTResultFromFailureShouldProduceExpectedResultWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
        const int expected = 0;

        // When
        int actual = await result.MatchAsync(Success, Failure, TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        int Success() => 1;
        Task<int> Failure(Exception exception) => Yielding.TaskOf(0);
    }

    [Fact(DisplayName = "ValueResult.MatchAsync(Func<TResult>, Func<Exception, ValueTask<TResult>>) from failure should produce the expected result.")]
    public async Task ValueResultMatchAsyncFuncTResultFuncExceptionTaskTResultFromFailureShouldProduceExpectedResultWithValueTaskDelegates()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
        const int expected = 0;

        // When
        int actual = await result.MatchAsync(Success, Failure, TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        int Success() => 1;
        ValueTask<int> Failure(Exception exception) => ValueTask.FromResult(0);
    }

    [Fact(DisplayName = "ValueResult.MatchAsync(Func<TResult>, Func<Exception, ValueTask<TResult>>) from failure should produce the expected result when the delegates complete asynchronously.")]
    public async Task ValueResultMatchAsyncFuncTResultFuncExceptionTaskTResultFromFailureShouldProduceExpectedResultWithValueTaskDelegatesWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
        const int expected = 0;

        // When
        int actual = await result.MatchAsync(Success, Failure, TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        int Success() => 1;
        ValueTask<int> Failure(Exception exception) => Yielding.ValueTaskOf(0);
    }

    [Fact(DisplayName = "ValueResult.MatchAsync(Func<TResult>, Func<Exception, CancellationToken, Task<TResult>>) from success should produce the expected result.")]
    public async Task ValueResultMatchAsyncFuncTResultFuncExceptionCancellationTokenTaskTResultFromSuccessShouldProduceExpectedResult()
    {
        // Given
        ValueResult result = ValueResult.Success();
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

    [Fact(DisplayName = "ValueResult.MatchAsync(Func<TResult>, Func<Exception, CancellationToken, Task<TResult>>) from success should produce the expected result when the delegates complete asynchronously.")]
    public async Task ValueResultMatchAsyncFuncTResultFuncExceptionCancellationTokenTaskTResultFromSuccessShouldProduceExpectedResultWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult result = ValueResult.Success();
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

    [Fact(DisplayName = "ValueResult.MatchAsync(Func<TResult>, Func<Exception, CancellationToken, ValueTask<TResult>>) from success should produce the expected result.")]
    public async Task ValueResultMatchAsyncFuncTResultFuncExceptionCancellationTokenTaskTResultFromSuccessShouldProduceExpectedResultWithValueTaskDelegates()
    {
        // Given
        ValueResult result = ValueResult.Success();
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

    [Fact(DisplayName = "ValueResult.MatchAsync(Func<TResult>, Func<Exception, CancellationToken, ValueTask<TResult>>) from success should produce the expected result when the delegates complete asynchronously.")]
    public async Task ValueResultMatchAsyncFuncTResultFuncExceptionCancellationTokenTaskTResultFromSuccessShouldProduceExpectedResultWithValueTaskDelegatesWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult result = ValueResult.Success();
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

    [Fact(DisplayName = "ValueResult.MatchAsync(Func<TResult>, Func<Exception, CancellationToken, Task<TResult>>) from failure should produce the expected result.")]
    public async Task ValueResultMatchAsyncFuncTResultFuncExceptionCancellationTokenTaskTResultFromFailureShouldProduceExpectedResult()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
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

    [Fact(DisplayName = "ValueResult.MatchAsync(Func<TResult>, Func<Exception, CancellationToken, Task<TResult>>) from failure should produce the expected result when the delegates complete asynchronously.")]
    public async Task ValueResultMatchAsyncFuncTResultFuncExceptionCancellationTokenTaskTResultFromFailureShouldProduceExpectedResultWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
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

    [Fact(DisplayName = "ValueResult.MatchAsync(Func<TResult>, Func<Exception, CancellationToken, ValueTask<TResult>>) from failure should produce the expected result.")]
    public async Task ValueResultMatchAsyncFuncTResultFuncExceptionCancellationTokenTaskTResultFromFailureShouldProduceExpectedResultWithValueTaskDelegates()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
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

    [Fact(DisplayName = "ValueResult.MatchAsync(Func<TResult>, Func<Exception, CancellationToken, ValueTask<TResult>>) from failure should produce the expected result when the delegates complete asynchronously.")]
    public async Task ValueResultMatchAsyncFuncTResultFuncExceptionCancellationTokenTaskTResultFromFailureShouldProduceExpectedResultWithValueTaskDelegatesWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
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

    [Fact(DisplayName = "ValueResult.MatchAsync(Func<Task<TResult>>, Func<Exception, Task<TResult>>) from success should produce the expected result.")]
    public async Task ValueResultMatchAsyncFuncTaskTResultFuncExceptionTaskTResultFromSuccessShouldProduceExpectedResult()
    {
        // Given
        ValueResult result = ValueResult.Success();
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

    [Fact(DisplayName = "ValueResult.MatchAsync(Func<Task<TResult>>, Func<Exception, Task<TResult>>) from success should produce the expected result when the delegates complete asynchronously.")]
    public async Task ValueResultMatchAsyncFuncTaskTResultFuncExceptionTaskTResultFromSuccessShouldProduceExpectedResultWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult result = ValueResult.Success();
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

    [Fact(DisplayName = "ValueResult.MatchAsync(Func<ValueTask<TResult>>, Func<Exception, ValueTask<TResult>>) from success should produce the expected result.")]
    public async Task ValueResultMatchAsyncFuncTaskTResultFuncExceptionTaskTResultFromSuccessShouldProduceExpectedResultWithValueTaskDelegates()
    {
        // Given
        ValueResult result = ValueResult.Success();
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

    [Fact(DisplayName = "ValueResult.MatchAsync(Func<ValueTask<TResult>>, Func<Exception, ValueTask<TResult>>) from success should produce the expected result when the delegates complete asynchronously.")]
    public async Task ValueResultMatchAsyncFuncTaskTResultFuncExceptionTaskTResultFromSuccessShouldProduceExpectedResultWithValueTaskDelegatesWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult result = ValueResult.Success();
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

    [Fact(DisplayName = "ValueResult.MatchAsync(Func<Task<TResult>>, Func<Exception, Task<TResult>>) from failure should produce the expected result.")]
    public async Task ValueResultMatchAsyncFuncTaskTResultFuncExceptionTaskTResultFromFailureShouldProduceExpectedResult()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
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

    [Fact(DisplayName = "ValueResult.MatchAsync(Func<Task<TResult>>, Func<Exception, Task<TResult>>) from failure should produce the expected result when the delegates complete asynchronously.")]
    public async Task ValueResultMatchAsyncFuncTaskTResultFuncExceptionTaskTResultFromFailureShouldProduceExpectedResultWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
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

    [Fact(DisplayName = "ValueResult.MatchAsync(Func<ValueTask<TResult>>, Func<Exception, ValueTask<TResult>>) from failure should produce the expected result.")]
    public async Task ValueResultMatchAsyncFuncTaskTResultFuncExceptionTaskTResultFromFailureShouldProduceExpectedResultWithValueTaskDelegates()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
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

    [Fact(DisplayName = "ValueResult.MatchAsync(Func<ValueTask<TResult>>, Func<Exception, ValueTask<TResult>>) from failure should produce the expected result when the delegates complete asynchronously.")]
    public async Task ValueResultMatchAsyncFuncTaskTResultFuncExceptionTaskTResultFromFailureShouldProduceExpectedResultWithValueTaskDelegatesWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
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

    [Fact(DisplayName = "ValueResult.MatchAsync(Func<CancellationToken, Task<TResult>>, Func<Exception, CancellationToken, Task<TResult>>) from success should produce the expected result.")]
    public async Task ValueResultMatchAsyncFuncCancellationTokenTaskTResultFuncExceptionCancellationTokenTaskTResultFromSuccessShouldProduceExpectedResult()
    {
        // Given
        ValueResult result = ValueResult.Success();
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

    [Fact(DisplayName = "ValueResult.MatchAsync(Func<CancellationToken, Task<TResult>>, Func<Exception, CancellationToken, Task<TResult>>) from success should produce the expected result when the delegates complete asynchronously.")]
    public async Task ValueResultMatchAsyncFuncCancellationTokenTaskTResultFuncExceptionCancellationTokenTaskTResultFromSuccessShouldProduceExpectedResultWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult result = ValueResult.Success();
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

    [Fact(DisplayName = "ValueResult.MatchAsync(Func<CancellationToken, ValueTask<TResult>>, Func<Exception, CancellationToken, ValueTask<TResult>>) from success should produce the expected result.")]
    public async Task ValueResultMatchAsyncFuncCancellationTokenTaskTResultFuncExceptionCancellationTokenTaskTResultFromSuccessShouldProduceExpectedResultWithValueTaskDelegates()
    {
        // Given
        ValueResult result = ValueResult.Success();
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

    [Fact(DisplayName = "ValueResult.MatchAsync(Func<CancellationToken, ValueTask<TResult>>, Func<Exception, CancellationToken, ValueTask<TResult>>) from success should produce the expected result when the delegates complete asynchronously.")]
    public async Task ValueResultMatchAsyncFuncCancellationTokenTaskTResultFuncExceptionCancellationTokenTaskTResultFromSuccessShouldProduceExpectedResultWithValueTaskDelegatesWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult result = ValueResult.Success();
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

    [Fact(DisplayName = "ValueResult.MatchAsync(Func<CancellationToken, Task<TResult>>, Func<Exception, CancellationToken, Task<TResult>>) from failure should produce the expected result.")]
    public async Task ValueResultMatchAsyncFuncCancellationTokenTaskTResultFuncExceptionCancellationTokenTaskTResultFromFailureShouldProduceExpectedResult()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
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

    [Fact(DisplayName = "ValueResult.MatchAsync(Func<CancellationToken, Task<TResult>>, Func<Exception, CancellationToken, Task<TResult>>) from failure should produce the expected result when the delegates complete asynchronously.")]
    public async Task ValueResultMatchAsyncFuncCancellationTokenTaskTResultFuncExceptionCancellationTokenTaskTResultFromFailureShouldProduceExpectedResultWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
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

    [Fact(DisplayName = "ValueResult.MatchAsync(Func<CancellationToken, ValueTask<TResult>>, Func<Exception, CancellationToken, ValueTask<TResult>>) from failure should produce the expected result.")]
    public async Task ValueResultMatchAsyncFuncCancellationTokenTaskTResultFuncExceptionCancellationTokenTaskTResultFromFailureShouldProduceExpectedResultWithValueTaskDelegates()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
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

    [Fact(DisplayName = "ValueResult.MatchAsync(Func<CancellationToken, ValueTask<TResult>>, Func<Exception, CancellationToken, ValueTask<TResult>>) from failure should produce the expected result when the delegates complete asynchronously.")]
    public async Task ValueResultMatchAsyncFuncCancellationTokenTaskTResultFuncExceptionCancellationTokenTaskTResultFromFailureShouldProduceExpectedResultWithValueTaskDelegatesWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
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

    [Fact(DisplayName = "ValueResult switch expression should match the exception of a failure and null for a success")]
    public void ValueResultSwitchExpressionShouldMatchTheExceptionOfFailureAndNullForSuccess()
    {
        // Given
        ValueResult success = ValueResult.Success();
        ValueResult failure = ValueResult.Failure(FailureException);

        // When
        string successText = Describe(success);
        string failureText = Describe(failure);

        // Then
        Assert.Equal("Success", successText);
        Assert.Equal("Failure", failureText);

        // No discard arm: removing the null arm reports CS8655 (null not handled).
        static string Describe(ValueResult result) => result switch
        {
            Exception exception => exception.Message,
            null => "Success"
        };
    }

    [Fact(DisplayName = "ValueResult type patterns should test the underlying exception")]
    public void ValueResultTypePatternsShouldTestTheUnderlyingException()
    {
        // Given
        ValueResult success = ValueResult.Success();
        ValueResult failure = ValueResult.Failure(new InvalidOperationException("Failure"));

        // When / Then
        Assert.True(failure is InvalidOperationException { Message: "Failure" });
        Assert.False(failure is ArgumentException);
        Assert.True(failure is not null);
        Assert.False(success is Exception);
        Assert.True(success is null);
    }

    [Fact(DisplayName = "ValueResult.Select should produce the expected result")]
    public void ValueResultSelectShouldProduceExpectedResult()
    {
        // Given
        ValueResult success = ValueResult.Success();
        ValueResult failure = ValueResult.Failure(FailureException);
        int invocations = 0;

        // When
        ValueResult successThenSuccess = success.Select(() => { invocations++; });
        ValueResult successThenFailure = success.Select(() => throw FailureException);
        ValueResult failureThenSuccess = failure.Select(() => { invocations++; });

        // Then
        Assert.True(successThenSuccess.IsSuccess);
        Assert.Same(FailureException, successThenFailure.GetExceptionOrDefault());
        Assert.Same(FailureException, failureThenSuccess.GetExceptionOrDefault());
        Assert.Equal(1, invocations);
    }

    [Fact(DisplayName = "ValueResult.SelectAsync(Func<Task>) from failure should produce the expected result.")]
    public async Task ValueResultSelectAsyncFuncTaskFromFailureShouldProduceExpectedResult()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
        ValueResult expected = ValueResult.Failure(FailureException);

        // When
        ValueResult actual = await result.SelectAsync(() => Task.CompletedTask, TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult.SelectAsync(Func<Task>) from failure should produce the expected result when the delegates complete asynchronously.")]
    public async Task ValueResultSelectAsyncFuncTaskFromFailureShouldProduceExpectedResultWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
        ValueResult expected = ValueResult.Failure(FailureException);

        // When
        ValueResult actual = await result.SelectAsync(() => Yielding.CompletedTask(), TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult.SelectAsync(Func<ValueTask>) from failure should produce the expected result.")]
    public async Task ValueResultSelectAsyncFuncTaskFromFailureShouldProduceExpectedResultWithValueTaskDelegates()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
        ValueResult expected = ValueResult.Failure(FailureException);

        // When
        ValueResult actual = await result.SelectAsync(() => ValueTask.CompletedTask, TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult.SelectAsync(Func<ValueTask>) from failure should produce the expected result when the delegates complete asynchronously.")]
    public async Task ValueResultSelectAsyncFuncTaskFromFailureShouldProduceExpectedResultWithValueTaskDelegatesWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
        ValueResult expected = ValueResult.Failure(FailureException);

        // When
        ValueResult actual = await result.SelectAsync(() => Yielding.CompletedValueTask(), TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult.SelectAsync(Func<Task>) from success should produce the expected result.")]
    public async Task ValueResultSelectAsyncFuncTaskFromSuccessShouldProduceExpectedResult()
    {
        // Given
        ValueResult result = ValueResult.Success();
        ValueResult expected = ValueResult.Success();

        // When
        ValueResult actual = await result.SelectAsync(() => Task.CompletedTask, TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult.SelectAsync(Func<Task>) from success should produce the expected result when the delegates complete asynchronously.")]
    public async Task ValueResultSelectAsyncFuncTaskFromSuccessShouldProduceExpectedResultWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult result = ValueResult.Success();
        ValueResult expected = ValueResult.Success();

        // When
        ValueResult actual = await result.SelectAsync(() => Yielding.CompletedTask(), TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult.SelectAsync(Func<ValueTask>) from success should produce the expected result.")]
    public async Task ValueResultSelectAsyncFuncTaskFromSuccessShouldProduceExpectedResultWithValueTaskDelegates()
    {
        // Given
        ValueResult result = ValueResult.Success();
        ValueResult expected = ValueResult.Success();

        // When
        ValueResult actual = await result.SelectAsync(() => ValueTask.CompletedTask, TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult.SelectAsync(Func<ValueTask>) from success should produce the expected result when the delegates complete asynchronously.")]
    public async Task ValueResultSelectAsyncFuncTaskFromSuccessShouldProduceExpectedResultWithValueTaskDelegatesWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult result = ValueResult.Success();
        ValueResult expected = ValueResult.Success();

        // When
        ValueResult actual = await result.SelectAsync(() => Yielding.CompletedValueTask(), TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult.SelectAsync(Func<Task>) from success with exception should produce the expected result.")]
    public async Task ValueResultSelectAsyncFuncTaskFromSuccessWithExceptionShouldProduceExpectedResult()
    {
        // Given
        ValueResult result = ValueResult.Success();
        ValueResult expected = ValueResult.Failure(FailureException);

        // When
        ValueResult actual = await result.SelectAsync(Task () => throw FailureException, TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult.SelectAsync(Func<ValueTask>) from success with exception should produce the expected result.")]
    public async Task ValueResultSelectAsyncFuncTaskFromSuccessWithExceptionShouldProduceExpectedResultWithValueTaskDelegates()
    {
        // Given
        ValueResult result = ValueResult.Success();
        ValueResult expected = ValueResult.Failure(FailureException);

        // When
        ValueResult actual = await result.SelectAsync(ValueTask () => throw FailureException, TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult.SelectAsync(Func<CancellationToken, Task>) from failure should produce the expected result.")]
    public async Task ValueResultSelectAsyncFuncCancellationTokenTaskFromFailureShouldProduceExpectedResult()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
        ValueResult expected = ValueResult.Failure(FailureException);
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        ValueResult actual = await result.SelectAsync(_ => Task.CompletedTask, cancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult.SelectAsync(Func<CancellationToken, Task>) from failure should produce the expected result when the delegates complete asynchronously.")]
    public async Task ValueResultSelectAsyncFuncCancellationTokenTaskFromFailureShouldProduceExpectedResultWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
        ValueResult expected = ValueResult.Failure(FailureException);
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        ValueResult actual = await result.SelectAsync(_ => Yielding.CompletedTask(), cancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult.SelectAsync(Func<CancellationToken, ValueTask>) from failure should produce the expected result.")]
    public async Task ValueResultSelectAsyncFuncCancellationTokenTaskFromFailureShouldProduceExpectedResultWithValueTaskDelegates()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
        ValueResult expected = ValueResult.Failure(FailureException);
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        ValueResult actual = await result.SelectAsync(_ => ValueTask.CompletedTask, cancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult.SelectAsync(Func<CancellationToken, ValueTask>) from failure should produce the expected result when the delegates complete asynchronously.")]
    public async Task ValueResultSelectAsyncFuncCancellationTokenTaskFromFailureShouldProduceExpectedResultWithValueTaskDelegatesWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
        ValueResult expected = ValueResult.Failure(FailureException);
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        ValueResult actual = await result.SelectAsync(_ => Yielding.CompletedValueTask(), cancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult.SelectAsync(Func<CancellationToken, Task>) from success should produce the expected result.")]
    public async Task ValueResultSelectAsyncFuncCancellationTokenTaskFromSuccessShouldProduceExpectedResult()
    {
        // Given
        ValueResult result = ValueResult.Success();
        ValueResult expected = ValueResult.Success();
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        ValueResult actual = await result.SelectAsync(_ => Task.CompletedTask, cancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult.SelectAsync(Func<CancellationToken, Task>) from success should produce the expected result when the delegates complete asynchronously.")]
    public async Task ValueResultSelectAsyncFuncCancellationTokenTaskFromSuccessShouldProduceExpectedResultWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult result = ValueResult.Success();
        ValueResult expected = ValueResult.Success();
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        ValueResult actual = await result.SelectAsync(_ => Yielding.CompletedTask(), cancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult.SelectAsync(Func<CancellationToken, ValueTask>) from success should produce the expected result.")]
    public async Task ValueResultSelectAsyncFuncCancellationTokenTaskFromSuccessShouldProduceExpectedResultWithValueTaskDelegates()
    {
        // Given
        ValueResult result = ValueResult.Success();
        ValueResult expected = ValueResult.Success();
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        ValueResult actual = await result.SelectAsync(_ => ValueTask.CompletedTask, cancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult.SelectAsync(Func<CancellationToken, ValueTask>) from success should produce the expected result when the delegates complete asynchronously.")]
    public async Task ValueResultSelectAsyncFuncCancellationTokenTaskFromSuccessShouldProduceExpectedResultWithValueTaskDelegatesWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult result = ValueResult.Success();
        ValueResult expected = ValueResult.Success();
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        ValueResult actual = await result.SelectAsync(_ => Yielding.CompletedValueTask(), cancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult.SelectAsync(Func<CancellationToken, Task>) from success with exception should produce the expected result.")]
    public async Task ValueResultSelectAsyncFuncCancellationTokenTaskFromSuccessWithExceptionShouldProduceExpectedResult()
    {
        // Given
        ValueResult result = ValueResult.Success();
        ValueResult expected = ValueResult.Failure(FailureException);
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        ValueResult actual = await result.SelectAsync(Task (_) => throw FailureException, cancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult.SelectAsync(Func<CancellationToken, ValueTask>) from success with exception should produce the expected result.")]
    public async Task ValueResultSelectAsyncFuncCancellationTokenTaskFromSuccessWithExceptionShouldProduceExpectedResultWithValueTaskDelegates()
    {
        // Given
        ValueResult result = ValueResult.Success();
        ValueResult expected = ValueResult.Failure(FailureException);
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        ValueResult actual = await result.SelectAsync(ValueTask (_) => throw FailureException, cancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult.Select function of TResult from failure should produce the expected result.")]
    public void ValueResultSelectFunctionOfTResultFromFailureShouldProduceExpectedResult()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
        ValueResult<int> expected = ValueResult<int>.Failure(FailureException);

        // Then
        ValueResult<int> actual = result.Select(() => 1);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult.Select function of TResult from success should produce the expected result.")]
    public void ValueResultSelectFunctionOfTResultFromSuccessShouldProduceExpectedResult()
    {
        // Given
        ValueResult result = ValueResult.Success();
        ValueResult<int> expected = 1;

        // Then
        ValueResult<int> actual = result.Select(() => 1);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult.Select function of TResult from success with exception should produce the expected result.")]
    public void ValueResultSelectFunctionOfTResultFromSuccessWithExceptionShouldProduceExpectedResult()
    {
        // Given
        ValueResult result = ValueResult.Success();
        ValueResult<int> expected = ValueResult<int>.Failure(FailureException);

        // Then
        ValueResult<int> actual = result.Select<int>(() => throw FailureException);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult.SelectAsync function of TResult from failure should produce the expected result.")]
    public async Task ValueResultSelectAsyncFunctionOfTResultFromFailureShouldProduceExpectedResult()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
        ValueResult<int> expected = ValueResult<int>.Failure(FailureException);

        // When
        ValueResult<int> actual = await result.SelectAsync(() => Task.FromResult(1), TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult.SelectAsync function of TResult from failure should produce the expected result when the delegates complete asynchronously.")]
    public async Task ValueResultSelectAsyncFunctionOfTResultFromFailureShouldProduceExpectedResultWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
        ValueResult<int> expected = ValueResult<int>.Failure(FailureException);

        // When
        ValueResult<int> actual = await result.SelectAsync(() => Yielding.TaskOf(1), TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult.SelectAsync function of TResult from failure should produce the expected result with ValueTask delegates.")]
    public async Task ValueResultSelectAsyncFunctionOfTResultFromFailureShouldProduceExpectedResultWithValueTaskDelegates()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
        ValueResult<int> expected = ValueResult<int>.Failure(FailureException);

        // When
        ValueResult<int> actual = await result.SelectAsync(() => ValueTask.FromResult(1), TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult.SelectAsync function of TResult from failure should produce the expected result with ValueTask delegates when the delegates complete asynchronously.")]
    public async Task ValueResultSelectAsyncFunctionOfTResultFromFailureShouldProduceExpectedResultWithValueTaskDelegatesWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
        ValueResult<int> expected = ValueResult<int>.Failure(FailureException);

        // When
        ValueResult<int> actual = await result.SelectAsync(() => Yielding.ValueTaskOf(1), TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult.SelectAsync function of TResult from success should produce the expected result.")]
    public async Task ValueResultSelectAsyncFunctionOfTResultFromSuccessShouldProduceExpectedResult()
    {
        // Given
        ValueResult result = ValueResult.Success();
        ValueResult<int> expected = 1;

        // When
        ValueResult<int> actual = await result.SelectAsync(() => Task.FromResult(1), TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult.SelectAsync function of TResult from success should produce the expected result when the delegates complete asynchronously.")]
    public async Task ValueResultSelectAsyncFunctionOfTResultFromSuccessShouldProduceExpectedResultWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult result = ValueResult.Success();
        ValueResult<int> expected = 1;

        // When
        ValueResult<int> actual = await result.SelectAsync(() => Yielding.TaskOf(1), TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult.SelectAsync function of TResult from success should produce the expected result with ValueTask delegates.")]
    public async Task ValueResultSelectAsyncFunctionOfTResultFromSuccessShouldProduceExpectedResultWithValueTaskDelegates()
    {
        // Given
        ValueResult result = ValueResult.Success();
        ValueResult<int> expected = 1;

        // When
        ValueResult<int> actual = await result.SelectAsync(() => ValueTask.FromResult(1), TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult.SelectAsync function of TResult from success should produce the expected result with ValueTask delegates when the delegates complete asynchronously.")]
    public async Task ValueResultSelectAsyncFunctionOfTResultFromSuccessShouldProduceExpectedResultWithValueTaskDelegatesWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult result = ValueResult.Success();
        ValueResult<int> expected = 1;

        // When
        ValueResult<int> actual = await result.SelectAsync(() => Yielding.ValueTaskOf(1), TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult.SelectAsync function of TResult from success with exception should produce the expected result.")]
    public async Task ValueResultSelectAsyncFunctionOfTResultFromSuccessWithExceptionShouldProduceExpectedResult()
    {
        // Given
        ValueResult result = ValueResult.Success();
        ValueResult<int> expected = ValueResult<int>.Failure(FailureException);

        // When
        ValueResult<int> actual = await result.SelectAsync<int>(Task<int> () => throw FailureException, TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult.SelectAsync function of TResult from success with exception should produce the expected result with ValueTask delegates.")]
    public async Task ValueResultSelectAsyncFunctionOfTResultFromSuccessWithExceptionShouldProduceExpectedResultWithValueTaskDelegates()
    {
        // Given
        ValueResult result = ValueResult.Success();
        ValueResult<int> expected = ValueResult<int>.Failure(FailureException);

        // When
        ValueResult<int> actual = await result.SelectAsync<int>(ValueTask<int> () => throw FailureException, TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult.SelectAsync function of TResult with CancellationToken from failure should produce the expected result.")]
    public async Task ValueResultSelectAsyncFunctionOfTResultWithCancellationTokenFromFailureShouldProduceExpectedResult()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
        ValueResult<int> expected = ValueResult<int>.Failure(FailureException);
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        ValueResult<int> actual = await result.SelectAsync(_ => Task.FromResult(1), cancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult.SelectAsync function of TResult with CancellationToken from failure should produce the expected result when the delegates complete asynchronously.")]
    public async Task ValueResultSelectAsyncFunctionOfTResultWithCancellationTokenFromFailureShouldProduceExpectedResultWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
        ValueResult<int> expected = ValueResult<int>.Failure(FailureException);
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        ValueResult<int> actual = await result.SelectAsync(_ => Yielding.TaskOf(1), cancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult.SelectAsync function of TResult with CancellationToken from failure should produce the expected result with ValueTask delegates.")]
    public async Task ValueResultSelectAsyncFunctionOfTResultWithCancellationTokenFromFailureShouldProduceExpectedResultWithValueTaskDelegates()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
        ValueResult<int> expected = ValueResult<int>.Failure(FailureException);
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        ValueResult<int> actual = await result.SelectAsync(_ => ValueTask.FromResult(1), cancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult.SelectAsync function of TResult with CancellationToken from failure should produce the expected result with ValueTask delegates when the delegates complete asynchronously.")]
    public async Task ValueResultSelectAsyncFunctionOfTResultWithCancellationTokenFromFailureShouldProduceExpectedResultWithValueTaskDelegatesWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
        ValueResult<int> expected = ValueResult<int>.Failure(FailureException);
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        ValueResult<int> actual = await result.SelectAsync(_ => Yielding.ValueTaskOf(1), cancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult.SelectAsync function of TResult with CancellationToken from success should produce the expected result.")]
    public async Task ValueResultSelectAsyncFunctionOfTResultWithCancellationTokenFromSuccessShouldProduceExpectedResult()
    {
        // Given
        ValueResult result = ValueResult.Success();
        ValueResult<int> expected = 1;
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        ValueResult<int> actual = await result.SelectAsync(_ => Task.FromResult(1), cancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult.SelectAsync function of TResult with CancellationToken from success should produce the expected result when the delegates complete asynchronously.")]
    public async Task ValueResultSelectAsyncFunctionOfTResultWithCancellationTokenFromSuccessShouldProduceExpectedResultWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult result = ValueResult.Success();
        ValueResult<int> expected = 1;
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        ValueResult<int> actual = await result.SelectAsync(_ => Yielding.TaskOf(1), cancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult.SelectAsync function of TResult with CancellationToken from success should produce the expected result with ValueTask delegates.")]
    public async Task ValueResultSelectAsyncFunctionOfTResultWithCancellationTokenFromSuccessShouldProduceExpectedResultWithValueTaskDelegates()
    {
        // Given
        ValueResult result = ValueResult.Success();
        ValueResult<int> expected = 1;
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        ValueResult<int> actual = await result.SelectAsync(_ => ValueTask.FromResult(1), cancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult.SelectAsync function of TResult with CancellationToken from success should produce the expected result with ValueTask delegates when the delegates complete asynchronously.")]
    public async Task ValueResultSelectAsyncFunctionOfTResultWithCancellationTokenFromSuccessShouldProduceExpectedResultWithValueTaskDelegatesWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult result = ValueResult.Success();
        ValueResult<int> expected = 1;
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        ValueResult<int> actual = await result.SelectAsync(_ => Yielding.ValueTaskOf(1), cancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult.SelectAsync function of TResult with CancellationToken from success with exception should produce the expected result.")]
    public async Task ValueResultSelectAsyncFunctionOfTResultWithCancellationTokenFromSuccessWithExceptionShouldProduceExpectedResult()
    {
        // Given
        ValueResult result = ValueResult.Success();
        ValueResult<int> expected = ValueResult<int>.Failure(FailureException);
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        ValueResult<int> actual = await result.SelectAsync<int>(Task<int> (_) => throw FailureException, cancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult.SelectAsync function of TResult with CancellationToken from success with exception should produce the expected result with ValueTask delegates.")]
    public async Task ValueResultSelectAsyncFunctionOfTResultWithCancellationTokenFromSuccessWithExceptionShouldProduceExpectedResultWithValueTaskDelegates()
    {
        // Given
        ValueResult result = ValueResult.Success();
        ValueResult<int> expected = ValueResult<int>.Failure(FailureException);
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        ValueResult<int> actual = await result.SelectAsync<int>(ValueTask<int> (_) => throw FailureException, cancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult.SelectMany should produce the expected result")]
    public void ValueResultSelectManyShouldProduceExpectedResult()
    {
        // Given
        ValueResult success = ValueResult.Success();
        ValueResult failure = ValueResult.Failure(FailureException);
        Exception otherException = new("Other");

        // When
        ValueResult successThenSuccess = success.SelectMany(ValueResult.Success);
        ValueResult successThenFailure = success.SelectMany(() => otherException);
        ValueResult successThenThrow = success.SelectMany(() => throw otherException);
        ValueResult failureThenSuccess = failure.SelectMany(ValueResult.Success);

        // Then
        Assert.True(successThenSuccess.IsSuccess);
        Assert.Same(otherException, successThenFailure.GetExceptionOrDefault());
        Assert.Same(otherException, successThenThrow.GetExceptionOrDefault());
        Assert.Same(FailureException, failureThenSuccess.GetExceptionOrDefault());
    }

    [Fact(DisplayName = "ValueResult.SelectManyAsync function from failure should produce the expected result.")]
    public async Task ValueResultSelectManyAsyncFunctionFromFailureShouldProduceExpectedResult()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
        ValueResult expected = ValueResult.Failure(FailureException);

        // When
        ValueResult actual = await result.SelectManyAsync(async Task<ValueResult> () => await Task.FromResult(ValueResult.Success()), TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult.SelectManyAsync function from failure should produce the expected result when the delegates complete asynchronously.")]
    public async Task ValueResultSelectManyAsyncFunctionFromFailureShouldProduceExpectedResultWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
        ValueResult expected = ValueResult.Failure(FailureException);

        // When
        ValueResult actual = await result.SelectManyAsync(async Task<ValueResult> () => await Yielding.TaskOf(ValueResult.Success()), TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult.SelectManyAsync function from failure should produce the expected result with ValueTask delegates.")]
    public async Task ValueResultSelectManyAsyncFunctionFromFailureShouldProduceExpectedResultWithValueTaskDelegates()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
        ValueResult expected = ValueResult.Failure(FailureException);

        // When
        ValueResult actual = await result.SelectManyAsync(async ValueTask<ValueResult> () => await ValueTask.FromResult(ValueResult.Success()), TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult.SelectManyAsync function from failure should produce the expected result with ValueTask delegates when the delegates complete asynchronously.")]
    public async Task ValueResultSelectManyAsyncFunctionFromFailureShouldProduceExpectedResultWithValueTaskDelegatesWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
        ValueResult expected = ValueResult.Failure(FailureException);

        // When
        ValueResult actual = await result.SelectManyAsync(async ValueTask<ValueResult> () => await Yielding.ValueTaskOf(ValueResult.Success()), TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult.SelectManyAsync function from success should produce the expected result.")]
    public async Task ValueResultSelectManyAsyncFunctionFromSuccessShouldProduceExpectedResult()
    {
        // Given
        ValueResult result = ValueResult.Success();
        ValueResult expected = ValueResult.Success();

        // When
        ValueResult actual = await result.SelectManyAsync(async Task<ValueResult> () => await Task.FromResult(ValueResult.Success()), TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult.SelectManyAsync function from success should produce the expected result when the delegates complete asynchronously.")]
    public async Task ValueResultSelectManyAsyncFunctionFromSuccessShouldProduceExpectedResultWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult result = ValueResult.Success();
        ValueResult expected = ValueResult.Success();

        // When
        ValueResult actual = await result.SelectManyAsync(async Task<ValueResult> () => await Yielding.TaskOf(ValueResult.Success()), TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult.SelectManyAsync function from success should produce the expected result with ValueTask delegates.")]
    public async Task ValueResultSelectManyAsyncFunctionFromSuccessShouldProduceExpectedResultWithValueTaskDelegates()
    {
        // Given
        ValueResult result = ValueResult.Success();
        ValueResult expected = ValueResult.Success();

        // When
        ValueResult actual = await result.SelectManyAsync(async ValueTask<ValueResult> () => await ValueTask.FromResult(ValueResult.Success()), TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult.SelectManyAsync function from success should produce the expected result with ValueTask delegates when the delegates complete asynchronously.")]
    public async Task ValueResultSelectManyAsyncFunctionFromSuccessShouldProduceExpectedResultWithValueTaskDelegatesWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult result = ValueResult.Success();
        ValueResult expected = ValueResult.Success();

        // When
        ValueResult actual = await result.SelectManyAsync(async ValueTask<ValueResult> () => await Yielding.ValueTaskOf(ValueResult.Success()), TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult.SelectManyAsync function from success with exception should produce the expected result.")]
    public async Task ValueResultSelectManyAsyncFunctionFromSuccessWithExceptionShouldProduceExpectedResult()
    {
        // Given
        ValueResult result = ValueResult.Success();
        ValueResult expected = ValueResult.Failure(FailureException);

        // When
        ValueResult actual = await result.SelectManyAsync(Task<ValueResult> () => throw FailureException, TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult.SelectManyAsync function from success with exception should produce the expected result with ValueTask delegates.")]
    public async Task ValueResultSelectManyAsyncFunctionFromSuccessWithExceptionShouldProduceExpectedResultWithValueTaskDelegates()
    {
        // Given
        ValueResult result = ValueResult.Success();
        ValueResult expected = ValueResult.Failure(FailureException);

        // When
        ValueResult actual = await result.SelectManyAsync(ValueTask<ValueResult> () => throw FailureException, TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult.SelectManyAsync function with CancellationToken from failure should produce the expected result.")]
    public async Task ValueResultSelectManyAsyncFunctionWithCancellationTokenFromFailureShouldProduceExpectedResult()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
        ValueResult expected = ValueResult.Failure(FailureException);
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        ValueResult actual = await result.SelectManyAsync(async Task<ValueResult> (_) => await Task.FromResult(ValueResult.Success()), cancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult.SelectManyAsync function with CancellationToken from failure should produce the expected result when the delegates complete asynchronously.")]
    public async Task ValueResultSelectManyAsyncFunctionWithCancellationTokenFromFailureShouldProduceExpectedResultWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
        ValueResult expected = ValueResult.Failure(FailureException);
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        ValueResult actual = await result.SelectManyAsync(async Task<ValueResult> (_) => await Yielding.TaskOf(ValueResult.Success()), cancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult.SelectManyAsync function with CancellationToken from failure should produce the expected result with ValueTask delegates.")]
    public async Task ValueResultSelectManyAsyncFunctionWithCancellationTokenFromFailureShouldProduceExpectedResultWithValueTaskDelegates()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
        ValueResult expected = ValueResult.Failure(FailureException);
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        ValueResult actual = await result.SelectManyAsync(async ValueTask<ValueResult> (_) => await ValueTask.FromResult(ValueResult.Success()), cancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult.SelectManyAsync function with CancellationToken from failure should produce the expected result with ValueTask delegates when the delegates complete asynchronously.")]
    public async Task ValueResultSelectManyAsyncFunctionWithCancellationTokenFromFailureShouldProduceExpectedResultWithValueTaskDelegatesWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
        ValueResult expected = ValueResult.Failure(FailureException);
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        ValueResult actual = await result.SelectManyAsync(async ValueTask<ValueResult> (_) => await Yielding.ValueTaskOf(ValueResult.Success()), cancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult.SelectManyAsync function with CancellationToken from success should produce the expected result.")]
    public async Task ValueResultSelectManyAsyncFunctionWithCancellationTokenFromSuccessShouldProduceExpectedResult()
    {
        // Given
        ValueResult result = ValueResult.Success();
        ValueResult expected = ValueResult.Success();
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        ValueResult actual = await result.SelectManyAsync(async Task<ValueResult> (_) => await Task.FromResult(ValueResult.Success()), cancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult.SelectManyAsync function with CancellationToken from success should produce the expected result when the delegates complete asynchronously.")]
    public async Task ValueResultSelectManyAsyncFunctionWithCancellationTokenFromSuccessShouldProduceExpectedResultWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult result = ValueResult.Success();
        ValueResult expected = ValueResult.Success();
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        ValueResult actual = await result.SelectManyAsync(async Task<ValueResult> (_) => await Yielding.TaskOf(ValueResult.Success()), cancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult.SelectManyAsync function with CancellationToken from success should produce the expected result with ValueTask delegates.")]
    public async Task ValueResultSelectManyAsyncFunctionWithCancellationTokenFromSuccessShouldProduceExpectedResultWithValueTaskDelegates()
    {
        // Given
        ValueResult result = ValueResult.Success();
        ValueResult expected = ValueResult.Success();
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        ValueResult actual = await result.SelectManyAsync(async ValueTask<ValueResult> (_) => await ValueTask.FromResult(ValueResult.Success()), cancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult.SelectManyAsync function with CancellationToken from success should produce the expected result with ValueTask delegates when the delegates complete asynchronously.")]
    public async Task ValueResultSelectManyAsyncFunctionWithCancellationTokenFromSuccessShouldProduceExpectedResultWithValueTaskDelegatesWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult result = ValueResult.Success();
        ValueResult expected = ValueResult.Success();
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        ValueResult actual = await result.SelectManyAsync(async ValueTask<ValueResult> (_) => await Yielding.ValueTaskOf(ValueResult.Success()), cancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult.SelectManyAsync function with CancellationToken from success with exception should produce the expected result.")]
    public async Task ValueResultSelectManyAsyncFunctionWithCancellationTokenFromSuccessWithExceptionShouldProduceExpectedResult()
    {
        // Given
        ValueResult result = ValueResult.Success();
        ValueResult expected = ValueResult.Failure(FailureException);
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        ValueResult actual = await result.SelectManyAsync(Task<ValueResult> (_) => throw FailureException, cancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult.SelectManyAsync function with CancellationToken from success with exception should produce the expected result with ValueTask delegates.")]
    public async Task ValueResultSelectManyAsyncFunctionWithCancellationTokenFromSuccessWithExceptionShouldProduceExpectedResultWithValueTaskDelegates()
    {
        // Given
        ValueResult result = ValueResult.Success();
        ValueResult expected = ValueResult.Failure(FailureException);
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        ValueResult actual = await result.SelectManyAsync(ValueTask<ValueResult> (_) => throw FailureException, cancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult.SelectMany function of TResult from failure should produce the expected result.")]
    public void ValueResultSelectManyFunctionOfTResultFromFailureShouldProduceExpectedResult()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
        ValueResult<int> expected = ValueResult<int>.Failure(FailureException);

        // When
        ValueResult<int> actual = result.SelectMany<int>(() => 1);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult.SelectMany function of TResult from success should produce the expected result.")]
    public void ValueResultSelectManyFunctionOfTResultFromSuccessShouldProduceExpectedResult()
    {
        // Given
        ValueResult result = ValueResult.Success();
        ValueResult<int> expected = 1;

        // When
        ValueResult<int> actual = result.SelectMany<int>(() => 1);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult.SelectMany function of TResult from success with exception should produce the expected result.")]
    public void ValueResultSelectManyFunctionOfTResultFromSuccessWithExceptionShouldProduceExpectedResult()
    {
        // Given
        ValueResult result = ValueResult.Success();
        ValueResult<int> expected = ValueResult<int>.Failure(FailureException);

        // When
        ValueResult<int> actual = result.SelectMany<int>(() => throw FailureException);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult.SelectManyAsync function of TResult from failure should produce the expected result.")]
    public async Task ValueResultSelectManyAsyncFunctionOfTResultFromFailureShouldProduceExpectedResult()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
        ValueResult<int> expected = ValueResult<int>.Failure(FailureException);

        // When
        ValueResult<int> actual = await result.SelectManyAsync<int>(async Task<ValueResult<int>> () =>
            await Task.FromResult(ValueResult<int>.Success(1)), TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult.SelectManyAsync function of TResult from failure should produce the expected result when the delegates complete asynchronously.")]
    public async Task ValueResultSelectManyAsyncFunctionOfTResultFromFailureShouldProduceExpectedResultWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
        ValueResult<int> expected = ValueResult<int>.Failure(FailureException);

        // When
        ValueResult<int> actual = await result.SelectManyAsync<int>(async Task<ValueResult<int>> () =>
            await Yielding.TaskOf(ValueResult<int>.Success(1)), TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult.SelectManyAsync function of TResult from failure should produce the expected result with ValueTask delegates.")]
    public async Task ValueResultSelectManyAsyncFunctionOfTResultFromFailureShouldProduceExpectedResultWithValueTaskDelegates()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
        ValueResult<int> expected = ValueResult<int>.Failure(FailureException);

        // When
        ValueResult<int> actual = await result.SelectManyAsync<int>(async ValueTask<ValueResult<int>> () =>
            await ValueTask.FromResult(ValueResult<int>.Success(1)), TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult.SelectManyAsync function of TResult from failure should produce the expected result with ValueTask delegates when the delegates complete asynchronously.")]
    public async Task ValueResultSelectManyAsyncFunctionOfTResultFromFailureShouldProduceExpectedResultWithValueTaskDelegatesWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
        ValueResult<int> expected = ValueResult<int>.Failure(FailureException);

        // When
        ValueResult<int> actual = await result.SelectManyAsync<int>(async ValueTask<ValueResult<int>> () =>
            await Yielding.ValueTaskOf(ValueResult<int>.Success(1)), TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult.SelectManyAsync function of TResult from success should produce the expected result.")]
    public async Task ValueResultSelectManyAsyncFunctionOfTResultFromSuccessShouldProduceExpectedResult()
    {
        // Given
        ValueResult result = ValueResult.Success();
        ValueResult<int> expected = ValueResult<int>.Success(1);

        // When
        ValueResult<int> actual = await result.SelectManyAsync<int>(async Task<ValueResult<int>> () =>
            await Task.FromResult(ValueResult<int>.Success(1)), TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult.SelectManyAsync function of TResult from success should produce the expected result when the delegates complete asynchronously.")]
    public async Task ValueResultSelectManyAsyncFunctionOfTResultFromSuccessShouldProduceExpectedResultWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult result = ValueResult.Success();
        ValueResult<int> expected = ValueResult<int>.Success(1);

        // When
        ValueResult<int> actual = await result.SelectManyAsync<int>(async Task<ValueResult<int>> () =>
            await Yielding.TaskOf(ValueResult<int>.Success(1)), TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult.SelectManyAsync function of TResult from success should produce the expected result with ValueTask delegates.")]
    public async Task ValueResultSelectManyAsyncFunctionOfTResultFromSuccessShouldProduceExpectedResultWithValueTaskDelegates()
    {
        // Given
        ValueResult result = ValueResult.Success();
        ValueResult<int> expected = ValueResult<int>.Success(1);

        // When
        ValueResult<int> actual = await result.SelectManyAsync<int>(async ValueTask<ValueResult<int>> () =>
            await ValueTask.FromResult(ValueResult<int>.Success(1)), TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult.SelectManyAsync function of TResult from success should produce the expected result with ValueTask delegates when the delegates complete asynchronously.")]
    public async Task ValueResultSelectManyAsyncFunctionOfTResultFromSuccessShouldProduceExpectedResultWithValueTaskDelegatesWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult result = ValueResult.Success();
        ValueResult<int> expected = ValueResult<int>.Success(1);

        // When
        ValueResult<int> actual = await result.SelectManyAsync<int>(async ValueTask<ValueResult<int>> () =>
            await Yielding.ValueTaskOf(ValueResult<int>.Success(1)), TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult.SelectManyAsync function of TResult from success with exception should produce the expected result.")]
    public async Task ValueResultSelectManyAsyncFunctionOfTResultFromSuccessWithExceptionShouldProduceExpectedResult()
    {
        // Given
        ValueResult result = ValueResult.Success();
        ValueResult<int> expected = ValueResult<int>.Failure(FailureException);

        // When
        ValueResult<int> actual = await result.SelectManyAsync<int>(Task<ValueResult<int>> () => throw FailureException, TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult.SelectManyAsync function of TResult from success with exception should produce the expected result with ValueTask delegates.")]
    public async Task ValueResultSelectManyAsyncFunctionOfTResultFromSuccessWithExceptionShouldProduceExpectedResultWithValueTaskDelegates()
    {
        // Given
        ValueResult result = ValueResult.Success();
        ValueResult<int> expected = ValueResult<int>.Failure(FailureException);

        // When
        ValueResult<int> actual = await result.SelectManyAsync<int>(ValueTask<ValueResult<int>> () => throw FailureException, TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult.SelectManyAsync function of TResult with CancellationToken from failure should produce the expected result.")]
    public async Task ValueResultSelectManyAsyncFunctionOfTResultWithCancellationTokenFromFailureShouldProduceExpectedResult()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
        ValueResult<int> expected = ValueResult<int>.Failure(FailureException);
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        ValueResult<int> actual = await result.SelectManyAsync<int>(async Task<ValueResult<int>> (_) => await Task.FromResult(ValueResult<int>.Success(1)), cancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult.SelectManyAsync function of TResult with CancellationToken from failure should produce the expected result when the delegates complete asynchronously.")]
    public async Task ValueResultSelectManyAsyncFunctionOfTResultWithCancellationTokenFromFailureShouldProduceExpectedResultWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
        ValueResult<int> expected = ValueResult<int>.Failure(FailureException);
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        ValueResult<int> actual = await result.SelectManyAsync<int>(async Task<ValueResult<int>> (_) => await Yielding.TaskOf(ValueResult<int>.Success(1)), cancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult.SelectManyAsync function of TResult with CancellationToken from failure should produce the expected result with ValueTask delegates.")]
    public async Task ValueResultSelectManyAsyncFunctionOfTResultWithCancellationTokenFromFailureShouldProduceExpectedResultWithValueTaskDelegates()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
        ValueResult<int> expected = ValueResult<int>.Failure(FailureException);
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        ValueResult<int> actual = await result.SelectManyAsync<int>(async ValueTask<ValueResult<int>> (_) => await ValueTask.FromResult(ValueResult<int>.Success(1)), cancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult.SelectManyAsync function of TResult with CancellationToken from failure should produce the expected result with ValueTask delegates when the delegates complete asynchronously.")]
    public async Task ValueResultSelectManyAsyncFunctionOfTResultWithCancellationTokenFromFailureShouldProduceExpectedResultWithValueTaskDelegatesWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
        ValueResult<int> expected = ValueResult<int>.Failure(FailureException);
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        ValueResult<int> actual = await result.SelectManyAsync<int>(async ValueTask<ValueResult<int>> (_) => await Yielding.ValueTaskOf(ValueResult<int>.Success(1)), cancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult.SelectManyAsync function of TResult with CancellationToken from success should produce the expected result.")]
    public async Task ValueResultSelectManyAsyncFunctionOfTResultWithCancellationTokenFromSuccessShouldProduceExpectedResult()
    {
        // Given
        ValueResult result = ValueResult.Success();
        ValueResult<int> expected = ValueResult<int>.Success(1);
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        ValueResult<int> actual = await result.SelectManyAsync<int>(async Task<ValueResult<int>> (_) => await Task.FromResult(ValueResult<int>.Success(1)), cancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult.SelectManyAsync function of TResult with CancellationToken from success should produce the expected result when the delegates complete asynchronously.")]
    public async Task ValueResultSelectManyAsyncFunctionOfTResultWithCancellationTokenFromSuccessShouldProduceExpectedResultWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult result = ValueResult.Success();
        ValueResult<int> expected = ValueResult<int>.Success(1);
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        ValueResult<int> actual = await result.SelectManyAsync<int>(async Task<ValueResult<int>> (_) => await Yielding.TaskOf(ValueResult<int>.Success(1)), cancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult.SelectManyAsync function of TResult with CancellationToken from success should produce the expected result with ValueTask delegates.")]
    public async Task ValueResultSelectManyAsyncFunctionOfTResultWithCancellationTokenFromSuccessShouldProduceExpectedResultWithValueTaskDelegates()
    {
        // Given
        ValueResult result = ValueResult.Success();
        ValueResult<int> expected = ValueResult<int>.Success(1);
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        ValueResult<int> actual = await result.SelectManyAsync<int>(async ValueTask<ValueResult<int>> (_) => await ValueTask.FromResult(ValueResult<int>.Success(1)), cancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult.SelectManyAsync function of TResult with CancellationToken from success should produce the expected result with ValueTask delegates when the delegates complete asynchronously.")]
    public async Task ValueResultSelectManyAsyncFunctionOfTResultWithCancellationTokenFromSuccessShouldProduceExpectedResultWithValueTaskDelegatesWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueResult result = ValueResult.Success();
        ValueResult<int> expected = ValueResult<int>.Success(1);
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        ValueResult<int> actual = await result.SelectManyAsync<int>(async ValueTask<ValueResult<int>> (_) => await Yielding.ValueTaskOf(ValueResult<int>.Success(1)), cancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult.SelectManyAsync function of TResult with CancellationToken from success with exception should produce the expected result.")]
    public async Task ValueResultSelectManyAsyncFunctionOfTResultWithCancellationTokenFromSuccessWithExceptionShouldProduceExpectedResult()
    {
        // Given
        ValueResult result = ValueResult.Success();
        ValueResult<int> expected = ValueResult<int>.Failure(FailureException);
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        ValueResult<int> actual = await result.SelectManyAsync<int>(Task<ValueResult<int>> (_) => throw FailureException, cancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult.SelectManyAsync function of TResult with CancellationToken from success with exception should produce the expected result with ValueTask delegates.")]
    public async Task ValueResultSelectManyAsyncFunctionOfTResultWithCancellationTokenFromSuccessWithExceptionShouldProduceExpectedResultWithValueTaskDelegates()
    {
        // Given
        ValueResult result = ValueResult.Success();
        ValueResult<int> expected = ValueResult<int>.Failure(FailureException);
        CancellationToken cancellationToken = CancellationToken.None;

        // When
        ValueResult<int> actual = await result.SelectManyAsync<int>(ValueTask<ValueResult<int>> (_) => throw FailureException, cancellationToken);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueResult.Throw should throw the underlying exception of a failure only")]
    public void ValueResultThrowShouldThrowTheUnderlyingExceptionOfFailureOnly()
    {
        // Given
        ValueResult success = ValueResult.Success();
        ValueResult failure = ValueResult.Failure(FailureException);

        // When
        success.Throw();
        Exception exception = Assert.Throws<Exception>(failure.Throw);

        // Then
        Assert.Same(FailureException, exception);
    }

    [Fact(DisplayName = "ValueResult.ToResult and ValueResult.From should round-trip through Result")]
    public void ValueResultToResultAndFromShouldRoundTripThroughResult()
    {
        // Given
        ValueResult success = ValueResult.Success();
        ValueResult failure = ValueResult.Failure(FailureException);

        // When
        Result successResult = success.ToResult();
        Result failureResult = failure.ToResult();

        // Then
        Assert.IsType<Success>(successResult);
        Assert.Same(FailureException, Assert.IsType<Failure>(failureResult).Exception);
        Assert.Equal(success, ValueResult.From(successResult));
        Assert.Equal(failure, ValueResult.From(failureResult));
    }

    [Fact(DisplayName = "ValueResult.ToString should produce the expected result")]
    public void ValueResultToStringShouldProduceExpectedResult()
    {
        // Given
        ValueResult success = ValueResult.Success();
        ValueResult failure = ValueResult.Failure(FailureException);

        // When / Then
        Assert.Equal(string.Empty, success.ToString());
        Assert.Equal("Failure", failure.ToString());
    }
}
