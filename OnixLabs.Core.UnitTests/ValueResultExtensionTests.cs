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

public sealed class ValueResultExtensionTests
{
    private static readonly Exception FailureException = new("Failure");

    [Fact(DisplayName = "ValueResult Success.GetExceptionOrDefaultAsync should produce the expected result.")]
    public async Task ValueResultSuccessGetExceptionOrDefaultAsyncShouldProduceExpectedResult()
    {
        // Given
        ValueResult result = ValueResult.Success();

        // When
        Exception? actual = await Task.FromResult(result).GetExceptionOrDefaultAsync(cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Null(actual);
    }

    [Fact(DisplayName = "ValueResult Success.GetExceptionOrDefaultAsync with default value should produce the expected result.")]
    public async Task ValueResultSuccessGetExceptionOrDefaultAsyncWithDefaultValueShouldProduceExpectedResult()
    {
        // Given
        ValueResult result = ValueResult.Success();

        // When
        Exception actual = await Task.FromResult(result).GetExceptionOrDefaultAsync(FailureException, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(FailureException, actual);
    }

    [Fact(DisplayName = "ValueResult Success.GetExceptionOrThrowAsync should produce the expected result.")]
    public async Task ValueResultSuccessGetExceptionOrThrowAsyncShouldProduceExpectedResult()
    {
        // Given
        ValueResult result = ValueResult.Success();

        // When
        Exception exception = await Assert.ThrowsAsync<InvalidOperationException>(async () =>
            await Task.FromResult(result).GetExceptionOrThrowAsync(cancellationToken: TestContext.Current.CancellationToken));

        // Then
        Assert.Equal("The current result is in a success state; no exception to retrieve.", exception.Message);
    }

    [Fact(DisplayName = "ValueResult Failure.GetExceptionOrDefaultAsync should produce the expected result.")]
    public async Task ValueResultFailureGetExceptionOrDefaultAsyncShouldProduceExpectedResult()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);

        // When
        Exception? actual = await Task.FromResult(result).GetExceptionOrDefaultAsync(cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(FailureException, actual);
    }

    [Fact(DisplayName = "ValueResult Failure.GetExceptionOrDefaultAsync with default value should produce the expected result.")]
    public async Task ValueResultFailureGetExceptionOrDefaultAsyncWithDefaultValueShouldProduceExpectedResult()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);

        // When
        Exception actual = await Task.FromResult(result).GetExceptionOrDefaultAsync(new Exception("unexpected exception"), cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(FailureException, actual);
    }

