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
    // Result decomposition: `Mass.FromKilograms(sum)` × `Acceleration` with magnitude 1 m/s² (built from
    // `(Speed(Distance.FromMeters(1), Time.FromSeconds(1)), Time.FromSeconds(1))`). With Right.Magnitude = 1, the
    // magnitude formula `Left.KiloGrams * Right.Magnitude` reduces to `sum * 1 = sum`, so the round-trip identity
    // `new(...).Magnitude == sum` holds bit-exactly. ToString then reads `Mass.ValueOf("kg") = sum` and
    // `Acceleration.ValueOf("m/s²") = 1`, giving the human-readable "sum kg*m/s²" rendering.

    /// <inheritdoc/>
    public static Force<T> Add(Force<T> left, Force<T> right)
    {
        T sum = left.Magnitude + right.Magnitude;
        return new Force<T>(Mass<T>.FromKilograms(sum), OneMetrePerSecondSquared);
    }

    /// <inheritdoc/>
    public static Force<T> Subtract(Force<T> left, Force<T> right)
    {
        T difference = left.Magnitude - right.Magnitude;
        return new Force<T>(Mass<T>.FromKilograms(difference), OneMetrePerSecondSquared);
    }

    // Cached unit acceleration (1 m/s²) used as the canonical decomposition denominator for arithmetic results.
    // Cached as a property (rather than a static readonly field) because the inner Distance/Time/Speed factories
    // depend on T and would otherwise require a static constructor or generic helper.
    private static Acceleration<T> OneMetrePerSecondSquared => new(
        new Speed<T>(Distance<T>.FromMeters(T.One), Time<T>.FromSeconds(T.One)),
        Time<T>.FromSeconds(T.One));
}
