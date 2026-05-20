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
/// Represents molar mass — the mass of a substance per unit amount of substance (M = m / n). SI base unit is kg/mol.
/// </summary>
/// <typeparam name="T">The underlying <see cref="IFloatingPoint{TSelf}"/> value type.</typeparam>
// ReSharper disable MemberCanBePrivate.Global
#pragma warning disable CA2231
public readonly partial struct MolarMass<T>(
    in Mass<T> left,
    in AmountOfSubstance<T> right
) : IMagnitudinalUnit<T>,
    IAdditiveUnit<MolarMass<T>>,
    ICompositeUnit<Mass<T>, AmountOfSubstance<T>>
    where T : IFloatingPoint<T>
#pragma warning restore CA2231
{
    /// <summary>Gets the <see cref="Mass{T}"/> component.</summary>
    public Mass<T> Left { get; } = left;

    /// <summary>Gets the <see cref="AmountOfSubstance{T}"/> component.</summary>
    public AmountOfSubstance<T> Right { get; } = right;

    // Opaque scalar for equality, ordering, hashing, and arithmetic round-trips. Internal so the assembly (and
    // InternalsVisibleTo'd tests) can read it directly; exposed publicly only via the explicit interface impl.
    internal T Magnitude => Left.KiloGrams / Right.Moles;

    /// <inheritdoc/>
    T IMagnitudinalUnit<T>.Magnitude => Magnitude;

    internal T SIBaseValue => Magnitude;

    internal static MolarMass<T> One =>
        new(Mass<T>.FromKilograms(T.One), AmountOfSubstance<T>.FromMoles(T.One));

    internal static MolarMass<T> WithMagnitude(T magnitude) =>
        new(Mass<T>.FromKilograms(magnitude), AmountOfSubstance<T>.FromMoles(T.One));
}
