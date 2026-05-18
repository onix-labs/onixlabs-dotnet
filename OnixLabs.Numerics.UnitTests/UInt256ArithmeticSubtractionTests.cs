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

public sealed class UInt256ArithmeticSubtractionTests
{
    [Fact(DisplayName = "UInt256 subtraction of value minus zero should return the value")]
    public void UInt256SubtractionOfValueMinusZeroShouldReturnValue()
    {
        UInt256 value = (UInt256)123456789UL;
        Assert.Equal(value, value - UInt256.Zero);
    }

    [Fact(DisplayName = "UInt256 subtraction of value minus itself should return zero")]
    public void UInt256SubtractionOfValueMinusItselfShouldReturnZero()
    {
        UInt256 value = UInt256.Parse("123456789012345678901234567890");
        Assert.Equal(UInt256.Zero, value - value);
        Assert.Equal(UInt256.Zero, UInt256.MaxValue - UInt256.MaxValue);
    }

    [Fact(DisplayName = "UInt256 subtraction without borrow should subtract the halves independently")]
    public void UInt256SubtractionWithoutBorrowShouldSubtractHalvesIndependently()
    {
        UInt256 left = new((UInt128)10, (UInt128)20);
        UInt256 right = new((UInt128)3, (UInt128)4);
        UInt256 result = left - right;
        Assert.Equal((UInt128)7, result.UpperBits);
        Assert.Equal((UInt128)16, result.LowerBits);
    }

    [Fact(DisplayName = "UInt256 subtraction with borrow should decrement the upper half")]
    public void UInt256SubtractionWithBorrowShouldDecrementUpper()
    {
        UInt256 left = new((UInt128)5, UInt128.Zero);
        UInt256 right = new(UInt128.Zero, UInt128.One);
        UInt256 result = left - right;
        Assert.Equal((UInt128)4, result.UpperBits);
        Assert.Equal(UInt128.MaxValue, result.LowerBits);
    }

    [Fact(DisplayName = "UInt256 subtraction underflow should wrap to MaxValue when zero minus one")]
    public void UInt256SubtractionUnderflowShouldWrapToMaxValue()
    {
        UInt256 result = UInt256.Zero - UInt256.One;
        Assert.Equal(UInt256.MaxValue, result);
    }

    [Fact(DisplayName = "UInt256 checked subtraction with no underflow should match unchecked")]
    public void UInt256CheckedSubtractionWithoutUnderflowShouldMatchUnchecked()
    {
        UInt256 left = (UInt256)1000;
        UInt256 right = (UInt256)400;
        Assert.Equal(left - right, checked(left - right));
    }

    [Fact(DisplayName = "UInt256 checked subtraction Zero minus One should throw")]
    public void UInt256CheckedSubtractionZeroMinusOneShouldThrow()
    {
        Assert.Throws<OverflowException>(() => checked(UInt256.Zero - UInt256.One));
    }

    [Fact(DisplayName = "UInt256 checked subtraction larger from smaller should throw")]
    public void UInt256CheckedSubtractionLargerFromSmallerShouldThrow()
    {
        UInt256 smaller = (UInt256)10;
        UInt256 larger = (UInt256)20;
        Assert.Throws<OverflowException>(() => checked(smaller - larger));
    }

    [Fact(DisplayName = "UInt256 subtraction should match BigInteger oracle for large values")]
    public void UInt256SubtractionShouldMatchBigIntegerOracleForLargeValues()
    {
        UInt256 left = UInt256.Parse("99999999999999999999999999999999999999999999999999999999999");
        UInt256 right = UInt256.Parse("12345678901234567890123456789012345678901234567890123456789");
        UInt256 actual = left - right;
        BigInteger expected = (BigInteger)left - (BigInteger)right;
        Assert.Equal(expected, (BigInteger)actual);
    }

    [Fact(DisplayName = "UInt256 subtraction borrow propagating through upper should produce wrap")]
    public void UInt256SubtractionBorrowPropagatingThroughUpperShouldProduceWrap()
    {
        UInt256 left = new(UInt128.Zero, UInt128.Zero);
        UInt256 right = new(UInt128.Zero, UInt128.One);
        UInt256 result = left - right;
        Assert.Equal(UInt128.MaxValue, result.UpperBits);
        Assert.Equal(UInt128.MaxValue, result.LowerBits);
    }
}
