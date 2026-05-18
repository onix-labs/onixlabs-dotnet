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

public sealed class Float128ArithmeticScaleBTests
{
    [Theory(DisplayName = "Float128.ScaleB by positive n should multiply by 2^n exactly for normal values")]
    [InlineData(1.0, 0, 1.0)]
    [InlineData(1.0, 1, 2.0)]
    [InlineData(1.0, 2, 4.0)]
    [InlineData(1.0, 10, 1024.0)]
    [InlineData(1.5, 2, 6.0)]
    [InlineData(-1.0, 3, -8.0)]
    public void Float128ScaleBPositiveNShouldMultiplyByPowerOfTwo(double value, int n, double expected)
    {
        Float128 actual = Float128.ScaleB(value, n);
        Float128 expectedFloat = expected;
        Assert.Equal(expectedFloat.RawBits, actual.RawBits);
    }

    [Theory(DisplayName = "Float128.ScaleB by negative n should divide by 2^n exactly for normal values")]
    [InlineData(2.0, -1, 1.0)]
    [InlineData(4.0, -2, 1.0)]
    [InlineData(1.0, -1, 0.5)]
    [InlineData(8.0, -3, 1.0)]
    [InlineData(1024.0, -10, 1.0)]
    public void Float128ScaleBNegativeNShouldDivideByPowerOfTwo(double value, int n, double expected)
    {
        Float128 actual = Float128.ScaleB(value, n);
        Float128 expectedFloat = expected;
        Assert.Equal(expectedFloat.RawBits, actual.RawBits);
    }

    [Fact(DisplayName = "Float128.ScaleB of NaN should return NaN")]
    public void Float128ScaleBOfNaNShouldReturnNaN()
    {
        Assert.True(Float128.IsNaN(Float128.ScaleB(Float128.NaN, 5)));
    }

    [Fact(DisplayName = "Float128.ScaleB of infinity should return same infinity")]
    public void Float128ScaleBOfInfinityShouldReturnSameInfinity()
    {
        Assert.True(Float128.IsPositiveInfinity(Float128.ScaleB(Float128.PositiveInfinity, 100)));
        Assert.True(Float128.IsNegativeInfinity(Float128.ScaleB(Float128.NegativeInfinity, -100)));
    }

    [Fact(DisplayName = "Float128.ScaleB of zero should preserve sign")]
    public void Float128ScaleBOfZeroShouldPreserveSign()
    {
        Assert.Equal(Float128.Zero.RawBits, Float128.ScaleB(Float128.Zero, 50).RawBits);
        Assert.Equal(Float128.NegativeZero.RawBits, Float128.ScaleB(Float128.NegativeZero, 50).RawBits);
    }

    [Fact(DisplayName = "Float128.ScaleB by huge positive n should overflow to infinity")]
    public void Float128ScaleBHugeNShouldOverflow()
    {
        Float128 result = Float128.ScaleB(Float128.One, 100000);
        Assert.True(Float128.IsPositiveInfinity(result));
    }

    [Fact(DisplayName = "Float128.ScaleB by huge negative n should underflow to zero")]
    public void Float128ScaleBHugeNegativeNShouldUnderflowToZero()
    {
        Float128 result = Float128.ScaleB(Float128.One, -100000);
        Assert.True(Float128.IsZero(result));
    }
}
