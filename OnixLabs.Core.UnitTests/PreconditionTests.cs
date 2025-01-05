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
using OnixLabs.Core.UnitTests.Data;
using Xunit;

namespace OnixLabs.Core.UnitTests;

public sealed class PreconditionTests
{
    private static readonly Exception Exception = new("Failure");

    [Fact(DisplayName = "Check should throw an InvalidOperationException when the condition is false")]
    public void CheckShouldThrowInvalidOperationExceptionWhenConditionIsFalse()
    {
        // When
        Exception exception = Assert.Throws<InvalidOperationException>(() => Check(false));

        // Then
        Assert.Equal("Argument must satisfy the specified condition.", exception.Message);
    }

    [Fact(DisplayName = "Check should not throw an InvalidOperationException when the condition is true")]
    public void CheckShouldNotThrowInvalidOperationExceptionWhenConditionIsTrue()
    {
        // Given / When / Then
        Check(true);
    }

    [Fact(DisplayName = "CheckIsFailure should throw an InvalidOperationException when the result is not a failure state")]
    public void CheckIsFailureShouldThrowAnInvalidOperationExceptionWhenTheResultIsNotAFailureState()
    {
        // Given
        Result result = Result.Success();

        // When
        Exception exception = Assert.Throws<InvalidOperationException>(() => CheckIsFailure(result));

        // Then
        Assert.Equal("Argument must be a Failure state.", exception.Message);
    }

    [Fact(DisplayName = "CheckIsFailure should return a Failure when the result is a failure state")]
    public void CheckIsFailureShouldReturnFailureWhenTheResultIsAFailureState()
    {
        // Given
        Result result = Exception;

        // When
        Result actual = CheckIsFailure(result);

        // Then
        Assert.IsType<Failure>(actual);
    }

    [Fact(DisplayName = "CheckIsSuccess should throw an InvalidOperationException when the result is not a success state")]
    public void CheckIsSuccessShouldThrowAnInvalidOperationExceptionWhenTheResultIsNotASuccessState()
    {
        // Given
        Result result = Exception;

        // When
        Exception exception = Assert.Throws<InvalidOperationException>(() => CheckIsSuccess(result));

        // Then
        Assert.Equal("Argument must be a Success state.", exception.Message);
    }

    [Fact(DisplayName = "CheckIsSuccess should return a Success when the result is a success state")]
    public void CheckIsSuccessShouldReturnFailureWhenTheResultIsASuccessState()
    {
        // Given
        Result result = Result.Success();

        // When
        Result actual = CheckIsSuccess(result);

        // Then
        Assert.IsType<Success>(actual);
    }

    [Fact(DisplayName = "CheckIsFailure<T> should throw an InvalidOperationException when the result is not a failure state")]
    public void CheckIsFailureTShouldThrowAnInvalidOperationExceptionWhenTheResultIsNotAFailureState()
    {
        // Given
        Result<int> result = 1;

        // When
        Exception exception = Assert.Throws<InvalidOperationException>(() => CheckIsFailure(result));

        // Then
        Assert.Equal("Argument must be a Failure state.", exception.Message);
    }

    [Fact(DisplayName = "CheckIsFailure<T> should return a Failure when the result is a failure state")]
    public void CheckIsFailureTShouldReturnFailureWhenTheResultIsAFailureState()
    {
        // Given
        Result<int> result = Exception;

        // When
        Result<int> actual = CheckIsFailure(result);

        // Then
        Assert.IsType<Failure<int>>(actual);
    }

    [Fact(DisplayName = "CheckIsSuccess<T> should throw an InvalidOperationException when the result is not a success state")]
    public void CheckIsSuccessTShouldThrowAnInvalidOperationExceptionWhenTheResultIsNotASuccessState()
    {
        // Given
        Result<int> result = Exception;

        // When
        Exception exception = Assert.Throws<InvalidOperationException>(() => CheckIsSuccess(result));

        // Then
        Assert.Equal("Argument must be a Success state.", exception.Message);
    }

