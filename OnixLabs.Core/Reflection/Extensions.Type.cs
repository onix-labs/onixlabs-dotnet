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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace OnixLabs.Core.Reflection;

/// <summary>
/// Provides extension methods for types.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public static class TypeExtensions
{
    /// <summary>
    /// The identifier marker than indicates a generic type.
    /// </summary>
    private const char GenericTypeIdentifierMarker = '`';

    /// <summary>
    /// Gets the simple type name from the specified type, excluding any generic type parameters.
    /// </summary>
    /// <param name="type">The <see cref="Type"/> from which to obtain a simple type name.</param>
    /// <returns>Returns the simple type name from the specified type, excluding any generic type parameters.</returns>
    public static string GetSimpleName(this Type type)
    {
        return type.Name.SubstringBeforeFirst(GenericTypeIdentifierMarker);
    }

    /// <summary>
    /// Gets the formatted type name from the specified type, including any generic type parameters.
    /// </summary>
    /// <param name="type">The <see cref="Type"/> from which to obtain a formatted type name.</param>
    /// <returns>Returns the formatted type name from the specified type, including any generic type parameters.</returns>
    public static string GetFormattedName(this Type type)
    {
        if (!type.Name.Contains(GenericTypeIdentifierMarker))
        {
            return type.Name;
        }

        IEnumerable<string> genericTypeArguments = type.GenericTypeArguments.Select(GetFormattedName);
        string typeName = type.Name.SubstringBeforeFirst(GenericTypeIdentifierMarker);
        string generics = string.Join(", ", genericTypeArguments).Wrap("<", ">");

        return typeName + generics;
    }
}
