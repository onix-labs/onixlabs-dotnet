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

public readonly partial struct Mass<T>
{
    /// <inheritdoc/>
    public override string ToString() => ToString(QuectoGramsSpecifier);

    /// <inheritdoc/>
    public string ToString(string? format, IFormatProvider? formatProvider) => ToString(format.AsSpan(), formatProvider);

    /// <inheritdoc/>
    public string ToString(ReadOnlySpan<char> format, IFormatProvider? formatProvider = null)
    {
        (string specifier, int scale) = format.GetSpecifierAndScale(defaultSpecifier: QuectoGramsSpecifier);

        T value = specifier switch
        {
            QuectoGramsSpecifier => QuectoGrams,
            RontoGramsSpecifier => RontoGrams,
            YoctoGramsSpecifier => YoctoGrams,
            ZeptoGramsSpecifier => ZeptoGrams,
            AttoGramsSpecifier => AttoGrams,
            FemtoGramsSpecifier => FemtoGrams,
            PicoGramsSpecifier => PicoGrams,
            NanoGramsSpecifier => NanoGrams,
            MicroGramsSpecifier => MicroGrams,
            MilliGramsSpecifier => MilliGrams,
            CentiGramsSpecifier => CentiGrams,
            DeciGramsSpecifier => DeciGrams,
            GramsSpecifier => Grams,
            DecaGramsSpecifier => DecaGrams,
            HectoGramsSpecifier => HectoGrams,
            KiloGramsSpecifier => KiloGrams,
            MegaGramsSpecifier => MegaGrams,
            GigaGramsSpecifier => GigaGrams,
            TeraGramsSpecifier => TeraGrams,
            PetaGramsSpecifier => PetaGrams,
            ExaGramsSpecifier => ExaGrams,
            ZettaGramsSpecifier => ZettaGrams,
            YottaGramsSpecifier => YottaGrams,
            RonnaGramsSpecifier => RonnaGrams,
            QuettaGramsSpecifier => QuettaGrams,
            TonnesSpecifier => Tonnes,
            OuncesSpecifier => Ounces,
            PoundsSpecifier => Pounds,
            StonesSpecifier => Stones,
            ShortTonsSpecifier => ShortTons,
            LongTonsSpecifier => LongTons,
            CaratsSpecifier => Carats,
            GrainsSpecifier => Grains,
            DramsSpecifier => Drams,
            SlugsSpecifier => Slugs,
            DaltonsSpecifier => Daltons,
            _ => throw ArgumentException.InvalidFormat(format,
                "qg, rg, yg, zg, ag, fg, pg, ng, ug, mg, cg, dg, " +
                "g, dag, hg, kg, Mg, Gg, Tg, Pg, Eg, Zg, Yg, Rg, " +
                "Qg, t, oz, lb, st, sht, lt, ct, gr, dr, slug, and Da")
        };

        T rounded = scale > 0 ? T.Round(value, scale) : value;

        return $"{rounded.ToString($"N{scale}", formatProvider ?? CultureInfo.CurrentCulture)} {specifier}";
    }
}
