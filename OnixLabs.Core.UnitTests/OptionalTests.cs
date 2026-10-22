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

    [Fact(DisplayName = "Optional implicit conversion should produce the expected result for default values.")]
    public void OptionalImplicitConversionShouldProduceExpectedResultForDefaultValues()
    {
        // Given / When
        Optional<int> number = 0;
        Optional<string> text = (string?)null;

        // Then
        Assert.True(Optional<int>.IsSome(number));
        Assert.True(number.HasValue);
        Assert.IsType<Some<int>>(number);
        Assert.Equal(0, number);

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
        Assert.Null(actualText);
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

    [Fact(DisplayName = "Optional<T>.MatchAsync of Some should invoke the some delegate (Func<T, Task>, Action).")]
    public async Task OptionalMatchAsyncOfSomeShouldInvokeSomeDelegateFuncTTaskAction()
    {
        // Given
        Optional<int> optional = 21;
        int someValue = 0;
        bool noneCalled = false;

        // When
        await optional.MatchAsync(some: value => { someValue = value; return Task.CompletedTask; }, none: () => noneCalled = true, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(21, someValue);
        Assert.False(noneCalled);
    }

    [Fact(DisplayName = "Optional<T>.MatchAsync of Some should invoke the some delegate (Func<T, ValueTask>, Action).")]
    public async Task OptionalMatchAsyncOfSomeShouldInvokeSomeDelegateFuncTTaskActionWithValueTaskDelegates()
    {
        // Given
        Optional<int> optional = 21;
        int someValue = 0;
        bool noneCalled = false;

        // When
        await optional.MatchAsync(some: value => { someValue = value; return ValueTask.CompletedTask; }, none: () => noneCalled = true, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(21, someValue);
        Assert.False(noneCalled);
    }

    [Fact(DisplayName = "Optional<T>.MatchAsync of None should invoke the none delegate (Func<T, Task>, Action).")]
    public async Task OptionalMatchAsyncOfNoneShouldInvokeNoneDelegateFuncTTaskAction()
    {
        // Given
        Optional<int> optional = Optional<int>.None;
        int someValue = 0;
        bool noneCalled = false;

        // When
        await optional.MatchAsync(some: value => { someValue = value; return Task.CompletedTask; }, none: () => noneCalled = true, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(0, someValue);
        Assert.True(noneCalled);
    }

    [Fact(DisplayName = "Optional<T>.MatchAsync of None should invoke the none delegate (Func<T, ValueTask>, Action).")]
    public async Task OptionalMatchAsyncOfNoneShouldInvokeNoneDelegateFuncTTaskActionWithValueTaskDelegates()
    {
        // Given
        Optional<int> optional = Optional<int>.None;
        int someValue = 0;
        bool noneCalled = false;

        // When
        await optional.MatchAsync(some: value => { someValue = value; return ValueTask.CompletedTask; }, none: () => noneCalled = true, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(0, someValue);
        Assert.True(noneCalled);
    }

    [Fact(DisplayName = "Optional<T>.MatchAsync should do nothing when delegates are null (Func<T, Task>, Action).")]
    public async Task OptionalMatchAsyncShouldDoNothingWhenDelegatesAreNullFuncTTaskAction()
    {
        // Given
        Optional<int> some = 21;
        Optional<int> none = Optional<int>.None;

        // When
        await some.MatchAsync(some: (Func<int, Task>?)null, none: (Action?)null, cancellationToken: TestContext.Current.CancellationToken);
        await none.MatchAsync(some: (Func<int, Task>?)null, none: (Action?)null, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(Optional<int>.IsSome(some));
        Assert.True(Optional<int>.IsNone(none));
    }

    [Fact(DisplayName = "Optional<T>.MatchAsync should do nothing when delegates are null (Func<T, ValueTask>, Action).")]
    public async Task OptionalMatchAsyncShouldDoNothingWhenDelegatesAreNullFuncTTaskActionWithValueTaskDelegates()
    {
        // Given
        Optional<int> some = 21;
        Optional<int> none = Optional<int>.None;

        // When
        await some.MatchAsync(some: (Func<int, ValueTask>?)null, none: (Action?)null, cancellationToken: TestContext.Current.CancellationToken);
        await none.MatchAsync(some: (Func<int, ValueTask>?)null, none: (Action?)null, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(Optional<int>.IsSome(some));
        Assert.True(Optional<int>.IsNone(none));
    }

    [Fact(DisplayName = "Optional<T>.MatchAsync of Some should invoke the some delegate (Func<T, CancellationToken, Task>, Action).")]
    public async Task OptionalMatchAsyncOfSomeShouldInvokeSomeDelegateFuncTCancellationTokenTaskAction()
    {
        // Given
        Optional<int> optional = 21;
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

    [Fact(DisplayName = "Optional<T>.MatchAsync of Some should invoke the some delegate (Func<T, CancellationToken, ValueTask>, Action).")]
    public async Task OptionalMatchAsyncOfSomeShouldInvokeSomeDelegateFuncTCancellationTokenTaskActionWithValueTaskDelegates()
    {
        // Given
        Optional<int> optional = 21;
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

    [Fact(DisplayName = "Optional<T>.MatchAsync of None should invoke the none delegate (Func<T, CancellationToken, Task>, Action).")]
    public async Task OptionalMatchAsyncOfNoneShouldInvokeNoneDelegateFuncTCancellationTokenTaskAction()
    {
        // Given
        Optional<int> optional = Optional<int>.None;
        int someValue = 0;
        bool noneCalled = false;
        CancellationToken someToken = CancellationToken.None;

        // When
        await optional.MatchAsync(some: (value, ct) => { someValue = value; someToken = ct; return Task.CompletedTask; }, none: () => noneCalled = true, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(0, someValue);
        Assert.True(noneCalled);
    }

    [Fact(DisplayName = "Optional<T>.MatchAsync of None should invoke the none delegate (Func<T, CancellationToken, ValueTask>, Action).")]
    public async Task OptionalMatchAsyncOfNoneShouldInvokeNoneDelegateFuncTCancellationTokenTaskActionWithValueTaskDelegates()
    {
        // Given
        Optional<int> optional = Optional<int>.None;
        int someValue = 0;
        bool noneCalled = false;
        CancellationToken someToken = CancellationToken.None;

        // When
        await optional.MatchAsync(some: (value, ct) => { someValue = value; someToken = ct; return ValueTask.CompletedTask; }, none: () => noneCalled = true, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(0, someValue);
        Assert.True(noneCalled);
    }

    [Fact(DisplayName = "Optional<T>.MatchAsync should do nothing when delegates are null (Func<T, CancellationToken, Task>, Action).")]
    public async Task OptionalMatchAsyncShouldDoNothingWhenDelegatesAreNullFuncTCancellationTokenTaskAction()
    {
        // Given
        Optional<int> some = 21;
        Optional<int> none = Optional<int>.None;

        // When
        await some.MatchAsync(some: (Func<int, CancellationToken, Task>?)null, none: (Action?)null, cancellationToken: TestContext.Current.CancellationToken);
        await none.MatchAsync(some: (Func<int, CancellationToken, Task>?)null, none: (Action?)null, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(Optional<int>.IsSome(some));
        Assert.True(Optional<int>.IsNone(none));
    }

    [Fact(DisplayName = "Optional<T>.MatchAsync should do nothing when delegates are null (Func<T, CancellationToken, ValueTask>, Action).")]
    public async Task OptionalMatchAsyncShouldDoNothingWhenDelegatesAreNullFuncTCancellationTokenTaskActionWithValueTaskDelegates()
    {
        // Given
        Optional<int> some = 21;
        Optional<int> none = Optional<int>.None;

        // When
        await some.MatchAsync(some: (Func<int, CancellationToken, ValueTask>?)null, none: (Action?)null, cancellationToken: TestContext.Current.CancellationToken);
        await none.MatchAsync(some: (Func<int, CancellationToken, ValueTask>?)null, none: (Action?)null, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(Optional<int>.IsSome(some));
        Assert.True(Optional<int>.IsNone(none));
    }

    [Fact(DisplayName = "Optional<T>.MatchAsync of Some should invoke the some delegate (Action<T>, Func<Task>).")]
    public async Task OptionalMatchAsyncOfSomeShouldInvokeSomeDelegateActionTFuncTask()
    {
        // Given
        Optional<int> optional = 21;
        int someValue = 0;
        bool noneCalled = false;

        // When
        await optional.MatchAsync(some: value => someValue = value, none: () => { noneCalled = true; return Task.CompletedTask; }, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(21, someValue);
        Assert.False(noneCalled);
    }

    [Fact(DisplayName = "Optional<T>.MatchAsync of Some should invoke the some delegate (Action<T>, Func<ValueTask>).")]
    public async Task OptionalMatchAsyncOfSomeShouldInvokeSomeDelegateActionTFuncTaskWithValueTaskDelegates()
    {
        // Given
        Optional<int> optional = 21;
        int someValue = 0;
        bool noneCalled = false;

        // When
        await optional.MatchAsync(some: value => someValue = value, none: () => { noneCalled = true; return ValueTask.CompletedTask; }, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(21, someValue);
        Assert.False(noneCalled);
    }

    [Fact(DisplayName = "Optional<T>.MatchAsync of None should invoke the none delegate (Action<T>, Func<Task>).")]
    public async Task OptionalMatchAsyncOfNoneShouldInvokeNoneDelegateActionTFuncTask()
    {
        // Given
        Optional<int> optional = Optional<int>.None;
        int someValue = 0;
        bool noneCalled = false;

        // When
        await optional.MatchAsync(some: value => someValue = value, none: () => { noneCalled = true; return Task.CompletedTask; }, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(0, someValue);
        Assert.True(noneCalled);
    }

    [Fact(DisplayName = "Optional<T>.MatchAsync of None should invoke the none delegate (Action<T>, Func<ValueTask>).")]
    public async Task OptionalMatchAsyncOfNoneShouldInvokeNoneDelegateActionTFuncTaskWithValueTaskDelegates()
    {
        // Given
        Optional<int> optional = Optional<int>.None;
        int someValue = 0;
        bool noneCalled = false;

        // When
        await optional.MatchAsync(some: value => someValue = value, none: () => { noneCalled = true; return ValueTask.CompletedTask; }, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(0, someValue);
        Assert.True(noneCalled);
    }

    [Fact(DisplayName = "Optional<T>.MatchAsync should do nothing when delegates are null (Action<T>, Func<Task>).")]
    public async Task OptionalMatchAsyncShouldDoNothingWhenDelegatesAreNullActionTFuncTask()
    {
        // Given
        Optional<int> some = 21;
        Optional<int> none = Optional<int>.None;

        // When
        await some.MatchAsync(some: (Action<int>?)null, none: (Func<Task>?)null, cancellationToken: TestContext.Current.CancellationToken);
        await none.MatchAsync(some: (Action<int>?)null, none: (Func<Task>?)null, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(Optional<int>.IsSome(some));
        Assert.True(Optional<int>.IsNone(none));
    }

    [Fact(DisplayName = "Optional<T>.MatchAsync should do nothing when delegates are null (Action<T>, Func<ValueTask>).")]
    public async Task OptionalMatchAsyncShouldDoNothingWhenDelegatesAreNullActionTFuncTaskWithValueTaskDelegates()
    {
        // Given
        Optional<int> some = 21;
        Optional<int> none = Optional<int>.None;

        // When
        await some.MatchAsync(some: (Action<int>?)null, none: (Func<ValueTask>?)null, cancellationToken: TestContext.Current.CancellationToken);
        await none.MatchAsync(some: (Action<int>?)null, none: (Func<ValueTask>?)null, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(Optional<int>.IsSome(some));
        Assert.True(Optional<int>.IsNone(none));
    }

    [Fact(DisplayName = "Optional<T>.MatchAsync of Some should invoke the some delegate (Action<T>, Func<CancellationToken, Task>).")]
    public async Task OptionalMatchAsyncOfSomeShouldInvokeSomeDelegateActionTFuncCancellationTokenTask()
    {
        // Given
        Optional<int> optional = 21;
        int someValue = 0;
        bool noneCalled = false;
        CancellationToken noneToken = CancellationToken.None;

        // When
        await optional.MatchAsync(some: value => someValue = value, none: ct => { noneCalled = true; noneToken = ct; return Task.CompletedTask; }, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(21, someValue);
        Assert.False(noneCalled);
    }

    [Fact(DisplayName = "Optional<T>.MatchAsync of Some should invoke the some delegate (Action<T>, Func<CancellationToken, ValueTask>).")]
    public async Task OptionalMatchAsyncOfSomeShouldInvokeSomeDelegateActionTFuncCancellationTokenTaskWithValueTaskDelegates()
    {
        // Given
        Optional<int> optional = 21;
        int someValue = 0;
        bool noneCalled = false;
        CancellationToken noneToken = CancellationToken.None;

        // When
        await optional.MatchAsync(some: value => someValue = value, none: ct => { noneCalled = true; noneToken = ct; return ValueTask.CompletedTask; }, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(21, someValue);
        Assert.False(noneCalled);
    }

    [Fact(DisplayName = "Optional<T>.MatchAsync of None should invoke the none delegate (Action<T>, Func<CancellationToken, Task>).")]
    public async Task OptionalMatchAsyncOfNoneShouldInvokeNoneDelegateActionTFuncCancellationTokenTask()
    {
        // Given
        Optional<int> optional = Optional<int>.None;
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

    [Fact(DisplayName = "Optional<T>.MatchAsync of None should invoke the none delegate (Action<T>, Func<CancellationToken, ValueTask>).")]
    public async Task OptionalMatchAsyncOfNoneShouldInvokeNoneDelegateActionTFuncCancellationTokenTaskWithValueTaskDelegates()
    {
        // Given
        Optional<int> optional = Optional<int>.None;
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

    [Fact(DisplayName = "Optional<T>.MatchAsync should do nothing when delegates are null (Action<T>, Func<CancellationToken, Task>).")]
    public async Task OptionalMatchAsyncShouldDoNothingWhenDelegatesAreNullActionTFuncCancellationTokenTask()
    {
        // Given
        Optional<int> some = 21;
        Optional<int> none = Optional<int>.None;

        // When
        await some.MatchAsync(some: (Action<int>?)null, none: (Func<CancellationToken, Task>?)null, cancellationToken: TestContext.Current.CancellationToken);
        await none.MatchAsync(some: (Action<int>?)null, none: (Func<CancellationToken, Task>?)null, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(Optional<int>.IsSome(some));
        Assert.True(Optional<int>.IsNone(none));
    }

    [Fact(DisplayName = "Optional<T>.MatchAsync should do nothing when delegates are null (Action<T>, Func<CancellationToken, ValueTask>).")]
    public async Task OptionalMatchAsyncShouldDoNothingWhenDelegatesAreNullActionTFuncCancellationTokenTaskWithValueTaskDelegates()
    {
        // Given
        Optional<int> some = 21;
        Optional<int> none = Optional<int>.None;

        // When
        await some.MatchAsync(some: (Action<int>?)null, none: (Func<CancellationToken, ValueTask>?)null, cancellationToken: TestContext.Current.CancellationToken);
        await none.MatchAsync(some: (Action<int>?)null, none: (Func<CancellationToken, ValueTask>?)null, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(Optional<int>.IsSome(some));
        Assert.True(Optional<int>.IsNone(none));
    }

    [Fact(DisplayName = "Optional<T>.MatchAsync of Some should invoke the some delegate (Func<T, Task>, Func<Task>).")]
    public async Task OptionalMatchAsyncOfSomeShouldInvokeSomeDelegateFuncTTaskFuncTask()
    {
        // Given
        Optional<int> optional = 21;
        int someValue = 0;
        bool noneCalled = false;

        // When
        await optional.MatchAsync(some: value => { someValue = value; return Task.CompletedTask; }, none: () => { noneCalled = true; return Task.CompletedTask; }, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(21, someValue);
        Assert.False(noneCalled);
    }

    [Fact(DisplayName = "Optional<T>.MatchAsync of Some should invoke the some delegate (Func<T, ValueTask>, Func<ValueTask>).")]
    public async Task OptionalMatchAsyncOfSomeShouldInvokeSomeDelegateFuncTTaskFuncTaskWithValueTaskDelegates()
    {
        // Given
        Optional<int> optional = 21;
        int someValue = 0;
        bool noneCalled = false;

        // When
        await optional.MatchAsync(some: value => { someValue = value; return ValueTask.CompletedTask; }, none: () => { noneCalled = true; return ValueTask.CompletedTask; }, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(21, someValue);
        Assert.False(noneCalled);
    }

    [Fact(DisplayName = "Optional<T>.MatchAsync of None should invoke the none delegate (Func<T, Task>, Func<Task>).")]
    public async Task OptionalMatchAsyncOfNoneShouldInvokeNoneDelegateFuncTTaskFuncTask()
    {
        // Given
        Optional<int> optional = Optional<int>.None;
        int someValue = 0;
        bool noneCalled = false;

        // When
        await optional.MatchAsync(some: value => { someValue = value; return Task.CompletedTask; }, none: () => { noneCalled = true; return Task.CompletedTask; }, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(0, someValue);
        Assert.True(noneCalled);
    }

    [Fact(DisplayName = "Optional<T>.MatchAsync of None should invoke the none delegate (Func<T, ValueTask>, Func<ValueTask>).")]
    public async Task OptionalMatchAsyncOfNoneShouldInvokeNoneDelegateFuncTTaskFuncTaskWithValueTaskDelegates()
    {
        // Given
        Optional<int> optional = Optional<int>.None;
        int someValue = 0;
        bool noneCalled = false;

        // When
        await optional.MatchAsync(some: value => { someValue = value; return ValueTask.CompletedTask; }, none: () => { noneCalled = true; return ValueTask.CompletedTask; }, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(0, someValue);
        Assert.True(noneCalled);
    }

    [Fact(DisplayName = "Optional<T>.MatchAsync should do nothing when delegates are null (Func<T, Task>, Func<Task>).")]
    public async Task OptionalMatchAsyncShouldDoNothingWhenDelegatesAreNullFuncTTaskFuncTask()
    {
        // Given
        Optional<int> some = 21;
        Optional<int> none = Optional<int>.None;

        // When
        await some.MatchAsync(some: (Func<int, Task>?)null, none: (Func<Task>?)null, cancellationToken: TestContext.Current.CancellationToken);
        await none.MatchAsync(some: (Func<int, Task>?)null, none: (Func<Task>?)null, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(Optional<int>.IsSome(some));
        Assert.True(Optional<int>.IsNone(none));
    }

    [Fact(DisplayName = "Optional<T>.MatchAsync should do nothing when delegates are null (Func<T, ValueTask>, Func<ValueTask>).")]
    public async Task OptionalMatchAsyncShouldDoNothingWhenDelegatesAreNullFuncTTaskFuncTaskWithValueTaskDelegates()
    {
        // Given
        Optional<int> some = 21;
        Optional<int> none = Optional<int>.None;

        // When
        await some.MatchAsync(some: (Func<int, ValueTask>?)null, none: (Func<ValueTask>?)null, cancellationToken: TestContext.Current.CancellationToken);
        await none.MatchAsync(some: (Func<int, ValueTask>?)null, none: (Func<ValueTask>?)null, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(Optional<int>.IsSome(some));
        Assert.True(Optional<int>.IsNone(none));
    }

    [Fact(DisplayName = "Optional<T>.MatchAsync of Some should invoke the some delegate (Func<T, CancellationToken, Task>, Func<CancellationToken, Task>).")]
    public async Task OptionalMatchAsyncOfSomeShouldInvokeSomeDelegateFuncTCancellationTokenTaskFuncCancellationTokenTask()
    {
        // Given
        Optional<int> optional = 21;
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

    [Fact(DisplayName = "Optional<T>.MatchAsync of Some should invoke the some delegate (Func<T, CancellationToken, ValueTask>, Func<CancellationToken, ValueTask>).")]
    public async Task OptionalMatchAsyncOfSomeShouldInvokeSomeDelegateFuncTCancellationTokenTaskFuncCancellationTokenTaskWithValueTaskDelegates()
    {
        // Given
        Optional<int> optional = 21;
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

    [Fact(DisplayName = "Optional<T>.MatchAsync of None should invoke the none delegate (Func<T, CancellationToken, Task>, Func<CancellationToken, Task>).")]
    public async Task OptionalMatchAsyncOfNoneShouldInvokeNoneDelegateFuncTCancellationTokenTaskFuncCancellationTokenTask()
    {
        // Given
        Optional<int> optional = Optional<int>.None;
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

    [Fact(DisplayName = "Optional<T>.MatchAsync of None should invoke the none delegate (Func<T, CancellationToken, ValueTask>, Func<CancellationToken, ValueTask>).")]
    public async Task OptionalMatchAsyncOfNoneShouldInvokeNoneDelegateFuncTCancellationTokenTaskFuncCancellationTokenTaskWithValueTaskDelegates()
    {
        // Given
        Optional<int> optional = Optional<int>.None;
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

    [Fact(DisplayName = "Optional<T>.MatchAsync should do nothing when delegates are null (Func<T, CancellationToken, Task>, Func<CancellationToken, Task>).")]
    public async Task OptionalMatchAsyncShouldDoNothingWhenDelegatesAreNullFuncTCancellationTokenTaskFuncCancellationTokenTask()
    {
        // Given
        Optional<int> some = 21;
        Optional<int> none = Optional<int>.None;

        // When
        await some.MatchAsync(some: (Func<int, CancellationToken, Task>?)null, none: (Func<CancellationToken, Task>?)null, cancellationToken: TestContext.Current.CancellationToken);
        await none.MatchAsync(some: (Func<int, CancellationToken, Task>?)null, none: (Func<CancellationToken, Task>?)null, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(Optional<int>.IsSome(some));
        Assert.True(Optional<int>.IsNone(none));
    }

    [Fact(DisplayName = "Optional<T>.MatchAsync should do nothing when delegates are null (Func<T, CancellationToken, ValueTask>, Func<CancellationToken, ValueTask>).")]
    public async Task OptionalMatchAsyncShouldDoNothingWhenDelegatesAreNullFuncTCancellationTokenTaskFuncCancellationTokenTaskWithValueTaskDelegates()
    {
        // Given
        Optional<int> some = 21;
        Optional<int> none = Optional<int>.None;

        // When
        await some.MatchAsync(some: (Func<int, CancellationToken, ValueTask>?)null, none: (Func<CancellationToken, ValueTask>?)null, cancellationToken: TestContext.Current.CancellationToken);
        await none.MatchAsync(some: (Func<int, CancellationToken, ValueTask>?)null, none: (Func<CancellationToken, ValueTask>?)null, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.True(Optional<int>.IsSome(some));
        Assert.True(Optional<int>.IsNone(none));
    }

    [Fact(DisplayName = "Optional<T>.MatchAsync<TResult> of Some should return the result of the some delegate (Func<T, Task<TResult>>, Func<TResult>).")]
    public async Task OptionalMatchAsyncTResultOfSomeShouldReturnResultOfSomeDelegateFuncTTaskTResultFuncTResult()
    {
        // Given
        Optional<int> optional = 21;

        // When
        string actual = await optional.MatchAsync(some: value => Task.FromResult($"Some {value}"), none: () => "None", cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal("Some 21", actual);
    }

    [Fact(DisplayName = "Optional<T>.MatchAsync<TResult> of Some should return the result of the some delegate (Func<T, ValueTask<TResult>>, Func<TResult>).")]
    public async Task OptionalMatchAsyncTResultOfSomeShouldReturnResultOfSomeDelegateFuncTTaskTResultFuncTResultWithValueTaskDelegates()
    {
        // Given
        Optional<int> optional = 21;

        // When
        string actual = await optional.MatchAsync(some: value => ValueTask.FromResult($"Some {value}"), none: () => "None", cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal("Some 21", actual);
    }

    [Fact(DisplayName = "Optional<T>.MatchAsync<TResult> of None should return the result of the none delegate (Func<T, Task<TResult>>, Func<TResult>).")]
    public async Task OptionalMatchAsyncTResultOfNoneShouldReturnResultOfNoneDelegateFuncTTaskTResultFuncTResult()
    {
        // Given
        Optional<int> optional = Optional<int>.None;

        // When
        string actual = await optional.MatchAsync(some: value => Task.FromResult($"Some {value}"), none: () => "None", cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal("None", actual);
    }

    [Fact(DisplayName = "Optional<T>.MatchAsync<TResult> of None should return the result of the none delegate (Func<T, ValueTask<TResult>>, Func<TResult>).")]
    public async Task OptionalMatchAsyncTResultOfNoneShouldReturnResultOfNoneDelegateFuncTTaskTResultFuncTResultWithValueTaskDelegates()
    {
        // Given
        Optional<int> optional = Optional<int>.None;

        // When
        string actual = await optional.MatchAsync(some: value => ValueTask.FromResult($"Some {value}"), none: () => "None", cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal("None", actual);
    }

    [Fact(DisplayName = "Optional<T>.MatchAsync<TResult> of Some should return the result of the some delegate (Func<T, CancellationToken, Task<TResult>>, Func<TResult>).")]
    public async Task OptionalMatchAsyncTResultOfSomeShouldReturnResultOfSomeDelegateFuncTCancellationTokenTaskTResultFuncTResult()
    {
        // Given
        Optional<int> optional = 21;
        CancellationToken someToken = CancellationToken.None;

        // When
        string actual = await optional.MatchAsync(some: (value, ct) => { someToken = ct; return Task.FromResult($"Some {value}"); }, none: () => "None", cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal("Some 21", actual);
        Assert.Equal(TestContext.Current.CancellationToken, someToken);
    }

    [Fact(DisplayName = "Optional<T>.MatchAsync<TResult> of Some should return the result of the some delegate (Func<T, CancellationToken, ValueTask<TResult>>, Func<TResult>).")]
    public async Task OptionalMatchAsyncTResultOfSomeShouldReturnResultOfSomeDelegateFuncTCancellationTokenTaskTResultFuncTResultWithValueTaskDelegates()
    {
        // Given
        Optional<int> optional = 21;
        CancellationToken someToken = CancellationToken.None;

        // When
        string actual = await optional.MatchAsync(some: (value, ct) => { someToken = ct; return ValueTask.FromResult($"Some {value}"); }, none: () => "None", cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal("Some 21", actual);
        Assert.Equal(TestContext.Current.CancellationToken, someToken);
    }

    [Fact(DisplayName = "Optional<T>.MatchAsync<TResult> of None should return the result of the none delegate (Func<T, CancellationToken, Task<TResult>>, Func<TResult>).")]
    public async Task OptionalMatchAsyncTResultOfNoneShouldReturnResultOfNoneDelegateFuncTCancellationTokenTaskTResultFuncTResult()
    {
        // Given
        Optional<int> optional = Optional<int>.None;
        CancellationToken someToken = CancellationToken.None;

        // When
        string actual = await optional.MatchAsync(some: (value, ct) => { someToken = ct; return Task.FromResult($"Some {value}"); }, none: () => "None", cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal("None", actual);
    }

    [Fact(DisplayName = "Optional<T>.MatchAsync<TResult> of None should return the result of the none delegate (Func<T, CancellationToken, ValueTask<TResult>>, Func<TResult>).")]
    public async Task OptionalMatchAsyncTResultOfNoneShouldReturnResultOfNoneDelegateFuncTCancellationTokenTaskTResultFuncTResultWithValueTaskDelegates()
    {
        // Given
        Optional<int> optional = Optional<int>.None;
        CancellationToken someToken = CancellationToken.None;

        // When
        string actual = await optional.MatchAsync(some: (value, ct) => { someToken = ct; return ValueTask.FromResult($"Some {value}"); }, none: () => "None", cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal("None", actual);
    }

    [Fact(DisplayName = "Optional<T>.MatchAsync<TResult> of Some should return the result of the some delegate (Func<T, TResult>, Func<Task<TResult>>).")]
    public async Task OptionalMatchAsyncTResultOfSomeShouldReturnResultOfSomeDelegateFuncTTResultFuncTaskTResult()
    {
        // Given
        Optional<int> optional = 21;

        // When
        string actual = await optional.MatchAsync(some: value => $"Some {value}", none: () => Task.FromResult("None"), cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal("Some 21", actual);
    }

    [Fact(DisplayName = "Optional<T>.MatchAsync<TResult> of Some should return the result of the some delegate (Func<T, TResult>, Func<ValueTask<TResult>>).")]
    public async Task OptionalMatchAsyncTResultOfSomeShouldReturnResultOfSomeDelegateFuncTTResultFuncTaskTResultWithValueTaskDelegates()
    {
        // Given
        Optional<int> optional = 21;

        // When
        string actual = await optional.MatchAsync(some: value => $"Some {value}", none: () => ValueTask.FromResult("None"), cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal("Some 21", actual);
    }

    [Fact(DisplayName = "Optional<T>.MatchAsync<TResult> of None should return the result of the none delegate (Func<T, TResult>, Func<Task<TResult>>).")]
    public async Task OptionalMatchAsyncTResultOfNoneShouldReturnResultOfNoneDelegateFuncTTResultFuncTaskTResult()
    {
        // Given
        Optional<int> optional = Optional<int>.None;

        // When
        string actual = await optional.MatchAsync(some: value => $"Some {value}", none: () => Task.FromResult("None"), cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal("None", actual);
    }

    [Fact(DisplayName = "Optional<T>.MatchAsync<TResult> of None should return the result of the none delegate (Func<T, TResult>, Func<ValueTask<TResult>>).")]
    public async Task OptionalMatchAsyncTResultOfNoneShouldReturnResultOfNoneDelegateFuncTTResultFuncTaskTResultWithValueTaskDelegates()
    {
        // Given
        Optional<int> optional = Optional<int>.None;

        // When
        string actual = await optional.MatchAsync(some: value => $"Some {value}", none: () => ValueTask.FromResult("None"), cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal("None", actual);
    }

    [Fact(DisplayName = "Optional<T>.MatchAsync<TResult> of Some should return the result of the some delegate (Func<T, TResult>, Func<CancellationToken, Task<TResult>>).")]
    public async Task OptionalMatchAsyncTResultOfSomeShouldReturnResultOfSomeDelegateFuncTTResultFuncCancellationTokenTaskTResult()
    {
        // Given
        Optional<int> optional = 21;
        CancellationToken noneToken = CancellationToken.None;

        // When
        string actual = await optional.MatchAsync(some: value => $"Some {value}", none: ct => { noneToken = ct; return Task.FromResult("None"); }, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal("Some 21", actual);
    }

    [Fact(DisplayName = "Optional<T>.MatchAsync<TResult> of Some should return the result of the some delegate (Func<T, TResult>, Func<CancellationToken, ValueTask<TResult>>).")]
    public async Task OptionalMatchAsyncTResultOfSomeShouldReturnResultOfSomeDelegateFuncTTResultFuncCancellationTokenTaskTResultWithValueTaskDelegates()
    {
        // Given
        Optional<int> optional = 21;
        CancellationToken noneToken = CancellationToken.None;

        // When
        string actual = await optional.MatchAsync(some: value => $"Some {value}", none: ct => { noneToken = ct; return ValueTask.FromResult("None"); }, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal("Some 21", actual);
    }

    [Fact(DisplayName = "Optional<T>.MatchAsync<TResult> of None should return the result of the none delegate (Func<T, TResult>, Func<CancellationToken, Task<TResult>>).")]
    public async Task OptionalMatchAsyncTResultOfNoneShouldReturnResultOfNoneDelegateFuncTTResultFuncCancellationTokenTaskTResult()
    {
        // Given
        Optional<int> optional = Optional<int>.None;
        CancellationToken noneToken = CancellationToken.None;

        // When
        string actual = await optional.MatchAsync(some: value => $"Some {value}", none: ct => { noneToken = ct; return Task.FromResult("None"); }, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal("None", actual);
        Assert.Equal(TestContext.Current.CancellationToken, noneToken);
    }

    [Fact(DisplayName = "Optional<T>.MatchAsync<TResult> of None should return the result of the none delegate (Func<T, TResult>, Func<CancellationToken, ValueTask<TResult>>).")]
    public async Task OptionalMatchAsyncTResultOfNoneShouldReturnResultOfNoneDelegateFuncTTResultFuncCancellationTokenTaskTResultWithValueTaskDelegates()
    {
        // Given
        Optional<int> optional = Optional<int>.None;
        CancellationToken noneToken = CancellationToken.None;

        // When
        string actual = await optional.MatchAsync(some: value => $"Some {value}", none: ct => { noneToken = ct; return ValueTask.FromResult("None"); }, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal("None", actual);
        Assert.Equal(TestContext.Current.CancellationToken, noneToken);
    }

    [Fact(DisplayName = "Optional<T>.MatchAsync<TResult> of Some should return the result of the some delegate (Func<T, Task<TResult>>, Func<Task<TResult>>).")]
    public async Task OptionalMatchAsyncTResultOfSomeShouldReturnResultOfSomeDelegateFuncTTaskTResultFuncTaskTResult()
    {
        // Given
        Optional<int> optional = 21;

        // When
        string actual = await optional.MatchAsync(some: value => Task.FromResult($"Some {value}"), none: () => Task.FromResult("None"), cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal("Some 21", actual);
    }

    [Fact(DisplayName = "Optional<T>.MatchAsync<TResult> of Some should return the result of the some delegate (Func<T, ValueTask<TResult>>, Func<ValueTask<TResult>>).")]
    public async Task OptionalMatchAsyncTResultOfSomeShouldReturnResultOfSomeDelegateFuncTTaskTResultFuncTaskTResultWithValueTaskDelegates()
    {
        // Given
        Optional<int> optional = 21;

        // When
        string actual = await optional.MatchAsync(some: value => ValueTask.FromResult($"Some {value}"), none: () => ValueTask.FromResult("None"), cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal("Some 21", actual);
    }

    [Fact(DisplayName = "Optional<T>.MatchAsync<TResult> of None should return the result of the none delegate (Func<T, Task<TResult>>, Func<Task<TResult>>).")]
    public async Task OptionalMatchAsyncTResultOfNoneShouldReturnResultOfNoneDelegateFuncTTaskTResultFuncTaskTResult()
    {
        // Given
        Optional<int> optional = Optional<int>.None;

        // When
        string actual = await optional.MatchAsync(some: value => Task.FromResult($"Some {value}"), none: () => Task.FromResult("None"), cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal("None", actual);
    }

    [Fact(DisplayName = "Optional<T>.MatchAsync<TResult> of None should return the result of the none delegate (Func<T, ValueTask<TResult>>, Func<ValueTask<TResult>>).")]
    public async Task OptionalMatchAsyncTResultOfNoneShouldReturnResultOfNoneDelegateFuncTTaskTResultFuncTaskTResultWithValueTaskDelegates()
    {
        // Given
        Optional<int> optional = Optional<int>.None;

        // When
        string actual = await optional.MatchAsync(some: value => ValueTask.FromResult($"Some {value}"), none: () => ValueTask.FromResult("None"), cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal("None", actual);
    }

    [Fact(DisplayName = "Optional<T>.MatchAsync<TResult> of Some should return the result of the some delegate (Func<T, CancellationToken, Task<TResult>>, Func<CancellationToken, Task<TResult>>).")]
    public async Task OptionalMatchAsyncTResultOfSomeShouldReturnResultOfSomeDelegateFuncTCancellationTokenTaskTResultFuncCancellationTokenTaskTResult()
    {
        // Given
        Optional<int> optional = 21;
        CancellationToken someToken = CancellationToken.None;
        CancellationToken noneToken = CancellationToken.None;

        // When
        string actual = await optional.MatchAsync(some: (value, ct) => { someToken = ct; return Task.FromResult($"Some {value}"); }, none: ct => { noneToken = ct; return Task.FromResult("None"); }, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal("Some 21", actual);
        Assert.Equal(TestContext.Current.CancellationToken, someToken);
    }

    [Fact(DisplayName = "Optional<T>.MatchAsync<TResult> of Some should return the result of the some delegate (Func<T, CancellationToken, ValueTask<TResult>>, Func<CancellationToken, ValueTask<TResult>>).")]
    public async Task OptionalMatchAsyncTResultOfSomeShouldReturnResultOfSomeDelegateFuncTCancellationTokenTaskTResultFuncCancellationTokenTaskTResultWithValueTaskDelegates()
    {
        // Given
        Optional<int> optional = 21;
        CancellationToken someToken = CancellationToken.None;
        CancellationToken noneToken = CancellationToken.None;

        // When
        string actual = await optional.MatchAsync(some: (value, ct) => { someToken = ct; return ValueTask.FromResult($"Some {value}"); }, none: ct => { noneToken = ct; return ValueTask.FromResult("None"); }, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal("Some 21", actual);
        Assert.Equal(TestContext.Current.CancellationToken, someToken);
    }

    [Fact(DisplayName = "Optional<T>.MatchAsync<TResult> of None should return the result of the none delegate (Func<T, CancellationToken, Task<TResult>>, Func<CancellationToken, Task<TResult>>).")]
    public async Task OptionalMatchAsyncTResultOfNoneShouldReturnResultOfNoneDelegateFuncTCancellationTokenTaskTResultFuncCancellationTokenTaskTResult()
    {
        // Given
        Optional<int> optional = Optional<int>.None;
        CancellationToken someToken = CancellationToken.None;
        CancellationToken noneToken = CancellationToken.None;

        // When
        string actual = await optional.MatchAsync(some: (value, ct) => { someToken = ct; return Task.FromResult($"Some {value}"); }, none: ct => { noneToken = ct; return Task.FromResult("None"); }, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal("None", actual);
        Assert.Equal(TestContext.Current.CancellationToken, noneToken);
    }

    [Fact(DisplayName = "Optional<T>.MatchAsync<TResult> of None should return the result of the none delegate (Func<T, CancellationToken, ValueTask<TResult>>, Func<CancellationToken, ValueTask<TResult>>).")]
    public async Task OptionalMatchAsyncTResultOfNoneShouldReturnResultOfNoneDelegateFuncTCancellationTokenTaskTResultFuncCancellationTokenTaskTResultWithValueTaskDelegates()
    {
        // Given
        Optional<int> optional = Optional<int>.None;
        CancellationToken someToken = CancellationToken.None;
        CancellationToken noneToken = CancellationToken.None;

        // When
        string actual = await optional.MatchAsync(some: (value, ct) => { someToken = ct; return ValueTask.FromResult($"Some {value}"); }, none: ct => { noneToken = ct; return ValueTask.FromResult("None"); }, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal("None", actual);
        Assert.Equal(TestContext.Current.CancellationToken, noneToken);
    }

    [Fact(DisplayName = "Optional<T>.SelectAsync of Some should produce the expected result (Func<T, Task<TResult>>).")]
    public async Task OptionalSelectAsyncOfSomeShouldProduceExpectedResultFuncTTaskTResult()
    {
        // Given
        Optional<int> optional = 21;

        // When
        Optional<int> actual = await optional.SelectAsync(value => Task.FromResult(value * 2), TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(Optional<int>.Some(42), actual);
    }

    [Fact(DisplayName = "Optional<T>.SelectAsync of Some should produce the expected result (Func<T, ValueTask<TResult>>).")]
    public async Task OptionalSelectAsyncOfSomeShouldProduceExpectedResultFuncTTaskTResultWithValueTaskDelegates()
    {
        // Given
        Optional<int> optional = 21;

        // When
        Optional<int> actual = await optional.SelectAsync(value => ValueTask.FromResult(value * 2), TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(Optional<int>.Some(42), actual);
    }

    [Fact(DisplayName = "Optional<T>.SelectAsync should produce the same result as Select when the selector returns the default value.")]
    public async Task OptionalSelectAsyncShouldProduceSameResultAsSelectWhenSelectorReturnsDefaultValue()
    {
        // Given
        Optional<int> optional = 21;
        Optional<int> expected = optional.Select(_ => 0);

        // When
        Optional<int> actual = await optional.SelectAsync(_ => Task.FromResult(0), TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(Optional<int>.Some(0), expected);
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Optional<T>.SelectAsync should produce the same result as Select when the selector returns the default value with ValueTask delegates.")]
    public async Task OptionalSelectAsyncShouldProduceSameResultAsSelectWhenSelectorReturnsDefaultValueWithValueTaskDelegates()
    {
        // Given
        Optional<int> optional = 21;
        Optional<int> expected = optional.Select(_ => 0);

        // When
        Optional<int> actual = await optional.SelectAsync(_ => ValueTask.FromResult(0), TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(Optional<int>.Some(0), expected);
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Optional<T>.SelectAsync of None should produce the expected result (Func<T, Task<TResult>>).")]
    public async Task OptionalSelectAsyncOfNoneShouldProduceExpectedResultFuncTTaskTResult()
    {
        // Given
        Optional<int> optional = Optional<int>.None;

        // When
        Optional<int> actual = await optional.SelectAsync(value => Task.FromResult(value * 2), TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(Optional<int>.None, actual);
    }

    [Fact(DisplayName = "Optional<T>.SelectAsync of None should produce the expected result (Func<T, ValueTask<TResult>>).")]
    public async Task OptionalSelectAsyncOfNoneShouldProduceExpectedResultFuncTTaskTResultWithValueTaskDelegates()
    {
        // Given
        Optional<int> optional = Optional<int>.None;

        // When
        Optional<int> actual = await optional.SelectAsync(value => ValueTask.FromResult(value * 2), TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(Optional<int>.None, actual);
    }

    [Fact(DisplayName = "Optional<T>.SelectAsync of Some should produce the expected result (Func<T, CancellationToken, Task<TResult>>).")]
    public async Task OptionalSelectAsyncOfSomeShouldProduceExpectedResultFuncTCancellationTokenTaskTResult()
    {
        // Given
        Optional<int> optional = 21;
        CancellationToken selectorToken = CancellationToken.None;

        // When
        Optional<int> actual = await optional.SelectAsync((value, ct) => { selectorToken = ct; return Task.FromResult(value * 2); }, TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(Optional<int>.Some(42), actual);
        Assert.Equal(TestContext.Current.CancellationToken, selectorToken);
    }

    [Fact(DisplayName = "Optional<T>.SelectAsync of Some should produce the expected result (Func<T, CancellationToken, ValueTask<TResult>>).")]
    public async Task OptionalSelectAsyncOfSomeShouldProduceExpectedResultFuncTCancellationTokenTaskTResultWithValueTaskDelegates()
    {
        // Given
        Optional<int> optional = 21;
        CancellationToken selectorToken = CancellationToken.None;

        // When
        Optional<int> actual = await optional.SelectAsync((value, ct) => { selectorToken = ct; return ValueTask.FromResult(value * 2); }, TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(Optional<int>.Some(42), actual);
        Assert.Equal(TestContext.Current.CancellationToken, selectorToken);
    }

    [Fact(DisplayName = "Optional<T>.SelectAsync of None should produce the expected result (Func<T, CancellationToken, Task<TResult>>).")]
    public async Task OptionalSelectAsyncOfNoneShouldProduceExpectedResultFuncTCancellationTokenTaskTResult()
    {
        // Given
        Optional<int> optional = Optional<int>.None;
        CancellationToken selectorToken = CancellationToken.None;

        // When
        Optional<int> actual = await optional.SelectAsync((value, ct) => { selectorToken = ct; return Task.FromResult(value * 2); }, TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(Optional<int>.None, actual);
    }

    [Fact(DisplayName = "Optional<T>.SelectAsync of None should produce the expected result (Func<T, CancellationToken, ValueTask<TResult>>).")]
    public async Task OptionalSelectAsyncOfNoneShouldProduceExpectedResultFuncTCancellationTokenTaskTResultWithValueTaskDelegates()
    {
        // Given
        Optional<int> optional = Optional<int>.None;
        CancellationToken selectorToken = CancellationToken.None;

        // When
        Optional<int> actual = await optional.SelectAsync((value, ct) => { selectorToken = ct; return ValueTask.FromResult(value * 2); }, TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(Optional<int>.None, actual);
    }

    [Fact(DisplayName = "Optional<T>.SelectManyAsync of Some should produce the expected result (Func<T, Task<Optional<TResult>>>).")]
    public async Task OptionalSelectManyAsyncOfSomeShouldProduceExpectedResultFuncTTaskOptionalTResult()
    {
        // Given
        Optional<int> optional = 21;

        // When
        Optional<int> actual = await optional.SelectManyAsync(value => Task.FromResult<Optional<int>>(value * 2), TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(Optional<int>.Some(42), actual);
    }

    [Fact(DisplayName = "Optional<T>.SelectManyAsync of Some should produce the expected result (Func<T, ValueTask<Optional<TResult>>>).")]
    public async Task OptionalSelectManyAsyncOfSomeShouldProduceExpectedResultFuncTTaskOptionalTResultWithValueTaskDelegates()
    {
        // Given
        Optional<int> optional = 21;

        // When
        Optional<int> actual = await optional.SelectManyAsync(value => ValueTask.FromResult<Optional<int>>(value * 2), TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(Optional<int>.Some(42), actual);
    }

    [Fact(DisplayName = "Optional<T>.SelectManyAsync of None should produce the expected result (Func<T, Task<Optional<TResult>>>).")]
    public async Task OptionalSelectManyAsyncOfNoneShouldProduceExpectedResultFuncTTaskOptionalTResult()
    {
        // Given
        Optional<int> optional = Optional<int>.None;

        // When
        Optional<int> actual = await optional.SelectManyAsync(value => Task.FromResult<Optional<int>>(value * 2), TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(Optional<int>.None, actual);
    }

    [Fact(DisplayName = "Optional<T>.SelectManyAsync of None should produce the expected result (Func<T, ValueTask<Optional<TResult>>>).")]
    public async Task OptionalSelectManyAsyncOfNoneShouldProduceExpectedResultFuncTTaskOptionalTResultWithValueTaskDelegates()
    {
        // Given
        Optional<int> optional = Optional<int>.None;

        // When
        Optional<int> actual = await optional.SelectManyAsync(value => ValueTask.FromResult<Optional<int>>(value * 2), TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(Optional<int>.None, actual);
    }

    [Fact(DisplayName = "Optional<T>.SelectManyAsync of Some should produce the expected result (Func<T, CancellationToken, Task<Optional<TResult>>>).")]
    public async Task OptionalSelectManyAsyncOfSomeShouldProduceExpectedResultFuncTCancellationTokenTaskOptionalTResult()
    {
        // Given
        Optional<int> optional = 21;
        CancellationToken selectorToken = CancellationToken.None;

        // When
        Optional<int> actual = await optional.SelectManyAsync((value, ct) => { selectorToken = ct; return Task.FromResult<Optional<int>>(value * 2); }, TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(Optional<int>.Some(42), actual);
        Assert.Equal(TestContext.Current.CancellationToken, selectorToken);
    }

    [Fact(DisplayName = "Optional<T>.SelectManyAsync of Some should produce the expected result (Func<T, CancellationToken, ValueTask<Optional<TResult>>>).")]
    public async Task OptionalSelectManyAsyncOfSomeShouldProduceExpectedResultFuncTCancellationTokenTaskOptionalTResultWithValueTaskDelegates()
    {
        // Given
        Optional<int> optional = 21;
        CancellationToken selectorToken = CancellationToken.None;

        // When
        Optional<int> actual = await optional.SelectManyAsync((value, ct) => { selectorToken = ct; return ValueTask.FromResult<Optional<int>>(value * 2); }, TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(Optional<int>.Some(42), actual);
        Assert.Equal(TestContext.Current.CancellationToken, selectorToken);
    }

    [Fact(DisplayName = "Optional<T>.SelectManyAsync of None should produce the expected result (Func<T, CancellationToken, Task<Optional<TResult>>>).")]
    public async Task OptionalSelectManyAsyncOfNoneShouldProduceExpectedResultFuncTCancellationTokenTaskOptionalTResult()
    {
        // Given
        Optional<int> optional = Optional<int>.None;
        CancellationToken selectorToken = CancellationToken.None;

        // When
        Optional<int> actual = await optional.SelectManyAsync((value, ct) => { selectorToken = ct; return Task.FromResult<Optional<int>>(value * 2); }, TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(Optional<int>.None, actual);
    }

    [Fact(DisplayName = "Optional<T>.SelectManyAsync of None should produce the expected result (Func<T, CancellationToken, ValueTask<Optional<TResult>>>).")]
    public async Task OptionalSelectManyAsyncOfNoneShouldProduceExpectedResultFuncTCancellationTokenTaskOptionalTResultWithValueTaskDelegates()
    {
        // Given
        Optional<int> optional = Optional<int>.None;
        CancellationToken selectorToken = CancellationToken.None;

        // When
        Optional<int> actual = await optional.SelectManyAsync((value, ct) => { selectorToken = ct; return ValueTask.FromResult<Optional<int>>(value * 2); }, TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(Optional<int>.None, actual);
    }
}
