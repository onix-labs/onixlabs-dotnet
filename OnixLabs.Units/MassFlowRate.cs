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
/// Represents mass flow rate — the mass of matter passing per unit time (m-dot = m / t). SI base unit is kg/s.
/// </summary>
/// <typeparam name="T">The underlying <see cref="IFloatingPoint{TSelf}"/> value type.</typeparam>
// ReSharper disable MemberCanBePrivate.Global
#pragma warning disable CA2231
public readonly partial struct MassFlowRate<T>(
    in Mass<T> left,
    in Time<T> right
) : IMagnitudinalUnit<T>,
    IAdditiveUnit<MassFlowRate<T>>,
    ICompositeUnit<Mass<T>, Time<T>>
    where T : IFloatingPoint<T>
#pragma warning restore CA2231
{
    /// <summary>Gets the <see cref="Mass{T}"/> component.</summary>
    public Mass<T> Left { get; } = left;

    /// <summary>Gets the <see cref="Time{T}"/> component.</summary>
    public Time<T> Right { get; } = right;

    // Opaque scalar for equality, ordering, hashing, and arithmetic round-trips. Internal so the assembly (and
    // InternalsVisibleTo'd tests) can read it directly; exposed publicly only via the explicit interface impl.
    internal T Magnitude => Left.KiloGrams / Right.Seconds;

    /// <inheritdoc/>
    T IMagnitudinalUnit<T>.Magnitude => Magnitude;

    internal T SIBaseValue => Magnitude;

    internal static MassFlowRate<T> One => new(Mass<T>.FromKilograms(T.One), Time<T>.FromSeconds(T.One));

    internal static MassFlowRate<T> WithMagnitude(T magnitude) =>
        new(Mass<T>.FromKilograms(magnitude), Time<T>.FromSeconds(T.One));
}
