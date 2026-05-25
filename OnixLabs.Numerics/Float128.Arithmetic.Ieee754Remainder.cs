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

public readonly partial struct Float128
{
    /// <summary>
    /// Computes the IEEE 754 remainder of the specified <see cref="Float128"/> values, using round-to-nearest-even on the quotient.
    /// </summary>
    /// <param name="left">The dividend.</param>
    /// <param name="right">The divisor.</param>
    /// <returns>Returns the exact value of <paramref name="left"/> minus <c>n * right</c>, where <c>n</c> is <c>left / right</c> rounded to the nearest integer with ties to even; <see cref="NaN"/> for invalid combinations such as infinity divided by anything, division by zero, or any operand being NaN.</returns>
    /// <remarks>
    /// The exact truncating remainder and the parity of the truncating quotient are obtained from a single Sterbenz-safe reduction of
    /// <paramref name="left"/> modulo <c>2 * |right|</c>, then adjusted by one modulus when the remainder exceeds half the divisor (ties to even).
    /// Every arithmetic step is exact, so the IEEE 754 remainder is returned without rounding error even for very large quotients.
    /// </remarks>
    public static Float128 Ieee754Remainder(Float128 left, Float128 right)
    {
        if (IsNaN(left) || IsNaN(right)) return NaN;
        if (IsInfinity(left)) return NaN;
        if (IsZero(right)) return NaN;
        if (IsInfinity(right)) return left;
        if (IsZero(left)) return left;

        bool sign = IsNegative(left);
        Float128 absLeft = ClearSign(left);
        Float128 absRight = ClearSign(right);

        Float128 twoModulus = ScaleB(absRight, 1);
        Float128 doubleRemainder = absLeft < twoModulus ? absLeft : ReduceModulo(absLeft, twoModulus);

        bool quotientOdd = doubleRemainder >= absRight;
        Float128 truncatedRemainder = quotientOdd ? doubleRemainder - absRight : doubleRemainder;

        Float128 twiceRemainder = ScaleB(truncatedRemainder, 1);
        bool subtractModulus = twiceRemainder > absRight || (twiceRemainder == absRight && quotientOdd);

        if (!subtractModulus)
        {
            return sign ? new Float128(truncatedRemainder.Bits | SignMask) : truncatedRemainder;
        }

        Float128 adjusted = absRight - truncatedRemainder;
        return sign ? adjusted : new Float128(adjusted.Bits | SignMask);
    }
}
