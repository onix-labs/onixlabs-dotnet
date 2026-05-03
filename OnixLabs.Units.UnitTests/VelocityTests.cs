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

public sealed class VelocityTests
{
    // IEEE-754 binary floating-point arithmetic causes small discrepancies in calculation, therefore we need a tolerance.
    private const double Tolerance = 1e+42;

    [Fact(DisplayName = "Velocity.Zero should produce the expected result")]
    public void VelocityZeroShouldProduceExpectedResult()
    {
        // Given / When
        Velocity<double> velocity = Velocity<double>.Zero;

        // Then
        Assert.Equal(0.0, velocity.QuectoMetersPerSecond, Tolerance);
    }

    [Theory(DisplayName = "Velocity.FromQuectometersPerSecond should produce the expected QuectoMetersPerSecond")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1.0)]
    [InlineData(2.5, 2.5)]
    public void VelocityFromQuectometersPerSecondShouldProduceExpectedQuectoMetersPerSecond(double value, double expected)
    {
        Velocity<double> v = Velocity<double>.FromQuectometersPerSecond(value);
        Assert.Equal(expected, v.QuectoMetersPerSecond, Tolerance);
    }

    [Theory(DisplayName = "Velocity.FromRontometersPerSecond should produce the expected QuectoMetersPerSecond")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e3)]
    [InlineData(2.5, 2.5e3)]
    public void VelocityFromRontometersPerSecondShouldProduceExpectedQuectoMetersPerSecond(double value, double expected)
    {
        Velocity<double> v = Velocity<double>.FromRontometersPerSecond(value);
        Assert.Equal(expected, v.QuectoMetersPerSecond, Tolerance);
    }

    [Theory(DisplayName = "Velocity.FromYoctometersPerSecond should produce the expected QuectoMetersPerSecond")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e6)]
    [InlineData(2.5, 2.5e6)]
    public void VelocityFromYoctometersPerSecondShouldProduceExpectedQuectoMetersPerSecond(double value, double expected)
    {
        Velocity<double> v = Velocity<double>.FromYoctometersPerSecond(value);
        Assert.Equal(expected, v.QuectoMetersPerSecond, Tolerance);
    }

    [Theory(DisplayName = "Velocity.FromZeptometersPerSecond should produce the expected QuectoMetersPerSecond")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e9)]
    [InlineData(2.5, 2.5e9)]
    public void VelocityFromZeptometersPerSecondShouldProduceExpectedQuectoMetersPerSecond(double value, double expected)
    {
        Velocity<double> v = Velocity<double>.FromZeptometersPerSecond(value);
        Assert.Equal(expected, v.QuectoMetersPerSecond, Tolerance);
    }

    [Theory(DisplayName = "Velocity.FromAttometersPerSecond should produce the expected QuectoMetersPerSecond")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e12)]
    [InlineData(2.5, 2.5e12)]
    public void VelocityFromAttometersPerSecondShouldProduceExpectedQuectoMetersPerSecond(double value, double expected)
    {
        Velocity<double> v = Velocity<double>.FromAttometersPerSecond(value);
        Assert.Equal(expected, v.QuectoMetersPerSecond, Tolerance);
    }

    [Theory(DisplayName = "Velocity.FromFemtometersPerSecond should produce the expected QuectoMetersPerSecond")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e15)]
    [InlineData(2.5, 2.5e15)]
    public void VelocityFromFemtometersPerSecondShouldProduceExpectedQuectoMetersPerSecond(double value, double expected)
    {
        Velocity<double> v = Velocity<double>.FromFemtometersPerSecond(value);
        Assert.Equal(expected, v.QuectoMetersPerSecond, Tolerance);
    }

    [Theory(DisplayName = "Velocity.FromPicometersPerSecond should produce the expected QuectoMetersPerSecond")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e18)]
    [InlineData(2.5, 2.5e18)]
    public void VelocityFromPicometersPerSecondShouldProduceExpectedQuectoMetersPerSecond(double value, double expected)
    {
        Velocity<double> v = Velocity<double>.FromPicometersPerSecond(value);
        Assert.Equal(expected, v.QuectoMetersPerSecond, Tolerance);
    }

    [Theory(DisplayName = "Velocity.FromNanometersPerSecond should produce the expected QuectoMetersPerSecond")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e21)]
    [InlineData(2.5, 2.5e21)]
    public void VelocityFromNanometersPerSecondShouldProduceExpectedQuectoMetersPerSecond(double value, double expected)
    {
        Velocity<double> v = Velocity<double>.FromNanometersPerSecond(value);
        Assert.Equal(expected, v.QuectoMetersPerSecond, Tolerance);
    }

    [Theory(DisplayName = "Velocity.FromMicrometersPerSecond should produce the expected QuectoMetersPerSecond")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e24)]
    [InlineData(2.5, 2.5e24)]
    public void VelocityFromMicrometersPerSecondShouldProduceExpectedQuectoMetersPerSecond(double value, double expected)
    {
        Velocity<double> v = Velocity<double>.FromMicrometersPerSecond(value);
        Assert.Equal(expected, v.QuectoMetersPerSecond, Tolerance);
    }

    [Theory(DisplayName = "Velocity.FromMillimetersPerSecond should produce the expected QuectoMetersPerSecond")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e27)]
    [InlineData(2.5, 2.5e27)]
    public void VelocityFromMillimetersPerSecondShouldProduceExpectedQuectoMetersPerSecond(double value, double expected)
    {
        Velocity<double> v = Velocity<double>.FromMillimetersPerSecond(value);
        Assert.Equal(expected, v.QuectoMetersPerSecond, Tolerance);
    }

    [Theory(DisplayName = "Velocity.FromCentimetersPerSecond should produce the expected QuectoMetersPerSecond")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e28)]
    [InlineData(2.5, 2.5e28)]
    public void VelocityFromCentimetersPerSecondShouldProduceExpectedQuectoMetersPerSecond(double value, double expected)
    {
        Velocity<double> v = Velocity<double>.FromCentimetersPerSecond(value);
        Assert.Equal(expected, v.QuectoMetersPerSecond, Tolerance);
    }

    [Theory(DisplayName = "Velocity.FromDecimetersPerSecond should produce the expected QuectoMetersPerSecond")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e29)]
    [InlineData(2.5, 2.5e29)]
    public void VelocityFromDecimetersPerSecondShouldProduceExpectedQuectoMetersPerSecond(double value, double expected)
    {
        Velocity<double> v = Velocity<double>.FromDecimetersPerSecond(value);
        Assert.Equal(expected, v.QuectoMetersPerSecond, Tolerance);
    }

    [Theory(DisplayName = "Velocity.FromMetersPerSecond should produce the expected QuectoMetersPerSecond")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e30)]
    [InlineData(2.5, 2.5e30)]
    public void VelocityFromMetersPerSecondShouldProduceExpectedQuectoMetersPerSecond(double value, double expected)
    {
        Velocity<double> v = Velocity<double>.FromMetersPerSecond(value);
        Assert.Equal(expected, v.QuectoMetersPerSecond, Tolerance);
    }

    [Theory(DisplayName = "Velocity.FromDecametersPerSecond should produce the expected QuectoMetersPerSecond")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e31)]
    [InlineData(2.5, 2.5e31)]
    public void VelocityFromDecametersPerSecondShouldProduceExpectedQuectoMetersPerSecond(double value, double expected)
    {
        Velocity<double> v = Velocity<double>.FromDecametersPerSecond(value);
        Assert.Equal(expected, v.QuectoMetersPerSecond, Tolerance);
    }

    [Theory(DisplayName = "Velocity.FromHectometersPerSecond should produce the expected QuectoMetersPerSecond")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e32)]
    [InlineData(2.5, 2.5e32)]
    public void VelocityFromHectometersPerSecondShouldProduceExpectedQuectoMetersPerSecond(double value, double expected)
    {
        Velocity<double> v = Velocity<double>.FromHectometersPerSecond(value);
        Assert.Equal(expected, v.QuectoMetersPerSecond, Tolerance);
    }

    [Theory(DisplayName = "Velocity.FromKilometersPerSecond should produce the expected QuectoMetersPerSecond")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e33)]
    [InlineData(2.5, 2.5e33)]
    public void VelocityFromKilometersPerSecondShouldProduceExpectedQuectoMetersPerSecond(double value, double expected)
    {
        Velocity<double> v = Velocity<double>.FromKilometersPerSecond(value);
        Assert.Equal(expected, v.QuectoMetersPerSecond, Tolerance);
    }

    [Theory(DisplayName = "Velocity.FromMegametersPerSecond should produce the expected QuectoMetersPerSecond")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e36)]
    [InlineData(2.5, 2.5e36)]
    public void VelocityFromMegametersPerSecondShouldProduceExpectedQuectoMetersPerSecond(double value, double expected)
    {
        Velocity<double> v = Velocity<double>.FromMegametersPerSecond(value);
        Assert.Equal(expected, v.QuectoMetersPerSecond, Tolerance);
    }

    [Theory(DisplayName = "Velocity.FromGigametersPerSecond should produce the expected QuectoMetersPerSecond")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e39)]
    [InlineData(2.5, 2.5e39)]
    public void VelocityFromGigametersPerSecondShouldProduceExpectedQuectoMetersPerSecond(double value, double expected)
    {
        Velocity<double> v = Velocity<double>.FromGigametersPerSecond(value);
        Assert.Equal(expected, v.QuectoMetersPerSecond, Tolerance);
    }

    [Theory(DisplayName = "Velocity.FromTerametersPerSecond should produce the expected QuectoMetersPerSecond")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e42)]
    [InlineData(2.5, 2.5e42)]
    public void VelocityFromTerametersPerSecondShouldProduceExpectedQuectoMetersPerSecond(double value, double expected)
    {
        Velocity<double> v = Velocity<double>.FromTerametersPerSecond(value);
        Assert.Equal(expected, v.QuectoMetersPerSecond, Tolerance);
    }

    [Theory(DisplayName = "Velocity.FromPetametersPerSecond should produce the expected QuectoMetersPerSecond")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e45)]
    [InlineData(2.5, 2.5e45)]
    public void VelocityFromPetametersPerSecondShouldProduceExpectedQuectoMetersPerSecond(double value, double expected)
    {
        Velocity<double> v = Velocity<double>.FromPetametersPerSecond(value);
        Assert.Equal(expected, v.QuectoMetersPerSecond, Tolerance);
    }

    [Theory(DisplayName = "Velocity.FromExametersPerSecond should produce the expected QuectoMetersPerSecond")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e48)]
    [InlineData(2.5, 2.5e48)]
    public void VelocityFromExametersPerSecondShouldProduceExpectedQuectoMetersPerSecond(double value, double expected)
    {
        Velocity<double> v = Velocity<double>.FromExametersPerSecond(value);
        Assert.Equal(expected, v.QuectoMetersPerSecond, Tolerance);
    }

    [Theory(DisplayName = "Velocity.FromZettametersPerSecond should produce the expected QuectoMetersPerSecond")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e51)]
    [InlineData(2.5, 2.5e51)]
    public void VelocityFromZettametersPerSecondShouldProduceExpectedQuectoMetersPerSecond(double value, double expected)
    {
        Velocity<double> v = Velocity<double>.FromZettametersPerSecond(value);
        Assert.Equal(expected, v.QuectoMetersPerSecond, Tolerance);
    }

    [Theory(DisplayName = "Velocity.FromYottametersPerSecond should produce the expected QuectoMetersPerSecond")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e54)]
    [InlineData(2.5, 2.5e54)]
    public void VelocityFromYottametersPerSecondShouldProduceExpectedQuectoMetersPerSecond(double value, double expected)
    {
        Velocity<double> v = Velocity<double>.FromYottametersPerSecond(value);
        Assert.Equal(expected, v.QuectoMetersPerSecond, Tolerance);
    }

    [Theory(DisplayName = "Velocity.FromRonnametersPerSecond should produce the expected QuectoMetersPerSecond")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e57)]
    [InlineData(2.5, 2.5e57)]
    public void VelocityFromRonnametersPerSecondShouldProduceExpectedQuectoMetersPerSecond(double value, double expected)
    {
        Velocity<double> v = Velocity<double>.FromRonnametersPerSecond(value);
        Assert.Equal(expected, v.QuectoMetersPerSecond, Tolerance);
    }

    [Theory(DisplayName = "Velocity.FromQuettametersPerSecond should produce the expected QuectoMetersPerSecond")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e60)]
    [InlineData(2.5, 2.5e60)]
    public void VelocityFromQuettametersPerSecondShouldProduceExpectedQuectoMetersPerSecond(double value, double expected)
    {
        Velocity<double> v = Velocity<double>.FromQuettametersPerSecond(value);
        Assert.Equal(expected, v.QuectoMetersPerSecond, Tolerance);
    }

    [Theory(DisplayName = "Velocity.FromKilometersPerHour should produce the expected QuectoMetersPerSecond")]
    [InlineData(0.0)]
    [InlineData(1.0)]
    [InlineData(3.6)]    // = 1 m/s
    [InlineData(100.0)]  // ≈ 27.778 m/s
    public void VelocityFromKilometersPerHourShouldProduceExpectedQuectoMetersPerSecond(double value)
    {
        // Given: 1 km/h = 1000/3600 m/s, so qm/s = value × 1e30 / 3.6
        double expected = value * 1e30 / 3.6;

        // When
        Velocity<double> v = Velocity<double>.FromKilometersPerHour(value);

        // Then
        Assert.Equal(expected, v.QuectoMetersPerSecond, Tolerance);
    }

    [Theory(DisplayName = "Velocity.FromMilesPerHour should produce the expected QuectoMetersPerSecond")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 4.4704e29)]
    [InlineData(60.0, 2.68224e31)]
    public void VelocityFromMilesPerHourShouldProduceExpectedQuectoMetersPerSecond(double value, double expected)
    {
        Velocity<double> v = Velocity<double>.FromMilesPerHour(value);
        Assert.Equal(expected, v.QuectoMetersPerSecond, Tolerance);
    }

    [Theory(DisplayName = "Velocity.FromFeetPerSecond should produce the expected QuectoMetersPerSecond")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 3.048e29)]
    [InlineData(2.0, 6.096e29)]
    public void VelocityFromFeetPerSecondShouldProduceExpectedQuectoMetersPerSecond(double value, double expected)
    {
        Velocity<double> v = Velocity<double>.FromFeetPerSecond(value);
        Assert.Equal(expected, v.QuectoMetersPerSecond, Tolerance);
    }

    [Theory(DisplayName = "Velocity.FromKnots should produce the expected QuectoMetersPerSecond")]
    [InlineData(0.0)]
    [InlineData(1.0)]
    [InlineData(100.0)]
    public void VelocityFromKnotsShouldProduceExpectedQuectoMetersPerSecond(double value)
    {
        // Given: 1 knot = 1852/3600 m/s, so qm/s = value × 1852e30 / 3600
        double expected = value * 1852e30 / 3600.0;

        // When
        Velocity<double> v = Velocity<double>.FromKnots(value);

        // Then
        Assert.Equal(expected, v.QuectoMetersPerSecond, Tolerance);
    }

    [Theory(DisplayName = "Velocity.FromInchesPerSecond should produce the expected QuectoMetersPerSecond")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 2.54e28)]
    [InlineData(12.0, 3.048e29)] // 12 in/s = 1 ft/s
    public void VelocityFromInchesPerSecondShouldProduceExpectedQuectoMetersPerSecond(double value, double expected)
    {
        Velocity<double> v = Velocity<double>.FromInchesPerSecond(value);
        Assert.Equal(expected, v.QuectoMetersPerSecond, Tolerance);
    }

    [Theory(DisplayName = "Velocity.FromMach should produce the expected QuectoMetersPerSecond")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 3.4029e32)]
    [InlineData(2.0, 6.8058e32)]
    public void VelocityFromMachShouldProduceExpectedQuectoMetersPerSecond(double value, double expected)
    {
        Velocity<double> v = Velocity<double>.FromMach(value);
        Assert.Equal(expected, v.QuectoMetersPerSecond, Tolerance);
    }

    [Theory(DisplayName = "Velocity.FromSpeedOfLight should produce the expected QuectoMetersPerSecond")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 2.99792458e38)]
    [InlineData(0.5, 1.49896229e38)]
    public void VelocityFromSpeedOfLightShouldProduceExpectedQuectoMetersPerSecond(double value, double expected)
    {
        Velocity<double> v = Velocity<double>.FromSpeedOfLight(value);
        Assert.Equal(expected, v.QuectoMetersPerSecond, Tolerance);
    }

    [Fact(DisplayName = "Velocity non-SI conversions should roundtrip through MetersPerSecond")]
    public void VelocityNonSiConversionsShouldRoundtripThroughMetersPerSecond()
    {
        // 1 m/s = 3.6 km/h
        Velocity<double> oneMps = Velocity<double>.FromMetersPerSecond(1.0);
        Assert.Equal(3.6, oneMps.KilometersPerHour, 1e-9);

        // 1 mph = 0.44704 m/s
        Velocity<double> oneMph = Velocity<double>.FromMilesPerHour(1.0);
        Assert.Equal(0.44704, oneMph.MetersPerSecond, 1e-9);

        // 1 knot = 1.852 km/h
        Velocity<double> oneKnot = Velocity<double>.FromKnots(1.0);
        Assert.Equal(1.852, oneKnot.KilometersPerHour, 1e-9);

        // c roundtrip
        Velocity<double> light = Velocity<double>.FromMetersPerSecond(299792458.0);
        Assert.Equal(1.0, light.SpeedOfLight, 1e-9);
    }

    [Fact(DisplayName = "Velocity.Add should produce the expected result")]
    public void VelocityAddShouldProduceExpectedValue()
    {
        // Given
        Velocity<double> left = Velocity<double>.FromKilometersPerSecond(1.5);
        Velocity<double> right = Velocity<double>.FromKilometersPerSecond(0.5);

        // When
        Velocity<double> result = left.Add(right);

        // Then
        Assert.Equal(2.0, result.KiloMetersPerSecond, Tolerance);
    }

    [Fact(DisplayName = "Velocity.Subtract should produce the expected result")]
    public void VelocitySubtractShouldProduceExpectedValue()
    {
        // Given
        Velocity<double> left = Velocity<double>.FromKilometersPerSecond(1.5);
        Velocity<double> right = Velocity<double>.FromKilometersPerSecond(0.4);

        // When
        Velocity<double> result = left.Subtract(right);

        // Then
        Assert.Equal(1.1, result.KiloMetersPerSecond, Tolerance);
    }

    [Fact(DisplayName = "Velocity.Multiply should produce the expected result")]
    public void VelocityMultiplyShouldProduceExpectedValue()
    {
        // Given
        Velocity<double> left = Velocity<double>.FromMetersPerSecond(10.0);  // 1e31 qm/s
        Velocity<double> right = Velocity<double>.FromMetersPerSecond(3.0);  // 3e30 qm/s

        // When
        Velocity<double> result = left.Multiply(right);  // 1e31 * 3e30 = 3e61 qm/s

        // Then
        Assert.Equal(1e31, left.QuectoMetersPerSecond, Tolerance);
        Assert.Equal(3e30, right.QuectoMetersPerSecond, Tolerance);
        Assert.Equal(3e61, result.QuectoMetersPerSecond, Tolerance);
    }

    [Fact(DisplayName = "Velocity.Divide should produce the expected result")]
    public void VelocityDivideShouldProduceExpectedValue()
    {
        // Given
        Velocity<double> left = Velocity<double>.FromMetersPerSecond(100.0);  // 1e32 qm/s
        Velocity<double> right = Velocity<double>.FromMetersPerSecond(20.0);  // 2e31 qm/s

        // When
        Velocity<double> result = left.Divide(right);  // 1e32 / 2e31 = 5 qm/s

        // Then
        Assert.Equal(5.0, result.QuectoMetersPerSecond, Tolerance);
        Assert.Equal(5e-30, result.MetersPerSecond, Tolerance);
    }

    [Fact(DisplayName = "Velocity comparison should produce the expected result (left equal to right)")]
    public void VelocityComparisonShouldProduceExpectedResultLeftEqualToRight()
    {
        // Given
        Velocity<double> left = Velocity<double>.FromKilometersPerHour(123.0);
        Velocity<double> right = Velocity<double>.FromKilometersPerHour(123.0);

        // When / Then
        Assert.Equal(0, Velocity<double>.Compare(left, right));
        Assert.Equal(0, left.CompareTo(right));
        Assert.Equal(0, left.CompareTo((object)right));
        Assert.False(left > right);
        Assert.True(left >= right);
        Assert.False(left < right);
        Assert.True(left <= right);
    }

    [Fact(DisplayName = "Velocity comparison should produce the expected result (left greater than right)")]
    public void VelocityComparisonShouldProduceExpectedLeftGreaterThanRight()
    {
        // Given
        Velocity<double> left = Velocity<double>.FromKilometersPerHour(456.0);
        Velocity<double> right = Velocity<double>.FromKilometersPerHour(123.0);

        // When / Then
        Assert.Equal(1, Velocity<double>.Compare(left, right));
        Assert.Equal(1, left.CompareTo(right));
        Assert.Equal(1, left.CompareTo((object)right));
        Assert.True(left > right);
        Assert.True(left >= right);
        Assert.False(left < right);
        Assert.False(left <= right);
    }

    [Fact(DisplayName = "Velocity comparison should produce the expected result (left less than right)")]
    public void VelocityComparisonShouldProduceExpectedLeftLessThanRight()
    {
        // Given
        Velocity<double> left = Velocity<double>.FromKilometersPerHour(123.0);
        Velocity<double> right = Velocity<double>.FromKilometersPerHour(456.0);

        // When / Then
        Assert.Equal(-1, Velocity<double>.Compare(left, right));
        Assert.Equal(-1, left.CompareTo(right));
        Assert.Equal(-1, left.CompareTo((object)right));
        Assert.False(left > right);
        Assert.False(left >= right);
        Assert.True(left < right);
        Assert.True(left <= right);
    }

    [Fact(DisplayName = "Velocity equality should produce the expected result (left equal to right)")]
    public void VelocityEqualityShouldProduceExpectedResultLeftEqualToRight()
    {
        // Given: 2 m/s = 2000 mm/s (exact 1:1000 ratio)
        Velocity<BigDecimal> left = Velocity<BigDecimal>.FromMetersPerSecond(2.0);
        Velocity<BigDecimal> right = Velocity<BigDecimal>.FromMillimetersPerSecond(2000.0);

        // When / Then
        Assert.True(Velocity<BigDecimal>.Equals(left, right));
        Assert.True(left.Equals(right));
        Assert.True(left.Equals((object)right));
        Assert.True(left == right);
        Assert.False(left != right);
    }

    [Fact(DisplayName = "Velocity equality should produce the expected result (left not equal to right)")]
    public void VelocityEqualityShouldProduceExpectedResultLeftNotEqualToRight()
    {
        // Given
        Velocity<double> left = Velocity<double>.FromMetersPerSecond(2.0);
        Velocity<double> right = Velocity<double>.FromMillimetersPerSecond(2500.0);

        // When / Then
        Assert.False(Velocity<double>.Equals(left, right));
        Assert.False(left.Equals(right));
        Assert.False(left.Equals((object)right));
        Assert.False(left == right);
        Assert.True(left != right);
    }

    [Fact(DisplayName = "Velocity.ToString should produce the expected result")]
    public void VelocityToStringShouldProduceExpectedResult()
    {
        // Given
        Velocity<double> v = Velocity<double>.FromMetersPerSecond(1000.0);

        // When / Then
        Assert.Equal("1,000.000 m/s", $"{v:mps3}");
        Assert.Equal("1.000 km/s", $"{v:kmps3}");
        Assert.Equal("3,600.000 km/h", $"{v:kmph3}");
    }

    [Fact(DisplayName = "Velocity.ToString Mmps vs mmps are case-sensitive")]
    public void VelocityToStringMmpsVsMmpsAreCaseSensitive()
    {
        // Given
        Velocity<double> v = Velocity<double>.FromMetersPerSecond(1.0);

        // Then
        Assert.Equal("0.000001 Mm/s", $"{v:Mmps6}"); // mega
        Assert.Equal("1,000.000 mm/s", $"{v:mmps3}"); // milli
    }

    [Fact(DisplayName = "Velocity.ToString non-SI specifiers should use proper unit symbols")]
    public void VelocityToStringNonSiSpecifiersShouldUseProperUnitSymbols()
    {
        // Given: 100 km/h
        Velocity<double> v = Velocity<double>.FromKilometersPerHour(100.0);

        // Then
        Assert.Equal("100.000 km/h", $"{v:kmph3}");
        Assert.Equal("62.137 mph", $"{v:mph3}");
        Assert.Equal("91.134 ft/s", $"{v:ftps3}");
        Assert.Equal("53.996 kn", $"{v:kn3}");
        Assert.Equal("1,093.613 in/s", $"{v:inps3}");
    }

    [Fact(DisplayName = "Velocity.ToString Mach and SpeedOfLight should be rendered correctly")]
    public void VelocityToStringMachAndSpeedOfLightShouldBeRenderedCorrectly()
    {
        // Given: speed of light
        Velocity<double> v = Velocity<double>.FromMetersPerSecond(299792458.0);

        // Then
        Assert.Equal("1.000 c", $"{v:c3}");
        Assert.Equal("880,991.090 Ma", $"{v:Ma3}"); // c / Mach1
    }

    [Fact(DisplayName = "Velocity.ToString µm/s symbol should differ from format specifier")]
    public void VelocityToStringMicrometersPerSecondSymbolShouldDifferFromFormatSpecifier()
    {
        // Given
        Velocity<double> v = Velocity<double>.FromMetersPerSecond(1.0);

        // Then: specifier is umps, but symbol rendered is µm/s
        Assert.Equal("1,000,000.000 µm/s", $"{v:umps3}");
    }

    [Fact(DisplayName = "Velocity.ToString should honor custom culture separators")]
    public void VelocityToStringShouldHonorCustomCulture()
    {
        // Given
        CultureInfo customCulture = new("de-DE");
        Velocity<double> v = Velocity<double>.FromKilometersPerHour(1234.56);

        // When
        string formatted = v.ToString("kmph2", customCulture);

        // Then
        Assert.Equal("1.234,56 km/h", formatted);
    }
}
