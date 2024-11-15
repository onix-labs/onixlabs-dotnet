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
    /// Gets the type declaration for the current <see cref="Type"/> instance.
    /// <remarks>
    /// Depending on the specified <see cref="TypeDeclarationFlags"/>, this method is capable or returning type declarations including
    /// simple type names, namespace qualified types names, aliased types names, nullable shorthand notation, generic arguments, and value tuples.
    /// </remarks>
    /// </summary>
    /// <param name="type">The current <see cref="Type"/> instance from which to obtain the type declaration.</param>
    /// <param name="flags">The flags that specify how the type declaration should be formatted.</param>
    /// <returns>Returns the type declaration for the current <see cref="Type"/> instance.</returns>
    public static string GetCSharpTypeDeclaration(this Type type, TypeDeclarationFlags flags = default) =>
        CSharpTypeDeclarationFormatter.GetTypeDeclaration(type, flags);
}
