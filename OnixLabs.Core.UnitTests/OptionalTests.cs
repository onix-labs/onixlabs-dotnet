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

public sealed class OptionalTests
{
    [Fact(DisplayName = "Optional.None should produce the expected result.")]
    public void OptionalNoneShouldProduceExpectedResult()
    {
        // Given / When
        Optional<int> number = Optional<int>.None;
        Optional<string> text = Optional<string>.None;

        // Then
        Assert.True(Optional<int>.IsNone(number));
        Assert.False(number.HasValue);
        Assert.IsType<None<int>>(number);
        Assert.Equal(Optional<int>.None, number);

        Assert.True(Optional<string>.IsNone(text));
        Assert.False(text.HasValue);
        Assert.IsType<None<string>>(text);
        Assert.Equal(Optional<string>.None, text);
    }

    [Fact(DisplayName = "Optional.Of should produce the expected result for implicit default values.")]
    public void OptionalOfShouldProduceExpectedResultForImplicitDefaultValues()
    {
        // Given / When
        Optional<int> number = Optional<int>.Of(0);
        Optional<string> text = Optional<string>.Of(null);

        // Then
        Assert.True(Optional<int>.IsNone(number));
        Assert.False(number.HasValue);
        Assert.IsType<None<int>>(number);
        Assert.Equal(Optional<int>.None, number);

        Assert.True(Optional<string>.IsNone(text));
        Assert.False(text.HasValue);
        Assert.IsType<None<string>>(text);
        Assert.Equal(Optional<string>.None, text);
    }

    [Fact(DisplayName = "Optional.Of should produce the expected result for explicit default values.")]
    public void OptionalOfShouldProduceExpectedResultForExplicitDefaultValues()
    {
        // Given / When
        Optional<int> number = Optional<int>.Of(0);
        Optional<string> text = Optional<string>.Of(null);

        // Then
        Assert.True(Optional<int>.IsNone(number));
        Assert.False(number.HasValue);
        Assert.IsType<None<int>>(number);
        Assert.Equal(Optional<int>.None, number);

        Assert.True(Optional<string>.IsNone(text));
        Assert.False(text.HasValue);
        Assert.IsType<None<string>>(text);
        Assert.Equal(Optional<string>.None, text);
    }

    [Fact(DisplayName = "Optional.Of should produce the expected result for explicit non-default values.")]
    public void OptionalOfShouldProduceExpectedResultForExplicitNonDefaultValues()
    {
        // Given / When
        Optional<int> number = Optional<int>.Of(123);
        Optional<string> text = Optional<string>.Of("abc");

        // Then
        Assert.True(Optional<int>.IsSome(number));
        Assert.True(number.HasValue);
        Assert.IsType<Some<int>>(number);
        Assert.Equal(123, number);

        Assert.True(Optional<string>.IsSome(text));
        Assert.True(text.HasValue);
        Assert.IsType<Some<string>>(text);
        Assert.Equal("abc", text);
    }

    [Fact(DisplayName = "Optional.Of should produce the expected result for null nullable struct values.")]
    public void OptionalOfShouldProduceExpectedResultForNullNullableStructValues()
    {
        // Given / When
        Optional<int> number = Optional<int>.Of((int?)null);
        Optional<Guid> identifier = Optional<Guid>.Of((Guid?)null);

        // Then
        Assert.False(number.HasValue);
        Assert.IsType<None<int>>(number);

        Assert.False(identifier.HasValue);
        Assert.IsType<None<Guid>>(identifier);
    }

    [Fact(DisplayName = "Optional.Of should produce the expected result for non-null nullable struct values.")]
    public void OptionalOfShouldProduceExpectedResultForNonNullNullableStructValues()
    {
        // Given / When
        Optional<int> number = Optional<int>.Of((int?)123);
        Optional<Guid> identifier = Optional<Guid>.Of((Guid?)Guid.Empty);

        // Then
        Assert.True(Optional<int>.IsSome(number));
        Assert.True(number.HasValue);
        Assert.IsType<Some<int>>(number);
        Assert.Equal(123, number);

        Assert.True(Optional<Guid>.IsSome(identifier));
        Assert.True(identifier.HasValue);
        Assert.IsType<Some<Guid>>(identifier);
        Assert.Equal(Guid.Empty, identifier);
    }

