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
using System.Globalization;
using System.Numerics;

namespace OnixLabs.Numerics;

public readonly partial struct BigDecimal
{
    // Why: these bounds are constants, but converting a float, double or decimal to a BigDecimal formats and parses it, so the bounds
    // that the narrowing conversions check against are converted once rather than on every conversion.
    private static readonly BigDecimal SingleMinValue = float.MinValue;
    private static readonly BigDecimal SingleMaxValue = float.MaxValue;
    private static readonly BigDecimal DoubleMinValue = double.MinValue;
    private static readonly BigDecimal DoubleMaxValue = double.MaxValue;
    private static readonly BigDecimal DecimalMinValue = decimal.MinValue;
    private static readonly BigDecimal DecimalMaxValue = decimal.MaxValue;

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
    /// <exception cref="OverflowException">Thrown when the integral value of the specified <see cref="BigDecimal"/> value falls outside the range of <see cref="sbyte"/>.</exception>
    public static explicit operator sbyte(BigDecimal value)
    {
        return (sbyte)GetCheckedInteger(value, sbyte.MinValue, sbyte.MaxValue);
    }

    /// <summary>
    /// Converts the integral value of the specified <see cref="BigDecimal"/> value to a <see cref="byte"/> value.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns a <see cref="byte"/> value representing the integral value of the specified <see cref="BigDecimal"/> value.</returns>
    /// <exception cref="OverflowException">Thrown when the integral value of the specified <see cref="BigDecimal"/> value falls outside the range of <see cref="byte"/>.</exception>
    public static explicit operator byte(BigDecimal value)
    {
        return (byte)GetCheckedInteger(value, byte.MinValue, byte.MaxValue);
    }

    /// <summary>
    /// Converts the integral value of the specified <see cref="BigDecimal"/> value to a <see cref="short"/> value.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns a <see cref="short"/> value representing the integral value of the specified <see cref="BigDecimal"/> value.</returns>
    /// <exception cref="OverflowException">Thrown when the integral value of the specified <see cref="BigDecimal"/> value falls outside the range of <see cref="short"/>.</exception>
    public static explicit operator short(BigDecimal value)
    {
        return (short)GetCheckedInteger(value, short.MinValue, short.MaxValue);
    }

    /// <summary>
    /// Converts the integral value of the specified <see cref="BigDecimal"/> value to a <see cref="ushort"/> value.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns a <see cref="ushort"/> value representing the integral value of the specified <see cref="BigDecimal"/> value.</returns>
    /// <exception cref="OverflowException">Thrown when the integral value of the specified <see cref="BigDecimal"/> value falls outside the range of <see cref="ushort"/>.</exception>
    public static explicit operator ushort(BigDecimal value)
    {
        return (ushort)GetCheckedInteger(value, ushort.MinValue, ushort.MaxValue);
    }

    /// <summary>
    /// Converts the integral value of the specified <see cref="BigDecimal"/> value to an <see cref="int"/> value.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns an <see cref="int"/> value representing the integral value of the specified <see cref="BigDecimal"/> value.</returns>
    /// <exception cref="OverflowException">Thrown when the integral value of the specified <see cref="BigDecimal"/> value falls outside the range of <see cref="int"/>.</exception>
    public static explicit operator int(BigDecimal value)
    {
        return (int)GetCheckedInteger(value, int.MinValue, int.MaxValue);
    }

    /// <summary>
    /// Converts the integral value of the specified <see cref="BigDecimal"/> value to a <see cref="uint"/> value.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns a <see cref="uint"/> value representing the integral value of the specified <see cref="BigDecimal"/> value.</returns>
    /// <exception cref="OverflowException">Thrown when the integral value of the specified <see cref="BigDecimal"/> value falls outside the range of <see cref="uint"/>.</exception>
    public static explicit operator uint(BigDecimal value)
    {
        return (uint)GetCheckedInteger(value, uint.MinValue, uint.MaxValue);
    }

    /// <summary>
    /// Converts the integral value of the specified <see cref="BigDecimal"/> value to a <see cref="long"/> value.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns a <see cref="long"/> value representing the integral value of the specified <see cref="BigDecimal"/> value.</returns>
    /// <exception cref="OverflowException">Thrown when the integral value of the specified <see cref="BigDecimal"/> value falls outside the range of <see cref="long"/>.</exception>
    public static explicit operator long(BigDecimal value)
    {
        return (long)GetCheckedInteger(value, long.MinValue, long.MaxValue);
    }

    /// <summary>
    /// Converts the integral value of the specified <see cref="BigDecimal"/> value to a <see cref="ulong"/> value.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns a <see cref="ulong"/> value representing the integral value of the specified <see cref="BigDecimal"/> value.</returns>
    /// <exception cref="OverflowException">Thrown when the integral value of the specified <see cref="BigDecimal"/> value falls outside the range of <see cref="ulong"/>.</exception>
    public static explicit operator ulong(BigDecimal value)
    {
        return (ulong)GetCheckedInteger(value, ulong.MinValue, ulong.MaxValue);
    }

    /// <summary>
    /// Converts the integral value of the specified <see cref="BigDecimal"/> value to a <see cref="Int128"/> value.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns an <see cref="Int128"/> value representing the integral value of the specified <see cref="BigDecimal"/> value.</returns>
    /// <exception cref="OverflowException">Thrown when the integral value of the specified <see cref="BigDecimal"/> value falls outside the range of <see cref="Int128"/>.</exception>
    public static explicit operator Int128(BigDecimal value)
    {
        return (Int128)GetCheckedInteger(value, Int128.MinValue, Int128.MaxValue);
    }

    /// <summary>
    /// Converts the integral value of the specified <see cref="BigDecimal"/> value to a <see cref="UInt128"/> value.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns a <see cref="UInt128"/> value representing the integral value of the specified <see cref="BigDecimal"/> value.</returns>
    /// <exception cref="OverflowException">Thrown when the integral value of the specified <see cref="BigDecimal"/> value falls outside the range of <see cref="UInt128"/>.</exception>
    public static explicit operator UInt128(BigDecimal value)
    {
        return (UInt128)GetCheckedInteger(value, UInt128.MinValue, UInt128.MaxValue);
    }

    /// <summary>
    /// Converts the specified <see cref="BigDecimal"/> value to a <see cref="float"/> value.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns a <see cref="float"/> value equivalent to the specified <see cref="BigDecimal"/> value.</returns>
    /// <exception cref="OverflowException">Thrown when the specified <see cref="BigDecimal"/> value falls outside the range of <see cref="float"/>.</exception>
    public static explicit operator float(BigDecimal value)
    {
        if (value < SingleMinValue || value > SingleMaxValue)
            throw new OverflowException($"Value was either too large or too small for the specified type: {nameof(Single)}.");

        // Why: this is an internal machine round-trip; InvariantCulture for both format and parse keeps it locale-immune. NumberStyles.Float accepts the "E" exponent.
        return float.Parse(value.ToString("E", CultureInfo.InvariantCulture), NumberStyles.Float, CultureInfo.InvariantCulture);
    }

    /// <summary>
    /// Converts the specified <see cref="BigDecimal"/> value to a <see cref="double"/> value.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns a <see cref="double"/> value equivalent to the specified <see cref="BigDecimal"/> value.</returns>
    /// <exception cref="OverflowException">Thrown when the specified <see cref="BigDecimal"/> value falls outside the range of <see cref="double"/>.</exception>
    public static explicit operator double(BigDecimal value)
    {
        if (value < DoubleMinValue || value > DoubleMaxValue)
            throw new OverflowException($"Value was either too large or too small for the specified type: {nameof(Double)}.");

        // Why: this is an internal machine round-trip; InvariantCulture for both format and parse keeps it locale-immune. NumberStyles.Float accepts the "E" exponent.
        return double.Parse(value.ToString("E", CultureInfo.InvariantCulture), NumberStyles.Float, CultureInfo.InvariantCulture);
    }

    /// <summary>
    /// Converts the specified <see cref="BigDecimal"/> value to a <see cref="decimal"/> value.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns a <see cref="decimal"/> value equivalent to the specified <see cref="BigDecimal"/> value.</returns>
    /// <exception cref="OverflowException">Thrown when the specified <see cref="BigDecimal"/> value falls outside the range of <see cref="decimal"/>.</exception>
    public static explicit operator decimal(BigDecimal value)
    {
        if (value < DecimalMinValue || value > DecimalMaxValue)
            throw new OverflowException($"Value was either too large or too small for the specified type: {nameof(Decimal)}.");

        // Why: this is an internal machine round-trip; InvariantCulture for both format and parse keeps it locale-immune.
        // decimal.Parse needs NumberStyles.Float (AllowExponent) to accept the "E" exponent; Convert.ToDecimal would reject it.
        return decimal.Parse(value.ToString("E", CultureInfo.InvariantCulture), NumberStyles.Float, CultureInfo.InvariantCulture);
    }

    /// <summary>
    /// Gets the integral value of the specified <see cref="BigDecimal"/>, checking that it is within the bounds of the specified minimum
    /// and maximum values.
    /// </summary>
    /// <param name="value">The <see cref="BigDecimal"/> value to check.</param>
    /// <param name="min">The minimum bound value.</param>
    /// <param name="max">The maximum bound value.</param>
    /// <typeparam name="T">The underlying <see cref="IBinaryInteger{TSelf}"/> type.</typeparam>
    /// <returns>Returns the integral value of the specified <see cref="BigDecimal"/>.</returns>
    /// <exception cref="OverflowException">Thrown when the integral value of the specified <see cref="BigDecimal"/> value is not within the bounds of the specified minimum and maximum values.</exception>
    private static BigInteger GetCheckedInteger<T>(in BigDecimal value, T min, T max) where T : IBinaryInteger<T>
    {
        // Why: the integral value costs a division, so it is computed once for both the check and the conversion.
        BigInteger integer = value.number.Integer;

        if (integer < BigInteger.CreateChecked(min) || integer > BigInteger.CreateChecked(max))
            throw new OverflowException($"Value was either too large or too small for the specified type: {typeof(T).Name}.");

        return integer;
    }
}
