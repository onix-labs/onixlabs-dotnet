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

public sealed class Int512ArithmeticAdditionTests
{
    [Fact(DisplayName = "Int512 addition of two zeros should produce zero")]
    public void Int512AdditionOfTwoZerosShouldBeZero()
    {
        Assert.Equal(Int512.Zero, Int512.Zero + Int512.Zero);
    }

    [Fact(DisplayName = "Int512 addition with zero should be the identity")]
    public void Int512AdditionWithZeroShouldBeIdentity()
    {
        Int512 value = Int512.Parse("-123456789012345678901234567890");
        Assert.Equal(value, value + Int512.Zero);
        Assert.Equal(value, Int512.Zero + value);
    }

    [Fact(DisplayName = "Int512 addition of opposites should produce zero")]
    public void Int512AdditionOfOppositesShouldBeZero()
    {
        Int512 value = (Int512)12345;
        Assert.Equal(Int512.Zero, value + (-value));
    }

    [Fact(DisplayName = "Int512 addition with NegativeOne should subtract one")]
    public void Int512AdditionWithNegativeOneShouldSubtractOne()
    {
        Assert.Equal(Int512.Zero, Int512.One + Int512.NegativeOne);
        Assert.Equal((Int512)9, (Int512)10 + Int512.NegativeOne);
    }

    [Fact(DisplayName = "Int512 unchecked addition should wrap on overflow")]
    public void Int512AdditionShouldWrapOnOverflow()
    {
        Assert.Equal(Int512.MinValue, Int512.MaxValue + Int512.One);
    }

    [Fact(DisplayName = "Int512 unchecked addition should wrap on underflow")]
    public void Int512AdditionShouldWrapOnUnderflow()
    {
        Assert.Equal(Int512.MaxValue, Int512.MinValue + Int512.NegativeOne);
    }

    [Fact(DisplayName = "Int512 addition should match BigInteger for mixed signs")]
    public void Int512AdditionShouldMatchBigInteger()
    {
        Int512 a = (Int512)(-12345);
        Int512 b = (Int512)67890;
        Int512 result = a + b;
        Assert.Equal((BigInteger)(-12345) + 67890, (BigInteger)result);
    }

    [Fact(DisplayName = "Int512 addition should match BigInteger for two large negatives")]
    public void Int512AdditionTwoLargeNegativesShouldMatchBigInteger()
    {
        Int512 a = Int512.Parse("-123456789012345678901234567890123456789012345678901234567890");
        Int512 b = Int512.Parse("-987654321098765432109876543210987654321098765432109876543210");
        Int512 sum = a + b;
        Assert.Equal((BigInteger)a + (BigInteger)b, (BigInteger)sum);
    }

    [Fact(DisplayName = "Int512 checked addition of zeros should succeed")]
    public void Int512CheckedAdditionOfZerosShouldSucceed()
    {
        Assert.Equal(Int512.Zero, checked(Int512.Zero + Int512.Zero));
    }

    [Fact(DisplayName = "Int512 checked addition should succeed when result fits")]
    public void Int512CheckedAdditionInRangeShouldSucceed()
    {
        Int512 a = Int512.MaxValue - Int512.One;
        Int512 result = checked(a + Int512.One);
        Assert.Equal(Int512.MaxValue, result);
    }

    [Fact(DisplayName = "Int512 checked addition should throw on positive overflow")]
    public void Int512CheckedAdditionShouldThrowOnPositiveOverflow()
    {
        Assert.Throws<OverflowException>(() => checked(Int512.MaxValue + Int512.One));
    }

    [Fact(DisplayName = "Int512 checked addition should throw on negative overflow")]
    public void Int512CheckedAdditionShouldThrowOnNegativeOverflow()
    {
        Assert.Throws<OverflowException>(() => checked(Int512.MinValue + Int512.NegativeOne));
    }

    [Fact(DisplayName = "Int512 checked addition should not overflow when signs differ")]
    public void Int512CheckedAdditionShouldNotOverflowWhenSignsDiffer()
    {
        Int512 result = checked(Int512.MaxValue + Int512.MinValue);
        Assert.Equal(Int512.NegativeOne, result);
    }

    [Fact(DisplayName = "Int512 unary plus should return the value unchanged")]
    public void Int512UnaryPlusShouldReturnValue()
    {
        Int512 value = Int512.NegativeOne;
        Assert.Equal(value, +value);
    }
}
