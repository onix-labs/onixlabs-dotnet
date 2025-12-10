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
    // IEEE-754 binary floating-point arithmetic causes small discrepancies in calculation, therefore we need a tolerance.
    private const double Tolerance = 1e+106;

    [Fact(DisplayName = "Area.Zero should produce the expected result")]
    public void AreaZeroShouldProduceExpectedResult()
    {
        // Given / When
        Area<double> area = Area<double>.Zero;

        // Then
        Assert.Equal(0.0, area.SquareQuectoMeters, Tolerance);
    }

    [Theory(DisplayName = "Area.FromSquareQuectometers should produce the expected SquareQuectoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1.0)]
    [InlineData(2.5, 2.5)]
    public void AreaFromSquareQuectometersShouldProduceExpectedSquareQuectoMeters(double value, double expectedSquareQuectoMeters)
    {
        // Given / When
        Area<double> a = Area<double>.FromSquareQuectometers(value);

        // Then
        Assert.Equal(expectedSquareQuectoMeters, a.SquareQuectoMeters, Tolerance);
    }

    [Theory(DisplayName = "Area.FromSquareRontometers should produce the expected SquareQuectoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e6)]
    [InlineData(2.5, 2.5e6)]
    public void AreaFromSquareRontometersShouldProduceExpectedSquareQuectoMeters(double value, double expectedSquareQuectoMeters)
    {
        // Given / When
        Area<double> a = Area<double>.FromSquareRontometers(value);

        // Then
        Assert.Equal(expectedSquareQuectoMeters, a.SquareQuectoMeters, Tolerance);
    }

    [Theory(DisplayName = "Area.FromSquareYoctometers should produce the expected SquareQuectoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e12)]
    [InlineData(2.5, 2.5e12)]
    public void AreaFromSquareYoctometersShouldProduceExpectedSquareQuectoMeters(double value, double expectedSquareQuectoMeters)
    {
        // Given / When
        Area<double> a = Area<double>.FromSquareYoctometers(value);

        // Then
        Assert.Equal(expectedSquareQuectoMeters, a.SquareQuectoMeters, Tolerance);
    }

    [Theory(DisplayName = "Area.FromSquareZeptometers should produce the expected SquareQuectoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e18)]
    [InlineData(2.5, 2.5e18)]
    public void AreaFromSquareZeptometersShouldProduceExpectedSquareQuectoMeters(double value, double expectedSquareQuectoMeters)
    {
        // Given / When
        Area<double> a = Area<double>.FromSquareZeptometers(value);

        // Then
        Assert.Equal(expectedSquareQuectoMeters, a.SquareQuectoMeters, Tolerance);
    }

    [Theory(DisplayName = "Area.FromSquareAttometers should produce the expected SquareQuectoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e24)]
    [InlineData(2.5, 2.5e24)]
    public void AreaFromSquareAttometersShouldProduceExpectedSquareQuectoMeters(double value, double expectedSquareQuectoMeters)
    {
        // Given / When
        Area<double> a = Area<double>.FromSquareAttometers(value);

        // Then
        Assert.Equal(expectedSquareQuectoMeters, a.SquareQuectoMeters, Tolerance);
    }

    [Theory(DisplayName = "Area.FromSquareFemtometers should produce the expected SquareQuectoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e30)]
    [InlineData(2.5, 2.5e30)]
    public void AreaFromSquareFemtometersShouldProduceExpectedSquareQuectoMeters(double value, double expectedSquareQuectoMeters)
    {
        // Given / When
        Area<double> a = Area<double>.FromSquareFemtometers(value);

        // Then
        Assert.Equal(expectedSquareQuectoMeters, a.SquareQuectoMeters, Tolerance);
    }

    [Theory(DisplayName = "Area.FromSquarePicometers should produce the expected SquareQuectoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e36)]
    [InlineData(2.5, 2.5e36)]
    public void AreaFromSquarePicometersShouldProduceExpectedSquareQuectoMeters(double value, double expectedSquareQuectoMeters)
    {
        // Given / When
        Area<double> a = Area<double>.FromSquarePicometers(value);

        // Then
        Assert.Equal(expectedSquareQuectoMeters, a.SquareQuectoMeters, Tolerance);
    }

    [Theory(DisplayName = "Area.FromSquareNanometers should produce the expected SquareQuectoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e42)]
    [InlineData(2.5, 2.5e42)]
    public void AreaFromSquareNanometersShouldProduceExpectedSquareQuectoMeters(double value, double expectedSquareQuectoMeters)
    {
        // Given / When
        Area<double> a = Area<double>.FromSquareNanometers(value);

        // Then
        Assert.Equal(expectedSquareQuectoMeters, a.SquareQuectoMeters, Tolerance);
    }

    [Theory(DisplayName = "Area.FromSquareMicrometers should produce the expected SquareQuectoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e48)]
    [InlineData(2.5, 2.5e48)]
    public void AreaFromSquareMicrometersShouldProduceExpectedSquareQuectoMeters(double value, double expectedSquareQuectoMeters)
    {
        // Given / When
        Area<double> a = Area<double>.FromSquareMicrometers(value);

        // Then
        Assert.Equal(expectedSquareQuectoMeters, a.SquareQuectoMeters, Tolerance);
    }

    [Theory(DisplayName = "Area.FromSquareMillimeters should produce the expected SquareQuectoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e54)]
    [InlineData(2.5, 2.5e54)]
    public void AreaFromSquareMillimetersShouldProduceExpectedSquareQuectoMeters(double value, double expectedSquareQuectoMeters)
    {
        // Given / When
        Area<double> a = Area<double>.FromSquareMillimeters(value);

        // Then
        Assert.Equal(expectedSquareQuectoMeters, a.SquareQuectoMeters, Tolerance);
    }

    [Theory(DisplayName = "Area.FromSquareCentimeters should produce the expected SquareQuectoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e56)]
    [InlineData(2.5, 2.5e56)]
    public void AreaFromSquareCentimetersShouldProduceExpectedSquareQuectoMeters(double value, double expectedSquareQuectoMeters)
    {
        // Given / When
        Area<double> a = Area<double>.FromSquareCentimeters(value);

        // Then
        Assert.Equal(expectedSquareQuectoMeters, a.SquareQuectoMeters, Tolerance);
    }

    [Theory(DisplayName = "Area.FromSquareDecimeters should produce the expected SquareQuectoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e58)]
    [InlineData(2.5, 2.5e58)]
    public void AreaFromSquareDecimetersShouldProduceExpectedSquareQuectoMeters(double value, double expectedSquareQuectoMeters)
    {
        // Given / When
        Area<double> a = Area<double>.FromSquareDecimeters(value);

        // Then
        Assert.Equal(expectedSquareQuectoMeters, a.SquareQuectoMeters, Tolerance);
    }

    [Theory(DisplayName = "Area.FromSquareMeters should produce the expected SquareQuectoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e60)]
    [InlineData(2.5, 2.5e60)]
    public void AreaFromSquareMetersShouldProduceExpectedSquareQuectoMeters(double value, double expectedSquareQuectoMeters)
    {
        // Given / When
        Area<double> a = Area<double>.FromSquareMeters(value);

        // Then
        Assert.Equal(expectedSquareQuectoMeters, a.SquareQuectoMeters, Tolerance);
    }

    [Theory(DisplayName = "Area.FromSquareDecameters should produce the expected SquareQuectoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e62)]
    [InlineData(2.5, 2.5e62)]
    public void AreaFromSquareDecametersShouldProduceExpectedSquareQuectoMeters(double value, double expectedSquareQuectoMeters)
    {
        // Given / When
        Area<double> a = Area<double>.FromSquareDecameters(value);

        // Then
        Assert.Equal(expectedSquareQuectoMeters, a.SquareQuectoMeters, Tolerance);
    }

    [Theory(DisplayName = "Area.FromSquareHectometers should produce the expected SquareQuectoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e64)]
    [InlineData(2.5, 2.5e64)]
    public void AreaFromSquareHectometersShouldProduceExpectedSquareQuectoMeters(double value, double expectedSquareQuectoMeters)
    {
        // Given / When
        Area<double> a = Area<double>.FromSquareHectometers(value);

        // Then
        Assert.Equal(expectedSquareQuectoMeters, a.SquareQuectoMeters, Tolerance);
    }

    [Theory(DisplayName = "Area.FromSquareKilometers should produce the expected SquareQuectoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e66)]
    [InlineData(2.5, 2.5e66)]
    public void AreaFromSquareKilometersShouldProduceExpectedSquareQuectoMeters(double value, double expectedSquareQuectoMeters)
    {
        // Given / When
        Area<double> a = Area<double>.FromSquareKilometers(value);

        // Then
        Assert.Equal(expectedSquareQuectoMeters, a.SquareQuectoMeters, Tolerance);
    }

    [Theory(DisplayName = "Area.FromSquareMegameters should produce the expected SquareQuectoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e72)]
    [InlineData(2.5, 2.5e72)]
    public void AreaFromSquareMegametersShouldProduceExpectedSquareQuectoMeters(double value, double expectedSquareQuectoMeters)
    {
        // Given / When
        Area<double> a = Area<double>.FromSquareMegameters(value);

        // Then
        Assert.Equal(expectedSquareQuectoMeters, a.SquareQuectoMeters, Tolerance);
    }

    [Theory(DisplayName = "Area.FromSquareGigameters should produce the expected SquareQuectoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e78)]
    [InlineData(2.5, 2.5e78)]
    public void AreaFromSquareGigametersShouldProduceExpectedSquareQuectoMeters(double value, double expectedSquareQuectoMeters)
    {
        // Given / When
        Area<double> a = Area<double>.FromSquareGigameters(value);

        // Then
        Assert.Equal(expectedSquareQuectoMeters, a.SquareQuectoMeters, Tolerance);
    }

    [Theory(DisplayName = "Area.FromSquareTerameters should produce the expected SquareQuectoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e84)]
    [InlineData(2.5, 2.5e84)]
    public void AreaFromSquareTerametersShouldProduceExpectedSquareQuectoMeters(double value, double expectedSquareQuectoMeters)
    {
        // Given / When
        Area<double> a = Area<double>.FromSquareTerameters(value);

        // Then
        Assert.Equal(expectedSquareQuectoMeters, a.SquareQuectoMeters, Tolerance);
    }

    [Theory(DisplayName = "Area.FromSquarePetameters should produce the expected SquareQuectoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e90)]
    [InlineData(2.5, 2.5e90)]
    public void AreaFromSquarePetametersShouldProduceExpectedSquareQuectoMeters(double value, double expectedSquareQuectoMeters)
    {
        // Given / When
        Area<double> a = Area<double>.FromSquarePetameters(value);

        // Then
        Assert.Equal(expectedSquareQuectoMeters, a.SquareQuectoMeters, Tolerance);
    }

    [Theory(DisplayName = "Area.FromSquareExameters should produce the expected SquareQuectoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e96)]
    [InlineData(2.5, 2.5e96)]
    public void AreaFromSquareExametersShouldProduceExpectedSquareQuectoMeters(double value, double expectedSquareQuectoMeters)
    {
        // Given / When
        Area<double> a = Area<double>.FromSquareExameters(value);

        // Then
        Assert.Equal(expectedSquareQuectoMeters, a.SquareQuectoMeters, Tolerance);
    }

    [Theory(DisplayName = "Area.FromSquareZettameters should produce the expected SquareQuectoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e102)]
    [InlineData(2.5, 2.5e102)]
    public void AreaFromSquareZettametersShouldProduceExpectedSquareQuectoMeters(double value, double expectedSquareQuectoMeters)
    {
        // Given / When
        Area<double> a = Area<double>.FromSquareZettameters(value);

        // Then
        Assert.Equal(expectedSquareQuectoMeters, a.SquareQuectoMeters, Tolerance);
    }

    [Theory(DisplayName = "Area.FromSquareYottameters should produce the expected SquareQuectoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e108)]
    [InlineData(2.5, 2.5e108)]
    public void AreaFromSquareYottametersShouldProduceExpectedSquareQuectoMeters(double value, double expectedSquareQuectoMeters)
    {
        // Given / When
        Area<double> a = Area<double>.FromSquareYottameters(value);

        // Then
        Assert.Equal(expectedSquareQuectoMeters, a.SquareQuectoMeters, Tolerance);
    }

    [Theory(DisplayName = "Area.FromSquareRonnameters should produce the expected SquareQuectoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e114)]
    [InlineData(2.5, 2.5e114)]
    public void AreaFromSquareRonnametersShouldProduceExpectedSquareQuectoMeters(double value, double expectedSquareQuectoMeters)
    {
        // Given / When
        Area<double> a = Area<double>.FromSquareRonnameters(value);

        // Then
        Assert.Equal(expectedSquareQuectoMeters, a.SquareQuectoMeters, Tolerance);
    }

    [Theory(DisplayName = "Area.FromSquareQuettameters should produce the expected SquareQuectoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e120)]
    [InlineData(2.5, 2.5e120)]
    public void AreaFromSquareQuettametersShouldProduceExpectedSquareQuectoMeters(double value, double expectedSquareQuectoMeters)
    {
        // Given / When
        Area<double> a = Area<double>.FromSquareQuettameters(value);

        // Then
        Assert.Equal(expectedSquareQuectoMeters, a.SquareQuectoMeters, Tolerance);
    }

    [Theory(DisplayName = "Area.FromSquareInches should produce the expected SquareQuectoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 6.4516e56)]
    [InlineData(2.5, 1.6129e57)]
    public void AreaFromSquareInchesShouldProduceExpectedSquareQuectoMeters(double value, double expectedSquareQuectoMeters)
    {
        // Given / When
        Area<double> a = Area<double>.FromSquareInches(value);

        // Then
        Assert.Equal(expectedSquareQuectoMeters, a.SquareQuectoMeters, Tolerance);
    }

    [Theory(DisplayName = "Area.FromSquareFeet should produce the expected SquareQuectoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 9.290304e58)]
    [InlineData(2.5, 2.322576e59)]
    public void AreaFromSquareFeetShouldProduceExpectedSquareQuectoMeters(double value, double expectedSquareQuectoMeters)
    {
        // Given / When
        Area<double> a = Area<double>.FromSquareFeet(value);

        // Then
        Assert.Equal(expectedSquareQuectoMeters, a.SquareQuectoMeters, Tolerance);
    }

    [Theory(DisplayName = "Area.FromSquareYards should produce the expected SquareQuectoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 8.3612736e59)]
    [InlineData(2.5, 2.0903184e60)]
    public void AreaFromSquareYardsShouldProduceExpectedSquareQuectoMeters(double value, double expectedSquareQuectoMeters)
    {
        // Given / When
        Area<double> a = Area<double>.FromSquareYards(value);

        // Then
        Assert.Equal(expectedSquareQuectoMeters, a.SquareQuectoMeters, Tolerance);
    }

    [Theory(DisplayName = "Area.FromSquareMiles should produce the expected SquareQuectoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 2.589988110336e66)]
    [InlineData(2.5, 6.47497027584e66)]
    public void AreaFromSquareMilesShouldProduceExpectedSquareQuectoMeters(double value, double expectedSquareQuectoMeters)
    {
        // Given / When
        Area<double> a = Area<double>.FromSquareMiles(value);

        // Then
        Assert.Equal(expectedSquareQuectoMeters, a.SquareQuectoMeters, Tolerance);
    }

    [Theory(DisplayName = "Area.FromSquareNauticalMiles should produce the expected SquareQuectoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 3.4299040000e66)]
    [InlineData(2.5, 8.5747600000e66)]
    public void AreaFromSquareNauticalMilesShouldProduceExpectedSquareQuectoMeters(double value, double expectedSquareQuectoMeters)
    {
        // Given / When
        Area<double> a = Area<double>.FromSquareNauticalMiles(value);

        // Then
        Assert.Equal(expectedSquareQuectoMeters, a.SquareQuectoMeters, Tolerance);
    }

    [Theory(DisplayName = "Area.FromSquareFermis should produce the expected SquareQuectoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e30)]
    [InlineData(2.5, 2.5e30)]
    public void AreaFromSquareFermisShouldProduceExpectedSquareQuectoMeters(double value, double expectedSquareQuectoMeters)
    {
        // Given / When
        Area<double> a = Area<double>.FromSquareFermis(value);

        // Then
        Assert.Equal(expectedSquareQuectoMeters, a.SquareQuectoMeters, Tolerance);
    }

    [Theory(DisplayName = "Area.FromSquareAngstroms should produce the expected SquareQuectoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e40)]
    [InlineData(2.5, 2.5e40)]
    public void AreaFromSquareAngstromsShouldProduceExpectedSquareQuectoMeters(double value, double expectedSquareQuectoMeters)
    {
        // Given / When
        Area<double> a = Area<double>.FromSquareAngstroms(value);

        // Then
        Assert.Equal(expectedSquareQuectoMeters, a.SquareQuectoMeters, Tolerance);
    }

    [Theory(DisplayName = "Area.FromSquareAstronomicalUnits should produce the expected SquareQuectoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 2.2379522821e82)]
    [InlineData(2.5, 5.59488070525e82)]
    public void AreaFromSquareAstronomicalUnitsShouldProduceExpectedSquareQuectoMeters(double value, double expectedSquareQuectoMeters)
    {
        // Given / When
        Area<double> a = Area<double>.FromSquareAstronomicalUnits(value);

        // Then
        Assert.Equal(expectedSquareQuectoMeters, a.SquareQuectoMeters, Tolerance);
    }

    [Theory(DisplayName = "Area.FromSquareLightYears should produce the expected SquareQuectoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 8.9505421074819e91)]
    [InlineData(2.5, 2.237635526870475e92)]
    public void AreaFromSquareLightYearsShouldProduceExpectedSquareQuectoMeters(double value, double expectedSquareQuectoMeters)
    {
        // Given / When
        Area<double> a = Area<double>.FromSquareLightYears(value);

        // Then
        Assert.Equal(expectedSquareQuectoMeters, a.SquareQuectoMeters, Tolerance);
    }

    [Theory(DisplayName = "Area.FromSquareParsecs should produce the expected SquareQuectoMeters")]
    [InlineData(0.0)]
    [InlineData(1.0)]
    [InlineData(2.5)]
    public void AreaFromSquareParsecsShouldProduceExpectedSquareQuectoMeters(double value)
    {
        // Given
        const double metersPerParsec = 1.495978707e11 * 648000.0 / Math.PI;
        const double sqMetersPerSqParsec = metersPerParsec * metersPerParsec;
        const double sqQmPerSqParsec = sqMetersPerSqParsec * 1e60;
        double expectedSquareQuectoMeters = value * sqQmPerSqParsec;

        // When
        Area<double> a = Area<double>.FromSquareParsecs(value);

        // Then
        Assert.Equal(expectedSquareQuectoMeters, a.SquareQuectoMeters, Tolerance);
    }

    [Fact(DisplayName = "Area.Add should produce the expected result")]
    public void AreaAddShouldProduceExpectedValue()
    {
        // Given
        Area<double> left = Area<double>.FromSquareMeters(1500.0);
        Area<double> right = Area<double>.FromSquareMeters(500.0);

        // When
        Area<double> result = left.Add(right);

        // Then
        Assert.Equal(2000.0, result.SquareMeters, Tolerance);
    }

    [Fact(DisplayName = "Area.Subtract should produce the expected result")]
    public void AreaSubtractShouldProduceExpectedValue()
    {
        // Given
        Area<double> left = Area<double>.FromSquareMeters(1500.0);
        Area<double> right = Area<double>.FromSquareMeters(400.0);

        // When
        Area<double> result = left.Subtract(right);

        // Then
        Assert.Equal(1100.0, result.SquareMeters, Tolerance);
    }

    [Fact(DisplayName = "Area.Multiply should produce the expected result")]
    public void AreaMultiplyShouldProduceExpectedValue()
    {
        // Given
        Area<double> left = Area<double>.FromSquareMeters(10.0); // 1e61 sqQm
        Area<double> right = Area<double>.FromSquareMeters(3.0); // 3e60 sqQm

        // When
        Area<double> result = left.Multiply(right); // 1e61 * 3e60 = 3e121 sqQm

        // Then
        Assert.Equal(1e61, left.SquareQuectoMeters, Tolerance);
        Assert.Equal(3e60, right.SquareQuectoMeters, Tolerance);
        Assert.Equal(3e121, result.SquareQuectoMeters, Tolerance);
    }

    [Fact(DisplayName = "Area.Divide should produce the expected result")]
    public void AreaDivideShouldProduceExpectedValue()
    {
        // Given
        Area<double> left = Area<double>.FromSquareMeters(100.0); // 1e62 sqQm
        Area<double> right = Area<double>.FromSquareMeters(20.0); // 2e61 sqQm

        // When
        Area<double> result = left.Divide(right); // 1e62 / 2e61 = 5 sqQm

        // Then
        Assert.Equal(5.0, result.SquareQuectoMeters, Tolerance);
        Assert.Equal(5e-60, result.SquareMeters, Tolerance);
    }

    [Fact(DisplayName = "Area comparison should produce the expected result (left equal to right)")]
    public void AreaComparisonShouldProduceExpectedResultLeftEqualToRight()
    {
        // Given
        Area<double> left = Area<double>.FromSquareMeters(1234.0);
        Area<double> right = Area<double>.FromSquareMeters(1234.0);

        // When / Then
        Assert.Equal(0, Area<double>.Compare(left, right));
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
        Area<double> left = Area<double>.FromSquareMeters(4567.0);
        Area<double> right = Area<double>.FromSquareMeters(1234.0);

        // When / Then
        Assert.Equal(1, Area<double>.Compare(left, right));
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
        Area<double> left = Area<double>.FromSquareMeters(1234.0);
        Area<double> right = Area<double>.FromSquareMeters(4567.0);

        // When / Then
        Assert.Equal(-1, Area<double>.Compare(left, right));
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
        // Given
        Area<BigDecimal> left = Area<BigDecimal>.FromSquareKilometers(2.0);
        Area<BigDecimal> right = Area<BigDecimal>.FromSquareMeters(2000000.0);

        // When / Then
        Assert.True(Area<BigDecimal>.Equals(left, right));
        Assert.True(left.Equals(right));
        Assert.True(left.Equals((object)right));
        Assert.True(left == right);
        Assert.False(left != right);
    }

    [Fact(DisplayName = "Area equality should produce the expected result (left not equal to right)")]
    public void AreaEqualityShouldProduceExpectedResultLeftNotEqualToRight()
    {
        // Given
        Area<double> left = Area<double>.FromSquareKilometers(2.0);
        Area<double> right = Area<double>.FromSquareMeters(2500000.0);

        // When / Then
        Assert.False(Area<double>.Equals(left, right));
        Assert.False(left.Equals(right));
        Assert.False(left.Equals((object)right));
        Assert.False(left == right);
        Assert.True(left != right);
    }

    [Fact(DisplayName = "Area.ToString should produce the expected result")]
    public void AreaToStringShouldProduceExpectedResult()
    {
        // Given
        Area<double> a = Area<double>.FromSquareMeters(1000000.0);

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
        Area<double> a = Area<double>.FromSquareMeters(1234.56);

        // When
        string formatted = a.ToString("sqm2", customCulture);

        // Then
        Assert.Equal("1.234,56 m²", formatted);
    }

    [Fact(DisplayName = "Area property conversions should be consistent")]
    public void AreaPropertyConversionsShouldBeConsistent()
    {
        // Given
        Area<double> a = Area<double>.FromSquareMeters(1.0);

        // Then - verify SI unit conversions are consistent
        Assert.Equal(1.0, a.SquareMeters, Tolerance);
        Assert.Equal(1e60, a.SquareQuectoMeters, Tolerance);
        Assert.Equal(1e-6, a.SquareKiloMeters, Tolerance);
        Assert.Equal(100.0, a.SquareDeciMeters, Tolerance);
        Assert.Equal(10000.0, a.SquareCentiMeters, Tolerance);
        Assert.Equal(1000000.0, a.SquareMilliMeters, Tolerance);
    }

    [Fact(DisplayName = "Area imperial unit conversions should be accurate")]
    public void AreaImperialUnitConversionsShouldBeAccurate()
    {
        // Given - 1 square meter
        Area<double> a = Area<double>.FromSquareMeters(1.0);

        // Then - verify imperial conversions
        Assert.Equal(1550.0031000062, a.SquareInches, 1e-6);
        Assert.Equal(10.76391041671, a.SquareFeet, 1e-6);
        Assert.Equal(1.1959900463011, a.SquareYards, 1e-6);
    }

    [Fact(DisplayName = "Area round-trip conversions should be accurate")]
    public void AreaRoundTripConversionsShouldBeAccurate()
    {
        // Given
        double originalValue = 123.456;

        // When - convert from square meters and back
        Area<double> a = Area<double>.FromSquareMeters(originalValue);

        // Then
        Assert.Equal(originalValue, a.SquareMeters, 1e-10);
    }

    [Fact(DisplayName = "Area from square kilometers to square meters should be accurate")]
    public void AreaFromSquareKilometersToSquareMetersShouldBeAccurate()
    {
        // Given
        Area<double> a = Area<double>.FromSquareKilometers(1.0);

        // Then
        Assert.Equal(1000000.0, a.SquareMeters, Tolerance);
    }

    [Fact(DisplayName = "Area from square miles to square kilometers should be accurate")]
    public void AreaFromSquareMilesToSquareKilometersShouldBeAccurate()
    {
        // Given
        Area<double> a = Area<double>.FromSquareMiles(1.0);

        // Then - 1 square mile = 2.589988110336 square kilometers
        Assert.Equal(2.589988110336, a.SquareKiloMeters, 1e-6);
    }

    [Fact(DisplayName = "Area GetHashCode should be consistent for equal values")]
    public void AreaGetHashCodeShouldBeConsistentForEqualValues()
    {
        // Given
        Area<double> a1 = Area<double>.FromSquareMeters(100.0);
        Area<double> a2 = Area<double>.FromSquareMeters(100.0);

        // Then
        Assert.Equal(a1.GetHashCode(), a2.GetHashCode());
    }

    [Fact(DisplayName = "Area.Equals with null object should return false")]
    public void AreaEqualsWithNullObjectShouldReturnFalse()
    {
        // Given
        Area<double> a = Area<double>.FromSquareMeters(100.0);

        // Then
        Assert.False(a.Equals(null));
    }

    [Fact(DisplayName = "Area.Equals with different type should return false")]
    public void AreaEqualsWithDifferentTypeShouldReturnFalse()
    {
        // Given
        Area<double> a = Area<double>.FromSquareMeters(100.0);

        // Then
        Assert.False(a.Equals("not an area"));
    }

    [Fact(DisplayName = "Area default ToString should use SquareQuectoMeters")]
    public void AreaDefaultToStringShouldUseSquareQuectoMeters()
    {
        // Given
        Area<double> a = Area<double>.FromSquareQuectometers(123.0);

        // When
        string result = a.ToString();

        // Then
        Assert.Contains("qm²", result);
    }
}
