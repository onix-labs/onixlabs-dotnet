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

using System.Numerics;

namespace OnixLabs.Numerics.UnitTests.Data.CrossValidation;

/// <summary>
/// Provides an exact rational oracle for <see cref="BigDecimal"/> arithmetic. A <see cref="BigDecimal"/> represents
/// the exact value <c>UnscaledValue × 10^(-Scale)</c>, so its value is the rational <c>UnscaledValue / 10^Scale</c>.
/// Comparing results as rationals (via cross-multiplication over <see cref="BigInteger"/>) validates value equality
/// independently of scale/representation and without re-using <see cref="BigDecimal"/>'s own arithmetic.
/// </summary>
public static class BigDecimalRationalOracle
{
    /// <summary>Decomposes the specified value into its exact rational <c>(numerator, denominator)</c>, where the denominator is a power of ten.</summary>
    public static (BigInteger Numerator, BigInteger Denominator) ToRational(BigDecimal value)
        => (value.UnscaledValue, BigInteger.Pow(10, value.Scale));

    /// <summary>Returns <see langword="true"/> if <paramref name="actual"/> equals the rational <paramref name="expectedNumerator"/> / <paramref name="expectedDenominator"/> in value.</summary>
    public static bool ValueEquals(BigDecimal actual, BigInteger expectedNumerator, BigInteger expectedDenominator)
    {
        (BigInteger numerator, BigInteger denominator) = ToRational(actual);
        return numerator * expectedDenominator == expectedNumerator * denominator;
    }
}
