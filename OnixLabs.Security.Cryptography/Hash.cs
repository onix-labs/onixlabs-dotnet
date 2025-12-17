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
using System.Linq;
using OnixLabs.Core;

namespace OnixLabs.Security.Cryptography;

/// <summary>
/// Represents a cryptographic hash.
/// </summary>
// ReSharper disable MemberCanBePrivate.Global
public readonly partial struct Hash : ICryptoPrimitive<Hash>, IValueComparable<Hash>, ISpanParsable<Hash>
{
    private readonly byte[] value;

    /// <summary>
    /// Initializes a new instance of the <see cref="Hash"/> struct.
    /// </summary>
    /// <remarks>
    /// This constructor is intentionally marked <see langword="private"/> and is used exclusively to initialize the underlying <see cref="byte"/> array.
    /// Because <see cref="Hash"/> is designed to be immutable, external array references are not permitted.
    /// The overloaded constructors below ensure immutability by creating defensive copies of the provided arrays.
    /// </remarks>
    /// <param name="value">The value from which to initialize a new <see cref="Hash"/> instance.</param>
    private Hash(byte[] value) => this.value = value;

    /// <summary>
    /// Initializes a new instance of the <see cref="Hash"/> struct.
    /// </summary>
    /// <param name="value">The value from which to initialize a new <see cref="Hash"/> instance.</param>
    public Hash(ReadOnlySpan<byte> value) : this(value.ToArray())
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Hash"/> struct.
    /// </summary>
    /// <param name="value">The value from which to initialize a new <see cref="Hash"/> instance.</param>
    public Hash(ReadOnlyMemory<byte> value) : this(value.ToArray())
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Hash"/> struct.
    /// </summary>
    /// <param name="value">The value from which to initialize a new <see cref="Hash"/> instance.</param>
    public Hash(ReadOnlySequence<byte> value) : this(value.ToArray())
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Hash"/> struct.
    /// </summary>
    /// <param name="value">The underlying value of the cryptographic hash.</param>
    /// <param name="length">The length of the cryptographic hash in bytes.</param>
    public Hash(byte value, int length) : this(Enumerable.Repeat(value, length).ToArray())
    {
    }

    /// <summary>
    /// Gets the length of the current <see cref="Hash"/> in bytes.
    /// </summary>
    public int Length => value?.Length ?? 0;
}
