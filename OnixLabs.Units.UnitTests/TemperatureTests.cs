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

namespace OnixLabs.Units.UnitTests;

public sealed class TemperatureTests
{
    // IEEE-754 binary floating-point arithmetic causes small discrepancies in calculation, therefore we need a tolerance.
    private const double Tolerance = 1e-12;

    [Fact(DisplayName = "Temperature.Zero should produce the expected result")]
    public void TemperatureZeroShouldProduceExpectedResult()
    {
        // Given / When
        Temperature<double> temperature = Temperature<double>.Zero;

        // Then
        Assert.Equal(-273.15, temperature.Celsius, Tolerance);
        Assert.Equal(0.0, temperature.Kelvin, Tolerance);
        Assert.Equal(-459.67, temperature.Fahrenheit, Tolerance);
        Assert.Equal(559.725, temperature.Delisle, Tolerance);
        Assert.Equal(-90.1395, temperature.Newton, Tolerance);
        Assert.Equal(0.0, temperature.Rankine, Tolerance);
        Assert.Equal(-218.52, temperature.Reaumur, Tolerance);
        Assert.Equal(-135.90375, temperature.Romer, Tolerance);
    }

    [Theory(DisplayName = "Temperature.FromCelsius should produce the expected result")]
    [InlineData(-273.15, -273.15, 0.0, -459.67, 559.725, -90.1395, 0.0, -218.52, -135.90375)]
    [InlineData(0.0, 0.0, 273.15, 32.0, 150.0, 0.0, 491.67, 0.0, 7.5)]
    [InlineData(100.0, 100.0, 373.15, 212.0, 0.0, 33.0, 671.67, 80.0, 60.0)]
    [InlineData(-40.0, -40.0, 233.15, -40.0, 210.0, -13.2, 419.67, -32.0, -13.5)]
    [InlineData(20.0, 20.0, 293.15, 68.0, 120.0, 6.6, 527.67, 16.0, 18.0)]
    public void TemperatureFromCelsiusShouldProduceExpectedResult(
        double celsius,
        double expectedCelsius,
        double expectedKelvin,
        double expectedFahrenheit,
        double expectedDelisle,
        double expectedNewton,
        double expectedRankine,
        double expectedReaumur,
        double expectedRomer)
    {
        // When
        Temperature<double> temperature = Temperature<double>.FromCelsius(celsius);

        // Then
        Assert.Equal(expectedCelsius, temperature.Celsius, Tolerance);
        Assert.Equal(expectedKelvin, temperature.Kelvin, Tolerance);
        Assert.Equal(expectedFahrenheit, temperature.Fahrenheit, Tolerance);
        Assert.Equal(expectedDelisle, temperature.Delisle, Tolerance);
        Assert.Equal(expectedNewton, temperature.Newton, Tolerance);
        Assert.Equal(expectedRankine, temperature.Rankine, Tolerance);
        Assert.Equal(expectedReaumur, temperature.Reaumur, Tolerance);
        Assert.Equal(expectedRomer, temperature.Romer, Tolerance);
    }

