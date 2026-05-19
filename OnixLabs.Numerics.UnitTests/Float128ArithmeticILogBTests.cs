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

public sealed class Float128ArithmeticILogBTests
{
    [Theory(DisplayName = "Float128.ILogB should return the unbiased exponent for normal values")]
    [InlineData(1.0, 0)]
    [InlineData(2.0, 1)]
    [InlineData(4.0, 2)]
    [InlineData(8.0, 3)]
    [InlineData(0.5, -1)]
    [InlineData(0.25, -2)]
    [InlineData(-1.0, 0)]
    [InlineData(-2.0, 1)]
    public void Float128ILogBShouldReturnUnbiasedExponentForNormals(double value, int expected)
    {
        Assert.Equal(expected, Float128.ILogB(value));
    }

    [Fact(DisplayName = "Float128.ILogB of zero should return int.MinValue")]
    public void Float128ILogBOfZeroShouldReturnIntMinValue()
    {
        Assert.Equal(int.MinValue, Float128.ILogB(Float128.Zero));
        Assert.Equal(int.MinValue, Float128.ILogB(Float128.NegativeZero));
    }

    [Fact(DisplayName = "Float128.ILogB of NaN should return int.MaxValue")]
    public void Float128ILogBOfNaNShouldReturnIntMaxValue()
    {
        Assert.Equal(int.MaxValue, Float128.ILogB(Float128.NaN));
    }

    [Fact(DisplayName = "Float128.ILogB of infinity should return int.MaxValue")]
    public void Float128ILogBOfInfinityShouldReturnIntMaxValue()
    {
        Assert.Equal(int.MaxValue, Float128.ILogB(Float128.PositiveInfinity));
        Assert.Equal(int.MaxValue, Float128.ILogB(Float128.NegativeInfinity));
    }

    [Fact(DisplayName = "Float128.ILogB of Epsilon should return the smallest subnormal exponent")]
    public void Float128ILogBOfEpsilonShouldReturnSmallestSubnormalExponent()
    {
        Assert.Equal(-16494, Float128.ILogB(Float128.Epsilon));
    }
}
