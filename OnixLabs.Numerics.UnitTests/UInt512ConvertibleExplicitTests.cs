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

public sealed class UInt512ConvertibleExplicitTests
{
    [Fact(DisplayName = "UInt512 explicit conversion from sbyte non-negative should preserve value")]
    public void UInt512ExplicitFromSByteNonNegativeShouldPreserveValue()
    {
        Assert.Equal(UInt512.Zero, (UInt512)(sbyte)0);
        Assert.Equal((UInt512)42UL, (UInt512)(sbyte)42);
        Assert.Equal((UInt512)127UL, (UInt512)sbyte.MaxValue);
    }

    [Fact(DisplayName = "UInt512 explicit conversion from negative sbyte should two's-complement-wrap")]
    public void UInt512ExplicitFromNegativeSByteShouldWrap()
    {
        UInt512 result = (UInt512)(sbyte)(-1);
        Assert.Equal(UInt512.MaxValue, result);
    }

    [Fact(DisplayName = "UInt512 checked conversion from negative sbyte should throw")]
    public void UInt512CheckedFromNegativeSByteShouldThrow()
    {
        Assert.Throws<OverflowException>(() => checked((UInt512)(sbyte)(-1)));
        Assert.Throws<OverflowException>(() => checked((UInt512)sbyte.MinValue));
    }

    [Fact(DisplayName = "UInt512 checked conversion from non-negative sbyte should succeed")]
    public void UInt512CheckedFromNonNegativeSByteShouldSucceed()
    {
        Assert.Equal((UInt512)42UL, checked((UInt512)(sbyte)42));
    }

    [Fact(DisplayName = "UInt512 explicit conversion from int should preserve non-negative values")]
    public void UInt512ExplicitFromIntNonNegativeShouldPreserveValue()
    {
        Assert.Equal((UInt512)0UL, (UInt512)0);
        Assert.Equal((UInt512)int.MaxValue, (UInt512)int.MaxValue);
    }

    [Fact(DisplayName = "UInt512 explicit conversion from negative int should two's-complement-wrap")]
    public void UInt512ExplicitFromNegativeIntShouldWrap()
    {
        UInt512 result = (UInt512)(-1);
        Assert.Equal(UInt512.MaxValue, result);
    }

    [Fact(DisplayName = "UInt512 checked conversion from negative int should throw")]
    public void UInt512CheckedFromNegativeIntShouldThrow()
    {
        Assert.Throws<OverflowException>(() => checked((UInt512)(-1)));
        Assert.Throws<OverflowException>(() => checked((UInt512)int.MinValue));
    }

    [Fact(DisplayName = "UInt512 explicit conversion from long should match BigInteger for negative values (wrap)")]
    public void UInt512ExplicitFromNegativeLongShouldWrap()
    {
        UInt512 result = (UInt512)long.MinValue;
        BigInteger expected = ((BigInteger.One << 512) - BigInteger.One) - (long.MaxValue);
        Assert.Equal(expected, (BigInteger)result);
    }

    [Fact(DisplayName = "UInt512 explicit narrowing to byte should match low 8 bits")]
    public void UInt512ExplicitToByteShouldMatchLowBits()
    {
        UInt512 value = (UInt512)0x123456789ABCDEF0UL;
        Assert.Equal((byte)0xF0, (byte)value);
    }

    [Fact(DisplayName = "UInt512 explicit narrowing to ushort should match low 16 bits")]
    public void UInt512ExplicitToUShortShouldMatchLowBits()
    {
        UInt512 value = (UInt512)0x123456789ABCDEF0UL;
        Assert.Equal((ushort)0xDEF0, (ushort)value);
    }

    [Fact(DisplayName = "UInt512 explicit narrowing to uint should match low 32 bits")]
    public void UInt512ExplicitToUIntShouldMatchLowBits()
    {
        UInt512 value = (UInt512)0x123456789ABCDEF0UL;
        Assert.Equal(0x9ABCDEF0u, (uint)value);
    }

    [Fact(DisplayName = "UInt512 explicit narrowing to ulong should match low 64 bits")]
    public void UInt512ExplicitToULongShouldMatchLowBits()
    {
        UInt512 value = UInt512.MaxValue;
        Assert.Equal(ulong.MaxValue, (ulong)value);
    }

    [Fact(DisplayName = "UInt512 checked narrowing to byte should throw on overflow")]
    public void UInt512CheckedToByteShouldThrowOnOverflow()
    {
        Assert.Throws<OverflowException>(() => checked((byte)((UInt512)256UL)));
        Assert.Throws<OverflowException>(() => checked((byte)UInt512.MaxValue));
    }

    [Fact(DisplayName = "UInt512 checked narrowing to byte should succeed when in range")]
    public void UInt512CheckedToByteInRangeShouldSucceed()
    {
        Assert.Equal((byte)0xFF, checked((byte)((UInt512)0xFFUL)));
    }

    [Fact(DisplayName = "UInt512 checked narrowing to int should throw on values exceeding int.MaxValue")]
    public void UInt512CheckedToIntShouldThrowOnOverflow()
    {
        Assert.Throws<OverflowException>(() => checked((int)((UInt512)(uint)int.MaxValue + UInt512.One)));
        Assert.Throws<OverflowException>(() => checked((int)UInt512.MaxValue));
    }

    [Fact(DisplayName = "UInt512 checked narrowing to ulong should throw when upper bits are set")]
    public void UInt512CheckedToULongShouldThrowOnHighBits()
    {
        Assert.Throws<OverflowException>(() => checked((ulong)UInt512.MaxValue));
        UInt512 above = (UInt512)ulong.MaxValue + UInt512.One;
        Assert.Throws<OverflowException>(() => checked((ulong)above));
    }

