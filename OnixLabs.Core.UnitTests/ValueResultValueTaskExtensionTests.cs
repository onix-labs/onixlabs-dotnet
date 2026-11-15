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

public sealed class ValueResultValueTaskExtensionTests
{
    private static readonly Exception FailureException = new("Failure");

    [Fact(DisplayName = "ValueTask of ValueResult Success.GetExceptionOrDefaultAsync should produce the expected result.")]
    public async Task ValueTaskValueResultSuccessGetExceptionOrDefaultAsyncShouldProduceExpectedResult()
    {
        // Given
        ValueResult result = ValueResult.Success();

        // When
        Exception? actual = await ValueTask.FromResult(result).GetExceptionOrDefaultAsync(cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Null(actual);
    }

    [Fact(DisplayName = "ValueTask of ValueResult Success.GetExceptionOrDefaultAsync with default value should produce the expected result.")]
    public async Task ValueTaskValueResultSuccessGetExceptionOrDefaultAsyncWithDefaultValueShouldProduceExpectedResult()
    {
        // Given
        ValueResult result = ValueResult.Success();

        // When
        Exception actual = await ValueTask.FromResult(result).GetExceptionOrDefaultAsync(FailureException, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(FailureException, actual);
    }

    [Fact(DisplayName = "ValueTask of ValueResult Success.GetExceptionOrThrowAsync should produce the expected result.")]
    public async Task ValueTaskValueResultSuccessGetExceptionOrThrowAsyncShouldProduceExpectedResult()
    {
        // Given
        ValueResult result = ValueResult.Success();

        // When
        Exception exception = await Assert.ThrowsAsync<InvalidOperationException>(async () =>
            await ValueTask.FromResult(result).GetExceptionOrThrowAsync(cancellationToken: TestContext.Current.CancellationToken));

        // Then
        Assert.Equal("The current result is in a success state; no exception to retrieve.", exception.Message);
    }

    [Fact(DisplayName = "ValueTask of ValueResult Failure.GetExceptionOrDefaultAsync should produce the expected result.")]
    public async Task ValueTaskValueResultFailureGetExceptionOrDefaultAsyncShouldProduceExpectedResult()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);

        // When
        Exception? actual = await ValueTask.FromResult(result).GetExceptionOrDefaultAsync(cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(FailureException, actual);
    }

    [Fact(DisplayName = "ValueTask of ValueResult Failure.GetExceptionOrDefaultAsync with default value should produce the expected result.")]
    public async Task ValueTaskValueResultFailureGetExceptionOrDefaultAsyncWithDefaultValueShouldProduceExpectedResult()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);

        // When
        Exception actual = await ValueTask.FromResult(result).GetExceptionOrDefaultAsync(new Exception("unexpected exception"), cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(FailureException, actual);
    }

