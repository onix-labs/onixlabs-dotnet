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

public readonly partial struct AngularAcceleration<T>
{
    // Result decomposition: AngularVelocityInRadiansPerSecond(sum) / Time.FromSeconds(1). With Right.Seconds = 1, the
    // magnitude formula `Left.Magnitude / Right.Seconds` reduces to `sum / 1 = sum`, so the round-trip identity
    // holds bit-exactly.

    /// <inheritdoc/>
    public static AngularAcceleration<T> Add(AngularAcceleration<T> left, AngularAcceleration<T> right) =>
        new(AngularVelocityInRadiansPerSecond(left.Magnitude + right.Magnitude), Time<T>.FromSeconds(T.One));

    /// <inheritdoc/>
    public static AngularAcceleration<T> Subtract(AngularAcceleration<T> left, AngularAcceleration<T> right) =>
        new(AngularVelocityInRadiansPerSecond(left.Magnitude - right.Magnitude), Time<T>.FromSeconds(T.One));

    // Builds an AngularVelocity of the given rad/s magnitude in canonical "(rad × 1 s)" form.
    private static AngularVelocity<T> AngularVelocityInRadiansPerSecond(T radiansPerSecond) =>
        new(Angle<T>.FromRadians(radiansPerSecond), Time<T>.FromSeconds(T.One));
}
