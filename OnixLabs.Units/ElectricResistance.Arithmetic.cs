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
    // Result decomposition: `VoltsPotential(sum) / Current.FromAmperes(1)`. With Right.Amperes = 1, the magnitude
    // formula `Left.Magnitude / Right.Amperes` reduces to `sum / 1 = sum`, so the round-trip identity holds bit-exactly.

    /// <inheritdoc/>
    public static ElectricResistance<T> Add(ElectricResistance<T> left, ElectricResistance<T> right) =>
        new(VoltsPotential(left.Magnitude + right.Magnitude), Current<T>.FromAmperes(T.One));

    /// <inheritdoc/>
    public static ElectricResistance<T> Subtract(ElectricResistance<T> left, ElectricResistance<T> right) =>
        new(VoltsPotential(left.Magnitude - right.Magnitude), Current<T>.FromAmperes(T.One));

    // Builds an ElectricPotential of the given volt magnitude in canonical "(volts J × 1 C)" form. The charge
    // denominator is 1 C (1 A × 1 s), so the resulting potential's Magnitude equals `volts / 1 = volts`.
    internal static ElectricPotential<T> VoltsPotential(T volts) => new(EnergyInJoules(volts), OneCoulomb);

    private static Energy<T> EnergyInJoules(T joules) => new(NewtonsForce(joules), Distance<T>.FromMeters(T.One));

    private static Force<T> NewtonsForce(T newtons) => new(
        Mass<T>.FromKilograms(newtons),
        new Acceleration<T>(
            new Speed<T>(Distance<T>.FromMeters(T.One), Time<T>.FromSeconds(T.One)),
            Time<T>.FromSeconds(T.One)));

    private static ElectricCharge<T> OneCoulomb =>
        new(Current<T>.FromAmperes(T.One), Time<T>.FromSeconds(T.One));
}
