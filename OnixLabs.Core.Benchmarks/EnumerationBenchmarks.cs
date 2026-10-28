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

namespace OnixLabs.Core.Benchmarks;

/// <summary>
/// Represents benchmarks for looking up <see cref="Enumeration{T}"/> entries by name and by value.
/// </summary>
// Why: not sealed, because BenchmarkDotNet generates a class that derives from each benchmark class.
public class EnumerationBenchmarks
{
    /// <summary>
    /// Looks up an entry by name.
    /// </summary>
    /// <returns>Returns the matching entry.</returns>
    [Benchmark(Baseline = true)]
    public Weekday FromName() => Weekday.FromName(nameof(Weekday.Friday));

    /// <summary>
    /// Looks up an entry by value.
    /// </summary>
    /// <returns>Returns the matching entry.</returns>
    [Benchmark]
    public Weekday FromValue() => Weekday.FromValue(5);

    /// <summary>
    /// Represents a seven-entry enumeration, a typical size for an <see cref="Enumeration{T}"/>.
    /// </summary>
    public sealed class Weekday : Enumeration<Weekday>
    {
        /// <summary>
        /// Gets the Monday entry.
        /// </summary>
        public static readonly Weekday Monday = new(1, nameof(Monday));

        /// <summary>
        /// Gets the Tuesday entry.
        /// </summary>
        public static readonly Weekday Tuesday = new(2, nameof(Tuesday));

        /// <summary>
        /// Gets the Wednesday entry.
        /// </summary>
        public static readonly Weekday Wednesday = new(3, nameof(Wednesday));

        /// <summary>
        /// Gets the Thursday entry.
        /// </summary>
        public static readonly Weekday Thursday = new(4, nameof(Thursday));

        /// <summary>
        /// Gets the Friday entry.
        /// </summary>
        public static readonly Weekday Friday = new(5, nameof(Friday));

        /// <summary>
        /// Gets the Saturday entry.
        /// </summary>
        public static readonly Weekday Saturday = new(6, nameof(Saturday));

        /// <summary>
        /// Gets the Sunday entry.
        /// </summary>
        public static readonly Weekday Sunday = new(7, nameof(Sunday));

        /// <summary>
        /// Initializes a new instance of the <see cref="Weekday"/> class.
        /// </summary>
        /// <param name="value">The value of the entry.</param>
        /// <param name="name">The name of the entry.</param>
        private Weekday(int value, string name) : base(value, name)
        {
        }
    }
}
