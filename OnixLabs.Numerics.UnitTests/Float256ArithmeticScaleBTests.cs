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

public sealed class Float256ArithmeticScaleBTests
{
    [Theory(DisplayName = "Float256.ScaleB by positive n should multiply by 2^n exactly for normal values")]
    [InlineData(1.0, 0, 1.0)]
    [InlineData(1.0, 1, 2.0)]
    [InlineData(1.0, 2, 4.0)]
    [InlineData(1.0, 10, 1024.0)]
    [InlineData(1.5, 2, 6.0)]
    [InlineData(-1.0, 3, -8.0)]
    public void Float256ScaleBPositiveNShouldMultiplyByPowerOfTwo(double value, int n, double expected)
    {
        Float256 actual = Float256.ScaleB(value, n);
        Float256 expectedFloat = expected;
        Assert.Equal(expectedFloat.RawHighBits, actual.RawHighBits);
        Assert.Equal(expectedFloat.RawLowBits, actual.RawLowBits);
    }

    [Theory(DisplayName = "Float256.ScaleB by negative n should divide by 2^n exactly for normal values")]
    [InlineData(2.0, -1, 1.0)]
    [InlineData(4.0, -2, 1.0)]
    [InlineData(1.0, -1, 0.5)]
    [InlineData(8.0, -3, 1.0)]
    [InlineData(1024.0, -10, 1.0)]
    public void Float256ScaleBNegativeNShouldDivideByPowerOfTwo(double value, int n, double expected)
    {
        Float256 actual = Float256.ScaleB(value, n);
        Float256 expectedFloat = expected;
        Assert.Equal(expectedFloat.RawHighBits, actual.RawHighBits);
        Assert.Equal(expectedFloat.RawLowBits, actual.RawLowBits);
    }

    [Fact(DisplayName = "Float256.ScaleB of NaN should return NaN")]
    public void Float256ScaleBOfNaNShouldReturnNaN()
    {
        Assert.True(Float256.IsNaN(Float256.ScaleB(Float256.NaN, 5)));
    }

    [Fact(DisplayName = "Float256.ScaleB of infinity should return same infinity")]
    public void Float256ScaleBOfInfinityShouldReturnSameInfinity()
    {
        Assert.True(Float256.IsPositiveInfinity(Float256.ScaleB(Float256.PositiveInfinity, 100)));
        Assert.True(Float256.IsNegativeInfinity(Float256.ScaleB(Float256.NegativeInfinity, -100)));
    }

    [Fact(DisplayName = "Float256.ScaleB of zero should preserve sign")]
    public void Float256ScaleBOfZeroShouldPreserveSign()
    {
        Assert.Equal(Float256.Zero.RawHighBits, Float256.ScaleB(Float256.Zero, 50).RawHighBits);
        Assert.Equal(Float256.Zero.RawLowBits, Float256.ScaleB(Float256.Zero, 50).RawLowBits);
        Assert.Equal(Float256.NegativeZero.RawHighBits, Float256.ScaleB(Float256.NegativeZero, 50).RawHighBits);
        Assert.Equal(Float256.NegativeZero.RawLowBits, Float256.ScaleB(Float256.NegativeZero, 50).RawLowBits);
    }

    [Fact(DisplayName = "Float256.ScaleB by huge positive n should overflow to infinity")]
    public void Float256ScaleBHugeNShouldOverflow()
    {
        Float256 result = Float256.ScaleB(Float256.One, 1_000_000);
        Assert.True(Float256.IsPositiveInfinity(result));
    }

    [Fact(DisplayName = "Float256.ScaleB by huge negative n should underflow to zero")]
    public void Float256ScaleBHugeNegativeNShouldUnderflowToZero()
    {
        Float256 result = Float256.ScaleB(Float256.One, -1_000_000);
        Assert.True(Float256.IsZero(result));
    }
}
