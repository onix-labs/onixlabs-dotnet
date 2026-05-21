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

public sealed class VolumeTests
{
    [Fact(DisplayName = "Volume.Zero should produce the expected result")]
    public void VolumeZeroShouldProduceExpectedResult()
    {
        // Given / When
        Volume<Float256> volume = Volume<Float256>.Zero;

        // Then
        Assert.Equal(Float256.Zero, volume.CubicQuectoMeters);
    }

    [Theory(DisplayName = "Volume.FromCubicQuectometers should produce the expected CubicQuectoMeters")]
    [InlineData("0", "0")]
    [InlineData("1", "1")]
    [InlineData("2.5", "2.5")]
    public void VolumeFromCubicQuectometersShouldProduceExpectedCubicQuectoMeters(string value, string expected)
    {
        Volume<Float256> v = Volume<Float256>.FromCubicQuectometers(Float256.Parse(value));
        Assert.Equal(Float256.Parse(expected), v.CubicQuectoMeters);
    }

    [Theory(DisplayName = "Volume.FromCubicRontometers should produce the expected CubicQuectoMeters")]
    [InlineData("0", "0")]
    [InlineData("1", "1e9")]
    [InlineData("2.5", "2.5e9")]
    public void VolumeFromCubicRontometersShouldProduceExpectedCubicQuectoMeters(string value, string expected)
    {
        Volume<Float256> v = Volume<Float256>.FromCubicRontometers(Float256.Parse(value));
        Assert.Equal(Float256.Parse(expected), v.CubicQuectoMeters);
    }

    [Theory(DisplayName = "Volume.FromCubicYoctometers should produce the expected CubicQuectoMeters")]
    [InlineData("0", "0")]
    [InlineData("1", "1e18")]
    [InlineData("2.5", "2.5e18")]
    public void VolumeFromCubicYoctometersShouldProduceExpectedCubicQuectoMeters(string value, string expected)
    {
        Volume<Float256> v = Volume<Float256>.FromCubicYoctometers(Float256.Parse(value));
        Assert.Equal(Float256.Parse(expected), v.CubicQuectoMeters);
    }

    [Theory(DisplayName = "Volume.FromCubicZeptometers should produce the expected CubicQuectoMeters")]
    [InlineData("0", "0")]
    [InlineData("1", "1e27")]
    [InlineData("2.5", "2.5e27")]
    public void VolumeFromCubicZeptometersShouldProduceExpectedCubicQuectoMeters(string value, string expected)
    {
        Volume<Float256> v = Volume<Float256>.FromCubicZeptometers(Float256.Parse(value));
        Assert.Equal(Float256.Parse(expected), v.CubicQuectoMeters);
    }

    [Theory(DisplayName = "Volume.FromCubicAttometers should produce the expected CubicQuectoMeters")]
    [InlineData("0", "0")]
    [InlineData("1", "1e36")]
    [InlineData("2.5", "2.5e36")]
    public void VolumeFromCubicAttometersShouldProduceExpectedCubicQuectoMeters(string value, string expected)
    {
        Volume<Float256> v = Volume<Float256>.FromCubicAttometers(Float256.Parse(value));
        Assert.Equal(Float256.Parse(expected), v.CubicQuectoMeters);
    }

    [Theory(DisplayName = "Volume.FromCubicFemtometers should produce the expected CubicQuectoMeters")]
    [InlineData("0", "0")]
    [InlineData("1", "1e45")]
    [InlineData("2.5", "2.5e45")]
    public void VolumeFromCubicFemtometersShouldProduceExpectedCubicQuectoMeters(string value, string expected)
    {
        Volume<Float256> v = Volume<Float256>.FromCubicFemtometers(Float256.Parse(value));
        Assert.Equal(Float256.Parse(expected), v.CubicQuectoMeters);
    }

    [Theory(DisplayName = "Volume.FromCubicPicometers should produce the expected CubicQuectoMeters")]
    [InlineData("0", "0")]
    [InlineData("1", "1e54")]
    [InlineData("2.5", "2.5e54")]
    public void VolumeFromCubicPicometersShouldProduceExpectedCubicQuectoMeters(string value, string expected)
    {
        Volume<Float256> v = Volume<Float256>.FromCubicPicometers(Float256.Parse(value));
        Assert.Equal(Float256.Parse(expected), v.CubicQuectoMeters);
    }

    [Theory(DisplayName = "Volume.FromCubicNanometers should produce the expected CubicQuectoMeters")]
    [InlineData("0", "0")]
    [InlineData("1", "1e63")]
    [InlineData("2.5", "2.5e63")]
    public void VolumeFromCubicNanometersShouldProduceExpectedCubicQuectoMeters(string value, string expected)
    {
        Volume<Float256> v = Volume<Float256>.FromCubicNanometers(Float256.Parse(value));
        Assert.Equal(Float256.Parse(expected), v.CubicQuectoMeters);
    }

