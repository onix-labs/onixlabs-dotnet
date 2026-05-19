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
    // Result decomposition: `CoulombsCharge(sum) / VoltsPotential(1)`. With Right.Magnitude = 1 (1-V potential), the
    // magnitude formula `Left.Magnitude / Right.Magnitude` reduces to `sum / 1 = sum`, so the round-trip identity
    // holds bit-exactly.

    /// <inheritdoc/>
    public static ElectricCapacitance<T> Add(ElectricCapacitance<T> left, ElectricCapacitance<T> right) =>
        new(CoulombsCharge(left.Magnitude + right.Magnitude), ElectricResistance<T>.VoltsPotential(T.One));

    /// <inheritdoc/>
    public static ElectricCapacitance<T> Subtract(ElectricCapacitance<T> left, ElectricCapacitance<T> right) =>
        new(CoulombsCharge(left.Magnitude - right.Magnitude), ElectricResistance<T>.VoltsPotential(T.One));

    // Builds an ElectricCharge of the given coulomb magnitude in canonical "(coulombs A × 1 s)" form.
    private static ElectricCharge<T> CoulombsCharge(T coulombs) =>
        new(Current<T>.FromAmperes(coulombs), Time<T>.FromSeconds(T.One));
}
