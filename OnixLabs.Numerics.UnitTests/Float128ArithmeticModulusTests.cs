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

public sealed class Float128ArithmeticModulusTests
{
    [Fact(DisplayName = "Float128.Remainder NaN should propagate")]
    public void Float128RemainderNaNShouldPropagate()
    {
        Assert.True(Float128.IsNaN(Float128.NaN % Float128.One));
        Assert.True(Float128.IsNaN(Float128.One % Float128.NaN));
    }

    [Fact(DisplayName = "Float128.Remainder of infinity should return NaN")]
    public void Float128RemainderOfInfinityShouldReturnNaN()
    {
        Assert.True(Float128.IsNaN(Float128.PositiveInfinity % Float128.One));
        Assert.True(Float128.IsNaN(Float128.NegativeInfinity % Float128.One));
    }

    [Fact(DisplayName = "Float128.Remainder by zero should return NaN")]
    public void Float128RemainderByZeroShouldReturnNaN()
    {
        Assert.True(Float128.IsNaN(Float128.One % Float128.Zero));
        Assert.True(Float128.IsNaN(Float128.Two % Float128.NegativeZero));
    }

    [Fact(DisplayName = "Float128.Remainder of finite by infinity should return the finite value")]
    public void Float128RemainderByInfinityShouldReturnValue()
    {
        Assert.Equal(Float128.One.Bits, (Float128.One % Float128.PositiveInfinity).Bits);
        Assert.Equal(Float128.NegativeOne.Bits, (Float128.NegativeOne % Float128.PositiveInfinity).Bits);
    }

    [Theory(DisplayName = "Float128.Remainder should match double remainder for exact double values")]
    [InlineData(7.0, 3.0, 1.0)]
    [InlineData(-7.0, 3.0, -1.0)]
    [InlineData(7.0, -3.0, 1.0)]
    [InlineData(-7.0, -3.0, -1.0)]
    [InlineData(10.0, 4.0, 2.0)]
    [InlineData(1.0, 1.0, 0.0)]
    [InlineData(5.5, 2.0, 1.5)]
    public void Float128RemainderShouldMatchDoubleRemainderForExactValues(double left, double right, double expected)
    {
        Float128 actual = (Float128)left % (Float128)right;
        Float128 expectedFloat = expected;
        Assert.Equal(expectedFloat.Bits, actual.Bits);
    }
}
