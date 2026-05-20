// Copyright 2020-2025 ONIXLabs
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
using OnixLabs.Numerics;

namespace OnixLabs.Units.UnitTests;

public sealed class AreaTests
{
    [Fact(DisplayName = "Area.Zero should produce the expected result")]
    public void AreaZeroShouldProduceExpectedResult()
    {
        // Given / When
        Area<Float256> area = Area<Float256>.Zero;

        // Then
        Assert.Equal(Float256.Zero, area.SquareQuectoMeters);
    }

    [Theory(DisplayName = "Area.FromSquareQuectometers should produce the expected SquareQuectoMeters")]
    [InlineData("0", "0")]
    [InlineData("1", "1")]
    [InlineData("2.5", "2.5")]
    public void AreaFromSquareQuectometersShouldProduceExpectedSquareQuectoMeters(string value, string expected)
    {
        Area<Float256> a = Area<Float256>.FromSquareQuectometers(Float256.Parse(value));
        Assert.Equal(Float256.Parse(expected), a.SquareQuectoMeters);
    }

    [Theory(DisplayName = "Area.FromSquareRontometers should produce the expected SquareQuectoMeters")]
    [InlineData("0", "0")]
    [InlineData("1", "1e6")]
    [InlineData("2.5", "2.5e6")]
    public void AreaFromSquareRontometersShouldProduceExpectedSquareQuectoMeters(string value, string expected)
    {
        Area<Float256> a = Area<Float256>.FromSquareRontometers(Float256.Parse(value));
        Assert.Equal(Float256.Parse(expected), a.SquareQuectoMeters);
    }

    [Theory(DisplayName = "Area.FromSquareYoctometers should produce the expected SquareQuectoMeters")]
    [InlineData("0", "0")]
    [InlineData("1", "1e12")]
    [InlineData("2.5", "2.5e12")]
    public void AreaFromSquareYoctometersShouldProduceExpectedSquareQuectoMeters(string value, string expected)
    {
        Area<Float256> a = Area<Float256>.FromSquareYoctometers(Float256.Parse(value));
        Assert.Equal(Float256.Parse(expected), a.SquareQuectoMeters);
    }

    [Theory(DisplayName = "Area.FromSquareZeptometers should produce the expected SquareQuectoMeters")]
    [InlineData("0", "0")]
    [InlineData("1", "1e18")]
    [InlineData("2.5", "2.5e18")]
    public void AreaFromSquareZeptometersShouldProduceExpectedSquareQuectoMeters(string value, string expected)
    {
        Area<Float256> a = Area<Float256>.FromSquareZeptometers(Float256.Parse(value));
        Assert.Equal(Float256.Parse(expected), a.SquareQuectoMeters);
    }

    [Theory(DisplayName = "Area.FromSquareAttometers should produce the expected SquareQuectoMeters")]
    [InlineData("0", "0")]
    [InlineData("1", "1e24")]
    [InlineData("2.5", "2.5e24")]
    public void AreaFromSquareAttometersShouldProduceExpectedSquareQuectoMeters(string value, string expected)
    {
        Area<Float256> a = Area<Float256>.FromSquareAttometers(Float256.Parse(value));
        Assert.Equal(Float256.Parse(expected), a.SquareQuectoMeters);
    }

    [Theory(DisplayName = "Area.FromSquareFemtometers should produce the expected SquareQuectoMeters")]
    [InlineData("0", "0")]
    [InlineData("1", "1e30")]
    [InlineData("2.5", "2.5e30")]
    public void AreaFromSquareFemtometersShouldProduceExpectedSquareQuectoMeters(string value, string expected)
    {
        Area<Float256> a = Area<Float256>.FromSquareFemtometers(Float256.Parse(value));
        Assert.Equal(Float256.Parse(expected), a.SquareQuectoMeters);
    }

    [Theory(DisplayName = "Area.FromSquarePicometers should produce the expected SquareQuectoMeters")]
    [InlineData("0", "0")]
    [InlineData("1", "1e36")]
    [InlineData("2.5", "2.5e36")]
    public void AreaFromSquarePicometersShouldProduceExpectedSquareQuectoMeters(string value, string expected)
    {
        Area<Float256> a = Area<Float256>.FromSquarePicometers(Float256.Parse(value));
        Assert.Equal(Float256.Parse(expected), a.SquareQuectoMeters);
    }

