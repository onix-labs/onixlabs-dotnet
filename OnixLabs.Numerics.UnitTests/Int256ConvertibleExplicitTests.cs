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

public sealed class Int256ConvertibleExplicitTests
{
    [Fact(DisplayName = "Int256 explicit conversion from UInt256 should reinterpret bits")]
    public void Int256ExplicitFromUInt256ShouldReinterpretBits()
    {
        Int256 negative = (Int256)UInt256.MaxValue;
        Assert.Equal(Int256.NegativeOne, negative);
    }

    [Fact(DisplayName = "Int256 explicit checked conversion from UInt256 above MaxValue should throw")]
    public void Int256ExplicitCheckedFromUInt256AboveMaxShouldThrow()
    {
        UInt256 source = (UInt256)Int256.MaxValue + UInt256.One;
        Assert.Throws<OverflowException>(() => checked((Int256)source));
    }

    [Fact(DisplayName = "Int256 explicit checked conversion from UInt256 below MaxValue should succeed")]
    public void Int256ExplicitCheckedFromUInt256BelowMaxShouldSucceed()
    {
        UInt256 source = (UInt256)1234567890UL;
        Assert.Equal((Int256)1234567890UL, checked((Int256)source));
    }

    [Fact(DisplayName = "UInt256 explicit conversion from Int256 NegativeOne should reinterpret as MaxValue")]
    public void UInt256ExplicitFromInt256NegativeOneShouldReinterpret()
    {
        UInt256 converted = (UInt256)Int256.NegativeOne;
        Assert.Equal(UInt256.MaxValue, converted);
    }

    [Fact(DisplayName = "UInt256 explicit checked conversion from Int256 negative should throw")]
    public void UInt256ExplicitCheckedFromInt256NegativeShouldThrow()
    {
        Assert.Throws<OverflowException>(() => checked((UInt256)Int256.NegativeOne));
        Assert.Throws<OverflowException>(() => checked((UInt256)Int256.MinValue));
    }

    [Fact(DisplayName = "Int256 explicit conversion from BigInteger should truncate to low 256 bits")]
    public void Int256ExplicitFromBigIntegerShouldTruncate()
    {
        BigInteger source = (BigInteger.One << 256) + BigInteger.One;
        Int256 converted = (Int256)source;
        Assert.Equal(Int256.One, converted);
    }

    [Fact(DisplayName = "Int256 explicit checked conversion from BigInteger out of range should throw")]
    public void Int256ExplicitCheckedFromBigIntegerOutOfRangeShouldThrow()
    {
        BigInteger over = (BigInteger)Int256.MaxValue + BigInteger.One;
        BigInteger under = (BigInteger)Int256.MinValue - BigInteger.One;
        Assert.Throws<OverflowException>(() => checked((Int256)over));
        Assert.Throws<OverflowException>(() => checked((Int256)under));
    }

    [Fact(DisplayName = "Int256 explicit narrowing to byte should return low byte and ignore sign")]
    public void Int256ExplicitNarrowingToByteShouldReturnLowByte()
    {
        Int256 source = (Int256)(-1);
        Assert.Equal((byte)0xFF, (byte)source);
    }

    [Fact(DisplayName = "Int256 explicit checked narrowing to byte should throw for negative")]
    public void Int256ExplicitCheckedNarrowingToByteShouldThrowForNegative()
    {
        Assert.Throws<OverflowException>(() => checked((byte)Int256.NegativeOne));
        Assert.Throws<OverflowException>(() => checked((byte)(Int256)256));
    }

    [Fact(DisplayName = "Int256 explicit narrowing to sbyte should reinterpret low byte as signed")]
    public void Int256ExplicitNarrowingToSByteShouldReinterpret()
    {
        Assert.Equal((sbyte)(-1), (sbyte)Int256.NegativeOne);
        Assert.Equal((sbyte)42, (sbyte)(Int256)42);
    }

    [Fact(DisplayName = "Int256 explicit checked narrowing to sbyte should throw for out-of-range values")]
    public void Int256ExplicitCheckedNarrowingToSByteShouldThrowForOutOfRange()
    {
        Assert.Throws<OverflowException>(() => checked((sbyte)(Int256)200));
        Assert.Throws<OverflowException>(() => checked((sbyte)(Int256)(-200)));
    }

    [Fact(DisplayName = "Int256 explicit narrowing to Int128 should preserve value when in range")]
    public void Int256ExplicitNarrowingToInt128ShouldPreserveValueInRange()
    {
        Int256 source = Int128.MinValue;
        Assert.Equal(Int128.MinValue, (Int128)source);
    }

    [Fact(DisplayName = "Int256 explicit checked narrowing to Int128 should throw for out-of-range values")]
    public void Int256ExplicitCheckedNarrowingToInt128ShouldThrowForOutOfRange()
    {
        Int256 above = (Int256)Int128.MaxValue + Int256.One;
        Int256 below = (Int256)Int128.MinValue - Int256.One;
        Assert.Throws<OverflowException>(() => checked((Int128)above));
        Assert.Throws<OverflowException>(() => checked((Int128)below));
    }

    [Fact(DisplayName = "Int256 explicit conversion to BigInteger of negative value should produce negative BigInteger")]
    public void Int256ExplicitToBigIntegerOfNegativeShouldProduceNegative()
    {
        BigInteger source = -BigInteger.Pow(2, 200);
        Int256 converted = (Int256)source;
        BigInteger roundTripped = (BigInteger)converted;
        Assert.Equal(source, roundTripped);
        Assert.True(roundTripped.Sign < 0);
    }

    [Fact(DisplayName = "Int256 explicit conversion to double should produce sign-correct finite values")]
    public void Int256ExplicitToDoubleShouldProduceSignCorrectFinite()
    {
        double positive = (double)(Int256)1024;
        double negative = (double)(Int256)(-1024);
        Assert.Equal(1024d, positive);
        Assert.Equal(-1024d, negative);
        Assert.True(double.IsFinite((double)Int256.MaxValue));
    }

    [Fact(DisplayName = "Int256 explicit conversion to Float128 of zero should produce zero")]
    public void Int256ExplicitToFloat128OfZeroShouldProduceZero()
    {
        Assert.True(Float128.IsZero((Float128)Int256.Zero));
    }

    [Fact(DisplayName = "Int256 explicit conversion from Float128 NaN should produce zero unchecked")]
    public void Int256ExplicitFromFloat128NaNShouldProduceZeroUnchecked()
    {
        Assert.Equal(Int256.Zero, (Int256)Float128.NaN);
        Assert.Equal(Int256.Zero, (Int256)Float128.PositiveInfinity);
        Assert.Equal(Int256.Zero, (Int256)Float128.NegativeInfinity);
    }

    [Fact(DisplayName = "Int256 explicit checked conversion from Float128 non-finite should throw")]
    public void Int256ExplicitCheckedFromFloat128NonFiniteShouldThrow()
    {
        Assert.Throws<OverflowException>(() => checked((Int256)Float128.NaN));
        Assert.Throws<OverflowException>(() => checked((Int256)Float128.PositiveInfinity));
    }
}
