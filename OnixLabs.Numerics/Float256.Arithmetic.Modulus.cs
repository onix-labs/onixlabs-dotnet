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
    /// <returns>Returns <c>left - Truncate(left / right) * right</c>; NaN for invalid combinations.</returns>
    public static Float256 Remainder(Float256 left, Float256 right)
    {
        if (IsNaN(left) || IsNaN(right)) return NaN;
        if (IsInfinity(left)) return NaN;
        if (IsZero(right)) return NaN;
        if (IsInfinity(right)) return left;
        if (IsZero(left)) return left;

        Float256 quotient = left / right;
        Float256 truncatedQuotient = Truncate(quotient);
        return left - truncatedQuotient * right;
    }

    /// <inheritdoc cref="Remainder(Float256, Float256)"/>
    public static Float256 operator %(Float256 left, Float256 right) => Remainder(left, right);
}