    [Theory(DisplayName = "Area.FromSquareNanometers should produce the expected SquareQuectoMeters")]
    [InlineData("0", "0")]
    [InlineData("1", "1e42")]
    [InlineData("2.5", "2.5e42")]
    public void AreaFromSquareNanometersShouldProduceExpectedSquareQuectoMeters(string value, string expected)
    {
        Area<Float256> a = Area<Float256>.FromSquareNanometers(Float256.Parse(value));
        Assert.Equal(Float256.Parse(expected), a.SquareQuectoMeters);
    }

    [Theory(DisplayName = "Area.FromSquareMicrometers should produce the expected SquareQuectoMeters")]
    [InlineData("0", "0")]
    [InlineData("1", "1e48")]
    [InlineData("2.5", "2.5e48")]
    public void AreaFromSquareMicrometersShouldProduceExpectedSquareQuectoMeters(string value, string expected)
    {
        Area<Float256> a = Area<Float256>.FromSquareMicrometers(Float256.Parse(value));
        Assert.Equal(Float256.Parse(expected), a.SquareQuectoMeters);
    }

    [Theory(DisplayName = "Area.FromSquareMillimeters should produce the expected SquareQuectoMeters")]
    [InlineData("0", "0")]
    [InlineData("1", "1e54")]
    [InlineData("2.5", "2.5e54")]
    public void AreaFromSquareMillimetersShouldProduceExpectedSquareQuectoMeters(string value, string expected)
    {
        Area<Float256> a = Area<Float256>.FromSquareMillimeters(Float256.Parse(value));
        Assert.Equal(Float256.Parse(expected), a.SquareQuectoMeters);
    }

    [Theory(DisplayName = "Area.FromSquareCentimeters should produce the expected SquareQuectoMeters")]
    [InlineData("0", "0")]
    [InlineData("1", "1e56")]
    [InlineData("2.5", "2.5e56")]
    public void AreaFromSquareCentimetersShouldProduceExpectedSquareQuectoMeters(string value, string expected)
    {
        Area<Float256> a = Area<Float256>.FromSquareCentimeters(Float256.Parse(value));
        Assert.Equal(Float256.Parse(expected), a.SquareQuectoMeters);
    }

    [Theory(DisplayName = "Area.FromSquareDecimeters should produce the expected SquareQuectoMeters")]
    [InlineData("0", "0")]
    [InlineData("1", "1e58")]
    [InlineData("2.5", "2.5e58")]
    public void AreaFromSquareDecimetersShouldProduceExpectedSquareQuectoMeters(string value, string expected)
    {
        Area<Float256> a = Area<Float256>.FromSquareDecimeters(Float256.Parse(value));
        Assert.Equal(Float256.Parse(expected), a.SquareQuectoMeters);
    }

    [Theory(DisplayName = "Area.FromSquareMeters should produce the expected SquareQuectoMeters")]
    [InlineData("0", "0")]
    [InlineData("1", "1e60")]
    [InlineData("2.5", "2.5e60")]
    public void AreaFromSquareMetersShouldProduceExpectedSquareQuectoMeters(string value, string expected)
    {
        Area<Float256> a = Area<Float256>.FromSquareMeters(Float256.Parse(value));
        Assert.Equal(Float256.Parse(expected), a.SquareQuectoMeters);
    }

    [Theory(DisplayName = "Area.FromSquareDecameters should produce the expected SquareQuectoMeters")]
    [InlineData("0", "0")]
    [InlineData("1", "1e62")]
    [InlineData("2.5", "2.5e62")]
    public void AreaFromSquareDecametersShouldProduceExpectedSquareQuectoMeters(string value, string expected)
    {
        Area<Float256> a = Area<Float256>.FromSquareDecameters(Float256.Parse(value));
        Assert.Equal(Float256.Parse(expected), a.SquareQuectoMeters);
    }

    [Theory(DisplayName = "Area.FromSquareHectometers should produce the expected SquareQuectoMeters")]
    [InlineData("0", "0")]
    [InlineData("1", "1e64")]
    [InlineData("2.5", "2.5e64")]
    public void AreaFromSquareHectometersShouldProduceExpectedSquareQuectoMeters(string value, string expected)
    {
        Area<Float256> a = Area<Float256>.FromSquareHectometers(Float256.Parse(value));
        Assert.Equal(Float256.Parse(expected), a.SquareQuectoMeters);
    }

