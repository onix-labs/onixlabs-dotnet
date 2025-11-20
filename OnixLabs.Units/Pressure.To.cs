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
public readonly partial struct Pressure<T>
{
    /// <summary>
    /// Formats the value of the current instance using the default format.
    /// </summary>
    /// <returns>Returns the value of the current instance in the default format.</returns>
    public override string ToString() => ToString(QuectoPascalsSpecifier);

    /// <summary>
    /// Formats the value of the current instance using the specified format.
    /// </summary>
    /// <param name="format">The format to use, or <see langword="null"/> to use the default format.</param>
    /// <param name="formatProvider">The provider to use to format the value.</param>
    /// <returns>Returns the value of the current instance in the specified format.</returns>
    public string ToString(string? format, IFormatProvider? formatProvider = null) =>
        ToString(format.AsSpan(), formatProvider);

    /// <summary>
    /// Formats the value of the current instance using the specified format.
    /// </summary>
    /// <param name="format">The format to use, or null to use the default format.</param>
    /// <param name="formatProvider">The provider to use to format the value.</param>
    /// <returns>Returns the value of the current instance in the specified format.</returns>
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
            BarsSpecifier => Bars,
            MillibarsSpecifier => Millibars,
            AtmospheresSpecifier => Atmospheres,
            TechnicalAtmospheresSpecifier => TechnicalAtmospheres,
            TorrSpecifier => Torr,
            MillimetersOfMercurySpecifier => MillimetersOfMercury,
            InchesOfMercurySpecifier => InchesOfMercury,
            PoundsPerSquareInchSpecifier => PoundsPerSquareInch,
            PoundsPerSquareFootSpecifier => PoundsPerSquareFoot,
            BaryeSpecifier => Barye,
            MillimetersOfWaterColumnSpecifier => MillimetersOfWaterColumn,
            InchesOfWaterColumnSpecifier => InchesOfWaterColumn,
            _ => throw ArgumentException.InvalidFormat(format,
                "qPa, rPa, yPa, zPa, aPa, fPa, pPa, nPa, uPa, mPa, cPa, dPa, Pa, " +
                "daPa, hPa, kPa, MPa, GPa, TPa, PPa, EPa, ZPa, YPa, RPa, QPa, bar, " +
                "mbar, atm, at, Torr, mmHg, inHg, psi, psf, Ba, mmwc, and inwc")
        };

        T rounded = scale > 0 ? T.Round(value, scale) : value;

        return $"{rounded.ToString($"N{scale}", formatProvider ?? CultureInfo.CurrentCulture)} {specifier}";
    }
}
