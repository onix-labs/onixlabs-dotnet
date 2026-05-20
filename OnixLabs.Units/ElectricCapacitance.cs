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
/// Represents electric capacitance — charge stored per unit electric potential (<c>C = Q / V</c>). The SI unit of
/// electric capacitance is the farad (1 F = 1 C/V).
/// </summary>
/// <typeparam name="T">The underlying <see cref="IFloatingPoint{TSelf}"/> value type.</typeparam>
// ReSharper disable MemberCanBePrivate.Global
#pragma warning disable CA2231
public readonly partial struct ElectricCapacitance<T>(
    in ElectricCharge<T> left,
    in ElectricPotential<T> right
) : IMagnitudinalUnit<T>,
    IAdditiveUnit<ElectricCapacitance<T>>,
    ICompositeUnit<ElectricCharge<T>, ElectricPotential<T>>
    where T : IFloatingPoint<T>
#pragma warning restore CA2231
{
    /// <summary>Gets the <see cref="ElectricCharge{T}"/> component.</summary>
    public ElectricCharge<T> Left { get; } = left;

    /// <summary>Gets the <see cref="ElectricPotential{T}"/> component.</summary>
    public ElectricPotential<T> Right { get; } = right;

    // Opaque scalar for equality, ordering, hashing, and arithmetic round-trips. Internal so the assembly (and
    // InternalsVisibleTo'd tests) can read it directly; exposed publicly only via the explicit interface impl.
    internal T Magnitude => Left.Magnitude / Right.Magnitude;

    /// <inheritdoc/>
    T IMagnitudinalUnit<T>.Magnitude => Magnitude;

    // Display-ready SI base value (farads, C/V).
    internal T SIBaseValue => Magnitude;

    internal static ElectricCapacitance<T> One => new(ElectricCharge<T>.One, ElectricPotential<T>.One);

    internal static ElectricCapacitance<T> WithMagnitude(T magnitude) =>
        new(ElectricCharge<T>.WithMagnitude(magnitude), ElectricPotential<T>.One);
}
