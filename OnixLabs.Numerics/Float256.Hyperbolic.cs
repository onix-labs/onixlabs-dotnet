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
    /// The absolute input threshold beyond which <see cref="Tanh"/> saturates to ±one within <see cref="Float256"/> precision.
    /// </summary>
    private static readonly Float256 TanhSaturationThreshold = (Float256)90;

    /// <summary>Computes the hyperbolic sine of the specified value.</summary>
    /// <param name="value">The value.</param>
    /// <returns>Returns the hyperbolic sine of <paramref name="value"/>.</returns>
    public static Float256 Sinh(Float256 value)
    {
        if (IsNaN(value)) return value;
        if (IsInfinity(value)) return value;
        if (IsZero(value)) return value;
        if (value > ExpUpperThreshold) return PositiveInfinity;
        if (value < -ExpUpperThreshold) return NegativeInfinity;

        Float256 expPlus = ExpM1(value);
        Float256 expMinus = ExpM1(-value);
        return ScaleB(expPlus - expMinus, -1);
    }

    /// <summary>Computes the hyperbolic cosine of the specified value.</summary>
    /// <param name="value">The value.</param>
    /// <returns>Returns the hyperbolic cosine of <paramref name="value"/>.</returns>
    public static Float256 Cosh(Float256 value)
    {
        if (IsNaN(value)) return value;
        if (IsInfinity(value)) return PositiveInfinity;
        if (IsZero(value)) return One;

        Float256 absValue = Abs(value);
        if (absValue > ExpUpperThreshold) return PositiveInfinity;

        Float256 expPositive = Exp(absValue);
        return ScaleB(expPositive + One / expPositive, -1);
    }

    /// <summary>Computes the hyperbolic tangent of the specified value.</summary>
    /// <param name="value">The value.</param>
    /// <returns>Returns the hyperbolic tangent of <paramref name="value"/>.</returns>
    public static Float256 Tanh(Float256 value)
    {
        if (IsNaN(value)) return value;
        if (IsZero(value)) return value;
        if (IsPositiveInfinity(value)) return One;
        if (IsNegativeInfinity(value)) return NegativeOne;

        Float256 absValue = Abs(value);
        if (absValue > TanhSaturationThreshold)
        {
            return IsNegative(value) ? NegativeOne : One;
        }

        Float256 expM1TwoX = ExpM1(ScaleB(value, 1));
        return expM1TwoX / (expM1TwoX + Two);
    }

    /// <summary>Computes the inverse hyperbolic sine of the specified value.</summary>
    /// <param name="value">The value.</param>
    /// <returns>Returns the inverse hyperbolic sine of <paramref name="value"/>.</returns>
    public static Float256 Asinh(Float256 value)
    {
        if (IsNaN(value)) return value;
        if (IsInfinity(value)) return value;
        if (IsZero(value)) return value;

        bool negative = IsNegative(value);
        Float256 absValue = Abs(value);
        Float256 squared = absValue * absValue;
        Float256 sqrtOnePlusSquared = Sqrt(squared + One);
        Float256 result = LogP1(absValue + squared / (One + sqrtOnePlusSquared));
        return negative ? -result : result;
    }

    /// <summary>Computes the inverse hyperbolic cosine of the specified value.</summary>
    /// <param name="value">The value.</param>
    /// <returns>Returns the inverse hyperbolic cosine of <paramref name="value"/>.</returns>
    public static Float256 Acosh(Float256 value)
    {
        if (IsNaN(value)) return value;
        if (value < One) return NaN;
        if (value == One) return Zero;
        if (IsPositiveInfinity(value)) return PositiveInfinity;

        Float256 reduced = value - One;
        Float256 inner = reduced + Sqrt(reduced * (reduced + Two));
        return LogP1(inner);
    }

    /// <summary>Computes the inverse hyperbolic tangent of the specified value.</summary>
    /// <param name="value">The value.</param>
    /// <returns>Returns the inverse hyperbolic tangent of <paramref name="value"/>.</returns>
    public static Float256 Atanh(Float256 value)
    {
        if (IsNaN(value)) return value;
        if (IsZero(value)) return value;
        if (value == One) return PositiveInfinity;
        if (value == NegativeOne) return NegativeInfinity;
        if (Abs(value) > One) return NaN;

        Float256 twiceValueOverOneMinusValue = ScaleB(value, 1) / (One - value);
        return ScaleB(LogP1(twiceValueOverOneMinusValue), -1);
    }
}
