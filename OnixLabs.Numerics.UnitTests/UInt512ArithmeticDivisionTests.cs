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

public sealed class UInt512ArithmeticDivisionTests
{
    [Fact(DisplayName = "UInt512 division by one should return the dividend")]
    public void UInt512DivisionByOneShouldReturnDividend()
    {
        UInt512 value = UInt512.Parse("123456789012345678901234567890");
        Assert.Equal(value, value / UInt512.One);
    }

    [Fact(DisplayName = "UInt512 division of zero by any non-zero should produce zero")]
    public void UInt512DivisionOfZeroShouldProduceZero()
    {
        Assert.Equal(UInt512.Zero, UInt512.Zero / UInt512.One);
        Assert.Equal(UInt512.Zero, UInt512.Zero / UInt512.MaxValue);
    }

    [Fact(DisplayName = "UInt512 division when dividend equals divisor should produce one")]
    public void UInt512DivisionWhenDividendEqualsDivisorShouldProduceOne()
    {
        UInt512 value = UInt512.Parse("123456789012345678901234567890");
        Assert.Equal(UInt512.One, value / value);
    }

    [Fact(DisplayName = "UInt512 division when dividend is less than divisor should produce zero")]
    public void UInt512DivisionWhenDividendLessThanDivisorShouldProduceZero()
    {
        UInt512 a = (UInt512)5UL;
        UInt512 b = (UInt512)10UL;
        Assert.Equal(UInt512.Zero, a / b);
    }

    [Fact(DisplayName = "UInt512 division by zero should throw DivideByZeroException")]
    public void UInt512DivisionByZeroShouldThrow()
    {
        Assert.Throws<DivideByZeroException>(() => UInt512.MaxValue / UInt512.Zero);
        Assert.Throws<DivideByZeroException>(() => UInt512.One / UInt512.Zero);
    }

    [Fact(DisplayName = "UInt512 division by two should match BigInteger")]
    public void UInt512DivisionByTwoShouldMatchBigInteger()
    {
        UInt512 value = UInt512.MaxValue;
        UInt512 quotient = value / (UInt512)2UL;
        Assert.Equal((BigInteger)value / 2, (BigInteger)quotient);
    }

    [Fact(DisplayName = "UInt512 division of large values should match BigInteger")]
    public void UInt512LargeDivisionShouldMatchBigInteger()
    {
        UInt512 dividend = UInt512.Parse("987654321098765432109876543210987654321098765432109876543210");
        UInt512 divisor = UInt512.Parse("123456789012345678901234567890");
        UInt512 quotient = dividend / divisor;
        Assert.Equal((BigInteger)dividend / (BigInteger)divisor, (BigInteger)quotient);
    }

    [Fact(DisplayName = "UInt512 modulus when dividend is less than divisor should produce dividend")]
    public void UInt512ModulusWhenDividendLessThanDivisorShouldProduceDividend()
    {
        UInt512 a = (UInt512)7UL;
        UInt512 b = (UInt512)100UL;
        Assert.Equal(a, a % b);
    }

    [Fact(DisplayName = "UInt512 modulus by one should produce zero")]
    public void UInt512ModulusByOneShouldProduceZero()
    {
        UInt512 value = UInt512.MaxValue;
        Assert.Equal(UInt512.Zero, value % UInt512.One);
    }

    [Fact(DisplayName = "UInt512 modulus by zero should throw DivideByZeroException")]
    public void UInt512ModulusByZeroShouldThrow()
    {
        Assert.Throws<DivideByZeroException>(() => UInt512.MaxValue % UInt512.Zero);
    }

    [Fact(DisplayName = "UInt512.DivRem should return matching quotient and remainder for small values")]
    public void UInt512DivRemShouldReturnMatchingQuotientAndRemainder()
    {
        UInt512 dividend = (UInt512)1_234_567_890UL;
        UInt512 divisor = (UInt512)1000UL;
        (UInt512 quotient, UInt512 remainder) = UInt512.DivRem(dividend, divisor);
        Assert.Equal((UInt512)1_234_567UL, quotient);
        Assert.Equal((UInt512)890UL, remainder);
    }

