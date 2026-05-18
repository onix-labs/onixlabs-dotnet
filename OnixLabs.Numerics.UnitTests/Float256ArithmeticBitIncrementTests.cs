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
        Assert.Equal(Float256.Epsilon.RawBits.Upper, Float256.BitIncrement(Float256.Zero).RawBits.Upper);
        Assert.Equal(Float256.Epsilon.RawBits.Lower, Float256.BitIncrement(Float256.Zero).RawBits.Lower);
    }

    [Fact(DisplayName = "Float256.BitIncrement of negative zero should return Epsilon")]
    public void Float256BitIncrementOfNegativeZeroShouldReturnEpsilon()
    {
        Assert.Equal(Float256.Epsilon.RawBits.Upper, Float256.BitIncrement(Float256.NegativeZero).RawBits.Upper);
        Assert.Equal(Float256.Epsilon.RawBits.Lower, Float256.BitIncrement(Float256.NegativeZero).RawBits.Lower);
    }

    [Fact(DisplayName = "Float256.BitIncrement of MaxValue should return PositiveInfinity")]
    public void Float256BitIncrementOfMaxValueShouldReturnPositiveInfinity()
    {
        Assert.Equal(Float256.PositiveInfinity.RawBits.Upper, Float256.BitIncrement(Float256.MaxValue).RawBits.Upper);
        Assert.Equal(Float256.PositiveInfinity.RawBits.Lower, Float256.BitIncrement(Float256.MaxValue).RawBits.Lower);
    }

    [Fact(DisplayName = "Float256.BitIncrement of PositiveInfinity should return PositiveInfinity")]
    public void Float256BitIncrementOfPositiveInfinityShouldReturnPositiveInfinity()
    {
        Assert.Equal(Float256.PositiveInfinity.RawBits.Upper, Float256.BitIncrement(Float256.PositiveInfinity).RawBits.Upper);
        Assert.Equal(Float256.PositiveInfinity.RawBits.Lower, Float256.BitIncrement(Float256.PositiveInfinity).RawBits.Lower);
    }

    [Fact(DisplayName = "Float256.BitIncrement of NegativeInfinity should return MinValue")]
    public void Float256BitIncrementOfNegativeInfinityShouldReturnMinValue()
    {
        Assert.Equal(Float256.MinValue.RawBits.Upper, Float256.BitIncrement(Float256.NegativeInfinity).RawBits.Upper);
        Assert.Equal(Float256.MinValue.RawBits.Lower, Float256.BitIncrement(Float256.NegativeInfinity).RawBits.Lower);
    }

    [Fact(DisplayName = "Float256.BitIncrement of -Epsilon should return NegativeZero")]
    public void Float256BitIncrementOfNegativeEpsilonShouldReturnNegativeZero()
    {
        Float256 negativeEpsilon = new(new UInt256(UInt128.One << 127, UInt128.One));
        Assert.Equal(Float256.NegativeZero.RawBits.Upper, Float256.BitIncrement(negativeEpsilon).RawBits.Upper);
        Assert.Equal(Float256.NegativeZero.RawBits.Lower, Float256.BitIncrement(negativeEpsilon).RawBits.Lower);
    }

    [Fact(DisplayName = "Float256.BitDecrement of positive zero should return -Epsilon")]
    public void Float256BitDecrementOfPositiveZeroShouldReturnNegativeEpsilon()
    {
        UInt128 expectedNegativeEpsilonHigh = UInt128.One << 127;
        UInt128 expectedNegativeEpsilonLow = UInt128.One;
        Assert.Equal(expectedNegativeEpsilonHigh, Float256.BitDecrement(Float256.Zero).RawBits.Upper);
        Assert.Equal(expectedNegativeEpsilonLow, Float256.BitDecrement(Float256.Zero).RawBits.Lower);
    }

    [Fact(DisplayName = "Float256.BitDecrement of MinValue should return NegativeInfinity")]
    public void Float256BitDecrementOfMinValueShouldReturnNegativeInfinity()
    {
        Assert.Equal(Float256.NegativeInfinity.RawBits.Upper, Float256.BitDecrement(Float256.MinValue).RawBits.Upper);
        Assert.Equal(Float256.NegativeInfinity.RawBits.Lower, Float256.BitDecrement(Float256.MinValue).RawBits.Lower);
    }

    [Fact(DisplayName = "Float256.BitDecrement of PositiveInfinity should return MaxValue")]
    public void Float256BitDecrementOfPositiveInfinityShouldReturnMaxValue()
    {
        Assert.Equal(Float256.MaxValue.RawBits.Upper, Float256.BitDecrement(Float256.PositiveInfinity).RawBits.Upper);
        Assert.Equal(Float256.MaxValue.RawBits.Lower, Float256.BitDecrement(Float256.PositiveInfinity).RawBits.Lower);
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
        Assert.Equal(Float256.One.RawBits.Upper, Float256.BitDecrement(Float256.BitIncrement(Float256.One)).RawBits.Upper);
        Assert.Equal(Float256.One.RawBits.Lower, Float256.BitDecrement(Float256.BitIncrement(Float256.One)).RawBits.Lower);
        Assert.Equal(Float256.NegativeOne.RawBits.Upper, Float256.BitDecrement(Float256.BitIncrement(Float256.NegativeOne)).RawBits.Upper);
        Assert.Equal(Float256.NegativeOne.RawBits.Lower, Float256.BitDecrement(Float256.BitIncrement(Float256.NegativeOne)).RawBits.Lower);
    }
}
