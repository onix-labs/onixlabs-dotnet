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

    // Opaque scalar for equality, ordering, hashing, and arithmetic round-trips. Internal so the assembly (and
    // InternalsVisibleTo'd tests) can read it directly; exposed publicly only via the explicit interface impl.
    internal T Magnitude => Left.Magnitude * Right.Seconds;

    /// <inheritdoc/>
    T IMagnitudinalUnit<T>.Magnitude => Magnitude;

    // Display-ready SI base value (Newton-seconds, N·s).
    internal T SIBaseValue => Magnitude;

    internal static Impulse<T> One => new(Force<T>.One, Time<T>.FromSeconds(T.One));

    internal static Impulse<T> WithMagnitude(T magnitude) =>
        new(Force<T>.WithMagnitude(magnitude), Time<T>.FromSeconds(T.One));
}
