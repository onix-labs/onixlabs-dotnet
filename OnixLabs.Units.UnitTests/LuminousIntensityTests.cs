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
    [Fact(DisplayName = "LuminousIntensity.Zero should produce the expected result")]
    public void LuminousIntensityZeroShouldProduceExpectedResult()
    {
        // Given / When
        LuminousIntensity<Float128> intensity = LuminousIntensity<Float128>.Zero;

        // Then
        Assert.Equal(Float128.Zero, intensity.QuectoCandelas);
    }

    [Theory(DisplayName = "LuminousIntensity.FromQuectocandelas should produce the expected QuectoCandelas")]
    [InlineData("0", "0")]
    [InlineData("1", "1")]
    [InlineData("2.5", "2.5")]
    public void LuminousIntensityFromQuectocandelasShouldProduceExpectedQuectoCandelas(string value, string expected)
    {
        LuminousIntensity<Float128> l = LuminousIntensity<Float128>.FromQuectocandelas(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), l.QuectoCandelas);
    }

    [Theory(DisplayName = "LuminousIntensity.FromRontocandelas should produce the expected QuectoCandelas")]
    [InlineData("0", "0")]
    [InlineData("1", "1e3")]
    [InlineData("2.5", "2.5e3")]
    public void LuminousIntensityFromRontocandelasShouldProduceExpectedQuectoCandelas(string value, string expected)
    {
        LuminousIntensity<Float128> l = LuminousIntensity<Float128>.FromRontocandelas(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), l.QuectoCandelas);
    }

    [Theory(DisplayName = "LuminousIntensity.FromYoctocandelas should produce the expected QuectoCandelas")]
    [InlineData("0", "0")]
    [InlineData("1", "1e6")]
    [InlineData("2.5", "2.5e6")]
    public void LuminousIntensityFromYoctocandelasShouldProduceExpectedQuectoCandelas(string value, string expected)
    {
        LuminousIntensity<Float128> l = LuminousIntensity<Float128>.FromYoctocandelas(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), l.QuectoCandelas);
    }

    [Theory(DisplayName = "LuminousIntensity.FromZeptocandelas should produce the expected QuectoCandelas")]
    [InlineData("0", "0")]
    [InlineData("1", "1e9")]
    [InlineData("2.5", "2.5e9")]
    public void LuminousIntensityFromZeptocandelasShouldProduceExpectedQuectoCandelas(string value, string expected)
    {
        LuminousIntensity<Float128> l = LuminousIntensity<Float128>.FromZeptocandelas(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), l.QuectoCandelas);
    }

    [Theory(DisplayName = "LuminousIntensity.FromAttocandelas should produce the expected QuectoCandelas")]
    [InlineData("0", "0")]
    [InlineData("1", "1e12")]
    [InlineData("2.5", "2.5e12")]
    public void LuminousIntensityFromAttocandelasShouldProduceExpectedQuectoCandelas(string value, string expected)
    {
        LuminousIntensity<Float128> l = LuminousIntensity<Float128>.FromAttocandelas(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), l.QuectoCandelas);
    }

    [Theory(DisplayName = "LuminousIntensity.FromFemtocandelas should produce the expected QuectoCandelas")]
    [InlineData("0", "0")]
    [InlineData("1", "1e15")]
    [InlineData("2.5", "2.5e15")]
    public void LuminousIntensityFromFemtocandelasShouldProduceExpectedQuectoCandelas(string value, string expected)
    {
        LuminousIntensity<Float128> l = LuminousIntensity<Float128>.FromFemtocandelas(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), l.QuectoCandelas);
    }

    [Theory(DisplayName = "LuminousIntensity.FromPicocandelas should produce the expected QuectoCandelas")]
    [InlineData("0", "0")]
    [InlineData("1", "1e18")]
    [InlineData("2.5", "2.5e18")]
    public void LuminousIntensityFromPicocandelasShouldProduceExpectedQuectoCandelas(string value, string expected)
    {
        LuminousIntensity<Float128> l = LuminousIntensity<Float128>.FromPicocandelas(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), l.QuectoCandelas);
    }

    [Theory(DisplayName = "LuminousIntensity.FromNanocandelas should produce the expected QuectoCandelas")]
    [InlineData("0", "0")]
    [InlineData("1", "1e21")]
    [InlineData("2.5", "2.5e21")]
    public void LuminousIntensityFromNanocandelasShouldProduceExpectedQuectoCandelas(string value, string expected)
    {
        LuminousIntensity<Float128> l = LuminousIntensity<Float128>.FromNanocandelas(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), l.QuectoCandelas);
    }

    [Theory(DisplayName = "LuminousIntensity.FromMicrocandelas should produce the expected QuectoCandelas")]
    [InlineData("0", "0")]
    [InlineData("1", "1e24")]
    [InlineData("2.5", "2.5e24")]
    public void LuminousIntensityFromMicrocandelasShouldProduceExpectedQuectoCandelas(string value, string expected)
    {
        LuminousIntensity<Float128> l = LuminousIntensity<Float128>.FromMicrocandelas(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), l.QuectoCandelas);
    }

    [Theory(DisplayName = "LuminousIntensity.FromMillicandelas should produce the expected QuectoCandelas")]
    [InlineData("0", "0")]
    [InlineData("1", "1e27")]
    [InlineData("2.5", "2.5e27")]
    public void LuminousIntensityFromMillicandelasShouldProduceExpectedQuectoCandelas(string value, string expected)
    {
        LuminousIntensity<Float128> l = LuminousIntensity<Float128>.FromMillicandelas(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), l.QuectoCandelas);
    }

    [Theory(DisplayName = "LuminousIntensity.FromCenticandelas should produce the expected QuectoCandelas")]
    [InlineData("0", "0")]
    [InlineData("1", "1e28")]
    [InlineData("2.5", "2.5e28")]
    public void LuminousIntensityFromCenticandelasShouldProduceExpectedQuectoCandelas(string value, string expected)
    {
        LuminousIntensity<Float128> l = LuminousIntensity<Float128>.FromCenticandelas(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), l.QuectoCandelas);
    }

    [Theory(DisplayName = "LuminousIntensity.FromDecicandelas should produce the expected QuectoCandelas")]
    [InlineData("0", "0")]
    [InlineData("1", "1e29")]
    [InlineData("2.5", "2.5e29")]
    public void LuminousIntensityFromDecicandelasShouldProduceExpectedQuectoCandelas(string value, string expected)
    {
        LuminousIntensity<Float128> l = LuminousIntensity<Float128>.FromDecicandelas(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), l.QuectoCandelas);
    }

    [Theory(DisplayName = "LuminousIntensity.FromCandelas should produce the expected QuectoCandelas")]
    [InlineData("0", "0")]
    [InlineData("1", "1e30")]
    [InlineData("2.5", "2.5e30")]
    public void LuminousIntensityFromCandelasShouldProduceExpectedQuectoCandelas(string value, string expected)
    {
        LuminousIntensity<Float128> l = LuminousIntensity<Float128>.FromCandelas(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), l.QuectoCandelas);
    }

    [Theory(DisplayName = "LuminousIntensity.FromDecacandelas should produce the expected QuectoCandelas")]
    [InlineData("0", "0")]
    [InlineData("1", "1e31")]
    [InlineData("2.5", "2.5e31")]
    public void LuminousIntensityFromDecacandelasShouldProduceExpectedQuectoCandelas(string value, string expected)
    {
        LuminousIntensity<Float128> l = LuminousIntensity<Float128>.FromDecacandelas(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), l.QuectoCandelas);
    }

    [Theory(DisplayName = "LuminousIntensity.FromHectocandelas should produce the expected QuectoCandelas")]
    [InlineData("0", "0")]
    [InlineData("1", "1e32")]
    [InlineData("2.5", "2.5e32")]
    public void LuminousIntensityFromHectocandelasShouldProduceExpectedQuectoCandelas(string value, string expected)
    {
        LuminousIntensity<Float128> l = LuminousIntensity<Float128>.FromHectocandelas(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), l.QuectoCandelas);
    }

    [Theory(DisplayName = "LuminousIntensity.FromKilocandelas should produce the expected QuectoCandelas")]
    [InlineData("0", "0")]
    [InlineData("1", "1e33")]
    [InlineData("2.5", "2.5e33")]
    public void LuminousIntensityFromKilocandelasShouldProduceExpectedQuectoCandelas(string value, string expected)
    {
        LuminousIntensity<Float128> l = LuminousIntensity<Float128>.FromKilocandelas(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), l.QuectoCandelas);
    }

    [Theory(DisplayName = "LuminousIntensity.FromMegacandelas should produce the expected QuectoCandelas")]
    [InlineData("0", "0")]
    [InlineData("1", "1e36")]
    [InlineData("2.5", "2.5e36")]
    public void LuminousIntensityFromMegacandelasShouldProduceExpectedQuectoCandelas(string value, string expected)
    {
        LuminousIntensity<Float128> l = LuminousIntensity<Float128>.FromMegacandelas(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), l.QuectoCandelas);
    }

    [Theory(DisplayName = "LuminousIntensity.FromGigacandelas should produce the expected QuectoCandelas")]
    [InlineData("0", "0")]
    [InlineData("1", "1e39")]
    [InlineData("2.5", "2.5e39")]
    public void LuminousIntensityFromGigacandelasShouldProduceExpectedQuectoCandelas(string value, string expected)
    {
        LuminousIntensity<Float128> l = LuminousIntensity<Float128>.FromGigacandelas(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), l.QuectoCandelas);
    }

    [Theory(DisplayName = "LuminousIntensity.FromTeracandelas should produce the expected QuectoCandelas")]
    [InlineData("0", "0")]
    [InlineData("1", "1e42")]
    [InlineData("2.5", "2.5e42")]
    public void LuminousIntensityFromTeracandelasShouldProduceExpectedQuectoCandelas(string value, string expected)
    {
        LuminousIntensity<Float128> l = LuminousIntensity<Float128>.FromTeracandelas(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), l.QuectoCandelas);
    }

    [Theory(DisplayName = "LuminousIntensity.FromPetacandelas should produce the expected QuectoCandelas")]
    [InlineData("0", "0")]
    [InlineData("1", "1e45")]
    [InlineData("2.5", "2.5e45")]
    public void LuminousIntensityFromPetacandelasShouldProduceExpectedQuectoCandelas(string value, string expected)
    {
        LuminousIntensity<Float128> l = LuminousIntensity<Float128>.FromPetacandelas(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), l.QuectoCandelas);
    }

    [Theory(DisplayName = "LuminousIntensity.FromExacandelas should produce the expected QuectoCandelas")]
    [InlineData("0", "0")]
    [InlineData("1", "1e48")]
    [InlineData("2.5", "2.5e48")]
    public void LuminousIntensityFromExacandelasShouldProduceExpectedQuectoCandelas(string value, string expected)
    {
        LuminousIntensity<Float128> l = LuminousIntensity<Float128>.FromExacandelas(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), l.QuectoCandelas);
    }

    [Theory(DisplayName = "LuminousIntensity.FromZettacandelas should produce the expected QuectoCandelas")]
    [InlineData("0", "0")]
    [InlineData("1", "1e51")]
    [InlineData("2.5", "2.5e51")]
    public void LuminousIntensityFromZettacandelasShouldProduceExpectedQuectoCandelas(string value, string expected)
    {
        LuminousIntensity<Float128> l = LuminousIntensity<Float128>.FromZettacandelas(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), l.QuectoCandelas);
    }

    [Theory(DisplayName = "LuminousIntensity.FromYottacandelas should produce the expected QuectoCandelas")]
    [InlineData("0", "0")]
    [InlineData("1", "1e54")]
    [InlineData("2.5", "2.5e54")]
    public void LuminousIntensityFromYottacandelasShouldProduceExpectedQuectoCandelas(string value, string expected)
    {
        LuminousIntensity<Float128> l = LuminousIntensity<Float128>.FromYottacandelas(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), l.QuectoCandelas);
    }

    [Theory(DisplayName = "LuminousIntensity.FromRonnacandelas should produce the expected QuectoCandelas")]
    [InlineData("0", "0")]
    [InlineData("1", "1e57")]
    [InlineData("2.5", "2.5e57")]
    public void LuminousIntensityFromRonnacandelasShouldProduceExpectedQuectoCandelas(string value, string expected)
    {
        LuminousIntensity<Float128> l = LuminousIntensity<Float128>.FromRonnacandelas(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), l.QuectoCandelas);
    }

    [Theory(DisplayName = "LuminousIntensity.FromQuettacandelas should produce the expected QuectoCandelas")]
    [InlineData("0", "0")]
    [InlineData("1", "1e60")]
    [InlineData("2.5", "2.5e60")]
    public void LuminousIntensityFromQuettacandelasShouldProduceExpectedQuectoCandelas(string value, string expected)
    {
        LuminousIntensity<Float128> l = LuminousIntensity<Float128>.FromQuettacandelas(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), l.QuectoCandelas);
    }

    [Fact(DisplayName = "LuminousIntensity.Add should produce the expected result")]
    public void LuminousIntensityAddShouldProduceExpectedValue()
    {
        // Given
        LuminousIntensity<Float128> left = LuminousIntensity<Float128>.FromCandelas(Float128.Parse("1.5"));
        LuminousIntensity<Float128> right = LuminousIntensity<Float128>.FromCandelas(Float128.Parse("0.5"));

        // When
        LuminousIntensity<Float128> result = left.Add(right);

        // Then
        Assert.Equal(Float128.Parse("2"), result.Candelas);
    }

    [Fact(DisplayName = "LuminousIntensity.Subtract should produce the expected result")]
    public void LuminousIntensitySubtractShouldProduceExpectedValue()
    {
        // Given
        LuminousIntensity<Float128> left = LuminousIntensity<Float128>.FromCandelas(Float128.Parse("1.5"));
        LuminousIntensity<Float128> right = LuminousIntensity<Float128>.FromCandelas(Float128.Parse("0.4"));

        // When
        LuminousIntensity<Float128> result = left.Subtract(right);

        // Then
        Assert.Equal(Float128.Parse("1.1"), result.Candelas);
    }

    [Fact(DisplayName = "LuminousIntensity comparison should produce the expected result (left equal to right)")]
    public void LuminousIntensityComparisonShouldProduceExpectedResultLeftEqualToRight()
    {
        // Given
        LuminousIntensity<Float128> left = LuminousIntensity<Float128>.FromCandelas(Float128.Parse("123"));
        LuminousIntensity<Float128> right = LuminousIntensity<Float128>.FromCandelas(Float128.Parse("123"));

        // When / Then
        Assert.Equal(0, LuminousIntensity<Float128>.Compare(left, right));
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
        LuminousIntensity<Float128> left = LuminousIntensity<Float128>.FromCandelas(Float128.Parse("456"));
        LuminousIntensity<Float128> right = LuminousIntensity<Float128>.FromCandelas(Float128.Parse("123"));

        // When / Then
        Assert.Equal(1, LuminousIntensity<Float128>.Compare(left, right));
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
        LuminousIntensity<Float128> left = LuminousIntensity<Float128>.FromCandelas(Float128.Parse("123"));
        LuminousIntensity<Float128> right = LuminousIntensity<Float128>.FromCandelas(Float128.Parse("456"));

        // When / Then
        Assert.Equal(-1, LuminousIntensity<Float128>.Compare(left, right));
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
        // Given — 2 cd and 2000 mcd are the same canonical intensity; equality should hold at Float128.
        LuminousIntensity<Float128> left = LuminousIntensity<Float128>.FromCandelas(Float128.Parse("2"));
        LuminousIntensity<Float128> right = LuminousIntensity<Float128>.FromMillicandelas(Float128.Parse("2000"));

        // When / Then
        Assert.True(LuminousIntensity<Float128>.Equals(left, right));
        Assert.True(left.Equals(right));
        Assert.True(left.Equals((object)right));
        Assert.True(left == right);
        Assert.False(left != right);
    }

    [Fact(DisplayName = "LuminousIntensity equality should produce the expected result (left not equal to right)")]
    public void LuminousIntensityEqualityShouldProduceExpectedResultLeftNotEqualToRight()
    {
        // Given
        LuminousIntensity<Float128> left = LuminousIntensity<Float128>.FromCandelas(Float128.Parse("2"));
        LuminousIntensity<Float128> right = LuminousIntensity<Float128>.FromMillicandelas(Float128.Parse("2500"));

        // When / Then
        Assert.False(LuminousIntensity<Float128>.Equals(left, right));
        Assert.False(left.Equals(right));
        Assert.False(left.Equals((object)right));
        Assert.False(left == right);
        Assert.True(left != right);
    }

    [Fact(DisplayName = "LuminousIntensity.ToString should produce the expected result")]
    public void LuminousIntensityToStringShouldProduceExpectedResult()
    {
        // Given
        LuminousIntensity<Float128> l = LuminousIntensity<Float128>.FromCandelas(Float128.Parse("1000"));

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
        LuminousIntensity<Float128> l = LuminousIntensity<Float128>.FromCandelas(Float128.Parse("1"));

        // Then
        Assert.Equal("0.000001 Mcd", $"{l:Mcd6}"); // mega
        Assert.Equal("1,000.000 mcd", $"{l:mcd3}"); // milli
    }

    [Fact(DisplayName = "LuminousIntensity.ToString Pcd vs pcd are case-sensitive")]
    public void LuminousIntensityToStringPcdVsPcdAreCaseSensitive()
    {
        // Given
        LuminousIntensity<Float128> l = LuminousIntensity<Float128>.FromCandelas(Float128.Parse("1"));

        // Then
        Assert.Equal("0.000000000000001 Pcd", $"{l:Pcd15}"); // peta
        Assert.Equal("1,000,000,000,000.000 pcd", $"{l:pcd3}"); // pico
    }

    [Fact(DisplayName = "LuminousIntensity.ToString µcd symbol should differ from format specifier")]
    public void LuminousIntensityToStringMicrocandelasSymbolShouldDifferFromFormatSpecifier()
    {
        // Given
        LuminousIntensity<Float128> l = LuminousIntensity<Float128>.FromCandelas(Float128.Parse("1"));

        // Then: specifier is ucd, but symbol rendered is µcd
        Assert.Equal("1,000,000.000 µcd", $"{l:ucd3}");
    }

    [Fact(DisplayName = "LuminousIntensity.ToString should honor custom culture separators")]
    public void LuminousIntensityToStringShouldHonorCustomCulture()
    {
        // Given
        CultureInfo customCulture = new("de-DE");
        LuminousIntensity<Float128> l = LuminousIntensity<Float128>.FromCandelas(Float128.Parse("1234.56"));

        // When
        string formatted = l.ToString("cd2", customCulture);

        // Then
        Assert.Equal("1.234,56 cd", formatted);
    }
}
