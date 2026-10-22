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

public sealed class ValueTaskExtensionTests
{
    [Fact(DisplayName = "ValueTask.WaitAsync should produce the expected result when the ValueTask has already completed.")]
    public async Task ValueTaskWaitAsyncShouldProduceExpectedResultWhenValueTaskHasAlreadyCompleted()
    {
        // Given
        ValueTask<int> completed = ValueTask.FromResult(123);

        // When
        ValueTask<int> waiting = completed.WaitAsync(TestContext.Current.CancellationToken);

        // Then
        Assert.True(waiting.IsCompletedSuccessfully);
        Assert.Equal(123, await waiting);
    }

    [Fact(DisplayName = "ValueTask.WaitAsync should produce the expected result when the ValueTask has already completed and the token is cancelled.")]
    public async Task ValueTaskWaitAsyncShouldProduceExpectedResultWhenValueTaskHasAlreadyCompletedAndTokenIsCancelled()
    {
        // Given
        ValueTask<int> completed = ValueTask.FromResult(123);
        CancellationToken token = new(canceled: true);

        // When
        int actual = await completed.WaitAsync(token);

        // Then
        Assert.Equal(123, actual);
    }

    [Fact(DisplayName = "ValueTask.WaitAsync should produce the expected result when the ValueTask completes after waiting begins.")]
    public async Task ValueTaskWaitAsyncShouldProduceExpectedResultWhenValueTaskCompletesAfterWaitingBegins()
    {
        // Given
        TaskCompletionSource<int> source = new();
        ValueTask<int> waiting = new ValueTask<int>(source.Task).WaitAsync(TestContext.Current.CancellationToken);

        // When
        source.SetResult(123);
        int actual = await waiting;

        // Then
        Assert.Equal(123, actual);
    }

    [Fact(DisplayName = "ValueTask.WaitAsync should throw OperationCanceledException when cancelled before the ValueTask completes.")]
    public async Task ValueTaskWaitAsyncShouldThrowOperationCanceledExceptionWhenCancelledBeforeValueTaskCompletes()
    {
        // Given
        TaskCompletionSource<int> source = new();
        using CancellationTokenSource cancellation = new();
        ValueTask<int> waiting = new ValueTask<int>(source.Task).WaitAsync(cancellation.Token);

        // When
        await cancellation.CancelAsync();
        source.SetResult(123);

        // Then
        await Assert.ThrowsAnyAsync<OperationCanceledException>(async () => await waiting);
    }

    [Fact(DisplayName = "ValueTask.WaitAsync should throw OperationCanceledException when the token is cancelled and the ValueTask has not completed.")]
    public async Task ValueTaskWaitAsyncShouldThrowOperationCanceledExceptionWhenTokenIsCancelledAndValueTaskHasNotCompleted()
    {
        // Given
        TaskCompletionSource<int> source = new();
        CancellationToken token = new(canceled: true);

        // When
        ValueTask<int> waiting = new ValueTask<int>(source.Task).WaitAsync(token);

        // Then
        await Assert.ThrowsAnyAsync<OperationCanceledException>(async () => await waiting);
    }

    [Fact(DisplayName = "ValueTask.WaitAsync should propagate the exception when the ValueTask faults after waiting begins.")]
    public async Task ValueTaskWaitAsyncShouldPropagateExceptionWhenValueTaskFaultsAfterWaitingBegins()
    {
        // Given
        TaskCompletionSource<int> source = new();
        Exception expected = new InvalidOperationException("Faulted");
        ValueTask<int> waiting = new ValueTask<int>(source.Task).WaitAsync(TestContext.Current.CancellationToken);

        // When
        source.SetException(expected);
        Exception actual = await Assert.ThrowsAsync<InvalidOperationException>(async () => await waiting);

        // Then
        Assert.Same(expected, actual);
    }
}
