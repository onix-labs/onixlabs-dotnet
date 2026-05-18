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

public sealed class Float256InverseTrigonometryTests
{
    [Fact(DisplayName = "Float256.Atan of NaN should return NaN")]
    public void Float256AtanOfNaNShouldReturnNaN()
    {
        Assert.True(Float256.IsNaN(Float256.Atan(Float256.NaN)));
    }

    [Fact(DisplayName = "Float256.Atan of zero should preserve sign of zero")]
    public void Float256AtanOfZeroShouldPreserveSign()
    {
        Assert.Equal(Float256.Zero.Bits.Upper, Float256.Atan(Float256.Zero).Bits.Upper);
        Assert.Equal(Float256.Zero.Bits.Lower, Float256.Atan(Float256.Zero).Bits.Lower);
        Assert.Equal(Float256.NegativeZero.Bits.Upper, Float256.Atan(Float256.NegativeZero).Bits.Upper);
        Assert.Equal(Float256.NegativeZero.Bits.Lower, Float256.Atan(Float256.NegativeZero).Bits.Lower);
    }

    [Fact(DisplayName = "Float256.Atan of positive infinity should return π/2")]
    public void Float256AtanOfPositiveInfinityShouldReturnPiOverTwo()
    {
        Float256 expected = Float256.Pi / Float256.Two;
        AssertCloseToReference(expected, Float256.Atan(Float256.PositiveInfinity), ulpTolerance: 2);
    }

    [Fact(DisplayName = "Float256.Atan of negative infinity should return -π/2")]
    public void Float256AtanOfNegativeInfinityShouldReturnNegativePiOverTwo()
    {
        Float256 expected = -(Float256.Pi / Float256.Two);
        AssertCloseToReference(expected, Float256.Atan(Float256.NegativeInfinity), ulpTolerance: 2);
    }

    [Fact(DisplayName = "Float256.Atan of one should return π/4")]
    public void Float256AtanOfOneShouldReturnPiOverFour()
    {
        Float256 expected = Float256.Pi / (Float256)4;
        Float256 result = Float256.Atan(Float256.One);
        AssertCloseToReference(expected, result, ulpTolerance: 32);
    }

    [Theory(DisplayName = "Float256.Atan should match Math.Atan within double precision")]
    [InlineData(0.5)]
    [InlineData(-0.5)]
    [InlineData(1.0)]
    [InlineData(2.0)]
    [InlineData(10.0)]
    [InlineData(100.0)]
    [InlineData(-100.0)]
    public void Float256AtanShouldMatchMathAtan(double input)
    {
        Float256 actual = Float256.Atan((Float256)input);
        Float256 expected = (Float256)Math.Atan(input);
        Float256 tolerance = Float256.Max(Float256.Abs(expected) * Float256.Parse("1E-14"), Float256.Parse("1E-14"));
        Assert.True(Float256.Abs(actual - expected) <= tolerance, $"Expected {expected} but got {actual}");
    }

    [Fact(DisplayName = "Float256.Asin of zero should preserve sign of zero")]
    public void Float256AsinOfZeroShouldPreserveSign()
    {
        Assert.Equal(Float256.Zero.Bits.Upper, Float256.Asin(Float256.Zero).Bits.Upper);
        Assert.Equal(Float256.Zero.Bits.Lower, Float256.Asin(Float256.Zero).Bits.Lower);
        Assert.Equal(Float256.NegativeZero.Bits.Upper, Float256.Asin(Float256.NegativeZero).Bits.Upper);
        Assert.Equal(Float256.NegativeZero.Bits.Lower, Float256.Asin(Float256.NegativeZero).Bits.Lower);
    }

    [Fact(DisplayName = "Float256.Asin of one should return π/2")]
    public void Float256AsinOfOneShouldReturnPiOverTwo()
    {
        Float256 expected = Float256.Pi / Float256.Two;
        Float256 result = Float256.Asin(Float256.One);
        AssertCloseToReference(expected, result, ulpTolerance: 2);
    }

    [Fact(DisplayName = "Float256.Asin of negative one should return -π/2")]
    public void Float256AsinOfNegativeOneShouldReturnNegativePiOverTwo()
    {
        Float256 expected = -(Float256.Pi / Float256.Two);
        Float256 result = Float256.Asin(Float256.NegativeOne);
        AssertCloseToReference(expected, result, ulpTolerance: 2);
    }

    [Fact(DisplayName = "Float256.Asin out of range should return NaN")]
    public void Float256AsinOutOfRangeShouldReturnNaN()
    {
        Assert.True(Float256.IsNaN(Float256.Asin((Float256)1.5)));
        Assert.True(Float256.IsNaN(Float256.Asin((Float256)(-1.5))));
    }

    [Fact(DisplayName = "Float256.Acos of one should return zero")]
    public void Float256AcosOfOneShouldReturnZero()
    {
        Assert.Equal(Float256.Zero, Float256.Acos(Float256.One));
    }

