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
using System.Numerics;

namespace OnixLabs.Numerics;

public readonly partial struct BigDecimal
{
    /// <summary>
    /// Converts the specified <see cref="BigInteger"/> value to a <see cref="BigDecimal"/> value.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns a <see cref="BigDecimal"/> value representing the specified <see cref="BigInteger"/> value.</returns>
    public static implicit operator BigDecimal(BigInteger value) => new(value, 0, ScaleMode.Integral);

    /// <summary>
    /// Converts the specified <see cref="sbyte"/> value to a <see cref="BigDecimal"/> value.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns a <see cref="BigDecimal"/> value representing the specified <see cref="sbyte"/> value.</returns>
    public static implicit operator BigDecimal(sbyte value) => new(value, 0, ScaleMode.Integral);

    /// <summary>
    /// Converts the specified <see cref="byte"/> value to a <see cref="BigDecimal"/> value.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns a <see cref="BigDecimal"/> value representing the specified <see cref="byte"/> value.</returns>
    public static implicit operator BigDecimal(byte value) => new(value, 0, ScaleMode.Integral);

    /// <summary>
    /// Converts the specified <see cref="short"/> value to a <see cref="BigDecimal"/> value.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns a <see cref="BigDecimal"/> value representing the specified <see cref="short"/> value.</returns>
    public static implicit operator BigDecimal(short value) => new(value, 0, ScaleMode.Integral);

    /// <summary>
    /// Converts the specified <see cref="ushort"/> value to a <see cref="BigDecimal"/> value.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns a <see cref="BigDecimal"/> value representing the specified <see cref="ushort"/> value.</returns>
    public static implicit operator BigDecimal(ushort value) => new(value, 0, ScaleMode.Integral);

    /// <summary>
    /// Converts the specified <see cref="int"/> value to a <see cref="BigDecimal"/> value.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns a <see cref="BigDecimal"/> value representing the specified <see cref="int"/> value.</returns>
    public static implicit operator BigDecimal(int value) => new(value, 0, ScaleMode.Integral);

    /// <summary>
    /// Converts the specified <see cref="uint"/> value to a <see cref="BigDecimal"/> value.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns a <see cref="BigDecimal"/> value representing the specified <see cref="uint"/> value.</returns>
    public static implicit operator BigDecimal(uint value) => new(value, 0, ScaleMode.Integral);

    /// <summary>
    /// Converts the specified <see cref="long"/> value to a <see cref="BigDecimal"/> value.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns a <see cref="BigDecimal"/> value representing the specified <see cref="long"/> value.</returns>
    public static implicit operator BigDecimal(long value) => new(value, 0, ScaleMode.Integral);

    /// <summary>
    /// Converts the specified <see cref="ulong"/> value to a <see cref="BigDecimal"/> value.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns a <see cref="BigDecimal"/> value representing the specified <see cref="ulong"/> value.</returns>
    public static implicit operator BigDecimal(ulong value) => new(value, 0, ScaleMode.Integral);

    /// <summary>
    /// Converts the specified <see cref="Int128"/> value to a <see cref="BigDecimal"/> value.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns a <see cref="BigDecimal"/> value representing the specified <see cref="Int128"/> value.</returns>
    public static implicit operator BigDecimal(Int128 value) => new(value, 0, ScaleMode.Integral);

    /// <summary>
    /// Converts the specified <see cref="UInt128"/> value to a <see cref="BigDecimal"/> value.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns a <see cref="BigDecimal"/> value representing the specified <see cref="UInt128"/> value.</returns>
    public static implicit operator BigDecimal(UInt128 value) => new(value, 0, ScaleMode.Integral);

    /// <summary>
    /// Converts the specified <see cref="float"/> value to a <see cref="BigDecimal"/> value.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns a <see cref="BigDecimal"/> value representing the specified <see cref="float"/> value.</returns>
    public static implicit operator BigDecimal(float value) => new(value);

    /// <summary>
    /// Converts the specified <see cref="double"/> value to a <see cref="BigDecimal"/> value.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns a <see cref="BigDecimal"/> value representing the specified <see cref="double"/> value.</returns>
    public static implicit operator BigDecimal(double value) => new(value);

    /// <summary>
    /// Converts the specified <see cref="decimal"/> value to a <see cref="BigDecimal"/> value.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns a <see cref="BigDecimal"/> value representing the specified <see cref="decimal"/> value.</returns>
    public static implicit operator BigDecimal(decimal value) => new(value);
}
