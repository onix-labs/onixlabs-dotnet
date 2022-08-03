// Copyright 2020-2022 ONIXLabs
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
    /// Converts the current <see cref="decimal"/> value to a <see cref="byte"/> array.
    /// </summary>
    /// <param name="value">The <see cref="decimal"/> value to convert.</param>
    /// <returns>Returns a new <see cref="byte"/> array representing the current <see cref="decimal"/> value.</returns>
    public static byte[] GetBytes(this decimal value)
    {
        const int length = 16;
        byte[] result = new byte[length];
        int[] bits = decimal.GetBits(value);

        for (int index = 0; index < length; index++)
        {
            int bit = bits[index / 4];
            int shift = index % 4 * 8;
            result[index] = (byte) (bit >> shift);
        }

        return result;
    }

    /// <summary>
    /// Gets the digit length of the current <see cref="BigInteger"/>. 
    /// </summary>
    /// <param name="value">The <see cref="BigInteger"/> value from which to obtain the digit length.</param>
    /// <returns>Returns the digit length of the current <see cref="BigInteger"/>.</returns>
    public static int GetDigitLength(this BigInteger value)
    {
        double log10 = BigInteger.Log10(value);
        return (int) Math.Ceiling(log10) + 1;
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
    /// <returns>Returns a new <see cref="BigDecimal"/> representing the current <see cref="float"/> value.</returns>
    public static BigDecimal ToBigDecimal(this float value)
    {
        return new BigDecimal(value);
    }

    /// <summary>
    /// Converts the current <see cref="double"/> value to a <see cref="BigDecimal"/>.
    /// </summary>
    /// <param name="value">The <see cref="double"/> value to convert.</param>
    /// <returns>Returns a new <see cref="BigDecimal"/> representing the current <see cref="double"/> value.</returns>
    public static BigDecimal ToBigDecimal(this double value)
    {
        return new BigDecimal(value);
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
