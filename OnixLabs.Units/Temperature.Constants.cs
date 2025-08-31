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

public readonly partial struct Temperature<T>
{
    /// <summary>
    /// Gets a zero <c>0</c> <see cref="Temperature{T}"/> value, equal to absolute zero, or 0 K.
    /// </summary>
    public static readonly Temperature<T> Zero = new(T.Zero);

    private const string CelsiusSpecifier = "C";
    private const string CelsiusSymbol = "°C";

    private const string DelisleSpecifier = "DE";
    private const string DelisleSymbol = "°De";

    private const string FahrenheitSpecifier = "F";
    private const string FahrenheitSymbol = "°F";

    private const string KelvinSpecifier = "K";
    private const string KelvinSymbol = "K";

    private const string NewtonSpecifier = "N";
    private const string NewtonSymbol = "°N";

    private const string RankineSpecifier = "R";
    private const string RankineSymbol = "°R";

    private const string ReaumurSpecifier = "RE";
    private const string ReaumurSymbol = "°Ré";
}
