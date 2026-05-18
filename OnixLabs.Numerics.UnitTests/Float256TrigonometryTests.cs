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

public sealed class Float256TrigonometryTests
{
    [Fact(DisplayName = "Float256.Sin of NaN should return NaN")]
    public void Float256SinOfNaNShouldReturnNaN()
    {
        Assert.True(Float256.IsNaN(Float256.Sin(Float256.NaN)));
    }

    [Fact(DisplayName = "Float256.Sin of infinity should return NaN")]
    public void Float256SinOfInfinityShouldReturnNaN()
    {
        Assert.True(Float256.IsNaN(Float256.Sin(Float256.PositiveInfinity)));
        Assert.True(Float256.IsNaN(Float256.Sin(Float256.NegativeInfinity)));
    }

    [Fact(DisplayName = "Float256.Sin of zero should preserve sign of zero")]
    public void Float256SinOfZeroShouldPreserveSign()
    {
        Assert.Equal(Float256.Zero.RawBits.Upper, Float256.Sin(Float256.Zero).RawBits.Upper);
        Assert.Equal(Float256.Zero.RawBits.Lower, Float256.Sin(Float256.Zero).RawBits.Lower);
        Assert.Equal(Float256.NegativeZero.RawBits.Upper, Float256.Sin(Float256.NegativeZero).RawBits.Upper);
        Assert.Equal(Float256.NegativeZero.RawBits.Lower, Float256.Sin(Float256.NegativeZero).RawBits.Lower);
    }

    [Fact(DisplayName = "Float256.Sin of π/6 should return approximately one half")]
    public void Float256SinOfPiOverSixShouldReturnOneHalf()
    {
        Float256 input = Float256.Pi / (Float256)6;
        Float256 result = Float256.Sin(input);
        AssertCloseToReference((Float256)0.5, result, ulpTolerance: 32);
    }

    [Fact(DisplayName = "Float256.Sin of π should return approximately zero")]
    public void Float256SinOfPiShouldReturnApproximatelyZero()
    {
        Float256 result = Float256.Sin(Float256.Pi);
        Assert.True(Float256.Abs(result) < Float256.Parse("1E-67"));
    }

    [Fact(DisplayName = "Float256.Sin of π/2 should return one")]
    public void Float256SinOfPiOverTwoShouldReturnOne()
    {
        Float256 result = Float256.Sin(Float256.Pi / Float256.Two);
        AssertCloseToReference(Float256.One, result, ulpTolerance: 32);
    }

    [Fact(DisplayName = "Float256.Cos of zero should return one")]
    public void Float256CosOfZeroShouldReturnOne()
    {
        Assert.Equal(Float256.One, Float256.Cos(Float256.Zero));
        Assert.Equal(Float256.One, Float256.Cos(Float256.NegativeZero));
    }

    [Fact(DisplayName = "Float256.Cos of π should return negative one")]
    public void Float256CosOfPiShouldReturnNegativeOne()
    {
        Float256 result = Float256.Cos(Float256.Pi);
        AssertCloseToReference(Float256.NegativeOne, result, ulpTolerance: 32);
    }

    [Fact(DisplayName = "Float256.Cos of π/2 should return approximately zero")]
    public void Float256CosOfPiOverTwoShouldReturnApproximatelyZero()
    {
        Float256 result = Float256.Cos(Float256.Pi / Float256.Two);
        Assert.True(Float256.Abs(result) < Float256.Parse("1E-67"));
    }

    [Fact(DisplayName = "Float256.Tan of zero should preserve sign of zero")]
    public void Float256TanOfZeroShouldPreserveSign()
    {
        Assert.Equal(Float256.Zero.RawBits.Upper, Float256.Tan(Float256.Zero).RawBits.Upper);
        Assert.Equal(Float256.Zero.RawBits.Lower, Float256.Tan(Float256.Zero).RawBits.Lower);
        Assert.Equal(Float256.NegativeZero.RawBits.Upper, Float256.Tan(Float256.NegativeZero).RawBits.Upper);
        Assert.Equal(Float256.NegativeZero.RawBits.Lower, Float256.Tan(Float256.NegativeZero).RawBits.Lower);
    }

