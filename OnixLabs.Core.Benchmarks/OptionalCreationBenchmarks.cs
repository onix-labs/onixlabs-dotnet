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

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;

namespace OnixLabs.Core.Benchmarks;

/// <summary>
/// Represents benchmarks that compare creating an <see cref="Optional{T}"/> with creating a <see cref="ValueOptional{T}"/>.
/// </summary>
/// <remarks>
/// Each category pairs an <see cref="Optional{T}"/> baseline with the equivalent <see cref="ValueOptional{T}"/> operation, for an <see cref="int"/> value.
/// </remarks>
// Why: not sealed, because BenchmarkDotNet generates a class that derives from each benchmark class.
[CategoriesColumn]
[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
public class OptionalCreationBenchmarks
{
    private int value = 123;

    /// <summary>
    /// Creates an <see cref="Optional{T}"/> that holds a value.
    /// </summary>
    /// <returns>Returns an <see cref="Optional{T}"/> that holds a value.</returns>
    [Benchmark(Baseline = true), BenchmarkCategory("Some")]
    public Optional<int> OptionalSome() => Optional<int>.Some(value);

    /// <summary>
    /// Creates a <see cref="ValueOptional{T}"/> that holds a value.
    /// </summary>
    /// <returns>Returns a <see cref="ValueOptional{T}"/> that holds a value.</returns>
    [Benchmark, BenchmarkCategory("Some")]
    public ValueOptional<int> ValueOptionalSome() => ValueOptional<int>.Some(value);

    /// <summary>
    /// Gets an <see cref="Optional{T}"/> that has no value.
    /// </summary>
    /// <returns>Returns an <see cref="Optional{T}"/> that has no value.</returns>
    [Benchmark(Baseline = true), BenchmarkCategory("None")]
    public Optional<int> OptionalNone() => Optional<int>.None;

    /// <summary>
    /// Gets a <see cref="ValueOptional{T}"/> that has no value.
    /// </summary>
    /// <returns>Returns a <see cref="ValueOptional{T}"/> that has no value.</returns>
    [Benchmark, BenchmarkCategory("None")]
    public ValueOptional<int> ValueOptionalNone() => ValueOptional<int>.None;

    /// <summary>
    /// Creates an <see cref="Optional{T}"/> from a value, which treats the default value as absent.
    /// </summary>
    /// <returns>Returns an <see cref="Optional{T}"/> that holds a value.</returns>
    [Benchmark(Baseline = true), BenchmarkCategory("Of")]
    public Optional<int> OptionalOf() => Optional<int>.Of(value);

    /// <summary>
    /// Creates a <see cref="ValueOptional{T}"/> from a value, which treats the default value as absent.
    /// </summary>
    /// <returns>Returns a <see cref="ValueOptional{T}"/> that holds a value.</returns>
    [Benchmark, BenchmarkCategory("Of")]
    public ValueOptional<int> ValueOptionalOf() => ValueOptional<int>.Of(value);
}
