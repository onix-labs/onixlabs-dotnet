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
/// Represents molar concentration — the amount of substance per unit volume (c = n / V). SI base unit is mol/m³.
/// </summary>
/// <typeparam name="T">The underlying <see cref="IFloatingPoint{TSelf}"/> value type.</typeparam>
// ReSharper disable MemberCanBePrivate.Global
#pragma warning disable CA2231
public readonly partial struct MolarConcentration<T>(
    in AmountOfSubstance<T> left,
    in Volume<T> right
) : IMagnitudinalUnit<T>,
    IAdditiveUnit<MolarConcentration<T>>,
    ICompositeUnit<AmountOfSubstance<T>, Volume<T>>
    where T : IFloatingPoint<T>
#pragma warning restore CA2231
{
    /// <summary>Gets the <see cref="AmountOfSubstance{T}"/> component.</summary>
    public AmountOfSubstance<T> Left { get; } = left;

    /// <summary>Gets the <see cref="Volume{T}"/> component.</summary>
    public Volume<T> Right { get; } = right;

    /// <summary>Gets the magnitude in mol/m³.</summary>
    /// <remarks>Opaque scalar for equality/comparison/arithmetic.</remarks>
    public T Magnitude => Left.Moles / Right.CubicMeters;
}
