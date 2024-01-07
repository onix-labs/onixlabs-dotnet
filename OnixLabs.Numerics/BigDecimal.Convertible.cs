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

public readonly partial struct BigDecimal : IConvertible
{
    /// <summary>
    /// Gets the <see cref="TypeCode"/> for the current instance.
    /// </summary>
    /// <returns>Returns the enumerated constant that is the <see cref="TypeCode"/> of the class or value type that implements this interface.</returns>
    TypeCode IConvertible.GetTypeCode()
    {
        return TypeCode.Object;
    }

    /// <summary>
    /// Converts the value of this instance to an equivalent <see cref="sbyte"/> 8-bit signed integer using the specified culture-specific formatting information.
    /// </summary>
    /// <param name="provider">An <see cref="IFormatProvider"/> interface implementation that supplies culture-specific formatting information.</param>
    /// <returns>Returns an 8-bit signed integer equivalent to the value of this instance.</returns>
    sbyte IConvertible.ToSByte(IFormatProvider? provider)
    {
        return (sbyte)this;
    }

    /// <summary>
    /// Converts the value of this instance to an equivalent <see cref="byte"/> 8-bit unsigned integer using the specified culture-specific formatting information.
    /// </summary>
    /// <param name="provider">An <see cref="IFormatProvider"/> interface implementation that supplies culture-specific formatting information.</param>
    /// <returns>Returns an 8-bit unsigned integer equivalent to the value of this instance.</returns>
    byte IConvertible.ToByte(IFormatProvider? provider)
    {
        return (byte)this;
    }

    /// <summary>
    /// Converts the value of this instance to an equivalent <see cref="short"/> 16-bit signed integer using the specified culture-specific formatting information.
    /// </summary>
    /// <param name="provider">An <see cref="IFormatProvider"/> interface implementation that supplies culture-specific formatting information.</param>
    /// <returns>Returns a 16-bit signed integer equivalent to the value of this instance.</returns>
    short IConvertible.ToInt16(IFormatProvider? provider)
    {
        return (short)this;
    }

    /// <summary>
    /// Converts the value of this instance to an equivalent <see cref="ushort"/> 32-bit signed integer using the specified culture-specific formatting information.
    /// </summary>
    /// <param name="provider">An <see cref="IFormatProvider"/> interface implementation that supplies culture-specific formatting information.</param>
    /// <returns>Returns a 32-bit signed integer equivalent to the value of this instance.</returns>
    int IConvertible.ToInt32(IFormatProvider? provider)
    {
        return (int)this;
    }

    /// <summary>
    /// Converts the value of this instance to an equivalent <see cref="long"/> 64-bit signed integer using the specified culture-specific formatting information.
    /// </summary>
    /// <param name="provider">An <see cref="IFormatProvider"/> interface implementation that supplies culture-specific formatting information.</param>
    /// <returns>Returns a 64-bit signed integer equivalent to the value of this instance.</returns>
    long IConvertible.ToInt64(IFormatProvider? provider)
    {
        return (long)this;
    }

    /// <summary>
    /// Converts the value of this instance to an equivalent <see cref="ushort"/> 16-bit unsigned integer using the specified culture-specific formatting information.
    /// </summary>
    /// <param name="provider">An <see cref="IFormatProvider"/> interface implementation that supplies culture-specific formatting information.</param>
    /// <returns>Returns a 16-bit unsigned integer equivalent to the value of this instance.</returns>
    ushort IConvertible.ToUInt16(IFormatProvider? provider)
    {
        return (ushort)this;
    }

    /// <summary>
    /// Converts the value of this instance to an equivalent <see cref="uint"/> 32-bit unsigned integer using the specified culture-specific formatting information.
    /// </summary>
    /// <param name="provider">An <see cref="IFormatProvider"/> interface implementation that supplies culture-specific formatting information.</param>
    /// <returns>Returns a 32-bit unsigned integer equivalent to the value of this instance.</returns>
    uint IConvertible.ToUInt32(IFormatProvider? provider)
    {
        return (uint)this;
    }

    /// <summary>
    /// Converts the value of this instance to an equivalent <see cref="ulong"/> 64-bit unsigned integer using the specified culture-specific formatting information.
    /// </summary>
    /// <param name="provider">An <see cref="IFormatProvider"/> interface implementation that supplies culture-specific formatting information.</param>
    /// <returns>Returns a 64-bit unsigned integer equivalent to the value of this instance.</returns>
    ulong IConvertible.ToUInt64(IFormatProvider? provider)
    {
        return (ulong)this;
    }

    /// <summary>
    /// Converts the value of this instance to an equivalent <see cref="float"/> single-precision floating-point number using the specified culture-specific formatting information.
    /// </summary>
    /// <param name="provider">An <see cref="IFormatProvider"/> interface implementation that supplies culture-specific formatting information.</param>
    /// <returns>Returns a single-precision floating-point number equivalent to the value of this instance.</returns>
    float IConvertible.ToSingle(IFormatProvider? provider)
    {
        return (float)this;
    }

    /// <summary>
    /// Converts the value of this instance to an equivalent <see cref="double"/> double-precision floating-point number using the specified culture-specific formatting information.
    /// </summary>
    /// <param name="provider">An <see cref="IFormatProvider"/> interface implementation that supplies culture-specific formatting information.</param>
    /// <returns>Returns a double-precision floating-point number equivalent to the value of this instance.</returns>
    double IConvertible.ToDouble(IFormatProvider? provider)
    {
        return (double)this;
    }

    /// <summary>
    /// Converts the value of this instance to an equivalent <see cref="decimal"/> number using the specified culture-specific formatting information.
    /// </summary>
    /// <param name="provider">An <see cref="IFormatProvider"/> interface implementation that supplies culture-specific formatting information.</param>
    /// <returns>Returns a <see cref="decimal"/> number equivalent to the value of this instance.</returns>
    decimal IConvertible.ToDecimal(IFormatProvider? provider)
    {
        return (decimal)this;
    }

    /// <summary>
    /// Converts the value of this instance to an equivalent <see cref="DateTime"/> using the specified culture-specific formatting information.
    /// </summary>
    /// <param name="provider">An <see cref="IFormatProvider"/> interface implementation that supplies culture-specific formatting information.</param>
    /// <returns>Returns a <see cref="DateTime"/> instance equivalent to the value of this instance.</returns>
    DateTime IConvertible.ToDateTime(IFormatProvider? provider)
    {
        long binary = (long)NumberInfo.UnscaledValue;
        return DateTime.FromBinary(binary);
    }

    /// <summary>
    /// Converts the value of this instance to an equivalent <see cref="bool"/> value using the specified culture-specific formatting information.
    /// </summary>
    /// <param name="provider">An <see cref="IFormatProvider"/> interface implementation that supplies culture-specific formatting information.</param>
    /// <returns>Returns a <see cref="bool"/> value equivalent to the value of this instance.</returns>
    bool IConvertible.ToBoolean(IFormatProvider? provider)
    {
        return !IsZero(this);
    }

    /// <summary>
    /// Converts the value of this instance to an equivalent <see cref="char"/> Unicode character using the specified culture-specific formatting information.
    /// </summary>
    /// <param name="provider">An <see cref="IFormatProvider"/> interface implementation that supplies culture-specific formatting information.</param>
    /// <returns>Returns a <see cref="char"/> Unicode character equivalent to the value of this instance</returns>.
    char IConvertible.ToChar(IFormatProvider? provider)
    {
        return (char)this;
    }

    /// <summary>
    /// Converts the value of this instance to an equivalent <see cref="string"/> using the specified culture-specific formatting information.
    /// </summary>
    /// <param name="provider">An <see cref="IFormatProvider"/> interface implementation that supplies culture-specific formatting information.</param>
    /// <returns>Returns a <see cref="string"/> instance equivalent to the value of this instance.</returns>
    string IConvertible.ToString(IFormatProvider? provider)
    {
        return ToString(DefaultNumberFormat, provider);
    }

    /// <summary>
    /// Converts the value of this instance to an <see cref="object"/> of the specified <see cref="Type"/> that has an equivalent value, using the specified culture-specific formatting information.
    /// </summary>
    /// <param name="conversionType">The <see cref="Type"/> to which the value of this instance is converted</param>
    /// <param name="provider">An <see cref="IFormatProvider"/> interface implementation that supplies culture-specific formatting information.</param>
    /// <returns>Returns an <see cref="object"/> instance of type conversionType whose value is equivalent to the value of this instance.</returns>
    object IConvertible.ToType(Type conversionType, IFormatProvider? provider)
    {
        return Convert.ChangeType(this, conversionType, provider);
    }
}
