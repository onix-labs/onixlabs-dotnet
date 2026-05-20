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

public sealed class TemperatureTests
{
    // Temperature conversion math (e.g. Fahrenheit = Kelvin * 9/5 - 459.67) accumulates a small number
    // of round-to-nearest steps in T's precision. For Float128 (~33-34 decimal digits), the per-step
    // ULP at the magnitudes used here (~10^2 to 10^3) is around 1e-30. Strict bitwise equality between
    // (closest-Float128 to "X.YZ" via Parse) and (the result of the unit's arithmetic chain) would
    // require both paths to round identically at every intermediate step — which they don't. The
    // tolerance below sits a couple of orders of magnitude above one ULP at these scales, so it
    // catches genuine bugs but accepts the inherent 1–2 ULP drift of IEEE-754 binary arithmetic.
    private static readonly Float128 Tolerance = Float128.Parse("1e-28");

    private static void AssertNearlyEqual(Float128 expected, Float128 actual)
    {
        Float128 diff = Float128.Abs(expected - actual);
        Assert.True(
            diff <= Tolerance,
            $"Expected: {expected}\nActual:   {actual}\nDiff:     {diff} exceeds tolerance {Tolerance}");
    }

    [Fact(DisplayName = "Temperature.Zero should produce the expected result")]
    public void TemperatureZeroShouldProduceExpectedResult()
    {
        // Given / When
        Temperature<Float128> temperature = Temperature<Float128>.Zero;

        // Then
        AssertNearlyEqual(Float128.Parse("-273.15"), temperature.Celsius);
        Assert.Equal(Float128.Zero, temperature.Kelvin);
        AssertNearlyEqual(Float128.Parse("-459.67"), temperature.Fahrenheit);
        AssertNearlyEqual(Float128.Parse("559.725"), temperature.Delisle);
        AssertNearlyEqual(Float128.Parse("-90.1395"), temperature.Newton);
        Assert.Equal(Float128.Zero, temperature.Rankine);
        AssertNearlyEqual(Float128.Parse("-218.52"), temperature.Reaumur);
        AssertNearlyEqual(Float128.Parse("-135.90375"), temperature.Romer);
    }

    [Theory(DisplayName = "Temperature.FromCelsius should produce the expected result")]
    [InlineData("-273.15", "-273.15", "0", "-459.67", "559.725", "-90.1395", "0", "-218.52", "-135.90375")]
    [InlineData("0", "0", "273.15", "32", "150", "0", "491.67", "0", "7.5")]
    [InlineData("100", "100", "373.15", "212", "0", "33", "671.67", "80", "60")]
    [InlineData("-40", "-40", "233.15", "-40", "210", "-13.2", "419.67", "-32", "-13.5")]
    [InlineData("20", "20", "293.15", "68", "120", "6.6", "527.67", "16", "18")]
    public void TemperatureFromCelsiusShouldProduceExpectedResult(
        string celsius,
        string expectedCelsius,
        string expectedKelvin,
        string expectedFahrenheit,
        string expectedDelisle,
        string expectedNewton,
        string expectedRankine,
        string expectedReaumur,
        string expectedRomer)
    {
        // When
        Temperature<Float128> temperature = Temperature<Float128>.FromCelsius(Float128.Parse(celsius));

        // Then
        AssertNearlyEqual(Float128.Parse(expectedCelsius), temperature.Celsius);
        AssertNearlyEqual(Float128.Parse(expectedKelvin), temperature.Kelvin);
        AssertNearlyEqual(Float128.Parse(expectedFahrenheit), temperature.Fahrenheit);
        AssertNearlyEqual(Float128.Parse(expectedDelisle), temperature.Delisle);
        AssertNearlyEqual(Float128.Parse(expectedNewton), temperature.Newton);
        AssertNearlyEqual(Float128.Parse(expectedRankine), temperature.Rankine);
        AssertNearlyEqual(Float128.Parse(expectedReaumur), temperature.Reaumur);
        AssertNearlyEqual(Float128.Parse(expectedRomer), temperature.Romer);
    }

