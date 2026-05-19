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

public sealed class Int256ConvertTests
{
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

    [Fact(DisplayName = "Int256.CreateChecked from negative sbyte should preserve sign")]
    public void Int256CreateCheckedFromNegativeSByteShouldPreserveSign()
    {
        Assert.Equal(Int256.NegativeOne, CreateChecked<Int256, sbyte>(-1));
        Assert.Equal((Int256)(-128), CreateChecked<Int256, sbyte>(-128));
    }

    [Fact(DisplayName = "Int256.CreateChecked from large positive ulong should succeed")]
    public void Int256CreateCheckedFromLargePositiveUlongShouldSucceed()
    {
        Assert.Equal((Int256)ulong.MaxValue, CreateChecked<Int256, ulong>(ulong.MaxValue));
    }

    [Fact(DisplayName = "Int256.CreateChecked from BigInteger out of range should throw")]
    public void Int256CreateCheckedFromBigIntegerOutOfRangeShouldThrow()
    {
        BigInteger over = (BigInteger)Int256.MaxValue + BigInteger.One;
        Assert.Throws<OverflowException>(() => CreateChecked<Int256, BigInteger>(over));
    }

    [Fact(DisplayName = "Int256.CreateSaturating from BigInteger above MaxValue should saturate to MaxValue")]
    public void Int256CreateSaturatingFromBigIntegerAboveMaxShouldSaturate()
    {
        BigInteger over = (BigInteger)Int256.MaxValue + BigInteger.One;
        Assert.Equal(Int256.MaxValue, CreateSaturating<Int256, BigInteger>(over));
    }

    [Fact(DisplayName = "Int256.CreateSaturating from BigInteger below MinValue should saturate to MinValue")]
    public void Int256CreateSaturatingFromBigIntegerBelowMinShouldSaturate()
    {
        BigInteger under = (BigInteger)Int256.MinValue - BigInteger.One;
        Assert.Equal(Int256.MinValue, CreateSaturating<Int256, BigInteger>(under));
    }

    [Fact(DisplayName = "Int256.CreateTruncating from BigInteger should truncate to low 256 bits")]
    public void Int256CreateTruncatingFromBigIntegerShouldTruncate()
    {
        BigInteger over = (BigInteger.One << 256) + BigInteger.One;
        Assert.Equal(Int256.One, CreateTruncating<Int256, BigInteger>(over));
    }

    [Fact(DisplayName = "byte.CreateChecked from negative Int256 should throw")]
    public void ByteCreateCheckedFromNegativeInt256ShouldThrow()
    {
        Assert.Throws<OverflowException>(() => CreateChecked<byte, Int256>(Int256.NegativeOne));
    }

    [Fact(DisplayName = "byte.CreateSaturating from negative Int256 should saturate to zero")]
    public void ByteCreateSaturatingFromNegativeInt256ShouldSaturateZero()
    {
        Assert.Equal((byte)0, CreateSaturating<byte, Int256>(Int256.NegativeOne));
    }

    [Fact(DisplayName = "sbyte.CreateSaturating from out-of-range Int256 should saturate to bounds")]
    public void SByteCreateSaturatingFromOutOfRangeInt256ShouldSaturate()
    {
        Assert.Equal(sbyte.MaxValue, CreateSaturating<sbyte, Int256>(Int256.MaxValue));
        Assert.Equal(sbyte.MinValue, CreateSaturating<sbyte, Int256>(Int256.MinValue));
    }

    [Fact(DisplayName = "long.CreateChecked from in-range Int256 should succeed")]
    public void LongCreateCheckedFromInRangeInt256ShouldSucceed()
    {
        Int256 source = (Int256)(-12345678L);
        Assert.Equal(-12345678L, CreateChecked<long, Int256>(source));
    }

    [Fact(DisplayName = "long.CreateChecked from out-of-range Int256 should throw")]
    public void LongCreateCheckedFromOutOfRangeInt256ShouldThrow()
    {
        Assert.Throws<OverflowException>(() => CreateChecked<long, Int256>(Int256.MaxValue));
        Assert.Throws<OverflowException>(() => CreateChecked<long, Int256>(Int256.MinValue));
    }

    [Fact(DisplayName = "Int128.CreateChecked from in-range Int256 should preserve value")]
    public void Int128CreateCheckedFromInRangeInt256ShouldPreserveValue()
    {
        Int256 source = Int128.MinValue;
        Assert.Equal(Int128.MinValue, CreateChecked<Int128, Int256>(source));
    }

    [Fact(DisplayName = "UInt128.CreateChecked from negative Int256 should throw")]
    public void UInt128CreateCheckedFromNegativeInt256ShouldThrow()
    {
        Assert.Throws<OverflowException>(() => CreateChecked<UInt128, Int256>(Int256.NegativeOne));
    }

    [Fact(DisplayName = "BigInteger.CreateChecked from Int256 should preserve sign and magnitude")]
    public void BigIntegerCreateCheckedFromInt256ShouldPreserveSignAndMagnitude()
    {
        Int256 source = (Int256)(-12345678901234567L);
        Assert.Equal((BigInteger)source, CreateChecked<BigInteger, Int256>(source));
    }

    [Fact(DisplayName = "Int256.CreateChecked round-trip from Int256 should preserve value")]
    public void Int256CreateCheckedRoundTripShouldPreserveValue()
    {
        Int256 source = (Int256)(-9876);
        Assert.Equal(source, CreateChecked<Int256, Int256>(source));
    }

    [Fact(DisplayName = "Float128.CreateChecked from Int256 should produce a finite Float128")]
    public void Float128CreateCheckedFromInt256ShouldProduceFinite()
    {
        Assert.True(Float128.IsFinite(CreateChecked<Float128, Int256>(Int256.MaxValue)));
        Assert.True(Float128.IsFinite(CreateChecked<Float128, Int256>(Int256.MinValue)));
    }
}
