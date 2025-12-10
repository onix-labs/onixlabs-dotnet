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
    /// <summary>
    /// Gets a zero <c>0</c> <see cref="Distance{T}"/> value.
    /// </summary>
    public static readonly Distance<T> Zero = new(T.Zero);

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
}
