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

using System.Collections.Frozen;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace OnixLabs.Core;

public abstract partial class Enumeration<T>
{
    /// <summary>
    /// Gets all the enumeration entries for the current type.
    /// </summary>
    /// <returns>Returns all the enumeration entries for the current type.</returns>
    public static IReadOnlySet<T> GetAll() => EnumerationCache.All;

    /// <summary>
    /// Gets all the enumeration entries, as value/name tuples, for the current type.
    /// </summary>
    /// <returns>Returns all the enumeration entries, as value/name tuples, for the current type.</returns>
    public static IReadOnlySet<(int Value, string Name)> GetEntries() => EnumerationCache.Entries;

    /// <summary>
    /// Gets all the enumeration names for the current type.
    /// </summary>
    /// <returns>Returns all the enumeration names for the current type.</returns>
    public static IReadOnlySet<string> GetNames() => EnumerationCache.Names;

    /// <summary>
    /// Gets all the enumeration values for the current type.
    /// </summary>
    /// <returns>Returns all the enumeration values for the current type.</returns>
    public static IReadOnlySet<int> GetValues() => EnumerationCache.Values;

    /// <summary>
    /// Represents a per-enumeration type cache of the discovered enumeration entries.
    /// The CLR initialises one static field per closed generic, so the reflection scan runs once per concrete enumeration type.
    /// </summary>
    private static class EnumerationCache
    {
        /// <summary>
        /// The set of all enumeration entries declared by <typeparamref name="T"/>, discovered once per concrete type.
        /// </summary>
        public static readonly FrozenSet<T> All = typeof(T)
            .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly)
            .Select(field => field.GetValue(null))
            .OfType<T>()
            .ToFrozenSet();

        // Why: the entries never change, so every lookup and projection is computed once per enumeration type rather than on every call.

        /// <summary>
        /// The enumeration entries grouped by name. A name normally has one entry; more than one is reported as an error by
        /// <see cref="FromName"/>.
        /// </summary>
        public static readonly FrozenDictionary<string, T[]> ByName = All
            .GroupBy(entry => entry.Name)
            .ToFrozenDictionary(group => group.Key, group => group.ToArray());

        /// <summary>
        /// The enumeration entries grouped by value. A value normally has one entry; more than one is reported as an error by
        /// <see cref="FromValue"/>.
        /// </summary>
        public static readonly FrozenDictionary<int, T[]> ByValue = All
            .GroupBy(entry => entry.Value)
            .ToFrozenDictionary(group => group.Key, group => group.ToArray());

        /// <summary>
        /// The value and name of every enumeration entry.
        /// </summary>
        public static readonly FrozenSet<(int Value, string Name)> Entries = All.Select(entry => entry.ToEntry()).ToFrozenSet();

        /// <summary>
        /// The name of every enumeration entry.
        /// </summary>
        public static readonly FrozenSet<string> Names = All.Select(entry => entry.Name).ToFrozenSet();

        /// <summary>
        /// The value of every enumeration entry.
        /// </summary>
        public static readonly FrozenSet<int> Values = All.Select(entry => entry.Value).ToFrozenSet();
    }
}
