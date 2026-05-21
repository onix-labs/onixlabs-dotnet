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

public sealed class DistanceTests
{
    [Fact(DisplayName = "Distance.Zero should produce the expected result")]
    public void DistanceZeroShouldProduceExpectedResult()
    {
        // Given / When
        Distance<Float128> distance = Distance<Float128>.Zero;

        // Then
        Assert.Equal(Float128.Zero, distance.QuectoMeters);
    }

    [Theory(DisplayName = "Distance.FromQuectometers should produce the expected QuectoMeters")]
    [InlineData("0", "0")]
    [InlineData("1", "1")]
    [InlineData("2.5", "2.5")]
    public void DistanceFromQuectometersShouldProduceExpectedQuectoMeters(string value, string expectedQuectometers)
    {
        Distance<Float128> d = Distance<Float128>.FromQuectometers(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expectedQuectometers), d.QuectoMeters);
    }

    [Theory(DisplayName = "Distance.FromRontometers should produce the expected QuectoMeters")]
    [InlineData("0", "0")]
    [InlineData("1", "1e3")]
    [InlineData("2.5", "2.5e3")]
    public void DistanceFromRontometersShouldProduceExpectedQuectoMeters(string value, string expectedQuectometers)
    {
        Distance<Float128> d = Distance<Float128>.FromRontometers(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expectedQuectometers), d.QuectoMeters);
    }

    [Theory(DisplayName = "Distance.FromYoctometers should produce the expected QuectoMeters")]
    [InlineData("0", "0")]
    [InlineData("1", "1e6")]
    [InlineData("2.5", "2.5e6")]
    public void DistanceFromYoctometersShouldProduceExpectedQuectoMeters(string value, string expectedQuectometers)
    {
        Distance<Float128> d = Distance<Float128>.FromYoctometers(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expectedQuectometers), d.QuectoMeters);
    }

    [Theory(DisplayName = "Distance.FromZeptometers should produce the expected QuectoMeters")]
    [InlineData("0", "0")]
    [InlineData("1", "1e9")]
    [InlineData("2.5", "2.5e9")]
    public void DistanceFromZeptometersShouldProduceExpectedQuectoMeters(string value, string expectedQuectometers)
    {
        Distance<Float128> d = Distance<Float128>.FromZeptometers(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expectedQuectometers), d.QuectoMeters);
    }

    [Theory(DisplayName = "Distance.FromAttometers should produce the expected QuectoMeters")]
    [InlineData("0", "0")]
    [InlineData("1", "1e12")]
    [InlineData("2.5", "2.5e12")]
    public void DistanceFromAttometersShouldProduceExpectedQuectoMeters(string value, string expectedQuectometers)
    {
        Distance<Float128> d = Distance<Float128>.FromAttometers(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expectedQuectometers), d.QuectoMeters);
    }

    [Theory(DisplayName = "Distance.FromFemtometers should produce the expected QuectoMeters")]
    [InlineData("0", "0")]
    [InlineData("1", "1e15")]
    [InlineData("2.5", "2.5e15")]
    public void DistanceFromFemtometersShouldProduceExpectedQuectoMeters(string value, string expectedQuectometers)
    {
        Distance<Float128> d = Distance<Float128>.FromFemtometers(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expectedQuectometers), d.QuectoMeters);
    }

    [Theory(DisplayName = "Distance.FromPicometers should produce the expected QuectoMeters")]
    [InlineData("0", "0")]
    [InlineData("1", "1e18")]
    [InlineData("2.5", "2.5e18")]
    public void DistanceFromPicometersShouldProduceExpectedQuectoMeters(string value, string expectedQuectometers)
    {
        Distance<Float128> d = Distance<Float128>.FromPicometers(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expectedQuectometers), d.QuectoMeters);
    }

    [Theory(DisplayName = "Distance.FromNanometers should produce the expected QuectoMeters")]
    [InlineData("0", "0")]
    [InlineData("1", "1e21")]
    [InlineData("2.5", "2.5e21")]
    public void DistanceFromNanometersShouldProduceExpectedQuectoMeters(string value, string expectedQuectometers)
    {
        Distance<Float128> d = Distance<Float128>.FromNanometers(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expectedQuectometers), d.QuectoMeters);
    }

    [Theory(DisplayName = "Distance.FromMicrometers should produce the expected QuectoMeters")]
    [InlineData("0", "0")]
    [InlineData("1", "1e24")]
    [InlineData("2.5", "2.5e24")]
    public void DistanceFromMicrometersShouldProduceExpectedQuectoMeters(string value, string expectedQuectometers)
    {
        Distance<Float128> d = Distance<Float128>.FromMicrometers(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expectedQuectometers), d.QuectoMeters);
    }

    [Theory(DisplayName = "Distance.FromMillimeters should produce the expected QuectoMeters")]
    [InlineData("0", "0")]
    [InlineData("1", "1e27")]
    [InlineData("2.5", "2.5e27")]
    public void DistanceFromMillimetersShouldProduceExpectedQuectoMeters(string value, string expectedQuectometers)
    {
        Distance<Float128> d = Distance<Float128>.FromMillimeters(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expectedQuectometers), d.QuectoMeters);
    }

    [Theory(DisplayName = "Distance.FromCentimeters should produce the expected QuectoMeters")]
    [InlineData("0", "0")]
    [InlineData("1", "1e28")]
    [InlineData("2.5", "2.5e28")]
    public void DistanceFromCentimetersShouldProduceExpectedQuectoMeters(string value, string expectedQuectometers)
    {
        Distance<Float128> d = Distance<Float128>.FromCentimeters(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expectedQuectometers), d.QuectoMeters);
    }

    [Theory(DisplayName = "Distance.FromDecimeters should produce the expected QuectoMeters")]
    [InlineData("0", "0")]
    [InlineData("1", "1e29")]
    [InlineData("2.5", "2.5e29")]
    public void DistanceFromDecimetersShouldProduceExpectedQuectoMeters(string value, string expectedQuectometers)
    {
        Distance<Float128> d = Distance<Float128>.FromDecimeters(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expectedQuectometers), d.QuectoMeters);
    }

    [Theory(DisplayName = "Distance.FromMeters should produce the expected QuectoMeters")]
    [InlineData("0", "0")]
    [InlineData("1", "1e30")]
    [InlineData("2.5", "2.5e30")]
    public void DistanceFromMetersShouldProduceExpectedQuectoMeters(string value, string expectedQuectometers)
    {
        Distance<Float128> d = Distance<Float128>.FromMeters(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expectedQuectometers), d.QuectoMeters);
    }

    [Theory(DisplayName = "Distance.FromDecameters should produce the expected QuectoMeters")]
    [InlineData("0", "0")]
    [InlineData("1", "1e31")]
    [InlineData("2.5", "2.5e31")]
    public void DistanceFromDecametersShouldProduceExpectedQuectoMeters(string value, string expectedQuectometers)
    {
        Distance<Float128> d = Distance<Float128>.FromDecameters(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expectedQuectometers), d.QuectoMeters);
    }

    [Theory(DisplayName = "Distance.FromHectometers should produce the expected QuectoMeters")]
    [InlineData("0", "0")]
    [InlineData("1", "1e32")]
    [InlineData("2.5", "2.5e32")]
    public void DistanceFromHectometersShouldProduceExpectedQuectoMeters(string value, string expectedQuectometers)
    {
        Distance<Float128> d = Distance<Float128>.FromHectometers(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expectedQuectometers), d.QuectoMeters);
    }

    [Theory(DisplayName = "Distance.FromKilometers should produce the expected QuectoMeters")]
    [InlineData("0", "0")]
    [InlineData("1", "1e33")]
    [InlineData("2.5", "2.5e33")]
    public void DistanceFromKilometersShouldProduceExpectedQuectoMeters(string value, string expectedQuectometers)
    {
        Distance<Float128> d = Distance<Float128>.FromKilometers(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expectedQuectometers), d.QuectoMeters);
    }

    [Theory(DisplayName = "Distance.FromMegameters should produce the expected QuectoMeters")]
    [InlineData("0", "0")]
    [InlineData("1", "1e36")]
    [InlineData("2.5", "2.5e36")]
    public void DistanceFromMegametersShouldProduceExpectedQuectoMeters(string value, string expectedQuectometers)
    {
        Distance<Float128> d = Distance<Float128>.FromMegameters(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expectedQuectometers), d.QuectoMeters);
    }

    [Theory(DisplayName = "Distance.FromGigameters should produce the expected QuectoMeters")]
    [InlineData("0", "0")]
    [InlineData("1", "1e39")]
    [InlineData("2.5", "2.5e39")]
    public void DistanceFromGigametersShouldProduceExpectedQuectoMeters(string value, string expectedQuectometers)
    {
        Distance<Float128> d = Distance<Float128>.FromGigameters(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expectedQuectometers), d.QuectoMeters);
    }

    [Theory(DisplayName = "Distance.FromTerameters should produce the expected QuectoMeters")]
    [InlineData("0", "0")]
    [InlineData("1", "1e42")]
    [InlineData("2.5", "2.5e42")]
    public void DistanceFromTerametersShouldProduceExpectedQuectoMeters(string value, string expectedQuectometers)
    {
        Distance<Float128> d = Distance<Float128>.FromTerameters(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expectedQuectometers), d.QuectoMeters);
    }

    [Theory(DisplayName = "Distance.FromPetameters should produce the expected QuectoMeters")]
    [InlineData("0", "0")]
    [InlineData("1", "1e45")]
    [InlineData("2.5", "2.5e45")]
    public void DistanceFromPetametersShouldProduceExpectedQuectoMeters(string value, string expectedQuectometers)
    {
        Distance<Float128> d = Distance<Float128>.FromPetameters(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expectedQuectometers), d.QuectoMeters);
    }

    [Theory(DisplayName = "Distance.FromExameters should produce the expected QuectoMeters")]
    [InlineData("0", "0")]
    [InlineData("1", "1e48")]
    [InlineData("2.5", "2.5e48")]
    public void DistanceFromExametersShouldProduceExpectedQuectoMeters(string value, string expectedQuectometers)
    {
        Distance<Float128> d = Distance<Float128>.FromExameters(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expectedQuectometers), d.QuectoMeters);
    }

    [Theory(DisplayName = "Distance.FromZettameters should produce the expected QuectoMeters")]
    [InlineData("0", "0")]
    [InlineData("1", "1e51")]
    [InlineData("2.5", "2.5e51")]
    public void DistanceFromZettametersShouldProduceExpectedQuectoMeters(string value, string expectedQuectometers)
    {
        Distance<Float128> d = Distance<Float128>.FromZettameters(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expectedQuectometers), d.QuectoMeters);
    }

    [Theory(DisplayName = "Distance.FromYottameters should produce the expected QuectoMeters")]
    [InlineData("0", "0")]
    [InlineData("1", "1e54")]
    [InlineData("2.5", "2.5e54")]
    public void DistanceFromYottametersShouldProduceExpectedQuectoMeters(string value, string expectedQuectometers)
    {
        Distance<Float128> d = Distance<Float128>.FromYottameters(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expectedQuectometers), d.QuectoMeters);
    }

    [Theory(DisplayName = "Distance.FromRonnameters should produce the expected QuectoMeters")]
    [InlineData("0", "0")]
    [InlineData("1", "1e57")]
    [InlineData("2.5", "2.5e57")]
    public void DistanceFromRonnametersShouldProduceExpectedQuectoMeters(string value, string expectedQuectometers)
    {
        Distance<Float128> d = Distance<Float128>.FromRonnameters(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expectedQuectometers), d.QuectoMeters);
    }

    [Theory(DisplayName = "Distance.FromQuettameters should produce the expected QuectoMeters")]
    [InlineData("0", "0")]
    [InlineData("1", "1e60")]
    [InlineData("2.5", "2.5e60")]
    public void DistanceFromQuettametersShouldProduceExpectedQuectoMeters(string value, string expectedQuectometers)
    {
        Distance<Float128> d = Distance<Float128>.FromQuettameters(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expectedQuectometers), d.QuectoMeters);
    }

    [Theory(DisplayName = "Distance.FromInches should produce the expected QuectoMeters")]
    [InlineData("0", "0")]
    [InlineData("1", "2.54e28")]
    [InlineData("2.5", "6.35e28")]
    public void DistanceFromInchesShouldProduceExpectedQuectoMeters(string value, string expectedQuectometers)
    {
        Distance<Float128> d = Distance<Float128>.FromInches(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expectedQuectometers), d.QuectoMeters);
    }

    [Theory(DisplayName = "Distance.FromFeet should produce the expected QuectoMeters")]
    [InlineData("0", "0")]
    [InlineData("1", "3.048e29")]
    [InlineData("2.5", "7.62e29")]
    public void DistanceFromFeetShouldProduceExpectedQuectoMeters(string value, string expectedQuectometers)
    {
        Distance<Float128> d = Distance<Float128>.FromFeet(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expectedQuectometers), d.QuectoMeters);
    }

    [Theory(DisplayName = "Distance.FromYards should produce the expected QuectoMeters")]
    [InlineData("0", "0")]
    [InlineData("1", "9.144e29")]
    [InlineData("2.5", "2.286e30")]
    public void DistanceFromYardsShouldProduceExpectedQuectoMeters(string value, string expectedQuectometers)
    {
        Distance<Float128> d = Distance<Float128>.FromYards(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expectedQuectometers), d.QuectoMeters);
    }

    [Theory(DisplayName = "Distance.FromMiles should produce the expected QuectoMeters")]
    [InlineData("0", "0")]
    [InlineData("1", "1.609344e33")]
    [InlineData("2.5", "4.02336e33")]
    public void DistanceFromMilesShouldProduceExpectedQuectoMeters(string value, string expectedQuectometers)
    {
        Distance<Float128> d = Distance<Float128>.FromMiles(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expectedQuectometers), d.QuectoMeters);
    }

    [Theory(DisplayName = "Distance.FromNauticalMiles should produce the expected QuectoMeters")]
    [InlineData("0", "0")]
    [InlineData("1", "1.852e33")]
    [InlineData("2.5", "4.63e33")]
    public void DistanceFromNauticalMilesShouldProduceExpectedQuectoMeters(string value, string expectedQuectometers)
    {
        Distance<Float128> d = Distance<Float128>.FromNauticalMiles(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expectedQuectometers), d.QuectoMeters);
    }

    [Theory(DisplayName = "Distance.FromFermis should produce the expected QuectoMeters")]
    [InlineData("0", "0")]
    [InlineData("1", "1e15")]
    [InlineData("2.5", "2.5e15")]
    public void DistanceFromFermisShouldProduceExpectedQuectoMeters(string value, string expectedQuectometers)
    {
        Distance<Float128> d = Distance<Float128>.FromFermis(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expectedQuectometers), d.QuectoMeters);
    }

    [Theory(DisplayName = "Distance.FromAngstroms should produce the expected QuectoMeters")]
    [InlineData("0", "0")]
    [InlineData("1", "1e20")]
    [InlineData("2.5", "2.5e20")]
    public void DistanceFromAngstromsShouldProduceExpectedQuectoMeters(string value, string expectedQuectometers)
    {
        Distance<Float128> d = Distance<Float128>.FromAngstroms(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expectedQuectometers), d.QuectoMeters);
    }

    [Theory(DisplayName = "Distance.FromAstronomicalUnits should produce the expected QuectoMeters")]
    [InlineData("0", "0")]
    [InlineData("1", "1.495978707e41")]
    [InlineData("2.5", "3.7399467675e41")]
    public void DistanceFromAstronomicalUnitsShouldProduceExpectedQuectoMeters(string value, string expectedQuectometers)
    {
        Distance<Float128> d = Distance<Float128>.FromAstronomicalUnits(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expectedQuectometers), d.QuectoMeters);
    }

    [Theory(DisplayName = "Distance.FromLightYears should produce the expected QuectoMeters")]
    [InlineData("0", "0")]
    [InlineData("1", "9.4607304725808e45")]
    [InlineData("2.5", "2.3651826181452e46")]
    public void DistanceFromLightYearsShouldProduceExpectedQuectoMeters(string value, string expectedQuectometers)
    {
        Distance<Float128> d = Distance<Float128>.FromLightYears(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expectedQuectometers), d.QuectoMeters);
    }

    [Theory(DisplayName = "Distance.FromParsecs should produce the expected QuectoMeters")]
    [InlineData("0")]
    [InlineData("1")]
    [InlineData("2.5")]
    public void DistanceFromParsecsShouldProduceExpectedQuectoMeters(string value)
    {
        // Given — verify the unit applies the IAU parsec definition: 1 pc = (648000 / π) AU.
        // Compute the expected value at Float128 precision using the same chain as the unit; π is
        // irrational so the result is the closest-Float128 to (648000 × AU_meters × 10^30 / π).
        Float128 input = Float128.Parse(value);
        Float128 auMeters = 149_597_870_700L;
        Float128 quectometersPerParsec = auMeters * 648000 * UnitMath.Pow10<Float128>(30) / Float128.Pi;
        Float128 expectedQuectometers = input * quectometersPerParsec;

        // When
        Distance<Float128> d = Distance<Float128>.FromParsecs(input);

        // Then
        Assert.Equal(expectedQuectometers, d.QuectoMeters);
    }

    [Fact(DisplayName = "Distance.Add should produce the expected result")]
    public void DistanceAddShouldProduceExpectedValue()
    {
        // Given
        Distance<Float128> left = Distance<Float128>.FromMeters(Float128.Parse("1500"));
        Distance<Float128> right = Distance<Float128>.FromMeters(Float128.Parse("500"));

        // When
        Distance<Float128> result = left.Add(right);

        // Then
        Assert.Equal(Float128.Parse("2000"), result.Meters);
    }

    [Fact(DisplayName = "Distance.Subtract should produce the expected result")]
    public void DistanceSubtractShouldProduceExpectedValue()
    {
        // Given
        Distance<Float128> left = Distance<Float128>.FromMeters(Float128.Parse("1500"));
        Distance<Float128> right = Distance<Float128>.FromMeters(Float128.Parse("400"));

        // When
        Distance<Float128> result = left.Subtract(right);

        // Then
        Assert.Equal(Float128.Parse("1100"), result.Meters);
    }

    [Fact(DisplayName = "Distance comparison should produce the expected result (left equal to right)")]
    public void DistanceComparisonShouldProduceExpectedResultLeftEqualToRight()
    {
        // Given
        Distance<Float128> left = Distance<Float128>.FromMeters(Float128.Parse("1234"));
        Distance<Float128> right = Distance<Float128>.FromMeters(Float128.Parse("1234"));

        // When / Then
        Assert.Equal(0, Distance<Float128>.Compare(left, right));
        Assert.Equal(0, left.CompareTo(right));
        Assert.Equal(0, left.CompareTo((object)right));
        Assert.False(left > right);
        Assert.True(left >= right);
        Assert.False(left < right);
        Assert.True(left <= right);
    }

    [Fact(DisplayName = "Distance comparison should produce the expected result (left greater than right)")]
    public void DistanceComparisonShouldProduceExpectedLeftGreaterThanRight()
    {
        // Given
        Distance<Float128> left = Distance<Float128>.FromMeters(Float128.Parse("4567"));
        Distance<Float128> right = Distance<Float128>.FromMeters(Float128.Parse("1234"));

        // When / Then
        Assert.Equal(1, Distance<Float128>.Compare(left, right));
        Assert.Equal(1, left.CompareTo(right));
        Assert.Equal(1, left.CompareTo((object)right));
        Assert.True(left > right);
        Assert.True(left >= right);
        Assert.False(left < right);
        Assert.False(left <= right);
    }

    [Fact(DisplayName = "Distance comparison should produce the expected result (left less than right)")]
    public void DistanceComparisonShouldProduceExpectedLeftLessThanRight()
    {
        // Given
        Distance<Float128> left = Distance<Float128>.FromMeters(Float128.Parse("1234"));
        Distance<Float128> right = Distance<Float128>.FromMeters(Float128.Parse("4567"));

        // When / Then
        Assert.Equal(-1, Distance<Float128>.Compare(left, right));
        Assert.Equal(-1, left.CompareTo(right));
        Assert.Equal(-1, left.CompareTo((object)right));
        Assert.False(left > right);
        Assert.False(left >= right);
        Assert.True(left < right);
        Assert.True(left <= right);
    }

    [Fact(DisplayName = "Distance equality should produce the expected result (left equal to right)")]
    public void DistanceEqualityShouldProduceExpectedResultLeftEqualToRight()
    {
        // Given — 2 km and 2000 m reduce to the same QuectoMeters canonical at Float128.
        Distance<Float128> left = Distance<Float128>.FromKilometers(Float128.Parse("2"));
        Distance<Float128> right = Distance<Float128>.FromMeters(Float128.Parse("2000"));

        // When / Then
        Assert.True(Distance<Float128>.Equals(left, right));
        Assert.True(left.Equals(right));
        Assert.True(left.Equals((object)right));
        Assert.True(left == right);
        Assert.False(left != right);
    }

    [Fact(DisplayName = "Distance equality should produce the expected result (left not equal to right)")]
    public void DistanceEqualityShouldProduceExpectedResultLeftNotEqualToRight()
    {
        // Given
        Distance<Float128> left = Distance<Float128>.FromKilometers(Float128.Parse("2"));
        Distance<Float128> right = Distance<Float128>.FromMeters(Float128.Parse("2500"));

        // When / Then
        Assert.False(Distance<Float128>.Equals(left, right));
        Assert.False(left.Equals(right));
        Assert.False(left.Equals((object)right));
        Assert.False(left == right);
        Assert.True(left != right);
    }

    [Fact(DisplayName = "Distance.ToString should produce the expected result")]
    public void DistanceToStringShouldProduceExpectedResult()
    {
        // Given
        Distance<Float128> d = Distance<Float128>.FromMeters(Float128.Parse("1000"));

        // When / Then
        Assert.Equal("1,000.000 m", $"{d:m3}");
        Assert.Equal("1.000 km", $"{d:km3}");
        Assert.Equal("10.000 hm", $"{d:hm3}");
        Assert.Equal("100.000 dam", $"{d:dam3}");
        Assert.Equal("100,000.000 cm", $"{d:cm3}");
        Assert.Equal("39,370.079 in", $"{d:in3}");
        Assert.Equal("3,280.840 ft", $"{d:ft3}");
        Assert.Equal("1,093.613 yd", $"{d:yd3}");
    }

    [Fact(DisplayName = "Distance.ToString should honor custom culture separators")]
    public void DistanceToStringShouldHonorCustomCulture()
    {
        // Given
        CultureInfo customCulture = new("de-DE");
        Distance<Float128> d = Distance<Float128>.FromMeters(Float128.Parse("1234.56"));

        // When
        string formatted = d.ToString("m2", customCulture);

        // Then
        Assert.Equal("1.234,56 m", formatted);
    }

    [Theory(DisplayName = "Distance.ValueOf should return the value at the matching scale")]
    [InlineData("qm")]
    [InlineData("rm")]
    [InlineData("ym")]
    [InlineData("zm")]
    [InlineData("am")]
    [InlineData("fm")]
    [InlineData("pm")]
    [InlineData("nm")]
    [InlineData("um")]
    [InlineData("mm")]
    [InlineData("cm")]
    [InlineData("dm")]
    [InlineData("m")]
    [InlineData("dam")]
    [InlineData("hm")]
    [InlineData("km")]
    [InlineData("Mm")]
    [InlineData("Gm")]
    [InlineData("Tm")]
    [InlineData("Pm")]
    [InlineData("Em")]
    [InlineData("Zm")]
    [InlineData("Ym")]
    [InlineData("Rm")]
    [InlineData("Qm")]
    [InlineData("in")]
    [InlineData("ft")]
    [InlineData("yd")]
    [InlineData("mi")]
    [InlineData("nmi")]
    [InlineData("fmi")]
    [InlineData("a")]
    [InlineData("au")]
    [InlineData("ly")]
    [InlineData("pc")]
    public void DistanceValueOfShouldReturnValueAtMatchingScale(string specifier)
    {
        // Given
        Distance<Float128> d = Distance<Float128>.FromMeters(Float128.Parse("1234.567"));

        // When
        Float128 expected = specifier switch
        {
            "qm" => d.QuectoMeters,
            "rm" => d.RontoMeters,
            "ym" => d.YoctoMeters,
            "zm" => d.ZeptoMeters,
            "am" => d.AttoMeters,
            "fm" => d.FemtoMeters,
            "pm" => d.PicoMeters,
            "nm" => d.NanoMeters,
            "um" => d.MicroMeters,
            "mm" => d.MilliMeters,
            "cm" => d.CentiMeters,
            "dm" => d.DeciMeters,
            "m" => d.Meters,
            "dam" => d.DecaMeters,
            "hm" => d.HectoMeters,
            "km" => d.KiloMeters,
            "Mm" => d.MegaMeters,
            "Gm" => d.GigaMeters,
            "Tm" => d.TeraMeters,
            "Pm" => d.PetaMeters,
            "Em" => d.ExaMeters,
            "Zm" => d.ZettaMeters,
            "Ym" => d.YottaMeters,
            "Rm" => d.RonnaMeters,
            "Qm" => d.QuettaMeters,
            "in" => d.Inches,
            "ft" => d.Feet,
            "yd" => d.Yards,
            "mi" => d.Miles,
            "nmi" => d.NauticalMiles,
            "fmi" => d.Fermis,
            "a" => d.Angstroms,
            "au" => d.AstronomicalUnits,
            "ly" => d.LightYears,
            "pc" => d.Parsecs,
            _ => throw new InvalidOperationException($"Unhandled specifier: {specifier}")
        };

        // Then
        Assert.Equal(expected, d.ValueOf(specifier));
    }

    [Fact(DisplayName = "Distance.ValueOf should throw on invalid specifier")]
    public void DistanceValueOfShouldThrowOnInvalidSpecifier()
    {
        // Given
        Distance<Float128> distance = Distance<Float128>.FromMiles(Float128.Parse("25"));

        // Then
        Assert.Throws<ArgumentException>(() => distance.ValueOf("xx"));
    }
}
