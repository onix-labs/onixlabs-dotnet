// Copyright 2020 ONIXLabs
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
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Globalization;

namespace OnixLabs.Numerics.UnitTests;

/// <summary>
/// Verifies formatting of <see cref="Float128"/> and <see cref="Float256"/> across their ranges, including values straddling powers of
/// ten, where the decimal exponent estimate used while formatting is most likely to be off, and values at or above 10^36, which the
/// general and round-trip formats used to reject (#169).
/// </summary>
public sealed class FloatRoundTripPropertyTests
{
    private const int RandomCaseCount = 3000;

    [Fact(DisplayName = "Float128 round-trip format should parse back to the identical bits for random and power-of-ten-boundary values")]
    public void Float128RoundTripFormatShouldParseBackToTheIdenticalBits()
    {
        // Given
        Random random = new(Seed: 128);
        List<Float128> values = [];
        byte[] bytes = new byte[16];

        for (int index = 0; index < RandomCaseCount; index++)
        {
            random.NextBytes(bytes);
            Float128 value = new(BinaryPrimitives.ReadUInt128LittleEndian(bytes));

            if (Float128.IsFinite(value)) values.Add(value);
        }

        for (int exponent = -4900; exponent <= 4900; exponent += 37)
        {
            Float128 power = Float128.Parse($"1e{exponent}", CultureInfo.InvariantCulture);
            values.AddRange([power, Float128.BitIncrement(power), Float128.BitDecrement(power), -power]);
        }

        List<string> failures = [];

        // When
        foreach (Float128 value in values)
        {
            string text = value.ToString("R", CultureInfo.InvariantCulture);
            Float128 parsed = Float128.Parse(text, CultureInfo.InvariantCulture);

            if (parsed.Bits != value.Bits && !(Float128.IsZero(value) && Float128.IsZero(parsed)))
                failures.Add($"{value.Bits:X32} formatted as {text} parsed as {parsed.Bits:X32}");
        }

        // Then
        Assert.Empty(failures);
    }

    [Theory(DisplayName = "Float128 general, default and round-trip formats should format values at or above 10^36 (#169)")]
    [InlineData("1e36")]
    [InlineData("1.5e37")]
    [InlineData("1e100")]
    [InlineData("-1e4000")]
    public void Float128GeneralFormatsShouldFormatLargeValues(string text)
    {
        // Given
        Float128 value = Float128.Parse(text, CultureInfo.InvariantCulture);

        // When
        string general = value.ToString("G", CultureInfo.InvariantCulture);
        string roundTrip = value.ToString("R", CultureInfo.InvariantCulture);
        string defaultFormat = value.ToString();

        // Then
        Assert.Equal(value.Bits, Float128.Parse(general, CultureInfo.InvariantCulture).Bits);
        Assert.Equal(value.Bits, Float128.Parse(roundTrip, CultureInfo.InvariantCulture).Bits);
        Assert.False(string.IsNullOrEmpty(defaultFormat));
    }

    [Fact(DisplayName = "Float256 general and round-trip formats should format values at or above 10^36 and parse back within a few ULPs (#169)")]
    public void Float256GeneralFormatsShouldFormatLargeValuesAndParseBackWithinAFewUlps()
    {
        // Given
        Random random = new(Seed: 256);
        Float256 tolerance = Float256.Parse("1e-68", CultureInfo.InvariantCulture);
        List<string> failures = [];

        // When
        for (int exponent = 36; exponent <= 1000; exponent += 7)
        {
            Float256 power = Float256.Parse($"1e{exponent}", CultureInfo.InvariantCulture);
            Float256 scaled = power * (Float256)(random.NextDouble() + 0.5);

            foreach (Float256 value in new[] { power, Float256.BitIncrement(power), scaled, -scaled })
            foreach (string format in new[] { "G", "R" })
            {
                string text = value.ToString(format, CultureInfo.InvariantCulture);
                Float256 parsed = Float256.Parse(text, CultureInfo.InvariantCulture);

                if (Float256.Abs(parsed - value) > Float256.Abs(value) * tolerance)
                    failures.Add($"1e{exponent} ({format}): {text} parsed too far from the value");
            }
        }

        // Then
        Assert.Empty(failures);
    }
}
