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

public sealed class Float256ConvertTests
{
    private static T CreateChecked<T, TOther>(TOther value) where T : INumberBase<T> where TOther : INumberBase<TOther>
        => T.CreateChecked(value);

    private static T CreateSaturating<T, TOther>(TOther value) where T : INumberBase<T> where TOther : INumberBase<TOther>
        => T.CreateSaturating(value);

    private static T CreateTruncating<T, TOther>(TOther value) where T : INumberBase<T> where TOther : INumberBase<TOther>
        => T.CreateTruncating(value);

    [Fact(DisplayName = "Float256 should be creatable from all primitive integer source types via CreateChecked")]
    public void Float256CreateCheckedShouldAcceptPrimitiveIntegerSourceTypes()
    {
        Assert.Equal(Float256.NegativeOne, CreateChecked<Float256, sbyte>(-1));
        Assert.Equal(Float256.Two, CreateChecked<Float256, byte>(2));
        Assert.Equal(Float256.NegativeOne, CreateChecked<Float256, short>(-1));
        Assert.Equal(Float256.Two, CreateChecked<Float256, ushort>(2));
        Assert.Equal(Float256.NegativeOne, CreateChecked<Float256, int>(-1));
        Assert.Equal(Float256.Two, CreateChecked<Float256, uint>(2u));
        Assert.Equal(Float256.NegativeOne, CreateChecked<Float256, long>(-1L));
        Assert.Equal(Float256.Two, CreateChecked<Float256, ulong>(2UL));
    }

    [Fact(DisplayName = "Float256 should be creatable from primitive floating-point source types via CreateChecked")]
    public void Float256CreateCheckedShouldAcceptPrimitiveFloatingPointSourceTypes()
    {
        Assert.Equal((Float256)1.5, CreateChecked<Float256, Half>((Half)1.5));
        Assert.Equal((Float256)1.5f, CreateChecked<Float256, float>(1.5f));
        Assert.Equal((Float256)1.5, CreateChecked<Float256, double>(1.5));
    }

    [Fact(DisplayName = "Float256 should be creatable from large-width integer source types via CreateChecked")]
    public void Float256CreateCheckedShouldAcceptLargeIntegerSourceTypes()
    {
        Float256 fromInt128 = CreateChecked<Float256, Int128>(Int128.MinValue);
        Assert.Equal((Float256)(BigDecimal)(BigInteger)Int128.MinValue, fromInt128);

        Float256 fromUInt128 = CreateChecked<Float256, UInt128>(UInt128.MaxValue);
        Assert.Equal((Float256)(BigDecimal)(BigInteger)UInt128.MaxValue, fromUInt128);

        BigInteger huge = BigInteger.Pow(10, 100);
        Float256 fromBigInteger = CreateChecked<Float256, BigInteger>(huge);
        Assert.Equal((Float256)(BigDecimal)huge, fromBigInteger);
    }

    [Fact(DisplayName = "Float256 should be creatable from decimal and BigDecimal source types via CreateChecked")]
    public void Float256CreateCheckedShouldAcceptDecimalSourceTypes()
    {
        Float256 fromDecimal = CreateChecked<Float256, decimal>(123.456m);
        Assert.Equal((Float256)(BigDecimal)123.456m, fromDecimal);

        BigDecimal bigDecimal = BigDecimal.Parse("3.14159265358979323846");
        Float256 fromBigDecimal = CreateChecked<Float256, BigDecimal>(bigDecimal);
        Assert.Equal((Float256)bigDecimal, fromBigDecimal);
    }

    [Fact(DisplayName = "Float256.CreateChecked should be a no-op when source type is Float256")]
    public void Float256CreateCheckedShouldBeNoOpForFloat256SourceType()
    {
        Float256 input = (Float256)123.5;
        Float256 result = CreateChecked<Float256, Float256>(input);
        Assert.Equal(input, result);
    }

