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
using System.Globalization;

namespace OnixLabs.Numerics.UnitTests;

public sealed class Float128ConstantTests
{
    [Fact(DisplayName = "Float128.Zero should produce the canonical positive zero bit pattern")]
    public void Float128ZeroShouldProduceExpectedResult()
    {
        // Given
        UInt128 expected = UInt128.Zero;

        // When
        UInt128 actual = Float128.Zero.Bits;

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Float128.NegativeZero should produce the canonical negative zero bit pattern")]
    public void Float128NegativeZeroShouldProduceExpectedResult()
    {
        // Given
        UInt128 expected = new(0x8000_0000_0000_0000UL, 0UL);

        // When
        UInt128 actual = Float128.NegativeZero.Bits;

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Float128.One should produce the canonical positive one bit pattern")]
    public void Float128OneShouldProduceExpectedResult()
    {
        // Given
        UInt128 expected = new(0x3FFF_0000_0000_0000UL, 0UL);

        // When
        UInt128 actual = Float128.One.Bits;

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Float128.NegativeOne should produce the canonical negative one bit pattern")]
    public void Float128NegativeOneShouldProduceExpectedResult()
    {
        // Given
        UInt128 expected = new(0xBFFF_0000_0000_0000UL, 0UL);

        // When
        UInt128 actual = Float128.NegativeOne.Bits;

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Float128.Two should produce the canonical two bit pattern")]
    public void Float128TwoShouldProduceExpectedResult()
    {
        // Given
        UInt128 expected = new(0x4000_0000_0000_0000UL, 0UL);

        // When
        UInt128 actual = Float128.Two.Bits;

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Float128.Ten should produce the canonical ten bit pattern")]
    public void Float128TenShouldProduceExpectedResult()
    {
        // Given
        UInt128 expected = new(0x4002_4000_0000_0000UL, 0UL);

        // When
        UInt128 actual = Float128.Ten.Bits;

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Float128.MaxValue should produce the canonical maximum finite bit pattern")]
    public void Float128MaxValueShouldProduceExpectedResult()
    {
        // Given
        UInt128 expected = new(0x7FFE_FFFF_FFFF_FFFFUL, 0xFFFF_FFFF_FFFF_FFFFUL);

        // When
        UInt128 actual = Float128.MaxValue.Bits;

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Float128.MinValue should produce the canonical minimum finite bit pattern")]
    public void Float128MinValueShouldProduceExpectedResult()
    {
        // Given
        UInt128 expected = new(0xFFFE_FFFF_FFFF_FFFFUL, 0xFFFF_FFFF_FFFF_FFFFUL);

        // When
        UInt128 actual = Float128.MinValue.Bits;

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Float128.Epsilon should produce the smallest positive subnormal bit pattern")]
    public void Float128EpsilonShouldProduceExpectedResult()
    {
        // Given
        UInt128 expected = UInt128.One;

        // When
        UInt128 actual = Float128.Epsilon.Bits;

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Float128.PositiveInfinity should produce the canonical positive infinity bit pattern")]
    public void Float128PositiveInfinityShouldProduceExpectedResult()
    {
        // Given
        UInt128 expected = new(0x7FFF_0000_0000_0000UL, 0UL);

        // When
        UInt128 actual = Float128.PositiveInfinity.Bits;

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Float128.NegativeInfinity should produce the canonical negative infinity bit pattern")]
    public void Float128NegativeInfinityShouldProduceExpectedResult()
    {
        // Given
        UInt128 expected = new(0xFFFF_0000_0000_0000UL, 0UL);

        // When
        UInt128 actual = Float128.NegativeInfinity.Bits;

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Float128.NaN should produce the canonical quiet NaN bit pattern")]
    public void Float128NaNShouldProduceExpectedResult()
    {
        // Given
        UInt128 expected = new(0x7FFF_8000_0000_0000UL, 0UL);

        // When
        UInt128 actual = Float128.NaN.Bits;

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Float128 math constants must initialise culture-invariantly (regression: run isolated)")]
    public void Float128MathConstantsShouldInitialiseCultureInvariantly()
    {
        // Given: a comma-decimal culture where '.' is a group separator, which (with AllowThousands)
        // would misparse the '.'-delimited constant literals as gigantic integers if parsed culture-sensitively.
        CultureInfo original = CultureInfo.CurrentCulture;
        CultureInfo.CurrentCulture = new CultureInfo("de-DE");

        try
        {
            // Then: Pi must remain ~3.14159, not a misparsed ~40-digit integer.
            Assert.True(Float128.Pi > Float128.Parse("3", CultureInfo.InvariantCulture));
            Assert.True(Float128.Pi < Float128.Parse("4", CultureInfo.InvariantCulture));
            Assert.True(Float128.E > Float128.Parse("2", CultureInfo.InvariantCulture));
            Assert.True(Float128.E < Float128.Parse("3", CultureInfo.InvariantCulture));
        }
        finally
        {
            CultureInfo.CurrentCulture = original;
        }
    }
}
