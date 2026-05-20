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

public readonly partial struct ElectricCapacitance<T>
{
    /// <inheritdoc/>
    /// <remarks>
    /// Non-zero denominator (1 V) avoids 0/0 = NaN; numerator zero gives a genuine zero magnitude. The 1-V
    /// ElectricPotential is built from a 1-J Energy over a 1-C charge so the round-trip identity holds.
    /// </remarks>
    public static ElectricCapacitance<T> Zero => new(ElectricCharge<T>.Zero, ElectricPotential<T>.One);

    /// <summary>The SI named-unit symbol: <c>F</c> (farad). Accepts SI prefixes via <see cref="NamedUnitAlias"/>, so <c>pF</c>/<c>nF</c>/<c>μF</c>/<c>mF</c>/etc. all work.</summary>
    private const string NamedSymbol = "F";

    private const string DefaultFormat = NamedSymbol;
}
