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

public sealed class ResistanceTests
{
    // IEEE-754 binary floating-point arithmetic causes small discrepancies in calculation, therefore we need a tolerance.
    private const double Tolerance = 1e+42;

    [Fact(DisplayName = "Resistance.Zero should produce the expected result")]
    public void ResistanceZeroShouldProduceExpectedResult()
    {
        // Given / When
        Resistance<double> resistance = Resistance<double>.Zero;

        // Then
        Assert.Equal(0.0, resistance.QuectoOhms, Tolerance);
    }

    [Theory(DisplayName = "Resistance.FromQuectoohms should produce the expected QuectoOhms")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1.0)]
    [InlineData(2.5, 2.5)]
    public void ResistanceFromQuectoohmsShouldProduceExpectedQuectoOhms(double value, double expected)
    {
        Resistance<double> r = Resistance<double>.FromQuectoohms(value);
        Assert.Equal(expected, r.QuectoOhms, Tolerance);
    }

    [Theory(DisplayName = "Resistance.FromRontoohms should produce the expected QuectoOhms")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e3)]
    [InlineData(2.5, 2.5e3)]
    public void ResistanceFromRontoohmsShouldProduceExpectedQuectoOhms(double value, double expected)
    {
        Resistance<double> r = Resistance<double>.FromRontoohms(value);
        Assert.Equal(expected, r.QuectoOhms, Tolerance);
    }

    [Theory(DisplayName = "Resistance.FromYoctoohms should produce the expected QuectoOhms")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e6)]
    [InlineData(2.5, 2.5e6)]
    public void ResistanceFromYoctoohmsShouldProduceExpectedQuectoOhms(double value, double expected)
    {
        Resistance<double> r = Resistance<double>.FromYoctoohms(value);
        Assert.Equal(expected, r.QuectoOhms, Tolerance);
    }

    [Theory(DisplayName = "Resistance.FromZeptoohms should produce the expected QuectoOhms")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e9)]
    [InlineData(2.5, 2.5e9)]
    public void ResistanceFromZeptoohmsShouldProduceExpectedQuectoOhms(double value, double expected)
    {
        Resistance<double> r = Resistance<double>.FromZeptoohms(value);
        Assert.Equal(expected, r.QuectoOhms, Tolerance);
    }

    [Theory(DisplayName = "Resistance.FromAttoohms should produce the expected QuectoOhms")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e12)]
    [InlineData(2.5, 2.5e12)]
    public void ResistanceFromAttoohmsShouldProduceExpectedQuectoOhms(double value, double expected)
    {
        Resistance<double> r = Resistance<double>.FromAttoohms(value);
        Assert.Equal(expected, r.QuectoOhms, Tolerance);
    }

    [Theory(DisplayName = "Resistance.FromFemtoohms should produce the expected QuectoOhms")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e15)]
    [InlineData(2.5, 2.5e15)]
    public void ResistanceFromFemtoohmsShouldProduceExpectedQuectoOhms(double value, double expected)
    {
        Resistance<double> r = Resistance<double>.FromFemtoohms(value);
        Assert.Equal(expected, r.QuectoOhms, Tolerance);
    }

    [Theory(DisplayName = "Resistance.FromPicoohms should produce the expected QuectoOhms")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e18)]
    [InlineData(2.5, 2.5e18)]
    public void ResistanceFromPicoohmsShouldProduceExpectedQuectoOhms(double value, double expected)
    {
        Resistance<double> r = Resistance<double>.FromPicoohms(value);
        Assert.Equal(expected, r.QuectoOhms, Tolerance);
    }

    [Theory(DisplayName = "Resistance.FromNanoohms should produce the expected QuectoOhms")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e21)]
    [InlineData(2.5, 2.5e21)]
    public void ResistanceFromNanoohmsShouldProduceExpectedQuectoOhms(double value, double expected)
    {
        Resistance<double> r = Resistance<double>.FromNanoohms(value);
        Assert.Equal(expected, r.QuectoOhms, Tolerance);
    }

    [Theory(DisplayName = "Resistance.FromMicroohms should produce the expected QuectoOhms")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e24)]
    [InlineData(2.5, 2.5e24)]
    public void ResistanceFromMicroohmsShouldProduceExpectedQuectoOhms(double value, double expected)
    {
        Resistance<double> r = Resistance<double>.FromMicroohms(value);
        Assert.Equal(expected, r.QuectoOhms, Tolerance);
    }

    [Theory(DisplayName = "Resistance.FromMilliohms should produce the expected QuectoOhms")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e27)]
    [InlineData(2.5, 2.5e27)]
    public void ResistanceFromMilliohmsShouldProduceExpectedQuectoOhms(double value, double expected)
    {
        Resistance<double> r = Resistance<double>.FromMilliohms(value);
        Assert.Equal(expected, r.QuectoOhms, Tolerance);
    }

    [Theory(DisplayName = "Resistance.FromCentiohms should produce the expected QuectoOhms")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e28)]
    [InlineData(2.5, 2.5e28)]
    public void ResistanceFromCentiohmsShouldProduceExpectedQuectoOhms(double value, double expected)
    {
        Resistance<double> r = Resistance<double>.FromCentiohms(value);
        Assert.Equal(expected, r.QuectoOhms, Tolerance);
    }

    [Theory(DisplayName = "Resistance.FromDeciohms should produce the expected QuectoOhms")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e29)]
    [InlineData(2.5, 2.5e29)]
    public void ResistanceFromDeciohmsShouldProduceExpectedQuectoOhms(double value, double expected)
    {
        Resistance<double> r = Resistance<double>.FromDeciohms(value);
        Assert.Equal(expected, r.QuectoOhms, Tolerance);
    }

    [Theory(DisplayName = "Resistance.FromOhms should produce the expected QuectoOhms")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e30)]
    [InlineData(2.5, 2.5e30)]
    public void ResistanceFromOhmsShouldProduceExpectedQuectoOhms(double value, double expected)
    {
        Resistance<double> r = Resistance<double>.FromOhms(value);
        Assert.Equal(expected, r.QuectoOhms, Tolerance);
    }

    [Theory(DisplayName = "Resistance.FromDecaohms should produce the expected QuectoOhms")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e31)]
    [InlineData(2.5, 2.5e31)]
    public void ResistanceFromDecaohmsShouldProduceExpectedQuectoOhms(double value, double expected)
    {
        Resistance<double> r = Resistance<double>.FromDecaohms(value);
        Assert.Equal(expected, r.QuectoOhms, Tolerance);
    }

    [Theory(DisplayName = "Resistance.FromHectoohms should produce the expected QuectoOhms")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e32)]
    [InlineData(2.5, 2.5e32)]
    public void ResistanceFromHectoohmsShouldProduceExpectedQuectoOhms(double value, double expected)
    {
        Resistance<double> r = Resistance<double>.FromHectoohms(value);
        Assert.Equal(expected, r.QuectoOhms, Tolerance);
    }

    [Theory(DisplayName = "Resistance.FromKiloohms should produce the expected QuectoOhms")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e33)]
    [InlineData(2.5, 2.5e33)]
    public void ResistanceFromKiloohmsShouldProduceExpectedQuectoOhms(double value, double expected)
    {
        Resistance<double> r = Resistance<double>.FromKiloohms(value);
        Assert.Equal(expected, r.QuectoOhms, Tolerance);
    }

    [Theory(DisplayName = "Resistance.FromMegaohms should produce the expected QuectoOhms")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e36)]
    [InlineData(2.5, 2.5e36)]
    public void ResistanceFromMegaohmsShouldProduceExpectedQuectoOhms(double value, double expected)
    {
        Resistance<double> r = Resistance<double>.FromMegaohms(value);
        Assert.Equal(expected, r.QuectoOhms, Tolerance);
    }

    [Theory(DisplayName = "Resistance.FromGigaohms should produce the expected QuectoOhms")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e39)]
    [InlineData(2.5, 2.5e39)]
    public void ResistanceFromGigaohmsShouldProduceExpectedQuectoOhms(double value, double expected)
    {
        Resistance<double> r = Resistance<double>.FromGigaohms(value);
        Assert.Equal(expected, r.QuectoOhms, Tolerance);
    }

    [Theory(DisplayName = "Resistance.FromTeraohms should produce the expected QuectoOhms")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e42)]
    [InlineData(2.5, 2.5e42)]
    public void ResistanceFromTeraohmsShouldProduceExpectedQuectoOhms(double value, double expected)
    {
        Resistance<double> r = Resistance<double>.FromTeraohms(value);
        Assert.Equal(expected, r.QuectoOhms, Tolerance);
    }

    [Theory(DisplayName = "Resistance.FromPetaohms should produce the expected QuectoOhms")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e45)]
    [InlineData(2.5, 2.5e45)]
    public void ResistanceFromPetaohmsShouldProduceExpectedQuectoOhms(double value, double expected)
    {
        Resistance<double> r = Resistance<double>.FromPetaohms(value);
        Assert.Equal(expected, r.QuectoOhms, Tolerance);
    }

    [Theory(DisplayName = "Resistance.FromExaohms should produce the expected QuectoOhms")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e48)]
    [InlineData(2.5, 2.5e48)]
    public void ResistanceFromExaohmsShouldProduceExpectedQuectoOhms(double value, double expected)
    {
        Resistance<double> r = Resistance<double>.FromExaohms(value);
        Assert.Equal(expected, r.QuectoOhms, Tolerance);
    }

    [Theory(DisplayName = "Resistance.FromZettaohms should produce the expected QuectoOhms")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e51)]
    [InlineData(2.5, 2.5e51)]
    public void ResistanceFromZettaohmsShouldProduceExpectedQuectoOhms(double value, double expected)
    {
        Resistance<double> r = Resistance<double>.FromZettaohms(value);
        Assert.Equal(expected, r.QuectoOhms, Tolerance);
    }

    [Theory(DisplayName = "Resistance.FromYottaohms should produce the expected QuectoOhms")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e54)]
    [InlineData(2.5, 2.5e54)]
    public void ResistanceFromYottaohmsShouldProduceExpectedQuectoOhms(double value, double expected)
    {
        Resistance<double> r = Resistance<double>.FromYottaohms(value);
        Assert.Equal(expected, r.QuectoOhms, Tolerance);
    }

    [Theory(DisplayName = "Resistance.FromRonnaohms should produce the expected QuectoOhms")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e57)]
    [InlineData(2.5, 2.5e57)]
    public void ResistanceFromRonnaohmsShouldProduceExpectedQuectoOhms(double value, double expected)
    {
        Resistance<double> r = Resistance<double>.FromRonnaohms(value);
        Assert.Equal(expected, r.QuectoOhms, Tolerance);
    }

    [Theory(DisplayName = "Resistance.FromQuettaohms should produce the expected QuectoOhms")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e60)]
    [InlineData(2.5, 2.5e60)]
    public void ResistanceFromQuettaohmsShouldProduceExpectedQuectoOhms(double value, double expected)
    {
        Resistance<double> r = Resistance<double>.FromQuettaohms(value);
        Assert.Equal(expected, r.QuectoOhms, Tolerance);
    }

    [Fact(DisplayName = "Resistance.Add should produce the expected result")]
    public void ResistanceAddShouldProduceExpectedValue()
    {
        // Given
        Resistance<double> left = Resistance<double>.FromOhms(1.5);
        Resistance<double> right = Resistance<double>.FromOhms(0.5);

        // When
        Resistance<double> result = left.Add(right);

        // Then
        Assert.Equal(2.0, result.Ohms, Tolerance);
    }

    [Fact(DisplayName = "Resistance.Subtract should produce the expected result")]
    public void ResistanceSubtractShouldProduceExpectedValue()
    {
        // Given
        Resistance<double> left = Resistance<double>.FromOhms(1.5);
        Resistance<double> right = Resistance<double>.FromOhms(0.4);

        // When
        Resistance<double> result = left.Subtract(right);

        // Then
        Assert.Equal(1.1, result.Ohms, Tolerance);
    }

    [Fact(DisplayName = "Resistance.Multiply should produce the expected result")]
    public void ResistanceMultiplyShouldProduceExpectedValue()
    {
        // Given
        Resistance<double> left = Resistance<double>.FromOhms(10.0);  // 1e31 qΩ
        Resistance<double> right = Resistance<double>.FromOhms(3.0);  // 3e30 qΩ

        // When
        Resistance<double> result = left.Multiply(right);  // 1e31 * 3e30 = 3e61 qΩ

        // Then
        Assert.Equal(1e31, left.QuectoOhms, Tolerance);
        Assert.Equal(3e30, right.QuectoOhms, Tolerance);
        Assert.Equal(3e61, result.QuectoOhms, Tolerance);
    }

    [Fact(DisplayName = "Resistance.Divide should produce the expected result")]
    public void ResistanceDivideShouldProduceExpectedValue()
    {
        // Given
        Resistance<double> left = Resistance<double>.FromOhms(100.0);  // 1e32 qΩ
        Resistance<double> right = Resistance<double>.FromOhms(20.0);  // 2e31 qΩ

        // When
        Resistance<double> result = left.Divide(right);  // 1e32 / 2e31 = 5 qΩ

        // Then
        Assert.Equal(5.0, result.QuectoOhms, Tolerance);
        Assert.Equal(5e-30, result.Ohms, Tolerance);
    }

    [Fact(DisplayName = "Resistance comparison should produce the expected result (left equal to right)")]
    public void ResistanceComparisonShouldProduceExpectedResultLeftEqualToRight()
    {
        // Given
        Resistance<double> left = Resistance<double>.FromOhms(123.0);
        Resistance<double> right = Resistance<double>.FromOhms(123.0);

        // When / Then
        Assert.Equal(0, Resistance<double>.Compare(left, right));
        Assert.Equal(0, left.CompareTo(right));
        Assert.Equal(0, left.CompareTo((object)right));
        Assert.False(left > right);
        Assert.True(left >= right);
        Assert.False(left < right);
        Assert.True(left <= right);
    }

    [Fact(DisplayName = "Resistance comparison should produce the expected result (left greater than right)")]
    public void ResistanceComparisonShouldProduceExpectedLeftGreaterThanRight()
    {
        // Given
        Resistance<double> left = Resistance<double>.FromOhms(456.0);
        Resistance<double> right = Resistance<double>.FromOhms(123.0);

        // When / Then
        Assert.Equal(1, Resistance<double>.Compare(left, right));
        Assert.Equal(1, left.CompareTo(right));
        Assert.Equal(1, left.CompareTo((object)right));
        Assert.True(left > right);
        Assert.True(left >= right);
        Assert.False(left < right);
        Assert.False(left <= right);
    }

    [Fact(DisplayName = "Resistance comparison should produce the expected result (left less than right)")]
    public void ResistanceComparisonShouldProduceExpectedLeftLessThanRight()
    {
        // Given
        Resistance<double> left = Resistance<double>.FromOhms(123.0);
        Resistance<double> right = Resistance<double>.FromOhms(456.0);

        // When / Then
        Assert.Equal(-1, Resistance<double>.Compare(left, right));
        Assert.Equal(-1, left.CompareTo(right));
        Assert.Equal(-1, left.CompareTo((object)right));
        Assert.False(left > right);
        Assert.False(left >= right);
        Assert.True(left < right);
        Assert.True(left <= right);
    }

    [Fact(DisplayName = "Resistance equality should produce the expected result (left equal to right)")]
    public void ResistanceEqualityShouldProduceExpectedResultLeftEqualToRight()
    {
        // Given: 2 Ω = 2000 mΩ
        Resistance<BigDecimal> left = Resistance<BigDecimal>.FromOhms(2.0);
        Resistance<BigDecimal> right = Resistance<BigDecimal>.FromMilliohms(2000.0);

        // When / Then
        Assert.True(Resistance<BigDecimal>.Equals(left, right));
        Assert.True(left.Equals(right));
        Assert.True(left.Equals((object)right));
        Assert.True(left == right);
        Assert.False(left != right);
    }

    [Fact(DisplayName = "Resistance equality should produce the expected result (left not equal to right)")]
    public void ResistanceEqualityShouldProduceExpectedResultLeftNotEqualToRight()
    {
        // Given
        Resistance<double> left = Resistance<double>.FromOhms(2.0);
        Resistance<double> right = Resistance<double>.FromMilliohms(2500.0);

        // When / Then
        Assert.False(Resistance<double>.Equals(left, right));
        Assert.False(left.Equals(right));
        Assert.False(left.Equals((object)right));
        Assert.False(left == right);
        Assert.True(left != right);
    }

    [Fact(DisplayName = "Resistance.ToString should produce the expected result")]
    public void ResistanceToStringShouldProduceExpectedResult()
    {
        // Given
        Resistance<double> r = Resistance<double>.FromOhms(1000.0);

        // When / Then
        Assert.Equal("1,000.000 Ω", $"{r:ohm3}");
        Assert.Equal("1.000 kΩ", $"{r:kohm3}");
        Assert.Equal("0.001 MΩ", $"{r:Mohm3}");
        Assert.Equal("1,000,000.000 mΩ", $"{r:mohm3}");
    }

    [Fact(DisplayName = "Resistance.ToString Mohm vs mohm are case-sensitive")]
    public void ResistanceToStringMohmVsMohmAreCaseSensitive()
    {
        // Given
        Resistance<double> r = Resistance<double>.FromOhms(1.0);

        // Then
        Assert.Equal("0.000001 MΩ", $"{r:Mohm6}"); // mega
        Assert.Equal("1,000.000 mΩ", $"{r:mohm3}"); // milli
    }

    [Fact(DisplayName = "Resistance.ToString Pohm vs pohm are case-sensitive")]
    public void ResistanceToStringPohmVsPohmAreCaseSensitive()
    {
        // Given
        Resistance<double> r = Resistance<double>.FromOhms(1.0);

        // Then
        Assert.Equal("0.000000000000001 PΩ", $"{r:Pohm15}"); // peta
        Assert.Equal("1,000,000,000,000.000 pΩ", $"{r:pohm3}"); // pico
    }

    [Fact(DisplayName = "Resistance.ToString µΩ symbol should differ from format specifier")]
    public void ResistanceToStringMicroohmsSymbolShouldDifferFromFormatSpecifier()
    {
        // Given
        Resistance<double> r = Resistance<double>.FromOhms(1.0);

        // Then: specifier is uohm, but symbol rendered is µΩ
        Assert.Equal("1,000,000.000 µΩ", $"{r:uohm3}");
    }

    [Fact(DisplayName = "Resistance.ToString should honor custom culture separators")]
    public void ResistanceToStringShouldHonorCustomCulture()
    {
        // Given
        CultureInfo customCulture = new("de-DE");
        Resistance<double> r = Resistance<double>.FromOhms(1234.56);

        // When
        string formatted = r.ToString("ohm2", customCulture);

        // Then
        Assert.Equal("1.234,56 Ω", formatted);
    }
}
