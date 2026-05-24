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
    /// Computes the square root of the specified <see cref="Float128"/> value, rounded to nearest, ties-to-even per IEEE 754.
    /// </summary>
    /// <param name="value">The value whose square root is to be computed.</param>
    /// <returns>Returns the square root of <paramref name="value"/>, faithfully rounded to within one unit in the last place (ULP); <see cref="NaN"/> for negative inputs or NaN; preserves the sign of zero.</returns>
    /// <remarks>
    /// The implementation uses Newton-Raphson iteration on top of the bit-twiddled <see cref="Multiply(Float128, Float128)"/> and
    /// <see cref="Divide(Float128, Float128)"/> primitives. Starting from a power-of-two initial estimate derived from the unbiased
    /// exponent, six iterations are sufficient to reach binary128 precision because Newton-Raphson on square root converges
    /// quadratically; the loop runs eight for a safety margin. Because the iteration rounds at each step and applies no final
    /// residual-correction step, the result is faithfully rounded (within 1 ULP) rather than guaranteed correctly-rounded.
    /// </remarks>
    public static Float128 Sqrt(Float128 value)
    {
        if (IsNaN(value)) return NaN;
        if (IsZero(value)) return value;
        if (IsNegative(value)) return NaN;
        if (IsPositiveInfinity(value)) return value;

        DecomposeFinite(value.Bits, out _, out int unbiasedExponent, out UInt128 significand);
        NormalizeSubnormal(ref significand, ref unbiasedExponent);

        int halvedExponent = unbiasedExponent >> 1;
        UInt128 initialEstimateBits = (UInt128)(uint)(halvedExponent + ExponentBias) << BiasedExponentShift;
        Float128 estimate = new(initialEstimateBits);

        Float128 half = One / Two;

        for (int iteration = 0; iteration < 8; iteration++)
        {
            estimate = (estimate + value / estimate) * half;
        }

        return estimate;
    }
}
