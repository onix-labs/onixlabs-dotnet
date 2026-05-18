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

public sealed class Int256ArithmeticBinaryIntegerTests
{
    [Fact(DisplayName = "Int256.LeadingZeroCount of zero should be 256")]
    public void Int256LeadingZeroCountOfZeroShouldBe256()
    {
        Assert.Equal((Int256)256, Int256.LeadingZeroCount(Int256.Zero));
    }

    [Theory(DisplayName = "Int256.LeadingZeroCount of One shifted left should equal 255 minus shift")]
    [InlineData(0, 255)]
    [InlineData(1, 254)]
    [InlineData(63, 192)]
    [InlineData(64, 191)]
    [InlineData(127, 128)]
    [InlineData(128, 127)]
    [InlineData(254, 1)]
    public void Int256LeadingZeroCountOfOneShiftedShouldEqual255MinusShift(int shift, int expected)
    {
        Int256 value = Int256.One << shift;
        Assert.Equal((Int256)expected, Int256.LeadingZeroCount(value));
    }

    [Fact(DisplayName = "Int256.LeadingZeroCount of NegativeOne should be zero (MSB set)")]
    public void Int256LeadingZeroCountOfNegativeOneShouldBeZero()
    {
        Assert.Equal(Int256.Zero, Int256.LeadingZeroCount(Int256.NegativeOne));
    }

    [Theory(DisplayName = "Int256.TrailingZeroCount of One shifted left should equal shift")]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(64)]
    [InlineData(127)]
    [InlineData(128)]
    [InlineData(254)]
    public void Int256TrailingZeroCountOfOneShiftedShouldEqualShift(int shift)
    {
        Int256 value = Int256.One << shift;
        Assert.Equal((Int256)shift, Int256.TrailingZeroCount(value));
    }

    [Fact(DisplayName = "Int256.TrailingZeroCount of zero should be 256")]
    public void Int256TrailingZeroCountOfZeroShouldBe256()
    {
        Assert.Equal((Int256)256, Int256.TrailingZeroCount(Int256.Zero));
    }

    [Fact(DisplayName = "Int256.TrailingZeroCount of MinValue should be 255 (only sign bit set)")]
    public void Int256TrailingZeroCountOfMinValueShouldBe255()
    {
        Assert.Equal((Int256)255, Int256.TrailingZeroCount(Int256.MinValue));
    }

    [Fact(DisplayName = "Int256.PopCount of zero should be zero")]
    public void Int256PopCountOfZeroShouldBeZero()
    {
        Assert.Equal(Int256.Zero, Int256.PopCount(Int256.Zero));
    }

    [Fact(DisplayName = "Int256.PopCount of NegativeOne should be 256 (all bits set)")]
    public void Int256PopCountOfNegativeOneShouldBe256()
    {
        Assert.Equal((Int256)256, Int256.PopCount(Int256.NegativeOne));
    }

    [Fact(DisplayName = "Int256.PopCount of MinValue should be 1 (only sign bit set)")]
    public void Int256PopCountOfMinValueShouldBe1()
    {
        Assert.Equal(Int256.One, Int256.PopCount(Int256.MinValue));
    }

    [Fact(DisplayName = "Int256.PopCount of MaxValue should be 255")]
    public void Int256PopCountOfMaxValueShouldBe255()
    {
        Assert.Equal((Int256)255, Int256.PopCount(Int256.MaxValue));
    }

    [Theory(DisplayName = "Int256.Log2 of One shifted left should equal shift for positive values")]
    [InlineData(0)]
    [InlineData(64)]
    [InlineData(127)]
    [InlineData(128)]
    [InlineData(200)]
    [InlineData(254)]
    public void Int256Log2OfOneShiftedShouldEqualShift(int shift)
    {
        Int256 value = Int256.One << shift;
        Assert.Equal((Int256)shift, Int256.Log2(value));
    }

    [Fact(DisplayName = "Int256.Log2 of zero should return zero")]
    public void Int256Log2OfZeroShouldReturnZero()
    {
        Assert.Equal(Int256.Zero, Int256.Log2(Int256.Zero));
    }

    [Fact(DisplayName = "Int256.Log2 of negative should throw ArgumentOutOfRangeException")]
    public void Int256Log2OfNegativeShouldThrow()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => Int256.Log2(Int256.NegativeOne));
        Assert.Throws<ArgumentOutOfRangeException>(() => Int256.Log2(Int256.MinValue));
    }

    [Fact(DisplayName = "Int256.RotateLeft by 128 should swap halves")]
    public void Int256RotateLeftBy128ShouldSwapHalves()
    {
        Int256 value = new(UInt128.Zero, UInt128.MaxValue);
        Int256 rotated = Int256.RotateLeft(value, 128);
        Assert.Equal(UInt128.MaxValue, rotated.Upper);
        Assert.Equal(UInt128.Zero, rotated.Lower);
    }

    [Fact(DisplayName = "Int256.RotateRight by 128 should swap halves")]
    public void Int256RotateRightBy128ShouldSwapHalves()
    {
        Int256 value = new(UInt128.MaxValue, UInt128.Zero);
        Int256 rotated = Int256.RotateRight(value, 128);
        Assert.Equal(UInt128.Zero, rotated.Upper);
        Assert.Equal(UInt128.MaxValue, rotated.Lower);
    }

    [Fact(DisplayName = "Int256.RotateLeft followed by RotateRight by same amount should be identity")]
    public void Int256RotateLeftFollowedByRotateRightShouldBeIdentity()
    {
        Int256 value = (Int256)(-12345678);
        Assert.Equal(value, Int256.RotateRight(Int256.RotateLeft(value, 73), 73));
    }
}
