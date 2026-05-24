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
    /// Sets the scale of the specified <see cref="BigDecimal"/> value.
    /// </summary>
    /// <param name="value">The value to scale.</param>
    /// <param name="scale">The scale to apply to the value.</param>
    /// <param name="mode">The <see cref="MidpointRounding"/> mode to be used when the specified scale is less than the current scale.</param>
    /// <returns>Returns a new <see cref="BigDecimal"/> value with the specified scale.</returns>
    // ReSharper disable once MemberCanBePrivate.Global
    public static BigDecimal SetScale(BigDecimal value, int scale, MidpointRounding mode = default)
    {
        Require(scale >= 0, "Scale must be greater than or equal to zero.", nameof(scale));
        RequireIsDefined(mode);

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
    public BigDecimal SetScale(int scale, MidpointRounding mode = default) => SetScale(this, scale, mode);

    /// <summary>
    /// Normalizes the unscaled values of the specified <see cref="BigDecimal"/> values.
    /// </summary>
    /// <param name="left">The <paramref name="left"/> value to normalize.</param>
    /// <param name="right">The <paramref name="right"/> value to normalize.</param>
    /// <returns>Returns the normalized unscaled values of the specified <see cref="BigDecimal"/> values.</returns>
    public static (BigInteger Left, BigInteger Right) NormalizeUnscaledValues(BigDecimal left, BigDecimal right)
    {
        BigInteger leftNormalized = left.UnscaledValue;
        BigInteger rightNormalized = right.UnscaledValue;

        // Align both values to the larger scale by raising the value with the smaller scale. This needs a single
        // power-of-ten factor, rather than scaling both sides by their full scale factor and dividing by the smaller.
        if (left.Scale < right.Scale) leftNormalized *= BigInteger.Pow(10, right.Scale - left.Scale);
        else if (right.Scale < left.Scale) rightNormalized *= BigInteger.Pow(10, left.Scale - right.Scale);

        return (leftNormalized, rightNormalized);
    }
}
