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
    // IEEE-754 binary floating-point arithmetic causes small discrepancies in calculation, therefore we need a tolerance.
    private const double Tolerance = 1e+42;

    [Fact(DisplayName = "Distance.Zero should produce the expected result")]
    public void DistanceZeroShouldProduceExpectedResult()
    {
        // Given / When
        Distance<double> distance = Distance<double>.Zero;

        // Then
        Assert.Equal(0.0, distance.QuectoMeters, Tolerance);
    }

    [Theory(DisplayName = "Distance.FromQuectometers should produce the expected QuectoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1.0)]
    [InlineData(2.5, 2.5)]
    public void DistanceFromQuectometersShouldProduceExpectedQuectoMeters(double value, double expectedQuectometers)
    {
        // Given / When
        Distance<double> d = Distance<double>.FromQuectometers(value);

        // Then
        Assert.Equal(expectedQuectometers, d.QuectoMeters, Tolerance);
    }

    [Theory(DisplayName = "Distance.FromRontometers should produce the expected QuectoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e3)]
    [InlineData(2.5, 2.5e3)]
    public void DistanceFromRontometersShouldProduceExpectedQuectoMeters(double value, double expectedQuectometers)
    {
        // Given / When
        Distance<double> d = Distance<double>.FromRontometers(value);

        // Then
        Assert.Equal(expectedQuectometers, d.QuectoMeters, Tolerance);
    }

    [Theory(DisplayName = "Distance.FromYoctometers should produce the expected QuectoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e6)]
    [InlineData(2.5, 2.5e6)]
    public void DistanceFromYoctometersShouldProduceExpectedQuectoMeters(double value, double expectedQuectometers)
    {
        // Given / When
        Distance<double> d = Distance<double>.FromYoctometers(value);

        // Then
        Assert.Equal(expectedQuectometers, d.QuectoMeters, Tolerance);
    }

    [Theory(DisplayName = "Distance.FromZeptometers should produce the expected QuectoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e9)]
    [InlineData(2.5, 2.5e9)]
    public void DistanceFromZeptometersShouldProduceExpectedQuectoMeters(double value, double expectedQuectometers)
    {
        // Given / When
        Distance<double> d = Distance<double>.FromZeptometers(value);

        // Then
        Assert.Equal(expectedQuectometers, d.QuectoMeters, Tolerance);
    }

    [Theory(DisplayName = "Distance.FromAttometers should produce the expected QuectoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e12)]
    [InlineData(2.5, 2.5e12)]
    public void DistanceFromAttometersShouldProduceExpectedQuectoMeters(double value, double expectedQuectometers)
    {
        // Given / When
        Distance<double> d = Distance<double>.FromAttometers(value);

        // Then
        Assert.Equal(expectedQuectometers, d.QuectoMeters, Tolerance);
    }

    [Theory(DisplayName = "Distance.FromFemtometers should produce the expected QuectoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e15)]
    [InlineData(2.5, 2.5e15)]
    public void DistanceFromFemtometersShouldProduceExpectedQuectoMeters(double value, double expectedQuectometers)
    {
        // Given / When
        Distance<double> d = Distance<double>.FromFemtometers(value);

        // Then
        Assert.Equal(expectedQuectometers, d.QuectoMeters, Tolerance);
    }

    [Theory(DisplayName = "Distance.FromPicometers should produce the expected QuectoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e18)]
    [InlineData(2.5, 2.5e18)]
    public void DistanceFromPicometersShouldProduceExpectedQuectoMeters(double value, double expectedQuectometers)
    {
        // Given / When
        Distance<double> d = Distance<double>.FromPicometers(value);

        // Then
        Assert.Equal(expectedQuectometers, d.QuectoMeters, Tolerance);
    }

    [Theory(DisplayName = "Distance.FromNanometers should produce the expected QuectoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e21)]
    [InlineData(2.5, 2.5e21)]
    public void DistanceFromNanometersShouldProduceExpectedQuectoMeters(double value, double expectedQuectometers)
    {
        // Given / When
        Distance<double> d = Distance<double>.FromNanometers(value);

        // Then
        Assert.Equal(expectedQuectometers, d.QuectoMeters, Tolerance);
    }

    [Theory(DisplayName = "Distance.FromMicrometers should produce the expected QuectoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e24)]
    [InlineData(2.5, 2.5e24)]
    public void DistanceFromMicrometersShouldProduceExpectedQuectoMeters(double value, double expectedQuectometers)
    {
        // Given / When
        Distance<double> d = Distance<double>.FromMicrometers(value);

        // Then
        Assert.Equal(expectedQuectometers, d.QuectoMeters, Tolerance);
    }

    [Theory(DisplayName = "Distance.FromMillimeters should produce the expected QuectoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e27)]
    [InlineData(2.5, 2.5e27)]
    public void DistanceFromMillimetersShouldProduceExpectedQuectoMeters(double value, double expectedQuectometers)
    {
        // Given / When
        Distance<double> d = Distance<double>.FromMillimeters(value);

        // Then
        Assert.Equal(expectedQuectometers, d.QuectoMeters, Tolerance);
    }

    [Theory(DisplayName = "Distance.FromCentimeters should produce the expected QuectoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e28)]
    [InlineData(2.5, 2.5e28)]
    public void DistanceFromCentimetersShouldProduceExpectedQuectoMeters(double value, double expectedQuectometers)
    {
        // Given / When
        Distance<double> d = Distance<double>.FromCentimeters(value);

        // Then
        Assert.Equal(expectedQuectometers, d.QuectoMeters, Tolerance);
    }

    [Theory(DisplayName = "Distance.FromDecimeters should produce the expected QuectoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e29)]
    [InlineData(2.5, 2.5e29)]
    public void DistanceFromDecimetersShouldProduceExpectedQuectoMeters(double value, double expectedQuectometers)
    {
        // Given / When
        Distance<double> d = Distance<double>.FromDecimeters(value);

        // Then
        Assert.Equal(expectedQuectometers, d.QuectoMeters, Tolerance);
    }

    [Theory(DisplayName = "Distance.FromMeters should produce the expected QuectoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e30)]
    [InlineData(2.5, 2.5e30)]
    public void DistanceFromMetersShouldProduceExpectedQuectoMeters(double value, double expectedQuectometers)
    {
        // Given / When
        Distance<double> d = Distance<double>.FromMeters(value);

        // Then
        Assert.Equal(expectedQuectometers, d.QuectoMeters, Tolerance);
    }

    [Theory(DisplayName = "Distance.FromDecameters should produce the expected QuectoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e31)]
    [InlineData(2.5, 2.5e31)]
    public void DistanceFromDecametersShouldProduceExpectedQuectoMeters(double value, double expectedQuectometers)
    {
        // Given / When
        Distance<double> d = Distance<double>.FromDecameters(value);

        // Then
        Assert.Equal(expectedQuectometers, d.QuectoMeters, Tolerance);
    }

    [Theory(DisplayName = "Distance.FromHectometers should produce the expected QuectoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e32)]
    [InlineData(2.5, 2.5e32)]
    public void DistanceFromHectometersShouldProduceExpectedQuectoMeters(double value, double expectedQuectometers)
    {
        // Given / When
        Distance<double> d = Distance<double>.FromHectometers(value);

        // Then
        Assert.Equal(expectedQuectometers, d.QuectoMeters, Tolerance);
    }

    [Theory(DisplayName = "Distance.FromKilometers should produce the expected QuectoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e33)]
    [InlineData(2.5, 2.5e33)]
    public void DistanceFromKilometersShouldProduceExpectedQuectoMeters(double value, double expectedQuectometers)
    {
        // Given / When
        Distance<double> d = Distance<double>.FromKilometers(value);

        // Then
        Assert.Equal(expectedQuectometers, d.QuectoMeters, Tolerance);
    }

    [Theory(DisplayName = "Distance.FromMegameters should produce the expected QuectoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e36)]
    [InlineData(2.5, 2.5e36)]
    public void DistanceFromMegametersShouldProduceExpectedQuectoMeters(double value, double expectedQuectometers)
    {
        // Given / When
        Distance<double> d = Distance<double>.FromMegameters(value);

        // Then
        Assert.Equal(expectedQuectometers, d.QuectoMeters, Tolerance);
    }

    [Theory(DisplayName = "Distance.FromGigameters should produce the expected QuectoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e39)]
    [InlineData(2.5, 2.5e39)]
    public void DistanceFromGigametersShouldProduceExpectedQuectoMeters(double value, double expectedQuectometers)
    {
        // Given / When
        Distance<double> d = Distance<double>.FromGigameters(value);

        // Then
        Assert.Equal(expectedQuectometers, d.QuectoMeters, Tolerance);
    }

    [Theory(DisplayName = "Distance.FromTerameters should produce the expected QuectoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e42)]
    [InlineData(2.5, 2.5e42)]
    public void DistanceFromTerametersShouldProduceExpectedQuectoMeters(double value, double expectedQuectometers)
    {
        // Given / When
        Distance<double> d = Distance<double>.FromTerameters(value);

        // Then
        Assert.Equal(expectedQuectometers, d.QuectoMeters, Tolerance);
    }

    [Theory(DisplayName = "Distance.FromPetameters should produce the expected QuectoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e45)]
    [InlineData(2.5, 2.5e45)]
    public void DistanceFromPetametersShouldProduceExpectedQuectoMeters(double value, double expectedQuectometers)
    {
        // Given / When
        Distance<double> d = Distance<double>.FromPetameters(value);

        // Then
        Assert.Equal(expectedQuectometers, d.QuectoMeters, Tolerance);
    }

    [Theory(DisplayName = "Distance.FromExameters should produce the expected QuectoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e48)]
    [InlineData(2.5, 2.5e48)]
    public void DistanceFromExametersShouldProduceExpectedQuectoMeters(double value, double expectedQuectometers)
    {
        // Given / When
        Distance<double> d = Distance<double>.FromExameters(value);

        // Then
        Assert.Equal(expectedQuectometers, d.QuectoMeters, Tolerance);
    }

    [Theory(DisplayName = "Distance.FromZettameters should produce the expected QuectoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e51)]
    [InlineData(2.5, 2.5e51)]
    public void DistanceFromZettametersShouldProduceExpectedQuectoMeters(double value, double expectedQuectometers)
    {
        // Given / When
        Distance<double> d = Distance<double>.FromZettameters(value);

        // Then
        Assert.Equal(expectedQuectometers, d.QuectoMeters, Tolerance);
    }

    [Theory(DisplayName = "Distance.FromYottameters should produce the expected QuectoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e54)]
    [InlineData(2.5, 2.5e54)]
    public void DistanceFromYottametersShouldProduceExpectedQuectoMeters(double value, double expectedQuectometers)
    {
        // Given / When
        Distance<double> d = Distance<double>.FromYottameters(value);

        // Then
        Assert.Equal(expectedQuectometers, d.QuectoMeters, Tolerance);
    }

    [Theory(DisplayName = "Distance.FromRonnameters should produce the expected QuectoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e57)]
    [InlineData(2.5, 2.5e57)]
    public void DistanceFromRonnametersShouldProduceExpectedQuectoMeters(double value, double expectedQuectometers)
    {
        // Given / When
        Distance<double> d = Distance<double>.FromRonnameters(value);

        // Then
        Assert.Equal(expectedQuectometers, d.QuectoMeters, Tolerance);
    }

    [Theory(DisplayName = "Distance.FromQuettameters should produce the expected QuectoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e60)]
    [InlineData(2.5, 2.5e60)]
    public void DistanceFromQuettametersShouldProduceExpectedQuectoMeters(double value, double expectedQuectometers)
    {
        // Given / When
        Distance<double> d = Distance<double>.FromQuettameters(value);

        // Then
        Assert.Equal(expectedQuectometers, d.QuectoMeters, Tolerance);
    }

    [Theory(DisplayName = "Distance.FromInches should produce the expected QuectoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 2.54e28)]
    [InlineData(2.5, 6.35e28)]
    public void DistanceFromInchesShouldProduceExpectedQuectoMeters(double value, double expectedQuectometers)
    {
        // Given / When
        Distance<double> d = Distance<double>.FromInches(value);

        // Then
        Assert.Equal(expectedQuectometers, d.QuectoMeters, Tolerance);
    }

    [Theory(DisplayName = "Distance.FromFeet should produce the expected QuectoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 3.048e29)]
    [InlineData(2.5, 7.62e29)]
    public void DistanceFromFeetShouldProduceExpectedQuectoMeters(double value, double expectedQuectometers)
    {
        // Given / When
        Distance<double> d = Distance<double>.FromFeet(value);

        // Then
        Assert.Equal(expectedQuectometers, d.QuectoMeters, Tolerance);
    }

    [Theory(DisplayName = "Distance.FromYards should produce the expected QuectoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 9.144e29)]
    [InlineData(2.5, 2.286e30)]
    public void DistanceFromYardsShouldProduceExpectedQuectoMeters(double value, double expectedQuectometers)
    {
        // Given / When
        Distance<double> d = Distance<double>.FromYards(value);

        // Then
        Assert.Equal(expectedQuectometers, d.QuectoMeters, Tolerance);
    }

    [Theory(DisplayName = "Distance.FromMiles should produce the expected QuectoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1.609344e33)]
    [InlineData(2.5, 4.02336e33)]
    public void DistanceFromMilesShouldProduceExpectedQuectoMeters(double value, double expectedQuectometers)
    {
        // Given / When
        Distance<double> d = Distance<double>.FromMiles(value);

        // Then
        Assert.Equal(expectedQuectometers, d.QuectoMeters, Tolerance);
    }

    [Theory(DisplayName = "Distance.FromNauticalMiles should produce the expected QuectoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1.852e33)]
    [InlineData(2.5, 4.63e33)]
    public void DistanceFromNauticalMilesShouldProduceExpectedQuectoMeters(double value, double expectedQuectometers)
    {
        // Given / When
        Distance<double> d = Distance<double>.FromNauticalMiles(value);

        // Then
        Assert.Equal(expectedQuectometers, d.QuectoMeters, Tolerance);
    }

    [Theory(DisplayName = "Distance.FromFermis should produce the expected QuectoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e15)]
    [InlineData(2.5, 2.5e15)]
    public void DistanceFromFermisShouldProduceExpectedQuectoMeters(double value, double expectedQuectometers)
    {
        // Given / When
        Distance<double> d = Distance<double>.FromFermis(value);

        // Then
        Assert.Equal(expectedQuectometers, d.QuectoMeters, Tolerance);
    }

    [Theory(DisplayName = "Distance.FromAngstroms should produce the expected QuectoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e20)]
    [InlineData(2.5, 2.5e20)]
    public void DistanceFromAngstromsShouldProduceExpectedQuectoMeters(double value, double expectedQuectometers)
    {
        // Given / When
        Distance<double> d = Distance<double>.FromAngstroms(value);

        // Then
        Assert.Equal(expectedQuectometers, d.QuectoMeters, Tolerance);
    }

    [Theory(DisplayName = "Distance.FromAstronomicalUnits should produce the expected QuectoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1.495978707e41)]
    [InlineData(2.5, 3.7399467675e41)]
    public void DistanceFromAstronomicalUnitsShouldProduceExpectedQuectoMeters(double value, double expectedQuectometers)
    {
        // Given / When
        Distance<double> d = Distance<double>.FromAstronomicalUnits(value);

        // Then
        Assert.Equal(expectedQuectometers, d.QuectoMeters, Tolerance);
    }

    [Theory(DisplayName = "Distance.FromLightYears should produce the expected QuectoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 9.4607304725808e45)]
    [InlineData(2.5, 2.3651826181452e46)]
    public void DistanceFromLightYearsShouldProduceExpectedQuectoMeters(double value, double expectedQuectometers)
    {
        // Given / When
        Distance<double> d = Distance<double>.FromLightYears(value);

        // Then
        Assert.Equal(expectedQuectometers, d.QuectoMeters, Tolerance);
    }

    [Theory(DisplayName = "Distance.FromParsecs should produce the expected QuectoMeters")]
    [InlineData(0.0)]
    [InlineData(1.0)]
    [InlineData(2.5)]
    public void DistanceFromParsecsShouldProduceExpectedQuectoMeters(double value)
    {
        // Given
        const double metersPerParsec = 1.495978707e11 * 648000.0 / Math.PI;
        const double qmPerParsec = metersPerParsec * 1e30;
        double expectedQuectometers = value * qmPerParsec;

        // When
        Distance<double> d = Distance<double>.FromParsecs(value);

        // Then
        Assert.Equal(expectedQuectometers, d.QuectoMeters, Tolerance);
    }

    [Fact(DisplayName = "Distance.Add should produce the expected result")]
    public void DistanceAddShouldProduceExpectedValue()
    {
        // Given
        Distance<double> left = Distance<double>.FromMeters(1500.0);
        Distance<double> right = Distance<double>.FromMeters(500.0);

        // When
        Distance<double> result = left.Add(right);

        // Then
        Assert.Equal(2000.0, result.Meters, Tolerance);
    }

    [Fact(DisplayName = "Distance.Subtract should produce the expected result")]
    public void DistanceSubtractShouldProduceExpectedValue()
    {
        // Given
        Distance<double> left = Distance<double>.FromMeters(1500.0);
        Distance<double> right = Distance<double>.FromMeters(400.0);

        // When
        Distance<double> result = left.Subtract(right);

        // Then
        Assert.Equal(1100.0, result.Meters, Tolerance);
    }

    [Fact(DisplayName = "Distance.Multiply should produce the expected result")]
    public void DistanceMultiplyShouldProduceExpectedValue()
    {
        // Given
        Distance<double> left = Distance<double>.FromMeters(10.0); // 1e31 qm
        Distance<double> right = Distance<double>.FromMeters(3.0); // 3e30 qm

        // When
        Distance<double> result = left.Multiply(right); // 1e31 * 3e30 = 3e61 qm

        // Then
        Assert.Equal(1e31, left.QuectoMeters, Tolerance);
        Assert.Equal(3e30, right.QuectoMeters, Tolerance);
        Assert.Equal(3e61, result.QuectoMeters, Tolerance);
    }

    [Fact(DisplayName = "Distance.Divide should produce the expected result")]
    public void DistanceDivideShouldProduceExpectedValue()
    {
        // Given
        Distance<double> left = Distance<double>.FromMeters(100.0); // 1e32 qm
        Distance<double> right = Distance<double>.FromMeters(20.0); // 2e31 qm

        // When
        Distance<double> result = left.Divide(right); // 1e32 / 2e31 = 5 qm

        // Then
        Assert.Equal(5.0, result.QuectoMeters, Tolerance);
        Assert.Equal(5e-30, result.Meters, Tolerance);
    }

    [Fact(DisplayName = "Distance comparison should produce the expected result (left equal to right)")]
    public void DistanceComparisonShouldProduceExpectedResultLeftEqualToRight()
    {
        // Given
        Distance<double> left = Distance<double>.FromMeters(1234.0);
        Distance<double> right = Distance<double>.FromMeters(1234.0);

        // When / Then
        Assert.Equal(0, Distance<double>.Compare(left, right));
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
        Distance<double> left = Distance<double>.FromMeters(4567.0);
        Distance<double> right = Distance<double>.FromMeters(1234.0);

        // When / Then
        Assert.Equal(1, Distance<double>.Compare(left, right));
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
        Distance<double> left = Distance<double>.FromMeters(1234.0);
        Distance<double> right = Distance<double>.FromMeters(4567.0);

        // When / Then
        Assert.Equal(-1, Distance<double>.Compare(left, right));
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
        // Given
        Distance<BigDecimal> left = Distance<BigDecimal>.FromKilometers(2.0);
        Distance<BigDecimal> right = Distance<BigDecimal>.FromMeters(2000.0);

        // When / Then
        Assert.True(Distance<BigDecimal>.Equals(left, right));
        Assert.True(left.Equals(right));
        Assert.True(left.Equals((object)right));
        Assert.True(left == right);
        Assert.False(left != right);
    }

    [Fact(DisplayName = "Distance equality should produce the expected result (left not equal to right)")]
    public void DistanceEqualityShouldProduceExpectedResultLeftNotEqualToRight()
    {
        // Given
        Distance<double> left = Distance<double>.FromKilometers(2.0);
        Distance<double> right = Distance<double>.FromMeters(2500.0);

        // When / Then
        Assert.False(Distance<double>.Equals(left, right));
        Assert.False(left.Equals(right));
        Assert.False(left.Equals((object)right));
        Assert.False(left == right);
        Assert.True(left != right);
    }

    [Fact(DisplayName = "Distance.ToString should produce the expected result")]
    public void DistanceToStringShouldProduceExpectedResult()
    {
        // Given
        Distance<double> d = Distance<double>.FromMeters(1000.0);

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
        Distance<double> d = Distance<double>.FromMeters(1234.56);

        // When
        string formatted = d.ToString("m2", customCulture);

        // Then
        Assert.Equal("1.234,56 m", formatted);
    }
}
