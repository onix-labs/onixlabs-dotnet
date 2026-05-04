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

public sealed class TimeTests
{
    // IEEE-754 binary floating-point arithmetic causes small discrepancies in calculation, therefore we need a tolerance.
    private const double Tolerance = 1e+42;

    [Fact(DisplayName = "Time.Zero should produce the expected result")]
    public void TimeZeroShouldProduceExpectedResult()
    {
        // Given / When
        Time<double> time = Time<double>.Zero;

        // Then
        Assert.Equal(0.0, time.QuectoSeconds, Tolerance);
    }

    [Theory(DisplayName = "Time.FromQuectoseconds should produce the expected QuectoSeconds")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1.0)]
    [InlineData(2.5, 2.5)]
    public void TimeFromQuectosecondsShouldProduceExpectedQuectoSeconds(double value, double expected)
    {
        Time<double> t = Time<double>.FromQuectoseconds(value);
        Assert.Equal(expected, t.QuectoSeconds, Tolerance);
    }

    [Theory(DisplayName = "Time.FromRontoseconds should produce the expected QuectoSeconds")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e3)]
    [InlineData(2.5, 2.5e3)]
    public void TimeFromRontosecondsShouldProduceExpectedQuectoSeconds(double value, double expected)
    {
        Time<double> t = Time<double>.FromRontoseconds(value);
        Assert.Equal(expected, t.QuectoSeconds, Tolerance);
    }

    [Theory(DisplayName = "Time.FromYoctoseconds should produce the expected QuectoSeconds")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e6)]
    [InlineData(2.5, 2.5e6)]
    public void TimeFromYoctosecondsShouldProduceExpectedQuectoSeconds(double value, double expected)
    {
        Time<double> t = Time<double>.FromYoctoseconds(value);
        Assert.Equal(expected, t.QuectoSeconds, Tolerance);
    }

    [Theory(DisplayName = "Time.FromZeptoseconds should produce the expected QuectoSeconds")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e9)]
    [InlineData(2.5, 2.5e9)]
    public void TimeFromZeptosecondsShouldProduceExpectedQuectoSeconds(double value, double expected)
    {
        Time<double> t = Time<double>.FromZeptoseconds(value);
        Assert.Equal(expected, t.QuectoSeconds, Tolerance);
    }

    [Theory(DisplayName = "Time.FromAttoseconds should produce the expected QuectoSeconds")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e12)]
    [InlineData(2.5, 2.5e12)]
    public void TimeFromAttosecondsShouldProduceExpectedQuectoSeconds(double value, double expected)
    {
        Time<double> t = Time<double>.FromAttoseconds(value);
        Assert.Equal(expected, t.QuectoSeconds, Tolerance);
    }

    [Theory(DisplayName = "Time.FromFemtoseconds should produce the expected QuectoSeconds")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e15)]
    [InlineData(2.5, 2.5e15)]
    public void TimeFromFemtosecondsShouldProduceExpectedQuectoSeconds(double value, double expected)
    {
        Time<double> t = Time<double>.FromFemtoseconds(value);
        Assert.Equal(expected, t.QuectoSeconds, Tolerance);
    }

    [Theory(DisplayName = "Time.FromPicoseconds should produce the expected QuectoSeconds")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e18)]
    [InlineData(2.5, 2.5e18)]
    public void TimeFromPicosecondsShouldProduceExpectedQuectoSeconds(double value, double expected)
    {
        Time<double> t = Time<double>.FromPicoseconds(value);
        Assert.Equal(expected, t.QuectoSeconds, Tolerance);
    }

    [Theory(DisplayName = "Time.FromNanoseconds should produce the expected QuectoSeconds")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e21)]
    [InlineData(2.5, 2.5e21)]
    public void TimeFromNanosecondsShouldProduceExpectedQuectoSeconds(double value, double expected)
    {
        Time<double> t = Time<double>.FromNanoseconds(value);
        Assert.Equal(expected, t.QuectoSeconds, Tolerance);
    }

    [Theory(DisplayName = "Time.FromMicroseconds should produce the expected QuectoSeconds")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e24)]
    [InlineData(2.5, 2.5e24)]
    public void TimeFromMicrosecondsShouldProduceExpectedQuectoSeconds(double value, double expected)
    {
        Time<double> t = Time<double>.FromMicroseconds(value);
        Assert.Equal(expected, t.QuectoSeconds, Tolerance);
    }

    [Theory(DisplayName = "Time.FromMilliseconds should produce the expected QuectoSeconds")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e27)]
    [InlineData(2.5, 2.5e27)]
    public void TimeFromMillisecondsShouldProduceExpectedQuectoSeconds(double value, double expected)
    {
        Time<double> t = Time<double>.FromMilliseconds(value);
        Assert.Equal(expected, t.QuectoSeconds, Tolerance);
    }

    [Theory(DisplayName = "Time.FromCentiseconds should produce the expected QuectoSeconds")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e28)]
    [InlineData(2.5, 2.5e28)]
    public void TimeFromCentisecondsShouldProduceExpectedQuectoSeconds(double value, double expected)
    {
        Time<double> t = Time<double>.FromCentiseconds(value);
        Assert.Equal(expected, t.QuectoSeconds, Tolerance);
    }

    [Theory(DisplayName = "Time.FromDeciseconds should produce the expected QuectoSeconds")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e29)]
    [InlineData(2.5, 2.5e29)]
    public void TimeFromDecisecondsShouldProduceExpectedQuectoSeconds(double value, double expected)
    {
        Time<double> t = Time<double>.FromDeciseconds(value);
        Assert.Equal(expected, t.QuectoSeconds, Tolerance);
    }

    [Theory(DisplayName = "Time.FromSeconds should produce the expected QuectoSeconds")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e30)]
    [InlineData(2.5, 2.5e30)]
    public void TimeFromSecondsShouldProduceExpectedQuectoSeconds(double value, double expected)
    {
        Time<double> t = Time<double>.FromSeconds(value);
        Assert.Equal(expected, t.QuectoSeconds, Tolerance);
    }

    [Theory(DisplayName = "Time.FromDecaseconds should produce the expected QuectoSeconds")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e31)]
    [InlineData(2.5, 2.5e31)]
    public void TimeFromDecasecondsShouldProduceExpectedQuectoSeconds(double value, double expected)
    {
        Time<double> t = Time<double>.FromDecaseconds(value);
        Assert.Equal(expected, t.QuectoSeconds, Tolerance);
    }

    [Theory(DisplayName = "Time.FromHectoseconds should produce the expected QuectoSeconds")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e32)]
    [InlineData(2.5, 2.5e32)]
    public void TimeFromHectosecondsShouldProduceExpectedQuectoSeconds(double value, double expected)
    {
        Time<double> t = Time<double>.FromHectoseconds(value);
        Assert.Equal(expected, t.QuectoSeconds, Tolerance);
    }

    [Theory(DisplayName = "Time.FromKiloseconds should produce the expected QuectoSeconds")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e33)]
    [InlineData(2.5, 2.5e33)]
    public void TimeFromKilosecondsShouldProduceExpectedQuectoSeconds(double value, double expected)
    {
        Time<double> t = Time<double>.FromKiloseconds(value);
        Assert.Equal(expected, t.QuectoSeconds, Tolerance);
    }

    [Theory(DisplayName = "Time.FromMegaseconds should produce the expected QuectoSeconds")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e36)]
    [InlineData(2.5, 2.5e36)]
    public void TimeFromMegasecondsShouldProduceExpectedQuectoSeconds(double value, double expected)
    {
        Time<double> t = Time<double>.FromMegaseconds(value);
        Assert.Equal(expected, t.QuectoSeconds, Tolerance);
    }

    [Theory(DisplayName = "Time.FromGigaseconds should produce the expected QuectoSeconds")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e39)]
    [InlineData(2.5, 2.5e39)]
    public void TimeFromGigasecondsShouldProduceExpectedQuectoSeconds(double value, double expected)
    {
        Time<double> t = Time<double>.FromGigaseconds(value);
        Assert.Equal(expected, t.QuectoSeconds, Tolerance);
    }

    [Theory(DisplayName = "Time.FromTeraseconds should produce the expected QuectoSeconds")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e42)]
    [InlineData(2.5, 2.5e42)]
    public void TimeFromTerasecondsShouldProduceExpectedQuectoSeconds(double value, double expected)
    {
        Time<double> t = Time<double>.FromTeraseconds(value);
        Assert.Equal(expected, t.QuectoSeconds, Tolerance);
    }

    [Theory(DisplayName = "Time.FromPetaseconds should produce the expected QuectoSeconds")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e45)]
    [InlineData(2.5, 2.5e45)]
    public void TimeFromPetasecondsShouldProduceExpectedQuectoSeconds(double value, double expected)
    {
        Time<double> t = Time<double>.FromPetaseconds(value);
        Assert.Equal(expected, t.QuectoSeconds, Tolerance);
    }

    [Theory(DisplayName = "Time.FromExaseconds should produce the expected QuectoSeconds")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e48)]
    [InlineData(2.5, 2.5e48)]
    public void TimeFromExasecondsShouldProduceExpectedQuectoSeconds(double value, double expected)
    {
        Time<double> t = Time<double>.FromExaseconds(value);
        Assert.Equal(expected, t.QuectoSeconds, Tolerance);
    }

    [Theory(DisplayName = "Time.FromZettaseconds should produce the expected QuectoSeconds")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e51)]
    [InlineData(2.5, 2.5e51)]
    public void TimeFromZettasecondsShouldProduceExpectedQuectoSeconds(double value, double expected)
    {
        Time<double> t = Time<double>.FromZettaseconds(value);
        Assert.Equal(expected, t.QuectoSeconds, Tolerance);
    }

    [Theory(DisplayName = "Time.FromYottaseconds should produce the expected QuectoSeconds")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e54)]
    [InlineData(2.5, 2.5e54)]
    public void TimeFromYottasecondsShouldProduceExpectedQuectoSeconds(double value, double expected)
    {
        Time<double> t = Time<double>.FromYottaseconds(value);
        Assert.Equal(expected, t.QuectoSeconds, Tolerance);
    }

    [Theory(DisplayName = "Time.FromRonnaseconds should produce the expected QuectoSeconds")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e57)]
    [InlineData(2.5, 2.5e57)]
    public void TimeFromRonnasecondsShouldProduceExpectedQuectoSeconds(double value, double expected)
    {
        Time<double> t = Time<double>.FromRonnaseconds(value);
        Assert.Equal(expected, t.QuectoSeconds, Tolerance);
    }

    [Theory(DisplayName = "Time.FromQuettaseconds should produce the expected QuectoSeconds")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e60)]
    [InlineData(2.5, 2.5e60)]
    public void TimeFromQuettasecondsShouldProduceExpectedQuectoSeconds(double value, double expected)
    {
        Time<double> t = Time<double>.FromQuettaseconds(value);
        Assert.Equal(expected, t.QuectoSeconds, Tolerance);
    }

    [Theory(DisplayName = "Time.FromMinutes should produce the expected QuectoSeconds")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 6e31)]
    [InlineData(2.5, 1.5e32)]
    public void TimeFromMinutesShouldProduceExpectedQuectoSeconds(double value, double expected)
    {
        Time<double> t = Time<double>.FromMinutes(value);
        Assert.Equal(expected, t.QuectoSeconds, Tolerance);
    }

    [Theory(DisplayName = "Time.FromHours should produce the expected QuectoSeconds")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 3.6e33)]
    [InlineData(2.5, 9e33)]
    public void TimeFromHoursShouldProduceExpectedQuectoSeconds(double value, double expected)
    {
        Time<double> t = Time<double>.FromHours(value);
        Assert.Equal(expected, t.QuectoSeconds, Tolerance);
    }

    [Theory(DisplayName = "Time.FromDays should produce the expected QuectoSeconds")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 8.64e34)]
    [InlineData(2.5, 2.16e35)]
    public void TimeFromDaysShouldProduceExpectedQuectoSeconds(double value, double expected)
    {
        Time<double> t = Time<double>.FromDays(value);
        Assert.Equal(expected, t.QuectoSeconds, Tolerance);
    }

    [Theory(DisplayName = "Time.FromWeeks should produce the expected QuectoSeconds")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 6.048e35)]
    [InlineData(2.5, 1.512e36)]
    public void TimeFromWeeksShouldProduceExpectedQuectoSeconds(double value, double expected)
    {
        Time<double> t = Time<double>.FromWeeks(value);
        Assert.Equal(expected, t.QuectoSeconds, Tolerance);
    }

    [Theory(DisplayName = "Time.FromJulianYears should produce the expected QuectoSeconds")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 3.15576e37)]
    [InlineData(2.5, 7.8894e37)]
    public void TimeFromJulianYearsShouldProduceExpectedQuectoSeconds(double value, double expected)
    {
        Time<double> t = Time<double>.FromJulianYears(value);
        Assert.Equal(expected, t.QuectoSeconds, Tolerance);
    }

    [Fact(DisplayName = "Time.Add should produce the expected result")]
    public void TimeAddShouldProduceExpectedValue()
    {
        // Given
        Time<double> left = Time<double>.FromSeconds(60.0);
        Time<double> right = Time<double>.FromSeconds(30.0);

        // When
        Time<double> result = left.Add(right);

        // Then
        Assert.Equal(90.0, result.Seconds, Tolerance);
    }

    [Fact(DisplayName = "Time.Subtract should produce the expected result")]
    public void TimeSubtractShouldProduceExpectedValue()
    {
        // Given
        Time<double> left = Time<double>.FromSeconds(120.0);
        Time<double> right = Time<double>.FromSeconds(45.0);

        // When
        Time<double> result = left.Subtract(right);

        // Then
        Assert.Equal(75.0, result.Seconds, Tolerance);
    }

    [Fact(DisplayName = "Time.Multiply should produce the expected result")]
    public void TimeMultiplyShouldProduceExpectedValue()
    {
        // Given
        Time<double> left = Time<double>.FromSeconds(10.0);  // 1e31 qs
        Time<double> right = Time<double>.FromSeconds(3.0);  // 3e30 qs

        // When
        Time<double> result = left.Multiply(right);          // 1e31 * 3e30 = 3e61 qs

        // Then
        Assert.Equal(1e31, left.QuectoSeconds, Tolerance);
        Assert.Equal(3e30, right.QuectoSeconds, Tolerance);
        Assert.Equal(3e61, result.QuectoSeconds, Tolerance);
    }

    [Fact(DisplayName = "Time.Divide should produce the expected result")]
    public void TimeDivideShouldProduceExpectedValue()
    {
        // Given
        Time<double> left = Time<double>.FromSeconds(100.0); // 1e32 qs
        Time<double> right = Time<double>.FromSeconds(20.0); // 2e31 qs

        // When
        Time<double> result = left.Divide(right);            // 1e32 / 2e31 = 5 qs

        // Then
        Assert.Equal(5.0, result.QuectoSeconds, Tolerance);
        Assert.Equal(5e-30, result.Seconds, Tolerance);
    }

    [Fact(DisplayName = "Time comparison should produce the expected result (left equal to right)")]
    public void TimeComparisonShouldProduceExpectedResultLeftEqualToRight()
    {
        // Given
        Time<double> left = Time<double>.FromSeconds(60.0);
        Time<double> right = Time<double>.FromSeconds(60.0);

        // When / Then
        Assert.Equal(0, Time<double>.Compare(left, right));
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
        Time<double> left = Time<double>.FromSeconds(120.0);
        Time<double> right = Time<double>.FromSeconds(60.0);

        // When / Then
        Assert.Equal(1, Time<double>.Compare(left, right));
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
        Time<double> left = Time<double>.FromSeconds(30.0);
        Time<double> right = Time<double>.FromSeconds(90.0);

        // When / Then
        Assert.Equal(-1, Time<double>.Compare(left, right));
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
        // Given
        Time<BigDecimal> left = Time<BigDecimal>.FromMinutes(2.0);
        Time<BigDecimal> right = Time<BigDecimal>.FromSeconds(120.0);

        // When / Then
        Assert.True(Time<BigDecimal>.Equals(left, right));
        Assert.True(left.Equals(right));
        Assert.True(left.Equals((object)right));
        Assert.True(left == right);
        Assert.False(left != right);
    }

    [Fact(DisplayName = "Time equality should produce the expected result (left not equal to right)")]
    public void TimeEqualityShouldProduceExpectedResultLeftNotEqualToRight()
    {
        // Given
        Time<double> left = Time<double>.FromMinutes(2.0);
        Time<double> right = Time<double>.FromSeconds(150.0);

        // When / Then
        Assert.False(Time<double>.Equals(left, right));
        Assert.False(left.Equals(right));
        Assert.False(left.Equals((object)right));
        Assert.False(left == right);
        Assert.True(left != right);
    }

    [Fact(DisplayName = "Time.ToString should produce the expected result")]
    public void TimeToStringShouldProduceExpectedResult()
    {
        // Given
        Time<double> t = Time<double>.FromHours(1.0);

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
        Time<double> t = Time<double>.FromSeconds(1234.56);

        // When
        string formatted = t.ToString("s2", customCulture);

        // Then
        Assert.Equal("1.234,56 s", formatted);
    }
}