    [Theory(DisplayName = "Area.FromSquareKilometers should produce the expected SquareQuectoMeters")]
    [InlineData("0", "0")]
    [InlineData("1", "1e66")]
    [InlineData("2.5", "2.5e66")]
    public void AreaFromSquareKilometersShouldProduceExpectedSquareQuectoMeters(string value, string expected)
    {
        Area<Float256> a = Area<Float256>.FromSquareKilometers(Float256.Parse(value));
        Assert.Equal(Float256.Parse(expected), a.SquareQuectoMeters);
    }

    [Theory(DisplayName = "Area.FromSquareMegameters should produce the expected SquareQuectoMeters")]
    [InlineData("0", "0")]
    [InlineData("1", "1e72")]
    [InlineData("2.5", "2.5e72")]
    public void AreaFromSquareMegametersShouldProduceExpectedSquareQuectoMeters(string value, string expected)
    {
        Area<Float256> a = Area<Float256>.FromSquareMegameters(Float256.Parse(value));
        Assert.Equal(Float256.Parse(expected), a.SquareQuectoMeters);
    }

    [Theory(DisplayName = "Area.FromSquareGigameters should produce the expected SquareQuectoMeters")]
    [InlineData("0", "0")]
    [InlineData("1", "1e78")]
    [InlineData("2.5", "2.5e78")]
    public void AreaFromSquareGigametersShouldProduceExpectedSquareQuectoMeters(string value, string expected)
    {
        Area<Float256> a = Area<Float256>.FromSquareGigameters(Float256.Parse(value));
        Assert.Equal(Float256.Parse(expected), a.SquareQuectoMeters);
    }

    [Theory(DisplayName = "Area.FromSquareTerameters should produce the expected SquareQuectoMeters")]
    [InlineData("0", "0")]
    [InlineData("1", "1e84")]
    [InlineData("2.5", "2.5e84")]
    public void AreaFromSquareTerametersShouldProduceExpectedSquareQuectoMeters(string value, string expected)
    {
        Area<Float256> a = Area<Float256>.FromSquareTerameters(Float256.Parse(value));
        Assert.Equal(Float256.Parse(expected), a.SquareQuectoMeters);
    }

    [Theory(DisplayName = "Area.FromSquarePetameters should produce the expected SquareQuectoMeters")]
    [InlineData("0", "0")]
    [InlineData("1", "1e90")]
    [InlineData("2.5", "2.5e90")]
    public void AreaFromSquarePetametersShouldProduceExpectedSquareQuectoMeters(string value, string expected)
    {
        Area<Float256> a = Area<Float256>.FromSquarePetameters(Float256.Parse(value));
        Assert.Equal(Float256.Parse(expected), a.SquareQuectoMeters);
    }

    [Theory(DisplayName = "Area.FromSquareExameters should produce the expected SquareQuectoMeters")]
    [InlineData("0", "0")]
    [InlineData("1", "1e96")]
    [InlineData("2.5", "2.5e96")]
    public void AreaFromSquareExametersShouldProduceExpectedSquareQuectoMeters(string value, string expected)
    {
        Area<Float256> a = Area<Float256>.FromSquareExameters(Float256.Parse(value));
        Assert.Equal(Float256.Parse(expected), a.SquareQuectoMeters);
    }

    [Theory(DisplayName = "Area.FromSquareZettameters should produce the expected SquareQuectoMeters")]
    [InlineData("0", "0")]
    [InlineData("1", "1e102")]
    [InlineData("2.5", "2.5e102")]
    public void AreaFromSquareZettametersShouldProduceExpectedSquareQuectoMeters(string value, string expected)
    {
        Area<Float256> a = Area<Float256>.FromSquareZettameters(Float256.Parse(value));
        Assert.Equal(Float256.Parse(expected), a.SquareQuectoMeters);
    }

    [Theory(DisplayName = "Area.FromSquareYottameters should produce the expected SquareQuectoMeters")]
    [InlineData("0", "0")]
    [InlineData("1", "1e108")]
    [InlineData("2.5", "2.5e108")]
    public void AreaFromSquareYottametersShouldProduceExpectedSquareQuectoMeters(string value, string expected)
    {
        Area<Float256> a = Area<Float256>.FromSquareYottameters(Float256.Parse(value));
        Assert.Equal(Float256.Parse(expected), a.SquareQuectoMeters);
    }

