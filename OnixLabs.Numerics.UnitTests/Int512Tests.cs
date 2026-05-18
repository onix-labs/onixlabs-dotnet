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

public sealed class Int512Tests
{
    [Fact(DisplayName = "Int512.Zero should have both halves equal to zero")]
    public void Int512ZeroShouldHaveBothHalvesEqualToZero()
    {
        Assert.Equal(UInt256.Zero, Int512.Zero.UpperBits);
        Assert.Equal(UInt256.Zero, Int512.Zero.LowerBits);
    }

    [Fact(DisplayName = "Int512.NegativeOne should have all bits set")]
    public void Int512NegativeOneShouldHaveAllBitsSet()
    {
        Assert.Equal(UInt256.MaxValue, Int512.NegativeOne.UpperBits);
        Assert.Equal(UInt256.MaxValue, Int512.NegativeOne.LowerBits);
        Assert.True(Int512.IsNegative(Int512.NegativeOne));
    }

    [Fact(DisplayName = "Int512.MinValue should be -2^511")]
    public void Int512MinValueShouldBeNegativeTwoToThe511()
    {
        Assert.Equal(-(BigInteger.Pow(2, 511)), (BigInteger)Int512.MinValue);
    }

    [Fact(DisplayName = "Int512.MaxValue should be 2^511 - 1")]
    public void Int512MaxValueShouldBeTwoToThe511MinusOne()
    {
        Assert.Equal(BigInteger.Pow(2, 511) - BigInteger.One, (BigInteger)Int512.MaxValue);
    }

    [Fact(DisplayName = "Int512.AllBitsSet should equal NegativeOne")]
    public void Int512AllBitsSetShouldEqualNegativeOne()
    {
        Assert.Equal(Int512.NegativeOne, Int512.AllBitsSet);
    }

    [Fact(DisplayName = "Int512.IsNegative should detect negative values")]
    public void Int512IsNegativeShouldDetectNegativeValues()
    {
        Assert.False(Int512.IsNegative(Int512.Zero));
        Assert.False(Int512.IsNegative(Int512.One));
        Assert.False(Int512.IsNegative(Int512.MaxValue));
        Assert.True(Int512.IsNegative(Int512.NegativeOne));
        Assert.True(Int512.IsNegative(Int512.MinValue));
    }

    [Fact(DisplayName = "Int512.IsPositive should detect non-negative values")]
    public void Int512IsPositiveShouldDetectNonNegativeValues()
    {
        Assert.True(Int512.IsPositive(Int512.Zero));
        Assert.True(Int512.IsPositive(Int512.One));
        Assert.True(Int512.IsPositive(Int512.MaxValue));
        Assert.False(Int512.IsPositive(Int512.NegativeOne));
        Assert.False(Int512.IsPositive(Int512.MinValue));
    }

    [Fact(DisplayName = "Int512 signed comparison should treat negatives as less than positives")]
    public void Int512SignedComparisonShouldTreatNegativesAsLess()
    {
        Assert.True(Int512.NegativeOne < Int512.Zero);
        Assert.True(Int512.MinValue < Int512.MaxValue);
        Assert.True(Int512.MinValue < Int512.NegativeOne);
        Assert.True(Int512.MaxValue > Int512.Zero);
    }

    [Fact(DisplayName = "Int512 addition should wrap on overflow")]
    public void Int512AdditionShouldWrapOnOverflow()
    {
        Int512 result = Int512.MaxValue + Int512.One;
        Assert.Equal(Int512.MinValue, result);
    }

    [Fact(DisplayName = "Int512 checked addition should throw on overflow")]
    public void Int512CheckedAdditionShouldThrowOnOverflow()
    {
        Assert.Throws<OverflowException>(() => checked(Int512.MaxValue + Int512.One));
        Assert.Throws<OverflowException>(() => checked(Int512.MinValue + Int512.NegativeOne));
    }

    [Fact(DisplayName = "Int512 multiplication should match BigInteger for negative operands")]
    public void Int512MultiplicationShouldMatchBigIntegerForNegativeOperands()
    {
        Int512 left = (Int512)(-12345);
        Int512 right = (Int512)6789;
        Int512 product = left * right;
        Assert.Equal((BigInteger)(-12345) * 6789, (BigInteger)product);
    }

    [Fact(DisplayName = "Int512 checked multiplication should throw on overflow")]
    public void Int512CheckedMultiplicationShouldThrowOnOverflow()
    {
        Assert.Throws<OverflowException>(() => checked(Int512.MaxValue * (Int512)2));
        Assert.Throws<OverflowException>(() => checked(Int512.MinValue * (Int512)2));
    }

    [Fact(DisplayName = "Int512 division by negative one should match BigInteger")]
    public void Int512DivisionByNegativeOneShouldMatchBigInteger()
    {
        Assert.Equal(Int512.NegativeOne, Int512.One / Int512.NegativeOne);
        Assert.Equal((Int512)(-10), (Int512)10 / Int512.NegativeOne);
    }

