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

public sealed class Float128TrigonometryTests
{
    [Fact(DisplayName = "Float128.Sin of NaN should return NaN")]
    public void Float128SinOfNaNShouldReturnNaN()
    {
        Assert.True(Float128.IsNaN(Float128.Sin(Float128.NaN)));
    }

    [Fact(DisplayName = "Float128.Sin of infinity should return NaN")]
    public void Float128SinOfInfinityShouldReturnNaN()
    {
        Assert.True(Float128.IsNaN(Float128.Sin(Float128.PositiveInfinity)));
        Assert.True(Float128.IsNaN(Float128.Sin(Float128.NegativeInfinity)));
    }

    [Fact(DisplayName = "Float128.Sin of zero should preserve sign of zero")]
    public void Float128SinOfZeroShouldPreserveSign()
    {
        Assert.Equal(Float128.Zero.RawBits, Float128.Sin(Float128.Zero).RawBits);
        Assert.Equal(Float128.NegativeZero.RawBits, Float128.Sin(Float128.NegativeZero).RawBits);
    }

    [Fact(DisplayName = "Float128.Sin of π/6 should return approximately one half")]
    public void Float128SinOfPiOverSixShouldReturnOneHalf()
    {
        Float128 input = Float128.Pi / (Float128)6;
        Float128 result = Float128.Sin(input);
        AssertCloseToReference((Float128)0.5, result, ulpTolerance: 32);
    }

    [Fact(DisplayName = "Float128.Sin of π should return approximately zero")]
    public void Float128SinOfPiShouldReturnApproximatelyZero()
    {
        Float128 result = Float128.Sin(Float128.Pi);
        Assert.True(Float128.Abs(result) < Float128.Parse("1E-32"));
    }

    [Fact(DisplayName = "Float128.Sin of π/2 should return one")]
    public void Float128SinOfPiOverTwoShouldReturnOne()
    {
        Float128 result = Float128.Sin(Float128.Pi / Float128.Two);
        AssertCloseToReference(Float128.One, result, ulpTolerance: 32);
    }

    [Fact(DisplayName = "Float128.Cos of zero should return one")]
    public void Float128CosOfZeroShouldReturnOne()
    {
        Assert.Equal(Float128.One, Float128.Cos(Float128.Zero));
        Assert.Equal(Float128.One, Float128.Cos(Float128.NegativeZero));
    }

    [Fact(DisplayName = "Float128.Cos of π should return negative one")]
    public void Float128CosOfPiShouldReturnNegativeOne()
    {
        Float128 result = Float128.Cos(Float128.Pi);
        AssertCloseToReference(Float128.NegativeOne, result, ulpTolerance: 32);
    }

    [Fact(DisplayName = "Float128.Cos of π/2 should return approximately zero")]
    public void Float128CosOfPiOverTwoShouldReturnApproximatelyZero()
    {
        Float128 result = Float128.Cos(Float128.Pi / Float128.Two);
        Assert.True(Float128.Abs(result) < Float128.Parse("1E-32"));
    }

    [Fact(DisplayName = "Float128.Tan of zero should preserve sign of zero")]
    public void Float128TanOfZeroShouldPreserveSign()
    {
        Assert.Equal(Float128.Zero.RawBits, Float128.Tan(Float128.Zero).RawBits);
        Assert.Equal(Float128.NegativeZero.RawBits, Float128.Tan(Float128.NegativeZero).RawBits);
    }

    [Fact(DisplayName = "Float128.Tan of π/4 should return one")]
    public void Float128TanOfPiOverFourShouldReturnOne()
    {
        Float128 input = Float128.Pi / (Float128)4;
        Float128 result = Float128.Tan(input);
        AssertCloseToReference(Float128.One, result, ulpTolerance: 64);
    }

    [Fact(DisplayName = "Float128.SinCos should return matching tuple values")]
    public void Float128SinCosShouldReturnMatchingTupleValues()
    {
        Float128 input = Float128.Pi / (Float128)3;
        (Float128 sin, Float128 cos) = Float128.SinCos(input);
        AssertCloseToReference(Float128.Sin(input), sin, ulpTolerance: 1);
        AssertCloseToReference(Float128.Cos(input), cos, ulpTolerance: 1);
    }

    [Theory(DisplayName = "Float128 sine identity should hold for various angles")]
    [InlineData(0.1)]
    [InlineData(0.5)]
    [InlineData(1.0)]
    [InlineData(1.5)]
    [InlineData(2.0)]
    [InlineData(3.0)]
    [InlineData(-1.0)]
    [InlineData(-2.5)]
    public void Float128SinSquaredPlusCosSquaredShouldEqualOne(double input)
    {
        Float128 x = (Float128)input;
        Float128 sin = Float128.Sin(x);
        Float128 cos = Float128.Cos(x);
        Float128 sum = sin * sin + cos * cos;
        AssertCloseToReference(Float128.One, sum, ulpTolerance: 32);
    }

    [Theory(DisplayName = "Float128.Sin should match Math.Sin within double precision")]
    [InlineData(0.5)]
    [InlineData(1.0)]
    [InlineData(2.5)]
    [InlineData(-1.5)]
    public void Float128SinShouldMatchMathSin(double input)
    {
        Float128 actual = Float128.Sin((Float128)input);
        double expected = Math.Sin(input);
        double actualDouble = (double)actual;
        double tolerance = Math.Max(Math.Abs(expected) * 1e-14, 1e-14);
        Assert.True(Math.Abs(actualDouble - expected) <= tolerance, $"Expected {expected} but got {actualDouble}");
    }

    [Fact(DisplayName = "Float128.SinPi of integer should return zero with sign")]
    public void Float128SinPiOfIntegerShouldReturnZero()
    {
        Assert.Equal(Float128.Zero.RawBits, Float128.SinPi(Float128.Zero).RawBits);
        Assert.True(Float128.IsZero(Float128.SinPi(Float128.One)));
        Assert.True(Float128.IsZero(Float128.SinPi(Float128.Two)));
    }

    [Fact(DisplayName = "Float128.CosPi of zero should return one")]
    public void Float128CosPiOfZeroShouldReturnOne()
    {
        Assert.Equal(Float128.One, Float128.CosPi(Float128.Zero));
    }

    [Fact(DisplayName = "Float128.SinCosPi should match Float128.SinCos of value times Pi")]
    public void Float128SinCosPiShouldMatchSinCosOfValueTimesPi()
    {
        Float128 input = (Float128)0.25;
        (Float128 sin, Float128 cos) = Float128.SinCosPi(input);
        (Float128 expectedSin, Float128 expectedCos) = Float128.SinCos(input * Float128.Pi);
        AssertCloseToReference(expectedSin, sin, ulpTolerance: 8);
        AssertCloseToReference(expectedCos, cos, ulpTolerance: 8);
    }

    private static void AssertCloseToReference(Float128 expected, Float128 actual, int ulpTolerance)
    {
        if (expected == actual) return;

        Float128 difference = Float128.Abs(expected - actual);
        Float128 expectedUlp = Float128.IsZero(expected)
            ? Float128.Epsilon
            : Float128.Abs(Float128.BitIncrement(Float128.Abs(expected)) - Float128.Abs(expected));
        Float128 tolerance = expectedUlp * (Float128)ulpTolerance;
        Assert.True(difference <= tolerance, $"Expected {expected} but got {actual} (difference {difference}, tolerance {tolerance})");
    }
}
