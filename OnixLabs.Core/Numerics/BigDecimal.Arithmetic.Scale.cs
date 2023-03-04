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

using System.Numerics;

namespace OnixLabs.Core.Numerics;

public readonly partial struct BigDecimal
{
    /// <summary>
    /// Applies the specified scale to the specified <see cref="BigDecimal"/> value.
    /// </summary>
    /// <param name="value">The <see cref="BigDecimal"/> value for which to apply the specified scale.</param>
    /// <param name="scale">The scale value to apply to the new <see cref="BigDecimal"/> value.</param>
    /// <returns>Returns a new <see cref="BigDecimal"/> value with the specified scale.</returns>
    public static BigDecimal SetScale(BigDecimal value, int scale)
    {
        return value.SetScale(scale);
    }

    /// <summary>
    /// Applies the specified scale to the current <see cref="BigDecimal"/> value absolutely.
    /// </summary>
    /// <param name="value">The <see cref="BigDecimal"/> value for which to apply the specified absolute scale.</param>
    /// <param name="scale">The absolute scale value to apply to the new <see cref="BigDecimal"/> value.</param>
    /// <returns>Returns a new <see cref="BigDecimal"/> value with the specified scale.</returns>
    public static BigDecimal SetScaleAbsolute(BigDecimal value, int scale)
    {
        return value.SetScaleAbsolute(scale);
    }

    /// <summary>
    /// Truncates the fractional part of the specified <see cref="BigDecimal"/> value, leaving only the integral component.
    /// </summary>
    /// <param name="value">The value to truncate.</param>
    /// <returns>Returns the fractional part of the specified <see cref="BigDecimal"/> value, leaving only the integral component.</returns>
    public static BigDecimal Truncate(BigDecimal value)
    {
        return value.Truncate();
    }

    /// <summary>
    /// Applies the specified scale to the current <see cref="BigDecimal"/> value.
    /// </summary>
    /// <param name="scale">The scale value to apply to the new <see cref="BigDecimal"/> value.</param>
    /// <returns>Returns a new <see cref="BigDecimal"/> value with the specified scale.</returns>
    public BigDecimal SetScale(int scale)
    {
        return new BigDecimal(UnscaledValue, scale);
    }

    /// <summary>
    /// Applies the specified scale to the current <see cref="BigDecimal"/> value absolutely.
    /// </summary>
    /// <param name="scale">The absolute scale value to apply to the new <see cref="BigDecimal"/> value.</param>
    /// <returns>Returns a new <see cref="BigDecimal"/> value with the specified scale.</returns>
    public BigDecimal SetScaleAbsolute(int scale)
    {
        return new BigDecimal(UnscaledValue * BigInteger.Pow(10, scale), scale);
    }

    /// <summary>
    /// Truncates the fractional part of the current <see cref="BigDecimal"/> value, leaving only the integral component.
    /// </summary>
    /// <returns>Returns the fractional part of the current <see cref="BigDecimal"/> value, leaving only the integral component.</returns>
    public BigDecimal Truncate()
    {
        return new BigDecimal(IntegralValue, 0);
    }
}
