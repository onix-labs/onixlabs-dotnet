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

public sealed class Int256ArithmeticShiftTests
{
    [Theory(DisplayName = "Int256 left shift of One by various positions should match BigInteger")]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(64)]
    [InlineData(127)]
    [InlineData(128)]
    [InlineData(129)]
    [InlineData(192)]
    [InlineData(254)]
    public void Int256LeftShiftOfOneShouldMatchBigInteger(int shift)
    {
        Int256 actual = Int256.One << shift;
        BigInteger expected = BigInteger.One << shift;
        Assert.Equal(expected, (BigInteger)actual);
    }

    [Fact(DisplayName = "Int256 left shift of One by 255 should produce MinValue (sign bit)")]
    public void Int256LeftShiftBy255ShouldProduceMinValue()
    {
        Int256 actual = Int256.One << 255;
        Assert.Equal(Int256.MinValue, actual);
    }

    [Fact(DisplayName = "Int256 arithmetic right shift of NegativeOne should preserve all bits set")]
    public void Int256ArithmeticRightShiftOfNegativeOneShouldPreserveAllBitsSet()
    {
        for (int shift = 0; shift < 256; shift++)
        {
            Assert.Equal(Int256.NegativeOne, Int256.NegativeOne >> shift);
        }
    }

    [Fact(DisplayName = "Int256 arithmetic right shift of negative value should sign-extend the upper half")]
    public void Int256ArithmeticRightShiftOfNegativeShouldSignExtend()
    {
        Int256 negative = Int256.MinValue;
        Int256 shifted = negative >> 1;
        // -2^255 >> 1 = -2^254
        Assert.Equal(-(BigInteger.One << 254), (BigInteger)shifted);
        Assert.True(Int256.IsNegative(shifted));
    }

    [Fact(DisplayName = "Int256 arithmetic right shift of positive should not sign-extend")]
    public void Int256ArithmeticRightShiftOfPositiveShouldNotSignExtend()
    {
        Int256 positive = Int256.MaxValue;
        Int256 shifted = positive >> 1;
        Assert.True(Int256.IsPositive(shifted));
        Assert.Equal(((BigInteger.One << 255) - BigInteger.One) >> 1, (BigInteger)shifted);
    }

    [Fact(DisplayName = "Int256 logical right shift of NegativeOne should fill with zeros")]
    public void Int256LogicalRightShiftOfNegativeOneShouldFillWithZeros()
    {
        Int256 shifted = Int256.NegativeOne >>> 1;
        Assert.False(Int256.IsNegative(shifted));
        Assert.Equal(Int256.MaxValue, shifted);
    }

    [Fact(DisplayName = "Int256 logical right shift of MinValue by 1 should produce a positive MSB-clear value")]
    public void Int256LogicalRightShiftOfMinValueShouldProducePositive()
    {
        Int256 shifted = Int256.MinValue >>> 1;
        Assert.False(Int256.IsNegative(shifted));
        Assert.Equal(BigInteger.One << 254, (BigInteger)shifted);
    }

    [Fact(DisplayName = "Int256 left shift by 128 should move lower into upper")]
    public void Int256LeftShiftBy128ShouldMoveLowerIntoUpper()
    {
        Int256 value = new(UInt128.Zero, UInt128.MaxValue);
        Int256 shifted = value << 128;
        Assert.Equal(UInt128.MaxValue, shifted.UpperBits);
        Assert.Equal(UInt128.Zero, shifted.LowerBits);
    }

    [Fact(DisplayName = "Int256 arithmetic right shift by 128 of a negative should fill upper with all-ones and move upper to lower")]
    public void Int256ArithmeticRightShiftBy128OfNegativeShouldSignExtend()
    {
        Int256 negative = new(UInt128.MaxValue, UInt128.Zero);  // negative with all-ones upper
        Int256 shifted = negative >> 128;
        Assert.Equal(UInt128.MaxValue, shifted.UpperBits);
        Assert.Equal(UInt128.MaxValue, shifted.LowerBits);
    }

    [Fact(DisplayName = "Int256 logical right shift by 128 of a negative should fill upper with zeros")]
    public void Int256LogicalRightShiftBy128OfNegativeShouldFillWithZeros()
    {
        Int256 negative = new(UInt128.MaxValue, UInt128.Zero);
        Int256 shifted = negative >>> 128;
        Assert.Equal(UInt128.Zero, shifted.UpperBits);
        Assert.Equal(UInt128.MaxValue, shifted.LowerBits);
    }

    [Fact(DisplayName = "Int256 shift by zero should return the value unchanged")]
    public void Int256ShiftByZeroShouldReturnValueUnchanged()
    {
        Int256 value = Int256.MaxValue;
        Assert.Equal(value, value << 0);
        Assert.Equal(value, value >> 0);
        Assert.Equal(value, value >>> 0);
    }

    [Fact(DisplayName = "Int256 left shift by full BitWidth should return value unchanged (mod 256)")]
    public void Int256LeftShiftByFullBitWidthShouldReturnValueUnchanged()
    {
        Int256 value = Int256.MaxValue;
        Assert.Equal(value, value << 256);
    }
}
