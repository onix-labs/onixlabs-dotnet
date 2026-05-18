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
    /// <summary>Computes the product of two <see cref="Int512"/> values, wrapping on overflow.</summary>
    /// <param name="left">The left multiplicand.</param>
    /// <param name="right">The right multiplicand.</param>
    /// <returns>Returns the wrapping product.</returns>
    public static Int512 operator *(Int512 left, Int512 right)
    {
        UInt512 unsignedLeft = ReinterpretAsUnsigned(left);
        UInt512 unsignedRight = ReinterpretAsUnsigned(right);
        UInt512 product = unsignedLeft * unsignedRight;
        return ReinterpretAsSigned(product);
    }

    /// <summary>Computes the product of two <see cref="Int512"/> values, throwing on overflow.</summary>
    /// <param name="left">The left multiplicand.</param>
    /// <param name="right">The right multiplicand.</param>
    /// <returns>Returns the product.</returns>
    /// <exception cref="OverflowException">Thrown when the product overflows the range of <see cref="Int512"/>.</exception>
    public static Int512 operator checked *(Int512 left, Int512 right)
    {
        bool resultNegative = IsNegative(left) ^ IsNegative(right);
        UInt512 absLeft = AbsToUnsigned(left);
        UInt512 absRight = AbsToUnsigned(right);
        UInt512 high = UInt512.BigMul(absLeft, absRight, out UInt512 low);

        if (high != UInt512.Zero) throw new OverflowException($"Value was either too large or too small for the specified type: {nameof(Int512)}.");

        UInt512 maxPositiveMagnitude = new(~SignBitMask, UInt256.MaxValue);
        UInt512 maxNegativeMagnitude = new(SignBitMask, UInt256.Zero);

        if (resultNegative)
        {
            if (low > maxNegativeMagnitude) throw new OverflowException($"Value was either too large or too small for the specified type: {nameof(Int512)}.");
            return ReinterpretAsSigned(UInt512.Zero - low);
        }

        if (low > maxPositiveMagnitude) throw new OverflowException($"Value was either too large or too small for the specified type: {nameof(Int512)}.");
        return ReinterpretAsSigned(low);
    }

    /// <summary>
    /// Reinterprets the bit pattern of an <see cref="Int512"/> value as a <see cref="UInt512"/> value.
    /// </summary>
    /// <param name="value">The signed value whose bits should be reinterpreted.</param>
    /// <returns>Returns the <see cref="UInt512"/> value with the same bit pattern as <paramref name="value"/>.</returns>
    internal static UInt512 ReinterpretAsUnsigned(Int512 value) => new(value.UpperBits, value.LowerBits);

    /// <summary>
    /// Reinterprets the bit pattern of a <see cref="UInt512"/> value as an <see cref="Int512"/> value.
    /// </summary>
    /// <param name="value">The unsigned value whose bits should be reinterpreted.</param>
    /// <returns>Returns the <see cref="Int512"/> value with the same bit pattern as <paramref name="value"/>.</returns>
    internal static Int512 ReinterpretAsSigned(UInt512 value) => new(value.UpperBits, value.LowerBits);

    /// <summary>
    /// Computes the unsigned magnitude of an <see cref="Int512"/> value by negating the bit pattern when negative.
    /// </summary>
    /// <param name="value">The signed value whose absolute magnitude is required.</param>
    /// <returns>Returns the absolute magnitude of <paramref name="value"/> as a <see cref="UInt512"/>.</returns>
    internal static UInt512 AbsToUnsigned(Int512 value)
    {
        if (IsNegative(value))
        {
            UInt512 unsigned = ReinterpretAsUnsigned(value);
            return UInt512.Zero - unsigned;
        }
        return ReinterpretAsUnsigned(value);
    }
}
