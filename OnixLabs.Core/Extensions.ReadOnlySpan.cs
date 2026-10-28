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
using System.Runtime.InteropServices;

namespace OnixLabs.Core;

/// <summary>
/// Provides extension methods for <see cref="ReadOnlySpan{T}"/> instances.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public static class ReadOnlySpanExtensions
{
    /// <summary>
    /// Provides extension methods for <see cref="ReadOnlySpan{T}"/> of <see cref="byte"/> instances.
    /// </summary>
    /// <param name="receiver">The current <see cref="ReadOnlySpan{T}"/> of <see cref="byte"/> instance.</param>
    extension(ReadOnlySpan<byte> receiver)
    {
        /// <summary>
        /// Gets the content hash code of the bytes in the current <see cref="ReadOnlySpan{T}"/>.
        /// </summary>
        /// <returns>Returns the content hash code of the bytes in the current <see cref="ReadOnlySpan{T}"/>.</returns>
        /// <remarks>
        /// Spans with equal content produce equal hash codes. The bytes are hashed several at a time, without allocating.
        /// Like <see cref="HashCode"/>, the result is randomised per process and must not be persisted.
        /// </remarks>
        public int GetContentHashCode()
        {
            HashCode result = new();
            result.AddBytes(receiver);
            return result.ToHashCode();
        }
    }

    /// <summary>
    /// Provides extension methods for <see cref="ReadOnlySpan{T}"/> of <see cref="char"/> instances.
    /// </summary>
    /// <param name="receiver">The current <see cref="ReadOnlySpan{T}"/> of <see cref="char"/> instance.</param>
    extension(ReadOnlySpan<char> receiver)
    {
        /// <summary>
        /// Gets the ordinal content hash code of the characters in the current <see cref="ReadOnlySpan{T}"/>.
        /// </summary>
        /// <returns>Returns the ordinal content hash code of the characters in the current <see cref="ReadOnlySpan{T}"/>.</returns>
        /// <remarks>
        /// Spans with ordinally equal content produce equal hash codes. The characters are hashed several at a time, without allocating.
        /// Like <see cref="HashCode"/>, the result is randomised per process and must not be persisted.
        /// </remarks>
        public int GetContentHashCode() => MemoryMarshal.AsBytes(receiver).GetContentHashCode();
    }
}
