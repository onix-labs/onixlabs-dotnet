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

public sealed class Float128ArithmeticRoundTests
{
    [Theory(DisplayName = "Float128.Round with default ToEven should match double.Round")]
    [InlineData(0.0)]
    [InlineData(0.5)]
    [InlineData(-0.5)]
    [InlineData(1.5)]
    [InlineData(-1.5)]
    [InlineData(2.5)]
    [InlineData(-2.5)]
    [InlineData(3.5)]
    [InlineData(-3.5)]
    [InlineData(0.4)]
    [InlineData(0.6)]
    [InlineData(-0.4)]
    [InlineData(-0.6)]
    [InlineData(100.0)]
    [InlineData(100.5)]
    [InlineData(101.5)]
    public void Float128RoundDefaultShouldMatchDoubleRound(double value)
    {
        Float128 actual = Float128.Round(value);
        Float128 expected = double.Round(value);
        Assert.Equal(expected.RawBits, actual.RawBits);
    }

    [Theory(DisplayName = "Float128.Round with AwayFromZero should match double.Round")]
    [InlineData(0.5)]
    [InlineData(-0.5)]
    [InlineData(1.5)]
    [InlineData(-1.5)]
    [InlineData(2.5)]
    [InlineData(-2.5)]
    public void Float128RoundAwayFromZeroShouldMatchDoubleRound(double value)
    {
        Float128 actual = Float128.Round(value, MidpointRounding.AwayFromZero);
        Float128 expected = double.Round(value, MidpointRounding.AwayFromZero);
        Assert.Equal(expected.RawBits, actual.RawBits);
    }

    [Fact(DisplayName = "Float128.Round with ToZero should match Truncate")]
    public void Float128RoundToZeroShouldMatchTruncate()
    {
        foreach (double value in new[] { 0.5, -0.5, 1.5, -1.5, 2.7, -2.7 })
        {
            Float128 actual = Float128.Round(value, MidpointRounding.ToZero);
            Float128 expected = Float128.Truncate(value);
            Assert.Equal(expected.RawBits, actual.RawBits);
        }
    }

    [Fact(DisplayName = "Float128.Round with ToNegativeInfinity should match Floor")]
    public void Float128RoundToNegativeInfinityShouldMatchFloor()
    {
        foreach (double value in new[] { 0.5, -0.5, 1.5, -1.5, 2.7, -2.7 })
        {
            Float128 actual = Float128.Round(value, MidpointRounding.ToNegativeInfinity);
            Float128 expected = Float128.Floor(value);
            Assert.Equal(expected.RawBits, actual.RawBits);
        }
    }

    [Fact(DisplayName = "Float128.Round with ToPositiveInfinity should match Ceiling")]
    public void Float128RoundToPositiveInfinityShouldMatchCeiling()
    {
        foreach (double value in new[] { 0.5, -0.5, 1.5, -1.5, 2.7, -2.7 })
        {
            Float128 actual = Float128.Round(value, MidpointRounding.ToPositiveInfinity);
            Float128 expected = Float128.Ceiling(value);
            Assert.Equal(expected.RawBits, actual.RawBits);
        }
    }

    [Fact(DisplayName = "Float128.Round with invalid mode should throw")]
    public void Float128RoundWithInvalidModeShouldThrow()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => Float128.Round(Float128.One, (MidpointRounding)int.MaxValue));
    }

    [Fact(DisplayName = "Float128.Round of special values should preserve them")]
    public void Float128RoundOfSpecialValuesShouldPreserve()
    {
        Assert.Equal(Float128.Zero.RawBits, Float128.Round(Float128.Zero).RawBits);
        Assert.Equal(Float128.NegativeZero.RawBits, Float128.Round(Float128.NegativeZero).RawBits);
        Assert.Equal(Float128.PositiveInfinity.RawBits, Float128.Round(Float128.PositiveInfinity).RawBits);
        Assert.Equal(Float128.NegativeInfinity.RawBits, Float128.Round(Float128.NegativeInfinity).RawBits);
        Assert.True(Float128.IsNaN(Float128.Round(Float128.NaN)));
    }

    [Theory(DisplayName = "Float128.Round(value, digits) should produce the correctly-rounded decimal result")]
    [InlineData("3.14159", 2, "3.14")]
    [InlineData("-3.14159", 2, "-3.14")]
    [InlineData("3.14159", 4, "3.1416")]
    [InlineData("1.2345", 3, "1.234")]
    [InlineData("1234567.89", 1, "1234567.9")]
    [InlineData("0.000123456", 6, "0.000123")]
    public void Float128RoundWithDigitsShouldProduceCorrectlyRoundedResult(string valueLiteral, int digits, string expectedLiteral)
    {
        Float128 value = Float128.Parse(valueLiteral, System.Globalization.CultureInfo.InvariantCulture);
        Float128 expected = Float128.Parse(expectedLiteral, System.Globalization.CultureInfo.InvariantCulture);
        Float128 actual = Float128.Round(value, digits);
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Float128.Round with positive digits and AwayFromZero should round halves away from zero")]
    public void Float128RoundWithDigitsAwayFromZeroShouldRoundHalvesAway()
    {
        Float128 value = Float128.Parse("1.25", System.Globalization.CultureInfo.InvariantCulture);
        Float128 expected = Float128.Parse("1.3", System.Globalization.CultureInfo.InvariantCulture);
        Float128 result = Float128.Round(value, 1, MidpointRounding.AwayFromZero);
        Assert.Equal(expected, result);
    }

    [Fact(DisplayName = "Float128.Round with negative digits should round to the nearest power of ten multiple")]
    public void Float128RoundWithNegativeDigitsShouldRoundToPowerOfTenMultiple()
    {
        Float128 result = Float128.Round((Float128)1234567, -3);
        Assert.Equal((Float128)1235000, result);
    }

    [Fact(DisplayName = "Float128.Round with digits beyond precision should return the value unchanged")]
    public void Float128RoundWithDigitsBeyondPrecisionShouldReturnValue()
    {
        Float128 value = Float128.Parse("3.14159265358979323846", System.Globalization.CultureInfo.InvariantCulture);
        Float128 result = Float128.Round(value, 1000);
        Assert.Equal(value, result);
    }

    [Fact(DisplayName = "Float128.Round with negative digits exceeding Float128 range should round small values to zero")]
    public void Float128RoundWithExtremeNegativeDigitsShouldRoundSmallValuesToZero()
    {
        Float128 value = (Float128)42;
        Float128 result = Float128.Round(value, -100000);
        Assert.Equal(Float128.Zero, result);
    }

    [Fact(DisplayName = "Float128.Round with digits == int.MinValue should round to zero")]
    public void Float128RoundWithIntMinValueDigitsShouldReturnZero()
    {
        Assert.Equal(Float128.Zero, Float128.Round((Float128)1, int.MinValue));
        Assert.Equal(Float128.NegativeZero, Float128.Round((Float128)(-1), int.MinValue));
    }
}