    [Fact(DisplayName = "CheckIsSuccess<T> should return a Success when the result is a success state")]
    public void CheckIsSuccessTShouldReturnFailureWhenTheResultIsASuccessState()
    {
        // Given
        Result<int> result = 1;

        // When
        Result<int> actual = CheckIsSuccess(result);

        // Then
        Assert.IsType<Success<int>>(actual);
    }

    [Fact(DisplayName = "CheckIsNone<T> should throw an InvalidOperationException when the optional is not a None<T> value")]
    public void CheckIsNoneShouldThrowAnInvalidOperationExceptionWhenTheOptionalIsNotNoneValue()
    {
        // Given
        Optional<int> optional = 1;

        // When
        Exception exception = Assert.Throws<InvalidOperationException>(() => CheckIsNone(optional));

        // Then
        Assert.Equal("Argument must be a None<T> value.", exception.Message);
    }

    [Fact(DisplayName = "CheckIsNone<T> should return a None<T> when the optional is a None<T> value")]
    public void CheckIsNoneShouldReturnNoneWhenOptionalIsNoneValue()
    {
        // Given
        Optional<int> optional = Optional<int>.None;

        // When
        Optional<int> actual = CheckIsNone(optional);

        // Then
        Assert.IsType<None<int>>(actual);
    }

    [Fact(DisplayName = "CheckIsSome<T> should throw an InvalidOperationException when the optional is not a Some<T> value")]
    public void CheckIsSomeShouldThrowAnInvalidOperationExceptionWhenTheOptionalIsNotSomeValue()
    {
        // Given
        Optional<int> optional = Optional<int>.None;

        // When
        Exception exception = Assert.Throws<InvalidOperationException>(() => CheckIsSome(optional));

        // Then
        Assert.Equal("Argument must be a Some<T> value.", exception.Message);
    }

    [Fact(DisplayName = "CheckIsSome<T> should return a Some<T> when the optional is a Some<T> value")]
    public void CheckIsSomeShouldReturnSomeWhenOptionalIsSomeValue()
    {
        // Given
        Optional<int> optional = 1;

        // When
        Optional<int> actual = CheckIsSome(optional);

        // Then
        Assert.IsType<Some<int>>(actual);
    }

    [Fact(DisplayName = "CheckNotNull should throw an InvalidOperationException when the condition is null")]
    public void CheckNotNullShouldThrowInvalidOperationExceptionWhenConditionIsNull()
    {
        // When
        Exception exception = Assert.Throws<InvalidOperationException>(() => CheckNotNull<object>(null));

        // Then
        Assert.Equal("Argument must not be null.", exception.Message);
    }

    [Fact(DisplayName = "CheckNotNull should not throw an InvalidOperationException when the condition is not null")]
    public void CheckNotNullShouldNotThrowInvalidOperationExceptionWhenConditionIsNotNull()
    {
        // Given / When / Then
        CheckNotNull(new object());
    }

    [Fact(DisplayName = "CheckNotNull of ValueType should throw an InvalidOperationException when the condition is null")]
    public void CheckNotNullOfValueTypeShouldThrowInvalidOperationExceptionWhenConditionIsNull()
    {
        // Given
        int? expected = null;
        int actual = 0;

        // When
        Exception exception = Assert.Throws<InvalidOperationException>(() => actual = CheckNotNull(expected));

        // Then
        Assert.Equal(0, actual);
        Assert.Equal("Argument must not be null.", exception.Message);
    }

