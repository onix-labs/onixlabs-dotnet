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
    /// Computes the natural (base-<see cref="E"/>) logarithm of the specified <see cref="Float128"/> value.
    /// </summary>
    /// <param name="value">The value for which to compute the natural logarithm.</param>
    /// <returns>Returns the natural logarithm of <paramref name="value"/>; NaN for negative inputs and NaN; <see cref="NegativeInfinity"/> for zero; <see cref="PositiveInfinity"/> for positive infinity.</returns>
    public static Float128 Log(Float128 value)
    {
        if (IsNaN(value)) return value;
        if (IsZero(value)) return NegativeInfinity;
        if (IsNegative(value)) return NaN;
        if (IsPositiveInfinity(value)) return PositiveInfinity;
        if (value == One) return Zero;

        return LogCore(value, out _);
    }

    /// <summary>
    /// Computes the base-2 logarithm of the specified <see cref="Float128"/> value.
    /// </summary>
    /// <param name="value">The value for which to compute the base-2 logarithm.</param>
    /// <returns>Returns the base-2 logarithm of <paramref name="value"/>; NaN for negative inputs and NaN; <see cref="NegativeInfinity"/> for zero; <see cref="PositiveInfinity"/> for positive infinity.</returns>
    public static Float128 Log2(Float128 value)
    {
        if (IsNaN(value)) return value;
        if (IsZero(value)) return NegativeInfinity;
        if (IsNegative(value)) return NaN;
        if (IsPositiveInfinity(value)) return PositiveInfinity;
        if (value == One) return Zero;

        return Log2Core(value);
    }

    /// <summary>
    /// Computes the base-10 logarithm of the specified <see cref="Float128"/> value.
    /// </summary>
    /// <param name="value">The value for which to compute the base-10 logarithm.</param>
    /// <returns>Returns the base-10 logarithm of <paramref name="value"/>; NaN for negative inputs and NaN; <see cref="NegativeInfinity"/> for zero; <see cref="PositiveInfinity"/> for positive infinity.</returns>
    public static Float128 Log10(Float128 value)
    {
        if (IsNaN(value)) return value;
        if (IsZero(value)) return NegativeInfinity;
        if (IsNegative(value)) return NaN;
        if (IsPositiveInfinity(value)) return PositiveInfinity;
        if (value == One) return Zero;

        return LogCore(value, out _) * Log10E;
    }

    /// <summary>
    /// Computes the natural logarithm of one plus the specified <see cref="Float128"/> value, preserving accuracy near zero.
    /// </summary>
    /// <param name="value">The value to be added to one before computing the natural logarithm.</param>
    /// <returns>Returns the natural logarithm of <c>1 + value</c>; NaN for inputs less than negative one; <see cref="NegativeInfinity"/> when <paramref name="value"/> equals negative one.</returns>
    public static Float128 LogP1(Float128 value)
    {
        if (IsNaN(value)) return value;
        if (IsZero(value)) return value;
        if (value == NegativeOne) return NegativeInfinity;
        if (value < NegativeOne) return NaN;
        if (IsPositiveInfinity(value)) return PositiveInfinity;

        return Log1pCore(value);
    }

    /// <summary>
    /// Computes the base-2 logarithm of one plus the specified <see cref="Float128"/> value, preserving accuracy near zero.
    /// </summary>
    /// <param name="value">The value to be added to one before computing the base-2 logarithm.</param>
    /// <returns>Returns the base-2 logarithm of <c>1 + value</c>; NaN for inputs less than negative one; <see cref="NegativeInfinity"/> when <paramref name="value"/> equals negative one.</returns>
    public static Float128 Log2P1(Float128 value)
    {
        if (IsNaN(value)) return value;
        if (IsZero(value)) return value;
        if (value == NegativeOne) return NegativeInfinity;
        if (value < NegativeOne) return NaN;
        if (IsPositiveInfinity(value)) return PositiveInfinity;

        return Log1pCore(value) * Log2E;
    }

    /// <summary>
    /// Computes the base-10 logarithm of one plus the specified <see cref="Float128"/> value, preserving accuracy near zero.
    /// </summary>
    /// <param name="value">The value to be added to one before computing the base-10 logarithm.</param>
    /// <returns>Returns the base-10 logarithm of <c>1 + value</c>; NaN for inputs less than negative one; <see cref="NegativeInfinity"/> when <paramref name="value"/> equals negative one.</returns>
    public static Float128 Log10P1(Float128 value)
    {
        if (IsNaN(value)) return value;
        if (IsZero(value)) return value;
        if (value == NegativeOne) return NegativeInfinity;
        if (value < NegativeOne) return NaN;
        if (IsPositiveInfinity(value)) return PositiveInfinity;

        return Log1pCore(value) * Log10E;
    }

    /// <summary>
    /// Computes the logarithm of the specified <see cref="Float128"/> value in the specified base.
    /// </summary>
    /// <param name="value">The value for which to compute the logarithm.</param>
    /// <param name="newBase">The base of the logarithm.</param>
    /// <returns>Returns the logarithm of <paramref name="value"/> in base <paramref name="newBase"/>.</returns>
    public static Float128 Log(Float128 value, Float128 newBase)
    {
        if (IsNaN(value)) return value;
        if (IsNaN(newBase)) return newBase;
        if (newBase == One) return NaN;
        if (newBase == Two) return Log2(value);
        if (newBase == Ten) return Log10(value);

        return Log(value) / Log(newBase);
    }

    /// <summary>
    /// Computes the base-2 logarithm of the specified <see cref="Float128"/> value, returning the exact exponent for exact powers of two.
    /// </summary>
    /// <param name="value">The positive finite value whose base-2 logarithm is to be computed.</param>
    /// <returns>Returns the <see cref="Float128"/> approximation of <c>log2(value)</c>.</returns>
    private static Float128 Log2Core(in Float128 value)
    {
        if (IsPow2(value)) return (Float128)ILogB(value);

        return LogCore(value, out _) * Log2E;
    }

    /// <summary>
    /// Computes the natural logarithm of the specified <see cref="Float128"/> value, range-reducing the mantissa into the interval around one before invoking the <c>atanh</c> series.
    /// </summary>
    /// <param name="value">The positive finite value whose natural logarithm is to be computed.</param>
    /// <param name="reducedExponent">When this method returns, contains the binary exponent used during reduction.</param>
    /// <returns>Returns the <see cref="Float128"/> approximation of <c>ln(value)</c>.</returns>
    private static Float128 LogCore(in Float128 value, out int reducedExponent)
    {
        int e = ILogB(value);
        Float128 m = ScaleB(value, -e);

        if (m > SqrtTwo)
        {
            m = ScaleB(m, -1);
            e++;
        }

        reducedExponent = e;

        Float128 u = (m - One) / (m + One);
        Float128 logM = AtanhSeries(u) * Two;
        return (Float128)e * Ln2 + logM;
    }

    /// <summary>
    /// Computes <c>ln(1 + value)</c> using the <c>atanh</c> series directly for small arguments to retain precision near zero.
    /// </summary>
    /// <param name="value">The value satisfying <c>1 + value &gt; 0</c>.</param>
    /// <returns>Returns the <see cref="Float128"/> approximation of <c>ln(1 + value)</c>.</returns>
    private static Float128 Log1pCore(in Float128 value)
    {
        Float128 absValue = Abs(value);

        if (absValue >= Half)
        {
            return LogCore(One + value, out _);
        }

        Float128 u = value / (Two + value);
        return AtanhSeries(u) * Two;
    }

    /// <summary>
    /// Evaluates the Taylor series for <c>atanh(u)</c>, used as the core kernel for logarithm reductions.
    /// </summary>
    /// <param name="u">The reduced argument whose magnitude is small enough for rapid series convergence.</param>
    /// <returns>Returns the <see cref="Float128"/> approximation of <c>atanh(u)</c>.</returns>
    private static Float128 AtanhSeries(in Float128 u)
    {
        Float128 sum = u;
        Float128 term = u;
        Float128 uSquared = u * u;

        for (int k = 1; k <= 80; k++)
        {
            term = term * uSquared;
            Float128 summand = term / (Float128)(2 * k + 1);
            Float128 previous = sum;
            sum = sum + summand;
            if (sum == previous) break;
        }

        return sum;
    }
}