    [Theory(DisplayName = "Volume.FromCubicMicrometers should produce the expected CubicQuectoMeters")]
    [InlineData("0", "0")]
    [InlineData("1", "1e72")]
    [InlineData("2.5", "2.5e72")]
    public void VolumeFromCubicMicrometersShouldProduceExpectedCubicQuectoMeters(string value, string expected)
    {
        Volume<Float256> v = Volume<Float256>.FromCubicMicrometers(Float256.Parse(value));
        Assert.Equal(Float256.Parse(expected), v.CubicQuectoMeters);
    }

    [Theory(DisplayName = "Volume.FromCubicMillimeters should produce the expected CubicQuectoMeters")]
    [InlineData("0", "0")]
    [InlineData("1", "1e81")]
    [InlineData("2.5", "2.5e81")]
    public void VolumeFromCubicMillimetersShouldProduceExpectedCubicQuectoMeters(string value, string expected)
    {
        Volume<Float256> v = Volume<Float256>.FromCubicMillimeters(Float256.Parse(value));
        Assert.Equal(Float256.Parse(expected), v.CubicQuectoMeters);
    }

    [Theory(DisplayName = "Volume.FromCubicCentimeters should produce the expected CubicQuectoMeters")]
    [InlineData("0", "0")]
    [InlineData("1", "1e84")]
    [InlineData("2.5", "2.5e84")]
    public void VolumeFromCubicCentimetersShouldProduceExpectedCubicQuectoMeters(string value, string expected)
    {
        Volume<Float256> v = Volume<Float256>.FromCubicCentimeters(Float256.Parse(value));
        Assert.Equal(Float256.Parse(expected), v.CubicQuectoMeters);
    }

    [Theory(DisplayName = "Volume.FromCubicDecimeters should produce the expected CubicQuectoMeters")]
    [InlineData("0", "0")]
    [InlineData("1", "1e87")]
    [InlineData("2.5", "2.5e87")]
    public void VolumeFromCubicDecimetersShouldProduceExpectedCubicQuectoMeters(string value, string expected)
    {
        Volume<Float256> v = Volume<Float256>.FromCubicDecimeters(Float256.Parse(value));
        Assert.Equal(Float256.Parse(expected), v.CubicQuectoMeters);
    }

    [Theory(DisplayName = "Volume.FromCubicMeters should produce the expected CubicQuectoMeters")]
    [InlineData("0", "0")]
    [InlineData("1", "1e90")]
    [InlineData("2.5", "2.5e90")]
    public void VolumeFromCubicMetersShouldProduceExpectedCubicQuectoMeters(string value, string expected)
    {
        Volume<Float256> v = Volume<Float256>.FromCubicMeters(Float256.Parse(value));
        Assert.Equal(Float256.Parse(expected), v.CubicQuectoMeters);
    }

    [Theory(DisplayName = "Volume.FromCubicDecameters should produce the expected CubicQuectoMeters")]
    [InlineData("0", "0")]
    [InlineData("1", "1e93")]
    [InlineData("2.5", "2.5e93")]
    public void VolumeFromCubicDecametersShouldProduceExpectedCubicQuectoMeters(string value, string expected)
    {
        Volume<Float256> v = Volume<Float256>.FromCubicDecameters(Float256.Parse(value));
        Assert.Equal(Float256.Parse(expected), v.CubicQuectoMeters);
    }

    [Theory(DisplayName = "Volume.FromCubicHectometers should produce the expected CubicQuectoMeters")]
    [InlineData("0", "0")]
    [InlineData("1", "1e96")]
    [InlineData("2.5", "2.5e96")]
    public void VolumeFromCubicHectometersShouldProduceExpectedCubicQuectoMeters(string value, string expected)
    {
        Volume<Float256> v = Volume<Float256>.FromCubicHectometers(Float256.Parse(value));
        Assert.Equal(Float256.Parse(expected), v.CubicQuectoMeters);
    }

    [Theory(DisplayName = "Volume.FromCubicKilometers should produce the expected CubicQuectoMeters")]
    [InlineData("0", "0")]
    [InlineData("1", "1e99")]
    [InlineData("2.5", "2.5e99")]
    public void VolumeFromCubicKilometersShouldProduceExpectedCubicQuectoMeters(string value, string expected)
    {
        Volume<Float256> v = Volume<Float256>.FromCubicKilometers(Float256.Parse(value));
        Assert.Equal(Float256.Parse(expected), v.CubicQuectoMeters);
    }

    [Theory(DisplayName = "Volume.FromCubicMegameters should produce the expected CubicQuectoMeters")]
    [InlineData("0", "0")]
    [InlineData("1", "1e108")]
    [InlineData("2.5", "2.5e108")]
    public void VolumeFromCubicMegametersShouldProduceExpectedCubicQuectoMeters(string value, string expected)
    {
        Volume<Float256> v = Volume<Float256>.FromCubicMegameters(Float256.Parse(value));
        Assert.Equal(Float256.Parse(expected), v.CubicQuectoMeters);
    }

