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

public sealed class Float128ConvertTests
{
    private static T CreateChecked<T, TOther>(TOther value) where T : INumberBase<T> where TOther : INumberBase<TOther>
        => T.CreateChecked(value);

    private static T CreateSaturating<T, TOther>(TOther value) where T : INumberBase<T> where TOther : INumberBase<TOther>
        => T.CreateSaturating(value);

    private static T CreateTruncating<T, TOther>(TOther value) where T : INumberBase<T> where TOther : INumberBase<TOther>
        => T.CreateTruncating(value);

    [Fact(DisplayName = "Float128 should be creatable from all primitive integer source types via CreateChecked")]
    public void Float128CreateCheckedShouldAcceptPrimitiveIntegerSourceTypes()
    {
        Assert.Equal(Float128.NegativeOne, CreateChecked<Float128, sbyte>(-1));
        Assert.Equal(Float128.Two, CreateChecked<Float128, byte>(2));
        Assert.Equal(Float128.NegativeOne, CreateChecked<Float128, short>(-1));
        Assert.Equal(Float128.Two, CreateChecked<Float128, ushort>(2));
        Assert.Equal(Float128.NegativeOne, CreateChecked<Float128, int>(-1));
        Assert.Equal(Float128.Two, CreateChecked<Float128, uint>(2u));
        Assert.Equal(Float128.NegativeOne, CreateChecked<Float128, long>(-1L));
        Assert.Equal(Float128.Two, CreateChecked<Float128, ulong>(2UL));
    }

    [Fact(DisplayName = "Float128 should be creatable from primitive floating-point source types via CreateChecked")]
    public void Float128CreateCheckedShouldAcceptPrimitiveFloatingPointSourceTypes()
    {
        Assert.Equal((Float128)1.5, CreateChecked<Float128, Half>((Half)1.5));
        Assert.Equal((Float128)1.5f, CreateChecked<Float128, float>(1.5f));
        Assert.Equal((Float128)1.5, CreateChecked<Float128, double>(1.5));
    }

    [Fact(DisplayName = "Float128 should be creatable from large-width integer source types via CreateChecked")]
    public void Float128CreateCheckedShouldAcceptLargeIntegerSourceTypes()
    {
        Float128 fromInt128 = CreateChecked<Float128, Int128>(Int128.MinValue);
        Assert.Equal((Float128)(BigDecimal)(BigInteger)Int128.MinValue, fromInt128);

        Float128 fromUInt128 = CreateChecked<Float128, UInt128>(UInt128.MaxValue);
        Assert.Equal((Float128)(BigDecimal)(BigInteger)UInt128.MaxValue, fromUInt128);

        BigInteger huge = BigInteger.Pow(10, 100);
        Float128 fromBigInteger = CreateChecked<Float128, BigInteger>(huge);
        Assert.Equal((Float128)(BigDecimal)huge, fromBigInteger);
    }

    [Fact(DisplayName = "Float128 should be creatable from decimal and BigDecimal source types via CreateChecked")]
    public void Float128CreateCheckedShouldAcceptDecimalSourceTypes()
    {
        Float128 fromDecimal = CreateChecked<Float128, decimal>(123.456m);
        Assert.Equal((Float128)(BigDecimal)123.456m, fromDecimal);

        BigDecimal bigDecimal = BigDecimal.Parse("3.14159265358979323846");
        Float128 fromBigDecimal = CreateChecked<Float128, BigDecimal>(bigDecimal);
        Assert.Equal((Float128)bigDecimal, fromBigDecimal);
    }

    [Fact(DisplayName = "Float128.CreateChecked should be a no-op when source type is Float128")]
    public void Float128CreateCheckedShouldBeNoOpForFloat128SourceType()
    {
        Float128 input = (Float128)123.5;
        Float128 result = CreateChecked<Float128, Float128>(input);
        Assert.Equal(input, result);
    }

