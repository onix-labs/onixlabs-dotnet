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

public sealed class Float256HyperbolicTests
{
    [Fact(DisplayName = "Float256.Sinh of zero should preserve sign")]
    public void Float256SinhOfZeroShouldPreserveSign()
    {
        Assert.Equal(Float256.Zero.Bits.UpperBits, Float256.Sinh(Float256.Zero).Bits.UpperBits);
        Assert.Equal(Float256.Zero.Bits.LowerBits, Float256.Sinh(Float256.Zero).Bits.LowerBits);
        Assert.Equal(Float256.NegativeZero.Bits.UpperBits, Float256.Sinh(Float256.NegativeZero).Bits.UpperBits);
        Assert.Equal(Float256.NegativeZero.Bits.LowerBits, Float256.Sinh(Float256.NegativeZero).Bits.LowerBits);
    }

    [Fact(DisplayName = "Float256.Sinh of infinities should preserve sign")]
    public void Float256SinhOfInfinitiesShouldPreserveSign()
    {
        Assert.True(Float256.IsPositiveInfinity(Float256.Sinh(Float256.PositiveInfinity)));
        Assert.True(Float256.IsNegativeInfinity(Float256.Sinh(Float256.NegativeInfinity)));
    }

    [Fact(DisplayName = "Float256.Sinh of NaN should return NaN")]
    public void Float256SinhOfNaNShouldReturnNaN()
    {
        Assert.True(Float256.IsNaN(Float256.Sinh(Float256.NaN)));
    }

    [Theory(DisplayName = "Float256.Sinh should match Math.Sinh within double precision")]
    [InlineData(0.5)]
    [InlineData(-0.5)]
    [InlineData(1.0)]
    [InlineData(5.0)]
    [InlineData(-5.0)]
    public void Float256SinhShouldMatchMathSinh(double input)
    {
        Float256 actual = Float256.Sinh((Float256)input);
        double expected = Math.Sinh(input);
        double actualDouble = (double)actual;
        double tolerance = Math.Max(Math.Abs(expected) * 1e-14, 1e-14);
        Assert.True(Math.Abs(actualDouble - expected) <= tolerance, $"Expected {expected} but got {actualDouble}");
    }

    [Fact(DisplayName = "Float256.Cosh of zero should return one")]
    public void Float256CoshOfZeroShouldReturnOne()
    {
        Assert.Equal(Float256.One, Float256.Cosh(Float256.Zero));
        Assert.Equal(Float256.One, Float256.Cosh(Float256.NegativeZero));
    }

    [Fact(DisplayName = "Float256.Cosh of infinity should return positive infinity")]
    public void Float256CoshOfInfinityShouldReturnPositiveInfinity()
    {
        Assert.True(Float256.IsPositiveInfinity(Float256.Cosh(Float256.PositiveInfinity)));
        Assert.True(Float256.IsPositiveInfinity(Float256.Cosh(Float256.NegativeInfinity)));
    }

    [Theory(DisplayName = "Float256.Cosh should match Math.Cosh within double precision")]
    [InlineData(0.5)]
    [InlineData(1.0)]
    [InlineData(5.0)]
    public void Float256CoshShouldMatchMathCosh(double input)
    {
        Float256 actual = Float256.Cosh((Float256)input);
        double expected = Math.Cosh(input);
        double actualDouble = (double)actual;
        double tolerance = Math.Max(Math.Abs(expected) * 1e-14, 1e-14);
        Assert.True(Math.Abs(actualDouble - expected) <= tolerance, $"Expected {expected} but got {actualDouble}");
    }

    [Fact(DisplayName = "Float256.Tanh of zero should preserve sign")]
    public void Float256TanhOfZeroShouldPreserveSign()
    {
        Assert.Equal(Float256.Zero.Bits.UpperBits, Float256.Tanh(Float256.Zero).Bits.UpperBits);
        Assert.Equal(Float256.Zero.Bits.LowerBits, Float256.Tanh(Float256.Zero).Bits.LowerBits);
        Assert.Equal(Float256.NegativeZero.Bits.UpperBits, Float256.Tanh(Float256.NegativeZero).Bits.UpperBits);
        Assert.Equal(Float256.NegativeZero.Bits.LowerBits, Float256.Tanh(Float256.NegativeZero).Bits.LowerBits);
    }

    [Fact(DisplayName = "Float256.Tanh of infinity should saturate to ±1")]
    public void Float256TanhOfInfinityShouldSaturate()
    {
        Assert.Equal(Float256.One, Float256.Tanh(Float256.PositiveInfinity));
        Assert.Equal(Float256.NegativeOne, Float256.Tanh(Float256.NegativeInfinity));
    }

    [Fact(DisplayName = "Float256.Tanh of large input should saturate to ±1")]
    public void Float256TanhOfLargeInputShouldSaturate()
    {
        Assert.Equal(Float256.One, Float256.Tanh((Float256)100));
        Assert.Equal(Float256.NegativeOne, Float256.Tanh((Float256)(-100)));
    }

    [Theory(DisplayName = "Float256.Tanh should match Math.Tanh within double precision")]
    [InlineData(0.5)]
    [InlineData(1.0)]
    [InlineData(5.0)]
    public void Float256TanhShouldMatchMathTanh(double input)
    {
        Float256 actual = Float256.Tanh((Float256)input);
        double expected = Math.Tanh(input);
        double actualDouble = (double)actual;
        double tolerance = Math.Max(Math.Abs(expected) * 1e-14, 1e-14);
        Assert.True(Math.Abs(actualDouble - expected) <= tolerance, $"Expected {expected} but got {actualDouble}");
    }

