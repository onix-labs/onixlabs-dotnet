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

using System;
using System.Globalization;
using OnixLabs.Numerics;

namespace OnixLabs.Units.UnitTests;

public sealed class TimeTests
{
    [Fact(DisplayName = "Time.Zero should produce the expected result")]
    public void TimeZeroShouldProduceExpectedResult()
    {
        // Given / When
        Time<Float128> time = Time<Float128>.Zero;

        // Then
        Assert.Equal(Float128.Zero, time.QuectoSeconds);
    }

    [Theory(DisplayName = "Time.FromQuectoseconds should produce the expected QuectoSeconds")]
    [InlineData("0", "0")]
    [InlineData("1", "1")]
    [InlineData("2.5", "2.5")]
    public void TimeFromQuectosecondsShouldProduceExpectedQuectoSeconds(string value, string expected)
    {
        Time<Float128> t = Time<Float128>.FromQuectoseconds(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), t.QuectoSeconds);
    }

    [Theory(DisplayName = "Time.FromRontoseconds should produce the expected QuectoSeconds")]
    [InlineData("0", "0")]
    [InlineData("1", "1e3")]
    [InlineData("2.5", "2.5e3")]
    public void TimeFromRontosecondsShouldProduceExpectedQuectoSeconds(string value, string expected)
    {
        Time<Float128> t = Time<Float128>.FromRontoseconds(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), t.QuectoSeconds);
    }

    [Theory(DisplayName = "Time.FromYoctoseconds should produce the expected QuectoSeconds")]
    [InlineData("0", "0")]
    [InlineData("1", "1e6")]
    [InlineData("2.5", "2.5e6")]
    public void TimeFromYoctosecondsShouldProduceExpectedQuectoSeconds(string value, string expected)
    {
        Time<Float128> t = Time<Float128>.FromYoctoseconds(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), t.QuectoSeconds);
    }

    [Theory(DisplayName = "Time.FromZeptoseconds should produce the expected QuectoSeconds")]
    [InlineData("0", "0")]
    [InlineData("1", "1e9")]
    [InlineData("2.5", "2.5e9")]
    public void TimeFromZeptosecondsShouldProduceExpectedQuectoSeconds(string value, string expected)
    {
        Time<Float128> t = Time<Float128>.FromZeptoseconds(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), t.QuectoSeconds);
    }

    [Theory(DisplayName = "Time.FromAttoseconds should produce the expected QuectoSeconds")]
    [InlineData("0", "0")]
    [InlineData("1", "1e12")]
    [InlineData("2.5", "2.5e12")]
    public void TimeFromAttosecondsShouldProduceExpectedQuectoSeconds(string value, string expected)
    {
        Time<Float128> t = Time<Float128>.FromAttoseconds(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), t.QuectoSeconds);
    }

    [Theory(DisplayName = "Time.FromFemtoseconds should produce the expected QuectoSeconds")]
    [InlineData("0", "0")]
    [InlineData("1", "1e15")]
    [InlineData("2.5", "2.5e15")]
    public void TimeFromFemtosecondsShouldProduceExpectedQuectoSeconds(string value, string expected)
    {
        Time<Float128> t = Time<Float128>.FromFemtoseconds(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), t.QuectoSeconds);
    }

    [Theory(DisplayName = "Time.FromPicoseconds should produce the expected QuectoSeconds")]
    [InlineData("0", "0")]
    [InlineData("1", "1e18")]
    [InlineData("2.5", "2.5e18")]
    public void TimeFromPicosecondsShouldProduceExpectedQuectoSeconds(string value, string expected)
    {
        Time<Float128> t = Time<Float128>.FromPicoseconds(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), t.QuectoSeconds);
    }

    [Theory(DisplayName = "Time.FromNanoseconds should produce the expected QuectoSeconds")]
    [InlineData("0", "0")]
    [InlineData("1", "1e21")]
    [InlineData("2.5", "2.5e21")]
    public void TimeFromNanosecondsShouldProduceExpectedQuectoSeconds(string value, string expected)
    {
        Time<Float128> t = Time<Float128>.FromNanoseconds(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), t.QuectoSeconds);
    }

    [Theory(DisplayName = "Time.FromMicroseconds should produce the expected QuectoSeconds")]
    [InlineData("0", "0")]
    [InlineData("1", "1e24")]
    [InlineData("2.5", "2.5e24")]
    public void TimeFromMicrosecondsShouldProduceExpectedQuectoSeconds(string value, string expected)
    {
        Time<Float128> t = Time<Float128>.FromMicroseconds(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), t.QuectoSeconds);
    }

    [Theory(DisplayName = "Time.FromMilliseconds should produce the expected QuectoSeconds")]
    [InlineData("0", "0")]
    [InlineData("1", "1e27")]
    [InlineData("2.5", "2.5e27")]
    public void TimeFromMillisecondsShouldProduceExpectedQuectoSeconds(string value, string expected)
    {
        Time<Float128> t = Time<Float128>.FromMilliseconds(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), t.QuectoSeconds);
    }

    [Theory(DisplayName = "Time.FromCentiseconds should produce the expected QuectoSeconds")]
    [InlineData("0", "0")]
    [InlineData("1", "1e28")]
    [InlineData("2.5", "2.5e28")]
    public void TimeFromCentisecondsShouldProduceExpectedQuectoSeconds(string value, string expected)
    {
        Time<Float128> t = Time<Float128>.FromCentiseconds(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), t.QuectoSeconds);
    }

    [Theory(DisplayName = "Time.FromDeciseconds should produce the expected QuectoSeconds")]
    [InlineData("0", "0")]
    [InlineData("1", "1e29")]
    [InlineData("2.5", "2.5e29")]
    public void TimeFromDecisecondsShouldProduceExpectedQuectoSeconds(string value, string expected)
    {
        Time<Float128> t = Time<Float128>.FromDeciseconds(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), t.QuectoSeconds);
    }

    [Theory(DisplayName = "Time.FromSeconds should produce the expected QuectoSeconds")]
    [InlineData("0", "0")]
    [InlineData("1", "1e30")]
    [InlineData("2.5", "2.5e30")]
    public void TimeFromSecondsShouldProduceExpectedQuectoSeconds(string value, string expected)
    {
        Time<Float128> t = Time<Float128>.FromSeconds(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), t.QuectoSeconds);
    }

    [Theory(DisplayName = "Time.FromDecaseconds should produce the expected QuectoSeconds")]
    [InlineData("0", "0")]
    [InlineData("1", "1e31")]
    [InlineData("2.5", "2.5e31")]
    public void TimeFromDecasecondsShouldProduceExpectedQuectoSeconds(string value, string expected)
    {
        Time<Float128> t = Time<Float128>.FromDecaseconds(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), t.QuectoSeconds);
    }

    [Theory(DisplayName = "Time.FromHectoseconds should produce the expected QuectoSeconds")]
    [InlineData("0", "0")]
    [InlineData("1", "1e32")]
    [InlineData("2.5", "2.5e32")]
    public void TimeFromHectosecondsShouldProduceExpectedQuectoSeconds(string value, string expected)
    {
        Time<Float128> t = Time<Float128>.FromHectoseconds(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), t.QuectoSeconds);
    }

    [Theory(DisplayName = "Time.FromKiloseconds should produce the expected QuectoSeconds")]
    [InlineData("0", "0")]
    [InlineData("1", "1e33")]
    [InlineData("2.5", "2.5e33")]
    public void TimeFromKilosecondsShouldProduceExpectedQuectoSeconds(string value, string expected)
    {
        Time<Float128> t = Time<Float128>.FromKiloseconds(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), t.QuectoSeconds);
    }

    [Theory(DisplayName = "Time.FromMegaseconds should produce the expected QuectoSeconds")]
    [InlineData("0", "0")]
    [InlineData("1", "1e36")]
    [InlineData("2.5", "2.5e36")]
    public void TimeFromMegasecondsShouldProduceExpectedQuectoSeconds(string value, string expected)
    {
        Time<Float128> t = Time<Float128>.FromMegaseconds(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), t.QuectoSeconds);
    }

    [Theory(DisplayName = "Time.FromGigaseconds should produce the expected QuectoSeconds")]
    [InlineData("0", "0")]
    [InlineData("1", "1e39")]
    [InlineData("2.5", "2.5e39")]
    public void TimeFromGigasecondsShouldProduceExpectedQuectoSeconds(string value, string expected)
    {
        Time<Float128> t = Time<Float128>.FromGigaseconds(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), t.QuectoSeconds);
    }

    [Theory(DisplayName = "Time.FromTeraseconds should produce the expected QuectoSeconds")]
    [InlineData("0", "0")]
    [InlineData("1", "1e42")]
    [InlineData("2.5", "2.5e42")]
    public void TimeFromTerasecondsShouldProduceExpectedQuectoSeconds(string value, string expected)
    {
        Time<Float128> t = Time<Float128>.FromTeraseconds(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), t.QuectoSeconds);
    }

    [Theory(DisplayName = "Time.FromPetaseconds should produce the expected QuectoSeconds")]
    [InlineData("0", "0")]
    [InlineData("1", "1e45")]
    [InlineData("2.5", "2.5e45")]
    public void TimeFromPetasecondsShouldProduceExpectedQuectoSeconds(string value, string expected)
    {
        Time<Float128> t = Time<Float128>.FromPetaseconds(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), t.QuectoSeconds);
    }

    [Theory(DisplayName = "Time.FromExaseconds should produce the expected QuectoSeconds")]
    [InlineData("0", "0")]
    [InlineData("1", "1e48")]
    [InlineData("2.5", "2.5e48")]
    public void TimeFromExasecondsShouldProduceExpectedQuectoSeconds(string value, string expected)
    {
        Time<Float128> t = Time<Float128>.FromExaseconds(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), t.QuectoSeconds);
    }

    [Theory(DisplayName = "Time.FromZettaseconds should produce the expected QuectoSeconds")]
    [InlineData("0", "0")]
    [InlineData("1", "1e51")]
    [InlineData("2.5", "2.5e51")]
    public void TimeFromZettasecondsShouldProduceExpectedQuectoSeconds(string value, string expected)
    {
        Time<Float128> t = Time<Float128>.FromZettaseconds(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), t.QuectoSeconds);
    }

    [Theory(DisplayName = "Time.FromYottaseconds should produce the expected QuectoSeconds")]
    [InlineData("0", "0")]
    [InlineData("1", "1e54")]
    [InlineData("2.5", "2.5e54")]
    public void TimeFromYottasecondsShouldProduceExpectedQuectoSeconds(string value, string expected)
    {
        Time<Float128> t = Time<Float128>.FromYottaseconds(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), t.QuectoSeconds);
    }

    [Theory(DisplayName = "Time.FromRonnaseconds should produce the expected QuectoSeconds")]
    [InlineData("0", "0")]
    [InlineData("1", "1e57")]
    [InlineData("2.5", "2.5e57")]
    public void TimeFromRonnasecondsShouldProduceExpectedQuectoSeconds(string value, string expected)
    {
        Time<Float128> t = Time<Float128>.FromRonnaseconds(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), t.QuectoSeconds);
    }

    [Theory(DisplayName = "Time.FromQuettaseconds should produce the expected QuectoSeconds")]
    [InlineData("0", "0")]
    [InlineData("1", "1e60")]
    [InlineData("2.5", "2.5e60")]
    public void TimeFromQuettasecondsShouldProduceExpectedQuectoSeconds(string value, string expected)
    {
        Time<Float128> t = Time<Float128>.FromQuettaseconds(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), t.QuectoSeconds);
    }

    [Theory(DisplayName = "Time.FromMinutes should produce the expected QuectoSeconds")]
    [InlineData("0", "0")]
    [InlineData("1", "6e31")]
    [InlineData("2.5", "1.5e32")]
    public void TimeFromMinutesShouldProduceExpectedQuectoSeconds(string value, string expected)
    {
        Time<Float128> t = Time<Float128>.FromMinutes(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), t.QuectoSeconds);
    }

    [Theory(DisplayName = "Time.FromHours should produce the expected QuectoSeconds")]
    [InlineData("0", "0")]
    [InlineData("1", "3.6e33")]
    [InlineData("2.5", "9e33")]
    public void TimeFromHoursShouldProduceExpectedQuectoSeconds(string value, string expected)
    {
        Time<Float128> t = Time<Float128>.FromHours(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), t.QuectoSeconds);
    }

    [Theory(DisplayName = "Time.FromDays should produce the expected QuectoSeconds")]
    [InlineData("0", "0")]
    [InlineData("1", "8.64e34")]
    [InlineData("2.5", "2.16e35")]
    public void TimeFromDaysShouldProduceExpectedQuectoSeconds(string value, string expected)
    {
        Time<Float128> t = Time<Float128>.FromDays(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), t.QuectoSeconds);
    }

    [Theory(DisplayName = "Time.FromWeeks should produce the expected QuectoSeconds")]
    [InlineData("0", "0")]
    [InlineData("1", "6.048e35")]
    [InlineData("2.5", "1.512e36")]
    public void TimeFromWeeksShouldProduceExpectedQuectoSeconds(string value, string expected)
    {
        Time<Float128> t = Time<Float128>.FromWeeks(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), t.QuectoSeconds);
    }

    [Theory(DisplayName = "Time.FromJulianYears should produce the expected QuectoSeconds")]
    [InlineData("0", "0")]
    [InlineData("1", "3.15576e37")]
    [InlineData("2.5", "7.8894e37")]
    public void TimeFromJulianYearsShouldProduceExpectedQuectoSeconds(string value, string expected)
    {
        Time<Float128> t = Time<Float128>.FromJulianYears(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), t.QuectoSeconds);
    }

    [Fact(DisplayName = "Time.Add should produce the expected result")]
    public void TimeAddShouldProduceExpectedValue()
    {
        // Given
        Time<Float128> left = Time<Float128>.FromSeconds(Float128.Parse("60"));
        Time<Float128> right = Time<Float128>.FromSeconds(Float128.Parse("30"));

        // When
        Time<Float128> result = left.Add(right);

        // Then
        Assert.Equal(Float128.Parse("90"), result.Seconds);
    }

    [Fact(DisplayName = "Time.Subtract should produce the expected result")]
    public void TimeSubtractShouldProduceExpectedValue()
    {
        // Given
        Time<Float128> left = Time<Float128>.FromSeconds(Float128.Parse("120"));
        Time<Float128> right = Time<Float128>.FromSeconds(Float128.Parse("45"));

        // When
        Time<Float128> result = left.Subtract(right);

        // Then
        Assert.Equal(Float128.Parse("75"), result.Seconds);
    }

    [Fact(DisplayName = "Time comparison should produce the expected result (left equal to right)")]
    public void TimeComparisonShouldProduceExpectedResultLeftEqualToRight()
    {
        // Given
        Time<Float128> left = Time<Float128>.FromSeconds(Float128.Parse("60"));
        Time<Float128> right = Time<Float128>.FromSeconds(Float128.Parse("60"));

        // When / Then
        Assert.Equal(0, Time<Float128>.Compare(left, right));
        Assert.Equal(0, left.CompareTo(right));
        Assert.Equal(0, left.CompareTo((object)right));
        Assert.False(left > right);
        Assert.True(left >= right);
        Assert.False(left < right);
        Assert.True(left <= right);
    }

    [Fact(DisplayName = "Time comparison should produce the expected result (left greater than right)")]
    public void TimeComparisonShouldProduceExpectedLeftGreaterThanRight()
    {
        // Given
        Time<Float128> left = Time<Float128>.FromSeconds(Float128.Parse("120"));
        Time<Float128> right = Time<Float128>.FromSeconds(Float128.Parse("60"));

        // When / Then
        Assert.Equal(1, Time<Float128>.Compare(left, right));
        Assert.Equal(1, left.CompareTo(right));
        Assert.Equal(1, left.CompareTo((object)right));
        Assert.True(left > right);
        Assert.True(left >= right);
        Assert.False(left < right);
        Assert.False(left <= right);
    }

    [Fact(DisplayName = "Time comparison should produce the expected result (left less than right)")]
    public void TimeComparisonShouldProduceExpectedLeftLessThanRight()
    {
        // Given
        Time<Float128> left = Time<Float128>.FromSeconds(Float128.Parse("30"));
        Time<Float128> right = Time<Float128>.FromSeconds(Float128.Parse("90"));

        // When / Then
        Assert.Equal(-1, Time<Float128>.Compare(left, right));
        Assert.Equal(-1, left.CompareTo(right));
        Assert.Equal(-1, left.CompareTo((object)right));
        Assert.False(left > right);
        Assert.False(left >= right);
        Assert.True(left < right);
        Assert.True(left <= right);
    }

    [Fact(DisplayName = "Time equality should produce the expected result (left equal to right)")]
    public void TimeEqualityShouldProduceExpectedResultLeftEqualToRight()
    {
        // Given — 2 minutes and 120 seconds reduce to the same QuectoSeconds canonical at Float128.
        Time<Float128> left = Time<Float128>.FromMinutes(Float128.Parse("2"));
        Time<Float128> right = Time<Float128>.FromSeconds(Float128.Parse("120"));

        // When / Then
        Assert.True(Time<Float128>.Equals(left, right));
        Assert.True(left.Equals(right));
        Assert.True(left.Equals((object)right));
        Assert.True(left == right);
        Assert.False(left != right);
    }

    [Fact(DisplayName = "Time equality should produce the expected result (left not equal to right)")]
    public void TimeEqualityShouldProduceExpectedResultLeftNotEqualToRight()
    {
        // Given
        Time<Float128> left = Time<Float128>.FromMinutes(Float128.Parse("2"));
        Time<Float128> right = Time<Float128>.FromSeconds(Float128.Parse("150"));

        // When / Then
        Assert.False(Time<Float128>.Equals(left, right));
        Assert.False(left.Equals(right));
        Assert.False(left.Equals((object)right));
        Assert.False(left == right);
        Assert.True(left != right);
    }

    [Fact(DisplayName = "Time.ToString should produce the expected result")]
    public void TimeToStringShouldProduceExpectedResult()
    {
        // Given
        Time<Float128> t = Time<Float128>.FromHours(Float128.Parse("1"));

        // When / Then
        Assert.Equal("1.000 h", $"{t:h3}");
        Assert.Equal("60.000 min", $"{t:min3}");
        Assert.Equal("3,600.000 s", $"{t:s3}");
        Assert.Equal("3,600,000.000 ms", $"{t:ms3}");
        Assert.Equal("3.600 ks", $"{t:ks3}");
        Assert.Equal("0.042 d", $"{t:d3}");
        Assert.Equal("0.006 wk", $"{t:wk3}");
    }

    [Fact(DisplayName = "Time.ToString should honor custom culture separators")]
    public void TimeToStringShouldHonorCustomCulture()
    {
        // Given
        CultureInfo customCulture = new("de-DE");
        Time<Float128> t = Time<Float128>.FromSeconds(Float128.Parse("1234.56"));

        // When
        string formatted = t.ToString("s2", customCulture);

        // Then
        Assert.Equal("1.234,56 s", formatted);
    }

    [Fact(DisplayName = "Time.ValueOf should round-trip with ToString specifier")]
    public void TimeValueOfShouldRoundTripWithToStringSpecifier()
    {
        // Given
        Time<Float128> time = Time<Float128>.FromHours(Float128.Parse("2"));

        // Then
        Assert.Equal(Float128.Parse("2"), time.ValueOf("h"));
        Assert.Equal(Float128.Parse("7200"), time.ValueOf("s"));
    }

    [Fact(DisplayName = "Time.ValueOf should throw on invalid specifier")]
    public void TimeValueOfShouldThrowOnInvalidSpecifier()
    {
        // Given
        Time<Float128> time = Time<Float128>.FromHours(Float128.Parse("2"));

        // Then
        Assert.Throws<ArgumentException>(() => time.ValueOf("xx"));
    }
}
