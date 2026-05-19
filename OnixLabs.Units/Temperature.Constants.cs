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
    /// <inheritdoc/>
    public static Temperature<T> Zero => new(T.Zero);

    // T-precision rational constants. Stored as static readonly per closed T so they're
    // computed once and reused. `T.CreateChecked(<decimal>)` would silently pre-round through
    // double's ~15-17 digit precision before reaching T; the rational form
    // `T.CreateChecked(N) / T.CreateChecked(D)` produces the closest T value to the rational
    // at whatever precision T offers.
    private static readonly T CelsiusKelvinOffset = T.CreateChecked(27315) / T.CreateChecked(100);    // 273.15
    private static readonly T DelisleKelvinReference = T.CreateChecked(37315) / T.CreateChecked(100); // 373.15
    private static readonly T FahrenheitKelvinOffset = T.CreateChecked(45967) / T.CreateChecked(100); // 459.67
    private static readonly T NineFifths = T.CreateChecked(9) / T.CreateChecked(5);                   // 1.8
    private static readonly T FourFifths = T.CreateChecked(4) / T.CreateChecked(5);                   // 0.8
    private static readonly T TwentyOneFortieths = T.CreateChecked(21) / T.CreateChecked(40);         // 0.525
    private static readonly T FortyTwentyFirsts = T.CreateChecked(40) / T.CreateChecked(21);          // ~1.9048

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

    private const string RomerSpecifier = "RO";
    private const string RomerSymbol = "°Rø";

    private const string ValidSpecifiers = "C, De, F, K, N, R, Re, and Ro";
}