    [Theory(DisplayName = "Volume.FromCubicGigameters should produce the expected CubicQuectoMeters")]
    [InlineData("0")]
    [InlineData("1")]
    [InlineData("2.5")]
    public void VolumeFromCubicGigametersShouldProduceExpectedCubicQuectoMeters(string value)
    {
        // Above Float256's 10^102 exact-power-of-10 range, Parse("X.Ye117") and value × Pow10(117)
        // can diverge in the LSB because they multiply mantissa fragments in different orders.
        // Compute expected via the same chain as the unit to keep strict equality meaningful.
        Float256 input = Float256.Parse(value);
        Float256 expected = input * UnitMath.Pow10<Float256>(117);

        Volume<Float256> v = Volume<Float256>.FromCubicGigameters(input);

        Assert.Equal(expected, v.CubicQuectoMeters);
    }

    [Theory(DisplayName = "Volume.FromCubicTerameters should produce the expected CubicQuectoMeters")]
    [InlineData("0", "0")]
    [InlineData("1", "1e126")]
    [InlineData("2.5", "2.5e126")]
    public void VolumeFromCubicTerametersShouldProduceExpectedCubicQuectoMeters(string value, string expected)
    {
        Volume<Float256> v = Volume<Float256>.FromCubicTerameters(Float256.Parse(value));
        Assert.Equal(Float256.Parse(expected), v.CubicQuectoMeters);
    }

    [Theory(DisplayName = "Volume.FromCubicPetameters should produce the expected CubicQuectoMeters")]
    [InlineData("0", "0")]
    [InlineData("1", "1e135")]
    [InlineData("2.5", "2.5e135")]
    public void VolumeFromCubicPetametersShouldProduceExpectedCubicQuectoMeters(string value, string expected)
    {
        Volume<Float256> v = Volume<Float256>.FromCubicPetameters(Float256.Parse(value));
        Assert.Equal(Float256.Parse(expected), v.CubicQuectoMeters);
    }

    [Theory(DisplayName = "Volume.FromCubicExameters should produce the expected CubicQuectoMeters")]
    [InlineData("0", "0")]
    [InlineData("1", "1e144")]
    [InlineData("2.5", "2.5e144")]
    public void VolumeFromCubicExametersShouldProduceExpectedCubicQuectoMeters(string value, string expected)
    {
        Volume<Float256> v = Volume<Float256>.FromCubicExameters(Float256.Parse(value));
        Assert.Equal(Float256.Parse(expected), v.CubicQuectoMeters);
    }

    [Theory(DisplayName = "Volume.FromCubicZettameters should produce the expected CubicQuectoMeters")]
    [InlineData("0", "0")]
    [InlineData("1", "1e153")]
    [InlineData("2.5", "2.5e153")]
    public void VolumeFromCubicZettametersShouldProduceExpectedCubicQuectoMeters(string value, string expected)
    {
        Volume<Float256> v = Volume<Float256>.FromCubicZettameters(Float256.Parse(value));
        Assert.Equal(Float256.Parse(expected), v.CubicQuectoMeters);
    }

    [Theory(DisplayName = "Volume.FromCubicYottameters should produce the expected CubicQuectoMeters")]
    [InlineData("0", "0")]
    [InlineData("1", "1e162")]
    [InlineData("2.5", "2.5e162")]
    public void VolumeFromCubicYottametersShouldProduceExpectedCubicQuectoMeters(string value, string expected)
    {
        Volume<Float256> v = Volume<Float256>.FromCubicYottameters(Float256.Parse(value));
        Assert.Equal(Float256.Parse(expected), v.CubicQuectoMeters);
    }

    [Theory(DisplayName = "Volume.FromCubicRonnameters should produce the expected CubicQuectoMeters")]
    [InlineData("0")]
    [InlineData("1")]
    [InlineData("2.5")]
    public void VolumeFromCubicRonnametersShouldProduceExpectedCubicQuectoMeters(string value)
    {
        // Above Float256's 10^102 exact-power-of-10 range, Parse("X.Ye171") and value × Pow10(171)
        // can diverge in the LSB because they multiply mantissa fragments in different orders.
        // Compute expected via the same chain as the unit to keep strict equality meaningful.
        Float256 input = Float256.Parse(value);
        Float256 expected = input * UnitMath.Pow10<Float256>(171);

        Volume<Float256> v = Volume<Float256>.FromCubicRonnameters(input);

        Assert.Equal(expected, v.CubicQuectoMeters);
    }

