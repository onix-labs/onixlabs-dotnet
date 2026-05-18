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

public sealed class Float256ArithmeticBitIncrementTests
{
    [Fact(DisplayName = "Float256.BitIncrement of positive zero should return Epsilon")]
    public void Float256BitIncrementOfPositiveZeroShouldReturnEpsilon()
    {
        Assert.Equal(Float256.Epsilon.RawHighBits, Float256.BitIncrement(Float256.Zero).RawHighBits);
        Assert.Equal(Float256.Epsilon.RawLowBits, Float256.BitIncrement(Float256.Zero).RawLowBits);
    }

    [Fact(DisplayName = "Float256.BitIncrement of negative zero should return Epsilon")]
    public void Float256BitIncrementOfNegativeZeroShouldReturnEpsilon()
    {
        Assert.Equal(Float256.Epsilon.RawHighBits, Float256.BitIncrement(Float256.NegativeZero).RawHighBits);
        Assert.Equal(Float256.Epsilon.RawLowBits, Float256.BitIncrement(Float256.NegativeZero).RawLowBits);
    }

    [Fact(DisplayName = "Float256.BitIncrement of MaxValue should return PositiveInfinity")]
    public void Float256BitIncrementOfMaxValueShouldReturnPositiveInfinity()
    {
        Assert.Equal(Float256.PositiveInfinity.RawHighBits, Float256.BitIncrement(Float256.MaxValue).RawHighBits);
        Assert.Equal(Float256.PositiveInfinity.RawLowBits, Float256.BitIncrement(Float256.MaxValue).RawLowBits);
    }

    [Fact(DisplayName = "Float256.BitIncrement of PositiveInfinity should return PositiveInfinity")]
    public void Float256BitIncrementOfPositiveInfinityShouldReturnPositiveInfinity()
    {
        Assert.Equal(Float256.PositiveInfinity.RawHighBits, Float256.BitIncrement(Float256.PositiveInfinity).RawHighBits);
        Assert.Equal(Float256.PositiveInfinity.RawLowBits, Float256.BitIncrement(Float256.PositiveInfinity).RawLowBits);
    }

    [Fact(DisplayName = "Float256.BitIncrement of NegativeInfinity should return MinValue")]
    public void Float256BitIncrementOfNegativeInfinityShouldReturnMinValue()
    {
        Assert.Equal(Float256.MinValue.RawHighBits, Float256.BitIncrement(Float256.NegativeInfinity).RawHighBits);
        Assert.Equal(Float256.MinValue.RawLowBits, Float256.BitIncrement(Float256.NegativeInfinity).RawLowBits);
    }

    [Fact(DisplayName = "Float256.BitIncrement of -Epsilon should return NegativeZero")]
    public void Float256BitIncrementOfNegativeEpsilonShouldReturnNegativeZero()
    {
        Float256 negativeEpsilon = new(UInt128.One << 127, UInt128.One);
        Assert.Equal(Float256.NegativeZero.RawHighBits, Float256.BitIncrement(negativeEpsilon).RawHighBits);
        Assert.Equal(Float256.NegativeZero.RawLowBits, Float256.BitIncrement(negativeEpsilon).RawLowBits);
    }

    [Fact(DisplayName = "Float256.BitDecrement of positive zero should return -Epsilon")]
    public void Float256BitDecrementOfPositiveZeroShouldReturnNegativeEpsilon()
    {
        UInt128 expectedNegativeEpsilonHigh = UInt128.One << 127;
        UInt128 expectedNegativeEpsilonLow = UInt128.One;
        Assert.Equal(expectedNegativeEpsilonHigh, Float256.BitDecrement(Float256.Zero).RawHighBits);
        Assert.Equal(expectedNegativeEpsilonLow, Float256.BitDecrement(Float256.Zero).RawLowBits);
    }

    [Fact(DisplayName = "Float256.BitDecrement of MinValue should return NegativeInfinity")]
    public void Float256BitDecrementOfMinValueShouldReturnNegativeInfinity()
    {
        Assert.Equal(Float256.NegativeInfinity.RawHighBits, Float256.BitDecrement(Float256.MinValue).RawHighBits);
        Assert.Equal(Float256.NegativeInfinity.RawLowBits, Float256.BitDecrement(Float256.MinValue).RawLowBits);
    }

    [Fact(DisplayName = "Float256.BitDecrement of PositiveInfinity should return MaxValue")]
    public void Float256BitDecrementOfPositiveInfinityShouldReturnMaxValue()
    {
        Assert.Equal(Float256.MaxValue.RawHighBits, Float256.BitDecrement(Float256.PositiveInfinity).RawHighBits);
        Assert.Equal(Float256.MaxValue.RawLowBits, Float256.BitDecrement(Float256.PositiveInfinity).RawLowBits);
    }

    [Fact(DisplayName = "Float256.BitIncrement and BitDecrement of NaN should remain NaN")]
    public void Float256BitIncrementAndBitDecrementOfNaNShouldRemainNaN()
    {
        Assert.True(Float256.IsNaN(Float256.BitIncrement(Float256.NaN)));
        Assert.True(Float256.IsNaN(Float256.BitDecrement(Float256.NaN)));
    }

    [Fact(DisplayName = "Float256.BitIncrement followed by BitDecrement should be a no-op for finite values")]
    public void Float256BitIncrementBitDecrementShouldBeNoOpForFinite()
    {
        Assert.Equal(Float256.One.RawHighBits, Float256.BitDecrement(Float256.BitIncrement(Float256.One)).RawHighBits);
        Assert.Equal(Float256.One.RawLowBits, Float256.BitDecrement(Float256.BitIncrement(Float256.One)).RawLowBits);
        Assert.Equal(Float256.NegativeOne.RawHighBits, Float256.BitDecrement(Float256.BitIncrement(Float256.NegativeOne)).RawHighBits);
        Assert.Equal(Float256.NegativeOne.RawLowBits, Float256.BitDecrement(Float256.BitIncrement(Float256.NegativeOne)).RawLowBits);
    }
}
