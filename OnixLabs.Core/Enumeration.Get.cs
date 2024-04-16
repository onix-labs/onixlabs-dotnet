// Copyright © 2020 ONIXLabs
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
using OnixLabs.Core.Linq;

namespace OnixLabs.Core;

// ReSharper disable ReturnTypeCanBeEnumerable.Global
public abstract partial class Enumeration<T>
{
    /// <summary>
    /// Gets all of the enumeration entries for the current type.
    /// </summary>
    /// <returns>Returns all of the enumeration entries for the current type.</returns>
    public static IReadOnlySet<T> GetAll() => typeof(T)
        .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly)
        .Select(field => field.GetValue(null))
        .OfType<T>()
        .ToFrozenSet();

    /// <summary>
    /// Gets all of the enumeration entries for the current type.
    /// </summary>
    /// <returns>Returns all of the enumeration entries for the current type.</returns>
    public static IReadOnlySet<(int Value, string Name)> GetEntries() => GetAll().Select(entry => entry.ToEntry()).ToFrozenSet();

    /// <summary>
    /// Gets all of the enumeration names for the current type.
    /// </summary>
    /// <returns>Returns all of the enumeration names for the current type.</returns>
    public static IReadOnlySet<string> GetNames() => GetAll().Select(entry => entry.Name).ToFrozenSet();

    /// <summary>
    /// Gets all of the enumeration values for the current type.
    /// </summary>
    /// <returns>Returns all of the enumeration values for the current type.</returns>
    public static IReadOnlySet<int> GetValues() => GetAll().Select(entry => entry.Value).ToFrozenSet();
}
