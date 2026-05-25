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

public sealed class UInt256ConvertibleExplicitTests
{
    [Fact(DisplayName = "UInt256 explicit conversion from negative sbyte should wrap via two's-complement")]
    public void UInt256ExplicitFromNegativeSByteShouldWrap()
    {
        UInt256 converted = (UInt256)(sbyte)(-1);
        Assert.Equal(UInt256.MaxValue, converted);
    }

    [Fact(DisplayName = "UInt256 explicit checked conversion from negative sbyte should throw")]
    public void UInt256ExplicitCheckedFromNegativeSByteShouldThrow()
    {
        Assert.Throws<OverflowException>(() => checked((UInt256)(sbyte)(-1)));
    }

    [Fact(DisplayName = "UInt256 explicit conversion from negative int should wrap via two's-complement")]
    public void UInt256ExplicitFromNegativeIntShouldWrap()
    {
        UInt256 converted = (UInt256)(-1);
        Assert.Equal(UInt256.MaxValue, converted);
    }

    [Fact(DisplayName = "UInt256 explicit conversion from negative Int128 should wrap via two's-complement")]
    public void UInt256ExplicitFromNegativeInt128ShouldWrap()
    {
        UInt256 converted = (UInt256)Int128.MinValue;
        BigInteger expected = ((BigInteger.One << 256) + (BigInteger)Int128.MinValue);
        Assert.Equal(expected, (BigInteger)converted);
    }

    [Fact(DisplayName = "UInt256 explicit checked conversion from negative Int128 should throw")]
    public void UInt256ExplicitCheckedFromNegativeInt128ShouldThrow()
    {
        Assert.Throws<OverflowException>(() => checked((UInt256)(Int128)(-1)));
    }

    [Fact(DisplayName = "UInt256 explicit conversion from BigInteger should truncate to low 256 bits")]
    public void UInt256ExplicitFromBigIntegerShouldTruncate()
    {
        BigInteger source = (BigInteger.One << 256) + BigInteger.One;
        UInt256 converted = (UInt256)source;
        Assert.Equal(UInt256.One, converted);
    }

    [Fact(DisplayName = "UInt256 explicit checked conversion from too-large BigInteger should throw")]
    public void UInt256ExplicitCheckedFromTooLargeBigIntegerShouldThrow()
    {
        BigInteger source = BigInteger.One << 256;
        Assert.Throws<OverflowException>(() => checked((UInt256)source));
    }

    [Fact(DisplayName = "UInt256 explicit checked conversion from negative BigInteger should throw")]
    public void UInt256ExplicitCheckedFromNegativeBigIntegerShouldThrow()
    {
        Assert.Throws<OverflowException>(() => checked((UInt256)(-BigInteger.One)));
    }

    [Fact(DisplayName = "UInt256 explicit narrowing to UInt128 should preserve the low half")]
    public void UInt256ExplicitNarrowingToUInt128ShouldPreserveLowHalf()
    {
        UInt256 source = new((UInt128)42, UInt128.MaxValue);
        Assert.Equal(UInt128.MaxValue, (UInt128)source);
    }

    [Fact(DisplayName = "UInt256 explicit checked narrowing to UInt128 should throw when upper is non-zero")]
    public void UInt256ExplicitCheckedNarrowingToUInt128ShouldThrowWhenUpperNonZero()
    {
        UInt256 source = new(UInt128.One, UInt128.Zero);
        Assert.Throws<OverflowException>(() => checked((UInt128)source));
    }

    [Fact(DisplayName = "UInt256 explicit narrowing to Int128 should reinterpret low half as signed")]
    public void UInt256ExplicitNarrowingToInt128ShouldReinterpretLowHalf()
    {
        UInt256 source = UInt256.MaxValue;
        Assert.Equal((Int128)UInt128.MaxValue, (Int128)source);
    }

    [Fact(DisplayName = "UInt256 explicit checked narrowing to Int128 should throw for value greater than Int128.MaxValue")]
    public void UInt256ExplicitCheckedNarrowingToInt128ShouldThrowWhenTooLarge()
    {
        UInt256 source = new(UInt128.Zero, ((UInt128)Int128.MaxValue) + UInt128.One);
        Assert.Throws<OverflowException>(() => checked((Int128)source));
    }

    [Fact(DisplayName = "UInt256 explicit conversion to double should approximate the BigInteger value")]
    public void UInt256ExplicitToDoubleShouldApproximateTheBigIntegerValue()
    {
        UInt256 source = UInt256.Parse("123456789012345678901234567890");
        double actual = (double)source;
        double oracle = (double)(BigInteger)source;
        // Both conversions go through different precision paths, so they should agree within one ULP.
        double tolerance = Math.Abs(oracle) * 1e-15;
        Assert.True(Math.Abs(actual - oracle) <= tolerance, $"Actual={actual} oracle={oracle}");
    }

    [Fact(DisplayName = "UInt256 explicit conversion to float of MaxValue should overflow to PositiveInfinity")]
    public void UInt256ExplicitToFloatOfMaxValueShouldOverflowToInfinity()
    {
        float actual = (float)UInt256.MaxValue;
        Assert.True(float.IsPositiveInfinity(actual));
    }

