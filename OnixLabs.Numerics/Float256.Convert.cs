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

public readonly partial struct Float256
{
    static bool INumberBase<Float256>.TryConvertFromChecked<TOther>(TOther value, out Float256 result) => TryConvertFrom(value, out result);
    static bool INumberBase<Float256>.TryConvertFromSaturating<TOther>(TOther value, out Float256 result) => TryConvertFrom(value, out result);
    static bool INumberBase<Float256>.TryConvertFromTruncating<TOther>(TOther value, out Float256 result) => TryConvertFrom(value, out result);
    static bool INumberBase<Float256>.TryConvertToChecked<TOther>(Float256 value, [MaybeNullWhen(false)] out TOther result) => TryConvertTo(value, out result);
    static bool INumberBase<Float256>.TryConvertToSaturating<TOther>(Float256 value, [MaybeNullWhen(false)] out TOther result) => TryConvertTo(value, out result);
    static bool INumberBase<Float256>.TryConvertToTruncating<TOther>(Float256 value, [MaybeNullWhen(false)] out TOther result) => TryConvertTo(value, out result);

    /// <summary>
    /// Tries to convert the specified value of type <typeparamref name="TOther"/> to a <see cref="Float256"/>.
    /// </summary>
    /// <typeparam name="TOther">The numeric type of <paramref name="value"/>.</typeparam>
    /// <param name="value">The value to convert.</param>
    /// <param name="result">When this method returns, contains the converted <see cref="Float256"/> value if successful; otherwise, the default value.</param>
    /// <returns>Returns <see langword="true"/> if <typeparamref name="TOther"/> is a recognised numeric type; otherwise, <see langword="false"/>.</returns>
    private static bool TryConvertFrom<TOther>(TOther value, out Float256 result) where TOther : INumberBase<TOther>
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
        if (typeof(TOther) == typeof(BigInteger)) { result = (Float256)(BigDecimal)(BigInteger)(object)value!; return true; }
        if (typeof(TOther) == typeof(char)) { result = (char)(object)value!; return true; }
        if (typeof(TOther) == typeof(Half)) { result = (Half)(object)value!; return true; }
        if (typeof(TOther) == typeof(float)) { result = (float)(object)value!; return true; }
        if (typeof(TOther) == typeof(double)) { result = (double)(object)value!; return true; }
        if (typeof(TOther) == typeof(decimal)) { result = (decimal)(object)value!; return true; }
        if (typeof(TOther) == typeof(BigDecimal)) { result = (Float256)(BigDecimal)(object)value!; return true; }
        if (typeof(TOther) == typeof(Float128)) { result = (Float128)(object)value!; return true; }
        if (typeof(TOther) == typeof(Float256)) { result = (Float256)(object)value!; return true; }

        result = default;
        return false;
    }

    /// <summary>
    /// Tries to convert the specified <see cref="Float256"/> value to a value of type <typeparamref name="TOther"/>.
    /// </summary>
    /// <typeparam name="TOther">The target numeric type.</typeparam>
    /// <param name="value">The value to convert.</param>
    /// <param name="result">When this method returns, contains the converted value if successful; otherwise, the default value.</param>
    /// <returns>Returns <see langword="true"/> if <typeparamref name="TOther"/> is a recognised numeric type; otherwise, <see langword="false"/>.</returns>
    private static bool TryConvertTo<TOther>(Float256 value, [MaybeNullWhen(false)] out TOther result) where TOther : INumberBase<TOther>
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
        if (typeof(TOther) == typeof(Half)) { result = (TOther)(object)(Half)value; return true; }
        if (typeof(TOther) == typeof(float)) { result = (TOther)(object)(float)value; return true; }
        if (typeof(TOther) == typeof(double)) { result = (TOther)(object)(double)value; return true; }
        if (typeof(TOther) == typeof(decimal)) { result = (TOther)(object)(decimal)value; return true; }
        if (typeof(TOther) == typeof(BigDecimal)) { result = (TOther)(object)(BigDecimal)value; return true; }
        if (typeof(TOther) == typeof(Float128)) { result = (TOther)(object)(Float128)value; return true; }
        if (typeof(TOther) == typeof(Float256)) { result = (TOther)(object)value; return true; }

        result = default;
        return false;
    }
}
