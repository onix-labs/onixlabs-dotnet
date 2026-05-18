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

public sealed class Float128PowerTests
{
    [Fact(DisplayName = "Float128.Pow of x and zero should always return one")]
    public void Float128PowOfXAndZeroShouldReturnOne()
    {
        Assert.Equal(Float128.One, Float128.Pow(Float128.Two, Float128.Zero));
        Assert.Equal(Float128.One, Float128.Pow(Float128.Zero, Float128.Zero));
        Assert.Equal(Float128.One, Float128.Pow(Float128.NaN, Float128.Zero));
        Assert.Equal(Float128.One, Float128.Pow(Float128.PositiveInfinity, Float128.Zero));
    }

    [Fact(DisplayName = "Float128.Pow of one and y should always return one")]
    public void Float128PowOfOneAndYShouldReturnOne()
    {
        Assert.Equal(Float128.One, Float128.Pow(Float128.One, Float128.Two));
        Assert.Equal(Float128.One, Float128.Pow(Float128.One, Float128.NaN));
        Assert.Equal(Float128.One, Float128.Pow(Float128.One, Float128.PositiveInfinity));
    }

    [Fact(DisplayName = "Float128.Pow of NaN base should return NaN unless exponent is zero")]
    public void Float128PowOfNaNBaseShouldReturnNaN()
    {
        Assert.True(Float128.IsNaN(Float128.Pow(Float128.NaN, Float128.Two)));
    }

    [Fact(DisplayName = "Float128.Pow of NaN exponent should return NaN unless base is one")]
    public void Float128PowOfNaNExponentShouldReturnNaN()
    {
        Assert.True(Float128.IsNaN(Float128.Pow(Float128.Two, Float128.NaN)));
    }

    [Fact(DisplayName = "Float128.Pow of zero and positive exponent should return zero")]
    public void Float128PowOfZeroAndPositiveShouldReturnZero()
    {
        Assert.Equal(Float128.Zero, Float128.Pow(Float128.Zero, Float128.Two));
        Assert.Equal(Float128.Zero, Float128.Pow(Float128.Zero, (Float128)0.5));
    }

    [Fact(DisplayName = "Float128.Pow of zero and negative exponent should return positive infinity")]
    public void Float128PowOfZeroAndNegativeShouldReturnPositiveInfinity()
    {
        Assert.True(Float128.IsPositiveInfinity(Float128.Pow(Float128.Zero, Float128.NegativeOne)));
        Assert.True(Float128.IsPositiveInfinity(Float128.Pow(Float128.Zero, -Float128.Two)));
    }

    [Fact(DisplayName = "Float128.Pow of negative-zero and negative odd integer should return negative infinity")]
    public void Float128PowOfNegativeZeroAndNegativeOddIntegerShouldReturnNegativeInfinity()
    {
        Assert.True(Float128.IsNegativeInfinity(Float128.Pow(Float128.NegativeZero, Float128.NegativeOne)));
        Assert.True(Float128.IsNegativeInfinity(Float128.Pow(Float128.NegativeZero, (Float128)(-3))));
    }

    [Fact(DisplayName = "Float128.Pow of negative-one and infinite exponent should return one")]
    public void Float128PowOfNegativeOneAndInfiniteShouldReturnOne()
    {
        Assert.Equal(Float128.One, Float128.Pow(Float128.NegativeOne, Float128.PositiveInfinity));
        Assert.Equal(Float128.One, Float128.Pow(Float128.NegativeOne, Float128.NegativeInfinity));
    }

    [Fact(DisplayName = "Float128.Pow of negative base and non-integer exponent should return NaN")]
    public void Float128PowOfNegativeBaseAndNonIntegerExponentShouldReturnNaN()
    {
        Assert.True(Float128.IsNaN(Float128.Pow(Float128.NegativeOne, (Float128)0.5)));
        Assert.True(Float128.IsNaN(Float128.Pow((Float128)(-2), (Float128)1.5)));
    }

    [Fact(DisplayName = "Float128.Pow of negative base and odd integer exponent should preserve sign")]
    public void Float128PowOfNegativeBaseAndOddIntegerShouldPreserveSign()
    {
        Assert.Equal(Float128.NegativeOne, Float128.Pow(Float128.NegativeOne, (Float128)3));
        Assert.Equal((Float128)(-8), Float128.Pow((Float128)(-2), (Float128)3));
    }

    [Fact(DisplayName = "Float128.Pow of negative base and even integer exponent should be positive")]
    public void Float128PowOfNegativeBaseAndEvenIntegerShouldBePositive()
    {
        Assert.Equal(Float128.One, Float128.Pow(Float128.NegativeOne, Float128.Two));
        Assert.Equal((Float128)4, Float128.Pow((Float128)(-2), Float128.Two));
    }

    [Theory(DisplayName = "Float128.Pow should match Math.Pow within double precision")]
    [InlineData(2.0, 10.0)]
    [InlineData(3.0, 4.0)]
    [InlineData(0.5, 2.0)]
    [InlineData(7.0, -3.0)]
    [InlineData(2.0, 0.5)]
    public void Float128PowShouldMatchMathPow(double x, double y)
    {
        Float128 actual = Float128.Pow((Float128)x, (Float128)y);
        double expected = Math.Pow(x, y);
        double actualDouble = (double)actual;
        double tolerance = Math.Max(Math.Abs(expected) * 1e-14, 1e-14);
        Assert.True(Math.Abs(actualDouble - expected) <= tolerance, $"Expected {expected} but got {actualDouble}");
    }

    [Fact(DisplayName = "Float128.Pow of two raised to power should give exact powers of two")]
    public void Float128PowOfTwoShouldGiveExactPowers()
    {
        Assert.Equal(Float128.Two, Float128.Pow(Float128.Two, Float128.One));
        Assert.Equal((Float128)4, Float128.Pow(Float128.Two, Float128.Two));
        Assert.Equal((Float128)1024, Float128.Pow(Float128.Two, (Float128)10));
    }

    [Fact(DisplayName = "Float128.Pow with negative integer exponent should match reciprocal of positive power")]
    public void Float128PowWithNegativeIntegerExponentShouldMatchReciprocal()
    {
        Float128 result = Float128.Pow(Float128.Two, (Float128)(-3));
        Float128 expected = Float128.One / (Float128)8;
        AssertCloseToReference(expected, result, ulpTolerance: 4);
    }

    [Fact(DisplayName = "Float128.Pow of positive infinity should saturate")]
    public void Float128PowOfPositiveInfinityShouldSaturate()
    {
        Assert.True(Float128.IsPositiveInfinity(Float128.Pow(Float128.PositiveInfinity, Float128.Two)));
        Assert.Equal(Float128.Zero, Float128.Pow(Float128.PositiveInfinity, Float128.NegativeOne));
    }

    [Fact(DisplayName = "Float128.Pow of fractional base raised to infinity should converge")]
    public void Float128PowOfFractionalBaseRaisedToInfinityShouldConverge()
    {
        Assert.Equal(Float128.Zero, Float128.Pow((Float128)0.5, Float128.PositiveInfinity));
        Assert.True(Float128.IsPositiveInfinity(Float128.Pow((Float128)0.5, Float128.NegativeInfinity)));
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
