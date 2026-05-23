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

namespace OnixLabs.Core;

/// <summary>
/// Represents the base class for the "smart enumeration" pattern: a class-based alternative to a C# <see langword="enum"/>
/// that allows each entry to carry behaviour and methods alongside its integer value and stable name.
/// </summary>
/// <typeparam name="T">The concrete derived type, constrained to <see cref="Enumeration{T}"/>.</typeparam>
public abstract partial class Enumeration<T> : IEquatable<T>, IComparable<T>, IComparable where T : Enumeration<T>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Enumeration{T}"/> class.
    /// </summary>
    /// <param name="value">The value of the enumeration entry.</param>
    /// <param name="name">The name of the enumeration entry. Must be non-null, non-empty, and non-whitespace.</param>
    /// <exception cref="ArgumentException">
    /// Thrown when <paramref name="name"/> is <see langword="null"/>, empty, or whitespace.
    /// </exception>
    protected Enumeration(int value, string name)
    {
        Value = value;
        Name = RequireNotNullOrWhiteSpace(name, "Enumeration name must not be null, empty, or whitespace.");
    }

    /// <summary>
    /// Gets the name of the enumeration entry.
    /// </summary>
    /// <value>A stable, non-null, non-empty identifier; conventionally matches the static field name (e.g. <c>nameof(Active)</c>).</value>
    public string Name { get; }

    /// <summary>
    /// Gets the value of the enumeration entry.
    /// </summary>
    /// <value>The integer value associated with the current enumeration entry.</value>
    public int Value { get; }
}
