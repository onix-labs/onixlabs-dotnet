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
using System.Numerics;

namespace OnixLabs.Numerics;

public readonly partial struct UInt256
{
    /// <summary>Computes the sum of two <see cref="UInt256"/> values, wrapping on overflow.</summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns>Returns the wrapping sum of <paramref name="left"/> and <paramref name="right"/>.</returns>
    public static UInt256 operator +(UInt256 left, UInt256 right)
    {
        UInt128 newLower = left.lower + right.lower;
        UInt128 carry = newLower < left.lower ? UInt128.One : UInt128.Zero;
        UInt128 newUpper = left.upper + right.upper + carry;
        return new UInt256(newUpper, newLower);
    }

    /// <summary>Computes the sum of two <see cref="UInt256"/> values, throwing on overflow.</summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns>Returns the sum of <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <exception cref="OverflowException">Thrown when the sum exceeds <see cref="MaxValue"/>.</exception>
    public static UInt256 operator checked +(UInt256 left, UInt256 right)
    {
        UInt128 newLower = left.lower + right.lower;
        UInt128 lowerCarry = newLower < left.lower ? UInt128.One : UInt128.Zero;
        UInt128 upperSum = checked(left.upper + right.upper);
        UInt128 newUpper = checked(upperSum + lowerCarry);
        return new UInt256(newUpper, newLower);
    }
}
