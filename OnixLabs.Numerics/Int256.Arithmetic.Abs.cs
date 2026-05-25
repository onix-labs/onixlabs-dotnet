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

public readonly partial struct Int256
{
    /// <summary>Returns the absolute value of the specified <see cref="Int256"/>.</summary>
    /// <param name="value">The value whose absolute value is to be computed.</param>
    /// <returns>Returns the absolute value of <paramref name="value"/>.</returns>
    /// <exception cref="OverflowException">Thrown when <paramref name="value"/> equals <see cref="MinValue"/>.</exception>
    public static Int256 Abs(Int256 value)
    {
        if (value == MinValue) throw new OverflowException($"Value was either too large or too small for the specified type: {nameof(Int256)}.");
        return IsNegative(value) ? -value : value;
    }

    /// <summary>Returns the absolute value of the specified <see cref="Int256"/> without throwing on overflow.</summary>
    /// <param name="value">The value whose absolute value is to be computed.</param>
    /// <returns>Returns the absolute value; <see cref="MinValue"/> is returned unchanged.</returns>
    internal static Int256 AbsUnchecked(in Int256 value) => IsNegative(value) ? -value : value;

    /// <summary>Returns the negation of the specified <see cref="Int256"/>.</summary>
    /// <param name="value">The value to negate.</param>
    /// <returns>Returns the negation of <paramref name="value"/>.</returns>
    public static Int256 Negate(Int256 value) => -value;

    /// <summary>Returns the sign of the specified <see cref="Int256"/>.</summary>
    /// <param name="value">The value whose sign is to be returned.</param>
    /// <returns>Returns -1 for negative, 0 for zero, and 1 for positive.</returns>
    public static int Sign(Int256 value)
    {
        if (IsZero(value)) return 0;
        return IsNegative(value) ? -1 : 1;
    }

    /// <summary>Copies the sign of <paramref name="sign"/> onto the magnitude of <paramref name="value"/>.</summary>
    /// <param name="value">The value whose magnitude is to be used.</param>
    /// <param name="sign">The value whose sign is to be used.</param>
    /// <returns>Returns a value with the magnitude of <paramref name="value"/> and the sign of <paramref name="sign"/>.</returns>
    public static Int256 CopySign(Int256 value, Int256 sign)
    {
        Int256 magnitude = AbsUnchecked(value);
        return IsNegative(sign) ? -magnitude : magnitude;
    }
}
