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

public sealed class ValueOptionalExtensionTests
{
    [Fact(DisplayName = "Task<ValueOptional<T>>.GetValueOrDefaultAsync of Some should produce the expected result.")]
    public async Task TaskValueOptionalGetValueOrDefaultAsyncOfSomeShouldProduceExpectedResult()
    {
        // Given
        ValueOptional<int> optional = 21;

        // When
        int actual = await Task.FromResult(optional).GetValueOrDefaultAsync(TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(21, actual);
    }

    [Fact(DisplayName = "Task<ValueOptional<T>>.GetValueOrDefaultAsync with default value of Some should produce the expected result.")]
    public async Task TaskValueOptionalGetValueOrDefaultAsyncWithDefaultValueOfSomeShouldProduceExpectedResult()
    {
        // Given
        ValueOptional<int> optional = 21;

        // When
        int actual = await Task.FromResult(optional).GetValueOrDefaultAsync(99, TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(21, actual);
    }

    [Fact(DisplayName = "Task<ValueOptional<T>>.GetValueOrDefaultAsync of None should produce the expected result.")]
    public async Task TaskValueOptionalGetValueOrDefaultAsyncOfNoneShouldProduceExpectedResult()
    {
        // Given
        ValueOptional<int> optional = ValueOptional<int>.None;

        // When
        int actual = await Task.FromResult(optional).GetValueOrDefaultAsync(TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(0, actual);
    }

    [Fact(DisplayName = "Task<ValueOptional<T>>.GetValueOrDefaultAsync with default value of None should produce the expected result.")]
    public async Task TaskValueOptionalGetValueOrDefaultAsyncWithDefaultValueOfNoneShouldProduceExpectedResult()
    {
        // Given
        ValueOptional<int> optional = ValueOptional<int>.None;

        // When
        int actual = await Task.FromResult(optional).GetValueOrDefaultAsync(99, TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(99, actual);
    }

    [Fact(DisplayName = "Task<ValueOptional<T>>.GetValueOrThrowAsync of Some should produce the expected result.")]
    public async Task TaskValueOptionalGetValueOrThrowAsyncOfSomeShouldProduceExpectedResult()
    {
        // Given
        ValueOptional<int> optional = 21;

        // When
        int actual = await Task.FromResult(optional).GetValueOrThrowAsync(TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(21, actual);
    }

    [Fact(DisplayName = "Task<ValueOptional<T>>.GetValueOrThrowAsync of None should throw InvalidOperationException.")]
    public async Task TaskValueOptionalGetValueOrThrowAsyncOfNoneShouldThrowInvalidOperationException()
    {
        // Given
        ValueOptional<int> optional = ValueOptional<int>.None;

        // When
        Exception exception = await Assert.ThrowsAsync<InvalidOperationException>(async () => await Task.FromResult(optional).GetValueOrThrowAsync(TestContext.Current.CancellationToken));

        // Then
        Assert.Equal("Optional value of type System.Int32 is not present.", exception.Message);
    }

    [Fact(DisplayName = "Task<ValueOptional<T>>.MatchAsync of Some should invoke the some delegate (Action<T>, Action).")]
    public async Task TaskValueOptionalMatchAsyncOfSomeShouldInvokeSomeDelegateActionTAction()
    {
        // Given
        ValueOptional<int> optional = 21;
        int someValue = 0;
        bool noneCalled = false;

        // When
        await Task.FromResult(optional).MatchAsync(some: value => someValue = value, none: () => noneCalled = true, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(21, someValue);
        Assert.False(noneCalled);
    }

    [Fact(DisplayName = "Task<ValueOptional<T>>.MatchAsync of None should invoke the none delegate (Action<T>, Action).")]
    public async Task TaskValueOptionalMatchAsyncOfNoneShouldInvokeNoneDelegateActionTAction()
    {
        // Given
        ValueOptional<int> optional = ValueOptional<int>.None;
        int someValue = 0;
        bool noneCalled = false;

        // When
        await Task.FromResult(optional).MatchAsync(some: value => someValue = value, none: () => noneCalled = true, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(0, someValue);
        Assert.True(noneCalled);
    }

    [Fact(DisplayName = "Task<ValueOptional<T>>.MatchAsync of Some should invoke the some delegate (Func<T, Task>, Action).")]
    public async Task TaskValueOptionalMatchAsyncOfSomeShouldInvokeSomeDelegateFuncTTaskAction()
    {
        // Given
        ValueOptional<int> optional = 21;
        int someValue = 0;
        bool noneCalled = false;

        // When
        await Task.FromResult(optional).MatchAsync(some: value => { someValue = value; return Task.CompletedTask; }, none: () => noneCalled = true, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(21, someValue);
        Assert.False(noneCalled);
    }

    [Fact(DisplayName = "Task<ValueOptional<T>>.MatchAsync of Some should invoke the some delegate (Func<T, ValueTask>, Action).")]
    public async Task TaskValueOptionalMatchAsyncOfSomeShouldInvokeSomeDelegateFuncTTaskActionWithValueTaskDelegates()
    {
        // Given
        ValueOptional<int> optional = 21;
        int someValue = 0;
        bool noneCalled = false;

        // When
        await Task.FromResult(optional).MatchAsync(some: value => { someValue = value; return ValueTask.CompletedTask; }, none: () => noneCalled = true, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(21, someValue);
        Assert.False(noneCalled);
    }

    [Fact(DisplayName = "Task<ValueOptional<T>>.MatchAsync of None should invoke the none delegate (Func<T, Task>, Action).")]
    public async Task TaskValueOptionalMatchAsyncOfNoneShouldInvokeNoneDelegateFuncTTaskAction()
    {
        // Given
        ValueOptional<int> optional = ValueOptional<int>.None;
        int someValue = 0;
        bool noneCalled = false;

        // When
        await Task.FromResult(optional).MatchAsync(some: value => { someValue = value; return Task.CompletedTask; }, none: () => noneCalled = true, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(0, someValue);
        Assert.True(noneCalled);
    }

    [Fact(DisplayName = "Task<ValueOptional<T>>.MatchAsync of None should invoke the none delegate (Func<T, ValueTask>, Action).")]
    public async Task TaskValueOptionalMatchAsyncOfNoneShouldInvokeNoneDelegateFuncTTaskActionWithValueTaskDelegates()
    {
        // Given
        ValueOptional<int> optional = ValueOptional<int>.None;
        int someValue = 0;
        bool noneCalled = false;

        // When
        await Task.FromResult(optional).MatchAsync(some: value => { someValue = value; return ValueTask.CompletedTask; }, none: () => noneCalled = true, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(0, someValue);
        Assert.True(noneCalled);
    }

    [Fact(DisplayName = "Task<ValueOptional<T>>.MatchAsync of Some should invoke the some delegate (Func<T, CancellationToken, Task>, Action).")]
    public async Task TaskValueOptionalMatchAsyncOfSomeShouldInvokeSomeDelegateFuncTCancellationTokenTaskAction()
    {
        // Given
        ValueOptional<int> optional = 21;
        int someValue = 0;
        bool noneCalled = false;
        CancellationToken someToken = CancellationToken.None;

        // When
        await Task.FromResult(optional).MatchAsync(some: (value, ct) => { someValue = value; someToken = ct; return Task.CompletedTask; }, none: () => noneCalled = true, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(21, someValue);
        Assert.False(noneCalled);
        Assert.Equal(TestContext.Current.CancellationToken, someToken);
    }

    [Fact(DisplayName = "Task<ValueOptional<T>>.MatchAsync of Some should invoke the some delegate (Func<T, CancellationToken, ValueTask>, Action).")]
    public async Task TaskValueOptionalMatchAsyncOfSomeShouldInvokeSomeDelegateFuncTCancellationTokenTaskActionWithValueTaskDelegates()
    {
        // Given
        ValueOptional<int> optional = 21;
        int someValue = 0;
        bool noneCalled = false;
        CancellationToken someToken = CancellationToken.None;

        // When
        await Task.FromResult(optional).MatchAsync(some: (value, ct) => { someValue = value; someToken = ct; return ValueTask.CompletedTask; }, none: () => noneCalled = true, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(21, someValue);
        Assert.False(noneCalled);
        Assert.Equal(TestContext.Current.CancellationToken, someToken);
    }

    [Fact(DisplayName = "Task<ValueOptional<T>>.MatchAsync of None should invoke the none delegate (Func<T, CancellationToken, Task>, Action).")]
    public async Task TaskValueOptionalMatchAsyncOfNoneShouldInvokeNoneDelegateFuncTCancellationTokenTaskAction()
    {
        // Given
        ValueOptional<int> optional = ValueOptional<int>.None;
        int someValue = 0;
        bool noneCalled = false;
        CancellationToken someToken = CancellationToken.None;

        // When
        await Task.FromResult(optional).MatchAsync(some: (value, ct) => { someValue = value; someToken = ct; return Task.CompletedTask; }, none: () => noneCalled = true, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(0, someValue);
        Assert.True(noneCalled);
    }

    [Fact(DisplayName = "Task<ValueOptional<T>>.MatchAsync of None should invoke the none delegate (Func<T, CancellationToken, ValueTask>, Action).")]
    public async Task TaskValueOptionalMatchAsyncOfNoneShouldInvokeNoneDelegateFuncTCancellationTokenTaskActionWithValueTaskDelegates()
    {
        // Given
        ValueOptional<int> optional = ValueOptional<int>.None;
        int someValue = 0;
        bool noneCalled = false;
        CancellationToken someToken = CancellationToken.None;

        // When
        await Task.FromResult(optional).MatchAsync(some: (value, ct) => { someValue = value; someToken = ct; return ValueTask.CompletedTask; }, none: () => noneCalled = true, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(0, someValue);
        Assert.True(noneCalled);
    }

    [Fact(DisplayName = "Task<ValueOptional<T>>.MatchAsync of Some should invoke the some delegate (Action<T>, Func<Task>).")]
    public async Task TaskValueOptionalMatchAsyncOfSomeShouldInvokeSomeDelegateActionTFuncTask()
    {
        // Given
        ValueOptional<int> optional = 21;
        int someValue = 0;
        bool noneCalled = false;

        // When
        await Task.FromResult(optional).MatchAsync(some: value => someValue = value, none: () => { noneCalled = true; return Task.CompletedTask; }, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(21, someValue);
        Assert.False(noneCalled);
    }

    [Fact(DisplayName = "Task<ValueOptional<T>>.MatchAsync of Some should invoke the some delegate (Action<T>, Func<ValueTask>).")]
    public async Task TaskValueOptionalMatchAsyncOfSomeShouldInvokeSomeDelegateActionTFuncTaskWithValueTaskDelegates()
    {
        // Given
        ValueOptional<int> optional = 21;
        int someValue = 0;
        bool noneCalled = false;

        // When
        await Task.FromResult(optional).MatchAsync(some: value => someValue = value, none: () => { noneCalled = true; return ValueTask.CompletedTask; }, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(21, someValue);
        Assert.False(noneCalled);
    }

    [Fact(DisplayName = "Task<ValueOptional<T>>.MatchAsync of None should invoke the none delegate (Action<T>, Func<Task>).")]
    public async Task TaskValueOptionalMatchAsyncOfNoneShouldInvokeNoneDelegateActionTFuncTask()
    {
        // Given
        ValueOptional<int> optional = ValueOptional<int>.None;
        int someValue = 0;
        bool noneCalled = false;

        // When
        await Task.FromResult(optional).MatchAsync(some: value => someValue = value, none: () => { noneCalled = true; return Task.CompletedTask; }, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(0, someValue);
        Assert.True(noneCalled);
    }

    [Fact(DisplayName = "Task<ValueOptional<T>>.MatchAsync of None should invoke the none delegate (Action<T>, Func<ValueTask>).")]
    public async Task TaskValueOptionalMatchAsyncOfNoneShouldInvokeNoneDelegateActionTFuncTaskWithValueTaskDelegates()
    {
        // Given
        ValueOptional<int> optional = ValueOptional<int>.None;
        int someValue = 0;
        bool noneCalled = false;

        // When
        await Task.FromResult(optional).MatchAsync(some: value => someValue = value, none: () => { noneCalled = true; return ValueTask.CompletedTask; }, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(0, someValue);
        Assert.True(noneCalled);
    }

    [Fact(DisplayName = "Task<ValueOptional<T>>.MatchAsync of Some should invoke the some delegate (Action<T>, Func<CancellationToken, Task>).")]
    public async Task TaskValueOptionalMatchAsyncOfSomeShouldInvokeSomeDelegateActionTFuncCancellationTokenTask()
    {
        // Given
        ValueOptional<int> optional = 21;
        int someValue = 0;
        bool noneCalled = false;
        CancellationToken noneToken = CancellationToken.None;

        // When
        await Task.FromResult(optional).MatchAsync(some: value => someValue = value, none: ct => { noneCalled = true; noneToken = ct; return Task.CompletedTask; }, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(21, someValue);
        Assert.False(noneCalled);
    }

    [Fact(DisplayName = "Task<ValueOptional<T>>.MatchAsync of Some should invoke the some delegate (Action<T>, Func<CancellationToken, ValueTask>).")]
    public async Task TaskValueOptionalMatchAsyncOfSomeShouldInvokeSomeDelegateActionTFuncCancellationTokenTaskWithValueTaskDelegates()
    {
        // Given
        ValueOptional<int> optional = 21;
        int someValue = 0;
        bool noneCalled = false;
        CancellationToken noneToken = CancellationToken.None;

        // When
        await Task.FromResult(optional).MatchAsync(some: value => someValue = value, none: ct => { noneCalled = true; noneToken = ct; return ValueTask.CompletedTask; }, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(21, someValue);
        Assert.False(noneCalled);
    }

    [Fact(DisplayName = "Task<ValueOptional<T>>.MatchAsync of None should invoke the none delegate (Action<T>, Func<CancellationToken, Task>).")]
    public async Task TaskValueOptionalMatchAsyncOfNoneShouldInvokeNoneDelegateActionTFuncCancellationTokenTask()
    {
        // Given
        ValueOptional<int> optional = ValueOptional<int>.None;
        int someValue = 0;
        bool noneCalled = false;
        CancellationToken noneToken = CancellationToken.None;

        // When
        await Task.FromResult(optional).MatchAsync(some: value => someValue = value, none: ct => { noneCalled = true; noneToken = ct; return Task.CompletedTask; }, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(0, someValue);
        Assert.True(noneCalled);
        Assert.Equal(TestContext.Current.CancellationToken, noneToken);
    }

    [Fact(DisplayName = "Task<ValueOptional<T>>.MatchAsync of None should invoke the none delegate (Action<T>, Func<CancellationToken, ValueTask>).")]
    public async Task TaskValueOptionalMatchAsyncOfNoneShouldInvokeNoneDelegateActionTFuncCancellationTokenTaskWithValueTaskDelegates()
    {
        // Given
        ValueOptional<int> optional = ValueOptional<int>.None;
        int someValue = 0;
        bool noneCalled = false;
        CancellationToken noneToken = CancellationToken.None;

        // When
        await Task.FromResult(optional).MatchAsync(some: value => someValue = value, none: ct => { noneCalled = true; noneToken = ct; return ValueTask.CompletedTask; }, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(0, someValue);
        Assert.True(noneCalled);
        Assert.Equal(TestContext.Current.CancellationToken, noneToken);
    }

    [Fact(DisplayName = "Task<ValueOptional<T>>.MatchAsync of Some should invoke the some delegate (Func<T, Task>, Func<Task>).")]
    public async Task TaskValueOptionalMatchAsyncOfSomeShouldInvokeSomeDelegateFuncTTaskFuncTask()
    {
        // Given
        ValueOptional<int> optional = 21;
        int someValue = 0;
        bool noneCalled = false;

        // When
        await Task.FromResult(optional).MatchAsync(some: value => { someValue = value; return Task.CompletedTask; }, none: () => { noneCalled = true; return Task.CompletedTask; }, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(21, someValue);
        Assert.False(noneCalled);
    }

    [Fact(DisplayName = "Task<ValueOptional<T>>.MatchAsync of Some should invoke the some delegate (Func<T, ValueTask>, Func<ValueTask>).")]
    public async Task TaskValueOptionalMatchAsyncOfSomeShouldInvokeSomeDelegateFuncTTaskFuncTaskWithValueTaskDelegates()
    {
        // Given
        ValueOptional<int> optional = 21;
        int someValue = 0;
        bool noneCalled = false;

        // When
        await Task.FromResult(optional).MatchAsync(some: value => { someValue = value; return ValueTask.CompletedTask; }, none: () => { noneCalled = true; return ValueTask.CompletedTask; }, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(21, someValue);
        Assert.False(noneCalled);
    }

    [Fact(DisplayName = "Task<ValueOptional<T>>.MatchAsync of None should invoke the none delegate (Func<T, Task>, Func<Task>).")]
    public async Task TaskValueOptionalMatchAsyncOfNoneShouldInvokeNoneDelegateFuncTTaskFuncTask()
    {
        // Given
        ValueOptional<int> optional = ValueOptional<int>.None;
        int someValue = 0;
        bool noneCalled = false;

        // When
        await Task.FromResult(optional).MatchAsync(some: value => { someValue = value; return Task.CompletedTask; }, none: () => { noneCalled = true; return Task.CompletedTask; }, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(0, someValue);
        Assert.True(noneCalled);
    }

    [Fact(DisplayName = "Task<ValueOptional<T>>.MatchAsync of None should invoke the none delegate (Func<T, ValueTask>, Func<ValueTask>).")]
    public async Task TaskValueOptionalMatchAsyncOfNoneShouldInvokeNoneDelegateFuncTTaskFuncTaskWithValueTaskDelegates()
    {
        // Given
        ValueOptional<int> optional = ValueOptional<int>.None;
        int someValue = 0;
        bool noneCalled = false;

        // When
        await Task.FromResult(optional).MatchAsync(some: value => { someValue = value; return ValueTask.CompletedTask; }, none: () => { noneCalled = true; return ValueTask.CompletedTask; }, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(0, someValue);
        Assert.True(noneCalled);
    }

    [Fact(DisplayName = "Task<ValueOptional<T>>.MatchAsync of Some should invoke the some delegate (Func<T, CancellationToken, Task>, Func<CancellationToken, Task>).")]
    public async Task TaskValueOptionalMatchAsyncOfSomeShouldInvokeSomeDelegateFuncTCancellationTokenTaskFuncCancellationTokenTask()
    {
        // Given
        ValueOptional<int> optional = 21;
        int someValue = 0;
        bool noneCalled = false;
        CancellationToken someToken = CancellationToken.None;
        CancellationToken noneToken = CancellationToken.None;

        // When
        await Task.FromResult(optional).MatchAsync(some: (value, ct) => { someValue = value; someToken = ct; return Task.CompletedTask; }, none: ct => { noneCalled = true; noneToken = ct; return Task.CompletedTask; }, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(21, someValue);
        Assert.False(noneCalled);
        Assert.Equal(TestContext.Current.CancellationToken, someToken);
    }

    [Fact(DisplayName = "Task<ValueOptional<T>>.MatchAsync of Some should invoke the some delegate (Func<T, CancellationToken, ValueTask>, Func<CancellationToken, ValueTask>).")]
    public async Task TaskValueOptionalMatchAsyncOfSomeShouldInvokeSomeDelegateFuncTCancellationTokenTaskFuncCancellationTokenTaskWithValueTaskDelegates()
    {
        // Given
        ValueOptional<int> optional = 21;
        int someValue = 0;
        bool noneCalled = false;
        CancellationToken someToken = CancellationToken.None;
        CancellationToken noneToken = CancellationToken.None;

        // When
        await Task.FromResult(optional).MatchAsync(some: (value, ct) => { someValue = value; someToken = ct; return ValueTask.CompletedTask; }, none: ct => { noneCalled = true; noneToken = ct; return ValueTask.CompletedTask; }, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(21, someValue);
        Assert.False(noneCalled);
        Assert.Equal(TestContext.Current.CancellationToken, someToken);
    }

    [Fact(DisplayName = "Task<ValueOptional<T>>.MatchAsync of None should invoke the none delegate (Func<T, CancellationToken, Task>, Func<CancellationToken, Task>).")]
    public async Task TaskValueOptionalMatchAsyncOfNoneShouldInvokeNoneDelegateFuncTCancellationTokenTaskFuncCancellationTokenTask()
    {
        // Given
        ValueOptional<int> optional = ValueOptional<int>.None;
        int someValue = 0;
        bool noneCalled = false;
        CancellationToken someToken = CancellationToken.None;
        CancellationToken noneToken = CancellationToken.None;

        // When
        await Task.FromResult(optional).MatchAsync(some: (value, ct) => { someValue = value; someToken = ct; return Task.CompletedTask; }, none: ct => { noneCalled = true; noneToken = ct; return Task.CompletedTask; }, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(0, someValue);
        Assert.True(noneCalled);
        Assert.Equal(TestContext.Current.CancellationToken, noneToken);
    }

    [Fact(DisplayName = "Task<ValueOptional<T>>.MatchAsync of None should invoke the none delegate (Func<T, CancellationToken, ValueTask>, Func<CancellationToken, ValueTask>).")]
    public async Task TaskValueOptionalMatchAsyncOfNoneShouldInvokeNoneDelegateFuncTCancellationTokenTaskFuncCancellationTokenTaskWithValueTaskDelegates()
    {
        // Given
        ValueOptional<int> optional = ValueOptional<int>.None;
        int someValue = 0;
        bool noneCalled = false;
        CancellationToken someToken = CancellationToken.None;
        CancellationToken noneToken = CancellationToken.None;

        // When
        await Task.FromResult(optional).MatchAsync(some: (value, ct) => { someValue = value; someToken = ct; return ValueTask.CompletedTask; }, none: ct => { noneCalled = true; noneToken = ct; return ValueTask.CompletedTask; }, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(0, someValue);
        Assert.True(noneCalled);
        Assert.Equal(TestContext.Current.CancellationToken, noneToken);
    }

    [Fact(DisplayName = "Task<ValueOptional<T>>.MatchAsync<TResult> of Some should return the result of the some delegate (Func<T, TResult>, Func<TResult>).")]
    public async Task TaskValueOptionalMatchAsyncTResultOfSomeShouldReturnResultOfSomeDelegateFuncTTResultFuncTResult()
    {
        // Given
        ValueOptional<int> optional = 21;

        // When
        string actual = await Task.FromResult(optional).MatchAsync(some: value => $"Some {value}", none: () => "None", cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal("Some 21", actual);
    }

    [Fact(DisplayName = "Task<ValueOptional<T>>.MatchAsync<TResult> of None should return the result of the none delegate (Func<T, TResult>, Func<TResult>).")]
    public async Task TaskValueOptionalMatchAsyncTResultOfNoneShouldReturnResultOfNoneDelegateFuncTTResultFuncTResult()
    {
        // Given
        ValueOptional<int> optional = ValueOptional<int>.None;

        // When
        string actual = await Task.FromResult(optional).MatchAsync(some: value => $"Some {value}", none: () => "None", cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal("None", actual);
    }

    [Fact(DisplayName = "Task<ValueOptional<T>>.MatchAsync<TResult> of Some should return the result of the some delegate (Func<T, Task<TResult>>, Func<TResult>).")]
    public async Task TaskValueOptionalMatchAsyncTResultOfSomeShouldReturnResultOfSomeDelegateFuncTTaskTResultFuncTResult()
    {
        // Given
        ValueOptional<int> optional = 21;

        // When
        string actual = await Task.FromResult(optional).MatchAsync(some: value => Task.FromResult($"Some {value}"), none: () => "None", cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal("Some 21", actual);
    }

    [Fact(DisplayName = "Task<ValueOptional<T>>.MatchAsync<TResult> of Some should return the result of the some delegate (Func<T, ValueTask<TResult>>, Func<TResult>).")]
    public async Task TaskValueOptionalMatchAsyncTResultOfSomeShouldReturnResultOfSomeDelegateFuncTTaskTResultFuncTResultWithValueTaskDelegates()
    {
        // Given
        ValueOptional<int> optional = 21;

        // When
        string actual = await Task.FromResult(optional).MatchAsync(some: value => ValueTask.FromResult($"Some {value}"), none: () => "None", cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal("Some 21", actual);
    }

    [Fact(DisplayName = "Task<ValueOptional<T>>.MatchAsync<TResult> of None should return the result of the none delegate (Func<T, Task<TResult>>, Func<TResult>).")]
    public async Task TaskValueOptionalMatchAsyncTResultOfNoneShouldReturnResultOfNoneDelegateFuncTTaskTResultFuncTResult()
    {
        // Given
        ValueOptional<int> optional = ValueOptional<int>.None;

        // When
        string actual = await Task.FromResult(optional).MatchAsync(some: value => Task.FromResult($"Some {value}"), none: () => "None", cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal("None", actual);
    }

    [Fact(DisplayName = "Task<ValueOptional<T>>.MatchAsync<TResult> of None should return the result of the none delegate (Func<T, ValueTask<TResult>>, Func<TResult>).")]
    public async Task TaskValueOptionalMatchAsyncTResultOfNoneShouldReturnResultOfNoneDelegateFuncTTaskTResultFuncTResultWithValueTaskDelegates()
    {
        // Given
        ValueOptional<int> optional = ValueOptional<int>.None;

        // When
        string actual = await Task.FromResult(optional).MatchAsync(some: value => ValueTask.FromResult($"Some {value}"), none: () => "None", cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal("None", actual);
    }

    [Fact(DisplayName = "Task<ValueOptional<T>>.MatchAsync<TResult> of Some should return the result of the some delegate (Func<T, CancellationToken, Task<TResult>>, Func<TResult>).")]
    public async Task TaskValueOptionalMatchAsyncTResultOfSomeShouldReturnResultOfSomeDelegateFuncTCancellationTokenTaskTResultFuncTResult()
    {
        // Given
        ValueOptional<int> optional = 21;
        CancellationToken someToken = CancellationToken.None;

        // When
        string actual = await Task.FromResult(optional).MatchAsync(some: (value, ct) => { someToken = ct; return Task.FromResult($"Some {value}"); }, none: () => "None", cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal("Some 21", actual);
        Assert.Equal(TestContext.Current.CancellationToken, someToken);
    }

    [Fact(DisplayName = "Task<ValueOptional<T>>.MatchAsync<TResult> of Some should return the result of the some delegate (Func<T, CancellationToken, ValueTask<TResult>>, Func<TResult>).")]
    public async Task TaskValueOptionalMatchAsyncTResultOfSomeShouldReturnResultOfSomeDelegateFuncTCancellationTokenTaskTResultFuncTResultWithValueTaskDelegates()
    {
        // Given
        ValueOptional<int> optional = 21;
        CancellationToken someToken = CancellationToken.None;

        // When
        string actual = await Task.FromResult(optional).MatchAsync(some: (value, ct) => { someToken = ct; return ValueTask.FromResult($"Some {value}"); }, none: () => "None", cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal("Some 21", actual);
        Assert.Equal(TestContext.Current.CancellationToken, someToken);
    }

    [Fact(DisplayName = "Task<ValueOptional<T>>.MatchAsync<TResult> of None should return the result of the none delegate (Func<T, CancellationToken, Task<TResult>>, Func<TResult>).")]
    public async Task TaskValueOptionalMatchAsyncTResultOfNoneShouldReturnResultOfNoneDelegateFuncTCancellationTokenTaskTResultFuncTResult()
    {
        // Given
        ValueOptional<int> optional = ValueOptional<int>.None;
        CancellationToken someToken = CancellationToken.None;

        // When
        string actual = await Task.FromResult(optional).MatchAsync(some: (value, ct) => { someToken = ct; return Task.FromResult($"Some {value}"); }, none: () => "None", cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal("None", actual);
    }

    [Fact(DisplayName = "Task<ValueOptional<T>>.MatchAsync<TResult> of None should return the result of the none delegate (Func<T, CancellationToken, ValueTask<TResult>>, Func<TResult>).")]
    public async Task TaskValueOptionalMatchAsyncTResultOfNoneShouldReturnResultOfNoneDelegateFuncTCancellationTokenTaskTResultFuncTResultWithValueTaskDelegates()
    {
        // Given
        ValueOptional<int> optional = ValueOptional<int>.None;
        CancellationToken someToken = CancellationToken.None;

        // When
        string actual = await Task.FromResult(optional).MatchAsync(some: (value, ct) => { someToken = ct; return ValueTask.FromResult($"Some {value}"); }, none: () => "None", cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal("None", actual);
    }

    [Fact(DisplayName = "Task<ValueOptional<T>>.MatchAsync<TResult> of Some should return the result of the some delegate (Func<T, TResult>, Func<Task<TResult>>).")]
    public async Task TaskValueOptionalMatchAsyncTResultOfSomeShouldReturnResultOfSomeDelegateFuncTTResultFuncTaskTResult()
    {
        // Given
        ValueOptional<int> optional = 21;

        // When
        string actual = await Task.FromResult(optional).MatchAsync(some: value => $"Some {value}", none: () => Task.FromResult("None"), cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal("Some 21", actual);
    }

    [Fact(DisplayName = "Task<ValueOptional<T>>.MatchAsync<TResult> of Some should return the result of the some delegate (Func<T, TResult>, Func<ValueTask<TResult>>).")]
    public async Task TaskValueOptionalMatchAsyncTResultOfSomeShouldReturnResultOfSomeDelegateFuncTTResultFuncTaskTResultWithValueTaskDelegates()
    {
        // Given
        ValueOptional<int> optional = 21;

        // When
        string actual = await Task.FromResult(optional).MatchAsync(some: value => $"Some {value}", none: () => ValueTask.FromResult("None"), cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal("Some 21", actual);
    }

    [Fact(DisplayName = "Task<ValueOptional<T>>.MatchAsync<TResult> of None should return the result of the none delegate (Func<T, TResult>, Func<Task<TResult>>).")]
    public async Task TaskValueOptionalMatchAsyncTResultOfNoneShouldReturnResultOfNoneDelegateFuncTTResultFuncTaskTResult()
    {
        // Given
        ValueOptional<int> optional = ValueOptional<int>.None;

        // When
        string actual = await Task.FromResult(optional).MatchAsync(some: value => $"Some {value}", none: () => Task.FromResult("None"), cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal("None", actual);
    }

    [Fact(DisplayName = "Task<ValueOptional<T>>.MatchAsync<TResult> of None should return the result of the none delegate (Func<T, TResult>, Func<ValueTask<TResult>>).")]
    public async Task TaskValueOptionalMatchAsyncTResultOfNoneShouldReturnResultOfNoneDelegateFuncTTResultFuncTaskTResultWithValueTaskDelegates()
    {
        // Given
        ValueOptional<int> optional = ValueOptional<int>.None;

        // When
        string actual = await Task.FromResult(optional).MatchAsync(some: value => $"Some {value}", none: () => ValueTask.FromResult("None"), cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal("None", actual);
    }

    [Fact(DisplayName = "Task<ValueOptional<T>>.MatchAsync<TResult> of Some should return the result of the some delegate (Func<T, TResult>, Func<CancellationToken, Task<TResult>>).")]
    public async Task TaskValueOptionalMatchAsyncTResultOfSomeShouldReturnResultOfSomeDelegateFuncTTResultFuncCancellationTokenTaskTResult()
    {
        // Given
        ValueOptional<int> optional = 21;
        CancellationToken noneToken = CancellationToken.None;

        // When
        string actual = await Task.FromResult(optional).MatchAsync(some: value => $"Some {value}", none: ct => { noneToken = ct; return Task.FromResult("None"); }, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal("Some 21", actual);
    }

    [Fact(DisplayName = "Task<ValueOptional<T>>.MatchAsync<TResult> of Some should return the result of the some delegate (Func<T, TResult>, Func<CancellationToken, ValueTask<TResult>>).")]
    public async Task TaskValueOptionalMatchAsyncTResultOfSomeShouldReturnResultOfSomeDelegateFuncTTResultFuncCancellationTokenTaskTResultWithValueTaskDelegates()
    {
        // Given
        ValueOptional<int> optional = 21;
        CancellationToken noneToken = CancellationToken.None;

        // When
        string actual = await Task.FromResult(optional).MatchAsync(some: value => $"Some {value}", none: ct => { noneToken = ct; return ValueTask.FromResult("None"); }, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal("Some 21", actual);
    }

    [Fact(DisplayName = "Task<ValueOptional<T>>.MatchAsync<TResult> of None should return the result of the none delegate (Func<T, TResult>, Func<CancellationToken, Task<TResult>>).")]
    public async Task TaskValueOptionalMatchAsyncTResultOfNoneShouldReturnResultOfNoneDelegateFuncTTResultFuncCancellationTokenTaskTResult()
    {
        // Given
        ValueOptional<int> optional = ValueOptional<int>.None;
        CancellationToken noneToken = CancellationToken.None;

        // When
        string actual = await Task.FromResult(optional).MatchAsync(some: value => $"Some {value}", none: ct => { noneToken = ct; return Task.FromResult("None"); }, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal("None", actual);
        Assert.Equal(TestContext.Current.CancellationToken, noneToken);
    }

    [Fact(DisplayName = "Task<ValueOptional<T>>.MatchAsync<TResult> of None should return the result of the none delegate (Func<T, TResult>, Func<CancellationToken, ValueTask<TResult>>).")]
    public async Task TaskValueOptionalMatchAsyncTResultOfNoneShouldReturnResultOfNoneDelegateFuncTTResultFuncCancellationTokenTaskTResultWithValueTaskDelegates()
    {
        // Given
        ValueOptional<int> optional = ValueOptional<int>.None;
        CancellationToken noneToken = CancellationToken.None;

        // When
        string actual = await Task.FromResult(optional).MatchAsync(some: value => $"Some {value}", none: ct => { noneToken = ct; return ValueTask.FromResult("None"); }, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal("None", actual);
        Assert.Equal(TestContext.Current.CancellationToken, noneToken);
    }

    [Fact(DisplayName = "Task<ValueOptional<T>>.MatchAsync<TResult> of Some should return the result of the some delegate (Func<T, Task<TResult>>, Func<Task<TResult>>).")]
    public async Task TaskValueOptionalMatchAsyncTResultOfSomeShouldReturnResultOfSomeDelegateFuncTTaskTResultFuncTaskTResult()
    {
        // Given
        ValueOptional<int> optional = 21;

        // When
        string actual = await Task.FromResult(optional).MatchAsync(some: value => Task.FromResult($"Some {value}"), none: () => Task.FromResult("None"), cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal("Some 21", actual);
    }

    [Fact(DisplayName = "Task<ValueOptional<T>>.MatchAsync<TResult> of Some should return the result of the some delegate (Func<T, ValueTask<TResult>>, Func<ValueTask<TResult>>).")]
    public async Task TaskValueOptionalMatchAsyncTResultOfSomeShouldReturnResultOfSomeDelegateFuncTTaskTResultFuncTaskTResultWithValueTaskDelegates()
    {
        // Given
        ValueOptional<int> optional = 21;

        // When
        string actual = await Task.FromResult(optional).MatchAsync(some: value => ValueTask.FromResult($"Some {value}"), none: () => ValueTask.FromResult("None"), cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal("Some 21", actual);
    }

    [Fact(DisplayName = "Task<ValueOptional<T>>.MatchAsync<TResult> of None should return the result of the none delegate (Func<T, Task<TResult>>, Func<Task<TResult>>).")]
    public async Task TaskValueOptionalMatchAsyncTResultOfNoneShouldReturnResultOfNoneDelegateFuncTTaskTResultFuncTaskTResult()
    {
        // Given
        ValueOptional<int> optional = ValueOptional<int>.None;

        // When
        string actual = await Task.FromResult(optional).MatchAsync(some: value => Task.FromResult($"Some {value}"), none: () => Task.FromResult("None"), cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal("None", actual);
    }

    [Fact(DisplayName = "Task<ValueOptional<T>>.MatchAsync<TResult> of None should return the result of the none delegate (Func<T, ValueTask<TResult>>, Func<ValueTask<TResult>>).")]
    public async Task TaskValueOptionalMatchAsyncTResultOfNoneShouldReturnResultOfNoneDelegateFuncTTaskTResultFuncTaskTResultWithValueTaskDelegates()
    {
        // Given
        ValueOptional<int> optional = ValueOptional<int>.None;

        // When
        string actual = await Task.FromResult(optional).MatchAsync(some: value => ValueTask.FromResult($"Some {value}"), none: () => ValueTask.FromResult("None"), cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal("None", actual);
    }

    [Fact(DisplayName = "Task<ValueOptional<T>>.MatchAsync<TResult> of Some should return the result of the some delegate (Func<T, CancellationToken, Task<TResult>>, Func<CancellationToken, Task<TResult>>).")]
    public async Task TaskValueOptionalMatchAsyncTResultOfSomeShouldReturnResultOfSomeDelegateFuncTCancellationTokenTaskTResultFuncCancellationTokenTaskTResult()
    {
        // Given
        ValueOptional<int> optional = 21;
        CancellationToken someToken = CancellationToken.None;
        CancellationToken noneToken = CancellationToken.None;

        // When
        string actual = await Task.FromResult(optional).MatchAsync(some: (value, ct) => { someToken = ct; return Task.FromResult($"Some {value}"); }, none: ct => { noneToken = ct; return Task.FromResult("None"); }, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal("Some 21", actual);
        Assert.Equal(TestContext.Current.CancellationToken, someToken);
    }

    [Fact(DisplayName = "Task<ValueOptional<T>>.MatchAsync<TResult> of Some should return the result of the some delegate (Func<T, CancellationToken, ValueTask<TResult>>, Func<CancellationToken, ValueTask<TResult>>).")]
    public async Task TaskValueOptionalMatchAsyncTResultOfSomeShouldReturnResultOfSomeDelegateFuncTCancellationTokenTaskTResultFuncCancellationTokenTaskTResultWithValueTaskDelegates()
    {
        // Given
        ValueOptional<int> optional = 21;
        CancellationToken someToken = CancellationToken.None;
        CancellationToken noneToken = CancellationToken.None;

        // When
        string actual = await Task.FromResult(optional).MatchAsync(some: (value, ct) => { someToken = ct; return ValueTask.FromResult($"Some {value}"); }, none: ct => { noneToken = ct; return ValueTask.FromResult("None"); }, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal("Some 21", actual);
        Assert.Equal(TestContext.Current.CancellationToken, someToken);
    }

    [Fact(DisplayName = "Task<ValueOptional<T>>.MatchAsync<TResult> of None should return the result of the none delegate (Func<T, CancellationToken, Task<TResult>>, Func<CancellationToken, Task<TResult>>).")]
    public async Task TaskValueOptionalMatchAsyncTResultOfNoneShouldReturnResultOfNoneDelegateFuncTCancellationTokenTaskTResultFuncCancellationTokenTaskTResult()
    {
        // Given
        ValueOptional<int> optional = ValueOptional<int>.None;
        CancellationToken someToken = CancellationToken.None;
        CancellationToken noneToken = CancellationToken.None;

        // When
        string actual = await Task.FromResult(optional).MatchAsync(some: (value, ct) => { someToken = ct; return Task.FromResult($"Some {value}"); }, none: ct => { noneToken = ct; return Task.FromResult("None"); }, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal("None", actual);
        Assert.Equal(TestContext.Current.CancellationToken, noneToken);
    }

    [Fact(DisplayName = "Task<ValueOptional<T>>.MatchAsync<TResult> of None should return the result of the none delegate (Func<T, CancellationToken, ValueTask<TResult>>, Func<CancellationToken, ValueTask<TResult>>).")]
    public async Task TaskValueOptionalMatchAsyncTResultOfNoneShouldReturnResultOfNoneDelegateFuncTCancellationTokenTaskTResultFuncCancellationTokenTaskTResultWithValueTaskDelegates()
    {
        // Given
        ValueOptional<int> optional = ValueOptional<int>.None;
        CancellationToken someToken = CancellationToken.None;
        CancellationToken noneToken = CancellationToken.None;

        // When
        string actual = await Task.FromResult(optional).MatchAsync(some: (value, ct) => { someToken = ct; return ValueTask.FromResult($"Some {value}"); }, none: ct => { noneToken = ct; return ValueTask.FromResult("None"); }, cancellationToken: TestContext.Current.CancellationToken);

        // Then
        Assert.Equal("None", actual);
        Assert.Equal(TestContext.Current.CancellationToken, noneToken);
    }

    [Fact(DisplayName = "Task<ValueOptional<T>>.SelectAsync of Some should produce the expected result (Func<T, TResult>).")]
    public async Task TaskValueOptionalSelectAsyncOfSomeShouldProduceExpectedResultFuncTTResult()
    {
        // Given
        ValueOptional<int> optional = 21;

        // When
        ValueOptional<int> actual = await Task.FromResult(optional).SelectAsync(value => value * 2, TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(ValueOptional<int>.Some(42), actual);
    }

    [Fact(DisplayName = "Task<ValueOptional<T>>.SelectAsync of None should produce the expected result (Func<T, TResult>).")]
    public async Task TaskValueOptionalSelectAsyncOfNoneShouldProduceExpectedResultFuncTTResult()
    {
        // Given
        ValueOptional<int> optional = ValueOptional<int>.None;

        // When
        ValueOptional<int> actual = await Task.FromResult(optional).SelectAsync(value => value * 2, TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(ValueOptional<int>.None, actual);
    }

    [Fact(DisplayName = "Task<ValueOptional<T>>.SelectAsync of Some should produce the expected result (Func<T, Task<TResult>>).")]
    public async Task TaskValueOptionalSelectAsyncOfSomeShouldProduceExpectedResultFuncTTaskTResult()
    {
        // Given
        ValueOptional<int> optional = 21;

        // When
        ValueOptional<int> actual = await Task.FromResult(optional).SelectAsync(value => Task.FromResult(value * 2), TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(ValueOptional<int>.Some(42), actual);
    }

    [Fact(DisplayName = "Task<ValueOptional<T>>.SelectAsync of Some should produce the expected result (Func<T, ValueTask<TResult>>).")]
    public async Task TaskValueOptionalSelectAsyncOfSomeShouldProduceExpectedResultFuncTTaskTResultWithValueTaskDelegates()
    {
        // Given
        ValueOptional<int> optional = 21;

        // When
        ValueOptional<int> actual = await Task.FromResult(optional).SelectAsync(value => ValueTask.FromResult(value * 2), TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(ValueOptional<int>.Some(42), actual);
    }

    [Fact(DisplayName = "Task<ValueOptional<T>>.SelectAsync of None should produce the expected result (Func<T, Task<TResult>>).")]
    public async Task TaskValueOptionalSelectAsyncOfNoneShouldProduceExpectedResultFuncTTaskTResult()
    {
        // Given
        ValueOptional<int> optional = ValueOptional<int>.None;

        // When
        ValueOptional<int> actual = await Task.FromResult(optional).SelectAsync(value => Task.FromResult(value * 2), TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(ValueOptional<int>.None, actual);
    }

    [Fact(DisplayName = "Task<ValueOptional<T>>.SelectAsync of None should produce the expected result (Func<T, ValueTask<TResult>>).")]
    public async Task TaskValueOptionalSelectAsyncOfNoneShouldProduceExpectedResultFuncTTaskTResultWithValueTaskDelegates()
    {
        // Given
        ValueOptional<int> optional = ValueOptional<int>.None;

        // When
        ValueOptional<int> actual = await Task.FromResult(optional).SelectAsync(value => ValueTask.FromResult(value * 2), TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(ValueOptional<int>.None, actual);
    }

    [Fact(DisplayName = "Task<ValueOptional<T>>.SelectAsync of Some should produce the expected result (Func<T, CancellationToken, Task<TResult>>).")]
    public async Task TaskValueOptionalSelectAsyncOfSomeShouldProduceExpectedResultFuncTCancellationTokenTaskTResult()
    {
        // Given
        ValueOptional<int> optional = 21;
        CancellationToken selectorToken = CancellationToken.None;

        // When
        ValueOptional<int> actual = await Task.FromResult(optional).SelectAsync((value, ct) => { selectorToken = ct; return Task.FromResult(value * 2); }, TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(ValueOptional<int>.Some(42), actual);
        Assert.Equal(TestContext.Current.CancellationToken, selectorToken);
    }

    [Fact(DisplayName = "Task<ValueOptional<T>>.SelectAsync of Some should produce the expected result (Func<T, CancellationToken, ValueTask<TResult>>).")]
    public async Task TaskValueOptionalSelectAsyncOfSomeShouldProduceExpectedResultFuncTCancellationTokenTaskTResultWithValueTaskDelegates()
    {
        // Given
        ValueOptional<int> optional = 21;
        CancellationToken selectorToken = CancellationToken.None;

        // When
        ValueOptional<int> actual = await Task.FromResult(optional).SelectAsync((value, ct) => { selectorToken = ct; return ValueTask.FromResult(value * 2); }, TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(ValueOptional<int>.Some(42), actual);
        Assert.Equal(TestContext.Current.CancellationToken, selectorToken);
    }

    [Fact(DisplayName = "Task<ValueOptional<T>>.SelectAsync of None should produce the expected result (Func<T, CancellationToken, Task<TResult>>).")]
    public async Task TaskValueOptionalSelectAsyncOfNoneShouldProduceExpectedResultFuncTCancellationTokenTaskTResult()
    {
        // Given
        ValueOptional<int> optional = ValueOptional<int>.None;
        CancellationToken selectorToken = CancellationToken.None;

        // When
        ValueOptional<int> actual = await Task.FromResult(optional).SelectAsync((value, ct) => { selectorToken = ct; return Task.FromResult(value * 2); }, TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(ValueOptional<int>.None, actual);
    }

    [Fact(DisplayName = "Task<ValueOptional<T>>.SelectAsync of None should produce the expected result (Func<T, CancellationToken, ValueTask<TResult>>).")]
    public async Task TaskValueOptionalSelectAsyncOfNoneShouldProduceExpectedResultFuncTCancellationTokenTaskTResultWithValueTaskDelegates()
    {
        // Given
        ValueOptional<int> optional = ValueOptional<int>.None;
        CancellationToken selectorToken = CancellationToken.None;

        // When
        ValueOptional<int> actual = await Task.FromResult(optional).SelectAsync((value, ct) => { selectorToken = ct; return ValueTask.FromResult(value * 2); }, TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(ValueOptional<int>.None, actual);
    }

    [Fact(DisplayName = "Task<ValueOptional<T>>.SelectManyAsync of Some should produce the expected result (Func<T, ValueOptional<TResult>>).")]
    public async Task TaskValueOptionalSelectManyAsyncOfSomeShouldProduceExpectedResultFuncTValueOptionalTResult()
    {
        // Given
        ValueOptional<int> optional = 21;

        // When
        ValueOptional<int> actual = await Task.FromResult(optional).SelectManyAsync(value => ValueOptional<int>.Some(value * 2), TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(ValueOptional<int>.Some(42), actual);
    }

    [Fact(DisplayName = "Task<ValueOptional<T>>.SelectManyAsync of None should produce the expected result (Func<T, ValueOptional<TResult>>).")]
    public async Task TaskValueOptionalSelectManyAsyncOfNoneShouldProduceExpectedResultFuncTValueOptionalTResult()
    {
        // Given
        ValueOptional<int> optional = ValueOptional<int>.None;

        // When
        ValueOptional<int> actual = await Task.FromResult(optional).SelectManyAsync(value => ValueOptional<int>.Some(value * 2), TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(ValueOptional<int>.None, actual);
    }

    [Fact(DisplayName = "Task<ValueOptional<T>>.SelectManyAsync of Some should produce the expected result (Func<T, Task<ValueOptional<TResult>>>).")]
    public async Task TaskValueOptionalSelectManyAsyncOfSomeShouldProduceExpectedResultFuncTTaskValueOptionalTResult()
    {
        // Given
        ValueOptional<int> optional = 21;

        // When
        ValueOptional<int> actual = await Task.FromResult(optional).SelectManyAsync(value => Task.FromResult<ValueOptional<int>>(value * 2), TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(ValueOptional<int>.Some(42), actual);
    }

    [Fact(DisplayName = "Task<ValueOptional<T>>.SelectManyAsync of Some should produce the expected result (Func<T, ValueTask<ValueOptional<TResult>>>).")]
    public async Task TaskValueOptionalSelectManyAsyncOfSomeShouldProduceExpectedResultFuncTTaskValueOptionalTResultWithValueTaskDelegates()
    {
        // Given
        ValueOptional<int> optional = 21;

        // When
        ValueOptional<int> actual = await Task.FromResult(optional).SelectManyAsync(value => ValueTask.FromResult<ValueOptional<int>>(value * 2), TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(ValueOptional<int>.Some(42), actual);
    }

    [Fact(DisplayName = "Task<ValueOptional<T>>.SelectManyAsync of None should produce the expected result (Func<T, Task<ValueOptional<TResult>>>).")]
    public async Task TaskValueOptionalSelectManyAsyncOfNoneShouldProduceExpectedResultFuncTTaskValueOptionalTResult()
    {
        // Given
        ValueOptional<int> optional = ValueOptional<int>.None;

        // When
        ValueOptional<int> actual = await Task.FromResult(optional).SelectManyAsync(value => Task.FromResult<ValueOptional<int>>(value * 2), TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(ValueOptional<int>.None, actual);
    }

    [Fact(DisplayName = "Task<ValueOptional<T>>.SelectManyAsync of None should produce the expected result (Func<T, ValueTask<ValueOptional<TResult>>>).")]
    public async Task TaskValueOptionalSelectManyAsyncOfNoneShouldProduceExpectedResultFuncTTaskValueOptionalTResultWithValueTaskDelegates()
    {
        // Given
        ValueOptional<int> optional = ValueOptional<int>.None;

        // When
        ValueOptional<int> actual = await Task.FromResult(optional).SelectManyAsync(value => ValueTask.FromResult<ValueOptional<int>>(value * 2), TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(ValueOptional<int>.None, actual);
    }

    [Fact(DisplayName = "Task<ValueOptional<T>>.SelectManyAsync of Some should produce the expected result (Func<T, CancellationToken, Task<ValueOptional<TResult>>>).")]
    public async Task TaskValueOptionalSelectManyAsyncOfSomeShouldProduceExpectedResultFuncTCancellationTokenTaskValueOptionalTResult()
    {
        // Given
        ValueOptional<int> optional = 21;
        CancellationToken selectorToken = CancellationToken.None;

        // When
        ValueOptional<int> actual = await Task.FromResult(optional).SelectManyAsync((value, ct) => { selectorToken = ct; return Task.FromResult<ValueOptional<int>>(value * 2); }, TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(ValueOptional<int>.Some(42), actual);
        Assert.Equal(TestContext.Current.CancellationToken, selectorToken);
    }

    [Fact(DisplayName = "Task<ValueOptional<T>>.SelectManyAsync of Some should produce the expected result (Func<T, CancellationToken, ValueTask<ValueOptional<TResult>>>).")]
    public async Task TaskValueOptionalSelectManyAsyncOfSomeShouldProduceExpectedResultFuncTCancellationTokenTaskValueOptionalTResultWithValueTaskDelegates()
    {
        // Given
        ValueOptional<int> optional = 21;
        CancellationToken selectorToken = CancellationToken.None;

        // When
        ValueOptional<int> actual = await Task.FromResult(optional).SelectManyAsync((value, ct) => { selectorToken = ct; return ValueTask.FromResult<ValueOptional<int>>(value * 2); }, TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(ValueOptional<int>.Some(42), actual);
        Assert.Equal(TestContext.Current.CancellationToken, selectorToken);
    }

    [Fact(DisplayName = "Task<ValueOptional<T>>.SelectManyAsync of None should produce the expected result (Func<T, CancellationToken, Task<ValueOptional<TResult>>>).")]
    public async Task TaskValueOptionalSelectManyAsyncOfNoneShouldProduceExpectedResultFuncTCancellationTokenTaskValueOptionalTResult()
    {
        // Given
        ValueOptional<int> optional = ValueOptional<int>.None;
        CancellationToken selectorToken = CancellationToken.None;

        // When
        ValueOptional<int> actual = await Task.FromResult(optional).SelectManyAsync((value, ct) => { selectorToken = ct; return Task.FromResult<ValueOptional<int>>(value * 2); }, TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(ValueOptional<int>.None, actual);
    }

    [Fact(DisplayName = "Task<ValueOptional<T>>.SelectManyAsync of None should produce the expected result (Func<T, CancellationToken, ValueTask<ValueOptional<TResult>>>).")]
    public async Task TaskValueOptionalSelectManyAsyncOfNoneShouldProduceExpectedResultFuncTCancellationTokenTaskValueOptionalTResultWithValueTaskDelegates()
    {
        // Given
        ValueOptional<int> optional = ValueOptional<int>.None;
        CancellationToken selectorToken = CancellationToken.None;

        // When
        ValueOptional<int> actual = await Task.FromResult(optional).SelectManyAsync((value, ct) => { selectorToken = ct; return ValueTask.FromResult<ValueOptional<int>>(value * 2); }, TestContext.Current.CancellationToken);

        // Then
        Assert.Equal(ValueOptional<int>.None, actual);
    }

    [Fact(DisplayName = "Task<ValueOptional<T>>.ToResultAsync of Some should produce a successful result containing the current optional.")]
    public async Task TaskValueOptionalToResultAsyncOfSomeShouldProduceSuccessfulResultContainingCurrentOptional()
    {
        // Given
        ValueOptional<int> optional = 21;

        // When
        ValueResult<ValueOptional<int>> actual = await Task.FromResult(optional).ToResultAsync(TestContext.Current.CancellationToken);

        // Then
        Assert.True(actual.IsSuccess);
        Assert.Equal(optional, actual.GetValueOrThrow());
    }

    [Fact(DisplayName = "Task<ValueOptional<T>>.ToResultAsync of None should produce a successful result containing the current optional.")]
    public async Task TaskValueOptionalToResultAsyncOfNoneShouldProduceSuccessfulResultContainingCurrentOptional()
    {
        // Given
        ValueOptional<int> optional = ValueOptional<int>.None;

        // When
        ValueResult<ValueOptional<int>> actual = await Task.FromResult(optional).ToResultAsync(TestContext.Current.CancellationToken);

        // Then
        Assert.True(actual.IsSuccess);
        Assert.Equal(optional, actual.GetValueOrThrow());
    }
}
