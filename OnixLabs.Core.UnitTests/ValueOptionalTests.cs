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

public sealed class ValueOptionalTests
{
    [Fact(DisplayName = "ValueOptional<T>.None should produce the expected result.")]
    public void ValueOptionalNoneShouldProduceExpectedResult()
    {
        // Given / When
        ValueOptional<int> number = ValueOptional<int>.None;
        ValueOptional<string> text = ValueOptional<string>.None;

        // Then
        Assert.True(ValueOptional<int>.IsNone(number));
        Assert.False(number.HasValue);
        Assert.Equal(ValueOptional<int>.None, number);

        Assert.True(ValueOptional<string>.IsNone(text));
        Assert.False(text.HasValue);
        Assert.Equal(ValueOptional<string>.None, text);
    }

    [Fact(DisplayName = "ValueOptional<T> implicit conversion should produce the expected result for default values.")]
    public void ValueOptionalImplicitConversionShouldProduceExpectedResultForDefaultValues()
    {
        // Given / When
        ValueOptional<int> number = 0;
        ValueOptional<string> text = (string?)null;

        // Then
        Assert.True(ValueOptional<int>.IsSome(number));
        Assert.True(number.HasValue);
        Assert.Equal(0, number.GetValueOrThrow());
        Assert.Equal(0, number);

        Assert.True(ValueOptional<string>.IsNone(text));
        Assert.False(text.HasValue);
        Assert.Equal(ValueOptional<string>.None, text);
    }

    [Fact(DisplayName = "ValueOptional<T>.Of should produce the expected result for explicit default values.")]
    public void ValueOptionalOfShouldProduceExpectedResultForExplicitDefaultValues()
    {
        // Given / When
        ValueOptional<int> number = ValueOptional<int>.Of(0);
        ValueOptional<string> text = ValueOptional<string>.Of(null);

        // Then
        Assert.True(ValueOptional<int>.IsNone(number));
        Assert.False(number.HasValue);
        Assert.Equal(ValueOptional<int>.None, number);

        Assert.True(ValueOptional<string>.IsNone(text));
        Assert.False(text.HasValue);
        Assert.Equal(ValueOptional<string>.None, text);
    }

    [Fact(DisplayName = "ValueOptional<T>.Of should produce the expected result for explicit non-default values.")]
    public void ValueOptionalOfShouldProduceExpectedResultForExplicitNonDefaultValues()
    {
        // Given / When
        ValueOptional<int> number = ValueOptional<int>.Of(123);
        ValueOptional<string> text = ValueOptional<string>.Of("abc");

        // Then
        Assert.True(ValueOptional<int>.IsSome(number));
        Assert.True(number.HasValue);
        Assert.Equal(123, number.GetValueOrThrow());
        Assert.Equal(123, number);

        Assert.True(ValueOptional<string>.IsSome(text));
        Assert.True(text.HasValue);
        Assert.Equal("abc", text.GetValueOrThrow());
        Assert.Equal("abc", text);
    }

    [Fact(DisplayName = "ValueOptional<T>.Of should produce the expected result for null nullable struct values.")]
    public void ValueOptionalOfShouldProduceExpectedResultForNullNullableStructValues()
    {
        // Given / When
        ValueOptional<int> number = ValueOptional<int>.Of((int?)null);
        ValueOptional<Guid> identifier = ValueOptional<Guid>.Of((Guid?)null);

        // Then
        Assert.False(number.HasValue);
        Assert.False(identifier.HasValue);
    }

    [Fact(DisplayName = "ValueOptional<T>.Of should produce the expected result for non-null nullable struct values.")]
    public void ValueOptionalOfShouldProduceExpectedResultForNonNullNullableStructValues()
    {
        // Given / When
        ValueOptional<int> number = ValueOptional<int>.Of((int?)123);
        ValueOptional<Guid> identifier = ValueOptional<Guid>.Of((Guid?)Guid.Empty);

        // Then
        Assert.True(ValueOptional<int>.IsSome(number));
        Assert.True(number.HasValue);
        Assert.Equal(123, number.GetValueOrThrow());
        Assert.Equal(123, number);

        Assert.True(ValueOptional<Guid>.IsSome(identifier));
        Assert.True(identifier.HasValue);
        Assert.Equal(Guid.Empty, identifier.GetValueOrThrow());
        Assert.Equal(Guid.Empty, identifier);
    }

    [Fact(DisplayName = "ValueOptional<T>.Some should produce the expected result.")]
    public void ValueOptionalSomeShouldProduceExpectedResult()
    {
        // Given / When
        ValueOptional<int> number = ValueOptional<int>.Some(123);
        ValueOptional<string> text = ValueOptional<string>.Some("abc");

        // Then
        Assert.True(ValueOptional<int>.IsSome(number));
        Assert.True(number.HasValue);
        Assert.Equal(123, number.GetValueOrThrow());
        Assert.Equal(123, number);

        Assert.True(ValueOptional<string>.IsSome(text));
        Assert.True(text.HasValue);
        Assert.Equal("abc", text.GetValueOrThrow());
        Assert.Equal("abc", text);
    }

    [Fact(DisplayName = "ValueOptional<T> implicit operator should produce the expected some result.")]
    public void ValueOptionalImplicitOperatorShouldProduceExpectedSomeResult()
    {
        // Given / When
        ValueOptional<int> number = 123;
        ValueOptional<string> text = "abc";

        // Then
        Assert.True(ValueOptional<int>.IsSome(number));
        Assert.True(number.HasValue);
        Assert.Equal(123, number.GetValueOrThrow());
        Assert.Equal(123, number);

        Assert.True(ValueOptional<string>.IsSome(text));
        Assert.True(text.HasValue);
        Assert.Equal("abc", text.GetValueOrThrow());
        Assert.Equal("abc", text);
    }

    [Fact(DisplayName = "ValueOptional<T> implicit operator should produce the expected none result.")]
    public void ValueOptionalImplicitOperatorShouldProduceExpectedNoneResult()
    {
        // Given / When
        const string? value = null;
        ValueOptional<string> optional = value;

        // Then
        Assert.False(optional.HasValue);
    }

    [Fact(DisplayName = "ValueOptional<T> Some explicit operator should produce the expected result.")]
    public void ValueOptionalSomeExplicitOperatorShouldProduceExpectedResult()
    {
        // Given
        ValueOptional<int> number = ValueOptional<int>.Some(123);
        ValueOptional<string> text = ValueOptional<string>.Some("abc");

        // When
        int underlyingNumber = (int)number;
        string underlyingText = (string)text;

        // Then
        Assert.Equal(123, underlyingNumber);
        Assert.Equal("abc", underlyingText);
    }

    [Fact(DisplayName = "ValueOptional<T> None explicit operator should produce the expected result.")]
    public void ValueOptionalNoneExplicitOperatorShouldProduceExpectedResult()
    {
        // Given
        ValueOptional<int> number = ValueOptional<int>.None;
        ValueOptional<string> text = ValueOptional<string>.None;

        // When
        Exception numberException = Assert.Throws<InvalidOperationException>(() => (int)number);
        Exception textException = Assert.Throws<InvalidOperationException>(() => (string)text);

        // Then
        Assert.Equal("Optional value of type System.Int32 is not present.", numberException.Message);
        Assert.Equal("Optional value of type System.String is not present.", textException.Message);
    }

    [Fact(DisplayName = "ValueOptional<T> Some values should be considered equal.")]
    public void ValueOptionalSomeValuesShouldBeConsideredEqual()
    {
        // Given
        ValueOptional<int> a = ValueOptional<int>.Some(123);
        ValueOptional<int> b = ValueOptional<int>.Some(123);
        object bObject = b;

        // When / Then
        Assert.Equal(a, b);
        Assert.Equal(a, bObject);
        Assert.True(a.Equals(b));
        Assert.True(a.Equals(bObject));
        Assert.True(a == b);
        Assert.False(a != b);
    }

    [Fact(DisplayName = "ValueOptional<T> Some values should not be considered equal.")]
    public void ValueOptionalSomeValuesShouldNotBeConsideredEqual()
    {
        // Given
        ValueOptional<int> a = ValueOptional<int>.Some(123);
        ValueOptional<int> b = ValueOptional<int>.Some(456);
        object bObject = b;

        // When / Then
        Assert.NotEqual(a, b);
        Assert.NotEqual(a, bObject);
        Assert.False(a.Equals(b));
        Assert.False(a.Equals(bObject));
        Assert.False(a == b);
        Assert.True(a != b);
    }

    [Fact(DisplayName = "ValueOptional<T> None values should be considered equal.")]
    public void ValueOptionalNoneValuesShouldBeConsideredEqual()
    {
        // Given
        ValueOptional<int> a = ValueOptional<int>.None;
        ValueOptional<int> b = ValueOptional<int>.None;
        object bObject = b;

        // When / Then
        Assert.Equal(a, b);
        Assert.Equal(a, bObject);
        Assert.True(a.Equals(b));
        Assert.True(a.Equals(bObject));
        Assert.True(a == b);
        Assert.False(a != b);
    }

    [Fact(DisplayName = "ValueOptional<T> Some and None values should not be considered equal.")]
    public void ValueOptionalSomeAndNoneValuesShouldNotBeConsideredEqual()
    {
        // Given
        ValueOptional<int> a = ValueOptional<int>.Some(123);
        ValueOptional<int> b = ValueOptional<int>.None;
        object bObject = b;

        // When / Then
        Assert.NotEqual(a, b);
        Assert.NotEqual(a, bObject);
        Assert.False(a.Equals(b));
        Assert.False(a.Equals(bObject));
        Assert.False(a == b);
        Assert.True(a != b);
    }

