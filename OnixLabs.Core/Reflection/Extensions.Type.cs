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
using System.ComponentModel;

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
    /// Gets the simple type name from the current <see cref="System.Type"/> instance.
    /// </summary>
    /// <param name="type">The current <see cref="System.Type"/> instance from which to obtain the simple name.</param>
    /// <returns>Returns the simple type name from the current <see cref="System.Type"/> instance.</returns>
    public static string GetName(this Type type)
    {
        return type.Name.SubstringBeforeFirst(GenericTypeIdentifierMarker);
    }
}
