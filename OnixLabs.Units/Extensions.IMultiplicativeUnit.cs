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
/// Provides extension methods for <see cref="IMultiplicativeUnit{T}"/> instances.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public static class IMultiplicativeUnitExtensions
{
    /// <summary>
    /// Provides multiplicative operator overloads for <see cref="IMultiplicativeUnit{T}"/> instances.
    /// </summary>
    /// <typeparam name="T">The underlying type of the <see cref="IMultiplicativeUnit{T}"/> instance.</typeparam>
    extension<T>(T) where T : struct, IMultiplicativeUnit<T>
    {
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
    }

    /// <summary>
    /// Provides instance multiplicative methods for <see cref="IMultiplicativeUnit{T}"/> instances.
    /// </summary>
    /// <param name="left">The left-hand value of the multiplicative operation.</param>
    /// <typeparam name="T">The underlying type of the <see cref="IMultiplicativeUnit{T}"/> instance.</typeparam>
    extension<T>(T left) where T : struct, IMultiplicativeUnit<T>
    {
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
