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

public sealed class UInt256ArithmeticAdditionTests
{
    [Fact(DisplayName = "UInt256 addition of zero plus value should return the value")]
    public void UInt256AdditionOfZeroPlusValueShouldReturnValue()
    {
        UInt256 value = (UInt256)12345;
        Assert.Equal(value, UInt256.Zero + value);
        Assert.Equal(value, value + UInt256.Zero);
    }

    [Fact(DisplayName = "UInt256 addition with no carry should add the halves independently")]
    public void UInt256AdditionWithoutCarryShouldAddHalvesIndependently()
    {
        UInt256 left = new((UInt128)1, (UInt128)2);
        UInt256 right = new((UInt128)3, (UInt128)4);
        UInt256 result = left + right;
        Assert.Equal((UInt128)4, result.Upper);
        Assert.Equal((UInt128)6, result.Lower);
    }

    [Fact(DisplayName = "UInt256 addition with carry exactly at the half-boundary should produce upper = 1")]
    public void UInt256AdditionWithCarryAtBoundaryShouldProduceUpperOne()
    {
        UInt256 left = new(UInt128.Zero, UInt128.MaxValue);
        UInt256 right = new(UInt128.Zero, UInt128.One);
        UInt256 result = left + right;
        Assert.Equal(UInt128.One, result.Upper);
        Assert.Equal(UInt128.Zero, result.Lower);
    }

    [Fact(DisplayName = "UInt256 addition with carry and upper sum should add the carry to the upper sum")]
    public void UInt256AdditionWithCarryAndUpperSumShouldIncludeCarry()
    {
        UInt256 left = new((UInt128)5, UInt128.MaxValue);
        UInt256 right = new((UInt128)7, UInt128.One);
        UInt256 result = left + right;
        Assert.Equal((UInt128)13, result.Upper);
        Assert.Equal(UInt128.Zero, result.Lower);
    }

    [Fact(DisplayName = "UInt256 addition should wrap when sum exceeds MaxValue")]
    public void UInt256AdditionShouldWrapWhenSumExceedsMaxValue()
    {
        UInt256 result = UInt256.MaxValue + UInt256.One;
        Assert.Equal(UInt256.Zero, result);
    }

    [Fact(DisplayName = "UInt256 checked addition with no overflow should match unchecked addition")]
    public void UInt256CheckedAdditionWithoutOverflowShouldMatchUnchecked()
    {
        UInt256 left = (UInt256)1000;
        UInt256 right = (UInt256)2000;
        Assert.Equal(left + right, checked(left + right));
    }

    [Fact(DisplayName = "UInt256 checked addition MaxValue plus One should throw")]
    public void UInt256CheckedAdditionMaxPlusOneShouldThrow()
    {
        Assert.Throws<OverflowException>(() => checked(UInt256.MaxValue + UInt256.One));
    }

    [Fact(DisplayName = "UInt256 checked addition should throw when carry overflows the upper sum")]
    public void UInt256CheckedAdditionShouldThrowWhenCarryOverflowsUpperSum()
    {
        // Upper sum fits, then carry causes overflow.
        UInt256 left = new(UInt128.MaxValue, UInt128.MaxValue);
        UInt256 right = new(UInt128.Zero, UInt128.One);
        Assert.Throws<OverflowException>(() => checked(left + right));
    }

    [Fact(DisplayName = "UInt256 addition should match BigInteger oracle for large values")]
    public void UInt256AdditionShouldMatchBigIntegerOracleForLargeValues()
    {
        UInt256 left = UInt256.Parse("11579208923731619542357098500868790785326998466564056403945758400791312963993");
        UInt256 right = UInt256.Parse("23158417847463239084714197001737581570653996933128112807891516801582625927986");
        UInt256 actual = left + right;
        BigInteger expected = (BigInteger)left + (BigInteger)right;
        Assert.Equal(expected, (BigInteger)actual);
    }

    [Fact(DisplayName = "UInt256 addition should be commutative")]
    public void UInt256AdditionShouldBeCommutative()
    {
        UInt256 left = UInt256.Parse("123456789012345678901234567890");
        UInt256 right = UInt256.Parse("987654321098765432109876543210");
        Assert.Equal(left + right, right + left);
    }
}
