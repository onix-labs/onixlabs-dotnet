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
using System.Security.Cryptography;

namespace OnixLabs.Security.Cryptography;

/// <summary>
/// Represents a cryptographic secret.
/// </summary>
// ReSharper disable MemberCanBePrivate.Global
public readonly partial struct Secret : ICryptoPrimitive<Secret>, ISpanParsable<Secret>
{
    // ReSharper disable once HeapView.ObjectAllocation.Evident
    private readonly ProtectedData protectedData = new();
    private readonly byte[] encryptedValue;
    private readonly Hash hash;

    /// <summary>
    /// Initializes a new instance of the <see cref="Secret"/> struct.
    /// </summary>
    /// <remarks>
    /// This constructor is intentionally marked <see langword="private"/> and is used exclusively to initialize the underlying <see cref="byte"/> array.
    /// Because <see cref="Secret"/> is designed to be immutable, external array references are not permitted.
    /// The overloaded constructors below ensure immutability by creating defensive copies of the provided arrays.
    /// </remarks>
    /// <param name="value">The value from which to initialize a new <see cref="Secret"/> instance.</param>
    private Secret(byte[] value)
    {
        encryptedValue = protectedData.Encrypt(value);
        hash = Hash.Compute(SHA256.Create(), value);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Secret"/> struct.
    /// </summary>
    /// <param name="value">The value from which to initialize a new <see cref="Secret"/> instance.</param>
    public Secret(ReadOnlySpan<byte> value) : this(value.ToArray())
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Secret"/> struct.
    /// </summary>
    /// <param name="value">The value from which to initialize a new <see cref="Secret"/> instance.</param>
    public Secret(ReadOnlyMemory<byte> value) : this(value.ToArray())
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Secret"/> struct.
    /// </summary>
    /// <param name="value">The value from which to initialize a new <see cref="Secret"/> instance.</param>
    public Secret(ReadOnlySequence<byte> value) : this(value.ToArray())
    {
    }
}