    [Fact(DisplayName = "UInt256 explicit conversion to float of a small value should be exact")]
    public void UInt256ExplicitToFloatOfSmallValueShouldBeExact()
    {
        Assert.Equal(1024f, (float)(UInt256)1024);
        Assert.Equal(0f, (float)UInt256.Zero);
    }

    [Fact(DisplayName = "UInt256 explicit conversion to decimal should match BigInteger conversion when in range")]
    public void UInt256ExplicitToDecimalShouldMatchBigIntegerConversion()
    {
        UInt256 source = (UInt256)1234567890UL;
        decimal actual = (decimal)source;
        Assert.Equal(1234567890m, actual);
    }

    [Fact(DisplayName = "UInt256 explicit conversion to decimal should throw on overflow")]
    public void UInt256ExplicitToDecimalShouldThrowOnOverflow()
    {
        Assert.Throws<OverflowException>(() => (decimal)UInt256.MaxValue);
    }

    [Fact(DisplayName = "UInt256 explicit conversion to Half should produce a finite value for small inputs")]
    public void UInt256ExplicitToHalfShouldProduceFiniteForSmall()
    {
        UInt256 source = (UInt256)1024;
        Half actual = (Half)source;
        Assert.Equal((Half)1024, actual);
    }

    [Fact(DisplayName = "UInt256 explicit conversion from Float128 NaN should produce zero in unchecked mode")]
    public void UInt256ExplicitFromFloat128NaNShouldProduceZeroUnchecked()
    {
        Assert.Equal(UInt256.Zero, (UInt256)Float128.NaN);
        Assert.Equal(UInt256.Zero, (UInt256)Float128.PositiveInfinity);
        Assert.Equal(UInt256.Zero, (UInt256)Float128.NegativeInfinity);
    }

    [Fact(DisplayName = "UInt256 explicit checked conversion from Float128 NaN should throw")]
    public void UInt256ExplicitCheckedFromFloat128NaNShouldThrow()
    {
        Assert.Throws<OverflowException>(() => checked((UInt256)Float128.NaN));
        Assert.Throws<OverflowException>(() => checked((UInt256)Float128.PositiveInfinity));
        Assert.Throws<OverflowException>(() => checked((UInt256)Float128.NegativeOne));
    }

    [Fact(DisplayName = "UInt256 explicit conversion to BigInteger should round-trip via Parse")]
    public void UInt256ExplicitToBigIntegerShouldRoundTrip()
    {
        UInt256 source = UInt256.Parse("12345678901234567890123456789012345678901234567890");
        BigInteger converted = (BigInteger)source;
        Assert.Equal(source, (UInt256)converted);
    }

    [Fact(DisplayName = "UInt256 explicit narrowing to primitive integers should equal the BigInteger low-bits masks")]
    public void UInt256ExplicitNarrowingToPrimitivesShouldMatchMaskedLowBits()
    {
        // A value with the upper limb populated, so the narrowed low bits are non-trivial.
        BigInteger oracle = (BigInteger.One << 200) + 0xDEAD_BEEF_CAFE_F00DUL;
        UInt256 source = (UInt256)oracle;

        Assert.Equal((byte)(oracle & 0xFF), (byte)source);
        Assert.Equal((ushort)(oracle & 0xFFFF), (ushort)source);
        Assert.Equal((uint)(oracle & 0xFFFFFFFF), (uint)source);
        Assert.Equal((ulong)(oracle & ulong.MaxValue), (ulong)source);
    }

    [Fact(DisplayName = "UInt256 explicit checked narrowing should throw when the value exceeds the target range")]
    public void UInt256ExplicitCheckedNarrowingShouldThrowWhenOutOfRange()
    {
        UInt256 source = (UInt256)0x1_0000UL;
        Assert.Throws<OverflowException>(() => checked((byte)source));
        Assert.Throws<OverflowException>(() => checked((ushort)((UInt256)(ushort.MaxValue + 1))));
        Assert.Throws<OverflowException>(() => checked((uint)((UInt256)uint.MaxValue + UInt256.One)));
        Assert.Throws<OverflowException>(() => checked((ulong)((UInt256)ulong.MaxValue + UInt256.One)));
        // Signed targets overflow when the unsigned value exceeds the signed maximum.
        Assert.Throws<OverflowException>(() => checked((int)((UInt256)(uint)int.MaxValue + UInt256.One)));
        Assert.Throws<OverflowException>(() => checked((long)((UInt256)(ulong)long.MaxValue + UInt256.One)));
    }

    [Fact(DisplayName = "UInt256 explicit checked narrowing of in-range values should match the unchecked result")]
    public void UInt256ExplicitCheckedNarrowingOfInRangeShouldMatchUnchecked()
    {
        UInt256 source = (UInt256)250UL;
        Assert.Equal((byte)250, checked((byte)source));
        Assert.Equal((ushort)250, checked((ushort)source));
        Assert.Equal(250, checked((int)source));
        Assert.Equal(250L, checked((long)source));
    }
}
