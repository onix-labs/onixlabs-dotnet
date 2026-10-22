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

public sealed class OptionalValueTaskExtensionTests
{
    [Fact(DisplayName = "ValueTask<Optional<T>>.GetValueOrDefaultAsync of Some should produce the expected result.")]
    public async Task ValueTaskOptionalGetValueOrDefaultAsyncOfSomeShouldProduceExpectedResult()
    {
        // Given
        Optional<int> optional = 21;

        // When
        int actual = await ValueTask.FromResult(optional).GetValueOrDefaultAsync(TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(21, actual);
    }

    [Fact(DisplayName = "ValueTask<Optional<T>>.GetValueOrDefaultAsync with default value of Some should produce the expected result.")]
    public async Task ValueTaskOptionalGetValueOrDefaultAsyncWithDefaultValueOfSomeShouldProduceExpectedResult()
    {
        // Given
        Optional<int> optional = 21;

        // When
        int actual = await ValueTask.FromResult(optional).GetValueOrDefaultAsync(99, TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(21, actual);
    }

    [Fact(DisplayName = "ValueTask<Optional<T>>.GetValueOrDefaultAsync of None should produce the expected result.")]
    public async Task ValueTaskOptionalGetValueOrDefaultAsyncOfNoneShouldProduceExpectedResult()
    {
        // Given
        Optional<int> optional = Optional<int>.None;

        // When
        int actual = await ValueTask.FromResult(optional).GetValueOrDefaultAsync(TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(0, actual);
    }

    [Fact(DisplayName = "ValueTask<Optional<T>>.GetValueOrDefaultAsync with default value of None should produce the expected result.")]
    public async Task ValueTaskOptionalGetValueOrDefaultAsyncWithDefaultValueOfNoneShouldProduceExpectedResult()
    {
        // Given
        Optional<int> optional = Optional<int>.None;

        // When
        int actual = await ValueTask.FromResult(optional).GetValueOrDefaultAsync(99, TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(99, actual);
    }

    [Fact(DisplayName = "ValueTask<Optional<T>>.GetValueOrThrowAsync of Some should produce the expected result.")]
    public async Task ValueTaskOptionalGetValueOrThrowAsyncOfSomeShouldProduceExpectedResult()
    {
        // Given
        Optional<int> optional = 21;

        // When
        int actual = await ValueTask.FromResult(optional).GetValueOrThrowAsync(TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(21, actual);
    }

    [Fact(DisplayName = "ValueTask<Optional<T>>.GetValueOrThrowAsync of None should throw InvalidOperationException.")]
    public async Task ValueTaskOptionalGetValueOrThrowAsyncOfNoneShouldThrowInvalidOperationException()
    {
        // Given
        Optional<int> optional = Optional<int>.None;

        // When
        Exception exception = await Assert.ThrowsAsync<InvalidOperationException>(async () => await ValueTask.FromResult(optional).GetValueOrThrowAsync(TestContext.Current.CancellationToken));

        // Then
        Assert.Equal("Optional value of type System.Int32 is not present.", exception.Message);
    }

    [Fact(DisplayName = "ValueTask<Optional<T>>.MatchAsync of Some should invoke the some delegate (Action<T>, Action).")]
    public async Task ValueTaskOptionalMatchAsyncOfSomeShouldInvokeSomeDelegateActionTAction()
    {
        // Given
        Optional<int> optional = 21;
        int someValue = 0;
        bool noneCalled = false;

        // When
        await ValueTask.FromResult(optional).MatchAsync(some: value => someValue = value, none: () => noneCalled = true, token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(21, someValue);
        Assert.False(noneCalled);
    }

    [Fact(DisplayName = "ValueTask<Optional<T>>.MatchAsync of None should invoke the none delegate (Action<T>, Action).")]
    public async Task ValueTaskOptionalMatchAsyncOfNoneShouldInvokeNoneDelegateActionTAction()
    {
        // Given
        Optional<int> optional = Optional<int>.None;
        int someValue = 0;
        bool noneCalled = false;

        // When
        await ValueTask.FromResult(optional).MatchAsync(some: value => someValue = value, none: () => noneCalled = true, token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(0, someValue);
        Assert.True(noneCalled);
    }

    [Fact(DisplayName = "ValueTask<Optional<T>>.MatchAsync of Some should invoke the some delegate (Func<T, Task>, Action).")]
    public async Task ValueTaskOptionalMatchAsyncOfSomeShouldInvokeSomeDelegateFuncTTaskAction()
    {
        // Given
        Optional<int> optional = 21;
        int someValue = 0;
        bool noneCalled = false;

        // When
        await ValueTask.FromResult(optional).MatchAsync(some: value => { someValue = value; return Task.CompletedTask; }, none: () => noneCalled = true, token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(21, someValue);
        Assert.False(noneCalled);
    }

    [Fact(DisplayName = "ValueTask<Optional<T>>.MatchAsync of None should invoke the none delegate (Func<T, Task>, Action).")]
    public async Task ValueTaskOptionalMatchAsyncOfNoneShouldInvokeNoneDelegateFuncTTaskAction()
    {
        // Given
        Optional<int> optional = Optional<int>.None;
        int someValue = 0;
        bool noneCalled = false;

        // When
        await ValueTask.FromResult(optional).MatchAsync(some: value => { someValue = value; return Task.CompletedTask; }, none: () => noneCalled = true, token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(0, someValue);
        Assert.True(noneCalled);
    }

    [Fact(DisplayName = "ValueTask<Optional<T>>.MatchAsync of Some should invoke the some delegate (Func<T, CancellationToken, Task>, Action).")]
    public async Task ValueTaskOptionalMatchAsyncOfSomeShouldInvokeSomeDelegateFuncTCancellationTokenTaskAction()
    {
        // Given
        Optional<int> optional = 21;
        int someValue = 0;
        bool noneCalled = false;
        CancellationToken someToken = CancellationToken.None;

        // When
        await ValueTask.FromResult(optional).MatchAsync(some: (value, ct) => { someValue = value; someToken = ct; return Task.CompletedTask; }, none: () => noneCalled = true, token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(21, someValue);
        Assert.False(noneCalled);
        Assert.Equal(TestContext.Current.CancellationToken, someToken);
    }

    [Fact(DisplayName = "ValueTask<Optional<T>>.MatchAsync of None should invoke the none delegate (Func<T, CancellationToken, Task>, Action).")]
    public async Task ValueTaskOptionalMatchAsyncOfNoneShouldInvokeNoneDelegateFuncTCancellationTokenTaskAction()
    {
        // Given
        Optional<int> optional = Optional<int>.None;
        int someValue = 0;
        bool noneCalled = false;
        CancellationToken someToken = CancellationToken.None;

        // When
        await ValueTask.FromResult(optional).MatchAsync(some: (value, ct) => { someValue = value; someToken = ct; return Task.CompletedTask; }, none: () => noneCalled = true, token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(0, someValue);
        Assert.True(noneCalled);
    }

    [Fact(DisplayName = "ValueTask<Optional<T>>.MatchAsync of Some should invoke the some delegate (Action<T>, Func<Task>).")]
    public async Task ValueTaskOptionalMatchAsyncOfSomeShouldInvokeSomeDelegateActionTFuncTask()
    {
        // Given
        Optional<int> optional = 21;
        int someValue = 0;
        bool noneCalled = false;

        // When
        await ValueTask.FromResult(optional).MatchAsync(some: value => someValue = value, none: () => { noneCalled = true; return Task.CompletedTask; }, token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(21, someValue);
        Assert.False(noneCalled);
    }

    [Fact(DisplayName = "ValueTask<Optional<T>>.MatchAsync of None should invoke the none delegate (Action<T>, Func<Task>).")]
    public async Task ValueTaskOptionalMatchAsyncOfNoneShouldInvokeNoneDelegateActionTFuncTask()
    {
        // Given
        Optional<int> optional = Optional<int>.None;
        int someValue = 0;
        bool noneCalled = false;

        // When
        await ValueTask.FromResult(optional).MatchAsync(some: value => someValue = value, none: () => { noneCalled = true; return Task.CompletedTask; }, token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(0, someValue);
        Assert.True(noneCalled);
    }

    [Fact(DisplayName = "ValueTask<Optional<T>>.MatchAsync of Some should invoke the some delegate (Action<T>, Func<CancellationToken, Task>).")]
    public async Task ValueTaskOptionalMatchAsyncOfSomeShouldInvokeSomeDelegateActionTFuncCancellationTokenTask()
    {
        // Given
        Optional<int> optional = 21;
        int someValue = 0;
        bool noneCalled = false;
        CancellationToken noneToken = CancellationToken.None;

        // When
        await ValueTask.FromResult(optional).MatchAsync(some: value => someValue = value, none: ct => { noneCalled = true; noneToken = ct; return Task.CompletedTask; }, token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(21, someValue);
        Assert.False(noneCalled);
    }

    [Fact(DisplayName = "ValueTask<Optional<T>>.MatchAsync of None should invoke the none delegate (Action<T>, Func<CancellationToken, Task>).")]
    public async Task ValueTaskOptionalMatchAsyncOfNoneShouldInvokeNoneDelegateActionTFuncCancellationTokenTask()
    {
        // Given
        Optional<int> optional = Optional<int>.None;
        int someValue = 0;
        bool noneCalled = false;
        CancellationToken noneToken = CancellationToken.None;

        // When
        await ValueTask.FromResult(optional).MatchAsync(some: value => someValue = value, none: ct => { noneCalled = true; noneToken = ct; return Task.CompletedTask; }, token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(0, someValue);
        Assert.True(noneCalled);
        Assert.Equal(TestContext.Current.CancellationToken, noneToken);
    }

    [Fact(DisplayName = "ValueTask<Optional<T>>.MatchAsync of Some should invoke the some delegate (Func<T, Task>, Func<Task>).")]
    public async Task ValueTaskOptionalMatchAsyncOfSomeShouldInvokeSomeDelegateFuncTTaskFuncTask()
    {
        // Given
        Optional<int> optional = 21;
        int someValue = 0;
        bool noneCalled = false;

        // When
        await ValueTask.FromResult(optional).MatchAsync(some: value => { someValue = value; return Task.CompletedTask; }, none: () => { noneCalled = true; return Task.CompletedTask; }, token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(21, someValue);
        Assert.False(noneCalled);
    }

    [Fact(DisplayName = "ValueTask<Optional<T>>.MatchAsync of None should invoke the none delegate (Func<T, Task>, Func<Task>).")]
    public async Task ValueTaskOptionalMatchAsyncOfNoneShouldInvokeNoneDelegateFuncTTaskFuncTask()
    {
        // Given
        Optional<int> optional = Optional<int>.None;
        int someValue = 0;
        bool noneCalled = false;

        // When
        await ValueTask.FromResult(optional).MatchAsync(some: value => { someValue = value; return Task.CompletedTask; }, none: () => { noneCalled = true; return Task.CompletedTask; }, token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(0, someValue);
        Assert.True(noneCalled);
    }

    [Fact(DisplayName = "ValueTask<Optional<T>>.MatchAsync of Some should invoke the some delegate (Func<T, CancellationToken, Task>, Func<CancellationToken, Task>).")]
    public async Task ValueTaskOptionalMatchAsyncOfSomeShouldInvokeSomeDelegateFuncTCancellationTokenTaskFuncCancellationTokenTask()
    {
        // Given
        Optional<int> optional = 21;
        int someValue = 0;
        bool noneCalled = false;
        CancellationToken someToken = CancellationToken.None;
        CancellationToken noneToken = CancellationToken.None;

        // When
        await ValueTask.FromResult(optional).MatchAsync(some: (value, ct) => { someValue = value; someToken = ct; return Task.CompletedTask; }, none: ct => { noneCalled = true; noneToken = ct; return Task.CompletedTask; }, token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(21, someValue);
        Assert.False(noneCalled);
        Assert.Equal(TestContext.Current.CancellationToken, someToken);
    }

    [Fact(DisplayName = "ValueTask<Optional<T>>.MatchAsync of None should invoke the none delegate (Func<T, CancellationToken, Task>, Func<CancellationToken, Task>).")]
    public async Task ValueTaskOptionalMatchAsyncOfNoneShouldInvokeNoneDelegateFuncTCancellationTokenTaskFuncCancellationTokenTask()
    {
        // Given
        Optional<int> optional = Optional<int>.None;
        int someValue = 0;
        bool noneCalled = false;
        CancellationToken someToken = CancellationToken.None;
        CancellationToken noneToken = CancellationToken.None;

        // When
        await ValueTask.FromResult(optional).MatchAsync(some: (value, ct) => { someValue = value; someToken = ct; return Task.CompletedTask; }, none: ct => { noneCalled = true; noneToken = ct; return Task.CompletedTask; }, token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(0, someValue);
        Assert.True(noneCalled);
        Assert.Equal(TestContext.Current.CancellationToken, noneToken);
    }

    [Fact(DisplayName = "ValueTask<Optional<T>>.MatchAsync<TResult> of Some should return the result of the some delegate (Func<T, TResult>, Func<TResult>).")]
    public async Task ValueTaskOptionalMatchAsyncTResultOfSomeShouldReturnResultOfSomeDelegateFuncTTResultFuncTResult()
    {
        // Given
        Optional<int> optional = 21;

        // When
        string actual = await ValueTask.FromResult(optional).MatchAsync(some: value => $"Some {value}", none: () => "None", token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal("Some 21", actual);
    }

    [Fact(DisplayName = "ValueTask<Optional<T>>.MatchAsync<TResult> of None should return the result of the none delegate (Func<T, TResult>, Func<TResult>).")]
    public async Task ValueTaskOptionalMatchAsyncTResultOfNoneShouldReturnResultOfNoneDelegateFuncTTResultFuncTResult()
    {
        // Given
        Optional<int> optional = Optional<int>.None;

        // When
        string actual = await ValueTask.FromResult(optional).MatchAsync(some: value => $"Some {value}", none: () => "None", token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal("None", actual);
    }

    [Fact(DisplayName = "ValueTask<Optional<T>>.MatchAsync<TResult> of Some should return the result of the some delegate (Func<T, Task<TResult>>, Func<TResult>).")]
    public async Task ValueTaskOptionalMatchAsyncTResultOfSomeShouldReturnResultOfSomeDelegateFuncTTaskTResultFuncTResult()
    {
        // Given
        Optional<int> optional = 21;

        // When
        string actual = await ValueTask.FromResult(optional).MatchAsync(some: value => Task.FromResult($"Some {value}"), none: () => "None", token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal("Some 21", actual);
    }

    [Fact(DisplayName = "ValueTask<Optional<T>>.MatchAsync<TResult> of None should return the result of the none delegate (Func<T, Task<TResult>>, Func<TResult>).")]
    public async Task ValueTaskOptionalMatchAsyncTResultOfNoneShouldReturnResultOfNoneDelegateFuncTTaskTResultFuncTResult()
    {
        // Given
        Optional<int> optional = Optional<int>.None;

        // When
        string actual = await ValueTask.FromResult(optional).MatchAsync(some: value => Task.FromResult($"Some {value}"), none: () => "None", token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal("None", actual);
    }

    [Fact(DisplayName = "ValueTask<Optional<T>>.MatchAsync<TResult> of Some should return the result of the some delegate (Func<T, CancellationToken, Task<TResult>>, Func<TResult>).")]
    public async Task ValueTaskOptionalMatchAsyncTResultOfSomeShouldReturnResultOfSomeDelegateFuncTCancellationTokenTaskTResultFuncTResult()
    {
        // Given
        Optional<int> optional = 21;
        CancellationToken someToken = CancellationToken.None;

        // When
        string actual = await ValueTask.FromResult(optional).MatchAsync(some: (value, ct) => { someToken = ct; return Task.FromResult($"Some {value}"); }, none: () => "None", token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal("Some 21", actual);
        Assert.Equal(TestContext.Current.CancellationToken, someToken);
    }

    [Fact(DisplayName = "ValueTask<Optional<T>>.MatchAsync<TResult> of None should return the result of the none delegate (Func<T, CancellationToken, Task<TResult>>, Func<TResult>).")]
    public async Task ValueTaskOptionalMatchAsyncTResultOfNoneShouldReturnResultOfNoneDelegateFuncTCancellationTokenTaskTResultFuncTResult()
    {
        // Given
        Optional<int> optional = Optional<int>.None;
        CancellationToken someToken = CancellationToken.None;

        // When
        string actual = await ValueTask.FromResult(optional).MatchAsync(some: (value, ct) => { someToken = ct; return Task.FromResult($"Some {value}"); }, none: () => "None", token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal("None", actual);
    }

    [Fact(DisplayName = "ValueTask<Optional<T>>.MatchAsync<TResult> of Some should return the result of the some delegate (Func<T, TResult>, Func<Task<TResult>>).")]
    public async Task ValueTaskOptionalMatchAsyncTResultOfSomeShouldReturnResultOfSomeDelegateFuncTTResultFuncTaskTResult()
    {
        // Given
        Optional<int> optional = 21;

        // When
        string actual = await ValueTask.FromResult(optional).MatchAsync(some: value => $"Some {value}", none: () => Task.FromResult("None"), token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal("Some 21", actual);
    }

    [Fact(DisplayName = "ValueTask<Optional<T>>.MatchAsync<TResult> of None should return the result of the none delegate (Func<T, TResult>, Func<Task<TResult>>).")]
    public async Task ValueTaskOptionalMatchAsyncTResultOfNoneShouldReturnResultOfNoneDelegateFuncTTResultFuncTaskTResult()
    {
        // Given
        Optional<int> optional = Optional<int>.None;

        // When
        string actual = await ValueTask.FromResult(optional).MatchAsync(some: value => $"Some {value}", none: () => Task.FromResult("None"), token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal("None", actual);
    }

    [Fact(DisplayName = "ValueTask<Optional<T>>.MatchAsync<TResult> of Some should return the result of the some delegate (Func<T, TResult>, Func<CancellationToken, Task<TResult>>).")]
    public async Task ValueTaskOptionalMatchAsyncTResultOfSomeShouldReturnResultOfSomeDelegateFuncTTResultFuncCancellationTokenTaskTResult()
    {
        // Given
        Optional<int> optional = 21;
        CancellationToken noneToken = CancellationToken.None;

        // When
        string actual = await ValueTask.FromResult(optional).MatchAsync(some: value => $"Some {value}", none: ct => { noneToken = ct; return Task.FromResult("None"); }, token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal("Some 21", actual);
    }

    [Fact(DisplayName = "ValueTask<Optional<T>>.MatchAsync<TResult> of None should return the result of the none delegate (Func<T, TResult>, Func<CancellationToken, Task<TResult>>).")]
    public async Task ValueTaskOptionalMatchAsyncTResultOfNoneShouldReturnResultOfNoneDelegateFuncTTResultFuncCancellationTokenTaskTResult()
    {
        // Given
        Optional<int> optional = Optional<int>.None;
        CancellationToken noneToken = CancellationToken.None;

        // When
        string actual = await ValueTask.FromResult(optional).MatchAsync(some: value => $"Some {value}", none: ct => { noneToken = ct; return Task.FromResult("None"); }, token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal("None", actual);
        Assert.Equal(TestContext.Current.CancellationToken, noneToken);
    }

    [Fact(DisplayName = "ValueTask<Optional<T>>.MatchAsync<TResult> of Some should return the result of the some delegate (Func<T, Task<TResult>>, Func<Task<TResult>>).")]
    public async Task ValueTaskOptionalMatchAsyncTResultOfSomeShouldReturnResultOfSomeDelegateFuncTTaskTResultFuncTaskTResult()
    {
        // Given
        Optional<int> optional = 21;

        // When
        string actual = await ValueTask.FromResult(optional).MatchAsync(some: value => Task.FromResult($"Some {value}"), none: () => Task.FromResult("None"), token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal("Some 21", actual);
    }

    [Fact(DisplayName = "ValueTask<Optional<T>>.MatchAsync<TResult> of None should return the result of the none delegate (Func<T, Task<TResult>>, Func<Task<TResult>>).")]
    public async Task ValueTaskOptionalMatchAsyncTResultOfNoneShouldReturnResultOfNoneDelegateFuncTTaskTResultFuncTaskTResult()
    {
        // Given
        Optional<int> optional = Optional<int>.None;

        // When
        string actual = await ValueTask.FromResult(optional).MatchAsync(some: value => Task.FromResult($"Some {value}"), none: () => Task.FromResult("None"), token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal("None", actual);
    }

    [Fact(DisplayName = "ValueTask<Optional<T>>.MatchAsync<TResult> of Some should return the result of the some delegate (Func<T, CancellationToken, Task<TResult>>, Func<CancellationToken, Task<TResult>>).")]
    public async Task ValueTaskOptionalMatchAsyncTResultOfSomeShouldReturnResultOfSomeDelegateFuncTCancellationTokenTaskTResultFuncCancellationTokenTaskTResult()
    {
        // Given
        Optional<int> optional = 21;
        CancellationToken someToken = CancellationToken.None;
        CancellationToken noneToken = CancellationToken.None;

        // When
        string actual = await ValueTask.FromResult(optional).MatchAsync(some: (value, ct) => { someToken = ct; return Task.FromResult($"Some {value}"); }, none: ct => { noneToken = ct; return Task.FromResult("None"); }, token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal("Some 21", actual);
        Assert.Equal(TestContext.Current.CancellationToken, someToken);
    }

    [Fact(DisplayName = "ValueTask<Optional<T>>.MatchAsync<TResult> of None should return the result of the none delegate (Func<T, CancellationToken, Task<TResult>>, Func<CancellationToken, Task<TResult>>).")]
    public async Task ValueTaskOptionalMatchAsyncTResultOfNoneShouldReturnResultOfNoneDelegateFuncTCancellationTokenTaskTResultFuncCancellationTokenTaskTResult()
    {
        // Given
        Optional<int> optional = Optional<int>.None;
        CancellationToken someToken = CancellationToken.None;
        CancellationToken noneToken = CancellationToken.None;

        // When
        string actual = await ValueTask.FromResult(optional).MatchAsync(some: (value, ct) => { someToken = ct; return Task.FromResult($"Some {value}"); }, none: ct => { noneToken = ct; return Task.FromResult("None"); }, token: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal("None", actual);
        Assert.Equal(TestContext.Current.CancellationToken, noneToken);
    }

    [Fact(DisplayName = "ValueTask<Optional<T>>.SelectAsync of Some should produce the expected result (Func<T, TResult>).")]
    public async Task ValueTaskOptionalSelectAsyncOfSomeShouldProduceExpectedResultFuncTTResult()
    {
        // Given
        Optional<int> optional = 21;

        // When
        Optional<int> actual = await ValueTask.FromResult(optional).SelectAsync(value => value * 2, TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(Optional<int>.Some(42), actual);
    }

    [Fact(DisplayName = "ValueTask<Optional<T>>.SelectAsync of None should produce the expected result (Func<T, TResult>).")]
    public async Task ValueTaskOptionalSelectAsyncOfNoneShouldProduceExpectedResultFuncTTResult()
    {
        // Given
        Optional<int> optional = Optional<int>.None;

        // When
        Optional<int> actual = await ValueTask.FromResult(optional).SelectAsync(value => value * 2, TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(Optional<int>.None, actual);
    }

    [Fact(DisplayName = "ValueTask<Optional<T>>.SelectAsync of Some should produce the expected result (Func<T, Task<TResult>>).")]
    public async Task ValueTaskOptionalSelectAsyncOfSomeShouldProduceExpectedResultFuncTTaskTResult()
    {
        // Given
        Optional<int> optional = 21;

        // When
        Optional<int> actual = await ValueTask.FromResult(optional).SelectAsync(value => Task.FromResult(value * 2), TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(Optional<int>.Some(42), actual);
    }

    [Fact(DisplayName = "ValueTask<Optional<T>>.SelectAsync of None should produce the expected result (Func<T, Task<TResult>>).")]
    public async Task ValueTaskOptionalSelectAsyncOfNoneShouldProduceExpectedResultFuncTTaskTResult()
    {
        // Given
        Optional<int> optional = Optional<int>.None;

        // When
        Optional<int> actual = await ValueTask.FromResult(optional).SelectAsync(value => Task.FromResult(value * 2), TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(Optional<int>.None, actual);
    }

    [Fact(DisplayName = "ValueTask<Optional<T>>.SelectAsync of Some should produce the expected result (Func<T, CancellationToken, Task<TResult>>).")]
    public async Task ValueTaskOptionalSelectAsyncOfSomeShouldProduceExpectedResultFuncTCancellationTokenTaskTResult()
    {
        // Given
        Optional<int> optional = 21;
        CancellationToken selectorToken = CancellationToken.None;

        // When
        Optional<int> actual = await ValueTask.FromResult(optional).SelectAsync((value, ct) => { selectorToken = ct; return Task.FromResult(value * 2); }, TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(Optional<int>.Some(42), actual);
        Assert.Equal(TestContext.Current.CancellationToken, selectorToken);
    }

    [Fact(DisplayName = "ValueTask<Optional<T>>.SelectAsync of None should produce the expected result (Func<T, CancellationToken, Task<TResult>>).")]
    public async Task ValueTaskOptionalSelectAsyncOfNoneShouldProduceExpectedResultFuncTCancellationTokenTaskTResult()
    {
        // Given
        Optional<int> optional = Optional<int>.None;
        CancellationToken selectorToken = CancellationToken.None;

        // When
        Optional<int> actual = await ValueTask.FromResult(optional).SelectAsync((value, ct) => { selectorToken = ct; return Task.FromResult(value * 2); }, TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(Optional<int>.None, actual);
    }

    [Fact(DisplayName = "ValueTask<Optional<T>>.SelectManyAsync of Some should produce the expected result (Func<T, Optional<TResult>>).")]
    public async Task ValueTaskOptionalSelectManyAsyncOfSomeShouldProduceExpectedResultFuncTOptionalTResult()
    {
        // Given
        Optional<int> optional = 21;

        // When
        Optional<int> actual = await ValueTask.FromResult(optional).SelectManyAsync(value => Optional<int>.Some(value * 2), TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(Optional<int>.Some(42), actual);
    }

    [Fact(DisplayName = "ValueTask<Optional<T>>.SelectManyAsync of None should produce the expected result (Func<T, Optional<TResult>>).")]
    public async Task ValueTaskOptionalSelectManyAsyncOfNoneShouldProduceExpectedResultFuncTOptionalTResult()
    {
        // Given
        Optional<int> optional = Optional<int>.None;

        // When
        Optional<int> actual = await ValueTask.FromResult(optional).SelectManyAsync(value => Optional<int>.Some(value * 2), TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(Optional<int>.None, actual);
    }

    [Fact(DisplayName = "ValueTask<Optional<T>>.SelectManyAsync of Some should produce the expected result (Func<T, Task<Optional<TResult>>>).")]
    public async Task ValueTaskOptionalSelectManyAsyncOfSomeShouldProduceExpectedResultFuncTTaskOptionalTResult()
    {
        // Given
        Optional<int> optional = 21;

        // When
        Optional<int> actual = await ValueTask.FromResult(optional).SelectManyAsync(value => Task.FromResult<Optional<int>>(value * 2), TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(Optional<int>.Some(42), actual);
    }

    [Fact(DisplayName = "ValueTask<Optional<T>>.SelectManyAsync of None should produce the expected result (Func<T, Task<Optional<TResult>>>).")]
    public async Task ValueTaskOptionalSelectManyAsyncOfNoneShouldProduceExpectedResultFuncTTaskOptionalTResult()
    {
        // Given
        Optional<int> optional = Optional<int>.None;

        // When
        Optional<int> actual = await ValueTask.FromResult(optional).SelectManyAsync(value => Task.FromResult<Optional<int>>(value * 2), TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(Optional<int>.None, actual);
    }

    [Fact(DisplayName = "ValueTask<Optional<T>>.SelectManyAsync of Some should produce the expected result (Func<T, CancellationToken, Task<Optional<TResult>>>).")]
    public async Task ValueTaskOptionalSelectManyAsyncOfSomeShouldProduceExpectedResultFuncTCancellationTokenTaskOptionalTResult()
    {
        // Given
        Optional<int> optional = 21;
        CancellationToken selectorToken = CancellationToken.None;

        // When
        Optional<int> actual = await ValueTask.FromResult(optional).SelectManyAsync((value, ct) => { selectorToken = ct; return Task.FromResult<Optional<int>>(value * 2); }, TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(Optional<int>.Some(42), actual);
        Assert.Equal(TestContext.Current.CancellationToken, selectorToken);
    }

    [Fact(DisplayName = "ValueTask<Optional<T>>.SelectManyAsync of None should produce the expected result (Func<T, CancellationToken, Task<Optional<TResult>>>).")]
    public async Task ValueTaskOptionalSelectManyAsyncOfNoneShouldProduceExpectedResultFuncTCancellationTokenTaskOptionalTResult()
    {
        // Given
        Optional<int> optional = Optional<int>.None;
        CancellationToken selectorToken = CancellationToken.None;

        // When
        Optional<int> actual = await ValueTask.FromResult(optional).SelectManyAsync((value, ct) => { selectorToken = ct; return Task.FromResult<Optional<int>>(value * 2); }, TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(Optional<int>.None, actual);
    }

    [Fact(DisplayName = "ValueTask<Optional<T>>.ToResultAsync of Some should produce a successful result containing the current optional.")]
    public async Task ValueTaskOptionalToResultAsyncOfSomeShouldProduceSuccessfulResultContainingCurrentOptional()
    {
        // Given
        Optional<int> optional = 21;

        // When
        Result<Optional<int>> actual = await ValueTask.FromResult(optional).ToResultAsync(TestContext.Current.CancellationToken);

        // Then
        Assert.IsType<Success<Optional<int>>>(actual);
        Assert.Equal(optional, actual.GetValueOrThrow());
    }

    [Fact(DisplayName = "ValueTask<Optional<T>>.ToResultAsync of None should produce a successful result containing the current optional.")]
    public async Task ValueTaskOptionalToResultAsyncOfNoneShouldProduceSuccessfulResultContainingCurrentOptional()
    {
        // Given
        Optional<int> optional = Optional<int>.None;

        // When
        Result<Optional<int>> actual = await ValueTask.FromResult(optional).ToResultAsync(TestContext.Current.CancellationToken);

        // Then
        Assert.IsType<Success<Optional<int>>>(actual);
        Assert.Equal(optional, actual.GetValueOrThrow());
    }
}
