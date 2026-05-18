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

public readonly partial struct UInt256
{
    static bool INumberBase<UInt256>.TryConvertFromChecked<TOther>(TOther value, out UInt256 result) => TryConvertFromChecked(value, out result);
    static bool INumberBase<UInt256>.TryConvertFromSaturating<TOther>(TOther value, out UInt256 result) => TryConvertFromSaturating(value, out result);
    static bool INumberBase<UInt256>.TryConvertFromTruncating<TOther>(TOther value, out UInt256 result) => TryConvertFromTruncating(value, out result);
    static bool INumberBase<UInt256>.TryConvertToChecked<TOther>(UInt256 value, [MaybeNullWhen(false)] out TOther result) => TryConvertToChecked(value, out result);
    static bool INumberBase<UInt256>.TryConvertToSaturating<TOther>(UInt256 value, [MaybeNullWhen(false)] out TOther result) => TryConvertToSaturating(value, out result);
    static bool INumberBase<UInt256>.TryConvertToTruncating<TOther>(UInt256 value, [MaybeNullWhen(false)] out TOther result) => TryConvertToTruncating(value, out result);

    /// <summary>
    /// Tries to convert the specified <typeparamref name="TOther"/> value to a <see cref="UInt256"/> value, throwing on overflow.
    /// </summary>
    /// <typeparam name="TOther">The type of the source value.</typeparam>
    /// <param name="value">The value to convert.</param>
    /// <param name="result">When this method returns, contains the converted value if the source type is supported; otherwise, the default value.</param>
    /// <returns>Returns <see langword="true"/> if the source type is supported; otherwise, <see langword="false"/>.</returns>
    /// <exception cref="OverflowException">Thrown when <paramref name="value"/> is outside the range of <see cref="UInt256"/>.</exception>
    private static bool TryConvertFromChecked<TOther>(TOther value, out UInt256 result) where TOther : INumberBase<TOther>
    {
        if (typeof(TOther) == typeof(sbyte)) { result = checked((UInt256)(sbyte)(object)value!); return true; }
        if (typeof(TOther) == typeof(byte)) { result = (byte)(object)value!; return true; }
        if (typeof(TOther) == typeof(short)) { result = checked((UInt256)(short)(object)value!); return true; }
        if (typeof(TOther) == typeof(ushort)) { result = (ushort)(object)value!; return true; }
        if (typeof(TOther) == typeof(int)) { result = checked((UInt256)(int)(object)value!); return true; }
        if (typeof(TOther) == typeof(uint)) { result = (uint)(object)value!; return true; }
        if (typeof(TOther) == typeof(long)) { result = checked((UInt256)(long)(object)value!); return true; }
        if (typeof(TOther) == typeof(ulong)) { result = (ulong)(object)value!; return true; }
        if (typeof(TOther) == typeof(Int128)) { result = checked((UInt256)(Int128)(object)value!); return true; }
        if (typeof(TOther) == typeof(UInt128)) { result = (UInt128)(object)value!; return true; }
        if (typeof(TOther) == typeof(BigInteger)) { result = checked((UInt256)(BigInteger)(object)value!); return true; }
        if (typeof(TOther) == typeof(char)) { result = (char)(object)value!; return true; }
        if (typeof(TOther) == typeof(UInt256)) { result = (UInt256)(object)value!; return true; }
        if (typeof(TOther) == typeof(Float128)) { result = checked((UInt256)(Float128)(object)value!); return true; }
        if (typeof(TOther) == typeof(Float256)) { result = checked((UInt256)(Float256)(object)value!); return true; }

        result = default;
        return false;
    }

    /// <summary>
    /// Tries to convert the specified <typeparamref name="TOther"/> value to a <see cref="UInt256"/> value, saturating values that fall outside the representable range.
    /// </summary>
    /// <typeparam name="TOther">The type of the source value.</typeparam>
    /// <param name="value">The value to convert.</param>
    /// <param name="result">When this method returns, contains the saturated value if the source type is supported; otherwise, the default value.</param>
    /// <returns>Returns <see langword="true"/> if the source type is supported; otherwise, <see langword="false"/>.</returns>
    private static bool TryConvertFromSaturating<TOther>(TOther value, out UInt256 result) where TOther : INumberBase<TOther>
    {
        if (typeof(TOther) == typeof(sbyte)) { sbyte v = (sbyte)(object)value!; result = v < 0 ? Zero : (UInt256)v; return true; }
        if (typeof(TOther) == typeof(byte)) { result = (byte)(object)value!; return true; }
        if (typeof(TOther) == typeof(short)) { short v = (short)(object)value!; result = v < 0 ? Zero : (UInt256)v; return true; }
        if (typeof(TOther) == typeof(ushort)) { result = (ushort)(object)value!; return true; }
        if (typeof(TOther) == typeof(int)) { int v = (int)(object)value!; result = v < 0 ? Zero : (UInt256)v; return true; }
        if (typeof(TOther) == typeof(uint)) { result = (uint)(object)value!; return true; }
        if (typeof(TOther) == typeof(long)) { long v = (long)(object)value!; result = v < 0L ? Zero : (UInt256)v; return true; }
        if (typeof(TOther) == typeof(ulong)) { result = (ulong)(object)value!; return true; }
        if (typeof(TOther) == typeof(Int128)) { Int128 v = (Int128)(object)value!; result = v < Int128.Zero ? Zero : (UInt256)v; return true; }
        if (typeof(TOther) == typeof(UInt128)) { result = (UInt128)(object)value!; return true; }
        if (typeof(TOther) == typeof(BigInteger))
        {
            BigInteger v = (BigInteger)(object)value!;
            if (v.Sign < 0) { result = Zero; return true; }
            if (v.GetBitLength() > BitWidth) { result = MaxValue; return true; }
            result = (UInt256)v;
            return true;
        }
        if (typeof(TOther) == typeof(char)) { result = (char)(object)value!; return true; }
        if (typeof(TOther) == typeof(UInt256)) { result = (UInt256)(object)value!; return true; }
        if (typeof(TOther) == typeof(Float128))
        {
            Float128 v = (Float128)(object)value!;
            if (Float128.IsNaN(v) || Float128.IsNegative(v) || Float128.IsZero(v)) { result = Zero; return true; }
            if (Float128.IsPositiveInfinity(v)) { result = MaxValue; return true; }
            BigInteger truncated = (BigInteger)(BigDecimal)Float128.Truncate(v);
            result = truncated.GetBitLength() > BitWidth ? MaxValue : (UInt256)truncated;
            return true;
        }
        if (typeof(TOther) == typeof(Float256))
        {
            Float256 v = (Float256)(object)value!;
            if (Float256.IsNaN(v) || Float256.IsNegative(v) || Float256.IsZero(v)) { result = Zero; return true; }
            if (Float256.IsPositiveInfinity(v)) { result = MaxValue; return true; }
            BigInteger truncated = (BigInteger)(BigDecimal)Float256.Truncate(v);
            result = truncated.GetBitLength() > BitWidth ? MaxValue : (UInt256)truncated;
            return true;
        }

        result = default;
        return false;
    }

    /// <summary>
    /// Tries to convert the specified <typeparamref name="TOther"/> value to a <see cref="UInt256"/> value, truncating any bits that fall outside the representable range.
    /// </summary>
    /// <typeparam name="TOther">The type of the source value.</typeparam>
    /// <param name="value">The value to convert.</param>
    /// <param name="result">When this method returns, contains the truncated value if the source type is supported; otherwise, the default value.</param>
    /// <returns>Returns <see langword="true"/> if the source type is supported; otherwise, <see langword="false"/>.</returns>
    private static bool TryConvertFromTruncating<TOther>(TOther value, out UInt256 result) where TOther : INumberBase<TOther>
    {
        if (typeof(TOther) == typeof(sbyte)) { result = (UInt256)(sbyte)(object)value!; return true; }
        if (typeof(TOther) == typeof(byte)) { result = (byte)(object)value!; return true; }
        if (typeof(TOther) == typeof(short)) { result = (UInt256)(short)(object)value!; return true; }
        if (typeof(TOther) == typeof(ushort)) { result = (ushort)(object)value!; return true; }

        if (typeof(TOther) == typeof(int)) { result = (UInt256)(int)(object)value!; return true; }
        if (typeof(TOther) == typeof(uint)) { result = (uint)(object)value!; return true; }
        if (typeof(TOther) == typeof(long)) { result = (UInt256)(long)(object)value!; return true; }
        if (typeof(TOther) == typeof(ulong)) { result = (ulong)(object)value!; return true; }
        if (typeof(TOther) == typeof(Int128)) { result = (UInt256)(Int128)(object)value!; return true; }
        if (typeof(TOther) == typeof(UInt128)) { result = (UInt128)(object)value!; return true; }
        if (typeof(TOther) == typeof(BigInteger)) { result = (UInt256)(BigInteger)(object)value!; return true; }
        if (typeof(TOther) == typeof(char)) { result = (char)(object)value!; return true; }
        if (typeof(TOther) == typeof(UInt256)) { result = (UInt256)(object)value!; return true; }
        if (typeof(TOther) == typeof(Float128)) { result = (UInt256)(Float128)(object)value!; return true; }
        if (typeof(TOther) == typeof(Float256)) { result = (UInt256)(Float256)(object)value!; return true; }

        result = default;
        return false;
    }

    /// <summary>
    /// Tries to convert the specified <see cref="UInt256"/> value to a <typeparamref name="TOther"/> value, throwing on overflow.
    /// </summary>
    /// <typeparam name="TOther">The type of the destination value.</typeparam>
    /// <param name="value">The value to convert.</param>
    /// <param name="result">When this method returns, contains the converted value if the destination type is supported; otherwise, the default value.</param>
    /// <returns>Returns <see langword="true"/> if the destination type is supported; otherwise, <see langword="false"/>.</returns>
    /// <exception cref="OverflowException">Thrown when <paramref name="value"/> is outside the range of <typeparamref name="TOther"/>.</exception>
    private static bool TryConvertToChecked<TOther>(UInt256 value, [MaybeNullWhen(false)] out TOther result) where TOther : INumberBase<TOther>
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
        if (typeof(TOther) == typeof(BigInteger)) { result = (TOther)(object)(BigInteger)value; return true; }
        if (typeof(TOther) == typeof(char)) { result = (TOther)(object)checked((char)value); return true; }
        if (typeof(TOther) == typeof(float)) { result = (TOther)(object)(float)value; return true; }
        if (typeof(TOther) == typeof(double)) { result = (TOther)(object)(double)value; return true; }
        if (typeof(TOther) == typeof(decimal)) { result = (TOther)(object)(decimal)value; return true; }
        if (typeof(TOther) == typeof(Half)) { result = (TOther)(object)(Half)value; return true; }
        if (typeof(TOther) == typeof(UInt256)) { result = (TOther)(object)value; return true; }
        if (typeof(TOther) == typeof(Float128)) { result = (TOther)(object)(Float128)value; return true; }
        if (typeof(TOther) == typeof(Float256)) { result = (TOther)(object)(Float256)value; return true; }

        result = default;
        return false;
    }

    /// <summary>
    /// Tries to convert the specified <see cref="UInt256"/> value to a <typeparamref name="TOther"/> value, saturating values that fall outside the representable range.
    /// </summary>
    /// <typeparam name="TOther">The type of the destination value.</typeparam>
    /// <param name="value">The value to convert.</param>
    /// <param name="result">When this method returns, contains the saturated value if the destination type is supported; otherwise, the default value.</param>
    /// <returns>Returns <see langword="true"/> if the destination type is supported; otherwise, <see langword="false"/>.</returns>
    private static bool TryConvertToSaturating<TOther>(UInt256 value, [MaybeNullWhen(false)] out TOther result) where TOther : INumberBase<TOther>
    {
        if (typeof(TOther) == typeof(sbyte)) { result = (TOther)(object)(value > (UInt256)(byte)sbyte.MaxValue ? sbyte.MaxValue : (sbyte)value); return true; }
        if (typeof(TOther) == typeof(byte)) { result = (TOther)(object)(value > byte.MaxValue ? byte.MaxValue : (byte)value); return true; }
        if (typeof(TOther) == typeof(short)) { result = (TOther)(object)(value > (UInt256)(ushort)short.MaxValue ? short.MaxValue : (short)value); return true; }
        if (typeof(TOther) == typeof(ushort)) { result = (TOther)(object)(value > ushort.MaxValue ? ushort.MaxValue : (ushort)value); return true; }
        if (typeof(TOther) == typeof(int)) { result = (TOther)(object)(value > (UInt256)(uint)int.MaxValue ? int.MaxValue : (int)value); return true; }
        if (typeof(TOther) == typeof(uint)) { result = (TOther)(object)(value > uint.MaxValue ? uint.MaxValue : (uint)value); return true; }
        if (typeof(TOther) == typeof(long)) { result = (TOther)(object)(value > (UInt256)(ulong)long.MaxValue ? long.MaxValue : (long)value); return true; }
        if (typeof(TOther) == typeof(ulong)) { result = (TOther)(object)(value > ulong.MaxValue ? ulong.MaxValue : (ulong)value); return true; }
        if (typeof(TOther) == typeof(Int128)) { result = (TOther)(object)(value > (UInt256)(UInt128)Int128.MaxValue ? Int128.MaxValue : (Int128)value); return true; }
        if (typeof(TOther) == typeof(UInt128)) { result = (TOther)(object)(value.Upper != UInt128.Zero ? UInt128.MaxValue : value.Lower); return true; }
        if (typeof(TOther) == typeof(BigInteger)) { result = (TOther)(object)(BigInteger)value; return true; }
        if (typeof(TOther) == typeof(char)) { result = (TOther)(object)(value > char.MaxValue ? char.MaxValue : (char)value); return true; }
        if (typeof(TOther) == typeof(float)) { result = (TOther)(object)(float)value; return true; }
        if (typeof(TOther) == typeof(double)) { result = (TOther)(object)(double)value; return true; }
        if (typeof(TOther) == typeof(decimal)) { result = (TOther)(object)(decimal)value; return true; }
        if (typeof(TOther) == typeof(Half)) { result = (TOther)(object)(Half)value; return true; }
        if (typeof(TOther) == typeof(UInt256)) { result = (TOther)(object)value; return true; }
        if (typeof(TOther) == typeof(Float128)) { result = (TOther)(object)(Float128)value; return true; }
        if (typeof(TOther) == typeof(Float256)) { result = (TOther)(object)(Float256)value; return true; }

        result = default;
        return false;
    }

    /// <summary>
    /// Tries to convert the specified <see cref="UInt256"/> value to a <typeparamref name="TOther"/> value, truncating any bits that fall outside the representable range.
    /// </summary>
    /// <typeparam name="TOther">The type of the destination value.</typeparam>
    /// <param name="value">The value to convert.</param>
    /// <param name="result">When this method returns, contains the truncated value if the destination type is supported; otherwise, the default value.</param>
    /// <returns>Returns <see langword="true"/> if the destination type is supported; otherwise, <see langword="false"/>.</returns>
    private static bool TryConvertToTruncating<TOther>(UInt256 value, [MaybeNullWhen(false)] out TOther result) where TOther : INumberBase<TOther>
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
        if (typeof(TOther) == typeof(BigInteger)) { result = (TOther)(object)(BigInteger)value; return true; }
        if (typeof(TOther) == typeof(char)) { result = (TOther)(object)(char)value; return true; }
        if (typeof(TOther) == typeof(float)) { result = (TOther)(object)(float)value; return true; }
        if (typeof(TOther) == typeof(double)) { result = (TOther)(object)(double)value; return true; }
        if (typeof(TOther) == typeof(decimal)) { result = (TOther)(object)(decimal)value; return true; }
        if (typeof(TOther) == typeof(Half)) { result = (TOther)(object)(Half)value; return true; }
        if (typeof(TOther) == typeof(UInt256)) { result = (TOther)(object)value; return true; }
        if (typeof(TOther) == typeof(Float128)) { result = (TOther)(object)(Float128)value; return true; }
        if (typeof(TOther) == typeof(Float256)) { result = (TOther)(object)(Float256)value; return true; }

        result = default;
        return false;
    }
}
