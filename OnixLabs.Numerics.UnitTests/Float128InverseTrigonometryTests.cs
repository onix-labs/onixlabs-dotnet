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

namespace OnixLabs.Numerics.UnitTests;

public sealed class Float128InverseTrigonometryTests
{
    [Fact(DisplayName = "Float128.Atan of NaN should return NaN")]
    public void Float128AtanOfNaNShouldReturnNaN()
    {
        Assert.True(Float128.IsNaN(Float128.Atan(Float128.NaN)));
    }

    [Fact(DisplayName = "Float128.Atan of zero should preserve sign of zero")]
    public void Float128AtanOfZeroShouldPreserveSign()
    {
        Assert.Equal(Float128.Zero.Bits, Float128.Atan(Float128.Zero).Bits);
        Assert.Equal(Float128.NegativeZero.Bits, Float128.Atan(Float128.NegativeZero).Bits);
    }

    [Fact(DisplayName = "Float128.Atan of positive infinity should return π/2")]
    public void Float128AtanOfPositiveInfinityShouldReturnPiOverTwo()
    {
        Float128 expected = Float128.Pi / Float128.Two;
        AssertCloseToReference(expected, Float128.Atan(Float128.PositiveInfinity), ulpTolerance: 2);
    }

    [Fact(DisplayName = "Float128.Atan of negative infinity should return -π/2")]
    public void Float128AtanOfNegativeInfinityShouldReturnNegativePiOverTwo()
    {
        Float128 expected = -(Float128.Pi / Float128.Two);
        AssertCloseToReference(expected, Float128.Atan(Float128.NegativeInfinity), ulpTolerance: 2);
    }

    [Fact(DisplayName = "Float128.Atan of one should return π/4")]
    public void Float128AtanOfOneShouldReturnPiOverFour()
    {
        Float128 expected = Float128.Pi / (Float128)4;
        Float128 result = Float128.Atan(Float128.One);
        AssertCloseToReference(expected, result, ulpTolerance: 32);
    }

    [Theory(DisplayName = "Float128.Atan should match Math.Atan within double precision")]
    [InlineData(0.5)]
    [InlineData(-0.5)]
    [InlineData(1.0)]
    [InlineData(2.0)]
    [InlineData(10.0)]
    [InlineData(100.0)]
    [InlineData(-100.0)]
    public void Float128AtanShouldMatchMathAtan(double input)
    {
        Float128 actual = Float128.Atan((Float128)input);
        double expected = Math.Atan(input);
        double actualDouble = (double)actual;
        double tolerance = Math.Max(Math.Abs(expected) * 1e-14, 1e-14);
        Assert.True(Math.Abs(actualDouble - expected) <= tolerance, $"Expected {expected} but got {actualDouble}");
    }

    [Fact(DisplayName = "Float128.Atan must use a culture-invariant reduction threshold (regression: run isolated)")]
    public void Float128AtanShouldUseCultureInvariantThreshold()
    {
        // The atan reduction threshold is a decimal-point constant. A culture-sensitive misparse skips the
        // half-angle acceleration loop, leaving the series to converge only Leibniz-slowly near a reduced argument of 1.
        CultureInfo original = CultureInfo.CurrentCulture;
        CultureInfo.CurrentCulture = new CultureInfo("de-DE");

        try
        {
            // Inputs whose reduced argument lands in (threshold, 1]: without the acceleration loop the
            // series converges only Leibniz-slowly, so a misparsed threshold loses many digits here.
            foreach (double input in new[] { 0.5, 1.0, 1.5 })
            {
                double expected = Math.Atan(input);
                double actual = (double)Float128.Atan((Float128)input);
                Assert.True(Math.Abs(actual - expected) <= 1e-12, $"Atan({input}) = {actual}, expected {expected}");
            }
        }
        finally
        {
            CultureInfo.CurrentCulture = original;
        }
    }

    [Fact(DisplayName = "Float128.Asin of zero should preserve sign of zero")]
    public void Float128AsinOfZeroShouldPreserveSign()
    {
        Assert.Equal(Float128.Zero.Bits, Float128.Asin(Float128.Zero).Bits);
        Assert.Equal(Float128.NegativeZero.Bits, Float128.Asin(Float128.NegativeZero).Bits);
    }

    [Fact(DisplayName = "Float128.Asin of one should return π/2")]
    public void Float128AsinOfOneShouldReturnPiOverTwo()
    {
        Float128 expected = Float128.Pi / Float128.Two;
        Float128 result = Float128.Asin(Float128.One);
        AssertCloseToReference(expected, result, ulpTolerance: 2);
    }

    [Fact(DisplayName = "Float128.Asin of negative one should return -π/2")]
    public void Float128AsinOfNegativeOneShouldReturnNegativePiOverTwo()
    {
        Float128 expected = -(Float128.Pi / Float128.Two);
        Float128 result = Float128.Asin(Float128.NegativeOne);
        AssertCloseToReference(expected, result, ulpTolerance: 2);
    }

    [Fact(DisplayName = "Float128.Asin out of range should return NaN")]
    public void Float128AsinOutOfRangeShouldReturnNaN()
    {
        Assert.True(Float128.IsNaN(Float128.Asin((Float128)1.5)));
        Assert.True(Float128.IsNaN(Float128.Asin((Float128)(-1.5))));
    }

