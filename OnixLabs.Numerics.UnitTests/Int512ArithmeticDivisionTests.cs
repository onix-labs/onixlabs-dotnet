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

public sealed class Int512ArithmeticDivisionTests
{
    [Fact(DisplayName = "Int512 division by one should return dividend")]
    public void Int512DivisionByOneShouldReturnDividend()
    {
        Int512 value = Int512.Parse("-123456789012345678901234567890");
        Assert.Equal(value, value / Int512.One);
    }

    [Fact(DisplayName = "Int512 division by NegativeOne should negate the dividend")]
    public void Int512DivisionByNegativeOneShouldNegate()
    {
        Assert.Equal(Int512.NegativeOne, Int512.One / Int512.NegativeOne);
        Assert.Equal((Int512)(-10), (Int512)10 / Int512.NegativeOne);
        Assert.Equal((Int512)42, (Int512)(-42) / Int512.NegativeOne);
    }

    [Fact(DisplayName = "Int512 MinValue divided by NegativeOne should throw")]
    public void Int512MinValueDividedByNegativeOneShouldThrow()
    {
        Assert.Throws<OverflowException>(() => Int512.MinValue / Int512.NegativeOne);
    }

    [Fact(DisplayName = "Int512 division by zero should throw DivideByZeroException")]
    public void Int512DivisionByZeroShouldThrow()
    {
        Assert.Throws<DivideByZeroException>(() => Int512.One / Int512.Zero);
        Assert.Throws<DivideByZeroException>(() => Int512.NegativeOne / Int512.Zero);
        Assert.Throws<DivideByZeroException>(() => Int512.MaxValue / Int512.Zero);
    }

    [Fact(DisplayName = "Int512 division should truncate toward zero for positive dividends")]
    public void Int512DivisionShouldTruncateTowardZeroPositive()
    {
        Assert.Equal((Int512)3, (Int512)10 / (Int512)3);
    }

    [Fact(DisplayName = "Int512 division should truncate toward zero for negative dividends")]
    public void Int512DivisionShouldTruncateTowardZeroNegative()
    {
        // -10 / 3 = -3 in truncation-toward-zero
        Assert.Equal((Int512)(-3), (Int512)(-10) / (Int512)3);
    }

    [Fact(DisplayName = "Int512 division of two negatives should produce positive quotient")]
    public void Int512DivisionOfTwoNegativesShouldBePositive()
    {
        Int512 a = (Int512)(-100);
        Int512 b = (Int512)(-7);
        Assert.Equal((Int512)14, a / b);
    }

    [Fact(DisplayName = "Int512 division should match BigInteger for large operands")]
    public void Int512DivisionShouldMatchBigInteger()
    {
        Int512 dividend = Int512.Parse("123456789012345678901234567890123456789012345678901234567890");
        Int512 divisor = Int512.Parse("-987654321098765432109876543210");
        Int512 quotient = dividend / divisor;
        Assert.Equal((BigInteger)dividend / (BigInteger)divisor, (BigInteger)quotient);
    }

    [Fact(DisplayName = "Int512 modulus should preserve sign of dividend (positive case)")]
    public void Int512ModulusPositiveDividendShouldHavePositiveRemainder()
    {
        Assert.Equal((Int512)1, (Int512)10 % (Int512)3);
    }

    [Fact(DisplayName = "Int512 modulus should preserve sign of dividend (negative case)")]
    public void Int512ModulusNegativeDividendShouldHaveNegativeRemainder()
    {
        Assert.Equal((Int512)(-1), (Int512)(-10) % (Int512)3);
        Assert.Equal((Int512)(-1), (Int512)(-10) % (Int512)(-3));
    }

    [Fact(DisplayName = "Int512 modulus by zero should throw")]
    public void Int512ModulusByZeroShouldThrow()
    {
        Assert.Throws<DivideByZeroException>(() => Int512.One % Int512.Zero);
    }

    [Fact(DisplayName = "Int512.DivRem should match BigInteger.DivRem for two negatives")]
    public void Int512DivRemTwoNegativesShouldMatchBigInteger()
    {
        Int512 a = (Int512)(-100);
        Int512 b = (Int512)(-7);
        (Int512 q, Int512 r) = Int512.DivRem(a, b);
        BigInteger bigQuot = BigInteger.DivRem(-100, -7, out BigInteger bigRem);
        Assert.Equal(bigQuot, (BigInteger)q);
        Assert.Equal(bigRem, (BigInteger)r);
    }

    [Fact(DisplayName = "Int512.DivRem should match BigInteger.DivRem for negative dividend, positive divisor")]
    public void Int512DivRemNegativePositiveShouldMatchBigInteger()
    {
        Int512 a = (Int512)(-100);
        Int512 b = (Int512)7;
        (Int512 q, Int512 r) = Int512.DivRem(a, b);
        BigInteger bigQuot = BigInteger.DivRem(-100, 7, out BigInteger bigRem);
        Assert.Equal(bigQuot, (BigInteger)q);
        Assert.Equal(bigRem, (BigInteger)r);
    }

    [Fact(DisplayName = "Int512.DivRem(MinValue, NegativeOne) should throw")]
    public void Int512DivRemMinValueByNegativeOneShouldThrow()
    {
        Assert.Throws<OverflowException>(() => Int512.DivRem(Int512.MinValue, Int512.NegativeOne));
    }

    [Fact(DisplayName = "Int512.DivRem by zero should throw")]
    public void Int512DivRemByZeroShouldThrow()
    {
        Assert.Throws<DivideByZeroException>(() => Int512.DivRem(Int512.One, Int512.Zero));
    }

    [Fact(DisplayName = "Int512.DivRem of Zero divided by any non-zero should produce (Zero, Zero)")]
    public void Int512DivRemOfZeroShouldProduceZeros()
    {
        (Int512 q, Int512 r) = Int512.DivRem(Int512.Zero, (Int512)(-99));
        Assert.Equal(Int512.Zero, q);
        Assert.Equal(Int512.Zero, r);
    }

    [Fact(DisplayName = "Int512 division identity quotient * divisor + remainder = dividend")]
    public void Int512DivisionIdentityShouldHold()
    {
        Int512 dividend = Int512.Parse("-9876543210987654321098765432109876543210");
        Int512 divisor = Int512.Parse("123456789012345");
        (Int512 q, Int512 r) = Int512.DivRem(dividend, divisor);
        Assert.Equal(dividend, q * divisor + r);
    }
}
