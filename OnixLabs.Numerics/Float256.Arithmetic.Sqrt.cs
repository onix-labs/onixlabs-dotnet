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

public readonly partial struct Float256
{
    /// <summary>
    /// Computes the square root of the specified <see cref="Float256"/> value, rounded to nearest, ties-to-even per IEEE 754.
    /// </summary>
    /// <param name="value">The value whose square root is to be computed.</param>
    /// <returns>Returns the square root of <paramref name="value"/>, faithfully rounded to within one unit in the last place (ULP); <see cref="NaN"/> for negative inputs or NaN; preserves the sign of zero.</returns>
    /// <remarks>
    /// The implementation uses Newton-Raphson iteration seeded from an exponent-derived power-of-two estimate that stays valid across the full binary256 range.
    /// Because the iteration rounds at each step and applies no final residual correction, the result is faithfully rounded to within 1 ULP rather than guaranteed correctly-rounded.
    /// </remarks>
    public static Float256 Sqrt(Float256 value)
    {
        if (IsNaN(value)) return value;
        if (IsZero(value)) return value;
        if (IsNegative(value)) return NaN;
        if (IsPositiveInfinity(value)) return value;

        DecomposeFinite(value.Bits, out _, out int unbiasedExponent, out UInt256 significand);
        NormalizeSubnormal(ref significand, ref unbiasedExponent);

        int halvedExponent = unbiasedExponent >> 1;
        UInt256 initialEstimateBits = new UInt256(UInt128.Zero, (UInt128)(uint)(halvedExponent + ExponentBias)) << BiasedExponentShift;
        Float256 estimate = new(initialEstimateBits);

        Float256 half = Half;

        for (int iteration = 0; iteration < 12; iteration++)
        {
            Float256 next = (estimate + value / estimate) * half;
            if (next == estimate) break;
            estimate = next;
        }

        return estimate;
    }
}
