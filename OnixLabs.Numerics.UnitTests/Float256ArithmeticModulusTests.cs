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

public sealed class Float256ArithmeticModulusTests
{
    [Fact(DisplayName = "Float256.Remainder NaN should propagate")]
    public void Float256RemainderNaNShouldPropagate()
    {
        Assert.True(Float256.IsNaN(Float256.NaN % Float256.One));
        Assert.True(Float256.IsNaN(Float256.One % Float256.NaN));
    }

    [Fact(DisplayName = "Float256.Remainder of infinity should return NaN")]
    public void Float256RemainderOfInfinityShouldReturnNaN()
    {
        Assert.True(Float256.IsNaN(Float256.PositiveInfinity % Float256.One));
        Assert.True(Float256.IsNaN(Float256.NegativeInfinity % Float256.One));
    }

    [Fact(DisplayName = "Float256.Remainder by zero should return NaN")]
    public void Float256RemainderByZeroShouldReturnNaN()
    {
        Assert.True(Float256.IsNaN(Float256.One % Float256.Zero));
        Assert.True(Float256.IsNaN(Float256.Two % Float256.NegativeZero));
    }

    [Fact(DisplayName = "Float256.Remainder of finite by infinity should return the finite value")]
    public void Float256RemainderByInfinityShouldReturnValue()
    {
        Assert.Equal(Float256.One.RawHighBits, (Float256.One % Float256.PositiveInfinity).RawHighBits);
        Assert.Equal(Float256.One.RawLowBits, (Float256.One % Float256.PositiveInfinity).RawLowBits);
        Assert.Equal(Float256.NegativeOne.RawHighBits, (Float256.NegativeOne % Float256.PositiveInfinity).RawHighBits);
        Assert.Equal(Float256.NegativeOne.RawLowBits, (Float256.NegativeOne % Float256.PositiveInfinity).RawLowBits);
    }

    [Theory(DisplayName = "Float256.Remainder should match double remainder for exact double values")]
    [InlineData(7.0, 3.0, 1.0)]
    [InlineData(-7.0, 3.0, -1.0)]
    [InlineData(7.0, -3.0, 1.0)]
    [InlineData(-7.0, -3.0, -1.0)]
    [InlineData(10.0, 4.0, 2.0)]
    [InlineData(1.0, 1.0, 0.0)]
    [InlineData(5.5, 2.0, 1.5)]
    public void Float256RemainderShouldMatchDoubleRemainderForExactValues(double left, double right, double expected)
    {
        Float256 actual = (Float256)left % (Float256)right;
        Float256 expectedFloat = expected;
        Assert.Equal(expectedFloat.RawHighBits, actual.RawHighBits);
        Assert.Equal(expectedFloat.RawLowBits, actual.RawLowBits);
    }
}
