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

namespace OnixLabs.Security.Cryptography;

/// <summary>
/// Represents a cryptographic private key.
/// </summary>
public abstract partial class PrivateKey : ICryptoPrimitive<PrivateKey>
{
    // ReSharper disable once HeapView.ObjectAllocation.Evident
    private readonly ProtectedData protectedData = new();

    /// <summary>
    /// Initializes a new instance of the <see cref="PrivateKey"/> class.
    /// </summary>
    /// <remarks>
    /// This constructor is intentionally marked <see langword="private"/> and is used exclusively to initialize the underlying <see cref="byte"/> array.
    /// Because <see cref="PrivateKey"/> is designed to be immutable, external array references are not permitted.
    /// The overloaded constructors below ensure immutability by creating defensive copies of the provided arrays.
    /// </remarks>
    /// <param name="keyData">The underlying key data of the cryptographic private key.</param>
    private PrivateKey(byte[] keyData) => KeyData = protectedData.Encrypt(keyData);

    /// <summary>
    /// Initializes a new instance of the <see cref="PrivateKey"/> class.
    /// </summary>
    /// <param name="keyData">The underlying key data of the cryptographic private key.</param>
    protected PrivateKey(ReadOnlySpan<byte> keyData) : this(keyData.ToArray())
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="PrivateKey"/> class.
    /// </summary>
    /// <param name="keyData">The underlying key data of the cryptographic private key.</param>
    protected PrivateKey(ReadOnlyMemory<byte> keyData) : this(keyData.ToArray())
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="PrivateKey"/> class.
    /// </summary>
    /// <param name="keyData">The underlying key data of the cryptographic private key.</param>
    protected PrivateKey(ReadOnlySequence<byte> keyData) : this(keyData.ToArray())
    {
    }

    /// <summary>
    /// Gets the cryptographic private key data.
    /// </summary>
    protected byte[] KeyData => protectedData.Decrypt(field);
}
