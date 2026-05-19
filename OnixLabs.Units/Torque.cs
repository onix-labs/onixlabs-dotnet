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
/// Represents a unit of torque — the rotational analogue of force, the product of a force and the perpendicular
/// distance from the axis of rotation (<c>τ = F · r</c>). The SI unit of torque is the Newton-metre (1 N·m = 1 kg·m²/s²).
/// </summary>
/// <typeparam name="T">The underlying <see cref="IFloatingPoint{TSelf}"/> value type.</typeparam>
// ReSharper disable MemberCanBePrivate.Global
#pragma warning disable CA2231
public readonly partial struct Torque<T>(
    in Force<T> left,
    in Distance<T> right
) : IMagnitudinalUnit<T>,
    IAdditiveUnit<Torque<T>>,
    ICompositeUnit<Force<T>, Distance<T>>
    where T : IFloatingPoint<T>
#pragma warning restore CA2231
{
    /// <summary>
    /// Gets the <see cref="Force{T}"/> component of the torque.
    /// </summary>
    public Force<T> Left { get; } = left;

    /// <summary>
    /// Gets the <see cref="Distance{T}"/> component of the torque — the lever-arm length about the axis of rotation.
    /// </summary>
    public Distance<T> Right { get; } = right;

    /// <summary>
    /// Gets the magnitude of the current <see cref="Torque{T}"/> as the product of the force (in Newtons) and
    /// the lever-arm length (in meters), yielding the torque in Newton-metres (N·m).
    /// </summary>
    /// <remarks>
    /// The magnitude is an opaque scalar used by <see cref="Equals(Torque{T})"/>, <see cref="CompareTo(Torque{T})"/>,
    /// <see cref="GetHashCode"/>, and the arithmetic operators. It is not intended for display — use
    /// <see cref="ToString(string, System.IFormatProvider)"/> for that.
    ///
    /// Both components are read at their SI base scale (Force at Newtons via <see cref="Force{T}.Magnitude"/>, Distance
    /// at meters via <see cref="Distance{T}.Meters"/>) so the resulting magnitude lands at the human-readable
    /// Newton-metre scale and round-trips cleanly through arithmetic.
    /// </remarks>
    public T Magnitude => Left.Magnitude * Right.Meters;
}
