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
    /// <summary>Returns the integral base-2 logarithm of the specified <see cref="Float256"/> value, returning the unbiased exponent.</summary>
    /// <param name="value">The value for which to compute the integral base-2 logarithm.</param>
    /// <returns>Returns the unbiased binary exponent of <paramref name="value"/>; <see cref="int.MaxValue"/> for NaN or infinity; <see cref="int.MinValue"/> for zero.</returns>
    public static int ILogB(Float256 value)
    {
        if (IsNaN(value)) return int.MaxValue;
        if (IsInfinity(value)) return int.MaxValue;
        if (IsZero(value)) return int.MinValue;

        uint biasedExponent = ExtractBiasedExponent(value.bits);
        if (biasedExponent != 0u) return (int)biasedExponent - ExponentBias;

        UInt256 trailing = ExtractTrailingSignificand(value.bits);
        int leadingBitPosition = 255 - (int)UInt256.LeadingZeroCount(trailing);
        return leadingBitPosition + MinNormalUnbiasedExponent - TrailingSignificandBits;
    }

    /// <summary>Returns the specified <see cref="Float256"/> value scaled by an integer power of two.</summary>
    /// <param name="value">The value to scale.</param>
    /// <param name="n">The integer power of two by which to scale.</param>
    /// <returns>Returns <paramref name="value"/> multiplied by <c>2^n</c>.</returns>
    public static Float256 ScaleB(Float256 value, int n)
    {
        if (IsNaN(value) || IsInfinity(value) || IsZero(value)) return value;

        DecomposeFinite(value.bits, out bool sign, out int unbiasedExponent, out UInt256 significand);
        NormalizeSubnormal(ref significand, ref unbiasedExponent);

        long scaledExponentWide = (long)unbiasedExponent + n;

        if (scaledExponentWide > MaxFiniteUnbiasedExponent)
        {
            return sign ? NegativeInfinity : PositiveInfinity;
        }

        if (scaledExponentWide < MinNormalUnbiasedExponent - (TrailingSignificandBits + 2))
        {
            return sign ? NegativeZero : Zero;
        }

        return RoundToNearestEven(sign, (int)scaledExponentWide, significand, false, false);
    }
}
