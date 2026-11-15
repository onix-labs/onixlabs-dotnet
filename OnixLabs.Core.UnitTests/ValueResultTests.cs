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
        Assert.Throws<InvalidOperationException>(() => success.GetExceptionOrThrow());
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
