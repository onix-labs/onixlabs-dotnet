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
/// Represents benchmarks that compare operations on an existing <see cref="Optional{T}"/> with the same operations on a <see cref="ValueOptional{T}"/>.
/// </summary>
/// <remarks>
/// Each category pairs an <see cref="Optional{T}"/> baseline with the equivalent <see cref="ValueOptional{T}"/> operation, for an <see cref="int"/> value,
/// and runs once for an optional that holds a value and once for one that does not. Delegates are static lambdas, which the compiler caches, so
/// that they do not contribute allocations.
/// </remarks>
// Why: not sealed, because BenchmarkDotNet generates a class that derives from each benchmark class.
[CategoriesColumn]
[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory, BenchmarkLogicalGroupRule.ByParams)]
public class OptionalOperationBenchmarks
{
    private Optional<int> optional = Optional<int>.Some(123);
    private Optional<int> otherOptional = Optional<int>.Some(123);
    private ValueOptional<int> valueOptional = ValueOptional<int>.Some(123);
    private ValueOptional<int> otherValueOptional = ValueOptional<int>.Some(123);

    /// <summary>
    /// Gets or sets a value indicating whether the optionals under test hold a value.
    /// </summary>
    /// <value><see langword="true"/> if the optionals under test hold a value; otherwise, <see langword="false"/>.</value>
    [Params(true, false)]
    public bool HasValue { get; set; }

    /// <summary>
    /// Creates the optionals under test in the state given by <see cref="HasValue"/>.
    /// </summary>
    [GlobalSetup]
    public void Setup()
    {
        optional = HasValue ? Optional<int>.Some(123) : Optional<int>.None;
        otherOptional = HasValue ? Optional<int>.Some(123) : Optional<int>.None;
        valueOptional = HasValue ? ValueOptional<int>.Some(123) : ValueOptional<int>.None;
        otherValueOptional = HasValue ? ValueOptional<int>.Some(123) : ValueOptional<int>.None;
    }

    /// <summary>
    /// Gets the value of an <see cref="Optional{T}"/>, or a default value.
    /// </summary>
    /// <returns>Returns the value of the optional, or the default value.</returns>
    [Benchmark(Baseline = true), BenchmarkCategory("GetValueOrDefault")]
    public int OptionalGetValueOrDefault() => optional.GetValueOrDefault(0);

    /// <summary>
    /// Gets the value of a <see cref="ValueOptional{T}"/>, or a default value.
    /// </summary>
    /// <returns>Returns the value of the optional, or the default value.</returns>
    [Benchmark, BenchmarkCategory("GetValueOrDefault")]
    public int ValueOptionalGetValueOrDefault() => valueOptional.GetValueOrDefault(0);

    /// <summary>
    /// Matches an <see cref="Optional{T}"/> with delegates.
    /// </summary>
    /// <returns>Returns the value produced by the matching delegate.</returns>
    [Benchmark(Baseline = true), BenchmarkCategory("Match")]
    public int OptionalMatch() => optional.Match(static value => value, static () => 0);

    /// <summary>
    /// Matches a <see cref="ValueOptional{T}"/> with delegates.
    /// </summary>
    /// <returns>Returns the value produced by the matching delegate.</returns>
    [Benchmark, BenchmarkCategory("Match")]
    public int ValueOptionalMatch() => valueOptional.Match(static value => value, static () => 0);

    /// <summary>
    /// Maps the value of an <see cref="Optional{T}"/>.
    /// </summary>
    /// <returns>Returns the result of applying the selector.</returns>
    [Benchmark(Baseline = true), BenchmarkCategory("Select")]
    public Optional<int> OptionalSelect() => optional.Select(static value => value + 1);

    /// <summary>
    /// Maps the value of a <see cref="ValueOptional{T}"/>.
    /// </summary>
    /// <returns>Returns the result of applying the selector.</returns>
    [Benchmark, BenchmarkCategory("Select")]
    public ValueOptional<int> ValueOptionalSelect() => valueOptional.Select(static value => value + 1);

    /// <summary>
    /// Binds the value of an <see cref="Optional{T}"/> to a selector that returns an optional holding a value.
    /// </summary>
    /// <returns>Returns the optional produced by the selector.</returns>
    [Benchmark(Baseline = true), BenchmarkCategory("SelectMany")]
    public Optional<int> OptionalSelectMany() => optional.SelectMany(static value => Optional<int>.Some(value * 2));

    /// <summary>
    /// Binds the value of a <see cref="ValueOptional{T}"/> to a selector that returns an optional holding a value.
    /// </summary>
    /// <returns>Returns the optional produced by the selector.</returns>
    [Benchmark, BenchmarkCategory("SelectMany")]
    public ValueOptional<int> ValueOptionalSelectMany() => valueOptional.SelectMany(static value => ValueOptional<int>.Some(value * 2));

    /// <summary>
    /// Compares two <see cref="Optional{T}"/> instances for equality.
    /// </summary>
    /// <returns>Returns <see langword="true"/> if the optionals are equal; otherwise, <see langword="false"/>.</returns>
    [Benchmark(Baseline = true), BenchmarkCategory("Equals")]
    public bool OptionalEquals() => optional == otherOptional;

    /// <summary>
    /// Compares two <see cref="ValueOptional{T}"/> instances for equality.
    /// </summary>
    /// <returns>Returns <see langword="true"/> if the optionals are equal; otherwise, <see langword="false"/>.</returns>
    [Benchmark, BenchmarkCategory("Equals")]
    public bool ValueOptionalEquals() => valueOptional == otherValueOptional;

    /// <summary>
    /// Runs an <see cref="Optional{T}"/> through a map, a bind and a match, as a typical caller would.
    /// </summary>
    /// <returns>Returns the value produced by the matching delegate.</returns>
    [Benchmark(Baseline = true), BenchmarkCategory("Pipeline")]
    public int OptionalPipeline() => optional
        .Select(static value => value + 1)
        .SelectMany(static value => Optional<int>.Some(value * 2))
        .Match(static value => value, static () => 0);

    /// <summary>
    /// Runs a <see cref="ValueOptional{T}"/> through a map, a bind and a match, as a typical caller would.
    /// </summary>
    /// <returns>Returns the value produced by the matching delegate.</returns>
    [Benchmark, BenchmarkCategory("Pipeline")]
    public int ValueOptionalPipeline() => valueOptional
        .Select(static value => value + 1)
        .SelectMany(static value => ValueOptional<int>.Some(value * 2))
        .Match(static value => value, static () => 0);
}
