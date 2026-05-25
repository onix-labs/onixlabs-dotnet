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

public sealed class UInt512Tests
{
    [Fact(DisplayName = "UInt512.Zero should have both halves equal to zero")]
    public void UInt512ZeroShouldHaveBothHalvesEqualToZero()
    {
        Assert.Equal(UInt256.Zero, UInt512.Zero.UpperBits);
        Assert.Equal(UInt256.Zero, UInt512.Zero.LowerBits);
    }

    [Fact(DisplayName = "UInt512.One should have lower equal to one and upper equal to zero")]
    public void UInt512OneShouldHaveLowerEqualToOneAndUpperEqualToZero()
    {
        Assert.Equal(UInt256.Zero, UInt512.One.UpperBits);
        Assert.Equal(UInt256.One, UInt512.One.LowerBits);
    }

    [Fact(DisplayName = "UInt512.MaxValue should have both halves set to UInt256.MaxValue")]
    public void UInt512MaxValueShouldHaveBothHalvesSetToUInt256MaxValue()
    {
        Assert.Equal(UInt256.MaxValue, UInt512.MaxValue.UpperBits);
        Assert.Equal(UInt256.MaxValue, UInt512.MaxValue.LowerBits);
    }

    [Fact(DisplayName = "UInt512.MinValue should equal Zero")]
    public void UInt512MinValueShouldEqualZero()
    {
        Assert.Equal(UInt512.Zero, UInt512.MinValue);
    }

    [Fact(DisplayName = "UInt512.AllBitsSet should equal MaxValue")]
    public void UInt512AllBitsSetShouldEqualMaxValue()
    {
        Assert.Equal(UInt512.MaxValue, UInt512.AllBitsSet);
    }

    [Fact(DisplayName = "UInt512 constructor should preserve upper and lower halves")]
    public void UInt512ConstructorShouldPreserveHalves()
    {
        UInt256 upper = (UInt256)123UL;
        UInt256 lower = (UInt256)456UL;
        UInt512 value = new(upper, lower);
        Assert.Equal(upper, value.UpperBits);
        Assert.Equal(lower, value.LowerBits);
    }

    [Fact(DisplayName = "UInt512 addition should carry into the upper half")]
    public void UInt512AdditionShouldCarry()
    {
        UInt512 left = new(UInt256.Zero, UInt256.MaxValue);
        UInt512 right = new(UInt256.Zero, UInt256.One);
        UInt512 result = left + right;
        Assert.Equal(UInt256.One, result.UpperBits);
        Assert.Equal(UInt256.Zero, result.LowerBits);
    }

    [Fact(DisplayName = "UInt512 subtraction should borrow from the upper half")]
    public void UInt512SubtractionShouldBorrow()
    {
        UInt512 left = new(UInt256.One, UInt256.Zero);
        UInt512 right = new(UInt256.Zero, UInt256.One);
        UInt512 result = left - right;
        Assert.Equal(UInt256.Zero, result.UpperBits);
        Assert.Equal(UInt256.MaxValue, result.LowerBits);
    }

    [Fact(DisplayName = "UInt512 multiplication should match BigInteger")]
    public void UInt512MultiplicationShouldMatchBigInteger()
    {
        UInt512 left = UInt512.Parse("123456789012345678901234567890123456789012345678901234567890");
        UInt512 right = UInt512.Parse("987654321098765432109876543210987654321098765432109876543210");
        UInt512 product = left * right;
        BigInteger mask = (BigInteger.One << 512) - BigInteger.One;
        BigInteger expected = ((BigInteger)left * (BigInteger)right) & mask;
        Assert.Equal(expected, (BigInteger)product);
    }

    [Fact(DisplayName = "UInt512.BigMul of two UInt256 should match BigInteger multiplication")]
    public void UInt512BigMulShouldMatchBigInteger()
    {
        UInt256 left = UInt256.MaxValue;
        UInt256 right = UInt256.MaxValue;
        UInt512 product = UInt512.BigMul(left, right);
        BigInteger expected = (BigInteger)left * (BigInteger)right;
        Assert.Equal(expected, (BigInteger)product);
    }

    [Fact(DisplayName = "UInt512.BigMul of two UInt512 should expose the full 1024-bit product")]
    public void UInt512BigMulOfTwoUInt512ShouldExposeFullProduct()
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

    [Fact(DisplayName = "UInt512 division should match BigInteger")]
    public void UInt512DivisionShouldMatchBigInteger()
    {
        UInt512 dividend = UInt512.Parse("100000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000");
        UInt512 divisor = UInt512.Parse("123456789012345");
        UInt512 quotient = dividend / divisor;
        BigInteger expected = (BigInteger)dividend / (BigInteger)divisor;
        Assert.Equal(expected, (BigInteger)quotient);
    }

    [Fact(DisplayName = "UInt512 modulus should match BigInteger")]
    public void UInt512ModulusShouldMatchBigInteger()
    {
        UInt512 dividend = UInt512.Parse("100000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000");
        UInt512 divisor = UInt512.Parse("123456789012345");
        UInt512 remainder = dividend % divisor;
        BigInteger expected = (BigInteger)dividend % (BigInteger)divisor;
        Assert.Equal(expected, (BigInteger)remainder);
    }

