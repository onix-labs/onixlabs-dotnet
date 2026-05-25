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
    /// Returns the smallest <see cref="Float256"/> value larger than the specified value.
    /// </summary>
    /// <param name="value">The value to increment.</param>
    /// <returns>Returns the next representable <see cref="Float256"/> value above <paramref name="value"/>; <see cref="NaN"/> remains NaN; <see cref="PositiveInfinity"/> remains positive infinity; <see cref="NegativeInfinity"/> becomes <see cref="MinValue"/>; either zero becomes <see cref="Epsilon"/>.</returns>
    public static Float256 BitIncrement(Float256 value)
    {
        if (IsNaN(value)) return value;
        if (IsPositiveInfinity(value)) return value;
        if (IsNegativeInfinity(value)) return MinValue;
        if (IsZero(value)) return Epsilon;

        return IsNegative(value)
            ? new Float256(value.Bits - UInt256.One)
            : new Float256(value.Bits + UInt256.One);
    }

    /// <summary>
    /// Returns the largest <see cref="Float256"/> value smaller than the specified value.
    /// </summary>
    /// <param name="value">The value to decrement.</param>
    /// <returns>Returns the next representable <see cref="Float256"/> value below <paramref name="value"/>; <see cref="NaN"/> remains NaN; <see cref="NegativeInfinity"/> remains negative infinity; <see cref="PositiveInfinity"/> becomes <see cref="MaxValue"/>; either zero becomes the largest negative subnormal.</returns>
    public static Float256 BitDecrement(Float256 value)
    {
        if (IsNaN(value)) return value;
        if (IsNegativeInfinity(value)) return value;
        if (IsPositiveInfinity(value)) return MaxValue;
        if (IsZero(value)) return new Float256(SignMask | UInt256.One);

        return IsNegative(value)
            ? new Float256(value.Bits + UInt256.One)
            : new Float256(value.Bits - UInt256.One);
    }
}
