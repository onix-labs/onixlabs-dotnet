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

using System.ComponentModel;
using System.Text;

namespace OnixLabs.Core;

/// <summary>
/// Provides extension methods for arrays.
/// </summary>
// ReSharper disable HeapView.ObjectAllocation
[EditorBrowsable(EditorBrowsableState.Never)]
public static class ArrayExtensions
{
    /// <summary>
    /// Provides extensions for generic arrays.
    /// </summary>
    /// <param name="receiver">The current generic array.</param>
    /// <typeparam name="T">The underlying element type of the current generic array.</typeparam>
    extension<T>(T[] receiver)
    {
        /// <summary>
        /// Creates a copy of the current <typeparamref name="T"/> array.
        /// </summary>
        /// <returns>Returns an exact copy of the current <typeparamref name="T"/> array.</returns>
        public T[] Copy() => [..receiver];

        /// <summary>
        /// Creates a copy of the current <typeparamref name="T"/> array.
        /// </summary>
        /// <param name="index">The index of the array to begin copying from.</param>
        /// <param name="count">The number of elements of the array to copy.</param>
        /// <returns>Returns an exact copy of the current <typeparamref name="T"/> array.</returns>
        public T[] Copy(int index, int count) => [..receiver[index..(index + count)]];

        /// <summary>
        /// Concatenates the current <typeparamref name="T"/> array with another <typeparamref name="T"/> array.
        /// </summary>
        /// <param name="other">The other <typeparamref name="T"/> array to concatenate with the current <typeparamref name="T"/> array.</param>
        /// <returns>Returns a new <typeparamref name="T"/> array, consisting of the current <typeparamref name="T"/> array concatenated with the other <typeparamref name="T"/> array.</returns>
        public T[] ConcatenateWith(T[] other) => [..receiver, ..other];
    }

    /// <summary>
    /// Provides extension methods for <see cref="byte"/> arrays.
    /// </summary>
    /// <param name="array">The current <see cref="byte"/> array.</param>
    extension(byte[] array)
    {
        /// <summary>
        /// Obtains the <see cref="string"/> representation of the current <see cref="byte"/> array.
        /// </summary>
        /// <param name="encoding">The <see cref="Encoding"/> which will be used to convert the current <see cref="byte"/> array into a <see cref="string"/> representation.</param>
        /// <returns>Returns the <see cref="string"/> representation of the current <see cref="byte"/> array.</returns>
        public string ToString(Encoding encoding) => encoding.GetString(array);
    }
}
