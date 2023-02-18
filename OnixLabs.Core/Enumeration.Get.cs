// Copyright 2020-2023 ONIXLabs
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

using System.Collections.Immutable;
using System.Linq;
using System.Reflection;
using OnixLabs.Core.Linq;

namespace OnixLabs.Core;

public abstract partial class Enumeration<T>
{
    /// <summary>
    /// Gets all of the enumeration entries for the current type.
    /// </summary>
    /// <returns>Returns all of the enumeration entries for the current type.</returns>
    public static ImmutableHashSet<T> GetAll()
    {
        return typeof(T)
            .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly)
            .Select(field => field.GetValue(null))
            .WhereIs<T>()
            .ToImmutableHashSet();
    }

    /// <summary>
    /// Gets all of the enumeration entries for the current type.
    /// </summary>
    /// <returns>Returns all of the enumeration entries for the current type.</returns>
    public static ImmutableHashSet<(int Value, string Name)> GetEntries()
    {
        return GetAll().Select(entry => entry.ToEntry()).ToImmutableHashSet();
    }

    /// <summary>
    /// Gets all of the enumeration names for the current type.
    /// </summary>
    /// <returns>Returns all of the enumeration names for the current type.</returns>
    public static ImmutableHashSet<string> GetNames()
    {
        return GetAll().Select(entry => entry.Name).ToImmutableHashSet();
    }

    /// <summary>
    /// Gets all of the enumeration values for the current type.
    /// </summary>
    /// <returns>Returns all of the enumeration values for the current type.</returns>
    public static ImmutableHashSet<int> GetValues()
    {
        return GetAll().Select(entry => entry.Value).ToImmutableHashSet();
    }
}