    [Theory(DisplayName = "Area.FromSquareRonnameters should produce the expected SquareQuectoMeters")]
    [InlineData("0", "0")]
    [InlineData("1", "1e114")]
    [InlineData("2.5", "2.5e114")]
    public void AreaFromSquareRonnametersShouldProduceExpectedSquareQuectoMeters(string value, string expected)
    {
        Area<Float256> a = Area<Float256>.FromSquareRonnameters(Float256.Parse(value));
        Assert.Equal(Float256.Parse(expected), a.SquareQuectoMeters);
    }

    [Theory(DisplayName = "Area.FromSquareQuettameters should produce the expected SquareQuectoMeters")]
    [InlineData("0", "0")]
    [InlineData("1", "1e120")]
    [InlineData("2.5", "2.5e120")]
    public void AreaFromSquareQuettametersShouldProduceExpectedSquareQuectoMeters(string value, string expected)
    {
        Area<Float256> a = Area<Float256>.FromSquareQuettameters(Float256.Parse(value));
        Assert.Equal(Float256.Parse(expected), a.SquareQuectoMeters);
    }

    [Theory(DisplayName = "Area.FromSquareInches should produce the expected SquareQuectoMeters")]
    [InlineData("0", "0")]
    [InlineData("1", "6.4516e56")]
    [InlineData("2.5", "1.6129e57")]
    public void AreaFromSquareInchesShouldProduceExpectedSquareQuectoMeters(string value, string expected)
    {
        Area<Float256> a = Area<Float256>.FromSquareInches(Float256.Parse(value));
        Assert.Equal(Float256.Parse(expected), a.SquareQuectoMeters);
    }

    [Theory(DisplayName = "Area.FromSquareFeet should produce the expected SquareQuectoMeters")]
    [InlineData("0", "0")]
    [InlineData("1", "9.290304e58")]
    [InlineData("2.5", "2.322576e59")]
    public void AreaFromSquareFeetShouldProduceExpectedSquareQuectoMeters(string value, string expected)
    {
        Area<Float256> a = Area<Float256>.FromSquareFeet(Float256.Parse(value));
        Assert.Equal(Float256.Parse(expected), a.SquareQuectoMeters);
    }

    [Theory(DisplayName = "Area.FromSquareYards should produce the expected SquareQuectoMeters")]
    [InlineData("0", "0")]
    [InlineData("1", "8.3612736e59")]
    [InlineData("2.5", "2.0903184e60")]
    public void AreaFromSquareYardsShouldProduceExpectedSquareQuectoMeters(string value, string expected)
    {
        Area<Float256> a = Area<Float256>.FromSquareYards(Float256.Parse(value));
        Assert.Equal(Float256.Parse(expected), a.SquareQuectoMeters);
    }

    [Theory(DisplayName = "Area.FromSquareMiles should produce the expected SquareQuectoMeters")]
    [InlineData("0", "0")]
    [InlineData("1", "2.589988110336e66")]
    [InlineData("2.5", "6.47497027584e66")]
    public void AreaFromSquareMilesShouldProduceExpectedSquareQuectoMeters(string value, string expected)
    {
        Area<Float256> a = Area<Float256>.FromSquareMiles(Float256.Parse(value));
        Assert.Equal(Float256.Parse(expected), a.SquareQuectoMeters);
    }

    [Theory(DisplayName = "Area.FromSquareNauticalMiles should produce the expected SquareQuectoMeters")]
    [InlineData("0", "0")]
    [InlineData("1", "3.429904e66")]
    [InlineData("2.5", "8.57476e66")]
    public void AreaFromSquareNauticalMilesShouldProduceExpectedSquareQuectoMeters(string value, string expected)
    {
        Area<Float256> a = Area<Float256>.FromSquareNauticalMiles(Float256.Parse(value));
        Assert.Equal(Float256.Parse(expected), a.SquareQuectoMeters);
    }

    [Theory(DisplayName = "Area.FromSquareFermis should produce the expected SquareQuectoMeters")]
    [InlineData("0", "0")]
    [InlineData("1", "1e30")]
    [InlineData("2.5", "2.5e30")]
    public void AreaFromSquareFermisShouldProduceExpectedSquareQuectoMeters(string value, string expected)
    {
        Area<Float256> a = Area<Float256>.FromSquareFermis(Float256.Parse(value));
        Assert.Equal(Float256.Parse(expected), a.SquareQuectoMeters);
    }

