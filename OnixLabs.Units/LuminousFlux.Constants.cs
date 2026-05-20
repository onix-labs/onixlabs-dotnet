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

public readonly partial struct LuminousFlux<T>
{
    /// <inheritdoc/>
    /// <remarks>
    /// As a product composite the magnitude is <c>LuminousIntensity × SolidAngle</c>; zero on either side produces a
    /// genuine zero. A non-zero solid angle (1 sr) is used here for safety/symmetry with quotient composites, though
    /// products tolerate both-zero.
    /// </remarks>
    public static LuminousFlux<T> Zero => new(LuminousIntensity<T>.Zero, SolidAngle<T>.FromSteradians(T.One));

    /// <summary>The SI named-unit symbol: <c>lm</c> (lumen). Accepts SI prefixes via <see cref="NamedUnitAlias"/>.</summary>
    private const string NamedSymbol = "lm";

    private const string DefaultFormat = NamedSymbol;
}
