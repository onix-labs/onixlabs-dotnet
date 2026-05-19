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

public readonly partial struct Int256
{
    /// <summary>Implicitly converts an <see cref="sbyte"/> value to an <see cref="Int256"/> value.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the <see cref="Int256"/> representation of <paramref name="value"/>.</returns>
    public static implicit operator Int256(sbyte value) => (Int128)value;

    /// <summary>Implicitly converts a <see cref="byte"/> value to an <see cref="Int256"/> value.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the <see cref="Int256"/> representation of <paramref name="value"/>.</returns>
    public static implicit operator Int256(byte value) => new(UInt128.Zero, value);

    /// <summary>Implicitly converts a <see cref="short"/> value to an <see cref="Int256"/> value.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the <see cref="Int256"/> representation of <paramref name="value"/>.</returns>
    public static implicit operator Int256(short value) => (Int128)value;

    /// <summary>Implicitly converts a <see cref="ushort"/> value to an <see cref="Int256"/> value.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the <see cref="Int256"/> representation of <paramref name="value"/>.</returns>
    public static implicit operator Int256(ushort value) => new(UInt128.Zero, value);

    /// <summary>Implicitly converts an <see cref="int"/> value to an <see cref="Int256"/> value.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the <see cref="Int256"/> representation of <paramref name="value"/>.</returns>
    public static implicit operator Int256(int value) => (Int128)value;

    /// <summary>Implicitly converts a <see cref="uint"/> value to an <see cref="Int256"/> value.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the <see cref="Int256"/> representation of <paramref name="value"/>.</returns>
    public static implicit operator Int256(uint value) => new(UInt128.Zero, value);

    /// <summary>Implicitly converts a <see cref="long"/> value to an <see cref="Int256"/> value.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the <see cref="Int256"/> representation of <paramref name="value"/>.</returns>
    public static implicit operator Int256(long value) => (Int128)value;

    /// <summary>Implicitly converts a <see cref="ulong"/> value to an <see cref="Int256"/> value.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the <see cref="Int256"/> representation of <paramref name="value"/>.</returns>
    public static implicit operator Int256(ulong value) => new(UInt128.Zero, value);

    /// <summary>Implicitly converts an <see cref="Int128"/> value to an <see cref="Int256"/> value.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the <see cref="Int256"/> representation of <paramref name="value"/>.</returns>
    public static implicit operator Int256(Int128 value)
    {
        UInt128 signExtension = value < Int128.Zero ? UInt128.MaxValue : UInt128.Zero;
        return new Int256(signExtension, (UInt128)value);
    }

    /// <summary>Implicitly converts a <see cref="UInt128"/> value to an <see cref="Int256"/> value.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the <see cref="Int256"/> representation of <paramref name="value"/>.</returns>
    public static implicit operator Int256(UInt128 value) => new(UInt128.Zero, value);

    /// <summary>Implicitly converts a <see cref="char"/> value to an <see cref="Int256"/> value.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the <see cref="Int256"/> representation of <paramref name="value"/>.</returns>
    public static implicit operator Int256(char value) => new(UInt128.Zero, value);
}
