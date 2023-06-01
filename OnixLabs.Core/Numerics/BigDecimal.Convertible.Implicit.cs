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
    /// Converts the specified <see cref="BigInteger"/> value to an unscaled <see cref="BigDecimal"/> value.
    /// </summary>
    /// <param name="value">The <see cref="BigInteger"/> value to convert.</param>
    /// <returns>Returns a new, unscaled <see cref="BigDecimal"/> value converted from the specified <see cref="BigInteger"/> value.</returns>
    public static implicit operator BigDecimal(BigInteger value)
    {
        return new BigDecimal(value, 0);
    }

    /// <summary>
    /// Converts the specified <see cref="sbyte"/> value to an unscaled <see cref="BigDecimal"/> value.
    /// </summary>
    /// <param name="value">The <see cref="sbyte"/> value to convert.</param>
    /// <returns>Returns a new, unscaled <see cref="BigDecimal"/> value converted from the specified <see cref="sbyte"/> value.</returns>
    public static implicit operator BigDecimal(sbyte value)
    {
        return new BigDecimal(value, 0);
    }

    /// <summary>
    /// Converts the specified <see cref="byte"/> value to an unscaled <see cref="BigDecimal"/> value.
    /// </summary>
    /// <param name="value">The <see cref="byte"/> value to convert.</param>
    /// <returns>Returns a new, unscaled <see cref="BigDecimal"/> value converted from the specified <see cref="byte"/> value.</returns>
    public static implicit operator BigDecimal(byte value)
    {
        return new BigDecimal(value, 0);
    }

    /// <summary>
    /// Converts the specified <see cref="short"/> value to an unscaled <see cref="BigDecimal"/> value.
    /// </summary>
    /// <param name="value">The <see cref="short"/> value to convert.</param>
    /// <returns>Returns a new, unscaled <see cref="BigDecimal"/> value converted from the specified <see cref="short"/> value.</returns>
    public static implicit operator BigDecimal(short value)
    {
        return new BigDecimal(value, 0);
    }

    /// <summary>
    /// Converts the specified <see cref="ushort"/> value to an unscaled <see cref="BigDecimal"/> value.
    /// </summary>
    /// <param name="value">The <see cref="ushort"/> value to convert.</param>
    /// <returns>Returns a new, unscaled <see cref="BigDecimal"/> value converted from the specified <see cref="ushort"/> value.</returns>
    public static implicit operator BigDecimal(ushort value)
    {
        return new BigDecimal(value, 0);
    }

    /// <summary>
    /// Converts the specified <see cref="int"/> value to an unscaled <see cref="BigDecimal"/> value.
    /// </summary>
    /// <param name="value">The <see cref="int"/> value to convert.</param>
    /// <returns>Returns a new, unscaled <see cref="BigDecimal"/> value converted from the specified <see cref="int"/> value.</returns>
    public static implicit operator BigDecimal(int value)
    {
        return new BigDecimal(value, 0);
    }

    /// <summary>
    /// Converts the specified <see cref="uint"/> value to an unscaled <see cref="BigDecimal"/> value.
    /// </summary>
    /// <param name="value">The <see cref="uint"/> value to convert.</param>
    /// <returns>Returns a new, unscaled <see cref="BigDecimal"/> value converted from the specified <see cref="uint"/> value.</returns>
    public static implicit operator BigDecimal(uint value)
    {
        return new BigDecimal(value, 0);
    }

    /// <summary>
    /// Converts the specified <see cref="long"/> value to an unscaled <see cref="BigDecimal"/> value.
    /// </summary>
    /// <param name="value">The <see cref="long"/> value to convert.</param>
    /// <returns>Returns a new, unscaled <see cref="BigDecimal"/> value converted from the specified <see cref="long"/> value.</returns>
    public static implicit operator BigDecimal(long value)
    {
        return new BigDecimal(value, 0);
    }

    /// <summary>
    /// Converts the specified <see cref="ulong"/> value to an unscaled <see cref="BigDecimal"/> value.
    /// </summary>
    /// <param name="value">The <see cref="ulong"/> value to convert.</param>
    /// <returns>Returns a new, unscaled <see cref="BigDecimal"/> value converted from the specified <see cref="ulong"/> value.</returns>
    public static implicit operator BigDecimal(ulong value)
    {
        return new BigDecimal(value, 0);
    }

    /// <summary>
    /// Converts the specified <see cref="decimal"/> value to an unscaled <see cref="BigDecimal"/> value.
    /// </summary>
    /// <param name="value">The <see cref="decimal"/> value to convert.</param>
    /// <returns>Returns a new, unscaled <see cref="BigDecimal"/> value converted from the specified <see cref="decimal"/> value.</returns>
    public static implicit operator BigDecimal(decimal value)
    {
        return new BigDecimal(value);
    }

    /// <summary>
    /// Converts the specified <see cref="double"/> value to an unscaled <see cref="BigDecimal"/> value.
    /// </summary>
    /// <param name="value">The <see cref="double"/> value to convert.</param>
    /// <returns>Returns a new, unscaled <see cref="BigDecimal"/> value converted from the specified <see cref="double"/> value.</returns>
    public static implicit operator BigDecimal(double value)
    {
        return new BigDecimal(value);
    }

    /// <summary>
    /// Converts the specified <see cref="float"/> value to an unscaled <see cref="BigDecimal"/> value.
    /// </summary>
    /// <param name="value">The <see cref="float"/> value to convert.</param>
    /// <returns>Returns a new, unscaled <see cref="BigDecimal"/> value converted from the specified <see cref="float"/> value.</returns>
    public static implicit operator BigDecimal(float value)
    {
        return new BigDecimal(value);
    }
}
