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

public sealed class Float128ConvertibleImplicitTests
{
    [Theory(DisplayName = "Float128 implicit conversion from sbyte should produce expected bits")]
    [InlineData((sbyte)0, 0x0000_0000_0000_0000UL, 0x0000_0000_0000_0000UL)]
    [InlineData((sbyte)1, 0x3FFF_0000_0000_0000UL, 0x0000_0000_0000_0000UL)]
    [InlineData((sbyte)-1, 0xBFFF_0000_0000_0000UL, 0x0000_0000_0000_0000UL)]
    [InlineData(sbyte.MaxValue, 0x4005_FC00_0000_0000UL, 0x0000_0000_0000_0000UL)]
    [InlineData(sbyte.MinValue, 0xC006_0000_0000_0000UL, 0x0000_0000_0000_0000UL)]
    public void Float128ImplicitFromSByteShouldProduceExpectedBits(sbyte value, ulong expectedHigh, ulong expectedLow)
    {
        Float128 actual = value;
        Assert.Equal(new UInt128(expectedHigh, expectedLow), actual.RawBits);
    }

    [Theory(DisplayName = "Float128 implicit conversion from byte should produce expected bits")]
    [InlineData((byte)0, 0x0000_0000_0000_0000UL, 0x0000_0000_0000_0000UL)]
    [InlineData((byte)1, 0x3FFF_0000_0000_0000UL, 0x0000_0000_0000_0000UL)]
    [InlineData((byte)255, 0x4006_FE00_0000_0000UL, 0x0000_0000_0000_0000UL)]
    public void Float128ImplicitFromByteShouldProduceExpectedBits(byte value, ulong expectedHigh, ulong expectedLow)
    {
        Float128 actual = value;
        Assert.Equal(new UInt128(expectedHigh, expectedLow), actual.RawBits);
    }

    [Theory(DisplayName = "Float128 implicit conversion from int should produce expected bits")]
    [InlineData(0, 0x0000_0000_0000_0000UL, 0x0000_0000_0000_0000UL)]
    [InlineData(1, 0x3FFF_0000_0000_0000UL, 0x0000_0000_0000_0000UL)]
    [InlineData(-1, 0xBFFF_0000_0000_0000UL, 0x0000_0000_0000_0000UL)]
    [InlineData(2, 0x4000_0000_0000_0000UL, 0x0000_0000_0000_0000UL)]
    [InlineData(10, 0x4002_4000_0000_0000UL, 0x0000_0000_0000_0000UL)]
    [InlineData(int.MaxValue, 0x401D_FFFF_FFFC_0000UL, 0x0000_0000_0000_0000UL)]
    [InlineData(int.MinValue, 0xC01E_0000_0000_0000UL, 0x0000_0000_0000_0000UL)]
    public void Float128ImplicitFromInt32ShouldProduceExpectedBits(int value, ulong expectedHigh, ulong expectedLow)
    {
        Float128 actual = value;
        Assert.Equal(new UInt128(expectedHigh, expectedLow), actual.RawBits);
    }

    [Theory(DisplayName = "Float128 implicit conversion from long should produce expected bits")]
    [InlineData(0L, 0x0000_0000_0000_0000UL, 0x0000_0000_0000_0000UL)]
    [InlineData(1L, 0x3FFF_0000_0000_0000UL, 0x0000_0000_0000_0000UL)]
    [InlineData(-1L, 0xBFFF_0000_0000_0000UL, 0x0000_0000_0000_0000UL)]
    [InlineData(long.MaxValue, 0x403D_FFFF_FFFF_FFFFUL, 0xFFFC_0000_0000_0000UL)]
    [InlineData(long.MinValue, 0xC03E_0000_0000_0000UL, 0x0000_0000_0000_0000UL)]
    public void Float128ImplicitFromInt64ShouldProduceExpectedBits(long value, ulong expectedHigh, ulong expectedLow)
    {
        Float128 actual = value;
        Assert.Equal(new UInt128(expectedHigh, expectedLow), actual.RawBits);
    }

