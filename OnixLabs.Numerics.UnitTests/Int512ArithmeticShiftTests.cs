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

public sealed class Int512ArithmeticShiftTests
{
    [Theory(DisplayName = "Int512 shift left of One should match BigInteger across valid positions")]
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
    [InlineData(510)]
    public void Int512ShiftLeftShouldMatchBigInteger(int amount)
    {
        Int512 value = Int512.One;
        Int512 shifted = value << amount;
        BigInteger mask = (BigInteger.One << 512) - BigInteger.One;
        BigInteger expectedUnsigned = (BigInteger.One << amount) & mask;
        // Compare via UInt512 bit pattern because Int512 interprets the top bit as sign.
        UInt512 expectedBits = (UInt512)expectedUnsigned;
        Assert.Equal(expectedBits, (UInt512)shifted);
    }

    [Fact(DisplayName = "Int512 shift left of One by 511 should equal MinValue (sign bit set)")]
    public void Int512ShiftLeftBy511ShouldEqualMinValue()
    {
        Assert.Equal(Int512.MinValue, Int512.One << 511);
    }

    [Fact(DisplayName = "Int512 shift left by 512 should be identity (mod 512)")]
    public void Int512ShiftLeftBy512ShouldBeIdentity()
    {
        Int512 value = Int512.Parse("12345");
        Assert.Equal(value, value << 512);
    }

    [Fact(DisplayName = "Int512 arithmetic right shift of a negative value preserves the sign")]
    public void Int512ArithmeticRightShiftShouldPreserveSign()
    {
        Int512 negative = Int512.NegativeOne;
        Assert.Equal(Int512.NegativeOne, negative >> 10);
        Assert.Equal(Int512.NegativeOne, negative >> 256);
        Assert.Equal(Int512.NegativeOne, negative >> 511);
    }

    [Fact(DisplayName = "Int512 arithmetic right shift of a positive value should not become negative")]
    public void Int512ArithmeticRightShiftOfPositiveShouldStayPositive()
    {
        Int512 positive = Int512.MaxValue;
        Int512 shifted = positive >> 1;
        Assert.False(Int512.IsNegative(shifted));
        Assert.Equal((BigInteger)positive >> 1, (BigInteger)shifted);
    }

    [Fact(DisplayName = "Int512 logical right shift of a negative value zero-fills the high bits")]
    public void Int512LogicalRightShiftOfNegativeShouldZeroFill()
    {
        Int512 negative = Int512.NegativeOne;
        Int512 shifted = negative >>> 1;
        Assert.False(Int512.IsNegative(shifted));
        Assert.Equal(Int512.MaxValue, shifted);
    }

    [Fact(DisplayName = "Int512 logical right shift by 511 should isolate the sign bit")]
    public void Int512LogicalRightShiftBy511ShouldIsolateSignBit()
    {
        Int512 negative = Int512.NegativeOne;
        Int512 shifted = negative >>> 511;
        Assert.Equal(Int512.One, shifted);
    }

    [Fact(DisplayName = "Int512 arithmetic right shift across the half boundary preserves sign")]
    public void Int512ArithmeticRightShiftAcrossHalfBoundaryShouldPreserveSign()
    {
        Int512 negative = Int512.NegativeOne;
        Int512 shifted = negative >> 300;
        Assert.True(Int512.IsNegative(shifted));
        Assert.Equal(Int512.NegativeOne, shifted);
    }

    [Fact(DisplayName = "Int512 shift left then arithmetic right of a positive value should round-trip")]
    public void Int512LeftThenRightArithmeticShouldRoundTripForPositive()
    {
        Int512 value = (Int512)0x1234_5678UL;
        Int512 result = (value << 100) >> 100;
        Assert.Equal(value, result);
    }

    [Fact(DisplayName = "Int512 shift left by zero should be identity")]
    public void Int512ShiftLeftByZeroShouldBeIdentity()
    {
        Int512 value = Int512.NegativeOne;
        Assert.Equal(value, value << 0);
        Assert.Equal(value, value >> 0);
        Assert.Equal(value, value >>> 0);
    }
}
