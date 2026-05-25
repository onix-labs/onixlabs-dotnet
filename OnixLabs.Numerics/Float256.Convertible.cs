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

namespace OnixLabs.Numerics;

public readonly partial struct Float256
{
    /// <inheritdoc cref="IConvertible.GetTypeCode"/>
    TypeCode IConvertible.GetTypeCode() => TypeCode.Object;

    /// <inheritdoc cref="IConvertible.ToSByte"/>
    sbyte IConvertible.ToSByte(IFormatProvider? provider) => checked((sbyte)this);

    /// <inheritdoc cref="IConvertible.ToByte"/>
    byte IConvertible.ToByte(IFormatProvider? provider) => checked((byte)this);

    /// <inheritdoc cref="IConvertible.ToInt16"/>
    short IConvertible.ToInt16(IFormatProvider? provider) => checked((short)this);

    /// <inheritdoc cref="IConvertible.ToInt32"/>
    int IConvertible.ToInt32(IFormatProvider? provider) => checked((int)this);

    /// <inheritdoc cref="IConvertible.ToInt64"/>
    long IConvertible.ToInt64(IFormatProvider? provider) => checked((long)this);

    /// <inheritdoc cref="IConvertible.ToUInt16"/>
    ushort IConvertible.ToUInt16(IFormatProvider? provider) => checked((ushort)this);

    /// <inheritdoc cref="IConvertible.ToUInt32"/>
    uint IConvertible.ToUInt32(IFormatProvider? provider) => checked((uint)this);

    /// <inheritdoc cref="IConvertible.ToUInt64"/>
    ulong IConvertible.ToUInt64(IFormatProvider? provider) => checked((ulong)this);

    /// <inheritdoc cref="IConvertible.ToSingle"/>
    float IConvertible.ToSingle(IFormatProvider? provider) => (float)this;

    /// <inheritdoc cref="IConvertible.ToDouble"/>
    double IConvertible.ToDouble(IFormatProvider? provider) => (double)this;

    /// <inheritdoc cref="IConvertible.ToDecimal"/>
    decimal IConvertible.ToDecimal(IFormatProvider? provider) => (decimal)this;

    /// <inheritdoc cref="IConvertible.ToDateTime"/>
    /// <exception cref="InvalidCastException">Thrown when the value is converted to a <see cref="DateTime"/>.</exception>
    DateTime IConvertible.ToDateTime(IFormatProvider? provider) => throw new InvalidCastException($"Cannot convert {nameof(Float256)} to {nameof(DateTime)}.");

    /// <inheritdoc cref="IConvertible.ToBoolean"/>
    bool IConvertible.ToBoolean(IFormatProvider? provider) => !IsZero(this);

    /// <inheritdoc cref="IConvertible.ToChar"/>
    char IConvertible.ToChar(IFormatProvider? provider) => checked((char)this);

    /// <inheritdoc cref="IConvertible.ToString(IFormatProvider)"/>
    string IConvertible.ToString(IFormatProvider? provider) => ToString(DefaultFormatSpecifier, provider);

    /// <inheritdoc cref="IConvertible.ToType"/>
    object IConvertible.ToType(Type conversionType, IFormatProvider? provider) => Convert.ChangeType(this, conversionType, provider);
}
