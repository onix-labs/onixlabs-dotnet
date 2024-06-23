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
    [Fact(DisplayName = "Result.Of should produce expected success result")]
    public void ResultOfShouldProduceExpectedSuccessResult()
    {
        // Given / When
        Result result = Result.Of(() => { });

        // Then
        Assert.True(result.IsSuccess);
        Assert.False(result.IsFailure);
        Assert.IsType<Success>(result);
    }

    [Fact(DisplayName = "Result.Of should produce expected failure result")]
    public void ResultOfShouldProduceExpectedFailureResult()
    {
        // Given / When
        Exception exception = new("failure");
        Result result = Result.Of(() => throw exception);

        // Then
        Assert.False(result.IsSuccess);
        Assert.True(result.IsFailure);
        Assert.IsType<Failure>(result);
    }

    [Fact(DisplayName = "Result.OfAsync should produce expected success result")]
    public async Task ResultOfAsyncShouldProduceExpectedSuccessResult()
    {
        // Given / When
        Result result = await Result.OfAsync(async () => await Task.CompletedTask);

        // Then
        Assert.True(result.IsSuccess);
        Assert.False(result.IsFailure);
        Assert.IsType<Success>(result);
    }

    [Fact(DisplayName = "Result.OfAsync should produce expected failure result")]
    public async Task ResultOfAsyncShouldProduceExpectedFailureResult()
    {
        // Given / When
        Exception exception = new("failure");
        Result result = await Result.OfAsync(() => throw exception);

        // Then
        Assert.False(result.IsSuccess);
        Assert.True(result.IsFailure);
        Assert.IsType<Failure>(result);
    }

    [Fact(DisplayName = "Result.OfAsync with cancellation token should produce expected success result")]
    public async Task ResultOfAsyncWithCancellationTokenShouldProduceExpectedSuccessResult()
    {
        // Given / When
        CancellationToken token = CancellationToken.None;
        Result result = await Result.OfAsync(async _ => await Task.CompletedTask, token);

        // Then
        Assert.True(result.IsSuccess);
        Assert.False(result.IsFailure);
        Assert.IsType<Success>(result);
    }

    [Fact(DisplayName = "Result.OfAsync with cancellation token should produce expected failure result")]
    public async Task ResultOfAsyncWithCancellationTokenShouldProduceExpectedFailureResult()
    {
        // Given / When
        Exception exception = new("failure");
        CancellationToken token = CancellationToken.None;
        Result result = await Result.OfAsync(_ => throw exception, token);

        // Then
        Assert.False(result.IsSuccess);
        Assert.True(result.IsFailure);
        Assert.IsType<Failure>(result);
    }

    [Fact(DisplayName = "Result.Success should produce the expected result")]
    public void ResultSuccessShouldProduceExpectedResult()
    {
        // Given / When
        Result result = Result.Success();

        // Then
        Assert.True(result.IsSuccess);
        Assert.False(result.IsFailure);
        Assert.IsType<Success>(result);
    }

    [Fact(DisplayName = "Result.Failure should produce the expected result")]
    public void ResultFailureShouldProduceExpectedResult()
    {
        // Given / When
        Exception exception = new("failure");
        Result result = Result.Failure(exception);

        // Then
        Assert.False(result.IsSuccess);
        Assert.True(result.IsFailure);
        Assert.IsType<Failure>(result);
    }

    [Fact(DisplayName = "Result implicit operator should produce the expected failure result.")]
    public void ResultImplicitOperatorShouldProduceTheExpectedFailureResult()
    {
        // Given / When
        Exception exception = new("failure");
        Result result = exception;

        // Then
        Assert.False(result.IsSuccess);
        Assert.True(result.IsFailure);
        Assert.IsType<Failure>(result);
        Assert.Equal("failure", (result as Failure)!.Exception.Message);
    }

    [Fact(DisplayName = "Result Success values should be considered equal.")]
    public void ResultSuccessValuesShouldBeConsideredEqual()
    {
        // Given
        Success a = Result.Success();
        Success b = Success.Instance;

        // When / Then
        Assert.Equal(a, b);
        Assert.True(a.Equals(b));
        Assert.True(a == b);
        Assert.False(a != b);
        Assert.True((Result)a == (Result)b);
        Assert.False((Result)a != (Result)b);
    }

    [Fact(DisplayName = "Result Failure values should be considered equal.")]
    public void ResultFailureValuesShouldBeConsideredEqual()
    {
        // Given
        Exception exception = new("failure");
        Failure a = Result.Failure(exception);
        Failure b = exception;

        // When / Then
        Assert.Equal(a, b);
        Assert.True(a.Equals(b));
        Assert.True(a == b);
        Assert.False(a != b);
        Assert.True((Result)a == (Result)b);
        Assert.False((Result)a != (Result)b);

        // Note that a and b are equal because they share references to the same exception.
    }

    [Fact(DisplayName = "Result Failure values should not be considered equal.")]
    public void ResultFailureValuesShouldNotBeConsideredEqual()
    {
        // Given
        Exception exception1 = new("failure a");
        Exception exception2 = new("failure b");
        Result a = Result.Failure(exception1);
        Result b = Result.Failure(exception2);

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
        Result a = Result.Success();
        Result b = Result.Failure(exception);

        // When / Then
        Assert.NotEqual(a, b);
        Assert.True(a != b);
        Assert.False(a.Equals(b));
    }

    [Fact(DisplayName = "Result Success.GetHashCode should produce the expected result.")]
    public void ResultSuccessGetHashCodeShouldProduceExpectedResult()
    {
        // Given
        const int expected = 0;
        Result result = Result.Success();

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
        Result result = Result.Failure(exception);

        // When
        int actual = result.GetHashCode();

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result Success.Match should execute the some action.")]
    public void ResultSuccessMatchShouldExecuteSuccessAction()
    {
        // Given
        bool successCalled = false;
        Result result = Result.Success();

        // When
        result.Match(success: () => { successCalled = true; });

        // Then
        Assert.True(successCalled);
    }

    [Fact(DisplayName = "Result Failure.Match should execute the none action.")]
    public void ResultFailureMatchShouldExecuteFailureAction()
    {
        // Given
        bool failureCalled = false;
        Exception exception = new("failure");
        Result result = Result.Failure(exception);

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
        Result result = Result.Success();

        // When
        int actual = result.Match(
            success: () => 9,
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
        Result result = Result.Failure(exception);

        // When
        int actual = result.Match(
            success: () => 9,
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

        // When
        Result actual = expected.Select(() => { });

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result Failure.Select should produce the expected result")]
    public void ResultFailureSelectShouldProduceExpectedResult()
    {
        // Given
        Result expected = Result.Failure(new Exception("Failure"));

        // When
        Result actual = expected.Select(() => { });

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result Success.Select<TResult> should produce the expected result")]
    public void ResultSuccessSelectTResultShouldProduceExpectedResult()
    {
        // Given
        const int expected = 9;
        Result result = Result.Success();

        // When
        Result<int> actual = result.Select(() => 9);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result Failure.Select<TResult> should produce the expected result")]
    public void ResultFailureSelectTResultShouldProduceExpectedResult()
    {
        // Given
        Exception exception = new("failure");
        Result result = Result.Failure(exception);

        // When
        Result<int> actual = result.Select(() => 9);

        // Then
        Assert.Equal(Result<int>.Failure(exception), actual);
    }

    [Fact(DisplayName = "Result Success.SelectMany should produce the expected result")]
    public void ResultSuccessSelectManyShouldProduceExpectedResult()
    {
        // Given
        Result expected = Result.Success();

        // When
        Result actual = expected.SelectMany(Result.Success);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result Failure.SelectMany should produce the expected result")]
    public void ResultFailureSelectManyShouldProduceExpectedResult()
    {
        // Given
        Result expected = Result.Failure(new Exception("Failure"));

        // When
        Result actual = expected.SelectMany(Result.Success);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result Success.SelectMany<TResult> should produce the expected result")]
    public void ResultSuccessSelectManyTResultShouldProduceExpectedResult()
    {
        // Given
        const int expected = 9;
        Result result = Result.Success();

        // When
        Result<int> actual = result.SelectMany<int>(() => 9);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result Failure.SelectMany<TResult> should produce the expected result")]
    public void ResultFailureSelectManyTResultShouldProduceExpectedResult()
    {
        // Given
        Exception exception = new("failure");
        Result result = Result.Failure(exception);

        // When
        Result<int> actual = result.SelectMany<int>(() => 9);

        // Then
        Assert.Equal(Result<int>.Failure(exception), actual);
    }

    [Fact(DisplayName = "Result Success.Throw should do nothing")]
    public void ResultSuccessThrowShouldDoNothing()
    {
        // Given
        Result result = Result.Success();

        // When / Then
        result.Throw();
    }

    [Fact(DisplayName = "Result Failure.Throw should throw Exception")]
    public void ResultFailureThrowShouldThrowException()
    {
        // Given
        Exception exception = new("failure");
        Result result = Result.Failure(exception);

        // When / Then
        Assert.Throws<Exception>(() => result.Throw());
    }

    [Fact(DisplayName = "Result Success.ToString should produce the expected result.")]
    public void ResultSuccessToStringShouldProduceExpectedResult()
    {
        // Given
        Result result = Result.Success();

        // When
        string resultString = result.ToString();

        // Then
        Assert.Equal(string.Empty, resultString);
    }

    [Fact(DisplayName = "Result Failure.ToString should produce the expected result.")]
    public void ResultFailureToStringShouldProduceExpectedResult()
    {
        // Given
        Exception exception = new("failure");
        Result result = Result.Failure(exception);

        // When
        string resultString = result.ToString();

        // Then
        Assert.Equal("System.Exception: failure", resultString);
    }
}
