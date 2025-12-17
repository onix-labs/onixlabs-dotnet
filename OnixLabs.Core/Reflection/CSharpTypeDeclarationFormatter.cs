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
using System.Text;
using OnixLabs.Core.Text;

namespace OnixLabs.Core.Reflection;

/// <summary>
/// Represents a formatter for C# type declarations.
/// <remarks>
/// There are some limitations as to what this formatter is capable of producing; for example, nullability state information
/// for nullable reference types, and <see cref="ValueTuple"/> custom names are not available within a <see cref="Type"/>
/// instance, and therefore cannot be produced by this type declaration formatter.
/// </remarks>
/// </summary>
internal static class CSharpTypeDeclarationFormatter
{
    private const char NullableTypeIdentifier = '?';
    private const char GenericTypeIdentifierMarker = '`';
    private const char GenericTypeOpenBracket = '<';
    private const char GenericTypeCloseBracket = '>';
    private const char ValueTupleOpenParenthesis = '(';
    private const char ValueTupleCloseParenthesis = ')';
    private const string TypeSeparator = ", ";
    private const string ValueTupleItemName = " Item";
    private const string TypeNullExceptionMessage = "Type must not be null.";

    private static readonly Dictionary<Type, string> TypeAliases = new()
    {
        [typeof(byte)] = "byte",
        [typeof(sbyte)] = "sbyte",
        [typeof(short)] = "short",
        [typeof(ushort)] = "ushort",
        [typeof(int)] = "int",
        [typeof(uint)] = "uint",
        [typeof(long)] = "long",
        [typeof(ulong)] = "ulong",
        [typeof(nint)] = "nint",
        [typeof(nuint)] = "nuint",
        [typeof(float)] = "float",
        [typeof(double)] = "double",
        [typeof(decimal)] = "decimal",
        [typeof(object)] = "object",
        [typeof(bool)] = "bool",
        [typeof(char)] = "char",
        [typeof(string)] = "string",
        [typeof(void)] = "void"
    };

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
    public static string GetTypeDeclaration(Type type, TypeDeclarationFlags flags)
    {
        RequireNotNull(type, TypeNullExceptionMessage);

        Type unwrappedType = ConditionallyUnwrapNullableType(type, flags);
        StringBuilder builder = new();

        if (CanFormatValueTupleType(unwrappedType, flags))
            FormatValueTupleType(unwrappedType, builder, flags);

        else if (CanFormatGenericType(unwrappedType, flags))
            FormatGenericType(unwrappedType, builder, flags);

        else FormatTypeName(unwrappedType, builder, flags);

        FormatNullableShorthandNotation(type, builder, flags);

        return builder.ToString();
    }

    /// <summary>
    /// Conditionally unwraps a <see cref="Nullable{T}"/> type, if the <see cref="TypeDeclarationFlags.UseNullableShorthandTypeNames"/> flag is set.
    /// </summary>
    /// <param name="type">The potential <see cref="Nullable{T}"/> type to unwrap.</param>
    /// <param name="flags">The flags that specify how the type declaration should be formatted.</param>
    /// <returns>
    /// Returns the underlying type, if the specified type is <see cref="Nullable{T}"/>, and the
    /// <see cref="TypeDeclarationFlags.UseNullableShorthandTypeNames"/> flag is set; otherwise, returns the current <see cref="Type"/>.
    /// </returns>
    private static Type ConditionallyUnwrapNullableType(Type type, TypeDeclarationFlags flags)
    {
        if ((flags & TypeDeclarationFlags.UseNullableShorthandTypeNames) is not 0)
            return Nullable.GetUnderlyingType(type) ?? type;

        return type;
    }

    /// <summary>
    /// Determines whether the specified type is a generic type, and whether the <see cref="TypeDeclarationFlags.UseGenericTypeArguments"/> flag is set.
    /// </summary>
    /// <param name="type">The type to check is potentially generic.</param>
    /// <param name="flags">The flags that specify how the type declaration should be formatted.</param>
    /// <returns>Returns true if the specified type is a generic type, and the <see cref="TypeDeclarationFlags.UseGenericTypeArguments"/> flag is set; otherwise, false.</returns>
    private static bool CanFormatGenericType(Type type, TypeDeclarationFlags flags)
    {
        bool useGenericTypeArguments = (flags & TypeDeclarationFlags.UseGenericTypeArguments) is not 0;
        return useGenericTypeArguments && type.IsGenericType;
    }

