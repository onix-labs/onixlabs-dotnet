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

public readonly partial struct AmountOfSubstance<T>
{
    /// <inheritdoc/>
    public override string ToString() => ToString(QuectoMolesSpecifier);

    /// <inheritdoc/>
    public string ToString(string? format, IFormatProvider? formatProvider) => ToString(format.AsSpan(), formatProvider);

    /// <inheritdoc/>
    public string ToString(ReadOnlySpan<char> format, IFormatProvider? formatProvider = null)
    {
        (string specifier, int scale) = format.GetSpecifierAndScale(defaultSpecifier: QuectoMolesSpecifier);

        (T value, string symbol) = specifier switch
        {
            QuectoMolesSpecifier => (QuectoMoles, QuectoMolesSymbol),
            RontoMolesSpecifier => (RontoMoles, RontoMolesSymbol),
            YoctoMolesSpecifier => (YoctoMoles, YoctoMolesSymbol),
            ZeptoMolesSpecifier => (ZeptoMoles, ZeptoMolesSymbol),
            AttoMolesSpecifier => (AttoMoles, AttoMolesSymbol),
            FemtoMolesSpecifier => (FemtoMoles, FemtoMolesSymbol),
            PicoMolesSpecifier => (PicoMoles, PicoMolesSymbol),
            NanoMolesSpecifier => (NanoMoles, NanoMolesSymbol),
            MicroMolesSpecifier => (MicroMoles, MicroMolesSymbol),
            MilliMolesSpecifier => (MilliMoles, MilliMolesSymbol),
            CentiMolesSpecifier => (CentiMoles, CentiMolesSymbol),
            DeciMolesSpecifier => (DeciMoles, DeciMolesSymbol),
            MolesSpecifier => (Moles, MolesSymbol),
            DecaMolesSpecifier => (DecaMoles, DecaMolesSymbol),
            HectoMolesSpecifier => (HectoMoles, HectoMolesSymbol),
            KiloMolesSpecifier => (KiloMoles, KiloMolesSymbol),
            MegaMolesSpecifier => (MegaMoles, MegaMolesSymbol),
            GigaMolesSpecifier => (GigaMoles, GigaMolesSymbol),
            TeraMolesSpecifier => (TeraMoles, TeraMolesSymbol),
            PetaMolesSpecifier => (PetaMoles, PetaMolesSymbol),
            ExaMolesSpecifier => (ExaMoles, ExaMolesSymbol),
            ZettaMolesSpecifier => (ZettaMoles, ZettaMolesSymbol),
            YottaMolesSpecifier => (YottaMoles, YottaMolesSymbol),
            RonnaMolesSpecifier => (RonnaMoles, RonnaMolesSymbol),
            QuettaMolesSpecifier => (QuettaMoles, QuettaMolesSymbol),
            _ => throw ArgumentException.InvalidFormat(format,
                "qmol, rmol, ymol, zmol, amol, fmol, pmol, nmol, umol, mmol, cmol, dmol, " +
                "mol, damol, hmol, kmol, Mmol, Gmol, Tmol, Pmol, Emol, Zmol, Ymol, Rmol, and Qmol")
        };

        T rounded = scale > 0 ? T.Round(value, scale) : value;

        return $"{rounded.ToString($"N{scale}", formatProvider ?? CultureInfo.CurrentCulture)} {symbol}";
    }
}
