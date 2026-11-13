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

#if !NET11_0_OR_GREATER
namespace System.Runtime.CompilerServices;

/// <summary>
/// Marks a class as a closed hierarchy, whose direct subclasses may only be declared in the declaring assembly.
/// </summary>
/// <remarks>
/// The C# 15 compiler emits this attribute on every <c>closed</c> class and requires it to exist. .NET 11 ships it in the runtime;
/// this internal definition stands in for it on the older target frameworks so that the same source compiles everywhere.
/// </remarks>
[AttributeUsage(AttributeTargets.Class, Inherited = false)]
internal sealed class IsClosedTypeAttribute : Attribute;
#endif
