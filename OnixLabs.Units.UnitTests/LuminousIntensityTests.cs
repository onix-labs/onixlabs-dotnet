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

public sealed class LuminousIntensityTests
{
    // IEEE-754 binary floating-point arithmetic causes small discrepancies in calculation, therefore we need a tolerance.
    private const double Tolerance = 1e+42;

    [Fact(DisplayName = "LuminousIntensity.Zero should produce the expected result")]
    public void LuminousIntensityZeroShouldProduceExpectedResult()
    {
        // Given / When
        LuminousIntensity<double> intensity = LuminousIntensity<double>.Zero;

        // Then
        Assert.Equal(0.0, intensity.QuectoCandelas, Tolerance);
    }

    [Theory(DisplayName = "LuminousIntensity.FromQuectocandelas should produce the expected QuectoCandelas")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1.0)]
    [InlineData(2.5, 2.5)]
    public void LuminousIntensityFromQuectocandelasShouldProduceExpectedQuectoCandelas(double value, double expected)
    {
        LuminousIntensity<double> l = LuminousIntensity<double>.FromQuectocandelas(value);
        Assert.Equal(expected, l.QuectoCandelas, Tolerance);
    }

    [Theory(DisplayName = "LuminousIntensity.FromRontocandelas should produce the expected QuectoCandelas")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e3)]
    [InlineData(2.5, 2.5e3)]
    public void LuminousIntensityFromRontocandelasShouldProduceExpectedQuectoCandelas(double value, double expected)
    {
        LuminousIntensity<double> l = LuminousIntensity<double>.FromRontocandelas(value);
        Assert.Equal(expected, l.QuectoCandelas, Tolerance);
    }

    [Theory(DisplayName = "LuminousIntensity.FromYoctocandelas should produce the expected QuectoCandelas")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e6)]
    [InlineData(2.5, 2.5e6)]
    public void LuminousIntensityFromYoctocandelasShouldProduceExpectedQuectoCandelas(double value, double expected)
    {
        LuminousIntensity<double> l = LuminousIntensity<double>.FromYoctocandelas(value);
        Assert.Equal(expected, l.QuectoCandelas, Tolerance);
    }

    [Theory(DisplayName = "LuminousIntensity.FromZeptocandelas should produce the expected QuectoCandelas")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e9)]
    [InlineData(2.5, 2.5e9)]
    public void LuminousIntensityFromZeptocandelasShouldProduceExpectedQuectoCandelas(double value, double expected)
    {
        LuminousIntensity<double> l = LuminousIntensity<double>.FromZeptocandelas(value);
        Assert.Equal(expected, l.QuectoCandelas, Tolerance);
    }

    [Theory(DisplayName = "LuminousIntensity.FromAttocandelas should produce the expected QuectoCandelas")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e12)]
    [InlineData(2.5, 2.5e12)]
    public void LuminousIntensityFromAttocandelasShouldProduceExpectedQuectoCandelas(double value, double expected)
    {
        LuminousIntensity<double> l = LuminousIntensity<double>.FromAttocandelas(value);
        Assert.Equal(expected, l.QuectoCandelas, Tolerance);
    }

    [Theory(DisplayName = "LuminousIntensity.FromFemtocandelas should produce the expected QuectoCandelas")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e15)]
    [InlineData(2.5, 2.5e15)]
    public void LuminousIntensityFromFemtocandelasShouldProduceExpectedQuectoCandelas(double value, double expected)
    {
        LuminousIntensity<double> l = LuminousIntensity<double>.FromFemtocandelas(value);
        Assert.Equal(expected, l.QuectoCandelas, Tolerance);
    }

    [Theory(DisplayName = "LuminousIntensity.FromPicocandelas should produce the expected QuectoCandelas")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e18)]
    [InlineData(2.5, 2.5e18)]
    public void LuminousIntensityFromPicocandelasShouldProduceExpectedQuectoCandelas(double value, double expected)
    {
        LuminousIntensity<double> l = LuminousIntensity<double>.FromPicocandelas(value);
        Assert.Equal(expected, l.QuectoCandelas, Tolerance);
    }

    [Theory(DisplayName = "LuminousIntensity.FromNanocandelas should produce the expected QuectoCandelas")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e21)]
    [InlineData(2.5, 2.5e21)]
    public void LuminousIntensityFromNanocandelasShouldProduceExpectedQuectoCandelas(double value, double expected)
    {
        LuminousIntensity<double> l = LuminousIntensity<double>.FromNanocandelas(value);
        Assert.Equal(expected, l.QuectoCandelas, Tolerance);
    }

    [Theory(DisplayName = "LuminousIntensity.FromMicrocandelas should produce the expected QuectoCandelas")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e24)]
    [InlineData(2.5, 2.5e24)]
    public void LuminousIntensityFromMicrocandelasShouldProduceExpectedQuectoCandelas(double value, double expected)
    {
        LuminousIntensity<double> l = LuminousIntensity<double>.FromMicrocandelas(value);
        Assert.Equal(expected, l.QuectoCandelas, Tolerance);
    }

    [Theory(DisplayName = "LuminousIntensity.FromMillicandelas should produce the expected QuectoCandelas")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e27)]
    [InlineData(2.5, 2.5e27)]
    public void LuminousIntensityFromMillicandelasShouldProduceExpectedQuectoCandelas(double value, double expected)
    {
        LuminousIntensity<double> l = LuminousIntensity<double>.FromMillicandelas(value);
        Assert.Equal(expected, l.QuectoCandelas, Tolerance);
    }

    [Theory(DisplayName = "LuminousIntensity.FromCenticandelas should produce the expected QuectoCandelas")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e28)]
    [InlineData(2.5, 2.5e28)]
    public void LuminousIntensityFromCenticandelasShouldProduceExpectedQuectoCandelas(double value, double expected)
    {
        LuminousIntensity<double> l = LuminousIntensity<double>.FromCenticandelas(value);
        Assert.Equal(expected, l.QuectoCandelas, Tolerance);
    }

    [Theory(DisplayName = "LuminousIntensity.FromDecicandelas should produce the expected QuectoCandelas")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e29)]
    [InlineData(2.5, 2.5e29)]
    public void LuminousIntensityFromDecicandelasShouldProduceExpectedQuectoCandelas(double value, double expected)
    {
        LuminousIntensity<double> l = LuminousIntensity<double>.FromDecicandelas(value);
        Assert.Equal(expected, l.QuectoCandelas, Tolerance);
    }

    [Theory(DisplayName = "LuminousIntensity.FromCandelas should produce the expected QuectoCandelas")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e30)]
    [InlineData(2.5, 2.5e30)]
    public void LuminousIntensityFromCandelasShouldProduceExpectedQuectoCandelas(double value, double expected)
    {
        LuminousIntensity<double> l = LuminousIntensity<double>.FromCandelas(value);
        Assert.Equal(expected, l.QuectoCandelas, Tolerance);
    }

    [Theory(DisplayName = "LuminousIntensity.FromDecacandelas should produce the expected QuectoCandelas")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e31)]
    [InlineData(2.5, 2.5e31)]
    public void LuminousIntensityFromDecacandelasShouldProduceExpectedQuectoCandelas(double value, double expected)
    {
        LuminousIntensity<double> l = LuminousIntensity<double>.FromDecacandelas(value);
        Assert.Equal(expected, l.QuectoCandelas, Tolerance);
    }

    [Theory(DisplayName = "LuminousIntensity.FromHectocandelas should produce the expected QuectoCandelas")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e32)]
    [InlineData(2.5, 2.5e32)]
    public void LuminousIntensityFromHectocandelasShouldProduceExpectedQuectoCandelas(double value, double expected)
    {
        LuminousIntensity<double> l = LuminousIntensity<double>.FromHectocandelas(value);
        Assert.Equal(expected, l.QuectoCandelas, Tolerance);
    }

    [Theory(DisplayName = "LuminousIntensity.FromKilocandelas should produce the expected QuectoCandelas")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e33)]
    [InlineData(2.5, 2.5e33)]
    public void LuminousIntensityFromKilocandelasShouldProduceExpectedQuectoCandelas(double value, double expected)
    {
        LuminousIntensity<double> l = LuminousIntensity<double>.FromKilocandelas(value);
        Assert.Equal(expected, l.QuectoCandelas, Tolerance);
    }

    [Theory(DisplayName = "LuminousIntensity.FromMegacandelas should produce the expected QuectoCandelas")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e36)]
    [InlineData(2.5, 2.5e36)]
    public void LuminousIntensityFromMegacandelasShouldProduceExpectedQuectoCandelas(double value, double expected)
    {
        LuminousIntensity<double> l = LuminousIntensity<double>.FromMegacandelas(value);
        Assert.Equal(expected, l.QuectoCandelas, Tolerance);
    }

    [Theory(DisplayName = "LuminousIntensity.FromGigacandelas should produce the expected QuectoCandelas")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e39)]
    [InlineData(2.5, 2.5e39)]
    public void LuminousIntensityFromGigacandelasShouldProduceExpectedQuectoCandelas(double value, double expected)
    {
        LuminousIntensity<double> l = LuminousIntensity<double>.FromGigacandelas(value);
        Assert.Equal(expected, l.QuectoCandelas, Tolerance);
    }

    [Theory(DisplayName = "LuminousIntensity.FromTeracandelas should produce the expected QuectoCandelas")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e42)]
    [InlineData(2.5, 2.5e42)]
    public void LuminousIntensityFromTeracandelasShouldProduceExpectedQuectoCandelas(double value, double expected)
    {
        LuminousIntensity<double> l = LuminousIntensity<double>.FromTeracandelas(value);
        Assert.Equal(expected, l.QuectoCandelas, Tolerance);
    }

    [Theory(DisplayName = "LuminousIntensity.FromPetacandelas should produce the expected QuectoCandelas")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e45)]
    [InlineData(2.5, 2.5e45)]
    public void LuminousIntensityFromPetacandelasShouldProduceExpectedQuectoCandelas(double value, double expected)
    {
        LuminousIntensity<double> l = LuminousIntensity<double>.FromPetacandelas(value);
        Assert.Equal(expected, l.QuectoCandelas, Tolerance);
    }

    [Theory(DisplayName = "LuminousIntensity.FromExacandelas should produce the expected QuectoCandelas")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e48)]
    [InlineData(2.5, 2.5e48)]
    public void LuminousIntensityFromExacandelasShouldProduceExpectedQuectoCandelas(double value, double expected)
    {
        LuminousIntensity<double> l = LuminousIntensity<double>.FromExacandelas(value);
        Assert.Equal(expected, l.QuectoCandelas, Tolerance);
    }

    [Theory(DisplayName = "LuminousIntensity.FromZettacandelas should produce the expected QuectoCandelas")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e51)]
    [InlineData(2.5, 2.5e51)]
    public void LuminousIntensityFromZettacandelasShouldProduceExpectedQuectoCandelas(double value, double expected)
    {
        LuminousIntensity<double> l = LuminousIntensity<double>.FromZettacandelas(value);
        Assert.Equal(expected, l.QuectoCandelas, Tolerance);
    }

    [Theory(DisplayName = "LuminousIntensity.FromYottacandelas should produce the expected QuectoCandelas")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e54)]
    [InlineData(2.5, 2.5e54)]
    public void LuminousIntensityFromYottacandelasShouldProduceExpectedQuectoCandelas(double value, double expected)
    {
        LuminousIntensity<double> l = LuminousIntensity<double>.FromYottacandelas(value);
        Assert.Equal(expected, l.QuectoCandelas, Tolerance);
    }

    [Theory(DisplayName = "LuminousIntensity.FromRonnacandelas should produce the expected QuectoCandelas")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e57)]
    [InlineData(2.5, 2.5e57)]
    public void LuminousIntensityFromRonnacandelasShouldProduceExpectedQuectoCandelas(double value, double expected)
    {
        LuminousIntensity<double> l = LuminousIntensity<double>.FromRonnacandelas(value);
        Assert.Equal(expected, l.QuectoCandelas, Tolerance);
    }

    [Theory(DisplayName = "LuminousIntensity.FromQuettacandelas should produce the expected QuectoCandelas")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e60)]
    [InlineData(2.5, 2.5e60)]
    public void LuminousIntensityFromQuettacandelasShouldProduceExpectedQuectoCandelas(double value, double expected)
    {
        LuminousIntensity<double> l = LuminousIntensity<double>.FromQuettacandelas(value);
        Assert.Equal(expected, l.QuectoCandelas, Tolerance);
    }

    [Fact(DisplayName = "LuminousIntensity.Add should produce the expected result")]
    public void LuminousIntensityAddShouldProduceExpectedValue()
    {
        // Given
        LuminousIntensity<double> left = LuminousIntensity<double>.FromCandelas(1.5);
        LuminousIntensity<double> right = LuminousIntensity<double>.FromCandelas(0.5);

        // When
        LuminousIntensity<double> result = left.Add(right);

        // Then
        Assert.Equal(2.0, result.Candelas, Tolerance);
    }

    [Fact(DisplayName = "LuminousIntensity.Subtract should produce the expected result")]
    public void LuminousIntensitySubtractShouldProduceExpectedValue()
    {
        // Given
        LuminousIntensity<double> left = LuminousIntensity<double>.FromCandelas(1.5);
        LuminousIntensity<double> right = LuminousIntensity<double>.FromCandelas(0.4);

        // When
        LuminousIntensity<double> result = left.Subtract(right);

        // Then
        Assert.Equal(1.1, result.Candelas, Tolerance);
    }

    [Fact(DisplayName = "LuminousIntensity.Multiply should produce the expected result")]
    public void LuminousIntensityMultiplyShouldProduceExpectedValue()
    {
        // Given
        LuminousIntensity<double> left = LuminousIntensity<double>.FromCandelas(10.0);  // 1e31 qcd
        LuminousIntensity<double> right = LuminousIntensity<double>.FromCandelas(3.0);  // 3e30 qcd

        // When
        LuminousIntensity<double> result = left.Multiply(right);  // 1e31 * 3e30 = 3e61 qcd

        // Then
        Assert.Equal(1e31, left.QuectoCandelas, Tolerance);
        Assert.Equal(3e30, right.QuectoCandelas, Tolerance);
        Assert.Equal(3e61, result.QuectoCandelas, Tolerance);
    }

    [Fact(DisplayName = "LuminousIntensity.Divide should produce the expected result")]
    public void LuminousIntensityDivideShouldProduceExpectedValue()
    {
        // Given
        LuminousIntensity<double> left = LuminousIntensity<double>.FromCandelas(100.0);  // 1e32 qcd
        LuminousIntensity<double> right = LuminousIntensity<double>.FromCandelas(20.0);  // 2e31 qcd

        // When
        LuminousIntensity<double> result = left.Divide(right);  // 1e32 / 2e31 = 5 qcd

        // Then
        Assert.Equal(5.0, result.QuectoCandelas, Tolerance);
        Assert.Equal(5e-30, result.Candelas, Tolerance);
    }

    [Fact(DisplayName = "LuminousIntensity comparison should produce the expected result (left equal to right)")]
    public void LuminousIntensityComparisonShouldProduceExpectedResultLeftEqualToRight()
    {
        // Given
        LuminousIntensity<double> left = LuminousIntensity<double>.FromCandelas(123.0);
        LuminousIntensity<double> right = LuminousIntensity<double>.FromCandelas(123.0);

        // When / Then
        Assert.Equal(0, LuminousIntensity<double>.Compare(left, right));
        Assert.Equal(0, left.CompareTo(right));
        Assert.Equal(0, left.CompareTo((object)right));
        Assert.False(left > right);
        Assert.True(left >= right);
        Assert.False(left < right);
        Assert.True(left <= right);
    }

    [Fact(DisplayName = "LuminousIntensity comparison should produce the expected result (left greater than right)")]
    public void LuminousIntensityComparisonShouldProduceExpectedLeftGreaterThanRight()
    {
        // Given
        LuminousIntensity<double> left = LuminousIntensity<double>.FromCandelas(456.0);
        LuminousIntensity<double> right = LuminousIntensity<double>.FromCandelas(123.0);

        // When / Then
        Assert.Equal(1, LuminousIntensity<double>.Compare(left, right));
        Assert.Equal(1, left.CompareTo(right));
        Assert.Equal(1, left.CompareTo((object)right));
        Assert.True(left > right);
        Assert.True(left >= right);
        Assert.False(left < right);
        Assert.False(left <= right);
    }

    [Fact(DisplayName = "LuminousIntensity comparison should produce the expected result (left less than right)")]
    public void LuminousIntensityComparisonShouldProduceExpectedLeftLessThanRight()
    {
        // Given
        LuminousIntensity<double> left = LuminousIntensity<double>.FromCandelas(123.0);
        LuminousIntensity<double> right = LuminousIntensity<double>.FromCandelas(456.0);

        // When / Then
        Assert.Equal(-1, LuminousIntensity<double>.Compare(left, right));
        Assert.Equal(-1, left.CompareTo(right));
        Assert.Equal(-1, left.CompareTo((object)right));
        Assert.False(left > right);
        Assert.False(left >= right);
        Assert.True(left < right);
        Assert.True(left <= right);
    }

    [Fact(DisplayName = "LuminousIntensity equality should produce the expected result (left equal to right)")]
    public void LuminousIntensityEqualityShouldProduceExpectedResultLeftEqualToRight()
    {
        // Given: 2 cd = 2000 mcd
        LuminousIntensity<BigDecimal> left = LuminousIntensity<BigDecimal>.FromCandelas(2.0);
        LuminousIntensity<BigDecimal> right = LuminousIntensity<BigDecimal>.FromMillicandelas(2000.0);

        // When / Then
        Assert.True(LuminousIntensity<BigDecimal>.Equals(left, right));
        Assert.True(left.Equals(right));
        Assert.True(left.Equals((object)right));
        Assert.True(left == right);
        Assert.False(left != right);
    }

    [Fact(DisplayName = "LuminousIntensity equality should produce the expected result (left not equal to right)")]
    public void LuminousIntensityEqualityShouldProduceExpectedResultLeftNotEqualToRight()
    {
        // Given
        LuminousIntensity<double> left = LuminousIntensity<double>.FromCandelas(2.0);
        LuminousIntensity<double> right = LuminousIntensity<double>.FromMillicandelas(2500.0);

        // When / Then
        Assert.False(LuminousIntensity<double>.Equals(left, right));
        Assert.False(left.Equals(right));
        Assert.False(left.Equals((object)right));
        Assert.False(left == right);
        Assert.True(left != right);
    }

    [Fact(DisplayName = "LuminousIntensity.ToString should produce the expected result")]
    public void LuminousIntensityToStringShouldProduceExpectedResult()
    {
        // Given
        LuminousIntensity<double> l = LuminousIntensity<double>.FromCandelas(1000.0);

        // When / Then
        Assert.Equal("1,000.000 cd", $"{l:cd3}");
        Assert.Equal("1.000 kcd", $"{l:kcd3}");
        Assert.Equal("0.001 Mcd", $"{l:Mcd3}");
        Assert.Equal("1,000,000.000 mcd", $"{l:mcd3}");
    }

    [Fact(DisplayName = "LuminousIntensity.ToString Mcd vs mcd are case-sensitive")]
    public void LuminousIntensityToStringMcdVsMcdAreCaseSensitive()
    {
        // Given
        LuminousIntensity<double> l = LuminousIntensity<double>.FromCandelas(1.0);

        // Then
        Assert.Equal("0.000001 Mcd", $"{l:Mcd6}"); // mega
        Assert.Equal("1,000.000 mcd", $"{l:mcd3}"); // milli
    }

    [Fact(DisplayName = "LuminousIntensity.ToString Pcd vs pcd are case-sensitive")]
    public void LuminousIntensityToStringPcdVsPcdAreCaseSensitive()
    {
        // Given
        LuminousIntensity<double> l = LuminousIntensity<double>.FromCandelas(1.0);

        // Then
        Assert.Equal("0.000000000000001 Pcd", $"{l:Pcd15}"); // peta
        Assert.Equal("1,000,000,000,000.000 pcd", $"{l:pcd3}"); // pico
    }

    [Fact(DisplayName = "LuminousIntensity.ToString µcd symbol should differ from format specifier")]
    public void LuminousIntensityToStringMicrocandelasSymbolShouldDifferFromFormatSpecifier()
    {
        // Given
        LuminousIntensity<double> l = LuminousIntensity<double>.FromCandelas(1.0);

        // Then: specifier is ucd, but symbol rendered is µcd
        Assert.Equal("1,000,000.000 µcd", $"{l:ucd3}");
    }

    [Fact(DisplayName = "LuminousIntensity.ToString should honor custom culture separators")]
    public void LuminousIntensityToStringShouldHonorCustomCulture()
    {
        // Given
        CultureInfo customCulture = new("de-DE");
        LuminousIntensity<double> l = LuminousIntensity<double>.FromCandelas(1234.56);

        // When
        string formatted = l.ToString("cd2", customCulture);

        // Then
        Assert.Equal("1.234,56 cd", formatted);
    }
}
