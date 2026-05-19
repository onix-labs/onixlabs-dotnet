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

public sealed class UInt256ConvertTests
{
    // INumberBase.TryConvert* members are protected, so the documented public path is
    // through CreateChecked / CreateSaturating / CreateTruncating, which internally
    // dispatch to TryConvert*FromChecked first and then TryConvert*ToChecked.
    private static T CreateChecked<T, TOther>(TOther value)
        where T : INumberBase<T>
        where TOther : INumberBase<TOther>
        => T.CreateChecked(value);

    private static T CreateSaturating<T, TOther>(TOther value)
        where T : INumberBase<T>
        where TOther : INumberBase<TOther>
        => T.CreateSaturating(value);

    private static T CreateTruncating<T, TOther>(TOther value)
        where T : INumberBase<T>
        where TOther : INumberBase<TOther>
        => T.CreateTruncating(value);

    [Fact(DisplayName = "UInt256.CreateChecked from byte should succeed and preserve value")]
    public void UInt256CreateCheckedFromByteShouldSucceed()
    {
        Assert.Equal((UInt256)42, CreateChecked<UInt256, byte>(42));
    }

    [Fact(DisplayName = "UInt256.CreateChecked from negative sbyte should throw")]
    public void UInt256CreateCheckedFromNegativeSByteShouldThrow()
    {
        Assert.Throws<OverflowException>(() => CreateChecked<UInt256, sbyte>(-1));
    }

    [Fact(DisplayName = "UInt256.CreateSaturating from negative sbyte should saturate to zero")]
    public void UInt256CreateSaturatingFromNegativeSByteShouldSaturateZero()
    {
        Assert.Equal(UInt256.Zero, CreateSaturating<UInt256, sbyte>(-1));
    }

    [Fact(DisplayName = "UInt256.CreateTruncating from negative sbyte should wrap to MaxValue")]
    public void UInt256CreateTruncatingFromNegativeSByteShouldWrap()
    {
        Assert.Equal(UInt256.MaxValue, CreateTruncating<UInt256, sbyte>(-1));
    }

    [Fact(DisplayName = "UInt256.CreateSaturating from oversized BigInteger should saturate to MaxValue")]
    public void UInt256CreateSaturatingFromOversizedBigIntegerShouldSaturate()
    {
        BigInteger source = BigInteger.One << 300;
        Assert.Equal(UInt256.MaxValue, CreateSaturating<UInt256, BigInteger>(source));
    }

    [Fact(DisplayName = "UInt256.CreateTruncating from oversized BigInteger should truncate to low 256 bits")]
    public void UInt256CreateTruncatingFromOversizedBigIntegerShouldTruncate()
    {
        BigInteger source = (BigInteger.One << 256) + BigInteger.One;
        Assert.Equal(UInt256.One, CreateTruncating<UInt256, BigInteger>(source));
    }

    [Fact(DisplayName = "UInt256.CreateChecked from UInt128 should preserve value")]
    public void UInt256CreateCheckedFromUInt128ShouldPreserveValue()
    {
        Assert.Equal((UInt256)UInt128.MaxValue, CreateChecked<UInt256, UInt128>(UInt128.MaxValue));
    }

    [Fact(DisplayName = "UInt256.CreateChecked from Int128.MinValue should throw")]
    public void UInt256CreateCheckedFromNegativeInt128ShouldThrow()
    {
        Assert.Throws<OverflowException>(() => CreateChecked<UInt256, Int128>(Int128.MinValue));
    }

    [Fact(DisplayName = "byte.CreateChecked from UInt256 should throw when out of range")]
    public void ByteCreateCheckedFromUInt256ShouldThrowWhenOutOfRange()
    {
        Assert.Throws<OverflowException>(() => CreateChecked<byte, UInt256>(UInt256.MaxValue));
    }

    [Fact(DisplayName = "sbyte.CreateSaturating from UInt256.MaxValue should saturate to sbyte.MaxValue")]
    public void SByteCreateSaturatingFromUInt256MaxShouldSaturate()
    {
        Assert.Equal(sbyte.MaxValue, CreateSaturating<sbyte, UInt256>(UInt256.MaxValue));
    }

    [Fact(DisplayName = "byte.CreateTruncating from UInt256 should return the low byte")]
    public void ByteCreateTruncatingFromUInt256ShouldReturnLowByte()
    {
        UInt256 source = (UInt256)0x1234567890ABCDEFUL;
        Assert.Equal((byte)0xEF, CreateTruncating<byte, UInt256>(source));
    }

    [Fact(DisplayName = "ulong.CreateChecked from in-range UInt256 should succeed")]
    public void UlongCreateCheckedFromInRangeUInt256ShouldSucceed()
    {
        UInt256 source = (UInt256)1234567890UL;
        Assert.Equal(1234567890UL, CreateChecked<ulong, UInt256>(source));
    }

    [Fact(DisplayName = "Int128.CreateChecked from UInt256.MaxValue should throw")]
    public void Int128CreateCheckedFromUInt256MaxShouldThrow()
    {
        Assert.Throws<OverflowException>(() => CreateChecked<Int128, UInt256>(UInt256.MaxValue));
    }

    [Fact(DisplayName = "UInt128.CreateChecked from UInt256 with zero upper should succeed")]
    public void UInt128CreateCheckedFromUInt256WithZeroUpperShouldSucceed()
    {
        UInt256 source = new(UInt128.Zero, UInt128.MaxValue);
        Assert.Equal(UInt128.MaxValue, CreateChecked<UInt128, UInt256>(source));
    }

    [Fact(DisplayName = "BigInteger.CreateChecked from UInt256 should preserve value")]
    public void BigIntegerCreateCheckedFromUInt256ShouldPreserveValue()
    {
        UInt256 source = UInt256.Parse("12345678901234567890123456789012345678901234567890");
        Assert.Equal((BigInteger)source, CreateChecked<BigInteger, UInt256>(source));
    }

    [Fact(DisplayName = "Float128.CreateSaturating from UInt256.MaxValue should produce a finite Float128")]
    public void Float128CreateSaturatingFromUInt256MaxShouldProduceFinite()
    {
        Assert.True(Float128.IsFinite(CreateSaturating<Float128, UInt256>(UInt256.MaxValue)));
    }

    [Fact(DisplayName = "Float256.CreateChecked from UInt256 should succeed for any value")]
    public void Float256CreateCheckedFromUInt256ShouldSucceed()
    {
        // The conversion path is exercised — we only care that it does not throw.
        CreateChecked<Float256, UInt256>(UInt256.MaxValue);
        CreateChecked<Float256, UInt256>(UInt256.Zero);
    }
}
