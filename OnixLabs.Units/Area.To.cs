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
public readonly partial struct Area<T>
{
    /// <summary>
    /// Returns a string representation of the current instance using the default format.
    /// </summary>
    /// <returns>Returns a string representation of the current instance.</returns>
    public override string ToString() => ToString(SquareYoctoMetersSpecifier);

    /// <summary>
    /// Returns a string representation of the current instance using the specified format and format provider.
    /// </summary>
    /// <param name="format">A standard or custom format string.</param>
    /// <param name="formatProvider">An object that provides culture-specific formatting information.</param>
    /// <returns>Returns a string representation of the current instance in the specified format.</returns>
    public string ToString(string? format, IFormatProvider? formatProvider = null) =>
        ToString(format.AsSpan(), formatProvider);

    /// <summary>
    /// Returns a string representation of the current instance using the specified format and format provider.
    /// </summary>
    /// <param name="format">A read-only span of characters representing a standard or custom format string.</param>
    /// <param name="formatProvider">An object that provides culture-specific formatting information.</param>
    /// <returns>Returns a string representation of the current instance in the specified format.</returns>
    public string ToString(ReadOnlySpan<char> format, IFormatProvider? formatProvider = null)
    {
        (string specifier, int scale) = format.GetSpecifierAndScale(defaultSpecifier: SquareYoctoMetersSpecifier);

        T value = specifier switch
        {
            SquareYoctoMetersSpecifier => SquareYoctoMeters,
            SquareZeptoMetersSpecifier => SquareZeptoMeters,
            SquareAttoMetersSpecifier => SquareAttoMeters,
            SquareFemtoMetersSpecifier => SquareFemtoMeters,
            SquarePicoMetersSpecifier => SquarePicoMeters,
            SquareNanoMetersSpecifier => SquareNanoMeters,
            SquareMicroMetersSpecifier => SquareMicroMeters,
            SquareMilliMetersSpecifier => SquareMilliMeters,
            SquareCentiMetersSpecifier => SquareCentiMeters,
            SquareDeciMetersSpecifier => SquareDeciMeters,
            SquareMetersSpecifier => SquareMeters,
            SquareDecaMetersSpecifier => SquareDecaMeters,
            SquareHectoMetersSpecifier => SquareHectoMeters,
            SquareKiloMetersSpecifier => SquareKiloMeters,
            SquareMegaMetersSpecifier => SquareMegaMeters,
            SquareGigaMetersSpecifier => SquareGigaMeters,
            SquareTeraMetersSpecifier => SquareTeraMeters,
            SquarePetaMetersSpecifier => SquarePetaMeters,
            SquareExaMetersSpecifier => SquareExaMeters,
            SquareZettaMetersSpecifier => SquareZettaMeters,
            SquareYottaMetersSpecifier => SquareYottaMeters,
            SquareInchesSpecifier => SquareInches,
            SquareFeetSpecifier => SquareFeet,
            SquareYardsSpecifier => SquareYards,
            SquareMilesSpecifier => SquareMiles,
            AcresSpecifier => Acres,
            HectaresSpecifier => Hectares,
            _ => throw new ArgumentException(
                $"Format '{format.ToString()}' is invalid. Valid format specifiers are: " +
                "sqym, sqzm, sqam, sqfm, sqpm, sqnm, squm, sqmm, sqcm, sqdm, sqm, sqdam, sqhm, sqkm, " +
                "sqMm, sqGm, sqTm, sqPm, sqEm, sqZm, sqYm, sqin, sqft, sqyd, sqmi, ac, ha. " +
                "Format specifiers may also be suffixed with a scale value.",
                nameof(format))
        };

        T rounded = scale > 0 ? T.Round(value, scale) : value;

        return $"{rounded.ToString($"N{scale}", formatProvider ?? CultureInfo.CurrentCulture)} {specifier}";
    }
}
