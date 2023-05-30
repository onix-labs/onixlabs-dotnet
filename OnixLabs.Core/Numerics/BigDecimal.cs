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

/// <summary>
/// Represents an arbitrarily large signed decimal.
/// </summary>
public readonly partial struct BigDecimal : IFloatingPoint<BigDecimal>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BigDecimal"/> struct.
    /// </summary>
    /// <param name="value">The <see cref="T:byte[]"/> value from which to construct a new <see cref="BigDecimal"/> instance.</param>
    public BigDecimal(ReadOnlySpan<byte> value)
    {
        const int scaleBytesLength = sizeof(int);
        int unscaledValueBytesLength = value.Length - scaleBytesLength;

        ReadOnlySpan<byte> unscaledValueBytes = value[..unscaledValueBytesLength];
        BigInteger unscaledValue = new(unscaledValueBytes);

        ReadOnlySpan<byte> scaleBytes = value.Slice(unscaledValueBytesLength, scaleBytesLength);
        int scale = BitConverter.ToInt32(scaleBytes);

        Require(scale >= 0, "Scale must be greater than or equal to zero.", nameof(value));

        UnscaledValue = unscaledValue;
        Scale = scale;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BigDecimal"/> struct.
    /// </summary>
    /// <param name="unscaledValue">The unscaled value from which to construct a new <see cref="BigDecimal"/> instance.</param>
    /// <param name="scale">The scale from which to construct a new <see cref="BigDecimal"/> instance.</param>
    public BigDecimal(BigInteger unscaledValue, int scale)
    {
        Require(scale >= 0, "Scale must be greater than or equal to zero.", nameof(scale));

        UnscaledValue = unscaledValue;
        Scale = scale;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BigDecimal"/> struct.
    /// </summary>
    /// <param name="unscaledValue">The unscaled value from which to construct a new <see cref="BigDecimal"/> instance.</param>
    /// <param name="scale">The scale from which to construct a new <see cref="BigDecimal"/> instance.</param>
    public BigDecimal(sbyte unscaledValue, int scale) : this(unscaledValue.ToBigInteger(), scale)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BigDecimal"/> struct.
    /// </summary>
    /// <param name="unscaledValue">The unscaled value from which to construct a new <see cref="BigDecimal"/> instance.</param>
    /// <param name="scale">The scale from which to construct a new <see cref="BigDecimal"/> instance.</param>
    public BigDecimal(byte unscaledValue, int scale) : this(unscaledValue.ToBigInteger(), scale)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BigDecimal"/> struct.
    /// </summary>
    /// <param name="unscaledValue">The unscaled value from which to construct a new <see cref="BigDecimal"/> instance.</param>
    /// <param name="scale">The scale from which to construct a new <see cref="BigDecimal"/> instance.</param>
    public BigDecimal(short unscaledValue, int scale) : this(unscaledValue.ToBigInteger(), scale)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BigDecimal"/> struct.
    /// </summary>
    /// <param name="unscaledValue">The unscaled value from which to construct a new <see cref="BigDecimal"/> instance.</param>
    /// <param name="scale">The scale from which to construct a new <see cref="BigDecimal"/> instance.</param>
    public BigDecimal(ushort unscaledValue, int scale) : this(unscaledValue.ToBigInteger(), scale)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BigDecimal"/> struct.
    /// </summary>
    /// <param name="unscaledValue">The unscaled value from which to construct a new <see cref="BigDecimal"/> instance.</param>
    /// <param name="scale">The scale from which to construct a new <see cref="BigDecimal"/> instance.</param>
    public BigDecimal(int unscaledValue, int scale) : this(unscaledValue.ToBigInteger(), scale)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BigDecimal"/> struct.
    /// </summary>
    /// <param name="unscaledValue">The unscaled value from which to construct a new <see cref="BigDecimal"/> instance.</param>
    /// <param name="scale">The scale from which to construct a new <see cref="BigDecimal"/> instance.</param>
    public BigDecimal(uint unscaledValue, int scale) : this(unscaledValue.ToBigInteger(), scale)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BigDecimal"/> struct.
    /// </summary>
    /// <param name="unscaledValue">The unscaled value from which to construct a new <see cref="BigDecimal"/> instance.</param>
    /// <param name="scale">The scale from which to construct a new <see cref="BigDecimal"/> instance.</param>
    public BigDecimal(long unscaledValue, int scale) : this(unscaledValue.ToBigInteger(), scale)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BigDecimal"/> struct.
    /// </summary>
    /// <param name="unscaledValue">The unscaled value from which to construct a new <see cref="BigDecimal"/> instance.</param>
    /// <param name="scale">The scale from which to construct a new <see cref="BigDecimal"/> instance.</param>
    public BigDecimal(ulong unscaledValue, int scale) : this(unscaledValue.ToBigInteger(), scale)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BigDecimal"/> struct.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="BigDecimal"/> instance.</param>
    /// <param name="mode">Specifies the conversion mode from <see cref="double"/> to <see cref="BigDecimal"/>.</param>
    public BigDecimal(float value, ConversionMode mode = ConversionMode.Decimal)
    {
        (UnscaledValue, Scale) = Ieee754Converter.Convert(value, mode);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BigDecimal"/> struct.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="BigDecimal"/> instance.</param>
    /// <param name="mode">Specifies the conversion mode from <see cref="double"/> to <see cref="BigDecimal"/>.</param>
    public BigDecimal(double value, ConversionMode mode = ConversionMode.Decimal)
    {
        (UnscaledValue, Scale) = Ieee754Converter.Convert(value, mode);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BigDecimal"/> struct.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="BigDecimal"/> instance.</param>
    public BigDecimal(decimal value) : this(value.GetUnscaledValue(), value.Scale)
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
    public BigInteger IntegralValue => AbsoluteUnscaledValue / OrderOfMagnitude * Sign;

    /// <summary>
    /// Gets the fractional value of the current <see cref="BigDecimal"/> value.
    /// </summary>
    public BigInteger FractionalValue => AbsoluteUnscaledValue - AbsoluteIntegralValue * OrderOfMagnitude;

    /// <summary>
    /// Gets the scale factor of the current <see cref="BigDecimal"/>, which is 10 raised to the power of its scale.
    /// </summary>
    private BigInteger OrderOfMagnitude => BigInteger.Pow(10, Scale);

    /// <summary>
    /// Gets the absolute, unscaled value of the current <see cref="BigDecimal"/> value.
    /// </summary>
    private BigInteger AbsoluteUnscaledValue => BigInteger.Abs(UnscaledValue);

    /// <summary>
    /// Gets the absolute, integral value of the current <see cref="BigDecimal"/> value.
    /// </summary>
    private BigInteger AbsoluteIntegralValue => BigInteger.Abs(IntegralValue);
}