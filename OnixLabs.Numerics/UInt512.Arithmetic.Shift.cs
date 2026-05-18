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
    /// <summary>Shifts a <see cref="UInt512"/> value left by the specified amount.</summary>
    /// <param name="value">The value to shift.</param>
    /// <param name="shiftAmount">The number of bits to shift by.</param>
    /// <returns>Returns <paramref name="value"/> shifted left by <paramref name="shiftAmount"/> bits.</returns>
    public static UInt512 operator <<(UInt512 value, int shiftAmount)
    {
        shiftAmount &= BitWidth - 1;
        if (shiftAmount == 0) return value;
        if (shiftAmount >= HalfBitWidth)
        {
            return new UInt512(value.Lower << (shiftAmount - HalfBitWidth), UInt256.Zero);
        }

        UInt256 newUpper = (value.Upper << shiftAmount) | (value.Lower >> (HalfBitWidth - shiftAmount));
        UInt256 newLower = value.Lower << shiftAmount;
        return new UInt512(newUpper, newLower);
    }

    /// <summary>Shifts a <see cref="UInt512"/> value right by the specified amount.</summary>
    /// <param name="value">The value to shift.</param>
    /// <param name="shiftAmount">The number of bits to shift by.</param>
    /// <returns>Returns <paramref name="value"/> shifted right by <paramref name="shiftAmount"/> bits.</returns>
    public static UInt512 operator >>(UInt512 value, int shiftAmount) => value >>> shiftAmount;

    /// <summary>Shifts a <see cref="UInt512"/> value right (unsigned) by the specified amount.</summary>
    /// <param name="value">The value to shift.</param>
    /// <param name="shiftAmount">The number of bits to shift by.</param>
    /// <returns>Returns <paramref name="value"/> shifted right by <paramref name="shiftAmount"/> bits.</returns>
    public static UInt512 operator >>>(UInt512 value, int shiftAmount)
    {
        shiftAmount &= BitWidth - 1;
        if (shiftAmount == 0) return value;
        if (shiftAmount >= HalfBitWidth)
        {
            return new UInt512(UInt256.Zero, value.Upper >> (shiftAmount - HalfBitWidth));
        }

        UInt256 newUpper = value.Upper >> shiftAmount;
        UInt256 newLower = (value.Lower >> shiftAmount) | (value.Upper << (HalfBitWidth - shiftAmount));
        return new UInt512(newUpper, newLower);
    }
}