    [Theory(DisplayName = "Area.FromSquareAngstroms should produce the expected SquareQuectoMeters")]
    [InlineData("0", "0")]
    [InlineData("1", "1e40")]
    [InlineData("2.5", "2.5e40")]
    public void AreaFromSquareAngstromsShouldProduceExpectedSquareQuectoMeters(string value, string expected)
    {
        Area<Float256> a = Area<Float256>.FromSquareAngstroms(Float256.Parse(value));
        Assert.Equal(Float256.Parse(expected), a.SquareQuectoMeters);
    }

    [Theory(DisplayName = "Area.FromSquareAstronomicalUnits should produce the expected SquareQuectoMeters")]
    [InlineData("0")]
    [InlineData("1")]
    [InlineData("2.5")]
    public void AreaFromSquareAstronomicalUnitsShouldProduceExpectedSquareQuectoMeters(string value)
    {
        // Compute expected at Float256 precision via the same chain as the unit: AU² × 10^60.
        Float256 input = Float256.Parse(value);
        Float256 auMeters = 149_597_870_700L;
        Float256 expected = input * auMeters * auMeters * UnitMath.Pow10<Float256>(60);

        Area<Float256> a = Area<Float256>.FromSquareAstronomicalUnits(input);

        Assert.Equal(expected, a.SquareQuectoMeters);
    }

    [Theory(DisplayName = "Area.FromSquareLightYears should produce the expected SquareQuectoMeters")]
    [InlineData("0")]
    [InlineData("1")]
    [InlineData("2.5")]
    public void AreaFromSquareLightYearsShouldProduceExpectedSquareQuectoMeters(string value)
    {
        // Compute expected at Float256 precision via the same chain as the unit: LY² × 10^60.
        Float256 input = Float256.Parse(value);
        Float256 lyMeters = 9_460_730_472_580_800L;
        Float256 expected = input * lyMeters * lyMeters * UnitMath.Pow10<Float256>(60);

        Area<Float256> a = Area<Float256>.FromSquareLightYears(input);

        Assert.Equal(expected, a.SquareQuectoMeters);
    }

    [Theory(DisplayName = "Area.FromSquareParsecs should produce the expected SquareQuectoMeters")]
    [InlineData("0")]
    [InlineData("1")]
    [InlineData("2.5")]
    public void AreaFromSquareParsecsShouldProduceExpectedSquareQuectoMeters(string value)
    {
        // Compute expected via the IAU definition: 1 pc = (648000 / π) AU. Match the unit's
        // associativity exactly — group as `(metersPerParsec × metersPerParsec × Pow10(60))`
        // first (matching the cached SqQuectometersPerSquareParsec constant), then multiply by
        // input. π is irrational, so the division rounds and grouping changes the LSB.
        Float256 input = Float256.Parse(value);
        Float256 metersPerParsec = (Float256)149_597_870_700L * 648000 / Float256.Pi;
        Float256 sqQmPerSqParsec = metersPerParsec * metersPerParsec * UnitMath.Pow10<Float256>(60);
        Float256 expected = input * sqQmPerSqParsec;

        Area<Float256> a = Area<Float256>.FromSquareParsecs(input);

        Assert.Equal(expected, a.SquareQuectoMeters);
    }

    [Fact(DisplayName = "Area.Add should produce the expected result")]
    public void AreaAddShouldProduceExpectedValue()
    {
        // Given
        Area<Float256> left = Area<Float256>.FromSquareMeters(Float256.Parse("1500"));
        Area<Float256> right = Area<Float256>.FromSquareMeters(Float256.Parse("500"));

        // When
        Area<Float256> result = left.Add(right);

        // Then
        Assert.Equal(Float256.Parse("2000"), result.SquareMeters);
    }

    [Fact(DisplayName = "Area.Subtract should produce the expected result")]
    public void AreaSubtractShouldProduceExpectedValue()
    {
        // Given
        Area<Float256> left = Area<Float256>.FromSquareMeters(Float256.Parse("1500"));
        Area<Float256> right = Area<Float256>.FromSquareMeters(Float256.Parse("400"));

        // When
        Area<Float256> result = left.Subtract(right);

        // Then
        Assert.Equal(Float256.Parse("1100"), result.SquareMeters);
    }