    [Fact(DisplayName = "Float256 hyperbolic identity should hold (Cosh² - Sinh² = 1)")]
    public void Float256HyperbolicIdentityShouldHold()
    {
        Float256 x = (Float256)1.5;
        Float256 sinh = Float256.Sinh(x);
        Float256 cosh = Float256.Cosh(x);
        Float256 identity = cosh * cosh - sinh * sinh;
        AssertCloseToReference(Float256.One, identity, ulpTolerance: 64);
    }

    [Fact(DisplayName = "Float256.Asinh of zero should preserve sign")]
    public void Float256AsinhOfZeroShouldPreserveSign()
    {
        Assert.Equal(Float256.Zero.Bits.UpperBits, Float256.Asinh(Float256.Zero).Bits.UpperBits);
        Assert.Equal(Float256.Zero.Bits.LowerBits, Float256.Asinh(Float256.Zero).Bits.LowerBits);
        Assert.Equal(Float256.NegativeZero.Bits.UpperBits, Float256.Asinh(Float256.NegativeZero).Bits.UpperBits);
        Assert.Equal(Float256.NegativeZero.Bits.LowerBits, Float256.Asinh(Float256.NegativeZero).Bits.LowerBits);
    }

    [Fact(DisplayName = "Float256.Asinh of infinities should preserve sign")]
    public void Float256AsinhOfInfinitiesShouldPreserveSign()
    {
        Assert.True(Float256.IsPositiveInfinity(Float256.Asinh(Float256.PositiveInfinity)));
        Assert.True(Float256.IsNegativeInfinity(Float256.Asinh(Float256.NegativeInfinity)));
    }

    [Theory(DisplayName = "Float256.Asinh should match Math.Asinh within double precision")]
    [InlineData(0.5)]
    [InlineData(-0.5)]
    [InlineData(1.0)]
    [InlineData(10.0)]
    public void Float256AsinhShouldMatchMathAsinh(double input)
    {
        Float256 actual = Float256.Asinh((Float256)input);
        double expected = Math.Asinh(input);
        double actualDouble = (double)actual;
        double tolerance = Math.Max(Math.Abs(expected) * 1e-14, 1e-14);
        Assert.True(Math.Abs(actualDouble - expected) <= tolerance, $"Expected {expected} but got {actualDouble}");
    }

    [Fact(DisplayName = "Float256.Acosh of one should return zero")]
    public void Float256AcoshOfOneShouldReturnZero()
    {
        Assert.Equal(Float256.Zero, Float256.Acosh(Float256.One));
    }

    [Fact(DisplayName = "Float256.Acosh of less than one should return NaN")]
    public void Float256AcoshOfLessThanOneShouldReturnNaN()
    {
        Assert.True(Float256.IsNaN(Float256.Acosh((Float256)0.5)));
        Assert.True(Float256.IsNaN(Float256.Acosh(Float256.NegativeOne)));
    }

    [Theory(DisplayName = "Float256.Acosh should match Math.Acosh within double precision")]
    [InlineData(1.5)]
    [InlineData(2.0)]
    [InlineData(10.0)]
    public void Float256AcoshShouldMatchMathAcosh(double input)
    {
        Float256 actual = Float256.Acosh((Float256)input);
        double expected = Math.Acosh(input);
        double actualDouble = (double)actual;
        double tolerance = Math.Max(Math.Abs(expected) * 1e-14, 1e-14);
        Assert.True(Math.Abs(actualDouble - expected) <= tolerance, $"Expected {expected} but got {actualDouble}");
    }

    [Fact(DisplayName = "Float256.Atanh of zero should preserve sign")]
    public void Float256AtanhOfZeroShouldPreserveSign()
    {
        Assert.Equal(Float256.Zero.Bits.UpperBits, Float256.Atanh(Float256.Zero).Bits.UpperBits);
        Assert.Equal(Float256.Zero.Bits.LowerBits, Float256.Atanh(Float256.Zero).Bits.LowerBits);
        Assert.Equal(Float256.NegativeZero.Bits.UpperBits, Float256.Atanh(Float256.NegativeZero).Bits.UpperBits);
        Assert.Equal(Float256.NegativeZero.Bits.LowerBits, Float256.Atanh(Float256.NegativeZero).Bits.LowerBits);
    }

    [Fact(DisplayName = "Float256.Atanh of ±1 should return ±infinity")]
    public void Float256AtanhOfOneShouldReturnInfinity()
    {
        Assert.True(Float256.IsPositiveInfinity(Float256.Atanh(Float256.One)));
        Assert.True(Float256.IsNegativeInfinity(Float256.Atanh(Float256.NegativeOne)));
    }

    [Fact(DisplayName = "Float256.Atanh of out-of-range should return NaN")]
    public void Float256AtanhOfOutOfRangeShouldReturnNaN()
    {
        Assert.True(Float256.IsNaN(Float256.Atanh((Float256)1.5)));
    }

    [Theory(DisplayName = "Float256.Atanh should match Math.Atanh within double precision")]
    [InlineData(0.5)]
    [InlineData(-0.5)]
    [InlineData(0.9)]
    public void Float256AtanhShouldMatchMathAtanh(double input)
    {
        Float256 actual = Float256.Atanh((Float256)input);
        double expected = Math.Atanh(input);
        double actualDouble = (double)actual;
        double tolerance = Math.Max(Math.Abs(expected) * 1e-14, 1e-14);
        Assert.True(Math.Abs(actualDouble - expected) <= tolerance, $"Expected {expected} but got {actualDouble}");
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
