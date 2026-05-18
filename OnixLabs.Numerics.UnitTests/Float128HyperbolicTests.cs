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

public sealed class Float128HyperbolicTests
{
    [Fact(DisplayName = "Float128.Sinh of zero should preserve sign")]
    public void Float128SinhOfZeroShouldPreserveSign()
    {
        Assert.Equal(Float128.Zero.RawBits, Float128.Sinh(Float128.Zero).RawBits);
        Assert.Equal(Float128.NegativeZero.RawBits, Float128.Sinh(Float128.NegativeZero).RawBits);
    }

    [Fact(DisplayName = "Float128.Sinh of infinities should preserve sign")]
    public void Float128SinhOfInfinitiesShouldPreserveSign()
    {
        Assert.True(Float128.IsPositiveInfinity(Float128.Sinh(Float128.PositiveInfinity)));
        Assert.True(Float128.IsNegativeInfinity(Float128.Sinh(Float128.NegativeInfinity)));
    }

    [Fact(DisplayName = "Float128.Sinh of NaN should return NaN")]
    public void Float128SinhOfNaNShouldReturnNaN()
    {
        Assert.True(Float128.IsNaN(Float128.Sinh(Float128.NaN)));
    }

    [Theory(DisplayName = "Float128.Sinh should match Math.Sinh within double precision")]
    [InlineData(0.5)]
    [InlineData(-0.5)]
    [InlineData(1.0)]
    [InlineData(5.0)]
    [InlineData(-5.0)]
    public void Float128SinhShouldMatchMathSinh(double input)
    {
        Float128 actual = Float128.Sinh((Float128)input);
        double expected = Math.Sinh(input);
        double actualDouble = (double)actual;
        double tolerance = Math.Max(Math.Abs(expected) * 1e-14, 1e-14);
        Assert.True(Math.Abs(actualDouble - expected) <= tolerance, $"Expected {expected} but got {actualDouble}");
    }

    [Fact(DisplayName = "Float128.Cosh of zero should return one")]
    public void Float128CoshOfZeroShouldReturnOne()
    {
        Assert.Equal(Float128.One, Float128.Cosh(Float128.Zero));
        Assert.Equal(Float128.One, Float128.Cosh(Float128.NegativeZero));
    }

    [Fact(DisplayName = "Float128.Cosh of infinity should return positive infinity")]
    public void Float128CoshOfInfinityShouldReturnPositiveInfinity()
    {
        Assert.True(Float128.IsPositiveInfinity(Float128.Cosh(Float128.PositiveInfinity)));
        Assert.True(Float128.IsPositiveInfinity(Float128.Cosh(Float128.NegativeInfinity)));
    }

    [Theory(DisplayName = "Float128.Cosh should match Math.Cosh within double precision")]
    [InlineData(0.5)]
    [InlineData(1.0)]
    [InlineData(5.0)]
    public void Float128CoshShouldMatchMathCosh(double input)
    {
        Float128 actual = Float128.Cosh((Float128)input);
        double expected = Math.Cosh(input);
        double actualDouble = (double)actual;
        double tolerance = Math.Max(Math.Abs(expected) * 1e-14, 1e-14);
        Assert.True(Math.Abs(actualDouble - expected) <= tolerance, $"Expected {expected} but got {actualDouble}");
    }

    [Fact(DisplayName = "Float128.Tanh of zero should preserve sign")]
    public void Float128TanhOfZeroShouldPreserveSign()
    {
        Assert.Equal(Float128.Zero.RawBits, Float128.Tanh(Float128.Zero).RawBits);
        Assert.Equal(Float128.NegativeZero.RawBits, Float128.Tanh(Float128.NegativeZero).RawBits);
    }

    [Fact(DisplayName = "Float128.Tanh of infinity should saturate to ±1")]
    public void Float128TanhOfInfinityShouldSaturate()
    {
        Assert.Equal(Float128.One, Float128.Tanh(Float128.PositiveInfinity));
        Assert.Equal(Float128.NegativeOne, Float128.Tanh(Float128.NegativeInfinity));
    }

    [Fact(DisplayName = "Float128.Tanh of large input should saturate to ±1")]
    public void Float128TanhOfLargeInputShouldSaturate()
    {
        Assert.Equal(Float128.One, Float128.Tanh((Float128)100));
        Assert.Equal(Float128.NegativeOne, Float128.Tanh((Float128)(-100)));
    }

    [Theory(DisplayName = "Float128.Tanh should match Math.Tanh within double precision")]
    [InlineData(0.5)]
    [InlineData(1.0)]
    [InlineData(5.0)]
    public void Float128TanhShouldMatchMathTanh(double input)
    {
        Float128 actual = Float128.Tanh((Float128)input);
        double expected = Math.Tanh(input);
        double actualDouble = (double)actual;
        double tolerance = Math.Max(Math.Abs(expected) * 1e-14, 1e-14);
        Assert.True(Math.Abs(actualDouble - expected) <= tolerance, $"Expected {expected} but got {actualDouble}");
    }

