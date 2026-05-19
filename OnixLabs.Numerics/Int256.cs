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
/// Represents a 256-bit signed integer (two's-complement).
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public readonly partial struct Int256 :
    IConvertible,
    IMinMaxValue<Int256>,
    IBinaryInteger<Int256>,
    ISignedNumber<Int256>,
    IValueEquatable<Int256>,
    IValueComparable<Int256>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Int256"/> struct from the specified upper and lower halves.
    /// </summary>
    /// <param name="upper">The upper 128 bits of the new <see cref="Int256"/> value (whose high bit is interpreted as the sign).</param>
    /// <param name="lower">The lower 128 bits of the new <see cref="Int256"/> value.</param>
    public Int256(UInt128 upper, UInt128 lower) => (UpperBits, LowerBits) = (upper, lower);

    /// <summary>
    /// Gets the upper 128 bits of the current <see cref="Int256"/> value.
    /// </summary>
    /// <value>The upper 128 bits; the high bit encodes the sign.</value>
    public UInt128 UpperBits { get; }

    /// <summary>
    /// Gets the lower 128 bits of the current <see cref="Int256"/> value.
    /// </summary>
    /// <value>The lower 128 bits.</value>
    public UInt128 LowerBits { get; }
}