    [Theory(DisplayName = "Temperature.FromDelisle should produce the expected result")]
    [InlineData(559.725, -273.15, 0.0, -459.67, 559.725, -90.1395, 0.0, -218.52, -135.90375)]
    [InlineData(150.0, 0.0, 273.15, 32.0, 150.0, 0.0, 491.67, 0.0, 7.5)]
    [InlineData(0.0, 100.0, 373.15, 212.0, 0.0, 33.0, 671.67, 80.0, 60.0)]
    [InlineData(210.0, -40.0, 233.15, -40.0, 210.0, -13.2, 419.67, -32.0, -13.5)]
    [InlineData(120.0, 20.0, 293.15, 68.0, 120.0, 6.6, 527.67, 16.0, 18.0)]
    public void TemperatureFromDelisleShouldProduceExpectedResult(
        double delisle,
        double expectedCelsius,
        double expectedKelvin,
        double expectedFahrenheit,
        double expectedDelisle,
        double expectedNewton,
        double expectedRankine,
        double expectedReaumur,
        double expectedRomer)
    {
        // When
        Temperature<double> temperature = Temperature<double>.FromDelisle(delisle);

        // Then
        Assert.Equal(expectedCelsius, temperature.Celsius, Tolerance);
        Assert.Equal(expectedKelvin, temperature.Kelvin, Tolerance);
        Assert.Equal(expectedFahrenheit, temperature.Fahrenheit, Tolerance);
        Assert.Equal(expectedDelisle, temperature.Delisle, Tolerance);
        Assert.Equal(expectedNewton, temperature.Newton, Tolerance);
        Assert.Equal(expectedRankine, temperature.Rankine, Tolerance);
        Assert.Equal(expectedReaumur, temperature.Reaumur, Tolerance);
        Assert.Equal(expectedRomer, temperature.Romer, Tolerance);
    }

    [Theory(DisplayName = "Temperature.FromFahrenheit should produce the expected result")]
    [InlineData(-459.67, -273.15, 0.0, -459.67, 559.725, -90.1395, 0.0, -218.52, -135.90375)]
    [InlineData(32.0, 0.0, 273.15, 32.0, 150.0, 0.0, 491.67, 0.0, 7.5)]
    [InlineData(212.0, 100.0, 373.15, 212.0, 0.0, 33.0, 671.67, 80.0, 60.0)]
    [InlineData(-40.0, -40.0, 233.15, -40.0, 210.0, -13.2, 419.67, -32.0, -13.5)]
    [InlineData(68.0, 20.0, 293.15, 68.0, 120.0, 6.6, 527.67, 16.0, 18.0)]
    public void TemperatureFromFahrenheitShouldProduceExpectedResult(
        double fahrenheit,
        double expectedCelsius,
        double expectedKelvin,
        double expectedFahrenheit,
        double expectedDelisle,
        double expectedNewton,
        double expectedRankine,
        double expectedReaumur,
        double expectedRomer)
    {
        Temperature<double> temperature = Temperature<double>.FromFahrenheit(fahrenheit);

        Assert.Equal(expectedCelsius, temperature.Celsius, Tolerance);
        Assert.Equal(expectedKelvin, temperature.Kelvin, Tolerance);
        Assert.Equal(expectedFahrenheit, temperature.Fahrenheit, Tolerance);
        Assert.Equal(expectedDelisle, temperature.Delisle, Tolerance);
        Assert.Equal(expectedNewton, temperature.Newton, Tolerance);
        Assert.Equal(expectedRankine, temperature.Rankine, Tolerance);
        Assert.Equal(expectedReaumur, temperature.Reaumur, Tolerance);
        Assert.Equal(expectedRomer, temperature.Romer, Tolerance);
    }

    [Theory(DisplayName = "Temperature.FromKelvin should produce the expected result")]
    [InlineData(0.0, -273.15, 0.0, -459.67, 559.725, -90.1395, 0.0, -218.52, -135.90375)]
    [InlineData(273.15, 0.0, 273.15, 32.0, 150.0, 0.0, 491.67, 0.0, 7.5)]
    [InlineData(373.15, 100.0, 373.15, 212.0, 0.0, 33.0, 671.67, 80.0, 60.0)]
    [InlineData(233.15, -40.0, 233.15, -40.0, 210.0, -13.2, 419.67, -32.0, -13.5)]
    [InlineData(293.15, 20.0, 293.15, 68.0, 120.0, 6.6, 527.67, 16.0, 18.0)]
    public void TemperatureFromKelvinShouldProduceExpectedResult(
        double kelvin,
        double expectedCelsius,
        double expectedKelvin,
        double expectedFahrenheit,
        double expectedDelisle,
        double expectedNewton,
        double expectedRankine,
        double expectedReaumur,
        double expectedRomer)
    {
        Temperature<double> temperature = Temperature<double>.FromKelvin(kelvin);

        Assert.Equal(expectedCelsius, temperature.Celsius, Tolerance);
        Assert.Equal(expectedKelvin, temperature.Kelvin, Tolerance);
        Assert.Equal(expectedFahrenheit, temperature.Fahrenheit, Tolerance);
        Assert.Equal(expectedDelisle, temperature.Delisle, Tolerance);
        Assert.Equal(expectedNewton, temperature.Newton, Tolerance);
        Assert.Equal(expectedRankine, temperature.Rankine, Tolerance);
        Assert.Equal(expectedReaumur, temperature.Reaumur, Tolerance);
        Assert.Equal(expectedRomer, temperature.Romer, Tolerance);
    }

