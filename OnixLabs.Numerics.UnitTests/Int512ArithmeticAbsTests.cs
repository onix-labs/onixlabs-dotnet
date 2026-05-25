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

public sealed class Int512ArithmeticAbsTests
{
    [Fact(DisplayName = "Int512.Abs of positive should return the same value")]
    public void Int512AbsOfPositiveShouldReturnSame()
    {
        Int512 value = (Int512)42;
        Assert.Equal(value, Int512.Abs(value));
    }

    [Fact(DisplayName = "Int512.Abs of Zero should be Zero")]
    public void Int512AbsOfZeroShouldBeZero()
    {
        Assert.Equal(Int512.Zero, Int512.Abs(Int512.Zero));
    }

    [Fact(DisplayName = "Int512.Abs of negative should return the magnitude")]
    public void Int512AbsOfNegativeShouldReturnMagnitude()
    {
        Assert.Equal((Int512)42, Int512.Abs((Int512)(-42)));
        Assert.Equal(Int512.One, Int512.Abs(Int512.NegativeOne));
    }

    [Fact(DisplayName = "Int512.Abs of large negative should match BigInteger.Abs")]
    public void Int512AbsOfLargeNegativeShouldMatchBigInteger()
    {
        Int512 value = Int512.Parse("-987654321098765432109876543210");
        Int512 abs = Int512.Abs(value);
        Assert.Equal(BigInteger.Abs((BigInteger)value), (BigInteger)abs);
    }

    [Fact(DisplayName = "Int512.Abs of MinValue should throw OverflowException")]
    public void Int512AbsOfMinValueShouldThrow()
    {
        Assert.Throws<OverflowException>(() => Int512.Abs(Int512.MinValue));
    }

    [Fact(DisplayName = "Int512.Negate of zero should be zero")]
    public void Int512NegateOfZeroShouldBeZero()
    {
        Assert.Equal(Int512.Zero, Int512.Negate(Int512.Zero));
    }

    [Fact(DisplayName = "Int512.Negate of positive should produce negative of same magnitude")]
    public void Int512NegateOfPositiveShouldProduceNegative()
    {
        Int512 value = (Int512)12345;
        Assert.Equal((BigInteger)(-12345), (BigInteger)Int512.Negate(value));
    }

    [Fact(DisplayName = "Int512.Negate of negative should produce positive of same magnitude")]
    public void Int512NegateOfNegativeShouldProducePositive()
    {
        Int512 value = (Int512)(-12345);
        Assert.Equal((Int512)12345, Int512.Negate(value));
    }

    [Fact(DisplayName = "Int512 unchecked unary minus of MinValue should wrap to MinValue")]
    public void Int512UnaryMinusOfMinValueShouldWrap()
    {
        Assert.Equal(Int512.MinValue, -Int512.MinValue);
    }

    [Fact(DisplayName = "Int512 checked unary negation of MinValue should throw")]
    public void Int512CheckedUnaryMinusOfMinValueShouldThrow()
    {
        Assert.Throws<OverflowException>(() => checked(-Int512.MinValue));
    }

    [Fact(DisplayName = "Int512.Sign should return -1 for negative")]
    public void Int512SignOfNegativeShouldBeMinusOne()
    {
        Assert.Equal(-1, Int512.Sign(Int512.NegativeOne));
        Assert.Equal(-1, Int512.Sign(Int512.MinValue));
        Assert.Equal(-1, Int512.Sign((Int512)(-1)));
    }

    [Fact(DisplayName = "Int512.Sign should return 0 for zero")]
    public void Int512SignOfZeroShouldBeZero()
    {
        Assert.Equal(0, Int512.Sign(Int512.Zero));
    }

    [Fact(DisplayName = "Int512.Sign should return 1 for positive")]
    public void Int512SignOfPositiveShouldBeOne()
    {
        Assert.Equal(1, Int512.Sign(Int512.One));
        Assert.Equal(1, Int512.Sign(Int512.MaxValue));
        Assert.Equal(1, Int512.Sign((Int512)42));
    }

    [Fact(DisplayName = "Int512.CopySign should copy the sign of the second operand")]
    public void Int512CopySignShouldCopyTheSign()
    {
        Assert.Equal((Int512)(-42), Int512.CopySign((Int512)42, Int512.NegativeOne));
        Assert.Equal((Int512)42, Int512.CopySign((Int512)(-42), Int512.One));
        Assert.Equal((Int512)42, Int512.CopySign((Int512)42, Int512.MaxValue));
        Assert.Equal((Int512)(-42), Int512.CopySign((Int512)42, Int512.MinValue));
    }

    [Fact(DisplayName = "Int512.CopySign with positive sign source should preserve positive sign for already-positive values")]
    public void Int512CopySignPositivePreservesPositive()
    {
        Int512 value = (Int512)10;
        Assert.Equal(value, Int512.CopySign(value, Int512.One));
    }

    [Fact(DisplayName = "Int512.CopySign with Zero sign source should produce a positive (sign(0) == positive)")]
    public void Int512CopySignWithZeroSourceShouldProducePositive()
    {
        Int512 value = (Int512)(-10);
        Assert.Equal((Int512)10, Int512.CopySign(value, Int512.Zero));
    }
}