    [Theory(DisplayName = "Temperature.FromDelisle should produce the expected result")]
    [InlineData("559.725", "-273.15", "0", "-459.67", "559.725", "-90.1395", "0", "-218.52", "-135.90375")]
    [InlineData("150", "0", "273.15", "32", "150", "0", "491.67", "0", "7.5")]
    [InlineData("0", "100", "373.15", "212", "0", "33", "671.67", "80", "60")]
    [InlineData("210", "-40", "233.15", "-40", "210", "-13.2", "419.67", "-32", "-13.5")]
    [InlineData("120", "20", "293.15", "68", "120", "6.6", "527.67", "16", "18")]
    public void TemperatureFromDelisleShouldProduceExpectedResult(
        string delisle,
        string expectedCelsius,
        string expectedKelvin,
        string expectedFahrenheit,
        string expectedDelisle,
        string expectedNewton,
        string expectedRankine,
        string expectedReaumur,
        string expectedRomer)
    {
        // When
        Temperature<Float128> temperature = Temperature<Float128>.FromDelisle(Float128.Parse(delisle));

        // Then
        AssertNearlyEqual(Float128.Parse(expectedCelsius), temperature.Celsius);
        AssertNearlyEqual(Float128.Parse(expectedKelvin), temperature.Kelvin);
        AssertNearlyEqual(Float128.Parse(expectedFahrenheit), temperature.Fahrenheit);
        AssertNearlyEqual(Float128.Parse(expectedDelisle), temperature.Delisle);
        AssertNearlyEqual(Float128.Parse(expectedNewton), temperature.Newton);
        AssertNearlyEqual(Float128.Parse(expectedRankine), temperature.Rankine);
        AssertNearlyEqual(Float128.Parse(expectedReaumur), temperature.Reaumur);
        AssertNearlyEqual(Float128.Parse(expectedRomer), temperature.Romer);
    }

    [Theory(DisplayName = "Temperature.FromFahrenheit should produce the expected result")]
    [InlineData("-459.67", "-273.15", "0", "-459.67", "559.725", "-90.1395", "0", "-218.52", "-135.90375")]
    [InlineData("32", "0", "273.15", "32", "150", "0", "491.67", "0", "7.5")]
    [InlineData("212", "100", "373.15", "212", "0", "33", "671.67", "80", "60")]
    [InlineData("-40", "-40", "233.15", "-40", "210", "-13.2", "419.67", "-32", "-13.5")]
    [InlineData("68", "20", "293.15", "68", "120", "6.6", "527.67", "16", "18")]
    public void TemperatureFromFahrenheitShouldProduceExpectedResult(
        string fahrenheit,
        string expectedCelsius,
        string expectedKelvin,
        string expectedFahrenheit,
        string expectedDelisle,
        string expectedNewton,
        string expectedRankine,
        string expectedReaumur,
        string expectedRomer)
    {
        Temperature<Float128> temperature = Temperature<Float128>.FromFahrenheit(Float128.Parse(fahrenheit));

        AssertNearlyEqual(Float128.Parse(expectedCelsius), temperature.Celsius);
        AssertNearlyEqual(Float128.Parse(expectedKelvin), temperature.Kelvin);
        AssertNearlyEqual(Float128.Parse(expectedFahrenheit), temperature.Fahrenheit);
        AssertNearlyEqual(Float128.Parse(expectedDelisle), temperature.Delisle);
        AssertNearlyEqual(Float128.Parse(expectedNewton), temperature.Newton);
        AssertNearlyEqual(Float128.Parse(expectedRankine), temperature.Rankine);
        AssertNearlyEqual(Float128.Parse(expectedReaumur), temperature.Reaumur);
        AssertNearlyEqual(Float128.Parse(expectedRomer), temperature.Romer);
    }

