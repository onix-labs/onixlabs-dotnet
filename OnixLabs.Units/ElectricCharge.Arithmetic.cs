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

public readonly partial struct ElectricCharge<T>
{
    // Result decomposition: `Current.FromAmperes(sum)` × `Time.FromSeconds(1)`. With Right.Seconds = 1, the magnitude
    // formula `Left.Amperes * Right.Seconds` reduces to `sum * 1 = sum`, so the round-trip identity
    // `new(...).Magnitude == sum` holds bit-exactly. ToString then reads `Current.ValueOf("A") = sum` and
    // `Time.ValueOf("s") = 1`, giving the human-readable "sum A*s" rendering (i.e. value in coulombs).

    /// <inheritdoc/>
    public static ElectricCharge<T> Add(ElectricCharge<T> left, ElectricCharge<T> right) =>
        new(Current<T>.FromAmperes(left.Magnitude + right.Magnitude), Time<T>.FromSeconds(T.One));

    /// <inheritdoc/>
    public static ElectricCharge<T> Subtract(ElectricCharge<T> left, ElectricCharge<T> right) =>
        new(Current<T>.FromAmperes(left.Magnitude - right.Magnitude), Time<T>.FromSeconds(T.One));
}
