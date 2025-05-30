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
using OnixLabs.Core;

namespace OnixLabs.Security.Cryptography;

/// <summary>
/// Represents a cryptographically secure random number, otherwise known as a salt value.
/// </summary>
/// <param name="value">The underlying value of the salt.</param>
public readonly partial struct Salt(ReadOnlySpan<byte> value) : ICryptoPrimitive<Salt>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Salt"/> struct.
    /// </summary>
    /// <param name="value">The <see cref="ReadOnlySequence{T}"/> with which to initialize the <see cref="Salt"/> instance.</param>
    // ReSharper disable once MemberCanBePrivate.Global
    public Salt(ReadOnlySequence<byte> value) : this(ReadOnlySpan<byte>.Empty) => value.CopyTo(out this.value);

    private readonly byte[] value = value.ToArray();

    /// <summary>
    /// Gets the length of the current <see cref="Salt"/> in bytes.
    /// </summary>
    public int Length => value?.Length ?? 0;
}
