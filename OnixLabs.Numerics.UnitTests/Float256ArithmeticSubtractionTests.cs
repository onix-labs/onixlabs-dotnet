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

public sealed class Float256ArithmeticSubtractionTests
{
    [Fact(DisplayName = "Float256.Subtract NaN should propagate NaN")]
    public void Float256SubtractNaNShouldPropagate()
    {
        Assert.True(Float256.IsNaN(Float256.NaN - Float256.One));
        Assert.True(Float256.IsNaN(Float256.One - Float256.NaN));
    }

    [Fact(DisplayName = "Float256.Subtract positive infinity from positive infinity should return NaN")]
    public void Float256SubtractInfinitiesShouldReturnNaN()
    {
        Assert.True(Float256.IsNaN(Float256.PositiveInfinity - Float256.PositiveInfinity));
        Assert.True(Float256.IsNaN(Float256.NegativeInfinity - Float256.NegativeInfinity));
    }

    [Fact(DisplayName = "Float256.Subtract opposite-signed infinities should return signed infinity")]
    public void Float256SubtractOppositeInfinitiesShouldReturnSignedInfinity()
    {
        Assert.True(Float256.IsPositiveInfinity(Float256.PositiveInfinity - Float256.NegativeInfinity));
        Assert.True(Float256.IsNegativeInfinity(Float256.NegativeInfinity - Float256.PositiveInfinity));
    }

    [Theory(DisplayName = "Float256.Subtract should match double subtraction for exact double values")]
    [InlineData(2.0, 1.0, 1.0)]
    [InlineData(1.0, 1.0, 0.0)]
    [InlineData(3.0, 1.0, 2.0)]
    [InlineData(1.0, -1.0, 2.0)]
    [InlineData(-1.0, 1.0, -2.0)]
    [InlineData(100.5, 0.5, 100.0)]
    [InlineData(1e100, 1.0, 1e100)]
    [InlineData(1.5, 0.5, 1.0)]
    public void Float256SubtractShouldMatchDoubleSubtractionForExactValues(double left, double right, double expected)
    {
        Float256 actual = (Float256)left - (Float256)right;
        Float256 expectedFloat = expected;
        Assert.Equal(expectedFloat.RawHighBits, actual.RawHighBits);
        Assert.Equal(expectedFloat.RawLowBits, actual.RawLowBits);
    }

    [Fact(DisplayName = "Float256.Subtract of value from itself should produce positive zero")]
    public void Float256SubtractValueFromItselfShouldProducePositiveZero()
    {
        Float256 value = 3.14;
        Float256 result = value - value;
        Assert.Equal(Float256.Zero.RawHighBits, result.RawHighBits);
        Assert.Equal(Float256.Zero.RawLowBits, result.RawLowBits);
    }

    [Fact(DisplayName = "Float256.Subtract zero from a value should return the value")]
    public void Float256SubtractZeroShouldReturnValue()
    {
        Float256 value = 7.5;
        Assert.Equal(value.RawHighBits, (value - Float256.Zero).RawHighBits);
        Assert.Equal(value.RawLowBits, (value - Float256.Zero).RawLowBits);
        Assert.Equal(value.RawHighBits, (value - Float256.NegativeZero).RawHighBits);
        Assert.Equal(value.RawLowBits, (value - Float256.NegativeZero).RawLowBits);
    }

    [Fact(DisplayName = "Float256 increment should add one")]
    public void Float256IncrementShouldAddOne()
    {
        Float256 value = 5.0;
        value++;
        Assert.Equal(((Float256)6.0).RawHighBits, value.RawHighBits);
        Assert.Equal(((Float256)6.0).RawLowBits, value.RawLowBits);
    }

    [Fact(DisplayName = "Float256 decrement should subtract one")]
    public void Float256DecrementShouldSubtractOne()
    {
        Float256 value = 5.0;
        value--;
        Assert.Equal(((Float256)4.0).RawHighBits, value.RawHighBits);
        Assert.Equal(((Float256)4.0).RawLowBits, value.RawLowBits);
    }
}
