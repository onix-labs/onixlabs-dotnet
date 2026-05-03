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

public readonly partial struct Pressure<T>
{
    /// <inheritdoc/>
    public override string ToString() => ToString(QuectoPascalsSpecifier);

    /// <inheritdoc/>
    public string ToString(string? format, IFormatProvider? formatProvider) => ToString(format.AsSpan(), formatProvider);

    /// <inheritdoc/>
    public string ToString(ReadOnlySpan<char> format, IFormatProvider? formatProvider = null)
    {
        (string specifier, int scale) = format.GetSpecifierAndScale(defaultSpecifier: QuectoPascalsSpecifier);

        T value = specifier switch
        {
            QuectoPascalsSpecifier => QuectoPascals,
            RontoPascalsSpecifier => RontoPascals,
            YoctoPascalsSpecifier => YoctoPascals,
            ZeptoPascalsSpecifier => ZeptoPascals,
            AttoPascalsSpecifier => AttoPascals,
            FemtoPascalsSpecifier => FemtoPascals,
            PicoPascalsSpecifier => PicoPascals,
            NanoPascalsSpecifier => NanoPascals,
            MicroPascalsSpecifier => MicroPascals,
            MilliPascalsSpecifier => MilliPascals,
            CentiPascalsSpecifier => CentiPascals,
            DeciPascalsSpecifier => DeciPascals,
            PascalsSpecifier => Pascals,
            DecaPascalsSpecifier => DecaPascals,
            HectoPascalsSpecifier => HectoPascals,
            KiloPascalsSpecifier => KiloPascals,
            MegaPascalsSpecifier => MegaPascals,
            GigaPascalsSpecifier => GigaPascals,
            TeraPascalsSpecifier => TeraPascals,
            PetaPascalsSpecifier => PetaPascals,
            ExaPascalsSpecifier => ExaPascals,
            ZettaPascalsSpecifier => ZettaPascals,
            YottaPascalsSpecifier => YottaPascals,
            RonnaPascalsSpecifier => RonnaPascals,
            QuettaPascalsSpecifier => QuettaPascals,
            BarSpecifier => Bar,
            MillibarSpecifier => Millibar,
            AtmospheresSpecifier => Atmospheres,
            TorrSpecifier => Torr,
            MillimetersOfMercurySpecifier => MillimetersOfMercury,
            InchesOfMercurySpecifier => InchesOfMercury,
            PoundsPerSquareInchSpecifier => PoundsPerSquareInch,
            KilopoundsPerSquareInchSpecifier => KilopoundsPerSquareInch,
            TechnicalAtmospheresSpecifier => TechnicalAtmospheres,
            BaryesSpecifier => Baryes,
            _ => throw ArgumentException.InvalidFormat(format,
                "qPa, rPa, yPa, zPa, aPa, fPa, pPa, nPa, uPa, mPa, cPa, dPa, " +
                "Pa, daPa, hPa, kPa, MPa, GPa, TPa, PPa, EPa, ZPa, YPa, RPa, " +
                "QPa, bar, mbar, atm, Torr, mmHg, inHg, psi, ksi, at, and Ba")
        };

        T rounded = scale > 0 ? T.Round(value, scale) : value;

        return $"{rounded.ToString($"N{scale}", formatProvider ?? CultureInfo.CurrentCulture)} {specifier}";
    }
}
