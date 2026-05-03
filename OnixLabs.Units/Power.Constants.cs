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
    /// <inheritdoc/>
    public static Power<T> Zero => new(T.Zero);

    private const string QuectoWattsSpecifier = "qW";
    private const string QuectoWattsSymbol = "qW";

    private const string RontoWattsSpecifier = "rW";
    private const string RontoWattsSymbol = "rW";

    private const string YoctoWattsSpecifier = "yW";
    private const string YoctoWattsSymbol = "yW";

    private const string ZeptoWattsSpecifier = "zW";
    private const string ZeptoWattsSymbol = "zW";

    private const string AttoWattsSpecifier = "aW";
    private const string AttoWattsSymbol = "aW";

    private const string FemtoWattsSpecifier = "fW";
    private const string FemtoWattsSymbol = "fW";

    private const string PicoWattsSpecifier = "pW";
    private const string PicoWattsSymbol = "pW";

    private const string NanoWattsSpecifier = "nW";
    private const string NanoWattsSymbol = "nW";

    private const string MicroWattsSpecifier = "uW";
    private const string MicroWattsSymbol = "µW";

    private const string MilliWattsSpecifier = "mW";
    private const string MilliWattsSymbol = "mW";

    private const string CentiWattsSpecifier = "cW";
    private const string CentiWattsSymbol = "cW";

    private const string DeciWattsSpecifier = "dW";
    private const string DeciWattsSymbol = "dW";

    private const string WattsSpecifier = "W";
    private const string WattsSymbol = "W";

    private const string DecaWattsSpecifier = "daW";
    private const string DecaWattsSymbol = "daW";

    private const string HectoWattsSpecifier = "hW";
    private const string HectoWattsSymbol = "hW";

    private const string KiloWattsSpecifier = "kW";
    private const string KiloWattsSymbol = "kW";

    private const string MegaWattsSpecifier = "MW";
    private const string MegaWattsSymbol = "MW";

    private const string GigaWattsSpecifier = "GW";
    private const string GigaWattsSymbol = "GW";

    private const string TeraWattsSpecifier = "TW";
    private const string TeraWattsSymbol = "TW";

    private const string PetaWattsSpecifier = "PW";
    private const string PetaWattsSymbol = "PW";

    private const string ExaWattsSpecifier = "EW";
    private const string ExaWattsSymbol = "EW";

    private const string ZettaWattsSpecifier = "ZW";
    private const string ZettaWattsSymbol = "ZW";

    private const string YottaWattsSpecifier = "YW";
    private const string YottaWattsSymbol = "YW";

    private const string RonnaWattsSpecifier = "RW";
    private const string RonnaWattsSymbol = "RW";

    private const string QuettaWattsSpecifier = "QW";
    private const string QuettaWattsSymbol = "QW";

    private const string MechanicalHorsepowerSpecifier = "hp";
    private const string MechanicalHorsepowerSymbol = "hp";

    private const string MetricHorsepowerSpecifier = "PS";
    private const string MetricHorsepowerSymbol = "PS";

    private const string BtusPerHourSpecifier = "BTUph";
    private const string BtusPerHourSymbol = "BTU/h";

    private const string CaloriesPerSecondSpecifier = "calps";
    private const string CaloriesPerSecondSymbol = "cal/s";

    private const string ErgsPerSecondSpecifier = "ergps";
    private const string ErgsPerSecondSymbol = "erg/s";

    private const string FootPoundsPerSecondSpecifier = "ftlbfps";
    private const string FootPoundsPerSecondSymbol = "ft·lbf/s";

    private const string TonsOfRefrigerationSpecifier = "tref";
    private const string TonsOfRefrigerationSymbol = "TR";
}
