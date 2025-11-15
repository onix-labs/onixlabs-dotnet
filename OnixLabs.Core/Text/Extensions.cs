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
/// Provides extension methods <see cref="ReadOnlySpan{T}"/> instances.
/// </summary>
// ReSharper disable UnusedMethodReturnValue.Global
[EditorBrowsable(EditorBrowsableState.Never)]
public static class Extensions
{
    /// <summary>
    /// Provides extension methods <see cref="ReadOnlySpan{T}"/> instances.
    /// </summary>
    /// <param name="receiver">The current <see cref="ReadOnlySpan{T}"/> instance.</param>
    extension(ReadOnlySpan<byte> receiver)
    {
        /// <summary>
        /// Converts the current <see cref="ReadOnlySpan{T}"/> into a new <see cref="Base16"/> instance.
        /// </summary>
        /// <returns>Returns a new <see cref="Base16"/> instance.</returns>
        public Base16 ToBase16() => new(receiver);

        /// <summary>
        /// Converts the current <see cref="ReadOnlySpan{T}"/> into a new <see cref="Base32"/> instance.
        /// </summary>
        /// <returns>Returns a new <see cref="Base32"/> instance.</returns>
        public Base32 ToBase32() => new(receiver);

        /// <summary>
        /// Converts the current <see cref="ReadOnlySpan{T}"/> into a new <see cref="Base58"/> instance.
        /// </summary>
        /// <returns>Returns a new <see cref="Base58"/> instance.</returns>
        public Base58 ToBase58() => new(receiver);

        /// <summary>
        /// Converts the current <see cref="ReadOnlySpan{T}"/> into a new <see cref="Base64"/> instance.
        /// </summary>
        /// <returns>Returns a new <see cref="Base64"/> instance.</returns>
        public Base64 ToBase64() => new(receiver);
    }
}
