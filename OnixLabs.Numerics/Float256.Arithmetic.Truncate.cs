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
    /// <summary>Returns the integral part of the specified <see cref="Float256"/> value.</summary>
    /// <param name="value">The value whose integral part is to be computed.</param>
    /// <returns>Returns the integral part of <paramref name="value"/>.</returns>
    public static Float256 Truncate(Float256 value)
    {
        UInt256 bits = value.Bits;

        if (!IsFinite(value)) return value;
        if (IsZero(value)) return value;

        uint biasedExponent = ExtractBiasedExponent(bits);

        if (biasedExponent == 0u) return new Float256(bits & SignMask);

        int unbiasedExponent = (int)biasedExponent - ExponentBias;

        if (unbiasedExponent < 0) return new Float256(bits & SignMask);
        if (unbiasedExponent >= TrailingSignificandBits) return value;

        UInt256 fractionMask = (UInt256.One << (TrailingSignificandBits - unbiasedExponent)) - UInt256.One;
        return new Float256(bits & ~fractionMask);
    }
}
