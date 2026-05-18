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
    /// Computes the integral component of the specified <see cref="Float128"/> value, rounding toward zero.
    /// </summary>
    /// <param name="value">The <see cref="Float128"/> value to truncate.</param>
    /// <returns>Returns the integral component of <paramref name="value"/>; NaN, ±infinity and ±zero are returned unchanged.</returns>
    public static Float128 Truncate(Float128 value)
    {
        UInt128 bits = value.bits;

        if (!IsFinite(value)) return value;
        if (IsZero(value)) return value;

        uint biasedExponent = ExtractBiasedExponent(bits);

        if (biasedExponent == 0u) return new Float128(bits & SignMask);

        int unbiasedExponent = (int)biasedExponent - ExponentBias;

        if (unbiasedExponent < 0) return new Float128(bits & SignMask);
        if (unbiasedExponent >= TrailingSignificandBits) return value;

        UInt128 fractionMask = (UInt128.One << (TrailingSignificandBits - unbiasedExponent)) - UInt128.One;
        return new Float128(bits & ~fractionMask);
    }
}