    [Fact(DisplayName = "Float128.Acos of one should return zero")]
    public void Float128AcosOfOneShouldReturnZero()
    {
        Assert.Equal(Float128.Zero, Float128.Acos(Float128.One));
    }

    [Fact(DisplayName = "Float128.Acos of negative one should return π")]
    public void Float128AcosOfNegativeOneShouldReturnPi()
    {
        Assert.Equal(Float128.Pi, Float128.Acos(Float128.NegativeOne));
    }

    [Fact(DisplayName = "Float128.Acos of zero should return π/2")]
    public void Float128AcosOfZeroShouldReturnPiOverTwo()
    {
        Float128 expected = Float128.Pi / Float128.Two;
        Float128 result = Float128.Acos(Float128.Zero);
        AssertCloseToReference(expected, result, ulpTolerance: 2);
    }

    [Theory(DisplayName = "Float128.Atan of Float128.Tan should be approximately identity in (-π/2, π/2)")]
    [InlineData(0.1)]
    [InlineData(0.5)]
    [InlineData(-0.5)]
    [InlineData(1.0)]
    [InlineData(-1.0)]
    public void Float128AtanOfTanShouldBeApproximatelyIdentity(double input)
    {
        Float128 x = (Float128)input;
        Float128 result = Float128.Atan(Float128.Tan(x));
        AssertCloseToReference(x, result, ulpTolerance: 256);
    }

    [Fact(DisplayName = "Float128.Atan2 of positive y and zero x should return π/2")]
    public void Float128Atan2OfPositiveYAndZeroXShouldReturnPiOverTwo()
    {
        Float128 expected = Float128.Pi / Float128.Two;
        Float128 result = Float128.Atan2(Float128.One, Float128.Zero);
        AssertCloseToReference(expected, result, ulpTolerance: 2);
    }

    [Fact(DisplayName = "Float128.Atan2 of negative y and zero x should return -π/2")]
    public void Float128Atan2OfNegativeYAndZeroXShouldReturnNegativePiOverTwo()
    {
        Float128 expected = -(Float128.Pi / Float128.Two);
        Float128 result = Float128.Atan2(Float128.NegativeOne, Float128.Zero);
        AssertCloseToReference(expected, result, ulpTolerance: 2);
    }

    [Fact(DisplayName = "Float128.Atan2 of zero y and positive x should return zero")]
    public void Float128Atan2OfZeroYAndPositiveXShouldReturnZero()
    {
        Assert.Equal(Float128.Zero, Float128.Atan2(Float128.Zero, Float128.One));
    }

    [Fact(DisplayName = "Float128.Atan2 of zero y and negative x should return π")]
    public void Float128Atan2OfZeroYAndNegativeXShouldReturnPi()
    {
        Assert.Equal(Float128.Pi, Float128.Atan2(Float128.Zero, Float128.NegativeOne));
    }

    [Fact(DisplayName = "Float128.Atan2 in first quadrant should match Atan(y/x)")]
    public void Float128Atan2InFirstQuadrantShouldMatchAtan()
    {
        Float128 y = Float128.One;
        Float128 x = Float128.One;
        Float128 result = Float128.Atan2(y, x);
        Float128 expected = Float128.Pi / (Float128)4;
        AssertCloseToReference(expected, result, ulpTolerance: 32);
    }

    [Fact(DisplayName = "Float128.Atan2 in second quadrant should return positive angle near π")]
    public void Float128Atan2InSecondQuadrantShouldReturnPositiveAngle()
    {
        Float128 y = Float128.One;
        Float128 x = Float128.NegativeOne;
        Float128 result = Float128.Atan2(y, x);
        Float128 expected = (Float128)3 * Float128.Pi / (Float128)4;
        AssertCloseToReference(expected, result, ulpTolerance: 32);
    }

    [Fact(DisplayName = "Float128.AsinPi of one should return one half")]
    public void Float128AsinPiOfOneShouldReturnOneHalf()
    {
        Float128 result = Float128.AsinPi(Float128.One);
        AssertCloseToReference((Float128)0.5, result, ulpTolerance: 16);
    }

    [Fact(DisplayName = "Float128.AcosPi of one should return zero")]
    public void Float128AcosPiOfOneShouldReturnZero()
    {
        Assert.Equal(Float128.Zero, Float128.AcosPi(Float128.One));
    }

    [Fact(DisplayName = "Float128.AtanPi of one should return one quarter")]
    public void Float128AtanPiOfOneShouldReturnOneQuarter()
    {
        Float128 result = Float128.AtanPi(Float128.One);
        AssertCloseToReference((Float128)0.25, result, ulpTolerance: 32);
    }

    [Fact(DisplayName = "Float128.Atan2Pi in first quadrant should match AtanPi(y/x)")]
    public void Float128Atan2PiInFirstQuadrantShouldMatchAtanPi()
    {
        Float128 result = Float128.Atan2Pi(Float128.One, Float128.One);
        AssertCloseToReference((Float128)0.25, result, ulpTolerance: 32);
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
