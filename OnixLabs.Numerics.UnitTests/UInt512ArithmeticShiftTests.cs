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

public sealed class UInt512ArithmeticShiftTests
{
    [Theory(DisplayName = "UInt512 shift left by valid positions should match BigInteger")]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(63)]
    [InlineData(64)]
    [InlineData(127)]
    [InlineData(128)]
    [InlineData(255)]
    [InlineData(256)]
    [InlineData(257)]
    [InlineData(384)]
    [InlineData(511)]
    public void UInt512ShiftLeftShouldMatchBigInteger(int amount)
    {
        UInt512 value = UInt512.One;
        UInt512 shifted = value << amount;
        BigInteger mask = (BigInteger.One << 512) - BigInteger.One;
        BigInteger expected = (BigInteger.One << amount) & mask;
        Assert.Equal(expected, (BigInteger)shifted);
    }

    [Theory(DisplayName = "UInt512 logical right shift should match BigInteger for valid positions")]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(63)]
    [InlineData(64)]
    [InlineData(127)]
    [InlineData(128)]
    [InlineData(255)]
    [InlineData(256)]
    [InlineData(257)]
    [InlineData(384)]
    [InlineData(511)]
    public void UInt512LogicalRightShiftShouldMatchBigInteger(int amount)
    {
        UInt512 value = UInt512.MaxValue;
        UInt512 shifted = value >>> amount;
        BigInteger expected = ((BigInteger.One << 512) - BigInteger.One) >> amount;
        Assert.Equal(expected, (BigInteger)shifted);
    }

    [Fact(DisplayName = "UInt512 shift right operator (>>) should be identical to logical >>> for unsigned values")]
    public void UInt512ShiftRightShouldBeIdenticalToLogicalRight()
    {
        UInt512 value = UInt512.MaxValue;
        Assert.Equal(value >>> 100, value >> 100);
        Assert.Equal(value >>> 256, value >> 256);
        Assert.Equal(value >>> 511, value >> 511);
    }

    [Fact(DisplayName = "UInt512 shift by zero should be the identity")]
    public void UInt512ShiftByZeroShouldBeIdentity()
    {
        UInt512 value = UInt512.Parse("123456789012345678901234567890");
        Assert.Equal(value, value << 0);
        Assert.Equal(value, value >> 0);
        Assert.Equal(value, value >>> 0);
    }

    [Fact(DisplayName = "UInt512 shift left by amount mod 512 should be the same value for multiples of 512")]
    public void UInt512ShiftLeftByMultiplesOf512ShouldBeIdentity()
    {
        UInt512 value = UInt512.Parse("123456789012345678901234567890");
        Assert.Equal(value, value << 512);
        Assert.Equal(value << 1, value << 513);
    }

    [Fact(DisplayName = "UInt512 shift left by amounts past 512 should wrap modulo 512")]
    public void UInt512ShiftLeftLargeAmountsWrapModulo512()
    {
        UInt512 value = UInt512.One;
        // 768 mod 512 = 256
        Assert.Equal(value << 256, value << 768);
        // 1023 mod 512 = 511
        Assert.Equal(value << 511, value << 1023);
    }

    [Fact(DisplayName = "UInt512 right shift by amounts past 512 should wrap modulo 512")]
    public void UInt512RightShiftLargeAmountsWrapModulo512()
    {
        UInt512 value = UInt512.MaxValue;
        Assert.Equal(value >>> 0, value >>> 512);
        Assert.Equal(value >>> 256, value >>> 768);
        Assert.Equal(value >>> 511, value >>> 1023);
    }

    [Fact(DisplayName = "UInt512.One shifted left by 511 should have just the highest bit set")]
    public void UInt512OneShiftedBy511ShouldHaveOnlyTopBitSet()
    {
        UInt512 result = UInt512.One << 511;
        Assert.Equal((UInt512)1UL, UInt512.PopCount(result));
        Assert.Equal((UInt512)511UL, UInt512.Log2(result));
    }

    [Fact(DisplayName = "UInt512.MaxValue shifted right by 511 should be one")]
    public void UInt512MaxValueShiftedRightBy511ShouldBeOne()
    {
        Assert.Equal(UInt512.One, UInt512.MaxValue >>> 511);
    }

    [Fact(DisplayName = "UInt512 left shift should clear lower bits when crossing half boundary")]
    public void UInt512LeftShiftAcrossHalfBoundaryShouldClearLowerBits()
    {
        UInt512 value = new(UInt256.Zero, UInt256.MaxValue);
        UInt512 shifted = value << 256;
        Assert.Equal(new UInt512(UInt256.MaxValue, UInt256.Zero), shifted);
    }

    [Fact(DisplayName = "UInt512 right shift should clear upper bits when crossing half boundary")]
    public void UInt512RightShiftAcrossHalfBoundaryShouldClearUpperBits()
    {
        UInt512 value = new(UInt256.MaxValue, UInt256.Zero);
        UInt512 shifted = value >>> 256;
        Assert.Equal(new UInt512(UInt256.Zero, UInt256.MaxValue), shifted);
    }

    [Fact(DisplayName = "UInt512 round-trip of shift left then right should restore the lower bits")]
    public void UInt512RoundTripShiftLeftThenRightShouldRestoreLowerBits()
    {
        UInt512 value = (UInt512)0xDEAD_BEEF_CAFE_BABEUL;
        UInt512 result = (value << 100) >>> 100;
        Assert.Equal(value, result);
    }

    [Fact(DisplayName = "UInt512 shift left of MaxValue by 1 should discard the topmost bit")]
    public void UInt512MaxValueShiftLeftBy1ShouldDiscardTopBit()
    {
        UInt512 result = UInt512.MaxValue << 1;
        Assert.Equal(UInt512.MaxValue - UInt512.One, result);
    }
}
