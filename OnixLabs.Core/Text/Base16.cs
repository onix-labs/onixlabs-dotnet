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
using System.Buffers;
using System.Text;

namespace OnixLabs.Core.Text;

/// <summary>
/// Represents a Base-16 value.
/// </summary>
/// <param name="value">The <see cref="ReadOnlySpan{T}"/> with which to initialize the <see cref="Base16"/> instance.</param>
public readonly partial struct Base16(ReadOnlySpan<byte> value) : IBaseValue<Base16>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Base16"/> struct.
    /// </summary>
    /// <param name="value">The <see cref="ReadOnlySequence{T}"/> with which to initialize the <see cref="Base16"/> instance.</param>
    public Base16(ReadOnlySequence<byte> value) : this(ReadOnlySpan<byte>.Empty) => value.CopyTo(out this.value);

    /// <summary>
    /// Initializes a new instance of the <see cref="Base16"/> struct.
    /// </summary>
    /// <param name="value">The <see cref="string"/> with which to initialize the <see cref="Base16"/> instance.</param>
    /// <param name="encoding">The <see cref="Encoding"/> which will be used to obtain the underlying value.</param>
    public Base16(string value, Encoding? encoding = null) : this(encoding.GetOrDefault().GetBytes(value))
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Base16"/> struct.
    /// </summary>
    /// <param name="value">The <see cref="T:char[]"/> with which to initialize the <see cref="Base16"/> instance.</param>
    /// <param name="encoding">The <see cref="Encoding"/> which will be used to obtain the underlying value.</param>
    public Base16(char[] value, Encoding? encoding = null) : this(encoding.GetOrDefault().GetBytes(value))
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Base16"/> struct.
    /// </summary>
    /// <param name="value">The <see cref="ReadOnlySequence{T}"/> with which to initialize the <see cref="Base16"/> instance.</param>
    /// <param name="encoding">The <see cref="Encoding"/> which will be used to obtain the underlying value.</param>
    public Base16(ReadOnlySequence<char> value, Encoding? encoding = null) : this(encoding.GetOrDefault().GetBytes(value))
    {
    }

    private readonly byte[] value = value.ToArray();
}
