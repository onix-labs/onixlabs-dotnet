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

namespace OnixLabs.Numerics;

public readonly partial struct Float256
{
    /// <summary>Computes the truncating remainder of two <see cref="Float256"/> values.</summary>
    /// <param name="left">The dividend.</param>
    /// <param name="right">The divisor.</param>
    /// <returns>Returns the exact remainder <c>left - Truncate(left / right) * right</c>, carrying the sign of <paramref name="left"/>; NaN for invalid combinations.</returns>
    /// <remarks>
    /// The reduction is computed exactly: each step subtracts the largest power-of-two multiple of the divisor that does not exceed the
    /// running remainder, which is a Sterbenz-safe subtraction and therefore incurs no rounding. Unlike a <c>left - Truncate(left / right) * right</c>
    /// formulation, this remains exact even when <c>left / right</c> exceeds the significand precision.
    /// </remarks>
    public static Float256 Remainder(Float256 left, Float256 right)
    {
        if (IsNaN(left) || IsNaN(right)) return NaN;
        if (IsInfinity(left)) return NaN;
        if (IsZero(right)) return NaN;
        if (IsInfinity(right)) return left;
        if (IsZero(left)) return left;

        bool sign = IsNegative(left);
        Float256 absLeft = ClearSign(left);
        Float256 absRight = ClearSign(right);

        Float256 remainder = absLeft < absRight ? absLeft : ReduceModulo(absLeft, absRight);
        return sign ? new Float256(remainder.Bits | SignMask) : remainder;
    }

    /// <inheritdoc cref="Remainder(Float256, Float256)"/>
    public static Float256 operator %(Float256 left, Float256 right) => Remainder(left, right);

    /// <summary>
    /// Returns the specified <see cref="Float256"/> value with its sign bit cleared.
    /// </summary>
    /// <param name="value">The value whose magnitude is required.</param>
    /// <returns>Returns the absolute value of <paramref name="value"/>.</returns>
    internal static Float256 ClearSign(Float256 value) => new(value.Bits & ~SignMask);

    /// <summary>
    /// Computes the exact remainder of <paramref name="absLeft"/> modulo <paramref name="modulus"/>, where both are finite, positive and <paramref name="absLeft"/> is greater than or equal to <paramref name="modulus"/>.
    /// </summary>
    /// <param name="absLeft">The non-negative dividend magnitude.</param>
    /// <param name="modulus">The positive modulus.</param>
    /// <returns>Returns the exact non-negative remainder in the range <c>[0, modulus)</c>.</returns>
    /// <remarks>
    /// Each iteration subtracts the largest power-of-two multiple of <paramref name="modulus"/> not exceeding the running remainder.
    /// Because that multiple lies in <c>(remainder / 2, remainder]</c>, the subtraction satisfies the Sterbenz lemma and is exact.
    /// The number of iterations is bounded by the binary exponent difference between the operands.
    /// </remarks>
    private static Float256 ReduceModulo(Float256 absLeft, Float256 modulus)
    {
        Float256 remainder = absLeft;

        while (remainder >= modulus)
        {
            int shift = ILogB(remainder) - ILogB(modulus);
            Float256 scaledModulus = ScaleB(modulus, shift);
            if (scaledModulus > remainder) scaledModulus = ScaleB(modulus, shift - 1);
            remainder -= scaledModulus;
        }

        return remainder;
    }
}
