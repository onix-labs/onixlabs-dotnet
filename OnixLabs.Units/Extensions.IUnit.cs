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
    /// Provides extension methods for <see cref="IUnit{T}"/> instances.
    /// </summary>
    /// <typeparam name="T">The underlying type of the <see cref="IUnit{T}"/> instance.</typeparam>
    extension<T>(T) where T : struct, IUnit<T>
    {
        /// <summary>
        /// Computes the sum of the specified <typeparamref name="T"/> values.
        /// </summary>
        /// <param name="left">The left-hand value to add to.</param>
        /// <param name="right">The right-hand value to add.</param>
        /// <returns>Returns the sum of the specified <typeparamref name="T"/> values.</returns>
        public static T operator +(T left, T right) => T.Add(left, right);

        /// <summary>
        /// Computes the difference of the specified <typeparamref name="T"/> values.
        /// </summary>
        /// <param name="left">The left-hand value to subtract from.</param>
        /// <param name="right">The right-hand value to subtract.</param>
        /// <returns>Returns the difference of the specified <typeparamref name="T"/> values.</returns>
        public static T operator -(T left, T right) => T.Subtract(left, right);

        /// <summary>
        /// Computes the product of the specified <typeparamref name="T"/> values.
        /// </summary>
        /// <param name="left">The left-hand value to multiply.</param>
        /// <param name="right">The right-hand value to multiply by.</param>
        /// <returns>Returns the product of the specified <typeparamref name="T"/> values.</returns>
        public static T operator *(T left, T right) => T.Multiply(left, right);

        /// <summary>
        /// Computes the quotient of the specified <typeparamref name="T"/> values.
        /// </summary>
        /// <param name="left">The left-hand value to divide.</param>
        /// <param name="right">The right-hand value to divide by.</param>
        /// <returns>Returns the quotient of the specified <typeparamref name="T"/> values.</returns>
        public static T operator /(T left, T right) => T.Divide(left, right);

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

    /// <summary>
    /// Provides extension methods for <see cref="IUnit{T}"/> instances.
    /// </summary>
    /// <param name="left">The left-hand value of the arithmetic operation.</param>
    /// <typeparam name="T">The underlying type of the <see cref="IUnit{T}"/> instance.</typeparam>
    extension<T>(T left) where T : struct, IUnit<T>
    {
        /// <summary>
        /// Computes the sum of the current <typeparamref name="T"/> value and the specified other <typeparamref name="T"/> value.
        /// </summary>
        /// <param name="right">The value to add to the current <typeparamref name="T"/> value.</param>
        /// <returns>Returns the sum of the current <typeparamref name="T"/> value and the specified other <typeparamref name="T"/> value.</returns>
        public T Add(T right) => T.Add(left, right);

        /// <summary>
        /// Computes the difference of the current <typeparamref name="T"/> value and the specified other <typeparamref name="T"/> value.
        /// </summary>
        /// <param name="right">The value to subtract from the current <typeparamref name="T"/> value.</param>
        /// <returns>Returns the difference of the current <typeparamref name="T"/> value and the specified other <typeparamref name="T"/> value.</returns>
        public T Subtract(T right) => T.Subtract(left, right);

        /// <summary>
        /// Computes the product of the current <typeparamref name="T"/> value and the specified other <typeparamref name="T"/> value.
        /// </summary>
        /// <param name="right">The value to multiply the current <typeparamref name="T"/> value by.</param>
        /// <returns>Returns the product of the current <typeparamref name="T"/> value and the specified other <typeparamref name="T"/> value.</returns>
        public T Multiply(T right) => T.Multiply(left, right);

        /// <summary>
        /// Computes the quotient of the current <typeparamref name="T"/> value and the specified other <typeparamref name="T"/> value.
        /// </summary>
        /// <param name="right">The value to divide the current <typeparamref name="T"/> value by.</param>
        /// <returns>Returns the quotient of the current <typeparamref name="T"/> value and the specified other <typeparamref name="T"/> value.</returns>
        public T Divide(T right) => T.Divide(left, right);
    }
}
