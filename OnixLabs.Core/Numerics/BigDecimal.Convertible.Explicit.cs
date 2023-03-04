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
using System.Numerics;

namespace OnixLabs.Core.Numerics;

public readonly partial struct BigDecimal
{
    /// <summary>
    /// Converts the specified <see cref="BigDecimal"/> value to an unscaled <see cref="sbyte"/>.
    /// </summary>
    /// <param name="value">The <see cref="BigDecimal"/> value to convert.</param>
    /// <returns>Returns a new, unscaled <see cref="sbyte"/> representing the current <see cref="BigDecimal"/> value.</returns>
    public static explicit operator sbyte(BigDecimal value)
    {
        return (value as IConvertible).ToSByte(null);
    }

    /// <summary>
    /// Converts the specified <see cref="BigDecimal"/> value to an unscaled <see cref="byte"/>.
    /// </summary>
    /// <param name="value">The <see cref="BigDecimal"/> value to convert.</param>
    /// <returns>Returns a new, unscaled <see cref="byte"/> representing the current <see cref="BigDecimal"/> value.</returns>
    public static explicit operator byte(BigDecimal value)
    {
        return (value as IConvertible).ToByte(null);
    }

    /// <summary>
    /// Converts the specified <see cref="BigDecimal"/> value to an unscaled <see cref="short"/>.
    /// </summary>
    /// <param name="value">The <see cref="BigDecimal"/> value to convert.</param>
    /// <returns>Returns a new, unscaled <see cref="short"/> representing the current <see cref="BigDecimal"/> value.</returns>
    public static explicit operator short(BigDecimal value)
    {
        return (value as IConvertible).ToInt16(null);
    }

    /// <summary>
    /// Converts the specified <see cref="BigDecimal"/> value to an unscaled <see cref="ushort"/>.
    /// </summary>
    /// <param name="value">The <see cref="BigDecimal"/> value to convert.</param>
    /// <returns>Returns a new, unscaled <see cref="ushort"/> representing the current <see cref="BigDecimal"/> value.</returns>
    public static explicit operator ushort(BigDecimal value)
    {
        return (value as IConvertible).ToUInt16(null);
    }

    /// <summary>
    /// Converts the specified <see cref="BigDecimal"/> value to an unscaled <see cref="int"/>.
    /// </summary>
    /// <param name="value">The <see cref="BigDecimal"/> value to convert.</param>
    /// <returns>Returns a new, unscaled <see cref="int"/> representing the current <see cref="BigDecimal"/> value.</returns>
    public static explicit operator int(BigDecimal value)
    {
        return (value as IConvertible).ToInt32(null);
    }

    /// <summary>
    /// Converts the specified <see cref="BigDecimal"/> value to an unscaled <see cref="uint"/>.
    /// </summary>
    /// <param name="value">The <see cref="BigDecimal"/> value to convert.</param>
    /// <returns>Returns a new, unscaled <see cref="uint"/> representing the current <see cref="BigDecimal"/> value.</returns>
    public static explicit operator uint(BigDecimal value)
    {
        return (value as IConvertible).ToUInt32(null);
    }

    /// <summary>
    /// Converts the specified <see cref="BigDecimal"/> value to an unscaled <see cref="long"/>.
    /// </summary>
    /// <param name="value">The <see cref="BigDecimal"/> value to convert.</param>
    /// <returns>Returns a new, unscaled <see cref="long"/> representing the current <see cref="BigDecimal"/> value.</returns>
    public static explicit operator long(BigDecimal value)
    {
        return (value as IConvertible).ToInt64(null);
    }

    /// <summary>
    /// Converts the specified <see cref="BigDecimal"/> value to an unscaled <see cref="ulong"/>.
    /// </summary>
    /// <param name="value">The <see cref="BigDecimal"/> value to convert.</param>
    /// <returns>Returns a new, unscaled <see cref="ulong"/> representing the current <see cref="BigDecimal"/> value.</returns>
    public static explicit operator ulong(BigDecimal value)
    {
        return (value as IConvertible).ToUInt64(null);
    }

    /// <summary>
    /// Converts the specified <see cref="BigDecimal"/> value to an unscaled <see cref="BigInteger"/>.
    /// </summary>
    /// <param name="value">The <see cref="BigInteger"/> value to convert.</param>
    /// <returns>Returns a new, unscaled <see cref="BigInteger"/> representing the current <see cref="BigDecimal"/> value.</returns>
    public static explicit operator BigInteger(BigDecimal value)
    {
        return value.UnscaledValue;
    }

    /// <summary>
    /// Converts the specified <see cref="BigDecimal"/> value to a <see cref="float"/>.
    /// </summary>
    /// <param name="value">The <see cref="BigInteger"/> value to convert.</param>
    /// <returns>Returns a new <see cref="float"/> representing the current <see cref="BigDecimal"/> value.</returns>
    public static explicit operator float(BigDecimal value)
    {
        return (value as IConvertible).ToSingle(null);
    }

    /// <summary>
    /// Converts the specified <see cref="BigDecimal"/> value to a <see cref="double"/>.
    /// </summary>
    /// <param name="value">The <see cref="BigInteger"/> value to convert.</param>
    /// <returns>Returns a new <see cref="double"/> representing the current <see cref="BigDecimal"/> value.</returns>
    public static explicit operator double(BigDecimal value)
    {
        return (value as IConvertible).ToDouble(null);
    }

    /// <summary>
    /// Converts the specified <see cref="BigDecimal"/> value to a <see cref="decimal"/>.
    /// </summary>
    /// <param name="value">The <see cref="BigInteger"/> value to convert.</param>
    /// <returns>Returns a new <see cref="decimal"/> representing the current <see cref="BigDecimal"/> value.</returns>
    public static explicit operator decimal(BigDecimal value)
    {
        return (value as IConvertible).ToDecimal(null);
    }
}
