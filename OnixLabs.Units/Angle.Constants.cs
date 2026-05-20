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

public readonly partial struct Angle<T>
{
    /// <inheritdoc/>
    public static Angle<T> Zero => new(T.Zero);

    // T-precision π-based conversion factors for non-radian angle scales. Each is "QuectoRadians in one X",
    // computed at T's precision so π keeps its full T.Pi precision (40+ digits at Float128). The
    // `T.CreateChecked(<double-literal>)` form would silently pre-round π through double's 15-17 digits.
    private static readonly T QuectoRadiansPerDegree = T.Pi * UnitMath.Pow10<T>(180) / T.CreateChecked(180); // π/180 rad
    private static readonly T QuectoRadiansPerArcminute = T.Pi * UnitMath.Pow10<T>(30) / T.CreateChecked(10800); // π/(180×60) rad
    private static readonly T QuectoRadiansPerArcsecond = T.Pi * UnitMath.Pow10<T>(30) / T.CreateChecked(648000); // π/(180×3600) rad
    private static readonly T QuectoRadiansPerGradian = T.Pi * UnitMath.Pow10<T>(30) / T.CreateChecked(200); // π/200 rad
    private static readonly T QuectoRadiansPerTurn = T.Pi * UnitMath.Pow10<T>(30) * T.CreateChecked(2); // 2π rad

    private const string QuectoRadiansSpecifier = "qrad";
    private const string QuectoRadiansSymbol = "qrad";

    private const string RontoRadiansSpecifier = "rrad";
    private const string RontoRadiansSymbol = "rrad";

    private const string YoctoRadiansSpecifier = "yrad";
    private const string YoctoRadiansSymbol = "yrad";

    private const string ZeptoRadiansSpecifier = "zrad";
    private const string ZeptoRadiansSymbol = "zrad";

    private const string AttoRadiansSpecifier = "arad";
    private const string AttoRadiansSymbol = "arad";

    private const string FemtoRadiansSpecifier = "frad";
    private const string FemtoRadiansSymbol = "frad";

    private const string PicoRadiansSpecifier = "prad";
    private const string PicoRadiansSymbol = "prad";

    private const string NanoRadiansSpecifier = "nrad";
    private const string NanoRadiansSymbol = "nrad";

    private const string MicroRadiansSpecifier = "urad";
    private const string MicroRadiansSymbol = "µrad";

    private const string MilliRadiansSpecifier = "mrad";
    private const string MilliRadiansSymbol = "mrad";

    private const string CentiRadiansSpecifier = "crad";
    private const string CentiRadiansSymbol = "crad";

    private const string DeciRadiansSpecifier = "drad";
    private const string DeciRadiansSymbol = "drad";

    private const string RadiansSpecifier = "rad";
    private const string RadiansSymbol = "rad";

    private const string DecaRadiansSpecifier = "darad";
    private const string DecaRadiansSymbol = "darad";

    private const string HectoRadiansSpecifier = "hrad";
    private const string HectoRadiansSymbol = "hrad";

    private const string KiloRadiansSpecifier = "krad";
    private const string KiloRadiansSymbol = "krad";

    private const string MegaRadiansSpecifier = "Mrad";
    private const string MegaRadiansSymbol = "Mrad";

    private const string GigaRadiansSpecifier = "Grad";
    private const string GigaRadiansSymbol = "Grad";

    private const string TeraRadiansSpecifier = "Trad";
    private const string TeraRadiansSymbol = "Trad";

    private const string PetaRadiansSpecifier = "Prad";
    private const string PetaRadiansSymbol = "Prad";

    private const string ExaRadiansSpecifier = "Erad";
    private const string ExaRadiansSymbol = "Erad";

    private const string ZettaRadiansSpecifier = "Zrad";
    private const string ZettaRadiansSymbol = "Zrad";

    private const string YottaRadiansSpecifier = "Yrad";
    private const string YottaRadiansSymbol = "Yrad";

    private const string RonnaRadiansSpecifier = "Rrad";
    private const string RonnaRadiansSymbol = "Rrad";

    private const string QuettaRadiansSpecifier = "Qrad";
    private const string QuettaRadiansSymbol = "Qrad";

    private const string DegreesSpecifier = "deg";
    private const string DegreesSymbol = "°";

    private const string ArcminutesSpecifier = "arcmin";
    private const string ArcminutesSymbol = "′";

    private const string ArcsecondsSpecifier = "arcsec";
    private const string ArcsecondsSymbol = "″";

    private const string GradiansSpecifier = "gon";
    private const string GradiansSymbol = "gon";

    private const string TurnsSpecifier = "tr";
    private const string TurnsSymbol = "tr";

    private const string ValidSpecifiers =
        "qrad, rrad, yrad, zrad, arad, frad, prad, nrad, urad, mrad, crad, drad, " +
        "rad, darad, hrad, krad, Mrad, Grad, Trad, Prad, Erad, Zrad, Yrad, Rrad, " +
        "Qrad, deg, arcmin, arcsec, gon, and tr";
}
