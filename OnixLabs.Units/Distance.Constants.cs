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

public readonly partial struct Distance<T>
{
    /// <inheritdoc/>
    public static Distance<T> Zero => new(T.Zero);

    // T-precision conversion factors for non-power-of-10 distance scales (imperial, astronomical).
    // Stored as static readonly per closed T so they're computed once and reused at the target type's
    // precision. Each is "QuectoMeters in one X", written so the entire factor stays at T's precision;
    // `T.CreateChecked(<decimal>)` would silently pre-round through double's ~15-17 digit precision.
    private static readonly T QuectometersPerInch = T.CreateChecked(254) * UnitMath.Pow10<T>(26); // 0.0254 m
    private static readonly T QuectometersPerFoot = T.CreateChecked(3048) * UnitMath.Pow10<T>(26); // 0.3048 m
    private static readonly T QuectometersPerYard = T.CreateChecked(9144) * UnitMath.Pow10<T>(26); // 0.9144 m
    private static readonly T QuectometersPerMile = T.CreateChecked(1609344) * UnitMath.Pow10<T>(27); // 1 609.344 m
    private static readonly T QuectometersPerNauticalMile = T.CreateChecked(1852) * UnitMath.Pow10<T>(30); // 1 852 m
    private static readonly T QuectometersPerFermi = UnitMath.Pow10<T>(15); // 1e-15 m
    private static readonly T QuectometersPerAngstrom = UnitMath.Pow10<T>(20); // 1e-10 m
    private static readonly T QuectometersPerAstronomicalUnit = T.CreateChecked(149_597_870_700L) * UnitMath.Pow10<T>(30); // exact IAU definition (m)
    private static readonly T QuectometersPerLightYear = T.CreateChecked(9_460_730_472_580_800L) * UnitMath.Pow10<T>(30); // 365.25 d × c (m)

    // Parsec is defined as (648000 / π) astronomical units. The division by π introduces one ULP of
    // T-precision rounding (π is irrational); every other factor in the chain is exact at Float128.
    private static readonly T QuectometersPerParsec = T.CreateChecked(149_597_870_700L) * T.CreateChecked(648000) * UnitMath.Pow10<T>(30) / T.Pi;

    private const string QuectoMetersSpecifier = "qm";
    private const string RontoMetersSpecifier = "rm";
    private const string YoctoMetersSpecifier = "ym";
    private const string ZeptoMetersSpecifier = "zm";
    private const string AttoMetersSpecifier = "am";
    private const string FemtoMetersSpecifier = "fm";
    private const string PicoMetersSpecifier = "pm";
    private const string NanoMetersSpecifier = "nm";
    private const string MicroMetersSpecifier = "um";
    private const string MilliMetersSpecifier = "mm";
    private const string CentiMetersSpecifier = "cm";
    private const string DeciMetersSpecifier = "dm";
    private const string MetersSpecifier = "m";
    private const string DecaMetersSpecifier = "dam";
    private const string HectoMetersSpecifier = "hm";
    private const string KiloMetersSpecifier = "km";
    private const string MegaMetersSpecifier = "Mm";
    private const string GigaMetersSpecifier = "Gm";
    private const string TeraMetersSpecifier = "Tm";
    private const string PetaMetersSpecifier = "Pm";
    private const string ExaMetersSpecifier = "Em";
    private const string ZettaMetersSpecifier = "Zm";
    private const string YottaMetersSpecifier = "Ym";
    private const string RonnaMetersSpecifier = "Rm";
    private const string QuettaMetersSpecifier = "Qm";
    private const string InchesSpecifier = "in";
    private const string FeetSpecifier = "ft";
    private const string YardsSpecifier = "yd";
    private const string MilesSpecifier = "mi";
    private const string NauticalMilesSpecifier = "nmi";
    private const string FermisSpecifier = "fmi";
    private const string AngstromsSpecifier = "a";
    private const string AstronomicalUnitsSpecifier = "au";
    private const string LightYearsSpecifier = "ly";
    private const string ParsecsSpecifier = "pc";

    private const string ValidSpecifiers =
        "qm, rm, ym, zm, am, fm, pm, nm, um, mm, cm, dm, " +
        "m, dam, hm, km, Mm, Gm, Tm, Pm, Em, Zm, Ym, Rm, " +
        "Qm, in, ft, yd, mi, nmi, fmi, a, au, ly, and pc";
}
