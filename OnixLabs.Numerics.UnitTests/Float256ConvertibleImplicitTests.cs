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
using System.Globalization;

namespace OnixLabs.Numerics.UnitTests;

public sealed class Float256ConvertibleImplicitTests
{
    [Theory(DisplayName = "Float256 implicit conversion from sbyte should equal the parsed decimal value")]
    [InlineData((sbyte)0)]
    [InlineData((sbyte)1)]
    [InlineData((sbyte)-1)]
    [InlineData(sbyte.MaxValue)]
    [InlineData(sbyte.MinValue)]
    public void Float256ImplicitFromSByteShouldMatchParsedDecimal(sbyte value)
    {
        Float256 actual = value;
        Float256 expected = Float256.Parse(value.ToString(CultureInfo.InvariantCulture));
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "Float256 implicit conversion from byte should equal the parsed decimal value")]
    [InlineData((byte)0)]
    [InlineData((byte)1)]
    [InlineData((byte)255)]
    public void Float256ImplicitFromByteShouldMatchParsedDecimal(byte value)
    {
        Float256 actual = value;
        Float256 expected = Float256.Parse(value.ToString(CultureInfo.InvariantCulture));
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "Float256 implicit conversion from int should equal the parsed decimal value")]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(-1)]
    [InlineData(2)]
    [InlineData(10)]
    [InlineData(int.MaxValue)]
    [InlineData(int.MinValue)]
    public void Float256ImplicitFromInt32ShouldMatchParsedDecimal(int value)
    {
        Float256 actual = value;
        Float256 expected = Float256.Parse(value.ToString(CultureInfo.InvariantCulture));
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "Float256 implicit conversion from long should equal the parsed decimal value")]
    [InlineData(0L)]
    [InlineData(1L)]
    [InlineData(-1L)]
    [InlineData(long.MaxValue)]
    [InlineData(long.MinValue)]
    public void Float256ImplicitFromInt64ShouldMatchParsedDecimal(long value)
    {
        Float256 actual = value;
        Float256 expected = Float256.Parse(value.ToString(CultureInfo.InvariantCulture));
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "Float256 implicit conversion from ulong should equal the parsed decimal value")]
    [InlineData(0UL)]
    [InlineData(1UL)]
    [InlineData(ulong.MaxValue)]
    public void Float256ImplicitFromUInt64ShouldMatchParsedDecimal(ulong value)
    {
        Float256 actual = value;
        Float256 expected = Float256.Parse(value.ToString(CultureInfo.InvariantCulture));
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "Float256 implicit conversion from double should preserve the double value losslessly")]
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
    public void Float256ImplicitFromDoubleShouldRoundTripValue(double value)
    {
        Float256 wide = value;
        Assert.True(Float256.IsFinite(wide));
        Assert.Equal(double.IsNegative(value), Float256.IsNegative(wide));
    }

    [Fact(DisplayName = "Float256 implicit conversion from double should produce the expected bits for 1.0")]
    public void Float256ImplicitFromDoubleShouldProduceExpectedBitsForOne()
    {
        Float256 actual = 1.0;
        Assert.Equal(Float256.One.RawBits.Upper, actual.RawBits.Upper);
        Assert.Equal(Float256.One.RawBits.Lower, actual.RawBits.Lower);
    }

    [Fact(DisplayName = "Float256 implicit conversion from double should preserve infinity")]
    public void Float256ImplicitFromDoubleShouldPreservePositiveInfinity()
    {
        Float256 actual = double.PositiveInfinity;
        Assert.True(Float256.IsPositiveInfinity(actual));
    }

    [Fact(DisplayName = "Float256 implicit conversion from double should preserve negative infinity")]
    public void Float256ImplicitFromDoubleShouldPreserveNegativeInfinity()
    {
        Float256 actual = double.NegativeInfinity;
        Assert.True(Float256.IsNegativeInfinity(actual));
    }

    [Fact(DisplayName = "Float256 implicit conversion from double should preserve NaN")]
    public void Float256ImplicitFromDoubleShouldPreserveNaN()
    {
        Float256 actual = double.NaN;
        Assert.True(Float256.IsNaN(actual));
    }

    [Fact(DisplayName = "Float256 implicit conversion from double should preserve negative zero")]
    public void Float256ImplicitFromDoubleShouldPreserveNegativeZero()
    {
        Float256 actual = -0.0;
        Assert.True(Float256.IsZero(actual));
        Assert.True(Float256.IsNegative(actual));
    }

    [Fact(DisplayName = "Float256 implicit conversion from float should preserve the value")]
    public void Float256ImplicitFromSingleShouldPreserveValue()
    {
        Float256 actual = 1.5f;
        Assert.True(Float256.IsFinite(actual));
        Float256 doubleWide = 1.5;
        Assert.Equal(doubleWide.RawBits.Upper, actual.RawBits.Upper);
        Assert.Equal(doubleWide.RawBits.Lower, actual.RawBits.Lower);
    }

    [Fact(DisplayName = "Float256 implicit conversion from float should preserve NaN")]
    public void Float256ImplicitFromSingleShouldPreserveNaN()
    {
        Float256 actual = float.NaN;
        Assert.True(Float256.IsNaN(actual));
    }

    [Fact(DisplayName = "Float256 implicit conversion from float should preserve positive infinity")]
    public void Float256ImplicitFromSingleShouldPreservePositiveInfinity()
    {
        Float256 actual = float.PositiveInfinity;
        Assert.True(Float256.IsPositiveInfinity(actual));
    }

    [Fact(DisplayName = "Float256 implicit conversion from float subnormal should renormalise into the binary256 normal range")]
    public void Float256ImplicitFromSingleSubnormalShouldRenormalise()
    {
        Float256 actual = float.Epsilon;
        Assert.True(Float256.IsFinite(actual));
        Assert.True(Float256.IsNormal(actual));
        Float256 doubleWide = (double)float.Epsilon;
        Assert.Equal(doubleWide.RawBits.Upper, actual.RawBits.Upper);
        Assert.Equal(doubleWide.RawBits.Lower, actual.RawBits.Lower);
    }
}
