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

public readonly partial struct Time<T>
{
    /// <inheritdoc/>
    public static Time<T> Zero => new(T.Zero);

    // T-precision conversion factors for non-SI time scales. Each constant is
    // "QuectoSeconds in one X", written as `<seconds-per-unit> × 10^30 qs/s`. Stored as
    // static readonly per closed T so they're computed once and reused at the target type's
    // precision; `T.CreateChecked(6.048e35)` would pre-round through double's ~15-17 digit
    // precision before reaching T.
    private static readonly T QuectosecondsPerMinute = T.CreateChecked(60) * UnitMath.Pow10<T>(30); // 60 s
    private static readonly T QuectosecondsPerHour = T.CreateChecked(3600) * UnitMath.Pow10<T>(30); // 3 600 s
    private static readonly T QuectosecondsPerDay = T.CreateChecked(86400) * UnitMath.Pow10<T>(30); // 86 400 s
    private static readonly T QuectosecondsPerWeek = T.CreateChecked(604800) * UnitMath.Pow10<T>(30); // 604 800 s
    private static readonly T QuectosecondsPerJulianYear = T.CreateChecked(31557600) * UnitMath.Pow10<T>(30); // 365.25 × 86 400 s

    private const string QuectoSecondsSpecifier = "qs";
    private const string RontoSecondsSpecifier = "rs";
    private const string YoctoSecondsSpecifier = "ys";
    private const string ZeptoSecondsSpecifier = "zs";
    private const string AttoSecondsSpecifier = "as";
    private const string FemtoSecondsSpecifier = "fs";
    private const string PicoSecondsSpecifier = "ps";
    private const string NanoSecondsSpecifier = "ns";
    private const string MicroSecondsSpecifier = "us";
    private const string MilliSecondsSpecifier = "ms";
    private const string CentiSecondsSpecifier = "cs";
    private const string DeciSecondsSpecifier = "ds";
    private const string SecondsSpecifier = "s";
    private const string DecaSecondsSpecifier = "das";
    private const string HectoSecondsSpecifier = "hs";
    private const string KiloSecondsSpecifier = "ks";
    private const string MegaSecondsSpecifier = "Ms";
    private const string GigaSecondsSpecifier = "Gs";
    private const string TeraSecondsSpecifier = "Ts";
    private const string PetaSecondsSpecifier = "Ps";
    private const string ExaSecondsSpecifier = "Es";
    private const string ZettaSecondsSpecifier = "Zs";
    private const string YottaSecondsSpecifier = "Ys";
    private const string RonnaSecondsSpecifier = "Rs";
    private const string QuettaSecondsSpecifier = "Qs";
    private const string MinutesSpecifier = "min";
    private const string HoursSpecifier = "h";
    private const string DaysSpecifier = "d";
    private const string WeeksSpecifier = "wk";
    private const string JulianYearsSpecifier = "yr";

    private const string ValidSpecifiers =
        "qs, rs, ys, zs, as, fs, ps, ns, us, ms, cs, ds, " +
        "s, das, hs, ks, Ms, Gs, Ts, Ps, Es, Zs, Ys, Rs, " +
        "Qs, min, h, d, wk, and yr";
}
