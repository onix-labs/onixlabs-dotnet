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

public readonly partial struct Float256
{
    /// <summary>
    /// The upper input bound for <see cref="Exp(Float256)"/>; inputs above this value saturate to <see cref="PositiveInfinity"/>.
    /// </summary>
    private static readonly Float256 ExpUpperThreshold = Parse("181708", CultureInfo.InvariantCulture);

    /// <summary>
    /// The lower input bound for <see cref="Exp(Float256)"/>; inputs below this value underflow to <see cref="Zero"/>.
    /// </summary>
    private static readonly Float256 ExpLowerThreshold = Parse("-181851", CultureInfo.InvariantCulture);

    /// <summary>
    /// Computes <see cref="E"/> raised to the power of the specified <see cref="Float256"/> value.
    /// </summary>
    /// <param name="value">The exponent to raise <see cref="E"/> to.</param>
    /// <returns>Returns <c>e^value</c>; NaN for NaN; <see cref="PositiveInfinity"/> for positive infinity or overflow; <see cref="Zero"/> for negative infinity or underflow.</returns>
    public static Float256 Exp(Float256 value)
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
    /// Computes <c>2</c> raised to the power of the specified <see cref="Float256"/> value.
    /// </summary>
    /// <param name="value">The exponent to raise <c>2</c> to.</param>
    /// <returns>Returns <c>2^value</c>; NaN for NaN; <see cref="PositiveInfinity"/> for positive infinity or overflow; <see cref="Zero"/> for negative infinity or underflow.</returns>
    public static Float256 Exp2(Float256 value)
    {
        if (IsNaN(value)) return value;
        if (IsPositiveInfinity(value)) return PositiveInfinity;
        if (IsNegativeInfinity(value)) return Zero;
        if (IsZero(value)) return One;

        if (IsInteger(value))
        {
            if (value > (Float256)MaxFiniteUnbiasedExponent) return PositiveInfinity;
            if (value < (Float256)(MinNormalUnbiasedExponent - TrailingSignificandBits - 1)) return Zero;
            return ScaleB(One, (int)value);
        }

        return Exp(value * Ln2);
    }

    /// <summary>
    /// Computes <c>10</c> raised to the power of the specified <see cref="Float256"/> value.
    /// </summary>
    /// <param name="value">The exponent to raise <c>10</c> to.</param>
    /// <returns>Returns <c>10^value</c>; NaN for NaN; <see cref="PositiveInfinity"/> for positive infinity or overflow; <see cref="Zero"/> for negative infinity or underflow.</returns>
    public static Float256 Exp10(Float256 value)
    {
        if (IsNaN(value)) return value;
        if (IsPositiveInfinity(value)) return PositiveInfinity;
        if (IsNegativeInfinity(value)) return Zero;
        if (IsZero(value)) return One;

        return Exp(value * Ln10);
    }

    /// <summary>
    /// Computes <see cref="E"/> raised to the power of the specified <see cref="Float256"/> value, minus one, preserving accuracy near zero.
    /// </summary>
    /// <param name="value">The exponent to raise <see cref="E"/> to.</param>
    /// <returns>Returns <c>e^value - 1</c>; preserves the sign of zero; returns <see cref="NegativeOne"/> for negative infinity.</returns>
    public static Float256 ExpM1(Float256 value)
    {
        if (IsNaN(value)) return value;
        if (IsPositiveInfinity(value)) return PositiveInfinity;
        if (IsNegativeInfinity(value)) return NegativeOne;
        if (IsZero(value)) return value;

        return ExpM1Core(value);
    }

    /// <summary>
    /// Computes <c>2</c> raised to the power of the specified <see cref="Float256"/> value, minus one, preserving accuracy near zero.
    /// </summary>
    /// <param name="value">The exponent to raise <c>2</c> to.</param>
    /// <returns>Returns <c>2^value - 1</c>; preserves the sign of zero; returns <see cref="NegativeOne"/> for negative infinity.</returns>
    public static Float256 Exp2M1(Float256 value)
    {
        if (IsNaN(value)) return value;
        if (IsPositiveInfinity(value)) return PositiveInfinity;
        if (IsNegativeInfinity(value)) return NegativeOne;
        if (IsZero(value)) return value;

        return ExpM1Core(value * Ln2);
    }

    /// <summary>
    /// Computes <c>10</c> raised to the power of the specified <see cref="Float256"/> value, minus one, preserving accuracy near zero.
    /// </summary>
    /// <param name="value">The exponent to raise <c>10</c> to.</param>
    /// <returns>Returns <c>10^value - 1</c>; preserves the sign of zero; returns <see cref="NegativeOne"/> for negative infinity.</returns>
    public static Float256 Exp10M1(Float256 value)
    {
        if (IsNaN(value)) return value;
        if (IsPositiveInfinity(value)) return PositiveInfinity;
        if (IsNegativeInfinity(value)) return NegativeOne;
        if (IsZero(value)) return value;

        return ExpM1Core(value * Ln10);
    }

    /// <summary>
    /// Computes <c>e^value</c> using range reduction by <see cref="Ln2"/> followed by Taylor series summation of the reduced argument.
    /// </summary>
    /// <param name="value">The exponent, assumed to be finite and inside the saturation thresholds.</param>
    /// <returns>Returns <c>e^value</c> rounded to <see cref="Float256"/> precision.</returns>
    private static Float256 ExpCore(in Float256 value)
    {
        Float256 nFloat = Round(value * Log2E);
        int n = (int)nFloat;
        Float256 r = value - nFloat * Ln2;

        Float256 sum = One;
        Float256 term = One;
        for (int k = 1; k <= 160; k++)
        {
            term = term * r / (Float256)k;
            Float256 previous = sum;
            sum = sum + term;
            if (sum == previous) break;
        }

        return ScaleB(sum, n);
    }

    /// <summary>
    /// Computes <c>e^value - 1</c> using a Taylor series for arguments near zero to preserve precision, delegating to <see cref="ExpCore"/> for larger arguments.
    /// </summary>
    /// <param name="value">The exponent, assumed to be finite.</param>
    /// <returns>Returns <c>e^value - 1</c> rounded to <see cref="Float256"/> precision.</returns>
    private static Float256 ExpM1Core(in Float256 value)
    {
        if (Abs(value) >= Half)
        {
            if (value > ExpUpperThreshold) return PositiveInfinity;
            if (value < ExpLowerThreshold) return NegativeOne;
            return ExpCore(value) - One;
        }

        Float256 sum = value;
        Float256 term = value;
        for (int k = 2; k <= 160; k++)
        {
            term = term * value / (Float256)k;
            Float256 previous = sum;
            sum = sum + term;
            if (sum == previous) break;
        }
        return sum;
    }
}
