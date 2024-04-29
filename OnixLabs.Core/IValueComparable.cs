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

namespace OnixLabs.Core;

/// <summary>
/// Defines an extension to the default <see cref="IComparable{T}"/> and <see cref="IComparable"/> interfaces,
/// including equality, inequality, greater than, greater than or equal, less than, and less than or equal operators.
/// </summary>
/// <typeparam name="T">The underlying type of the objects to compare.</typeparam>
public interface IValueComparable<in T> : IComparable<T>, IComparable where T : IValueComparable<T>
{
    /// <summary>
    /// Performs an equality comparison between two object instances.
    /// </summary>
    /// <param name="left">The left-hand instance to compare.</param>
    /// <param name="right">The right-hand instance to compare.</param>
    /// <returns>Returns <see langword="true"/> if the left-hand instance is equal to the right-hand instance; otherwise, <see langword="false"/>.</returns>
    public static abstract bool operator ==(T left, T right);

    /// <summary>
    /// Performs an inequality comparison between two object instances.
    /// </summary>
    /// <param name="left">The left-hand instance to compare.</param>
    /// <param name="right">The right-hand instance to compare.</param>
    /// <returns>Returns <see langword="true"/> if the left-hand instance is not equal to the right-hand instance; otherwise, <see langword="false"/>.</returns>
    public static abstract bool operator !=(T left, T right);

    /// <summary>
    /// Performs a greater than comparison between two object instances.
    /// </summary>
    /// <param name="left">The left-hand instance to compare.</param>
    /// <param name="right">The right-hand instance to compare.</param>
    /// <returns>Returns <see langword="true"/> if the left-hand instance is greater than the right-hand instance; otherwise, <see langword="false"/>.</returns>
    public static abstract bool operator >(T left, T right);

    /// <summary>
    /// Performs a greater than or equal comparison between two object instances.
    /// </summary>
    /// <param name="left">The left-hand instance to compare.</param>
    /// <param name="right">The right-hand instance to compare.</param>
    /// <returns>Returns <see langword="true"/> if the left-hand instance is greater than or equal to the right-hand instance; otherwise, <see langword="false"/>.</returns>
    public static abstract bool operator >=(T left, T right);

    /// <summary>
    /// Performs a less than comparison between two object instances.
    /// </summary>
    /// <param name="left">The left-hand instance to compare.</param>
    /// <param name="right">The right-hand instance to compare.</param>
    /// <returns>Returns <see langword="true"/> if the left-hand instance is less than the right-hand instance; otherwise, <see langword="false"/>.</returns>
    public static abstract bool operator <(T left, T right);

    /// <summary>
    /// Performs a less than or equal comparison between two object instances.
    /// </summary>
    /// <param name="left">The left-hand instance to compare.</param>
    /// <param name="right">The right-hand instance to compare.</param>
    /// <returns>Returns <see langword="true"/> if the left-hand instance is less than or equal to the right-hand instance; otherwise, <see langword="false"/>.</returns>
    public static abstract bool operator <=(T left, T right);
}
