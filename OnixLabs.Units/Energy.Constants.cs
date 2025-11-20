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

public readonly partial struct Energy<T>
{
    /// <summary>
    /// Represents an empty <see cref="Energy{T}"/> value.
    /// </summary>
    public static readonly Energy<T> Zero = new(T.Zero);

    private const string JoulesSpecifier = "J";
    private const string JoulesSymbol = "J";

    private const string KiloJoulesSpecifier = "KJ";
    private const string KiloJoulesSymbol = "kJ";

    private const string MegaJoulesSpecifier = "MJ";
    private const string MegaJoulesSymbol = "MJ";

    private const string GigaJoulesSpecifier = "GJ";
    private const string GigaJoulesSymbol = "GJ";

    private const string CaloriesSpecifier = "CAL";
    private const string CaloriesSymbol = "cal";

    private const string KiloCaloriesSpecifier = "KCAL";
    private const string KiloCaloriesSymbol = "kcal";

    private const string WattHoursSpecifier = "WH";
    private const string WattHoursSymbol = "Wh";

    private const string KiloWattHoursSpecifier = "KWH";
    private const string KiloWattHoursSymbol = "kWh";

    private const string ErgsSpecifier = "ERG";
    private const string ErgsSymbol = "erg";

    private const string BritishThermalUnitsSpecifier = "BTU";
    private const string BritishThermalUnitsSymbol = "BTU";

    private const string FootPoundsSpecifier = "FTLB";
    private const string FootPoundsSymbol = "ft·lbf";

    private const string ElectronVoltsSpecifier = "EV";
    private const string ElectronVoltsSymbol = "eV";
}
