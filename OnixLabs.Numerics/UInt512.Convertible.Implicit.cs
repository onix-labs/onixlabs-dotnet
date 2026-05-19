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

public readonly partial struct UInt512
{
    /// <summary>Implicitly converts a <see cref="byte"/> value to a <see cref="UInt512"/> value.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the <see cref="UInt512"/> representation of <paramref name="value"/>.</returns>
    public static implicit operator UInt512(byte value) => new(UInt256.Zero, value);

    /// <summary>Implicitly converts a <see cref="ushort"/> value to a <see cref="UInt512"/> value.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the <see cref="UInt512"/> representation of <paramref name="value"/>.</returns>
    public static implicit operator UInt512(ushort value) => new(UInt256.Zero, value);

    /// <summary>Implicitly converts a <see cref="uint"/> value to a <see cref="UInt512"/> value.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the <see cref="UInt512"/> representation of <paramref name="value"/>.</returns>
    public static implicit operator UInt512(uint value) => new(UInt256.Zero, value);

    /// <summary>Implicitly converts a <see cref="ulong"/> value to a <see cref="UInt512"/> value.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the <see cref="UInt512"/> representation of <paramref name="value"/>.</returns>
    public static implicit operator UInt512(ulong value) => new(UInt256.Zero, value);

    /// <summary>Implicitly converts a <see cref="UInt128"/> value to a <see cref="UInt512"/> value.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the <see cref="UInt512"/> representation of <paramref name="value"/>.</returns>
    public static implicit operator UInt512(UInt128 value) => new(UInt256.Zero, value);

    /// <summary>Implicitly converts a <see cref="UInt256"/> value to a <see cref="UInt512"/> value.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the <see cref="UInt512"/> representation of <paramref name="value"/>.</returns>
    public static implicit operator UInt512(UInt256 value) => new(UInt256.Zero, value);

    /// <summary>Implicitly converts a <see cref="char"/> value to a <see cref="UInt512"/> value.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the <see cref="UInt512"/> representation of <paramref name="value"/>.</returns>
    public static implicit operator UInt512(char value) => new(UInt256.Zero, value);
}
