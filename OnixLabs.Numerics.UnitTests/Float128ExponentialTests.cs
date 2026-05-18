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

public sealed class Float128ExponentialTests
{
    [Fact(DisplayName = "Float128.Exp of NaN should return NaN")]
    public void Float128ExpOfNaNShouldReturnNaN()
    {
        Assert.True(Float128.IsNaN(Float128.Exp(Float128.NaN)));
    }

    [Fact(DisplayName = "Float128.Exp of positive infinity should return positive infinity")]
    public void Float128ExpOfPositiveInfinityShouldReturnPositiveInfinity()
    {
        Assert.True(Float128.IsPositiveInfinity(Float128.Exp(Float128.PositiveInfinity)));
    }

    [Fact(DisplayName = "Float128.Exp of negative infinity should return zero")]
    public void Float128ExpOfNegativeInfinityShouldReturnZero()
    {
        Assert.Equal(Float128.Zero, Float128.Exp(Float128.NegativeInfinity));
    }

    [Fact(DisplayName = "Float128.Exp of zero should return one")]
    public void Float128ExpOfZeroShouldReturnOne()
    {
        Assert.Equal(Float128.One, Float128.Exp(Float128.Zero));
        Assert.Equal(Float128.One, Float128.Exp(Float128.NegativeZero));
    }

    [Fact(DisplayName = "Float128.Exp of one should return E")]
    public void Float128ExpOfOneShouldReturnE()
    {
        Float128 result = Float128.Exp(Float128.One);
        AssertCloseToReference(Float128.E, result, ulpTolerance: 16);
    }

    [Fact(DisplayName = "Float128.Exp of two should return E squared")]
    public void Float128ExpOfTwoShouldReturnESquared()
    {
        Float128 expected = Float128.Parse("7.3890560989306502272304274605750078131803");
        Float128 result = Float128.Exp(Float128.Two);
        AssertCloseToReference(expected, result, ulpTolerance: 16);
    }

    [Fact(DisplayName = "Float128.Exp of negative one should return reciprocal of E")]
    public void Float128ExpOfNegativeOneShouldReturnReciprocalOfE()
    {
        Float128 expected = Float128.Parse("0.3678794411714423215955237701614608674458");
        Float128 result = Float128.Exp(Float128.NegativeOne);
        AssertCloseToReference(expected, result, ulpTolerance: 16);
    }

    [Fact(DisplayName = "Float128.Exp of large positive should saturate to positive infinity")]
    public void Float128ExpOfLargePositiveShouldReturnPositiveInfinity()
    {
        Assert.True(Float128.IsPositiveInfinity(Float128.Exp((Float128)20000)));
    }

    [Fact(DisplayName = "Float128.Exp of large negative should underflow to zero")]
    public void Float128ExpOfLargeNegativeShouldReturnZero()
    {
        Assert.Equal(Float128.Zero, Float128.Exp((Float128)(-20000)));
    }

    [Theory(DisplayName = "Float128.Exp should match Math.Exp within double precision")]
    [InlineData(0.5)]
    [InlineData(-0.5)]
    [InlineData(3.0)]
    [InlineData(-3.0)]
    [InlineData(10.0)]
    [InlineData(-10.0)]
    [InlineData(100.0)]
    [InlineData(-100.0)]
    public void Float128ExpShouldMatchMathExp(double input)
    {
        Float128 actual = Float128.Exp((Float128)input);
        double expected = Math.Exp(input);
        AssertCloseToDouble(expected, actual, relativeTolerance: 1e-14);
    }

    [Fact(DisplayName = "Float128.Exp2 of zero should return one")]
    public void Float128Exp2OfZeroShouldReturnOne()
    {
        Assert.Equal(Float128.One, Float128.Exp2(Float128.Zero));
    }

    [Fact(DisplayName = "Float128.Exp2 of an integer should return an exact power of two")]
    public void Float128Exp2OfIntegerShouldReturnExactPowerOfTwo()
    {
        Assert.Equal(Float128.Two, Float128.Exp2(Float128.One));
        Assert.Equal((Float128)1024, Float128.Exp2((Float128)10));
        Assert.Equal((Float128)0.5, Float128.Exp2(Float128.NegativeOne));
    }

    [Fact(DisplayName = "Float128.Exp2 of large positive should overflow to positive infinity")]
    public void Float128Exp2OfLargePositiveShouldOverflow()
    {
        Assert.True(Float128.IsPositiveInfinity(Float128.Exp2((Float128)20000)));
    }

    [Fact(DisplayName = "Float128.Exp10 of zero should return one")]
    public void Float128Exp10OfZeroShouldReturnOne()
    {
        Assert.Equal(Float128.One, Float128.Exp10(Float128.Zero));
    }

    [Fact(DisplayName = "Float128.Exp10 of small integers should match powers of ten")]
    public void Float128Exp10OfSmallIntegersShouldMatchPowersOfTen()
    {
        AssertCloseToReference(Float128.Ten, Float128.Exp10(Float128.One), ulpTolerance: 16);
        AssertCloseToReference((Float128)100, Float128.Exp10(Float128.Two), ulpTolerance: 16);
        AssertCloseToReference((Float128)1000, Float128.Exp10((Float128)3), ulpTolerance: 16);
    }

    [Fact(DisplayName = "Float128.ExpM1 of zero should return zero with preserved sign")]
    public void Float128ExpM1OfZeroShouldPreserveSign()
    {
        Assert.Equal(Float128.Zero.RawBits, Float128.ExpM1(Float128.Zero).RawBits);
        Assert.Equal(Float128.NegativeZero.RawBits, Float128.ExpM1(Float128.NegativeZero).RawBits);
    }

    [Fact(DisplayName = "Float128.ExpM1 should preserve precision for small inputs")]
    public void Float128ExpM1ShouldPreservePrecisionForSmallInputs()
    {
        Float128 tiny = Float128.Parse("1E-30");
        Float128 result = Float128.ExpM1(tiny);
        Assert.True(result > Float128.Zero);
        Float128 ratio = result / tiny;
        Assert.True(Float128.Abs(ratio - Float128.One) < Float128.Parse("1E-25"));
    }

    [Fact(DisplayName = "Float128.ExpM1 of one should return E minus one")]
    public void Float128ExpM1OfOneShouldReturnEMinusOne()
    {
        Float128 expected = Float128.E - Float128.One;
        Float128 result = Float128.ExpM1(Float128.One);
        AssertCloseToReference(expected, result, ulpTolerance: 16);
    }

    private static void AssertCloseToReference(Float128 expected, Float128 actual, int ulpTolerance)
    {
        if (expected == actual) return;

        Float128 difference = Float128.Abs(expected - actual);
        Float128 expectedUlp = Float128.Abs(Float128.BitIncrement(Float128.Abs(expected)) - Float128.Abs(expected));
        Float128 tolerance = expectedUlp * (Float128)ulpTolerance;
        Assert.True(difference <= tolerance, $"Expected {expected} but got {actual} (difference {difference}, tolerance {tolerance})");
    }

    private static void AssertCloseToDouble(double expected, Float128 actual, double relativeTolerance)
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
