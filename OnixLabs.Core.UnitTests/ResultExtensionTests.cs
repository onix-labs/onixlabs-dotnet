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

public sealed class ResultExtensionTests
{
    private static readonly Exception FailureException = new("Failure");

    [Fact(DisplayName = "Result Success.GetExceptionOrDefaultAsync should produce the expected result.")]
    public async Task ResultSuccessGetExceptionOrDefaultAsyncShouldProduceExpectedResult()
    {
        // Given
        Result result = Result.Success();

        // When
        Exception? actual = await Task.FromResult(result).GetExceptionOrDefaultAsync(token: TestContext.Current.CancellationToken);

        // Then
        Assert.Null(actual);
    }

    [Fact(DisplayName = "Result Success.GetExceptionOrDefaultAsync with default value should produce the expected result.")]
    public async Task ResultSuccessGetExceptionOrDefaultAsyncWithDefaultValueShouldProduceExpectedResult()
    {
        // Given
        Result result = Result.Success();

        // When
        Exception actual = await Task.FromResult(result).GetExceptionOrDefaultAsync(FailureException, token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(FailureException, actual);
    }

    [Fact(DisplayName = "Result Success.GetExceptionOrThrowAsync should produce the expected result.")]
    public async Task ResultSuccessGetExceptionOrThrowAsyncShouldProduceExpectedResult()
    {
        // Given
        Result result = Result.Success();

        // When
        Exception exception = await Assert.ThrowsAsync<InvalidOperationException>(async () =>
            await Task.FromResult(result).GetExceptionOrThrowAsync(token: TestContext.Current.CancellationToken));

        // Then
        Assert.Equal("The current result is not in a failure state.", exception.Message);
    }

    [Fact(DisplayName = "Result Failure.GetExceptionOrDefaultAsync should produce the expected result.")]
    public async Task ResultFailureGetExceptionOrDefaultAsyncShouldProduceExpectedResult()
    {
        // Given
        Result result = Result.Failure(FailureException);

        // When
        Exception? actual = await Task.FromResult(result).GetExceptionOrDefaultAsync(token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(FailureException, actual);
    }

    [Fact(DisplayName = "Result Failure.GetExceptionOrDefaultAsync with default value should produce the expected result.")]
    public async Task ResultFailureGetExceptionOrDefaultAsyncWithDefaultValueShouldProduceExpectedResult()
    {
        // Given
        Result result = Result.Failure(FailureException);

        // When
        Exception actual = await Task.FromResult(result).GetExceptionOrDefaultAsync(new Exception("unexpected exception"), token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(FailureException, actual);
    }

    [Fact(DisplayName = "Result Failure.GetExceptionOrThrowAsync should produce the expected result.")]
    public async Task ResultFailureGetExceptionOrThrowAsyncShouldProduceExpectedResult()
    {
        // Given
        Result result = Result.Failure(FailureException);

        // When
        Exception actual = await Task.FromResult(result).GetExceptionOrThrowAsync(token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(FailureException, actual);
    }

    [Fact(DisplayName = "Result<T> Success.GetExceptionOrDefaultAsync should produce the expected result.")]
    public async Task ResultOfTSuccessGetExceptionOrDefaultAsyncShouldProduceExpectedResult()
    {
        // Given
        Result<int> result = Result<int>.Success(123);

        // When
        Exception? actual = await Task.FromResult(result).GetExceptionOrDefaultAsync(token: TestContext.Current.CancellationToken);

        // Then
        Assert.Null(actual);
    }

    [Fact(DisplayName = "Result<T> Success.GetExceptionOrDefaultAsync with default value should produce the expected result.")]
    public async Task ResultOfTSuccessGetExceptionOrDefaultAsyncWithDefaultValueShouldProduceExpectedResult()
    {
        // Given
        Result<int> result = Result<int>.Success(123);

        // When
        Exception actual = await Task.FromResult(result).GetExceptionOrDefaultAsync(FailureException, token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(FailureException, actual);
    }

    [Fact(DisplayName = "Result<T> Success.GetExceptionOrThrowAsync should produce the expected result.")]
    public async Task ResultOfTSuccessGetExceptionOrThrowAsyncShouldProduceExpectedResult()
    {
        // Given
        Result<int> result = Result<int>.Success(123);

        // When
        Exception exception = await Assert.ThrowsAsync<InvalidOperationException>(async () =>
            await Task.FromResult(result).GetExceptionOrThrowAsync(token: TestContext.Current.CancellationToken));

        // Then
        Assert.Equal("The current result is not in a failure state.", exception.Message);
    }

    [Fact(DisplayName = "Result<T> Failure.GetExceptionOrDefaultAsync should produce the expected result.")]
    public async Task ResultOfTFailureGetExceptionOrDefaultAsyncShouldProduceExpectedResult()
    {
        // Given
        Result<int> result = Result<int>.Failure(FailureException);

        // When
        Exception? actual = await Task.FromResult(result).GetExceptionOrDefaultAsync(token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(FailureException, actual);
    }

    [Fact(DisplayName = "Result<T> Failure.GetExceptionOrDefaultAsync with default value should produce the expected result.")]
    public async Task ResultOfTFailureGetExceptionOrDefaultAsyncWithDefaultValueShouldProduceExpectedResult()
    {
        // Given
        Result<int> result = Result<int>.Failure(FailureException);

        // When
        Exception actual = await Task.FromResult(result).GetExceptionOrDefaultAsync(new Exception("unexpected exception"), token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(FailureException, actual);
    }

    [Fact(DisplayName = "Result<T> Failure.GetExceptionOrThrowAsync should produce the expected result.")]
    public async Task ResultOfTFailureGetExceptionOrThrowAsyncShouldProduceExpectedResult()
    {
        // Given
        Result<int> result = Result<int>.Failure(FailureException);

        // When
        Exception actual = await Task.FromResult(result).GetExceptionOrThrowAsync(token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(FailureException, actual);
    }

    [Fact(DisplayName = "Result.GetValueOrDefaultAsync should return the result value when the result is Success")]
    public async Task ResultGetValueOrDefaultAsyncShouldReturnResultValueWhenResultIsSuccess()
    {
        // Given
        const int expectedNumber = 123;
        const string expectedText = "abc";
        Task<Result<int>> numberTask = Result<int>.OfAsync(async () => await Task.FromResult(expectedNumber), TestContext.Current.CancellationToken);
        Task<Result<string>> textTask = Result<string>.OfAsync(async () => await Task.FromResult(expectedText), TestContext.Current.CancellationToken);

        // When
        int actualNumber = await numberTask.GetValueOrDefaultAsync(token: TestContext.Current.CancellationToken);
        string? actualText = await textTask.GetValueOrDefaultAsync(token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expectedNumber, actualNumber);

        Assert.NotNull(actualText);
        Assert.Equal(expectedText, actualText);
    }

    [Fact(DisplayName = "Result.GetValueOrDefaultAsync should return default when the result is Failure")]
    public async Task ResultGetValueOrDefaultAsyncShouldReturnDefaultWhenResultIsFailure()
    {
        // Given
        Task<Result<int>> numberTask = Result<int>.OfAsync(() => throw FailureException, TestContext.Current.CancellationToken);
        Task<Result<string>> textTask = Result<string>.OfAsync(() => throw FailureException, TestContext.Current.CancellationToken);

        // When
        int actualNumber = await numberTask.GetValueOrDefaultAsync(token: TestContext.Current.CancellationToken);
        string? actualText = await textTask.GetValueOrDefaultAsync(token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(0, actualNumber);
        Assert.Null(actualText);
    }

    [Fact(DisplayName = "Result.GetValueOrDefaultAsync with default value should return the result value when the result is Success")]
    public async Task ResultGetValueOrDefaultAsyncWithDefaultValueShouldReturnResultValueWhenResultIsSuccess()
    {
        // Given
        const int expectedNumber = 123;
        const string expectedText = "abc";
        Task<Result<int>> numberTask = Result<int>.OfAsync(async () => await Task.FromResult(expectedNumber), TestContext.Current.CancellationToken);
        Task<Result<string>> textTask = Result<string>.OfAsync(async () => await Task.FromResult(expectedText), TestContext.Current.CancellationToken);

        // When
        int actualNumber = await numberTask.GetValueOrDefaultAsync(456, token: TestContext.Current.CancellationToken);
        // ReSharper disable once VariableCanBeNotNullable
        string? actualText = await textTask.GetValueOrDefaultAsync("xyz", token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expectedNumber, actualNumber);

        Assert.NotNull(actualText);
        Assert.Equal(expectedText, actualText);
    }

    [Fact(DisplayName = "Result.GetValueOrDefaultAsync with default value should return default value when the result is Failure")]
    public async Task ResultGetValueOrDefaultAsyncWithDefaultValueShouldReturnDefaultValueWhenResultIsFailure()
    {
        // Given
        const int expectedNumber = 456;
        const string expectedText = "abc";
        Task<Result<int>> numberTask = Result<int>.OfAsync(() => throw FailureException, TestContext.Current.CancellationToken);
        Task<Result<string>> textTask = Result<string>.OfAsync(() => throw FailureException, TestContext.Current.CancellationToken);

        // When
        int actualNumber = await numberTask.GetValueOrDefaultAsync(expectedNumber, token: TestContext.Current.CancellationToken);
        string actualText = await textTask.GetValueOrDefaultAsync(expectedText, token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expectedNumber, actualNumber);

        Assert.NotNull(actualText);
        Assert.Equal(expectedText, actualText);
    }

    [Fact(DisplayName = "Result.GetValueOrThrowAsync should return the result value when the result is Success")]
    public async Task ResultGetValueOrThrowAsyncShouldReturnResultValueWhenResultIsSuccess()
    {
        // Given
        const int expectedNumber = 123;
        const string expectedText = "abc";
        Task<Result<int>> numberTask = Result<int>.OfAsync(async () => await Task.FromResult(expectedNumber), TestContext.Current.CancellationToken);
        Task<Result<string>> textTask = Result<string>.OfAsync(async () => await Task.FromResult(expectedText), TestContext.Current.CancellationToken);

        // When
        int actualNumber = await numberTask.GetValueOrThrowAsync(token: TestContext.Current.CancellationToken);
        string actualText = await textTask.GetValueOrThrowAsync(token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expectedNumber, actualNumber);

        Assert.NotNull(actualText);
        Assert.Equal(expectedText, actualText);
    }

    [Fact(DisplayName = "Result.GetValueOrThrowAsync should throw Exception when the result is Failure")]
    public async Task ResultGetValueOrThrowAsyncShouldReturnDefaultWhenResultIsFailure()
    {
        // Given
        Task<Result<int>> numberTask = Result<int>.OfAsync(() => throw FailureException, TestContext.Current.CancellationToken);
        Task<Result<string>> textTask = Result<string>.OfAsync(() => throw FailureException, TestContext.Current.CancellationToken);

        // When
        Exception numberException = await Assert.ThrowsAsync<Exception>(async () =>
            await numberTask.GetValueOrThrowAsync(token: TestContext.Current.CancellationToken));

        Exception textException = await Assert.ThrowsAsync<Exception>(async () =>
            await textTask.GetValueOrThrowAsync(token: TestContext.Current.CancellationToken));

        // Then
        Assert.Equal(numberException, FailureException);
        Assert.Equal(textException, FailureException);
    }

    [Fact(DisplayName = "Result<T>.GetValueOrNone should return the value wrapped in Optional when the result is Success and the value is not None")]
    public void ResultGetValueOrNoneShouldReturnOptionalValueWhenResultIsSuccessAndValueIsNotNone()
    {
        // Given
        const int expectedNumber = 123;
        const string expectedText = "abc";
        Result<int> numberResult = expectedNumber;
        Result<string> textResult = expectedText;

        // When
        Optional<int> actualNumber = numberResult.GetValueOrNone();
        Optional<string> actualText = textResult.GetValueOrNone();

        // Then
        Assert.Equal(Optional<int>.Of(expectedNumber), actualNumber);
        Assert.Equal(Optional<string>.Of(expectedText), actualText);
    }

    [Fact(DisplayName = "Result<T>.GetValueOrNone should return None when the result is Success and the value is None")]
    public void ResultGetValueOrNoneShouldReturnNoneWhenResultIsSuccessAndValueIsNone()
    {
        // Given
        Result<int> numberResult = Result<int>.Success(0);
        Result<string> textResult = Result<string>.Success(null!);

        // When
        Optional<int> actualNumber = numberResult.GetValueOrNone();
        Optional<string> actualText = textResult.GetValueOrNone();

        // Then
        Assert.Equal(Optional<int>.None, actualNumber);
        Assert.Equal(Optional<string>.None, actualText);
    }

    [Fact(DisplayName = "Result<T>.GetValueOrNone should return None when the result is Failure")]
    public void ResultGetValueOrNoneShouldReturnNoneWhenResultIsFailure()
    {
        // Given
        Result<int> numberResult = Result<int>.Failure(FailureException);
        Result<string> textResult = Result<string>.Failure(FailureException);

        // When
        Optional<int> actualNumber = numberResult.GetValueOrNone();
        Optional<string> actualText = textResult.GetValueOrNone();

        // Then
        Assert.Equal(Optional<int>.None, actualNumber);
        Assert.Equal(Optional<string>.None, actualText);
    }

    [Fact(DisplayName = "Result<T>.GetValueOrNoneAsync should return the value wrapped in Optional when the result is Success and the value is not None")]
    public async Task ResultGetValueOrNoneAsyncShouldReturnOptionalValueWhenResultIsSuccessAndValueIsNotNone()
    {
        // Given
        const int expectedNumber = 123;
        const string expectedText = "abc";
        Result<int> numberResult = expectedNumber;
        Result<string> textResult = expectedText;

        // When
        Optional<int> actualNumber = await Task.FromResult(numberResult).GetValueOrNoneAsync(token: TestContext.Current.CancellationToken);
        Optional<string> actualText = await Task.FromResult(textResult).GetValueOrNoneAsync(token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(Optional<int>.Of(expectedNumber), actualNumber);
        Assert.Equal(Optional<string>.Of(expectedText), actualText);
    }

    [Fact(DisplayName = "Result<T>.GetValueOrNoneAsync should return None when the result is Success and the value is None")]
    public async Task ResultGetValueOrNoneAsyncShouldReturnNoneWhenResultIsSuccessAndValueIsNone()
    {
        // Given
        Result<int> numberResult = Result<int>.Success(0);
        Result<string> textResult = Result<string>.Success(null!);

        // When
        Optional<int> actualNumber = await Task.FromResult(numberResult).GetValueOrNoneAsync(token: TestContext.Current.CancellationToken);
        Optional<string> actualText = await Task.FromResult(textResult).GetValueOrNoneAsync(token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(Optional<int>.None, actualNumber);
        Assert.Equal(Optional<string>.None, actualText);
    }

    [Fact(DisplayName = "Result<T>.GetValueOrNoneAsync should return None when the result is Failure")]
    public async Task ResultGetValueOrNoneAsyncShouldReturnNoneWhenResultIsFailure()
    {
        // Given
        Result<int> numberResult = Result<int>.Failure(FailureException);
        Result<string> textResult = Result<string>.Failure(FailureException);

        // When
        Optional<int> actualNumber = await Task.FromResult(numberResult).GetValueOrNoneAsync(token: TestContext.Current.CancellationToken);
        Optional<string> actualText = await Task.FromResult(textResult).GetValueOrNoneAsync(token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(Optional<int>.None, actualNumber);
        Assert.Equal(Optional<string>.None, actualText);
    }

    [Fact(DisplayName = "Result<Optional<T>>.GetValueOrNone should return the Optional value when the result is Success and the Optional value is not None")]
    public void ResultOptionalGetValueOrNoneShouldReturnOptionalValueWhenResultIsSuccessAndOptionalValueIsNotNone()
    {
        // Given
        Optional<int> expectedNumber = 123;
        Optional<string> expectedText = "abc";
        Result<Optional<int>> numberResult = expectedNumber;
        Result<Optional<string>> textResult = expectedText;

        // When
        Optional<int> actualNumber = numberResult.GetValueOrNone();
        Optional<string> actualText = textResult.GetValueOrNone();

        // Then
        Assert.Equal(expectedNumber, actualNumber);
        Assert.Equal(expectedText, actualText);
    }

    [Fact(DisplayName = "Result<Optional<T>>.GetValueOrNone should return None value when the result is Success and the Optional value is None")]
    public void ResultOptionalGetValueOrNoneShouldReturnNoneWhenResultIsSuccessAndOptionalValueIsNone()
    {
        // Given
        Optional<int> expectedNumber = Optional<int>.None;
        Optional<string> expectedText = Optional<string>.None;
        Result<Optional<int>> numberResult = expectedNumber;
        Result<Optional<string>> textResult = expectedText;

        // When
        Optional<int> actualNumber = numberResult.GetValueOrNone();
        Optional<string> actualText = textResult.GetValueOrNone();

        // Then
        Assert.Equal(expectedNumber, actualNumber);
        Assert.Equal(expectedText, actualText);
    }

    [Fact(DisplayName = "Result<Optional<T>>.GetValueOrNone should return None value when the result is Failure")]
    public void ResultOptionalGetValueOrNoneShouldReturnNoneWhenResultIsFailure()
    {
        // Given
        Optional<int> expectedNumber = Optional<int>.None;
        Optional<string> expectedText = Optional<string>.None;
        Result<Optional<int>> numberResult = Result<Optional<int>>.Failure(FailureException);
        Result<Optional<string>> textResult = Result<Optional<string>>.Failure(FailureException);

        // When
        Optional<int> actualNumber = numberResult.GetValueOrNone();
        Optional<string> actualText = textResult.GetValueOrNone();

        // Then
        Assert.Equal(expectedNumber, actualNumber);
        Assert.Equal(expectedText, actualText);
    }

    [Fact(DisplayName = "Result<Optional<T>>.GetValueOrNoneAsync should return the Optional value when the result is Success and the Optional value is not None")]
    public async Task ResultOptionalGetValueOrNoneAsyncShouldReturnOptionalValueWhenResultIsSuccessAndOptionalValueIsNotNone()
    {
        // Given
        Optional<int> expectedNumber = 123;
        Optional<string> expectedText = "abc";
        Result<Optional<int>> numberResult = expectedNumber;
        Result<Optional<string>> textResult = expectedText;

        // When
        Optional<int> actualNumber = await Task.FromResult(numberResult).GetValueOrNoneAsync(token: TestContext.Current.CancellationToken);
        Optional<string> actualText = await Task.FromResult(textResult).GetValueOrNoneAsync(token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expectedNumber, actualNumber);
        Assert.Equal(expectedText, actualText);
    }

    [Fact(DisplayName = "Result<Optional<T>>.GetValueOrNoneAsync should return None value when the result is Success and the Optional value is None")]
    public async Task ResultOptionalGetValueOrNoneAsyncShouldReturnNoneWhenResultIsSuccessAndOptionalValueIsNone()
    {
        // Given
        Optional<int> expectedNumber = Optional<int>.None;
        Optional<string> expectedText = Optional<string>.None;
        Result<Optional<int>> numberResult = expectedNumber;
        Result<Optional<string>> textResult = expectedText;

        // When
        Optional<int> actualNumber = await Task.FromResult(numberResult).GetValueOrNoneAsync(token: TestContext.Current.CancellationToken);
        Optional<string> actualText = await Task.FromResult(textResult).GetValueOrNoneAsync(token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expectedNumber, actualNumber);
        Assert.Equal(expectedText, actualText);
    }

    [Fact(DisplayName = "Result<Optional<T>>.GetValueOrNoneAsync should return None value when the result is Failure")]
    public async Task ResultOptionalGetValueOrNoneAsyncShouldReturnNoneWhenResultIsFailure()
    {
        // Given
        Optional<int> expectedNumber = Optional<int>.None;
        Optional<string> expectedText = Optional<string>.None;
        Result<Optional<int>> numberResult = Result<Optional<int>>.Failure(FailureException);
        Result<Optional<string>> textResult = Result<Optional<string>>.Failure(FailureException);

        // When
        Optional<int> actualNumber = await Task.FromResult(numberResult).GetValueOrNoneAsync(token: TestContext.Current.CancellationToken);
        Optional<string> actualText = await Task.FromResult(textResult).GetValueOrNoneAsync(token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expectedNumber, actualNumber);
        Assert.Equal(expectedText, actualText);
    }

    [Fact(DisplayName = "Result.GetOptionalValueOrThrow should return the Optional value when the Result is Success and the Optional value is not None")]
    public void ResultGetOptionalValueOrThrowShouldReturnOptionalValueWhenResultIsSuccessAndOptionalValueIsNotNone()
    {
        // Given
        const int expectedNumber = 123;
        const string expectedText = "abc";
        Result<Optional<int>> numberResult = Optional<int>.Some(expectedNumber);
        Result<Optional<string>> textResult = Optional<string>.Some(expectedText);

        // When
        int actualNumber = numberResult.GetOptionalValueOrThrow();
        string actualText = textResult.GetOptionalValueOrThrow();

        // Then
        Assert.Equal(expectedNumber, actualNumber);
        Assert.Equal(expectedText, actualText);
    }

    [Fact(DisplayName = "Result.GetOptionalValueOrThrow should throw InvalidOperationException when the Result is Success and the Optional value is None")]
    public void ResultGetOptionalValueOrThrowShouldThrowInvalidOperationExceptionWhenResultIsSuccessAndOptionalValueIsNone()
    {
        // Given
        Result<Optional<int>> numberResult = Optional<int>.None;
        Result<Optional<string>> textResult = Optional<string>.None;

        // When
        Exception numberException = Assert.Throws<InvalidOperationException>(() => numberResult.GetOptionalValueOrThrow());
        Exception textException = Assert.Throws<InvalidOperationException>(() => textResult.GetOptionalValueOrThrow());

        // Then
        Assert.Equal("Optional value of type System.Int32 is not present.", numberException.Message);
        Assert.Equal("Optional value of type System.String is not present.", textException.Message);
    }

    [Fact(DisplayName = "Result.GetOptionalValueOrThrow should throw InvalidOperationException when the Result is Failure")]
    public void ResultGetOptionalValueOrThrowShouldThrowInvalidOperationExceptionWhenResultIsFailure()
    {
        // Given
        Result<Optional<int>> numberResult = Result<Optional<int>>.Failure(new Exception("Result has failed."));
        Result<Optional<string>> textResult = Result<Optional<string>>.Failure(new Exception("Result has failed."));

        // When
        Exception numberException = Assert.Throws<Exception>(() => numberResult.GetOptionalValueOrThrow());
        Exception textException = Assert.Throws<Exception>(() => textResult.GetOptionalValueOrThrow());

        // Then
        Assert.Equal("Result has failed.", numberException.Message);
        Assert.Equal("Result has failed.", textException.Message);
    }

    [Fact(DisplayName = "Result.GetOptionalValueOrThrowAsync should return the Optional value when the Result is Success and the Optional value is not None")]
    public async Task ResultGetOptionalValueOrThrowAsyncShouldReturnOptionalValueWhenResultIsSuccessAndOptionalValueIsNotNone()
    {
        // Given
        const int expectedNumber = 123;
        const string expectedText = "abc";
        Result<Optional<int>> numberResult = Optional<int>.Some(expectedNumber);
        Result<Optional<string>> textResult = Optional<string>.Some(expectedText);

        // When
        int actualNumber = await Task.FromResult(numberResult).GetOptionalValueOrThrowAsync(token: TestContext.Current.CancellationToken);
        string actualText = await Task.FromResult(textResult).GetOptionalValueOrThrowAsync(token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expectedNumber, actualNumber);
        Assert.Equal(expectedText, actualText);
    }

    [Fact(DisplayName = "Result.GetOptionalValueOrThrowAsync should throw InvalidOperationException when the Result is Success and the Optional value is None")]
    public async Task ResultGetOptionalValueOrThrowAsyncShouldThrowInvalidOperationExceptionWhenResultIsSuccessAndOptionalValueIsNone()
    {
        // Given
        Result<Optional<int>> numberResult = Optional<int>.None;
        Result<Optional<string>> textResult = Optional<string>.None;

        // When
        Exception numberException = await Assert.ThrowsAsync<InvalidOperationException>(async () =>
            await Task.FromResult(numberResult).GetOptionalValueOrThrowAsync(token: TestContext.Current.CancellationToken));

        Exception textException = await Assert.ThrowsAsync<InvalidOperationException>(async () =>
            await Task.FromResult(textResult).GetOptionalValueOrThrowAsync(token: TestContext.Current.CancellationToken));

        // Then
        Assert.Equal("Optional value of type System.Int32 is not present.", numberException.Message);
        Assert.Equal("Optional value of type System.String is not present.", textException.Message);
    }

    [Fact(DisplayName = "Result.GetOptionalValueOrThrowAsync should throw InvalidOperationException when the Result is Failure")]
    public async Task ResultGetOptionalValueOrThrowAsyncShouldThrowInvalidOperationExceptionWhenResultIsFailure()
    {
        // Given
        Result<Optional<int>> numberResult = Result<Optional<int>>.Failure(new Exception("Result has failed."));
        Result<Optional<string>> textResult = Result<Optional<string>>.Failure(new Exception("Result has failed."));

        // When
        Exception numberException = await Assert.ThrowsAsync<Exception>(async () =>
            await Task.FromResult(numberResult).GetOptionalValueOrThrowAsync(token: TestContext.Current.CancellationToken));

        Exception textException = await Assert.ThrowsAsync<Exception>(async () =>
            await Task.FromResult(textResult).GetOptionalValueOrThrowAsync(token: TestContext.Current.CancellationToken));

        // Then
        Assert.Equal("Result has failed.", numberException.Message);
        Assert.Equal("Result has failed.", textException.Message);
    }

    [Fact(DisplayName = "Result.GetOptionalValueOrDefault should return the Optional value when the Result is Success and the Optional value is not None")]
    public void ResultGetOptionalValueOrDefaultShouldReturnOptionalValueWhenResultIsSuccessAndOptionalValueIsNotNone()
    {
        // Given
        const int expectedNumber = 123;
        const string expectedText = "abc";
        Result<Optional<int>> numberResult = Optional<int>.Some(expectedNumber);
        Result<Optional<string>> textResult = Optional<string>.Some(expectedText);

        // When
        int actualNumber = numberResult.GetOptionalValueOrDefault(456);
        string actualText = textResult.GetOptionalValueOrDefault("xyz");

        // Then
        Assert.Equal(expectedNumber, actualNumber);
        Assert.Equal(expectedText, actualText);
    }

    [Fact(DisplayName = "Result.GetOptionalValueOrDefault should throw return the default value when the Result is Success and the Optional value is None")]
    public void ResultGetOptionalValueOrDefaultShouldReturnDefaultValueWhenResultIsSuccessAndOptionalValueIsNone()
    {
        // Given
        const int expectedNumber = 456;
        const string expectedText = "xyz";
        Result<Optional<int>> numberResult = Optional<int>.None;
        Result<Optional<string>> textResult = Optional<string>.None;

        // When
        int actualNumber = numberResult.GetOptionalValueOrDefault(expectedNumber);
        string actualText = textResult.GetOptionalValueOrDefault(expectedText);

        // Then
        Assert.Equal(expectedNumber, actualNumber);
        Assert.Equal(expectedText, actualText);
    }

    [Fact(DisplayName = "Result.GetOptionalValueOrDefault should return default value when the result is Failure")]
    public void ResultGetOptionalValueOrDefaultShouldReturnDefaultValueWhenResultIsFailure()
    {
        // Given
        const int expectedNumber = 456;
        const string expectedText = "xyz";
        Result<Optional<int>> numberResult = Result<Optional<int>>.Failure(new Exception("Result has failed."));
        Result<Optional<string>> textResult = Result<Optional<string>>.Failure(new Exception("Result has failed."));

        // When
        int actualNumber = numberResult.GetOptionalValueOrDefault(expectedNumber);
        string actualText = textResult.GetOptionalValueOrDefault(expectedText);

        // Then
        Assert.Equal(expectedNumber, actualNumber);
        Assert.Equal(expectedText, actualText);
    }


    [Fact(DisplayName = "Result.GetOptionalValueOrDefaultAsync should return the Optional value when the Result is Success and the Optional value is not None")]
    public async Task ResultGetOptionalValueOrDefaultAsyncShouldReturnOptionalValueWhenResultIsSuccessAndOptionalValueIsNotNone()
    {
        // Given
        const int expectedNumber = 123;
        const string expectedText = "abc";
        Result<Optional<int>> numberResult = Optional<int>.Some(expectedNumber);
        Result<Optional<string>> textResult = Optional<string>.Some(expectedText);

        // When
        int actualNumber = await Task.FromResult(numberResult).GetOptionalValueOrDefaultAsync(456, token: TestContext.Current.CancellationToken);
        string actualText = await Task.FromResult(textResult).GetOptionalValueOrDefaultAsync("xyz", token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expectedNumber, actualNumber);
        Assert.Equal(expectedText, actualText);
    }

    [Fact(DisplayName = "Result.GetOptionalValueOrDefaultAsync should throw return the default value when the Result is Success and the Optional value is None")]
    public async Task ResultGetOptionalValueOrDefaultAsyncShouldReturnDefaultValueWhenResultIsSuccessAndOptionalValueIsNone()
    {
        // Given
        const int expectedNumber = 456;
        const string expectedText = "xyz";
        Result<Optional<int>> numberResult = Optional<int>.None;
        Result<Optional<string>> textResult = Optional<string>.None;

        // When
        int actualNumber = await Task.FromResult(numberResult).GetOptionalValueOrDefaultAsync(expectedNumber, token: TestContext.Current.CancellationToken);
        string actualText = await Task.FromResult(textResult).GetOptionalValueOrDefaultAsync(expectedText, token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expectedNumber, actualNumber);
        Assert.Equal(expectedText, actualText);
    }

    [Fact(DisplayName = "Result.GetOptionalValueOrDefaultAsync should return default value when the result is Failure")]
    public async Task ResultGetOptionalValueOrDefaultAsyncShouldReturnDefaultValueWhenResultIsFailure()
    {
        // Given
        const int expectedNumber = 456;
        const string expectedText = "xyz";
        Result<Optional<int>> numberResult = Result<Optional<int>>.Failure(new Exception("Result has failed."));
        Result<Optional<string>> textResult = Result<Optional<string>>.Failure(new Exception("Result has failed."));

        // When
        int actualNumber = await Task.FromResult(numberResult).GetOptionalValueOrDefaultAsync(expectedNumber, token: TestContext.Current.CancellationToken);
        string actualText = await Task.FromResult(textResult).GetOptionalValueOrDefaultAsync(expectedText, token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expectedNumber, actualNumber);
        Assert.Equal(expectedText, actualText);
    }

    [Fact(DisplayName = "Task<Result>.MatchAsync of Success should invoke the success delegate (Action, Action<Exception>)")]
    public async Task TaskResultMatchAsyncOfSuccessShouldInvokeSuccessDelegateActionActionException()
    {
        // Given
        Result result = Result.Success();
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await Task.FromResult(result).MatchAsync(Success, Failure, token: TestContext.Current.CancellationToken);

        // Then
        Assert.True(successCalled);
        Assert.False(failureCalled);

        return;
        void Success() => successCalled = true;
        void Failure(Exception exception) => failureCalled = true;
    }

    [Fact(DisplayName = "Task<Result>.MatchAsync of Failure should invoke the failure delegate (Action, Action<Exception>)")]
    public async Task TaskResultMatchAsyncOfFailureShouldInvokeFailureDelegateActionActionException()
    {
        // Given
        Result result = Result.Failure(FailureException);
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await Task.FromResult(result).MatchAsync(Success, Failure, token: TestContext.Current.CancellationToken);

        // Then
        Assert.False(successCalled);
        Assert.True(failureCalled);

        return;
        void Success() => successCalled = true;
        void Failure(Exception exception) => failureCalled = true;
    }

    [Fact(DisplayName = "Task<Result>.MatchAsync of Success should invoke the success delegate (Func<Task>, Action<Exception>)")]
    public async Task TaskResultMatchAsyncOfSuccessShouldInvokeSuccessDelegateFuncTaskActionException()
    {
        // Given
        Result result = Result.Success();
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await Task.FromResult(result).MatchAsync(SuccessAsync, Failure, token: TestContext.Current.CancellationToken);

        // Then
        Assert.True(successCalled);
        Assert.False(failureCalled);

        return;
        async Task SuccessAsync() => await Task.Run(() => successCalled = true);
        void Failure(Exception exception) => failureCalled = true;
    }

    [Fact(DisplayName = "Task<Result>.MatchAsync of Failure should invoke the failure delegate (Func<Task>, Action<Exception>)")]
    public async Task TaskResultMatchAsyncOfFailureShouldInvokeFailureDelegateFuncTaskActionException()
    {
        // Given
        Result result = Result.Failure(FailureException);
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await Task.FromResult(result).MatchAsync(SuccessAsync, Failure, token: TestContext.Current.CancellationToken);

        // Then
        Assert.False(successCalled);
        Assert.True(failureCalled);

        return;
        async Task SuccessAsync() => await Task.Run(() => successCalled = true);
        void Failure(Exception exception) => failureCalled = true;
    }

    [Fact(DisplayName = "Task<Result>.MatchAsync of Success should invoke the success delegate (Func<CancellationToken, Task>, Action<Exception>)")]
    public async Task TaskResultMatchAsyncOfSuccessShouldInvokeSuccessDelegateFuncCancellationTokenTaskActionException()
    {
        // Given
        Result result = Result.Success();
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await Task.FromResult(result).MatchAsync(SuccessAsync, Failure, token: TestContext.Current.CancellationToken);

        // Then
        Assert.True(successCalled);
        Assert.False(failureCalled);

        return;
        async Task SuccessAsync(CancellationToken ct) => await Task.Run(() => successCalled = true, ct);
        void Failure(Exception exception) => failureCalled = true;
    }

    [Fact(DisplayName = "Task<Result>.MatchAsync of Failure should invoke the failure delegate (Func<CancellationToken, Task>, Action<Exception>)")]
    public async Task TaskResultMatchAsyncOfFailureShouldInvokeFailureDelegateFuncCancellationTokenTaskActionException()
    {
        // Given
        Result result = Result.Failure(FailureException);
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await Task.FromResult(result).MatchAsync(SuccessAsync, Failure, token: TestContext.Current.CancellationToken);

        // Then
        Assert.False(successCalled);
        Assert.True(failureCalled);

        return;
        async Task SuccessAsync(CancellationToken ct) => await Task.Run(() => successCalled = true, ct);
        void Failure(Exception exception) => failureCalled = true;
    }

    [Fact(DisplayName = "Task<Result>.MatchAsync of Success should invoke the success delegate (Action, Func<Exception, Task>)")]
    public async Task TaskResultMatchAsyncOfSuccessShouldInvokeSuccessDelegateActionFuncExceptionTask()
    {
        // Given
        Result result = Result.Success();
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await Task.FromResult(result).MatchAsync(Success, FailureAsync, token: TestContext.Current.CancellationToken);

        // Then
        Assert.True(successCalled);
        Assert.False(failureCalled);

        return;
        void Success() => successCalled = true;
        async Task FailureAsync(Exception exception) => await Task.Run(() => failureCalled = true);
    }

    [Fact(DisplayName = "Task<Result>.MatchAsync of Failure should invoke the failure delegate (Action, Func<Exception, Task>)")]
    public async Task TaskResultMatchAsyncOfFailureShouldInvokeFailureDelegateActionFuncExceptionTask()
    {
        // Given
        Result result = Result.Failure(FailureException);
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await Task.FromResult(result).MatchAsync(Success, FailureAsync, token: TestContext.Current.CancellationToken);

        // Then
        Assert.False(successCalled);
        Assert.True(failureCalled);

        return;
        void Success() => successCalled = true;
        async Task FailureAsync(Exception exception) => await Task.Run(() => failureCalled = true);
    }

    [Fact(DisplayName = "Task<Result>.MatchAsync of Success should invoke the success delegate (Action, Func<Exception, CancellationToken, Task>)")]
    public async Task TaskResultMatchAsyncOfSuccessShouldInvokeSuccessDelegateActionFuncExceptionCancellationTokenTask()
    {
        // Given
        Result result = Result.Success();
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await Task.FromResult(result).MatchAsync(Success, FailureAsync, token: TestContext.Current.CancellationToken);

        // Then
        Assert.True(successCalled);
        Assert.False(failureCalled);

        return;
        void Success() => successCalled = true;
        async Task FailureAsync(Exception exception, CancellationToken ct) => await Task.Run(() => failureCalled = true, ct);
    }

    [Fact(DisplayName = "Task<Result>.MatchAsync of Failure should invoke the failure delegate (Action, Func<Exception, CancellationToken, Task>)")]
    public async Task TaskResultMatchAsyncOfFailureShouldInvokeFailureDelegateActionFuncExceptionCancellationTokenTask()
    {
        // Given
        Result result = Result.Failure(FailureException);
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await Task.FromResult(result).MatchAsync(Success, FailureAsync, token: TestContext.Current.CancellationToken);

        // Then
        Assert.False(successCalled);
        Assert.True(failureCalled);

        return;
        void Success() => successCalled = true;
        async Task FailureAsync(Exception exception, CancellationToken ct) => await Task.Run(() => failureCalled = true, ct);
    }

    [Fact(DisplayName = "Task<Result>.MatchAsync of Success should invoke the success delegate (Func<Task>, Func<Exception, Task>)")]
    public async Task TaskResultMatchAsyncOfSuccessShouldInvokeSuccessDelegateFuncTaskFuncExceptionTask()
    {
        // Given
        Result result = Result.Success();
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await Task.FromResult(result).MatchAsync(SuccessAsync, FailureAsync, token: TestContext.Current.CancellationToken);

        // Then
        Assert.True(successCalled);
        Assert.False(failureCalled);

        return;
        async Task SuccessAsync() => await Task.Run(() => successCalled = true);
        async Task FailureAsync(Exception exception) => await Task.Run(() => failureCalled = true);
    }

    [Fact(DisplayName = "Task<Result>.MatchAsync of Failure should invoke the failure delegate (Func<Task>, Func<Exception, Task>)")]
    public async Task TaskResultMatchAsyncOfFailureShouldInvokeFailureDelegateFuncTaskFuncExceptionTask()
    {
        // Given
        Result result = Result.Failure(FailureException);
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await Task.FromResult(result).MatchAsync(SuccessAsync, FailureAsync, token: TestContext.Current.CancellationToken);

        // Then
        Assert.False(successCalled);
        Assert.True(failureCalled);

        return;
        async Task SuccessAsync() => await Task.Run(() => successCalled = true);
        async Task FailureAsync(Exception exception) => await Task.Run(() => failureCalled = true);
    }

    [Fact(DisplayName = "Task<Result>.MatchAsync of Success should invoke the success delegate (Func<CancellationToken, Task>, Func<Exception, CancellationToken, Task>)")]
    public async Task TaskResultMatchAsyncOfSuccessShouldInvokeSuccessDelegateFuncCancellationTokenTaskFuncExceptionCancellationTokenTask()
    {
        // Given
        Result result = Result.Success();
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await Task.FromResult(result).MatchAsync(SuccessAsync, FailureAsync, token: TestContext.Current.CancellationToken);

        // Then
        Assert.True(successCalled);
        Assert.False(failureCalled);

        return;
        async Task SuccessAsync(CancellationToken ct) => await Task.Run(() => successCalled = true, ct);
        async Task FailureAsync(Exception exception, CancellationToken ct) => await Task.Run(() => failureCalled = true, ct);
    }

    [Fact(DisplayName = "Task<Result>.MatchAsync of Failure should invoke the failure delegate (Func<CancellationToken, Task>, Func<Exception, CancellationToken, Task>)")]
    public async Task TaskResultMatchAsyncOfFailureShouldInvokeFailureDelegateFuncCancellationTokenTaskFuncExceptionCancellationTokenTask()
    {
        // Given
        Result result = Result.Failure(FailureException);
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await Task.FromResult(result).MatchAsync(SuccessAsync, FailureAsync, token: TestContext.Current.CancellationToken);

        // Then
        Assert.False(successCalled);
        Assert.True(failureCalled);

        return;
        async Task SuccessAsync(CancellationToken ct) => await Task.Run(() => successCalled = true, ct);
        async Task FailureAsync(Exception exception, CancellationToken ct) => await Task.Run(() => failureCalled = true, ct);
    }

    [Fact(DisplayName = "Task<Result>.MatchAsync<TResult> of Success should return the result from success delegate (Func<TResult>, Func<Exception, TResult>)")]
    public async Task TaskResultMatchAsyncOfSuccessShouldReturnResultFromSuccessDelegateFuncResultFuncExceptionResult()
    {
        // Given
        Result result = Result.Success();
        const string expected = "Success";

        // When
        string actual = await Task.FromResult(result).MatchAsync(Success, Failure, token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        string Success() => "Success";
        string Failure(Exception exception) => "Failure";
    }

    [Fact(DisplayName = "Task<Result>.MatchAsync<TResult> of Failure should return the result from failure delegate (Func<TResult>, Func<Exception, TResult>)")]
    public async Task TaskResultMatchAsyncOfFailureShouldReturnResultFromFailureDelegateFuncResultFuncExceptionResult()
    {
        // Given
        Result result = Result.Failure(FailureException);
        const string expected = "Failure";

        // When
        string actual = await Task.FromResult(result).MatchAsync(Success, Failure, token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        string Success() => "Success";
        string Failure(Exception exception) => "Failure";
    }

    [Fact(DisplayName = "Task<Result>.MatchAsync<TResult> of Success should return the result from success delegate (Func<Task<TResult>>, Func<Exception, TResult>)")]
    public async Task TaskResultMatchAsyncOfSuccessShouldReturnResultFromSuccessDelegateFuncTaskResultFuncExceptionResult()
    {
        // Given
        Result result = Result.Success();
        const string expected = "Success";

        // When
        string actual = await Task.FromResult(result).MatchAsync(SuccessAsync, Failure, token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        async Task<string> SuccessAsync() => await Task.FromResult("Success");
        string Failure(Exception exception) => "Failure";
    }

    [Fact(DisplayName = "Task<Result>.MatchAsync<TResult> of Failure should return the result from failure delegate (Func<Task<TResult>>, Func<Exception, TResult>)")]
    public async Task TaskResultMatchAsyncOfFailureShouldReturnResultFromFailureDelegateFuncTaskResultFuncExceptionResult()
    {
        // Given
        Result result = Result.Failure(FailureException);
        const string expected = "Failure";

        // When
        string actual = await Task.FromResult(result).MatchAsync(SuccessAsync, Failure, token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        async Task<string> SuccessAsync() => await Task.FromResult("Success");
        string Failure(Exception exception) => "Failure";
    }

    [Fact(DisplayName = "Task<Result>.MatchAsync<TResult> of Success should return the result from success delegate (Func<CancellationToken, Task<TResult>>, Func<Exception, TResult>)")]
    public async Task TaskResultMatchAsyncOfSuccessShouldReturnResultFromSuccessDelegateFuncCancellationTokenTaskResultFuncExceptionResult()
    {
        // Given
        Result result = Result.Success();
        const string expected = "Success";

        // When
        string actual = await Task.FromResult(result).MatchAsync(SuccessAsync, Failure, token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        async Task<string> SuccessAsync(CancellationToken ct) => await Task.FromResult("Success");
        string Failure(Exception exception) => "Failure";
    }

    [Fact(DisplayName = "Task<Result>.MatchAsync<TResult> of Failure should return the result from failure delegate (Func<CancellationToken, Task<TResult>>, Func<Exception, TResult>)")]
    public async Task TaskResultMatchAsyncOfFailureShouldReturnResultFromFailureDelegateFuncCancellationTokenTaskResultFuncExceptionResult()
    {
        // Given
        Result result = Result.Failure(FailureException);
        const string expected = "Failure";

        // When
        string actual = await Task.FromResult(result).MatchAsync(SuccessAsync, Failure, token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        async Task<string> SuccessAsync(CancellationToken ct) => await Task.FromResult("Success");
        string Failure(Exception exception) => "Failure";
    }

    [Fact(DisplayName = "Task<Result>.MatchAsync<TResult> of Success should return the result from success delegate (Func<TResult>, Func<Exception, Task<TResult>>>)")]
    public async Task TaskResultMatchAsyncOfSuccessShouldReturnResultFromSuccessDelegateFuncResultFuncExceptionTaskResult()
    {
        // Given
        Result result = Result.Success();
        const string expected = "Success";

        // When
        string actual = await Task.FromResult(result).MatchAsync(Success, FailureAsync, token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        string Success() => "Success";
        async Task<string> FailureAsync(Exception exception) => await Task.FromResult("Failure");
    }

    [Fact(DisplayName = "Task<Result>.MatchAsync<TResult> of Failure should return the result from failure delegate (Func<TResult>, Func<Exception, Task<TResult>>>)")]
    public async Task TaskResultMatchAsyncOfFailureShouldReturnResultFromFailureDelegateFuncResultFuncExceptionTaskResult()
    {
        // Given
        Result result = Result.Failure(FailureException);
        const string expected = "Failure";

        // When
        string actual = await Task.FromResult(result).MatchAsync(Success, FailureAsync, token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        string Success() => "Success";
        async Task<string> FailureAsync(Exception exception) => await Task.FromResult("Failure");
    }

    [Fact(DisplayName = "Task<Result>.MatchAsync<TResult> of Success should return the result from success delegate (Func<TResult>, Func<Exception, CancellationToken, Task<TResult>>>)")]
    public async Task TaskResultMatchAsyncOfSuccessShouldReturnResultFromSuccessDelegateFuncResultFuncExceptionCancellationTokenTaskResult()
    {
        // Given
        Result result = Result.Success();
        const string expected = "Success";

        // When
        string actual = await Task.FromResult(result).MatchAsync(Success, FailureAsync, token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        string Success() => "Success";
        async Task<string> FailureAsync(Exception exception, CancellationToken ct) => await Task.FromResult("Failure");
    }

    [Fact(DisplayName = "Task<Result>.MatchAsync<TResult> of Failure should return the result from failure delegate (Func<TResult>, Func<Exception, CancellationToken, Task<TResult>>>)")]
    public async Task TaskResultMatchAsyncOfFailureShouldReturnResultFromFailureDelegateFuncResultFuncExceptionCancellationTokenTaskResult()
    {
        // Given
        Result result = Result.Failure(FailureException);
        const string expected = "Failure";

        // When
        string actual = await Task.FromResult(result).MatchAsync(Success, FailureAsync, token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        string Success() => "Success";
        async Task<string> FailureAsync(Exception exception, CancellationToken ct) => await Task.FromResult("Failure");
    }

    [Fact(DisplayName = "Task<Result>.MatchAsync<TResult> of Success should return the result from success delegate (Func<Task<TResult>>, Func<Exception, Task<TResult>>>)")]
    public async Task TaskResultMatchAsyncOfSuccessShouldReturnResultFromSuccessDelegateFuncTaskResultFuncExceptionTaskResult()
    {
        // Given
        Result result = Result.Success();
        const string expected = "Success";

        // When
        string actual = await Task.FromResult(result).MatchAsync(SuccessAsync, FailureAsync, token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        async Task<string> SuccessAsync() => await Task.FromResult("Success");
        async Task<string> FailureAsync(Exception exception) => await Task.FromResult("Failure");
    }

    [Fact(DisplayName = "Task<Result>.MatchAsync<TResult> of Failure should return the result from failure delegate (Func<Task<TResult>>, Func<Exception, Task<TResult>>>)")]
    public async Task TaskResultMatchAsyncOfFailureShouldReturnResultFromFailureDelegateFuncTaskResultFuncExceptionTaskResult()
    {
        // Given
        Result result = Result.Failure(FailureException);
        const string expected = "Failure";

        // When
        string actual = await Task.FromResult(result).MatchAsync(SuccessAsync, FailureAsync, token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        async Task<string> SuccessAsync() => await Task.FromResult("Success");
        async Task<string> FailureAsync(Exception exception) => await Task.FromResult("Failure");
    }

    [Fact(DisplayName = "Task<Result>.MatchAsync<TResult> of Success should return the result from success delegate (Func<CancellationToken, Task<TResult>>, Func<Exception, CancellationToken, Task<TResult>>>)")]
    public async Task TaskResultMatchAsyncOfSuccessShouldReturnResultFromSuccessDelegateFuncCancellationTokenTaskResultFuncExceptionCancellationTokenTaskResult()
    {
        // Given
        Result result = Result.Success();
        const string expected = "Success";

        // When
        string actual = await Task.FromResult(result).MatchAsync(SuccessAsync, FailureAsync, token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        async Task<string> SuccessAsync(CancellationToken ct) => await Task.FromResult("Success");
        async Task<string> FailureAsync(Exception exception, CancellationToken ct) => await Task.FromResult("Failure");
    }

    [Fact(DisplayName = "Task<Result>.MatchAsync<TResult> of Failure should return the result from failure delegate (Func<CancellationToken, Task<TResult>>, Func<Exception, CancellationToken, Task<TResult>>>)")]
    public async Task TaskResultMatchAsyncOfFailureShouldReturnResultFromFailureDelegateFuncCancellationTokenTaskResultFuncExceptionCancellationTokenTaskResult()
    {
        // Given
        Result result = Result.Failure(FailureException);
        const string expected = "Failure";

        // When
        string actual = await Task.FromResult(result).MatchAsync(SuccessAsync, FailureAsync, token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        async Task<string> SuccessAsync(CancellationToken ct) => await Task.FromResult("Success");
        async Task<string> FailureAsync(Exception exception, CancellationToken ct) => await Task.FromResult("Failure");
    }

    [Fact(DisplayName = "Task<Result<T>>.MatchAsync of Success should invoke the success delegate (Action<T>, Action<Exception>)")]
    public async Task TaskResultOfTMatchAsyncOfSuccessShouldInvokeSuccessDelegateActionTActionException()
    {
        // Given
        Result<int> result = Result<int>.Success(42);
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await Task.FromResult(result).MatchAsync(Success, Failure, token: TestContext.Current.CancellationToken);

        // Then
        Assert.True(successCalled);
        Assert.False(failureCalled);

        return;
        void Success(int value) => successCalled = value == 42;
        void Failure(Exception exception) => failureCalled = true;
    }

    [Fact(DisplayName = "Task<Result<T>>.MatchAsync of Failure should invoke the failure delegate (Action<T>, Action<Exception>)")]
    public async Task TaskResultOfTMatchAsyncOfFailureShouldInvokeFailureDelegateActionTActionException()
    {
        // Given
        Result<int> result = Result<int>.Failure(FailureException);
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await Task.FromResult(result).MatchAsync(Success, Failure, token: TestContext.Current.CancellationToken);

        // Then
        Assert.False(successCalled);
        Assert.True(failureCalled);

        return;
        void Success(int value) => successCalled = true;
        void Failure(Exception exception) => failureCalled = true;
    }

    [Fact(DisplayName = "Task<Result<T>>.MatchAsync of Success should invoke the success delegate (Func<T, Task>, Action<Exception>)")]
    public async Task TaskResultOfTMatchAsyncOfSuccessShouldInvokeSuccessDelegateFuncTTaskActionException()
    {
        // Given
        Result<int> result = Result<int>.Success(42);
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await Task.FromResult(result).MatchAsync(SuccessAsync, Failure, token: TestContext.Current.CancellationToken);

        // Then
        Assert.True(successCalled);
        Assert.False(failureCalled);

        return;
        async Task SuccessAsync(int value) => await Task.Run(() => successCalled = value == 42);
        void Failure(Exception exception) => failureCalled = true;
    }

    [Fact(DisplayName = "Task<Result<T>>.MatchAsync of Failure should invoke the failure delegate (Func<T, Task>, Action<Exception>)")]
    public async Task TaskResultOfTMatchAsyncOfFailureShouldInvokeFailureDelegateFuncTTaskActionException()
    {
        // Given
        Result<int> result = Result<int>.Failure(FailureException);
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await Task.FromResult(result).MatchAsync(SuccessAsync, Failure, token: TestContext.Current.CancellationToken);

        // Then
        Assert.False(successCalled);
        Assert.True(failureCalled);

        return;
        async Task SuccessAsync(int value) => await Task.Run(() => successCalled = true);
        void Failure(Exception exception) => failureCalled = true;
    }

    [Fact(DisplayName = "Task<Result<T>>.MatchAsync of Success should invoke the success delegate (Func<T, CancellationToken, Task>, Action<Exception>)")]
    public async Task TaskResultOfTMatchAsyncOfSuccessShouldInvokeSuccessDelegateFuncTCancellationTokenTaskActionException()
    {
        // Given
        Result<int> result = Result<int>.Success(42);
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await Task.FromResult(result).MatchAsync(SuccessAsync, Failure, token: TestContext.Current.CancellationToken);

        // Then
        Assert.True(successCalled);
        Assert.False(failureCalled);

        return;
        async Task SuccessAsync(int value, CancellationToken ct) => await Task.Run(() => successCalled = value == 42, ct);
        void Failure(Exception exception) => failureCalled = true;
    }

    [Fact(DisplayName = "Task<Result<T>>.MatchAsync of Failure should invoke the failure delegate (Func<T, CancellationToken, Task>, Action<Exception>)")]
    public async Task TaskResultOfTMatchAsyncOfFailureShouldInvokeFailureDelegateFuncTCancellationTokenTaskActionException()
    {
        // Given
        Result<int> result = Result<int>.Failure(FailureException);
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await Task.FromResult(result).MatchAsync(SuccessAsync, Failure, token: TestContext.Current.CancellationToken);

        // Then
        Assert.False(successCalled);
        Assert.True(failureCalled);

        return;
        async Task SuccessAsync(int value, CancellationToken ct) => await Task.Run(() => successCalled = true, ct);
        void Failure(Exception exception) => failureCalled = true;
    }

    [Fact(DisplayName = "Task<Result<T>>.MatchAsync of Success should invoke the success delegate (Action<T>, Func<Exception, Task>)")]
    public async Task TaskResultOfTMatchAsyncOfSuccessShouldInvokeSuccessDelegateActionTFuncExceptionTask()
    {
        // Given
        Result<int> result = Result<int>.Success(42);
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await Task.FromResult(result).MatchAsync(Success, FailureAsync, token: TestContext.Current.CancellationToken);

        // Then
        Assert.True(successCalled);
        Assert.False(failureCalled);

        return;
        void Success(int value) => successCalled = value == 42;
        async Task FailureAsync(Exception exception) => await Task.Run(() => failureCalled = true);
    }

    [Fact(DisplayName = "Task<Result<T>>.MatchAsync of Failure should invoke the failure delegate (Action<T>, Func<Exception, Task>)")]
    public async Task TaskResultOfTMatchAsyncOfFailureShouldInvokeFailureDelegateActionTFuncExceptionTask()
    {
        // Given
        Result<int> result = Result<int>.Failure(FailureException);
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await Task.FromResult(result).MatchAsync(Success, FailureAsync, token: TestContext.Current.CancellationToken);

        // Then
        Assert.False(successCalled);
        Assert.True(failureCalled);

        return;
        void Success(int value) => successCalled = true;
        async Task FailureAsync(Exception exception) => await Task.Run(() => failureCalled = true);
    }

    [Fact(DisplayName = "Task<Result<T>>.MatchAsync of Success should invoke the success delegate (Action<T>, Func<Exception, CancellationToken, Task>)")]
    public async Task TaskResultOfTMatchAsyncOfSuccessShouldInvokeSuccessDelegateActionTFuncExceptionCancellationTokenTask()
    {
        // Given
        Result<int> result = Result<int>.Success(42);
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await Task.FromResult(result).MatchAsync(Success, FailureAsync, token: TestContext.Current.CancellationToken);

        // Then
        Assert.True(successCalled);
        Assert.False(failureCalled);

        return;
        void Success(int value) => successCalled = value == 42;
        async Task FailureAsync(Exception exception, CancellationToken ct) => await Task.Run(() => failureCalled = true, ct);
    }

    [Fact(DisplayName = "Task<Result<T>>.MatchAsync of Failure should invoke the failure delegate (Action<T>, Func<Exception, CancellationToken, Task>)")]
    public async Task TaskResultOfTMatchAsyncOfFailureShouldInvokeFailureDelegateActionTFuncExceptionCancellationTokenTask()
    {
        // Given
        Result<int> result = Result<int>.Failure(FailureException);
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await Task.FromResult(result).MatchAsync(Success, FailureAsync, token: TestContext.Current.CancellationToken);

        // Then
        Assert.False(successCalled);
        Assert.True(failureCalled);

        return;
        void Success(int value) => successCalled = true;
        async Task FailureAsync(Exception exception, CancellationToken ct) => await Task.Run(() => failureCalled = true, ct);
    }

    [Fact(DisplayName = "Task<Result<T>>.MatchAsync of Success should invoke the success delegate (Func<T, Task>, Func<Exception, Task>)")]
    public async Task TaskResultOfTMatchAsyncOfSuccessShouldInvokeSuccessDelegateFuncTTaskFuncExceptionTask()
    {
        // Given
        Result<int> result = Result<int>.Success(42);
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await Task.FromResult(result).MatchAsync(SuccessAsync, FailureAsync, token: TestContext.Current.CancellationToken);

        // Then
        Assert.True(successCalled);
        Assert.False(failureCalled);

        return;
        async Task SuccessAsync(int value) => await Task.Run(() => successCalled = value == 42);
        async Task FailureAsync(Exception exception) => await Task.Run(() => failureCalled = true);
    }

    [Fact(DisplayName = "Task<Result<T>>.MatchAsync of Failure should invoke the failure delegate (Func<T, Task>, Func<Exception, Task>)")]
    public async Task TaskResultOfTMatchAsyncOfFailureShouldInvokeFailureDelegateFuncTTaskFuncExceptionTask()
    {
        // Given
        Result<int> result = Result<int>.Failure(FailureException);
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await Task.FromResult(result).MatchAsync(SuccessAsync, FailureAsync, token: TestContext.Current.CancellationToken);

        // Then
        Assert.False(successCalled);
        Assert.True(failureCalled);

        return;
        async Task SuccessAsync(int value) => await Task.Run(() => successCalled = true);
        async Task FailureAsync(Exception exception) => await Task.Run(() => failureCalled = true);
    }

    [Fact(DisplayName = "Task<Result<T>>.MatchAsync of Success should invoke the success delegate (Func<T, CancellationToken, Task>, Func<Exception, CancellationToken, Task>)")]
    public async Task TaskResultOfTMatchAsyncOfSuccessShouldInvokeSuccessDelegateFuncTCancellationTokenTaskFuncExceptionCancellationTokenTask()
    {
        // Given
        Result<int> result = Result<int>.Success(42);
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await Task.FromResult(result).MatchAsync(SuccessAsync, FailureAsync, token: TestContext.Current.CancellationToken);

        // Then
        Assert.True(successCalled);
        Assert.False(failureCalled);

        return;
        async Task SuccessAsync(int value, CancellationToken ct) => await Task.Run(() => successCalled = value == 42, ct);
        async Task FailureAsync(Exception exception, CancellationToken ct) => await Task.Run(() => failureCalled = true, ct);
    }

    [Fact(DisplayName = "Task<Result<T>>.MatchAsync of Failure should invoke the failure delegate (Func<T, CancellationToken, Task>, Func<Exception, CancellationToken, Task>)")]
    public async Task TaskResultOfTMatchAsyncOfFailureShouldInvokeFailureDelegateFuncTCancellationTokenTaskFuncExceptionCancellationTokenTask()
    {
        // Given
        Result<int> result = Result<int>.Failure(FailureException);
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await Task.FromResult(result).MatchAsync(SuccessAsync, FailureAsync, token: TestContext.Current.CancellationToken);

        // Then
        Assert.False(successCalled);
        Assert.True(failureCalled);

        return;
        async Task SuccessAsync(int value, CancellationToken ct) => await Task.Run(() => successCalled = true, ct);
        async Task FailureAsync(Exception exception, CancellationToken ct) => await Task.Run(() => failureCalled = true, ct);
    }

    [Fact(DisplayName = "Task<Result<T>>.MatchAsync<TResult> of Success should return result from success delegate (Func<T, TResult>, Func<Exception, TResult>)")]
    public async Task TaskResultOfTMatchAsyncOfSuccessShouldReturnResultFromSuccessDelegateFuncTResultFuncExceptionResult()
    {
        // Given
        Result<int> result = Result<int>.Success(42);
        const string expected = "Success";

        // When
        string actual = await Task.FromResult(result).MatchAsync(Success, Failure, token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        string Success(int value) => value == 42 ? "Success" : "Failure";
        string Failure(Exception exception) => "Failure";
    }

    [Fact(DisplayName = "Task<Result<T>>.MatchAsync<TResult> of Failure should return result from failure delegate (Func<T, TResult>, Func<Exception, TResult>)")]
    public async Task TaskResultOfTMatchAsyncOfFailureShouldReturnResultFromFailureDelegateFuncTResultFuncExceptionResult()
    {
        // Given
        Result<int> result = Result<int>.Failure(FailureException);
        const string expected = "Failure";

        // When
        string actual = await Task.FromResult(result).MatchAsync(Success, Failure, token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        string Success(int value) => "Success";
        string Failure(Exception exception) => "Failure";
    }

    [Fact(DisplayName = "Task<Result<T>>.MatchAsync<TResult> of Success should return result from success delegate (Func<T, Task<TResult>>, Func<Exception, TResult>)")]
    public async Task TaskResultOfTMatchAsyncOfSuccessShouldReturnResultFromSuccessDelegateFuncTTaskResultFuncExceptionResult()
    {
        // Given
        Result<int> result = Result<int>.Success(42);
        const string expected = "Success";

        // When
        string actual = await Task.FromResult(result).MatchAsync(SuccessAsync, Failure, token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        async Task<string> SuccessAsync(int value) => await Task.FromResult(value == 42 ? "Success" : "Failure");
        string Failure(Exception exception) => "Failure";
    }

    [Fact(DisplayName = "Task<Result<T>>.MatchAsync<TResult> of Failure should return result from failure delegate (Func<T, Task<TResult>>, Func<Exception, TResult>)")]
    public async Task TaskResultOfTMatchAsyncOfFailureShouldReturnResultFromFailureDelegateFuncTTaskResultFuncExceptionResult()
    {
        // Given
        Result<int> result = Result<int>.Failure(FailureException);
        const string expected = "Failure";

        // When
        string actual = await Task.FromResult(result).MatchAsync(SuccessAsync, Failure, token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        async Task<string> SuccessAsync(int value) => await Task.FromResult("Success");
        string Failure(Exception exception) => "Failure";
    }

    [Fact(DisplayName = "Task<Result<T>>.MatchAsync<TResult> of Success should return result from success delegate (Func<T, CancellationToken, Task<TResult>>, Func<Exception, TResult>)")]
    public async Task TaskResultOfTMatchAsyncOfSuccessShouldReturnResultFromSuccessDelegateFuncTCancellationTokenTaskResultFuncExceptionResult()
    {
        // Given
        Result<int> result = Result<int>.Success(42);
        const string expected = "Success";

        // When
        string actual = await Task.FromResult(result).MatchAsync(SuccessAsync, Failure, token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        async Task<string> SuccessAsync(int value, CancellationToken ct) => await Task.FromResult(value == 42 ? "Success" : "Failure");
        string Failure(Exception exception) => "Failure";
    }

    [Fact(DisplayName = "Task<Result<T>>.MatchAsync<TResult> of Failure should return result from failure delegate (Func<T, CancellationToken, Task<TResult>>, Func<Exception, TResult>)")]
    public async Task TaskResultOfTMatchAsyncOfFailureShouldReturnResultFromFailureDelegateFuncTCancellationTokenTaskResultFuncExceptionResult()
    {
        // Given
        Result<int> result = Result<int>.Failure(FailureException);
        const string expected = "Failure";

        // When
        string actual = await Task.FromResult(result).MatchAsync(SuccessAsync, Failure, token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        async Task<string> SuccessAsync(int value, CancellationToken ct) => await Task.FromResult("Success");
        string Failure(Exception exception) => "Failure";
    }

    [Fact(DisplayName = "Task<Result<T>>.MatchAsync<TResult> of Success should return result from success delegate (Func<T, TResult>, Func<Exception, Task<TResult>>>)")]
    public async Task TaskResultOfTMatchAsyncOfSuccessShouldReturnResultFromSuccessDelegateFuncTResultFuncExceptionTaskResult()
    {
        // Given
        Result<int> result = Result<int>.Success(42);
        const string expected = "Success";

        // When
        string actual = await Task.FromResult(result).MatchAsync(Success, FailureAsync, token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        string Success(int value) => value == 42 ? "Success" : "Failure";
        async Task<string> FailureAsync(Exception exception) => await Task.FromResult("Failure");
    }

    [Fact(DisplayName = "Task<Result<T>>.MatchAsync<TResult> of Failure should return result from failure delegate (Func<T, TResult>, Func<Exception, Task<TResult>>>)")]
    public async Task TaskResultOfTMatchAsyncOfFailureShouldReturnResultFromFailureDelegateFuncTResultFuncExceptionTaskResult()
    {
        // Given
        Result<int> result = Result<int>.Failure(FailureException);
        const string expected = "Failure";

        // When
        string actual = await Task.FromResult(result).MatchAsync(Success, FailureAsync, token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        string Success(int value) => "Success";
        async Task<string> FailureAsync(Exception exception) => await Task.FromResult("Failure");
    }

    [Fact(DisplayName = "Task<Result<T>>.MatchAsync<TResult> of Success should return result from success delegate (Func<T, TResult>, Func<Exception, CancellationToken, Task<TResult>>>)")]
    public async Task TaskResultOfTMatchAsyncOfSuccessShouldReturnResultFromSuccessDelegateFuncTResultFuncExceptionCancellationTokenTaskResult()
    {
        // Given
        Result<int> result = Result<int>.Success(42);
        const string expected = "Success";

        // When
        string actual = await Task.FromResult(result).MatchAsync(Success, FailureAsync, token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        string Success(int value) => value == 42 ? "Success" : "Failure";
        async Task<string> FailureAsync(Exception exception, CancellationToken ct) => await Task.FromResult("Failure");
    }

    [Fact(DisplayName = "Task<Result<T>>.MatchAsync<TResult> of Failure should return result from failure delegate (Func<T, TResult>, Func<Exception, CancellationToken, Task<TResult>>>)")]
    public async Task TaskResultOfTMatchAsyncOfFailureShouldReturnResultFromFailureDelegateFuncTResultFuncExceptionCancellationTokenTaskResult()
    {
        // Given
        Result<int> result = Result<int>.Failure(FailureException);
        const string expected = "Failure";

        // When
        string actual = await Task.FromResult(result).MatchAsync(Success, FailureAsync, token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        string Success(int value) => "Success";
        async Task<string> FailureAsync(Exception exception, CancellationToken ct) => await Task.FromResult("Failure");
    }

    [Fact(DisplayName = "Task<Result<T>>.MatchAsync<TResult> of Success should return result from success delegate (Func<T, Task<TResult>>, Func<Exception, Task<TResult>>>)")]
    public async Task TaskResultOfTMatchAsyncOfSuccessShouldReturnResultFromSuccessDelegateFuncTTaskResultFuncExceptionTaskResult()
    {
        // Given
        Result<int> result = Result<int>.Success(42);
        const string expected = "Success";

        // When
        string actual = await Task.FromResult(result).MatchAsync(SuccessAsync, FailureAsync, token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        async Task<string> SuccessAsync(int value) => await Task.FromResult(value == 42 ? "Success" : "Failure");
        async Task<string> FailureAsync(Exception exception) => await Task.FromResult("Failure");
    }

    [Fact(DisplayName = "Task<Result<T>>.MatchAsync<TResult> of Failure should return result from failure delegate (Func<T, Task<TResult>>, Func<Exception, Task<TResult>>>)")]
    public async Task TaskResultOfTMatchAsyncOfFailureShouldReturnResultFromFailureDelegateFuncTTaskResultFuncExceptionTaskResult()
    {
        // Given
        Result<int> result = Result<int>.Failure(FailureException);
        const string expected = "Failure";

        // When
        string actual = await Task.FromResult(result).MatchAsync(SuccessAsync, FailureAsync, token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        async Task<string> SuccessAsync(int value) => await Task.FromResult("Success");
        async Task<string> FailureAsync(Exception exception) => await Task.FromResult("Failure");
    }

    [Fact(DisplayName = "Task<Result<T>>.MatchAsync<TResult> of Success should return result from success delegate (Func<T, CancellationToken, Task<TResult>>, Func<Exception, CancellationToken, Task<TResult>>>)")]
    public async Task TaskResultOfTMatchAsyncOfSuccessShouldReturnResultFromSuccessDelegateFuncTCancellationTokenTaskResultFuncExceptionCancellationTokenTaskResult()
    {
        // Given
        Result<int> result = Result<int>.Success(42);
        const string expected = "Success";

        // When
        string actual = await Task.FromResult(result).MatchAsync(SuccessAsync, FailureAsync, token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        async Task<string> SuccessAsync(int value, CancellationToken ct) => await Task.FromResult(value == 42 ? "Success" : "Failure");
        async Task<string> FailureAsync(Exception exception, CancellationToken ct) => await Task.FromResult("Failure");
    }

    [Fact(DisplayName = "Task<Result<T>>.MatchAsync<TResult> of Failure should return result from failure delegate (Func<T, CancellationToken, Task<TResult>>, Func<Exception, CancellationToken, Task<TResult>>>)")]
    public async Task TaskResultOfTMatchAsyncOfFailureShouldReturnResultFromFailureDelegateFuncTCancellationTokenTaskResultFuncExceptionCancellationTokenTaskResult()
    {
        // Given
        Result<int> result = Result<int>.Failure(FailureException);
        const string expected = "Failure";

        // When
        string actual = await Task.FromResult(result).MatchAsync(SuccessAsync, FailureAsync, token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        async Task<string> SuccessAsync(int value, CancellationToken ct) => await Task.FromResult("Success");
        async Task<string> FailureAsync(Exception exception, CancellationToken ct) => await Task.FromResult("Failure");
    }

    [Fact(DisplayName = "Task<Result>.SelectAsync of Success should invoke the selector delegate (Action)")]
    public async Task TaskResultSelectAsyncOfSuccessShouldInvokeSelectorDelegateAction()
    {
        // Given
        Result result = Result.Success();
        bool selectorInvoked = false;

        // When
        Result actual = await Task.FromResult(result).SelectAsync(Selector, token: TestContext.Current.CancellationToken);

        // Then
        Assert.IsType<Success>(actual);
        Assert.True(selectorInvoked);

        return;
        void Selector() => selectorInvoked = true;
    }

    [Fact(DisplayName = "Task<Result>.SelectAsync of Failure should not invoke the selector delegate (Action)")]
    public async Task TaskResultSelectAsyncOfFailureShouldNotInvokeSelectorDelegateAction()
    {
        // Given
        Result result = Result.Failure(FailureException);
        bool selectorInvoked = false;

        // When
        Result actual = await Task.FromResult(result).SelectAsync(Selector, token: TestContext.Current.CancellationToken);

        // Then
        Assert.IsType<Failure>(actual);
        Assert.False(selectorInvoked);

        return;
        void Selector() => selectorInvoked = true;
    }

    [Fact(DisplayName = "Task<Result>.SelectAsync of Success should invoke the selector delegate (Func<Task>)")]
    public async Task TaskResultSelectAsyncOfSuccessShouldInvokeSelectorDelegateFuncTask()
    {
        // Given
        Result result = Result.Success();
        bool selectorInvoked = false;

        // When
        Result actual = await Task.FromResult(result).SelectAsync(SelectorAsync, token: TestContext.Current.CancellationToken);

        // Then
        Assert.IsType<Success>(actual);
        Assert.True(selectorInvoked);

        return;
        async Task SelectorAsync() => await Task.Run(() => selectorInvoked = true);
    }

    [Fact(DisplayName = "Task<Result>.SelectAsync of Failure should not invoke the selector delegate (Func<Task>)")]
    public async Task TaskResultSelectAsyncOfFailureShouldNotInvokeSelectorDelegateFuncTask()
    {
        // Given
        Result result = Result.Failure(FailureException);
        bool selectorInvoked = false;

        // When
        Result actual = await Task.FromResult(result).SelectAsync(SelectorAsync, token: TestContext.Current.CancellationToken);

        // Then
        Assert.IsType<Failure>(actual);
        Assert.False(selectorInvoked);

        return;
        async Task SelectorAsync() => await Task.Run(() => selectorInvoked = true);
    }

    [Fact(DisplayName = "Task<Result>.SelectAsync of Success should invoke the selector delegate (Func<CancellationToken, Task>)")]
    public async Task TaskResultSelectAsyncOfSuccessShouldInvokeSelectorDelegateFuncCancellationTokenTask()
    {
        // Given
        Result result = Result.Success();
        bool selectorInvoked = false;

        // When
        Result actual = await Task.FromResult(result).SelectAsync(SelectorAsync, token: TestContext.Current.CancellationToken);

        // Then
        Assert.IsType<Success>(actual);
        Assert.True(selectorInvoked);

        return;
        async Task SelectorAsync(CancellationToken ct) => await Task.Run(() => selectorInvoked = true, ct);
    }

    [Fact(DisplayName = "Task<Result>.SelectAsync of Failure should not invoke the selector delegate (Func<CancellationToken, Task>)")]
    public async Task TaskResultSelectAsyncOfFailureShouldNotInvokeSelectorDelegateFuncCancellationTokenTask()
    {
        // Given
        Result result = Result.Failure(FailureException);
        bool selectorInvoked = false;

        // When
        Result actual = await Task.FromResult(result).SelectAsync(SelectorAsync, token: TestContext.Current.CancellationToken);

        // Then
        Assert.IsType<Failure>(actual);
        Assert.False(selectorInvoked);

        return;
        async Task SelectorAsync(CancellationToken ct) => await Task.Run(() => selectorInvoked = true, ct);
    }

    [Fact(DisplayName = "Task<Result>.SelectAsync<TResult> of Success should invoke the selector delegate (Func<TResult>)")]
    public async Task TaskResultSelectAsyncOfSuccessShouldInvokeSelectorDelegateFuncTResult()
    {
        // Given
        Result result = Result.Success();
        bool selectorInvoked = false;

        // When
        Result<string> actual = await Task.FromResult(result).SelectAsync(Selector, token: TestContext.Current.CancellationToken);

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

    [Fact(DisplayName = "Task<Result>.SelectAsync<TResult> of Failure should not invoke the selector delegate (Func<TResult>)")]
    public async Task TaskResultSelectAsyncOfFailureShouldNotInvokeSelectorDelegateFuncTResult()
    {
        // Given
        Result result = Result.Failure(FailureException);
        bool selectorInvoked = false;

        // When
        Result<string> actual = await Task.FromResult(result).SelectAsync(Selector, token: TestContext.Current.CancellationToken);

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

    [Fact(DisplayName = "Task<Result>.SelectAsync<TResult> of Success should invoke the selector delegate (Func<Task<TResult>>)")]
    public async Task TaskResultSelectAsyncOfSuccessShouldInvokeSelectorDelegateFuncTaskTResult()
    {
        // Given
        Result result = Result.Success();
        bool selectorInvoked = false;

        // When
        Result<string> actual = await Task.FromResult(result).SelectAsync(SelectorAsync, token: TestContext.Current.CancellationToken);

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

    [Fact(DisplayName = "Task<Result>.SelectAsync<TResult> of Failure should not invoke the selector delegate (Func<Task<TResult>>)")]
    public async Task TaskResultSelectAsyncOfFailureShouldNotInvokeSelectorDelegateFuncTaskTResult()
    {
        // Given
        Result result = Result.Failure(FailureException);
        bool selectorInvoked = false;

        // When
        Result<string> actual = await Task.FromResult(result).SelectAsync(SelectorAsync, token: TestContext.Current.CancellationToken);

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

    [Fact(DisplayName = "Task<Result>.SelectAsync<TResult> of Success should invoke the selector delegate (Func<CancellationToken, Task<TResult>>)")]
    public async Task TaskResultSelectAsyncOfSuccessShouldInvokeSelectorDelegateFuncCancellationTokenTaskTResult()
    {
        // Given
        Result result = Result.Success();
        bool selectorInvoked = false;

        // When
        Result<string> actual = await Task.FromResult(result).SelectAsync(SelectorAsync, token: TestContext.Current.CancellationToken);

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

    [Fact(DisplayName = "Task<Result>.SelectAsync<TResult> of Failure should not invoke the selector delegate (Func<CancellationToken, Task<TResult>>)")]
    public async Task TaskResultSelectAsyncOfFailureShouldNotInvokeSelectorDelegateFuncCancellationTokenTaskTResult()
    {
        // Given
        Result result = Result.Failure(FailureException);
        bool selectorInvoked = false;

        // When
        Result<string> actual = await Task.FromResult(result).SelectAsync(SelectorAsync, token: TestContext.Current.CancellationToken);

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

    [Fact(DisplayName = "Task<Result<T>>.SelectAsync of Success should invoke the selector delegate (Action<T>)")]
    public async Task TaskResultOfTSelectAsyncOfSuccessShouldInvokeSelectorDelegateActionT()
    {
        // Given
        Result<int> result = Result<int>.Success(42);
        bool selectorInvoked = false;

        // When
        Result actual = await Task.FromResult(result).SelectAsync(Selector, token: TestContext.Current.CancellationToken);

        // Then
        Assert.IsType<Success>(actual);
        Assert.True(selectorInvoked);

        return;

        void Selector(int value)
        {
            selectorInvoked = value == 42;
        }
    }

    [Fact(DisplayName = "Task<Result<T>>.SelectAsync of Failure should not invoke the selector delegate (Action<T>)")]
    public async Task TaskResultOfTSelectAsyncOfFailureShouldNotInvokeSelectorDelegateActionT()
    {
        // Given
        Result<int> result = Result<int>.Failure(FailureException);
        bool selectorInvoked = false;

        // When
        Result actual = await Task.FromResult(result).SelectAsync(Selector, token: TestContext.Current.CancellationToken);

        // Then
        Assert.IsType<Failure>(actual);
        Assert.False(selectorInvoked);

        return;

        void Selector(int value)
        {
            selectorInvoked = true;
        }
    }

    [Fact(DisplayName = "Task<Result<T>>.SelectAsync of Success should invoke the selector delegate (Func<T, Task>)")]
    public async Task TaskResultOfTSelectAsyncOfSuccessShouldInvokeSelectorDelegateFuncTTask()
    {
        // Given
        Result<int> result = Result<int>.Success(42);
        bool selectorInvoked = false;

        // When
        Result actual = await Task.FromResult(result).SelectAsync(SelectorAsync, token: TestContext.Current.CancellationToken);

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

    [Fact(DisplayName = "Task<Result<T>>.SelectAsync of Failure should not invoke the selector delegate (Func<T, Task>)")]
    public async Task TaskResultOfTSelectAsyncOfFailureShouldNotInvokeSelectorDelegateFuncTTask()
    {
        // Given
        Result<int> result = Result<int>.Failure(FailureException);
        bool selectorInvoked = false;

        // When
        Result actual = await Task.FromResult(result).SelectAsync(SelectorAsync, token: TestContext.Current.CancellationToken);

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

    [Fact(DisplayName = "Task<Result<T>>.SelectAsync of Success should invoke the selector delegate (Func<T, CancellationToken, Task>)")]
    public async Task TaskResultOfTSelectAsyncOfSuccessShouldInvokeSelectorDelegateFuncTCancellationTokenTask()
    {
        // Given
        Result<int> result = Result<int>.Success(42);
        bool selectorInvoked = false;

        // When
        Result actual = await Task.FromResult(result).SelectAsync(SelectorAsync, token: TestContext.Current.CancellationToken);

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

    [Fact(DisplayName = "Task<Result<T>>.SelectAsync of Failure should not invoke the selector delegate (Func<T, CancellationToken, Task>)")]
    public async Task TaskResultOfTSelectAsyncOfFailureShouldNotInvokeSelectorDelegateFuncTCancellationTokenTask()
    {
        // Given
        Result<int> result = Result<int>.Failure(FailureException);
        bool selectorInvoked = false;

        // When
        Result actual = await Task.FromResult(result).SelectAsync(SelectorAsync, token: TestContext.Current.CancellationToken);

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

    [Fact(DisplayName = "Task<Result<T>>.SelectAsync<TResult> of Success should invoke the selector delegate (Func<T, TResult>)")]
    public async Task TaskResultOfTSelectAsyncOfSuccessShouldInvokeSelectorDelegateFuncTResult()
    {
        // Given
        Result<int> result = Result<int>.Success(42);
        bool selectorInvoked = false;

        // When
        Result<string> actual = await Task.FromResult(result).SelectAsync(Selector, token: TestContext.Current.CancellationToken);

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

    [Fact(DisplayName = "Task<Result<T>>.SelectAsync<TResult> of Failure should not invoke the selector delegate (Func<T, TResult>)")]
    public async Task TaskResultOfTSelectAsyncOfFailureShouldNotInvokeSelectorDelegateFuncTResult()
    {
        // Given
        Result<int> result = Result<int>.Failure(FailureException);
        bool selectorInvoked = false;

        // When
        Result<string> actual = await Task.FromResult(result).SelectAsync(Selector, token: TestContext.Current.CancellationToken);

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

    [Fact(DisplayName = "Task<Result<T>>.SelectAsync<TResult> of Success should invoke the selector delegate (Func<T, Task<TResult>>)")]
    public async Task TaskResultOfTSelectAsyncOfSuccessShouldInvokeSelectorDelegateFuncTTaskResult()
    {
        // Given
        Result<int> result = Result<int>.Success(42);
        bool selectorInvoked = false;

        // When
        Result<string> actual = await Task.FromResult(result).SelectAsync(SelectorAsync, token: TestContext.Current.CancellationToken);

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

    [Fact(DisplayName = "Task<Result<T>>.SelectAsync<TResult> of Failure should not invoke the selector delegate (Func<T, Task<TResult>>)")]
    public async Task TaskResultOfTSelectAsyncOfFailureShouldNotInvokeSelectorDelegateFuncTTaskResult()
    {
        // Given
        Result<int> result = Result<int>.Failure(FailureException);
        bool selectorInvoked = false;

        // When
        Result<string> actual = await Task.FromResult(result).SelectAsync(SelectorAsync, token: TestContext.Current.CancellationToken);

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

    [Fact(DisplayName = "Task<Result<T>>.SelectAsync<TResult> of Success should invoke the selector delegate (Func<T, CancellationToken, Task<TResult>>)")]
    public async Task TaskResultOfTSelectAsyncOfSuccessShouldInvokeSelectorDelegateFuncTCancellationTokenTaskResult()
    {
        // Given
        Result<int> result = Result<int>.Success(42);
        bool selectorInvoked = false;

        // When
        Result<string> actual = await Task.FromResult(result).SelectAsync(SelectorAsync, token: TestContext.Current.CancellationToken);

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

    [Fact(DisplayName = "Task<Result<T>>.SelectAsync<TResult> of Failure should not invoke the selector delegate (Func<T, CancellationToken, Task<TResult>>)")]
    public async Task TaskResultOfTSelectAsyncOfFailureShouldNotInvokeSelectorDelegateFuncTCancellationTokenTaskResult()
    {
        // Given
        Result<int> result = Result<int>.Failure(FailureException);
        bool selectorInvoked = false;

        // When
        Result<string> actual = await Task.FromResult(result).SelectAsync(SelectorAsync, token: TestContext.Current.CancellationToken);

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

    [Fact(DisplayName = "Task<Result>.SelectManyAsync of Success should invoke the selector delegate (Func<Result>) and return its result")]
    public async Task TaskResultSelectManyAsyncOfSuccessShouldInvokeSelectorDelegateFuncResult()
    {
        // Given
        Result result = Result.Success();
        bool selectorInvoked = false;

        // When
        Result actual = await Task.FromResult(result).SelectManyAsync(Selector, token: TestContext.Current.CancellationToken);

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

    [Fact(DisplayName = "Task<Result>.SelectManyAsync of Failure should not invoke the selector delegate (Func<Result>)")]
    public async Task TaskResultSelectManyAsyncOfFailureShouldNotInvokeSelectorDelegateFuncResult()
    {
        // Given
        Result result = Result.Failure(FailureException);
        bool selectorInvoked = false;

        // When
        Result actual = await Task.FromResult(result).SelectManyAsync(Selector, token: TestContext.Current.CancellationToken);

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

    [Fact(DisplayName = "Task<Result>.SelectManyAsync of Success should invoke the selector delegate (Func<Task<Result>>) and return its result")]
    public async Task TaskResultSelectManyAsyncOfSuccessShouldInvokeSelectorDelegateFuncTaskResult()
    {
        // Given
        Result result = Result.Success();
        bool selectorInvoked = false;

        // When
        Result actual = await Task.FromResult(result).SelectManyAsync(SelectorAsync, token: TestContext.Current.CancellationToken);

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

    [Fact(DisplayName = "Task<Result>.SelectManyAsync of Failure should not invoke the selector delegate (Func<Task<Result>>)")]
    public async Task TaskResultSelectManyAsyncOfFailureShouldNotInvokeSelectorDelegateFuncTaskResult()
    {
        // Given
        Result result = Result.Failure(FailureException);
        bool selectorInvoked = false;

        // When
        Result actual = await Task.FromResult(result).SelectManyAsync(SelectorAsync, token: TestContext.Current.CancellationToken);

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

    [Fact(DisplayName = "Task<Result>.SelectManyAsync of Success should invoke the selector delegate (Func<CancellationToken, Task<Result>>) and return its result")]
    public async Task TaskResultSelectManyAsyncOfSuccessShouldInvokeSelectorDelegateFuncCancellationTokenTaskResult()
    {
        // Given
        Result result = Result.Success();
        bool selectorInvoked = false;

        // When
        Result actual = await Task.FromResult(result).SelectManyAsync(SelectorAsync, token: TestContext.Current.CancellationToken);

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

    [Fact(DisplayName = "Task<Result>.SelectManyAsync of Failure should not invoke the selector delegate (Func<CancellationToken, Task<Result>>)")]
    public async Task TaskResultSelectManyAsyncOfFailureShouldNotInvokeSelectorDelegateFuncCancellationTokenTaskResult()
    {
        // Given
        Result result = Result.Failure(FailureException);
        bool selectorInvoked = false;

        // When
        Result actual = await Task.FromResult(result).SelectManyAsync(SelectorAsync, token: TestContext.Current.CancellationToken);

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

    [Fact(DisplayName = "Task<Result>.SelectManyAsync<TResult> of Success should invoke the selector delegate (Func<Result<TResult>>) and return its result")]
    public async Task TaskResultSelectManyAsyncOfSuccessShouldInvokeSelectorDelegateFuncResultTResult()
    {
        // Given
        Result result = Result.Success();
        bool selectorInvoked = false;

        // When
        Result<string> actual = await Task.FromResult(result).SelectManyAsync(Selector, token: TestContext.Current.CancellationToken);

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

    [Fact(DisplayName = "Task<Result>.SelectManyAsync<TResult> of Failure should not invoke the selector delegate (Func<Result<TResult>>)")]
    public async Task TaskResultSelectManyAsyncOfFailureShouldNotInvokeSelectorDelegateFuncResultTResult()
    {
        // Given
        Result result = Result.Failure(FailureException);
        bool selectorInvoked = false;

        // When
        Result<string> actual = await Task.FromResult(result).SelectManyAsync(Selector, token: TestContext.Current.CancellationToken);

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

    [Fact(DisplayName = "Task<Result>.SelectManyAsync<TResult> of Success should invoke the selector delegate (Func<Task<Result<TResult>>>) and return its result")]
    public async Task TaskResultSelectManyAsyncOfSuccessShouldInvokeSelectorDelegateFuncTaskResultTResult()
    {
        // Given
        Result result = Result.Success();
        bool selectorInvoked = false;

        // When
        Result<string> actual = await Task.FromResult(result).SelectManyAsync(SelectorAsync, token: TestContext.Current.CancellationToken);

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

    [Fact(DisplayName = "Task<Result>.SelectManyAsync<TResult> of Failure should not invoke the selector delegate (Func<Task<Result<TResult>>>)")]
    public async Task TaskResultSelectManyAsyncOfFailureShouldNotInvokeSelectorDelegateFuncTaskResultTResult()
    {
        // Given
        Result result = Result.Failure(FailureException);
        bool selectorInvoked = false;

        // When
        Result<string> actual = await Task.FromResult(result).SelectManyAsync(SelectorAsync, token: TestContext.Current.CancellationToken);

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

    [Fact(DisplayName = "Task<Result>.SelectManyAsync<TResult> of Success should invoke the selector delegate (Func<CancellationToken, Task<Result<TResult>>>) and return its result")]
    public async Task TaskResultSelectManyAsyncOfSuccessShouldInvokeSelectorDelegateFuncCancellationTokenTaskResultTResult()
    {
        // Given
        Result result = Result.Success();
        bool selectorInvoked = false;

        // When
        Result<string> actual = await Task.FromResult(result).SelectManyAsync(SelectorAsync, token: TestContext.Current.CancellationToken);

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

    [Fact(DisplayName = "Task<Result>.SelectManyAsync<TResult> of Failure should not invoke the selector delegate (Func<CancellationToken, Task<Result<TResult>>>)")]
    public async Task TaskResultSelectManyAsyncOfFailureShouldNotInvokeSelectorDelegateFuncCancellationTokenTaskResultTResult()
    {
        // Given
        Result result = Result.Failure(FailureException);
        bool selectorInvoked = false;

        // When
        Result<string> actual = await Task.FromResult(result).SelectManyAsync(SelectorAsync, token: TestContext.Current.CancellationToken);

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

    [Fact(DisplayName = "Task<Result<T>>.SelectManyAsync of Success should invoke the selector delegate (Func<T, Result>) and return its result")]
    public async Task TaskResultOfTSelectManyAsyncOfSuccessShouldInvokeSelectorDelegateFuncTResult()
    {
        // Given
        Result<int> result = Result<int>.Success(42);
        bool selectorInvoked = false;

        // When
        Result actual = await Task.FromResult(result).SelectManyAsync(Selector, token: TestContext.Current.CancellationToken);

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

    [Fact(DisplayName = "Task<Result<T>>.SelectManyAsync of Failure should not invoke the selector delegate (Func<T, Result>)")]
    public async Task TaskResultOfTSelectManyAsyncOfFailureShouldNotInvokeSelectorDelegateFuncTResult()
    {
        // Given
        Result<int> result = Result<int>.Failure(FailureException);
        bool selectorInvoked = false;

        // When
        Result actual = await Task.FromResult(result).SelectManyAsync(Selector, token: TestContext.Current.CancellationToken);

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

    [Fact(DisplayName = "Task<Result<T>>.SelectManyAsync of Success should invoke the selector delegate (Func<T, Task<Result>>) and return its result")]
    public async Task TaskResultOfTSelectManyAsyncOfSuccessShouldInvokeSelectorDelegateFuncTTaskResult()
    {
        // Given
        Result<int> result = Result<int>.Success(42);
        bool selectorInvoked = false;

        // When
        Result actual = await Task.FromResult(result).SelectManyAsync(SelectorAsync, token: TestContext.Current.CancellationToken);

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

    [Fact(DisplayName = "Task<Result<T>>.SelectManyAsync of Failure should not invoke the selector delegate (Func<T, Task<Result>>)")]
    public async Task TaskResultOfTSelectManyAsyncOfFailureShouldNotInvokeSelectorDelegateFuncTTaskResult()
    {
        // Given
        Result<int> result = Result<int>.Failure(FailureException);
        bool selectorInvoked = false;

        // When
        Result actual = await Task.FromResult(result).SelectManyAsync(SelectorAsync, token: TestContext.Current.CancellationToken);

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

    [Fact(DisplayName = "Task<Result<T>>.SelectManyAsync of Success should invoke the selector delegate (Func<T, CancellationToken, Task<Result>>) and return its result")]
    public async Task TaskResultOfTSelectManyAsyncOfSuccessShouldInvokeSelectorDelegateFuncTCancellationTokenTaskResult()
    {
        // Given
        Result<int> result = Result<int>.Success(42);
        bool selectorInvoked = false;

        // When
        Result actual = await Task.FromResult(result).SelectManyAsync(SelectorAsync, token: TestContext.Current.CancellationToken);

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

    [Fact(DisplayName = "Task<Result<T>>.SelectManyAsync of Failure should not invoke the selector delegate (Func<T, CancellationToken, Task<Result>>)")]
    public async Task TaskResultOfTSelectManyAsyncOfFailureShouldNotInvokeSelectorDelegateFuncTCancellationTokenTaskResult()
    {
        // Given
        Result<int> result = Result<int>.Failure(FailureException);
        bool selectorInvoked = false;

        // When
        Result actual = await Task.FromResult(result).SelectManyAsync(SelectorAsync, token: TestContext.Current.CancellationToken);

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

    [Fact(DisplayName = "Task<Result<T>>.SelectManyAsync<TResult> of Success should invoke the selector delegate (Func<T, Result<TResult>>) and return its result")]
    public async Task TaskResultOfTSelectManyAsyncOfSuccessShouldInvokeSelectorDelegateFuncTResultTResult()
    {
        // Given
        Result<int> result = Result<int>.Success(42);
        bool selectorInvoked = false;

        // When
        Result<string> actual = await Task.FromResult(result).SelectManyAsync(Selector, token: TestContext.Current.CancellationToken);

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

    [Fact(DisplayName = "Task<Result<T>>.SelectManyAsync<TResult> of Failure should not invoke the selector delegate (Func<T, Result<TResult>>)")]
    public async Task TaskResultOfTSelectManyAsyncOfFailureShouldNotInvokeSelectorDelegateFuncTResultTResult()
    {
        // Given
        Result<int> result = Result<int>.Failure(FailureException);
        bool selectorInvoked = false;

        // When
        Result<string> actual = await Task.FromResult(result).SelectManyAsync(Selector, token: TestContext.Current.CancellationToken);

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

    [Fact(DisplayName = "Task<Result<T>>.SelectManyAsync<TResult> of Success should invoke the selector delegate (Func<T, Task<Result<TResult>>>) and return its result")]
    public async Task TaskResultOfTSelectManyAsyncOfSuccessShouldInvokeSelectorDelegateFuncTTaskResultTResult()
    {
        // Given
        Result<int> result = Result<int>.Success(42);
        bool selectorInvoked = false;

        // When
        Result<string> actual = await Task.FromResult(result).SelectManyAsync(SelectorAsync, token: TestContext.Current.CancellationToken);

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

    [Fact(DisplayName = "Task<Result<T>>.SelectManyAsync<TResult> of Failure should not invoke the selector delegate (Func<T, Task<Result<TResult>>>)")]
    public async Task TaskResultOfTSelectManyAsyncOfFailureShouldNotInvokeSelectorDelegateFuncTTaskResultTResult()
    {
        // Given
        Result<int> result = Result<int>.Failure(FailureException);
        bool selectorInvoked = false;

        // When
        Result<string> actual = await Task.FromResult(result).SelectManyAsync(SelectorAsync, token: TestContext.Current.CancellationToken);

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

    [Fact(DisplayName = "Task<Result<T>>.SelectManyAsync<TResult> of Success should invoke the selector delegate (Func<T, CancellationToken, Task<Result<TResult>>>) and return its result")]
    public async Task TaskResultOfTSelectManyAsyncOfSuccessShouldInvokeSelectorDelegateFuncTCancellationTokenTaskResultTResult()
    {
        // Given
        Result<int> result = Result<int>.Success(42);
        bool selectorInvoked = false;

        // When
        Result<string> actual = await Task.FromResult(result).SelectManyAsync(SelectorAsync, token: TestContext.Current.CancellationToken);

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

    [Fact(DisplayName = "Task<Result<T>>.SelectManyAsync<TResult> of Failure should not invoke the selector delegate (Func<T, CancellationToken, Task<Result<TResult>>>)")]
    public async Task TaskResultOfTSelectManyAsyncOfFailureShouldNotInvokeSelectorDelegateFuncTCancellationTokenTaskResultTResult()
    {
        // Given
        Result<int> result = Result<int>.Failure(FailureException);
        bool selectorInvoked = false;

        // When
        Result<string> actual = await Task.FromResult(result).SelectManyAsync(SelectorAsync, token: TestContext.Current.CancellationToken);

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

    [Fact(DisplayName = "Result Success.ThrowAsync should do nothing")]
    public async Task ResultSuccessThrowAsyncShouldDoNothing()
    {
        // Given
        Result result = Result.Success();

        // When / Then
        await Task.FromResult(result).ThrowAsync(token: TestContext.Current.CancellationToken);
    }

    [Fact(DisplayName = "Result Failure.Throw should throw Exception")]
    public async Task ResultFailureThrowAsyncShouldThrowException()
    {
        // Given
        Result result = Result.Failure(FailureException);

        // When / Then
        await Assert.ThrowsAsync<Exception>(async () => await Task.FromResult(result).ThrowAsync(token: TestContext.Current.CancellationToken));
    }

    [Fact(DisplayName = "Result<T> Success.ThrowAsync should do nothing")]
    public async Task ResultOfTSuccessThrowAsyncShouldDoNothing()
    {
        // Given
        Result<int> result = Result<int>.Success(123);

        // When / Then
        await Task.FromResult(result).ThrowAsync(token: TestContext.Current.CancellationToken);
    }

    [Fact(DisplayName = "Result<T> Failure.ThrowAsync should throw Exception")]
    public async Task ResultOfTFailureThrowAsyncShouldThrowException()
    {
        // Given
        Result<int> result = Result<int>.Failure(FailureException);

        // When / Then
        await Assert.ThrowsAsync<Exception>(async () => await Task.FromResult(result).ThrowAsync(token: TestContext.Current.CancellationToken));
    }

    [Fact(DisplayName = "Result Failure.ToTypedResultAsync should produce the expected result.")]
    public async Task ResultFailureToTypedResultAsyncShouldProduceExpectedResult()
    {
        // Given
        Failure result = Result.Failure(FailureException);

        // When
        Result<int> actual = await Task.FromResult(result).ToTypedResultAsync<int>(token: TestContext.Current.CancellationToken);

        // Then
        Assert.IsType<Failure<int>>(actual);
        Assert.Equal(FailureException, actual.GetExceptionOrThrow());
    }

    [Fact(DisplayName = "Result<T> Failure.ToTypedResultAsync should produce the expected result.")]
    public async Task ResultOfTFailureToTypedResultAsyncShouldProduceExpectedResult()
    {
        // Given
        Failure<string> result = Result<string>.Failure(FailureException);

        // When
        Result<int> actual = await Task.FromResult(result).ToTypedResultAsync<string, int>(token: TestContext.Current.CancellationToken);

        // Then
        Assert.IsType<Failure<int>>(actual);
        Assert.Equal(FailureException, actual.GetExceptionOrThrow());
    }

    [Fact(DisplayName = "Result<T> Failure.ToUntypedResultAsync should produce the expected result.")]
    public async Task ResultOfTFailureToUntypedResultAsyncShouldProduceExpectedResult()
    {
        // Given
        Failure<string> result = Result<string>.Failure(FailureException);

        // When
        Result actual = await Task.FromResult(result).ToUntypedResultAsync(token: TestContext.Current.CancellationToken);

        // Then
        Assert.IsType<Failure>(actual);
        Assert.StartsWith("System.Exception: Failure", actual.GetExceptionOrThrow().ToString());
    }
}
