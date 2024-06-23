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

    [Fact(DisplayName = "RequireWithinRange should throw an ArgumentOutOfRangeException when the condition is false")]
    public void RequireWithinRangeShouldThrowArgumentOutOfRangeExceptionWhenConditionIsFalse()
    {
        // When
        Exception exception = Assert.Throws<ArgumentOutOfRangeException>(() => RequireWithinRange(false));

        // Then
        Assert.Equal("Argument must be within range.", exception.Message);
    }

    [Theory(DisplayName = "RequireWithinRange should not throw an ArgumentOutOfRangeException when the condition is true")]
    [InlineData(100, 0, 123)]
    public void RequireWithinRangeShouldNotThrowArgumentOutOfRangeExceptionWhenConditionIsTrue(int value, int min, int max)
    {
        // Given / When / Then
        RequireWithinRange(value >= min && value <= max);
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
        // Given / When / Then
        RequireWithinRangeInclusive(1, 1, 3);
    }

    [Fact(DisplayName = "RequireWithinRangeInclusive should not throw an ArgumentOutOfRangeException when the value is exactly the maximum value")]
    public void RequireWithinRangeInclusiveShouldNotThrowArgumentOutOfRangeExceptionWhenValueIsExactlyTheMaximumValue()
    {
        // Given / When / Then
        RequireWithinRangeInclusive(3, 1, 3);
    }

    [Fact(DisplayName = "RequireWithinRangeInclusive should not throw an ArgumentOutOfRangeException when the value is between the specified range")]
    public void RequireWithinRangeInclusiveShouldNotThrowArgumentOutOfRangeExceptionWhenValueIsBetweenSpecifiedRange()
    {
        // Given / When / Then
        RequireWithinRangeInclusive(2, 1, 3);
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
        // Given / When / Then
        RequireWithinRangeExclusive(3, 2, 4);
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
