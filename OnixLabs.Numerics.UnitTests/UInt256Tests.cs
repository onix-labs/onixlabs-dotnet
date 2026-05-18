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

public sealed class UInt256Tests
{
    [Fact(DisplayName = "UInt256.Zero should have both halves equal to zero")]
    public void Float256ZeroShouldHaveBothHalvesEqualToZero()
    {
        Assert.Equal(UInt128.Zero, UInt256.Zero.Upper);
        Assert.Equal(UInt128.Zero, UInt256.Zero.Lower);
    }

    [Fact(DisplayName = "UInt256.One should have lower equal to one and upper equal to zero")]
    public void UInt256OneShouldHaveLowerEqualToOneAndUpperEqualToZero()
    {
        Assert.Equal(UInt128.Zero, UInt256.One.Upper);
        Assert.Equal(UInt128.One, UInt256.One.Lower);
    }

    [Fact(DisplayName = "UInt256.MaxValue should have both halves set to UInt128.MaxValue")]
    public void UInt256MaxValueShouldHaveBothHalvesSetToUInt128MaxValue()
    {
        Assert.Equal(UInt128.MaxValue, UInt256.MaxValue.Upper);
        Assert.Equal(UInt128.MaxValue, UInt256.MaxValue.Lower);
    }

    [Fact(DisplayName = "UInt256.MinValue should equal Zero")]
    public void UInt256MinValueShouldEqualZero()
    {
        Assert.Equal(UInt256.Zero, UInt256.MinValue);
    }

    [Fact(DisplayName = "UInt256.AllBitsSet should equal MaxValue")]
    public void UInt256AllBitsSetShouldEqualMaxValue()
    {
        Assert.Equal(UInt256.MaxValue, UInt256.AllBitsSet);
    }

    [Fact(DisplayName = "UInt256 constructor should preserve upper and lower halves")]
    public void UInt256ConstructorShouldPreserveHalves()
    {
        UInt128 upper = (UInt128)123;
        UInt128 lower = (UInt128)456;
        UInt256 value = new(upper, lower);
        Assert.Equal(upper, value.Upper);
        Assert.Equal(lower, value.Lower);
    }

    [Fact(DisplayName = "UInt256 addition should carry into the upper half")]
    public void UInt256AdditionShouldCarry()
    {
        UInt256 left = new(UInt128.Zero, UInt128.MaxValue);
        UInt256 right = new(UInt128.Zero, UInt128.One);
        UInt256 result = left + right;
        Assert.Equal(UInt128.One, result.Upper);
        Assert.Equal(UInt128.Zero, result.Lower);
    }

    [Fact(DisplayName = "UInt256 subtraction should borrow from the upper half")]
    public void UInt256SubtractionShouldBorrow()
    {
        UInt256 left = new(UInt128.One, UInt128.Zero);
        UInt256 right = new(UInt128.Zero, UInt128.One);
        UInt256 result = left - right;
        Assert.Equal(UInt128.Zero, result.Upper);
        Assert.Equal(UInt128.MaxValue, result.Lower);
    }

    [Fact(DisplayName = "UInt256 multiplication should match BigInteger")]
    public void UInt256MultiplicationShouldMatchBigInteger()
    {
        UInt256 left = UInt256.Parse("123456789012345678901234567890");
        UInt256 right = UInt256.Parse("987654321098765432109876543210");
        UInt256 product = left * right;
        BigInteger expected = (BigInteger)left * (BigInteger)right;
        Assert.Equal(expected, (BigInteger)product);
    }

    [Fact(DisplayName = "UInt256.BigMul of two UInt128 should match BigInteger multiplication")]
    public void UInt256BigMulShouldMatchBigInteger()
    {
        UInt128 left = UInt128.MaxValue;
        UInt128 right = UInt128.MaxValue;
        UInt256 product = UInt256.BigMul(left, right);
        BigInteger expected = (BigInteger)left * (BigInteger)right;
        Assert.Equal(expected, (BigInteger)product);
    }

    [Fact(DisplayName = "UInt256 division should match BigInteger")]
    public void UInt256DivisionShouldMatchBigInteger()
    {
        UInt256 dividend = UInt256.Parse("100000000000000000000000000000000000000000000");
        UInt256 divisor = UInt256.Parse("123456789");
        UInt256 quotient = dividend / divisor;
        BigInteger expected = (BigInteger)dividend / (BigInteger)divisor;
        Assert.Equal(expected, (BigInteger)quotient);
    }

    [Fact(DisplayName = "UInt256 modulus should match BigInteger")]
    public void UInt256ModulusShouldMatchBigInteger()
    {
        UInt256 dividend = UInt256.Parse("100000000000000000000000000000000000000000000");
        UInt256 divisor = UInt256.Parse("123456789");
        UInt256 remainder = dividend % divisor;
        BigInteger expected = (BigInteger)dividend % (BigInteger)divisor;
        Assert.Equal(expected, (BigInteger)remainder);
    }

