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

public readonly partial struct Volume<T>
{
    /// <inheritdoc/>
    public static Volume<T> Zero => new(T.Zero);

    private const string CubicQuectoMetersSpecifier = "cuqm";
    private const string CubicQuectoMetersSymbol = "qm³";

    private const string CubicRontoMetersSpecifier = "curm";
    private const string CubicRontoMetersSymbol = "rm³";

    private const string CubicYoctoMetersSpecifier = "cuym";
    private const string CubicYoctoMetersSymbol = "ym³";

    private const string CubicZeptoMetersSpecifier = "cuzm";
    private const string CubicZeptoMetersSymbol = "zm³";

    private const string CubicAttoMetersSpecifier = "cuam";
    private const string CubicAttoMetersSymbol = "am³";

    private const string CubicFemtoMetersSpecifier = "cufm";
    private const string CubicFemtoMetersSymbol = "fm³";

    private const string CubicPicoMetersSpecifier = "cupm";
    private const string CubicPicoMetersSymbol = "pm³";

    private const string CubicNanoMetersSpecifier = "cunm";
    private const string CubicNanoMetersSymbol = "nm³";

    private const string CubicMicroMetersSpecifier = "cuum";
    private const string CubicMicroMetersSymbol = "µm³";

    private const string CubicMilliMetersSpecifier = "cumm";
    private const string CubicMilliMetersSymbol = "mm³";

    private const string CubicCentiMetersSpecifier = "cucm";
    private const string CubicCentiMetersSymbol = "cm³";

    private const string CubicDeciMetersSpecifier = "cudm";
    private const string CubicDeciMetersSymbol = "dm³";

    private const string CubicMetersSpecifier = "cum";
    private const string CubicMetersSymbol = "m³";

    private const string CubicDecaMetersSpecifier = "cudam";
    private const string CubicDecaMetersSymbol = "dam³";

    private const string CubicHectoMetersSpecifier = "cuhm";
    private const string CubicHectoMetersSymbol = "hm³";

    private const string CubicKiloMetersSpecifier = "cukm";
    private const string CubicKiloMetersSymbol = "km³";

    private const string CubicMegaMetersSpecifier = "cuMm";
    private const string CubicMegaMetersSymbol = "Mm³";

    private const string CubicGigaMetersSpecifier = "cuGm";
    private const string CubicGigaMetersSymbol = "Gm³";

    private const string CubicTeraMetersSpecifier = "cuTm";
    private const string CubicTeraMetersSymbol = "Tm³";

    private const string CubicPetaMetersSpecifier = "cuPm";
    private const string CubicPetaMetersSymbol = "Pm³";

    private const string CubicExaMetersSpecifier = "cuEm";
    private const string CubicExaMetersSymbol = "Em³";

    private const string CubicZettaMetersSpecifier = "cuZm";
    private const string CubicZettaMetersSymbol = "Zm³";

    private const string CubicYottaMetersSpecifier = "cuYm";
    private const string CubicYottaMetersSymbol = "Ym³";

    private const string CubicRonnaMetersSpecifier = "cuRm";
    private const string CubicRonnaMetersSymbol = "Rm³";

    private const string CubicQuettaMetersSpecifier = "cuQm";
    private const string CubicQuettaMetersSymbol = "Qm³";

    private const string CubicInchesSpecifier = "cuin";
    private const string CubicInchesSymbol = "in³";

    private const string CubicFeetSpecifier = "cuft";
    private const string CubicFeetSymbol = "ft³";

    private const string CubicYardsSpecifier = "cuyd";
    private const string CubicYardsSymbol = "yd³";

    private const string CubicMilesSpecifier = "cumi";
    private const string CubicMilesSymbol = "mi³";

    private const string CubicAstronomicalUnitsSpecifier = "cuau";
    private const string CubicAstronomicalUnitsSymbol = "au³";

    private const string CubicLightYearsSpecifier = "culy";
    private const string CubicLightYearsSymbol = "ly³";

    private const string CubicParsecsSpecifier = "cupc";
    private const string CubicParsecsSymbol = "pc³";

    private const string LitersSpecifier = "L";
    private const string LitersSymbol = "L";

    private const string MillilitersSpecifier = "mL";
    private const string MillilitersSymbol = "mL";

    private const string USGallonsSpecifier = "USgal";
    private const string USGallonsSymbol = "US gal";

    private const string USQuartsSpecifier = "USqt";
    private const string USQuartsSymbol = "US qt";

    private const string USPintsSpecifier = "USpt";
    private const string USPintsSymbol = "US pt";

    private const string USCupsSpecifier = "UScup";
    private const string USCupsSymbol = "US cup";

    private const string USFluidOuncesSpecifier = "USfloz";
    private const string USFluidOuncesSymbol = "US fl oz";

    private const string USTablespoonsSpecifier = "UStbsp";
    private const string USTablespoonsSymbol = "US tbsp";

    private const string USTeaspoonsSpecifier = "UStsp";
    private const string USTeaspoonsSymbol = "US tsp";

    private const string ImperialGallonsSpecifier = "impgal";
    private const string ImperialGallonsSymbol = "imp gal";

    private const string ImperialQuartsSpecifier = "impqt";
    private const string ImperialQuartsSymbol = "imp qt";

    private const string ImperialPintsSpecifier = "imppt";
    private const string ImperialPintsSymbol = "imp pt";

    private const string ImperialFluidOuncesSpecifier = "impfloz";
    private const string ImperialFluidOuncesSymbol = "imp fl oz";

    private const string OilBarrelsSpecifier = "bbl";
    private const string OilBarrelsSymbol = "bbl";
}
