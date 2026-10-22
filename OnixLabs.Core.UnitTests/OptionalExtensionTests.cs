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

public sealed class OptionalExtensionTests
{
    [Fact(DisplayName = "Optional<T>.MatchAsync of Some should invoke the some delegate (Func<T, Task>, Action).")]
    public async Task OptionalMatchAsyncOfSomeShouldInvokeSomeDelegateFuncTTaskAction()
    {
        // Given
        Optional<int> optional = 21;
        int someValue = 0;
        bool noneCalled = false;

        // When
        await optional.MatchAsync(some: value => { someValue = value; return Task.CompletedTask; }, none: () => noneCalled = true, token: TestContext.Current.CancellationToken);

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
        await optional.MatchAsync(some: value => { someValue = value; return Task.CompletedTask; }, none: () => noneCalled = true, token: TestContext.Current.CancellationToken);

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
        await some.MatchAsync(some: (Func<int, Task>?)null, none: (Action?)null, token: TestContext.Current.CancellationToken);
        await none.MatchAsync(some: (Func<int, Task>?)null, none: (Action?)null, token: TestContext.Current.CancellationToken);

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
        await optional.MatchAsync(some: (value, ct) => { someValue = value; someToken = ct; return Task.CompletedTask; }, none: () => noneCalled = true, token: TestContext.Current.CancellationToken);

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
        await optional.MatchAsync(some: (value, ct) => { someValue = value; someToken = ct; return Task.CompletedTask; }, none: () => noneCalled = true, token: TestContext.Current.CancellationToken);

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
        await some.MatchAsync(some: (Func<int, CancellationToken, Task>?)null, none: (Action?)null, token: TestContext.Current.CancellationToken);
        await none.MatchAsync(some: (Func<int, CancellationToken, Task>?)null, none: (Action?)null, token: TestContext.Current.CancellationToken);

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
        await optional.MatchAsync(some: value => someValue = value, none: () => { noneCalled = true; return Task.CompletedTask; }, token: TestContext.Current.CancellationToken);

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
        await optional.MatchAsync(some: value => someValue = value, none: () => { noneCalled = true; return Task.CompletedTask; }, token: TestContext.Current.CancellationToken);

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
        await some.MatchAsync(some: (Action<int>?)null, none: (Func<Task>?)null, token: TestContext.Current.CancellationToken);
        await none.MatchAsync(some: (Action<int>?)null, none: (Func<Task>?)null, token: TestContext.Current.CancellationToken);

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
        await optional.MatchAsync(some: value => someValue = value, none: ct => { noneCalled = true; noneToken = ct; return Task.CompletedTask; }, token: TestContext.Current.CancellationToken);

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
        await optional.MatchAsync(some: value => someValue = value, none: ct => { noneCalled = true; noneToken = ct; return Task.CompletedTask; }, token: TestContext.Current.CancellationToken);

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
        await some.MatchAsync(some: (Action<int>?)null, none: (Func<CancellationToken, Task>?)null, token: TestContext.Current.CancellationToken);
        await none.MatchAsync(some: (Action<int>?)null, none: (Func<CancellationToken, Task>?)null, token: TestContext.Current.CancellationToken);

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
        await optional.MatchAsync(some: value => { someValue = value; return Task.CompletedTask; }, none: () => { noneCalled = true; return Task.CompletedTask; }, token: TestContext.Current.CancellationToken);

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
        await optional.MatchAsync(some: value => { someValue = value; return Task.CompletedTask; }, none: () => { noneCalled = true; return Task.CompletedTask; }, token: TestContext.Current.CancellationToken);

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
        await some.MatchAsync(some: (Func<int, Task>?)null, none: (Func<Task>?)null, token: TestContext.Current.CancellationToken);
        await none.MatchAsync(some: (Func<int, Task>?)null, none: (Func<Task>?)null, token: TestContext.Current.CancellationToken);

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
        await optional.MatchAsync(some: (value, ct) => { someValue = value; someToken = ct; return Task.CompletedTask; }, none: ct => { noneCalled = true; noneToken = ct; return Task.CompletedTask; }, token: TestContext.Current.CancellationToken);

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
        await optional.MatchAsync(some: (value, ct) => { someValue = value; someToken = ct; return Task.CompletedTask; }, none: ct => { noneCalled = true; noneToken = ct; return Task.CompletedTask; }, token: TestContext.Current.CancellationToken);

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
        await some.MatchAsync(some: (Func<int, CancellationToken, Task>?)null, none: (Func<CancellationToken, Task>?)null, token: TestContext.Current.CancellationToken);
        await none.MatchAsync(some: (Func<int, CancellationToken, Task>?)null, none: (Func<CancellationToken, Task>?)null, token: TestContext.Current.CancellationToken);

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
        string actual = await optional.MatchAsync(some: value => Task.FromResult($"Some {value}"), none: () => "None", token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal("Some 21", actual);
    }

    [Fact(DisplayName = "Optional<T>.MatchAsync<TResult> of None should return the result of the none delegate (Func<T, Task<TResult>>, Func<TResult>).")]
    public async Task OptionalMatchAsyncTResultOfNoneShouldReturnResultOfNoneDelegateFuncTTaskTResultFuncTResult()
    {
        // Given
        Optional<int> optional = Optional<int>.None;

        // When
        string actual = await optional.MatchAsync(some: value => Task.FromResult($"Some {value}"), none: () => "None", token: TestContext.Current.CancellationToken);

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
        string actual = await optional.MatchAsync(some: (value, ct) => { someToken = ct; return Task.FromResult($"Some {value}"); }, none: () => "None", token: TestContext.Current.CancellationToken);

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
        string actual = await optional.MatchAsync(some: (value, ct) => { someToken = ct; return Task.FromResult($"Some {value}"); }, none: () => "None", token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal("None", actual);
    }

    [Fact(DisplayName = "Optional<T>.MatchAsync<TResult> of Some should return the result of the some delegate (Func<T, TResult>, Func<Task<TResult>>).")]
    public async Task OptionalMatchAsyncTResultOfSomeShouldReturnResultOfSomeDelegateFuncTTResultFuncTaskTResult()
    {
        // Given
        Optional<int> optional = 21;

        // When
        string actual = await optional.MatchAsync(some: value => $"Some {value}", none: () => Task.FromResult("None"), token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal("Some 21", actual);
    }

    [Fact(DisplayName = "Optional<T>.MatchAsync<TResult> of None should return the result of the none delegate (Func<T, TResult>, Func<Task<TResult>>).")]
    public async Task OptionalMatchAsyncTResultOfNoneShouldReturnResultOfNoneDelegateFuncTTResultFuncTaskTResult()
    {
        // Given
        Optional<int> optional = Optional<int>.None;

        // When
        string actual = await optional.MatchAsync(some: value => $"Some {value}", none: () => Task.FromResult("None"), token: TestContext.Current.CancellationToken);

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
        string actual = await optional.MatchAsync(some: value => $"Some {value}", none: ct => { noneToken = ct; return Task.FromResult("None"); }, token: TestContext.Current.CancellationToken);

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
        string actual = await optional.MatchAsync(some: value => $"Some {value}", none: ct => { noneToken = ct; return Task.FromResult("None"); }, token: TestContext.Current.CancellationToken);

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
        string actual = await optional.MatchAsync(some: value => Task.FromResult($"Some {value}"), none: () => Task.FromResult("None"), token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal("Some 21", actual);
    }

    [Fact(DisplayName = "Optional<T>.MatchAsync<TResult> of None should return the result of the none delegate (Func<T, Task<TResult>>, Func<Task<TResult>>).")]
    public async Task OptionalMatchAsyncTResultOfNoneShouldReturnResultOfNoneDelegateFuncTTaskTResultFuncTaskTResult()
    {
        // Given
        Optional<int> optional = Optional<int>.None;

        // When
        string actual = await optional.MatchAsync(some: value => Task.FromResult($"Some {value}"), none: () => Task.FromResult("None"), token: TestContext.Current.CancellationToken);

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
        string actual = await optional.MatchAsync(some: (value, ct) => { someToken = ct; return Task.FromResult($"Some {value}"); }, none: ct => { noneToken = ct; return Task.FromResult("None"); }, token: TestContext.Current.CancellationToken);

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
        string actual = await optional.MatchAsync(some: (value, ct) => { someToken = ct; return Task.FromResult($"Some {value}"); }, none: ct => { noneToken = ct; return Task.FromResult("None"); }, token: TestContext.Current.CancellationToken);

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

    [Fact(DisplayName = "Task<Optional<T>>.GetValueOrDefaultAsync of Some should produce the expected result.")]
    public async Task TaskOptionalGetValueOrDefaultAsyncOfSomeShouldProduceExpectedResult()
    {
        // Given
        Optional<int> optional = 21;

        // When
        int actual = await Task.FromResult(optional).GetValueOrDefaultAsync(TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(21, actual);
    }

    [Fact(DisplayName = "Task<Optional<T>>.GetValueOrDefaultAsync with default value of Some should produce the expected result.")]
    public async Task TaskOptionalGetValueOrDefaultAsyncWithDefaultValueOfSomeShouldProduceExpectedResult()
    {
        // Given
        Optional<int> optional = 21;

        // When
        int actual = await Task.FromResult(optional).GetValueOrDefaultAsync(99, TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(21, actual);
    }

    [Fact(DisplayName = "Task<Optional<T>>.GetValueOrDefaultAsync of None should produce the expected result.")]
    public async Task TaskOptionalGetValueOrDefaultAsyncOfNoneShouldProduceExpectedResult()
    {
        // Given
        Optional<int> optional = Optional<int>.None;

        // When
        int actual = await Task.FromResult(optional).GetValueOrDefaultAsync(TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(0, actual);
    }

    [Fact(DisplayName = "Task<Optional<T>>.GetValueOrDefaultAsync with default value of None should produce the expected result.")]
    public async Task TaskOptionalGetValueOrDefaultAsyncWithDefaultValueOfNoneShouldProduceExpectedResult()
    {
        // Given
        Optional<int> optional = Optional<int>.None;

        // When
        int actual = await Task.FromResult(optional).GetValueOrDefaultAsync(99, TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(99, actual);
    }

    [Fact(DisplayName = "Task<Optional<T>>.GetValueOrThrowAsync of Some should produce the expected result.")]
    public async Task TaskOptionalGetValueOrThrowAsyncOfSomeShouldProduceExpectedResult()
    {
        // Given
        Optional<int> optional = 21;

        // When
        int actual = await Task.FromResult(optional).GetValueOrThrowAsync(TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(21, actual);
    }

    [Fact(DisplayName = "Task<Optional<T>>.GetValueOrThrowAsync of None should throw InvalidOperationException.")]
    public async Task TaskOptionalGetValueOrThrowAsyncOfNoneShouldThrowInvalidOperationException()
    {
        // Given
        Optional<int> optional = Optional<int>.None;

        // When
        Exception exception = await Assert.ThrowsAsync<InvalidOperationException>(async () => await Task.FromResult(optional).GetValueOrThrowAsync(TestContext.Current.CancellationToken));

        // Then
        Assert.Equal("Optional value of type System.Int32 is not present.", exception.Message);
    }

    [Fact(DisplayName = "Task<Optional<T>>.MatchAsync of Some should invoke the some delegate (Action<T>, Action).")]
    public async Task TaskOptionalMatchAsyncOfSomeShouldInvokeSomeDelegateActionTAction()
    {
        // Given
        Optional<int> optional = 21;
        int someValue = 0;
        bool noneCalled = false;

        // When
        await Task.FromResult(optional).MatchAsync(some: value => someValue = value, none: () => noneCalled = true, token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(21, someValue);
        Assert.False(noneCalled);
    }

    [Fact(DisplayName = "Task<Optional<T>>.MatchAsync of None should invoke the none delegate (Action<T>, Action).")]
    public async Task TaskOptionalMatchAsyncOfNoneShouldInvokeNoneDelegateActionTAction()
    {
        // Given
        Optional<int> optional = Optional<int>.None;
        int someValue = 0;
        bool noneCalled = false;

        // When
        await Task.FromResult(optional).MatchAsync(some: value => someValue = value, none: () => noneCalled = true, token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(0, someValue);
        Assert.True(noneCalled);
    }

    [Fact(DisplayName = "Task<Optional<T>>.MatchAsync of Some should invoke the some delegate (Func<T, Task>, Action).")]
    public async Task TaskOptionalMatchAsyncOfSomeShouldInvokeSomeDelegateFuncTTaskAction()
    {
        // Given
        Optional<int> optional = 21;
        int someValue = 0;
        bool noneCalled = false;

        // When
        await Task.FromResult(optional).MatchAsync(some: value => { someValue = value; return Task.CompletedTask; }, none: () => noneCalled = true, token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(21, someValue);
        Assert.False(noneCalled);
    }

    [Fact(DisplayName = "Task<Optional<T>>.MatchAsync of None should invoke the none delegate (Func<T, Task>, Action).")]
    public async Task TaskOptionalMatchAsyncOfNoneShouldInvokeNoneDelegateFuncTTaskAction()
    {
        // Given
        Optional<int> optional = Optional<int>.None;
        int someValue = 0;
        bool noneCalled = false;

        // When
        await Task.FromResult(optional).MatchAsync(some: value => { someValue = value; return Task.CompletedTask; }, none: () => noneCalled = true, token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(0, someValue);
        Assert.True(noneCalled);
    }

    [Fact(DisplayName = "Task<Optional<T>>.MatchAsync of Some should invoke the some delegate (Func<T, CancellationToken, Task>, Action).")]
    public async Task TaskOptionalMatchAsyncOfSomeShouldInvokeSomeDelegateFuncTCancellationTokenTaskAction()
    {
        // Given
        Optional<int> optional = 21;
        int someValue = 0;
        bool noneCalled = false;
        CancellationToken someToken = CancellationToken.None;

        // When
        await Task.FromResult(optional).MatchAsync(some: (value, ct) => { someValue = value; someToken = ct; return Task.CompletedTask; }, none: () => noneCalled = true, token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(21, someValue);
        Assert.False(noneCalled);
        Assert.Equal(TestContext.Current.CancellationToken, someToken);
    }

    [Fact(DisplayName = "Task<Optional<T>>.MatchAsync of None should invoke the none delegate (Func<T, CancellationToken, Task>, Action).")]
    public async Task TaskOptionalMatchAsyncOfNoneShouldInvokeNoneDelegateFuncTCancellationTokenTaskAction()
    {
        // Given
        Optional<int> optional = Optional<int>.None;
        int someValue = 0;
        bool noneCalled = false;
        CancellationToken someToken = CancellationToken.None;

        // When
        await Task.FromResult(optional).MatchAsync(some: (value, ct) => { someValue = value; someToken = ct; return Task.CompletedTask; }, none: () => noneCalled = true, token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(0, someValue);
        Assert.True(noneCalled);
    }

    [Fact(DisplayName = "Task<Optional<T>>.MatchAsync of Some should invoke the some delegate (Action<T>, Func<Task>).")]
    public async Task TaskOptionalMatchAsyncOfSomeShouldInvokeSomeDelegateActionTFuncTask()
    {
        // Given
        Optional<int> optional = 21;
        int someValue = 0;
        bool noneCalled = false;

        // When
        await Task.FromResult(optional).MatchAsync(some: value => someValue = value, none: () => { noneCalled = true; return Task.CompletedTask; }, token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(21, someValue);
        Assert.False(noneCalled);
    }

    [Fact(DisplayName = "Task<Optional<T>>.MatchAsync of None should invoke the none delegate (Action<T>, Func<Task>).")]
    public async Task TaskOptionalMatchAsyncOfNoneShouldInvokeNoneDelegateActionTFuncTask()
    {
        // Given
        Optional<int> optional = Optional<int>.None;
        int someValue = 0;
        bool noneCalled = false;

        // When
        await Task.FromResult(optional).MatchAsync(some: value => someValue = value, none: () => { noneCalled = true; return Task.CompletedTask; }, token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(0, someValue);
        Assert.True(noneCalled);
    }

    [Fact(DisplayName = "Task<Optional<T>>.MatchAsync of Some should invoke the some delegate (Action<T>, Func<CancellationToken, Task>).")]
    public async Task TaskOptionalMatchAsyncOfSomeShouldInvokeSomeDelegateActionTFuncCancellationTokenTask()
    {
        // Given
        Optional<int> optional = 21;
        int someValue = 0;
        bool noneCalled = false;
        CancellationToken noneToken = CancellationToken.None;

        // When
        await Task.FromResult(optional).MatchAsync(some: value => someValue = value, none: ct => { noneCalled = true; noneToken = ct; return Task.CompletedTask; }, token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(21, someValue);
        Assert.False(noneCalled);
    }

    [Fact(DisplayName = "Task<Optional<T>>.MatchAsync of None should invoke the none delegate (Action<T>, Func<CancellationToken, Task>).")]
    public async Task TaskOptionalMatchAsyncOfNoneShouldInvokeNoneDelegateActionTFuncCancellationTokenTask()
    {
        // Given
        Optional<int> optional = Optional<int>.None;
        int someValue = 0;
        bool noneCalled = false;
        CancellationToken noneToken = CancellationToken.None;

        // When
        await Task.FromResult(optional).MatchAsync(some: value => someValue = value, none: ct => { noneCalled = true; noneToken = ct; return Task.CompletedTask; }, token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(0, someValue);
        Assert.True(noneCalled);
        Assert.Equal(TestContext.Current.CancellationToken, noneToken);
    }

    [Fact(DisplayName = "Task<Optional<T>>.MatchAsync of Some should invoke the some delegate (Func<T, Task>, Func<Task>).")]
    public async Task TaskOptionalMatchAsyncOfSomeShouldInvokeSomeDelegateFuncTTaskFuncTask()
    {
        // Given
        Optional<int> optional = 21;
        int someValue = 0;
        bool noneCalled = false;

        // When
        await Task.FromResult(optional).MatchAsync(some: value => { someValue = value; return Task.CompletedTask; }, none: () => { noneCalled = true; return Task.CompletedTask; }, token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(21, someValue);
        Assert.False(noneCalled);
    }

    [Fact(DisplayName = "Task<Optional<T>>.MatchAsync of None should invoke the none delegate (Func<T, Task>, Func<Task>).")]
    public async Task TaskOptionalMatchAsyncOfNoneShouldInvokeNoneDelegateFuncTTaskFuncTask()
    {
        // Given
        Optional<int> optional = Optional<int>.None;
        int someValue = 0;
        bool noneCalled = false;

        // When
        await Task.FromResult(optional).MatchAsync(some: value => { someValue = value; return Task.CompletedTask; }, none: () => { noneCalled = true; return Task.CompletedTask; }, token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(0, someValue);
        Assert.True(noneCalled);
    }

    [Fact(DisplayName = "Task<Optional<T>>.MatchAsync of Some should invoke the some delegate (Func<T, CancellationToken, Task>, Func<CancellationToken, Task>).")]
    public async Task TaskOptionalMatchAsyncOfSomeShouldInvokeSomeDelegateFuncTCancellationTokenTaskFuncCancellationTokenTask()
    {
        // Given
        Optional<int> optional = 21;
        int someValue = 0;
        bool noneCalled = false;
        CancellationToken someToken = CancellationToken.None;
        CancellationToken noneToken = CancellationToken.None;

        // When
        await Task.FromResult(optional).MatchAsync(some: (value, ct) => { someValue = value; someToken = ct; return Task.CompletedTask; }, none: ct => { noneCalled = true; noneToken = ct; return Task.CompletedTask; }, token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(21, someValue);
        Assert.False(noneCalled);
        Assert.Equal(TestContext.Current.CancellationToken, someToken);
    }

    [Fact(DisplayName = "Task<Optional<T>>.MatchAsync of None should invoke the none delegate (Func<T, CancellationToken, Task>, Func<CancellationToken, Task>).")]
    public async Task TaskOptionalMatchAsyncOfNoneShouldInvokeNoneDelegateFuncTCancellationTokenTaskFuncCancellationTokenTask()
    {
        // Given
        Optional<int> optional = Optional<int>.None;
        int someValue = 0;
        bool noneCalled = false;
        CancellationToken someToken = CancellationToken.None;
        CancellationToken noneToken = CancellationToken.None;

        // When
        await Task.FromResult(optional).MatchAsync(some: (value, ct) => { someValue = value; someToken = ct; return Task.CompletedTask; }, none: ct => { noneCalled = true; noneToken = ct; return Task.CompletedTask; }, token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(0, someValue);
        Assert.True(noneCalled);
        Assert.Equal(TestContext.Current.CancellationToken, noneToken);
    }

    [Fact(DisplayName = "Task<Optional<T>>.MatchAsync<TResult> of Some should return the result of the some delegate (Func<T, TResult>, Func<TResult>).")]
    public async Task TaskOptionalMatchAsyncTResultOfSomeShouldReturnResultOfSomeDelegateFuncTTResultFuncTResult()
    {
        // Given
        Optional<int> optional = 21;

        // When
        string actual = await Task.FromResult(optional).MatchAsync(some: value => $"Some {value}", none: () => "None", token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal("Some 21", actual);
    }

    [Fact(DisplayName = "Task<Optional<T>>.MatchAsync<TResult> of None should return the result of the none delegate (Func<T, TResult>, Func<TResult>).")]
    public async Task TaskOptionalMatchAsyncTResultOfNoneShouldReturnResultOfNoneDelegateFuncTTResultFuncTResult()
    {
        // Given
        Optional<int> optional = Optional<int>.None;

        // When
        string actual = await Task.FromResult(optional).MatchAsync(some: value => $"Some {value}", none: () => "None", token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal("None", actual);
    }

    [Fact(DisplayName = "Task<Optional<T>>.MatchAsync<TResult> of Some should return the result of the some delegate (Func<T, Task<TResult>>, Func<TResult>).")]
    public async Task TaskOptionalMatchAsyncTResultOfSomeShouldReturnResultOfSomeDelegateFuncTTaskTResultFuncTResult()
    {
        // Given
        Optional<int> optional = 21;

        // When
        string actual = await Task.FromResult(optional).MatchAsync(some: value => Task.FromResult($"Some {value}"), none: () => "None", token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal("Some 21", actual);
    }

    [Fact(DisplayName = "Task<Optional<T>>.MatchAsync<TResult> of None should return the result of the none delegate (Func<T, Task<TResult>>, Func<TResult>).")]
    public async Task TaskOptionalMatchAsyncTResultOfNoneShouldReturnResultOfNoneDelegateFuncTTaskTResultFuncTResult()
    {
        // Given
        Optional<int> optional = Optional<int>.None;

        // When
        string actual = await Task.FromResult(optional).MatchAsync(some: value => Task.FromResult($"Some {value}"), none: () => "None", token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal("None", actual);
    }

    [Fact(DisplayName = "Task<Optional<T>>.MatchAsync<TResult> of Some should return the result of the some delegate (Func<T, CancellationToken, Task<TResult>>, Func<TResult>).")]
    public async Task TaskOptionalMatchAsyncTResultOfSomeShouldReturnResultOfSomeDelegateFuncTCancellationTokenTaskTResultFuncTResult()
    {
        // Given
        Optional<int> optional = 21;
        CancellationToken someToken = CancellationToken.None;

        // When
        string actual = await Task.FromResult(optional).MatchAsync(some: (value, ct) => { someToken = ct; return Task.FromResult($"Some {value}"); }, none: () => "None", token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal("Some 21", actual);
        Assert.Equal(TestContext.Current.CancellationToken, someToken);
    }

    [Fact(DisplayName = "Task<Optional<T>>.MatchAsync<TResult> of None should return the result of the none delegate (Func<T, CancellationToken, Task<TResult>>, Func<TResult>).")]
    public async Task TaskOptionalMatchAsyncTResultOfNoneShouldReturnResultOfNoneDelegateFuncTCancellationTokenTaskTResultFuncTResult()
    {
        // Given
        Optional<int> optional = Optional<int>.None;
        CancellationToken someToken = CancellationToken.None;

        // When
        string actual = await Task.FromResult(optional).MatchAsync(some: (value, ct) => { someToken = ct; return Task.FromResult($"Some {value}"); }, none: () => "None", token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal("None", actual);
    }

    [Fact(DisplayName = "Task<Optional<T>>.MatchAsync<TResult> of Some should return the result of the some delegate (Func<T, TResult>, Func<Task<TResult>>).")]
    public async Task TaskOptionalMatchAsyncTResultOfSomeShouldReturnResultOfSomeDelegateFuncTTResultFuncTaskTResult()
    {
        // Given
        Optional<int> optional = 21;

        // When
        string actual = await Task.FromResult(optional).MatchAsync(some: value => $"Some {value}", none: () => Task.FromResult("None"), token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal("Some 21", actual);
    }

    [Fact(DisplayName = "Task<Optional<T>>.MatchAsync<TResult> of None should return the result of the none delegate (Func<T, TResult>, Func<Task<TResult>>).")]
    public async Task TaskOptionalMatchAsyncTResultOfNoneShouldReturnResultOfNoneDelegateFuncTTResultFuncTaskTResult()
    {
        // Given
        Optional<int> optional = Optional<int>.None;

        // When
        string actual = await Task.FromResult(optional).MatchAsync(some: value => $"Some {value}", none: () => Task.FromResult("None"), token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal("None", actual);
    }

    [Fact(DisplayName = "Task<Optional<T>>.MatchAsync<TResult> of Some should return the result of the some delegate (Func<T, TResult>, Func<CancellationToken, Task<TResult>>).")]
    public async Task TaskOptionalMatchAsyncTResultOfSomeShouldReturnResultOfSomeDelegateFuncTTResultFuncCancellationTokenTaskTResult()
    {
        // Given
        Optional<int> optional = 21;
        CancellationToken noneToken = CancellationToken.None;

        // When
        string actual = await Task.FromResult(optional).MatchAsync(some: value => $"Some {value}", none: ct => { noneToken = ct; return Task.FromResult("None"); }, token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal("Some 21", actual);
    }

    [Fact(DisplayName = "Task<Optional<T>>.MatchAsync<TResult> of None should return the result of the none delegate (Func<T, TResult>, Func<CancellationToken, Task<TResult>>).")]
    public async Task TaskOptionalMatchAsyncTResultOfNoneShouldReturnResultOfNoneDelegateFuncTTResultFuncCancellationTokenTaskTResult()
    {
        // Given
        Optional<int> optional = Optional<int>.None;
        CancellationToken noneToken = CancellationToken.None;

        // When
        string actual = await Task.FromResult(optional).MatchAsync(some: value => $"Some {value}", none: ct => { noneToken = ct; return Task.FromResult("None"); }, token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal("None", actual);
        Assert.Equal(TestContext.Current.CancellationToken, noneToken);
    }

    [Fact(DisplayName = "Task<Optional<T>>.MatchAsync<TResult> of Some should return the result of the some delegate (Func<T, Task<TResult>>, Func<Task<TResult>>).")]
    public async Task TaskOptionalMatchAsyncTResultOfSomeShouldReturnResultOfSomeDelegateFuncTTaskTResultFuncTaskTResult()
    {
        // Given
        Optional<int> optional = 21;

        // When
        string actual = await Task.FromResult(optional).MatchAsync(some: value => Task.FromResult($"Some {value}"), none: () => Task.FromResult("None"), token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal("Some 21", actual);
    }

    [Fact(DisplayName = "Task<Optional<T>>.MatchAsync<TResult> of None should return the result of the none delegate (Func<T, Task<TResult>>, Func<Task<TResult>>).")]
    public async Task TaskOptionalMatchAsyncTResultOfNoneShouldReturnResultOfNoneDelegateFuncTTaskTResultFuncTaskTResult()
    {
        // Given
        Optional<int> optional = Optional<int>.None;

        // When
        string actual = await Task.FromResult(optional).MatchAsync(some: value => Task.FromResult($"Some {value}"), none: () => Task.FromResult("None"), token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal("None", actual);
    }

    [Fact(DisplayName = "Task<Optional<T>>.MatchAsync<TResult> of Some should return the result of the some delegate (Func<T, CancellationToken, Task<TResult>>, Func<CancellationToken, Task<TResult>>).")]
    public async Task TaskOptionalMatchAsyncTResultOfSomeShouldReturnResultOfSomeDelegateFuncTCancellationTokenTaskTResultFuncCancellationTokenTaskTResult()
    {
        // Given
        Optional<int> optional = 21;
        CancellationToken someToken = CancellationToken.None;
        CancellationToken noneToken = CancellationToken.None;

        // When
        string actual = await Task.FromResult(optional).MatchAsync(some: (value, ct) => { someToken = ct; return Task.FromResult($"Some {value}"); }, none: ct => { noneToken = ct; return Task.FromResult("None"); }, token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal("Some 21", actual);
        Assert.Equal(TestContext.Current.CancellationToken, someToken);
    }

    [Fact(DisplayName = "Task<Optional<T>>.MatchAsync<TResult> of None should return the result of the none delegate (Func<T, CancellationToken, Task<TResult>>, Func<CancellationToken, Task<TResult>>).")]
    public async Task TaskOptionalMatchAsyncTResultOfNoneShouldReturnResultOfNoneDelegateFuncTCancellationTokenTaskTResultFuncCancellationTokenTaskTResult()
    {
        // Given
        Optional<int> optional = Optional<int>.None;
        CancellationToken someToken = CancellationToken.None;
        CancellationToken noneToken = CancellationToken.None;

        // When
        string actual = await Task.FromResult(optional).MatchAsync(some: (value, ct) => { someToken = ct; return Task.FromResult($"Some {value}"); }, none: ct => { noneToken = ct; return Task.FromResult("None"); }, token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal("None", actual);
        Assert.Equal(TestContext.Current.CancellationToken, noneToken);
    }

    [Fact(DisplayName = "Task<Optional<T>>.SelectAsync of Some should produce the expected result (Func<T, TResult>).")]
    public async Task TaskOptionalSelectAsyncOfSomeShouldProduceExpectedResultFuncTTResult()
    {
        // Given
        Optional<int> optional = 21;

        // When
        Optional<int> actual = await Task.FromResult(optional).SelectAsync(value => value * 2, TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(Optional<int>.Some(42), actual);
    }

    [Fact(DisplayName = "Task<Optional<T>>.SelectAsync of None should produce the expected result (Func<T, TResult>).")]
    public async Task TaskOptionalSelectAsyncOfNoneShouldProduceExpectedResultFuncTTResult()
    {
        // Given
        Optional<int> optional = Optional<int>.None;

        // When
        Optional<int> actual = await Task.FromResult(optional).SelectAsync(value => value * 2, TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(Optional<int>.None, actual);
    }

    [Fact(DisplayName = "Task<Optional<T>>.SelectAsync of Some should produce the expected result (Func<T, Task<TResult>>).")]
    public async Task TaskOptionalSelectAsyncOfSomeShouldProduceExpectedResultFuncTTaskTResult()
    {
        // Given
        Optional<int> optional = 21;

        // When
        Optional<int> actual = await Task.FromResult(optional).SelectAsync(value => Task.FromResult(value * 2), TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(Optional<int>.Some(42), actual);
    }

    [Fact(DisplayName = "Task<Optional<T>>.SelectAsync of None should produce the expected result (Func<T, Task<TResult>>).")]
    public async Task TaskOptionalSelectAsyncOfNoneShouldProduceExpectedResultFuncTTaskTResult()
    {
        // Given
        Optional<int> optional = Optional<int>.None;

        // When
        Optional<int> actual = await Task.FromResult(optional).SelectAsync(value => Task.FromResult(value * 2), TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(Optional<int>.None, actual);
    }

    [Fact(DisplayName = "Task<Optional<T>>.SelectAsync of Some should produce the expected result (Func<T, CancellationToken, Task<TResult>>).")]
    public async Task TaskOptionalSelectAsyncOfSomeShouldProduceExpectedResultFuncTCancellationTokenTaskTResult()
    {
        // Given
        Optional<int> optional = 21;
        CancellationToken selectorToken = CancellationToken.None;

        // When
        Optional<int> actual = await Task.FromResult(optional).SelectAsync((value, ct) => { selectorToken = ct; return Task.FromResult(value * 2); }, TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(Optional<int>.Some(42), actual);
        Assert.Equal(TestContext.Current.CancellationToken, selectorToken);
    }

    [Fact(DisplayName = "Task<Optional<T>>.SelectAsync of None should produce the expected result (Func<T, CancellationToken, Task<TResult>>).")]
    public async Task TaskOptionalSelectAsyncOfNoneShouldProduceExpectedResultFuncTCancellationTokenTaskTResult()
    {
        // Given
        Optional<int> optional = Optional<int>.None;
        CancellationToken selectorToken = CancellationToken.None;

        // When
        Optional<int> actual = await Task.FromResult(optional).SelectAsync((value, ct) => { selectorToken = ct; return Task.FromResult(value * 2); }, TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(Optional<int>.None, actual);
    }

    [Fact(DisplayName = "Task<Optional<T>>.SelectManyAsync of Some should produce the expected result (Func<T, Optional<TResult>>).")]
    public async Task TaskOptionalSelectManyAsyncOfSomeShouldProduceExpectedResultFuncTOptionalTResult()
    {
        // Given
        Optional<int> optional = 21;

        // When
        Optional<int> actual = await Task.FromResult(optional).SelectManyAsync(value => Optional<int>.Some(value * 2), TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(Optional<int>.Some(42), actual);
    }

    [Fact(DisplayName = "Task<Optional<T>>.SelectManyAsync of None should produce the expected result (Func<T, Optional<TResult>>).")]
    public async Task TaskOptionalSelectManyAsyncOfNoneShouldProduceExpectedResultFuncTOptionalTResult()
    {
        // Given
        Optional<int> optional = Optional<int>.None;

        // When
        Optional<int> actual = await Task.FromResult(optional).SelectManyAsync(value => Optional<int>.Some(value * 2), TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(Optional<int>.None, actual);
    }

    [Fact(DisplayName = "Task<Optional<T>>.SelectManyAsync of Some should produce the expected result (Func<T, Task<Optional<TResult>>>).")]
    public async Task TaskOptionalSelectManyAsyncOfSomeShouldProduceExpectedResultFuncTTaskOptionalTResult()
    {
        // Given
        Optional<int> optional = 21;

        // When
        Optional<int> actual = await Task.FromResult(optional).SelectManyAsync(value => Task.FromResult<Optional<int>>(value * 2), TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(Optional<int>.Some(42), actual);
    }

    [Fact(DisplayName = "Task<Optional<T>>.SelectManyAsync of None should produce the expected result (Func<T, Task<Optional<TResult>>>).")]
    public async Task TaskOptionalSelectManyAsyncOfNoneShouldProduceExpectedResultFuncTTaskOptionalTResult()
    {
        // Given
        Optional<int> optional = Optional<int>.None;

        // When
        Optional<int> actual = await Task.FromResult(optional).SelectManyAsync(value => Task.FromResult<Optional<int>>(value * 2), TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(Optional<int>.None, actual);
    }

    [Fact(DisplayName = "Task<Optional<T>>.SelectManyAsync of Some should produce the expected result (Func<T, CancellationToken, Task<Optional<TResult>>>).")]
    public async Task TaskOptionalSelectManyAsyncOfSomeShouldProduceExpectedResultFuncTCancellationTokenTaskOptionalTResult()
    {
        // Given
        Optional<int> optional = 21;
        CancellationToken selectorToken = CancellationToken.None;

        // When
        Optional<int> actual = await Task.FromResult(optional).SelectManyAsync((value, ct) => { selectorToken = ct; return Task.FromResult<Optional<int>>(value * 2); }, TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(Optional<int>.Some(42), actual);
        Assert.Equal(TestContext.Current.CancellationToken, selectorToken);
    }

    [Fact(DisplayName = "Task<Optional<T>>.SelectManyAsync of None should produce the expected result (Func<T, CancellationToken, Task<Optional<TResult>>>).")]
    public async Task TaskOptionalSelectManyAsyncOfNoneShouldProduceExpectedResultFuncTCancellationTokenTaskOptionalTResult()
    {
        // Given
        Optional<int> optional = Optional<int>.None;
        CancellationToken selectorToken = CancellationToken.None;

        // When
        Optional<int> actual = await Task.FromResult(optional).SelectManyAsync((value, ct) => { selectorToken = ct; return Task.FromResult<Optional<int>>(value * 2); }, TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(Optional<int>.None, actual);
    }

    [Fact(DisplayName = "Task<Optional<T>>.ToResultAsync of Some should produce a successful result containing the current optional.")]
    public async Task TaskOptionalToResultAsyncOfSomeShouldProduceSuccessfulResultContainingCurrentOptional()
    {
        // Given
        Optional<int> optional = 21;

        // When
        Result<Optional<int>> actual = await Task.FromResult(optional).ToResultAsync(TestContext.Current.CancellationToken);

        // Then
        Assert.IsType<Success<Optional<int>>>(actual);
        Assert.Equal(optional, actual.GetValueOrThrow());
    }

    [Fact(DisplayName = "Task<Optional<T>>.ToResultAsync of None should produce a successful result containing the current optional.")]
    public async Task TaskOptionalToResultAsyncOfNoneShouldProduceSuccessfulResultContainingCurrentOptional()
    {
        // Given
        Optional<int> optional = Optional<int>.None;

        // When
        Result<Optional<int>> actual = await Task.FromResult(optional).ToResultAsync(TestContext.Current.CancellationToken);

        // Then
        Assert.IsType<Success<Optional<int>>>(actual);
        Assert.Equal(optional, actual.GetValueOrThrow());
    }
}
