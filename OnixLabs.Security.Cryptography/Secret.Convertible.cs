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
using OnixLabs.Core.Text;

namespace OnixLabs.Security.Cryptography;

public readonly partial struct Secret
{
    /// <inheritdoc/>
    public ReadOnlyMemory<byte> AsReadOnlyMemory() => protectedData.Decrypt(encryptedValue);

    /// <inheritdoc/>
    public ReadOnlySpan<byte> AsReadOnlySpan() => protectedData.Decrypt(encryptedValue);

    /// <summary>
    /// Creates a new <see cref="Secret"/> instance from the specified <see cref="ReadOnlySpan{T}"/> value.
    /// </summary>
    /// <param name="value">The value from which to create a new <see cref="Secret"/> instance.</param>
    /// <returns>Returns a new <see cref="Secret"/> instance from the specified <see cref="ReadOnlySpan{T}"/> value.</returns>
    public static implicit operator Secret(ReadOnlySpan<byte> value) => new(value);

    /// <summary>
    /// Creates a new <see cref="Secret"/> instance from the specified <see cref="ReadOnlyMemory{T}"/> value.
    /// </summary>
    /// <param name="value">The value from which to create a new <see cref="Secret"/> instance.</param>
    /// <returns>Returns a new <see cref="Secret"/> instance from the specified <see cref="ReadOnlyMemory{T}"/> value.</returns>
    public static implicit operator Secret(ReadOnlyMemory<byte> value) => new(value);

    /// <summary>
    /// Creates a new <see cref="Secret"/> instance from the specified <see cref="ReadOnlySequence{T}"/> value.
    /// </summary>
    /// <param name="value">The value from which to create a new <see cref="Secret"/> instance.</param>
    /// <returns>Returns a new <see cref="Secret"/> instance from the specified <see cref="ReadOnlySequence{T}"/> value.</returns>
    public static implicit operator Secret(ReadOnlySequence<byte> value) => new(value);

    /// <summary>
    /// Creates a new <see cref="Secret"/> instance from the specified <see cref="ReadOnlySpan{T}"/> value.
    /// <remarks>The <see cref="ReadOnlySpan{T}"/> value will be encoded using the <see cref="Encoding.UTF8"/> encoding.</remarks>
    /// </summary>
    /// <param name="value">The value from which to create a new <see cref="Secret"/> instance.</param>
    /// <returns>Returns a new <see cref="Secret"/> instance from the specified <see cref="ReadOnlySpan{T}"/> value.</returns>
    public static implicit operator Secret(ReadOnlySpan<char> value) => new(Encoding.UTF8.GetBytes(value));

    /// <summary>
    /// Creates a new <see cref="Secret"/> instance from the specified <see cref="ReadOnlyMemory{T}"/> value.
    /// <remarks>The <see cref="ReadOnlyMemory{T}"/> value will be encoded using the <see cref="Encoding.UTF8"/> encoding.</remarks>
    /// </summary>
    /// <param name="value">The value from which to create a new <see cref="Secret"/> instance.</param>
    /// <returns>Returns a new <see cref="Secret"/> instance from the specified <see cref="ReadOnlyMemory{T}"/> value.</returns>
    public static implicit operator Secret(ReadOnlyMemory<char> value) => new(Encoding.UTF8.GetBytes(value.Span));

    /// <summary>
    /// Creates a new <see cref="Secret"/> instance from the specified <see cref="ReadOnlySequence{T}"/> value.
    /// <remarks>The <see cref="ReadOnlySequence{T}"/> value will be encoded using the <see cref="Encoding.UTF8"/> encoding.</remarks>
    /// </summary>
    /// <param name="value">The value from which to create a new <see cref="Secret"/> instance.</param>
    /// <returns>Returns a new <see cref="Secret"/> instance from the specified <see cref="ReadOnlySequence{T}"/> value.</returns>
    public static implicit operator Secret(ReadOnlySequence<char> value) => new(Encoding.UTF8.GetBytes(value));
}
