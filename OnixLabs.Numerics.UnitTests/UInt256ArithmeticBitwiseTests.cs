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

namespace OnixLabs.Numerics.UnitTests;

public sealed class UInt256ArithmeticBitwiseTests
{
    private static readonly UInt256 AlternatingHigh = new(new UInt128(0xAAAA_AAAA_AAAA_AAAAUL, 0xAAAA_AAAA_AAAA_AAAAUL), new UInt128(0xAAAA_AAAA_AAAA_AAAAUL, 0xAAAA_AAAA_AAAA_AAAAUL));
    private static readonly UInt256 AlternatingLow = new(new UInt128(0x5555_5555_5555_5555UL, 0x5555_5555_5555_5555UL), new UInt128(0x5555_5555_5555_5555UL, 0x5555_5555_5555_5555UL));

    [Fact(DisplayName = "UInt256 bitwise AND of a value with MaxValue should return the value")]
    public void UInt256BitwiseAndWithMaxValueShouldReturnValue()
    {
        UInt256 value = AlternatingHigh;
        Assert.Equal(value, value & UInt256.MaxValue);
    }

    [Fact(DisplayName = "UInt256 bitwise AND of complementary patterns should be zero")]
    public void UInt256BitwiseAndOfComplementaryPatternsShouldBeZero()
    {
        Assert.Equal(UInt256.Zero, AlternatingHigh & AlternatingLow);
    }

    [Fact(DisplayName = "UInt256 bitwise AND should AND each half independently")]
    public void UInt256BitwiseAndShouldAndEachHalfIndependently()
    {
        UInt256 left = new(UInt128.MaxValue, UInt128.Zero);
        UInt256 right = new(UInt128.Zero, UInt128.MaxValue);
        Assert.Equal(UInt256.Zero, left & right);
        Assert.Equal(new UInt256(UInt128.MaxValue, UInt128.Zero), left & left);
    }

    [Fact(DisplayName = "UInt256 bitwise OR of complementary alternating patterns should equal MaxValue")]
    public void UInt256BitwiseOrOfComplementaryAlternatingShouldEqualMaxValue()
    {
        Assert.Equal(UInt256.MaxValue, AlternatingHigh | AlternatingLow);
    }

    [Fact(DisplayName = "UInt256 bitwise OR with zero should return the other operand")]
    public void UInt256BitwiseOrWithZeroShouldReturnOtherOperand()
    {
        UInt256 value = new((UInt128)42, (UInt128)1024);
        Assert.Equal(value, value | UInt256.Zero);
        Assert.Equal(value, UInt256.Zero | value);
    }

    [Fact(DisplayName = "UInt256 bitwise XOR of value with itself should return zero")]
    public void UInt256BitwiseXorOfValueWithItselfShouldReturnZero()
    {
        UInt256 value = AlternatingHigh;
        Assert.Equal(UInt256.Zero, value ^ value);
    }

    [Fact(DisplayName = "UInt256 bitwise XOR of complementary patterns should equal MaxValue")]
    public void UInt256BitwiseXorOfComplementaryPatternsShouldEqualMaxValue()
    {
        Assert.Equal(UInt256.MaxValue, AlternatingHigh ^ AlternatingLow);
    }

    [Fact(DisplayName = "UInt256 bitwise NOT of zero should equal MaxValue")]
    public void UInt256BitwiseNotOfZeroShouldEqualMaxValue()
    {
        Assert.Equal(UInt256.MaxValue, ~UInt256.Zero);
    }

    [Fact(DisplayName = "UInt256 bitwise NOT of MaxValue should equal zero")]
    public void UInt256BitwiseNotOfMaxValueShouldEqualZero()
    {
        Assert.Equal(UInt256.Zero, ~UInt256.MaxValue);
    }

    [Fact(DisplayName = "UInt256 bitwise NOT applied twice should return the original value")]
    public void UInt256BitwiseNotTwiceShouldReturnOriginal()
    {
        UInt256 value = AlternatingHigh;
        Assert.Equal(value, ~~value);
    }

    [Fact(DisplayName = "UInt256 bitwise NOT of alternating high pattern should produce alternating low")]
    public void UInt256BitwiseNotOfAlternatingHighShouldProduceAlternatingLow()
    {
        Assert.Equal(AlternatingLow, ~AlternatingHigh);
        Assert.Equal(AlternatingHigh, ~AlternatingLow);
    }

    [Fact(DisplayName = "UInt256 bitwise operations should be associative for OR and AND")]
    public void UInt256BitwiseOperationsShouldBeAssociative()
    {
        UInt256 a = new(UInt128.MaxValue, UInt128.Zero);
        UInt256 b = new((UInt128)42, (UInt128)1024);
        UInt256 c = new(UInt128.Zero, UInt128.MaxValue);
        Assert.Equal((a | b) | c, a | (b | c));
        Assert.Equal((a & b) & c, a & (b & c));
        Assert.Equal((a ^ b) ^ c, a ^ (b ^ c));
    }
}
