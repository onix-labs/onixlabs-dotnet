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

public sealed class Int512ConvertTests
{
    [Fact(DisplayName = "Int512 INumberBase.CreateChecked from byte should preserve value")]
    public void Int512CreateCheckedFromByteShouldPreserveValue()
    {
        Int512 result = NumberBaseHelper.CreateChecked<Int512, byte>(255);
        Assert.Equal((Int512)255, result);
    }

    [Fact(DisplayName = "Int512 INumberBase.CreateChecked from negative int should preserve negative value")]
    public void Int512CreateCheckedFromNegativeIntShouldPreserveValue()
    {
        Int512 result = NumberBaseHelper.CreateChecked<Int512, int>(-12345);
        Assert.Equal((Int512)(-12345), result);
    }

    [Fact(DisplayName = "Int512 INumberBase.CreateChecked from over-large BigInteger should throw")]
    public void Int512CreateCheckedFromOverLargeBigIntegerShouldThrow()
    {
        BigInteger oversized = BigInteger.Pow(2, 600);
        Assert.Throws<OverflowException>(() => NumberBaseHelper.CreateChecked<Int512, BigInteger>(oversized));
    }

    [Fact(DisplayName = "Int512 INumberBase.CreateChecked from under-large BigInteger should throw")]
    public void Int512CreateCheckedFromUnderLargeBigIntegerShouldThrow()
    {
        BigInteger undersized = -BigInteger.Pow(2, 600);
        Assert.Throws<OverflowException>(() => NumberBaseHelper.CreateChecked<Int512, BigInteger>(undersized));
    }

    [Fact(DisplayName = "Int512 INumberBase.CreateSaturating from over-large BigInteger should saturate to MaxValue")]
    public void Int512CreateSaturatingFromOverLargeBigIntegerShouldSaturateToMax()
    {
        BigInteger oversized = BigInteger.Pow(2, 600);
        Assert.Equal(Int512.MaxValue, NumberBaseHelper.CreateSaturating<Int512, BigInteger>(oversized));
    }

    [Fact(DisplayName = "Int512 INumberBase.CreateSaturating from under-large BigInteger should saturate to MinValue")]
    public void Int512CreateSaturatingFromUnderLargeBigIntegerShouldSaturateToMin()
    {
        BigInteger undersized = -BigInteger.Pow(2, 600);
        Assert.Equal(Int512.MinValue, NumberBaseHelper.CreateSaturating<Int512, BigInteger>(undersized));
    }

    [Fact(DisplayName = "Int512 INumberBase.CreateTruncating from over-large BigInteger should truncate to low 512 bits")]
    public void Int512CreateTruncatingFromOverLargeBigIntegerShouldTruncate()
    {
        BigInteger source = (BigInteger.One << 600) + BigInteger.One;
        Int512 result = NumberBaseHelper.CreateTruncating<Int512, BigInteger>(source);
        Assert.Equal(Int512.One, result);
    }

    [Fact(DisplayName = "Int512 to byte via CreateChecked should succeed in range")]
    public void Int512ToByteCreateCheckedShouldSucceed()
    {
        Int512 source = (Int512)200;
        byte result = NumberBaseHelper.CreateChecked<byte, Int512>(source);
        Assert.Equal((byte)200, result);
    }

    [Fact(DisplayName = "Int512 to byte via CreateChecked should throw for negative")]
    public void Int512ToByteCreateCheckedShouldThrowForNegative()
    {
        Int512 source = Int512.NegativeOne;
        Assert.Throws<OverflowException>(() => NumberBaseHelper.CreateChecked<byte, Int512>(source));
    }

    [Fact(DisplayName = "Int512 to long via CreateSaturating should clamp to long range")]
    public void Int512ToLongCreateSaturatingShouldClamp()
    {
        Assert.Equal(long.MaxValue, NumberBaseHelper.CreateSaturating<long, Int512>(Int512.MaxValue));
        Assert.Equal(long.MinValue, NumberBaseHelper.CreateSaturating<long, Int512>(Int512.MinValue));
    }

    [Fact(DisplayName = "Int512 to UInt512 via CreateChecked should throw for negative")]
    public void Int512ToUInt512CreateCheckedShouldThrowForNegative()
    {
        Assert.Throws<OverflowException>(() => NumberBaseHelper.CreateChecked<UInt512, Int512>(Int512.NegativeOne));
    }

    [Fact(DisplayName = "Int512 to UInt512 via CreateSaturating should clamp negative to zero")]
    public void Int512ToUInt512CreateSaturatingShouldClampNegativeToZero()
    {
        Assert.Equal(UInt512.Zero, NumberBaseHelper.CreateSaturating<UInt512, Int512>(Int512.NegativeOne));
        Assert.Equal(UInt512.Zero, NumberBaseHelper.CreateSaturating<UInt512, Int512>(Int512.MinValue));
    }

    [Fact(DisplayName = "Int512 INumberBase.CreateTruncating from long should preserve the value")]
    public void Int512CreateTruncatingFromLongShouldPreserveValue()
    {
        long source = -987654321098765L;
        Int512 result = NumberBaseHelper.CreateTruncating<Int512, long>(source);
        Assert.Equal((Int512)source, result);
    }

    [Fact(DisplayName = "Int512 to Float128 via CreateChecked should round-trip small values")]
    public void Int512ToFloat128CreateCheckedShouldRoundTripSmall()
    {
        Int512 source = (Int512)(-42);
        Float128 result = NumberBaseHelper.CreateChecked<Float128, Int512>(source);
        Assert.Equal((Float128)(-42), result);
    }

    [Fact(DisplayName = "Int512 round-trip through CreateChecked of large value")]
    public void Int512RoundTripCreateCheckedLargeValue()
    {
        BigInteger source = -((BigInteger.One << 400) + BigInteger.One);
        Int512 mid = NumberBaseHelper.CreateChecked<Int512, BigInteger>(source);
        BigInteger back = NumberBaseHelper.CreateChecked<BigInteger, Int512>(mid);
        Assert.Equal(source, back);
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
