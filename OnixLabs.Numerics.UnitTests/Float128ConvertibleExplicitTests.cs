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

public sealed class Float128ConvertibleExplicitTests
{
    [Theory(DisplayName = "Float128 to Int32 saturating cast should match double semantics for in-range values")]
    [InlineData(0.0, 0)]
    [InlineData(1.0, 1)]
    [InlineData(-1.0, -1)]
    [InlineData(1.5, 1)]
    [InlineData(-1.5, -1)]
    [InlineData(100.999, 100)]
    [InlineData(-100.999, -100)]
    [InlineData(2147483647.0, int.MaxValue)]
    [InlineData(-2147483648.0, int.MinValue)]
    public void Float128ToInt32SaturatingShouldMatchDoubleSemantics(double value, int expected)
    {
        int actual = (int)(Float128)value;
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Float128 to Int32 saturating cast of out-of-range values should saturate")]
    public void Float128ToInt32SaturatingShouldSaturateOutOfRange()
    {
        Assert.Equal(int.MaxValue, (int)(Float128)1e20);
        Assert.Equal(int.MinValue, (int)(Float128)(-1e20));
        Assert.Equal(int.MaxValue, (int)Float128.MaxValue);
        Assert.Equal(int.MinValue, (int)Float128.MinValue);
        Assert.Equal(int.MaxValue, (int)Float128.PositiveInfinity);
        Assert.Equal(int.MinValue, (int)Float128.NegativeInfinity);
    }

    [Fact(DisplayName = "Float128 to Int32 saturating cast of NaN should return zero")]
    public void Float128ToInt32SaturatingNaNShouldReturnZero()
    {
        Assert.Equal(0, (int)Float128.NaN);
    }

    [Fact(DisplayName = "Float128 to Int32 checked cast of out-of-range values should throw")]
    public void Float128ToInt32CheckedShouldThrowOutOfRange()
    {
        Assert.Throws<OverflowException>(() => checked((int)(Float128)1e20));
        Assert.Throws<OverflowException>(() => checked((int)Float128.MaxValue));
        Assert.Throws<OverflowException>(() => checked((int)Float128.PositiveInfinity));
        Assert.Throws<OverflowException>(() => checked((int)Float128.NaN));
    }

    [Theory(DisplayName = "Float128 to UInt32 cast should reject negative values")]
    [InlineData(0.0, (uint)0)]
    [InlineData(1.0, (uint)1)]
    [InlineData(4294967295.0, uint.MaxValue)]
    public void Float128ToUInt32ShouldMatchExpected(double value, uint expected)
    {
        uint actual = (uint)(Float128)value;
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Float128 to UInt32 of negative should saturate to zero")]
    public void Float128ToUInt32NegativeShouldSaturateToZero()
    {
        Assert.Equal(0u, (uint)(Float128)(-1.0));
    }

    [Fact(DisplayName = "Float128 to UInt32 checked of negative should throw")]
    public void Float128ToUInt32CheckedNegativeShouldThrow()
    {
        Assert.Throws<OverflowException>(() => checked((uint)(Float128)(-1.0)));
    }

    [Theory(DisplayName = "Float128 to byte cast should saturate")]
    [InlineData(0.0, (byte)0)]
    [InlineData(255.0, (byte)255)]
    [InlineData(127.7, (byte)127)]
    public void Float128ToByteShouldMatchExpected(double value, byte expected)
    {
        Assert.Equal(expected, (byte)(Float128)value);
    }

    [Fact(DisplayName = "Float128 to byte saturating of out-of-range should saturate")]
    public void Float128ToByteSaturatingShouldSaturate()
    {
        Assert.Equal((byte)255, (byte)(Float128)1000.0);
        Assert.Equal((byte)0, (byte)(Float128)(-1.0));
    }

    [Theory(DisplayName = "Float128 to long cast should match expected values")]
    [InlineData(0.0, 0L)]
    [InlineData(1.0, 1L)]
    [InlineData(-1.0, -1L)]
    [InlineData(9223372036854775000.0, 9223372036854774784L)]
    public void Float128ToInt64ShouldMatchExpected(double value, long expected)
    {
        Assert.Equal(expected, (long)(Float128)value);
    }

    [Fact(DisplayName = "Float128 to Int128 cast should handle large values")]
    public void Float128ToInt128ShouldHandleLargeValues()
    {
        Float128 hundred = 100;
        Assert.Equal((Int128)100, (Int128)hundred);
        Float128 negHundred = -100;
        Assert.Equal((Int128)(-100), (Int128)negHundred);
    }

    [Fact(DisplayName = "Float128 to UInt128 cast of negative should saturate to zero")]
    public void Float128ToUInt128NegativeShouldSaturateToZero()
    {
        Assert.Equal(UInt128.Zero, (UInt128)(Float128)(-1.0));
    }

    [Fact(DisplayName = "Float128 to BigInteger should handle exact integer values")]
    public void Float128ToBigIntegerShouldHandleExactIntegers()
    {
        Assert.Equal(BigInteger.Zero, (BigInteger)Float128.Zero);
        Assert.Equal(BigInteger.One, (BigInteger)Float128.One);
        Assert.Equal(BigInteger.MinusOne, (BigInteger)Float128.NegativeOne);
        Assert.Equal((BigInteger)100, (BigInteger)(Float128)100);
        Assert.Equal((BigInteger)(-100), (BigInteger)(Float128)(-100));
    }

    [Fact(DisplayName = "Float128 to BigInteger of NaN should throw")]
    public void Float128ToBigIntegerOfNaNShouldThrow()
    {
        Assert.Throws<OverflowException>(() => (BigInteger)Float128.NaN);
        Assert.Throws<OverflowException>(() => (BigInteger)Float128.PositiveInfinity);
    }

    [Fact(DisplayName = "Float128 to BigInteger should truncate fractional values")]
    public void Float128ToBigIntegerShouldTruncate()
    {
        Float128 value = 3.7;
        Assert.Equal((BigInteger)3, (BigInteger)value);
        Float128 negValue = -3.7;
        Assert.Equal((BigInteger)(-3), (BigInteger)negValue);
    }

    [Theory(DisplayName = "Float128 to double cast should round-trip representable values")]
    [InlineData(0.0)]
    [InlineData(1.0)]
    [InlineData(-1.0)]
    [InlineData(3.14)]
    [InlineData(-2.5)]
    [InlineData(100.0)]
    [InlineData(0.1)]
    [InlineData(double.MaxValue)]
    [InlineData(double.MinValue)]
    public void Float128ToDoubleShouldRoundTrip(double value)
    {
        Float128 wide = value;
        double back = (double)wide;
        if (double.IsNaN(value))
        {
            Assert.True(double.IsNaN(back));
        }
        else
        {
            Assert.Equal(value, back);
        }
    }

    [Fact(DisplayName = "Float128 to double should preserve NaN, infinity and signed zero")]
    public void Float128ToDoubleShouldPreserveSpecialValues()
    {
        Assert.True(double.IsNaN((double)Float128.NaN));
        Assert.True(double.IsPositiveInfinity((double)Float128.PositiveInfinity));
        Assert.True(double.IsNegativeInfinity((double)Float128.NegativeInfinity));
        Assert.Equal(0.0, (double)Float128.Zero);
        Assert.Equal(-0.0, (double)Float128.NegativeZero);
        Assert.True(double.IsNegative((double)Float128.NegativeZero));
    }

    [Fact(DisplayName = "Float128 to double of value larger than double.MaxValue should overflow to infinity")]
    public void Float128ToDoubleOverflowShouldReturnInfinity()
    {
        Assert.True(double.IsPositiveInfinity((double)Float128.MaxValue));
        Assert.True(double.IsNegativeInfinity((double)Float128.MinValue));
    }

    [Fact(DisplayName = "Float128 to double of Float128.Epsilon should underflow to zero")]
    public void Float128ToDoubleUnderflowShouldReturnZero()
    {
        Assert.Equal(0.0, (double)Float128.Epsilon);
    }

    [Fact(DisplayName = "Float128 to float should produce a finite single-precision value for representable inputs")]
    public void Float128ToSingleShouldProduceFinite()
    {
        float result = (float)(Float128)1.5;
        Assert.Equal(1.5f, result);
        Assert.True(float.IsPositiveInfinity((float)Float128.MaxValue));
        Assert.True(float.IsNaN((float)Float128.NaN));
    }

    [Fact(DisplayName = "Float128 to Half should produce a finite half-precision value for representable inputs")]
    public void Float128ToHalfShouldProduceFinite()
    {
        Half result = (Half)(Float128)1.0;
        Assert.Equal((Half)1.0f, result);
        Assert.True(Half.IsPositiveInfinity((Half)Float128.MaxValue));
        Assert.True(Half.IsNaN((Half)Float128.NaN));
    }

    [Theory(DisplayName = "Float128 to decimal cast should match expected for representable values")]
    [InlineData(0.0)]
    [InlineData(1.0)]
    [InlineData(-1.0)]
    [InlineData(100.0)]
    [InlineData(-100.0)]
    public void Float128ToDecimalShouldMatchExpected(double value)
    {
        decimal expected = (decimal)value;
        decimal actual = (decimal)(Float128)value;
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Float128 to decimal of NaN should throw")]
    public void Float128ToDecimalOfNaNShouldThrow()
    {
        Assert.Throws<OverflowException>(() => (decimal)Float128.NaN);
        Assert.Throws<OverflowException>(() => (decimal)Float128.PositiveInfinity);
    }
}
