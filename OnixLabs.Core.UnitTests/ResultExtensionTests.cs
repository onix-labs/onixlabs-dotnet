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
using System.Threading.Tasks;
using Xunit;

namespace OnixLabs.Core.UnitTests;

public sealed class ResultExtensionTests
{
    [Fact(DisplayName = "Result Success.GetExceptionOrDefaultAsync should produce the expected result.")]
    public async Task ResultSuccessGetExceptionOrDefaultAsyncShouldProduceExpectedResult()
    {
        // Given
        Result result = Result.Success();

        // When
        Exception? actual = await Task.FromResult(result).GetExceptionOrDefaultAsync();

        // Then
        Assert.Null(actual);
    }

    [Fact(DisplayName = "Result Success.GetExceptionOrDefaultAsync with default value should produce the expected result.")]
    public async Task ResultSuccessGetExceptionOrDefaultAsyncWithDefaultValueShouldProduceExpectedResult()
    {
        // Given
        Exception expected = new("failure");
        Result result = Result.Success();

        // When
        Exception actual = await Task.FromResult(result).GetExceptionOrDefaultAsync(expected);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result Success.GetExceptionOrThrowAsync should produce the expected result.")]
    public async Task ResultSuccessGetExceptionOrThrowAsyncShouldProduceExpectedResult()
    {
        // Given
        Result result = Result.Success();

        // When
        Exception exception = await Assert.ThrowsAsync<InvalidOperationException>(async () => await Task.FromResult(result).GetExceptionOrThrowAsync());

        // Then
        Assert.Equal("The current result is not in a failure state.", exception.Message);
    }

    [Fact(DisplayName = "Result Failure.GetExceptionOrDefaultAsync should produce the expected result.")]
    public async Task ResultFailureGetExceptionOrDefaultAsyncShouldProduceExpectedResult()
    {
        // Given
        Exception expected = new("failure");
        Result result = Result.Failure(expected);

        // When
        Exception? actual = await Task.FromResult(result).GetExceptionOrDefaultAsync();

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result Failure.GetExceptionOrDefaultAsync with default value should produce the expected result.")]
    public async Task ResultFailureGetExceptionOrDefaultAsyncWithDefaultValueShouldProduceExpectedResult()
    {
        // Given
        Exception expected = new("failure");
        Result result = Result.Failure(expected);

        // When
        Exception actual = await Task.FromResult(result).GetExceptionOrDefaultAsync(new Exception("unexpected exception"));

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result Failure.GetExceptionOrThrowAsync should produce the expected result.")]
    public async Task ResultFailureGetExceptionOrThrowAsyncShouldProduceExpectedResult()
    {
        // Given
        Exception expected = new("failure");
        Result result = Result.Failure(expected);

        // When
        Exception actual = await Task.FromResult(result).GetExceptionOrThrowAsync();

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result<T> Success.GetExceptionOrDefaultAsync should produce the expected result.")]
    public async Task ResultOfTSuccessGetExceptionOrDefaultAsyncShouldProduceExpectedResult()
    {
        // Given
        Result<int> result = Result<int>.Success(123);

        // When
        Exception? actual = await Task.FromResult(result).GetExceptionOrDefaultAsync();

        // Then
        Assert.Null(actual);
    }

    [Fact(DisplayName = "Result<T> Success.GetExceptionOrDefaultAsync with default value should produce the expected result.")]
    public async Task ResultOfTSuccessGetExceptionOrDefaultAsyncWithDefaultValueShouldProduceExpectedResult()
    {
        // Given
        Exception expected = new("failure");
        Result<int> result = Result<int>.Success(123);

        // When
        Exception actual = await Task.FromResult(result).GetExceptionOrDefaultAsync(expected);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result<T> Success.GetExceptionOrThrowAsync should produce the expected result.")]
    public async Task ResultOfTSuccessGetExceptionOrThrowAsyncShouldProduceExpectedResult()
    {
        // Given
        Result<int> result = Result<int>.Success(123);

        // When
        Exception exception = await Assert.ThrowsAsync<InvalidOperationException>(async () => await Task.FromResult(result).GetExceptionOrThrowAsync());

        // Then
        Assert.Equal("The current result is not in a failure state.", exception.Message);
    }

    [Fact(DisplayName = "Result<T> Failure.GetExceptionOrDefaultAsync should produce the expected result.")]
    public async Task ResultOfTFailureGetExceptionOrDefaultAsyncShouldProduceExpectedResult()
    {
        // Given
        Exception expected = new("failure");
        Result<int> result = Result<int>.Failure(expected);

        // When
        Exception? actual = await Task.FromResult(result).GetExceptionOrDefaultAsync();

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result<T> Failure.GetExceptionOrDefaultAsync with default value should produce the expected result.")]
    public async Task ResultOfTFailureGetExceptionOrDefaultAsyncWithDefaultValueShouldProduceExpectedResult()
    {
        // Given
        Exception expected = new("failure");
        Result<int> result = Result<int>.Failure(expected);

        // When
        Exception actual = await Task.FromResult(result).GetExceptionOrDefaultAsync(new Exception("unexpected exception"));

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result<T> Failure.GetExceptionOrThrowAsync should produce the expected result.")]
    public async Task ResultOfTFailureGetExceptionOrThrowAsyncShouldProduceExpectedResult()
    {
        // Given
        Exception expected = new("failure");
        Result<int> result = Result<int>.Failure(expected);

        // When
        Exception actual = await Task.FromResult(result).GetExceptionOrThrowAsync();

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result.GetValueOrDefaultAsync should return the result value when the result is Success")]
    public async Task ResultGetValueOrDefaultAsyncShouldReturnResultValueWhenResultIsSuccess()
    {
        // Given
        const int expectedNumber = 123;
        const string expectedText = "abc";
        Task<Result<int>> numberTask = Result<int>.OfAsync(async () => await Task.FromResult(expectedNumber));
        Task<Result<string>> textTask = Result<string>.OfAsync(async () => await Task.FromResult(expectedText));

        // When
        int actualNumber = await numberTask.GetValueOrDefaultAsync();
        string? actualText = await textTask.GetValueOrDefaultAsync();

        // Then
        Assert.Equal(expectedNumber, actualNumber);

        Assert.NotNull(actualText);
        Assert.Equal(expectedText, actualText);
    }

    [Fact(DisplayName = "Result.GetValueOrDefaultAsync should return default when the result is Failure")]
    public async Task ResultGetValueOrDefaultAsyncShouldReturnDefaultWhenResultIsFailure()
    {
        // Given
        Exception exception = new("failure");
        Task<Result<int>> numberTask = Result<int>.OfAsync(() => throw exception);
        Task<Result<string>> textTask = Result<string>.OfAsync(() => throw exception);

        // When
        int actualNumber = await numberTask.GetValueOrDefaultAsync();
        string? actualText = await textTask.GetValueOrDefaultAsync();

        // Then
        Assert.Equal(default, actualNumber);
        Assert.Null(actualText);
    }

    [Fact(DisplayName = "Result.GetValueOrDefaultAsync with default value should return the result value when the result is Success")]
    public async Task ResultGetValueOrDefaultAsyncWithDefaultValueShouldReturnResultValueWhenResultIsSuccess()
    {
        // Given
        const int expectedNumber = 123;
        const string expectedText = "abc";
        Task<Result<int>> numberTask = Result<int>.OfAsync(async () => await Task.FromResult(expectedNumber));
        Task<Result<string>> textTask = Result<string>.OfAsync(async () => await Task.FromResult(expectedText));

        // When
        int actualNumber = await numberTask.GetValueOrDefaultAsync(456);
        // ReSharper disable once VariableCanBeNotNullable
        string? actualText = await textTask.GetValueOrDefaultAsync("xyz");

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
        Exception exception = new("failure");
        Task<Result<int>> numberTask = Result<int>.OfAsync(() => throw exception);
        Task<Result<string>> textTask = Result<string>.OfAsync(() => throw exception);

        // When
        int actualNumber = await numberTask.GetValueOrDefaultAsync(expectedNumber);
        string actualText = await textTask.GetValueOrDefaultAsync(expectedText);

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
        Task<Result<int>> numberTask = Result<int>.OfAsync(async () => await Task.FromResult(expectedNumber));
        Task<Result<string>> textTask = Result<string>.OfAsync(async () => await Task.FromResult(expectedText));

        // When
        int actualNumber = await numberTask.GetValueOrThrowAsync();
        string actualText = await textTask.GetValueOrThrowAsync();

        // Then
        Assert.Equal(expectedNumber, actualNumber);

        Assert.NotNull(actualText);
        Assert.Equal(expectedText, actualText);
    }

    [Fact(DisplayName = "Result.GetValueOrThrowAsync should throw Exception when the result is Failure")]
    public async Task ResultGetValueOrThrowAsyncShouldReturnDefaultWhenResultIsFailure()
    {
        // Given
        Exception exception = new("failure");
        Task<Result<int>> numberTask = Result<int>.OfAsync(() => throw exception);
        Task<Result<string>> textTask = Result<string>.OfAsync(() => throw exception);

        // When
        Exception numberException = await Assert.ThrowsAsync<Exception>(async () => await numberTask.GetValueOrThrowAsync());
        Exception textException = await Assert.ThrowsAsync<Exception>(async () => await textTask.GetValueOrThrowAsync());

        // Then
        Assert.Equal(numberException, exception);
        Assert.Equal(textException, exception);
    }

    [Fact(DisplayName = "Result.GetValueOrNone should return the Optional value when the result is Success and the Optional value is not None")]
    public void ResultGetValueOrNoneShouldReturnOptionalValueWhenResultIsSuccessAndOptionalValueIsNotNone()
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

    [Fact(DisplayName = "Result.GetValueOrNone should return None value when the result is Success and the Optional value is None")]
    public void ResultGetValueOrNoneShouldReturnNoneWhenResultIsSuccessAndOptionalValueIsNotNone()
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

    [Fact(DisplayName = "Result.GetValueOrNone should return None value when the result is Failure")]
    public void ResultGetValueOrNoneShouldReturnNoneWhenResultIsFailure()
    {
        // Given
        Optional<int> expectedNumber = Optional<int>.None;
        Optional<string> expectedText = Optional<string>.None;
        Result<Optional<int>> numberResult = Result<Optional<int>>.Failure(new Exception("Result has failed."));
        Result<Optional<string>> textResult = Result<Optional<string>>.Failure(new Exception("Result has failed."));

        // When
        Optional<int> actualNumber = numberResult.GetValueOrNone();
        Optional<string> actualText = textResult.GetValueOrNone();

        // Then
        Assert.Equal(expectedNumber, actualNumber);
        Assert.Equal(expectedText, actualText);
    }

    [Fact(DisplayName = "Result.GetValueOrNoneAsync should return the Optional value when the result is Success and the Optional value is not None")]
    public async Task ResultGetValueOrNoneAsyncShouldReturnOptionalValueWhenResultIsSuccessAndOptionalValueIsNotNone()
    {
        // Given
        Optional<int> expectedNumber = 123;
        Optional<string> expectedText = "abc";
        Result<Optional<int>> numberResult = expectedNumber;
        Result<Optional<string>> textResult = expectedText;

        // When
        Optional<int> actualNumber = await Task.FromResult(numberResult).GetValueOrNoneAsync();
        Optional<string> actualText = await Task.FromResult(textResult).GetValueOrNoneAsync();

        // Then
        Assert.Equal(expectedNumber, actualNumber);
        Assert.Equal(expectedText, actualText);
    }

    [Fact(DisplayName = "Result.GetValueOrNoneAsync should return None value when the result is Success and the Optional value is None")]
    public async Task ResultGetValueOrNoneAsyncShouldReturnNoneWhenResultIsSuccessAndOptionalValueIsNotNone()
    {
        // Given
        Optional<int> expectedNumber = Optional<int>.None;
        Optional<string> expectedText = Optional<string>.None;
        Result<Optional<int>> numberResult = expectedNumber;
        Result<Optional<string>> textResult = expectedText;

        // When
        Optional<int> actualNumber = await Task.FromResult(numberResult).GetValueOrNoneAsync();
        Optional<string> actualText = await Task.FromResult(textResult).GetValueOrNoneAsync();

        // Then
        Assert.Equal(expectedNumber, actualNumber);
        Assert.Equal(expectedText, actualText);
    }

    [Fact(DisplayName = "Result.GetValueOrNoneAsync should return None value when the result is Failure")]
    public async Task ResultGetValueOrNoneAsyncShouldReturnNoneWhenResultIsFailure()
    {
        // Given
        Optional<int> expectedNumber = Optional<int>.None;
        Optional<string> expectedText = Optional<string>.None;
        Result<Optional<int>> numberResult = Result<Optional<int>>.Failure(new Exception("Result has failed."));
        Result<Optional<string>> textResult = Result<Optional<string>>.Failure(new Exception("Result has failed."));

        // When
        Optional<int> actualNumber = await Task.FromResult(numberResult).GetValueOrNoneAsync();
        Optional<string> actualText = await Task.FromResult(textResult).GetValueOrNoneAsync();

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
        int actualNumber = await Task.FromResult(numberResult).GetOptionalValueOrThrowAsync();
        string actualText = await Task.FromResult(textResult).GetOptionalValueOrThrowAsync();

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
        Exception numberException = await Assert.ThrowsAsync<InvalidOperationException>(async () => await Task.FromResult(numberResult).GetOptionalValueOrThrowAsync());
        Exception textException = await Assert.ThrowsAsync<InvalidOperationException>(async () => await Task.FromResult(textResult).GetOptionalValueOrThrowAsync());

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
        Exception numberException = await Assert.ThrowsAsync<Exception>(async () => await Task.FromResult(numberResult).GetOptionalValueOrThrowAsync());
        Exception textException = await Assert.ThrowsAsync<Exception>(async () => await Task.FromResult(textResult).GetOptionalValueOrThrowAsync());

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
        int actualNumber = await Task.FromResult(numberResult).GetOptionalValueOrDefaultAsync(456);
        string actualText = await Task.FromResult(textResult).GetOptionalValueOrDefaultAsync("xyz");

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
        int actualNumber = await Task.FromResult(numberResult).GetOptionalValueOrDefaultAsync(expectedNumber);
        string actualText = await Task.FromResult(textResult).GetOptionalValueOrDefaultAsync(expectedText);

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
        int actualNumber = await Task.FromResult(numberResult).GetOptionalValueOrDefaultAsync(expectedNumber);
        string actualText = await Task.FromResult(textResult).GetOptionalValueOrDefaultAsync(expectedText);

        // Then
        Assert.Equal(expectedNumber, actualNumber);
        Assert.Equal(expectedText, actualText);
    }

    [Fact(DisplayName = "Result Success.MatchAsync should execute the some action.")]
    public async Task ResultSuccessMatchAsyncShouldExecuteSuccessAction()
    {
        // Given
        bool successCalled = false;
        Result result = Result.Success();

        // When
        await Task.FromResult(result).MatchAsync(success: () => { successCalled = true; });

        // Then
        Assert.True(successCalled);
    }

    [Fact(DisplayName = "Result Failure.MatchAsync should execute the none action.")]
    public async Task ResultFailureMatchAsyncShouldExecuteFailureAction()
    {
        // Given
        bool failureCalled = false;
        Exception exception = new("failure");
        Result result = Result.Failure(exception);

        // When
        await Task.FromResult(result).MatchAsync(failure: _ => { failureCalled = true; });

        // Then
        Assert.True(failureCalled);
    }

    [Fact(DisplayName = "Result Success.MatchAsync should produce the expected result.")]
    public async Task ResultSuccessMatchAsyncShouldProduceExpectedResult()
    {
        // Given
        const int expected = 9;
        Result result = Result.Success();

        // When
        int actual = await Task.FromResult(result).MatchAsync(
            success: () => 9,
            failure: _ => 0
        );

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result Failure.MatchAsync should produce the expected result.")]
    public async Task ResultFailureMatchAsyncShouldProduceExpectedResult()
    {
        // Given
        const int expected = 0;
        Exception exception = new("failure");
        Result result = Result.Failure(exception);

        // When
        int actual = await Task.FromResult(result).MatchAsync(
            success: () => 9,
            failure: _ => 0
        );

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result<T> Success.MatchAsync should execute the some action.")]
    public async Task ResultOfTSuccessMatchAsyncShouldExecuteSuccessAction()
    {
        // Given
        bool successCalled = false;
        Result<int> result = 123;

        // When
        await Task.FromResult(result).MatchAsync(success: _ => { successCalled = true; });

        // Then
        Assert.True(successCalled);
    }

    [Fact(DisplayName = "Result<T> Failure.MatchAsync should execute the none action.")]
    public async Task ResultOfTFailureMatchAsyncShouldExecuteFailureAction()
    {
        // Given
        bool failureCalled = false;
        Exception exception = new("failure");
        Result<int> result = Result<int>.Failure(exception);

        // When
        await Task.FromResult(result).MatchAsync(failure: _ => { failureCalled = true; });

        // Then
        Assert.True(failureCalled);
    }

    [Fact(DisplayName = "Result<T> Success.MatchAsync should produce the expected result.")]
    public async Task ResultOfTSuccessMatchAsyncShouldProduceExpectedResult()
    {
        // Given
        const int expected = 9;
        Result<int> result = 3;

        // When
        int actual = await Task.FromResult(result).MatchAsync(
            success: value => value * value,
            failure: _ => 0
        );

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result<T> Failure.MatchAsync should produce the expected result.")]
    public async Task ResultOfTFailureMatchAsyncShouldProduceExpectedResult()
    {
        // Given
        const int expected = 0;
        Exception exception = new("failure");
        Result<int> result = Result<int>.Failure(exception);

        // When
        int actual = await Task.FromResult(result).MatchAsync(
            success: value => value * value,
            failure: _ => 0
        );

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result Success.SelectAsync should produce the expected result")]
    public async Task ResultSuccessSelectAsyncShouldProduceExpectedResult()
    {
        // Given
        Result expected = Result.Success();

        // When
        Result actual = await Task.FromResult(expected).SelectAsync(() => { });

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result Failure.SelectAsync should produce the expected result")]
    public async Task ResultFailureSelectAsyncShouldProduceExpectedResult()
    {
        // Given
        Result expected = Result.Failure(new Exception("Failure"));

        // When
        Result actual = await Task.FromResult(expected).SelectAsync(() => { });

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result Success.SelectAsync<TResult> should produce the expected result")]
    public async Task ResultSuccessSelectAsyncTResultShouldProduceExpectedResult()
    {
        // Given
        const int expected = 9;
        Result result = Result.Success();

        // When
        Result<int> actual = await Task.FromResult(result).SelectAsync(() => 9);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result Failure.SelectAsync<TResult> should produce the expected result")]
    public async Task ResultFailureSelectAsyncTResultShouldProduceExpectedResult()
    {
        // Given
        Exception exception = new("failure");
        Result result = Result.Failure(exception);

        // When
        Result<int> actual = await Task.FromResult(result).SelectAsync(() => 9);

        // Then
        Assert.Equal(Result<int>.Failure(exception), actual);
    }

    [Fact(DisplayName = "Result<T> Success.SelectAsync should produce the expected result")]
    public async Task ResultOfTSuccessSelectAsyncShouldProduceExpectedResult()
    {
        // Given
        Result expected = Result.Success();
        Result<int> result = 123;

        // When
        Result actual = await Task.FromResult(result).SelectAsync(_ => { });

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result<T> Failure.SelectAsync should produce the expected result")]
    public async Task ResultOfTFailureSelectAsyncShouldProduceExpectedResult()
    {
        // Given
        Exception exception = new("Failure");
        Result expected = Result.Failure(exception);
        Result<int> result = Result<int>.Failure(exception);

        // When
        Result actual = await Task.FromResult(result).SelectAsync(_ => { });

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result<T> Success.SelectAsync<TResult> should produce the expected result")]
    public async Task ResultOfTSuccessSelectAsyncTResultShouldProduceExpectedResult()
    {
        // Given
        Result<int> expected = 9;
        Result<int> result = 3;

        // When
        Result<int> actual = await Task.FromResult(result).SelectAsync(x => x * x);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result<T> Failure.SelectAsync<TResult> should produce the expected result")]
    public async Task ResultOfTFailureSelectAsyncTResultShouldProduceExpectedResult()
    {
        // Given
        Exception exception = new("failure");
        Result<int> result = Result<int>.Failure(exception);

        // When
        Result<int> actual = await Task.FromResult(result).SelectAsync(x => x * x);

        // Then
        Assert.Equal(Result<int>.Failure(exception), actual);
    }

    [Fact(DisplayName = "Result Success.SelectManyAsync should produce the expected result")]
    public async Task ResultSuccessSelectManyAsyncShouldProduceExpectedResult()
    {
        // Given
        Result expected = Result.Success();

        // When
        Result actual = await Task.FromResult(expected).SelectManyAsync(Result.Success);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result Failure.SelectManyAsync should produce the expected result")]
    public async Task ResultFailureSelectManyAsyncShouldProduceExpectedResult()
    {
        // Given
        Result expected = Result.Failure(new Exception("Failure"));

        // When
        Result actual = await Task.FromResult(expected).SelectManyAsync(Result.Success);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result Success.SelectManyAsync<TResult> should produce the expected result")]
    public async Task ResultSuccessSelectManyAsyncTResultShouldProduceExpectedResult()
    {
        // Given
        const int expected = 9;
        Result result = Result.Success();

        // When
        Result<int> actual = await Task.FromResult(result).SelectManyAsync<int>(() => 9);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result Failure.SelectManyAsync<TResult> should produce the expected result")]
    public async Task ResultFailureSelectManyAsyncTResultShouldProduceExpectedResult()
    {
        // Given
        Exception exception = new("failure");
        Result result = Result.Failure(exception);

        // When
        Result<int> actual = await Task.FromResult(result).SelectManyAsync<int>(() => 9);

        // Then
        Assert.Equal(Result<int>.Failure(exception), actual);
    }

    [Fact(DisplayName = "Result<T> Success.SelectManyAsync should produce the expected result")]
    public async Task ResultOfTSuccessSelectManyAsyncShouldProduceExpectedResult()
    {
        // Given
        Result expected = Result.Success();
        Result<int> result = 3;

        // When
        Result actual = await Task.FromResult(result).SelectManyAsync(_ => Result.Success());

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result<T> Failure.SelectManyAsync should produce the expected result")]
    public async Task ResultOfTFailureSelectManyAsyncShouldProduceExpectedResult()
    {
        // Given
        Exception exception = new("Failure");
        Result expected = Result.Failure(exception);
        Result<int> result = Result<int>.Failure(exception);

        // When
        Result actual = await Task.FromResult(result).SelectManyAsync(_ => Result.Success());

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result<T> Success.SelectManyAsync<TResult> should produce the expected result")]
    public async Task ResultOfTSuccessSelectManyAsyncTResultShouldProduceExpectedResult()
    {
        // Given
        Result<int> expected = 9;
        Result<int> result = 3;

        // When
        Result<int> actual = await Task.FromResult(result).SelectManyAsync(x => Result<int>.Success(x * x));

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Result<T> Failure.SelectManyAsync<TResult> should produce the expected result")]
    public async Task ResultOfTFailureSelectManyAsyncTResultShouldProduceExpectedResult()
    {
        // Given
        Exception exception = new("failure");
        Result<int> result = Result<int>.Failure(exception);

        // When
        Result<int> actual = await Task.FromResult(result).SelectManyAsync(x => Result<int>.Success(x * x));

        // Then
        Assert.Equal(Result<int>.Failure(exception), actual);
    }

    [Fact(DisplayName = "Result Success.ThrowAsync should do nothing")]
    public async Task ResultSuccessThrowAsyncShouldDoNothing()
    {
        // Given
        Result result = Result.Success();

        // When / Then
        await Task.FromResult(result).ThrowAsync();
    }

    [Fact(DisplayName = "Result Failure.Throw should throw Exception")]
    public async Task ResultFailureThrowAsyncShouldThrowException()
    {
        // Given
        Exception exception = new("failure");
        Result result = Result.Failure(exception);

        // When / Then
        await Assert.ThrowsAsync<Exception>(async () => await Task.FromResult(result).ThrowAsync());
    }

    [Fact(DisplayName = "Result<T> Success.ThrowAsync should do nothing")]
    public async Task ResultOfTSuccessThrowAsyncShouldDoNothing()
    {
        // Given
        Result<int> result = Result<int>.Success(123);

        // When / Then
        await Task.FromResult(result).ThrowAsync();
    }

    [Fact(DisplayName = "Result<T> Failure.ThrowAsync should throw Exception")]
    public async Task ResultOfTFailureThrowAsyncShouldThrowException()
    {
        // Given
        Exception exception = new("failure");
        Result<int> result = Result<int>.Failure(exception);

        // When / Then
        await Assert.ThrowsAsync<Exception>(async () => await Task.FromResult(result).ThrowAsync());
    }

    [Fact(DisplayName = "Result Failure.ToTypedResultAsync should produce the expected result.")]
    public async Task ResultFailureToTypedResultAsyncShouldProduceExpectedResult()
    {
        // Given
        Exception exception = new("failure");
        Failure result = Result.Failure(exception);

        // When
        Result<int> actual = await Task.FromResult(result).ToTypedResultAsync<int>();

        // Then
        Assert.IsType<Failure<int>>(actual);
        Assert.Equal("System.Exception: failure", actual.GetExceptionOrThrow().ToString());
    }

    [Fact(DisplayName = "Result<T> Failure.ToTypedResultAsync should produce the expected result.")]
    public async Task ResultOfTFailureToTypedResultAsyncShouldProduceExpectedResult()
    {
        // Given
        Exception exception = new("failure");
        Failure<string> result = Result<string>.Failure(exception);

        // When
        Result<int> actual = await Task.FromResult(result).ToTypedResultAsync<string, int>();

        // Then
        Assert.IsType<Failure<int>>(actual);
        Assert.Equal("System.Exception: failure", actual.GetExceptionOrThrow().ToString());
    }

    [Fact(DisplayName = "Result<T> Failure.ToUntypedResultAsync should produce the expected result.")]
    public async Task ResultOfTFailureToUntypedResultAsyncShouldProduceExpectedResult()
    {
        // Given
        Exception exception = new("failure");
        Failure<string> result = Result<string>.Failure(exception);

        // When
        Result actual = await Task.FromResult(result).ToUntypedResultAsync();

        // Then
        Assert.IsType<Failure>(actual);
        Assert.Equal("System.Exception: failure", actual.GetExceptionOrThrow().ToString());
    }
}
