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

using System.Buffers;
using System.ComponentModel;

namespace OnixLabs.Core;

/// <summary>
/// Provides extension methods for <see cref="ReadOnlySequence{T}"/> instances.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public static class ReadOnlySequenceExtensions
{
    /// <summary>
    /// Provides extension methods for <see cref="ReadOnlySequence{T}"/> instances.
    /// </summary>
    /// <param name="receiver">The current <see cref="ReadOnlySequence{T}"/> instance.</param>
    /// <typeparam name="T">The underlying type of the current <see cref="ReadOnlySequence{T}"/> instance.</typeparam>
    extension<T>(ReadOnlySequence<T> receiver)
    {
        /// <summary>
        /// Copies the current <see cref="ReadOnlySequence{T}"/> to the specified <typeparamref name="T"/> array.
        /// </summary>
        /// <param name="array">The <typeparamref name="T"/> array to copy in to.</param>
        public void CopyTo(out T[] array)
        {
            if (receiver.IsSingleSegment)
                array = receiver.First.Span.ToArray();
            else
            {
                // ReSharper disable once HeapView.ObjectAllocation.Evident
                array = new T[receiver.Length];
                receiver.CopyTo(array);
            }
        }
    }
}
