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
    // Result decomposition: `LuminousIntensity.FromCandelas(sum)` × `SolidAngle.FromSteradians(1)`. With
    // Right.Steradians = 1, the magnitude formula `Left.Candelas * Right.Steradians` reduces to `sum * 1 = sum`,
    // so the round-trip identity `new(...).Magnitude == sum` holds bit-exactly. ToString then reads
    // `LuminousIntensity.ValueOf("cd") = sum` and `SolidAngle.ValueOf("sr") = 1`, giving the human-readable
    // "sum cd*sr" rendering (i.e. value in lumens).

    /// <inheritdoc/>
    public static LuminousFlux<T> Add(LuminousFlux<T> left, LuminousFlux<T> right) =>
        new(LuminousIntensity<T>.FromCandelas(left.Magnitude + right.Magnitude), SolidAngle<T>.FromSteradians(T.One));

    /// <inheritdoc/>
    public static LuminousFlux<T> Subtract(LuminousFlux<T> left, LuminousFlux<T> right) =>
        new(LuminousIntensity<T>.FromCandelas(left.Magnitude - right.Magnitude), SolidAngle<T>.FromSteradians(T.One));
}
