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

using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;

namespace OnixLabs.Core.Benchmarks;

/// <summary>
/// Represents benchmarks that compare the asynchronous members of the reference types with those of the value types.
/// </summary>
/// <remarks>
/// <para>
/// Each category pairs a <see cref="Result{T}"/> or <see cref="Optional{T}"/> baseline with the equivalent <see cref="ValueResult{T}"/> or
/// <see cref="ValueOptional{T}"/> operation, for an <see cref="int"/> value that is present. The categories differ in what the awaited delegate does:
/// </para>
/// <list type="bullet">
/// <item><description>"completes": the delegate returns a <see cref="ValueTask{TResult}"/> that has already completed, so nothing suspends.</description></item>
/// <item><description>"Task": the delegate returns a <see cref="Task{TResult}"/> that has already completed, which allocates the task itself.</description></item>
/// <item><description>"yields": the delegate suspends with <see cref="Task.Yield"/>, so the asynchronous machinery allocates and the thread pool resumes it.</description></item>
/// </list>
/// </remarks>
// Why: not sealed, because BenchmarkDotNet generates a class that derives from each benchmark class.
[CategoriesColumn]
[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
public class AsyncBenchmarks
{
    private Result<int> result = Result<int>.Success(123);
    private ValueResult<int> valueResult = ValueResult<int>.Success(123);
    private Optional<int> optional = Optional<int>.Some(123);
    private ValueOptional<int> valueOptional = ValueOptional<int>.Some(123);

    /// <summary>
    /// Maps a <see cref="Result{T}"/> with a delegate whose <see cref="ValueTask{TResult}"/> has already completed.
    /// </summary>
    /// <returns>Returns the mapped value.</returns>
    [Benchmark(Baseline = true), BenchmarkCategory("Result SelectAsync (completes)")]
    public async ValueTask<int> ResultSelectAsyncCompletes() =>
        (await result.SelectAsync(static value => ValueTask.FromResult(value + 1)).ConfigureAwait(false)).GetValueOrDefault(0);

    /// <summary>
    /// Maps a <see cref="ValueResult{T}"/> with a delegate whose <see cref="ValueTask{TResult}"/> has already completed.
    /// </summary>
    /// <returns>Returns the mapped value.</returns>
    [Benchmark, BenchmarkCategory("Result SelectAsync (completes)")]
    public async ValueTask<int> ValueResultSelectAsyncCompletes() =>
        (await valueResult.SelectAsync(static value => ValueTask.FromResult(value + 1)).ConfigureAwait(false)).GetValueOrDefault(0);

    /// <summary>
    /// Maps a <see cref="Result{T}"/> with a delegate whose <see cref="Task{TResult}"/> has already completed.
    /// </summary>
    /// <returns>Returns the mapped value.</returns>
    [Benchmark(Baseline = true), BenchmarkCategory("Result SelectAsync (Task)")]
    public async Task<int> ResultSelectAsyncTask() =>
        (await result.SelectAsync(static value => Task.FromResult(value + 1)).ConfigureAwait(false)).GetValueOrDefault(0);

    /// <summary>
    /// Maps a <see cref="ValueResult{T}"/> with a delegate whose <see cref="Task{TResult}"/> has already completed.
    /// </summary>
    /// <returns>Returns the mapped value.</returns>
    [Benchmark, BenchmarkCategory("Result SelectAsync (Task)")]
    public async Task<int> ValueResultSelectAsyncTask() =>
        (await valueResult.SelectAsync(static value => Task.FromResult(value + 1)).ConfigureAwait(false)).GetValueOrDefault(0);

    /// <summary>
    /// Maps a <see cref="Result{T}"/> with a delegate that suspends.
    /// </summary>
    /// <returns>Returns the mapped value.</returns>
    [Benchmark(Baseline = true), BenchmarkCategory("Result SelectAsync (yields)")]
    public async ValueTask<int> ResultSelectAsyncYields() =>
        (await result.SelectAsync(static async ValueTask<int> (int value) =>
        {
            await Task.Yield();
            return value + 1;
        }).ConfigureAwait(false)).GetValueOrDefault(0);

    /// <summary>
    /// Maps a <see cref="ValueResult{T}"/> with a delegate that suspends.
    /// </summary>
    /// <returns>Returns the mapped value.</returns>
    [Benchmark, BenchmarkCategory("Result SelectAsync (yields)")]
    public async ValueTask<int> ValueResultSelectAsyncYields() =>
        (await valueResult.SelectAsync(static async ValueTask<int> (int value) =>
        {
            await Task.Yield();
            return value + 1;
        }).ConfigureAwait(false)).GetValueOrDefault(0);

    /// <summary>
    /// Matches a <see cref="Result{T}"/> with a delegate whose <see cref="ValueTask{TResult}"/> has already completed.
    /// </summary>
    /// <returns>Returns the value produced by the matching delegate.</returns>
    [Benchmark(Baseline = true), BenchmarkCategory("Result MatchAsync (completes)")]
    public ValueTask<int> ResultMatchAsyncCompletes() => result.MatchAsync(static value => ValueTask.FromResult(value), static _ => 0);

    /// <summary>
    /// Matches a <see cref="ValueResult{T}"/> with a delegate whose <see cref="ValueTask{TResult}"/> has already completed.
    /// </summary>
    /// <returns>Returns the value produced by the matching delegate.</returns>
    [Benchmark, BenchmarkCategory("Result MatchAsync (completes)")]
    public ValueTask<int> ValueResultMatchAsyncCompletes() => valueResult.MatchAsync(static value => ValueTask.FromResult(value), static _ => 0);

    /// <summary>
    /// Maps an <see cref="Optional{T}"/> with a delegate whose <see cref="ValueTask{TResult}"/> has already completed.
    /// </summary>
    /// <returns>Returns the mapped value.</returns>
    [Benchmark(Baseline = true), BenchmarkCategory("Optional SelectAsync (completes)")]
    public async ValueTask<int> OptionalSelectAsyncCompletes() =>
        (await optional.SelectAsync(static value => ValueTask.FromResult(value + 1)).ConfigureAwait(false)).GetValueOrDefault(0);

    /// <summary>
    /// Maps a <see cref="ValueOptional{T}"/> with a delegate whose <see cref="ValueTask{TResult}"/> has already completed.
    /// </summary>
    /// <returns>Returns the mapped value.</returns>
    [Benchmark, BenchmarkCategory("Optional SelectAsync (completes)")]
    public async ValueTask<int> ValueOptionalSelectAsyncCompletes() =>
        (await valueOptional.SelectAsync(static value => ValueTask.FromResult(value + 1)).ConfigureAwait(false)).GetValueOrDefault(0);
}
