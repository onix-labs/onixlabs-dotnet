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

public sealed class UInt512ConvertTests
{
    [Fact(DisplayName = "UInt512 INumberBase.CreateChecked from byte should preserve value")]
    public void UInt512CreateCheckedFromByteShouldPreserveValue()
    {
        UInt512 result = NumberBaseHelper.CreateChecked<UInt512, byte>(255);
        Assert.Equal((UInt512)255UL, result);
    }

    [Fact(DisplayName = "UInt512 INumberBase.CreateChecked from negative int should throw")]
    public void UInt512CreateCheckedFromNegativeIntShouldThrow()
    {
        Assert.Throws<OverflowException>(() => NumberBaseHelper.CreateChecked<UInt512, int>(-1));
    }

    [Fact(DisplayName = "UInt512 INumberBase.CreateChecked from BigInteger MaxValue should round-trip")]
    public void UInt512CreateCheckedFromBigIntegerMaxValueShouldRoundTrip()
    {
        BigInteger expected = (BigInteger.One << 512) - BigInteger.One;
        UInt512 result = NumberBaseHelper.CreateChecked<UInt512, BigInteger>(expected);
        Assert.Equal(expected, (BigInteger)result);
    }

    [Fact(DisplayName = "UInt512 INumberBase.CreateChecked from over-large BigInteger should throw")]
    public void UInt512CreateCheckedFromOverLargeBigIntegerShouldThrow()
    {
        BigInteger oversized = BigInteger.One << 512;
        Assert.Throws<OverflowException>(() => NumberBaseHelper.CreateChecked<UInt512, BigInteger>(oversized));
    }

    [Fact(DisplayName = "UInt512 INumberBase.CreateSaturating from negative int should saturate to Zero")]
    public void UInt512CreateSaturatingFromNegativeIntShouldSaturateToZero()
    {
        Assert.Equal(UInt512.Zero, NumberBaseHelper.CreateSaturating<UInt512, int>(-100));
        Assert.Equal(UInt512.Zero, NumberBaseHelper.CreateSaturating<UInt512, int>(int.MinValue));
    }

    [Fact(DisplayName = "UInt512 INumberBase.CreateSaturating from over-large BigInteger should saturate to MaxValue")]
    public void UInt512CreateSaturatingFromOverLargeBigIntegerShouldSaturateToMax()
    {
        BigInteger oversized = BigInteger.One << 600;
        Assert.Equal(UInt512.MaxValue, NumberBaseHelper.CreateSaturating<UInt512, BigInteger>(oversized));
    }

    [Fact(DisplayName = "UInt512 INumberBase.CreateTruncating from negative int should two's-complement-wrap")]
    public void UInt512CreateTruncatingFromNegativeIntShouldWrap()
    {
        Assert.Equal(UInt512.MaxValue, NumberBaseHelper.CreateTruncating<UInt512, int>(-1));
    }

    [Fact(DisplayName = "UInt512 INumberBase.CreateTruncating from over-large BigInteger should truncate to low 512 bits")]
    public void UInt512CreateTruncatingFromOverLargeBigIntegerShouldTruncate()
    {
        BigInteger source = (BigInteger.One << 600) + BigInteger.One;
        UInt512 result = NumberBaseHelper.CreateTruncating<UInt512, BigInteger>(source);
        Assert.Equal(UInt512.One, result);
    }

    [Fact(DisplayName = "UInt512 to byte via INumberBase.CreateChecked from in-range value should succeed")]
    public void UInt512ToByteCreateCheckedShouldSucceed()
    {
        UInt512 source = (UInt512)200UL;
        byte result = NumberBaseHelper.CreateChecked<byte, UInt512>(source);
        Assert.Equal((byte)200, result);
    }

    [Fact(DisplayName = "UInt512 to byte via INumberBase.CreateChecked from out-of-range value should throw")]
    public void UInt512ToByteCreateCheckedShouldThrowOutOfRange()
    {
        UInt512 source = (UInt512)256UL;
        Assert.Throws<OverflowException>(() => NumberBaseHelper.CreateChecked<byte, UInt512>(source));
    }

    [Fact(DisplayName = "UInt512 to ulong via INumberBase.CreateSaturating should saturate to MaxValue")]
    public void UInt512ToULongCreateSaturatingShouldSaturate()
    {
        UInt512 source = UInt512.MaxValue;
        ulong result = NumberBaseHelper.CreateSaturating<ulong, UInt512>(source);
        Assert.Equal(ulong.MaxValue, result);
    }

