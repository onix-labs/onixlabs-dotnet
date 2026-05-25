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
using System.Runtime.InteropServices;
using OnixLabs.Core;

namespace OnixLabs.Numerics;

/// <summary>
/// Represents a 256-bit unsigned integer.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public readonly partial struct UInt256 :
    IConvertible,
    IMinMaxValue<UInt256>,
    IBinaryInteger<UInt256>,
    IUnsignedNumber<UInt256>,
    IValueEquatable<UInt256>,
    IValueComparable<UInt256>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UInt256"/> struct from the specified upper and lower halves.
    /// </summary>
    /// <param name="upper">The upper 128 bits of the new <see cref="UInt256"/> value.</param>
    /// <param name="lower">The lower 128 bits of the new <see cref="UInt256"/> value.</param>
    public UInt256(UInt128 upper, UInt128 lower) => (UpperBits, LowerBits) = (upper, lower);

    /// <summary>
    /// Gets the upper 128 bits of the current <see cref="UInt256"/> value.
    /// </summary>
    /// <value>The upper 128 bits.</value>
    public UInt128 UpperBits { get; }

    /// <summary>
    /// Gets the lower 128 bits of the current <see cref="UInt256"/> value.
    /// </summary>
    /// <value>The lower 128 bits.</value>
    public UInt128 LowerBits { get; }
}
