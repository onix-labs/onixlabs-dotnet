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

public sealed class UInt256ArithmeticBinaryIntegerTests
{
    [Theory(DisplayName = "UInt256.LeadingZeroCount should equal 256 minus shift for One shifted left")]
    [InlineData(0, 255)]
    [InlineData(1, 254)]
    [InlineData(63, 192)]
    [InlineData(64, 191)]
    [InlineData(127, 128)]
    [InlineData(128, 127)]
    [InlineData(192, 63)]
    [InlineData(255, 0)]
    public void UInt256LeadingZeroCountShouldEqual256MinusShift(int shift, int expected)
    {
        UInt256 value = UInt256.One << shift;
        Assert.Equal((UInt256)expected, UInt256.LeadingZeroCount(value));
    }

    [Fact(DisplayName = "UInt256.LeadingZeroCount of zero should be 256")]
    public void UInt256LeadingZeroCountOfZeroShouldBe256()
    {
        Assert.Equal((UInt256)256, UInt256.LeadingZeroCount(UInt256.Zero));
    }

    [Theory(DisplayName = "UInt256.TrailingZeroCount of One shifted left should equal shift")]
    [InlineData(0, 0)]
    [InlineData(1, 1)]
    [InlineData(63, 63)]
    [InlineData(64, 64)]
    [InlineData(127, 127)]
    [InlineData(128, 128)]
    [InlineData(192, 192)]
    [InlineData(255, 255)]
    public void UInt256TrailingZeroCountOfOneShiftedShouldEqualShift(int shift, int expected)
    {
        UInt256 value = UInt256.One << shift;
        Assert.Equal((UInt256)expected, UInt256.TrailingZeroCount(value));
    }

    [Fact(DisplayName = "UInt256.TrailingZeroCount of zero should be 256")]
    public void UInt256TrailingZeroCountOfZeroShouldBe256()
    {
        Assert.Equal((UInt256)256, UInt256.TrailingZeroCount(UInt256.Zero));
    }

    [Fact(DisplayName = "UInt256.PopCount of MaxValue should be 256")]
    public void UInt256PopCountOfMaxValueShouldBe256()
    {
        Assert.Equal((UInt256)256, UInt256.PopCount(UInt256.MaxValue));
    }

    [Theory(DisplayName = "UInt256.PopCount of One shifted left should be one for any shift")]
    [InlineData(0)]
    [InlineData(64)]
    [InlineData(128)]
    [InlineData(255)]
    public void UInt256PopCountOfOneShiftedShouldBeOne(int shift)
    {
        Assert.Equal(UInt256.One, UInt256.PopCount(UInt256.One << shift));
    }

    [Fact(DisplayName = "UInt256.PopCount of value with multiple set bits should match count")]
    public void UInt256PopCountOfValueWithMultipleSetBitsShouldMatchCount()
    {
        UInt256 value = UInt256.One | (UInt256.One << 64) | (UInt256.One << 128) | (UInt256.One << 192);
        Assert.Equal((UInt256)4, UInt256.PopCount(value));
    }

    [Theory(DisplayName = "UInt256.Log2 of One shifted left should equal shift")]
    [InlineData(1)]
    [InlineData(63)]
    [InlineData(64)]
    [InlineData(127)]
    [InlineData(128)]
    [InlineData(200)]
    [InlineData(255)]
    public void UInt256Log2OfOneShiftedShouldEqualShift(int shift)
    {
        UInt256 value = UInt256.One << shift;
        Assert.Equal((UInt256)shift, UInt256.Log2(value));
    }

    [Fact(DisplayName = "UInt256.Log2 of zero should return zero by convention")]
    public void UInt256Log2OfZeroShouldReturnZero()
    {
        Assert.Equal(UInt256.Zero, UInt256.Log2(UInt256.Zero));
    }

    [Theory(DisplayName = "UInt256.RotateLeft by full BitWidth should be identity")]
    [InlineData(0)]
    [InlineData(256)]
    [InlineData(512)]
    public void UInt256RotateLeftByFullBitWidthShouldBeIdentity(int rotation)
    {
        UInt256 value = (UInt256)0xDEADBEEF;
        Assert.Equal(value, UInt256.RotateLeft(value, rotation));
    }

    [Fact(DisplayName = "UInt256.RotateLeft by 128 should swap halves")]
    public void UInt256RotateLeftBy128ShouldSwapHalves()
    {
        UInt256 value = new(UInt128.Zero, UInt128.MaxValue);
        UInt256 rotated = UInt256.RotateLeft(value, 128);
        Assert.Equal(UInt128.MaxValue, rotated.UpperBits);
        Assert.Equal(UInt128.Zero, rotated.LowerBits);
    }

    [Fact(DisplayName = "UInt256.RotateRight by 128 should swap halves")]
    public void UInt256RotateRightBy128ShouldSwapHalves()
    {
        UInt256 value = new(UInt128.MaxValue, UInt128.Zero);
        UInt256 rotated = UInt256.RotateRight(value, 128);
        Assert.Equal(UInt128.Zero, rotated.UpperBits);
        Assert.Equal(UInt128.MaxValue, rotated.LowerBits);
    }

    [Fact(DisplayName = "UInt256.RotateLeft followed by RotateRight by same amount should be identity")]
    public void UInt256RotateLeftFollowedByRotateRightByEqualAmountShouldBeIdentity()
    {
        UInt256 value = UInt256.Parse("123456789012345678901234567890");
        Assert.Equal(value, UInt256.RotateRight(UInt256.RotateLeft(value, 73), 73));
    }
}
