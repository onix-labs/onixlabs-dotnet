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
/// Provides extension methods for <see cref="ReadOnlySpan{T}"/> instances.
/// </summary>
// ReSharper disable UnusedMethodReturnValue.Global
[EditorBrowsable(EditorBrowsableState.Never)]
public static class Extensions
{
    /// <summary>
    /// /// Provides extension methods for <see cref="ReadOnlySpan{T}"/> instances.
    /// </summary>
    /// <param name="receiver">The current <see cref="ReadOnlySpan{T}"/> instance.</param>
    extension(ReadOnlySpan<byte> receiver)
    {
        /// <summary>
        /// Converts the current <see cref="ReadOnlySpan{T}"/> into a new <see cref="Hash"/> instance.
        /// </summary>
        /// <returns>Returns a new <see cref="Hash"/> instance.</returns>
        public Hash ToHash() => receiver;

        /// <summary>
        /// Converts the current <see cref="ReadOnlySpan{T}"/> into a new <see cref="Secret"/> instance.
        /// </summary>
        /// <returns>Returns a new <see cref="Secret"/> instance.</returns>
        public Secret ToSecret() => receiver;
    }
}