    [Fact(DisplayName = "ValueOptional<T> Some.GetHashCode should produce the expected result.")]
    public void ValueOptionalSomeGetHashCodeShouldProduceExpectedResult()
    {
        // Given
        const int expected = 123;
        ValueOptional<int> optional = 123;

        // When
        int actual = optional.GetHashCode();

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueOptional<T> None.GetHashCode should produce the expected result.")]
    public void ValueOptionalNoneGetHashCodeShouldProduceExpectedResult()
    {
        // Given
        const int expected = 0;
        ValueOptional<int> optional = ValueOptional<int>.None;

        // When
        int actual = optional.GetHashCode();

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueOptional<T> Some.GetValueOrDefault should produce the expected result.")]
    public void ValueOptionalSomeGetValueOrDefaultShouldProduceExpectedResult()
    {
        // Given
        ValueOptional<int> number = 123;
        ValueOptional<string> text = "abc";

        // When
        int actualNumber = number.GetValueOrDefault();
        string? actualText = text.GetValueOrDefault();

        // Then
        Assert.Equal(123, actualNumber);
        Assert.Equal("abc", actualText);
    }

    [Fact(DisplayName = "ValueOptional<T> None.GetValueOrDefault should produce the expected result.")]
    public void ValueOptionalNoneGetValueOrDefaultShouldProduceExpectedResult()
    {
        // Given
        ValueOptional<int> number = ValueOptional<int>.None;
        ValueOptional<string> text = ValueOptional<string>.None;

        // When
        int actualNumber = number.GetValueOrDefault();
        string? actualText = text.GetValueOrDefault();

        // Then
        Assert.Equal(0, actualNumber);
        Assert.Null(actualText);
    }

    [Fact(DisplayName = "ValueOptional<T> Some.GetValueOrDefault with default value should produce the expected result.")]
    public void ValueOptionalSomeGetValueOrDefaultWithDefaultValueShouldProduceExpectedResult()
    {
        // Given
        ValueOptional<int> number = 123;
        ValueOptional<string> text = "abc";

        // When
        int? actualNumber = number.GetValueOrDefault(456);
        // ReSharper disable once VariableCanBeNotNullable
        string? actualText = text.GetValueOrDefault("xyz");

        // Then
        Assert.Equal(123, actualNumber);
        Assert.Equal("abc", actualText);
    }

    [Fact(DisplayName = "ValueOptional<T> None.GetValueOrDefault with default value should produce the expected result.")]
    public void ValueOptionalNoneGetValueOrDefaultWithDefaultValueShouldProduceExpectedResult()
    {
        // Given
        ValueOptional<int> number = ValueOptional<int>.None;
        ValueOptional<string> text = ValueOptional<string>.None;

        // When
        int actualNumber = number.GetValueOrDefault(456);
        string actualText = text.GetValueOrDefault("xyz");

        // Then
        Assert.Equal(456, actualNumber);
        Assert.Equal("xyz", actualText);
    }

    [Fact(DisplayName = "ValueOptional<T> Some.GetValueOrThrow should produce the expected result.")]
    public void ValueOptionalSomeGetValueOrThrowShouldProduceExpectedResult()
    {
        // Given
        ValueOptional<int> number = 123;
        ValueOptional<string> text = "abc";

        // When
        int actualNumber = number.GetValueOrThrow();
        string actualText = text.GetValueOrThrow();

        // Then
        Assert.Equal(123, actualNumber);
        Assert.Equal("abc", actualText);
    }

    [Fact(DisplayName = "ValueOptional<T> None.GetValueOrThrow should produce the expected result.")]
    public void ValueOptionalNoneGetValueOrThrowShouldProduceExpectedResult()
    {
        // Given
        ValueOptional<int> number = ValueOptional<int>.None;
        ValueOptional<string> text = ValueOptional<string>.None;

        // When
        Exception numberException = Assert.Throws<InvalidOperationException>(() => number.GetValueOrThrow());
        Exception textException = Assert.Throws<InvalidOperationException>(() => text.GetValueOrThrow());

        // Then
        Assert.Equal("Optional value of type System.Int32 is not present.", numberException.Message);
        Assert.Equal("Optional value of type System.String is not present.", textException.Message);
    }

    [Fact(DisplayName = "ValueOptional<T> Some.Match should execute the some action.")]
    public void ValueOptionalSomeMatchShouldExecuteSomeAction()
    {
        // Given
        bool someCalled = false;
        ValueOptional<int> optional = 123;

        // When
        optional.Match(some: _ => { someCalled = true; });

        // Then
        Assert.True(someCalled);
    }

    [Fact(DisplayName = "ValueOptional<T> None.Match should execute the none action.")]
    public void ValueOptionalNoneMatchShouldExecuteNoneAction()
    {
        // Given
        bool noneCalled = false;
        ValueOptional<int> optional = ValueOptional<int>.None;

        // When
        optional.Match(none: () => { noneCalled = true; });

        // Then
        Assert.True(noneCalled);
    }

    [Fact(DisplayName = "ValueOptional<T> Some.Match should produce the expected result.")]
    public void ValueOptionalSomeMatchShouldProduceExpectedResult()
    {
        // Given
        const int expected = 9;
        ValueOptional<int> optional = 3;

        // When
        int actual = optional.Match(
            some: value => value * value,
            none: () => 0
        );

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueOptional<T> None.Match should produce the expected result.")]
    public void ValueOptionalNoneMatchShouldProduceExpectedResult()
    {
        // Given
        const int expected = 0;
        ValueOptional<int> optional = ValueOptional<int>.None;

        // When
        int actual = optional.Match(
            some: value => value * value,
            none: () => 0
        );

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueOptional<T> Some.Select should produce the expected result")]
    public void ValueOptionalSomeSelectShouldProduceExpectedResult()
    {
        // Given
        const int expected = 9;
        ValueOptional<int> optional = 3;

        // When
        ValueOptional<int> actual = optional.Select(value => value * value);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueOptional<T> None.Select should produce the expected result")]
    public void ValueOptionalNoneSelectShouldProduceExpectedResult()
    {
        // Given
        ValueOptional<int> optional = ValueOptional<int>.None;

        // When
        ValueOptional<int> actual = optional.Select(value => value * value);

        // Then
        Assert.Equal(ValueOptional<int>.None, actual);
    }

    [Fact(DisplayName = "ValueOptional<T> Some.SelectMany should produce the expected result")]
    public void ValueOptionalSomeSelectManyShouldProduceExpectedResult()
    {
        // Given
        const int expected = 9;
        ValueOptional<int> optional = 3;

        // When
        ValueOptional<int> actual = optional.SelectMany<int>(value => value * value);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueOptional<T> None.SelectMany should produce the expected result")]
    public void ValueOptionalNoneSelectManyShouldProduceExpectedResult()
    {
        // Given
        ValueOptional<int> optional = ValueOptional<int>.None;

        // When
        ValueOptional<int> actual = optional.SelectMany<int>(value => value * value);

        // Then
        Assert.Equal(ValueOptional<int>.None, actual);
    }

    [Fact(DisplayName = "ValueOptional<T> Some.ToString should produce the expected result.")]
    public void ValueOptionalSomeToStringShouldProduceExpectedResult()
    {
        // Given
        ValueOptional<int> number = 123;
        ValueOptional<string> text = "abc";

        // When
        string numberString = number.ToString();
        string textString = text.ToString();

        // Then
        Assert.Equal("123", numberString);
        Assert.Equal("abc", textString);
    }

    [Fact(DisplayName = "ValueOptional<T> None.ToString should produce the expected result.")]
    public void ValueOptionalNoneToStringShouldProduceExpectedResult()
    {
        // Given
        ValueOptional<int> number = ValueOptional<int>.None;
        ValueOptional<string> text = ValueOptional<string>.None;

        // When
        string numberString = number.ToString();
        string textString = text.ToString();

        // Then
        Assert.Equal("None", numberString);
        Assert.Equal("None", textString);
    }

    [Fact(DisplayName = "ValueOptional<T> default should be None.")]
    public void ValueOptionalDefaultShouldBeNone()
    {
        // Given / When
        ValueOptional<int> number = default;
        ValueOptional<string> text = default;

        // Then
        Assert.True(ValueOptional<int>.IsNone(number));
        Assert.False(number.HasValue);
        Assert.Equal(ValueOptional<int>.None, number);
        Assert.Equal(0, number.GetHashCode());
        Assert.Equal("None", number.ToString());

        Assert.True(ValueOptional<string>.IsNone(text));
        Assert.False(text.HasValue);
        Assert.Equal(ValueOptional<string>.None, text);
        Assert.Null(text.GetValueOrDefault());
    }

    [Fact(DisplayName = "ValueOptional<T>.ToOptional and ValueOptional<T>.From should round-trip a Some value.")]
    public void ValueOptionalToOptionalAndFromShouldRoundTripSomeValue()
    {
        // Given
        ValueOptional<int> number = 123;
        ValueOptional<string> text = "abc";

        // When
        Optional<int> numberOptional = number.ToOptional();
        Optional<string> textOptional = text.ToOptional();
        ValueOptional<int> numberRoundTrip = ValueOptional<int>.From(numberOptional);
        ValueOptional<string> textRoundTrip = ValueOptional<string>.From(textOptional);

        // Then
        Assert.IsType<Some<int>>(numberOptional);
        Assert.Equal(123, numberOptional);
        Assert.IsType<Some<string>>(textOptional);
        Assert.Equal("abc", textOptional);

        Assert.True(numberRoundTrip.HasValue);
        Assert.Equal(number, numberRoundTrip);
        Assert.True(textRoundTrip.HasValue);
        Assert.Equal(text, textRoundTrip);
    }

    [Fact(DisplayName = "ValueOptional<T>.ToOptional and ValueOptional<T>.From should round-trip a None value.")]
    public void ValueOptionalToOptionalAndFromShouldRoundTripNoneValue()
    {
        // Given
        ValueOptional<int> number = ValueOptional<int>.None;
        ValueOptional<string> text = ValueOptional<string>.None;

        // When
        Optional<int> numberOptional = number.ToOptional();
        Optional<string> textOptional = text.ToOptional();
        ValueOptional<int> numberRoundTrip = ValueOptional<int>.From(numberOptional);
        ValueOptional<string> textRoundTrip = ValueOptional<string>.From(textOptional);

        // Then
        Assert.IsType<None<int>>(numberOptional);
        Assert.IsType<None<string>>(textOptional);

        Assert.False(numberRoundTrip.HasValue);
        Assert.Equal(number, numberRoundTrip);
        Assert.False(textRoundTrip.HasValue);
        Assert.Equal(text, textRoundTrip);
    }

    [Fact(DisplayName = "ValueOptional<T>.From should preserve a Some value that is the default value of T.")]
    public void ValueOptionalFromShouldPreserveSomeValueThatIsDefaultValueOfT()
    {
        // Given
        Optional<int> optional = Optional<int>.Some(0);

        // When
        ValueOptional<int> actual = ValueOptional<int>.From(optional);

        // Then
        Assert.True(actual.HasValue);
        Assert.Equal(0, actual.GetValueOrThrow());
        Assert.IsType<Some<int>>(actual.ToOptional());
    }

    [Fact(DisplayName = "ValueOptional<T>.From should throw ArgumentNullException when the optional is null.")]
    public void ValueOptionalFromShouldThrowArgumentNullExceptionWhenOptionalIsNull()
    {
        // Given
        Optional<string> optional = null!;

        // When / Then
        Assert.Throws<ArgumentNullException>(() => ValueOptional<string>.From(optional));
    }

    [Fact(DisplayName = "ValueOptional<T> Some.ToResult should produce a successful ValueResult<ValueOptional<T>>.")]
    public void ValueOptionalSomeToResultShouldProduceSuccessfulValueResult()
    {
        // Given
        ValueOptional<int> optional = 123;

        // When
        ValueResult<ValueOptional<int>> result = optional.ToResult();

        // Then
        Assert.True(result.IsSuccess);
        Assert.Equal(optional, result.GetValueOrThrow());
        Assert.True(result.GetValueOrThrow().HasValue);
        Assert.Equal(123, result.GetValueOrThrow().GetValueOrThrow());
    }

    [Fact(DisplayName = "ValueOptional<T> None.ToResult should produce a successful ValueResult<ValueOptional<T>>.")]
    public void ValueOptionalNoneToResultShouldProduceSuccessfulValueResult()
    {
        // Given
        ValueOptional<int> optional = ValueOptional<int>.None;

        // When
        ValueResult<ValueOptional<int>> result = optional.ToResult();

        // Then
        Assert.True(result.IsSuccess);
        Assert.Equal(ValueOptional<int>.None, result.GetValueOrThrow());
        Assert.False(result.GetValueOrThrow().HasValue);
    }

    [Fact(DisplayName = "ValueOptional<T>.SelectAsync should produce None when the selector returns null, like Select.")]
    public async Task ValueOptionalSelectAsyncShouldProduceNoneWhenSelectorReturnsNull()
    {
        // Given
        ValueOptional<int> optional = 21;

        // When
        ValueOptional<string> expected = optional.Select<string>(_ => null!);
        ValueOptional<string> actualTask = await optional.SelectAsync(_ => Task.FromResult<string>(null!), TestContext.Current.CancellationToken);
        ValueOptional<string> actualValueTask = await optional.SelectAsync(_ => Yielding.ValueTaskOf<string>(null!), TestContext.Current.CancellationToken);

        // Then
        Assert.False(expected.HasValue);
        Assert.False(actualTask.HasValue);
        Assert.False(actualValueTask.HasValue);
    }

    [Fact(DisplayName = "ValueOptional<T>.MatchAsync of Some should invoke the some delegate (Func<T, Task>, Action).")]
    public async Task ValueOptionalMatchAsyncOfSomeShouldInvokeSomeDelegateFuncTTaskAction()
    {
        // Given
        ValueOptional<int> optional = 21;
        int someValue = 0;
        bool noneCalled = false;

        // When
        await optional.MatchAsync(some: value => { someValue = value; return Task.CompletedTask; }, none: () => noneCalled = true, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(21, someValue);
        Assert.False(noneCalled);
    }

    [Fact(DisplayName = "ValueOptional<T>.MatchAsync of Some should invoke the some delegate (Func<T, Task>, Action) when the delegates complete asynchronously.")]
    public async Task ValueOptionalMatchAsyncOfSomeShouldInvokeSomeDelegateFuncTTaskActionWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueOptional<int> optional = 21;
        int someValue = 0;
        bool noneCalled = false;

        // When
        await optional.MatchAsync(some: value => { someValue = value; return Yielding.CompletedTask(); }, none: () => noneCalled = true, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(21, someValue);
        Assert.False(noneCalled);
    }

    [Fact(DisplayName = "ValueOptional<T>.MatchAsync of Some should invoke the some delegate (Func<T, ValueTask>, Action).")]
    public async Task ValueOptionalMatchAsyncOfSomeShouldInvokeSomeDelegateFuncTTaskActionWithValueTaskDelegates()
    {
        // Given
        ValueOptional<int> optional = 21;
        int someValue = 0;
        bool noneCalled = false;

        // When
        await optional.MatchAsync(some: value => { someValue = value; return ValueTask.CompletedTask; }, none: () => noneCalled = true, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(21, someValue);
        Assert.False(noneCalled);
    }

    [Fact(DisplayName = "ValueOptional<T>.MatchAsync of Some should invoke the some delegate (Func<T, ValueTask>, Action) when the delegates complete asynchronously.")]
    public async Task ValueOptionalMatchAsyncOfSomeShouldInvokeSomeDelegateFuncTTaskActionWithValueTaskDelegatesWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueOptional<int> optional = 21;
        int someValue = 0;
        bool noneCalled = false;

        // When
        await optional.MatchAsync(some: value => { someValue = value; return Yielding.CompletedValueTask(); }, none: () => noneCalled = true, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(21, someValue);
        Assert.False(noneCalled);
    }

    [Fact(DisplayName = "ValueOptional<T>.MatchAsync of None should invoke the none delegate (Func<T, Task>, Action).")]
    public async Task ValueOptionalMatchAsyncOfNoneShouldInvokeNoneDelegateFuncTTaskAction()
    {
        // Given
        ValueOptional<int> optional = ValueOptional<int>.None;
        int someValue = 0;
        bool noneCalled = false;

        // When
        await optional.MatchAsync(some: value => { someValue = value; return Task.CompletedTask; }, none: () => noneCalled = true, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(0, someValue);
        Assert.True(noneCalled);
    }

    [Fact(DisplayName = "ValueOptional<T>.MatchAsync of None should invoke the none delegate (Func<T, Task>, Action) when the delegates complete asynchronously.")]
    public async Task ValueOptionalMatchAsyncOfNoneShouldInvokeNoneDelegateFuncTTaskActionWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueOptional<int> optional = ValueOptional<int>.None;
        int someValue = 0;
        bool noneCalled = false;

        // When
        await optional.MatchAsync(some: value => { someValue = value; return Yielding.CompletedTask(); }, none: () => noneCalled = true, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(0, someValue);
        Assert.True(noneCalled);
    }

    [Fact(DisplayName = "ValueOptional<T>.MatchAsync of None should invoke the none delegate (Func<T, ValueTask>, Action).")]
    public async Task ValueOptionalMatchAsyncOfNoneShouldInvokeNoneDelegateFuncTTaskActionWithValueTaskDelegates()
    {
        // Given
        ValueOptional<int> optional = ValueOptional<int>.None;
        int someValue = 0;
        bool noneCalled = false;

        // When
        await optional.MatchAsync(some: value => { someValue = value; return ValueTask.CompletedTask; }, none: () => noneCalled = true, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(0, someValue);
        Assert.True(noneCalled);
    }

    [Fact(DisplayName = "ValueOptional<T>.MatchAsync of None should invoke the none delegate (Func<T, ValueTask>, Action) when the delegates complete asynchronously.")]
    public async Task ValueOptionalMatchAsyncOfNoneShouldInvokeNoneDelegateFuncTTaskActionWithValueTaskDelegatesWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueOptional<int> optional = ValueOptional<int>.None;
        int someValue = 0;
        bool noneCalled = false;

        // When
        await optional.MatchAsync(some: value => { someValue = value; return Yielding.CompletedValueTask(); }, none: () => noneCalled = true, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(0, someValue);
        Assert.True(noneCalled);
    }

    [Fact(DisplayName = "ValueOptional<T>.MatchAsync should do nothing when delegates are null (Func<T, Task>, Action).")]
    public async Task ValueOptionalMatchAsyncShouldDoNothingWhenDelegatesAreNullFuncTTaskAction()
    {
        // Given
        ValueOptional<int> some = 21;
        ValueOptional<int> none = ValueOptional<int>.None;

        // When
        await some.MatchAsync(some: (Func<int, Task>?)null, none: (Action?)null, cancellationToken: TestContext.Current.CancellationToken);
        await none.MatchAsync(some: (Func<int, Task>?)null, none: (Action?)null, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(ValueOptional<int>.IsSome(some));
        Assert.True(ValueOptional<int>.IsNone(none));
    }

    [Fact(DisplayName = "ValueOptional<T>.MatchAsync should do nothing when delegates are null (Func<T, ValueTask>, Action).")]
    public async Task ValueOptionalMatchAsyncShouldDoNothingWhenDelegatesAreNullFuncTTaskActionWithValueTaskDelegates()
    {
        // Given
        ValueOptional<int> some = 21;
        ValueOptional<int> none = ValueOptional<int>.None;

        // When
        await some.MatchAsync(some: (Func<int, ValueTask>?)null, none: (Action?)null, cancellationToken: TestContext.Current.CancellationToken);
        await none.MatchAsync(some: (Func<int, ValueTask>?)null, none: (Action?)null, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(ValueOptional<int>.IsSome(some));
        Assert.True(ValueOptional<int>.IsNone(none));
    }

    [Fact(DisplayName = "ValueOptional<T>.MatchAsync of Some should invoke the some delegate (Func<T, CancellationToken, Task>, Action).")]
    public async Task ValueOptionalMatchAsyncOfSomeShouldInvokeSomeDelegateFuncTCancellationTokenTaskAction()
    {
        // Given
        ValueOptional<int> optional = 21;
        int someValue = 0;
        bool noneCalled = false;
        CancellationToken someToken = CancellationToken.None;

        // When
        await optional.MatchAsync(some: (value, ct) => { someValue = value; someToken = ct; return Task.CompletedTask; }, none: () => noneCalled = true, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(21, someValue);
        Assert.False(noneCalled);
        Assert.Equal(TestContext.Current.CancellationToken, someToken);
    }

    [Fact(DisplayName = "ValueOptional<T>.MatchAsync of Some should invoke the some delegate (Func<T, CancellationToken, Task>, Action) when the delegates complete asynchronously.")]
    public async Task ValueOptionalMatchAsyncOfSomeShouldInvokeSomeDelegateFuncTCancellationTokenTaskActionWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueOptional<int> optional = 21;
        int someValue = 0;
        bool noneCalled = false;
        CancellationToken someToken = CancellationToken.None;

        // When
        await optional.MatchAsync(some: (value, ct) => { someValue = value; someToken = ct; return Yielding.CompletedTask(); }, none: () => noneCalled = true, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(21, someValue);
        Assert.False(noneCalled);
        Assert.Equal(TestContext.Current.CancellationToken, someToken);
    }

    [Fact(DisplayName = "ValueOptional<T>.MatchAsync of Some should invoke the some delegate (Func<T, CancellationToken, ValueTask>, Action).")]
    public async Task ValueOptionalMatchAsyncOfSomeShouldInvokeSomeDelegateFuncTCancellationTokenTaskActionWithValueTaskDelegates()
    {
        // Given
        ValueOptional<int> optional = 21;
        int someValue = 0;
        bool noneCalled = false;
        CancellationToken someToken = CancellationToken.None;

        // When
        await optional.MatchAsync(some: (value, ct) => { someValue = value; someToken = ct; return ValueTask.CompletedTask; }, none: () => noneCalled = true, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(21, someValue);
        Assert.False(noneCalled);
        Assert.Equal(TestContext.Current.CancellationToken, someToken);
    }

    [Fact(DisplayName = "ValueOptional<T>.MatchAsync of Some should invoke the some delegate (Func<T, CancellationToken, ValueTask>, Action) when the delegates complete asynchronously.")]
    public async Task ValueOptionalMatchAsyncOfSomeShouldInvokeSomeDelegateFuncTCancellationTokenTaskActionWithValueTaskDelegatesWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueOptional<int> optional = 21;
        int someValue = 0;
        bool noneCalled = false;
        CancellationToken someToken = CancellationToken.None;

        // When
        await optional.MatchAsync(some: (value, ct) => { someValue = value; someToken = ct; return Yielding.CompletedValueTask(); }, none: () => noneCalled = true, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(21, someValue);
        Assert.False(noneCalled);
        Assert.Equal(TestContext.Current.CancellationToken, someToken);
    }

    [Fact(DisplayName = "ValueOptional<T>.MatchAsync of None should invoke the none delegate (Func<T, CancellationToken, Task>, Action).")]
    public async Task ValueOptionalMatchAsyncOfNoneShouldInvokeNoneDelegateFuncTCancellationTokenTaskAction()
    {
        // Given
        ValueOptional<int> optional = ValueOptional<int>.None;
        int someValue = 0;
        bool noneCalled = false;
        CancellationToken someToken = CancellationToken.None;

        // When
        await optional.MatchAsync(some: (value, ct) => { someValue = value; someToken = ct; return Task.CompletedTask; }, none: () => noneCalled = true, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(0, someValue);
        Assert.True(noneCalled);
    }

    [Fact(DisplayName = "ValueOptional<T>.MatchAsync of None should invoke the none delegate (Func<T, CancellationToken, Task>, Action) when the delegates complete asynchronously.")]
    public async Task ValueOptionalMatchAsyncOfNoneShouldInvokeNoneDelegateFuncTCancellationTokenTaskActionWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueOptional<int> optional = ValueOptional<int>.None;
        int someValue = 0;
        bool noneCalled = false;
        CancellationToken someToken = CancellationToken.None;

        // When
        await optional.MatchAsync(some: (value, ct) => { someValue = value; someToken = ct; return Yielding.CompletedTask(); }, none: () => noneCalled = true, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(0, someValue);
        Assert.True(noneCalled);
    }

    [Fact(DisplayName = "ValueOptional<T>.MatchAsync of None should invoke the none delegate (Func<T, CancellationToken, ValueTask>, Action).")]
    public async Task ValueOptionalMatchAsyncOfNoneShouldInvokeNoneDelegateFuncTCancellationTokenTaskActionWithValueTaskDelegates()
    {
        // Given
        ValueOptional<int> optional = ValueOptional<int>.None;
        int someValue = 0;
        bool noneCalled = false;
        CancellationToken someToken = CancellationToken.None;

        // When
        await optional.MatchAsync(some: (value, ct) => { someValue = value; someToken = ct; return ValueTask.CompletedTask; }, none: () => noneCalled = true, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(0, someValue);
        Assert.True(noneCalled);
    }

    [Fact(DisplayName = "ValueOptional<T>.MatchAsync of None should invoke the none delegate (Func<T, CancellationToken, ValueTask>, Action) when the delegates complete asynchronously.")]
    public async Task ValueOptionalMatchAsyncOfNoneShouldInvokeNoneDelegateFuncTCancellationTokenTaskActionWithValueTaskDelegatesWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueOptional<int> optional = ValueOptional<int>.None;
        int someValue = 0;
        bool noneCalled = false;
        CancellationToken someToken = CancellationToken.None;

        // When
        await optional.MatchAsync(some: (value, ct) => { someValue = value; someToken = ct; return Yielding.CompletedValueTask(); }, none: () => noneCalled = true, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(0, someValue);
        Assert.True(noneCalled);
    }

    [Fact(DisplayName = "ValueOptional<T>.MatchAsync should do nothing when delegates are null (Func<T, CancellationToken, Task>, Action).")]
    public async Task ValueOptionalMatchAsyncShouldDoNothingWhenDelegatesAreNullFuncTCancellationTokenTaskAction()
    {
        // Given
        ValueOptional<int> some = 21;
        ValueOptional<int> none = ValueOptional<int>.None;

        // When
        await some.MatchAsync(some: (Func<int, CancellationToken, Task>?)null, none: (Action?)null, cancellationToken: TestContext.Current.CancellationToken);
        await none.MatchAsync(some: (Func<int, CancellationToken, Task>?)null, none: (Action?)null, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(ValueOptional<int>.IsSome(some));
        Assert.True(ValueOptional<int>.IsNone(none));
    }

    [Fact(DisplayName = "ValueOptional<T>.MatchAsync should do nothing when delegates are null (Func<T, CancellationToken, ValueTask>, Action).")]
    public async Task ValueOptionalMatchAsyncShouldDoNothingWhenDelegatesAreNullFuncTCancellationTokenTaskActionWithValueTaskDelegates()
    {
        // Given
        ValueOptional<int> some = 21;
        ValueOptional<int> none = ValueOptional<int>.None;

        // When
        await some.MatchAsync(some: (Func<int, CancellationToken, ValueTask>?)null, none: (Action?)null, cancellationToken: TestContext.Current.CancellationToken);
        await none.MatchAsync(some: (Func<int, CancellationToken, ValueTask>?)null, none: (Action?)null, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(ValueOptional<int>.IsSome(some));
        Assert.True(ValueOptional<int>.IsNone(none));
    }

    [Fact(DisplayName = "ValueOptional<T>.MatchAsync of Some should invoke the some delegate (Action<T>, Func<Task>).")]
    public async Task ValueOptionalMatchAsyncOfSomeShouldInvokeSomeDelegateActionTFuncTask()
    {
        // Given
        ValueOptional<int> optional = 21;
        int someValue = 0;
        bool noneCalled = false;

        // When
        await optional.MatchAsync(some: value => someValue = value, none: () => { noneCalled = true; return Task.CompletedTask; }, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(21, someValue);
        Assert.False(noneCalled);
    }

    [Fact(DisplayName = "ValueOptional<T>.MatchAsync of Some should invoke the some delegate (Action<T>, Func<Task>) when the delegates complete asynchronously.")]
    public async Task ValueOptionalMatchAsyncOfSomeShouldInvokeSomeDelegateActionTFuncTaskWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueOptional<int> optional = 21;
        int someValue = 0;
        bool noneCalled = false;

        // When
        await optional.MatchAsync(some: value => someValue = value, none: () => { noneCalled = true; return Yielding.CompletedTask(); }, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(21, someValue);
        Assert.False(noneCalled);
    }

    [Fact(DisplayName = "ValueOptional<T>.MatchAsync of Some should invoke the some delegate (Action<T>, Func<ValueTask>).")]
    public async Task ValueOptionalMatchAsyncOfSomeShouldInvokeSomeDelegateActionTFuncTaskWithValueTaskDelegates()
    {
        // Given
        ValueOptional<int> optional = 21;
        int someValue = 0;
        bool noneCalled = false;

        // When
        await optional.MatchAsync(some: value => someValue = value, none: () => { noneCalled = true; return ValueTask.CompletedTask; }, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(21, someValue);
        Assert.False(noneCalled);
    }

    [Fact(DisplayName = "ValueOptional<T>.MatchAsync of Some should invoke the some delegate (Action<T>, Func<ValueTask>) when the delegates complete asynchronously.")]
    public async Task ValueOptionalMatchAsyncOfSomeShouldInvokeSomeDelegateActionTFuncTaskWithValueTaskDelegatesWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueOptional<int> optional = 21;
        int someValue = 0;
        bool noneCalled = false;

        // When
        await optional.MatchAsync(some: value => someValue = value, none: () => { noneCalled = true; return Yielding.CompletedValueTask(); }, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(21, someValue);
        Assert.False(noneCalled);
    }

    [Fact(DisplayName = "ValueOptional<T>.MatchAsync of None should invoke the none delegate (Action<T>, Func<Task>).")]
    public async Task ValueOptionalMatchAsyncOfNoneShouldInvokeNoneDelegateActionTFuncTask()
    {
        // Given
        ValueOptional<int> optional = ValueOptional<int>.None;
        int someValue = 0;
        bool noneCalled = false;

        // When
        await optional.MatchAsync(some: value => someValue = value, none: () => { noneCalled = true; return Task.CompletedTask; }, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(0, someValue);
        Assert.True(noneCalled);
    }

    [Fact(DisplayName = "ValueOptional<T>.MatchAsync of None should invoke the none delegate (Action<T>, Func<Task>) when the delegates complete asynchronously.")]
    public async Task ValueOptionalMatchAsyncOfNoneShouldInvokeNoneDelegateActionTFuncTaskWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueOptional<int> optional = ValueOptional<int>.None;
        int someValue = 0;
        bool noneCalled = false;

        // When
        await optional.MatchAsync(some: value => someValue = value, none: () => { noneCalled = true; return Yielding.CompletedTask(); }, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(0, someValue);
        Assert.True(noneCalled);
    }

    [Fact(DisplayName = "ValueOptional<T>.MatchAsync of None should invoke the none delegate (Action<T>, Func<ValueTask>).")]
    public async Task ValueOptionalMatchAsyncOfNoneShouldInvokeNoneDelegateActionTFuncTaskWithValueTaskDelegates()
    {
        // Given
        ValueOptional<int> optional = ValueOptional<int>.None;
        int someValue = 0;
        bool noneCalled = false;

        // When
        await optional.MatchAsync(some: value => someValue = value, none: () => { noneCalled = true; return ValueTask.CompletedTask; }, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(0, someValue);
        Assert.True(noneCalled);
    }

    [Fact(DisplayName = "ValueOptional<T>.MatchAsync of None should invoke the none delegate (Action<T>, Func<ValueTask>) when the delegates complete asynchronously.")]
    public async Task ValueOptionalMatchAsyncOfNoneShouldInvokeNoneDelegateActionTFuncTaskWithValueTaskDelegatesWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueOptional<int> optional = ValueOptional<int>.None;
        int someValue = 0;
        bool noneCalled = false;

        // When
        await optional.MatchAsync(some: value => someValue = value, none: () => { noneCalled = true; return Yielding.CompletedValueTask(); }, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(0, someValue);
        Assert.True(noneCalled);
    }

    [Fact(DisplayName = "ValueOptional<T>.MatchAsync should do nothing when delegates are null (Action<T>, Func<Task>).")]
    public async Task ValueOptionalMatchAsyncShouldDoNothingWhenDelegatesAreNullActionTFuncTask()
    {
        // Given
        ValueOptional<int> some = 21;
        ValueOptional<int> none = ValueOptional<int>.None;

        // When
        await some.MatchAsync(some: (Action<int>?)null, none: (Func<Task>?)null, cancellationToken: TestContext.Current.CancellationToken);
        await none.MatchAsync(some: (Action<int>?)null, none: (Func<Task>?)null, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(ValueOptional<int>.IsSome(some));
        Assert.True(ValueOptional<int>.IsNone(none));
    }

    [Fact(DisplayName = "ValueOptional<T>.MatchAsync should do nothing when delegates are null (Action<T>, Func<ValueTask>).")]
    public async Task ValueOptionalMatchAsyncShouldDoNothingWhenDelegatesAreNullActionTFuncTaskWithValueTaskDelegates()
    {
        // Given
        ValueOptional<int> some = 21;
        ValueOptional<int> none = ValueOptional<int>.None;

        // When
        await some.MatchAsync(some: (Action<int>?)null, none: (Func<ValueTask>?)null, cancellationToken: TestContext.Current.CancellationToken);
        await none.MatchAsync(some: (Action<int>?)null, none: (Func<ValueTask>?)null, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(ValueOptional<int>.IsSome(some));
        Assert.True(ValueOptional<int>.IsNone(none));
    }

    [Fact(DisplayName = "ValueOptional<T>.MatchAsync of Some should invoke the some delegate (Action<T>, Func<CancellationToken, Task>).")]
    public async Task ValueOptionalMatchAsyncOfSomeShouldInvokeSomeDelegateActionTFuncCancellationTokenTask()
    {
        // Given
        ValueOptional<int> optional = 21;
        int someValue = 0;
        bool noneCalled = false;
        CancellationToken noneToken = CancellationToken.None;

        // When
        await optional.MatchAsync(some: value => someValue = value, none: ct => { noneCalled = true; noneToken = ct; return Task.CompletedTask; }, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(21, someValue);
        Assert.False(noneCalled);
    }

    [Fact(DisplayName = "ValueOptional<T>.MatchAsync of Some should invoke the some delegate (Action<T>, Func<CancellationToken, Task>) when the delegates complete asynchronously.")]
    public async Task ValueOptionalMatchAsyncOfSomeShouldInvokeSomeDelegateActionTFuncCancellationTokenTaskWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueOptional<int> optional = 21;
        int someValue = 0;
        bool noneCalled = false;
        CancellationToken noneToken = CancellationToken.None;

        // When
        await optional.MatchAsync(some: value => someValue = value, none: ct => { noneCalled = true; noneToken = ct; return Yielding.CompletedTask(); }, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(21, someValue);
        Assert.False(noneCalled);
    }

    [Fact(DisplayName = "ValueOptional<T>.MatchAsync of Some should invoke the some delegate (Action<T>, Func<CancellationToken, ValueTask>).")]
    public async Task ValueOptionalMatchAsyncOfSomeShouldInvokeSomeDelegateActionTFuncCancellationTokenTaskWithValueTaskDelegates()
    {
        // Given
        ValueOptional<int> optional = 21;
        int someValue = 0;
        bool noneCalled = false;
        CancellationToken noneToken = CancellationToken.None;

        // When
        await optional.MatchAsync(some: value => someValue = value, none: ct => { noneCalled = true; noneToken = ct; return ValueTask.CompletedTask; }, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(21, someValue);
        Assert.False(noneCalled);
    }

    [Fact(DisplayName = "ValueOptional<T>.MatchAsync of Some should invoke the some delegate (Action<T>, Func<CancellationToken, ValueTask>) when the delegates complete asynchronously.")]
    public async Task ValueOptionalMatchAsyncOfSomeShouldInvokeSomeDelegateActionTFuncCancellationTokenTaskWithValueTaskDelegatesWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueOptional<int> optional = 21;
        int someValue = 0;
        bool noneCalled = false;
        CancellationToken noneToken = CancellationToken.None;

        // When
        await optional.MatchAsync(some: value => someValue = value, none: ct => { noneCalled = true; noneToken = ct; return Yielding.CompletedValueTask(); }, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(21, someValue);
        Assert.False(noneCalled);
    }

    [Fact(DisplayName = "ValueOptional<T>.MatchAsync of None should invoke the none delegate (Action<T>, Func<CancellationToken, Task>).")]
    public async Task ValueOptionalMatchAsyncOfNoneShouldInvokeNoneDelegateActionTFuncCancellationTokenTask()
    {
        // Given
        ValueOptional<int> optional = ValueOptional<int>.None;
        int someValue = 0;
        bool noneCalled = false;
        CancellationToken noneToken = CancellationToken.None;

        // When
        await optional.MatchAsync(some: value => someValue = value, none: ct => { noneCalled = true; noneToken = ct; return Task.CompletedTask; }, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(0, someValue);
        Assert.True(noneCalled);
        Assert.Equal(TestContext.Current.CancellationToken, noneToken);
    }

    [Fact(DisplayName = "ValueOptional<T>.MatchAsync of None should invoke the none delegate (Action<T>, Func<CancellationToken, Task>) when the delegates complete asynchronously.")]
    public async Task ValueOptionalMatchAsyncOfNoneShouldInvokeNoneDelegateActionTFuncCancellationTokenTaskWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueOptional<int> optional = ValueOptional<int>.None;
        int someValue = 0;
        bool noneCalled = false;
        CancellationToken noneToken = CancellationToken.None;

        // When
        await optional.MatchAsync(some: value => someValue = value, none: ct => { noneCalled = true; noneToken = ct; return Yielding.CompletedTask(); }, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(0, someValue);
        Assert.True(noneCalled);
        Assert.Equal(TestContext.Current.CancellationToken, noneToken);
    }

    [Fact(DisplayName = "ValueOptional<T>.MatchAsync of None should invoke the none delegate (Action<T>, Func<CancellationToken, ValueTask>).")]
    public async Task ValueOptionalMatchAsyncOfNoneShouldInvokeNoneDelegateActionTFuncCancellationTokenTaskWithValueTaskDelegates()
    {
        // Given
        ValueOptional<int> optional = ValueOptional<int>.None;
        int someValue = 0;
        bool noneCalled = false;
        CancellationToken noneToken = CancellationToken.None;

        // When
        await optional.MatchAsync(some: value => someValue = value, none: ct => { noneCalled = true; noneToken = ct; return ValueTask.CompletedTask; }, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(0, someValue);
        Assert.True(noneCalled);
        Assert.Equal(TestContext.Current.CancellationToken, noneToken);
    }

    [Fact(DisplayName = "ValueOptional<T>.MatchAsync of None should invoke the none delegate (Action<T>, Func<CancellationToken, ValueTask>) when the delegates complete asynchronously.")]
    public async Task ValueOptionalMatchAsyncOfNoneShouldInvokeNoneDelegateActionTFuncCancellationTokenTaskWithValueTaskDelegatesWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueOptional<int> optional = ValueOptional<int>.None;
        int someValue = 0;
        bool noneCalled = false;
        CancellationToken noneToken = CancellationToken.None;

        // When
        await optional.MatchAsync(some: value => someValue = value, none: ct => { noneCalled = true; noneToken = ct; return Yielding.CompletedValueTask(); }, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(0, someValue);
        Assert.True(noneCalled);
        Assert.Equal(TestContext.Current.CancellationToken, noneToken);
    }

    [Fact(DisplayName = "ValueOptional<T>.MatchAsync should do nothing when delegates are null (Action<T>, Func<CancellationToken, Task>).")]
    public async Task ValueOptionalMatchAsyncShouldDoNothingWhenDelegatesAreNullActionTFuncCancellationTokenTask()
    {
        // Given
        ValueOptional<int> some = 21;
        ValueOptional<int> none = ValueOptional<int>.None;

        // When
        await some.MatchAsync(some: (Action<int>?)null, none: (Func<CancellationToken, Task>?)null, cancellationToken: TestContext.Current.CancellationToken);
        await none.MatchAsync(some: (Action<int>?)null, none: (Func<CancellationToken, Task>?)null, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(ValueOptional<int>.IsSome(some));
        Assert.True(ValueOptional<int>.IsNone(none));
    }

    [Fact(DisplayName = "ValueOptional<T>.MatchAsync should do nothing when delegates are null (Action<T>, Func<CancellationToken, ValueTask>).")]
    public async Task ValueOptionalMatchAsyncShouldDoNothingWhenDelegatesAreNullActionTFuncCancellationTokenTaskWithValueTaskDelegates()
    {
        // Given
        ValueOptional<int> some = 21;
        ValueOptional<int> none = ValueOptional<int>.None;

        // When
        await some.MatchAsync(some: (Action<int>?)null, none: (Func<CancellationToken, ValueTask>?)null, cancellationToken: TestContext.Current.CancellationToken);
        await none.MatchAsync(some: (Action<int>?)null, none: (Func<CancellationToken, ValueTask>?)null, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(ValueOptional<int>.IsSome(some));
        Assert.True(ValueOptional<int>.IsNone(none));
    }

    [Fact(DisplayName = "ValueOptional<T>.MatchAsync of Some should invoke the some delegate (Func<T, Task>, Func<Task>).")]
    public async Task ValueOptionalMatchAsyncOfSomeShouldInvokeSomeDelegateFuncTTaskFuncTask()
    {
        // Given
        ValueOptional<int> optional = 21;
        int someValue = 0;
        bool noneCalled = false;

        // When
        await optional.MatchAsync(some: value => { someValue = value; return Task.CompletedTask; }, none: () => { noneCalled = true; return Task.CompletedTask; }, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(21, someValue);
        Assert.False(noneCalled);
    }

    [Fact(DisplayName = "ValueOptional<T>.MatchAsync of Some should invoke the some delegate (Func<T, Task>, Func<Task>) when the delegates complete asynchronously.")]
    public async Task ValueOptionalMatchAsyncOfSomeShouldInvokeSomeDelegateFuncTTaskFuncTaskWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueOptional<int> optional = 21;
        int someValue = 0;
        bool noneCalled = false;

        // When
        await optional.MatchAsync(some: value => { someValue = value; return Yielding.CompletedTask(); }, none: () => { noneCalled = true; return Yielding.CompletedTask(); }, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(21, someValue);
        Assert.False(noneCalled);
    }

    [Fact(DisplayName = "ValueOptional<T>.MatchAsync of Some should invoke the some delegate (Func<T, ValueTask>, Func<ValueTask>).")]
    public async Task ValueOptionalMatchAsyncOfSomeShouldInvokeSomeDelegateFuncTTaskFuncTaskWithValueTaskDelegates()
    {
        // Given
        ValueOptional<int> optional = 21;
        int someValue = 0;
        bool noneCalled = false;

        // When
        await optional.MatchAsync(some: value => { someValue = value; return ValueTask.CompletedTask; }, none: () => { noneCalled = true; return ValueTask.CompletedTask; }, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(21, someValue);
        Assert.False(noneCalled);
    }

    [Fact(DisplayName = "ValueOptional<T>.MatchAsync of Some should invoke the some delegate (Func<T, ValueTask>, Func<ValueTask>) when the delegates complete asynchronously.")]
    public async Task ValueOptionalMatchAsyncOfSomeShouldInvokeSomeDelegateFuncTTaskFuncTaskWithValueTaskDelegatesWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueOptional<int> optional = 21;
        int someValue = 0;
        bool noneCalled = false;

        // When
        await optional.MatchAsync(some: value => { someValue = value; return Yielding.CompletedValueTask(); }, none: () => { noneCalled = true; return Yielding.CompletedValueTask(); }, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(21, someValue);
        Assert.False(noneCalled);
    }

    [Fact(DisplayName = "ValueOptional<T>.MatchAsync of None should invoke the none delegate (Func<T, Task>, Func<Task>).")]
    public async Task ValueOptionalMatchAsyncOfNoneShouldInvokeNoneDelegateFuncTTaskFuncTask()
    {
        // Given
        ValueOptional<int> optional = ValueOptional<int>.None;
        int someValue = 0;
        bool noneCalled = false;

        // When
        await optional.MatchAsync(some: value => { someValue = value; return Task.CompletedTask; }, none: () => { noneCalled = true; return Task.CompletedTask; }, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(0, someValue);
        Assert.True(noneCalled);
    }

    [Fact(DisplayName = "ValueOptional<T>.MatchAsync of None should invoke the none delegate (Func<T, Task>, Func<Task>) when the delegates complete asynchronously.")]
    public async Task ValueOptionalMatchAsyncOfNoneShouldInvokeNoneDelegateFuncTTaskFuncTaskWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueOptional<int> optional = ValueOptional<int>.None;
        int someValue = 0;
        bool noneCalled = false;

        // When
        await optional.MatchAsync(some: value => { someValue = value; return Yielding.CompletedTask(); }, none: () => { noneCalled = true; return Yielding.CompletedTask(); }, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(0, someValue);
        Assert.True(noneCalled);
    }

    [Fact(DisplayName = "ValueOptional<T>.MatchAsync of None should invoke the none delegate (Func<T, ValueTask>, Func<ValueTask>).")]
    public async Task ValueOptionalMatchAsyncOfNoneShouldInvokeNoneDelegateFuncTTaskFuncTaskWithValueTaskDelegates()
    {
        // Given
        ValueOptional<int> optional = ValueOptional<int>.None;
        int someValue = 0;
        bool noneCalled = false;

        // When
        await optional.MatchAsync(some: value => { someValue = value; return ValueTask.CompletedTask; }, none: () => { noneCalled = true; return ValueTask.CompletedTask; }, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(0, someValue);
        Assert.True(noneCalled);
    }

    [Fact(DisplayName = "ValueOptional<T>.MatchAsync of None should invoke the none delegate (Func<T, ValueTask>, Func<ValueTask>) when the delegates complete asynchronously.")]
    public async Task ValueOptionalMatchAsyncOfNoneShouldInvokeNoneDelegateFuncTTaskFuncTaskWithValueTaskDelegatesWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueOptional<int> optional = ValueOptional<int>.None;
        int someValue = 0;
        bool noneCalled = false;

        // When
        await optional.MatchAsync(some: value => { someValue = value; return Yielding.CompletedValueTask(); }, none: () => { noneCalled = true; return Yielding.CompletedValueTask(); }, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(0, someValue);
        Assert.True(noneCalled);
    }

    [Fact(DisplayName = "ValueOptional<T>.MatchAsync should do nothing when delegates are null (Func<T, Task>, Func<Task>).")]
    public async Task ValueOptionalMatchAsyncShouldDoNothingWhenDelegatesAreNullFuncTTaskFuncTask()
    {
        // Given
        ValueOptional<int> some = 21;
        ValueOptional<int> none = ValueOptional<int>.None;

        // When
        await some.MatchAsync(some: (Func<int, Task>?)null, none: (Func<Task>?)null, cancellationToken: TestContext.Current.CancellationToken);
        await none.MatchAsync(some: (Func<int, Task>?)null, none: (Func<Task>?)null, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(ValueOptional<int>.IsSome(some));
        Assert.True(ValueOptional<int>.IsNone(none));
    }

    [Fact(DisplayName = "ValueOptional<T>.MatchAsync should do nothing when delegates are null (Func<T, ValueTask>, Func<ValueTask>).")]
    public async Task ValueOptionalMatchAsyncShouldDoNothingWhenDelegatesAreNullFuncTTaskFuncTaskWithValueTaskDelegates()
    {
        // Given
        ValueOptional<int> some = 21;
        ValueOptional<int> none = ValueOptional<int>.None;

        // When
        await some.MatchAsync(some: (Func<int, ValueTask>?)null, none: (Func<ValueTask>?)null, cancellationToken: TestContext.Current.CancellationToken);
        await none.MatchAsync(some: (Func<int, ValueTask>?)null, none: (Func<ValueTask>?)null, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(ValueOptional<int>.IsSome(some));
        Assert.True(ValueOptional<int>.IsNone(none));
    }

    [Fact(DisplayName = "ValueOptional<T>.MatchAsync of Some should invoke the some delegate (Func<T, CancellationToken, Task>, Func<CancellationToken, Task>).")]
    public async Task ValueOptionalMatchAsyncOfSomeShouldInvokeSomeDelegateFuncTCancellationTokenTaskFuncCancellationTokenTask()
    {
        // Given
        ValueOptional<int> optional = 21;
        int someValue = 0;
        bool noneCalled = false;
        CancellationToken someToken = CancellationToken.None;
        CancellationToken noneToken = CancellationToken.None;

        // When
        await optional.MatchAsync(some: (value, ct) => { someValue = value; someToken = ct; return Task.CompletedTask; }, none: ct => { noneCalled = true; noneToken = ct; return Task.CompletedTask; }, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(21, someValue);
        Assert.False(noneCalled);
        Assert.Equal(TestContext.Current.CancellationToken, someToken);
    }

    [Fact(DisplayName = "ValueOptional<T>.MatchAsync of Some should invoke the some delegate (Func<T, CancellationToken, Task>, Func<CancellationToken, Task>) when the delegates complete asynchronously.")]
    public async Task ValueOptionalMatchAsyncOfSomeShouldInvokeSomeDelegateFuncTCancellationTokenTaskFuncCancellationTokenTaskWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueOptional<int> optional = 21;
        int someValue = 0;
        bool noneCalled = false;
        CancellationToken someToken = CancellationToken.None;
        CancellationToken noneToken = CancellationToken.None;

        // When
        await optional.MatchAsync(some: (value, ct) => { someValue = value; someToken = ct; return Yielding.CompletedTask(); }, none: ct => { noneCalled = true; noneToken = ct; return Yielding.CompletedTask(); }, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(21, someValue);
        Assert.False(noneCalled);
        Assert.Equal(TestContext.Current.CancellationToken, someToken);
    }

    [Fact(DisplayName = "ValueOptional<T>.MatchAsync of Some should invoke the some delegate (Func<T, CancellationToken, ValueTask>, Func<CancellationToken, ValueTask>).")]
    public async Task ValueOptionalMatchAsyncOfSomeShouldInvokeSomeDelegateFuncTCancellationTokenTaskFuncCancellationTokenTaskWithValueTaskDelegates()
    {
        // Given
        ValueOptional<int> optional = 21;
        int someValue = 0;
        bool noneCalled = false;
        CancellationToken someToken = CancellationToken.None;
        CancellationToken noneToken = CancellationToken.None;

        // When
        await optional.MatchAsync(some: (value, ct) => { someValue = value; someToken = ct; return ValueTask.CompletedTask; }, none: ct => { noneCalled = true; noneToken = ct; return ValueTask.CompletedTask; }, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(21, someValue);
        Assert.False(noneCalled);
        Assert.Equal(TestContext.Current.CancellationToken, someToken);
    }

    [Fact(DisplayName = "ValueOptional<T>.MatchAsync of Some should invoke the some delegate (Func<T, CancellationToken, ValueTask>, Func<CancellationToken, ValueTask>) when the delegates complete asynchronously.")]
    public async Task ValueOptionalMatchAsyncOfSomeShouldInvokeSomeDelegateFuncTCancellationTokenTaskFuncCancellationTokenTaskWithValueTaskDelegatesWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueOptional<int> optional = 21;
        int someValue = 0;
        bool noneCalled = false;
        CancellationToken someToken = CancellationToken.None;
        CancellationToken noneToken = CancellationToken.None;

        // When
        await optional.MatchAsync(some: (value, ct) => { someValue = value; someToken = ct; return Yielding.CompletedValueTask(); }, none: ct => { noneCalled = true; noneToken = ct; return Yielding.CompletedValueTask(); }, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(21, someValue);
        Assert.False(noneCalled);
        Assert.Equal(TestContext.Current.CancellationToken, someToken);
    }

    [Fact(DisplayName = "ValueOptional<T>.MatchAsync of None should invoke the none delegate (Func<T, CancellationToken, Task>, Func<CancellationToken, Task>).")]
    public async Task ValueOptionalMatchAsyncOfNoneShouldInvokeNoneDelegateFuncTCancellationTokenTaskFuncCancellationTokenTask()
    {
        // Given
        ValueOptional<int> optional = ValueOptional<int>.None;
        int someValue = 0;
        bool noneCalled = false;
        CancellationToken someToken = CancellationToken.None;
        CancellationToken noneToken = CancellationToken.None;

        // When
        await optional.MatchAsync(some: (value, ct) => { someValue = value; someToken = ct; return Task.CompletedTask; }, none: ct => { noneCalled = true; noneToken = ct; return Task.CompletedTask; }, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(0, someValue);
        Assert.True(noneCalled);
        Assert.Equal(TestContext.Current.CancellationToken, noneToken);
    }

    [Fact(DisplayName = "ValueOptional<T>.MatchAsync of None should invoke the none delegate (Func<T, CancellationToken, Task>, Func<CancellationToken, Task>) when the delegates complete asynchronously.")]
    public async Task ValueOptionalMatchAsyncOfNoneShouldInvokeNoneDelegateFuncTCancellationTokenTaskFuncCancellationTokenTaskWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueOptional<int> optional = ValueOptional<int>.None;
        int someValue = 0;
        bool noneCalled = false;
        CancellationToken someToken = CancellationToken.None;
        CancellationToken noneToken = CancellationToken.None;

        // When
        await optional.MatchAsync(some: (value, ct) => { someValue = value; someToken = ct; return Yielding.CompletedTask(); }, none: ct => { noneCalled = true; noneToken = ct; return Yielding.CompletedTask(); }, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(0, someValue);
        Assert.True(noneCalled);
        Assert.Equal(TestContext.Current.CancellationToken, noneToken);
    }

    [Fact(DisplayName = "ValueOptional<T>.MatchAsync of None should invoke the none delegate (Func<T, CancellationToken, ValueTask>, Func<CancellationToken, ValueTask>).")]
    public async Task ValueOptionalMatchAsyncOfNoneShouldInvokeNoneDelegateFuncTCancellationTokenTaskFuncCancellationTokenTaskWithValueTaskDelegates()
    {
        // Given
        ValueOptional<int> optional = ValueOptional<int>.None;
        int someValue = 0;
        bool noneCalled = false;
        CancellationToken someToken = CancellationToken.None;
        CancellationToken noneToken = CancellationToken.None;

        // When
        await optional.MatchAsync(some: (value, ct) => { someValue = value; someToken = ct; return ValueTask.CompletedTask; }, none: ct => { noneCalled = true; noneToken = ct; return ValueTask.CompletedTask; }, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(0, someValue);
        Assert.True(noneCalled);
        Assert.Equal(TestContext.Current.CancellationToken, noneToken);
    }

    [Fact(DisplayName = "ValueOptional<T>.MatchAsync of None should invoke the none delegate (Func<T, CancellationToken, ValueTask>, Func<CancellationToken, ValueTask>) when the delegates complete asynchronously.")]
    public async Task ValueOptionalMatchAsyncOfNoneShouldInvokeNoneDelegateFuncTCancellationTokenTaskFuncCancellationTokenTaskWithValueTaskDelegatesWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueOptional<int> optional = ValueOptional<int>.None;
        int someValue = 0;
        bool noneCalled = false;
        CancellationToken someToken = CancellationToken.None;
        CancellationToken noneToken = CancellationToken.None;

        // When
        await optional.MatchAsync(some: (value, ct) => { someValue = value; someToken = ct; return Yielding.CompletedValueTask(); }, none: ct => { noneCalled = true; noneToken = ct; return Yielding.CompletedValueTask(); }, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(0, someValue);
        Assert.True(noneCalled);
        Assert.Equal(TestContext.Current.CancellationToken, noneToken);
    }

    [Fact(DisplayName = "ValueOptional<T>.MatchAsync should do nothing when delegates are null (Func<T, CancellationToken, Task>, Func<CancellationToken, Task>).")]
    public async Task ValueOptionalMatchAsyncShouldDoNothingWhenDelegatesAreNullFuncTCancellationTokenTaskFuncCancellationTokenTask()
    {
        // Given
        ValueOptional<int> some = 21;
        ValueOptional<int> none = ValueOptional<int>.None;

        // When
        await some.MatchAsync(some: (Func<int, CancellationToken, Task>?)null, none: (Func<CancellationToken, Task>?)null, cancellationToken: TestContext.Current.CancellationToken);
        await none.MatchAsync(some: (Func<int, CancellationToken, Task>?)null, none: (Func<CancellationToken, Task>?)null, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(ValueOptional<int>.IsSome(some));
        Assert.True(ValueOptional<int>.IsNone(none));
    }

    [Fact(DisplayName = "ValueOptional<T>.MatchAsync should do nothing when delegates are null (Func<T, CancellationToken, ValueTask>, Func<CancellationToken, ValueTask>).")]
    public async Task ValueOptionalMatchAsyncShouldDoNothingWhenDelegatesAreNullFuncTCancellationTokenTaskFuncCancellationTokenTaskWithValueTaskDelegates()
    {
        // Given
        ValueOptional<int> some = 21;
        ValueOptional<int> none = ValueOptional<int>.None;

        // When
        await some.MatchAsync(some: (Func<int, CancellationToken, ValueTask>?)null, none: (Func<CancellationToken, ValueTask>?)null, cancellationToken: TestContext.Current.CancellationToken);
        await none.MatchAsync(some: (Func<int, CancellationToken, ValueTask>?)null, none: (Func<CancellationToken, ValueTask>?)null, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(ValueOptional<int>.IsSome(some));
        Assert.True(ValueOptional<int>.IsNone(none));
    }

    [Fact(DisplayName = "ValueOptional<T>.MatchAsync<TResult> of Some should return the result of the some delegate (Func<T, Task<TResult>>, Func<TResult>).")]
    public async Task ValueOptionalMatchAsyncTResultOfSomeShouldReturnResultOfSomeDelegateFuncTTaskTResultFuncTResult()
    {
        // Given
        ValueOptional<int> optional = 21;

        // When
        string actual = await optional.MatchAsync(some: value => Task.FromResult($"Some {value}"), none: () => "None", cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal("Some 21", actual);
    }

    [Fact(DisplayName = "ValueOptional<T>.MatchAsync<TResult> of Some should return the result of the some delegate (Func<T, Task<TResult>>, Func<TResult>) when the delegates complete asynchronously.")]
    public async Task ValueOptionalMatchAsyncTResultOfSomeShouldReturnResultOfSomeDelegateFuncTTaskTResultFuncTResultWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueOptional<int> optional = 21;

        // When
        string actual = await optional.MatchAsync(some: value => Yielding.TaskOf($"Some {value}"), none: () => "None", cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal("Some 21", actual);
    }

    [Fact(DisplayName = "ValueOptional<T>.MatchAsync<TResult> of Some should return the result of the some delegate (Func<T, ValueTask<TResult>>, Func<TResult>).")]
    public async Task ValueOptionalMatchAsyncTResultOfSomeShouldReturnResultOfSomeDelegateFuncTTaskTResultFuncTResultWithValueTaskDelegates()
    {
        // Given
        ValueOptional<int> optional = 21;

        // When
        string actual = await optional.MatchAsync(some: value => ValueTask.FromResult($"Some {value}"), none: () => "None", cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal("Some 21", actual);
    }

    [Fact(DisplayName = "ValueOptional<T>.MatchAsync<TResult> of Some should return the result of the some delegate (Func<T, ValueTask<TResult>>, Func<TResult>) when the delegates complete asynchronously.")]
    public async Task ValueOptionalMatchAsyncTResultOfSomeShouldReturnResultOfSomeDelegateFuncTTaskTResultFuncTResultWithValueTaskDelegatesWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueOptional<int> optional = 21;

        // When
        string actual = await optional.MatchAsync(some: value => Yielding.ValueTaskOf($"Some {value}"), none: () => "None", cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal("Some 21", actual);
    }

    [Fact(DisplayName = "ValueOptional<T>.MatchAsync<TResult> of None should return the result of the none delegate (Func<T, Task<TResult>>, Func<TResult>).")]
    public async Task ValueOptionalMatchAsyncTResultOfNoneShouldReturnResultOfNoneDelegateFuncTTaskTResultFuncTResult()
    {
        // Given
        ValueOptional<int> optional = ValueOptional<int>.None;

        // When
        string actual = await optional.MatchAsync(some: value => Task.FromResult($"Some {value}"), none: () => "None", cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal("None", actual);
    }

    [Fact(DisplayName = "ValueOptional<T>.MatchAsync<TResult> of None should return the result of the none delegate (Func<T, Task<TResult>>, Func<TResult>) when the delegates complete asynchronously.")]
    public async Task ValueOptionalMatchAsyncTResultOfNoneShouldReturnResultOfNoneDelegateFuncTTaskTResultFuncTResultWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueOptional<int> optional = ValueOptional<int>.None;

        // When
        string actual = await optional.MatchAsync(some: value => Yielding.TaskOf($"Some {value}"), none: () => "None", cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal("None", actual);
    }

    [Fact(DisplayName = "ValueOptional<T>.MatchAsync<TResult> of None should return the result of the none delegate (Func<T, ValueTask<TResult>>, Func<TResult>).")]
    public async Task ValueOptionalMatchAsyncTResultOfNoneShouldReturnResultOfNoneDelegateFuncTTaskTResultFuncTResultWithValueTaskDelegates()
    {
        // Given
        ValueOptional<int> optional = ValueOptional<int>.None;

        // When
        string actual = await optional.MatchAsync(some: value => ValueTask.FromResult($"Some {value}"), none: () => "None", cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal("None", actual);
    }

    [Fact(DisplayName = "ValueOptional<T>.MatchAsync<TResult> of None should return the result of the none delegate (Func<T, ValueTask<TResult>>, Func<TResult>) when the delegates complete asynchronously.")]
    public async Task ValueOptionalMatchAsyncTResultOfNoneShouldReturnResultOfNoneDelegateFuncTTaskTResultFuncTResultWithValueTaskDelegatesWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueOptional<int> optional = ValueOptional<int>.None;

        // When
        string actual = await optional.MatchAsync(some: value => Yielding.ValueTaskOf($"Some {value}"), none: () => "None", cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal("None", actual);
    }

    [Fact(DisplayName = "ValueOptional<T>.MatchAsync<TResult> of Some should return the result of the some delegate (Func<T, CancellationToken, Task<TResult>>, Func<TResult>).")]
    public async Task ValueOptionalMatchAsyncTResultOfSomeShouldReturnResultOfSomeDelegateFuncTCancellationTokenTaskTResultFuncTResult()
    {
        // Given
        ValueOptional<int> optional = 21;
        CancellationToken someToken = CancellationToken.None;

        // When
        string actual = await optional.MatchAsync(some: (value, ct) => { someToken = ct; return Task.FromResult($"Some {value}"); }, none: () => "None", cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal("Some 21", actual);
        Assert.Equal(TestContext.Current.CancellationToken, someToken);
    }

    [Fact(DisplayName = "ValueOptional<T>.MatchAsync<TResult> of Some should return the result of the some delegate (Func<T, CancellationToken, Task<TResult>>, Func<TResult>) when the delegates complete asynchronously.")]
    public async Task ValueOptionalMatchAsyncTResultOfSomeShouldReturnResultOfSomeDelegateFuncTCancellationTokenTaskTResultFuncTResultWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueOptional<int> optional = 21;
        CancellationToken someToken = CancellationToken.None;

        // When
        string actual = await optional.MatchAsync(some: (value, ct) => { someToken = ct; return Yielding.TaskOf($"Some {value}"); }, none: () => "None", cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal("Some 21", actual);
        Assert.Equal(TestContext.Current.CancellationToken, someToken);
    }

    [Fact(DisplayName = "ValueOptional<T>.MatchAsync<TResult> of Some should return the result of the some delegate (Func<T, CancellationToken, ValueTask<TResult>>, Func<TResult>).")]
    public async Task ValueOptionalMatchAsyncTResultOfSomeShouldReturnResultOfSomeDelegateFuncTCancellationTokenTaskTResultFuncTResultWithValueTaskDelegates()
    {
        // Given
        ValueOptional<int> optional = 21;
        CancellationToken someToken = CancellationToken.None;

        // When
        string actual = await optional.MatchAsync(some: (value, ct) => { someToken = ct; return ValueTask.FromResult($"Some {value}"); }, none: () => "None", cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal("Some 21", actual);
        Assert.Equal(TestContext.Current.CancellationToken, someToken);
    }

    [Fact(DisplayName = "ValueOptional<T>.MatchAsync<TResult> of Some should return the result of the some delegate (Func<T, CancellationToken, ValueTask<TResult>>, Func<TResult>) when the delegates complete asynchronously.")]
    public async Task ValueOptionalMatchAsyncTResultOfSomeShouldReturnResultOfSomeDelegateFuncTCancellationTokenTaskTResultFuncTResultWithValueTaskDelegatesWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueOptional<int> optional = 21;
        CancellationToken someToken = CancellationToken.None;

        // When
        string actual = await optional.MatchAsync(some: (value, ct) => { someToken = ct; return Yielding.ValueTaskOf($"Some {value}"); }, none: () => "None", cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal("Some 21", actual);
        Assert.Equal(TestContext.Current.CancellationToken, someToken);
    }

    [Fact(DisplayName = "ValueOptional<T>.MatchAsync<TResult> of None should return the result of the none delegate (Func<T, CancellationToken, Task<TResult>>, Func<TResult>).")]
    public async Task ValueOptionalMatchAsyncTResultOfNoneShouldReturnResultOfNoneDelegateFuncTCancellationTokenTaskTResultFuncTResult()
    {
        // Given
        ValueOptional<int> optional = ValueOptional<int>.None;
        CancellationToken someToken = CancellationToken.None;

        // When
        string actual = await optional.MatchAsync(some: (value, ct) => { someToken = ct; return Task.FromResult($"Some {value}"); }, none: () => "None", cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal("None", actual);
    }

    [Fact(DisplayName = "ValueOptional<T>.MatchAsync<TResult> of None should return the result of the none delegate (Func<T, CancellationToken, Task<TResult>>, Func<TResult>) when the delegates complete asynchronously.")]
    public async Task ValueOptionalMatchAsyncTResultOfNoneShouldReturnResultOfNoneDelegateFuncTCancellationTokenTaskTResultFuncTResultWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueOptional<int> optional = ValueOptional<int>.None;
        CancellationToken someToken = CancellationToken.None;

        // When
        string actual = await optional.MatchAsync(some: (value, ct) => { someToken = ct; return Yielding.TaskOf($"Some {value}"); }, none: () => "None", cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal("None", actual);
    }

    [Fact(DisplayName = "ValueOptional<T>.MatchAsync<TResult> of None should return the result of the none delegate (Func<T, CancellationToken, ValueTask<TResult>>, Func<TResult>).")]
    public async Task ValueOptionalMatchAsyncTResultOfNoneShouldReturnResultOfNoneDelegateFuncTCancellationTokenTaskTResultFuncTResultWithValueTaskDelegates()
    {
        // Given
        ValueOptional<int> optional = ValueOptional<int>.None;
        CancellationToken someToken = CancellationToken.None;

        // When
        string actual = await optional.MatchAsync(some: (value, ct) => { someToken = ct; return ValueTask.FromResult($"Some {value}"); }, none: () => "None", cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal("None", actual);
    }

    [Fact(DisplayName = "ValueOptional<T>.MatchAsync<TResult> of None should return the result of the none delegate (Func<T, CancellationToken, ValueTask<TResult>>, Func<TResult>) when the delegates complete asynchronously.")]
    public async Task ValueOptionalMatchAsyncTResultOfNoneShouldReturnResultOfNoneDelegateFuncTCancellationTokenTaskTResultFuncTResultWithValueTaskDelegatesWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueOptional<int> optional = ValueOptional<int>.None;
        CancellationToken someToken = CancellationToken.None;

        // When
        string actual = await optional.MatchAsync(some: (value, ct) => { someToken = ct; return Yielding.ValueTaskOf($"Some {value}"); }, none: () => "None", cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal("None", actual);
    }

    [Fact(DisplayName = "ValueOptional<T>.MatchAsync<TResult> of Some should return the result of the some delegate (Func<T, TResult>, Func<Task<TResult>>).")]
    public async Task ValueOptionalMatchAsyncTResultOfSomeShouldReturnResultOfSomeDelegateFuncTTResultFuncTaskTResult()
    {
        // Given
        ValueOptional<int> optional = 21;

        // When
        string actual = await optional.MatchAsync(some: value => $"Some {value}", none: () => Task.FromResult("None"), cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal("Some 21", actual);
    }

    [Fact(DisplayName = "ValueOptional<T>.MatchAsync<TResult> of Some should return the result of the some delegate (Func<T, TResult>, Func<Task<TResult>>) when the delegates complete asynchronously.")]
    public async Task ValueOptionalMatchAsyncTResultOfSomeShouldReturnResultOfSomeDelegateFuncTTResultFuncTaskTResultWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueOptional<int> optional = 21;

        // When
        string actual = await optional.MatchAsync(some: value => $"Some {value}", none: () => Yielding.TaskOf("None"), cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal("Some 21", actual);
    }

    [Fact(DisplayName = "ValueOptional<T>.MatchAsync<TResult> of Some should return the result of the some delegate (Func<T, TResult>, Func<ValueTask<TResult>>).")]
    public async Task ValueOptionalMatchAsyncTResultOfSomeShouldReturnResultOfSomeDelegateFuncTTResultFuncTaskTResultWithValueTaskDelegates()
    {
        // Given
        ValueOptional<int> optional = 21;

        // When
        string actual = await optional.MatchAsync(some: value => $"Some {value}", none: () => ValueTask.FromResult("None"), cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal("Some 21", actual);
    }

    [Fact(DisplayName = "ValueOptional<T>.MatchAsync<TResult> of Some should return the result of the some delegate (Func<T, TResult>, Func<ValueTask<TResult>>) when the delegates complete asynchronously.")]
    public async Task ValueOptionalMatchAsyncTResultOfSomeShouldReturnResultOfSomeDelegateFuncTTResultFuncTaskTResultWithValueTaskDelegatesWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueOptional<int> optional = 21;

        // When
        string actual = await optional.MatchAsync(some: value => $"Some {value}", none: () => Yielding.ValueTaskOf("None"), cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal("Some 21", actual);
    }

    [Fact(DisplayName = "ValueOptional<T>.MatchAsync<TResult> of None should return the result of the none delegate (Func<T, TResult>, Func<Task<TResult>>).")]
    public async Task ValueOptionalMatchAsyncTResultOfNoneShouldReturnResultOfNoneDelegateFuncTTResultFuncTaskTResult()
    {
        // Given
        ValueOptional<int> optional = ValueOptional<int>.None;

        // When
        string actual = await optional.MatchAsync(some: value => $"Some {value}", none: () => Task.FromResult("None"), cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal("None", actual);
    }

    [Fact(DisplayName = "ValueOptional<T>.MatchAsync<TResult> of None should return the result of the none delegate (Func<T, TResult>, Func<Task<TResult>>) when the delegates complete asynchronously.")]
    public async Task ValueOptionalMatchAsyncTResultOfNoneShouldReturnResultOfNoneDelegateFuncTTResultFuncTaskTResultWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueOptional<int> optional = ValueOptional<int>.None;

        // When
        string actual = await optional.MatchAsync(some: value => $"Some {value}", none: () => Yielding.TaskOf("None"), cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal("None", actual);
    }

    [Fact(DisplayName = "ValueOptional<T>.MatchAsync<TResult> of None should return the result of the none delegate (Func<T, TResult>, Func<ValueTask<TResult>>).")]
    public async Task ValueOptionalMatchAsyncTResultOfNoneShouldReturnResultOfNoneDelegateFuncTTResultFuncTaskTResultWithValueTaskDelegates()
    {
        // Given
        ValueOptional<int> optional = ValueOptional<int>.None;

        // When
        string actual = await optional.MatchAsync(some: value => $"Some {value}", none: () => ValueTask.FromResult("None"), cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal("None", actual);
    }

    [Fact(DisplayName = "ValueOptional<T>.MatchAsync<TResult> of None should return the result of the none delegate (Func<T, TResult>, Func<ValueTask<TResult>>) when the delegates complete asynchronously.")]
    public async Task ValueOptionalMatchAsyncTResultOfNoneShouldReturnResultOfNoneDelegateFuncTTResultFuncTaskTResultWithValueTaskDelegatesWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueOptional<int> optional = ValueOptional<int>.None;

        // When
        string actual = await optional.MatchAsync(some: value => $"Some {value}", none: () => Yielding.ValueTaskOf("None"), cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal("None", actual);
    }

    [Fact(DisplayName = "ValueOptional<T>.MatchAsync<TResult> of Some should return the result of the some delegate (Func<T, TResult>, Func<CancellationToken, Task<TResult>>).")]
    public async Task ValueOptionalMatchAsyncTResultOfSomeShouldReturnResultOfSomeDelegateFuncTTResultFuncCancellationTokenTaskTResult()
    {
        // Given
        ValueOptional<int> optional = 21;
        CancellationToken noneToken = CancellationToken.None;

        // When
        string actual = await optional.MatchAsync(some: value => $"Some {value}", none: ct => { noneToken = ct; return Task.FromResult("None"); }, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal("Some 21", actual);
    }

    [Fact(DisplayName = "ValueOptional<T>.MatchAsync<TResult> of Some should return the result of the some delegate (Func<T, TResult>, Func<CancellationToken, Task<TResult>>) when the delegates complete asynchronously.")]
    public async Task ValueOptionalMatchAsyncTResultOfSomeShouldReturnResultOfSomeDelegateFuncTTResultFuncCancellationTokenTaskTResultWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueOptional<int> optional = 21;
        CancellationToken noneToken = CancellationToken.None;

        // When
        string actual = await optional.MatchAsync(some: value => $"Some {value}", none: ct => { noneToken = ct; return Yielding.TaskOf("None"); }, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal("Some 21", actual);
    }

    [Fact(DisplayName = "ValueOptional<T>.MatchAsync<TResult> of Some should return the result of the some delegate (Func<T, TResult>, Func<CancellationToken, ValueTask<TResult>>).")]
    public async Task ValueOptionalMatchAsyncTResultOfSomeShouldReturnResultOfSomeDelegateFuncTTResultFuncCancellationTokenTaskTResultWithValueTaskDelegates()
    {
        // Given
        ValueOptional<int> optional = 21;
        CancellationToken noneToken = CancellationToken.None;

        // When
        string actual = await optional.MatchAsync(some: value => $"Some {value}", none: ct => { noneToken = ct; return ValueTask.FromResult("None"); }, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal("Some 21", actual);
    }

    [Fact(DisplayName = "ValueOptional<T>.MatchAsync<TResult> of Some should return the result of the some delegate (Func<T, TResult>, Func<CancellationToken, ValueTask<TResult>>) when the delegates complete asynchronously.")]
    public async Task ValueOptionalMatchAsyncTResultOfSomeShouldReturnResultOfSomeDelegateFuncTTResultFuncCancellationTokenTaskTResultWithValueTaskDelegatesWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueOptional<int> optional = 21;
        CancellationToken noneToken = CancellationToken.None;

        // When
        string actual = await optional.MatchAsync(some: value => $"Some {value}", none: ct => { noneToken = ct; return Yielding.ValueTaskOf("None"); }, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal("Some 21", actual);
    }

    [Fact(DisplayName = "ValueOptional<T>.MatchAsync<TResult> of None should return the result of the none delegate (Func<T, TResult>, Func<CancellationToken, Task<TResult>>).")]
    public async Task ValueOptionalMatchAsyncTResultOfNoneShouldReturnResultOfNoneDelegateFuncTTResultFuncCancellationTokenTaskTResult()
    {
        // Given
        ValueOptional<int> optional = ValueOptional<int>.None;
        CancellationToken noneToken = CancellationToken.None;

        // When
        string actual = await optional.MatchAsync(some: value => $"Some {value}", none: ct => { noneToken = ct; return Task.FromResult("None"); }, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal("None", actual);
        Assert.Equal(TestContext.Current.CancellationToken, noneToken);
    }

    [Fact(DisplayName = "ValueOptional<T>.MatchAsync<TResult> of None should return the result of the none delegate (Func<T, TResult>, Func<CancellationToken, Task<TResult>>) when the delegates complete asynchronously.")]
    public async Task ValueOptionalMatchAsyncTResultOfNoneShouldReturnResultOfNoneDelegateFuncTTResultFuncCancellationTokenTaskTResultWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueOptional<int> optional = ValueOptional<int>.None;
        CancellationToken noneToken = CancellationToken.None;

        // When
        string actual = await optional.MatchAsync(some: value => $"Some {value}", none: ct => { noneToken = ct; return Yielding.TaskOf("None"); }, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal("None", actual);
        Assert.Equal(TestContext.Current.CancellationToken, noneToken);
    }

    [Fact(DisplayName = "ValueOptional<T>.MatchAsync<TResult> of None should return the result of the none delegate (Func<T, TResult>, Func<CancellationToken, ValueTask<TResult>>).")]
    public async Task ValueOptionalMatchAsyncTResultOfNoneShouldReturnResultOfNoneDelegateFuncTTResultFuncCancellationTokenTaskTResultWithValueTaskDelegates()
    {
        // Given
        ValueOptional<int> optional = ValueOptional<int>.None;
        CancellationToken noneToken = CancellationToken.None;

        // When
        string actual = await optional.MatchAsync(some: value => $"Some {value}", none: ct => { noneToken = ct; return ValueTask.FromResult("None"); }, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal("None", actual);
        Assert.Equal(TestContext.Current.CancellationToken, noneToken);
    }

    [Fact(DisplayName = "ValueOptional<T>.MatchAsync<TResult> of None should return the result of the none delegate (Func<T, TResult>, Func<CancellationToken, ValueTask<TResult>>) when the delegates complete asynchronously.")]
    public async Task ValueOptionalMatchAsyncTResultOfNoneShouldReturnResultOfNoneDelegateFuncTTResultFuncCancellationTokenTaskTResultWithValueTaskDelegatesWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueOptional<int> optional = ValueOptional<int>.None;
        CancellationToken noneToken = CancellationToken.None;

        // When
        string actual = await optional.MatchAsync(some: value => $"Some {value}", none: ct => { noneToken = ct; return Yielding.ValueTaskOf("None"); }, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal("None", actual);
        Assert.Equal(TestContext.Current.CancellationToken, noneToken);
    }

    [Fact(DisplayName = "ValueOptional<T>.MatchAsync<TResult> of Some should return the result of the some delegate (Func<T, Task<TResult>>, Func<Task<TResult>>).")]
    public async Task ValueOptionalMatchAsyncTResultOfSomeShouldReturnResultOfSomeDelegateFuncTTaskTResultFuncTaskTResult()
    {
        // Given
        ValueOptional<int> optional = 21;

        // When
        string actual = await optional.MatchAsync(some: value => Task.FromResult($"Some {value}"), none: () => Task.FromResult("None"), cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal("Some 21", actual);
    }

    [Fact(DisplayName = "ValueOptional<T>.MatchAsync<TResult> of Some should return the result of the some delegate (Func<T, Task<TResult>>, Func<Task<TResult>>) when the delegates complete asynchronously.")]
    public async Task ValueOptionalMatchAsyncTResultOfSomeShouldReturnResultOfSomeDelegateFuncTTaskTResultFuncTaskTResultWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueOptional<int> optional = 21;

        // When
        string actual = await optional.MatchAsync(some: value => Yielding.TaskOf($"Some {value}"), none: () => Yielding.TaskOf("None"), cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal("Some 21", actual);
    }

    [Fact(DisplayName = "ValueOptional<T>.MatchAsync<TResult> of Some should return the result of the some delegate (Func<T, ValueTask<TResult>>, Func<ValueTask<TResult>>).")]
    public async Task ValueOptionalMatchAsyncTResultOfSomeShouldReturnResultOfSomeDelegateFuncTTaskTResultFuncTaskTResultWithValueTaskDelegates()
    {
        // Given
        ValueOptional<int> optional = 21;

        // When
        string actual = await optional.MatchAsync(some: value => ValueTask.FromResult($"Some {value}"), none: () => ValueTask.FromResult("None"), cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal("Some 21", actual);
    }

    [Fact(DisplayName = "ValueOptional<T>.MatchAsync<TResult> of Some should return the result of the some delegate (Func<T, ValueTask<TResult>>, Func<ValueTask<TResult>>) when the delegates complete asynchronously.")]
    public async Task ValueOptionalMatchAsyncTResultOfSomeShouldReturnResultOfSomeDelegateFuncTTaskTResultFuncTaskTResultWithValueTaskDelegatesWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueOptional<int> optional = 21;

        // When
        string actual = await optional.MatchAsync(some: value => Yielding.ValueTaskOf($"Some {value}"), none: () => Yielding.ValueTaskOf("None"), cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal("Some 21", actual);
    }

    [Fact(DisplayName = "ValueOptional<T>.MatchAsync<TResult> of None should return the result of the none delegate (Func<T, Task<TResult>>, Func<Task<TResult>>).")]
    public async Task ValueOptionalMatchAsyncTResultOfNoneShouldReturnResultOfNoneDelegateFuncTTaskTResultFuncTaskTResult()
    {
        // Given
        ValueOptional<int> optional = ValueOptional<int>.None;

        // When
        string actual = await optional.MatchAsync(some: value => Task.FromResult($"Some {value}"), none: () => Task.FromResult("None"), cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal("None", actual);
    }

    [Fact(DisplayName = "ValueOptional<T>.MatchAsync<TResult> of None should return the result of the none delegate (Func<T, Task<TResult>>, Func<Task<TResult>>) when the delegates complete asynchronously.")]
    public async Task ValueOptionalMatchAsyncTResultOfNoneShouldReturnResultOfNoneDelegateFuncTTaskTResultFuncTaskTResultWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueOptional<int> optional = ValueOptional<int>.None;

        // When
        string actual = await optional.MatchAsync(some: value => Yielding.TaskOf($"Some {value}"), none: () => Yielding.TaskOf("None"), cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal("None", actual);
    }

    [Fact(DisplayName = "ValueOptional<T>.MatchAsync<TResult> of None should return the result of the none delegate (Func<T, ValueTask<TResult>>, Func<ValueTask<TResult>>).")]
    public async Task ValueOptionalMatchAsyncTResultOfNoneShouldReturnResultOfNoneDelegateFuncTTaskTResultFuncTaskTResultWithValueTaskDelegates()
    {
        // Given
        ValueOptional<int> optional = ValueOptional<int>.None;

        // When
        string actual = await optional.MatchAsync(some: value => ValueTask.FromResult($"Some {value}"), none: () => ValueTask.FromResult("None"), cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal("None", actual);
    }

    [Fact(DisplayName = "ValueOptional<T>.MatchAsync<TResult> of None should return the result of the none delegate (Func<T, ValueTask<TResult>>, Func<ValueTask<TResult>>) when the delegates complete asynchronously.")]
    public async Task ValueOptionalMatchAsyncTResultOfNoneShouldReturnResultOfNoneDelegateFuncTTaskTResultFuncTaskTResultWithValueTaskDelegatesWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueOptional<int> optional = ValueOptional<int>.None;

        // When
        string actual = await optional.MatchAsync(some: value => Yielding.ValueTaskOf($"Some {value}"), none: () => Yielding.ValueTaskOf("None"), cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal("None", actual);
    }

    [Fact(DisplayName = "ValueOptional<T>.MatchAsync<TResult> of Some should return the result of the some delegate (Func<T, CancellationToken, Task<TResult>>, Func<CancellationToken, Task<TResult>>).")]
    public async Task ValueOptionalMatchAsyncTResultOfSomeShouldReturnResultOfSomeDelegateFuncTCancellationTokenTaskTResultFuncCancellationTokenTaskTResult()
    {
        // Given
        ValueOptional<int> optional = 21;
        CancellationToken someToken = CancellationToken.None;
        CancellationToken noneToken = CancellationToken.None;

        // When
        string actual = await optional.MatchAsync(some: (value, ct) => { someToken = ct; return Task.FromResult($"Some {value}"); }, none: ct => { noneToken = ct; return Task.FromResult("None"); }, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal("Some 21", actual);
        Assert.Equal(TestContext.Current.CancellationToken, someToken);
    }

    [Fact(DisplayName = "ValueOptional<T>.MatchAsync<TResult> of Some should return the result of the some delegate (Func<T, CancellationToken, Task<TResult>>, Func<CancellationToken, Task<TResult>>) when the delegates complete asynchronously.")]
    public async Task ValueOptionalMatchAsyncTResultOfSomeShouldReturnResultOfSomeDelegateFuncTCancellationTokenTaskTResultFuncCancellationTokenTaskTResultWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueOptional<int> optional = 21;
        CancellationToken someToken = CancellationToken.None;
        CancellationToken noneToken = CancellationToken.None;

        // When
        string actual = await optional.MatchAsync(some: (value, ct) => { someToken = ct; return Yielding.TaskOf($"Some {value}"); }, none: ct => { noneToken = ct; return Yielding.TaskOf("None"); }, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal("Some 21", actual);
        Assert.Equal(TestContext.Current.CancellationToken, someToken);
    }

    [Fact(DisplayName = "ValueOptional<T>.MatchAsync<TResult> of Some should return the result of the some delegate (Func<T, CancellationToken, ValueTask<TResult>>, Func<CancellationToken, ValueTask<TResult>>).")]
    public async Task ValueOptionalMatchAsyncTResultOfSomeShouldReturnResultOfSomeDelegateFuncTCancellationTokenTaskTResultFuncCancellationTokenTaskTResultWithValueTaskDelegates()
    {
        // Given
        ValueOptional<int> optional = 21;
        CancellationToken someToken = CancellationToken.None;
        CancellationToken noneToken = CancellationToken.None;

        // When
        string actual = await optional.MatchAsync(some: (value, ct) => { someToken = ct; return ValueTask.FromResult($"Some {value}"); }, none: ct => { noneToken = ct; return ValueTask.FromResult("None"); }, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal("Some 21", actual);
        Assert.Equal(TestContext.Current.CancellationToken, someToken);
    }

    [Fact(DisplayName = "ValueOptional<T>.MatchAsync<TResult> of Some should return the result of the some delegate (Func<T, CancellationToken, ValueTask<TResult>>, Func<CancellationToken, ValueTask<TResult>>) when the delegates complete asynchronously.")]
    public async Task ValueOptionalMatchAsyncTResultOfSomeShouldReturnResultOfSomeDelegateFuncTCancellationTokenTaskTResultFuncCancellationTokenTaskTResultWithValueTaskDelegatesWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueOptional<int> optional = 21;
        CancellationToken someToken = CancellationToken.None;
        CancellationToken noneToken = CancellationToken.None;

        // When
        string actual = await optional.MatchAsync(some: (value, ct) => { someToken = ct; return Yielding.ValueTaskOf($"Some {value}"); }, none: ct => { noneToken = ct; return Yielding.ValueTaskOf("None"); }, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal("Some 21", actual);
        Assert.Equal(TestContext.Current.CancellationToken, someToken);
    }

    [Fact(DisplayName = "ValueOptional<T>.MatchAsync<TResult> of None should return the result of the none delegate (Func<T, CancellationToken, Task<TResult>>, Func<CancellationToken, Task<TResult>>).")]
    public async Task ValueOptionalMatchAsyncTResultOfNoneShouldReturnResultOfNoneDelegateFuncTCancellationTokenTaskTResultFuncCancellationTokenTaskTResult()
    {
        // Given
        ValueOptional<int> optional = ValueOptional<int>.None;
        CancellationToken someToken = CancellationToken.None;
        CancellationToken noneToken = CancellationToken.None;

        // When
        string actual = await optional.MatchAsync(some: (value, ct) => { someToken = ct; return Task.FromResult($"Some {value}"); }, none: ct => { noneToken = ct; return Task.FromResult("None"); }, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal("None", actual);
        Assert.Equal(TestContext.Current.CancellationToken, noneToken);
    }

    [Fact(DisplayName = "ValueOptional<T>.MatchAsync<TResult> of None should return the result of the none delegate (Func<T, CancellationToken, Task<TResult>>, Func<CancellationToken, Task<TResult>>) when the delegates complete asynchronously.")]
    public async Task ValueOptionalMatchAsyncTResultOfNoneShouldReturnResultOfNoneDelegateFuncTCancellationTokenTaskTResultFuncCancellationTokenTaskTResultWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueOptional<int> optional = ValueOptional<int>.None;
        CancellationToken someToken = CancellationToken.None;
        CancellationToken noneToken = CancellationToken.None;

        // When
        string actual = await optional.MatchAsync(some: (value, ct) => { someToken = ct; return Yielding.TaskOf($"Some {value}"); }, none: ct => { noneToken = ct; return Yielding.TaskOf("None"); }, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal("None", actual);
        Assert.Equal(TestContext.Current.CancellationToken, noneToken);
    }

    [Fact(DisplayName = "ValueOptional<T>.MatchAsync<TResult> of None should return the result of the none delegate (Func<T, CancellationToken, ValueTask<TResult>>, Func<CancellationToken, ValueTask<TResult>>).")]
    public async Task ValueOptionalMatchAsyncTResultOfNoneShouldReturnResultOfNoneDelegateFuncTCancellationTokenTaskTResultFuncCancellationTokenTaskTResultWithValueTaskDelegates()
    {
        // Given
        ValueOptional<int> optional = ValueOptional<int>.None;
        CancellationToken someToken = CancellationToken.None;
        CancellationToken noneToken = CancellationToken.None;

        // When
        string actual = await optional.MatchAsync(some: (value, ct) => { someToken = ct; return ValueTask.FromResult($"Some {value}"); }, none: ct => { noneToken = ct; return ValueTask.FromResult("None"); }, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal("None", actual);
        Assert.Equal(TestContext.Current.CancellationToken, noneToken);
    }

    [Fact(DisplayName = "ValueOptional<T>.MatchAsync<TResult> of None should return the result of the none delegate (Func<T, CancellationToken, ValueTask<TResult>>, Func<CancellationToken, ValueTask<TResult>>) when the delegates complete asynchronously.")]
    public async Task ValueOptionalMatchAsyncTResultOfNoneShouldReturnResultOfNoneDelegateFuncTCancellationTokenTaskTResultFuncCancellationTokenTaskTResultWithValueTaskDelegatesWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueOptional<int> optional = ValueOptional<int>.None;
        CancellationToken someToken = CancellationToken.None;
        CancellationToken noneToken = CancellationToken.None;

        // When
        string actual = await optional.MatchAsync(some: (value, ct) => { someToken = ct; return Yielding.ValueTaskOf($"Some {value}"); }, none: ct => { noneToken = ct; return Yielding.ValueTaskOf("None"); }, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal("None", actual);
        Assert.Equal(TestContext.Current.CancellationToken, noneToken);
    }

    [Fact(DisplayName = "ValueOptional<T>.SelectAsync of Some should produce the expected result (Func<T, Task<TResult>>).")]
    public async Task ValueOptionalSelectAsyncOfSomeShouldProduceExpectedResultFuncTTaskTResult()
    {
        // Given
        ValueOptional<int> optional = 21;

        // When
        ValueOptional<int> actual = await optional.SelectAsync(value => Task.FromResult(value * 2), TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(ValueOptional<int>.Some(42), actual);
    }

    [Fact(DisplayName = "ValueOptional<T>.SelectAsync of Some should produce the expected result (Func<T, Task<TResult>>) when the delegates complete asynchronously.")]
    public async Task ValueOptionalSelectAsyncOfSomeShouldProduceExpectedResultFuncTTaskTResultWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueOptional<int> optional = 21;

        // When
        ValueOptional<int> actual = await optional.SelectAsync(value => Yielding.TaskOf(value * 2), TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(ValueOptional<int>.Some(42), actual);
    }

    [Fact(DisplayName = "ValueOptional<T>.SelectAsync of Some should produce the expected result (Func<T, ValueTask<TResult>>).")]
    public async Task ValueOptionalSelectAsyncOfSomeShouldProduceExpectedResultFuncTTaskTResultWithValueTaskDelegates()
    {
        // Given
        ValueOptional<int> optional = 21;

        // When
        ValueOptional<int> actual = await optional.SelectAsync(value => ValueTask.FromResult(value * 2), TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(ValueOptional<int>.Some(42), actual);
    }

    [Fact(DisplayName = "ValueOptional<T>.SelectAsync of Some should produce the expected result (Func<T, ValueTask<TResult>>) when the delegates complete asynchronously.")]
    public async Task ValueOptionalSelectAsyncOfSomeShouldProduceExpectedResultFuncTTaskTResultWithValueTaskDelegatesWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueOptional<int> optional = 21;

        // When
        ValueOptional<int> actual = await optional.SelectAsync(value => Yielding.ValueTaskOf(value * 2), TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(ValueOptional<int>.Some(42), actual);
    }

    [Fact(DisplayName = "ValueOptional<T>.SelectAsync should produce the same result as Select when the selector returns the default value.")]
    public async Task ValueOptionalSelectAsyncShouldProduceSameResultAsSelectWhenSelectorReturnsDefaultValue()
    {
        // Given
        ValueOptional<int> optional = 21;
        ValueOptional<int> expected = optional.Select(_ => 0);

        // When
        ValueOptional<int> actual = await optional.SelectAsync(_ => Task.FromResult(0), TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(ValueOptional<int>.Some(0), expected);
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueOptional<T>.SelectAsync should produce the same result as Select when the selector returns the default value when the delegates complete asynchronously.")]
    public async Task ValueOptionalSelectAsyncShouldProduceSameResultAsSelectWhenSelectorReturnsDefaultValueWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueOptional<int> optional = 21;
        ValueOptional<int> expected = optional.Select(_ => 0);

        // When
        ValueOptional<int> actual = await optional.SelectAsync(_ => Yielding.TaskOf(0), TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(ValueOptional<int>.Some(0), expected);
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueOptional<T>.SelectAsync should produce the same result as Select when the selector returns the default value with ValueTask delegates.")]
    public async Task ValueOptionalSelectAsyncShouldProduceSameResultAsSelectWhenSelectorReturnsDefaultValueWithValueTaskDelegates()
    {
        // Given
        ValueOptional<int> optional = 21;
        ValueOptional<int> expected = optional.Select(_ => 0);

        // When
        ValueOptional<int> actual = await optional.SelectAsync(_ => ValueTask.FromResult(0), TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(ValueOptional<int>.Some(0), expected);
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueOptional<T>.SelectAsync should produce the same result as Select when the selector returns the default value with ValueTask delegates when the delegates complete asynchronously.")]
    public async Task ValueOptionalSelectAsyncShouldProduceSameResultAsSelectWhenSelectorReturnsDefaultValueWithValueTaskDelegatesWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueOptional<int> optional = 21;
        ValueOptional<int> expected = optional.Select(_ => 0);

        // When
        ValueOptional<int> actual = await optional.SelectAsync(_ => Yielding.ValueTaskOf(0), TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(ValueOptional<int>.Some(0), expected);
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ValueOptional<T>.SelectAsync of None should produce the expected result (Func<T, Task<TResult>>).")]
    public async Task ValueOptionalSelectAsyncOfNoneShouldProduceExpectedResultFuncTTaskTResult()
    {
        // Given
        ValueOptional<int> optional = ValueOptional<int>.None;

        // When
        ValueOptional<int> actual = await optional.SelectAsync(value => Task.FromResult(value * 2), TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(ValueOptional<int>.None, actual);
    }

    [Fact(DisplayName = "ValueOptional<T>.SelectAsync of None should produce the expected result (Func<T, Task<TResult>>) when the delegates complete asynchronously.")]
    public async Task ValueOptionalSelectAsyncOfNoneShouldProduceExpectedResultFuncTTaskTResultWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueOptional<int> optional = ValueOptional<int>.None;

        // When
        ValueOptional<int> actual = await optional.SelectAsync(value => Yielding.TaskOf(value * 2), TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(ValueOptional<int>.None, actual);
    }

    [Fact(DisplayName = "ValueOptional<T>.SelectAsync of None should produce the expected result (Func<T, ValueTask<TResult>>).")]
    public async Task ValueOptionalSelectAsyncOfNoneShouldProduceExpectedResultFuncTTaskTResultWithValueTaskDelegates()
    {
        // Given
        ValueOptional<int> optional = ValueOptional<int>.None;

        // When
        ValueOptional<int> actual = await optional.SelectAsync(value => ValueTask.FromResult(value * 2), TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(ValueOptional<int>.None, actual);
    }

    [Fact(DisplayName = "ValueOptional<T>.SelectAsync of None should produce the expected result (Func<T, ValueTask<TResult>>) when the delegates complete asynchronously.")]
    public async Task ValueOptionalSelectAsyncOfNoneShouldProduceExpectedResultFuncTTaskTResultWithValueTaskDelegatesWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueOptional<int> optional = ValueOptional<int>.None;

        // When
        ValueOptional<int> actual = await optional.SelectAsync(value => Yielding.ValueTaskOf(value * 2), TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(ValueOptional<int>.None, actual);
    }

    [Fact(DisplayName = "ValueOptional<T>.SelectAsync of Some should produce the expected result (Func<T, CancellationToken, Task<TResult>>).")]
    public async Task ValueOptionalSelectAsyncOfSomeShouldProduceExpectedResultFuncTCancellationTokenTaskTResult()
    {
        // Given
        ValueOptional<int> optional = 21;
        CancellationToken selectorToken = CancellationToken.None;

        // When
        ValueOptional<int> actual = await optional.SelectAsync((value, ct) => { selectorToken = ct; return Task.FromResult(value * 2); }, TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(ValueOptional<int>.Some(42), actual);
        Assert.Equal(TestContext.Current.CancellationToken, selectorToken);
    }

    [Fact(DisplayName = "ValueOptional<T>.SelectAsync of Some should produce the expected result (Func<T, CancellationToken, Task<TResult>>) when the delegates complete asynchronously.")]
    public async Task ValueOptionalSelectAsyncOfSomeShouldProduceExpectedResultFuncTCancellationTokenTaskTResultWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueOptional<int> optional = 21;
        CancellationToken selectorToken = CancellationToken.None;

        // When
        ValueOptional<int> actual = await optional.SelectAsync((value, ct) => { selectorToken = ct; return Yielding.TaskOf(value * 2); }, TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(ValueOptional<int>.Some(42), actual);
        Assert.Equal(TestContext.Current.CancellationToken, selectorToken);
    }

    [Fact(DisplayName = "ValueOptional<T>.SelectAsync of Some should produce the expected result (Func<T, CancellationToken, ValueTask<TResult>>).")]
    public async Task ValueOptionalSelectAsyncOfSomeShouldProduceExpectedResultFuncTCancellationTokenTaskTResultWithValueTaskDelegates()
    {
        // Given
        ValueOptional<int> optional = 21;
        CancellationToken selectorToken = CancellationToken.None;

        // When
        ValueOptional<int> actual = await optional.SelectAsync((value, ct) => { selectorToken = ct; return ValueTask.FromResult(value * 2); }, TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(ValueOptional<int>.Some(42), actual);
        Assert.Equal(TestContext.Current.CancellationToken, selectorToken);
    }

    [Fact(DisplayName = "ValueOptional<T>.SelectAsync of Some should produce the expected result (Func<T, CancellationToken, ValueTask<TResult>>) when the delegates complete asynchronously.")]
    public async Task ValueOptionalSelectAsyncOfSomeShouldProduceExpectedResultFuncTCancellationTokenTaskTResultWithValueTaskDelegatesWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueOptional<int> optional = 21;
        CancellationToken selectorToken = CancellationToken.None;

        // When
        ValueOptional<int> actual = await optional.SelectAsync((value, ct) => { selectorToken = ct; return Yielding.ValueTaskOf(value * 2); }, TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(ValueOptional<int>.Some(42), actual);
        Assert.Equal(TestContext.Current.CancellationToken, selectorToken);
    }

    [Fact(DisplayName = "ValueOptional<T>.SelectAsync of None should produce the expected result (Func<T, CancellationToken, Task<TResult>>).")]
    public async Task ValueOptionalSelectAsyncOfNoneShouldProduceExpectedResultFuncTCancellationTokenTaskTResult()
    {
        // Given
        ValueOptional<int> optional = ValueOptional<int>.None;
        CancellationToken selectorToken = CancellationToken.None;

        // When
        ValueOptional<int> actual = await optional.SelectAsync((value, ct) => { selectorToken = ct; return Task.FromResult(value * 2); }, TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(ValueOptional<int>.None, actual);
    }

    [Fact(DisplayName = "ValueOptional<T>.SelectAsync of None should produce the expected result (Func<T, CancellationToken, Task<TResult>>) when the delegates complete asynchronously.")]
    public async Task ValueOptionalSelectAsyncOfNoneShouldProduceExpectedResultFuncTCancellationTokenTaskTResultWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueOptional<int> optional = ValueOptional<int>.None;
        CancellationToken selectorToken = CancellationToken.None;

        // When
        ValueOptional<int> actual = await optional.SelectAsync((value, ct) => { selectorToken = ct; return Yielding.TaskOf(value * 2); }, TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(ValueOptional<int>.None, actual);
    }

    [Fact(DisplayName = "ValueOptional<T>.SelectAsync of None should produce the expected result (Func<T, CancellationToken, ValueTask<TResult>>).")]
    public async Task ValueOptionalSelectAsyncOfNoneShouldProduceExpectedResultFuncTCancellationTokenTaskTResultWithValueTaskDelegates()
    {
        // Given
        ValueOptional<int> optional = ValueOptional<int>.None;
        CancellationToken selectorToken = CancellationToken.None;

        // When
        ValueOptional<int> actual = await optional.SelectAsync((value, ct) => { selectorToken = ct; return ValueTask.FromResult(value * 2); }, TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(ValueOptional<int>.None, actual);
    }

    [Fact(DisplayName = "ValueOptional<T>.SelectAsync of None should produce the expected result (Func<T, CancellationToken, ValueTask<TResult>>) when the delegates complete asynchronously.")]
    public async Task ValueOptionalSelectAsyncOfNoneShouldProduceExpectedResultFuncTCancellationTokenTaskTResultWithValueTaskDelegatesWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueOptional<int> optional = ValueOptional<int>.None;
        CancellationToken selectorToken = CancellationToken.None;

        // When
        ValueOptional<int> actual = await optional.SelectAsync((value, ct) => { selectorToken = ct; return Yielding.ValueTaskOf(value * 2); }, TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(ValueOptional<int>.None, actual);
    }

    [Fact(DisplayName = "ValueOptional<T>.SelectManyAsync of Some should produce the expected result (Func<T, Task<ValueOptional<TResult>>>).")]
    public async Task ValueOptionalSelectManyAsyncOfSomeShouldProduceExpectedResultFuncTTaskOptionalTResult()
    {
        // Given
        ValueOptional<int> optional = 21;

        // When
        ValueOptional<int> actual = await optional.SelectManyAsync(value => Task.FromResult<ValueOptional<int>>(value * 2), TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(ValueOptional<int>.Some(42), actual);
    }

    [Fact(DisplayName = "ValueOptional<T>.SelectManyAsync of Some should produce the expected result (Func<T, Task<ValueOptional<TResult>>>) when the delegates complete asynchronously.")]
    public async Task ValueOptionalSelectManyAsyncOfSomeShouldProduceExpectedResultFuncTTaskOptionalTResultWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueOptional<int> optional = 21;

        // When
        ValueOptional<int> actual = await optional.SelectManyAsync(value => Yielding.TaskOf<ValueOptional<int>>(value * 2), TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(ValueOptional<int>.Some(42), actual);
    }

    [Fact(DisplayName = "ValueOptional<T>.SelectManyAsync of Some should produce the expected result (Func<T, ValueTask<ValueOptional<TResult>>>).")]
    public async Task ValueOptionalSelectManyAsyncOfSomeShouldProduceExpectedResultFuncTTaskOptionalTResultWithValueTaskDelegates()
    {
        // Given
        ValueOptional<int> optional = 21;

        // When
        ValueOptional<int> actual = await optional.SelectManyAsync(value => ValueTask.FromResult<ValueOptional<int>>(value * 2), TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(ValueOptional<int>.Some(42), actual);
    }

    [Fact(DisplayName = "ValueOptional<T>.SelectManyAsync of Some should produce the expected result (Func<T, ValueTask<ValueOptional<TResult>>>) when the delegates complete asynchronously.")]
    public async Task ValueOptionalSelectManyAsyncOfSomeShouldProduceExpectedResultFuncTTaskOptionalTResultWithValueTaskDelegatesWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueOptional<int> optional = 21;

        // When
        ValueOptional<int> actual = await optional.SelectManyAsync(value => Yielding.ValueTaskOf<ValueOptional<int>>(value * 2), TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(ValueOptional<int>.Some(42), actual);
    }

    [Fact(DisplayName = "ValueOptional<T>.SelectManyAsync of None should produce the expected result (Func<T, Task<ValueOptional<TResult>>>).")]
    public async Task ValueOptionalSelectManyAsyncOfNoneShouldProduceExpectedResultFuncTTaskOptionalTResult()
    {
        // Given
        ValueOptional<int> optional = ValueOptional<int>.None;

        // When
        ValueOptional<int> actual = await optional.SelectManyAsync(value => Task.FromResult<ValueOptional<int>>(value * 2), TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(ValueOptional<int>.None, actual);
    }

    [Fact(DisplayName = "ValueOptional<T>.SelectManyAsync of None should produce the expected result (Func<T, Task<ValueOptional<TResult>>>) when the delegates complete asynchronously.")]
    public async Task ValueOptionalSelectManyAsyncOfNoneShouldProduceExpectedResultFuncTTaskOptionalTResultWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueOptional<int> optional = ValueOptional<int>.None;

        // When
        ValueOptional<int> actual = await optional.SelectManyAsync(value => Yielding.TaskOf<ValueOptional<int>>(value * 2), TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(ValueOptional<int>.None, actual);
    }

    [Fact(DisplayName = "ValueOptional<T>.SelectManyAsync of None should produce the expected result (Func<T, ValueTask<ValueOptional<TResult>>>).")]
    public async Task ValueOptionalSelectManyAsyncOfNoneShouldProduceExpectedResultFuncTTaskOptionalTResultWithValueTaskDelegates()
    {
        // Given
        ValueOptional<int> optional = ValueOptional<int>.None;

        // When
        ValueOptional<int> actual = await optional.SelectManyAsync(value => ValueTask.FromResult<ValueOptional<int>>(value * 2), TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(ValueOptional<int>.None, actual);
    }

    [Fact(DisplayName = "ValueOptional<T>.SelectManyAsync of None should produce the expected result (Func<T, ValueTask<ValueOptional<TResult>>>) when the delegates complete asynchronously.")]
    public async Task ValueOptionalSelectManyAsyncOfNoneShouldProduceExpectedResultFuncTTaskOptionalTResultWithValueTaskDelegatesWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueOptional<int> optional = ValueOptional<int>.None;

        // When
        ValueOptional<int> actual = await optional.SelectManyAsync(value => Yielding.ValueTaskOf<ValueOptional<int>>(value * 2), TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(ValueOptional<int>.None, actual);
    }

    [Fact(DisplayName = "ValueOptional<T>.SelectManyAsync of Some should produce the expected result (Func<T, CancellationToken, Task<ValueOptional<TResult>>>).")]
    public async Task ValueOptionalSelectManyAsyncOfSomeShouldProduceExpectedResultFuncTCancellationTokenTaskOptionalTResult()
    {
        // Given
        ValueOptional<int> optional = 21;
        CancellationToken selectorToken = CancellationToken.None;

        // When
        ValueOptional<int> actual = await optional.SelectManyAsync((value, ct) => { selectorToken = ct; return Task.FromResult<ValueOptional<int>>(value * 2); }, TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(ValueOptional<int>.Some(42), actual);
        Assert.Equal(TestContext.Current.CancellationToken, selectorToken);
    }

    [Fact(DisplayName = "ValueOptional<T>.SelectManyAsync of Some should produce the expected result (Func<T, CancellationToken, Task<ValueOptional<TResult>>>) when the delegates complete asynchronously.")]
    public async Task ValueOptionalSelectManyAsyncOfSomeShouldProduceExpectedResultFuncTCancellationTokenTaskOptionalTResultWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueOptional<int> optional = 21;
        CancellationToken selectorToken = CancellationToken.None;

        // When
        ValueOptional<int> actual = await optional.SelectManyAsync((value, ct) => { selectorToken = ct; return Yielding.TaskOf<ValueOptional<int>>(value * 2); }, TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(ValueOptional<int>.Some(42), actual);
        Assert.Equal(TestContext.Current.CancellationToken, selectorToken);
    }

    [Fact(DisplayName = "ValueOptional<T>.SelectManyAsync of Some should produce the expected result (Func<T, CancellationToken, ValueTask<ValueOptional<TResult>>>).")]
    public async Task ValueOptionalSelectManyAsyncOfSomeShouldProduceExpectedResultFuncTCancellationTokenTaskOptionalTResultWithValueTaskDelegates()
    {
        // Given
        ValueOptional<int> optional = 21;
        CancellationToken selectorToken = CancellationToken.None;

        // When
        ValueOptional<int> actual = await optional.SelectManyAsync((value, ct) => { selectorToken = ct; return ValueTask.FromResult<ValueOptional<int>>(value * 2); }, TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(ValueOptional<int>.Some(42), actual);
        Assert.Equal(TestContext.Current.CancellationToken, selectorToken);
    }

    [Fact(DisplayName = "ValueOptional<T>.SelectManyAsync of Some should produce the expected result (Func<T, CancellationToken, ValueTask<ValueOptional<TResult>>>) when the delegates complete asynchronously.")]
    public async Task ValueOptionalSelectManyAsyncOfSomeShouldProduceExpectedResultFuncTCancellationTokenTaskOptionalTResultWithValueTaskDelegatesWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueOptional<int> optional = 21;
        CancellationToken selectorToken = CancellationToken.None;

        // When
        ValueOptional<int> actual = await optional.SelectManyAsync((value, ct) => { selectorToken = ct; return Yielding.ValueTaskOf<ValueOptional<int>>(value * 2); }, TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(ValueOptional<int>.Some(42), actual);
        Assert.Equal(TestContext.Current.CancellationToken, selectorToken);
    }

    [Fact(DisplayName = "ValueOptional<T>.SelectManyAsync of None should produce the expected result (Func<T, CancellationToken, Task<ValueOptional<TResult>>>).")]
    public async Task ValueOptionalSelectManyAsyncOfNoneShouldProduceExpectedResultFuncTCancellationTokenTaskOptionalTResult()
    {
        // Given
        ValueOptional<int> optional = ValueOptional<int>.None;
        CancellationToken selectorToken = CancellationToken.None;

        // When
        ValueOptional<int> actual = await optional.SelectManyAsync((value, ct) => { selectorToken = ct; return Task.FromResult<ValueOptional<int>>(value * 2); }, TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(ValueOptional<int>.None, actual);
    }

    [Fact(DisplayName = "ValueOptional<T>.SelectManyAsync of None should produce the expected result (Func<T, CancellationToken, Task<ValueOptional<TResult>>>) when the delegates complete asynchronously.")]
    public async Task ValueOptionalSelectManyAsyncOfNoneShouldProduceExpectedResultFuncTCancellationTokenTaskOptionalTResultWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueOptional<int> optional = ValueOptional<int>.None;
        CancellationToken selectorToken = CancellationToken.None;

        // When
        ValueOptional<int> actual = await optional.SelectManyAsync((value, ct) => { selectorToken = ct; return Yielding.TaskOf<ValueOptional<int>>(value * 2); }, TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(ValueOptional<int>.None, actual);
    }

    [Fact(DisplayName = "ValueOptional<T>.SelectManyAsync of None should produce the expected result (Func<T, CancellationToken, ValueTask<ValueOptional<TResult>>>).")]
    public async Task ValueOptionalSelectManyAsyncOfNoneShouldProduceExpectedResultFuncTCancellationTokenTaskOptionalTResultWithValueTaskDelegates()
    {
        // Given
        ValueOptional<int> optional = ValueOptional<int>.None;
        CancellationToken selectorToken = CancellationToken.None;

        // When
        ValueOptional<int> actual = await optional.SelectManyAsync((value, ct) => { selectorToken = ct; return ValueTask.FromResult<ValueOptional<int>>(value * 2); }, TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(ValueOptional<int>.None, actual);
    }

    [Fact(DisplayName = "ValueOptional<T>.SelectManyAsync of None should produce the expected result (Func<T, CancellationToken, ValueTask<ValueOptional<TResult>>>) when the delegates complete asynchronously.")]
    public async Task ValueOptionalSelectManyAsyncOfNoneShouldProduceExpectedResultFuncTCancellationTokenTaskOptionalTResultWithValueTaskDelegatesWhenDelegatesCompleteAsynchronously()
    {
        // Given
        ValueOptional<int> optional = ValueOptional<int>.None;
        CancellationToken selectorToken = CancellationToken.None;

        // When
        ValueOptional<int> actual = await optional.SelectManyAsync((value, ct) => { selectorToken = ct; return Yielding.ValueTaskOf<ValueOptional<int>>(value * 2); }, TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(ValueOptional<int>.None, actual);
    }
}
