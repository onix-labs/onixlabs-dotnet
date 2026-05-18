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
    /// Computes the largest integral <see cref="Float128"/> value that is less than or equal to the specified value.
    /// </summary>
    /// <param name="value">The <see cref="Float128"/> value to floor.</param>
    /// <returns>Returns the floor of <paramref name="value"/>; NaN, ±infinity and ±zero are returned unchanged.</returns>
    public static Float128 Floor(Float128 value)
    {
        Float128 truncated = Truncate(value);
        if (truncated.bits == value.bits) return truncated;
        if (!IsNegative(value)) return truncated;

        UInt128 absoluteTruncatedBits = truncated.bits & ~SignMask;
        if (absoluteTruncatedBits == UInt128.Zero) return NegativeOne;

        UInt128 incrementedAbsolute = IncrementIntegerMagnitudeBits(absoluteTruncatedBits);
        return new Float128(incrementedAbsolute | SignMask);
    }
}