    [Theory(DisplayName = "Volume.FromCubicQuettameters should produce the expected CubicQuectoMeters")]
    [InlineData("0", "0")]
    [InlineData("1", "1e180")]
    [InlineData("2.5", "2.5e180")]
    public void VolumeFromCubicQuettametersShouldProduceExpectedCubicQuectoMeters(string value, string expected)
    {
        Volume<Float256> v = Volume<Float256>.FromCubicQuettameters(Float256.Parse(value));
        Assert.Equal(Float256.Parse(expected), v.CubicQuectoMeters);
    }

    [Theory(DisplayName = "Volume.FromCubicInches should produce the expected CubicQuectoMeters")]
    [InlineData("0", "0")]
    [InlineData("1", "1.6387064e85")]
    [InlineData("2.5", "4.096766e85")]
    public void VolumeFromCubicInchesShouldProduceExpectedCubicQuectoMeters(string value, string expected)
    {
        Volume<Float256> v = Volume<Float256>.FromCubicInches(Float256.Parse(value));
        Assert.Equal(Float256.Parse(expected), v.CubicQuectoMeters);
    }

    [Theory(DisplayName = "Volume.FromCubicFeet should produce the expected CubicQuectoMeters")]
    [InlineData("0", "0")]
    [InlineData("1", "2.8316846592e88")]
    [InlineData("2.5", "7.0792116480e88")]
    public void VolumeFromCubicFeetShouldProduceExpectedCubicQuectoMeters(string value, string expected)
    {
        Volume<Float256> v = Volume<Float256>.FromCubicFeet(Float256.Parse(value));
        Assert.Equal(Float256.Parse(expected), v.CubicQuectoMeters);
    }

    [Theory(DisplayName = "Volume.FromCubicYards should produce the expected CubicQuectoMeters")]
    [InlineData("0", "0")]
    [InlineData("1", "7.64554857984e89")]
    [InlineData("2", "1.529109715968e90")]
    public void VolumeFromCubicYardsShouldProduceExpectedCubicQuectoMeters(string value, string expected)
    {
        Volume<Float256> v = Volume<Float256>.FromCubicYards(Float256.Parse(value));
        Assert.Equal(Float256.Parse(expected), v.CubicQuectoMeters);
    }

    [Theory(DisplayName = "Volume.FromCubicMiles should produce the expected CubicQuectoMeters")]
    [InlineData("0", "0")]
    [InlineData("1", "4.168181825440579584e99")]
    [InlineData("2", "8.336363650881159168e99")]
    public void VolumeFromCubicMilesShouldProduceExpectedCubicQuectoMeters(string value, string expected)
    {
        Volume<Float256> v = Volume<Float256>.FromCubicMiles(Float256.Parse(value));
        Assert.Equal(Float256.Parse(expected), v.CubicQuectoMeters);
    }

    [Theory(DisplayName = "Volume.FromCubicAstronomicalUnits should produce the expected CubicQuectoMeters")]
    [InlineData("0")]
    [InlineData("1")]
    [InlineData("2.5")]
    public void VolumeFromCubicAstronomicalUnitsShouldProduceExpectedCubicQuectoMeters(string value)
    {
        // Compute expected at Float256 precision via the same chain as the unit: AU³ × 10^90.
        Float256 input = Float256.Parse(value);
        Float256 auMeters = 149_597_870_700L;
        Float256 expected = input * auMeters * auMeters * auMeters * UnitMath.Pow10<Float256>(90);

        Volume<Float256> v = Volume<Float256>.FromCubicAstronomicalUnits(input);

        Assert.Equal(expected, v.CubicQuectoMeters);
    }

    [Theory(DisplayName = "Volume.FromCubicLightYears should produce the expected CubicQuectoMeters")]
    [InlineData("0")]
    [InlineData("1")]
    [InlineData("2.5")]
    public void VolumeFromCubicLightYearsShouldProduceExpectedCubicQuectoMeters(string value)
    {
        // Compute expected at Float256 precision via the same chain as the unit: LY³ × 10^90.
        Float256 input = Float256.Parse(value);
        Float256 lyMeters = 9_460_730_472_580_800L;
        Float256 expected = input * lyMeters * lyMeters * lyMeters * UnitMath.Pow10<Float256>(90);

        Volume<Float256> v = Volume<Float256>.FromCubicLightYears(input);

        Assert.Equal(expected, v.CubicQuectoMeters);
    }

    [Theory(DisplayName = "Volume.FromCubicParsecs should produce the expected CubicQuectoMeters")]
    [InlineData("0")]
    [InlineData("1")]
    [InlineData("2.5")]
    public void VolumeFromCubicParsecsShouldProduceExpectedCubicQuectoMeters(string value)
    {
        // Compute expected via the IAU definition: 1 pc = (648000 / π) AU. Match the unit's
        // associativity exactly — group as `(metersPerParsec³ × Pow10(90))` first (matching the
        // cached CuQuectometersPerCubicParsec constant), then multiply by input. π is irrational,
        // so the division rounds and grouping changes the LSB.
        Float256 input = Float256.Parse(value);
        Float256 metersPerParsec = (Float256)149_597_870_700L * 648000 / Float256.Pi;
        Float256 cuQmPerCuParsec = metersPerParsec * metersPerParsec * metersPerParsec * UnitMath.Pow10<Float256>(90);
        Float256 expected = input * cuQmPerCuParsec;

        Volume<Float256> v = Volume<Float256>.FromCubicParsecs(input);

        Assert.Equal(expected, v.CubicQuectoMeters);
    }

