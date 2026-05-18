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

public sealed class Float256ArithmeticRoundTests
{
    [Theory(DisplayName = "Float256.Round with default ToEven should match double.Round")]
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
    public void Float256RoundDefaultShouldMatchDoubleRound(double value)
    {
        Float256 actual = Float256.Round(value);
        Float256 expected = double.Round(value);
        Assert.Equal(expected.RawHighBits, actual.RawHighBits);
        Assert.Equal(expected.RawLowBits, actual.RawLowBits);
    }

    [Theory(DisplayName = "Float256.Round with AwayFromZero should match double.Round")]
    [InlineData(0.5)]
    [InlineData(-0.5)]
    [InlineData(1.5)]
    [InlineData(-1.5)]
    [InlineData(2.5)]
    [InlineData(-2.5)]
    public void Float256RoundAwayFromZeroShouldMatchDoubleRound(double value)
    {
        Float256 actual = Float256.Round(value, MidpointRounding.AwayFromZero);
        Float256 expected = double.Round(value, MidpointRounding.AwayFromZero);
        Assert.Equal(expected.RawHighBits, actual.RawHighBits);
        Assert.Equal(expected.RawLowBits, actual.RawLowBits);
    }

    [Fact(DisplayName = "Float256.Round with ToZero should match Truncate")]
    public void Float256RoundToZeroShouldMatchTruncate()
    {
        foreach (double value in new[] { 0.5, -0.5, 1.5, -1.5, 2.7, -2.7 })
        {
            Float256 actual = Float256.Round(value, MidpointRounding.ToZero);
            Float256 expected = Float256.Truncate(value);
            Assert.Equal(expected.RawHighBits, actual.RawHighBits);
            Assert.Equal(expected.RawLowBits, actual.RawLowBits);
        }
    }

    [Fact(DisplayName = "Float256.Round with ToNegativeInfinity should match Floor")]
    public void Float256RoundToNegativeInfinityShouldMatchFloor()
    {
        foreach (double value in new[] { 0.5, -0.5, 1.5, -1.5, 2.7, -2.7 })
        {
            Float256 actual = Float256.Round(value, MidpointRounding.ToNegativeInfinity);
            Float256 expected = Float256.Floor(value);
            Assert.Equal(expected.RawHighBits, actual.RawHighBits);
            Assert.Equal(expected.RawLowBits, actual.RawLowBits);
        }
    }

    [Fact(DisplayName = "Float256.Round with ToPositiveInfinity should match Ceiling")]
    public void Float256RoundToPositiveInfinityShouldMatchCeiling()
    {
        foreach (double value in new[] { 0.5, -0.5, 1.5, -1.5, 2.7, -2.7 })
        {
            Float256 actual = Float256.Round(value, MidpointRounding.ToPositiveInfinity);
            Float256 expected = Float256.Ceiling(value);
            Assert.Equal(expected.RawHighBits, actual.RawHighBits);
            Assert.Equal(expected.RawLowBits, actual.RawLowBits);
        }
    }

