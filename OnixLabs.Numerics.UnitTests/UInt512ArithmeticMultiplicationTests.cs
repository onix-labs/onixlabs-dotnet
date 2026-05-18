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

public sealed class UInt512ArithmeticMultiplicationTests
{
    [Fact(DisplayName = "UInt512 multiplication of any value by zero should produce zero")]
    public void UInt512MultiplicationByZeroShouldProduceZero()
    {
        Assert.Equal(UInt512.Zero, UInt512.Zero * UInt512.MaxValue);
        Assert.Equal(UInt512.Zero, UInt512.MaxValue * UInt512.Zero);
        Assert.Equal(UInt512.Zero, UInt512.Zero * UInt512.Zero);
    }

    [Fact(DisplayName = "UInt512 multiplication by one should be the identity")]
    public void UInt512MultiplicationByOneShouldBeIdentity()
    {
        UInt512 value = UInt512.Parse("123456789012345678901234567890");
        Assert.Equal(value, value * UInt512.One);
        Assert.Equal(value, UInt512.One * value);
    }

    [Fact(DisplayName = "UInt512 multiplication of small values should match BigInteger")]
    public void UInt512SmallMultiplicationShouldMatchBigInteger()
    {
        UInt512 left = (UInt512)123UL;
        UInt512 right = (UInt512)456UL;
        UInt512 product = left * right;
        Assert.Equal((BigInteger)123 * 456, (BigInteger)product);
    }

    [Fact(DisplayName = "UInt512 multiplication of large values should match BigInteger wrapping")]
    public void UInt512LargeMultiplicationShouldMatchBigIntegerWrapping()
    {
        UInt512 left = UInt512.Parse("123456789012345678901234567890123456789012345678901234567890");
        UInt512 right = UInt512.Parse("987654321098765432109876543210987654321098765432109876543210");
        UInt512 product = left * right;
        BigInteger mask = (BigInteger.One << 512) - BigInteger.One;
        BigInteger expected = ((BigInteger)left * (BigInteger)right) & mask;
        Assert.Equal(expected, (BigInteger)product);
    }

    [Fact(DisplayName = "UInt512 multiplication should wrap when product exceeds MaxValue")]
    public void UInt512MultiplicationShouldWrapOnOverflow()
    {
        UInt512 huge = UInt512.MaxValue;
        UInt512 result = huge * (UInt512)2UL;
        BigInteger mask = (BigInteger.One << 512) - BigInteger.One;
        BigInteger expected = ((BigInteger)huge * 2) & mask;
        Assert.Equal(expected, (BigInteger)result);
    }

    [Fact(DisplayName = "UInt512 checked multiplication of small values should succeed")]
    public void UInt512CheckedSmallMultiplicationShouldSucceed()
    {
        UInt512 left = (UInt512)123UL;
        UInt512 right = (UInt512)456UL;
        UInt512 product = checked(left * right);
        Assert.Equal((BigInteger)123 * 456, (BigInteger)product);
    }

    [Fact(DisplayName = "UInt512 checked multiplication should throw on overflow")]
    public void UInt512CheckedMultiplicationShouldThrowOnOverflow()
    {
        Assert.Throws<OverflowException>(() => checked(UInt512.MaxValue * (UInt512)2UL));
    }

    [Fact(DisplayName = "UInt512 checked multiplication where product just exceeds 2^512 should throw")]
    public void UInt512CheckedMultiplicationJustOverShouldThrow()
    {
        UInt512 a = UInt512.One << 256;
        UInt512 b = UInt512.One << 256;
        Assert.Throws<OverflowException>(() => checked(a * b));
    }

    [Fact(DisplayName = "UInt512 checked multiplication where product fits should succeed")]
    public void UInt512CheckedMultiplicationFittingShouldSucceed()
    {
        UInt512 a = UInt512.One << 255;
        UInt512 b = UInt512.One << 256;
        UInt512 product = checked(a * b);
        Assert.Equal(UInt512.One << 511, product);
    }

