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

namespace OnixLabs.Core;

/// <summary>
/// Provides extension methods for arrays.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public static class ArrayExtensions
{
    /// <summary>
    /// Creates a copy of the current <see cref="T:T[]"/>.
    /// </summary>
    /// <param name="array">The current <see cref="T:T[]"/> to copy.</param>
    /// <typeparam name="T">The underlying type of the array.</typeparam>
    /// <returns>Returns an exact copy of the current <see cref="T:T[]"/>.</returns>
    public static T[] Copy<T>(this T[] array)
    {
        return [..array];
    }

    /// <summary>
    /// Creates a copy of the current <see cref="T:T[]"/>.
    /// </summary>
    /// <param name="array">The current <see cref="T:T[]"/> to copy.</param>
    /// <param name="index">The index of the array to begin copying from.</param>
    /// <param name="count">The number of elements of the array to copy.</param>
    /// <typeparam name="T">The underlying type of the array.</typeparam>
    /// <returns>Returns an exact copy of the current <see cref="T:T[]"/>.</returns>
    public static T[] Copy<T>(this T[] array, int index, int count)
    {
        return [..array[index..(index + count)]];
    }

    /// <summary>
    /// Concatenates the current <see cref="T:T[]"/> with another <see cref="T:T[]"/>.
    /// </summary>
    /// <param name="array">The source <see cref="T:T[]"/> to concatenate with the other <see cref="T:T[]"/>.</param>
    /// <param name="other">The other <see cref="T:T[]"/> to concatenate with the source <see cref="T:T[]"/>.</param>
    /// <typeparam name="T">The underlying type of the <see cref="T:T[]"/>.</typeparam>
    /// <returns>Returns the current <see cref="T:T[]"/> concatenated with the other <see cref="T:T[]"/>.</returns>
    public static T[] ConcatenateWith<T>(this T[] array, T[] other)
    {
        return [..array, ..other];
    }
}