    [Theory(DisplayName = "Temperature.FromNewton should produce the expected result")]
    [InlineData(-90.1395, -273.15, 0.0, -459.67, 559.725, -90.1395, 0.0, -218.52, -135.90375)]
    [InlineData(0.0, 0.0, 273.15, 32.0, 150.0, 0.0, 491.67, 0.0, 7.5)]
    [InlineData(33.0, 100.0, 373.15, 212.0, 0.0, 33.0, 671.67, 80.0, 60.0)]
    [InlineData(-13.2, -40.0, 233.15, -40.0, 210.0, -13.2, 419.67, -32.0, -13.5)]
    [InlineData(6.6, 20.0, 293.15, 68.0, 120.0, 6.6, 527.67, 16.0, 18.0)]
    public void TemperatureFromNewtonShouldProduceExpectedResult(
        double newton,
        double expectedCelsius,
        double expectedKelvin,
        double expectedFahrenheit,
        double expectedDelisle,
        double expectedNewton,
        double expectedRankine,
        double expectedReaumur,
        double expectedRomer)
    {
        Temperature<double> temperature = Temperature<double>.FromNewton(newton);

        Assert.Equal(expectedCelsius, temperature.Celsius, Tolerance);
        Assert.Equal(expectedKelvin, temperature.Kelvin, Tolerance);
        Assert.Equal(expectedFahrenheit, temperature.Fahrenheit, Tolerance);
        Assert.Equal(expectedDelisle, temperature.Delisle, Tolerance);
        Assert.Equal(expectedNewton, temperature.Newton, Tolerance);
        Assert.Equal(expectedRankine, temperature.Rankine, Tolerance);
        Assert.Equal(expectedReaumur, temperature.Reaumur, Tolerance);
        Assert.Equal(expectedRomer, temperature.Romer, Tolerance);
    }

    [Theory(DisplayName = "Temperature.FromRankine should produce the expected result")]
    [InlineData(0.0, -273.15, 0.0, -459.67, 559.725, -90.1395, 0.0, -218.52, -135.90375)]
    [InlineData(491.67, 0.0, 273.15, 32.0, 150.0, 0.0, 491.67, 0.0, 7.5)]
    [InlineData(671.67, 100.0, 373.15, 212.0, 0.0, 33.0, 671.67, 80.0, 60.0)]
    [InlineData(419.67, -40.0, 233.15, -40.0, 210.0, -13.2, 419.67, -32.0, -13.5)]
    [InlineData(527.67, 20.0, 293.15, 68.0, 120.0, 6.6, 527.67, 16.0, 18.0)]
    public void TemperatureFromRankineShouldProduceExpectedResult(
        double rankine,
        double expectedCelsius,
        double expectedKelvin,
        double expectedFahrenheit,
        double expectedDelisle,
        double expectedNewton,
        double expectedRankine,
        double expectedReaumur,
        double expectedRomer)
    {
        Temperature<double> temperature = Temperature<double>.FromRankine(rankine);

        Assert.Equal(expectedCelsius, temperature.Celsius, Tolerance);
        Assert.Equal(expectedKelvin, temperature.Kelvin, Tolerance);
        Assert.Equal(expectedFahrenheit, temperature.Fahrenheit, Tolerance);
        Assert.Equal(expectedDelisle, temperature.Delisle, Tolerance);
        Assert.Equal(expectedNewton, temperature.Newton, Tolerance);
        Assert.Equal(expectedRankine, temperature.Rankine, Tolerance);
        Assert.Equal(expectedReaumur, temperature.Reaumur, Tolerance);
        Assert.Equal(expectedRomer, temperature.Romer, Tolerance);
    }