    [Theory(DisplayName = "Volume.FromLiters should produce the expected CubicQuectoMeters")]
    [InlineData("0", "0")]
    [InlineData("1", "1e87")]
    [InlineData("1000", "1e90")] // 1000 L = 1 m³
    public void VolumeFromLitersShouldProduceExpectedCubicQuectoMeters(string value, string expected)
    {
        Volume<Float256> v = Volume<Float256>.FromLiters(Float256.Parse(value));
        Assert.Equal(Float256.Parse(expected), v.CubicQuectoMeters);
    }

    [Theory(DisplayName = "Volume.FromMilliliters should produce the expected CubicQuectoMeters")]
    [InlineData("0", "0")]
    [InlineData("1", "1e84")] // 1 mL = 1 cm³
    [InlineData("1000", "1e87")] // 1000 mL = 1 L
    public void VolumeFromMillilitersShouldProduceExpectedCubicQuectoMeters(string value, string expected)
    {
        Volume<Float256> v = Volume<Float256>.FromMilliliters(Float256.Parse(value));
        Assert.Equal(Float256.Parse(expected), v.CubicQuectoMeters);
    }

    [Theory(DisplayName = "Volume.FromUsGallons should produce the expected CubicQuectoMeters")]
    [InlineData("0", "0")]
    [InlineData("1", "3.785411784e87")]
    [InlineData("42", "1.58987294928e89")] // 42 US gal = 1 oil barrel
    public void VolumeFromUsGallonsShouldProduceExpectedCubicQuectoMeters(string value, string expected)
    {
        Volume<Float256> v = Volume<Float256>.FromUsGallons(Float256.Parse(value));
        Assert.Equal(Float256.Parse(expected), v.CubicQuectoMeters);
    }

    [Theory(DisplayName = "Volume.FromUsQuarts should produce the expected CubicQuectoMeters")]
    [InlineData("0", "0")]
    [InlineData("1", "9.46352946e86")]
    [InlineData("4", "3.785411784e87")] // 4 US qt = 1 US gal
    public void VolumeFromUsQuartsShouldProduceExpectedCubicQuectoMeters(string value, string expected)
    {
        Volume<Float256> v = Volume<Float256>.FromUsQuarts(Float256.Parse(value));
        Assert.Equal(Float256.Parse(expected), v.CubicQuectoMeters);
    }

    [Theory(DisplayName = "Volume.FromUsPints should produce the expected CubicQuectoMeters")]
    [InlineData("0", "0")]
    [InlineData("1", "4.73176473e86")]
    [InlineData("2", "9.46352946e86")] // 2 US pt = 1 US qt
    public void VolumeFromUsPintsShouldProduceExpectedCubicQuectoMeters(string value, string expected)
    {
        Volume<Float256> v = Volume<Float256>.FromUsPints(Float256.Parse(value));
        Assert.Equal(Float256.Parse(expected), v.CubicQuectoMeters);
    }

    [Theory(DisplayName = "Volume.FromUsCups should produce the expected CubicQuectoMeters")]
    [InlineData("0", "0")]
    [InlineData("1", "2.365882365e86")]
    [InlineData("2", "4.73176473e86")] // 2 US cups = 1 US pt
    public void VolumeFromUsCupsShouldProduceExpectedCubicQuectoMeters(string value, string expected)
    {
        Volume<Float256> v = Volume<Float256>.FromUsCups(Float256.Parse(value));
        Assert.Equal(Float256.Parse(expected), v.CubicQuectoMeters);
    }

    [Theory(DisplayName = "Volume.FromUsFluidOunces should produce the expected CubicQuectoMeters")]
    [InlineData("0", "0")]
    [InlineData("1", "2.95735295625e85")]
    [InlineData("8", "2.365882365e86")] // 8 US fl oz = 1 US cup
    public void VolumeFromUsFluidOuncesShouldProduceExpectedCubicQuectoMeters(string value, string expected)
    {
        Volume<Float256> v = Volume<Float256>.FromUsFluidOunces(Float256.Parse(value));
        Assert.Equal(Float256.Parse(expected), v.CubicQuectoMeters);
    }

