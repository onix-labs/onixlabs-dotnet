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

public readonly partial struct Resistance<T>
{
    /// <inheritdoc/>
    public override string ToString() => ToString(QuectoOhmsSpecifier);

    /// <inheritdoc/>
    public string ToString(string? format, IFormatProvider? formatProvider) => ToString(format.AsSpan(), formatProvider);

    /// <inheritdoc/>
    public string ToString(ReadOnlySpan<char> format, IFormatProvider? formatProvider = null)
    {
        (string specifier, int scale) = format.GetSpecifierAndScale(defaultSpecifier: QuectoOhmsSpecifier);

        (T value, string symbol) = specifier switch
        {
            QuectoOhmsSpecifier => (QuectoOhms, QuectoOhmsSymbol),
            RontoOhmsSpecifier => (RontoOhms, RontoOhmsSymbol),
            YoctoOhmsSpecifier => (YoctoOhms, YoctoOhmsSymbol),
            ZeptoOhmsSpecifier => (ZeptoOhms, ZeptoOhmsSymbol),
            AttoOhmsSpecifier => (AttoOhms, AttoOhmsSymbol),
            FemtoOhmsSpecifier => (FemtoOhms, FemtoOhmsSymbol),
            PicoOhmsSpecifier => (PicoOhms, PicoOhmsSymbol),
            NanoOhmsSpecifier => (NanoOhms, NanoOhmsSymbol),
            MicroOhmsSpecifier => (MicroOhms, MicroOhmsSymbol),
            MilliOhmsSpecifier => (MilliOhms, MilliOhmsSymbol),
            CentiOhmsSpecifier => (CentiOhms, CentiOhmsSymbol),
            DeciOhmsSpecifier => (DeciOhms, DeciOhmsSymbol),
            OhmsSpecifier => (Ohms, OhmsSymbol),
            DecaOhmsSpecifier => (DecaOhms, DecaOhmsSymbol),
            HectoOhmsSpecifier => (HectoOhms, HectoOhmsSymbol),
            KiloOhmsSpecifier => (KiloOhms, KiloOhmsSymbol),
            MegaOhmsSpecifier => (MegaOhms, MegaOhmsSymbol),
            GigaOhmsSpecifier => (GigaOhms, GigaOhmsSymbol),
            TeraOhmsSpecifier => (TeraOhms, TeraOhmsSymbol),
            PetaOhmsSpecifier => (PetaOhms, PetaOhmsSymbol),
            ExaOhmsSpecifier => (ExaOhms, ExaOhmsSymbol),
            ZettaOhmsSpecifier => (ZettaOhms, ZettaOhmsSymbol),
            YottaOhmsSpecifier => (YottaOhms, YottaOhmsSymbol),
            RonnaOhmsSpecifier => (RonnaOhms, RonnaOhmsSymbol),
            QuettaOhmsSpecifier => (QuettaOhms, QuettaOhmsSymbol),
            _ => throw ArgumentException.InvalidFormat(format,
                "qohm, rohm, yohm, zohm, aohm, fohm, pohm, nohm, uohm, mohm, cohm, dohm, " +
                "ohm, daohm, hohm, kohm, Mohm, Gohm, Tohm, Pohm, Eohm, Zohm, Yohm, Rohm, and Qohm")
        };

        T rounded = scale > 0 ? T.Round(value, scale) : value;

        return $"{rounded.ToString($"N{scale}", formatProvider ?? CultureInfo.CurrentCulture)} {symbol}";
    }
}
