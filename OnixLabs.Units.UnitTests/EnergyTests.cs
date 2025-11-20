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

public sealed class EnergyTests
{
    // IEEE-754 binary floating-point arithmetic causes small discrepancies in calculation, therefore we need a tolerance.
    private const double Tolerance = 1e+42;

    [Fact(DisplayName = "Energy.Zero should produce the expected result")]
    public void EnergyZeroShouldProduceExpectedResult()
    {
        // Given / When
        Energy<double> energy = Energy<double>.Zero;

        // Then
        Assert.Equal(0.0, energy.YoctoJoules, Tolerance);
    }

    [Theory(DisplayName = "Energy.FromYoctoJoules should produce the expected YoctoJoules")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1.0)]
    [InlineData(2.5, 2.5)]
    public void EnergyFromYoctoJoulesShouldProduceExpectedYoctoJoules(double value, double expectedYoctoJoules)
    {
        // Given / When
        Energy<double> energy = Energy<double>.FromYoctoJoules(value);

        // Then
        Assert.Equal(expectedYoctoJoules, energy.YoctoJoules, Tolerance);
    }

    [Theory(DisplayName = "Energy.FromJoules should produce the expected YoctoJoules")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e24)]
    [InlineData(2.5, 2.5e24)]
    public void EnergyFromJoulesShouldProduceExpectedYoctoJoules(double value, double expectedYoctoJoules)
    {
        // Given / When
        Energy<double> energy = Energy<double>.FromJoules(value);

        // Then
        Assert.Equal(expectedYoctoJoules, energy.YoctoJoules, Tolerance);
    }

    [Theory(DisplayName = "Energy.FromKiloJoules should produce the expected YoctoJoules")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e27)]
    [InlineData(2.5, 2.5e27)]
    public void EnergyFromKiloJoulesShouldProduceExpectedYoctoJoules(double value, double expectedYoctoJoules)
    {
        // Given / When
        Energy<double> energy = Energy<double>.FromKiloJoules(value);

        // Then
        Assert.Equal(expectedYoctoJoules, energy.YoctoJoules, Tolerance);
    }

    [Theory(DisplayName = "Energy.FromMegaJoules should produce the expected YoctoJoules")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e30)]
    [InlineData(2.5, 2.5e30)]
    public void EnergyFromMegaJoulesShouldProduceExpectedYoctoJoules(double value, double expectedYoctoJoules)
    {
        // Given / When
        Energy<double> energy = Energy<double>.FromMegaJoules(value);

        // Then
        Assert.Equal(expectedYoctoJoules, energy.YoctoJoules, Tolerance);
    }

    [Theory(DisplayName = "Energy.FromGigaJoules should produce the expected YoctoJoules")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e33)]
    [InlineData(2.5, 2.5e33)]
    public void EnergyFromGigaJoulesShouldProduceExpectedYoctoJoules(double value, double expectedYoctoJoules)
    {
        // Given / When
        Energy<double> energy = Energy<double>.FromGigaJoules(value);

        // Then
        Assert.Equal(expectedYoctoJoules, energy.YoctoJoules, Tolerance);
    }

    [Theory(DisplayName = "Energy.FromCalories should produce the expected YoctoJoules")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 4.184e24)]
    [InlineData(2.5, 1.046e25)]
    public void EnergyFromCaloriesShouldProduceExpectedYoctoJoules(double value, double expectedYoctoJoules)
    {
        // Given / When
        Energy<double> energy = Energy<double>.FromCalories(value);

        // Then
        Assert.Equal(expectedYoctoJoules, energy.YoctoJoules, Tolerance);
    }

    [Theory(DisplayName = "Energy.FromKiloCalories should produce the expected YoctoJoules")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 4.184e27)]
    [InlineData(2.5, 1.046e28)]
    public void EnergyFromKiloCaloriesShouldProduceExpectedYoctoJoules(double value, double expectedYoctoJoules)
    {
        // Given / When
        Energy<double> energy = Energy<double>.FromKiloCalories(value);

        // Then
        Assert.Equal(expectedYoctoJoules, energy.YoctoJoules, Tolerance);
    }

    [Theory(DisplayName = "Energy.FromWattHours should produce the expected YoctoJoules")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 3.6e27)]
    [InlineData(2.5, 9.0e27)]
    public void EnergyFromWattHoursShouldProduceExpectedYoctoJoules(double value, double expectedYoctoJoules)
    {
        // Given / When
        Energy<double> energy = Energy<double>.FromWattHours(value);

        // Then
        Assert.Equal(expectedYoctoJoules, energy.YoctoJoules, Tolerance);
    }

    [Theory(DisplayName = "Energy.FromKiloWattHours should produce the expected YoctoJoules")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 3.6e30)]
    [InlineData(2.5, 9.0e30)]
    public void EnergyFromKiloWattHoursShouldProduceExpectedYoctoJoules(double value, double expectedYoctoJoules)
    {
        // Given / When
        Energy<double> energy = Energy<double>.FromKiloWattHours(value);

        // Then
        Assert.Equal(expectedYoctoJoules, energy.YoctoJoules, Tolerance);
    }

    [Theory(DisplayName = "Energy.FromErgs should produce the expected YoctoJoules")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e17)]
    [InlineData(2.5, 2.5e17)]
    public void EnergyFromErgsShouldProduceExpectedYoctoJoules(double value, double expectedYoctoJoules)
    {
        // Given / When
        Energy<double> energy = Energy<double>.FromErgs(value);

        // Then
        Assert.Equal(expectedYoctoJoules, energy.YoctoJoules, Tolerance);
    }

    [Theory(DisplayName = "Energy.FromBritishThermalUnits should produce the expected YoctoJoules")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1.05505585262e27)]
    [InlineData(2.5, 2.63763963155e27)]
    public void EnergyFromBritishThermalUnitsShouldProduceExpectedYoctoJoules(double value, double expectedYoctoJoules)
    {
        // Given / When
        Energy<double> energy = Energy<double>.FromBritishThermalUnits(value);

        // Then
        Assert.Equal(expectedYoctoJoules, energy.YoctoJoules, Tolerance);
    }

    [Theory(DisplayName = "Energy.FromFootPounds should produce the expected YoctoJoules")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1.3558179483314e24)]
    [InlineData(2.5, 3.3895448708285e24)]
    public void EnergyFromFootPoundsShouldProduceExpectedYoctoJoules(double value, double expectedYoctoJoules)
    {
        // Given / When
        Energy<double> energy = Energy<double>.FromFootPounds(value);

        // Then
        Assert.Equal(expectedYoctoJoules, energy.YoctoJoules, Tolerance);
    }

    [Theory(DisplayName = "Energy.FromElectronVolts should produce the expected YoctoJoules")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1.602176634e5)]
    [InlineData(2.5, 4.005441584999999e5)]
    public void EnergyFromElectronVoltsShouldProduceExpectedYoctoJoules(double value, double expectedYoctoJoules)
    {
        // Given / When
        Energy<double> energy = Energy<double>.FromElectronVolts(value);

        // Then
        Assert.Equal(expectedYoctoJoules, energy.YoctoJoules, Tolerance);
    }

    [Fact(DisplayName = "Energy.Add should produce the expected result")]
    public void EnergyAddShouldProduceExpectedValue()
    {
        // Given
        Energy<double> left = Energy<double>.FromJoules(1500.0);
        Energy<double> right = Energy<double>.FromJoules(500.0);

        // When
        Energy<double> result = left.Add(right);

        // Then
        Assert.Equal(2000.0, result.Joules, Tolerance);
    }

    [Fact(DisplayName = "Energy.Subtract should produce the expected result")]
    public void EnergySubtractShouldProduceExpectedValue()
    {
        // Given
        Energy<double> left = Energy<double>.FromJoules(1500.0);
        Energy<double> right = Energy<double>.FromJoules(400.0);

        // When
        Energy<double> result = left.Subtract(right);

        // Then
        Assert.Equal(1100.0, result.Joules, Tolerance);
    }

    [Fact(DisplayName = "Energy.Multiply should produce the expected result")]
    public void EnergyMultiplyShouldProduceExpectedValue()
    {
        // Given
        Energy<double> left = Energy<double>.FromJoules(10.0); // 1e25 yJ
        Energy<double> right = Energy<double>.FromJoules(3.0); // 3e24 yJ

        // When
        Energy<double> result = left.Multiply(right); // 1e25 * 3e24 = 3e49 yJ

        // Then
        Assert.Equal(1e25, left.YoctoJoules, Tolerance);
        Assert.Equal(3e24, right.YoctoJoules, Tolerance);
        Assert.Equal(3e49, result.YoctoJoules, Tolerance);
        Assert.Equal(3e25, result.Joules, Tolerance);
    }

    [Fact(DisplayName = "Energy.Divide should produce the expected result")]
    public void EnergyDivideShouldProduceExpectedValue()
    {
        // Given
        Energy<double> left = Energy<double>.FromJoules(100.0); // 1e26 yJ
        Energy<double> right = Energy<double>.FromJoules(20.0); // 2e25 yJ

        // When
        Energy<double> result = left.Divide(right); // 1e26 / 2e25 = 5 yJ

        // Then
        Assert.Equal(5.0, result.YoctoJoules, Tolerance);
        Assert.Equal(5e-24, result.Joules, Tolerance);
    }

    [Fact(DisplayName = "Energy equality should produce the expected result (left equal to right)")]
    public void EnergyEqualityShouldProduceExpectedResultLeftEqualToRight()
    {
        // Given
        Energy<BigDecimal> left = Energy<BigDecimal>.FromKiloJoules(2.0);
        Energy<BigDecimal> right = Energy<BigDecimal>.FromJoules(2000.0);

        // When / Then
        Assert.True(Energy<BigDecimal>.Equals(left, right));
        Assert.True(left.Equals(right));
        Assert.True(left.Equals((object)right));
        Assert.True(left == right);
        Assert.False(left != right);
    }

    [Fact(DisplayName = "Energy equality should produce the expected result (left not equal to right)")]
    public void EnergyEqualityShouldProduceExpectedResultLeftNotEqualToRight()
    {
        // Given
        Energy<double> left = Energy<double>.FromKiloJoules(2.0);
        Energy<double> right = Energy<double>.FromJoules(2500.0);

        // When / Then
        Assert.False(Energy<double>.Equals(left, right));
        Assert.False(left.Equals(right));
        Assert.False(left.Equals((object)right));
        Assert.False(left == right);
        Assert.True(left != right);
    }

    [Fact(DisplayName = "Energy comparison should produce the expected result (left equal to right)")]
    public void EnergyComparisonShouldProduceExpectedResultLeftEqualToRight()
    {
        // Given
        Energy<double> left = Energy<double>.FromJoules(1234.0);
        Energy<double> right = Energy<double>.FromJoules(1234.0);

        // When / Then
        Assert.Equal(0, Energy<double>.Compare(left, right));
        Assert.Equal(0, left.CompareTo(right));
        Assert.Equal(0, left.CompareTo((object)right));
        Assert.False(left > right);
        Assert.True(left >= right);
        Assert.False(left < right);
        Assert.True(left <= right);
    }

    [Fact(DisplayName = "Energy comparison should produce the expected result (left greater than right)")]
    public void EnergyComparisonShouldProduceExpectedLeftGreaterThanRight()
    {
        // Given
        Energy<double> left = Energy<double>.FromJoules(4567.0);
        Energy<double> right = Energy<double>.FromJoules(1234.0);

        // When / Then
        Assert.Equal(1, Energy<double>.Compare(left, right));
        Assert.Equal(1, left.CompareTo(right));
        Assert.Equal(1, left.CompareTo((object)right));
        Assert.True(left > right);
        Assert.True(left >= right);
        Assert.False(left < right);
        Assert.False(left <= right);
    }

    [Fact(DisplayName = "Energy comparison should produce the expected result (left less than right)")]
    public void EnergyComparisonShouldProduceExpectedLeftLessThanRight()
    {
        // Given
        Energy<double> left = Energy<double>.FromJoules(1234.0);
        Energy<double> right = Energy<double>.FromJoules(4567.0);

        // When / Then
        Assert.Equal(-1, Energy<double>.Compare(left, right));
        Assert.Equal(-1, left.CompareTo(right));
        Assert.Equal(-1, left.CompareTo((object)right));
        Assert.False(left > right);
        Assert.False(left >= right);
        Assert.True(left < right);
        Assert.True(left <= right);
    }

    [Fact(DisplayName = "Energy.ToString should produce the expected result")]
    public void EnergyToStringShouldProduceExpectedResult()
    {
        // Given
        // Use a value that renders nicely across several specifiers.
        Energy<double> energy = Energy<double>.FromJoules(1000.0);

        // When / Then
        Assert.Equal("1,000.000 J", $"{energy:J3}");
        Assert.Equal("1.000 kJ", $"{energy:kJ3}");
        Assert.Equal("0.001 MJ", $"{energy:MJ3}");
        Assert.Equal("0.000 GJ", $"{energy:GJ3}");
        Assert.Equal("239.006 cal", $"{energy:cal3}");
        Assert.Equal("0.239 kcal", $"{energy:kcal3}");
        Assert.Equal("0.278 Wh", $"{energy:Wh3}");
        Assert.Equal("0.000 kWh", $"{energy:kWh3}");
        Assert.Equal("10,000,000,000.000 erg", $"{energy:erg3}");
        Assert.Equal("0.948 BTU", $"{energy:BTU3}");
        Assert.Equal("737.562 ft·lbf", $"{energy:ftlb3}");
        Assert.Equal("6,241,509,074,460,762,701,824.000 eV", $"{energy:eV3}");
    }

    [Fact(DisplayName = "Energy.ToString should honor custom culture separators")]
    public void EnergyToStringShouldHonorCustomCulture()
    {
        // Given
        CultureInfo customCulture = new("de-DE");
        Energy<double> energy = Energy<double>.FromJoules(1234.56);

        // When
        string formatted = energy.ToString("J2", customCulture);

        // Then
        // German uses '.' for thousands and ',' for decimals.
        Assert.Equal("1.234,56 J", formatted);
    }
}
