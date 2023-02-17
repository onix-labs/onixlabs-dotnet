// Copyright 2020-2022 ONIXLabs
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
using System.Collections.Generic;
using System.Linq;
using OnixLabs.Core.Linq;
using static OnixLabs.Core.Preconditions;

namespace OnixLabs.Core;

public abstract partial class Enumeration<T>
{
    /// <summary>
    /// Gets an enumeration entry for the specified name.
    /// </summary>
    /// <param name="name">The name of the enumeration entry.</param>
    /// <returns>Returns an enumeration entry for the specified name.</returns>
    /// <exception cref="ArgumentException">If a single enumeration entry for the specified name does not exist.</exception>
    public static T FromName(string name)
    {
        IEnumerable<T> results = GetAll().Where(entry => entry.Name == name).ToArray();

        Check(results.IsNotEmpty(), $"Enumeration entry for name '{name}' not found in {typeof(T).Name}.");
        Check(results.IsSingle(), $"Multiple enumeration entries for name '{name}' found in {typeof(T).Name}.");

        return results.Single();
    }

    /// <summary>
    /// Gets an enumeration entry for the specified value.
    /// </summary>
    /// <param name="value">The value of the enumeration entry.</param>
    /// <returns>Returns an enumeration entry for the specified value.</returns>
    /// <exception cref="ArgumentException">If a single enumeration entry for the specified value does not exist.</exception>
    public static T FromValue(int value)
    {
        IEnumerable<T> results = GetAll().Where(entry => entry.Value == value).ToArray();

        Check(results.IsNotEmpty(), $"Enumeration entry for value '{value}' not found in {typeof(T).Name}.");
        Check(results.IsSingle(), $"Multiple enumeration entries for value '{value}' found in {typeof(T).Name}.");

        return results.Single();
    }
}
