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

public readonly partial struct Force<T>
{
    /// <inheritdoc/>
    /// <remarks>
    /// As a product composite the magnitude is <c>Mass × Acceleration</c>; zero on either side produces a genuine zero
    /// without the <c>0 / 0 = NaN</c> hazard that quotient composites must guard against. Using <c>Mass.Zero</c> and
    /// <c>Acceleration.Zero</c> is therefore safe.
    /// </remarks>
    public static Force<T> Zero => new(Mass<T>.Zero, Acceleration<T>.Zero);

    /// <summary>The SI named-unit symbol: <c>N</c> (newton). Accepts SI prefixes via <see cref="NamedUnitAlias"/>.</summary>
    private const string NamedSymbol = "N";

    private const string DefaultFormat = NamedSymbol;
}
