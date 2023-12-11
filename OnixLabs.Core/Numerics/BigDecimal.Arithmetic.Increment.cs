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

namespace OnixLabs.Core.Numerics;

public readonly partial struct BigDecimal
{
    /// <summary>
    /// Increments the integral component of the specified <see cref="BigDecimal"/> value by one.
    /// </summary>
    /// <param name="value">The value to increment.</param>
    /// <returns>Returns a new <see cref="BigDecimal"/> value incremented by one integral unit.</returns>
    public static BigDecimal Increment(BigDecimal value)
    {
        BigInteger power = BigInteger.Pow(10, value.Scale);
        (BigInteger quotient, BigInteger remainder) = BigInteger.DivRem(value.UnscaledValue, power);

        return new BigDecimal((quotient + 1) * power + remainder, value.Scale);
    }

    /// <summary>
    /// Increments the fractional component of the specified <see cref="BigDecimal"/> value by one.
    /// </summary>
    /// <param name="value">The value to increment.</param>
    /// <returns>Returns a new <see cref="BigDecimal"/> value incremented by one fractional unit.</returns>
    public static BigDecimal IncrementFraction(BigDecimal value)
    {
        return new BigDecimal(value.UnscaledValue + 1, value.Scale);
    }

    /// <summary>
    /// Increments the integral component of the specified <see cref="BigDecimal"/> value by one.
    /// </summary>
    /// <param name="value">The value to increment.</param>
    /// <returns>Returns a new <see cref="BigDecimal"/> value incremented by one integral unit.</returns>
    public static BigDecimal operator ++(BigDecimal value)
    {
        return Increment(value);
    }
}
