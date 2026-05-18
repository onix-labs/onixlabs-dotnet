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

public sealed class Int256Tests
{
    [Fact(DisplayName = "Int256.Zero should have both halves equal to zero")]
    public void Int256ZeroShouldHaveBothHalvesEqualToZero()
    {
        Assert.Equal(UInt128.Zero, Int256.Zero.Upper);
        Assert.Equal(UInt128.Zero, Int256.Zero.Lower);
    }

    [Fact(DisplayName = "Int256.NegativeOne should have all bits set")]
    public void Int256NegativeOneShouldHaveAllBitsSet()
    {
        Assert.Equal(UInt128.MaxValue, Int256.NegativeOne.Upper);
        Assert.Equal(UInt128.MaxValue, Int256.NegativeOne.Lower);
        Assert.True(Int256.IsNegative(Int256.NegativeOne));
    }

    [Fact(DisplayName = "Int256.MinValue should be -2^255")]
    public void Int256MinValueShouldBeNegativeTwoToThe255()
    {
        Assert.Equal(-(BigInteger.Pow(2, 255)), (BigInteger)Int256.MinValue);
    }

    [Fact(DisplayName = "Int256.MaxValue should be 2^255 - 1")]
    public void Int256MaxValueShouldBeTwoToThe255MinusOne()
    {
        Assert.Equal(BigInteger.Pow(2, 255) - BigInteger.One, (BigInteger)Int256.MaxValue);
    }

    [Fact(DisplayName = "Int256.AllBitsSet should equal NegativeOne")]
    public void Int256AllBitsSetShouldEqualNegativeOne()
    {
        Assert.Equal(Int256.NegativeOne, Int256.AllBitsSet);
    }

    [Fact(DisplayName = "Int256.IsNegative should detect negative values")]
    public void Int256IsNegativeShouldDetectNegativeValues()
    {
        Assert.False(Int256.IsNegative(Int256.Zero));
        Assert.False(Int256.IsNegative(Int256.One));
        Assert.False(Int256.IsNegative(Int256.MaxValue));
        Assert.True(Int256.IsNegative(Int256.NegativeOne));
        Assert.True(Int256.IsNegative(Int256.MinValue));
    }

    [Fact(DisplayName = "Int256.IsPositive should detect non-negative values")]
    public void Int256IsPositiveShouldDetectNonNegativeValues()
    {
        Assert.True(Int256.IsPositive(Int256.Zero));
        Assert.True(Int256.IsPositive(Int256.One));
        Assert.True(Int256.IsPositive(Int256.MaxValue));
        Assert.False(Int256.IsPositive(Int256.NegativeOne));
        Assert.False(Int256.IsPositive(Int256.MinValue));
    }

    [Fact(DisplayName = "Int256 signed comparison should treat negatives as less than positives")]
    public void Int256SignedComparisonShouldTreatNegativesAsLess()
    {
        Assert.True(Int256.NegativeOne < Int256.Zero);
        Assert.True(Int256.MinValue < Int256.MaxValue);
        Assert.True(Int256.MinValue < Int256.NegativeOne);
        Assert.True(Int256.MaxValue > Int256.Zero);
    }

    [Fact(DisplayName = "Int256 addition should wrap on overflow")]
    public void Int256AdditionShouldWrapOnOverflow()
    {
        Int256 result = Int256.MaxValue + Int256.One;
        Assert.Equal(Int256.MinValue, result);
    }

    [Fact(DisplayName = "Int256 checked addition should throw on overflow")]
    public void Int256CheckedAdditionShouldThrowOnOverflow()
    {
        Assert.Throws<OverflowException>(() => checked(Int256.MaxValue + Int256.One));
        Assert.Throws<OverflowException>(() => checked(Int256.MinValue + Int256.NegativeOne));
    }

    [Fact(DisplayName = "Int256 multiplication should match BigInteger for negative operands")]
    public void Int256MultiplicationShouldMatchBigIntegerForNegativeOperands()
    {
        Int256 left = (Int256)(-12345);
        Int256 right = (Int256)6789;
        Int256 product = left * right;
        Assert.Equal((BigInteger)(-12345) * 6789, (BigInteger)product);
    }

