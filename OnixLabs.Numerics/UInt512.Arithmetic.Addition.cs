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

public readonly partial struct UInt512
{
    /// <summary>Computes the sum of two <see cref="UInt512"/> values, wrapping on overflow.</summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns>Returns the wrapping sum of <paramref name="left"/> and <paramref name="right"/>.</returns>
    public static UInt512 operator +(UInt512 left, UInt512 right)
    {
        UInt256 newLower = left.LowerBits + right.LowerBits;
        UInt256 carry = newLower < left.LowerBits ? UInt256.One : UInt256.Zero;
        UInt256 newUpper = left.UpperBits + right.UpperBits + carry;
        return new UInt512(newUpper, newLower);
    }

    /// <summary>Computes the sum of two <see cref="UInt512"/> values, throwing on overflow.</summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns>Returns the sum of <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <exception cref="OverflowException">Thrown when the sum exceeds <see cref="MaxValue"/>.</exception>
    public static UInt512 operator checked +(UInt512 left, UInt512 right)
    {
        UInt256 newLower = left.LowerBits + right.LowerBits;
        UInt256 lowerCarry = newLower < left.LowerBits ? UInt256.One : UInt256.Zero;
        UInt256 upperSum = checked(left.UpperBits + right.UpperBits);
        UInt256 newUpper = checked(upperSum + lowerCarry);
        return new UInt512(newUpper, newLower);
    }
}
