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
    /// Gets the absolute value of the specified <see cref="Float128"/> value.
    /// </summary>
    /// <param name="value">The <see cref="Float128"/> from which to obtain an absolute value.</param>
    /// <returns>Returns the absolute value of the specified <see cref="Float128"/> value.</returns>
    /// <remarks>
    /// The result preserves NaN payloads; for any NaN input, the result is also a NaN.
    /// Negative zero becomes positive zero, and negative infinity becomes positive infinity.
    /// </remarks>
    public static Float128 Abs(Float128 value) => new(value.Bits & ~SignMask);

    /// <summary>
    /// Returns the unary negation of the specified <see cref="Float128"/> value.
    /// </summary>
    /// <param name="value">The value to negate.</param>
    /// <returns>Returns the negation of <paramref name="value"/>.</returns>
    public static Float128 Negate(Float128 value) => -value;

    /// <summary>
    /// Returns the sign of the specified <see cref="Float128"/> value.
    /// </summary>
    /// <param name="value">The value whose sign is to be returned.</param>
    /// <returns>Returns -1 for negative values, 0 for zero, and 1 for positive values.</returns>
    /// <exception cref="ArithmeticException">If <paramref name="value"/> is <see cref="NaN"/>.</exception>
    public static int Sign(Float128 value)
    {
        if (IsNaN(value)) throw new ArithmeticException("Function does not accept floating point Not-a-Number values.");
        if (IsZero(value)) return 0;
        return IsNegative(value) ? -1 : 1;
    }
}
