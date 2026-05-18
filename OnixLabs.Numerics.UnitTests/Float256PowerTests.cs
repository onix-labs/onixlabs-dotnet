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

public sealed class Float256PowerTests
{
    [Fact(DisplayName = "Float256.Pow of x and zero should always return one")]
    public void Float256PowOfXAndZeroShouldReturnOne()
    {
        Assert.Equal(Float256.One, Float256.Pow(Float256.Two, Float256.Zero));
        Assert.Equal(Float256.One, Float256.Pow(Float256.Zero, Float256.Zero));
        Assert.Equal(Float256.One, Float256.Pow(Float256.NaN, Float256.Zero));
        Assert.Equal(Float256.One, Float256.Pow(Float256.PositiveInfinity, Float256.Zero));
    }

    [Fact(DisplayName = "Float256.Pow of one and y should always return one")]
    public void Float256PowOfOneAndYShouldReturnOne()
    {
        Assert.Equal(Float256.One, Float256.Pow(Float256.One, Float256.Two));
        Assert.Equal(Float256.One, Float256.Pow(Float256.One, Float256.NaN));
        Assert.Equal(Float256.One, Float256.Pow(Float256.One, Float256.PositiveInfinity));
    }

    [Fact(DisplayName = "Float256.Pow of NaN base should return NaN unless exponent is zero")]
    public void Float256PowOfNaNBaseShouldReturnNaN()
    {
        Assert.True(Float256.IsNaN(Float256.Pow(Float256.NaN, Float256.Two)));
    }

    [Fact(DisplayName = "Float256.Pow of NaN exponent should return NaN unless base is one")]
    public void Float256PowOfNaNExponentShouldReturnNaN()
    {
        Assert.True(Float256.IsNaN(Float256.Pow(Float256.Two, Float256.NaN)));
    }

    [Fact(DisplayName = "Float256.Pow of zero and positive exponent should return zero")]
    public void Float256PowOfZeroAndPositiveShouldReturnZero()
    {
        Assert.Equal(Float256.Zero, Float256.Pow(Float256.Zero, Float256.Two));
        Assert.Equal(Float256.Zero, Float256.Pow(Float256.Zero, (Float256)0.5));
    }

    [Fact(DisplayName = "Float256.Pow of zero and negative exponent should return positive infinity")]
    public void Float256PowOfZeroAndNegativeShouldReturnPositiveInfinity()
    {
        Assert.True(Float256.IsPositiveInfinity(Float256.Pow(Float256.Zero, Float256.NegativeOne)));
        Assert.True(Float256.IsPositiveInfinity(Float256.Pow(Float256.Zero, -Float256.Two)));
    }

    [Fact(DisplayName = "Float256.Pow of negative-zero and negative odd integer should return negative infinity")]
    public void Float256PowOfNegativeZeroAndNegativeOddIntegerShouldReturnNegativeInfinity()
    {
        Assert.True(Float256.IsNegativeInfinity(Float256.Pow(Float256.NegativeZero, Float256.NegativeOne)));
        Assert.True(Float256.IsNegativeInfinity(Float256.Pow(Float256.NegativeZero, (Float256)(-3))));
    }

    [Fact(DisplayName = "Float256.Pow of negative-one and infinite exponent should return one")]
    public void Float256PowOfNegativeOneAndInfiniteShouldReturnOne()
    {
        Assert.Equal(Float256.One, Float256.Pow(Float256.NegativeOne, Float256.PositiveInfinity));
        Assert.Equal(Float256.One, Float256.Pow(Float256.NegativeOne, Float256.NegativeInfinity));
    }

    [Fact(DisplayName = "Float256.Pow of negative base and non-integer exponent should return NaN")]
    public void Float256PowOfNegativeBaseAndNonIntegerExponentShouldReturnNaN()
    {
        Assert.True(Float256.IsNaN(Float256.Pow(Float256.NegativeOne, (Float256)0.5)));
        Assert.True(Float256.IsNaN(Float256.Pow((Float256)(-2), (Float256)1.5)));
    }

    [Fact(DisplayName = "Float256.Pow of negative base and odd integer exponent should preserve sign")]
    public void Float256PowOfNegativeBaseAndOddIntegerShouldPreserveSign()
    {
        Assert.Equal(Float256.NegativeOne, Float256.Pow(Float256.NegativeOne, (Float256)3));
        Assert.Equal((Float256)(-8), Float256.Pow((Float256)(-2), (Float256)3));
    }

    [Fact(DisplayName = "Float256.Pow of negative base and even integer exponent should be positive")]
    public void Float256PowOfNegativeBaseAndEvenIntegerShouldBePositive()
    {
        Assert.Equal(Float256.One, Float256.Pow(Float256.NegativeOne, Float256.Two));
        Assert.Equal((Float256)4, Float256.Pow((Float256)(-2), Float256.Two));
    }

    [Theory(DisplayName = "Float256.Pow should match Math.Pow within double precision")]
    [InlineData(2.0, 10.0)]
    [InlineData(3.0, 4.0)]
    [InlineData(0.5, 2.0)]
    [InlineData(7.0, -3.0)]
    [InlineData(2.0, 0.5)]
    public void Float256PowShouldMatchMathPow(double x, double y)
    {
        Float256 actual = Float256.Pow((Float256)x, (Float256)y);
        double expected = Math.Pow(x, y);
        double actualDouble = (double)actual;
        double tolerance = Math.Max(Math.Abs(expected) * 1e-14, 1e-14);
        Assert.True(Math.Abs(actualDouble - expected) <= tolerance, $"Expected {expected} but got {actualDouble}");
    }

    [Fact(DisplayName = "Float256.Pow of two raised to power should give exact powers of two")]
    public void Float256PowOfTwoShouldGiveExactPowers()
    {
        Assert.Equal(Float256.Two, Float256.Pow(Float256.Two, Float256.One));
        Assert.Equal((Float256)4, Float256.Pow(Float256.Two, Float256.Two));
        Assert.Equal((Float256)1024, Float256.Pow(Float256.Two, (Float256)10));
    }

    [Fact(DisplayName = "Float256.Pow with negative integer exponent should match reciprocal of positive power")]
    public void Float256PowWithNegativeIntegerExponentShouldMatchReciprocal()
    {
        Float256 result = Float256.Pow(Float256.Two, (Float256)(-3));
        Float256 expected = Float256.One / (Float256)8;
        AssertCloseToReference(expected, result, ulpTolerance: 4);
    }

    [Fact(DisplayName = "Float256.Pow of positive infinity should saturate")]
    public void Float256PowOfPositiveInfinityShouldSaturate()
    {
        Assert.True(Float256.IsPositiveInfinity(Float256.Pow(Float256.PositiveInfinity, Float256.Two)));
        Assert.Equal(Float256.Zero, Float256.Pow(Float256.PositiveInfinity, Float256.NegativeOne));
    }

    [Fact(DisplayName = "Float256.Pow of fractional base raised to infinity should converge")]
    public void Float256PowOfFractionalBaseRaisedToInfinityShouldConverge()
    {
        Assert.Equal(Float256.Zero, Float256.Pow((Float256)0.5, Float256.PositiveInfinity));
        Assert.True(Float256.IsPositiveInfinity(Float256.Pow((Float256)0.5, Float256.NegativeInfinity)));
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
