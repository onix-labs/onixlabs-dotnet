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
    /// <summary>Computes the quotient of two <see cref="UInt256"/> values.</summary>
    /// <param name="left">The dividend.</param>
    /// <param name="right">The divisor.</param>
    /// <returns>Returns the quotient of <paramref name="left"/> divided by <paramref name="right"/>.</returns>
    /// <exception cref="DivideByZeroException">Thrown when <paramref name="right"/> is zero.</exception>
    public static UInt256 operator /(UInt256 left, UInt256 right)
    {
        (UInt256 quotient, _) = DivRem(left, right);
        return quotient;
    }

    /// <summary>Computes the quotient and remainder of two <see cref="UInt256"/> values.</summary>
    /// <param name="left">The dividend.</param>
    /// <param name="right">The divisor.</param>
    /// <returns>Returns a tuple containing the quotient and the remainder.</returns>
    /// <exception cref="DivideByZeroException">Thrown when <paramref name="right"/> is zero.</exception>
    public static (UInt256 Quotient, UInt256 Remainder) DivRem(UInt256 left, UInt256 right)
    {
        if (IsZero(right)) throw new DivideByZeroException();
        if (left < right) return (Zero, left);
        if (left == right) return (One, Zero);

        if (right.Upper == UInt128.Zero)
        {
            UInt128 divisor = right.Lower;
            if (left.Upper == UInt128.Zero)
            {
                UInt128 quotientSmall = left.Lower / divisor;
                UInt128 remainderSmall = left.Lower % divisor;
                return (new UInt256(UInt128.Zero, quotientSmall), new UInt256(UInt128.Zero, remainderSmall));
            }

            UInt128 quotientLow = DivRemBy128(left, divisor, out UInt128 remainder128);
            UInt128 quotientHigh = left.Upper / divisor;
            return (new UInt256(quotientHigh, quotientLow), new UInt256(UInt128.Zero, remainder128));
        }

        UInt256 working = Zero;
        UInt256 quotient = Zero;

        for (int bitIndex = BitWidth - 1; bitIndex >= 0; bitIndex--)
        {
            UInt256 nextBit = (left >>> bitIndex) & One;
            working = (working << 1) | nextBit;

            if (working >= right)
            {
                working = working - right;
                quotient = quotient | (One << bitIndex);
            }
        }

        return (quotient, working);
    }

    /// <summary>Divides a 256-bit dividend by a 128-bit divisor and returns the 128-bit quotient and remainder.</summary>
    /// <param name="dividend">The 256-bit dividend.</param>
    /// <param name="divisor">The 128-bit divisor.</param>
    /// <param name="remainder">When this method returns, contains the 128-bit remainder.</param>
    /// <returns>Returns the 128-bit quotient.</returns>
    /// <exception cref="DivideByZeroException">Thrown when <paramref name="divisor"/> is zero.</exception>
    /// <remarks>The caller is responsible for ensuring the quotient fits in 128 bits (i.e. <c>dividend.Upper &lt; divisor</c>).</remarks>
    public static UInt128 DivRemBy128(UInt256 dividend, UInt128 divisor, out UInt128 remainder)
    {
        if (divisor == UInt128.Zero) throw new DivideByZeroException();

        UInt128 quotientLow = UInt128.Zero;
        UInt128 working = UInt128.Zero;
        UInt128 numHigh = dividend.Upper;
        UInt128 numLow = dividend.Lower;

        for (int bitIndex = HalfBitWidth - 1; bitIndex >= 0; bitIndex--)
        {
            UInt128 nextBit = (numHigh >> bitIndex) & UInt128.One;
            working = (working << 1) | nextBit;

            if (working >= divisor) working -= divisor;
        }

        for (int bitIndex = HalfBitWidth - 1; bitIndex >= 0; bitIndex--)
        {
            UInt128 nextBit = (numLow >> bitIndex) & UInt128.One;
            working = (working << 1) | nextBit;

            if (working >= divisor)
            {
                working -= divisor;
                quotientLow |= UInt128.One << bitIndex;
            }
        }

        remainder = working;
        return quotientLow;
    }
}
