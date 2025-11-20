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
        Assert.Equal(0.0, frequency.YoctoHertz, Tolerance);
    }

    [Theory(DisplayName = "Frequency.FromYoctoHertz should produce the expected YoctoHertz")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1.0)]
    [InlineData(2.5, 2.5)]
    public void FrequencyFromYoctoHertzShouldProduceExpectedYoctoHertz(double value, double expectedYoctoHertz)
    {
        // Given / When
        Frequency<double> frequency = Frequency<double>.FromYoctoHertz(value);

        // Then
        Assert.Equal(expectedYoctoHertz, frequency.YoctoHertz, Tolerance);
    }

    [Theory(DisplayName = "Frequency.FromZeptoHertz should produce the expected YoctoHertz")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e3)]
    [InlineData(2.5, 2.5e3)]
    public void FrequencyFromZeptoHertzShouldProduceExpectedYoctoHertz(double value, double expectedYoctoHertz)
    {
        // Given / When
        Frequency<double> frequency = Frequency<double>.FromZeptoHertz(value);

        // Then
        Assert.Equal(expectedYoctoHertz, frequency.YoctoHertz, Tolerance);
    }

    [Theory(DisplayName = "Frequency.FromAttoHertz should produce the expected YoctoHertz")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e6)]
    [InlineData(2.5, 2.5e6)]
    public void FrequencyFromAttoHertzShouldProduceExpectedYoctoHertz(double value, double expectedYoctoHertz)
    {
        // Given / When
        Frequency<double> frequency = Frequency<double>.FromAttoHertz(value);

        // Then
        Assert.Equal(expectedYoctoHertz, frequency.YoctoHertz, Tolerance);
    }

    [Theory(DisplayName = "Frequency.FromFemtoHertz should produce the expected YoctoHertz")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e9)]
    [InlineData(2.5, 2.5e9)]
    public void FrequencyFromFemtoHertzShouldProduceExpectedYoctoHertz(double value, double expectedYoctoHertz)
    {
        // Given / When
        Frequency<double> frequency = Frequency<double>.FromFemtoHertz(value);

        // Then
        Assert.Equal(expectedYoctoHertz, frequency.YoctoHertz, Tolerance);
    }

    [Theory(DisplayName = "Frequency.FromPicoHertz should produce the expected YoctoHertz")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e12)]
    [InlineData(2.5, 2.5e12)]
    public void FrequencyFromPicoHertzShouldProduceExpectedYoctoHertz(double value, double expectedYoctoHertz)
    {
        // Given / When
        Frequency<double> frequency = Frequency<double>.FromPicoHertz(value);

        // Then
        Assert.Equal(expectedYoctoHertz, frequency.YoctoHertz, Tolerance);
    }

    [Theory(DisplayName = "Frequency.FromNanoHertz should produce the expected YoctoHertz")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e15)]
    [InlineData(2.5, 2.5e15)]
    public void FrequencyFromNanoHertzShouldProduceExpectedYoctoHertz(double value, double expectedYoctoHertz)
    {
        // Given / When
        Frequency<double> frequency = Frequency<double>.FromNanoHertz(value);

        // Then
        Assert.Equal(expectedYoctoHertz, frequency.YoctoHertz, Tolerance);
    }

    [Theory(DisplayName = "Frequency.FromMicroHertz should produce the expected YoctoHertz")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e18)]
    [InlineData(2.5, 2.5e18)]
    public void FrequencyFromMicroHertzShouldProduceExpectedYoctoHertz(double value, double expectedYoctoHertz)
    {
        // Given / When
        Frequency<double> frequency = Frequency<double>.FromMicroHertz(value);

        // Then
        Assert.Equal(expectedYoctoHertz, frequency.YoctoHertz, Tolerance);
    }

    [Theory(DisplayName = "Frequency.FromMilliHertz should produce the expected YoctoHertz")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e21)]
    [InlineData(2.5, 2.5e21)]
    public void FrequencyFromMilliHertzShouldProduceExpectedYoctoHertz(double value, double expectedYoctoHertz)
    {
        // Given / When
        Frequency<double> frequency = Frequency<double>.FromMilliHertz(value);

        // Then
        Assert.Equal(expectedYoctoHertz, frequency.YoctoHertz, Tolerance);
    }

    [Theory(DisplayName = "Frequency.FromHertz should produce the expected YoctoHertz")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e24)]
    [InlineData(2.5, 2.5e24)]
    public void FrequencyFromHertzShouldProduceExpectedYoctoHertz(double value, double expectedYoctoHertz)
    {
        // Given / When
        Frequency<double> frequency = Frequency<double>.FromHertz(value);

        // Then
        Assert.Equal(expectedYoctoHertz, frequency.YoctoHertz, Tolerance);
    }

    [Theory(DisplayName = "Frequency.FromKiloHertz should produce the expected YoctoHertz")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e27)]
    [InlineData(2.5, 2.5e27)]
    public void FrequencyFromKiloHertzShouldProduceExpectedYoctoHertz(double value, double expectedYoctoHertz)
    {
        // Given / When
        Frequency<double> frequency = Frequency<double>.FromKiloHertz(value);

        // Then
        Assert.Equal(expectedYoctoHertz, frequency.YoctoHertz, Tolerance);
    }

    [Theory(DisplayName = "Frequency.FromMegaHertz should produce the expected YoctoHertz")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e30)]
    [InlineData(2.5, 2.5e30)]
    public void FrequencyFromMegaHertzShouldProduceExpectedYoctoHertz(double value, double expectedYoctoHertz)
    {
        // Given / When
        Frequency<double> frequency = Frequency<double>.FromMegaHertz(value);

        // Then
        Assert.Equal(expectedYoctoHertz, frequency.YoctoHertz, Tolerance);
    }

    [Theory(DisplayName = "Frequency.FromGigaHertz should produce the expected YoctoHertz")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e33)]
    [InlineData(2.5, 2.5e33)]
    public void FrequencyFromGigaHertzShouldProduceExpectedYoctoHertz(double value, double expectedYoctoHertz)
    {
        // Given / When
        Frequency<double> frequency = Frequency<double>.FromGigaHertz(value);

        // Then
        Assert.Equal(expectedYoctoHertz, frequency.YoctoHertz, Tolerance);
    }

    [Theory(DisplayName = "Frequency.FromTeraHertz should produce the expected YoctoHertz")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e36)]
    [InlineData(2.5, 2.5e36)]
    public void FrequencyFromTeraHertzShouldProduceExpectedYoctoHertz(double value, double expectedYoctoHertz)
    {
        // Given / When
        Frequency<double> frequency = Frequency<double>.FromTeraHertz(value);

        // Then
        Assert.Equal(expectedYoctoHertz, frequency.YoctoHertz, Tolerance);
    }

    [Theory(DisplayName = "Frequency.FromPetaHertz should produce the expected YoctoHertz")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e39)]
    [InlineData(2.5, 2.5e39)]
    public void FrequencyFromPetaHertzShouldProduceExpectedYoctoHertz(double value, double expectedYoctoHertz)
    {
        // Given / When
        Frequency<double> frequency = Frequency<double>.FromPetaHertz(value);

        // Then
        Assert.Equal(expectedYoctoHertz, frequency.YoctoHertz, Tolerance);
    }

    [Theory(DisplayName = "Frequency.FromExaHertz should produce the expected YoctoHertz")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e42)]
    [InlineData(2.5, 2.5e42)]
    public void FrequencyFromExaHertzShouldProduceExpectedYoctoHertz(double value, double expectedYoctoHertz)
    {
        // Given / When
        Frequency<double> frequency = Frequency<double>.FromExaHertz(value);

        // Then
        Assert.Equal(expectedYoctoHertz, frequency.YoctoHertz, Tolerance);
    }

    [Theory(DisplayName = "Frequency.FromZettaHertz should produce the expected YoctoHertz")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e45)]
    [InlineData(2.5, 2.5e45)]
    public void FrequencyFromZettaHertzShouldProduceExpectedYoctoHertz(double value, double expectedYoctoHertz)
    {
        // Given / When
        Frequency<double> frequency = Frequency<double>.FromZettaHertz(value);

        // Then
        Assert.Equal(expectedYoctoHertz, frequency.YoctoHertz, Tolerance);
    }

    [Theory(DisplayName = "Frequency.FromYottaHertz should produce the expected YoctoHertz")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e48)]
    [InlineData(2.5, 2.5e48)]
    public void FrequencyFromYottaHertzShouldProduceExpectedYoctoHertz(double value, double expectedYoctoHertz)
    {
        // Given / When
        Frequency<double> frequency = Frequency<double>.FromYottaHertz(value);

        // Then
        Assert.Equal(expectedYoctoHertz, frequency.YoctoHertz, Tolerance);
    }

    [Theory(DisplayName = "Frequency.FromRevolutionsPerMinute should produce the expected Hertz")]
    [InlineData(0.0, 0.0)]
    [InlineData(60.0, 1.0)]
    [InlineData(3600.0, 60.0)]
    public void FrequencyFromRevolutionsPerMinuteShouldProduceExpectedHertz(double rpm, double expectedHertz)
    {
        // Given / When
        Frequency<double> frequency = Frequency<double>.FromRevolutionsPerMinute(rpm);

        // Then
        Assert.Equal(expectedHertz, frequency.Hertz, 1e-12);
    }

    [Fact(DisplayName = "Frequency.Add should produce the expected result")]
    public void FrequencyAddShouldProduceExpectedValue()
    {
        // Given
        Frequency<double> left = Frequency<double>.FromHertz(1500.0);
        Frequency<double> right = Frequency<double>.FromHertz(500.0);

        // When
        Frequency<double> result = left.Add(right);

        // Then
        Assert.Equal(2000.0, result.Hertz, Tolerance);
    }

    [Fact(DisplayName = "Frequency.Subtract should produce the expected result")]
    public void FrequencySubtractShouldProduceExpectedValue()
    {
        // Given
        Frequency<double> left = Frequency<double>.FromHertz(1500.0);
        Frequency<double> right = Frequency<double>.FromHertz(500.0);

        // When
        Frequency<double> result = left.Subtract(right);

        // Then
        Assert.Equal(1000.0, result.Hertz, Tolerance);
    }

    [Fact(DisplayName = "Frequency.Multiply should produce the expected result")]
    public void FrequencyMultiplyShouldProduceExpectedValue()
    {
        // Given
        Frequency<double> left = Frequency<double>.FromHertz(10.0); // 1e25 yHz
        Frequency<double> right = Frequency<double>.FromHertz(3.0); // 3e24 yHz

        // When
        Frequency<double> result = left.Multiply(right); // 1e25 * 3e24 = 3e49 yHz

        // Then
        Assert.Equal(1e25, left.YoctoHertz, Tolerance);
        Assert.Equal(3e24, right.YoctoHertz, Tolerance);
        Assert.Equal(3e49, result.YoctoHertz, Tolerance);
        Assert.Equal(3e25, result.Hertz, Tolerance);
    }

    [Fact(DisplayName = "Frequency.Divide should produce the expected result")]
    public void FrequencyDivideShouldProduceExpectedValue()
    {
        // Given
        Frequency<double> left = Frequency<double>.FromHertz(100.0); // 1e26 yHz
        Frequency<double> right = Frequency<double>.FromHertz(20.0); // 2e25 yHz

        // When
        Frequency<double> result = left.Divide(right); // 1e26 / 2e25 = 5 yHz

        // Then
        Assert.Equal(5.0, result.YoctoHertz, Tolerance);
        Assert.Equal(5e-24, result.Hertz, Tolerance);
    }

    [Fact(DisplayName = "Frequency comparison should produce the expected result (left equal to right)")]
    public void FrequencyComparisonShouldProduceExpectedLeftEqualToRight()
    {
        // Given
        Frequency<double> left = Frequency<double>.FromHertz(1234.0);
        Frequency<double> right = Frequency<double>.FromHertz(1234.0);

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
        Frequency<double> left = Frequency<double>.FromHertz(4567.0);
        Frequency<double> right = Frequency<double>.FromHertz(1234.0);

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
        Frequency<double> left = Frequency<double>.FromHertz(1234.0);
        Frequency<double> right = Frequency<double>.FromHertz(4567.0);

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
        Frequency<BigDecimal> left = Frequency<BigDecimal>.FromKiloHertz(2.0);
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
        Frequency<double> left = Frequency<double>.FromKiloHertz(2.0);
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
        Frequency<double> frequency = Frequency<double>.FromHertz(1000.0);

        // When / Then
        Assert.Equal("1,000.000 Hz", $"{frequency:Hz3}");
        Assert.Equal("1.000 kHz", $"{frequency:kHz3}");
        Assert.Equal("0.001 MHz", $"{frequency:MHz3}");
        Assert.Equal("60,000.000 rpm", $"{frequency:rpm3}");
    }

    [Fact(DisplayName = "Frequency.ToString should honor custom culture separators")]
    public void FrequencyToStringShouldHonorCustomCulture()
    {
        // Given
        CultureInfo customCulture = new("de-DE");
        Frequency<double> frequency = Frequency<double>.FromHertz(1234.56);

        // When
        string formatted = frequency.ToString("Hz2", customCulture);

        // Then
        // German uses '.' for thousands and ',' for decimals.
        Assert.Equal("1.234,56 Hz", formatted);
    }
}
