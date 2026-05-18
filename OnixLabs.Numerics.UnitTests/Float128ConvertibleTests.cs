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

namespace OnixLabs.Numerics.UnitTests;

public sealed class Float128ConvertibleTests
{
    private static IConvertible AsConvertible(Float128 value) => value;

    [Fact(DisplayName = "Float128.IConvertible.GetTypeCode should return Object")]
    public void Float128IConvertibleGetTypeCodeShouldReturnObject()
    {
        Assert.Equal(TypeCode.Object, AsConvertible(Float128.One).GetTypeCode());
    }

    [Fact(DisplayName = "Float128.IConvertible.ToBoolean should be false for zero and true otherwise")]
    public void Float128IConvertibleToBooleanShouldMatchZeroSemantics()
    {
        Assert.False(AsConvertible(Float128.Zero).ToBoolean(null));
        Assert.False(AsConvertible(Float128.NegativeZero).ToBoolean(null));
        Assert.True(AsConvertible(Float128.One).ToBoolean(null));
        Assert.True(AsConvertible(Float128.NegativeOne).ToBoolean(null));
    }

    [Fact(DisplayName = "Float128.IConvertible integer conversions should match checked operators")]
    public void Float128IConvertibleIntegerConversionsShouldMatchCheckedOperators()
    {
        Float128 value = 42;
        Assert.Equal((sbyte)42, AsConvertible(value).ToSByte(null));
        Assert.Equal((byte)42, AsConvertible(value).ToByte(null));
        Assert.Equal((short)42, AsConvertible(value).ToInt16(null));
        Assert.Equal((ushort)42, AsConvertible(value).ToUInt16(null));
        Assert.Equal(42, AsConvertible(value).ToInt32(null));
        Assert.Equal(42u, AsConvertible(value).ToUInt32(null));
        Assert.Equal(42L, AsConvertible(value).ToInt64(null));
        Assert.Equal(42UL, AsConvertible(value).ToUInt64(null));
        Assert.Equal('*', AsConvertible(value).ToChar(null));
    }

    [Fact(DisplayName = "Float128.IConvertible integer conversion of out-of-range should throw")]
    public void Float128IConvertibleIntegerOutOfRangeShouldThrow()
    {
        Float128 huge = 1e20;
        Assert.Throws<OverflowException>(() => AsConvertible(huge).ToInt32(null));
        Assert.Throws<OverflowException>(() => AsConvertible(Float128.NaN).ToInt32(null));
    }

    [Fact(DisplayName = "Float128.IConvertible.ToSingle and ToDouble should match explicit casts")]
    public void Float128IConvertibleFloatConversionsShouldMatchExplicitCasts()
    {
        Float128 value = 3.14;
        Assert.Equal((double)value, AsConvertible(value).ToDouble(null));
        Assert.Equal((float)value, AsConvertible(value).ToSingle(null));
    }

    [Fact(DisplayName = "Float128.IConvertible.ToDecimal should match explicit cast")]
    public void Float128IConvertibleToDecimalShouldMatchExplicitCast()
    {
        Float128 value = 100;
        Assert.Equal((decimal)value, AsConvertible(value).ToDecimal(null));
    }

    [Fact(DisplayName = "Float128.IConvertible.ToDateTime should throw InvalidCastException")]
    public void Float128IConvertibleToDateTimeShouldThrow()
    {
        Assert.Throws<InvalidCastException>(() => AsConvertible(Float128.One).ToDateTime(null));
    }

    [Fact(DisplayName = "Float128.IConvertible.ToString of an integer should produce the integer's decimal text")]
    public void Float128IConvertibleToStringOfIntegerShouldProduceIntegerText()
    {
        Float128 value = 42;
        string asString = AsConvertible(value).ToString(null);
        Assert.Equal("42", asString);
    }

    [Fact(DisplayName = "Float128.IConvertible.ToString of NaN should produce NaN")]
    public void Float128IConvertibleToStringOfNaNShouldProduceNaN()
    {
        Assert.Equal("NaN", AsConvertible(Float128.NaN).ToString(null));
    }

    [Fact(DisplayName = "Float128.IConvertible.ToString of infinity should produce the Infinity literal")]
    public void Float128IConvertibleToStringOfInfinityShouldProduceLiteral()
    {
        Assert.Equal("Infinity", AsConvertible(Float128.PositiveInfinity).ToString(null));
        Assert.Equal("-Infinity", AsConvertible(Float128.NegativeInfinity).ToString(null));
    }
}