    [Fact(DisplayName = "Area comparison should produce the expected result (left equal to right)")]
    public void AreaComparisonShouldProduceExpectedResultLeftEqualToRight()
    {
        // Given
        Area<Float256> left = Area<Float256>.FromSquareMeters(Float256.Parse("1234"));
        Area<Float256> right = Area<Float256>.FromSquareMeters(Float256.Parse("1234"));

        // When / Then
        Assert.Equal(0, Area<Float256>.Compare(left, right));
        Assert.Equal(0, left.CompareTo(right));
        Assert.Equal(0, left.CompareTo((object)right));
        Assert.False(left > right);
        Assert.True(left >= right);
        Assert.False(left < right);
        Assert.True(left <= right);
    }

    [Fact(DisplayName = "Area comparison should produce the expected result (left greater than right)")]
    public void AreaComparisonShouldProduceExpectedLeftGreaterThanRight()
    {
        // Given
        Area<Float256> left = Area<Float256>.FromSquareMeters(Float256.Parse("4567"));
        Area<Float256> right = Area<Float256>.FromSquareMeters(Float256.Parse("1234"));

        // When / Then
        Assert.Equal(1, Area<Float256>.Compare(left, right));
        Assert.Equal(1, left.CompareTo(right));
        Assert.Equal(1, left.CompareTo((object)right));
        Assert.True(left > right);
        Assert.True(left >= right);
        Assert.False(left < right);
        Assert.False(left <= right);
    }

    [Fact(DisplayName = "Area comparison should produce the expected result (left less than right)")]
    public void AreaComparisonShouldProduceExpectedLeftLessThanRight()
    {
        // Given
        Area<Float256> left = Area<Float256>.FromSquareMeters(Float256.Parse("1234"));
        Area<Float256> right = Area<Float256>.FromSquareMeters(Float256.Parse("4567"));

        // When / Then
        Assert.Equal(-1, Area<Float256>.Compare(left, right));
        Assert.Equal(-1, left.CompareTo(right));
        Assert.Equal(-1, left.CompareTo((object)right));
        Assert.False(left > right);
        Assert.False(left >= right);
        Assert.True(left < right);
        Assert.True(left <= right);
    }

    [Fact(DisplayName = "Area equality should produce the expected result (left equal to right)")]
    public void AreaEqualityShouldProduceExpectedResultLeftEqualToRight()
    {
        // Given — 2 km² and 2 000 000 m² reduce to the same canonical at Float256.
        Area<Float256> left = Area<Float256>.FromSquareKilometers(Float256.Parse("2"));
        Area<Float256> right = Area<Float256>.FromSquareMeters(Float256.Parse("2000000"));

        // When / Then
        Assert.True(Area<Float256>.Equals(left, right));
        Assert.True(left.Equals(right));
        Assert.True(left.Equals((object)right));
        Assert.True(left == right);
        Assert.False(left != right);
    }

    [Fact(DisplayName = "Area equality should produce the expected result (left not equal to right)")]
    public void AreaEqualityShouldProduceExpectedResultLeftNotEqualToRight()
    {
        // Given
        Area<Float256> left = Area<Float256>.FromSquareKilometers(Float256.Parse("2"));
        Area<Float256> right = Area<Float256>.FromSquareMeters(Float256.Parse("2500000"));

        // When / Then
        Assert.False(Area<Float256>.Equals(left, right));
        Assert.False(left.Equals(right));
        Assert.False(left.Equals((object)right));
        Assert.False(left == right);
        Assert.True(left != right);
    }

    [Fact(DisplayName = "Area.ToString should produce the expected result")]
    public void AreaToStringShouldProduceExpectedResult()
    {
        // Given
        Area<Float256> a = Area<Float256>.FromSquareMeters(Float256.Parse("1000000"));

        // When / Then
        Assert.Equal("1,000,000.000 m²", $"{a:sqm3}");
        Assert.Equal("1.000 km²", $"{a:sqkm3}");
        Assert.Equal("100.000 hm²", $"{a:sqhm3}");
        Assert.Equal("10,000.000 dam²", $"{a:sqdam3}");
        Assert.Equal("10,000,000,000.000 cm²", $"{a:sqcm3}");
        Assert.Equal("1,550,003,100.006 in²", $"{a:sqin3}");
        Assert.Equal("10,763,910.417 ft²", $"{a:sqft3}");
        Assert.Equal("1,195,990.046 yd²", $"{a:sqyd3}");
    }

