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
public readonly partial struct Force<T>
{
    /// <summary>
    /// Computes the quotient of the specified <see cref="Force{T}"/> values.
    /// </summary>
    /// <param name="left">The left-hand value to divide.</param>
    /// <param name="right">The right-hand value to divide by.</param>
    /// <returns>Returns the quotient of the specified <see cref="Force{T}"/> values.</returns>
    public static Force<T> Divide(Force<T> left, Force<T> right) => new(left.YoctoNewtons / right.YoctoNewtons);

    /// <summary>
    /// Computes the quotient of the specified <see cref="Force{T}"/> values.
    /// </summary>
    /// <param name="left">The left-hand value to divide.</param>
    /// <param name="right">The right-hand value to divide by.</param>
    /// <returns>Returns the quotient of the specified <see cref="Force{T}"/> values.</returns>
    public static Force<T> operator /(Force<T> left, Force<T> right) => Divide(left, right);

    /// <summary>
    /// Computes the quotient of the current <see cref="Force{T}"/> value, divided by the specified other <see cref="Force{T}"/> value.
    /// </summary>
    /// <param name="other">The value to divide the current <see cref="Force{T}"/> value by.</param>
    /// <returns>Returns the quotient of the current <see cref="Force{T}"/> value, divided by the specified other <see cref="Force{T}"/> value.</returns>
    public Force<T> Divide(Force<T> other) => Divide(this, other);
}