    [Fact(DisplayName = "Float256.CreateSaturating should match CreateChecked for supported source types")]
    public void Float256CreateSaturatingShouldMatchCreateCheckedForSupportedTypes()
    {
        Assert.Equal(CreateChecked<Float256, int>(int.MinValue), CreateSaturating<Float256, int>(int.MinValue));
        Assert.Equal(CreateChecked<Float256, UInt128>(UInt128.MaxValue), CreateSaturating<Float256, UInt128>(UInt128.MaxValue));
        Assert.Equal(CreateChecked<Float256, double>(1.5), CreateSaturating<Float256, double>(1.5));
    }

    [Fact(DisplayName = "Float256.CreateTruncating should match CreateChecked for supported source types")]
    public void Float256CreateTruncatingShouldMatchCreateCheckedForSupportedTypes()
    {
        Assert.Equal(CreateChecked<Float256, int>(int.MinValue), CreateTruncating<Float256, int>(int.MinValue));
        Assert.Equal(CreateChecked<Float256, UInt128>(UInt128.MaxValue), CreateTruncating<Float256, UInt128>(UInt128.MaxValue));
        Assert.Equal(CreateChecked<Float256, double>(1.5), CreateTruncating<Float256, double>(1.5));
    }

    [Fact(DisplayName = "Primitive integer types should be creatable from Float256 source via CreateChecked")]
    public void PrimitiveIntegerCreateCheckedShouldAcceptFloat256Source()
    {
        Float256 input = (Float256)42;

        Assert.Equal((sbyte)42, CreateChecked<sbyte, Float256>(input));
        Assert.Equal((byte)42, CreateChecked<byte, Float256>(input));
        Assert.Equal((short)42, CreateChecked<short, Float256>(input));
        Assert.Equal((ushort)42, CreateChecked<ushort, Float256>(input));
        Assert.Equal(42, CreateChecked<int, Float256>(input));
        Assert.Equal(42u, CreateChecked<uint, Float256>(input));
        Assert.Equal(42L, CreateChecked<long, Float256>(input));
        Assert.Equal(42UL, CreateChecked<ulong, Float256>(input));
        Assert.Equal((Int128)42, CreateChecked<Int128, Float256>(input));
        Assert.Equal((UInt128)42U, CreateChecked<UInt128, Float256>(input));
    }

    [Fact(DisplayName = "Primitive floating-point types should be creatable from Float256 source via CreateChecked")]
    public void PrimitiveFloatingPointCreateCheckedShouldAcceptFloat256Source()
    {
        Float256 input = (Float256)1.5;

        Assert.Equal((Half)1.5, CreateChecked<Half, Float256>(input));
        Assert.Equal(1.5f, CreateChecked<float, Float256>(input));
        Assert.Equal(1.5, CreateChecked<double, Float256>(input));
        Assert.Equal(1.5m, CreateChecked<decimal, Float256>(input));
    }

    [Fact(DisplayName = "BigInteger should be creatable from Float256 source via CreateChecked")]
    public void BigIntegerCreateCheckedShouldAcceptFloat256Source()
    {
        Float256 input = (Float256)42;
        Assert.Equal((BigInteger)42, CreateChecked<BigInteger, Float256>(input));
    }

    [Fact(DisplayName = "int.CreateSaturating should saturate Float256 overflow values")]
    public void CreateSaturatingOnIntShouldSaturateFloat256Overflow()
    {
        Assert.Equal(int.MaxValue, CreateSaturating<int, Float256>(Float256.MaxValue));
        Assert.Equal(int.MinValue, CreateSaturating<int, Float256>(Float256.MinValue));
    }

    [Fact(DisplayName = "int.CreateTruncating should truncate fractional Float256 source")]
    public void CreateTruncatingOnIntShouldTruncateFloat256Source()
    {
        Assert.Equal(1, CreateTruncating<int, Float256>((Float256)1.9));
    }
}
