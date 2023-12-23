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

namespace OnixLabs.Core.Numerics;

public readonly partial struct BigDecimal
{
    /// <summary>
    /// Sets the scale of the specified <see cref="BigDecimal"/> value.
    /// </summary>
    /// <param name="value">The value to scale.</param>
    /// <param name="scale">The scale to apply to the value.</param>
    /// <param name="mode">The <see cref="MidpointRounding"/> mode to be used when the specified scale is less than the current scale.</param>
    /// <returns>Returns a new <see cref="BigDecimal"/> value with the specified scale.</returns>
    public static BigDecimal SetScale(BigDecimal value, int scale, MidpointRounding mode = default)
    {
        Require(scale >= 0, "Scale must be greater than or equal to zero.", nameof(scale));
        RequireIsDefined(mode, nameof(mode));

        if (scale == value.Scale) return value;
        if (scale < value.Scale) return Round(value, scale, mode);

        BigInteger magnitude = BigInteger.Pow(10, scale - value.Scale);
        return new BigDecimal(value.UnscaledValue * magnitude, scale);
    }

    /// <summary>
    /// Sets the scale of the specified <see cref="BigDecimal"/> value.
    /// </summary>
    /// <param name="scale">The scale to apply to the value.</param>
    /// <param name="mode">The <see cref="MidpointRounding"/> mode to be used when the specified scale is less than the current scale.</param>
    /// <returns>Returns a new <see cref="BigDecimal"/> value with the specified scale.</returns>
    public BigDecimal SetScale(int scale, MidpointRounding mode = default)
    {
        return SetScale(this, scale, mode);
    }

    /// <summary>
    /// Normalizes the unscaled values of the specified <see cref="BigDecimal"/> values.
    /// </summary>
    /// <param name="left">The left-hand value to normalize.</param>
    /// <param name="right">The right-hand value to normalize.</param>
    /// <returns>Returns the normalized unscaled values of the specified <see cref="BigDecimal"/> values.</returns>
    internal static (BigInteger Left, BigInteger Right) NormalizeUnscaledValues(BigDecimal left, BigDecimal right)
    {
        BigInteger minOrderOfMagnitude = BigInteger.Min(left.ScaleMagnitude, right.ScaleMagnitude);
        BigInteger leftNormalized = left.UnscaledValue * right.ScaleMagnitude / minOrderOfMagnitude;
        BigInteger rightNormalized = right.UnscaledValue * left.ScaleMagnitude / minOrderOfMagnitude;

        return (leftNormalized, rightNormalized);
    }
}
