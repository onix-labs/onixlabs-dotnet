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

public sealed partial class EcdsaPublicKey
{
    /// <summary>
    /// Create a new <see cref="EcdsaPublicKey"/> instance from the specified <see cref="T:byte[]"/> value.
    /// </summary>
    /// <param name="value">The value from which to create a new <see cref="EcdsaPublicKey"/> instance.</param>
    /// <returns>Returns a new <see cref="EcdsaPublicKey"/> instance from the specified <see cref="T:byte[]"/> value.</returns>
    public static implicit operator EcdsaPublicKey(byte[] value) => new(value);

    /// <summary>
    /// Create a new <see cref="EcdsaPublicKey"/> instance from the specified <see cref="ReadOnlySpan{T}"/> value.
    /// </summary>
    /// <param name="value">The value from which to create a new <see cref="EcdsaPublicKey"/> instance.</param>
    /// <returns>Returns a new <see cref="EcdsaPublicKey"/> instance from the specified <see cref="ReadOnlySpan{T}"/> value.</returns>
    public static implicit operator EcdsaPublicKey(ReadOnlySpan<byte> value) => new(value);

    /// <summary>
    /// Create a new <see cref="EcdsaPublicKey"/> instance from the specified <see cref="ReadOnlySequence{T}"/> value.
    /// </summary>
    /// <param name="value">The value from which to create a new <see cref="EcdsaPublicKey"/> instance.</param>
    /// <returns>Returns a new <see cref="EcdsaPublicKey"/> instance from the specified <see cref="ReadOnlySequence{T}"/> value.</returns>
    public static implicit operator EcdsaPublicKey(ReadOnlySequence<byte> value) => new(value);
}
