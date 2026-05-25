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
/// Represents component information about rational numbers.
/// </summary>
public readonly partial struct NumberInfo : IValueEquatable<NumberInfo>, IValueComparable<NumberInfo>, ISpanParsable<NumberInfo>, IFormattable
{
    /// <summary>
    /// Initializes a new instance of the <see cref="NumberInfo"/> struct.
    /// </summary>
    /// <param name="unscaledValue">The unscaled value of the represented number.</param>
    /// <param name="scale">The scale of the represented number.</param>
    internal NumberInfo(BigInteger unscaledValue, int scale) => (UnscaledValue, Scale) = (unscaledValue, scale);

    /// <summary>
    /// Gets the unscaled value of the represented number.
    /// The unscaled value is represented as a signed value, including any trailing zeros.
    /// If the represented number contains a fractional component, the trailing zeros are considered significant.
    /// </summary>
    /// <value>The unscaled <see cref="BigInteger"/> value of the represented number, preserving any trailing zeros.</value>
    public BigInteger UnscaledValue { get; }

    /// <summary>
    /// Gets the scale of the represented number.
    /// The scale is represented as a positive or neutral integer.
    /// The scale indicates how many digits from the right-hand side of the <see cref="UnscaledValue"/> represent the fractional component of the represented number.
    /// </summary>
    /// <value>The non-negative scale of the represented number.</value>
    public int Scale { get; }

    /// <summary>
    /// Gets the significand of the represented number.
    /// The significand is represented as a signed value, excluding any trailing zeros.
    /// If the represented number contains a fractional component, the trailing zeros are considered insignificant and must be calculated from the <see cref="Precision"/> of the represented number.
    /// </summary>
    /// <value>The <see cref="BigInteger"/> significand of the represented number, with trailing zeros removed.</value>
    public BigInteger Significand
    {
        get
        {
            if (UnscaledValue == BigInteger.Zero) return BigInteger.Zero;

            // Strip trailing zeros by repeated division. The previous implementation recomputed
            // BigInteger.Pow(10, exponent) on every iteration, making this O(k^2) in the trailing-zero count;
            // dividing by ten each step is O(k) and allocation-free for the divisor.
            BigInteger significand = UnscaledValue;
            while (significand % 10 == BigInteger.Zero) significand /= 10;
            return significand;
        }
    }

    /// <summary>
    /// Gets the exponent of the represented number.
    /// The exponent is represented as a positive, negative or neutral number.
    /// </summary>
    /// <value>The exponent of the represented number, expressed as a positive, negative or zero value.</value>
    public int Exponent
    {
        get
        {
            if (UnscaledValue == BigInteger.Zero) return 0;
            if (Scale == 0) return Precision - GenericMath.IntegerLength(Significand);
            return -(Scale - (GenericMath.IntegerLength(UnscaledValue) - GenericMath.IntegerLength(Significand)));
        }
    }

    /// <summary>
    /// Gets the precision of the represented number.
    /// The precision is represented as a positive value, indicating how many significant digits the represented number contains.
    /// If the represented number's <see cref="UnscaledValue"/> or <see cref="Significand"/> contain fewer digits that the <see cref="Precision"/>, then trailing zeros are considered significant.
    /// </summary>
    /// <value>The total number of significant digits in the represented number.</value>
    public int Precision => int.Max(GenericMath.IntegerLength(UnscaledValue), Scale + 1);

    /// <summary>
    /// Gets the sign of the represented number.
    /// The sign is represented as negative one for negative numbers, positive one for positive numbers; otherwise, zero.
    /// </summary>
    /// <value>Negative one if the represented number is negative; positive one if the represented number is positive; otherwise, zero.</value>
    public int Sign => UnscaledValue.Sign;

    /// <summary>
    /// Gets the scale factor of the represented number.
    /// </summary>
    /// <value>The scale factor, computed as ten raised to the power of <see cref="Scale"/>.</value>
    internal BigInteger ScaleFactor => BigInteger.Pow(10, Scale);

    /// <summary>
    /// Gets the integral component of the represented number.
    /// </summary>
    /// <value>The integral part of the represented number.</value>
    internal BigInteger Integer => UnscaledValue / ScaleFactor;

    /// <summary>
    /// Gets the fractional component of the represented number.
    /// </summary>
    /// <value>The non-negative fractional part of the represented number.</value>
    internal BigInteger Fraction => BigInteger.Abs(UnscaledValue - Integer * ScaleFactor);
}