    [Fact(DisplayName = "UInt512.DivRem should match BigInteger.DivRem for large operands")]
    public void UInt512DivRemShouldMatchBigInteger()
    {
        UInt512 dividend = UInt512.Parse("99999999999999999999999999999999999999999999999999999999999999999999999999999999");
        UInt512 divisor = UInt512.Parse("12345678901234567890");
        (UInt512 quotient, UInt512 remainder) = UInt512.DivRem(dividend, divisor);
        BigInteger bigQuot = BigInteger.DivRem((BigInteger)dividend, (BigInteger)divisor, out BigInteger bigRem);
        Assert.Equal(bigQuot, (BigInteger)quotient);
        Assert.Equal(bigRem, (BigInteger)remainder);
    }

    [Fact(DisplayName = "UInt512.DivRem with zero dividend should produce (Zero, Zero)")]
    public void UInt512DivRemWithZeroDividendShouldProduceZeros()
    {
        (UInt512 q, UInt512 r) = UInt512.DivRem(UInt512.Zero, (UInt512)123UL);
        Assert.Equal(UInt512.Zero, q);
        Assert.Equal(UInt512.Zero, r);
    }

    [Fact(DisplayName = "UInt512.DivRem with divisor equal to dividend should produce (One, Zero)")]
    public void UInt512DivRemWhenDividendEqualsDivisorShouldProduceOneAndZero()
    {
        UInt512 value = UInt512.Parse("123456789012345678901234567890");
        (UInt512 q, UInt512 r) = UInt512.DivRem(value, value);
        Assert.Equal(UInt512.One, q);
        Assert.Equal(UInt512.Zero, r);
    }

    [Fact(DisplayName = "UInt512.DivRem with divisor greater than dividend should produce (Zero, Dividend)")]
    public void UInt512DivRemWhenDivisorGreaterThanDividendShouldProduceZeroAndDividend()
    {
        UInt512 dividend = (UInt512)42UL;
        UInt512 divisor = (UInt512)1000UL;
        (UInt512 q, UInt512 r) = UInt512.DivRem(dividend, divisor);
        Assert.Equal(UInt512.Zero, q);
        Assert.Equal(dividend, r);
    }

    [Fact(DisplayName = "UInt512.DivRem of MaxValue divided by One should produce (MaxValue, Zero)")]
    public void UInt512DivRemMaxValueByOneShouldProduceMaxValueAndZero()
    {
        (UInt512 q, UInt512 r) = UInt512.DivRem(UInt512.MaxValue, UInt512.One);
        Assert.Equal(UInt512.MaxValue, q);
        Assert.Equal(UInt512.Zero, r);
    }

    [Fact(DisplayName = "UInt512.DivRem should throw DivideByZeroException on zero divisor")]
    public void UInt512DivRemByZeroShouldThrow()
    {
        Assert.Throws<DivideByZeroException>(() => UInt512.DivRem(UInt512.MaxValue, UInt512.Zero));
    }

    [Fact(DisplayName = "UInt512.DivRemBy256 should match BigInteger for moderate divisors satisfying the quotient-fits precondition")]
    public void UInt512DivRemBy256ShouldMatchBigInteger()
    {
        UInt512 dividend = UInt512.Parse("99999999999999999999999999999999999999999999999999999999999999999999999999999");
        UInt256 divisor = (UInt256)(UInt128.MaxValue);
        Assert.True(dividend.UpperBits < divisor);
        UInt256 quotient = UInt512.DivRemBy256(dividend, divisor, out UInt256 remainder);
        Assert.Equal((BigInteger)dividend / (BigInteger)divisor, (BigInteger)quotient);
        Assert.Equal((BigInteger)dividend % (BigInteger)divisor, (BigInteger)remainder);
    }