    [Fact(DisplayName = "Int256 checked multiplication should throw on overflow")]
    public void Int256CheckedMultiplicationShouldThrowOnOverflow()
    {
        Assert.Throws<OverflowException>(() => checked(Int256.MaxValue * (Int256)2));
        Assert.Throws<OverflowException>(() => checked(Int256.MinValue * (Int256)2));
    }

    [Fact(DisplayName = "Int256 division by negative one should match BigInteger")]
    public void Int256DivisionByNegativeOneShouldMatchBigInteger()
    {
        Assert.Equal(Int256.NegativeOne, Int256.One / Int256.NegativeOne);
        Assert.Equal((Int256)(-10), (Int256)10 / Int256.NegativeOne);
    }

    [Fact(DisplayName = "Int256.MinValue divided by NegativeOne should throw")]
    public void Int256MinValueDividedByNegativeOneShouldThrow()
    {
        Assert.Throws<OverflowException>(() => Int256.MinValue / Int256.NegativeOne);
    }

    [Fact(DisplayName = "Int256 modulus should preserve sign of dividend")]
    public void Int256ModulusShouldPreserveSignOfDividend()
    {
        Assert.Equal((Int256)(-1), (Int256)(-10) % (Int256)3);
        Assert.Equal((Int256)1, (Int256)10 % (Int256)3);
    }

    [Fact(DisplayName = "Int256 arithmetic right shift should preserve sign")]
    public void Int256ArithmeticRightShiftShouldPreserveSign()
    {
        Int256 negative = Int256.NegativeOne;
        Int256 shifted = negative >> 10;
        Assert.Equal(Int256.NegativeOne, shifted);

        Int256 positive = Int256.MaxValue;
        Int256 shiftedPositive = positive >> 1;
        Assert.False(Int256.IsNegative(shiftedPositive));
    }

    [Fact(DisplayName = "Int256 logical right shift should fill with zero")]
    public void Int256LogicalRightShiftShouldFillWithZero()
    {
        Int256 negative = Int256.NegativeOne;
        Int256 shifted = negative >>> 1;
        Assert.False(Int256.IsNegative(shifted));
        Assert.Equal(Int256.MaxValue, shifted);
    }

    [Fact(DisplayName = "Int256.Abs should return positive magnitude for negative input")]
    public void Int256AbsShouldReturnPositiveMagnitudeForNegativeInput()
    {
        Int256 negative = (Int256)(-42);
        Assert.Equal((Int256)42, Int256.Abs(negative));
    }

    [Fact(DisplayName = "Int256.Abs of MinValue should throw")]
    public void Int256AbsOfMinValueShouldThrow()
    {
        Assert.Throws<OverflowException>(() => Int256.Abs(Int256.MinValue));
    }

    [Fact(DisplayName = "Int256.Sign should return -1 for negative, 0 for zero, 1 for positive")]
    public void Int256SignShouldReturnExpectedValues()
    {
        Assert.Equal(-1, Int256.Sign(Int256.NegativeOne));
        Assert.Equal(0, Int256.Sign(Int256.Zero));
        Assert.Equal(1, Int256.Sign(Int256.One));
    }

    [Fact(DisplayName = "Int256.CopySign should copy the sign of the second operand")]
    public void Int256CopySignShouldCopyTheSignOfTheSecondOperand()
    {
        Assert.Equal((Int256)(-42), Int256.CopySign((Int256)42, Int256.NegativeOne));
        Assert.Equal((Int256)42, Int256.CopySign((Int256)(-42), Int256.One));
    }

    [Fact(DisplayName = "Int256.Negate should match BigInteger negation")]
    public void Int256NegateShouldMatchBigInteger()
    {
        Int256 value = (Int256)12345;
        Assert.Equal(-(BigInteger)12345, (BigInteger)(-value));
    }

    [Fact(DisplayName = "Int256.Negate of MinValue should wrap to MinValue")]
    public void Int256NegateOfMinValueShouldWrap()
    {
        Assert.Equal(Int256.MinValue, -Int256.MinValue);
    }