    [Theory(DisplayName = "Temperature.FromKelvin should produce the expected result")]
    [InlineData("0", "-273.15", "0", "-459.67", "559.725", "-90.1395", "0", "-218.52", "-135.90375")]
    [InlineData("273.15", "0", "273.15", "32", "150", "0", "491.67", "0", "7.5")]
    [InlineData("373.15", "100", "373.15", "212", "0", "33", "671.67", "80", "60")]
    [InlineData("233.15", "-40", "233.15", "-40", "210", "-13.2", "419.67", "-32", "-13.5")]
    [InlineData("293.15", "20", "293.15", "68", "120", "6.6", "527.67", "16", "18")]
    public void TemperatureFromKelvinShouldProduceExpectedResult(
        string kelvin,
        string expectedCelsius,
        string expectedKelvin,
        string expectedFahrenheit,
        string expectedDelisle,
        string expectedNewton,
        string expectedRankine,
        string expectedReaumur,
        string expectedRomer)
    {
        Temperature<Float128> temperature = Temperature<Float128>.FromKelvin(Float128.Parse(kelvin));

        AssertNearlyEqual(Float128.Parse(expectedCelsius), temperature.Celsius);
        AssertNearlyEqual(Float128.Parse(expectedKelvin), temperature.Kelvin);
        AssertNearlyEqual(Float128.Parse(expectedFahrenheit), temperature.Fahrenheit);
        AssertNearlyEqual(Float128.Parse(expectedDelisle), temperature.Delisle);
        AssertNearlyEqual(Float128.Parse(expectedNewton), temperature.Newton);
        AssertNearlyEqual(Float128.Parse(expectedRankine), temperature.Rankine);
        AssertNearlyEqual(Float128.Parse(expectedReaumur), temperature.Reaumur);
        AssertNearlyEqual(Float128.Parse(expectedRomer), temperature.Romer);
    }

    [Theory(DisplayName = "Temperature.FromNewton should produce the expected result")]
    [InlineData("-90.1395", "-273.15", "0", "-459.67", "559.725", "-90.1395", "0", "-218.52", "-135.90375")]
    [InlineData("0", "0", "273.15", "32", "150", "0", "491.67", "0", "7.5")]
    [InlineData("33", "100", "373.15", "212", "0", "33", "671.67", "80", "60")]
    [InlineData("-13.2", "-40", "233.15", "-40", "210", "-13.2", "419.67", "-32", "-13.5")]
    [InlineData("6.6", "20", "293.15", "68", "120", "6.6", "527.67", "16", "18")]
    public void TemperatureFromNewtonShouldProduceExpectedResult(
        string newton,
        string expectedCelsius,
        string expectedKelvin,
        string expectedFahrenheit,
        string expectedDelisle,
        string expectedNewton,
        string expectedRankine,
        string expectedReaumur,
        string expectedRomer)
    {
        Temperature<Float128> temperature = Temperature<Float128>.FromNewton(Float128.Parse(newton));

        AssertNearlyEqual(Float128.Parse(expectedCelsius), temperature.Celsius);
        AssertNearlyEqual(Float128.Parse(expectedKelvin), temperature.Kelvin);
        AssertNearlyEqual(Float128.Parse(expectedFahrenheit), temperature.Fahrenheit);
        AssertNearlyEqual(Float128.Parse(expectedDelisle), temperature.Delisle);
        AssertNearlyEqual(Float128.Parse(expectedNewton), temperature.Newton);
        AssertNearlyEqual(Float128.Parse(expectedRankine), temperature.Rankine);
        AssertNearlyEqual(Float128.Parse(expectedReaumur), temperature.Reaumur);
        AssertNearlyEqual(Float128.Parse(expectedRomer), temperature.Romer);
    }

    [Theory(DisplayName = "Temperature.FromRankine should produce the expected result")]
    [InlineData("0", "-273.15", "0", "-459.67", "559.725", "-90.1395", "0", "-218.52", "-135.90375")]
    [InlineData("491.67", "0", "273.15", "32", "150", "0", "491.67", "0", "7.5")]
    [InlineData("671.67", "100", "373.15", "212", "0", "33", "671.67", "80", "60")]
    [InlineData("419.67", "-40", "233.15", "-40", "210", "-13.2", "419.67", "-32", "-13.5")]
    [InlineData("527.67", "20", "293.15", "68", "120", "6.6", "527.67", "16", "18")]
    public void TemperatureFromRankineShouldProduceExpectedResult(
        string rankine,
        string expectedCelsius,
        string expectedKelvin,
        string expectedFahrenheit,
        string expectedDelisle,
        string expectedNewton,
        string expectedRankine,
        string expectedReaumur,
        string expectedRomer)
    {
        Temperature<Float128> temperature = Temperature<Float128>.FromRankine(Float128.Parse(rankine));

        AssertNearlyEqual(Float128.Parse(expectedCelsius), temperature.Celsius);
        AssertNearlyEqual(Float128.Parse(expectedKelvin), temperature.Kelvin);
        AssertNearlyEqual(Float128.Parse(expectedFahrenheit), temperature.Fahrenheit);
        AssertNearlyEqual(Float128.Parse(expectedDelisle), temperature.Delisle);
        AssertNearlyEqual(Float128.Parse(expectedNewton), temperature.Newton);
        AssertNearlyEqual(Float128.Parse(expectedRankine), temperature.Rankine);
        AssertNearlyEqual(Float128.Parse(expectedReaumur), temperature.Reaumur);
        AssertNearlyEqual(Float128.Parse(expectedRomer), temperature.Romer);
    }

