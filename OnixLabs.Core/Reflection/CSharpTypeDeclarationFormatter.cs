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

    public static string GetTypeDeclaration(Type type, TypeDeclarationFlags flags)
    {
        RequireNotNull(type, TypeNullExceptionMessage, nameof(type));

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

    private static Type ConditionallyUnwrapNullableType(Type type, TypeDeclarationFlags flags)
    {
        if ((flags & TypeDeclarationFlags.UseNullableShorthandTypeNames) is not 0)
            return Nullable.GetUnderlyingType(type) ?? type;

        return type;
    }

    private static bool CanFormatGenericType(Type type, TypeDeclarationFlags flags)
    {
        bool useGenericTypeArguments = (flags & TypeDeclarationFlags.UseGenericTypeArguments) is not 0;
        return useGenericTypeArguments && type.IsGenericType;
    }

    private static bool CanFormatValueTupleType(Type type, TypeDeclarationFlags flags)
    {
        bool useTupleSyntax = (flags & TypeDeclarationFlags.UseValueTupleSyntax) is not 0;
        bool useTupleNames = (flags & TypeDeclarationFlags.UseValueTupleNames) is not 0;

        return (useTupleSyntax || useTupleNames)
               && type.Name.StartsWith(nameof(ValueTuple))
               && type.GenericTypeArguments.Length > 1;
    }

    private static void FormatValueTupleType(Type type, StringBuilder builder, TypeDeclarationFlags flags)
    {
        bool useTupleNames = (flags & TypeDeclarationFlags.UseValueTupleNames) is not 0;

        builder.Append(ValueTupleOpenParenthesis);
        FormatTypeArguments(type.GetGenericArguments(), builder, flags, useTupleNames);
        builder.Append(ValueTupleCloseParenthesis);
    }

    private static void FormatGenericType(Type type, StringBuilder builder, TypeDeclarationFlags flags)
    {
        FormatTypeName(type, builder, flags);
        builder.Append(GenericTypeOpenBracket);
        FormatTypeArguments(type.GetGenericArguments(), builder, flags, useTupleNames: false);
        builder.Append(GenericTypeCloseBracket);
    }

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

    private static void FormatNullableShorthandNotation(Type type, StringBuilder builder, TypeDeclarationFlags flags)
    {
        if ((flags & TypeDeclarationFlags.UseNullableShorthandTypeNames) is not 0 && Nullable.GetUnderlyingType(type) is not null)
            builder.Append(NullableTypeIdentifier);
    }
}
