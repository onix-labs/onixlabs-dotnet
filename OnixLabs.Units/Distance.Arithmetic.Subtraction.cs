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
public readonly partial struct Distance<T>
{
    /// <summary>
    /// Computes the difference of the specified <see cref="Distance{T}"/> values.
    /// </summary>
    /// <param name="left">The left-hand value to subtract from.</param>
    /// <param name="right">The right-hand value to subtract.</param>
    /// <returns>Returns the difference of the specified <see cref="Distance{T}"/> values.</returns>
    public static Distance<T> Subtract(Distance<T> left, Distance<T> right) => new(left.QuectoMeters - right.QuectoMeters);

    /// <summary>
    /// Computes the difference of the specified <see cref="Distance{T}"/> values.
    /// </summary>
    /// <param name="left">The left-hand value to subtract from.</param>
    /// <param name="right">The right-hand value to subtract.</param>
    /// <returns>Returns the difference of the specified <see cref="Distance{T}"/> values.</returns>
    public static Distance<T> operator -(Distance<T> left, Distance<T> right) => Subtract(left, right);

    /// <summary>
    /// Computes the difference of the specified other <see cref="Distance{T}"/> value, subtracted from the current <see cref="Distance{T}"/> value.
    /// </summary>
    /// <param name="other">The value to subtract from the current <see cref="Distance{T}"/> value.</param>
    /// <returns>Returns the difference of the specified other <see cref="Distance{T}"/> value, subtracted from the current <see cref="Distance{T}"/> value.</returns>
    public Distance<T> Subtract(Distance<T> other) => Subtract(this, other);
}