    [Fact(DisplayName = "CheckNotNull of ValueType should not throw an InvalidOperationException when the condition is not null")]
    public void CheckNotNullOfValueTypeShouldNotThrowInvalidOperationExceptionWhenConditionIsNotNull()
    {
        // Given
        int? expected = 123;

        // When
        int actual = CheckNotNull(expected);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "CheckNotNullOrEmpty should throw an InvalidOperationException when the value is null")]
    public void CheckNotNullOrEmptyShouldThrowInvalidOperationExceptionWhenValueIsNull()
    {
        // When
        Exception exception = Assert.Throws<InvalidOperationException>(() => CheckNotNullOrEmpty(null));

        // Then
        Assert.Equal("Argument must not be null or empty.", exception.Message);
    }

    [Fact(DisplayName = "CheckNotNullOrEmpty should throw an InvalidOperationException when the value is empty")]
    public void CheckNotNullOrEmptyShouldThrowInvalidOperationExceptionWhenValueIsEmpty()
    {
        // When
        Exception exception = Assert.Throws<InvalidOperationException>(() => CheckNotNullOrEmpty(string.Empty));

        // Then
        Assert.Equal("Argument must not be null or empty.", exception.Message);
    }

    [Fact(DisplayName = "CheckNotNullOrEmpty should return the argument value when the value is not null and not empty")]
    public void CheckNotNullShouldReturnArgumentValueWhenValueIsNotNullAndNotEmpty()
    {
        // Given
        const string expected = "Hello, World!";

        // When
        string actual = CheckNotNullOrEmpty(expected);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "CheckNotNullOrWhiteSpace should throw an InvalidOperationException when the value is null")]
    public void CheckNotNullOrWhiteSpaceShouldThrowInvalidOperationExceptionWhenValueIsNull()
    {
        // When
        Exception exception = Assert.Throws<InvalidOperationException>(() => CheckNotNullOrWhiteSpace(null));

        // Then
        Assert.Equal("Argument must not be null or whitespace.", exception.Message);
    }

    [Fact(DisplayName = "CheckNotNullOrWhiteSpace should throw an InvalidOperationException when the value is whitespace")]
    public void CheckNotNullOrWhiteSpaceShouldThrowInvalidOperationExceptionWhenValueIsWhiteSpace()
    {
        // When
        Exception exception = Assert.Throws<InvalidOperationException>(() => CheckNotNullOrWhiteSpace("    "));

        // Then
        Assert.Equal("Argument must not be null or whitespace.", exception.Message);
    }

    [Fact(DisplayName = "CheckNotNullOrWhiteSpace should return the argument value when the value is not null and not whitespace")]
    public void CheckNotNullShouldReturnArgumentValueWhenValueIsNotNullAndNotWhiteSpace()
    {
        // Given
        const string expected = "Hello, World!";

        // When
        string actual = CheckNotNullOrWhiteSpace(expected);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Require should throw an ArgumentException when the condition is false")]
    public void RequireShouldThrowArgumentExceptionWhenConditionIsFalse()
    {
        // When
        Exception exception = Assert.Throws<ArgumentException>(() => Require(false));

        // Then
        Assert.Equal("Argument must satisfy the specified condition.", exception.Message);
    }

    [Fact(DisplayName = "Require should not throw an InvalidOperationException when the condition is true")]
    public void RequireShouldNotThrowInvalidOperationExceptionWhenConditionIsTrue()
    {
        // Given / When / Then
        Require(true);
    }

    [Fact(DisplayName = "RequireIsFailure should throw an InvalidOperationException when the result is not a failure state")]
    public void RequireIsFailureShouldThrowAnInvalidOperationExceptionWhenTheResultIsNotAFailureState()
    {
        // Given
        Result result = Result.Success();

        // When
        Exception exception = Assert.Throws<ArgumentException>(() => RequireIsFailure(result));

        // Then
        Assert.Equal("Argument must be a Failure state.", exception.Message);
    }

    [Fact(DisplayName = "RequireIsFailure should return a Failure when the result is a failure state")]
    public void RequireIsFailureShouldReturnFailureWhenTheResultIsAFailureState()
    {
        // Given
        Result result = Exception;

        // When
        Result actual = RequireIsFailure(result);

        // Then
        Assert.IsType<Failure>(actual);
    }

    [Fact(DisplayName = "RequireIsSuccess should throw an InvalidOperationException when the result is not a success state")]
    public void RequireIsSuccessShouldThrowAnInvalidOperationExceptionWhenTheResultIsNotASuccessState()
    {
        // Given
        Result result = Exception;

        // When
        Exception exception = Assert.Throws<ArgumentException>(() => RequireIsSuccess(result));

        // Then
        Assert.Equal("Argument must be a Success state.", exception.Message);
    }

    [Fact(DisplayName = "RequireIsSuccess should return a Success when the result is a success state")]
    public void RequireIsSuccessShouldReturnFailureWhenTheResultIsASuccessState()
    {
        // Given
        Result result = Result.Success();

        // When
        Result actual = RequireIsSuccess(result);

        // Then
        Assert.IsType<Success>(actual);
    }

    [Fact(DisplayName = "RequireIsFailure<T> should throw an InvalidOperationException when the result is not a failure state")]
    public void RequireIsFailureTShouldThrowAnInvalidOperationExceptionWhenTheResultIsNotAFailureState()
    {
        // Given
        Result<int> result = 1;

        // When
        Exception exception = Assert.Throws<ArgumentException>(() => RequireIsFailure(result));

        // Then
        Assert.Equal("Argument must be a Failure state.", exception.Message);
    }

    [Fact(DisplayName = "RequireIsFailure<T> should return a Failure when the result is a failure state")]
    public void RequireIsFailureTShouldReturnFailureWhenTheResultIsAFailureState()
    {
        // Given
        Result<int> result = Exception;

        // When
        Result<int> actual = RequireIsFailure(result);

        // Then
        Assert.IsType<Failure<int>>(actual);
    }

    [Fact(DisplayName = "RequireIsSuccess<T> should throw an InvalidOperationException when the result is not a success state")]
    public void RequireIsSuccessTShouldThrowAnInvalidOperationExceptionWhenTheResultIsNotASuccessState()
    {
        // Given
        Result<int> result = Exception;

        // When
        Exception exception = Assert.Throws<ArgumentException>(() => RequireIsSuccess(result));

        // Then
        Assert.Equal("Argument must be a Success state.", exception.Message);
    }

    [Fact(DisplayName = "RequireIsSuccess<T> should return a Success when the result is a success state")]
    public void RequireIsSuccessTShouldReturnFailureWhenTheResultIsASuccessState()
    {
        // Given
        Result<int> result = 1;

        // When
        Result<int> actual = RequireIsSuccess(result);

        // Then
        Assert.IsType<Success<int>>(actual);
    }

    [Fact(DisplayName = "RequireIsNone<T> should throw an InvalidOperationException when the optional is not a None<T> value")]
    public void RequireIsNoneShouldThrowAnInvalidOperationExceptionWhenTheOptionalIsNotNoneValue()
    {
        // Given
        Optional<int> optional = 1;

        // When
        Exception exception = Assert.Throws<ArgumentException>(() => RequireIsNone(optional));

        // Then
        Assert.Equal("Argument must be a None<T> value.", exception.Message);
    }

    [Fact(DisplayName = "RequireIsNone<T> should return a None<T> when the optional is a None<T> value")]
    public void RequireIsNoneShouldReturnNoneWhenOptionalIsNoneValue()
    {
        // Given
        Optional<int> optional = Optional<int>.None;

        // When
        Optional<int> actual = RequireIsNone(optional);

        // Then
        Assert.IsType<None<int>>(actual);
    }

    [Fact(DisplayName = "RequireIsSome<T> should throw an InvalidOperationException when the optional is not a Some<T> value")]
    public void RequireIsSomeShouldThrowAnInvalidOperationExceptionWhenTheOptionalIsNotSomeValue()
    {
        // Given
        Optional<int> optional = Optional<int>.None;

        // When
        Exception exception = Assert.Throws<ArgumentException>(() => RequireIsSome(optional));

        // Then
        Assert.Equal("Argument must be a Some<T> value.", exception.Message);
    }

    [Fact(DisplayName = "RequireIsSome<T> should return a Some<T> when the optional is a Some<T> value")]
    public void RequireIsSomeShouldReturnSomeWhenOptionalIsSomeValue()
    {
        // Given
        Optional<int> optional = 1;

        // When
        Optional<int> actual = RequireIsSome(optional);

        // Then
        Assert.IsType<Some<int>>(actual);
    }

    [Fact(DisplayName = "RequireWithinRangeInclusive should throw an ArgumentOutOfRangeException when the value falls below the specified range")]
    public void RequireWithinRangeInclusiveShouldThrowArgumentOutOfRangeExceptionWhenValueFallsBelowSpecifiedRange()
    {
        // Given
        Exception exception = Assert.Throws<ArgumentOutOfRangeException>(() => RequireWithinRangeInclusive(1, 2, 3));

        // Then
        Assert.Equal("Argument must be within range.", exception.Message);
    }

    [Fact(DisplayName = "RequireWithinRangeInclusive should throw an ArgumentOutOfRangeException when the value falls above the specified range")]
    public void RequireWithinRangeInclusiveShouldThrowArgumentOutOfRangeExceptionWhenValueFallsAboveSpecifiedRange()
    {
        // Given
        Exception exception = Assert.Throws<ArgumentOutOfRangeException>(() => RequireWithinRangeInclusive(4, 2, 3));

        // Then
        Assert.Equal("Argument must be within range.", exception.Message);
    }

    [Fact(DisplayName = "RequireWithinRangeInclusive should not throw an ArgumentOutOfRangeException when the value is exactly the minimum value")]
    public void RequireWithinRangeInclusiveShouldNotThrowArgumentOutOfRangeExceptionWhenValueIsExactlyTheMinimumValue()
    {
        // Given
        const int expected = 1;

        // When
        int actual = RequireWithinRangeInclusive(expected, 1, 3);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "RequireWithinRangeInclusive should not throw an ArgumentOutOfRangeException when the value is exactly the maximum value")]
    public void RequireWithinRangeInclusiveShouldNotThrowArgumentOutOfRangeExceptionWhenValueIsExactlyTheMaximumValue()
    {
        // Given
        const int expected = 3;

        // When
        int actual = RequireWithinRangeInclusive(expected, 1, 3);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "RequireWithinRangeInclusive should not throw an ArgumentOutOfRangeException when the value is between the specified range")]
    public void RequireWithinRangeInclusiveShouldNotThrowArgumentOutOfRangeExceptionWhenValueIsBetweenSpecifiedRange()
    {
        // Given
        const int expected = 2;

        // When
        int actual = RequireWithinRangeInclusive(expected, 1, 3);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "RequireWithinRangeExclusive should throw an ArgumentOutOfRangeException when the value falls below the specified range")]
    public void RequireWithinRangeExclusiveShouldThrowArgumentOutOfRangeExceptionWhenValueFallsBelowSpecifiedRange()
    {
        // Given
        Exception exception = Assert.Throws<ArgumentOutOfRangeException>(() => RequireWithinRangeExclusive(2, 2, 4));

        // Then
        Assert.Equal("Argument must be within range.", exception.Message);
    }

    [Fact(DisplayName = "RequireWithinRangeExclusive should throw an ArgumentOutOfRangeException when the value falls above the specified range")]
    public void RequireWithinRangeExclusiveShouldThrowArgumentOutOfRangeExceptionWhenValueFallsAboveSpecifiedRange()
    {
        // Given
        Exception exception = Assert.Throws<ArgumentOutOfRangeException>(() => RequireWithinRangeExclusive(4, 2, 4));

        // Then
        Assert.Equal("Argument must be within range.", exception.Message);
    }

    [Fact(DisplayName = "RequireWithinRangeExclusive should not throw an ArgumentOutOfRangeException when the value falls between the specified range")]
    public void RequireWithinRangeExclusiveShouldThrowArgumentOutOfRangeExceptionWhenValueFallsBetweenSpecifiedRange()
    {
        // Given
        const int expected = 2;

        // When
        int actual = RequireWithinRangeExclusive(expected, 1, 3);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "RequireNotNull should throw an ArgumentNullException when the condition is null")]
    public void RequireNotNullShouldThrowArgumentNullExceptionWhenConditionIsNull()
    {
        // When
        Exception exception = Assert.Throws<ArgumentNullException>(() => RequireNotNull<object>(null));

        // Then
        Assert.Equal("Argument must not be null.", exception.Message);
    }

    [Fact(DisplayName = "RequireNotNull should not throw an InvalidOperationException when the condition is not null")]
    public void RequireNotNullShouldNotThrowInvalidOperationExceptionWhenConditionIsNotNull()
    {
        // Given / When / Then
        RequireNotNull(new object());
    }

    [Fact(DisplayName = "RequireNotNull of ValueType should throw an InvalidOperationException when the condition is null")]
    public void RequireNotNullOfValueTypeShouldThrowInvalidOperationExceptionWhenConditionIsNull()
    {
        // Given
        int? expected = null;
        int actual = 0;

        // When
        Exception exception = Assert.Throws<ArgumentNullException>(() => actual = RequireNotNull(expected));

        // Then
        Assert.Equal(0, actual);
        Assert.Equal("Argument must not be null.", exception.Message);
    }

    [Fact(DisplayName = "RequireNotNull of ValueType should not throw an InvalidOperationException when the condition is not null")]
    public void RequireNotNullOfValueTypeShouldNotThrowInvalidOperationExceptionWhenConditionIsNotNull()
    {
        // Given
        int? expected = 123;

        // When
        int actual = RequireNotNull(expected);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "RequireNotNullOrEmpty should throw an ArgumentException when the value is null")]
    public void RequireNotNullOrEmptyShouldThrowArgumentExceptionWhenValueIsNull()
    {
        // When
        Exception exception = Assert.Throws<ArgumentException>(() => RequireNotNullOrEmpty(null));

        // Then
        Assert.Equal("Argument must not be null or empty.", exception.Message);
    }

    [Fact(DisplayName = "RequireNotNullOrEmpty should throw an ArgumentException when the value is empty")]
    public void RequireNotNullOrEmptyShouldThrowArgumentExceptionWhenValueIsEmpty()
    {
        // When
        Exception exception = Assert.Throws<ArgumentException>(() => RequireNotNullOrEmpty(string.Empty));

        // Then
        Assert.Equal("Argument must not be null or empty.", exception.Message);
    }

    [Fact(DisplayName = "RequireNotNullOrEmpty should return the argument value when the value is not null and not empty")]
    public void RequireNotNullShouldReturnArgumentValueWhenValueIsNotNullAndNotEmpty()
    {
        // Given
        const string expected = "Hello, World!";

        // When
        string actual = RequireNotNullOrEmpty(expected);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "RequireNotNullOrWhiteSpace should throw an ArgumentException when the value is null")]
    public void RequireNotNullOrWhiteSpaceShouldThrowArgumentExceptionWhenValueIsNull()
    {
        // When
        Exception exception = Assert.Throws<ArgumentException>(() => RequireNotNullOrWhiteSpace(null));

        // Then
        Assert.Equal("Argument must not be null or whitespace.", exception.Message);
    }

    [Fact(DisplayName = "RequireNotNullOrWhiteSpace should throw an ArgumentException when the value is whitespace")]
    public void RequireNotNullOrWhiteSpaceShouldThrowArgumentExceptionWhenValueIsWhiteSpace()
    {
        // When
        Exception exception = Assert.Throws<ArgumentException>(() => RequireNotNullOrWhiteSpace("    "));

        // Then
        Assert.Equal("Argument must not be null or whitespace.", exception.Message);
    }

    [Fact(DisplayName = "RequireNotNullOrWhiteSpace should return the argument value when the value is not null and not whitespace")]
    public void RequireNotNullShouldReturnArgumentValueWhenValueIsNotNullAndNotWhiteSpace()
    {
        // Given
        const string expected = "Hello, World!";

        // When
        string actual = RequireNotNullOrWhiteSpace(expected);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "RequireIsDefined should throw an ArgumentOutOfRangeException when the specified enum value is not defined")]
    public void RequireIsDefinedShouldThrowArgumentOutOfRangeExceptionWhenSpecifiedEnumValueIsNotDefined()
    {
        // When
        Exception exception = Assert.Throws<ArgumentOutOfRangeException>(() => RequireIsDefined((Shape)2));

        // Then
        Assert.Equal("Invalid Shape enum value: 2. Valid values include: Square, Circle.", exception.Message);
    }

    [Fact(DisplayName = "RequireIsDefined should not throw an ArgumentOutOfRangeException when the specified enum value is defined")]
    public void RequireIsDefinedShouldNotThrowArgumentOutOfRangeExceptionWhenSpecifiedEnumValueIsDefined()
    {
        // Given / When / Then
        RequireIsDefined((Shape)1);
    }
}
