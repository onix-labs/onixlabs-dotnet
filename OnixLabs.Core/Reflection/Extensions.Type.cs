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
using System.Text;
using OnixLabs.Core.Text;

namespace OnixLabs.Core.Reflection;

/// <summary>
/// Provides extension methods for types.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public static class TypeExtensions
{
    private const char GenericTypeIdentifierMarker = '`';
    private const char GenericTypeOpenBracket = '<';
    private const char GenericTypeCloseBracket = '>';
    private const string GenericTypeSeparator = ", ";
    private const string TypeNullExceptionMessage = "Type must not be null.";

    /// <summary>
    /// Gets the formatted type name from the current <see cref="Type"/> instance.
    /// </summary>
    /// <param name="type">The current <see cref="Type"/> instance from which to obtain the formatted type name.</param>
    /// <param name="flags">The type name flags that will be used to format the type name.</param>
    /// <returns>Returns the formatted type name from the current <see cref="Type"/> instance.</returns>
    public static string GetName(this Type type, TypeNameFlags flags = default)
    {
        RequireNotNull(type, TypeNullExceptionMessage, nameof(type));
        RequireIsDefined(flags, nameof(flags));

        // ReSharper disable once HeapView.ObjectAllocation.Evident
        StringBuilder builder = new();

        builder.Append(type.GetName((flags & TypeNameFlags.UseFullNames) is not 0));

        if (!type.IsGenericType || (flags & TypeNameFlags.UseGenericTypeArguments) is 0)
            return builder.ToString();

        builder.Append(GenericTypeOpenBracket);

        foreach (Type argument in type.GenericTypeArguments)
            builder.Append(argument.GetName(flags)).Append(GenericTypeSeparator);

        return builder.TrimEnd(GenericTypeSeparator).Append(GenericTypeCloseBracket).ToString();
    }

    /// <summary>
    /// Gets the simple type name from the current <see cref="Type"/> instance.
    /// </summary>
    /// <param name="type">The current <see cref="Type"/> instance from which to obtain the simple type name.</param>
    /// <param name="useFullName">Determines whether the current <see cref="Type"/>'s full name or short name should be returned.</param>
    /// <returns>Returns the simple type name from the current <see cref="Type"/> instance.</returns>
    private static string GetName(this Type type, bool useFullName) =>
        (useFullName ? type.FullName ?? type.Name : type.Name).SubstringBeforeFirst(GenericTypeIdentifierMarker);
}
