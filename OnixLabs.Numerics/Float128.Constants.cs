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

namespace OnixLabs.Numerics;

public readonly partial struct Float128
{
    /// <summary>
    /// The number of bits in the trailing significand field of an IEEE 754 binary128 value, excluding the implicit leading bit.
    /// </summary>
    internal const int TrailingSignificandBits = 112;

    /// <summary>
    /// The total number of significand bits, including the implicit leading bit, of an IEEE 754 binary128 value.
    /// </summary>
    internal const int SignificandPrecision = 113;

    /// <summary>
    /// The number of bits in the biased exponent field of an IEEE 754 binary128 value.
    /// </summary>
    internal const int BiasedExponentBits = 15;

    /// <summary>
    /// The exponent bias applied to the biased exponent of an IEEE 754 binary128 value.
    /// </summary>
    internal const int ExponentBias = 16383;

    /// <summary>
    /// The largest possible biased exponent of an IEEE 754 binary128 value, reserved to encode infinity and NaN.
    /// </summary>
    internal const int MaxBiasedExponent = 32767;

    /// <summary>
    /// The minimum unbiased exponent of a normal IEEE 754 binary128 value.
    /// </summary>
    internal const int MinNormalUnbiasedExponent = 1 - ExponentBias;

    /// <summary>
    /// The maximum unbiased exponent of a finite IEEE 754 binary128 value.
    /// </summary>
    internal const int MaxFiniteUnbiasedExponent = ExponentBias;

    /// <summary>
    /// The bit position of the sign bit within the IEEE 754 binary128 layout.
    /// </summary>
    internal const int SignShift = 127;

    /// <summary>
    /// The bit position of the least significant bit of the biased exponent within the IEEE 754 binary128 layout.
    /// </summary>
    internal const int BiasedExponentShift = TrailingSignificandBits;

    /// <summary>
    /// The sign bit mask of the IEEE 754 binary128 layout.
    /// </summary>
    internal static readonly UInt128 SignMask = UInt128.One << SignShift;

    /// <summary>
    /// The trailing significand bit mask of the IEEE 754 binary128 layout.
    /// </summary>
    internal static readonly UInt128 TrailingSignificandMask = (UInt128.One << TrailingSignificandBits) - UInt128.One;

    /// <summary>
    /// The biased exponent bit mask of the IEEE 754 binary128 layout.
    /// </summary>
    internal static readonly UInt128 BiasedExponentMask = (UInt128)MaxBiasedExponent << BiasedExponentShift;

    /// <summary>
    /// The implicit leading bit of a normalised IEEE 754 binary128 significand, expressed at the position it occupies in the 113-bit significand.
    /// </summary>
    internal static readonly UInt128 ImplicitSignificandBit = UInt128.One << TrailingSignificandBits;

    /// <summary>
    /// The bit that distinguishes a quiet NaN from a signalling NaN within the IEEE 754 binary128 layout.
    /// </summary>
    internal static readonly UInt128 QuietNaNBit = UInt128.One << (TrailingSignificandBits - 1);

    /// <summary>
    /// Gets the <see cref="Float128"/> value representing positive zero.
    /// </summary>
    /// <value>The <see cref="Float128"/> value representing positive zero.</value>
    public static Float128 Zero => new(UInt128.Zero);

    /// <summary>
    /// Gets the <see cref="Float128"/> value representing negative zero.
    /// </summary>
    /// <value>The <see cref="Float128"/> value representing negative zero.</value>
    public static Float128 NegativeZero => new(SignMask);

    /// <summary>
    /// Gets the <see cref="Float128"/> value representing positive one.
    /// </summary>
    /// <value>The <see cref="Float128"/> value representing positive one.</value>
    public static Float128 One => new((UInt128)ExponentBias << BiasedExponentShift);

    /// <summary>
    /// Gets the <see cref="Float128"/> value representing negative one.
    /// </summary>
    /// <value>The <see cref="Float128"/> value representing negative one.</value>
    public static Float128 NegativeOne => new(SignMask | ((UInt128)ExponentBias << BiasedExponentShift));

    /// <summary>
    /// Gets the <see cref="Float128"/> value representing the integer two.
    /// </summary>
    /// <value>The <see cref="Float128"/> value representing the integer two.</value>
    public static Float128 Two => new((UInt128)(ExponentBias + 1) << BiasedExponentShift);

    /// <summary>
    /// Gets the <see cref="Float128"/> value representing one half (0.5).
    /// </summary>
    /// <value>The <see cref="Float128"/> value representing one half (0.5).</value>
    internal static Float128 Half => new((UInt128)(ExponentBias - 1) << BiasedExponentShift);

    /// <summary>
    /// Gets the <see cref="Float128"/> value representing the integer ten.
    /// </summary>
    /// <value>The <see cref="Float128"/> value representing the integer ten.</value>
    public static Float128 Ten => new(((UInt128)(ExponentBias + 3) << BiasedExponentShift) | (UInt128.One << (TrailingSignificandBits - 2)));

    /// <summary>
    /// Gets the smallest positive finite <see cref="Float128"/> value greater than zero (a subnormal).
    /// </summary>
    /// <value>The smallest positive subnormal <see cref="Float128"/> value.</value>
    public static Float128 Epsilon => new(UInt128.One);

    /// <summary>
    /// Gets the largest finite positive <see cref="Float128"/> value.
    /// </summary>
    /// <value>The largest finite positive <see cref="Float128"/> value.</value>
    public static Float128 MaxValue => new(((UInt128)(MaxBiasedExponent - 1) << BiasedExponentShift) | TrailingSignificandMask);

    /// <summary>
    /// Gets the smallest finite negative <see cref="Float128"/> value.
    /// </summary>
    /// <value>The smallest finite negative <see cref="Float128"/> value.</value>
    public static Float128 MinValue => new(SignMask | ((UInt128)(MaxBiasedExponent - 1) << BiasedExponentShift) | TrailingSignificandMask);

    /// <summary>
    /// Gets the <see cref="Float128"/> value representing positive infinity.
    /// </summary>
    /// <value>The <see cref="Float128"/> value representing positive infinity.</value>
    public static Float128 PositiveInfinity => new((UInt128)MaxBiasedExponent << BiasedExponentShift);

    /// <summary>
    /// Gets the <see cref="Float128"/> value representing negative infinity.
    /// </summary>
    /// <value>The <see cref="Float128"/> value representing negative infinity.</value>
    public static Float128 NegativeInfinity => new(SignMask | ((UInt128)MaxBiasedExponent << BiasedExponentShift));

    /// <summary>
    /// Gets the canonical quiet not-a-number (NaN) <see cref="Float128"/> value.
    /// </summary>
    /// <value>The canonical quiet not-a-number (NaN) <see cref="Float128"/> value.</value>
    public static Float128 NaN => new(((UInt128)MaxBiasedExponent << BiasedExponentShift) | QuietNaNBit);

    /// <summary>
    /// Gets the <see cref="Float128"/> value with every bit of its IEEE 754 binary128 representation set.
    /// </summary>
    /// <value>The <see cref="Float128"/> value whose bit pattern equals <see cref="UInt128.MaxValue"/>; this is a negative NaN.</value>
    public static Float128 AllBitsSet => new(UInt128.MaxValue);

    /// <summary>Gets the natural logarithmic base, specified by the constant, e.</summary>
    /// <value>The <see cref="Float128"/> value of Euler's number, rounded to binary128 precision.</value>
    /// <remarks>The constant is parsed from a 40-significant-digit decimal representation to guarantee a bit-correct binary128 result.</remarks>
    public static Float128 E => CachedE;

    /// <summary>Gets the ratio of the circumference of a circle to its diameter, specified by the constant, π.</summary>
    /// <value>The <see cref="Float128"/> value of π, rounded to binary128 precision.</value>
    /// <remarks>The constant is parsed from a 40-significant-digit decimal representation to guarantee a bit-correct binary128 result.</remarks>
    public static Float128 Pi => CachedPi;

    /// <summary>Gets the number of radians in one turn, specified by the constant, τ.</summary>
    /// <value>The <see cref="Float128"/> value of τ, rounded to binary128 precision.</value>
    /// <remarks>The constant is parsed from a 40-significant-digit decimal representation to guarantee a bit-correct binary128 result.</remarks>
    public static Float128 Tau => CachedTau;

    /// <summary>
    /// The cached <see cref="Float128"/> value of Euler's number, rounded to binary128 precision.
    /// </summary>
    private static readonly Float128 CachedE = Parse("2.7182818284590452353602874713526624977572");

    /// <summary>
    /// The cached <see cref="Float128"/> value of the ratio of a circle's circumference to its diameter, rounded to binary128 precision.
    /// </summary>
    private static readonly Float128 CachedPi = Parse("3.1415926535897932384626433832795028841972");

    /// <summary>
    /// The cached <see cref="Float128"/> value of the number of radians in one turn, rounded to binary128 precision.
    /// </summary>
    private static readonly Float128 CachedTau = Parse("6.2831853071795864769252867665590057683944");

    /// <summary>
    /// The natural logarithm of two, rounded to binary128 precision.
    /// </summary>
    internal static readonly Float128 Ln2 = Parse("0.6931471805599453094172321214581765680755");

    /// <summary>
    /// The natural logarithm of ten, rounded to binary128 precision.
    /// </summary>
    internal static readonly Float128 Ln10 = Parse("2.3025850929940456840179914546843642076011");

    /// <summary>
    /// The base-2 logarithm of <see cref="E"/>, rounded to binary128 precision.
    /// </summary>
    internal static readonly Float128 Log2E = Parse("1.4426950408889634073599246810018921374266");

    /// <summary>
    /// The base-10 logarithm of <see cref="E"/>, rounded to binary128 precision.
    /// </summary>
    internal static readonly Float128 Log10E = Parse("0.4342944819032518276511289189166050822944");

    /// <summary>
    /// The square root of two, rounded to binary128 precision.
    /// </summary>
    internal static readonly Float128 SqrtTwo = Parse("1.4142135623730950488016887242096980785697");

    /// <summary>
    /// The constant π divided by two, rounded to binary128 precision.
    /// </summary>
    internal static readonly Float128 PiOver2 = Parse("1.5707963267948966192313216916397514420985");

    /// <summary>
    /// The constant π divided by four, rounded to binary128 precision.
    /// </summary>
    internal static readonly Float128 PiOver4 = Parse("0.7853981633974483096156608458198757210493");

    /// <summary>
    /// The constant two divided by π, rounded to binary128 precision.
    /// </summary>
    internal static readonly Float128 TwoOverPi = Parse("0.6366197723675813430755350534900574481378");

    /// <summary>
    /// The constant three times π divided by four, rounded to binary128 precision.
    /// </summary>
    internal static readonly Float128 ThreePiOver4 = Parse("2.3561944901923449288469825374596271631478");

    /// <inheritdoc cref="IAdditiveIdentity{TSelf,TResult}.AdditiveIdentity"/>
    static Float128 IAdditiveIdentity<Float128, Float128>.AdditiveIdentity => Zero;

    /// <inheritdoc cref="IMultiplicativeIdentity{TSelf,TResult}.MultiplicativeIdentity"/>
    static Float128 IMultiplicativeIdentity<Float128, Float128>.MultiplicativeIdentity => One;

    /// <inheritdoc cref="INumberBase{TSelf}.Radix"/>
    static int INumberBase<Float128>.Radix => 2;

    /// <inheritdoc cref="ISignedNumber{TSelf}.NegativeOne"/>
    static Float128 ISignedNumber<Float128>.NegativeOne => NegativeOne;
}