    [Fact(DisplayName = "Int512.MinValue divided by NegativeOne should throw")]
    public void Int512MinValueDividedByNegativeOneShouldThrow()
    {
        Assert.Throws<OverflowException>(() => Int512.MinValue / Int512.NegativeOne);
    }

    [Fact(DisplayName = "Int512 modulus should preserve sign of dividend")]
    public void Int512ModulusShouldPreserveSignOfDividend()
    {
        Assert.Equal((Int512)(-1), (Int512)(-10) % (Int512)3);
        Assert.Equal((Int512)1, (Int512)10 % (Int512)3);
    }

    [Fact(DisplayName = "Int512 arithmetic right shift should preserve sign")]
    public void Int512ArithmeticRightShiftShouldPreserveSign()
    {
        Int512 negative = Int512.NegativeOne;
        Int512 shifted = negative >> 10;
        Assert.Equal(Int512.NegativeOne, shifted);

        Int512 positive = Int512.MaxValue;
        Int512 shiftedPositive = positive >> 1;
        Assert.False(Int512.IsNegative(shiftedPositive));
    }

    [Fact(DisplayName = "Int512 logical right shift should fill with zero")]
    public void Int512LogicalRightShiftShouldFillWithZero()
    {
        Int512 negative = Int512.NegativeOne;
        Int512 shifted = negative >>> 1;
        Assert.False(Int512.IsNegative(shifted));
        Assert.Equal(Int512.MaxValue, shifted);
    }

    [Fact(DisplayName = "Int512.Abs should return positive magnitude for negative input")]
    public void Int512AbsShouldReturnPositiveMagnitudeForNegativeInput()
    {
        Int512 negative = (Int512)(-42);
        Assert.Equal((Int512)42, Int512.Abs(negative));
    }

    [Fact(DisplayName = "Int512.Abs of MinValue should throw")]
    public void Int512AbsOfMinValueShouldThrow()
    {
        Assert.Throws<OverflowException>(() => Int512.Abs(Int512.MinValue));
    }

    [Fact(DisplayName = "Int512.Sign should return -1 for negative, 0 for zero, 1 for positive")]
    public void Int512SignShouldReturnExpectedValues()
    {
        Assert.Equal(-1, Int512.Sign(Int512.NegativeOne));
        Assert.Equal(0, Int512.Sign(Int512.Zero));
        Assert.Equal(1, Int512.Sign(Int512.One));
    }

    [Fact(DisplayName = "Int512.CopySign should copy the sign of the second operand")]
    public void Int512CopySignShouldCopyTheSignOfTheSecondOperand()
    {
        Assert.Equal((Int512)(-42), Int512.CopySign((Int512)42, Int512.NegativeOne));
        Assert.Equal((Int512)42, Int512.CopySign((Int512)(-42), Int512.One));
    }

    [Fact(DisplayName = "Int512.Negate should match BigInteger negation")]
    public void Int512NegateShouldMatchBigInteger()
    {
        Int512 value = (Int512)12345;
        Assert.Equal(-(BigInteger)12345, (BigInteger)(-value));
    }

    [Fact(DisplayName = "Int512.Negate of MinValue should wrap to MinValue")]
    public void Int512NegateOfMinValueShouldWrap()
    {
        Assert.Equal(Int512.MinValue, -Int512.MinValue);
    }

    [Fact(DisplayName = "Int512 checked unary negation should throw on MinValue")]
    public void Int512CheckedUnaryNegationShouldThrowOnMinValue()
    {
        Assert.Throws<OverflowException>(() => checked(-Int512.MinValue));
    }

    [Fact(DisplayName = "Int512 implicit conversions from signed primitives should preserve value")]
    public void Int512ImplicitFromSignedShouldPreserveValue()
    {
        Assert.Equal(Int512.NegativeOne, (Int512)(-1));
        Assert.Equal((Int512)(-128), (Int512)(sbyte)(-128));
        Assert.Equal((Int512)long.MinValue, (Int512)long.MinValue);
        Assert.Equal((Int512)Int128.MinValue, (Int512)Int128.MinValue);
        Assert.Equal((Int512)Int256.MinValue, (Int512)Int256.MinValue);
    }

    [Fact(DisplayName = "Int512.Parse should round-trip negative values")]
    public void Int512ParseShouldRoundTripNegativeValues()
    {
        string input = "-6703903964971298549787012499102923063739682910296196688861780721860882015036773488400937149083451713845015929093243025426876941405973284973216824503042048";
        Int512 value = Int512.Parse(input);
        Assert.Equal(input, value.ToString());
        Assert.Equal(Int512.MinValue, value);
    }

    [Fact(DisplayName = "Int512.Parse of overflow should throw")]
    public void Int512ParseOfOverflowShouldThrow()
    {
        string overflow = "6703903964971298549787012499102923063739682910296196688861780721860882015036773488400937149083451713845015929093243025426876941405973284973216824503042048";
        Assert.Throws<OverflowException>(() => Int512.Parse(overflow));
    }

