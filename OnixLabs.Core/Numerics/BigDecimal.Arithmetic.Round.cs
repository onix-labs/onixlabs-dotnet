// Copyright 2020-2023 ONIXLabs
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
using System.Numerics;

namespace OnixLabs.Core.Numerics;

public readonly partial struct BigDecimal
{
    /// <summary>
    /// Rounds the specified <see cref="BigDecimal"/> value to the nearest integer, or to the specified scale.
    /// </summary>
    /// <param name="value">The value to be rounded.</param>
    /// <param name="scale">The number of decimal places to round by. The default value is zero.</param>
    /// <param name="mode">The rounding strategy to use. The default value is <see cref="MidpointRounding.ToEven"/>.</param>
    /// <returns>
    /// Returns the specified <see cref="BigDecimal"/> rounded to the specified number of fractional digits. 
    /// If the scale of the specified value is less than the specified scale to round by, the value is returned unchanged.
    /// </returns>
    /// <exception cref="ArgumentException">If the specified scale is non-negative.</exception>
    /// <exception cref="ArgumentException">If the specified rounding mode is invalid.</exception>
    public static BigDecimal Round(BigDecimal value, int scale = 0, MidpointRounding mode = MidpointRounding.ToEven)
    {
        Require(scale >= 0, "Scale must be greater than or equal to zero.", nameof(scale));

        if (value.Scale <= scale) return value;

        BigInteger dividend = BigInteger.Abs(value.UnscaledValue);
        BigInteger divisor = value.OrderOfMagnitude / BigInteger.Pow(10, scale);
        (BigInteger quotient, BigInteger remainder) = BigInteger.DivRem(dividend, divisor);
        BigInteger midpoint = BigInteger.Pow(10, value.Scale - scale - 1) * 5;
        BigInteger sign = value.Sign is 1 ? BigInteger.One : BigInteger.MinusOne;

        bool isIncrementRequired = mode switch
        {
            MidpointRounding.ToZero => false,
            MidpointRounding.AwayFromZero => remainder >= midpoint,
            MidpointRounding.ToPositiveInfinity => remainder > 0 && sign > 0,
            MidpointRounding.ToNegativeInfinity => remainder > 0 && sign < 0,
            MidpointRounding.ToEven => remainder > midpoint || remainder == midpoint && !quotient.IsEven,
            _ => throw new ArgumentOutOfRangeException(nameof(mode), mode,
                "Rounding mode must be ToEven, ToZero, AwayFromZero, ToPositiveInfinity, or ToNegativeInfinity.")
        };

        if (isIncrementRequired) quotient++;

        return new BigDecimal(quotient * sign, scale);
    }

    /// <summary>
    /// Rounds the specified <see cref="BigDecimal"/> value up to the smallest integral value greater than or equal to the specified number.
    /// This kind of rounding is sometimes called rounding towards positive infinity, following IEEE Standard 754, section 4.
    /// </summary>
    /// <param name="value">The value to be rounded up towards positive infinity.</param>
    /// <returns>Returns the smallest integral value that is greater than or equal to the specified <see cref="BigDecimal"/> value.</returns>
    public static BigDecimal Ceiling(BigDecimal value)
    {
        return Round(value, 0, MidpointRounding.ToPositiveInfinity);
    }

    /// <summary>
    /// Rounds the specified <see cref="BigDecimal"/> value down to the largest integral value less than or equal to the specified number.
    /// This kind of rounding is sometimes called rounding towards negative infinity, following IEEE Standard 754, section 4.
    /// </summary>
    /// <param name="value">The value to be rounded down towards positive infinity.</param>
    /// <returns>Returns the largest integral value that is less than or equal to the specified <see cref="BigDecimal"/> value.</returns>
    public static BigDecimal Floor(BigDecimal value)
    {
        return Round(value, 0, MidpointRounding.ToNegativeInfinity);
    }

    /// <summary>
    /// Rounds the current <see cref="BigDecimal"/> value to the nearest integer, or to the specified scale.
    /// </summary>
    /// <param name="scale">The number of decimal places to round by. The default value is zero.</param>
    /// <param name="mode">The rounding strategy to use. The default value is <see cref="MidpointRounding.ToEven"/>.</param>
    /// <returns>
    /// Returns the current <see cref="BigDecimal"/> rounded to the specified number of fractional digits. 
    /// If the scale of the current value is less than the specified scale to round by, the value is returned unchanged.
    /// </returns>
    /// <exception cref="ArgumentException">If the specified scale is non-negative.</exception>
    /// <exception cref="ArgumentException">If the specified rounding mode is invalid.</exception>
    public BigDecimal Round(int scale = 0, MidpointRounding mode = MidpointRounding.ToEven)
    {
        return Round(this, scale, mode);
    }

    /// <summary>
    /// Rounds the current <see cref="BigDecimal"/> value up to the smallest integral value greater than or equal to the current number.
    /// This kind of rounding is sometimes called rounding towards positive infinity, following IEEE Standard 754, section 4.
    /// </summary>
    /// <returns>Returns the smallest integral value that is greater than or equal to the current <see cref="BigDecimal"/> value.</returns>
    public BigDecimal Ceiling()
    {
        return Ceiling(this);
    }

    /// <summary>
    /// Rounds the current <see cref="BigDecimal"/> value down to the largest integral value less than or equal to the current number.
    /// This kind of rounding is sometimes called rounding towards negative infinity, following IEEE Standard 754, section 4.
    /// </summary>
    /// <returns>Returns the largest integral value that is less than or equal to the current <see cref="BigDecimal"/> value.</returns>
    public BigDecimal Floor()
    {
        return Floor(this);
    }
}
