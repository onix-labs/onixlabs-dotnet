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
using OnixLabs.Core;

namespace OnixLabs.Numerics;

/// <summary>
/// Represents an IEEE 754 binary128 quadruple-precision floating-point number.
/// </summary>
/// <remarks>
/// The binary128 format consists of a single sign bit, a 15-bit biased exponent and a 112-bit trailing significand,
/// with an implicit leading bit for normal values, yielding 113 bits of significand precision.
/// </remarks>
public readonly partial struct Float128 :
    IBinaryFloatingPointIeee754<Float128>,
    IMinMaxValue<Float128>,
    IValueEquatable<Float128>,
    IValueComparable<Float128>,
    IConvertible
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Float128"/> struct from the specified raw IEEE 754 binary128 bit pattern.
    /// </summary>
    /// <param name="rawBits">The raw IEEE 754 binary128 bit pattern from which to construct the <see cref="Float128"/> value.</param>
    public Float128(UInt128 rawBits) => RawBits = rawBits;

    /// <summary>
    /// Gets the raw IEEE 754 binary128 bit pattern that represents the current <see cref="Float128"/> value.
    /// </summary>
    /// <value>The raw 128-bit IEEE 754 binary128 representation of the current value.</value>
    public UInt128 RawBits { get; }
}
