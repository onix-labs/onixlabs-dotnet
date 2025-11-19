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

public sealed class AreaTests
{
    // IEEE-754 binary floating-point arithmetic causes small discrepancies in calculation, therefore we need a tolerance.
    private const double Tolerance = 1e+85;

    [Fact(DisplayName = "Area.Empty should produce the expected result")]
    public void AreaEmptyShouldProduceExpectedResult()
    {
        // Given / When
        Area<double> area = Area<double>.Empty;

        // Then
        Assert.Equal(0.0, area.SquareYoctoMeters, Tolerance);
    }

    [Theory(DisplayName = "Area.FromSquareYoctoMeters should produce the expected SquareYoctoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1.0)]
    [InlineData(2.5, 2.5)]
    public void AreaFromSquareYoctoMetersShouldProduceExpectedSquareYoctoMeters(double value, double expectedSquareYoctoMeters)
    {
        // Given / When
        Area<double> area = Area<double>.FromSquareYoctoMeters(value);

        // Then
        Assert.Equal(expectedSquareYoctoMeters, area.SquareYoctoMeters, Tolerance);
    }

    [Theory(DisplayName = "Area.FromSquareZeptoMeters should produce the expected SquareYoctoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e6)]
    [InlineData(2.5, 2.5e6)]
    public void AreaFromSquareZeptoMetersShouldProduceExpectedSquareYoctoMeters(double value, double expectedSquareYoctoMeters)
    {
        // Given / When
        Area<double> area = Area<double>.FromSquareZeptoMeters(value);

        // Then
        Assert.Equal(expectedSquareYoctoMeters, area.SquareYoctoMeters, Tolerance);
    }

    [Theory(DisplayName = "Area.FromSquareAttoMeters should produce the expected SquareYoctoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e12)]
    [InlineData(2.5, 2.5e12)]
    public void AreaFromSquareAttoMetersShouldProduceExpectedSquareYoctoMeters(double value, double expectedSquareYoctoMeters)
    {
        // Given / When
        Area<double> area = Area<double>.FromSquareAttoMeters(value);

        // Then
        Assert.Equal(expectedSquareYoctoMeters, area.SquareYoctoMeters, Tolerance);
    }

    [Theory(DisplayName = "Area.FromSquareFemtoMeters should produce the expected SquareYoctoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e18)]
    [InlineData(2.5, 2.5e18)]
    public void AreaFromSquareFemtoMetersShouldProduceExpectedSquareYoctoMeters(double value, double expectedSquareYoctoMeters)
    {
        // Given / When
        Area<double> area = Area<double>.FromSquareFemtoMeters(value);

        // Then
        Assert.Equal(expectedSquareYoctoMeters, area.SquareYoctoMeters, Tolerance);
    }

    [Theory(DisplayName = "Area.FromSquarePicoMeters should produce the expected SquareYoctoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e24)]
    [InlineData(2.5, 2.5e24)]
    public void AreaFromSquarePicoMetersShouldProduceExpectedSquareYoctoMeters(double value, double expectedSquareYoctoMeters)
    {
        // Given / When
        Area<double> area = Area<double>.FromSquarePicoMeters(value);

        // Then
        Assert.Equal(expectedSquareYoctoMeters, area.SquareYoctoMeters, Tolerance);
    }

    [Theory(DisplayName = "Area.FromSquareNanoMeters should produce the expected SquareYoctoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e30)]
    [InlineData(2.5, 2.5e30)]
    public void AreaFromSquareNanoMetersShouldProduceExpectedSquareYoctoMeters(double value, double expectedSquareYoctoMeters)
    {
        // Given / When
        Area<double> area = Area<double>.FromSquareNanoMeters(value);

        // Then
        Assert.Equal(expectedSquareYoctoMeters, area.SquareYoctoMeters, Tolerance);
    }

    [Theory(DisplayName = "Area.FromSquareMicroMeters should produce the expected SquareYoctoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e36)]
    [InlineData(2.5, 2.5e36)]
    public void AreaFromSquareMicroMetersShouldProduceExpectedSquareYoctoMeters(double value, double expectedSquareYoctoMeters)
    {
        // Given / When
        Area<double> area = Area<double>.FromSquareMicroMeters(value);

        // Then
        Assert.Equal(expectedSquareYoctoMeters, area.SquareYoctoMeters, Tolerance);
    }

    [Theory(DisplayName = "Area.FromSquareMilliMeters should produce the expected SquareYoctoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e42)]
    [InlineData(2.5, 2.5e42)]
    public void AreaFromSquareMilliMetersShouldProduceExpectedSquareYoctoMeters(double value, double expectedSquareYoctoMeters)
    {
        // Given / When
        Area<double> area = Area<double>.FromSquareMilliMeters(value);

        // Then
        Assert.Equal(expectedSquareYoctoMeters, area.SquareYoctoMeters, Tolerance);
    }

    [Theory(DisplayName = "Area.FromSquareCentiMeters should produce the expected SquareYoctoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e44)]
    [InlineData(2.5, 2.5e44)]
    public void AreaFromSquareCentiMetersShouldProduceExpectedSquareYoctoMeters(double value, double expectedSquareYoctoMeters)
    {
        // Given / When
        Area<double> area = Area<double>.FromSquareCentiMeters(value);

        // Then
        Assert.Equal(expectedSquareYoctoMeters, area.SquareYoctoMeters, Tolerance);
    }

    [Theory(DisplayName = "Area.FromSquareDeciMeters should produce the expected SquareYoctoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e46)]
    [InlineData(2.5, 2.5e46)]
    public void AreaFromSquareDeciMetersShouldProduceExpectedSquareYoctoMeters(double value, double expectedSquareYoctoMeters)
    {
        // Given / When
        Area<double> area = Area<double>.FromSquareDeciMeters(value);

        // Then
        Assert.Equal(expectedSquareYoctoMeters, area.SquareYoctoMeters, Tolerance);
    }

    [Theory(DisplayName = "Area.FromSquareMeters should produce the expected SquareYoctoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e48)]
    [InlineData(2.5, 2.5e48)]
    public void AreaFromSquareMetersShouldProduceExpectedSquareYoctoMeters(double value, double expectedSquareYoctoMeters)
    {
        // Given / When
        Area<double> area = Area<double>.FromSquareMeters(value);

        // Then
        Assert.Equal(expectedSquareYoctoMeters, area.SquareYoctoMeters, Tolerance);
    }

    [Theory(DisplayName = "Area.FromSquareDecaMeters should produce the expected SquareYoctoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e50)]
    [InlineData(2.5, 2.5e50)]
    public void AreaFromSquareDecaMetersShouldProduceExpectedSquareYoctoMeters(double value, double expectedSquareYoctoMeters)
    {
        // Given / When
        Area<double> area = Area<double>.FromSquareDecaMeters(value);

        // Then
        Assert.Equal(expectedSquareYoctoMeters, area.SquareYoctoMeters, Tolerance);
    }

    [Theory(DisplayName = "Area.FromSquareHectoMeters should produce the expected SquareYoctoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e52)]
    [InlineData(2.5, 2.5e52)]
    public void AreaFromSquareHectoMetersShouldProduceExpectedSquareYoctoMeters(double value, double expectedSquareYoctoMeters)
    {
        // Given / When
        Area<double> area = Area<double>.FromSquareHectoMeters(value);

        // Then
        Assert.Equal(expectedSquareYoctoMeters, area.SquareYoctoMeters, Tolerance);
    }

    [Theory(DisplayName = "Area.FromSquareKiloMeters should produce the expected SquareYoctoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e54)]
    [InlineData(2.5, 2.5e54)]
    public void AreaFromSquareKiloMetersShouldProduceExpectedSquareYoctoMeters(double value, double expectedSquareYoctoMeters)
    {
        // Given / When
        Area<double> area = Area<double>.FromSquareKiloMeters(value);

        // Then
        Assert.Equal(expectedSquareYoctoMeters, area.SquareYoctoMeters, Tolerance);
    }

    [Theory(DisplayName = "Area.FromSquareMegaMeters should produce the expected SquareYoctoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e60)]
    [InlineData(2.5, 2.5e60)]
    public void AreaFromSquareMegaMetersShouldProduceExpectedSquareYoctoMeters(double value, double expectedSquareYoctoMeters)
    {
        // Given / When
        Area<double> area = Area<double>.FromSquareMegaMeters(value);

        // Then
        Assert.Equal(expectedSquareYoctoMeters, area.SquareYoctoMeters, Tolerance);
    }

    [Theory(DisplayName = "Area.FromSquareGigaMeters should produce the expected SquareYoctoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e66)]
    [InlineData(2.5, 2.5e66)]
    public void AreaFromSquareGigaMetersShouldProduceExpectedSquareYoctoMeters(double value, double expectedSquareYoctoMeters)
    {
        // Given / When
        Area<double> area = Area<double>.FromSquareGigaMeters(value);

        // Then
        Assert.Equal(expectedSquareYoctoMeters, area.SquareYoctoMeters, Tolerance);
    }

    [Theory(DisplayName = "Area.FromSquareTeraMeters should produce the expected SquareYoctoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e72)]
    [InlineData(2.5, 2.5e72)]
    public void AreaFromSquareTeraMetersShouldProduceExpectedSquareYoctoMeters(double value, double expectedSquareYoctoMeters)
    {
        // Given / When
        Area<double> area = Area<double>.FromSquareTeraMeters(value);

        // Then
        Assert.Equal(expectedSquareYoctoMeters, area.SquareYoctoMeters, Tolerance);
    }

    [Theory(DisplayName = "Area.FromSquarePetaMeters should produce the expected SquareYoctoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e78)]
    [InlineData(2.5, 2.5e78)]
    public void AreaFromSquarePetaMetersShouldProduceExpectedSquareYoctoMeters(double value, double expectedSquareYoctoMeters)
    {
        // Given / When
        Area<double> area = Area<double>.FromSquarePetaMeters(value);

        // Then
        Assert.Equal(expectedSquareYoctoMeters, area.SquareYoctoMeters, Tolerance);
    }

    [Theory(DisplayName = "Area.FromSquareExaMeters should produce the expected SquareYoctoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e84)]
    [InlineData(2.5, 2.5e84)]
    public void AreaFromSquareExaMetersShouldProduceExpectedSquareYoctoMeters(double value, double expectedSquareYoctoMeters)
    {
        // Given / When
        Area<double> area = Area<double>.FromSquareExaMeters(value);

        // Then
        Assert.Equal(expectedSquareYoctoMeters, area.SquareYoctoMeters, Tolerance);
    }

    [Theory(DisplayName = "Area.FromSquareZettaMeters should produce the expected SquareYoctoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e90)]
    [InlineData(2.5, 2.5e90)]
    public void AreaFromSquareZettaMetersShouldProduceExpectedSquareYoctoMeters(double value, double expectedSquareYoctoMeters)
    {
        // Given / When
        Area<double> area = Area<double>.FromSquareZettaMeters(value);

        // Then
        Assert.Equal(expectedSquareYoctoMeters, area.SquareYoctoMeters, Tolerance);
    }

    [Theory(DisplayName = "Area.FromSquareYottaMeters should produce the expected SquareYoctoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e96)]
    [InlineData(2.5, 2.5e96)]
    public void AreaFromSquareYottaMetersShouldProduceExpectedSquareYoctoMeters(double value, double expectedSquareYoctoMeters)
    {
        // Given / When
        Area<double> area = Area<double>.FromSquareYottaMeters(value);

        // Then
        Assert.Equal(expectedSquareYoctoMeters, area.SquareYoctoMeters, Tolerance);
    }

    [Theory(DisplayName = "Area.FromSquareInches should produce the expected SquareYoctoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 6.4516e44)]
    [InlineData(2.5, 1.6129e45)]
    public void AreaFromSquareInchesShouldProduceExpectedSquareYoctoMeters(double value, double expectedSquareYoctoMeters)
    {
        // Given / When
        Area<double> area = Area<double>.FromSquareInches(value);

        // Then
        Assert.Equal(expectedSquareYoctoMeters, area.SquareYoctoMeters, Tolerance);
    }

    [Theory(DisplayName = "Area.FromSquareFeet should produce the expected SquareYoctoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 9.290304e46)]
    [InlineData(2.5, 2.322576e47)]
    public void AreaFromSquareFeetShouldProduceExpectedSquareYoctoMeters(double value, double expectedSquareYoctoMeters)
    {
        // Given / When
        Area<double> area = Area<double>.FromSquareFeet(value);

        // Then
        Assert.Equal(expectedSquareYoctoMeters, area.SquareYoctoMeters, Tolerance);
    }

    [Theory(DisplayName = "Area.FromSquareYards should produce the expected SquareYoctoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 8.3612736e47)]
    [InlineData(2.5, 2.0903184e48)]
    public void AreaFromSquareYardsShouldProduceExpectedSquareYoctoMeters(double value, double expectedSquareYoctoMeters)
    {
        // Given / When
        Area<double> area = Area<double>.FromSquareYards(value);

        // Then
        Assert.Equal(expectedSquareYoctoMeters, area.SquareYoctoMeters, Tolerance);
    }

    [Theory(DisplayName = "Area.FromSquareMiles should produce the expected SquareYoctoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 2.589988110336e54)]
    [InlineData(2.5, 6.47497027584e54)]
    public void AreaFromSquareMilesShouldProduceExpectedSquareYoctoMeters(double value, double expectedSquareYoctoMeters)
    {
        // Given / When
        Area<double> area = Area<double>.FromSquareMiles(value);

        // Then
        Assert.Equal(expectedSquareYoctoMeters, area.SquareYoctoMeters, Tolerance);
    }

    [Theory(DisplayName = "Area.FromAcres should produce the expected SquareYoctoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 4.0468564224e51)]
    [InlineData(2.5, 1.0117141056e52)]
    public void AreaFromAcresShouldProduceExpectedSquareYoctoMeters(double value, double expectedSquareYoctoMeters)
    {
        // Given / When
        Area<double> area = Area<double>.FromAcres(value);

        // Then
        Assert.Equal(expectedSquareYoctoMeters, area.SquareYoctoMeters, Tolerance);
    }

    [Theory(DisplayName = "Area.FromHectares should produce the expected SquareYoctoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e52)]
    [InlineData(2.5, 2.5e52)]
    public void AreaFromHectaresShouldProduceExpectedSquareYoctoMeters(double value, double expectedSquareYoctoMeters)
    {
        // Given / When
        Area<double> area = Area<double>.FromHectares(value);

        // Then
        Assert.Equal(expectedSquareYoctoMeters, area.SquareYoctoMeters, Tolerance);
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
        Area<double> left = Area<double>.FromSquareMeters(10.0); // 1e49 sqym
        Area<double> right = Area<double>.FromSquareMeters(3.0); // 3e48 sqym

        // When
        Area<double> result = left.Multiply(right); // 1e49 * 3e48 = 3e97 sqym

        // Then
        Assert.Equal(1e49, left.SquareYoctoMeters, Tolerance);
        Assert.Equal(3e48, right.SquareYoctoMeters, Tolerance);
        Assert.Equal(3e97, result.SquareYoctoMeters, Tolerance);
    }

    [Fact(DisplayName = "Area.Divide should produce the expected result")]
    public void AreaDivideShouldProduceExpectedValue()
    {
        // Given
        Area<double> left = Area<double>.FromSquareMeters(100.0); // 1e50 sqym
        Area<double> right = Area<double>.FromSquareMeters(20.0); // 2e49 sqym

        // When
        Area<double> result = left.Divide(right); // 1e50 / 2e49 = 5 sqym

        // Then
        Assert.Equal(5.0, result.SquareYoctoMeters, Tolerance);
        Assert.Equal(5e-48, result.SquareMeters, Tolerance);
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
        Area<BigDecimal> left = Area<BigDecimal>.FromSquareKiloMeters(2.0);        // 2 km²
        Area<BigDecimal> right = Area<BigDecimal>.FromSquareMeters(2_000_000.0);   // 2,000,000 m²

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
        Area<double> left = Area<double>.FromSquareMeters(2.0);
        Area<double> right = Area<double>.FromSquareMeters(2.5);

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
        Area<double> area = Area<double>.FromSquareMeters(1_000_000.0); // 1 km²

        // When / Then
        Assert.Equal("1,000,000.000 sqm", $"{area:sqm3}");
        Assert.Equal("1.000 sqkm", $"{area:sqkm3}");
        Assert.Equal("10,000,000,000.000 sqcm", $"{area:sqcm3}");
        Assert.Equal("1,550,003,100.006 sqin", $"{area:sqin3}");
        Assert.Equal("10,763,910.417 sqft", $"{area:sqft3}");
        Assert.Equal("1,195,990.046 sqyd", $"{area:sqyd3}");
        Assert.Equal("0.386 sqmi", $"{area:sqmi3}");
        Assert.Equal("100.000 ha", $"{area:ha3}");
        Assert.Equal("247.105 ac", $"{area:ac3}");
    }

    [Fact(DisplayName = "Area.ToString should honor custom culture separators")]
    public void AreaToStringShouldHonorCustomCulture()
    {
        // Given
        CultureInfo customCulture = new("de-DE");
        Area<double> area = Area<double>.FromSquareMeters(1234.56);

        // When
        string formatted = area.ToString("sqm2", customCulture);

        // Then
        // German uses '.' for thousands and ',' for decimals.
        Assert.Equal("1.234,56 sqm", formatted);
    }
}
