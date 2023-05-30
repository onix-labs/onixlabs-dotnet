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
    /// Obtains the minimum scale of the specified left-hand and right-hand <see cref="BigDecimal"/> values.
    /// </summary>
    /// <param name="left">The left-hand value from which to obtain the minimum scale value.</param>
    /// <param name="right">The left-hand value from which to obtain the minimum scale value.</param>
    /// <returns>Returns the minimum scale of the specified left-hand and right-hand <see cref="BigDecimal"/> values.</returns>
    public static int MinScale(BigDecimal left, BigDecimal right)
    {
        return int.Min(left.Scale, right.Scale);
    }

    /// <summary>
    /// Obtains the maximum scale of the specified left-hand and right-hand <see cref="BigDecimal"/> values.
    /// </summary>
    /// <param name="left">The left-hand value from which to obtain the maximum scale value.</param>
    /// <param name="right">The left-hand value from which to obtain the maximum scale value.</param>
    /// <returns>Returns the maximum scale of the specified left-hand and right-hand <see cref="BigDecimal"/> values.</returns>
    public static int MaxScale(BigDecimal left, BigDecimal right)
    {
        return int.Max(left.Scale, right.Scale);
    }

    /// <summary>
    /// Obtains the minimum and maximum scale of the specified left-hand and right-hand <see cref="BigDecimal"/> values.
    /// </summary>
    /// <param name="left">The left-hand value from which to obtain the minimum and maximum values.</param>
    /// <param name="right">The left-hand value from which to obtain the minimum and maximum values.</param>
    /// <returns>Returns the minimum and maximum of the specified left-hand and right-hand <see cref="BigDecimal"/> values.</returns>
    public static (int Min, int Max) MinMaxScale(BigDecimal left, BigDecimal right)
    {
        int min = MinScale(left, right);
        int max = MaxScale(left, right);

        return (min, max);
    }

    /// <summary>
    /// Applies the specified scale to the specified <see cref="BigDecimal"/> value.
    /// </summary>
    /// <param name="value">The <see cref="BigDecimal"/> value to scale.</param>
    /// <param name="scale">The scale to apply to the specified <see cref="BigDecimal"/> value.</param>
    /// <param name="mode">
    /// The scale mode that specifies the type of scaling to apply.
    /// The default value is <see cref="ScaleMode.Fixed"/>.
    /// </param>
    /// <param name="rounding">
    /// The rounding mode that specifies the type of rounding to apply.
    /// This is applied for <see cref="ScaleMode.Fixed"/>, where the desired scale is less than the original scale.
    /// For <see cref="ScaleMode.Floating"/> and <see cref="ScaleMode.Absolute"/> this parameter is ignored.
    /// The default value is <see cref="MidpointRounding.ToEven"/>.
    /// </param>
    /// <returns>Returns a new <see cref="BigDecimal"/> value with the specified scale.</returns>
    /// <exception cref="ArgumentOutOfRangeException">If the specified scale mode is invalid.</exception>
    public static BigDecimal SetScale(BigDecimal value, int scale, ScaleMode mode = 0, MidpointRounding rounding = 0)
    {
        return mode switch
        {
            ScaleMode.Fixed => SetScaleFixed(value, scale, rounding),
            ScaleMode.Floating => SetScaleFloating(value, scale),
            ScaleMode.Absolute => SetScaleAbsolute(value, scale),
            _ => throw new ArgumentOutOfRangeException(nameof(mode), mode, "Scale mode must be Fixed, Floating, or Absolute.")
        };
    }

    /// <summary>
    /// Applies the specified fixed-point scale to the specified <see cref="BigDecimal"/> value.
    /// </summary>
    /// <param name="value">The <see cref="BigDecimal"/> value to scale.</param>
    /// <param name="scale">The fixed-point scale to apply to the specified <see cref="BigDecimal"/> value.</param>
    /// <param name="mode">
    /// The rounding mode that specifies the type of rounding to apply.
    /// This is applied for <see cref="ScaleMode.Fixed"/>, where the desired scale is less than the original scale.
    /// The default value is <see cref="MidpointRounding.ToEven"/>.
    /// </param>
    /// <returns>Returns a new <see cref="BigDecimal"/> value with the specified fixed-point scale.</returns>
    public static BigDecimal SetScaleFixed(BigDecimal value, int scale, MidpointRounding mode = MidpointRounding.ToEven)
    {
        Require(scale >= 0, "Scale must be greater than or equal to zero.", nameof(scale));

        if (value.Scale == scale) return value;
        if (value.Scale > scale) return Round(value, scale, mode);

        int delta = GenericMath.Delta(value.Scale, scale);
        BigInteger pow = BigInteger.Pow(10, delta);
        BigInteger unscaledValue = value.UnscaledValue * pow;

        return new BigDecimal(unscaledValue, scale);
    }

    /// <summary>
    /// Applies the specified floating-point scale to the specified <see cref="BigDecimal"/> value.
    /// </summary>
    /// <param name="value">The <see cref="BigDecimal"/> value to scale.</param>
    /// <param name="scale">The floating-point scale to apply to the specified <see cref="BigDecimal"/> value.</param>
    /// <returns>Returns a new <see cref="BigDecimal"/> value with the specified floating-point scale.</returns>
    public static BigDecimal SetScaleFloating(BigDecimal value, int scale)
    {
        Require(scale >= 0, "Scale must be greater than or equal to zero.", nameof(scale));
        return value.Scale == scale ? value : new BigDecimal(value.UnscaledValue, scale);
    }

    /// <summary>
    /// Applies the specified absolute scale to the specified <see cref="BigDecimal"/> value.
    /// </summary>
    /// <param name="value">The <see cref="BigDecimal"/> value to scale.</param>
    /// <param name="scale">The absolute scale to apply to the specified <see cref="BigDecimal"/> value.</param>
    /// <returns>Returns a new <see cref="BigDecimal"/> value with the specified absolute scale.</returns>
    public static BigDecimal SetScaleAbsolute(BigDecimal value, int scale)
    {
        Require(scale >= 0, "Scale must be greater than or equal to zero.", nameof(scale));

        BigInteger pow = BigInteger.Pow(10, scale);
        BigInteger unscaledValue = value.UnscaledValue * pow;

        return new BigDecimal(unscaledValue, scale);
    }

    /// <summary>
    /// Applies the specified scale to the current <see cref="BigDecimal"/> value.
    /// </summary>
    /// <param name="scale">The scale to apply to the current <see cref="BigDecimal"/> value.</param>
    /// <param name="mode">
    /// The scale mode that specifies the type of scaling to apply.
    /// The default value is <see cref="ScaleMode.Fixed"/>.
    /// </param>
    /// <param name="rounding">
    /// The rounding mode that specifies the type of rounding to apply.
    /// This is applied for <see cref="ScaleMode.Fixed"/>, where the desired scale is less than the original scale.
    /// For <see cref="ScaleMode.Floating"/> and <see cref="ScaleMode.Absolute"/> this parameter is ignored.
    /// The default value is <see cref="MidpointRounding.ToEven"/>.
    /// </param>
    /// <returns>Returns a new <see cref="BigDecimal"/> value with the specified scale.</returns>
    /// <exception cref="ArgumentOutOfRangeException">If the specified scale mode is invalid.</exception>
    public BigDecimal SetScale(int scale, ScaleMode mode = ScaleMode.Fixed, MidpointRounding rounding = MidpointRounding.ToEven)
    {
        return SetScale(this, scale, mode, rounding);
    }

    /// <summary>
    /// Applies the specified fixed-point scale to the current <see cref="BigDecimal"/> value.
    /// </summary>
    /// <param name="scale">The fixed-point scale to apply to the current <see cref="BigDecimal"/> value.</param>
    /// <param name="mode">
    /// The rounding mode that specifies the type of rounding to apply.
    /// This is applied for <see cref="ScaleMode.Fixed"/>, where the desired scale is less than the original scale.
    /// The default value is <see cref="MidpointRounding.ToEven"/>.
    /// </param>
    /// <returns>Returns a new <see cref="BigDecimal"/> value with the specified fixed-point scale.</returns>
    public BigDecimal SetScaleFixed(int scale, MidpointRounding mode = MidpointRounding.ToEven)
    {
        return SetScaleFixed(this, scale, mode);
    }

    /// <summary>
    /// Applies the specified floating-point scale to the current <see cref="BigDecimal"/> value.
    /// </summary>
    /// <param name="scale">The floating-point scale to apply to the current <see cref="BigDecimal"/> value.</param>
    /// <returns>Returns a new <see cref="BigDecimal"/> value with the specified floating-point scale.</returns>
    public BigDecimal SetScaleFloating(int scale)
    {
        return SetScaleFloating(this, scale);
    }

    /// <summary>
    /// Applies the specified absolute scale to the current <see cref="BigDecimal"/> value.
    /// </summary>
    /// <param name="scale">The absolute scale to apply to the current <see cref="BigDecimal"/> value.</param>
    /// <returns>Returns a new <see cref="BigDecimal"/> value with the specified absolute scale.</returns>
    public BigDecimal SetScaleAbsolute(int scale)
    {
        return SetScaleAbsolute(this, scale);
    }

    /// <summary>
    /// Normalizes the scale of the specified left and right <see cref="BigDecimal"/> values.
    /// </summary>
    /// <param name="left">The left-hand value to normalize.</param>
    /// <param name="right">The right-hand value to normalize.</param>
    /// <returns>Returns the normalized scale of the specified left and right <see cref="BigDecimal"/> values.</returns>
    private static (BigDecimal Left, BigDecimal Right) NormalizeScale(BigDecimal left, BigDecimal right)
    {
        int scale = MaxScale(left, right);
        (BigInteger leftUnscaled, BigInteger rightUnscaled) = NormalizeUnscaledOrderOfMagnitude(left, right);
        BigDecimal leftNormalized = new(leftUnscaled, scale);
        BigDecimal rightNormalized = new(rightUnscaled, scale);

        return (leftNormalized, rightNormalized);
    }

    /// <summary>
    /// Normalizes the order of magnitude of the specified left and right <see cref="BigDecimal"/> unscaled values.
    /// </summary>
    /// <param name="left">The left-hand value to normalize.</param>
    /// <param name="right">The right-hand value to normalize.</param>
    /// <returns>Returns the normalized order of magnitude of the left and right <see cref="BigDecimal"/> unscaled values.</returns>
    private static (BigInteger Left, BigInteger Right) NormalizeUnscaledOrderOfMagnitude(BigDecimal left, BigDecimal right)
    {
        BigInteger minOrderOfMagnitude = BigInteger.Min(left.OrderOfMagnitude, right.OrderOfMagnitude);
        BigInteger leftNormalized = left.UnscaledValue * right.OrderOfMagnitude / minOrderOfMagnitude;
        BigInteger rightNormalized = right.UnscaledValue * left.OrderOfMagnitude / minOrderOfMagnitude;

        return (leftNormalized, rightNormalized);
    }
}
