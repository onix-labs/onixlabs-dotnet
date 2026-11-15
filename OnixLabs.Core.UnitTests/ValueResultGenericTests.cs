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

public sealed class ValueResultGenericTests
{
    private static readonly Exception FailureException = new("Failure");

    [Fact(DisplayName = "ValueResult<T>.IsSuccess should produce the expected result")]
    public void ValueResultIsSuccessShouldProduceExpectedResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Success(123);

        // When / Then
        Assert.True(result.IsSuccess);
        Assert.False(result.IsFailure);
    }

    [Fact(DisplayName = "ValueResult<T>.IsFailure should produce the expected result")]
    public void ValueResultIsFailureShouldProduceExpectedResult()
    {
        // Given
        ValueResult<int> result = ValueResult<int>.Failure(FailureException);

        // When / Then
        Assert.True(result.IsFailure);
        Assert.False(result.IsSuccess);
    }

    [Fact(DisplayName = "ValueResult<T> default should be a failure reporting that it was never initialised")]
    public void ValueResultDefaultShouldBeFailureReportingThatItWasNeverInitialised()
    {
        // Given
        ValueResult<int>[] results = new ValueResult<int>[1];

        // When
        ValueResult<int> result = results[0];

        // Then
        Assert.True(result.IsFailure);
        InvalidOperationException exception = Assert.IsType<InvalidOperationException>(result.GetExceptionOrDefault());
        Assert.Contains("never initialised", exception.Message);
        Assert.Same(exception, Assert.Throws<InvalidOperationException>(() => result.GetValueOrThrow()));
        Assert.Equal(default(ValueResult<int>), result);
    }

    [Fact(DisplayName = "ValueResult<T>.Of should produce the expected success result")]
    public void ValueResultOfShouldProduceExpectedSuccessResult()
    {
        // Given / When
        ValueResult<int> result = ValueResult<int>.Of(() => 123);

        // Then
        Assert.True(result.IsSuccess);
        Assert.Equal(123, result.GetValueOrThrow());
    }

    [Fact(DisplayName = "ValueResult<T>.Of should produce the expected failure result")]
    public void ValueResultOfShouldProduceExpectedFailureResult()
    {
        // Given / When
        ValueResult<int> result = ValueResult<int>.Of(() => throw FailureException);

        // Then
        Assert.True(result.IsFailure);
        Assert.Same(FailureException, result.GetExceptionOrDefault());
    }

    [Fact(DisplayName = "ValueResult<T>.Success should allow a null value when T is nullable")]
    public void ValueResultSuccessShouldAllowNullValueWhenTIsNullable()
    {
        // Given / When
        ValueResult<string?> result = ValueResult<string?>.Success(null);

        // Then
        Assert.True(result.IsSuccess);
        Assert.Null(result.GetValueOrThrow());
    }

    [Fact(DisplayName = "ValueResult<T>.Failure with a null exception should throw ArgumentNullException")]
    public void ValueResultFailureWithNullExceptionShouldThrowArgumentNullException()
    {
        // Given / When / Then
        Assert.Throws<ArgumentNullException>(() => ValueResult<int>.Failure(null!));
    }

    [Fact(DisplayName = "ValueResult<T> implicit and explicit conversions should produce the expected result")]
    public void ValueResultImplicitAndExplicitConversionsShouldProduceExpectedResult()
    {
        // Given / When
        ValueResult<int> success = 123;
        ValueResult<int> failure = FailureException;

        // Then
        Assert.Equal(123, (int)success);
        Assert.Same(FailureException, failure.GetExceptionOrDefault());
        Assert.Same(FailureException, Assert.Throws<Exception>(() => (int)failure));
    }

    [Fact(DisplayName = "ValueResult<T> equality should produce the expected result")]
    public void ValueResultEqualityShouldProduceExpectedResult()
    {
        // Given
        ValueResult<int> success = ValueResult<int>.Success(123);
        ValueResult<int> failure = ValueResult<int>.Failure(FailureException);

        // When / Then
        Assert.True(success == ValueResult<int>.Success(123));
        Assert.True(success != ValueResult<int>.Success(456));
        Assert.True(failure == ValueResult<int>.Failure(FailureException));
        Assert.True(failure != ValueResult<int>.Failure(new Exception("Failure")));
        Assert.True(success != failure);
        Assert.True(ValueResult<int>.Success(0) != default);
        Assert.True(ValueResult<string?>.Success(null) == ValueResult<string?>.Success(null));
        Assert.True(success.Equals((object)ValueResult<int>.Success(123)));
        Assert.False(success.Equals((object)Result<int>.Success(123)));
        Assert.Equal(123.GetHashCode(), success.GetHashCode());
        Assert.Equal(FailureException.GetHashCode(), failure.GetHashCode());
    }

    [Fact(DisplayName = "ValueResult<T> exception getters should produce the expected result")]
    public void ValueResultExceptionGettersShouldProduceExpectedResult()
    {
        // Given
        ValueResult<int> success = ValueResult<int>.Success(123);
        ValueResult<int> failure = ValueResult<int>.Failure(FailureException);
        Exception defaultException = new("Default");

        // When / Then
        Assert.Null(success.GetExceptionOrDefault());
        Assert.Same(FailureException, failure.GetExceptionOrDefault());
        Assert.Same(defaultException, success.GetExceptionOrDefault(defaultException));
        Assert.Same(FailureException, failure.GetExceptionOrDefault(defaultException));
        Assert.Equal(ValueOptional<Exception>.None, success.GetExceptionOrNone());
        Assert.Equal(ValueOptional<Exception>.Some(FailureException), failure.GetExceptionOrNone());
        Assert.Same(FailureException, failure.GetExceptionOrThrow());
        Assert.Throws<InvalidOperationException>(() => success.GetExceptionOrThrow());
    }

    [Fact(DisplayName = "ValueResult<T> value getters should produce the expected result")]
    public void ValueResultValueGettersShouldProduceExpectedResult()
    {
        // Given
        ValueResult<int> success = ValueResult<int>.Success(123);
        ValueResult<int> failure = ValueResult<int>.Failure(FailureException);

        // When / Then
        Assert.Equal(123, success.GetValueOrDefault());
        Assert.Equal(0, failure.GetValueOrDefault());
        Assert.Equal(123, success.GetValueOrDefault(456));
        Assert.Equal(456, failure.GetValueOrDefault(456));
        Assert.Equal(123, success.GetValueOrThrow());
        Assert.Same(FailureException, Assert.Throws<Exception>(() => failure.GetValueOrThrow()));
    }

    [Fact(DisplayName = "ValueResult<T>.Match should invoke the delegate that matches the state")]
    public void ValueResultMatchShouldInvokeTheDelegateThatMatchesTheState()
    {
        // Given
        ValueResult<int> success = ValueResult<int>.Success(123);
        ValueResult<int> failure = ValueResult<int>.Failure(FailureException);
        string matched = string.Empty;

        // When / Then
        success.Match(success: value => matched = $"Value: {value}", failure: exception => matched = exception.Message);
        Assert.Equal("Value: 123", matched);

        failure.Match(success: value => matched = $"Value: {value}", failure: exception => matched = exception.Message);
        Assert.Equal("Failure", matched);

        Assert.Equal("Value: 123", success.Match(success: value => $"Value: {value}", failure: exception => exception.Message));
        Assert.Equal("Failure", failure.Match(success: value => $"Value: {value}", failure: exception => exception.Message));
    }

    [Fact(DisplayName = "ValueResult<T> switch expression should match the value of a success and the exception of a failure")]
    public void ValueResultSwitchExpressionShouldMatchTheValueOfSuccessAndTheExceptionOfFailure()
    {
        // Given
        ValueResult<int> success = ValueResult<int>.Success(123);
        ValueResult<int> failure = ValueResult<int>.Failure(FailureException);
        ValueResult<int> uninitialised = default;

        // When / Then
        Assert.Equal("Value: 123", Describe(success));
        Assert.Equal("Failure", Describe(failure));
        Assert.Contains("never initialised", Describe(uninitialised));

        // No discard or null arm is needed: neither case type is nullable, and default is a failure.
        static string Describe(ValueResult<int> result) => result switch
        {
            int value => $"Value: {value}",
            Exception exception => exception.Message
        };
    }

    [Fact(DisplayName = "ValueResult<T> switch expression should match null for a success holding null")]
    public void ValueResultSwitchExpressionShouldMatchNullForSuccessHoldingNull()
    {
        // Given
        ValueResult<string?> success = ValueResult<string?>.Success("abc");
        ValueResult<string?> successOfNull = ValueResult<string?>.Success(null);
        ValueResult<string?> failure = ValueResult<string?>.Failure(FailureException);

        // When / Then
        Assert.Equal("Value: abc", Describe(success));
        Assert.Equal("Value: null", Describe(successOfNull));
        Assert.Equal("Failure", Describe(failure));

        // T is nullable, so a success can hold null; removing the null arm reports CS8655 (null not handled).
        static string Describe(ValueResult<string?> result) => result switch
        {
            string value => $"Value: {value}",
            Exception exception => exception.Message,
            null => "Value: null"
        };
    }

    [Fact(DisplayName = "ValueResult<T> type patterns should test the underlying value or exception")]
    public void ValueResultTypePatternsShouldTestTheUnderlyingValueOrException()
    {
        // Given
        ValueResult<int> success = ValueResult<int>.Success(123);
        ValueResult<int> failure = ValueResult<int>.Failure(new InvalidOperationException("Failure"));

        // When / Then
        Assert.True(success is 123);
        Assert.True(success is > 100 and < 200);
        Assert.False(success is Exception);
        Assert.True(failure is InvalidOperationException { Message: "Failure" });
        Assert.False(failure is int);
    }

    [Fact(DisplayName = "ValueResult<T>.Select should produce the expected result")]
    public void ValueResultSelectShouldProduceExpectedResult()
    {
        // Given
        ValueResult<int> success = ValueResult<int>.Success(123);
        ValueResult<int> failure = ValueResult<int>.Failure(FailureException);
        int observed = 0;

        // When
        ValueResult successThenAction = success.Select(value => { observed = value; });
        ValueResult failureThenAction = failure.Select(value => { observed = -value; });
        ValueResult<string> successThenFunction = success.Select(value => $"Value: {value}");
        ValueResult<string> successThenThrow = success.Select<string>(_ => throw FailureException);
        ValueResult<string> failureThenFunction = failure.Select(value => $"Value: {value}");

        // Then
        Assert.True(successThenAction.IsSuccess);
        Assert.Same(FailureException, failureThenAction.GetExceptionOrDefault());
        Assert.Equal(123, observed);
        Assert.Equal("Value: 123", successThenFunction.GetValueOrThrow());
        Assert.Same(FailureException, successThenThrow.GetExceptionOrDefault());
        Assert.Same(FailureException, failureThenFunction.GetExceptionOrDefault());
    }

    [Fact(DisplayName = "ValueResult<T>.SelectMany should produce the expected result")]
    public void ValueResultSelectManyShouldProduceExpectedResult()
    {
        // Given
        ValueResult<int> success = ValueResult<int>.Success(123);
        ValueResult<int> failure = ValueResult<int>.Failure(FailureException);
        Exception otherException = new("Other");

        // When
        ValueResult successThenUntyped = success.SelectMany(_ => ValueResult.Failure(otherException));
        ValueResult failureThenUntyped = failure.SelectMany(_ => ValueResult.Success());
        ValueResult<string> successThenTyped = success.SelectMany(value => ValueResult<string>.Success($"Value: {value}"));
        ValueResult<string> successThenThrow = success.SelectMany<string>(_ => throw otherException);
        ValueResult<string> failureThenTyped = failure.SelectMany(value => ValueResult<string>.Success($"Value: {value}"));

        // Then
        Assert.Same(otherException, successThenUntyped.GetExceptionOrDefault());
        Assert.Same(FailureException, failureThenUntyped.GetExceptionOrDefault());
        Assert.Equal("Value: 123", successThenTyped.GetValueOrThrow());
        Assert.Same(otherException, successThenThrow.GetExceptionOrDefault());
        Assert.Same(FailureException, failureThenTyped.GetExceptionOrDefault());
    }

    [Fact(DisplayName = "ValueResult.Select and ValueResult.SelectMany should produce a typed result")]
    public void ValueResultSelectAndSelectManyShouldProduceTypedResult()
    {
        // Given
        ValueResult success = ValueResult.Success();
        ValueResult failure = ValueResult.Failure(FailureException);

        // When
        ValueResult<int> successThenSelect = success.Select(() => 123);
        ValueResult<int> failureThenSelect = failure.Select(() => 123);
        ValueResult<int> successThenSelectMany = success.SelectMany(() => ValueResult<int>.Success(456));
        ValueResult<int> successThenThrow = success.SelectMany<int>(() => throw FailureException);

        // Then
        Assert.Equal(123, successThenSelect.GetValueOrThrow());
        Assert.Same(FailureException, failureThenSelect.GetExceptionOrDefault());
        Assert.Equal(456, successThenSelectMany.GetValueOrThrow());
        Assert.Same(FailureException, successThenThrow.GetExceptionOrDefault());
    }

    [Fact(DisplayName = "ValueResult<T> default should propagate its uninitialised exception through Select")]
    public void ValueResultDefaultShouldPropagateItsUninitialisedExceptionThroughSelect()
    {
        // Given
        ValueResult<int> uninitialised = default;

        // When
        ValueResult<string> selected = uninitialised.Select(value => $"Value: {value}");

        // Then
        Assert.True(selected.IsFailure);
        Assert.Same(uninitialised.GetExceptionOrDefault(), selected.GetExceptionOrDefault());
    }

    [Fact(DisplayName = "ValueResult<T>.Throw should throw the underlying exception of a failure only")]
    public void ValueResultThrowShouldThrowTheUnderlyingExceptionOfFailureOnly()
    {
        // Given
        ValueResult<int> success = ValueResult<int>.Success(123);
        ValueResult<int> failure = ValueResult<int>.Failure(FailureException);

        // When
        success.Throw();
        Exception exception = Assert.Throws<Exception>(failure.Throw);

        // Then
        Assert.Same(FailureException, exception);
    }

    [Fact(DisplayName = "ValueResult<T>.ToResult and ValueResult<T>.From should round-trip through Result<T>")]
    public void ValueResultToResultAndFromShouldRoundTripThroughResult()
    {
        // Given
        ValueResult<int> success = ValueResult<int>.Success(123);
        ValueResult<int> failure = ValueResult<int>.Failure(FailureException);

        // When
        Result<int> successResult = success.ToResult();
        Result<int> failureResult = failure.ToResult();

        // Then
        Assert.Equal(123, Assert.IsType<Success<int>>(successResult).Value);
        Assert.Same(FailureException, Assert.IsType<Failure<int>>(failureResult).Exception);
        Assert.Equal(success, ValueResult<int>.From(successResult));
        Assert.Equal(failure, ValueResult<int>.From(failureResult));
    }

    [Fact(DisplayName = "ValueResult<T>.ToString should produce the expected result")]
    public void ValueResultToStringShouldProduceExpectedResult()
    {
        // Given
        ValueResult<int> success = ValueResult<int>.Success(123);
        ValueResult<string?> successOfNull = ValueResult<string?>.Success(null);
        ValueResult<int> failure = ValueResult<int>.Failure(FailureException);

        // When / Then
        Assert.Equal("123", success.ToString());
        Assert.Equal(string.Empty, successOfNull.ToString());
        Assert.Equal("Failure", failure.ToString());
    }
}
