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
    /// <summary>Computes the quotient of two <see cref="UInt512"/> values.</summary>
    /// <param name="left">The dividend.</param>
    /// <param name="right">The divisor.</param>
    /// <returns>Returns the quotient of <paramref name="left"/> divided by <paramref name="right"/>.</returns>
    /// <exception cref="DivideByZeroException">Thrown when <paramref name="right"/> is zero.</exception>
    public static UInt512 operator /(UInt512 left, UInt512 right)
    {
        (UInt512 quotient, _) = DivRem(left, right);
        return quotient;
    }

    /// <summary>Computes the quotient and remainder of two <see cref="UInt512"/> values.</summary>
    /// <param name="left">The dividend.</param>
    /// <param name="right">The divisor.</param>
    /// <returns>Returns a tuple containing the quotient and the remainder.</returns>
    /// <exception cref="DivideByZeroException">Thrown when <paramref name="right"/> is zero.</exception>
    public static (UInt512 Quotient, UInt512 Remainder) DivRem(UInt512 left, UInt512 right)
    {
        if (IsZero(right)) throw new DivideByZeroException();
        if (left < right) return (Zero, left);
        if (left == right) return (One, Zero);

        if (UInt256.IsZero(right.UpperBits))
        {
            UInt256 divisor = right.LowerBits;
            if (UInt256.IsZero(left.UpperBits))
            {
                (UInt256 quotientSmall, UInt256 remainderSmall) = UInt256.DivRem(left.LowerBits, divisor);
                return (new UInt512(UInt256.Zero, quotientSmall), new UInt512(UInt256.Zero, remainderSmall));
            }

            UInt256 quotientLow = DivRemBy256(left, divisor, out UInt256 remainder256);
            UInt256 quotientHigh = left.UpperBits / divisor;
            return (new UInt512(quotientHigh, quotientLow), new UInt512(UInt256.Zero, remainder256));
        }

        UInt512 working = Zero;
        UInt512 quotient = Zero;

        for (int bitIndex = BitWidth - 1; bitIndex >= 0; bitIndex--)
        {
            UInt512 nextBit = (left >>> bitIndex) & One;
            working = (working << 1) | nextBit;

            if (working >= right)
            {
                working = working - right;
                quotient = quotient | (One << bitIndex);
            }
        }

        return (quotient, working);
    }

    /// <summary>Divides a 512-bit dividend by a 256-bit divisor and returns the 256-bit quotient and remainder.</summary>
    /// <param name="dividend">The 512-bit dividend.</param>
    /// <param name="divisor">The 256-bit divisor.</param>
    /// <param name="remainder">When this method returns, contains the 256-bit remainder.</param>
    /// <returns>Returns the 256-bit quotient.</returns>
    /// <exception cref="DivideByZeroException">Thrown when <paramref name="divisor"/> is zero.</exception>
    /// <remarks>The caller is responsible for ensuring the quotient fits in 256 bits (i.e. <c>dividend.Upper &lt; divisor</c>).</remarks>
    public static UInt256 DivRemBy256(UInt512 dividend, UInt256 divisor, out UInt256 remainder)
    {
        if (UInt256.IsZero(divisor)) throw new DivideByZeroException();

        UInt256 quotientLow = UInt256.Zero;
        UInt256 working = UInt256.Zero;
        UInt256 numHigh = dividend.UpperBits;
        UInt256 numLow = dividend.LowerBits;

        for (int bitIndex = HalfBitWidth - 1; bitIndex >= 0; bitIndex--)
        {
            UInt256 nextBit = (numHigh >> bitIndex) & UInt256.One;
            // Capture the top bit before shifting so values whose true magnitude is in [2^256, 2*divisor)
            // — which can happen when divisor is close to 2^256 — are still recognised as >= divisor.
            bool workingOverflow = (working >> (HalfBitWidth - 1)) != UInt256.Zero;
            working = (working << 1) | nextBit;

            if (workingOverflow || working >= divisor) working -= divisor;
        }

        for (int bitIndex = HalfBitWidth - 1; bitIndex >= 0; bitIndex--)
        {
            UInt256 nextBit = (numLow >> bitIndex) & UInt256.One;
            bool workingOverflow = (working >> (HalfBitWidth - 1)) != UInt256.Zero;
            working = (working << 1) | nextBit;

            if (workingOverflow || working >= divisor)
            {
                working -= divisor;
                quotientLow |= UInt256.One << bitIndex;
            }
        }

        remainder = working;
        return quotientLow;
    }
}