    [Theory(DisplayName = "Volume.FromUsTablespoons should produce the expected CubicQuectoMeters")]
    [InlineData("0", "0")]
    [InlineData("1", "1.478676478125e85")]
    [InlineData("2", "2.95735295625e85")] // 2 US tbsp = 1 US fl oz
    public void VolumeFromUsTablespoonsShouldProduceExpectedCubicQuectoMeters(string value, string expected)
    {
        Volume<Float256> v = Volume<Float256>.FromUsTablespoons(Float256.Parse(value));
        Assert.Equal(Float256.Parse(expected), v.CubicQuectoMeters);
    }

    [Theory(DisplayName = "Volume.FromUsTeaspoons should produce the expected CubicQuectoMeters")]
    [InlineData("0", "0")]
    [InlineData("1", "4.92892159375e84")]
    [InlineData("3", "1.478676478125e85")] // 3 US tsp = 1 US tbsp
    public void VolumeFromUsTeaspoonsShouldProduceExpectedCubicQuectoMeters(string value, string expected)
    {
        Volume<Float256> v = Volume<Float256>.FromUsTeaspoons(Float256.Parse(value));
        Assert.Equal(Float256.Parse(expected), v.CubicQuectoMeters);
    }

    [Theory(DisplayName = "Volume.FromImperialGallons should produce the expected CubicQuectoMeters")]
    [InlineData("0", "0")]
    [InlineData("1", "4.54609e87")]
    [InlineData("2.5", "1.1365225e88")]
    public void VolumeFromImperialGallonsShouldProduceExpectedCubicQuectoMeters(string value, string expected)
    {
        Volume<Float256> v = Volume<Float256>.FromImperialGallons(Float256.Parse(value));
        Assert.Equal(Float256.Parse(expected), v.CubicQuectoMeters);
    }

    [Theory(DisplayName = "Volume.FromImperialQuarts should produce the expected CubicQuectoMeters")]
    [InlineData("0", "0")]
    [InlineData("1", "1.1365225e87")]
    [InlineData("4", "4.54609e87")] // 4 imp qt = 1 imp gal
    public void VolumeFromImperialQuartsShouldProduceExpectedCubicQuectoMeters(string value, string expected)
    {
        Volume<Float256> v = Volume<Float256>.FromImperialQuarts(Float256.Parse(value));
        Assert.Equal(Float256.Parse(expected), v.CubicQuectoMeters);
    }

    [Theory(DisplayName = "Volume.FromImperialPints should produce the expected CubicQuectoMeters")]
    [InlineData("0", "0")]
    [InlineData("1", "5.6826125e86")]
    [InlineData("2", "1.1365225e87")] // 2 imp pt = 1 imp qt
    public void VolumeFromImperialPintsShouldProduceExpectedCubicQuectoMeters(string value, string expected)
    {
        Volume<Float256> v = Volume<Float256>.FromImperialPints(Float256.Parse(value));
        Assert.Equal(Float256.Parse(expected), v.CubicQuectoMeters);
    }

    [Theory(DisplayName = "Volume.FromImperialFluidOunces should produce the expected CubicQuectoMeters")]
    [InlineData("0", "0")]
    [InlineData("1", "2.84130625e85")]
    [InlineData("20", "5.6826125e86")] // 20 imp fl oz = 1 imp pt
    public void VolumeFromImperialFluidOuncesShouldProduceExpectedCubicQuectoMeters(string value, string expected)
    {
        Volume<Float256> v = Volume<Float256>.FromImperialFluidOunces(Float256.Parse(value));
        Assert.Equal(Float256.Parse(expected), v.CubicQuectoMeters);
    }

    [Theory(DisplayName = "Volume.FromOilBarrels should produce the expected CubicQuectoMeters")]
    [InlineData("0", "0")]
    [InlineData("1", "1.58987294928e89")]
    [InlineData("2.5", "3.9746823732e89")]
    public void VolumeFromOilBarrelsShouldProduceExpectedCubicQuectoMeters(string value, string expected)
    {
        Volume<Float256> v = Volume<Float256>.FromOilBarrels(Float256.Parse(value));
        Assert.Equal(Float256.Parse(expected), v.CubicQuectoMeters);
    }

    [Fact(DisplayName = "Volume.Add should produce the expected result")]
    public void VolumeAddShouldProduceExpectedValue()
    {
        // Given
        Volume<Float256> left = Volume<Float256>.FromLiters(Float256.Parse("1.5"));
        Volume<Float256> right = Volume<Float256>.FromLiters(Float256.Parse("0.5"));

        // When
        Volume<Float256> result = left.Add(right);

        // Then
        Assert.Equal(Float256.Parse("2"), result.Liters);
    }

    [Fact(DisplayName = "Volume.Subtract should produce the expected result")]
    public void VolumeSubtractShouldProduceExpectedValue()
    {
        // Given
        Volume<Float256> left = Volume<Float256>.FromLiters(Float256.Parse("1.5"));
        Volume<Float256> right = Volume<Float256>.FromLiters(Float256.Parse("0.4"));

        // When
        Volume<Float256> result = left.Subtract(right);

        // Then
        Assert.Equal(Float256.Parse("1.1"), result.Liters);
    }

