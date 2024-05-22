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
    /// Converts the integral value of the specified <see cref="BigDecimal"/> value to a <see cref="BigInteger"/> value.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns a <see cref="BigInteger"/> value representing the integral value of the specified <see cref="BigDecimal"/> value.</returns>
    public static explicit operator BigInteger(BigDecimal value) => value.number.Integer;

    /// <summary>
    /// Converts the integral value of the specified <see cref="BigDecimal"/> value to a <see cref="sbyte"/> value.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns a <see cref="sbyte"/> value representing the integral value of the specified <see cref="BigDecimal"/> value.</returns>
    public static explicit operator sbyte(BigDecimal value)
    {
        CheckIntegerOverflow(value, sbyte.MinValue, sbyte.MaxValue);
        return (sbyte)value.number.Integer;
    }

    /// <summary>
    /// Converts the integral value of the specified <see cref="BigDecimal"/> value to a <see cref="byte"/> value.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns a <see cref="byte"/> value representing the integral value of the specified <see cref="BigDecimal"/> value.</returns>
    public static explicit operator byte(BigDecimal value)
    {
        CheckIntegerOverflow(value, byte.MinValue, byte.MaxValue);
        return (byte)value.number.Integer;
    }

    /// <summary>
    /// Converts the integral value of the specified <see cref="BigDecimal"/> value to a <see cref="short"/> value.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns a <see cref="short"/> value representing the integral value of the specified <see cref="BigDecimal"/> value.</returns>
    public static explicit operator short(BigDecimal value)
    {
        CheckIntegerOverflow(value, short.MinValue, short.MaxValue);
        return (short)value.number.Integer;
    }

    /// <summary>
    /// Converts the integral value of the specified <see cref="BigDecimal"/> value to a <see cref="ushort"/> value.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns a <see cref="ushort"/> value representing the integral value of the specified <see cref="BigDecimal"/> value.</returns>
    public static explicit operator ushort(BigDecimal value)
    {
        CheckIntegerOverflow(value, ushort.MinValue, ushort.MaxValue);
        return (ushort)value.number.Integer;
    }

    /// <summary>
    /// Converts the integral value of the specified <see cref="BigDecimal"/> value to a <see cref="int"/> value.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns a <see cref="int"/> value representing the integral value of the specified <see cref="BigDecimal"/> value.</returns>
    public static explicit operator int(BigDecimal value)
    {
        CheckIntegerOverflow(value, int.MinValue, int.MaxValue);
        return (int)value.number.Integer;
    }

    /// <summary>
    /// Converts the integral value of the specified <see cref="BigDecimal"/> value to a <see cref="uint"/> value.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns a <see cref="uint"/> value representing the integral value of the specified <see cref="BigDecimal"/> value.</returns>
    public static explicit operator uint(BigDecimal value)
    {
        CheckIntegerOverflow(value, uint.MinValue, uint.MaxValue);
        return (uint)value.number.Integer;
    }

    /// <summary>
    /// Converts the integral value of the specified <see cref="BigDecimal"/> value to a <see cref="long"/> value.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns a <see cref="long"/> value representing the integral value of the specified <see cref="BigDecimal"/> value.</returns>
    public static explicit operator long(BigDecimal value)
    {
        CheckIntegerOverflow(value, long.MinValue, long.MaxValue);
        return (long)value.number.Integer;
    }

    /// <summary>
    /// Converts the integral value of the specified <see cref="BigDecimal"/> value to a <see cref="ulong"/> value.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns a <see cref="ulong"/> value representing the integral value of the specified <see cref="BigDecimal"/> value.</returns>
    public static explicit operator ulong(BigDecimal value)
    {
        CheckIntegerOverflow(value, ulong.MinValue, ulong.MaxValue);
        return (ulong)value.number.Integer;
    }

    /// <summary>
    /// Converts the integral value of the specified <see cref="BigDecimal"/> value to a <see cref="Int128"/> value.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns a <see cref="Int128"/> value representing the integral value of the specified <see cref="BigDecimal"/> value.</returns>
    public static explicit operator Int128(BigDecimal value)
    {
        CheckIntegerOverflow(value, Int128.MinValue, Int128.MaxValue);
        return (Int128)value.number.Integer;
    }

    /// <summary>
    /// Converts the integral value of the specified <see cref="BigDecimal"/> value to a <see cref="UInt128"/> value.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns a <see cref="UInt128"/> value representing the integral value of the specified <see cref="BigDecimal"/> value.</returns>
    public static explicit operator UInt128(BigDecimal value)
    {
        CheckIntegerOverflow(value, UInt128.MinValue, UInt128.MaxValue);
        return (UInt128)value.number.Integer;
    }

    /// <summary>
    /// Converts the specified <see cref="BigDecimal"/> value to a <see cref="float"/> value.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns a <see cref="float"/> value the specified <see cref="BigDecimal"/> value.</returns>
    public static explicit operator float(BigDecimal value)
    {
        if (value < float.MinValue || value > float.MaxValue)
            throw new OverflowException($"Value was either too large or too small for the specified type: {nameof(Single)}.");

        return Convert.ToSingle(value.ToString("E"));
    }

    /// <summary>
    /// Converts the specified <see cref="BigDecimal"/> value to a <see cref="double"/> value.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns a <see cref="double"/> value the specified <see cref="BigDecimal"/> value.</returns>
    public static explicit operator double(BigDecimal value)
    {
        if (value < double.MinValue || value > double.MaxValue)
            throw new OverflowException($"Value was either too large or too small for the specified type: {nameof(Double)}.");

        return Convert.ToDouble(value.ToString("E"));
    }

    /// <summary>
    /// Converts the specified <see cref="BigDecimal"/> value to a <see cref="decimal"/> value.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns a <see cref="decimal"/> value the specified <see cref="BigDecimal"/> value.</returns>
    public static explicit operator decimal(BigDecimal value)
    {
        if (value < decimal.MinValue || value > decimal.MaxValue)
            throw new OverflowException($"Value was either too large or too small for the specified type: {nameof(Decimal)}.");

        return Convert.ToDecimal(value.ToString("E"));
    }

    /// <summary>
    /// Checks whether the integral value of the specified <see cref="BigDecimal"/>
    /// is within the bounds of the specified minimum and maximum values.
    /// </summary>
    /// <param name="value">The <see cref="BigDecimal"/> value to check.</param>
    /// <param name="min">The minimum bound value.</param>
    /// <param name="max">The maximum bound value.</param>
    /// <typeparam name="T">The underlying <see cref="INumber{TSelf}"/> value.</typeparam>
    /// <exception cref="OverflowException"> if the integral value of the specified <see cref="BigDecimal"/>
    /// value is not within the bounds of the specified minimum and maximum value.</exception>
    private static void CheckIntegerOverflow<T>(BigDecimal value, T min, T max) where T : IBinaryInteger<T>
    {
        BigInteger checkedMin = BigInteger.CreateChecked(min);
        BigInteger checkedMax = BigInteger.CreateChecked(max);

        if (value.number.Integer < checkedMin || value.number.Integer > checkedMax)
            throw new OverflowException($"Value was either too large or too small for the specified type: {typeof(T).Name}.");
    }
}
