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

public sealed class VolumeTests
{
    // IEEE-754 binary floating-point arithmetic causes small discrepancies in calculation, therefore we need a tolerance.
    private const double Tolerance = 1e+120;

    [Fact(DisplayName = "Volume.Zero should produce the expected result")]
    public void VolumeZeroShouldProduceExpectedResult()
    {
        // Given / When
        Volume<double> volume = Volume<double>.Zero;

        // Then
        Assert.Equal(0.0, volume.CubicYoctoMeters, Tolerance);
    }

    [Theory(DisplayName = "Volume.FromCubicYoctoMeters should produce the expected CubicYoctoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1.0)]
    [InlineData(2.5, 2.5)]
    public void VolumeFromCubicYoctoMetersShouldProduceExpectedCubicYoctoMeters(double value, double expectedCubicYoctoMeters)
    {
        // Given / When
        Volume<double> volume = Volume<double>.FromCubicYoctoMeters(value);

        // Then
        Assert.Equal(expectedCubicYoctoMeters, volume.CubicYoctoMeters, Tolerance);
    }

    [Theory(DisplayName = "Volume.FromCubicZeptoMeters should produce the expected CubicYoctoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e9)]
    [InlineData(2.5, 2.5e9)]
    public void VolumeFromCubicZeptoMetersShouldProduceExpectedCubicYoctoMeters(double value, double expectedCubicYoctoMeters)
    {
        // Given / When
        Volume<double> volume = Volume<double>.FromCubicZeptoMeters(value);

        // Then
        Assert.Equal(expectedCubicYoctoMeters, volume.CubicYoctoMeters, Tolerance);
    }

    [Theory(DisplayName = "Volume.FromCubicAttoMeters should produce the expected CubicYoctoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e18)]
    [InlineData(2.5, 2.5e18)]
    public void VolumeFromCubicAttoMetersShouldProduceExpectedCubicYoctoMeters(double value, double expectedCubicYoctoMeters)
    {
        // Given / When
        Volume<double> volume = Volume<double>.FromCubicAttoMeters(value);

        // Then
        Assert.Equal(expectedCubicYoctoMeters, volume.CubicYoctoMeters, Tolerance);
    }

    [Theory(DisplayName = "Volume.FromCubicFemtoMeters should produce the expected CubicYoctoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e27)]
    [InlineData(2.5, 2.5e27)]
    public void VolumeFromCubicFemtoMetersShouldProduceExpectedCubicYoctoMeters(double value, double expectedCubicYoctoMeters)
    {
        // Given / When
        Volume<double> volume = Volume<double>.FromCubicFemtoMeters(value);

        // Then
        Assert.Equal(expectedCubicYoctoMeters, volume.CubicYoctoMeters, Tolerance);
    }

    [Theory(DisplayName = "Volume.FromCubicPicoMeters should produce the expected CubicYoctoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e36)]
    [InlineData(2.5, 2.5e36)]
    public void VolumeFromCubicPicoMetersShouldProduceExpectedCubicYoctoMeters(double value, double expectedCubicYoctoMeters)
    {
        // Given / When
        Volume<double> volume = Volume<double>.FromCubicPicoMeters(value);

        // Then
        Assert.Equal(expectedCubicYoctoMeters, volume.CubicYoctoMeters, Tolerance);
    }

    [Theory(DisplayName = "Volume.FromCubicNanoMeters should produce the expected CubicYoctoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e45)]
    [InlineData(2.5, 2.5e45)]
    public void VolumeFromCubicNanoMetersShouldProduceExpectedCubicYoctoMeters(double value, double expectedCubicYoctoMeters)
    {
        // Given / When
        Volume<double> volume = Volume<double>.FromCubicNanoMeters(value);

        // Then
        Assert.Equal(expectedCubicYoctoMeters, volume.CubicYoctoMeters, Tolerance);
    }

    [Theory(DisplayName = "Volume.FromCubicMicroMeters should produce the expected CubicYoctoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e54)]
    [InlineData(2.5, 2.5e54)]
    public void VolumeFromCubicMicroMetersShouldProduceExpectedCubicYoctoMeters(double value, double expectedCubicYoctoMeters)
    {
        // Given / When
        Volume<double> volume = Volume<double>.FromCubicMicroMeters(value);

        // Then
        Assert.Equal(expectedCubicYoctoMeters, volume.CubicYoctoMeters, Tolerance);
    }

    [Theory(DisplayName = "Volume.FromCubicMilliMeters should produce the expected CubicYoctoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e63)]
    [InlineData(2.5, 2.5e63)]
    public void VolumeFromCubicMilliMetersShouldProduceExpectedCubicYoctoMeters(double value, double expectedCubicYoctoMeters)
    {
        // Given / When
        Volume<double> volume = Volume<double>.FromCubicMilliMeters(value);

        // Then
        Assert.Equal(expectedCubicYoctoMeters, volume.CubicYoctoMeters, Tolerance);
    }

    [Theory(DisplayName = "Volume.FromCubicCentiMeters should produce the expected CubicYoctoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e66)]
    [InlineData(2.5, 2.5e66)]
    public void VolumeFromCubicCentiMetersShouldProduceExpectedCubicYoctoMeters(double value, double expectedCubicYoctoMeters)
    {
        // Given / When
        Volume<double> volume = Volume<double>.FromCubicCentiMeters(value);

        // Then
        Assert.Equal(expectedCubicYoctoMeters, volume.CubicYoctoMeters, Tolerance);
    }

    [Theory(DisplayName = "Volume.FromCubicDeciMeters should produce the expected CubicYoctoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e69)]
    [InlineData(2.5, 2.5e69)]
    public void VolumeFromCubicDeciMetersShouldProduceExpectedCubicYoctoMeters(double value, double expectedCubicYoctoMeters)
    {
        // Given / When
        Volume<double> volume = Volume<double>.FromCubicDeciMeters(value);

        // Then
        Assert.Equal(expectedCubicYoctoMeters, volume.CubicYoctoMeters, Tolerance);
    }

    [Theory(DisplayName = "Volume.FromCubicMeters should produce the expected CubicYoctoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e72)]
    [InlineData(2.5, 2.5e72)]
    public void VolumeFromCubicMetersShouldProduceExpectedCubicYoctoMeters(double value, double expectedCubicYoctoMeters)
    {
        // Given / When
        Volume<double> volume = Volume<double>.FromCubicMeters(value);

        // Then
        Assert.Equal(expectedCubicYoctoMeters, volume.CubicYoctoMeters, Tolerance);
    }

    [Theory(DisplayName = "Volume.FromCubicDecaMeters should produce the expected CubicYoctoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e75)]
    [InlineData(2.5, 2.5e75)]
    public void VolumeFromCubicDecaMetersShouldProduceExpectedCubicYoctoMeters(double value, double expectedCubicYoctoMeters)
    {
        // Given / When
        Volume<double> volume = Volume<double>.FromCubicDecaMeters(value);

        // Then
        Assert.Equal(expectedCubicYoctoMeters, volume.CubicYoctoMeters, Tolerance);
    }

    [Theory(DisplayName = "Volume.FromCubicHectoMeters should produce the expected CubicYoctoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e78)]
    [InlineData(2.5, 2.5e78)]
    public void VolumeFromCubicHectoMetersShouldProduceExpectedCubicYoctoMeters(double value, double expectedCubicYoctoMeters)
    {
        // Given / When
        Volume<double> volume = Volume<double>.FromCubicHectoMeters(value);

        // Then
        Assert.Equal(expectedCubicYoctoMeters, volume.CubicYoctoMeters, Tolerance);
    }

    [Theory(DisplayName = "Volume.FromCubicKiloMeters should produce the expected CubicYoctoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e81)]
    [InlineData(2.5, 2.5e81)]
    public void VolumeFromCubicKiloMetersShouldProduceExpectedCubicYoctoMeters(double value, double expectedCubicYoctoMeters)
    {
        // Given / When
        Volume<double> volume = Volume<double>.FromCubicKiloMeters(value);

        // Then
        Assert.Equal(expectedCubicYoctoMeters, volume.CubicYoctoMeters, Tolerance);
    }

    [Theory(DisplayName = "Volume.FromCubicMegaMeters should produce the expected CubicYoctoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e90)]
    [InlineData(2.5, 2.5e90)]
    public void VolumeFromCubicMegaMetersShouldProduceExpectedCubicYoctoMeters(double value, double expectedCubicYoctoMeters)
    {
        // Given / When
        Volume<double> volume = Volume<double>.FromCubicMegaMeters(value);

        // Then
        Assert.Equal(expectedCubicYoctoMeters, volume.CubicYoctoMeters, Tolerance);
    }

    [Theory(DisplayName = "Volume.FromCubicGigaMeters should produce the expected CubicYoctoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e99)]
    [InlineData(2.5, 2.5e99)]
    public void VolumeFromCubicGigaMetersShouldProduceExpectedCubicYoctoMeters(double value, double expectedCubicYoctoMeters)
    {
        // Given / When
        Volume<double> volume = Volume<double>.FromCubicGigaMeters(value);

        // Then
        Assert.Equal(expectedCubicYoctoMeters, volume.CubicYoctoMeters, Tolerance);
    }

    [Theory(DisplayName = "Volume.FromCubicTeraMeters should produce the expected CubicYoctoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e108)]
    [InlineData(2.5, 2.5e108)]
    public void VolumeFromCubicTeraMetersShouldProduceExpectedCubicYoctoMeters(double value, double expectedCubicYoctoMeters)
    {
        // Given / When
        Volume<double> volume = Volume<double>.FromCubicTeraMeters(value);

        // Then
        Assert.Equal(expectedCubicYoctoMeters, volume.CubicYoctoMeters, Tolerance);
    }

    [Theory(DisplayName = "Volume.FromCubicPetaMeters should produce the expected CubicYoctoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e117)]
    [InlineData(2.5, 2.5e117)]
    public void VolumeFromCubicPetaMetersShouldProduceExpectedCubicYoctoMeters(double value, double expectedCubicYoctoMeters)
    {
        // Given / When
        Volume<double> volume = Volume<double>.FromCubicPetaMeters(value);

        // Then
        Assert.Equal(expectedCubicYoctoMeters, volume.CubicYoctoMeters, Tolerance);
    }

    [Theory(DisplayName = "Volume.FromCubicExaMeters should produce the expected CubicYoctoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e126)]
    [InlineData(2.5, 2.5e126)]
    public void VolumeFromCubicExaMetersShouldProduceExpectedCubicYoctoMeters(double value, double expectedCubicYoctoMeters)
    {
        // Given / When
        Volume<double> volume = Volume<double>.FromCubicExaMeters(value);

        // Then
        Assert.Equal(expectedCubicYoctoMeters, volume.CubicYoctoMeters, Tolerance);
    }

    [Theory(DisplayName = "Volume.FromCubicZettaMeters should produce the expected CubicYoctoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e135)]
    [InlineData(2.5, 2.5e135)]
    public void VolumeFromCubicZettaMetersShouldProduceExpectedCubicYoctoMeters(double value, double expectedCubicYoctoMeters)
    {
        // Given / When
        Volume<double> volume = Volume<double>.FromCubicZettaMeters(value);

        // Then
        Assert.Equal(expectedCubicYoctoMeters, volume.CubicYoctoMeters, Tolerance);
    }

    [Theory(DisplayName = "Volume.FromCubicYottaMeters should produce the expected CubicYoctoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e144)]
    [InlineData(2.5, 2.5e144)]
    public void VolumeFromCubicYottaMetersShouldProduceExpectedCubicYoctoMeters(double value, double expectedCubicYoctoMeters)
    {
        // Given / When
        Volume<double> volume = Volume<double>.FromCubicYottaMeters(value);

        // Then
        Assert.Equal(expectedCubicYoctoMeters, volume.CubicYoctoMeters, Tolerance);
    }

    [Theory(DisplayName = "Volume.FromLiters should produce the expected CubicYoctoMeters")]
    [InlineData(0.0, 0.0)] // 0 L
    [InlineData(1.0, 1e69)] // 1 L = 1e-3 m^3
    [InlineData(2.5, 2.5e69)] // 2.5 L
    public void VolumeFromLitersShouldProduceExpectedCubicYoctoMeters(double value, double expectedCubicYoctoMeters)
    {
        // Given / When
        Volume<double> volume = Volume<double>.FromLiters(value);

        // Then
        Assert.Equal(expectedCubicYoctoMeters, volume.CubicYoctoMeters, Tolerance);
    }

    [Theory(DisplayName = "Volume.FromMilliLiters should produce the expected CubicYoctoMeters")]
    [InlineData(0.0, 0.0)] // 0 mL
    [InlineData(1.0, 1e66)] // 1 mL = 1 cm^3 = 1e-6 m^3
    [InlineData(250.0, 2.5e68)] // 250 mL
    public void VolumeFromMilliLitersShouldProduceExpectedCubicYoctoMeters(double value, double expectedCubicYoctoMeters)
    {
        // Given / When
        Volume<double> volume = Volume<double>.FromMilliLiters(value);

        // Then
        Assert.Equal(expectedCubicYoctoMeters, volume.CubicYoctoMeters, Tolerance);
    }

    [Theory(DisplayName = "Volume.FromCentiLiters should produce the expected CubicYoctoMeters")]
    [InlineData(0.0, 0.0)] // 0 cL
    [InlineData(1.0, 1e67)] // 1 cL = 10 mL
    [InlineData(250.0, 2.5e69)] // 250 cL = 2.5 L
    public void VolumeFromCentiLitersShouldProduceExpectedCubicYoctoMeters(double value, double expectedCubicYoctoMeters)
    {
        // Given / When
        Volume<double> volume = Volume<double>.FromCentiLiters(value);

        // Then
        Assert.Equal(expectedCubicYoctoMeters, volume.CubicYoctoMeters, Tolerance);
    }

    [Theory(DisplayName = "Volume.FromDeciLiters should produce the expected CubicYoctoMeters")]
    [InlineData(0.0, 0.0)] // 0 dL
    [InlineData(1.0, 1e68)] // 1 dL = 0.1 L
    [InlineData(25.0, 2.5e69)] // 25 dL = 2.5 L
    public void VolumeFromDeciLitersShouldProduceExpectedCubicYoctoMeters(double value, double expectedCubicYoctoMeters)
    {
        // Given / When
        Volume<double> volume = Volume<double>.FromDeciLiters(value);

        // Then
        Assert.Equal(expectedCubicYoctoMeters, volume.CubicYoctoMeters, Tolerance);
    }

    [Theory(DisplayName = "Volume.FromCubicInches should produce the expected CubicYoctoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1.6387064e67)] // 1 in^3 = (0.0254 m)^3
    [InlineData(2.5, 2.5 * 1.6387064e67)]
    public void VolumeFromCubicInchesShouldProduceExpectedCubicYoctoMeters(double value, double expectedCubicYoctoMeters)
    {
        // Given / When
        Volume<double> volume = Volume<double>.FromCubicInches(value);

        // Then
        Assert.Equal(expectedCubicYoctoMeters, volume.CubicYoctoMeters, Tolerance);
    }

    [Theory(DisplayName = "Volume.FromCubicFeet should produce the expected CubicYoctoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 2.8316846592e70)] // 1 ft^3 = (0.3048 m)^3
    [InlineData(2.5, 2.5 * 2.8316846592e70)]
    public void VolumeFromCubicFeetShouldProduceExpectedCubicYoctoMeters(double value, double expectedCubicYoctoMeters)
    {
        // Given / When
        Volume<double> volume = Volume<double>.FromCubicFeet(value);

        // Then
        Assert.Equal(expectedCubicYoctoMeters, volume.CubicYoctoMeters, Tolerance);
    }

    [Theory(DisplayName = "Volume.FromCubicYards should produce the expected CubicYoctoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 7.64554857984e71)] // 1 yd^3 = (0.9144 m)^3
    [InlineData(2.5, 2.5 * 7.64554857984e71)]
    public void VolumeFromCubicYardsShouldProduceExpectedCubicYoctoMeters(double value, double expectedCubicYoctoMeters)
    {
        // Given / When
        Volume<double> volume = Volume<double>.FromCubicYards(value);

        // Then
        Assert.Equal(expectedCubicYoctoMeters, volume.CubicYoctoMeters, Tolerance);
    }

    [Theory(DisplayName = "Volume.FromFluidOunces should produce the expected CubicYoctoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 2.95735295625e67)] // 1 US fl oz ≈ 29.5735295625 mL
    [InlineData(2.5, 2.5 * 2.95735295625e67)]
    public void VolumeFromFluidOuncesShouldProduceExpectedCubicYoctoMeters(double value, double expectedCubicYoctoMeters)
    {
        // Given / When
        Volume<double> volume = Volume<double>.FromFluidOunces(value);

        // Then
        Assert.Equal(expectedCubicYoctoMeters, volume.CubicYoctoMeters, Tolerance);
    }

    [Theory(DisplayName = "Volume.FromCups should produce the expected CubicYoctoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 2.365882365e68)] // 1 cup = 8 fl oz
    [InlineData(2.5, 2.5 * 2.365882365e68)]
    public void VolumeFromCupsShouldProduceExpectedCubicYoctoMeters(double value, double expectedCubicYoctoMeters)
    {
        // Given / When
        Volume<double> volume = Volume<double>.FromCups(value);

        // Then
        Assert.Equal(expectedCubicYoctoMeters, volume.CubicYoctoMeters, Tolerance);
    }

    [Theory(DisplayName = "Volume.FromPints should produce the expected CubicYoctoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 4.73176473e68)] // 1 pint = 16 fl oz
    [InlineData(2.5, 2.5 * 4.73176473e68)]
    public void VolumeFromPintsShouldProduceExpectedCubicYoctoMeters(double value, double expectedCubicYoctoMeters)
    {
        // Given / When
        Volume<double> volume = Volume<double>.FromPints(value);

        // Then
        Assert.Equal(expectedCubicYoctoMeters, volume.CubicYoctoMeters, Tolerance);
    }

    [Theory(DisplayName = "Volume.FromQuarts should produce the expected CubicYoctoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 9.46352946e68)] // 1 quart = 32 fl oz
    [InlineData(2.5, 2.5 * 9.46352946e68)]
    public void VolumeFromQuartsShouldProduceExpectedCubicYoctoMeters(double value, double expectedCubicYoctoMeters)
    {
        // Given / When
        Volume<double> volume = Volume<double>.FromQuarts(value);

        // Then
        Assert.Equal(expectedCubicYoctoMeters, volume.CubicYoctoMeters, Tolerance);
    }

    [Theory(DisplayName = "Volume.FromGallons should produce the expected CubicYoctoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 3.785411784e69)] // 1 gallon = 128 fl oz
    [InlineData(2.5, 2.5 * 3.785411784e69)]
    public void VolumeFromGallonsShouldProduceExpectedCubicYoctoMeters(double value, double expectedCubicYoctoMeters)
    {
        // Given / When
        Volume<double> volume = Volume<double>.FromGallons(value);

        // Then
        Assert.Equal(expectedCubicYoctoMeters, volume.CubicYoctoMeters, Tolerance);
    }

    [Fact(DisplayName = "Volume comparison should produce the expected result (left equal to right)")]
    public void VolumeComparisonShouldProduceExpectedResultLeftEqualToRight()
    {
        // Given
        Volume<double> left = Volume<double>.FromCubicMeters(1234.0);
        Volume<double> right = Volume<double>.FromCubicMeters(1234.0);

        // When / Then
        Assert.Equal(0, Volume<double>.Compare(left, right));
        Assert.Equal(0, left.CompareTo(right));
        Assert.Equal(0, left.CompareTo((object)right));
        Assert.False(left > right);
        Assert.True(left >= right);
        Assert.False(left < right);
        Assert.True(left <= right);
    }

    [Fact(DisplayName = "Volume comparison should produce the expected result (left greater than right)")]
    public void VolumeComparisonShouldProduceExpectedResultLeftGreaterThanRight()
    {
        // Given
        Volume<double> left = Volume<double>.FromCubicMeters(2000.0);
        Volume<double> right = Volume<double>.FromCubicMeters(1500.0);

        // When / Then
        Assert.Equal(1, Volume<double>.Compare(left, right));
        Assert.Equal(1, left.CompareTo(right));
        Assert.Equal(1, left.CompareTo((object)right));
        Assert.True(left > right);
        Assert.True(left >= right);
        Assert.False(left < right);
        Assert.False(left <= right);
    }

    [Fact(DisplayName = "Volume comparison should produce the expected result (left less than right)")]
    public void VolumeComparisonShouldProduceExpectedResultLeftLessThanRight()
    {
        // Given
        Volume<double> left = Volume<double>.FromCubicMeters(1500.0);
        Volume<double> right = Volume<double>.FromCubicMeters(2000.0);

        // When / Then
        Assert.Equal(-1, Volume<double>.Compare(left, right));
        Assert.Equal(-1, left.CompareTo(right));
        Assert.Equal(-1, left.CompareTo((object)right));
        Assert.False(left > right);
        Assert.False(left >= right);
        Assert.True(left < right);
        Assert.True(left <= right);
    }

    [Fact(DisplayName = "Volume equality should produce the expected result (left equal to right)")]
    public void VolumeEqualityShouldProduceExpectedResultLeftEqualToRight()
    {
        // Given
        Volume<BigDecimal> left = Volume<BigDecimal>.FromCubicMeters(2.0);
        Volume<BigDecimal> right = Volume<BigDecimal>.FromLiters(2000.0);

        // When / Then
        Assert.True(Volume<BigDecimal>.Equals(left, right));
        Assert.True(left.Equals(right));
        Assert.True(left.Equals((object)right));
        Assert.True(left == right);
        Assert.False(left != right);
    }

    [Fact(DisplayName = "Volume equality should produce the expected result (left not equal to right)")]
    public void VolumeEqualityShouldProduceExpectedResultLeftNotEqualToRight()
    {
        // Given
        Volume<double> left = Volume<double>.FromCubicMeters(2.0);
        Volume<double> right = Volume<double>.FromLiters(2500.0);

        // When / Then
        Assert.False(Volume<double>.Equals(left, right));
        Assert.False(left.Equals(right));
        Assert.False(left.Equals((object)right));
        Assert.False(left == right);
        Assert.True(left != right);
    }

    [Fact(DisplayName = "Volume.ToString should produce the expected result")]
    public void VolumeToStringShouldProduceExpectedResult()
    {
        // Given
        Volume<double> volume = Volume<double>.FromCubicMeters(1.0);

        // When / Then
        // 1 m³ = 1,000 L = 1,000,000 mL
        Assert.Equal("1.000 cum", $"{volume:cum3}");
        Assert.Equal("1,000.000 l", $"{volume:l3}");
        Assert.Equal("1,000,000.000 ml", $"{volume:ml3}");

        // Using some imperial conversions for sanity-check formatting.
        Assert.Equal("35.315 cuft", $"{volume:cuft3}");
        Assert.Equal("61,023.744 cuin", $"{volume:cuin3}");
    }

    [Fact(DisplayName = "Volume.ToString should honor custom culture separators")]
    public void VolumeToStringShouldHonorCustomCulture()
    {
        // Given
        CultureInfo customCulture = new("de-DE");
        Volume<double> volume = Volume<double>.FromLiters(1234.56); // 1,234.56 L

        // When
        string formatted = volume.ToString("l2", customCulture);

        // Then (German uses '.' for thousands and ',' for decimals)
        Assert.Equal("1.234,56 l", formatted);
    }
}