    [Fact(DisplayName = "ValueResult Failure.GetExceptionOrThrowAsync should produce the expected result.")]
    public async Task ValueResultFailureGetExceptionOrThrowAsyncShouldProduceExpectedResult()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);

        // When
        Exception actual = await Task.FromResult(result).GetExceptionOrThrowAsync(cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(FailureException, actual);
    }

    [Fact(DisplayName = "ValueResult<T> Success.GetExceptionOrDefaultAsync should produce the expected result.")]
    public async Task ValueResultOfTSuccessGetExceptionOrDefaultAsyncShouldProduceExpectedResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(123);

        // When
        Exception? actual = await Task.FromResult(result).GetExceptionOrDefaultAsync(cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Null(actual);
    }

    [Fact(DisplayName = "ValueResult<T> Success.GetExceptionOrDefaultAsync with default value should produce the expected result.")]
    public async Task ValueResultOfTSuccessGetExceptionOrDefaultAsyncWithDefaultValueShouldProduceExpectedResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(123);

        // When
        Exception actual = await Task.FromResult(result).GetExceptionOrDefaultAsync(FailureException, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(FailureException, actual);
    }

    [Fact(DisplayName = "ValueResult<T> Success.GetExceptionOrThrowAsync should produce the expected result.")]
    public async Task ValueResultOfTSuccessGetExceptionOrThrowAsyncShouldProduceExpectedResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(123);

        // When
        Exception exception = await Assert.ThrowsAsync<InvalidOperationException>(async () =>
            await Task.FromResult(result).GetExceptionOrThrowAsync(cancellationToken: TestContext.Current.CancellationToken));

        // Then
        Assert.Equal("The current result is in a success state; no exception to retrieve.", exception.Message);
    }

    [Fact(DisplayName = "ValueResult<T> Failure.GetExceptionOrDefaultAsync should produce the expected result.")]
    public async Task ValueResultOfTFailureGetExceptionOrDefaultAsyncShouldProduceExpectedResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);

        // When
        Exception? actual = await Task.FromResult(result).GetExceptionOrDefaultAsync(cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(FailureException, actual);
    }

    [Fact(DisplayName = "ValueResult<T> Failure.GetExceptionOrDefaultAsync with default value should produce the expected result.")]
    public async Task ValueResultOfTFailureGetExceptionOrDefaultAsyncWithDefaultValueShouldProduceExpectedResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);

        // When
        Exception actual = await Task.FromResult(result).GetExceptionOrDefaultAsync(new Exception("unexpected exception"), cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(FailureException, actual);
    }

    [Fact(DisplayName = "ValueResult<T> Failure.GetExceptionOrThrowAsync should produce the expected result.")]
    public async Task ValueResultOfTFailureGetExceptionOrThrowAsyncShouldProduceExpectedResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);

        // When
        Exception actual = await Task.FromResult(result).GetExceptionOrThrowAsync(cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(FailureException, actual);
    }

    [Fact(DisplayName = "ValueResult.GetValueOrDefaultAsync should return the result value when the result is Success")]
    public async Task ValueResultGetValueOrDefaultAsyncShouldReturnResultValueWhenResultIsSuccess()
    {
        // Given
        const int expectedNumber = 123;
        const string expectedText = "abc";
        Task<ValueResult<int>> numberTask = ValueResult<int>.OfAsync(async () => await Task.FromResult(expectedNumber), TestContext.Current.CancellationToken);
        Task<ValueResult<string>> textTask = ValueResult<string>.OfAsync(async () => await Task.FromResult(expectedText), TestContext.Current.CancellationToken);

        // When
        int actualNumber = await numberTask.GetValueOrDefaultAsync(cancellationToken: TestContext.Current.CancellationToken);
        string? actualText = await textTask.GetValueOrDefaultAsync(cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expectedNumber, actualNumber);

        Assert.NotNull(actualText);
        Assert.Equal(expectedText, actualText);
    }

    [Fact(DisplayName = "ValueResult.GetValueOrDefaultAsync should return default when the result is Failure")]
    public async Task ValueResultGetValueOrDefaultAsyncShouldReturnDefaultWhenResultIsFailure()
    {
        // Given
        Task<ValueResult<int>> numberTask = ValueResult<int>.OfAsync(() => throw FailureException, TestContext.Current.CancellationToken);
        Task<ValueResult<string>> textTask = ValueResult<string>.OfAsync(() => throw FailureException, TestContext.Current.CancellationToken);

        // When
        int actualNumber = await numberTask.GetValueOrDefaultAsync(cancellationToken: TestContext.Current.CancellationToken);
        string? actualText = await textTask.GetValueOrDefaultAsync(cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(0, actualNumber);
        Assert.Null(actualText);
    }

    [Fact(DisplayName = "ValueResult.GetValueOrDefaultAsync with default value should return the result value when the result is Success")]
    public async Task ValueResultGetValueOrDefaultAsyncWithDefaultValueShouldReturnResultValueWhenResultIsSuccess()
    {
        // Given
        const int expectedNumber = 123;
        const string expectedText = "abc";
        Task<ValueResult<int>> numberTask = ValueResult<int>.OfAsync(async () => await Task.FromResult(expectedNumber), TestContext.Current.CancellationToken);
        Task<ValueResult<string>> textTask = ValueResult<string>.OfAsync(async () => await Task.FromResult(expectedText), TestContext.Current.CancellationToken);

        // When
        int actualNumber = await numberTask.GetValueOrDefaultAsync(456, cancellationToken: TestContext.Current.CancellationToken);
        // ReSharper disable once VariableCanBeNotNullable
        string? actualText = await textTask.GetValueOrDefaultAsync("xyz", cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expectedNumber, actualNumber);

        Assert.NotNull(actualText);
        Assert.Equal(expectedText, actualText);
    }

    [Fact(DisplayName = "ValueResult.GetValueOrDefaultAsync with default value should return default value when the result is Failure")]
    public async Task ValueResultGetValueOrDefaultAsyncWithDefaultValueShouldReturnDefaultValueWhenResultIsFailure()
    {
        // Given
        const int expectedNumber = 456;
        const string expectedText = "abc";
        Task<ValueResult<int>> numberTask = ValueResult<int>.OfAsync(() => throw FailureException, TestContext.Current.CancellationToken);
        Task<ValueResult<string>> textTask = ValueResult<string>.OfAsync(() => throw FailureException, TestContext.Current.CancellationToken);

        // When
        int actualNumber = await numberTask.GetValueOrDefaultAsync(expectedNumber, cancellationToken: TestContext.Current.CancellationToken);
        string actualText = await textTask.GetValueOrDefaultAsync(expectedText, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expectedNumber, actualNumber);

        Assert.NotNull(actualText);
        Assert.Equal(expectedText, actualText);
    }

    [Fact(DisplayName = "ValueResult.GetValueOrThrowAsync should return the result value when the result is Success")]
    public async Task ValueResultGetValueOrThrowAsyncShouldReturnResultValueWhenResultIsSuccess()
    {
        // Given
        const int expectedNumber = 123;
        const string expectedText = "abc";
        Task<ValueResult<int>> numberTask = ValueResult<int>.OfAsync(async () => await Task.FromResult(expectedNumber), TestContext.Current.CancellationToken);
        Task<ValueResult<string>> textTask = ValueResult<string>.OfAsync(async () => await Task.FromResult(expectedText), TestContext.Current.CancellationToken);

        // When
        int actualNumber = await numberTask.GetValueOrThrowAsync(cancellationToken: TestContext.Current.CancellationToken);
        string actualText = await textTask.GetValueOrThrowAsync(cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expectedNumber, actualNumber);

        Assert.NotNull(actualText);
        Assert.Equal(expectedText, actualText);
    }

    [Fact(DisplayName = "ValueResult.GetValueOrThrowAsync should throw Exception when the result is Failure")]
    public async Task ValueResultGetValueOrThrowAsyncShouldReturnDefaultWhenResultIsFailure()
    {
        // Given
        Task<ValueResult<int>> numberTask = ValueResult<int>.OfAsync(() => throw FailureException, TestContext.Current.CancellationToken);
        Task<ValueResult<string>> textTask = ValueResult<string>.OfAsync(() => throw FailureException, TestContext.Current.CancellationToken);

        // When
        Exception numberException = await Assert.ThrowsAsync<Exception>(async () =>
            await numberTask.GetValueOrThrowAsync(cancellationToken: TestContext.Current.CancellationToken));

        Exception textException = await Assert.ThrowsAsync<Exception>(async () =>
            await textTask.GetValueOrThrowAsync(cancellationToken: TestContext.Current.CancellationToken));

        // Then
        Assert.Equal(numberException, FailureException);
        Assert.Equal(textException, FailureException);
    }

    [Fact(DisplayName = "ValueResult<T>.GetValueOrNone should return the value wrapped in Optional when the result is Success and the value is not None")]
    public void ValueResultGetValueOrNoneShouldReturnOptionalValueWhenResultIsSuccessAndValueIsNotNone()
    {
        // Given
        const int expectedNumber = 123;
        const string expectedText = "abc";
        ValueResult<int> numberResult = expectedNumber;
        ValueResult<string> textResult = expectedText;

        // When
        ValueOptional<int> actualNumber = numberResult.GetValueOrNone();
        ValueOptional<string> actualText = textResult.GetValueOrNone();

        // Then
        Assert.Equal(ValueOptional<int>.Of(expectedNumber), actualNumber);
        Assert.Equal(ValueOptional<string>.Of(expectedText), actualText);
    }

    [Fact(DisplayName = "ValueResult<T>.GetValueOrNone should return Some(default) when the result is Success and the value is the default of a value type")]
    public void ValueResultGetValueOrNoneShouldReturnSomeDefaultWhenResultIsSuccessAndValueIsValueTypeDefault()
    {
        // Given
        ValueResult<int> numberResult = ValueResult<int>.Success(0);
        ValueResult<bool> boolResult = ValueResult<bool>.Success(false);

        // When
        ValueOptional<int> actualNumber = numberResult.GetValueOrNone();
        ValueOptional<bool> actualBool = boolResult.GetValueOrNone();

        // Then
        Assert.True(ValueOptional<int>.IsSome(actualNumber));
        Assert.Equal(0, actualNumber);
        Assert.True(ValueOptional<bool>.IsSome(actualBool));
        Assert.Equal(false, actualBool);
    }

    [Fact(DisplayName = "ValueResult<T>.GetValueOrNone should return None when the result is Success and the underlying value is null")]
    public void ValueResultGetValueOrNoneShouldReturnNoneWhenResultIsSuccessAndUnderlyingValueIsNull()
    {
        // Given
        ValueResult<string> textResult = ValueResult<string>.Success(null!);

        // When
        ValueOptional<string> actualText = textResult.GetValueOrNone();

        // Then
        Assert.Equal(ValueOptional<string>.None, actualText);
    }

    [Fact(DisplayName = "ValueResult<T>.GetValueOrNone should return None when the result is Failure")]
    public void ValueResultGetValueOrNoneShouldReturnNoneWhenResultIsFailure()
    {
        // Given
        ValueResult<int> numberResult = ValueResult<int>.Failure(FailureException);
        ValueResult<string> textResult = ValueResult<string>.Failure(FailureException);

        // When
        ValueOptional<int> actualNumber = numberResult.GetValueOrNone();
        ValueOptional<string> actualText = textResult.GetValueOrNone();

        // Then
        Assert.Equal(ValueOptional<int>.None, actualNumber);
        Assert.Equal(ValueOptional<string>.None, actualText);
    }

    [Fact(DisplayName = "ValueResult<T>.GetValueOrNoneAsync should return the value wrapped in Optional when the result is Success and the value is not None")]
    public async Task ValueResultGetValueOrNoneAsyncShouldReturnOptionalValueWhenResultIsSuccessAndValueIsNotNone()
    {
        // Given
        const int expectedNumber = 123;
        const string expectedText = "abc";
        ValueResult<int> numberResult = expectedNumber;
        ValueResult<string> textResult = expectedText;

        // When
        ValueOptional<int> actualNumber = await Task.FromResult(numberResult).GetValueOrNoneAsync(cancellationToken: TestContext.Current.CancellationToken);
        ValueOptional<string> actualText = await Task.FromResult(textResult).GetValueOrNoneAsync(cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(ValueOptional<int>.Of(expectedNumber), actualNumber);
        Assert.Equal(ValueOptional<string>.Of(expectedText), actualText);
    }

    [Fact(DisplayName = "ValueResult<T>.GetValueOrNoneAsync should return Some(default) when the result is Success and the value is the default of a value type")]
    public async Task ValueResultGetValueOrNoneAsyncShouldReturnSomeDefaultWhenResultIsSuccessAndValueIsValueTypeDefault()
    {
        // Given
        ValueResult<int> numberResult = ValueResult<int>.Success(0);

        // When
        ValueOptional<int> actualNumber = await Task.FromResult(numberResult).GetValueOrNoneAsync(cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(ValueOptional<int>.IsSome(actualNumber));
        Assert.Equal(0, actualNumber);
    }

    [Fact(DisplayName = "ValueResult<T>.GetValueOrNoneAsync should return None when the result is Success and the underlying value is null")]
    public async Task ValueResultGetValueOrNoneAsyncShouldReturnNoneWhenResultIsSuccessAndUnderlyingValueIsNull()
    {
        // Given
        ValueResult<string> textResult = ValueResult<string>.Success(null!);

        // When
        ValueOptional<string> actualText = await Task.FromResult(textResult).GetValueOrNoneAsync(cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(ValueOptional<string>.None, actualText);
    }

    [Fact(DisplayName = "ValueResult<T>.GetValueOrNoneAsync should return None when the result is Failure")]
    public async Task ValueResultGetValueOrNoneAsyncShouldReturnNoneWhenResultIsFailure()
    {
        // Given
        ValueResult<int> numberResult = ValueResult<int>.Failure(FailureException);
        ValueResult<string> textResult = ValueResult<string>.Failure(FailureException);

        // When
        ValueOptional<int> actualNumber = await Task.FromResult(numberResult).GetValueOrNoneAsync(cancellationToken: TestContext.Current.CancellationToken);
        ValueOptional<string> actualText = await Task.FromResult(textResult).GetValueOrNoneAsync(cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(ValueOptional<int>.None, actualNumber);
        Assert.Equal(ValueOptional<string>.None, actualText);
    }

    [Fact(DisplayName = "ValueResult<ValueOptional<T>>.GetValueOrNone should return the Optional value when the result is Success and the Optional value is not None")]
    public void ValueResultValueOptionalGetValueOrNoneShouldReturnOptionalValueWhenResultIsSuccessAndOptionalValueIsNotNone()
    {
        // Given
        ValueOptional<int> expectedNumber = 123;
        ValueOptional<string> expectedText = "abc";
        ValueResult<ValueOptional<int>> numberResult = expectedNumber;
        ValueResult<ValueOptional<string>> textResult = expectedText;

        // When
        ValueOptional<int> actualNumber = numberResult.GetValueOrNone();
        ValueOptional<string> actualText = textResult.GetValueOrNone();

        // Then
        Assert.Equal(expectedNumber, actualNumber);
        Assert.Equal(expectedText, actualText);
    }

    [Fact(DisplayName = "ValueResult<ValueOptional<T>>.GetValueOrNone should return None value when the result is Success and the Optional value is None")]
    public void ValueResultValueOptionalGetValueOrNoneShouldReturnNoneWhenResultIsSuccessAndOptionalValueIsNone()
    {
        // Given
        ValueOptional<int> expectedNumber = ValueOptional<int>.None;
        ValueOptional<string> expectedText = ValueOptional<string>.None;
        ValueResult<ValueOptional<int>> numberResult = expectedNumber;
        ValueResult<ValueOptional<string>> textResult = expectedText;

        // When
        ValueOptional<int> actualNumber = numberResult.GetValueOrNone();
        ValueOptional<string> actualText = textResult.GetValueOrNone();

        // Then
        Assert.Equal(expectedNumber, actualNumber);
        Assert.Equal(expectedText, actualText);
    }

    [Fact(DisplayName = "ValueResult<ValueOptional<T>>.GetValueOrNone should return None value when the result is Failure")]
    public void ValueResultValueOptionalGetValueOrNoneShouldReturnNoneWhenResultIsFailure()
    {
        // Given
        ValueOptional<int> expectedNumber = ValueOptional<int>.None;
        ValueOptional<string> expectedText = ValueOptional<string>.None;
        ValueResult<ValueOptional<int>> numberResult = ValueResult<ValueOptional<int>>.Failure(FailureException);
        ValueResult<ValueOptional<string>> textResult = ValueResult<ValueOptional<string>>.Failure(FailureException);

        // When
        ValueOptional<int> actualNumber = numberResult.GetValueOrNone();
        ValueOptional<string> actualText = textResult.GetValueOrNone();

        // Then
        Assert.Equal(expectedNumber, actualNumber);
        Assert.Equal(expectedText, actualText);
    }

    [Fact(DisplayName = "ValueResult<ValueOptional<T>>.GetValueOrNoneAsync should return the Optional value when the result is Success and the Optional value is not None")]
    public async Task ValueResultValueOptionalGetValueOrNoneAsyncShouldReturnOptionalValueWhenResultIsSuccessAndOptionalValueIsNotNone()
    {
        // Given
        ValueOptional<int> expectedNumber = 123;
        ValueOptional<string> expectedText = "abc";
        ValueResult<ValueOptional<int>> numberResult = expectedNumber;
        ValueResult<ValueOptional<string>> textResult = expectedText;

        // When
        ValueOptional<int> actualNumber = await Task.FromResult(numberResult).GetValueOrNoneAsync(cancellationToken: TestContext.Current.CancellationToken);
        ValueOptional<string> actualText = await Task.FromResult(textResult).GetValueOrNoneAsync(cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expectedNumber, actualNumber);
        Assert.Equal(expectedText, actualText);
    }

    [Fact(DisplayName = "ValueResult<ValueOptional<T>>.GetValueOrNoneAsync should return None value when the result is Success and the Optional value is None")]
    public async Task ValueResultValueOptionalGetValueOrNoneAsyncShouldReturnNoneWhenResultIsSuccessAndOptionalValueIsNone()
    {
        // Given
        ValueOptional<int> expectedNumber = ValueOptional<int>.None;
        ValueOptional<string> expectedText = ValueOptional<string>.None;
        ValueResult<ValueOptional<int>> numberResult = expectedNumber;
        ValueResult<ValueOptional<string>> textResult = expectedText;

        // When
        ValueOptional<int> actualNumber = await Task.FromResult(numberResult).GetValueOrNoneAsync(cancellationToken: TestContext.Current.CancellationToken);
        ValueOptional<string> actualText = await Task.FromResult(textResult).GetValueOrNoneAsync(cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expectedNumber, actualNumber);
        Assert.Equal(expectedText, actualText);
    }

    [Fact(DisplayName = "ValueResult<ValueOptional<T>>.GetValueOrNoneAsync should return None value when the result is Failure")]
    public async Task ValueResultValueOptionalGetValueOrNoneAsyncShouldReturnNoneWhenResultIsFailure()
    {
        // Given
        ValueOptional<int> expectedNumber = ValueOptional<int>.None;
        ValueOptional<string> expectedText = ValueOptional<string>.None;
        ValueResult<ValueOptional<int>> numberResult = ValueResult<ValueOptional<int>>.Failure(FailureException);
        ValueResult<ValueOptional<string>> textResult = ValueResult<ValueOptional<string>>.Failure(FailureException);

        // When
        ValueOptional<int> actualNumber = await Task.FromResult(numberResult).GetValueOrNoneAsync(cancellationToken: TestContext.Current.CancellationToken);
        ValueOptional<string> actualText = await Task.FromResult(textResult).GetValueOrNoneAsync(cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expectedNumber, actualNumber);
        Assert.Equal(expectedText, actualText);
    }

    [Fact(DisplayName = "ValueResult.GetOptionalValueOrThrow should return the Optional value when the ValueResult is Success and the Optional value is not None")]
    public void ValueResultGetOptionalValueOrThrowShouldReturnOptionalValueWhenResultIsSuccessAndOptionalValueIsNotNone()
    {
        // Given
        const int expectedNumber = 123;
        const string expectedText = "abc";
        ValueResult<ValueOptional<int>> numberResult = ValueOptional<int>.Some(expectedNumber);
        ValueResult<ValueOptional<string>> textResult = ValueOptional<string>.Some(expectedText);

        // When
        int actualNumber = numberResult.GetOptionalValueOrThrow();
        string actualText = textResult.GetOptionalValueOrThrow();

        // Then
        Assert.Equal(expectedNumber, actualNumber);
        Assert.Equal(expectedText, actualText);
    }

    [Fact(DisplayName = "ValueResult.GetOptionalValueOrThrow should throw InvalidOperationException when the ValueResult is Success and the Optional value is None")]
    public void ValueResultGetOptionalValueOrThrowShouldThrowInvalidOperationExceptionWhenResultIsSuccessAndOptionalValueIsNone()
    {
        // Given
        ValueResult<ValueOptional<int>> numberResult = ValueOptional<int>.None;
        ValueResult<ValueOptional<string>> textResult = ValueOptional<string>.None;

        // When
        Exception numberException = Assert.Throws<InvalidOperationException>(() => numberResult.GetOptionalValueOrThrow());
        Exception textException = Assert.Throws<InvalidOperationException>(() => textResult.GetOptionalValueOrThrow());

        // Then
        Assert.Equal("Optional value of type System.Int32 is not present.", numberException.Message);
        Assert.Equal("Optional value of type System.String is not present.", textException.Message);
    }

    [Fact(DisplayName = "ValueResult.GetOptionalValueOrThrow should throw InvalidOperationException when the ValueResult is Failure")]
    public void ValueResultGetOptionalValueOrThrowShouldThrowInvalidOperationExceptionWhenResultIsFailure()
    {
        // Given
        ValueResult<ValueOptional<int>> numberResult = ValueResult<ValueOptional<int>>.Failure(new Exception("Result has failed."));
        ValueResult<ValueOptional<string>> textResult = ValueResult<ValueOptional<string>>.Failure(new Exception("Result has failed."));

        // When
        Exception numberException = Assert.Throws<Exception>(() => numberResult.GetOptionalValueOrThrow());
        Exception textException = Assert.Throws<Exception>(() => textResult.GetOptionalValueOrThrow());

        // Then
        Assert.Equal("Result has failed.", numberException.Message);
        Assert.Equal("Result has failed.", textException.Message);
    }

    [Fact(DisplayName = "ValueResult.GetOptionalValueOrThrowAsync should return the Optional value when the ValueResult is Success and the Optional value is not None")]
    public async Task ValueResultGetOptionalValueOrThrowAsyncShouldReturnOptionalValueWhenResultIsSuccessAndOptionalValueIsNotNone()
    {
        // Given
        const int expectedNumber = 123;
        const string expectedText = "abc";
        ValueResult<ValueOptional<int>> numberResult = ValueOptional<int>.Some(expectedNumber);
        ValueResult<ValueOptional<string>> textResult = ValueOptional<string>.Some(expectedText);

        // When
        int actualNumber = await Task.FromResult(numberResult).GetOptionalValueOrThrowAsync(cancellationToken: TestContext.Current.CancellationToken);
        string actualText = await Task.FromResult(textResult).GetOptionalValueOrThrowAsync(cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expectedNumber, actualNumber);
        Assert.Equal(expectedText, actualText);
    }

    [Fact(DisplayName = "ValueResult.GetOptionalValueOrThrowAsync should throw InvalidOperationException when the ValueResult is Success and the Optional value is None")]
    public async Task ValueResultGetOptionalValueOrThrowAsyncShouldThrowInvalidOperationExceptionWhenResultIsSuccessAndOptionalValueIsNone()
    {
        // Given
        ValueResult<ValueOptional<int>> numberResult = ValueOptional<int>.None;
        ValueResult<ValueOptional<string>> textResult = ValueOptional<string>.None;

        // When
        Exception numberException = await Assert.ThrowsAsync<InvalidOperationException>(async () =>
            await Task.FromResult(numberResult).GetOptionalValueOrThrowAsync(cancellationToken: TestContext.Current.CancellationToken));

        Exception textException = await Assert.ThrowsAsync<InvalidOperationException>(async () =>
            await Task.FromResult(textResult).GetOptionalValueOrThrowAsync(cancellationToken: TestContext.Current.CancellationToken));

        // Then
        Assert.Equal("Optional value of type System.Int32 is not present.", numberException.Message);
        Assert.Equal("Optional value of type System.String is not present.", textException.Message);
    }

    [Fact(DisplayName = "ValueResult.GetOptionalValueOrThrowAsync should throw InvalidOperationException when the ValueResult is Failure")]
    public async Task ValueResultGetOptionalValueOrThrowAsyncShouldThrowInvalidOperationExceptionWhenResultIsFailure()
    {
        // Given
        ValueResult<ValueOptional<int>> numberResult = ValueResult<ValueOptional<int>>.Failure(new Exception("Result has failed."));
        ValueResult<ValueOptional<string>> textResult = ValueResult<ValueOptional<string>>.Failure(new Exception("Result has failed."));

        // When
        Exception numberException = await Assert.ThrowsAsync<Exception>(async () =>
            await Task.FromResult(numberResult).GetOptionalValueOrThrowAsync(cancellationToken: TestContext.Current.CancellationToken));

        Exception textException = await Assert.ThrowsAsync<Exception>(async () =>
            await Task.FromResult(textResult).GetOptionalValueOrThrowAsync(cancellationToken: TestContext.Current.CancellationToken));

        // Then
        Assert.Equal("Result has failed.", numberException.Message);
        Assert.Equal("Result has failed.", textException.Message);
    }

    [Fact(DisplayName = "ValueResult.GetOptionalValueOrDefault should return the Optional value when the ValueResult is Success and the Optional value is not None")]
    public void ValueResultGetOptionalValueOrDefaultShouldReturnOptionalValueWhenResultIsSuccessAndOptionalValueIsNotNone()
    {
        // Given
        const int expectedNumber = 123;
        const string expectedText = "abc";
        ValueResult<ValueOptional<int>> numberResult = ValueOptional<int>.Some(expectedNumber);
        ValueResult<ValueOptional<string>> textResult = ValueOptional<string>.Some(expectedText);

        // When
        int actualNumber = numberResult.GetOptionalValueOrDefault(456);
        string actualText = textResult.GetOptionalValueOrDefault("xyz");

        // Then
        Assert.Equal(expectedNumber, actualNumber);
        Assert.Equal(expectedText, actualText);
    }

    [Fact(DisplayName = "ValueResult.GetOptionalValueOrDefault should throw return the default value when the ValueResult is Success and the Optional value is None")]
    public void ValueResultGetOptionalValueOrDefaultShouldReturnDefaultValueWhenResultIsSuccessAndOptionalValueIsNone()
    {
        // Given
        const int expectedNumber = 456;
        const string expectedText = "xyz";
        ValueResult<ValueOptional<int>> numberResult = ValueOptional<int>.None;
        ValueResult<ValueOptional<string>> textResult = ValueOptional<string>.None;

        // When
        int actualNumber = numberResult.GetOptionalValueOrDefault(expectedNumber);
        string actualText = textResult.GetOptionalValueOrDefault(expectedText);

        // Then
        Assert.Equal(expectedNumber, actualNumber);
        Assert.Equal(expectedText, actualText);
    }

    [Fact(DisplayName = "ValueResult.GetOptionalValueOrDefault should return default value when the result is Failure")]
    public void ValueResultGetOptionalValueOrDefaultShouldReturnDefaultValueWhenResultIsFailure()
    {
        // Given
        const int expectedNumber = 456;
        const string expectedText = "xyz";
        ValueResult<ValueOptional<int>> numberResult = ValueResult<ValueOptional<int>>.Failure(new Exception("Result has failed."));
        ValueResult<ValueOptional<string>> textResult = ValueResult<ValueOptional<string>>.Failure(new Exception("Result has failed."));

        // When
        int actualNumber = numberResult.GetOptionalValueOrDefault(expectedNumber);
        string actualText = textResult.GetOptionalValueOrDefault(expectedText);

        // Then
        Assert.Equal(expectedNumber, actualNumber);
        Assert.Equal(expectedText, actualText);
    }

    [Fact(DisplayName = "ValueResult.GetOptionalValueOrDefaultAsync should return the Optional value when the ValueResult is Success and the Optional value is not None")]
    public async Task ValueResultGetOptionalValueOrDefaultAsyncShouldReturnOptionalValueWhenResultIsSuccessAndOptionalValueIsNotNone()
    {
        // Given
        const int expectedNumber = 123;
        const string expectedText = "abc";
        ValueResult<ValueOptional<int>> numberResult = ValueOptional<int>.Some(expectedNumber);
        ValueResult<ValueOptional<string>> textResult = ValueOptional<string>.Some(expectedText);

        // When
        int actualNumber = await Task.FromResult(numberResult).GetOptionalValueOrDefaultAsync(456, cancellationToken: TestContext.Current.CancellationToken);
        string actualText = await Task.FromResult(textResult).GetOptionalValueOrDefaultAsync("xyz", cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expectedNumber, actualNumber);
        Assert.Equal(expectedText, actualText);
    }

    [Fact(DisplayName = "ValueResult.GetOptionalValueOrDefaultAsync should throw return the default value when the ValueResult is Success and the Optional value is None")]
    public async Task ValueResultGetOptionalValueOrDefaultAsyncShouldReturnDefaultValueWhenResultIsSuccessAndOptionalValueIsNone()
    {
        // Given
        const int expectedNumber = 456;
        const string expectedText = "xyz";
        ValueResult<ValueOptional<int>> numberResult = ValueOptional<int>.None;
        ValueResult<ValueOptional<string>> textResult = ValueOptional<string>.None;

        // When
        int actualNumber = await Task.FromResult(numberResult).GetOptionalValueOrDefaultAsync(expectedNumber, cancellationToken: TestContext.Current.CancellationToken);
        string actualText = await Task.FromResult(textResult).GetOptionalValueOrDefaultAsync(expectedText, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expectedNumber, actualNumber);
        Assert.Equal(expectedText, actualText);
    }

    [Fact(DisplayName = "ValueResult.GetOptionalValueOrDefaultAsync should return default value when the result is Failure")]
    public async Task ValueResultGetOptionalValueOrDefaultAsyncShouldReturnDefaultValueWhenResultIsFailure()
    {
        // Given
        const int expectedNumber = 456;
        const string expectedText = "xyz";
        ValueResult<ValueOptional<int>> numberResult = ValueResult<ValueOptional<int>>.Failure(new Exception("Result has failed."));
        ValueResult<ValueOptional<string>> textResult = ValueResult<ValueOptional<string>>.Failure(new Exception("Result has failed."));

        // When
        int actualNumber = await Task.FromResult(numberResult).GetOptionalValueOrDefaultAsync(expectedNumber, cancellationToken: TestContext.Current.CancellationToken);
        string actualText = await Task.FromResult(textResult).GetOptionalValueOrDefaultAsync(expectedText, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expectedNumber, actualNumber);
        Assert.Equal(expectedText, actualText);
    }

    [Fact(DisplayName = "Task<ValueResult>.MatchAsync of Success should invoke the success delegate (Action, Action<Exception>)")]
    public async Task TaskValueResultMatchAsyncOfSuccessShouldInvokeSuccessDelegateActionActionException()
    {
        // Given
        ValueResult result = ValueResult.Success();
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await Task.FromResult(result).MatchAsync(Success, Failure, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(successCalled);
        Assert.False(failureCalled);

        return;
        void Success() => successCalled = true;
        void Failure(Exception exception) => failureCalled = true;
    }

    [Fact(DisplayName = "Task<ValueResult>.MatchAsync of Failure should invoke the failure delegate (Action, Action<Exception>)")]
    public async Task TaskValueResultMatchAsyncOfFailureShouldInvokeFailureDelegateActionActionException()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await Task.FromResult(result).MatchAsync(Success, Failure, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.False(successCalled);
        Assert.True(failureCalled);

        return;
        void Success() => successCalled = true;
        void Failure(Exception exception) => failureCalled = true;
    }

    [Fact(DisplayName = "Task<ValueResult>.MatchAsync of Success should invoke the success delegate (Func<Task>, Action<Exception>)")]
    public async Task TaskValueResultMatchAsyncOfSuccessShouldInvokeSuccessDelegateFuncTaskActionException()
    {
        // Given
        ValueResult result = ValueResult.Success();
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await Task.FromResult(result).MatchAsync(SuccessAsync, Failure, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(successCalled);
        Assert.False(failureCalled);

        return;
        async Task SuccessAsync() => await Task.Run(() => successCalled = true);
        void Failure(Exception exception) => failureCalled = true;
    }

    [Fact(DisplayName = "Task<ValueResult>.MatchAsync of Success should invoke the success delegate (Func<ValueTask>, Action<Exception>)")]
    public async Task TaskValueResultMatchAsyncOfSuccessShouldInvokeSuccessDelegateFuncTaskActionExceptionWithValueTaskDelegates()
    {
        // Given
        ValueResult result = ValueResult.Success();
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await Task.FromResult(result).MatchAsync(SuccessAsync, Failure, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(successCalled);
        Assert.False(failureCalled);

        return;
        async ValueTask SuccessAsync() => await Task.Run(() => successCalled = true);
        void Failure(Exception exception) => failureCalled = true;
    }

    [Fact(DisplayName = "Task<ValueResult>.MatchAsync of Failure should invoke the failure delegate (Func<Task>, Action<Exception>)")]
    public async Task TaskValueResultMatchAsyncOfFailureShouldInvokeFailureDelegateFuncTaskActionException()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await Task.FromResult(result).MatchAsync(SuccessAsync, Failure, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.False(successCalled);
        Assert.True(failureCalled);

        return;
        async Task SuccessAsync() => await Task.Run(() => successCalled = true);
        void Failure(Exception exception) => failureCalled = true;
    }

    [Fact(DisplayName = "Task<ValueResult>.MatchAsync of Failure should invoke the failure delegate (Func<ValueTask>, Action<Exception>)")]
    public async Task TaskValueResultMatchAsyncOfFailureShouldInvokeFailureDelegateFuncTaskActionExceptionWithValueTaskDelegates()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await Task.FromResult(result).MatchAsync(SuccessAsync, Failure, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.False(successCalled);
        Assert.True(failureCalled);

        return;
        async ValueTask SuccessAsync() => await Task.Run(() => successCalled = true);
        void Failure(Exception exception) => failureCalled = true;
    }

    [Fact(DisplayName = "Task<ValueResult>.MatchAsync of Success should invoke the success delegate (Func<CancellationToken, Task>, Action<Exception>)")]
    public async Task TaskValueResultMatchAsyncOfSuccessShouldInvokeSuccessDelegateFuncCancellationTokenTaskActionException()
    {
        // Given
        ValueResult result = ValueResult.Success();
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await Task.FromResult(result).MatchAsync(SuccessAsync, Failure, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(successCalled);
        Assert.False(failureCalled);

        return;
        async Task SuccessAsync(CancellationToken ct) => await Task.Run(() => successCalled = true, ct);
        void Failure(Exception exception) => failureCalled = true;
    }

    [Fact(DisplayName = "Task<ValueResult>.MatchAsync of Success should invoke the success delegate (Func<CancellationToken, ValueTask>, Action<Exception>)")]
    public async Task TaskValueResultMatchAsyncOfSuccessShouldInvokeSuccessDelegateFuncCancellationTokenTaskActionExceptionWithValueTaskDelegates()
    {
        // Given
        ValueResult result = ValueResult.Success();
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await Task.FromResult(result).MatchAsync(SuccessAsync, Failure, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(successCalled);
        Assert.False(failureCalled);

        return;
        async ValueTask SuccessAsync(CancellationToken ct) => await Task.Run(() => successCalled = true, ct);
        void Failure(Exception exception) => failureCalled = true;
    }

    [Fact(DisplayName = "Task<ValueResult>.MatchAsync of Failure should invoke the failure delegate (Func<CancellationToken, Task>, Action<Exception>)")]
    public async Task TaskValueResultMatchAsyncOfFailureShouldInvokeFailureDelegateFuncCancellationTokenTaskActionException()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await Task.FromResult(result).MatchAsync(SuccessAsync, Failure, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.False(successCalled);
        Assert.True(failureCalled);

        return;
        async Task SuccessAsync(CancellationToken ct) => await Task.Run(() => successCalled = true, ct);
        void Failure(Exception exception) => failureCalled = true;
    }

    [Fact(DisplayName = "Task<ValueResult>.MatchAsync of Failure should invoke the failure delegate (Func<CancellationToken, ValueTask>, Action<Exception>)")]
    public async Task TaskValueResultMatchAsyncOfFailureShouldInvokeFailureDelegateFuncCancellationTokenTaskActionExceptionWithValueTaskDelegates()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await Task.FromResult(result).MatchAsync(SuccessAsync, Failure, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.False(successCalled);
        Assert.True(failureCalled);

        return;
        async ValueTask SuccessAsync(CancellationToken ct) => await Task.Run(() => successCalled = true, ct);
        void Failure(Exception exception) => failureCalled = true;
    }

    [Fact(DisplayName = "Task<ValueResult>.MatchAsync of Success should invoke the success delegate (Action, Func<Exception, Task>)")]
    public async Task TaskValueResultMatchAsyncOfSuccessShouldInvokeSuccessDelegateActionFuncExceptionTask()
    {
        // Given
        ValueResult result = ValueResult.Success();
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await Task.FromResult(result).MatchAsync(Success, FailureAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(successCalled);
        Assert.False(failureCalled);

        return;
        void Success() => successCalled = true;
        async Task FailureAsync(Exception exception) => await Task.Run(() => failureCalled = true);
    }

    [Fact(DisplayName = "Task<ValueResult>.MatchAsync of Success should invoke the success delegate (Action, Func<Exception, ValueTask>)")]
    public async Task TaskValueResultMatchAsyncOfSuccessShouldInvokeSuccessDelegateActionFuncExceptionTaskWithValueTaskDelegates()
    {
        // Given
        ValueResult result = ValueResult.Success();
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await Task.FromResult(result).MatchAsync(Success, FailureAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(successCalled);
        Assert.False(failureCalled);

        return;
        void Success() => successCalled = true;
        async ValueTask FailureAsync(Exception exception) => await Task.Run(() => failureCalled = true);
    }

    [Fact(DisplayName = "Task<ValueResult>.MatchAsync of Failure should invoke the failure delegate (Action, Func<Exception, Task>)")]
    public async Task TaskValueResultMatchAsyncOfFailureShouldInvokeFailureDelegateActionFuncExceptionTask()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await Task.FromResult(result).MatchAsync(Success, FailureAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.False(successCalled);
        Assert.True(failureCalled);

        return;
        void Success() => successCalled = true;
        async Task FailureAsync(Exception exception) => await Task.Run(() => failureCalled = true);
    }

    [Fact(DisplayName = "Task<ValueResult>.MatchAsync of Failure should invoke the failure delegate (Action, Func<Exception, ValueTask>)")]
    public async Task TaskValueResultMatchAsyncOfFailureShouldInvokeFailureDelegateActionFuncExceptionTaskWithValueTaskDelegates()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await Task.FromResult(result).MatchAsync(Success, FailureAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.False(successCalled);
        Assert.True(failureCalled);

        return;
        void Success() => successCalled = true;
        async ValueTask FailureAsync(Exception exception) => await Task.Run(() => failureCalled = true);
    }

    [Fact(DisplayName = "Task<ValueResult>.MatchAsync of Success should invoke the success delegate (Action, Func<Exception, CancellationToken, Task>)")]
    public async Task TaskValueResultMatchAsyncOfSuccessShouldInvokeSuccessDelegateActionFuncExceptionCancellationTokenTask()
    {
        // Given
        ValueResult result = ValueResult.Success();
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await Task.FromResult(result).MatchAsync(Success, FailureAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(successCalled);
        Assert.False(failureCalled);

        return;
        void Success() => successCalled = true;
        async Task FailureAsync(Exception exception, CancellationToken ct) => await Task.Run(() => failureCalled = true, ct);
    }

    [Fact(DisplayName = "Task<ValueResult>.MatchAsync of Success should invoke the success delegate (Action, Func<Exception, CancellationToken, ValueTask>)")]
    public async Task TaskValueResultMatchAsyncOfSuccessShouldInvokeSuccessDelegateActionFuncExceptionCancellationTokenTaskWithValueTaskDelegates()
    {
        // Given
        ValueResult result = ValueResult.Success();
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await Task.FromResult(result).MatchAsync(Success, FailureAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(successCalled);
        Assert.False(failureCalled);

        return;
        void Success() => successCalled = true;
        async ValueTask FailureAsync(Exception exception, CancellationToken ct) => await Task.Run(() => failureCalled = true, ct);
    }

    [Fact(DisplayName = "Task<ValueResult>.MatchAsync of Failure should invoke the failure delegate (Action, Func<Exception, CancellationToken, Task>)")]
    public async Task TaskValueResultMatchAsyncOfFailureShouldInvokeFailureDelegateActionFuncExceptionCancellationTokenTask()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await Task.FromResult(result).MatchAsync(Success, FailureAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.False(successCalled);
        Assert.True(failureCalled);

        return;
        void Success() => successCalled = true;
        async Task FailureAsync(Exception exception, CancellationToken ct) => await Task.Run(() => failureCalled = true, ct);
    }

    [Fact(DisplayName = "Task<ValueResult>.MatchAsync of Failure should invoke the failure delegate (Action, Func<Exception, CancellationToken, ValueTask>)")]
    public async Task TaskValueResultMatchAsyncOfFailureShouldInvokeFailureDelegateActionFuncExceptionCancellationTokenTaskWithValueTaskDelegates()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await Task.FromResult(result).MatchAsync(Success, FailureAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.False(successCalled);
        Assert.True(failureCalled);

        return;
        void Success() => successCalled = true;
        async ValueTask FailureAsync(Exception exception, CancellationToken ct) => await Task.Run(() => failureCalled = true, ct);
    }

    [Fact(DisplayName = "Task<ValueResult>.MatchAsync of Success should invoke the success delegate (Func<Task>, Func<Exception, Task>)")]
    public async Task TaskValueResultMatchAsyncOfSuccessShouldInvokeSuccessDelegateFuncTaskFuncExceptionTask()
    {
        // Given
        ValueResult result = ValueResult.Success();
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await Task.FromResult(result).MatchAsync(SuccessAsync, FailureAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(successCalled);
        Assert.False(failureCalled);

        return;
        async Task SuccessAsync() => await Task.Run(() => successCalled = true);
        async Task FailureAsync(Exception exception) => await Task.Run(() => failureCalled = true);
    }

    [Fact(DisplayName = "Task<ValueResult>.MatchAsync of Success should invoke the success delegate (Func<ValueTask>, Func<Exception, ValueTask>)")]
    public async Task TaskValueResultMatchAsyncOfSuccessShouldInvokeSuccessDelegateFuncTaskFuncExceptionTaskWithValueTaskDelegates()
    {
        // Given
        ValueResult result = ValueResult.Success();
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await Task.FromResult(result).MatchAsync(SuccessAsync, FailureAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(successCalled);
        Assert.False(failureCalled);

        return;
        async ValueTask SuccessAsync() => await Task.Run(() => successCalled = true);
        async ValueTask FailureAsync(Exception exception) => await Task.Run(() => failureCalled = true);
    }

    [Fact(DisplayName = "Task<ValueResult>.MatchAsync of Failure should invoke the failure delegate (Func<Task>, Func<Exception, Task>)")]
    public async Task TaskValueResultMatchAsyncOfFailureShouldInvokeFailureDelegateFuncTaskFuncExceptionTask()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await Task.FromResult(result).MatchAsync(SuccessAsync, FailureAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.False(successCalled);
        Assert.True(failureCalled);

        return;
        async Task SuccessAsync() => await Task.Run(() => successCalled = true);
        async Task FailureAsync(Exception exception) => await Task.Run(() => failureCalled = true);
    }

    [Fact(DisplayName = "Task<ValueResult>.MatchAsync of Failure should invoke the failure delegate (Func<ValueTask>, Func<Exception, ValueTask>)")]
    public async Task TaskValueResultMatchAsyncOfFailureShouldInvokeFailureDelegateFuncTaskFuncExceptionTaskWithValueTaskDelegates()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await Task.FromResult(result).MatchAsync(SuccessAsync, FailureAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.False(successCalled);
        Assert.True(failureCalled);

        return;
        async ValueTask SuccessAsync() => await Task.Run(() => successCalled = true);
        async ValueTask FailureAsync(Exception exception) => await Task.Run(() => failureCalled = true);
    }

    [Fact(DisplayName = "Task<ValueResult>.MatchAsync of Success should invoke the success delegate (Func<CancellationToken, Task>, Func<Exception, CancellationToken, Task>)")]
    public async Task TaskValueResultMatchAsyncOfSuccessShouldInvokeSuccessDelegateFuncCancellationTokenTaskFuncExceptionCancellationTokenTask()
    {
        // Given
        ValueResult result = ValueResult.Success();
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await Task.FromResult(result).MatchAsync(SuccessAsync, FailureAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(successCalled);
        Assert.False(failureCalled);

        return;
        async Task SuccessAsync(CancellationToken ct) => await Task.Run(() => successCalled = true, ct);
        async Task FailureAsync(Exception exception, CancellationToken ct) => await Task.Run(() => failureCalled = true, ct);
    }

    [Fact(DisplayName = "Task<ValueResult>.MatchAsync of Success should invoke the success delegate (Func<CancellationToken, ValueTask>, Func<Exception, CancellationToken, ValueTask>)")]
    public async Task TaskValueResultMatchAsyncOfSuccessShouldInvokeSuccessDelegateFuncCancellationTokenTaskFuncExceptionCancellationTokenTaskWithValueTaskDelegates()
    {
        // Given
        ValueResult result = ValueResult.Success();
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await Task.FromResult(result).MatchAsync(SuccessAsync, FailureAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(successCalled);
        Assert.False(failureCalled);

        return;
        async ValueTask SuccessAsync(CancellationToken ct) => await Task.Run(() => successCalled = true, ct);
        async ValueTask FailureAsync(Exception exception, CancellationToken ct) => await Task.Run(() => failureCalled = true, ct);
    }

    [Fact(DisplayName = "Task<ValueResult>.MatchAsync of Failure should invoke the failure delegate (Func<CancellationToken, Task>, Func<Exception, CancellationToken, Task>)")]
    public async Task TaskValueResultMatchAsyncOfFailureShouldInvokeFailureDelegateFuncCancellationTokenTaskFuncExceptionCancellationTokenTask()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await Task.FromResult(result).MatchAsync(SuccessAsync, FailureAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.False(successCalled);
        Assert.True(failureCalled);

        return;
        async Task SuccessAsync(CancellationToken ct) => await Task.Run(() => successCalled = true, ct);
        async Task FailureAsync(Exception exception, CancellationToken ct) => await Task.Run(() => failureCalled = true, ct);
    }

    [Fact(DisplayName = "Task<ValueResult>.MatchAsync of Failure should invoke the failure delegate (Func<CancellationToken, ValueTask>, Func<Exception, CancellationToken, ValueTask>)")]
    public async Task TaskValueResultMatchAsyncOfFailureShouldInvokeFailureDelegateFuncCancellationTokenTaskFuncExceptionCancellationTokenTaskWithValueTaskDelegates()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await Task.FromResult(result).MatchAsync(SuccessAsync, FailureAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.False(successCalled);
        Assert.True(failureCalled);

        return;
        async ValueTask SuccessAsync(CancellationToken ct) => await Task.Run(() => successCalled = true, ct);
        async ValueTask FailureAsync(Exception exception, CancellationToken ct) => await Task.Run(() => failureCalled = true, ct);
    }

    [Fact(DisplayName = "Task<ValueResult>.MatchAsync<TResult> of Success should return the result from success delegate (Func<TResult>, Func<Exception, TResult>)")]
    public async Task TaskValueResultMatchAsyncOfSuccessShouldReturnResultFromSuccessDelegateFuncResultFuncExceptionResult()
    {
        // Given
        ValueResult result = ValueResult.Success();
        const string expected = "Success";

        // When
        string actual = await Task.FromResult(result).MatchAsync(Success, Failure, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        string Success() => "Success";
        string Failure(Exception exception) => "Failure";
    }

    [Fact(DisplayName = "Task<ValueResult>.MatchAsync<TResult> of Failure should return the result from failure delegate (Func<TResult>, Func<Exception, TResult>)")]
    public async Task TaskValueResultMatchAsyncOfFailureShouldReturnResultFromFailureDelegateFuncResultFuncExceptionResult()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
        const string expected = "Failure";

        // When
        string actual = await Task.FromResult(result).MatchAsync(Success, Failure, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        string Success() => "Success";
        string Failure(Exception exception) => "Failure";
    }

    [Fact(DisplayName = "Task<ValueResult>.MatchAsync<TResult> of Success should return the result from success delegate (Func<Task<TResult>>, Func<Exception, TResult>)")]
    public async Task TaskValueResultMatchAsyncOfSuccessShouldReturnResultFromSuccessDelegateFuncTaskResultFuncExceptionResult()
    {
        // Given
        ValueResult result = ValueResult.Success();
        const string expected = "Success";

        // When
        string actual = await Task.FromResult(result).MatchAsync(SuccessAsync, Failure, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        async Task<string> SuccessAsync() => await Task.FromResult("Success");
        string Failure(Exception exception) => "Failure";
    }

    [Fact(DisplayName = "Task<ValueResult>.MatchAsync<TResult> of Success should return the result from success delegate (Func<ValueTask<TResult>>, Func<Exception, TResult>)")]
    public async Task TaskValueResultMatchAsyncOfSuccessShouldReturnResultFromSuccessDelegateFuncTaskResultFuncExceptionResultWithValueTaskDelegates()
    {
        // Given
        ValueResult result = ValueResult.Success();
        const string expected = "Success";

        // When
        string actual = await Task.FromResult(result).MatchAsync(SuccessAsync, Failure, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        async ValueTask<string> SuccessAsync() => await ValueTask.FromResult("Success");
        string Failure(Exception exception) => "Failure";
    }

    [Fact(DisplayName = "Task<ValueResult>.MatchAsync<TResult> of Failure should return the result from failure delegate (Func<Task<TResult>>, Func<Exception, TResult>)")]
    public async Task TaskValueResultMatchAsyncOfFailureShouldReturnResultFromFailureDelegateFuncTaskResultFuncExceptionResult()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
        const string expected = "Failure";

        // When
        string actual = await Task.FromResult(result).MatchAsync(SuccessAsync, Failure, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        async Task<string> SuccessAsync() => await Task.FromResult("Success");
        string Failure(Exception exception) => "Failure";
    }

    [Fact(DisplayName = "Task<ValueResult>.MatchAsync<TResult> of Failure should return the result from failure delegate (Func<ValueTask<TResult>>, Func<Exception, TResult>)")]
    public async Task TaskValueResultMatchAsyncOfFailureShouldReturnResultFromFailureDelegateFuncTaskResultFuncExceptionResultWithValueTaskDelegates()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
        const string expected = "Failure";

        // When
        string actual = await Task.FromResult(result).MatchAsync(SuccessAsync, Failure, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        async ValueTask<string> SuccessAsync() => await ValueTask.FromResult("Success");
        string Failure(Exception exception) => "Failure";
    }

    [Fact(DisplayName = "Task<ValueResult>.MatchAsync<TResult> of Success should return the result from success delegate (Func<CancellationToken, Task<TResult>>, Func<Exception, TResult>)")]
    public async Task TaskValueResultMatchAsyncOfSuccessShouldReturnResultFromSuccessDelegateFuncCancellationTokenTaskResultFuncExceptionResult()
    {
        // Given
        ValueResult result = ValueResult.Success();
        const string expected = "Success";

        // When
        string actual = await Task.FromResult(result).MatchAsync(SuccessAsync, Failure, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        async Task<string> SuccessAsync(CancellationToken ct) => await Task.FromResult("Success");
        string Failure(Exception exception) => "Failure";
    }

    [Fact(DisplayName = "Task<ValueResult>.MatchAsync<TResult> of Success should return the result from success delegate (Func<CancellationToken, ValueTask<TResult>>, Func<Exception, TResult>)")]
    public async Task TaskValueResultMatchAsyncOfSuccessShouldReturnResultFromSuccessDelegateFuncCancellationTokenTaskResultFuncExceptionResultWithValueTaskDelegates()
    {
        // Given
        ValueResult result = ValueResult.Success();
        const string expected = "Success";

        // When
        string actual = await Task.FromResult(result).MatchAsync(SuccessAsync, Failure, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        async ValueTask<string> SuccessAsync(CancellationToken ct) => await ValueTask.FromResult("Success");
        string Failure(Exception exception) => "Failure";
    }

    [Fact(DisplayName = "Task<ValueResult>.MatchAsync<TResult> of Failure should return the result from failure delegate (Func<CancellationToken, Task<TResult>>, Func<Exception, TResult>)")]
    public async Task TaskValueResultMatchAsyncOfFailureShouldReturnResultFromFailureDelegateFuncCancellationTokenTaskResultFuncExceptionResult()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
        const string expected = "Failure";

        // When
        string actual = await Task.FromResult(result).MatchAsync(SuccessAsync, Failure, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        async Task<string> SuccessAsync(CancellationToken ct) => await Task.FromResult("Success");
        string Failure(Exception exception) => "Failure";
    }

    [Fact(DisplayName = "Task<ValueResult>.MatchAsync<TResult> of Failure should return the result from failure delegate (Func<CancellationToken, ValueTask<TResult>>, Func<Exception, TResult>)")]
    public async Task TaskValueResultMatchAsyncOfFailureShouldReturnResultFromFailureDelegateFuncCancellationTokenTaskResultFuncExceptionResultWithValueTaskDelegates()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
        const string expected = "Failure";

        // When
        string actual = await Task.FromResult(result).MatchAsync(SuccessAsync, Failure, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        async ValueTask<string> SuccessAsync(CancellationToken ct) => await ValueTask.FromResult("Success");
        string Failure(Exception exception) => "Failure";
    }

    [Fact(DisplayName = "Task<ValueResult>.MatchAsync<TResult> of Success should return the result from success delegate (Func<TResult>, Func<Exception, Task<TResult>>>)")]
    public async Task TaskValueResultMatchAsyncOfSuccessShouldReturnResultFromSuccessDelegateFuncResultFuncExceptionTaskResult()
    {
        // Given
        ValueResult result = ValueResult.Success();
        const string expected = "Success";

        // When
        string actual = await Task.FromResult(result).MatchAsync(Success, FailureAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        string Success() => "Success";
        async Task<string> FailureAsync(Exception exception) => await Task.FromResult("Failure");
    }

    [Fact(DisplayName = "Task<ValueResult>.MatchAsync<TResult> of Success should return the result from success delegate (Func<TResult>, Func<Exception, ValueTask<TResult>>>)")]
    public async Task TaskValueResultMatchAsyncOfSuccessShouldReturnResultFromSuccessDelegateFuncResultFuncExceptionTaskResultWithValueTaskDelegates()
    {
        // Given
        ValueResult result = ValueResult.Success();
        const string expected = "Success";

        // When
        string actual = await Task.FromResult(result).MatchAsync(Success, FailureAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        string Success() => "Success";
        async ValueTask<string> FailureAsync(Exception exception) => await ValueTask.FromResult("Failure");
    }

    [Fact(DisplayName = "Task<ValueResult>.MatchAsync<TResult> of Failure should return the result from failure delegate (Func<TResult>, Func<Exception, Task<TResult>>>)")]
    public async Task TaskValueResultMatchAsyncOfFailureShouldReturnResultFromFailureDelegateFuncResultFuncExceptionTaskResult()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
        const string expected = "Failure";

        // When
        string actual = await Task.FromResult(result).MatchAsync(Success, FailureAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        string Success() => "Success";
        async Task<string> FailureAsync(Exception exception) => await Task.FromResult("Failure");
    }

    [Fact(DisplayName = "Task<ValueResult>.MatchAsync<TResult> of Failure should return the result from failure delegate (Func<TResult>, Func<Exception, ValueTask<TResult>>>)")]
    public async Task TaskValueResultMatchAsyncOfFailureShouldReturnResultFromFailureDelegateFuncResultFuncExceptionTaskResultWithValueTaskDelegates()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
        const string expected = "Failure";

        // When
        string actual = await Task.FromResult(result).MatchAsync(Success, FailureAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        string Success() => "Success";
        async ValueTask<string> FailureAsync(Exception exception) => await ValueTask.FromResult("Failure");
    }

    [Fact(DisplayName = "Task<ValueResult>.MatchAsync<TResult> of Success should return the result from success delegate (Func<TResult>, Func<Exception, CancellationToken, Task<TResult>>>)")]
    public async Task TaskValueResultMatchAsyncOfSuccessShouldReturnResultFromSuccessDelegateFuncResultFuncExceptionCancellationTokenTaskResult()
    {
        // Given
        ValueResult result = ValueResult.Success();
        const string expected = "Success";

        // When
        string actual = await Task.FromResult(result).MatchAsync(Success, FailureAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        string Success() => "Success";
        async Task<string> FailureAsync(Exception exception, CancellationToken ct) => await Task.FromResult("Failure");
    }

    [Fact(DisplayName = "Task<ValueResult>.MatchAsync<TResult> of Success should return the result from success delegate (Func<TResult>, Func<Exception, CancellationToken, ValueTask<TResult>>>)")]
    public async Task TaskValueResultMatchAsyncOfSuccessShouldReturnResultFromSuccessDelegateFuncResultFuncExceptionCancellationTokenTaskResultWithValueTaskDelegates()
    {
        // Given
        ValueResult result = ValueResult.Success();
        const string expected = "Success";

        // When
        string actual = await Task.FromResult(result).MatchAsync(Success, FailureAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        string Success() => "Success";
        async ValueTask<string> FailureAsync(Exception exception, CancellationToken ct) => await ValueTask.FromResult("Failure");
    }

    [Fact(DisplayName = "Task<ValueResult>.MatchAsync<TResult> of Failure should return the result from failure delegate (Func<TResult>, Func<Exception, CancellationToken, Task<TResult>>>)")]
    public async Task TaskValueResultMatchAsyncOfFailureShouldReturnResultFromFailureDelegateFuncResultFuncExceptionCancellationTokenTaskResult()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
        const string expected = "Failure";

        // When
        string actual = await Task.FromResult(result).MatchAsync(Success, FailureAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        string Success() => "Success";
        async Task<string> FailureAsync(Exception exception, CancellationToken ct) => await Task.FromResult("Failure");
    }

    [Fact(DisplayName = "Task<ValueResult>.MatchAsync<TResult> of Failure should return the result from failure delegate (Func<TResult>, Func<Exception, CancellationToken, ValueTask<TResult>>>)")]
    public async Task TaskValueResultMatchAsyncOfFailureShouldReturnResultFromFailureDelegateFuncResultFuncExceptionCancellationTokenTaskResultWithValueTaskDelegates()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
        const string expected = "Failure";

        // When
        string actual = await Task.FromResult(result).MatchAsync(Success, FailureAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        string Success() => "Success";
        async ValueTask<string> FailureAsync(Exception exception, CancellationToken ct) => await ValueTask.FromResult("Failure");
    }

    [Fact(DisplayName = "Task<ValueResult>.MatchAsync<TResult> of Success should return the result from success delegate (Func<Task<TResult>>, Func<Exception, Task<TResult>>>)")]
    public async Task TaskValueResultMatchAsyncOfSuccessShouldReturnResultFromSuccessDelegateFuncTaskResultFuncExceptionTaskResult()
    {
        // Given
        ValueResult result = ValueResult.Success();
        const string expected = "Success";

        // When
        string actual = await Task.FromResult(result).MatchAsync(SuccessAsync, FailureAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        async Task<string> SuccessAsync() => await Task.FromResult("Success");
        async Task<string> FailureAsync(Exception exception) => await Task.FromResult("Failure");
    }

    [Fact(DisplayName = "Task<ValueResult>.MatchAsync<TResult> of Success should return the result from success delegate (Func<ValueTask<TResult>>, Func<Exception, ValueTask<TResult>>>)")]
    public async Task TaskValueResultMatchAsyncOfSuccessShouldReturnResultFromSuccessDelegateFuncTaskResultFuncExceptionTaskResultWithValueTaskDelegates()
    {
        // Given
        ValueResult result = ValueResult.Success();
        const string expected = "Success";

        // When
        string actual = await Task.FromResult(result).MatchAsync(SuccessAsync, FailureAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        async ValueTask<string> SuccessAsync() => await ValueTask.FromResult("Success");
        async ValueTask<string> FailureAsync(Exception exception) => await ValueTask.FromResult("Failure");
    }

    [Fact(DisplayName = "Task<ValueResult>.MatchAsync<TResult> of Failure should return the result from failure delegate (Func<Task<TResult>>, Func<Exception, Task<TResult>>>)")]
    public async Task TaskValueResultMatchAsyncOfFailureShouldReturnResultFromFailureDelegateFuncTaskResultFuncExceptionTaskResult()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
        const string expected = "Failure";

        // When
        string actual = await Task.FromResult(result).MatchAsync(SuccessAsync, FailureAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        async Task<string> SuccessAsync() => await Task.FromResult("Success");
        async Task<string> FailureAsync(Exception exception) => await Task.FromResult("Failure");
    }

    [Fact(DisplayName = "Task<ValueResult>.MatchAsync<TResult> of Failure should return the result from failure delegate (Func<ValueTask<TResult>>, Func<Exception, ValueTask<TResult>>>)")]
    public async Task TaskValueResultMatchAsyncOfFailureShouldReturnResultFromFailureDelegateFuncTaskResultFuncExceptionTaskResultWithValueTaskDelegates()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
        const string expected = "Failure";

        // When
        string actual = await Task.FromResult(result).MatchAsync(SuccessAsync, FailureAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        async ValueTask<string> SuccessAsync() => await ValueTask.FromResult("Success");
        async ValueTask<string> FailureAsync(Exception exception) => await ValueTask.FromResult("Failure");
    }

    [Fact(DisplayName = "Task<ValueResult>.MatchAsync<TResult> of Success should return the result from success delegate (Func<CancellationToken, Task<TResult>>, Func<Exception, CancellationToken, Task<TResult>>>)")]
    public async Task TaskValueResultMatchAsyncOfSuccessShouldReturnResultFromSuccessDelegateFuncCancellationTokenTaskResultFuncExceptionCancellationTokenTaskResult()
    {
        // Given
        ValueResult result = ValueResult.Success();
        const string expected = "Success";

        // When
        string actual = await Task.FromResult(result).MatchAsync(SuccessAsync, FailureAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        async Task<string> SuccessAsync(CancellationToken ct) => await Task.FromResult("Success");
        async Task<string> FailureAsync(Exception exception, CancellationToken ct) => await Task.FromResult("Failure");
    }

    [Fact(DisplayName = "Task<ValueResult>.MatchAsync<TResult> of Success should return the result from success delegate (Func<CancellationToken, ValueTask<TResult>>, Func<Exception, CancellationToken, ValueTask<TResult>>>)")]
    public async Task TaskValueResultMatchAsyncOfSuccessShouldReturnResultFromSuccessDelegateFuncCancellationTokenTaskResultFuncExceptionCancellationTokenTaskResultWithValueTaskDelegates()
    {
        // Given
        ValueResult result = ValueResult.Success();
        const string expected = "Success";

        // When
        string actual = await Task.FromResult(result).MatchAsync(SuccessAsync, FailureAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        async ValueTask<string> SuccessAsync(CancellationToken ct) => await ValueTask.FromResult("Success");
        async ValueTask<string> FailureAsync(Exception exception, CancellationToken ct) => await ValueTask.FromResult("Failure");
    }

    [Fact(DisplayName = "Task<ValueResult>.MatchAsync<TResult> of Failure should return the result from failure delegate (Func<CancellationToken, Task<TResult>>, Func<Exception, CancellationToken, Task<TResult>>>)")]
    public async Task TaskValueResultMatchAsyncOfFailureShouldReturnResultFromFailureDelegateFuncCancellationTokenTaskResultFuncExceptionCancellationTokenTaskResult()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
        const string expected = "Failure";

        // When
        string actual = await Task.FromResult(result).MatchAsync(SuccessAsync, FailureAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        async Task<string> SuccessAsync(CancellationToken ct) => await Task.FromResult("Success");
        async Task<string> FailureAsync(Exception exception, CancellationToken ct) => await Task.FromResult("Failure");
    }

    [Fact(DisplayName = "Task<ValueResult>.MatchAsync<TResult> of Failure should return the result from failure delegate (Func<CancellationToken, ValueTask<TResult>>, Func<Exception, CancellationToken, ValueTask<TResult>>>)")]
    public async Task TaskValueResultMatchAsyncOfFailureShouldReturnResultFromFailureDelegateFuncCancellationTokenTaskResultFuncExceptionCancellationTokenTaskResultWithValueTaskDelegates()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
        const string expected = "Failure";

        // When
        string actual = await Task.FromResult(result).MatchAsync(SuccessAsync, FailureAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        async ValueTask<string> SuccessAsync(CancellationToken ct) => await ValueTask.FromResult("Success");
        async ValueTask<string> FailureAsync(Exception exception, CancellationToken ct) => await ValueTask.FromResult("Failure");
    }

    [Fact(DisplayName = "Task<ValueResult<T>>.MatchAsync of Success should invoke the success delegate (Action<T>, Action<Exception>)")]
    public async Task TaskValueResultOfTMatchAsyncOfSuccessShouldInvokeSuccessDelegateActionTActionException()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(42);
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await Task.FromResult(result).MatchAsync(Success, Failure, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(successCalled);
        Assert.False(failureCalled);

        return;
        void Success(int value) => successCalled = value == 42;
        void Failure(Exception exception) => failureCalled = true;
    }

    [Fact(DisplayName = "Task<ValueResult<T>>.MatchAsync of Failure should invoke the failure delegate (Action<T>, Action<Exception>)")]
    public async Task TaskValueResultOfTMatchAsyncOfFailureShouldInvokeFailureDelegateActionTActionException()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await Task.FromResult(result).MatchAsync(Success, Failure, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.False(successCalled);
        Assert.True(failureCalled);

        return;
        void Success(int value) => successCalled = true;
        void Failure(Exception exception) => failureCalled = true;
    }

    [Fact(DisplayName = "Task<ValueResult<T>>.MatchAsync of Success should invoke the success delegate (Func<T, Task>, Action<Exception>)")]
    public async Task TaskValueResultOfTMatchAsyncOfSuccessShouldInvokeSuccessDelegateFuncTTaskActionException()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(42);
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await Task.FromResult(result).MatchAsync(SuccessAsync, Failure, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(successCalled);
        Assert.False(failureCalled);

        return;
        async Task SuccessAsync(int value) => await Task.Run(() => successCalled = value == 42);
        void Failure(Exception exception) => failureCalled = true;
    }

    [Fact(DisplayName = "Task<ValueResult<T>>.MatchAsync of Success should invoke the success delegate (Func<T, ValueTask>, Action<Exception>)")]
    public async Task TaskValueResultOfTMatchAsyncOfSuccessShouldInvokeSuccessDelegateFuncTTaskActionExceptionWithValueTaskDelegates()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(42);
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await Task.FromResult(result).MatchAsync(SuccessAsync, Failure, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(successCalled);
        Assert.False(failureCalled);

        return;
        async ValueTask SuccessAsync(int value) => await Task.Run(() => successCalled = value == 42);
        void Failure(Exception exception) => failureCalled = true;
    }

    [Fact(DisplayName = "Task<ValueResult<T>>.MatchAsync of Failure should invoke the failure delegate (Func<T, Task>, Action<Exception>)")]
    public async Task TaskValueResultOfTMatchAsyncOfFailureShouldInvokeFailureDelegateFuncTTaskActionException()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await Task.FromResult(result).MatchAsync(SuccessAsync, Failure, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.False(successCalled);
        Assert.True(failureCalled);

        return;
        async Task SuccessAsync(int value) => await Task.Run(() => successCalled = true);
        void Failure(Exception exception) => failureCalled = true;
    }

    [Fact(DisplayName = "Task<ValueResult<T>>.MatchAsync of Failure should invoke the failure delegate (Func<T, ValueTask>, Action<Exception>)")]
    public async Task TaskValueResultOfTMatchAsyncOfFailureShouldInvokeFailureDelegateFuncTTaskActionExceptionWithValueTaskDelegates()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await Task.FromResult(result).MatchAsync(SuccessAsync, Failure, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.False(successCalled);
        Assert.True(failureCalled);

        return;
        async ValueTask SuccessAsync(int value) => await Task.Run(() => successCalled = true);
        void Failure(Exception exception) => failureCalled = true;
    }

    [Fact(DisplayName = "Task<ValueResult<T>>.MatchAsync of Success should invoke the success delegate (Func<T, CancellationToken, Task>, Action<Exception>)")]
    public async Task TaskValueResultOfTMatchAsyncOfSuccessShouldInvokeSuccessDelegateFuncTCancellationTokenTaskActionException()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(42);
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await Task.FromResult(result).MatchAsync(SuccessAsync, Failure, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(successCalled);
        Assert.False(failureCalled);

        return;
        async Task SuccessAsync(int value, CancellationToken ct) => await Task.Run(() => successCalled = value == 42, ct);
        void Failure(Exception exception) => failureCalled = true;
    }

    [Fact(DisplayName = "Task<ValueResult<T>>.MatchAsync of Success should invoke the success delegate (Func<T, CancellationToken, ValueTask>, Action<Exception>)")]
    public async Task TaskValueResultOfTMatchAsyncOfSuccessShouldInvokeSuccessDelegateFuncTCancellationTokenTaskActionExceptionWithValueTaskDelegates()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(42);
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await Task.FromResult(result).MatchAsync(SuccessAsync, Failure, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(successCalled);
        Assert.False(failureCalled);

        return;
        async ValueTask SuccessAsync(int value, CancellationToken ct) => await Task.Run(() => successCalled = value == 42, ct);
        void Failure(Exception exception) => failureCalled = true;
    }

    [Fact(DisplayName = "Task<ValueResult<T>>.MatchAsync of Failure should invoke the failure delegate (Func<T, CancellationToken, Task>, Action<Exception>)")]
    public async Task TaskValueResultOfTMatchAsyncOfFailureShouldInvokeFailureDelegateFuncTCancellationTokenTaskActionException()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await Task.FromResult(result).MatchAsync(SuccessAsync, Failure, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.False(successCalled);
        Assert.True(failureCalled);

        return;
        async Task SuccessAsync(int value, CancellationToken ct) => await Task.Run(() => successCalled = true, ct);
        void Failure(Exception exception) => failureCalled = true;
    }

    [Fact(DisplayName = "Task<ValueResult<T>>.MatchAsync of Failure should invoke the failure delegate (Func<T, CancellationToken, ValueTask>, Action<Exception>)")]
    public async Task TaskValueResultOfTMatchAsyncOfFailureShouldInvokeFailureDelegateFuncTCancellationTokenTaskActionExceptionWithValueTaskDelegates()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await Task.FromResult(result).MatchAsync(SuccessAsync, Failure, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.False(successCalled);
        Assert.True(failureCalled);

        return;
        async ValueTask SuccessAsync(int value, CancellationToken ct) => await Task.Run(() => successCalled = true, ct);
        void Failure(Exception exception) => failureCalled = true;
    }

    [Fact(DisplayName = "Task<ValueResult<T>>.MatchAsync of Success should invoke the success delegate (Action<T>, Func<Exception, Task>)")]
    public async Task TaskValueResultOfTMatchAsyncOfSuccessShouldInvokeSuccessDelegateActionTFuncExceptionTask()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(42);
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await Task.FromResult(result).MatchAsync(Success, FailureAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(successCalled);
        Assert.False(failureCalled);

        return;
        void Success(int value) => successCalled = value == 42;
        async Task FailureAsync(Exception exception) => await Task.Run(() => failureCalled = true);
    }

    [Fact(DisplayName = "Task<ValueResult<T>>.MatchAsync of Success should invoke the success delegate (Action<T>, Func<Exception, ValueTask>)")]
    public async Task TaskValueResultOfTMatchAsyncOfSuccessShouldInvokeSuccessDelegateActionTFuncExceptionTaskWithValueTaskDelegates()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(42);
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await Task.FromResult(result).MatchAsync(Success, FailureAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(successCalled);
        Assert.False(failureCalled);

        return;
        void Success(int value) => successCalled = value == 42;
        async ValueTask FailureAsync(Exception exception) => await Task.Run(() => failureCalled = true);
    }

    [Fact(DisplayName = "Task<ValueResult<T>>.MatchAsync of Failure should invoke the failure delegate (Action<T>, Func<Exception, Task>)")]
    public async Task TaskValueResultOfTMatchAsyncOfFailureShouldInvokeFailureDelegateActionTFuncExceptionTask()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await Task.FromResult(result).MatchAsync(Success, FailureAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.False(successCalled);
        Assert.True(failureCalled);

        return;
        void Success(int value) => successCalled = true;
        async Task FailureAsync(Exception exception) => await Task.Run(() => failureCalled = true);
    }

    [Fact(DisplayName = "Task<ValueResult<T>>.MatchAsync of Failure should invoke the failure delegate (Action<T>, Func<Exception, ValueTask>)")]
    public async Task TaskValueResultOfTMatchAsyncOfFailureShouldInvokeFailureDelegateActionTFuncExceptionTaskWithValueTaskDelegates()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await Task.FromResult(result).MatchAsync(Success, FailureAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.False(successCalled);
        Assert.True(failureCalled);

        return;
        void Success(int value) => successCalled = true;
        async ValueTask FailureAsync(Exception exception) => await Task.Run(() => failureCalled = true);
    }

    [Fact(DisplayName = "Task<ValueResult<T>>.MatchAsync of Success should invoke the success delegate (Action<T>, Func<Exception, CancellationToken, Task>)")]
    public async Task TaskValueResultOfTMatchAsyncOfSuccessShouldInvokeSuccessDelegateActionTFuncExceptionCancellationTokenTask()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(42);
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await Task.FromResult(result).MatchAsync(Success, FailureAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(successCalled);
        Assert.False(failureCalled);

        return;
        void Success(int value) => successCalled = value == 42;
        async Task FailureAsync(Exception exception, CancellationToken ct) => await Task.Run(() => failureCalled = true, ct);
    }

    [Fact(DisplayName = "Task<ValueResult<T>>.MatchAsync of Success should invoke the success delegate (Action<T>, Func<Exception, CancellationToken, ValueTask>)")]
    public async Task TaskValueResultOfTMatchAsyncOfSuccessShouldInvokeSuccessDelegateActionTFuncExceptionCancellationTokenTaskWithValueTaskDelegates()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(42);
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await Task.FromResult(result).MatchAsync(Success, FailureAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(successCalled);
        Assert.False(failureCalled);

        return;
        void Success(int value) => successCalled = value == 42;
        async ValueTask FailureAsync(Exception exception, CancellationToken ct) => await Task.Run(() => failureCalled = true, ct);
    }

    [Fact(DisplayName = "Task<ValueResult<T>>.MatchAsync of Failure should invoke the failure delegate (Action<T>, Func<Exception, CancellationToken, Task>)")]
    public async Task TaskValueResultOfTMatchAsyncOfFailureShouldInvokeFailureDelegateActionTFuncExceptionCancellationTokenTask()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await Task.FromResult(result).MatchAsync(Success, FailureAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.False(successCalled);
        Assert.True(failureCalled);

        return;
        void Success(int value) => successCalled = true;
        async Task FailureAsync(Exception exception, CancellationToken ct) => await Task.Run(() => failureCalled = true, ct);
    }

    [Fact(DisplayName = "Task<ValueResult<T>>.MatchAsync of Failure should invoke the failure delegate (Action<T>, Func<Exception, CancellationToken, ValueTask>)")]
    public async Task TaskValueResultOfTMatchAsyncOfFailureShouldInvokeFailureDelegateActionTFuncExceptionCancellationTokenTaskWithValueTaskDelegates()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await Task.FromResult(result).MatchAsync(Success, FailureAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.False(successCalled);
        Assert.True(failureCalled);

        return;
        void Success(int value) => successCalled = true;
        async ValueTask FailureAsync(Exception exception, CancellationToken ct) => await Task.Run(() => failureCalled = true, ct);
    }

    [Fact(DisplayName = "Task<ValueResult<T>>.MatchAsync of Success should invoke the success delegate (Func<T, Task>, Func<Exception, Task>)")]
    public async Task TaskValueResultOfTMatchAsyncOfSuccessShouldInvokeSuccessDelegateFuncTTaskFuncExceptionTask()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(42);
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await Task.FromResult(result).MatchAsync(SuccessAsync, FailureAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(successCalled);
        Assert.False(failureCalled);

        return;
        async Task SuccessAsync(int value) => await Task.Run(() => successCalled = value == 42);
        async Task FailureAsync(Exception exception) => await Task.Run(() => failureCalled = true);
    }

    [Fact(DisplayName = "Task<ValueResult<T>>.MatchAsync of Success should invoke the success delegate (Func<T, ValueTask>, Func<Exception, ValueTask>)")]
    public async Task TaskValueResultOfTMatchAsyncOfSuccessShouldInvokeSuccessDelegateFuncTTaskFuncExceptionTaskWithValueTaskDelegates()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(42);
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await Task.FromResult(result).MatchAsync(SuccessAsync, FailureAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(successCalled);
        Assert.False(failureCalled);

        return;
        async ValueTask SuccessAsync(int value) => await Task.Run(() => successCalled = value == 42);
        async ValueTask FailureAsync(Exception exception) => await Task.Run(() => failureCalled = true);
    }

    [Fact(DisplayName = "Task<ValueResult<T>>.MatchAsync of Failure should invoke the failure delegate (Func<T, Task>, Func<Exception, Task>)")]
    public async Task TaskValueResultOfTMatchAsyncOfFailureShouldInvokeFailureDelegateFuncTTaskFuncExceptionTask()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await Task.FromResult(result).MatchAsync(SuccessAsync, FailureAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.False(successCalled);
        Assert.True(failureCalled);

        return;
        async Task SuccessAsync(int value) => await Task.Run(() => successCalled = true);
        async Task FailureAsync(Exception exception) => await Task.Run(() => failureCalled = true);
    }

    [Fact(DisplayName = "Task<ValueResult<T>>.MatchAsync of Failure should invoke the failure delegate (Func<T, ValueTask>, Func<Exception, ValueTask>)")]
    public async Task TaskValueResultOfTMatchAsyncOfFailureShouldInvokeFailureDelegateFuncTTaskFuncExceptionTaskWithValueTaskDelegates()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await Task.FromResult(result).MatchAsync(SuccessAsync, FailureAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.False(successCalled);
        Assert.True(failureCalled);

        return;
        async ValueTask SuccessAsync(int value) => await Task.Run(() => successCalled = true);
        async ValueTask FailureAsync(Exception exception) => await Task.Run(() => failureCalled = true);
    }

    [Fact(DisplayName = "Task<ValueResult<T>>.MatchAsync of Success should invoke the success delegate (Func<T, CancellationToken, Task>, Func<Exception, CancellationToken, Task>)")]
    public async Task TaskValueResultOfTMatchAsyncOfSuccessShouldInvokeSuccessDelegateFuncTCancellationTokenTaskFuncExceptionCancellationTokenTask()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(42);
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await Task.FromResult(result).MatchAsync(SuccessAsync, FailureAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(successCalled);
        Assert.False(failureCalled);

        return;
        async Task SuccessAsync(int value, CancellationToken ct) => await Task.Run(() => successCalled = value == 42, ct);
        async Task FailureAsync(Exception exception, CancellationToken ct) => await Task.Run(() => failureCalled = true, ct);
    }

    [Fact(DisplayName = "Task<ValueResult<T>>.MatchAsync of Success should invoke the success delegate (Func<T, CancellationToken, ValueTask>, Func<Exception, CancellationToken, ValueTask>)")]
    public async Task TaskValueResultOfTMatchAsyncOfSuccessShouldInvokeSuccessDelegateFuncTCancellationTokenTaskFuncExceptionCancellationTokenTaskWithValueTaskDelegates()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(42);
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await Task.FromResult(result).MatchAsync(SuccessAsync, FailureAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(successCalled);
        Assert.False(failureCalled);

        return;
        async ValueTask SuccessAsync(int value, CancellationToken ct) => await Task.Run(() => successCalled = value == 42, ct);
        async ValueTask FailureAsync(Exception exception, CancellationToken ct) => await Task.Run(() => failureCalled = true, ct);
    }

    [Fact(DisplayName = "Task<ValueResult<T>>.MatchAsync of Failure should invoke the failure delegate (Func<T, CancellationToken, Task>, Func<Exception, CancellationToken, Task>)")]
    public async Task TaskValueResultOfTMatchAsyncOfFailureShouldInvokeFailureDelegateFuncTCancellationTokenTaskFuncExceptionCancellationTokenTask()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await Task.FromResult(result).MatchAsync(SuccessAsync, FailureAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.False(successCalled);
        Assert.True(failureCalled);

        return;
        async Task SuccessAsync(int value, CancellationToken ct) => await Task.Run(() => successCalled = true, ct);
        async Task FailureAsync(Exception exception, CancellationToken ct) => await Task.Run(() => failureCalled = true, ct);
    }

    [Fact(DisplayName = "Task<ValueResult<T>>.MatchAsync of Failure should invoke the failure delegate (Func<T, CancellationToken, ValueTask>, Func<Exception, CancellationToken, ValueTask>)")]
    public async Task TaskValueResultOfTMatchAsyncOfFailureShouldInvokeFailureDelegateFuncTCancellationTokenTaskFuncExceptionCancellationTokenTaskWithValueTaskDelegates()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        bool successCalled = false;
        bool failureCalled = false;

        // When
        await Task.FromResult(result).MatchAsync(SuccessAsync, FailureAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.False(successCalled);
        Assert.True(failureCalled);

        return;
        async ValueTask SuccessAsync(int value, CancellationToken ct) => await Task.Run(() => successCalled = true, ct);
        async ValueTask FailureAsync(Exception exception, CancellationToken ct) => await Task.Run(() => failureCalled = true, ct);
    }

    [Fact(DisplayName = "Task<ValueResult<T>>.MatchAsync<TResult> of Success should return result from success delegate (Func<T, TResult>, Func<Exception, TResult>)")]
    public async Task TaskValueResultOfTMatchAsyncOfSuccessShouldReturnResultFromSuccessDelegateFuncTResultFuncExceptionResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(42);
        const string expected = "Success";

        // When
        string actual = await Task.FromResult(result).MatchAsync(Success, Failure, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        string Success(int value) => value == 42 ? "Success" : "Failure";
        string Failure(Exception exception) => "Failure";
    }

    [Fact(DisplayName = "Task<ValueResult<T>>.MatchAsync<TResult> of Failure should return result from failure delegate (Func<T, TResult>, Func<Exception, TResult>)")]
    public async Task TaskValueResultOfTMatchAsyncOfFailureShouldReturnResultFromFailureDelegateFuncTResultFuncExceptionResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        const string expected = "Failure";

        // When
        string actual = await Task.FromResult(result).MatchAsync(Success, Failure, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        string Success(int value) => "Success";
        string Failure(Exception exception) => "Failure";
    }

    [Fact(DisplayName = "Task<ValueResult<T>>.MatchAsync<TResult> of Success should return result from success delegate (Func<T, Task<TResult>>, Func<Exception, TResult>)")]
    public async Task TaskValueResultOfTMatchAsyncOfSuccessShouldReturnResultFromSuccessDelegateFuncTTaskResultFuncExceptionResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(42);
        const string expected = "Success";

        // When
        string actual = await Task.FromResult(result).MatchAsync(SuccessAsync, Failure, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        async Task<string> SuccessAsync(int value) => await Task.FromResult(value == 42 ? "Success" : "Failure");
        string Failure(Exception exception) => "Failure";
    }

    [Fact(DisplayName = "Task<ValueResult<T>>.MatchAsync<TResult> of Success should return result from success delegate (Func<T, ValueTask<TResult>>, Func<Exception, TResult>)")]
    public async Task TaskValueResultOfTMatchAsyncOfSuccessShouldReturnResultFromSuccessDelegateFuncTTaskResultFuncExceptionResultWithValueTaskDelegates()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(42);
        const string expected = "Success";

        // When
        string actual = await Task.FromResult(result).MatchAsync(SuccessAsync, Failure, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        async ValueTask<string> SuccessAsync(int value) => await ValueTask.FromResult(value == 42 ? "Success" : "Failure");
        string Failure(Exception exception) => "Failure";
    }

    [Fact(DisplayName = "Task<ValueResult<T>>.MatchAsync<TResult> of Failure should return result from failure delegate (Func<T, Task<TResult>>, Func<Exception, TResult>)")]
    public async Task TaskValueResultOfTMatchAsyncOfFailureShouldReturnResultFromFailureDelegateFuncTTaskResultFuncExceptionResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        const string expected = "Failure";

        // When
        string actual = await Task.FromResult(result).MatchAsync(SuccessAsync, Failure, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        async Task<string> SuccessAsync(int value) => await Task.FromResult("Success");
        string Failure(Exception exception) => "Failure";
    }

    [Fact(DisplayName = "Task<ValueResult<T>>.MatchAsync<TResult> of Failure should return result from failure delegate (Func<T, ValueTask<TResult>>, Func<Exception, TResult>)")]
    public async Task TaskValueResultOfTMatchAsyncOfFailureShouldReturnResultFromFailureDelegateFuncTTaskResultFuncExceptionResultWithValueTaskDelegates()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        const string expected = "Failure";

        // When
        string actual = await Task.FromResult(result).MatchAsync(SuccessAsync, Failure, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        async ValueTask<string> SuccessAsync(int value) => await ValueTask.FromResult("Success");
        string Failure(Exception exception) => "Failure";
    }

    [Fact(DisplayName = "Task<ValueResult<T>>.MatchAsync<TResult> of Success should return result from success delegate (Func<T, CancellationToken, Task<TResult>>, Func<Exception, TResult>)")]
    public async Task TaskValueResultOfTMatchAsyncOfSuccessShouldReturnResultFromSuccessDelegateFuncTCancellationTokenTaskResultFuncExceptionResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(42);
        const string expected = "Success";

        // When
        string actual = await Task.FromResult(result).MatchAsync(SuccessAsync, Failure, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        async Task<string> SuccessAsync(int value, CancellationToken ct) => await Task.FromResult(value == 42 ? "Success" : "Failure");
        string Failure(Exception exception) => "Failure";
    }

    [Fact(DisplayName = "Task<ValueResult<T>>.MatchAsync<TResult> of Success should return result from success delegate (Func<T, CancellationToken, ValueTask<TResult>>, Func<Exception, TResult>)")]
    public async Task TaskValueResultOfTMatchAsyncOfSuccessShouldReturnResultFromSuccessDelegateFuncTCancellationTokenTaskResultFuncExceptionResultWithValueTaskDelegates()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(42);
        const string expected = "Success";

        // When
        string actual = await Task.FromResult(result).MatchAsync(SuccessAsync, Failure, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        async ValueTask<string> SuccessAsync(int value, CancellationToken ct) => await ValueTask.FromResult(value == 42 ? "Success" : "Failure");
        string Failure(Exception exception) => "Failure";
    }

    [Fact(DisplayName = "Task<ValueResult<T>>.MatchAsync<TResult> of Failure should return result from failure delegate (Func<T, CancellationToken, Task<TResult>>, Func<Exception, TResult>)")]
    public async Task TaskValueResultOfTMatchAsyncOfFailureShouldReturnResultFromFailureDelegateFuncTCancellationTokenTaskResultFuncExceptionResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        const string expected = "Failure";

        // When
        string actual = await Task.FromResult(result).MatchAsync(SuccessAsync, Failure, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        async Task<string> SuccessAsync(int value, CancellationToken ct) => await Task.FromResult("Success");
        string Failure(Exception exception) => "Failure";
    }

    [Fact(DisplayName = "Task<ValueResult<T>>.MatchAsync<TResult> of Failure should return result from failure delegate (Func<T, CancellationToken, ValueTask<TResult>>, Func<Exception, TResult>)")]
    public async Task TaskValueResultOfTMatchAsyncOfFailureShouldReturnResultFromFailureDelegateFuncTCancellationTokenTaskResultFuncExceptionResultWithValueTaskDelegates()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        const string expected = "Failure";

        // When
        string actual = await Task.FromResult(result).MatchAsync(SuccessAsync, Failure, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        async ValueTask<string> SuccessAsync(int value, CancellationToken ct) => await ValueTask.FromResult("Success");
        string Failure(Exception exception) => "Failure";
    }

    [Fact(DisplayName = "Task<ValueResult<T>>.MatchAsync<TResult> of Success should return result from success delegate (Func<T, TResult>, Func<Exception, Task<TResult>>>)")]
    public async Task TaskValueResultOfTMatchAsyncOfSuccessShouldReturnResultFromSuccessDelegateFuncTResultFuncExceptionTaskResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(42);
        const string expected = "Success";

        // When
        string actual = await Task.FromResult(result).MatchAsync(Success, FailureAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        string Success(int value) => value == 42 ? "Success" : "Failure";
        async Task<string> FailureAsync(Exception exception) => await Task.FromResult("Failure");
    }

    [Fact(DisplayName = "Task<ValueResult<T>>.MatchAsync<TResult> of Success should return result from success delegate (Func<T, TResult>, Func<Exception, ValueTask<TResult>>>)")]
    public async Task TaskValueResultOfTMatchAsyncOfSuccessShouldReturnResultFromSuccessDelegateFuncTResultFuncExceptionTaskResultWithValueTaskDelegates()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(42);
        const string expected = "Success";

        // When
        string actual = await Task.FromResult(result).MatchAsync(Success, FailureAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        string Success(int value) => value == 42 ? "Success" : "Failure";
        async ValueTask<string> FailureAsync(Exception exception) => await ValueTask.FromResult("Failure");
    }

    [Fact(DisplayName = "Task<ValueResult<T>>.MatchAsync<TResult> of Failure should return result from failure delegate (Func<T, TResult>, Func<Exception, Task<TResult>>>)")]
    public async Task TaskValueResultOfTMatchAsyncOfFailureShouldReturnResultFromFailureDelegateFuncTResultFuncExceptionTaskResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        const string expected = "Failure";

        // When
        string actual = await Task.FromResult(result).MatchAsync(Success, FailureAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        string Success(int value) => "Success";
        async Task<string> FailureAsync(Exception exception) => await Task.FromResult("Failure");
    }

    [Fact(DisplayName = "Task<ValueResult<T>>.MatchAsync<TResult> of Failure should return result from failure delegate (Func<T, TResult>, Func<Exception, ValueTask<TResult>>>)")]
    public async Task TaskValueResultOfTMatchAsyncOfFailureShouldReturnResultFromFailureDelegateFuncTResultFuncExceptionTaskResultWithValueTaskDelegates()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        const string expected = "Failure";

        // When
        string actual = await Task.FromResult(result).MatchAsync(Success, FailureAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        string Success(int value) => "Success";
        async ValueTask<string> FailureAsync(Exception exception) => await ValueTask.FromResult("Failure");
    }

    [Fact(DisplayName = "Task<ValueResult<T>>.MatchAsync<TResult> of Success should return result from success delegate (Func<T, TResult>, Func<Exception, CancellationToken, Task<TResult>>>)")]
    public async Task TaskValueResultOfTMatchAsyncOfSuccessShouldReturnResultFromSuccessDelegateFuncTResultFuncExceptionCancellationTokenTaskResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(42);
        const string expected = "Success";

        // When
        string actual = await Task.FromResult(result).MatchAsync(Success, FailureAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        string Success(int value) => value == 42 ? "Success" : "Failure";
        async Task<string> FailureAsync(Exception exception, CancellationToken ct) => await Task.FromResult("Failure");
    }

    [Fact(DisplayName = "Task<ValueResult<T>>.MatchAsync<TResult> of Success should return result from success delegate (Func<T, TResult>, Func<Exception, CancellationToken, ValueTask<TResult>>>)")]
    public async Task TaskValueResultOfTMatchAsyncOfSuccessShouldReturnResultFromSuccessDelegateFuncTResultFuncExceptionCancellationTokenTaskResultWithValueTaskDelegates()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(42);
        const string expected = "Success";

        // When
        string actual = await Task.FromResult(result).MatchAsync(Success, FailureAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        string Success(int value) => value == 42 ? "Success" : "Failure";
        async ValueTask<string> FailureAsync(Exception exception, CancellationToken ct) => await ValueTask.FromResult("Failure");
    }

    [Fact(DisplayName = "Task<ValueResult<T>>.MatchAsync<TResult> of Failure should return result from failure delegate (Func<T, TResult>, Func<Exception, CancellationToken, Task<TResult>>>)")]
    public async Task TaskValueResultOfTMatchAsyncOfFailureShouldReturnResultFromFailureDelegateFuncTResultFuncExceptionCancellationTokenTaskResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        const string expected = "Failure";

        // When
        string actual = await Task.FromResult(result).MatchAsync(Success, FailureAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        string Success(int value) => "Success";
        async Task<string> FailureAsync(Exception exception, CancellationToken ct) => await Task.FromResult("Failure");
    }

    [Fact(DisplayName = "Task<ValueResult<T>>.MatchAsync<TResult> of Failure should return result from failure delegate (Func<T, TResult>, Func<Exception, CancellationToken, ValueTask<TResult>>>)")]
    public async Task TaskValueResultOfTMatchAsyncOfFailureShouldReturnResultFromFailureDelegateFuncTResultFuncExceptionCancellationTokenTaskResultWithValueTaskDelegates()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        const string expected = "Failure";

        // When
        string actual = await Task.FromResult(result).MatchAsync(Success, FailureAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        string Success(int value) => "Success";
        async ValueTask<string> FailureAsync(Exception exception, CancellationToken ct) => await ValueTask.FromResult("Failure");
    }

    [Fact(DisplayName = "Task<ValueResult<T>>.MatchAsync<TResult> of Success should return result from success delegate (Func<T, Task<TResult>>, Func<Exception, Task<TResult>>>)")]
    public async Task TaskValueResultOfTMatchAsyncOfSuccessShouldReturnResultFromSuccessDelegateFuncTTaskResultFuncExceptionTaskResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(42);
        const string expected = "Success";

        // When
        string actual = await Task.FromResult(result).MatchAsync(SuccessAsync, FailureAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        async Task<string> SuccessAsync(int value) => await Task.FromResult(value == 42 ? "Success" : "Failure");
        async Task<string> FailureAsync(Exception exception) => await Task.FromResult("Failure");
    }

    [Fact(DisplayName = "Task<ValueResult<T>>.MatchAsync<TResult> of Success should return result from success delegate (Func<T, ValueTask<TResult>>, Func<Exception, ValueTask<TResult>>>)")]
    public async Task TaskValueResultOfTMatchAsyncOfSuccessShouldReturnResultFromSuccessDelegateFuncTTaskResultFuncExceptionTaskResultWithValueTaskDelegates()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(42);
        const string expected = "Success";

        // When
        string actual = await Task.FromResult(result).MatchAsync(SuccessAsync, FailureAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        async ValueTask<string> SuccessAsync(int value) => await ValueTask.FromResult(value == 42 ? "Success" : "Failure");
        async ValueTask<string> FailureAsync(Exception exception) => await ValueTask.FromResult("Failure");
    }

    [Fact(DisplayName = "Task<ValueResult<T>>.MatchAsync<TResult> of Failure should return result from failure delegate (Func<T, Task<TResult>>, Func<Exception, Task<TResult>>>)")]
    public async Task TaskValueResultOfTMatchAsyncOfFailureShouldReturnResultFromFailureDelegateFuncTTaskResultFuncExceptionTaskResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        const string expected = "Failure";

        // When
        string actual = await Task.FromResult(result).MatchAsync(SuccessAsync, FailureAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        async Task<string> SuccessAsync(int value) => await Task.FromResult("Success");
        async Task<string> FailureAsync(Exception exception) => await Task.FromResult("Failure");
    }

    [Fact(DisplayName = "Task<ValueResult<T>>.MatchAsync<TResult> of Failure should return result from failure delegate (Func<T, ValueTask<TResult>>, Func<Exception, ValueTask<TResult>>>)")]
    public async Task TaskValueResultOfTMatchAsyncOfFailureShouldReturnResultFromFailureDelegateFuncTTaskResultFuncExceptionTaskResultWithValueTaskDelegates()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        const string expected = "Failure";

        // When
        string actual = await Task.FromResult(result).MatchAsync(SuccessAsync, FailureAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        async ValueTask<string> SuccessAsync(int value) => await ValueTask.FromResult("Success");
        async ValueTask<string> FailureAsync(Exception exception) => await ValueTask.FromResult("Failure");
    }

    [Fact(DisplayName = "Task<ValueResult<T>>.MatchAsync<TResult> of Success should return result from success delegate (Func<T, CancellationToken, Task<TResult>>, Func<Exception, CancellationToken, Task<TResult>>>)")]
    public async Task TaskValueResultOfTMatchAsyncOfSuccessShouldReturnResultFromSuccessDelegateFuncTCancellationTokenTaskResultFuncExceptionCancellationTokenTaskResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(42);
        const string expected = "Success";

        // When
        string actual = await Task.FromResult(result).MatchAsync(SuccessAsync, FailureAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        async Task<string> SuccessAsync(int value, CancellationToken ct) => await Task.FromResult(value == 42 ? "Success" : "Failure");
        async Task<string> FailureAsync(Exception exception, CancellationToken ct) => await Task.FromResult("Failure");
    }

    [Fact(DisplayName = "Task<ValueResult<T>>.MatchAsync<TResult> of Success should return result from success delegate (Func<T, CancellationToken, ValueTask<TResult>>, Func<Exception, CancellationToken, ValueTask<TResult>>>)")]
    public async Task TaskValueResultOfTMatchAsyncOfSuccessShouldReturnResultFromSuccessDelegateFuncTCancellationTokenTaskResultFuncExceptionCancellationTokenTaskResultWithValueTaskDelegates()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(42);
        const string expected = "Success";

        // When
        string actual = await Task.FromResult(result).MatchAsync(SuccessAsync, FailureAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        async ValueTask<string> SuccessAsync(int value, CancellationToken ct) => await ValueTask.FromResult(value == 42 ? "Success" : "Failure");
        async ValueTask<string> FailureAsync(Exception exception, CancellationToken ct) => await ValueTask.FromResult("Failure");
    }

    [Fact(DisplayName = "Task<ValueResult<T>>.MatchAsync<TResult> of Failure should return result from failure delegate (Func<T, CancellationToken, Task<TResult>>, Func<Exception, CancellationToken, Task<TResult>>>)")]
    public async Task TaskValueResultOfTMatchAsyncOfFailureShouldReturnResultFromFailureDelegateFuncTCancellationTokenTaskResultFuncExceptionCancellationTokenTaskResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        const string expected = "Failure";

        // When
        string actual = await Task.FromResult(result).MatchAsync(SuccessAsync, FailureAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        async Task<string> SuccessAsync(int value, CancellationToken ct) => await Task.FromResult("Success");
        async Task<string> FailureAsync(Exception exception, CancellationToken ct) => await Task.FromResult("Failure");
    }

    [Fact(DisplayName = "Task<ValueResult<T>>.MatchAsync<TResult> of Failure should return result from failure delegate (Func<T, CancellationToken, ValueTask<TResult>>, Func<Exception, CancellationToken, ValueTask<TResult>>>)")]
    public async Task TaskValueResultOfTMatchAsyncOfFailureShouldReturnResultFromFailureDelegateFuncTCancellationTokenTaskResultFuncExceptionCancellationTokenTaskResultWithValueTaskDelegates()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        const string expected = "Failure";

        // When
        string actual = await Task.FromResult(result).MatchAsync(SuccessAsync, FailureAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(expected, actual);

        return;
        async ValueTask<string> SuccessAsync(int value, CancellationToken ct) => await ValueTask.FromResult("Success");
        async ValueTask<string> FailureAsync(Exception exception, CancellationToken ct) => await ValueTask.FromResult("Failure");
    }

    [Fact(DisplayName = "Task<ValueResult>.SelectAsync of Success should invoke the selector delegate (Action)")]
    public async Task TaskValueResultSelectAsyncOfSuccessShouldInvokeSelectorDelegateAction()
    {
        // Given
        ValueResult result = ValueResult.Success();
        bool selectorInvoked = false;

        // When
        ValueResult actual = await Task.FromResult(result).SelectAsync(Selector, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(actual.IsSuccess);
        Assert.True(selectorInvoked);

        return;
        void Selector() => selectorInvoked = true;
    }

    [Fact(DisplayName = "Task<ValueResult>.SelectAsync of Failure should not invoke the selector delegate (Action)")]
    public async Task TaskValueResultSelectAsyncOfFailureShouldNotInvokeSelectorDelegateAction()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
        bool selectorInvoked = false;

        // When
        ValueResult actual = await Task.FromResult(result).SelectAsync(Selector, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(actual.IsFailure);
        Assert.Equal(FailureException, actual.GetExceptionOrThrow());
        Assert.False(selectorInvoked);

        return;
        void Selector() => selectorInvoked = true;
    }

    [Fact(DisplayName = "Task<ValueResult>.SelectAsync of Success should invoke the selector delegate (Func<Task>)")]
    public async Task TaskValueResultSelectAsyncOfSuccessShouldInvokeSelectorDelegateFuncTask()
    {
        // Given
        ValueResult result = ValueResult.Success();
        bool selectorInvoked = false;

        // When
        ValueResult actual = await Task.FromResult(result).SelectAsync(SelectorAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(actual.IsSuccess);
        Assert.True(selectorInvoked);

        return;
        async Task SelectorAsync() => await Task.Run(() => selectorInvoked = true);
    }

    [Fact(DisplayName = "Task<ValueResult>.SelectAsync of Success should invoke the selector delegate (Func<ValueTask>)")]
    public async Task TaskValueResultSelectAsyncOfSuccessShouldInvokeSelectorDelegateFuncTaskWithValueTaskDelegates()
    {
        // Given
        ValueResult result = ValueResult.Success();
        bool selectorInvoked = false;

        // When
        ValueResult actual = await Task.FromResult(result).SelectAsync(SelectorAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(actual.IsSuccess);
        Assert.True(selectorInvoked);

        return;
        async ValueTask SelectorAsync() => await Task.Run(() => selectorInvoked = true);
    }

    [Fact(DisplayName = "Task<ValueResult>.SelectAsync of Failure should not invoke the selector delegate (Func<Task>)")]
    public async Task TaskValueResultSelectAsyncOfFailureShouldNotInvokeSelectorDelegateFuncTask()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
        bool selectorInvoked = false;

        // When
        ValueResult actual = await Task.FromResult(result).SelectAsync(SelectorAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(actual.IsFailure);
        Assert.Equal(FailureException, actual.GetExceptionOrThrow());
        Assert.False(selectorInvoked);

        return;
        async Task SelectorAsync() => await Task.Run(() => selectorInvoked = true);
    }

    [Fact(DisplayName = "Task<ValueResult>.SelectAsync of Failure should not invoke the selector delegate (Func<ValueTask>)")]
    public async Task TaskValueResultSelectAsyncOfFailureShouldNotInvokeSelectorDelegateFuncTaskWithValueTaskDelegates()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
        bool selectorInvoked = false;

        // When
        ValueResult actual = await Task.FromResult(result).SelectAsync(SelectorAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(actual.IsFailure);
        Assert.Equal(FailureException, actual.GetExceptionOrThrow());
        Assert.False(selectorInvoked);

        return;
        async ValueTask SelectorAsync() => await Task.Run(() => selectorInvoked = true);
    }

    [Fact(DisplayName = "Task<ValueResult>.SelectAsync of Success should invoke the selector delegate (Func<CancellationToken, Task>)")]
    public async Task TaskValueResultSelectAsyncOfSuccessShouldInvokeSelectorDelegateFuncCancellationTokenTask()
    {
        // Given
        ValueResult result = ValueResult.Success();
        bool selectorInvoked = false;

        // When
        ValueResult actual = await Task.FromResult(result).SelectAsync(SelectorAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(actual.IsSuccess);
        Assert.True(selectorInvoked);

        return;
        async Task SelectorAsync(CancellationToken ct) => await Task.Run(() => selectorInvoked = true, ct);
    }

    [Fact(DisplayName = "Task<ValueResult>.SelectAsync of Success should invoke the selector delegate (Func<CancellationToken, ValueTask>)")]
    public async Task TaskValueResultSelectAsyncOfSuccessShouldInvokeSelectorDelegateFuncCancellationTokenTaskWithValueTaskDelegates()
    {
        // Given
        ValueResult result = ValueResult.Success();
        bool selectorInvoked = false;

        // When
        ValueResult actual = await Task.FromResult(result).SelectAsync(SelectorAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(actual.IsSuccess);
        Assert.True(selectorInvoked);

        return;
        async ValueTask SelectorAsync(CancellationToken ct) => await Task.Run(() => selectorInvoked = true, ct);
    }

    [Fact(DisplayName = "Task<ValueResult>.SelectAsync of Failure should not invoke the selector delegate (Func<CancellationToken, Task>)")]
    public async Task TaskValueResultSelectAsyncOfFailureShouldNotInvokeSelectorDelegateFuncCancellationTokenTask()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
        bool selectorInvoked = false;

        // When
        ValueResult actual = await Task.FromResult(result).SelectAsync(SelectorAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(actual.IsFailure);
        Assert.Equal(FailureException, actual.GetExceptionOrThrow());
        Assert.False(selectorInvoked);

        return;
        async Task SelectorAsync(CancellationToken ct) => await Task.Run(() => selectorInvoked = true, ct);
    }

    [Fact(DisplayName = "Task<ValueResult>.SelectAsync of Failure should not invoke the selector delegate (Func<CancellationToken, ValueTask>)")]
    public async Task TaskValueResultSelectAsyncOfFailureShouldNotInvokeSelectorDelegateFuncCancellationTokenTaskWithValueTaskDelegates()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
        bool selectorInvoked = false;

        // When
        ValueResult actual = await Task.FromResult(result).SelectAsync(SelectorAsync, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(actual.IsFailure);
        Assert.Equal(FailureException, actual.GetExceptionOrThrow());
        Assert.False(selectorInvoked);

        return;
        async ValueTask SelectorAsync(CancellationToken ct) => await Task.Run(() => selectorInvoked = true, ct);
    }

    [Fact(DisplayName = "Task<ValueResult>.SelectAsync<TResult> of Success should invoke the selector delegate (Func<TResult>)")]
    public async Task TaskValueResultSelectAsyncOfSuccessShouldInvokeSelectorDelegateFuncTResult()
    {
        // Given
        ValueResult result = ValueResult.Success();
        bool selectorInvoked = false;

        // When
        ValueResult<string> actual = await Task.FromResult(result).SelectAsync(Selector, cancellationToken: TestContext.Current.CancellationToken);

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

    [Fact(DisplayName = "Task<ValueResult>.SelectAsync<TResult> of Failure should not invoke the selector delegate (Func<TResult>)")]
    public async Task TaskValueResultSelectAsyncOfFailureShouldNotInvokeSelectorDelegateFuncTResult()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
        bool selectorInvoked = false;

        // When
        ValueResult<string> actual = await Task.FromResult(result).SelectAsync(Selector, cancellationToken: TestContext.Current.CancellationToken);

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

    [Fact(DisplayName = "Task<ValueResult>.SelectAsync<TResult> of Success should invoke the selector delegate (Func<Task<TResult>>)")]
    public async Task TaskValueResultSelectAsyncOfSuccessShouldInvokeSelectorDelegateFuncTaskTResult()
    {
        // Given
        ValueResult result = ValueResult.Success();
        bool selectorInvoked = false;

        // When
        ValueResult<string> actual = await Task.FromResult(result).SelectAsync(SelectorAsync, cancellationToken: TestContext.Current.CancellationToken);

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

    [Fact(DisplayName = "Task<ValueResult>.SelectAsync<TResult> of Success should invoke the selector delegate (Func<ValueTask<TResult>>)")]
    public async Task TaskValueResultSelectAsyncOfSuccessShouldInvokeSelectorDelegateFuncTaskTResultWithValueTaskDelegates()
    {
        // Given
        ValueResult result = ValueResult.Success();
        bool selectorInvoked = false;

        // When
        ValueResult<string> actual = await Task.FromResult(result).SelectAsync(SelectorAsync, cancellationToken: TestContext.Current.CancellationToken);

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

    [Fact(DisplayName = "Task<ValueResult>.SelectAsync<TResult> of Failure should not invoke the selector delegate (Func<Task<TResult>>)")]
    public async Task TaskValueResultSelectAsyncOfFailureShouldNotInvokeSelectorDelegateFuncTaskTResult()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
        bool selectorInvoked = false;

        // When
        ValueResult<string> actual = await Task.FromResult(result).SelectAsync(SelectorAsync, cancellationToken: TestContext.Current.CancellationToken);

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

    [Fact(DisplayName = "Task<ValueResult>.SelectAsync<TResult> of Failure should not invoke the selector delegate (Func<ValueTask<TResult>>)")]
    public async Task TaskValueResultSelectAsyncOfFailureShouldNotInvokeSelectorDelegateFuncTaskTResultWithValueTaskDelegates()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
        bool selectorInvoked = false;

        // When
        ValueResult<string> actual = await Task.FromResult(result).SelectAsync(SelectorAsync, cancellationToken: TestContext.Current.CancellationToken);

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

    [Fact(DisplayName = "Task<ValueResult>.SelectAsync<TResult> of Success should invoke the selector delegate (Func<CancellationToken, Task<TResult>>)")]
    public async Task TaskValueResultSelectAsyncOfSuccessShouldInvokeSelectorDelegateFuncCancellationTokenTaskTResult()
    {
        // Given
        ValueResult result = ValueResult.Success();
        bool selectorInvoked = false;

        // When
        ValueResult<string> actual = await Task.FromResult(result).SelectAsync(SelectorAsync, cancellationToken: TestContext.Current.CancellationToken);

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

    [Fact(DisplayName = "Task<ValueResult>.SelectAsync<TResult> of Success should invoke the selector delegate (Func<CancellationToken, ValueTask<TResult>>)")]
    public async Task TaskValueResultSelectAsyncOfSuccessShouldInvokeSelectorDelegateFuncCancellationTokenTaskTResultWithValueTaskDelegates()
    {
        // Given
        ValueResult result = ValueResult.Success();
        bool selectorInvoked = false;

        // When
        ValueResult<string> actual = await Task.FromResult(result).SelectAsync(SelectorAsync, cancellationToken: TestContext.Current.CancellationToken);

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

    [Fact(DisplayName = "Task<ValueResult>.SelectAsync<TResult> of Failure should not invoke the selector delegate (Func<CancellationToken, Task<TResult>>)")]
    public async Task TaskValueResultSelectAsyncOfFailureShouldNotInvokeSelectorDelegateFuncCancellationTokenTaskTResult()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
        bool selectorInvoked = false;

        // When
        ValueResult<string> actual = await Task.FromResult(result).SelectAsync(SelectorAsync, cancellationToken: TestContext.Current.CancellationToken);

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

    [Fact(DisplayName = "Task<ValueResult>.SelectAsync<TResult> of Failure should not invoke the selector delegate (Func<CancellationToken, ValueTask<TResult>>)")]
    public async Task TaskValueResultSelectAsyncOfFailureShouldNotInvokeSelectorDelegateFuncCancellationTokenTaskTResultWithValueTaskDelegates()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
        bool selectorInvoked = false;

        // When
        ValueResult<string> actual = await Task.FromResult(result).SelectAsync(SelectorAsync, cancellationToken: TestContext.Current.CancellationToken);

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

    [Fact(DisplayName = "Task<ValueResult<T>>.SelectAsync of Success should invoke the selector delegate (Action<T>)")]
    public async Task TaskValueResultOfTSelectAsyncOfSuccessShouldInvokeSelectorDelegateActionT()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(42);
        bool selectorInvoked = false;

        // When
        ValueResult actual = await Task.FromResult(result).SelectAsync(Selector, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(actual.IsSuccess);
        Assert.True(selectorInvoked);

        return;

        void Selector(int value)
        {
            selectorInvoked = value == 42;
        }
    }

    [Fact(DisplayName = "Task<ValueResult<T>>.SelectAsync of Failure should not invoke the selector delegate (Action<T>)")]
    public async Task TaskValueResultOfTSelectAsyncOfFailureShouldNotInvokeSelectorDelegateActionT()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        bool selectorInvoked = false;

        // When
        ValueResult actual = await Task.FromResult(result).SelectAsync(Selector, cancellationToken: TestContext.Current.CancellationToken);

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

    [Fact(DisplayName = "Task<ValueResult<T>>.SelectAsync of Success should invoke the selector delegate (Func<T, Task>)")]
    public async Task TaskValueResultOfTSelectAsyncOfSuccessShouldInvokeSelectorDelegateFuncTTask()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(42);
        bool selectorInvoked = false;

        // When
        ValueResult actual = await Task.FromResult(result).SelectAsync(SelectorAsync, cancellationToken: TestContext.Current.CancellationToken);

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

    [Fact(DisplayName = "Task<ValueResult<T>>.SelectAsync of Success should invoke the selector delegate (Func<T, ValueTask>)")]
    public async Task TaskValueResultOfTSelectAsyncOfSuccessShouldInvokeSelectorDelegateFuncTTaskWithValueTaskDelegates()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(42);
        bool selectorInvoked = false;

        // When
        ValueResult actual = await Task.FromResult(result).SelectAsync(SelectorAsync, cancellationToken: TestContext.Current.CancellationToken);

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

    [Fact(DisplayName = "Task<ValueResult<T>>.SelectAsync of Failure should not invoke the selector delegate (Func<T, Task>)")]
    public async Task TaskValueResultOfTSelectAsyncOfFailureShouldNotInvokeSelectorDelegateFuncTTask()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        bool selectorInvoked = false;

        // When
        ValueResult actual = await Task.FromResult(result).SelectAsync(SelectorAsync, cancellationToken: TestContext.Current.CancellationToken);

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

    [Fact(DisplayName = "Task<ValueResult<T>>.SelectAsync of Failure should not invoke the selector delegate (Func<T, ValueTask>)")]
    public async Task TaskValueResultOfTSelectAsyncOfFailureShouldNotInvokeSelectorDelegateFuncTTaskWithValueTaskDelegates()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        bool selectorInvoked = false;

        // When
        ValueResult actual = await Task.FromResult(result).SelectAsync(SelectorAsync, cancellationToken: TestContext.Current.CancellationToken);

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

    [Fact(DisplayName = "Task<ValueResult<T>>.SelectAsync of Success should invoke the selector delegate (Func<T, CancellationToken, Task>)")]
    public async Task TaskValueResultOfTSelectAsyncOfSuccessShouldInvokeSelectorDelegateFuncTCancellationTokenTask()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(42);
        bool selectorInvoked = false;

        // When
        ValueResult actual = await Task.FromResult(result).SelectAsync(SelectorAsync, cancellationToken: TestContext.Current.CancellationToken);

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

    [Fact(DisplayName = "Task<ValueResult<T>>.SelectAsync of Success should invoke the selector delegate (Func<T, CancellationToken, ValueTask>)")]
    public async Task TaskValueResultOfTSelectAsyncOfSuccessShouldInvokeSelectorDelegateFuncTCancellationTokenTaskWithValueTaskDelegates()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(42);
        bool selectorInvoked = false;

        // When
        ValueResult actual = await Task.FromResult(result).SelectAsync(SelectorAsync, cancellationToken: TestContext.Current.CancellationToken);

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

    [Fact(DisplayName = "Task<ValueResult<T>>.SelectAsync of Failure should not invoke the selector delegate (Func<T, CancellationToken, Task>)")]
    public async Task TaskValueResultOfTSelectAsyncOfFailureShouldNotInvokeSelectorDelegateFuncTCancellationTokenTask()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        bool selectorInvoked = false;

        // When
        ValueResult actual = await Task.FromResult(result).SelectAsync(SelectorAsync, cancellationToken: TestContext.Current.CancellationToken);

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

    [Fact(DisplayName = "Task<ValueResult<T>>.SelectAsync of Failure should not invoke the selector delegate (Func<T, CancellationToken, ValueTask>)")]
    public async Task TaskValueResultOfTSelectAsyncOfFailureShouldNotInvokeSelectorDelegateFuncTCancellationTokenTaskWithValueTaskDelegates()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        bool selectorInvoked = false;

        // When
        ValueResult actual = await Task.FromResult(result).SelectAsync(SelectorAsync, cancellationToken: TestContext.Current.CancellationToken);

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

    [Fact(DisplayName = "Task<ValueResult<T>>.SelectAsync<TResult> of Success should invoke the selector delegate (Func<T, TResult>)")]
    public async Task TaskValueResultOfTSelectAsyncOfSuccessShouldInvokeSelectorDelegateFuncTResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(42);
        bool selectorInvoked = false;

        // When
        ValueResult<string> actual = await Task.FromResult(result).SelectAsync(Selector, cancellationToken: TestContext.Current.CancellationToken);

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

    [Fact(DisplayName = "Task<ValueResult<T>>.SelectAsync<TResult> of Failure should not invoke the selector delegate (Func<T, TResult>)")]
    public async Task TaskValueResultOfTSelectAsyncOfFailureShouldNotInvokeSelectorDelegateFuncTResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        bool selectorInvoked = false;

        // When
        ValueResult<string> actual = await Task.FromResult(result).SelectAsync(Selector, cancellationToken: TestContext.Current.CancellationToken);

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

    [Fact(DisplayName = "Task<ValueResult<T>>.SelectAsync<TResult> of Success should invoke the selector delegate (Func<T, Task<TResult>>)")]
    public async Task TaskValueResultOfTSelectAsyncOfSuccessShouldInvokeSelectorDelegateFuncTTaskResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(42);
        bool selectorInvoked = false;

        // When
        ValueResult<string> actual = await Task.FromResult(result).SelectAsync(SelectorAsync, cancellationToken: TestContext.Current.CancellationToken);

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

    [Fact(DisplayName = "Task<ValueResult<T>>.SelectAsync<TResult> of Success should invoke the selector delegate (Func<T, ValueTask<TResult>>)")]
    public async Task TaskValueResultOfTSelectAsyncOfSuccessShouldInvokeSelectorDelegateFuncTTaskResultWithValueTaskDelegates()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(42);
        bool selectorInvoked = false;

        // When
        ValueResult<string> actual = await Task.FromResult(result).SelectAsync(SelectorAsync, cancellationToken: TestContext.Current.CancellationToken);

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

    [Fact(DisplayName = "Task<ValueResult<T>>.SelectAsync<TResult> of Failure should not invoke the selector delegate (Func<T, Task<TResult>>)")]
    public async Task TaskValueResultOfTSelectAsyncOfFailureShouldNotInvokeSelectorDelegateFuncTTaskResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        bool selectorInvoked = false;

        // When
        ValueResult<string> actual = await Task.FromResult(result).SelectAsync(SelectorAsync, cancellationToken: TestContext.Current.CancellationToken);

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

    [Fact(DisplayName = "Task<ValueResult<T>>.SelectAsync<TResult> of Failure should not invoke the selector delegate (Func<T, ValueTask<TResult>>)")]
    public async Task TaskValueResultOfTSelectAsyncOfFailureShouldNotInvokeSelectorDelegateFuncTTaskResultWithValueTaskDelegates()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        bool selectorInvoked = false;

        // When
        ValueResult<string> actual = await Task.FromResult(result).SelectAsync(SelectorAsync, cancellationToken: TestContext.Current.CancellationToken);

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

    [Fact(DisplayName = "Task<ValueResult<T>>.SelectAsync<TResult> of Success should invoke the selector delegate (Func<T, CancellationToken, Task<TResult>>)")]
    public async Task TaskValueResultOfTSelectAsyncOfSuccessShouldInvokeSelectorDelegateFuncTCancellationTokenTaskResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(42);
        bool selectorInvoked = false;

        // When
        ValueResult<string> actual = await Task.FromResult(result).SelectAsync(SelectorAsync, cancellationToken: TestContext.Current.CancellationToken);

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

    [Fact(DisplayName = "Task<ValueResult<T>>.SelectAsync<TResult> of Success should invoke the selector delegate (Func<T, CancellationToken, ValueTask<TResult>>)")]
    public async Task TaskValueResultOfTSelectAsyncOfSuccessShouldInvokeSelectorDelegateFuncTCancellationTokenTaskResultWithValueTaskDelegates()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(42);
        bool selectorInvoked = false;

        // When
        ValueResult<string> actual = await Task.FromResult(result).SelectAsync(SelectorAsync, cancellationToken: TestContext.Current.CancellationToken);

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

    [Fact(DisplayName = "Task<ValueResult<T>>.SelectAsync<TResult> of Failure should not invoke the selector delegate (Func<T, CancellationToken, Task<TResult>>)")]
    public async Task TaskValueResultOfTSelectAsyncOfFailureShouldNotInvokeSelectorDelegateFuncTCancellationTokenTaskResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        bool selectorInvoked = false;

        // When
        ValueResult<string> actual = await Task.FromResult(result).SelectAsync(SelectorAsync, cancellationToken: TestContext.Current.CancellationToken);

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

    [Fact(DisplayName = "Task<ValueResult<T>>.SelectAsync<TResult> of Failure should not invoke the selector delegate (Func<T, CancellationToken, ValueTask<TResult>>)")]
    public async Task TaskValueResultOfTSelectAsyncOfFailureShouldNotInvokeSelectorDelegateFuncTCancellationTokenTaskResultWithValueTaskDelegates()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        bool selectorInvoked = false;

        // When
        ValueResult<string> actual = await Task.FromResult(result).SelectAsync(SelectorAsync, cancellationToken: TestContext.Current.CancellationToken);

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

    [Fact(DisplayName = "Task<ValueResult>.SelectManyAsync of Success should invoke the selector delegate (Func<ValueResult>) and return its result")]
    public async Task TaskValueResultSelectManyAsyncOfSuccessShouldInvokeSelectorDelegateFuncResult()
    {
        // Given
        ValueResult result = ValueResult.Success();
        bool selectorInvoked = false;

        // When
        ValueResult actual = await Task.FromResult(result).SelectManyAsync(Selector, cancellationToken: TestContext.Current.CancellationToken);

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

    [Fact(DisplayName = "Task<ValueResult>.SelectManyAsync of Failure should not invoke the selector delegate (Func<ValueResult>)")]
    public async Task TaskValueResultSelectManyAsyncOfFailureShouldNotInvokeSelectorDelegateFuncResult()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
        bool selectorInvoked = false;

        // When
        ValueResult actual = await Task.FromResult(result).SelectManyAsync(Selector, cancellationToken: TestContext.Current.CancellationToken);

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

    [Fact(DisplayName = "Task<ValueResult>.SelectManyAsync of Success should invoke the selector delegate (Func<Task<ValueResult>>) and return its result")]
    public async Task TaskValueResultSelectManyAsyncOfSuccessShouldInvokeSelectorDelegateFuncTaskResult()
    {
        // Given
        ValueResult result = ValueResult.Success();
        bool selectorInvoked = false;

        // When
        ValueResult actual = await Task.FromResult(result).SelectManyAsync(SelectorAsync, cancellationToken: TestContext.Current.CancellationToken);

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

    [Fact(DisplayName = "Task<ValueResult>.SelectManyAsync of Success should invoke the selector delegate (Func<ValueTask<ValueResult>>) and return its result")]
    public async Task TaskValueResultSelectManyAsyncOfSuccessShouldInvokeSelectorDelegateFuncTaskResultWithValueTaskDelegates()
    {
        // Given
        ValueResult result = ValueResult.Success();
        bool selectorInvoked = false;

        // When
        ValueResult actual = await Task.FromResult(result).SelectManyAsync(SelectorAsync, cancellationToken: TestContext.Current.CancellationToken);

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

    [Fact(DisplayName = "Task<ValueResult>.SelectManyAsync of Failure should not invoke the selector delegate (Func<Task<ValueResult>>)")]
    public async Task TaskValueResultSelectManyAsyncOfFailureShouldNotInvokeSelectorDelegateFuncTaskResult()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
        bool selectorInvoked = false;

        // When
        ValueResult actual = await Task.FromResult(result).SelectManyAsync(SelectorAsync, cancellationToken: TestContext.Current.CancellationToken);

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

    [Fact(DisplayName = "Task<ValueResult>.SelectManyAsync of Failure should not invoke the selector delegate (Func<ValueTask<ValueResult>>)")]
    public async Task TaskValueResultSelectManyAsyncOfFailureShouldNotInvokeSelectorDelegateFuncTaskResultWithValueTaskDelegates()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
        bool selectorInvoked = false;

        // When
        ValueResult actual = await Task.FromResult(result).SelectManyAsync(SelectorAsync, cancellationToken: TestContext.Current.CancellationToken);

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

    [Fact(DisplayName = "Task<ValueResult>.SelectManyAsync of Success should invoke the selector delegate (Func<CancellationToken, Task<ValueResult>>) and return its result")]
    public async Task TaskValueResultSelectManyAsyncOfSuccessShouldInvokeSelectorDelegateFuncCancellationTokenTaskResult()
    {
        // Given
        ValueResult result = ValueResult.Success();
        bool selectorInvoked = false;

        // When
        ValueResult actual = await Task.FromResult(result).SelectManyAsync(SelectorAsync, cancellationToken: TestContext.Current.CancellationToken);

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

    [Fact(DisplayName = "Task<ValueResult>.SelectManyAsync of Success should invoke the selector delegate (Func<CancellationToken, ValueTask<ValueResult>>) and return its result")]
    public async Task TaskValueResultSelectManyAsyncOfSuccessShouldInvokeSelectorDelegateFuncCancellationTokenTaskResultWithValueTaskDelegates()
    {
        // Given
        ValueResult result = ValueResult.Success();
        bool selectorInvoked = false;

        // When
        ValueResult actual = await Task.FromResult(result).SelectManyAsync(SelectorAsync, cancellationToken: TestContext.Current.CancellationToken);

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

    [Fact(DisplayName = "Task<ValueResult>.SelectManyAsync of Failure should not invoke the selector delegate (Func<CancellationToken, Task<ValueResult>>)")]
    public async Task TaskValueResultSelectManyAsyncOfFailureShouldNotInvokeSelectorDelegateFuncCancellationTokenTaskResult()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
        bool selectorInvoked = false;

        // When
        ValueResult actual = await Task.FromResult(result).SelectManyAsync(SelectorAsync, cancellationToken: TestContext.Current.CancellationToken);

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

    [Fact(DisplayName = "Task<ValueResult>.SelectManyAsync of Failure should not invoke the selector delegate (Func<CancellationToken, ValueTask<ValueResult>>)")]
    public async Task TaskValueResultSelectManyAsyncOfFailureShouldNotInvokeSelectorDelegateFuncCancellationTokenTaskResultWithValueTaskDelegates()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
        bool selectorInvoked = false;

        // When
        ValueResult actual = await Task.FromResult(result).SelectManyAsync(SelectorAsync, cancellationToken: TestContext.Current.CancellationToken);

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

    [Fact(DisplayName = "Task<ValueResult>.SelectManyAsync<TResult> of Success should invoke the selector delegate (Func<ValueResult<TResult>>) and return its result")]
    public async Task TaskValueResultSelectManyAsyncOfSuccessShouldInvokeSelectorDelegateFuncResultTResult()
    {
        // Given
        ValueResult result = ValueResult.Success();
        bool selectorInvoked = false;

        // When
        ValueResult<string> actual = await Task.FromResult(result).SelectManyAsync(Selector, cancellationToken: TestContext.Current.CancellationToken);

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

    [Fact(DisplayName = "Task<ValueResult>.SelectManyAsync<TResult> of Failure should not invoke the selector delegate (Func<ValueResult<TResult>>)")]
    public async Task TaskValueResultSelectManyAsyncOfFailureShouldNotInvokeSelectorDelegateFuncResultTResult()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
        bool selectorInvoked = false;

        // When
        ValueResult<string> actual = await Task.FromResult(result).SelectManyAsync(Selector, cancellationToken: TestContext.Current.CancellationToken);

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

    [Fact(DisplayName = "Task<ValueResult>.SelectManyAsync<TResult> of Success should invoke the selector delegate (Func<Task<ValueResult<TResult>>>) and return its result")]
    public async Task TaskValueResultSelectManyAsyncOfSuccessShouldInvokeSelectorDelegateFuncTaskResultTResult()
    {
        // Given
        ValueResult result = ValueResult.Success();
        bool selectorInvoked = false;

        // When
        ValueResult<string> actual = await Task.FromResult(result).SelectManyAsync(SelectorAsync, cancellationToken: TestContext.Current.CancellationToken);

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

    [Fact(DisplayName = "Task<ValueResult>.SelectManyAsync<TResult> of Success should invoke the selector delegate (Func<ValueTask<ValueResult<TResult>>>) and return its result")]
    public async Task TaskValueResultSelectManyAsyncOfSuccessShouldInvokeSelectorDelegateFuncTaskResultTResultWithValueTaskDelegates()
    {
        // Given
        ValueResult result = ValueResult.Success();
        bool selectorInvoked = false;

        // When
        ValueResult<string> actual = await Task.FromResult(result).SelectManyAsync(SelectorAsync, cancellationToken: TestContext.Current.CancellationToken);

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

    [Fact(DisplayName = "Task<ValueResult>.SelectManyAsync<TResult> of Failure should not invoke the selector delegate (Func<Task<ValueResult<TResult>>>)")]
    public async Task TaskValueResultSelectManyAsyncOfFailureShouldNotInvokeSelectorDelegateFuncTaskResultTResult()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
        bool selectorInvoked = false;

        // When
        ValueResult<string> actual = await Task.FromResult(result).SelectManyAsync(SelectorAsync, cancellationToken: TestContext.Current.CancellationToken);

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

    [Fact(DisplayName = "Task<ValueResult>.SelectManyAsync<TResult> of Failure should not invoke the selector delegate (Func<ValueTask<ValueResult<TResult>>>)")]
    public async Task TaskValueResultSelectManyAsyncOfFailureShouldNotInvokeSelectorDelegateFuncTaskResultTResultWithValueTaskDelegates()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
        bool selectorInvoked = false;

        // When
        ValueResult<string> actual = await Task.FromResult(result).SelectManyAsync(SelectorAsync, cancellationToken: TestContext.Current.CancellationToken);

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

    [Fact(DisplayName = "Task<ValueResult>.SelectManyAsync<TResult> of Success should invoke the selector delegate (Func<CancellationToken, Task<ValueResult<TResult>>>) and return its result")]
    public async Task TaskValueResultSelectManyAsyncOfSuccessShouldInvokeSelectorDelegateFuncCancellationTokenTaskResultTResult()
    {
        // Given
        ValueResult result = ValueResult.Success();
        bool selectorInvoked = false;

        // When
        ValueResult<string> actual = await Task.FromResult(result).SelectManyAsync(SelectorAsync, cancellationToken: TestContext.Current.CancellationToken);

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

    [Fact(DisplayName = "Task<ValueResult>.SelectManyAsync<TResult> of Success should invoke the selector delegate (Func<CancellationToken, ValueTask<ValueResult<TResult>>>) and return its result")]
    public async Task TaskValueResultSelectManyAsyncOfSuccessShouldInvokeSelectorDelegateFuncCancellationTokenTaskResultTResultWithValueTaskDelegates()
    {
        // Given
        ValueResult result = ValueResult.Success();
        bool selectorInvoked = false;

        // When
        ValueResult<string> actual = await Task.FromResult(result).SelectManyAsync(SelectorAsync, cancellationToken: TestContext.Current.CancellationToken);

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

    [Fact(DisplayName = "Task<ValueResult>.SelectManyAsync<TResult> of Failure should not invoke the selector delegate (Func<CancellationToken, Task<ValueResult<TResult>>>)")]
    public async Task TaskValueResultSelectManyAsyncOfFailureShouldNotInvokeSelectorDelegateFuncCancellationTokenTaskResultTResult()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
        bool selectorInvoked = false;

        // When
        ValueResult<string> actual = await Task.FromResult(result).SelectManyAsync(SelectorAsync, cancellationToken: TestContext.Current.CancellationToken);

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

    [Fact(DisplayName = "Task<ValueResult>.SelectManyAsync<TResult> of Failure should not invoke the selector delegate (Func<CancellationToken, ValueTask<ValueResult<TResult>>>)")]
    public async Task TaskValueResultSelectManyAsyncOfFailureShouldNotInvokeSelectorDelegateFuncCancellationTokenTaskResultTResultWithValueTaskDelegates()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);
        bool selectorInvoked = false;

        // When
        ValueResult<string> actual = await Task.FromResult(result).SelectManyAsync(SelectorAsync, cancellationToken: TestContext.Current.CancellationToken);

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

    [Fact(DisplayName = "Task<ValueResult<T>>.SelectManyAsync of Success should invoke the selector delegate (Func<T, ValueResult>) and return its result")]
    public async Task TaskValueResultOfTSelectManyAsyncOfSuccessShouldInvokeSelectorDelegateFuncTResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(42);
        bool selectorInvoked = false;

        // When
        ValueResult actual = await Task.FromResult(result).SelectManyAsync(Selector, cancellationToken: TestContext.Current.CancellationToken);

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

    [Fact(DisplayName = "Task<ValueResult<T>>.SelectManyAsync of Failure should not invoke the selector delegate (Func<T, ValueResult>)")]
    public async Task TaskValueResultOfTSelectManyAsyncOfFailureShouldNotInvokeSelectorDelegateFuncTResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        bool selectorInvoked = false;

        // When
        ValueResult actual = await Task.FromResult(result).SelectManyAsync(Selector, cancellationToken: TestContext.Current.CancellationToken);

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

    [Fact(DisplayName = "Task<ValueResult<T>>.SelectManyAsync of Success should invoke the selector delegate (Func<T, Task<ValueResult>>) and return its result")]
    public async Task TaskValueResultOfTSelectManyAsyncOfSuccessShouldInvokeSelectorDelegateFuncTTaskResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(42);
        bool selectorInvoked = false;

        // When
        ValueResult actual = await Task.FromResult(result).SelectManyAsync(SelectorAsync, cancellationToken: TestContext.Current.CancellationToken);

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

    [Fact(DisplayName = "Task<ValueResult<T>>.SelectManyAsync of Success should invoke the selector delegate (Func<T, ValueTask<ValueResult>>) and return its result")]
    public async Task TaskValueResultOfTSelectManyAsyncOfSuccessShouldInvokeSelectorDelegateFuncTTaskResultWithValueTaskDelegates()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(42);
        bool selectorInvoked = false;

        // When
        ValueResult actual = await Task.FromResult(result).SelectManyAsync(SelectorAsync, cancellationToken: TestContext.Current.CancellationToken);

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

    [Fact(DisplayName = "Task<ValueResult<T>>.SelectManyAsync of Failure should not invoke the selector delegate (Func<T, Task<ValueResult>>)")]
    public async Task TaskValueResultOfTSelectManyAsyncOfFailureShouldNotInvokeSelectorDelegateFuncTTaskResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        bool selectorInvoked = false;

        // When
        ValueResult actual = await Task.FromResult(result).SelectManyAsync(SelectorAsync, cancellationToken: TestContext.Current.CancellationToken);

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

    [Fact(DisplayName = "Task<ValueResult<T>>.SelectManyAsync of Failure should not invoke the selector delegate (Func<T, ValueTask<ValueResult>>)")]
    public async Task TaskValueResultOfTSelectManyAsyncOfFailureShouldNotInvokeSelectorDelegateFuncTTaskResultWithValueTaskDelegates()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        bool selectorInvoked = false;

        // When
        ValueResult actual = await Task.FromResult(result).SelectManyAsync(SelectorAsync, cancellationToken: TestContext.Current.CancellationToken);

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

    [Fact(DisplayName = "Task<ValueResult<T>>.SelectManyAsync of Success should invoke the selector delegate (Func<T, CancellationToken, Task<ValueResult>>) and return its result")]
    public async Task TaskValueResultOfTSelectManyAsyncOfSuccessShouldInvokeSelectorDelegateFuncTCancellationTokenTaskResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(42);
        bool selectorInvoked = false;

        // When
        ValueResult actual = await Task.FromResult(result).SelectManyAsync(SelectorAsync, cancellationToken: TestContext.Current.CancellationToken);

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

    [Fact(DisplayName = "Task<ValueResult<T>>.SelectManyAsync of Success should invoke the selector delegate (Func<T, CancellationToken, ValueTask<ValueResult>>) and return its result")]
    public async Task TaskValueResultOfTSelectManyAsyncOfSuccessShouldInvokeSelectorDelegateFuncTCancellationTokenTaskResultWithValueTaskDelegates()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(42);
        bool selectorInvoked = false;

        // When
        ValueResult actual = await Task.FromResult(result).SelectManyAsync(SelectorAsync, cancellationToken: TestContext.Current.CancellationToken);

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

    [Fact(DisplayName = "Task<ValueResult<T>>.SelectManyAsync of Failure should not invoke the selector delegate (Func<T, CancellationToken, Task<ValueResult>>)")]
    public async Task TaskValueResultOfTSelectManyAsyncOfFailureShouldNotInvokeSelectorDelegateFuncTCancellationTokenTaskResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        bool selectorInvoked = false;

        // When
        ValueResult actual = await Task.FromResult(result).SelectManyAsync(SelectorAsync, cancellationToken: TestContext.Current.CancellationToken);

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

    [Fact(DisplayName = "Task<ValueResult<T>>.SelectManyAsync of Failure should not invoke the selector delegate (Func<T, CancellationToken, ValueTask<ValueResult>>)")]
    public async Task TaskValueResultOfTSelectManyAsyncOfFailureShouldNotInvokeSelectorDelegateFuncTCancellationTokenTaskResultWithValueTaskDelegates()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        bool selectorInvoked = false;

        // When
        ValueResult actual = await Task.FromResult(result).SelectManyAsync(SelectorAsync, cancellationToken: TestContext.Current.CancellationToken);

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

    [Fact(DisplayName = "Task<ValueResult<T>>.SelectManyAsync<TResult> of Success should invoke the selector delegate (Func<T, ValueResult<TResult>>) and return its result")]
    public async Task TaskValueResultOfTSelectManyAsyncOfSuccessShouldInvokeSelectorDelegateFuncTResultTResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(42);
        bool selectorInvoked = false;

        // When
        ValueResult<string> actual = await Task.FromResult(result).SelectManyAsync(Selector, cancellationToken: TestContext.Current.CancellationToken);

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

    [Fact(DisplayName = "Task<ValueResult<T>>.SelectManyAsync<TResult> of Failure should not invoke the selector delegate (Func<T, ValueResult<TResult>>)")]
    public async Task TaskValueResultOfTSelectManyAsyncOfFailureShouldNotInvokeSelectorDelegateFuncTResultTResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        bool selectorInvoked = false;

        // When
        ValueResult<string> actual = await Task.FromResult(result).SelectManyAsync(Selector, cancellationToken: TestContext.Current.CancellationToken);

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

    [Fact(DisplayName = "Task<ValueResult<T>>.SelectManyAsync<TResult> of Success should invoke the selector delegate (Func<T, Task<ValueResult<TResult>>>) and return its result")]
    public async Task TaskValueResultOfTSelectManyAsyncOfSuccessShouldInvokeSelectorDelegateFuncTTaskResultTResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(42);
        bool selectorInvoked = false;

        // When
        ValueResult<string> actual = await Task.FromResult(result).SelectManyAsync(SelectorAsync, cancellationToken: TestContext.Current.CancellationToken);

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

    [Fact(DisplayName = "Task<ValueResult<T>>.SelectManyAsync<TResult> of Success should invoke the selector delegate (Func<T, ValueTask<ValueResult<TResult>>>) and return its result")]
    public async Task TaskValueResultOfTSelectManyAsyncOfSuccessShouldInvokeSelectorDelegateFuncTTaskResultTResultWithValueTaskDelegates()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(42);
        bool selectorInvoked = false;

        // When
        ValueResult<string> actual = await Task.FromResult(result).SelectManyAsync(SelectorAsync, cancellationToken: TestContext.Current.CancellationToken);

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

    [Fact(DisplayName = "Task<ValueResult<T>>.SelectManyAsync<TResult> of Failure should not invoke the selector delegate (Func<T, Task<ValueResult<TResult>>>)")]
    public async Task TaskValueResultOfTSelectManyAsyncOfFailureShouldNotInvokeSelectorDelegateFuncTTaskResultTResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        bool selectorInvoked = false;

        // When
        ValueResult<string> actual = await Task.FromResult(result).SelectManyAsync(SelectorAsync, cancellationToken: TestContext.Current.CancellationToken);

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

    [Fact(DisplayName = "Task<ValueResult<T>>.SelectManyAsync<TResult> of Failure should not invoke the selector delegate (Func<T, ValueTask<ValueResult<TResult>>>)")]
    public async Task TaskValueResultOfTSelectManyAsyncOfFailureShouldNotInvokeSelectorDelegateFuncTTaskResultTResultWithValueTaskDelegates()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        bool selectorInvoked = false;

        // When
        ValueResult<string> actual = await Task.FromResult(result).SelectManyAsync(SelectorAsync, cancellationToken: TestContext.Current.CancellationToken);

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

    [Fact(DisplayName = "Task<ValueResult<T>>.SelectManyAsync<TResult> of Success should invoke the selector delegate (Func<T, CancellationToken, Task<ValueResult<TResult>>>) and return its result")]
    public async Task TaskValueResultOfTSelectManyAsyncOfSuccessShouldInvokeSelectorDelegateFuncTCancellationTokenTaskResultTResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(42);
        bool selectorInvoked = false;

        // When
        ValueResult<string> actual = await Task.FromResult(result).SelectManyAsync(SelectorAsync, cancellationToken: TestContext.Current.CancellationToken);

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

    [Fact(DisplayName = "Task<ValueResult<T>>.SelectManyAsync<TResult> of Success should invoke the selector delegate (Func<T, CancellationToken, ValueTask<ValueResult<TResult>>>) and return its result")]
    public async Task TaskValueResultOfTSelectManyAsyncOfSuccessShouldInvokeSelectorDelegateFuncTCancellationTokenTaskResultTResultWithValueTaskDelegates()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(42);
        bool selectorInvoked = false;

        // When
        ValueResult<string> actual = await Task.FromResult(result).SelectManyAsync(SelectorAsync, cancellationToken: TestContext.Current.CancellationToken);

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

    [Fact(DisplayName = "Task<ValueResult<T>>.SelectManyAsync<TResult> of Failure should not invoke the selector delegate (Func<T, CancellationToken, Task<ValueResult<TResult>>>)")]
    public async Task TaskValueResultOfTSelectManyAsyncOfFailureShouldNotInvokeSelectorDelegateFuncTCancellationTokenTaskResultTResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        bool selectorInvoked = false;

        // When
        ValueResult<string> actual = await Task.FromResult(result).SelectManyAsync(SelectorAsync, cancellationToken: TestContext.Current.CancellationToken);

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

    [Fact(DisplayName = "Task<ValueResult<T>>.SelectManyAsync<TResult> of Failure should not invoke the selector delegate (Func<T, CancellationToken, ValueTask<ValueResult<TResult>>>)")]
    public async Task TaskValueResultOfTSelectManyAsyncOfFailureShouldNotInvokeSelectorDelegateFuncTCancellationTokenTaskResultTResultWithValueTaskDelegates()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);
        bool selectorInvoked = false;

        // When
        ValueResult<string> actual = await Task.FromResult(result).SelectManyAsync(SelectorAsync, cancellationToken: TestContext.Current.CancellationToken);

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

    [Fact(DisplayName = "ValueResult Success.ThrowAsync should do nothing")]
    public async Task ValueResultSuccessThrowAsyncShouldDoNothing()
    {
        // Given
        ValueResult result = ValueResult.Success();

        // When / Then
        await Task.FromResult(result).ThrowAsync(cancellationToken: TestContext.Current.CancellationToken);
    }

    [Fact(DisplayName = "ValueResult Failure.Throw should throw Exception")]
    public async Task ValueResultFailureThrowAsyncShouldThrowException()
    {
        // Given
        ValueResult result = ValueResult.Failure(FailureException);

        // When / Then
        await Assert.ThrowsAsync<Exception>(async () => await Task.FromResult(result).ThrowAsync(cancellationToken: TestContext.Current.CancellationToken));
    }

    [Fact(DisplayName = "ValueResult<T> Success.ThrowAsync should do nothing")]
    public async Task ValueResultOfTSuccessThrowAsyncShouldDoNothing()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(123);

        // When / Then
        await Task.FromResult(result).ThrowAsync(cancellationToken: TestContext.Current.CancellationToken);
    }

    [Fact(DisplayName = "ValueResult<T> Failure.ThrowAsync should throw Exception")]
    public async Task ValueResultOfTFailureThrowAsyncShouldThrowException()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);

        // When / Then
        await Assert.ThrowsAsync<Exception>(async () => await Task.FromResult(result).ThrowAsync(cancellationToken: TestContext.Current.CancellationToken));
    }

}
