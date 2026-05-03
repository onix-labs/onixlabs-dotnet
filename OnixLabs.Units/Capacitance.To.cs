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

public readonly partial struct Capacitance<T>
{
    /// <inheritdoc/>
    public override string ToString() => ToString(QuectoFaradsSpecifier);

    /// <inheritdoc/>
    public string ToString(string? format, IFormatProvider? formatProvider) => ToString(format.AsSpan(), formatProvider);

    /// <inheritdoc/>
    public string ToString(ReadOnlySpan<char> format, IFormatProvider? formatProvider = null)
    {
        (string specifier, int scale) = format.GetSpecifierAndScale(defaultSpecifier: QuectoFaradsSpecifier);

        (T value, string symbol) = specifier switch
        {
            QuectoFaradsSpecifier => (QuectoFarads, QuectoFaradsSymbol),
            RontoFaradsSpecifier => (RontoFarads, RontoFaradsSymbol),
            YoctoFaradsSpecifier => (YoctoFarads, YoctoFaradsSymbol),
            ZeptoFaradsSpecifier => (ZeptoFarads, ZeptoFaradsSymbol),
            AttoFaradsSpecifier => (AttoFarads, AttoFaradsSymbol),
            FemtoFaradsSpecifier => (FemtoFarads, FemtoFaradsSymbol),
            PicoFaradsSpecifier => (PicoFarads, PicoFaradsSymbol),
            NanoFaradsSpecifier => (NanoFarads, NanoFaradsSymbol),
            MicroFaradsSpecifier => (MicroFarads, MicroFaradsSymbol),
            MilliFaradsSpecifier => (MilliFarads, MilliFaradsSymbol),
            CentiFaradsSpecifier => (CentiFarads, CentiFaradsSymbol),
            DeciFaradsSpecifier => (DeciFarads, DeciFaradsSymbol),
            FaradsSpecifier => (Farads, FaradsSymbol),
            DecaFaradsSpecifier => (DecaFarads, DecaFaradsSymbol),
            HectoFaradsSpecifier => (HectoFarads, HectoFaradsSymbol),
            KiloFaradsSpecifier => (KiloFarads, KiloFaradsSymbol),
            MegaFaradsSpecifier => (MegaFarads, MegaFaradsSymbol),
            GigaFaradsSpecifier => (GigaFarads, GigaFaradsSymbol),
            TeraFaradsSpecifier => (TeraFarads, TeraFaradsSymbol),
            PetaFaradsSpecifier => (PetaFarads, PetaFaradsSymbol),
            ExaFaradsSpecifier => (ExaFarads, ExaFaradsSymbol),
            ZettaFaradsSpecifier => (ZettaFarads, ZettaFaradsSymbol),
            YottaFaradsSpecifier => (YottaFarads, YottaFaradsSymbol),
            RonnaFaradsSpecifier => (RonnaFarads, RonnaFaradsSymbol),
            QuettaFaradsSpecifier => (QuettaFarads, QuettaFaradsSymbol),
            _ => throw ArgumentException.InvalidFormat(format,
                "qF, rF, yF, zF, aF, fF, pF, nF, uF, mF, cF, dF, " +
                "F, daF, hF, kF, MF, GF, TF, PF, EF, ZF, YF, RF, and QF")
        };

        T rounded = scale > 0 ? T.Round(value, scale) : value;

        return $"{rounded.ToString($"N{scale}", formatProvider ?? CultureInfo.CurrentCulture)} {symbol}";
    }
}
