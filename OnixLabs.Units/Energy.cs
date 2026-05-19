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

    /// <summary>
    /// Gets the magnitude of the current <see cref="Energy{T}"/> as the product of the force (in Newtons) and
    /// displacement (in meters), yielding the energy in joules (N·m).
    /// </summary>
    /// <remarks>
    /// The magnitude is an opaque scalar used by <see cref="Equals(Energy{T})"/>, <see cref="CompareTo(Energy{T})"/>,
    /// <see cref="GetHashCode"/>, and the arithmetic operators. It is not intended for display — use
    /// <see cref="ToString(string, System.IFormatProvider)"/> for that.
    ///
    /// Both components are read at their SI base scale (Force at Newtons via <see cref="Force{T}.Magnitude"/>, Distance
    /// at meters via <see cref="Distance{T}.Meters"/>) so the resulting magnitude lands at the human-readable joule
    /// scale and round-trips cleanly through arithmetic.
    /// </remarks>
    public T Magnitude => Left.Magnitude * Right.Meters;
}
