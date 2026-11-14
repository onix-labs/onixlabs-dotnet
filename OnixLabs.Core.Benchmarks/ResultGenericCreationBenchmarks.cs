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
/// Represents benchmarks that compare creating a <see cref="Result{T}"/> with creating a <see cref="ValueResult{T}"/>.
/// </summary>
/// <remarks>
/// Each category pairs a <see cref="Result{T}"/> baseline with the equivalent <see cref="ValueResult{T}"/> operation, for an <see cref="int"/> value.
/// The exception is created once, so that the failure benchmarks measure only the cost of the result that wraps it.
/// </remarks>
// Why: not sealed, because BenchmarkDotNet generates a class that derives from each benchmark class.
[CategoriesColumn]
[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
public class ResultGenericCreationBenchmarks
{
    private static readonly Exception FailureException = new InvalidOperationException("Failure");

    private int value = 123;

    /// <summary>
    /// Creates a successful <see cref="Result{T}"/>.
    /// </summary>
    /// <returns>Returns a successful <see cref="Result{T}"/>.</returns>
    [Benchmark(Baseline = true), BenchmarkCategory("Success")]
    public Result<int> ResultSuccess() => Result<int>.Success(value);

    /// <summary>
    /// Creates a successful <see cref="ValueResult{T}"/>.
    /// </summary>
    /// <returns>Returns a successful <see cref="ValueResult{T}"/>.</returns>
    [Benchmark, BenchmarkCategory("Success")]
    public ValueResult<int> ValueResultSuccess() => ValueResult<int>.Success(value);

    /// <summary>
    /// Creates a failed <see cref="Result{T}"/> from an existing exception.
    /// </summary>
    /// <returns>Returns a failed <see cref="Result{T}"/>.</returns>
    [Benchmark(Baseline = true), BenchmarkCategory("Failure")]
    public Result<int> ResultFailure() => Result<int>.Failure(FailureException);

    /// <summary>
    /// Creates a failed <see cref="ValueResult{T}"/> from an existing exception.
    /// </summary>
    /// <returns>Returns a failed <see cref="ValueResult{T}"/>.</returns>
    [Benchmark, BenchmarkCategory("Failure")]
    public ValueResult<int> ValueResultFailure() => ValueResult<int>.Failure(FailureException);

    /// <summary>
    /// Creates a <see cref="Result{T}"/> from a function that returns a value.
    /// </summary>
    /// <returns>Returns a successful <see cref="Result{T}"/>.</returns>
    [Benchmark(Baseline = true), BenchmarkCategory("Of (returns)")]
    public Result<int> ResultOfReturns() => Result<int>.Of(static () => 123);

    /// <summary>
    /// Creates a <see cref="ValueResult{T}"/> from a function that returns a value.
    /// </summary>
    /// <returns>Returns a successful <see cref="ValueResult{T}"/>.</returns>
    [Benchmark, BenchmarkCategory("Of (returns)")]
    public ValueResult<int> ValueResultOfReturns() => ValueResult<int>.Of(static () => 123);

    /// <summary>
    /// Creates a <see cref="Result{T}"/> from a function that throws.
    /// </summary>
    /// <returns>Returns a failed <see cref="Result{T}"/>.</returns>
    [Benchmark(Baseline = true), BenchmarkCategory("Of (throws)")]
    public Result<int> ResultOfThrows() => Result<int>.Of(static () => throw FailureException);

    /// <summary>
    /// Creates a <see cref="ValueResult{T}"/> from a function that throws.
    /// </summary>
    /// <returns>Returns a failed <see cref="ValueResult{T}"/>.</returns>
    [Benchmark, BenchmarkCategory("Of (throws)")]
    public ValueResult<int> ValueResultOfThrows() => ValueResult<int>.Of(static () => throw FailureException);
}
