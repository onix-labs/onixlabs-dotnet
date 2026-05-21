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

public sealed class FrequencyTests
{
    [Fact(DisplayName = "Frequency.Zero should produce the expected result")]
    public void FrequencyZeroShouldProduceExpectedResult()
    {
        // Given / When
        Frequency<Float128> frequency = Frequency<Float128>.Zero;

        // Then
        Assert.Equal(Float128.Zero, frequency.QuectoHertz);
    }

    [Theory(DisplayName = "Frequency.FromQuectohertz should produce the expected QuectoHertz")]
    [InlineData("0", "0")]
    [InlineData("1", "1")]
    [InlineData("2.5", "2.5")]
    public void FrequencyFromQuectohertzShouldProduceExpectedQuectoHertz(string value, string expected)
    {
        Frequency<Float128> f = Frequency<Float128>.FromQuectohertz(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), f.QuectoHertz);
    }

    [Theory(DisplayName = "Frequency.FromRontohertz should produce the expected QuectoHertz")]
    [InlineData("0", "0")]
    [InlineData("1", "1e3")]
    [InlineData("2.5", "2.5e3")]
    public void FrequencyFromRontohertzShouldProduceExpectedQuectoHertz(string value, string expected)
    {
        Frequency<Float128> f = Frequency<Float128>.FromRontohertz(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), f.QuectoHertz);
    }

    [Theory(DisplayName = "Frequency.FromYoctohertz should produce the expected QuectoHertz")]
    [InlineData("0", "0")]
    [InlineData("1", "1e6")]
    [InlineData("2.5", "2.5e6")]
    public void FrequencyFromYoctohertzShouldProduceExpectedQuectoHertz(string value, string expected)
    {
        Frequency<Float128> f = Frequency<Float128>.FromYoctohertz(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), f.QuectoHertz);
    }

    [Theory(DisplayName = "Frequency.FromZeptohertz should produce the expected QuectoHertz")]
    [InlineData("0", "0")]
    [InlineData("1", "1e9")]
    [InlineData("2.5", "2.5e9")]
    public void FrequencyFromZeptohertzShouldProduceExpectedQuectoHertz(string value, string expected)
    {
        Frequency<Float128> f = Frequency<Float128>.FromZeptohertz(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), f.QuectoHertz);
    }

    [Theory(DisplayName = "Frequency.FromAttohertz should produce the expected QuectoHertz")]
    [InlineData("0", "0")]
    [InlineData("1", "1e12")]
    [InlineData("2.5", "2.5e12")]
    public void FrequencyFromAttohertzShouldProduceExpectedQuectoHertz(string value, string expected)
    {
        Frequency<Float128> f = Frequency<Float128>.FromAttohertz(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), f.QuectoHertz);
    }

    [Theory(DisplayName = "Frequency.FromFemtohertz should produce the expected QuectoHertz")]
    [InlineData("0", "0")]
    [InlineData("1", "1e15")]
    [InlineData("2.5", "2.5e15")]
    public void FrequencyFromFemtohertzShouldProduceExpectedQuectoHertz(string value, string expected)
    {
        Frequency<Float128> f = Frequency<Float128>.FromFemtohertz(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), f.QuectoHertz);
    }

    [Theory(DisplayName = "Frequency.FromPicohertz should produce the expected QuectoHertz")]
    [InlineData("0", "0")]
    [InlineData("1", "1e18")]
    [InlineData("2.5", "2.5e18")]
    public void FrequencyFromPicohertzShouldProduceExpectedQuectoHertz(string value, string expected)
    {
        Frequency<Float128> f = Frequency<Float128>.FromPicohertz(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), f.QuectoHertz);
    }

    [Theory(DisplayName = "Frequency.FromNanohertz should produce the expected QuectoHertz")]
    [InlineData("0", "0")]
    [InlineData("1", "1e21")]
    [InlineData("2.5", "2.5e21")]
    public void FrequencyFromNanohertzShouldProduceExpectedQuectoHertz(string value, string expected)
    {
        Frequency<Float128> f = Frequency<Float128>.FromNanohertz(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), f.QuectoHertz);
    }

    [Theory(DisplayName = "Frequency.FromMicrohertz should produce the expected QuectoHertz")]
    [InlineData("0", "0")]
    [InlineData("1", "1e24")]
    [InlineData("2.5", "2.5e24")]
    public void FrequencyFromMicrohertzShouldProduceExpectedQuectoHertz(string value, string expected)
    {
        Frequency<Float128> f = Frequency<Float128>.FromMicrohertz(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), f.QuectoHertz);
    }

    [Theory(DisplayName = "Frequency.FromMillihertz should produce the expected QuectoHertz")]
    [InlineData("0", "0")]
    [InlineData("1", "1e27")]
    [InlineData("2.5", "2.5e27")]
    public void FrequencyFromMillihertzShouldProduceExpectedQuectoHertz(string value, string expected)
    {
        Frequency<Float128> f = Frequency<Float128>.FromMillihertz(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), f.QuectoHertz);
    }

    [Theory(DisplayName = "Frequency.FromCentihertz should produce the expected QuectoHertz")]
    [InlineData("0", "0")]
    [InlineData("1", "1e28")]
    [InlineData("2.5", "2.5e28")]
    public void FrequencyFromCentihertzShouldProduceExpectedQuectoHertz(string value, string expected)
    {
        Frequency<Float128> f = Frequency<Float128>.FromCentihertz(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), f.QuectoHertz);
    }

    [Theory(DisplayName = "Frequency.FromDecihertz should produce the expected QuectoHertz")]
    [InlineData("0", "0")]
    [InlineData("1", "1e29")]
    [InlineData("2.5", "2.5e29")]
    public void FrequencyFromDecihertzShouldProduceExpectedQuectoHertz(string value, string expected)
    {
        Frequency<Float128> f = Frequency<Float128>.FromDecihertz(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), f.QuectoHertz);
    }

    [Theory(DisplayName = "Frequency.FromHertz should produce the expected QuectoHertz")]
    [InlineData("0", "0")]
    [InlineData("1", "1e30")]
    [InlineData("2.5", "2.5e30")]
    public void FrequencyFromHertzShouldProduceExpectedQuectoHertz(string value, string expected)
    {
        Frequency<Float128> f = Frequency<Float128>.FromHertz(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), f.QuectoHertz);
    }

    [Theory(DisplayName = "Frequency.FromDecahertz should produce the expected QuectoHertz")]
    [InlineData("0", "0")]
    [InlineData("1", "1e31")]
    [InlineData("2.5", "2.5e31")]
    public void FrequencyFromDecahertzShouldProduceExpectedQuectoHertz(string value, string expected)
    {
        Frequency<Float128> f = Frequency<Float128>.FromDecahertz(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), f.QuectoHertz);
    }

    [Theory(DisplayName = "Frequency.FromHectohertz should produce the expected QuectoHertz")]
    [InlineData("0", "0")]
    [InlineData("1", "1e32")]
    [InlineData("2.5", "2.5e32")]
    public void FrequencyFromHectohertzShouldProduceExpectedQuectoHertz(string value, string expected)
    {
        Frequency<Float128> f = Frequency<Float128>.FromHectohertz(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), f.QuectoHertz);
    }

    [Theory(DisplayName = "Frequency.FromKilohertz should produce the expected QuectoHertz")]
    [InlineData("0", "0")]
    [InlineData("1", "1e33")]
    [InlineData("2.5", "2.5e33")]
    public void FrequencyFromKilohertzShouldProduceExpectedQuectoHertz(string value, string expected)
    {
        Frequency<Float128> f = Frequency<Float128>.FromKilohertz(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), f.QuectoHertz);
    }

    [Theory(DisplayName = "Frequency.FromMegahertz should produce the expected QuectoHertz")]
    [InlineData("0", "0")]
    [InlineData("1", "1e36")]
    [InlineData("2.5", "2.5e36")]
    public void FrequencyFromMegahertzShouldProduceExpectedQuectoHertz(string value, string expected)
    {
        Frequency<Float128> f = Frequency<Float128>.FromMegahertz(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), f.QuectoHertz);
    }

    [Theory(DisplayName = "Frequency.FromGigahertz should produce the expected QuectoHertz")]
    [InlineData("0", "0")]
    [InlineData("1", "1e39")]
    [InlineData("2.5", "2.5e39")]
    public void FrequencyFromGigahertzShouldProduceExpectedQuectoHertz(string value, string expected)
    {
        Frequency<Float128> f = Frequency<Float128>.FromGigahertz(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), f.QuectoHertz);
    }

    [Theory(DisplayName = "Frequency.FromTerahertz should produce the expected QuectoHertz")]
    [InlineData("0", "0")]
    [InlineData("1", "1e42")]
    [InlineData("2.5", "2.5e42")]
    public void FrequencyFromTerahertzShouldProduceExpectedQuectoHertz(string value, string expected)
    {
        Frequency<Float128> f = Frequency<Float128>.FromTerahertz(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), f.QuectoHertz);
    }

    [Theory(DisplayName = "Frequency.FromPetahertz should produce the expected QuectoHertz")]
    [InlineData("0", "0")]
    [InlineData("1", "1e45")]
    [InlineData("2.5", "2.5e45")]
    public void FrequencyFromPetahertzShouldProduceExpectedQuectoHertz(string value, string expected)
    {
        Frequency<Float128> f = Frequency<Float128>.FromPetahertz(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), f.QuectoHertz);
    }

    [Theory(DisplayName = "Frequency.FromExahertz should produce the expected QuectoHertz")]
    [InlineData("0", "0")]
    [InlineData("1", "1e48")]
    [InlineData("2.5", "2.5e48")]
    public void FrequencyFromExahertzShouldProduceExpectedQuectoHertz(string value, string expected)
    {
        Frequency<Float128> f = Frequency<Float128>.FromExahertz(Float128.Parse(value));
        Assert.Equal(Float128.Parse(expected), f.QuectoHertz);
    }

    [Theory(DisplayName = "Frequency.FromZettahertz should produce the expected QuectoHertz")]
    [InlineData("0")]
    [InlineData("1")]
    [InlineData("2.5")]
    public void FrequencyFromZettahertzShouldProduceExpectedQuectoHertz(string value)
    {
        // Above Float128's 10^48 exact-power-of-10 range, Parse("X.Ye51") and value × Pow10(51) can
        // diverge in the LSB. Compute expected via the same chain as the unit to keep strict equality.
        Float128 input = Float128.Parse(value);
        Float128 expected = input * UnitMath.Pow10<Float128>(51);

        Frequency<Float128> f = Frequency<Float128>.FromZettahertz(input);

        Assert.Equal(expected, f.QuectoHertz);
    }

    [Theory(DisplayName = "Frequency.FromYottahertz should produce the expected QuectoHertz")]
    [InlineData("0")]
    [InlineData("1")]
    [InlineData("2.5")]
    public void FrequencyFromYottahertzShouldProduceExpectedQuectoHertz(string value)
    {
        Float128 input = Float128.Parse(value);
        Float128 expected = input * UnitMath.Pow10<Float128>(54);

        Frequency<Float128> f = Frequency<Float128>.FromYottahertz(input);

        Assert.Equal(expected, f.QuectoHertz);
    }

    [Theory(DisplayName = "Frequency.FromRonnahertz should produce the expected QuectoHertz")]
    [InlineData("0")]
    [InlineData("1")]
    [InlineData("2.5")]
    public void FrequencyFromRonnahertzShouldProduceExpectedQuectoHertz(string value)
    {
        Float128 input = Float128.Parse(value);
        Float128 expected = input * UnitMath.Pow10<Float128>(57);

        Frequency<Float128> f = Frequency<Float128>.FromRonnahertz(input);

        Assert.Equal(expected, f.QuectoHertz);
    }

    [Theory(DisplayName = "Frequency.FromQuettahertz should produce the expected QuectoHertz")]
    [InlineData("0")]
    [InlineData("1")]
    [InlineData("2.5")]
    public void FrequencyFromQuettahertzShouldProduceExpectedQuectoHertz(string value)
    {
        Float128 input = Float128.Parse(value);
        Float128 expected = input * UnitMath.Pow10<Float128>(60);

        Frequency<Float128> f = Frequency<Float128>.FromQuettahertz(input);

        Assert.Equal(expected, f.QuectoHertz);
    }

    [Theory(DisplayName = "Frequency.FromRevolutionsPerMinute should produce the expected QuectoHertz")]
    [InlineData("0", "0")]
    [InlineData("60", "1e30")]   // 60 rpm = 1 Hz
    [InlineData("120", "2e30")]  // 120 rpm = 2 Hz
    [InlineData("3000", "5e31")] // 3000 rpm = 50 Hz
    public void FrequencyFromRevolutionsPerMinuteShouldProduceExpectedQuectoHertz(string value, string expected)
    {
        // Match the unit's chain exactly: value × (Pow10(30) / 60). The /60 introduces 1 ULP at T;
        // computing the expected via the same chain ensures strict equality remains meaningful.
        Float128 input = Float128.Parse(value);
        Float128 expectedCanonical = input * (UnitMath.Pow10<Float128>(30) / (Float128)60);
        Float128 expectedLiteral = Float128.Parse(expected);

        Frequency<Float128> f = Frequency<Float128>.FromRevolutionsPerMinute(input);

        // Both invariants hold: literal Hz form (clean integer multiples) and chain-derived form.
        Assert.Equal(expectedCanonical, f.QuectoHertz);
        Assert.Equal(expectedLiteral, f.QuectoHertz);
    }

    [Theory(DisplayName = "Frequency.FromBeatsPerMinute should produce the expected QuectoHertz")]
    [InlineData("0", "0")]
    [InlineData("60", "1e30")]      // 60 bpm = 1 Hz
    [InlineData("120", "2e30")]     // 120 bpm = 2 Hz
    [InlineData("72", "1.2e30")]    // resting heart rate
    public void FrequencyFromBeatsPerMinuteShouldProduceExpectedQuectoHertz(string value, string expected)
    {
        Float128 input = Float128.Parse(value);
        Float128 expectedCanonical = input * (UnitMath.Pow10<Float128>(30) / (Float128)60);
        Float128 expectedLiteral = Float128.Parse(expected);

        Frequency<Float128> f = Frequency<Float128>.FromBeatsPerMinute(input);

        Assert.Equal(expectedCanonical, f.QuectoHertz);
        Assert.Equal(expectedLiteral, f.QuectoHertz);
    }

    [Theory(DisplayName = "Frequency.FromRadiansPerSecond should produce the expected QuectoHertz")]
    [InlineData("0")]
    [InlineData("1")]
    [InlineData("2.5")]
    public void FrequencyFromRadiansPerSecondShouldProduceExpectedQuectoHertz(string value)
    {
        // 1 rad/s = 1/(2π) Hz, so qHz = value × Pow10(30) / (2π). T.Pi keeps Float128's 40+ digit
        // precision; compute expected via the same chain to keep strict equality meaningful.
        Float128 input = Float128.Parse(value);
        Float128 expected = input * (UnitMath.Pow10<Float128>(30) / ((Float128)2 * Float128.Pi));

        Frequency<Float128> f = Frequency<Float128>.FromRadiansPerSecond(input);

        Assert.Equal(expected, f.QuectoHertz);
    }

    // Round-trips through divide-by-N (RPM/BPM) and π (rad/s) accumulate ~1 ULP residual at T's
    // precision because the cached factor's rounding doesn't divide out cleanly. A Float128 ULP at
    // magnitude 60 is ~1.2e-32; at 2π it's ~7e-34. This tolerance accepts a few ULPs at either,
    // still ~22 orders of magnitude tighter than the old `double` test's 1e-9.
    private const string RoundtripTolerance = "1e-30";

    [Fact(DisplayName = "Frequency.RevolutionsPerMinute conversion roundtrips through Hertz")]
    public void FrequencyRevolutionsPerMinuteRoundtripsThroughHertz()
    {
        Float128 tolerance = Float128.Parse(RoundtripTolerance);

        // Given
        Frequency<Float128> f = Frequency<Float128>.FromHertz((Float128)1);

        // Then
        AssertNearlyEqual((Float128)60, f.RevolutionsPerMinute, tolerance);
        AssertNearlyEqual((Float128)60, f.BeatsPerMinute, tolerance);
    }

    [Fact(DisplayName = "Frequency.RadiansPerSecond conversion roundtrips through Hertz")]
    public void FrequencyRadiansPerSecondRoundtripsThroughHertz()
    {
        Float128 tolerance = Float128.Parse(RoundtripTolerance);

        // Given
        Frequency<Float128> f = Frequency<Float128>.FromHertz((Float128)1);

        // Then — 1 Hz = 2π rad/s
        AssertNearlyEqual((Float128)2 * Float128.Pi, f.RadiansPerSecond, tolerance);
    }

    [Fact(DisplayName = "Frequency.Add should produce the expected result")]
    public void FrequencyAddShouldProduceExpectedValue()
    {
        // Given
        Frequency<Float128> left = Frequency<Float128>.FromKilohertz(Float128.Parse("1.5"));
        Frequency<Float128> right = Frequency<Float128>.FromKilohertz(Float128.Parse("0.5"));

        // When
        Frequency<Float128> result = left.Add(right);

        // Then
        Assert.Equal((Float128)2, result.KiloHertz);
    }

    [Fact(DisplayName = "Frequency.Subtract should produce the expected result")]
    public void FrequencySubtractShouldProduceExpectedValue()
    {
        // Given
        Frequency<Float128> left = Frequency<Float128>.FromKilohertz(Float128.Parse("1.5"));
        Frequency<Float128> right = Frequency<Float128>.FromKilohertz(Float128.Parse("0.4"));

        // When
        Frequency<Float128> result = left.Subtract(right);

        // Then
        Assert.Equal(Float128.Parse("1.1"), result.KiloHertz);
    }

    [Fact(DisplayName = "Frequency comparison should produce the expected result (left equal to right)")]
    public void FrequencyComparisonShouldProduceExpectedResultLeftEqualToRight()
    {
        // Given
        Frequency<Float128> left = Frequency<Float128>.FromKilohertz((Float128)123);
        Frequency<Float128> right = Frequency<Float128>.FromKilohertz((Float128)123);

        // When / Then
        Assert.Equal(0, Frequency<Float128>.Compare(left, right));
        Assert.Equal(0, left.CompareTo(right));
        Assert.Equal(0, left.CompareTo((object)right));
        Assert.False(left > right);
        Assert.True(left >= right);
        Assert.False(left < right);
        Assert.True(left <= right);
    }

    [Fact(DisplayName = "Frequency comparison should produce the expected result (left greater than right)")]
    public void FrequencyComparisonShouldProduceExpectedLeftGreaterThanRight()
    {
        // Given
        Frequency<Float128> left = Frequency<Float128>.FromKilohertz((Float128)456);
        Frequency<Float128> right = Frequency<Float128>.FromKilohertz((Float128)123);

        // When / Then
        Assert.Equal(1, Frequency<Float128>.Compare(left, right));
        Assert.Equal(1, left.CompareTo(right));
        Assert.Equal(1, left.CompareTo((object)right));
        Assert.True(left > right);
        Assert.True(left >= right);
        Assert.False(left < right);
        Assert.False(left <= right);
    }

    [Fact(DisplayName = "Frequency comparison should produce the expected result (left less than right)")]
    public void FrequencyComparisonShouldProduceExpectedLeftLessThanRight()
    {
        // Given
        Frequency<Float128> left = Frequency<Float128>.FromKilohertz((Float128)123);
        Frequency<Float128> right = Frequency<Float128>.FromKilohertz((Float128)456);

        // When / Then
        Assert.Equal(-1, Frequency<Float128>.Compare(left, right));
        Assert.Equal(-1, left.CompareTo(right));
        Assert.Equal(-1, left.CompareTo((object)right));
        Assert.False(left > right);
        Assert.False(left >= right);
        Assert.True(left < right);
        Assert.True(left <= right);
    }

    [Fact(DisplayName = "Frequency equality should produce the expected result (left equal to right)")]
    public void FrequencyEqualityShouldProduceExpectedResultLeftEqualToRight()
    {
        // Given: 2 kHz and 2000 Hz reduce to the same canonical at Float128.
        Frequency<Float128> left = Frequency<Float128>.FromKilohertz((Float128)2);
        Frequency<Float128> right = Frequency<Float128>.FromHertz((Float128)2000);

        // When / Then
        Assert.True(Frequency<Float128>.Equals(left, right));
        Assert.True(left.Equals(right));
        Assert.True(left.Equals((object)right));
        Assert.True(left == right);
        Assert.False(left != right);
    }

    [Fact(DisplayName = "Frequency equality should produce the expected result (left not equal to right)")]
    public void FrequencyEqualityShouldProduceExpectedResultLeftNotEqualToRight()
    {
        // Given
        Frequency<Float128> left = Frequency<Float128>.FromKilohertz((Float128)2);
        Frequency<Float128> right = Frequency<Float128>.FromHertz((Float128)2500);

        // When / Then
        Assert.False(Frequency<Float128>.Equals(left, right));
        Assert.False(left.Equals(right));
        Assert.False(left.Equals((object)right));
        Assert.False(left == right);
        Assert.True(left != right);
    }

    [Fact(DisplayName = "Frequency.ToString should produce the expected result")]
    public void FrequencyToStringShouldProduceExpectedResult()
    {
        // Given
        Frequency<Float128> f = Frequency<Float128>.FromHertz((Float128)1000);

        // When / Then
        Assert.Equal("1,000.000 Hz", $"{f:Hz3}");
        Assert.Equal("1.000 kHz", $"{f:kHz3}");
        Assert.Equal("0.001 MHz", $"{f:MHz3}");
        Assert.Equal("1,000,000.000 mHz", $"{f:mHz3}");
        Assert.Equal("60,000.000 rpm", $"{f:rpm3}");
        Assert.Equal("60,000.000 bpm", $"{f:bpm3}");
    }

    [Fact(DisplayName = "Frequency.ToString MHz vs mHz are case-sensitive")]
    public void FrequencyToStringMhzVsMhzAreCaseSensitive()
    {
        // Given
        Frequency<Float128> f = Frequency<Float128>.FromHertz((Float128)1);

        // Then
        Assert.Equal("0.000001 MHz", $"{f:MHz6}"); // mega
        Assert.Equal("1,000.000 mHz", $"{f:mHz3}"); // milli
    }

    [Fact(DisplayName = "Frequency.ToString rad/s should use proper unit symbol")]
    public void FrequencyToStringRadiansPerSecondShouldUseProperUnitSymbol()
    {
        // Given
        Frequency<Float128> f = Frequency<Float128>.FromHertz((Float128)1);

        // Then
        Assert.Equal("6.283185 rad/s", $"{f:radps6}");
    }

    [Fact(DisplayName = "Frequency.ToString should honor custom culture separators")]
    public void FrequencyToStringShouldHonorCustomCulture()
    {
        // Given
        CultureInfo customCulture = new("de-DE");
        Frequency<Float128> f = Frequency<Float128>.FromKilohertz(Float128.Parse("1234.56"));

        // When
        string formatted = f.ToString("kHz2", customCulture);

        // Then
        Assert.Equal("1.234,56 kHz", formatted);
    }

    private static void AssertNearlyEqual(Float128 expected, Float128 actual, Float128 tolerance)
    {
        Float128 diff = expected > actual ? expected - actual : actual - expected;
        Assert.True(diff <= tolerance, $"Expected {expected}, got {actual} (diff {diff} exceeds tolerance {tolerance})");
    }

    [Theory(DisplayName = "Frequency.ValueOf should return the value at the matching scale")]
    [InlineData("qHz")]
    [InlineData("rHz")]
    [InlineData("yHz")]
    [InlineData("zHz")]
    [InlineData("aHz")]
    [InlineData("fHz")]
    [InlineData("pHz")]
    [InlineData("nHz")]
    [InlineData("uHz")]
    [InlineData("mHz")]
    [InlineData("cHz")]
    [InlineData("dHz")]
    [InlineData("Hz")]
    [InlineData("daHz")]
    [InlineData("hHz")]
    [InlineData("kHz")]
    [InlineData("MHz")]
    [InlineData("GHz")]
    [InlineData("THz")]
    [InlineData("PHz")]
    [InlineData("EHz")]
    [InlineData("ZHz")]
    [InlineData("YHz")]
    [InlineData("RHz")]
    [InlineData("QHz")]
    [InlineData("rpm")]
    [InlineData("bpm")]
    [InlineData("radps")]
    public void FrequencyValueOfShouldReturnValueAtMatchingScale(string specifier)
    {
        // Given
        Frequency<Float128> f = Frequency<Float128>.FromHertz(Float128.Parse("1234.567"));

        // When
        Float128 expected = specifier switch
        {
            "qHz" => f.QuectoHertz,
            "rHz" => f.RontoHertz,
            "yHz" => f.YoctoHertz,
            "zHz" => f.ZeptoHertz,
            "aHz" => f.AttoHertz,
            "fHz" => f.FemtoHertz,
            "pHz" => f.PicoHertz,
            "nHz" => f.NanoHertz,
            "uHz" => f.MicroHertz,
            "mHz" => f.MilliHertz,
            "cHz" => f.CentiHertz,
            "dHz" => f.DeciHertz,
            "Hz" => f.Hertz,
            "daHz" => f.DecaHertz,
            "hHz" => f.HectoHertz,
            "kHz" => f.KiloHertz,
            "MHz" => f.MegaHertz,
            "GHz" => f.GigaHertz,
            "THz" => f.TeraHertz,
            "PHz" => f.PetaHertz,
            "EHz" => f.ExaHertz,
            "ZHz" => f.ZettaHertz,
            "YHz" => f.YottaHertz,
            "RHz" => f.RonnaHertz,
            "QHz" => f.QuettaHertz,
            "rpm" => f.RevolutionsPerMinute,
            "bpm" => f.BeatsPerMinute,
            "radps" => f.RadiansPerSecond,
            _ => throw new InvalidOperationException($"Unhandled specifier: {specifier}")
        };

        // Then
        Assert.Equal(expected, f.ValueOf(specifier));
    }

    [Fact(DisplayName = "Frequency.ValueOf should throw on invalid specifier")]
    public void FrequencyValueOfShouldThrowOnInvalidSpecifier()
    {
        // Given
        Frequency<Float128> f = Frequency<Float128>.FromHertz(Float128.Parse("1"));

        // Then
        Assert.Throws<ArgumentException>(() => f.ValueOf("xx"));
    }
}
