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

public sealed class Float256ConstantTests
{
    [Fact(DisplayName = "Float256.Zero should produce the canonical positive zero bit pattern")]
    public void Float256ZeroShouldProduceExpectedResult()
    {
        // Given / When
        Float256 value = Float256.Zero;

        // Then
        Assert.Equal(UInt128.Zero, value.Bits.Upper);
        Assert.Equal(UInt128.Zero, value.Bits.Lower);
    }

    [Fact(DisplayName = "Float256.NegativeZero should produce the canonical negative zero bit pattern")]
    public void Float256NegativeZeroShouldProduceExpectedResult()
    {
        // Given / When
        Float256 value = Float256.NegativeZero;

        // Then
        Assert.Equal(new UInt128(0x8000_0000_0000_0000UL, 0UL), value.Bits.Upper);
        Assert.Equal(UInt128.Zero, value.Bits.Lower);
    }

    [Fact(DisplayName = "Float256.One should produce the canonical positive one bit pattern")]
    public void Float256OneShouldProduceExpectedResult()
    {
        // Given / When
        Float256 value = Float256.One;

        // Then
        Assert.Equal(new UInt128(0x3FFF_F000_0000_0000UL, 0UL), value.Bits.Upper);
        Assert.Equal(UInt128.Zero, value.Bits.Lower);
    }

    [Fact(DisplayName = "Float256.NegativeOne should produce the canonical negative one bit pattern")]
    public void Float256NegativeOneShouldProduceExpectedResult()
    {
        // Given / When
        Float256 value = Float256.NegativeOne;

        // Then
        Assert.Equal(new UInt128(0xBFFF_F000_0000_0000UL, 0UL), value.Bits.Upper);
        Assert.Equal(UInt128.Zero, value.Bits.Lower);
    }

    [Fact(DisplayName = "Float256.Two should produce the canonical two bit pattern")]
    public void Float256TwoShouldProduceExpectedResult()
    {
        // Given / When
        Float256 value = Float256.Two;

        // Then
        Assert.Equal(new UInt128(0x4000_0000_0000_0000UL, 0UL), value.Bits.Upper);
        Assert.Equal(UInt128.Zero, value.Bits.Lower);
    }

    [Fact(DisplayName = "Float256.Ten should produce the canonical ten bit pattern")]
    public void Float256TenShouldProduceExpectedResult()
    {
        // Given / When
        Float256 value = Float256.Ten;

        // Then
        Assert.Equal(new UInt128(0x4000_2400_0000_0000UL, 0UL), value.Bits.Upper);
        Assert.Equal(UInt128.Zero, value.Bits.Lower);
    }

    [Fact(DisplayName = "Float256.MaxValue should produce the canonical maximum finite bit pattern")]
    public void Float256MaxValueShouldProduceExpectedResult()
    {
        // Given / When
        Float256 value = Float256.MaxValue;

        // Then
        Assert.Equal(new UInt128(0x7FFF_EFFF_FFFF_FFFFUL, 0xFFFF_FFFF_FFFF_FFFFUL), value.Bits.Upper);
        Assert.Equal(new UInt128(0xFFFF_FFFF_FFFF_FFFFUL, 0xFFFF_FFFF_FFFF_FFFFUL), value.Bits.Lower);
    }

    [Fact(DisplayName = "Float256.MinValue should produce the canonical minimum finite bit pattern")]
    public void Float256MinValueShouldProduceExpectedResult()
    {
        // Given / When
        Float256 value = Float256.MinValue;

        // Then
        Assert.Equal(new UInt128(0xFFFF_EFFF_FFFF_FFFFUL, 0xFFFF_FFFF_FFFF_FFFFUL), value.Bits.Upper);
        Assert.Equal(new UInt128(0xFFFF_FFFF_FFFF_FFFFUL, 0xFFFF_FFFF_FFFF_FFFFUL), value.Bits.Lower);
    }

    [Fact(DisplayName = "Float256.Epsilon should produce the smallest positive subnormal bit pattern")]
    public void Float256EpsilonShouldProduceExpectedResult()
    {
        // Given / When
        Float256 value = Float256.Epsilon;

        // Then
        Assert.Equal(UInt128.Zero, value.Bits.Upper);
        Assert.Equal(UInt128.One, value.Bits.Lower);
    }

    [Fact(DisplayName = "Float256.PositiveInfinity should produce the canonical positive infinity bit pattern")]
    public void Float256PositiveInfinityShouldProduceExpectedResult()
    {
        // Given / When
        Float256 value = Float256.PositiveInfinity;

        // Then
        Assert.Equal(new UInt128(0x7FFF_F000_0000_0000UL, 0UL), value.Bits.Upper);
        Assert.Equal(UInt128.Zero, value.Bits.Lower);
    }

    [Fact(DisplayName = "Float256.NegativeInfinity should produce the canonical negative infinity bit pattern")]
    public void Float256NegativeInfinityShouldProduceExpectedResult()
    {
        // Given / When
        Float256 value = Float256.NegativeInfinity;

        // Then
        Assert.Equal(new UInt128(0xFFFF_F000_0000_0000UL, 0UL), value.Bits.Upper);
        Assert.Equal(UInt128.Zero, value.Bits.Lower);
    }

    [Fact(DisplayName = "Float256.NaN should produce the canonical quiet NaN bit pattern")]
    public void Float256NaNShouldProduceExpectedResult()
    {
        // Given / When
        Float256 value = Float256.NaN;

        // Then
        Assert.Equal(new UInt128(0x7FFF_F800_0000_0000UL, 0UL), value.Bits.Upper);
        Assert.Equal(UInt128.Zero, value.Bits.Lower);
    }
}
