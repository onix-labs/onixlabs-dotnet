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

    [Theory(DisplayName = "Float128 to decimal should round-trip decimal values with up to 29 significant digits and keep their shortest scale")]
    [InlineData("0.1")]
    [InlineData("-0.1")]
    [InlineData("0.5")]
    [InlineData("123.456")]
    [InlineData("0.3333333333333333333333333333")]
    [InlineData("7.9228162514264337593543950335")]
    [InlineData("12345678901234567890.123456789")]
    [InlineData("0.0000000000000000000000000001")]
    [InlineData("79228162514264337593543950335")]
    [InlineData("-79228162514264337593543950335")]
    public void Float128ToDecimalShouldRoundTripDecimalValues(string text)
    {
        // Oracle: decimal.Parse of the literal. A decimal with at most 29 significant digits lies within 2^-113 relative of its
        // nearest binary128 value, far inside half a unit of its own last place, so the exact conversion must recover it.
        decimal expected = decimal.Parse(text, CultureInfo.InvariantCulture);
        Float128 wide = expected;

        decimal actual = (decimal)wide;

        Assert.Equal(expected, actual);
        Assert.Equal(text, actual.ToString(CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "Float128 to decimal should expose the exact binary value of a double rather than its shortest representation")]
    public void Float128ToDecimalShouldUseExactBinaryValueOfDouble()
    {
        // Oracle: the binary64 nearest to 0.1 is exactly 0.1000000000000000055511151231257827021181583404541015625;
        // at decimal's 28 fractional digits the 29th digit (2) rounds down.
        Float128 wide = 0.1;

        decimal actual = (decimal)wide;

        Assert.Equal(decimal.Parse("0.1000000000000000055511151231", CultureInfo.InvariantCulture), actual);
    }

    [Theory(DisplayName = "Float128 to decimal should round to nearest at 28 fractional digits")]
    [InlineData(1, 3, "0.3333333333333333333333333333")]
    [InlineData(2, 3, "0.6666666666666666666666666667")]
    [InlineData(1, 7, "0.1428571428571428571428571429")]
    public void Float128ToDecimalShouldRoundQuotientsToNearest(int numerator, int denominator, string expected)
    {
        // Oracle: the exact quotients 1/3, 2/3 and 1/7 are 0.333…, 0.666… and 0.142857 142857 142857 142857 1428|57…;
        // the binary128 quotient differs from them by less than 1E-33, which cannot move the 28th digit.
        Float128 quotient = (Float128)numerator / denominator;

        decimal actual = (decimal)quotient;

        Assert.Equal(decimal.Parse(expected, CultureInfo.InvariantCulture), actual);
    }

    [Theory(DisplayName = "Float128 to decimal should round magnitudes below half of 1E-28 to zero and above it to 1E-28")]
    [InlineData(1E-30, "0")]
    [InlineData(4E-29, "0")]
    [InlineData(6E-29, "0.0000000000000000000000000001")]
    [InlineData(-6E-29, "-0.0000000000000000000000000001")]
    public void Float128ToDecimalShouldRoundTinyMagnitudes(double value, string expected)
    {
        Float128 wide = value;

        decimal actual = (decimal)wide;

        Assert.Equal(decimal.Parse(expected, CultureInfo.InvariantCulture), actual);
    }

    [Fact(DisplayName = "Float128 to decimal should convert negative zero and the smallest subnormals to zero")]
    public void Float128ToDecimalShouldConvertNegativeZeroAndEpsilonToZero()
    {
        Assert.Equal(decimal.Zero, (decimal)Float128.NegativeZero);
        Assert.Equal(decimal.Zero, (decimal)Float128.Epsilon);
        Assert.Equal(decimal.Zero, (decimal)(-Float128.Epsilon));
    }

    [Fact(DisplayName = "Float128 to decimal should fit values that round below 2^96 and throw once the magnitude rounds to 2^96")]
    public void Float128ToDecimalShouldThrowWhenMagnitudeRoundsToTwoToThe96()
    {
        Float128 limit = Float128.ScaleB(Float128.One, 96);
        Float128 largestDecimal = decimal.MaxValue;
        Float128 roundsDown = limit - (Float128)0.75;
        Float128 tiesToEven = limit - (Float128)0.5;

        Assert.Equal(decimal.MaxValue, (decimal)largestDecimal);
        Assert.Equal(decimal.MaxValue, (decimal)roundsDown);
        Assert.Throws<OverflowException>(() => (decimal)tiesToEven);
        Assert.Throws<OverflowException>(() => (decimal)limit);
        Assert.Throws<OverflowException>(() => (decimal)(-limit));
        Assert.Throws<OverflowException>(() => (decimal)Float128.MaxValue);
    }
}
