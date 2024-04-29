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
    /// Compares the current <see cref="System.IComparable{T}"/> instance with the specified <see cref="System.Object"/> instance.
    /// </summary>
    /// <param name="comparable">The left-hand <see cref="System.IComparable{T}"/> instance to compare.</param>
    /// <param name="obj">The right-hand <see cref="System.Object"/> instance to compare.</param>
    /// <typeparam name="T">The underlying type of the current <see cref="System.IComparable{T}"/>.</typeparam>
    /// <returns>Returns a signed integer that indicates the relative order of the objects being compared.</returns>
    /// <exception cref="System.ArgumentException">If the specified object is not <see langword="null"/>, or of the specified type.</exception>
    public static int CompareToObject<T>(this IComparable<T> comparable, object? obj)
    {
        if (obj is null) return 1;
        if (obj is T other) return comparable.CompareTo(other);
        throw new ArgumentException($"Object must be of type {typeof(T).Name}", nameof(obj));
    }

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
