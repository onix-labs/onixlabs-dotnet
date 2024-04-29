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
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using OnixLabs.Core.Reflection;

namespace OnixLabs.Core;

/// <summary>
/// Provides extension methods for objects.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public static class ObjectExtensions
{
    /// <summary>
    /// Gets a record-like <see cref="System.String"/> representation of the current <see cref="System.Object"/> instance.
    /// </summary>
    /// <param name="value">The current <see cref="System.Object"/> instance.</param>
    /// <returns>Returns a record-like <see cref="System.String"/> representation of the current <see cref="System.Object"/> instance.</returns>
    public static string ToRecordString(this object value)
    {
        Type type = value.GetType();

        IEnumerable<string> properties = type
            .GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Select(property => $"{property.Name} = {property.GetValue(value)}");

        return $"{type.GetName()} {{ {string.Join(", ", properties)} }}";
    }
}
