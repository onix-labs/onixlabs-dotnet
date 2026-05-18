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

public readonly partial struct Int512
{
    static bool INumberBase<Int512>.TryConvertFromChecked<TOther>(TOther value, out Int512 result) => TryConvertFromChecked(value, out result);
    static bool INumberBase<Int512>.TryConvertFromSaturating<TOther>(TOther value, out Int512 result) => TryConvertFromSaturating(value, out result);
    static bool INumberBase<Int512>.TryConvertFromTruncating<TOther>(TOther value, out Int512 result) => TryConvertFromTruncating(value, out result);
    static bool INumberBase<Int512>.TryConvertToChecked<TOther>(Int512 value, [MaybeNullWhen(false)] out TOther result) => TryConvertToChecked(value, out result);
    static bool INumberBase<Int512>.TryConvertToSaturating<TOther>(Int512 value, [MaybeNullWhen(false)] out TOther result) => TryConvertToSaturating(value, out result);
    static bool INumberBase<Int512>.TryConvertToTruncating<TOther>(Int512 value, [MaybeNullWhen(false)] out TOther result) => TryConvertToTruncating(value, out result);

    /// <summary>
    /// Tries to convert a value of the specified type to an <see cref="Int512"/> using checked semantics, throwing on overflow.
    /// </summary>
    /// <typeparam name="TOther">The source numeric type.</typeparam>
    /// <param name="value">The value to convert.</param>
    /// <param name="result">When this method returns, contains the converted value if the source type is recognised; otherwise, the default value.</param>
    /// <returns>Returns <see langword="true"/> if <typeparamref name="TOther"/> is a recognised source type; otherwise, <see langword="false"/>.</returns>
    private static bool TryConvertFromChecked<TOther>(TOther value, out Int512 result) where TOther : INumberBase<TOther>
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
        if (typeof(TOther) == typeof(Int256)) { result = (Int256)(object)value!; return true; }
        if (typeof(TOther) == typeof(UInt256)) { result = (UInt256)(object)value!; return true; }
        if (typeof(TOther) == typeof(UInt512)) { result = checked((Int512)(UInt512)(object)value!); return true; }
        if (typeof(TOther) == typeof(BigInteger)) { result = checked((Int512)(BigInteger)(object)value!); return true; }
        if (typeof(TOther) == typeof(char)) { result = (char)(object)value!; return true; }
        if (typeof(TOther) == typeof(Int512)) { result = (Int512)(object)value!; return true; }
        if (typeof(TOther) == typeof(Float128)) { result = checked((Int512)(Float128)(object)value!); return true; }
        if (typeof(TOther) == typeof(Float256)) { result = checked((Int512)(Float256)(object)value!); return true; }

        result = default;
        return false;
    }

    /// <summary>
    /// Tries to convert a value of the specified type to an <see cref="Int512"/> using saturating semantics, clamping out-of-range values to <see cref="MinValue"/> or <see cref="MaxValue"/>.
    /// </summary>
    /// <typeparam name="TOther">The source numeric type.</typeparam>
    /// <param name="value">The value to convert.</param>
    /// <param name="result">When this method returns, contains the converted value if the source type is recognised; otherwise, the default value.</param>
    /// <returns>Returns <see langword="true"/> if <typeparamref name="TOther"/> is a recognised source type; otherwise, <see langword="false"/>.</returns>
    private static bool TryConvertFromSaturating<TOther>(TOther value, out Int512 result) where TOther : INumberBase<TOther>
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
        if (typeof(TOther) == typeof(Int256)) { result = (Int256)(object)value!; return true; }
        if (typeof(TOther) == typeof(UInt256)) { result = (UInt256)(object)value!; return true; }
        if (typeof(TOther) == typeof(UInt512))
        {
            UInt512 v = (UInt512)(object)value!;
            result = v > (UInt512)MaxValue ? MaxValue : (Int512)v;
            return true;
        }
        if (typeof(TOther) == typeof(BigInteger))
        {
            BigInteger v = (BigInteger)(object)value!;
            if (v < (BigInteger)MinValue) { result = MinValue; return true; }
            if (v > (BigInteger)MaxValue) { result = MaxValue; return true; }
            result = (Int512)v;
            return true;
        }
        if (typeof(TOther) == typeof(char)) { result = (char)(object)value!; return true; }
        if (typeof(TOther) == typeof(Int512)) { result = (Int512)(object)value!; return true; }
        if (typeof(TOther) == typeof(Float128))
        {
            Float128 v = (Float128)(object)value!;
            if (Float128.IsNaN(v) || Float128.IsZero(v)) { result = Zero; return true; }
            if (Float128.IsPositiveInfinity(v)) { result = MaxValue; return true; }
            if (Float128.IsNegativeInfinity(v)) { result = MinValue; return true; }
            BigInteger truncated = (BigInteger)(BigDecimal)Float128.Truncate(v);
            if (truncated < (BigInteger)MinValue) { result = MinValue; return true; }
            if (truncated > (BigInteger)MaxValue) { result = MaxValue; return true; }
            result = (Int512)truncated;
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
            result = (Int512)truncated;
            return true;
        }

        result = default;
        return false;
    }

    /// <summary>
    /// Tries to convert a value of the specified type to an <see cref="Int512"/> using truncating semantics, discarding any high-order bits that do not fit.
    /// </summary>
    /// <typeparam name="TOther">The source numeric type.</typeparam>
    /// <param name="value">The value to convert.</param>
    /// <param name="result">When this method returns, contains the converted value if the source type is recognised; otherwise, the default value.</param>
    /// <returns>Returns <see langword="true"/> if <typeparamref name="TOther"/> is a recognised source type; otherwise, <see langword="false"/>.</returns>
    private static bool TryConvertFromTruncating<TOther>(TOther value, out Int512 result) where TOther : INumberBase<TOther>
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
        if (typeof(TOther) == typeof(Int256)) { result = (Int256)(object)value!; return true; }
        if (typeof(TOther) == typeof(UInt256)) { result = (UInt256)(object)value!; return true; }
        if (typeof(TOther) == typeof(UInt512)) { result = (Int512)(UInt512)(object)value!; return true; }
        if (typeof(TOther) == typeof(BigInteger)) { result = (Int512)(BigInteger)(object)value!; return true; }
        if (typeof(TOther) == typeof(char)) { result = (char)(object)value!; return true; }
        if (typeof(TOther) == typeof(Int512)) { result = (Int512)(object)value!; return true; }
        if (typeof(TOther) == typeof(Float128)) { result = (Int512)(Float128)(object)value!; return true; }
        if (typeof(TOther) == typeof(Float256)) { result = (Int512)(Float256)(object)value!; return true; }

        result = default;
        return false;
    }

    /// <summary>
    /// Tries to convert an <see cref="Int512"/> to a value of the specified type using checked semantics, throwing on overflow.
    /// </summary>
    /// <typeparam name="TOther">The destination numeric type.</typeparam>
    /// <param name="value">The value to convert.</param>
    /// <param name="result">When this method returns, contains the converted value if the destination type is recognised; otherwise, the default value.</param>
    /// <returns>Returns <see langword="true"/> if <typeparamref name="TOther"/> is a recognised destination type; otherwise, <see langword="false"/>.</returns>
    private static bool TryConvertToChecked<TOther>(Int512 value, [MaybeNullWhen(false)] out TOther result) where TOther : INumberBase<TOther>
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
        if (typeof(TOther) == typeof(Int256)) { result = (TOther)(object)checked((Int256)value); return true; }
        if (typeof(TOther) == typeof(UInt256)) { result = (TOther)(object)checked((UInt256)value); return true; }
        if (typeof(TOther) == typeof(UInt512))
        {
            if (IsNegative(value)) { result = default; throw new OverflowException($"Value was either too large or too small for the specified type: {nameof(UInt512)}."); }
            result = (TOther)(object)new UInt512(value.Upper, value.Lower);
            return true;
        }
        if (typeof(TOther) == typeof(BigInteger)) { result = (TOther)(object)(BigInteger)value; return true; }
        if (typeof(TOther) == typeof(char)) { result = (TOther)(object)checked((char)value); return true; }
        if (typeof(TOther) == typeof(float)) { result = (TOther)(object)(float)value; return true; }
        if (typeof(TOther) == typeof(double)) { result = (TOther)(object)(double)value; return true; }
        if (typeof(TOther) == typeof(decimal)) { result = (TOther)(object)(decimal)value; return true; }
        if (typeof(TOther) == typeof(Half)) { result = (TOther)(object)(Half)value; return true; }
        if (typeof(TOther) == typeof(Int512)) { result = (TOther)(object)value; return true; }
        if (typeof(TOther) == typeof(Float128)) { result = (TOther)(object)(Float128)value; return true; }
        if (typeof(TOther) == typeof(Float256)) { result = (TOther)(object)(Float256)value; return true; }

        result = default;
        return false;
    }

    /// <summary>
    /// Tries to convert an <see cref="Int512"/> to a value of the specified type using saturating semantics, clamping out-of-range values to the destination type's minimum or maximum.
    /// </summary>
    /// <typeparam name="TOther">The destination numeric type.</typeparam>
    /// <param name="value">The value to convert.</param>
    /// <param name="result">When this method returns, contains the converted value if the destination type is recognised; otherwise, the default value.</param>
    /// <returns>Returns <see langword="true"/> if <typeparamref name="TOther"/> is a recognised destination type; otherwise, <see langword="false"/>.</returns>
    private static bool TryConvertToSaturating<TOther>(Int512 value, [MaybeNullWhen(false)] out TOther result) where TOther : INumberBase<TOther>
    {
        if (typeof(TOther) == typeof(sbyte)) { result = (TOther)(object)(value < (Int512)sbyte.MinValue ? sbyte.MinValue : value > (Int512)sbyte.MaxValue ? sbyte.MaxValue : (sbyte)value); return true; }
        if (typeof(TOther) == typeof(byte)) { result = (TOther)(object)(IsNegative(value) ? (byte)0 : value > (Int512)byte.MaxValue ? byte.MaxValue : (byte)value); return true; }
        if (typeof(TOther) == typeof(short)) { result = (TOther)(object)(value < (Int512)short.MinValue ? short.MinValue : value > (Int512)short.MaxValue ? short.MaxValue : (short)value); return true; }
        if (typeof(TOther) == typeof(ushort)) { result = (TOther)(object)(IsNegative(value) ? (ushort)0 : value > (Int512)ushort.MaxValue ? ushort.MaxValue : (ushort)value); return true; }
        if (typeof(TOther) == typeof(int)) { result = (TOther)(object)(value < (Int512)int.MinValue ? int.MinValue : value > (Int512)int.MaxValue ? int.MaxValue : (int)value); return true; }
        if (typeof(TOther) == typeof(uint)) { result = (TOther)(object)(IsNegative(value) ? 0u : value > (Int512)uint.MaxValue ? uint.MaxValue : (uint)value); return true; }
        if (typeof(TOther) == typeof(long)) { result = (TOther)(object)(value < (Int512)long.MinValue ? long.MinValue : value > (Int512)long.MaxValue ? long.MaxValue : (long)value); return true; }
        if (typeof(TOther) == typeof(ulong)) { result = (TOther)(object)(IsNegative(value) ? 0UL : value > (Int512)ulong.MaxValue ? ulong.MaxValue : (ulong)value); return true; }
        if (typeof(TOther) == typeof(Int128)) { result = (TOther)(object)(value < (Int512)Int128.MinValue ? Int128.MinValue : value > (Int512)Int128.MaxValue ? Int128.MaxValue : (Int128)value); return true; }
        if (typeof(TOther) == typeof(UInt128)) { result = (TOther)(object)(IsNegative(value) ? UInt128.Zero : value > (Int512)UInt128.MaxValue ? UInt128.MaxValue : (UInt128)value); return true; }
        if (typeof(TOther) == typeof(Int256)) { result = (TOther)(object)(value < (Int512)Int256.MinValue ? Int256.MinValue : value > (Int512)Int256.MaxValue ? Int256.MaxValue : (Int256)value); return true; }
        if (typeof(TOther) == typeof(UInt256)) { result = (TOther)(object)(IsNegative(value) ? UInt256.Zero : !UInt256.IsZero(value.Upper) ? UInt256.MaxValue : value.Lower); return true; }
        if (typeof(TOther) == typeof(UInt512)) { result = (TOther)(object)(IsNegative(value) ? UInt512.Zero : new UInt512(value.Upper, value.Lower)); return true; }
        if (typeof(TOther) == typeof(BigInteger)) { result = (TOther)(object)(BigInteger)value; return true; }
        if (typeof(TOther) == typeof(char)) { result = (TOther)(object)(IsNegative(value) ? (char)0 : value > (Int512)char.MaxValue ? char.MaxValue : (char)value); return true; }
        if (typeof(TOther) == typeof(float)) { result = (TOther)(object)(float)value; return true; }
        if (typeof(TOther) == typeof(double)) { result = (TOther)(object)(double)value; return true; }
        if (typeof(TOther) == typeof(decimal)) { result = (TOther)(object)(decimal)value; return true; }
        if (typeof(TOther) == typeof(Half)) { result = (TOther)(object)(Half)value; return true; }
        if (typeof(TOther) == typeof(Int512)) { result = (TOther)(object)value; return true; }
        if (typeof(TOther) == typeof(Float128)) { result = (TOther)(object)(Float128)value; return true; }
        if (typeof(TOther) == typeof(Float256)) { result = (TOther)(object)(Float256)value; return true; }

        result = default;
        return false;
    }

    /// <summary>
    /// Tries to convert an <see cref="Int512"/> to a value of the specified type using truncating semantics, discarding any high-order bits that do not fit.
    /// </summary>
    /// <typeparam name="TOther">The destination numeric type.</typeparam>
    /// <param name="value">The value to convert.</param>
    /// <param name="result">When this method returns, contains the converted value if the destination type is recognised; otherwise, the default value.</param>
    /// <returns>Returns <see langword="true"/> if <typeparamref name="TOther"/> is a recognised destination type; otherwise, <see langword="false"/>.</returns>
    private static bool TryConvertToTruncating<TOther>(Int512 value, [MaybeNullWhen(false)] out TOther result) where TOther : INumberBase<TOther>
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
        if (typeof(TOther) == typeof(Int256)) { result = (TOther)(object)(Int256)value; return true; }
        if (typeof(TOther) == typeof(UInt256)) { result = (TOther)(object)(UInt256)value; return true; }
        if (typeof(TOther) == typeof(UInt512)) { result = (TOther)(object)new UInt512(value.Upper, value.Lower); return true; }
        if (typeof(TOther) == typeof(BigInteger)) { result = (TOther)(object)(BigInteger)value; return true; }
        if (typeof(TOther) == typeof(char)) { result = (TOther)(object)(char)value; return true; }
        if (typeof(TOther) == typeof(float)) { result = (TOther)(object)(float)value; return true; }
        if (typeof(TOther) == typeof(double)) { result = (TOther)(object)(double)value; return true; }
        if (typeof(TOther) == typeof(decimal)) { result = (TOther)(object)(decimal)value; return true; }
        if (typeof(TOther) == typeof(Half)) { result = (TOther)(object)(Half)value; return true; }
        if (typeof(TOther) == typeof(Int512)) { result = (TOther)(object)value; return true; }
        if (typeof(TOther) == typeof(Float128)) { result = (TOther)(object)(Float128)value; return true; }
        if (typeof(TOther) == typeof(Float256)) { result = (TOther)(object)(Float256)value; return true; }

        result = default;
        return false;
    }
}
