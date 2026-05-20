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

public sealed class CurrentTests
{
    [Fact(DisplayName = "Current.Zero should produce the expected result")]
    public void CurrentZeroShouldProduceExpectedResult()
    {
        // Given / When
        Current<Float128> current = Current<Float128>.Zero;

        // Then
        Assert.Equal(Float128.Zero, current.QuectoAmperes);
    }

    [Theory(DisplayName = "Current.FromQuectoamperes should produce the expected QuectoAmperes")]
    [InlineData("0", "0")]
    [InlineData("1", "1")]
    [InlineData("2.5", "2.5")]
    public void CurrentFromQuectoamperesShouldProduceExpectedQuectoAmperes(string value, string expected)
    {
        Current<Float128> c = Current<Float128>.FromQuectoamperes(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), c.QuectoAmperes);
    }

    [Theory(DisplayName = "Current.FromRontoamperes should produce the expected QuectoAmperes")]
    [InlineData("0", "0")]
    [InlineData("1", "1e3")]
    [InlineData("2.5", "2.5e3")]
    public void CurrentFromRontoamperesShouldProduceExpectedQuectoAmperes(string value, string expected)
    {
        Current<Float128> c = Current<Float128>.FromRontoamperes(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), c.QuectoAmperes);
    }

    [Theory(DisplayName = "Current.FromYoctoamperes should produce the expected QuectoAmperes")]
    [InlineData("0", "0")]
    [InlineData("1", "1e6")]
    [InlineData("2.5", "2.5e6")]
    public void CurrentFromYoctoamperesShouldProduceExpectedQuectoAmperes(string value, string expected)
    {
        Current<Float128> c = Current<Float128>.FromYoctoamperes(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), c.QuectoAmperes);
    }

    [Theory(DisplayName = "Current.FromZeptoamperes should produce the expected QuectoAmperes")]
    [InlineData("0", "0")]
    [InlineData("1", "1e9")]
    [InlineData("2.5", "2.5e9")]
    public void CurrentFromZeptoamperesShouldProduceExpectedQuectoAmperes(string value, string expected)
    {
        Current<Float128> c = Current<Float128>.FromZeptoamperes(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), c.QuectoAmperes);
    }

    [Theory(DisplayName = "Current.FromAttoamperes should produce the expected QuectoAmperes")]
    [InlineData("0", "0")]
    [InlineData("1", "1e12")]
    [InlineData("2.5", "2.5e12")]
    public void CurrentFromAttoamperesShouldProduceExpectedQuectoAmperes(string value, string expected)
    {
        Current<Float128> c = Current<Float128>.FromAttoamperes(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), c.QuectoAmperes);
    }

    [Theory(DisplayName = "Current.FromFemtoamperes should produce the expected QuectoAmperes")]
    [InlineData("0", "0")]
    [InlineData("1", "1e15")]
    [InlineData("2.5", "2.5e15")]
    public void CurrentFromFemtoamperesShouldProduceExpectedQuectoAmperes(string value, string expected)
    {
        Current<Float128> c = Current<Float128>.FromFemtoamperes(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), c.QuectoAmperes);
    }

    [Theory(DisplayName = "Current.FromPicoamperes should produce the expected QuectoAmperes")]
    [InlineData("0", "0")]
    [InlineData("1", "1e18")]
    [InlineData("2.5", "2.5e18")]
    public void CurrentFromPicoamperesShouldProduceExpectedQuectoAmperes(string value, string expected)
    {
        Current<Float128> c = Current<Float128>.FromPicoamperes(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), c.QuectoAmperes);
    }

    [Theory(DisplayName = "Current.FromNanoamperes should produce the expected QuectoAmperes")]
    [InlineData("0", "0")]
    [InlineData("1", "1e21")]
    [InlineData("2.5", "2.5e21")]
    public void CurrentFromNanoamperesShouldProduceExpectedQuectoAmperes(string value, string expected)
    {
        Current<Float128> c = Current<Float128>.FromNanoamperes(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), c.QuectoAmperes);
    }

    [Theory(DisplayName = "Current.FromMicroamperes should produce the expected QuectoAmperes")]
    [InlineData("0", "0")]
    [InlineData("1", "1e24")]
    [InlineData("2.5", "2.5e24")]
    public void CurrentFromMicroamperesShouldProduceExpectedQuectoAmperes(string value, string expected)
    {
        Current<Float128> c = Current<Float128>.FromMicroamperes(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), c.QuectoAmperes);
    }

    [Theory(DisplayName = "Current.FromMilliamperes should produce the expected QuectoAmperes")]
    [InlineData("0", "0")]
    [InlineData("1", "1e27")]
    [InlineData("2.5", "2.5e27")]
    public void CurrentFromMilliamperesShouldProduceExpectedQuectoAmperes(string value, string expected)
    {
        Current<Float128> c = Current<Float128>.FromMilliamperes(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), c.QuectoAmperes);
    }

    [Theory(DisplayName = "Current.FromCentiamperes should produce the expected QuectoAmperes")]
    [InlineData("0", "0")]
    [InlineData("1", "1e28")]
    [InlineData("2.5", "2.5e28")]
    public void CurrentFromCentiamperesShouldProduceExpectedQuectoAmperes(string value, string expected)
    {
        Current<Float128> c = Current<Float128>.FromCentiamperes(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), c.QuectoAmperes);
    }

    [Theory(DisplayName = "Current.FromDeciamperes should produce the expected QuectoAmperes")]
    [InlineData("0", "0")]
    [InlineData("1", "1e29")]
    [InlineData("2.5", "2.5e29")]
    public void CurrentFromDeciamperesShouldProduceExpectedQuectoAmperes(string value, string expected)
    {
        Current<Float128> c = Current<Float128>.FromDeciamperes(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), c.QuectoAmperes);
    }

    [Theory(DisplayName = "Current.FromAmperes should produce the expected QuectoAmperes")]
    [InlineData("0", "0")]
    [InlineData("1", "1e30")]
    [InlineData("2.5", "2.5e30")]
    public void CurrentFromAmperesShouldProduceExpectedQuectoAmperes(string value, string expected)
    {
        Current<Float128> c = Current<Float128>.FromAmperes(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), c.QuectoAmperes);
    }

    [Theory(DisplayName = "Current.FromDecaamperes should produce the expected QuectoAmperes")]
    [InlineData("0", "0")]
    [InlineData("1", "1e31")]
    [InlineData("2.5", "2.5e31")]
    public void CurrentFromDecaamperesShouldProduceExpectedQuectoAmperes(string value, string expected)
    {
        Current<Float128> c = Current<Float128>.FromDecaamperes(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), c.QuectoAmperes);
    }

    [Theory(DisplayName = "Current.FromHectoamperes should produce the expected QuectoAmperes")]
    [InlineData("0", "0")]
    [InlineData("1", "1e32")]
    [InlineData("2.5", "2.5e32")]
    public void CurrentFromHectoamperesShouldProduceExpectedQuectoAmperes(string value, string expected)
    {
        Current<Float128> c = Current<Float128>.FromHectoamperes(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), c.QuectoAmperes);
    }

    [Theory(DisplayName = "Current.FromKiloamperes should produce the expected QuectoAmperes")]
    [InlineData("0", "0")]
    [InlineData("1", "1e33")]
    [InlineData("2.5", "2.5e33")]
    public void CurrentFromKiloamperesShouldProduceExpectedQuectoAmperes(string value, string expected)
    {
        Current<Float128> c = Current<Float128>.FromKiloamperes(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), c.QuectoAmperes);
    }

    [Theory(DisplayName = "Current.FromMegaamperes should produce the expected QuectoAmperes")]
    [InlineData("0", "0")]
    [InlineData("1", "1e36")]
    [InlineData("2.5", "2.5e36")]
    public void CurrentFromMegaamperesShouldProduceExpectedQuectoAmperes(string value, string expected)
    {
        Current<Float128> c = Current<Float128>.FromMegaamperes(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), c.QuectoAmperes);
    }

    [Theory(DisplayName = "Current.FromGigaamperes should produce the expected QuectoAmperes")]
    [InlineData("0", "0")]
    [InlineData("1", "1e39")]
    [InlineData("2.5", "2.5e39")]
    public void CurrentFromGigaamperesShouldProduceExpectedQuectoAmperes(string value, string expected)
    {
        Current<Float128> c = Current<Float128>.FromGigaamperes(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), c.QuectoAmperes);
    }

    [Theory(DisplayName = "Current.FromTeraamperes should produce the expected QuectoAmperes")]
    [InlineData("0", "0")]
    [InlineData("1", "1e42")]
    [InlineData("2.5", "2.5e42")]
    public void CurrentFromTeraamperesShouldProduceExpectedQuectoAmperes(string value, string expected)
    {
        Current<Float128> c = Current<Float128>.FromTeraamperes(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), c.QuectoAmperes);
    }

    [Theory(DisplayName = "Current.FromPetaamperes should produce the expected QuectoAmperes")]
    [InlineData("0", "0")]
    [InlineData("1", "1e45")]
    [InlineData("2.5", "2.5e45")]
    public void CurrentFromPetaamperesShouldProduceExpectedQuectoAmperes(string value, string expected)
    {
        Current<Float128> c = Current<Float128>.FromPetaamperes(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), c.QuectoAmperes);
    }

    [Theory(DisplayName = "Current.FromExaamperes should produce the expected QuectoAmperes")]
    [InlineData("0", "0")]
    [InlineData("1", "1e48")]
    [InlineData("2.5", "2.5e48")]
    public void CurrentFromExaamperesShouldProduceExpectedQuectoAmperes(string value, string expected)
    {
        Current<Float128> c = Current<Float128>.FromExaamperes(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), c.QuectoAmperes);
    }

    [Theory(DisplayName = "Current.FromZettaamperes should produce the expected QuectoAmperes")]
    [InlineData("0", "0")]
    [InlineData("1", "1e51")]
    [InlineData("2.5", "2.5e51")]
    public void CurrentFromZettaamperesShouldProduceExpectedQuectoAmperes(string value, string expected)
    {
        Current<Float128> c = Current<Float128>.FromZettaamperes(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), c.QuectoAmperes);
    }

    [Theory(DisplayName = "Current.FromYottaamperes should produce the expected QuectoAmperes")]
    [InlineData("0", "0")]
    [InlineData("1", "1e54")]
    [InlineData("2.5", "2.5e54")]
    public void CurrentFromYottaamperesShouldProduceExpectedQuectoAmperes(string value, string expected)
    {
        Current<Float128> c = Current<Float128>.FromYottaamperes(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), c.QuectoAmperes);
    }

    [Theory(DisplayName = "Current.FromRonnaamperes should produce the expected QuectoAmperes")]
    [InlineData("0", "0")]
    [InlineData("1", "1e57")]
    [InlineData("2.5", "2.5e57")]
    public void CurrentFromRonnaamperesShouldProduceExpectedQuectoAmperes(string value, string expected)
    {
        Current<Float128> c = Current<Float128>.FromRonnaamperes(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), c.QuectoAmperes);
    }

    [Theory(DisplayName = "Current.FromQuettaamperes should produce the expected QuectoAmperes")]
    [InlineData("0", "0")]
    [InlineData("1", "1e60")]
    [InlineData("2.5", "2.5e60")]
    public void CurrentFromQuettaamperesShouldProduceExpectedQuectoAmperes(string value, string expected)
    {
        Current<Float128> c = Current<Float128>.FromQuettaamperes(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), c.QuectoAmperes);
    }

    [Fact(DisplayName = "Current.Add should produce the expected result")]
    public void CurrentAddShouldProduceExpectedValue()
    {
        // Given
        Current<Float128> left = Current<Float128>.FromAmperes(Float128.Parse("1.5"));
        Current<Float128> right = Current<Float128>.FromAmperes(Float128.Parse("0.5"));

        // When
        Current<Float128> result = left.Add(right);

        // Then
        Assert.Equal(Float128.Parse("2"), result.Amperes);
    }

    [Fact(DisplayName = "Current.Subtract should produce the expected result")]
    public void CurrentSubtractShouldProduceExpectedValue()
    {
        // Given
        Current<Float128> left = Current<Float128>.FromAmperes(Float128.Parse("1.5"));
        Current<Float128> right = Current<Float128>.FromAmperes(Float128.Parse("0.4"));

        // When
        Current<Float128> result = left.Subtract(right);

        // Then
        Assert.Equal(Float128.Parse("1.1"), result.Amperes);
    }

    [Fact(DisplayName = "Current comparison should produce the expected result (left equal to right)")]
    public void CurrentComparisonShouldProduceExpectedResultLeftEqualToRight()
    {
        // Given
        Current<Float128> left = Current<Float128>.FromAmperes(Float128.Parse("123"));
        Current<Float128> right = Current<Float128>.FromAmperes(Float128.Parse("123"));

        // When / Then
        Assert.Equal(0, Current<Float128>.Compare(left, right));
        Assert.Equal(0, left.CompareTo(right));
        Assert.Equal(0, left.CompareTo((object)right));
        Assert.False(left > right);
        Assert.True(left >= right);
        Assert.False(left < right);
        Assert.True(left <= right);
    }

    [Fact(DisplayName = "Current comparison should produce the expected result (left greater than right)")]
    public void CurrentComparisonShouldProduceExpectedLeftGreaterThanRight()
    {
        // Given
        Current<Float128> left = Current<Float128>.FromAmperes(Float128.Parse("456"));
        Current<Float128> right = Current<Float128>.FromAmperes(Float128.Parse("123"));

        // When / Then
        Assert.Equal(1, Current<Float128>.Compare(left, right));
        Assert.Equal(1, left.CompareTo(right));
        Assert.Equal(1, left.CompareTo((object)right));
        Assert.True(left > right);
        Assert.True(left >= right);
        Assert.False(left < right);
        Assert.False(left <= right);
    }

    [Fact(DisplayName = "Current comparison should produce the expected result (left less than right)")]
    public void CurrentComparisonShouldProduceExpectedLeftLessThanRight()
    {
        // Given
        Current<Float128> left = Current<Float128>.FromAmperes(Float128.Parse("123"));
        Current<Float128> right = Current<Float128>.FromAmperes(Float128.Parse("456"));

        // When / Then
        Assert.Equal(-1, Current<Float128>.Compare(left, right));
        Assert.Equal(-1, left.CompareTo(right));
        Assert.Equal(-1, left.CompareTo((object)right));
        Assert.False(left > right);
        Assert.False(left >= right);
        Assert.True(left < right);
        Assert.True(left <= right);
    }

    [Fact(DisplayName = "Current equality should produce the expected result (left equal to right)")]
    public void CurrentEqualityShouldProduceExpectedResultLeftEqualToRight()
    {
        // Given — 2 A and 2000 mA are the same canonical current; equality should hold at Float128.
        Current<Float128> left = Current<Float128>.FromAmperes(Float128.Parse("2"));
        Current<Float128> right = Current<Float128>.FromMilliamperes(Float128.Parse("2000"));

        // When / Then
        Assert.True(Current<Float128>.Equals(left, right));
        Assert.True(left.Equals(right));
        Assert.True(left.Equals((object)right));
        Assert.True(left == right);
        Assert.False(left != right);
    }

    [Fact(DisplayName = "Current equality should produce the expected result (left not equal to right)")]
    public void CurrentEqualityShouldProduceExpectedResultLeftNotEqualToRight()
    {
        // Given
        Current<Float128> left = Current<Float128>.FromAmperes(Float128.Parse("2"));
        Current<Float128> right = Current<Float128>.FromMilliamperes(Float128.Parse("2500"));

        // When / Then
        Assert.False(Current<Float128>.Equals(left, right));
        Assert.False(left.Equals(right));
        Assert.False(left.Equals((object)right));
        Assert.False(left == right);
        Assert.True(left != right);
    }

    [Fact(DisplayName = "Current.ToString should produce the expected result")]
    public void CurrentToStringShouldProduceExpectedResult()
    {
        // Given
        Current<Float128> c = Current<Float128>.FromAmperes(Float128.Parse("1000"));

        // When / Then
        Assert.Equal("1,000.000 A", $"{c:A3}");
        Assert.Equal("1.000 kA", $"{c:kA3}");
        Assert.Equal("0.001 MA", $"{c:MA3}");
        Assert.Equal("1,000,000.000 mA", $"{c:mA3}");
    }

    [Fact(DisplayName = "Current.ToString MA vs mA are case-sensitive")]
    public void CurrentToStringMaVsMaAreCaseSensitive()
    {
        // Given
        Current<Float128> c = Current<Float128>.FromAmperes(Float128.Parse("1"));

        // Then
        Assert.Equal("0.000001 MA", $"{c:MA6}"); // mega
        Assert.Equal("1,000.000 mA", $"{c:mA3}"); // milli
    }

    [Fact(DisplayName = "Current.ToString PA vs pA are case-sensitive")]
    public void CurrentToStringPaVsPaAreCaseSensitive()
    {
        // Given
        Current<Float128> c = Current<Float128>.FromAmperes(Float128.Parse("1"));

        // Then
        Assert.Equal("0.000000000000001 PA", $"{c:PA15}"); // peta
        Assert.Equal("1,000,000,000,000.000 pA", $"{c:pA3}"); // pico
    }

    [Fact(DisplayName = "Current.ToString µA symbol should differ from format specifier")]
    public void CurrentToStringMicroamperesSymbolShouldDifferFromFormatSpecifier()
    {
        // Given
        Current<Float128> c = Current<Float128>.FromAmperes(Float128.Parse("1"));

        // Then: specifier is uA, but symbol rendered is µA
        Assert.Equal("1,000,000.000 µA", $"{c:uA3}");
    }

    [Fact(DisplayName = "Current.ToString should honor custom culture separators")]
    public void CurrentToStringShouldHonorCustomCulture()
    {
        // Given
        CultureInfo customCulture = new("de-DE");
        Current<Float128> c = Current<Float128>.FromAmperes(Float128.Parse("1234.56"));

        // When
        string formatted = c.ToString("A2", customCulture);

        // Then
        Assert.Equal("1.234,56 A", formatted);
    }
}
