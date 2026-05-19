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

public sealed class UInt256ArithmeticMultiplicationTests
{
    [Fact(DisplayName = "UInt256 multiplication by zero should return zero")]
    public void UInt256MultiplicationByZeroShouldReturnZero()
    {
        Assert.Equal(UInt256.Zero, UInt256.MaxValue * UInt256.Zero);
        Assert.Equal(UInt256.Zero, UInt256.Zero * UInt256.MaxValue);
    }

    [Fact(DisplayName = "UInt256 multiplication by one should return the value")]
    public void UInt256MultiplicationByOneShouldReturnValue()
    {
        UInt256 value = UInt256.Parse("123456789012345678901234567890");
        Assert.Equal(value, value * UInt256.One);
        Assert.Equal(value, UInt256.One * value);
    }

    [Fact(DisplayName = "UInt256 multiplication wrapping should produce only low 256 bits")]
    public void UInt256MultiplicationWrappingShouldProduceOnlyLow256Bits()
    {
        UInt256 actual = UInt256.MaxValue * UInt256.MaxValue;
        BigInteger expected = ((BigInteger)UInt256.MaxValue * (BigInteger)UInt256.MaxValue)
            & (((BigInteger.One << 256) - BigInteger.One));
        Assert.Equal(expected, (BigInteger)actual);
    }

    [Fact(DisplayName = "UInt256 checked multiplication should throw when product exceeds MaxValue")]
    public void UInt256CheckedMultiplicationShouldThrowWhenProductExceedsMaxValue()
    {
        UInt256 large = UInt256.One << 200;
        Assert.Throws<OverflowException>(() => checked(large * large));
    }

    [Fact(DisplayName = "UInt256 checked multiplication should succeed when product is within range")]
    public void UInt256CheckedMultiplicationShouldSucceedInRange()
    {
        UInt256 left = (UInt256)1234567890UL;
        UInt256 right = (UInt256)987654321UL;
        UInt256 product = checked(left * right);
        Assert.Equal((BigInteger)left * (BigInteger)right, (BigInteger)product);
    }

    [Fact(DisplayName = "UInt256.BigMul of UInt128.MaxValue squared should match BigInteger")]
    public void UInt256BigMulUInt128MaxValueSquaredShouldMatchBigInteger()
    {
        UInt256 product = UInt256.BigMul(UInt128.MaxValue, UInt128.MaxValue);
        BigInteger expected = (BigInteger)UInt128.MaxValue * (BigInteger)UInt128.MaxValue;
        Assert.Equal(expected, (BigInteger)product);
    }

    [Fact(DisplayName = "UInt256.BigMul of zero should return zero")]
    public void UInt256BigMulOfZeroShouldReturnZero()
    {
        Assert.Equal(UInt256.Zero, UInt256.BigMul(UInt128.Zero, UInt128.MaxValue));
        Assert.Equal(UInt256.Zero, UInt256.BigMul(UInt128.MaxValue, UInt128.Zero));
    }

    [Theory(DisplayName = "UInt256.BigMul of UInt128 by UInt128 should match BigInteger oracle")]
    [InlineData("11", "13")]
    [InlineData("18446744073709551616", "9223372036854775808")]
    [InlineData("340282366920938463463374607431768211455", "1")]
    [InlineData("87654321987654321987654321", "123456789123456789123")]
    [InlineData("250", "1000000000000000000000000")]
    public void UInt256BigMulUInt128ByUInt128ShouldMatchBigIntegerOracle(string leftText, string rightText)
    {
        UInt128 left = UInt128.Parse(leftText);
        UInt128 right = UInt128.Parse(rightText);
        UInt256 product = UInt256.BigMul(left, right);
        BigInteger expected = (BigInteger)left * (BigInteger)right;
        Assert.Equal(expected, (BigInteger)product);
    }

    [Theory(DisplayName = "UInt256.BigMul of UInt256 by UInt256 should match BigInteger oracle producing a 512-bit product")]
    [InlineData("0", "0")]
    [InlineData("1", "0")]
    [InlineData("115792089237316195423570985008687907853269984665640564039457584007913129639935", "115792089237316195423570985008687907853269984665640564039457584007913129639935")]
    [InlineData("12345678901234567890", "98765432109876543210")]
    [InlineData("57896044618658097711785492504343953926634992332820282019728792003956564819968", "2")]
    public void UInt256BigMulUInt256ByUInt256ShouldMatchBigIntegerOracle(string leftText, string rightText)
    {
        UInt256 left = UInt256.Parse(leftText);
        UInt256 right = UInt256.Parse(rightText);
        UInt256 high = UInt256.BigMul(left, right, out UInt256 low);
        BigInteger expected = (BigInteger)left * (BigInteger)right;
        BigInteger actual = ((BigInteger)high << 256) | (BigInteger)low;
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "UInt256 multiplication should be commutative")]
    public void UInt256MultiplicationShouldBeCommutative()
    {
        UInt256 left = UInt256.Parse("123456789012345678901234567890");
        UInt256 right = UInt256.Parse("987654321098765432109876543210");
        Assert.Equal(left * right, right * left);
    }
}
