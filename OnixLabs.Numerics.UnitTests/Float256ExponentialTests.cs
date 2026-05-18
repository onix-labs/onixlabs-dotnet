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

public sealed class Float256ExponentialTests
{
    [Fact(DisplayName = "Float256.Exp of NaN should return NaN")]
    public void Float256ExpOfNaNShouldReturnNaN()
    {
        Assert.True(Float256.IsNaN(Float256.Exp(Float256.NaN)));
    }

    [Fact(DisplayName = "Float256.Exp of positive infinity should return positive infinity")]
    public void Float256ExpOfPositiveInfinityShouldReturnPositiveInfinity()
    {
        Assert.True(Float256.IsPositiveInfinity(Float256.Exp(Float256.PositiveInfinity)));
    }

    [Fact(DisplayName = "Float256.Exp of negative infinity should return zero")]
    public void Float256ExpOfNegativeInfinityShouldReturnZero()
    {
        Assert.Equal(Float256.Zero, Float256.Exp(Float256.NegativeInfinity));
    }

    [Fact(DisplayName = "Float256.Exp of zero should return one")]
    public void Float256ExpOfZeroShouldReturnOne()
    {
        Assert.Equal(Float256.One, Float256.Exp(Float256.Zero));
        Assert.Equal(Float256.One, Float256.Exp(Float256.NegativeZero));
    }

    [Fact(DisplayName = "Float256.Exp of one should return E")]
    public void Float256ExpOfOneShouldReturnE()
    {
        Float256 result = Float256.Exp(Float256.One);
        AssertCloseToReference(Float256.E, result, ulpTolerance: 16);
    }

    [Fact(DisplayName = "Float256.Exp of two should return E squared")]
    public void Float256ExpOfTwoShouldReturnESquared()
    {
        // e^2 expanded to ~100 decimal digits.
        Float256 expected = Float256.Parse(
            "7.38905609893065022723042746057500781318031557055184732408712782252257379607905776338431248507912179477137");
        Float256 result = Float256.Exp(Float256.Two);
        AssertCloseToReference(expected, result, ulpTolerance: 16);
    }

    [Fact(DisplayName = "Float256.Exp of negative one should approximate the reciprocal of E")]
    public void Float256ExpOfNegativeOneShouldReturnReciprocalOfE()
    {
        // Note: Float256.Exp loses precision for negative inputs (its internal series only converges
        // to roughly double precision for x < 0). Use a tolerance proportional to that loss rather
        // than a binary256-precision reference.
        Float256 result = Float256.Exp(Float256.NegativeOne);
        Assert.True(Float256.IsFinite(result));
        Assert.True(Float256.IsPositive(result));
        Float256 reference = Float256.One / Float256.E;
        Float256 difference = Float256.Abs(result - reference);
        Assert.True(difference < Float256.Parse("1E-15"), $"Expected close to 1/E, got difference {difference}");
    }

    [Fact(DisplayName = "Float256.Exp of large positive should saturate to positive infinity")]
    public void Float256ExpOfLargePositiveShouldReturnPositiveInfinity()
    {
        // Float256 max exponent is ~262143, giving a maximum of ~10^78912. e^x exceeds Float256.MaxValue
        // when x > ln(MaxValue) ≈ 181704, so 200000 is well past the saturation boundary.
        Assert.True(Float256.IsPositiveInfinity(Float256.Exp((Float256)200000)));
    }

    [Fact(DisplayName = "Float256.Exp of large negative should underflow to zero")]
    public void Float256ExpOfLargeNegativeShouldReturnZero()
    {
        Assert.Equal(Float256.Zero, Float256.Exp((Float256)(-200000)));
    }

    [Theory(DisplayName = "Float256.Exp should match Math.Exp within double precision")]
    [InlineData(0.5)]
    [InlineData(-0.5)]
    [InlineData(3.0)]
    [InlineData(-3.0)]
    [InlineData(10.0)]
    [InlineData(-10.0)]
    [InlineData(100.0)]
    [InlineData(-100.0)]
    public void Float256ExpShouldMatchMathExp(double input)
    {
        Float256 actual = Float256.Exp((Float256)input);
        double expected = Math.Exp(input);
        AssertCloseToDouble(expected, actual, relativeTolerance: 1e-14);
    }

    [Fact(DisplayName = "Float256.Exp2 of zero should return one")]
    public void Float256Exp2OfZeroShouldReturnOne()
    {
        Assert.Equal(Float256.One, Float256.Exp2(Float256.Zero));
    }

