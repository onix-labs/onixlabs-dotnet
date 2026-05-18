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

public sealed class Float256BigDecimalConversionTests
{
    [Fact(DisplayName = "Float256 to BigDecimal of zero should produce BigDecimal.Zero")]
    public void Float256ToBigDecimalOfZeroShouldProduceZero()
    {
        Assert.Equal(BigDecimal.Zero, (BigDecimal)Float256.Zero);
        Assert.Equal(BigDecimal.Zero, (BigDecimal)Float256.NegativeZero);
    }

    [Fact(DisplayName = "Float256 to BigDecimal of NaN should throw")]
    public void Float256ToBigDecimalOfNaNShouldThrow()
    {
        Assert.Throws<OverflowException>(() => (BigDecimal)Float256.NaN);
        Assert.Throws<OverflowException>(() => (BigDecimal)Float256.PositiveInfinity);
        Assert.Throws<OverflowException>(() => (BigDecimal)Float256.NegativeInfinity);
    }

    [Theory(DisplayName = "Float256 to BigDecimal of small integers should be exact")]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(-1)]
    [InlineData(100)]
    [InlineData(-100)]
    [InlineData(1000000)]
    public void Float256ToBigDecimalOfSmallIntegersShouldBeExact(int value)
    {
        Float256 wide = value;
        BigDecimal bd = (BigDecimal)wide;
        Assert.Equal((BigDecimal)value, bd);
    }

    [Fact(DisplayName = "Float256 to BigDecimal of 0.5 should be exact")]
    public void Float256ToBigDecimalOfHalfShouldBeExact()
    {
        Float256 wide = 0.5;
        BigDecimal bd = (BigDecimal)wide;
        Assert.Equal(BigDecimal.Parse("0.5"), bd);
    }

    [Fact(DisplayName = "BigDecimal to Float256 of zero should produce Float256.Zero")]
    public void BigDecimalToFloat256OfZeroShouldProduceZero()
    {
        Assert.Equal(Float256.Zero.RawHighBits, ((Float256)BigDecimal.Zero).RawHighBits);
        Assert.Equal(Float256.Zero.RawLowBits, ((Float256)BigDecimal.Zero).RawLowBits);
    }

    [Theory(DisplayName = "BigDecimal to Float256 of small integers should be exact")]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(-1)]
    [InlineData(2)]
    [InlineData(10)]
    [InlineData(100)]
    [InlineData(-100)]
    public void BigDecimalToFloat256OfSmallIntegersShouldBeExact(int value)
    {
        BigDecimal bd = value;
        Float256 wide = (Float256)bd;
        Float256 expected = value;
        Assert.Equal(expected.RawHighBits, wide.RawHighBits);
        Assert.Equal(expected.RawLowBits, wide.RawLowBits);
    }

    [Theory(DisplayName = "Float256 round-trips through BigDecimal preserve representable values")]
    [InlineData(0.0)]
    [InlineData(1.0)]
    [InlineData(-1.0)]
    [InlineData(0.5)]
    [InlineData(2.5)]
    [InlineData(100.0)]
    [InlineData(-100.0)]
    [InlineData(0.25)]
    [InlineData(0.125)]
    public void Float256RoundTripThroughBigDecimalShouldPreserveValue(double value)
    {
        Float256 original = value;
        BigDecimal bd = (BigDecimal)original;
        Float256 roundTripped = (Float256)bd;
        Assert.Equal(original.RawHighBits, roundTripped.RawHighBits);
        Assert.Equal(original.RawLowBits, roundTripped.RawLowBits);
    }
}
