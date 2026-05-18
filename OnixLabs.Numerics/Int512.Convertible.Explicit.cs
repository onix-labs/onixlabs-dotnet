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

public readonly partial struct Int512
{
    /// <summary>Explicitly converts a <see cref="UInt512"/> value to an <see cref="Int512"/> value, reinterpreting the bits.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the <see cref="Int512"/> with the same bit pattern as <paramref name="value"/>.</returns>
    public static explicit operator Int512(UInt512 value) => new(value.Upper, value.Lower);

    /// <summary>Explicitly converts a <see cref="UInt512"/> value to an <see cref="Int512"/> value, throwing on overflow.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the <see cref="Int512"/> representation of <paramref name="value"/>.</returns>
    /// <exception cref="OverflowException">Thrown when <paramref name="value"/> exceeds <see cref="MaxValue"/>.</exception>
    public static explicit operator checked Int512(UInt512 value)
    {
        UInt512 maxAsUnsigned = new(~SignBitMask, UInt256.MaxValue);
        if (value > maxAsUnsigned) throw new OverflowException($"Value was either too large or too small for the specified type: {nameof(Int512)}.");
        return new Int512(value.Upper, value.Lower);
    }

    /// <summary>Explicitly converts an <see cref="Int512"/> value to a <see cref="UInt512"/> value, reinterpreting the bits.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the <see cref="UInt512"/> with the same bit pattern as <paramref name="value"/>.</returns>
    public static explicit operator UInt512(Int512 value) => new(value.upper, value.lower);

    /// <summary>Explicitly converts an <see cref="Int512"/> value to a <see cref="UInt512"/> value, throwing on overflow.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the <see cref="UInt512"/> representation of <paramref name="value"/>.</returns>
    /// <exception cref="OverflowException">Thrown when <paramref name="value"/> is negative.</exception>
    public static explicit operator checked UInt512(Int512 value)
    {
        if (IsNegative(value)) throw new OverflowException($"Value was either too large or too small for the specified type: {nameof(UInt512)}.");
        return new UInt512(value.upper, value.lower);
    }

    /// <summary>Explicitly converts a <see cref="BigInteger"/> value to an <see cref="Int512"/>, truncating to the low 512 bits.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the low 512 bits of <paramref name="value"/> interpreted as signed.</returns>
    public static explicit operator Int512(BigInteger value)
    {
        BigInteger mask = (BigInteger.One << BitWidth) - BigInteger.One;
        BigInteger truncated = value & mask;
        UInt512 unsigned = (UInt512)truncated;
        return new Int512(unsigned.Upper, unsigned.Lower);
    }

    /// <summary>Explicitly converts a <see cref="BigInteger"/> value to an <see cref="Int512"/>, throwing on overflow.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the <see cref="Int512"/> representation of <paramref name="value"/>.</returns>
    /// <exception cref="OverflowException">Thrown when <paramref name="value"/> is outside the range of <see cref="Int512"/>.</exception>
    public static explicit operator checked Int512(BigInteger value)
    {
        if (value < (BigInteger)MinValue || value > (BigInteger)MaxValue)
        {
            throw new OverflowException($"Value was either too large or too small for the specified type: {nameof(Int512)}.");
        }
        return (Int512)value;
    }

    /// <summary>Explicitly converts an <see cref="Int512"/> to a <see cref="byte"/>.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the low 8 bits of <paramref name="value"/>.</returns>
    public static explicit operator byte(Int512 value) => (byte)value.lower;

    /// <summary>Explicitly converts an <see cref="Int512"/> to a <see cref="byte"/>, throwing on overflow.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the <see cref="byte"/> representation of <paramref name="value"/>.</returns>
    /// <exception cref="OverflowException">Thrown when <paramref name="value"/> is outside the range of <see cref="byte"/>.</exception>
    public static explicit operator checked byte(Int512 value)
    {
        if (IsNegative(value) || value > (Int512)byte.MaxValue) throw new OverflowException($"Value was either too large or too small for the specified type: {nameof(Byte)}.");
        return (byte)value.lower;
    }

    /// <summary>Explicitly converts an <see cref="Int512"/> to an <see cref="sbyte"/>.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the low 8 bits of <paramref name="value"/> as a signed byte.</returns>
    public static explicit operator sbyte(Int512 value) => (sbyte)value.lower;

    /// <summary>Explicitly converts an <see cref="Int512"/> to an <see cref="sbyte"/>, throwing on overflow.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the <see cref="sbyte"/> representation of <paramref name="value"/>.</returns>
    /// <exception cref="OverflowException">Thrown when <paramref name="value"/> is outside the range of <see cref="sbyte"/>.</exception>
    public static explicit operator checked sbyte(Int512 value)
    {
        if (value < (Int512)sbyte.MinValue || value > (Int512)sbyte.MaxValue) throw new OverflowException($"Value was either too large or too small for the specified type: {nameof(SByte)}.");
        return (sbyte)value.lower;
    }

    /// <summary>Explicitly converts an <see cref="Int512"/> to a <see cref="short"/>.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the low 16 bits of <paramref name="value"/> as a signed short.</returns>
    public static explicit operator short(Int512 value) => (short)value.lower;

    /// <summary>Explicitly converts an <see cref="Int512"/> to a <see cref="short"/>, throwing on overflow.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the <see cref="short"/> representation of <paramref name="value"/>.</returns>
    /// <exception cref="OverflowException">Thrown when <paramref name="value"/> is outside the range of <see cref="short"/>.</exception>
    public static explicit operator checked short(Int512 value)
    {
        if (value < (Int512)short.MinValue || value > (Int512)short.MaxValue) throw new OverflowException($"Value was either too large or too small for the specified type: {nameof(Int16)}.");
        return (short)value.lower;
    }

    /// <summary>Explicitly converts an <see cref="Int512"/> to a <see cref="ushort"/>.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the low 16 bits of <paramref name="value"/>.</returns>
    public static explicit operator ushort(Int512 value) => (ushort)value.lower;

    /// <summary>Explicitly converts an <see cref="Int512"/> to a <see cref="ushort"/>, throwing on overflow.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the <see cref="ushort"/> representation of <paramref name="value"/>.</returns>
    /// <exception cref="OverflowException">Thrown when <paramref name="value"/> is outside the range of <see cref="ushort"/>.</exception>
    public static explicit operator checked ushort(Int512 value)
    {
        if (IsNegative(value) || value > (Int512)ushort.MaxValue) throw new OverflowException($"Value was either too large or too small for the specified type: {nameof(UInt16)}.");
        return (ushort)value.lower;
    }

    /// <summary>Explicitly converts an <see cref="Int512"/> to an <see cref="int"/>.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the low 32 bits of <paramref name="value"/> as a signed int.</returns>
    public static explicit operator int(Int512 value) => (int)value.lower;

    /// <summary>Explicitly converts an <see cref="Int512"/> to an <see cref="int"/>, throwing on overflow.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the <see cref="int"/> representation of <paramref name="value"/>.</returns>
    /// <exception cref="OverflowException">Thrown when <paramref name="value"/> is outside the range of <see cref="int"/>.</exception>
    public static explicit operator checked int(Int512 value)
    {
        if (value < (Int512)int.MinValue || value > (Int512)int.MaxValue) throw new OverflowException($"Value was either too large or too small for the specified type: {nameof(Int32)}.");
        return (int)value.lower;
    }

    /// <summary>Explicitly converts an <see cref="Int512"/> to a <see cref="uint"/>.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the low 32 bits of <paramref name="value"/>.</returns>
    public static explicit operator uint(Int512 value) => (uint)value.lower;

    /// <summary>Explicitly converts an <see cref="Int512"/> to a <see cref="uint"/>, throwing on overflow.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the <see cref="uint"/> representation of <paramref name="value"/>.</returns>
    /// <exception cref="OverflowException">Thrown when <paramref name="value"/> is outside the range of <see cref="uint"/>.</exception>
    public static explicit operator checked uint(Int512 value)
    {
        if (IsNegative(value) || value > (Int512)uint.MaxValue) throw new OverflowException($"Value was either too large or too small for the specified type: {nameof(UInt32)}.");
        return (uint)value.lower;
    }

    /// <summary>Explicitly converts an <see cref="Int512"/> to a <see cref="long"/>.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the low 64 bits of <paramref name="value"/> as a signed long.</returns>
    public static explicit operator long(Int512 value) => (long)value.lower;

    /// <summary>Explicitly converts an <see cref="Int512"/> to a <see cref="long"/>, throwing on overflow.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the <see cref="long"/> representation of <paramref name="value"/>.</returns>
    /// <exception cref="OverflowException">Thrown when <paramref name="value"/> is outside the range of <see cref="long"/>.</exception>
    public static explicit operator checked long(Int512 value)
    {
        if (value < (Int512)long.MinValue || value > (Int512)long.MaxValue) throw new OverflowException($"Value was either too large or too small for the specified type: {nameof(Int64)}.");
        return (long)value.lower;
    }

    /// <summary>Explicitly converts an <see cref="Int512"/> to a <see cref="ulong"/>.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the low 64 bits of <paramref name="value"/>.</returns>
    public static explicit operator ulong(Int512 value) => (ulong)value.lower;

    /// <summary>Explicitly converts an <see cref="Int512"/> to a <see cref="ulong"/>, throwing on overflow.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the <see cref="ulong"/> representation of <paramref name="value"/>.</returns>
    /// <exception cref="OverflowException">Thrown when <paramref name="value"/> is outside the range of <see cref="ulong"/>.</exception>
    public static explicit operator checked ulong(Int512 value)
    {
        if (IsNegative(value) || value > (Int512)ulong.MaxValue) throw new OverflowException($"Value was either too large or too small for the specified type: {nameof(UInt64)}.");
        return (ulong)value.lower;
    }

    /// <summary>Explicitly converts an <see cref="Int512"/> to an <see cref="Int128"/>.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the low 128 bits of <paramref name="value"/> as a signed <see cref="Int128"/>.</returns>
    public static explicit operator Int128(Int512 value) => (Int128)(UInt128)value.lower;

    /// <summary>Explicitly converts an <see cref="Int512"/> to an <see cref="Int128"/>, throwing on overflow.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the <see cref="Int128"/> representation of <paramref name="value"/>.</returns>
    /// <exception cref="OverflowException">Thrown when <paramref name="value"/> is outside the range of <see cref="Int128"/>.</exception>
    public static explicit operator checked Int128(Int512 value)
    {
        if (value < (Int512)Int128.MinValue || value > (Int512)Int128.MaxValue) throw new OverflowException($"Value was either too large or too small for the specified type: {nameof(Int128)}.");
        return (Int128)(UInt128)value.lower;
    }

    /// <summary>Explicitly converts an <see cref="Int512"/> to a <see cref="UInt128"/>.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the low 128 bits of <paramref name="value"/>.</returns>
    public static explicit operator UInt128(Int512 value) => (UInt128)value.lower;

    /// <summary>Explicitly converts an <see cref="Int512"/> to a <see cref="UInt128"/>, throwing on overflow.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the <see cref="UInt128"/> representation of <paramref name="value"/>.</returns>
    /// <exception cref="OverflowException">Thrown when <paramref name="value"/> is negative or exceeds <see cref="UInt128.MaxValue"/>.</exception>
    public static explicit operator checked UInt128(Int512 value)
    {
        if (IsNegative(value) || !UInt256.IsZero(value.upper) || value.lower > (UInt256)UInt128.MaxValue) throw new OverflowException($"Value was either too large or too small for the specified type: {nameof(UInt128)}.");
        return (UInt128)value.lower;
    }

    /// <summary>Explicitly converts an <see cref="Int512"/> to an <see cref="Int256"/>.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the low 256 bits of <paramref name="value"/> as a signed <see cref="Int256"/>.</returns>
    public static explicit operator Int256(Int512 value) => Int256.ReinterpretAsSigned(value.lower);

    /// <summary>Explicitly converts an <see cref="Int512"/> to an <see cref="Int256"/>, throwing on overflow.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the <see cref="Int256"/> representation of <paramref name="value"/>.</returns>
    /// <exception cref="OverflowException">Thrown when <paramref name="value"/> is outside the range of <see cref="Int256"/>.</exception>
    public static explicit operator checked Int256(Int512 value)
    {
        if (value < (Int512)Int256.MinValue || value > (Int512)Int256.MaxValue) throw new OverflowException($"Value was either too large or too small for the specified type: {nameof(Int256)}.");
        return Int256.ReinterpretAsSigned(value.lower);
    }

    /// <summary>Explicitly converts an <see cref="Int512"/> to a <see cref="UInt256"/>.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the low 256 bits of <paramref name="value"/>.</returns>
    public static explicit operator UInt256(Int512 value) => value.lower;

    /// <summary>Explicitly converts an <see cref="Int512"/> to a <see cref="UInt256"/>, throwing on overflow.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the <see cref="UInt256"/> representation of <paramref name="value"/>.</returns>
    /// <exception cref="OverflowException">Thrown when <paramref name="value"/> is negative or exceeds <see cref="UInt256.MaxValue"/>.</exception>
    public static explicit operator checked UInt256(Int512 value)
    {
        if (IsNegative(value) || !UInt256.IsZero(value.upper)) throw new OverflowException($"Value was either too large or too small for the specified type: {nameof(UInt256)}.");
        return value.lower;
    }

    /// <summary>Explicitly converts an <see cref="Int512"/> to a <see cref="char"/>.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the low 16 bits of <paramref name="value"/> as a <see cref="char"/>.</returns>
    public static explicit operator char(Int512 value) => (char)value.lower;

    /// <summary>Explicitly converts an <see cref="Int512"/> to a <see cref="char"/>, throwing on overflow.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the <see cref="char"/> representation of <paramref name="value"/>.</returns>
    /// <exception cref="OverflowException">Thrown when <paramref name="value"/> is outside the range of <see cref="char"/>.</exception>
    public static explicit operator checked char(Int512 value)
    {
        if (IsNegative(value) || value > (Int512)char.MaxValue) throw new OverflowException($"Value was either too large or too small for the specified type: {nameof(Char)}.");
        return (char)value.lower;
    }

    /// <summary>Implicitly converts an <see cref="Int512"/> to a <see cref="BigInteger"/>.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the <see cref="BigInteger"/> representation of <paramref name="value"/>.</returns>
    public static implicit operator BigInteger(Int512 value)
    {
        UInt512 unsigned = new(value.upper, value.lower);
        if (!IsNegative(value)) return (BigInteger)unsigned;
        UInt512 magnitude = UInt512.Zero - unsigned;
        return -(BigInteger)magnitude;
    }

    /// <summary>Explicitly converts an <see cref="Int512"/> to a <see cref="float"/>.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the <see cref="float"/> closest to <paramref name="value"/>.</returns>
    public static explicit operator float(Int512 value) => (float)(double)value;

    /// <summary>Explicitly converts an <see cref="Int512"/> to a <see cref="double"/>.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the <see cref="double"/> closest to <paramref name="value"/>.</returns>
    public static explicit operator double(Int512 value)
    {
        bool isNegative = IsNegative(value);
        UInt512 magnitude = AbsToUnsigned(value);
        double result = (double)magnitude;
        return isNegative ? -result : result;
    }

    /// <summary>Explicitly converts an <see cref="Int512"/> to a <see cref="Half"/>.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the <see cref="Half"/> closest to <paramref name="value"/>.</returns>
    public static explicit operator Half(Int512 value) => (Half)(double)value;

    /// <summary>Explicitly converts an <see cref="Int512"/> to a <see cref="decimal"/>.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the <see cref="decimal"/> representation of <paramref name="value"/>.</returns>
    /// <exception cref="OverflowException">Thrown when <paramref name="value"/> is outside the range of <see cref="decimal"/>.</exception>
    public static explicit operator decimal(Int512 value) => (decimal)(BigInteger)value;

    /// <summary>Explicitly converts an <see cref="Int512"/> value to a <see cref="Float128"/>.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the <see cref="Float128"/> nearest to <paramref name="value"/>, with rounding to nearest, ties-to-even.</returns>
    public static explicit operator Float128(Int512 value) => (Float128)(BigDecimal)(BigInteger)value;

    /// <summary>Explicitly converts an <see cref="Int512"/> value to a <see cref="Float256"/>.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the <see cref="Float256"/> nearest to <paramref name="value"/>, with rounding to nearest, ties-to-even.</returns>
    public static explicit operator Float256(Int512 value) => (Float256)(BigDecimal)(BigInteger)value;

    /// <summary>Explicitly converts a <see cref="Float128"/> value to an <see cref="Int512"/>, truncating toward zero.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the truncated <see cref="Int512"/> representation of <paramref name="value"/>, wrapping for out-of-range or non-finite inputs.</returns>
    public static explicit operator Int512(Float128 value)
    {
        if (Float128.IsNaN(value) || Float128.IsInfinity(value) || Float128.IsZero(value)) return Zero;
        return (Int512)(BigInteger)(BigDecimal)Float128.Truncate(value);
    }

    /// <summary>Explicitly converts a <see cref="Float128"/> value to an <see cref="Int512"/>, throwing on overflow or non-finite input.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the truncated <see cref="Int512"/> representation of <paramref name="value"/>.</returns>
    /// <exception cref="OverflowException">Thrown when <paramref name="value"/> is NaN, infinite, or outside the range of <see cref="Int512"/>.</exception>
    public static explicit operator checked Int512(Float128 value)
    {
        if (Float128.IsNaN(value) || Float128.IsInfinity(value))
        {
            throw new OverflowException($"Value was either too large or too small for the specified type: {nameof(Int512)}.");
        }
        return checked((Int512)(BigInteger)(BigDecimal)Float128.Truncate(value));
    }

    /// <summary>Explicitly converts a <see cref="Float256"/> value to an <see cref="Int512"/>, truncating toward zero.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the truncated <see cref="Int512"/> representation of <paramref name="value"/>, wrapping for out-of-range or non-finite inputs.</returns>
    public static explicit operator Int512(Float256 value)
    {
        if (Float256.IsNaN(value) || Float256.IsInfinity(value) || Float256.IsZero(value)) return Zero;
        return (Int512)(BigInteger)(BigDecimal)Float256.Truncate(value);
    }

    /// <summary>Explicitly converts a <see cref="Float256"/> value to an <see cref="Int512"/>, throwing on overflow or non-finite input.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the truncated <see cref="Int512"/> representation of <paramref name="value"/>.</returns>
    /// <exception cref="OverflowException">Thrown when <paramref name="value"/> is NaN, infinite, or outside the range of <see cref="Int512"/>.</exception>
    public static explicit operator checked Int512(Float256 value)
    {
        if (Float256.IsNaN(value) || Float256.IsInfinity(value))
        {
            throw new OverflowException($"Value was either too large or too small for the specified type: {nameof(Int512)}.");
        }
        return checked((Int512)(BigInteger)(BigDecimal)Float256.Truncate(value));
    }
}
