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

namespace OnixLabs.Numerics.UnitTests;

public sealed class Float128ArithmeticDivisionTests
{
    [Fact(DisplayName = "Float128.Divide NaN should propagate")]
    public void Float128DivideNaNShouldPropagate()
    {
        Assert.True(Float128.IsNaN(Float128.NaN / Float128.One));
        Assert.True(Float128.IsNaN(Float128.One / Float128.NaN));
    }

    [Fact(DisplayName = "Float128.Divide infinity by infinity should return NaN")]
    public void Float128DivideInfinityByInfinityShouldReturnNaN()
    {
        Assert.True(Float128.IsNaN(Float128.PositiveInfinity / Float128.PositiveInfinity));
        Assert.True(Float128.IsNaN(Float128.NegativeInfinity / Float128.PositiveInfinity));
    }

    [Fact(DisplayName = "Float128.Divide zero by zero should return NaN")]
    public void Float128DivideZeroByZeroShouldReturnNaN()
    {
        Assert.True(Float128.IsNaN(Float128.Zero / Float128.Zero));
        Assert.True(Float128.IsNaN(Float128.NegativeZero / Float128.Zero));
    }

    [Fact(DisplayName = "Float128.Divide non-zero by zero should return signed infinity")]
    public void Float128DivideNonZeroByZeroShouldReturnSignedInfinity()
    {
        Assert.True(Float128.IsPositiveInfinity(Float128.One / Float128.Zero));
        Assert.True(Float128.IsNegativeInfinity(Float128.NegativeOne / Float128.Zero));
        Assert.True(Float128.IsNegativeInfinity(Float128.One / Float128.NegativeZero));
        Assert.True(Float128.IsPositiveInfinity(Float128.NegativeOne / Float128.NegativeZero));
    }

    [Fact(DisplayName = "Float128.Divide infinity by finite non-zero should return signed infinity")]
    public void Float128DivideInfinityByFiniteShouldReturnSignedInfinity()
    {
        Assert.True(Float128.IsPositiveInfinity(Float128.PositiveInfinity / Float128.One));
        Assert.True(Float128.IsNegativeInfinity(Float128.PositiveInfinity / Float128.NegativeOne));
    }

    [Fact(DisplayName = "Float128.Divide finite by infinity should return signed zero")]
    public void Float128DivideFiniteByInfinityShouldReturnSignedZero()
    {
        Assert.Equal(Float128.Zero.Bits, (Float128.One / Float128.PositiveInfinity).Bits);
        Assert.Equal(Float128.NegativeZero.Bits, (Float128.NegativeOne / Float128.PositiveInfinity).Bits);
    }

    [Theory(DisplayName = "Float128.Divide should match double division for exact double values")]
    [InlineData(1.0, 1.0, 1.0)]
    [InlineData(6.0, 2.0, 3.0)]
    [InlineData(10.0, 4.0, 2.5)]
    [InlineData(1.0, 2.0, 0.5)]
    [InlineData(1.0, 4.0, 0.25)]
    [InlineData(-6.0, 2.0, -3.0)]
    [InlineData(-6.0, -2.0, 3.0)]
    [InlineData(100.0, 10.0, 10.0)]
    [InlineData(1.0, 8.0, 0.125)]
    public void Float128DivideShouldMatchDoubleDivisionForExactValues(double left, double right, double expected)
    {
        Float128 actual = (Float128)left / (Float128)right;
        Float128 expectedFloat = expected;
        Assert.Equal(expectedFloat.Bits, actual.Bits);
    }

    [Fact(DisplayName = "Float128.Divide by self should return one")]
    public void Float128DivideBySelfShouldReturnOne()
    {
        Float128 value = 7.5;
        Float128 result = value / value;
        Assert.Equal(Float128.One.Bits, result.Bits);
    }

    [Fact(DisplayName = "Float128.Divide by one should return the value")]
    public void Float128DivideByOneShouldReturnValue()
    {
        Float128 value = 3.14;
        Assert.Equal(value.Bits, (value / Float128.One).Bits);
    }

    [Fact(DisplayName = "Float128.Divide a value by its double should produce 0.5")]
    public void Float128DivideValueByDoubleShouldProduceHalf()
    {
        Float128 value = 5.0;
        Float128 result = value / (value + value);
        Assert.Equal(((Float128)0.5).Bits, result.Bits);
    }
}
