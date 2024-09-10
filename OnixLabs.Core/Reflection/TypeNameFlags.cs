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
[Flags, Obsolete("This enumeration has been replaced with TypeDeclarationFlags and will be removed in version 10.0.0")]
public enum TypeNameFlags
{
    /// <summary>
    /// Specifies that no type name flags are applied.
    /// </summary>
    None = default,

    /// <summary>
    /// Specifies that type names should be formatted with their full name, where applicable.
    /// </summary>
    UseFullNames = 1,

    /// <summary>
    /// Specifies that if a type is generic, it should be formatted with its generic type arguments.
    /// </summary>
    UseGenericTypeArguments = 2,

    /// <summary>
    /// Specifies that type names should be formatted with their full name, where applicable, and that if a type is generic, it should be formatted with its generic type arguments.
    /// </summary>
    All = UseFullNames | UseGenericTypeArguments
}
