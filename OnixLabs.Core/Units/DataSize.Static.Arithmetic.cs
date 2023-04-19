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

using System.Numerics;

namespace OnixLabs.Core.Units;

/// <summary>
/// Provides functionality for managing <see cref="DataSize{T}"/> instances.
/// </summary>
public static partial class DataSize
{
    /// <summary>
    /// Computes the sum of the specified <see cref="DataSize{T}"/> values.
    /// </summary>
    /// <param name="left">The left-hand value to add to.</param>
    /// <param name="right">The right-hand value to add.</param>
    /// <typeparam name="T">The underlying <see cref="IFloatingPoint{TSelf}"/> type that represents the <see cref="DataSize{T}"/> unit.</typeparam>
    /// <returns>Returns the sum of the specified <see cref="DataSize{T}"/> values.</returns>
    public static DataSize<T> Add<T>(DataSize<T> left, DataSize<T> right) where T : IFloatingPoint<T>
    {
        return DataSize<T>.Add(left, right);
    }

    /// <summary>
    /// Computes the difference between the specified <see cref="DataSize{T}"/> values.
    /// </summary>
    /// <param name="left">The left-hand value to subtract from.</param>
    /// <param name="right">The right-hand value to subtract.</param>
    /// <typeparam name="T">The underlying <see cref="IFloatingPoint{TSelf}"/> type that represents the <see cref="DataSize{T}"/> unit.</typeparam>
    /// <returns>Returns the difference between the specified <see cref="DataSize{T}"/> values.</returns>
    public static DataSize<T> Subtract<T>(DataSize<T> left, DataSize<T> right) where T : IFloatingPoint<T>
    {
        return DataSize<T>.Subtract(left, right);
    }

    /// <summary>
    /// Computes the product of the specified <see cref="DataSize{T}"/> values.
    /// </summary>
    /// <param name="left">The left-hand value to multiply.</param>
    /// <param name="right">The right-hand value to multiply by.</param>
    /// <typeparam name="T">The underlying <see cref="IFloatingPoint{TSelf}"/> type that represents the <see cref="DataSize{T}"/> unit.</typeparam>
    /// <returns>Returns the product of the specified <see cref="DataSize{T}"/> values.</returns>
    public static DataSize<T> Multiply<T>(DataSize<T> left, DataSize<T> right) where T : IFloatingPoint<T>
    {
        return DataSize<T>.Multiply(left, right);
    }

    /// <summary>
    /// Computes the quotient of the specified <see cref="DataSize{T}"/> values.
    /// </summary>
    /// <param name="left">The left-hand value to divide.</param>
    /// <param name="right">The right-hand value to divide by.</param>
    /// <typeparam name="T">The underlying <see cref="IFloatingPoint{TSelf}"/> type that represents the <see cref="DataSize{T}"/> unit.</typeparam>
    /// <returns>Returns the quotient of the specified <see cref="DataSize{T}"/> values.</returns>
    public static DataSize<T> Divide<T>(DataSize<T> left, DataSize<T> right) where T : IFloatingPoint<T>
    {
        return DataSize<T>.Divide(left, right);
    }
}
