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
    /// <summary>Computes the quotient of two <see cref="Int256"/> values.</summary>
    /// <param name="left">The dividend.</param>
    /// <param name="right">The divisor.</param>
    /// <returns>Returns the quotient of <paramref name="left"/> divided by <paramref name="right"/>, truncated toward zero.</returns>
    /// <exception cref="DivideByZeroException">Thrown when <paramref name="right"/> is zero.</exception>
    /// <exception cref="OverflowException">Thrown when dividing <see cref="MinValue"/> by <see cref="NegativeOne"/>.</exception>
    public static Int256 operator /(Int256 left, Int256 right)
    {
        if (IsZero(right)) throw new DivideByZeroException();
        if (left == MinValue && right == NegativeOne) throw new OverflowException($"Value was either too large or too small for the specified type: {nameof(Int256)}.");

        bool resultNegative = IsNegative(left) ^ IsNegative(right);
        UInt256 absLeft = AbsToUnsigned(left);
        UInt256 absRight = AbsToUnsigned(right);
        UInt256 quotient = absLeft / absRight;
        Int256 signed = ReinterpretAsSigned(quotient);
        return resultNegative ? -signed : signed;
    }

    /// <summary>Computes the quotient and remainder of two <see cref="Int256"/> values.</summary>
    /// <param name="left">The dividend.</param>
    /// <param name="right">The divisor.</param>
    /// <returns>Returns a tuple containing the quotient and the remainder.</returns>
    /// <exception cref="DivideByZeroException">Thrown when <paramref name="right"/> is zero.</exception>
    /// <exception cref="OverflowException">Thrown when dividing <see cref="MinValue"/> by <see cref="NegativeOne"/>.</exception>
    public static (Int256 Quotient, Int256 Remainder) DivRem(Int256 left, Int256 right)
    {
        if (IsZero(right)) throw new DivideByZeroException();
        if (left == MinValue && right == NegativeOne) throw new OverflowException($"Value was either too large or too small for the specified type: {nameof(Int256)}.");

        bool quotientNegative = IsNegative(left) ^ IsNegative(right);
        bool remainderNegative = IsNegative(left);
        UInt256 absLeft = AbsToUnsigned(left);
        UInt256 absRight = AbsToUnsigned(right);
        (UInt256 absQuotient, UInt256 absRemainder) = UInt256.DivRem(absLeft, absRight);

        Int256 quotient = ReinterpretAsSigned(absQuotient);
        Int256 remainder = ReinterpretAsSigned(absRemainder);

        if (quotientNegative) quotient = -quotient;
        if (remainderNegative) remainder = -remainder;

        return (quotient, remainder);
    }
}
