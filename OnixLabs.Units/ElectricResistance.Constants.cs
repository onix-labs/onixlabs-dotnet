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

public readonly partial struct ElectricResistance<T>
{
    /// <inheritdoc/>
    /// <remarks>Non-zero denominator (1 A) avoids 0/0 = NaN; numerator zero gives a genuine zero magnitude.</remarks>
    public static ElectricResistance<T> Zero => new(ElectricPotential<T>.Zero, Current<T>.FromAmperes(T.One));

    /// <summary>The SI named-unit symbol: <c>Ω</c> (ohm). Accepts SI prefixes via <see cref="NamedUnitAlias"/>, so <c>mΩ</c>/<c>kΩ</c>/<c>MΩ</c>/etc. all work.</summary>
    private const string NamedSymbol = "Ω";

    /// <summary>
    /// Keyboard-friendly ASCII alias accepted on input only: <c>Ohm</c>. So <c>ToString("Ohm:3")</c>, <c>"kOhm:3"</c>,
    /// <c>"mOhm:3"</c>, etc. all parse and render with the canonical <c>Ω</c> symbol.
    /// </summary>
    private const string NamedSymbolAscii = "Ohm";

    private const string DefaultFormat = NamedSymbol;
}