    [Theory(DisplayName = "Temperature.FromReaumur should produce the expected result")]
    [InlineData(-218.52, -273.15, 0.0, -459.67, 559.725, -90.1395, 0.0, -218.52, -135.90375)]
    [InlineData(0.0, 0.0, 273.15, 32.0, 150.0, 0.0, 491.67, 0.0, 7.5)]
    [InlineData(80.0, 100.0, 373.15, 212.0, 0.0, 33.0, 671.67, 80.0, 60.0)]
    [InlineData(-32.0, -40.0, 233.15, -40.0, 210.0, -13.2, 419.67, -32.0, -13.5)]
    [InlineData(16.0, 20.0, 293.15, 68.0, 120.0, 6.6, 527.67, 16.0, 18.0)]
    public void TemperatureFromReaumurShouldProduceExpectedResult(
        double reaumur,
        double expectedCelsius,
        double expectedKelvin,
        double expectedFahrenheit,
        double expectedDelisle,
        double expectedNewton,
        double expectedRankine,
        double expectedReaumur,
        double expectedRomer)
    {
        Temperature<double> temperature = Temperature<double>.FromReaumur(reaumur);

        Assert.Equal(expectedCelsius, temperature.Celsius, Tolerance);
        Assert.Equal(expectedKelvin, temperature.Kelvin, Tolerance);
        Assert.Equal(expectedFahrenheit, temperature.Fahrenheit, Tolerance);
        Assert.Equal(expectedDelisle, temperature.Delisle, Tolerance);
        Assert.Equal(expectedNewton, temperature.Newton, Tolerance);
        Assert.Equal(expectedRankine, temperature.Rankine, Tolerance);
        Assert.Equal(expectedReaumur, temperature.Reaumur, Tolerance);
        Assert.Equal(expectedRomer, temperature.Romer, Tolerance);
    }

    [Theory(DisplayName = "Temperature.FromRomer should produce the expected result")]
    [InlineData(-135.90375, -273.15, 0.0, -459.67, 559.725, -90.1395, 0.0, -218.52, -135.90375)]
    [InlineData(7.5, 0.0, 273.15, 32.0, 150.0, 0.0, 491.67, 0.0, 7.5)]
    [InlineData(60.0, 100.0, 373.15, 212.0, 0.0, 33.0, 671.67, 80.0, 60.0)]
    [InlineData(-13.5, -40.0, 233.15, -40.0, 210.0, -13.2, 419.67, -32.0, -13.5)]
    [InlineData(18.0, 20.0, 293.15, 68.0, 120.0, 6.6, 527.67, 16.0, 18.0)]
    public void TemperatureFromRomerShouldProduceExpectedResult(
        double romer,
        double expectedCelsius,
        double expectedKelvin,
        double expectedFahrenheit,
        double expectedDelisle,
        double expectedNewton,
        double expectedRankine,
        double expectedReaumur,
        double expectedRomer)
    {
        Temperature<double> temperature = Temperature<double>.FromRomer(romer);

        Assert.Equal(expectedCelsius, temperature.Celsius, Tolerance);
        Assert.Equal(expectedKelvin, temperature.Kelvin, Tolerance);
        Assert.Equal(expectedFahrenheit, temperature.Fahrenheit, Tolerance);
        Assert.Equal(expectedDelisle, temperature.Delisle, Tolerance);
        Assert.Equal(expectedNewton, temperature.Newton, Tolerance);
        Assert.Equal(expectedRankine, temperature.Rankine, Tolerance);
        Assert.Equal(expectedReaumur, temperature.Reaumur, Tolerance);
        Assert.Equal(expectedRomer, temperature.Romer, Tolerance);
    }