    [Fact(DisplayName = "Int512 BigInteger round-trip should preserve negative values")]
    public void Int512BigIntegerRoundTripShouldPreserveNegativeValues()
    {
        BigInteger source = -BigInteger.Pow(2, 400);
        Int512 value = (Int512)source;
        Assert.Equal(source, (BigInteger)value);
    }

    [Fact(DisplayName = "Int512.TryWriteBigEndian should round-trip with TryReadBigEndian for negative values")]
    public void Int512TryWriteBigEndianRoundTripsForNegativeValues()
    {
        Int512 source = (Int512)(-(BigInteger.Pow(2, 300) + BigInteger.One));
        Span<byte> buffer = stackalloc byte[64];
        Assert.True(source.TryWriteBigEndian(buffer, out int written));
        Assert.Equal(64, written);

        Assert.True(Int512.TryReadBigEndian(buffer, false, out Int512 read));
        Assert.Equal(source, read);
    }

    [Fact(DisplayName = "Int512.TryWriteLittleEndian should round-trip with TryReadLittleEndian for negative values")]
    public void Int512TryWriteLittleEndianRoundTripsForNegativeValues()
    {
        Int512 source = (Int512)(-(BigInteger.Pow(2, 400)));
        Span<byte> buffer = stackalloc byte[64];
        Assert.True(source.TryWriteLittleEndian(buffer, out int written));
        Assert.Equal(64, written);

        Assert.True(Int512.TryReadLittleEndian(buffer, false, out Int512 read));
        Assert.Equal(source, read);
    }

    [Fact(DisplayName = "Int512 to UInt512 conversion should reinterpret bits")]
    public void Int512ToUInt512ConversionShouldReinterpretBits()
    {
        Int512 negative = Int512.NegativeOne;
        UInt512 unsigned = (UInt512)negative;
        Assert.Equal(UInt512.MaxValue, unsigned);
    }

    [Fact(DisplayName = "Checked Int512 to UInt512 should throw for negative")]
    public void CheckedInt512ToUInt512ShouldThrowForNegative()
    {
        Assert.Throws<OverflowException>(() => checked((UInt512)Int512.NegativeOne));
    }

    [Fact(DisplayName = "Int512.IsPow2 should be false for negatives")]
    public void Int512IsPow2ShouldBeFalseForNegatives()
    {
        Assert.False(Int512.IsPow2(Int512.NegativeOne));
        Assert.False(Int512.IsPow2(Int512.MinValue));
        Assert.True(Int512.IsPow2(Int512.One));
        Assert.True(Int512.IsPow2((Int512)1024));
    }

    [Fact(DisplayName = "Int512 multiplication should match BigInteger for large operands")]
    public void Int512MultiplicationShouldMatchBigIntegerForLargeOperands()
    {
        Int512 left = Int512.Parse("123456789012345678901234567890");
        Int512 right = Int512.Parse("-987654321098765432109876543210");
        Int512 product = left * right;
        Assert.Equal((BigInteger)left * (BigInteger)right, (BigInteger)product);
    }

    [Fact(DisplayName = "Int512.DivRem should match BigInteger.DivRem for negatives")]
    public void Int512DivRemShouldMatchBigIntegerForNegatives()
    {
        Int512 left = (Int512)(-100);
        Int512 right = (Int512)7;
        (Int512 quotient, Int512 remainder) = Int512.DivRem(left, right);
        BigInteger bigQuot = BigInteger.DivRem(-100, 7, out BigInteger bigRem);
        Assert.Equal(bigQuot, (BigInteger)quotient);
        Assert.Equal(bigRem, (BigInteger)remainder);
    }

    [Fact(DisplayName = "Int512.GetByteCount should return 64")]
    public void Int512GetByteCountShouldReturn64()
    {
        Assert.Equal(64, Int512.MaxValue.GetByteCount());
    }

    [Fact(DisplayName = "Int512 widening from Int256 should preserve negative values")]
    public void Int512WideningFromInt256ShouldPreserveNegative()
    {
        Int256 source = Int256.MinValue;
        Int512 widened = source;
        Assert.Equal((BigInteger)source, (BigInteger)widened);
    }

    [Fact(DisplayName = "Int512 narrowing to Int256 should reinterpret low bits")]
    public void Int512NarrowingToInt256ShouldReinterpretLowBits()
    {
        Int512 source = (Int512)(-42);
        Int256 narrowed = (Int256)source;
        Assert.Equal((Int256)(-42), narrowed);
    }

    [Fact(DisplayName = "Checked Int512 narrowing to Int256 should throw on overflow")]
    public void CheckedInt512NarrowingToInt256ShouldThrowOnOverflow()
    {
        Int512 source = (Int512)Int256.MaxValue + Int512.One;
        Assert.Throws<OverflowException>(() => checked((Int256)source));
    }
}
