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

public readonly partial struct Current<T>
{
    /// <inheritdoc/>
    public override string ToString() => ToString(QuectoAmperesSpecifier);

    /// <inheritdoc/>
    public string ToString(string? format, IFormatProvider? formatProvider) => ToString(format.AsSpan(), formatProvider);

    /// <inheritdoc/>
    public string ToString(ReadOnlySpan<char> format, IFormatProvider? formatProvider = null)
    {
        (string specifier, int scale) = format.GetSpecifierAndScale(defaultSpecifier: QuectoAmperesSpecifier);

        (T value, string symbol) = specifier switch
        {
            QuectoAmperesSpecifier => (QuectoAmperes, QuectoAmperesSymbol),
            RontoAmperesSpecifier => (RontoAmperes, RontoAmperesSymbol),
            YoctoAmperesSpecifier => (YoctoAmperes, YoctoAmperesSymbol),
            ZeptoAmperesSpecifier => (ZeptoAmperes, ZeptoAmperesSymbol),
            AttoAmperesSpecifier => (AttoAmperes, AttoAmperesSymbol),
            FemtoAmperesSpecifier => (FemtoAmperes, FemtoAmperesSymbol),
            PicoAmperesSpecifier => (PicoAmperes, PicoAmperesSymbol),
            NanoAmperesSpecifier => (NanoAmperes, NanoAmperesSymbol),
            MicroAmperesSpecifier => (MicroAmperes, MicroAmperesSymbol),
            MilliAmperesSpecifier => (MilliAmperes, MilliAmperesSymbol),
            CentiAmperesSpecifier => (CentiAmperes, CentiAmperesSymbol),
            DeciAmperesSpecifier => (DeciAmperes, DeciAmperesSymbol),
            AmperesSpecifier => (Amperes, AmperesSymbol),
            DecaAmperesSpecifier => (DecaAmperes, DecaAmperesSymbol),
            HectoAmperesSpecifier => (HectoAmperes, HectoAmperesSymbol),
            KiloAmperesSpecifier => (KiloAmperes, KiloAmperesSymbol),
            MegaAmperesSpecifier => (MegaAmperes, MegaAmperesSymbol),
            GigaAmperesSpecifier => (GigaAmperes, GigaAmperesSymbol),
            TeraAmperesSpecifier => (TeraAmperes, TeraAmperesSymbol),
            PetaAmperesSpecifier => (PetaAmperes, PetaAmperesSymbol),
            ExaAmperesSpecifier => (ExaAmperes, ExaAmperesSymbol),
            ZettaAmperesSpecifier => (ZettaAmperes, ZettaAmperesSymbol),
            YottaAmperesSpecifier => (YottaAmperes, YottaAmperesSymbol),
            RonnaAmperesSpecifier => (RonnaAmperes, RonnaAmperesSymbol),
            QuettaAmperesSpecifier => (QuettaAmperes, QuettaAmperesSymbol),
            _ => throw ArgumentException.InvalidFormat(format,
                "qA, rA, yA, zA, aA, fA, pA, nA, uA, mA, cA, dA, " +
                "A, daA, hA, kA, MA, GA, TA, PA, EA, ZA, YA, RA, and QA")
        };

        T rounded = scale > 0 ? T.Round(value, scale) : value;

        return $"{rounded.ToString($"N{scale}", formatProvider ?? CultureInfo.CurrentCulture)} {symbol}";
    }
}
