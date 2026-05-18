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

public sealed class Float256LogarithmTests
{
    [Fact(DisplayName = "Float256.Log of NaN should return NaN")]
    public void Float256LogOfNaNShouldReturnNaN()
    {
        Assert.True(Float256.IsNaN(Float256.Log(Float256.NaN)));
    }

    [Fact(DisplayName = "Float256.Log of zero should return negative infinity")]
    public void Float256LogOfZeroShouldReturnNegativeInfinity()
    {
        Assert.True(Float256.IsNegativeInfinity(Float256.Log(Float256.Zero)));
        Assert.True(Float256.IsNegativeInfinity(Float256.Log(Float256.NegativeZero)));
    }

    [Fact(DisplayName = "Float256.Log of negative finite should return NaN")]
    public void Float256LogOfNegativeFiniteShouldReturnNaN()
    {
        Assert.True(Float256.IsNaN(Float256.Log(Float256.NegativeOne)));
        Assert.True(Float256.IsNaN(Float256.Log(Float256.MinValue)));
    }

    [Fact(DisplayName = "Float256.Log of negative infinity should return NaN")]
    public void Float256LogOfNegativeInfinityShouldReturnNaN()
    {
        Assert.True(Float256.IsNaN(Float256.Log(Float256.NegativeInfinity)));
    }

    [Fact(DisplayName = "Float256.Log of positive infinity should return positive infinity")]
    public void Float256LogOfPositiveInfinityShouldReturnPositiveInfinity()
    {
        Assert.True(Float256.IsPositiveInfinity(Float256.Log(Float256.PositiveInfinity)));
    }

    [Fact(DisplayName = "Float256.Log of one should return zero")]
    public void Float256LogOfOneShouldReturnZero()
    {
        Assert.Equal(Float256.Zero, Float256.Log(Float256.One));
    }

    [Fact(DisplayName = "Float256.Log of E should return one")]
    public void Float256LogOfEShouldReturnOne()
    {
        Float256 result = Float256.Log(Float256.E);
        AssertCloseToReference(Float256.One, result, ulpTolerance: 16);
    }

    [Fact(DisplayName = "Float256.Log of two should return Ln2")]
    public void Float256LogOfTwoShouldReturnLn2()
    {
        // ln(2) expanded to ~100 decimal digits to match binary256 precision.
        Float256 expected = Float256.Parse(
            "0.69314718055994530941723212145817656807550013436025525412068000949339362196969471560586332699641868754200");
        Float256 result = Float256.Log(Float256.Two);
        AssertCloseToReference(expected, result, ulpTolerance: 16);
    }

    [Fact(DisplayName = "Float256.Log of ten should return Ln10")]
    public void Float256LogOfTenShouldReturnLn10()
    {
        // ln(10) expanded to ~100 decimal digits to match binary256 precision.
        Float256 expected = Float256.Parse(
            "2.30258509299404568401799145468436420760110148862877297603332790096757260967735248023599720508959829834197");
        Float256 result = Float256.Log(Float256.Ten);
        AssertCloseToReference(expected, result, ulpTolerance: 16);
    }

    [Theory(DisplayName = "Float256.Log should match Math.Log within double precision")]
    [InlineData(0.5)]
    [InlineData(1.5)]
    [InlineData(2.0)]
    [InlineData(7.0)]
    [InlineData(100.0)]
    [InlineData(1e20)]
    public void Float256LogShouldMatchMathLog(double input)
    {
        Float256 actual = Float256.Log((Float256)input);
        double expected = Math.Log(input);
        AssertCloseToDouble(expected, actual, relativeTolerance: 1e-14);
    }

    [Fact(DisplayName = "Float256.Log2 of one should return zero")]
    public void Float256Log2OfOneShouldReturnZero()
    {
        Assert.Equal(Float256.Zero, Float256.Log2(Float256.One));
    }