    [Theory(DisplayName = "Temperature.FromReaumur should produce the expected result")]
    [InlineData("-218.52", "-273.15", "0", "-459.67", "559.725", "-90.1395", "0", "-218.52", "-135.90375")]
    [InlineData("0", "0", "273.15", "32", "150", "0", "491.67", "0", "7.5")]
    [InlineData("80", "100", "373.15", "212", "0", "33", "671.67", "80", "60")]
    [InlineData("-32", "-40", "233.15", "-40", "210", "-13.2", "419.67", "-32", "-13.5")]
    [InlineData("16", "20", "293.15", "68", "120", "6.6", "527.67", "16", "18")]
    public void TemperatureFromReaumurShouldProduceExpectedResult(
        string reaumur,
        string expectedCelsius,
        string expectedKelvin,
        string expectedFahrenheit,
        string expectedDelisle,
        string expectedNewton,
        string expectedRankine,
        string expectedReaumur,
        string expectedRomer)
    {
        Temperature<Float128> temperature = Temperature<Float128>.FromReaumur(Float128.Parse(reaumur));

        AssertNearlyEqual(Float128.Parse(expectedCelsius), temperature.Celsius);
        AssertNearlyEqual(Float128.Parse(expectedKelvin), temperature.Kelvin);
        AssertNearlyEqual(Float128.Parse(expectedFahrenheit), temperature.Fahrenheit);
        AssertNearlyEqual(Float128.Parse(expectedDelisle), temperature.Delisle);
        AssertNearlyEqual(Float128.Parse(expectedNewton), temperature.Newton);
        AssertNearlyEqual(Float128.Parse(expectedRankine), temperature.Rankine);
        AssertNearlyEqual(Float128.Parse(expectedReaumur), temperature.Reaumur);
        AssertNearlyEqual(Float128.Parse(expectedRomer), temperature.Romer);
    }

    [Theory(DisplayName = "Temperature.FromRomer should produce the expected result")]
    [InlineData("-135.90375", "-273.15", "0", "-459.67", "559.725", "-90.1395", "0", "-218.52", "-135.90375")]
    [InlineData("7.5", "0", "273.15", "32", "150", "0", "491.67", "0", "7.5")]
    [InlineData("60", "100", "373.15", "212", "0", "33", "671.67", "80", "60")]
    [InlineData("-13.5", "-40", "233.15", "-40", "210", "-13.2", "419.67", "-32", "-13.5")]
    [InlineData("18", "20", "293.15", "68", "120", "6.6", "527.67", "16", "18")]
    public void TemperatureFromRomerShouldProduceExpectedResult(
        string romer,
        string expectedCelsius,
        string expectedKelvin,
        string expectedFahrenheit,
        string expectedDelisle,
        string expectedNewton,
        string expectedRankine,
        string expectedReaumur,
        string expectedRomer)
    {
        Temperature<Float128> temperature = Temperature<Float128>.FromRomer(Float128.Parse(romer));

        AssertNearlyEqual(Float128.Parse(expectedCelsius), temperature.Celsius);
        AssertNearlyEqual(Float128.Parse(expectedKelvin), temperature.Kelvin);
        AssertNearlyEqual(Float128.Parse(expectedFahrenheit), temperature.Fahrenheit);
        AssertNearlyEqual(Float128.Parse(expectedDelisle), temperature.Delisle);
        AssertNearlyEqual(Float128.Parse(expectedNewton), temperature.Newton);
        AssertNearlyEqual(Float128.Parse(expectedRankine), temperature.Rankine);
        AssertNearlyEqual(Float128.Parse(expectedReaumur), temperature.Reaumur);
        AssertNearlyEqual(Float128.Parse(expectedRomer), temperature.Romer);
    }

