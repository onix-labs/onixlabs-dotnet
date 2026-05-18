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
    /// <summary>Returns the number of leading zero bits in the specified <see cref="Int512"/> value.</summary>
    /// <param name="value">The value whose leading zero count is to be computed.</param>
    /// <returns>Returns the number of leading zero bits.</returns>
    public static Int512 LeadingZeroCount(Int512 value)
    {
        if (!UInt256.IsZero(value.upper)) return new Int512(UInt256.Zero, UInt256.LeadingZeroCount(value.upper));
        return new Int512(UInt256.Zero, (UInt256)HalfBitWidth + UInt256.LeadingZeroCount(value.lower));
    }

    /// <summary>Returns the number of trailing zero bits in the specified <see cref="Int512"/> value.</summary>
    /// <param name="value">The value whose trailing zero count is to be computed.</param>
    /// <returns>Returns the number of trailing zero bits.</returns>
    public static Int512 TrailingZeroCount(Int512 value)
    {
        if (!UInt256.IsZero(value.lower)) return new Int512(UInt256.Zero, UInt256.TrailingZeroCount(value.lower));
        if (!UInt256.IsZero(value.upper)) return new Int512(UInt256.Zero, (UInt256)HalfBitWidth + UInt256.TrailingZeroCount(value.upper));
        return new Int512(UInt256.Zero, (UInt256)BitWidth);
    }

    /// <summary>Returns the number of bits set to one in the specified <see cref="Int512"/> value.</summary>
    /// <param name="value">The value whose population count is to be computed.</param>
    /// <returns>Returns the number of bits set to one.</returns>
    public static Int512 PopCount(Int512 value) => new(UInt256.Zero, UInt256.PopCount(value.upper) + UInt256.PopCount(value.lower));

    /// <summary>Returns the base-2 logarithm of the specified <see cref="Int512"/> value, truncated to an integer.</summary>
    /// <param name="value">The value whose base-2 logarithm is to be computed.</param>
    /// <returns>Returns the truncated base-2 logarithm; zero for an input of zero.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="value"/> is negative.</exception>
    public static Int512 Log2(Int512 value)
    {
        if (IsNegative(value)) throw new ArgumentOutOfRangeException(nameof(value), "Log2 is undefined for negative values.");
        if (IsZero(value)) return Zero;
        if (!UInt256.IsZero(value.upper)) return new Int512(UInt256.Zero, (UInt256)HalfBitWidth + UInt256.Log2(value.upper));
        return new Int512(UInt256.Zero, UInt256.Log2(value.lower));
    }

    /// <summary>Rotates the specified <see cref="Int512"/> value left by the specified number of bits.</summary>
    /// <param name="value">The value to rotate.</param>
    /// <param name="rotateAmount">The number of bits to rotate by.</param>
    /// <returns>Returns <paramref name="value"/> rotated left by <paramref name="rotateAmount"/> bits.</returns>
    public static Int512 RotateLeft(Int512 value, int rotateAmount)
    {
        rotateAmount &= BitWidth - 1;
        if (rotateAmount == 0) return value;
        return (value << rotateAmount) | (value >>> (BitWidth - rotateAmount));
    }

    /// <summary>Rotates the specified <see cref="Int512"/> value right by the specified number of bits.</summary>
    /// <param name="value">The value to rotate.</param>
    /// <param name="rotateAmount">The number of bits to rotate by.</param>
    /// <returns>Returns <paramref name="value"/> rotated right by <paramref name="rotateAmount"/> bits.</returns>
    public static Int512 RotateRight(Int512 value, int rotateAmount)
    {
        rotateAmount &= BitWidth - 1;
        if (rotateAmount == 0) return value;
        return (value >>> rotateAmount) | (value << (BitWidth - rotateAmount));
    }
}
