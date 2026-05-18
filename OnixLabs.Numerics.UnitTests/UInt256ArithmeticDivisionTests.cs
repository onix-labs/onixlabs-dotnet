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

public sealed class UInt256ArithmeticDivisionTests
{
    [Fact(DisplayName = "UInt256 division by one should return the dividend")]
    public void UInt256DivisionByOneShouldReturnDividend()
    {
        UInt256 value = UInt256.Parse("123456789012345678901234567890");
        Assert.Equal(value, value / UInt256.One);
    }

    [Fact(DisplayName = "UInt256 division by zero should throw DivideByZeroException")]
    public void UInt256DivisionByZeroShouldThrow()
    {
        Assert.Throws<DivideByZeroException>(() => UInt256.One / UInt256.Zero);
        Assert.Throws<DivideByZeroException>(() => UInt256.MaxValue / UInt256.Zero);
    }

    [Fact(DisplayName = "UInt256 division of value by itself should return one")]
    public void UInt256DivisionOfValueByItselfShouldReturnOne()
    {
        UInt256 value = UInt256.Parse("987654321098765432109876543210");
        Assert.Equal(UInt256.One, value / value);
        Assert.Equal(UInt256.One, UInt256.MaxValue / UInt256.MaxValue);
    }

    [Fact(DisplayName = "UInt256 division of smaller by larger should return zero")]
    public void UInt256DivisionOfSmallerByLargerShouldReturnZero()
    {
        UInt256 small = (UInt256)5;
        UInt256 large = (UInt256)1000;
        Assert.Equal(UInt256.Zero, small / large);
    }

    [Fact(DisplayName = "UInt256.DivRem of zero by non-zero should return zero quotient and zero remainder")]
    public void UInt256DivRemOfZeroShouldReturnZeroAndZero()
    {
        (UInt256 q, UInt256 r) = UInt256.DivRem(UInt256.Zero, (UInt256)123);
        Assert.Equal(UInt256.Zero, q);
        Assert.Equal(UInt256.Zero, r);
    }

    [Fact(DisplayName = "UInt256.DivRem should match BigInteger oracle for large dividends and small divisors")]
    public void UInt256DivRemShouldMatchBigIntegerOracleForLargeDividends()
    {
        UInt256 dividend = UInt256.Parse("100000000000000000000000000000000000000000000");
        UInt256 divisor = (UInt256)123456789UL;
        (UInt256 q, UInt256 r) = UInt256.DivRem(dividend, divisor);
        BigInteger expectedQ = (BigInteger)dividend / (BigInteger)divisor;
        BigInteger expectedR = (BigInteger)dividend % (BigInteger)divisor;
        Assert.Equal(expectedQ, (BigInteger)q);
        Assert.Equal(expectedR, (BigInteger)r);
    }

    [Fact(DisplayName = "UInt256.DivRem with both upper halves nonzero should match BigInteger")]
    public void UInt256DivRemWithBothUpperHalvesNonzeroShouldMatchBigInteger()
    {
        UInt256 dividend = new(UInt128.MaxValue, UInt128.MaxValue);
        UInt256 divisor = new((UInt128)1234567, (UInt128)8901234UL);
        (UInt256 q, UInt256 r) = UInt256.DivRem(dividend, divisor);
        BigInteger expectedQ = (BigInteger)dividend / (BigInteger)divisor;
        BigInteger expectedR = (BigInteger)dividend % (BigInteger)divisor;
        Assert.Equal(expectedQ, (BigInteger)q);
        Assert.Equal(expectedR, (BigInteger)r);
    }

    [Theory(DisplayName = "UInt256.DivRem should match BigInteger oracle for random cross-checks")]
    [InlineData("18446744073709551616", "3")]
    [InlineData("340282366920938463463374607431768211456", "100000")]
    [InlineData("99999999999999999999999999999999999999999999999999999999999", "1234567890")]
    [InlineData("115792089237316195423570985008687907853269984665640564039457584007913129639935", "57896044618658097711785492504343953926634992332820282019728792003956564819968")]
    [InlineData("57896044618658097711785492504343953926634992332820282019728792003956564819968", "2")]
    public void UInt256DivRemShouldMatchBigIntegerOracleForRandomCases(string dividendText, string divisorText)
    {
        UInt256 dividend = UInt256.Parse(dividendText);
        UInt256 divisor = UInt256.Parse(divisorText);
        (UInt256 q, UInt256 r) = UInt256.DivRem(dividend, divisor);
        BigInteger expectedQ = (BigInteger)dividend / (BigInteger)divisor;
        BigInteger expectedR = (BigInteger)dividend % (BigInteger)divisor;
        Assert.Equal(expectedQ, (BigInteger)q);
        Assert.Equal(expectedR, (BigInteger)r);
    }

    [Fact(DisplayName = "UInt256.DivRemBy128 should produce correct quotient and remainder for the combined-quotient operator path")]
    public void UInt256DivRemBy128ShouldProduceCorrectResultForCombinedQuotientPath()
    {
        // DivRemBy128 returns only the low 128 bits of the quotient; the operator computes the
        // upper part separately as left.upper / divisor. Validate the combined result against
        // BigInteger.
        UInt256 dividend = new((UInt128)5, UInt128.MaxValue);
        UInt128 divisor = (UInt128)12345;
        UInt128 quotientLow = UInt256.DivRemBy128(dividend, divisor, out UInt128 remainder);
        UInt128 quotientHigh = dividend.Upper / divisor;
        BigInteger combined = ((BigInteger)quotientHigh << 128) | (BigInteger)quotientLow;
        BigInteger expected = (BigInteger)dividend / (BigInteger)divisor;
        Assert.Equal(expected, combined);
        Assert.Equal((BigInteger)dividend % (BigInteger)divisor, (BigInteger)remainder);
    }

    [Fact(DisplayName = "UInt256.DivRemBy128 should throw DivideByZeroException when divisor is zero")]
    public void UInt256DivRemBy128ShouldThrowOnZeroDivisor()
    {
        Assert.Throws<DivideByZeroException>(() => UInt256.DivRemBy128(UInt256.One, UInt128.Zero, out _));
    }

    [Fact(DisplayName = "UInt256 modulus by one should return zero")]
    public void UInt256ModulusByOneShouldReturnZero()
    {
        UInt256 value = UInt256.Parse("9999999999999999999999999999");
        Assert.Equal(UInt256.Zero, value % UInt256.One);
    }
}
