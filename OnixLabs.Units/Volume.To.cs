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
public readonly partial struct Volume<T>
{
    /// <summary>
    /// Formats the value of the current instance using the default format.
    /// </summary>
    /// <returns>Returns the value of the current instance in the default format.</returns>
    public override string ToString() => ToString(CubicYoctoMetersSpecifier);

    /// <summary>
    /// Formats the value of the current instance using the specified format and format provider.
    /// </summary>
    /// <param name="format">The format to use, or null to use the default format.</param>
    /// <param name="formatProvider">The provider to use to format the value.</param>
    /// <returns>Returns the value of the current instance in the specified format.</returns>
    public string ToString(string? format, IFormatProvider? formatProvider = null) => ToString(format.AsSpan(), formatProvider);

    /// <summary>
    /// Formats the value of the current instance using the specified format and format provider.
    /// </summary>
    /// <param name="format">The format to use, or null to use the default format.</param>
    /// <param name="formatProvider">The provider to use to format the value.</param>
    /// <returns>Returns the value of the current instance in the specified format.</returns>
    public string ToString(ReadOnlySpan<char> format, IFormatProvider? formatProvider = null)
    {
        (string specifier, int scale) = format.GetSpecifierAndScale(defaultSpecifier: CubicYoctoMetersSpecifier);

        T value = specifier switch
        {
            CubicYoctoMetersSpecifier => CubicYoctoMeters,
            CubicZeptoMetersSpecifier => CubicZeptoMeters,
            CubicAttoMetersSpecifier => CubicAttoMeters,
            CubicFemtoMetersSpecifier => CubicFemtoMeters,
            CubicPicoMetersSpecifier => CubicPicoMeters,
            CubicNanoMetersSpecifier => CubicNanoMeters,
            CubicMicroMetersSpecifier => CubicMicroMeters,
            CubicMilliMetersSpecifier => CubicMilliMeters,
            CubicCentiMetersSpecifier => CubicCentiMeters,
            CubicDeciMetersSpecifier => CubicDeciMeters,
            CubicMetersSpecifier => CubicMeters,
            CubicDecaMetersSpecifier => CubicDecaMeters,
            CubicHectoMetersSpecifier => CubicHectoMeters,
            CubicKiloMetersSpecifier => CubicKiloMeters,
            CubicMegaMetersSpecifier => CubicMegaMeters,
            CubicGigaMetersSpecifier => CubicGigaMeters,
            CubicTeraMetersSpecifier => CubicTeraMeters,
            CubicPetaMetersSpecifier => CubicPetaMeters,
            CubicExaMetersSpecifier => CubicExaMeters,
            CubicZettaMetersSpecifier => CubicZettaMeters,
            CubicYottaMetersSpecifier => CubicYottaMeters,
            LitersSpecifier => Liters,
            MilliLitersSpecifier => MilliLiters,
            CentiLitersSpecifier => CentiLiters,
            DeciLitersSpecifier => DeciLiters,
            CubicInchesSpecifier => CubicInches,
            CubicFeetSpecifier => CubicFeet,
            CubicYardsSpecifier => CubicYards,
            FluidOuncesSpecifier => FluidOunces,
            CupsSpecifier => Cups,
            PintsSpecifier => Pints,
            QuartsSpecifier => Quarts,
            GallonsSpecifier => Gallons,
            _ => throw new ArgumentException(
                $"Format '{format.ToString()}' is invalid. Valid format specifiers are: " +
                "cuym, cuzm, cuam, cufm, cupm, cunm, cuum, cumm, cucm, cudm, cum, cudam, cuhm, cukm, cuMm, " +
                "cuGm, cuTm, cuPm, cuEm, cuZm, cuYm, l, ml, cl, dl, cuin, cuft, cuyd, floz, cup, pt, qt, gal. " +
                "Format specifiers may also be suffixed with a scale value.",
                nameof(format))
        };

        T rounded = scale > 0 ? T.Round(value, scale) : value;

        return $"{rounded.ToString($"N{scale}", formatProvider ?? CultureInfo.CurrentCulture)} {specifier}";
    }
}
