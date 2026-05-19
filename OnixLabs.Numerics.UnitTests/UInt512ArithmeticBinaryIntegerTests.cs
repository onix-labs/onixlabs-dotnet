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
using System.Numerics;

namespace OnixLabs.Numerics.UnitTests;

public sealed class UInt512ArithmeticBinaryIntegerTests
{
    [Fact(DisplayName = "UInt512.LeadingZeroCount of Zero should be 512")]
    public void UInt512LeadingZeroCountOfZeroShouldBe512()
    {
        Assert.Equal((UInt512)512UL, UInt512.LeadingZeroCount(UInt512.Zero));
    }

    [Fact(DisplayName = "UInt512.LeadingZeroCount of One should be 511")]
    public void UInt512LeadingZeroCountOfOneShouldBe511()
    {
        Assert.Equal((UInt512)511UL, UInt512.LeadingZeroCount(UInt512.One));
    }

    [Fact(DisplayName = "UInt512.LeadingZeroCount of MaxValue should be 0")]
    public void UInt512LeadingZeroCountOfMaxValueShouldBeZero()
    {
        Assert.Equal(UInt512.Zero, UInt512.LeadingZeroCount(UInt512.MaxValue));
    }

    [Fact(DisplayName = "UInt512.LeadingZeroCount with upper-half bit set should be in [0, 255]")]
    public void UInt512LeadingZeroCountUpperBitSetShouldBeUnder256()
    {
        UInt512 value = UInt512.One << 256;
        Assert.Equal((UInt512)255UL, UInt512.LeadingZeroCount(value));
    }

    [Fact(DisplayName = "UInt512.LeadingZeroCount with only lower bit set should be 256 + zerosInLower")]
    public void UInt512LeadingZeroCountLowerOnlyShouldExclude256()
    {
        UInt512 value = new(UInt256.Zero, UInt256.One << 255);
        Assert.Equal((UInt512)256UL, UInt512.LeadingZeroCount(value));
    }

    [Fact(DisplayName = "UInt512.TrailingZeroCount of Zero should be 512")]
    public void UInt512TrailingZeroCountOfZeroShouldBe512()
    {
        Assert.Equal((UInt512)512UL, UInt512.TrailingZeroCount(UInt512.Zero));
    }

    [Fact(DisplayName = "UInt512.TrailingZeroCount of One should be 0")]
    public void UInt512TrailingZeroCountOfOneShouldBeZero()
    {
        Assert.Equal(UInt512.Zero, UInt512.TrailingZeroCount(UInt512.One));
    }

    [Fact(DisplayName = "UInt512.TrailingZeroCount of high-half-only value should be 256")]
    public void UInt512TrailingZeroCountOfHighOnlyShouldBe256()
    {
        UInt512 value = new(UInt256.One, UInt256.Zero);
        Assert.Equal((UInt512)256UL, UInt512.TrailingZeroCount(value));
    }

    [Fact(DisplayName = "UInt512.TrailingZeroCount of single high bit should be 511")]
    public void UInt512TrailingZeroCountOfSingleHighBitShouldBe511()
    {
        UInt512 value = UInt512.One << 511;
        Assert.Equal((UInt512)511UL, UInt512.TrailingZeroCount(value));
    }

    [Fact(DisplayName = "UInt512.PopCount of Zero should be 0")]
    public void UInt512PopCountOfZeroShouldBeZero()
    {
        Assert.Equal(UInt512.Zero, UInt512.PopCount(UInt512.Zero));
    }

    [Fact(DisplayName = "UInt512.PopCount of MaxValue should be 512")]
    public void UInt512PopCountOfMaxValueShouldBe512()
    {
        Assert.Equal((UInt512)512UL, UInt512.PopCount(UInt512.MaxValue));
    }

    [Fact(DisplayName = "UInt512.PopCount of One should be 1")]
    public void UInt512PopCountOfOneShouldBeOne()
    {
        Assert.Equal(UInt512.One, UInt512.PopCount(UInt512.One));
    }

