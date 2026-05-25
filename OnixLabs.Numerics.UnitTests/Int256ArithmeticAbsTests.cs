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

public sealed class Int256ArithmeticAbsTests
{
    [Fact(DisplayName = "Int256.Abs of positive value should return the value")]
    public void Int256AbsOfPositiveShouldReturnValue()
    {
        Assert.Equal((Int256)42, Int256.Abs((Int256)42));
        Assert.Equal(Int256.MaxValue, Int256.Abs(Int256.MaxValue));
    }

    [Fact(DisplayName = "Int256.Abs of zero should return zero")]
    public void Int256AbsOfZeroShouldReturnZero()
    {
        Assert.Equal(Int256.Zero, Int256.Abs(Int256.Zero));
    }

    [Fact(DisplayName = "Int256.Abs of negative value should return its positive magnitude")]
    public void Int256AbsOfNegativeShouldReturnPositiveMagnitude()
    {
        Assert.Equal((Int256)42, Int256.Abs((Int256)(-42)));
        Assert.Equal(Int256.One, Int256.Abs(Int256.NegativeOne));
        Assert.Equal(Int256.MaxValue, Int256.Abs(Int256.MinValue + Int256.One));
    }

    [Fact(DisplayName = "Int256.Abs of MinValue should throw OverflowException")]
    public void Int256AbsOfMinValueShouldThrow()
    {
        Assert.Throws<OverflowException>(() => Int256.Abs(Int256.MinValue));
    }

    [Fact(DisplayName = "Int256.Negate of any non-MinValue should produce the additive inverse")]
    public void Int256NegateOfNonMinShouldProduceAdditiveInverse()
    {
        Int256 value = (Int256)9999;
        Assert.Equal((Int256)(-9999), Int256.Negate(value));
        Assert.Equal(value, Int256.Negate(Int256.Negate(value)));
    }

    [Fact(DisplayName = "Int256.Negate of MinValue should wrap to MinValue in two's-complement")]
    public void Int256NegateOfMinValueShouldWrapToMinValue()
    {
        Assert.Equal(Int256.MinValue, Int256.Negate(Int256.MinValue));
    }

    [Fact(DisplayName = "Int256 checked unary negation of MinValue should throw")]
    public void Int256CheckedUnaryNegationOfMinValueShouldThrow()
    {
        Assert.Throws<OverflowException>(() => checked(-Int256.MinValue));
    }

    [Fact(DisplayName = "Int256 unchecked unary negation of MinValue should return MinValue")]
    public void Int256UncheckedUnaryNegationOfMinValueShouldReturnMinValue()
    {
        Assert.Equal(Int256.MinValue, -Int256.MinValue);
    }

    [Fact(DisplayName = "Int256.Sign should return -1, 0, 1 for negative, zero, positive")]
    public void Int256SignShouldReturnExpectedValues()
    {
        Assert.Equal(-1, Int256.Sign(Int256.NegativeOne));
        Assert.Equal(-1, Int256.Sign(Int256.MinValue));
        Assert.Equal(0, Int256.Sign(Int256.Zero));
        Assert.Equal(1, Int256.Sign(Int256.One));
        Assert.Equal(1, Int256.Sign(Int256.MaxValue));
    }

    [Fact(DisplayName = "Int256.CopySign should preserve magnitude of first operand and sign of second")]
    public void Int256CopySignShouldPreserveMagnitudeAndCopySign()
    {
        Assert.Equal((Int256)(-42), Int256.CopySign((Int256)42, Int256.NegativeOne));
        Assert.Equal((Int256)42, Int256.CopySign((Int256)(-42), Int256.One));
        Assert.Equal(Int256.Zero, Int256.CopySign(Int256.Zero, Int256.NegativeOne));
    }

    [Fact(DisplayName = "Int256.CopySign with zero sign source should produce positive magnitude")]
    public void Int256CopySignWithZeroSignSourceShouldProducePositive()
    {
        Assert.Equal((Int256)42, Int256.CopySign((Int256)(-42), Int256.Zero));
    }

    [Fact(DisplayName = "Int256 absolute value should match BigInteger.Abs for in-range values")]
    public void Int256AbsShouldMatchBigIntegerForInRangeValues()
    {
        Int256 value = Int256.Parse("-12345678901234567890123456789");
        BigInteger expected = BigInteger.Abs((BigInteger)value);
        Assert.Equal(expected, (BigInteger)Int256.Abs(value));
    }
}