    [Fact(DisplayName = "Optional.Some should produce the expected result.")]
    public void OptionalSomeShouldProduceExpectedResult()
    {
        // Given / When
        Optional<int> number = Optional<int>.Some(123);
        Optional<string> text = Optional<string>.Some("abc");

        // Then
        Assert.True(Optional<int>.IsSome(number));
        Assert.True(number.HasValue);
        Assert.IsType<Some<int>>(number);
        Assert.Equal(123, number);

        Assert.True(Optional<string>.IsSome(text));
        Assert.True(text.HasValue);
        Assert.IsType<Some<string>>(text);
        Assert.Equal("abc", text);
    }

    [Fact(DisplayName = "Optional implicit operator should produce the expected some result.")]
    public void OptionalImplicitOperatorShouldProduceExpectedSomeResult()
    {
        // Given / When
        Optional<int> number = 123;
        Optional<string> text = "abc";

        // Then
        Assert.True(Optional<int>.IsSome(number));
        Assert.True(number.HasValue);
        Assert.IsType<Some<int>>(number);
        Assert.Equal(123, number);

        Assert.True(Optional<string>.IsSome(text));
        Assert.True(text.HasValue);
        Assert.IsType<Some<string>>(text);
        Assert.Equal("abc", text);
    }

    [Fact(DisplayName = "Optional implicit operator should produce the expected none result.")]
    public void OptionalImplicitOperatorShouldProduceExpectedNoneResult()
    {
        // Given / When
        const string? value = null;
        Optional<string> optional = value;

        // Then
        Assert.False(optional.HasValue);
        Assert.IsType<None<string>>(optional);
    }

    [Fact(DisplayName = "Optional Some explicit operator should produce the expected result.")]
    public void OptionalSomeExplicitOperatorShouldProduceExpectedResult()
    {
        // Given
        Optional<int> number = Optional<int>.Some(123);
        Optional<string> text = Optional<string>.Some("abc");

        // When
        int underlyingNumber = (int)number;
        string underlyingText = (string)text;

        // Then
        Assert.Equal(123, underlyingNumber);
        Assert.Equal("abc", underlyingText);
    }

    [Fact(DisplayName = "Optional None explicit operator should produce the expected result.")]
    public void OptionalNoneExplicitOperatorShouldProduceExpectedResult()
    {
        // Given
        Optional<int> number = Optional<int>.None;
        Optional<string> text = Optional<string>.None;

        // When
        Exception numberException = Assert.Throws<InvalidOperationException>(() => (int)number);
        Exception textException = Assert.Throws<InvalidOperationException>(() => (string)text);

        // Then
        Assert.Equal("Optional value of type System.Int32 is not present.", numberException.Message);
        Assert.Equal("Optional value of type System.String is not present.", textException.Message);
    }

    [Fact(DisplayName = "Optional Some values should be considered equal.")]
    public void OptionalSomeValuesShouldBeConsideredEqual()
    {
        // Given
        Some<int> a = Optional<int>.Some(123);
        Some<int> b = Optional<int>.Some(123);
        object bObject = b;

        // When / Then
        Assert.Equal(a, b);
        Assert.Equal(a, bObject);
        Assert.True(a.Equals(b));
        Assert.True(a.Equals(bObject));
        Assert.True(a == b);
        Assert.False(a != b);
    }

    [Fact(DisplayName = "Optional Some values should not be considered equal.")]
    public void OptionalSomeValuesShouldNotBeConsideredEqual()
    {
        // Given
        Some<int> a = Optional<int>.Some(123);
        Some<int> b = Optional<int>.Some(456);
        object bObject = b;

        // When / Then
        Assert.NotEqual(a, b);
        Assert.NotEqual(a, bObject);
        Assert.False(a.Equals(b));
        Assert.False(a.Equals(bObject));
        Assert.False(a == b);
        Assert.True(a != b);
    }

