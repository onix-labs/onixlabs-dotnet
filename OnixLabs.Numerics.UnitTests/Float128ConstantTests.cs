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

public sealed class Float128ConstantTests
{
    [Fact(DisplayName = "Float128.Zero should produce the canonical positive zero bit pattern")]
    public void Float128ZeroShouldProduceExpectedResult()
    {
        // Given
        UInt128 expected = UInt128.Zero;

        // When
        UInt128 actual = Float128.Zero.RawBits;

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Float128.NegativeZero should produce the canonical negative zero bit pattern")]
    public void Float128NegativeZeroShouldProduceExpectedResult()
    {
        // Given
        UInt128 expected = new(0x8000_0000_0000_0000UL, 0UL);

        // When
        UInt128 actual = Float128.NegativeZero.RawBits;

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Float128.One should produce the canonical positive one bit pattern")]
    public void Float128OneShouldProduceExpectedResult()
    {
        // Given
        UInt128 expected = new(0x3FFF_0000_0000_0000UL, 0UL);

        // When
        UInt128 actual = Float128.One.RawBits;

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Float128.NegativeOne should produce the canonical negative one bit pattern")]
    public void Float128NegativeOneShouldProduceExpectedResult()
    {
        // Given
        UInt128 expected = new(0xBFFF_0000_0000_0000UL, 0UL);

        // When
        UInt128 actual = Float128.NegativeOne.RawBits;

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Float128.Two should produce the canonical two bit pattern")]
    public void Float128TwoShouldProduceExpectedResult()
    {
        // Given
        UInt128 expected = new(0x4000_0000_0000_0000UL, 0UL);

        // When
        UInt128 actual = Float128.Two.RawBits;

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Float128.Ten should produce the canonical ten bit pattern")]
    public void Float128TenShouldProduceExpectedResult()
    {
        // Given
        UInt128 expected = new(0x4002_4000_0000_0000UL, 0UL);

        // When
        UInt128 actual = Float128.Ten.RawBits;

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Float128.MaxValue should produce the canonical maximum finite bit pattern")]
    public void Float128MaxValueShouldProduceExpectedResult()
    {
        // Given
        UInt128 expected = new(0x7FFE_FFFF_FFFF_FFFFUL, 0xFFFF_FFFF_FFFF_FFFFUL);

        // When
        UInt128 actual = Float128.MaxValue.RawBits;

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Float128.MinValue should produce the canonical minimum finite bit pattern")]
    public void Float128MinValueShouldProduceExpectedResult()
    {
        // Given
        UInt128 expected = new(0xFFFE_FFFF_FFFF_FFFFUL, 0xFFFF_FFFF_FFFF_FFFFUL);

        // When
        UInt128 actual = Float128.MinValue.RawBits;

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Float128.Epsilon should produce the smallest positive subnormal bit pattern")]
    public void Float128EpsilonShouldProduceExpectedResult()
    {
        // Given
        UInt128 expected = UInt128.One;

        // When
        UInt128 actual = Float128.Epsilon.RawBits;

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Float128.PositiveInfinity should produce the canonical positive infinity bit pattern")]
    public void Float128PositiveInfinityShouldProduceExpectedResult()
    {
        // Given
        UInt128 expected = new(0x7FFF_0000_0000_0000UL, 0UL);

        // When
        UInt128 actual = Float128.PositiveInfinity.RawBits;

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Float128.NegativeInfinity should produce the canonical negative infinity bit pattern")]
    public void Float128NegativeInfinityShouldProduceExpectedResult()
    {
        // Given
        UInt128 expected = new(0xFFFF_0000_0000_0000UL, 0UL);

        // When
        UInt128 actual = Float128.NegativeInfinity.RawBits;

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Float128.NaN should produce the canonical quiet NaN bit pattern")]
    public void Float128NaNShouldProduceExpectedResult()
    {
        // Given
        UInt128 expected = new(0x7FFF_8000_0000_0000UL, 0UL);

        // When
        UInt128 actual = Float128.NaN.RawBits;

        // Then
        Assert.Equal(expected, actual);
    }
}
