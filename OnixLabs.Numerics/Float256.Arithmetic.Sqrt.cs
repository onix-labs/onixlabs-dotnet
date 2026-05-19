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
    /// <summary>Computes the square root of the specified <see cref="Float256"/> value.</summary>
    /// <param name="value">The value whose square root is to be computed.</param>
    /// <returns>Returns the square root of <paramref name="value"/>.</returns>
    public static Float256 Sqrt(Float256 value)
    {
        if (IsNaN(value)) return value;
        if (IsZero(value)) return value;
        if (IsNegative(value)) return NaN;
        if (IsPositiveInfinity(value)) return value;

        double seed = Math.Sqrt((double)value);
        if (double.IsNaN(seed) || seed == 0.0) seed = 1.0;
        Float256 estimate = (Float256)seed;
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
