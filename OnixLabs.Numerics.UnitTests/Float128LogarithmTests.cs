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

public sealed class Float128LogarithmTests
{
    [Fact(DisplayName = "Float128.Log of NaN should return NaN")]
    public void Float128LogOfNaNShouldReturnNaN()
    {
        Assert.True(Float128.IsNaN(Float128.Log(Float128.NaN)));
    }

    [Fact(DisplayName = "Float128.Log of zero should return negative infinity")]
    public void Float128LogOfZeroShouldReturnNegativeInfinity()
    {
        Assert.True(Float128.IsNegativeInfinity(Float128.Log(Float128.Zero)));
        Assert.True(Float128.IsNegativeInfinity(Float128.Log(Float128.NegativeZero)));
    }

    [Fact(DisplayName = "Float128.Log of negative finite should return NaN")]
    public void Float128LogOfNegativeFiniteShouldReturnNaN()
    {
        Assert.True(Float128.IsNaN(Float128.Log(Float128.NegativeOne)));
        Assert.True(Float128.IsNaN(Float128.Log(Float128.MinValue)));
    }

    [Fact(DisplayName = "Float128.Log of negative infinity should return NaN")]
    public void Float128LogOfNegativeInfinityShouldReturnNaN()
    {
        Assert.True(Float128.IsNaN(Float128.Log(Float128.NegativeInfinity)));
    }

    [Fact(DisplayName = "Float128.Log of positive infinity should return positive infinity")]
    public void Float128LogOfPositiveInfinityShouldReturnPositiveInfinity()
    {
        Assert.True(Float128.IsPositiveInfinity(Float128.Log(Float128.PositiveInfinity)));
    }

    [Fact(DisplayName = "Float128.Log of one should return zero")]
    public void Float128LogOfOneShouldReturnZero()
    {
        Assert.Equal(Float128.Zero, Float128.Log(Float128.One));
    }

    [Fact(DisplayName = "Float128.Log of E should return one")]
    public void Float128LogOfEShouldReturnOne()
    {
        Float128 result = Float128.Log(Float128.E);
        AssertCloseToReference(Float128.One, result, ulpTolerance: 16);
    }

    [Fact(DisplayName = "Float128.Log of two should return Ln2")]
    public void Float128LogOfTwoShouldReturnLn2()
    {
        Float128 expected = Float128.Parse("0.6931471805599453094172321214581765680755");
        Float128 result = Float128.Log(Float128.Two);
        AssertCloseToReference(expected, result, ulpTolerance: 16);
    }

    [Fact(DisplayName = "Float128.Log of ten should return Ln10")]
    public void Float128LogOfTenShouldReturnLn10()
    {
        Float128 expected = Float128.Parse("2.3025850929940456840179914546843642076011");
        Float128 result = Float128.Log(Float128.Ten);
        AssertCloseToReference(expected, result, ulpTolerance: 16);
    }

    [Theory(DisplayName = "Float128.Log should match Math.Log within double precision")]
    [InlineData(0.5)]
    [InlineData(1.5)]
    [InlineData(2.0)]
    [InlineData(7.0)]
    [InlineData(100.0)]
    [InlineData(1e20)]
    public void Float128LogShouldMatchMathLog(double input)
    {
        Float128 actual = Float128.Log((Float128)input);
        double expected = Math.Log(input);
        AssertCloseToDouble(expected, actual, relativeTolerance: 1e-14);
    }

    [Fact(DisplayName = "Float128.Log2 of one should return zero")]
    public void Float128Log2OfOneShouldReturnZero()
    {
        Assert.Equal(Float128.Zero, Float128.Log2(Float128.One));
    }

    [Fact(DisplayName = "Float128.Log2 of an exact power of two should return the exact integer")]
    public void Float128Log2OfExactPowerOfTwoShouldReturnExactInteger()
    {
        Assert.Equal(Float128.One, Float128.Log2(Float128.Two));
        Assert.Equal((Float128)10, Float128.Log2((Float128)1024));
        Assert.Equal(Float128.NegativeOne, Float128.Log2((Float128)0.5));
    }

    [Fact(DisplayName = "Float128.Log10 of one should return zero")]
    public void Float128Log10OfOneShouldReturnZero()
    {
        Assert.Equal(Float128.Zero, Float128.Log10(Float128.One));
    }

    [Fact(DisplayName = "Float128.Log10 of ten should return one")]
    public void Float128Log10OfTenShouldReturnOne()
    {
        Float128 result = Float128.Log10(Float128.Ten);
        AssertCloseToReference(Float128.One, result, ulpTolerance: 16);
    }

    [Fact(DisplayName = "Float128.Log10 of one hundred should return two")]
    public void Float128Log10OfOneHundredShouldReturnTwo()
    {
        Float128 result = Float128.Log10((Float128)100);
        AssertCloseToReference(Float128.Two, result, ulpTolerance: 16);
    }

    [Fact(DisplayName = "Float128.LogP1 of zero should return zero with preserved sign")]
    public void Float128LogP1OfZeroShouldPreserveSign()
    {
        Assert.Equal(Float128.Zero.Bits, Float128.LogP1(Float128.Zero).Bits);
        Assert.Equal(Float128.NegativeZero.Bits, Float128.LogP1(Float128.NegativeZero).Bits);
    }

    [Fact(DisplayName = "Float128.LogP1 of negative one should return negative infinity")]
    public void Float128LogP1OfNegativeOneShouldReturnNegativeInfinity()
    {
        Assert.True(Float128.IsNegativeInfinity(Float128.LogP1(Float128.NegativeOne)));
    }

    [Fact(DisplayName = "Float128.LogP1 of less than negative one should return NaN")]
    public void Float128LogP1OfLessThanNegativeOneShouldReturnNaN()
    {
        Assert.True(Float128.IsNaN(Float128.LogP1((Float128)(-2))));
    }

    [Fact(DisplayName = "Float128.LogP1 should preserve precision for small inputs")]
    public void Float128LogP1ShouldPreservePrecisionForSmallInputs()
    {
        Float128 tiny = Float128.Parse("1E-30");
        Float128 result = Float128.LogP1(tiny);
        Assert.True(result > Float128.Zero);
        Float128 ratio = result / tiny;
        Assert.True(Float128.Abs(ratio - Float128.One) < Float128.Parse("1E-25"));
    }

    [Fact(DisplayName = "Float128.Log with base should compute change-of-base correctly")]
    public void Float128LogWithBaseShouldComputeChangeOfBase()
    {
        Float128 result = Float128.Log((Float128)100, Float128.Ten);
        AssertCloseToReference(Float128.Two, result, ulpTolerance: 16);
    }

    [Fact(DisplayName = "Float128.Log with base one should return NaN")]
    public void Float128LogWithBaseOneShouldReturnNaN()
    {
        Assert.True(Float128.IsNaN(Float128.Log(Float128.Two, Float128.One)));
    }

    [Theory(DisplayName = "Float128.Log of Float128.Exp should be approximately identity")]
    [InlineData(0.5)]
    [InlineData(1.0)]
    [InlineData(2.5)]
    [InlineData(-3.0)]
    [InlineData(100.0)]
    public void Float128LogOfExpShouldBeIdentity(double input)
    {
        Float128 source = (Float128)input;
        Float128 result = Float128.Log(Float128.Exp(source));
        AssertCloseToReference(source, result, ulpTolerance: 256);
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
