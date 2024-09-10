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

namespace OnixLabs.Core.Reflection;

/// <summary>
/// Specifies flags that control how a type name is formatted.
/// </summary>
[Flags]
public enum TypeDeclarationFlags
{
    /// <summary>
    /// Specifies that no type name arguments are applied.
    /// <remarks>Only simple CLR type names will be used, and excludes generic type arguments and tuple syntax.</remarks>
    /// </summary>
    None = default,

    /// <summary>
    /// Specifies that namespace qualified CLR type names will be used, where applicable.
    /// <remarks>
    /// If the namespace qualified CLR type name is not available, then this will use the type's simple CLR type name instead.
    /// </remarks>
    /// </summary>
    UseNamespaceQualifiedTypeNames = 1 << 0,

    /// <summary>
    /// Specifies that type alias names will be used, where applicable.
    /// <remarks>
    /// This flag supersedes the <see cref="UseNamespaceQualifiedTypeNames"/> flag, therefore if a type alias name is not available, then the namespace
    /// qualified CLR type name will be used if <see cref="UseNamespaceQualifiedTypeNames"/> is set; otherwise, the type's simple CLR type name will be used.
    /// </remarks>
    /// </summary>
    UseAliasedTypeNames = 1 << 1,

    /// <summary>
    /// Specifies that <see cref="Nullable{T}"/> types will be formatted using nullable shorthand syntax.
    /// <remarks>
    /// This flag supersedes the <see cref="UseGenericTypeArguments"/> flag.
    /// </remarks>
    /// </summary>
    UseNullableShorthandTypeNames = 1 << 2,

    /// <summary>
    /// Specifies that if a type is generic, it should be formatted with its generic type arguments.
    /// </summary>
    UseGenericTypeArguments = 1 << 3,

    /// <summary>
    /// Specifies that types of <see cref="ValueTuple"/> will be formatted using tuple syntax.
    /// <remarks>This flag supersedes the <see cref="UseGenericTypeArguments"/> flag.</remarks>
    /// </summary>
    UseValueTupleSyntax = 1 << 4,

    /// <summary>
    /// Specifies that types of <see cref="ValueTuple"/> will be formatted using tuple names, where applicable.
    /// <remarks>This flag supersedes the <see cref="UseValueTupleSyntax"/> and <see cref="UseGenericTypeArguments"/> flags.</remarks>
    /// </summary>
    UseValueTupleNames = 1 << 5,

    /// <summary>
    /// Specifies that all type name flags are set.
    /// </summary>
    All = UseNamespaceQualifiedTypeNames
          | UseAliasedTypeNames
          | UseNullableShorthandTypeNames
          | UseGenericTypeArguments
          | UseValueTupleSyntax
          | UseValueTupleNames
}
