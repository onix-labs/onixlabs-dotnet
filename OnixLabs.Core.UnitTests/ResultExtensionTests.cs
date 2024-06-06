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
using Xunit;

namespace OnixLabs.Core.UnitTests;

public sealed class ResultExtensionTests
{
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
    public void ResultGetOptionalValueOrThrowShouldReturnDefaultValueWhenResultIsSuccessAndOptionalValueIsNone()
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
}