    [Fact(DisplayName = "Float128.CreateSaturating should match CreateChecked for supported source types")]
    public void Float128CreateSaturatingShouldMatchCreateCheckedForSupportedTypes()
    {
        Assert.Equal(CreateChecked<Float128, int>(int.MinValue), CreateSaturating<Float128, int>(int.MinValue));
        Assert.Equal(CreateChecked<Float128, UInt128>(UInt128.MaxValue), CreateSaturating<Float128, UInt128>(UInt128.MaxValue));
        Assert.Equal(CreateChecked<Float128, double>(1.5), CreateSaturating<Float128, double>(1.5));
    }

    [Fact(DisplayName = "Float128.CreateTruncating should match CreateChecked for supported source types")]
    public void Float128CreateTruncatingShouldMatchCreateCheckedForSupportedTypes()
    {
        Assert.Equal(CreateChecked<Float128, int>(int.MinValue), CreateTruncating<Float128, int>(int.MinValue));
        Assert.Equal(CreateChecked<Float128, UInt128>(UInt128.MaxValue), CreateTruncating<Float128, UInt128>(UInt128.MaxValue));
        Assert.Equal(CreateChecked<Float128, double>(1.5), CreateTruncating<Float128, double>(1.5));
    }

    [Fact(DisplayName = "Primitive integer types should be creatable from Float128 source via CreateChecked")]
    public void PrimitiveIntegerCreateCheckedShouldAcceptFloat128Source()
    {
        Float128 input = (Float128)42;

        Assert.Equal((sbyte)42, CreateChecked<sbyte, Float128>(input));
        Assert.Equal((byte)42, CreateChecked<byte, Float128>(input));
        Assert.Equal((short)42, CreateChecked<short, Float128>(input));
        Assert.Equal((ushort)42, CreateChecked<ushort, Float128>(input));
        Assert.Equal(42, CreateChecked<int, Float128>(input));
        Assert.Equal(42u, CreateChecked<uint, Float128>(input));
        Assert.Equal(42L, CreateChecked<long, Float128>(input));
        Assert.Equal(42UL, CreateChecked<ulong, Float128>(input));
        Assert.Equal((Int128)42, CreateChecked<Int128, Float128>(input));
        Assert.Equal((UInt128)42U, CreateChecked<UInt128, Float128>(input));
    }

    [Fact(DisplayName = "Primitive floating-point types should be creatable from Float128 source via CreateChecked")]
    public void PrimitiveFloatingPointCreateCheckedShouldAcceptFloat128Source()
    {
        Float128 input = (Float128)1.5;

        Assert.Equal((Half)1.5, CreateChecked<Half, Float128>(input));
        Assert.Equal(1.5f, CreateChecked<float, Float128>(input));
        Assert.Equal(1.5, CreateChecked<double, Float128>(input));
        Assert.Equal(1.5m, CreateChecked<decimal, Float128>(input));
    }

    [Fact(DisplayName = "BigInteger should be creatable from Float128 source via CreateChecked")]
    public void BigIntegerCreateCheckedShouldAcceptFloat128Source()
    {
        Float128 input = (Float128)42;
        Assert.Equal((BigInteger)42, CreateChecked<BigInteger, Float128>(input));
    }

    [Fact(DisplayName = "int.CreateSaturating should saturate Float128 overflow values")]
    public void CreateSaturatingOnIntShouldSaturateFloat128Overflow()
    {
        Assert.Equal(int.MaxValue, CreateSaturating<int, Float128>(Float128.MaxValue));
        Assert.Equal(int.MinValue, CreateSaturating<int, Float128>(Float128.MinValue));
    }

    [Fact(DisplayName = "int.CreateTruncating should truncate fractional Float128 source")]
    public void CreateTruncatingOnIntShouldTruncateFloat128Source()
    {
        Assert.Equal(1, CreateTruncating<int, Float128>((Float128)1.9));
    }
}
