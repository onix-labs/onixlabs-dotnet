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

public readonly partial struct Frequency<T>
{
    /// <inheritdoc/>
    public static Frequency<T> Zero => new(T.Zero);

    private const string QuectoHertzSpecifier = "qHz";
    private const string QuectoHertzSymbol = "qHz";

    private const string RontoHertzSpecifier = "rHz";
    private const string RontoHertzSymbol = "rHz";

    private const string YoctoHertzSpecifier = "yHz";
    private const string YoctoHertzSymbol = "yHz";

    private const string ZeptoHertzSpecifier = "zHz";
    private const string ZeptoHertzSymbol = "zHz";

    private const string AttoHertzSpecifier = "aHz";
    private const string AttoHertzSymbol = "aHz";

    private const string FemtoHertzSpecifier = "fHz";
    private const string FemtoHertzSymbol = "fHz";

    private const string PicoHertzSpecifier = "pHz";
    private const string PicoHertzSymbol = "pHz";

    private const string NanoHertzSpecifier = "nHz";
    private const string NanoHertzSymbol = "nHz";

    private const string MicroHertzSpecifier = "uHz";
    private const string MicroHertzSymbol = "µHz";

    private const string MilliHertzSpecifier = "mHz";
    private const string MilliHertzSymbol = "mHz";

    private const string CentiHertzSpecifier = "cHz";
    private const string CentiHertzSymbol = "cHz";

    private const string DeciHertzSpecifier = "dHz";
    private const string DeciHertzSymbol = "dHz";

    private const string HertzSpecifier = "Hz";
    private const string HertzSymbol = "Hz";

    private const string DecaHertzSpecifier = "daHz";
    private const string DecaHertzSymbol = "daHz";

    private const string HectoHertzSpecifier = "hHz";
    private const string HectoHertzSymbol = "hHz";

    private const string KiloHertzSpecifier = "kHz";
    private const string KiloHertzSymbol = "kHz";

    private const string MegaHertzSpecifier = "MHz";
    private const string MegaHertzSymbol = "MHz";

    private const string GigaHertzSpecifier = "GHz";
    private const string GigaHertzSymbol = "GHz";

    private const string TeraHertzSpecifier = "THz";
    private const string TeraHertzSymbol = "THz";

    private const string PetaHertzSpecifier = "PHz";
    private const string PetaHertzSymbol = "PHz";

    private const string ExaHertzSpecifier = "EHz";
    private const string ExaHertzSymbol = "EHz";

    private const string ZettaHertzSpecifier = "ZHz";
    private const string ZettaHertzSymbol = "ZHz";

    private const string YottaHertzSpecifier = "YHz";
    private const string YottaHertzSymbol = "YHz";

    private const string RonnaHertzSpecifier = "RHz";
    private const string RonnaHertzSymbol = "RHz";

    private const string QuettaHertzSpecifier = "QHz";
    private const string QuettaHertzSymbol = "QHz";

    private const string RevolutionsPerMinuteSpecifier = "rpm";
    private const string RevolutionsPerMinuteSymbol = "rpm";

    private const string BeatsPerMinuteSpecifier = "bpm";
    private const string BeatsPerMinuteSymbol = "bpm";

    private const string RadiansPerSecondSpecifier = "radps";
    private const string RadiansPerSecondSymbol = "rad/s";

    private const string ValidSpecifiers =
        "qHz, rHz, yHz, zHz, aHz, fHz, pHz, nHz, uHz, mHz, cHz, dHz, " +
        "Hz, daHz, hHz, kHz, MHz, GHz, THz, PHz, EHz, ZHz, YHz, RHz, " +
        "QHz, rpm, bpm, and radps";
}
