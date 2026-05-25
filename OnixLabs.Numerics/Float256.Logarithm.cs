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
    /// Computes the natural (base-<see cref="E"/>) logarithm of the specified <see cref="Float256"/> value.
    /// </summary>
    /// <param name="value">The value for which to compute the natural logarithm.</param>
    /// <returns>Returns the natural logarithm of <paramref name="value"/>; NaN for negative inputs and NaN; <see cref="NegativeInfinity"/> for zero; <see cref="PositiveInfinity"/> for positive infinity.</returns>
    public static Float256 Log(Float256 value)
    {
        if (IsNaN(value)) return value;
        if (IsZero(value)) return NegativeInfinity;
        if (IsNegative(value)) return NaN;
        if (IsPositiveInfinity(value)) return PositiveInfinity;
        if (value == One) return Zero;

        return LogCore(value);
    }

    /// <summary>
    /// Computes the base-2 logarithm of the specified <see cref="Float256"/> value.
    /// </summary>
    /// <param name="value">The value for which to compute the base-2 logarithm.</param>
    /// <returns>Returns the base-2 logarithm of <paramref name="value"/>; NaN for negative inputs and NaN; <see cref="NegativeInfinity"/> for zero; <see cref="PositiveInfinity"/> for positive infinity.</returns>
    public static Float256 Log2(Float256 value)
    {
        if (IsNaN(value)) return value;
        if (IsZero(value)) return NegativeInfinity;
        if (IsNegative(value)) return NaN;
        if (IsPositiveInfinity(value)) return PositiveInfinity;
        if (value == One) return Zero;
        if (IsPow2(value)) return (Float256)ILogB(value);

        return LogCore(value) * Log2E;
    }

    /// <summary>
    /// Computes the base-10 logarithm of the specified <see cref="Float256"/> value.
    /// </summary>
    /// <param name="value">The value for which to compute the base-10 logarithm.</param>
    /// <returns>Returns the base-10 logarithm of <paramref name="value"/>; NaN for negative inputs and NaN; <see cref="NegativeInfinity"/> for zero; <see cref="PositiveInfinity"/> for positive infinity.</returns>
    public static Float256 Log10(Float256 value)
    {
        if (IsNaN(value)) return value;
        if (IsZero(value)) return NegativeInfinity;
        if (IsNegative(value)) return NaN;
        if (IsPositiveInfinity(value)) return PositiveInfinity;
        if (value == One) return Zero;

        return LogCore(value) * Log10E;
    }

    /// <summary>
    /// Computes the natural logarithm of one plus the specified <see cref="Float256"/> value, preserving accuracy near zero.
    /// </summary>
    /// <param name="value">The value to be added to one before computing the natural logarithm.</param>
    /// <returns>Returns the natural logarithm of <c>1 + value</c>; NaN for inputs less than negative one; <see cref="NegativeInfinity"/> when <paramref name="value"/> equals negative one.</returns>
    public static Float256 LogP1(Float256 value)
    {
        if (IsNaN(value)) return value;
        if (IsZero(value)) return value;
        if (value == NegativeOne) return NegativeInfinity;
        if (value < NegativeOne) return NaN;
        if (IsPositiveInfinity(value)) return PositiveInfinity;

        return Log1pCore(value);
    }

    /// <summary>
    /// Computes the base-2 logarithm of one plus the specified <see cref="Float256"/> value, preserving accuracy near zero.
    /// </summary>
    /// <param name="value">The value to be added to one before computing the base-2 logarithm.</param>
    /// <returns>Returns the base-2 logarithm of <c>1 + value</c>; NaN for inputs less than negative one; <see cref="NegativeInfinity"/> when <paramref name="value"/> equals negative one.</returns>
    public static Float256 Log2P1(Float256 value)
    {
        if (IsNaN(value)) return value;
        if (IsZero(value)) return value;
        if (value == NegativeOne) return NegativeInfinity;
        if (value < NegativeOne) return NaN;
        if (IsPositiveInfinity(value)) return PositiveInfinity;

        return Log1pCore(value) * Log2E;
    }

    /// <summary>
    /// Computes the base-10 logarithm of one plus the specified <see cref="Float256"/> value, preserving accuracy near zero.
    /// </summary>
    /// <param name="value">The value to be added to one before computing the base-10 logarithm.</param>
    /// <returns>Returns the base-10 logarithm of <c>1 + value</c>; NaN for inputs less than negative one; <see cref="NegativeInfinity"/> when <paramref name="value"/> equals negative one.</returns>
    public static Float256 Log10P1(Float256 value)
    {
        if (IsNaN(value)) return value;
        if (IsZero(value)) return value;
        if (value == NegativeOne) return NegativeInfinity;
        if (value < NegativeOne) return NaN;
        if (IsPositiveInfinity(value)) return PositiveInfinity;

        return Log1pCore(value) * Log10E;
    }

    /// <summary>Computes the logarithm of the specified <see cref="Float256"/> value in the specified base.</summary>
    /// <param name="value">The value for which to compute the logarithm.</param>
    /// <param name="newBase">The base of the logarithm.</param>
    /// <returns>Returns the logarithm of <paramref name="value"/> in base <paramref name="newBase"/>.</returns>
    public static Float256 Log(Float256 value, Float256 newBase)
    {
        if (IsNaN(value)) return value;
        if (IsNaN(newBase)) return newBase;
        if (newBase == One) return NaN;
        if (newBase == Two) return Log2(value);
        if (newBase == Ten) return Log10(value);

        return Log(value) / Log(newBase);
    }

    /// <summary>
    /// Computes the natural logarithm of <paramref name="value"/> by extracting the binary exponent and applying an <see cref="AtanhSeries"/> expansion on the normalised mantissa.
    /// </summary>
    /// <param name="value">The value whose natural logarithm is required, assumed to be a positive finite <see cref="Float256"/>.</param>
    /// <returns>Returns the natural logarithm of <paramref name="value"/> rounded to <see cref="Float256"/> precision.</returns>
    private static Float256 LogCore(in Float256 value)
    {
        int e = ILogB(value);
        Float256 m = ScaleB(value, -e);

        if (m > SqrtTwo)
        {
            m = ScaleB(m, -1);
            e++;
        }

        Float256 u = (m - One) / (m + One);
        Float256 logM = AtanhSeries(u) * Two;
        return (Float256)e * Ln2 + logM;
    }

    /// <summary>
    /// Computes <c>log(1 + value)</c> using a direct series for small arguments to preserve precision, delegating to <see cref="LogCore"/> otherwise.
    /// </summary>
    /// <param name="value">The value to add to one before computing the logarithm.</param>
    /// <returns>Returns <c>log(1 + value)</c> rounded to <see cref="Float256"/> precision.</returns>
    private static Float256 Log1pCore(in Float256 value)
    {
        if (Abs(value) >= Half) return LogCore(One + value);

        Float256 u = value / (Two + value);
        return AtanhSeries(u) * Two;
    }

    /// <summary>
    /// Evaluates the Maclaurin series for inverse hyperbolic tangent at the specified reduced argument <paramref name="u"/>.
    /// </summary>
    /// <param name="u">The reduced argument, assumed to lie strictly within <c>(-1, 1)</c> for series convergence.</param>
    /// <returns>Returns the inverse hyperbolic tangent of <paramref name="u"/> rounded to <see cref="Float256"/> precision.</returns>
    private static Float256 AtanhSeries(in Float256 u)
    {
        Float256 sum = u;
        Float256 term = u;
        Float256 uSquared = u * u;

        for (int k = 1; k <= 200; k++)
        {
            term = term * uSquared;
            Float256 summand = term / (Float256)(2 * k + 1);
            Float256 previous = sum;
            sum = sum + summand;
            if (sum == previous) break;
        }

        return sum;
    }
}
