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
    /// <summary>Computes the product of two <see cref="Int256"/> values, wrapping on overflow.</summary>
    /// <param name="left">The left multiplicand.</param>
    /// <param name="right">The right multiplicand.</param>
    /// <returns>Returns the wrapping product.</returns>
    public static Int256 operator *(Int256 left, Int256 right)
    {
        UInt256 unsignedLeft = ReinterpretAsUnsigned(left);
        UInt256 unsignedRight = ReinterpretAsUnsigned(right);
        UInt256 product = unsignedLeft * unsignedRight;
        return ReinterpretAsSigned(product);
    }

    /// <summary>Computes the product of two <see cref="Int256"/> values, throwing on overflow.</summary>
    /// <param name="left">The left multiplicand.</param>
    /// <param name="right">The right multiplicand.</param>
    /// <returns>Returns the product.</returns>
    /// <exception cref="OverflowException">Thrown when the product overflows the range of <see cref="Int256"/>.</exception>
    public static Int256 operator checked *(Int256 left, Int256 right)
    {
        bool resultNegative = IsNegative(left) ^ IsNegative(right);
        UInt256 absLeft = AbsToUnsigned(left);
        UInt256 absRight = AbsToUnsigned(right);
        UInt256 high = UInt256.BigMul(absLeft, absRight, out UInt256 low);

        if (high != UInt256.Zero) throw new OverflowException($"Value was either too large or too small for the specified type: {nameof(Int256)}.");

        UInt256 maxPositiveMagnitude = new(~SignBitMask, UInt128.MaxValue);
        UInt256 maxNegativeMagnitude = new(SignBitMask, UInt128.Zero);

        if (resultNegative)
        {
            if (low > maxNegativeMagnitude) throw new OverflowException($"Value was either too large or too small for the specified type: {nameof(Int256)}.");
            return ReinterpretAsSigned(UInt256.Zero - low);
        }

        if (low > maxPositiveMagnitude) throw new OverflowException($"Value was either too large or too small for the specified type: {nameof(Int256)}.");
        return ReinterpretAsSigned(low);
    }

    /// <summary>
    /// Reinterprets the bit pattern of the specified <see cref="Int256"/> value as a <see cref="UInt256"/> value without changing the underlying bytes.
    /// </summary>
    /// <param name="value">The <see cref="Int256"/> value to reinterpret.</param>
    /// <returns>Returns the <see cref="UInt256"/> value sharing the same bit pattern as <paramref name="value"/>.</returns>
    internal static UInt256 ReinterpretAsUnsigned(Int256 value) => new(value.Upper, value.Lower);

    /// <summary>
    /// Reinterprets the bit pattern of the specified <see cref="UInt256"/> value as an <see cref="Int256"/> value without changing the underlying bytes.
    /// </summary>
    /// <param name="value">The <see cref="UInt256"/> value to reinterpret.</param>
    /// <returns>Returns the <see cref="Int256"/> value sharing the same bit pattern as <paramref name="value"/>.</returns>
    internal static Int256 ReinterpretAsSigned(UInt256 value) => new(value.Upper, value.Lower);

    /// <summary>
    /// Computes the absolute magnitude of the specified <see cref="Int256"/> value as a <see cref="UInt256"/> value, so that <see cref="Int256.MinValue"/> can be represented without overflow.
    /// </summary>
    /// <param name="value">The <see cref="Int256"/> value whose absolute magnitude is required.</param>
    /// <returns>Returns the unsigned magnitude of <paramref name="value"/>.</returns>
    internal static UInt256 AbsToUnsigned(Int256 value)
    {
        if (IsNegative(value))
        {
            UInt256 unsigned = ReinterpretAsUnsigned(value);
            return UInt256.Zero - unsigned;
        }
        return ReinterpretAsUnsigned(value);
    }
}
