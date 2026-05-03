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

public readonly partial struct Voltage<T>
{
    /// <inheritdoc/>
    public static Voltage<T> Zero => new(T.Zero);

    private const string QuectoVoltsSpecifier = "qV";
    private const string QuectoVoltsSymbol = "qV";

    private const string RontoVoltsSpecifier = "rV";
    private const string RontoVoltsSymbol = "rV";

    private const string YoctoVoltsSpecifier = "yV";
    private const string YoctoVoltsSymbol = "yV";

    private const string ZeptoVoltsSpecifier = "zV";
    private const string ZeptoVoltsSymbol = "zV";

    private const string AttoVoltsSpecifier = "aV";
    private const string AttoVoltsSymbol = "aV";

    private const string FemtoVoltsSpecifier = "fV";
    private const string FemtoVoltsSymbol = "fV";

    private const string PicoVoltsSpecifier = "pV";
    private const string PicoVoltsSymbol = "pV";

    private const string NanoVoltsSpecifier = "nV";
    private const string NanoVoltsSymbol = "nV";

    private const string MicroVoltsSpecifier = "uV";
    private const string MicroVoltsSymbol = "µV";

    private const string MilliVoltsSpecifier = "mV";
    private const string MilliVoltsSymbol = "mV";

    private const string CentiVoltsSpecifier = "cV";
    private const string CentiVoltsSymbol = "cV";

    private const string DeciVoltsSpecifier = "dV";
    private const string DeciVoltsSymbol = "dV";

    private const string VoltsSpecifier = "V";
    private const string VoltsSymbol = "V";

    private const string DecaVoltsSpecifier = "daV";
    private const string DecaVoltsSymbol = "daV";

    private const string HectoVoltsSpecifier = "hV";
    private const string HectoVoltsSymbol = "hV";

    private const string KiloVoltsSpecifier = "kV";
    private const string KiloVoltsSymbol = "kV";

    private const string MegaVoltsSpecifier = "MV";
    private const string MegaVoltsSymbol = "MV";

    private const string GigaVoltsSpecifier = "GV";
    private const string GigaVoltsSymbol = "GV";

    private const string TeraVoltsSpecifier = "TV";
    private const string TeraVoltsSymbol = "TV";

    private const string PetaVoltsSpecifier = "PV";
    private const string PetaVoltsSymbol = "PV";

    private const string ExaVoltsSpecifier = "EV";
    private const string ExaVoltsSymbol = "EV";

    private const string ZettaVoltsSpecifier = "ZV";
    private const string ZettaVoltsSymbol = "ZV";

    private const string YottaVoltsSpecifier = "YV";
    private const string YottaVoltsSymbol = "YV";

    private const string RonnaVoltsSpecifier = "RV";
    private const string RonnaVoltsSymbol = "RV";

    private const string QuettaVoltsSpecifier = "QV";
    private const string QuettaVoltsSymbol = "QV";
}