    [Fact(DisplayName = "UInt512.BigMul of two UInt256 MaxValue should equal the full 512-bit product")]
    public void UInt512BigMulUInt256MaxShouldEqualFullProduct()
    {
        UInt256 left = UInt256.MaxValue;
        UInt256 right = UInt256.MaxValue;
        UInt512 product = UInt512.BigMul(left, right);
        BigInteger expected = (BigInteger)left * (BigInteger)right;
        Assert.Equal(expected, (BigInteger)product);
    }

    [Fact(DisplayName = "UInt512.BigMul of UInt256 with zero should produce zero")]
    public void UInt512BigMulUInt256WithZeroShouldProduceZero()
    {
        Assert.Equal(UInt512.Zero, UInt512.BigMul(UInt256.MaxValue, UInt256.Zero));
        Assert.Equal(UInt512.Zero, UInt512.BigMul(UInt256.Zero, UInt256.MaxValue));
    }

    [Fact(DisplayName = "UInt512.BigMul of UInt256 with one should produce the other operand")]
    public void UInt512BigMulUInt256WithOneShouldProduceOther()
    {
        UInt512 product = UInt512.BigMul(UInt256.MaxValue, UInt256.One);
        Assert.Equal((BigInteger)UInt256.MaxValue, (BigInteger)product);
    }

    [Fact(DisplayName = "UInt512.BigMul of two UInt512 MaxValue should expose the full 1024-bit product")]
    public void UInt512BigMulOfTwoMaxValueShouldExposeFullProduct()
    {
        UInt512 left = UInt512.MaxValue;
        UInt512 right = UInt512.MaxValue;
        UInt512 high = UInt512.BigMul(left, right, out UInt512 low);
        BigInteger product = (BigInteger)left * (BigInteger)right;
        BigInteger expectedLow = product & ((BigInteger.One << 512) - BigInteger.One);
        BigInteger expectedHigh = product >> 512;
        Assert.Equal(expectedLow, (BigInteger)low);
        Assert.Equal(expectedHigh, (BigInteger)high);
    }

    [Fact(DisplayName = "UInt512.BigMul of two UInt512 with zero should produce both halves zero")]
    public void UInt512BigMulOfTwoUInt512WithZeroShouldProduceZero()
    {
        UInt512 high = UInt512.BigMul(UInt512.MaxValue, UInt512.Zero, out UInt512 low);
        Assert.Equal(UInt512.Zero, high);
        Assert.Equal(UInt512.Zero, low);
    }

    [Fact(DisplayName = "UInt512.BigMul of two UInt512 with one should produce the other operand in low half and zero in high")]
    public void UInt512BigMulOfTwoUInt512WithOneShouldEqualOperand()
    {
        UInt512 value = UInt512.Parse("123456789012345678901234567890");
        UInt512 high = UInt512.BigMul(value, UInt512.One, out UInt512 low);
        Assert.Equal(UInt512.Zero, high);
        Assert.Equal(value, low);
    }

    [Fact(DisplayName = "UInt512.BigMul for halves whose product just fits in 512 bits should have high zero")]
    public void UInt512BigMulProductFittingIn512BitsShouldHaveHighZero()
    {
        UInt512 a = UInt512.One << 255;
        UInt512 b = UInt512.One << 256;
        UInt512 high = UInt512.BigMul(a, b, out UInt512 low);
        Assert.Equal(UInt512.Zero, high);
        Assert.Equal(UInt512.One << 511, low);
    }

    [Fact(DisplayName = "UInt512 multiplication is commutative")]
    public void UInt512MultiplicationIsCommutative()
    {
        UInt512 a = UInt512.Parse("12345678901234567890");
        UInt512 b = UInt512.Parse("98765432109876543210");
        Assert.Equal(a * b, b * a);
    }
}