    [Fact(DisplayName = "UInt512 checked narrowing to UInt128 should throw when upper bits are set")]
    public void UInt512CheckedToUInt128ShouldThrowOnUpperBits()
    {
        Assert.Throws<OverflowException>(() => checked((UInt128)UInt512.MaxValue));
        UInt512 above = (UInt512)UInt128.MaxValue + UInt512.One;
        Assert.Throws<OverflowException>(() => checked((UInt128)above));
    }

    [Fact(DisplayName = "UInt512 checked narrowing to UInt256 should throw when upper half is non-zero")]
    public void UInt512CheckedToUInt256ShouldThrowOnUpperHalfNonZero()
    {
        Assert.Throws<OverflowException>(() => checked((UInt256)UInt512.MaxValue));
    }

    [Fact(DisplayName = "UInt512 checked narrowing to UInt256 should succeed when upper half is zero")]
    public void UInt512CheckedToUInt256ShouldSucceedWhenUpperHalfZero()
    {
        UInt512 value = new(UInt256.Zero, UInt256.MaxValue);
        Assert.Equal(UInt256.MaxValue, checked((UInt256)value));
    }

    [Fact(DisplayName = "UInt512 to BigInteger should round-trip MaxValue")]
    public void UInt512ToBigIntegerShouldRoundTripMaxValue()
    {
        UInt512 source = UInt512.MaxValue;
        BigInteger big = source;
        UInt512 back = (UInt512)big;
        Assert.Equal(source, back);
    }

    [Fact(DisplayName = "UInt512 checked conversion from negative BigInteger should throw")]
    public void UInt512CheckedFromNegativeBigIntegerShouldThrow()
    {
        Assert.Throws<OverflowException>(() => checked((UInt512)(-BigInteger.One)));
    }

    [Fact(DisplayName = "UInt512 checked conversion from over-large BigInteger should throw")]
    public void UInt512CheckedFromOverLargeBigIntegerShouldThrow()
    {
        BigInteger oversized = BigInteger.One << 512;
        Assert.Throws<OverflowException>(() => checked((UInt512)oversized));
    }

    [Fact(DisplayName = "UInt512 explicit conversion to double should be approximately the value")]
    public void UInt512ExplicitToDoubleShouldBeApproximately()
    {
        UInt512 value = UInt512.One << 256;
        double expected = Math.Pow(2.0, 256);
        Assert.Equal(expected, (double)value, 5);
    }

    [Fact(DisplayName = "UInt512 explicit conversion to decimal should match small values")]
    public void UInt512ExplicitToDecimalShouldMatchSmallValues()
    {
        UInt512 value = (UInt512)1234567890UL;
        Assert.Equal(1234567890m, (decimal)value);
    }

    [Fact(DisplayName = "UInt512 explicit conversion to Float128 should round-trip exact integer values")]
    public void UInt512ToFloat128ShouldRoundTripExactIntegers()
    {
        UInt512 value = (UInt512)123UL;
        Float128 wide = (Float128)value;
        Assert.Equal((Float128)123, wide);
    }

    [Fact(DisplayName = "UInt512 explicit conversion from Float128 truncates fractional values")]
    public void UInt512FromFloat128TruncatesFractional()
    {
        Float128 source = (Float128)123.7;
        Assert.Equal((UInt512)123UL, (UInt512)source);
    }

    [Fact(DisplayName = "UInt512 unchecked conversion from negative Float128 should produce zero")]
    public void UInt512UncheckedFromNegativeFloat128ShouldProduceZero()
    {
        Float128 source = (Float128)(-42);
        Assert.Equal(UInt512.Zero, (UInt512)source);
    }

    [Fact(DisplayName = "UInt512 checked conversion from negative Float128 should throw")]
    public void UInt512CheckedFromNegativeFloat128ShouldThrow()
    {
        Float128 source = (Float128)(-42);
        Assert.Throws<OverflowException>(() => checked((UInt512)source));
    }

    [Fact(DisplayName = "UInt512 unchecked conversion from Float128 NaN should produce zero")]
    public void UInt512UncheckedFromNaNFloat128ShouldProduceZero()
    {
        Assert.Equal(UInt512.Zero, (UInt512)Float128.NaN);
    }

    [Fact(DisplayName = "UInt512 checked conversion from Float128 NaN should throw")]
    public void UInt512CheckedFromNaNFloat128ShouldThrow()
    {
        Assert.Throws<OverflowException>(() => checked((UInt512)Float128.NaN));
    }

    [Fact(DisplayName = "UInt512 explicit narrowing to primitive integers should equal the BigInteger low-bits masks for an upper-limb value")]
    public void UInt512ExplicitNarrowingToPrimitivesShouldMatchMaskedLowBits()
    {
        // A value with the upper 256-bit limb populated, so the narrowed low bits are non-trivial.
        BigInteger oracle = (BigInteger.One << 400) + 0xDEAD_BEEF_CAFE_F00DUL;
        UInt512 source = (UInt512)oracle;

        Assert.Equal((byte)(oracle & 0xFF), (byte)source);
        Assert.Equal((ushort)(oracle & 0xFFFF), (ushort)source);
        Assert.Equal((uint)(oracle & 0xFFFFFFFF), (uint)source);
        Assert.Equal((ulong)(oracle & ulong.MaxValue), (ulong)source);
        Assert.NotEqual(UInt256.Zero, source.UpperBits);
    }
}
