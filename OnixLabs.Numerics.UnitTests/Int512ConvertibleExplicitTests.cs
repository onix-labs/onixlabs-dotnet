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

public sealed class Int512ConvertibleExplicitTests
{
    [Fact(DisplayName = "Int512 to UInt512 unchecked should reinterpret bits and turn negatives into MaxValue-class large positives")]
    public void Int512ToUInt512ReinterpretsBits()
    {
        Assert.Equal(UInt512.MaxValue, (UInt512)Int512.NegativeOne);
        Assert.Equal(UInt512.Zero, (UInt512)Int512.Zero);
        Assert.Equal(UInt512.One, (UInt512)Int512.One);
    }

    [Fact(DisplayName = "UInt512 to Int512 unchecked should reinterpret bits and turn high values into negatives")]
    public void UInt512ToInt512ReinterpretsBits()
    {
        Assert.Equal(Int512.NegativeOne, (Int512)UInt512.MaxValue);
        Assert.Equal(Int512.Zero, (Int512)UInt512.Zero);
    }

    [Fact(DisplayName = "Int512 to UInt512 checked should throw for negative values")]
    public void Int512ToUInt512CheckedShouldThrowForNegative()
    {
        Assert.Throws<OverflowException>(() => checked((UInt512)Int512.NegativeOne));
        Assert.Throws<OverflowException>(() => checked((UInt512)Int512.MinValue));
    }

    [Fact(DisplayName = "Int512 to UInt512 checked should succeed for non-negative values")]
    public void Int512ToUInt512CheckedShouldSucceedForNonNegative()
    {
        Assert.Equal(UInt512.Zero, checked((UInt512)Int512.Zero));
        Assert.Equal(UInt512.One, checked((UInt512)Int512.One));
        UInt512 expected = new UInt512(Int512.MaxValue.UpperBits, Int512.MaxValue.LowerBits);
        Assert.Equal(expected, checked((UInt512)Int512.MaxValue));
    }

    [Fact(DisplayName = "UInt512 to Int512 checked should throw when high bit would interpret as negative")]
    public void UInt512ToInt512CheckedShouldThrowOnHighBit()
    {
        UInt512 source = (UInt512)Int512.MaxValue + UInt512.One;
        Assert.Throws<OverflowException>(() => checked((Int512)source));
    }

    [Fact(DisplayName = "UInt512 to Int512 checked should succeed when value fits in the signed range")]
    public void UInt512ToInt512CheckedShouldSucceedWhenInRange()
    {
        UInt512 source = new(Int512.MaxValue.UpperBits, Int512.MaxValue.LowerBits);
        Assert.Equal(Int512.MaxValue, checked((Int512)source));
    }

    [Fact(DisplayName = "Int512 from BigInteger unchecked should truncate to low 512 bits")]
    public void Int512FromBigIntegerUncheckedShouldTruncate()
    {
        BigInteger source = (BigInteger.One << 800) | BigInteger.One;
        Int512 result = (Int512)source;
        Assert.Equal(Int512.One, result);
    }

    [Fact(DisplayName = "Int512 from BigInteger checked should throw for over-large positive value")]
    public void Int512FromBigIntegerCheckedShouldThrowOnPositiveOverflow()
    {
        BigInteger source = BigInteger.Pow(2, 511);
        Assert.Throws<OverflowException>(() => checked((Int512)source));
    }

    [Fact(DisplayName = "Int512 from BigInteger checked should throw for under-large negative value")]
    public void Int512FromBigIntegerCheckedShouldThrowOnNegativeOverflow()
    {
        BigInteger source = -(BigInteger.Pow(2, 511) + BigInteger.One);
        Assert.Throws<OverflowException>(() => checked((Int512)source));
    }

    [Fact(DisplayName = "Int512 BigInteger round-trip should preserve large negative values")]
    public void Int512BigIntegerRoundTripShouldPreserveLargeNegatives()
    {
        BigInteger source = -BigInteger.Pow(2, 400);
        Int512 value = (Int512)source;
        Assert.Equal(source, (BigInteger)value);
    }

    [Fact(DisplayName = "Int512 narrowing to byte unchecked should match low bits")]
    public void Int512NarrowingToByteShouldMatchLowBits()
    {
        Int512 value = (Int512)0x1234ABCDUL;
        Assert.Equal((byte)0xCD, (byte)value);
    }

    [Fact(DisplayName = "Int512 narrowing to byte checked should throw for negative values")]
    public void Int512NarrowingToByteCheckedShouldThrowForNegative()
    {
        Assert.Throws<OverflowException>(() => checked((byte)Int512.NegativeOne));
    }

