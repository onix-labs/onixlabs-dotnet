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

public sealed class Int256ArithmeticMultiplicationTests
{
    [Fact(DisplayName = "Int256 multiplication by zero should return zero")]
    public void Int256MultiplicationByZeroShouldReturnZero()
    {
        Assert.Equal(Int256.Zero, Int256.MaxValue * Int256.Zero);
        Assert.Equal(Int256.Zero, Int256.MinValue * Int256.Zero);
        Assert.Equal(Int256.Zero, Int256.Zero * Int256.NegativeOne);
    }

    [Fact(DisplayName = "Int256 multiplication by one should return the value")]
    public void Int256MultiplicationByOneShouldReturnValue()
    {
        Int256 value = (Int256)(-12345);
        Assert.Equal(value, value * Int256.One);
        Assert.Equal(value, Int256.One * value);
    }

    [Fact(DisplayName = "Int256 multiplication by NegativeOne should negate")]
    public void Int256MultiplicationByNegativeOneShouldNegate()
    {
        Int256 value = (Int256)1234;
        Assert.Equal((Int256)(-1234), value * Int256.NegativeOne);
        Assert.Equal((Int256)1234, value * Int256.NegativeOne * Int256.NegativeOne);
    }

    [Fact(DisplayName = "Int256 multiplication sign rules should follow standard arithmetic")]
    public void Int256MultiplicationSignRulesShouldFollowArithmetic()
    {
        Assert.True(Int256.IsPositive((Int256)3 * (Int256)4));
        Assert.True(Int256.IsNegative((Int256)3 * (Int256)(-4)));
        Assert.True(Int256.IsNegative((Int256)(-3) * (Int256)4));
        Assert.True(Int256.IsPositive((Int256)(-3) * (Int256)(-4)));
    }

    [Fact(DisplayName = "Int256 multiplication should match BigInteger oracle for negative operands")]
    public void Int256MultiplicationShouldMatchBigIntegerForNegativeOperands()
    {
        Int256 left = (Int256)(-99999999);
        Int256 right = Int256.Parse("1234567890123456789");
        Int256 actual = left * right;
        BigInteger expected = (BigInteger)left * (BigInteger)right;
        Assert.Equal(expected, (BigInteger)actual);
    }

    [Fact(DisplayName = "Int256 checked multiplication positive overflow should throw")]
    public void Int256CheckedMultiplicationPositiveOverflowShouldThrow()
    {
        Assert.Throws<OverflowException>(() => checked(Int256.MaxValue * (Int256)2));
        Assert.Throws<OverflowException>(() => checked(Int256.MaxValue * Int256.MaxValue));
    }

    [Fact(DisplayName = "Int256 checked multiplication negative overflow should throw")]
    public void Int256CheckedMultiplicationNegativeOverflowShouldThrow()
    {
        Assert.Throws<OverflowException>(() => checked(Int256.MinValue * (Int256)2));
        Assert.Throws<OverflowException>(() => checked(Int256.MinValue * Int256.NegativeOne));
    }

    [Fact(DisplayName = "Int256 checked multiplication MinValue times One should succeed")]
    public void Int256CheckedMultiplicationMinValueTimesOneShouldSucceed()
    {
        Assert.Equal(Int256.MinValue, checked(Int256.MinValue * Int256.One));
    }

    [Fact(DisplayName = "Int256 checked multiplication MaxValue times NegativeOne should equal MinValue plus One")]
    public void Int256CheckedMultiplicationMaxTimesNegativeOneShouldEqualMinPlusOne()
    {
        Int256 expected = Int256.MinValue + Int256.One;
        Assert.Equal(expected, checked(Int256.MaxValue * Int256.NegativeOne));
    }

    [Fact(DisplayName = "Int256 unchecked multiplication overflow should wrap")]
    public void Int256UncheckedMultiplicationOverflowShouldWrap()
    {
        // MaxValue * 2 wraps to NegativeTwo in two's-complement.
        Int256 expected = (Int256)(-2);
        Assert.Equal(expected, Int256.MaxValue * (Int256)2);
    }

    [Fact(DisplayName = "Int256 multiplication should be commutative")]
    public void Int256MultiplicationShouldBeCommutative()
    {
        Int256 left = (Int256)12345;
        Int256 right = (Int256)(-67890);
        Assert.Equal(left * right, right * left);
    }
}
