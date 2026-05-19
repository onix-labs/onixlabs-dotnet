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

public sealed class Int512ArithmeticBinaryIntegerTests
{
    [Fact(DisplayName = "Int512.LeadingZeroCount of Zero should be 512")]
    public void Int512LeadingZeroCountOfZeroShouldBe512()
    {
        Assert.Equal((Int512)512, Int512.LeadingZeroCount(Int512.Zero));
    }

    [Fact(DisplayName = "Int512.LeadingZeroCount of One should be 511")]
    public void Int512LeadingZeroCountOfOneShouldBe511()
    {
        Assert.Equal((Int512)511, Int512.LeadingZeroCount(Int512.One));
    }

    [Fact(DisplayName = "Int512.LeadingZeroCount of NegativeOne should be 0 (all bits set)")]
    public void Int512LeadingZeroCountOfNegativeOneShouldBeZero()
    {
        Assert.Equal(Int512.Zero, Int512.LeadingZeroCount(Int512.NegativeOne));
    }

    [Fact(DisplayName = "Int512.LeadingZeroCount of MaxValue should be 1 (sign bit clear)")]
    public void Int512LeadingZeroCountOfMaxValueShouldBeOne()
    {
        Assert.Equal(Int512.One, Int512.LeadingZeroCount(Int512.MaxValue));
    }

    [Fact(DisplayName = "Int512.LeadingZeroCount of MinValue should be 0 (sign bit set)")]
    public void Int512LeadingZeroCountOfMinValueShouldBeZero()
    {
        Assert.Equal(Int512.Zero, Int512.LeadingZeroCount(Int512.MinValue));
    }

    [Fact(DisplayName = "Int512.TrailingZeroCount of Zero should be 512")]
    public void Int512TrailingZeroCountOfZeroShouldBe512()
    {
        Assert.Equal((Int512)512, Int512.TrailingZeroCount(Int512.Zero));
    }

    [Fact(DisplayName = "Int512.TrailingZeroCount of One should be 0")]
    public void Int512TrailingZeroCountOfOneShouldBeZero()
    {
        Assert.Equal(Int512.Zero, Int512.TrailingZeroCount(Int512.One));
    }

    [Fact(DisplayName = "Int512.TrailingZeroCount of MinValue (sign bit only) should be 511")]
    public void Int512TrailingZeroCountOfMinValueShouldBe511()
    {
        Assert.Equal((Int512)511, Int512.TrailingZeroCount(Int512.MinValue));
    }

    [Fact(DisplayName = "Int512.PopCount of Zero should be 0")]
    public void Int512PopCountOfZeroShouldBeZero()
    {
        Assert.Equal(Int512.Zero, Int512.PopCount(Int512.Zero));
    }

    [Fact(DisplayName = "Int512.PopCount of NegativeOne should be 512")]
    public void Int512PopCountOfNegativeOneShouldBe512()
    {
        Assert.Equal((Int512)512, Int512.PopCount(Int512.NegativeOne));
    }

    [Fact(DisplayName = "Int512.PopCount of MaxValue should be 511")]
    public void Int512PopCountOfMaxValueShouldBe511()
    {
        Assert.Equal((Int512)511, Int512.PopCount(Int512.MaxValue));
    }

    [Fact(DisplayName = "Int512.PopCount of MinValue should be 1")]
    public void Int512PopCountOfMinValueShouldBeOne()
    {
        Assert.Equal(Int512.One, Int512.PopCount(Int512.MinValue));
    }

    [Fact(DisplayName = "Int512.Log2 of MaxValue should be 510")]
    public void Int512Log2OfMaxValueShouldBe510()
    {
        Assert.Equal((Int512)510, Int512.Log2(Int512.MaxValue));
    }

    [Fact(DisplayName = "Int512.Log2 of One should be 0")]
    public void Int512Log2OfOneShouldBeZero()
    {
        Assert.Equal(Int512.Zero, Int512.Log2(Int512.One));
    }

    [Fact(DisplayName = "Int512.Log2 of negative value should throw ArgumentOutOfRangeException")]
    public void Int512Log2OfNegativeShouldThrow()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => Int512.Log2(Int512.NegativeOne));
        Assert.Throws<ArgumentOutOfRangeException>(() => Int512.Log2(Int512.MinValue));
    }

    [Theory(DisplayName = "Int512.Log2 of 2^n should be n for n in [0, 510]")]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(63)]
    [InlineData(127)]
    [InlineData(255)]
    [InlineData(256)]
    [InlineData(384)]
    [InlineData(510)]
    public void Int512Log2OfPowerOfTwoShouldMatchExponent(int exponent)
    {
        Int512 value = Int512.One << exponent;
        Assert.Equal((Int512)exponent, Int512.Log2(value));
    }

    [Fact(DisplayName = "Int512.RotateLeft by zero should be identity")]
    public void Int512RotateLeftByZeroShouldBeIdentity()
    {
        Int512 value = Int512.NegativeOne;
        Assert.Equal(value, Int512.RotateLeft(value, 0));
    }

    [Fact(DisplayName = "Int512.RotateLeft of NegativeOne by any amount should remain NegativeOne")]
    public void Int512RotateLeftOfNegativeOneShouldRemainNegativeOne()
    {
        Assert.Equal(Int512.NegativeOne, Int512.RotateLeft(Int512.NegativeOne, 100));
        Assert.Equal(Int512.NegativeOne, Int512.RotateLeft(Int512.NegativeOne, 511));
    }

    [Fact(DisplayName = "Int512.RotateRight of NegativeOne by any amount should remain NegativeOne")]
    public void Int512RotateRightOfNegativeOneShouldRemainNegativeOne()
    {
        Assert.Equal(Int512.NegativeOne, Int512.RotateRight(Int512.NegativeOne, 100));
        Assert.Equal(Int512.NegativeOne, Int512.RotateRight(Int512.NegativeOne, 511));
    }

    [Fact(DisplayName = "Int512.RotateLeft by 256 should swap halves")]
    public void Int512RotateLeftBy256ShouldSwapHalves()
    {
        Int512 value = new((UInt256)0x1111_2222_3333_4444UL, (UInt256)0x5555_6666_7777_8888UL);
        Int512 rotated = Int512.RotateLeft(value, 256);
        Assert.Equal(new Int512((UInt256)0x5555_6666_7777_8888UL, (UInt256)0x1111_2222_3333_4444UL), rotated);
    }

    [Fact(DisplayName = "Int512.RotateLeft then RotateRight by same amount should be identity")]
    public void Int512RotateLeftThenRightShouldBeIdentity()
    {
        Int512 value = Int512.Parse("-12345678901234567890");
        Int512 rotated = Int512.RotateRight(Int512.RotateLeft(value, 73), 73);
        Assert.Equal(value, rotated);
    }
}