    [Theory(DisplayName = "Float128 implicit conversion from ulong should produce expected bits")]
    [InlineData(0UL, 0x0000_0000_0000_0000UL, 0x0000_0000_0000_0000UL)]
    [InlineData(1UL, 0x3FFF_0000_0000_0000UL, 0x0000_0000_0000_0000UL)]
    [InlineData(ulong.MaxValue, 0x403E_FFFF_FFFF_FFFFUL, 0xFFFE_0000_0000_0000UL)]
    public void Float128ImplicitFromUInt64ShouldProduceExpectedBits(ulong value, ulong expectedHigh, ulong expectedLow)
    {
        Float128 actual = value;
        Assert.Equal(new UInt128(expectedHigh, expectedLow), actual.RawBits);
    }

    [Theory(DisplayName = "Float128 implicit conversion from double should preserve the double value losslessly")]
    [InlineData(0.0)]
    [InlineData(1.0)]
    [InlineData(-1.0)]
    [InlineData(2.0)]
    [InlineData(10.0)]
    [InlineData(3.141592653589793)]
    [InlineData(2.718281828459045)]
    [InlineData(double.MaxValue)]
    [InlineData(double.MinValue)]
    [InlineData(double.Epsilon)]
    public void Float128ImplicitFromDoubleShouldRoundTripValue(double value)
    {
        Float128 wide = value;
        Assert.True(Float128.IsFinite(wide));
        Assert.Equal(double.IsNegative(value), Float128.IsNegative(wide));
    }

    [Fact(DisplayName = "Float128 implicit conversion from double should produce the expected bits for 1.0")]
    public void Float128ImplicitFromDoubleShouldProduceExpectedBitsForOne()
    {
        Float128 actual = 1.0;
        Assert.Equal(Float128.One.RawBits, actual.RawBits);
    }

    [Fact(DisplayName = "Float128 implicit conversion from double should preserve infinity")]
    public void Float128ImplicitFromDoubleShouldPreservePositiveInfinity()
    {
        Float128 actual = double.PositiveInfinity;
        Assert.True(Float128.IsPositiveInfinity(actual));
    }

    [Fact(DisplayName = "Float128 implicit conversion from double should preserve negative infinity")]
    public void Float128ImplicitFromDoubleShouldPreserveNegativeInfinity()
    {
        Float128 actual = double.NegativeInfinity;
        Assert.True(Float128.IsNegativeInfinity(actual));
    }

    [Fact(DisplayName = "Float128 implicit conversion from double should preserve NaN")]
    public void Float128ImplicitFromDoubleShouldPreserveNaN()
    {
        Float128 actual = double.NaN;
        Assert.True(Float128.IsNaN(actual));
    }

    [Fact(DisplayName = "Float128 implicit conversion from double should preserve negative zero")]
    public void Float128ImplicitFromDoubleShouldPreserveNegativeZero()
    {
        Float128 actual = -0.0;
        Assert.True(Float128.IsZero(actual));
        Assert.True(Float128.IsNegative(actual));
    }

    [Fact(DisplayName = "Float128 implicit conversion from float should preserve the value")]
    public void Float128ImplicitFromSingleShouldPreserveValue()
    {
        Float128 actual = 1.5f;
        Assert.True(Float128.IsFinite(actual));
        Float128 doubleWide = 1.5;
        Assert.Equal(doubleWide.RawBits, actual.RawBits);
    }

    [Fact(DisplayName = "Float128 implicit conversion from float should preserve NaN")]
    public void Float128ImplicitFromSingleShouldPreserveNaN()
    {
        Float128 actual = float.NaN;
        Assert.True(Float128.IsNaN(actual));
    }

    [Fact(DisplayName = "Float128 implicit conversion from float should preserve positive infinity")]
    public void Float128ImplicitFromSingleShouldPreservePositiveInfinity()
    {
        Float128 actual = float.PositiveInfinity;
        Assert.True(Float128.IsPositiveInfinity(actual));
    }

    [Fact(DisplayName = "Float128 implicit conversion from float subnormal should renormalise into the binary128 normal range")]
    public void Float128ImplicitFromSingleSubnormalShouldRenormalise()
    {
        Float128 actual = float.Epsilon;
        Assert.True(Float128.IsFinite(actual));
        Assert.True(Float128.IsNormal(actual));
        Float128 doubleWide = (double)float.Epsilon;
        Assert.Equal(doubleWide.RawBits, actual.RawBits);
    }
}