    [Fact(DisplayName = "Temperature.Add should produce the expected result")]
    public void TemperatureAddShouldProduceExpectedValue()
    {
        // Given
        Temperature<Float128> left = Temperature<Float128>.FromKelvin(Float128.Parse("100"));
        Temperature<Float128> right = Temperature<Float128>.FromKelvin(Float128.Parse("50"));

        // When
        Temperature<Float128> result = left.Add(right);

        // Then
        Assert.Equal(Float128.Parse("150"), result.Kelvin);
    }

    [Fact(DisplayName = "Temperature.Subtract should produce the expected result")]
    public void TemperatureSubtractShouldProduceExpectedValue()
    {
        // Given
        Temperature<Float128> left = Temperature<Float128>.FromKelvin(Float128.Parse("100"));
        Temperature<Float128> right = Temperature<Float128>.FromKelvin(Float128.Parse("40"));

        // When
        Temperature<Float128> result = left.Subtract(right);

        // Then
        Assert.Equal(Float128.Parse("60"), result.Kelvin);
    }

    [Fact(DisplayName = "Temperature comparison should produce the expected result (left equal to right)")]
    public void TemperatureComparisonShouldProduceExpectedResultLeftEqualToRight()
    {
        // Given
        Temperature<Float128> left = Temperature<Float128>.FromKelvin(Float128.Parse("123"));
        Temperature<Float128> right = Temperature<Float128>.FromKelvin(Float128.Parse("123"));

        // When / Then
        Assert.Equal(0, Temperature<Float128>.Compare(left, right));
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
        Temperature<Float128> left = Temperature<Float128>.FromKelvin(Float128.Parse("456"));
        Temperature<Float128> right = Temperature<Float128>.FromKelvin(Float128.Parse("123"));

        // When / Then
        Assert.Equal(1, Temperature<Float128>.Compare(left, right));
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
        Temperature<Float128> left = Temperature<Float128>.FromKelvin(Float128.Parse("456"));
        Temperature<Float128> right = Temperature<Float128>.FromKelvin(Float128.Parse("123"));

        // When / Then
        Assert.Equal(1, Temperature<Float128>.Compare(left, right));
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
        Temperature<Float128> left = Temperature<Float128>.FromKelvin(Float128.Parse("123"));
        Temperature<Float128> right = Temperature<Float128>.FromKelvin(Float128.Parse("456"));

        // When / Then
        Assert.Equal(-1, Temperature<Float128>.Compare(left, right));
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
        Temperature<Float128> left = Temperature<Float128>.FromKelvin(Float128.Parse("123"));
        Temperature<Float128> right = Temperature<Float128>.FromKelvin(Float128.Parse("456"));

        // When / Then
        Assert.Equal(-1, Temperature<Float128>.Compare(left, right));
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
        Temperature<Float128> left = Temperature<Float128>.FromKelvin(Float128.Parse("123"));
        Temperature<Float128> right = Temperature<Float128>.FromKelvin(Float128.Parse("123"));

        // When / Then
        Assert.True(Temperature<Float128>.Equals(left, right));
        Assert.True(left.Equals(right));
        Assert.True(left.Equals((object)right));
        Assert.True(left == right);
        Assert.False(left != right);
    }

    [Fact(DisplayName = "Temperature equality should produce the expected result (left not equal to right)")]
    public void TemperatureEqualityShouldProduceExpectedResultLeftNotEqualToRight()
    {
        // Given
        Temperature<Float128> left = Temperature<Float128>.FromKelvin(Float128.Parse("123"));
        Temperature<Float128> right = Temperature<Float128>.FromKelvin(Float128.Parse("456"));

        // When / Then
        Assert.False(Temperature<Float128>.Equals(left, right));
        Assert.False(left.Equals(right));
        Assert.False(left.Equals((object)right));
        Assert.False(left == right);
        Assert.True(left != right);
    }

    [Fact(DisplayName = "Temperature.ToString should produce the expected result")]
    public void TemperatureToStringShouldProduceExpectedResult()
    {
        // Given / When
        Temperature<Float128> temperature = Temperature<Float128>.FromCelsius(Float128.Parse("100"));

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
        Temperature<Float128> temperature = Temperature<Float128>.FromKelvin(Float128.Parse("1234.56"));

        // When
        string formatted = temperature.ToString("K2", customCulture);

        // Then
        Assert.Equal("1.234,56 K", formatted);
    }
}