    [Fact(DisplayName = "UInt256.DivRem should return both quotient and remainder")]
    public void UInt256DivRemShouldReturnBothQuotientAndRemainder()
    {
        UInt256 dividend = (UInt256)1234567890UL;
        UInt256 divisor = (UInt256)1000UL;
        (UInt256 quotient, UInt256 remainder) = UInt256.DivRem(dividend, divisor);
        Assert.Equal((UInt256)1234567UL, quotient);
        Assert.Equal((UInt256)890UL, remainder);
    }

    [Fact(DisplayName = "UInt256 shift left should match BigInteger")]
    public void UInt256ShiftLeftShouldMatchBigInteger()
    {
        UInt256 value = UInt256.One;
        UInt256 shifted = value << 200;
        Assert.Equal(BigInteger.One << 200, (BigInteger)shifted);
    }

    [Fact(DisplayName = "UInt256 shift right should match BigInteger")]
    public void UInt256ShiftRightShouldMatchBigInteger()
    {
        UInt256 value = UInt256.MaxValue;
        UInt256 shifted = value >> 200;
        BigInteger expected = ((BigInteger.One << 256) - BigInteger.One) >> 200;
        Assert.Equal(expected, (BigInteger)shifted);
    }

    [Fact(DisplayName = "UInt256 bitwise AND should AND the halves")]
    public void UInt256BitwiseAndShouldAndTheHalves()
    {
        UInt256 left = UInt256.MaxValue;
        UInt256 right = new(UInt128.Zero, UInt128.MaxValue);
        UInt256 result = left & right;
        Assert.Equal(right, result);
    }

    [Fact(DisplayName = "UInt256.LeadingZeroCount should count correctly")]
    public void UInt256LeadingZeroCountShouldCountCorrectly()
    {
        Assert.Equal((UInt256)256, UInt256.LeadingZeroCount(UInt256.Zero));
        Assert.Equal((UInt256)255, UInt256.LeadingZeroCount(UInt256.One));
        Assert.Equal(UInt256.Zero, UInt256.LeadingZeroCount(UInt256.MaxValue));
    }

    [Fact(DisplayName = "UInt256.TrailingZeroCount should count correctly")]
    public void UInt256TrailingZeroCountShouldCountCorrectly()
    {
        Assert.Equal((UInt256)256, UInt256.TrailingZeroCount(UInt256.Zero));
        Assert.Equal(UInt256.Zero, UInt256.TrailingZeroCount(UInt256.One));
        Assert.Equal((UInt256)128, UInt256.TrailingZeroCount(new UInt256(UInt128.One, UInt128.Zero)));
    }

    [Fact(DisplayName = "UInt256.PopCount should count set bits")]
    public void UInt256PopCountShouldCountSetBits()
    {
        Assert.Equal(UInt256.Zero, UInt256.PopCount(UInt256.Zero));
        Assert.Equal((UInt256)256, UInt256.PopCount(UInt256.MaxValue));
        Assert.Equal(UInt256.One, UInt256.PopCount(UInt256.One));
    }

    [Fact(DisplayName = "UInt256.Log2 should match BigInteger.Log2")]
    public void UInt256Log2ShouldMatchBigIntegerLog2()
    {
        UInt256 value = UInt256.One << 200;
        Assert.Equal((UInt256)200, UInt256.Log2(value));
    }

    [Fact(DisplayName = "UInt256.IsPow2 should identify powers of two")]
    public void UInt256IsPow2ShouldIdentifyPowersOfTwo()
    {
        Assert.True(UInt256.IsPow2(UInt256.One));
        Assert.True(UInt256.IsPow2((UInt256)1024));
        Assert.True(UInt256.IsPow2(UInt256.One << 200));
        Assert.False(UInt256.IsPow2(UInt256.Zero));
        Assert.False(UInt256.IsPow2((UInt256)3));
        Assert.False(UInt256.IsPow2(UInt256.MaxValue));
    }

    [Fact(DisplayName = "UInt256 equality should be by value")]
    public void UInt256EqualityShouldBeByValue()
    {
        UInt256 left = new((UInt128)42, (UInt128)100);
        UInt256 right = new((UInt128)42, (UInt128)100);
        Assert.Equal(left, right);
        Assert.True(left == right);
        Assert.True(left.Equals(right));
        Assert.Equal(left.GetHashCode(), right.GetHashCode());
    }

    [Fact(DisplayName = "UInt256 comparison should be by upper then lower")]
    public void UInt256ComparisonShouldBeByUpperThenLower()
    {
        UInt256 smaller = new(UInt128.Zero, UInt128.MaxValue);
        UInt256 larger = new(UInt128.One, UInt128.Zero);
        Assert.True(smaller < larger);
        Assert.True(larger > smaller);
    }

