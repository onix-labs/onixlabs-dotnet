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

public sealed class Float128ParseTests
{
    [Fact(DisplayName = "Float128.Parse of NaN literal should return NaN")]
    public void Float128ParseOfNaNShouldReturnNaN()
    {
        Assert.True(Float128.IsNaN(Float128.Parse("NaN", CultureInfo.InvariantCulture)));
    }

    [Fact(DisplayName = "Float128.Parse of Infinity literal should return positive infinity")]
    public void Float128ParseOfInfinityShouldReturnPositiveInfinity()
    {
        Assert.True(Float128.IsPositiveInfinity(Float128.Parse("Infinity", CultureInfo.InvariantCulture)));
    }

    [Fact(DisplayName = "Float128.Parse of -Infinity literal should return negative infinity")]
    public void Float128ParseOfNegativeInfinityShouldReturnNegativeInfinity()
    {
        Assert.True(Float128.IsNegativeInfinity(Float128.Parse("-Infinity", CultureInfo.InvariantCulture)));
    }

    [Theory(DisplayName = "Float128.Parse of integer strings should produce integer Float128 values")]
    [InlineData("0", 0)]
    [InlineData("1", 1)]
    [InlineData("-1", -1)]
    [InlineData("100", 100)]
    [InlineData("-100", -100)]
    [InlineData("1000000", 1000000)]
    public void Float128ParseOfIntegerStringsShouldProduceIntegerValues(string input, int expected)
    {
        Float128 parsed = Float128.Parse(input, CultureInfo.InvariantCulture);
        Float128 expectedFloat = expected;
        Assert.Equal(expectedFloat.RawBits, parsed.RawBits);
    }

    [Theory(DisplayName = "Float128.Parse of half-precision decimals should produce exact values")]
    [InlineData("0.5", 0.5)]
    [InlineData("0.25", 0.25)]
    [InlineData("0.125", 0.125)]
    [InlineData("1.5", 1.5)]
    [InlineData("-1.5", -1.5)]
    [InlineData("2.5", 2.5)]
    public void Float128ParseOfBinaryFractionStringsShouldProduceExactValues(string input, double expected)
    {
        Float128 parsed = Float128.Parse(input, CultureInfo.InvariantCulture);
        Float128 expectedFloat = expected;
        Assert.Equal(expectedFloat.RawBits, parsed.RawBits);
    }

    [Fact(DisplayName = "Float128.Parse of scientific notation should work")]
    public void Float128ParseOfScientificNotationShouldWork()
    {
        Float128 parsed = Float128.Parse("1.5E+10", CultureInfo.InvariantCulture);
        Float128 expected = 15000000000.0;
        Assert.Equal(expected.RawBits, parsed.RawBits);
    }

    [Fact(DisplayName = "Float128.TryParse should return true for valid input")]
    public void Float128TryParseShouldReturnTrueForValidInput()
    {
        Assert.True(Float128.TryParse("1.5", CultureInfo.InvariantCulture, out Float128 result));
        Assert.Equal(((Float128)1.5).RawBits, result.RawBits);
    }

    [Fact(DisplayName = "Float128.TryParse should return false for invalid input")]
    public void Float128TryParseShouldReturnFalseForInvalidInput()
    {
        Assert.False(Float128.TryParse("not a number", CultureInfo.InvariantCulture, out _));
    }

    [Fact(DisplayName = "Float128.Parse should throw FormatException for invalid input")]
    public void Float128ParseShouldThrowForInvalidInput()
    {
        Assert.Throws<FormatException>(() => Float128.Parse("not a number", CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "Float128.Parse should handle Pi with high precision")]
    public void Float128ParseShouldHandlePiHighPrecision()
    {
        Float128 pi = Float128.Parse("3.1415926535897932384626433832795028841972", CultureInfo.InvariantCulture);
        Assert.True(Float128.IsFinite(pi));
        Assert.True(Float128.IsPositive(pi));
        Assert.Equal(Float128.Pi.RawBits, pi.RawBits);
    }
}
