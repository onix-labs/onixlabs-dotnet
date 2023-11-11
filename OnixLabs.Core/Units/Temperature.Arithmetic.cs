// Copyright © 2020 ONIXLabs
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

namespace OnixLabs.Core.Units;

public readonly partial struct Temperature<T>
{
    /// <summary>
    /// Computes the sum of the specified <see cref="Temperature{T}"/> values.
    /// </summary>
    /// <param name="left">The left-hand value to add to.</param>
    /// <param name="right">The right-hand value to add.</param>
    /// <returns>Returns the sum of the specified <see cref="Temperature{T}"/> values.</returns>
    public static Temperature<T> Add(Temperature<T> left, Temperature<T> right)
    {
        return new Temperature<T>(left.Kelvin + right.Kelvin);
    }

    /// <summary>
    /// Computes the difference between the specified <see cref="Temperature{T}"/> values.
    /// </summary>
    /// <param name="left">The left-hand value to subtract from.</param>
    /// <param name="right">The right-hand value to subtract.</param>
    /// <returns>Returns the difference between the specified <see cref="Temperature{T}"/> values.</returns>
    public static Temperature<T> Subtract(Temperature<T> left, Temperature<T> right)
    {
        return new Temperature<T>(left.Kelvin - right.Kelvin);
    }

    /// <summary>
    /// Computes the product of the specified <see cref="Temperature{T}"/> values.
    /// </summary>
    /// <param name="left">The left-hand value to multiply.</param>
    /// <param name="right">The right-hand value to multiply by.</param>
    /// <returns>Returns the product of the specified <see cref="Temperature{T}"/> values.</returns>
    public static Temperature<T> Multiply(Temperature<T> left, Temperature<T> right)
    {
        return new Temperature<T>(left.Kelvin * right.Kelvin);
    }

    /// <summary>
    /// Computes the quotient of the specified <see cref="Temperature{T}"/> values.
    /// </summary>
    /// <param name="left">The left-hand value to divide.</param>
    /// <param name="right">The right-hand value to divide by.</param>
    /// <returns>Returns the quotient of the specified <see cref="Temperature{T}"/> values.</returns>
    public static Temperature<T> Divide(Temperature<T> left, Temperature<T> right)
    {
        return new Temperature<T>(left.Kelvin / right.Kelvin);
    }

    /// <summary>
    /// Computes the sum of the specified <see cref="Temperature{T}"/> values.
    /// </summary>
    /// <param name="left">The left-hand value to add to.</param>
    /// <param name="right">The right-hand value to add.</param>
    /// <returns>Returns the sum of the specified <see cref="Temperature{T}"/> values.</returns>
    public static Temperature<T> operator +(Temperature<T> left, Temperature<T> right)
    {
        return Add(left, right);
    }

    /// <summary>
    /// Computes the difference between the specified <see cref="Temperature{T}"/> values.
    /// </summary>
    /// <param name="left">The left-hand value to subtract from.</param>
    /// <param name="right">The right-hand value to subtract.</param>
    /// <returns>Returns the difference between the specified <see cref="Temperature{T}"/> values.</returns>
    public static Temperature<T> operator -(Temperature<T> left, Temperature<T> right)
    {
        return Subtract(left, right);
    }

    /// <summary>
    /// Computes the product of the specified <see cref="Temperature{T}"/> values.
    /// </summary>
    /// <param name="left">The left-hand value to multiply.</param>
    /// <param name="right">The right-hand value to multiply by.</param>
    /// <returns>Returns the product of the specified <see cref="Temperature{T}"/> values.</returns>
    public static Temperature<T> operator *(Temperature<T> left, Temperature<T> right)
    {
        return Multiply(left, right);
    }

    /// <summary>
    /// Computes the quotient of the specified <see cref="Temperature{T}"/> values.
    /// </summary>
    /// <param name="left">The left-hand value to divide.</param>
    /// <param name="right">The right-hand value to divide by.</param>
    /// <returns>Returns the quotient of the specified <see cref="Temperature{T}"/> values.</returns>
    public static Temperature<T> operator /(Temperature<T> left, Temperature<T> right)
    {
        return Divide(left, right);
    }
}
