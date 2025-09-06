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
public readonly partial struct Distance<T>
{
    /// <summary>
    /// Formats the value of the current instance using the default format.
    /// </summary>
    /// <returns>Returns the value of the current instance in the default format.</returns>
    public override string ToString() => ToString(QuectoMetersSpecifier);

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
        (string specifier, int scale) = format.GetSpecifierAndScale(defaultSpecifier: MetersSpecifier);

        T value = specifier switch
        {
            QuectoMetersSpecifier => QuectoMeters,
            RontoMetersSpecifier => RontoMeters,
            YoctoMetersSpecifier => YoctoMeters,
            ZeptoMetersSpecifier => ZeptoMeters,
            AttoMetersSpecifier => AttoMeters,
            FemtoMetersSpecifier => FemtoMeters,
            PicoMetersSpecifier => PicoMeters,
            NanoMetersSpecifier => NanoMeters,
            MicroMetersSpecifier => MicroMeters,
            MilliMetersSpecifier => MilliMeters,
            CentiMetersSpecifier => CentiMeters,
            DeciMetersSpecifier => DeciMeters,
            MetersSpecifier => Meters,
            DecaMetersSpecifier => DecaMeters,
            HectoMetersSpecifier => HectoMeters,
            KiloMetersSpecifier => KiloMeters,
            MegaMetersSpecifier => MegaMeters,
            GigaMetersSpecifier => GigaMeters,
            TeraMetersSpecifier => TeraMeters,
            PetaMetersSpecifier => PetaMeters,
            ExaMetersSpecifier => ExaMeters,
            ZettaMetersSpecifier => ZettaMeters,
            YottaMetersSpecifier => YottaMeters,
            RonnaMetersSpecifier => RonnaMeters,
            QuettaMetersSpecifier => QuettaMeters,
            InchesSpecifier => Inches,
            FeetSpecifier => Feet,
            YardsSpecifier => Yards,
            MilesSpecifier => Miles,
            NauticalMilesSpecifier => NauticalMiles,
            FermisSpecifier => Fermis,
            AngstromsSpecifier => Angstroms,
            AstronomicalUnitsSpecifier => AstronomicalUnits,
            LightYearsSpecifier => LightYears,
            ParsecsSpecifier => Parsecs,
            _ => throw new ArgumentException(
                $"Format '{format.ToString()}' is invalid. " +
                "Valid format specifiers are: " +
                "qm, rm, ym, zm, am, fm, pm, nm, um, mm, cm, dm, m, dam, hm, km, Mm, Gm, Tm, Pm, Em, Zm, Ym, Rm, Qm, " +
                "in, ft, yd, mi, nmi, fmi, a, au, ly, pc. " +
                "Format specifiers may also be suffixed with a scale value.",
                nameof(format))
        };

        T rounded = scale > 0 ? T.Round(value, scale) : value;

        return $"{rounded.ToString($"N{scale}", formatProvider ?? CultureInfo.CurrentCulture)} {specifier}";
    }
}
