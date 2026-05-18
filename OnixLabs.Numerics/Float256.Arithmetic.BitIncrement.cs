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
    /// <summary>Returns the smallest <see cref="Float256"/> value strictly greater than <paramref name="value"/>.</summary>
    /// <param name="value">The value whose successor is to be returned.</param>
    /// <returns>Returns the next-larger representable <see cref="Float256"/> value.</returns>
    public static Float256 BitIncrement(Float256 value)
    {
        if (IsNaN(value)) return value;
        if (IsPositiveInfinity(value)) return value;
        if (IsNegativeInfinity(value)) return MinValue;
        if (IsNegative(value))
        {
            if (IsZero(value)) return Epsilon;
            return new Float256(value.bits - UInt256.One);
        }
        return new Float256(value.bits + UInt256.One);
    }

    /// <summary>Returns the largest <see cref="Float256"/> value strictly less than <paramref name="value"/>.</summary>
    /// <param name="value">The value whose predecessor is to be returned.</param>
    /// <returns>Returns the next-smaller representable <see cref="Float256"/> value.</returns>
    public static Float256 BitDecrement(Float256 value)
    {
        if (IsNaN(value)) return value;
        if (IsNegativeInfinity(value)) return value;
        if (IsPositiveInfinity(value)) return MaxValue;
        if (IsNegative(value))
        {
            return new Float256(value.bits + UInt256.One);
        }
        if (IsZero(value)) return new Float256(SignMask | UInt256.One);
        return new Float256(value.bits - UInt256.One);
    }
}
