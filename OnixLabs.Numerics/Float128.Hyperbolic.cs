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
    /// The magnitude above which <see cref="Tanh"/> saturates to <see cref="One"/> or <see cref="NegativeOne"/> within binary128 precision.
    /// </summary>
    private static readonly Float128 TanhSaturationThreshold = (Float128)40;

    /// <summary>
    /// Computes the hyperbolic sine of the specified <see cref="Float128"/> value.
    /// </summary>
    /// <param name="value">The value whose hyperbolic sine is to be computed.</param>
    /// <returns>Returns the hyperbolic sine of <paramref name="value"/>; preserves the sign of zero; ±infinity for ±infinity or overflow.</returns>
    public static Float128 Sinh(Float128 value)
    {
        if (IsNaN(value)) return value;
        if (IsInfinity(value)) return value;
        if (IsZero(value)) return value;
        if (value > ExpUpperThreshold) return PositiveInfinity;
        if (value < -ExpUpperThreshold) return NegativeInfinity;

        Float128 expPlus = ExpM1(value);
        Float128 expMinus = ExpM1(-value);
        return ScaleB(expPlus - expMinus, -1);
    }

    /// <summary>
    /// Computes the hyperbolic cosine of the specified <see cref="Float128"/> value.
    /// </summary>
    /// <param name="value">The value whose hyperbolic cosine is to be computed.</param>
    /// <returns>Returns the hyperbolic cosine of <paramref name="value"/>; <see cref="One"/> for zero; <see cref="PositiveInfinity"/> for ±infinity or overflow.</returns>
    public static Float128 Cosh(Float128 value)
    {
        if (IsNaN(value)) return value;
        if (IsInfinity(value)) return PositiveInfinity;
        if (IsZero(value)) return One;

        Float128 absValue = Abs(value);
        if (absValue > ExpUpperThreshold) return PositiveInfinity;

        Float128 expPositive = Exp(absValue);
        return ScaleB(expPositive + One / expPositive, -1);
    }

    /// <summary>
    /// Computes the hyperbolic tangent of the specified <see cref="Float128"/> value.
    /// </summary>
    /// <param name="value">The value whose hyperbolic tangent is to be computed.</param>
    /// <returns>Returns the hyperbolic tangent of <paramref name="value"/>; preserves the sign of zero; saturates to ±1 for large magnitudes; ±1 for ±infinity.</returns>
    public static Float128 Tanh(Float128 value)
    {
        if (IsNaN(value)) return value;
        if (IsZero(value)) return value;
        if (IsPositiveInfinity(value)) return One;
        if (IsNegativeInfinity(value)) return NegativeOne;

        Float128 absValue = Abs(value);
        if (absValue > TanhSaturationThreshold)
        {
            return IsNegative(value) ? NegativeOne : One;
        }

        Float128 expM1TwoX = ExpM1(ScaleB(value, 1));
        return expM1TwoX / (expM1TwoX + Two);
    }

    /// <summary>
    /// Computes the inverse hyperbolic sine of the specified <see cref="Float128"/> value.
    /// </summary>
    /// <param name="value">The value whose inverse hyperbolic sine is to be computed.</param>
    /// <returns>Returns the inverse hyperbolic sine of <paramref name="value"/>; preserves the sign of zero; ±infinity for ±infinity.</returns>
    public static Float128 Asinh(Float128 value)
    {
        if (IsNaN(value)) return value;
        if (IsInfinity(value)) return value;
        if (IsZero(value)) return value;

        bool negative = IsNegative(value);
        Float128 absValue = Abs(value);
        Float128 squared = absValue * absValue;
        Float128 sqrtOnePlusSquared = Sqrt(squared + One);
        Float128 result = LogP1(absValue + squared / (One + sqrtOnePlusSquared));
        return negative ? -result : result;
    }

    /// <summary>
    /// Computes the inverse hyperbolic cosine of the specified <see cref="Float128"/> value.
    /// </summary>
    /// <param name="value">The value whose inverse hyperbolic cosine is to be computed.</param>
    /// <returns>Returns the inverse hyperbolic cosine of <paramref name="value"/>; NaN for inputs less than one; <see cref="Zero"/> when <paramref name="value"/> equals one; <see cref="PositiveInfinity"/> for positive infinity.</returns>
    public static Float128 Acosh(Float128 value)
    {
        if (IsNaN(value)) return value;
        if (value < One) return NaN;
        if (value == One) return Zero;
        if (IsPositiveInfinity(value)) return PositiveInfinity;

        Float128 reduced = value - One;
        Float128 inner = reduced + Sqrt(reduced * (reduced + Two));
        return LogP1(inner);
    }

    /// <summary>
    /// Computes the inverse hyperbolic tangent of the specified <see cref="Float128"/> value.
    /// </summary>
    /// <param name="value">The value whose inverse hyperbolic tangent is to be computed.</param>
    /// <returns>Returns the inverse hyperbolic tangent of <paramref name="value"/>; preserves the sign of zero; ±infinity at ±1; NaN for inputs outside <c>[-1, 1]</c>.</returns>
    public static Float128 Atanh(Float128 value)
    {
        if (IsNaN(value)) return value;
        if (IsZero(value)) return value;
        if (value == One) return PositiveInfinity;
        if (value == NegativeOne) return NegativeInfinity;
        if (Abs(value) > One) return NaN;

        Float128 twiceValueOverOneMinusValue = ScaleB(value, 1) / (One - value);
        return ScaleB(LogP1(twiceValueOverOneMinusValue), -1);
    }
}
