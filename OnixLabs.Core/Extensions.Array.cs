// Copyright 2020-2023 ONIXLabs
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

namespace OnixLabs.Core;

/// <summary>
/// Provides extension methods for arrays.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public static class ArrayExtensions
{
    /// <summary>
    /// Creates a copy of the current array.
    /// </summary>
    /// <param name="array">The array to copy.</param>
    /// <typeparam name="T">The underlying type of the array.</typeparam>
    /// <returns>Returns an exact copy of the current array.</returns>
    public static T[] Copy<T>(this T[] array)
    {
        return Copy(array, 0, array.Length);
    }

    /// <summary>
    /// Creates a copy of the current array.
    /// </summary>
    /// <param name="array">The array to copy.</param>
    /// <param name="index">The index of the array to begin copying from.</param>
    /// <param name="count">The number of elements of the array to copy.</param>
    /// <typeparam name="T">The underlying type of the array.</typeparam>
    /// <returns>Returns an exact copy of the current array.</returns>
    public static T[] Copy<T>(this T[] array, int index, int count)
    {
        T[] result = new T[count];
        Array.Copy(array, index, result, 0, count);

        return result;
    }

    /// <summary>
    /// Concatenates the current array with another array.
    /// </summary>
    /// <param name="array">The source array to concatenate with the other array.</param>
    /// <param name="other">The other array to concatenate with the source array.</param>
    /// <typeparam name="T">The underlying type of the array.</typeparam>
    /// <returns>Returns the current array concatenated with the other array.</returns>
    public static T[] ConcatenateWith<T>(this T[] array, T[] other)
    {
        T[] result = new T[array.Length + other.Length];

        Array.Copy(array, 0, result, 0, array.Length);
        Array.Copy(other, 0, result, array.Length, other.Length);

        return result;
    }
}