    [Fact(DisplayName = "Float256.Log2 of an exact power of two should return the exact integer")]
    public void Float256Log2OfExactPowerOfTwoShouldReturnExactInteger()
    {
        Assert.Equal(Float256.One, Float256.Log2(Float256.Two));
        Assert.Equal((Float256)10, Float256.Log2((Float256)1024));
        Assert.Equal(Float256.NegativeOne, Float256.Log2((Float256)0.5));
    }

    [Fact(DisplayName = "Float256.Log10 of one should return zero")]
    public void Float256Log10OfOneShouldReturnZero()
    {
        Assert.Equal(Float256.Zero, Float256.Log10(Float256.One));
    }

    [Fact(DisplayName = "Float256.Log10 of ten should return one")]
    public void Float256Log10OfTenShouldReturnOne()
    {
        Float256 result = Float256.Log10(Float256.Ten);
        AssertCloseToReference(Float256.One, result, ulpTolerance: 16);
    }

    [Fact(DisplayName = "Float256.Log10 of one hundred should return two")]
    public void Float256Log10OfOneHundredShouldReturnTwo()
    {
        Float256 result = Float256.Log10((Float256)100);
        AssertCloseToReference(Float256.Two, result, ulpTolerance: 16);
    }

    [Fact(DisplayName = "Float256.LogP1 of zero should return zero with preserved sign")]
    public void Float256LogP1OfZeroShouldPreserveSign()
    {
        Assert.Equal(Float256.Zero.Bits.Upper, Float256.LogP1(Float256.Zero).Bits.Upper);
        Assert.Equal(Float256.Zero.Bits.Lower, Float256.LogP1(Float256.Zero).Bits.Lower);
        Assert.Equal(Float256.NegativeZero.Bits.Upper, Float256.LogP1(Float256.NegativeZero).Bits.Upper);
        Assert.Equal(Float256.NegativeZero.Bits.Lower, Float256.LogP1(Float256.NegativeZero).Bits.Lower);
    }

    [Fact(DisplayName = "Float256.LogP1 of negative one should return negative infinity")]
    public void Float256LogP1OfNegativeOneShouldReturnNegativeInfinity()
    {
        Assert.True(Float256.IsNegativeInfinity(Float256.LogP1(Float256.NegativeOne)));
    }

    [Fact(DisplayName = "Float256.LogP1 of less than negative one should return NaN")]
    public void Float256LogP1OfLessThanNegativeOneShouldReturnNaN()
    {
        Assert.True(Float256.IsNaN(Float256.LogP1((Float256)(-2))));
    }

    [Fact(DisplayName = "Float256.LogP1 should preserve precision for small inputs")]
    public void Float256LogP1ShouldPreservePrecisionForSmallInputs()
    {
        Float256 tiny = Float256.Parse("1E-65");
        Float256 result = Float256.LogP1(tiny);
        Assert.True(result > Float256.Zero);
        Float256 ratio = result / tiny;
        Assert.True(Float256.Abs(ratio - Float256.One) < Float256.Parse("1E-60"));
    }

    [Fact(DisplayName = "Float256.Log with base should compute change-of-base correctly")]
    public void Float256LogWithBaseShouldComputeChangeOfBase()
    {
        Float256 result = Float256.Log((Float256)100, Float256.Ten);
        AssertCloseToReference(Float256.Two, result, ulpTolerance: 16);
    }

    [Fact(DisplayName = "Float256.Log with base one should return NaN")]
    public void Float256LogWithBaseOneShouldReturnNaN()
    {
        Assert.True(Float256.IsNaN(Float256.Log(Float256.Two, Float256.One)));
    }

    [Theory(DisplayName = "Float256.Log of Float256.Exp should be approximately identity")]
    [InlineData(0.5)]
    [InlineData(1.0)]
    [InlineData(2.5)]
    [InlineData(-3.0)]
    [InlineData(100.0)]
    public void Float256LogOfExpShouldBeIdentity(double input)
    {
        Float256 source = (Float256)input;
        Float256 result = Float256.Log(Float256.Exp(source));
        AssertCloseToReference(source, result, ulpTolerance: 256);
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
