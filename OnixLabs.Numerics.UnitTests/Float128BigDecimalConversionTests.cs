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

public sealed class Float128BigDecimalConversionTests
{
    [Fact(DisplayName = "Float128 to BigDecimal of zero should produce BigDecimal.Zero")]
    public void Float128ToBigDecimalOfZeroShouldProduceZero()
    {
        Assert.Equal(BigDecimal.Zero, (BigDecimal)Float128.Zero);
        Assert.Equal(BigDecimal.Zero, (BigDecimal)Float128.NegativeZero);
    }

    [Fact(DisplayName = "Float128 to BigDecimal of NaN should throw")]
    public void Float128ToBigDecimalOfNaNShouldThrow()
    {
        Assert.Throws<OverflowException>(() => (BigDecimal)Float128.NaN);
        Assert.Throws<OverflowException>(() => (BigDecimal)Float128.PositiveInfinity);
        Assert.Throws<OverflowException>(() => (BigDecimal)Float128.NegativeInfinity);
    }

    [Theory(DisplayName = "Float128 to BigDecimal of small integers should be exact")]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(-1)]
    [InlineData(100)]
    [InlineData(-100)]
    [InlineData(1000000)]
    public void Float128ToBigDecimalOfSmallIntegersShouldBeExact(int value)
    {
        Float128 wide = value;
        BigDecimal bd = (BigDecimal)wide;
        Assert.Equal((BigDecimal)value, bd);
    }

    [Fact(DisplayName = "Float128 to BigDecimal of 0.5 should be exact")]
    public void Float128ToBigDecimalOfHalfShouldBeExact()
    {
        Float128 wide = 0.5;
        BigDecimal bd = (BigDecimal)wide;
        Assert.Equal(BigDecimal.Parse("0.5"), bd);
    }

    [Fact(DisplayName = "BigDecimal to Float128 of zero should produce Float128.Zero")]
    public void BigDecimalToFloat128OfZeroShouldProduceZero()
    {
        Assert.Equal(Float128.Zero.RawBits, ((Float128)BigDecimal.Zero).RawBits);
    }

    [Theory(DisplayName = "BigDecimal to Float128 of small integers should be exact")]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(-1)]
    [InlineData(2)]
    [InlineData(10)]
    [InlineData(100)]
    [InlineData(-100)]
    public void BigDecimalToFloat128OfSmallIntegersShouldBeExact(int value)
    {
        BigDecimal bd = value;
        Float128 wide = (Float128)bd;
        Float128 expected = value;
        Assert.Equal(expected.RawBits, wide.RawBits);
    }

    [Theory(DisplayName = "Float128 round-trips through BigDecimal preserve representable values")]
    [InlineData(0.0)]
    [InlineData(1.0)]
    [InlineData(-1.0)]
    [InlineData(0.5)]
    [InlineData(2.5)]
    [InlineData(100.0)]
    [InlineData(-100.0)]
    [InlineData(0.25)]
    [InlineData(0.125)]
    public void Float128RoundTripThroughBigDecimalShouldPreserveValue(double value)
    {
        Float128 original = value;
        BigDecimal bd = (BigDecimal)original;
        Float128 roundTripped = (Float128)bd;
        Assert.Equal(original.RawBits, roundTripped.RawBits);
    }
}
