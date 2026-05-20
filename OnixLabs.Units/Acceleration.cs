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
/// Represents a unit of acceleration — the rate of change of speed with respect to time, expressed as a
/// <see cref="Speed{T}"/> divided by a <see cref="Time{T}"/>.
/// </summary>
/// <typeparam name="T">The underlying <see cref="IFloatingPoint{TSelf}"/> value type.</typeparam>
// ReSharper disable MemberCanBePrivate.Global
#pragma warning disable CA2231
public readonly partial struct Acceleration<T>(
    in Speed<T> left,
    in Time<T> right
) : IMagnitudinalUnit<T>,
    IAdditiveUnit<Acceleration<T>>,
    ICompositeUnit<Speed<T>, Time<T>>
    where T : IFloatingPoint<T>
#pragma warning restore CA2231
{
    /// <summary>
    /// Gets the <see cref="Speed{T}"/> component of the acceleration.
    /// </summary>
    public Speed<T> Left { get; } = left;

    /// <summary>
    /// Gets the <see cref="Time{T}"/> component of the acceleration — the interval over which the speed changes.
    /// </summary>
    public Time<T> Right { get; } = right;

    // Opaque scalar for equality, ordering, hashing, and arithmetic round-trips. Internal so the assembly (and
    // InternalsVisibleTo'd tests) can read it directly; exposed publicly only via the explicit interface impl.
    // Composed from the inner Speed's opaque Magnitude over Time at SI scale (seconds).
    internal T Magnitude => Left.Magnitude / Right.Seconds;

    /// <inheritdoc/>
    T IMagnitudinalUnit<T>.Magnitude => Magnitude;

    // Display-ready SI base value (m/s²).
    internal T SIBaseValue => Magnitude;

    internal static Acceleration<T> One => new(Speed<T>.One, Time<T>.FromSeconds(T.One));

    internal static Acceleration<T> WithMagnitude(T magnitude) =>
        new(Speed<T>.WithMagnitude(magnitude), Time<T>.FromSeconds(T.One));
}