    [Fact(DisplayName = "UInt256.Parse should round-trip with ToString")]
    public void UInt256ParseShouldRoundTripWithToString()
    {
        string input = "115792089237316195423570985008687907853269984665640564039457584007913129639935";
        UInt256 value = UInt256.Parse(input);
        Assert.Equal(input, value.ToString());
    }

    [Fact(DisplayName = "UInt256.Parse of MaxValue plus one should throw OverflowException")]
    public void UInt256ParseOfMaxValuePlusOneShouldThrow()
    {
        string overflow = "115792089237316195423570985008687907853269984665640564039457584007913129639936";
        Assert.Throws<OverflowException>(() => UInt256.Parse(overflow));
    }

    [Fact(DisplayName = "UInt256 implicit conversions from primitives should preserve value")]
    public void UInt256ImplicitConversionsShouldPreserveValue()
    {
        Assert.Equal(UInt256.Zero, (UInt256)(byte)0);
        Assert.Equal((UInt256)42UL, (UInt256)42);
        Assert.Equal((UInt256)ulong.MaxValue, (UInt256)ulong.MaxValue);
        Assert.Equal((UInt256)(UInt128)UInt128.MaxValue, (UInt256)UInt128.MaxValue);
    }

    [Fact(DisplayName = "UInt256 explicit narrowing conversions should match low bits")]
    public void UInt256ExplicitNarrowingShouldMatchLowBits()
    {
        UInt256 value = (UInt256)0x123456789ABCDEF0UL;
        Assert.Equal((byte)0xF0, (byte)value);
        Assert.Equal((ushort)0xDEF0, (ushort)value);
        Assert.Equal(0x9ABCDEF0u, (uint)value);
        Assert.Equal(0x123456789ABCDEF0UL, (ulong)value);
    }

    [Fact(DisplayName = "UInt256 checked narrowing should throw on overflow")]
    public void UInt256CheckedNarrowingShouldThrowOnOverflow()
    {
        UInt256 huge = UInt256.MaxValue;
        Assert.Throws<OverflowException>(() => checked((byte)huge));
        Assert.Throws<OverflowException>(() => checked((int)huge));
        Assert.Throws<OverflowException>(() => checked((ulong)huge));
        Assert.Throws<OverflowException>(() => checked((UInt128)huge));
    }

    [Fact(DisplayName = "UInt256.GetByteCount should return 32")]
    public void UInt256GetByteCountShouldReturn32()
    {
        UInt256 value = UInt256.MaxValue;
        Assert.Equal(32, value.GetByteCount());
    }

    [Fact(DisplayName = "UInt256.GetShortestBitLength should return the position of the highest set bit plus one")]
    public void UInt256GetShortestBitLengthShouldReturnHighSetBitPositionPlusOne()
    {
        Assert.Equal(0, UInt256.Zero.GetShortestBitLength());
        Assert.Equal(1, UInt256.One.GetShortestBitLength());
        Assert.Equal(256, UInt256.MaxValue.GetShortestBitLength());
    }

    [Fact(DisplayName = "UInt256.TryWriteBigEndian round trips through TryReadBigEndian")]
    public void UInt256TryWriteBigEndianRoundTripsThroughTryReadBigEndian()
    {
        UInt256 source = UInt256.Parse("123456789012345678901234567890123456789012345678901234567890123456789012345");
        Span<byte> buffer = stackalloc byte[32];
        Assert.True(source.TryWriteBigEndian(buffer, out int written));
        Assert.Equal(32, written);

        Assert.True(UInt256.TryReadBigEndian(buffer, true, out UInt256 read));
        Assert.Equal(source, read);
    }

    [Fact(DisplayName = "UInt256.TryWriteLittleEndian round trips through TryReadLittleEndian")]
    public void UInt256TryWriteLittleEndianRoundTripsThroughTryReadLittleEndian()
    {
        UInt256 source = UInt256.Parse("987654321098765432109876543210987654321098765432109876543210987654321098765");
        Span<byte> buffer = stackalloc byte[32];
        Assert.True(source.TryWriteLittleEndian(buffer, out int written));
        Assert.Equal(32, written);

        Assert.True(UInt256.TryReadLittleEndian(buffer, true, out UInt256 read));
        Assert.Equal(source, read);
    }

    [Fact(DisplayName = "UInt256.RotateLeft should rotate bits cyclically")]
    public void UInt256RotateLeftShouldRotateBitsCyclically()
    {
        UInt256 value = UInt256.One;
        UInt256 rotated = UInt256.RotateLeft(value, 255);
        UInt256 expected = UInt256.One << 255;
        Assert.Equal(expected, rotated);
    }

    [Fact(DisplayName = "UInt256.RotateRight should rotate bits cyclically")]
    public void UInt256RotateRightShouldRotateBitsCyclically()
    {
        UInt256 value = UInt256.One;
        UInt256 rotated = UInt256.RotateRight(value, 1);
        UInt256 expected = UInt256.One << 255;
        Assert.Equal(expected, rotated);
    }
}
