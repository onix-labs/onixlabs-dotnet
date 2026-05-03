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

public readonly partial struct Power<T>
{
    /// <inheritdoc/>
    public override string ToString() => ToString(QuectoWattsSpecifier);

    /// <inheritdoc/>
    public string ToString(string? format, IFormatProvider? formatProvider) => ToString(format.AsSpan(), formatProvider);

    /// <inheritdoc/>
    public string ToString(ReadOnlySpan<char> format, IFormatProvider? formatProvider = null)
    {
        (string specifier, int scale) = format.GetSpecifierAndScale(defaultSpecifier: QuectoWattsSpecifier);

        (T value, string symbol) = specifier switch
        {
            QuectoWattsSpecifier => (QuectoWatts, QuectoWattsSymbol),
            RontoWattsSpecifier => (RontoWatts, RontoWattsSymbol),
            YoctoWattsSpecifier => (YoctoWatts, YoctoWattsSymbol),
            ZeptoWattsSpecifier => (ZeptoWatts, ZeptoWattsSymbol),
            AttoWattsSpecifier => (AttoWatts, AttoWattsSymbol),
            FemtoWattsSpecifier => (FemtoWatts, FemtoWattsSymbol),
            PicoWattsSpecifier => (PicoWatts, PicoWattsSymbol),
            NanoWattsSpecifier => (NanoWatts, NanoWattsSymbol),
            MicroWattsSpecifier => (MicroWatts, MicroWattsSymbol),
            MilliWattsSpecifier => (MilliWatts, MilliWattsSymbol),
            CentiWattsSpecifier => (CentiWatts, CentiWattsSymbol),
            DeciWattsSpecifier => (DeciWatts, DeciWattsSymbol),
            WattsSpecifier => (Watts, WattsSymbol),
            DecaWattsSpecifier => (DecaWatts, DecaWattsSymbol),
            HectoWattsSpecifier => (HectoWatts, HectoWattsSymbol),
            KiloWattsSpecifier => (KiloWatts, KiloWattsSymbol),
            MegaWattsSpecifier => (MegaWatts, MegaWattsSymbol),
            GigaWattsSpecifier => (GigaWatts, GigaWattsSymbol),
            TeraWattsSpecifier => (TeraWatts, TeraWattsSymbol),
            PetaWattsSpecifier => (PetaWatts, PetaWattsSymbol),
            ExaWattsSpecifier => (ExaWatts, ExaWattsSymbol),
            ZettaWattsSpecifier => (ZettaWatts, ZettaWattsSymbol),
            YottaWattsSpecifier => (YottaWatts, YottaWattsSymbol),
            RonnaWattsSpecifier => (RonnaWatts, RonnaWattsSymbol),
            QuettaWattsSpecifier => (QuettaWatts, QuettaWattsSymbol),
            MechanicalHorsepowerSpecifier => (MechanicalHorsepower, MechanicalHorsepowerSymbol),
            MetricHorsepowerSpecifier => (MetricHorsepower, MetricHorsepowerSymbol),
            BtusPerHourSpecifier => (BtusPerHour, BtusPerHourSymbol),
            CaloriesPerSecondSpecifier => (CaloriesPerSecond, CaloriesPerSecondSymbol),
            ErgsPerSecondSpecifier => (ErgsPerSecond, ErgsPerSecondSymbol),
            FootPoundsPerSecondSpecifier => (FootPoundsPerSecond, FootPoundsPerSecondSymbol),
            TonsOfRefrigerationSpecifier => (TonsOfRefrigeration, TonsOfRefrigerationSymbol),
            _ => throw ArgumentException.InvalidFormat(format,
                "qW, rW, yW, zW, aW, fW, pW, nW, uW, mW, cW, dW, " +
                "W, daW, hW, kW, MW, GW, TW, PW, EW, ZW, YW, RW, " +
                "QW, hp, PS, BTUph, calps, ergps, ftlbfps, and tref")
        };

        T rounded = scale > 0 ? T.Round(value, scale) : value;

        return $"{rounded.ToString($"N{scale}", formatProvider ?? CultureInfo.CurrentCulture)} {symbol}";
    }
}
