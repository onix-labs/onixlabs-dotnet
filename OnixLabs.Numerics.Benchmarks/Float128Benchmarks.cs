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
/// Represents benchmarks for <see cref="Float128"/> arithmetic, square root, exponentiation, formatting, parsing and conversion.
/// </summary>
// Why: not sealed, because BenchmarkDotNet generates a class that derives from each benchmark class.
public class Float128Benchmarks
{
    private const string PiText = "3.14159265358979323846264338327950288";

    private readonly Float128 left = Float128.Pi;
    private readonly Float128 right = 3;
    private readonly UInt256 wideInteger = new(upper: UInt128.MaxValue >> 1, lower: UInt128.MaxValue);
    private readonly Float128 wideValue;

    /// <summary>
    /// Initializes a new instance of the <see cref="Float128Benchmarks"/> class.
    /// </summary>
    public Float128Benchmarks() => wideValue = (Float128)wideInteger;

    /// <summary>
    /// Multiplies two values.
    /// </summary>
    /// <returns>Returns the product.</returns>
    [Benchmark(Baseline = true)]
    public Float128 Multiply() => left * right;

    /// <summary>
    /// Divides one value by another.
    /// </summary>
    /// <returns>Returns the quotient.</returns>
    [Benchmark]
    public Float128 Divide() => left / right;

    /// <summary>
    /// Computes a square root.
    /// </summary>
    /// <returns>Returns the square root.</returns>
    [Benchmark]
    public Float128 Sqrt() => Float128.Sqrt(left);

    /// <summary>
    /// Computes e raised to a power, a series evaluation with one division per term.
    /// </summary>
    /// <returns>Returns the result.</returns>
    [Benchmark]
    public Float128 Exp() => Float128.Exp(left);

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
    public Float128 Parse() => Float128.Parse(PiText, CultureInfo.InvariantCulture);

    /// <summary>
    /// Converts a 255-bit integer to the nearest value.
    /// </summary>
    /// <returns>Returns the converted value.</returns>
    [Benchmark]
    public Float128 FromUInt256() => (Float128)wideInteger;

    /// <summary>
    /// Converts a value of about 2^254 to a 256-bit integer, truncating toward zero.
    /// </summary>
    /// <returns>Returns the converted value.</returns>
    [Benchmark]
    public UInt256 ToUInt256() => (UInt256)wideValue;
}
