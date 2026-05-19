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
        Distance<Float128> distance = Distance<Float128>.FromMiles((Float128)25);
        Time<Float128> time = Time<Float128>.FromHours((Float128)1);

        // When
        Speed<Float128> speed = new(distance, time);

        // Then
        Assert.Equal(distance, speed.Left);
        Assert.Equal(time, speed.Right);
    }

    [Fact(DisplayName = "Speed.Zero should produce zero magnitude (avoids 0/0 NaN)")]
    public void SpeedZeroShouldProduceZeroMagnitude()
    {
        // Given / When — Zero stores Distance.Zero over Time.FromSeconds(1), so the magnitude is
        // a genuine 0 and not NaN from a 0/0 ratio.
        Speed<Float128> zero = Speed<Float128>.Zero;

        // Then
        Assert.Equal(Float128.Zero, zero.Magnitude);
        Assert.True(zero.Equals(zero));
    }

    [Fact(DisplayName = "Speed.Add should produce the expected magnitude")]
    public void SpeedAddShouldProduceExpectedValue()
    {
        // Given — 25 m/s + 5 m/s = 30 m/s
        Speed<Float128> left = new(Distance<Float128>.FromMeters((Float128)25), Time<Float128>.FromSeconds((Float128)1));
        Speed<Float128> right = new(Distance<Float128>.FromMeters((Float128)5), Time<Float128>.FromSeconds((Float128)1));

        // When
        Speed<Float128> result = Speed<Float128>.Add(left, right);

        // Then
        Assert.Equal((Float128)30, result.Magnitude);
    }

    [Fact(DisplayName = "Speed.Add should reduce magnitudes across decompositions")]
    public void SpeedAddShouldReduceMagnitudes()
    {
        // Given — (50 m / 2 s) = 25 m/s, plus (10 m / 1 s) = 10 m/s, total = 35 m/s.
        // Both decompositions reduce exactly at Float128 (integer ratios within mantissa range).
        Speed<Float128> twentyFiveMps = new(Distance<Float128>.FromMeters((Float128)50), Time<Float128>.FromSeconds((Float128)2));
        Speed<Float128> tenMps = new(Distance<Float128>.FromMeters((Float128)10), Time<Float128>.FromSeconds((Float128)1));

        // When
        Speed<Float128> result = Speed<Float128>.Add(twentyFiveMps, tenMps);

        // Then
        Assert.Equal((Float128)35, result.Magnitude);
    }

    [Fact(DisplayName = "Speed.Add should reduce across mixed distance/time units")]
    public void SpeedAddShouldReduceAcrossMixedUnits()
    {
        // Given — 36 km/h = 10 m/s exactly at Float128 (36 × 10^33 / (3600 × 10^30) = 10 with no
        // residual rounding), plus 5 m/s = 15 m/s total. Replaces the original BigDecimal test
        // with Float128 strict equality.
        Speed<Float128> left = new(Distance<Float128>.FromKilometers((Float128)36), Time<Float128>.FromHours((Float128)1));
        Speed<Float128> right = new(Distance<Float128>.FromMeters((Float128)5), Time<Float128>.FromSeconds((Float128)1));

        // When
        Speed<Float128> result = Speed<Float128>.Add(left, right);

        // Then
        Assert.Equal((Float128)15, result.Magnitude);
    }

    [Fact(DisplayName = "Speed.Add with Zero should return an equal-magnitude speed")]
    public void SpeedAddWithZeroShouldReturnSameMagnitude()
    {
        // Given
        Speed<Float128> speed = new(Distance<Float128>.FromMeters((Float128)25), Time<Float128>.FromSeconds((Float128)1));

        // When
        Speed<Float128> result = Speed<Float128>.Add(speed, Speed<Float128>.Zero);

        // Then
        Assert.Equal(speed, result);
    }

    [Fact(DisplayName = "Speed.Subtract should produce the expected magnitude")]
    public void SpeedSubtractShouldProduceExpectedValue()
    {
        // Given — 30 m/s - 5 m/s = 25 m/s
        Speed<Float128> left = new(Distance<Float128>.FromMeters((Float128)30), Time<Float128>.FromSeconds((Float128)1));
        Speed<Float128> right = new(Distance<Float128>.FromMeters((Float128)5), Time<Float128>.FromSeconds((Float128)1));

        // When
        Speed<Float128> result = Speed<Float128>.Subtract(left, right);

        // Then
        Assert.Equal((Float128)25, result.Magnitude);
    }

    [Fact(DisplayName = "Speed.Subtract should reduce magnitudes across decompositions")]
    public void SpeedSubtractShouldReduceMagnitudes()
    {
        // Given — (60 m / 2 s) = 30 m/s, minus (10 m / 1 s) = 10 m/s, result = 20 m/s.
        Speed<Float128> thirtyMps = new(Distance<Float128>.FromMeters((Float128)60), Time<Float128>.FromSeconds((Float128)2));
        Speed<Float128> tenMps = new(Distance<Float128>.FromMeters((Float128)10), Time<Float128>.FromSeconds((Float128)1));

        // When
        Speed<Float128> result = Speed<Float128>.Subtract(thirtyMps, tenMps);

        // Then
        Assert.Equal((Float128)20, result.Magnitude);
    }

    [Fact(DisplayName = "Speed.Subtract should produce a negative result when left is less than right")]
    public void SpeedSubtractShouldProduceNegativeWhenLeftLessThanRight()
    {
        // Given — 5 m/s - 20 m/s = -15 m/s
        Speed<Float128> left = new(Distance<Float128>.FromMeters((Float128)5), Time<Float128>.FromSeconds((Float128)1));
        Speed<Float128> right = new(Distance<Float128>.FromMeters((Float128)20), Time<Float128>.FromSeconds((Float128)1));

        // When
        Speed<Float128> result = Speed<Float128>.Subtract(left, right);

        // Then
        Assert.Equal(-(Float128)15, result.Magnitude);
    }

    [Fact(DisplayName = "Speed + operator should produce the expected result")]
    public void SpeedAddOperatorShouldProduceExpectedValue()
    {
        // Given
        Speed<Float128> left = new(Distance<Float128>.FromMeters((Float128)25), Time<Float128>.FromSeconds((Float128)1));
        Speed<Float128> right = new(Distance<Float128>.FromMeters((Float128)5), Time<Float128>.FromSeconds((Float128)1));

        // When
        Speed<Float128> result = left + right;

        // Then
        Assert.Equal((Float128)30, result.Magnitude);
    }

    [Fact(DisplayName = "Speed - operator should produce the expected result")]
    public void SpeedSubtractOperatorShouldProduceExpectedValue()
    {
        // Given
        Speed<Float128> left = new(Distance<Float128>.FromMeters((Float128)30), Time<Float128>.FromSeconds((Float128)1));
        Speed<Float128> right = new(Distance<Float128>.FromMeters((Float128)5), Time<Float128>.FromSeconds((Float128)1));

        // When
        Speed<Float128> result = left - right;

        // Then
        Assert.Equal((Float128)25, result.Magnitude);
    }

    [Fact(DisplayName = "Speed instance Add should produce the expected result")]
    public void SpeedInstanceAddShouldProduceExpectedValue()
    {
        // Given
        Speed<Float128> left = new(Distance<Float128>.FromMeters((Float128)25), Time<Float128>.FromSeconds((Float128)1));
        Speed<Float128> right = new(Distance<Float128>.FromMeters((Float128)5), Time<Float128>.FromSeconds((Float128)1));

        // When
        Speed<Float128> result = left.Add(right);

        // Then
        Assert.Equal((Float128)30, result.Magnitude);
    }

    [Fact(DisplayName = "Speed instance Subtract should produce the expected result")]
    public void SpeedInstanceSubtractShouldProduceExpectedValue()
    {
        // Given
        Speed<Float128> left = new(Distance<Float128>.FromMeters((Float128)30), Time<Float128>.FromSeconds((Float128)1));
        Speed<Float128> right = new(Distance<Float128>.FromMeters((Float128)5), Time<Float128>.FromSeconds((Float128)1));

        // When
        Speed<Float128> result = left.Subtract(right);

        // Then
        Assert.Equal((Float128)25, result.Magnitude);
    }

    [Fact(DisplayName = "Speed Add and Subtract should agree across static / operator / instance forms")]
    public void SpeedAddAndSubtractShouldAgreeAcrossForms()
    {
        // Given
        Speed<Float128> left = new(Distance<Float128>.FromMeters((Float128)30), Time<Float128>.FromSeconds((Float128)1));
        Speed<Float128> right = new(Distance<Float128>.FromMeters((Float128)5), Time<Float128>.FromSeconds((Float128)1));

        // Then
        Assert.Equal(Speed<Float128>.Add(left, right), left + right);
        Assert.Equal(Speed<Float128>.Add(left, right), left.Add(right));
        Assert.Equal(Speed<Float128>.Subtract(left, right), left - right);
        Assert.Equal(Speed<Float128>.Subtract(left, right), left.Subtract(right));
    }

    [Fact(DisplayName = "Speed equality should be by magnitude (identical components)")]
    public void SpeedEqualityShouldBeByMagnitudeIdenticalComponents()
    {
        // Given
        Speed<Float128> left = new(Distance<Float128>.FromMiles((Float128)25), Time<Float128>.FromHours((Float128)1));
        Speed<Float128> right = new(Distance<Float128>.FromMiles((Float128)25), Time<Float128>.FromHours((Float128)1));

        // Then
        Assert.True(left.Equals(right));
        Assert.True(left.Equals((object)right));
        Assert.Equal(left.GetHashCode(), right.GetHashCode());
    }

    [Fact(DisplayName = "Speed equality should be by magnitude (proportional components)")]
    public void SpeedEqualityShouldBeByMagnitudeProportionalComponents()
    {
        // Given — 25 mph in two different decompositions
        Speed<Float128> left = new(Distance<Float128>.FromMiles((Float128)25), Time<Float128>.FromHours((Float128)1));
        Speed<Float128> right = new(Distance<Float128>.FromMiles((Float128)50), Time<Float128>.FromHours((Float128)2));

        // Then
        Assert.True(left.Equals(right));
        Assert.True(left.Equals((object)right));
        Assert.Equal(left.GetHashCode(), right.GetHashCode());
    }

    [Fact(DisplayName = "Speed equality should be by magnitude (cross-unit conversion)")]
    public void SpeedEqualityShouldBeByMagnitudeCrossUnit()
    {
        // Given — 1 km/h vs 1000 m/h reduce to the same Float128 canonical because the conversion
        // factors are integer powers of ten exactly representable in Float128's 113-bit mantissa.
        // Replaces the original BigDecimal-based test with Float128 strict equality.
        Speed<Float128> left = new(Distance<Float128>.FromKilometers((Float128)1), Time<Float128>.FromHours((Float128)1));
        Speed<Float128> right = new(Distance<Float128>.FromMeters((Float128)1000), Time<Float128>.FromHours((Float128)1));

        // Then
        Assert.True(left.Equals(right));
        Assert.Equal(left.GetHashCode(), right.GetHashCode());
    }

    [Fact(DisplayName = "Speed inequality should hold for different magnitudes")]
    public void SpeedInequalityShouldHoldForDifferentMagnitudes()
    {
        // Given
        Speed<Float128> left = new(Distance<Float128>.FromMiles((Float128)25), Time<Float128>.FromHours((Float128)1));
        Speed<Float128> right = new(Distance<Float128>.FromMiles((Float128)25), Time<Float128>.FromHours((Float128)2));

        // Then
        Assert.False(left.Equals(right));
        Assert.False(left.Equals((object)right));
    }

    [Fact(DisplayName = "Speed.Equals(null) should return false")]
    public void SpeedEqualsNullShouldReturnFalse()
    {
        // Given
        Speed<Float128> speed = new(Distance<Float128>.FromMiles((Float128)25), Time<Float128>.FromHours((Float128)1));

        // Then
        Assert.False(speed.Equals(null));
        Assert.False(speed.Equals((object?)null));
    }

    [Fact(DisplayName = "Speed.Equals with different type should return false")]
    public void SpeedEqualsWithDifferentTypeShouldReturnFalse()
    {
        // Given
        Speed<Float128> speed = new(Distance<Float128>.FromMiles((Float128)25), Time<Float128>.FromHours((Float128)1));

        // Then
        Assert.False(speed.Equals("not a speed"));
    }

    [Fact(DisplayName = "Speed.CompareTo should produce the expected result (left equal to right)")]
    public void SpeedCompareToShouldProduceExpectedResultLeftEqualToRight()
    {
        // Given
        Speed<Float128> left = new(Distance<Float128>.FromMiles((Float128)50), Time<Float128>.FromHours((Float128)2));
        Speed<Float128> right = new(Distance<Float128>.FromMiles((Float128)25), Time<Float128>.FromHours((Float128)1));

        // Then
        Assert.Equal(0, left.CompareTo(right));
        Assert.Equal(0, left.CompareTo((object)right));
    }

    [Fact(DisplayName = "Speed.CompareTo should produce the expected result (left greater than right)")]
    public void SpeedCompareToShouldProduceExpectedLeftGreaterThanRight()
    {
        // Given
        Speed<Float128> left = new(Distance<Float128>.FromMiles((Float128)50), Time<Float128>.FromHours((Float128)1));
        Speed<Float128> right = new(Distance<Float128>.FromMiles((Float128)25), Time<Float128>.FromHours((Float128)1));

        // Then
        Assert.Equal(1, left.CompareTo(right));
        Assert.Equal(1, left.CompareTo((object)right));
    }

    [Fact(DisplayName = "Speed.CompareTo should produce the expected result (left less than right)")]
    public void SpeedCompareToShouldProduceExpectedLeftLessThanRight()
    {
        // Given
        Speed<Float128> left = new(Distance<Float128>.FromMiles((Float128)25), Time<Float128>.FromHours((Float128)2));
        Speed<Float128> right = new(Distance<Float128>.FromMiles((Float128)25), Time<Float128>.FromHours((Float128)1));

        // Then
        Assert.Equal(-1, left.CompareTo(right));
        Assert.Equal(-1, left.CompareTo((object)right));
    }

    [Fact(DisplayName = "Speed.CompareTo(null) should return 1")]
    public void SpeedCompareToNullShouldReturnOne()
    {
        // Given
        Speed<Float128> speed = new(Distance<Float128>.FromMiles((Float128)25), Time<Float128>.FromHours((Float128)1));

        // Then
        Assert.Equal(1, speed.CompareTo(null));
        Assert.Equal(1, speed.CompareTo((object?)null));
    }

    [Fact(DisplayName = "Speed.CompareTo with incompatible type should throw")]
    public void SpeedCompareToWithIncompatibleTypeShouldThrow()
    {
        // Given
        Speed<Float128> speed = new(Distance<Float128>.FromMiles((Float128)25), Time<Float128>.FromHours((Float128)1));

        // Then
        Assert.Throws<ArgumentException>(() => speed.CompareTo("not a speed"));
    }

    [Fact(DisplayName = "Speed.ToString with mi/h:3 should produce the expected result")]
    public void SpeedToStringWithMilesPerHourScale3ShouldProduceExpectedResult()
    {
        // Given
        Speed<Float128> speed = new(Distance<Float128>.FromMiles((Float128)25), Time<Float128>.FromHours((Float128)1));

        // Then
        Assert.Equal("25.000 mi/h", speed.ToString("mi/h:3", CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "Speed.ToString should reduce magnitude (50 mi / 2 h equals 25 mi/h)")]
    public void SpeedToStringShouldReduceMagnitude()
    {
        // Given
        Speed<Float128> speed = new(Distance<Float128>.FromMiles((Float128)50), Time<Float128>.FromHours((Float128)2));

        // Then
        Assert.Equal("25.000 mi/h", speed.ToString("mi/h:3", CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "Speed.ToString with mi/h:0 should produce the expected result")]
    public void SpeedToStringWithMilesPerHourScale0ShouldProduceExpectedResult()
    {
        // Given
        Speed<Float128> speed = new(Distance<Float128>.FromMiles((Float128)25), Time<Float128>.FromHours((Float128)1));

        // Then
        Assert.Equal("25 mi/h", speed.ToString("mi/h:0", CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "Speed.ToString with km/s should produce the expected result")]
    public void SpeedToStringWithKmPerSecondShouldProduceExpectedResult()
    {
        // Given — 1000 km in 1 hour = 1000 km / 3600 s ≈ 0.278 km/s
        Speed<Float128> speed = new(Distance<Float128>.FromKilometers((Float128)1000), Time<Float128>.FromHours((Float128)1));

        // Then
        Assert.Equal("0.278 km/s", speed.ToString("km/s:3", CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "Speed.ToString with m/s should produce the expected result")]
    public void SpeedToStringWithMetersPerSecondShouldProduceExpectedResult()
    {
        // Given — 100 m in 10 s = 10 m/s
        Speed<Float128> speed = new(Distance<Float128>.FromMeters((Float128)100), Time<Float128>.FromSeconds((Float128)10));

        // Then
        Assert.Equal("10.000 m/s", speed.ToString("m/s:3", CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "Speed default ToString should use m/s")]
    public void SpeedDefaultToStringShouldUseMetersPerSecond()
    {
        // Given — 36 km/h = 10 m/s
        Speed<Float128> speed = new(Distance<Float128>.FromKilometers((Float128)36), Time<Float128>.FromHours((Float128)1));

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
        Speed<Float128> speed = new(Distance<Float128>.FromMiles(Float128.Parse("1234.56")), Time<Float128>.FromHours((Float128)1));

        // When
        string formatted = speed.ToString("mi/h:2", customCulture);

        // Then
        Assert.Equal("1.234,56 mi/h", formatted);
    }

    [Fact(DisplayName = "Speed.ToString should support span-interpolation format")]
    public void SpeedToStringShouldSupportSpanInterpolation()
    {
        // Given
        Speed<Float128> speed = new(Distance<Float128>.FromMiles((Float128)25), Time<Float128>.FromHours((Float128)1));

        // Then — magnitude formatted via current culture's default scale, then unit suffix.
        Assert.Equal($"{((Float128)25).ToString("N", CultureInfo.CurrentCulture)} mi/h", $"{speed:mi/h}");
    }

    [Fact(DisplayName = "Speed.ToString should throw when format has no slash separator")]
    public void SpeedToStringShouldThrowWhenFormatHasNoSlash()
    {
        // Given
        Speed<Float128> speed = new(Distance<Float128>.FromMiles((Float128)25), Time<Float128>.FromHours((Float128)1));

        // Then
        Assert.Throws<FormatException>(() => speed.ToString("mph", CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "Speed.ToString should throw when distance specifier is empty")]
    public void SpeedToStringShouldThrowWhenDistanceSpecifierIsEmpty()
    {
        // Given
        Speed<Float128> speed = new(Distance<Float128>.FromMiles((Float128)25), Time<Float128>.FromHours((Float128)1));

        // Then
        Assert.Throws<FormatException>(() => speed.ToString("/h", CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "Speed.ToString should throw when time specifier is empty")]
    public void SpeedToStringShouldThrowWhenTimeSpecifierIsEmpty()
    {
        // Given
        Speed<Float128> speed = new(Distance<Float128>.FromMiles((Float128)25), Time<Float128>.FromHours((Float128)1));

        // Then
        Assert.Throws<FormatException>(() => speed.ToString("mi/", CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "Speed.ToString should throw when scale is not numeric")]
    public void SpeedToStringShouldThrowWhenScaleNotNumeric()
    {
        // Given
        Speed<Float128> speed = new(Distance<Float128>.FromMiles((Float128)25), Time<Float128>.FromHours((Float128)1));

        // Then
        Assert.Throws<FormatException>(() => speed.ToString("mi/h:abc", CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "Speed.ToString should throw when distance specifier is invalid")]
    public void SpeedToStringShouldThrowWhenDistanceSpecifierIsInvalid()
    {
        // Given
        Speed<Float128> speed = new(Distance<Float128>.FromMiles((Float128)25), Time<Float128>.FromHours((Float128)1));

        // Then
        Assert.Throws<ArgumentException>(() => speed.ToString("xx/h:3", CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "Speed.ToString should throw when time specifier is invalid")]
    public void SpeedToStringShouldThrowWhenTimeSpecifierIsInvalid()
    {
        // Given
        Speed<Float128> speed = new(Distance<Float128>.FromMiles((Float128)25), Time<Float128>.FromHours((Float128)1));

        // Then
        Assert.Throws<ArgumentException>(() => speed.ToString("mi/xx:3", CultureInfo.InvariantCulture));
    }
}
