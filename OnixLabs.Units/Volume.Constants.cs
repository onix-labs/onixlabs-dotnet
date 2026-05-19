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

using OnixLabs.Numerics;

namespace OnixLabs.Units;

public readonly partial struct Volume<T>
{
    /// <inheritdoc/>
    public static Volume<T> Zero => new(T.Zero);

    // T-precision conversion factors for non-power-of-10 volume scales (imperial, astronomical, customary).
    // Each is "CubicQuectoMeters in one X³", written so the entire factor stays at T's precision.
    // Stored as static readonly per closed T so they're computed once and reused. `T.CreateChecked(<decimal>)`
    // would silently pre-round through double's ~15-17 digit precision before reaching T.
    private static readonly T CuQuectometersPerCubicInch             = T.CreateChecked(16387064)             * GenericMath.Pow10<T>(78); // 0.0254³ m³
    private static readonly T CuQuectometersPerCubicFoot             = T.CreateChecked(28316846592L)         * GenericMath.Pow10<T>(78); // 0.3048³ m³
    private static readonly T CuQuectometersPerCubicYard             = T.CreateChecked(764554857984L)        * GenericMath.Pow10<T>(78); // 0.9144³ m³
    private static readonly T CuQuectometersPerCubicMile             = T.CreateChecked(4168181825440579584L) * GenericMath.Pow10<T>(81); // 1 609.344³ m³

    // AU³ and LY³ are computed at T precision. Both 149_597_870_700 (AU m) and 9_460_730_472_580_800 (LY m) fit
    // in long and are exact at any IFloatingPoint<T> with ≥64-bit mantissa. Cubing stays at T precision.
    private static readonly T CuQuectometersPerCubicAstronomicalUnit =
        T.CreateChecked(149_597_870_700L) * T.CreateChecked(149_597_870_700L) * T.CreateChecked(149_597_870_700L) * GenericMath.Pow10<T>(90); // AU³ m³
    private static readonly T CuQuectometersPerCubicLightYear =
        T.CreateChecked(9_460_730_472_580_800L) * T.CreateChecked(9_460_730_472_580_800L) * T.CreateChecked(9_460_730_472_580_800L) * GenericMath.Pow10<T>(90); // (365.25 d × c)³ m³

    // Parsec³ is ((648000 / π) AU)³. π is irrational; the division rounds to 1 ULP and is cubed.
    private static readonly T MetersPerParsec = T.CreateChecked(149_597_870_700L) * T.CreateChecked(648000) / T.Pi;
    private static readonly T CuQuectometersPerCubicParsec = MetersPerParsec * MetersPerParsec * MetersPerParsec * GenericMath.Pow10<T>(90);

    private static readonly T CuQuectometersPerUSGallon            = T.CreateChecked(3785411784L)          * GenericMath.Pow10<T>(78); // 3.785411784e-3 m³
    private static readonly T CuQuectometersPerUSQuart             = T.CreateChecked(946352946L)           * GenericMath.Pow10<T>(78); // gal / 4
    private static readonly T CuQuectometersPerUSPint              = T.CreateChecked(473176473L)           * GenericMath.Pow10<T>(78); // qt / 2
    private static readonly T CuQuectometersPerUSCup               = T.CreateChecked(2365882365L)          * GenericMath.Pow10<T>(77); // pt / 2
    private static readonly T CuQuectometersPerUSFluidOunce        = T.CreateChecked(295735295625L)        * GenericMath.Pow10<T>(74); // cup / 8
    private static readonly T CuQuectometersPerUSTablespoon        = T.CreateChecked(1478676478125L)       * GenericMath.Pow10<T>(73); // fl oz / 2
    private static readonly T CuQuectometersPerUSTeaspoon          = T.CreateChecked(492892159375L)        * GenericMath.Pow10<T>(73); // tbsp / 3
    private static readonly T CuQuectometersPerImperialGallon      = T.CreateChecked(454609)               * GenericMath.Pow10<T>(82); // 4.54609e-3 m³
    private static readonly T CuQuectometersPerImperialQuart       = T.CreateChecked(11365225)             * GenericMath.Pow10<T>(80); // imp gal / 4
    private static readonly T CuQuectometersPerImperialPint        = T.CreateChecked(56826125)             * GenericMath.Pow10<T>(79); // imp qt / 2
    private static readonly T CuQuectometersPerImperialFluidOunce  = T.CreateChecked(284130625)            * GenericMath.Pow10<T>(77); // imp pt / 20
    private static readonly T CuQuectometersPerOilBarrel           = T.CreateChecked(158987294928L)        * GenericMath.Pow10<T>(78); // 42 US gal

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

    private const string UsGallonsSpecifier = "USgal";
    private const string UsGallonsSymbol = "US gal";

    private const string UsQuartsSpecifier = "USqt";
    private const string UsQuartsSymbol = "US qt";

    private const string UsPintsSpecifier = "USpt";
    private const string UsPintsSymbol = "US pt";

    private const string UsCupsSpecifier = "UScup";
    private const string UsCupsSymbol = "US cup";

    private const string UsFluidOuncesSpecifier = "USfloz";
    private const string UsFluidOuncesSymbol = "US fl oz";

    private const string UsTablespoonsSpecifier = "UStbsp";
    private const string UsTablespoonsSymbol = "US tbsp";

    private const string UsTeaspoonsSpecifier = "UStsp";
    private const string UsTeaspoonsSymbol = "US tsp";

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

    private const string ValidSpecifiers =
        "cuqm, curm, cuym, cuzm, cuam, cufm, cupm, cunm, cuum, cumm, cucm, cudm, " +
        "cum, cudam, cuhm, cukm, cuMm, cuGm, cuTm, cuPm, cuEm, cuZm, cuYm, cuRm, " +
        "cuQm, cuin, cuft, cuyd, cumi, cuau, culy, cupc, L, mL, USgal, USqt, USpt, " +
        "UScup, USfloz, UStbsp, UStsp, impgal, impqt, imppt, impfloz, and bbl";
}