    [Fact(DisplayName = "UInt512.DivRem should return both quotient and remainder")]
    public void UInt512DivRemShouldReturnBothQuotientAndRemainder()
    {
        UInt512 dividend = (UInt512)1234567890UL;
        UInt512 divisor = (UInt512)1000UL;
        (UInt512 quotient, UInt512 remainder) = UInt512.DivRem(dividend, divisor);
        Assert.Equal((UInt512)1234567UL, quotient);
        Assert.Equal((UInt512)890UL, remainder);
    }

    [Fact(DisplayName = "UInt512 shift left should match BigInteger")]
    public void UInt512ShiftLeftShouldMatchBigInteger()
    {
        UInt512 value = UInt512.One;
        UInt512 shifted = value << 400;
        Assert.Equal(BigInteger.One << 400, (BigInteger)shifted);
    }

    [Fact(DisplayName = "UInt512 shift right should match BigInteger")]
    public void UInt512ShiftRightShouldMatchBigInteger()
    {
        UInt512 value = UInt512.MaxValue;
        UInt512 shifted = value >> 400;
        BigInteger expected = ((BigInteger.One << 512) - BigInteger.One) >> 400;
        Assert.Equal(expected, (BigInteger)shifted);
    }

    [Fact(DisplayName = "UInt512 bitwise AND should AND the halves")]
    public void UInt512BitwiseAndShouldAndTheHalves()
    {
        UInt512 left = UInt512.MaxValue;
        UInt512 right = new(UInt256.Zero, UInt256.MaxValue);
        UInt512 result = left & right;
        Assert.Equal(right, result);
    }

    [Fact(DisplayName = "UInt512.LeadingZeroCount should count correctly")]
    public void UInt512LeadingZeroCountShouldCountCorrectly()
    {
        Assert.Equal((UInt512)512UL, UInt512.LeadingZeroCount(UInt512.Zero));
        Assert.Equal((UInt512)511UL, UInt512.LeadingZeroCount(UInt512.One));
        Assert.Equal(UInt512.Zero, UInt512.LeadingZeroCount(UInt512.MaxValue));
    }

    [Fact(DisplayName = "UInt512.TrailingZeroCount should count correctly")]
    public void UInt512TrailingZeroCountShouldCountCorrectly()
    {
        Assert.Equal((UInt512)512UL, UInt512.TrailingZeroCount(UInt512.Zero));
        Assert.Equal(UInt512.Zero, UInt512.TrailingZeroCount(UInt512.One));
        Assert.Equal((UInt512)256UL, UInt512.TrailingZeroCount(new UInt512(UInt256.One, UInt256.Zero)));
    }

    [Fact(DisplayName = "UInt512.PopCount should count set bits")]
    public void UInt512PopCountShouldCountSetBits()
    {
        Assert.Equal(UInt512.Zero, UInt512.PopCount(UInt512.Zero));
        Assert.Equal((UInt512)512UL, UInt512.PopCount(UInt512.MaxValue));
        Assert.Equal(UInt512.One, UInt512.PopCount(UInt512.One));
    }

    [Fact(DisplayName = "UInt512.Log2 should match BigInteger.Log2")]
    public void UInt512Log2ShouldMatchBigIntegerLog2()
    {
        UInt512 value = UInt512.One << 400;
        Assert.Equal((UInt512)400UL, UInt512.Log2(value));
    }

    [Fact(DisplayName = "UInt512.IsPow2 should identify powers of two")]
    public void UInt512IsPow2ShouldIdentifyPowersOfTwo()
    {
        Assert.True(UInt512.IsPow2(UInt512.One));
        Assert.True(UInt512.IsPow2((UInt512)1024UL));
        Assert.True(UInt512.IsPow2(UInt512.One << 400));
        Assert.False(UInt512.IsPow2(UInt512.Zero));
        Assert.False(UInt512.IsPow2((UInt512)3UL));
        Assert.False(UInt512.IsPow2(UInt512.MaxValue));
    }

    [Fact(DisplayName = "UInt512 equality should be by value")]
    public void UInt512EqualityShouldBeByValue()
    {
        UInt512 left = new((UInt256)42UL, (UInt256)100UL);
        UInt512 right = new((UInt256)42UL, (UInt256)100UL);
        Assert.Equal(left, right);
        Assert.True(left == right);
        Assert.True(left.Equals(right));
        Assert.Equal(left.GetHashCode(), right.GetHashCode());
    }

    [Fact(DisplayName = "UInt512 comparison should be by upper then lower")]
    public void UInt512ComparisonShouldBeByUpperThenLower()
    {
        UInt512 smaller = new(UInt256.Zero, UInt256.MaxValue);
        UInt512 larger = new(UInt256.One, UInt256.Zero);
        Assert.True(smaller < larger);
        Assert.True(larger > smaller);
    }

