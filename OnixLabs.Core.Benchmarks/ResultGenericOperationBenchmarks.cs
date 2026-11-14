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
/// Represents benchmarks that compare operations on an existing <see cref="Result{T}"/> with the same operations on a <see cref="ValueResult{T}"/>.
/// </summary>
/// <remarks>
/// Each category pairs a <see cref="Result{T}"/> baseline with the equivalent <see cref="ValueResult{T}"/> operation, for an <see cref="int"/> value,
/// and runs once for a successful result and once for a failed one. Delegates are static lambdas, which the compiler caches, so that they do not
/// contribute allocations.
/// </remarks>
// Why: not sealed, because BenchmarkDotNet generates a class that derives from each benchmark class.
[CategoriesColumn]
[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory, BenchmarkLogicalGroupRule.ByParams)]
public class ResultGenericOperationBenchmarks
{
    private static readonly Exception FailureException = new InvalidOperationException("Failure");

    private Result<int> result = Result<int>.Success(123);
    private Result<int> otherResult = Result<int>.Success(123);
    private ValueResult<int> valueResult = ValueResult<int>.Success(123);
    private ValueResult<int> otherValueResult = ValueResult<int>.Success(123);

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
        result = IsFailure ? Result<int>.Failure(FailureException) : Result<int>.Success(123);
        otherResult = IsFailure ? Result<int>.Failure(FailureException) : Result<int>.Success(123);
        valueResult = IsFailure ? ValueResult<int>.Failure(FailureException) : ValueResult<int>.Success(123);
        otherValueResult = IsFailure ? ValueResult<int>.Failure(FailureException) : ValueResult<int>.Success(123);
    }

    /// <summary>
    /// Gets the value of a <see cref="Result{T}"/>, or a default value.
    /// </summary>
    /// <returns>Returns the value of the result, or the default value.</returns>
    [Benchmark(Baseline = true), BenchmarkCategory("GetValueOrDefault")]
    public int ResultGetValueOrDefault() => result.GetValueOrDefault(0);

    /// <summary>
    /// Gets the value of a <see cref="ValueResult{T}"/>, or a default value.
    /// </summary>
    /// <returns>Returns the value of the result, or the default value.</returns>
    [Benchmark, BenchmarkCategory("GetValueOrDefault")]
    public int ValueResultGetValueOrDefault() => valueResult.GetValueOrDefault(0);

    /// <summary>
    /// Matches a <see cref="Result{T}"/> with delegates.
    /// </summary>
    /// <returns>Returns the value produced by the matching delegate.</returns>
    [Benchmark(Baseline = true), BenchmarkCategory("Match")]
    public int ResultMatch() => result.Match(static value => value, static _ => 0);

    /// <summary>
    /// Matches a <see cref="ValueResult{T}"/> with delegates.
    /// </summary>
    /// <returns>Returns the value produced by the matching delegate.</returns>
    [Benchmark, BenchmarkCategory("Match")]
    public int ValueResultMatch() => valueResult.Match(static value => value, static _ => 0);

    /// <summary>
    /// Matches a <see cref="Result{T}"/> with a switch expression over its closed hierarchy.
    /// </summary>
    /// <returns>Returns the value produced by the matching arm.</returns>
    [Benchmark(Baseline = true), BenchmarkCategory("Switch")]
    public int ResultSwitch() => result switch
    {
        Success<int> success => success.Value,
        Failure<int> failure => failure.Exception.HResult & 0
    };

    /// <summary>
    /// Matches a <see cref="ValueResult{T}"/> with a switch expression over its union cases.
    /// </summary>
    /// <returns>Returns the value produced by the matching arm.</returns>
    [Benchmark, BenchmarkCategory("Switch")]
    public int ValueResultSwitch() => valueResult switch
    {
        int value => value,
        Exception exception => exception.HResult & 0
    };

    /// <summary>
    /// Maps the value of a <see cref="Result{T}"/>.
    /// </summary>
    /// <returns>Returns the result of applying the selector.</returns>
    [Benchmark(Baseline = true), BenchmarkCategory("Select")]
    public Result<int> ResultSelect() => result.Select(static value => value + 1);

    /// <summary>
    /// Maps the value of a <see cref="ValueResult{T}"/>.
    /// </summary>
    /// <returns>Returns the result of applying the selector.</returns>
    [Benchmark, BenchmarkCategory("Select")]
    public ValueResult<int> ValueResultSelect() => valueResult.Select(static value => value + 1);

    /// <summary>
    /// Binds the value of a <see cref="Result{T}"/> to a selector that returns a successful result.
    /// </summary>
    /// <returns>Returns the result produced by the selector.</returns>
    [Benchmark(Baseline = true), BenchmarkCategory("SelectMany")]
    public Result<int> ResultSelectMany() => result.SelectMany(static value => Result<int>.Success(value * 2));

    /// <summary>
    /// Binds the value of a <see cref="ValueResult{T}"/> to a selector that returns a successful result.
    /// </summary>
    /// <returns>Returns the result produced by the selector.</returns>
    [Benchmark, BenchmarkCategory("SelectMany")]
    public ValueResult<int> ValueResultSelectMany() => valueResult.SelectMany(static value => ValueResult<int>.Success(value * 2));

    /// <summary>
    /// Compares two <see cref="Result{T}"/> instances for equality.
    /// </summary>
    /// <returns>Returns <see langword="true"/> if the results are equal; otherwise, <see langword="false"/>.</returns>
    [Benchmark(Baseline = true), BenchmarkCategory("Equals")]
    public bool ResultEquals() => result == otherResult;

    /// <summary>
    /// Compares two <see cref="ValueResult{T}"/> instances for equality.
    /// </summary>
    /// <returns>Returns <see langword="true"/> if the results are equal; otherwise, <see langword="false"/>.</returns>
    [Benchmark, BenchmarkCategory("Equals")]
    public bool ValueResultEquals() => valueResult == otherValueResult;

    /// <summary>
    /// Runs a <see cref="Result{T}"/> through a map, a bind and a match, as a typical caller would.
    /// </summary>
    /// <returns>Returns the value produced by the matching delegate.</returns>
    [Benchmark(Baseline = true), BenchmarkCategory("Pipeline")]
    public int ResultPipeline() => result
        .Select(static value => value + 1)
        .SelectMany(static value => Result<int>.Success(value * 2))
        .Match(static value => value, static _ => 0);

    /// <summary>
    /// Runs a <see cref="ValueResult{T}"/> through a map, a bind and a match, as a typical caller would.
    /// </summary>
    /// <returns>Returns the value produced by the matching delegate.</returns>
    [Benchmark, BenchmarkCategory("Pipeline")]
    public int ValueResultPipeline() => valueResult
        .Select(static value => value + 1)
        .SelectMany(static value => ValueResult<int>.Success(value * 2))
        .Match(static value => value, static _ => 0);
}