    [Fact(DisplayName = "Float256.Round with invalid mode should throw")]
    public void Float256RoundWithInvalidModeShouldThrow()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => Float256.Round(Float256.One, (MidpointRounding)int.MaxValue));
    }

    [Fact(DisplayName = "Float256.Round of special values should preserve them")]
    public void Float256RoundOfSpecialValuesShouldPreserve()
    {
        Assert.Equal(Float256.Zero.RawHighBits, Float256.Round(Float256.Zero).RawHighBits);
        Assert.Equal(Float256.Zero.RawLowBits, Float256.Round(Float256.Zero).RawLowBits);
        Assert.Equal(Float256.NegativeZero.RawHighBits, Float256.Round(Float256.NegativeZero).RawHighBits);
        Assert.Equal(Float256.NegativeZero.RawLowBits, Float256.Round(Float256.NegativeZero).RawLowBits);
        Assert.Equal(Float256.PositiveInfinity.RawHighBits, Float256.Round(Float256.PositiveInfinity).RawHighBits);
        Assert.Equal(Float256.PositiveInfinity.RawLowBits, Float256.Round(Float256.PositiveInfinity).RawLowBits);
        Assert.Equal(Float256.NegativeInfinity.RawHighBits, Float256.Round(Float256.NegativeInfinity).RawHighBits);
        Assert.Equal(Float256.NegativeInfinity.RawLowBits, Float256.Round(Float256.NegativeInfinity).RawLowBits);
        Assert.True(Float256.IsNaN(Float256.Round(Float256.NaN)));
    }

    [Theory(DisplayName = "Float256.Round(value, digits) should produce the correctly-rounded decimal result")]
    // Note: inputs are parsed as Float256 so the test exercises the actual decimal rounding rather than
    // binary-double pre-rounding; outputs are checked against an explicitly-parsed Float256 reference.
    [InlineData("3.14159", 2, "3.14")]
    [InlineData("-3.14159", 2, "-3.14")]
    [InlineData("3.14159", 4, "3.1416")]
    [InlineData("1.2345", 3, "1.234")]
    [InlineData("1234567.89", 1, "1234567.9")]
    [InlineData("0.000123456", 6, "0.000123")]
    public void Float256RoundWithDigitsShouldProduceCorrectlyRoundedResult(string valueLiteral, int digits, string expectedLiteral)
    {
        Float256 value = Float256.Parse(valueLiteral, System.Globalization.CultureInfo.InvariantCulture);
        Float256 expected = Float256.Parse(expectedLiteral, System.Globalization.CultureInfo.InvariantCulture);
        Float256 actual = Float256.Round(value, digits);
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Float256.Round with positive digits and AwayFromZero should round halves away from zero")]
    public void Float256RoundWithDigitsAwayFromZeroShouldRoundHalvesAway()
    {
        // 1.25 is exactly representable in binary; round to 1 fractional digit away from zero → 1.3.
        Float256 value = Float256.Parse("1.25", System.Globalization.CultureInfo.InvariantCulture);
        Float256 expected = Float256.Parse("1.3", System.Globalization.CultureInfo.InvariantCulture);
        Float256 result = Float256.Round(value, 1, MidpointRounding.AwayFromZero);
        Assert.Equal(expected, result);
    }

    [Fact(DisplayName = "Float256.Round with negative digits should round to the nearest power of ten multiple")]
    public void Float256RoundWithNegativeDigitsShouldRoundToPowerOfTenMultiple()
    {
        // Round 1234567 to nearest 1000 (digits = -3) with ToEven → 1235000.
        Float256 result = Float256.Round((Float256)1234567, -3);
        Assert.Equal((Float256)1235000, result);
    }

    [Fact(DisplayName = "Float256.Round with digits beyond precision should return the value unchanged")]
    public void Float256RoundWithDigitsBeyondPrecisionShouldReturnValue()
    {
        Float256 value = Float256.Parse("3.14159265358979323846", System.Globalization.CultureInfo.InvariantCulture);
        Float256 result = Float256.Round(value, 1000);
        Assert.Equal(value, result);
    }

    [Fact(DisplayName = "Float256.Round with negative digits exceeding Float256 range should round small values to zero")]
    public void Float256RoundWithExtremeNegativeDigitsShouldRoundSmallValuesToZero()
    {
        Float256 value = (Float256)42;
        Float256 result = Float256.Round(value, -100000);
        Assert.Equal(Float256.Zero, result);
    }

    [Fact(DisplayName = "Float256.Round with digits == int.MinValue should round to zero")]
    public void Float256RoundWithIntMinValueDigitsShouldReturnZero()
    {
        Assert.Equal(Float256.Zero, Float256.Round((Float256)1, int.MinValue));
        Assert.Equal(Float256.NegativeZero, Float256.Round((Float256)(-1), int.MinValue));
    }
}
