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
using System.Globalization;
using BenchmarkDotNet.Attributes;

namespace OnixLabs.Numerics.Benchmarks;

/// <summary>
/// Represents benchmarks for <see cref="UInt256"/> multiplication, division, formatting and parsing.
/// </summary>
// Why: not sealed, because BenchmarkDotNet generates a class that derives from each benchmark class.
public class UInt256Benchmarks
{
    private readonly UInt256 dividend = new(upper: UInt128.MaxValue >> 3, lower: UInt128.MaxValue / 7);
    private readonly UInt256 wideDivisor = new(upper: UInt128.MaxValue >> 70, lower: UInt128.MaxValue / 11);
    private readonly UInt256 narrowDivisor = new(upper: UInt128.Zero, lower: UInt128.MaxValue / 13);
    private readonly string text;

    /// <summary>
    /// Initializes a new instance of the <see cref="UInt256Benchmarks"/> class.
    /// </summary>
    public UInt256Benchmarks() => text = dividend.ToString(format: null, CultureInfo.InvariantCulture);

    /// <summary>
    /// Multiplies two values, wrapping on overflow.
    /// </summary>
    /// <returns>Returns the product.</returns>
    [Benchmark(Baseline = true)]
    public UInt256 Multiply() => dividend * narrowDivisor;

    /// <summary>
    /// Divides by a divisor wider than 128 bits, which takes the general division path.
    /// </summary>
    /// <returns>Returns the quotient.</returns>
    [Benchmark]
    public UInt256 DivideByWide() => dividend / wideDivisor;

    /// <summary>
    /// Divides by a divisor that fits in 128 bits, which takes the 256-by-128-bit division path used by floating-point division.
    /// </summary>
    /// <returns>Returns the quotient.</returns>
    [Benchmark]
    public UInt256 DivideByNarrow() => dividend / narrowDivisor;

    /// <summary>
    /// Formats a value as a decimal string.
    /// </summary>
    /// <returns>Returns the formatted string.</returns>
    [Benchmark]
    public string Format() => dividend.ToString(format: null, CultureInfo.InvariantCulture);

    /// <summary>
    /// Parses a decimal string.
    /// </summary>
    /// <returns>Returns the parsed value.</returns>
    [Benchmark]
    public UInt256 Parse() => UInt256.Parse(text, CultureInfo.InvariantCulture);
}
