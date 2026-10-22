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

public sealed class ResultValueTaskExtensionTests
{
    private static readonly Exception FailureException = new("Failure");

    [Fact(DisplayName = "ValueTask of Result Success.GetExceptionOrDefaultAsync should produce the expected result.")]
    public async Task ValueTaskResultSuccessGetExceptionOrDefaultAsyncShouldProduceExpectedResult()
    {
        // Given
        Result result = Result.Success();

        // When
        Exception? actual = await ValueTask.FromResult(result).GetExceptionOrDefaultAsync(token: TestContext.Current.CancellationToken);

        // Then
        Assert.Null(actual);
    }

    [Fact(DisplayName = "ValueTask of Result Success.GetExceptionOrDefaultAsync with default value should produce the expected result.")]
    public async Task ValueTaskResultSuccessGetExceptionOrDefaultAsyncWithDefaultValueShouldProduceExpectedResult()
    {
        // Given
        Result result = Result.Success();

        // When
        Exception actual = await ValueTask.FromResult(result).GetExceptionOrDefaultAsync(FailureException, token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(FailureException, actual);
    }

    [Fact(DisplayName = "ValueTask of Result Success.GetExceptionOrThrowAsync should produce the expected result.")]
    public async Task ValueTaskResultSuccessGetExceptionOrThrowAsyncShouldProduceExpectedResult()
    {
        // Given
        Result result = Result.Success();

        // When
        Exception exception = await Assert.ThrowsAsync<InvalidOperationException>(async () =>
            await ValueTask.FromResult(result).GetExceptionOrThrowAsync(token: TestContext.Current.CancellationToken));

        // Then
        Assert.Equal("The current result is in a success state; no exception to retrieve.", exception.Message);
    }

    [Fact(DisplayName = "ValueTask of Result Failure.GetExceptionOrDefaultAsync should produce the expected result.")]
    public async Task ValueTaskResultFailureGetExceptionOrDefaultAsyncShouldProduceExpectedResult()
    {
        // Given
        Result result = Result.Failure(FailureException);

        // When
        Exception? actual = await ValueTask.FromResult(result).GetExceptionOrDefaultAsync(token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(FailureException, actual);
    }

    [Fact(DisplayName = "ValueTask of Result Failure.GetExceptionOrDefaultAsync with default value should produce the expected result.")]
    public async Task ValueTaskResultFailureGetExceptionOrDefaultAsyncWithDefaultValueShouldProduceExpectedResult()
    {
        // Given
        Result result = Result.Failure(FailureException);

        // When
        Exception actual = await ValueTask.FromResult(result).GetExceptionOrDefaultAsync(new Exception("unexpected exception"), token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(FailureException, actual);
    }

    [Fact(DisplayName = "ValueTask of Result Failure.GetExceptionOrThrowAsync should produce the expected result.")]
    public async Task ValueTaskResultFailureGetExceptionOrThrowAsyncShouldProduceExpectedResult()
    {
        // Given
        Result result = Result.Failure(FailureException);

        // When
        Exception actual = await ValueTask.FromResult(result).GetExceptionOrThrowAsync(token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(FailureException, actual);
    }

    [Fact(DisplayName = "ValueTask of Result<T> Success.GetExceptionOrDefaultAsync should produce the expected result.")]
    public async Task ValueTaskResultOfTSuccessGetExceptionOrDefaultAsyncShouldProduceExpectedResult()
    {
        // Given
        Result<int> result = Result<int>.Success(123);

        // When
        Exception? actual = await ValueTask.FromResult(result).GetExceptionOrDefaultAsync(token: TestContext.Current.CancellationToken);

        // Then
        Assert.Null(actual);
    }

    [Fact(DisplayName = "ValueTask of Result<T> Success.GetExceptionOrDefaultAsync with default value should produce the expected result.")]
    public async Task ValueTaskResultOfTSuccessGetExceptionOrDefaultAsyncWithDefaultValueShouldProduceExpectedResult()
    {
        // Given
        Result<int> result = Result<int>.Success(123);

        // When
        Exception actual = await ValueTask.FromResult(result).GetExceptionOrDefaultAsync(FailureException, token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(FailureException, actual);
    }

    [Fact(DisplayName = "ValueTask of Result<T> Success.GetExceptionOrThrowAsync should produce the expected result.")]
    public async Task ValueTaskResultOfTSuccessGetExceptionOrThrowAsyncShouldProduceExpectedResult()
    {
        // Given
        Result<int> result = Result<int>.Success(123);

        // When
        Exception exception = await Assert.ThrowsAsync<InvalidOperationException>(async () =>
            await ValueTask.FromResult(result).GetExceptionOrThrowAsync(token: TestContext.Current.CancellationToken));

        // Then
        Assert.Equal("The current result is in a success state; no exception to retrieve.", exception.Message);
    }

    [Fact(DisplayName = "ValueTask of Result<T> Failure.GetExceptionOrDefaultAsync should produce the expected result.")]
    public async Task ValueTaskResultOfTFailureGetExceptionOrDefaultAsyncShouldProduceExpectedResult()
    {
        // Given
        Result<int> result = Result<int>.Failure(FailureException);

        // When
        Exception? actual = await ValueTask.FromResult(result).GetExceptionOrDefaultAsync(token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(FailureException, actual);
    }

    [Fact(DisplayName = "ValueTask of Result<T> Failure.GetExceptionOrDefaultAsync with default value should produce the expected result.")]
    public async Task ValueTaskResultOfTFailureGetExceptionOrDefaultAsyncWithDefaultValueShouldProduceExpectedResult()
    {
        // Given
        Result<int> result = Result<int>.Failure(FailureException);

        // When
        Exception actual = await ValueTask.FromResult(result).GetExceptionOrDefaultAsync(new Exception("unexpected exception"), token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(FailureException, actual);
    }

    [Fact(DisplayName = "ValueTask of Result<T> Failure.GetExceptionOrThrowAsync should produce the expected result.")]
    public async Task ValueTaskResultOfTFailureGetExceptionOrThrowAsyncShouldProduceExpectedResult()
    {
        // Given
        Result<int> result = Result<int>.Failure(FailureException);

        // When
        Exception actual = await ValueTask.FromResult(result).GetExceptionOrThrowAsync(token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(FailureException, actual);
    }

    [Fact(DisplayName = "ValueTask of Result.GetValueOrDefaultAsync should return the result value when the result is Success")]
    public async Task ValueTaskResultGetValueOrDefaultAsyncShouldReturnResultValueWhenResultIsSuccess()
    {
        // Given
        const int expectedNumber = 123;
        const string expectedText = "abc";
        ValueTask<Result<int>> numberTask = new(Result<int>.OfAsync(async () => await Task.FromResult(expectedNumber), TestContext.Current.CancellationToken));
        ValueTask<Result<string>> textTask = new(Result<string>.OfAsync(async () => await Task.FromResult(expectedText), TestContext.Current.CancellationToken));

        // When
        int actualNumber = await numberTask.GetValueOrDefaultAsync(token: TestContext.Current.CancellationToken);
        string? actualText = await textTask.GetValueOrDefaultAsync(token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expectedNumber, actualNumber);

        Assert.NotNull(actualText);
        Assert.Equal(expectedText, actualText);
    }

    [Fact(DisplayName = "ValueTask of Result.GetValueOrDefaultAsync should return default when the result is Failure")]
    public async Task ValueTaskResultGetValueOrDefaultAsyncShouldReturnDefaultWhenResultIsFailure()
    {
        // Given
        ValueTask<Result<int>> numberTask = new(Result<int>.OfAsync(() => throw FailureException, TestContext.Current.CancellationToken));
        ValueTask<Result<string>> textTask = new(Result<string>.OfAsync(() => throw FailureException, TestContext.Current.CancellationToken));

        // When
        int actualNumber = await numberTask.GetValueOrDefaultAsync(token: TestContext.Current.CancellationToken);
        string? actualText = await textTask.GetValueOrDefaultAsync(token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(0, actualNumber);
        Assert.Null(actualText);
    }

    [Fact(DisplayName = "ValueTask of Result.GetValueOrDefaultAsync with default value should return the result value when the result is Success")]
    public async Task ValueTaskResultGetValueOrDefaultAsyncWithDefaultValueShouldReturnResultValueWhenResultIsSuccess()
    {
        // Given
        const int expectedNumber = 123;
        const string expectedText = "abc";
        ValueTask<Result<int>> numberTask = new(Result<int>.OfAsync(async () => await Task.FromResult(expectedNumber), TestContext.Current.CancellationToken));
        ValueTask<Result<string>> textTask = new(Result<string>.OfAsync(async () => await Task.FromResult(expectedText), TestContext.Current.CancellationToken));

        // When
        int actualNumber = await numberTask.GetValueOrDefaultAsync(456, token: TestContext.Current.CancellationToken);
        // ReSharper disable once VariableCanBeNotNullable
        string? actualText = await textTask.GetValueOrDefaultAsync("xyz", token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expectedNumber, actualNumber);

        Assert.NotNull(actualText);
        Assert.Equal(expectedText, actualText);
    }

    [Fact(DisplayName = "ValueTask of Result.GetValueOrDefaultAsync with default value should return default value when the result is Failure")]
    public async Task ValueTaskResultGetValueOrDefaultAsyncWithDefaultValueShouldReturnDefaultValueWhenResultIsFailure()
    {
        // Given
        const int expectedNumber = 456;
        const string expectedText = "abc";
        ValueTask<Result<int>> numberTask = new(Result<int>.OfAsync(() => throw FailureException, TestContext.Current.CancellationToken));
        ValueTask<Result<string>> textTask = new(Result<string>.OfAsync(() => throw FailureException, TestContext.Current.CancellationToken));

        // When
        int actualNumber = await numberTask.GetValueOrDefaultAsync(expectedNumber, token: TestContext.Current.CancellationToken);
        string actualText = await textTask.GetValueOrDefaultAsync(expectedText, token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expectedNumber, actualNumber);

        Assert.NotNull(actualText);
        Assert.Equal(expectedText, actualText);
    }

    [Fact(DisplayName = "ValueTask of Result.GetValueOrThrowAsync should return the result value when the result is Success")]
    public async Task ValueTaskResultGetValueOrThrowAsyncShouldReturnResultValueWhenResultIsSuccess()
    {
        // Given
        const int expectedNumber = 123;
        const string expectedText = "abc";
        ValueTask<Result<int>> numberTask = new(Result<int>.OfAsync(async () => await Task.FromResult(expectedNumber), TestContext.Current.CancellationToken));
        ValueTask<Result<string>> textTask = new(Result<string>.OfAsync(async () => await Task.FromResult(expectedText), TestContext.Current.CancellationToken));

        // When
        int actualNumber = await numberTask.GetValueOrThrowAsync(token: TestContext.Current.CancellationToken);
        string actualText = await textTask.GetValueOrThrowAsync(token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expectedNumber, actualNumber);

        Assert.NotNull(actualText);
        Assert.Equal(expectedText, actualText);
    }

    [Fact(DisplayName = "ValueTask of Result.GetValueOrThrowAsync should throw Exception when the result is Failure")]
    public async Task ValueTaskResultGetValueOrThrowAsyncShouldReturnDefaultWhenResultIsFailure()
    {
        // Given
        ValueTask<Result<int>> numberTask = new(Result<int>.OfAsync(() => throw FailureException, TestContext.Current.CancellationToken));
        ValueTask<Result<string>> textTask = new(Result<string>.OfAsync(() => throw FailureException, TestContext.Current.CancellationToken));

        // When
        Exception numberException = await Assert.ThrowsAsync<Exception>(async () =>
            await numberTask.GetValueOrThrowAsync(token: TestContext.Current.CancellationToken));

        Exception textException = await Assert.ThrowsAsync<Exception>(async () =>
            await textTask.GetValueOrThrowAsync(token: TestContext.Current.CancellationToken));

        // Then
        Assert.Equal(numberException, FailureException);
        Assert.Equal(textException, FailureException);
    }

    [Fact(DisplayName = "ValueTask of Result<T>.GetValueOrNoneAsync should return the value wrapped in Optional when the result is Success and the value is not None")]
    public async Task ValueTaskResultGetValueOrNoneAsyncShouldReturnOptionalValueWhenResultIsSuccessAndValueIsNotNone()
    {
        // Given
        const int expectedNumber = 123;
        const string expectedText = "abc";
        Result<int> numberResult = expectedNumber;
        Result<string> textResult = expectedText;

        // When
        Optional<int> actualNumber = await ValueTask.FromResult(numberResult).GetValueOrNoneAsync(token: TestContext.Current.CancellationToken);
        Optional<string> actualText = await ValueTask.FromResult(textResult).GetValueOrNoneAsync(token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(Optional<int>.Of(expectedNumber), actualNumber);
        Assert.Equal(Optional<string>.Of(expectedText), actualText);
    }

    [Fact(DisplayName = "ValueTask of Result<T>.GetValueOrNoneAsync should return Some(default) when the result is Success and the value is the default of a value type")]
    public async Task ValueTaskResultGetValueOrNoneAsyncShouldReturnSomeDefaultWhenResultIsSuccessAndValueIsValueTypeDefault()
    {
        // Given
        Result<int> numberResult = Result<int>.Success(0);

        // When
        Optional<int> actualNumber = await ValueTask.FromResult(numberResult).GetValueOrNoneAsync(token: TestContext.Current.CancellationToken);

        // Then
        Assert.True(Optional<int>.IsSome(actualNumber));
        Assert.Equal(0, actualNumber);
    }

    [Fact(DisplayName = "ValueTask of Result<T>.GetValueOrNoneAsync should return None when the result is Success and the underlying value is null")]
    public async Task ValueTaskResultGetValueOrNoneAsyncShouldReturnNoneWhenResultIsSuccessAndUnderlyingValueIsNull()
    {
        // Given
        Result<string> textResult = Result<string>.Success(null!);

        // When
        Optional<string> actualText = await ValueTask.FromResult(textResult).GetValueOrNoneAsync(token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(Optional<string>.None, actualText);
    }

    [Fact(DisplayName = "ValueTask of Result<T>.GetValueOrNoneAsync should return None when the result is Failure")]
    public async Task ValueTaskResultGetValueOrNoneAsyncShouldReturnNoneWhenResultIsFailure()
    {
        // Given
        Result<int> numberResult = Result<int>.Failure(FailureException);
        Result<string> textResult = Result<string>.Failure(FailureException);

        // When
        Optional<int> actualNumber = await ValueTask.FromResult(numberResult).GetValueOrNoneAsync(token: TestContext.Current.CancellationToken);
        Optional<string> actualText = await ValueTask.FromResult(textResult).GetValueOrNoneAsync(token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(Optional<int>.None, actualNumber);
        Assert.Equal(Optional<string>.None, actualText);
    }

    [Fact(DisplayName = "ValueTask of Result<Optional<T>>.GetValueOrNoneAsync should return the Optional value when the result is Success and the Optional value is not None")]
    public async Task ValueTaskResultOptionalGetValueOrNoneAsyncShouldReturnOptionalValueWhenResultIsSuccessAndOptionalValueIsNotNone()
    {
        // Given
        Optional<int> expectedNumber = 123;
        Optional<string> expectedText = "abc";
        Result<Optional<int>> numberResult = expectedNumber;
        Result<Optional<string>> textResult = expectedText;

        // When
        Optional<int> actualNumber = await ValueTask.FromResult(numberResult).GetValueOrNoneAsync(token: TestContext.Current.CancellationToken);
        Optional<string> actualText = await ValueTask.FromResult(textResult).GetValueOrNoneAsync(token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expectedNumber, actualNumber);
        Assert.Equal(expectedText, actualText);
    }

    [Fact(DisplayName = "ValueTask of Result<Optional<T>>.GetValueOrNoneAsync should return None value when the result is Success and the Optional value is None")]
    public async Task ValueTaskResultOptionalGetValueOrNoneAsyncShouldReturnNoneWhenResultIsSuccessAndOptionalValueIsNone()
    {
        // Given
        Optional<int> expectedNumber = Optional<int>.None;
        Optional<string> expectedText = Optional<string>.None;
        Result<Optional<int>> numberResult = expectedNumber;
        Result<Optional<string>> textResult = expectedText;

        // When
        Optional<int> actualNumber = await ValueTask.FromResult(numberResult).GetValueOrNoneAsync(token: TestContext.Current.CancellationToken);
        Optional<string> actualText = await ValueTask.FromResult(textResult).GetValueOrNoneAsync(token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expectedNumber, actualNumber);
        Assert.Equal(expectedText, actualText);
    }

    [Fact(DisplayName = "ValueTask of Result<Optional<T>>.GetValueOrNoneAsync should return None value when the result is Failure")]
    public async Task ValueTaskResultOptionalGetValueOrNoneAsyncShouldReturnNoneWhenResultIsFailure()
    {
        // Given
        Optional<int> expectedNumber = Optional<int>.None;
        Optional<string> expectedText = Optional<string>.None;
        Result<Optional<int>> numberResult = Result<Optional<int>>.Failure(FailureException);
        Result<Optional<string>> textResult = Result<Optional<string>>.Failure(FailureException);

        // When
        Optional<int> actualNumber = await ValueTask.FromResult(numberResult).GetValueOrNoneAsync(token: TestContext.Current.CancellationToken);
        Optional<string> actualText = await ValueTask.FromResult(textResult).GetValueOrNoneAsync(token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expectedNumber, actualNumber);
        Assert.Equal(expectedText, actualText);
    }

    [Fact(DisplayName = "ValueTask of Result.GetOptionalValueOrThrowAsync should return the Optional value when the Result is Success and the Optional value is not None")]
    public async Task ValueTaskResultGetOptionalValueOrThrowAsyncShouldReturnOptionalValueWhenResultIsSuccessAndOptionalValueIsNotNone()
    {
        // Given
        const int expectedNumber = 123;
        const string expectedText = "abc";
        Result<Optional<int>> numberResult = Optional<int>.Some(expectedNumber);
        Result<Optional<string>> textResult = Optional<string>.Some(expectedText);

        // When
        int actualNumber = await ValueTask.FromResult(numberResult).GetOptionalValueOrThrowAsync(token: TestContext.Current.CancellationToken);
        string actualText = await ValueTask.FromResult(textResult).GetOptionalValueOrThrowAsync(token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expectedNumber, actualNumber);
        Assert.Equal(expectedText, actualText);
    }

    [Fact(DisplayName = "ValueTask of Result.GetOptionalValueOrThrowAsync should throw InvalidOperationException when the Result is Success and the Optional value is None")]
    public async Task ValueTaskResultGetOptionalValueOrThrowAsyncShouldThrowInvalidOperationExceptionWhenResultIsSuccessAndOptionalValueIsNone()
    {
        // Given
        Result<Optional<int>> numberResult = Optional<int>.None;
        Result<Optional<string>> textResult = Optional<string>.None;

        // When
        Exception numberException = await Assert.ThrowsAsync<InvalidOperationException>(async () =>
            await ValueTask.FromResult(numberResult).GetOptionalValueOrThrowAsync(token: TestContext.Current.CancellationToken));

        Exception textException = await Assert.ThrowsAsync<InvalidOperationException>(async () =>
            await ValueTask.FromResult(textResult).GetOptionalValueOrThrowAsync(token: TestContext.Current.CancellationToken));

        // Then
        Assert.Equal("Optional value of type System.Int32 is not present.", numberException.Message);
        Assert.Equal("Optional value of type System.String is not present.", textException.Message);
    }

    [Fact(DisplayName = "ValueTask of Result.GetOptionalValueOrThrowAsync should throw InvalidOperationException when the Result is Failure")]
    public async Task ValueTaskResultGetOptionalValueOrThrowAsyncShouldThrowInvalidOperationExceptionWhenResultIsFailure()
    {
        // Given
        Result<Optional<int>> numberResult = Result<Optional<int>>.Failure(new Exception("Result has failed."));
        Result<Optional<string>> textResult = Result<Optional<string>>.Failure(new Exception("Result has failed."));

        // When
        Exception numberException = await Assert.ThrowsAsync<Exception>(async () =>
            await ValueTask.FromResult(numberResult).GetOptionalValueOrThrowAsync(token: TestContext.Current.CancellationToken));

        Exception textException = await Assert.ThrowsAsync<Exception>(async () =>
            await ValueTask.FromResult(textResult).GetOptionalValueOrThrowAsync(token: TestContext.Current.CancellationToken));

        // Then
        Assert.Equal("Result has failed.", numberException.Message);
        Assert.Equal("Result has failed.", textException.Message);
    }

    [Fact(DisplayName = "ValueTask of Result.GetOptionalValueOrDefaultAsync should return the Optional value when the Result is Success and the Optional value is not None")]
    public async Task ValueTaskResultGetOptionalValueOrDefaultAsyncShouldReturnOptionalValueWhenResultIsSuccessAndOptionalValueIsNotNone()
    {
        // Given
        const int expectedNumber = 123;
        const string expectedText = "abc";
        Result<Optional<int>> numberResult = Optional<int>.Some(expectedNumber);
        Result<Optional<string>> textResult = Optional<string>.Some(expectedText);

        // When
        int actualNumber = await ValueTask.FromResult(numberResult).GetOptionalValueOrDefaultAsync(456, token: TestContext.Current.CancellationToken);
        string actualText = await ValueTask.FromResult(textResult).GetOptionalValueOrDefaultAsync("xyz", token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expectedNumber, actualNumber);
        Assert.Equal(expectedText, actualText);
    }

    [Fact(DisplayName = "ValueTask of Result.GetOptionalValueOrDefaultAsync should throw return the default value when the Result is Success and the Optional value is None")]
    public async Task ValueTaskResultGetOptionalValueOrDefaultAsyncShouldReturnDefaultValueWhenResultIsSuccessAndOptionalValueIsNone()
    {
        // Given
        const int expectedNumber = 456;
        const string expectedText = "xyz";
        Result<Optional<int>> numberResult = Optional<int>.None;
        Result<Optional<string>> textResult = Optional<string>.None;

        // When
        int actualNumber = await ValueTask.FromResult(numberResult).GetOptionalValueOrDefaultAsync(expectedNumber, token: TestContext.Current.CancellationToken);
        string actualText = await ValueTask.FromResult(textResult).GetOptionalValueOrDefaultAsync(expectedText, token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expectedNumber, actualNumber);
        Assert.Equal(expectedText, actualText);
    }

    [Fact(DisplayName = "ValueTask of Result.GetOptionalValueOrDefaultAsync should return default value when the result is Failure")]
    public async Task ValueTaskResultGetOptionalValueOrDefaultAsyncShouldReturnDefaultValueWhenResultIsFailure()
    {
        // Given
        const int expectedNumber = 456;
        const string expectedText = "xyz";
        Result<Optional<int>> numberResult = Result<Optional<int>>.Failure(new Exception("Result has failed."));
        Result<Optional<string>> textResult = Result<Optional<string>>.Failure(new Exception("Result has failed."));

        // When
        int actualNumber = await ValueTask.FromResult(numberResult).GetOptionalValueOrDefaultAsync(expectedNumber, token: TestContext.Current.CancellationToken);
        string actualText = await ValueTask.FromResult(textResult).GetOptionalValueOrDefaultAsync(expectedText, token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expectedNumber, actualNumber);
        Assert.Equal(expectedText, actualText);
    }

    [Fact(DisplayName = "ValueTask<Result>.MatchAsync of Success should invoke the success delegate (Action, Action<Exception>)")]
    public async Task ValueTaskResultMatchAsyncOfSuccessShouldInvokeSuccessDelegateActionActionException()
    {
        // Given
        Result result = Result.Success();
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await ValueTask.FromResult(result).MatchAsync(Success, Failure, token: TestContext.Current.CancellationToken);

        // Then
        Assert.True(successCalled);
        Assert.False(failureCalled);

        return;
        void Success() => successCalled = true;
        void Failure(Exception exception) => failureCalled = true;
    }

    [Fact(DisplayName = "ValueTask<Result>.MatchAsync of Failure should invoke the failure delegate (Action, Action<Exception>)")]
    public async Task ValueTaskResultMatchAsyncOfFailureShouldInvokeFailureDelegateActionActionException()
    {
        // Given
        Result result = Result.Failure(FailureException);
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await ValueTask.FromResult(result).MatchAsync(Success, Failure, token: TestContext.Current.CancellationToken);

        // Then
        Assert.False(successCalled);
        Assert.True(failureCalled);

        return;
        void Success() => successCalled = true;
        void Failure(Exception exception) => failureCalled = true;
    }

    [Fact(DisplayName = "ValueTask<Result>.MatchAsync of Success should invoke the success delegate (Func<Task>, Action<Exception>)")]
    public async Task ValueTaskResultMatchAsyncOfSuccessShouldInvokeSuccessDelegateFuncTaskActionException()
    {
        // Given
        Result result = Result.Success();
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await ValueTask.FromResult(result).MatchAsync(SuccessAsync, Failure, token: TestContext.Current.CancellationToken);

        // Then
        Assert.True(successCalled);
        Assert.False(failureCalled);

        return;
        async Task SuccessAsync() => await Task.Run(() => successCalled = true);
        void Failure(Exception exception) => failureCalled = true;
    }

    [Fact(DisplayName = "ValueTask<Result>.MatchAsync of Failure should invoke the failure delegate (Func<Task>, Action<Exception>)")]
    public async Task ValueTaskResultMatchAsyncOfFailureShouldInvokeFailureDelegateFuncTaskActionException()
    {
        // Given
        Result result = Result.Failure(FailureException);
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await ValueTask.FromResult(result).MatchAsync(SuccessAsync, Failure, token: TestContext.Current.CancellationToken);

        // Then
        Assert.False(successCalled);
        Assert.True(failureCalled);

        return;
        async Task SuccessAsync() => await Task.Run(() => successCalled = true);
        void Failure(Exception exception) => failureCalled = true;
    }

    [Fact(DisplayName = "ValueTask<Result>.MatchAsync of Success should invoke the success delegate (Func<CancellationToken, Task>, Action<Exception>)")]
    public async Task ValueTaskResultMatchAsyncOfSuccessShouldInvokeSuccessDelegateFuncCancellationTokenTaskActionException()
    {
        // Given
        Result result = Result.Success();
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await ValueTask.FromResult(result).MatchAsync(SuccessAsync, Failure, token: TestContext.Current.CancellationToken);

        // Then
        Assert.True(successCalled);
        Assert.False(failureCalled);

        return;
        async Task SuccessAsync(CancellationToken ct) => await Task.Run(() => successCalled = true, ct);
        void Failure(Exception exception) => failureCalled = true;
    }

    [Fact(DisplayName = "ValueTask<Result>.MatchAsync of Failure should invoke the failure delegate (Func<CancellationToken, Task>, Action<Exception>)")]
    public async Task ValueTaskResultMatchAsyncOfFailureShouldInvokeFailureDelegateFuncCancellationTokenTaskActionException()
    {
        // Given
        Result result = Result.Failure(FailureException);
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await ValueTask.FromResult(result).MatchAsync(SuccessAsync, Failure, token: TestContext.Current.CancellationToken);

        // Then
        Assert.False(successCalled);
        Assert.True(failureCalled);

        return;
        async Task SuccessAsync(CancellationToken ct) => await Task.Run(() => successCalled = true, ct);
        void Failure(Exception exception) => failureCalled = true;
    }

    [Fact(DisplayName = "ValueTask<Result>.MatchAsync of Success should invoke the success delegate (Action, Func<Exception, Task>)")]
    public async Task ValueTaskResultMatchAsyncOfSuccessShouldInvokeSuccessDelegateActionFuncExceptionTask()
    {
        // Given
        Result result = Result.Success();
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await ValueTask.FromResult(result).MatchAsync(Success, FailureAsync, token: TestContext.Current.CancellationToken);

        // Then
        Assert.True(successCalled);
        Assert.False(failureCalled);

        return;
        void Success() => successCalled = true;
        async Task FailureAsync(Exception exception) => await Task.Run(() => failureCalled = true);
    }

    [Fact(DisplayName = "ValueTask<Result>.MatchAsync of Failure should invoke the failure delegate (Action, Func<Exception, Task>)")]
    public async Task ValueTaskResultMatchAsyncOfFailureShouldInvokeFailureDelegateActionFuncExceptionTask()
    {
        // Given
        Result result = Result.Failure(FailureException);
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await ValueTask.FromResult(result).MatchAsync(Success, FailureAsync, token: TestContext.Current.CancellationToken);

        // Then
        Assert.False(successCalled);
        Assert.True(failureCalled);

        return;
        void Success() => successCalled = true;
        async Task FailureAsync(Exception exception) => await Task.Run(() => failureCalled = true);
    }

    [Fact(DisplayName = "ValueTask<Result>.MatchAsync of Success should invoke the success delegate (Action, Func<Exception, CancellationToken, Task>)")]
    public async Task ValueTaskResultMatchAsyncOfSuccessShouldInvokeSuccessDelegateActionFuncExceptionCancellationTokenTask()
    {
        // Given
        Result result = Result.Success();
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await ValueTask.FromResult(result).MatchAsync(Success, FailureAsync, token: TestContext.Current.CancellationToken);

        // Then
        Assert.True(successCalled);
        Assert.False(failureCalled);

        return;
        void Success() => successCalled = true;
        async Task FailureAsync(Exception exception, CancellationToken ct) => await Task.Run(() => failureCalled = true, ct);
    }

    [Fact(DisplayName = "ValueTask<Result>.MatchAsync of Failure should invoke the failure delegate (Action, Func<Exception, CancellationToken, Task>)")]
    public async Task ValueTaskResultMatchAsyncOfFailureShouldInvokeFailureDelegateActionFuncExceptionCancellationTokenTask()
    {
        // Given
        Result result = Result.Failure(FailureException);
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await ValueTask.FromResult(result).MatchAsync(Success, FailureAsync, token: TestContext.Current.CancellationToken);

        // Then
        Assert.False(successCalled);
        Assert.True(failureCalled);

        return;
        void Success() => successCalled = true;
        async Task FailureAsync(Exception exception, CancellationToken ct) => await Task.Run(() => failureCalled = true, ct);
    }

    [Fact(DisplayName = "ValueTask<Result>.MatchAsync of Success should invoke the success delegate (Func<Task>, Func<Exception, Task>)")]
    public async Task ValueTaskResultMatchAsyncOfSuccessShouldInvokeSuccessDelegateFuncTaskFuncExceptionTask()
    {
        // Given
        Result result = Result.Success();
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await ValueTask.FromResult(result).MatchAsync(SuccessAsync, FailureAsync, token: TestContext.Current.CancellationToken);

        // Then
        Assert.True(successCalled);
        Assert.False(failureCalled);

        return;
        async Task SuccessAsync() => await Task.Run(() => successCalled = true);
        async Task FailureAsync(Exception exception) => await Task.Run(() => failureCalled = true);
    }

    [Fact(DisplayName = "ValueTask<Result>.MatchAsync of Failure should invoke the failure delegate (Func<Task>, Func<Exception, Task>)")]
    public async Task ValueTaskResultMatchAsyncOfFailureShouldInvokeFailureDelegateFuncTaskFuncExceptionTask()
    {
        // Given
        Result result = Result.Failure(FailureException);
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await ValueTask.FromResult(result).MatchAsync(SuccessAsync, FailureAsync, token: TestContext.Current.CancellationToken);

        // Then
        Assert.False(successCalled);
        Assert.True(failureCalled);

        return;
        async Task SuccessAsync() => await Task.Run(() => successCalled = true);
        async Task FailureAsync(Exception exception) => await Task.Run(() => failureCalled = true);
    }

    [Fact(DisplayName = "ValueTask<Result>.MatchAsync of Success should invoke the success delegate (Func<CancellationToken, Task>, Func<Exception, CancellationToken, Task>)")]
    public async Task ValueTaskResultMatchAsyncOfSuccessShouldInvokeSuccessDelegateFuncCancellationTokenTaskFuncExceptionCancellationTokenTask()
    {
        // Given
        Result result = Result.Success();
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await ValueTask.FromResult(result).MatchAsync(SuccessAsync, FailureAsync, token: TestContext.Current.CancellationToken);

        // Then
        Assert.True(successCalled);
        Assert.False(failureCalled);

        return;
        async Task SuccessAsync(CancellationToken ct) => await Task.Run(() => successCalled = true, ct);
        async Task FailureAsync(Exception exception, CancellationToken ct) => await Task.Run(() => failureCalled = true, ct);
    }

    [Fact(DisplayName = "ValueTask<Result>.MatchAsync of Failure should invoke the failure delegate (Func<CancellationToken, Task>, Func<Exception, CancellationToken, Task>)")]
    public async Task ValueTaskResultMatchAsyncOfFailureShouldInvokeFailureDelegateFuncCancellationTokenTaskFuncExceptionCancellationTokenTask()
    {
        // Given
        Result result = Result.Failure(FailureException);
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await ValueTask.FromResult(result).MatchAsync(SuccessAsync, FailureAsync, token: TestContext.Current.CancellationToken);

        // Then
        Assert.False(successCalled);
        Assert.True(failureCalled);

        return;
        async Task SuccessAsync(CancellationToken ct) => await Task.Run(() => successCalled = true, ct);
        async Task FailureAsync(Exception exception, CancellationToken ct) => await Task.Run(() => failureCalled = true, ct);
    }

    [Fact(DisplayName = "ValueTask<Result>.MatchAsync<TResult> of Success should return the result from success delegate (Func<TResult>, Func<Exception, TResult>)")]
    public async Task ValueTaskResultMatchAsyncOfSuccessShouldReturnResultFromSuccessDelegateFuncResultFuncExceptionResult()
    {
        // Given
        Result result = Result.Success();
        const string expected = "Success";

        // When
        string actual = await ValueTask.FromResult(result).MatchAsync(Success, Failure, token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        string Success() => "Success";
        string Failure(Exception exception) => "Failure";
    }

    [Fact(DisplayName = "ValueTask<Result>.MatchAsync<TResult> of Failure should return the result from failure delegate (Func<TResult>, Func<Exception, TResult>)")]
    public async Task ValueTaskResultMatchAsyncOfFailureShouldReturnResultFromFailureDelegateFuncResultFuncExceptionResult()
    {
        // Given
        Result result = Result.Failure(FailureException);
        const string expected = "Failure";

        // When
        string actual = await ValueTask.FromResult(result).MatchAsync(Success, Failure, token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        string Success() => "Success";
        string Failure(Exception exception) => "Failure";
    }

    [Fact(DisplayName = "ValueTask<Result>.MatchAsync<TResult> of Success should return the result from success delegate (Func<Task<TResult>>, Func<Exception, TResult>)")]
    public async Task ValueTaskResultMatchAsyncOfSuccessShouldReturnResultFromSuccessDelegateFuncTaskResultFuncExceptionResult()
    {
        // Given
        Result result = Result.Success();
        const string expected = "Success";

        // When
        string actual = await ValueTask.FromResult(result).MatchAsync(SuccessAsync, Failure, token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        async Task<string> SuccessAsync() => await Task.FromResult("Success");
        string Failure(Exception exception) => "Failure";
    }

    [Fact(DisplayName = "ValueTask<Result>.MatchAsync<TResult> of Failure should return the result from failure delegate (Func<Task<TResult>>, Func<Exception, TResult>)")]
    public async Task ValueTaskResultMatchAsyncOfFailureShouldReturnResultFromFailureDelegateFuncTaskResultFuncExceptionResult()
    {
        // Given
        Result result = Result.Failure(FailureException);
        const string expected = "Failure";

        // When
        string actual = await ValueTask.FromResult(result).MatchAsync(SuccessAsync, Failure, token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        async Task<string> SuccessAsync() => await Task.FromResult("Success");
        string Failure(Exception exception) => "Failure";
    }

    [Fact(DisplayName = "ValueTask<Result>.MatchAsync<TResult> of Success should return the result from success delegate (Func<CancellationToken, Task<TResult>>, Func<Exception, TResult>)")]
    public async Task ValueTaskResultMatchAsyncOfSuccessShouldReturnResultFromSuccessDelegateFuncCancellationTokenTaskResultFuncExceptionResult()
    {
        // Given
        Result result = Result.Success();
        const string expected = "Success";

        // When
        string actual = await ValueTask.FromResult(result).MatchAsync(SuccessAsync, Failure, token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        async Task<string> SuccessAsync(CancellationToken ct) => await Task.FromResult("Success");
        string Failure(Exception exception) => "Failure";
    }

    [Fact(DisplayName = "ValueTask<Result>.MatchAsync<TResult> of Failure should return the result from failure delegate (Func<CancellationToken, Task<TResult>>, Func<Exception, TResult>)")]
    public async Task ValueTaskResultMatchAsyncOfFailureShouldReturnResultFromFailureDelegateFuncCancellationTokenTaskResultFuncExceptionResult()
    {
        // Given
        Result result = Result.Failure(FailureException);
        const string expected = "Failure";

        // When
        string actual = await ValueTask.FromResult(result).MatchAsync(SuccessAsync, Failure, token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        async Task<string> SuccessAsync(CancellationToken ct) => await Task.FromResult("Success");
        string Failure(Exception exception) => "Failure";
    }

    [Fact(DisplayName = "ValueTask<Result>.MatchAsync<TResult> of Success should return the result from success delegate (Func<TResult>, Func<Exception, Task<TResult>>>)")]
    public async Task ValueTaskResultMatchAsyncOfSuccessShouldReturnResultFromSuccessDelegateFuncResultFuncExceptionTaskResult()
    {
        // Given
        Result result = Result.Success();
        const string expected = "Success";

        // When
        string actual = await ValueTask.FromResult(result).MatchAsync(Success, FailureAsync, token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        string Success() => "Success";
        async Task<string> FailureAsync(Exception exception) => await Task.FromResult("Failure");
    }

    [Fact(DisplayName = "ValueTask<Result>.MatchAsync<TResult> of Failure should return the result from failure delegate (Func<TResult>, Func<Exception, Task<TResult>>>)")]
    public async Task ValueTaskResultMatchAsyncOfFailureShouldReturnResultFromFailureDelegateFuncResultFuncExceptionTaskResult()
    {
        // Given
        Result result = Result.Failure(FailureException);
        const string expected = "Failure";

        // When
        string actual = await ValueTask.FromResult(result).MatchAsync(Success, FailureAsync, token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        string Success() => "Success";
        async Task<string> FailureAsync(Exception exception) => await Task.FromResult("Failure");
    }

    [Fact(DisplayName = "ValueTask<Result>.MatchAsync<TResult> of Success should return the result from success delegate (Func<TResult>, Func<Exception, CancellationToken, Task<TResult>>>)")]
    public async Task ValueTaskResultMatchAsyncOfSuccessShouldReturnResultFromSuccessDelegateFuncResultFuncExceptionCancellationTokenTaskResult()
    {
        // Given
        Result result = Result.Success();
        const string expected = "Success";

        // When
        string actual = await ValueTask.FromResult(result).MatchAsync(Success, FailureAsync, token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        string Success() => "Success";
        async Task<string> FailureAsync(Exception exception, CancellationToken ct) => await Task.FromResult("Failure");
    }

    [Fact(DisplayName = "ValueTask<Result>.MatchAsync<TResult> of Failure should return the result from failure delegate (Func<TResult>, Func<Exception, CancellationToken, Task<TResult>>>)")]
    public async Task ValueTaskResultMatchAsyncOfFailureShouldReturnResultFromFailureDelegateFuncResultFuncExceptionCancellationTokenTaskResult()
    {
        // Given
        Result result = Result.Failure(FailureException);
        const string expected = "Failure";

        // When
        string actual = await ValueTask.FromResult(result).MatchAsync(Success, FailureAsync, token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        string Success() => "Success";
        async Task<string> FailureAsync(Exception exception, CancellationToken ct) => await Task.FromResult("Failure");
    }

    [Fact(DisplayName = "ValueTask<Result>.MatchAsync<TResult> of Success should return the result from success delegate (Func<Task<TResult>>, Func<Exception, Task<TResult>>>)")]
    public async Task ValueTaskResultMatchAsyncOfSuccessShouldReturnResultFromSuccessDelegateFuncTaskResultFuncExceptionTaskResult()
    {
        // Given
        Result result = Result.Success();
        const string expected = "Success";

        // When
        string actual = await ValueTask.FromResult(result).MatchAsync(SuccessAsync, FailureAsync, token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        async Task<string> SuccessAsync() => await Task.FromResult("Success");
        async Task<string> FailureAsync(Exception exception) => await Task.FromResult("Failure");
    }

    [Fact(DisplayName = "ValueTask<Result>.MatchAsync<TResult> of Failure should return the result from failure delegate (Func<Task<TResult>>, Func<Exception, Task<TResult>>>)")]
    public async Task ValueTaskResultMatchAsyncOfFailureShouldReturnResultFromFailureDelegateFuncTaskResultFuncExceptionTaskResult()
    {
        // Given
        Result result = Result.Failure(FailureException);
        const string expected = "Failure";

        // When
        string actual = await ValueTask.FromResult(result).MatchAsync(SuccessAsync, FailureAsync, token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        async Task<string> SuccessAsync() => await Task.FromResult("Success");
        async Task<string> FailureAsync(Exception exception) => await Task.FromResult("Failure");
    }

    [Fact(DisplayName = "ValueTask<Result>.MatchAsync<TResult> of Success should return the result from success delegate (Func<CancellationToken, Task<TResult>>, Func<Exception, CancellationToken, Task<TResult>>>)")]
    public async Task ValueTaskResultMatchAsyncOfSuccessShouldReturnResultFromSuccessDelegateFuncCancellationTokenTaskResultFuncExceptionCancellationTokenTaskResult()
    {
        // Given
        Result result = Result.Success();
        const string expected = "Success";

        // When
        string actual = await ValueTask.FromResult(result).MatchAsync(SuccessAsync, FailureAsync, token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        async Task<string> SuccessAsync(CancellationToken ct) => await Task.FromResult("Success");
        async Task<string> FailureAsync(Exception exception, CancellationToken ct) => await Task.FromResult("Failure");
    }

    [Fact(DisplayName = "ValueTask<Result>.MatchAsync<TResult> of Failure should return the result from failure delegate (Func<CancellationToken, Task<TResult>>, Func<Exception, CancellationToken, Task<TResult>>>)")]
    public async Task ValueTaskResultMatchAsyncOfFailureShouldReturnResultFromFailureDelegateFuncCancellationTokenTaskResultFuncExceptionCancellationTokenTaskResult()
    {
        // Given
        Result result = Result.Failure(FailureException);
        const string expected = "Failure";

        // When
        string actual = await ValueTask.FromResult(result).MatchAsync(SuccessAsync, FailureAsync, token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        async Task<string> SuccessAsync(CancellationToken ct) => await Task.FromResult("Success");
        async Task<string> FailureAsync(Exception exception, CancellationToken ct) => await Task.FromResult("Failure");
    }

    [Fact(DisplayName = "ValueTask<Result<T>>.MatchAsync of Success should invoke the success delegate (Action<T>, Action<Exception>)")]
    public async Task ValueTaskResultOfTMatchAsyncOfSuccessShouldInvokeSuccessDelegateActionTActionException()
    {
        // Given
        Result<int> result = Result<int>.Success(42);
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await ValueTask.FromResult(result).MatchAsync(Success, Failure, token: TestContext.Current.CancellationToken);

        // Then
        Assert.True(successCalled);
        Assert.False(failureCalled);

        return;
        void Success(int value) => successCalled = value == 42;
        void Failure(Exception exception) => failureCalled = true;
    }

    [Fact(DisplayName = "ValueTask<Result<T>>.MatchAsync of Failure should invoke the failure delegate (Action<T>, Action<Exception>)")]
    public async Task ValueTaskResultOfTMatchAsyncOfFailureShouldInvokeFailureDelegateActionTActionException()
    {
        // Given
        Result<int> result = Result<int>.Failure(FailureException);
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await ValueTask.FromResult(result).MatchAsync(Success, Failure, token: TestContext.Current.CancellationToken);

        // Then
        Assert.False(successCalled);
        Assert.True(failureCalled);

        return;
        void Success(int value) => successCalled = true;
        void Failure(Exception exception) => failureCalled = true;
    }

    [Fact(DisplayName = "ValueTask<Result<T>>.MatchAsync of Success should invoke the success delegate (Func<T, Task>, Action<Exception>)")]
    public async Task ValueTaskResultOfTMatchAsyncOfSuccessShouldInvokeSuccessDelegateFuncTTaskActionException()
    {
        // Given
        Result<int> result = Result<int>.Success(42);
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await ValueTask.FromResult(result).MatchAsync(SuccessAsync, Failure, token: TestContext.Current.CancellationToken);

        // Then
        Assert.True(successCalled);
        Assert.False(failureCalled);

        return;
        async Task SuccessAsync(int value) => await Task.Run(() => successCalled = value == 42);
        void Failure(Exception exception) => failureCalled = true;
    }

    [Fact(DisplayName = "ValueTask<Result<T>>.MatchAsync of Failure should invoke the failure delegate (Func<T, Task>, Action<Exception>)")]
    public async Task ValueTaskResultOfTMatchAsyncOfFailureShouldInvokeFailureDelegateFuncTTaskActionException()
    {
        // Given
        Result<int> result = Result<int>.Failure(FailureException);
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await ValueTask.FromResult(result).MatchAsync(SuccessAsync, Failure, token: TestContext.Current.CancellationToken);

        // Then
        Assert.False(successCalled);
        Assert.True(failureCalled);

        return;
        async Task SuccessAsync(int value) => await Task.Run(() => successCalled = true);
        void Failure(Exception exception) => failureCalled = true;
    }

    [Fact(DisplayName = "ValueTask<Result<T>>.MatchAsync of Success should invoke the success delegate (Func<T, CancellationToken, Task>, Action<Exception>)")]
    public async Task ValueTaskResultOfTMatchAsyncOfSuccessShouldInvokeSuccessDelegateFuncTCancellationTokenTaskActionException()
    {
        // Given
        Result<int> result = Result<int>.Success(42);
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await ValueTask.FromResult(result).MatchAsync(SuccessAsync, Failure, token: TestContext.Current.CancellationToken);

        // Then
        Assert.True(successCalled);
        Assert.False(failureCalled);

        return;
        async Task SuccessAsync(int value, CancellationToken ct) => await Task.Run(() => successCalled = value == 42, ct);
        void Failure(Exception exception) => failureCalled = true;
    }

    [Fact(DisplayName = "ValueTask<Result<T>>.MatchAsync of Failure should invoke the failure delegate (Func<T, CancellationToken, Task>, Action<Exception>)")]
    public async Task ValueTaskResultOfTMatchAsyncOfFailureShouldInvokeFailureDelegateFuncTCancellationTokenTaskActionException()
    {
        // Given
        Result<int> result = Result<int>.Failure(FailureException);
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await ValueTask.FromResult(result).MatchAsync(SuccessAsync, Failure, token: TestContext.Current.CancellationToken);

        // Then
        Assert.False(successCalled);
        Assert.True(failureCalled);

        return;
        async Task SuccessAsync(int value, CancellationToken ct) => await Task.Run(() => successCalled = true, ct);
        void Failure(Exception exception) => failureCalled = true;
    }

    [Fact(DisplayName = "ValueTask<Result<T>>.MatchAsync of Success should invoke the success delegate (Action<T>, Func<Exception, Task>)")]
    public async Task ValueTaskResultOfTMatchAsyncOfSuccessShouldInvokeSuccessDelegateActionTFuncExceptionTask()
    {
        // Given
        Result<int> result = Result<int>.Success(42);
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await ValueTask.FromResult(result).MatchAsync(Success, FailureAsync, token: TestContext.Current.CancellationToken);

        // Then
        Assert.True(successCalled);
        Assert.False(failureCalled);

        return;
        void Success(int value) => successCalled = value == 42;
        async Task FailureAsync(Exception exception) => await Task.Run(() => failureCalled = true);
    }

    [Fact(DisplayName = "ValueTask<Result<T>>.MatchAsync of Failure should invoke the failure delegate (Action<T>, Func<Exception, Task>)")]
    public async Task ValueTaskResultOfTMatchAsyncOfFailureShouldInvokeFailureDelegateActionTFuncExceptionTask()
    {
        // Given
        Result<int> result = Result<int>.Failure(FailureException);
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await ValueTask.FromResult(result).MatchAsync(Success, FailureAsync, token: TestContext.Current.CancellationToken);

        // Then
        Assert.False(successCalled);
        Assert.True(failureCalled);

        return;
        void Success(int value) => successCalled = true;
        async Task FailureAsync(Exception exception) => await Task.Run(() => failureCalled = true);
    }

    [Fact(DisplayName = "ValueTask<Result<T>>.MatchAsync of Success should invoke the success delegate (Action<T>, Func<Exception, CancellationToken, Task>)")]
    public async Task ValueTaskResultOfTMatchAsyncOfSuccessShouldInvokeSuccessDelegateActionTFuncExceptionCancellationTokenTask()
    {
        // Given
        Result<int> result = Result<int>.Success(42);
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await ValueTask.FromResult(result).MatchAsync(Success, FailureAsync, token: TestContext.Current.CancellationToken);

        // Then
        Assert.True(successCalled);
        Assert.False(failureCalled);

        return;
        void Success(int value) => successCalled = value == 42;
        async Task FailureAsync(Exception exception, CancellationToken ct) => await Task.Run(() => failureCalled = true, ct);
    }

    [Fact(DisplayName = "ValueTask<Result<T>>.MatchAsync of Failure should invoke the failure delegate (Action<T>, Func<Exception, CancellationToken, Task>)")]
    public async Task ValueTaskResultOfTMatchAsyncOfFailureShouldInvokeFailureDelegateActionTFuncExceptionCancellationTokenTask()
    {
        // Given
        Result<int> result = Result<int>.Failure(FailureException);
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await ValueTask.FromResult(result).MatchAsync(Success, FailureAsync, token: TestContext.Current.CancellationToken);

        // Then
        Assert.False(successCalled);
        Assert.True(failureCalled);

        return;
        void Success(int value) => successCalled = true;
        async Task FailureAsync(Exception exception, CancellationToken ct) => await Task.Run(() => failureCalled = true, ct);
    }

    [Fact(DisplayName = "ValueTask<Result<T>>.MatchAsync of Success should invoke the success delegate (Func<T, Task>, Func<Exception, Task>)")]
    public async Task ValueTaskResultOfTMatchAsyncOfSuccessShouldInvokeSuccessDelegateFuncTTaskFuncExceptionTask()
    {
        // Given
        Result<int> result = Result<int>.Success(42);
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await ValueTask.FromResult(result).MatchAsync(SuccessAsync, FailureAsync, token: TestContext.Current.CancellationToken);

        // Then
        Assert.True(successCalled);
        Assert.False(failureCalled);

        return;
        async Task SuccessAsync(int value) => await Task.Run(() => successCalled = value == 42);
        async Task FailureAsync(Exception exception) => await Task.Run(() => failureCalled = true);
    }

    [Fact(DisplayName = "ValueTask<Result<T>>.MatchAsync of Failure should invoke the failure delegate (Func<T, Task>, Func<Exception, Task>)")]
    public async Task ValueTaskResultOfTMatchAsyncOfFailureShouldInvokeFailureDelegateFuncTTaskFuncExceptionTask()
    {
        // Given
        Result<int> result = Result<int>.Failure(FailureException);
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await ValueTask.FromResult(result).MatchAsync(SuccessAsync, FailureAsync, token: TestContext.Current.CancellationToken);

        // Then
        Assert.False(successCalled);
        Assert.True(failureCalled);

        return;
        async Task SuccessAsync(int value) => await Task.Run(() => successCalled = true);
        async Task FailureAsync(Exception exception) => await Task.Run(() => failureCalled = true);
    }

    [Fact(DisplayName = "ValueTask<Result<T>>.MatchAsync of Success should invoke the success delegate (Func<T, CancellationToken, Task>, Func<Exception, CancellationToken, Task>)")]
    public async Task ValueTaskResultOfTMatchAsyncOfSuccessShouldInvokeSuccessDelegateFuncTCancellationTokenTaskFuncExceptionCancellationTokenTask()
    {
        // Given
        Result<int> result = Result<int>.Success(42);
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await ValueTask.FromResult(result).MatchAsync(SuccessAsync, FailureAsync, token: TestContext.Current.CancellationToken);

        // Then
        Assert.True(successCalled);
        Assert.False(failureCalled);

        return;
        async Task SuccessAsync(int value, CancellationToken ct) => await Task.Run(() => successCalled = value == 42, ct);
        async Task FailureAsync(Exception exception, CancellationToken ct) => await Task.Run(() => failureCalled = true, ct);
    }

    [Fact(DisplayName = "ValueTask<Result<T>>.MatchAsync of Failure should invoke the failure delegate (Func<T, CancellationToken, Task>, Func<Exception, CancellationToken, Task>)")]
    public async Task ValueTaskResultOfTMatchAsyncOfFailureShouldInvokeFailureDelegateFuncTCancellationTokenTaskFuncExceptionCancellationTokenTask()
    {
        // Given
        Result<int> result = Result<int>.Failure(FailureException);
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await ValueTask.FromResult(result).MatchAsync(SuccessAsync, FailureAsync, token: TestContext.Current.CancellationToken);

        // Then
        Assert.False(successCalled);
        Assert.True(failureCalled);

        return;
        async Task SuccessAsync(int value, CancellationToken ct) => await Task.Run(() => successCalled = true, ct);
        async Task FailureAsync(Exception exception, CancellationToken ct) => await Task.Run(() => failureCalled = true, ct);
    }

    [Fact(DisplayName = "ValueTask<Result<T>>.MatchAsync<TResult> of Success should return result from success delegate (Func<T, TResult>, Func<Exception, TResult>)")]
    public async Task ValueTaskResultOfTMatchAsyncOfSuccessShouldReturnResultFromSuccessDelegateFuncTResultFuncExceptionResult()
    {
        // Given
        Result<int> result = Result<int>.Success(42);
        const string expected = "Success";

        // When
        string actual = await ValueTask.FromResult(result).MatchAsync(Success, Failure, token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        string Success(int value) => value == 42 ? "Success" : "Failure";
        string Failure(Exception exception) => "Failure";
    }

    [Fact(DisplayName = "ValueTask<Result<T>>.MatchAsync<TResult> of Failure should return result from failure delegate (Func<T, TResult>, Func<Exception, TResult>)")]
    public async Task ValueTaskResultOfTMatchAsyncOfFailureShouldReturnResultFromFailureDelegateFuncTResultFuncExceptionResult()
    {
        // Given
        Result<int> result = Result<int>.Failure(FailureException);
        const string expected = "Failure";

        // When
        string actual = await ValueTask.FromResult(result).MatchAsync(Success, Failure, token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        string Success(int value) => "Success";
        string Failure(Exception exception) => "Failure";
    }

    [Fact(DisplayName = "ValueTask<Result<T>>.MatchAsync<TResult> of Success should return result from success delegate (Func<T, Task<TResult>>, Func<Exception, TResult>)")]
    public async Task ValueTaskResultOfTMatchAsyncOfSuccessShouldReturnResultFromSuccessDelegateFuncTTaskResultFuncExceptionResult()
    {
        // Given
        Result<int> result = Result<int>.Success(42);
        const string expected = "Success";

        // When
        string actual = await ValueTask.FromResult(result).MatchAsync(SuccessAsync, Failure, token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        async Task<string> SuccessAsync(int value) => await Task.FromResult(value == 42 ? "Success" : "Failure");
        string Failure(Exception exception) => "Failure";
    }

    [Fact(DisplayName = "ValueTask<Result<T>>.MatchAsync<TResult> of Failure should return result from failure delegate (Func<T, Task<TResult>>, Func<Exception, TResult>)")]
    public async Task ValueTaskResultOfTMatchAsyncOfFailureShouldReturnResultFromFailureDelegateFuncTTaskResultFuncExceptionResult()
    {
        // Given
        Result<int> result = Result<int>.Failure(FailureException);
        const string expected = "Failure";

        // When
        string actual = await ValueTask.FromResult(result).MatchAsync(SuccessAsync, Failure, token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        async Task<string> SuccessAsync(int value) => await Task.FromResult("Success");
        string Failure(Exception exception) => "Failure";
    }

    [Fact(DisplayName = "ValueTask<Result<T>>.MatchAsync<TResult> of Success should return result from success delegate (Func<T, CancellationToken, Task<TResult>>, Func<Exception, TResult>)")]
    public async Task ValueTaskResultOfTMatchAsyncOfSuccessShouldReturnResultFromSuccessDelegateFuncTCancellationTokenTaskResultFuncExceptionResult()
    {
        // Given
        Result<int> result = Result<int>.Success(42);
        const string expected = "Success";

        // When
        string actual = await ValueTask.FromResult(result).MatchAsync(SuccessAsync, Failure, token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        async Task<string> SuccessAsync(int value, CancellationToken ct) => await Task.FromResult(value == 42 ? "Success" : "Failure");
        string Failure(Exception exception) => "Failure";
    }

    [Fact(DisplayName = "ValueTask<Result<T>>.MatchAsync<TResult> of Failure should return result from failure delegate (Func<T, CancellationToken, Task<TResult>>, Func<Exception, TResult>)")]
    public async Task ValueTaskResultOfTMatchAsyncOfFailureShouldReturnResultFromFailureDelegateFuncTCancellationTokenTaskResultFuncExceptionResult()
    {
        // Given
        Result<int> result = Result<int>.Failure(FailureException);
        const string expected = "Failure";

        // When
        string actual = await ValueTask.FromResult(result).MatchAsync(SuccessAsync, Failure, token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        async Task<string> SuccessAsync(int value, CancellationToken ct) => await Task.FromResult("Success");
        string Failure(Exception exception) => "Failure";
    }

    [Fact(DisplayName = "ValueTask<Result<T>>.MatchAsync<TResult> of Success should return result from success delegate (Func<T, TResult>, Func<Exception, Task<TResult>>>)")]
    public async Task ValueTaskResultOfTMatchAsyncOfSuccessShouldReturnResultFromSuccessDelegateFuncTResultFuncExceptionTaskResult()
    {
        // Given
        Result<int> result = Result<int>.Success(42);
        const string expected = "Success";

        // When
        string actual = await ValueTask.FromResult(result).MatchAsync(Success, FailureAsync, token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        string Success(int value) => value == 42 ? "Success" : "Failure";
        async Task<string> FailureAsync(Exception exception) => await Task.FromResult("Failure");
    }

    [Fact(DisplayName = "ValueTask<Result<T>>.MatchAsync<TResult> of Failure should return result from failure delegate (Func<T, TResult>, Func<Exception, Task<TResult>>>)")]
    public async Task ValueTaskResultOfTMatchAsyncOfFailureShouldReturnResultFromFailureDelegateFuncTResultFuncExceptionTaskResult()
    {
        // Given
        Result<int> result = Result<int>.Failure(FailureException);
        const string expected = "Failure";

        // When
        string actual = await ValueTask.FromResult(result).MatchAsync(Success, FailureAsync, token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        string Success(int value) => "Success";
        async Task<string> FailureAsync(Exception exception) => await Task.FromResult("Failure");
    }

    [Fact(DisplayName = "ValueTask<Result<T>>.MatchAsync<TResult> of Success should return result from success delegate (Func<T, TResult>, Func<Exception, CancellationToken, Task<TResult>>>)")]
    public async Task ValueTaskResultOfTMatchAsyncOfSuccessShouldReturnResultFromSuccessDelegateFuncTResultFuncExceptionCancellationTokenTaskResult()
    {
        // Given
        Result<int> result = Result<int>.Success(42);
        const string expected = "Success";

        // When
        string actual = await ValueTask.FromResult(result).MatchAsync(Success, FailureAsync, token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        string Success(int value) => value == 42 ? "Success" : "Failure";
        async Task<string> FailureAsync(Exception exception, CancellationToken ct) => await Task.FromResult("Failure");
    }

    [Fact(DisplayName = "ValueTask<Result<T>>.MatchAsync<TResult> of Failure should return result from failure delegate (Func<T, TResult>, Func<Exception, CancellationToken, Task<TResult>>>)")]
    public async Task ValueTaskResultOfTMatchAsyncOfFailureShouldReturnResultFromFailureDelegateFuncTResultFuncExceptionCancellationTokenTaskResult()
    {
        // Given
        Result<int> result = Result<int>.Failure(FailureException);
        const string expected = "Failure";

        // When
        string actual = await ValueTask.FromResult(result).MatchAsync(Success, FailureAsync, token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        string Success(int value) => "Success";
        async Task<string> FailureAsync(Exception exception, CancellationToken ct) => await Task.FromResult("Failure");
    }

    [Fact(DisplayName = "ValueTask<Result<T>>.MatchAsync<TResult> of Success should return result from success delegate (Func<T, Task<TResult>>, Func<Exception, Task<TResult>>>)")]
    public async Task ValueTaskResultOfTMatchAsyncOfSuccessShouldReturnResultFromSuccessDelegateFuncTTaskResultFuncExceptionTaskResult()
    {
        // Given
        Result<int> result = Result<int>.Success(42);
        const string expected = "Success";

        // When
        string actual = await ValueTask.FromResult(result).MatchAsync(SuccessAsync, FailureAsync, token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        async Task<string> SuccessAsync(int value) => await Task.FromResult(value == 42 ? "Success" : "Failure");
        async Task<string> FailureAsync(Exception exception) => await Task.FromResult("Failure");
    }

    [Fact(DisplayName = "ValueTask<Result<T>>.MatchAsync<TResult> of Failure should return result from failure delegate (Func<T, Task<TResult>>, Func<Exception, Task<TResult>>>)")]
    public async Task ValueTaskResultOfTMatchAsyncOfFailureShouldReturnResultFromFailureDelegateFuncTTaskResultFuncExceptionTaskResult()
    {
        // Given
        Result<int> result = Result<int>.Failure(FailureException);
        const string expected = "Failure";

        // When
        string actual = await ValueTask.FromResult(result).MatchAsync(SuccessAsync, FailureAsync, token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        async Task<string> SuccessAsync(int value) => await Task.FromResult("Success");
        async Task<string> FailureAsync(Exception exception) => await Task.FromResult("Failure");
    }

    [Fact(DisplayName = "ValueTask<Result<T>>.MatchAsync<TResult> of Success should return result from success delegate (Func<T, CancellationToken, Task<TResult>>, Func<Exception, CancellationToken, Task<TResult>>>)")]
    public async Task ValueTaskResultOfTMatchAsyncOfSuccessShouldReturnResultFromSuccessDelegateFuncTCancellationTokenTaskResultFuncExceptionCancellationTokenTaskResult()
    {
        // Given
        Result<int> result = Result<int>.Success(42);
        const string expected = "Success";

        // When
        string actual = await ValueTask.FromResult(result).MatchAsync(SuccessAsync, FailureAsync, token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        async Task<string> SuccessAsync(int value, CancellationToken ct) => await Task.FromResult(value == 42 ? "Success" : "Failure");
        async Task<string> FailureAsync(Exception exception, CancellationToken ct) => await Task.FromResult("Failure");
    }

    [Fact(DisplayName = "ValueTask<Result<T>>.MatchAsync<TResult> of Failure should return result from failure delegate (Func<T, CancellationToken, Task<TResult>>, Func<Exception, CancellationToken, Task<TResult>>>)")]
    public async Task ValueTaskResultOfTMatchAsyncOfFailureShouldReturnResultFromFailureDelegateFuncTCancellationTokenTaskResultFuncExceptionCancellationTokenTaskResult()
    {
        // Given
        Result<int> result = Result<int>.Failure(FailureException);
        const string expected = "Failure";

        // When
        string actual = await ValueTask.FromResult(result).MatchAsync(SuccessAsync, FailureAsync, token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        async Task<string> SuccessAsync(int value, CancellationToken ct) => await Task.FromResult("Success");
        async Task<string> FailureAsync(Exception exception, CancellationToken ct) => await Task.FromResult("Failure");
    }

    [Fact(DisplayName = "ValueTask<Result>.SelectAsync of Success should invoke the selector delegate (Action)")]
    public async Task ValueTaskResultSelectAsyncOfSuccessShouldInvokeSelectorDelegateAction()
    {
        // Given
        Result result = Result.Success();
        bool selectorInvoked = false;

        // When
        Result actual = await ValueTask.FromResult(result).SelectAsync(Selector, token: TestContext.Current.CancellationToken);

        // Then
        Assert.IsType<Success>(actual);
        Assert.True(selectorInvoked);

        return;
        void Selector() => selectorInvoked = true;
    }

    [Fact(DisplayName = "ValueTask<Result>.SelectAsync of Failure should not invoke the selector delegate (Action)")]
    public async Task ValueTaskResultSelectAsyncOfFailureShouldNotInvokeSelectorDelegateAction()
    {
        // Given
        Result result = Result.Failure(FailureException);
        bool selectorInvoked = false;

        // When
        Result actual = await ValueTask.FromResult(result).SelectAsync(Selector, token: TestContext.Current.CancellationToken);

        // Then
        Assert.IsType<Failure>(actual);
        Assert.False(selectorInvoked);

        return;
        void Selector() => selectorInvoked = true;
    }

    [Fact(DisplayName = "ValueTask<Result>.SelectAsync of Success should invoke the selector delegate (Func<Task>)")]
    public async Task ValueTaskResultSelectAsyncOfSuccessShouldInvokeSelectorDelegateFuncTask()
    {
        // Given
        Result result = Result.Success();
        bool selectorInvoked = false;

        // When
        Result actual = await ValueTask.FromResult(result).SelectAsync(SelectorAsync, token: TestContext.Current.CancellationToken);

        // Then
        Assert.IsType<Success>(actual);
        Assert.True(selectorInvoked);

        return;
        async Task SelectorAsync() => await Task.Run(() => selectorInvoked = true);
    }

    [Fact(DisplayName = "ValueTask<Result>.SelectAsync of Failure should not invoke the selector delegate (Func<Task>)")]
    public async Task ValueTaskResultSelectAsyncOfFailureShouldNotInvokeSelectorDelegateFuncTask()
    {
        // Given
        Result result = Result.Failure(FailureException);
        bool selectorInvoked = false;

        // When
        Result actual = await ValueTask.FromResult(result).SelectAsync(SelectorAsync, token: TestContext.Current.CancellationToken);

        // Then
        Assert.IsType<Failure>(actual);
        Assert.False(selectorInvoked);

        return;
        async Task SelectorAsync() => await Task.Run(() => selectorInvoked = true);
    }

    [Fact(DisplayName = "ValueTask<Result>.SelectAsync of Success should invoke the selector delegate (Func<CancellationToken, Task>)")]
    public async Task ValueTaskResultSelectAsyncOfSuccessShouldInvokeSelectorDelegateFuncCancellationTokenTask()
    {
        // Given
        Result result = Result.Success();
        bool selectorInvoked = false;

        // When
        Result actual = await ValueTask.FromResult(result).SelectAsync(SelectorAsync, token: TestContext.Current.CancellationToken);

        // Then
        Assert.IsType<Success>(actual);
        Assert.True(selectorInvoked);

        return;
        async Task SelectorAsync(CancellationToken ct) => await Task.Run(() => selectorInvoked = true, ct);
    }

    [Fact(DisplayName = "ValueTask<Result>.SelectAsync of Failure should not invoke the selector delegate (Func<CancellationToken, Task>)")]
    public async Task ValueTaskResultSelectAsyncOfFailureShouldNotInvokeSelectorDelegateFuncCancellationTokenTask()
    {
        // Given
        Result result = Result.Failure(FailureException);
        bool selectorInvoked = false;

        // When
        Result actual = await ValueTask.FromResult(result).SelectAsync(SelectorAsync, token: TestContext.Current.CancellationToken);

        // Then
        Assert.IsType<Failure>(actual);
        Assert.False(selectorInvoked);

        return;
        async Task SelectorAsync(CancellationToken ct) => await Task.Run(() => selectorInvoked = true, ct);
    }

    [Fact(DisplayName = "ValueTask<Result>.SelectAsync<TResult> of Success should invoke the selector delegate (Func<TResult>)")]
    public async Task ValueTaskResultSelectAsyncOfSuccessShouldInvokeSelectorDelegateFuncTResult()
    {
        // Given
        Result result = Result.Success();
        bool selectorInvoked = false;

        // When
        Result<string> actual = await ValueTask.FromResult(result).SelectAsync(Selector, token: TestContext.Current.CancellationToken);

        // Then
        Assert.IsType<Success<string>>(actual);
        Assert.True(selectorInvoked);
        Assert.Equal("Success Result", actual.GetValueOrThrow());

        return;

        string Selector()
        {
            selectorInvoked = true;
            return "Success Result";
        }
    }

    [Fact(DisplayName = "ValueTask<Result>.SelectAsync<TResult> of Failure should not invoke the selector delegate (Func<TResult>)")]
    public async Task ValueTaskResultSelectAsyncOfFailureShouldNotInvokeSelectorDelegateFuncTResult()
    {
        // Given
        Result result = Result.Failure(FailureException);
        bool selectorInvoked = false;

        // When
        Result<string> actual = await ValueTask.FromResult(result).SelectAsync(Selector, token: TestContext.Current.CancellationToken);

        // Then
        Assert.IsType<Failure<string>>(actual);
        Assert.False(selectorInvoked);

        return;

        string Selector()
        {
            selectorInvoked = true;
            return "Success Result";
        }
    }

    [Fact(DisplayName = "ValueTask<Result>.SelectAsync<TResult> of Success should invoke the selector delegate (Func<Task<TResult>>)")]
    public async Task ValueTaskResultSelectAsyncOfSuccessShouldInvokeSelectorDelegateFuncTaskTResult()
    {
        // Given
        Result result = Result.Success();
        bool selectorInvoked = false;

        // When
        Result<string> actual = await ValueTask.FromResult(result).SelectAsync(SelectorAsync, token: TestContext.Current.CancellationToken);

        // Then
        Assert.IsType<Success<string>>(actual);
        Assert.True(selectorInvoked);
        Assert.Equal("Success Result", actual.GetValueOrThrow());

        return;

        async Task<string> SelectorAsync()
        {
            selectorInvoked = true;
            return await Task.FromResult("Success Result");
        }
    }

    [Fact(DisplayName = "ValueTask<Result>.SelectAsync<TResult> of Failure should not invoke the selector delegate (Func<Task<TResult>>)")]
    public async Task ValueTaskResultSelectAsyncOfFailureShouldNotInvokeSelectorDelegateFuncTaskTResult()
    {
        // Given
        Result result = Result.Failure(FailureException);
        bool selectorInvoked = false;

        // When
        Result<string> actual = await ValueTask.FromResult(result).SelectAsync(SelectorAsync, token: TestContext.Current.CancellationToken);

        // Then
        Assert.IsType<Failure<string>>(actual);
        Assert.False(selectorInvoked);

        return;

        async Task<string> SelectorAsync()
        {
            selectorInvoked = true;
            return await Task.FromResult("Success Result");
        }
    }

    [Fact(DisplayName = "ValueTask<Result>.SelectAsync<TResult> of Success should invoke the selector delegate (Func<CancellationToken, Task<TResult>>)")]
    public async Task ValueTaskResultSelectAsyncOfSuccessShouldInvokeSelectorDelegateFuncCancellationTokenTaskTResult()
    {
        // Given
        Result result = Result.Success();
        bool selectorInvoked = false;

        // When
        Result<string> actual = await ValueTask.FromResult(result).SelectAsync(SelectorAsync, token: TestContext.Current.CancellationToken);

        // Then
        Assert.IsType<Success<string>>(actual);
        Assert.True(selectorInvoked);
        Assert.Equal("Success Result", actual.GetValueOrThrow());

        return;

        async Task<string> SelectorAsync(CancellationToken ct)
        {
            selectorInvoked = true;
            return await Task.FromResult("Success Result");
        }
    }

    [Fact(DisplayName = "ValueTask<Result>.SelectAsync<TResult> of Failure should not invoke the selector delegate (Func<CancellationToken, Task<TResult>>)")]
    public async Task ValueTaskResultSelectAsyncOfFailureShouldNotInvokeSelectorDelegateFuncCancellationTokenTaskTResult()
    {
        // Given
        Result result = Result.Failure(FailureException);
        bool selectorInvoked = false;

        // When
        Result<string> actual = await ValueTask.FromResult(result).SelectAsync(SelectorAsync, token: TestContext.Current.CancellationToken);

        // Then
        Assert.IsType<Failure<string>>(actual);
        Assert.False(selectorInvoked);

        return;

        async Task<string> SelectorAsync(CancellationToken ct)
        {
            selectorInvoked = true;
            return await Task.FromResult("Success Result");
        }
    }

    [Fact(DisplayName = "ValueTask<Result<T>>.SelectAsync of Success should invoke the selector delegate (Action<T>)")]
    public async Task ValueTaskResultOfTSelectAsyncOfSuccessShouldInvokeSelectorDelegateActionT()
    {
        // Given
        Result<int> result = Result<int>.Success(42);
        bool selectorInvoked = false;

        // When
        Result actual = await ValueTask.FromResult(result).SelectAsync(Selector, token: TestContext.Current.CancellationToken);

        // Then
        Assert.IsType<Success>(actual);
        Assert.True(selectorInvoked);

        return;

        void Selector(int value)
        {
            selectorInvoked = value == 42;
        }
    }

    [Fact(DisplayName = "ValueTask<Result<T>>.SelectAsync of Failure should not invoke the selector delegate (Action<T>)")]
    public async Task ValueTaskResultOfTSelectAsyncOfFailureShouldNotInvokeSelectorDelegateActionT()
    {
        // Given
        Result<int> result = Result<int>.Failure(FailureException);
        bool selectorInvoked = false;

        // When
        Result actual = await ValueTask.FromResult(result).SelectAsync(Selector, token: TestContext.Current.CancellationToken);

        // Then
        Assert.IsType<Failure>(actual);
        Assert.False(selectorInvoked);

        return;

        void Selector(int value)
        {
            selectorInvoked = true;
        }
    }

    [Fact(DisplayName = "ValueTask<Result<T>>.SelectAsync of Success should invoke the selector delegate (Func<T, Task>)")]
    public async Task ValueTaskResultOfTSelectAsyncOfSuccessShouldInvokeSelectorDelegateFuncTTask()
    {
        // Given
        Result<int> result = Result<int>.Success(42);
        bool selectorInvoked = false;

        // When
        Result actual = await ValueTask.FromResult(result).SelectAsync(SelectorAsync, token: TestContext.Current.CancellationToken);

        // Then
        Assert.IsType<Success>(actual);
        Assert.True(selectorInvoked);

        return;

        async Task SelectorAsync(int value)
        {
            selectorInvoked = value == 42;
            await Task.CompletedTask;
        }
    }

    [Fact(DisplayName = "ValueTask<Result<T>>.SelectAsync of Failure should not invoke the selector delegate (Func<T, Task>)")]
    public async Task ValueTaskResultOfTSelectAsyncOfFailureShouldNotInvokeSelectorDelegateFuncTTask()
    {
        // Given
        Result<int> result = Result<int>.Failure(FailureException);
        bool selectorInvoked = false;

        // When
        Result actual = await ValueTask.FromResult(result).SelectAsync(SelectorAsync, token: TestContext.Current.CancellationToken);

        // Then
        Assert.IsType<Failure>(actual);
        Assert.False(selectorInvoked);

        return;

        async Task SelectorAsync(int value)
        {
            selectorInvoked = true;
            await Task.CompletedTask;
        }
    }

    [Fact(DisplayName = "ValueTask<Result<T>>.SelectAsync of Success should invoke the selector delegate (Func<T, CancellationToken, Task>)")]
    public async Task ValueTaskResultOfTSelectAsyncOfSuccessShouldInvokeSelectorDelegateFuncTCancellationTokenTask()
    {
        // Given
        Result<int> result = Result<int>.Success(42);
        bool selectorInvoked = false;

        // When
        Result actual = await ValueTask.FromResult(result).SelectAsync(SelectorAsync, token: TestContext.Current.CancellationToken);

        // Then
        Assert.IsType<Success>(actual);
        Assert.True(selectorInvoked);

        return;

        async Task SelectorAsync(int value, CancellationToken ct)
        {
            selectorInvoked = value == 42;
            await Task.CompletedTask;
        }
    }

    [Fact(DisplayName = "ValueTask<Result<T>>.SelectAsync of Failure should not invoke the selector delegate (Func<T, CancellationToken, Task>)")]
    public async Task ValueTaskResultOfTSelectAsyncOfFailureShouldNotInvokeSelectorDelegateFuncTCancellationTokenTask()
    {
        // Given
        Result<int> result = Result<int>.Failure(FailureException);
        bool selectorInvoked = false;

        // When
        Result actual = await ValueTask.FromResult(result).SelectAsync(SelectorAsync, token: TestContext.Current.CancellationToken);

        // Then
        Assert.IsType<Failure>(actual);
        Assert.False(selectorInvoked);

        return;

        async Task SelectorAsync(int value, CancellationToken ct)
        {
            selectorInvoked = true;
            await Task.CompletedTask;
        }
    }

    [Fact(DisplayName = "ValueTask<Result<T>>.SelectAsync<TResult> of Success should invoke the selector delegate (Func<T, TResult>)")]
    public async Task ValueTaskResultOfTSelectAsyncOfSuccessShouldInvokeSelectorDelegateFuncTResult()
    {
        // Given
        Result<int> result = Result<int>.Success(42);
        bool selectorInvoked = false;

        // When
        Result<string> actual = await ValueTask.FromResult(result).SelectAsync(Selector, token: TestContext.Current.CancellationToken);

        // Then
        Assert.IsType<Success<string>>(actual);
        Assert.True(selectorInvoked);
        Assert.Equal("Value is 42", actual.GetValueOrThrow());

        return;

        string Selector(int value)
        {
            selectorInvoked = value == 42;
            return $"Value is {value}";
        }
    }

    [Fact(DisplayName = "ValueTask<Result<T>>.SelectAsync<TResult> of Failure should not invoke the selector delegate (Func<T, TResult>)")]
    public async Task ValueTaskResultOfTSelectAsyncOfFailureShouldNotInvokeSelectorDelegateFuncTResult()
    {
        // Given
        Result<int> result = Result<int>.Failure(FailureException);
        bool selectorInvoked = false;

        // When
        Result<string> actual = await ValueTask.FromResult(result).SelectAsync(Selector, token: TestContext.Current.CancellationToken);

        // Then
        Assert.IsType<Failure<string>>(actual);
        Assert.False(selectorInvoked);

        return;

        string Selector(int value)
        {
            selectorInvoked = true;
            return $"Value is {value}";
        }
    }

    [Fact(DisplayName = "ValueTask<Result<T>>.SelectAsync<TResult> of Success should invoke the selector delegate (Func<T, Task<TResult>>)")]
    public async Task ValueTaskResultOfTSelectAsyncOfSuccessShouldInvokeSelectorDelegateFuncTTaskResult()
    {
        // Given
        Result<int> result = Result<int>.Success(42);
        bool selectorInvoked = false;

        // When
        Result<string> actual = await ValueTask.FromResult(result).SelectAsync(SelectorAsync, token: TestContext.Current.CancellationToken);

        // Then
        Assert.IsType<Success<string>>(actual);
        Assert.True(selectorInvoked);
        Assert.Equal("Value is 42", actual.GetValueOrThrow());

        return;

        async Task<string> SelectorAsync(int value)
        {
            selectorInvoked = value == 42;
            return await Task.FromResult($"Value is {value}");
        }
    }

    [Fact(DisplayName = "ValueTask<Result<T>>.SelectAsync<TResult> of Failure should not invoke the selector delegate (Func<T, Task<TResult>>)")]
    public async Task ValueTaskResultOfTSelectAsyncOfFailureShouldNotInvokeSelectorDelegateFuncTTaskResult()
    {
        // Given
        Result<int> result = Result<int>.Failure(FailureException);
        bool selectorInvoked = false;

        // When
        Result<string> actual = await ValueTask.FromResult(result).SelectAsync(SelectorAsync, token: TestContext.Current.CancellationToken);

        // Then
        Assert.IsType<Failure<string>>(actual);
        Assert.False(selectorInvoked);

        return;

        async Task<string> SelectorAsync(int value)
        {
            selectorInvoked = true;
            return await Task.FromResult($"Value is {value}");
        }
    }

    [Fact(DisplayName = "ValueTask<Result<T>>.SelectAsync<TResult> of Success should invoke the selector delegate (Func<T, CancellationToken, Task<TResult>>)")]
    public async Task ValueTaskResultOfTSelectAsyncOfSuccessShouldInvokeSelectorDelegateFuncTCancellationTokenTaskResult()
    {
        // Given
        Result<int> result = Result<int>.Success(42);
        bool selectorInvoked = false;

        // When
        Result<string> actual = await ValueTask.FromResult(result).SelectAsync(SelectorAsync, token: TestContext.Current.CancellationToken);

        // Then
        Assert.IsType<Success<string>>(actual);
        Assert.True(selectorInvoked);
        Assert.Equal("Value is 42", actual.GetValueOrThrow());

        return;

        async Task<string> SelectorAsync(int value, CancellationToken ct)
        {
            selectorInvoked = value == 42;
            return await Task.FromResult($"Value is {value}");
        }
    }

    [Fact(DisplayName = "ValueTask<Result<T>>.SelectAsync<TResult> of Failure should not invoke the selector delegate (Func<T, CancellationToken, Task<TResult>>)")]
    public async Task ValueTaskResultOfTSelectAsyncOfFailureShouldNotInvokeSelectorDelegateFuncTCancellationTokenTaskResult()
    {
        // Given
        Result<int> result = Result<int>.Failure(FailureException);
        bool selectorInvoked = false;

        // When
        Result<string> actual = await ValueTask.FromResult(result).SelectAsync(SelectorAsync, token: TestContext.Current.CancellationToken);

        // Then
        Assert.IsType<Failure<string>>(actual);
        Assert.False(selectorInvoked);

        return;

        async Task<string> SelectorAsync(int value, CancellationToken ct)
        {
            selectorInvoked = true;
            return await Task.FromResult($"Value is {value}");
        }
    }

    [Fact(DisplayName = "ValueTask<Result>.SelectManyAsync of Success should invoke the selector delegate (Func<Result>) and return its result")]
    public async Task ValueTaskResultSelectManyAsyncOfSuccessShouldInvokeSelectorDelegateFuncResult()
    {
        // Given
        Result result = Result.Success();
        bool selectorInvoked = false;

        // When
        Result actual = await ValueTask.FromResult(result).SelectManyAsync(Selector, token: TestContext.Current.CancellationToken);

        // Then
        Assert.IsType<Success>(actual);
        Assert.True(selectorInvoked);
        return;

        Result Selector()
        {
            selectorInvoked = true;
            return Result.Success();
        }
    }

    [Fact(DisplayName = "ValueTask<Result>.SelectManyAsync of Failure should not invoke the selector delegate (Func<Result>)")]
    public async Task ValueTaskResultSelectManyAsyncOfFailureShouldNotInvokeSelectorDelegateFuncResult()
    {
        // Given
        Result result = Result.Failure(FailureException);
        bool selectorInvoked = false;

        // When
        Result actual = await ValueTask.FromResult(result).SelectManyAsync(Selector, token: TestContext.Current.CancellationToken);

        // Then
        Assert.IsType<Failure>(actual);
        Assert.False(selectorInvoked);
        return;

        Result Selector()
        {
            selectorInvoked = true;
            return Result.Success();
        }
    }

    [Fact(DisplayName = "ValueTask<Result>.SelectManyAsync of Success should invoke the selector delegate (Func<Task<Result>>) and return its result")]
    public async Task ValueTaskResultSelectManyAsyncOfSuccessShouldInvokeSelectorDelegateFuncTaskResult()
    {
        // Given
        Result result = Result.Success();
        bool selectorInvoked = false;

        // When
        Result actual = await ValueTask.FromResult(result).SelectManyAsync(SelectorAsync, token: TestContext.Current.CancellationToken);

        // Then
        Assert.IsType<Success>(actual);
        Assert.True(selectorInvoked);
        return;

        async Task<Result> SelectorAsync()
        {
            selectorInvoked = true;
            return await Task.FromResult(Result.Success());
        }
    }

    [Fact(DisplayName = "ValueTask<Result>.SelectManyAsync of Failure should not invoke the selector delegate (Func<Task<Result>>)")]
    public async Task ValueTaskResultSelectManyAsyncOfFailureShouldNotInvokeSelectorDelegateFuncTaskResult()
    {
        // Given
        Result result = Result.Failure(FailureException);
        bool selectorInvoked = false;

        // When
        Result actual = await ValueTask.FromResult(result).SelectManyAsync(SelectorAsync, token: TestContext.Current.CancellationToken);

        // Then
        Assert.IsType<Failure>(actual);
        Assert.False(selectorInvoked);
        return;

        async Task<Result> SelectorAsync()
        {
            selectorInvoked = true;
            return await Task.FromResult(Result.Success());
        }
    }

    [Fact(DisplayName = "ValueTask<Result>.SelectManyAsync of Success should invoke the selector delegate (Func<CancellationToken, Task<Result>>) and return its result")]
    public async Task ValueTaskResultSelectManyAsyncOfSuccessShouldInvokeSelectorDelegateFuncCancellationTokenTaskResult()
    {
        // Given
        Result result = Result.Success();
        bool selectorInvoked = false;

        // When
        Result actual = await ValueTask.FromResult(result).SelectManyAsync(SelectorAsync, token: TestContext.Current.CancellationToken);

        // Then
        Assert.IsType<Success>(actual);
        Assert.True(selectorInvoked);
        return;

        async Task<Result> SelectorAsync(CancellationToken ct)
        {
            selectorInvoked = true;
            return await Task.FromResult(Result.Success());
        }
    }

    [Fact(DisplayName = "ValueTask<Result>.SelectManyAsync of Failure should not invoke the selector delegate (Func<CancellationToken, Task<Result>>)")]
    public async Task ValueTaskResultSelectManyAsyncOfFailureShouldNotInvokeSelectorDelegateFuncCancellationTokenTaskResult()
    {
        // Given
        Result result = Result.Failure(FailureException);
        bool selectorInvoked = false;

        // When
        Result actual = await ValueTask.FromResult(result).SelectManyAsync(SelectorAsync, token: TestContext.Current.CancellationToken);

        // Then
        Assert.IsType<Failure>(actual);
        Assert.False(selectorInvoked);
        return;

        async Task<Result> SelectorAsync(CancellationToken ct)
        {
            selectorInvoked = true;
            return await Task.FromResult(Result.Success());
        }
    }

    [Fact(DisplayName = "ValueTask<Result>.SelectManyAsync<TResult> of Success should invoke the selector delegate (Func<Result<TResult>>) and return its result")]
    public async Task ValueTaskResultSelectManyAsyncOfSuccessShouldInvokeSelectorDelegateFuncResultTResult()
    {
        // Given
        Result result = Result.Success();
        bool selectorInvoked = false;

        // When
        Result<string> actual = await ValueTask.FromResult(result).SelectManyAsync(Selector, token: TestContext.Current.CancellationToken);

        // Then
        Assert.IsType<Success<string>>(actual);
        Assert.True(selectorInvoked);
        Assert.Equal("Success Result", actual.GetValueOrThrow());

        return;

        Result<string> Selector()
        {
            selectorInvoked = true;
            return Result<string>.Success("Success Result");
        }
    }

    [Fact(DisplayName = "ValueTask<Result>.SelectManyAsync<TResult> of Failure should not invoke the selector delegate (Func<Result<TResult>>)")]
    public async Task ValueTaskResultSelectManyAsyncOfFailureShouldNotInvokeSelectorDelegateFuncResultTResult()
    {
        // Given
        Result result = Result.Failure(FailureException);
        bool selectorInvoked = false;

        // When
        Result<string> actual = await ValueTask.FromResult(result).SelectManyAsync(Selector, token: TestContext.Current.CancellationToken);

        // Then
        Assert.IsType<Failure<string>>(actual);
        Assert.False(selectorInvoked);

        return;

        Result<string> Selector()
        {
            selectorInvoked = true;
            return Result<string>.Success("Success Result");
        }
    }

    [Fact(DisplayName = "ValueTask<Result>.SelectManyAsync<TResult> of Success should invoke the selector delegate (Func<Task<Result<TResult>>>) and return its result")]
    public async Task ValueTaskResultSelectManyAsyncOfSuccessShouldInvokeSelectorDelegateFuncTaskResultTResult()
    {
        // Given
        Result result = Result.Success();
        bool selectorInvoked = false;

        // When
        Result<string> actual = await ValueTask.FromResult(result).SelectManyAsync(SelectorAsync, token: TestContext.Current.CancellationToken);

        // Then
        Assert.IsType<Success<string>>(actual);
        Assert.True(selectorInvoked);
        Assert.Equal("Success Result", actual.GetValueOrThrow());

        return;

        async Task<Result<string>> SelectorAsync()
        {
            selectorInvoked = true;
            return await Task.FromResult(Result<string>.Success("Success Result"));
        }
    }

    [Fact(DisplayName = "ValueTask<Result>.SelectManyAsync<TResult> of Failure should not invoke the selector delegate (Func<Task<Result<TResult>>>)")]
    public async Task ValueTaskResultSelectManyAsyncOfFailureShouldNotInvokeSelectorDelegateFuncTaskResultTResult()
    {
        // Given
        Result result = Result.Failure(FailureException);
        bool selectorInvoked = false;

        // When
        Result<string> actual = await ValueTask.FromResult(result).SelectManyAsync(SelectorAsync, token: TestContext.Current.CancellationToken);

        // Then
        Assert.IsType<Failure<string>>(actual);
        Assert.False(selectorInvoked);

        return;

        async Task<Result<string>> SelectorAsync()
        {
            selectorInvoked = true;
            return await Task.FromResult(Result<string>.Success("Success Result"));
        }
    }

    [Fact(DisplayName = "ValueTask<Result>.SelectManyAsync<TResult> of Success should invoke the selector delegate (Func<CancellationToken, Task<Result<TResult>>>) and return its result")]
    public async Task ValueTaskResultSelectManyAsyncOfSuccessShouldInvokeSelectorDelegateFuncCancellationTokenTaskResultTResult()
    {
        // Given
        Result result = Result.Success();
        bool selectorInvoked = false;

        // When
        Result<string> actual = await ValueTask.FromResult(result).SelectManyAsync(SelectorAsync, token: TestContext.Current.CancellationToken);

        // Then
        Assert.IsType<Success<string>>(actual);
        Assert.True(selectorInvoked);
        Assert.Equal("Success Result", actual.GetValueOrThrow());

        return;

        async Task<Result<string>> SelectorAsync(CancellationToken ct)
        {
            selectorInvoked = true;
            return await Task.FromResult(Result<string>.Success("Success Result"));
        }
    }

    [Fact(DisplayName = "ValueTask<Result>.SelectManyAsync<TResult> of Failure should not invoke the selector delegate (Func<CancellationToken, Task<Result<TResult>>>)")]
    public async Task ValueTaskResultSelectManyAsyncOfFailureShouldNotInvokeSelectorDelegateFuncCancellationTokenTaskResultTResult()
    {
        // Given
        Result result = Result.Failure(FailureException);
        bool selectorInvoked = false;

        // When
        Result<string> actual = await ValueTask.FromResult(result).SelectManyAsync(SelectorAsync, token: TestContext.Current.CancellationToken);

        // Then
        Assert.IsType<Failure<string>>(actual);
        Assert.False(selectorInvoked);

        return;

        async Task<Result<string>> SelectorAsync(CancellationToken ct)
        {
            selectorInvoked = true;
            return await Task.FromResult(Result<string>.Success("Success Result"));
        }
    }

    [Fact(DisplayName = "ValueTask<Result<T>>.SelectManyAsync of Success should invoke the selector delegate (Func<T, Result>) and return its result")]
    public async Task ValueTaskResultOfTSelectManyAsyncOfSuccessShouldInvokeSelectorDelegateFuncTResult()
    {
        // Given
        Result<int> result = Result<int>.Success(42);
        bool selectorInvoked = false;

        // When
        Result actual = await ValueTask.FromResult(result).SelectManyAsync(Selector, token: TestContext.Current.CancellationToken);

        // Then
        Assert.IsType<Success>(actual);
        Assert.True(selectorInvoked);

        return;

        Result Selector(int value)
        {
            selectorInvoked = value == 42;
            return Result.Success();
        }
    }

    [Fact(DisplayName = "ValueTask<Result<T>>.SelectManyAsync of Failure should not invoke the selector delegate (Func<T, Result>)")]
    public async Task ValueTaskResultOfTSelectManyAsyncOfFailureShouldNotInvokeSelectorDelegateFuncTResult()
    {
        // Given
        Result<int> result = Result<int>.Failure(FailureException);
        bool selectorInvoked = false;

        // When
        Result actual = await ValueTask.FromResult(result).SelectManyAsync(Selector, token: TestContext.Current.CancellationToken);

        // Then
        Assert.IsType<Failure>(actual);
        Assert.False(selectorInvoked);

        return;

        Result Selector(int value)
        {
            selectorInvoked = true;
            return Result.Success();
        }
    }

    [Fact(DisplayName = "ValueTask<Result<T>>.SelectManyAsync of Success should invoke the selector delegate (Func<T, Task<Result>>) and return its result")]
    public async Task ValueTaskResultOfTSelectManyAsyncOfSuccessShouldInvokeSelectorDelegateFuncTTaskResult()
    {
        // Given
        Result<int> result = Result<int>.Success(42);
        bool selectorInvoked = false;

        // When
        Result actual = await ValueTask.FromResult(result).SelectManyAsync(SelectorAsync, token: TestContext.Current.CancellationToken);

        // Then
        Assert.IsType<Success>(actual);
        Assert.True(selectorInvoked);

        return;

        async Task<Result> SelectorAsync(int value)
        {
            selectorInvoked = value == 42;
            return await Task.FromResult(Result.Success());
        }
    }

    [Fact(DisplayName = "ValueTask<Result<T>>.SelectManyAsync of Failure should not invoke the selector delegate (Func<T, Task<Result>>)")]
    public async Task ValueTaskResultOfTSelectManyAsyncOfFailureShouldNotInvokeSelectorDelegateFuncTTaskResult()
    {
        // Given
        Result<int> result = Result<int>.Failure(FailureException);
        bool selectorInvoked = false;

        // When
        Result actual = await ValueTask.FromResult(result).SelectManyAsync(SelectorAsync, token: TestContext.Current.CancellationToken);

        // Then
        Assert.IsType<Failure>(actual);
        Assert.False(selectorInvoked);

        return;

        async Task<Result> SelectorAsync(int value)
        {
            selectorInvoked = true;
            return await Task.FromResult(Result.Success());
        }
    }

    [Fact(DisplayName = "ValueTask<Result<T>>.SelectManyAsync of Success should invoke the selector delegate (Func<T, CancellationToken, Task<Result>>) and return its result")]
    public async Task ValueTaskResultOfTSelectManyAsyncOfSuccessShouldInvokeSelectorDelegateFuncTCancellationTokenTaskResult()
    {
        // Given
        Result<int> result = Result<int>.Success(42);
        bool selectorInvoked = false;

        // When
        Result actual = await ValueTask.FromResult(result).SelectManyAsync(SelectorAsync, token: TestContext.Current.CancellationToken);

        // Then
        Assert.IsType<Success>(actual);
        Assert.True(selectorInvoked);

        return;

        async Task<Result> SelectorAsync(int value, CancellationToken ct)
        {
            selectorInvoked = value == 42;
            return await Task.FromResult(Result.Success());
        }
    }

    [Fact(DisplayName = "ValueTask<Result<T>>.SelectManyAsync of Failure should not invoke the selector delegate (Func<T, CancellationToken, Task<Result>>)")]
    public async Task ValueTaskResultOfTSelectManyAsyncOfFailureShouldNotInvokeSelectorDelegateFuncTCancellationTokenTaskResult()
    {
        // Given
        Result<int> result = Result<int>.Failure(FailureException);
        bool selectorInvoked = false;

        // When
        Result actual = await ValueTask.FromResult(result).SelectManyAsync(SelectorAsync, token: TestContext.Current.CancellationToken);

        // Then
        Assert.IsType<Failure>(actual);
        Assert.False(selectorInvoked);

        return;

        async Task<Result> SelectorAsync(int value, CancellationToken ct)
        {
            selectorInvoked = true;
            return await Task.FromResult(Result.Success());
        }
    }

    [Fact(DisplayName = "ValueTask<Result<T>>.SelectManyAsync<TResult> of Success should invoke the selector delegate (Func<T, Result<TResult>>) and return its result")]
    public async Task ValueTaskResultOfTSelectManyAsyncOfSuccessShouldInvokeSelectorDelegateFuncTResultTResult()
    {
        // Given
        Result<int> result = Result<int>.Success(42);
        bool selectorInvoked = false;

        // When
        Result<string> actual = await ValueTask.FromResult(result).SelectManyAsync(Selector, token: TestContext.Current.CancellationToken);

        // Then
        Assert.IsType<Success<string>>(actual);
        Assert.True(selectorInvoked);
        Assert.Equal("Result is 42", actual.GetValueOrThrow());

        return;

        Result<string> Selector(int value)
        {
            selectorInvoked = value == 42;
            return Result<string>.Success($"Result is {value}");
        }
    }

    [Fact(DisplayName = "ValueTask<Result<T>>.SelectManyAsync<TResult> of Failure should not invoke the selector delegate (Func<T, Result<TResult>>)")]
    public async Task ValueTaskResultOfTSelectManyAsyncOfFailureShouldNotInvokeSelectorDelegateFuncTResultTResult()
    {
        // Given
        Result<int> result = Result<int>.Failure(FailureException);
        bool selectorInvoked = false;

        // When
        Result<string> actual = await ValueTask.FromResult(result).SelectManyAsync(Selector, token: TestContext.Current.CancellationToken);

        // Then
        Assert.IsType<Failure<string>>(actual);
        Assert.False(selectorInvoked);

        return;

        Result<string> Selector(int value)
        {
            selectorInvoked = true;
            return Result<string>.Success($"Result is {value}");
        }
    }

    [Fact(DisplayName = "ValueTask<Result<T>>.SelectManyAsync<TResult> of Success should invoke the selector delegate (Func<T, Task<Result<TResult>>>) and return its result")]
    public async Task ValueTaskResultOfTSelectManyAsyncOfSuccessShouldInvokeSelectorDelegateFuncTTaskResultTResult()
    {
        // Given
        Result<int> result = Result<int>.Success(42);
        bool selectorInvoked = false;

        // When
        Result<string> actual = await ValueTask.FromResult(result).SelectManyAsync(SelectorAsync, token: TestContext.Current.CancellationToken);

        // Then
        Assert.IsType<Success<string>>(actual);
        Assert.True(selectorInvoked);
        Assert.Equal("Result is 42", actual.GetValueOrThrow());

        return;

        async Task<Result<string>> SelectorAsync(int value)
        {
            selectorInvoked = value == 42;
            return await Task.FromResult(Result<string>.Success($"Result is {value}"));
        }
    }

    [Fact(DisplayName = "ValueTask<Result<T>>.SelectManyAsync<TResult> of Failure should not invoke the selector delegate (Func<T, Task<Result<TResult>>>)")]
    public async Task ValueTaskResultOfTSelectManyAsyncOfFailureShouldNotInvokeSelectorDelegateFuncTTaskResultTResult()
    {
        // Given
        Result<int> result = Result<int>.Failure(FailureException);
        bool selectorInvoked = false;

        // When
        Result<string> actual = await ValueTask.FromResult(result).SelectManyAsync(SelectorAsync, token: TestContext.Current.CancellationToken);

        // Then
        Assert.IsType<Failure<string>>(actual);
        Assert.False(selectorInvoked);

        return;

        async Task<Result<string>> SelectorAsync(int value)
        {
            selectorInvoked = true;
            return await Task.FromResult(Result<string>.Success($"Result is {value}"));
        }
    }

    [Fact(DisplayName = "ValueTask<Result<T>>.SelectManyAsync<TResult> of Success should invoke the selector delegate (Func<T, CancellationToken, Task<Result<TResult>>>) and return its result")]
    public async Task ValueTaskResultOfTSelectManyAsyncOfSuccessShouldInvokeSelectorDelegateFuncTCancellationTokenTaskResultTResult()
    {
        // Given
        Result<int> result = Result<int>.Success(42);
        bool selectorInvoked = false;

        // When
        Result<string> actual = await ValueTask.FromResult(result).SelectManyAsync(SelectorAsync, token: TestContext.Current.CancellationToken);

        // Then
        Assert.IsType<Success<string>>(actual);
        Assert.True(selectorInvoked);
        Assert.Equal("Result is 42", actual.GetValueOrThrow());

        return;

        async Task<Result<string>> SelectorAsync(int value, CancellationToken ct)
        {
            selectorInvoked = value == 42;
            return await Task.FromResult(Result<string>.Success($"Result is {value}"));
        }
    }

    [Fact(DisplayName = "ValueTask<Result<T>>.SelectManyAsync<TResult> of Failure should not invoke the selector delegate (Func<T, CancellationToken, Task<Result<TResult>>>)")]
    public async Task ValueTaskResultOfTSelectManyAsyncOfFailureShouldNotInvokeSelectorDelegateFuncTCancellationTokenTaskResultTResult()
    {
        // Given
        Result<int> result = Result<int>.Failure(FailureException);
        bool selectorInvoked = false;

        // When
        Result<string> actual = await ValueTask.FromResult(result).SelectManyAsync(SelectorAsync, token: TestContext.Current.CancellationToken);

        // Then
        Assert.IsType<Failure<string>>(actual);
        Assert.False(selectorInvoked);

        return;

        async Task<Result<string>> SelectorAsync(int value, CancellationToken ct)
        {
            selectorInvoked = true;
            return await Task.FromResult(Result<string>.Success($"Result is {value}"));
        }
    }

    [Fact(DisplayName = "ValueTask of Result Success.ThrowAsync should do nothing")]
    public async Task ValueTaskResultSuccessThrowAsyncShouldDoNothing()
    {
        // Given
        Result result = Result.Success();

        // When / Then
        await ValueTask.FromResult(result).ThrowAsync(token: TestContext.Current.CancellationToken);
    }

    [Fact(DisplayName = "ValueTask of Result Failure.Throw should throw Exception")]
    public async Task ValueTaskResultFailureThrowAsyncShouldThrowException()
    {
        // Given
        Result result = Result.Failure(FailureException);

        // When / Then
        await Assert.ThrowsAsync<Exception>(async () => await ValueTask.FromResult(result).ThrowAsync(token: TestContext.Current.CancellationToken));
    }

    [Fact(DisplayName = "ValueTask of Result<T> Success.ThrowAsync should do nothing")]
    public async Task ValueTaskResultOfTSuccessThrowAsyncShouldDoNothing()
    {
        // Given
        Result<int> result = Result<int>.Success(123);

        // When / Then
        await ValueTask.FromResult(result).ThrowAsync(token: TestContext.Current.CancellationToken);
    }

    [Fact(DisplayName = "ValueTask of Result<T> Failure.ThrowAsync should throw Exception")]
    public async Task ValueTaskResultOfTFailureThrowAsyncShouldThrowException()
    {
        // Given
        Result<int> result = Result<int>.Failure(FailureException);

        // When / Then
        await Assert.ThrowsAsync<Exception>(async () => await ValueTask.FromResult(result).ThrowAsync(token: TestContext.Current.CancellationToken));
    }

    [Fact(DisplayName = "ValueTask of Result Failure.ToTypedResultAsync should produce the expected result.")]
    public async Task ValueTaskResultFailureToTypedResultAsyncShouldProduceExpectedResult()
    {
        // Given
        Failure result = Result.Failure(FailureException);

        // When
        Result<int> actual = await ValueTask.FromResult(result).ToTypedResultAsync<int>(token: TestContext.Current.CancellationToken);

        // Then
        Assert.IsType<Failure<int>>(actual);
        Assert.Equal(FailureException, actual.GetExceptionOrThrow());
    }

    [Fact(DisplayName = "ValueTask of Result<T> Failure.ToTypedResultAsync should produce the expected result.")]
    public async Task ValueTaskResultOfTFailureToTypedResultAsyncShouldProduceExpectedResult()
    {
        // Given
        Failure<string> result = Result<string>.Failure(FailureException);

        // When
        Result<int> actual = await ValueTask.FromResult(result).ToTypedResultAsync<string, int>(token: TestContext.Current.CancellationToken);

        // Then
        Assert.IsType<Failure<int>>(actual);
        Assert.Equal(FailureException, actual.GetExceptionOrThrow());
    }

    [Fact(DisplayName = "ValueTask of Result<T> Failure.ToUntypedResultAsync should produce the expected result.")]
    public async Task ValueTaskResultOfTFailureToUntypedResultAsyncShouldProduceExpectedResult()
    {
        // Given
        Failure<string> result = Result<string>.Failure(FailureException);

        // When
        Result actual = await ValueTask.FromResult(result).ToUntypedResultAsync(token: TestContext.Current.CancellationToken);

        // Then
        Assert.IsType<Failure>(actual);
        Assert.StartsWith("System.Exception: Failure", actual.GetExceptionOrThrow().ToString());
    }

    [Fact(DisplayName = "ValueTask<Result<T>>.GetValueOrThrowAsync should produce the expected result when the ValueTask has not yet completed.")]
    public async Task ValueTaskResultOfTGetValueOrThrowAsyncShouldProduceExpectedResultWhenValueTaskHasNotYetCompleted()
    {
        // Given
        TaskCompletionSource<Result<int>> source = new();
        ValueTask<int> pending = new ValueTask<Result<int>>(source.Task).GetValueOrThrowAsync(TestContext.Current.CancellationToken);

        // When
        source.SetResult(Result<int>.Success(123));
        int actual = await pending;

        // Then
        Assert.Equal(123, actual);
    }

    [Fact(DisplayName = "ValueTask<Result<T>>.GetValueOrThrowAsync should throw OperationCanceledException when cancelled before the ValueTask completes.")]
    public async Task ValueTaskResultOfTGetValueOrThrowAsyncShouldThrowOperationCanceledExceptionWhenCancelledBeforeValueTaskCompletes()
    {
        // Given
        TaskCompletionSource<Result<int>> source = new();
        using CancellationTokenSource cancellation = new();
        ValueTask<int> pending = new ValueTask<Result<int>>(source.Task).GetValueOrThrowAsync(cancellation.Token);

        // When
        await cancellation.CancelAsync();
        source.SetResult(Result<int>.Success(123));

        // Then
        await Assert.ThrowsAnyAsync<OperationCanceledException>(async () => await pending);
    }

    [Fact(DisplayName = "ValueTask<Result<T>>.GetValueOrThrowAsync should produce the expected result when the ValueTask has already completed and the token is cancelled.")]
    public async Task ValueTaskResultOfTGetValueOrThrowAsyncShouldProduceExpectedResultWhenValueTaskHasCompletedAndTokenIsCancelled()
    {
        // Given
        ValueTask<Result<int>> completed = new(Result<int>.Success(123));
        CancellationToken token = new(canceled: true);

        // When
        int actual = await completed.GetValueOrThrowAsync(token);

        // Then
        Assert.Equal(123, actual);
    }
}
