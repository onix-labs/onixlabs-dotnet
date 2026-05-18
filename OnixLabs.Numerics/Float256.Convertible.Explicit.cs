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

public readonly partial struct Float256
{
    /// <summary>Converts the specified <see cref="Float256"/> value to an <see cref="sbyte"/>, saturating on overflow.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>The integral part of <paramref name="value"/> as an <see cref="sbyte"/>.</returns>
    public static explicit operator sbyte(Float256 value) => (sbyte)Clamp(value, sbyte.MinValue, sbyte.MaxValue);

    /// <summary>Converts the specified <see cref="Float256"/> value to an <see cref="sbyte"/>, throwing on overflow.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>The integral part of <paramref name="value"/> as an <see cref="sbyte"/>.</returns>
    public static explicit operator checked sbyte(Float256 value) => checked((sbyte)ToBigIntegerChecked(value, nameof(SByte)));

    /// <summary>Converts the specified <see cref="Float256"/> value to a <see cref="byte"/>, saturating on overflow.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>The integral part of <paramref name="value"/> as a <see cref="byte"/>.</returns>
    public static explicit operator byte(Float256 value) => (byte)ClampUnsigned(value, byte.MaxValue);

    /// <summary>Converts the specified <see cref="Float256"/> value to a <see cref="byte"/>, throwing on overflow.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>The integral part of <paramref name="value"/> as a <see cref="byte"/>.</returns>
    public static explicit operator checked byte(Float256 value) => checked((byte)ToBigIntegerChecked(value, nameof(Byte)));

    /// <summary>Converts the specified <see cref="Float256"/> value to a <see cref="short"/>, saturating on overflow.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>The integral part of <paramref name="value"/> as a <see cref="short"/>.</returns>
    public static explicit operator short(Float256 value) => (short)Clamp(value, short.MinValue, short.MaxValue);

    /// <summary>Converts the specified <see cref="Float256"/> value to a <see cref="short"/>, throwing on overflow.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>The integral part of <paramref name="value"/> as a <see cref="short"/>.</returns>
    public static explicit operator checked short(Float256 value) => checked((short)ToBigIntegerChecked(value, nameof(Int16)));

    /// <summary>Converts the specified <see cref="Float256"/> value to a <see cref="ushort"/>, saturating on overflow.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>The integral part of <paramref name="value"/> as a <see cref="ushort"/>.</returns>
    public static explicit operator ushort(Float256 value) => (ushort)ClampUnsigned(value, ushort.MaxValue);

    /// <summary>Converts the specified <see cref="Float256"/> value to a <see cref="ushort"/>, throwing on overflow.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>The integral part of <paramref name="value"/> as a <see cref="ushort"/>.</returns>
    public static explicit operator checked ushort(Float256 value) => checked((ushort)ToBigIntegerChecked(value, nameof(UInt16)));

    /// <summary>Converts the specified <see cref="Float256"/> value to an <see cref="int"/>, saturating on overflow.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>The integral part of <paramref name="value"/> as an <see cref="int"/>.</returns>
    public static explicit operator int(Float256 value) => (int)Clamp(value, int.MinValue, int.MaxValue);

    /// <summary>Converts the specified <see cref="Float256"/> value to an <see cref="int"/>, throwing on overflow.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>The integral part of <paramref name="value"/> as an <see cref="int"/>.</returns>
    public static explicit operator checked int(Float256 value) => checked((int)ToBigIntegerChecked(value, nameof(Int32)));

    /// <summary>Converts the specified <see cref="Float256"/> value to a <see cref="uint"/>, saturating on overflow.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>The integral part of <paramref name="value"/> as a <see cref="uint"/>.</returns>
    public static explicit operator uint(Float256 value) => (uint)ClampUnsigned(value, uint.MaxValue);

    /// <summary>Converts the specified <see cref="Float256"/> value to a <see cref="uint"/>, throwing on overflow.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>The integral part of <paramref name="value"/> as a <see cref="uint"/>.</returns>
    public static explicit operator checked uint(Float256 value) => checked((uint)ToBigIntegerChecked(value, nameof(UInt32)));

    /// <summary>Converts the specified <see cref="Float256"/> value to a <see cref="long"/>, saturating on overflow.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>The integral part of <paramref name="value"/> as a <see cref="long"/>.</returns>
    public static explicit operator long(Float256 value) => (long)Clamp(value, long.MinValue, long.MaxValue);

    /// <summary>Converts the specified <see cref="Float256"/> value to a <see cref="long"/>, throwing on overflow.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>The integral part of <paramref name="value"/> as a <see cref="long"/>.</returns>
    public static explicit operator checked long(Float256 value) => checked((long)ToBigIntegerChecked(value, nameof(Int64)));

    /// <summary>Converts the specified <see cref="Float256"/> value to a <see cref="ulong"/>, saturating on overflow.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>The integral part of <paramref name="value"/> as a <see cref="ulong"/>.</returns>
    public static explicit operator ulong(Float256 value) => (ulong)ClampUnsigned(value, ulong.MaxValue);

    /// <summary>Converts the specified <see cref="Float256"/> value to a <see cref="ulong"/>, throwing on overflow.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>The integral part of <paramref name="value"/> as a <see cref="ulong"/>.</returns>
    public static explicit operator checked ulong(Float256 value) => checked((ulong)ToBigIntegerChecked(value, nameof(UInt64)));

    /// <summary>Converts the specified <see cref="Float256"/> value to an <see cref="Int128"/>, saturating on overflow.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>The integral part of <paramref name="value"/> as an <see cref="Int128"/>.</returns>
    public static explicit operator Int128(Float256 value) => (Int128)ClampInt128(value);

    /// <summary>Converts the specified <see cref="Float256"/> value to an <see cref="Int128"/>, throwing on overflow.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>The integral part of <paramref name="value"/> as an <see cref="Int128"/>.</returns>
    public static explicit operator checked Int128(Float256 value) => checked((Int128)ToBigIntegerChecked(value, nameof(Int128)));

    /// <summary>Converts the specified <see cref="Float256"/> value to a <see cref="UInt128"/>, saturating on overflow.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>The integral part of <paramref name="value"/> as a <see cref="UInt128"/>.</returns>
    public static explicit operator UInt128(Float256 value) => ClampUInt128(value);

    /// <summary>Converts the specified <see cref="Float256"/> value to a <see cref="UInt128"/>, throwing on overflow.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>The integral part of <paramref name="value"/> as a <see cref="UInt128"/>.</returns>
    public static explicit operator checked UInt128(Float256 value) => checked((UInt128)ToBigIntegerChecked(value, nameof(UInt128)));

    /// <summary>Converts the specified <see cref="Float256"/> value to a <see cref="char"/>, saturating on overflow.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>The integral part of <paramref name="value"/> as a <see cref="char"/>.</returns>
    public static explicit operator char(Float256 value) => (char)ClampUnsigned(value, char.MaxValue);

    /// <summary>Converts the specified <see cref="Float256"/> value to a <see cref="char"/>, throwing on overflow.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>The integral part of <paramref name="value"/> as a <see cref="char"/>.</returns>
    public static explicit operator checked char(Float256 value) => checked((char)ToBigIntegerChecked(value, nameof(Char)));

    /// <summary>Converts the specified <see cref="Float256"/> value to a <see cref="BigInteger"/>, truncating any fractional part.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>The integral part of <paramref name="value"/> as a <see cref="BigInteger"/>.</returns>
    /// <exception cref="OverflowException">Thrown when <paramref name="value"/> is NaN or infinite.</exception>
    public static explicit operator BigInteger(Float256 value) => ToBigIntegerChecked(value, nameof(BigInteger));

    /// <summary>Converts the specified <see cref="Float256"/> value to a <see cref="Half"/>.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>The <see cref="Half"/> closest to <paramref name="value"/>.</returns>
    public static explicit operator Half(Float256 value) => (Half)(double)value;

    /// <summary>Converts the specified <see cref="Float256"/> value to a <see cref="float"/>.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>The <see cref="float"/> closest to <paramref name="value"/>.</returns>
    public static explicit operator float(Float256 value) => (float)(double)value;

    /// <summary>Converts the specified <see cref="Float256"/> value to a <see cref="double"/>.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>The <see cref="double"/> closest to <paramref name="value"/>.</returns>
    public static explicit operator double(Float256 value)
    {
        if (IsNaN(value)) return double.NaN;
        if (IsPositiveInfinity(value)) return double.PositiveInfinity;
        if (IsNegativeInfinity(value)) return double.NegativeInfinity;
        if (IsZero(value)) return IsNegative(value) ? -0.0 : 0.0;
        BigDecimal bd = (BigDecimal)value;
        return (double)bd;
    }

    /// <summary>Converts the specified <see cref="Float256"/> value to a <see cref="decimal"/>.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>The <see cref="decimal"/> closest to <paramref name="value"/>.</returns>
    /// <exception cref="OverflowException">Thrown when <paramref name="value"/> is NaN, infinite, or outside the range of <see cref="decimal"/>.</exception>
    public static explicit operator decimal(Float256 value)
    {
        if (IsNaN(value)) throw new OverflowException($"Cannot convert NaN to {nameof(Decimal)}.");
        if (IsInfinity(value)) throw new OverflowException($"Cannot convert infinity to {nameof(Decimal)}.");
        return (decimal)(BigDecimal)value;
    }

    /// <summary>Converts the specified <see cref="Float256"/> value to a <see cref="Float128"/>.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>The <see cref="Float128"/> closest to <paramref name="value"/>.</returns>
    public static explicit operator Float128(Float256 value)
    {
        if (IsNaN(value)) return Float128.NaN;
        if (IsPositiveInfinity(value)) return Float128.PositiveInfinity;
        if (IsNegativeInfinity(value)) return Float128.NegativeInfinity;
        if (IsZero(value)) return IsNegative(value) ? Float128.NegativeZero : Float128.Zero;
        return (Float128)(BigDecimal)value;
    }

    /// <summary>
    /// Converts the specified <see cref="Float256"/> value to a <see cref="BigInteger"/>, throwing on NaN or infinity.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <param name="typeName">The name of the target type used in error messages.</param>
    /// <returns>Returns the integral portion of <paramref name="value"/> as a <see cref="BigInteger"/>.</returns>
    /// <exception cref="OverflowException">Thrown when <paramref name="value"/> is NaN or infinite.</exception>
    private static BigInteger ToBigIntegerChecked(Float256 value, string typeName)
    {
        if (IsNaN(value)) throw new OverflowException($"Cannot convert NaN to {typeName}.");
        if (IsInfinity(value)) throw new OverflowException($"Cannot convert infinity to {typeName}.");
        if (IsZero(value)) return BigInteger.Zero;
        BigDecimal exact = (BigDecimal)value;
        return TruncateToBigInteger(exact);
    }

    /// <summary>
    /// Truncates the specified <see cref="BigDecimal"/> toward zero and returns the integral part as a <see cref="BigInteger"/>.
    /// </summary>
    /// <param name="value">The value to truncate.</param>
    /// <returns>Returns the integer obtained by discarding the fractional digits of <paramref name="value"/>.</returns>
    private static BigInteger TruncateToBigInteger(BigDecimal value)
    {
        BigInteger unscaled = value.UnscaledValue;
        int scale = value.Scale;
        if (scale <= 0)
        {
            return scale == 0 ? unscaled : unscaled * BigInteger.Pow(10, -scale);
        }

        return unscaled / BigInteger.Pow(10, scale);
    }

    /// <summary>
    /// Truncates the specified <see cref="Float256"/> value to a signed 64-bit integer, saturating to the given bounds and mapping NaN to zero.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <param name="minValue">The lower bound to which the result is saturated.</param>
    /// <param name="maxValue">The upper bound to which the result is saturated.</param>
    /// <returns>Returns the integral portion of <paramref name="value"/> clamped to the inclusive range [<paramref name="minValue"/>, <paramref name="maxValue"/>].</returns>
    private static long Clamp(Float256 value, long minValue, long maxValue)
    {
        if (IsNaN(value)) return 0L;
        if (IsPositiveInfinity(value)) return maxValue;
        if (IsNegativeInfinity(value)) return minValue;
        if (IsZero(value)) return 0L;
        BigInteger big = TruncateToBigInteger((BigDecimal)value);
        if (big > maxValue) return maxValue;
        if (big < minValue) return minValue;
        return (long)big;
    }

    /// <summary>
    /// Truncates the specified <see cref="Float256"/> value to an unsigned 64-bit integer, saturating to <paramref name="maxValue"/> and mapping NaN or negative inputs to zero.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <param name="maxValue">The upper bound to which the result is saturated.</param>
    /// <returns>Returns the integral portion of <paramref name="value"/> clamped to the inclusive range [<c>0</c>, <paramref name="maxValue"/>].</returns>
    private static ulong ClampUnsigned(Float256 value, ulong maxValue)
    {
        if (IsNaN(value)) return 0UL;
        if (IsPositiveInfinity(value)) return maxValue;
        if (IsNegativeInfinity(value)) return 0UL;
        if (IsZero(value)) return 0UL;
        if (IsNegative(value)) return 0UL;
        BigInteger big = TruncateToBigInteger((BigDecimal)value);
        if (big > maxValue) return maxValue;
        return (ulong)big;
    }

    /// <summary>
    /// Truncates the specified <see cref="Float256"/> value to an <see cref="Int128"/>, saturating to <see cref="Int128.MinValue"/> or <see cref="Int128.MaxValue"/> on overflow and mapping NaN to zero.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the integral portion of <paramref name="value"/> clamped to the <see cref="Int128"/> range.</returns>
    private static Int128 ClampInt128(Float256 value)
    {
        if (IsNaN(value)) return Int128.Zero;
        if (IsPositiveInfinity(value)) return Int128.MaxValue;
        if (IsNegativeInfinity(value)) return Int128.MinValue;
        if (IsZero(value)) return Int128.Zero;
        BigInteger big = TruncateToBigInteger((BigDecimal)value);
        if (big > (BigInteger)Int128.MaxValue) return Int128.MaxValue;
        if (big < (BigInteger)Int128.MinValue) return Int128.MinValue;
        return (Int128)big;
    }

    /// <summary>
    /// Truncates the specified <see cref="Float256"/> value to a <see cref="UInt128"/>, saturating to <see cref="UInt128.MaxValue"/> on overflow and mapping NaN or negative inputs to zero.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the integral portion of <paramref name="value"/> clamped to the <see cref="UInt128"/> range.</returns>
    private static UInt128 ClampUInt128(Float256 value)
    {
        if (IsNaN(value)) return UInt128.Zero;
        if (IsPositiveInfinity(value)) return UInt128.MaxValue;
        if (IsNegativeInfinity(value)) return UInt128.Zero;
        if (IsZero(value)) return UInt128.Zero;
        if (IsNegative(value)) return UInt128.Zero;
        BigInteger big = TruncateToBigInteger((BigDecimal)value);
        if (big > (BigInteger)UInt128.MaxValue) return UInt128.MaxValue;
        return (UInt128)big;
    }
}
