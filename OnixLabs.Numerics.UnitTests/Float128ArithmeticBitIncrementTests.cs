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

public sealed class Float128ArithmeticBitIncrementTests
{
    [Fact(DisplayName = "Float128.BitIncrement of positive zero should return Epsilon")]
    public void Float128BitIncrementOfPositiveZeroShouldReturnEpsilon()
    {
        Assert.Equal(Float128.Epsilon.Bits, Float128.BitIncrement(Float128.Zero).Bits);
    }

    [Fact(DisplayName = "Float128.BitIncrement of negative zero should return Epsilon")]
    public void Float128BitIncrementOfNegativeZeroShouldReturnEpsilon()
    {
        Assert.Equal(Float128.Epsilon.Bits, Float128.BitIncrement(Float128.NegativeZero).Bits);
    }

    [Fact(DisplayName = "Float128.BitIncrement of MaxValue should return PositiveInfinity")]
    public void Float128BitIncrementOfMaxValueShouldReturnPositiveInfinity()
    {
        Assert.Equal(Float128.PositiveInfinity.Bits, Float128.BitIncrement(Float128.MaxValue).Bits);
    }

    [Fact(DisplayName = "Float128.BitIncrement of PositiveInfinity should return PositiveInfinity")]
    public void Float128BitIncrementOfPositiveInfinityShouldReturnPositiveInfinity()
    {
        Assert.Equal(Float128.PositiveInfinity.Bits, Float128.BitIncrement(Float128.PositiveInfinity).Bits);
    }

    [Fact(DisplayName = "Float128.BitIncrement of NegativeInfinity should return MinValue")]
    public void Float128BitIncrementOfNegativeInfinityShouldReturnMinValue()
    {
        Assert.Equal(Float128.MinValue.Bits, Float128.BitIncrement(Float128.NegativeInfinity).Bits);
    }

    [Fact(DisplayName = "Float128.BitIncrement of -Epsilon should return NegativeZero")]
    public void Float128BitIncrementOfNegativeEpsilonShouldReturnNegativeZero()
    {
        Float128 negativeEpsilon = new(UInt128.One | (UInt128.One << 127));
        Assert.Equal(Float128.NegativeZero.Bits, Float128.BitIncrement(negativeEpsilon).Bits);
    }

    [Fact(DisplayName = "Float128.BitDecrement of positive zero should return -Epsilon")]
    public void Float128BitDecrementOfPositiveZeroShouldReturnNegativeEpsilon()
    {
        UInt128 expectedNegativeEpsilon = UInt128.One | (UInt128.One << 127);
        Assert.Equal(expectedNegativeEpsilon, Float128.BitDecrement(Float128.Zero).Bits);
    }

    [Fact(DisplayName = "Float128.BitDecrement of MinValue should return NegativeInfinity")]
    public void Float128BitDecrementOfMinValueShouldReturnNegativeInfinity()
    {
        Assert.Equal(Float128.NegativeInfinity.Bits, Float128.BitDecrement(Float128.MinValue).Bits);
    }

    [Fact(DisplayName = "Float128.BitDecrement of PositiveInfinity should return MaxValue")]
    public void Float128BitDecrementOfPositiveInfinityShouldReturnMaxValue()
    {
        Assert.Equal(Float128.MaxValue.Bits, Float128.BitDecrement(Float128.PositiveInfinity).Bits);
    }

    [Fact(DisplayName = "Float128.BitIncrement and BitDecrement of NaN should remain NaN")]
    public void Float128BitIncrementAndBitDecrementOfNaNShouldRemainNaN()
    {
        Assert.True(Float128.IsNaN(Float128.BitIncrement(Float128.NaN)));
        Assert.True(Float128.IsNaN(Float128.BitDecrement(Float128.NaN)));
    }

    [Fact(DisplayName = "Float128.BitIncrement followed by BitDecrement should be a no-op for finite values")]
    public void Float128BitIncrementBitDecrementShouldBeNoOpForFinite()
    {
        Assert.Equal(Float128.One.Bits, Float128.BitDecrement(Float128.BitIncrement(Float128.One)).Bits);
        Assert.Equal(Float128.NegativeOne.Bits, Float128.BitDecrement(Float128.BitIncrement(Float128.NegativeOne)).Bits);
    }
}
