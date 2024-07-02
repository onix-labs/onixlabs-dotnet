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
/// Provides extension methods for read-only sequences.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public static class ReadOnlySequenceExtensions
{
    /// <summary>
    /// Copies the current <see cref="ReadOnlySequence{T}"/> to the specified <see cref="T:[]"/> by reference.
    /// </summary>
    /// <param name="sequence">The current <see cref="ReadOnlySequence{T}"/> to copy.</param>
    /// <param name="array">The <see cref="T:[]"/> to copy to.</param>
    /// <typeparam name="T">The underlying type of the specified <see cref="ReadOnlySequence{T}"/> and <see cref="T:[]"/>.</typeparam>
    public static void CopyTo<T>(this ReadOnlySequence<T> sequence, out T[] array)
    {
        if (sequence.IsSingleSegment)
        {
            array = sequence.First.Span.ToArray();
        }
        else
        {
            array = new T[sequence.Length];
            sequence.CopyTo(array);
        }
    }
}
