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
    /// Decrements the integral component of the specified <see cref="BigDecimal"/> value by one.
    /// </summary>
    /// <param name="value">The value to decrement.</param>
    /// <returns>Returns a new <see cref="BigDecimal"/> value decremented by one integral unit.</returns>
    public static BigDecimal Decrement(BigDecimal value)
    {
        return new BigDecimal(value.NumberInfo.UnscaledValue - value.NumberInfo.ScaleFactor, value.NumberInfo.Scale);
    }

    /// <summary>
    /// Decrements the fractional component of the specified <see cref="BigDecimal"/> value by one.
    /// </summary>
    /// <param name="value">The value to decrement.</param>
    /// <returns>Returns a new <see cref="BigDecimal"/> value decremented by one fractional unit.</returns>
    public static BigDecimal DecrementFraction(BigDecimal value)
    {
        return new BigDecimal(value.NumberInfo.UnscaledValue - BigInteger.One, value.NumberInfo.Scale);
    }

    /// <summary>
    /// Decrements the integral component of the specified <see cref="BigDecimal"/> value by one.
    /// </summary>
    /// <param name="value">The value to decrement.</param>
    /// <returns>Returns a new <see cref="BigDecimal"/> value decremented by one integral unit.</returns>
    public static BigDecimal operator --(BigDecimal value)
    {
        return Decrement(value);
    }
}
