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
using System.Text;

namespace OnixLabs.Core.Text;

/// <summary>
/// Represents a Base-64 value.
/// </summary>
/// <param name="value">The underlying <see cref="T:byte[]"/> value.</param>
public readonly partial struct Base64(ReadOnlySpan<byte> value) : IBaseRepresentation<Base64>
{
    private readonly byte[] value = value.ToArray();

    /// <summary>
    /// Initializes a new <see cref="Base64"/> value.
    /// </summary>
    /// <param name="value">The <see cref="ReadOnlySpan{T}"/> value from which to create a new <see cref="Base64"/> value.</param>
    /// <param name="encoding">The <see cref="Encoding"/> which will be used to obtain a <see cref="T:byte[]"/> from the specified <see cref="ReadOnlySpan{T}"/> value.</param>
    public Base64(ReadOnlySpan<char> value, Encoding? encoding = null) : this((encoding ?? Encoding.Default).GetBytes(value.ToArray()))
    {
    }
}