    [Fact(DisplayName = "ValueTask of ValueResult Failure.GetExceptionOrThrowAsync should produce the expected result.")]
    public async Task ValueTaskValueResultFailureGetExceptionOrThrowAsyncShouldProduceExpectedResult()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);

        // When
        Exception actual = await ValueTask.FromResult(result).GetExceptionOrThrowAsync(cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(FailureException, actual);
    }

    [Fact(DisplayName = "ValueTask of ValueResult<T> Success.GetExceptionOrDefaultAsync should produce the expected result.")]
    public async Task ValueTaskValueResultOfTSuccessGetExceptionOrDefaultAsyncShouldProduceExpectedResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(123);

        // When
        Exception? actual = await ValueTask.FromResult(result).GetExceptionOrDefaultAsync(cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Null(actual);
    }

    [Fact(DisplayName = "ValueTask of ValueResult<T> Success.GetExceptionOrDefaultAsync with default value should produce the expected result.")]
    public async Task ValueTaskValueResultOfTSuccessGetExceptionOrDefaultAsyncWithDefaultValueShouldProduceExpectedResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(123);

        // When
        Exception actual = await ValueTask.FromResult(result).GetExceptionOrDefaultAsync(FailureException, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(FailureException, actual);
    }

    [Fact(DisplayName = "ValueTask of ValueResult<T> Success.GetExceptionOrThrowAsync should produce the expected result.")]
    public async Task ValueTaskValueResultOfTSuccessGetExceptionOrThrowAsyncShouldProduceExpectedResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(123);

        // When
        Exception exception = await Assert.ThrowsAsync<InvalidOperationException>(async () =>
            await ValueTask.FromResult(result).GetExceptionOrThrowAsync(cancellationToken: TestContext.Current.CancellationToken));

        // Then
        Assert.Equal("The current result is in a success state; no exception to retrieve.", exception.Message);
    }

    [Fact(DisplayName = "ValueTask of ValueResult<T> Failure.GetExceptionOrDefaultAsync should produce the expected result.")]
    public async Task ValueTaskValueResultOfTFailureGetExceptionOrDefaultAsyncShouldProduceExpectedResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);

        // When
        Exception? actual = await ValueTask.FromResult(result).GetExceptionOrDefaultAsync(cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(FailureException, actual);
    }

    [Fact(DisplayName = "ValueTask of ValueResult<T> Failure.GetExceptionOrDefaultAsync with default value should produce the expected result.")]
    public async Task ValueTaskValueResultOfTFailureGetExceptionOrDefaultAsyncWithDefaultValueShouldProduceExpectedResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);

        // When
        Exception actual = await ValueTask.FromResult(result).GetExceptionOrDefaultAsync(new Exception("unexpected exception"), cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(FailureException, actual);
    }

    [Fact(DisplayName = "ValueTask of ValueResult<T> Failure.GetExceptionOrThrowAsync should produce the expected result.")]
    public async Task ValueTaskValueResultOfTFailureGetExceptionOrThrowAsyncShouldProduceExpectedResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);

        // When
        Exception actual = await ValueTask.FromResult(result).GetExceptionOrThrowAsync(cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(FailureException, actual);
    }

    [Fact(DisplayName = "ValueTask of ValueResult.GetValueOrDefaultAsync should return the result value when the result is Success")]
    public async Task ValueTaskValueResultGetValueOrDefaultAsyncShouldReturnResultValueWhenResultIsSuccess()
    {
        // Given
        const int expectedNumber = 123;
        const string expectedText = "abc";
        ValueTask<ValueResult<int>> numberTask = new(ValueResult<int>.OfAsync(async () => await Task.FromResult(expectedNumber), TestContext.Current.CancellationToken));
        ValueTask<ValueResult<string>> textTask = new(ValueResult<string>.OfAsync(async () => await Task.FromResult(expectedText), TestContext.Current.CancellationToken));

        // When
        int actualNumber = await numberTask.GetValueOrDefaultAsync(cancellationToken: TestContext.Current.CancellationToken);
        string? actualText = await textTask.GetValueOrDefaultAsync(cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expectedNumber, actualNumber);

        Assert.NotNull(actualText);
        Assert.Equal(expectedText, actualText);
    }

    [Fact(DisplayName = "ValueTask of ValueResult.GetValueOrDefaultAsync should return default when the result is Failure")]
    public async Task ValueTaskValueResultGetValueOrDefaultAsyncShouldReturnDefaultWhenResultIsFailure()
    {
        // Given
        ValueTask<ValueResult<int>> numberTask = new(ValueResult<int>.OfAsync(() => throw FailureException, TestContext.Current.CancellationToken));
        ValueTask<ValueResult<string>> textTask = new(ValueResult<string>.OfAsync(() => throw FailureException, TestContext.Current.CancellationToken));

        // When
        int actualNumber = await numberTask.GetValueOrDefaultAsync(cancellationToken: TestContext.Current.CancellationToken);
        string? actualText = await textTask.GetValueOrDefaultAsync(cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(0, actualNumber);
        Assert.Null(actualText);
    }

    [Fact(DisplayName = "ValueTask of ValueResult.GetValueOrDefaultAsync with default value should return the result value when the result is Success")]
    public async Task ValueTaskValueResultGetValueOrDefaultAsyncWithDefaultValueShouldReturnResultValueWhenResultIsSuccess()
    {
        // Given
        const int expectedNumber = 123;
        const string expectedText = "abc";
        ValueTask<ValueResult<int>> numberTask = new(ValueResult<int>.OfAsync(async () => await Task.FromResult(expectedNumber), TestContext.Current.CancellationToken));
        ValueTask<ValueResult<string>> textTask = new(ValueResult<string>.OfAsync(async () => await Task.FromResult(expectedText), TestContext.Current.CancellationToken));

        // When
        int actualNumber = await numberTask.GetValueOrDefaultAsync(456, cancellationToken: TestContext.Current.CancellationToken);
        // ReSharper disable once VariableCanBeNotNullable
        string? actualText = await textTask.GetValueOrDefaultAsync("xyz", cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expectedNumber, actualNumber);

        Assert.NotNull(actualText);
        Assert.Equal(expectedText, actualText);
    }

    [Fact(DisplayName = "ValueTask of ValueResult.GetValueOrDefaultAsync with default value should return default value when the result is Failure")]
    public async Task ValueTaskValueResultGetValueOrDefaultAsyncWithDefaultValueShouldReturnDefaultValueWhenResultIsFailure()
    {
        // Given
        const int expectedNumber = 456;
        const string expectedText = "abc";
        ValueTask<ValueResult<int>> numberTask = new(ValueResult<int>.OfAsync(() => throw FailureException, TestContext.Current.CancellationToken));
        ValueTask<ValueResult<string>> textTask = new(ValueResult<string>.OfAsync(() => throw FailureException, TestContext.Current.CancellationToken));

        // When
        int actualNumber = await numberTask.GetValueOrDefaultAsync(expectedNumber, cancellationToken: TestContext.Current.CancellationToken);
        string actualText = await textTask.GetValueOrDefaultAsync(expectedText, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expectedNumber, actualNumber);

        Assert.NotNull(actualText);
        Assert.Equal(expectedText, actualText);
    }

    [Fact(DisplayName = "ValueTask of ValueResult.GetValueOrThrowAsync should return the result value when the result is Success")]
    public async Task ValueTaskValueResultGetValueOrThrowAsyncShouldReturnResultValueWhenResultIsSuccess()
    {
        // Given
        const int expectedNumber = 123;
        const string expectedText = "abc";
        ValueTask<ValueResult<int>> numberTask = new(ValueResult<int>.OfAsync(async () => await Task.FromResult(expectedNumber), TestContext.Current.CancellationToken));
        ValueTask<ValueResult<string>> textTask = new(ValueResult<string>.OfAsync(async () => await Task.FromResult(expectedText), TestContext.Current.CancellationToken));

        // When
        int actualNumber = await numberTask.GetValueOrThrowAsync(cancellationToken: TestContext.Current.CancellationToken);
        string actualText = await textTask.GetValueOrThrowAsync(cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expectedNumber, actualNumber);

        Assert.NotNull(actualText);
        Assert.Equal(expectedText, actualText);
    }

    [Fact(DisplayName = "ValueTask of ValueResult.GetValueOrThrowAsync should throw Exception when the result is Failure")]
    public async Task ValueTaskValueResultGetValueOrThrowAsyncShouldReturnDefaultWhenResultIsFailure()
    {
        // Given
        ValueTask<ValueResult<int>> numberTask = new(ValueResult<int>.OfAsync(() => throw FailureException, TestContext.Current.CancellationToken));
        ValueTask<ValueResult<string>> textTask = new(ValueResult<string>.OfAsync(() => throw FailureException, TestContext.Current.CancellationToken));

        // When
        Exception numberException = await Assert.ThrowsAsync<Exception>(async () =>
            await numberTask.GetValueOrThrowAsync(cancellationToken: TestContext.Current.CancellationToken));

        Exception textException = await Assert.ThrowsAsync<Exception>(async () =>
            await textTask.GetValueOrThrowAsync(cancellationToken: TestContext.Current.CancellationToken));

        // Then
        Assert.Equal(numberException, FailureException);
        Assert.Equal(textException, FailureException);
    }

    [Fact(DisplayName = "ValueTask of ValueResult<T>.GetValueOrNoneAsync should return the value wrapped in ValueOptional when the result is Success and the value is not None")]
    public async Task ValueTaskValueResultGetValueOrNoneAsyncShouldReturnOptionalValueWhenResultIsSuccessAndValueIsNotNone()
    {
        // Given
        const int expectedNumber = 123;
        const string expectedText = "abc";
        ValueResult<int> numberResult = expectedNumber;
        ValueResult<string> textResult = expectedText;

        // When
        ValueOptional<int> actualNumber = await ValueTask.FromResult(numberResult).GetValueOrNoneAsync(cancellationToken: TestContext.Current.CancellationToken);
        ValueOptional<string> actualText = await ValueTask.FromResult(textResult).GetValueOrNoneAsync(cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(ValueOptional<int>.Of(expectedNumber), actualNumber);
        Assert.Equal(ValueOptional<string>.Of(expectedText), actualText);
    }

    [Fact(DisplayName = "ValueTask of ValueResult<T>.GetValueOrNoneAsync should return Some(default) when the result is Success and the value is the default of a value type", Skip = "Source bug: ValueResult<T>.GetValueOrNone uses ValueOptional<T>.Of, which maps a default value to None; Result<T>.GetValueOrNone returns Some(default).")]
    public async Task ValueTaskValueResultGetValueOrNoneAsyncShouldReturnSomeDefaultWhenResultIsSuccessAndValueIsValueTypeDefault()
    {
        // Given
        ValueResult<int> numberResult = ValueResult<int>.Success(0);

        // When
        ValueOptional<int> actualNumber = await ValueTask.FromResult(numberResult).GetValueOrNoneAsync(cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(ValueOptional<int>.IsSome(actualNumber));
        Assert.Equal(0, actualNumber);
    }

    [Fact(DisplayName = "ValueTask of ValueResult<T>.GetValueOrNoneAsync should return None when the result is Success and the underlying value is null")]
    public async Task ValueTaskValueResultGetValueOrNoneAsyncShouldReturnNoneWhenResultIsSuccessAndUnderlyingValueIsNull()
    {
        // Given
        ValueResult<string> textResult = ValueResult<string>.Success(null!);

        // When
        ValueOptional<string> actualText = await ValueTask.FromResult(textResult).GetValueOrNoneAsync(cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(ValueOptional<string>.None, actualText);
    }

    [Fact(DisplayName = "ValueTask of ValueResult<T>.GetValueOrNoneAsync should return None when the result is Failure")]
    public async Task ValueTaskValueResultGetValueOrNoneAsyncShouldReturnNoneWhenResultIsFailure()
    {
        // Given
        ValueResult<int> numberResult = ValueResult<int>.Failure(FailureException);
        ValueResult<string> textResult = ValueResult<string>.Failure(FailureException);

        // When
        ValueOptional<int> actualNumber = await ValueTask.FromResult(numberResult).GetValueOrNoneAsync(cancellationToken: TestContext.Current.CancellationToken);
        ValueOptional<string> actualText = await ValueTask.FromResult(textResult).GetValueOrNoneAsync(cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(ValueOptional<int>.None, actualNumber);
        Assert.Equal(ValueOptional<string>.None, actualText);
    }

    [Fact(DisplayName = "ValueTask of ValueResult<ValueOptional<T>>.GetValueOrNoneAsync should return the ValueOptional value when the result is Success and the ValueOptional value is not None")]
    public async Task ValueTaskValueResultOptionalGetValueOrNoneAsyncShouldReturnOptionalValueWhenResultIsSuccessAndOptionalValueIsNotNone()
    {
        // Given
        ValueOptional<int> expectedNumber = 123;
        ValueOptional<string> expectedText = "abc";
        ValueResult<ValueOptional<int>> numberResult = expectedNumber;
        ValueResult<ValueOptional<string>> textResult = expectedText;

        // When
        ValueOptional<int> actualNumber = await ValueTask.FromResult(numberResult).GetValueOrNoneAsync(cancellationToken: TestContext.Current.CancellationToken);
        ValueOptional<string> actualText = await ValueTask.FromResult(textResult).GetValueOrNoneAsync(cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expectedNumber, actualNumber);
        Assert.Equal(expectedText, actualText);
    }

    [Fact(DisplayName = "ValueTask of ValueResult<ValueOptional<T>>.GetValueOrNoneAsync should return None value when the result is Success and the ValueOptional value is None")]
    public async Task ValueTaskValueResultOptionalGetValueOrNoneAsyncShouldReturnNoneWhenResultIsSuccessAndOptionalValueIsNone()
    {
        // Given
        ValueOptional<int> expectedNumber = ValueOptional<int>.None;
        ValueOptional<string> expectedText = ValueOptional<string>.None;
        ValueResult<ValueOptional<int>> numberResult = expectedNumber;
        ValueResult<ValueOptional<string>> textResult = expectedText;

        // When
        ValueOptional<int> actualNumber = await ValueTask.FromResult(numberResult).GetValueOrNoneAsync(cancellationToken: TestContext.Current.CancellationToken);
        ValueOptional<string> actualText = await ValueTask.FromResult(textResult).GetValueOrNoneAsync(cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expectedNumber, actualNumber);
        Assert.Equal(expectedText, actualText);
    }

    [Fact(DisplayName = "ValueTask of ValueResult<ValueOptional<T>>.GetValueOrNoneAsync should return None value when the result is Failure")]
    public async Task ValueTaskValueResultOptionalGetValueOrNoneAsyncShouldReturnNoneWhenResultIsFailure()
    {
        // Given
        ValueOptional<int> expectedNumber = ValueOptional<int>.None;
        ValueOptional<string> expectedText = ValueOptional<string>.None;
        ValueResult<ValueOptional<int>> numberResult = ValueResult<ValueOptional<int>>.Failure(FailureException);
        ValueResult<ValueOptional<string>> textResult = ValueResult<ValueOptional<string>>.Failure(FailureException);

        // When
        ValueOptional<int> actualNumber = await ValueTask.FromResult(numberResult).GetValueOrNoneAsync(cancellationToken: TestContext.Current.CancellationToken);
        ValueOptional<string> actualText = await ValueTask.FromResult(textResult).GetValueOrNoneAsync(cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expectedNumber, actualNumber);
        Assert.Equal(expectedText, actualText);
    }

    [Fact(DisplayName = "ValueTask of ValueResult.GetOptionalValueOrThrowAsync should return the ValueOptional value when the ValueResult is Success and the ValueOptional value is not None")]
    public async Task ValueTaskValueResultGetOptionalValueOrThrowAsyncShouldReturnOptionalValueWhenResultIsSuccessAndOptionalValueIsNotNone()
    {
        // Given
        const int expectedNumber = 123;
        const string expectedText = "abc";
        ValueResult<ValueOptional<int>> numberResult = ValueOptional<int>.Some(expectedNumber);
        ValueResult<ValueOptional<string>> textResult = ValueOptional<string>.Some(expectedText);

        // When
        int actualNumber = await ValueTask.FromResult(numberResult).GetOptionalValueOrThrowAsync(cancellationToken: TestContext.Current.CancellationToken);
        string actualText = await ValueTask.FromResult(textResult).GetOptionalValueOrThrowAsync(cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expectedNumber, actualNumber);
        Assert.Equal(expectedText, actualText);
    }

    [Fact(DisplayName = "ValueTask of ValueResult.GetOptionalValueOrThrowAsync should throw InvalidOperationException when the ValueResult is Success and the ValueOptional value is None")]
    public async Task ValueTaskValueResultGetOptionalValueOrThrowAsyncShouldThrowInvalidOperationExceptionWhenResultIsSuccessAndOptionalValueIsNone()
    {
        // Given
        ValueResult<ValueOptional<int>> numberResult = ValueOptional<int>.None;
        ValueResult<ValueOptional<string>> textResult = ValueOptional<string>.None;

        // When
        Exception numberException = await Assert.ThrowsAsync<InvalidOperationException>(async () =>
            await ValueTask.FromResult(numberResult).GetOptionalValueOrThrowAsync(cancellationToken: TestContext.Current.CancellationToken));

        Exception textException = await Assert.ThrowsAsync<InvalidOperationException>(async () =>
            await ValueTask.FromResult(textResult).GetOptionalValueOrThrowAsync(cancellationToken: TestContext.Current.CancellationToken));

        // Then
        Assert.Equal("Optional value of type System.Int32 is not present.", numberException.Message);
        Assert.Equal("Optional value of type System.String is not present.", textException.Message);
    }

    [Fact(DisplayName = "ValueTask of ValueResult.GetOptionalValueOrThrowAsync should throw InvalidOperationException when the ValueResult is Failure")]
    public async Task ValueTaskValueResultGetOptionalValueOrThrowAsyncShouldThrowInvalidOperationExceptionWhenResultIsFailure()
    {
        // Given
        ValueResult<ValueOptional<int>> numberResult = ValueResult<ValueOptional<int>>.Failure(new Exception("Result has failed."));
        ValueResult<ValueOptional<string>> textResult = ValueResult<ValueOptional<string>>.Failure(new Exception("Result has failed."));

        // When
        Exception numberException = await Assert.ThrowsAsync<Exception>(async () =>
            await ValueTask.FromResult(numberResult).GetOptionalValueOrThrowAsync(cancellationToken: TestContext.Current.CancellationToken));

        Exception textException = await Assert.ThrowsAsync<Exception>(async () =>
            await ValueTask.FromResult(textResult).GetOptionalValueOrThrowAsync(cancellationToken: TestContext.Current.CancellationToken));

        // Then
        Assert.Equal("Result has failed.", numberException.Message);
        Assert.Equal("Result has failed.", textException.Message);
    }

    [Fact(DisplayName = "ValueTask of ValueResult.GetOptionalValueOrDefaultAsync should return the ValueOptional value when the ValueResult is Success and the ValueOptional value is not None")]
    public async Task ValueTaskValueResultGetOptionalValueOrDefaultAsyncShouldReturnOptionalValueWhenResultIsSuccessAndOptionalValueIsNotNone()
    {
        // Given
        const int expectedNumber = 123;
        const string expectedText = "abc";
        ValueResult<ValueOptional<int>> numberResult = ValueOptional<int>.Some(expectedNumber);
        ValueResult<ValueOptional<string>> textResult = ValueOptional<string>.Some(expectedText);

        // When
        int actualNumber = await ValueTask.FromResult(numberResult).GetOptionalValueOrDefaultAsync(456, cancellationToken: TestContext.Current.CancellationToken);
        string actualText = await ValueTask.FromResult(textResult).GetOptionalValueOrDefaultAsync("xyz", cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expectedNumber, actualNumber);
        Assert.Equal(expectedText, actualText);
    }

    [Fact(DisplayName = "ValueTask of ValueResult.GetOptionalValueOrDefaultAsync should throw return the default value when the ValueResult is Success and the ValueOptional value is None")]
    public async Task ValueTaskValueResultGetOptionalValueOrDefaultAsyncShouldReturnDefaultValueWhenResultIsSuccessAndOptionalValueIsNone()
    {
        // Given
        const int expectedNumber = 456;
        const string expectedText = "xyz";
        ValueResult<ValueOptional<int>> numberResult = ValueOptional<int>.None;
        ValueResult<ValueOptional<string>> textResult = ValueOptional<string>.None;

        // When
        int actualNumber = await ValueTask.FromResult(numberResult).GetOptionalValueOrDefaultAsync(expectedNumber, cancellationToken: TestContext.Current.CancellationToken);
        string actualText = await ValueTask.FromResult(textResult).GetOptionalValueOrDefaultAsync(expectedText, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expectedNumber, actualNumber);
        Assert.Equal(expectedText, actualText);
    }

    [Fact(DisplayName = "ValueTask of ValueResult.GetOptionalValueOrDefaultAsync should return default value when the result is Failure")]
    public async Task ValueTaskValueResultGetOptionalValueOrDefaultAsyncShouldReturnDefaultValueWhenResultIsFailure()
    {
        // Given
        const int expectedNumber = 456;
        const string expectedText = "xyz";
        ValueResult<ValueOptional<int>> numberResult = ValueResult<ValueOptional<int>>.Failure(new Exception("Result has failed."));
        ValueResult<ValueOptional<string>> textResult = ValueResult<ValueOptional<string>>.Failure(new Exception("Result has failed."));

        // When
        int actualNumber = await ValueTask.FromResult(numberResult).GetOptionalValueOrDefaultAsync(expectedNumber, cancellationToken: TestContext.Current.CancellationToken);
        string actualText = await ValueTask.FromResult(textResult).GetOptionalValueOrDefaultAsync(expectedText, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expectedNumber, actualNumber);
        Assert.Equal(expectedText, actualText);
    }

    [Fact(DisplayName = "ValueTask<ValueResult>.MatchAsync of Success should invoke the success delegate (Action, Action<Exception>)")]
    public async Task ValueTaskValueResultMatchAsyncOfSuccessShouldInvokeSuccessDelegateActionActionException()
    {
        // Given
        ValueResult result = ValueResult.Success();
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await ValueTask.FromResult(result).MatchAsync(Success, Failure, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(successCalled);
        Assert.False(failureCalled);

        return;
        void Success() => successCalled = true;
        void Failure(Exception exception) => failureCalled = true;
    }

    [Fact(DisplayName = "ValueTask<ValueResult>.MatchAsync of Failure should invoke the failure delegate (Action, Action<Exception>)")]
    public async Task ValueTaskValueResultMatchAsyncOfFailureShouldInvokeFailureDelegateActionActionException()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await ValueTask.FromResult(result).MatchAsync(Success, Failure, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.False(successCalled);
        Assert.True(failureCalled);

        return;
        void Success() => successCalled = true;
        void Failure(Exception exception) => failureCalled = true;
    }

    [Fact(DisplayName = "ValueTask<ValueResult>.MatchAsync of Success should invoke the success delegate (Func<Task>, Action<Exception>)")]
    public async Task ValueTaskValueResultMatchAsyncOfSuccessShouldInvokeSuccessDelegateFuncTaskActionException()
    {
        // Given
        ValueResult result = ValueResult.Success();
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await ValueTask.FromResult(result).MatchAsync(SuccessAsync, Failure, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(successCalled);
        Assert.False(failureCalled);

        return;
        async Task SuccessAsync() => await Task.Run(() => successCalled = true);
        void Failure(Exception exception) => failureCalled = true;
    }

    [Fact(DisplayName = "ValueTask<ValueResult>.MatchAsync of Success should invoke the success delegate (Func<ValueTask>, Action<Exception>)")]
    public async Task ValueTaskValueResultMatchAsyncOfSuccessShouldInvokeSuccessDelegateFuncTaskActionExceptionWithValueTaskDelegates()
    {
        // Given
        ValueResult result = ValueResult.Success();
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await ValueTask.FromResult(result).MatchAsync(SuccessAsync, Failure, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(successCalled);
        Assert.False(failureCalled);

        return;
        async ValueTask SuccessAsync() => await Task.Run(() => successCalled = true);
        void Failure(Exception exception) => failureCalled = true;
    }

    [Fact(DisplayName = "ValueTask<ValueResult>.MatchAsync of Failure should invoke the failure delegate (Func<Task>, Action<Exception>)")]
    public async Task ValueTaskValueResultMatchAsyncOfFailureShouldInvokeFailureDelegateFuncTaskActionException()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await ValueTask.FromResult(result).MatchAsync(SuccessAsync, Failure, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.False(successCalled);
        Assert.True(failureCalled);

        return;
        async Task SuccessAsync() => await Task.Run(() => successCalled = true);
        void Failure(Exception exception) => failureCalled = true;
    }

    [Fact(DisplayName = "ValueTask<ValueResult>.MatchAsync of Failure should invoke the failure delegate (Func<ValueTask>, Action<Exception>)")]
    public async Task ValueTaskValueResultMatchAsyncOfFailureShouldInvokeFailureDelegateFuncTaskActionExceptionWithValueTaskDelegates()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await ValueTask.FromResult(result).MatchAsync(SuccessAsync, Failure, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.False(successCalled);
        Assert.True(failureCalled);

        return;
        async ValueTask SuccessAsync() => await Task.Run(() => successCalled = true);
        void Failure(Exception exception) => failureCalled = true;
    }

    [Fact(DisplayName = "ValueTask<ValueResult>.MatchAsync of Success should invoke the success delegate (Func<CancellationToken, Task>, Action<Exception>)")]
    public async Task ValueTaskValueResultMatchAsyncOfSuccessShouldInvokeSuccessDelegateFuncCancellationTokenTaskActionException()
    {
        // Given
        ValueResult result = ValueResult.Success();
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await ValueTask.FromResult(result).MatchAsync(SuccessAsync, Failure, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(successCalled);
        Assert.False(failureCalled);

        return;
        async Task SuccessAsync(CancellationToken ct) => await Task.Run(() => successCalled = true, ct);
        void Failure(Exception exception) => failureCalled = true;
    }

    [Fact(DisplayName = "ValueTask<ValueResult>.MatchAsync of Success should invoke the success delegate (Func<CancellationToken, ValueTask>, Action<Exception>)")]
    public async Task ValueTaskValueResultMatchAsyncOfSuccessShouldInvokeSuccessDelegateFuncCancellationTokenTaskActionExceptionWithValueTaskDelegates()
    {
        // Given
        ValueResult result = ValueResult.Success();
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await ValueTask.FromResult(result).MatchAsync(SuccessAsync, Failure, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(successCalled);
        Assert.False(failureCalled);

        return;
        async ValueTask SuccessAsync(CancellationToken ct) => await Task.Run(() => successCalled = true, ct);
        void Failure(Exception exception) => failureCalled = true;
    }

    [Fact(DisplayName = "ValueTask<ValueResult>.MatchAsync of Failure should invoke the failure delegate (Func<CancellationToken, Task>, Action<Exception>)")]
    public async Task ValueTaskValueResultMatchAsyncOfFailureShouldInvokeFailureDelegateFuncCancellationTokenTaskActionException()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await ValueTask.FromResult(result).MatchAsync(SuccessAsync, Failure, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.False(successCalled);
        Assert.True(failureCalled);

        return;
        async Task SuccessAsync(CancellationToken ct) => await Task.Run(() => successCalled = true, ct);
        void Failure(Exception exception) => failureCalled = true;
    }

    [Fact(DisplayName = "ValueTask<ValueResult>.MatchAsync of Failure should invoke the failure delegate (Func<CancellationToken, ValueTask>, Action<Exception>)")]
    public async Task ValueTaskValueResultMatchAsyncOfFailureShouldInvokeFailureDelegateFuncCancellationTokenTaskActionExceptionWithValueTaskDelegates()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await ValueTask.FromResult(result).MatchAsync(SuccessAsync, Failure, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.False(successCalled);
        Assert.True(failureCalled);

        return;
        async ValueTask SuccessAsync(CancellationToken ct) => await Task.Run(() => successCalled = true, ct);
        void Failure(Exception exception) => failureCalled = true;
    }

    [Fact(DisplayName = "ValueTask<ValueResult>.MatchAsync of Success should invoke the success delegate (Action, Func<Exception, Task>)")]
    public async Task ValueTaskValueResultMatchAsyncOfSuccessShouldInvokeSuccessDelegateActionFuncExceptionTask()
    {
        // Given
        ValueResult result = ValueResult.Success();
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await ValueTask.FromResult(result).MatchAsync(Success, FailureAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(successCalled);
        Assert.False(failureCalled);

        return;
        void Success() => successCalled = true;
        async Task FailureAsync(Exception exception) => await Task.Run(() => failureCalled = true);
    }

    [Fact(DisplayName = "ValueTask<ValueResult>.MatchAsync of Success should invoke the success delegate (Action, Func<Exception, ValueTask>)")]
    public async Task ValueTaskValueResultMatchAsyncOfSuccessShouldInvokeSuccessDelegateActionFuncExceptionTaskWithValueTaskDelegates()
    {
        // Given
        ValueResult result = ValueResult.Success();
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await ValueTask.FromResult(result).MatchAsync(Success, FailureAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(successCalled);
        Assert.False(failureCalled);

        return;
        void Success() => successCalled = true;
        async ValueTask FailureAsync(Exception exception) => await Task.Run(() => failureCalled = true);
    }

    [Fact(DisplayName = "ValueTask<ValueResult>.MatchAsync of Failure should invoke the failure delegate (Action, Func<Exception, Task>)")]
    public async Task ValueTaskValueResultMatchAsyncOfFailureShouldInvokeFailureDelegateActionFuncExceptionTask()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await ValueTask.FromResult(result).MatchAsync(Success, FailureAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.False(successCalled);
        Assert.True(failureCalled);

        return;
        void Success() => successCalled = true;
        async Task FailureAsync(Exception exception) => await Task.Run(() => failureCalled = true);
    }

    [Fact(DisplayName = "ValueTask<ValueResult>.MatchAsync of Failure should invoke the failure delegate (Action, Func<Exception, ValueTask>)")]
    public async Task ValueTaskValueResultMatchAsyncOfFailureShouldInvokeFailureDelegateActionFuncExceptionTaskWithValueTaskDelegates()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await ValueTask.FromResult(result).MatchAsync(Success, FailureAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.False(successCalled);
        Assert.True(failureCalled);

        return;
        void Success() => successCalled = true;
        async ValueTask FailureAsync(Exception exception) => await Task.Run(() => failureCalled = true);
    }

    [Fact(DisplayName = "ValueTask<ValueResult>.MatchAsync of Success should invoke the success delegate (Action, Func<Exception, CancellationToken, Task>)")]
    public async Task ValueTaskValueResultMatchAsyncOfSuccessShouldInvokeSuccessDelegateActionFuncExceptionCancellationTokenTask()
    {
        // Given
        ValueResult result = ValueResult.Success();
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await ValueTask.FromResult(result).MatchAsync(Success, FailureAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(successCalled);
        Assert.False(failureCalled);

        return;
        void Success() => successCalled = true;
        async Task FailureAsync(Exception exception, CancellationToken ct) => await Task.Run(() => failureCalled = true, ct);
    }

    [Fact(DisplayName = "ValueTask<ValueResult>.MatchAsync of Success should invoke the success delegate (Action, Func<Exception, CancellationToken, ValueTask>)")]
    public async Task ValueTaskValueResultMatchAsyncOfSuccessShouldInvokeSuccessDelegateActionFuncExceptionCancellationTokenTaskWithValueTaskDelegates()
    {
        // Given
        ValueResult result = ValueResult.Success();
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await ValueTask.FromResult(result).MatchAsync(Success, FailureAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(successCalled);
        Assert.False(failureCalled);

        return;
        void Success() => successCalled = true;
        async ValueTask FailureAsync(Exception exception, CancellationToken ct) => await Task.Run(() => failureCalled = true, ct);
    }

    [Fact(DisplayName = "ValueTask<ValueResult>.MatchAsync of Failure should invoke the failure delegate (Action, Func<Exception, CancellationToken, Task>)")]
    public async Task ValueTaskValueResultMatchAsyncOfFailureShouldInvokeFailureDelegateActionFuncExceptionCancellationTokenTask()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await ValueTask.FromResult(result).MatchAsync(Success, FailureAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.False(successCalled);
        Assert.True(failureCalled);

        return;
        void Success() => successCalled = true;
        async Task FailureAsync(Exception exception, CancellationToken ct) => await Task.Run(() => failureCalled = true, ct);
    }

    [Fact(DisplayName = "ValueTask<ValueResult>.MatchAsync of Failure should invoke the failure delegate (Action, Func<Exception, CancellationToken, ValueTask>)")]
    public async Task ValueTaskValueResultMatchAsyncOfFailureShouldInvokeFailureDelegateActionFuncExceptionCancellationTokenTaskWithValueTaskDelegates()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await ValueTask.FromResult(result).MatchAsync(Success, FailureAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.False(successCalled);
        Assert.True(failureCalled);

        return;
        void Success() => successCalled = true;
        async ValueTask FailureAsync(Exception exception, CancellationToken ct) => await Task.Run(() => failureCalled = true, ct);
    }

    [Fact(DisplayName = "ValueTask<ValueResult>.MatchAsync of Success should invoke the success delegate (Func<Task>, Func<Exception, Task>)")]
    public async Task ValueTaskValueResultMatchAsyncOfSuccessShouldInvokeSuccessDelegateFuncTaskFuncExceptionTask()
    {
        // Given
        ValueResult result = ValueResult.Success();
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await ValueTask.FromResult(result).MatchAsync(SuccessAsync, FailureAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(successCalled);
        Assert.False(failureCalled);

        return;
        async Task SuccessAsync() => await Task.Run(() => successCalled = true);
        async Task FailureAsync(Exception exception) => await Task.Run(() => failureCalled = true);
    }

    [Fact(DisplayName = "ValueTask<ValueResult>.MatchAsync of Success should invoke the success delegate (Func<ValueTask>, Func<Exception, ValueTask>)")]
    public async Task ValueTaskValueResultMatchAsyncOfSuccessShouldInvokeSuccessDelegateFuncTaskFuncExceptionTaskWithValueTaskDelegates()
    {
        // Given
        ValueResult result = ValueResult.Success();
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await ValueTask.FromResult(result).MatchAsync(SuccessAsync, FailureAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(successCalled);
        Assert.False(failureCalled);

        return;
        async ValueTask SuccessAsync() => await Task.Run(() => successCalled = true);
        async ValueTask FailureAsync(Exception exception) => await Task.Run(() => failureCalled = true);
    }

    [Fact(DisplayName = "ValueTask<ValueResult>.MatchAsync of Failure should invoke the failure delegate (Func<Task>, Func<Exception, Task>)")]
    public async Task ValueTaskValueResultMatchAsyncOfFailureShouldInvokeFailureDelegateFuncTaskFuncExceptionTask()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await ValueTask.FromResult(result).MatchAsync(SuccessAsync, FailureAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.False(successCalled);
        Assert.True(failureCalled);

        return;
        async Task SuccessAsync() => await Task.Run(() => successCalled = true);
        async Task FailureAsync(Exception exception) => await Task.Run(() => failureCalled = true);
    }

    [Fact(DisplayName = "ValueTask<ValueResult>.MatchAsync of Failure should invoke the failure delegate (Func<ValueTask>, Func<Exception, ValueTask>)")]
    public async Task ValueTaskValueResultMatchAsyncOfFailureShouldInvokeFailureDelegateFuncTaskFuncExceptionTaskWithValueTaskDelegates()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await ValueTask.FromResult(result).MatchAsync(SuccessAsync, FailureAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.False(successCalled);
        Assert.True(failureCalled);

        return;
        async ValueTask SuccessAsync() => await Task.Run(() => successCalled = true);
        async ValueTask FailureAsync(Exception exception) => await Task.Run(() => failureCalled = true);
    }

    [Fact(DisplayName = "ValueTask<ValueResult>.MatchAsync of Success should invoke the success delegate (Func<CancellationToken, Task>, Func<Exception, CancellationToken, Task>)")]
    public async Task ValueTaskValueResultMatchAsyncOfSuccessShouldInvokeSuccessDelegateFuncCancellationTokenTaskFuncExceptionCancellationTokenTask()
    {
        // Given
        ValueResult result = ValueResult.Success();
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await ValueTask.FromResult(result).MatchAsync(SuccessAsync, FailureAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(successCalled);
        Assert.False(failureCalled);

        return;
        async Task SuccessAsync(CancellationToken ct) => await Task.Run(() => successCalled = true, ct);
        async Task FailureAsync(Exception exception, CancellationToken ct) => await Task.Run(() => failureCalled = true, ct);
    }

    [Fact(DisplayName = "ValueTask<ValueResult>.MatchAsync of Success should invoke the success delegate (Func<CancellationToken, ValueTask>, Func<Exception, CancellationToken, ValueTask>)")]
    public async Task ValueTaskValueResultMatchAsyncOfSuccessShouldInvokeSuccessDelegateFuncCancellationTokenTaskFuncExceptionCancellationTokenTaskWithValueTaskDelegates()
    {
        // Given
        ValueResult result = ValueResult.Success();
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await ValueTask.FromResult(result).MatchAsync(SuccessAsync, FailureAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(successCalled);
        Assert.False(failureCalled);

        return;
        async ValueTask SuccessAsync(CancellationToken ct) => await Task.Run(() => successCalled = true, ct);
        async ValueTask FailureAsync(Exception exception, CancellationToken ct) => await Task.Run(() => failureCalled = true, ct);
    }

    [Fact(DisplayName = "ValueTask<ValueResult>.MatchAsync of Failure should invoke the failure delegate (Func<CancellationToken, Task>, Func<Exception, CancellationToken, Task>)")]
    public async Task ValueTaskValueResultMatchAsyncOfFailureShouldInvokeFailureDelegateFuncCancellationTokenTaskFuncExceptionCancellationTokenTask()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await ValueTask.FromResult(result).MatchAsync(SuccessAsync, FailureAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.False(successCalled);
        Assert.True(failureCalled);

        return;
        async Task SuccessAsync(CancellationToken ct) => await Task.Run(() => successCalled = true, ct);
        async Task FailureAsync(Exception exception, CancellationToken ct) => await Task.Run(() => failureCalled = true, ct);
    }

    [Fact(DisplayName = "ValueTask<ValueResult>.MatchAsync of Failure should invoke the failure delegate (Func<CancellationToken, ValueTask>, Func<Exception, CancellationToken, ValueTask>)")]
    public async Task ValueTaskValueResultMatchAsyncOfFailureShouldInvokeFailureDelegateFuncCancellationTokenTaskFuncExceptionCancellationTokenTaskWithValueTaskDelegates()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await ValueTask.FromResult(result).MatchAsync(SuccessAsync, FailureAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.False(successCalled);
        Assert.True(failureCalled);

        return;
        async ValueTask SuccessAsync(CancellationToken ct) => await Task.Run(() => successCalled = true, ct);
        async ValueTask FailureAsync(Exception exception, CancellationToken ct) => await Task.Run(() => failureCalled = true, ct);
    }

    [Fact(DisplayName = "ValueTask<ValueResult>.MatchAsync<TResult> of Success should return the result from success delegate (Func<TResult>, Func<Exception, TResult>)")]
    public async Task ValueTaskValueResultMatchAsyncOfSuccessShouldReturnResultFromSuccessDelegateFuncResultFuncExceptionResult()
    {
        // Given
        ValueResult result = ValueResult.Success();
        const string expected = "Success";

        // When
        string actual = await ValueTask.FromResult(result).MatchAsync(Success, Failure, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        string Success() => "Success";
        string Failure(Exception exception) => "Failure";
    }

    [Fact(DisplayName = "ValueTask<ValueResult>.MatchAsync<TResult> of Failure should return the result from failure delegate (Func<TResult>, Func<Exception, TResult>)")]
    public async Task ValueTaskValueResultMatchAsyncOfFailureShouldReturnResultFromFailureDelegateFuncResultFuncExceptionResult()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
        const string expected = "Failure";

        // When
        string actual = await ValueTask.FromResult(result).MatchAsync(Success, Failure, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        string Success() => "Success";
        string Failure(Exception exception) => "Failure";
    }

    [Fact(DisplayName = "ValueTask<ValueResult>.MatchAsync<TResult> of Success should return the result from success delegate (Func<Task<TResult>>, Func<Exception, TResult>)")]
    public async Task ValueTaskValueResultMatchAsyncOfSuccessShouldReturnResultFromSuccessDelegateFuncTaskResultFuncExceptionResult()
    {
        // Given
        ValueResult result = ValueResult.Success();
        const string expected = "Success";

        // When
        string actual = await ValueTask.FromResult(result).MatchAsync(SuccessAsync, Failure, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        async Task<string> SuccessAsync() => await Task.FromResult("Success");
        string Failure(Exception exception) => "Failure";
    }

    [Fact(DisplayName = "ValueTask<ValueResult>.MatchAsync<TResult> of Success should return the result from success delegate (Func<ValueTask<TResult>>, Func<Exception, TResult>)")]
    public async Task ValueTaskValueResultMatchAsyncOfSuccessShouldReturnResultFromSuccessDelegateFuncTaskResultFuncExceptionResultWithValueTaskDelegates()
    {
        // Given
        ValueResult result = ValueResult.Success();
        const string expected = "Success";

        // When
        string actual = await ValueTask.FromResult(result).MatchAsync(SuccessAsync, Failure, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        async ValueTask<string> SuccessAsync() => await ValueTask.FromResult("Success");
        string Failure(Exception exception) => "Failure";
    }

    [Fact(DisplayName = "ValueTask<ValueResult>.MatchAsync<TResult> of Failure should return the result from failure delegate (Func<Task<TResult>>, Func<Exception, TResult>)")]
    public async Task ValueTaskValueResultMatchAsyncOfFailureShouldReturnResultFromFailureDelegateFuncTaskResultFuncExceptionResult()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
        const string expected = "Failure";

        // When
        string actual = await ValueTask.FromResult(result).MatchAsync(SuccessAsync, Failure, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        async Task<string> SuccessAsync() => await Task.FromResult("Success");
        string Failure(Exception exception) => "Failure";
    }

    [Fact(DisplayName = "ValueTask<ValueResult>.MatchAsync<TResult> of Failure should return the result from failure delegate (Func<ValueTask<TResult>>, Func<Exception, TResult>)")]
    public async Task ValueTaskValueResultMatchAsyncOfFailureShouldReturnResultFromFailureDelegateFuncTaskResultFuncExceptionResultWithValueTaskDelegates()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
        const string expected = "Failure";

        // When
        string actual = await ValueTask.FromResult(result).MatchAsync(SuccessAsync, Failure, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        async ValueTask<string> SuccessAsync() => await ValueTask.FromResult("Success");
        string Failure(Exception exception) => "Failure";
    }

    [Fact(DisplayName = "ValueTask<ValueResult>.MatchAsync<TResult> of Success should return the result from success delegate (Func<CancellationToken, Task<TResult>>, Func<Exception, TResult>)")]
    public async Task ValueTaskValueResultMatchAsyncOfSuccessShouldReturnResultFromSuccessDelegateFuncCancellationTokenTaskResultFuncExceptionResult()
    {
        // Given
        ValueResult result = ValueResult.Success();
        const string expected = "Success";

        // When
        string actual = await ValueTask.FromResult(result).MatchAsync(SuccessAsync, Failure, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        async Task<string> SuccessAsync(CancellationToken ct) => await Task.FromResult("Success");
        string Failure(Exception exception) => "Failure";
    }

    [Fact(DisplayName = "ValueTask<ValueResult>.MatchAsync<TResult> of Success should return the result from success delegate (Func<CancellationToken, ValueTask<TResult>>, Func<Exception, TResult>)")]
    public async Task ValueTaskValueResultMatchAsyncOfSuccessShouldReturnResultFromSuccessDelegateFuncCancellationTokenTaskResultFuncExceptionResultWithValueTaskDelegates()
    {
        // Given
        ValueResult result = ValueResult.Success();
        const string expected = "Success";

        // When
        string actual = await ValueTask.FromResult(result).MatchAsync(SuccessAsync, Failure, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        async ValueTask<string> SuccessAsync(CancellationToken ct) => await ValueTask.FromResult("Success");
        string Failure(Exception exception) => "Failure";
    }

    [Fact(DisplayName = "ValueTask<ValueResult>.MatchAsync<TResult> of Failure should return the result from failure delegate (Func<CancellationToken, Task<TResult>>, Func<Exception, TResult>)")]
    public async Task ValueTaskValueResultMatchAsyncOfFailureShouldReturnResultFromFailureDelegateFuncCancellationTokenTaskResultFuncExceptionResult()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
        const string expected = "Failure";

        // When
        string actual = await ValueTask.FromResult(result).MatchAsync(SuccessAsync, Failure, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        async Task<string> SuccessAsync(CancellationToken ct) => await Task.FromResult("Success");
        string Failure(Exception exception) => "Failure";
    }

    [Fact(DisplayName = "ValueTask<ValueResult>.MatchAsync<TResult> of Failure should return the result from failure delegate (Func<CancellationToken, ValueTask<TResult>>, Func<Exception, TResult>)")]
    public async Task ValueTaskValueResultMatchAsyncOfFailureShouldReturnResultFromFailureDelegateFuncCancellationTokenTaskResultFuncExceptionResultWithValueTaskDelegates()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
        const string expected = "Failure";

        // When
        string actual = await ValueTask.FromResult(result).MatchAsync(SuccessAsync, Failure, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        async ValueTask<string> SuccessAsync(CancellationToken ct) => await ValueTask.FromResult("Success");
        string Failure(Exception exception) => "Failure";
    }

    [Fact(DisplayName = "ValueTask<ValueResult>.MatchAsync<TResult> of Success should return the result from success delegate (Func<TResult>, Func<Exception, Task<TResult>>>)")]
    public async Task ValueTaskValueResultMatchAsyncOfSuccessShouldReturnResultFromSuccessDelegateFuncResultFuncExceptionTaskResult()
    {
        // Given
        ValueResult result = ValueResult.Success();
        const string expected = "Success";

        // When
        string actual = await ValueTask.FromResult(result).MatchAsync(Success, FailureAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        string Success() => "Success";
        async Task<string> FailureAsync(Exception exception) => await Task.FromResult("Failure");
    }

    [Fact(DisplayName = "ValueTask<ValueResult>.MatchAsync<TResult> of Success should return the result from success delegate (Func<TResult>, Func<Exception, ValueTask<TResult>>>)")]
    public async Task ValueTaskValueResultMatchAsyncOfSuccessShouldReturnResultFromSuccessDelegateFuncResultFuncExceptionTaskResultWithValueTaskDelegates()
    {
        // Given
        ValueResult result = ValueResult.Success();
        const string expected = "Success";

        // When
        string actual = await ValueTask.FromResult(result).MatchAsync(Success, FailureAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        string Success() => "Success";
        async ValueTask<string> FailureAsync(Exception exception) => await ValueTask.FromResult("Failure");
    }

    [Fact(DisplayName = "ValueTask<ValueResult>.MatchAsync<TResult> of Failure should return the result from failure delegate (Func<TResult>, Func<Exception, Task<TResult>>>)")]
    public async Task ValueTaskValueResultMatchAsyncOfFailureShouldReturnResultFromFailureDelegateFuncResultFuncExceptionTaskResult()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
        const string expected = "Failure";

        // When
        string actual = await ValueTask.FromResult(result).MatchAsync(Success, FailureAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        string Success() => "Success";
        async Task<string> FailureAsync(Exception exception) => await Task.FromResult("Failure");
    }

    [Fact(DisplayName = "ValueTask<ValueResult>.MatchAsync<TResult> of Failure should return the result from failure delegate (Func<TResult>, Func<Exception, ValueTask<TResult>>>)")]
    public async Task ValueTaskValueResultMatchAsyncOfFailureShouldReturnResultFromFailureDelegateFuncResultFuncExceptionTaskResultWithValueTaskDelegates()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
        const string expected = "Failure";

        // When
        string actual = await ValueTask.FromResult(result).MatchAsync(Success, FailureAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        string Success() => "Success";
        async ValueTask<string> FailureAsync(Exception exception) => await ValueTask.FromResult("Failure");
    }

    [Fact(DisplayName = "ValueTask<ValueResult>.MatchAsync<TResult> of Success should return the result from success delegate (Func<TResult>, Func<Exception, CancellationToken, Task<TResult>>>)")]
    public async Task ValueTaskValueResultMatchAsyncOfSuccessShouldReturnResultFromSuccessDelegateFuncResultFuncExceptionCancellationTokenTaskResult()
    {
        // Given
        ValueResult result = ValueResult.Success();
        const string expected = "Success";

        // When
        string actual = await ValueTask.FromResult(result).MatchAsync(Success, FailureAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        string Success() => "Success";
        async Task<string> FailureAsync(Exception exception, CancellationToken ct) => await Task.FromResult("Failure");
    }

    [Fact(DisplayName = "ValueTask<ValueResult>.MatchAsync<TResult> of Success should return the result from success delegate (Func<TResult>, Func<Exception, CancellationToken, ValueTask<TResult>>>)")]
    public async Task ValueTaskValueResultMatchAsyncOfSuccessShouldReturnResultFromSuccessDelegateFuncResultFuncExceptionCancellationTokenTaskResultWithValueTaskDelegates()
    {
        // Given
        ValueResult result = ValueResult.Success();
        const string expected = "Success";

        // When
        string actual = await ValueTask.FromResult(result).MatchAsync(Success, FailureAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        string Success() => "Success";
        async ValueTask<string> FailureAsync(Exception exception, CancellationToken ct) => await ValueTask.FromResult("Failure");
    }

    [Fact(DisplayName = "ValueTask<ValueResult>.MatchAsync<TResult> of Failure should return the result from failure delegate (Func<TResult>, Func<Exception, CancellationToken, Task<TResult>>>)")]
    public async Task ValueTaskValueResultMatchAsyncOfFailureShouldReturnResultFromFailureDelegateFuncResultFuncExceptionCancellationTokenTaskResult()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
        const string expected = "Failure";

        // When
        string actual = await ValueTask.FromResult(result).MatchAsync(Success, FailureAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        string Success() => "Success";
        async Task<string> FailureAsync(Exception exception, CancellationToken ct) => await Task.FromResult("Failure");
    }

    [Fact(DisplayName = "ValueTask<ValueResult>.MatchAsync<TResult> of Failure should return the result from failure delegate (Func<TResult>, Func<Exception, CancellationToken, ValueTask<TResult>>>)")]
    public async Task ValueTaskValueResultMatchAsyncOfFailureShouldReturnResultFromFailureDelegateFuncResultFuncExceptionCancellationTokenTaskResultWithValueTaskDelegates()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
        const string expected = "Failure";

        // When
        string actual = await ValueTask.FromResult(result).MatchAsync(Success, FailureAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        string Success() => "Success";
        async ValueTask<string> FailureAsync(Exception exception, CancellationToken ct) => await ValueTask.FromResult("Failure");
    }

    [Fact(DisplayName = "ValueTask<ValueResult>.MatchAsync<TResult> of Success should return the result from success delegate (Func<Task<TResult>>, Func<Exception, Task<TResult>>>)")]
    public async Task ValueTaskValueResultMatchAsyncOfSuccessShouldReturnResultFromSuccessDelegateFuncTaskResultFuncExceptionTaskResult()
    {
        // Given
        ValueResult result = ValueResult.Success();
        const string expected = "Success";

        // When
        string actual = await ValueTask.FromResult(result).MatchAsync(SuccessAsync, FailureAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        async Task<string> SuccessAsync() => await Task.FromResult("Success");
        async Task<string> FailureAsync(Exception exception) => await Task.FromResult("Failure");
    }

    [Fact(DisplayName = "ValueTask<ValueResult>.MatchAsync<TResult> of Success should return the result from success delegate (Func<ValueTask<TResult>>, Func<Exception, ValueTask<TResult>>>)")]
    public async Task ValueTaskValueResultMatchAsyncOfSuccessShouldReturnResultFromSuccessDelegateFuncTaskResultFuncExceptionTaskResultWithValueTaskDelegates()
    {
        // Given
        ValueResult result = ValueResult.Success();
        const string expected = "Success";

        // When
        string actual = await ValueTask.FromResult(result).MatchAsync(SuccessAsync, FailureAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        async ValueTask<string> SuccessAsync() => await ValueTask.FromResult("Success");
        async ValueTask<string> FailureAsync(Exception exception) => await ValueTask.FromResult("Failure");
    }

    [Fact(DisplayName = "ValueTask<ValueResult>.MatchAsync<TResult> of Failure should return the result from failure delegate (Func<Task<TResult>>, Func<Exception, Task<TResult>>>)")]
    public async Task ValueTaskValueResultMatchAsyncOfFailureShouldReturnResultFromFailureDelegateFuncTaskResultFuncExceptionTaskResult()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
        const string expected = "Failure";

        // When
        string actual = await ValueTask.FromResult(result).MatchAsync(SuccessAsync, FailureAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        async Task<string> SuccessAsync() => await Task.FromResult("Success");
        async Task<string> FailureAsync(Exception exception) => await Task.FromResult("Failure");
    }

    [Fact(DisplayName = "ValueTask<ValueResult>.MatchAsync<TResult> of Failure should return the result from failure delegate (Func<ValueTask<TResult>>, Func<Exception, ValueTask<TResult>>>)")]
    public async Task ValueTaskValueResultMatchAsyncOfFailureShouldReturnResultFromFailureDelegateFuncTaskResultFuncExceptionTaskResultWithValueTaskDelegates()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
        const string expected = "Failure";

        // When
        string actual = await ValueTask.FromResult(result).MatchAsync(SuccessAsync, FailureAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        async ValueTask<string> SuccessAsync() => await ValueTask.FromResult("Success");
        async ValueTask<string> FailureAsync(Exception exception) => await ValueTask.FromResult("Failure");
    }

    [Fact(DisplayName = "ValueTask<ValueResult>.MatchAsync<TResult> of Success should return the result from success delegate (Func<CancellationToken, Task<TResult>>, Func<Exception, CancellationToken, Task<TResult>>>)")]
    public async Task ValueTaskValueResultMatchAsyncOfSuccessShouldReturnResultFromSuccessDelegateFuncCancellationTokenTaskResultFuncExceptionCancellationTokenTaskResult()
    {
        // Given
        ValueResult result = ValueResult.Success();
        const string expected = "Success";

        // When
        string actual = await ValueTask.FromResult(result).MatchAsync(SuccessAsync, FailureAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        async Task<string> SuccessAsync(CancellationToken ct) => await Task.FromResult("Success");
        async Task<string> FailureAsync(Exception exception, CancellationToken ct) => await Task.FromResult("Failure");
    }

    [Fact(DisplayName = "ValueTask<ValueResult>.MatchAsync<TResult> of Success should return the result from success delegate (Func<CancellationToken, ValueTask<TResult>>, Func<Exception, CancellationToken, ValueTask<TResult>>>)")]
    public async Task ValueTaskValueResultMatchAsyncOfSuccessShouldReturnResultFromSuccessDelegateFuncCancellationTokenTaskResultFuncExceptionCancellationTokenTaskResultWithValueTaskDelegates()
    {
        // Given
        ValueResult result = ValueResult.Success();
        const string expected = "Success";

        // When
        string actual = await ValueTask.FromResult(result).MatchAsync(SuccessAsync, FailureAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        async ValueTask<string> SuccessAsync(CancellationToken ct) => await ValueTask.FromResult("Success");
        async ValueTask<string> FailureAsync(Exception exception, CancellationToken ct) => await ValueTask.FromResult("Failure");
    }

    [Fact(DisplayName = "ValueTask<ValueResult>.MatchAsync<TResult> of Failure should return the result from failure delegate (Func<CancellationToken, Task<TResult>>, Func<Exception, CancellationToken, Task<TResult>>>)")]
    public async Task ValueTaskValueResultMatchAsyncOfFailureShouldReturnResultFromFailureDelegateFuncCancellationTokenTaskResultFuncExceptionCancellationTokenTaskResult()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
        const string expected = "Failure";

        // When
        string actual = await ValueTask.FromResult(result).MatchAsync(SuccessAsync, FailureAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        async Task<string> SuccessAsync(CancellationToken ct) => await Task.FromResult("Success");
        async Task<string> FailureAsync(Exception exception, CancellationToken ct) => await Task.FromResult("Failure");
    }

    [Fact(DisplayName = "ValueTask<ValueResult>.MatchAsync<TResult> of Failure should return the result from failure delegate (Func<CancellationToken, ValueTask<TResult>>, Func<Exception, CancellationToken, ValueTask<TResult>>>)")]
    public async Task ValueTaskValueResultMatchAsyncOfFailureShouldReturnResultFromFailureDelegateFuncCancellationTokenTaskResultFuncExceptionCancellationTokenTaskResultWithValueTaskDelegates()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
        const string expected = "Failure";

        // When
        string actual = await ValueTask.FromResult(result).MatchAsync(SuccessAsync, FailureAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        async ValueTask<string> SuccessAsync(CancellationToken ct) => await ValueTask.FromResult("Success");
        async ValueTask<string> FailureAsync(Exception exception, CancellationToken ct) => await ValueTask.FromResult("Failure");
    }

    [Fact(DisplayName = "ValueTask<ValueResult<T>>.MatchAsync of Success should invoke the success delegate (Action<T>, Action<Exception>)")]
    public async Task ValueTaskValueResultOfTMatchAsyncOfSuccessShouldInvokeSuccessDelegateActionTActionException()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(42);
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await ValueTask.FromResult(result).MatchAsync(Success, Failure, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(successCalled);
        Assert.False(failureCalled);

        return;
        void Success(int value) => successCalled = value == 42;
        void Failure(Exception exception) => failureCalled = true;
    }

    [Fact(DisplayName = "ValueTask<ValueResult<T>>.MatchAsync of Failure should invoke the failure delegate (Action<T>, Action<Exception>)")]
    public async Task ValueTaskValueResultOfTMatchAsyncOfFailureShouldInvokeFailureDelegateActionTActionException()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await ValueTask.FromResult(result).MatchAsync(Success, Failure, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.False(successCalled);
        Assert.True(failureCalled);

        return;
        void Success(int value) => successCalled = true;
        void Failure(Exception exception) => failureCalled = true;
    }

    [Fact(DisplayName = "ValueTask<ValueResult<T>>.MatchAsync of Success should invoke the success delegate (Func<T, Task>, Action<Exception>)")]
    public async Task ValueTaskValueResultOfTMatchAsyncOfSuccessShouldInvokeSuccessDelegateFuncTTaskActionException()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(42);
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await ValueTask.FromResult(result).MatchAsync(SuccessAsync, Failure, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(successCalled);
        Assert.False(failureCalled);

        return;
        async Task SuccessAsync(int value) => await Task.Run(() => successCalled = value == 42);
        void Failure(Exception exception) => failureCalled = true;
    }

    [Fact(DisplayName = "ValueTask<ValueResult<T>>.MatchAsync of Success should invoke the success delegate (Func<T, ValueTask>, Action<Exception>)")]
    public async Task ValueTaskValueResultOfTMatchAsyncOfSuccessShouldInvokeSuccessDelegateFuncTTaskActionExceptionWithValueTaskDelegates()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(42);
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await ValueTask.FromResult(result).MatchAsync(SuccessAsync, Failure, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(successCalled);
        Assert.False(failureCalled);

        return;
        async ValueTask SuccessAsync(int value) => await Task.Run(() => successCalled = value == 42);
        void Failure(Exception exception) => failureCalled = true;
    }

    [Fact(DisplayName = "ValueTask<ValueResult<T>>.MatchAsync of Failure should invoke the failure delegate (Func<T, Task>, Action<Exception>)")]
    public async Task ValueTaskValueResultOfTMatchAsyncOfFailureShouldInvokeFailureDelegateFuncTTaskActionException()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await ValueTask.FromResult(result).MatchAsync(SuccessAsync, Failure, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.False(successCalled);
        Assert.True(failureCalled);

        return;
        async Task SuccessAsync(int value) => await Task.Run(() => successCalled = true);
        void Failure(Exception exception) => failureCalled = true;
    }

    [Fact(DisplayName = "ValueTask<ValueResult<T>>.MatchAsync of Failure should invoke the failure delegate (Func<T, ValueTask>, Action<Exception>)")]
    public async Task ValueTaskValueResultOfTMatchAsyncOfFailureShouldInvokeFailureDelegateFuncTTaskActionExceptionWithValueTaskDelegates()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await ValueTask.FromResult(result).MatchAsync(SuccessAsync, Failure, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.False(successCalled);
        Assert.True(failureCalled);

        return;
        async ValueTask SuccessAsync(int value) => await Task.Run(() => successCalled = true);
        void Failure(Exception exception) => failureCalled = true;
    }

    [Fact(DisplayName = "ValueTask<ValueResult<T>>.MatchAsync of Success should invoke the success delegate (Func<T, CancellationToken, Task>, Action<Exception>)")]
    public async Task ValueTaskValueResultOfTMatchAsyncOfSuccessShouldInvokeSuccessDelegateFuncTCancellationTokenTaskActionException()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(42);
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await ValueTask.FromResult(result).MatchAsync(SuccessAsync, Failure, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(successCalled);
        Assert.False(failureCalled);

        return;
        async Task SuccessAsync(int value, CancellationToken ct) => await Task.Run(() => successCalled = value == 42, ct);
        void Failure(Exception exception) => failureCalled = true;
    }

    [Fact(DisplayName = "ValueTask<ValueResult<T>>.MatchAsync of Success should invoke the success delegate (Func<T, CancellationToken, ValueTask>, Action<Exception>)")]
    public async Task ValueTaskValueResultOfTMatchAsyncOfSuccessShouldInvokeSuccessDelegateFuncTCancellationTokenTaskActionExceptionWithValueTaskDelegates()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(42);
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await ValueTask.FromResult(result).MatchAsync(SuccessAsync, Failure, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(successCalled);
        Assert.False(failureCalled);

        return;
        async ValueTask SuccessAsync(int value, CancellationToken ct) => await Task.Run(() => successCalled = value == 42, ct);
        void Failure(Exception exception) => failureCalled = true;
    }

    [Fact(DisplayName = "ValueTask<ValueResult<T>>.MatchAsync of Failure should invoke the failure delegate (Func<T, CancellationToken, Task>, Action<Exception>)")]
    public async Task ValueTaskValueResultOfTMatchAsyncOfFailureShouldInvokeFailureDelegateFuncTCancellationTokenTaskActionException()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await ValueTask.FromResult(result).MatchAsync(SuccessAsync, Failure, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.False(successCalled);
        Assert.True(failureCalled);

        return;
        async Task SuccessAsync(int value, CancellationToken ct) => await Task.Run(() => successCalled = true, ct);
        void Failure(Exception exception) => failureCalled = true;
    }

    [Fact(DisplayName = "ValueTask<ValueResult<T>>.MatchAsync of Failure should invoke the failure delegate (Func<T, CancellationToken, ValueTask>, Action<Exception>)")]
    public async Task ValueTaskValueResultOfTMatchAsyncOfFailureShouldInvokeFailureDelegateFuncTCancellationTokenTaskActionExceptionWithValueTaskDelegates()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await ValueTask.FromResult(result).MatchAsync(SuccessAsync, Failure, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.False(successCalled);
        Assert.True(failureCalled);

        return;
        async ValueTask SuccessAsync(int value, CancellationToken ct) => await Task.Run(() => successCalled = true, ct);
        void Failure(Exception exception) => failureCalled = true;
    }

    [Fact(DisplayName = "ValueTask<ValueResult<T>>.MatchAsync of Success should invoke the success delegate (Action<T>, Func<Exception, Task>)")]
    public async Task ValueTaskValueResultOfTMatchAsyncOfSuccessShouldInvokeSuccessDelegateActionTFuncExceptionTask()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(42);
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await ValueTask.FromResult(result).MatchAsync(Success, FailureAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(successCalled);
        Assert.False(failureCalled);

        return;
        void Success(int value) => successCalled = value == 42;
        async Task FailureAsync(Exception exception) => await Task.Run(() => failureCalled = true);
    }

    [Fact(DisplayName = "ValueTask<ValueResult<T>>.MatchAsync of Success should invoke the success delegate (Action<T>, Func<Exception, ValueTask>)")]
    public async Task ValueTaskValueResultOfTMatchAsyncOfSuccessShouldInvokeSuccessDelegateActionTFuncExceptionTaskWithValueTaskDelegates()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(42);
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await ValueTask.FromResult(result).MatchAsync(Success, FailureAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(successCalled);
        Assert.False(failureCalled);

        return;
        void Success(int value) => successCalled = value == 42;
        async ValueTask FailureAsync(Exception exception) => await Task.Run(() => failureCalled = true);
    }

    [Fact(DisplayName = "ValueTask<ValueResult<T>>.MatchAsync of Failure should invoke the failure delegate (Action<T>, Func<Exception, Task>)")]
    public async Task ValueTaskValueResultOfTMatchAsyncOfFailureShouldInvokeFailureDelegateActionTFuncExceptionTask()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await ValueTask.FromResult(result).MatchAsync(Success, FailureAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.False(successCalled);
        Assert.True(failureCalled);

        return;
        void Success(int value) => successCalled = true;
        async Task FailureAsync(Exception exception) => await Task.Run(() => failureCalled = true);
    }

    [Fact(DisplayName = "ValueTask<ValueResult<T>>.MatchAsync of Failure should invoke the failure delegate (Action<T>, Func<Exception, ValueTask>)")]
    public async Task ValueTaskValueResultOfTMatchAsyncOfFailureShouldInvokeFailureDelegateActionTFuncExceptionTaskWithValueTaskDelegates()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await ValueTask.FromResult(result).MatchAsync(Success, FailureAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.False(successCalled);
        Assert.True(failureCalled);

        return;
        void Success(int value) => successCalled = true;
        async ValueTask FailureAsync(Exception exception) => await Task.Run(() => failureCalled = true);
    }

    [Fact(DisplayName = "ValueTask<ValueResult<T>>.MatchAsync of Success should invoke the success delegate (Action<T>, Func<Exception, CancellationToken, Task>)")]
    public async Task ValueTaskValueResultOfTMatchAsyncOfSuccessShouldInvokeSuccessDelegateActionTFuncExceptionCancellationTokenTask()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(42);
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await ValueTask.FromResult(result).MatchAsync(Success, FailureAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(successCalled);
        Assert.False(failureCalled);

        return;
        void Success(int value) => successCalled = value == 42;
        async Task FailureAsync(Exception exception, CancellationToken ct) => await Task.Run(() => failureCalled = true, ct);
    }

    [Fact(DisplayName = "ValueTask<ValueResult<T>>.MatchAsync of Success should invoke the success delegate (Action<T>, Func<Exception, CancellationToken, ValueTask>)")]
    public async Task ValueTaskValueResultOfTMatchAsyncOfSuccessShouldInvokeSuccessDelegateActionTFuncExceptionCancellationTokenTaskWithValueTaskDelegates()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(42);
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await ValueTask.FromResult(result).MatchAsync(Success, FailureAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(successCalled);
        Assert.False(failureCalled);

        return;
        void Success(int value) => successCalled = value == 42;
        async ValueTask FailureAsync(Exception exception, CancellationToken ct) => await Task.Run(() => failureCalled = true, ct);
    }

    [Fact(DisplayName = "ValueTask<ValueResult<T>>.MatchAsync of Failure should invoke the failure delegate (Action<T>, Func<Exception, CancellationToken, Task>)")]
    public async Task ValueTaskValueResultOfTMatchAsyncOfFailureShouldInvokeFailureDelegateActionTFuncExceptionCancellationTokenTask()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await ValueTask.FromResult(result).MatchAsync(Success, FailureAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.False(successCalled);
        Assert.True(failureCalled);

        return;
        void Success(int value) => successCalled = true;
        async Task FailureAsync(Exception exception, CancellationToken ct) => await Task.Run(() => failureCalled = true, ct);
    }

    [Fact(DisplayName = "ValueTask<ValueResult<T>>.MatchAsync of Failure should invoke the failure delegate (Action<T>, Func<Exception, CancellationToken, ValueTask>)")]
    public async Task ValueTaskValueResultOfTMatchAsyncOfFailureShouldInvokeFailureDelegateActionTFuncExceptionCancellationTokenTaskWithValueTaskDelegates()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await ValueTask.FromResult(result).MatchAsync(Success, FailureAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.False(successCalled);
        Assert.True(failureCalled);

        return;
        void Success(int value) => successCalled = true;
        async ValueTask FailureAsync(Exception exception, CancellationToken ct) => await Task.Run(() => failureCalled = true, ct);
    }

    [Fact(DisplayName = "ValueTask<ValueResult<T>>.MatchAsync of Success should invoke the success delegate (Func<T, Task>, Func<Exception, Task>)")]
    public async Task ValueTaskValueResultOfTMatchAsyncOfSuccessShouldInvokeSuccessDelegateFuncTTaskFuncExceptionTask()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(42);
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await ValueTask.FromResult(result).MatchAsync(SuccessAsync, FailureAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(successCalled);
        Assert.False(failureCalled);

        return;
        async Task SuccessAsync(int value) => await Task.Run(() => successCalled = value == 42);
        async Task FailureAsync(Exception exception) => await Task.Run(() => failureCalled = true);
    }

    [Fact(DisplayName = "ValueTask<ValueResult<T>>.MatchAsync of Success should invoke the success delegate (Func<T, ValueTask>, Func<Exception, ValueTask>)")]
    public async Task ValueTaskValueResultOfTMatchAsyncOfSuccessShouldInvokeSuccessDelegateFuncTTaskFuncExceptionTaskWithValueTaskDelegates()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(42);
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await ValueTask.FromResult(result).MatchAsync(SuccessAsync, FailureAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(successCalled);
        Assert.False(failureCalled);

        return;
        async ValueTask SuccessAsync(int value) => await Task.Run(() => successCalled = value == 42);
        async ValueTask FailureAsync(Exception exception) => await Task.Run(() => failureCalled = true);
    }

    [Fact(DisplayName = "ValueTask<ValueResult<T>>.MatchAsync of Failure should invoke the failure delegate (Func<T, Task>, Func<Exception, Task>)")]
    public async Task ValueTaskValueResultOfTMatchAsyncOfFailureShouldInvokeFailureDelegateFuncTTaskFuncExceptionTask()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await ValueTask.FromResult(result).MatchAsync(SuccessAsync, FailureAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.False(successCalled);
        Assert.True(failureCalled);

        return;
        async Task SuccessAsync(int value) => await Task.Run(() => successCalled = true);
        async Task FailureAsync(Exception exception) => await Task.Run(() => failureCalled = true);
    }

    [Fact(DisplayName = "ValueTask<ValueResult<T>>.MatchAsync of Failure should invoke the failure delegate (Func<T, ValueTask>, Func<Exception, ValueTask>)")]
    public async Task ValueTaskValueResultOfTMatchAsyncOfFailureShouldInvokeFailureDelegateFuncTTaskFuncExceptionTaskWithValueTaskDelegates()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await ValueTask.FromResult(result).MatchAsync(SuccessAsync, FailureAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.False(successCalled);
        Assert.True(failureCalled);

        return;
        async ValueTask SuccessAsync(int value) => await Task.Run(() => successCalled = true);
        async ValueTask FailureAsync(Exception exception) => await Task.Run(() => failureCalled = true);
    }

    [Fact(DisplayName = "ValueTask<ValueResult<T>>.MatchAsync of Success should invoke the success delegate (Func<T, CancellationToken, Task>, Func<Exception, CancellationToken, Task>)")]
    public async Task ValueTaskValueResultOfTMatchAsyncOfSuccessShouldInvokeSuccessDelegateFuncTCancellationTokenTaskFuncExceptionCancellationTokenTask()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(42);
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await ValueTask.FromResult(result).MatchAsync(SuccessAsync, FailureAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(successCalled);
        Assert.False(failureCalled);

        return;
        async Task SuccessAsync(int value, CancellationToken ct) => await Task.Run(() => successCalled = value == 42, ct);
        async Task FailureAsync(Exception exception, CancellationToken ct) => await Task.Run(() => failureCalled = true, ct);
    }

    [Fact(DisplayName = "ValueTask<ValueResult<T>>.MatchAsync of Success should invoke the success delegate (Func<T, CancellationToken, ValueTask>, Func<Exception, CancellationToken, ValueTask>)")]
    public async Task ValueTaskValueResultOfTMatchAsyncOfSuccessShouldInvokeSuccessDelegateFuncTCancellationTokenTaskFuncExceptionCancellationTokenTaskWithValueTaskDelegates()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(42);
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await ValueTask.FromResult(result).MatchAsync(SuccessAsync, FailureAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(successCalled);
        Assert.False(failureCalled);

        return;
        async ValueTask SuccessAsync(int value, CancellationToken ct) => await Task.Run(() => successCalled = value == 42, ct);
        async ValueTask FailureAsync(Exception exception, CancellationToken ct) => await Task.Run(() => failureCalled = true, ct);
    }

    [Fact(DisplayName = "ValueTask<ValueResult<T>>.MatchAsync of Failure should invoke the failure delegate (Func<T, CancellationToken, Task>, Func<Exception, CancellationToken, Task>)")]
    public async Task ValueTaskValueResultOfTMatchAsyncOfFailureShouldInvokeFailureDelegateFuncTCancellationTokenTaskFuncExceptionCancellationTokenTask()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await ValueTask.FromResult(result).MatchAsync(SuccessAsync, FailureAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.False(successCalled);
        Assert.True(failureCalled);

        return;
        async Task SuccessAsync(int value, CancellationToken ct) => await Task.Run(() => successCalled = true, ct);
        async Task FailureAsync(Exception exception, CancellationToken ct) => await Task.Run(() => failureCalled = true, ct);
    }

    [Fact(DisplayName = "ValueTask<ValueResult<T>>.MatchAsync of Failure should invoke the failure delegate (Func<T, CancellationToken, ValueTask>, Func<Exception, CancellationToken, ValueTask>)")]
    public async Task ValueTaskValueResultOfTMatchAsyncOfFailureShouldInvokeFailureDelegateFuncTCancellationTokenTaskFuncExceptionCancellationTokenTaskWithValueTaskDelegates()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await ValueTask.FromResult(result).MatchAsync(SuccessAsync, FailureAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.False(successCalled);
        Assert.True(failureCalled);

        return;
        async ValueTask SuccessAsync(int value, CancellationToken ct) => await Task.Run(() => successCalled = true, ct);
        async ValueTask FailureAsync(Exception exception, CancellationToken ct) => await Task.Run(() => failureCalled = true, ct);
    }

    [Fact(DisplayName = "ValueTask<ValueResult<T>>.MatchAsync<TResult> of Success should return result from success delegate (Func<T, TResult>, Func<Exception, TResult>)")]
    public async Task ValueTaskValueResultOfTMatchAsyncOfSuccessShouldReturnResultFromSuccessDelegateFuncTResultFuncExceptionResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(42);
        const string expected = "Success";

        // When
        string actual = await ValueTask.FromResult(result).MatchAsync(Success, Failure, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        string Success(int value) => value == 42 ? "Success" : "Failure";
        string Failure(Exception exception) => "Failure";
    }

    [Fact(DisplayName = "ValueTask<ValueResult<T>>.MatchAsync<TResult> of Failure should return result from failure delegate (Func<T, TResult>, Func<Exception, TResult>)")]
    public async Task ValueTaskValueResultOfTMatchAsyncOfFailureShouldReturnResultFromFailureDelegateFuncTResultFuncExceptionResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        const string expected = "Failure";

        // When
        string actual = await ValueTask.FromResult(result).MatchAsync(Success, Failure, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        string Success(int value) => "Success";
        string Failure(Exception exception) => "Failure";
    }

    [Fact(DisplayName = "ValueTask<ValueResult<T>>.MatchAsync<TResult> of Success should return result from success delegate (Func<T, Task<TResult>>, Func<Exception, TResult>)")]
    public async Task ValueTaskValueResultOfTMatchAsyncOfSuccessShouldReturnResultFromSuccessDelegateFuncTTaskResultFuncExceptionResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(42);
        const string expected = "Success";

        // When
        string actual = await ValueTask.FromResult(result).MatchAsync(SuccessAsync, Failure, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        async Task<string> SuccessAsync(int value) => await Task.FromResult(value == 42 ? "Success" : "Failure");
        string Failure(Exception exception) => "Failure";
    }

    [Fact(DisplayName = "ValueTask<ValueResult<T>>.MatchAsync<TResult> of Success should return result from success delegate (Func<T, ValueTask<TResult>>, Func<Exception, TResult>)")]
    public async Task ValueTaskValueResultOfTMatchAsyncOfSuccessShouldReturnResultFromSuccessDelegateFuncTTaskResultFuncExceptionResultWithValueTaskDelegates()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(42);
        const string expected = "Success";

        // When
        string actual = await ValueTask.FromResult(result).MatchAsync(SuccessAsync, Failure, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        async ValueTask<string> SuccessAsync(int value) => await ValueTask.FromResult(value == 42 ? "Success" : "Failure");
        string Failure(Exception exception) => "Failure";
    }

    [Fact(DisplayName = "ValueTask<ValueResult<T>>.MatchAsync<TResult> of Failure should return result from failure delegate (Func<T, Task<TResult>>, Func<Exception, TResult>)")]
    public async Task ValueTaskValueResultOfTMatchAsyncOfFailureShouldReturnResultFromFailureDelegateFuncTTaskResultFuncExceptionResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        const string expected = "Failure";

        // When
        string actual = await ValueTask.FromResult(result).MatchAsync(SuccessAsync, Failure, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        async Task<string> SuccessAsync(int value) => await Task.FromResult("Success");
        string Failure(Exception exception) => "Failure";
    }

    [Fact(DisplayName = "ValueTask<ValueResult<T>>.MatchAsync<TResult> of Failure should return result from failure delegate (Func<T, ValueTask<TResult>>, Func<Exception, TResult>)")]
    public async Task ValueTaskValueResultOfTMatchAsyncOfFailureShouldReturnResultFromFailureDelegateFuncTTaskResultFuncExceptionResultWithValueTaskDelegates()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        const string expected = "Failure";

        // When
        string actual = await ValueTask.FromResult(result).MatchAsync(SuccessAsync, Failure, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        async ValueTask<string> SuccessAsync(int value) => await ValueTask.FromResult("Success");
        string Failure(Exception exception) => "Failure";
    }

    [Fact(DisplayName = "ValueTask<ValueResult<T>>.MatchAsync<TResult> of Success should return result from success delegate (Func<T, CancellationToken, Task<TResult>>, Func<Exception, TResult>)")]
    public async Task ValueTaskValueResultOfTMatchAsyncOfSuccessShouldReturnResultFromSuccessDelegateFuncTCancellationTokenTaskResultFuncExceptionResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(42);
        const string expected = "Success";

        // When
        string actual = await ValueTask.FromResult(result).MatchAsync(SuccessAsync, Failure, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        async Task<string> SuccessAsync(int value, CancellationToken ct) => await Task.FromResult(value == 42 ? "Success" : "Failure");
        string Failure(Exception exception) => "Failure";
    }

    [Fact(DisplayName = "ValueTask<ValueResult<T>>.MatchAsync<TResult> of Success should return result from success delegate (Func<T, CancellationToken, ValueTask<TResult>>, Func<Exception, TResult>)")]
    public async Task ValueTaskValueResultOfTMatchAsyncOfSuccessShouldReturnResultFromSuccessDelegateFuncTCancellationTokenTaskResultFuncExceptionResultWithValueTaskDelegates()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(42);
        const string expected = "Success";

        // When
        string actual = await ValueTask.FromResult(result).MatchAsync(SuccessAsync, Failure, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        async ValueTask<string> SuccessAsync(int value, CancellationToken ct) => await ValueTask.FromResult(value == 42 ? "Success" : "Failure");
        string Failure(Exception exception) => "Failure";
    }

    [Fact(DisplayName = "ValueTask<ValueResult<T>>.MatchAsync<TResult> of Failure should return result from failure delegate (Func<T, CancellationToken, Task<TResult>>, Func<Exception, TResult>)")]
    public async Task ValueTaskValueResultOfTMatchAsyncOfFailureShouldReturnResultFromFailureDelegateFuncTCancellationTokenTaskResultFuncExceptionResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        const string expected = "Failure";

        // When
        string actual = await ValueTask.FromResult(result).MatchAsync(SuccessAsync, Failure, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        async Task<string> SuccessAsync(int value, CancellationToken ct) => await Task.FromResult("Success");
        string Failure(Exception exception) => "Failure";
    }

    [Fact(DisplayName = "ValueTask<ValueResult<T>>.MatchAsync<TResult> of Failure should return result from failure delegate (Func<T, CancellationToken, ValueTask<TResult>>, Func<Exception, TResult>)")]
    public async Task ValueTaskValueResultOfTMatchAsyncOfFailureShouldReturnResultFromFailureDelegateFuncTCancellationTokenTaskResultFuncExceptionResultWithValueTaskDelegates()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        const string expected = "Failure";

        // When
        string actual = await ValueTask.FromResult(result).MatchAsync(SuccessAsync, Failure, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        async ValueTask<string> SuccessAsync(int value, CancellationToken ct) => await ValueTask.FromResult("Success");
        string Failure(Exception exception) => "Failure";
    }

    [Fact(DisplayName = "ValueTask<ValueResult<T>>.MatchAsync<TResult> of Success should return result from success delegate (Func<T, TResult>, Func<Exception, Task<TResult>>>)")]
    public async Task ValueTaskValueResultOfTMatchAsyncOfSuccessShouldReturnResultFromSuccessDelegateFuncTResultFuncExceptionTaskResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(42);
        const string expected = "Success";

        // When
        string actual = await ValueTask.FromResult(result).MatchAsync(Success, FailureAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        string Success(int value) => value == 42 ? "Success" : "Failure";
        async Task<string> FailureAsync(Exception exception) => await Task.FromResult("Failure");
    }

    [Fact(DisplayName = "ValueTask<ValueResult<T>>.MatchAsync<TResult> of Success should return result from success delegate (Func<T, TResult>, Func<Exception, ValueTask<TResult>>>)")]
    public async Task ValueTaskValueResultOfTMatchAsyncOfSuccessShouldReturnResultFromSuccessDelegateFuncTResultFuncExceptionTaskResultWithValueTaskDelegates()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(42);
        const string expected = "Success";

        // When
        string actual = await ValueTask.FromResult(result).MatchAsync(Success, FailureAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        string Success(int value) => value == 42 ? "Success" : "Failure";
        async ValueTask<string> FailureAsync(Exception exception) => await ValueTask.FromResult("Failure");
    }

    [Fact(DisplayName = "ValueTask<ValueResult<T>>.MatchAsync<TResult> of Failure should return result from failure delegate (Func<T, TResult>, Func<Exception, Task<TResult>>>)")]
    public async Task ValueTaskValueResultOfTMatchAsyncOfFailureShouldReturnResultFromFailureDelegateFuncTResultFuncExceptionTaskResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        const string expected = "Failure";

        // When
        string actual = await ValueTask.FromResult(result).MatchAsync(Success, FailureAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        string Success(int value) => "Success";
        async Task<string> FailureAsync(Exception exception) => await Task.FromResult("Failure");
    }

    [Fact(DisplayName = "ValueTask<ValueResult<T>>.MatchAsync<TResult> of Failure should return result from failure delegate (Func<T, TResult>, Func<Exception, ValueTask<TResult>>>)")]
    public async Task ValueTaskValueResultOfTMatchAsyncOfFailureShouldReturnResultFromFailureDelegateFuncTResultFuncExceptionTaskResultWithValueTaskDelegates()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        const string expected = "Failure";

        // When
        string actual = await ValueTask.FromResult(result).MatchAsync(Success, FailureAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        string Success(int value) => "Success";
        async ValueTask<string> FailureAsync(Exception exception) => await ValueTask.FromResult("Failure");
    }

    [Fact(DisplayName = "ValueTask<ValueResult<T>>.MatchAsync<TResult> of Success should return result from success delegate (Func<T, TResult>, Func<Exception, CancellationToken, Task<TResult>>>)")]
    public async Task ValueTaskValueResultOfTMatchAsyncOfSuccessShouldReturnResultFromSuccessDelegateFuncTResultFuncExceptionCancellationTokenTaskResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(42);
        const string expected = "Success";

        // When
        string actual = await ValueTask.FromResult(result).MatchAsync(Success, FailureAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        string Success(int value) => value == 42 ? "Success" : "Failure";
        async Task<string> FailureAsync(Exception exception, CancellationToken ct) => await Task.FromResult("Failure");
    }

    [Fact(DisplayName = "ValueTask<ValueResult<T>>.MatchAsync<TResult> of Success should return result from success delegate (Func<T, TResult>, Func<Exception, CancellationToken, ValueTask<TResult>>>)")]
    public async Task ValueTaskValueResultOfTMatchAsyncOfSuccessShouldReturnResultFromSuccessDelegateFuncTResultFuncExceptionCancellationTokenTaskResultWithValueTaskDelegates()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(42);
        const string expected = "Success";

        // When
        string actual = await ValueTask.FromResult(result).MatchAsync(Success, FailureAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        string Success(int value) => value == 42 ? "Success" : "Failure";
        async ValueTask<string> FailureAsync(Exception exception, CancellationToken ct) => await ValueTask.FromResult("Failure");
    }

    [Fact(DisplayName = "ValueTask<ValueResult<T>>.MatchAsync<TResult> of Failure should return result from failure delegate (Func<T, TResult>, Func<Exception, CancellationToken, Task<TResult>>>)")]
    public async Task ValueTaskValueResultOfTMatchAsyncOfFailureShouldReturnResultFromFailureDelegateFuncTResultFuncExceptionCancellationTokenTaskResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        const string expected = "Failure";

        // When
        string actual = await ValueTask.FromResult(result).MatchAsync(Success, FailureAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        string Success(int value) => "Success";
        async Task<string> FailureAsync(Exception exception, CancellationToken ct) => await Task.FromResult("Failure");
    }

    [Fact(DisplayName = "ValueTask<ValueResult<T>>.MatchAsync<TResult> of Failure should return result from failure delegate (Func<T, TResult>, Func<Exception, CancellationToken, ValueTask<TResult>>>)")]
    public async Task ValueTaskValueResultOfTMatchAsyncOfFailureShouldReturnResultFromFailureDelegateFuncTResultFuncExceptionCancellationTokenTaskResultWithValueTaskDelegates()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        const string expected = "Failure";

        // When
        string actual = await ValueTask.FromResult(result).MatchAsync(Success, FailureAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        string Success(int value) => "Success";
        async ValueTask<string> FailureAsync(Exception exception, CancellationToken ct) => await ValueTask.FromResult("Failure");
    }

    [Fact(DisplayName = "ValueTask<ValueResult<T>>.MatchAsync<TResult> of Success should return result from success delegate (Func<T, Task<TResult>>, Func<Exception, Task<TResult>>>)")]
    public async Task ValueTaskValueResultOfTMatchAsyncOfSuccessShouldReturnResultFromSuccessDelegateFuncTTaskResultFuncExceptionTaskResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(42);
        const string expected = "Success";

        // When
        string actual = await ValueTask.FromResult(result).MatchAsync(SuccessAsync, FailureAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        async Task<string> SuccessAsync(int value) => await Task.FromResult(value == 42 ? "Success" : "Failure");
        async Task<string> FailureAsync(Exception exception) => await Task.FromResult("Failure");
    }

    [Fact(DisplayName = "ValueTask<ValueResult<T>>.MatchAsync<TResult> of Success should return result from success delegate (Func<T, ValueTask<TResult>>, Func<Exception, ValueTask<TResult>>>)")]
    public async Task ValueTaskValueResultOfTMatchAsyncOfSuccessShouldReturnResultFromSuccessDelegateFuncTTaskResultFuncExceptionTaskResultWithValueTaskDelegates()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(42);
        const string expected = "Success";

        // When
        string actual = await ValueTask.FromResult(result).MatchAsync(SuccessAsync, FailureAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        async ValueTask<string> SuccessAsync(int value) => await ValueTask.FromResult(value == 42 ? "Success" : "Failure");
        async ValueTask<string> FailureAsync(Exception exception) => await ValueTask.FromResult("Failure");
    }

    [Fact(DisplayName = "ValueTask<ValueResult<T>>.MatchAsync<TResult> of Failure should return result from failure delegate (Func<T, Task<TResult>>, Func<Exception, Task<TResult>>>)")]
    public async Task ValueTaskValueResultOfTMatchAsyncOfFailureShouldReturnResultFromFailureDelegateFuncTTaskResultFuncExceptionTaskResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        const string expected = "Failure";

        // When
        string actual = await ValueTask.FromResult(result).MatchAsync(SuccessAsync, FailureAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        async Task<string> SuccessAsync(int value) => await Task.FromResult("Success");
        async Task<string> FailureAsync(Exception exception) => await Task.FromResult("Failure");
    }

    [Fact(DisplayName = "ValueTask<ValueResult<T>>.MatchAsync<TResult> of Failure should return result from failure delegate (Func<T, ValueTask<TResult>>, Func<Exception, ValueTask<TResult>>>)")]
    public async Task ValueTaskValueResultOfTMatchAsyncOfFailureShouldReturnResultFromFailureDelegateFuncTTaskResultFuncExceptionTaskResultWithValueTaskDelegates()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        const string expected = "Failure";

        // When
        string actual = await ValueTask.FromResult(result).MatchAsync(SuccessAsync, FailureAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        async ValueTask<string> SuccessAsync(int value) => await ValueTask.FromResult("Success");
        async ValueTask<string> FailureAsync(Exception exception) => await ValueTask.FromResult("Failure");
    }

    [Fact(DisplayName = "ValueTask<ValueResult<T>>.MatchAsync<TResult> of Success should return result from success delegate (Func<T, CancellationToken, Task<TResult>>, Func<Exception, CancellationToken, Task<TResult>>>)")]
    public async Task ValueTaskValueResultOfTMatchAsyncOfSuccessShouldReturnResultFromSuccessDelegateFuncTCancellationTokenTaskResultFuncExceptionCancellationTokenTaskResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(42);
        const string expected = "Success";

        // When
        string actual = await ValueTask.FromResult(result).MatchAsync(SuccessAsync, FailureAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        async Task<string> SuccessAsync(int value, CancellationToken ct) => await Task.FromResult(value == 42 ? "Success" : "Failure");
        async Task<string> FailureAsync(Exception exception, CancellationToken ct) => await Task.FromResult("Failure");
    }

    [Fact(DisplayName = "ValueTask<ValueResult<T>>.MatchAsync<TResult> of Success should return result from success delegate (Func<T, CancellationToken, ValueTask<TResult>>, Func<Exception, CancellationToken, ValueTask<TResult>>>)")]
    public async Task ValueTaskValueResultOfTMatchAsyncOfSuccessShouldReturnResultFromSuccessDelegateFuncTCancellationTokenTaskResultFuncExceptionCancellationTokenTaskResultWithValueTaskDelegates()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(42);
        const string expected = "Success";

        // When
        string actual = await ValueTask.FromResult(result).MatchAsync(SuccessAsync, FailureAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        async ValueTask<string> SuccessAsync(int value, CancellationToken ct) => await ValueTask.FromResult(value == 42 ? "Success" : "Failure");
        async ValueTask<string> FailureAsync(Exception exception, CancellationToken ct) => await ValueTask.FromResult("Failure");
    }

    [Fact(DisplayName = "ValueTask<ValueResult<T>>.MatchAsync<TResult> of Failure should return result from failure delegate (Func<T, CancellationToken, Task<TResult>>, Func<Exception, CancellationToken, Task<TResult>>>)")]
    public async Task ValueTaskValueResultOfTMatchAsyncOfFailureShouldReturnResultFromFailureDelegateFuncTCancellationTokenTaskResultFuncExceptionCancellationTokenTaskResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        const string expected = "Failure";

        // When
        string actual = await ValueTask.FromResult(result).MatchAsync(SuccessAsync, FailureAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        async Task<string> SuccessAsync(int value, CancellationToken ct) => await Task.FromResult("Success");
        async Task<string> FailureAsync(Exception exception, CancellationToken ct) => await Task.FromResult("Failure");
    }

    [Fact(DisplayName = "ValueTask<ValueResult<T>>.MatchAsync<TResult> of Failure should return result from failure delegate (Func<T, CancellationToken, ValueTask<TResult>>, Func<Exception, CancellationToken, ValueTask<TResult>>>)")]
    public async Task ValueTaskValueResultOfTMatchAsyncOfFailureShouldReturnResultFromFailureDelegateFuncTCancellationTokenTaskResultFuncExceptionCancellationTokenTaskResultWithValueTaskDelegates()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        const string expected = "Failure";

        // When
        string actual = await ValueTask.FromResult(result).MatchAsync(SuccessAsync, FailureAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        async ValueTask<string> SuccessAsync(int value, CancellationToken ct) => await ValueTask.FromResult("Success");
        async ValueTask<string> FailureAsync(Exception exception, CancellationToken ct) => await ValueTask.FromResult("Failure");
    }

    [Fact(DisplayName = "ValueTask<ValueResult>.SelectAsync of Success should invoke the selector delegate (Action)")]
    public async Task ValueTaskValueResultSelectAsyncOfSuccessShouldInvokeSelectorDelegateAction()
    {
        // Given
        ValueResult result = ValueResult.Success();
        bool selectorInvoked = false;

        // When
        ValueResult actual = await ValueTask.FromResult(result).SelectAsync(Selector, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(actual.IsSuccess);
        Assert.True(selectorInvoked);

        return;
        void Selector() => selectorInvoked = true;
    }

    [Fact(DisplayName = "ValueTask<ValueResult>.SelectAsync of Failure should not invoke the selector delegate (Action)")]
    public async Task ValueTaskValueResultSelectAsyncOfFailureShouldNotInvokeSelectorDelegateAction()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
        bool selectorInvoked = false;

        // When
        ValueResult actual = await ValueTask.FromResult(result).SelectAsync(Selector, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(actual.IsFailure);
        Assert.Equal(FailureException, actual.GetExceptionOrThrow());
        Assert.False(selectorInvoked);

        return;
        void Selector() => selectorInvoked = true;
    }

    [Fact(DisplayName = "ValueTask<ValueResult>.SelectAsync of Success should invoke the selector delegate (Func<Task>)")]
    public async Task ValueTaskValueResultSelectAsyncOfSuccessShouldInvokeSelectorDelegateFuncTask()
    {
        // Given
        ValueResult result = ValueResult.Success();
        bool selectorInvoked = false;

        // When
        ValueResult actual = await ValueTask.FromResult(result).SelectAsync(SelectorAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(actual.IsSuccess);
        Assert.True(selectorInvoked);

        return;
        async Task SelectorAsync() => await Task.Run(() => selectorInvoked = true);
    }

    [Fact(DisplayName = "ValueTask<ValueResult>.SelectAsync of Success should invoke the selector delegate (Func<ValueTask>)")]
    public async Task ValueTaskValueResultSelectAsyncOfSuccessShouldInvokeSelectorDelegateFuncTaskWithValueTaskDelegates()
    {
        // Given
        ValueResult result = ValueResult.Success();
        bool selectorInvoked = false;

        // When
        ValueResult actual = await ValueTask.FromResult(result).SelectAsync(SelectorAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(actual.IsSuccess);
        Assert.True(selectorInvoked);

        return;
        async ValueTask SelectorAsync() => await Task.Run(() => selectorInvoked = true);
    }

    [Fact(DisplayName = "ValueTask<ValueResult>.SelectAsync of Failure should not invoke the selector delegate (Func<Task>)")]
    public async Task ValueTaskValueResultSelectAsyncOfFailureShouldNotInvokeSelectorDelegateFuncTask()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
        bool selectorInvoked = false;

        // When
        ValueResult actual = await ValueTask.FromResult(result).SelectAsync(SelectorAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(actual.IsFailure);
        Assert.Equal(FailureException, actual.GetExceptionOrThrow());
        Assert.False(selectorInvoked);

        return;
        async Task SelectorAsync() => await Task.Run(() => selectorInvoked = true);
    }

    [Fact(DisplayName = "ValueTask<ValueResult>.SelectAsync of Failure should not invoke the selector delegate (Func<ValueTask>)")]
    public async Task ValueTaskValueResultSelectAsyncOfFailureShouldNotInvokeSelectorDelegateFuncTaskWithValueTaskDelegates()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
        bool selectorInvoked = false;

        // When
        ValueResult actual = await ValueTask.FromResult(result).SelectAsync(SelectorAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(actual.IsFailure);
        Assert.Equal(FailureException, actual.GetExceptionOrThrow());
        Assert.False(selectorInvoked);

        return;
        async ValueTask SelectorAsync() => await Task.Run(() => selectorInvoked = true);
    }

    [Fact(DisplayName = "ValueTask<ValueResult>.SelectAsync of Success should invoke the selector delegate (Func<CancellationToken, Task>)")]
    public async Task ValueTaskValueResultSelectAsyncOfSuccessShouldInvokeSelectorDelegateFuncCancellationTokenTask()
    {
        // Given
        ValueResult result = ValueResult.Success();
        bool selectorInvoked = false;

        // When
        ValueResult actual = await ValueTask.FromResult(result).SelectAsync(SelectorAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(actual.IsSuccess);
        Assert.True(selectorInvoked);

        return;
        async Task SelectorAsync(CancellationToken ct) => await Task.Run(() => selectorInvoked = true, ct);
    }

    [Fact(DisplayName = "ValueTask<ValueResult>.SelectAsync of Success should invoke the selector delegate (Func<CancellationToken, ValueTask>)")]
    public async Task ValueTaskValueResultSelectAsyncOfSuccessShouldInvokeSelectorDelegateFuncCancellationTokenTaskWithValueTaskDelegates()
    {
        // Given
        ValueResult result = ValueResult.Success();
        bool selectorInvoked = false;

        // When
        ValueResult actual = await ValueTask.FromResult(result).SelectAsync(SelectorAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(actual.IsSuccess);
        Assert.True(selectorInvoked);

        return;
        async ValueTask SelectorAsync(CancellationToken ct) => await Task.Run(() => selectorInvoked = true, ct);
    }

    [Fact(DisplayName = "ValueTask<ValueResult>.SelectAsync of Failure should not invoke the selector delegate (Func<CancellationToken, Task>)")]
    public async Task ValueTaskValueResultSelectAsyncOfFailureShouldNotInvokeSelectorDelegateFuncCancellationTokenTask()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
        bool selectorInvoked = false;

        // When
        ValueResult actual = await ValueTask.FromResult(result).SelectAsync(SelectorAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(actual.IsFailure);
        Assert.Equal(FailureException, actual.GetExceptionOrThrow());
        Assert.False(selectorInvoked);

        return;
        async Task SelectorAsync(CancellationToken ct) => await Task.Run(() => selectorInvoked = true, ct);
    }

    [Fact(DisplayName = "ValueTask<ValueResult>.SelectAsync of Failure should not invoke the selector delegate (Func<CancellationToken, ValueTask>)")]
    public async Task ValueTaskValueResultSelectAsyncOfFailureShouldNotInvokeSelectorDelegateFuncCancellationTokenTaskWithValueTaskDelegates()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
        bool selectorInvoked = false;

        // When
        ValueResult actual = await ValueTask.FromResult(result).SelectAsync(SelectorAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(actual.IsFailure);
        Assert.Equal(FailureException, actual.GetExceptionOrThrow());
        Assert.False(selectorInvoked);

        return;
        async ValueTask SelectorAsync(CancellationToken ct) => await Task.Run(() => selectorInvoked = true, ct);
    }

    [Fact(DisplayName = "ValueTask<ValueResult>.SelectAsync<TResult> of Success should invoke the selector delegate (Func<TResult>)")]
    public async Task ValueTaskValueResultSelectAsyncOfSuccessShouldInvokeSelectorDelegateFuncTResult()
    {
        // Given
        ValueResult result = ValueResult.Success();
        bool selectorInvoked = false;

        // When
        ValueResult<string> actual = await ValueTask.FromResult(result).SelectAsync(Selector, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(actual.IsSuccess);
        Assert.True(selectorInvoked);
        Assert.Equal("Success Result", actual.GetValueOrThrow());

        return;

        string Selector()
        {
            selectorInvoked = true;
            return "Success Result";
        }
    }

    [Fact(DisplayName = "ValueTask<ValueResult>.SelectAsync<TResult> of Failure should not invoke the selector delegate (Func<TResult>)")]
    public async Task ValueTaskValueResultSelectAsyncOfFailureShouldNotInvokeSelectorDelegateFuncTResult()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
        bool selectorInvoked = false;

        // When
        ValueResult<string> actual = await ValueTask.FromResult(result).SelectAsync(Selector, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(actual.IsFailure);
        Assert.Equal(FailureException, actual.GetExceptionOrThrow());
        Assert.False(selectorInvoked);

        return;

        string Selector()
        {
            selectorInvoked = true;
            return "Success Result";
        }
    }

    [Fact(DisplayName = "ValueTask<ValueResult>.SelectAsync<TResult> of Success should invoke the selector delegate (Func<Task<TResult>>)")]
    public async Task ValueTaskValueResultSelectAsyncOfSuccessShouldInvokeSelectorDelegateFuncTaskTResult()
    {
        // Given
        ValueResult result = ValueResult.Success();
        bool selectorInvoked = false;

        // When
        ValueResult<string> actual = await ValueTask.FromResult(result).SelectAsync(SelectorAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(actual.IsSuccess);
        Assert.True(selectorInvoked);
        Assert.Equal("Success Result", actual.GetValueOrThrow());

        return;

        async Task<string> SelectorAsync()
        {
            selectorInvoked = true;
            return await Task.FromResult("Success Result");
        }
    }

    [Fact(DisplayName = "ValueTask<ValueResult>.SelectAsync<TResult> of Success should invoke the selector delegate (Func<ValueTask<TResult>>)")]
    public async Task ValueTaskValueResultSelectAsyncOfSuccessShouldInvokeSelectorDelegateFuncTaskTResultWithValueTaskDelegates()
    {
        // Given
        ValueResult result = ValueResult.Success();
        bool selectorInvoked = false;

        // When
        ValueResult<string> actual = await ValueTask.FromResult(result).SelectAsync(SelectorAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(actual.IsSuccess);
        Assert.True(selectorInvoked);
        Assert.Equal("Success Result", actual.GetValueOrThrow());

        return;

        async ValueTask<string> SelectorAsync()
        {
            selectorInvoked = true;
            return await ValueTask.FromResult("Success Result");
        }
    }

    [Fact(DisplayName = "ValueTask<ValueResult>.SelectAsync<TResult> of Failure should not invoke the selector delegate (Func<Task<TResult>>)")]
    public async Task ValueTaskValueResultSelectAsyncOfFailureShouldNotInvokeSelectorDelegateFuncTaskTResult()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
        bool selectorInvoked = false;

        // When
        ValueResult<string> actual = await ValueTask.FromResult(result).SelectAsync(SelectorAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(actual.IsFailure);
        Assert.Equal(FailureException, actual.GetExceptionOrThrow());
        Assert.False(selectorInvoked);

        return;

        async Task<string> SelectorAsync()
        {
            selectorInvoked = true;
            return await Task.FromResult("Success Result");
        }
    }

    [Fact(DisplayName = "ValueTask<ValueResult>.SelectAsync<TResult> of Failure should not invoke the selector delegate (Func<ValueTask<TResult>>)")]
    public async Task ValueTaskValueResultSelectAsyncOfFailureShouldNotInvokeSelectorDelegateFuncTaskTResultWithValueTaskDelegates()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
        bool selectorInvoked = false;

        // When
        ValueResult<string> actual = await ValueTask.FromResult(result).SelectAsync(SelectorAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(actual.IsFailure);
        Assert.Equal(FailureException, actual.GetExceptionOrThrow());
        Assert.False(selectorInvoked);

        return;

        async ValueTask<string> SelectorAsync()
        {
            selectorInvoked = true;
            return await ValueTask.FromResult("Success Result");
        }
    }

    [Fact(DisplayName = "ValueTask<ValueResult>.SelectAsync<TResult> of Success should invoke the selector delegate (Func<CancellationToken, Task<TResult>>)")]
    public async Task ValueTaskValueResultSelectAsyncOfSuccessShouldInvokeSelectorDelegateFuncCancellationTokenTaskTResult()
    {
        // Given
        ValueResult result = ValueResult.Success();
        bool selectorInvoked = false;

        // When
        ValueResult<string> actual = await ValueTask.FromResult(result).SelectAsync(SelectorAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(actual.IsSuccess);
        Assert.True(selectorInvoked);
        Assert.Equal("Success Result", actual.GetValueOrThrow());

        return;

        async Task<string> SelectorAsync(CancellationToken ct)
        {
            selectorInvoked = true;
            return await Task.FromResult("Success Result");
        }
    }

    [Fact(DisplayName = "ValueTask<ValueResult>.SelectAsync<TResult> of Success should invoke the selector delegate (Func<CancellationToken, ValueTask<TResult>>)")]
    public async Task ValueTaskValueResultSelectAsyncOfSuccessShouldInvokeSelectorDelegateFuncCancellationTokenTaskTResultWithValueTaskDelegates()
    {
        // Given
        ValueResult result = ValueResult.Success();
        bool selectorInvoked = false;

        // When
        ValueResult<string> actual = await ValueTask.FromResult(result).SelectAsync(SelectorAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(actual.IsSuccess);
        Assert.True(selectorInvoked);
        Assert.Equal("Success Result", actual.GetValueOrThrow());

        return;

        async ValueTask<string> SelectorAsync(CancellationToken ct)
        {
            selectorInvoked = true;
            return await ValueTask.FromResult("Success Result");
        }
    }

    [Fact(DisplayName = "ValueTask<ValueResult>.SelectAsync<TResult> of Failure should not invoke the selector delegate (Func<CancellationToken, Task<TResult>>)")]
    public async Task ValueTaskValueResultSelectAsyncOfFailureShouldNotInvokeSelectorDelegateFuncCancellationTokenTaskTResult()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
        bool selectorInvoked = false;

        // When
        ValueResult<string> actual = await ValueTask.FromResult(result).SelectAsync(SelectorAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(actual.IsFailure);
        Assert.Equal(FailureException, actual.GetExceptionOrThrow());
        Assert.False(selectorInvoked);

        return;

        async Task<string> SelectorAsync(CancellationToken ct)
        {
            selectorInvoked = true;
            return await Task.FromResult("Success Result");
        }
    }

    [Fact(DisplayName = "ValueTask<ValueResult>.SelectAsync<TResult> of Failure should not invoke the selector delegate (Func<CancellationToken, ValueTask<TResult>>)")]
    public async Task ValueTaskValueResultSelectAsyncOfFailureShouldNotInvokeSelectorDelegateFuncCancellationTokenTaskTResultWithValueTaskDelegates()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
        bool selectorInvoked = false;

        // When
        ValueResult<string> actual = await ValueTask.FromResult(result).SelectAsync(SelectorAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(actual.IsFailure);
        Assert.Equal(FailureException, actual.GetExceptionOrThrow());
        Assert.False(selectorInvoked);

        return;

        async ValueTask<string> SelectorAsync(CancellationToken ct)
        {
            selectorInvoked = true;
            return await ValueTask.FromResult("Success Result");
        }
    }

    [Fact(DisplayName = "ValueTask<ValueResult<T>>.SelectAsync of Success should invoke the selector delegate (Action<T>)")]
    public async Task ValueTaskValueResultOfTSelectAsyncOfSuccessShouldInvokeSelectorDelegateActionT()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(42);
        bool selectorInvoked = false;

        // When
        ValueResult actual = await ValueTask.FromResult(result).SelectAsync(Selector, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(actual.IsSuccess);
        Assert.True(selectorInvoked);

        return;

        void Selector(int value)
        {
            selectorInvoked = value == 42;
        }
    }

    [Fact(DisplayName = "ValueTask<ValueResult<T>>.SelectAsync of Failure should not invoke the selector delegate (Action<T>)")]
    public async Task ValueTaskValueResultOfTSelectAsyncOfFailureShouldNotInvokeSelectorDelegateActionT()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        bool selectorInvoked = false;

        // When
        ValueResult actual = await ValueTask.FromResult(result).SelectAsync(Selector, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(actual.IsFailure);
        Assert.Equal(FailureException, actual.GetExceptionOrThrow());
        Assert.False(selectorInvoked);

        return;

        void Selector(int value)
        {
            selectorInvoked = true;
        }
    }

    [Fact(DisplayName = "ValueTask<ValueResult<T>>.SelectAsync of Success should invoke the selector delegate (Func<T, Task>)")]
    public async Task ValueTaskValueResultOfTSelectAsyncOfSuccessShouldInvokeSelectorDelegateFuncTTask()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(42);
        bool selectorInvoked = false;

        // When
        ValueResult actual = await ValueTask.FromResult(result).SelectAsync(SelectorAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(actual.IsSuccess);
        Assert.True(selectorInvoked);

        return;

        async Task SelectorAsync(int value)
        {
            selectorInvoked = value == 42;
            await Task.CompletedTask;
        }
    }

    [Fact(DisplayName = "ValueTask<ValueResult<T>>.SelectAsync of Success should invoke the selector delegate (Func<T, ValueTask>)")]
    public async Task ValueTaskValueResultOfTSelectAsyncOfSuccessShouldInvokeSelectorDelegateFuncTTaskWithValueTaskDelegates()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(42);
        bool selectorInvoked = false;

        // When
        ValueResult actual = await ValueTask.FromResult(result).SelectAsync(SelectorAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(actual.IsSuccess);
        Assert.True(selectorInvoked);

        return;

        async ValueTask SelectorAsync(int value)
        {
            selectorInvoked = value == 42;
            await ValueTask.CompletedTask;
        }
    }

    [Fact(DisplayName = "ValueTask<ValueResult<T>>.SelectAsync of Failure should not invoke the selector delegate (Func<T, Task>)")]
    public async Task ValueTaskValueResultOfTSelectAsyncOfFailureShouldNotInvokeSelectorDelegateFuncTTask()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        bool selectorInvoked = false;

        // When
        ValueResult actual = await ValueTask.FromResult(result).SelectAsync(SelectorAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(actual.IsFailure);
        Assert.Equal(FailureException, actual.GetExceptionOrThrow());
        Assert.False(selectorInvoked);

        return;

        async Task SelectorAsync(int value)
        {
            selectorInvoked = true;
            await Task.CompletedTask;
        }
    }

    [Fact(DisplayName = "ValueTask<ValueResult<T>>.SelectAsync of Failure should not invoke the selector delegate (Func<T, ValueTask>)")]
    public async Task ValueTaskValueResultOfTSelectAsyncOfFailureShouldNotInvokeSelectorDelegateFuncTTaskWithValueTaskDelegates()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        bool selectorInvoked = false;

        // When
        ValueResult actual = await ValueTask.FromResult(result).SelectAsync(SelectorAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(actual.IsFailure);
        Assert.Equal(FailureException, actual.GetExceptionOrThrow());
        Assert.False(selectorInvoked);

        return;

        async ValueTask SelectorAsync(int value)
        {
            selectorInvoked = true;
            await ValueTask.CompletedTask;
        }
    }

    [Fact(DisplayName = "ValueTask<ValueResult<T>>.SelectAsync of Success should invoke the selector delegate (Func<T, CancellationToken, Task>)")]
    public async Task ValueTaskValueResultOfTSelectAsyncOfSuccessShouldInvokeSelectorDelegateFuncTCancellationTokenTask()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(42);
        bool selectorInvoked = false;

        // When
        ValueResult actual = await ValueTask.FromResult(result).SelectAsync(SelectorAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(actual.IsSuccess);
        Assert.True(selectorInvoked);

        return;

        async Task SelectorAsync(int value, CancellationToken ct)
        {
            selectorInvoked = value == 42;
            await Task.CompletedTask;
        }
    }

    [Fact(DisplayName = "ValueTask<ValueResult<T>>.SelectAsync of Success should invoke the selector delegate (Func<T, CancellationToken, ValueTask>)")]
    public async Task ValueTaskValueResultOfTSelectAsyncOfSuccessShouldInvokeSelectorDelegateFuncTCancellationTokenTaskWithValueTaskDelegates()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(42);
        bool selectorInvoked = false;

        // When
        ValueResult actual = await ValueTask.FromResult(result).SelectAsync(SelectorAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(actual.IsSuccess);
        Assert.True(selectorInvoked);

        return;

        async ValueTask SelectorAsync(int value, CancellationToken ct)
        {
            selectorInvoked = value == 42;
            await ValueTask.CompletedTask;
        }
    }

    [Fact(DisplayName = "ValueTask<ValueResult<T>>.SelectAsync of Failure should not invoke the selector delegate (Func<T, CancellationToken, Task>)")]
    public async Task ValueTaskValueResultOfTSelectAsyncOfFailureShouldNotInvokeSelectorDelegateFuncTCancellationTokenTask()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        bool selectorInvoked = false;

        // When
        ValueResult actual = await ValueTask.FromResult(result).SelectAsync(SelectorAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(actual.IsFailure);
        Assert.Equal(FailureException, actual.GetExceptionOrThrow());
        Assert.False(selectorInvoked);

        return;

        async Task SelectorAsync(int value, CancellationToken ct)
        {
            selectorInvoked = true;
            await Task.CompletedTask;
        }
    }

    [Fact(DisplayName = "ValueTask<ValueResult<T>>.SelectAsync of Failure should not invoke the selector delegate (Func<T, CancellationToken, ValueTask>)")]
    public async Task ValueTaskValueResultOfTSelectAsyncOfFailureShouldNotInvokeSelectorDelegateFuncTCancellationTokenTaskWithValueTaskDelegates()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        bool selectorInvoked = false;

        // When
        ValueResult actual = await ValueTask.FromResult(result).SelectAsync(SelectorAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(actual.IsFailure);
        Assert.Equal(FailureException, actual.GetExceptionOrThrow());
        Assert.False(selectorInvoked);

        return;

        async ValueTask SelectorAsync(int value, CancellationToken ct)
        {
            selectorInvoked = true;
            await ValueTask.CompletedTask;
        }
    }

    [Fact(DisplayName = "ValueTask<ValueResult<T>>.SelectAsync<TResult> of Success should invoke the selector delegate (Func<T, TResult>)")]
    public async Task ValueTaskValueResultOfTSelectAsyncOfSuccessShouldInvokeSelectorDelegateFuncTResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(42);
        bool selectorInvoked = false;

        // When
        ValueResult<string> actual = await ValueTask.FromResult(result).SelectAsync(Selector, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(actual.IsSuccess);
        Assert.True(selectorInvoked);
        Assert.Equal("Value is 42", actual.GetValueOrThrow());

        return;

        string Selector(int value)
        {
            selectorInvoked = value == 42;
            return $"Value is {value}";
        }
    }

    [Fact(DisplayName = "ValueTask<ValueResult<T>>.SelectAsync<TResult> of Failure should not invoke the selector delegate (Func<T, TResult>)")]
    public async Task ValueTaskValueResultOfTSelectAsyncOfFailureShouldNotInvokeSelectorDelegateFuncTResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        bool selectorInvoked = false;

        // When
        ValueResult<string> actual = await ValueTask.FromResult(result).SelectAsync(Selector, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(actual.IsFailure);
        Assert.Equal(FailureException, actual.GetExceptionOrThrow());
        Assert.False(selectorInvoked);

        return;

        string Selector(int value)
        {
            selectorInvoked = true;
            return $"Value is {value}";
        }
    }

    [Fact(DisplayName = "ValueTask<ValueResult<T>>.SelectAsync<TResult> of Success should invoke the selector delegate (Func<T, Task<TResult>>)")]
    public async Task ValueTaskValueResultOfTSelectAsyncOfSuccessShouldInvokeSelectorDelegateFuncTTaskResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(42);
        bool selectorInvoked = false;

        // When
        ValueResult<string> actual = await ValueTask.FromResult(result).SelectAsync(SelectorAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(actual.IsSuccess);
        Assert.True(selectorInvoked);
        Assert.Equal("Value is 42", actual.GetValueOrThrow());

        return;

        async Task<string> SelectorAsync(int value)
        {
            selectorInvoked = value == 42;
            return await Task.FromResult($"Value is {value}");
        }
    }

    [Fact(DisplayName = "ValueTask<ValueResult<T>>.SelectAsync<TResult> of Success should invoke the selector delegate (Func<T, ValueTask<TResult>>)")]
    public async Task ValueTaskValueResultOfTSelectAsyncOfSuccessShouldInvokeSelectorDelegateFuncTTaskResultWithValueTaskDelegates()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(42);
        bool selectorInvoked = false;

        // When
        ValueResult<string> actual = await ValueTask.FromResult(result).SelectAsync(SelectorAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(actual.IsSuccess);
        Assert.True(selectorInvoked);
        Assert.Equal("Value is 42", actual.GetValueOrThrow());

        return;

        async ValueTask<string> SelectorAsync(int value)
        {
            selectorInvoked = value == 42;
            return await ValueTask.FromResult($"Value is {value}");
        }
    }

    [Fact(DisplayName = "ValueTask<ValueResult<T>>.SelectAsync<TResult> of Failure should not invoke the selector delegate (Func<T, Task<TResult>>)")]
    public async Task ValueTaskValueResultOfTSelectAsyncOfFailureShouldNotInvokeSelectorDelegateFuncTTaskResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        bool selectorInvoked = false;

        // When
        ValueResult<string> actual = await ValueTask.FromResult(result).SelectAsync(SelectorAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(actual.IsFailure);
        Assert.Equal(FailureException, actual.GetExceptionOrThrow());
        Assert.False(selectorInvoked);

        return;

        async Task<string> SelectorAsync(int value)
        {
            selectorInvoked = true;
            return await Task.FromResult($"Value is {value}");
        }
    }

    [Fact(DisplayName = "ValueTask<ValueResult<T>>.SelectAsync<TResult> of Failure should not invoke the selector delegate (Func<T, ValueTask<TResult>>)")]
    public async Task ValueTaskValueResultOfTSelectAsyncOfFailureShouldNotInvokeSelectorDelegateFuncTTaskResultWithValueTaskDelegates()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        bool selectorInvoked = false;

        // When
        ValueResult<string> actual = await ValueTask.FromResult(result).SelectAsync(SelectorAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(actual.IsFailure);
        Assert.Equal(FailureException, actual.GetExceptionOrThrow());
        Assert.False(selectorInvoked);

        return;

        async ValueTask<string> SelectorAsync(int value)
        {
            selectorInvoked = true;
            return await ValueTask.FromResult($"Value is {value}");
        }
    }

    [Fact(DisplayName = "ValueTask<ValueResult<T>>.SelectAsync<TResult> of Success should invoke the selector delegate (Func<T, CancellationToken, Task<TResult>>)")]
    public async Task ValueTaskValueResultOfTSelectAsyncOfSuccessShouldInvokeSelectorDelegateFuncTCancellationTokenTaskResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(42);
        bool selectorInvoked = false;

        // When
        ValueResult<string> actual = await ValueTask.FromResult(result).SelectAsync(SelectorAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(actual.IsSuccess);
        Assert.True(selectorInvoked);
        Assert.Equal("Value is 42", actual.GetValueOrThrow());

        return;

        async Task<string> SelectorAsync(int value, CancellationToken ct)
        {
            selectorInvoked = value == 42;
            return await Task.FromResult($"Value is {value}");
        }
    }

    [Fact(DisplayName = "ValueTask<ValueResult<T>>.SelectAsync<TResult> of Success should invoke the selector delegate (Func<T, CancellationToken, ValueTask<TResult>>)")]
    public async Task ValueTaskValueResultOfTSelectAsyncOfSuccessShouldInvokeSelectorDelegateFuncTCancellationTokenTaskResultWithValueTaskDelegates()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(42);
        bool selectorInvoked = false;

        // When
        ValueResult<string> actual = await ValueTask.FromResult(result).SelectAsync(SelectorAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(actual.IsSuccess);
        Assert.True(selectorInvoked);
        Assert.Equal("Value is 42", actual.GetValueOrThrow());

        return;

        async ValueTask<string> SelectorAsync(int value, CancellationToken ct)
        {
            selectorInvoked = value == 42;
            return await ValueTask.FromResult($"Value is {value}");
        }
    }

    [Fact(DisplayName = "ValueTask<ValueResult<T>>.SelectAsync<TResult> of Failure should not invoke the selector delegate (Func<T, CancellationToken, Task<TResult>>)")]
    public async Task ValueTaskValueResultOfTSelectAsyncOfFailureShouldNotInvokeSelectorDelegateFuncTCancellationTokenTaskResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        bool selectorInvoked = false;

        // When
        ValueResult<string> actual = await ValueTask.FromResult(result).SelectAsync(SelectorAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(actual.IsFailure);
        Assert.Equal(FailureException, actual.GetExceptionOrThrow());
        Assert.False(selectorInvoked);

        return;

        async Task<string> SelectorAsync(int value, CancellationToken ct)
        {
            selectorInvoked = true;
            return await Task.FromResult($"Value is {value}");
        }
    }

    [Fact(DisplayName = "ValueTask<ValueResult<T>>.SelectAsync<TResult> of Failure should not invoke the selector delegate (Func<T, CancellationToken, ValueTask<TResult>>)")]
    public async Task ValueTaskValueResultOfTSelectAsyncOfFailureShouldNotInvokeSelectorDelegateFuncTCancellationTokenTaskResultWithValueTaskDelegates()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        bool selectorInvoked = false;

        // When
        ValueResult<string> actual = await ValueTask.FromResult(result).SelectAsync(SelectorAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(actual.IsFailure);
        Assert.Equal(FailureException, actual.GetExceptionOrThrow());
        Assert.False(selectorInvoked);

        return;

        async ValueTask<string> SelectorAsync(int value, CancellationToken ct)
        {
            selectorInvoked = true;
            return await ValueTask.FromResult($"Value is {value}");
        }
    }

    [Fact(DisplayName = "ValueTask<ValueResult>.SelectManyAsync of Success should invoke the selector delegate (Func<ValueResult>) and return its result")]
    public async Task ValueTaskValueResultSelectManyAsyncOfSuccessShouldInvokeSelectorDelegateFuncResult()
    {
        // Given
        ValueResult result = ValueResult.Success();
        bool selectorInvoked = false;

        // When
        ValueResult actual = await ValueTask.FromResult(result).SelectManyAsync(Selector, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(actual.IsSuccess);
        Assert.True(selectorInvoked);
        return;

        ValueResult Selector()
        {
            selectorInvoked = true;
            return ValueResult.Success();
        }
    }

    [Fact(DisplayName = "ValueTask<ValueResult>.SelectManyAsync of Failure should not invoke the selector delegate (Func<ValueResult>)")]
    public async Task ValueTaskValueResultSelectManyAsyncOfFailureShouldNotInvokeSelectorDelegateFuncResult()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
        bool selectorInvoked = false;

        // When
        ValueResult actual = await ValueTask.FromResult(result).SelectManyAsync(Selector, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(actual.IsFailure);
        Assert.Equal(FailureException, actual.GetExceptionOrThrow());
        Assert.False(selectorInvoked);
        return;

        ValueResult Selector()
        {
            selectorInvoked = true;
            return ValueResult.Success();
        }
    }

    [Fact(DisplayName = "ValueTask<ValueResult>.SelectManyAsync of Success should invoke the selector delegate (Func<Task<ValueResult>>) and return its result")]
    public async Task ValueTaskValueResultSelectManyAsyncOfSuccessShouldInvokeSelectorDelegateFuncTaskResult()
    {
        // Given
        ValueResult result = ValueResult.Success();
        bool selectorInvoked = false;

        // When
        ValueResult actual = await ValueTask.FromResult(result).SelectManyAsync(SelectorAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(actual.IsSuccess);
        Assert.True(selectorInvoked);
        return;

        async Task<ValueResult> SelectorAsync()
        {
            selectorInvoked = true;
            return await Task.FromResult(ValueResult.Success());
        }
    }

    [Fact(DisplayName = "ValueTask<ValueResult>.SelectManyAsync of Success should invoke the selector delegate (Func<ValueTask<ValueResult>>) and return its result")]
    public async Task ValueTaskValueResultSelectManyAsyncOfSuccessShouldInvokeSelectorDelegateFuncTaskResultWithValueTaskDelegates()
    {
        // Given
        ValueResult result = ValueResult.Success();
        bool selectorInvoked = false;

        // When
        ValueResult actual = await ValueTask.FromResult(result).SelectManyAsync(SelectorAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(actual.IsSuccess);
        Assert.True(selectorInvoked);
        return;

        async ValueTask<ValueResult> SelectorAsync()
        {
            selectorInvoked = true;
            return await ValueTask.FromResult(ValueResult.Success());
        }
    }

    [Fact(DisplayName = "ValueTask<ValueResult>.SelectManyAsync of Failure should not invoke the selector delegate (Func<Task<ValueResult>>)")]
    public async Task ValueTaskValueResultSelectManyAsyncOfFailureShouldNotInvokeSelectorDelegateFuncTaskResult()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
        bool selectorInvoked = false;

        // When
        ValueResult actual = await ValueTask.FromResult(result).SelectManyAsync(SelectorAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(actual.IsFailure);
        Assert.Equal(FailureException, actual.GetExceptionOrThrow());
        Assert.False(selectorInvoked);
        return;

        async Task<ValueResult> SelectorAsync()
        {
            selectorInvoked = true;
            return await Task.FromResult(ValueResult.Success());
        }
    }

    [Fact(DisplayName = "ValueTask<ValueResult>.SelectManyAsync of Failure should not invoke the selector delegate (Func<ValueTask<ValueResult>>)")]
    public async Task ValueTaskValueResultSelectManyAsyncOfFailureShouldNotInvokeSelectorDelegateFuncTaskResultWithValueTaskDelegates()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
        bool selectorInvoked = false;

        // When
        ValueResult actual = await ValueTask.FromResult(result).SelectManyAsync(SelectorAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(actual.IsFailure);
        Assert.Equal(FailureException, actual.GetExceptionOrThrow());
        Assert.False(selectorInvoked);
        return;

        async ValueTask<ValueResult> SelectorAsync()
        {
            selectorInvoked = true;
            return await ValueTask.FromResult(ValueResult.Success());
        }
    }

    [Fact(DisplayName = "ValueTask<ValueResult>.SelectManyAsync of Success should invoke the selector delegate (Func<CancellationToken, Task<ValueResult>>) and return its result")]
    public async Task ValueTaskValueResultSelectManyAsyncOfSuccessShouldInvokeSelectorDelegateFuncCancellationTokenTaskResult()
    {
        // Given
        ValueResult result = ValueResult.Success();
        bool selectorInvoked = false;

        // When
        ValueResult actual = await ValueTask.FromResult(result).SelectManyAsync(SelectorAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(actual.IsSuccess);
        Assert.True(selectorInvoked);
        return;

        async Task<ValueResult> SelectorAsync(CancellationToken ct)
        {
            selectorInvoked = true;
            return await Task.FromResult(ValueResult.Success());
        }
    }

    [Fact(DisplayName = "ValueTask<ValueResult>.SelectManyAsync of Success should invoke the selector delegate (Func<CancellationToken, ValueTask<ValueResult>>) and return its result")]
    public async Task ValueTaskValueResultSelectManyAsyncOfSuccessShouldInvokeSelectorDelegateFuncCancellationTokenTaskResultWithValueTaskDelegates()
    {
        // Given
        ValueResult result = ValueResult.Success();
        bool selectorInvoked = false;

        // When
        ValueResult actual = await ValueTask.FromResult(result).SelectManyAsync(SelectorAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(actual.IsSuccess);
        Assert.True(selectorInvoked);
        return;

        async ValueTask<ValueResult> SelectorAsync(CancellationToken ct)
        {
            selectorInvoked = true;
            return await ValueTask.FromResult(ValueResult.Success());
        }
    }

    [Fact(DisplayName = "ValueTask<ValueResult>.SelectManyAsync of Failure should not invoke the selector delegate (Func<CancellationToken, Task<ValueResult>>)")]
    public async Task ValueTaskValueResultSelectManyAsyncOfFailureShouldNotInvokeSelectorDelegateFuncCancellationTokenTaskResult()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
        bool selectorInvoked = false;

        // When
        ValueResult actual = await ValueTask.FromResult(result).SelectManyAsync(SelectorAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(actual.IsFailure);
        Assert.Equal(FailureException, actual.GetExceptionOrThrow());
        Assert.False(selectorInvoked);
        return;

        async Task<ValueResult> SelectorAsync(CancellationToken ct)
        {
            selectorInvoked = true;
            return await Task.FromResult(ValueResult.Success());
        }
    }

    [Fact(DisplayName = "ValueTask<ValueResult>.SelectManyAsync of Failure should not invoke the selector delegate (Func<CancellationToken, ValueTask<ValueResult>>)")]
    public async Task ValueTaskValueResultSelectManyAsyncOfFailureShouldNotInvokeSelectorDelegateFuncCancellationTokenTaskResultWithValueTaskDelegates()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
        bool selectorInvoked = false;

        // When
        ValueResult actual = await ValueTask.FromResult(result).SelectManyAsync(SelectorAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(actual.IsFailure);
        Assert.Equal(FailureException, actual.GetExceptionOrThrow());
        Assert.False(selectorInvoked);
        return;

        async ValueTask<ValueResult> SelectorAsync(CancellationToken ct)
        {
            selectorInvoked = true;
            return await ValueTask.FromResult(ValueResult.Success());
        }
    }

    [Fact(DisplayName = "ValueTask<ValueResult>.SelectManyAsync<TResult> of Success should invoke the selector delegate (Func<ValueResult<TResult>>) and return its result")]
    public async Task ValueTaskValueResultSelectManyAsyncOfSuccessShouldInvokeSelectorDelegateFuncResultTResult()
    {
        // Given
        ValueResult result = ValueResult.Success();
        bool selectorInvoked = false;

        // When
        ValueResult<string> actual = await ValueTask.FromResult(result).SelectManyAsync(Selector, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(actual.IsSuccess);
        Assert.True(selectorInvoked);
        Assert.Equal("Success Result", actual.GetValueOrThrow());

        return;

        ValueResult<string> Selector()
        {
            selectorInvoked = true;
            return ValueResult<string>.Success("Success Result");
        }
    }

    [Fact(DisplayName = "ValueTask<ValueResult>.SelectManyAsync<TResult> of Failure should not invoke the selector delegate (Func<ValueResult<TResult>>)")]
    public async Task ValueTaskValueResultSelectManyAsyncOfFailureShouldNotInvokeSelectorDelegateFuncResultTResult()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
        bool selectorInvoked = false;

        // When
        ValueResult<string> actual = await ValueTask.FromResult(result).SelectManyAsync(Selector, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(actual.IsFailure);
        Assert.Equal(FailureException, actual.GetExceptionOrThrow());
        Assert.False(selectorInvoked);

        return;

        ValueResult<string> Selector()
        {
            selectorInvoked = true;
            return ValueResult<string>.Success("Success Result");
        }
    }

    [Fact(DisplayName = "ValueTask<ValueResult>.SelectManyAsync<TResult> of Success should invoke the selector delegate (Func<Task<ValueResult<TResult>>>) and return its result")]
    public async Task ValueTaskValueResultSelectManyAsyncOfSuccessShouldInvokeSelectorDelegateFuncTaskResultTResult()
    {
        // Given
        ValueResult result = ValueResult.Success();
        bool selectorInvoked = false;

        // When
        ValueResult<string> actual = await ValueTask.FromResult(result).SelectManyAsync(SelectorAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(actual.IsSuccess);
        Assert.True(selectorInvoked);
        Assert.Equal("Success Result", actual.GetValueOrThrow());

        return;

        async Task<ValueResult<string>> SelectorAsync()
        {
            selectorInvoked = true;
            return await Task.FromResult(ValueResult<string>.Success("Success Result"));
        }
    }

    [Fact(DisplayName = "ValueTask<ValueResult>.SelectManyAsync<TResult> of Success should invoke the selector delegate (Func<ValueTask<ValueResult<TResult>>>) and return its result")]
    public async Task ValueTaskValueResultSelectManyAsyncOfSuccessShouldInvokeSelectorDelegateFuncTaskResultTResultWithValueTaskDelegates()
    {
        // Given
        ValueResult result = ValueResult.Success();
        bool selectorInvoked = false;

        // When
        ValueResult<string> actual = await ValueTask.FromResult(result).SelectManyAsync(SelectorAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(actual.IsSuccess);
        Assert.True(selectorInvoked);
        Assert.Equal("Success Result", actual.GetValueOrThrow());

        return;

        async ValueTask<ValueResult<string>> SelectorAsync()
        {
            selectorInvoked = true;
            return await ValueTask.FromResult(ValueResult<string>.Success("Success Result"));
        }
    }

    [Fact(DisplayName = "ValueTask<ValueResult>.SelectManyAsync<TResult> of Failure should not invoke the selector delegate (Func<Task<ValueResult<TResult>>>)")]
    public async Task ValueTaskValueResultSelectManyAsyncOfFailureShouldNotInvokeSelectorDelegateFuncTaskResultTResult()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
        bool selectorInvoked = false;

        // When
        ValueResult<string> actual = await ValueTask.FromResult(result).SelectManyAsync(SelectorAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(actual.IsFailure);
        Assert.Equal(FailureException, actual.GetExceptionOrThrow());
        Assert.False(selectorInvoked);

        return;

        async Task<ValueResult<string>> SelectorAsync()
        {
            selectorInvoked = true;
            return await Task.FromResult(ValueResult<string>.Success("Success Result"));
        }
    }

    [Fact(DisplayName = "ValueTask<ValueResult>.SelectManyAsync<TResult> of Failure should not invoke the selector delegate (Func<ValueTask<ValueResult<TResult>>>)")]
    public async Task ValueTaskValueResultSelectManyAsyncOfFailureShouldNotInvokeSelectorDelegateFuncTaskResultTResultWithValueTaskDelegates()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
        bool selectorInvoked = false;

        // When
        ValueResult<string> actual = await ValueTask.FromResult(result).SelectManyAsync(SelectorAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(actual.IsFailure);
        Assert.Equal(FailureException, actual.GetExceptionOrThrow());
        Assert.False(selectorInvoked);

        return;

        async ValueTask<ValueResult<string>> SelectorAsync()
        {
            selectorInvoked = true;
            return await ValueTask.FromResult(ValueResult<string>.Success("Success Result"));
        }
    }

    [Fact(DisplayName = "ValueTask<ValueResult>.SelectManyAsync<TResult> of Success should invoke the selector delegate (Func<CancellationToken, Task<ValueResult<TResult>>>) and return its result")]
    public async Task ValueTaskValueResultSelectManyAsyncOfSuccessShouldInvokeSelectorDelegateFuncCancellationTokenTaskResultTResult()
    {
        // Given
        ValueResult result = ValueResult.Success();
        bool selectorInvoked = false;

        // When
        ValueResult<string> actual = await ValueTask.FromResult(result).SelectManyAsync(SelectorAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(actual.IsSuccess);
        Assert.True(selectorInvoked);
        Assert.Equal("Success Result", actual.GetValueOrThrow());

        return;

        async Task<ValueResult<string>> SelectorAsync(CancellationToken ct)
        {
            selectorInvoked = true;
            return await Task.FromResult(ValueResult<string>.Success("Success Result"));
        }
    }

    [Fact(DisplayName = "ValueTask<ValueResult>.SelectManyAsync<TResult> of Success should invoke the selector delegate (Func<CancellationToken, ValueTask<ValueResult<TResult>>>) and return its result")]
    public async Task ValueTaskValueResultSelectManyAsyncOfSuccessShouldInvokeSelectorDelegateFuncCancellationTokenTaskResultTResultWithValueTaskDelegates()
    {
        // Given
        ValueResult result = ValueResult.Success();
        bool selectorInvoked = false;

        // When
        ValueResult<string> actual = await ValueTask.FromResult(result).SelectManyAsync(SelectorAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(actual.IsSuccess);
        Assert.True(selectorInvoked);
        Assert.Equal("Success Result", actual.GetValueOrThrow());

        return;

        async ValueTask<ValueResult<string>> SelectorAsync(CancellationToken ct)
        {
            selectorInvoked = true;
            return await ValueTask.FromResult(ValueResult<string>.Success("Success Result"));
        }
    }

    [Fact(DisplayName = "ValueTask<ValueResult>.SelectManyAsync<TResult> of Failure should not invoke the selector delegate (Func<CancellationToken, Task<ValueResult<TResult>>>)")]
    public async Task ValueTaskValueResultSelectManyAsyncOfFailureShouldNotInvokeSelectorDelegateFuncCancellationTokenTaskResultTResult()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
        bool selectorInvoked = false;

        // When
        ValueResult<string> actual = await ValueTask.FromResult(result).SelectManyAsync(SelectorAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(actual.IsFailure);
        Assert.Equal(FailureException, actual.GetExceptionOrThrow());
        Assert.False(selectorInvoked);

        return;

        async Task<ValueResult<string>> SelectorAsync(CancellationToken ct)
        {
            selectorInvoked = true;
            return await Task.FromResult(ValueResult<string>.Success("Success Result"));
        }
    }

    [Fact(DisplayName = "ValueTask<ValueResult>.SelectManyAsync<TResult> of Failure should not invoke the selector delegate (Func<CancellationToken, ValueTask<ValueResult<TResult>>>)")]
    public async Task ValueTaskValueResultSelectManyAsyncOfFailureShouldNotInvokeSelectorDelegateFuncCancellationTokenTaskResultTResultWithValueTaskDelegates()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
        bool selectorInvoked = false;

        // When
        ValueResult<string> actual = await ValueTask.FromResult(result).SelectManyAsync(SelectorAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(actual.IsFailure);
        Assert.Equal(FailureException, actual.GetExceptionOrThrow());
        Assert.False(selectorInvoked);

        return;

        async ValueTask<ValueResult<string>> SelectorAsync(CancellationToken ct)
        {
            selectorInvoked = true;
            return await ValueTask.FromResult(ValueResult<string>.Success("Success Result"));
        }
    }

    [Fact(DisplayName = "ValueTask<ValueResult<T>>.SelectManyAsync of Success should invoke the selector delegate (Func<T, ValueResult>) and return its result")]
    public async Task ValueTaskValueResultOfTSelectManyAsyncOfSuccessShouldInvokeSelectorDelegateFuncTResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(42);
        bool selectorInvoked = false;

        // When
        ValueResult actual = await ValueTask.FromResult(result).SelectManyAsync(Selector, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(actual.IsSuccess);
        Assert.True(selectorInvoked);

        return;

        ValueResult Selector(int value)
        {
            selectorInvoked = value == 42;
            return ValueResult.Success();
        }
    }

    [Fact(DisplayName = "ValueTask<ValueResult<T>>.SelectManyAsync of Failure should not invoke the selector delegate (Func<T, ValueResult>)")]
    public async Task ValueTaskValueResultOfTSelectManyAsyncOfFailureShouldNotInvokeSelectorDelegateFuncTResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        bool selectorInvoked = false;

        // When
        ValueResult actual = await ValueTask.FromResult(result).SelectManyAsync(Selector, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(actual.IsFailure);
        Assert.Equal(FailureException, actual.GetExceptionOrThrow());
        Assert.False(selectorInvoked);

        return;

        ValueResult Selector(int value)
        {
            selectorInvoked = true;
            return ValueResult.Success();
        }
    }

    [Fact(DisplayName = "ValueTask<ValueResult<T>>.SelectManyAsync of Success should invoke the selector delegate (Func<T, Task<ValueResult>>) and return its result")]
    public async Task ValueTaskValueResultOfTSelectManyAsyncOfSuccessShouldInvokeSelectorDelegateFuncTTaskResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(42);
        bool selectorInvoked = false;

        // When
        ValueResult actual = await ValueTask.FromResult(result).SelectManyAsync(SelectorAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(actual.IsSuccess);
        Assert.True(selectorInvoked);

        return;

        async Task<ValueResult> SelectorAsync(int value)
        {
            selectorInvoked = value == 42;
            return await Task.FromResult(ValueResult.Success());
        }
    }

    [Fact(DisplayName = "ValueTask<ValueResult<T>>.SelectManyAsync of Success should invoke the selector delegate (Func<T, ValueTask<ValueResult>>) and return its result")]
    public async Task ValueTaskValueResultOfTSelectManyAsyncOfSuccessShouldInvokeSelectorDelegateFuncTTaskResultWithValueTaskDelegates()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(42);
        bool selectorInvoked = false;

        // When
        ValueResult actual = await ValueTask.FromResult(result).SelectManyAsync(SelectorAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(actual.IsSuccess);
        Assert.True(selectorInvoked);

        return;

        async ValueTask<ValueResult> SelectorAsync(int value)
        {
            selectorInvoked = value == 42;
            return await ValueTask.FromResult(ValueResult.Success());
        }
    }

    [Fact(DisplayName = "ValueTask<ValueResult<T>>.SelectManyAsync of Failure should not invoke the selector delegate (Func<T, Task<ValueResult>>)")]
    public async Task ValueTaskValueResultOfTSelectManyAsyncOfFailureShouldNotInvokeSelectorDelegateFuncTTaskResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        bool selectorInvoked = false;

        // When
        ValueResult actual = await ValueTask.FromResult(result).SelectManyAsync(SelectorAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(actual.IsFailure);
        Assert.Equal(FailureException, actual.GetExceptionOrThrow());
        Assert.False(selectorInvoked);

        return;

        async Task<ValueResult> SelectorAsync(int value)
        {
            selectorInvoked = true;
            return await Task.FromResult(ValueResult.Success());
        }
    }

    [Fact(DisplayName = "ValueTask<ValueResult<T>>.SelectManyAsync of Failure should not invoke the selector delegate (Func<T, ValueTask<ValueResult>>)")]
    public async Task ValueTaskValueResultOfTSelectManyAsyncOfFailureShouldNotInvokeSelectorDelegateFuncTTaskResultWithValueTaskDelegates()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        bool selectorInvoked = false;

        // When
        ValueResult actual = await ValueTask.FromResult(result).SelectManyAsync(SelectorAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(actual.IsFailure);
        Assert.Equal(FailureException, actual.GetExceptionOrThrow());
        Assert.False(selectorInvoked);

        return;

        async ValueTask<ValueResult> SelectorAsync(int value)
        {
            selectorInvoked = true;
            return await ValueTask.FromResult(ValueResult.Success());
        }
    }

    [Fact(DisplayName = "ValueTask<ValueResult<T>>.SelectManyAsync of Success should invoke the selector delegate (Func<T, CancellationToken, Task<ValueResult>>) and return its result")]
    public async Task ValueTaskValueResultOfTSelectManyAsyncOfSuccessShouldInvokeSelectorDelegateFuncTCancellationTokenTaskResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(42);
        bool selectorInvoked = false;

        // When
        ValueResult actual = await ValueTask.FromResult(result).SelectManyAsync(SelectorAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(actual.IsSuccess);
        Assert.True(selectorInvoked);

        return;

        async Task<ValueResult> SelectorAsync(int value, CancellationToken ct)
        {
            selectorInvoked = value == 42;
            return await Task.FromResult(ValueResult.Success());
        }
    }

    [Fact(DisplayName = "ValueTask<ValueResult<T>>.SelectManyAsync of Success should invoke the selector delegate (Func<T, CancellationToken, ValueTask<ValueResult>>) and return its result")]
    public async Task ValueTaskValueResultOfTSelectManyAsyncOfSuccessShouldInvokeSelectorDelegateFuncTCancellationTokenTaskResultWithValueTaskDelegates()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(42);
        bool selectorInvoked = false;

        // When
        ValueResult actual = await ValueTask.FromResult(result).SelectManyAsync(SelectorAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(actual.IsSuccess);
        Assert.True(selectorInvoked);

        return;

        async ValueTask<ValueResult> SelectorAsync(int value, CancellationToken ct)
        {
            selectorInvoked = value == 42;
            return await ValueTask.FromResult(ValueResult.Success());
        }
    }

    [Fact(DisplayName = "ValueTask<ValueResult<T>>.SelectManyAsync of Failure should not invoke the selector delegate (Func<T, CancellationToken, Task<ValueResult>>)")]
    public async Task ValueTaskValueResultOfTSelectManyAsyncOfFailureShouldNotInvokeSelectorDelegateFuncTCancellationTokenTaskResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        bool selectorInvoked = false;

        // When
        ValueResult actual = await ValueTask.FromResult(result).SelectManyAsync(SelectorAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(actual.IsFailure);
        Assert.Equal(FailureException, actual.GetExceptionOrThrow());
        Assert.False(selectorInvoked);

        return;

        async Task<ValueResult> SelectorAsync(int value, CancellationToken ct)
        {
            selectorInvoked = true;
            return await Task.FromResult(ValueResult.Success());
        }
    }

    [Fact(DisplayName = "ValueTask<ValueResult<T>>.SelectManyAsync of Failure should not invoke the selector delegate (Func<T, CancellationToken, ValueTask<ValueResult>>)")]
    public async Task ValueTaskValueResultOfTSelectManyAsyncOfFailureShouldNotInvokeSelectorDelegateFuncTCancellationTokenTaskResultWithValueTaskDelegates()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        bool selectorInvoked = false;

        // When
        ValueResult actual = await ValueTask.FromResult(result).SelectManyAsync(SelectorAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(actual.IsFailure);
        Assert.Equal(FailureException, actual.GetExceptionOrThrow());
        Assert.False(selectorInvoked);

        return;

        async ValueTask<ValueResult> SelectorAsync(int value, CancellationToken ct)
        {
            selectorInvoked = true;
            return await ValueTask.FromResult(ValueResult.Success());
        }
    }

    [Fact(DisplayName = "ValueTask<ValueResult<T>>.SelectManyAsync<TResult> of Success should invoke the selector delegate (Func<T, ValueResult<TResult>>) and return its result")]
    public async Task ValueTaskValueResultOfTSelectManyAsyncOfSuccessShouldInvokeSelectorDelegateFuncTResultTResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(42);
        bool selectorInvoked = false;

        // When
        ValueResult<string> actual = await ValueTask.FromResult(result).SelectManyAsync(Selector, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(actual.IsSuccess);
        Assert.True(selectorInvoked);
        Assert.Equal("Result is 42", actual.GetValueOrThrow());

        return;

        ValueResult<string> Selector(int value)
        {
            selectorInvoked = value == 42;
            return ValueResult<string>.Success($"Result is {value}");
        }
    }

    [Fact(DisplayName = "ValueTask<ValueResult<T>>.SelectManyAsync<TResult> of Failure should not invoke the selector delegate (Func<T, ValueResult<TResult>>)")]
    public async Task ValueTaskValueResultOfTSelectManyAsyncOfFailureShouldNotInvokeSelectorDelegateFuncTResultTResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        bool selectorInvoked = false;

        // When
        ValueResult<string> actual = await ValueTask.FromResult(result).SelectManyAsync(Selector, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(actual.IsFailure);
        Assert.Equal(FailureException, actual.GetExceptionOrThrow());
        Assert.False(selectorInvoked);

        return;

        ValueResult<string> Selector(int value)
        {
            selectorInvoked = true;
            return ValueResult<string>.Success($"Result is {value}");
        }
    }

    [Fact(DisplayName = "ValueTask<ValueResult<T>>.SelectManyAsync<TResult> of Success should invoke the selector delegate (Func<T, Task<ValueResult<TResult>>>) and return its result")]
    public async Task ValueTaskValueResultOfTSelectManyAsyncOfSuccessShouldInvokeSelectorDelegateFuncTTaskResultTResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(42);
        bool selectorInvoked = false;

        // When
        ValueResult<string> actual = await ValueTask.FromResult(result).SelectManyAsync(SelectorAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(actual.IsSuccess);
        Assert.True(selectorInvoked);
        Assert.Equal("Result is 42", actual.GetValueOrThrow());

        return;

        async Task<ValueResult<string>> SelectorAsync(int value)
        {
            selectorInvoked = value == 42;
            return await Task.FromResult(ValueResult<string>.Success($"Result is {value}"));
        }
    }

    [Fact(DisplayName = "ValueTask<ValueResult<T>>.SelectManyAsync<TResult> of Success should invoke the selector delegate (Func<T, ValueTask<ValueResult<TResult>>>) and return its result")]
    public async Task ValueTaskValueResultOfTSelectManyAsyncOfSuccessShouldInvokeSelectorDelegateFuncTTaskResultTResultWithValueTaskDelegates()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(42);
        bool selectorInvoked = false;

        // When
        ValueResult<string> actual = await ValueTask.FromResult(result).SelectManyAsync(SelectorAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(actual.IsSuccess);
        Assert.True(selectorInvoked);
        Assert.Equal("Result is 42", actual.GetValueOrThrow());

        return;

        async ValueTask<ValueResult<string>> SelectorAsync(int value)
        {
            selectorInvoked = value == 42;
            return await ValueTask.FromResult(ValueResult<string>.Success($"Result is {value}"));
        }
    }

    [Fact(DisplayName = "ValueTask<ValueResult<T>>.SelectManyAsync<TResult> of Failure should not invoke the selector delegate (Func<T, Task<ValueResult<TResult>>>)")]
    public async Task ValueTaskValueResultOfTSelectManyAsyncOfFailureShouldNotInvokeSelectorDelegateFuncTTaskResultTResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        bool selectorInvoked = false;

        // When
        ValueResult<string> actual = await ValueTask.FromResult(result).SelectManyAsync(SelectorAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(actual.IsFailure);
        Assert.Equal(FailureException, actual.GetExceptionOrThrow());
        Assert.False(selectorInvoked);

        return;

        async Task<ValueResult<string>> SelectorAsync(int value)
        {
            selectorInvoked = true;
            return await Task.FromResult(ValueResult<string>.Success($"Result is {value}"));
        }
    }

    [Fact(DisplayName = "ValueTask<ValueResult<T>>.SelectManyAsync<TResult> of Failure should not invoke the selector delegate (Func<T, ValueTask<ValueResult<TResult>>>)")]
    public async Task ValueTaskValueResultOfTSelectManyAsyncOfFailureShouldNotInvokeSelectorDelegateFuncTTaskResultTResultWithValueTaskDelegates()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        bool selectorInvoked = false;

        // When
        ValueResult<string> actual = await ValueTask.FromResult(result).SelectManyAsync(SelectorAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(actual.IsFailure);
        Assert.Equal(FailureException, actual.GetExceptionOrThrow());
        Assert.False(selectorInvoked);

        return;

        async ValueTask<ValueResult<string>> SelectorAsync(int value)
        {
            selectorInvoked = true;
            return await ValueTask.FromResult(ValueResult<string>.Success($"Result is {value}"));
        }
    }

    [Fact(DisplayName = "ValueTask<ValueResult<T>>.SelectManyAsync<TResult> of Success should invoke the selector delegate (Func<T, CancellationToken, Task<ValueResult<TResult>>>) and return its result")]
    public async Task ValueTaskValueResultOfTSelectManyAsyncOfSuccessShouldInvokeSelectorDelegateFuncTCancellationTokenTaskResultTResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(42);
        bool selectorInvoked = false;

        // When
        ValueResult<string> actual = await ValueTask.FromResult(result).SelectManyAsync(SelectorAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(actual.IsSuccess);
        Assert.True(selectorInvoked);
        Assert.Equal("Result is 42", actual.GetValueOrThrow());

        return;

        async Task<ValueResult<string>> SelectorAsync(int value, CancellationToken ct)
        {
            selectorInvoked = value == 42;
            return await Task.FromResult(ValueResult<string>.Success($"Result is {value}"));
        }
    }

    [Fact(DisplayName = "ValueTask<ValueResult<T>>.SelectManyAsync<TResult> of Success should invoke the selector delegate (Func<T, CancellationToken, ValueTask<ValueResult<TResult>>>) and return its result")]
    public async Task ValueTaskValueResultOfTSelectManyAsyncOfSuccessShouldInvokeSelectorDelegateFuncTCancellationTokenTaskResultTResultWithValueTaskDelegates()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(42);
        bool selectorInvoked = false;

        // When
        ValueResult<string> actual = await ValueTask.FromResult(result).SelectManyAsync(SelectorAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(actual.IsSuccess);
        Assert.True(selectorInvoked);
        Assert.Equal("Result is 42", actual.GetValueOrThrow());

        return;

        async ValueTask<ValueResult<string>> SelectorAsync(int value, CancellationToken ct)
        {
            selectorInvoked = value == 42;
            return await ValueTask.FromResult(ValueResult<string>.Success($"Result is {value}"));
        }
    }

    [Fact(DisplayName = "ValueTask<ValueResult<T>>.SelectManyAsync<TResult> of Failure should not invoke the selector delegate (Func<T, CancellationToken, Task<ValueResult<TResult>>>)")]
    public async Task ValueTaskValueResultOfTSelectManyAsyncOfFailureShouldNotInvokeSelectorDelegateFuncTCancellationTokenTaskResultTResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        bool selectorInvoked = false;

        // When
        ValueResult<string> actual = await ValueTask.FromResult(result).SelectManyAsync(SelectorAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(actual.IsFailure);
        Assert.Equal(FailureException, actual.GetExceptionOrThrow());
        Assert.False(selectorInvoked);

        return;

        async Task<ValueResult<string>> SelectorAsync(int value, CancellationToken ct)
        {
            selectorInvoked = true;
            return await Task.FromResult(ValueResult<string>.Success($"Result is {value}"));
        }
    }

    [Fact(DisplayName = "ValueTask<ValueResult<T>>.SelectManyAsync<TResult> of Failure should not invoke the selector delegate (Func<T, CancellationToken, ValueTask<ValueResult<TResult>>>)")]
    public async Task ValueTaskValueResultOfTSelectManyAsyncOfFailureShouldNotInvokeSelectorDelegateFuncTCancellationTokenTaskResultTResultWithValueTaskDelegates()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        bool selectorInvoked = false;

        // When
        ValueResult<string> actual = await ValueTask.FromResult(result).SelectManyAsync(SelectorAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(actual.IsFailure);
        Assert.Equal(FailureException, actual.GetExceptionOrThrow());
        Assert.False(selectorInvoked);

        return;

        async ValueTask<ValueResult<string>> SelectorAsync(int value, CancellationToken ct)
        {
            selectorInvoked = true;
            return await ValueTask.FromResult(ValueResult<string>.Success($"Result is {value}"));
        }
    }

    [Fact(DisplayName = "ValueTask of ValueResult Success.ThrowAsync should do nothing")]
    public async Task ValueTaskValueResultSuccessThrowAsyncShouldDoNothing()
    {
        // Given
        ValueResult result = ValueResult.Success();

        // When / Then
        await ValueTask.FromResult(result).ThrowAsync(cancellationToken: TestContext.Current.CancellationToken);
    }

    [Fact(DisplayName = "ValueTask of ValueResult Failure.Throw should throw Exception")]
    public async Task ValueTaskValueResultFailureThrowAsyncShouldThrowException()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);

        // When / Then
        await Assert.ThrowsAsync<Exception>(async () => await ValueTask.FromResult(result).ThrowAsync(cancellationToken: TestContext.Current.CancellationToken));
    }

    [Fact(DisplayName = "ValueTask of ValueResult<T> Success.ThrowAsync should do nothing")]
    public async Task ValueTaskValueResultOfTSuccessThrowAsyncShouldDoNothing()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(123);

        // When / Then
        await ValueTask.FromResult(result).ThrowAsync(cancellationToken: TestContext.Current.CancellationToken);
    }

    [Fact(DisplayName = "ValueTask of ValueResult<T> Failure.ThrowAsync should throw Exception")]
    public async Task ValueTaskValueResultOfTFailureThrowAsyncShouldThrowException()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);

        // When / Then
        await Assert.ThrowsAsync<Exception>(async () => await ValueTask.FromResult(result).ThrowAsync(cancellationToken: TestContext.Current.CancellationToken));
    }

    [Fact(DisplayName = "ValueTask<ValueResult<T>>.GetValueOrThrowAsync should produce the expected result when the ValueTask has not yet completed.")]
    public async Task ValueTaskValueResultOfTGetValueOrThrowAsyncShouldProduceExpectedResultWhenValueTaskHasNotYetCompleted()
    {
        // Given
        TaskCompletionSource<ValueResult<int>> source = new();
        ValueTask<int> pending = new ValueTask<ValueResult<int>>(source.Task).GetValueOrThrowAsync(TestContext.Current.CancellationToken);

        // When
        source.SetResult(ValueResult<int>.Success(123));
        int actual = await pending;

        // Then
        Assert.Equal(123, actual);
    }

    [Fact(DisplayName = "ValueTask<ValueResult<T>>.GetValueOrThrowAsync should throw OperationCanceledException when cancelled before the ValueTask completes.")]
    public async Task ValueTaskValueResultOfTGetValueOrThrowAsyncShouldThrowOperationCanceledExceptionWhenCancelledBeforeValueTaskCompletes()
    {
        // Given
        TaskCompletionSource<ValueResult<int>> source = new();
        using CancellationTokenSource cancellation = new();
        ValueTask<int> pending = new ValueTask<ValueResult<int>>(source.Task).GetValueOrThrowAsync(cancellation.Token);

        // When
        await cancellation.CancelAsync();
        source.SetResult(ValueResult<int>.Success(123));

        // Then
        await Assert.ThrowsAnyAsync<OperationCanceledException>(async () => await pending);
    }

    [Fact(DisplayName = "ValueTask<ValueResult<T>>.GetValueOrThrowAsync should produce the expected result when the ValueTask has already completed and the token is cancelled.")]
    public async Task ValueTaskValueResultOfTGetValueOrThrowAsyncShouldProduceExpectedResultWhenValueTaskHasCompletedAndTokenIsCancelled()
    {
        // Given
        ValueTask<ValueResult<int>> completed = new(ValueResult<int>.Success(123));
        CancellationToken cancellationToken = new(canceled: true);

        // When
        int actual = await completed.GetValueOrThrowAsync(cancellationToken);

        // Then
        Assert.Equal(123, actual);
    }
}
