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
    // IEEE-754 binary floating-point arithmetic causes small discrepancies in calculation, therefore we need a tolerance.
    private const double Tolerance = 1e+42;

    [Fact(DisplayName = "AmountOfSubstance.Zero should produce the expected result")]
    public void AmountOfSubstanceZeroShouldProduceExpectedResult()
    {
        // Given / When
        AmountOfSubstance<double> amount = AmountOfSubstance<double>.Zero;

        // Then
        Assert.Equal(0.0, amount.QuectoMoles, Tolerance);
    }

    [Theory(DisplayName = "AmountOfSubstance.FromQuectomoles should produce the expected QuectoMoles")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1.0)]
    [InlineData(2.5, 2.5)]
    public void AmountOfSubstanceFromQuectomolesShouldProduceExpectedQuectoMoles(double value, double expected)
    {
        AmountOfSubstance<double> a = AmountOfSubstance<double>.FromQuectomoles(value);
        Assert.Equal(expected, a.QuectoMoles, Tolerance);
    }

    [Theory(DisplayName = "AmountOfSubstance.FromRontomoles should produce the expected QuectoMoles")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e3)]
    [InlineData(2.5, 2.5e3)]
    public void AmountOfSubstanceFromRontomolesShouldProduceExpectedQuectoMoles(double value, double expected)
    {
        AmountOfSubstance<double> a = AmountOfSubstance<double>.FromRontomoles(value);
        Assert.Equal(expected, a.QuectoMoles, Tolerance);
    }

    [Theory(DisplayName = "AmountOfSubstance.FromYoctomoles should produce the expected QuectoMoles")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e6)]
    [InlineData(2.5, 2.5e6)]
    public void AmountOfSubstanceFromYoctomolesShouldProduceExpectedQuectoMoles(double value, double expected)
    {
        AmountOfSubstance<double> a = AmountOfSubstance<double>.FromYoctomoles(value);
        Assert.Equal(expected, a.QuectoMoles, Tolerance);
    }

    [Theory(DisplayName = "AmountOfSubstance.FromZeptomoles should produce the expected QuectoMoles")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e9)]
    [InlineData(2.5, 2.5e9)]
    public void AmountOfSubstanceFromZeptomolesShouldProduceExpectedQuectoMoles(double value, double expected)
    {
        AmountOfSubstance<double> a = AmountOfSubstance<double>.FromZeptomoles(value);
        Assert.Equal(expected, a.QuectoMoles, Tolerance);
    }

    [Theory(DisplayName = "AmountOfSubstance.FromAttomoles should produce the expected QuectoMoles")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e12)]
    [InlineData(2.5, 2.5e12)]
    public void AmountOfSubstanceFromAttomolesShouldProduceExpectedQuectoMoles(double value, double expected)
    {
        AmountOfSubstance<double> a = AmountOfSubstance<double>.FromAttomoles(value);
        Assert.Equal(expected, a.QuectoMoles, Tolerance);
    }

    [Theory(DisplayName = "AmountOfSubstance.FromFemtomoles should produce the expected QuectoMoles")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e15)]
    [InlineData(2.5, 2.5e15)]
    public void AmountOfSubstanceFromFemtomolesShouldProduceExpectedQuectoMoles(double value, double expected)
    {
        AmountOfSubstance<double> a = AmountOfSubstance<double>.FromFemtomoles(value);
        Assert.Equal(expected, a.QuectoMoles, Tolerance);
    }

    [Theory(DisplayName = "AmountOfSubstance.FromPicomoles should produce the expected QuectoMoles")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e18)]
    [InlineData(2.5, 2.5e18)]
    public void AmountOfSubstanceFromPicomolesShouldProduceExpectedQuectoMoles(double value, double expected)
    {
        AmountOfSubstance<double> a = AmountOfSubstance<double>.FromPicomoles(value);
        Assert.Equal(expected, a.QuectoMoles, Tolerance);
    }

    [Theory(DisplayName = "AmountOfSubstance.FromNanomoles should produce the expected QuectoMoles")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e21)]
    [InlineData(2.5, 2.5e21)]
    public void AmountOfSubstanceFromNanomolesShouldProduceExpectedQuectoMoles(double value, double expected)
    {
        AmountOfSubstance<double> a = AmountOfSubstance<double>.FromNanomoles(value);
        Assert.Equal(expected, a.QuectoMoles, Tolerance);
    }

    [Theory(DisplayName = "AmountOfSubstance.FromMicromoles should produce the expected QuectoMoles")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e24)]
    [InlineData(2.5, 2.5e24)]
    public void AmountOfSubstanceFromMicromolesShouldProduceExpectedQuectoMoles(double value, double expected)
    {
        AmountOfSubstance<double> a = AmountOfSubstance<double>.FromMicromoles(value);
        Assert.Equal(expected, a.QuectoMoles, Tolerance);
    }

    [Theory(DisplayName = "AmountOfSubstance.FromMillimoles should produce the expected QuectoMoles")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e27)]
    [InlineData(2.5, 2.5e27)]
    public void AmountOfSubstanceFromMillimolesShouldProduceExpectedQuectoMoles(double value, double expected)
    {
        AmountOfSubstance<double> a = AmountOfSubstance<double>.FromMillimoles(value);
        Assert.Equal(expected, a.QuectoMoles, Tolerance);
    }

    [Theory(DisplayName = "AmountOfSubstance.FromCentimoles should produce the expected QuectoMoles")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e28)]
    [InlineData(2.5, 2.5e28)]
    public void AmountOfSubstanceFromCentimolesShouldProduceExpectedQuectoMoles(double value, double expected)
    {
        AmountOfSubstance<double> a = AmountOfSubstance<double>.FromCentimoles(value);
        Assert.Equal(expected, a.QuectoMoles, Tolerance);
    }

    [Theory(DisplayName = "AmountOfSubstance.FromDecimoles should produce the expected QuectoMoles")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e29)]
    [InlineData(2.5, 2.5e29)]
    public void AmountOfSubstanceFromDecimolesShouldProduceExpectedQuectoMoles(double value, double expected)
    {
        AmountOfSubstance<double> a = AmountOfSubstance<double>.FromDecimoles(value);
        Assert.Equal(expected, a.QuectoMoles, Tolerance);
    }

    [Theory(DisplayName = "AmountOfSubstance.FromMoles should produce the expected QuectoMoles")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e30)]
    [InlineData(2.5, 2.5e30)]
    public void AmountOfSubstanceFromMolesShouldProduceExpectedQuectoMoles(double value, double expected)
    {
        AmountOfSubstance<double> a = AmountOfSubstance<double>.FromMoles(value);
        Assert.Equal(expected, a.QuectoMoles, Tolerance);
    }

    [Theory(DisplayName = "AmountOfSubstance.FromDecamoles should produce the expected QuectoMoles")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e31)]
    [InlineData(2.5, 2.5e31)]
    public void AmountOfSubstanceFromDecamolesShouldProduceExpectedQuectoMoles(double value, double expected)
    {
        AmountOfSubstance<double> a = AmountOfSubstance<double>.FromDecamoles(value);
        Assert.Equal(expected, a.QuectoMoles, Tolerance);
    }

    [Theory(DisplayName = "AmountOfSubstance.FromHectomoles should produce the expected QuectoMoles")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e32)]
    [InlineData(2.5, 2.5e32)]
    public void AmountOfSubstanceFromHectomolesShouldProduceExpectedQuectoMoles(double value, double expected)
    {
        AmountOfSubstance<double> a = AmountOfSubstance<double>.FromHectomoles(value);
        Assert.Equal(expected, a.QuectoMoles, Tolerance);
    }

    [Theory(DisplayName = "AmountOfSubstance.FromKilomoles should produce the expected QuectoMoles")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e33)]
    [InlineData(2.5, 2.5e33)]
    public void AmountOfSubstanceFromKilomolesShouldProduceExpectedQuectoMoles(double value, double expected)
    {
        AmountOfSubstance<double> a = AmountOfSubstance<double>.FromKilomoles(value);
        Assert.Equal(expected, a.QuectoMoles, Tolerance);
    }

    [Theory(DisplayName = "AmountOfSubstance.FromMegamoles should produce the expected QuectoMoles")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e36)]
    [InlineData(2.5, 2.5e36)]
    public void AmountOfSubstanceFromMegamolesShouldProduceExpectedQuectoMoles(double value, double expected)
    {
        AmountOfSubstance<double> a = AmountOfSubstance<double>.FromMegamoles(value);
        Assert.Equal(expected, a.QuectoMoles, Tolerance);
    }

    [Theory(DisplayName = "AmountOfSubstance.FromGigamoles should produce the expected QuectoMoles")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e39)]
    [InlineData(2.5, 2.5e39)]
    public void AmountOfSubstanceFromGigamolesShouldProduceExpectedQuectoMoles(double value, double expected)
    {
        AmountOfSubstance<double> a = AmountOfSubstance<double>.FromGigamoles(value);
        Assert.Equal(expected, a.QuectoMoles, Tolerance);
    }

    [Theory(DisplayName = "AmountOfSubstance.FromTeramoles should produce the expected QuectoMoles")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e42)]
    [InlineData(2.5, 2.5e42)]
    public void AmountOfSubstanceFromTeramolesShouldProduceExpectedQuectoMoles(double value, double expected)
    {
        AmountOfSubstance<double> a = AmountOfSubstance<double>.FromTeramoles(value);
        Assert.Equal(expected, a.QuectoMoles, Tolerance);
    }

    [Theory(DisplayName = "AmountOfSubstance.FromPetamoles should produce the expected QuectoMoles")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e45)]
    [InlineData(2.5, 2.5e45)]
    public void AmountOfSubstanceFromPetamolesShouldProduceExpectedQuectoMoles(double value, double expected)
    {
        AmountOfSubstance<double> a = AmountOfSubstance<double>.FromPetamoles(value);
        Assert.Equal(expected, a.QuectoMoles, Tolerance);
    }

    [Theory(DisplayName = "AmountOfSubstance.FromExamoles should produce the expected QuectoMoles")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e48)]
    [InlineData(2.5, 2.5e48)]
    public void AmountOfSubstanceFromExamolesShouldProduceExpectedQuectoMoles(double value, double expected)
    {
        AmountOfSubstance<double> a = AmountOfSubstance<double>.FromExamoles(value);
        Assert.Equal(expected, a.QuectoMoles, Tolerance);
    }

    [Theory(DisplayName = "AmountOfSubstance.FromZettamoles should produce the expected QuectoMoles")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e51)]
    [InlineData(2.5, 2.5e51)]
    public void AmountOfSubstanceFromZettamolesShouldProduceExpectedQuectoMoles(double value, double expected)
    {
        AmountOfSubstance<double> a = AmountOfSubstance<double>.FromZettamoles(value);
        Assert.Equal(expected, a.QuectoMoles, Tolerance);
    }

    [Theory(DisplayName = "AmountOfSubstance.FromYottamoles should produce the expected QuectoMoles")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e54)]
    [InlineData(2.5, 2.5e54)]
    public void AmountOfSubstanceFromYottamolesShouldProduceExpectedQuectoMoles(double value, double expected)
    {
        AmountOfSubstance<double> a = AmountOfSubstance<double>.FromYottamoles(value);
        Assert.Equal(expected, a.QuectoMoles, Tolerance);
    }

    [Theory(DisplayName = "AmountOfSubstance.FromRonnamoles should produce the expected QuectoMoles")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e57)]
    [InlineData(2.5, 2.5e57)]
    public void AmountOfSubstanceFromRonnamolesShouldProduceExpectedQuectoMoles(double value, double expected)
    {
        AmountOfSubstance<double> a = AmountOfSubstance<double>.FromRonnamoles(value);
        Assert.Equal(expected, a.QuectoMoles, Tolerance);
    }

    [Theory(DisplayName = "AmountOfSubstance.FromQuettamoles should produce the expected QuectoMoles")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e60)]
    [InlineData(2.5, 2.5e60)]
    public void AmountOfSubstanceFromQuettamolesShouldProduceExpectedQuectoMoles(double value, double expected)
    {
        AmountOfSubstance<double> a = AmountOfSubstance<double>.FromQuettamoles(value);
        Assert.Equal(expected, a.QuectoMoles, Tolerance);
    }

    [Fact(DisplayName = "AmountOfSubstance.Add should produce the expected result")]
    public void AmountOfSubstanceAddShouldProduceExpectedValue()
    {
        // Given
        AmountOfSubstance<double> left = AmountOfSubstance<double>.FromMoles(1.5);
        AmountOfSubstance<double> right = AmountOfSubstance<double>.FromMoles(0.5);

        // When
        AmountOfSubstance<double> result = left.Add(right);

        // Then
        Assert.Equal(2.0, result.Moles, Tolerance);
    }

    [Fact(DisplayName = "AmountOfSubstance.Subtract should produce the expected result")]
    public void AmountOfSubstanceSubtractShouldProduceExpectedValue()
    {
        // Given
        AmountOfSubstance<double> left = AmountOfSubstance<double>.FromMoles(1.5);
        AmountOfSubstance<double> right = AmountOfSubstance<double>.FromMoles(0.4);

        // When
        AmountOfSubstance<double> result = left.Subtract(right);

        // Then
        Assert.Equal(1.1, result.Moles, Tolerance);
    }

    [Fact(DisplayName = "AmountOfSubstance.Multiply should produce the expected result")]
    public void AmountOfSubstanceMultiplyShouldProduceExpectedValue()
    {
        // Given
        AmountOfSubstance<double> left = AmountOfSubstance<double>.FromMoles(10.0);  // 1e31 qmol
        AmountOfSubstance<double> right = AmountOfSubstance<double>.FromMoles(3.0);  // 3e30 qmol

        // When
        AmountOfSubstance<double> result = left.Multiply(right);  // 1e31 * 3e30 = 3e61 qmol

        // Then
        Assert.Equal(1e31, left.QuectoMoles, Tolerance);
        Assert.Equal(3e30, right.QuectoMoles, Tolerance);
        Assert.Equal(3e61, result.QuectoMoles, Tolerance);
    }

    [Fact(DisplayName = "AmountOfSubstance.Divide should produce the expected result")]
    public void AmountOfSubstanceDivideShouldProduceExpectedValue()
    {
        // Given
        AmountOfSubstance<double> left = AmountOfSubstance<double>.FromMoles(100.0);  // 1e32 qmol
        AmountOfSubstance<double> right = AmountOfSubstance<double>.FromMoles(20.0);  // 2e31 qmol

        // When
        AmountOfSubstance<double> result = left.Divide(right);  // 1e32 / 2e31 = 5 qmol

        // Then
        Assert.Equal(5.0, result.QuectoMoles, Tolerance);
        Assert.Equal(5e-30, result.Moles, Tolerance);
    }

    [Fact(DisplayName = "AmountOfSubstance comparison should produce the expected result (left equal to right)")]
    public void AmountOfSubstanceComparisonShouldProduceExpectedResultLeftEqualToRight()
    {
        // Given
        AmountOfSubstance<double> left = AmountOfSubstance<double>.FromMoles(123.0);
        AmountOfSubstance<double> right = AmountOfSubstance<double>.FromMoles(123.0);

        // When / Then
        Assert.Equal(0, AmountOfSubstance<double>.Compare(left, right));
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
        AmountOfSubstance<double> left = AmountOfSubstance<double>.FromMoles(456.0);
        AmountOfSubstance<double> right = AmountOfSubstance<double>.FromMoles(123.0);

        // When / Then
        Assert.Equal(1, AmountOfSubstance<double>.Compare(left, right));
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
        AmountOfSubstance<double> left = AmountOfSubstance<double>.FromMoles(123.0);
        AmountOfSubstance<double> right = AmountOfSubstance<double>.FromMoles(456.0);

        // When / Then
        Assert.Equal(-1, AmountOfSubstance<double>.Compare(left, right));
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
        // Given: 2 mol = 2000 mmol
        AmountOfSubstance<BigDecimal> left = AmountOfSubstance<BigDecimal>.FromMoles(2.0);
        AmountOfSubstance<BigDecimal> right = AmountOfSubstance<BigDecimal>.FromMillimoles(2000.0);

        // When / Then
        Assert.True(AmountOfSubstance<BigDecimal>.Equals(left, right));
        Assert.True(left.Equals(right));
        Assert.True(left.Equals((object)right));
        Assert.True(left == right);
        Assert.False(left != right);
    }

    [Fact(DisplayName = "AmountOfSubstance equality should produce the expected result (left not equal to right)")]
    public void AmountOfSubstanceEqualityShouldProduceExpectedResultLeftNotEqualToRight()
    {
        // Given
        AmountOfSubstance<double> left = AmountOfSubstance<double>.FromMoles(2.0);
        AmountOfSubstance<double> right = AmountOfSubstance<double>.FromMillimoles(2500.0);

        // When / Then
        Assert.False(AmountOfSubstance<double>.Equals(left, right));
        Assert.False(left.Equals(right));
        Assert.False(left.Equals((object)right));
        Assert.False(left == right);
        Assert.True(left != right);
    }

    [Fact(DisplayName = "AmountOfSubstance.ToString should produce the expected result")]
    public void AmountOfSubstanceToStringShouldProduceExpectedResult()
    {
        // Given
        AmountOfSubstance<double> a = AmountOfSubstance<double>.FromMoles(1000.0);

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
        AmountOfSubstance<double> a = AmountOfSubstance<double>.FromMoles(1.0);

        // Then
        Assert.Equal("0.000001 Mmol", $"{a:Mmol6}"); // mega
        Assert.Equal("1,000.000 mmol", $"{a:mmol3}"); // milli
    }

    [Fact(DisplayName = "AmountOfSubstance.ToString Pmol vs pmol are case-sensitive")]
    public void AmountOfSubstanceToStringPmolVsPmolAreCaseSensitive()
    {
        // Given
        AmountOfSubstance<double> a = AmountOfSubstance<double>.FromMoles(1.0);

        // Then
        Assert.Equal("0.000000000000001 Pmol", $"{a:Pmol15}"); // peta
        Assert.Equal("1,000,000,000,000.000 pmol", $"{a:pmol3}"); // pico
    }

    [Fact(DisplayName = "AmountOfSubstance.ToString µmol symbol should differ from format specifier")]
    public void AmountOfSubstanceToStringMicromolesSymbolShouldDifferFromFormatSpecifier()
    {
        // Given
        AmountOfSubstance<double> a = AmountOfSubstance<double>.FromMoles(1.0);

        // Then: specifier is umol, but symbol rendered is µmol
        Assert.Equal("1,000,000.000 µmol", $"{a:umol3}");
    }

    [Fact(DisplayName = "AmountOfSubstance.ToString should honor custom culture separators")]
    public void AmountOfSubstanceToStringShouldHonorCustomCulture()
    {
        // Given
        CultureInfo customCulture = new("de-DE");
        AmountOfSubstance<double> a = AmountOfSubstance<double>.FromMoles(1234.56);

        // When
        string formatted = a.ToString("mol2", customCulture);

        // Then
        Assert.Equal("1.234,56 mol", formatted);
    }
}
