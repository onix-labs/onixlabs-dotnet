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
using System.ComponentModel;

namespace OnixLabs.Core.Text;

/// <summary>
/// Provides extension methods for byte arrays and read-only spans.
/// </summary>
// ReSharper disable UnusedMethodReturnValue.Global
[EditorBrowsable(EditorBrowsableState.Never)]
public static class Extensions
{
    /// <summary>
    /// Converts the current <see cref="T:Byte[]"/> into a new <see cref="Base16"/> instance.
    /// </summary>
    /// <param name="value">The <see cref="T:Byte[]"/> value to convert.</param>
    /// <returns>Returns a new <see cref="Base16"/> instance.</returns>
    public static Base16 ToBase16(this byte[] value) => new(value);

    /// <summary>
    /// Converts the current <see cref="T:Byte[]"/> into a new <see cref="Base32"/> instance.
    /// </summary>
    /// <param name="value">The <see cref="T:Byte[]"/> value to convert.</param>
    /// <returns>Returns a new <see cref="Base32"/> instance.</returns>
    public static Base32 ToBase32(this byte[] value) => new(value);

    /// <summary>
    /// Converts the current <see cref="T:Byte[]"/> into a new <see cref="Base58"/> instance.
    /// </summary>
    /// <param name="value">The <see cref="T:Byte[]"/> value to convert.</param>
    /// <returns>Returns a new <see cref="Base58"/> instance.</returns>
    public static Base58 ToBase58(this byte[] value) => new(value);

    /// <summary>
    /// Converts the current <see cref="T:Byte[]"/> into a new <see cref="Base64"/> instance.
    /// </summary>
    /// <param name="value">The <see cref="T:Byte[]"/> value to convert.</param>
    /// <returns>Returns a new <see cref="Base64"/> instance.</returns>
    public static Base64 ToBase64(this byte[] value) => new(value);

    /// <summary>
    /// Converts the current <see cref="ReadOnlySpan{T}"/> into a new <see cref="Base16"/> instance.
    /// </summary>
    /// <param name="value">The <see cref="ReadOnlySpan{T}"/> value to convert.</param>
    /// <returns>Returns a new <see cref="Base16"/> instance.</returns>
    public static Base16 ToBase16(this ReadOnlySpan<byte> value) => new(value);

    /// <summary>
    /// Converts the current <see cref="ReadOnlySpan{T}"/> into a new <see cref="Base32"/> instance.
    /// </summary>
    /// <param name="value">The <see cref="ReadOnlySpan{T}"/> value to convert.</param>
    /// <returns>Returns a new <see cref="Base32"/> instance.</returns>
    public static Base32 ToBase32(this ReadOnlySpan<byte> value) => new(value);

    /// <summary>
    /// Converts the current <see cref="ReadOnlySpan{T}"/> into a new <see cref="Base58"/> instance.
    /// </summary>
    /// <param name="value">The <see cref="ReadOnlySpan{T}"/> value to convert.</param>
    /// <returns>Returns a new <see cref="Base58"/> instance.</returns>
    public static Base58 ToBase58(this ReadOnlySpan<byte> value) => new(value);

    /// <summary>
    /// Converts the current <see cref="ReadOnlySpan{T}"/> into a new <see cref="Base64"/> instance.
    /// </summary>
    /// <param name="value">The <see cref="ReadOnlySpan{T}"/> value to convert.</param>
    /// <returns>Returns a new <see cref="Base64"/> instance.</returns>
    public static Base64 ToBase64(this ReadOnlySpan<byte> value) => new(value);
}
