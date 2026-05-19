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

public sealed class Int256ArithmeticDivisionTests
{
    [Fact(DisplayName = "Int256 division by one should return the dividend")]
    public void Int256DivisionByOneShouldReturnDividend()
    {
        Int256 value = (Int256)(-12345);
        Assert.Equal(value, value / Int256.One);
    }

    [Fact(DisplayName = "Int256 division of value by itself should return one")]
    public void Int256DivisionOfValueByItselfShouldReturnOne()
    {
        Int256 value = (Int256)(-99999);
        Assert.Equal(Int256.One, value / value);
    }

    [Fact(DisplayName = "Int256 division by zero should throw DivideByZeroException")]
    public void Int256DivisionByZeroShouldThrow()
    {
        Assert.Throws<DivideByZeroException>(() => Int256.One / Int256.Zero);
        Assert.Throws<DivideByZeroException>(() => Int256.MinValue / Int256.Zero);
    }

    [Fact(DisplayName = "Int256.MinValue divided by NegativeOne should throw OverflowException")]
    public void Int256MinValueDividedByNegativeOneShouldThrow()
    {
        Assert.Throws<OverflowException>(() => Int256.MinValue / Int256.NegativeOne);
        Assert.Throws<OverflowException>(() => Int256.DivRem(Int256.MinValue, Int256.NegativeOne));
    }

    [Fact(DisplayName = "Int256 division should truncate toward zero for mixed-sign operands")]
    public void Int256DivisionShouldTruncateTowardZero()
    {
        Assert.Equal((Int256)(-3), (Int256)10 / (Int256)(-3));
        Assert.Equal((Int256)(-3), (Int256)(-10) / (Int256)3);
        Assert.Equal((Int256)3, (Int256)(-10) / (Int256)(-3));
        Assert.Equal((Int256)3, (Int256)10 / (Int256)3);
    }

    [Fact(DisplayName = "Int256 modulus should take sign of dividend")]
    public void Int256ModulusShouldTakeSignOfDividend()
    {
        Assert.Equal((Int256)1, (Int256)10 % (Int256)3);
        Assert.Equal((Int256)(-1), (Int256)(-10) % (Int256)3);
        Assert.Equal((Int256)1, (Int256)10 % (Int256)(-3));
        Assert.Equal((Int256)(-1), (Int256)(-10) % (Int256)(-3));
    }

    [Fact(DisplayName = "Int256.DivRem should return both quotient and remainder")]
    public void Int256DivRemShouldReturnBothQuotientAndRemainder()
    {
        (Int256 q, Int256 r) = Int256.DivRem((Int256)17, (Int256)5);
        Assert.Equal((Int256)3, q);
        Assert.Equal((Int256)2, r);
    }

    [Fact(DisplayName = "Int256.DivRem with negative dividend should follow truncation rules")]
    public void Int256DivRemWithNegativeDividendShouldTruncate()
    {
        (Int256 q, Int256 r) = Int256.DivRem((Int256)(-17), (Int256)5);
        Assert.Equal((Int256)(-3), q);
        Assert.Equal((Int256)(-2), r);
    }

    [Theory(DisplayName = "Int256 division should match BigInteger oracle for random cross-checks")]
    [InlineData("123456789012345678901", "9876543210")]
    [InlineData("-123456789012345678901", "9876543210")]
    [InlineData("123456789012345678901", "-9876543210")]
    [InlineData("-123456789012345678901", "-9876543210")]
    [InlineData("57896044618658097711785492504343953926634992332820282019728792003956564819967", "2")]
    public void Int256DivisionShouldMatchBigIntegerOracle(string dividendText, string divisorText)
    {
        Int256 dividend = Int256.Parse(dividendText);
        Int256 divisor = Int256.Parse(divisorText);
        Int256 q = dividend / divisor;
        Int256 r = dividend % divisor;
        BigInteger expectedQ = (BigInteger)dividend / (BigInteger)divisor;
        BigInteger expectedR = (BigInteger)dividend % (BigInteger)divisor;
        Assert.Equal(expectedQ, (BigInteger)q);
        Assert.Equal(expectedR, (BigInteger)r);
    }

    [Fact(DisplayName = "Int256 division of MaxValue by NegativeOne should equal negative MaxValue")]
    public void Int256DivisionMaxValueByNegativeOneShouldEqualNegativeMaxValue()
    {
        Int256 result = Int256.MaxValue / Int256.NegativeOne;
        Assert.Equal(-Int256.MaxValue, result);
        Assert.Equal(Int256.MinValue + Int256.One, result);
    }

    [Fact(DisplayName = "Int256 modulus by one should return zero")]
    public void Int256ModulusByOneShouldReturnZero()
    {
        Int256 value = Int256.Parse("9999999999999999999999");
        Assert.Equal(Int256.Zero, value % Int256.One);
    }
}
