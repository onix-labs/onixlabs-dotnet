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

public readonly partial struct UInt256
{
    /// <summary>Implicitly converts a <see cref="byte"/> value to a <see cref="UInt256"/> value.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the <see cref="UInt256"/> representation of <paramref name="value"/>.</returns>
    public static implicit operator UInt256(byte value) => new(UInt128.Zero, value);

    /// <summary>Implicitly converts a <see cref="ushort"/> value to a <see cref="UInt256"/> value.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the <see cref="UInt256"/> representation of <paramref name="value"/>.</returns>
    public static implicit operator UInt256(ushort value) => new(UInt128.Zero, value);

    /// <summary>Implicitly converts a <see cref="uint"/> value to a <see cref="UInt256"/> value.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the <see cref="UInt256"/> representation of <paramref name="value"/>.</returns>
    public static implicit operator UInt256(uint value) => new(UInt128.Zero, value);

    /// <summary>Implicitly converts a <see cref="ulong"/> value to a <see cref="UInt256"/> value.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the <see cref="UInt256"/> representation of <paramref name="value"/>.</returns>
    public static implicit operator UInt256(ulong value) => new(UInt128.Zero, value);

    /// <summary>Implicitly converts a <see cref="UInt128"/> value to a <see cref="UInt256"/> value.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the <see cref="UInt256"/> representation of <paramref name="value"/>.</returns>
    public static implicit operator UInt256(UInt128 value) => new(UInt128.Zero, value);

    /// <summary>Implicitly converts a <see cref="char"/> value to a <see cref="UInt256"/> value.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the <see cref="UInt256"/> representation of <paramref name="value"/>.</returns>
    public static implicit operator UInt256(char value) => new(UInt128.Zero, value);
}
