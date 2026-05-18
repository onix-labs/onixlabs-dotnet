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
using System.Diagnostics.CodeAnalysis;
using System.Numerics;

namespace OnixLabs.Numerics;

public readonly partial struct Int256
{
    static bool INumberBase<Int256>.TryConvertFromChecked<TOther>(TOther value, out Int256 result) => TryConvertFromChecked(value, out result);
    static bool INumberBase<Int256>.TryConvertFromSaturating<TOther>(TOther value, out Int256 result) => TryConvertFromSaturating(value, out result);
    static bool INumberBase<Int256>.TryConvertFromTruncating<TOther>(TOther value, out Int256 result) => TryConvertFromTruncating(value, out result);
    static bool INumberBase<Int256>.TryConvertToChecked<TOther>(Int256 value, [MaybeNullWhen(false)] out TOther result) => TryConvertToChecked(value, out result);
    static bool INumberBase<Int256>.TryConvertToSaturating<TOther>(Int256 value, [MaybeNullWhen(false)] out TOther result) => TryConvertToSaturating(value, out result);
    static bool INumberBase<Int256>.TryConvertToTruncating<TOther>(Int256 value, [MaybeNullWhen(false)] out TOther result) => TryConvertToTruncating(value, out result);

    /// <summary>
    /// Tries to convert the specified <typeparamref name="TOther"/> value to an <see cref="Int256"/> value, throwing on overflow.
    /// </summary>
    /// <typeparam name="TOther">The type of the source value.</typeparam>
    /// <param name="value">The value to convert.</param>
    /// <param name="result">When this method returns, contains the converted value if the source type is supported; otherwise, the default value.</param>
    /// <returns>Returns <see langword="true"/> if the source type is supported; otherwise, <see langword="false"/>.</returns>
    /// <exception cref="OverflowException">Thrown when <paramref name="value"/> is outside the range of <see cref="Int256"/>.</exception>
    private static bool TryConvertFromChecked<TOther>(TOther value, out Int256 result) where TOther : INumberBase<TOther>
    {
        if (typeof(TOther) == typeof(sbyte)) { result = (sbyte)(object)value!; return true; }
        if (typeof(TOther) == typeof(byte)) { result = (byte)(object)value!; return true; }
        if (typeof(TOther) == typeof(short)) { result = (short)(object)value!; return true; }
        if (typeof(TOther) == typeof(ushort)) { result = (ushort)(object)value!; return true; }
        if (typeof(TOther) == typeof(int)) { result = (int)(object)value!; return true; }
        if (typeof(TOther) == typeof(uint)) { result = (uint)(object)value!; return true; }
        if (typeof(TOther) == typeof(long)) { result = (long)(object)value!; return true; }
        if (typeof(TOther) == typeof(ulong)) { result = (ulong)(object)value!; return true; }
        if (typeof(TOther) == typeof(Int128)) { result = (Int128)(object)value!; return true; }
        if (typeof(TOther) == typeof(UInt128)) { result = (UInt128)(object)value!; return true; }
        if (typeof(TOther) == typeof(UInt256)) { result = checked((Int256)(UInt256)(object)value!); return true; }
        if (typeof(TOther) == typeof(BigInteger)) { result = checked((Int256)(BigInteger)(object)value!); return true; }
        if (typeof(TOther) == typeof(char)) { result = (char)(object)value!; return true; }
        if (typeof(TOther) == typeof(Int256)) { result = (Int256)(object)value!; return true; }
        if (typeof(TOther) == typeof(Float128)) { result = checked((Int256)(Float128)(object)value!); return true; }
        if (typeof(TOther) == typeof(Float256)) { result = checked((Int256)(Float256)(object)value!); return true; }

        result = default;
        return false;
    }

    /// <summary>
    /// Tries to convert the specified <typeparamref name="TOther"/> value to an <see cref="Int256"/> value, saturating values that fall outside the representable range.
    /// </summary>
    /// <typeparam name="TOther">The type of the source value.</typeparam>
    /// <param name="value">The value to convert.</param>
    /// <param name="result">When this method returns, contains the saturated value if the source type is supported; otherwise, the default value.</param>
    /// <returns>Returns <see langword="true"/> if the source type is supported; otherwise, <see langword="false"/>.</returns>
    private static bool TryConvertFromSaturating<TOther>(TOther value, out Int256 result) where TOther : INumberBase<TOther>
    {
        if (typeof(TOther) == typeof(sbyte)) { result = (sbyte)(object)value!; return true; }
        if (typeof(TOther) == typeof(byte)) { result = (byte)(object)value!; return true; }
        if (typeof(TOther) == typeof(short)) { result = (short)(object)value!; return true; }
        if (typeof(TOther) == typeof(ushort)) { result = (ushort)(object)value!; return true; }
        if (typeof(TOther) == typeof(int)) { result = (int)(object)value!; return true; }
        if (typeof(TOther) == typeof(uint)) { result = (uint)(object)value!; return true; }
        if (typeof(TOther) == typeof(long)) { result = (long)(object)value!; return true; }
        if (typeof(TOther) == typeof(ulong)) { result = (ulong)(object)value!; return true; }
        if (typeof(TOther) == typeof(Int128)) { result = (Int128)(object)value!; return true; }
        if (typeof(TOther) == typeof(UInt128)) { result = (UInt128)(object)value!; return true; }
        if (typeof(TOther) == typeof(UInt256))
        {
            UInt256 v = (UInt256)(object)value!;
            result = v > (UInt256)MaxValue ? MaxValue : (Int256)v;
            return true;
        }
        if (typeof(TOther) == typeof(BigInteger))
        {
            BigInteger v = (BigInteger)(object)value!;
            if (v < (BigInteger)MinValue) { result = MinValue; return true; }
            if (v > (BigInteger)MaxValue) { result = MaxValue; return true; }
            result = (Int256)v;
            return true;
        }
        if (typeof(TOther) == typeof(char)) { result = (char)(object)value!; return true; }
        if (typeof(TOther) == typeof(Int256)) { result = (Int256)(object)value!; return true; }
        if (typeof(TOther) == typeof(Float128))
        {
            Float128 v = (Float128)(object)value!;
            if (Float128.IsNaN(v) || Float128.IsZero(v)) { result = Zero; return true; }
            if (Float128.IsPositiveInfinity(v)) { result = MaxValue; return true; }
            if (Float128.IsNegativeInfinity(v)) { result = MinValue; return true; }
            BigInteger truncated = (BigInteger)(BigDecimal)Float128.Truncate(v);
            if (truncated < (BigInteger)MinValue) { result = MinValue; return true; }
            if (truncated > (BigInteger)MaxValue) { result = MaxValue; return true; }
            result = (Int256)truncated;
            return true;
        }
        if (typeof(TOther) == typeof(Float256))
        {
            Float256 v = (Float256)(object)value!;
            if (Float256.IsNaN(v) || Float256.IsZero(v)) { result = Zero; return true; }
            if (Float256.IsPositiveInfinity(v)) { result = MaxValue; return true; }
            if (Float256.IsNegativeInfinity(v)) { result = MinValue; return true; }
            BigInteger truncated = (BigInteger)(BigDecimal)Float256.Truncate(v);
            if (truncated < (BigInteger)MinValue) { result = MinValue; return true; }
            if (truncated > (BigInteger)MaxValue) { result = MaxValue; return true; }
            result = (Int256)truncated;
            return true;
        }

        result = default;
        return false;
    }

    /// <summary>
    /// Tries to convert the specified <typeparamref name="TOther"/> value to an <see cref="Int256"/> value, truncating any bits that fall outside the representable range.
    /// </summary>
    /// <typeparam name="TOther">The type of the source value.</typeparam>
    /// <param name="value">The value to convert.</param>
    /// <param name="result">When this method returns, contains the truncated value if the source type is supported; otherwise, the default value.</param>
    /// <returns>Returns <see langword="true"/> if the source type is supported; otherwise, <see langword="false"/>.</returns>
    private static bool TryConvertFromTruncating<TOther>(TOther value, out Int256 result) where TOther : INumberBase<TOther>
    {
        if (typeof(TOther) == typeof(sbyte)) { result = (sbyte)(object)value!; return true; }
        if (typeof(TOther) == typeof(byte)) { result = (byte)(object)value!; return true; }
        if (typeof(TOther) == typeof(short)) { result = (short)(object)value!; return true; }
        if (typeof(TOther) == typeof(ushort)) { result = (ushort)(object)value!; return true; }
        if (typeof(TOther) == typeof(int)) { result = (int)(object)value!; return true; }
        if (typeof(TOther) == typeof(uint)) { result = (uint)(object)value!; return true; }
        if (typeof(TOther) == typeof(long)) { result = (long)(object)value!; return true; }
        if (typeof(TOther) == typeof(ulong)) { result = (ulong)(object)value!; return true; }
        if (typeof(TOther) == typeof(Int128)) { result = (Int128)(object)value!; return true; }
        if (typeof(TOther) == typeof(UInt128)) { result = (UInt128)(object)value!; return true; }
        if (typeof(TOther) == typeof(UInt256)) { result = (Int256)(UInt256)(object)value!; return true; }
        if (typeof(TOther) == typeof(BigInteger)) { result = (Int256)(BigInteger)(object)value!; return true; }
        if (typeof(TOther) == typeof(char)) { result = (char)(object)value!; return true; }
        if (typeof(TOther) == typeof(Int256)) { result = (Int256)(object)value!; return true; }
        if (typeof(TOther) == typeof(Float128)) { result = (Int256)(Float128)(object)value!; return true; }
        if (typeof(TOther) == typeof(Float256)) { result = (Int256)(Float256)(object)value!; return true; }

        result = default;
        return false;
    }

    /// <summary>
    /// Tries to convert the specified <see cref="Int256"/> value to a <typeparamref name="TOther"/> value, throwing on overflow.
    /// </summary>
    /// <typeparam name="TOther">The type of the destination value.</typeparam>
    /// <param name="value">The value to convert.</param>
    /// <param name="result">When this method returns, contains the converted value if the destination type is supported; otherwise, the default value.</param>
    /// <returns>Returns <see langword="true"/> if the destination type is supported; otherwise, <see langword="false"/>.</returns>
    /// <exception cref="OverflowException">Thrown when <paramref name="value"/> is outside the range of <typeparamref name="TOther"/>.</exception>
    private static bool TryConvertToChecked<TOther>(Int256 value, [MaybeNullWhen(false)] out TOther result) where TOther : INumberBase<TOther>
    {
        if (typeof(TOther) == typeof(sbyte)) { result = (TOther)(object)checked((sbyte)value); return true; }
        if (typeof(TOther) == typeof(byte)) { result = (TOther)(object)checked((byte)value); return true; }
        if (typeof(TOther) == typeof(short)) { result = (TOther)(object)checked((short)value); return true; }
        if (typeof(TOther) == typeof(ushort)) { result = (TOther)(object)checked((ushort)value); return true; }
        if (typeof(TOther) == typeof(int)) { result = (TOther)(object)checked((int)value); return true; }
        if (typeof(TOther) == typeof(uint)) { result = (TOther)(object)checked((uint)value); return true; }
        if (typeof(TOther) == typeof(long)) { result = (TOther)(object)checked((long)value); return true; }
        if (typeof(TOther) == typeof(ulong)) { result = (TOther)(object)checked((ulong)value); return true; }
        if (typeof(TOther) == typeof(Int128)) { result = (TOther)(object)checked((Int128)value); return true; }
        if (typeof(TOther) == typeof(UInt128)) { result = (TOther)(object)checked((UInt128)value); return true; }
        if (typeof(TOther) == typeof(UInt256))
        {
            if (IsNegative(value)) { result = default; throw new OverflowException($"Value was either too large or too small for the specified type: {nameof(UInt256)}."); }
            result = (TOther)(object)new UInt256(value.Upper, value.Lower);
            return true;
        }
        if (typeof(TOther) == typeof(BigInteger)) { result = (TOther)(object)(BigInteger)value; return true; }
        if (typeof(TOther) == typeof(char)) { result = (TOther)(object)checked((char)value); return true; }
        if (typeof(TOther) == typeof(float)) { result = (TOther)(object)(float)value; return true; }
        if (typeof(TOther) == typeof(double)) { result = (TOther)(object)(double)value; return true; }
        if (typeof(TOther) == typeof(decimal)) { result = (TOther)(object)(decimal)value; return true; }
        if (typeof(TOther) == typeof(Half)) { result = (TOther)(object)(Half)value; return true; }
        if (typeof(TOther) == typeof(Int256)) { result = (TOther)(object)value; return true; }
        if (typeof(TOther) == typeof(Float128)) { result = (TOther)(object)(Float128)value; return true; }
        if (typeof(TOther) == typeof(Float256)) { result = (TOther)(object)(Float256)value; return true; }

        result = default;
        return false;
    }

    /// <summary>
    /// Tries to convert the specified <see cref="Int256"/> value to a <typeparamref name="TOther"/> value, saturating values that fall outside the representable range.
    /// </summary>
    /// <typeparam name="TOther">The type of the destination value.</typeparam>
    /// <param name="value">The value to convert.</param>
    /// <param name="result">When this method returns, contains the saturated value if the destination type is supported; otherwise, the default value.</param>
    /// <returns>Returns <see langword="true"/> if the destination type is supported; otherwise, <see langword="false"/>.</returns>
    private static bool TryConvertToSaturating<TOther>(Int256 value, [MaybeNullWhen(false)] out TOther result) where TOther : INumberBase<TOther>
    {
        if (typeof(TOther) == typeof(sbyte)) { result = (TOther)(object)(value < (Int256)sbyte.MinValue ? sbyte.MinValue : value > (Int256)sbyte.MaxValue ? sbyte.MaxValue : (sbyte)value); return true; }
        if (typeof(TOther) == typeof(byte)) { result = (TOther)(object)(IsNegative(value) ? (byte)0 : value > (Int256)byte.MaxValue ? byte.MaxValue : (byte)value); return true; }
        if (typeof(TOther) == typeof(short)) { result = (TOther)(object)(value < (Int256)short.MinValue ? short.MinValue : value > (Int256)short.MaxValue ? short.MaxValue : (short)value); return true; }
        if (typeof(TOther) == typeof(ushort)) { result = (TOther)(object)(IsNegative(value) ? (ushort)0 : value > (Int256)ushort.MaxValue ? ushort.MaxValue : (ushort)value); return true; }
        if (typeof(TOther) == typeof(int)) { result = (TOther)(object)(value < (Int256)int.MinValue ? int.MinValue : value > (Int256)int.MaxValue ? int.MaxValue : (int)value); return true; }
        if (typeof(TOther) == typeof(uint)) { result = (TOther)(object)(IsNegative(value) ? 0u : value > (Int256)uint.MaxValue ? uint.MaxValue : (uint)value); return true; }
        if (typeof(TOther) == typeof(long)) { result = (TOther)(object)(value < (Int256)long.MinValue ? long.MinValue : value > (Int256)long.MaxValue ? long.MaxValue : (long)value); return true; }
        if (typeof(TOther) == typeof(ulong)) { result = (TOther)(object)(IsNegative(value) ? 0UL : value > (Int256)ulong.MaxValue ? ulong.MaxValue : (ulong)value); return true; }
        if (typeof(TOther) == typeof(Int128)) { result = (TOther)(object)(value < (Int256)Int128.MinValue ? Int128.MinValue : value > (Int256)Int128.MaxValue ? Int128.MaxValue : (Int128)value); return true; }
        if (typeof(TOther) == typeof(UInt128)) { result = (TOther)(object)(IsNegative(value) ? UInt128.Zero : value.Upper != UInt128.Zero ? UInt128.MaxValue : value.Lower); return true; }
        if (typeof(TOther) == typeof(UInt256)) { result = (TOther)(object)(IsNegative(value) ? UInt256.Zero : new UInt256(value.Upper, value.Lower)); return true; }
        if (typeof(TOther) == typeof(BigInteger)) { result = (TOther)(object)(BigInteger)value; return true; }
        if (typeof(TOther) == typeof(char)) { result = (TOther)(object)(IsNegative(value) ? (char)0 : value > (Int256)char.MaxValue ? char.MaxValue : (char)value); return true; }
        if (typeof(TOther) == typeof(float)) { result = (TOther)(object)(float)value; return true; }
        if (typeof(TOther) == typeof(double)) { result = (TOther)(object)(double)value; return true; }
        if (typeof(TOther) == typeof(decimal)) { result = (TOther)(object)(decimal)value; return true; }
        if (typeof(TOther) == typeof(Half)) { result = (TOther)(object)(Half)value; return true; }
        if (typeof(TOther) == typeof(Int256)) { result = (TOther)(object)value; return true; }
        if (typeof(TOther) == typeof(Float128)) { result = (TOther)(object)(Float128)value; return true; }
        if (typeof(TOther) == typeof(Float256)) { result = (TOther)(object)(Float256)value; return true; }

        result = default;
        return false;
    }

    /// <summary>
    /// Tries to convert the specified <see cref="Int256"/> value to a <typeparamref name="TOther"/> value, truncating any bits that fall outside the representable range.
    /// </summary>
    /// <typeparam name="TOther">The type of the destination value.</typeparam>
    /// <param name="value">The value to convert.</param>
    /// <param name="result">When this method returns, contains the truncated value if the destination type is supported; otherwise, the default value.</param>
    /// <returns>Returns <see langword="true"/> if the destination type is supported; otherwise, <see langword="false"/>.</returns>
    private static bool TryConvertToTruncating<TOther>(Int256 value, [MaybeNullWhen(false)] out TOther result) where TOther : INumberBase<TOther>
    {
        if (typeof(TOther) == typeof(sbyte)) { result = (TOther)(object)(sbyte)value; return true; }
        if (typeof(TOther) == typeof(byte)) { result = (TOther)(object)(byte)value; return true; }
        if (typeof(TOther) == typeof(short)) { result = (TOther)(object)(short)value; return true; }
        if (typeof(TOther) == typeof(ushort)) { result = (TOther)(object)(ushort)value; return true; }
        if (typeof(TOther) == typeof(int)) { result = (TOther)(object)(int)value; return true; }
        if (typeof(TOther) == typeof(uint)) { result = (TOther)(object)(uint)value; return true; }
        if (typeof(TOther) == typeof(long)) { result = (TOther)(object)(long)value; return true; }
        if (typeof(TOther) == typeof(ulong)) { result = (TOther)(object)(ulong)value; return true; }
        if (typeof(TOther) == typeof(Int128)) { result = (TOther)(object)(Int128)value; return true; }
        if (typeof(TOther) == typeof(UInt128)) { result = (TOther)(object)(UInt128)value; return true; }
        if (typeof(TOther) == typeof(UInt256)) { result = (TOther)(object)new UInt256(value.Upper, value.Lower); return true; }
        if (typeof(TOther) == typeof(BigInteger)) { result = (TOther)(object)(BigInteger)value; return true; }
        if (typeof(TOther) == typeof(char)) { result = (TOther)(object)(char)value; return true; }
        if (typeof(TOther) == typeof(float)) { result = (TOther)(object)(float)value; return true; }
        if (typeof(TOther) == typeof(double)) { result = (TOther)(object)(double)value; return true; }
        if (typeof(TOther) == typeof(decimal)) { result = (TOther)(object)(decimal)value; return true; }
        if (typeof(TOther) == typeof(Half)) { result = (TOther)(object)(Half)value; return true; }
        if (typeof(TOther) == typeof(Int256)) { result = (TOther)(object)value; return true; }
        if (typeof(TOther) == typeof(Float128)) { result = (TOther)(object)(Float128)value; return true; }
        if (typeof(TOther) == typeof(Float256)) { result = (TOther)(object)(Float256)value; return true; }

        result = default;
        return false;
    }
}