    [Fact(DisplayName = "Float256.Acos of negative one should return π")]
    public void Float256AcosOfNegativeOneShouldReturnPi()
    {
        Assert.Equal(Float256.Pi, Float256.Acos(Float256.NegativeOne));
    }

    [Fact(DisplayName = "Float256.Acos of zero should return π/2")]
    public void Float256AcosOfZeroShouldReturnPiOverTwo()
    {
        Float256 expected = Float256.Pi / Float256.Two;
        Float256 result = Float256.Acos(Float256.Zero);
        AssertCloseToReference(expected, result, ulpTolerance: 2);
    }

    [Theory(DisplayName = "Float256.Atan of Float256.Tan should be approximately identity in (-π/2, π/2)")]
    [InlineData(0.1)]
    [InlineData(0.5)]
    [InlineData(-0.5)]
    [InlineData(1.0)]
    [InlineData(-1.0)]
    public void Float256AtanOfTanShouldBeApproximatelyIdentity(double input)
    {
        Float256 x = (Float256)input;
        Float256 result = Float256.Atan(Float256.Tan(x));
        AssertCloseToReference(x, result, ulpTolerance: 256);
    }

    [Fact(DisplayName = "Float256.Atan2 of positive y and zero x should return π/2")]
    public void Float256Atan2OfPositiveYAndZeroXShouldReturnPiOverTwo()
    {
        Float256 expected = Float256.Pi / Float256.Two;
        Float256 result = Float256.Atan2(Float256.One, Float256.Zero);
        AssertCloseToReference(expected, result, ulpTolerance: 2);
    }

    [Fact(DisplayName = "Float256.Atan2 of negative y and zero x should return -π/2")]
    public void Float256Atan2OfNegativeYAndZeroXShouldReturnNegativePiOverTwo()
    {
        Float256 expected = -(Float256.Pi / Float256.Two);
        Float256 result = Float256.Atan2(Float256.NegativeOne, Float256.Zero);
        AssertCloseToReference(expected, result, ulpTolerance: 2);
    }

    [Fact(DisplayName = "Float256.Atan2 of zero y and positive x should return zero")]
    public void Float256Atan2OfZeroYAndPositiveXShouldReturnZero()
    {
        Assert.Equal(Float256.Zero, Float256.Atan2(Float256.Zero, Float256.One));
    }

    [Fact(DisplayName = "Float256.Atan2 of zero y and negative x should return π")]
    public void Float256Atan2OfZeroYAndNegativeXShouldReturnPi()
    {
        Assert.Equal(Float256.Pi, Float256.Atan2(Float256.Zero, Float256.NegativeOne));
    }

    [Fact(DisplayName = "Float256.Atan2 in first quadrant should match Atan(y/x)")]
    public void Float256Atan2InFirstQuadrantShouldMatchAtan()
    {
        Float256 y = Float256.One;
        Float256 x = Float256.One;
        Float256 result = Float256.Atan2(y, x);
        Float256 expected = Float256.Pi / (Float256)4;
        AssertCloseToReference(expected, result, ulpTolerance: 32);
    }

    [Fact(DisplayName = "Float256.Atan2 in second quadrant should return positive angle near π")]
    public void Float256Atan2InSecondQuadrantShouldReturnPositiveAngle()
    {
        Float256 y = Float256.One;
        Float256 x = Float256.NegativeOne;
        Float256 result = Float256.Atan2(y, x);
        Float256 expected = (Float256)3 * Float256.Pi / (Float256)4;
        AssertCloseToReference(expected, result, ulpTolerance: 32);
    }

    [Fact(DisplayName = "Float256.AsinPi of one should return one half")]
    public void Float256AsinPiOfOneShouldReturnOneHalf()
    {
        Float256 result = Float256.AsinPi(Float256.One);
        AssertCloseToReference((Float256)0.5, result, ulpTolerance: 16);
    }

    [Fact(DisplayName = "Float256.AcosPi of one should return zero")]
    public void Float256AcosPiOfOneShouldReturnZero()
    {
        Assert.Equal(Float256.Zero, Float256.AcosPi(Float256.One));
    }

    [Fact(DisplayName = "Float256.AtanPi of one should return one quarter")]
    public void Float256AtanPiOfOneShouldReturnOneQuarter()
    {
        Float256 result = Float256.AtanPi(Float256.One);
        AssertCloseToReference((Float256)0.25, result, ulpTolerance: 32);
    }

    [Fact(DisplayName = "Float256.Atan2Pi in first quadrant should match AtanPi(y/x)")]
    public void Float256Atan2PiInFirstQuadrantShouldMatchAtanPi()
    {
        Float256 result = Float256.Atan2Pi(Float256.One, Float256.One);
        AssertCloseToReference((Float256)0.25, result, ulpTolerance: 32);
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
