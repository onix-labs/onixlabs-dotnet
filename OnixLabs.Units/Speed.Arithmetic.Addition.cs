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
public readonly partial struct Speed<T>
{
    /// <summary>
    /// Computes the sum of the specified <see cref="Speed{T}"/> values.
    /// </summary>
    /// <param name="left">The left-hand value to add to.</param>
    /// <param name="right">The right-hand value to add.</param>
    /// <returns>Returns the sum of the specified <see cref="Speed{T}"/> values.</returns>
    public static Speed<T> Add(Speed<T> left, Speed<T> right) => new(left.QuectoMetersPerSecond + right.QuectoMetersPerSecond);

    /// <summary>
    /// Computes the sum of the specified <see cref="Speed{T}"/> values.
    /// </summary>
    /// <param name="left">The left-hand value to add to.</param>
    /// <param name="right">The right-hand value to add.</param>
    /// <returns>Returns the sum of the specified <see cref="Speed{T}"/> values.</returns>
    public static Speed<T> operator +(Speed<T> left, Speed<T> right) => Add(left, right);

    /// <summary>
    /// Computes the sum of the current <see cref="Speed{T}"/> value and the specified other <see cref="Speed{T}"/> value.
    /// </summary>
    /// <param name="other">The value to add to the current <see cref="Speed{T}"/> value.</param>
    /// <returns>Returns the sum of the current <see cref="Speed{T}"/> value and the specified other <see cref="Speed{T}"/> value.</returns>
    public Speed<T> Add(Speed<T> other) => Add(this, other);
}