    [Fact(DisplayName = "Area.ToString should honor custom culture separators")]
    public void AreaToStringShouldHonorCustomCulture()
    {
        // Given
        CultureInfo customCulture = new("de-DE");
        Area<Float256> a = Area<Float256>.FromSquareMeters(Float256.Parse("1234.56"));

        // When
        string formatted = a.ToString("sqm2", customCulture);

        // Then
        Assert.Equal("1.234,56 m²", formatted);
    }

    [Fact(DisplayName = "Area property conversions should be consistent")]
    public void AreaPropertyConversionsShouldBeConsistent()
    {
        // Given
        Area<Float256> a = Area<Float256>.FromSquareMeters(Float256.Parse("1"));

        // Then — verify SI unit conversions are consistent
        Assert.Equal(Float256.Parse("1"), a.SquareMeters);
        Assert.Equal(Float256.Parse("1e60"), a.SquareQuectoMeters);
        Assert.Equal(Float256.Parse("1e-6"), a.SquareKiloMeters);
        Assert.Equal(Float256.Parse("100"), a.SquareDeciMeters);
        Assert.Equal(Float256.Parse("10000"), a.SquareCentiMeters);
        Assert.Equal(Float256.Parse("1000000"), a.SquareMilliMeters);
    }

    [Fact(DisplayName = "Area imperial unit conversions should be accurate")]
    public void AreaImperialUnitConversionsShouldBeAccurate()
    {
        // Given — 1 square meter
        Area<Float256> a = Area<Float256>.FromSquareMeters(Float256.Parse("1"));

        // Then — 1 m² in imperial units; compute expected via Float256 (same chain as the unit).
        // 1 m² = 10^60 sqqm. m²/in² = 10^60 / (64516 × 10^52) = 10^8 / 64516.
        Float256 sqQm = UnitMath.Pow10<Float256>(60);
        Assert.Equal(sqQm / ((Float256)64516 * UnitMath.Pow10<Float256>(52)), a.SquareInches);
        Assert.Equal(sqQm / ((Float256)9290304 * UnitMath.Pow10<Float256>(52)), a.SquareFeet);
        Assert.Equal(sqQm / ((Float256)83612736 * UnitMath.Pow10<Float256>(52)), a.SquareYards);
    }

    [Fact(DisplayName = "Area round-trip conversions should be accurate")]
    public void AreaRoundTripConversionsShouldBeAccurate()
    {
        // Given
        Float256 originalValue = Float256.Parse("123.456");

        // When — convert from square meters and back
        Area<Float256> a = Area<Float256>.FromSquareMeters(originalValue);

        // Then
        Assert.Equal(originalValue, a.SquareMeters);
    }

    [Fact(DisplayName = "Area from square kilometers to square meters should be accurate")]
    public void AreaFromSquareKilometersToSquareMetersShouldBeAccurate()
    {
        // Given
        Area<Float256> a = Area<Float256>.FromSquareKilometers(Float256.Parse("1"));

        // Then
        Assert.Equal(Float256.Parse("1000000"), a.SquareMeters);
    }

    [Fact(DisplayName = "Area from square miles to square kilometers should be accurate")]
    public void AreaFromSquareMilesToSquareKilometersShouldBeAccurate()
    {
        // Given
        Area<Float256> a = Area<Float256>.FromSquareMiles(Float256.Parse("1"));

        // Then — 1 square mile = exactly 2.589988110336 square kilometers
        Assert.Equal(Float256.Parse("2.589988110336"), a.SquareKiloMeters);
    }

    [Fact(DisplayName = "Area GetHashCode should be consistent for equal values")]
    public void AreaGetHashCodeShouldBeConsistentForEqualValues()
    {
        // Given
        Area<Float256> a1 = Area<Float256>.FromSquareMeters(Float256.Parse("100"));
        Area<Float256> a2 = Area<Float256>.FromSquareMeters(Float256.Parse("100"));

        // Then
        Assert.Equal(a1.GetHashCode(), a2.GetHashCode());
    }

    [Fact(DisplayName = "Area.Equals with null object should return false")]
    public void AreaEqualsWithNullObjectShouldReturnFalse()
    {
        // Given
        Area<Float256> a = Area<Float256>.FromSquareMeters(Float256.Parse("100"));

        // Then
        Assert.False(a.Equals(null));
    }

    [Fact(DisplayName = "Area.Equals with different type should return false")]
    public void AreaEqualsWithDifferentTypeShouldReturnFalse()
    {
        // Given
        Area<Float256> a = Area<Float256>.FromSquareMeters(Float256.Parse("100"));

        // Then
        Assert.False(a.Equals("not an area"));
    }

