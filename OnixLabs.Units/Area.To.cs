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

public readonly partial struct Area<T>
{
    /// <inheritdoc/>
    public override string ToString() => ToString(SquareQuectoMetersSpecifier);

    /// <inheritdoc/>
    public string ToString(string? format, IFormatProvider? formatProvider) => ToString(format.AsSpan(), formatProvider);

    /// <inheritdoc/>
    public string ToString(ReadOnlySpan<char> format, IFormatProvider? formatProvider = null)
    {
        (string specifier, int scale) = format.GetSpecifierAndScale(defaultSpecifier: SquareQuectoMetersSpecifier);

        (T value, string symbol) = specifier switch
        {
            SquareQuectoMetersSpecifier => (SquareQuectoMeters, SquareQuectoMetersSymbol),
            SquareRontoMetersSpecifier => (SquareRontoMeters, SquareRontoMetersSymbol),
            SquareYoctoMetersSpecifier => (SquareYoctoMeters, SquareYoctoMetersSymbol),
            SquareZeptoMetersSpecifier => (SquareZeptoMeters, SquareZeptoMetersSymbol),
            SquareAttoMetersSpecifier => (SquareAttoMeters, SquareAttoMetersSymbol),
            SquareFemtoMetersSpecifier => (SquareFemtoMeters, SquareFemtoMetersSymbol),
            SquarePicoMetersSpecifier => (SquarePicoMeters, SquarePicoMetersSymbol),
            SquareNanoMetersSpecifier => (SquareNanoMeters, SquareNanoMetersSymbol),
            SquareMicroMetersSpecifier => (SquareMicroMeters, SquareMicroMetersSymbol),
            SquareMilliMetersSpecifier => (SquareMilliMeters, SquareMilliMetersSymbol),
            SquareCentiMetersSpecifier => (SquareCentiMeters, SquareCentiMetersSymbol),
            SquareDeciMetersSpecifier => (SquareDeciMeters, SquareDeciMetersSymbol),
            SquareMetersSpecifier => (SquareMeters, SquareMetersSymbol),
            SquareDecaMetersSpecifier => (SquareDecaMeters, SquareDecaMetersSymbol),
            SquareHectoMetersSpecifier => (SquareHectoMeters, SquareHectoMetersSymbol),
            SquareKiloMetersSpecifier => (SquareKiloMeters, SquareKiloMetersSymbol),
            SquareMegaMetersSpecifier => (SquareMegaMeters, SquareMegaMetersSymbol),
            SquareGigaMetersSpecifier => (SquareGigaMeters, SquareGigaMetersSymbol),
            SquareTeraMetersSpecifier => (SquareTeraMeters, SquareTeraMetersSymbol),
            SquarePetaMetersSpecifier => (SquarePetaMeters, SquarePetaMetersSymbol),
            SquareExaMetersSpecifier => (SquareExaMeters, SquareExaMetersSymbol),
            SquareZettaMetersSpecifier => (SquareZettaMeters, SquareZettaMetersSymbol),
            SquareYottaMetersSpecifier => (SquareYottaMeters, SquareYottaMetersSymbol),
            SquareRonnaMetersSpecifier => (SquareRonnaMeters, SquareRonnaMetersSymbol),
            SquareQuettaMetersSpecifier => (SquareQuettaMeters, SquareQuettaMetersSymbol),
            SquareInchesSpecifier => (SquareInches, SquareInchesSymbol),
            SquareFeetSpecifier => (SquareFeet, SquareFeetSymbol),
            SquareYardsSpecifier => (SquareYards, SquareYardsSymbol),
            SquareMilesSpecifier => (SquareMiles, SquareMilesSymbol),
            SquareNauticalMilesSpecifier => (SquareNauticalMiles, SquareNauticalMilesSymbol),
            SquareFermisSpecifier => (SquareFermis, SquareFermisSymbol),
            SquareAngstromsSpecifier => (SquareAngstroms, SquareAngstromsSymbol),
            SquareAstronomicalUnitsSpecifier => (SquareAstronomicalUnits, SquareAstronomicalUnitsSymbol),
            SquareLightYearsSpecifier => (SquareLightYears, SquareLightYearsSymbol),
            SquareParsecsSpecifier => (SquareParsecs, SquareParsecsSymbol),
            _ => throw ArgumentException.InvalidFormat(format,
                "sqqm, sqrm, sqym, sqzm, sqam, sqfm, sqpm, sqnm, squm, sqmm, sqcm, sqdm, " +
                "sqm, sqdam, sqhm, sqkm, sqMm, sqGm, sqTm, sqPm, sqEm, sqZm, sqYm, sqRm, " +
                "sqQm, sqin, sqft, sqyd, sqmi, sqnmi, sqfmi, sqa, sqau, sqly, and sqpc")
        };

        T rounded = scale > 0 ? T.Round(value, scale) : value;

        return $"{rounded.ToString($"N{scale}", formatProvider ?? CultureInfo.CurrentCulture)} {symbol}";
    }
}