    [Fact(DisplayName = "UInt512.PopCount of an alternating bit pattern should be 256")]
    public void UInt512PopCountOfAlternatingShouldBe256()
    {
        // Construct a value of alternating 0xAAAA bytes spanning both halves
        UInt256 half = UInt256.MaxValue;
        UInt512 alternating = new(half, UInt256.Zero);
        Assert.Equal((UInt512)256UL, UInt512.PopCount(alternating));
    }

    [Fact(DisplayName = "UInt512.Log2 of Zero should be Zero")]
    public void UInt512Log2OfZeroShouldBeZero()
    {
        Assert.Equal(UInt512.Zero, UInt512.Log2(UInt512.Zero));
    }

    [Fact(DisplayName = "UInt512.Log2 of One should be Zero")]
    public void UInt512Log2OfOneShouldBeZero()
    {
        Assert.Equal(UInt512.Zero, UInt512.Log2(UInt512.One));
    }

    [Fact(DisplayName = "UInt512.Log2 of MaxValue should be 511")]
    public void UInt512Log2OfMaxValueShouldBe511()
    {
        Assert.Equal((UInt512)511UL, UInt512.Log2(UInt512.MaxValue));
    }

    [Theory(DisplayName = "UInt512.Log2 of 2^n should be n")]
    [InlineData(1)]
    [InlineData(63)]
    [InlineData(127)]
    [InlineData(255)]
    [InlineData(256)]
    [InlineData(384)]
    [InlineData(511)]
    public void UInt512Log2OfPowerOfTwoShouldMatchExponent(int exponent)
    {
        UInt512 value = UInt512.One << exponent;
        Assert.Equal((UInt512)(ulong)exponent, UInt512.Log2(value));
    }

    [Fact(DisplayName = "UInt512.RotateLeft by zero should be identity")]
    public void UInt512RotateLeftByZeroShouldBeIdentity()
    {
        UInt512 value = UInt512.Parse("123456789012345678901234567890");
        Assert.Equal(value, UInt512.RotateLeft(value, 0));
    }

    [Fact(DisplayName = "UInt512.RotateLeft by 1 should rotate top bit to position 0")]
    public void UInt512RotateLeftBy1ShouldWrapTopBit()
    {
        UInt512 value = UInt512.One << 511;
        Assert.Equal(UInt512.One, UInt512.RotateLeft(value, 1));
    }

    [Fact(DisplayName = "UInt512.RotateLeft by 512 should return same value")]
    public void UInt512RotateLeftBy512ShouldBeIdentity()
    {
        UInt512 value = UInt512.Parse("123456789012345678901234567890");
        Assert.Equal(value, UInt512.RotateLeft(value, 512));
    }

    [Fact(DisplayName = "UInt512.RotateRight by 1 should rotate position 0 to top bit")]
    public void UInt512RotateRightBy1ShouldWrapBottomBit()
    {
        UInt512 value = UInt512.One;
        Assert.Equal(UInt512.One << 511, UInt512.RotateRight(value, 1));
    }

    [Fact(DisplayName = "UInt512.RotateLeft then RotateRight by same amount should be identity")]
    public void UInt512RotateLeftThenRightShouldBeIdentity()
    {
        UInt512 value = UInt512.Parse("123456789012345678901234567890");
        UInt512 rotated = UInt512.RotateRight(UInt512.RotateLeft(value, 173), 173);
        Assert.Equal(value, rotated);
    }

    [Fact(DisplayName = "UInt512.RotateLeft by 256 should swap upper and lower halves")]
    public void UInt512RotateLeftBy256ShouldSwapHalves()
    {
        UInt512 value = new((UInt256)0x1111_2222_3333_4444UL, (UInt256)0x5555_6666_7777_8888UL);
        UInt512 rotated = UInt512.RotateLeft(value, 256);
        Assert.Equal(new UInt512((UInt256)0x5555_6666_7777_8888UL, (UInt256)0x1111_2222_3333_4444UL), rotated);
    }
}
