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

using System.Numerics;

namespace OnixLabs.Units;

/// <summary>
/// Represents a unit of speed.
/// </summary>
/// <typeparam name="T">The underlying <see cref="IFloatingPoint{TSelf}"/> value type.</typeparam>
// ReSharper disable MemberCanBePrivate.Global
#pragma warning disable CA2231
public readonly partial struct Speed<T>(
    in Distance<T> left,
    in Time<T> right
) : IMagnitudinalUnit<T>,
    IAdditiveUnit<Speed<T>>,
    ICompositeUnit<Distance<T>, Time<T>>
    where T : IFloatingPoint<T>
#pragma warning restore CA2231
{
    /// <summary>
    /// Gets the <see cref="Distance{T}"/> component of the speed.
    /// </summary>
    public Distance<T> Left { get; } = left;

    /// <summary>
    /// Gets the <see cref="Time{T}"/> component of the speed.
    /// </summary>
    public Time<T> Right { get; } = right;

    // Opaque scalar used by equality, ordering, hashing, and arithmetic round-trips. Computed from the components'
    // canonical storage. Internal so the rest of the assembly (and InternalsVisibleTo'd tests) can read it directly;
    // the public API exposes it only via the explicit IMagnitudinalUnit<T> implementation below.
    internal T Magnitude => Left.Canonical / Right.Canonical;

    /// <inheritdoc/>
    T IMagnitudinalUnit<T>.Magnitude => Magnitude;

    // Display-ready SI base value (m/s). Used by ToString and tests that assert against a readable scale.
    internal T SIBaseValue => Left.Meters / Right.Seconds;

    // Canonical decomposition with magnitude 1 m/s, used as the round-trip identity by arithmetic.
    internal static Speed<T> One => new(Distance<T>.FromMeters(T.One), Time<T>.FromSeconds(T.One));

    internal static Speed<T> WithMagnitude(T magnitude) =>
        new(Distance<T>.FromMeters(magnitude), Time<T>.FromSeconds(T.One));
}
