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

public sealed class MassTests
{
    // IEEE-754 binary floating-point arithmetic causes small discrepancies in calculation, therefore we need a tolerance.
    private const double Tolerance = 1e+42;

    [Fact(DisplayName = "Mass.Zero should produce the expected result")]
    public void MassZeroShouldProduceExpectedResult()
    {
        // Given / When
        Mass<double> mass = Mass<double>.Zero;

        // Then
        Assert.Equal(0.0, mass.YoctoGrams, Tolerance);
    }

    [Theory(DisplayName = "Mass.FromYoctograms should produce the expected YoctoGrams")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1.0)]
    [InlineData(2.5, 2.5)]
    public void MassFromYoctogramsShouldProduceExpectedYoctoGrams(double value, double expectedYoctoGrams)
    {
        // Given / When
        Mass<double> mass = Mass<double>.FromYoctograms(value);

        // Then
        Assert.Equal(expectedYoctoGrams, mass.YoctoGrams, Tolerance);
    }

    [Theory(DisplayName = "Mass.FromZeptograms should produce the expected YoctoGrams")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e3)]
    [InlineData(2.5, 2.5e3)]
    public void MassFromZeptogramsShouldProduceExpectedYoctoGrams(double value, double expectedYoctoGrams)
    {
        Mass<double> mass = Mass<double>.FromZeptograms(value);
        Assert.Equal(expectedYoctoGrams, mass.YoctoGrams, Tolerance);
    }

    [Theory(DisplayName = "Mass.FromAttograms should produce the expected YoctoGrams")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e6)]
    [InlineData(2.5, 2.5e6)]
    public void MassFromAttogramsShouldProduceExpectedYoctoGrams(double value, double expectedYoctoGrams)
    {
        // Given / When
        Mass<double> mass = Mass<double>.FromAttograms(value);

        // Then
        Assert.Equal(expectedYoctoGrams, mass.YoctoGrams, Tolerance);
    }

    [Theory(DisplayName = "Mass.FromFemtograms should produce the expected YoctoGrams")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e9)]
    [InlineData(2.5, 2.5e9)]
    public void MassFromFemtogramsShouldProduceExpectedYoctoGrams(double value, double expectedYoctoGrams)
    {
        // Given / When
        Mass<double> mass = Mass<double>.FromFemtograms(value);

        // Then
        Assert.Equal(expectedYoctoGrams, mass.YoctoGrams, Tolerance);
    }

    [Theory(DisplayName = "Mass.FromPicograms should produce the expected YoctoGrams")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e12)]
    [InlineData(2.5, 2.5e12)]
    public void MassFromPicogramsShouldProduceExpectedYoctoGrams(double value, double expectedYoctoGrams)
    {
        // Given / When
        Mass<double> mass = Mass<double>.FromPicograms(value);

        // Then
        Assert.Equal(expectedYoctoGrams, mass.YoctoGrams, Tolerance);
    }

    [Theory(DisplayName = "Mass.FromNanograms should produce the expected YoctoGrams")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e15)]
    [InlineData(2.5, 2.5e15)]
    public void MassFromNanogramsShouldProduceExpectedYoctoGrams(double value, double expectedYoctoGrams)
    {
        // Given / When
        Mass<double> mass = Mass<double>.FromNanograms(value);

        // Then
        Assert.Equal(expectedYoctoGrams, mass.YoctoGrams, Tolerance);
    }

    [Theory(DisplayName = "Mass.FromMicrograms should produce the expected YoctoGrams")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e18)]
    [InlineData(2.5, 2.5e18)]
    public void MassFromMicrogramsShouldProduceExpectedYoctoGrams(double value, double expectedYoctoGrams)
    {
        // Given / When
        Mass<double> mass = Mass<double>.FromMicrograms(value);

        // Then
        Assert.Equal(expectedYoctoGrams, mass.YoctoGrams, Tolerance);
    }

    [Theory(DisplayName = "Mass.FromMilligrams should produce the expected YoctoGrams")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e21)]
    [InlineData(2.5, 2.5e21)]
    public void MassFromMilligramsShouldProduceExpectedYoctoGrams(double value, double expectedYoctoGrams)
    {
        // Given / When
        Mass<double> mass = Mass<double>.FromMilligrams(value);

        // Then
        Assert.Equal(expectedYoctoGrams, mass.YoctoGrams, Tolerance);
    }

    [Theory(DisplayName = "Mass.FromGrams should produce the expected YoctoGrams")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e24)]
    [InlineData(2.5, 2.5e24)]
    public void MassFromGramsShouldProduceExpectedYoctoGrams(double value, double expectedYoctoGrams)
    {
        // Given / When
        Mass<double> mass = Mass<double>.FromGrams(value);

        // Then
        Assert.Equal(expectedYoctoGrams, mass.YoctoGrams, Tolerance);
    }

    [Theory(DisplayName = "Mass.FromKilograms should produce the expected YoctoGrams")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e27)]
    [InlineData(2.5, 2.5e27)]
    public void MassFromKilogramsShouldProduceExpectedYoctoGrams(double value, double expectedYoctoGrams)
    {
        // Given / When
        Mass<double> mass = Mass<double>.FromKilograms(value);

        // Then
        Assert.Equal(expectedYoctoGrams, mass.YoctoGrams, Tolerance);
    }

    [Theory(DisplayName = "Mass.FromMegagrams should produce the expected YoctoGrams")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e30)]
    [InlineData(2.5, 2.5e30)]
    public void MassFromMegagramsShouldProduceExpectedYoctoGrams(double value, double expectedYoctoGrams)
    {
        // Given / When
        Mass<double> mass = Mass<double>.FromMegagrams(value);

        // Then
        Assert.Equal(expectedYoctoGrams, mass.YoctoGrams, Tolerance);
    }

    [Theory(DisplayName = "Mass.FromTonnes should be equivalent to FromMegagrams")]
    [InlineData(0.0)]
    [InlineData(1.0)]
    [InlineData(2.5)]
    public void MassFromTonnesShouldBeEquivalentToFromMegagrams(double value)
    {
        // Given / When
        Mass<double> megagrams = Mass<double>.FromMegagrams(value);
        Mass<double> tonnes = Mass<double>.FromTonnes(value);

        // Then
        Assert.Equal(megagrams.YoctoGrams, tonnes.YoctoGrams, Tolerance);
    }

    [Theory(DisplayName = "Mass.FromGigagrams should produce the expected YoctoGrams")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e33)]
    [InlineData(2.5, 2.5e33)]
    public void MassFromGigagramsShouldProduceExpectedYoctoGrams(double value, double expectedYoctoGrams)
    {
        // Given / When
        Mass<double> mass = Mass<double>.FromGigagrams(value);

        // Then
        Assert.Equal(expectedYoctoGrams, mass.YoctoGrams, Tolerance);
    }

    [Theory(DisplayName = "Mass.FromTeragrams should produce the expected YoctoGrams")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e36)]
    [InlineData(2.5, 2.5e36)]
    public void MassFromTeragramsShouldProduceExpectedYoctoGrams(double value, double expectedYoctoGrams)
    {
        // Given / When
        Mass<double> mass = Mass<double>.FromTeragrams(value);

        // Then
        Assert.Equal(expectedYoctoGrams, mass.YoctoGrams, Tolerance);
    }

    [Theory(DisplayName = "Mass.FromPetagrams should produce the expected YoctoGrams")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e39)]
    [InlineData(2.5, 2.5e39)]
    public void MassFromPetagramsShouldProduceExpectedYoctoGrams(double value, double expectedYoctoGrams)
    {
        // Given / When
        Mass<double> mass = Mass<double>.FromPetagrams(value);

        // Then
        Assert.Equal(expectedYoctoGrams, mass.YoctoGrams, Tolerance);
    }

    [Theory(DisplayName = "Mass.FromExagrams should produce the expected YoctoGrams")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e42)]
    [InlineData(2.5, 2.5e42)]
    public void MassFromExagramsShouldProduceExpectedYoctoGrams(double value, double expectedYoctoGrams)
    {
        // Given / When
        Mass<double> mass = Mass<double>.FromExagrams(value);

        // Then
        Assert.Equal(expectedYoctoGrams, mass.YoctoGrams, Tolerance);
    }

    [Theory(DisplayName = "Mass.FromZettagrams should produce the expected YoctoGrams")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e45)]
    [InlineData(2.5, 2.5e45)]
    public void MassFromZettagramsShouldProduceExpectedYoctoGrams(double value, double expectedYoctoGrams)
    {
        // Given / When
        Mass<double> mass = Mass<double>.FromZettagrams(value);

        // Then
        Assert.Equal(expectedYoctoGrams, mass.YoctoGrams, Tolerance);
    }

    [Theory(DisplayName = "Mass.FromYottagrams should produce the expected YoctoGrams")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e48)]
    [InlineData(2.5, 2.5e48)]
    public void MassFromYottagramsShouldProduceExpectedYoctoGrams(double value, double expectedYoctoGrams)
    {
        // Given / When
        Mass<double> mass = Mass<double>.FromYottagrams(value);

        // Then
        Assert.Equal(expectedYoctoGrams, mass.YoctoGrams, Tolerance);
    }

    [Theory(DisplayName = "Mass.FromPounds should produce the expected YoctoGrams")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 4.5359237e26)]
    [InlineData(2.5, 1.133980925e27)]
    public void MassFromPoundsShouldProduceExpectedYoctoGrams(double value, double expectedYoctoGrams)
    {
        // Given / When
        Mass<double> mass = Mass<double>.FromPounds(value);

        // Then
        Assert.Equal(expectedYoctoGrams, mass.YoctoGrams, Tolerance);
    }

    [Theory(DisplayName = "Mass.FromOunces should produce the expected YoctoGrams")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 2.8349523125e25)]
    [InlineData(2.5, 7.08738078125e25)]
    public void MassFromOuncesShouldProduceExpectedYoctoGrams(double value, double expectedYoctoGrams)
    {
        // Given / When
        Mass<double> mass = Mass<double>.FromOunces(value);

        // Then
        Assert.Equal(expectedYoctoGrams, mass.YoctoGrams, Tolerance);
    }

    [Theory(DisplayName = "Mass.FromStones should produce the expected YoctoGrams")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 6.35029318e27)]
    [InlineData(2.5, 1.587573295e28)]
    public void MassFromStonesShouldProduceExpectedYoctoGrams(double value, double expectedYoctoGrams)
    {
        // Given / When
        Mass<double> mass = Mass<double>.FromStones(value);

        // Then
        Assert.Equal(expectedYoctoGrams, mass.YoctoGrams, Tolerance);
    }

    [Theory(DisplayName = "Mass.FromGrains should produce the expected YoctoGrams")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 6.479891e22)]
    [InlineData(2.5, 1.61997275e23)]
    public void MassFromGrainsShouldProduceExpectedYoctoGrams(double value, double expectedYoctoGrams)
    {
        // Given / When
        Mass<double> mass = Mass<double>.FromGrains(value);

        // Then
        Assert.Equal(expectedYoctoGrams, mass.YoctoGrams, Tolerance);
    }

    [Theory(DisplayName = "Mass.FromShortTons should produce the expected YoctoGrams")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 9.0718474e29)]
    [InlineData(2.5, 2.26796185e30)]
    public void MassFromShortTonsShouldProduceExpectedYoctoGrams(double value, double expectedYoctoGrams)
    {
        // Given / When
        Mass<double> mass = Mass<double>.FromShortTons(value);

        // Then
        Assert.Equal(expectedYoctoGrams, mass.YoctoGrams, Tolerance);
    }

    [Theory(DisplayName = "Mass.FromLongTons should produce the expected YoctoGrams")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1.0160469088e30)]
    [InlineData(2.5, 2.540117272e30)]
    public void MassFromLongTonsShouldProduceExpectedYoctoGrams(double value, double expectedYoctoGrams)
    {
        // Given / When
        Mass<double> mass = Mass<double>.FromLongTons(value);

        // Then
        Assert.Equal(expectedYoctoGrams, mass.YoctoGrams, Tolerance);
    }

    [Theory(DisplayName = "Mass.FromHundredweightUs should produce the expected YoctoGrams")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 4.5359237e28)]
    [InlineData(2.5, 1.133980925e29)]
    public void MassFromHundredweightUsShouldProduceExpectedYoctoGrams(double value, double expectedYoctoGrams)
    {
        // Given / When
        Mass<double> mass = Mass<double>.FromHundredweightUs(value);

        // Then
        Assert.Equal(expectedYoctoGrams, mass.YoctoGrams, Tolerance);
    }

    [Theory(DisplayName = "Mass.FromHundredweightUk should produce the expected YoctoGrams")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 5.080234544e28)]
    [InlineData(2.5, 1.270058636e29)]
    public void MassFromHundredweightUkShouldProduceExpectedYoctoGrams(double value, double expectedYoctoGrams)
    {
        // Given / When
        Mass<double> mass = Mass<double>.FromHundredweightUk(value);

        // Then
        Assert.Equal(expectedYoctoGrams, mass.YoctoGrams, Tolerance);
    }

    [Theory(DisplayName = "Mass.FromQuarters should produce the expected YoctoGrams")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1.270058636e28)]
    [InlineData(2.5, 3.17514659e28)]
    public void MassFromQuartersShouldProduceExpectedYoctoGrams(double value, double expectedYoctoGrams)
    {
        // Given / When
        Mass<double> mass = Mass<double>.FromQuarters(value);

        // Then
        Assert.Equal(expectedYoctoGrams, mass.YoctoGrams, Tolerance);
    }

    [Theory(DisplayName = "Mass.FromTroyPounds should produce the expected YoctoGrams")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 3.732417216e26)]
    [InlineData(2.5, 9.33104304e26)]
    public void MassFromTroyPoundsShouldProduceExpectedYoctoGrams(double value, double expectedYoctoGrams)
    {
        // Given / When
        Mass<double> mass = Mass<double>.FromTroyPounds(value);

        // Then
        Assert.Equal(expectedYoctoGrams, mass.YoctoGrams, Tolerance);
    }

    [Theory(DisplayName = "Mass.FromTroyOunces should produce the expected YoctoGrams")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 3.11034768e25)]
    [InlineData(2.5, 7.7758692e25)]
    public void MassFromTroyOuncesShouldProduceExpectedYoctoGrams(double value, double expectedYoctoGrams)
    {
        // Given / When
        Mass<double> mass = Mass<double>.FromTroyOunces(value);

        // Then
        Assert.Equal(expectedYoctoGrams, mass.YoctoGrams, Tolerance);
    }

    [Theory(DisplayName = "Mass.FromPennyweights should produce the expected YoctoGrams")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1.55517384e24)]
    [InlineData(2.5, 3.8879346e24)]
    public void MassFromPennyweightsShouldProduceExpectedYoctoGrams(double value, double expectedYoctoGrams)
    {
        // Given / When
        Mass<double> mass = Mass<double>.FromPennyweights(value);

        // Then
        Assert.Equal(expectedYoctoGrams, mass.YoctoGrams, Tolerance);
    }

    [Fact(DisplayName = "Mass.Add should produce the expected result")]
    public void MassAddShouldProduceExpectedValue()
    {
        // Given
        Mass<double> left = Mass<double>.FromKilograms(1500.0);
        Mass<double> right = Mass<double>.FromKilograms(500.0);

        // When
        Mass<double> result = left.Add(right);

        // Then
        Assert.Equal(2000.0, result.KiloGrams, Tolerance);
    }

    [Fact(DisplayName = "Mass.Subtract should produce the expected result")]
    public void MassSubtractShouldProduceExpectedValue()
    {
        // Given
        Mass<double> left = Mass<double>.FromKilograms(1500.0);
        Mass<double> right = Mass<double>.FromKilograms(400.0);

        // When
        Mass<double> result = left.Subtract(right);

        // Then
        Assert.Equal(1100.0, result.KiloGrams, Tolerance);
    }

    [Fact(DisplayName = "Mass.Multiply should produce the expected result")]
    public void MassMultiplyShouldProduceExpectedValue()
    {
        // Given
        Mass<double> left = Mass<double>.FromGrams(10.0); // 1e25 yg
        Mass<double> right = Mass<double>.FromGrams(3.0); // 3e24 yg

        // When
        Mass<double> result = left.Multiply(right); // 1e25 * 3e24 = 3e49 yg

        // Then
        Assert.Equal(1e25, left.YoctoGrams, Tolerance);
        Assert.Equal(3e24, right.YoctoGrams, Tolerance);
        Assert.Equal(3e49, result.YoctoGrams, Tolerance);
        Assert.Equal(3e25, result.Grams, Tolerance);
    }

    [Fact(DisplayName = "Mass.Divide should produce the expected result")]
    public void MassDivideShouldProduceExpectedValue()
    {
        // Given
        Mass<double> left = Mass<double>.FromGrams(100.0); // 1e26 yg
        Mass<double> right = Mass<double>.FromGrams(20.0); // 2e25 yg

        // When
        Mass<double> result = left.Divide(right); // 1e26 / 2e25 = 5 yg

        // Then
        Assert.Equal(5.0, result.YoctoGrams, Tolerance);
        Assert.Equal(5e-24, result.Grams, Tolerance);
    }

    [Fact(DisplayName = "Mass comparison should produce the expected result (left equal to right)")]
    public void MassComparisonShouldProduceExpectedResultLeftEqualToRight()
    {
        // Given
        Mass<double> left = Mass<double>.FromKilograms(1234.0);
        Mass<double> right = Mass<double>.FromKilograms(1234.0);

        // When / Then
        Assert.Equal(0, Mass<double>.Compare(left, right));
        Assert.Equal(0, left.CompareTo(right));
        Assert.Equal(0, left.CompareTo((object)right));
        Assert.False(left > right);
        Assert.True(left >= right);
        Assert.False(left < right);
        Assert.True(left <= right);
    }

    [Fact(DisplayName = "Mass comparison should produce the expected result (left greater than right)")]
    public void MassComparisonShouldProduceExpectedLeftGreaterThanRight()
    {
        // Given
        Mass<double> left = Mass<double>.FromKilograms(4567.0);
        Mass<double> right = Mass<double>.FromKilograms(1234.0);

        // When / Then
        Assert.Equal(1, Mass<double>.Compare(left, right));
        Assert.Equal(1, left.CompareTo(right));
        Assert.Equal(1, left.CompareTo((object)right));
        Assert.True(left > right);
        Assert.True(left >= right);
        Assert.False(left < right);
        Assert.False(left <= right);
    }

    [Fact(DisplayName = "Mass comparison should produce the expected result (left less than right)")]
    public void MassComparisonShouldProduceExpectedLeftLessThanRight()
    {
        // Given
        Mass<double> left = Mass<double>.FromKilograms(1234.0);
        Mass<double> right = Mass<double>.FromKilograms(4567.0);

        // When / Then
        Assert.Equal(-1, Mass<double>.Compare(left, right));
        Assert.Equal(-1, left.CompareTo(right));
        Assert.Equal(-1, left.CompareTo((object)right));
        Assert.False(left > right);
        Assert.False(left >= right);
        Assert.True(left < right);
        Assert.True(left <= right);
    }

    [Fact(DisplayName = "Mass equality should produce the expected result (left equal to right)")]
    public void MassEqualityShouldProduceExpectedResultLeftEqualToRight()
    {
        // Given
        Mass<BigDecimal> left = Mass<BigDecimal>.FromKilograms(2.0);
        Mass<BigDecimal> right = Mass<BigDecimal>.FromGrams(2000.0);

        // When / Then
        Assert.True(Mass<BigDecimal>.Equals(left, right));
        Assert.True(left.Equals(right));
        Assert.True(left.Equals((object)right));
        Assert.True(left == right);
        Assert.False(left != right);
    }

    [Fact(DisplayName = "Mass equality should produce the expected result (left not equal to right)")]
    public void MassEqualityShouldProduceExpectedResultLeftNotEqualToRight()
    {
        // Given
        Mass<double> left = Mass<double>.FromKilograms(2.0);
        Mass<double> right = Mass<double>.FromGrams(2500.0);

        // When / Then
        Assert.False(Mass<double>.Equals(left, right));
        Assert.False(left.Equals(right));
        Assert.False(left.Equals((object)right));
        Assert.False(left == right);
        Assert.True(left != right);
    }

    [Fact(DisplayName = "Mass.ToString should produce the expected result")]
    public void MassToStringShouldProduceExpectedResult()
    {
        // Given
        Mass<double> mass = Mass<double>.FromGrams(1000.0); // 1 kg

        // When / Then
        Assert.Equal("1,000.000 g", $"{mass:g3}");
        Assert.Equal("1.000 kg", $"{mass:kg3}");
        Assert.Equal("1,000,000.000 mg", $"{mass:mg3}");
        Assert.Equal("0.001 Mg", $"{mass:Mg3}");
        Assert.Equal("0.001 t", $"{mass:t3}");
        Assert.Equal("2.205 lb", $"{mass:lb3}");
        Assert.Equal("35.274 oz", $"{mass:oz3}");
    }

    [Fact(DisplayName = "Mass.ToString should honor custom culture separators")]
    public void MassToStringShouldHonorCustomCulture()
    {
        // Given
        CultureInfo customCulture = new("de-DE");
        Mass<double> mass = Mass<double>.FromGrams(1234.56);

        // When
        string formatted = mass.ToString("g2", customCulture);

        // Then
        // German uses '.' for thousands and ',' for decimals.
        Assert.Equal("1.234,56 g", formatted);
    }
}
