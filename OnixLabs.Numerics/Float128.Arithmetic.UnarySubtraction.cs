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

using System;

namespace OnixLabs.Numerics;

public readonly partial struct Float128
{
    /// <summary>
    /// Computes the unary subtraction of the specified <see cref="Float128"/> value.
    /// </summary>
    /// <param name="value">The value for which to perform unary subtraction.</param>
    /// <returns>Returns the unary subtraction of the specified <see cref="Float128"/> value.</returns>
    /// <remarks>
    /// Negation toggles the sign bit only; NaN bit patterns are preserved (with their sign bit flipped).
    /// </remarks>
    public static Float128 UnarySubtract(Float128 value) => new(value.RawBits ^ SignMask);

    /// <summary>
    /// Computes the unary subtraction of the specified <see cref="Float128"/> value.
    /// </summary>
    /// <param name="value">The value for which to perform unary subtraction.</param>
    /// <returns>Returns the unary subtraction of the specified <see cref="Float128"/> value.</returns>
    public static Float128 operator -(Float128 value) => UnarySubtract(value);
}
