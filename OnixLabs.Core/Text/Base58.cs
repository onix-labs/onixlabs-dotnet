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

namespace OnixLabs.Core.Text;

/// <summary>
/// Represents a Base-58 value.
/// </summary>
/// <param name="value">The underlying <see cref="T:Byte[]"/> value.</param>
public readonly partial struct Base58(ReadOnlySpan<byte> value) : IBaseValue<Base58>
{
    private readonly byte[] value = value.ToArray();

    /// <summary>
    /// Initializes a new default <see cref="Base58"/> value.
    /// </summary>
    public Base58() : this([])
    {
    }
}
