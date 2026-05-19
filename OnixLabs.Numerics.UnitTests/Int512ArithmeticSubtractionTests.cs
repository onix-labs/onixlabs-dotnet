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

public sealed class Int512ArithmeticSubtractionTests
{
    [Fact(DisplayName = "Int512 subtraction of identical values should produce zero")]
    public void Int512SubtractionOfIdenticalShouldBeZero()
    {
        Assert.Equal(Int512.Zero, Int512.MaxValue - Int512.MaxValue);
        Assert.Equal(Int512.Zero, Int512.MinValue - Int512.MinValue);
    }

    [Fact(DisplayName = "Int512 subtraction of zero should be the identity")]
    public void Int512SubtractionOfZeroShouldBeIdentity()
    {
        Int512 value = Int512.Parse("-12345678901234567890");
        Assert.Equal(value, value - Int512.Zero);
    }

    [Fact(DisplayName = "Int512 subtraction by NegativeOne should add one")]
    public void Int512SubtractionByNegativeOneShouldAddOne()
    {
        Assert.Equal((Int512)1, Int512.Zero - Int512.NegativeOne);
        Assert.Equal((Int512)11, (Int512)10 - Int512.NegativeOne);
    }

    [Fact(DisplayName = "Int512 unchecked subtraction should wrap on overflow")]
    public void Int512SubtractionShouldWrapOnOverflow()
    {
        Assert.Equal(Int512.MaxValue, Int512.MinValue - Int512.One);
    }

    [Fact(DisplayName = "Int512 unchecked subtraction should wrap on underflow")]
    public void Int512SubtractionShouldWrapOnUnderflow()
    {
        Assert.Equal(Int512.MinValue, Int512.MaxValue - Int512.NegativeOne);
    }

    [Fact(DisplayName = "Int512 subtraction should match BigInteger for mixed signs")]
    public void Int512SubtractionShouldMatchBigInteger()
    {
        Int512 a = (Int512)(-12345);
        Int512 b = (Int512)67890;
        Int512 result = a - b;
        Assert.Equal((BigInteger)(-12345) - 67890, (BigInteger)result);
    }

    [Fact(DisplayName = "Int512 subtraction of large negatives should match BigInteger")]
    public void Int512SubtractionLargeNegativesShouldMatchBigInteger()
    {
        Int512 a = Int512.Parse("-987654321098765432109876543210987654321098765432109876543210");
        Int512 b = Int512.Parse("-123456789012345678901234567890123456789012345678901234567890");
        Int512 diff = a - b;
        Assert.Equal((BigInteger)a - (BigInteger)b, (BigInteger)diff);
    }

    [Fact(DisplayName = "Int512 checked subtraction should succeed for in-range")]
    public void Int512CheckedSubtractionInRangeShouldSucceed()
    {
        Int512 a = (Int512)100;
        Int512 b = (Int512)25;
        Assert.Equal((Int512)75, checked(a - b));
    }

    [Fact(DisplayName = "Int512 checked subtraction should throw on positive overflow")]
    public void Int512CheckedSubtractionShouldThrowOnPositiveOverflow()
    {
        Assert.Throws<OverflowException>(() => checked(Int512.MaxValue - Int512.NegativeOne));
    }

    [Fact(DisplayName = "Int512 checked subtraction should throw on negative overflow")]
    public void Int512CheckedSubtractionShouldThrowOnNegativeOverflow()
    {
        Assert.Throws<OverflowException>(() => checked(Int512.MinValue - Int512.One));
    }

    [Fact(DisplayName = "Int512 checked subtraction should not overflow when signs are the same")]
    public void Int512CheckedSubtractionSameSignShouldNotOverflow()
    {
        Int512 result = checked(Int512.MaxValue - Int512.MaxValue);
        Assert.Equal(Int512.Zero, result);
    }
}
