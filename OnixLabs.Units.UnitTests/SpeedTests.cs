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

public sealed class SpeedTests
{
    [Fact(DisplayName = "Speed should preserve its underlying Distance and Time components")]
    public void SpeedShouldPreserveUnderlyingComponents()
    {
        // Given
        Distance<double> distance = Distance<double>.FromMiles(25);
        Time<double> time = Time<double>.FromHours(1);

        // When
        Speed<double> speed = new(distance, time);

        // Then
        Assert.Equal(distance, speed.Left);
        Assert.Equal(time, speed.Right);
    }

    [Fact(DisplayName = "Speed.Zero should produce the expected result")]
    public void SpeedZeroShouldProduceExpectedResult()
    {
        // Given / When
        Speed<double> zero = Speed<double>.Zero;

        // Then - magnitude must be 0 (avoids 0/0 NaN by using Time.FromSeconds(1) for the denominator).
        Assert.Equal("0.000 m/s", zero.ToString("m/s:3", CultureInfo.InvariantCulture));
        Assert.True(zero.Equals(zero));
    }

    [Fact(DisplayName = "Speed.Add should produce the expected result")]
    public void SpeedAddShouldProduceExpectedValue()
    {
        // Given - 25 m/s + 5 m/s = 30 m/s
        Speed<double> left = new(Distance<double>.FromMeters(25), Time<double>.FromSeconds(1));
        Speed<double> right = new(Distance<double>.FromMeters(5), Time<double>.FromSeconds(1));

        // When
        Speed<double> result = Speed<double>.Add(left, right);

        // Then
        Assert.Equal("30.000 m/s", result.ToString("m/s:3", CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "Speed.Add should reduce magnitudes across decompositions")]
    public void SpeedAddShouldReduceMagnitudes()
    {
        // Given - integer-ratio decompositions chosen to keep float math exact.
        // (50 m / 2 s) = 25 m/s, plus (10 m / 1 s) = 10 m/s, total = 35 m/s.
        Speed<double> twentyFiveMps = new(Distance<double>.FromMeters(50), Time<double>.FromSeconds(2));
        Speed<double> tenMps = new(Distance<double>.FromMeters(10), Time<double>.FromSeconds(1));

        // When
        Speed<double> result = Speed<double>.Add(twentyFiveMps, tenMps);

        // Then - 25 + 10 = 35 m/s
        Assert.Equal("35.000 m/s", result.ToString("m/s:3", CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "Speed.Add should reduce across mixed distance/time units")]
    public void SpeedAddShouldReduceAcrossMixedUnits()
    {
        // Given - 36 km/h = 10 m/s, plus 5 m/s = 15 m/s
        Speed<BigDecimal> left = new(Distance<BigDecimal>.FromKilometers(36), Time<BigDecimal>.FromHours(1));
        Speed<BigDecimal> right = new(Distance<BigDecimal>.FromMeters(5), Time<BigDecimal>.FromSeconds(1));

        // When
        Speed<BigDecimal> result = Speed<BigDecimal>.Add(left, right);

        // Then
        Assert.Equal("15.000 m/s", result.ToString("m/s:3", CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "Speed.Add with Zero should return an equal-magnitude speed")]
    public void SpeedAddWithZeroShouldReturnSameMagnitude()
    {
        // Given
        Speed<double> speed = new(Distance<double>.FromMeters(25), Time<double>.FromSeconds(1));

        // When
        Speed<double> result = Speed<double>.Add(speed, Speed<double>.Zero);

        // Then
        Assert.Equal(speed, result);
    }

    [Fact(DisplayName = "Speed.Subtract should produce the expected result")]
    public void SpeedSubtractShouldProduceExpectedValue()
    {
        // Given - 30 m/s - 5 m/s = 25 m/s
        Speed<double> left = new(Distance<double>.FromMeters(30), Time<double>.FromSeconds(1));
        Speed<double> right = new(Distance<double>.FromMeters(5), Time<double>.FromSeconds(1));

        // When
        Speed<double> result = Speed<double>.Subtract(left, right);

        // Then
        Assert.Equal("25.000 m/s", result.ToString("m/s:3", CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "Speed.Subtract should reduce magnitudes across decompositions")]
    public void SpeedSubtractShouldReduceMagnitudes()
    {
        // Given - (60 m / 2 s) = 30 m/s, minus (10 m / 1 s) = 10 m/s, result = 20 m/s.
        Speed<double> thirtyMps = new(Distance<double>.FromMeters(60), Time<double>.FromSeconds(2));
        Speed<double> tenMps = new(Distance<double>.FromMeters(10), Time<double>.FromSeconds(1));

        // When
        Speed<double> result = Speed<double>.Subtract(thirtyMps, tenMps);

        // Then
        Assert.Equal("20.000 m/s", result.ToString("m/s:3", CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "Speed.Subtract should produce a negative result when left is less than right")]
    public void SpeedSubtractShouldProduceNegativeWhenLeftLessThanRight()
    {
        // Given - 5 m/s - 20 m/s = -15 m/s
        Speed<double> left = new(Distance<double>.FromMeters(5), Time<double>.FromSeconds(1));
        Speed<double> right = new(Distance<double>.FromMeters(20), Time<double>.FromSeconds(1));

        // When
        Speed<double> result = Speed<double>.Subtract(left, right);

        // Then
        Assert.Equal("-15.000 m/s", result.ToString("m/s:3", CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "Speed + operator should produce the expected result")]
    public void SpeedAddOperatorShouldProduceExpectedValue()
    {
        // Given
        Speed<double> left = new(Distance<double>.FromMeters(25), Time<double>.FromSeconds(1));
        Speed<double> right = new(Distance<double>.FromMeters(5), Time<double>.FromSeconds(1));

        // When
        Speed<double> result = left + right;

        // Then
        Assert.Equal("30.000 m/s", result.ToString("m/s:3", CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "Speed - operator should produce the expected result")]
    public void SpeedSubtractOperatorShouldProduceExpectedValue()
    {
        // Given
        Speed<double> left = new(Distance<double>.FromMeters(30), Time<double>.FromSeconds(1));
        Speed<double> right = new(Distance<double>.FromMeters(5), Time<double>.FromSeconds(1));

        // When
        Speed<double> result = left - right;

        // Then
        Assert.Equal("25.000 m/s", result.ToString("m/s:3", CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "Speed instance Add should produce the expected result")]
    public void SpeedInstanceAddShouldProduceExpectedValue()
    {
        // Given
        Speed<double> left = new(Distance<double>.FromMeters(25), Time<double>.FromSeconds(1));
        Speed<double> right = new(Distance<double>.FromMeters(5), Time<double>.FromSeconds(1));

        // When
        Speed<double> result = left.Add(right);

        // Then
        Assert.Equal("30.000 m/s", result.ToString("m/s:3", CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "Speed instance Subtract should produce the expected result")]
    public void SpeedInstanceSubtractShouldProduceExpectedValue()
    {
        // Given
        Speed<double> left = new(Distance<double>.FromMeters(30), Time<double>.FromSeconds(1));
        Speed<double> right = new(Distance<double>.FromMeters(5), Time<double>.FromSeconds(1));

        // When
        Speed<double> result = left.Subtract(right);

        // Then
        Assert.Equal("25.000 m/s", result.ToString("m/s:3", CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "Speed Add and Subtract should agree across static / operator / instance forms")]
    public void SpeedAddAndSubtractShouldAgreeAcrossForms()
    {
        // Given
        Speed<double> left = new(Distance<double>.FromMeters(30), Time<double>.FromSeconds(1));
        Speed<double> right = new(Distance<double>.FromMeters(5), Time<double>.FromSeconds(1));

        // Then
        Assert.Equal(Speed<double>.Add(left, right), left + right);
        Assert.Equal(Speed<double>.Add(left, right), left.Add(right));
        Assert.Equal(Speed<double>.Subtract(left, right), left - right);
        Assert.Equal(Speed<double>.Subtract(left, right), left.Subtract(right));
    }

    [Fact(DisplayName = "Speed equality should be by magnitude (identical components)")]
    public void SpeedEqualityShouldBeByMagnitudeIdenticalComponents()
    {
        // Given
        Speed<double> left = new(Distance<double>.FromMiles(25), Time<double>.FromHours(1));
        Speed<double> right = new(Distance<double>.FromMiles(25), Time<double>.FromHours(1));

        // Then
        Assert.True(left.Equals(right));
        Assert.True(left.Equals((object)right));
        Assert.Equal(left.GetHashCode(), right.GetHashCode());
    }

    [Fact(DisplayName = "Speed equality should be by magnitude (proportional components)")]
    public void SpeedEqualityShouldBeByMagnitudeProportionalComponents()
    {
        // Given - 25 mph in two different decompositions
        Speed<double> left = new(Distance<double>.FromMiles(25), Time<double>.FromHours(1));
        Speed<double> right = new(Distance<double>.FromMiles(50), Time<double>.FromHours(2));

        // Then
        Assert.True(left.Equals(right));
        Assert.True(left.Equals((object)right));
        Assert.Equal(left.GetHashCode(), right.GetHashCode());
    }

    [Fact(DisplayName = "Speed equality should be by magnitude (cross-unit conversion)")]
    public void SpeedEqualityShouldBeByMagnitudeCrossUnit()
    {
        // Given - 1 km/h, expressed via 1000 m over 1 h. BigDecimal preserves exact precision
        // across unit conversions where binary floating-point would lose last-bit equality.
        Speed<BigDecimal> left = new(Distance<BigDecimal>.FromKilometers(1), Time<BigDecimal>.FromHours(1));
        Speed<BigDecimal> right = new(Distance<BigDecimal>.FromMeters(1000), Time<BigDecimal>.FromHours(1));

        // Then
        Assert.True(left.Equals(right));
        Assert.Equal(left.GetHashCode(), right.GetHashCode());
    }

    [Fact(DisplayName = "Speed inequality should hold for different magnitudes")]
    public void SpeedInequalityShouldHoldForDifferentMagnitudes()
    {
        // Given
        Speed<double> left = new(Distance<double>.FromMiles(25), Time<double>.FromHours(1));
        Speed<double> right = new(Distance<double>.FromMiles(25), Time<double>.FromHours(2));

        // Then
        Assert.False(left.Equals(right));
        Assert.False(left.Equals((object)right));
    }

    [Fact(DisplayName = "Speed.Equals(null) should return false")]
    public void SpeedEqualsNullShouldReturnFalse()
    {
        // Given
        Speed<double> speed = new(Distance<double>.FromMiles(25), Time<double>.FromHours(1));

        // Then
        Assert.False(speed.Equals(null));
        Assert.False(speed.Equals((object?)null));
    }

    [Fact(DisplayName = "Speed.Equals with different type should return false")]
    public void SpeedEqualsWithDifferentTypeShouldReturnFalse()
    {
        // Given
        Speed<double> speed = new(Distance<double>.FromMiles(25), Time<double>.FromHours(1));

        // Then
        Assert.False(speed.Equals("not a speed"));
    }

    [Fact(DisplayName = "Speed.CompareTo should produce the expected result (left equal to right)")]
    public void SpeedCompareToShouldProduceExpectedResultLeftEqualToRight()
    {
        // Given
        Speed<double> left = new(Distance<double>.FromMiles(50), Time<double>.FromHours(2));
        Speed<double> right = new(Distance<double>.FromMiles(25), Time<double>.FromHours(1));

        // Then
        Assert.Equal(0, left.CompareTo(right));
        Assert.Equal(0, left.CompareTo((object)right));
    }

    [Fact(DisplayName = "Speed.CompareTo should produce the expected result (left greater than right)")]
    public void SpeedCompareToShouldProduceExpectedLeftGreaterThanRight()
    {
        // Given
        Speed<double> left = new(Distance<double>.FromMiles(50), Time<double>.FromHours(1));
        Speed<double> right = new(Distance<double>.FromMiles(25), Time<double>.FromHours(1));

        // Then
        Assert.Equal(1, left.CompareTo(right));
        Assert.Equal(1, left.CompareTo((object)right));
    }

    [Fact(DisplayName = "Speed.CompareTo should produce the expected result (left less than right)")]
    public void SpeedCompareToShouldProduceExpectedLeftLessThanRight()
    {
        // Given
        Speed<double> left = new(Distance<double>.FromMiles(25), Time<double>.FromHours(2));
        Speed<double> right = new(Distance<double>.FromMiles(25), Time<double>.FromHours(1));

        // Then
        Assert.Equal(-1, left.CompareTo(right));
        Assert.Equal(-1, left.CompareTo((object)right));
    }

    [Fact(DisplayName = "Speed.CompareTo(null) should return 1")]
    public void SpeedCompareToNullShouldReturnOne()
    {
        // Given
        Speed<double> speed = new(Distance<double>.FromMiles(25), Time<double>.FromHours(1));

        // Then
        Assert.Equal(1, speed.CompareTo(null));
        Assert.Equal(1, speed.CompareTo((object?)null));
    }

    [Fact(DisplayName = "Speed.CompareTo with incompatible type should throw")]
    public void SpeedCompareToWithIncompatibleTypeShouldThrow()
    {
        // Given
        Speed<double> speed = new(Distance<double>.FromMiles(25), Time<double>.FromHours(1));

        // Then
        Assert.Throws<ArgumentException>(() => speed.CompareTo("not a speed"));
    }

    [Fact(DisplayName = "Speed.ToString with mi/h:3 should produce the expected result")]
    public void SpeedToStringWithMilesPerHourScale3ShouldProduceExpectedResult()
    {
        // Given
        Speed<double> speed = new(Distance<double>.FromMiles(25), Time<double>.FromHours(1));

        // Then
        Assert.Equal("25.000 mi/h", speed.ToString("mi/h:3", CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "Speed.ToString should reduce magnitude (50 mi / 2 h equals 25 mi/h)")]
    public void SpeedToStringShouldReduceMagnitude()
    {
        // Given
        Speed<double> speed = new(Distance<double>.FromMiles(50), Time<double>.FromHours(2));

        // Then
        Assert.Equal("25.000 mi/h", speed.ToString("mi/h:3", CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "Speed.ToString with mi/h:0 should produce the expected result")]
    public void SpeedToStringWithMilesPerHourScale0ShouldProduceExpectedResult()
    {
        // Given
        Speed<double> speed = new(Distance<double>.FromMiles(25), Time<double>.FromHours(1));

        // Then
        Assert.Equal("25 mi/h", speed.ToString("mi/h:0", CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "Speed.ToString with km/s should produce the expected result")]
    public void SpeedToStringWithKmPerSecondShouldProduceExpectedResult()
    {
        // Given - 1000 km in 1 hour = 1000 km / 3600 s ≈ 0.278 km/s
        Speed<double> speed = new(Distance<double>.FromKilometers(1000), Time<double>.FromHours(1));

        // Then
        Assert.Equal("0.278 km/s", speed.ToString("km/s:3", CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "Speed.ToString with m/s should produce the expected result")]
    public void SpeedToStringWithMetersPerSecondShouldProduceExpectedResult()
    {
        // Given - 100 m in 10 s = 10 m/s
        Speed<double> speed = new(Distance<double>.FromMeters(100), Time<double>.FromSeconds(10));

        // Then
        Assert.Equal("10.000 m/s", speed.ToString("m/s:3", CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "Speed default ToString should use m/s")]
    public void SpeedDefaultToStringShouldUseMetersPerSecond()
    {
        // Given - 36 km/h = 10 m/s
        Speed<double> speed = new(Distance<double>.FromKilometers(36), Time<double>.FromHours(1));

        // When
        string result = speed.ToString();

        // Then
        Assert.EndsWith(" m/s", result);
    }

    [Fact(DisplayName = "Speed.ToString should honor custom culture separators")]
    public void SpeedToStringShouldHonorCustomCulture()
    {
        // Given
        CultureInfo customCulture = new("de-DE");
        Speed<double> speed = new(Distance<double>.FromMiles(1234.56), Time<double>.FromHours(1));

        // When
        string formatted = speed.ToString("mi/h:2", customCulture);

        // Then
        Assert.Equal("1.234,56 mi/h", formatted);
    }

    [Fact(DisplayName = "Speed.ToString should support span-interpolation format")]
    public void SpeedToStringShouldSupportSpanInterpolation()
    {
        // Given
        Speed<double> speed = new(Distance<double>.FromMiles(25), Time<double>.FromHours(1));

        // Then - same expectation as the user's Playground example (current culture default scale).
        Assert.Equal($"{25.0.ToString("N", CultureInfo.CurrentCulture)} mi/h", $"{speed:mi/h}");
    }

    [Fact(DisplayName = "Speed.ToString should throw when format has no slash separator")]
    public void SpeedToStringShouldThrowWhenFormatHasNoSlash()
    {
        // Given
        Speed<double> speed = new(Distance<double>.FromMiles(25), Time<double>.FromHours(1));

        // Then
        Assert.Throws<FormatException>(() => speed.ToString("mph", CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "Speed.ToString should throw when distance specifier is empty")]
    public void SpeedToStringShouldThrowWhenDistanceSpecifierIsEmpty()
    {
        // Given
        Speed<double> speed = new(Distance<double>.FromMiles(25), Time<double>.FromHours(1));

        // Then
        Assert.Throws<FormatException>(() => speed.ToString("/h", CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "Speed.ToString should throw when time specifier is empty")]
    public void SpeedToStringShouldThrowWhenTimeSpecifierIsEmpty()
    {
        // Given
        Speed<double> speed = new(Distance<double>.FromMiles(25), Time<double>.FromHours(1));

        // Then
        Assert.Throws<FormatException>(() => speed.ToString("mi/", CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "Speed.ToString should throw when scale is not numeric")]
    public void SpeedToStringShouldThrowWhenScaleNotNumeric()
    {
        // Given
        Speed<double> speed = new(Distance<double>.FromMiles(25), Time<double>.FromHours(1));

        // Then
        Assert.Throws<FormatException>(() => speed.ToString("mi/h:abc", CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "Speed.ToString should throw when distance specifier is invalid")]
    public void SpeedToStringShouldThrowWhenDistanceSpecifierIsInvalid()
    {
        // Given
        Speed<double> speed = new(Distance<double>.FromMiles(25), Time<double>.FromHours(1));

        // Then
        Assert.Throws<ArgumentException>(() => speed.ToString("xx/h:3", CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "Speed.ToString should throw when time specifier is invalid")]
    public void SpeedToStringShouldThrowWhenTimeSpecifierIsInvalid()
    {
        // Given
        Speed<double> speed = new(Distance<double>.FromMiles(25), Time<double>.FromHours(1));

        // Then
        Assert.Throws<ArgumentException>(() => speed.ToString("mi/xx:3", CultureInfo.InvariantCulture));
    }
}
