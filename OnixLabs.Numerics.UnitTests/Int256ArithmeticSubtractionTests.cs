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

public sealed class Int256ArithmeticSubtractionTests
{
    [Fact(DisplayName = "Int256 subtraction of value minus zero should return the value")]
    public void Int256SubtractionOfValueMinusZeroShouldReturnValue()
    {
        Int256 value = (Int256)(-9999);
        Assert.Equal(value, value - Int256.Zero);
    }

    [Fact(DisplayName = "Int256 subtraction of value minus itself should return zero")]
    public void Int256SubtractionOfValueMinusItselfShouldReturnZero()
    {
        Int256 value = Int256.Parse("-12345678901234567890");
        Assert.Equal(Int256.Zero, value - value);
        Assert.Equal(Int256.Zero, Int256.MaxValue - Int256.MaxValue);
        Assert.Equal(Int256.Zero, Int256.MinValue - Int256.MinValue);
    }

    [Fact(DisplayName = "Int256 subtraction positive minus negative should be addition of magnitudes")]
    public void Int256SubtractionPositiveMinusNegativeShouldBeAdditionOfMagnitudes()
    {
        Int256 result = (Int256)100 - (Int256)(-50);
        Assert.Equal((Int256)150, result);
    }

    [Fact(DisplayName = "Int256 subtraction zero minus value should be negation")]
    public void Int256SubtractionZeroMinusValueShouldBeNegation()
    {
        Int256 value = (Int256)42;
        Assert.Equal(-value, Int256.Zero - value);
        Assert.Equal((Int256)(-42), Int256.Zero - value);
    }

    [Fact(DisplayName = "Int256 unchecked subtraction MinValue minus One should wrap to MaxValue")]
    public void Int256UncheckedSubtractionMinMinusOneShouldWrapToMax()
    {
        Assert.Equal(Int256.MaxValue, Int256.MinValue - Int256.One);
    }

    [Fact(DisplayName = "Int256 unchecked subtraction MaxValue minus NegativeOne should wrap to MinValue")]
    public void Int256UncheckedSubtractionMaxMinusNegativeOneShouldWrapToMin()
    {
        Assert.Equal(Int256.MinValue, Int256.MaxValue - Int256.NegativeOne);
    }

    [Fact(DisplayName = "Int256 checked subtraction negative overflow should throw")]
    public void Int256CheckedSubtractionNegativeOverflowShouldThrow()
    {
        Assert.Throws<OverflowException>(() => checked(Int256.MinValue - Int256.One));
        Assert.Throws<OverflowException>(() => checked(Int256.MinValue - Int256.MaxValue));
    }

    [Fact(DisplayName = "Int256 checked subtraction positive overflow should throw")]
    public void Int256CheckedSubtractionPositiveOverflowShouldThrow()
    {
        Assert.Throws<OverflowException>(() => checked(Int256.MaxValue - Int256.NegativeOne));
        Assert.Throws<OverflowException>(() => checked(Int256.MaxValue - Int256.MinValue));
    }

    [Fact(DisplayName = "Int256 checked subtraction of same-sign values should not overflow")]
    public void Int256CheckedSubtractionOfSameSignValuesShouldNotOverflow()
    {
        Assert.Equal((Int256)50, checked((Int256)100 - (Int256)50));
        Assert.Equal((Int256)(-50), checked((Int256)(-100) - (Int256)(-50)));
    }

    [Fact(DisplayName = "Int256 subtraction should match BigInteger oracle for mixed-sign large values")]
    public void Int256SubtractionShouldMatchBigIntegerOracleForLargeValues()
    {
        Int256 left = Int256.Parse("12345678901234567890123456789");
        Int256 right = Int256.Parse("-98765432109876543210987654321");
        Int256 actual = left - right;
        BigInteger expected = (BigInteger)left - (BigInteger)right;
        Assert.Equal(expected, (BigInteger)actual);
    }
}
