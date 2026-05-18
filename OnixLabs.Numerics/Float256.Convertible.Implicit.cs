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

public readonly partial struct Float256
{
    /// <summary>Implicitly converts a <see cref="byte"/> value to a <see cref="Float256"/> value.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the <see cref="Float256"/> representation of <paramref name="value"/>.</returns>
    public static implicit operator Float256(byte value) => (Float256)(BigDecimal)value;

    /// <summary>Implicitly converts an <see cref="sbyte"/> value to a <see cref="Float256"/> value.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the <see cref="Float256"/> representation of <paramref name="value"/>.</returns>
    public static implicit operator Float256(sbyte value) => (Float256)(BigDecimal)value;

    /// <summary>Implicitly converts a <see cref="short"/> value to a <see cref="Float256"/> value.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the <see cref="Float256"/> representation of <paramref name="value"/>.</returns>
    public static implicit operator Float256(short value) => (Float256)(BigDecimal)value;

    /// <summary>Implicitly converts a <see cref="ushort"/> value to a <see cref="Float256"/> value.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the <see cref="Float256"/> representation of <paramref name="value"/>.</returns>
    public static implicit operator Float256(ushort value) => (Float256)(BigDecimal)value;

    /// <summary>Implicitly converts an <see cref="int"/> value to a <see cref="Float256"/> value.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the <see cref="Float256"/> representation of <paramref name="value"/>.</returns>
    public static implicit operator Float256(int value) => (Float256)(BigDecimal)value;

    /// <summary>Implicitly converts a <see cref="uint"/> value to a <see cref="Float256"/> value.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the <see cref="Float256"/> representation of <paramref name="value"/>.</returns>
    public static implicit operator Float256(uint value) => (Float256)(BigDecimal)value;

    /// <summary>Implicitly converts a <see cref="long"/> value to a <see cref="Float256"/> value.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the <see cref="Float256"/> representation of <paramref name="value"/>.</returns>
    public static implicit operator Float256(long value) => (Float256)(BigDecimal)value;

    /// <summary>Implicitly converts a <see cref="ulong"/> value to a <see cref="Float256"/> value.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the <see cref="Float256"/> representation of <paramref name="value"/>.</returns>
    public static implicit operator Float256(ulong value) => (Float256)(BigDecimal)value;

    /// <summary>Implicitly converts an <see cref="Int128"/> value to a <see cref="Float256"/> value.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the <see cref="Float256"/> representation of <paramref name="value"/>.</returns>
    public static implicit operator Float256(Int128 value) => (Float256)(BigDecimal)(BigInteger)value;

    /// <summary>Implicitly converts a <see cref="UInt128"/> value to a <see cref="Float256"/> value.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the <see cref="Float256"/> representation of <paramref name="value"/>.</returns>
    public static implicit operator Float256(UInt128 value) => (Float256)(BigDecimal)(BigInteger)value;

    /// <summary>Implicitly converts a <see cref="char"/> value to a <see cref="Float256"/> value.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the <see cref="Float256"/> representation of <paramref name="value"/>.</returns>
    public static implicit operator Float256(char value) => (Float256)(BigDecimal)value;

    /// <summary>Implicitly converts a <see cref="decimal"/> value to a <see cref="Float256"/> value.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the <see cref="Float256"/> representation of <paramref name="value"/>.</returns>
    public static implicit operator Float256(decimal value) => (Float256)(BigDecimal)value;

    /// <summary>Implicitly converts a <see cref="Half"/> value to a <see cref="Float256"/> value.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the <see cref="Float256"/> representation of <paramref name="value"/>.</returns>
    public static implicit operator Float256(Half value) => FromDouble((double)value);

    /// <summary>Implicitly converts a <see cref="float"/> value to a <see cref="Float256"/> value.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the <see cref="Float256"/> representation of <paramref name="value"/>.</returns>
    public static implicit operator Float256(float value) => FromDouble(value);

    /// <summary>Implicitly converts a <see cref="double"/> value to a <see cref="Float256"/> value.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the <see cref="Float256"/> representation of <paramref name="value"/>.</returns>
    public static implicit operator Float256(double value) => FromDouble(value);

    /// <summary>Implicitly converts a <see cref="Float128"/> value to a <see cref="Float256"/> value.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the <see cref="Float256"/> representation of <paramref name="value"/>.</returns>
    public static implicit operator Float256(Float128 value)
    {
        if (Float128.IsNaN(value)) return NaN;
        if (Float128.IsPositiveInfinity(value)) return PositiveInfinity;
        if (Float128.IsNegativeInfinity(value)) return NegativeInfinity;
        if (Float128.IsZero(value)) return Float128.IsNegative(value) ? NegativeZero : Zero;
        return (Float256)(BigDecimal)value;
    }

    /// <summary>
    /// Converts the specified <see cref="double"/> value to a <see cref="Float256"/>, preserving NaN, infinities, and signed zero.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the <see cref="Float256"/> representation of <paramref name="value"/>.</returns>
    private static Float256 FromDouble(double value)
    {
        if (double.IsNaN(value)) return NaN;
        if (double.IsPositiveInfinity(value)) return PositiveInfinity;
        if (double.IsNegativeInfinity(value)) return NegativeInfinity;
        if (value == 0.0) return double.IsNegative(value) ? NegativeZero : Zero;
        return (Float256)(BigDecimal)value;
    }
}
