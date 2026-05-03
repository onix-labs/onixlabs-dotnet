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

namespace OnixLabs.Units;

public readonly partial struct Energy<T>
{
    /// <inheritdoc/>
    public override string ToString() => ToString(QuectoJoulesSpecifier);

    /// <inheritdoc/>
    public string ToString(string? format, IFormatProvider? formatProvider) => ToString(format.AsSpan(), formatProvider);

    /// <inheritdoc/>
    public string ToString(ReadOnlySpan<char> format, IFormatProvider? formatProvider = null)
    {
        (string specifier, int scale) = format.GetSpecifierAndScale(defaultSpecifier: QuectoJoulesSpecifier);

        T value = specifier switch
        {
            QuectoJoulesSpecifier => QuectoJoules,
            RontoJoulesSpecifier => RontoJoules,
            YoctoJoulesSpecifier => YoctoJoules,
            ZeptoJoulesSpecifier => ZeptoJoules,
            AttoJoulesSpecifier => AttoJoules,
            FemtoJoulesSpecifier => FemtoJoules,
            PicoJoulesSpecifier => PicoJoules,
            NanoJoulesSpecifier => NanoJoules,
            MicroJoulesSpecifier => MicroJoules,
            MilliJoulesSpecifier => MilliJoules,
            CentiJoulesSpecifier => CentiJoules,
            DeciJoulesSpecifier => DeciJoules,
            JoulesSpecifier => Joules,
            DecaJoulesSpecifier => DecaJoules,
            HectoJoulesSpecifier => HectoJoules,
            KiloJoulesSpecifier => KiloJoules,
            MegaJoulesSpecifier => MegaJoules,
            GigaJoulesSpecifier => GigaJoules,
            TeraJoulesSpecifier => TeraJoules,
            PetaJoulesSpecifier => PetaJoules,
            ExaJoulesSpecifier => ExaJoules,
            ZettaJoulesSpecifier => ZettaJoules,
            YottaJoulesSpecifier => YottaJoules,
            RonnaJoulesSpecifier => RonnaJoules,
            QuettaJoulesSpecifier => QuettaJoules,
            CaloriesSpecifier => Calories,
            KilocaloriesSpecifier => Kilocalories,
            WattHoursSpecifier => WattHours,
            KilowattHoursSpecifier => KilowattHours,
            MegawattHoursSpecifier => MegawattHours,
            GigawattHoursSpecifier => GigawattHours,
            TerawattHoursSpecifier => TerawattHours,
            BritishThermalUnitsSpecifier => BritishThermalUnits,
            ThermsSpecifier => Therms,
            ErgsSpecifier => Ergs,
            ElectronVoltsSpecifier => ElectronVolts,
            KiloElectronVoltsSpecifier => KiloElectronVolts,
            MegaElectronVoltsSpecifier => MegaElectronVolts,
            GigaElectronVoltsSpecifier => GigaElectronVolts,
            TeraElectronVoltsSpecifier => TeraElectronVolts,
            FootPoundsSpecifier => FootPounds,
            TonsOfTntSpecifier => TonsOfTnt,
            KilotonsOfTntSpecifier => KilotonsOfTnt,
            MegatonsOfTntSpecifier => MegatonsOfTnt,
            _ => throw ArgumentException.InvalidFormat(format,
                "qJ, rJ, yJ, zJ, aJ, fJ, pJ, nJ, uJ, mJ, cJ, dJ, " +
                "J, daJ, hJ, kJ, MJ, GJ, TJ, PJ, EJ, ZJ, YJ, RJ, " +
                "QJ, cal, kcal, Wh, kWh, MWh, GWh, TWh, BTU, therm, " +
                "erg, eV, keV, MeV, GeV, TeV, ftlbf, tTNT, ktTNT, and MtTNT")
        };

        T rounded = scale > 0 ? T.Round(value, scale) : value;

        return $"{rounded.ToString($"N{scale}", formatProvider ?? CultureInfo.CurrentCulture)} {specifier}";
    }
}
