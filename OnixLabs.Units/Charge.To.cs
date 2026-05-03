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

public readonly partial struct Charge<T>
{
    /// <inheritdoc/>
    public override string ToString() => ToString(QuectoCoulombsSpecifier);

    /// <inheritdoc/>
    public string ToString(string? format, IFormatProvider? formatProvider) => ToString(format.AsSpan(), formatProvider);

    /// <inheritdoc/>
    public string ToString(ReadOnlySpan<char> format, IFormatProvider? formatProvider = null)
    {
        (string specifier, int scale) = format.GetSpecifierAndScale(defaultSpecifier: QuectoCoulombsSpecifier);

        (T value, string symbol) = specifier switch
        {
            QuectoCoulombsSpecifier => (QuectoCoulombs, QuectoCoulombsSymbol),
            RontoCoulombsSpecifier => (RontoCoulombs, RontoCoulombsSymbol),
            YoctoCoulombsSpecifier => (YoctoCoulombs, YoctoCoulombsSymbol),
            ZeptoCoulombsSpecifier => (ZeptoCoulombs, ZeptoCoulombsSymbol),
            AttoCoulombsSpecifier => (AttoCoulombs, AttoCoulombsSymbol),
            FemtoCoulombsSpecifier => (FemtoCoulombs, FemtoCoulombsSymbol),
            PicoCoulombsSpecifier => (PicoCoulombs, PicoCoulombsSymbol),
            NanoCoulombsSpecifier => (NanoCoulombs, NanoCoulombsSymbol),
            MicroCoulombsSpecifier => (MicroCoulombs, MicroCoulombsSymbol),
            MilliCoulombsSpecifier => (MilliCoulombs, MilliCoulombsSymbol),
            CentiCoulombsSpecifier => (CentiCoulombs, CentiCoulombsSymbol),
            DeciCoulombsSpecifier => (DeciCoulombs, DeciCoulombsSymbol),
            CoulombsSpecifier => (Coulombs, CoulombsSymbol),
            DecaCoulombsSpecifier => (DecaCoulombs, DecaCoulombsSymbol),
            HectoCoulombsSpecifier => (HectoCoulombs, HectoCoulombsSymbol),
            KiloCoulombsSpecifier => (KiloCoulombs, KiloCoulombsSymbol),
            MegaCoulombsSpecifier => (MegaCoulombs, MegaCoulombsSymbol),
            GigaCoulombsSpecifier => (GigaCoulombs, GigaCoulombsSymbol),
            TeraCoulombsSpecifier => (TeraCoulombs, TeraCoulombsSymbol),
            PetaCoulombsSpecifier => (PetaCoulombs, PetaCoulombsSymbol),
            ExaCoulombsSpecifier => (ExaCoulombs, ExaCoulombsSymbol),
            ZettaCoulombsSpecifier => (ZettaCoulombs, ZettaCoulombsSymbol),
            YottaCoulombsSpecifier => (YottaCoulombs, YottaCoulombsSymbol),
            RonnaCoulombsSpecifier => (RonnaCoulombs, RonnaCoulombsSymbol),
            QuettaCoulombsSpecifier => (QuettaCoulombs, QuettaCoulombsSymbol),
            AmpereHoursSpecifier => (AmpereHours, AmpereHoursSymbol),
            MilliampereHoursSpecifier => (MilliampereHours, MilliampereHoursSymbol),
            ElementaryChargesSpecifier => (ElementaryCharges, ElementaryChargesSymbol),
            _ => throw ArgumentException.InvalidFormat(format,
                "qC, rC, yC, zC, aC, fC, pC, nC, uC, mC, cC, dC, " +
                "C, daC, hC, kC, MC, GC, TC, PC, EC, ZC, YC, RC, " +
                "QC, Ah, mAh, and e")
        };

        T rounded = scale > 0 ? T.Round(value, scale) : value;

        return $"{rounded.ToString($"N{scale}", formatProvider ?? CultureInfo.CurrentCulture)} {symbol}";
    }
}
