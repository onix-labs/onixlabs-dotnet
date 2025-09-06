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

namespace OnixLabs.Units;

// ReSharper disable MemberCanBePrivate.Global
public readonly partial struct DataSize<T>
{
    /// <summary>
    /// Computes the product of the specified <see cref="DataSize{T}"/> values.
    /// </summary>
    /// <param name="left">The left-hand value to multiply.</param>
    /// <param name="right">The right-hand value to multiply by.</param>
    /// <returns>Returns the product of the specified <see cref="DataSize{T}"/> values.</returns>
    public static DataSize<T> Multiply(DataSize<T> left, DataSize<T> right) => new(left.Bits * right.Bits);

    /// <summary>
    /// Computes the product of the specified <see cref="DataSize{T}"/> values.
    /// </summary>
    /// <param name="left">The left-hand value to multiply.</param>
    /// <param name="right">The right-hand value to multiply by.</param>
    /// <returns>Returns the product of the specified <see cref="DataSize{T}"/> values.</returns>
    public static DataSize<T> operator *(DataSize<T> left, DataSize<T> right) => Multiply(left, right);

    /// <summary>
    /// Computes the product of the current <see cref="DataSize{T}"/> value, multiplied by the specified other <see cref="DataSize{T}"/> value.
    /// </summary>
    /// <param name="other">The value to multiply the current <see cref="DataSize{T}"/> value by.</param>
    /// <returns>Returns the product of the current <see cref="DataSize{T}"/> value, multiplied by the specified other <see cref="DataSize{T}"/> value.</returns>
    public DataSize<T> Multiply(DataSize<T> other) => Multiply(this, other);
}
