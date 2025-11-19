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

using System.Globalization;
using OnixLabs.Numerics;

namespace OnixLabs.Units.UnitTests;

public sealed class PressureTests
{
    // IEEE-754 binary floating-point arithmetic causes small discrepancies in calculation, therefore we need a tolerance.
    private const double Tolerance = 1e+42;

    [Fact(DisplayName = "Pressure.Zero should produce the expected result")]
    public void PressureZeroShouldProduceExpectedResult()
    {
        // Given / When
        Pressure<double> pressure = Pressure<double>.Zero;

        // Then
        Assert.Equal(0.0, pressure.QuectoPascals, Tolerance);
    }

    [Theory(DisplayName = "Pressure.FromQuectoPascals should produce the expected QuectoPascals")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1.0)]
    [InlineData(2.5, 2.5)]
    public void PressureFromQuectoPascalsShouldProduceExpectedQuectoPascals(double value, double expectedQuectoPascals)
    {
        // Given / When
        Pressure<double> pressure = Pressure<double>.FromQuectoPascals(value);

        // Then
        Assert.Equal(expectedQuectoPascals, pressure.QuectoPascals, Tolerance);
    }

    [Theory(DisplayName = "Pressure.FromRontoPascals should produce the expected QuectoPascals")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e3)]
    [InlineData(2.5, 2.5e3)]
    public void PressureFromRontoPascalsShouldProduceExpectedQuectoPascals(double value, double expectedQuectoPascals)
    {
        // Given / When
        Pressure<double> pressure = Pressure<double>.FromRontoPascals(value);

        // Then
        Assert.Equal(expectedQuectoPascals, pressure.QuectoPascals, Tolerance);
    }

    [Theory(DisplayName = "Pressure.FromYoctoPascals should produce the expected QuectoPascals")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e6)]
    [InlineData(2.5, 2.5e6)]
    public void PressureFromYoctoPascalsShouldProduceExpectedQuectoPascals(double value, double expectedQuectoPascals)
    {
        // Given / When
        Pressure<double> pressure = Pressure<double>.FromYoctoPascals(value);

        // Then
        Assert.Equal(expectedQuectoPascals, pressure.QuectoPascals, Tolerance);
    }

    [Theory(DisplayName = "Pressure.FromZeptoPascals should produce the expected QuectoPascals")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e9)]
    [InlineData(2.5, 2.5e9)]
    public void PressureFromZeptoPascalsShouldProduceExpectedQuectoPascals(double value, double expectedQuectoPascals)
    {
        // Given / When
        Pressure<double> pressure = Pressure<double>.FromZeptoPascals(value);

        // Then
        Assert.Equal(expectedQuectoPascals, pressure.QuectoPascals, Tolerance);
    }

    [Theory(DisplayName = "Pressure.FromAttoPascals should produce the expected QuectoPascals")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e12)]
    [InlineData(2.5, 2.5e12)]
    public void PressureFromAttoPascalsShouldProduceExpectedQuectoPascals(double value, double expectedQuectoPascals)
    {
        // Given / When
        Pressure<double> pressure = Pressure<double>.FromAttoPascals(value);

        // Then
        Assert.Equal(expectedQuectoPascals, pressure.QuectoPascals, Tolerance);
    }

    [Theory(DisplayName = "Pressure.FromFemtoPascals should produce the expected QuectoPascals")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e15)]
    [InlineData(2.5, 2.5e15)]
    public void PressureFromFemtoPascalsShouldProduceExpectedQuectoPascals(double value, double expectedQuectoPascals)
    {
        // Given / When
        Pressure<double> pressure = Pressure<double>.FromFemtoPascals(value);

        // Then
        Assert.Equal(expectedQuectoPascals, pressure.QuectoPascals, Tolerance);
    }

    [Theory(DisplayName = "Pressure.FromPicoPascals should produce the expected QuectoPascals")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e18)]
    [InlineData(2.5, 2.5e18)]
    public void PressureFromPicoPascalsShouldProduceExpectedQuectoPascals(double value, double expectedQuectoPascals)
    {
        // Given / When
        Pressure<double> pressure = Pressure<double>.FromPicoPascals(value);

        // Then
        Assert.Equal(expectedQuectoPascals, pressure.QuectoPascals, Tolerance);
    }

    [Theory(DisplayName = "Pressure.FromNanoPascals should produce the expected QuectoPascals")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e21)]
    [InlineData(2.5, 2.5e21)]
    public void PressureFromNanoPascalsShouldProduceExpectedQuectoPascals(double value, double expectedQuectoPascals)
    {
        // Given / When
        Pressure<double> pressure = Pressure<double>.FromNanoPascals(value);

        // Then
        Assert.Equal(expectedQuectoPascals, pressure.QuectoPascals, Tolerance);
    }

    [Theory(DisplayName = "Pressure.FromMicroPascals should produce the expected QuectoPascals")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e24)]
    [InlineData(2.5, 2.5e24)]
    public void PressureFromMicroPascalsShouldProduceExpectedQuectoPascals(double value, double expectedQuectoPascals)
    {
        // Given / When
        Pressure<double> pressure = Pressure<double>.FromMicroPascals(value);

        // Then
        Assert.Equal(expectedQuectoPascals, pressure.QuectoPascals, Tolerance);
    }

    [Theory(DisplayName = "Pressure.FromMilliPascals should produce the expected QuectoPascals")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e27)]
    [InlineData(2.5, 2.5e27)]
    public void PressureFromMilliPascalsShouldProduceExpectedQuectoPascals(double value, double expectedQuectoPascals)
    {
        // Given / When
        Pressure<double> pressure = Pressure<double>.FromMilliPascals(value);

        // Then
        Assert.Equal(expectedQuectoPascals, pressure.QuectoPascals, Tolerance);
    }

    [Theory(DisplayName = "Pressure.FromCentiPascals should produce the expected QuectoPascals")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e28)]
    [InlineData(2.5, 2.5e28)]
    public void PressureFromCentiPascalsShouldProduceExpectedQuectoPascals(double value, double expectedQuectoPascals)
    {
        // Given / When
        Pressure<double> pressure = Pressure<double>.FromCentiPascals(value);

        // Then
        Assert.Equal(expectedQuectoPascals, pressure.QuectoPascals, Tolerance);
    }

    [Theory(DisplayName = "Pressure.FromDeciPascals should produce the expected QuectoPascals")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e29)]
    [InlineData(2.5, 2.5e29)]
    public void PressureFromDeciPascalsShouldProduceExpectedQuectoPascals(double value, double expectedQuectoPascals)
    {
        // Given / When
        Pressure<double> pressure = Pressure<double>.FromDeciPascals(value);

        // Then
        Assert.Equal(expectedQuectoPascals, pressure.QuectoPascals, Tolerance);
    }

    [Theory(DisplayName = "Pressure.FromPascals should produce the expected QuectoPascals")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e30)]
    [InlineData(2.5, 2.5e30)]
    public void PressureFromPascalsShouldProduceExpectedQuectoPascals(double value, double expectedQuectoPascals)
    {
        // Given / When
        Pressure<double> pressure = Pressure<double>.FromPascals(value);

        // Then
        Assert.Equal(expectedQuectoPascals, pressure.QuectoPascals, Tolerance);
    }

    [Theory(DisplayName = "Pressure.FromDecaPascals should produce the expected QuectoPascals")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e31)]
    [InlineData(2.5, 2.5e31)]
    public void PressureFromDecaPascalsShouldProduceExpectedQuectoPascals(double value, double expectedQuectoPascals)
    {
        // Given / When
        Pressure<double> pressure = Pressure<double>.FromDecaPascals(value);

        // Then
        Assert.Equal(expectedQuectoPascals, pressure.QuectoPascals, Tolerance);
    }

    [Theory(DisplayName = "Pressure.FromHectoPascals should produce the expected QuectoPascals")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e32)]
    [InlineData(2.5, 2.5e32)]
    public void PressureFromHectoPascalsShouldProduceExpectedQuectoPascals(double value, double expectedQuectoPascals)
    {
        // Given / When
        Pressure<double> pressure = Pressure<double>.FromHectoPascals(value);

        // Then
        Assert.Equal(expectedQuectoPascals, pressure.QuectoPascals, Tolerance);
    }

    [Theory(DisplayName = "Pressure.FromKiloPascals should produce the expected QuectoPascals")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e33)]
    [InlineData(2.5, 2.5e33)]
    public void PressureFromKiloPascalsShouldProduceExpectedQuectoPascals(double value, double expectedQuectoPascals)
    {
        // Given / When
        Pressure<double> pressure = Pressure<double>.FromKiloPascals(value);

        // Then
        Assert.Equal(expectedQuectoPascals, pressure.QuectoPascals, Tolerance);
    }

    [Theory(DisplayName = "Pressure.FromMegaPascals should produce the expected QuectoPascals")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e36)]
    [InlineData(2.5, 2.5e36)]
    public void PressureFromMegaPascalsShouldProduceExpectedQuectoPascals(double value, double expectedQuectoPascals)
    {
        // Given / When
        Pressure<double> pressure = Pressure<double>.FromMegaPascals(value);

        // Then
        Assert.Equal(expectedQuectoPascals, pressure.QuectoPascals, Tolerance);
    }

    [Theory(DisplayName = "Pressure.FromGigaPascals should produce the expected QuectoPascals")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e39)]
    [InlineData(2.5, 2.5e39)]
    public void PressureFromGigaPascalsShouldProduceExpectedQuectoPascals(double value, double expectedQuectoPascals)
    {
        // Given / When
        Pressure<double> pressure = Pressure<double>.FromGigaPascals(value);

        // Then
        Assert.Equal(expectedQuectoPascals, pressure.QuectoPascals, Tolerance);
    }

    [Theory(DisplayName = "Pressure.FromTeraPascals should produce the expected QuectoPascals")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e42)]
    [InlineData(2.5, 2.5e42)]
    public void PressureFromTeraPascalsShouldProduceExpectedQuectoPascals(double value, double expectedQuectoPascals)
    {
        // Given / When
        Pressure<double> pressure = Pressure<double>.FromTeraPascals(value);

        // Then
        Assert.Equal(expectedQuectoPascals, pressure.QuectoPascals, Tolerance);
    }

    [Theory(DisplayName = "Pressure.FromPetaPascals should produce the expected QuectoPascals")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e45)]
    [InlineData(2.5, 2.5e45)]
    public void PressureFromPetaPascalsShouldProduceExpectedQuectoPascals(double value, double expectedQuectoPascals)
    {
        // Given / When
        Pressure<double> pressure = Pressure<double>.FromPetaPascals(value);

        // Then
        Assert.Equal(expectedQuectoPascals, pressure.QuectoPascals, Tolerance);
    }

    [Theory(DisplayName = "Pressure.FromExaPascals should produce the expected QuectoPascals")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e48)]
    [InlineData(2.5, 2.5e48)]
    public void PressureFromExaPascalsShouldProduceExpectedQuectoPascals(double value, double expectedQuectoPascals)
    {
        // Given / When
        Pressure<double> pressure = Pressure<double>.FromExaPascals(value);

        // Then
        Assert.Equal(expectedQuectoPascals, pressure.QuectoPascals, Tolerance);
    }

    [Theory(DisplayName = "Pressure.FromZettaPascals should produce the expected QuectoPascals")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e51)]
    [InlineData(2.5, 2.5e51)]
    public void PressureFromZettaPascalsShouldProduceExpectedQuectoPascals(double value, double expectedQuectoPascals)
    {
        // Given / When
        Pressure<double> pressure = Pressure<double>.FromZettaPascals(value);

        // Then
        Assert.Equal(expectedQuectoPascals, pressure.QuectoPascals, Tolerance);
    }

    [Theory(DisplayName = "Pressure.FromYottaPascals should produce the expected QuectoPascals")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e54)]
    [InlineData(2.5, 2.5e54)]
    public void PressureFromYottaPascalsShouldProduceExpectedQuectoPascals(double value, double expectedQuectoPascals)
    {
        // Given / When
        Pressure<double> pressure = Pressure<double>.FromYottaPascals(value);

        // Then
        Assert.Equal(expectedQuectoPascals, pressure.QuectoPascals, Tolerance);
    }

    [Theory(DisplayName = "Pressure.FromRonnaPascals should produce the expected QuectoPascals")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e57)]
    [InlineData(2.5, 2.5e57)]
    public void PressureFromRonnaPascalsShouldProduceExpectedQuectoPascals(double value, double expectedQuectoPascals)
    {
        // Given / When
        Pressure<double> pressure = Pressure<double>.FromRonnaPascals(value);

        // Then
        Assert.Equal(expectedQuectoPascals, pressure.QuectoPascals, Tolerance);
    }

    [Theory(DisplayName = "Pressure.FromQuettaPascals should produce the expected QuectoPascals")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e60)]
    [InlineData(2.5, 2.5e60)]
    public void PressureFromQuettaPascalsShouldProduceExpectedQuectoPascals(double value, double expectedQuectoPascals)
    {
        // Given / When
        Pressure<double> pressure = Pressure<double>.FromQuettaPascals(value);

        // Then
        Assert.Equal(expectedQuectoPascals, pressure.QuectoPascals, Tolerance);
    }

    [Theory(DisplayName = "Pressure.FromBars should produce the expected QuectoPascals")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e+35)]
    [InlineData(2.5, 2.5e+35)]
    public void PressureFromBarsShouldProduceExpectedQuectoPascals(double value, double expectedQuectoPascals)
    {
        // Given / When
        Pressure<double> pressure = Pressure<double>.FromBars(value);

        // Then
        Assert.Equal(expectedQuectoPascals, pressure.QuectoPascals, Tolerance);
    }

    [Theory(DisplayName = "Pressure.FromMillibars should produce the expected QuectoPascals")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e+32)]
    [InlineData(2.5, 2.5e+32)]
    public void PressureFromMillibarsShouldProduceExpectedQuectoPascals(double value, double expectedQuectoPascals)
    {
        // Given / When
        Pressure<double> pressure = Pressure<double>.FromMillibars(value);

        // Then
        Assert.Equal(expectedQuectoPascals, pressure.QuectoPascals, Tolerance);
    }

    [Theory(DisplayName = "Pressure.FromAtmospheres should produce the expected QuectoPascals")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1.01325e+35)]
    [InlineData(2.5, 2.533125e+35)]
    public void PressureFromAtmospheresShouldProduceExpectedQuectoPascals(double value, double expectedQuectoPascals)
    {
        // Given / When
        Pressure<double> pressure = Pressure<double>.FromAtmospheres(value);

        // Then
        Assert.Equal(expectedQuectoPascals, pressure.QuectoPascals, Tolerance);
    }

    [Theory(DisplayName = "Pressure.FromTechnicalAtmospheres should produce the expected QuectoPascals")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 9.80665e+34)]
    [InlineData(2.5, 2.4516625e+35)]
    public void PressureFromTechnicalAtmospheresShouldProduceExpectedQuectoPascals(double value, double expectedQuectoPascals)
    {
        // Given / When
        Pressure<double> pressure = Pressure<double>.FromTechnicalAtmospheres(value);

        // Then
        Assert.Equal(expectedQuectoPascals, pressure.QuectoPascals, Tolerance);
    }

    [Theory(DisplayName = "Pressure.FromTorr should produce the expected QuectoPascals")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1.33322368421053e+32)]
    [InlineData(2.5, 3.33305921052632e+32)]
    public void PressureFromTorrShouldProduceExpectedQuectoPascals(double value, double expectedQuectoPascals)
    {
        // Given / When
        Pressure<double> pressure = Pressure<double>.FromTorr(value);

        // Then
        Assert.Equal(expectedQuectoPascals, pressure.QuectoPascals, Tolerance);
    }

    [Theory(DisplayName = "Pressure.FromMillimetersOfMercury should produce the expected QuectoPascals")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1.33322387415e+32)]
    [InlineData(2.5, 3.333059685375e+32)]
    public void PressureFromMillimetersOfMercuryShouldProduceExpectedQuectoPascals(double value, double expectedQuectoPascals)
    {
        // Given / When
        Pressure<double> pressure = Pressure<double>.FromMillimetersOfMercury(value);

        // Then
        Assert.Equal(expectedQuectoPascals, pressure.QuectoPascals, Tolerance);
    }

    [Theory(DisplayName = "Pressure.FromInchesOfMercury should produce the expected QuectoPascals")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 3.386389e+33)]
    [InlineData(2.5, 8.4659725e+33)]
    public void PressureFromInchesOfMercuryShouldProduceExpectedQuectoPascals(double value, double expectedQuectoPascals)
    {
        // Given / When
        Pressure<double> pressure = Pressure<double>.FromInchesOfMercury(value);

        // Then
        Assert.Equal(expectedQuectoPascals, pressure.QuectoPascals, Tolerance);
    }

    [Theory(DisplayName = "Pressure.FromPoundsPerSquareInch should produce the expected QuectoPascals")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 6.894757293168e+33)]
    [InlineData(2.5, 1.723689323292e+34)]
    public void PressureFromPoundsPerSquareInchShouldProduceExpectedQuectoPascals(double value, double expectedQuectoPascals)
    {
        // Given / When
        Pressure<double> pressure = Pressure<double>.FromPoundsPerSquareInch(value);

        // Then
        Assert.Equal(expectedQuectoPascals, pressure.QuectoPascals, Tolerance);
    }

    [Theory(DisplayName = "Pressure.FromPoundsPerSquareFoot should produce the expected QuectoPascals")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 4.788025898e+31)]
    [InlineData(2.5, 1.1970064745e+32)]
    public void PressureFromPoundsPerSquareFootShouldProduceExpectedQuectoPascals(double value, double expectedQuectoPascals)
    {
        // Given / When
        Pressure<double> pressure = Pressure<double>.FromPoundsPerSquareFoot(value);

        // Then
        Assert.Equal(expectedQuectoPascals, pressure.QuectoPascals, Tolerance);
    }

    [Theory(DisplayName = "Pressure.FromBarye should produce the expected QuectoPascals")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e+29)]
    [InlineData(2.5, 2.5e+29)]
    public void PressureFromBaryeShouldProduceExpectedQuectoPascals(double value, double expectedQuectoPascals)
    {
        // Given / When
        Pressure<double> pressure = Pressure<double>.FromBarye(value);

        // Then
        Assert.Equal(expectedQuectoPascals, pressure.QuectoPascals, Tolerance);
    }

    [Theory(DisplayName = "Pressure.FromMillimetersOfWaterColumn should produce the expected QuectoPascals")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 9.80665e+30)]
    [InlineData(2.5, 2.4516625e+31)]
    public void PressureFromMillimetersOfWaterColumnShouldProduceExpectedQuectoPascals(double value, double expectedQuectoPascals)
    {
        // Given / When
        Pressure<double> pressure = Pressure<double>.FromMillimetersOfWaterColumn(value);

        // Then
        Assert.Equal(expectedQuectoPascals, pressure.QuectoPascals, Tolerance);
    }

    [Theory(DisplayName = "Pressure.FromInchesOfWaterColumn should produce the expected QuectoPascals")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 2.4908891e+32)]
    [InlineData(2.5, 6.22722275e+32)]
    public void PressureFromInchesOfWaterColumnShouldProduceExpectedQuectoPascals(double value, double expectedQuectoPascals)
    {
        // Given / When
        Pressure<double> pressure = Pressure<double>.FromInchesOfWaterColumn(value);

        // Then
        Assert.Equal(expectedQuectoPascals, pressure.QuectoPascals, Tolerance);
    }

    [Fact(DisplayName = "Pressure.Add should produce the expected result")]
    public void PressureAddShouldProduceExpectedValue()
    {
        // Given
        Pressure<double> left = Pressure<double>.FromPascals(1500.0);
        Pressure<double> right = Pressure<double>.FromPascals(500.0);

        // When
        Pressure<double> result = left.Add(right);

        // Then
        Assert.Equal(2000.0, result.Pascals, Tolerance);
    }

    [Fact(DisplayName = "Pressure.Subtract should produce the expected result")]
    public void PressureSubtractShouldProduceExpectedValue()
    {
        // Given
        Pressure<double> left = Pressure<double>.FromPascals(1500.0);
        Pressure<double> right = Pressure<double>.FromPascals(500.0);

        // When
        Pressure<double> result = left.Subtract(right);

        // Then
        Assert.Equal(1000.0, result.Pascals, Tolerance);
    }

    [Fact(DisplayName = "Pressure.Multiply should produce the expected result")]
    public void PressureMultiplyShouldProduceExpectedValue()
    {
        // Given
        Pressure<double> left = Pressure<double>.FromPascals(10.0); // 1e31 qPa
        Pressure<double> right = Pressure<double>.FromPascals(3.0); // 3e30 qPa

        // When
        Pressure<double> result = left.Multiply(right); // 1e31 * 3e30 = 3e61 qPa

        // Then
        Assert.Equal(1e31, left.QuectoPascals, Tolerance);
        Assert.Equal(3e30, right.QuectoPascals, Tolerance);
        Assert.Equal(3e61, result.QuectoPascals, Tolerance);
        Assert.Equal(3e31, result.Pascals, Tolerance);
    }

    [Fact(DisplayName = "Pressure.Divide should produce the expected result")]
    public void PressureDivideShouldProduceExpectedValue()
    {
        // Given
        Pressure<double> left = Pressure<double>.FromPascals(100.0); // 1e32 qPa
        Pressure<double> right = Pressure<double>.FromPascals(20.0); // 2e31 qPa

        // When
        Pressure<double> result = left.Divide(right); // 1e32 / 2e31 = 5 qPa

        // Then
        Assert.Equal(5.0, result.QuectoPascals, Tolerance);
        Assert.Equal(5e-30, result.Pascals, Tolerance);
    }

    [Fact(DisplayName = "Pressure comparison should produce the expected result (left equal to right)")]
    public void PressureComparisonShouldProduceExpectedResultLeftEqualToRight()
    {
        // Given
        Pressure<double> left = Pressure<double>.FromPascals(1234.0);
        Pressure<double> right = Pressure<double>.FromPascals(1234.0);

        // When / Then
        Assert.Equal(0, Pressure<double>.Compare(left, right));
        Assert.Equal(0, left.CompareTo(right));
        Assert.Equal(0, left.CompareTo((object)right));
        Assert.False(left > right);
        Assert.True(left >= right);
        Assert.False(left < right);
        Assert.True(left <= right);
    }

    [Fact(DisplayName = "Pressure comparison should produce the expected result (left greater than right)")]
    public void PressureComparisonShouldProduceExpectedLeftGreaterThanRight()
    {
        // Given
        Pressure<double> left = Pressure<double>.FromPascals(4567.0);
        Pressure<double> right = Pressure<double>.FromPascals(1234.0);

        // When / Then
        Assert.Equal(1, Pressure<double>.Compare(left, right));
        Assert.Equal(1, left.CompareTo(right));
        Assert.Equal(1, left.CompareTo((object)right));
        Assert.True(left > right);
        Assert.True(left >= right);
        Assert.False(left < right);
        Assert.False(left <= right);
    }

    [Fact(DisplayName = "Pressure comparison should produce the expected result (left less than right)")]
    public void PressureComparisonShouldProduceExpectedLeftLessThanRight()
    {
        // Given
        Pressure<double> left = Pressure<double>.FromPascals(1234.0);
        Pressure<double> right = Pressure<double>.FromPascals(4567.0);

        // When / Then
        Assert.Equal(-1, Pressure<double>.Compare(left, right));
        Assert.Equal(-1, left.CompareTo(right));
        Assert.Equal(-1, left.CompareTo((object)right));
        Assert.False(left > right);
        Assert.False(left >= right);
        Assert.True(left < right);
        Assert.True(left <= right);
    }

    [Fact(DisplayName = "Pressure equality should produce the expected result (left equal to right)")]
    public void PressureEqualityShouldProduceExpectedResultLeftEqualToRight()
    {
        // Given
        Pressure<BigDecimal> left = Pressure<BigDecimal>.FromKiloPascals(2.0);
        Pressure<BigDecimal> right = Pressure<BigDecimal>.FromPascals(2000.0);

        // When / Then
        Assert.True(Pressure<BigDecimal>.Equals(left, right));
        Assert.True(left.Equals(right));
        Assert.True(left.Equals((object)right));
        Assert.True(left == right);
        Assert.False(left != right);
    }

    [Fact(DisplayName = "Pressure equality should produce the expected result (left not equal to right)")]
    public void PressureEqualityShouldProduceExpectedResultLeftNotEqualToRight()
    {
        // Given
        Pressure<double> left = Pressure<double>.FromKiloPascals(2.0);
        Pressure<double> right = Pressure<double>.FromPascals(2500.0);

        // When / Then
        Assert.False(Pressure<double>.Equals(left, right));
        Assert.False(left.Equals(right));
        Assert.False(left.Equals((object)right));
        Assert.False(left == right);
        Assert.True(left != right);
    }

    [Fact(DisplayName = "Pressure.ToString should produce the expected result")]
    public void PressureToStringShouldProduceExpectedResult()
    {
        // Given
        Pressure<double> pressure = Pressure<double>.FromPascals(101_325.0); // Approximately one atmosphere

        // When / Then
        Assert.Equal("101,325.000 Pa", $"{pressure:Pa3}");
        Assert.Equal("101.325 kPa", $"{pressure:kPa3}");
        Assert.Equal("1.013 bar", $"{pressure:bar3}");
        Assert.Equal("1.000 atm", $"{pressure:atm3}");
        Assert.Equal("14.696 psi", $"{pressure:psi3}");
        Assert.Equal("760.000 mmHg", $"{pressure:mmHg3}");
    }

    [Fact(DisplayName = "Pressure.ToString should honor custom culture separators")]
    public void PressureToStringShouldHonorCustomCulture()
    {
        // Given
        CultureInfo customCulture = new("de-DE");
        Pressure<double> pressure = Pressure<double>.FromPascals(1234.56);

        // When
        string formatted = pressure.ToString("Pa2", customCulture);

        // Then
        // German uses '.' for thousands and ',' for decimals.
        Assert.Equal("1.234,56 Pa", formatted);
    }
}