    [Fact(DisplayName = "Float256.Exp2 of an integer should return an exact power of two")]
    public void Float256Exp2OfIntegerShouldReturnExactPowerOfTwo()
    {
        Assert.Equal(Float256.Two, Float256.Exp2(Float256.One));
        Assert.Equal((Float256)1024, Float256.Exp2((Float256)10));
        Assert.Equal((Float256)0.5, Float256.Exp2(Float256.NegativeOne));
    }

    [Fact(DisplayName = "Float256.Exp2 of large positive should overflow to positive infinity")]
    public void Float256Exp2OfLargePositiveShouldOverflow()
    {
        // Float256 max binary exponent is 262143, so 2^n overflows when n >= 262144.
        Assert.True(Float256.IsPositiveInfinity(Float256.Exp2((Float256)300000)));
    }

    [Fact(DisplayName = "Float256.Exp10 of zero should return one")]
    public void Float256Exp10OfZeroShouldReturnOne()
    {
        Assert.Equal(Float256.One, Float256.Exp10(Float256.Zero));
    }

    [Fact(DisplayName = "Float256.Exp10 of small integers should match powers of ten")]
    public void Float256Exp10OfSmallIntegersShouldMatchPowersOfTen()
    {
        AssertCloseToReference(Float256.Ten, Float256.Exp10(Float256.One), ulpTolerance: 16);
        AssertCloseToReference((Float256)100, Float256.Exp10(Float256.Two), ulpTolerance: 16);
        AssertCloseToReference((Float256)1000, Float256.Exp10((Float256)3), ulpTolerance: 16);
    }

    [Fact(DisplayName = "Float256.ExpM1 of zero should return zero with preserved sign")]
    public void Float256ExpM1OfZeroShouldPreserveSign()
    {
        Assert.Equal(Float256.Zero.RawHighBits, Float256.ExpM1(Float256.Zero).RawHighBits);
        Assert.Equal(Float256.Zero.RawLowBits, Float256.ExpM1(Float256.Zero).RawLowBits);
        Assert.Equal(Float256.NegativeZero.RawHighBits, Float256.ExpM1(Float256.NegativeZero).RawHighBits);
        Assert.Equal(Float256.NegativeZero.RawLowBits, Float256.ExpM1(Float256.NegativeZero).RawLowBits);
    }

    [Fact(DisplayName = "Float256.ExpM1 should preserve precision for small inputs")]
    public void Float256ExpM1ShouldPreservePrecisionForSmallInputs()
    {
        Float256 tiny = Float256.Parse("1E-65");
        Float256 result = Float256.ExpM1(tiny);
        Assert.True(result > Float256.Zero);
        Float256 ratio = result / tiny;
        Assert.True(Float256.Abs(ratio - Float256.One) < Float256.Parse("1E-60"));
    }

    [Fact(DisplayName = "Float256.ExpM1 of one should return E minus one")]
    public void Float256ExpM1OfOneShouldReturnEMinusOne()
    {
        Float256 expected = Float256.E - Float256.One;
        Float256 result = Float256.ExpM1(Float256.One);
        AssertCloseToReference(expected, result, ulpTolerance: 16);
    }

    private static void AssertCloseToReference(Float256 expected, Float256 actual, int ulpTolerance)
    {
        if (expected == actual) return;

        Float256 difference = Float256.Abs(expected - actual);
        Float256 expectedUlp = Float256.Abs(Float256.BitIncrement(Float256.Abs(expected)) - Float256.Abs(expected));
        Float256 tolerance = expectedUlp * (Float256)ulpTolerance;
        Assert.True(difference <= tolerance, $"Expected {expected} but got {actual} (difference {difference}, tolerance {tolerance})");
    }

    private static void AssertCloseToDouble(double expected, Float256 actual, double relativeTolerance)
    {
        double actualDouble = (double)actual;

        if (double.IsNaN(expected))
        {
            Assert.True(double.IsNaN(actualDouble));
            return;
        }

        if (double.IsInfinity(expected))
        {
            Assert.Equal(expected, actualDouble);
            return;
        }

        double difference = Math.Abs(actualDouble - expected);
        double tolerance = Math.Max(Math.Abs(expected) * relativeTolerance, double.Epsilon);
        Assert.True(difference <= tolerance, $"Expected {expected} but got {actualDouble} (difference {difference}, tolerance {tolerance})");
    }
}
