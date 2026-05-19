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
/// Represents electric resistance — electric potential per unit current (<c>R = V / I</c>). The SI unit of electric
/// resistance is the ohm (1 Ω = 1 V/A).
/// </summary>
/// <typeparam name="T">The underlying <see cref="IFloatingPoint{TSelf}"/> value type.</typeparam>
// ReSharper disable MemberCanBePrivate.Global
#pragma warning disable CA2231
public readonly partial struct ElectricResistance<T>(
    in ElectricPotential<T> left,
    in Current<T> right
) : IMagnitudinalUnit<T>,
    IAdditiveUnit<ElectricResistance<T>>,
    ICompositeUnit<ElectricPotential<T>, Current<T>>
    where T : IFloatingPoint<T>
#pragma warning restore CA2231
{
    /// <summary>Gets the <see cref="ElectricPotential{T}"/> component.</summary>
    public ElectricPotential<T> Left { get; } = left;

    /// <summary>Gets the <see cref="Current{T}"/> component.</summary>
    public Current<T> Right { get; } = right;

    /// <summary>
    /// Gets the magnitude as volts divided by amperes, yielding electric resistance in ohms.
    /// </summary>
    /// <remarks>Opaque scalar for <see cref="Equals(ElectricResistance{T})"/>, <see cref="CompareTo(ElectricResistance{T})"/>, and arithmetic.</remarks>
    public T Magnitude => Left.Magnitude / Right.Amperes;
}