    [Fact(DisplayName = "Int512 narrowing to byte checked should throw for over-large values")]
    public void Int512NarrowingToByteCheckedShouldThrowForOverLarge()
    {
        Assert.Throws<OverflowException>(() => checked((byte)((Int512)256)));
    }

    [Fact(DisplayName = "Int512 narrowing to sbyte checked should accept in-range")]
    public void Int512NarrowingToSByteCheckedShouldAcceptInRange()
    {
        Assert.Equal((sbyte)(-42), checked((sbyte)(Int512)(-42)));
        Assert.Equal(sbyte.MaxValue, checked((sbyte)(Int512)sbyte.MaxValue));
    }

    [Fact(DisplayName = "Int512 narrowing to sbyte checked should throw on overflow")]
    public void Int512NarrowingToSByteCheckedShouldThrowOnOverflow()
    {
        Assert.Throws<OverflowException>(() => checked((sbyte)Int512.MaxValue));
        Assert.Throws<OverflowException>(() => checked((sbyte)Int512.MinValue));
    }

    [Fact(DisplayName = "Int512 narrowing to long checked should accept in-range")]
    public void Int512NarrowingToLongCheckedShouldAcceptInRange()
    {
        Assert.Equal(long.MinValue, checked((long)(Int512)long.MinValue));
        Assert.Equal(long.MaxValue, checked((long)(Int512)long.MaxValue));
    }

    [Fact(DisplayName = "Int512 narrowing to long checked should throw on overflow")]
    public void Int512NarrowingToLongCheckedShouldThrowOnOverflow()
    {
        Assert.Throws<OverflowException>(() => checked((long)Int512.MaxValue));
        Assert.Throws<OverflowException>(() => checked((long)Int512.MinValue));
    }

    [Fact(DisplayName = "Int512 narrowing to Int256 checked should throw on overflow")]
    public void Int512NarrowingToInt256CheckedShouldThrowOnOverflow()
    {
        Int512 source = (Int512)Int256.MaxValue + Int512.One;
        Assert.Throws<OverflowException>(() => checked((Int256)source));
    }

    [Fact(DisplayName = "Int512 narrowing to Int256 unchecked of small negative should reinterpret correctly")]
    public void Int512NarrowingToInt256UncheckedOfSmallNegativeShouldRoundTrip()
    {
        Int512 source = (Int512)(-42);
        Int256 narrowed = (Int256)source;
        Assert.Equal((Int256)(-42), narrowed);
    }

    [Fact(DisplayName = "Int512 to double of zero should be zero")]
    public void Int512ToDoubleZeroShouldBeZero()
    {
        Assert.Equal(0.0, (double)Int512.Zero);
    }

    [Fact(DisplayName = "Int512 to double of NegativeOne should be -1.0")]
    public void Int512ToDoubleNegativeOneShouldBeNegativeOne()
    {
        Assert.Equal(-1.0, (double)Int512.NegativeOne);
    }

    [Fact(DisplayName = "Int512 to decimal should preserve small values")]
    public void Int512ToDecimalShouldPreserveSmallValues()
    {
        Assert.Equal(-12345m, (decimal)((Int512)(-12345)));
    }

    [Fact(DisplayName = "Int512 to Float128 should round-trip small integer values")]
    public void Int512ToFloat128ShouldRoundTripSmallIntegers()
    {
        Int512 value = (Int512)(-42);
        Float128 wide = (Float128)value;
        Assert.Equal((Float128)(-42), wide);
    }

    [Fact(DisplayName = "Int512 from Float128 truncates fractional values")]
    public void Int512FromFloat128TruncatesFractional()
    {
        Float128 source = (Float128)(-123.7);
        Assert.Equal((Int512)(-123), (Int512)source);
    }

    [Fact(DisplayName = "Int512 unchecked from Float128 NaN should be zero")]
    public void Int512UncheckedFromFloat128NaNShouldBeZero()
    {
        Assert.Equal(Int512.Zero, (Int512)Float128.NaN);
    }

    [Fact(DisplayName = "Int512 checked from Float128 NaN should throw")]
    public void Int512CheckedFromFloat128NaNShouldThrow()
    {
        Assert.Throws<OverflowException>(() => checked((Int512)Float128.NaN));
    }

    [Fact(DisplayName = "Int512 checked from Float128 infinity should throw")]
    public void Int512CheckedFromFloat128InfinityShouldThrow()
    {
        Assert.Throws<OverflowException>(() => checked((Int512)Float128.PositiveInfinity));
        Assert.Throws<OverflowException>(() => checked((Int512)Float128.NegativeInfinity));
    }
}
