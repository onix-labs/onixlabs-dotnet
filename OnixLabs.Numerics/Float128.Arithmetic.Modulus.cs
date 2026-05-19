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

public readonly partial struct Float128
{
    /// <summary>
    /// Computes the truncating remainder of the specified <see cref="Float128"/> values, matching the semantics of the C# <c>%</c> operator for IEEE 754 floating-point types.
    /// </summary>
    /// <param name="left">The dividend.</param>
    /// <param name="right">The divisor.</param>
    /// <returns>Returns <paramref name="left"/> minus <c>Truncate(left / right) * right</c>; <see cref="NaN"/> for invalid combinations such as infinity divided by anything, division by zero, or any operand being NaN.</returns>
    public static Float128 Remainder(Float128 left, Float128 right)
    {
        if (IsNaN(left) || IsNaN(right)) return NaN;
        if (IsInfinity(left)) return NaN;
        if (IsZero(right)) return NaN;
        if (IsInfinity(right)) return left;
        if (IsZero(left)) return left;

        Float128 quotient = left / right;
        Float128 truncatedQuotient = Truncate(quotient);
        return left - truncatedQuotient * right;
    }

    /// <summary>
    /// Computes the truncating remainder of the specified <see cref="Float128"/> values, matching the semantics of the C# <c>%</c> operator for IEEE 754 floating-point types.
    /// </summary>
    /// <param name="left">The dividend.</param>
    /// <param name="right">The divisor.</param>
    /// <returns>Returns <paramref name="left"/> minus <c>Truncate(left / right) * right</c>.</returns>
    public static Float128 operator %(Float128 left, Float128 right) => Remainder(left, right);
}
