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

namespace OnixLabs.Units;

public readonly partial struct Energy<T>
{
    /// <inheritdoc/>
    public static Energy<T> Zero => new(T.Zero);

    private const string QuectoJoulesSpecifier = "qJ";
    private const string RontoJoulesSpecifier = "rJ";
    private const string YoctoJoulesSpecifier = "yJ";
    private const string ZeptoJoulesSpecifier = "zJ";
    private const string AttoJoulesSpecifier = "aJ";
    private const string FemtoJoulesSpecifier = "fJ";
    private const string PicoJoulesSpecifier = "pJ";
    private const string NanoJoulesSpecifier = "nJ";
    private const string MicroJoulesSpecifier = "uJ";
    private const string MilliJoulesSpecifier = "mJ";
    private const string CentiJoulesSpecifier = "cJ";
    private const string DeciJoulesSpecifier = "dJ";
    private const string JoulesSpecifier = "J";
    private const string DecaJoulesSpecifier = "daJ";
    private const string HectoJoulesSpecifier = "hJ";
    private const string KiloJoulesSpecifier = "kJ";
    private const string MegaJoulesSpecifier = "MJ";
    private const string GigaJoulesSpecifier = "GJ";
    private const string TeraJoulesSpecifier = "TJ";
    private const string PetaJoulesSpecifier = "PJ";
    private const string ExaJoulesSpecifier = "EJ";
    private const string ZettaJoulesSpecifier = "ZJ";
    private const string YottaJoulesSpecifier = "YJ";
    private const string RonnaJoulesSpecifier = "RJ";
    private const string QuettaJoulesSpecifier = "QJ";
    private const string CaloriesSpecifier = "cal";
    private const string KilocaloriesSpecifier = "kcal";
    private const string WattHoursSpecifier = "Wh";
    private const string KilowattHoursSpecifier = "kWh";
    private const string MegawattHoursSpecifier = "MWh";
    private const string GigawattHoursSpecifier = "GWh";
    private const string TerawattHoursSpecifier = "TWh";
    private const string BritishThermalUnitsSpecifier = "BTU";
    private const string ThermsSpecifier = "therm";
    private const string ErgsSpecifier = "erg";
    private const string ElectronVoltsSpecifier = "eV";
    private const string KiloElectronVoltsSpecifier = "keV";
    private const string MegaElectronVoltsSpecifier = "MeV";
    private const string GigaElectronVoltsSpecifier = "GeV";
    private const string TeraElectronVoltsSpecifier = "TeV";
    private const string FootPoundsSpecifier = "ftlbf";
    private const string TonsOfTntSpecifier = "tTNT";
    private const string KilotonsOfTntSpecifier = "ktTNT";
    private const string MegatonsOfTntSpecifier = "MtTNT";
}
