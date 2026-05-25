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

public sealed class Int256ArithmeticAdditionTests
{
    [Fact(DisplayName = "Int256 addition of value plus zero should return the value")]
    public void Int256AdditionOfValuePlusZeroShouldReturnValue()
    {
        Int256 value = (Int256)(-12345);
        Assert.Equal(value, value + Int256.Zero);
        Assert.Equal(value, Int256.Zero + value);
    }

    [Fact(DisplayName = "Int256 addition of positive plus negative should be subtraction")]
    public void Int256AdditionOfPositiveAndNegativeShouldBeSubtraction()
    {
        Int256 result = (Int256)100 + (Int256)(-30);
        Assert.Equal((Int256)70, result);
    }

    [Fact(DisplayName = "Int256 addition resulting in zero from opposite signs should be zero")]
    public void Int256AdditionOfOppositesShouldBeZero()
    {
        Int256 value = (Int256)9999;
        Assert.Equal(Int256.Zero, value + -value);
    }

    [Fact(DisplayName = "Int256 addition should match BigInteger oracle for mixed-sign large values")]
    public void Int256AdditionShouldMatchBigIntegerOracleForLargeValues()
    {
        Int256 left = Int256.Parse("-12345678901234567890123456789012345678901234567890");
        Int256 right = Int256.Parse("98765432109876543210987654321098765432109876543210");
        Int256 actual = left + right;
        BigInteger expected = (BigInteger)left + (BigInteger)right;
        Assert.Equal(expected, (BigInteger)actual);
    }

    [Fact(DisplayName = "Int256 unchecked addition MaxValue plus One should wrap to MinValue")]
    public void Int256UncheckedAdditionMaxPlusOneShouldWrapToMin()
    {
        Assert.Equal(Int256.MinValue, Int256.MaxValue + Int256.One);
    }

    [Fact(DisplayName = "Int256 unchecked addition MinValue plus NegativeOne should wrap to MaxValue")]
    public void Int256UncheckedAdditionMinPlusNegativeOneShouldWrapToMax()
    {
        Assert.Equal(Int256.MaxValue, Int256.MinValue + Int256.NegativeOne);
    }

    [Fact(DisplayName = "Int256 checked addition of positives in range should match unchecked")]
    public void Int256CheckedAdditionOfPositivesInRangeShouldMatchUnchecked()
    {
        Int256 left = (Int256)1000;
        Int256 right = (Int256)2000;
        Assert.Equal(left + right, checked(left + right));
    }

    [Fact(DisplayName = "Int256 checked addition positive overflow should throw")]
    public void Int256CheckedAdditionPositiveOverflowShouldThrow()
    {
        Assert.Throws<OverflowException>(() => checked(Int256.MaxValue + Int256.One));
        Assert.Throws<OverflowException>(() => checked(Int256.MaxValue + Int256.MaxValue));
    }

    [Fact(DisplayName = "Int256 checked addition negative overflow should throw")]
    public void Int256CheckedAdditionNegativeOverflowShouldThrow()
    {
        Assert.Throws<OverflowException>(() => checked(Int256.MinValue + Int256.NegativeOne));
        Assert.Throws<OverflowException>(() => checked(Int256.MinValue + Int256.MinValue));
    }

    [Fact(DisplayName = "Int256 checked addition of opposite signs should never overflow")]
    public void Int256CheckedAdditionOfOppositeSignsShouldNotOverflow()
    {
        Assert.Equal((Int256)0, checked((Int256)100 + (Int256)(-100)));
        Assert.Equal(Int256.NegativeOne, checked(Int256.MaxValue + Int256.MinValue));
    }

    [Fact(DisplayName = "Int256 addition should be commutative")]
    public void Int256AdditionShouldBeCommutative()
    {
        Int256 a = Int256.Parse("12345678901234567890");
        Int256 b = (Int256)(-9876543210);
        Assert.Equal(a + b, b + a);
    }
}