    [Fact(DisplayName = "Float128 hyperbolic identity should hold (Cosh² - Sinh² = 1)")]
    public void Float128HyperbolicIdentityShouldHold()
    {
        Float128 x = (Float128)1.5;
        Float128 sinh = Float128.Sinh(x);
        Float128 cosh = Float128.Cosh(x);
        Float128 identity = cosh * cosh - sinh * sinh;
        AssertCloseToReference(Float128.One, identity, ulpTolerance: 64);
    }

    [Fact(DisplayName = "Float128.Asinh of zero should preserve sign")]
    public void Float128AsinhOfZeroShouldPreserveSign()
    {
        Assert.Equal(Float128.Zero.RawBits, Float128.Asinh(Float128.Zero).RawBits);
        Assert.Equal(Float128.NegativeZero.RawBits, Float128.Asinh(Float128.NegativeZero).RawBits);
    }

    [Fact(DisplayName = "Float128.Asinh of infinities should preserve sign")]
    public void Float128AsinhOfInfinitiesShouldPreserveSign()
    {
        Assert.True(Float128.IsPositiveInfinity(Float128.Asinh(Float128.PositiveInfinity)));
        Assert.True(Float128.IsNegativeInfinity(Float128.Asinh(Float128.NegativeInfinity)));
    }

    [Theory(DisplayName = "Float128.Asinh should match Math.Asinh within double precision")]
    [InlineData(0.5)]
    [InlineData(-0.5)]
    [InlineData(1.0)]
    [InlineData(10.0)]
    public void Float128AsinhShouldMatchMathAsinh(double input)
    {
        Float128 actual = Float128.Asinh((Float128)input);
        double expected = Math.Asinh(input);
        double actualDouble = (double)actual;
        double tolerance = Math.Max(Math.Abs(expected) * 1e-14, 1e-14);
        Assert.True(Math.Abs(actualDouble - expected) <= tolerance, $"Expected {expected} but got {actualDouble}");
    }

    [Fact(DisplayName = "Float128.Acosh of one should return zero")]
    public void Float128AcoshOfOneShouldReturnZero()
    {
        Assert.Equal(Float128.Zero, Float128.Acosh(Float128.One));
    }

    [Fact(DisplayName = "Float128.Acosh of less than one should return NaN")]
    public void Float128AcoshOfLessThanOneShouldReturnNaN()
    {
        Assert.True(Float128.IsNaN(Float128.Acosh((Float128)0.5)));
        Assert.True(Float128.IsNaN(Float128.Acosh(Float128.NegativeOne)));
    }

    [Theory(DisplayName = "Float128.Acosh should match Math.Acosh within double precision")]
    [InlineData(1.5)]
    [InlineData(2.0)]
    [InlineData(10.0)]
    public void Float128AcoshShouldMatchMathAcosh(double input)
    {
        Float128 actual = Float128.Acosh((Float128)input);
        double expected = Math.Acosh(input);
        double actualDouble = (double)actual;
        double tolerance = Math.Max(Math.Abs(expected) * 1e-14, 1e-14);
        Assert.True(Math.Abs(actualDouble - expected) <= tolerance, $"Expected {expected} but got {actualDouble}");
    }

    [Fact(DisplayName = "Float128.Atanh of zero should preserve sign")]
    public void Float128AtanhOfZeroShouldPreserveSign()
    {
        Assert.Equal(Float128.Zero.RawBits, Float128.Atanh(Float128.Zero).RawBits);
        Assert.Equal(Float128.NegativeZero.RawBits, Float128.Atanh(Float128.NegativeZero).RawBits);
    }

    [Fact(DisplayName = "Float128.Atanh of ±1 should return ±infinity")]
    public void Float128AtanhOfOneShouldReturnInfinity()
    {
        Assert.True(Float128.IsPositiveInfinity(Float128.Atanh(Float128.One)));
        Assert.True(Float128.IsNegativeInfinity(Float128.Atanh(Float128.NegativeOne)));
    }

    [Fact(DisplayName = "Float128.Atanh of out-of-range should return NaN")]
    public void Float128AtanhOfOutOfRangeShouldReturnNaN()
    {
        Assert.True(Float128.IsNaN(Float128.Atanh((Float128)1.5)));
    }

    [Theory(DisplayName = "Float128.Atanh should match Math.Atanh within double precision")]
    [InlineData(0.5)]
    [InlineData(-0.5)]
    [InlineData(0.9)]
    public void Float128AtanhShouldMatchMathAtanh(double input)
    {
        Float128 actual = Float128.Atanh((Float128)input);
        double expected = Math.Atanh(input);
        double actualDouble = (double)actual;
        double tolerance = Math.Max(Math.Abs(expected) * 1e-14, 1e-14);
        Assert.True(Math.Abs(actualDouble - expected) <= tolerance, $"Expected {expected} but got {actualDouble}");
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
