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

public readonly partial struct Int512
{
    /// <summary>Computes the sum of two <see cref="Int512"/> values, wrapping on overflow.</summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns>Returns the wrapping sum of <paramref name="left"/> and <paramref name="right"/>.</returns>
    public static Int512 operator +(Int512 left, Int512 right)
    {
        UInt256 newLower = left.Lower + right.Lower;
        UInt256 carry = newLower < left.Lower ? UInt256.One : UInt256.Zero;
        UInt256 newUpper = left.Upper + right.Upper + carry;
        return new Int512(newUpper, newLower);
    }

    /// <summary>Computes the sum of two <see cref="Int512"/> values, throwing on overflow.</summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns>Returns the sum.</returns>
    /// <exception cref="OverflowException">Thrown when the sum overflows the range of <see cref="Int512"/>.</exception>
    public static Int512 operator checked +(Int512 left, Int512 right)
    {
        Int512 result = left + right;
        bool leftNegative = IsNegative(left);
        bool rightNegative = IsNegative(right);
        bool resultNegative = IsNegative(result);

        if (leftNegative == rightNegative && resultNegative != leftNegative)
        {
            throw new OverflowException($"Value was either too large or too small for the specified type: {nameof(Int512)}.");
        }

        return result;
    }
}