    [Fact(DisplayName = "Area default ToString should use SquareQuectoMeters")]
    public void AreaDefaultToStringShouldUseSquareQuectoMeters()
    {
        // Given
        Area<Float256> a = Area<Float256>.FromSquareQuectometers(Float256.Parse("123"));

        // When
        string result = a.ToString();

        // Then
        Assert.Contains("qm²", result);
    }

    [Theory(DisplayName = "Area.ValueOf should return the value at the matching scale")]
    [InlineData("sqqm")]
    [InlineData("sqrm")]
    [InlineData("sqym")]
    [InlineData("sqzm")]
    [InlineData("sqam")]
    [InlineData("sqfm")]
    [InlineData("sqpm")]
    [InlineData("sqnm")]
    [InlineData("squm")]
    [InlineData("sqmm")]
    [InlineData("sqcm")]
    [InlineData("sqdm")]
    [InlineData("sqm")]
    [InlineData("sqdam")]
    [InlineData("sqhm")]
    [InlineData("sqkm")]
    [InlineData("sqMm")]
    [InlineData("sqGm")]
    [InlineData("sqTm")]
    [InlineData("sqPm")]
    [InlineData("sqEm")]
    [InlineData("sqZm")]
    [InlineData("sqYm")]
    [InlineData("sqRm")]
    [InlineData("sqQm")]
    [InlineData("sqin")]
    [InlineData("sqft")]
    [InlineData("sqyd")]
    [InlineData("sqmi")]
    [InlineData("sqnmi")]
    [InlineData("sqfmi")]
    [InlineData("sqa")]
    [InlineData("sqau")]
    [InlineData("sqly")]
    [InlineData("sqpc")]
    public void AreaValueOfShouldReturnValueAtMatchingScale(string specifier)
    {
        // Given
        Area<Float256> a = Area<Float256>.FromSquareMeters(Float256.Parse("1234.567"));

        // When
        Float256 expected = specifier switch
        {
            "sqqm" => a.SquareQuectoMeters,
            "sqrm" => a.SquareRontoMeters,
            "sqym" => a.SquareYoctoMeters,
            "sqzm" => a.SquareZeptoMeters,
            "sqam" => a.SquareAttoMeters,
            "sqfm" => a.SquareFemtoMeters,
            "sqpm" => a.SquarePicoMeters,
            "sqnm" => a.SquareNanoMeters,
            "squm" => a.SquareMicroMeters,
            "sqmm" => a.SquareMilliMeters,
            "sqcm" => a.SquareCentiMeters,
            "sqdm" => a.SquareDeciMeters,
            "sqm" => a.SquareMeters,
            "sqdam" => a.SquareDecaMeters,
            "sqhm" => a.SquareHectoMeters,
            "sqkm" => a.SquareKiloMeters,
            "sqMm" => a.SquareMegaMeters,
            "sqGm" => a.SquareGigaMeters,
            "sqTm" => a.SquareTeraMeters,
            "sqPm" => a.SquarePetaMeters,
            "sqEm" => a.SquareExaMeters,
            "sqZm" => a.SquareZettaMeters,
            "sqYm" => a.SquareYottaMeters,
            "sqRm" => a.SquareRonnaMeters,
            "sqQm" => a.SquareQuettaMeters,
            "sqin" => a.SquareInches,
            "sqft" => a.SquareFeet,
            "sqyd" => a.SquareYards,
            "sqmi" => a.SquareMiles,
            "sqnmi" => a.SquareNauticalMiles,
            "sqfmi" => a.SquareFermis,
            "sqa" => a.SquareAngstroms,
            "sqau" => a.SquareAstronomicalUnits,
            "sqly" => a.SquareLightYears,
            "sqpc" => a.SquareParsecs,
            _ => throw new InvalidOperationException($"Unhandled specifier: {specifier}")
        };

        // Then
        Assert.Equal(expected, a.ValueOf(specifier));
    }

    [Fact(DisplayName = "Area.ValueOf should throw on invalid specifier")]
    public void AreaValueOfShouldThrowOnInvalidSpecifier()
    {
        // Given
        Area<Float256> a = Area<Float256>.FromSquareMeters(Float256.Parse("1"));

        // Then
        Assert.Throws<ArgumentException>(() => a.ValueOf("xx"));
    }
}
