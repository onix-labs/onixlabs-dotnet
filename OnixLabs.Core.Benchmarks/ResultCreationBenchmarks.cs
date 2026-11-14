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
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;

namespace OnixLabs.Core.Benchmarks;

/// <summary>
/// Represents benchmarks that compare creating a <see cref="Result"/> with creating a <see cref="ValueResult"/>.
/// </summary>
/// <remarks>
/// Each category pairs a <see cref="Result"/> baseline with the equivalent <see cref="ValueResult"/> operation.
/// The exception is created once, so that the failure benchmarks measure only the cost of the result that wraps it.
/// </remarks>
// Why: not sealed, because BenchmarkDotNet generates a class that derives from each benchmark class.
[CategoriesColumn]
[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
public class ResultCreationBenchmarks
{
    private static readonly Exception FailureException = new InvalidOperationException("Failure");

    /// <summary>
    /// Creates a successful <see cref="Result"/>.
    /// </summary>
    /// <returns>Returns a successful <see cref="Result"/>.</returns>
    [Benchmark(Baseline = true), BenchmarkCategory("Success")]
    public Result ResultSuccess() => Result.Success();

    /// <summary>
    /// Creates a successful <see cref="ValueResult"/>.
    /// </summary>
    /// <returns>Returns a successful <see cref="ValueResult"/>.</returns>
    [Benchmark, BenchmarkCategory("Success")]
    public ValueResult ValueResultSuccess() => ValueResult.Success();

    /// <summary>
    /// Creates a failed <see cref="Result"/> from an existing exception.
    /// </summary>
    /// <returns>Returns a failed <see cref="Result"/>.</returns>
    [Benchmark(Baseline = true), BenchmarkCategory("Failure")]
    public Result ResultFailure() => Result.Failure(FailureException);

    /// <summary>
    /// Creates a failed <see cref="ValueResult"/> from an existing exception.
    /// </summary>
    /// <returns>Returns a failed <see cref="ValueResult"/>.</returns>
    [Benchmark, BenchmarkCategory("Failure")]
    public ValueResult ValueResultFailure() => ValueResult.Failure(FailureException);

    /// <summary>
    /// Creates a <see cref="Result"/> from an action that completes.
    /// </summary>
    /// <returns>Returns a successful <see cref="Result"/>.</returns>
    [Benchmark(Baseline = true), BenchmarkCategory("Of (completes)")]
    public Result ResultOfCompletes() => Result.Of(static () => { });

    /// <summary>
    /// Creates a <see cref="ValueResult"/> from an action that completes.
    /// </summary>
    /// <returns>Returns a successful <see cref="ValueResult"/>.</returns>
    [Benchmark, BenchmarkCategory("Of (completes)")]
    public ValueResult ValueResultOfCompletes() => ValueResult.Of(static () => { });

    /// <summary>
    /// Creates a <see cref="Result"/> from an action that throws.
    /// </summary>
    /// <returns>Returns a failed <see cref="Result"/>.</returns>
    [Benchmark(Baseline = true), BenchmarkCategory("Of (throws)")]
    public Result ResultOfThrows() => Result.Of(static () => throw FailureException);

    /// <summary>
    /// Creates a <see cref="ValueResult"/> from an action that throws.
    /// </summary>
    /// <returns>Returns a failed <see cref="ValueResult"/>.</returns>
    [Benchmark, BenchmarkCategory("Of (throws)")]
    public ValueResult ValueResultOfThrows() => ValueResult.Of(static () => throw FailureException);
}
