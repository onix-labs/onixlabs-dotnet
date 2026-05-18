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

public sealed class Float256ConvertibleExplicitTests
{
    [Theory(DisplayName = "Float256 to Int32 saturating cast should match double semantics for in-range values")]
    [InlineData(0.0, 0)]
    [InlineData(1.0, 1)]
    [InlineData(-1.0, -1)]
    [InlineData(1.5, 1)]
    [InlineData(-1.5, -1)]
    [InlineData(100.999, 100)]
    [InlineData(-100.999, -100)]
    [InlineData(2147483647.0, int.MaxValue)]
    [InlineData(-2147483648.0, int.MinValue)]
    public void Float256ToInt32SaturatingShouldMatchDoubleSemantics(double value, int expected)
    {
        int actual = (int)(Float256)value;
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Float256 to Int32 saturating cast of out-of-range values should saturate")]
    public void Float256ToInt32SaturatingShouldSaturateOutOfRange()
    {
        Assert.Equal(int.MaxValue, (int)(Float256)1e20);
        Assert.Equal(int.MinValue, (int)(Float256)(-1e20));
        Assert.Equal(int.MaxValue, (int)Float256.MaxValue);
        Assert.Equal(int.MinValue, (int)Float256.MinValue);
        Assert.Equal(int.MaxValue, (int)Float256.PositiveInfinity);
        Assert.Equal(int.MinValue, (int)Float256.NegativeInfinity);
    }

    [Fact(DisplayName = "Float256 to Int32 saturating cast of NaN should return zero")]
    public void Float256ToInt32SaturatingNaNShouldReturnZero()
    {
        Assert.Equal(0, (int)Float256.NaN);
    }

    [Fact(DisplayName = "Float256 to Int32 checked cast of out-of-range values should throw")]
    public void Float256ToInt32CheckedShouldThrowOutOfRange()
    {
        Assert.Throws<OverflowException>(() => checked((int)(Float256)1e20));
        Assert.Throws<OverflowException>(() => checked((int)Float256.MaxValue));
        Assert.Throws<OverflowException>(() => checked((int)Float256.PositiveInfinity));
        Assert.Throws<OverflowException>(() => checked((int)Float256.NaN));
    }

    [Theory(DisplayName = "Float256 to UInt32 cast should reject negative values")]
    [InlineData(0.0, (uint)0)]
    [InlineData(1.0, (uint)1)]
    [InlineData(4294967295.0, uint.MaxValue)]
    public void Float256ToUInt32ShouldMatchExpected(double value, uint expected)
    {
        uint actual = (uint)(Float256)value;
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Float256 to UInt32 of negative should saturate to zero")]
    public void Float256ToUInt32NegativeShouldSaturateToZero()
    {
        Assert.Equal(0u, (uint)(Float256)(-1.0));
    }

    [Fact(DisplayName = "Float256 to UInt32 checked of negative should throw")]
    public void Float256ToUInt32CheckedNegativeShouldThrow()
    {
        Assert.Throws<OverflowException>(() => checked((uint)(Float256)(-1.0)));
    }

    [Theory(DisplayName = "Float256 to byte cast should saturate")]
    [InlineData(0.0, (byte)0)]
    [InlineData(255.0, (byte)255)]
    [InlineData(127.7, (byte)127)]
    public void Float256ToByteShouldMatchExpected(double value, byte expected)
    {
        Assert.Equal(expected, (byte)(Float256)value);
    }

    [Fact(DisplayName = "Float256 to byte saturating of out-of-range should saturate")]
    public void Float256ToByteSaturatingShouldSaturate()
    {
        Assert.Equal((byte)255, (byte)(Float256)1000.0);
        Assert.Equal((byte)0, (byte)(Float256)(-1.0));
    }

    [Theory(DisplayName = "Float256 to long cast should match expected values")]
    [InlineData(0.0, 0L)]
    [InlineData(1.0, 1L)]
    [InlineData(-1.0, -1L)]
    // The double literal 9223372036854775000.0 snaps to the nearest binary64 value, 9223372036854774784.
    // Float256 now preserves that exact bit pattern (rather than rebuilding from the decimal text),
    // so the round-trip recovers the binary value of the literal, not its decimal expansion.
    [InlineData(9223372036854775000.0, 9223372036854774784L)]
    public void Float256ToInt64ShouldMatchExpected(double value, long expected)
    {
        Assert.Equal(expected, (long)(Float256)value);
    }

    [Fact(DisplayName = "Float256 to Int128 cast should handle large values")]
    public void Float256ToInt128ShouldHandleLargeValues()
    {
        Float256 hundred = 100;
        Assert.Equal((Int128)100, (Int128)hundred);
        Float256 negHundred = -100;
        Assert.Equal((Int128)(-100), (Int128)negHundred);
    }

    [Fact(DisplayName = "Float256 to UInt128 cast of negative should saturate to zero")]
    public void Float256ToUInt128NegativeShouldSaturateToZero()
    {
        Assert.Equal(UInt128.Zero, (UInt128)(Float256)(-1.0));
    }

    [Fact(DisplayName = "Float256 to BigInteger should handle exact integer values")]
    public void Float256ToBigIntegerShouldHandleExactIntegers()
    {
        Assert.Equal(BigInteger.Zero, (BigInteger)Float256.Zero);
        Assert.Equal(BigInteger.One, (BigInteger)Float256.One);
        Assert.Equal(BigInteger.MinusOne, (BigInteger)Float256.NegativeOne);
        Assert.Equal((BigInteger)100, (BigInteger)(Float256)100);
        Assert.Equal((BigInteger)(-100), (BigInteger)(Float256)(-100));
    }

    [Fact(DisplayName = "Float256 to BigInteger of NaN should throw")]
    public void Float256ToBigIntegerOfNaNShouldThrow()
    {
        Assert.Throws<OverflowException>(() => (BigInteger)Float256.NaN);
        Assert.Throws<OverflowException>(() => (BigInteger)Float256.PositiveInfinity);
    }

    [Fact(DisplayName = "Float256 to BigInteger should truncate fractional values")]
    public void Float256ToBigIntegerShouldTruncate()
    {
        Float256 value = 3.7;
        Assert.Equal((BigInteger)3, (BigInteger)value);
        Float256 negValue = -3.7;
        Assert.Equal((BigInteger)(-3), (BigInteger)negValue);
    }

    [Theory(DisplayName = "Float256 to double cast should round-trip every finite double value (native binary256→binary64 conversion)")]
    [InlineData(0.0)]
    [InlineData(100.0)]
    [InlineData(-100.0)]
    [InlineData(1000.0)]
    [InlineData(-1000.0)]
    [InlineData(0.1)]
    [InlineData(-0.1)]
    [InlineData(3.141592653589793)]
    [InlineData(2.718281828459045)]
    [InlineData(1.7976931348623157e+308)]
    [InlineData(-1.7976931348623157e+308)]
    [InlineData(2.2250738585072014e-308)]
    [InlineData(4.9406564584124654e-324)]
    [InlineData(1e+200)]
    [InlineData(1e-200)]
    public void Float256ToDoubleShouldRoundTrip(double value)
    {
        Float256 wide = value;
        double back = (double)wide;
        Assert.Equal(value, back);
    }

    [Fact(DisplayName = "Float256 to double of values larger than double.MaxValue should saturate to ±infinity")]
    public void Float256ToDoubleOverflowShouldSaturateToInfinity()
    {
        Float256 large = Float256.Parse("1e500", System.Globalization.CultureInfo.InvariantCulture);
        Assert.True(double.IsPositiveInfinity((double)large));
        Float256 largeNegative = Float256.Parse("-1e500", System.Globalization.CultureInfo.InvariantCulture);
        Assert.True(double.IsNegativeInfinity((double)largeNegative));
    }

    [Fact(DisplayName = "Float256 to double of values smaller than double's smallest subnormal should underflow to zero")]
    public void Float256ToDoubleUnderflowShouldReturnZeroForExtremelySmall()
    {
        Float256 tiny = Float256.Parse("1e-400", System.Globalization.CultureInfo.InvariantCulture);
        Assert.Equal(0.0, (double)tiny);
    }

    [Fact(DisplayName = "Float256 to double should preserve NaN, infinity and signed zero")]
    public void Float256ToDoubleShouldPreserveSpecialValues()
    {
        Assert.True(double.IsNaN((double)Float256.NaN));
        Assert.True(double.IsPositiveInfinity((double)Float256.PositiveInfinity));
        Assert.True(double.IsNegativeInfinity((double)Float256.NegativeInfinity));
        Assert.Equal(0.0, (double)Float256.Zero);
        Assert.Equal(-0.0, (double)Float256.NegativeZero);
        Assert.True(double.IsNegative((double)Float256.NegativeZero));
    }

    [Fact(DisplayName = "Float256 to double of Float256.Epsilon should underflow to zero")]
    public void Float256ToDoubleUnderflowShouldReturnZero()
    {
        Assert.Equal(0.0, (double)Float256.Epsilon);
    }

    [Fact(DisplayName = "Float256 to float should produce a finite single-precision value for representable inputs")]
    public void Float256ToSingleShouldProduceFinite()
    {
        float result = (float)(Float256)1000.0;
        Assert.Equal(1000.0f, result);
        Assert.True(float.IsNaN((float)Float256.NaN));
    }

    [Fact(DisplayName = "Float256 to Half should produce a finite half-precision value for representable inputs")]
    public void Float256ToHalfShouldProduceFinite()
    {
        Half result = (Half)(Float256)100.0;
        Assert.Equal((Half)100.0f, result);
        Assert.True(Half.IsNaN((Half)Float256.NaN));
    }

    [Theory(DisplayName = "Float256 to decimal cast should match expected for representable values that avoid the BigDecimal-to-decimal rounding path")]
    [InlineData(0.0)]
    [InlineData(1.5)]
    [InlineData(3.25)]
    public void Float256ToDecimalShouldMatchExpected(double value)
    {
        decimal expected = (decimal)value;
        decimal actual = (decimal)(Float256)value;
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Float256 to decimal of NaN should throw")]
    public void Float256ToDecimalOfNaNShouldThrow()
    {
        Assert.Throws<OverflowException>(() => (decimal)Float256.NaN);
        Assert.Throws<OverflowException>(() => (decimal)Float256.PositiveInfinity);
    }
}
