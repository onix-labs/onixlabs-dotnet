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
    // IEEE-754 binary floating-point arithmetic causes small discrepancies in calculation, therefore we need a tolerance.
    // Volume canonical values reach ~1e180 (CubicQuettameters), so the tolerance must scale accordingly.
    private const double Tolerance = 1e+170;

    [Fact(DisplayName = "Volume.Zero should produce the expected result")]
    public void VolumeZeroShouldProduceExpectedResult()
    {
        // Given / When
        Volume<double> volume = Volume<double>.Zero;

        // Then
        Assert.Equal(0.0, volume.CubicQuectoMeters, Tolerance);
    }

    [Theory(DisplayName = "Volume.FromCubicQuectometers should produce the expected CubicQuectoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1.0)]
    [InlineData(2.5, 2.5)]
    public void VolumeFromCubicQuectometersShouldProduceExpectedCubicQuectoMeters(double value, double expected)
    {
        Volume<double> v = Volume<double>.FromCubicQuectometers(value);
        Assert.Equal(expected, v.CubicQuectoMeters, Tolerance);
    }

    [Theory(DisplayName = "Volume.FromCubicRontometers should produce the expected CubicQuectoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e9)]
    [InlineData(2.5, 2.5e9)]
    public void VolumeFromCubicRontometersShouldProduceExpectedCubicQuectoMeters(double value, double expected)
    {
        Volume<double> v = Volume<double>.FromCubicRontometers(value);
        Assert.Equal(expected, v.CubicQuectoMeters, Tolerance);
    }

    [Theory(DisplayName = "Volume.FromCubicYoctometers should produce the expected CubicQuectoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e18)]
    [InlineData(2.5, 2.5e18)]
    public void VolumeFromCubicYoctometersShouldProduceExpectedCubicQuectoMeters(double value, double expected)
    {
        Volume<double> v = Volume<double>.FromCubicYoctometers(value);
        Assert.Equal(expected, v.CubicQuectoMeters, Tolerance);
    }

    [Theory(DisplayName = "Volume.FromCubicZeptometers should produce the expected CubicQuectoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e27)]
    [InlineData(2.5, 2.5e27)]
    public void VolumeFromCubicZeptometersShouldProduceExpectedCubicQuectoMeters(double value, double expected)
    {
        Volume<double> v = Volume<double>.FromCubicZeptometers(value);
        Assert.Equal(expected, v.CubicQuectoMeters, Tolerance);
    }

    [Theory(DisplayName = "Volume.FromCubicAttometers should produce the expected CubicQuectoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e36)]
    [InlineData(2.5, 2.5e36)]
    public void VolumeFromCubicAttometersShouldProduceExpectedCubicQuectoMeters(double value, double expected)
    {
        Volume<double> v = Volume<double>.FromCubicAttometers(value);
        Assert.Equal(expected, v.CubicQuectoMeters, Tolerance);
    }

    [Theory(DisplayName = "Volume.FromCubicFemtometers should produce the expected CubicQuectoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e45)]
    [InlineData(2.5, 2.5e45)]
    public void VolumeFromCubicFemtometersShouldProduceExpectedCubicQuectoMeters(double value, double expected)
    {
        Volume<double> v = Volume<double>.FromCubicFemtometers(value);
        Assert.Equal(expected, v.CubicQuectoMeters, Tolerance);
    }

    [Theory(DisplayName = "Volume.FromCubicPicometers should produce the expected CubicQuectoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e54)]
    [InlineData(2.5, 2.5e54)]
    public void VolumeFromCubicPicometersShouldProduceExpectedCubicQuectoMeters(double value, double expected)
    {
        Volume<double> v = Volume<double>.FromCubicPicometers(value);
        Assert.Equal(expected, v.CubicQuectoMeters, Tolerance);
    }

    [Theory(DisplayName = "Volume.FromCubicNanometers should produce the expected CubicQuectoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e63)]
    [InlineData(2.5, 2.5e63)]
    public void VolumeFromCubicNanometersShouldProduceExpectedCubicQuectoMeters(double value, double expected)
    {
        Volume<double> v = Volume<double>.FromCubicNanometers(value);
        Assert.Equal(expected, v.CubicQuectoMeters, Tolerance);
    }

    [Theory(DisplayName = "Volume.FromCubicMicrometers should produce the expected CubicQuectoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e72)]
    [InlineData(2.5, 2.5e72)]
    public void VolumeFromCubicMicrometersShouldProduceExpectedCubicQuectoMeters(double value, double expected)
    {
        Volume<double> v = Volume<double>.FromCubicMicrometers(value);
        Assert.Equal(expected, v.CubicQuectoMeters, Tolerance);
    }

    [Theory(DisplayName = "Volume.FromCubicMillimeters should produce the expected CubicQuectoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e81)]
    [InlineData(2.5, 2.5e81)]
    public void VolumeFromCubicMillimetersShouldProduceExpectedCubicQuectoMeters(double value, double expected)
    {
        Volume<double> v = Volume<double>.FromCubicMillimeters(value);
        Assert.Equal(expected, v.CubicQuectoMeters, Tolerance);
    }

    [Theory(DisplayName = "Volume.FromCubicCentimeters should produce the expected CubicQuectoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e84)]
    [InlineData(2.5, 2.5e84)]
    public void VolumeFromCubicCentimetersShouldProduceExpectedCubicQuectoMeters(double value, double expected)
    {
        Volume<double> v = Volume<double>.FromCubicCentimeters(value);
        Assert.Equal(expected, v.CubicQuectoMeters, Tolerance);
    }

    [Theory(DisplayName = "Volume.FromCubicDecimeters should produce the expected CubicQuectoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e87)]
    [InlineData(2.5, 2.5e87)]
    public void VolumeFromCubicDecimetersShouldProduceExpectedCubicQuectoMeters(double value, double expected)
    {
        Volume<double> v = Volume<double>.FromCubicDecimeters(value);
        Assert.Equal(expected, v.CubicQuectoMeters, Tolerance);
    }

    [Theory(DisplayName = "Volume.FromCubicMeters should produce the expected CubicQuectoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e90)]
    [InlineData(2.5, 2.5e90)]
    public void VolumeFromCubicMetersShouldProduceExpectedCubicQuectoMeters(double value, double expected)
    {
        Volume<double> v = Volume<double>.FromCubicMeters(value);
        Assert.Equal(expected, v.CubicQuectoMeters, Tolerance);
    }

    [Theory(DisplayName = "Volume.FromCubicDecameters should produce the expected CubicQuectoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e93)]
    [InlineData(2.5, 2.5e93)]
    public void VolumeFromCubicDecametersShouldProduceExpectedCubicQuectoMeters(double value, double expected)
    {
        Volume<double> v = Volume<double>.FromCubicDecameters(value);
        Assert.Equal(expected, v.CubicQuectoMeters, Tolerance);
    }

    [Theory(DisplayName = "Volume.FromCubicHectometers should produce the expected CubicQuectoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e96)]
    [InlineData(2.5, 2.5e96)]
    public void VolumeFromCubicHectometersShouldProduceExpectedCubicQuectoMeters(double value, double expected)
    {
        Volume<double> v = Volume<double>.FromCubicHectometers(value);
        Assert.Equal(expected, v.CubicQuectoMeters, Tolerance);
    }

    [Theory(DisplayName = "Volume.FromCubicKilometers should produce the expected CubicQuectoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e99)]
    [InlineData(2.5, 2.5e99)]
    public void VolumeFromCubicKilometersShouldProduceExpectedCubicQuectoMeters(double value, double expected)
    {
        Volume<double> v = Volume<double>.FromCubicKilometers(value);
        Assert.Equal(expected, v.CubicQuectoMeters, Tolerance);
    }

    [Theory(DisplayName = "Volume.FromCubicMegameters should produce the expected CubicQuectoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e108)]
    [InlineData(2.5, 2.5e108)]
    public void VolumeFromCubicMegametersShouldProduceExpectedCubicQuectoMeters(double value, double expected)
    {
        Volume<double> v = Volume<double>.FromCubicMegameters(value);
        Assert.Equal(expected, v.CubicQuectoMeters, Tolerance);
    }

    [Theory(DisplayName = "Volume.FromCubicGigameters should produce the expected CubicQuectoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e117)]
    [InlineData(2.5, 2.5e117)]
    public void VolumeFromCubicGigametersShouldProduceExpectedCubicQuectoMeters(double value, double expected)
    {
        Volume<double> v = Volume<double>.FromCubicGigameters(value);
        Assert.Equal(expected, v.CubicQuectoMeters, Tolerance);
    }

    [Theory(DisplayName = "Volume.FromCubicTerameters should produce the expected CubicQuectoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e126)]
    [InlineData(2.5, 2.5e126)]
    public void VolumeFromCubicTerametersShouldProduceExpectedCubicQuectoMeters(double value, double expected)
    {
        Volume<double> v = Volume<double>.FromCubicTerameters(value);
        Assert.Equal(expected, v.CubicQuectoMeters, Tolerance);
    }

    [Theory(DisplayName = "Volume.FromCubicPetameters should produce the expected CubicQuectoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e135)]
    [InlineData(2.5, 2.5e135)]
    public void VolumeFromCubicPetametersShouldProduceExpectedCubicQuectoMeters(double value, double expected)
    {
        Volume<double> v = Volume<double>.FromCubicPetameters(value);
        Assert.Equal(expected, v.CubicQuectoMeters, Tolerance);
    }

    [Theory(DisplayName = "Volume.FromCubicExameters should produce the expected CubicQuectoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e144)]
    [InlineData(2.5, 2.5e144)]
    public void VolumeFromCubicExametersShouldProduceExpectedCubicQuectoMeters(double value, double expected)
    {
        Volume<double> v = Volume<double>.FromCubicExameters(value);
        Assert.Equal(expected, v.CubicQuectoMeters, Tolerance);
    }

    [Theory(DisplayName = "Volume.FromCubicZettameters should produce the expected CubicQuectoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e153)]
    [InlineData(2.5, 2.5e153)]
    public void VolumeFromCubicZettametersShouldProduceExpectedCubicQuectoMeters(double value, double expected)
    {
        Volume<double> v = Volume<double>.FromCubicZettameters(value);
        Assert.Equal(expected, v.CubicQuectoMeters, Tolerance);
    }

    [Theory(DisplayName = "Volume.FromCubicYottameters should produce the expected CubicQuectoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e162)]
    [InlineData(2.5, 2.5e162)]
    public void VolumeFromCubicYottametersShouldProduceExpectedCubicQuectoMeters(double value, double expected)
    {
        Volume<double> v = Volume<double>.FromCubicYottameters(value);
        Assert.Equal(expected, v.CubicQuectoMeters, Tolerance);
    }

    [Theory(DisplayName = "Volume.FromCubicRonnameters should produce the expected CubicQuectoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e171)]
    [InlineData(2.5, 2.5e171)]
    public void VolumeFromCubicRonnametersShouldProduceExpectedCubicQuectoMeters(double value, double expected)
    {
        Volume<double> v = Volume<double>.FromCubicRonnameters(value);
        Assert.Equal(expected, v.CubicQuectoMeters, Tolerance);
    }

    [Theory(DisplayName = "Volume.FromCubicQuettameters should produce the expected CubicQuectoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e180)]
    [InlineData(2.5, 2.5e180)]
    public void VolumeFromCubicQuettametersShouldProduceExpectedCubicQuectoMeters(double value, double expected)
    {
        Volume<double> v = Volume<double>.FromCubicQuettameters(value);
        Assert.Equal(expected, v.CubicQuectoMeters, Tolerance);
    }

    [Theory(DisplayName = "Volume.FromCubicInches should produce the expected CubicQuectoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1.6387064e85)]
    [InlineData(2.5, 4.096766e85)]
    public void VolumeFromCubicInchesShouldProduceExpectedCubicQuectoMeters(double value, double expected)
    {
        Volume<double> v = Volume<double>.FromCubicInches(value);
        Assert.Equal(expected, v.CubicQuectoMeters, Tolerance);
    }

    [Theory(DisplayName = "Volume.FromCubicFeet should produce the expected CubicQuectoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 2.8316846592e88)]
    [InlineData(2.5, 7.0792116480e88)]
    public void VolumeFromCubicFeetShouldProduceExpectedCubicQuectoMeters(double value, double expected)
    {
        Volume<double> v = Volume<double>.FromCubicFeet(value);
        Assert.Equal(expected, v.CubicQuectoMeters, Tolerance);
    }

    [Theory(DisplayName = "Volume.FromCubicYards should produce the expected CubicQuectoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 7.64554857984e89)]
    [InlineData(2.0, 1.529109715968e90)]
    public void VolumeFromCubicYardsShouldProduceExpectedCubicQuectoMeters(double value, double expected)
    {
        Volume<double> v = Volume<double>.FromCubicYards(value);
        Assert.Equal(expected, v.CubicQuectoMeters, Tolerance);
    }

    [Theory(DisplayName = "Volume.FromCubicMiles should produce the expected CubicQuectoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 4.168181825440579584e99)]
    [InlineData(2.0, 8.336363650881159168e99)]
    public void VolumeFromCubicMilesShouldProduceExpectedCubicQuectoMeters(double value, double expected)
    {
        Volume<double> v = Volume<double>.FromCubicMiles(value);
        Assert.Equal(expected, v.CubicQuectoMeters, Tolerance);
    }

    [Theory(DisplayName = "Volume.FromCubicAstronomicalUnits should produce the expected CubicQuectoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 3.347928976e123)]
    [InlineData(2.5, 8.36982244e123)]
    public void VolumeFromCubicAstronomicalUnitsShouldProduceExpectedCubicQuectoMeters(double value, double expected)
    {
        Volume<double> v = Volume<double>.FromCubicAstronomicalUnits(value);
        Assert.Equal(expected, v.CubicQuectoMeters, Tolerance);
    }

    [Theory(DisplayName = "Volume.FromCubicLightYears should produce the expected CubicQuectoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 8.46808e137)]
    [InlineData(2.5, 2.11702e138)]
    public void VolumeFromCubicLightYearsShouldProduceExpectedCubicQuectoMeters(double value, double expected)
    {
        Volume<double> v = Volume<double>.FromCubicLightYears(value);
        Assert.Equal(expected, v.CubicQuectoMeters, Tolerance);
    }

    [Theory(DisplayName = "Volume.FromCubicParsecs should produce the expected CubicQuectoMeters")]
    [InlineData(0.0)]
    [InlineData(1.0)]
    [InlineData(2.5)]
    public void VolumeFromCubicParsecsShouldProduceExpectedCubicQuectoMeters(double value)
    {
        // Given
        const double metersPerParsec = 1.495978707e11 * 648000.0 / Math.PI;
        const double cuMetersPerCuParsec = metersPerParsec * metersPerParsec * metersPerParsec;
        const double cuQmPerCuParsec = cuMetersPerCuParsec * 1e90;
        double expected = value * cuQmPerCuParsec;

        // When
        Volume<double> v = Volume<double>.FromCubicParsecs(value);

        // Then
        Assert.Equal(expected, v.CubicQuectoMeters, Tolerance);
    }

    [Theory(DisplayName = "Volume.FromLiters should produce the expected CubicQuectoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e87)]
    [InlineData(1000.0, 1e90)] // 1000 L = 1 m³
    public void VolumeFromLitersShouldProduceExpectedCubicQuectoMeters(double value, double expected)
    {
        Volume<double> v = Volume<double>.FromLiters(value);
        Assert.Equal(expected, v.CubicQuectoMeters, Tolerance);
    }

    [Theory(DisplayName = "Volume.FromMilliliters should produce the expected CubicQuectoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e84)] // 1 mL = 1 cm³
    [InlineData(1000.0, 1e87)] // 1000 mL = 1 L
    public void VolumeFromMillilitersShouldProduceExpectedCubicQuectoMeters(double value, double expected)
    {
        Volume<double> v = Volume<double>.FromMilliliters(value);
        Assert.Equal(expected, v.CubicQuectoMeters, Tolerance);
    }

    [Theory(DisplayName = "Volume.FromUSGallons should produce the expected CubicQuectoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 3.785411784e87)]
    [InlineData(42.0, 1.58987294928e89)] // 42 US gal = 1 oil barrel
    public void VolumeFromUSGallonsShouldProduceExpectedCubicQuectoMeters(double value, double expected)
    {
        Volume<double> v = Volume<double>.FromUSGallons(value);
        Assert.Equal(expected, v.CubicQuectoMeters, Tolerance);
    }

    [Theory(DisplayName = "Volume.FromUSQuarts should produce the expected CubicQuectoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 9.46352946e86)]
    [InlineData(4.0, 3.785411784e87)] // 4 US qt = 1 US gal
    public void VolumeFromUSQuartsShouldProduceExpectedCubicQuectoMeters(double value, double expected)
    {
        Volume<double> v = Volume<double>.FromUSQuarts(value);
        Assert.Equal(expected, v.CubicQuectoMeters, Tolerance);
    }

    [Theory(DisplayName = "Volume.FromUSPints should produce the expected CubicQuectoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 4.73176473e86)]
    [InlineData(2.0, 9.46352946e86)] // 2 US pt = 1 US qt
    public void VolumeFromUSPintsShouldProduceExpectedCubicQuectoMeters(double value, double expected)
    {
        Volume<double> v = Volume<double>.FromUSPints(value);
        Assert.Equal(expected, v.CubicQuectoMeters, Tolerance);
    }

    [Theory(DisplayName = "Volume.FromUSCups should produce the expected CubicQuectoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 2.365882365e86)]
    [InlineData(2.0, 4.73176473e86)] // 2 US cups = 1 US pt
    public void VolumeFromUSCupsShouldProduceExpectedCubicQuectoMeters(double value, double expected)
    {
        Volume<double> v = Volume<double>.FromUSCups(value);
        Assert.Equal(expected, v.CubicQuectoMeters, Tolerance);
    }

    [Theory(DisplayName = "Volume.FromUSFluidOunces should produce the expected CubicQuectoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 2.95735295625e85)]
    [InlineData(8.0, 2.365882365e86)] // 8 US fl oz = 1 US cup
    public void VolumeFromUSFluidOuncesShouldProduceExpectedCubicQuectoMeters(double value, double expected)
    {
        Volume<double> v = Volume<double>.FromUSFluidOunces(value);
        Assert.Equal(expected, v.CubicQuectoMeters, Tolerance);
    }

    [Theory(DisplayName = "Volume.FromUSTablespoons should produce the expected CubicQuectoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1.478676478125e85)]
    [InlineData(2.0, 2.95735295625e85)] // 2 US tbsp = 1 US fl oz
    public void VolumeFromUSTablespoonsShouldProduceExpectedCubicQuectoMeters(double value, double expected)
    {
        Volume<double> v = Volume<double>.FromUSTablespoons(value);
        Assert.Equal(expected, v.CubicQuectoMeters, Tolerance);
    }

    [Theory(DisplayName = "Volume.FromUSTeaspoons should produce the expected CubicQuectoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 4.92892159375e84)]
    [InlineData(3.0, 1.478676478125e85)] // 3 US tsp = 1 US tbsp
    public void VolumeFromUSTeaspoonsShouldProduceExpectedCubicQuectoMeters(double value, double expected)
    {
        Volume<double> v = Volume<double>.FromUSTeaspoons(value);
        Assert.Equal(expected, v.CubicQuectoMeters, Tolerance);
    }

    [Theory(DisplayName = "Volume.FromImperialGallons should produce the expected CubicQuectoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 4.54609e87)]
    [InlineData(2.5, 1.1365225e88)]
    public void VolumeFromImperialGallonsShouldProduceExpectedCubicQuectoMeters(double value, double expected)
    {
        Volume<double> v = Volume<double>.FromImperialGallons(value);
        Assert.Equal(expected, v.CubicQuectoMeters, Tolerance);
    }

    [Theory(DisplayName = "Volume.FromImperialQuarts should produce the expected CubicQuectoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1.1365225e87)]
    [InlineData(4.0, 4.54609e87)] // 4 imp qt = 1 imp gal
    public void VolumeFromImperialQuartsShouldProduceExpectedCubicQuectoMeters(double value, double expected)
    {
        Volume<double> v = Volume<double>.FromImperialQuarts(value);
        Assert.Equal(expected, v.CubicQuectoMeters, Tolerance);
    }

    [Theory(DisplayName = "Volume.FromImperialPints should produce the expected CubicQuectoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 5.6826125e86)]
    [InlineData(2.0, 1.1365225e87)] // 2 imp pt = 1 imp qt
    public void VolumeFromImperialPintsShouldProduceExpectedCubicQuectoMeters(double value, double expected)
    {
        Volume<double> v = Volume<double>.FromImperialPints(value);
        Assert.Equal(expected, v.CubicQuectoMeters, Tolerance);
    }

    [Theory(DisplayName = "Volume.FromImperialFluidOunces should produce the expected CubicQuectoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 2.84130625e85)]
    [InlineData(20.0, 5.6826125e86)] // 20 imp fl oz = 1 imp pt
    public void VolumeFromImperialFluidOuncesShouldProduceExpectedCubicQuectoMeters(double value, double expected)
    {
        Volume<double> v = Volume<double>.FromImperialFluidOunces(value);
        Assert.Equal(expected, v.CubicQuectoMeters, Tolerance);
    }

    [Theory(DisplayName = "Volume.FromOilBarrels should produce the expected CubicQuectoMeters")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1.58987294928e89)]
    [InlineData(2.5, 3.9746823732e89)]
    public void VolumeFromOilBarrelsShouldProduceExpectedCubicQuectoMeters(double value, double expected)
    {
        Volume<double> v = Volume<double>.FromOilBarrels(value);
        Assert.Equal(expected, v.CubicQuectoMeters, Tolerance);
    }

    [Fact(DisplayName = "Volume.Add should produce the expected result")]
    public void VolumeAddShouldProduceExpectedValue()
    {
        // Given
        Volume<double> left = Volume<double>.FromLiters(1.5);
        Volume<double> right = Volume<double>.FromLiters(0.5);

        // When
        Volume<double> result = left.Add(right);

        // Then
        Assert.Equal(2.0, result.Liters, Tolerance);
    }

    [Fact(DisplayName = "Volume.Subtract should produce the expected result")]
    public void VolumeSubtractShouldProduceExpectedValue()
    {
        // Given
        Volume<double> left = Volume<double>.FromLiters(1.5);
        Volume<double> right = Volume<double>.FromLiters(0.4);

        // When
        Volume<double> result = left.Subtract(right);

        // Then
        Assert.Equal(1.1, result.Liters, Tolerance);
    }

    [Fact(DisplayName = "Volume.Multiply should produce the expected result")]
    public void VolumeMultiplyShouldProduceExpectedValue()
    {
        // Given
        Volume<double> left = Volume<double>.FromCubicMeters(2.0);  // 2e90 qm³
        Volume<double> right = Volume<double>.FromCubicMeters(3.0); // 3e90 qm³

        // When
        Volume<double> result = left.Multiply(right);  // 2e90 * 3e90 = 6e180 qm³

        // Then
        Assert.Equal(2e90, left.CubicQuectoMeters, Tolerance);
        Assert.Equal(3e90, right.CubicQuectoMeters, Tolerance);
        Assert.Equal(6e180, result.CubicQuectoMeters, Tolerance);
    }

    [Fact(DisplayName = "Volume.Divide should produce the expected result")]
    public void VolumeDivideShouldProduceExpectedValue()
    {
        // Given
        Volume<double> left = Volume<double>.FromCubicMeters(10.0); // 1e91 qm³
        Volume<double> right = Volume<double>.FromCubicMeters(2.0); //  2e90 qm³

        // When
        Volume<double> result = left.Divide(right); // 1e91 / 2e90 = 5 qm³

        // Then
        Assert.Equal(5.0, result.CubicQuectoMeters, Tolerance);
    }

    [Fact(DisplayName = "Volume comparison should produce the expected result (left equal to right)")]
    public void VolumeComparisonShouldProduceExpectedResultLeftEqualToRight()
    {
        // Given
        Volume<double> left = Volume<double>.FromLiters(123.0);
        Volume<double> right = Volume<double>.FromLiters(123.0);

        // When / Then
        Assert.Equal(0, Volume<double>.Compare(left, right));
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
        Volume<double> left = Volume<double>.FromLiters(456.0);
        Volume<double> right = Volume<double>.FromLiters(123.0);

        // When / Then
        Assert.Equal(1, Volume<double>.Compare(left, right));
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
        Volume<double> left = Volume<double>.FromLiters(123.0);
        Volume<double> right = Volume<double>.FromLiters(456.0);

        // When / Then
        Assert.Equal(-1, Volume<double>.Compare(left, right));
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
        // Given
        Volume<BigDecimal> left = Volume<BigDecimal>.FromLiters(2.0);
        Volume<BigDecimal> right = Volume<BigDecimal>.FromMilliliters(2000.0);

        // When / Then
        Assert.True(Volume<BigDecimal>.Equals(left, right));
        Assert.True(left.Equals(right));
        Assert.True(left.Equals((object)right));
        Assert.True(left == right);
        Assert.False(left != right);
    }

    [Fact(DisplayName = "Volume equality should produce the expected result (left not equal to right)")]
    public void VolumeEqualityShouldProduceExpectedResultLeftNotEqualToRight()
    {
        // Given
        Volume<double> left = Volume<double>.FromLiters(2.0);
        Volume<double> right = Volume<double>.FromMilliliters(2500.0);

        // When / Then
        Assert.False(Volume<double>.Equals(left, right));
        Assert.False(left.Equals(right));
        Assert.False(left.Equals((object)right));
        Assert.False(left == right);
        Assert.True(left != right);
    }

    [Fact(DisplayName = "Volume.ToString should produce the expected result")]
    public void VolumeToStringShouldProduceExpectedResult()
    {
        // Given
        Volume<double> v = Volume<double>.FromCubicMeters(1.0);

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
        Volume<double> v = Volume<double>.FromLiters(1234.56);

        // When
        string formatted = v.ToString("L2", customCulture);

        // Then
        Assert.Equal("1.234,56 L", formatted);
    }
}