    [Fact(DisplayName = "Volume comparison should produce the expected result (left equal to right)")]
    public void VolumeComparisonShouldProduceExpectedResultLeftEqualToRight()
    {
        // Given
        Volume<Float256> left = Volume<Float256>.FromLiters(Float256.Parse("123"));
        Volume<Float256> right = Volume<Float256>.FromLiters(Float256.Parse("123"));

        // When / Then
        Assert.Equal(0, Volume<Float256>.Compare(left, right));
        Assert.Equal(0, left.CompareTo(right));
        Assert.Equal(0, left.CompareTo((object)right));
        Assert.False(left > right);
        Assert.True(left >= right);
        Assert.False(left < right);
        Assert.True(left <= right);
    }

    [Fact(DisplayName = "Volume comparison should produce the expected result (left greater than right)")]
    public void VolumeComparisonShouldProduceExpectedLeftGreaterThanRight()
    {
        // Given
        Volume<Float256> left = Volume<Float256>.FromLiters(Float256.Parse("456"));
        Volume<Float256> right = Volume<Float256>.FromLiters(Float256.Parse("123"));

        // When / Then
        Assert.Equal(1, Volume<Float256>.Compare(left, right));
        Assert.Equal(1, left.CompareTo(right));
        Assert.Equal(1, left.CompareTo((object)right));
        Assert.True(left > right);
        Assert.True(left >= right);
        Assert.False(left < right);
        Assert.False(left <= right);
    }

    [Fact(DisplayName = "Volume comparison should produce the expected result (left less than right)")]
    public void VolumeComparisonShouldProduceExpectedLeftLessThanRight()
    {
        // Given
        Volume<Float256> left = Volume<Float256>.FromLiters(Float256.Parse("123"));
        Volume<Float256> right = Volume<Float256>.FromLiters(Float256.Parse("456"));

        // When / Then
        Assert.Equal(-1, Volume<Float256>.Compare(left, right));
        Assert.Equal(-1, left.CompareTo(right));
        Assert.Equal(-1, left.CompareTo((object)right));
        Assert.False(left > right);
        Assert.False(left >= right);
        Assert.True(left < right);
        Assert.True(left <= right);
    }

    [Fact(DisplayName = "Volume equality should produce the expected result (left equal to right)")]
    public void VolumeEqualityShouldProduceExpectedResultLeftEqualToRight()
    {
        // Given — 2 L and 2000 mL reduce to the same canonical at Float256.
        Volume<Float256> left = Volume<Float256>.FromLiters(Float256.Parse("2"));
        Volume<Float256> right = Volume<Float256>.FromMilliliters(Float256.Parse("2000"));

        // When / Then
        Assert.True(Volume<Float256>.Equals(left, right));
        Assert.True(left.Equals(right));
        Assert.True(left.Equals((object)right));
        Assert.True(left == right);
        Assert.False(left != right);
    }

    [Fact(DisplayName = "Volume equality should produce the expected result (left not equal to right)")]
    public void VolumeEqualityShouldProduceExpectedResultLeftNotEqualToRight()
    {
        // Given
        Volume<Float256> left = Volume<Float256>.FromLiters(Float256.Parse("2"));
        Volume<Float256> right = Volume<Float256>.FromMilliliters(Float256.Parse("2500"));

        // When / Then
        Assert.False(Volume<Float256>.Equals(left, right));
        Assert.False(left.Equals(right));
        Assert.False(left.Equals((object)right));
        Assert.False(left == right);
        Assert.True(left != right);
    }

    [Fact(DisplayName = "Volume.ToString should produce the expected result")]
    public void VolumeToStringShouldProduceExpectedResult()
    {
        // Given
        Volume<Float256> v = Volume<Float256>.FromCubicMeters(Float256.Parse("1"));

        // When / Then
        Assert.Equal("1.000 m³", $"{v:cum3}");
        Assert.Equal("1,000.000 dm³", $"{v:cudm3}");
        Assert.Equal("1,000.000 L", $"{v:L3}");
        Assert.Equal("1,000,000.000 mL", $"{v:mL3}");
        Assert.Equal("264.172 US gal", $"{v:USgal3}");
        Assert.Equal("219.969 imp gal", $"{v:impgal3}");
        Assert.Equal("33,814.023 US fl oz", $"{v:USfloz3}");
        Assert.Equal("6.290 bbl", $"{v:bbl3}");
    }

    [Fact(DisplayName = "Volume.ToString should honor custom culture separators")]
    public void VolumeToStringShouldHonorCustomCulture()
    {
        // Given
        CultureInfo customCulture = new("de-DE");
        Volume<Float256> v = Volume<Float256>.FromLiters(Float256.Parse("1234.56"));

        // When
        string formatted = v.ToString("L2", customCulture);

        // Then
        Assert.Equal("1.234,56 L", formatted);
    }

