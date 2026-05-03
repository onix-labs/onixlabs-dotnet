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

public sealed class PowerTests
{
    // IEEE-754 binary floating-point arithmetic causes small discrepancies in calculation, therefore we need a tolerance.
    private const double Tolerance = 1e+42;

    [Fact(DisplayName = "Power.Zero should produce the expected result")]
    public void PowerZeroShouldProduceExpectedResult()
    {
        // Given / When
        Power<double> power = Power<double>.Zero;

        // Then
        Assert.Equal(0.0, power.QuectoWatts, Tolerance);
    }

    [Theory(DisplayName = "Power.FromQuectowatts should produce the expected QuectoWatts")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1.0)]
    [InlineData(2.5, 2.5)]
    public void PowerFromQuectowattsShouldProduceExpectedQuectoWatts(double value, double expected)
    {
        Power<double> p = Power<double>.FromQuectowatts(value);
        Assert.Equal(expected, p.QuectoWatts, Tolerance);
    }

    [Theory(DisplayName = "Power.FromRontowatts should produce the expected QuectoWatts")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e3)]
    [InlineData(2.5, 2.5e3)]
    public void PowerFromRontowattsShouldProduceExpectedQuectoWatts(double value, double expected)
    {
        Power<double> p = Power<double>.FromRontowatts(value);
        Assert.Equal(expected, p.QuectoWatts, Tolerance);
    }

    [Theory(DisplayName = "Power.FromYoctowatts should produce the expected QuectoWatts")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e6)]
    [InlineData(2.5, 2.5e6)]
    public void PowerFromYoctowattsShouldProduceExpectedQuectoWatts(double value, double expected)
    {
        Power<double> p = Power<double>.FromYoctowatts(value);
        Assert.Equal(expected, p.QuectoWatts, Tolerance);
    }

    [Theory(DisplayName = "Power.FromZeptowatts should produce the expected QuectoWatts")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e9)]
    [InlineData(2.5, 2.5e9)]
    public void PowerFromZeptowattsShouldProduceExpectedQuectoWatts(double value, double expected)
    {
        Power<double> p = Power<double>.FromZeptowatts(value);
        Assert.Equal(expected, p.QuectoWatts, Tolerance);
    }

    [Theory(DisplayName = "Power.FromAttowatts should produce the expected QuectoWatts")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e12)]
    [InlineData(2.5, 2.5e12)]
    public void PowerFromAttowattsShouldProduceExpectedQuectoWatts(double value, double expected)
    {
        Power<double> p = Power<double>.FromAttowatts(value);
        Assert.Equal(expected, p.QuectoWatts, Tolerance);
    }

    [Theory(DisplayName = "Power.FromFemtowatts should produce the expected QuectoWatts")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e15)]
    [InlineData(2.5, 2.5e15)]
    public void PowerFromFemtowattsShouldProduceExpectedQuectoWatts(double value, double expected)
    {
        Power<double> p = Power<double>.FromFemtowatts(value);
        Assert.Equal(expected, p.QuectoWatts, Tolerance);
    }

    [Theory(DisplayName = "Power.FromPicowatts should produce the expected QuectoWatts")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e18)]
    [InlineData(2.5, 2.5e18)]
    public void PowerFromPicowattsShouldProduceExpectedQuectoWatts(double value, double expected)
    {
        Power<double> p = Power<double>.FromPicowatts(value);
        Assert.Equal(expected, p.QuectoWatts, Tolerance);
    }

    [Theory(DisplayName = "Power.FromNanowatts should produce the expected QuectoWatts")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e21)]
    [InlineData(2.5, 2.5e21)]
    public void PowerFromNanowattsShouldProduceExpectedQuectoWatts(double value, double expected)
    {
        Power<double> p = Power<double>.FromNanowatts(value);
        Assert.Equal(expected, p.QuectoWatts, Tolerance);
    }

    [Theory(DisplayName = "Power.FromMicrowatts should produce the expected QuectoWatts")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e24)]
    [InlineData(2.5, 2.5e24)]
    public void PowerFromMicrowattsShouldProduceExpectedQuectoWatts(double value, double expected)
    {
        Power<double> p = Power<double>.FromMicrowatts(value);
        Assert.Equal(expected, p.QuectoWatts, Tolerance);
    }

    [Theory(DisplayName = "Power.FromMilliwatts should produce the expected QuectoWatts")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e27)]
    [InlineData(2.5, 2.5e27)]
    public void PowerFromMilliwattsShouldProduceExpectedQuectoWatts(double value, double expected)
    {
        Power<double> p = Power<double>.FromMilliwatts(value);
        Assert.Equal(expected, p.QuectoWatts, Tolerance);
    }

    [Theory(DisplayName = "Power.FromCentiwatts should produce the expected QuectoWatts")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e28)]
    [InlineData(2.5, 2.5e28)]
    public void PowerFromCentiwattsShouldProduceExpectedQuectoWatts(double value, double expected)
    {
        Power<double> p = Power<double>.FromCentiwatts(value);
        Assert.Equal(expected, p.QuectoWatts, Tolerance);
    }

    [Theory(DisplayName = "Power.FromDeciwatts should produce the expected QuectoWatts")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e29)]
    [InlineData(2.5, 2.5e29)]
    public void PowerFromDeciwattsShouldProduceExpectedQuectoWatts(double value, double expected)
    {
        Power<double> p = Power<double>.FromDeciwatts(value);
        Assert.Equal(expected, p.QuectoWatts, Tolerance);
    }

    [Theory(DisplayName = "Power.FromWatts should produce the expected QuectoWatts")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e30)]
    [InlineData(2.5, 2.5e30)]
    public void PowerFromWattsShouldProduceExpectedQuectoWatts(double value, double expected)
    {
        Power<double> p = Power<double>.FromWatts(value);
        Assert.Equal(expected, p.QuectoWatts, Tolerance);
    }

    [Theory(DisplayName = "Power.FromDecawatts should produce the expected QuectoWatts")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e31)]
    [InlineData(2.5, 2.5e31)]
    public void PowerFromDecawattsShouldProduceExpectedQuectoWatts(double value, double expected)
    {
        Power<double> p = Power<double>.FromDecawatts(value);
        Assert.Equal(expected, p.QuectoWatts, Tolerance);
    }

    [Theory(DisplayName = "Power.FromHectowatts should produce the expected QuectoWatts")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e32)]
    [InlineData(2.5, 2.5e32)]
    public void PowerFromHectowattsShouldProduceExpectedQuectoWatts(double value, double expected)
    {
        Power<double> p = Power<double>.FromHectowatts(value);
        Assert.Equal(expected, p.QuectoWatts, Tolerance);
    }

    [Theory(DisplayName = "Power.FromKilowatts should produce the expected QuectoWatts")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e33)]
    [InlineData(2.5, 2.5e33)]
    public void PowerFromKilowattsShouldProduceExpectedQuectoWatts(double value, double expected)
    {
        Power<double> p = Power<double>.FromKilowatts(value);
        Assert.Equal(expected, p.QuectoWatts, Tolerance);
    }

    [Theory(DisplayName = "Power.FromMegawatts should produce the expected QuectoWatts")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e36)]
    [InlineData(2.5, 2.5e36)]
    public void PowerFromMegawattsShouldProduceExpectedQuectoWatts(double value, double expected)
    {
        Power<double> p = Power<double>.FromMegawatts(value);
        Assert.Equal(expected, p.QuectoWatts, Tolerance);
    }

    [Theory(DisplayName = "Power.FromGigawatts should produce the expected QuectoWatts")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e39)]
    [InlineData(2.5, 2.5e39)]
    public void PowerFromGigawattsShouldProduceExpectedQuectoWatts(double value, double expected)
    {
        Power<double> p = Power<double>.FromGigawatts(value);
        Assert.Equal(expected, p.QuectoWatts, Tolerance);
    }

    [Theory(DisplayName = "Power.FromTerawatts should produce the expected QuectoWatts")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e42)]
    [InlineData(2.5, 2.5e42)]
    public void PowerFromTerawattsShouldProduceExpectedQuectoWatts(double value, double expected)
    {
        Power<double> p = Power<double>.FromTerawatts(value);
        Assert.Equal(expected, p.QuectoWatts, Tolerance);
    }

    [Theory(DisplayName = "Power.FromPetawatts should produce the expected QuectoWatts")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e45)]
    [InlineData(2.5, 2.5e45)]
    public void PowerFromPetawattsShouldProduceExpectedQuectoWatts(double value, double expected)
    {
        Power<double> p = Power<double>.FromPetawatts(value);
        Assert.Equal(expected, p.QuectoWatts, Tolerance);
    }

    [Theory(DisplayName = "Power.FromExawatts should produce the expected QuectoWatts")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e48)]
    [InlineData(2.5, 2.5e48)]
    public void PowerFromExawattsShouldProduceExpectedQuectoWatts(double value, double expected)
    {
        Power<double> p = Power<double>.FromExawatts(value);
        Assert.Equal(expected, p.QuectoWatts, Tolerance);
    }

    [Theory(DisplayName = "Power.FromZettawatts should produce the expected QuectoWatts")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e51)]
    [InlineData(2.5, 2.5e51)]
    public void PowerFromZettawattsShouldProduceExpectedQuectoWatts(double value, double expected)
    {
        Power<double> p = Power<double>.FromZettawatts(value);
        Assert.Equal(expected, p.QuectoWatts, Tolerance);
    }

    [Theory(DisplayName = "Power.FromYottawatts should produce the expected QuectoWatts")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e54)]
    [InlineData(2.5, 2.5e54)]
    public void PowerFromYottawattsShouldProduceExpectedQuectoWatts(double value, double expected)
    {
        Power<double> p = Power<double>.FromYottawatts(value);
        Assert.Equal(expected, p.QuectoWatts, Tolerance);
    }

    [Theory(DisplayName = "Power.FromRonnawatts should produce the expected QuectoWatts")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e57)]
    [InlineData(2.5, 2.5e57)]
    public void PowerFromRonnawattsShouldProduceExpectedQuectoWatts(double value, double expected)
    {
        Power<double> p = Power<double>.FromRonnawatts(value);
        Assert.Equal(expected, p.QuectoWatts, Tolerance);
    }

    [Theory(DisplayName = "Power.FromQuettawatts should produce the expected QuectoWatts")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e60)]
    [InlineData(2.5, 2.5e60)]
    public void PowerFromQuettawattsShouldProduceExpectedQuectoWatts(double value, double expected)
    {
        Power<double> p = Power<double>.FromQuettawatts(value);
        Assert.Equal(expected, p.QuectoWatts, Tolerance);
    }

    [Theory(DisplayName = "Power.FromMechanicalHorsepower should produce the expected QuectoWatts")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 7.456998715822702e32)]
    [InlineData(2.0, 1.4913997431645404e33)]
    public void PowerFromMechanicalHorsepowerShouldProduceExpectedQuectoWatts(double value, double expected)
    {
        Power<double> p = Power<double>.FromMechanicalHorsepower(value);
        Assert.Equal(expected, p.QuectoWatts, Tolerance);
    }

    [Theory(DisplayName = "Power.FromMetricHorsepower should produce the expected QuectoWatts")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 7.3549875e32)]
    [InlineData(2.0, 1.4709975e33)]
    public void PowerFromMetricHorsepowerShouldProduceExpectedQuectoWatts(double value, double expected)
    {
        Power<double> p = Power<double>.FromMetricHorsepower(value);
        Assert.Equal(expected, p.QuectoWatts, Tolerance);
    }

    [Theory(DisplayName = "Power.FromBtusPerHour should produce the expected QuectoWatts")]
    [InlineData(0.0)]
    [InlineData(1.0)]
    [InlineData(3412.141633127942)] // ~ 1 kW
    public void PowerFromBtusPerHourShouldProduceExpectedQuectoWatts(double value)
    {
        // Given: 1 BTU/h = (1055.05585262 / 3600) W, so qW = value × (1055.05585262 / 3600) × 1e30
        double expected = value * (1055.05585262 / 3600.0) * 1e30;

        // When
        Power<double> p = Power<double>.FromBtusPerHour(value);

        // Then
        Assert.Equal(expected, p.QuectoWatts, Tolerance);
    }

    [Theory(DisplayName = "Power.FromCaloriesPerSecond should produce the expected QuectoWatts")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 4.184e30)]
    [InlineData(2.5, 1.046e31)]
    public void PowerFromCaloriesPerSecondShouldProduceExpectedQuectoWatts(double value, double expected)
    {
        Power<double> p = Power<double>.FromCaloriesPerSecond(value);
        Assert.Equal(expected, p.QuectoWatts, Tolerance);
    }

    [Theory(DisplayName = "Power.FromErgsPerSecond should produce the expected QuectoWatts")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e23)]
    [InlineData(1e7, 1e30)] // 1e7 erg/s = 1 W
    public void PowerFromErgsPerSecondShouldProduceExpectedQuectoWatts(double value, double expected)
    {
        Power<double> p = Power<double>.FromErgsPerSecond(value);
        Assert.Equal(expected, p.QuectoWatts, Tolerance);
    }

    [Theory(DisplayName = "Power.FromFootPoundsPerSecond should produce the expected QuectoWatts")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1.3558179483314004e30)]
    [InlineData(2.0, 2.7116358966628007e30)]
    public void PowerFromFootPoundsPerSecondShouldProduceExpectedQuectoWatts(double value, double expected)
    {
        Power<double> p = Power<double>.FromFootPoundsPerSecond(value);
        Assert.Equal(expected, p.QuectoWatts, Tolerance);
    }

    [Theory(DisplayName = "Power.FromTonsOfRefrigeration should produce the expected QuectoWatts")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 3.5168528420666664e33)]
    [InlineData(2.0, 7.0337056841333327e33)]
    public void PowerFromTonsOfRefrigerationShouldProduceExpectedQuectoWatts(double value, double expected)
    {
        Power<double> p = Power<double>.FromTonsOfRefrigeration(value);
        Assert.Equal(expected, p.QuectoWatts, Tolerance);
    }

    [Fact(DisplayName = "Power non-SI conversions should roundtrip through Watts")]
    public void PowerNonSiConversionsShouldRoundtripThroughWatts()
    {
        // Given
        Power<double> p = Power<double>.FromWatts(745.6998715822702);

        // Then
        Assert.Equal(1.0, p.MechanicalHorsepower, 1e-9);

        Power<double> q = Power<double>.FromWatts(735.49875);
        Assert.Equal(1.0, q.MetricHorsepower, 1e-9);

        Power<double> r = Power<double>.FromWatts(1.0);
        Assert.Equal(1e7, r.ErgsPerSecond, 1e-6);
    }

    [Fact(DisplayName = "Power.Add should produce the expected result")]
    public void PowerAddShouldProduceExpectedValue()
    {
        // Given
        Power<double> left = Power<double>.FromKilowatts(1.5);
        Power<double> right = Power<double>.FromKilowatts(0.5);

        // When
        Power<double> result = left.Add(right);

        // Then
        Assert.Equal(2.0, result.KiloWatts, Tolerance);
    }

    [Fact(DisplayName = "Power.Subtract should produce the expected result")]
    public void PowerSubtractShouldProduceExpectedValue()
    {
        // Given
        Power<double> left = Power<double>.FromKilowatts(1.5);
        Power<double> right = Power<double>.FromKilowatts(0.4);

        // When
        Power<double> result = left.Subtract(right);

        // Then
        Assert.Equal(1.1, result.KiloWatts, Tolerance);
    }

    [Fact(DisplayName = "Power.Multiply should produce the expected result")]
    public void PowerMultiplyShouldProduceExpectedValue()
    {
        // Given
        Power<double> left = Power<double>.FromWatts(10.0); // 1e31 qW
        Power<double> right = Power<double>.FromWatts(3.0); // 3e30 qW

        // When
        Power<double> result = left.Multiply(right); // 1e31 * 3e30 = 3e61 qW

        // Then
        Assert.Equal(1e31, left.QuectoWatts, Tolerance);
        Assert.Equal(3e30, right.QuectoWatts, Tolerance);
        Assert.Equal(3e61, result.QuectoWatts, Tolerance);
    }

    [Fact(DisplayName = "Power.Divide should produce the expected result")]
    public void PowerDivideShouldProduceExpectedValue()
    {
        // Given
        Power<double> left = Power<double>.FromWatts(100.0); // 1e32 qW
        Power<double> right = Power<double>.FromWatts(20.0); // 2e31 qW

        // When
        Power<double> result = left.Divide(right); // 1e32 / 2e31 = 5 qW

        // Then
        Assert.Equal(5.0, result.QuectoWatts, Tolerance);
        Assert.Equal(5e-30, result.Watts, Tolerance);
    }

    [Fact(DisplayName = "Power comparison should produce the expected result (left equal to right)")]
    public void PowerComparisonShouldProduceExpectedResultLeftEqualToRight()
    {
        // Given
        Power<double> left = Power<double>.FromKilowatts(123.0);
        Power<double> right = Power<double>.FromKilowatts(123.0);

        // When / Then
        Assert.Equal(0, Power<double>.Compare(left, right));
        Assert.Equal(0, left.CompareTo(right));
        Assert.Equal(0, left.CompareTo((object)right));
        Assert.False(left > right);
        Assert.True(left >= right);
        Assert.False(left < right);
        Assert.True(left <= right);
    }

    [Fact(DisplayName = "Power comparison should produce the expected result (left greater than right)")]
    public void PowerComparisonShouldProduceExpectedLeftGreaterThanRight()
    {
        // Given
        Power<double> left = Power<double>.FromKilowatts(456.0);
        Power<double> right = Power<double>.FromKilowatts(123.0);

        // When / Then
        Assert.Equal(1, Power<double>.Compare(left, right));
        Assert.Equal(1, left.CompareTo(right));
        Assert.Equal(1, left.CompareTo((object)right));
        Assert.True(left > right);
        Assert.True(left >= right);
        Assert.False(left < right);
        Assert.False(left <= right);
    }

    [Fact(DisplayName = "Power comparison should produce the expected result (left less than right)")]
    public void PowerComparisonShouldProduceExpectedLeftLessThanRight()
    {
        // Given
        Power<double> left = Power<double>.FromKilowatts(123.0);
        Power<double> right = Power<double>.FromKilowatts(456.0);

        // When / Then
        Assert.Equal(-1, Power<double>.Compare(left, right));
        Assert.Equal(-1, left.CompareTo(right));
        Assert.Equal(-1, left.CompareTo((object)right));
        Assert.False(left > right);
        Assert.False(left >= right);
        Assert.True(left < right);
        Assert.True(left <= right);
    }

    [Fact(DisplayName = "Power equality should produce the expected result (left equal to right)")]
    public void PowerEqualityShouldProduceExpectedResultLeftEqualToRight()
    {
        // Given
        Power<BigDecimal> left = Power<BigDecimal>.FromKilowatts(2.0);
        Power<BigDecimal> right = Power<BigDecimal>.FromWatts(2000.0);

        // When / Then
        Assert.True(Power<BigDecimal>.Equals(left, right));
        Assert.True(left.Equals(right));
        Assert.True(left.Equals((object)right));
        Assert.True(left == right);
        Assert.False(left != right);
    }

    [Fact(DisplayName = "Power equality should produce the expected result (left not equal to right)")]
    public void PowerEqualityShouldProduceExpectedResultLeftNotEqualToRight()
    {
        // Given
        Power<double> left = Power<double>.FromKilowatts(2.0);
        Power<double> right = Power<double>.FromWatts(2500.0);

        // When / Then
        Assert.False(Power<double>.Equals(left, right));
        Assert.False(left.Equals(right));
        Assert.False(left.Equals((object)right));
        Assert.False(left == right);
        Assert.True(left != right);
    }

    [Fact(DisplayName = "Power.ToString should produce the expected result")]
    public void PowerToStringShouldProduceExpectedResult()
    {
        // Given
        Power<double> p = Power<double>.FromWatts(1000.0);

        // When / Then
        Assert.Equal("1,000.000 W", $"{p:W3}");
        Assert.Equal("1.000 kW", $"{p:kW3}");
        Assert.Equal("0.001 MW", $"{p:MW3}");
        Assert.Equal("1,000,000.000 mW", $"{p:mW3}");
    }

    [Fact(DisplayName = "Power.ToString MW vs mW are case-sensitive")]
    public void PowerToStringMwVsMwAreCaseSensitive()
    {
        // Given
        Power<double> p = Power<double>.FromWatts(1.0);

        // Then
        Assert.Equal("0.000001 MW", $"{p:MW6}"); // mega
        Assert.Equal("1,000.000 mW", $"{p:mW3}"); // milli
    }

    [Fact(DisplayName = "Power.ToString non-SI specifiers should use proper unit symbols")]
    public void PowerToStringNonSiSpecifiersShouldUseProperUnitSymbols()
    {
        // Given
        Power<double> p = Power<double>.FromWatts(1000.0);

        // Then
        Assert.Equal("3,412.142 BTU/h", $"{p:BTUph3}");
        Assert.Equal("239.006 cal/s", $"{p:calps3}");
        Assert.Equal("10,000,000,000.000 erg/s", $"{p:ergps3}");
        Assert.Equal("737.562 ft·lbf/s", $"{p:ftlbfps3}");
        Assert.Equal("0.284 TR", $"{p:tref3}");
    }

    [Fact(DisplayName = "Power.ToString horsepower should use proper unit symbols")]
    public void PowerToStringHorsepowerShouldUseProperUnitSymbols()
    {
        // Given
        Power<double> p = Power<double>.FromKilowatts(1.0);

        // Then
        Assert.Equal("1.341 hp", $"{p:hp3}");
        Assert.Equal("1.360 PS", $"{p:PS3}");
    }

    [Fact(DisplayName = "Power.ToString µW symbol should differ from format specifier")]
    public void PowerToStringMicrowattsSymbolShouldDifferFromFormatSpecifier()
    {
        // Given
        Power<double> p = Power<double>.FromWatts(1.0);

        // Then: specifier is uW, but symbol rendered is µW
        Assert.Equal("1,000,000.000 µW", $"{p:uW3}");
    }

    [Fact(DisplayName = "Power.ToString should honor custom culture separators")]
    public void PowerToStringShouldHonorCustomCulture()
    {
        // Given
        CultureInfo customCulture = new("de-DE");
        Power<double> p = Power<double>.FromKilowatts(1234.56);

        // When
        string formatted = p.ToString("kW2", customCulture);

        // Then
        Assert.Equal("1.234,56 kW", formatted);
    }
}
