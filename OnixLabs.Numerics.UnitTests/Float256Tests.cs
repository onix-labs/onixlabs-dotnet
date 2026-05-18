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

namespace OnixLabs.Numerics.UnitTests;

public sealed class Float256Tests
{
    [Fact(DisplayName = "Float256.Zero should have raw bits equal to zero")]
    public void Float256ZeroShouldHaveRawBitsEqualToZero()
    {
        Assert.Equal(UInt128.Zero, Float256.Zero.RawBits.Upper);
        Assert.Equal(UInt128.Zero, Float256.Zero.RawBits.Lower);
    }

    [Fact(DisplayName = "Float256.NegativeZero should differ from Zero only in the sign bit")]
    public void Float256NegativeZeroShouldDifferOnlyInSignBit()
    {
        Assert.NotEqual(Float256.Zero.RawBits.Upper, Float256.NegativeZero.RawBits.Upper);
        Assert.True(Float256.IsZero(Float256.NegativeZero));
        Assert.True(Float256.IsNegative(Float256.NegativeZero));
    }

    [Fact(DisplayName = "Float256.One should round-trip via ToString and Parse")]
    public void Float256OneShouldRoundTripViaToStringAndParse()
    {
        Float256 parsed = Float256.Parse(Float256.One.ToString());
        Assert.Equal(Float256.One, parsed);
    }

    [Fact(DisplayName = "Float256.Pi should be close to its decimal reference")]
    public void Float256PiShouldBeCloseToDecimalReference()
    {
        string text = Float256.Pi.ToString();
        Assert.StartsWith("3.1415926535897932384626433832795028841971693993751", text);
    }

    [Fact(DisplayName = "Float256.E should be close to its decimal reference")]
    public void Float256EShouldBeCloseToDecimalReference()
    {
        string text = Float256.E.ToString();
        Assert.StartsWith("2.7182818284590452353602874713526624977572470936999", text);
    }

    [Fact(DisplayName = "Float256.IsNaN should identify NaN")]
    public void Float256IsNaNShouldIdentifyNaN()
    {
        Assert.True(Float256.IsNaN(Float256.NaN));
        Assert.False(Float256.IsNaN(Float256.Zero));
        Assert.False(Float256.IsNaN(Float256.One));
        Assert.False(Float256.IsNaN(Float256.PositiveInfinity));
    }

    [Fact(DisplayName = "Float256.IsInfinity should identify both signed infinities")]
    public void Float256IsInfinityShouldIdentifyBothInfinities()
    {
        Assert.True(Float256.IsInfinity(Float256.PositiveInfinity));
        Assert.True(Float256.IsInfinity(Float256.NegativeInfinity));
        Assert.True(Float256.IsPositiveInfinity(Float256.PositiveInfinity));
        Assert.True(Float256.IsNegativeInfinity(Float256.NegativeInfinity));
    }

    [Fact(DisplayName = "Float256 equality should treat positive and negative zero as equal")]
    public void Float256EqualityShouldTreatZerosAsEqual()
    {
        Assert.True(Float256.Zero == Float256.NegativeZero);
        Assert.True(Float256.Zero.Equals(Float256.NegativeZero));
    }

    [Fact(DisplayName = "Float256 equality should treat NaN as not equal under operator semantics")]
    public void Float256EqualityShouldTreatNaNAsNotEqualUnderOperator()
    {
        Assert.False(Float256.NaN == Float256.NaN);
        Assert.True(Float256.NaN.Equals(Float256.NaN));
    }

    [Fact(DisplayName = "Float256 addition should match BigDecimal addition for small values")]
    public void Float256AdditionShouldMatchBigDecimal()
    {
        Float256 a = Float256.Parse("1.5");
        Float256 b = Float256.Parse("2.25");
        Float256 result = a + b;
        Assert.Equal(Float256.Parse("3.75"), result);
    }

    [Fact(DisplayName = "Float256 multiplication should preserve precision")]
    public void Float256MultiplicationShouldPreservePrecision()
    {
        Float256 a = Float256.Parse("3.5");
        Float256 b = Float256.Parse("2.0");
        Assert.Equal(Float256.Parse("7.0"), a * b);
    }

    [Fact(DisplayName = "Float256 division should give expected results")]
    public void Float256DivisionShouldGiveExpectedResults()
    {
        Float256 a = (Float256)10;
        Float256 b = (Float256)4;
        Assert.Equal((Float256)2.5, a / b);
    }

    [Fact(DisplayName = "Float256.Sqrt should produce correct results for perfect squares")]
    public void Float256SqrtShouldProduceCorrectResultsForPerfectSquares()
    {
        Assert.Equal(Float256.Two, Float256.Sqrt((Float256)4));
        Assert.Equal((Float256)3, Float256.Sqrt((Float256)9));
        Assert.Equal((Float256)10, Float256.Sqrt((Float256)100));
    }

