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

namespace OnixLabs.Numerics;

public readonly partial struct BigDecimal
{
    /// <summary>
    /// Determines if a value represents an integral number.
    /// </summary>
    /// <param name="value">The value to be checked.</param>
    /// <returns>Returns <see langword="true"/> if the value is an integer; otherwise, <see langword="false"/>.</returns>
    public static bool IsInteger(BigDecimal value) => value.number.Fraction == BigInteger.Zero;

    /// <summary>
    /// Determines if a value represents an even integral number.
    /// </summary>
    /// <param name="value">The value to be checked.</param>
    /// <returns>Returns <see langword="true"/> if the value is an even integer; otherwise, <see langword="false"/>.</returns>
    public static bool IsEvenInteger(BigDecimal value) => IsInteger(value) && BigInteger.IsEvenInteger(value.number.Integer);

    /// <summary>
    /// Determines if a value represents an odd integral number.
    /// </summary>
    /// <param name="value">The value to be checked.</param>
    /// <returns>Returns <see langword="true"/> if the value is an odd integer; otherwise, <see langword="false"/>.</returns>
    public static bool IsOddInteger(BigDecimal value) => IsInteger(value) && BigInteger.IsOddInteger(value.number.Integer);

    /// <summary>
    /// Determines if a value is negative.
    /// </summary>
    /// <param name="value">The value to be checked.</param>
    /// <returns>Returns <see langword="true"/> if the value is negative; otherwise, <see langword="false"/>.</returns>
    public static bool IsNegative(BigDecimal value) => BigInteger.IsNegative(value.UnscaledValue);

    /// <summary>
    /// Determines if a value is positive.
    /// </summary>
    /// <param name="value">The value to be checked.</param>
    /// <returns>Returns <see langword="true"/> if the value is positive; otherwise, <see langword="false"/>.</returns>
    public static bool IsPositive(BigDecimal value) => BigInteger.IsPositive(value.UnscaledValue);

    /// <summary>
    /// Determines if a value is zero.
    /// </summary>
    /// <param name="value">The value to be checked.</param>
    /// <returns>Returns <see langword="true"/> if the value is zero; otherwise, <see langword="false"/>.</returns>
    public static bool IsZero(BigDecimal value) => value.UnscaledValue == BigInteger.Zero;

    /// <summary>
    /// Determines if a value is in its canonical representation.
    /// </summary>
    /// <param name="value">The value to be checked.</param>
    /// <returns>Returns <see langword="true"/> if value is in its canonical representation; otherwise, <see langword="false"/>.</returns>
    static bool INumberBase<BigDecimal>.IsCanonical(BigDecimal value) => true;

    /// <summary>
    /// Determines if a value represents a complex number.
    /// </summary>
    /// <param name="value">The value to be checked.</param>
    /// <returns>Returns <see langword="true"/> if value is a complex number; otherwise, <see langword="false"/>.</returns>
    static bool INumberBase<BigDecimal>.IsComplexNumber(BigDecimal value) => false;

    /// <summary>
    /// Determines if a value is finite.
    /// </summary>
    /// <param name="value">The value to be checked.</param>
    /// <returns>Returns <see langword="true"/> if value is finite; otherwise, <see langword="false"/>.</returns>
    static bool INumberBase<BigDecimal>.IsFinite(BigDecimal value) => true;

    /// <summary>
    /// Determines if a value represents a pure imaginary number.
    /// </summary>
    /// <param name="value">The value to be checked.</param>
    /// <returns>Returns <see langword="true"/> if value is a pure imaginary number; otherwise, <see langword="false"/>.</returns>
    static bool INumberBase<BigDecimal>.IsImaginaryNumber(BigDecimal value) => false;

    /// <summary>
    /// Determines if a value is infinite.
    /// </summary>
    /// <param name="value">The value to be checked.</param>
    /// <returns>Returns <see langword="true"/> if value is infinite; otherwise, <see langword="false"/>.</returns>
    static bool INumberBase<BigDecimal>.IsInfinity(BigDecimal value) => false;

    /// <summary>
    /// Determines if a value is NaN.
    /// </summary>
    /// <param name="value">The value to be checked.</param>
    /// <returns>Returns <see langword="true"/> if value is NaN; otherwise, <see langword="false"/>.</returns>
    static bool INumberBase<BigDecimal>.IsNaN(BigDecimal value) => false;

    /// <summary>
    /// Determines if a value is negative infinity.
    /// </summary>
    /// <param name="value">The value to be checked.</param>
    /// <returns>Returns <see langword="true"/> if value is negative infinity; otherwise, <see langword="false"/>.</returns>
    static bool INumberBase<BigDecimal>.IsNegativeInfinity(BigDecimal value) => false;

    /// <summary>
    /// Determines if a value is normal.
    /// </summary>
    /// <param name="value">The value to be checked.</param>
    /// <returns>Returns <see langword="true"/> if value is normal; otherwise, <see langword="false"/>.</returns>
    static bool INumberBase<BigDecimal>.IsNormal(BigDecimal value) => true;

    /// <summary>
    /// Determines if a value is positive infinity.
    /// </summary>
    /// <param name="value">The value to be checked.</param>
    /// <returns>Returns <see langword="true"/> if value is positive infinity; otherwise, <see langword="false"/>.</returns>
    static bool INumberBase<BigDecimal>.IsPositiveInfinity(BigDecimal value) => false;

    /// <summary>
    /// Determines if a value represents a real number.
    /// </summary>
    /// <param name="value">The value to be checked.</param>
    /// <returns>Returns <see langword="true"/> if value is a real number; otherwise, <see langword="false"/></returns>
    static bool INumberBase<BigDecimal>.IsRealNumber(BigDecimal value) => true;

    /// <summary>
    /// Determines if a value is subnormal.
    /// </summary>
    /// <param name="value">The value to be checked.</param>
    /// <returns>Returns <see langword="true"/> if value is subnormal; otherwise, <see langword="false"/>.</returns>
    static bool INumberBase<BigDecimal>.IsSubnormal(BigDecimal value) => false;
}
