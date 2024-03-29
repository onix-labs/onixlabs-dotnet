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

public readonly partial struct DataSize<T>
{
    /// <summary>
    /// Computes the sum of the specified <see cref="DataSize{T}"/> values.
    /// </summary>
    /// <param name="left">The left-hand value to add to.</param>
    /// <param name="right">The right-hand value to add.</param>
    /// <returns>Returns the sum of the specified <see cref="DataSize{T}"/> values.</returns>
    public static DataSize<T> Add(DataSize<T> left, DataSize<T> right)
    {
        return new DataSize<T>(left.Bits + right.Bits);
    }

    /// <summary>
    /// Computes the difference between the specified <see cref="DataSize{T}"/> values.
    /// </summary>
    /// <param name="left">The left-hand value to subtract from.</param>
    /// <param name="right">The right-hand value to subtract.</param>
    /// <returns>Returns the difference between the specified <see cref="DataSize{T}"/> values.</returns>
    public static DataSize<T> Subtract(DataSize<T> left, DataSize<T> right)
    {
        return new DataSize<T>(left.Bits - right.Bits);
    }

    /// <summary>
    /// Computes the product of the specified <see cref="DataSize{T}"/> values.
    /// </summary>
    /// <param name="left">The left-hand value to multiply.</param>
    /// <param name="right">The right-hand value to multiply by.</param>
    /// <returns>Returns the product of the specified <see cref="DataSize{T}"/> values.</returns>
    public static DataSize<T> Multiply(DataSize<T> left, DataSize<T> right)
    {
        return new DataSize<T>(left.Bits * right.Bits);
    }

    /// <summary>
    /// Computes the quotient of the specified <see cref="DataSize{T}"/> values.
    /// </summary>
    /// <param name="left">The left-hand value to divide.</param>
    /// <param name="right">The right-hand value to divide by.</param>
    /// <returns>Returns the quotient of the specified <see cref="DataSize{T}"/> values.</returns>
    public static DataSize<T> Divide(DataSize<T> left, DataSize<T> right)
    {
        return new DataSize<T>(left.Bits / right.Bits);
    }

    /// <summary>
    /// Computes the sum of the specified <see cref="DataSize{T}"/> values.
    /// </summary>
    /// <param name="left">The left-hand value to add to.</param>
    /// <param name="right">The right-hand value to add.</param>
    /// <returns>Returns the sum of the specified <see cref="DataSize{T}"/> values.</returns>
    public static DataSize<T> operator +(DataSize<T> left, DataSize<T> right)
    {
        return Add(left, right);
    }

    /// <summary>
    /// Computes the difference between the specified <see cref="DataSize{T}"/> values.
    /// </summary>
    /// <param name="left">The left-hand value to subtract from.</param>
    /// <param name="right">The right-hand value to subtract.</param>
    /// <returns>Returns the difference between the specified <see cref="DataSize{T}"/> values.</returns>
    public static DataSize<T> operator -(DataSize<T> left, DataSize<T> right)
    {
        return Subtract(left, right);
    }

    /// <summary>
    /// Computes the product of the specified <see cref="DataSize{T}"/> values.
    /// </summary>
    /// <param name="left">The left-hand value to multiply.</param>
    /// <param name="right">The right-hand value to multiply by.</param>
    /// <returns>Returns the product of the specified <see cref="DataSize{T}"/> values.</returns>
    public static DataSize<T> operator *(DataSize<T> left, DataSize<T> right)
    {
        return Multiply(left, right);
    }

    /// <summary>
    /// Computes the quotient of the specified <see cref="DataSize{T}"/> values.
    /// </summary>
    /// <param name="left">The left-hand value to divide.</param>
    /// <param name="right">The right-hand value to divide by.</param>
    /// <returns>Returns the quotient of the specified <see cref="DataSize{T}"/> values.</returns>
    public static DataSize<T> operator /(DataSize<T> left, DataSize<T> right)
    {
        return Divide(left, right);
    }
}