    [Fact(DisplayName = "UInt512.Parse should round-trip with ToString")]
    public void UInt512ParseShouldRoundTripWithToString()
    {
        string input = "13407807929942597099574024998205846127479365820592393377723561443721764030073546976801874298166903427690031858186486050853753882811946569946433649006084095";
        UInt512 value = UInt512.Parse(input);
        Assert.Equal(input, value.ToString());
    }

    [Fact(DisplayName = "UInt512.Parse of MaxValue plus one should throw OverflowException")]
    public void UInt512ParseOfMaxValuePlusOneShouldThrow()
    {
        string overflow = "13407807929942597099574024998205846127479365820592393377723561443721764030073546976801874298166903427690031858186486050853753882811946569946433649006084096";
        Assert.Throws<OverflowException>(() => UInt512.Parse(overflow));
    }

    [Fact(DisplayName = "UInt512 implicit conversions from primitives should preserve value")]
    public void UInt512ImplicitConversionsShouldPreserveValue()
    {
        Assert.Equal(UInt512.Zero, (UInt512)(byte)0);
        Assert.Equal((UInt512)42UL, (UInt512)42);
        Assert.Equal((UInt512)ulong.MaxValue, (UInt512)ulong.MaxValue);
        Assert.Equal((UInt512)(UInt128)UInt128.MaxValue, (UInt512)UInt128.MaxValue);
        Assert.Equal((UInt512)UInt256.MaxValue, (UInt512)UInt256.MaxValue);
    }

    [Fact(DisplayName = "UInt512 explicit narrowing conversions should match low bits")]
    public void UInt512ExplicitNarrowingShouldMatchLowBits()
    {
        UInt512 value = (UInt512)0x123456789ABCDEF0UL;
        Assert.Equal((byte)0xF0, (byte)value);
        Assert.Equal((ushort)0xDEF0, (ushort)value);
        Assert.Equal(0x9ABCDEF0u, (uint)value);
        Assert.Equal(0x123456789ABCDEF0UL, (ulong)value);
    }

    [Fact(DisplayName = "UInt512 checked narrowing should throw on overflow")]
    public void UInt512CheckedNarrowingShouldThrowOnOverflow()
    {
        UInt512 huge = UInt512.MaxValue;
        Assert.Throws<OverflowException>(() => checked((byte)huge));
        Assert.Throws<OverflowException>(() => checked((int)huge));
        Assert.Throws<OverflowException>(() => checked((ulong)huge));
        Assert.Throws<OverflowException>(() => checked((UInt128)huge));
        Assert.Throws<OverflowException>(() => checked((UInt256)huge));
    }

    [Fact(DisplayName = "UInt512.GetByteCount should return 64")]
    public void UInt512GetByteCountShouldReturn64()
    {
        UInt512 value = UInt512.MaxValue;
        Assert.Equal(64, value.GetByteCount());
    }

    [Fact(DisplayName = "UInt512.GetShortestBitLength should return the position of the highest set bit plus one")]
    public void UInt512GetShortestBitLengthShouldReturnHighSetBitPositionPlusOne()
    {
        Assert.Equal(0, UInt512.Zero.GetShortestBitLength());
        Assert.Equal(1, UInt512.One.GetShortestBitLength());
        Assert.Equal(512, UInt512.MaxValue.GetShortestBitLength());
    }

    [Fact(DisplayName = "UInt512.TryWriteBigEndian round trips through TryReadBigEndian")]
    public void UInt512TryWriteBigEndianRoundTripsThroughTryReadBigEndian()
    {
        UInt512 source = UInt512.Parse("12345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345");
        Span<byte> buffer = stackalloc byte[64];
        Assert.True(source.TryWriteBigEndian(buffer, out int written));
        Assert.Equal(64, written);

        Assert.True(UInt512.TryReadBigEndian(buffer, true, out UInt512 read));
        Assert.Equal(source, read);
    }

    [Fact(DisplayName = "UInt512.TryWriteLittleEndian round trips through TryReadLittleEndian")]
    public void UInt512TryWriteLittleEndianRoundTripsThroughTryReadLittleEndian()
    {
        UInt512 source = UInt512.Parse("98765432109876543210987654321098765432109876543210987654321098765432109876543210987654321098765");
        Span<byte> buffer = stackalloc byte[64];
        Assert.True(source.TryWriteLittleEndian(buffer, out int written));
        Assert.Equal(64, written);

        Assert.True(UInt512.TryReadLittleEndian(buffer, true, out UInt512 read));
        Assert.Equal(source, read);
    }

    [Fact(DisplayName = "UInt512.RotateLeft should rotate bits cyclically")]
    public void UInt512RotateLeftShouldRotateBitsCyclically()
    {
        UInt512 value = UInt512.One;
        UInt512 rotated = UInt512.RotateLeft(value, 511);
        UInt512 expected = UInt512.One << 511;
        Assert.Equal(expected, rotated);
    }

    [Fact(DisplayName = "UInt512.RotateRight should rotate bits cyclically")]
    public void UInt512RotateRightShouldRotateBitsCyclically()
    {
        UInt512 value = UInt512.One;
        UInt512 rotated = UInt512.RotateRight(value, 1);
        UInt512 expected = UInt512.One << 511;
        Assert.Equal(expected, rotated);
    }
}
