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
/// Represents a unit of impulse — the product of a force and the time interval over which it acts
/// (<c>J = F · Δt</c>). The SI unit of impulse is the Newton-second (1 N·s = 1 kg·m/s).
/// </summary>
/// <typeparam name="T">The underlying <see cref="IFloatingPoint{TSelf}"/> value type.</typeparam>
// ReSharper disable MemberCanBePrivate.Global
#pragma warning disable CA2231
public readonly partial struct Impulse<T>(
    in Force<T> left,
    in Time<T> right
) : IMagnitudinalUnit<T>,
    IAdditiveUnit<Impulse<T>>,
    ICompositeUnit<Force<T>, Time<T>>
    where T : IFloatingPoint<T>
#pragma warning restore CA2231
{
    /// <summary>
    /// Gets the <see cref="Force{T}"/> component of the impulse.
    /// </summary>
    public Force<T> Left { get; } = left;

    /// <summary>
    /// Gets the <see cref="Time{T}"/> component of the impulse — the interval over which the force acts.
    /// </summary>
    public Time<T> Right { get; } = right;

    /// <summary>
    /// Gets the magnitude of the current <see cref="Impulse{T}"/> as the product of the force (in Newtons) and
    /// the duration (in seconds), yielding the impulse in Newton-seconds (N·s).
    /// </summary>
    /// <remarks>
    /// The magnitude is an opaque scalar used by <see cref="Equals(Impulse{T})"/>, <see cref="CompareTo(Impulse{T})"/>,
    /// <see cref="GetHashCode"/>, and the arithmetic operators. It is not intended for display — use
    /// <see cref="ToString(string, System.IFormatProvider)"/> for that.
    ///
    /// Both components are read at their SI base scale (Force at Newtons via <see cref="Force{T}.Magnitude"/>, Time
    /// at seconds via <see cref="Time{T}.Seconds"/>) so the resulting magnitude lands at the human-readable
    /// Newton-second scale and round-trips cleanly through arithmetic.
    /// </remarks>
    public T Magnitude => Left.Magnitude * Right.Seconds;
}
