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

/// <summary>
/// Represents an arbitrarily large signed decimal.
/// </summary>
public readonly partial struct BigDecimal : IFloatingPoint<BigDecimal>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BigDecimal"/> struct.
    /// </summary>
    /// <param name="value">The unscaled integer value from which to construct a <see cref="BigDecimal"/> value.</param>
    /// <param name="scale">The scale of the <see cref="BigDecimal"/> value.</param>
    /// <param name="mode">The scale mode that determines how the specified value should be scaled.</param>
    public BigDecimal(BigInteger value, int scale = default, ScaleMode mode = default)
    {
        Require(scale >= 0, "Scale must be greater than or equal to zero.", nameof(value));
        RequireIsDefined(mode, nameof(mode));

        UnscaledValue = value.GetUnscaledInteger(scale, mode);
        Scale = scale;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BigDecimal"/> struct.
    /// </summary>
    /// <param name="value">The floating-point value from which to construct a <see cref="BigDecimal"/>value.</param>
    /// <param name="mode">The conversion mode that determines whether the floating-point value should be converted from its binary or decimal representation.</param>
    public BigDecimal(float value, ConversionMode mode = default)
    {
        RequireIsDefined(mode, nameof(mode));
        (UnscaledValue, Scale) = Ieee754Converter.Convert(value, mode);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BigDecimal"/> struct.
    /// </summary>
    /// <param name="value">The floating-point value from which to construct a <see cref="BigDecimal"/>value.</param>
    /// <param name="mode">The conversion mode that determines whether the floating-point value should be converted from its binary or decimal representation.</param>
    public BigDecimal(double value, ConversionMode mode = default)
    {
        RequireIsDefined(mode, nameof(mode));
        (UnscaledValue, Scale) = Ieee754Converter.Convert(value, mode);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BigDecimal"/> struct.
    /// </summary>
    /// <param name="value">The decimal value from which to construct a <see cref="BigDecimal"/>value.</param>
    public BigDecimal(decimal value) : this(value.GetUnscaledValue(), value.Scale)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BigDecimal"/> struct.
    /// </summary>
    /// <param name="value">The bytes from which to construct a <see cref="BigDecimal"/>value.</param>
    public BigDecimal(ReadOnlySpan<byte> value) : this(new BigInteger(value[..^4]), BitConverter.ToInt32(value[^4..]))
    {
    }

    /// <summary>
    /// Gets the integral, unscaled value of the current <see cref="BigDecimal"/> value.
    /// </summary>
    public BigInteger UnscaledValue { get; }

    /// <summary>
    /// Gets the scale of the current <see cref="BigDecimal"/> value.
    /// </summary>
    public int Scale { get; }

    /// <summary>
    /// Gets a number that indicates the sign (negative, positive, or zero) of the current <see cref="BigDecimal"/> value. 
    /// </summary>
    public int Sign => UnscaledValue.Sign;

    /// <summary>
    /// Gets the integral value of the current <see cref="BigDecimal"/> value.
    /// </summary>
    internal BigInteger IntegralValue => UnscaledValue / ScaleMagnitude;

    /// <summary>
    /// Gets the fractional value of the current <see cref="BigDecimal"/> value.
    /// </summary>
    internal BigInteger FractionalValue => BigInteger.Abs(UnscaledValue - IntegralValue * ScaleMagnitude);

    /// <summary>
    /// Gets the scale factor of the current <see cref="BigDecimal"/>, which is 10 raised to the power of its scale.
    /// </summary>
    private BigInteger ScaleMagnitude => BigInteger.Pow(10, Scale);
}