    [Fact(DisplayName = "Float256.Exp of zero should return one")]
    public void Float256ExpOfZeroShouldReturnOne()
    {
        Assert.Equal(Float256.One, Float256.Exp(Float256.Zero));
    }

    [Fact(DisplayName = "Float256.Log of one should return zero")]
    public void Float256LogOfOneShouldReturnZero()
    {
        Assert.Equal(Float256.Zero, Float256.Log(Float256.One));
    }

    [Fact(DisplayName = "Float256.Log of Exp should be approximately identity")]
    public void Float256LogOfExpShouldBeApproximatelyIdentity()
    {
        Float256 x = (Float256)2;
        Float256 result = Float256.Log(Float256.Exp(x));
        Float256 diff = Float256.Abs(result - x);
        Assert.True(diff < Float256.Parse("1E-65"));
    }

    [Fact(DisplayName = "Float256.Sin of zero should return zero")]
    public void Float256SinOfZeroShouldReturnZero()
    {
        Assert.Equal(Float256.Zero, Float256.Sin(Float256.Zero));
    }

    [Fact(DisplayName = "Float256.Cos of zero should return one")]
    public void Float256CosOfZeroShouldReturnOne()
    {
        Assert.Equal(Float256.One, Float256.Cos(Float256.Zero));
    }

    [Fact(DisplayName = "Float256.Sin squared plus Cos squared should equal one")]
    public void Float256SinSquaredPlusCosSquaredShouldEqualOne()
    {
        Float256 x = (Float256)1;
        Float256 sin = Float256.Sin(x);
        Float256 cos = Float256.Cos(x);
        Float256 sum = sin * sin + cos * cos;
        Float256 diff = Float256.Abs(sum - Float256.One);
        Assert.True(diff < Float256.Parse("1E-65"));
    }

    [Fact(DisplayName = "Float256.Atan of one should return Pi/4")]
    public void Float256AtanOfOneShouldReturnPiOverFour()
    {
        Float256 result = Float256.Atan(Float256.One);
        Float256 expected = Float256.Pi / (Float256)4;
        Float256 diff = Float256.Abs(result - expected);
        Assert.True(diff < Float256.Parse("1E-65"));
    }

    [Fact(DisplayName = "Float256.Pow of integer exponent should be exact")]
    public void Float256PowOfIntegerExponentShouldBeExact()
    {
        Assert.Equal((Float256)8, Float256.Pow(Float256.Two, (Float256)3));
        Assert.Equal((Float256)1024, Float256.Pow(Float256.Two, (Float256)10));
    }

    [Fact(DisplayName = "Float256.Cbrt of 27 should return 3")]
    public void Float256CbrtOf27ShouldReturn3()
    {
        Float256 result = Float256.Cbrt((Float256)27);
        Float256 diff = Float256.Abs(result - (Float256)3);
        Assert.True(diff < Float256.Parse("1E-65"));
    }

    [Fact(DisplayName = "Float256.Hypot of 3 and 4 should return 5")]
    public void Float256HypotOf3And4ShouldReturn5()
    {
        Float256 result = Float256.Hypot((Float256)3, (Float256)4);
        Float256 diff = Float256.Abs(result - (Float256)5);
        Assert.True(diff < Float256.Parse("1E-65"));
    }

    [Fact(DisplayName = "Float256.IsPow2 should identify powers of two")]
    public void Float256IsPow2ShouldIdentifyPowersOfTwo()
    {
        Assert.True(Float256.IsPow2(Float256.One));
        Assert.True(Float256.IsPow2(Float256.Two));
        Assert.True(Float256.IsPow2((Float256)1024));
        Assert.False(Float256.IsPow2(Float256.Zero));
        Assert.False(Float256.IsPow2((Float256)3));
    }

    [Fact(DisplayName = "Float256 conversion from Float128 should preserve value")]
    public void Float256ConversionFromFloat128ShouldPreserveValue()
    {
        Float128 source = Float128.Parse("3.14159265358979323846");
        Float256 wide = source;
        Float128 narrowed = (Float128)wide;
        Assert.Equal(source, narrowed);
    }

    [Fact(DisplayName = "Float256.Truncate should preserve sign of negative fractional values")]
    public void Float256TruncateShouldPreserveSignOfNegativeFractional()
    {
        Float256 result = Float256.Truncate(Float256.Parse("-0.5"));
        Assert.True(Float256.IsZero(result));
        Assert.True(Float256.IsNegative(result));
    }

    [Fact(DisplayName = "Float256 abs should clear the sign bit")]
    public void Float256AbsShouldClearTheSignBit()
    {
        Float256 negative = -Float256.Two;
        Assert.Equal(Float256.Two, Float256.Abs(negative));
    }
}
