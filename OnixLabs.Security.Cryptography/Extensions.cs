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

namespace OnixLabs.Security.Cryptography;

/// <summary>
/// Provides extension methods for byte arrays and read-only spans.
/// </summary>
// ReSharper disable UnusedMethodReturnValue.Global
[EditorBrowsable(EditorBrowsableState.Never)]
public static class Extensions
{
    /// <summary>
    /// Converts the current <see cref="T:Byte[]"/> into a new <see cref="Hash"/> instance.
    /// </summary>
    /// <param name="value">The <see cref="T:Byte[]"/> value to convert.</param>
    /// <returns>Returns a new <see cref="Hash"/> instance.</returns>
    public static Hash ToHash(this byte[] value) => value;

    /// <summary>
    /// Converts the current <see cref="ReadOnlySpan{Byte}"/> into a new <see cref="Hash"/> instance.
    /// </summary>
    /// <param name="value">The <see cref="ReadOnlySpan{Byte}"/> value to convert.</param>
    /// <returns>Returns a new <see cref="Hash"/> instance.</returns>
    public static Hash ToHash(this ReadOnlySpan<byte> value) => value;

    /// <summary>
    /// Converts the current <see cref="T:Byte[]"/> into a new <see cref="Secret"/> instance.
    /// </summary>
    /// <param name="value">The <see cref="T:Byte[]"/> value to convert.</param>
    /// <returns>Returns a new <see cref="Secret"/> instance.</returns>
    public static Secret ToSecret(this byte[] value) => value;

    /// <summary>
    /// Converts the current <see cref="ReadOnlySpan{Byte}"/> into a new <see cref="Secret"/> instance.
    /// </summary>
    /// <param name="value">The <see cref="ReadOnlySpan{Byte}"/> value to convert.</param>
    /// <returns>Returns a new <see cref="Secret"/> instance.</returns>
    public static Secret ToSecret(this ReadOnlySpan<byte> value) => value;
}
