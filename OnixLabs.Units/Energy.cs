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
/// Represents a unit of energy — the product of force and distance (work done by a force over a distance,
/// <c>E = F · d</c>). The SI unit of energy is the joule (1 J = 1 N·m = 1 kg·m²/s²).
/// </summary>
/// <typeparam name="T">The underlying <see cref="IFloatingPoint{TSelf}"/> value type.</typeparam>
// ReSharper disable MemberCanBePrivate.Global
#pragma warning disable CA2231
public readonly partial struct Energy<T>(
    in Force<T> left,
    in Distance<T> right
) : IMagnitudinalUnit<T>,
    IAdditiveUnit<Energy<T>>,
    ICompositeUnit<Force<T>, Distance<T>>
    where T : IFloatingPoint<T>
#pragma warning restore CA2231
{
    /// <summary>
    /// Gets the <see cref="Force{T}"/> component of the energy.
    /// </summary>
    public Force<T> Left { get; } = left;

    /// <summary>
    /// Gets the <see cref="Distance{T}"/> component of the energy — the displacement over which the force acts.
    /// </summary>
    public Distance<T> Right { get; } = right;

    // Opaque scalar for equality, ordering, hashing, and arithmetic round-trips. Internal so the assembly (and
    // InternalsVisibleTo'd tests) can read it directly; exposed publicly only via the explicit interface impl.
    internal T Magnitude => Left.Magnitude * Right.Meters;

    /// <inheritdoc/>
    T IMagnitudinalUnit<T>.Magnitude => Magnitude;

    // Display-ready SI base value (joules, N·m).
    internal T SIBaseValue => Magnitude;

    internal static Energy<T> One => new(Force<T>.One, Distance<T>.FromMeters(T.One));

    internal static Energy<T> WithMagnitude(T magnitude) =>
        new(Force<T>.WithMagnitude(magnitude), Distance<T>.FromMeters(T.One));
}
