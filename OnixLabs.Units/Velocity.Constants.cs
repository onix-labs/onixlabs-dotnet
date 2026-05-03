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

public readonly partial struct Velocity<T>
{
    /// <inheritdoc/>
    public static Velocity<T> Zero => new(T.Zero);

    private const string QuectoMetersPerSecondSpecifier = "qmps";
    private const string QuectoMetersPerSecondSymbol = "qm/s";

    private const string RontoMetersPerSecondSpecifier = "rmps";
    private const string RontoMetersPerSecondSymbol = "rm/s";

    private const string YoctoMetersPerSecondSpecifier = "ymps";
    private const string YoctoMetersPerSecondSymbol = "ym/s";

    private const string ZeptoMetersPerSecondSpecifier = "zmps";
    private const string ZeptoMetersPerSecondSymbol = "zm/s";

    private const string AttoMetersPerSecondSpecifier = "amps";
    private const string AttoMetersPerSecondSymbol = "am/s";

    private const string FemtoMetersPerSecondSpecifier = "fmps";
    private const string FemtoMetersPerSecondSymbol = "fm/s";

    private const string PicoMetersPerSecondSpecifier = "pmps";
    private const string PicoMetersPerSecondSymbol = "pm/s";

    private const string NanoMetersPerSecondSpecifier = "nmps";
    private const string NanoMetersPerSecondSymbol = "nm/s";

    private const string MicroMetersPerSecondSpecifier = "umps";
    private const string MicroMetersPerSecondSymbol = "µm/s";

    private const string MilliMetersPerSecondSpecifier = "mmps";
    private const string MilliMetersPerSecondSymbol = "mm/s";

    private const string CentiMetersPerSecondSpecifier = "cmps";
    private const string CentiMetersPerSecondSymbol = "cm/s";

    private const string DeciMetersPerSecondSpecifier = "dmps";
    private const string DeciMetersPerSecondSymbol = "dm/s";

    private const string MetersPerSecondSpecifier = "mps";
    private const string MetersPerSecondSymbol = "m/s";

    private const string DecaMetersPerSecondSpecifier = "damps";
    private const string DecaMetersPerSecondSymbol = "dam/s";

    private const string HectoMetersPerSecondSpecifier = "hmps";
    private const string HectoMetersPerSecondSymbol = "hm/s";

    private const string KiloMetersPerSecondSpecifier = "kmps";
    private const string KiloMetersPerSecondSymbol = "km/s";

    private const string MegaMetersPerSecondSpecifier = "Mmps";
    private const string MegaMetersPerSecondSymbol = "Mm/s";

    private const string GigaMetersPerSecondSpecifier = "Gmps";
    private const string GigaMetersPerSecondSymbol = "Gm/s";

    private const string TeraMetersPerSecondSpecifier = "Tmps";
    private const string TeraMetersPerSecondSymbol = "Tm/s";

    private const string PetaMetersPerSecondSpecifier = "Pmps";
    private const string PetaMetersPerSecondSymbol = "Pm/s";

    private const string ExaMetersPerSecondSpecifier = "Emps";
    private const string ExaMetersPerSecondSymbol = "Em/s";

    private const string ZettaMetersPerSecondSpecifier = "Zmps";
    private const string ZettaMetersPerSecondSymbol = "Zm/s";

    private const string YottaMetersPerSecondSpecifier = "Ymps";
    private const string YottaMetersPerSecondSymbol = "Ym/s";

    private const string RonnaMetersPerSecondSpecifier = "Rmps";
    private const string RonnaMetersPerSecondSymbol = "Rm/s";

    private const string QuettaMetersPerSecondSpecifier = "Qmps";
    private const string QuettaMetersPerSecondSymbol = "Qm/s";

    private const string KilometersPerHourSpecifier = "kmph";
    private const string KilometersPerHourSymbol = "km/h";

    private const string MilesPerHourSpecifier = "mph";
    private const string MilesPerHourSymbol = "mph";

    private const string FeetPerSecondSpecifier = "ftps";
    private const string FeetPerSecondSymbol = "ft/s";

    private const string KnotsSpecifier = "kn";
    private const string KnotsSymbol = "kn";

    private const string InchesPerSecondSpecifier = "inps";
    private const string InchesPerSecondSymbol = "in/s";

    private const string MachSpecifier = "Ma";
    private const string MachSymbol = "Ma";

    private const string SpeedOfLightSpecifier = "c";
    private const string SpeedOfLightSymbol = "c";
}
