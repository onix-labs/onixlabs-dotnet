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

namespace OnixLabs.Core.Benchmarks;

/// <summary>
/// Represents benchmarks that compare a switch expression over many <see cref="Result{T}"/> values with one over many <see cref="ValueResult{T}"/> values.
/// </summary>
/// <remarks>
/// <see cref="ResultGenericOperationBenchmarks"/> measures one switch per call, so every call pays the fixed cost of the method's prologue and
/// epilogue, which is larger for <see cref="ValueResult{T}"/> because the compiler's <see cref="System.Runtime.CompilerServices.SwitchExpressionException"/>
/// fallback boxes the union. These benchmarks switch over an array, as a caller processing a batch of results would, so that fixed cost is paid once
/// per batch. Even-indexed results are successes and odd-indexed results are failures.
/// </remarks>
// Why: not sealed, because BenchmarkDotNet generates a class that derives from each benchmark class.
public class ResultGenericSwitchLoopBenchmarks
{
    private const int Count = 64;
    private static readonly Exception FailureException = new InvalidOperationException("Failure");

    private readonly Result<int>[] results = new Result<int>[Count];
    private readonly ValueResult<int>[] valueResults = new ValueResult<int>[Count];

    /// <summary>
    /// Creates the results under test, alternating successes and failures.
    /// </summary>
    [GlobalSetup]
    public void Setup()
    {
        for (int index = 0; index < Count; index++)
        {
            results[index] = index % 2 == 0 ? Result<int>.Success(index) : Result<int>.Failure(FailureException);
            valueResults[index] = index % 2 == 0 ? ValueResult<int>.Success(index) : ValueResult<int>.Failure(FailureException);
        }
    }

    /// <summary>
    /// Sums the values of an array of <see cref="Result{T}"/> with a switch expression over its closed hierarchy.
    /// </summary>
    /// <returns>Returns the sum of the successful values, counting each failure as -1.</returns>
    [Benchmark(Baseline = true)]
    public int ResultSwitchLoop()
    {
        int sum = 0;

        foreach (Result<int> result in results)
            sum += result switch { Success<int> success => success.Value, Failure<int> => -1 };

        return sum;
    }

    /// <summary>
    /// Sums the values of an array of <see cref="ValueResult{T}"/> with a switch expression over its union cases.
    /// </summary>
    /// <returns>Returns the sum of the successful values, counting each failure as -1.</returns>
    [Benchmark]
    public int ValueResultSwitchLoop()
    {
        int sum = 0;

        foreach (ValueResult<int> result in valueResults)
            sum += result switch { int value => value, Exception => -1 };

        return sum;
    }
}
