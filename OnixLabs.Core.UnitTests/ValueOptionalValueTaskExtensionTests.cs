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

public sealed class ValueOptionalValueTaskExtensionTests
{
    [Fact(DisplayName = "ValueTask<ValueOptional<T>>.GetValueOrDefaultAsync of Some should produce the expected result.")]
    public async Task ValueTaskValueOptionalGetValueOrDefaultAsyncOfSomeShouldProduceExpectedResult()
    {
        // Given
        ValueOptional<int> optional = 21;

        // When
        int actual = await ValueTask.FromResult(optional).GetValueOrDefaultAsync(TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(21, actual);
    }

    [Fact(DisplayName = "ValueTask<ValueOptional<T>>.GetValueOrDefaultAsync with default value of Some should produce the expected result.")]
    public async Task ValueTaskValueOptionalGetValueOrDefaultAsyncWithDefaultValueOfSomeShouldProduceExpectedResult()
    {
        // Given
        ValueOptional<int> optional = 21;

        // When
        int actual = await ValueTask.FromResult(optional).GetValueOrDefaultAsync(99, TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(21, actual);
    }

    [Fact(DisplayName = "ValueTask<ValueOptional<T>>.GetValueOrDefaultAsync of None should produce the expected result.")]
    public async Task ValueTaskValueOptionalGetValueOrDefaultAsyncOfNoneShouldProduceExpectedResult()
    {
        // Given
        ValueOptional<int> optional = ValueOptional<int>.None;

        // When
        int actual = await ValueTask.FromResult(optional).GetValueOrDefaultAsync(TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(0, actual);
    }

    [Fact(DisplayName = "ValueTask<ValueOptional<T>>.GetValueOrDefaultAsync with default value of None should produce the expected result.")]
    public async Task ValueTaskValueOptionalGetValueOrDefaultAsyncWithDefaultValueOfNoneShouldProduceExpectedResult()
    {
        // Given
        ValueOptional<int> optional = ValueOptional<int>.None;

        // When
        int actual = await ValueTask.FromResult(optional).GetValueOrDefaultAsync(99, TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(99, actual);
    }

    [Fact(DisplayName = "ValueTask<ValueOptional<T>>.GetValueOrThrowAsync of Some should produce the expected result.")]
    public async Task ValueTaskValueOptionalGetValueOrThrowAsyncOfSomeShouldProduceExpectedResult()
    {
        // Given
        ValueOptional<int> optional = 21;

        // When
        int actual = await ValueTask.FromResult(optional).GetValueOrThrowAsync(TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(21, actual);
    }

    [Fact(DisplayName = "ValueTask<ValueOptional<T>>.GetValueOrThrowAsync of None should throw InvalidOperationException.")]
    public async Task ValueTaskValueOptionalGetValueOrThrowAsyncOfNoneShouldThrowInvalidOperationException()
    {
        // Given
        ValueOptional<int> optional = ValueOptional<int>.None;

        // When
        Exception exception = await Assert.ThrowsAsync<InvalidOperationException>(async () => await ValueTask.FromResult(optional).GetValueOrThrowAsync(TestContext.Current.CancellationToken));

        // Then
        Assert.Equal("Optional value of type System.Int32 is not present.", exception.Message);
    }

    [Fact(DisplayName = "ValueTask<ValueOptional<T>>.MatchAsync of Some should invoke the some delegate (Action<T>, Action).")]
    public async Task ValueTaskValueOptionalMatchAsyncOfSomeShouldInvokeSomeDelegateActionTAction()
    {
        // Given
        ValueOptional<int> optional = 21;
        int someValue = 0;
        bool noneCalled = false;

        // When
        await ValueTask.FromResult(optional).MatchAsync(some: value => someValue = value, none: () => noneCalled = true, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(21, someValue);
        Assert.False(noneCalled);
    }

    [Fact(DisplayName = "ValueTask<ValueOptional<T>>.MatchAsync of None should invoke the none delegate (Action<T>, Action).")]
    public async Task ValueTaskValueOptionalMatchAsyncOfNoneShouldInvokeNoneDelegateActionTAction()
    {
        // Given
        ValueOptional<int> optional = ValueOptional<int>.None;
        int someValue = 0;
        bool noneCalled = false;

        // When
        await ValueTask.FromResult(optional).MatchAsync(some: value => someValue = value, none: () => noneCalled = true, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(0, someValue);
        Assert.True(noneCalled);
    }

    [Fact(DisplayName = "ValueTask<ValueOptional<T>>.MatchAsync of Some should invoke the some delegate (Func<T, Task>, Action).")]
    public async Task ValueTaskValueOptionalMatchAsyncOfSomeShouldInvokeSomeDelegateFuncTTaskAction()
    {
        // Given
        ValueOptional<int> optional = 21;
        int someValue = 0;
        bool noneCalled = false;

        // When
        await ValueTask.FromResult(optional).MatchAsync(some: value => { someValue = value; return Task.CompletedTask; }, none: () => noneCalled = true, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(21, someValue);
        Assert.False(noneCalled);
    }

    [Fact(DisplayName = "ValueTask<ValueOptional<T>>.MatchAsync of Some should invoke the some delegate (Func<T, ValueTask>, Action).")]
    public async Task ValueTaskValueOptionalMatchAsyncOfSomeShouldInvokeSomeDelegateFuncTTaskActionWithValueTaskDelegates()
    {
        // Given
        ValueOptional<int> optional = 21;
        int someValue = 0;
        bool noneCalled = false;

        // When
        await ValueTask.FromResult(optional).MatchAsync(some: value => { someValue = value; return ValueTask.CompletedTask; }, none: () => noneCalled = true, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(21, someValue);
        Assert.False(noneCalled);
    }

    [Fact(DisplayName = "ValueTask<ValueOptional<T>>.MatchAsync of None should invoke the none delegate (Func<T, Task>, Action).")]
    public async Task ValueTaskValueOptionalMatchAsyncOfNoneShouldInvokeNoneDelegateFuncTTaskAction()
    {
        // Given
        ValueOptional<int> optional = ValueOptional<int>.None;
        int someValue = 0;
        bool noneCalled = false;

        // When
        await ValueTask.FromResult(optional).MatchAsync(some: value => { someValue = value; return Task.CompletedTask; }, none: () => noneCalled = true, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(0, someValue);
        Assert.True(noneCalled);
    }

    [Fact(DisplayName = "ValueTask<ValueOptional<T>>.MatchAsync of None should invoke the none delegate (Func<T, ValueTask>, Action).")]
    public async Task ValueTaskValueOptionalMatchAsyncOfNoneShouldInvokeNoneDelegateFuncTTaskActionWithValueTaskDelegates()
    {
        // Given
        ValueOptional<int> optional = ValueOptional<int>.None;
        int someValue = 0;
        bool noneCalled = false;

        // When
        await ValueTask.FromResult(optional).MatchAsync(some: value => { someValue = value; return ValueTask.CompletedTask; }, none: () => noneCalled = true, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(0, someValue);
        Assert.True(noneCalled);
    }

    [Fact(DisplayName = "ValueTask<ValueOptional<T>>.MatchAsync of Some should invoke the some delegate (Func<T, CancellationToken, Task>, Action).")]
    public async Task ValueTaskValueOptionalMatchAsyncOfSomeShouldInvokeSomeDelegateFuncTCancellationTokenTaskAction()
    {
        // Given
        ValueOptional<int> optional = 21;
        int someValue = 0;
        bool noneCalled = false;
        CancellationToken someToken = CancellationToken.None;

        // When
        await ValueTask.FromResult(optional).MatchAsync(some: (value, ct) => { someValue = value; someToken = ct; return Task.CompletedTask; }, none: () => noneCalled = true, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(21, someValue);
        Assert.False(noneCalled);
        Assert.Equal(TestContext.Current.CancellationToken, someToken);
    }

    [Fact(DisplayName = "ValueTask<ValueOptional<T>>.MatchAsync of Some should invoke the some delegate (Func<T, CancellationToken, ValueTask>, Action).")]
    public async Task ValueTaskValueOptionalMatchAsyncOfSomeShouldInvokeSomeDelegateFuncTCancellationTokenTaskActionWithValueTaskDelegates()
    {
        // Given
        ValueOptional<int> optional = 21;
        int someValue = 0;
        bool noneCalled = false;
        CancellationToken someToken = CancellationToken.None;

        // When
        await ValueTask.FromResult(optional).MatchAsync(some: (value, ct) => { someValue = value; someToken = ct; return ValueTask.CompletedTask; }, none: () => noneCalled = true, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(21, someValue);
        Assert.False(noneCalled);
        Assert.Equal(TestContext.Current.CancellationToken, someToken);
    }

    [Fact(DisplayName = "ValueTask<ValueOptional<T>>.MatchAsync of None should invoke the none delegate (Func<T, CancellationToken, Task>, Action).")]
    public async Task ValueTaskValueOptionalMatchAsyncOfNoneShouldInvokeNoneDelegateFuncTCancellationTokenTaskAction()
    {
        // Given
        ValueOptional<int> optional = ValueOptional<int>.None;
        int someValue = 0;
        bool noneCalled = false;
        CancellationToken someToken = CancellationToken.None;

        // When
        await ValueTask.FromResult(optional).MatchAsync(some: (value, ct) => { someValue = value; someToken = ct; return Task.CompletedTask; }, none: () => noneCalled = true, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(0, someValue);
        Assert.True(noneCalled);
    }

    [Fact(DisplayName = "ValueTask<ValueOptional<T>>.MatchAsync of None should invoke the none delegate (Func<T, CancellationToken, ValueTask>, Action).")]
    public async Task ValueTaskValueOptionalMatchAsyncOfNoneShouldInvokeNoneDelegateFuncTCancellationTokenTaskActionWithValueTaskDelegates()
    {
        // Given
        ValueOptional<int> optional = ValueOptional<int>.None;
        int someValue = 0;
        bool noneCalled = false;
        CancellationToken someToken = CancellationToken.None;

        // When
        await ValueTask.FromResult(optional).MatchAsync(some: (value, ct) => { someValue = value; someToken = ct; return ValueTask.CompletedTask; }, none: () => noneCalled = true, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(0, someValue);
        Assert.True(noneCalled);
    }

    [Fact(DisplayName = "ValueTask<ValueOptional<T>>.MatchAsync of Some should invoke the some delegate (Action<T>, Func<Task>).")]
    public async Task ValueTaskValueOptionalMatchAsyncOfSomeShouldInvokeSomeDelegateActionTFuncTask()
    {
        // Given
        ValueOptional<int> optional = 21;
        int someValue = 0;
        bool noneCalled = false;

        // When
        await ValueTask.FromResult(optional).MatchAsync(some: value => someValue = value, none: () => { noneCalled = true; return Task.CompletedTask; }, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(21, someValue);
        Assert.False(noneCalled);
    }

    [Fact(DisplayName = "ValueTask<ValueOptional<T>>.MatchAsync of Some should invoke the some delegate (Action<T>, Func<ValueTask>).")]
    public async Task ValueTaskValueOptionalMatchAsyncOfSomeShouldInvokeSomeDelegateActionTFuncTaskWithValueTaskDelegates()
    {
        // Given
        ValueOptional<int> optional = 21;
        int someValue = 0;
        bool noneCalled = false;

        // When
        await ValueTask.FromResult(optional).MatchAsync(some: value => someValue = value, none: () => { noneCalled = true; return ValueTask.CompletedTask; }, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(21, someValue);
        Assert.False(noneCalled);
    }

    [Fact(DisplayName = "ValueTask<ValueOptional<T>>.MatchAsync of None should invoke the none delegate (Action<T>, Func<Task>).")]
    public async Task ValueTaskValueOptionalMatchAsyncOfNoneShouldInvokeNoneDelegateActionTFuncTask()
    {
        // Given
        ValueOptional<int> optional = ValueOptional<int>.None;
        int someValue = 0;
        bool noneCalled = false;

        // When
        await ValueTask.FromResult(optional).MatchAsync(some: value => someValue = value, none: () => { noneCalled = true; return Task.CompletedTask; }, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(0, someValue);
        Assert.True(noneCalled);
    }

    [Fact(DisplayName = "ValueTask<ValueOptional<T>>.MatchAsync of None should invoke the none delegate (Action<T>, Func<ValueTask>).")]
    public async Task ValueTaskValueOptionalMatchAsyncOfNoneShouldInvokeNoneDelegateActionTFuncTaskWithValueTaskDelegates()
    {
        // Given
        ValueOptional<int> optional = ValueOptional<int>.None;
        int someValue = 0;
        bool noneCalled = false;

        // When
        await ValueTask.FromResult(optional).MatchAsync(some: value => someValue = value, none: () => { noneCalled = true; return ValueTask.CompletedTask; }, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(0, someValue);
        Assert.True(noneCalled);
    }

    [Fact(DisplayName = "ValueTask<ValueOptional<T>>.MatchAsync of Some should invoke the some delegate (Action<T>, Func<CancellationToken, Task>).")]
    public async Task ValueTaskValueOptionalMatchAsyncOfSomeShouldInvokeSomeDelegateActionTFuncCancellationTokenTask()
    {
        // Given
        ValueOptional<int> optional = 21;
        int someValue = 0;
        bool noneCalled = false;
        CancellationToken noneToken = CancellationToken.None;

        // When
        await ValueTask.FromResult(optional).MatchAsync(some: value => someValue = value, none: ct => { noneCalled = true; noneToken = ct; return Task.CompletedTask; }, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(21, someValue);
        Assert.False(noneCalled);
    }

    [Fact(DisplayName = "ValueTask<ValueOptional<T>>.MatchAsync of Some should invoke the some delegate (Action<T>, Func<CancellationToken, ValueTask>).")]
    public async Task ValueTaskValueOptionalMatchAsyncOfSomeShouldInvokeSomeDelegateActionTFuncCancellationTokenTaskWithValueTaskDelegates()
    {
        // Given
        ValueOptional<int> optional = 21;
        int someValue = 0;
        bool noneCalled = false;
        CancellationToken noneToken = CancellationToken.None;

        // When
        await ValueTask.FromResult(optional).MatchAsync(some: value => someValue = value, none: ct => { noneCalled = true; noneToken = ct; return ValueTask.CompletedTask; }, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(21, someValue);
        Assert.False(noneCalled);
    }

    [Fact(DisplayName = "ValueTask<ValueOptional<T>>.MatchAsync of None should invoke the none delegate (Action<T>, Func<CancellationToken, Task>).")]
    public async Task ValueTaskValueOptionalMatchAsyncOfNoneShouldInvokeNoneDelegateActionTFuncCancellationTokenTask()
    {
        // Given
        ValueOptional<int> optional = ValueOptional<int>.None;
        int someValue = 0;
        bool noneCalled = false;
        CancellationToken noneToken = CancellationToken.None;

        // When
        await ValueTask.FromResult(optional).MatchAsync(some: value => someValue = value, none: ct => { noneCalled = true; noneToken = ct; return Task.CompletedTask; }, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(0, someValue);
        Assert.True(noneCalled);
        Assert.Equal(TestContext.Current.CancellationToken, noneToken);
    }

    [Fact(DisplayName = "ValueTask<ValueOptional<T>>.MatchAsync of None should invoke the none delegate (Action<T>, Func<CancellationToken, ValueTask>).")]
    public async Task ValueTaskValueOptionalMatchAsyncOfNoneShouldInvokeNoneDelegateActionTFuncCancellationTokenTaskWithValueTaskDelegates()
    {
        // Given
        ValueOptional<int> optional = ValueOptional<int>.None;
        int someValue = 0;
        bool noneCalled = false;
        CancellationToken noneToken = CancellationToken.None;

        // When
        await ValueTask.FromResult(optional).MatchAsync(some: value => someValue = value, none: ct => { noneCalled = true; noneToken = ct; return ValueTask.CompletedTask; }, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(0, someValue);
        Assert.True(noneCalled);
        Assert.Equal(TestContext.Current.CancellationToken, noneToken);
    }

    [Fact(DisplayName = "ValueTask<ValueOptional<T>>.MatchAsync of Some should invoke the some delegate (Func<T, Task>, Func<Task>).")]
    public async Task ValueTaskValueOptionalMatchAsyncOfSomeShouldInvokeSomeDelegateFuncTTaskFuncTask()
    {
        // Given
        ValueOptional<int> optional = 21;
        int someValue = 0;
        bool noneCalled = false;

        // When
        await ValueTask.FromResult(optional).MatchAsync(some: value => { someValue = value; return Task.CompletedTask; }, none: () => { noneCalled = true; return Task.CompletedTask; }, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(21, someValue);
        Assert.False(noneCalled);
    }

    [Fact(DisplayName = "ValueTask<ValueOptional<T>>.MatchAsync of Some should invoke the some delegate (Func<T, ValueTask>, Func<ValueTask>).")]
    public async Task ValueTaskValueOptionalMatchAsyncOfSomeShouldInvokeSomeDelegateFuncTTaskFuncTaskWithValueTaskDelegates()
    {
        // Given
        ValueOptional<int> optional = 21;
        int someValue = 0;
        bool noneCalled = false;

        // When
        await ValueTask.FromResult(optional).MatchAsync(some: value => { someValue = value; return ValueTask.CompletedTask; }, none: () => { noneCalled = true; return ValueTask.CompletedTask; }, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(21, someValue);
        Assert.False(noneCalled);
    }

    [Fact(DisplayName = "ValueTask<ValueOptional<T>>.MatchAsync of None should invoke the none delegate (Func<T, Task>, Func<Task>).")]
    public async Task ValueTaskValueOptionalMatchAsyncOfNoneShouldInvokeNoneDelegateFuncTTaskFuncTask()
    {
        // Given
        ValueOptional<int> optional = ValueOptional<int>.None;
        int someValue = 0;
        bool noneCalled = false;

        // When
        await ValueTask.FromResult(optional).MatchAsync(some: value => { someValue = value; return Task.CompletedTask; }, none: () => { noneCalled = true; return Task.CompletedTask; }, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(0, someValue);
        Assert.True(noneCalled);
    }

    [Fact(DisplayName = "ValueTask<ValueOptional<T>>.MatchAsync of None should invoke the none delegate (Func<T, ValueTask>, Func<ValueTask>).")]
    public async Task ValueTaskValueOptionalMatchAsyncOfNoneShouldInvokeNoneDelegateFuncTTaskFuncTaskWithValueTaskDelegates()
    {
        // Given
        ValueOptional<int> optional = ValueOptional<int>.None;
        int someValue = 0;
        bool noneCalled = false;

        // When
        await ValueTask.FromResult(optional).MatchAsync(some: value => { someValue = value; return ValueTask.CompletedTask; }, none: () => { noneCalled = true; return ValueTask.CompletedTask; }, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(0, someValue);
        Assert.True(noneCalled);
    }

    [Fact(DisplayName = "ValueTask<ValueOptional<T>>.MatchAsync of Some should invoke the some delegate (Func<T, CancellationToken, Task>, Func<CancellationToken, Task>).")]
    public async Task ValueTaskValueOptionalMatchAsyncOfSomeShouldInvokeSomeDelegateFuncTCancellationTokenTaskFuncCancellationTokenTask()
    {
        // Given
        ValueOptional<int> optional = 21;
        int someValue = 0;
        bool noneCalled = false;
        CancellationToken someToken = CancellationToken.None;
        CancellationToken noneToken = CancellationToken.None;

        // When
        await ValueTask.FromResult(optional).MatchAsync(some: (value, ct) => { someValue = value; someToken = ct; return Task.CompletedTask; }, none: ct => { noneCalled = true; noneToken = ct; return Task.CompletedTask; }, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(21, someValue);
        Assert.False(noneCalled);
        Assert.Equal(TestContext.Current.CancellationToken, someToken);
    }

    [Fact(DisplayName = "ValueTask<ValueOptional<T>>.MatchAsync of Some should invoke the some delegate (Func<T, CancellationToken, ValueTask>, Func<CancellationToken, ValueTask>).")]
    public async Task ValueTaskValueOptionalMatchAsyncOfSomeShouldInvokeSomeDelegateFuncTCancellationTokenTaskFuncCancellationTokenTaskWithValueTaskDelegates()
    {
        // Given
        ValueOptional<int> optional = 21;
        int someValue = 0;
        bool noneCalled = false;
        CancellationToken someToken = CancellationToken.None;
        CancellationToken noneToken = CancellationToken.None;

        // When
        await ValueTask.FromResult(optional).MatchAsync(some: (value, ct) => { someValue = value; someToken = ct; return ValueTask.CompletedTask; }, none: ct => { noneCalled = true; noneToken = ct; return ValueTask.CompletedTask; }, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(21, someValue);
        Assert.False(noneCalled);
        Assert.Equal(TestContext.Current.CancellationToken, someToken);
    }

    [Fact(DisplayName = "ValueTask<ValueOptional<T>>.MatchAsync of None should invoke the none delegate (Func<T, CancellationToken, Task>, Func<CancellationToken, Task>).")]
    public async Task ValueTaskValueOptionalMatchAsyncOfNoneShouldInvokeNoneDelegateFuncTCancellationTokenTaskFuncCancellationTokenTask()
    {
        // Given
        ValueOptional<int> optional = ValueOptional<int>.None;
        int someValue = 0;
        bool noneCalled = false;
        CancellationToken someToken = CancellationToken.None;
        CancellationToken noneToken = CancellationToken.None;

        // When
        await ValueTask.FromResult(optional).MatchAsync(some: (value, ct) => { someValue = value; someToken = ct; return Task.CompletedTask; }, none: ct => { noneCalled = true; noneToken = ct; return Task.CompletedTask; }, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(0, someValue);
        Assert.True(noneCalled);
        Assert.Equal(TestContext.Current.CancellationToken, noneToken);
    }

    [Fact(DisplayName = "ValueTask<ValueOptional<T>>.MatchAsync of None should invoke the none delegate (Func<T, CancellationToken, ValueTask>, Func<CancellationToken, ValueTask>).")]
    public async Task ValueTaskValueOptionalMatchAsyncOfNoneShouldInvokeNoneDelegateFuncTCancellationTokenTaskFuncCancellationTokenTaskWithValueTaskDelegates()
    {
        // Given
        ValueOptional<int> optional = ValueOptional<int>.None;
        int someValue = 0;
        bool noneCalled = false;
        CancellationToken someToken = CancellationToken.None;
        CancellationToken noneToken = CancellationToken.None;

        // When
        await ValueTask.FromResult(optional).MatchAsync(some: (value, ct) => { someValue = value; someToken = ct; return ValueTask.CompletedTask; }, none: ct => { noneCalled = true; noneToken = ct; return ValueTask.CompletedTask; }, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(0, someValue);
        Assert.True(noneCalled);
        Assert.Equal(TestContext.Current.CancellationToken, noneToken);
    }

    [Fact(DisplayName = "ValueTask<ValueOptional<T>>.MatchAsync<TResult> of Some should return the result of the some delegate (Func<T, TResult>, Func<TResult>).")]
    public async Task ValueTaskValueOptionalMatchAsyncTResultOfSomeShouldReturnResultOfSomeDelegateFuncTTResultFuncTResult()
    {
        // Given
        ValueOptional<int> optional = 21;

        // When
        string actual = await ValueTask.FromResult(optional).MatchAsync(some: value => $"Some {value}", none: () => "None", cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal("Some 21", actual);
    }

    [Fact(DisplayName = "ValueTask<ValueOptional<T>>.MatchAsync<TResult> of None should return the result of the none delegate (Func<T, TResult>, Func<TResult>).")]
    public async Task ValueTaskValueOptionalMatchAsyncTResultOfNoneShouldReturnResultOfNoneDelegateFuncTTResultFuncTResult()
    {
        // Given
        ValueOptional<int> optional = ValueOptional<int>.None;

        // When
        string actual = await ValueTask.FromResult(optional).MatchAsync(some: value => $"Some {value}", none: () => "None", cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal("None", actual);
    }

    [Fact(DisplayName = "ValueTask<ValueOptional<T>>.MatchAsync<TResult> of Some should return the result of the some delegate (Func<T, Task<TResult>>, Func<TResult>).")]
    public async Task ValueTaskValueOptionalMatchAsyncTResultOfSomeShouldReturnResultOfSomeDelegateFuncTTaskTResultFuncTResult()
    {
        // Given
        ValueOptional<int> optional = 21;

        // When
        string actual = await ValueTask.FromResult(optional).MatchAsync(some: value => Task.FromResult($"Some {value}"), none: () => "None", cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal("Some 21", actual);
    }

    [Fact(DisplayName = "ValueTask<ValueOptional<T>>.MatchAsync<TResult> of Some should return the result of the some delegate (Func<T, ValueTask<TResult>>, Func<TResult>).")]
    public async Task ValueTaskValueOptionalMatchAsyncTResultOfSomeShouldReturnResultOfSomeDelegateFuncTTaskTResultFuncTResultWithValueTaskDelegates()
    {
        // Given
        ValueOptional<int> optional = 21;

        // When
        string actual = await ValueTask.FromResult(optional).MatchAsync(some: value => ValueTask.FromResult($"Some {value}"), none: () => "None", cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal("Some 21", actual);
    }

    [Fact(DisplayName = "ValueTask<ValueOptional<T>>.MatchAsync<TResult> of None should return the result of the none delegate (Func<T, Task<TResult>>, Func<TResult>).")]
    public async Task ValueTaskValueOptionalMatchAsyncTResultOfNoneShouldReturnResultOfNoneDelegateFuncTTaskTResultFuncTResult()
    {
        // Given
        ValueOptional<int> optional = ValueOptional<int>.None;

        // When
        string actual = await ValueTask.FromResult(optional).MatchAsync(some: value => Task.FromResult($"Some {value}"), none: () => "None", cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal("None", actual);
    }

    [Fact(DisplayName = "ValueTask<ValueOptional<T>>.MatchAsync<TResult> of None should return the result of the none delegate (Func<T, ValueTask<TResult>>, Func<TResult>).")]
    public async Task ValueTaskValueOptionalMatchAsyncTResultOfNoneShouldReturnResultOfNoneDelegateFuncTTaskTResultFuncTResultWithValueTaskDelegates()
    {
        // Given
        ValueOptional<int> optional = ValueOptional<int>.None;

        // When
        string actual = await ValueTask.FromResult(optional).MatchAsync(some: value => ValueTask.FromResult($"Some {value}"), none: () => "None", cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal("None", actual);
    }

    [Fact(DisplayName = "ValueTask<ValueOptional<T>>.MatchAsync<TResult> of Some should return the result of the some delegate (Func<T, CancellationToken, Task<TResult>>, Func<TResult>).")]
    public async Task ValueTaskValueOptionalMatchAsyncTResultOfSomeShouldReturnResultOfSomeDelegateFuncTCancellationTokenTaskTResultFuncTResult()
    {
        // Given
        ValueOptional<int> optional = 21;
        CancellationToken someToken = CancellationToken.None;

        // When
        string actual = await ValueTask.FromResult(optional).MatchAsync(some: (value, ct) => { someToken = ct; return Task.FromResult($"Some {value}"); }, none: () => "None", cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal("Some 21", actual);
        Assert.Equal(TestContext.Current.CancellationToken, someToken);
    }

    [Fact(DisplayName = "ValueTask<ValueOptional<T>>.MatchAsync<TResult> of Some should return the result of the some delegate (Func<T, CancellationToken, ValueTask<TResult>>, Func<TResult>).")]
    public async Task ValueTaskValueOptionalMatchAsyncTResultOfSomeShouldReturnResultOfSomeDelegateFuncTCancellationTokenTaskTResultFuncTResultWithValueTaskDelegates()
    {
        // Given
        ValueOptional<int> optional = 21;
        CancellationToken someToken = CancellationToken.None;

        // When
        string actual = await ValueTask.FromResult(optional).MatchAsync(some: (value, ct) => { someToken = ct; return ValueTask.FromResult($"Some {value}"); }, none: () => "None", cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal("Some 21", actual);
        Assert.Equal(TestContext.Current.CancellationToken, someToken);
    }

    [Fact(DisplayName = "ValueTask<ValueOptional<T>>.MatchAsync<TResult> of None should return the result of the none delegate (Func<T, CancellationToken, Task<TResult>>, Func<TResult>).")]
    public async Task ValueTaskValueOptionalMatchAsyncTResultOfNoneShouldReturnResultOfNoneDelegateFuncTCancellationTokenTaskTResultFuncTResult()
    {
        // Given
        ValueOptional<int> optional = ValueOptional<int>.None;
        CancellationToken someToken = CancellationToken.None;

        // When
        string actual = await ValueTask.FromResult(optional).MatchAsync(some: (value, ct) => { someToken = ct; return Task.FromResult($"Some {value}"); }, none: () => "None", cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal("None", actual);
    }

    [Fact(DisplayName = "ValueTask<ValueOptional<T>>.MatchAsync<TResult> of None should return the result of the none delegate (Func<T, CancellationToken, ValueTask<TResult>>, Func<TResult>).")]
    public async Task ValueTaskValueOptionalMatchAsyncTResultOfNoneShouldReturnResultOfNoneDelegateFuncTCancellationTokenTaskTResultFuncTResultWithValueTaskDelegates()
    {
        // Given
        ValueOptional<int> optional = ValueOptional<int>.None;
        CancellationToken someToken = CancellationToken.None;

        // When
        string actual = await ValueTask.FromResult(optional).MatchAsync(some: (value, ct) => { someToken = ct; return ValueTask.FromResult($"Some {value}"); }, none: () => "None", cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal("None", actual);
    }

    [Fact(DisplayName = "ValueTask<ValueOptional<T>>.MatchAsync<TResult> of Some should return the result of the some delegate (Func<T, TResult>, Func<Task<TResult>>).")]
    public async Task ValueTaskValueOptionalMatchAsyncTResultOfSomeShouldReturnResultOfSomeDelegateFuncTTResultFuncTaskTResult()
    {
        // Given
        ValueOptional<int> optional = 21;

        // When
        string actual = await ValueTask.FromResult(optional).MatchAsync(some: value => $"Some {value}", none: () => Task.FromResult("None"), cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal("Some 21", actual);
    }

    [Fact(DisplayName = "ValueTask<ValueOptional<T>>.MatchAsync<TResult> of Some should return the result of the some delegate (Func<T, TResult>, Func<ValueTask<TResult>>).")]
    public async Task ValueTaskValueOptionalMatchAsyncTResultOfSomeShouldReturnResultOfSomeDelegateFuncTTResultFuncTaskTResultWithValueTaskDelegates()
    {
        // Given
        ValueOptional<int> optional = 21;

        // When
        string actual = await ValueTask.FromResult(optional).MatchAsync(some: value => $"Some {value}", none: () => ValueTask.FromResult("None"), cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal("Some 21", actual);
    }

    [Fact(DisplayName = "ValueTask<ValueOptional<T>>.MatchAsync<TResult> of None should return the result of the none delegate (Func<T, TResult>, Func<Task<TResult>>).")]
    public async Task ValueTaskValueOptionalMatchAsyncTResultOfNoneShouldReturnResultOfNoneDelegateFuncTTResultFuncTaskTResult()
    {
        // Given
        ValueOptional<int> optional = ValueOptional<int>.None;

        // When
        string actual = await ValueTask.FromResult(optional).MatchAsync(some: value => $"Some {value}", none: () => Task.FromResult("None"), cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal("None", actual);
    }

    [Fact(DisplayName = "ValueTask<ValueOptional<T>>.MatchAsync<TResult> of None should return the result of the none delegate (Func<T, TResult>, Func<ValueTask<TResult>>).")]
    public async Task ValueTaskValueOptionalMatchAsyncTResultOfNoneShouldReturnResultOfNoneDelegateFuncTTResultFuncTaskTResultWithValueTaskDelegates()
    {
        // Given
        ValueOptional<int> optional = ValueOptional<int>.None;

        // When
        string actual = await ValueTask.FromResult(optional).MatchAsync(some: value => $"Some {value}", none: () => ValueTask.FromResult("None"), cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal("None", actual);
    }

    [Fact(DisplayName = "ValueTask<ValueOptional<T>>.MatchAsync<TResult> of Some should return the result of the some delegate (Func<T, TResult>, Func<CancellationToken, Task<TResult>>).")]
    public async Task ValueTaskValueOptionalMatchAsyncTResultOfSomeShouldReturnResultOfSomeDelegateFuncTTResultFuncCancellationTokenTaskTResult()
    {
        // Given
        ValueOptional<int> optional = 21;
        CancellationToken noneToken = CancellationToken.None;

        // When
        string actual = await ValueTask.FromResult(optional).MatchAsync(some: value => $"Some {value}", none: ct => { noneToken = ct; return Task.FromResult("None"); }, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal("Some 21", actual);
    }

    [Fact(DisplayName = "ValueTask<ValueOptional<T>>.MatchAsync<TResult> of Some should return the result of the some delegate (Func<T, TResult>, Func<CancellationToken, ValueTask<TResult>>).")]
    public async Task ValueTaskValueOptionalMatchAsyncTResultOfSomeShouldReturnResultOfSomeDelegateFuncTTResultFuncCancellationTokenTaskTResultWithValueTaskDelegates()
    {
        // Given
        ValueOptional<int> optional = 21;
        CancellationToken noneToken = CancellationToken.None;

        // When
        string actual = await ValueTask.FromResult(optional).MatchAsync(some: value => $"Some {value}", none: ct => { noneToken = ct; return ValueTask.FromResult("None"); }, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal("Some 21", actual);
    }

    [Fact(DisplayName = "ValueTask<ValueOptional<T>>.MatchAsync<TResult> of None should return the result of the none delegate (Func<T, TResult>, Func<CancellationToken, Task<TResult>>).")]
    public async Task ValueTaskValueOptionalMatchAsyncTResultOfNoneShouldReturnResultOfNoneDelegateFuncTTResultFuncCancellationTokenTaskTResult()
    {
        // Given
        ValueOptional<int> optional = ValueOptional<int>.None;
        CancellationToken noneToken = CancellationToken.None;

        // When
        string actual = await ValueTask.FromResult(optional).MatchAsync(some: value => $"Some {value}", none: ct => { noneToken = ct; return Task.FromResult("None"); }, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal("None", actual);
        Assert.Equal(TestContext.Current.CancellationToken, noneToken);
    }

    [Fact(DisplayName = "ValueTask<ValueOptional<T>>.MatchAsync<TResult> of None should return the result of the none delegate (Func<T, TResult>, Func<CancellationToken, ValueTask<TResult>>).")]
    public async Task ValueTaskValueOptionalMatchAsyncTResultOfNoneShouldReturnResultOfNoneDelegateFuncTTResultFuncCancellationTokenTaskTResultWithValueTaskDelegates()
    {
        // Given
        ValueOptional<int> optional = ValueOptional<int>.None;
        CancellationToken noneToken = CancellationToken.None;

        // When
        string actual = await ValueTask.FromResult(optional).MatchAsync(some: value => $"Some {value}", none: ct => { noneToken = ct; return ValueTask.FromResult("None"); }, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal("None", actual);
        Assert.Equal(TestContext.Current.CancellationToken, noneToken);
    }

    [Fact(DisplayName = "ValueTask<ValueOptional<T>>.MatchAsync<TResult> of Some should return the result of the some delegate (Func<T, Task<TResult>>, Func<Task<TResult>>).")]
    public async Task ValueTaskValueOptionalMatchAsyncTResultOfSomeShouldReturnResultOfSomeDelegateFuncTTaskTResultFuncTaskTResult()
    {
        // Given
        ValueOptional<int> optional = 21;

        // When
        string actual = await ValueTask.FromResult(optional).MatchAsync(some: value => Task.FromResult($"Some {value}"), none: () => Task.FromResult("None"), cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal("Some 21", actual);
    }

    [Fact(DisplayName = "ValueTask<ValueOptional<T>>.MatchAsync<TResult> of Some should return the result of the some delegate (Func<T, ValueTask<TResult>>, Func<ValueTask<TResult>>).")]
    public async Task ValueTaskValueOptionalMatchAsyncTResultOfSomeShouldReturnResultOfSomeDelegateFuncTTaskTResultFuncTaskTResultWithValueTaskDelegates()
    {
        // Given
        ValueOptional<int> optional = 21;

        // When
        string actual = await ValueTask.FromResult(optional).MatchAsync(some: value => ValueTask.FromResult($"Some {value}"), none: () => ValueTask.FromResult("None"), cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal("Some 21", actual);
    }

    [Fact(DisplayName = "ValueTask<ValueOptional<T>>.MatchAsync<TResult> of None should return the result of the none delegate (Func<T, Task<TResult>>, Func<Task<TResult>>).")]
    public async Task ValueTaskValueOptionalMatchAsyncTResultOfNoneShouldReturnResultOfNoneDelegateFuncTTaskTResultFuncTaskTResult()
    {
        // Given
        ValueOptional<int> optional = ValueOptional<int>.None;

        // When
        string actual = await ValueTask.FromResult(optional).MatchAsync(some: value => Task.FromResult($"Some {value}"), none: () => Task.FromResult("None"), cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal("None", actual);
    }

    [Fact(DisplayName = "ValueTask<ValueOptional<T>>.MatchAsync<TResult> of None should return the result of the none delegate (Func<T, ValueTask<TResult>>, Func<ValueTask<TResult>>).")]
    public async Task ValueTaskValueOptionalMatchAsyncTResultOfNoneShouldReturnResultOfNoneDelegateFuncTTaskTResultFuncTaskTResultWithValueTaskDelegates()
    {
        // Given
        ValueOptional<int> optional = ValueOptional<int>.None;

        // When
        string actual = await ValueTask.FromResult(optional).MatchAsync(some: value => ValueTask.FromResult($"Some {value}"), none: () => ValueTask.FromResult("None"), cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal("None", actual);
    }

    [Fact(DisplayName = "ValueTask<ValueOptional<T>>.MatchAsync<TResult> of Some should return the result of the some delegate (Func<T, CancellationToken, Task<TResult>>, Func<CancellationToken, Task<TResult>>).")]
    public async Task ValueTaskValueOptionalMatchAsyncTResultOfSomeShouldReturnResultOfSomeDelegateFuncTCancellationTokenTaskTResultFuncCancellationTokenTaskTResult()
    {
        // Given
        ValueOptional<int> optional = 21;
        CancellationToken someToken = CancellationToken.None;
        CancellationToken noneToken = CancellationToken.None;

        // When
        string actual = await ValueTask.FromResult(optional).MatchAsync(some: (value, ct) => { someToken = ct; return Task.FromResult($"Some {value}"); }, none: ct => { noneToken = ct; return Task.FromResult("None"); }, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal("Some 21", actual);
        Assert.Equal(TestContext.Current.CancellationToken, someToken);
    }

    [Fact(DisplayName = "ValueTask<ValueOptional<T>>.MatchAsync<TResult> of Some should return the result of the some delegate (Func<T, CancellationToken, ValueTask<TResult>>, Func<CancellationToken, ValueTask<TResult>>).")]
    public async Task ValueTaskValueOptionalMatchAsyncTResultOfSomeShouldReturnResultOfSomeDelegateFuncTCancellationTokenTaskTResultFuncCancellationTokenTaskTResultWithValueTaskDelegates()
    {
        // Given
        ValueOptional<int> optional = 21;
        CancellationToken someToken = CancellationToken.None;
        CancellationToken noneToken = CancellationToken.None;

        // When
        string actual = await ValueTask.FromResult(optional).MatchAsync(some: (value, ct) => { someToken = ct; return ValueTask.FromResult($"Some {value}"); }, none: ct => { noneToken = ct; return ValueTask.FromResult("None"); }, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal("Some 21", actual);
        Assert.Equal(TestContext.Current.CancellationToken, someToken);
    }

    [Fact(DisplayName = "ValueTask<ValueOptional<T>>.MatchAsync<TResult> of None should return the result of the none delegate (Func<T, CancellationToken, Task<TResult>>, Func<CancellationToken, Task<TResult>>).")]
    public async Task ValueTaskValueOptionalMatchAsyncTResultOfNoneShouldReturnResultOfNoneDelegateFuncTCancellationTokenTaskTResultFuncCancellationTokenTaskTResult()
    {
        // Given
        ValueOptional<int> optional = ValueOptional<int>.None;
        CancellationToken someToken = CancellationToken.None;
        CancellationToken noneToken = CancellationToken.None;

        // When
        string actual = await ValueTask.FromResult(optional).MatchAsync(some: (value, ct) => { someToken = ct; return Task.FromResult($"Some {value}"); }, none: ct => { noneToken = ct; return Task.FromResult("None"); }, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal("None", actual);
        Assert.Equal(TestContext.Current.CancellationToken, noneToken);
    }

    [Fact(DisplayName = "ValueTask<ValueOptional<T>>.MatchAsync<TResult> of None should return the result of the none delegate (Func<T, CancellationToken, ValueTask<TResult>>, Func<CancellationToken, ValueTask<TResult>>).")]
    public async Task ValueTaskValueOptionalMatchAsyncTResultOfNoneShouldReturnResultOfNoneDelegateFuncTCancellationTokenTaskTResultFuncCancellationTokenTaskTResultWithValueTaskDelegates()
    {
        // Given
        ValueOptional<int> optional = ValueOptional<int>.None;
        CancellationToken someToken = CancellationToken.None;
        CancellationToken noneToken = CancellationToken.None;

        // When
        string actual = await ValueTask.FromResult(optional).MatchAsync(some: (value, ct) => { someToken = ct; return ValueTask.FromResult($"Some {value}"); }, none: ct => { noneToken = ct; return ValueTask.FromResult("None"); }, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal("None", actual);
        Assert.Equal(TestContext.Current.CancellationToken, noneToken);
    }

    [Fact(DisplayName = "ValueTask<ValueOptional<T>>.SelectAsync of Some should produce the expected result (Func<T, TResult>).")]
    public async Task ValueTaskValueOptionalSelectAsyncOfSomeShouldProduceExpectedResultFuncTTResult()
    {
        // Given
        ValueOptional<int> optional = 21;

        // When
        ValueOptional<int> actual = await ValueTask.FromResult(optional).SelectAsync(value => value * 2, TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(ValueOptional<int>.Some(42), actual);
    }

    [Fact(DisplayName = "ValueTask<ValueOptional<T>>.SelectAsync of None should produce the expected result (Func<T, TResult>).")]
    public async Task ValueTaskValueOptionalSelectAsyncOfNoneShouldProduceExpectedResultFuncTTResult()
    {
        // Given
        ValueOptional<int> optional = ValueOptional<int>.None;

        // When
        ValueOptional<int> actual = await ValueTask.FromResult(optional).SelectAsync(value => value * 2, TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(ValueOptional<int>.None, actual);
    }

    [Fact(DisplayName = "ValueTask<ValueOptional<T>>.SelectAsync of Some should produce the expected result (Func<T, Task<TResult>>).")]
    public async Task ValueTaskValueOptionalSelectAsyncOfSomeShouldProduceExpectedResultFuncTTaskTResult()
    {
        // Given
        ValueOptional<int> optional = 21;

        // When
        ValueOptional<int> actual = await ValueTask.FromResult(optional).SelectAsync(value => Task.FromResult(value * 2), TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(ValueOptional<int>.Some(42), actual);
    }

    [Fact(DisplayName = "ValueTask<ValueOptional<T>>.SelectAsync of Some should produce the expected result (Func<T, ValueTask<TResult>>).")]
    public async Task ValueTaskValueOptionalSelectAsyncOfSomeShouldProduceExpectedResultFuncTTaskTResultWithValueTaskDelegates()
    {
        // Given
        ValueOptional<int> optional = 21;

        // When
        ValueOptional<int> actual = await ValueTask.FromResult(optional).SelectAsync(value => ValueTask.FromResult(value * 2), TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(ValueOptional<int>.Some(42), actual);
    }

    [Fact(DisplayName = "ValueTask<ValueOptional<T>>.SelectAsync of None should produce the expected result (Func<T, Task<TResult>>).")]
    public async Task ValueTaskValueOptionalSelectAsyncOfNoneShouldProduceExpectedResultFuncTTaskTResult()
    {
        // Given
        ValueOptional<int> optional = ValueOptional<int>.None;

        // When
        ValueOptional<int> actual = await ValueTask.FromResult(optional).SelectAsync(value => Task.FromResult(value * 2), TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(ValueOptional<int>.None, actual);
    }

    [Fact(DisplayName = "ValueTask<ValueOptional<T>>.SelectAsync of None should produce the expected result (Func<T, ValueTask<TResult>>).")]
    public async Task ValueTaskValueOptionalSelectAsyncOfNoneShouldProduceExpectedResultFuncTTaskTResultWithValueTaskDelegates()
    {
        // Given
        ValueOptional<int> optional = ValueOptional<int>.None;

        // When
        ValueOptional<int> actual = await ValueTask.FromResult(optional).SelectAsync(value => ValueTask.FromResult(value * 2), TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(ValueOptional<int>.None, actual);
    }

    [Fact(DisplayName = "ValueTask<ValueOptional<T>>.SelectAsync of Some should produce the expected result (Func<T, CancellationToken, Task<TResult>>).")]
    public async Task ValueTaskValueOptionalSelectAsyncOfSomeShouldProduceExpectedResultFuncTCancellationTokenTaskTResult()
    {
        // Given
        ValueOptional<int> optional = 21;
        CancellationToken selectorToken = CancellationToken.None;

        // When
        ValueOptional<int> actual = await ValueTask.FromResult(optional).SelectAsync((value, ct) => { selectorToken = ct; return Task.FromResult(value * 2); }, TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(ValueOptional<int>.Some(42), actual);
        Assert.Equal(TestContext.Current.CancellationToken, selectorToken);
    }

    [Fact(DisplayName = "ValueTask<ValueOptional<T>>.SelectAsync of Some should produce the expected result (Func<T, CancellationToken, ValueTask<TResult>>).")]
    public async Task ValueTaskValueOptionalSelectAsyncOfSomeShouldProduceExpectedResultFuncTCancellationTokenTaskTResultWithValueTaskDelegates()
    {
        // Given
        ValueOptional<int> optional = 21;
        CancellationToken selectorToken = CancellationToken.None;

        // When
        ValueOptional<int> actual = await ValueTask.FromResult(optional).SelectAsync((value, ct) => { selectorToken = ct; return ValueTask.FromResult(value * 2); }, TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(ValueOptional<int>.Some(42), actual);
        Assert.Equal(TestContext.Current.CancellationToken, selectorToken);
    }

    [Fact(DisplayName = "ValueTask<ValueOptional<T>>.SelectAsync of None should produce the expected result (Func<T, CancellationToken, Task<TResult>>).")]
    public async Task ValueTaskValueOptionalSelectAsyncOfNoneShouldProduceExpectedResultFuncTCancellationTokenTaskTResult()
    {
        // Given
        ValueOptional<int> optional = ValueOptional<int>.None;
        CancellationToken selectorToken = CancellationToken.None;

        // When
        ValueOptional<int> actual = await ValueTask.FromResult(optional).SelectAsync((value, ct) => { selectorToken = ct; return Task.FromResult(value * 2); }, TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(ValueOptional<int>.None, actual);
    }

    [Fact(DisplayName = "ValueTask<ValueOptional<T>>.SelectAsync of None should produce the expected result (Func<T, CancellationToken, ValueTask<TResult>>).")]
    public async Task ValueTaskValueOptionalSelectAsyncOfNoneShouldProduceExpectedResultFuncTCancellationTokenTaskTResultWithValueTaskDelegates()
    {
        // Given
        ValueOptional<int> optional = ValueOptional<int>.None;
        CancellationToken selectorToken = CancellationToken.None;

        // When
        ValueOptional<int> actual = await ValueTask.FromResult(optional).SelectAsync((value, ct) => { selectorToken = ct; return ValueTask.FromResult(value * 2); }, TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(ValueOptional<int>.None, actual);
    }

    [Fact(DisplayName = "ValueTask<ValueOptional<T>>.SelectManyAsync of Some should produce the expected result (Func<T, ValueOptional<TResult>>).")]
    public async Task ValueTaskValueOptionalSelectManyAsyncOfSomeShouldProduceExpectedResultFuncTValueOptionalTResult()
    {
        // Given
        ValueOptional<int> optional = 21;

        // When
        ValueOptional<int> actual = await ValueTask.FromResult(optional).SelectManyAsync(value => ValueOptional<int>.Some(value * 2), TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(ValueOptional<int>.Some(42), actual);
    }

    [Fact(DisplayName = "ValueTask<ValueOptional<T>>.SelectManyAsync of None should produce the expected result (Func<T, ValueOptional<TResult>>).")]
    public async Task ValueTaskValueOptionalSelectManyAsyncOfNoneShouldProduceExpectedResultFuncTValueOptionalTResult()
    {
        // Given
        ValueOptional<int> optional = ValueOptional<int>.None;

        // When
        ValueOptional<int> actual = await ValueTask.FromResult(optional).SelectManyAsync(value => ValueOptional<int>.Some(value * 2), TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(ValueOptional<int>.None, actual);
    }

    [Fact(DisplayName = "ValueTask<ValueOptional<T>>.SelectManyAsync of Some should produce the expected result (Func<T, Task<ValueOptional<TResult>>>).")]
    public async Task ValueTaskValueOptionalSelectManyAsyncOfSomeShouldProduceExpectedResultFuncTTaskValueOptionalTResult()
    {
        // Given
        ValueOptional<int> optional = 21;

        // When
        ValueOptional<int> actual = await ValueTask.FromResult(optional).SelectManyAsync(value => Task.FromResult<ValueOptional<int>>(value * 2), TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(ValueOptional<int>.Some(42), actual);
    }

    [Fact(DisplayName = "ValueTask<ValueOptional<T>>.SelectManyAsync of Some should produce the expected result (Func<T, ValueTask<ValueOptional<TResult>>>).")]
    public async Task ValueTaskValueOptionalSelectManyAsyncOfSomeShouldProduceExpectedResultFuncTTaskValueOptionalTResultWithValueTaskDelegates()
    {
        // Given
        ValueOptional<int> optional = 21;

        // When
        ValueOptional<int> actual = await ValueTask.FromResult(optional).SelectManyAsync(value => ValueTask.FromResult<ValueOptional<int>>(value * 2), TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(ValueOptional<int>.Some(42), actual);
    }

    [Fact(DisplayName = "ValueTask<ValueOptional<T>>.SelectManyAsync of None should produce the expected result (Func<T, Task<ValueOptional<TResult>>>).")]
    public async Task ValueTaskValueOptionalSelectManyAsyncOfNoneShouldProduceExpectedResultFuncTTaskValueOptionalTResult()
    {
        // Given
        ValueOptional<int> optional = ValueOptional<int>.None;

        // When
        ValueOptional<int> actual = await ValueTask.FromResult(optional).SelectManyAsync(value => Task.FromResult<ValueOptional<int>>(value * 2), TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(ValueOptional<int>.None, actual);
    }

    [Fact(DisplayName = "ValueTask<ValueOptional<T>>.SelectManyAsync of None should produce the expected result (Func<T, ValueTask<ValueOptional<TResult>>>).")]
    public async Task ValueTaskValueOptionalSelectManyAsyncOfNoneShouldProduceExpectedResultFuncTTaskValueOptionalTResultWithValueTaskDelegates()
    {
        // Given
        ValueOptional<int> optional = ValueOptional<int>.None;

        // When
        ValueOptional<int> actual = await ValueTask.FromResult(optional).SelectManyAsync(value => ValueTask.FromResult<ValueOptional<int>>(value * 2), TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(ValueOptional<int>.None, actual);
    }

    [Fact(DisplayName = "ValueTask<ValueOptional<T>>.SelectManyAsync of Some should produce the expected result (Func<T, CancellationToken, Task<ValueOptional<TResult>>>).")]
    public async Task ValueTaskValueOptionalSelectManyAsyncOfSomeShouldProduceExpectedResultFuncTCancellationTokenTaskValueOptionalTResult()
    {
        // Given
        ValueOptional<int> optional = 21;
        CancellationToken selectorToken = CancellationToken.None;

        // When
        ValueOptional<int> actual = await ValueTask.FromResult(optional).SelectManyAsync((value, ct) => { selectorToken = ct; return Task.FromResult<ValueOptional<int>>(value * 2); }, TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(ValueOptional<int>.Some(42), actual);
        Assert.Equal(TestContext.Current.CancellationToken, selectorToken);
    }

    [Fact(DisplayName = "ValueTask<ValueOptional<T>>.SelectManyAsync of Some should produce the expected result (Func<T, CancellationToken, ValueTask<ValueOptional<TResult>>>).")]
    public async Task ValueTaskValueOptionalSelectManyAsyncOfSomeShouldProduceExpectedResultFuncTCancellationTokenTaskValueOptionalTResultWithValueTaskDelegates()
    {
        // Given
        ValueOptional<int> optional = 21;
        CancellationToken selectorToken = CancellationToken.None;

        // When
        ValueOptional<int> actual = await ValueTask.FromResult(optional).SelectManyAsync((value, ct) => { selectorToken = ct; return ValueTask.FromResult<ValueOptional<int>>(value * 2); }, TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(ValueOptional<int>.Some(42), actual);
        Assert.Equal(TestContext.Current.CancellationToken, selectorToken);
    }

    [Fact(DisplayName = "ValueTask<ValueOptional<T>>.SelectManyAsync of None should produce the expected result (Func<T, CancellationToken, Task<ValueOptional<TResult>>>).")]
    public async Task ValueTaskValueOptionalSelectManyAsyncOfNoneShouldProduceExpectedResultFuncTCancellationTokenTaskValueOptionalTResult()
    {
        // Given
        ValueOptional<int> optional = ValueOptional<int>.None;
        CancellationToken selectorToken = CancellationToken.None;

        // When
        ValueOptional<int> actual = await ValueTask.FromResult(optional).SelectManyAsync((value, ct) => { selectorToken = ct; return Task.FromResult<ValueOptional<int>>(value * 2); }, TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(ValueOptional<int>.None, actual);
    }

    [Fact(DisplayName = "ValueTask<ValueOptional<T>>.SelectManyAsync of None should produce the expected result (Func<T, CancellationToken, ValueTask<ValueOptional<TResult>>>).")]
    public async Task ValueTaskValueOptionalSelectManyAsyncOfNoneShouldProduceExpectedResultFuncTCancellationTokenTaskValueOptionalTResultWithValueTaskDelegates()
    {
        // Given
        ValueOptional<int> optional = ValueOptional<int>.None;
        CancellationToken selectorToken = CancellationToken.None;

        // When
        ValueOptional<int> actual = await ValueTask.FromResult(optional).SelectManyAsync((value, ct) => { selectorToken = ct; return ValueTask.FromResult<ValueOptional<int>>(value * 2); }, TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(ValueOptional<int>.None, actual);
    }

    [Fact(DisplayName = "ValueTask<ValueOptional<T>>.ToResultAsync of Some should produce a successful result containing the current optional.")]
    public async Task ValueTaskValueOptionalToResultAsyncOfSomeShouldProduceSuccessfulResultContainingCurrentOptional()
    {
        // Given
        ValueOptional<int> optional = 21;

        // When
        ValueResult<ValueOptional<int>> actual = await ValueTask.FromResult(optional).ToResultAsync(TestContext.Current.CancellationToken);

        // Then
        Assert.True(actual.IsSuccess);
        Assert.Equal(optional, actual.GetValueOrThrow());
    }

    [Fact(DisplayName = "ValueTask<ValueOptional<T>>.ToResultAsync of None should produce a successful result containing the current optional.")]
    public async Task ValueTaskValueOptionalToResultAsyncOfNoneShouldProduceSuccessfulResultContainingCurrentOptional()
    {
        // Given
        ValueOptional<int> optional = ValueOptional<int>.None;

        // When
        ValueResult<ValueOptional<int>> actual = await ValueTask.FromResult(optional).ToResultAsync(TestContext.Current.CancellationToken);

        // Then
        Assert.True(actual.IsSuccess);
        Assert.Equal(optional, actual.GetValueOrThrow());
    }
}
