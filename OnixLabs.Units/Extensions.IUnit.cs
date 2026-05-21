// Copyright 2020-2025 ONIXLabs
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

namespace OnixLabs.Units;

/// <summary>
/// Provides extension methods for <see cref="IUnit{T}"/> instances.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public static class IUnitExtensions
{
    /// <summary>
    /// Provides operator overloads for <see cref="IUnit{T}"/> instances.
    /// </summary>
    /// <typeparam name="T">The underlying type of the <see cref="IUnit{T}"/> instance.</typeparam>
    extension<T>(T) where T : struct, IUnit<T>
    {
        /// <summary>
        /// Determines whether the specified left-hand and right-hand <typeparamref name="T"/> values are equal.
        /// </summary>
        /// <param name="left">The left-hand value to compare.</param>
        /// <param name="right">The right-hand value to compare.</param>
        /// <returns>Returns <see langword="true"/> if the specified left-hand and right-hand <typeparamref name="T"/> values are equal; otherwise <see langword="false"/>.</returns>
        public static bool operator ==(T left, T right) => T.Equals(left, right);

        /// <summary>
        /// Determines whether the specified left-hand and right-hand <typeparamref name="T"/> values are not equal.
        /// </summary>
        /// <param name="left">The left-hand value to compare.</param>
        /// <param name="right">The right-hand value to compare.</param>
        /// <returns>Returns <see langword="true"/> if the specified left-hand and right-hand <typeparamref name="T"/> values are not equal; otherwise <see langword="false"/>.</returns>
        public static bool operator !=(T left, T right) => !T.Equals(left, right);

        /// <summary>
        /// Determines whether the specified left-hand <typeparamref name="T"/> value is greater than the specified right-hand <typeparamref name="T"/> value.
        /// </summary>
        /// <param name="left">The left-hand value to compare.</param>
        /// <param name="right">The right-hand value to compare.</param>
        /// <returns>Returns <see langword="true"/> if the specified left-hand <typeparamref name="T"/> value is greater than the specified right-hand <typeparamref name="T"/> value; otherwise <see langword="false"/>.</returns>
        public static bool operator >(T left, T right) => T.Compare(left, right) is 1;

        /// <summary>
        /// Determines whether the specified left-hand <typeparamref name="T"/> value is greater than or equal to the specified right-hand <typeparamref name="T"/> value.
        /// </summary>
        /// <param name="left">The left-hand value to compare.</param>
        /// <param name="right">The right-hand value to compare.</param>
        /// <returns>Returns <see langword="true"/> if the specified left-hand <typeparamref name="T"/> value is greater than or equal to the specified right-hand <typeparamref name="T"/> value; otherwise <see langword="false"/>.</returns>
        public static bool operator >=(T left, T right) => T.Compare(left, right) is 1 or 0;

        /// <summary>
        /// Determines whether the specified left-hand <typeparamref name="T"/> value is less than the specified right-hand <typeparamref name="T"/> value.
        /// </summary>
        /// <param name="left">The left-hand value to compare.</param>
        /// <param name="right">The right-hand value to compare.</param>
        /// <returns>Returns <see langword="true"/> if the specified left-hand <typeparamref name="T"/> value is less than the specified right-hand <typeparamref name="T"/> value; otherwise <see langword="false"/>.</returns>
        public static bool operator <(T left, T right) => T.Compare(left, right) is -1;

        /// <summary>
        /// Determines whether the specified left-hand <typeparamref name="T"/> value is less than or equal to the specified right-hand <typeparamref name="T"/> value.
        /// </summary>
        /// <param name="left">The left-hand value to compare.</param>
        /// <param name="right">The right-hand value to compare.</param>
        /// <returns>Returns <see langword="true"/> if the specified left-hand <typeparamref name="T"/> value is less than or equal to the specified right-hand <typeparamref name="T"/> value; otherwise <see langword="false"/>.</returns>
        public static bool operator <=(T left, T right) => T.Compare(left, right) is -1 or 0;
    }
}
