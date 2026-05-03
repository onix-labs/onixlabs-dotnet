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
/// Represents a Base-58 value.
/// </summary>
// ReSharper disable MemberCanBePrivate.Global
public readonly partial struct Base58 : IBaseValue<Base58>
{
    private readonly byte[] value;

    /// <summary>
    /// Initializes a new instance of the <see cref="Base58"/> struct.
    /// </summary>
    /// <remarks>
    /// This constructor is intentionally marked <see langword="private"/> and is used exclusively to initialize the underlying <see cref="byte"/> array.
    /// Because <see cref="Base58"/> is designed to be immutable, external array references are not permitted.
    /// The overloaded constructors below ensure immutability by creating defensive copies of the provided arrays.
    /// </remarks>
    /// <param name="value">The value from which to initialize the new <see cref="Base58"/> instance.</param>
    private Base58(byte[] value) => this.value = value;

    /// <summary>
    /// Initializes a new instance of the <see cref="Base58"/> struct.
    /// </summary>
    /// <param name="value">The value from which to initialize the new <see cref="Base58"/> instance.</param>
    public Base58(ReadOnlySpan<byte> value) : this(value.ToArray())
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Base58"/> struct.
    /// </summary>
    /// <param name="value">The value from which to initialize the new <see cref="Base58"/> instance.</param>
    public Base58(ReadOnlyMemory<byte> value) : this(value.ToArray())
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Base58"/> struct.
    /// </summary>
    /// <param name="value">The value from which to initialize the new <see cref="Base58"/> instance.</param>
    public Base58(ReadOnlySequence<byte> value) : this(value.ToArray())
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Base58"/> struct.
    /// </summary>
    /// <param name="value">The value from which to initialize the new <see cref="Base58"/> instance.</param>
    /// <param name="encoding">The <see cref="Encoding"/> that will be used to transform the specified value, or <see langword="null"/> to use the default <see cref="Encoding"/>.</param>
    public Base58(ReadOnlySpan<char> value, Encoding? encoding = null) : this(encoding.GetBytes(value))
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Base58"/> struct.
    /// </summary>
    /// <param name="value">The value from which to initialize the new <see cref="Base58"/> instance.</param>
    /// <param name="encoding">The <see cref="Encoding"/> that will be used to transform the specified value, or <see langword="null"/> to use the default <see cref="Encoding"/>.</param>
    public Base58(ReadOnlyMemory<char> value, Encoding? encoding = null) : this(encoding.GetBytes(value.Span))
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Base58"/> struct.
    /// </summary>
    /// <param name="value">The value from which to initialize the new <see cref="Base58"/> instance.</param>
    /// <param name="encoding">The <see cref="Encoding"/> that will be used to transform the specified value, or <see langword="null"/> to use the default <see cref="Encoding"/>.</param>
    public Base58(ReadOnlySequence<char> value, Encoding? encoding = null) : this(encoding.GetOrDefault().GetBytes(value))
    {
    }
}
