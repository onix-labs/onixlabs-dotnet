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

public readonly partial struct SolidAngle<T>
{
    /// <inheritdoc/>
    public static SolidAngle<T> Zero => new(T.Zero);

    // T-precision π-based conversion factors for non-steradian solid-angle scales. Each is "QuectoSteradians in one X",
    // computed at T's precision so π² keeps its full T.Pi precision (no double pre-rounding). Square Degree =
    // (π/180)² sr, Square Arcminute = (π/10800)² sr, Square Arcsecond = (π/648000)² sr, Spat = 4π sr.
    private static readonly T QuectoSteradiansPerSquareDegree = T.Pi * T.Pi * UnitMath.Pow10<T>(30) / T.CreateChecked(32400); // π²/180² sr
    private static readonly T QuectoSteradiansPerSquareArcminute = T.Pi * T.Pi * UnitMath.Pow10<T>(30) / T.CreateChecked(116640000L); // π²/10800² sr
    private static readonly T QuectoSteradiansPerSquareArcsecond = T.Pi * T.Pi * UnitMath.Pow10<T>(30) / T.CreateChecked(419904000000L); // π²/648000² sr
    private static readonly T QuectoSteradiansPerSpat = T.Pi * UnitMath.Pow10<T>(30) * T.CreateChecked(4); // 4π sr

    private const string QuectoSteradiansSpecifier = "qsr";
    private const string QuectoSteradiansSymbol = "qsr";

    private const string RontoSteradiansSpecifier = "rsr";
    private const string RontoSteradiansSymbol = "rsr";

    private const string YoctoSteradiansSpecifier = "ysr";
    private const string YoctoSteradiansSymbol = "ysr";

    private const string ZeptoSteradiansSpecifier = "zsr";
    private const string ZeptoSteradiansSymbol = "zsr";

    private const string AttoSteradiansSpecifier = "asr";
    private const string AttoSteradiansSymbol = "asr";

    private const string FemtoSteradiansSpecifier = "fsr";
    private const string FemtoSteradiansSymbol = "fsr";

    private const string PicoSteradiansSpecifier = "psr";
    private const string PicoSteradiansSymbol = "psr";

    private const string NanoSteradiansSpecifier = "nsr";
    private const string NanoSteradiansSymbol = "nsr";

    private const string MicroSteradiansSpecifier = "usr";
    private const string MicroSteradiansSymbol = "µsr";

    private const string MilliSteradiansSpecifier = "msr";
    private const string MilliSteradiansSymbol = "msr";

    private const string CentiSteradiansSpecifier = "csr";
    private const string CentiSteradiansSymbol = "csr";

    private const string DeciSteradiansSpecifier = "dsr";
    private const string DeciSteradiansSymbol = "dsr";

    private const string SteradiansSpecifier = "sr";
    private const string SteradiansSymbol = "sr";

    private const string DecaSteradiansSpecifier = "dasr";
    private const string DecaSteradiansSymbol = "dasr";

    private const string HectoSteradiansSpecifier = "hsr";
    private const string HectoSteradiansSymbol = "hsr";

    private const string KiloSteradiansSpecifier = "ksr";
    private const string KiloSteradiansSymbol = "ksr";

    private const string MegaSteradiansSpecifier = "Msr";
    private const string MegaSteradiansSymbol = "Msr";

    private const string GigaSteradiansSpecifier = "Gsr";
    private const string GigaSteradiansSymbol = "Gsr";

    private const string TeraSteradiansSpecifier = "Tsr";
    private const string TeraSteradiansSymbol = "Tsr";

    private const string PetaSteradiansSpecifier = "Psr";
    private const string PetaSteradiansSymbol = "Psr";

    private const string ExaSteradiansSpecifier = "Esr";
    private const string ExaSteradiansSymbol = "Esr";

    private const string ZettaSteradiansSpecifier = "Zsr";
    private const string ZettaSteradiansSymbol = "Zsr";

    private const string YottaSteradiansSpecifier = "Ysr";
    private const string YottaSteradiansSymbol = "Ysr";

    private const string RonnaSteradiansSpecifier = "Rsr";
    private const string RonnaSteradiansSymbol = "Rsr";

    private const string QuettaSteradiansSpecifier = "Qsr";
    private const string QuettaSteradiansSymbol = "Qsr";

    private const string SquareDegreesSpecifier = "sqdeg";
    private const string SquareDegreesSymbol = "deg²";

    private const string SquareArcminutesSpecifier = "sqarcmin";
    private const string SquareArcminutesSymbol = "arcmin²";

    private const string SquareArcsecondsSpecifier = "sqarcsec";
    private const string SquareArcsecondsSymbol = "arcsec²";

    private const string SpatsSpecifier = "sp";
    private const string SpatsSymbol = "sp";

    private const string ValidSpecifiers =
        "qsr, rsr, ysr, zsr, asr, fsr, psr, nsr, usr, msr, csr, dsr, " +
        "sr, dasr, hsr, ksr, Msr, Gsr, Tsr, Psr, Esr, Zsr, Ysr, Rsr, " +
        "Qsr, sqdeg, sqarcmin, sqarcsec, and sp";
}
