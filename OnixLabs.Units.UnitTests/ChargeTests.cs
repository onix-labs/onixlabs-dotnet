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

public sealed class ChargeTests
{
    // IEEE-754 binary floating-point arithmetic causes small discrepancies in calculation, therefore we need a tolerance.
    private const double Tolerance = 1e+42;

    [Fact(DisplayName = "Charge.Zero should produce the expected result")]
    public void ChargeZeroShouldProduceExpectedResult()
    {
        // Given / When
        Charge<double> charge = Charge<double>.Zero;

        // Then
        Assert.Equal(0.0, charge.QuectoCoulombs, Tolerance);
    }

    [Theory(DisplayName = "Charge.FromQuectocoulombs should produce the expected QuectoCoulombs")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1.0)]
    [InlineData(2.5, 2.5)]
    public void ChargeFromQuectocoulombsShouldProduceExpectedQuectoCoulombs(double value, double expected)
    {
        Charge<double> c = Charge<double>.FromQuectocoulombs(value);
        Assert.Equal(expected, c.QuectoCoulombs, Tolerance);
    }

    [Theory(DisplayName = "Charge.FromRontocoulombs should produce the expected QuectoCoulombs")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e3)]
    [InlineData(2.5, 2.5e3)]
    public void ChargeFromRontocoulombsShouldProduceExpectedQuectoCoulombs(double value, double expected)
    {
        Charge<double> c = Charge<double>.FromRontocoulombs(value);
        Assert.Equal(expected, c.QuectoCoulombs, Tolerance);
    }

    [Theory(DisplayName = "Charge.FromYoctocoulombs should produce the expected QuectoCoulombs")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e6)]
    [InlineData(2.5, 2.5e6)]
    public void ChargeFromYoctocoulombsShouldProduceExpectedQuectoCoulombs(double value, double expected)
    {
        Charge<double> c = Charge<double>.FromYoctocoulombs(value);
        Assert.Equal(expected, c.QuectoCoulombs, Tolerance);
    }

    [Theory(DisplayName = "Charge.FromZeptocoulombs should produce the expected QuectoCoulombs")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e9)]
    [InlineData(2.5, 2.5e9)]
    public void ChargeFromZeptocoulombsShouldProduceExpectedQuectoCoulombs(double value, double expected)
    {
        Charge<double> c = Charge<double>.FromZeptocoulombs(value);
        Assert.Equal(expected, c.QuectoCoulombs, Tolerance);
    }

    [Theory(DisplayName = "Charge.FromAttocoulombs should produce the expected QuectoCoulombs")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e12)]
    [InlineData(2.5, 2.5e12)]
    public void ChargeFromAttocoulombsShouldProduceExpectedQuectoCoulombs(double value, double expected)
    {
        Charge<double> c = Charge<double>.FromAttocoulombs(value);
        Assert.Equal(expected, c.QuectoCoulombs, Tolerance);
    }

    [Theory(DisplayName = "Charge.FromFemtocoulombs should produce the expected QuectoCoulombs")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e15)]
    [InlineData(2.5, 2.5e15)]
    public void ChargeFromFemtocoulombsShouldProduceExpectedQuectoCoulombs(double value, double expected)
    {
        Charge<double> c = Charge<double>.FromFemtocoulombs(value);
        Assert.Equal(expected, c.QuectoCoulombs, Tolerance);
    }

    [Theory(DisplayName = "Charge.FromPicocoulombs should produce the expected QuectoCoulombs")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e18)]
    [InlineData(2.5, 2.5e18)]
    public void ChargeFromPicocoulombsShouldProduceExpectedQuectoCoulombs(double value, double expected)
    {
        Charge<double> c = Charge<double>.FromPicocoulombs(value);
        Assert.Equal(expected, c.QuectoCoulombs, Tolerance);
    }

    [Theory(DisplayName = "Charge.FromNanocoulombs should produce the expected QuectoCoulombs")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e21)]
    [InlineData(2.5, 2.5e21)]
    public void ChargeFromNanocoulombsShouldProduceExpectedQuectoCoulombs(double value, double expected)
    {
        Charge<double> c = Charge<double>.FromNanocoulombs(value);
        Assert.Equal(expected, c.QuectoCoulombs, Tolerance);
    }

    [Theory(DisplayName = "Charge.FromMicrocoulombs should produce the expected QuectoCoulombs")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e24)]
    [InlineData(2.5, 2.5e24)]
    public void ChargeFromMicrocoulombsShouldProduceExpectedQuectoCoulombs(double value, double expected)
    {
        Charge<double> c = Charge<double>.FromMicrocoulombs(value);
        Assert.Equal(expected, c.QuectoCoulombs, Tolerance);
    }

    [Theory(DisplayName = "Charge.FromMillicoulombs should produce the expected QuectoCoulombs")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e27)]
    [InlineData(2.5, 2.5e27)]
    public void ChargeFromMillicoulombsShouldProduceExpectedQuectoCoulombs(double value, double expected)
    {
        Charge<double> c = Charge<double>.FromMillicoulombs(value);
        Assert.Equal(expected, c.QuectoCoulombs, Tolerance);
    }

    [Theory(DisplayName = "Charge.FromCenticoulombs should produce the expected QuectoCoulombs")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e28)]
    [InlineData(2.5, 2.5e28)]
    public void ChargeFromCenticoulombsShouldProduceExpectedQuectoCoulombs(double value, double expected)
    {
        Charge<double> c = Charge<double>.FromCenticoulombs(value);
        Assert.Equal(expected, c.QuectoCoulombs, Tolerance);
    }

    [Theory(DisplayName = "Charge.FromDecicoulombs should produce the expected QuectoCoulombs")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e29)]
    [InlineData(2.5, 2.5e29)]
    public void ChargeFromDecicoulombsShouldProduceExpectedQuectoCoulombs(double value, double expected)
    {
        Charge<double> c = Charge<double>.FromDecicoulombs(value);
        Assert.Equal(expected, c.QuectoCoulombs, Tolerance);
    }

    [Theory(DisplayName = "Charge.FromCoulombs should produce the expected QuectoCoulombs")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e30)]
    [InlineData(2.5, 2.5e30)]
    public void ChargeFromCoulombsShouldProduceExpectedQuectoCoulombs(double value, double expected)
    {
        Charge<double> c = Charge<double>.FromCoulombs(value);
        Assert.Equal(expected, c.QuectoCoulombs, Tolerance);
    }

    [Theory(DisplayName = "Charge.FromDecacoulombs should produce the expected QuectoCoulombs")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e31)]
    [InlineData(2.5, 2.5e31)]
    public void ChargeFromDecacoulombsShouldProduceExpectedQuectoCoulombs(double value, double expected)
    {
        Charge<double> c = Charge<double>.FromDecacoulombs(value);
        Assert.Equal(expected, c.QuectoCoulombs, Tolerance);
    }

    [Theory(DisplayName = "Charge.FromHectocoulombs should produce the expected QuectoCoulombs")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e32)]
    [InlineData(2.5, 2.5e32)]
    public void ChargeFromHectocoulombsShouldProduceExpectedQuectoCoulombs(double value, double expected)
    {
        Charge<double> c = Charge<double>.FromHectocoulombs(value);
        Assert.Equal(expected, c.QuectoCoulombs, Tolerance);
    }

    [Theory(DisplayName = "Charge.FromKilocoulombs should produce the expected QuectoCoulombs")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e33)]
    [InlineData(2.5, 2.5e33)]
    public void ChargeFromKilocoulombsShouldProduceExpectedQuectoCoulombs(double value, double expected)
    {
        Charge<double> c = Charge<double>.FromKilocoulombs(value);
        Assert.Equal(expected, c.QuectoCoulombs, Tolerance);
    }

    [Theory(DisplayName = "Charge.FromMegacoulombs should produce the expected QuectoCoulombs")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e36)]
    [InlineData(2.5, 2.5e36)]
    public void ChargeFromMegacoulombsShouldProduceExpectedQuectoCoulombs(double value, double expected)
    {
        Charge<double> c = Charge<double>.FromMegacoulombs(value);
        Assert.Equal(expected, c.QuectoCoulombs, Tolerance);
    }

    [Theory(DisplayName = "Charge.FromGigacoulombs should produce the expected QuectoCoulombs")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e39)]
    [InlineData(2.5, 2.5e39)]
    public void ChargeFromGigacoulombsShouldProduceExpectedQuectoCoulombs(double value, double expected)
    {
        Charge<double> c = Charge<double>.FromGigacoulombs(value);
        Assert.Equal(expected, c.QuectoCoulombs, Tolerance);
    }

    [Theory(DisplayName = "Charge.FromTeracoulombs should produce the expected QuectoCoulombs")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e42)]
    [InlineData(2.5, 2.5e42)]
    public void ChargeFromTeracoulombsShouldProduceExpectedQuectoCoulombs(double value, double expected)
    {
        Charge<double> c = Charge<double>.FromTeracoulombs(value);
        Assert.Equal(expected, c.QuectoCoulombs, Tolerance);
    }

    [Theory(DisplayName = "Charge.FromPetacoulombs should produce the expected QuectoCoulombs")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e45)]
    [InlineData(2.5, 2.5e45)]
    public void ChargeFromPetacoulombsShouldProduceExpectedQuectoCoulombs(double value, double expected)
    {
        Charge<double> c = Charge<double>.FromPetacoulombs(value);
        Assert.Equal(expected, c.QuectoCoulombs, Tolerance);
    }

    [Theory(DisplayName = "Charge.FromExacoulombs should produce the expected QuectoCoulombs")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e48)]
    [InlineData(2.5, 2.5e48)]
    public void ChargeFromExacoulombsShouldProduceExpectedQuectoCoulombs(double value, double expected)
    {
        Charge<double> c = Charge<double>.FromExacoulombs(value);
        Assert.Equal(expected, c.QuectoCoulombs, Tolerance);
    }

    [Theory(DisplayName = "Charge.FromZettacoulombs should produce the expected QuectoCoulombs")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e51)]
    [InlineData(2.5, 2.5e51)]
    public void ChargeFromZettacoulombsShouldProduceExpectedQuectoCoulombs(double value, double expected)
    {
        Charge<double> c = Charge<double>.FromZettacoulombs(value);
        Assert.Equal(expected, c.QuectoCoulombs, Tolerance);
    }

    [Theory(DisplayName = "Charge.FromYottacoulombs should produce the expected QuectoCoulombs")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e54)]
    [InlineData(2.5, 2.5e54)]
    public void ChargeFromYottacoulombsShouldProduceExpectedQuectoCoulombs(double value, double expected)
    {
        Charge<double> c = Charge<double>.FromYottacoulombs(value);
        Assert.Equal(expected, c.QuectoCoulombs, Tolerance);
    }

    [Theory(DisplayName = "Charge.FromRonnacoulombs should produce the expected QuectoCoulombs")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e57)]
    [InlineData(2.5, 2.5e57)]
    public void ChargeFromRonnacoulombsShouldProduceExpectedQuectoCoulombs(double value, double expected)
    {
        Charge<double> c = Charge<double>.FromRonnacoulombs(value);
        Assert.Equal(expected, c.QuectoCoulombs, Tolerance);
    }

    [Theory(DisplayName = "Charge.FromQuettacoulombs should produce the expected QuectoCoulombs")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e60)]
    [InlineData(2.5, 2.5e60)]
    public void ChargeFromQuettacoulombsShouldProduceExpectedQuectoCoulombs(double value, double expected)
    {
        Charge<double> c = Charge<double>.FromQuettacoulombs(value);
        Assert.Equal(expected, c.QuectoCoulombs, Tolerance);
    }

    [Theory(DisplayName = "Charge.FromAmpereHours should produce the expected QuectoCoulombs")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 3.6e33)]
    [InlineData(2.0, 7.2e33)]
    public void ChargeFromAmpereHoursShouldProduceExpectedQuectoCoulombs(double value, double expected)
    {
        Charge<double> c = Charge<double>.FromAmpereHours(value);
        Assert.Equal(expected, c.QuectoCoulombs, Tolerance);
    }

    [Theory(DisplayName = "Charge.FromMilliampereHours should produce the expected QuectoCoulombs")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 3.6e30)]
    [InlineData(1000.0, 3.6e33)] // 1000 mAh = 1 Ah
    public void ChargeFromMilliampereHoursShouldProduceExpectedQuectoCoulombs(double value, double expected)
    {
        Charge<double> c = Charge<double>.FromMilliampereHours(value);
        Assert.Equal(expected, c.QuectoCoulombs, Tolerance);
    }

    [Theory(DisplayName = "Charge.FromElementaryCharges should produce the expected QuectoCoulombs")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1.602176634e11)]
    [InlineData(2.0, 3.204353268e11)]
    public void ChargeFromElementaryChargesShouldProduceExpectedQuectoCoulombs(double value, double expected)
    {
        Charge<double> c = Charge<double>.FromElementaryCharges(value);
        Assert.Equal(expected, c.QuectoCoulombs, Tolerance);
    }

    [Fact(DisplayName = "Charge non-SI conversions should roundtrip through Coulombs")]
    public void ChargeNonSiConversionsShouldRoundtripThroughCoulombs()
    {
        // 1 Ah = 3600 C
        Charge<double> oneAh = Charge<double>.FromCoulombs(3600.0);
        Assert.Equal(1.0, oneAh.AmpereHours, 1e-9);

        // 1 mAh = 3.6 C
        Charge<double> oneMah = Charge<double>.FromCoulombs(3.6);
        Assert.Equal(1.0, oneMah.MilliampereHours, 1e-9);

        // 1 C = 6.241509...e18 e
        Charge<double> oneCoulomb = Charge<double>.FromCoulombs(1.0);
        Assert.Equal(1.0 / 1.602176634e-19, oneCoulomb.ElementaryCharges, 1e3);
    }

    [Fact(DisplayName = "Charge.Add should produce the expected result")]
    public void ChargeAddShouldProduceExpectedValue()
    {
        // Given
        Charge<double> left = Charge<double>.FromCoulombs(1.5);
        Charge<double> right = Charge<double>.FromCoulombs(0.5);

        // When
        Charge<double> result = left.Add(right);

        // Then
        Assert.Equal(2.0, result.Coulombs, Tolerance);
    }

    [Fact(DisplayName = "Charge.Subtract should produce the expected result")]
    public void ChargeSubtractShouldProduceExpectedValue()
    {
        // Given
        Charge<double> left = Charge<double>.FromCoulombs(1.5);
        Charge<double> right = Charge<double>.FromCoulombs(0.4);

        // When
        Charge<double> result = left.Subtract(right);

        // Then
        Assert.Equal(1.1, result.Coulombs, Tolerance);
    }

    [Fact(DisplayName = "Charge.Multiply should produce the expected result")]
    public void ChargeMultiplyShouldProduceExpectedValue()
    {
        // Given
        Charge<double> left = Charge<double>.FromCoulombs(10.0);  // 1e31 qC
        Charge<double> right = Charge<double>.FromCoulombs(3.0);  // 3e30 qC

        // When
        Charge<double> result = left.Multiply(right);  // 1e31 * 3e30 = 3e61 qC

        // Then
        Assert.Equal(1e31, left.QuectoCoulombs, Tolerance);
        Assert.Equal(3e30, right.QuectoCoulombs, Tolerance);
        Assert.Equal(3e61, result.QuectoCoulombs, Tolerance);
    }

    [Fact(DisplayName = "Charge.Divide should produce the expected result")]
    public void ChargeDivideShouldProduceExpectedValue()
    {
        // Given
        Charge<double> left = Charge<double>.FromCoulombs(100.0);  // 1e32 qC
        Charge<double> right = Charge<double>.FromCoulombs(20.0);  // 2e31 qC

        // When
        Charge<double> result = left.Divide(right);  // 1e32 / 2e31 = 5 qC

        // Then
        Assert.Equal(5.0, result.QuectoCoulombs, Tolerance);
        Assert.Equal(5e-30, result.Coulombs, Tolerance);
    }

    [Fact(DisplayName = "Charge comparison should produce the expected result (left equal to right)")]
    public void ChargeComparisonShouldProduceExpectedResultLeftEqualToRight()
    {
        // Given
        Charge<double> left = Charge<double>.FromCoulombs(123.0);
        Charge<double> right = Charge<double>.FromCoulombs(123.0);

        // When / Then
        Assert.Equal(0, Charge<double>.Compare(left, right));
        Assert.Equal(0, left.CompareTo(right));
        Assert.Equal(0, left.CompareTo((object)right));
        Assert.False(left > right);
        Assert.True(left >= right);
        Assert.False(left < right);
        Assert.True(left <= right);
    }

    [Fact(DisplayName = "Charge comparison should produce the expected result (left greater than right)")]
    public void ChargeComparisonShouldProduceExpectedLeftGreaterThanRight()
    {
        // Given
        Charge<double> left = Charge<double>.FromCoulombs(456.0);
        Charge<double> right = Charge<double>.FromCoulombs(123.0);

        // When / Then
        Assert.Equal(1, Charge<double>.Compare(left, right));
        Assert.Equal(1, left.CompareTo(right));
        Assert.Equal(1, left.CompareTo((object)right));
        Assert.True(left > right);
        Assert.True(left >= right);
        Assert.False(left < right);
        Assert.False(left <= right);
    }

    [Fact(DisplayName = "Charge comparison should produce the expected result (left less than right)")]
    public void ChargeComparisonShouldProduceExpectedLeftLessThanRight()
    {
        // Given
        Charge<double> left = Charge<double>.FromCoulombs(123.0);
        Charge<double> right = Charge<double>.FromCoulombs(456.0);

        // When / Then
        Assert.Equal(-1, Charge<double>.Compare(left, right));
        Assert.Equal(-1, left.CompareTo(right));
        Assert.Equal(-1, left.CompareTo((object)right));
        Assert.False(left > right);
        Assert.False(left >= right);
        Assert.True(left < right);
        Assert.True(left <= right);
    }

    [Fact(DisplayName = "Charge equality should produce the expected result (left equal to right)")]
    public void ChargeEqualityShouldProduceExpectedResultLeftEqualToRight()
    {
        // Given: 2 C = 2000 mC
        Charge<BigDecimal> left = Charge<BigDecimal>.FromCoulombs(2.0);
        Charge<BigDecimal> right = Charge<BigDecimal>.FromMillicoulombs(2000.0);

        // When / Then
        Assert.True(Charge<BigDecimal>.Equals(left, right));
        Assert.True(left.Equals(right));
        Assert.True(left.Equals((object)right));
        Assert.True(left == right);
        Assert.False(left != right);
    }

    [Fact(DisplayName = "Charge equality should produce the expected result (left not equal to right)")]
    public void ChargeEqualityShouldProduceExpectedResultLeftNotEqualToRight()
    {
        // Given
        Charge<double> left = Charge<double>.FromCoulombs(2.0);
        Charge<double> right = Charge<double>.FromMillicoulombs(2500.0);

        // When / Then
        Assert.False(Charge<double>.Equals(left, right));
        Assert.False(left.Equals(right));
        Assert.False(left.Equals((object)right));
        Assert.False(left == right);
        Assert.True(left != right);
    }

    [Fact(DisplayName = "Charge.ToString should produce the expected result")]
    public void ChargeToStringShouldProduceExpectedResult()
    {
        // Given
        Charge<double> c = Charge<double>.FromCoulombs(1000.0);

        // When / Then
        Assert.Equal("1,000.000 C", $"{c:C3}");
        Assert.Equal("1.000 kC", $"{c:kC3}");
        Assert.Equal("0.001 MC", $"{c:MC3}");
        Assert.Equal("1,000,000.000 mC", $"{c:mC3}");
    }

    [Fact(DisplayName = "Charge.ToString MC vs mC are case-sensitive")]
    public void ChargeToStringMcVsMcAreCaseSensitive()
    {
        // Given
        Charge<double> c = Charge<double>.FromCoulombs(1.0);

        // Then
        Assert.Equal("0.000001 MC", $"{c:MC6}"); // mega
        Assert.Equal("1,000.000 mC", $"{c:mC3}"); // milli
    }

    [Fact(DisplayName = "Charge.ToString PC vs pC are case-sensitive")]
    public void ChargeToStringPcVsPcAreCaseSensitive()
    {
        // Given
        Charge<double> c = Charge<double>.FromCoulombs(1.0);

        // Then
        Assert.Equal("0.000000000000001 PC", $"{c:PC15}"); // peta
        Assert.Equal("1,000,000,000,000.000 pC", $"{c:pC3}"); // pico
    }

    [Fact(DisplayName = "Charge.ToString µC symbol should differ from format specifier")]
    public void ChargeToStringMicrocoulombsSymbolShouldDifferFromFormatSpecifier()
    {
        // Given
        Charge<double> c = Charge<double>.FromCoulombs(1.0);

        // Then: specifier is uC, but symbol rendered is µC
        Assert.Equal("1,000,000.000 µC", $"{c:uC3}");
    }

    [Fact(DisplayName = "Charge.ToString non-SI specifiers should use proper unit symbols")]
    public void ChargeToStringNonSiSpecifiersShouldUseProperUnitSymbols()
    {
        // Given: 1 Ah = 3600 C
        Charge<double> c = Charge<double>.FromAmpereHours(1.0);

        // Then
        Assert.Equal("1.000 A·h", $"{c:Ah3}");
        Assert.Equal("1,000.000 mA·h", $"{c:mAh3}");
        Assert.Equal("3,600.000 C", $"{c:C3}");
    }

    [Fact(DisplayName = "Charge.ToString elementary charge should be rendered correctly")]
    public void ChargeToStringElementaryChargeShouldBeRenderedCorrectly()
    {
        // Given: 1 elementary charge
        Charge<double> c = Charge<double>.FromElementaryCharges(1.0);

        // Then
        Assert.Equal("1.000 e", $"{c:e3}");
    }

    [Fact(DisplayName = "Charge.ToString should honor custom culture separators")]
    public void ChargeToStringShouldHonorCustomCulture()
    {
        // Given
        CultureInfo customCulture = new("de-DE");
        Charge<double> c = Charge<double>.FromCoulombs(1234.56);

        // When
        string formatted = c.ToString("C2", customCulture);

        // Then
        Assert.Equal("1.234,56 C", formatted);
    }
}
