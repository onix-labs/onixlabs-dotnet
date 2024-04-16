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

public readonly partial struct BigDecimal
{
    static bool INumberBase<BigDecimal>.TryConvertFromChecked<TOther>(
        TOther value,
        out BigDecimal result
    ) => TryConvertFrom(value, out result);

    static bool INumberBase<BigDecimal>.TryConvertFromSaturating<TOther>(
        TOther value,
        out BigDecimal result
    ) => TryConvertFrom(value, out result);

    static bool INumberBase<BigDecimal>.TryConvertFromTruncating<TOther>(
        TOther value,
        out BigDecimal result
    ) => TryConvertFrom(value, out result);

    static bool INumberBase<BigDecimal>.TryConvertToChecked<TOther>(
        BigDecimal value,
        [MaybeNullWhen(false)] out TOther result
    ) => TryConvertTo(value, out result);

    static bool INumberBase<BigDecimal>.TryConvertToSaturating<TOther>(
        BigDecimal value,
        [MaybeNullWhen(false)] out TOther result
    ) => TryConvertTo(value, out result);

    static bool INumberBase<BigDecimal>.TryConvertToTruncating<TOther>(
        BigDecimal value,
        [MaybeNullWhen(false)] out TOther result
    ) => TryConvertTo(value, out result);

    private static bool TryConvertFrom<TOther>(TOther value, out BigDecimal result) where TOther : INumberBase<TOther>
    {
        if (typeof(TOther) == typeof(sbyte))
        {
            result = (sbyte)(object)value;
            return true;
        }

        if (typeof(TOther) == typeof(byte))
        {
            result = (byte)(object)value;
            return true;
        }

        if (typeof(TOther) == typeof(short))
        {
            result = (short)(object)value;
            return true;
        }

        if (typeof(TOther) == typeof(ushort))
        {
            result = (ushort)(object)value;
            return true;
        }

        if (typeof(TOther) == typeof(int))
        {
            result = (int)(object)value;
            return true;
        }

        if (typeof(TOther) == typeof(uint))
        {
            result = (uint)(object)value;
            return true;
        }

        if (typeof(TOther) == typeof(long))
        {
            result = (long)(object)value;
            return true;
        }

        if (typeof(TOther) == typeof(ulong))
        {
            result = (ulong)(object)value;
            return true;
        }

        if (typeof(TOther) == typeof(Int128))
        {
            result = (Int128)(object)value;
            return true;
        }

        if (typeof(TOther) == typeof(UInt128))
        {
            result = (UInt128)(object)value;
            return true;
        }

        if (typeof(TOther) == typeof(BigInteger))
        {
            result = (BigInteger)(object)value;
            return true;
        }

        if (typeof(TOther) == typeof(decimal))
        {
            result = (decimal)(object)value;
            return true;
        }

        if (typeof(TOther) == typeof(double))
        {
            result = (double)(object)value;
            return true;
        }

        if (typeof(TOther) == typeof(float))
        {
            result = (float)(object)value;
            return true;
        }

        result = default;
        return false;
    }

    private static bool TryConvertTo<TOther>(BigDecimal value, [MaybeNullWhen(false)] out TOther result) where TOther : INumberBase<TOther>
    {
        if (typeof(TOther) == typeof(sbyte))
        {
            result = (TOther)(object)value;
            return true;
        }

        if (typeof(TOther) == typeof(byte))
        {
            result = (TOther)(object)value;
            return true;
        }

        if (typeof(TOther) == typeof(short))
        {
            result = (TOther)(object)value;
            return true;
        }

        if (typeof(TOther) == typeof(ushort))
        {
            result = (TOther)(object)value;
            return true;
        }

        if (typeof(TOther) == typeof(int))
        {
            result = (TOther)(object)value;
            return true;
        }

        if (typeof(TOther) == typeof(uint))
        {
            result = (TOther)(object)value;
            return true;
        }

        if (typeof(TOther) == typeof(long))
        {
            result = (TOther)(object)value;
            return true;
        }

        if (typeof(TOther) == typeof(ulong))
        {
            result = (TOther)(object)value;
            return true;
        }

        if (typeof(TOther) == typeof(Int128))
        {
            result = (TOther)(object)value;
            return true;
        }

        if (typeof(TOther) == typeof(UInt128))
        {
            result = (TOther)(object)value;
            return true;
        }

        if (typeof(TOther) == typeof(BigInteger))
        {
            result = (TOther)(object)value;
            return true;
        }

        if (typeof(TOther) == typeof(decimal))
        {
            result = (TOther)(object)value;
            return true;
        }

        if (typeof(TOther) == typeof(double))
        {
            result = (TOther)(object)value;
            return true;
        }

        if (typeof(TOther) == typeof(float))
        {
            result = (TOther)(object)value;
            return true;
        }

        result = default;
        return false;
    }
}
