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

public readonly partial struct Acceleration<T>
{
    // Result decomposition: `Speed(Distance.FromMeters(sum), Time.FromSeconds(1))` over `Time.FromSeconds(1)`. With
    // every component at SI base scale and Right.Seconds = 1, the magnitude formula `Left.Magnitude / Right.Seconds`
    // reduces to `sum / 1 = sum`, so the round-trip `new(...).Magnitude == sum` holds bit-exactly. ToString then reads
    // `Left.Left.ValueOf("m") = sum` and `Right.ValueOf("s") = 1`, giving the human-readable "sum m/s²" rendering.

    /// <inheritdoc/>
    public static Acceleration<T> Add(Acceleration<T> left, Acceleration<T> right)
    {
        T sum = left.Magnitude + right.Magnitude;
        return new Acceleration<T>(
            new Speed<T>(Distance<T>.FromMeters(sum), Time<T>.FromSeconds(T.One)),
            Time<T>.FromSeconds(T.One));
    }

    /// <inheritdoc/>
    public static Acceleration<T> Subtract(Acceleration<T> left, Acceleration<T> right)
    {
        T difference = left.Magnitude - right.Magnitude;
        return new Acceleration<T>(
            new Speed<T>(Distance<T>.FromMeters(difference), Time<T>.FromSeconds(T.One)),
            Time<T>.FromSeconds(T.One));
    }
}
