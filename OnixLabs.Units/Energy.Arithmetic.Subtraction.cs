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

using System;
using System.Numerics;
using OnixLabs.Core;

namespace OnixLabs.Units;

public readonly partial struct Energy<T>
{
    /// <summary>
    /// Computes the difference between the specified <see cref="Energy{T}"/> values.
    /// </summary>
    /// <param name="left">The left-hand value to subtract from.</param>
    /// <param name="right">The right-hand value to subtract.</param>
    /// <returns>Returns the difference between the specified <see cref="Energy{T}"/> values.</returns>
    public static Energy<T> Subtract(Energy<T> left, Energy<T> right) => new(left.YoctoJoules - right.YoctoJoules);

    /// <summary>
    /// Computes the difference between the specified <see cref="Energy{T}"/> values.
    /// </summary>
    /// <param name="left">The left-hand value to subtract from.</param>
    /// <param name="right">The right-hand value to subtract.</param>
    /// <returns>Returns the difference between the specified <see cref="Energy{T}"/> values.</returns>
    public static Energy<T> operator -(Energy<T> left, Energy<T> right) => Subtract(left, right);

    /// <summary>
    /// Computes the difference between the current <see cref="Energy{T}"/> value and the specified other <see cref="Energy{T}"/> value.
    /// </summary>
    /// <param name="other">The value to subtract from the current <see cref="Energy{T}"/> value.</param>
    /// <returns>
    /// Returns the difference between the current <see cref="Energy{T}"/> value and the specified other <see cref="Energy{T}"/> value.
    /// </returns>
    public Energy<T> Subtract(Energy<T> other) => Subtract(this, other);
}
