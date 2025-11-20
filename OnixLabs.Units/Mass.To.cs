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

// ReSharper disable MemberCanBePrivate.Global
public readonly partial struct Mass<T>
{
    /// <summary>
    /// Formats the value of the current instance using the default format.
    /// </summary>
    /// <returns>Returns the value of the current instance in the default format.</returns>
    public override string ToString() => ToString(YoctoGramsSpecifier);

    /// <summary>
    /// Formats the value of the current instance using the specified format.
    /// </summary>
    /// <param name="format">The format to use, or null to use the default format.</param>
    /// <param name="formatProvider">The provider to use to format the value.</param>
    /// <returns>Returns the value of the current instance in the specified format.</returns>
    public string ToString(string? format, IFormatProvider? formatProvider = null) => ToString(format.AsSpan(), formatProvider);

    /// <summary>
    /// Formats the value of the current instance using the specified format.
    /// </summary>
    /// <param name="format">The format to use, or null to use the default format.</param>
    /// <param name="formatProvider">The provider to use to format the value.</param>
    /// <returns>Returns the value of the current instance in the specified format.</returns>
    public string ToString(ReadOnlySpan<char> format, IFormatProvider? formatProvider = null)
    {
        (string specifier, int scale) = format.GetSpecifierAndScale(defaultSpecifier: YoctoGramsSpecifier);

        T value = specifier switch
        {
            YoctoGramsSpecifier => YoctoGrams,
            ZeptoGramsSpecifier => ZeptoGrams,
            AttoGramsSpecifier => AttoGrams,
            FemtoGramsSpecifier => FemtoGrams,
            PicoGramsSpecifier => PicoGrams,
            NanoGramsSpecifier => NanoGrams,
            MicroGramsSpecifier => MicroGrams,
            MilliGramsSpecifier => MilliGrams,
            GramsSpecifier => Grams,
            KiloGramsSpecifier => KiloGrams,
            MegaGramsSpecifier => MegaGrams,
            TonneSpecifier => Tonnes,
            GigaGramsSpecifier => GigaGrams,
            TeraGramsSpecifier => TeraGrams,
            PetaGramsSpecifier => PetaGrams,
            ExaGramsSpecifier => ExaGrams,
            ZettaGramsSpecifier => ZettaGrams,
            YottaGramsSpecifier => YottaGrams,
            PoundsSpecifier => Pounds,
            OuncesSpecifier => Ounces,
            StonesSpecifier => Stones,
            GrainsSpecifier => Grains,
            ShortTonsSpecifier => ShortTons,
            LongTonsSpecifier => LongTons,
            HundredweightUsSpecifier => HundredweightUs,
            HundredweightUkSpecifier => HundredweightUk,
            QuartersSpecifier => Quarters,
            TroyPoundsSpecifier => TroyPounds,
            TroyOuncesSpecifier => TroyOunces,
            PennyweightsSpecifier => Pennyweights,
            _ => throw ArgumentException.InvalidFormat(format,
                "yg, zg, ag, fg, pg, ng, ug, mg, g, kg, Mg, " +
                "t, Gg, Tg, Pg, Eg, Zg, Yg, lb, oz, st, gr, " +
                "ton, lt, cwtUS, cwtUK, qr, lbt, ozt, and dwt")
        };

        T rounded = scale > 0 ? T.Round(value, scale) : value;

        return $"{rounded.ToString($"N{scale}", formatProvider ?? CultureInfo.CurrentCulture)} {specifier}";
    }
}
