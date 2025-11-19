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

namespace OnixLabs.Units.UnitTests;

public sealed class SpeedTests
{
    // IEEE-754 binary floating-point arithmetic causes small discrepancies in calculation, therefore we need a tolerance.
    private const double Tolerance = 1e+42;

    [Fact(DisplayName = "Speed.Zero should produce the expected result")]
    public void SpeedZeroShouldProduceExpectedResult()
    {
        // Given / When
        Speed<double> speed = Speed<double>.Zero;

        // Then
        Assert.Equal(0.0, speed.QuectoMetersPerSecond, Tolerance);
    }

    [Theory(DisplayName = "Speed.FromQuectometersPerSecond should produce the expected QuectoMetersPerSecond")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1.0)]
    [InlineData(2.5, 2.5)]
    public void SpeedFromQuectometersPerSecondShouldProduceExpectedQuectoMetersPerSecond(double value, double expectedQuectoMetersPerSecond)
    {
        // Given / When
        Speed<double> speed = Speed<double>.FromQuectometersPerSecond(value);

        // Then
        Assert.Equal(expectedQuectoMetersPerSecond, speed.QuectoMetersPerSecond, Tolerance);
    }

    [Theory(DisplayName = "Speed.FromRontometersPerSecond should produce the expected QuectoMetersPerSecond")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e3)]
    [InlineData(2.5, 2.5e3)]
    public void SpeedFromRontometersPerSecondShouldProduceExpectedQuectoMetersPerSecond(double value, double expectedQuectoMetersPerSecond)
    {
        // Given / When
        Speed<double> speed = Speed<double>.FromRontometersPerSecond(value);

        // Then
        Assert.Equal(expectedQuectoMetersPerSecond, speed.QuectoMetersPerSecond, Tolerance);
    }

    [Theory(DisplayName = "Speed.FromYoctometersPerSecond should produce the expected QuectoMetersPerSecond")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e6)]
    [InlineData(2.5, 2.5e6)]
    public void SpeedFromYoctometersPerSecondShouldProduceExpectedQuectoMetersPerSecond(double value, double expectedQuectoMetersPerSecond)
    {
        // Given / When
        Speed<double> speed = Speed<double>.FromYoctometersPerSecond(value);

        // Then
        Assert.Equal(expectedQuectoMetersPerSecond, speed.QuectoMetersPerSecond, Tolerance);
    }

    [Theory(DisplayName = "Speed.FromZeptometersPerSecond should produce the expected QuectoMetersPerSecond")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e9)]
    [InlineData(2.5, 2.5e9)]
    public void SpeedFromZeptometersPerSecondShouldProduceExpectedQuectoMetersPerSecond(double value, double expectedQuectoMetersPerSecond)
    {
        // Given / When
        Speed<double> speed = Speed<double>.FromZeptometersPerSecond(value);

        // Then
        Assert.Equal(expectedQuectoMetersPerSecond, speed.QuectoMetersPerSecond, Tolerance);
    }

    [Theory(DisplayName = "Speed.FromAttometersPerSecond should produce the expected QuectoMetersPerSecond")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e12)]
    [InlineData(2.5, 2.5e12)]
    public void SpeedFromAttometersPerSecondShouldProduceExpectedQuectoMetersPerSecond(double value, double expectedQuectoMetersPerSecond)
    {
        // Given / When
        Speed<double> speed = Speed<double>.FromAttometersPerSecond(value);

        // Then
        Assert.Equal(expectedQuectoMetersPerSecond, speed.QuectoMetersPerSecond, Tolerance);
    }

    [Theory(DisplayName = "Speed.FromFemtometersPerSecond should produce the expected QuectoMetersPerSecond")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e15)]
    [InlineData(2.5, 2.5e15)]
    public void SpeedFromFemtometersPerSecondShouldProduceExpectedQuectoMetersPerSecond(double value, double expectedQuectoMetersPerSecond)
    {
        // Given / When
        Speed<double> speed = Speed<double>.FromFemtometersPerSecond(value);

        // Then
        Assert.Equal(expectedQuectoMetersPerSecond, speed.QuectoMetersPerSecond, Tolerance);
    }

    [Theory(DisplayName = "Speed.FromPicometersPerSecond should produce the expected QuectoMetersPerSecond")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e18)]
    [InlineData(2.5, 2.5e18)]
    public void SpeedFromPicometersPerSecondShouldProduceExpectedQuectoMetersPerSecond(double value, double expectedQuectoMetersPerSecond)
    {
        // Given / When
        Speed<double> speed = Speed<double>.FromPicometersPerSecond(value);

        // Then
        Assert.Equal(expectedQuectoMetersPerSecond, speed.QuectoMetersPerSecond, Tolerance);
    }

    [Theory(DisplayName = "Speed.FromNanometersPerSecond should produce the expected QuectoMetersPerSecond")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e21)]
    [InlineData(2.5, 2.5e21)]
    public void SpeedFromNanometersPerSecondShouldProduceExpectedQuectoMetersPerSecond(double value, double expectedQuectoMetersPerSecond)
    {
        // Given / When
        Speed<double> speed = Speed<double>.FromNanometersPerSecond(value);

        // Then
        Assert.Equal(expectedQuectoMetersPerSecond, speed.QuectoMetersPerSecond, Tolerance);
    }

    [Theory(DisplayName = "Speed.FromMicrometersPerSecond should produce the expected QuectoMetersPerSecond")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e24)]
    [InlineData(2.5, 2.5e24)]
    public void SpeedFromMicrometersPerSecondShouldProduceExpectedQuectoMetersPerSecond(double value, double expectedQuectoMetersPerSecond)
    {
        // Given / When
        Speed<double> speed = Speed<double>.FromMicrometersPerSecond(value);

        // Then
        Assert.Equal(expectedQuectoMetersPerSecond, speed.QuectoMetersPerSecond, Tolerance);
    }

    [Theory(DisplayName = "Speed.FromMillimetersPerSecond should produce the expected QuectoMetersPerSecond")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e27)]
    [InlineData(2.5, 2.5e27)]
    public void SpeedFromMillimetersPerSecondShouldProduceExpectedQuectoMetersPerSecond(double value, double expectedQuectoMetersPerSecond)
    {
        // Given / When
        Speed<double> speed = Speed<double>.FromMillimetersPerSecond(value);

        // Then
        Assert.Equal(expectedQuectoMetersPerSecond, speed.QuectoMetersPerSecond, Tolerance);
    }

    [Theory(DisplayName = "Speed.FromCentimetersPerSecond should produce the expected QuectoMetersPerSecond")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e28)]
    [InlineData(2.5, 2.5e28)]
    public void SpeedFromCentimetersPerSecondShouldProduceExpectedQuectoMetersPerSecond(double value, double expectedQuectoMetersPerSecond)
    {
        // Given / When
        Speed<double> speed = Speed<double>.FromCentimetersPerSecond(value);

        // Then
        Assert.Equal(expectedQuectoMetersPerSecond, speed.QuectoMetersPerSecond, Tolerance);
    }

    [Theory(DisplayName = "Speed.FromDecimetersPerSecond should produce the expected QuectoMetersPerSecond")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e29)]
    [InlineData(2.5, 2.5e29)]
    public void SpeedFromDecimetersPerSecondShouldProduceExpectedQuectoMetersPerSecond(double value, double expectedQuectoMetersPerSecond)
    {
        // Given / When
        Speed<double> speed = Speed<double>.FromDecimetersPerSecond(value);

        // Then
        Assert.Equal(expectedQuectoMetersPerSecond, speed.QuectoMetersPerSecond, Tolerance);
    }

    [Theory(DisplayName = "Speed.FromMetersPerSecond should produce the expected QuectoMetersPerSecond")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e30)]
    [InlineData(2.5, 2.5e30)]
    public void SpeedFromMetersPerSecondShouldProduceExpectedQuectoMetersPerSecond(double value, double expectedQuectoMetersPerSecond)
    {
        // Given / When
        Speed<double> speed = Speed<double>.FromMetersPerSecond(value);

        // Then
        Assert.Equal(expectedQuectoMetersPerSecond, speed.QuectoMetersPerSecond, Tolerance);
    }

    [Theory(DisplayName = "Speed.FromDecametersPerSecond should produce the expected QuectoMetersPerSecond")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e31)]
    [InlineData(2.5, 2.5e31)]
    public void SpeedFromDecametersPerSecondShouldProduceExpectedQuectoMetersPerSecond(double value, double expectedQuectoMetersPerSecond)
    {
        // Given / When
        Speed<double> speed = Speed<double>.FromDecametersPerSecond(value);

        // Then
        Assert.Equal(expectedQuectoMetersPerSecond, speed.QuectoMetersPerSecond, Tolerance);
    }

    [Theory(DisplayName = "Speed.FromHectometersPerSecond should produce the expected QuectoMetersPerSecond")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e32)]
    [InlineData(2.5, 2.5e32)]
    public void SpeedFromHectometersPerSecondShouldProduceExpectedQuectoMetersPerSecond(double value, double expectedQuectoMetersPerSecond)
    {
        // Given / When
        Speed<double> speed = Speed<double>.FromHectometersPerSecond(value);

        // Then
        Assert.Equal(expectedQuectoMetersPerSecond, speed.QuectoMetersPerSecond, Tolerance);
    }

    [Theory(DisplayName = "Speed.FromKilometersPerSecond should produce the expected QuectoMetersPerSecond")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e33)]
    [InlineData(2.5, 2.5e33)]
    public void SpeedFromKilometersPerSecondShouldProduceExpectedQuectoMetersPerSecond(double value, double expectedQuectoMetersPerSecond)
    {
        // Given / When
        Speed<double> speed = Speed<double>.FromKilometersPerSecond(value);

        // Then
        Assert.Equal(expectedQuectoMetersPerSecond, speed.QuectoMetersPerSecond, Tolerance);
    }

    [Theory(DisplayName = "Speed.FromMegametersPerSecond should produce the expected QuectoMetersPerSecond")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e36)]
    [InlineData(2.5, 2.5e36)]
    public void SpeedFromMegametersPerSecondShouldProduceExpectedQuectoMetersPerSecond(double value, double expectedQuectoMetersPerSecond)
    {
        // Given / When
        Speed<double> speed = Speed<double>.FromMegametersPerSecond(value);

        // Then
        Assert.Equal(expectedQuectoMetersPerSecond, speed.QuectoMetersPerSecond, Tolerance);
    }

    [Theory(DisplayName = "Speed.FromGigametersPerSecond should produce the expected QuectoMetersPerSecond")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e39)]
    [InlineData(2.5, 2.5e39)]
    public void SpeedFromGigametersPerSecondShouldProduceExpectedQuectoMetersPerSecond(double value, double expectedQuectoMetersPerSecond)
    {
        // Given / When
        Speed<double> speed = Speed<double>.FromGigametersPerSecond(value);

        // Then
        Assert.Equal(expectedQuectoMetersPerSecond, speed.QuectoMetersPerSecond, Tolerance);
    }

    [Theory(DisplayName = "Speed.FromTerametersPerSecond should produce the expected QuectoMetersPerSecond")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e42)]
    [InlineData(2.5, 2.5e42)]
    public void SpeedFromTerametersPerSecondShouldProduceExpectedQuectoMetersPerSecond(double value, double expectedQuectoMetersPerSecond)
    {
        // Given / When
        Speed<double> speed = Speed<double>.FromTerametersPerSecond(value);

        // Then
        Assert.Equal(expectedQuectoMetersPerSecond, speed.QuectoMetersPerSecond, Tolerance);
    }

    [Theory(DisplayName = "Speed.FromPetametersPerSecond should produce the expected QuectoMetersPerSecond")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e45)]
    [InlineData(2.5, 2.5e45)]
    public void SpeedFromPetametersPerSecondShouldProduceExpectedQuectoMetersPerSecond(double value, double expectedQuectoMetersPerSecond)
    {
        // Given / When
        Speed<double> speed = Speed<double>.FromPetametersPerSecond(value);

        // Then
        Assert.Equal(expectedQuectoMetersPerSecond, speed.QuectoMetersPerSecond, Tolerance);
    }

    [Theory(DisplayName = "Speed.FromExametersPerSecond should produce the expected QuectoMetersPerSecond")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e48)]
    [InlineData(2.5, 2.5e48)]
    public void SpeedFromExametersPerSecondShouldProduceExpectedQuectoMetersPerSecond(double value, double expectedQuectoMetersPerSecond)
    {
        // Given / When
        Speed<double> speed = Speed<double>.FromExametersPerSecond(value);

        // Then
        Assert.Equal(expectedQuectoMetersPerSecond, speed.QuectoMetersPerSecond, Tolerance);
    }

    [Theory(DisplayName = "Speed.FromZettametersPerSecond should produce the expected QuectoMetersPerSecond")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e51)]
    [InlineData(2.5, 2.5e51)]
    public void SpeedFromZettametersPerSecondShouldProduceExpectedQuectoMetersPerSecond(double value, double expectedQuectoMetersPerSecond)
    {
        // Given / When
        Speed<double> speed = Speed<double>.FromZettametersPerSecond(value);

        // Then
        Assert.Equal(expectedQuectoMetersPerSecond, speed.QuectoMetersPerSecond, Tolerance);
    }

    [Theory(DisplayName = "Speed.FromYottametersPerSecond should produce the expected QuectoMetersPerSecond")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e54)]
    [InlineData(2.5, 2.5e54)]
    public void SpeedFromYottametersPerSecondShouldProduceExpectedQuectoMetersPerSecond(double value, double expectedQuectoMetersPerSecond)
    {
        // Given / When
        Speed<double> speed = Speed<double>.FromYottametersPerSecond(value);

        // Then
        Assert.Equal(expectedQuectoMetersPerSecond, speed.QuectoMetersPerSecond, Tolerance);
    }

    [Theory(DisplayName = "Speed.FromRonnametersPerSecond should produce the expected QuectoMetersPerSecond")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e57)]
    [InlineData(2.5, 2.5e57)]
    public void SpeedFromRonnametersPerSecondShouldProduceExpectedQuectoMetersPerSecond(double value, double expectedQuectoMetersPerSecond)
    {
        // Given / When
        Speed<double> speed = Speed<double>.FromRonnametersPerSecond(value);

        // Then
        Assert.Equal(expectedQuectoMetersPerSecond, speed.QuectoMetersPerSecond, Tolerance);
    }

    [Theory(DisplayName = "Speed.FromQuettametersPerSecond should produce the expected QuectoMetersPerSecond")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e60)]
    [InlineData(2.5, 2.5e60)]
    public void SpeedFromQuettametersPerSecondShouldProduceExpectedQuectoMetersPerSecond(double value, double expectedQuectoMetersPerSecond)
    {
        // Given / When
        Speed<double> speed = Speed<double>.FromQuettametersPerSecond(value);

        // Then
        Assert.Equal(expectedQuectoMetersPerSecond, speed.QuectoMetersPerSecond, Tolerance);
    }

    [Theory(DisplayName = "Speed.FromInchesPerSecond should produce the expected QuectoMetersPerSecond")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 2.54e28)]
    [InlineData(2.5, 6.35e28)]
    [InlineData(100.0, 2.54e30)]
    public void SpeedFromInchesPerSecondShouldProduceExpectedQuectoMetersPerSecond(double value, double expectedQuectoMetersPerSecond)
    {
        // Given / When
        Speed<double> speed = Speed<double>.FromInchesPerSecond(value);

        // Then
        Assert.Equal(expectedQuectoMetersPerSecond, speed.QuectoMetersPerSecond, Tolerance);
    }

    [Theory(DisplayName = "Speed.FromFeetPerSecond should produce the expected QuectoMetersPerSecond")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 3.048e29)]
    [InlineData(2.5, 7.62e29)]
    [InlineData(100.0, 3.048e31)]
    public void SpeedFromFeetPerSecondShouldProduceExpectedQuectoMetersPerSecond(double value, double expectedQuectoMetersPerSecond)
    {
        // Given / When
        Speed<double> speed = Speed<double>.FromFeetPerSecond(value);

        // Then
        Assert.Equal(expectedQuectoMetersPerSecond, speed.QuectoMetersPerSecond, Tolerance);
    }

    [Theory(DisplayName = "Speed.FromKilometersPerHour should produce the expected QuectoMetersPerSecond")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 2.777777777777778e29)]
    [InlineData(2.5, 6.944444444444445e29)]
    [InlineData(60.0, 1.666666666666667e31)]
    public void SpeedFromKilometersPerHourShouldProduceExpectedQuectoMetersPerSecond(double value, double expectedQuectoMetersPerSecond)
    {
        // Given / When
        Speed<double> speed = Speed<double>.FromKilometersPerHour(value);

        // Then
        Assert.Equal(expectedQuectoMetersPerSecond, speed.QuectoMetersPerSecond, Tolerance);
    }

    [Theory(DisplayName = "Speed.FromMilesPerHour should produce the expected QuectoMetersPerSecond")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 4.4704e29)]
    [InlineData(2.5, 1.1176e30)]
    [InlineData(60.0, 2.68224e31)]
    public void SpeedFromMilesPerHourShouldProduceExpectedQuectoMetersPerSecond(double value, double expectedQuectoMetersPerSecond)
    {
        // Given / When
        Speed<double> speed = Speed<double>.FromMilesPerHour(value);

        // Then
        Assert.Equal(expectedQuectoMetersPerSecond, speed.QuectoMetersPerSecond, Tolerance);
    }

    [Theory(DisplayName = "Speed.FromKnots should produce the expected QuectoMetersPerSecond")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 5.144444444444445e29)]
    [InlineData(2.5, 1.2861111111111113e30)]
    [InlineData(10.0, 5.144444444444445e30)]
    public void SpeedFromKnotsShouldProduceExpectedQuectoMetersPerSecond(double value, double expectedQuectoMetersPerSecond)
    {
        // Given / When
        Speed<double> speed = Speed<double>.FromKnots(value);

        // Then
        Assert.Equal(expectedQuectoMetersPerSecond, speed.QuectoMetersPerSecond, Tolerance);
    }

    [Fact(DisplayName = "Speed.Add should produce the expected result")]
    public void SpeedAddShouldProduceExpectedValue()
    {
        // Given
        Speed<double> left = Speed<double>.FromMetersPerSecond(15.0);
        Speed<double> right = Speed<double>.FromMetersPerSecond(5.0);

        // When
        Speed<double> result = left.Add(right);

        // Then
        Assert.Equal(20.0, result.MetersPerSecond, Tolerance);
    }

    [Fact(DisplayName = "Speed.Subtract should produce the expected result")]
    public void SpeedSubtractShouldProduceExpectedValue()
    {
        // Given
        Speed<double> left = Speed<double>.FromMetersPerSecond(15.0);
        Speed<double> right = Speed<double>.FromMetersPerSecond(5.0);

        // When
        Speed<double> result = left.Subtract(right);

        // Then
        Assert.Equal(10.0, result.MetersPerSecond, Tolerance);
    }

    [Fact(DisplayName = "Speed.Multiply should produce the expected result")]
    public void SpeedMultiplyShouldProduceExpectedValue()
    {
        // Given
        Speed<double> left = Speed<double>.FromMetersPerSecond(10.0); // 1e31 qm/s
        Speed<double> right = Speed<double>.FromMetersPerSecond(3.0); // 3e30 qm/s

        // When
        Speed<double> result = left.Multiply(right); // 1e31 * 3e30 = 3e61 qm/s

        // Then
        Assert.Equal(1e31, left.QuectoMetersPerSecond, Tolerance);
        Assert.Equal(3e30, right.QuectoMetersPerSecond, Tolerance);
        Assert.Equal(3e61, result.QuectoMetersPerSecond, Tolerance);
        Assert.Equal(3e31, result.MetersPerSecond, Tolerance);
    }

    [Fact(DisplayName = "Speed.Divide should produce the expected result")]
    public void SpeedDivideShouldProduceExpectedValue()
    {
        // Given
        Speed<double> left = Speed<double>.FromMetersPerSecond(10.0); // 1e31 qm/s
        Speed<double> right = Speed<double>.FromMetersPerSecond(2.0); // 2e30 qm/s

        // When
        Speed<double> result = left.Divide(right); // 1e31 / 2e30 = 5 qm/s

        // Then
        Assert.Equal(1e31, left.QuectoMetersPerSecond, Tolerance);
        Assert.Equal(2e30, right.QuectoMetersPerSecond, Tolerance);
        Assert.Equal(5.0, result.QuectoMetersPerSecond, Tolerance);
        Assert.Equal(5e-30, result.MetersPerSecond, Tolerance);
    }

    [Fact(DisplayName = "Speed comparison should produce the expected result (left equal to right)")]
    public void SpeedComparisonShouldProduceExpectedResultLeftEqualToRight()
    {
        // Given
        Speed<double> left = Speed<double>.FromMetersPerSecond(123.0);
        Speed<double> right = Speed<double>.FromMetersPerSecond(123.0);

        // When / Then
        Assert.Equal(0, Speed<double>.Compare(left, right));
        Assert.Equal(0, left.CompareTo(right));
        Assert.Equal(0, left.CompareTo((object)right));
        Assert.False(left > right);
        Assert.True(left >= right);
        Assert.False(left < right);
        Assert.True(left <= right);
    }

    [Fact(DisplayName = "Speed comparison should produce the expected result (left greater than right)")]
    public void SpeedComparisonShouldProduceExpectedResultLeftGreaterThanRight()
    {
        // Given
        Speed<double> left = Speed<double>.FromMetersPerSecond(456.0);
        Speed<double> right = Speed<double>.FromMetersPerSecond(123.0);

        // When / Then
        Assert.Equal(1, Speed<double>.Compare(left, right));
        Assert.Equal(1, left.CompareTo(right));
        Assert.Equal(1, left.CompareTo((object)right));
        Assert.True(left > right);
        Assert.True(left >= right);
        Assert.False(left < right);
        Assert.False(left <= right);
    }

    [Fact(DisplayName = "Speed comparison should produce the expected result (left less than right)")]
    public void SpeedComparisonShouldProduceExpectedResultLeftLessThanRight()
    {
        // Given
        Speed<double> left = Speed<double>.FromMetersPerSecond(123.0);
        Speed<double> right = Speed<double>.FromMetersPerSecond(456.0);

        // When / Then
        Assert.Equal(-1, Speed<double>.Compare(left, right));
        Assert.Equal(-1, left.CompareTo(right));
        Assert.Equal(-1, left.CompareTo((object)right));
        Assert.False(left > right);
        Assert.False(left >= right);
        Assert.True(left < right);
        Assert.True(left <= right);
    }

    [Fact(DisplayName = "Speed equality should produce the expected result (left equal to right)")]
    public void SpeedEqualityShouldProduceExpectedResultLeftEqualToRight()
    {
        // Given
        Speed<double> left = Speed<double>.FromMetersPerSecond(123.0);
        Speed<double> right = Speed<double>.FromMetersPerSecond(123.0);

        // When / Then
        Assert.True(Speed<double>.Equals(left, right));
        Assert.True(left.Equals(right));
        Assert.True(left.Equals((object)right));
        Assert.True(left == right);
        Assert.False(left != right);
    }

    [Fact(DisplayName = "Speed equality should produce the expected result (left not equal to right)")]
    public void SpeedEqualityShouldProduceExpectedResultLeftNotEqualToRight()
    {
        // Given
        Speed<double> left = Speed<double>.FromMetersPerSecond(2.0);
        Speed<double> right = Speed<double>.FromKilometersPerHour(7.2);

        // When / Then
        Assert.False(Speed<double>.Equals(left, right));
        Assert.False(left.Equals(right));
        Assert.False(left.Equals((object)right));
        Assert.False(left == right);
        Assert.True(left != right);
    }

    [Fact(DisplayName = "Speed equality should produce the expected result (different values)")]
    public void SpeedEqualityShouldProduceExpectedResultDifferentValues()
    {
        // Given
        Speed<double> left = Speed<double>.FromMetersPerSecond(2.0);
        Speed<double> right = Speed<double>.FromMetersPerSecond(3.0);

        // When / Then
        Assert.False(Speed<double>.Equals(left, right));
        Assert.False(left.Equals(right));
        Assert.False(left.Equals((object)right));
        Assert.False(left == right);
        Assert.True(left != right);
    }

    [Fact(DisplayName = "Speed.ToString should produce the expected result")]
    public void SpeedToStringShouldProduceExpectedResult()
    {
        // Given
        Speed<double> speed = Speed<double>.FromMetersPerSecond(1.0);

        // When / Then
        Assert.Equal("1.000 mps", $"{speed:mps3}");
        Assert.Equal("3.600 kmph", $"{speed:kmph3}");
        Assert.Equal("2.237 mph", $"{speed:mph3}");
        Assert.Equal("1.944 kt", $"{speed:kt3}");
        Assert.Equal("39.370 ips", $"{speed:ips3}");
        Assert.Equal("3.281 fps", $"{speed:fps3}");
    }

    [Fact(DisplayName = "Speed.ToString should honor custom culture separators")]
    public void SpeedToStringShouldHonorCustomCulture()
    {
        // Given
        CultureInfo customCulture = new("de-DE");
        Speed<double> speed = Speed<double>.FromMetersPerSecond(1234.56);

        // When
        string formatted = speed.ToString("mps2", customCulture);

        // Then
        // German uses '.' for thousands and ',' for decimals.
        Assert.Equal("1.234,56 mps", formatted);
    }
}
