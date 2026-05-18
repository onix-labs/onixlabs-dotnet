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

public sealed class Float128ArithmeticSubtractionTests
{
    [Fact(DisplayName = "Float128.Subtract NaN should propagate NaN")]
    public void Float128SubtractNaNShouldPropagate()
    {
        Assert.True(Float128.IsNaN(Float128.NaN - Float128.One));
        Assert.True(Float128.IsNaN(Float128.One - Float128.NaN));
    }

    [Fact(DisplayName = "Float128.Subtract positive infinity from positive infinity should return NaN")]
    public void Float128SubtractInfinitiesShouldReturnNaN()
    {
        Assert.True(Float128.IsNaN(Float128.PositiveInfinity - Float128.PositiveInfinity));
        Assert.True(Float128.IsNaN(Float128.NegativeInfinity - Float128.NegativeInfinity));
    }

    [Fact(DisplayName = "Float128.Subtract opposite-signed infinities should return signed infinity")]
    public void Float128SubtractOppositeInfinitiesShouldReturnSignedInfinity()
    {
        Assert.True(Float128.IsPositiveInfinity(Float128.PositiveInfinity - Float128.NegativeInfinity));
        Assert.True(Float128.IsNegativeInfinity(Float128.NegativeInfinity - Float128.PositiveInfinity));
    }

    [Theory(DisplayName = "Float128.Subtract should match double subtraction for exact double values")]
    [InlineData(2.0, 1.0, 1.0)]
    [InlineData(1.0, 1.0, 0.0)]
    [InlineData(3.0, 1.0, 2.0)]
    [InlineData(1.0, -1.0, 2.0)]
    [InlineData(-1.0, 1.0, -2.0)]
    [InlineData(100.5, 0.5, 100.0)]
    [InlineData(1e100, 1.0, 1e100)]
    [InlineData(1.5, 0.5, 1.0)]
    public void Float128SubtractShouldMatchDoubleSubtractionForExactValues(double left, double right, double expected)
    {
        Float128 actual = (Float128)left - (Float128)right;
        Float128 expectedFloat = expected;
        Assert.Equal(expectedFloat.RawBits, actual.RawBits);
    }

    [Fact(DisplayName = "Float128.Subtract of value from itself should produce positive zero")]
    public void Float128SubtractValueFromItselfShouldProducePositiveZero()
    {
        Float128 value = 3.14;
        Float128 result = value - value;
        Assert.Equal(Float128.Zero.RawBits, result.RawBits);
    }

    [Fact(DisplayName = "Float128.Subtract zero from a value should return the value")]
    public void Float128SubtractZeroShouldReturnValue()
    {
        Float128 value = 7.5;
        Assert.Equal(value.RawBits, (value - Float128.Zero).RawBits);
        Assert.Equal(value.RawBits, (value - Float128.NegativeZero).RawBits);
    }

    [Fact(DisplayName = "Float128 increment should add one")]
    public void Float128IncrementShouldAddOne()
    {
        Float128 value = 5.0;
        value++;
        Assert.Equal(((Float128)6.0).RawBits, value.RawBits);
    }

    [Fact(DisplayName = "Float128 decrement should subtract one")]
    public void Float128DecrementShouldSubtractOne()
    {
        Float128 value = 5.0;
        value--;
        Assert.Equal(((Float128)4.0).RawBits, value.RawBits);
    }
}
