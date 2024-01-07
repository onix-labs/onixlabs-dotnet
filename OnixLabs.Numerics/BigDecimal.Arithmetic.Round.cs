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
using System.Numerics;

namespace OnixLabs.Numerics;

public readonly partial struct BigDecimal
{
    /// <summary>
    /// Rounds the specified <see cref="BigDecimal"/> value.
    /// </summary>
    /// <param name="value">The value to round.</param>
    /// <param name="scale">The number of decimal places to round by. The default value is zero.</param>
    /// <param name="mode">The rounding mode to round by. The default value is <see cref="MidpointRounding.ToEven"/>.</param>
    /// <returns>Returns a <see cref="BigDecimal"/> value rounded to the specified number of fractional digits.</returns>
    /// <exception cref="ArgumentException">If the specified scale is non-negative.</exception>
    /// <exception cref="ArgumentException">If the specified rounding mode is invalid.</exception>
    public static BigDecimal Round(BigDecimal value, int scale = default, MidpointRounding mode = default)
    {
        Require(scale >= 0, "Scale must be greater than or equal to zero.", nameof(scale));
        RequireIsDefined(mode, nameof(mode));

        if (scale >= value.NumberInfo.Scale) return value;

        BigInteger divisor = BigInteger.Pow(10, value.NumberInfo.Scale - scale);
        BigInteger quotient = DivideAndRound(value.NumberInfo.UnscaledValue, divisor, mode);

        return new BigDecimal(quotient, scale);
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
    /// Rounds the current <see cref="BigDecimal"/> value.
    /// </summary>
    /// <param name="scale">The number of decimal places to round by. The default value is zero.</param>
    /// <param name="mode">The rounding mode to round by. The default value is <see cref="MidpointRounding.ToEven"/>.</param>
    /// <returns>Returns a <see cref="BigDecimal"/> value rounded to the specified number of fractional digits.</returns>
    /// <exception cref="ArgumentException">If the specified scale is non-negative.</exception>
    /// <exception cref="ArgumentException">If the specified rounding mode is invalid.</exception>
    public BigDecimal Round(int scale = default, MidpointRounding mode = default)
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
