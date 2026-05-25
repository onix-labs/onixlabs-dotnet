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
using System.Globalization;
using System.Numerics;

namespace OnixLabs.Numerics;

public readonly partial struct Float256
{
    /// <summary>
    /// The number of bits in the trailing significand field of an IEEE 754 binary256 value, excluding the implicit leading bit.
    /// </summary>
    internal const int TrailingSignificandBits = 236;

    /// <summary>
    /// The total number of significand bits, including the implicit leading bit, of an IEEE 754 binary256 value.
    /// </summary>
    internal const int SignificandPrecision = 237;

    /// <summary>
    /// The number of bits in the biased exponent field of an IEEE 754 binary256 value.
    /// </summary>
    internal const int BiasedExponentBits = 19;

    /// <summary>
    /// The exponent bias applied to the biased exponent of an IEEE 754 binary256 value.
    /// </summary>
    internal const int ExponentBias = 262143;

    /// <summary>
    /// The largest possible biased exponent of an IEEE 754 binary256 value, reserved to encode infinity and NaN.
    /// </summary>
    internal const int MaxBiasedExponent = 524287;

    /// <summary>
    /// The minimum unbiased exponent of a normal IEEE 754 binary256 value.
    /// </summary>
    internal const int MinNormalUnbiasedExponent = 1 - ExponentBias;

    /// <summary>
    /// The maximum unbiased exponent of a finite IEEE 754 binary256 value.
    /// </summary>
    internal const int MaxFiniteUnbiasedExponent = ExponentBias;

    /// <summary>
    /// The bit position of the sign bit within the IEEE 754 binary256 layout.
    /// </summary>
    internal const int SignShift = 255;

    /// <summary>
    /// The bit position of the least significant bit of the biased exponent within the IEEE 754 binary256 layout.
    /// </summary>
    internal const int BiasedExponentShift = TrailingSignificandBits;

    /// <summary>
    /// The sign bit mask of the IEEE 754 binary256 layout.
    /// </summary>
    internal static readonly UInt256 SignMask = UInt256.One << SignShift;

    /// <summary>
    /// The trailing significand bit mask of the IEEE 754 binary256 layout.
    /// </summary>
    internal static readonly UInt256 TrailingSignificandMask = (UInt256.One << TrailingSignificandBits) - UInt256.One;

    /// <summary>
    /// The biased exponent bit mask of the IEEE 754 binary256 layout.
    /// </summary>
    internal static readonly UInt256 BiasedExponentMask = (new UInt256(UInt128.Zero, (UInt128)MaxBiasedExponent)) << BiasedExponentShift;

    /// <summary>
    /// The implicit leading bit of a normalised IEEE 754 binary256 significand, expressed at the position it occupies in the 237-bit significand.
    /// </summary>
    internal static readonly UInt256 ImplicitSignificandBit = UInt256.One << TrailingSignificandBits;

    /// <summary>
    /// The bit that distinguishes a quiet NaN from a signalling NaN within the IEEE 754 binary256 layout.
    /// </summary>
    internal static readonly UInt256 QuietNaNBit = UInt256.One << (TrailingSignificandBits - 1);

    /// <summary>
    /// Gets the <see cref="Float256"/> value representing positive zero.
    /// </summary>
    /// <value>The <see cref="Float256"/> value representing positive zero.</value>
    public static Float256 Zero => new(UInt256.Zero);

    /// <summary>
    /// Gets the <see cref="Float256"/> value representing negative zero.
    /// </summary>
    /// <value>The <see cref="Float256"/> value representing negative zero.</value>
    public static Float256 NegativeZero => new(SignMask);

    /// <summary>
    /// Gets the <see cref="Float256"/> value representing positive one.
    /// </summary>
    /// <value>The <see cref="Float256"/> value representing positive one.</value>
    public static Float256 One => new(new UInt256(UInt128.Zero, (UInt128)ExponentBias) << BiasedExponentShift);

    /// <summary>
    /// Gets the <see cref="Float256"/> value representing negative one.
    /// </summary>
    /// <value>The <see cref="Float256"/> value representing negative one.</value>
    public static Float256 NegativeOne => new(SignMask | (new UInt256(UInt128.Zero, (UInt128)ExponentBias) << BiasedExponentShift));

    /// <summary>
    /// Gets the <see cref="Float256"/> value representing the integer two.
    /// </summary>
    /// <value>The <see cref="Float256"/> value representing the integer two.</value>
    public static Float256 Two => new(new UInt256(UInt128.Zero, (UInt128)(ExponentBias + 1)) << BiasedExponentShift);

    /// <summary>
    /// Gets the <see cref="Float256"/> value representing one half (0.5).
    /// </summary>
    /// <value>The <see cref="Float256"/> value representing one half (0.5).</value>
    internal static Float256 Half => new(new UInt256(UInt128.Zero, (UInt128)(ExponentBias - 1)) << BiasedExponentShift);

    /// <summary>
    /// Gets the <see cref="Float256"/> value representing the integer ten.
    /// </summary>
    /// <value>The <see cref="Float256"/> value representing the integer ten.</value>
    public static Float256 Ten => Parse("10", CultureInfo.InvariantCulture);

    /// <summary>
    /// Gets the smallest positive finite <see cref="Float256"/> value greater than zero (a subnormal).
    /// </summary>
    /// <value>The smallest positive subnormal <see cref="Float256"/> value.</value>
    public static Float256 Epsilon => new(UInt256.One);

    /// <summary>
    /// Gets the largest finite positive <see cref="Float256"/> value.
    /// </summary>
    /// <value>The largest finite positive <see cref="Float256"/> value.</value>
    public static Float256 MaxValue => new((new UInt256(UInt128.Zero, (UInt128)(MaxBiasedExponent - 1)) << BiasedExponentShift) | TrailingSignificandMask);

    /// <summary>
    /// Gets the smallest finite negative <see cref="Float256"/> value.
    /// </summary>
    /// <value>The smallest finite negative <see cref="Float256"/> value.</value>
    public static Float256 MinValue => new(SignMask | (new UInt256(UInt128.Zero, (UInt128)(MaxBiasedExponent - 1)) << BiasedExponentShift) | TrailingSignificandMask);

    /// <summary>
    /// Gets the <see cref="Float256"/> value representing positive infinity.
    /// </summary>
    /// <value>The <see cref="Float256"/> value representing positive infinity.</value>
    public static Float256 PositiveInfinity => new(new UInt256(UInt128.Zero, (UInt128)MaxBiasedExponent) << BiasedExponentShift);

    /// <summary>
    /// Gets the <see cref="Float256"/> value representing negative infinity.
    /// </summary>
    /// <value>The <see cref="Float256"/> value representing negative infinity.</value>
    public static Float256 NegativeInfinity => new(SignMask | (new UInt256(UInt128.Zero, (UInt128)MaxBiasedExponent) << BiasedExponentShift));

    /// <summary>
    /// Gets the canonical quiet not-a-number (NaN) <see cref="Float256"/> value.
    /// </summary>
    /// <value>The canonical quiet not-a-number (NaN) <see cref="Float256"/> value.</value>
    public static Float256 NaN => new((new UInt256(UInt128.Zero, (UInt128)MaxBiasedExponent) << BiasedExponentShift) | QuietNaNBit);

    /// <summary>
    /// Gets the <see cref="Float256"/> value with every bit of its IEEE 754 binary256 representation set.
    /// </summary>
    /// <value>The <see cref="Float256"/> value whose bit pattern equals <see cref="UInt256.MaxValue"/>; this is a negative NaN.</value>
    public static Float256 AllBitsSet => new(UInt256.MaxValue);

    /// <summary>Gets the natural logarithmic base, specified by the constant, e.</summary>
    /// <value>The <see cref="Float256"/> value of Euler's number, rounded to binary256 precision.</value>
    /// <remarks>The constant is parsed from a 108-significant-digit decimal representation to guarantee a bit-correct binary256 result.</remarks>
    public static Float256 E => CachedE;

    /// <summary>Gets the ratio of the circumference of a circle to its diameter, specified by the constant, π.</summary>
    /// <value>The <see cref="Float256"/> value of π, rounded to binary256 precision.</value>
    /// <remarks>The constant is parsed from a 108-significant-digit decimal representation to guarantee a bit-correct binary256 result.</remarks>
    public static Float256 Pi => CachedPi;

    /// <summary>Gets the number of radians in one turn, specified by the constant, τ.</summary>
    /// <value>The <see cref="Float256"/> value of τ, rounded to binary256 precision.</value>
    /// <remarks>The constant is parsed from a 108-significant-digit decimal representation to guarantee a bit-correct binary256 result.</remarks>
    public static Float256 Tau => CachedTau;

    /// <summary>
    /// The natural exponential constant <see cref="E"/>, cached at binary256 precision to avoid repeated parsing.
    /// </summary>
    private static readonly Float256 CachedE = Parse("2.71828182845904523536028747135266249775724709369995957496696762772407663035354759457138217852516642742746", CultureInfo.InvariantCulture);

    /// <summary>
    /// The mathematical constant π, cached at binary256 precision to avoid repeated parsing.
    /// </summary>
    private static readonly Float256 CachedPi = Parse("3.14159265358979323846264338327950288419716939937510582097494459230781640628620899862803482534211706798214", CultureInfo.InvariantCulture);

    /// <summary>
    /// The mathematical constant τ (one full turn in radians), cached at binary256 precision to avoid repeated parsing.
    /// </summary>
    private static readonly Float256 CachedTau = Parse("6.28318530717958647692528676655900576839433879875021164194988918461563281257241799725606965068423413596429", CultureInfo.InvariantCulture);

    /// <summary>
    /// The natural logarithm of two, rounded to binary256 precision.
    /// </summary>
    internal static readonly Float256 Ln2 = Parse("0.69314718055994530941723212145817656807550013436025525412068000949339362196969471560586332699641868754200", CultureInfo.InvariantCulture);

    /// <summary>
    /// The natural logarithm of ten, rounded to binary256 precision.
    /// </summary>
    internal static readonly Float256 Ln10 = Parse("2.30258509299404568401799145468436420760110148862877297603332790096757260967735248023599720508959829834197", CultureInfo.InvariantCulture);

    /// <summary>
    /// The base-2 logarithm of <see cref="E"/>, rounded to binary256 precision.
    /// </summary>
    internal static readonly Float256 Log2E = Parse("1.44269504088896340735992468100189213742664595415298593413544940693110921918118507988552662289350634449699", CultureInfo.InvariantCulture);

    /// <summary>
    /// The base-10 logarithm of <see cref="E"/>, rounded to binary256 precision.
    /// </summary>
    internal static readonly Float256 Log10E = Parse("0.43429448190325182765112891891660508229439700580366656611445378316586464920887077472922494933843174831870", CultureInfo.InvariantCulture);

    /// <summary>
    /// The square root of two, rounded to binary256 precision.
    /// </summary>
    internal static readonly Float256 SqrtTwo = Parse("1.41421356237309504880168872420969807856967187537694807317667973799073247846210703885038753432764157273501", CultureInfo.InvariantCulture);

    /// <summary>
    /// The constant π divided by two, rounded to binary256 precision.
    /// </summary>
    internal static readonly Float256 PiOver2 = Parse("1.57079632679489661923132169163975144209858469968755291048747229615390820314310449931401741267105853399107", CultureInfo.InvariantCulture);

    /// <summary>
    /// The constant π divided by four, rounded to binary256 precision.
    /// </summary>
    internal static readonly Float256 PiOver4 = Parse("0.78539816339744830961566084581987572104929234984377645524373614807695410157155224965700870633552926699553", CultureInfo.InvariantCulture);

    /// <summary>
    /// The constant two divided by π, rounded to binary256 precision.
    /// </summary>
    internal static readonly Float256 TwoOverPi = Parse("0.63661977236758134307553505349005744813783858296182579499066937623558719053934030817241015149997655711782", CultureInfo.InvariantCulture);

    /// <summary>
    /// The constant three times π divided by four, rounded to binary256 precision.
    /// </summary>
    internal static readonly Float256 ThreePiOver4 = Parse("2.35619449019234492884698253745962716314787704953132936573120453390050120206321674897644612900658560098661", CultureInfo.InvariantCulture);

    /// <inheritdoc cref="IAdditiveIdentity{TSelf,TResult}.AdditiveIdentity"/>
    static Float256 IAdditiveIdentity<Float256, Float256>.AdditiveIdentity => Zero;

    /// <inheritdoc cref="IMultiplicativeIdentity{TSelf,TResult}.MultiplicativeIdentity"/>
    static Float256 IMultiplicativeIdentity<Float256, Float256>.MultiplicativeIdentity => One;

    /// <inheritdoc cref="INumberBase{TSelf}.Radix"/>
    static int INumberBase<Float256>.Radix => 2;

    /// <inheritdoc cref="ISignedNumber{TSelf}.NegativeOne"/>
    static Float256 ISignedNumber<Float256>.NegativeOne => NegativeOne;
}
