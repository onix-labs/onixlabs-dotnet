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

using System;
using System.Numerics;
using OnixLabs.Core;

namespace OnixLabs.Units;

public readonly partial struct Frequency<T>
{
    /// <summary>
    /// Gets a zero <see cref="Frequency{T}"/> value, equal to zero yoctohertz.
    /// </summary>
    public static readonly Frequency<T> Zero = new(T.Zero);

    private const string YoctoHertzSpecifier = "yHz";
    private const string ZeptoHertzSpecifier = "zHz";
    private const string AttoHertzSpecifier = "aHz";
    private const string FemtoHertzSpecifier = "fHz";
    private const string PicoHertzSpecifier = "pHz";
    private const string NanoHertzSpecifier = "nHz";
    private const string MicroHertzSpecifier = "µHz";
    private const string MilliHertzSpecifier = "mHz";
    private const string HertzSpecifier = "Hz";
    private const string KiloHertzSpecifier = "kHz";
    private const string MegaHertzSpecifier = "MHz";
    private const string GigaHertzSpecifier = "GHz";
    private const string TeraHertzSpecifier = "THz";
    private const string PetaHertzSpecifier = "PHz";
    private const string ExaHertzSpecifier = "EHz";
    private const string ZettaHertzSpecifier = "ZHz";
    private const string YottaHertzSpecifier = "YHz";
    private const string RevolutionsPerMinuteSpecifier = "rpm";
}
