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

public readonly partial struct Impulse<T>
{
    // Result decomposition: `ForceInNewtons(sum)` × `Time.FromSeconds(1)`. With Right.Seconds = 1, the magnitude
    // formula `Left.Magnitude * Right.Seconds` reduces to `sum * 1 = sum`, so the round-trip identity
    // `new(...).Magnitude == sum` holds bit-exactly. ToString then reads `Force.ValueOf("kg*m/s²") = sum` and
    // `Time.ValueOf("s") = 1`, giving the human-readable "sum kg*m/s²*s" rendering (i.e. value in Newton-seconds).

    /// <inheritdoc/>
    public static Impulse<T> Add(Impulse<T> left, Impulse<T> right)
    {
        T sum = left.Magnitude + right.Magnitude;
        return new Impulse<T>(ForceInNewtons(sum), Time<T>.FromSeconds(T.One));
    }

    /// <inheritdoc/>
    public static Impulse<T> Subtract(Impulse<T> left, Impulse<T> right)
    {
        T difference = left.Magnitude - right.Magnitude;
        return new Impulse<T>(ForceInNewtons(difference), Time<T>.FromSeconds(T.One));
    }

    // Builds a Force of the given Newton magnitude in canonical (1-kg × 1-m/s²) form. Used by Add/Subtract to
    // construct the result's Force component without depending on Force.Arithmetic.cs's private helpers.
    private static Force<T> ForceInNewtons(T newtons) => new(
        Mass<T>.FromKilograms(newtons),
        new Acceleration<T>(
            new Speed<T>(Distance<T>.FromMeters(T.One), Time<T>.FromSeconds(T.One)),
            Time<T>.FromSeconds(T.One)));
}
