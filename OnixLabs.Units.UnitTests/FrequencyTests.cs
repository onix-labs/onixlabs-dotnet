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

public sealed class FrequencyTests
{
    // IEEE-754 binary floating-point arithmetic causes small discrepancies in calculation, therefore we need a tolerance.
    private const double Tolerance = 1e+42;

    [Fact(DisplayName = "Frequency.Zero should produce the expected result")]
    public void FrequencyZeroShouldProduceExpectedResult()
    {
        // Given / When
        Frequency<double> frequency = Frequency<double>.Zero;

        // Then
        Assert.Equal(0.0, frequency.QuectoHertz, Tolerance);
    }

    [Theory(DisplayName = "Frequency.FromQuectohertz should produce the expected QuectoHertz")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1.0)]
    [InlineData(2.5, 2.5)]
    public void FrequencyFromQuectohertzShouldProduceExpectedQuectoHertz(double value, double expected)
    {
        Frequency<double> f = Frequency<double>.FromQuectohertz(value);
        Assert.Equal(expected, f.QuectoHertz, Tolerance);
    }

    [Theory(DisplayName = "Frequency.FromRontohertz should produce the expected QuectoHertz")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e3)]
    [InlineData(2.5, 2.5e3)]
    public void FrequencyFromRontohertzShouldProduceExpectedQuectoHertz(double value, double expected)
    {
        Frequency<double> f = Frequency<double>.FromRontohertz(value);
        Assert.Equal(expected, f.QuectoHertz, Tolerance);
    }

    [Theory(DisplayName = "Frequency.FromYoctohertz should produce the expected QuectoHertz")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e6)]
    [InlineData(2.5, 2.5e6)]
    public void FrequencyFromYoctohertzShouldProduceExpectedQuectoHertz(double value, double expected)
    {
        Frequency<double> f = Frequency<double>.FromYoctohertz(value);
        Assert.Equal(expected, f.QuectoHertz, Tolerance);
    }

    [Theory(DisplayName = "Frequency.FromZeptohertz should produce the expected QuectoHertz")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e9)]
    [InlineData(2.5, 2.5e9)]
    public void FrequencyFromZeptohertzShouldProduceExpectedQuectoHertz(double value, double expected)
    {
        Frequency<double> f = Frequency<double>.FromZeptohertz(value);
        Assert.Equal(expected, f.QuectoHertz, Tolerance);
    }

    [Theory(DisplayName = "Frequency.FromAttohertz should produce the expected QuectoHertz")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e12)]
    [InlineData(2.5, 2.5e12)]
    public void FrequencyFromAttohertzShouldProduceExpectedQuectoHertz(double value, double expected)
    {
        Frequency<double> f = Frequency<double>.FromAttohertz(value);
        Assert.Equal(expected, f.QuectoHertz, Tolerance);
    }

    [Theory(DisplayName = "Frequency.FromFemtohertz should produce the expected QuectoHertz")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e15)]
    [InlineData(2.5, 2.5e15)]
    public void FrequencyFromFemtohertzShouldProduceExpectedQuectoHertz(double value, double expected)
    {
        Frequency<double> f = Frequency<double>.FromFemtohertz(value);
        Assert.Equal(expected, f.QuectoHertz, Tolerance);
    }

    [Theory(DisplayName = "Frequency.FromPicohertz should produce the expected QuectoHertz")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e18)]
    [InlineData(2.5, 2.5e18)]
    public void FrequencyFromPicohertzShouldProduceExpectedQuectoHertz(double value, double expected)
    {
        Frequency<double> f = Frequency<double>.FromPicohertz(value);
        Assert.Equal(expected, f.QuectoHertz, Tolerance);
    }

    [Theory(DisplayName = "Frequency.FromNanohertz should produce the expected QuectoHertz")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e21)]
    [InlineData(2.5, 2.5e21)]
    public void FrequencyFromNanohertzShouldProduceExpectedQuectoHertz(double value, double expected)
    {
        Frequency<double> f = Frequency<double>.FromNanohertz(value);
        Assert.Equal(expected, f.QuectoHertz, Tolerance);
    }

    [Theory(DisplayName = "Frequency.FromMicrohertz should produce the expected QuectoHertz")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e24)]
    [InlineData(2.5, 2.5e24)]
    public void FrequencyFromMicrohertzShouldProduceExpectedQuectoHertz(double value, double expected)
    {
        Frequency<double> f = Frequency<double>.FromMicrohertz(value);
        Assert.Equal(expected, f.QuectoHertz, Tolerance);
    }

    [Theory(DisplayName = "Frequency.FromMillihertz should produce the expected QuectoHertz")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e27)]
    [InlineData(2.5, 2.5e27)]
    public void FrequencyFromMillihertzShouldProduceExpectedQuectoHertz(double value, double expected)
    {
        Frequency<double> f = Frequency<double>.FromMillihertz(value);
        Assert.Equal(expected, f.QuectoHertz, Tolerance);
    }

    [Theory(DisplayName = "Frequency.FromCentihertz should produce the expected QuectoHertz")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e28)]
    [InlineData(2.5, 2.5e28)]
    public void FrequencyFromCentihertzShouldProduceExpectedQuectoHertz(double value, double expected)
    {
        Frequency<double> f = Frequency<double>.FromCentihertz(value);
        Assert.Equal(expected, f.QuectoHertz, Tolerance);
    }

    [Theory(DisplayName = "Frequency.FromDecihertz should produce the expected QuectoHertz")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e29)]
    [InlineData(2.5, 2.5e29)]
    public void FrequencyFromDecihertzShouldProduceExpectedQuectoHertz(double value, double expected)
    {
        Frequency<double> f = Frequency<double>.FromDecihertz(value);
        Assert.Equal(expected, f.QuectoHertz, Tolerance);
    }

    [Theory(DisplayName = "Frequency.FromHertz should produce the expected QuectoHertz")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e30)]
    [InlineData(2.5, 2.5e30)]
    public void FrequencyFromHertzShouldProduceExpectedQuectoHertz(double value, double expected)
    {
        Frequency<double> f = Frequency<double>.FromHertz(value);
        Assert.Equal(expected, f.QuectoHertz, Tolerance);
    }

    [Theory(DisplayName = "Frequency.FromDecahertz should produce the expected QuectoHertz")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e31)]
    [InlineData(2.5, 2.5e31)]
    public void FrequencyFromDecahertzShouldProduceExpectedQuectoHertz(double value, double expected)
    {
        Frequency<double> f = Frequency<double>.FromDecahertz(value);
        Assert.Equal(expected, f.QuectoHertz, Tolerance);
    }

    [Theory(DisplayName = "Frequency.FromHectohertz should produce the expected QuectoHertz")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e32)]
    [InlineData(2.5, 2.5e32)]
    public void FrequencyFromHectohertzShouldProduceExpectedQuectoHertz(double value, double expected)
    {
        Frequency<double> f = Frequency<double>.FromHectohertz(value);
        Assert.Equal(expected, f.QuectoHertz, Tolerance);
    }

    [Theory(DisplayName = "Frequency.FromKilohertz should produce the expected QuectoHertz")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e33)]
    [InlineData(2.5, 2.5e33)]
    public void FrequencyFromKilohertzShouldProduceExpectedQuectoHertz(double value, double expected)
    {
        Frequency<double> f = Frequency<double>.FromKilohertz(value);
        Assert.Equal(expected, f.QuectoHertz, Tolerance);
    }

    [Theory(DisplayName = "Frequency.FromMegahertz should produce the expected QuectoHertz")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e36)]
    [InlineData(2.5, 2.5e36)]
    public void FrequencyFromMegahertzShouldProduceExpectedQuectoHertz(double value, double expected)
    {
        Frequency<double> f = Frequency<double>.FromMegahertz(value);
        Assert.Equal(expected, f.QuectoHertz, Tolerance);
    }

    [Theory(DisplayName = "Frequency.FromGigahertz should produce the expected QuectoHertz")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e39)]
    [InlineData(2.5, 2.5e39)]
    public void FrequencyFromGigahertzShouldProduceExpectedQuectoHertz(double value, double expected)
    {
        Frequency<double> f = Frequency<double>.FromGigahertz(value);
        Assert.Equal(expected, f.QuectoHertz, Tolerance);
    }

    [Theory(DisplayName = "Frequency.FromTerahertz should produce the expected QuectoHertz")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e42)]
    [InlineData(2.5, 2.5e42)]
    public void FrequencyFromTerahertzShouldProduceExpectedQuectoHertz(double value, double expected)
    {
        Frequency<double> f = Frequency<double>.FromTerahertz(value);
        Assert.Equal(expected, f.QuectoHertz, Tolerance);
    }

    [Theory(DisplayName = "Frequency.FromPetahertz should produce the expected QuectoHertz")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e45)]
    [InlineData(2.5, 2.5e45)]
    public void FrequencyFromPetahertzShouldProduceExpectedQuectoHertz(double value, double expected)
    {
        Frequency<double> f = Frequency<double>.FromPetahertz(value);
        Assert.Equal(expected, f.QuectoHertz, Tolerance);
    }

    [Theory(DisplayName = "Frequency.FromExahertz should produce the expected QuectoHertz")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e48)]
    [InlineData(2.5, 2.5e48)]
    public void FrequencyFromExahertzShouldProduceExpectedQuectoHertz(double value, double expected)
    {
        Frequency<double> f = Frequency<double>.FromExahertz(value);
        Assert.Equal(expected, f.QuectoHertz, Tolerance);
    }

    [Theory(DisplayName = "Frequency.FromZettahertz should produce the expected QuectoHertz")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e51)]
    [InlineData(2.5, 2.5e51)]
    public void FrequencyFromZettahertzShouldProduceExpectedQuectoHertz(double value, double expected)
    {
        Frequency<double> f = Frequency<double>.FromZettahertz(value);
        Assert.Equal(expected, f.QuectoHertz, Tolerance);
    }

    [Theory(DisplayName = "Frequency.FromYottahertz should produce the expected QuectoHertz")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e54)]
    [InlineData(2.5, 2.5e54)]
    public void FrequencyFromYottahertzShouldProduceExpectedQuectoHertz(double value, double expected)
    {
        Frequency<double> f = Frequency<double>.FromYottahertz(value);
        Assert.Equal(expected, f.QuectoHertz, Tolerance);
    }

    [Theory(DisplayName = "Frequency.FromRonnahertz should produce the expected QuectoHertz")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e57)]
    [InlineData(2.5, 2.5e57)]
    public void FrequencyFromRonnahertzShouldProduceExpectedQuectoHertz(double value, double expected)
    {
        Frequency<double> f = Frequency<double>.FromRonnahertz(value);
        Assert.Equal(expected, f.QuectoHertz, Tolerance);
    }

    [Theory(DisplayName = "Frequency.FromQuettahertz should produce the expected QuectoHertz")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e60)]
    [InlineData(2.5, 2.5e60)]
    public void FrequencyFromQuettahertzShouldProduceExpectedQuectoHertz(double value, double expected)
    {
        Frequency<double> f = Frequency<double>.FromQuettahertz(value);
        Assert.Equal(expected, f.QuectoHertz, Tolerance);
    }

    [Theory(DisplayName = "Frequency.FromRevolutionsPerMinute should produce the expected QuectoHertz")]
    [InlineData(0.0, 0.0)]
    [InlineData(60.0, 1e30)]   // 60 rpm = 1 Hz
    [InlineData(120.0, 2e30)]  // 120 rpm = 2 Hz
    [InlineData(3000.0, 50e30)]
    public void FrequencyFromRevolutionsPerMinuteShouldProduceExpectedQuectoHertz(double value, double expected)
    {
        Frequency<double> f = Frequency<double>.FromRevolutionsPerMinute(value);
        Assert.Equal(expected, f.QuectoHertz, Tolerance);
    }

    [Theory(DisplayName = "Frequency.FromBeatsPerMinute should produce the expected QuectoHertz")]
    [InlineData(0.0, 0.0)]
    [InlineData(60.0, 1e30)]    // 60 bpm = 1 Hz
    [InlineData(120.0, 2e30)]   // 120 bpm = 2 Hz (typical dance tempo)
    [InlineData(72.0, 1.2e30)]  // resting heart rate
    public void FrequencyFromBeatsPerMinuteShouldProduceExpectedQuectoHertz(double value, double expected)
    {
        Frequency<double> f = Frequency<double>.FromBeatsPerMinute(value);
        Assert.Equal(expected, f.QuectoHertz, Tolerance);
    }

    [Theory(DisplayName = "Frequency.FromRadiansPerSecond should produce the expected QuectoHertz")]
    [InlineData(0.0)]
    [InlineData(1.0)]
    [InlineData(2.5)]
    public void FrequencyFromRadiansPerSecondShouldProduceExpectedQuectoHertz(double value)
    {
        // Given: 1 rad/s = 1/(2π) Hz, so qHz = value × 1e30 / (2π)
        double expected = value * 1e30 / (2.0 * Math.PI);

        // When
        Frequency<double> f = Frequency<double>.FromRadiansPerSecond(value);

        // Then
        Assert.Equal(expected, f.QuectoHertz, Tolerance);
    }

    [Fact(DisplayName = "Frequency.RevolutionsPerMinute conversion roundtrips through Hertz")]
    public void FrequencyRevolutionsPerMinuteRoundtripsThroughHertz()
    {
        // Given
        Frequency<double> f = Frequency<double>.FromHertz(1.0);

        // Then
        Assert.Equal(60.0, f.RevolutionsPerMinute, 1e-9);
        Assert.Equal(60.0, f.BeatsPerMinute, 1e-9);
    }

    [Fact(DisplayName = "Frequency.RadiansPerSecond conversion roundtrips through Hertz")]
    public void FrequencyRadiansPerSecondRoundtripsThroughHertz()
    {
        // Given
        Frequency<double> f = Frequency<double>.FromHertz(1.0);

        // Then
        Assert.Equal(2.0 * Math.PI, f.RadiansPerSecond, 1e-9);
    }

    [Fact(DisplayName = "Frequency.Add should produce the expected result")]
    public void FrequencyAddShouldProduceExpectedValue()
    {
        // Given
        Frequency<double> left = Frequency<double>.FromKilohertz(1.5);
        Frequency<double> right = Frequency<double>.FromKilohertz(0.5);

        // When
        Frequency<double> result = left.Add(right);

        // Then
        Assert.Equal(2.0, result.KiloHertz, Tolerance);
    }

    [Fact(DisplayName = "Frequency.Subtract should produce the expected result")]
    public void FrequencySubtractShouldProduceExpectedValue()
    {
        // Given
        Frequency<double> left = Frequency<double>.FromKilohertz(1.5);
        Frequency<double> right = Frequency<double>.FromKilohertz(0.4);

        // When
        Frequency<double> result = left.Subtract(right);

        // Then
        Assert.Equal(1.1, result.KiloHertz, Tolerance);
    }

    [Fact(DisplayName = "Frequency.Multiply should produce the expected result")]
    public void FrequencyMultiplyShouldProduceExpectedValue()
    {
        // Given
        Frequency<double> left = Frequency<double>.FromHertz(10.0);  // 1e31 qHz
        Frequency<double> right = Frequency<double>.FromHertz(3.0);  // 3e30 qHz

        // When
        Frequency<double> result = left.Multiply(right);  // 1e31 * 3e30 = 3e61 qHz

        // Then
        Assert.Equal(1e31, left.QuectoHertz, Tolerance);
        Assert.Equal(3e30, right.QuectoHertz, Tolerance);
        Assert.Equal(3e61, result.QuectoHertz, Tolerance);
    }

    [Fact(DisplayName = "Frequency.Divide should produce the expected result")]
    public void FrequencyDivideShouldProduceExpectedValue()
    {
        // Given
        Frequency<double> left = Frequency<double>.FromHertz(100.0);  // 1e32 qHz
        Frequency<double> right = Frequency<double>.FromHertz(20.0);  // 2e31 qHz

        // When
        Frequency<double> result = left.Divide(right);  // 1e32 / 2e31 = 5 qHz

        // Then
        Assert.Equal(5.0, result.QuectoHertz, Tolerance);
        Assert.Equal(5e-30, result.Hertz, Tolerance);
    }

    [Fact(DisplayName = "Frequency comparison should produce the expected result (left equal to right)")]
    public void FrequencyComparisonShouldProduceExpectedResultLeftEqualToRight()
    {
        // Given
        Frequency<double> left = Frequency<double>.FromKilohertz(123.0);
        Frequency<double> right = Frequency<double>.FromKilohertz(123.0);

        // When / Then
        Assert.Equal(0, Frequency<double>.Compare(left, right));
        Assert.Equal(0, left.CompareTo(right));
        Assert.Equal(0, left.CompareTo((object)right));
        Assert.False(left > right);
        Assert.True(left >= right);
        Assert.False(left < right);
        Assert.True(left <= right);
    }

    [Fact(DisplayName = "Frequency comparison should produce the expected result (left greater than right)")]
    public void FrequencyComparisonShouldProduceExpectedLeftGreaterThanRight()
    {
        // Given
        Frequency<double> left = Frequency<double>.FromKilohertz(456.0);
        Frequency<double> right = Frequency<double>.FromKilohertz(123.0);

        // When / Then
        Assert.Equal(1, Frequency<double>.Compare(left, right));
        Assert.Equal(1, left.CompareTo(right));
        Assert.Equal(1, left.CompareTo((object)right));
        Assert.True(left > right);
        Assert.True(left >= right);
        Assert.False(left < right);
        Assert.False(left <= right);
    }

    [Fact(DisplayName = "Frequency comparison should produce the expected result (left less than right)")]
    public void FrequencyComparisonShouldProduceExpectedLeftLessThanRight()
    {
        // Given
        Frequency<double> left = Frequency<double>.FromKilohertz(123.0);
        Frequency<double> right = Frequency<double>.FromKilohertz(456.0);

        // When / Then
        Assert.Equal(-1, Frequency<double>.Compare(left, right));
        Assert.Equal(-1, left.CompareTo(right));
        Assert.Equal(-1, left.CompareTo((object)right));
        Assert.False(left > right);
        Assert.False(left >= right);
        Assert.True(left < right);
        Assert.True(left <= right);
    }

    [Fact(DisplayName = "Frequency equality should produce the expected result (left equal to right)")]
    public void FrequencyEqualityShouldProduceExpectedResultLeftEqualToRight()
    {
        // Given
        Frequency<BigDecimal> left = Frequency<BigDecimal>.FromKilohertz(2.0);
        Frequency<BigDecimal> right = Frequency<BigDecimal>.FromHertz(2000.0);

        // When / Then
        Assert.True(Frequency<BigDecimal>.Equals(left, right));
        Assert.True(left.Equals(right));
        Assert.True(left.Equals((object)right));
        Assert.True(left == right);
        Assert.False(left != right);
    }

    [Fact(DisplayName = "Frequency equality should produce the expected result (left not equal to right)")]
    public void FrequencyEqualityShouldProduceExpectedResultLeftNotEqualToRight()
    {
        // Given
        Frequency<double> left = Frequency<double>.FromKilohertz(2.0);
        Frequency<double> right = Frequency<double>.FromHertz(2500.0);

        // When / Then
        Assert.False(Frequency<double>.Equals(left, right));
        Assert.False(left.Equals(right));
        Assert.False(left.Equals((object)right));
        Assert.False(left == right);
        Assert.True(left != right);
    }

    [Fact(DisplayName = "Frequency.ToString should produce the expected result")]
    public void FrequencyToStringShouldProduceExpectedResult()
    {
        // Given
        Frequency<double> f = Frequency<double>.FromHertz(1000.0);

        // When / Then
        Assert.Equal("1,000.000 Hz", $"{f:Hz3}");
        Assert.Equal("1.000 kHz", $"{f:kHz3}");
        Assert.Equal("0.001 MHz", $"{f:MHz3}");
        Assert.Equal("1,000,000.000 mHz", $"{f:mHz3}");
        Assert.Equal("60,000.000 rpm", $"{f:rpm3}");
        Assert.Equal("60,000.000 bpm", $"{f:bpm3}");
    }

    [Fact(DisplayName = "Frequency.ToString MHz vs mHz are case-sensitive")]
    public void FrequencyToStringMhzVsMhzAreCaseSensitive()
    {
        // Given
        Frequency<double> f = Frequency<double>.FromHertz(1.0);

        // Then
        Assert.Equal("0.000001 MHz", $"{f:MHz6}"); // mega
        Assert.Equal("1,000.000 mHz", $"{f:mHz3}"); // milli
    }

    [Fact(DisplayName = "Frequency.ToString rad/s should use proper unit symbol")]
    public void FrequencyToStringRadiansPerSecondShouldUseProperUnitSymbol()
    {
        // Given
        Frequency<double> f = Frequency<double>.FromHertz(1.0);

        // Then
        Assert.Equal("6.283185 rad/s", $"{f:radps6}");
    }

    [Fact(DisplayName = "Frequency.ToString should honor custom culture separators")]
    public void FrequencyToStringShouldHonorCustomCulture()
    {
        // Given
        CultureInfo customCulture = new("de-DE");
        Frequency<double> f = Frequency<double>.FromKilohertz(1234.56);

        // When
        string formatted = f.ToString("kHz2", customCulture);

        // Then
        Assert.Equal("1.234,56 kHz", formatted);
    }
}
