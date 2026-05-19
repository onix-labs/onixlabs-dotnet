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
/// Represents a unit of electric potential — the energy per unit charge (<c>V = E / Q</c>). The SI unit of electric
/// potential is the volt (1 V = 1 J/C).
/// </summary>
/// <typeparam name="T">The underlying <see cref="IFloatingPoint{TSelf}"/> value type.</typeparam>
// ReSharper disable MemberCanBePrivate.Global
#pragma warning disable CA2231
public readonly partial struct ElectricPotential<T>(
    in Energy<T> left,
    in ElectricCharge<T> right
) : IMagnitudinalUnit<T>,
    IAdditiveUnit<ElectricPotential<T>>,
    ICompositeUnit<Energy<T>, ElectricCharge<T>>
    where T : IFloatingPoint<T>
#pragma warning restore CA2231
{
    /// <summary>Gets the <see cref="Energy{T}"/> component.</summary>
    public Energy<T> Left { get; } = left;

    /// <summary>Gets the <see cref="ElectricCharge{T}"/> component.</summary>
    public ElectricCharge<T> Right { get; } = right;

    /// <summary>
    /// Gets the magnitude as joules divided by coulombs, yielding electric potential in volts.
    /// </summary>
    /// <remarks>Opaque scalar for <see cref="Equals(ElectricPotential{T})"/>, <see cref="CompareTo(ElectricPotential{T})"/>, and arithmetic.</remarks>
    public T Magnitude => Left.Magnitude / Right.Magnitude;
}
