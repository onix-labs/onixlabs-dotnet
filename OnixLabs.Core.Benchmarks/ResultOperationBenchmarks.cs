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
/// Represents benchmarks that compare operations on an existing <see cref="Result"/> with the same operations on a <see cref="ValueResult"/>.
/// </summary>
/// <remarks>
/// Each category pairs a <see cref="Result"/> baseline with the equivalent <see cref="ValueResult"/> operation, and runs once for a successful
/// result and once for a failed one. Delegates are static lambdas, which the compiler caches, so that they do not contribute allocations.
/// </remarks>
// Why: not sealed, because BenchmarkDotNet generates a class that derives from each benchmark class.
[CategoriesColumn]
[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory, BenchmarkLogicalGroupRule.ByParams)]
public class ResultOperationBenchmarks
{
    private static readonly Exception FailureException = new InvalidOperationException("Failure");

    private Result result = Result.Success();
    private Result otherResult = Result.Success();
    private ValueResult valueResult = ValueResult.Success();
    private ValueResult otherValueResult = ValueResult.Success();

    /// <summary>
    /// Gets or sets a value indicating whether the results under test are in a failed state.
    /// </summary>
    /// <value><see langword="true"/> if the results under test are in a failed state; otherwise, <see langword="false"/>.</value>
    [Params(false, true)]
    public bool IsFailure { get; set; }

    /// <summary>
    /// Creates the results under test in the state given by <see cref="IsFailure"/>.
    /// </summary>
    [GlobalSetup]
    public void Setup()
    {
        result = IsFailure ? Result.Failure(FailureException) : Result.Success();
        otherResult = IsFailure ? Result.Failure(FailureException) : Result.Success();
        valueResult = IsFailure ? ValueResult.Failure(FailureException) : ValueResult.Success();
        otherValueResult = IsFailure ? ValueResult.Failure(FailureException) : ValueResult.Success();
    }

    /// <summary>
    /// Matches a <see cref="Result"/> with delegates.
    /// </summary>
    /// <returns>Returns the value produced by the matching delegate.</returns>
    [Benchmark(Baseline = true), BenchmarkCategory("Match")]
    public int ResultMatch() => result.Match(static () => 1, static _ => 0);

    /// <summary>
    /// Matches a <see cref="ValueResult"/> with delegates.
    /// </summary>
    /// <returns>Returns the value produced by the matching delegate.</returns>
    [Benchmark, BenchmarkCategory("Match")]
    public int ValueResultMatch() => valueResult.Match(static () => 1, static _ => 0);

    /// <summary>
    /// Matches a <see cref="Result"/> with a switch expression over its closed hierarchy.
    /// </summary>
    /// <returns>Returns the value produced by the matching arm.</returns>
    [Benchmark(Baseline = true), BenchmarkCategory("Switch")]
    public int ResultSwitch() => result switch
    {
        Success => 1,
        Failure failure => failure.Exception.HResult & 0
    };

    /// <summary>
    /// Matches a <see cref="ValueResult"/> with a switch expression over its union case.
    /// </summary>
    /// <returns>Returns the value produced by the matching arm.</returns>
    [Benchmark, BenchmarkCategory("Switch")]
    public int ValueResultSwitch() => valueResult switch
    {
        Exception exception => exception.HResult & 0,
        null => 1
    };

    /// <summary>
    /// Applies a selector to a <see cref="Result"/>.
    /// </summary>
    /// <returns>Returns the result of applying the selector.</returns>
    [Benchmark(Baseline = true), BenchmarkCategory("Select")]
    public Result ResultSelect() => result.Select(static () => { });

    /// <summary>
    /// Applies a selector to a <see cref="ValueResult"/>.
    /// </summary>
    /// <returns>Returns the result of applying the selector.</returns>
    [Benchmark, BenchmarkCategory("Select")]
    public ValueResult ValueResultSelect() => valueResult.Select(static () => { });

    /// <summary>
    /// Applies a selector that returns a failed result to a <see cref="Result"/>, as a validation step would.
    /// </summary>
    /// <returns>Returns the result produced by the selector.</returns>
    [Benchmark(Baseline = true), BenchmarkCategory("SelectMany")]
    public Result ResultSelectMany() => result.SelectMany(static () => Result.Failure(FailureException));

    /// <summary>
    /// Applies a selector that returns a failed result to a <see cref="ValueResult"/>, as a validation step would.
    /// </summary>
    /// <returns>Returns the result produced by the selector.</returns>
    [Benchmark, BenchmarkCategory("SelectMany")]
    public ValueResult ValueResultSelectMany() => valueResult.SelectMany(static () => ValueResult.Failure(FailureException));

    /// <summary>
    /// Compares two <see cref="Result"/> instances for equality.
    /// </summary>
    /// <returns>Returns <see langword="true"/> if the results are equal; otherwise, <see langword="false"/>.</returns>
    [Benchmark(Baseline = true), BenchmarkCategory("Equals")]
    public bool ResultEquals() => result == otherResult;

    /// <summary>
    /// Compares two <see cref="ValueResult"/> instances for equality.
    /// </summary>
    /// <returns>Returns <see langword="true"/> if the results are equal; otherwise, <see langword="false"/>.</returns>
    [Benchmark, BenchmarkCategory("Equals")]
    public bool ValueResultEquals() => valueResult == otherValueResult;

    /// <summary>
    /// Runs a <see cref="Result"/> through two steps and matches the outcome, as a typical caller would.
    /// </summary>
    /// <returns>Returns the value produced by the matching delegate.</returns>
    [Benchmark(Baseline = true), BenchmarkCategory("Pipeline")]
    public int ResultPipeline() => result
        .Select(static () => { })
        .SelectMany(Result.Success)
        .Match(static () => 1, static _ => 0);

    /// <summary>
    /// Runs a <see cref="ValueResult"/> through two steps and matches the outcome, as a typical caller would.
    /// </summary>
    /// <returns>Returns the value produced by the matching delegate.</returns>
    [Benchmark, BenchmarkCategory("Pipeline")]
    public int ValueResultPipeline() => valueResult
        .Select(static () => { })
        .SelectMany(ValueResult.Success)
        .Match(static () => 1, static _ => 0);
}
