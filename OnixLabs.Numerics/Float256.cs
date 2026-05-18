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
/// Represents an IEEE 754 binary256 octuple-precision floating-point number.
/// </summary>
/// <remarks>
/// The binary256 format consists of a single sign bit, a 19-bit biased exponent and a 236-bit trailing significand,
/// with an implicit leading bit for normal values, yielding 237 bits of significand precision.
/// </remarks>
public readonly partial struct Float256 :
    IBinaryFloatingPointIeee754<Float256>,
    IMinMaxValue<Float256>,
    IValueEquatable<Float256>,
    IValueComparable<Float256>,
    IConvertible
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Float256"/> struct from the specified raw IEEE 754 binary256 bit pattern.
    /// </summary>
    /// <param name="bits">The raw IEEE 754 binary256 bit pattern from which to construct the <see cref="Float256"/> value.</param>
    public Float256(UInt256 bits) => Bits = bits;

    /// <summary>
    /// Gets the raw IEEE 754 binary256 bit pattern that represents the current <see cref="Float256"/> value.
    /// </summary>
    /// <value>The raw 256-bit IEEE 754 binary256 representation of the current value.</value>
    public UInt256 Bits { get; }
}