    [Fact(DisplayName = "UInt512.DivRemBy256 should match BigInteger when the divisor is close to 2^256 (carry edge)")]
    public void UInt512DivRemBy256NearTopBitDivisorShouldMatchBigInteger()
    {
        // Regression: a previous version of DivRemBy256 lost the overflow bit of `working` when shifting
        // left, so for divisors close to 2^256 the comparison `working >= divisor` missed cases where the
        // true value (2^256 + working) exceeded the divisor and we silently kept an inflated remainder.
        UInt256 divisor = UInt256.MaxValue;
        UInt512 dividend = new(divisor - UInt256.One, UInt256.MaxValue);
        UInt256 quotient = UInt512.DivRemBy256(dividend, divisor, out UInt256 remainder);
        Assert.Equal((BigInteger)dividend / (BigInteger)divisor, (BigInteger)quotient);
        Assert.Equal((BigInteger)dividend % (BigInteger)divisor, (BigInteger)remainder);
    }

    public static TheoryData<UInt256, UInt256> DivRemBy256LargeOperandData =>
        new()
        {
            // Divisor is MaxValue (all 1s). Upper is divisor - 16.
            { UInt256.MaxValue - (UInt256)16U, UInt256.MaxValue },
            // Divisor and upper both have the sign bit set (>= 2^255) — exercises the carry edge each iteration.
            { new(UInt128.One << 127, UInt128.One), new(UInt128.One << 127, (UInt128)3U) },
            // Mid-range pattern; upper < divisor by construction.
            { (UInt256.MaxValue >> 2) - (UInt256)5U, UInt256.MaxValue >> 1 }
        };

    [Theory(DisplayName = "UInt512.DivRemBy256 should match BigInteger across a range of large-divisor and large-dividend combinations")]
    [MemberData(nameof(DivRemBy256LargeOperandData))]
    public void UInt512DivRemBy256AcrossLargeOperandsShouldMatchBigInteger(UInt256 upper, UInt256 divisor)
    {
        Assert.True(upper < divisor);
        UInt512 dividend = new(upper, UInt256.MaxValue);
        UInt256 quotient = UInt512.DivRemBy256(dividend, divisor, out UInt256 remainder);
        Assert.Equal((BigInteger)dividend / (BigInteger)divisor, (BigInteger)quotient);
        Assert.Equal((BigInteger)dividend % (BigInteger)divisor, (BigInteger)remainder);
    }

    [Fact(DisplayName = "UInt512.DivRemBy256 should throw on zero divisor")]
    public void UInt512DivRemBy256ZeroDivisorShouldThrow()
    {
        Assert.Throws<DivideByZeroException>(() => UInt512.DivRemBy256(UInt512.MaxValue, UInt256.Zero, out _));
    }

    [Fact(DisplayName = "UInt512.DivRemBy256 with simple 64-bit divisor should match BigInteger")]
    public void UInt512DivRemBy256WithSmall64BitDivisorShouldMatchBigInteger()
    {
        UInt512 dividend = UInt512.Parse("999999999999999999999999999999999999999999999999999999999999999999999999");
        UInt256 divisor = (UInt256)1234567890UL;
        UInt256 quotient = UInt512.DivRemBy256(dividend, divisor, out UInt256 remainder);
        Assert.Equal((BigInteger)dividend / (BigInteger)divisor, (BigInteger)quotient);
        Assert.Equal((BigInteger)dividend % (BigInteger)divisor, (BigInteger)remainder);
    }

    [Fact(DisplayName = "UInt512 division identity quotient * divisor + remainder = dividend")]
    public void UInt512DivisionIdentityShouldHold()
    {
        UInt512 dividend = UInt512.Parse("123456789012345678901234567890123456789012345678901234567890");
        UInt512 divisor = UInt512.Parse("987654321098765432109876543210");
        (UInt512 q, UInt512 r) = UInt512.DivRem(dividend, divisor);
        Assert.Equal(dividend, q * divisor + r);
    }
}
