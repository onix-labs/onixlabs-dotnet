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
using OnixLabs.Core;

namespace OnixLabs.Security;

/// <summary>
/// Represents a security token.
/// </summary>
/// <param name="value">The underlying value of the security token.</param>
public readonly partial struct SecurityToken(ReadOnlySpan<char> value) : IValueEquatable<SecurityToken>
{
    private readonly string value = value.ToString();

    /// <summary>
    /// Gets the length of the underlying security token value.
    /// </summary>
    public int Length => value.Length;
}
