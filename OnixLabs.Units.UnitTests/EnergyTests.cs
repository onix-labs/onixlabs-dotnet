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

public sealed class EnergyTests
{
    // IEEE-754 binary floating-point arithmetic causes small discrepancies in calculation, therefore we need a tolerance.
    private const double Tolerance = 1e+42;

    [Fact(DisplayName = "Energy.Zero should produce the expected result")]
    public void EnergyZeroShouldProduceExpectedResult()
    {
        // Given / When
        Energy<double> energy = Energy<double>.Zero;

        // Then
        Assert.Equal(0.0, energy.QuectoJoules, Tolerance);
    }

    [Theory(DisplayName = "Energy.FromQuectojoules should produce the expected QuectoJoules")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1.0)]
    [InlineData(2.5, 2.5)]
    public void EnergyFromQuectojoulesShouldProduceExpectedQuectoJoules(double value, double expected)
    {
        Energy<double> e = Energy<double>.FromQuectojoules(value);
        Assert.Equal(expected, e.QuectoJoules, Tolerance);
    }

    [Theory(DisplayName = "Energy.FromRontojoules should produce the expected QuectoJoules")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e3)]
    [InlineData(2.5, 2.5e3)]
    public void EnergyFromRontojoulesShouldProduceExpectedQuectoJoules(double value, double expected)
    {
        Energy<double> e = Energy<double>.FromRontojoules(value);
        Assert.Equal(expected, e.QuectoJoules, Tolerance);
    }

    [Theory(DisplayName = "Energy.FromYoctojoules should produce the expected QuectoJoules")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e6)]
    [InlineData(2.5, 2.5e6)]
    public void EnergyFromYoctojoulesShouldProduceExpectedQuectoJoules(double value, double expected)
    {
        Energy<double> e = Energy<double>.FromYoctojoules(value);
        Assert.Equal(expected, e.QuectoJoules, Tolerance);
    }

    [Theory(DisplayName = "Energy.FromZeptojoules should produce the expected QuectoJoules")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e9)]
    [InlineData(2.5, 2.5e9)]
    public void EnergyFromZeptojoulesShouldProduceExpectedQuectoJoules(double value, double expected)
    {
        Energy<double> e = Energy<double>.FromZeptojoules(value);
        Assert.Equal(expected, e.QuectoJoules, Tolerance);
    }

    [Theory(DisplayName = "Energy.FromAttojoules should produce the expected QuectoJoules")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e12)]
    [InlineData(2.5, 2.5e12)]
    public void EnergyFromAttojoulesShouldProduceExpectedQuectoJoules(double value, double expected)
    {
        Energy<double> e = Energy<double>.FromAttojoules(value);
        Assert.Equal(expected, e.QuectoJoules, Tolerance);
    }

    [Theory(DisplayName = "Energy.FromFemtojoules should produce the expected QuectoJoules")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e15)]
    [InlineData(2.5, 2.5e15)]
    public void EnergyFromFemtojoulesShouldProduceExpectedQuectoJoules(double value, double expected)
    {
        Energy<double> e = Energy<double>.FromFemtojoules(value);
        Assert.Equal(expected, e.QuectoJoules, Tolerance);
    }

    [Theory(DisplayName = "Energy.FromPicojoules should produce the expected QuectoJoules")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e18)]
    [InlineData(2.5, 2.5e18)]
    public void EnergyFromPicojoulesShouldProduceExpectedQuectoJoules(double value, double expected)
    {
        Energy<double> e = Energy<double>.FromPicojoules(value);
        Assert.Equal(expected, e.QuectoJoules, Tolerance);
    }

    [Theory(DisplayName = "Energy.FromNanojoules should produce the expected QuectoJoules")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e21)]
    [InlineData(2.5, 2.5e21)]
    public void EnergyFromNanojoulesShouldProduceExpectedQuectoJoules(double value, double expected)
    {
        Energy<double> e = Energy<double>.FromNanojoules(value);
        Assert.Equal(expected, e.QuectoJoules, Tolerance);
    }

    [Theory(DisplayName = "Energy.FromMicrojoules should produce the expected QuectoJoules")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e24)]
    [InlineData(2.5, 2.5e24)]
    public void EnergyFromMicrojoulesShouldProduceExpectedQuectoJoules(double value, double expected)
    {
        Energy<double> e = Energy<double>.FromMicrojoules(value);
        Assert.Equal(expected, e.QuectoJoules, Tolerance);
    }

    [Theory(DisplayName = "Energy.FromMillijoules should produce the expected QuectoJoules")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e27)]
    [InlineData(2.5, 2.5e27)]
    public void EnergyFromMillijoulesShouldProduceExpectedQuectoJoules(double value, double expected)
    {
        Energy<double> e = Energy<double>.FromMillijoules(value);
        Assert.Equal(expected, e.QuectoJoules, Tolerance);
    }

    [Theory(DisplayName = "Energy.FromCentijoules should produce the expected QuectoJoules")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e28)]
    [InlineData(2.5, 2.5e28)]
    public void EnergyFromCentijoulesShouldProduceExpectedQuectoJoules(double value, double expected)
    {
        Energy<double> e = Energy<double>.FromCentijoules(value);
        Assert.Equal(expected, e.QuectoJoules, Tolerance);
    }

    [Theory(DisplayName = "Energy.FromDecijoules should produce the expected QuectoJoules")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e29)]
    [InlineData(2.5, 2.5e29)]
    public void EnergyFromDecijoulesShouldProduceExpectedQuectoJoules(double value, double expected)
    {
        Energy<double> e = Energy<double>.FromDecijoules(value);
        Assert.Equal(expected, e.QuectoJoules, Tolerance);
    }

    [Theory(DisplayName = "Energy.FromJoules should produce the expected QuectoJoules")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e30)]
    [InlineData(2.5, 2.5e30)]
    public void EnergyFromJoulesShouldProduceExpectedQuectoJoules(double value, double expected)
    {
        Energy<double> e = Energy<double>.FromJoules(value);
        Assert.Equal(expected, e.QuectoJoules, Tolerance);
    }

    [Theory(DisplayName = "Energy.FromDecajoules should produce the expected QuectoJoules")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e31)]
    [InlineData(2.5, 2.5e31)]
    public void EnergyFromDecajoulesShouldProduceExpectedQuectoJoules(double value, double expected)
    {
        Energy<double> e = Energy<double>.FromDecajoules(value);
        Assert.Equal(expected, e.QuectoJoules, Tolerance);
    }

    [Theory(DisplayName = "Energy.FromHectojoules should produce the expected QuectoJoules")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e32)]
    [InlineData(2.5, 2.5e32)]
    public void EnergyFromHectojoulesShouldProduceExpectedQuectoJoules(double value, double expected)
    {
        Energy<double> e = Energy<double>.FromHectojoules(value);
        Assert.Equal(expected, e.QuectoJoules, Tolerance);
    }

    [Theory(DisplayName = "Energy.FromKilojoules should produce the expected QuectoJoules")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e33)]
    [InlineData(2.5, 2.5e33)]
    public void EnergyFromKilojoulesShouldProduceExpectedQuectoJoules(double value, double expected)
    {
        Energy<double> e = Energy<double>.FromKilojoules(value);
        Assert.Equal(expected, e.QuectoJoules, Tolerance);
    }

    [Theory(DisplayName = "Energy.FromMegajoules should produce the expected QuectoJoules")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e36)]
    [InlineData(2.5, 2.5e36)]
    public void EnergyFromMegajoulesShouldProduceExpectedQuectoJoules(double value, double expected)
    {
        Energy<double> e = Energy<double>.FromMegajoules(value);
        Assert.Equal(expected, e.QuectoJoules, Tolerance);
    }

    [Theory(DisplayName = "Energy.FromGigajoules should produce the expected QuectoJoules")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e39)]
    [InlineData(2.5, 2.5e39)]
    public void EnergyFromGigajoulesShouldProduceExpectedQuectoJoules(double value, double expected)
    {
        Energy<double> e = Energy<double>.FromGigajoules(value);
        Assert.Equal(expected, e.QuectoJoules, Tolerance);
    }

    [Theory(DisplayName = "Energy.FromTerajoules should produce the expected QuectoJoules")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e42)]
    [InlineData(2.5, 2.5e42)]
    public void EnergyFromTerajoulesShouldProduceExpectedQuectoJoules(double value, double expected)
    {
        Energy<double> e = Energy<double>.FromTerajoules(value);
        Assert.Equal(expected, e.QuectoJoules, Tolerance);
    }

    [Theory(DisplayName = "Energy.FromPetajoules should produce the expected QuectoJoules")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e45)]
    [InlineData(2.5, 2.5e45)]
    public void EnergyFromPetajoulesShouldProduceExpectedQuectoJoules(double value, double expected)
    {
        Energy<double> e = Energy<double>.FromPetajoules(value);
        Assert.Equal(expected, e.QuectoJoules, Tolerance);
    }

    [Theory(DisplayName = "Energy.FromExajoules should produce the expected QuectoJoules")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e48)]
    [InlineData(2.5, 2.5e48)]
    public void EnergyFromExajoulesShouldProduceExpectedQuectoJoules(double value, double expected)
    {
        Energy<double> e = Energy<double>.FromExajoules(value);
        Assert.Equal(expected, e.QuectoJoules, Tolerance);
    }

    [Theory(DisplayName = "Energy.FromZettajoules should produce the expected QuectoJoules")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e51)]
    [InlineData(2.5, 2.5e51)]
    public void EnergyFromZettajoulesShouldProduceExpectedQuectoJoules(double value, double expected)
    {
        Energy<double> e = Energy<double>.FromZettajoules(value);
        Assert.Equal(expected, e.QuectoJoules, Tolerance);
    }

    [Theory(DisplayName = "Energy.FromYottajoules should produce the expected QuectoJoules")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e54)]
    [InlineData(2.5, 2.5e54)]
    public void EnergyFromYottajoulesShouldProduceExpectedQuectoJoules(double value, double expected)
    {
        Energy<double> e = Energy<double>.FromYottajoules(value);
        Assert.Equal(expected, e.QuectoJoules, Tolerance);
    }

    [Theory(DisplayName = "Energy.FromRonnajoules should produce the expected QuectoJoules")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e57)]
    [InlineData(2.5, 2.5e57)]
    public void EnergyFromRonnajoulesShouldProduceExpectedQuectoJoules(double value, double expected)
    {
        Energy<double> e = Energy<double>.FromRonnajoules(value);
        Assert.Equal(expected, e.QuectoJoules, Tolerance);
    }

    [Theory(DisplayName = "Energy.FromQuettajoules should produce the expected QuectoJoules")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e60)]
    [InlineData(2.5, 2.5e60)]
    public void EnergyFromQuettajoulesShouldProduceExpectedQuectoJoules(double value, double expected)
    {
        Energy<double> e = Energy<double>.FromQuettajoules(value);
        Assert.Equal(expected, e.QuectoJoules, Tolerance);
    }

    [Theory(DisplayName = "Energy.FromCalories should produce the expected QuectoJoules")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 4.184e30)]    // 1 cal = 4.184 J
    [InlineData(1000.0, 4.184e33)] // 1000 cal = 1 kcal
    public void EnergyFromCaloriesShouldProduceExpectedQuectoJoules(double value, double expected)
    {
        Energy<double> e = Energy<double>.FromCalories(value);
        Assert.Equal(expected, e.QuectoJoules, Tolerance);
    }

    [Theory(DisplayName = "Energy.FromKilocalories should produce the expected QuectoJoules")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 4.184e33)]
    [InlineData(2000.0, 8.368e36)]
    public void EnergyFromKilocaloriesShouldProduceExpectedQuectoJoules(double value, double expected)
    {
        Energy<double> e = Energy<double>.FromKilocalories(value);
        Assert.Equal(expected, e.QuectoJoules, Tolerance);
    }

    [Theory(DisplayName = "Energy.FromWattHours should produce the expected QuectoJoules")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 3.6e33)]      // 1 Wh = 3600 J
    [InlineData(1000.0, 3.6e36)]   // 1 kWh
    public void EnergyFromWattHoursShouldProduceExpectedQuectoJoules(double value, double expected)
    {
        Energy<double> e = Energy<double>.FromWattHours(value);
        Assert.Equal(expected, e.QuectoJoules, Tolerance);
    }

    [Theory(DisplayName = "Energy.FromKilowattHours should produce the expected QuectoJoules")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 3.6e36)]
    [InlineData(2.5, 9e36)]
    public void EnergyFromKilowattHoursShouldProduceExpectedQuectoJoules(double value, double expected)
    {
        Energy<double> e = Energy<double>.FromKilowattHours(value);
        Assert.Equal(expected, e.QuectoJoules, Tolerance);
    }

    [Theory(DisplayName = "Energy.FromMegawattHours should produce the expected QuectoJoules")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 3.6e39)]
    [InlineData(2.5, 9e39)]
    public void EnergyFromMegawattHoursShouldProduceExpectedQuectoJoules(double value, double expected)
    {
        Energy<double> e = Energy<double>.FromMegawattHours(value);
        Assert.Equal(expected, e.QuectoJoules, Tolerance);
    }

    [Theory(DisplayName = "Energy.FromGigawattHours should produce the expected QuectoJoules")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 3.6e42)]
    [InlineData(2.5, 9e42)]
    public void EnergyFromGigawattHoursShouldProduceExpectedQuectoJoules(double value, double expected)
    {
        Energy<double> e = Energy<double>.FromGigawattHours(value);
        Assert.Equal(expected, e.QuectoJoules, Tolerance);
    }

    [Theory(DisplayName = "Energy.FromTerawattHours should produce the expected QuectoJoules")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 3.6e45)]
    [InlineData(2.5, 9e45)]
    public void EnergyFromTerawattHoursShouldProduceExpectedQuectoJoules(double value, double expected)
    {
        Energy<double> e = Energy<double>.FromTerawattHours(value);
        Assert.Equal(expected, e.QuectoJoules, Tolerance);
    }

    [Theory(DisplayName = "Energy.FromBritishThermalUnits should produce the expected QuectoJoules")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1.05505585262e33)]
    [InlineData(100000.0, 1.05505585262e38)] // 100,000 BTU = 1 therm
    public void EnergyFromBritishThermalUnitsShouldProduceExpectedQuectoJoules(double value, double expected)
    {
        Energy<double> e = Energy<double>.FromBritishThermalUnits(value);
        Assert.Equal(expected, e.QuectoJoules, Tolerance);
    }

    [Theory(DisplayName = "Energy.FromTherms should produce the expected QuectoJoules")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1.05505585262e38)]
    [InlineData(2.5, 2.6376396316e38)]
    public void EnergyFromThermsShouldProduceExpectedQuectoJoules(double value, double expected)
    {
        Energy<double> e = Energy<double>.FromTherms(value);
        Assert.Equal(expected, e.QuectoJoules, Tolerance);
    }

    [Theory(DisplayName = "Energy.FromErgs should produce the expected QuectoJoules")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1e23)]       // 1 erg = 1e-7 J
    [InlineData(1e7, 1e30)]       // 1e7 erg = 1 J
    public void EnergyFromErgsShouldProduceExpectedQuectoJoules(double value, double expected)
    {
        Energy<double> e = Energy<double>.FromErgs(value);
        Assert.Equal(expected, e.QuectoJoules, Tolerance);
    }

    [Theory(DisplayName = "Energy.FromElectronVolts should produce the expected QuectoJoules")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1.602176634e11)]
    [InlineData(1000.0, 1.602176634e14)] // 1 keV
    public void EnergyFromElectronVoltsShouldProduceExpectedQuectoJoules(double value, double expected)
    {
        Energy<double> e = Energy<double>.FromElectronVolts(value);
        Assert.Equal(expected, e.QuectoJoules, Tolerance);
    }

    [Theory(DisplayName = "Energy.FromKiloElectronVolts should produce the expected QuectoJoules")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1.602176634e14)]
    [InlineData(1000.0, 1.602176634e17)] // 1 MeV
    public void EnergyFromKiloElectronVoltsShouldProduceExpectedQuectoJoules(double value, double expected)
    {
        Energy<double> e = Energy<double>.FromKiloElectronVolts(value);
        Assert.Equal(expected, e.QuectoJoules, Tolerance);
    }

    [Theory(DisplayName = "Energy.FromMegaElectronVolts should produce the expected QuectoJoules")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1.602176634e17)]
    [InlineData(1000.0, 1.602176634e20)] // 1 GeV
    public void EnergyFromMegaElectronVoltsShouldProduceExpectedQuectoJoules(double value, double expected)
    {
        Energy<double> e = Energy<double>.FromMegaElectronVolts(value);
        Assert.Equal(expected, e.QuectoJoules, Tolerance);
    }

    [Theory(DisplayName = "Energy.FromGigaElectronVolts should produce the expected QuectoJoules")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1.602176634e20)]
    [InlineData(1000.0, 1.602176634e23)] // 1 TeV
    public void EnergyFromGigaElectronVoltsShouldProduceExpectedQuectoJoules(double value, double expected)
    {
        Energy<double> e = Energy<double>.FromGigaElectronVolts(value);
        Assert.Equal(expected, e.QuectoJoules, Tolerance);
    }

    [Theory(DisplayName = "Energy.FromTeraElectronVolts should produce the expected QuectoJoules")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1.602176634e23)]
    [InlineData(2.5, 4.005441585e23)]
    public void EnergyFromTeraElectronVoltsShouldProduceExpectedQuectoJoules(double value, double expected)
    {
        Energy<double> e = Energy<double>.FromTeraElectronVolts(value);
        Assert.Equal(expected, e.QuectoJoules, Tolerance);
    }

    [Theory(DisplayName = "Energy.FromFootPounds should produce the expected QuectoJoules")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1.3558179483314004e30)]
    [InlineData(2.5, 3.389544870828501e30)]
    public void EnergyFromFootPoundsShouldProduceExpectedQuectoJoules(double value, double expected)
    {
        Energy<double> e = Energy<double>.FromFootPounds(value);
        Assert.Equal(expected, e.QuectoJoules, Tolerance);
    }

    [Theory(DisplayName = "Energy.FromTonsOfTNT should produce the expected QuectoJoules")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 4.184e39)]    // 1 ton TNT = 4.184e9 J
    [InlineData(1000.0, 4.184e42)] // 1 kt TNT
    public void EnergyFromTonsOfTNTShouldProduceExpectedQuectoJoules(double value, double expected)
    {
        Energy<double> e = Energy<double>.FromTonsOfTnt(value);
        Assert.Equal(expected, e.QuectoJoules, Tolerance);
    }

    [Theory(DisplayName = "Energy.FromKilotonsOfTNT should produce the expected QuectoJoules")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 4.184e42)]
    [InlineData(15.0, 6.276e43)] // ≈ Hiroshima yield
    public void EnergyFromKilotonsOfTNTShouldProduceExpectedQuectoJoules(double value, double expected)
    {
        Energy<double> e = Energy<double>.FromKilotonsOfTnt(value);
        Assert.Equal(expected, e.QuectoJoules, Tolerance);
    }

    [Theory(DisplayName = "Energy.FromMegatonsOfTNT should produce the expected QuectoJoules")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 4.184e45)]
    [InlineData(50.0, 2.092e47)] // ≈ Tsar Bomba yield
    public void EnergyFromMegatonsOfTNTShouldProduceExpectedQuectoJoules(double value, double expected)
    {
        Energy<double> e = Energy<double>.FromMegatonsOfTnt(value);
        Assert.Equal(expected, e.QuectoJoules, Tolerance);
    }

    [Fact(DisplayName = "Energy.Add should produce the expected result")]
    public void EnergyAddShouldProduceExpectedValue()
    {
        Energy<double> left = Energy<double>.FromKilojoules(1.5);
        Energy<double> right = Energy<double>.FromKilojoules(0.5);
        Energy<double> result = left.Add(right);
        Assert.Equal(2.0, result.KiloJoules, Tolerance);
    }

    [Fact(DisplayName = "Energy.Subtract should produce the expected result")]
    public void EnergySubtractShouldProduceExpectedValue()
    {
        Energy<double> left = Energy<double>.FromKilojoules(1.5);
        Energy<double> right = Energy<double>.FromKilojoules(0.4);
        Energy<double> result = left.Subtract(right);
        Assert.Equal(1.1, result.KiloJoules, Tolerance);
    }

    [Fact(DisplayName = "Energy.Multiply should produce the expected result")]
    public void EnergyMultiplyShouldProduceExpectedValue()
    {
        Energy<double> left = Energy<double>.FromJoules(10.0);
        Energy<double> right = Energy<double>.FromJoules(3.0);
        Energy<double> result = left.Multiply(right);
        Assert.Equal(1e31, left.QuectoJoules, Tolerance);
        Assert.Equal(3e30, right.QuectoJoules, Tolerance);
        Assert.Equal(3e61, result.QuectoJoules, Tolerance);
    }

    [Fact(DisplayName = "Energy.Divide should produce the expected result")]
    public void EnergyDivideShouldProduceExpectedValue()
    {
        Energy<double> left = Energy<double>.FromJoules(100.0);
        Energy<double> right = Energy<double>.FromJoules(20.0);
        Energy<double> result = left.Divide(right);
        Assert.Equal(5.0, result.QuectoJoules, Tolerance);
        Assert.Equal(5e-30, result.Joules, Tolerance);
    }

    [Fact(DisplayName = "Energy comparison should produce the expected result (left equal to right)")]
    public void EnergyComparisonShouldProduceExpectedResultLeftEqualToRight()
    {
        Energy<double> left = Energy<double>.FromKilojoules(123.0);
        Energy<double> right = Energy<double>.FromKilojoules(123.0);
        Assert.Equal(0, Energy<double>.Compare(left, right));
        Assert.Equal(0, left.CompareTo(right));
        Assert.Equal(0, left.CompareTo((object)right));
        Assert.False(left > right);
        Assert.True(left >= right);
        Assert.False(left < right);
        Assert.True(left <= right);
    }

    [Fact(DisplayName = "Energy comparison should produce the expected result (left greater than right)")]
    public void EnergyComparisonShouldProduceExpectedLeftGreaterThanRight()
    {
        Energy<double> left = Energy<double>.FromKilojoules(456.0);
        Energy<double> right = Energy<double>.FromKilojoules(123.0);
        Assert.Equal(1, Energy<double>.Compare(left, right));
        Assert.Equal(1, left.CompareTo(right));
        Assert.Equal(1, left.CompareTo((object)right));
        Assert.True(left > right);
        Assert.True(left >= right);
        Assert.False(left < right);
        Assert.False(left <= right);
    }

    [Fact(DisplayName = "Energy comparison should produce the expected result (left less than right)")]
    public void EnergyComparisonShouldProduceExpectedLeftLessThanRight()
    {
        Energy<double> left = Energy<double>.FromKilojoules(123.0);
        Energy<double> right = Energy<double>.FromKilojoules(456.0);
        Assert.Equal(-1, Energy<double>.Compare(left, right));
        Assert.Equal(-1, left.CompareTo(right));
        Assert.Equal(-1, left.CompareTo((object)right));
        Assert.False(left > right);
        Assert.False(left >= right);
        Assert.True(left < right);
        Assert.True(left <= right);
    }

    [Fact(DisplayName = "Energy equality should produce the expected result (left equal to right)")]
    public void EnergyEqualityShouldProduceExpectedResultLeftEqualToRight()
    {
        Energy<BigDecimal> left = Energy<BigDecimal>.FromKilojoules(2.0);
        Energy<BigDecimal> right = Energy<BigDecimal>.FromJoules(2000.0);
        Assert.True(Energy<BigDecimal>.Equals(left, right));
        Assert.True(left.Equals(right));
        Assert.True(left.Equals((object)right));
        Assert.True(left == right);
        Assert.False(left != right);
    }

    [Fact(DisplayName = "Energy equality should produce the expected result (left not equal to right)")]
    public void EnergyEqualityShouldProduceExpectedResultLeftNotEqualToRight()
    {
        Energy<double> left = Energy<double>.FromKilojoules(2.0);
        Energy<double> right = Energy<double>.FromJoules(2500.0);
        Assert.False(Energy<double>.Equals(left, right));
        Assert.False(left.Equals(right));
        Assert.False(left.Equals((object)right));
        Assert.False(left == right);
        Assert.True(left != right);
    }

    [Fact(DisplayName = "Energy.ToString should produce the expected result")]
    public void EnergyToStringShouldProduceExpectedResult()
    {
        // Given: 1 kWh
        Energy<double> e = Energy<double>.FromKilowattHours(1.0);

        // When / Then
        Assert.Equal("3,600,000.000 J", $"{e:J3}");
        Assert.Equal("3,600.000 kJ", $"{e:kJ3}");
        Assert.Equal("3.600 MJ", $"{e:MJ3}");
        Assert.Equal("1.000 kWh", $"{e:kWh3}");
        Assert.Equal("3,412.142 BTU", $"{e:BTU3}");
        Assert.Equal("860.421 kcal", $"{e:kcal3}");
    }

    [Fact(DisplayName = "Energy.ToString MJ vs mJ are case-sensitive")]
    public void EnergyToStringMjVsMjAreCaseSensitive()
    {
        Energy<double> e = Energy<double>.FromJoules(1.0);
        Assert.Equal("0.000001 MJ", $"{e:MJ6}"); // mega
        Assert.Equal("1,000.000 mJ", $"{e:mJ3}"); // milli
    }

    [Fact(DisplayName = "Energy.ToString should honor custom culture separators")]
    public void EnergyToStringShouldHonorCustomCulture()
    {
        CultureInfo customCulture = new("de-DE");
        Energy<double> e = Energy<double>.FromKilojoules(1234.56);
        string formatted = e.ToString("kJ2", customCulture);
        Assert.Equal("1.234,56 kJ", formatted);
    }
}
