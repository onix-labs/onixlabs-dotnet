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
    // The Dalton conversion factor (1660539.06892 qg, CODATA 2022) is not exactly representable in binary FP.
    // Constructing it as (166053906892 / 100000) gives the closest-Float128 to the rational, but a subsequent
    // multiplication by a value like 2.5 chains a second rounding, producing a result one ULP off from the
    // closest-Float128 of the true product. The tolerance below sits comfortably above one ULP at the magnitudes
    // used (~10^6) but well below anything that would mask a genuine factor bug.
    private static readonly Float128 DaltonTolerance = Float128.Parse("1e-25");

    private static void AssertNearlyEqual(Float128 expected, Float128 actual, Float128 tolerance)
    {
        Float128 diff = Float128.Abs(expected - actual);
        Assert.True(
            diff <= tolerance,
            $"Expected: {expected}\nActual:   {actual}\nDiff:     {diff} exceeds tolerance {tolerance}");
    }

    [Fact(DisplayName = "Mass.Zero should produce the expected result")]
    public void MassZeroShouldProduceExpectedResult()
    {
        // Given / When
        Mass<Float128> mass = Mass<Float128>.Zero;

        // Then
        Assert.Equal(Float128.Zero, mass.QuectoGrams);
    }

    [Theory(DisplayName = "Mass.FromQuectograms should produce the expected QuectoGrams")]
    [InlineData("0", "0")]
    [InlineData("1", "1")]
    [InlineData("2.5", "2.5")]
    public void MassFromQuectogramsShouldProduceExpectedQuectoGrams(string value, string expected)
    {
        Mass<Float128> m = Mass<Float128>.FromQuectograms(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), m.QuectoGrams);
    }

    [Theory(DisplayName = "Mass.FromRontograms should produce the expected QuectoGrams")]
    [InlineData("0", "0")]
    [InlineData("1", "1e3")]
    [InlineData("2.5", "2.5e3")]
    public void MassFromRontogramsShouldProduceExpectedQuectoGrams(string value, string expected)
    {
        Mass<Float128> m = Mass<Float128>.FromRontograms(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), m.QuectoGrams);
    }

    [Theory(DisplayName = "Mass.FromYoctograms should produce the expected QuectoGrams")]
    [InlineData("0", "0")]
    [InlineData("1", "1e6")]
    [InlineData("2.5", "2.5e6")]
    public void MassFromYoctogramsShouldProduceExpectedQuectoGrams(string value, string expected)
    {
        Mass<Float128> m = Mass<Float128>.FromYoctograms(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), m.QuectoGrams);
    }

    [Theory(DisplayName = "Mass.FromZeptograms should produce the expected QuectoGrams")]
    [InlineData("0", "0")]
    [InlineData("1", "1e9")]
    [InlineData("2.5", "2.5e9")]
    public void MassFromZeptogramsShouldProduceExpectedQuectoGrams(string value, string expected)
    {
        Mass<Float128> m = Mass<Float128>.FromZeptograms(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), m.QuectoGrams);
    }

    [Theory(DisplayName = "Mass.FromAttograms should produce the expected QuectoGrams")]
    [InlineData("0", "0")]
    [InlineData("1", "1e12")]
    [InlineData("2.5", "2.5e12")]
    public void MassFromAttogramsShouldProduceExpectedQuectoGrams(string value, string expected)
    {
        Mass<Float128> m = Mass<Float128>.FromAttograms(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), m.QuectoGrams);
    }

    [Theory(DisplayName = "Mass.FromFemtograms should produce the expected QuectoGrams")]
    [InlineData("0", "0")]
    [InlineData("1", "1e15")]
    [InlineData("2.5", "2.5e15")]
    public void MassFromFemtogramsShouldProduceExpectedQuectoGrams(string value, string expected)
    {
        Mass<Float128> m = Mass<Float128>.FromFemtograms(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), m.QuectoGrams);
    }

    [Theory(DisplayName = "Mass.FromPicograms should produce the expected QuectoGrams")]
    [InlineData("0", "0")]
    [InlineData("1", "1e18")]
    [InlineData("2.5", "2.5e18")]
    public void MassFromPicogramsShouldProduceExpectedQuectoGrams(string value, string expected)
    {
        Mass<Float128> m = Mass<Float128>.FromPicograms(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), m.QuectoGrams);
    }

    [Theory(DisplayName = "Mass.FromNanograms should produce the expected QuectoGrams")]
    [InlineData("0", "0")]
    [InlineData("1", "1e21")]
    [InlineData("2.5", "2.5e21")]
    public void MassFromNanogramsShouldProduceExpectedQuectoGrams(string value, string expected)
    {
        Mass<Float128> m = Mass<Float128>.FromNanograms(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), m.QuectoGrams);
    }

    [Theory(DisplayName = "Mass.FromMicrograms should produce the expected QuectoGrams")]
    [InlineData("0", "0")]
    [InlineData("1", "1e24")]
    [InlineData("2.5", "2.5e24")]
    public void MassFromMicrogramsShouldProduceExpectedQuectoGrams(string value, string expected)
    {
        Mass<Float128> m = Mass<Float128>.FromMicrograms(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), m.QuectoGrams);
    }

    [Theory(DisplayName = "Mass.FromMilligrams should produce the expected QuectoGrams")]
    [InlineData("0", "0")]
    [InlineData("1", "1e27")]
    [InlineData("2.5", "2.5e27")]
    public void MassFromMilligramsShouldProduceExpectedQuectoGrams(string value, string expected)
    {
        Mass<Float128> m = Mass<Float128>.FromMilligrams(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), m.QuectoGrams);
    }

    [Theory(DisplayName = "Mass.FromCentigrams should produce the expected QuectoGrams")]
    [InlineData("0", "0")]
    [InlineData("1", "1e28")]
    [InlineData("2.5", "2.5e28")]
    public void MassFromCentigramsShouldProduceExpectedQuectoGrams(string value, string expected)
    {
        Mass<Float128> m = Mass<Float128>.FromCentigrams(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), m.QuectoGrams);
    }

    [Theory(DisplayName = "Mass.FromDecigrams should produce the expected QuectoGrams")]
    [InlineData("0", "0")]
    [InlineData("1", "1e29")]
    [InlineData("2.5", "2.5e29")]
    public void MassFromDecigramsShouldProduceExpectedQuectoGrams(string value, string expected)
    {
        Mass<Float128> m = Mass<Float128>.FromDecigrams(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), m.QuectoGrams);
    }

    [Theory(DisplayName = "Mass.FromGrams should produce the expected QuectoGrams")]
    [InlineData("0", "0")]
    [InlineData("1", "1e30")]
    [InlineData("2.5", "2.5e30")]
    public void MassFromGramsShouldProduceExpectedQuectoGrams(string value, string expected)
    {
        Mass<Float128> m = Mass<Float128>.FromGrams(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), m.QuectoGrams);
    }

    [Theory(DisplayName = "Mass.FromDecagrams should produce the expected QuectoGrams")]
    [InlineData("0", "0")]
    [InlineData("1", "1e31")]
    [InlineData("2.5", "2.5e31")]
    public void MassFromDecagramsShouldProduceExpectedQuectoGrams(string value, string expected)
    {
        Mass<Float128> m = Mass<Float128>.FromDecagrams(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), m.QuectoGrams);
    }

    [Theory(DisplayName = "Mass.FromHectograms should produce the expected QuectoGrams")]
    [InlineData("0", "0")]
    [InlineData("1", "1e32")]
    [InlineData("2.5", "2.5e32")]
    public void MassFromHectogramsShouldProduceExpectedQuectoGrams(string value, string expected)
    {
        Mass<Float128> m = Mass<Float128>.FromHectograms(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), m.QuectoGrams);
    }

    [Theory(DisplayName = "Mass.FromKilograms should produce the expected QuectoGrams")]
    [InlineData("0", "0")]
    [InlineData("1", "1e33")]
    [InlineData("2.5", "2.5e33")]
    public void MassFromKilogramsShouldProduceExpectedQuectoGrams(string value, string expected)
    {
        Mass<Float128> m = Mass<Float128>.FromKilograms(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), m.QuectoGrams);
    }

    [Theory(DisplayName = "Mass.FromMegagrams should produce the expected QuectoGrams")]
    [InlineData("0", "0")]
    [InlineData("1", "1e36")]
    [InlineData("2.5", "2.5e36")]
    public void MassFromMegagramsShouldProduceExpectedQuectoGrams(string value, string expected)
    {
        Mass<Float128> m = Mass<Float128>.FromMegagrams(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), m.QuectoGrams);
    }

    [Theory(DisplayName = "Mass.FromGigagrams should produce the expected QuectoGrams")]
    [InlineData("0", "0")]
    [InlineData("1", "1e39")]
    [InlineData("2.5", "2.5e39")]
    public void MassFromGigagramsShouldProduceExpectedQuectoGrams(string value, string expected)
    {
        Mass<Float128> m = Mass<Float128>.FromGigagrams(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), m.QuectoGrams);
    }

    [Theory(DisplayName = "Mass.FromTeragrams should produce the expected QuectoGrams")]
    [InlineData("0", "0")]
    [InlineData("1", "1e42")]
    [InlineData("2.5", "2.5e42")]
    public void MassFromTeragramsShouldProduceExpectedQuectoGrams(string value, string expected)
    {
        Mass<Float128> m = Mass<Float128>.FromTeragrams(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), m.QuectoGrams);
    }

    [Theory(DisplayName = "Mass.FromPetagrams should produce the expected QuectoGrams")]
    [InlineData("0", "0")]
    [InlineData("1", "1e45")]
    [InlineData("2.5", "2.5e45")]
    public void MassFromPetagramsShouldProduceExpectedQuectoGrams(string value, string expected)
    {
        Mass<Float128> m = Mass<Float128>.FromPetagrams(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), m.QuectoGrams);
    }

    [Theory(DisplayName = "Mass.FromExagrams should produce the expected QuectoGrams")]
    [InlineData("0", "0")]
    [InlineData("1", "1e48")]
    [InlineData("2.5", "2.5e48")]
    public void MassFromExagramsShouldProduceExpectedQuectoGrams(string value, string expected)
    {
        Mass<Float128> m = Mass<Float128>.FromExagrams(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), m.QuectoGrams);
    }

    [Theory(DisplayName = "Mass.FromZettagrams should produce the expected QuectoGrams")]
    [InlineData("0", "0")]
    [InlineData("1", "1e51")]
    [InlineData("2.5", "2.5e51")]
    public void MassFromZettagramsShouldProduceExpectedQuectoGrams(string value, string expected)
    {
        Mass<Float128> m = Mass<Float128>.FromZettagrams(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), m.QuectoGrams);
    }

    [Theory(DisplayName = "Mass.FromYottagrams should produce the expected QuectoGrams")]
    [InlineData("0", "0")]
    [InlineData("1", "1e54")]
    [InlineData("2.5", "2.5e54")]
    public void MassFromYottagramsShouldProduceExpectedQuectoGrams(string value, string expected)
    {
        Mass<Float128> m = Mass<Float128>.FromYottagrams(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), m.QuectoGrams);
    }

    [Theory(DisplayName = "Mass.FromRonnagrams should produce the expected QuectoGrams")]
    [InlineData("0", "0")]
    [InlineData("1", "1e57")]
    [InlineData("2.5", "2.5e57")]
    public void MassFromRonnagramsShouldProduceExpectedQuectoGrams(string value, string expected)
    {
        Mass<Float128> m = Mass<Float128>.FromRonnagrams(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), m.QuectoGrams);
    }

    [Theory(DisplayName = "Mass.FromQuettagrams should produce the expected QuectoGrams")]
    [InlineData("0", "0")]
    [InlineData("1", "1e60")]
    [InlineData("2.5", "2.5e60")]
    public void MassFromQuettagramsShouldProduceExpectedQuectoGrams(string value, string expected)
    {
        Mass<Float128> m = Mass<Float128>.FromQuettagrams(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), m.QuectoGrams);
    }

    [Theory(DisplayName = "Mass.FromTonnes should produce the expected QuectoGrams")]
    [InlineData("0", "0")]
    [InlineData("1", "1e36")]
    [InlineData("2.5", "2.5e36")]
    public void MassFromTonnesShouldProduceExpectedQuectoGrams(string value, string expected)
    {
        Mass<Float128> m = Mass<Float128>.FromTonnes(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), m.QuectoGrams);
    }

    [Theory(DisplayName = "Mass.FromOunces should produce the expected QuectoGrams")]
    [InlineData("0", "0")]
    [InlineData("1", "28.349523125e30")]
    [InlineData("16", "453.59237e30")] // 16 oz = 1 lb
    public void MassFromOuncesShouldProduceExpectedQuectoGrams(string value, string expected)
    {
        Mass<Float128> m = Mass<Float128>.FromOunces(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), m.QuectoGrams);
    }

    [Theory(DisplayName = "Mass.FromPounds should produce the expected QuectoGrams")]
    [InlineData("0", "0")]
    [InlineData("1", "453.59237e30")]
    [InlineData("2.5", "1133.980925e30")]
    public void MassFromPoundsShouldProduceExpectedQuectoGrams(string value, string expected)
    {
        Mass<Float128> m = Mass<Float128>.FromPounds(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), m.QuectoGrams);
    }

    [Theory(DisplayName = "Mass.FromStones should produce the expected QuectoGrams")]
    [InlineData("0", "0")]
    [InlineData("1", "6350.29318e30")]
    [InlineData("2", "12700.58636e30")]
    public void MassFromStonesShouldProduceExpectedQuectoGrams(string value, string expected)
    {
        Mass<Float128> m = Mass<Float128>.FromStones(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), m.QuectoGrams);
    }

    [Theory(DisplayName = "Mass.FromShortTons should produce the expected QuectoGrams")]
    [InlineData("0", "0")]
    [InlineData("1", "907184.74e30")]
    [InlineData("2.5", "2267961.85e30")]
    public void MassFromShortTonsShouldProduceExpectedQuectoGrams(string value, string expected)
    {
        Mass<Float128> m = Mass<Float128>.FromShortTons(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), m.QuectoGrams);
    }

    [Theory(DisplayName = "Mass.FromLongTons should produce the expected QuectoGrams")]
    [InlineData("0", "0")]
    [InlineData("1", "1016046.9088e30")]
    [InlineData("2", "2032093.8176e30")]
    public void MassFromLongTonsShouldProduceExpectedQuectoGrams(string value, string expected)
    {
        Mass<Float128> m = Mass<Float128>.FromLongTons(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), m.QuectoGrams);
    }

    [Theory(DisplayName = "Mass.FromCarats should produce the expected QuectoGrams")]
    [InlineData("0", "0")]
    [InlineData("1", "0.2e30")]
    [InlineData("5", "1.0e30")] // 5 ct = 1 g
    public void MassFromCaratsShouldProduceExpectedQuectoGrams(string value, string expected)
    {
        Mass<Float128> m = Mass<Float128>.FromCarats(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), m.QuectoGrams);
    }

    [Theory(DisplayName = "Mass.FromGrains should produce the expected QuectoGrams")]
    [InlineData("0", "0")]
    [InlineData("1", "0.06479891e30")]
    [InlineData("7000", "453.59237e30")] // 7000 gr = 1 lb
    public void MassFromGrainsShouldProduceExpectedQuectoGrams(string value, string expected)
    {
        Mass<Float128> m = Mass<Float128>.FromGrains(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), m.QuectoGrams);
    }

    [Theory(DisplayName = "Mass.FromDrams should produce the expected QuectoGrams")]
    [InlineData("0", "0")]
    [InlineData("1", "1.7718451953125e30")]
    [InlineData("16", "28.349523125e30")] // 16 dr = 1 oz
    public void MassFromDramsShouldProduceExpectedQuectoGrams(string value, string expected)
    {
        Mass<Float128> m = Mass<Float128>.FromDrams(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), m.QuectoGrams);
    }

    [Theory(DisplayName = "Mass.FromSlugs should produce the expected QuectoGrams")]
    [InlineData("0", "0")]
    [InlineData("1", "14593.90293720636e30")]
    [InlineData("2", "29187.80587441272e30")]
    public void MassFromSlugsShouldProduceExpectedQuectoGrams(string value, string expected)
    {
        Mass<Float128> m = Mass<Float128>.FromSlugs(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), m.QuectoGrams);
    }

    [Theory(DisplayName = "Mass.FromDaltons should produce the expected QuectoGrams")]
    [InlineData("0", "0")]
    [InlineData("1", "1.66053906892e6")]
    [InlineData("2.5", "4.15134767230e6")]
    public void MassFromDaltonsShouldProduceExpectedQuectoGrams(string value, string expected)
    {
        Mass<Float128> m = Mass<Float128>.FromDaltons(Float128.Parse(value));
        AssertNearlyEqual(Float128.Parse(expected), m.QuectoGrams, DaltonTolerance);
    }

    [Fact(DisplayName = "Mass.Add should produce the expected result")]
    public void MassAddShouldProduceExpectedValue()
    {
        // Given
        Mass<Float128> left = Mass<Float128>.FromKilograms(Float128.Parse("1.5"));
        Mass<Float128> right = Mass<Float128>.FromKilograms(Float128.Parse("0.5"));

        // When
        Mass<Float128> result = left.Add(right);

        // Then
        Assert.Equal(Float128.Parse("2"), result.KiloGrams);
    }

    [Fact(DisplayName = "Mass.Subtract should produce the expected result")]
    public void MassSubtractShouldProduceExpectedValue()
    {
        // Given
        Mass<Float128> left = Mass<Float128>.FromKilograms(Float128.Parse("1.5"));
        Mass<Float128> right = Mass<Float128>.FromKilograms(Float128.Parse("0.4"));

        // When
        Mass<Float128> result = left.Subtract(right);

        // Then
        Assert.Equal(Float128.Parse("1.1"), result.KiloGrams);
    }

    [Fact(DisplayName = "Mass comparison should produce the expected result (left equal to right)")]
    public void MassComparisonShouldProduceExpectedResultLeftEqualToRight()
    {
        // Given
        Mass<Float128> left = Mass<Float128>.FromKilograms(Float128.Parse("123"));
        Mass<Float128> right = Mass<Float128>.FromKilograms(Float128.Parse("123"));

        // When / Then
        Assert.Equal(0, Mass<Float128>.Compare(left, right));
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
        Mass<Float128> left = Mass<Float128>.FromKilograms(Float128.Parse("456"));
        Mass<Float128> right = Mass<Float128>.FromKilograms(Float128.Parse("123"));

        // When / Then
        Assert.Equal(1, Mass<Float128>.Compare(left, right));
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
        Mass<Float128> left = Mass<Float128>.FromKilograms(Float128.Parse("123"));
        Mass<Float128> right = Mass<Float128>.FromKilograms(Float128.Parse("456"));

        // When / Then
        Assert.Equal(-1, Mass<Float128>.Compare(left, right));
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
        // Given — 2 kg and 2000 g are the same canonical mass; equality should hold at Float128.
        Mass<Float128> left = Mass<Float128>.FromKilograms(Float128.Parse("2"));
        Mass<Float128> right = Mass<Float128>.FromGrams(Float128.Parse("2000"));

        // When / Then
        Assert.True(Mass<Float128>.Equals(left, right));
        Assert.True(left.Equals(right));
        Assert.True(left.Equals((object)right));
        Assert.True(left == right);
        Assert.False(left != right);
    }

    [Fact(DisplayName = "Mass equality should produce the expected result (left not equal to right)")]
    public void MassEqualityShouldProduceExpectedResultLeftNotEqualToRight()
    {
        // Given
        Mass<Float128> left = Mass<Float128>.FromKilograms(Float128.Parse("2"));
        Mass<Float128> right = Mass<Float128>.FromGrams(Float128.Parse("2500"));

        // When / Then
        Assert.False(Mass<Float128>.Equals(left, right));
        Assert.False(left.Equals(right));
        Assert.False(left.Equals((object)right));
        Assert.False(left == right);
        Assert.True(left != right);
    }

    [Fact(DisplayName = "Mass.ToString should produce the expected result")]
    public void MassToStringShouldProduceExpectedResult()
    {
        // Given
        Mass<Float128> m = Mass<Float128>.FromKilograms(Float128.Parse("1"));

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
        Mass<Float128> m = Mass<Float128>.FromKilograms(Float128.Parse("1234.56"));

        // When
        string formatted = m.ToString("kg2", customCulture);

        // Then
        Assert.Equal("1.234,56 kg", formatted);
    }
}
