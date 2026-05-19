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

public readonly partial struct Illuminance<T>
{
    // Result decomposition: `LumensFlux(sum) / Area.FromSquareMeters(1)`. With Right.SquareMeters = 1, the magnitude
    // formula `Left.Magnitude / Right.SquareMeters` reduces to `sum / 1 = sum`, so the round-trip identity holds
    // bit-exactly.

    /// <inheritdoc/>
    public static Illuminance<T> Add(Illuminance<T> left, Illuminance<T> right) =>
        new(LumensFlux(left.Magnitude + right.Magnitude), Area<T>.FromSquareMeters(T.One));

    /// <inheritdoc/>
    public static Illuminance<T> Subtract(Illuminance<T> left, Illuminance<T> right) =>
        new(LumensFlux(left.Magnitude - right.Magnitude), Area<T>.FromSquareMeters(T.One));

    // Builds a LuminousFlux of the given lumen magnitude in canonical "(cd × 1 rad)" form.
    private static LuminousFlux<T> LumensFlux(T lumens) =>
        new(LuminousIntensity<T>.FromCandelas(lumens), Angle<T>.FromRadians(T.One));
}
