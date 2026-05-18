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

    [Fact(DisplayName = "Float256.Exp of negative one should equal the correctly-rounded reciprocal of E")]
    public void Float256ExpOfNegativeOneShouldReturnReciprocalOfE()
    {
        // Reference: e^(-1) to 100 decimal digits, then rounded to nearest Float256.
        // Float256.Exp(-1) is correctly-rounded so it must match this reference exactly.
        Float256 reference = Float256.Parse(
            "0.36787944117144232159552377016146086744581113103176783450783680169746149574489980335714727434591964374",
            System.Globalization.CultureInfo.InvariantCulture);
        Float256 result = Float256.Exp(Float256.NegativeOne);
        Assert.Equal(reference, result);
    }

    [Theory(DisplayName = "Float256.Exp of negative arguments should equal 1 / Exp(|x|) within a few ULPs (regression for the negative-input precision concern)")]
    [InlineData("-1")]
    [InlineData("-2")]
    [InlineData("-5")]
    [InlineData("-10")]
    [InlineData("-50")]
    [InlineData("-100")]
    [InlineData("-0.5")]
    [InlineData("-0.347")]
    [InlineData("-0.001")]
    public void Float256ExpOfNegativeShouldMatchReciprocalOfPositiveToFullPrecision(string xLiteral)
    {
        // Regression: a previous test comment claimed Exp(negative) only converged to ~17 digits.
        // The Taylor-series reduction in ExpCore actually achieves correctly-rounded precision; the
        // only entropy lost between Exp(-x) and 1/Exp(x) is the one ULP introduced by the division.
        Float256 x = Float256.Parse(xLiteral, System.Globalization.CultureInfo.InvariantCulture);
        Float256 forward = Float256.Exp(x);
        Float256 viaReciprocal = Float256.One / Float256.Exp(-x);
        Float256 difference = Float256.Abs(forward - viaReciprocal);
        // Tolerance: 8 relative ULPs at the value's magnitude. Binary256's relative ULP is 2^-236 ≈ 9.05e-72.
        Float256 tolerance = Float256.Abs(forward) * Float256.Parse("8E-71", System.Globalization.CultureInfo.InvariantCulture);
        Assert.True(difference <= tolerance, $"Exp({x}) = {forward}, 1/Exp(-x) = {viaReciprocal}, diff = {difference}, tolerance = {tolerance}");
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
        Assert.Equal(Float256.Zero.RawBits.Upper, Float256.ExpM1(Float256.Zero).RawBits.Upper);
        Assert.Equal(Float256.Zero.RawBits.Lower, Float256.ExpM1(Float256.Zero).RawBits.Lower);
        Assert.Equal(Float256.NegativeZero.RawBits.Upper, Float256.ExpM1(Float256.NegativeZero).RawBits.Upper);
        Assert.Equal(Float256.NegativeZero.RawBits.Lower, Float256.ExpM1(Float256.NegativeZero).RawBits.Lower);
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