    [Fact(DisplayName = "Temperature.Add should produce the expected result")]
    public void TemperatureAddShouldProduceExpectedValue()
    {
        // Given
        Temperature<double> left = Temperature<double>.FromKelvin(100.0);
        Temperature<double> right = Temperature<double>.FromKelvin(50.0);

        // When
        Temperature<double> result = left.Add(right);

        // Then
        Assert.Equal(150.0, result.Kelvin, Tolerance);
    }

    [Fact(DisplayName = "Temperature.Subtract should produce the expected result")]
    public void TemperatureSubtractShouldProduceExpectedValue()
    {
        // Given
        Temperature<double> left = Temperature<double>.FromKelvin(100.0);
        Temperature<double> right = Temperature<double>.FromKelvin(40.0);

        // When
        Temperature<double> result = left.Subtract(right);

        // Then
        Assert.Equal(60.0, result.Kelvin, Tolerance);
    }

    [Fact(DisplayName = "Temperature.Multiply should produce the expected result")]
    public void TemperatureMultiplyShouldProduceExpectedValue()
    {
        // Given
        Temperature<double> left = Temperature<double>.FromKelvin(10.0);
        Temperature<double> right = Temperature<double>.FromKelvin(3.0);

        // When
        Temperature<double> result = left.Multiply(right);

        // Then
        Assert.Equal(30.0, result.Kelvin, Tolerance);
    }

    [Fact(DisplayName = "Temperature.Divide should produce the expected result")]
    public void TemperatureDivideShouldProduceExpectedValue()
    {
        // Given
        Temperature<double> left = Temperature<double>.FromKelvin(100.0);
        Temperature<double> right = Temperature<double>.FromKelvin(20.0);

        // When
        Temperature<double> result = left.Divide(right);

        // Then
        Assert.Equal(5.0, result.Kelvin, Tolerance);
    }

    [Fact(DisplayName = "Temperature comparison should produce the expected result (left equal to right)")]
    public void TemperatureComparisonShouldProduceExpectedResultLeftEqualToRight()
    {
        // Given
        Temperature<double> left = Temperature<double>.FromKelvin(123);
        Temperature<double> right = Temperature<double>.FromKelvin(123);

        // When / Then
        Assert.Equal(0, Temperature<double>.Compare(left, right));
        Assert.Equal(0, left.CompareTo(right));
        Assert.Equal(0, left.CompareTo((object)right));
        Assert.False(left > right);
        Assert.True(left >= right);
        Assert.False(left < right);
        Assert.True(left <= right);
    }

    [Fact(DisplayName = "Temperature comparison should produce the expected result (left greater than right)")]
    public void TemperatureComparisonShouldProduceExpectedLeftGreaterThanRight()
    {
        // Given
        Temperature<double> left = Temperature<double>.FromKelvin(456);
        Temperature<double> right = Temperature<double>.FromKelvin(123);

        // When / Then
        Assert.Equal(1, Temperature<double>.Compare(left, right));
        Assert.Equal(1, left.CompareTo(right));
        Assert.Equal(1, left.CompareTo((object)right));
        Assert.True(left > right);
        Assert.True(left >= right);
        Assert.False(left < right);
        Assert.False(left <= right);
    }

    [Fact(DisplayName = "Temperature comparison should produce the expected result (left greater than or equal to right)")]
    public void TemperatureComparisonShouldProduceExpectedLeftGreaterThanOrEqualToRight()
    {
        // Given
        Temperature<double> left = Temperature<double>.FromKelvin(456);
        Temperature<double> right = Temperature<double>.FromKelvin(123);

        // When / Then
        Assert.Equal(1, Temperature<double>.Compare(left, right));
        Assert.Equal(1, left.CompareTo(right));
        Assert.Equal(1, left.CompareTo((object)right));
        Assert.True(left > right);
        Assert.True(left >= right);
        Assert.False(left < right);
        Assert.False(left <= right);
    }

