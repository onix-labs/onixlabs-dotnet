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
/// Represents a 512-bit unsigned integer.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public readonly partial struct UInt512 :
    IConvertible,
    IMinMaxValue<UInt512>,
    IBinaryInteger<UInt512>,
    IUnsignedNumber<UInt512>,
    IValueEquatable<UInt512>,
    IValueComparable<UInt512>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UInt512"/> struct from the specified upper and lower halves.
    /// </summary>
    /// <param name="upper">The upper 256 bits of the new <see cref="UInt512"/> value.</param>
    /// <param name="lower">The lower 256 bits of the new <see cref="UInt512"/> value.</param>
    public UInt512(UInt256 upper, UInt256 lower) => (Upper, Lower) = (upper, lower);

    /// <summary>
    /// Gets the upper 256 bits of the current <see cref="UInt512"/> value.
    /// </summary>
    /// <value>The upper 256 bits.</value>
    public UInt256 Upper { get; }

    /// <summary>
    /// Gets the lower 256 bits of the current <see cref="UInt512"/> value.
    /// </summary>
    /// <value>The lower 256 bits.</value>
    public UInt256 Lower { get; }
}
