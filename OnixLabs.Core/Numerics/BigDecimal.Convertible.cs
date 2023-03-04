// Copyright 2020-2023 ONIXLabs
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

namespace OnixLabs.Core.Numerics;

public readonly partial struct BigDecimal : IConvertible
{
    TypeCode IConvertible.GetTypeCode()
    {
        return TypeCode.Object;
    }

    bool IConvertible.ToBoolean(IFormatProvider? provider)
    {
        return this == Zero;
    }
    
    sbyte IConvertible.ToSByte(IFormatProvider? provider)
    {
        if (UnscaledValue >= sbyte.MinValue && UnscaledValue <= sbyte.MaxValue)
        {
            return (sbyte)UnscaledValue;
        }

        throw new OverflowException($"Value was either too large or too small for the specified type: {nameof(SByte)}.");
    }

    byte IConvertible.ToByte(IFormatProvider? provider)
    {
        if (UnscaledValue >= byte.MinValue && UnscaledValue <= byte.MaxValue)
        {
            return (byte)UnscaledValue;
        }

        throw new OverflowException($"Value was either too large or too small for the specified type: {nameof(Byte)}.");
    }

    char IConvertible.ToChar(IFormatProvider? provider)
    {
        if (UnscaledValue >= char.MinValue && UnscaledValue <= char.MaxValue)
        {
            return (char)UnscaledValue;
        }

        throw new OverflowException($"Value was either too large or too small for the specified type: {nameof(Char)}.");
    }

    DateTime IConvertible.ToDateTime(IFormatProvider? provider)
    {
        long binary = (long)UnscaledValue;
        return DateTime.FromBinary(binary);
    }

    decimal IConvertible.ToDecimal(IFormatProvider? provider)
    {
        if (this >= decimal.MinValue && this <= decimal.MaxValue)
        {
            throw new NotImplementedException();
        }

        throw new OverflowException($"Value was either too large or too small for the specified type: {nameof(Decimal)}.");
    }

    double IConvertible.ToDouble(IFormatProvider? provider)
    {
        if (this >= double.MinValue && this <= double.MaxValue)
        {
            throw new NotImplementedException();
        }

        throw new OverflowException($"Value was either too large or too small for the specified type: {nameof(Double)}.");
    }

    float IConvertible.ToSingle(IFormatProvider? provider)
    {
        if (this >= float.MinValue && this <= float.MaxValue)
        {
            throw new NotImplementedException();
        }

        throw new OverflowException($"Value was either too large or too small for the specified type: {nameof(Single)}.");
    }

    short IConvertible.ToInt16(IFormatProvider? provider)
    {
        if (UnscaledValue >= short.MinValue && UnscaledValue <= short.MaxValue)
        {
            return (short)UnscaledValue;
        }

        throw new OverflowException($"Value was either too large or too small for the specified type: {nameof(Int16)}.");
    }

    ushort IConvertible.ToUInt16(IFormatProvider? provider)
    {
        if (UnscaledValue >= ushort.MinValue && UnscaledValue <= ushort.MaxValue)
        {
            return (ushort)UnscaledValue;
        }

        throw new OverflowException($"Value was either too large or too small for the specified type: {nameof(UInt16)}.");
    }

    int IConvertible.ToInt32(IFormatProvider? provider)
    {
        if (UnscaledValue >= int.MinValue && UnscaledValue <= int.MaxValue)
        {
            return (int)UnscaledValue;
        }

        throw new OverflowException($"Value was either too large or too small for the specified type: {nameof(Int32)}.");
    }

    uint IConvertible.ToUInt32(IFormatProvider? provider)
    {
        if (UnscaledValue >= uint.MinValue && UnscaledValue <= uint.MaxValue)
        {
            return (uint)UnscaledValue;
        }

        throw new OverflowException($"Value was either too large or too small for the specified type: {nameof(UInt32)}.");
    }

    long IConvertible.ToInt64(IFormatProvider? provider)
    {
        if (UnscaledValue >= long.MinValue && UnscaledValue <= long.MaxValue)
        {
            return (long)UnscaledValue;
        }

        throw new OverflowException($"Value was either too large or too small for the specified type: {nameof(Int64)}.");
    }

    ulong IConvertible.ToUInt64(IFormatProvider? provider)
    {
        if (UnscaledValue >= ulong.MinValue && UnscaledValue <= ulong.MaxValue)
        {
            return (ulong)UnscaledValue;
        }

        throw new OverflowException($"Value was either too large or too small for the specified type: {nameof(UInt64)}.");
    }

    string IConvertible.ToString(IFormatProvider? provider)
    {
        return ToString(DefaultNumberFormatSpecifier, provider);
    }

    object IConvertible.ToType(Type conversionType, IFormatProvider? provider)
    {
        return Convert.ChangeType(this, conversionType, provider);
    }
}
