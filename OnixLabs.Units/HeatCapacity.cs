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
/// Represents heat capacity — the energy required per unit change in temperature (<c>C = E / ΔT</c>). The SI unit is
/// the joule per kelvin (1 J/K).
/// </summary>
/// <typeparam name="T">The underlying <see cref="IFloatingPoint{TSelf}"/> value type.</typeparam>
// ReSharper disable MemberCanBePrivate.Global
#pragma warning disable CA2231
public readonly partial struct HeatCapacity<T>(
    in Energy<T> left,
    in Temperature<T> right
) : IMagnitudinalUnit<T>,
    IAdditiveUnit<HeatCapacity<T>>,
    ICompositeUnit<Energy<T>, Temperature<T>>
    where T : IFloatingPoint<T>
#pragma warning restore CA2231
{
    /// <summary>Gets the <see cref="Energy{T}"/> component.</summary>
    public Energy<T> Left { get; } = left;

    /// <summary>Gets the <see cref="Temperature{T}"/> component.</summary>
    public Temperature<T> Right { get; } = right;

    // Opaque scalar for equality, ordering, hashing, and arithmetic round-trips. Internal so the assembly (and
    // InternalsVisibleTo'd tests) can read it directly; exposed publicly only via the explicit interface impl.
    internal T Magnitude => Left.Magnitude / Right.Kelvin;

    /// <inheritdoc/>
    T IMagnitudinalUnit<T>.Magnitude => Magnitude;

    // Display-ready SI base value (J/K).
    internal T SIBaseValue => Magnitude;

    internal static HeatCapacity<T> One => new(Energy<T>.One, Temperature<T>.FromKelvin(T.One));

    internal static HeatCapacity<T> WithMagnitude(T magnitude) =>
        new(Energy<T>.WithMagnitude(magnitude), Temperature<T>.FromKelvin(T.One));
}
