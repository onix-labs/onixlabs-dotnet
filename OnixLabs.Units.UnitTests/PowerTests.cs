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
        Assert.Equal(0.0, power.YoctoWatts, Tolerance);
    }

    [Theory(DisplayName = "Power.FromYoctoWatts should produce the expected YoctoWatts")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1.0)]
    [InlineData(2.5, 2.5)]
    public void PowerFromYoctoWattsShouldProduceExpectedYoctoWatts(double value, double expectedYoctoWatts)
    {
        // Given / When
        Power<double> power = Power<double>.FromYoctoWatts(value);

        // Then
        Assert.Equal(expectedYoctoWatts, power.YoctoWatts, Tolerance);
    }

    [Theory(DisplayName = "Power.FromZeptoWatts should produce the expected YoctoWatts")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e3)]
    [InlineData(2.5, 2.5e3)]
    public void PowerFromZeptoWattsShouldProduceExpectedYoctoWatts(double value, double expectedYoctoWatts)
    {
        // Given / When
        Power<double> power = Power<double>.FromZeptoWatts(value);

        // Then
        Assert.Equal(expectedYoctoWatts, power.YoctoWatts, Tolerance);
    }

    [Theory(DisplayName = "Power.FromAttoWatts should produce the expected YoctoWatts")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e6)]
    [InlineData(2.5, 2.5e6)]
    public void PowerFromAttoWattsShouldProduceExpectedYoctoWatts(double value, double expectedYoctoWatts)
    {
        // Given / When
        Power<double> power = Power<double>.FromAttoWatts(value);

        // Then
        Assert.Equal(expectedYoctoWatts, power.YoctoWatts, Tolerance);
    }

    [Theory(DisplayName = "Power.FromFemtoWatts should produce the expected YoctoWatts")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e9)]
    [InlineData(2.5, 2.5e9)]
    public void PowerFromFemtoWattsShouldProduceExpectedYoctoWatts(double value, double expectedYoctoWatts)
    {
        // Given / When
        Power<double> power = Power<double>.FromFemtoWatts(value);

        // Then
        Assert.Equal(expectedYoctoWatts, power.YoctoWatts, Tolerance);
    }

    [Theory(DisplayName = "Power.FromPicoWatts should produce the expected YoctoWatts")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e12)]
    [InlineData(2.5, 2.5e12)]
    public void PowerFromPicoWattsShouldProduceExpectedYoctoWatts(double value, double expectedYoctoWatts)
    {
        // Given / When
        Power<double> power = Power<double>.FromPicoWatts(value);

        // Then
        Assert.Equal(expectedYoctoWatts, power.YoctoWatts, Tolerance);
    }

    [Theory(DisplayName = "Power.FromNanoWatts should produce the expected YoctoWatts")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e15)]
    [InlineData(2.5, 2.5e15)]
    public void PowerFromNanoWattsShouldProduceExpectedYoctoWatts(double value, double expectedYoctoWatts)
    {
        // Given / When
        Power<double> power = Power<double>.FromNanoWatts(value);

        // Then
        Assert.Equal(expectedYoctoWatts, power.YoctoWatts, Tolerance);
    }

    [Theory(DisplayName = "Power.FromMicroWatts should produce the expected YoctoWatts")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e18)]
    [InlineData(2.5, 2.5e18)]
    public void PowerFromMicroWattsShouldProduceExpectedYoctoWatts(double value, double expectedYoctoWatts)
    {
        // Given / When
        Power<double> power = Power<double>.FromMicroWatts(value);

        // Then
        Assert.Equal(expectedYoctoWatts, power.YoctoWatts, Tolerance);
    }

    [Theory(DisplayName = "Power.FromMilliWatts should produce the expected YoctoWatts")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e21)]
    [InlineData(2.5, 2.5e21)]
    public void PowerFromMilliWattsShouldProduceExpectedYoctoWatts(double value, double expectedYoctoWatts)
    {
        // Given / When
        Power<double> power = Power<double>.FromMilliWatts(value);

        // Then
        Assert.Equal(expectedYoctoWatts, power.YoctoWatts, Tolerance);
    }

    [Theory(DisplayName = "Power.FromWatts should produce the expected YoctoWatts")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e24)]
    [InlineData(2.5, 2.5e24)]
    public void PowerFromWattsShouldProduceExpectedYoctoWatts(double value, double expectedYoctoWatts)
    {
        // Given / When
        Power<double> power = Power<double>.FromWatts(value);

        // Then
        Assert.Equal(expectedYoctoWatts, power.YoctoWatts, Tolerance);
    }

    [Theory(DisplayName = "Power.FromKiloWatts should produce the expected YoctoWatts")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e27)]
    [InlineData(2.5, 2.5e27)]
    public void PowerFromKiloWattsShouldProduceExpectedYoctoWatts(double value, double expectedYoctoWatts)
    {
        // Given / When
        Power<double> power = Power<double>.FromKiloWatts(value);

        // Then
        Assert.Equal(expectedYoctoWatts, power.YoctoWatts, Tolerance);
    }

    [Theory(DisplayName = "Power.FromMegaWatts should produce the expected YoctoWatts")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e30)]
    [InlineData(2.5, 2.5e30)]
    public void PowerFromMegaWattsShouldProduceExpectedYoctoWatts(double value, double expectedYoctoWatts)
    {
        // Given / When
        Power<double> power = Power<double>.FromMegaWatts(value);

        // Then
        Assert.Equal(expectedYoctoWatts, power.YoctoWatts, Tolerance);
    }

    [Theory(DisplayName = "Power.FromGigaWatts should produce the expected YoctoWatts")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e33)]
    [InlineData(2.5, 2.5e33)]
    public void PowerFromGigaWattsShouldProduceExpectedYoctoWatts(double value, double expectedYoctoWatts)
    {
        // Given / When
        Power<double> power = Power<double>.FromGigaWatts(value);

        // Then
        Assert.Equal(expectedYoctoWatts, power.YoctoWatts, Tolerance);
    }

    [Theory(DisplayName = "Power.FromTeraWatts should produce the expected YoctoWatts")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e36)]
    [InlineData(2.5, 2.5e36)]
    public void PowerFromTeraWattsShouldProduceExpectedYoctoWatts(double value, double expectedYoctoWatts)
    {
        // Given / When
        Power<double> power = Power<double>.FromTeraWatts(value);

        // Then
        Assert.Equal(expectedYoctoWatts, power.YoctoWatts, Tolerance);
    }

    [Theory(DisplayName = "Power.FromPetaWatts should produce the expected YoctoWatts")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e39)]
    [InlineData(2.5, 2.5e39)]
    public void PowerFromPetaWattsShouldProduceExpectedYoctoWatts(double value, double expectedYoctoWatts)
    {
        // Given / When
        Power<double> power = Power<double>.FromPetaWatts(value);

        // Then
        Assert.Equal(expectedYoctoWatts, power.YoctoWatts, Tolerance);
    }

    [Theory(DisplayName = "Power.FromExaWatts should produce the expected YoctoWatts")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e42)]
    [InlineData(2.5, 2.5e42)]
    public void PowerFromExaWattsShouldProduceExpectedYoctoWatts(double value, double expectedYoctoWatts)
    {
        // Given / When
        Power<double> power = Power<double>.FromExaWatts(value);

        // Then
        Assert.Equal(expectedYoctoWatts, power.YoctoWatts, Tolerance);
    }

    [Theory(DisplayName = "Power.FromZettaWatts should produce the expected YoctoWatts")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e45)]
    [InlineData(2.5, 2.5e45)]
    public void PowerFromZettaWattsShouldProduceExpectedYoctoWatts(double value, double expectedYoctoWatts)
    {
        // Given / When
        Power<double> power = Power<double>.FromZettaWatts(value);

        // Then
        Assert.Equal(expectedYoctoWatts, power.YoctoWatts, Tolerance);
    }

    [Theory(DisplayName = "Power.FromYottaWatts should produce the expected YoctoWatts")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e48)]
    [InlineData(2.5, 2.5e48)]
    public void PowerFromYottaWattsShouldProduceExpectedYoctoWatts(double value, double expectedYoctoWatts)
    {
        // Given / When
        Power<double> power = Power<double>.FromYottaWatts(value);

        // Then
        Assert.Equal(expectedYoctoWatts, power.YoctoWatts, Tolerance);
    }

    [Theory(DisplayName = "Power.FromHorsepower should produce the expected YoctoWatts")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 7.456998715822702e26)]
    [InlineData(2.5, 1.8642496789556757e27)]
    public void PowerFromHorsepowerShouldProduceExpectedYoctoWatts(double value, double expectedYoctoWatts)
    {
        // Given / When
        Power<double> power = Power<double>.FromHorsepower(value);

        // Then
        Assert.Equal(expectedYoctoWatts, power.YoctoWatts, Tolerance);
    }

    [Theory(DisplayName = "Power.FromMetricHorsepower should produce the expected YoctoWatts")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 7.3549875e26)]
    [InlineData(2.5, 1.838746875e27)]
    public void PowerFromMetricHorsepowerShouldProduceExpectedYoctoWatts(double value, double expectedYoctoWatts)
    {
        // Given / When
        Power<double> power = Power<double>.FromMetricHorsepower(value);

        // Then
        Assert.Equal(expectedYoctoWatts, power.YoctoWatts, Tolerance);
    }

    [Fact(DisplayName = "Power.Add should produce the expected result")]
    public void PowerAddShouldProduceExpectedValue()
    {
        // Given
        Power<double> left = Power<double>.FromWatts(1500.0);
        Power<double> right = Power<double>.FromWatts(500.0);

        // When
        Power<double> result = left.Add(right);

        // Then
        Assert.Equal(2000.0, result.Watts, Tolerance);
    }

    [Fact(DisplayName = "Power.Subtract should produce the expected result")]
    public void PowerSubtractShouldProduceExpectedValue()
    {
        // Given
        Power<double> left = Power<double>.FromWatts(1500.0);
        Power<double> right = Power<double>.FromWatts(500.0);

        // When
        Power<double> result = left.Subtract(right);

        // Then
        Assert.Equal(1000.0, result.Watts, Tolerance);
    }

    [Fact(DisplayName = "Power.Multiply should produce the expected result")]
    public void PowerMultiplyShouldProduceExpectedValue()
    {
        // Given
        Power<double> left = Power<double>.FromWatts(10.0); // 1e25 yW
        Power<double> right = Power<double>.FromWatts(3.0); // 3e24 yW

        // When
        Power<double> result = left.Multiply(right); // 1e25 * 3e24 = 3e49 yW

        // Then
        Assert.Equal(1e25, left.YoctoWatts, Tolerance);
        Assert.Equal(3e24, right.YoctoWatts, Tolerance);
        Assert.Equal(3e49, result.YoctoWatts, Tolerance);
        Assert.Equal(3e25, result.Watts, Tolerance);
    }

    [Fact(DisplayName = "Power.Divide should produce the expected result")]
    public void PowerDivideShouldProduceExpectedValue()
    {
        // Given
        Power<double> left = Power<double>.FromWatts(100.0); // 1e26 yW
        Power<double> right = Power<double>.FromWatts(20.0); // 2e25 yW

        // When
        Power<double> result = left.Divide(right); // 1e26 / 2e25 = 5 yW

        // Then
        Assert.Equal(5.0, result.YoctoWatts, Tolerance);
        Assert.Equal(5e-24, result.Watts, Tolerance);
    }

    [Fact(DisplayName = "Power comparison should produce the expected result (left equal to right)")]
    public void PowerComparisonShouldProduceExpectedResultLeftEqualToRight()
    {
        // Given
        Power<double> left = Power<double>.FromWatts(1234.0);
        Power<double> right = Power<double>.FromWatts(1234.0);

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
        Power<double> left = Power<double>.FromWatts(4567.0);
        Power<double> right = Power<double>.FromWatts(1234.0);

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
        Power<double> left = Power<double>.FromWatts(1234.0);
        Power<double> right = Power<double>.FromWatts(4567.0);

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
        Power<BigDecimal> left = Power<BigDecimal>.FromKiloWatts(2.0);
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
        Power<double> left = Power<double>.FromKiloWatts(2.0);
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
        Power<double> power = Power<double>.FromWatts(1000.0);

        // When / Then
        Assert.Equal("1,000.000 W", $"{power:W3}");
        Assert.Equal("1.000 kW", $"{power:kW3}");
        Assert.Equal("0.001 MW", $"{power:MW3}");
        Assert.Equal("1.341 hp", $"{power:hp3}");
        Assert.Equal("1.360 hpM", $"{power:hpM3}");
    }

    [Fact(DisplayName = "Power.ToString should honor custom culture separators")]
    public void PowerToStringShouldHonorCustomCulture()
    {
        // Given
        CultureInfo customCulture = new("de-DE");
        Power<double> power = Power<double>.FromWatts(1234.56);

        // When
        string formatted = power.ToString("W2", customCulture);

        // Then
        // German uses '.' for thousands and ',' for decimals.
        Assert.Equal("1.234,56 W", formatted);
    }
}
