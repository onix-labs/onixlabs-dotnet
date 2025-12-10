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

namespace OnixLabs.Units;

public readonly partial struct Area<T>
{
    /// <inheritdoc/>
    public static Area<T> Zero => new(T.Zero);

    private const string SquareQuectoMetersSpecifier = "sqqm";
    private const string SquareQuectoMetersSymbol = "qm²";

    private const string SquareRontoMetersSpecifier = "sqrm";
    private const string SquareRontoMetersSymbol = "rm²";

    private const string SquareYoctoMetersSpecifier = "sqym";
    private const string SquareYoctoMetersSymbol = "ym²";

    private const string SquareZeptoMetersSpecifier = "sqzm";
    private const string SquareZeptoMetersSymbol = "zm²";

    private const string SquareAttoMetersSpecifier = "sqam";
    private const string SquareAttoMetersSymbol = "am²";

    private const string SquareFemtoMetersSpecifier = "sqfm";
    private const string SquareFemtoMetersSymbol = "fm²";

    private const string SquarePicoMetersSpecifier = "sqpm";
    private const string SquarePicoMetersSymbol = "pm²";

    private const string SquareNanoMetersSpecifier = "sqnm";
    private const string SquareNanoMetersSymbol = "nm²";

    private const string SquareMicroMetersSpecifier = "squm";
    private const string SquareMicroMetersSymbol = "µm²";

    private const string SquareMilliMetersSpecifier = "sqmm";
    private const string SquareMilliMetersSymbol = "mm²";

    private const string SquareCentiMetersSpecifier = "sqcm";
    private const string SquareCentiMetersSymbol = "cm²";

    private const string SquareDeciMetersSpecifier = "sqdm";
    private const string SquareDeciMetersSymbol = "dm²";

    private const string SquareMetersSpecifier = "sqm";
    private const string SquareMetersSymbol = "m²";

    private const string SquareDecaMetersSpecifier = "sqdam";
    private const string SquareDecaMetersSymbol = "dam²";

    private const string SquareHectoMetersSpecifier = "sqhm";
    private const string SquareHectoMetersSymbol = "hm²";

    private const string SquareKiloMetersSpecifier = "sqkm";
    private const string SquareKiloMetersSymbol = "km²";

    private const string SquareMegaMetersSpecifier = "sqMm";
    private const string SquareMegaMetersSymbol = "Mm²";

    private const string SquareGigaMetersSpecifier = "sqGm";
    private const string SquareGigaMetersSymbol = "Gm²";

    private const string SquareTeraMetersSpecifier = "sqTm";
    private const string SquareTeraMetersSymbol = "Tm²";

    private const string SquarePetaMetersSpecifier = "sqPm";
    private const string SquarePetaMetersSymbol = "Pm²";

    private const string SquareExaMetersSpecifier = "sqEm";
    private const string SquareExaMetersSymbol = "Em²";

    private const string SquareZettaMetersSpecifier = "sqZm";
    private const string SquareZettaMetersSymbol = "Zm²";

    private const string SquareYottaMetersSpecifier = "sqYm";
    private const string SquareYottaMetersSymbol = "Ym²";

    private const string SquareRonnaMetersSpecifier = "sqRm";
    private const string SquareRonnaMetersSymbol = "Rm²";

    private const string SquareQuettaMetersSpecifier = "sqQm";
    private const string SquareQuettaMetersSymbol = "Qm²";

    private const string SquareInchesSpecifier = "sqin";
    private const string SquareInchesSymbol = "in²";

    private const string SquareFeetSpecifier = "sqft";
    private const string SquareFeetSymbol = "ft²";

    private const string SquareYardsSpecifier = "sqyd";
    private const string SquareYardsSymbol = "yd²";

    private const string SquareMilesSpecifier = "sqmi";
    private const string SquareMilesSymbol = "mi²";

    private const string SquareNauticalMilesSpecifier = "sqnmi";
    private const string SquareNauticalMilesSymbol = "nmi²";

    private const string SquareFermisSpecifier = "sqfmi";
    private const string SquareFermisSymbol = "fmi²";

    private const string SquareAngstromsSpecifier = "sqa";
    private const string SquareAngstromsSymbol = "Å²";

    private const string SquareAstronomicalUnitsSpecifier = "sqau";
    private const string SquareAstronomicalUnitsSymbol = "au²";

    private const string SquareLightYearsSpecifier = "sqly";
    private const string SquareLightYearsSymbol = "ly²";

    private const string SquareParsecsSpecifier = "sqpc";
    private const string SquareParsecsSymbol = "pc²";
}
