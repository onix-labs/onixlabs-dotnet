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

public sealed class Float256ArithmeticDivisionTests
{
    [Fact(DisplayName = "Float256.Divide NaN should propagate")]
    public void Float256DivideNaNShouldPropagate()
    {
        Assert.True(Float256.IsNaN(Float256.NaN / Float256.One));
        Assert.True(Float256.IsNaN(Float256.One / Float256.NaN));
    }

    [Fact(DisplayName = "Float256.Divide infinity by infinity should return NaN")]
    public void Float256DivideInfinityByInfinityShouldReturnNaN()
    {
        Assert.True(Float256.IsNaN(Float256.PositiveInfinity / Float256.PositiveInfinity));
        Assert.True(Float256.IsNaN(Float256.NegativeInfinity / Float256.PositiveInfinity));
    }

    [Fact(DisplayName = "Float256.Divide zero by zero should return NaN")]
    public void Float256DivideZeroByZeroShouldReturnNaN()
    {
        Assert.True(Float256.IsNaN(Float256.Zero / Float256.Zero));
        Assert.True(Float256.IsNaN(Float256.NegativeZero / Float256.Zero));
    }

    [Fact(DisplayName = "Float256.Divide non-zero by zero should return signed infinity")]
    public void Float256DivideNonZeroByZeroShouldReturnSignedInfinity()
    {
        Assert.True(Float256.IsPositiveInfinity(Float256.One / Float256.Zero));
        Assert.True(Float256.IsNegativeInfinity(Float256.NegativeOne / Float256.Zero));
        Assert.True(Float256.IsNegativeInfinity(Float256.One / Float256.NegativeZero));
        Assert.True(Float256.IsPositiveInfinity(Float256.NegativeOne / Float256.NegativeZero));
    }

    [Fact(DisplayName = "Float256.Divide infinity by finite non-zero should return signed infinity")]
    public void Float256DivideInfinityByFiniteShouldReturnSignedInfinity()
    {
        Assert.True(Float256.IsPositiveInfinity(Float256.PositiveInfinity / Float256.One));
        Assert.True(Float256.IsNegativeInfinity(Float256.PositiveInfinity / Float256.NegativeOne));
    }

    [Fact(DisplayName = "Float256.Divide finite by infinity should return signed zero")]
    public void Float256DivideFiniteByInfinityShouldReturnSignedZero()
    {
        Assert.Equal(Float256.Zero.RawHighBits, (Float256.One / Float256.PositiveInfinity).RawHighBits);
        Assert.Equal(Float256.Zero.RawLowBits, (Float256.One / Float256.PositiveInfinity).RawLowBits);
        Assert.Equal(Float256.NegativeZero.RawHighBits, (Float256.NegativeOne / Float256.PositiveInfinity).RawHighBits);
        Assert.Equal(Float256.NegativeZero.RawLowBits, (Float256.NegativeOne / Float256.PositiveInfinity).RawLowBits);
    }

    [Theory(DisplayName = "Float256.Divide should match double division for exact double values")]
    [InlineData(1.0, 1.0, 1.0)]
    [InlineData(6.0, 2.0, 3.0)]
    [InlineData(10.0, 4.0, 2.5)]
    [InlineData(1.0, 2.0, 0.5)]
    [InlineData(1.0, 4.0, 0.25)]
    [InlineData(-6.0, 2.0, -3.0)]
    [InlineData(-6.0, -2.0, 3.0)]
    [InlineData(100.0, 10.0, 10.0)]
    [InlineData(1.0, 8.0, 0.125)]
    public void Float256DivideShouldMatchDoubleDivisionForExactValues(double left, double right, double expected)
    {
        Float256 actual = (Float256)left / (Float256)right;
        Float256 expectedFloat = expected;
        Assert.Equal(expectedFloat.RawHighBits, actual.RawHighBits);
        Assert.Equal(expectedFloat.RawLowBits, actual.RawLowBits);
    }

    [Fact(DisplayName = "Float256.Divide by self should return one")]
    public void Float256DivideBySelfShouldReturnOne()
    {
        Float256 value = 7.5;
        Float256 result = value / value;
        Assert.Equal(Float256.One.RawHighBits, result.RawHighBits);
        Assert.Equal(Float256.One.RawLowBits, result.RawLowBits);
    }

    [Fact(DisplayName = "Float256.Divide by one should return the value")]
    public void Float256DivideByOneShouldReturnValue()
    {
        Float256 value = 3.14;
        Assert.Equal(value.RawHighBits, (value / Float256.One).RawHighBits);
        Assert.Equal(value.RawLowBits, (value / Float256.One).RawLowBits);
    }

    [Fact(DisplayName = "Float256.Divide a value by its double should produce 0.5")]
    public void Float256DivideValueByDoubleShouldProduceHalf()
    {
        Float256 value = 5.0;
        Float256 result = value / (value + value);
        Assert.Equal(((Float256)0.5).RawHighBits, result.RawHighBits);
        Assert.Equal(((Float256)0.5).RawLowBits, result.RawLowBits);
    }
}
