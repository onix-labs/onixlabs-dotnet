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
using System.Text;

namespace OnixLabs.Core.Text;

/// <summary>
/// Provides extension methods for <see cref="Encoding"/> instances.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
internal static class EncodingExtensions
{
    /// <summary>
    /// Provides extension methods for <see cref="Encoding"/> instances.
    /// </summary>
    /// <param name="receiver">The current <see cref="Encoding"/> instance.</param>
    extension(Encoding? receiver)
    {
        /// <summary>
        /// Gets the current <see cref="Encoding"/>, or the default encoding if the current <see cref="Encoding"/> is <see langword="null"/>.
        /// </summary>
        /// <remarks>The default encoding is <see cref="Encoding.UTF8"/>.</remarks>
        /// <returns>Returns the current <see cref="Encoding"/>, or the default encoding if the current <see cref="Encoding"/> is <see langword="null"/>.</returns>
        public Encoding GetOrDefault() => receiver ?? Encoding.UTF8;

        /// <summary>
        /// Gets a <see cref="byte"/> array from the specified <see cref="ReadOnlySpan{T}"/> value.
        /// </summary>
        /// <param name="value">The <see cref="ReadOnlySpan{T}"/> of <see cref="char"/> to convert into a <see cref="byte"/> array.</param>
        /// <returns>Returns a <see cref="byte"/> array from the specified <see cref="ReadOnlySpan{T}"/> value.</returns>
        public byte[] GetBytes(ReadOnlySpan<char> value)
        {
            Encoding encodingOrDefault = receiver.GetOrDefault();
            int count = encodingOrDefault.GetByteCount(value);
            byte[] result = new byte[count];

            encodingOrDefault.GetBytes(value, result);

            return result;
        }
    }
}