    [Fact(DisplayName = "UInt512 to int via INumberBase.CreateTruncating should truncate")]
    public void UInt512ToIntCreateTruncatingShouldTruncate()
    {
        UInt512 source = (UInt512)0xFFFFFFFFFFUL;
        int result = NumberBaseHelper.CreateTruncating<int, UInt512>(source);
        Assert.Equal(unchecked((int)0xFFFFFFFFu), result);
    }

    [Fact(DisplayName = "UInt512 to Float128 via INumberBase.CreateChecked should round-trip small values")]
    public void UInt512ToFloat128CreateCheckedShouldRoundTripSmall()
    {
        UInt512 source = (UInt512)42UL;
        Float128 result = NumberBaseHelper.CreateChecked<Float128, UInt512>(source);
        Assert.Equal((Float128)42, result);
    }

    [Fact(DisplayName = "UInt512 to Float256 via explicit cast should produce a value the size of the input")]
    public void UInt512ToFloat256ExplicitShouldProduceMatchingValue()
    {
        UInt512 source = (UInt512)42UL;
        Float256 result = (Float256)source;
        Assert.Equal(source, (UInt512)result);
    }

    [Fact(DisplayName = "UInt512 from Float128 via INumberBase.CreateChecked from negative value should throw")]
    public void UInt512FromFloat128CreateCheckedFromNegativeShouldThrow()
    {
        Float128 source = (Float128)(-1);
        Assert.Throws<OverflowException>(() => NumberBaseHelper.CreateChecked<UInt512, Float128>(source));
    }

    [Fact(DisplayName = "UInt512 from Float128 via INumberBase.CreateSaturating from negative value should produce zero")]
    public void UInt512FromFloat128CreateSaturatingFromNegativeShouldProduceZero()
    {
        Float128 source = (Float128)(-1);
        Assert.Equal(UInt512.Zero, NumberBaseHelper.CreateSaturating<UInt512, Float128>(source));
    }

    [Fact(DisplayName = "UInt512 from Float128 via INumberBase.CreateSaturating from positive infinity should saturate to MaxValue")]
    public void UInt512FromFloat128CreateSaturatingFromPositiveInfinityShouldSaturate()
    {
        Float128 source = Float128.PositiveInfinity;
        Assert.Equal(UInt512.MaxValue, NumberBaseHelper.CreateSaturating<UInt512, Float128>(source));
    }

    [Fact(DisplayName = "UInt512 round-trip through CreateChecked of large value")]
    public void UInt512RoundTripCreateCheckedLargeValue()
    {
        BigInteger source = (BigInteger.One << 400) + BigInteger.One;
        UInt512 mid = NumberBaseHelper.CreateChecked<UInt512, BigInteger>(source);
        BigInteger back = NumberBaseHelper.CreateChecked<BigInteger, UInt512>(mid);
        Assert.Equal(source, back);
    }

    [Fact(DisplayName = "UInt512 INumberBase.CreateSaturating from ulong should preserve value")]
    public void UInt512CreateSaturatingFromULongShouldPreserveValue()
    {
        ulong source = 9876543210UL;
        UInt512 result = NumberBaseHelper.CreateSaturating<UInt512, ulong>(source);
        Assert.Equal((UInt512)source, result);
    }

    [Fact(DisplayName = "UInt512 INumberBase.CreateTruncating from ulong should preserve value")]
    public void UInt512CreateTruncatingFromULongShouldPreserveValue()
    {
        ulong source = 9876543210UL;
        UInt512 result = NumberBaseHelper.CreateTruncating<UInt512, ulong>(source);
        Assert.Equal((UInt512)source, result);
    }

    [Fact(DisplayName = "UInt512 to UInt256 via CreateChecked should throw when high half is set")]
    public void UInt512ToUInt256CreateCheckedShouldThrowWhenHighSet()
    {
        UInt512 source = UInt512.MaxValue;
        Assert.Throws<OverflowException>(() => NumberBaseHelper.CreateChecked<UInt256, UInt512>(source));
    }

    private static class NumberBaseHelper
    {
        public static T CreateChecked<T, TOther>(TOther value)
            where T : INumberBase<T>
            where TOther : INumberBase<TOther>
            => T.CreateChecked(value);

        public static T CreateSaturating<T, TOther>(TOther value)
            where T : INumberBase<T>
            where TOther : INumberBase<TOther>
            => T.CreateSaturating(value);

        public static T CreateTruncating<T, TOther>(TOther value)
            where T : INumberBase<T>
            where TOther : INumberBase<TOther>
            => T.CreateTruncating(value);
    }
}
