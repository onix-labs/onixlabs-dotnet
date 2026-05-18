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
    /// <summary>Shifts an <see cref="Int256"/> value left by the specified amount.</summary>
    /// <param name="value">The value to shift.</param>
    /// <param name="shiftAmount">The number of bits to shift by.</param>
    /// <returns>Returns <paramref name="value"/> shifted left by <paramref name="shiftAmount"/> bits.</returns>
    public static Int256 operator <<(Int256 value, int shiftAmount)
    {
        shiftAmount &= BitWidth - 1;
        if (shiftAmount == 0) return value;
        if (shiftAmount >= HalfBitWidth)
        {
            return new Int256(value.lower << (shiftAmount - HalfBitWidth), UInt128.Zero);
        }

        UInt128 newUpper = (value.upper << shiftAmount) | (value.lower >> (HalfBitWidth - shiftAmount));
        UInt128 newLower = value.lower << shiftAmount;
        return new Int256(newUpper, newLower);
    }

    /// <summary>Shifts an <see cref="Int256"/> value right by the specified amount, preserving the sign (arithmetic shift).</summary>
    /// <param name="value">The value to shift.</param>
    /// <param name="shiftAmount">The number of bits to shift by.</param>
    /// <returns>Returns <paramref name="value"/> shifted right by <paramref name="shiftAmount"/> bits, sign-extended.</returns>
    public static Int256 operator >>(Int256 value, int shiftAmount)
    {
        shiftAmount &= BitWidth - 1;
        if (shiftAmount == 0) return value;

        UInt128 signFill = IsNegative(value) ? UInt128.MaxValue : UInt128.Zero;

        if (shiftAmount >= HalfBitWidth)
        {
            int extraShift = shiftAmount - HalfBitWidth;
            UInt128 newLower = extraShift == 0
                ? value.upper
                : (value.upper >> extraShift) | (signFill << (HalfBitWidth - extraShift));
            return new Int256(signFill, newLower);
        }

        UInt128 newUpperArith = (value.upper >> shiftAmount) | (signFill << (HalfBitWidth - shiftAmount));
        UInt128 newLowerArith = (value.lower >> shiftAmount) | (value.upper << (HalfBitWidth - shiftAmount));
        return new Int256(newUpperArith, newLowerArith);
    }

    /// <summary>Shifts an <see cref="Int256"/> value right (logical) by the specified amount, filling with zero.</summary>
    /// <param name="value">The value to shift.</param>
    /// <param name="shiftAmount">The number of bits to shift by.</param>
    /// <returns>Returns <paramref name="value"/> shifted right by <paramref name="shiftAmount"/> bits, zero-filled.</returns>
    public static Int256 operator >>>(Int256 value, int shiftAmount)
    {
        shiftAmount &= BitWidth - 1;
        if (shiftAmount == 0) return value;
        if (shiftAmount >= HalfBitWidth)
        {
            return new Int256(UInt128.Zero, value.upper >> (shiftAmount - HalfBitWidth));
        }

        UInt128 newUpper = value.upper >> shiftAmount;
        UInt128 newLower = (value.lower >> shiftAmount) | (value.upper << (HalfBitWidth - shiftAmount));
        return new Int256(newUpper, newLower);
    }
}
