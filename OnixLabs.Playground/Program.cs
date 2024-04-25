// Copyright © 2020 ONIXLabs
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
using System.Linq;
using System.Reflection;

namespace OnixLabs.Playground;

internal class Program
{
    private static void Main()
    {
        // typeof(RSA)
        //     .GetMethods(BindingFlags.Public | BindingFlags.Instance)
        //     .Where(method => method.Name.StartsWith("Verify"))
        //     .Select(MethodSignatureFormatter.PrettyPrintMethodSignature)
        //     .ForEach(Console.WriteLine);
    }
}

public static class MethodSignatureFormatter
{
    public static string PrettyPrintMethodSignature(MethodInfo methodInfo)
    {
        var returnType = SimplifyTypeName(methodInfo.ReturnType);
        var methodName = methodInfo.Name;
        var parameters = methodInfo.GetParameters()
            .Select(p => $"{SimplifyTypeName(p.ParameterType)} {p.Name}")
            .ToArray();

        return $"{returnType} {methodName}({string.Join(", ", parameters)})";
    }

    private static string SimplifyTypeName(Type type)
    {
        // Check for generic types
        if (type.IsGenericType)
        {
            var genericTypeName = type.GetGenericTypeDefinition().Name;
            // Remove the generic arity from the name
            genericTypeName = genericTypeName.Substring(0, genericTypeName.IndexOf('`'));
            var genericArgs = type.GetGenericArguments().Select(SimplifyTypeName).ToArray();
            return $"{genericTypeName}<{string.Join(", ", genericArgs)}>";
        }

        // Check for array types
        if (type.IsArray)
        {
            return $"{SimplifyTypeName(type.GetElementType())}[]";
        }

        // Use predefined type aliases
        switch (type.FullName)
        {
            case "System.Boolean": return "bool";
            case "System.Byte": return "byte";
            case "System.SByte": return "sbyte";
            case "System.Char": return "char";
            case "System.Decimal": return "decimal";
            case "System.Double": return "double";
            case "System.Single": return "float";
            case "System.Int32": return "int";
            case "System.UInt32": return "uint";
            case "System.Int64": return "long";
            case "System.UInt64": return "ulong";
            case "System.Object": return "object";
            case "System.Int16": return "short";
            case "System.UInt16": return "ushort";
            case "System.String": return "string";
            default:
                // Only include the last part of the namespace (simplified name for non-primitive types)
                return type.Name.Split('.').Last();
        }
    }
}
