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
    IComparable,
    IComparable<Float256>,
    IConvertible,
    IEquatable<Float256>,
    IParsable<Float256>,
    ISpanFormattable,
    ISpanParsable<Float256>,
    IUtf8SpanFormattable,
    IMinMaxValue<Float256>,
    IAdditiveIdentity<Float256, Float256>,
    IMultiplicativeIdentity<Float256, Float256>,
    IAdditionOperators<Float256, Float256, Float256>,
    ISubtractionOperators<Float256, Float256, Float256>,
    IMultiplyOperators<Float256, Float256, Float256>,
    IDivisionOperators<Float256, Float256, Float256>,
    IModulusOperators<Float256, Float256, Float256>,
    IUnaryNegationOperators<Float256, Float256>,
    IUnaryPlusOperators<Float256, Float256>,
    IIncrementOperators<Float256>,
    IDecrementOperators<Float256>,
    IEqualityOperators<Float256, Float256, bool>,
    IComparisonOperators<Float256, Float256, bool>,
    INumberBase<Float256>,
    ISignedNumber<Float256>,
    IFloatingPointConstants<Float256>,
    IFloatingPoint<Float256>,
    IExponentialFunctions<Float256>,
    ILogarithmicFunctions<Float256>,
    ITrigonometricFunctions<Float256>,
    IHyperbolicFunctions<Float256>,
    IPowerFunctions<Float256>,
    IRootFunctions<Float256>,
    IBinaryNumber<Float256>,
    IFloatingPointIeee754<Float256>,
    IBinaryFloatingPointIeee754<Float256>,
    IValueEquatable<Float256>,
    IValueComparable<Float256>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Float256"/> struct from the specified raw IEEE 754 binary256 bit pattern.
    /// </summary>
    /// <param name="rawBits">The raw IEEE 754 binary256 bit pattern from which to construct the <see cref="Float256"/> value.</param>
    public Float256(UInt256 rawBits) => RawBits = rawBits;

    /// <summary>
    /// Gets the raw IEEE 754 binary256 bit pattern that represents the current <see cref="Float256"/> value.
    /// </summary>
    /// <value>The raw 256-bit IEEE 754 binary256 representation of the current value.</value>
    public UInt256 RawBits { get; }
}
