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

namespace OnixLabs.Core;

/// <summary>
/// Represents the base class for implementing enumeration classes.
/// </summary>
/// <param name="value">The value of the enumeration entry.</param>
/// <param name="name">The name of the enumeration entry.</param>
/// <typeparam name="T">The underlying enumeration type.</typeparam>
public abstract partial class Enumeration<T>(int value, string name) : IEquatable<T>, IComparable<T>, IComparable where T : Enumeration<T>
{
    /// <summary>
    /// Gets the name of the enumeration entry.
    /// </summary>
    public string Name { get; } = name;

    /// <summary>
    /// Gets the value of the enumeration entry.
    /// </summary>
    public int Value { get; } = value;
}
