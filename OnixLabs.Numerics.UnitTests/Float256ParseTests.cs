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

public sealed class Float256ParseTests
{
    [Fact(DisplayName = "Float256.Parse of NaN literal should return NaN")]
    public void Float256ParseOfNaNShouldReturnNaN()
    {
        Assert.True(Float256.IsNaN(Float256.Parse("NaN", CultureInfo.InvariantCulture)));
    }

    [Fact(DisplayName = "Float256.Parse of Infinity literal should return positive infinity")]
    public void Float256ParseOfInfinityShouldReturnPositiveInfinity()
    {
        Assert.True(Float256.IsPositiveInfinity(Float256.Parse("Infinity", CultureInfo.InvariantCulture)));
    }

    [Fact(DisplayName = "Float256.Parse of -Infinity literal should return negative infinity")]
    public void Float256ParseOfNegativeInfinityShouldReturnNegativeInfinity()
    {
        Assert.True(Float256.IsNegativeInfinity(Float256.Parse("-Infinity", CultureInfo.InvariantCulture)));
    }

    [Theory(DisplayName = "Float256.Parse of integer strings should produce integer Float256 values")]
    [InlineData("0", 0)]
    [InlineData("1", 1)]
    [InlineData("-1", -1)]
    [InlineData("100", 100)]
    [InlineData("-100", -100)]
    [InlineData("1000000", 1000000)]
    public void Float256ParseOfIntegerStringsShouldProduceIntegerValues(string input, int expected)
    {
        Float256 parsed = Float256.Parse(input, CultureInfo.InvariantCulture);
        Float256 expectedFloat = expected;
        Assert.Equal(expectedFloat.Bits.UpperBits, parsed.Bits.UpperBits);
        Assert.Equal(expectedFloat.Bits.LowerBits, parsed.Bits.LowerBits);
    }

    [Theory(DisplayName = "Float256.Parse of half-precision decimals should produce exact values")]
    [InlineData("0.5", 0.5)]
    [InlineData("0.25", 0.25)]
    [InlineData("0.125", 0.125)]
    [InlineData("1.5", 1.5)]
    [InlineData("-1.5", -1.5)]
    [InlineData("2.5", 2.5)]
    public void Float256ParseOfBinaryFractionStringsShouldProduceExactValues(string input, double expected)
    {
        Float256 parsed = Float256.Parse(input, CultureInfo.InvariantCulture);
        Float256 expectedFloat = expected;
        Assert.Equal(expectedFloat.Bits.UpperBits, parsed.Bits.UpperBits);
        Assert.Equal(expectedFloat.Bits.LowerBits, parsed.Bits.LowerBits);
    }

    [Fact(DisplayName = "Float256.Parse of scientific notation should work")]
    public void Float256ParseOfScientificNotationShouldWork()
    {
        Float256 parsed = Float256.Parse("1.5E+10", CultureInfo.InvariantCulture);
        Float256 expected = 15000000000.0;
        Assert.Equal(expected.Bits.UpperBits, parsed.Bits.UpperBits);
        Assert.Equal(expected.Bits.LowerBits, parsed.Bits.LowerBits);
    }

    [Fact(DisplayName = "Float256.TryParse should return true for valid input")]
    public void Float256TryParseShouldReturnTrueForValidInput()
    {
        Assert.True(Float256.TryParse("1.5", CultureInfo.InvariantCulture, out Float256 result));
        Assert.Equal(((Float256)1.5).Bits.UpperBits, result.Bits.UpperBits);
        Assert.Equal(((Float256)1.5).Bits.LowerBits, result.Bits.LowerBits);
    }

    [Fact(DisplayName = "Float256.TryParse should return false for invalid input")]
    public void Float256TryParseShouldReturnFalseForInvalidInput()
    {
        Assert.False(Float256.TryParse("not a number", CultureInfo.InvariantCulture, out _));
    }

    [Fact(DisplayName = "Float256.Parse should throw FormatException for invalid input")]
    public void Float256ParseShouldThrowForInvalidInput()
    {
        Assert.Throws<FormatException>(() => Float256.Parse("not a number", CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "Float256.Parse should handle Pi with high precision")]
    public void Float256ParseShouldHandlePiHighPrecision()
    {
        // Use the full binary256-precision decimal expansion of Pi (~100 digits) to ensure correct
        // rounding to Float256.Pi.
        Float256 pi = Float256.Parse(
            "3.14159265358979323846264338327950288419716939937510582097494459230781640628620899862803482534211706798214",
            CultureInfo.InvariantCulture);
        Assert.True(Float256.IsFinite(pi));
        Assert.True(Float256.IsPositive(pi));
        Assert.Equal(Float256.Pi.Bits.UpperBits, pi.Bits.UpperBits);
        Assert.Equal(Float256.Pi.Bits.LowerBits, pi.Bits.LowerBits);
    }
}
