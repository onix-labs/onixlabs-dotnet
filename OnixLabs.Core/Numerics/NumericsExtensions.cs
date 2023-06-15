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
using System.ComponentModel;
using System.Numerics;

namespace OnixLabs.Core.Numerics;

/// <summary>
/// Provides extension methods for numeric types.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public static class NumericsExtensions
{
    /// <summary>
    /// Gets the unscaled value of the current <see cref="decimal"/>.
    /// </summary>
    /// <param name="value">The <see cref="decimal"/> from which to obtain an unscaled value.</param>
    /// <returns>Returns the unscaled value of the current <see cref="decimal"/> as a <see cref="BigInteger"/>.</returns>
    public static BigInteger GetUnscaledValue(this decimal value)
    {
        const int mantissaBits = 3;
        int[] bits = decimal.GetBits(value);
        byte[] bytes = new byte[mantissaBits * sizeof(int) + 1];
        Buffer.BlockCopy(bits, 0, bytes, 0, bytes.Length);
        BigInteger result = new(bytes);

        return decimal.IsPositive(value) ? result : -result;
    }

    /// <summary>
    /// Converts the current <see cref="sbyte"/> value to a <see cref="BigInteger"/>.
    /// </summary>
    /// <param name="value">The <see cref="sbyte"/> value to convert.</param>
    /// <returns>Returns a new <see cref="BigInteger"/> representing the current <see cref="sbyte"/> value.</returns>
    public static BigInteger ToBigInteger(this sbyte value)
    {
        return new BigInteger(value);
    }

    /// <summary>
    /// Converts the current <see cref="byte"/> value to a <see cref="BigInteger"/>.
    /// </summary>
    /// <param name="value">The <see cref="byte"/> value to convert.</param>
    /// <returns>Returns a new <see cref="BigInteger"/> representing the current <see cref="byte"/> value.</returns>
    public static BigInteger ToBigInteger(this byte value)
    {
        return new BigInteger(value);
    }

    /// <summary>
    /// Converts the current <see cref="short"/> value to a <see cref="BigInteger"/>.
    /// </summary>
    /// <param name="value">The <see cref="short"/> value to convert.</param>
    /// <returns>Returns a new <see cref="BigInteger"/> representing the current <see cref="short"/> value.</returns>
    public static BigInteger ToBigInteger(this short value)
    {
        return new BigInteger(value);
    }

    /// <summary>
    /// Converts the current <see cref="ushort"/> value to a <see cref="BigInteger"/>.
    /// </summary>
    /// <param name="value">The <see cref="ushort"/> value to convert.</param>
    /// <returns>Returns a new <see cref="BigInteger"/> representing the current <see cref="ushort"/> value.</returns>
    public static BigInteger ToBigInteger(this ushort value)
    {
        return new BigInteger(value);
    }

    /// <summary>
    /// Converts the current <see cref="int"/> value to a <see cref="BigInteger"/>.
    /// </summary>
    /// <param name="value">The <see cref="int"/> value to convert.</param>
    /// <returns>Returns a new <see cref="BigInteger"/> representing the current <see cref="int"/> value.</returns>
    public static BigInteger ToBigInteger(this int value)
    {
        return new BigInteger(value);
    }

    /// <summary>
    /// Converts the current <see cref="uint"/> value to a <see cref="BigInteger"/>.
    /// </summary>
    /// <param name="value">The <see cref="uint"/> value to convert.</param>
    /// <returns>Returns a new <see cref="BigInteger"/> representing the current <see cref="uint"/> value.</returns>
    public static BigInteger ToBigInteger(this uint value)
    {
        return new BigInteger(value);
    }

    /// <summary>
    /// Converts the current <see cref="long"/> value to a <see cref="BigInteger"/>.
    /// </summary>
    /// <param name="value">The <see cref="long"/> value to convert.</param>
    /// <returns>Returns a new <see cref="BigInteger"/> representing the current <see cref="long"/> value.</returns>
    public static BigInteger ToBigInteger(this long value)
    {
        return new BigInteger(value);
    }

    /// <summary>
    /// Converts the current <see cref="ulong"/> value to a <see cref="BigInteger"/>.
    /// </summary>
    /// <param name="value">The <see cref="ulong"/> value to convert.</param>
    /// <returns>Returns a new <see cref="BigInteger"/> representing the current <see cref="ulong"/> value.</returns>
    public static BigInteger ToBigInteger(this ulong value)
    {
        return new BigInteger(value);
    }

    /// <summary>
    /// Converts the current <see cref="BigInteger"/> value to a <see cref="BigDecimal"/>.
    /// </summary>
    /// <param name="value">The <see cref="BigInteger"/> value to convert.</param>
    /// <param name="scale">The scale of the <see cref="BigDecimal"/> value.</param>
    /// <returns>Returns a new <see cref="BigDecimal"/> representing the current <see cref="BigInteger"/> value.</returns>
    public static BigDecimal ToBigDecimal(this BigInteger value, int scale)
    {
        return new BigDecimal(value, scale);
    }

    /// <summary>
    /// Converts the current <see cref="sbyte"/> value to a <see cref="BigDecimal"/>.
    /// </summary>
    /// <param name="value">The <see cref="sbyte"/> value to convert.</param>
    /// <param name="scale">The scale of the <see cref="BigDecimal"/> value.</param>
    /// <returns>Returns a new <see cref="BigDecimal"/> representing the current <see cref="sbyte"/> value.</returns>
    public static BigDecimal ToBigDecimal(this sbyte value, int scale)
    {
        return new BigDecimal(value, scale);
    }

    /// <summary>
    /// Converts the current <see cref="byte"/> value to a <see cref="BigDecimal"/>.
    /// </summary>
    /// <param name="value">The <see cref="byte"/> value to convert.</param>
    /// <param name="scale">The scale of the <see cref="BigDecimal"/> value.</param>
    /// <returns>Returns a new <see cref="BigDecimal"/> representing the current <see cref="byte"/> value.</returns>
    public static BigDecimal ToBigDecimal(this byte value, int scale)
    {
        return new BigDecimal(value, scale);
    }

    /// <summary>
    /// Converts the current <see cref="short"/> value to a <see cref="BigDecimal"/>.
    /// </summary>
    /// <param name="value">The <see cref="short"/> value to convert.</param>
    /// <param name="scale">The scale of the <see cref="BigDecimal"/> value.</param>
    /// <returns>Returns a new <see cref="BigDecimal"/> representing the current <see cref="short"/> value.</returns>
    public static BigDecimal ToBigDecimal(this short value, int scale)
    {
        return new BigDecimal(value, scale);
    }

    /// <summary>
    /// Converts the current <see cref="ushort"/> value to a <see cref="BigDecimal"/>.
    /// </summary>
    /// <param name="value">The <see cref="ushort"/> value to convert.</param>
    /// <param name="scale">The scale of the <see cref="BigDecimal"/> value.</param>
    /// <returns>Returns a new <see cref="BigDecimal"/> representing the current <see cref="ushort"/> value.</returns>
    public static BigDecimal ToBigDecimal(this ushort value, int scale)
    {
        return new BigDecimal(value, scale);
    }

    /// <summary>
    /// Converts the current <see cref="int"/> value to a <see cref="BigDecimal"/>.
    /// </summary>
    /// <param name="value">The <see cref="int"/> value to convert.</param>
    /// <param name="scale">The scale of the <see cref="BigDecimal"/> value.</param>
    /// <returns>Returns a new <see cref="BigDecimal"/> representing the current <see cref="int"/> value.</returns>
    public static BigDecimal ToBigDecimal(this int value, int scale)
    {
        return new BigDecimal(value, scale);
    }

    /// <summary>
    /// Converts the current <see cref="uint"/> value to a <see cref="BigDecimal"/>.
    /// </summary>
    /// <param name="value">The <see cref="uint"/> value to convert.</param>
    /// <param name="scale">The scale of the <see cref="BigDecimal"/> value.</param>
    /// <returns>Returns a new <see cref="BigDecimal"/> representing the current <see cref="uint"/> value.</returns>
    public static BigDecimal ToBigDecimal(this uint value, int scale)
    {
        return new BigDecimal(value, scale);
    }

    /// <summary>
    /// Converts the current <see cref="long"/> value to a <see cref="BigDecimal"/>.
    /// </summary>
    /// <param name="value">The <see cref="long"/> value to convert.</param>
    /// <param name="scale">The scale of the <see cref="BigDecimal"/> value.</param>
    /// <returns>Returns a new <see cref="BigDecimal"/> representing the current <see cref="long"/> value.</returns>
    public static BigDecimal ToBigDecimal(this long value, int scale)
    {
        return new BigDecimal(value, scale);
    }

    /// <summary>
    /// Converts the current <see cref="ulong"/> value to a <see cref="BigDecimal"/>.
    /// </summary>
    /// <param name="value">The <see cref="ulong"/> value to convert.</param>
    /// <param name="scale">The scale of the <see cref="BigDecimal"/> value.</param>
    /// <returns>Returns a new <see cref="BigDecimal"/> representing the current <see cref="ulong"/> value.</returns>
    public static BigDecimal ToBigDecimal(this ulong value, int scale)
    {
        return new BigDecimal(value, scale);
    }

    /// <summary>
    /// Converts the current <see cref="float"/> value to a <see cref="BigDecimal"/>.
    /// </summary>
    /// <param name="value">The <see cref="float"/> value to convert.</param>
    /// <param name="mode">Specifies the conversion mode from <see cref="double"/> to <see cref="BigDecimal"/>.</param>
    /// <returns>Returns a new <see cref="BigDecimal"/> representing the current <see cref="float"/> value.</returns>
    public static BigDecimal ToBigDecimal(this float value, ConversionMode mode = ConversionMode.Decimal)
    {
        return new BigDecimal(value, mode);
    }

    /// <summary>
    /// Converts the current <see cref="double"/> value to a <see cref="BigDecimal"/>.
    /// </summary>
    /// <param name="value">The <see cref="double"/> value to convert.</param>
    /// <param name="mode">Specifies the conversion mode from <see cref="double"/> to <see cref="BigDecimal"/>.</param>
    /// <returns>Returns a new <see cref="BigDecimal"/> representing the current <see cref="double"/> value.</returns>
    public static BigDecimal ToBigDecimal(this double value, ConversionMode mode = ConversionMode.Decimal)
    {
        return new BigDecimal(value, mode);
    }

    /// <summary>
    /// Converts the current <see cref="decimal"/> value to a <see cref="BigDecimal"/>.
    /// </summary>
    /// <param name="value">The <see cref="decimal"/> value to convert.</param>
    /// <returns>Returns a new <see cref="BigDecimal"/> representing the current <see cref="decimal"/> value.</returns>
    public static BigDecimal ToBigDecimal(this decimal value)
    {
        return new BigDecimal(value);
    }
}