    [Fact(DisplayName = "Optional None values should be considered equal.")]
    public void OptionalNoneValuesShouldBeConsideredEqual()
    {
        // Given
        None<int> a = Optional<int>.None;
        None<int> b = Optional<int>.None;
        object bObject = b;

        // When / Then
        Assert.Equal(a, b);
        Assert.Equal(a, bObject);
        Assert.True(a.Equals(b));
        Assert.True(a.Equals(bObject));
        Assert.True(a == b);
        Assert.False(a != b);
    }

    [Fact(DisplayName = "Optional Some and None values should not be considered equal.")]
    public void OptionalSomeAndNoneValuesShouldNotBeConsideredEqual()
    {
        // Given
        Optional<int> a = Optional<int>.Some(123);
        Optional<int> b = Optional<int>.None;
        object bObject = b;

        // When / Then
        Assert.NotEqual(a, b);
        Assert.NotEqual(a, bObject);
        Assert.False(a.Equals(b));
        Assert.False(a.Equals(bObject));
        Assert.False(a == b);
        Assert.True(a != b);
    }

    [Fact(DisplayName = "Optional Some.GetHashCode should produce the expected result.")]
    public void OptionalSomeGetHashCodeShouldProduceExpectedResult()
    {
        // Given
        const int expected = 123;
        Optional<int> optional = 123;

        // When
        int actual = optional.GetHashCode();

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Optional None.GetHashCode should produce the expected result.")]
    public void OptionalNoneGetHashCodeShouldProduceExpectedResult()
    {
        // Given
        const int expected = 0;
        Optional<int> optional = Optional<int>.None;

        // When
        int actual = optional.GetHashCode();

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Optional Some.GetValueOrDefault should produce the expected result.")]
    public void OptionalSomeGetValueOrDefaultShouldProduceExpectedResult()
    {
        // Given
        Optional<int> number = 123;
        Optional<string> text = "abc";

        // When
        int actualNumber = number.GetValueOrDefault();
        string? actualText = text.GetValueOrDefault();

        // Then
        Assert.Equal(123, actualNumber);
        Assert.Equal("abc", actualText);
    }

    [Fact(DisplayName = "Optional None.GetValueOrDefault should produce the expected result.")]
    public void OptionalNoneGetValueOrDefaultShouldProduceExpectedResult()
    {
        // Given
        Optional<int> number = Optional<int>.None;
        Optional<string> text = Optional<string>.None;

        // When
        int actualNumber = number.GetValueOrDefault();
        string? actualText = text.GetValueOrDefault();

        // Then
        Assert.Equal(0, actualNumber);
        Assert.Equal(null, actualText);
    }

    [Fact(DisplayName = "Optional Some.GetValueOrDefault with default value should produce the expected result.")]
    public void OptionalSomeGetValueOrDefaultWithDefaultValueShouldProduceExpectedResult()
    {
        // Given
        Optional<int> number = 123;
        Optional<string> text = "abc";

        // When
        int? actualNumber = number.GetValueOrDefault(456);
        // ReSharper disable once VariableCanBeNotNullable
        string? actualText = text.GetValueOrDefault("xyz");

        // Then
        Assert.Equal(123, actualNumber);
        Assert.Equal("abc", actualText);
    }

    [Fact(DisplayName = "Optional None.GetValueOrDefault with default value should produce the expected result.")]
    public void OptionalNoneGetValueOrDefaultWithDefaultValueShouldProduceExpectedResult()
    {
        // Given
        Optional<int> number = Optional<int>.None;
        Optional<string> text = Optional<string>.None;

        // When
        int actualNumber = number.GetValueOrDefault(456);
        string actualText = text.GetValueOrDefault("xyz");

        // Then
        Assert.Equal(456, actualNumber);
        Assert.Equal("xyz", actualText);
    }

    [Fact(DisplayName = "Optional Some.GetValueOrThrow should produce the expected result.")]
    public void OptionalSomeGetValueOrThrowShouldProduceExpectedResult()
    {
        // Given
        Optional<int> number = 123;
        Optional<string> text = "abc";

        // When
        int actualNumber = number.GetValueOrThrow();
        string actualText = text.GetValueOrThrow();

        // Then
        Assert.Equal(123, actualNumber);
        Assert.Equal("abc", actualText);
    }

    [Fact(DisplayName = "Optional None.GetValueOrThrow should produce the expected result.")]
    public void OptionalNoneGetValueOrThrowShouldProduceExpectedResult()
    {
        // Given
        Optional<int> number = Optional<int>.None;
        Optional<string> text = Optional<string>.None;

        // When
        Exception numberException = Assert.Throws<InvalidOperationException>(() => number.GetValueOrThrow());
        Exception textException = Assert.Throws<InvalidOperationException>(() => text.GetValueOrThrow());

        // Then
        Assert.Equal("Optional value of type System.Int32 is not present.", numberException.Message);
        Assert.Equal("Optional value of type System.String is not present.", textException.Message);
    }

    [Fact(DisplayName = "Optional Some.Match should execute the some action.")]
    public void OptionalSomeMatchShouldExecuteSomeAction()
    {
        // Given
        bool someCalled = false;
        Optional<int> optional = 123;

        // When
        optional.Match(some: _ => { someCalled = true; });

        // Then
        Assert.True(someCalled);
    }

    [Fact(DisplayName = "Optional None.Match should execute the none action.")]
    public void OptionalNoneMatchShouldExecuteNoneAction()
    {
        // Given
        bool noneCalled = false;
        Optional<int> optional = Optional<int>.None;

        // When
        optional.Match(none: () => { noneCalled = true; });

        // Then
        Assert.True(noneCalled);
    }

    [Fact(DisplayName = "Optional Some.Match should produce the expected result.")]
    public void OptionalSomeMatchShouldProduceExpectedResult()
    {
        // Given
        const int expected = 9;
        Optional<int> optional = 3;

        // When
        int actual = optional.Match(
            some: value => value * value,
            none: () => 0
        );

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Optional None.Match should produce the expected result.")]
    public void OptionalNoneMatchShouldProduceExpectedResult()
    {
        // Given
        const int expected = 0;
        Optional<int> optional = Optional<int>.None;

        // When
        int actual = optional.Match(
            some: value => value * value,
            none: () => 0
        );

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Optional Some.Select should produce the expected result")]
    public void OptionalSomeSelectShouldProduceExpectedResult()
    {
        // Given
        const int expected = 9;
        Optional<int> optional = 3;

        // When
        Optional<int> actual = optional.Select(value => value * value);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Optional None.Select should produce the expected result")]
    public void OptionalNoneSelectShouldProduceExpectedResult()
    {
        // Given
        Optional<int> optional = Optional<int>.None;

        // When
        Optional<int> actual = optional.Select(value => value * value);

        // Then
        Assert.Equal(Optional<int>.None, actual);
    }

    [Fact(DisplayName = "Optional Some.SelectMany should produce the expected result")]
    public void OptionalSomeSelectManyShouldProduceExpectedResult()
    {
        // Given
        const int expected = 9;
        Optional<int> optional = 3;

        // When
        Optional<int> actual = optional.SelectMany<int>(value => value * value);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Optional None.SelectMany should produce the expected result")]
    public void OptionalNoneSelectManyShouldProduceExpectedResult()
    {
        // Given
        Optional<int> optional = Optional<int>.None;

        // When
        Optional<int> actual = optional.SelectMany<int>(value => value * value);

        // Then
        Assert.Equal(Optional<int>.None, actual);
    }

    [Fact(DisplayName = "Optional Some.ToString should produce the expected result.")]
    public void OptionalSomeToStringShouldProduceExpectedResult()
    {
        // Given
        Optional<int> number = 123;
        Optional<string> text = "abc";

        // When
        string numberString = number.ToString();
        string textString = text.ToString();

        // Then
        Assert.Equal("123", numberString);
        Assert.Equal("abc", textString);
    }

    [Fact(DisplayName = "Optional None.ToString should produce the expected result.")]
    public void OptionalNoneToStringShouldProduceExpectedResult()
    {
        // Given
        Optional<int> number = Optional<int>.None;
        Optional<string> text = Optional<string>.None;

        // When
        string numberString = number.ToString();
        string textString = text.ToString();

        // Then
        Assert.Equal("None", numberString);
        Assert.Equal("None", textString);
    }
}
