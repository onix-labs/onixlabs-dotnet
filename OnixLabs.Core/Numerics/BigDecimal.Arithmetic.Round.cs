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
using static OnixLabs.Core.Preconditions;

namespace OnixLabs.Core.Numerics;

public readonly partial struct BigDecimal
{
    /// <summary>
    /// Rounds the specified <see cref="BigDecimal"/> value to the nearest integer, or to the specified scale.
    /// </summary>
    /// <param name="value">The value to be rounded.</param>
    /// <param name="scale">The number of decimal places to round by. The default value is zero.</param>
    /// <param name="rounding">The rounding strategy to use. The default value is <see cref="MidpointRounding.ToEven"/>.</param>
    /// <returns>
    /// Returns the specified <see cref="BigDecimal"/> rounded to the specified number of fractional digits. 
    /// If the scale of the specified value is less than the specified scale to round by, the value is returned unchanged.
    /// </returns>
    /// <exception cref="ArgumentException">If the specified scale is non-negative.</exception>
    /// <exception cref="ArgumentException">If the specified rounding mode is invalid.</exception>
    public static BigDecimal Round(BigDecimal value, int scale = 0, MidpointRounding rounding = MidpointRounding.ToEven)
    {
        Check(scale >= 0, "BigDecimal scale cannot be non-negative.");

        if (value.Scale <= scale)
        {
            return value;
        }

        BigInteger divisor = value.ScaleFactor / BigInteger.Pow(Ten.UnscaledValue, scale);
        BigInteger quotient = BigInteger.DivRem(BigInteger.Abs(value.UnscaledValue), divisor, out BigInteger remainder);
        BigInteger midpoint = BigInteger.Pow(Ten.UnscaledValue, value.Scale - scale - 1) * 5;
        BigInteger sign = value.Sign is 1 ? BigInteger.One : BigInteger.MinusOne;

        bool isAddOneRequired = rounding switch
        {
            MidpointRounding.ToZero => false,
            MidpointRounding.AwayFromZero => remainder >= midpoint,
            MidpointRounding.ToPositiveInfinity => remainder > 0 && sign > 0,
            MidpointRounding.ToNegativeInfinity => remainder > 0 && sign < 0,
            MidpointRounding.ToEven => remainder > midpoint || remainder == midpoint && !quotient.IsEven,
            _ => throw new ArgumentException($"The specified value is not a valid MidpointRounding mode: {rounding}", nameof(rounding))
        };

        return isAddOneRequired ? new BigDecimal((quotient + BigInteger.One) * sign, scale) : new BigDecimal(quotient * sign, scale);
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
}