    [Fact(DisplayName = "Temperature comparison should produce the expected result (left less than right)")]
    public void TemperatureComparisonShouldProduceExpectedLeftLessThanRight()
    {
        // Given
        Temperature<double> left = Temperature<double>.FromKelvin(123);
        Temperature<double> right = Temperature<double>.FromKelvin(456);

        // When / Then
        Assert.Equal(-1, Temperature<double>.Compare(left, right));
        Assert.Equal(-1, left.CompareTo(right));
        Assert.Equal(-1, left.CompareTo((object)right));
        Assert.False(left > right);
        Assert.False(left >= right);
        Assert.True(left < right);
        Assert.True(left <= right);
    }

    [Fact(DisplayName = "Temperature comparison should produce the expected result (left less than or equal to right)")]
    public void TemperatureComparisonShouldProduceExpectedLeftLessThanOrEqualToRight()
    {
        // Given
        Temperature<double> left = Temperature<double>.FromKelvin(123);
        Temperature<double> right = Temperature<double>.FromKelvin(456);

        // When / Then
        Assert.Equal(-1, Temperature<double>.Compare(left, right));
        Assert.Equal(-1, left.CompareTo(right));
        Assert.Equal(-1, left.CompareTo((object)right));
        Assert.False(left > right);
        Assert.False(left >= right);
        Assert.True(left < right);
        Assert.True(left <= right);
    }

    [Fact(DisplayName = "Temperature equality should produce the expected result (left equal to right)")]
    public void TemperatureEqualityShouldProduceExpectedResultLeftEqualToRight()
    {
        // Given
        Temperature<double> left = Temperature<double>.FromKelvin(123);
        Temperature<double> right = Temperature<double>.FromKelvin(123);

        // When / Then
        Assert.True(Temperature<double>.Equals(left, right));
        Assert.True(left.Equals(right));
        Assert.True(left.Equals((object)right));
        Assert.True(left == right);
        Assert.False(left != right);
    }

    [Fact(DisplayName = "Temperature equality should produce the expected result (left not equal to right)")]
    public void TemperatureEqualityShouldProduceExpectedResultLeftNotEqualToRight()
    {
        // Given
        Temperature<double> left = Temperature<double>.FromKelvin(123);
        Temperature<double> right = Temperature<double>.FromKelvin(456);

        // When / Then
        Assert.False(Temperature<double>.Equals(left, right));
        Assert.False(left.Equals(right));
        Assert.False(left.Equals((object)right));
        Assert.False(left == right);
        Assert.True(left != right);
    }

    [Fact(DisplayName = "Temperature.ToString should produce the expected result")]
    public void TemperatureToStringShouldProduceExpectedResult()
    {
        // Given / When
        Temperature<double> temperature = Temperature<double>.FromCelsius(100.0);

        // Then
        Assert.Equal("373.150 K", $"{temperature:K3}");
        Assert.Equal("100.000 °C", $"{temperature:C3}");
        Assert.Equal("0.000 °De", $"{temperature:DE3}");
        Assert.Equal("212.000 °F", $"{temperature:F3}");
        Assert.Equal("33.000 °N", $"{temperature:N3}");
        Assert.Equal("671.670 °R", $"{temperature:R3}");
        Assert.Equal("80.000 °Ré", $"{temperature:RE3}");
        Assert.Equal("60.000 °Rø", $"{temperature:RO3}");
    }

    [Fact(DisplayName = "Temperature.ToString should honor custom culture separators")]
    public void TemperatureToStringShouldHonorCustomCulture()
    {
        // Given
        CultureInfo customCulture = new("de-DE");
        Temperature<double> temperature = Temperature<double>.FromKelvin(1234.56);

        // When
        string formatted = temperature.ToString("K2", customCulture);

        // Then
        Assert.Equal("1.234,56 K", formatted);
    }
}
