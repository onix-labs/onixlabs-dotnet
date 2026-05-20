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

public sealed class AmountOfSubstanceTests
{
    [Fact(DisplayName = "AmountOfSubstance.Zero should produce the expected result")]
    public void AmountOfSubstanceZeroShouldProduceExpectedResult()
    {
        // Given / When
        AmountOfSubstance<Float128> amount = AmountOfSubstance<Float128>.Zero;

        // Then
        Assert.Equal(Float128.Zero, amount.QuectoMoles);
    }

    [Theory(DisplayName = "AmountOfSubstance.FromQuectomoles should produce the expected QuectoMoles")]
    [InlineData("0", "0")]
    [InlineData("1", "1")]
    [InlineData("2.5", "2.5")]
    public void AmountOfSubstanceFromQuectomolesShouldProduceExpectedQuectoMoles(string value, string expected)
    {
        AmountOfSubstance<Float128> a = AmountOfSubstance<Float128>.FromQuectomoles(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), a.QuectoMoles);
    }

    [Theory(DisplayName = "AmountOfSubstance.FromRontomoles should produce the expected QuectoMoles")]
    [InlineData("0", "0")]
    [InlineData("1", "1e3")]
    [InlineData("2.5", "2.5e3")]
    public void AmountOfSubstanceFromRontomolesShouldProduceExpectedQuectoMoles(string value, string expected)
    {
        AmountOfSubstance<Float128> a = AmountOfSubstance<Float128>.FromRontomoles(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), a.QuectoMoles);
    }

    [Theory(DisplayName = "AmountOfSubstance.FromYoctomoles should produce the expected QuectoMoles")]
    [InlineData("0", "0")]
    [InlineData("1", "1e6")]
    [InlineData("2.5", "2.5e6")]
    public void AmountOfSubstanceFromYoctomolesShouldProduceExpectedQuectoMoles(string value, string expected)
    {
        AmountOfSubstance<Float128> a = AmountOfSubstance<Float128>.FromYoctomoles(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), a.QuectoMoles);
    }

    [Theory(DisplayName = "AmountOfSubstance.FromZeptomoles should produce the expected QuectoMoles")]
    [InlineData("0", "0")]
    [InlineData("1", "1e9")]
    [InlineData("2.5", "2.5e9")]
    public void AmountOfSubstanceFromZeptomolesShouldProduceExpectedQuectoMoles(string value, string expected)
    {
        AmountOfSubstance<Float128> a = AmountOfSubstance<Float128>.FromZeptomoles(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), a.QuectoMoles);
    }

    [Theory(DisplayName = "AmountOfSubstance.FromAttomoles should produce the expected QuectoMoles")]
    [InlineData("0", "0")]
    [InlineData("1", "1e12")]
    [InlineData("2.5", "2.5e12")]
    public void AmountOfSubstanceFromAttomolesShouldProduceExpectedQuectoMoles(string value, string expected)
    {
        AmountOfSubstance<Float128> a = AmountOfSubstance<Float128>.FromAttomoles(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), a.QuectoMoles);
    }

    [Theory(DisplayName = "AmountOfSubstance.FromFemtomoles should produce the expected QuectoMoles")]
    [InlineData("0", "0")]
    [InlineData("1", "1e15")]
    [InlineData("2.5", "2.5e15")]
    public void AmountOfSubstanceFromFemtomolesShouldProduceExpectedQuectoMoles(string value, string expected)
    {
        AmountOfSubstance<Float128> a = AmountOfSubstance<Float128>.FromFemtomoles(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), a.QuectoMoles);
    }

    [Theory(DisplayName = "AmountOfSubstance.FromPicomoles should produce the expected QuectoMoles")]
    [InlineData("0", "0")]
    [InlineData("1", "1e18")]
    [InlineData("2.5", "2.5e18")]
    public void AmountOfSubstanceFromPicomolesShouldProduceExpectedQuectoMoles(string value, string expected)
    {
        AmountOfSubstance<Float128> a = AmountOfSubstance<Float128>.FromPicomoles(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), a.QuectoMoles);
    }

    [Theory(DisplayName = "AmountOfSubstance.FromNanomoles should produce the expected QuectoMoles")]
    [InlineData("0", "0")]
    [InlineData("1", "1e21")]
    [InlineData("2.5", "2.5e21")]
    public void AmountOfSubstanceFromNanomolesShouldProduceExpectedQuectoMoles(string value, string expected)
    {
        AmountOfSubstance<Float128> a = AmountOfSubstance<Float128>.FromNanomoles(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), a.QuectoMoles);
    }

    [Theory(DisplayName = "AmountOfSubstance.FromMicromoles should produce the expected QuectoMoles")]
    [InlineData("0", "0")]
    [InlineData("1", "1e24")]
    [InlineData("2.5", "2.5e24")]
    public void AmountOfSubstanceFromMicromolesShouldProduceExpectedQuectoMoles(string value, string expected)
    {
        AmountOfSubstance<Float128> a = AmountOfSubstance<Float128>.FromMicromoles(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), a.QuectoMoles);
    }

    [Theory(DisplayName = "AmountOfSubstance.FromMillimoles should produce the expected QuectoMoles")]
    [InlineData("0", "0")]
    [InlineData("1", "1e27")]
    [InlineData("2.5", "2.5e27")]
    public void AmountOfSubstanceFromMillimolesShouldProduceExpectedQuectoMoles(string value, string expected)
    {
        AmountOfSubstance<Float128> a = AmountOfSubstance<Float128>.FromMillimoles(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), a.QuectoMoles);
    }

    [Theory(DisplayName = "AmountOfSubstance.FromCentimoles should produce the expected QuectoMoles")]
    [InlineData("0", "0")]
    [InlineData("1", "1e28")]
    [InlineData("2.5", "2.5e28")]
    public void AmountOfSubstanceFromCentimolesShouldProduceExpectedQuectoMoles(string value, string expected)
    {
        AmountOfSubstance<Float128> a = AmountOfSubstance<Float128>.FromCentimoles(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), a.QuectoMoles);
    }

    [Theory(DisplayName = "AmountOfSubstance.FromDecimoles should produce the expected QuectoMoles")]
    [InlineData("0", "0")]
    [InlineData("1", "1e29")]
    [InlineData("2.5", "2.5e29")]
    public void AmountOfSubstanceFromDecimolesShouldProduceExpectedQuectoMoles(string value, string expected)
    {
        AmountOfSubstance<Float128> a = AmountOfSubstance<Float128>.FromDecimoles(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), a.QuectoMoles);
    }

    [Theory(DisplayName = "AmountOfSubstance.FromMoles should produce the expected QuectoMoles")]
    [InlineData("0", "0")]
    [InlineData("1", "1e30")]
    [InlineData("2.5", "2.5e30")]
    public void AmountOfSubstanceFromMolesShouldProduceExpectedQuectoMoles(string value, string expected)
    {
        AmountOfSubstance<Float128> a = AmountOfSubstance<Float128>.FromMoles(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), a.QuectoMoles);
    }

    [Theory(DisplayName = "AmountOfSubstance.FromDecamoles should produce the expected QuectoMoles")]
    [InlineData("0", "0")]
    [InlineData("1", "1e31")]
    [InlineData("2.5", "2.5e31")]
    public void AmountOfSubstanceFromDecamolesShouldProduceExpectedQuectoMoles(string value, string expected)
    {
        AmountOfSubstance<Float128> a = AmountOfSubstance<Float128>.FromDecamoles(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), a.QuectoMoles);
    }

    [Theory(DisplayName = "AmountOfSubstance.FromHectomoles should produce the expected QuectoMoles")]
    [InlineData("0", "0")]
    [InlineData("1", "1e32")]
    [InlineData("2.5", "2.5e32")]
    public void AmountOfSubstanceFromHectomolesShouldProduceExpectedQuectoMoles(string value, string expected)
    {
        AmountOfSubstance<Float128> a = AmountOfSubstance<Float128>.FromHectomoles(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), a.QuectoMoles);
    }

    [Theory(DisplayName = "AmountOfSubstance.FromKilomoles should produce the expected QuectoMoles")]
    [InlineData("0", "0")]
    [InlineData("1", "1e33")]
    [InlineData("2.5", "2.5e33")]
    public void AmountOfSubstanceFromKilomolesShouldProduceExpectedQuectoMoles(string value, string expected)
    {
        AmountOfSubstance<Float128> a = AmountOfSubstance<Float128>.FromKilomoles(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), a.QuectoMoles);
    }

    [Theory(DisplayName = "AmountOfSubstance.FromMegamoles should produce the expected QuectoMoles")]
    [InlineData("0", "0")]
    [InlineData("1", "1e36")]
    [InlineData("2.5", "2.5e36")]
    public void AmountOfSubstanceFromMegamolesShouldProduceExpectedQuectoMoles(string value, string expected)
    {
        AmountOfSubstance<Float128> a = AmountOfSubstance<Float128>.FromMegamoles(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), a.QuectoMoles);
    }

    [Theory(DisplayName = "AmountOfSubstance.FromGigamoles should produce the expected QuectoMoles")]
    [InlineData("0", "0")]
    [InlineData("1", "1e39")]
    [InlineData("2.5", "2.5e39")]
    public void AmountOfSubstanceFromGigamolesShouldProduceExpectedQuectoMoles(string value, string expected)
    {
        AmountOfSubstance<Float128> a = AmountOfSubstance<Float128>.FromGigamoles(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), a.QuectoMoles);
    }

    [Theory(DisplayName = "AmountOfSubstance.FromTeramoles should produce the expected QuectoMoles")]
    [InlineData("0", "0")]
    [InlineData("1", "1e42")]
    [InlineData("2.5", "2.5e42")]
    public void AmountOfSubstanceFromTeramolesShouldProduceExpectedQuectoMoles(string value, string expected)
    {
        AmountOfSubstance<Float128> a = AmountOfSubstance<Float128>.FromTeramoles(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), a.QuectoMoles);
    }

    [Theory(DisplayName = "AmountOfSubstance.FromPetamoles should produce the expected QuectoMoles")]
    [InlineData("0", "0")]
    [InlineData("1", "1e45")]
    [InlineData("2.5", "2.5e45")]
    public void AmountOfSubstanceFromPetamolesShouldProduceExpectedQuectoMoles(string value, string expected)
    {
        AmountOfSubstance<Float128> a = AmountOfSubstance<Float128>.FromPetamoles(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), a.QuectoMoles);
    }

    [Theory(DisplayName = "AmountOfSubstance.FromExamoles should produce the expected QuectoMoles")]
    [InlineData("0", "0")]
    [InlineData("1", "1e48")]
    [InlineData("2.5", "2.5e48")]
    public void AmountOfSubstanceFromExamolesShouldProduceExpectedQuectoMoles(string value, string expected)
    {
        AmountOfSubstance<Float128> a = AmountOfSubstance<Float128>.FromExamoles(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), a.QuectoMoles);
    }

    [Theory(DisplayName = "AmountOfSubstance.FromZettamoles should produce the expected QuectoMoles")]
    [InlineData("0", "0")]
    [InlineData("1", "1e51")]
    [InlineData("2.5", "2.5e51")]
    public void AmountOfSubstanceFromZettamolesShouldProduceExpectedQuectoMoles(string value, string expected)
    {
        AmountOfSubstance<Float128> a = AmountOfSubstance<Float128>.FromZettamoles(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), a.QuectoMoles);
    }

    [Theory(DisplayName = "AmountOfSubstance.FromYottamoles should produce the expected QuectoMoles")]
    [InlineData("0", "0")]
    [InlineData("1", "1e54")]
    [InlineData("2.5", "2.5e54")]
    public void AmountOfSubstanceFromYottamolesShouldProduceExpectedQuectoMoles(string value, string expected)
    {
        AmountOfSubstance<Float128> a = AmountOfSubstance<Float128>.FromYottamoles(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), a.QuectoMoles);
    }

    [Theory(DisplayName = "AmountOfSubstance.FromRonnamoles should produce the expected QuectoMoles")]
    [InlineData("0", "0")]
    [InlineData("1", "1e57")]
    [InlineData("2.5", "2.5e57")]
    public void AmountOfSubstanceFromRonnamolesShouldProduceExpectedQuectoMoles(string value, string expected)
    {
        AmountOfSubstance<Float128> a = AmountOfSubstance<Float128>.FromRonnamoles(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), a.QuectoMoles);
    }

    [Theory(DisplayName = "AmountOfSubstance.FromQuettamoles should produce the expected QuectoMoles")]
    [InlineData("0", "0")]
    [InlineData("1", "1e60")]
    [InlineData("2.5", "2.5e60")]
    public void AmountOfSubstanceFromQuettamolesShouldProduceExpectedQuectoMoles(string value, string expected)
    {
        AmountOfSubstance<Float128> a = AmountOfSubstance<Float128>.FromQuettamoles(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), a.QuectoMoles);
    }

    [Fact(DisplayName = "AmountOfSubstance.Add should produce the expected result")]
    public void AmountOfSubstanceAddShouldProduceExpectedValue()
    {
        // Given
        AmountOfSubstance<Float128> left = AmountOfSubstance<Float128>.FromMoles(Float128.Parse("1.5"));
        AmountOfSubstance<Float128> right = AmountOfSubstance<Float128>.FromMoles(Float128.Parse("0.5"));

        // When
        AmountOfSubstance<Float128> result = left.Add(right);

        // Then
        Assert.Equal(Float128.Parse("2"), result.Moles);
    }

    [Fact(DisplayName = "AmountOfSubstance.Subtract should produce the expected result")]
    public void AmountOfSubstanceSubtractShouldProduceExpectedValue()
    {
        // Given
        AmountOfSubstance<Float128> left = AmountOfSubstance<Float128>.FromMoles(Float128.Parse("1.5"));
        AmountOfSubstance<Float128> right = AmountOfSubstance<Float128>.FromMoles(Float128.Parse("0.4"));

        // When
        AmountOfSubstance<Float128> result = left.Subtract(right);

        // Then
        Assert.Equal(Float128.Parse("1.1"), result.Moles);
    }

    [Fact(DisplayName = "AmountOfSubstance comparison should produce the expected result (left equal to right)")]
    public void AmountOfSubstanceComparisonShouldProduceExpectedResultLeftEqualToRight()
    {
        // Given
        AmountOfSubstance<Float128> left = AmountOfSubstance<Float128>.FromMoles(Float128.Parse("123"));
        AmountOfSubstance<Float128> right = AmountOfSubstance<Float128>.FromMoles(Float128.Parse("123"));

        // When / Then
        Assert.Equal(0, AmountOfSubstance<Float128>.Compare(left, right));
        Assert.Equal(0, left.CompareTo(right));
        Assert.Equal(0, left.CompareTo((object)right));
        Assert.False(left > right);
        Assert.True(left >= right);
        Assert.False(left < right);
        Assert.True(left <= right);
    }

    [Fact(DisplayName = "AmountOfSubstance comparison should produce the expected result (left greater than right)")]
    public void AmountOfSubstanceComparisonShouldProduceExpectedLeftGreaterThanRight()
    {
        // Given
        AmountOfSubstance<Float128> left = AmountOfSubstance<Float128>.FromMoles(Float128.Parse("456"));
        AmountOfSubstance<Float128> right = AmountOfSubstance<Float128>.FromMoles(Float128.Parse("123"));

        // When / Then
        Assert.Equal(1, AmountOfSubstance<Float128>.Compare(left, right));
        Assert.Equal(1, left.CompareTo(right));
        Assert.Equal(1, left.CompareTo((object)right));
        Assert.True(left > right);
        Assert.True(left >= right);
        Assert.False(left < right);
        Assert.False(left <= right);
    }

    [Fact(DisplayName = "AmountOfSubstance comparison should produce the expected result (left less than right)")]
    public void AmountOfSubstanceComparisonShouldProduceExpectedLeftLessThanRight()
    {
        // Given
        AmountOfSubstance<Float128> left = AmountOfSubstance<Float128>.FromMoles(Float128.Parse("123"));
        AmountOfSubstance<Float128> right = AmountOfSubstance<Float128>.FromMoles(Float128.Parse("456"));

        // When / Then
        Assert.Equal(-1, AmountOfSubstance<Float128>.Compare(left, right));
        Assert.Equal(-1, left.CompareTo(right));
        Assert.Equal(-1, left.CompareTo((object)right));
        Assert.False(left > right);
        Assert.False(left >= right);
        Assert.True(left < right);
        Assert.True(left <= right);
    }

    [Fact(DisplayName = "AmountOfSubstance equality should produce the expected result (left equal to right)")]
    public void AmountOfSubstanceEqualityShouldProduceExpectedResultLeftEqualToRight()
    {
        // Given — 2 mol and 2000 mmol are the same canonical amount; equality should hold at Float128.
        AmountOfSubstance<Float128> left = AmountOfSubstance<Float128>.FromMoles(Float128.Parse("2"));
        AmountOfSubstance<Float128> right = AmountOfSubstance<Float128>.FromMillimoles(Float128.Parse("2000"));

        // When / Then
        Assert.True(AmountOfSubstance<Float128>.Equals(left, right));
        Assert.True(left.Equals(right));
        Assert.True(left.Equals((object)right));
        Assert.True(left == right);
        Assert.False(left != right);
    }

    [Fact(DisplayName = "AmountOfSubstance equality should produce the expected result (left not equal to right)")]
    public void AmountOfSubstanceEqualityShouldProduceExpectedResultLeftNotEqualToRight()
    {
        // Given
        AmountOfSubstance<Float128> left = AmountOfSubstance<Float128>.FromMoles(Float128.Parse("2"));
        AmountOfSubstance<Float128> right = AmountOfSubstance<Float128>.FromMillimoles(Float128.Parse("2500"));

        // When / Then
        Assert.False(AmountOfSubstance<Float128>.Equals(left, right));
        Assert.False(left.Equals(right));
        Assert.False(left.Equals((object)right));
        Assert.False(left == right);
        Assert.True(left != right);
    }

    [Fact(DisplayName = "AmountOfSubstance.ToString should produce the expected result")]
    public void AmountOfSubstanceToStringShouldProduceExpectedResult()
    {
        // Given
        AmountOfSubstance<Float128> a = AmountOfSubstance<Float128>.FromMoles(Float128.Parse("1000"));

        // When / Then
        Assert.Equal("1,000.000 mol", $"{a:mol3}");
        Assert.Equal("1.000 kmol", $"{a:kmol3}");
        Assert.Equal("0.001 Mmol", $"{a:Mmol3}");
        Assert.Equal("1,000,000.000 mmol", $"{a:mmol3}");
    }

    [Fact(DisplayName = "AmountOfSubstance.ToString Mmol vs mmol are case-sensitive")]
    public void AmountOfSubstanceToStringMmolVsMmolAreCaseSensitive()
    {
        // Given
        AmountOfSubstance<Float128> a = AmountOfSubstance<Float128>.FromMoles(Float128.Parse("1"));

        // Then
        Assert.Equal("0.000001 Mmol", $"{a:Mmol6}"); // mega
        Assert.Equal("1,000.000 mmol", $"{a:mmol3}"); // milli
    }

    [Fact(DisplayName = "AmountOfSubstance.ToString Pmol vs pmol are case-sensitive")]
    public void AmountOfSubstanceToStringPmolVsPmolAreCaseSensitive()
    {
        // Given
        AmountOfSubstance<Float128> a = AmountOfSubstance<Float128>.FromMoles(Float128.Parse("1"));

        // Then
        Assert.Equal("0.000000000000001 Pmol", $"{a:Pmol15}"); // peta
        Assert.Equal("1,000,000,000,000.000 pmol", $"{a:pmol3}"); // pico
    }

    [Fact(DisplayName = "AmountOfSubstance.ToString µmol symbol should differ from format specifier")]
    public void AmountOfSubstanceToStringMicromolesSymbolShouldDifferFromFormatSpecifier()
    {
        // Given
        AmountOfSubstance<Float128> a = AmountOfSubstance<Float128>.FromMoles(Float128.Parse("1"));

        // Then: specifier is umol, but symbol rendered is µmol
        Assert.Equal("1,000,000.000 µmol", $"{a:umol3}");
    }

    [Fact(DisplayName = "AmountOfSubstance.ToString should honor custom culture separators")]
    public void AmountOfSubstanceToStringShouldHonorCustomCulture()
    {
        // Given
        CultureInfo customCulture = new("de-DE");
        AmountOfSubstance<Float128> a = AmountOfSubstance<Float128>.FromMoles(Float128.Parse("1234.56"));

        // When
        string formatted = a.ToString("mol2", customCulture);

        // Then
        Assert.Equal("1.234,56 mol", formatted);
    }
}
