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

using System.Globalization;
using BenchmarkDotNet.Attributes;

namespace OnixLabs.Numerics.Benchmarks;

/// <summary>
/// Represents benchmarks for <see cref="BigDecimal"/> arithmetic, comparison, formatting, parsing and conversion.
/// </summary>
/// <remarks>
/// The operands have different scales and an unscaled value beyond 64 bits, so arithmetic and comparison take the scale-alignment path
/// and the underlying <see cref="System.Numerics.BigInteger"/> values allocate, as they do for real monetary and scientific values.
/// </remarks>
// Why: not sealed, because BenchmarkDotNet generates a class that derives from each benchmark class.
public class BigDecimalBenchmarks
{
    private const string LeftText = "12345678901234567890.123456";
    private const string RightText = "98765.4321";

    private readonly BigDecimal left = BigDecimal.Parse(LeftText, CultureInfo.InvariantCulture);
    private readonly BigDecimal right = BigDecimal.Parse(RightText, CultureInfo.InvariantCulture);
    private readonly double doubleValue = 1234.5678;

    /// <summary>
    /// Adds two values with different scales.
    /// </summary>
    /// <returns>Returns the sum.</returns>
    [Benchmark(Baseline = true)]
    public BigDecimal Add() => left + right;

    /// <summary>
    /// Multiplies two values.
    /// </summary>
    /// <returns>Returns the product.</returns>
    [Benchmark]
    public BigDecimal Multiply() => left * right;

    /// <summary>
    /// Divides one value by another.
    /// </summary>
    /// <returns>Returns the quotient.</returns>
    [Benchmark]
    public BigDecimal Divide() => left / right;

    /// <summary>
    /// Compares two values with different scales.
    /// </summary>
    /// <returns>Returns the comparison result.</returns>
    [Benchmark]
    public int Compare() => left.CompareTo(right);

    /// <summary>
    /// Formats a value with the general format.
    /// </summary>
    /// <returns>Returns the formatted string.</returns>
    [Benchmark]
    public string Format() => left.ToString(format: null, CultureInfo.InvariantCulture);

    /// <summary>
    /// Parses a value.
    /// </summary>
    /// <returns>Returns the parsed value.</returns>
    [Benchmark]
    public BigDecimal Parse() => BigDecimal.Parse(LeftText, CultureInfo.InvariantCulture);

    /// <summary>
    /// Converts a <see cref="double"/> to a value, with the default conversion mode.
    /// </summary>
    /// <returns>Returns the converted value.</returns>
    [Benchmark]
    public BigDecimal FromDouble() => doubleValue;

    /// <summary>
    /// Converts a value to a <see cref="double"/>.
    /// </summary>
    /// <returns>Returns the converted value.</returns>
    [Benchmark]
    public double ToDouble() => (double)right;
}