    [Theory(DisplayName = "Volume.ValueOf should return the value at the matching scale")]
    [InlineData("cuqm")]
    [InlineData("curm")]
    [InlineData("cuym")]
    [InlineData("cuzm")]
    [InlineData("cuam")]
    [InlineData("cufm")]
    [InlineData("cupm")]
    [InlineData("cunm")]
    [InlineData("cuum")]
    [InlineData("cumm")]
    [InlineData("cucm")]
    [InlineData("cudm")]
    [InlineData("cum")]
    [InlineData("cudam")]
    [InlineData("cuhm")]
    [InlineData("cukm")]
    [InlineData("cuMm")]
    [InlineData("cuGm")]
    [InlineData("cuTm")]
    [InlineData("cuPm")]
    [InlineData("cuEm")]
    [InlineData("cuZm")]
    [InlineData("cuYm")]
    [InlineData("cuRm")]
    [InlineData("cuQm")]
    [InlineData("cuin")]
    [InlineData("cuft")]
    [InlineData("cuyd")]
    [InlineData("cumi")]
    [InlineData("cuau")]
    [InlineData("culy")]
    [InlineData("cupc")]
    [InlineData("L")]
    [InlineData("mL")]
    [InlineData("USgal")]
    [InlineData("USqt")]
    [InlineData("USpt")]
    [InlineData("UScup")]
    [InlineData("USfloz")]
    [InlineData("UStbsp")]
    [InlineData("UStsp")]
    [InlineData("impgal")]
    [InlineData("impqt")]
    [InlineData("imppt")]
    [InlineData("impfloz")]
    [InlineData("bbl")]
    public void VolumeValueOfShouldReturnValueAtMatchingScale(string specifier)
    {
        // Given
        Volume<Float256> v = Volume<Float256>.FromCubicMeters(Float256.Parse("1234.567"));

        // When
        Float256 expected = specifier switch
        {
            "cuqm" => v.CubicQuectoMeters,
            "curm" => v.CubicRontoMeters,
            "cuym" => v.CubicYoctoMeters,
            "cuzm" => v.CubicZeptoMeters,
            "cuam" => v.CubicAttoMeters,
            "cufm" => v.CubicFemtoMeters,
            "cupm" => v.CubicPicoMeters,
            "cunm" => v.CubicNanoMeters,
            "cuum" => v.CubicMicroMeters,
            "cumm" => v.CubicMilliMeters,
            "cucm" => v.CubicCentiMeters,
            "cudm" => v.CubicDeciMeters,
            "cum" => v.CubicMeters,
            "cudam" => v.CubicDecaMeters,
            "cuhm" => v.CubicHectoMeters,
            "cukm" => v.CubicKiloMeters,
            "cuMm" => v.CubicMegaMeters,
            "cuGm" => v.CubicGigaMeters,
            "cuTm" => v.CubicTeraMeters,
            "cuPm" => v.CubicPetaMeters,
            "cuEm" => v.CubicExaMeters,
            "cuZm" => v.CubicZettaMeters,
            "cuYm" => v.CubicYottaMeters,
            "cuRm" => v.CubicRonnaMeters,
            "cuQm" => v.CubicQuettaMeters,
            "cuin" => v.CubicInches,
            "cuft" => v.CubicFeet,
            "cuyd" => v.CubicYards,
            "cumi" => v.CubicMiles,
            "cuau" => v.CubicAstronomicalUnits,
            "culy" => v.CubicLightYears,
            "cupc" => v.CubicParsecs,
            "L" => v.Liters,
            "mL" => v.Milliliters,
            "USgal" => v.UsGallons,
            "USqt" => v.UsQuarts,
            "USpt" => v.UsPints,
            "UScup" => v.UsCups,
            "USfloz" => v.UsFluidOunces,
            "UStbsp" => v.UsTablespoons,
            "UStsp" => v.UsTeaspoons,
            "impgal" => v.ImperialGallons,
            "impqt" => v.ImperialQuarts,
            "imppt" => v.ImperialPints,
            "impfloz" => v.ImperialFluidOunces,
            "bbl" => v.OilBarrels,
            _ => throw new InvalidOperationException($"Unhandled specifier: {specifier}")
        };

        // Then
        Assert.Equal(expected, v.ValueOf(specifier));
    }

    [Fact(DisplayName = "Volume.ValueOf should throw on invalid specifier")]
    public void VolumeValueOfShouldThrowOnInvalidSpecifier()
    {
        // Given
        Volume<Float256> v = Volume<Float256>.FromCubicMeters(Float256.Parse("1"));

        // Then
        Assert.Throws<ArgumentException>(() => v.ValueOf("xx"));
    }
}