    /// <summary>
    /// Determines whether the specified type is a multi-argument value-tuple type, and whether the
    /// <see cref="TypeDeclarationFlags.UseValueTupleSyntax"/> or <see cref="TypeDeclarationFlags.UseValueTupleNames"/> flag is set.
    /// </summary>
    /// <param name="type">The type to check is potentially a multi-argument value-tuple type.</param>
    /// <param name="flags">The flags that specify how the type declaration should be formatted.</param>
    /// <returns>
    /// Returns true if the specified type is a multi-argument value-tuple type, and whether the
    /// <see cref="TypeDeclarationFlags.UseValueTupleSyntax"/> or <see cref="TypeDeclarationFlags.UseValueTupleNames"/> flag is set; otherwise, false.
    /// </returns>
    private static bool CanFormatValueTupleType(Type type, TypeDeclarationFlags flags)
    {
        bool useTupleSyntax = (flags & TypeDeclarationFlags.UseValueTupleSyntax) is not 0;
        bool useTupleNames = (flags & TypeDeclarationFlags.UseValueTupleNames) is not 0;

        return (useTupleSyntax || useTupleNames)
               && type.Name.StartsWith(nameof(ValueTuple))
               && type.GenericTypeArguments.Length > 1;
    }

    /// <summary>
    /// Formats the specified type as a value-tuple.
    /// </summary>
    /// <param name="type">The type to format.</param>
    /// <param name="builder">The <see cref="StringBuilder"/> to which the type information will be appended.</param>
    /// <param name="flags">The flags that specify how the type declaration should be formatted.</param>
    private static void FormatValueTupleType(Type type, StringBuilder builder, TypeDeclarationFlags flags)
    {
        bool useTupleNames = (flags & TypeDeclarationFlags.UseValueTupleNames) is not 0;

        builder.Append(ValueTupleOpenParenthesis);
        FormatTypeArguments(type.GetGenericArguments(), builder, flags, useTupleNames);
        builder.Append(ValueTupleCloseParenthesis);
    }

    /// <summary>
    /// Formats the specified type as a generic type.
    /// </summary>
    /// <param name="type">The type to format.</param>
    /// <param name="builder">The <see cref="StringBuilder"/> to which the type information will be appended.</param>
    /// <param name="flags">The flags that specify how the type declaration should be formatted.</param>
    private static void FormatGenericType(Type type, StringBuilder builder, TypeDeclarationFlags flags)
    {
        FormatTypeName(type, builder, flags);
        builder.Append(GenericTypeOpenBracket);
        FormatTypeArguments(type.GetGenericArguments(), builder, flags, useTupleNames: false);
        builder.Append(GenericTypeCloseBracket);
    }

    /// <summary>
    /// Formats the specified type as a type alias, namespace qualified name, or simple name.
    /// </summary>
    /// <param name="type">The type to format.</param>
    /// <param name="builder">The <see cref="StringBuilder"/> to which the type information will be appended.</param>
    /// <param name="flags">The flags that specify how the type declaration should be formatted.</param>
    private static void FormatTypeName(Type type, StringBuilder builder, TypeDeclarationFlags flags)
    {
        bool useAliasedTypeNames = (flags & TypeDeclarationFlags.UseAliasedTypeNames) is not 0;
        bool useNamespaceQualifiedTypeNames = (flags & TypeDeclarationFlags.UseNamespaceQualifiedTypeNames) is not 0;

        if (useAliasedTypeNames && TypeAliases.TryGetValue(type, out string? alias))
        {
            builder.Append(alias);
            return;
        }

        if (useNamespaceQualifiedTypeNames)
        {
            builder.Append((type.FullName ?? type.Name).SubstringBeforeFirst(GenericTypeIdentifierMarker));
            return;
        }

        builder.Append(type.Name.SubstringBeforeFirst(GenericTypeIdentifierMarker));
    }

    /// <summary>
    /// Formats the specified type's generic argument types.
    /// </summary>
    /// <param name="arguments">The argument types to format.</param>
    /// <param name="builder">The <see cref="StringBuilder"/> to which the type information will be appended.</param>
    /// <param name="flags">The flags that specify how the type declaration should be formatted.</param>
    /// <param name="useTupleNames">Specifies whether tuple names should be formatted.</param>
    private static void FormatTypeArguments(Type[] arguments, StringBuilder builder, TypeDeclarationFlags flags, bool useTupleNames)
    {
        for (int index = 0; index < arguments.Length; index++)
        {
            builder.Append(GetTypeDeclaration(arguments[index], flags));

            if (useTupleNames)
                builder
                    .Append(ValueTupleItemName)
                    .Append(index + 1);

            builder.Append(TypeSeparator);
        }

        builder.TrimEnd(TypeSeparator);
    }

    /// <summary>
    /// Formats the type using nullable shorthand notation.
    /// </summary>
    /// <param name="type">The type to format.</param>
    /// <param name="builder">The <see cref="StringBuilder"/> to which the type information will be appended.</param>
    /// <param name="flags">The flags that specify how the type declaration should be formatted.</param>
    private static void FormatNullableShorthandNotation(Type type, StringBuilder builder, TypeDeclarationFlags flags)
    {
        if ((flags & TypeDeclarationFlags.UseNullableShorthandTypeNames) is not 0 && Nullable.GetUnderlyingType(type) is not null)
            builder.Append(NullableTypeIdentifier);
    }
}
