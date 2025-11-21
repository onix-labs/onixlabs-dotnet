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

public readonly partial struct Power<T>
{
    /// <summary>
    /// Gets a zero <see cref="Power{T}"/> value, equal to zero yoctowatts.
    /// </summary>
    public static readonly Power<T> Zero = new(T.Zero);

    private const string YoctoWattsSpecifier = "yW";
    private const string ZeptoWattsSpecifier = "zW";
    private const string AttoWattsSpecifier = "aW";
    private const string FemtoWattsSpecifier = "fW";
    private const string PicoWattsSpecifier = "pW";
    private const string NanoWattsSpecifier = "nW";
    private const string MicroWattsSpecifier = "uW";
    private const string MilliWattsSpecifier = "mW";
    private const string WattsSpecifier = "W";
    private const string KiloWattsSpecifier = "kW";
    private const string MegaWattsSpecifier = "MW";
    private const string GigaWattsSpecifier = "GW";
    private const string TeraWattsSpecifier = "TW";
    private const string PetaWattsSpecifier = "PW";
    private const string ExaWattsSpecifier = "EW";
    private const string ZettaWattsSpecifier = "ZW";
    private const string YottaWattsSpecifier = "YW";
    private const string HorsepowerSpecifier = "hp";
    private const string MetricHorsepowerSpecifier = "hpM";
}
