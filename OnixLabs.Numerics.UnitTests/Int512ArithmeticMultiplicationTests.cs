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

public sealed class Int512ArithmeticMultiplicationTests
{
    [Fact(DisplayName = "Int512 multiplication by zero should produce zero")]
    public void Int512MultiplicationByZeroShouldProduceZero()
    {
        Assert.Equal(Int512.Zero, Int512.Zero * Int512.MaxValue);
        Assert.Equal(Int512.Zero, Int512.MinValue * Int512.Zero);
    }

    [Fact(DisplayName = "Int512 multiplication by one should be the identity")]
    public void Int512MultiplicationByOneShouldBeIdentity()
    {
        Int512 value = Int512.Parse("-123456789012345678901234567890");
        Assert.Equal(value, value * Int512.One);
        Assert.Equal(value, Int512.One * value);
    }

    [Fact(DisplayName = "Int512 multiplication by NegativeOne should negate")]
    public void Int512MultiplicationByNegativeOneShouldNegate()
    {
        Int512 value = (Int512)12345;
        Assert.Equal(-value, value * Int512.NegativeOne);
    }

    [Fact(DisplayName = "Int512 multiplication of two negatives should produce a positive product")]
    public void Int512MultiplicationOfTwoNegativesShouldBePositive()
    {
        Int512 a = (Int512)(-12345);
        Int512 b = (Int512)(-67890);
        Int512 product = a * b;
        Assert.Equal((BigInteger)(-12345) * (-67890), (BigInteger)product);
    }

    [Fact(DisplayName = "Int512 multiplication of mixed signs should produce a negative product")]
    public void Int512MultiplicationOfMixedSignsShouldBeNegative()
    {
        Int512 a = (Int512)(-12345);
        Int512 b = (Int512)67890;
        Int512 product = a * b;
        Assert.Equal((BigInteger)(-12345) * 67890, (BigInteger)product);
    }

    [Fact(DisplayName = "Int512 multiplication of small values should match BigInteger")]
    public void Int512SmallMultiplicationShouldMatchBigInteger()
    {
        Int512 a = (Int512)123;
        Int512 b = (Int512)456;
        Assert.Equal((BigInteger)123 * 456, (BigInteger)(a * b));
    }

    [Fact(DisplayName = "Int512 multiplication of large operands should match BigInteger")]
    public void Int512LargeMultiplicationShouldMatchBigInteger()
    {
        Int512 left = Int512.Parse("123456789012345678901234567890");
        Int512 right = Int512.Parse("-987654321098765432109876543210");
        Int512 product = left * right;
        Assert.Equal((BigInteger)left * (BigInteger)right, (BigInteger)product);
    }

    [Fact(DisplayName = "Int512 unchecked multiplication should wrap on overflow")]
    public void Int512MultiplicationShouldWrapOnOverflow()
    {
        Int512 result = Int512.MaxValue * (Int512)2;
        BigInteger bigMask = (BigInteger.One << 512) - BigInteger.One;
        BigInteger expectedUnsigned = ((BigInteger)Int512.MaxValue * 2) & bigMask;
        // Compare bit patterns via UInt512
        UInt512 expectedBits = (UInt512)expectedUnsigned;
        Assert.Equal(expectedBits, (UInt512)result);
    }

    [Fact(DisplayName = "Int512 checked multiplication should succeed for in-range values")]
    public void Int512CheckedMultiplicationInRangeShouldSucceed()
    {
        Int512 a = (Int512)1_000_000_000;
        Int512 b = (Int512)1_000_000_000;
        Int512 product = checked(a * b);
        Assert.Equal((Int512)1_000_000_000_000_000_000L, product);
    }

    [Fact(DisplayName = "Int512 checked multiplication should throw on positive overflow")]
    public void Int512CheckedMultiplicationPositiveOverflowShouldThrow()
    {
        Assert.Throws<OverflowException>(() => checked(Int512.MaxValue * (Int512)2));
    }

    [Fact(DisplayName = "Int512 checked multiplication should throw on negative overflow")]
    public void Int512CheckedMultiplicationNegativeOverflowShouldThrow()
    {
        Assert.Throws<OverflowException>(() => checked(Int512.MinValue * (Int512)2));
    }

    [Fact(DisplayName = "Int512 checked multiplication of MinValue by NegativeOne should throw")]
    public void Int512CheckedMultiplicationMinValueByNegativeOneShouldThrow()
    {
        Assert.Throws<OverflowException>(() => checked(Int512.MinValue * Int512.NegativeOne));
    }

    [Fact(DisplayName = "Int512 multiplication is commutative")]
    public void Int512MultiplicationIsCommutative()
    {
        Int512 a = (Int512)(-12345);
        Int512 b = (Int512)67890;
        Assert.Equal(a * b, b * a);
    }
}
