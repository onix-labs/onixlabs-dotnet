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

public readonly partial struct HeatCapacity<T>
{
    // Result decomposition: `EnergyInJoules(sum) / Temperature.FromKelvin(1)`. With Right.Kelvin = 1, the magnitude
    // formula `Left.Magnitude / Right.Kelvin` reduces to `sum / 1 = sum`, so the round-trip identity holds bit-exactly.

    /// <inheritdoc/>
    public static HeatCapacity<T> Add(HeatCapacity<T> left, HeatCapacity<T> right) =>
        new(EnergyInJoules(left.Magnitude + right.Magnitude), Temperature<T>.FromKelvin(T.One));

    /// <inheritdoc/>
    public static HeatCapacity<T> Subtract(HeatCapacity<T> left, HeatCapacity<T> right) =>
        new(EnergyInJoules(left.Magnitude - right.Magnitude), Temperature<T>.FromKelvin(T.One));

    // Builds an Energy of the given joule magnitude in canonical "(joules N × 1 m)" form. Used by Add/Subtract to
    // construct the result's Energy component without depending on Energy.Arithmetic.cs's private helpers.
    private static Energy<T> EnergyInJoules(T joules) => new(NewtonsForce(joules), Distance<T>.FromMeters(T.One));

    private static Force<T> NewtonsForce(T newtons) => new(
        Mass<T>.FromKilograms(newtons),
        new Acceleration<T>(
            new Speed<T>(Distance<T>.FromMeters(T.One), Time<T>.FromSeconds(T.One)),
            Time<T>.FromSeconds(T.One)));
}
