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
/// Represents a 512-bit signed integer (two's-complement).
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public readonly partial struct Int512 :
    IConvertible,
    IMinMaxValue<Int512>,
    IBinaryInteger<Int512>,
    ISignedNumber<Int512>,
    IUtf8SpanFormattable,
    IValueEquatable<Int512>,
    IValueComparable<Int512>
{
    /// <summary>
    /// The lower 256 bits of the current <see cref="Int512"/> value.
    /// </summary>
    private readonly UInt256 lower;

    /// <summary>
    /// The upper 256 bits of the current <see cref="Int512"/> value (whose high bit holds the sign).
    /// </summary>
    private readonly UInt256 upper;

    /// <summary>
    /// Initializes a new instance of the <see cref="Int512"/> struct from the specified upper and lower halves.
    /// </summary>
    /// <param name="upper">The upper 256 bits of the new <see cref="Int512"/> value (whose high bit is interpreted as the sign).</param>
    /// <param name="lower">The lower 256 bits of the new <see cref="Int512"/> value.</param>
    public Int512(UInt256 upper, UInt256 lower)
    {
        this.upper = upper;
        this.lower = lower;
    }

    /// <summary>
    /// Gets the upper 256 bits of the current <see cref="Int512"/> value.
    /// </summary>
    /// <value>The upper 256 bits; the high bit encodes the sign.</value>
    public UInt256 Upper => upper;

    /// <summary>
    /// Gets the lower 256 bits of the current <see cref="Int512"/> value.
    /// </summary>
    /// <value>The lower 256 bits.</value>
    public UInt256 Lower => lower;
}