    [Fact(DisplayName = "Int256 checked unary negation should throw on MinValue")]
    public void Int256CheckedUnaryNegationShouldThrowOnMinValue()
    {
        Assert.Throws<OverflowException>(() => checked(-Int256.MinValue));
    }

    [Fact(DisplayName = "Int256 implicit conversions from signed primitives should preserve value")]
    public void Int256ImplicitFromSignedShouldPreserveValue()
    {
        Assert.Equal(Int256.NegativeOne, (Int256)(-1));
        Assert.Equal((Int256)(-128), (Int256)(sbyte)(-128));
        Assert.Equal((Int256)long.MinValue, (Int256)long.MinValue);
        Assert.Equal((Int256)Int128.MinValue, (Int256)Int128.MinValue);
    }

    [Fact(DisplayName = "Int256.Parse should round-trip negative values")]
    public void Int256ParseShouldRoundTripNegativeValues()
    {
        string input = "-57896044618658097711785492504343953926634992332820282019728792003956564819968";
        Int256 value = Int256.Parse(input);
        Assert.Equal(input, value.ToString());
        Assert.Equal(Int256.MinValue, value);
    }

    [Fact(DisplayName = "Int256.Parse of overflow should throw")]
    public void Int256ParseOfOverflowShouldThrow()
    {
        string overflow = "57896044618658097711785492504343953926634992332820282019728792003956564819968";
        Assert.Throws<OverflowException>(() => Int256.Parse(overflow));
    }

    [Fact(DisplayName = "Int256 BigInteger round-trip should preserve negative values")]
    public void Int256BigIntegerRoundTripShouldPreserveNegativeValues()
    {
        BigInteger source = -BigInteger.Pow(2, 200);
        Int256 value = (Int256)source;
        Assert.Equal(source, (BigInteger)value);
    }

    [Fact(DisplayName = "Int256.TryWriteBigEndian should round-trip with TryReadBigEndian for negative values")]
    public void Int256TryWriteBigEndianRoundTripsForNegativeValues()
    {
        Int256 source = (Int256)(-(BigInteger.Pow(2, 100) + BigInteger.One));
        Span<byte> buffer = stackalloc byte[32];
        Assert.True(source.TryWriteBigEndian(buffer, out int written));
        Assert.Equal(32, written);

        Assert.True(Int256.TryReadBigEndian(buffer, false, out Int256 read));
        Assert.Equal(source, read);
    }

    [Fact(DisplayName = "Int256.TryWriteLittleEndian should round-trip with TryReadLittleEndian for negative values")]
    public void Int256TryWriteLittleEndianRoundTripsForNegativeValues()
    {
        Int256 source = (Int256)(-(BigInteger.Pow(2, 200)));
        Span<byte> buffer = stackalloc byte[32];
        Assert.True(source.TryWriteLittleEndian(buffer, out int written));
        Assert.Equal(32, written);

        Assert.True(Int256.TryReadLittleEndian(buffer, false, out Int256 read));
        Assert.Equal(source, read);
    }

    [Fact(DisplayName = "Int256 to UInt256 conversion should reinterpret bits")]
    public void Int256ToUInt256ConversionShouldReinterpretBits()
    {
        Int256 negative = Int256.NegativeOne;
        UInt256 unsigned = (UInt256)negative;
        Assert.Equal(UInt256.MaxValue, unsigned);
    }

    [Fact(DisplayName = "Checked Int256 to UInt256 should throw for negative")]
    public void CheckedInt256ToUInt256ShouldThrowForNegative()
    {
        Assert.Throws<OverflowException>(() => checked((UInt256)Int256.NegativeOne));
    }

    [Fact(DisplayName = "Int256.IsPow2 should be false for negatives")]
    public void Int256IsPow2ShouldBeFalseForNegatives()
    {
        Assert.False(Int256.IsPow2(Int256.NegativeOne));
        Assert.False(Int256.IsPow2(Int256.MinValue));
        Assert.True(Int256.IsPow2(Int256.One));
        Assert.True(Int256.IsPow2((Int256)1024));
    }
}
