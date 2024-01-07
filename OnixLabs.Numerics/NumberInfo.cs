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

using System.Numerics;

namespace OnixLabs.Numerics;

/// <summary>
/// Represents component information about numeric values.
/// Numeric values may be represented as an unscaled value and scale, or as a significand and exponent.
/// </summary>
public readonly partial struct NumberInfo
{
    /// <summary>
    /// Prevents a default instance of the <see cref="NumberInfo"/> struct from being created.
    /// </summary>
    /// <param name="unscaledValue">Represents the unscaled value of the number being represented.</param>
    /// <param name="scale">Represents the scale of the number being represented.</param>
    private NumberInfo(BigInteger unscaledValue, int scale)
    {
        UnscaledValue = unscaledValue;
        Scale = scale;
    }

    /// <summary>
    /// Gets the unscaled value of the number, represented as a signed value with trailing zeros.
    /// In the event that the number being represented contains a fractional component, the trailing zeros are considered significant.
    /// </summary>
    public BigInteger UnscaledValue { get; }

    /// <summary>
    /// Gets the scale of the number, represented as a positive, or neutral integer.
    /// The scale indicates how many digits from the right of the unscaled value represent the number's fractional component.
    /// </summary>
    public int Scale { get; }

    /// <summary>
    /// Gets the precision of the number, represented as a positive integer.
    /// The precision indicates how many significant digits the number represents.
    /// In the event that the unscaled value or significand contain fewer digits than the precision, then trailing zeros are considered significant.
    /// </summary>
    public int Precision => int.Max(GenericMath.IntegerLength(UnscaledValue), Scale + 1);

    /// <summary>
    /// Gets the sign of the number, represented as negative one for negative numbers, positive one for positive numbers; otherwise, zero.
    /// </summary>
    public int Sign => UnscaledValue.Sign;

    /// <summary>
    /// Gets the significand of the number, represented as the number's significant digits, excluding any trailing zeros.
    /// The significand is always represented as a positive number.
    /// </summary>
    public BigInteger Significand
    {
        get
        {
            if (UnscaledValue == BigInteger.Zero) return BigInteger.Zero;

            BigInteger significand = BigInteger.Abs(UnscaledValue);
            int exponent = 0;

            while (significand % BigInteger.Pow(10, exponent) == BigInteger.Zero) exponent++;
            return significand / BigInteger.Pow(10, --exponent);
        }
    }

    /// <summary>
    /// Gets the exponent of the number, represented as a positive, negative or neutral number.
    /// </summary>
    public int Exponent
    {
        get
        {
            if (Scale == 0) return Precision - GenericMath.IntegerLength(Significand);
            return -(Scale - (GenericMath.IntegerLength(UnscaledValue) - GenericMath.IntegerLength(Significand)));
        }
    }
}
