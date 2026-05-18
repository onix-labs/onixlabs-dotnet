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
    /// <summary>Shifts an <see cref="Int512"/> value left by the specified amount.</summary>
    /// <param name="value">The value to shift.</param>
    /// <param name="shiftAmount">The number of bits to shift by.</param>
    /// <returns>Returns <paramref name="value"/> shifted left by <paramref name="shiftAmount"/> bits.</returns>
    public static Int512 operator <<(Int512 value, int shiftAmount)
    {
        shiftAmount &= BitWidth - 1;
        if (shiftAmount == 0) return value;
        if (shiftAmount >= HalfBitWidth)
        {
            return new Int512(value.LowerBits << (shiftAmount - HalfBitWidth), UInt256.Zero);
        }

        UInt256 newUpper = (value.UpperBits << shiftAmount) | (value.LowerBits >> (HalfBitWidth - shiftAmount));
        UInt256 newLower = value.LowerBits << shiftAmount;
        return new Int512(newUpper, newLower);
    }

    /// <summary>Shifts an <see cref="Int512"/> value right by the specified amount, preserving the sign (arithmetic shift).</summary>
    /// <param name="value">The value to shift.</param>
    /// <param name="shiftAmount">The number of bits to shift by.</param>
    /// <returns>Returns <paramref name="value"/> shifted right by <paramref name="shiftAmount"/> bits, sign-extended.</returns>
    public static Int512 operator >>(Int512 value, int shiftAmount)
    {
        shiftAmount &= BitWidth - 1;
        if (shiftAmount == 0) return value;

        UInt256 signFill = IsNegative(value) ? UInt256.MaxValue : UInt256.Zero;

        if (shiftAmount >= HalfBitWidth)
        {
            int extraShift = shiftAmount - HalfBitWidth;
            UInt256 newLower = extraShift == 0
                ? value.UpperBits
                : (value.UpperBits >> extraShift) | (signFill << (HalfBitWidth - extraShift));
            return new Int512(signFill, newLower);
        }

        UInt256 newUpperArith = (value.UpperBits >> shiftAmount) | (signFill << (HalfBitWidth - shiftAmount));
        UInt256 newLowerArith = (value.LowerBits >> shiftAmount) | (value.UpperBits << (HalfBitWidth - shiftAmount));
        return new Int512(newUpperArith, newLowerArith);
    }

    /// <summary>Shifts an <see cref="Int512"/> value right (logical) by the specified amount, filling with zero.</summary>
    /// <param name="value">The value to shift.</param>
    /// <param name="shiftAmount">The number of bits to shift by.</param>
    /// <returns>Returns <paramref name="value"/> shifted right by <paramref name="shiftAmount"/> bits, zero-filled.</returns>
    public static Int512 operator >>>(Int512 value, int shiftAmount)
    {
        shiftAmount &= BitWidth - 1;
        if (shiftAmount == 0) return value;
        if (shiftAmount >= HalfBitWidth)
        {
            return new Int512(UInt256.Zero, value.UpperBits >> (shiftAmount - HalfBitWidth));
        }

        UInt256 newUpper = value.UpperBits >> shiftAmount;
        UInt256 newLower = (value.LowerBits >> shiftAmount) | (value.UpperBits << (HalfBitWidth - shiftAmount));
        return new Int512(newUpper, newLower);
    }
}
