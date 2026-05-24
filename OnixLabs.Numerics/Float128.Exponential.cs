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
using System.Globalization;

namespace OnixLabs.Numerics;

public readonly partial struct Float128
{
    /// <summary>
    /// The largest exponent above which <see cref="Exp"/> saturates to <see cref="PositiveInfinity"/>.
    /// </summary>
    private static readonly Float128 ExpUpperThreshold = Parse("11357", CultureInfo.InvariantCulture);

    /// <summary>
    /// The smallest exponent below which <see cref="Exp"/> underflows to <see cref="Zero"/>.
    /// </summary>
    private static readonly Float128 ExpLowerThreshold = Parse("-11434", CultureInfo.InvariantCulture);

    /// <summary>
    /// Computes <see cref="E"/> raised to the power of the specified <see cref="Float128"/> value.
    /// </summary>
    /// <param name="value">The exponent to raise <see cref="E"/> to.</param>
    /// <returns>Returns <c>e^value</c>; NaN for NaN; <see cref="PositiveInfinity"/> for positive infinity or overflow; <see cref="Zero"/> for negative infinity or underflow.</returns>
    public static Float128 Exp(Float128 value)
    {
        if (IsNaN(value)) return value;
        if (IsPositiveInfinity(value)) return PositiveInfinity;
        if (IsNegativeInfinity(value)) return Zero;
        if (IsZero(value)) return One;
        if (value > ExpUpperThreshold) return PositiveInfinity;
        if (value < ExpLowerThreshold) return Zero;

        return ExpCore(value);
    }

    /// <summary>
    /// Computes <c>2</c> raised to the power of the specified <see cref="Float128"/> value.
    /// </summary>
    /// <param name="value">The exponent to raise <c>2</c> to.</param>
    /// <returns>Returns <c>2^value</c>; NaN for NaN; <see cref="PositiveInfinity"/> for positive infinity or overflow; <see cref="Zero"/> for negative infinity or underflow.</returns>
    public static Float128 Exp2(Float128 value)
    {
        if (IsNaN(value)) return value;
        if (IsPositiveInfinity(value)) return PositiveInfinity;
        if (IsNegativeInfinity(value)) return Zero;
        if (IsZero(value)) return One;

        if (IsInteger(value))
        {
            if (value > (Float128)MaxFiniteUnbiasedExponent) return PositiveInfinity;
            if (value < (Float128)(MinNormalUnbiasedExponent - TrailingSignificandBits - 1)) return Zero;
            return ScaleB(One, (int)value);
        }

        return Exp(value * Ln2);
    }

    /// <summary>
    /// Computes <c>10</c> raised to the power of the specified <see cref="Float128"/> value.
    /// </summary>
    /// <param name="value">The exponent to raise <c>10</c> to.</param>
    /// <returns>Returns <c>10^value</c>; NaN for NaN; <see cref="PositiveInfinity"/> for positive infinity or overflow; <see cref="Zero"/> for negative infinity or underflow.</returns>
    public static Float128 Exp10(Float128 value)
    {
        if (IsNaN(value)) return value;
        if (IsPositiveInfinity(value)) return PositiveInfinity;
        if (IsNegativeInfinity(value)) return Zero;
        if (IsZero(value)) return One;

        return Exp(value * Ln10);
    }

    /// <summary>
    /// Computes <see cref="E"/> raised to the power of the specified <see cref="Float128"/> value, minus one, preserving accuracy near zero.
    /// </summary>
    /// <param name="value">The exponent to raise <see cref="E"/> to.</param>
    /// <returns>Returns <c>e^value - 1</c>; preserves the sign of zero; returns <see cref="NegativeOne"/> for negative infinity.</returns>
    public static Float128 ExpM1(Float128 value)
    {
        if (IsNaN(value)) return value;
        if (IsPositiveInfinity(value)) return PositiveInfinity;
        if (IsNegativeInfinity(value)) return NegativeOne;
        if (IsZero(value)) return value;

        return ExpM1Core(value);
    }

    /// <summary>
    /// Computes <c>2</c> raised to the power of the specified <see cref="Float128"/> value, minus one, preserving accuracy near zero.
    /// </summary>
    /// <param name="value">The exponent to raise <c>2</c> to.</param>
    /// <returns>Returns <c>2^value - 1</c>; preserves the sign of zero; returns <see cref="NegativeOne"/> for negative infinity.</returns>
    public static Float128 Exp2M1(Float128 value)
    {
        if (IsNaN(value)) return value;
        if (IsPositiveInfinity(value)) return PositiveInfinity;
        if (IsNegativeInfinity(value)) return NegativeOne;
        if (IsZero(value)) return value;

        return ExpM1Core(value * Ln2);
    }

    /// <summary>
    /// Computes <c>10</c> raised to the power of the specified <see cref="Float128"/> value, minus one, preserving accuracy near zero.
    /// </summary>
    /// <param name="value">The exponent to raise <c>10</c> to.</param>
    /// <returns>Returns <c>10^value - 1</c>; preserves the sign of zero; returns <see cref="NegativeOne"/> for negative infinity.</returns>
    public static Float128 Exp10M1(Float128 value)
    {
        if (IsNaN(value)) return value;
        if (IsPositiveInfinity(value)) return PositiveInfinity;
        if (IsNegativeInfinity(value)) return NegativeOne;
        if (IsZero(value)) return value;

        return ExpM1Core(value * Ln10);
    }

    /// <summary>
    /// Evaluates the exponential function by range-reducing to <c>r = value - n ln 2</c> and summing the Taylor series for <c>e^r</c>, then rescaling by <c>2^n</c>.
    /// </summary>
    /// <param name="value">The exponent within the supported finite range.</param>
    /// <returns>The <see cref="Float128"/> approximation of <c>e^value</c>.</returns>
    private static Float128 ExpCore(Float128 value)
    {
        Float128 nFloat = Round(value * Log2E);
        int n = (int)nFloat;
        Float128 r = value - nFloat * Ln2;

        Float128 sum = One;
        Float128 term = One;
        for (int k = 1; k <= 80; k++)
        {
            term = term * r / (Float128)k;
            Float128 previous = sum;
            sum = sum + term;
            if (sum == previous) break;
        }

        return ScaleB(sum, n);
    }

    /// <summary>
    /// Evaluates <c>e^value - 1</c>, summing the Taylor series directly for small arguments to retain precision near zero.
    /// </summary>
    /// <param name="value">The exponent within the supported finite range.</param>
    /// <returns>The <see cref="Float128"/> approximation of <c>e^value - 1</c>.</returns>
    private static Float128 ExpM1Core(Float128 value)
    {
        if (Abs(value) >= Half)
        {
            if (value > ExpUpperThreshold) return PositiveInfinity;
            if (value < ExpLowerThreshold) return NegativeOne;
            return ExpCore(value) - One;
        }

        Float128 sum = value;
        Float128 term = value;
        for (int k = 2; k <= 80; k++)
        {
            term = term * value / (Float128)k;
            Float128 previous = sum;
            sum = sum + term;
            if (sum == previous) break;
        }
        return sum;
    }
}
