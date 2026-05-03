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

public readonly partial struct Volume<T>
{
    /// <inheritdoc/>
    public override string ToString() => ToString(CubicQuectoMetersSpecifier);

    /// <inheritdoc/>
    public string ToString(string? format, IFormatProvider? formatProvider) => ToString(format.AsSpan(), formatProvider);

    /// <inheritdoc/>
    public string ToString(ReadOnlySpan<char> format, IFormatProvider? formatProvider = null)
    {
        (string specifier, int scale) = format.GetSpecifierAndScale(defaultSpecifier: CubicQuectoMetersSpecifier);

        (T value, string symbol) = specifier switch
        {
            CubicQuectoMetersSpecifier => (CubicQuectoMeters, CubicQuectoMetersSymbol),
            CubicRontoMetersSpecifier => (CubicRontoMeters, CubicRontoMetersSymbol),
            CubicYoctoMetersSpecifier => (CubicYoctoMeters, CubicYoctoMetersSymbol),
            CubicZeptoMetersSpecifier => (CubicZeptoMeters, CubicZeptoMetersSymbol),
            CubicAttoMetersSpecifier => (CubicAttoMeters, CubicAttoMetersSymbol),
            CubicFemtoMetersSpecifier => (CubicFemtoMeters, CubicFemtoMetersSymbol),
            CubicPicoMetersSpecifier => (CubicPicoMeters, CubicPicoMetersSymbol),
            CubicNanoMetersSpecifier => (CubicNanoMeters, CubicNanoMetersSymbol),
            CubicMicroMetersSpecifier => (CubicMicroMeters, CubicMicroMetersSymbol),
            CubicMilliMetersSpecifier => (CubicMilliMeters, CubicMilliMetersSymbol),
            CubicCentiMetersSpecifier => (CubicCentiMeters, CubicCentiMetersSymbol),
            CubicDeciMetersSpecifier => (CubicDeciMeters, CubicDeciMetersSymbol),
            CubicMetersSpecifier => (CubicMeters, CubicMetersSymbol),
            CubicDecaMetersSpecifier => (CubicDecaMeters, CubicDecaMetersSymbol),
            CubicHectoMetersSpecifier => (CubicHectoMeters, CubicHectoMetersSymbol),
            CubicKiloMetersSpecifier => (CubicKiloMeters, CubicKiloMetersSymbol),
            CubicMegaMetersSpecifier => (CubicMegaMeters, CubicMegaMetersSymbol),
            CubicGigaMetersSpecifier => (CubicGigaMeters, CubicGigaMetersSymbol),
            CubicTeraMetersSpecifier => (CubicTeraMeters, CubicTeraMetersSymbol),
            CubicPetaMetersSpecifier => (CubicPetaMeters, CubicPetaMetersSymbol),
            CubicExaMetersSpecifier => (CubicExaMeters, CubicExaMetersSymbol),
            CubicZettaMetersSpecifier => (CubicZettaMeters, CubicZettaMetersSymbol),
            CubicYottaMetersSpecifier => (CubicYottaMeters, CubicYottaMetersSymbol),
            CubicRonnaMetersSpecifier => (CubicRonnaMeters, CubicRonnaMetersSymbol),
            CubicQuettaMetersSpecifier => (CubicQuettaMeters, CubicQuettaMetersSymbol),
            CubicInchesSpecifier => (CubicInches, CubicInchesSymbol),
            CubicFeetSpecifier => (CubicFeet, CubicFeetSymbol),
            CubicYardsSpecifier => (CubicYards, CubicYardsSymbol),
            CubicMilesSpecifier => (CubicMiles, CubicMilesSymbol),
            CubicAstronomicalUnitsSpecifier => (CubicAstronomicalUnits, CubicAstronomicalUnitsSymbol),
            CubicLightYearsSpecifier => (CubicLightYears, CubicLightYearsSymbol),
            CubicParsecsSpecifier => (CubicParsecs, CubicParsecsSymbol),
            LitersSpecifier => (Liters, LitersSymbol),
            MillilitersSpecifier => (Milliliters, MillilitersSymbol),
            USGallonsSpecifier => (USGallons, USGallonsSymbol),
            USQuartsSpecifier => (USQuarts, USQuartsSymbol),
            USPintsSpecifier => (USPints, USPintsSymbol),
            USCupsSpecifier => (USCups, USCupsSymbol),
            USFluidOuncesSpecifier => (USFluidOunces, USFluidOuncesSymbol),
            USTablespoonsSpecifier => (USTablespoons, USTablespoonsSymbol),
            USTeaspoonsSpecifier => (USTeaspoons, USTeaspoonsSymbol),
            ImperialGallonsSpecifier => (ImperialGallons, ImperialGallonsSymbol),
            ImperialQuartsSpecifier => (ImperialQuarts, ImperialQuartsSymbol),
            ImperialPintsSpecifier => (ImperialPints, ImperialPintsSymbol),
            ImperialFluidOuncesSpecifier => (ImperialFluidOunces, ImperialFluidOuncesSymbol),
            OilBarrelsSpecifier => (OilBarrels, OilBarrelsSymbol),
            _ => throw ArgumentException.InvalidFormat(format,
                "cuqm, curm, cuym, cuzm, cuam, cufm, cupm, cunm, cuum, cumm, cucm, cudm, " +
                "cum, cudam, cuhm, cukm, cuMm, cuGm, cuTm, cuPm, cuEm, cuZm, cuYm, cuRm, " +
                "cuQm, cuin, cuft, cuyd, cumi, cuau, culy, cupc, " +
                "L, mL, " +
                "USgal, USqt, USpt, UScup, USfloz, UStbsp, UStsp, " +
                "impgal, impqt, imppt, impfloz, and bbl")
        };

        T rounded = scale > 0 ? T.Round(value, scale) : value;

        return $"{rounded.ToString($"N{scale}", formatProvider ?? CultureInfo.CurrentCulture)} {symbol}";
    }
}
