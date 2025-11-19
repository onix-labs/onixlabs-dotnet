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

public readonly partial struct Speed<T>
{
    /// <summary>
    /// Gets a zero <see cref="Speed{T}"/> value, equal to zero quectometers per second.
    /// </summary>
    public static readonly Speed<T> Zero = new(T.Zero);

    private const string QuectoMetersPerSecondSpecifier = "qmps";
    private const string RontoMetersPerSecondSpecifier = "rmps";
    private const string YoctoMetersPerSecondSpecifier = "ymps";
    private const string ZeptoMetersPerSecondSpecifier = "zmps";
    private const string AttoMetersPerSecondSpecifier = "amps";
    private const string FemtoMetersPerSecondSpecifier = "fmps";
    private const string PicoMetersPerSecondSpecifier = "pmps";
    private const string NanoMetersPerSecondSpecifier = "nmps";
    private const string MicroMetersPerSecondSpecifier = "umps";
    private const string MilliMetersPerSecondSpecifier = "mmps";
    private const string CentiMetersPerSecondSpecifier = "cmps";
    private const string DeciMetersPerSecondSpecifier = "dmps";
    private const string MetersPerSecondSpecifier = "mps";
    private const string DecaMetersPerSecondSpecifier = "damps";
    private const string HectoMetersPerSecondSpecifier = "hmps";
    private const string KiloMetersPerSecondSpecifier = "kmps";
    private const string MegaMetersPerSecondSpecifier = "Mmps";
    private const string GigaMetersPerSecondSpecifier = "Gmps";
    private const string TeraMetersPerSecondSpecifier = "Tmps";
    private const string PetaMetersPerSecondSpecifier = "Pmps";
    private const string ExaMetersPerSecondSpecifier = "Emps";
    private const string ZettaMetersPerSecondSpecifier = "Zmps";
    private const string YottaMetersPerSecondSpecifier = "Ymps";
    private const string RonnaMetersPerSecondSpecifier = "Rmps";
    private const string QuettaMetersPerSecondSpecifier = "Qmps";
    private const string InchesPerSecondSpecifier = "ips";
    private const string FeetPerSecondSpecifier = "fps";
    private const string KilometersPerHourSpecifier = "kmph";
    private const string MilesPerHourSpecifier = "mph";
    private const string KnotsSpecifier = "kt";
}