    [Fact(DisplayName = "Float256.Tan of π/4 should return one")]
    public void Float256TanOfPiOverFourShouldReturnOne()
    {
        Float256 input = Float256.Pi / (Float256)4;
        Float256 result = Float256.Tan(input);
        AssertCloseToReference(Float256.One, result, ulpTolerance: 64);
    }

    [Fact(DisplayName = "Float256.SinCos should return matching tuple values")]
    public void Float256SinCosShouldReturnMatchingTupleValues()
    {
        Float256 input = Float256.Pi / (Float256)3;
        (Float256 sin, Float256 cos) = Float256.SinCos(input);
        AssertCloseToReference(Float256.Sin(input), sin, ulpTolerance: 1);
        AssertCloseToReference(Float256.Cos(input), cos, ulpTolerance: 1);
    }

    [Theory(DisplayName = "Float256 sine identity should hold for various angles")]
    [InlineData(0.1)]
    [InlineData(0.5)]
    [InlineData(1.0)]
    [InlineData(1.5)]
    [InlineData(2.0)]
    [InlineData(3.0)]
    [InlineData(-1.0)]
    [InlineData(-2.5)]
    public void Float256SinSquaredPlusCosSquaredShouldEqualOne(double input)
    {
        Float256 x = (Float256)input;
        Float256 sin = Float256.Sin(x);
        Float256 cos = Float256.Cos(x);
        Float256 sum = sin * sin + cos * cos;
        AssertCloseToReference(Float256.One, sum, ulpTolerance: 32);
    }

    [Theory(DisplayName = "Float256.Sin should match Math.Sin within double precision")]
    [InlineData(0.5)]
    [InlineData(1.0)]
    [InlineData(2.5)]
    [InlineData(-1.5)]
    public void Float256SinShouldMatchMathSin(double input)
    {
        Float256 actual = Float256.Sin((Float256)input);
        double expected = Math.Sin(input);
        double actualDouble = (double)actual;
        double tolerance = Math.Max(Math.Abs(expected) * 1e-14, 1e-14);
        Assert.True(Math.Abs(actualDouble - expected) <= tolerance, $"Expected {expected} but got {actualDouble}");
    }

    [Fact(DisplayName = "Float256.SinPi of integer should return zero with sign")]
    public void Float256SinPiOfIntegerShouldReturnZero()
    {
        Assert.Equal(Float256.Zero.RawBits.Upper, Float256.SinPi(Float256.Zero).RawBits.Upper);
        Assert.Equal(Float256.Zero.RawBits.Lower, Float256.SinPi(Float256.Zero).RawBits.Lower);
        Assert.True(Float256.IsZero(Float256.SinPi(Float256.One)));
        Assert.True(Float256.IsZero(Float256.SinPi(Float256.Two)));
    }

    [Fact(DisplayName = "Float256.CosPi of zero should return one")]
    public void Float256CosPiOfZeroShouldReturnOne()
    {
        Assert.Equal(Float256.One, Float256.CosPi(Float256.Zero));
    }

    [Fact(DisplayName = "Float256.SinCosPi should match Float256.SinCos of value times Pi")]
    public void Float256SinCosPiShouldMatchSinCosOfValueTimesPi()
    {
        Float256 input = (Float256)0.25;
        (Float256 sin, Float256 cos) = Float256.SinCosPi(input);
        (Float256 expectedSin, Float256 expectedCos) = Float256.SinCos(input * Float256.Pi);
        AssertCloseToReference(expectedSin, sin, ulpTolerance: 8);
        AssertCloseToReference(expectedCos, cos, ulpTolerance: 8);
    }

    private static void AssertCloseToReference(Float256 expected, Float256 actual, int ulpTolerance)
    {
        if (expected == actual) return;

        Float256 difference = Float256.Abs(expected - actual);
        Float256 expectedUlp = Float256.IsZero(expected)
            ? Float256.Epsilon
            : Float256.Abs(Float256.BitIncrement(Float256.Abs(expected)) - Float256.Abs(expected));
        Float256 tolerance = expectedUlp * (Float256)ulpTolerance;
        Assert.True(difference <= tolerance, $"Expected {expected} but got {actual} (difference {difference}, tolerance {tolerance})");
    }
}
