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

namespace OnixLabs.Units;

public readonly partial struct Volume<T>
{
    /// <summary>
    /// Gets the value of the current instance expressed in the scale identified by the specified format specifier.
    /// </summary>
    /// <param name="specifier">The format specifier identifying the scale (e.g. <c>m</c>, <c>km</c>, <c>mi</c>) at which to read the value.</param>
    /// <returns>Returns the value of the current instance expressed in the scale identified by the specified format specifier.</returns>
    public T ValueOf(ReadOnlySpan<char> specifier) => new string(specifier) switch
    {
        CubicQuectoMetersSpecifier => CubicQuectoMeters,
        CubicRontoMetersSpecifier => CubicRontoMeters,
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
        CubicRonnaMetersSpecifier => CubicRonnaMeters,
        CubicQuettaMetersSpecifier => CubicQuettaMeters,
        CubicInchesSpecifier => CubicInches,
        CubicFeetSpecifier => CubicFeet,
        CubicYardsSpecifier => CubicYards,
        CubicMilesSpecifier => CubicMiles,
        CubicAstronomicalUnitsSpecifier => CubicAstronomicalUnits,
        CubicLightYearsSpecifier => CubicLightYears,
        CubicParsecsSpecifier => CubicParsecs,
        LitersSpecifier => Liters,
        MillilitersSpecifier => Milliliters,
        UsGallonsSpecifier => USGallons,
        UsQuartsSpecifier => USQuarts,
        UsPintsSpecifier => USPints,
        UsCupsSpecifier => USCups,
        UsFluidOuncesSpecifier => USFluidOunces,
        UsTablespoonsSpecifier => USTablespoons,
        UsTeaspoonsSpecifier => USTeaspoons,
        ImperialGallonsSpecifier => ImperialGallons,
        ImperialQuartsSpecifier => ImperialQuarts,
        ImperialPintsSpecifier => ImperialPints,
        ImperialFluidOuncesSpecifier => ImperialFluidOunces,
        OilBarrelsSpecifier => OilBarrels,
        _ => throw ArgumentException.InvalidFormat(specifier, ValidSpecifiers)
    };

    /// <summary>
    /// Gets the display symbol corresponding to the specified format specifier.
    /// </summary>
    /// <param name="specifier">The format specifier whose display symbol should be returned.</param>
    /// <returns>Returns the display symbol corresponding to the specified format specifier, or the specifier itself when no mapping exists.</returns>
    public string SymbolOf(ReadOnlySpan<char> specifier) => new string(specifier) switch
    {
        CubicQuectoMetersSpecifier => CubicQuectoMetersSymbol,
        CubicRontoMetersSpecifier => CubicRontoMetersSymbol,
        CubicYoctoMetersSpecifier => CubicYoctoMetersSymbol,
        CubicZeptoMetersSpecifier => CubicZeptoMetersSymbol,
        CubicAttoMetersSpecifier => CubicAttoMetersSymbol,
        CubicFemtoMetersSpecifier => CubicFemtoMetersSymbol,
        CubicPicoMetersSpecifier => CubicPicoMetersSymbol,
        CubicNanoMetersSpecifier => CubicNanoMetersSymbol,
        CubicMicroMetersSpecifier => CubicMicroMetersSymbol,
        CubicMilliMetersSpecifier => CubicMilliMetersSymbol,
        CubicCentiMetersSpecifier => CubicCentiMetersSymbol,
        CubicDeciMetersSpecifier => CubicDeciMetersSymbol,
        CubicMetersSpecifier => CubicMetersSymbol,
        CubicDecaMetersSpecifier => CubicDecaMetersSymbol,
        CubicHectoMetersSpecifier => CubicHectoMetersSymbol,
        CubicKiloMetersSpecifier => CubicKiloMetersSymbol,
        CubicMegaMetersSpecifier => CubicMegaMetersSymbol,
        CubicGigaMetersSpecifier => CubicGigaMetersSymbol,
        CubicTeraMetersSpecifier => CubicTeraMetersSymbol,
        CubicPetaMetersSpecifier => CubicPetaMetersSymbol,
        CubicExaMetersSpecifier => CubicExaMetersSymbol,
        CubicZettaMetersSpecifier => CubicZettaMetersSymbol,
        CubicYottaMetersSpecifier => CubicYottaMetersSymbol,
        CubicRonnaMetersSpecifier => CubicRonnaMetersSymbol,
        CubicQuettaMetersSpecifier => CubicQuettaMetersSymbol,
        CubicInchesSpecifier => CubicInchesSymbol,
        CubicFeetSpecifier => CubicFeetSymbol,
        CubicYardsSpecifier => CubicYardsSymbol,
        CubicMilesSpecifier => CubicMilesSymbol,
        CubicAstronomicalUnitsSpecifier => CubicAstronomicalUnitsSymbol,
        CubicLightYearsSpecifier => CubicLightYearsSymbol,
        CubicParsecsSpecifier => CubicParsecsSymbol,
        LitersSpecifier => LitersSymbol,
        MillilitersSpecifier => MillilitersSymbol,
        UsGallonsSpecifier => UsGallonsSymbol,
        UsQuartsSpecifier => UsQuartsSymbol,
        UsPintsSpecifier => UsPintsSymbol,
        UsCupsSpecifier => UsCupsSymbol,
        UsFluidOuncesSpecifier => UsFluidOuncesSymbol,
        UsTablespoonsSpecifier => UsTablespoonsSymbol,
        UsTeaspoonsSpecifier => UsTeaspoonsSymbol,
        ImperialGallonsSpecifier => ImperialGallonsSymbol,
        ImperialQuartsSpecifier => ImperialQuartsSymbol,
        ImperialPintsSpecifier => ImperialPintsSymbol,
        ImperialFluidOuncesSpecifier => ImperialFluidOuncesSymbol,
        OilBarrelsSpecifier => OilBarrelsSymbol,
        _ => throw ArgumentException.InvalidFormat(specifier, ValidSpecifiers)
    };
}
