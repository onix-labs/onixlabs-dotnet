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
        Assert.Equal(0.0, mass.QuectoGrams, Tolerance);
    }

    [Theory(DisplayName = "Mass.FromQuectograms should produce the expected QuectoGrams")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1.0)]
    [InlineData(2.5, 2.5)]
    public void MassFromQuectogramsShouldProduceExpectedQuectoGrams(double value, double expected)
    {
        Mass<double> m = Mass<double>.FromQuectograms(value);
        Assert.Equal(expected, m.QuectoGrams, Tolerance);
    }

    [Theory(DisplayName = "Mass.FromRontograms should produce the expected QuectoGrams")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e3)]
    [InlineData(2.5, 2.5e3)]
    public void MassFromRontogramsShouldProduceExpectedQuectoGrams(double value, double expected)
    {
        Mass<double> m = Mass<double>.FromRontograms(value);
        Assert.Equal(expected, m.QuectoGrams, Tolerance);
    }

    [Theory(DisplayName = "Mass.FromYoctograms should produce the expected QuectoGrams")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e6)]
    [InlineData(2.5, 2.5e6)]
    public void MassFromYoctogramsShouldProduceExpectedQuectoGrams(double value, double expected)
    {
        Mass<double> m = Mass<double>.FromYoctograms(value);
        Assert.Equal(expected, m.QuectoGrams, Tolerance);
    }

    [Theory(DisplayName = "Mass.FromZeptograms should produce the expected QuectoGrams")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e9)]
    [InlineData(2.5, 2.5e9)]
    public void MassFromZeptogramsShouldProduceExpectedQuectoGrams(double value, double expected)
    {
        Mass<double> m = Mass<double>.FromZeptograms(value);
        Assert.Equal(expected, m.QuectoGrams, Tolerance);
    }

    [Theory(DisplayName = "Mass.FromAttograms should produce the expected QuectoGrams")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e12)]
    [InlineData(2.5, 2.5e12)]
    public void MassFromAttogramsShouldProduceExpectedQuectoGrams(double value, double expected)
    {
        Mass<double> m = Mass<double>.FromAttograms(value);
        Assert.Equal(expected, m.QuectoGrams, Tolerance);
    }

    [Theory(DisplayName = "Mass.FromFemtograms should produce the expected QuectoGrams")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e15)]
    [InlineData(2.5, 2.5e15)]
    public void MassFromFemtogramsShouldProduceExpectedQuectoGrams(double value, double expected)
    {
        Mass<double> m = Mass<double>.FromFemtograms(value);
        Assert.Equal(expected, m.QuectoGrams, Tolerance);
    }

    [Theory(DisplayName = "Mass.FromPicograms should produce the expected QuectoGrams")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e18)]
    [InlineData(2.5, 2.5e18)]
    public void MassFromPicogramsShouldProduceExpectedQuectoGrams(double value, double expected)
    {
        Mass<double> m = Mass<double>.FromPicograms(value);
        Assert.Equal(expected, m.QuectoGrams, Tolerance);
    }

    [Theory(DisplayName = "Mass.FromNanograms should produce the expected QuectoGrams")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e21)]
    [InlineData(2.5, 2.5e21)]
    public void MassFromNanogramsShouldProduceExpectedQuectoGrams(double value, double expected)
    {
        Mass<double> m = Mass<double>.FromNanograms(value);
        Assert.Equal(expected, m.QuectoGrams, Tolerance);
    }

    [Theory(DisplayName = "Mass.FromMicrograms should produce the expected QuectoGrams")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e24)]
    [InlineData(2.5, 2.5e24)]
    public void MassFromMicrogramsShouldProduceExpectedQuectoGrams(double value, double expected)
    {
        Mass<double> m = Mass<double>.FromMicrograms(value);
        Assert.Equal(expected, m.QuectoGrams, Tolerance);
    }

    [Theory(DisplayName = "Mass.FromMilligrams should produce the expected QuectoGrams")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e27)]
    [InlineData(2.5, 2.5e27)]
    public void MassFromMilligramsShouldProduceExpectedQuectoGrams(double value, double expected)
    {
        Mass<double> m = Mass<double>.FromMilligrams(value);
        Assert.Equal(expected, m.QuectoGrams, Tolerance);
    }

    [Theory(DisplayName = "Mass.FromCentigrams should produce the expected QuectoGrams")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e28)]
    [InlineData(2.5, 2.5e28)]
    public void MassFromCentigramsShouldProduceExpectedQuectoGrams(double value, double expected)
    {
        Mass<double> m = Mass<double>.FromCentigrams(value);
        Assert.Equal(expected, m.QuectoGrams, Tolerance);
    }

    [Theory(DisplayName = "Mass.FromDecigrams should produce the expected QuectoGrams")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e29)]
    [InlineData(2.5, 2.5e29)]
    public void MassFromDecigramsShouldProduceExpectedQuectoGrams(double value, double expected)
    {
        Mass<double> m = Mass<double>.FromDecigrams(value);
        Assert.Equal(expected, m.QuectoGrams, Tolerance);
    }

    [Theory(DisplayName = "Mass.FromGrams should produce the expected QuectoGrams")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e30)]
    [InlineData(2.5, 2.5e30)]
    public void MassFromGramsShouldProduceExpectedQuectoGrams(double value, double expected)
    {
        Mass<double> m = Mass<double>.FromGrams(value);
        Assert.Equal(expected, m.QuectoGrams, Tolerance);
    }

    [Theory(DisplayName = "Mass.FromDecagrams should produce the expected QuectoGrams")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e31)]
    [InlineData(2.5, 2.5e31)]
    public void MassFromDecagramsShouldProduceExpectedQuectoGrams(double value, double expected)
    {
        Mass<double> m = Mass<double>.FromDecagrams(value);
        Assert.Equal(expected, m.QuectoGrams, Tolerance);
    }

    [Theory(DisplayName = "Mass.FromHectograms should produce the expected QuectoGrams")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e32)]
    [InlineData(2.5, 2.5e32)]
    public void MassFromHectogramsShouldProduceExpectedQuectoGrams(double value, double expected)
    {
        Mass<double> m = Mass<double>.FromHectograms(value);
        Assert.Equal(expected, m.QuectoGrams, Tolerance);
    }

    [Theory(DisplayName = "Mass.FromKilograms should produce the expected QuectoGrams")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e33)]
    [InlineData(2.5, 2.5e33)]
    public void MassFromKilogramsShouldProduceExpectedQuectoGrams(double value, double expected)
    {
        Mass<double> m = Mass<double>.FromKilograms(value);
        Assert.Equal(expected, m.QuectoGrams, Tolerance);
    }

    [Theory(DisplayName = "Mass.FromMegagrams should produce the expected QuectoGrams")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e36)]
    [InlineData(2.5, 2.5e36)]
    public void MassFromMegagramsShouldProduceExpectedQuectoGrams(double value, double expected)
    {
        Mass<double> m = Mass<double>.FromMegagrams(value);
        Assert.Equal(expected, m.QuectoGrams, Tolerance);
    }

    [Theory(DisplayName = "Mass.FromGigagrams should produce the expected QuectoGrams")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e39)]
    [InlineData(2.5, 2.5e39)]
    public void MassFromGigagramsShouldProduceExpectedQuectoGrams(double value, double expected)
    {
        Mass<double> m = Mass<double>.FromGigagrams(value);
        Assert.Equal(expected, m.QuectoGrams, Tolerance);
    }

    [Theory(DisplayName = "Mass.FromTeragrams should produce the expected QuectoGrams")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e42)]
    [InlineData(2.5, 2.5e42)]
    public void MassFromTeragramsShouldProduceExpectedQuectoGrams(double value, double expected)
    {
        Mass<double> m = Mass<double>.FromTeragrams(value);
        Assert.Equal(expected, m.QuectoGrams, Tolerance);
    }

    [Theory(DisplayName = "Mass.FromPetagrams should produce the expected QuectoGrams")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e45)]
    [InlineData(2.5, 2.5e45)]
    public void MassFromPetagramsShouldProduceExpectedQuectoGrams(double value, double expected)
    {
        Mass<double> m = Mass<double>.FromPetagrams(value);
        Assert.Equal(expected, m.QuectoGrams, Tolerance);
    }

    [Theory(DisplayName = "Mass.FromExagrams should produce the expected QuectoGrams")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e48)]
    [InlineData(2.5, 2.5e48)]
    public void MassFromExagramsShouldProduceExpectedQuectoGrams(double value, double expected)
    {
        Mass<double> m = Mass<double>.FromExagrams(value);
        Assert.Equal(expected, m.QuectoGrams, Tolerance);
    }

    [Theory(DisplayName = "Mass.FromZettagrams should produce the expected QuectoGrams")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e51)]
    [InlineData(2.5, 2.5e51)]
    public void MassFromZettagramsShouldProduceExpectedQuectoGrams(double value, double expected)
    {
        Mass<double> m = Mass<double>.FromZettagrams(value);
        Assert.Equal(expected, m.QuectoGrams, Tolerance);
    }

    [Theory(DisplayName = "Mass.FromYottagrams should produce the expected QuectoGrams")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e54)]
    [InlineData(2.5, 2.5e54)]
    public void MassFromYottagramsShouldProduceExpectedQuectoGrams(double value, double expected)
    {
        Mass<double> m = Mass<double>.FromYottagrams(value);
        Assert.Equal(expected, m.QuectoGrams, Tolerance);
    }

    [Theory(DisplayName = "Mass.FromRonnagrams should produce the expected QuectoGrams")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e57)]
    [InlineData(2.5, 2.5e57)]
    public void MassFromRonnagramsShouldProduceExpectedQuectoGrams(double value, double expected)
    {
        Mass<double> m = Mass<double>.FromRonnagrams(value);
        Assert.Equal(expected, m.QuectoGrams, Tolerance);
    }

    [Theory(DisplayName = "Mass.FromQuettagrams should produce the expected QuectoGrams")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e60)]
    [InlineData(2.5, 2.5e60)]
    public void MassFromQuettagramsShouldProduceExpectedQuectoGrams(double value, double expected)
    {
        Mass<double> m = Mass<double>.FromQuettagrams(value);
        Assert.Equal(expected, m.QuectoGrams, Tolerance);
    }

    [Theory(DisplayName = "Mass.FromTonnes should produce the expected QuectoGrams")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e36)]
    [InlineData(2.5, 2.5e36)]
    public void MassFromTonnesShouldProduceExpectedQuectoGrams(double value, double expected)
    {
        Mass<double> m = Mass<double>.FromTonnes(value);
        Assert.Equal(expected, m.QuectoGrams, Tolerance);
    }

    [Theory(DisplayName = "Mass.FromOunces should produce the expected QuectoGrams")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 28.349523125e30)]
    [InlineData(16.0, 453.59237e30)] // 16 oz = 1 lb
    public void MassFromOuncesShouldProduceExpectedQuectoGrams(double value, double expected)
    {
        Mass<double> m = Mass<double>.FromOunces(value);
        Assert.Equal(expected, m.QuectoGrams, Tolerance);
    }

    [Theory(DisplayName = "Mass.FromPounds should produce the expected QuectoGrams")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 453.59237e30)]
    [InlineData(2.5, 1133.980925e30)]
    public void MassFromPoundsShouldProduceExpectedQuectoGrams(double value, double expected)
    {
        Mass<double> m = Mass<double>.FromPounds(value);
        Assert.Equal(expected, m.QuectoGrams, Tolerance);
    }

    [Theory(DisplayName = "Mass.FromStones should produce the expected QuectoGrams")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 6350.29318e30)]
    [InlineData(2.0, 12700.58636e30)]
    public void MassFromStonesShouldProduceExpectedQuectoGrams(double value, double expected)
    {
        Mass<double> m = Mass<double>.FromStones(value);
        Assert.Equal(expected, m.QuectoGrams, Tolerance);
    }

    [Theory(DisplayName = "Mass.FromShortTons should produce the expected QuectoGrams")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 907184.74e30)]
    [InlineData(2.5, 2267961.85e30)]
    public void MassFromShortTonsShouldProduceExpectedQuectoGrams(double value, double expected)
    {
        Mass<double> m = Mass<double>.FromShortTons(value);
        Assert.Equal(expected, m.QuectoGrams, Tolerance);
    }

    [Theory(DisplayName = "Mass.FromLongTons should produce the expected QuectoGrams")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1016046.9088e30)]
    [InlineData(2.0, 2032093.8176e30)]
    public void MassFromLongTonsShouldProduceExpectedQuectoGrams(double value, double expected)
    {
        Mass<double> m = Mass<double>.FromLongTons(value);
        Assert.Equal(expected, m.QuectoGrams, Tolerance);
    }

    [Theory(DisplayName = "Mass.FromCarats should produce the expected QuectoGrams")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 0.2e30)]
    [InlineData(5.0, 1.0e30)] // 5 ct = 1 g
    public void MassFromCaratsShouldProduceExpectedQuectoGrams(double value, double expected)
    {
        Mass<double> m = Mass<double>.FromCarats(value);
        Assert.Equal(expected, m.QuectoGrams, Tolerance);
    }

    [Theory(DisplayName = "Mass.FromGrains should produce the expected QuectoGrams")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 0.06479891e30)]
    [InlineData(7000.0, 453.59237e30)] // 7000 gr = 1 lb
    public void MassFromGrainsShouldProduceExpectedQuectoGrams(double value, double expected)
    {
        Mass<double> m = Mass<double>.FromGrains(value);
        Assert.Equal(expected, m.QuectoGrams, Tolerance);
    }

    [Theory(DisplayName = "Mass.FromDrams should produce the expected QuectoGrams")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1.7718451953125e30)]
    [InlineData(16.0, 28.349523125e30)] // 16 dr = 1 oz
    public void MassFromDramsShouldProduceExpectedQuectoGrams(double value, double expected)
    {
        Mass<double> m = Mass<double>.FromDrams(value);
        Assert.Equal(expected, m.QuectoGrams, Tolerance);
    }

    [Theory(DisplayName = "Mass.FromSlugs should produce the expected QuectoGrams")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 14593.90293720636e30)]
    [InlineData(2.0, 29187.80587441272e30)]
    public void MassFromSlugsShouldProduceExpectedQuectoGrams(double value, double expected)
    {
        Mass<double> m = Mass<double>.FromSlugs(value);
        Assert.Equal(expected, m.QuectoGrams, Tolerance);
    }

    [Theory(DisplayName = "Mass.FromDaltons should produce the expected QuectoGrams")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1.66053906660e6)]
    [InlineData(2.5, 4.15134766650e6)]
    public void MassFromDaltonsShouldProduceExpectedQuectoGrams(double value, double expected)
    {
        Mass<double> m = Mass<double>.FromDaltons(value);
        Assert.Equal(expected, m.QuectoGrams, Tolerance);
    }

    [Fact(DisplayName = "Mass.Add should produce the expected result")]
    public void MassAddShouldProduceExpectedValue()
    {
        // Given
        Mass<double> left = Mass<double>.FromKilograms(1.5);
        Mass<double> right = Mass<double>.FromKilograms(0.5);

        // When
        Mass<double> result = left.Add(right);

        // Then
        Assert.Equal(2.0, result.KiloGrams, Tolerance);
    }

    [Fact(DisplayName = "Mass.Subtract should produce the expected result")]
    public void MassSubtractShouldProduceExpectedValue()
    {
        // Given
        Mass<double> left = Mass<double>.FromKilograms(1.5);
        Mass<double> right = Mass<double>.FromKilograms(0.4);

        // When
        Mass<double> result = left.Subtract(right);

        // Then
        Assert.Equal(1.1, result.KiloGrams, Tolerance);
    }

    [Fact(DisplayName = "Mass.Multiply should produce the expected result")]
    public void MassMultiplyShouldProduceExpectedValue()
    {
        // Given
        Mass<double> left = Mass<double>.FromGrams(10.0);  // 1e31 qg
        Mass<double> right = Mass<double>.FromGrams(3.0);  // 3e30 qg

        // When
        Mass<double> result = left.Multiply(right);  // 1e31 * 3e30 = 3e61 qg

        // Then
        Assert.Equal(1e31, left.QuectoGrams, Tolerance);
        Assert.Equal(3e30, right.QuectoGrams, Tolerance);
        Assert.Equal(3e61, result.QuectoGrams, Tolerance);
    }

    [Fact(DisplayName = "Mass.Divide should produce the expected result")]
    public void MassDivideShouldProduceExpectedValue()
    {
        // Given
        Mass<double> left = Mass<double>.FromGrams(100.0);  // 1e32 qg
        Mass<double> right = Mass<double>.FromGrams(20.0);  // 2e31 qg

        // When
        Mass<double> result = left.Divide(right);  // 1e32 / 2e31 = 5 qg

        // Then
        Assert.Equal(5.0, result.QuectoGrams, Tolerance);
        Assert.Equal(5e-30, result.Grams, Tolerance);
    }

    [Fact(DisplayName = "Mass comparison should produce the expected result (left equal to right)")]
    public void MassComparisonShouldProduceExpectedResultLeftEqualToRight()
    {
        // Given
        Mass<double> left = Mass<double>.FromKilograms(123.0);
        Mass<double> right = Mass<double>.FromKilograms(123.0);

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
        Mass<double> left = Mass<double>.FromKilograms(456.0);
        Mass<double> right = Mass<double>.FromKilograms(123.0);

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
        Mass<double> left = Mass<double>.FromKilograms(123.0);
        Mass<double> right = Mass<double>.FromKilograms(456.0);

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
        Mass<double> m = Mass<double>.FromKilograms(1.0);

        // When / Then
        Assert.Equal("1,000.000 g", $"{m:g3}");
        Assert.Equal("1.000 kg", $"{m:kg3}");
        Assert.Equal("1,000,000.000 mg", $"{m:mg3}");
        Assert.Equal("0.001 Mg", $"{m:Mg3}");
        Assert.Equal("0.001 t", $"{m:t3}");
        Assert.Equal("35.274 oz", $"{m:oz3}");
        Assert.Equal("2.205 lb", $"{m:lb3}");
        Assert.Equal("0.157 st", $"{m:st3}");
        Assert.Equal("5,000.000 ct", $"{m:ct3}");
    }

    [Fact(DisplayName = "Mass.ToString should honor custom culture separators")]
    public void MassToStringShouldHonorCustomCulture()
    {
        // Given
        CultureInfo customCulture = new("de-DE");
        Mass<double> m = Mass<double>.FromKilograms(1234.56);

        // When
        string formatted = m.ToString("kg2", customCulture);

        // Then
        Assert.Equal("1.234,56 kg", formatted);
    }
}
