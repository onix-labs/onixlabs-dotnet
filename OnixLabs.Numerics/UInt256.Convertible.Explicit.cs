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

public readonly partial struct UInt256
{
    /// <summary>Explicitly converts an <see cref="sbyte"/> value to a <see cref="UInt256"/> value.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the <see cref="UInt256"/> representation of <paramref name="value"/>, wrapping negatives via two's-complement.</returns>
    public static explicit operator UInt256(sbyte value)
    {
        if (value >= 0) return new UInt256(UInt128.Zero, (UInt128)(byte)value);
        return new UInt256(UInt128.MaxValue, (UInt128)(Int128)value);
    }

    /// <summary>Explicitly converts an <see cref="sbyte"/> value to a <see cref="UInt256"/> value, throwing on negative input.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the <see cref="UInt256"/> representation of <paramref name="value"/>.</returns>
    /// <exception cref="OverflowException">Thrown when <paramref name="value"/> is negative.</exception>
    public static explicit operator checked UInt256(sbyte value)
    {
        if (value < 0) throw new OverflowException($"Value was either too large or too small for the specified type: {nameof(UInt256)}.");
        return new UInt256(UInt128.Zero, (UInt128)(byte)value);
    }

    /// <summary>Explicitly converts a <see cref="short"/> value to a <see cref="UInt256"/> value.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the <see cref="UInt256"/> representation of <paramref name="value"/>, wrapping negatives via two's-complement.</returns>
    public static explicit operator UInt256(short value)
    {
        if (value >= 0) return new UInt256(UInt128.Zero, (UInt128)(ushort)value);
        return new UInt256(UInt128.MaxValue, (UInt128)(Int128)value);
    }

    /// <summary>Explicitly converts a <see cref="short"/> value to a <see cref="UInt256"/> value, throwing on negative input.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the <see cref="UInt256"/> representation of <paramref name="value"/>.</returns>
    /// <exception cref="OverflowException">Thrown when <paramref name="value"/> is negative.</exception>
    public static explicit operator checked UInt256(short value)
    {
        if (value < 0) throw new OverflowException($"Value was either too large or too small for the specified type: {nameof(UInt256)}.");
        return new UInt256(UInt128.Zero, (UInt128)(ushort)value);
    }

    /// <summary>Explicitly converts an <see cref="int"/> value to a <see cref="UInt256"/> value.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the <see cref="UInt256"/> representation of <paramref name="value"/>, wrapping negatives via two's-complement.</returns>
    public static explicit operator UInt256(int value)
    {
        if (value >= 0) return new UInt256(UInt128.Zero, (UInt128)(uint)value);
        return new UInt256(UInt128.MaxValue, (UInt128)(Int128)value);
    }

    /// <summary>Explicitly converts an <see cref="int"/> value to a <see cref="UInt256"/> value, throwing on negative input.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the <see cref="UInt256"/> representation of <paramref name="value"/>.</returns>
    /// <exception cref="OverflowException">Thrown when <paramref name="value"/> is negative.</exception>
    public static explicit operator checked UInt256(int value)
    {
        if (value < 0) throw new OverflowException($"Value was either too large or too small for the specified type: {nameof(UInt256)}.");
        return new UInt256(UInt128.Zero, (UInt128)(uint)value);
    }

    /// <summary>Explicitly converts a <see cref="long"/> value to a <see cref="UInt256"/> value.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the <see cref="UInt256"/> representation of <paramref name="value"/>, wrapping negatives via two's-complement.</returns>
    public static explicit operator UInt256(long value)
    {
        if (value >= 0) return new UInt256(UInt128.Zero, (UInt128)(ulong)value);
        return new UInt256(UInt128.MaxValue, (UInt128)(Int128)value);
    }

    /// <summary>Explicitly converts a <see cref="long"/> value to a <see cref="UInt256"/> value, throwing on negative input.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the <see cref="UInt256"/> representation of <paramref name="value"/>.</returns>
    /// <exception cref="OverflowException">Thrown when <paramref name="value"/> is negative.</exception>
    public static explicit operator checked UInt256(long value)
    {
        if (value < 0) throw new OverflowException($"Value was either too large or too small for the specified type: {nameof(UInt256)}.");
        return new UInt256(UInt128.Zero, (UInt128)(ulong)value);
    }

    /// <summary>Explicitly converts an <see cref="Int128"/> value to a <see cref="UInt256"/> value.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the <see cref="UInt256"/> representation of <paramref name="value"/>, wrapping negatives via two's-complement.</returns>
    public static explicit operator UInt256(Int128 value)
    {
        if (value >= Int128.Zero) return new UInt256(UInt128.Zero, (UInt128)value);
        return new UInt256(UInt128.MaxValue, (UInt128)value);
    }

    /// <summary>Explicitly converts an <see cref="Int128"/> value to a <see cref="UInt256"/> value, throwing on negative input.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the <see cref="UInt256"/> representation of <paramref name="value"/>.</returns>
    /// <exception cref="OverflowException">Thrown when <paramref name="value"/> is negative.</exception>
    public static explicit operator checked UInt256(Int128 value)
    {
        if (value < Int128.Zero) throw new OverflowException($"Value was either too large or too small for the specified type: {nameof(UInt256)}.");
        return new UInt256(UInt128.Zero, (UInt128)value);
    }

    /// <summary>Explicitly converts a <see cref="BigInteger"/> value to a <see cref="UInt256"/> value, truncating to 256 bits.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the low 256 bits of <paramref name="value"/>.</returns>
    public static explicit operator UInt256(BigInteger value)
    {
        BigInteger mask = (BigInteger.One << BitWidth) - BigInteger.One;
        BigInteger truncated = value & mask;
        return BigIntegerToUInt256(truncated);
    }

    /// <summary>Explicitly converts a <see cref="BigInteger"/> value to a <see cref="UInt256"/> value, throwing on overflow.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the <see cref="UInt256"/> representation of <paramref name="value"/>.</returns>
    /// <exception cref="OverflowException">Thrown when <paramref name="value"/> is negative or exceeds <see cref="MaxValue"/>.</exception>
    public static explicit operator checked UInt256(BigInteger value)
    {
        if (value.Sign < 0) throw new OverflowException($"Value was either too large or too small for the specified type: {nameof(UInt256)}.");
        if (value.GetBitLength() > BitWidth) throw new OverflowException($"Value was either too large or too small for the specified type: {nameof(UInt256)}.");
        return BigIntegerToUInt256(value);
    }

    /// <summary>Explicitly converts a <see cref="UInt256"/> value to a <see cref="byte"/>.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the low 8 bits of <paramref name="value"/>.</returns>
    public static explicit operator byte(UInt256 value) => (byte)value.lower;

    /// <summary>Explicitly converts a <see cref="UInt256"/> value to a <see cref="byte"/>, throwing on overflow.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the <see cref="byte"/> representation of <paramref name="value"/>.</returns>
    /// <exception cref="OverflowException">Thrown when <paramref name="value"/> exceeds <see cref="byte.MaxValue"/>.</exception>
    public static explicit operator checked byte(UInt256 value)
    {
        if (value > byte.MaxValue) throw new OverflowException($"Value was either too large or too small for the specified type: {nameof(Byte)}.");
        return (byte)value.lower;
    }

    /// <summary>Explicitly converts a <see cref="UInt256"/> value to an <see cref="sbyte"/>.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the low 8 bits of <paramref name="value"/> as a signed byte.</returns>
    public static explicit operator sbyte(UInt256 value) => (sbyte)value.lower;

    /// <summary>Explicitly converts a <see cref="UInt256"/> value to an <see cref="sbyte"/>, throwing on overflow.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the <see cref="sbyte"/> representation of <paramref name="value"/>.</returns>
    /// <exception cref="OverflowException">Thrown when <paramref name="value"/> exceeds <see cref="sbyte.MaxValue"/>.</exception>
    public static explicit operator checked sbyte(UInt256 value)
    {
        if (value > (UInt256)(byte)sbyte.MaxValue) throw new OverflowException($"Value was either too large or too small for the specified type: {nameof(SByte)}.");
        return (sbyte)value.lower;
    }

    /// <summary>Explicitly converts a <see cref="UInt256"/> value to a <see cref="short"/>.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the low 16 bits of <paramref name="value"/> as a signed short.</returns>
    public static explicit operator short(UInt256 value) => (short)value.lower;

    /// <summary>Explicitly converts a <see cref="UInt256"/> value to a <see cref="short"/>, throwing on overflow.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the <see cref="short"/> representation of <paramref name="value"/>.</returns>
    /// <exception cref="OverflowException">Thrown when <paramref name="value"/> exceeds <see cref="short.MaxValue"/>.</exception>
    public static explicit operator checked short(UInt256 value)
    {
        if (value > (UInt256)(ushort)short.MaxValue) throw new OverflowException($"Value was either too large or too small for the specified type: {nameof(Int16)}.");
        return (short)value.lower;
    }

    /// <summary>Explicitly converts a <see cref="UInt256"/> value to a <see cref="ushort"/>.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the low 16 bits of <paramref name="value"/>.</returns>
    public static explicit operator ushort(UInt256 value) => (ushort)value.lower;

    /// <summary>Explicitly converts a <see cref="UInt256"/> value to a <see cref="ushort"/>, throwing on overflow.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the <see cref="ushort"/> representation of <paramref name="value"/>.</returns>
    /// <exception cref="OverflowException">Thrown when <paramref name="value"/> exceeds <see cref="ushort.MaxValue"/>.</exception>
    public static explicit operator checked ushort(UInt256 value)
    {
        if (value > ushort.MaxValue) throw new OverflowException($"Value was either too large or too small for the specified type: {nameof(UInt16)}.");
        return (ushort)value.lower;
    }

    /// <summary>Explicitly converts a <see cref="UInt256"/> value to an <see cref="int"/>.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the low 32 bits of <paramref name="value"/> as a signed int.</returns>
    public static explicit operator int(UInt256 value) => (int)value.lower;

    /// <summary>Explicitly converts a <see cref="UInt256"/> value to an <see cref="int"/>, throwing on overflow.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the <see cref="int"/> representation of <paramref name="value"/>.</returns>
    /// <exception cref="OverflowException">Thrown when <paramref name="value"/> exceeds <see cref="int.MaxValue"/>.</exception>
    public static explicit operator checked int(UInt256 value)
    {
        if (value > (UInt256)(uint)int.MaxValue) throw new OverflowException($"Value was either too large or too small for the specified type: {nameof(Int32)}.");
        return (int)value.lower;
    }

    /// <summary>Explicitly converts a <see cref="UInt256"/> value to a <see cref="uint"/>.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the low 32 bits of <paramref name="value"/>.</returns>
    public static explicit operator uint(UInt256 value) => (uint)value.lower;

    /// <summary>Explicitly converts a <see cref="UInt256"/> value to a <see cref="uint"/>, throwing on overflow.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the <see cref="uint"/> representation of <paramref name="value"/>.</returns>
    /// <exception cref="OverflowException">Thrown when <paramref name="value"/> exceeds <see cref="uint.MaxValue"/>.</exception>
    public static explicit operator checked uint(UInt256 value)
    {
        if (value > uint.MaxValue) throw new OverflowException($"Value was either too large or too small for the specified type: {nameof(UInt32)}.");
        return (uint)value.lower;
    }

    /// <summary>Explicitly converts a <see cref="UInt256"/> value to a <see cref="long"/>.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the low 64 bits of <paramref name="value"/> as a signed long.</returns>
    public static explicit operator long(UInt256 value) => (long)value.lower;

    /// <summary>Explicitly converts a <see cref="UInt256"/> value to a <see cref="long"/>, throwing on overflow.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the <see cref="long"/> representation of <paramref name="value"/>.</returns>
    /// <exception cref="OverflowException">Thrown when <paramref name="value"/> exceeds <see cref="long.MaxValue"/>.</exception>
    public static explicit operator checked long(UInt256 value)
    {
        if (value > (UInt256)(ulong)long.MaxValue) throw new OverflowException($"Value was either too large or too small for the specified type: {nameof(Int64)}.");
        return (long)value.lower;
    }

    /// <summary>Explicitly converts a <see cref="UInt256"/> value to a <see cref="ulong"/>.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the low 64 bits of <paramref name="value"/>.</returns>
    public static explicit operator ulong(UInt256 value) => (ulong)value.lower;

    /// <summary>Explicitly converts a <see cref="UInt256"/> value to a <see cref="ulong"/>, throwing on overflow.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the <see cref="ulong"/> representation of <paramref name="value"/>.</returns>
    /// <exception cref="OverflowException">Thrown when <paramref name="value"/> exceeds <see cref="ulong.MaxValue"/>.</exception>
    public static explicit operator checked ulong(UInt256 value)
    {
        if (value > ulong.MaxValue) throw new OverflowException($"Value was either too large or too small for the specified type: {nameof(UInt64)}.");
        return (ulong)value.lower;
    }

    /// <summary>Explicitly converts a <see cref="UInt256"/> value to a <see cref="UInt128"/>.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the low 128 bits of <paramref name="value"/>.</returns>
    public static explicit operator UInt128(UInt256 value) => value.lower;

    /// <summary>Explicitly converts a <see cref="UInt256"/> value to a <see cref="UInt128"/>, throwing on overflow.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the <see cref="UInt128"/> representation of <paramref name="value"/>.</returns>
    /// <exception cref="OverflowException">Thrown when <paramref name="value"/> exceeds <see cref="UInt128.MaxValue"/>.</exception>
    public static explicit operator checked UInt128(UInt256 value)
    {
        if (value.upper != UInt128.Zero) throw new OverflowException($"Value was either too large or too small for the specified type: {nameof(UInt128)}.");
        return value.lower;
    }

    /// <summary>Explicitly converts a <see cref="UInt256"/> value to an <see cref="Int128"/>.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the low 128 bits of <paramref name="value"/> as a signed <see cref="Int128"/>.</returns>
    public static explicit operator Int128(UInt256 value) => (Int128)value.lower;

    /// <summary>Explicitly converts a <see cref="UInt256"/> value to an <see cref="Int128"/>, throwing on overflow.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the <see cref="Int128"/> representation of <paramref name="value"/>.</returns>
    /// <exception cref="OverflowException">Thrown when <paramref name="value"/> exceeds <see cref="Int128.MaxValue"/>.</exception>
    public static explicit operator checked Int128(UInt256 value)
    {
        if (value.upper != UInt128.Zero || value.lower > (UInt128)Int128.MaxValue) throw new OverflowException($"Value was either too large or too small for the specified type: {nameof(Int128)}.");
        return (Int128)value.lower;
    }

    /// <summary>Explicitly converts a <see cref="UInt256"/> value to a <see cref="char"/>.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the low 16 bits of <paramref name="value"/> as a <see cref="char"/>.</returns>
    public static explicit operator char(UInt256 value) => (char)value.lower;

    /// <summary>Explicitly converts a <see cref="UInt256"/> value to a <see cref="char"/>, throwing on overflow.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the <see cref="char"/> representation of <paramref name="value"/>.</returns>
    /// <exception cref="OverflowException">Thrown when <paramref name="value"/> exceeds <see cref="char.MaxValue"/>.</exception>
    public static explicit operator checked char(UInt256 value)
    {
        if (value > char.MaxValue) throw new OverflowException($"Value was either too large or too small for the specified type: {nameof(Char)}.");
        return (char)value.lower;
    }

    /// <summary>Explicitly converts a <see cref="UInt256"/> value to a <see cref="BigInteger"/>.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the <see cref="BigInteger"/> representation of <paramref name="value"/>.</returns>
    public static implicit operator BigInteger(UInt256 value) => UInt256ToBigInteger(value);

    /// <summary>Explicitly converts a <see cref="UInt256"/> value to a <see cref="float"/>.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the <see cref="float"/> closest to <paramref name="value"/>.</returns>
    public static explicit operator float(UInt256 value) => (float)(double)value;

    /// <summary>Explicitly converts a <see cref="UInt256"/> value to a <see cref="double"/>.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the <see cref="double"/> closest to <paramref name="value"/>.</returns>
    public static explicit operator double(UInt256 value)
    {
        if (value.upper == UInt128.Zero) return (double)value.lower;
        return (double)value.upper * 340282366920938463463374607431768211456.0 + (double)value.lower;
    }

    /// <summary>Explicitly converts a <see cref="UInt256"/> value to a <see cref="Half"/>.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the <see cref="Half"/> closest to <paramref name="value"/>.</returns>
    public static explicit operator Half(UInt256 value) => (Half)(double)value;

    /// <summary>Explicitly converts a <see cref="UInt256"/> value to a <see cref="decimal"/>.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the <see cref="decimal"/> representation of <paramref name="value"/>.</returns>
    /// <exception cref="OverflowException">Thrown when <paramref name="value"/> exceeds the range of <see cref="decimal"/>.</exception>
    public static explicit operator decimal(UInt256 value) => (decimal)(BigInteger)value;

    /// <summary>Explicitly converts a <see cref="UInt256"/> value to a <see cref="Float128"/>.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the <see cref="Float128"/> nearest to <paramref name="value"/>, with rounding to nearest, ties-to-even.</returns>
    public static explicit operator Float128(UInt256 value) => (Float128)(BigDecimal)(BigInteger)value;

    /// <summary>Explicitly converts a <see cref="UInt256"/> value to a <see cref="Float256"/>.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the <see cref="Float256"/> nearest to <paramref name="value"/>, with rounding to nearest, ties-to-even.</returns>
    public static explicit operator Float256(UInt256 value) => (Float256)(BigDecimal)(BigInteger)value;

    /// <summary>Explicitly converts a <see cref="Float128"/> value to a <see cref="UInt256"/>, truncating toward zero.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the truncated <see cref="UInt256"/> representation of <paramref name="value"/>, wrapping for out-of-range or non-finite inputs.</returns>
    public static explicit operator UInt256(Float128 value)
    {
        if (Float128.IsNaN(value) || Float128.IsInfinity(value)) return Zero;
        if (Float128.IsNegative(value) || Float128.IsZero(value)) return Zero;
        return (UInt256)(BigInteger)(BigDecimal)Float128.Truncate(value);
    }

    /// <summary>Explicitly converts a <see cref="Float128"/> value to a <see cref="UInt256"/>, throwing on overflow or non-finite input.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the truncated <see cref="UInt256"/> representation of <paramref name="value"/>.</returns>
    /// <exception cref="OverflowException">Thrown when <paramref name="value"/> is NaN, infinite, negative, or exceeds <see cref="MaxValue"/>.</exception>
    public static explicit operator checked UInt256(Float128 value)
    {
        if (Float128.IsNaN(value) || Float128.IsInfinity(value) || Float128.IsNegative(value))
        {
            throw new OverflowException($"Value was either too large or too small for the specified type: {nameof(UInt256)}.");
        }
        return checked((UInt256)(BigInteger)(BigDecimal)Float128.Truncate(value));
    }

    /// <summary>Explicitly converts a <see cref="Float256"/> value to a <see cref="UInt256"/>, truncating toward zero.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the truncated <see cref="UInt256"/> representation of <paramref name="value"/>, wrapping for out-of-range or non-finite inputs.</returns>
    public static explicit operator UInt256(Float256 value)
    {
        if (Float256.IsNaN(value) || Float256.IsInfinity(value)) return Zero;
        if (Float256.IsNegative(value) || Float256.IsZero(value)) return Zero;
        return (UInt256)(BigInteger)(BigDecimal)Float256.Truncate(value);
    }

    /// <summary>Explicitly converts a <see cref="Float256"/> value to a <see cref="UInt256"/>, throwing on overflow or non-finite input.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the truncated <see cref="UInt256"/> representation of <paramref name="value"/>.</returns>
    /// <exception cref="OverflowException">Thrown when <paramref name="value"/> is NaN, infinite, negative, or exceeds <see cref="MaxValue"/>.</exception>
    public static explicit operator checked UInt256(Float256 value)
    {
        if (Float256.IsNaN(value) || Float256.IsInfinity(value) || Float256.IsNegative(value))
        {
            throw new OverflowException($"Value was either too large or too small for the specified type: {nameof(UInt256)}.");
        }
        return checked((UInt256)(BigInteger)(BigDecimal)Float256.Truncate(value));
    }

    /// <summary>
    /// Converts the specified non-negative <see cref="BigInteger"/> value into a <see cref="UInt256"/> value by assembling it from 64-bit chunks.
    /// </summary>
    /// <param name="value">The <see cref="BigInteger"/> value to convert; callers are responsible for ensuring the value is non-negative and fits within 256 bits.</param>
    /// <returns>Returns the <see cref="UInt256"/> representation of <paramref name="value"/>.</returns>
    internal static UInt256 BigIntegerToUInt256(BigInteger value)
    {
        UInt256 result = Zero;
        BigInteger remainingBits = value;
        for (int shift = 0; shift < BitWidth && !remainingBits.IsZero; shift += 64)
        {
            ulong chunk = (ulong)(remainingBits & (BigInteger)ulong.MaxValue);
            if (chunk != 0UL)
            {
                UInt256 contribution = new UInt256(UInt128.Zero, (UInt128)chunk) << shift;
                result = result | contribution;
            }
            remainingBits >>= 64;
        }
        return result;
    }

    /// <summary>
    /// Converts the specified <see cref="UInt256"/> value into a non-negative <see cref="BigInteger"/> value.
    /// </summary>
    /// <param name="value">The <see cref="UInt256"/> value to convert.</param>
    /// <returns>Returns the <see cref="BigInteger"/> representation of <paramref name="value"/>.</returns>
    internal static BigInteger UInt256ToBigInteger(UInt256 value)
    {
        BigInteger high = ((BigInteger)(ulong)(value.upper >> 64) << 64) | (ulong)value.upper;
        BigInteger low = ((BigInteger)(ulong)(value.lower >> 64) << 64) | (ulong)value.lower;
        return (high << 128) | low;
    }
}
